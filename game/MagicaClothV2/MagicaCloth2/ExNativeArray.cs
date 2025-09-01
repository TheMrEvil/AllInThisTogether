using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Mathematics;

namespace MagicaCloth2
{
	// Token: 0x020000E8 RID: 232
	public class ExNativeArray<[IsUnmanaged] T> : IDisposable where T : struct, ValueType
	{
		// Token: 0x060003E2 RID: 994 RVA: 0x00020C93 File Offset: 0x0001EE93
		public void Dispose()
		{
			if (this.nativeArray.IsCreated)
			{
				this.nativeArray.Dispose();
			}
			this.emptyChunks.Clear();
			this.useCount = 0;
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x060003E3 RID: 995 RVA: 0x00020CBF File Offset: 0x0001EEBF
		public bool IsValid
		{
			get
			{
				return this.nativeArray.IsCreated;
			}
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x060003E4 RID: 996 RVA: 0x00020CCC File Offset: 0x0001EECC
		public int Length
		{
			get
			{
				if (!this.nativeArray.IsCreated)
				{
					return 0;
				}
				return this.nativeArray.Length;
			}
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x060003E5 RID: 997 RVA: 0x00020CE8 File Offset: 0x0001EEE8
		public int Count
		{
			get
			{
				return this.useCount;
			}
		}

		// Token: 0x060003E6 RID: 998 RVA: 0x00020CF0 File Offset: 0x0001EEF0
		public ExNativeArray()
		{
		}

		// Token: 0x060003E7 RID: 999 RVA: 0x00020D04 File Offset: 0x0001EF04
		public ExNativeArray(int emptyLength, bool create = false) : this()
		{
			if (emptyLength > 0)
			{
				this.nativeArray = new NativeArray<T>(emptyLength, Allocator.Persistent, NativeArrayOptions.ClearMemory);
				DataChunk item = new DataChunk(0, emptyLength);
				this.emptyChunks.Add(item);
				if (create)
				{
					this.AddRange(emptyLength);
					return;
				}
			}
			else if (create)
			{
				this.nativeArray = new NativeArray<T>(0, Allocator.Persistent, NativeArrayOptions.ClearMemory);
			}
		}

		// Token: 0x060003E8 RID: 1000 RVA: 0x00020D5B File Offset: 0x0001EF5B
		public ExNativeArray(int emptyLength, T fillData) : this(emptyLength, false)
		{
			if (emptyLength > 0)
			{
				this.Fill(fillData);
			}
		}

		// Token: 0x060003E9 RID: 1001 RVA: 0x00020D70 File Offset: 0x0001EF70
		public ExNativeArray(NativeArray<T> dataArray) : this()
		{
			this.AddRange<T>(dataArray);
		}

		// Token: 0x060003EA RID: 1002 RVA: 0x00020D80 File Offset: 0x0001EF80
		public ExNativeArray(T[] dataArray) : this()
		{
			this.AddRange(dataArray);
		}

		// Token: 0x060003EB RID: 1003 RVA: 0x00020D90 File Offset: 0x0001EF90
		public DataChunk AddRange(int dataLength)
		{
			DataChunk emptyChunk = this.GetEmptyChunk(dataLength);
			if (!emptyChunk.IsValid)
			{
				int length = this.Length;
				int num = this.Length + math.max(dataLength, length);
				if (length == 0)
				{
					if (this.nativeArray.IsCreated)
					{
						this.nativeArray.Dispose();
					}
					this.nativeArray = new NativeArray<T>(num, Allocator.Persistent, NativeArrayOptions.ClearMemory);
					emptyChunk.dataLength = dataLength;
				}
				else
				{
					NativeArray<T> dst = new NativeArray<T>(num, Allocator.Persistent, NativeArrayOptions.ClearMemory);
					NativeArray<T>.Copy(this.nativeArray, dst, length);
					this.nativeArray.Dispose();
					this.nativeArray = dst;
					emptyChunk.startIndex = length;
					emptyChunk.dataLength = dataLength;
					int num2 = length + dataLength;
					if (num2 < num)
					{
						DataChunk chunk = new DataChunk(num2, num - num2);
						this.AddEmptyChunk(chunk);
					}
				}
			}
			this.useCount = math.max(this.useCount, emptyChunk.startIndex + emptyChunk.dataLength);
			return emptyChunk;
		}

		// Token: 0x060003EC RID: 1004 RVA: 0x00020E74 File Offset: 0x0001F074
		public DataChunk AddRange(int dataLength, T fillData = default(T))
		{
			DataChunk dataChunk = this.AddRange(dataLength);
			this.Fill(dataChunk, fillData);
			return dataChunk;
		}

		// Token: 0x060003ED RID: 1005 RVA: 0x00020E94 File Offset: 0x0001F094
		public DataChunk AddRange(T[] array)
		{
			if (array == null || array.Length == 0)
			{
				return DataChunk.Empty;
			}
			int num = array.Length;
			DataChunk dataChunk = this.AddRange(num);
			NativeArray<T>.Copy(array, 0, this.nativeArray, dataChunk.startIndex, num);
			return dataChunk;
		}

		// Token: 0x060003EE RID: 1006 RVA: 0x00020ED0 File Offset: 0x0001F0D0
		public DataChunk AddRange(NativeArray<T> narray, int length = 0)
		{
			if (!narray.IsCreated || narray.Length == 0)
			{
				return DataChunk.Empty;
			}
			int num = (length > 0) ? length : narray.Length;
			DataChunk dataChunk = this.AddRange(num);
			NativeArray<T>.Copy(narray, 0, this.nativeArray, dataChunk.startIndex, num);
			return dataChunk;
		}

		// Token: 0x060003EF RID: 1007 RVA: 0x00020F21 File Offset: 0x0001F121
		public DataChunk AddRange(ExNativeArray<T> exarray)
		{
			return this.AddRange(exarray.GetNativeArray(), exarray.Count);
		}

		// Token: 0x060003F0 RID: 1008 RVA: 0x00020F35 File Offset: 0x0001F135
		public DataChunk AddRange(ExSimpleNativeArray<T> exarray)
		{
			return this.AddRange(exarray.GetNativeArray(), exarray.Count);
		}

		// Token: 0x060003F1 RID: 1009 RVA: 0x00020F4C File Offset: 0x0001F14C
		public unsafe DataChunk AddRange<U>(U[] array) where U : struct
		{
			if (array == null || array.Length == 0)
			{
				return DataChunk.Empty;
			}
			int num = UnsafeUtility.SizeOf<T>();
			int num2 = array.Length;
			DataChunk dataChunk = this.AddRange(num2);
			ulong gcHandle;
			void* source = UnsafeUtility.PinGCArrayAndGetDataAddress(array, out gcHandle);
			byte* unsafePtr = (byte*)this.nativeArray.GetUnsafePtr<T>();
			UnsafeUtility.MemCpy((void*)(unsafePtr + dataChunk.startIndex * num), source, (long)(num2 * num));
			UnsafeUtility.ReleaseGCObject(gcHandle);
			return dataChunk;
		}

		// Token: 0x060003F2 RID: 1010 RVA: 0x00020FAC File Offset: 0x0001F1AC
		public DataChunk AddRange<U>(NativeArray<U> udata) where U : struct
		{
			if (!udata.IsCreated || udata.Length == 0)
			{
				return DataChunk.Empty;
			}
			int length = udata.Length;
			DataChunk dataChunk = this.AddRange(length);
			NativeArray<T>.Copy(udata.Reinterpret<T>(), 0, this.nativeArray, dataChunk.startIndex, length);
			return dataChunk;
		}

		// Token: 0x060003F3 RID: 1011 RVA: 0x00020FFC File Offset: 0x0001F1FC
		public unsafe DataChunk AddRangeTypeChange<U>(U[] array) where U : struct
		{
			if (array == null || array.Length == 0)
			{
				return DataChunk.Empty;
			}
			int num = UnsafeUtility.SizeOf<U>();
			int num2 = UnsafeUtility.SizeOf<T>();
			int num3 = array.Length * num / num2;
			DataChunk dataChunk = this.AddRange(num3);
			ulong gcHandle;
			void* source = UnsafeUtility.PinGCArrayAndGetDataAddress(array, out gcHandle);
			byte* unsafePtr = (byte*)this.nativeArray.GetUnsafePtr<T>();
			UnsafeUtility.MemCpy((void*)(unsafePtr + dataChunk.startIndex * num2), source, (long)(num3 * num2));
			UnsafeUtility.ReleaseGCObject(gcHandle);
			return dataChunk;
		}

		// Token: 0x060003F4 RID: 1012 RVA: 0x00021068 File Offset: 0x0001F268
		public unsafe DataChunk AddRangeStride<U>(U[] array) where U : struct
		{
			if (array == null || array.Length == 0)
			{
				return DataChunk.Empty;
			}
			int num = UnsafeUtility.SizeOf<U>();
			int num2 = UnsafeUtility.SizeOf<T>();
			int num3 = array.Length;
			DataChunk dataChunk = this.AddRange(num3);
			ulong gcHandle;
			void* source = UnsafeUtility.PinGCArrayAndGetDataAddress(array, out gcHandle);
			byte* unsafePtr = (byte*)this.nativeArray.GetUnsafePtr<T>();
			int elementSize = math.min(num, num2);
			UnsafeUtility.MemCpyStride((void*)(unsafePtr + dataChunk.startIndex * num2), num2, source, num, elementSize, num3);
			UnsafeUtility.ReleaseGCObject(gcHandle);
			return dataChunk;
		}

		// Token: 0x060003F5 RID: 1013 RVA: 0x000210DC File Offset: 0x0001F2DC
		public DataChunk Add(T data)
		{
			DataChunk dataChunk = this.AddRange(1);
			this.nativeArray[dataChunk.startIndex] = data;
			return dataChunk;
		}

		// Token: 0x060003F6 RID: 1014 RVA: 0x00021104 File Offset: 0x0001F304
		public T[] ToArray()
		{
			return this.nativeArray.ToArray();
		}

		// Token: 0x060003F7 RID: 1015 RVA: 0x00021111 File Offset: 0x0001F311
		public void CopyTo(T[] array)
		{
			NativeArray<T>.Copy(this.nativeArray, array);
		}

		// Token: 0x060003F8 RID: 1016 RVA: 0x0002111F File Offset: 0x0001F31F
		public void CopyTo<U>(U[] array) where U : struct
		{
			NativeArray<U>.Copy(this.nativeArray.Reinterpret<U>(), array);
		}

		// Token: 0x060003F9 RID: 1017 RVA: 0x00021132 File Offset: 0x0001F332
		public void CopyFrom(NativeArray<T> array)
		{
			NativeArray<T>.Copy(array, this.nativeArray);
		}

		// Token: 0x060003FA RID: 1018 RVA: 0x00021140 File Offset: 0x0001F340
		public void CopyFrom<U>(NativeArray<U> array) where U : struct
		{
			NativeArray<T>.Copy(array.Reinterpret<T>(), this.nativeArray);
		}

		// Token: 0x060003FB RID: 1019 RVA: 0x00021154 File Offset: 0x0001F354
		public unsafe void CopyTypeChange<U>(U[] array) where U : struct
		{
			int num = UnsafeUtility.SizeOf<T>();
			int num2 = UnsafeUtility.SizeOf<U>();
			int num3 = this.Length * num / num2;
			byte* unsafePtr = (byte*)this.nativeArray.GetUnsafePtr<T>();
			ulong gcHandle;
			UnsafeUtility.MemCpy(UnsafeUtility.PinGCArrayAndGetDataAddress(array, out gcHandle), (void*)unsafePtr, (long)(num3 * num2));
			UnsafeUtility.ReleaseGCObject(gcHandle);
		}

		// Token: 0x060003FC RID: 1020 RVA: 0x000211A0 File Offset: 0x0001F3A0
		public unsafe void CopyTypeChangeStride<U>(U[] array) where U : struct
		{
			int num = UnsafeUtility.SizeOf<T>();
			int destinationStride = UnsafeUtility.SizeOf<U>();
			int length = this.Length;
			byte* unsafePtr = (byte*)this.nativeArray.GetUnsafePtr<T>();
			ulong gcHandle;
			void* destination = UnsafeUtility.PinGCArrayAndGetDataAddress(array, out gcHandle);
			int elementSize = num;
			UnsafeUtility.MemCpyStride(destination, destinationStride, (void*)unsafePtr, num, elementSize, length);
			UnsafeUtility.ReleaseGCObject(gcHandle);
		}

		// Token: 0x060003FD RID: 1021 RVA: 0x000211EC File Offset: 0x0001F3EC
		public void AddEmpty(int dataLength)
		{
			DataChunk chunk = this.AddRange(dataLength);
			this.Remove(chunk);
		}

		// Token: 0x060003FE RID: 1022 RVA: 0x00021208 File Offset: 0x0001F408
		public void Remove(DataChunk chunk)
		{
			if (!chunk.IsValid)
			{
				return;
			}
			this.AddEmptyChunk(chunk);
			if (chunk.startIndex + chunk.dataLength == this.useCount)
			{
				this.useCount = 0;
				foreach (DataChunk dataChunk in this.emptyChunks)
				{
					this.useCount = math.max(this.useCount, dataChunk.startIndex);
				}
			}
		}

		// Token: 0x060003FF RID: 1023 RVA: 0x00021298 File Offset: 0x0001F498
		public void RemoveAndFill(DataChunk chunk, T clearData = default(T))
		{
			this.Remove(chunk);
			this.Fill(chunk, clearData);
		}

		// Token: 0x06000400 RID: 1024 RVA: 0x000212A9 File Offset: 0x0001F4A9
		public void Fill(T fillData = default(T))
		{
			if (!this.IsValid)
			{
				return;
			}
			this.FillInternal(0, this.nativeArray.Length, fillData);
		}

		// Token: 0x06000401 RID: 1025 RVA: 0x000212C7 File Offset: 0x0001F4C7
		public void Fill(DataChunk chunk, T fillData = default(T))
		{
			if (!this.IsValid || !chunk.IsValid)
			{
				return;
			}
			this.FillInternal(chunk.startIndex, chunk.dataLength, fillData);
		}

		// Token: 0x06000402 RID: 1026 RVA: 0x000212F0 File Offset: 0x0001F4F0
		private unsafe void FillInternal(int start, int size, T fillData = default(T))
		{
			void* unsafePtr = this.nativeArray.GetUnsafePtr<T>();
			int num = start;
			int i = 0;
			while (i < size)
			{
				UnsafeUtility.WriteArrayElement<T>(unsafePtr, num, fillData);
				i++;
				num++;
			}
		}

		// Token: 0x06000403 RID: 1027 RVA: 0x00021324 File Offset: 0x0001F524
		public void Clear()
		{
			this.emptyChunks.Clear();
			this.useCount = 0;
			if (this.IsValid && this.Length > 0)
			{
				DataChunk item = new DataChunk(0, this.Length);
				this.emptyChunks.Add(item);
			}
		}

		// Token: 0x17000067 RID: 103
		public T this[int index]
		{
			get
			{
				return this.nativeArray[index];
			}
			set
			{
				this.nativeArray[index] = value;
			}
		}

		// Token: 0x06000406 RID: 1030 RVA: 0x0002138B File Offset: 0x0001F58B
		public NativeArray<T> GetNativeArray()
		{
			return this.nativeArray;
		}

		// Token: 0x06000407 RID: 1031 RVA: 0x00021393 File Offset: 0x0001F593
		public NativeArray<U> GetNativeArray<U>() where U : struct
		{
			return this.nativeArray.Reinterpret<U>();
		}

		// Token: 0x06000408 RID: 1032 RVA: 0x000213A0 File Offset: 0x0001F5A0
		private DataChunk GetEmptyChunk(int dataLength)
		{
			for (int i = 0; i < this.emptyChunks.Count; i++)
			{
				DataChunk dataChunk = this.emptyChunks[i];
				if (dataLength == dataChunk.dataLength)
				{
					this.emptyChunks.RemoveAtSwapBack(i);
					return dataChunk;
				}
				if (dataLength < dataChunk.dataLength)
				{
					DataChunk result = default(DataChunk);
					result.startIndex = dataChunk.startIndex;
					result.dataLength = dataLength;
					dataChunk.startIndex += dataLength;
					dataChunk.dataLength -= dataLength;
					this.emptyChunks[i] = dataChunk;
					return result;
				}
			}
			return default(DataChunk);
		}

		// Token: 0x06000409 RID: 1033 RVA: 0x00021440 File Offset: 0x0001F640
		private void AddEmptyChunk(DataChunk chunk)
		{
			for (int i = 0; i < this.emptyChunks.Count; i++)
			{
				DataChunk dataChunk = this.emptyChunks[i];
				if (dataChunk.startIndex + dataChunk.dataLength == chunk.startIndex)
				{
					dataChunk.dataLength += chunk.dataLength;
					chunk = dataChunk;
					this.emptyChunks.RemoveAtSwapBack(i);
					break;
				}
			}
			for (int j = 0; j < this.emptyChunks.Count; j++)
			{
				DataChunk dataChunk2 = this.emptyChunks[j];
				if (dataChunk2.startIndex == chunk.startIndex + chunk.dataLength)
				{
					chunk.dataLength += dataChunk2.dataLength;
					this.emptyChunks.RemoveAtSwapBack(j);
					break;
				}
			}
			this.emptyChunks.Add(chunk);
		}

		// Token: 0x0600040A RID: 1034 RVA: 0x0002150C File Offset: 0x0001F70C
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendLine(string.Format("ExNativeArray Length:{0} Count:{1} IsValid:{2}", this.Length, this.Count, this.IsValid));
			stringBuilder.AppendLine("---- Datas[100] ----");
			if (this.IsValid)
			{
				int num = 0;
				while (num < this.Length && num < 100)
				{
					StringBuilder stringBuilder2 = stringBuilder;
					T t = this.nativeArray[num];
					stringBuilder2.AppendLine(t.ToString());
					num++;
				}
			}
			stringBuilder.AppendLine("---- Empty Chunks ----");
			foreach (DataChunk dataChunk in this.emptyChunks)
			{
				stringBuilder.AppendLine(dataChunk.ToString());
			}
			stringBuilder.AppendLine();
			return stringBuilder.ToString();
		}

		// Token: 0x0400063C RID: 1596
		private NativeArray<T> nativeArray;

		// Token: 0x0400063D RID: 1597
		private List<DataChunk> emptyChunks = new List<DataChunk>();

		// Token: 0x0400063E RID: 1598
		private int useCount;
	}
}

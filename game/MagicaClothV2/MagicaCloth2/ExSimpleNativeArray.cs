using System;
using System.Runtime.CompilerServices;
using System.Text;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Mathematics;

namespace MagicaCloth2
{
	// Token: 0x020000EA RID: 234
	public class ExSimpleNativeArray<[IsUnmanaged] T> : IDisposable where T : struct, ValueType
	{
		// Token: 0x06000410 RID: 1040 RVA: 0x000216C3 File Offset: 0x0001F8C3
		public ExSimpleNativeArray()
		{
			this.count = 0;
			this.length = 0;
		}

		// Token: 0x06000411 RID: 1041 RVA: 0x000216D9 File Offset: 0x0001F8D9
		public ExSimpleNativeArray(int dataLength, bool areaOnly = false) : this()
		{
			this.nativeArray = new NativeArray<T>(dataLength, Allocator.Persistent, NativeArrayOptions.ClearMemory);
			this.length = dataLength;
			if (!areaOnly)
			{
				this.count = this.length;
			}
		}

		// Token: 0x06000412 RID: 1042 RVA: 0x00021705 File Offset: 0x0001F905
		public ExSimpleNativeArray(T[] dataArray) : this()
		{
			this.nativeArray = new NativeArray<T>(dataArray, Allocator.Persistent);
			this.length = dataArray.Length;
			this.count = this.length;
		}

		// Token: 0x06000413 RID: 1043 RVA: 0x0002172F File Offset: 0x0001F92F
		public ExSimpleNativeArray(NativeArray<T> array) : this()
		{
			this.nativeArray = new NativeArray<T>(array, Allocator.Persistent);
			this.length = array.Length;
			this.count = this.length;
		}

		// Token: 0x06000414 RID: 1044 RVA: 0x0002175D File Offset: 0x0001F95D
		public ExSimpleNativeArray(NativeList<T> array) : this()
		{
			this.nativeArray = new NativeArray<T>(array.AsArray(), Allocator.Persistent);
			this.length = array.Length;
			this.count = this.length;
		}

		// Token: 0x06000415 RID: 1045 RVA: 0x00021791 File Offset: 0x0001F991
		public void Dispose()
		{
			if (this.nativeArray.IsCreated)
			{
				this.nativeArray.Dispose();
			}
			this.count = 0;
			this.length = 0;
		}

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x06000416 RID: 1046 RVA: 0x000217B9 File Offset: 0x0001F9B9
		public bool IsValid
		{
			get
			{
				return this.nativeArray.IsCreated;
			}
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x06000417 RID: 1047 RVA: 0x000217C6 File Offset: 0x0001F9C6
		public int Count
		{
			get
			{
				return this.count;
			}
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x06000418 RID: 1048 RVA: 0x000217CE File Offset: 0x0001F9CE
		public int Length
		{
			get
			{
				return this.length;
			}
		}

		// Token: 0x06000419 RID: 1049 RVA: 0x000217D6 File Offset: 0x0001F9D6
		public void SetCount(int newCount)
		{
			this.count = newCount;
		}

		// Token: 0x0600041A RID: 1050 RVA: 0x000217DF File Offset: 0x0001F9DF
		public void AddCapacity(int capacity)
		{
			this.Expand(capacity, true);
		}

		// Token: 0x0600041B RID: 1051 RVA: 0x000217E9 File Offset: 0x0001F9E9
		public void AddRange(int dataLength)
		{
			this.Expand(dataLength, false);
			this.count += dataLength;
		}

		// Token: 0x0600041C RID: 1052 RVA: 0x00021804 File Offset: 0x0001FA04
		public void AddRange(T[] dataArray)
		{
			if (this.length == 0)
			{
				this.nativeArray = new NativeArray<T>(dataArray, Allocator.Persistent);
				this.length = dataArray.Length;
				this.count = this.length;
				return;
			}
			int num = dataArray.Length;
			this.Expand(num, false);
			NativeArray<T>.Copy(dataArray, 0, this.nativeArray, this.count, num);
			this.count += num;
		}

		// Token: 0x0600041D RID: 1053 RVA: 0x0002186C File Offset: 0x0001FA6C
		public void AddRange(T[] dataArray, int cnt)
		{
			if (this.length == 0)
			{
				this.nativeArray = new NativeArray<T>(cnt, Allocator.Persistent, NativeArrayOptions.ClearMemory);
				NativeArray<T>.Copy(dataArray, 0, this.nativeArray, 0, cnt);
				this.length = cnt;
				this.count = this.length;
				return;
			}
			this.Expand(cnt, false);
			NativeArray<T>.Copy(dataArray, 0, this.nativeArray, this.count, cnt);
			this.count += cnt;
		}

		// Token: 0x0600041E RID: 1054 RVA: 0x000218DE File Offset: 0x0001FADE
		public void AddRange(int dataLength, T fillData = default(T))
		{
			this.Expand(dataLength, false);
			this.Fill(this.count, dataLength, fillData);
			this.count += dataLength;
		}

		// Token: 0x0600041F RID: 1055 RVA: 0x00021904 File Offset: 0x0001FB04
		public void AddRange(NativeArray<T> narray)
		{
			if (this.length == 0)
			{
				this.nativeArray = new NativeArray<T>(narray, Allocator.Persistent);
				this.length = narray.Length;
				this.count = this.length;
				return;
			}
			int num = narray.Length;
			this.Expand(num, false);
			NativeArray<T>.Copy(narray, 0, this.nativeArray, this.count, num);
			this.count += num;
		}

		// Token: 0x06000420 RID: 1056 RVA: 0x00021972 File Offset: 0x0001FB72
		public void AddRange(NativeList<T> nlist)
		{
			this.AddRange(nlist.AsArray());
		}

		// Token: 0x06000421 RID: 1057 RVA: 0x00021981 File Offset: 0x0001FB81
		public void AddRange(ExSimpleNativeArray<T> exarray)
		{
			this.AddRange(exarray.GetNativeArray());
		}

		// Token: 0x06000422 RID: 1058 RVA: 0x00021990 File Offset: 0x0001FB90
		public unsafe void AddRange<U>(U[] array) where U : struct
		{
			int num = array.Length;
			this.Expand(num, false);
			int num2 = UnsafeUtility.SizeOf<T>();
			ulong gcHandle;
			void* source = UnsafeUtility.PinGCArrayAndGetDataAddress(array, out gcHandle);
			byte* unsafePtr = (byte*)this.nativeArray.GetUnsafePtr<T>();
			UnsafeUtility.MemCpy((void*)(unsafePtr + this.count * num2), source, (long)(num * num2));
			UnsafeUtility.ReleaseGCObject(gcHandle);
			this.count += num;
		}

		// Token: 0x06000423 RID: 1059 RVA: 0x000219F0 File Offset: 0x0001FBF0
		public unsafe void AddRangeTypeChange<U>(U[] array) where U : struct
		{
			int num = UnsafeUtility.SizeOf<U>();
			int num2 = UnsafeUtility.SizeOf<T>();
			int num3 = array.Length * num / num2;
			this.Expand(num3, false);
			ulong gcHandle;
			void* source = UnsafeUtility.PinGCArrayAndGetDataAddress(array, out gcHandle);
			byte* unsafePtr = (byte*)this.nativeArray.GetUnsafePtr<T>();
			UnsafeUtility.MemCpy((void*)(unsafePtr + this.count * num2), source, (long)(num3 * num2));
			UnsafeUtility.ReleaseGCObject(gcHandle);
			this.count += num3;
		}

		// Token: 0x06000424 RID: 1060 RVA: 0x00021A5C File Offset: 0x0001FC5C
		public unsafe void AddRangeTypeChange<U>(NativeArray<U> array) where U : struct
		{
			int num = UnsafeUtility.SizeOf<U>();
			int num2 = UnsafeUtility.SizeOf<T>();
			int num3 = array.Length * num / num2;
			this.Expand(num3, false);
			byte* unsafePtr = (byte*)array.GetUnsafePtr<U>();
			byte* unsafePtr2 = (byte*)this.nativeArray.GetUnsafePtr<T>();
			UnsafeUtility.MemCpy((void*)(unsafePtr2 + this.count * num2), (void*)unsafePtr, (long)(num3 * num2));
			this.count += num3;
		}

		// Token: 0x06000425 RID: 1061 RVA: 0x00021AC0 File Offset: 0x0001FCC0
		public unsafe void AddRangeStride<U>(U[] array) where U : struct
		{
			int num = array.Length;
			this.Expand(num, false);
			int num2 = UnsafeUtility.SizeOf<U>();
			int num3 = UnsafeUtility.SizeOf<T>();
			ulong gcHandle;
			void* source = UnsafeUtility.PinGCArrayAndGetDataAddress(array, out gcHandle);
			byte* unsafePtr = (byte*)this.nativeArray.GetUnsafePtr<T>();
			int elementSize = math.min(num2, num3);
			UnsafeUtility.MemCpyStride((void*)(unsafePtr + this.count * num3), num3, source, num2, elementSize, num);
			UnsafeUtility.ReleaseGCObject(gcHandle);
			this.count += num;
		}

		// Token: 0x06000426 RID: 1062 RVA: 0x00021B30 File Offset: 0x0001FD30
		public void Add(T data)
		{
			if (this.Length == 0)
			{
				this.Expand(16, false);
			}
			else if (this.count == this.Length)
			{
				this.Expand(this.Length, false);
			}
			this.nativeArray[this.count] = data;
			this.count++;
		}

		// Token: 0x06000427 RID: 1063 RVA: 0x00021B8B File Offset: 0x0001FD8B
		public T[] ToArray()
		{
			return this.nativeArray.ToArray();
		}

		// Token: 0x06000428 RID: 1064 RVA: 0x00021B98 File Offset: 0x0001FD98
		public void CopyTo(T[] array)
		{
			NativeArray<T>.Copy(this.nativeArray, array);
		}

		// Token: 0x06000429 RID: 1065 RVA: 0x00021BA6 File Offset: 0x0001FDA6
		public void CopyTo<U>(U[] array) where U : struct
		{
			NativeArray<U>.Copy(this.nativeArray.Reinterpret<U>(), array);
		}

		// Token: 0x0600042A RID: 1066 RVA: 0x00021BBC File Offset: 0x0001FDBC
		public unsafe void CopyToWithTypeChange<U>(U[] array) where U : struct
		{
			int num = UnsafeUtility.SizeOf<T>();
			int num2 = UnsafeUtility.SizeOf<U>();
			int num3 = this.Length * num / num2;
			byte* unsafePtr = (byte*)this.nativeArray.GetUnsafePtr<T>();
			ulong gcHandle;
			UnsafeUtility.MemCpy(UnsafeUtility.PinGCArrayAndGetDataAddress(array, out gcHandle), (void*)unsafePtr, (long)(num3 * num2));
			UnsafeUtility.ReleaseGCObject(gcHandle);
		}

		// Token: 0x0600042B RID: 1067 RVA: 0x00021C08 File Offset: 0x0001FE08
		public unsafe void CopyToWithTypeChangeStride<U>(U[] array) where U : struct
		{
			int num = UnsafeUtility.SizeOf<T>();
			int destinationStride = UnsafeUtility.SizeOf<U>();
			int num2 = this.Length;
			byte* unsafePtr = (byte*)this.nativeArray.GetUnsafePtr<T>();
			ulong gcHandle;
			void* destination = UnsafeUtility.PinGCArrayAndGetDataAddress(array, out gcHandle);
			int elementSize = num;
			UnsafeUtility.MemCpyStride(destination, destinationStride, (void*)unsafePtr, num, elementSize, num2);
			UnsafeUtility.ReleaseGCObject(gcHandle);
		}

		// Token: 0x0600042C RID: 1068 RVA: 0x00021C51 File Offset: 0x0001FE51
		public void CopyFrom(NativeArray<T> array)
		{
			NativeArray<T>.Copy(array, this.nativeArray);
		}

		// Token: 0x0600042D RID: 1069 RVA: 0x00021C5F File Offset: 0x0001FE5F
		public void CopyFrom<U>(NativeArray<U> array) where U : struct
		{
			NativeArray<T>.Copy(array.Reinterpret<T>(), this.nativeArray);
		}

		// Token: 0x0600042E RID: 1070 RVA: 0x00021C74 File Offset: 0x0001FE74
		public unsafe void CopyFromWithTypeChangeStride<U>(NativeArray<U> array) where U : struct
		{
			int sourceStride = UnsafeUtility.SizeOf<U>();
			int num = UnsafeUtility.SizeOf<T>();
			int num2 = array.Length;
			byte* unsafePtr = (byte*)array.GetUnsafePtr<U>();
			byte* unsafePtr2 = (byte*)this.nativeArray.GetUnsafePtr<T>();
			int elementSize = num;
			UnsafeUtility.MemCpyStride((void*)unsafePtr2, num, (void*)unsafePtr, sourceStride, elementSize, num2);
		}

		// Token: 0x0600042F RID: 1071 RVA: 0x00021CB9 File Offset: 0x0001FEB9
		public void Fill(int startIndex, int dataLength, T fillData = default(T))
		{
			this.FillInternal(startIndex, dataLength, fillData);
		}

		// Token: 0x06000430 RID: 1072 RVA: 0x00021CC4 File Offset: 0x0001FEC4
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

		// Token: 0x1700006B RID: 107
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

		// Token: 0x06000433 RID: 1075 RVA: 0x00021D14 File Offset: 0x0001FF14
		public NativeArray<T> GetNativeArray()
		{
			return this.nativeArray;
		}

		// Token: 0x06000434 RID: 1076 RVA: 0x00021D1C File Offset: 0x0001FF1C
		public NativeArray<U> GetNativeArray<U>() where U : struct
		{
			return this.nativeArray.Reinterpret<U>();
		}

		// Token: 0x06000435 RID: 1077 RVA: 0x00021D2C File Offset: 0x0001FF2C
		private void Expand(int dataLength, bool force = false)
		{
			int num = force ? (this.length + dataLength) : (this.count + dataLength);
			if (this.length == 0)
			{
				this.nativeArray = new NativeArray<T>(dataLength, Allocator.Persistent, NativeArrayOptions.ClearMemory);
				this.length = dataLength;
				return;
			}
			if (num > this.Length)
			{
				NativeArray<T> dst = new NativeArray<T>(num, Allocator.Persistent, NativeArrayOptions.ClearMemory);
				NativeArray<T>.Copy(this.nativeArray, dst, this.count);
				this.nativeArray.Dispose();
				this.nativeArray = dst;
				this.length = num;
			}
		}

		// Token: 0x06000436 RID: 1078 RVA: 0x00021DAC File Offset: 0x0001FFAC
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
			return stringBuilder.ToString();
		}

		// Token: 0x04000641 RID: 1601
		private NativeArray<T> nativeArray;

		// Token: 0x04000642 RID: 1602
		private int count;

		// Token: 0x04000643 RID: 1603
		private int length;
	}
}

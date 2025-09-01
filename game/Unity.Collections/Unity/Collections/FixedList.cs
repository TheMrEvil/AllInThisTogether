using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Mathematics;
using UnityEngine;

namespace Unity.Collections
{
	// Token: 0x02000052 RID: 82
	[BurstCompatible(GenericTypeArguments = new Type[]
	{
		typeof(int),
		typeof(FixedBytes30)
	})]
	[Serializable]
	internal struct FixedList<[IsUnmanaged] T, [IsUnmanaged] U> : INativeList<T>, IIndexable<T> where T : struct, ValueType where U : struct, ValueType
	{
		// Token: 0x17000038 RID: 56
		// (get) Token: 0x060001A4 RID: 420 RVA: 0x00004F6E File Offset: 0x0000316E
		// (set) Token: 0x060001A5 RID: 421 RVA: 0x00004F76 File Offset: 0x00003176
		[CreateProperty]
		public int Length
		{
			get
			{
				return (int)this.length;
			}
			set
			{
				this.length = (ushort)value;
			}
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x060001A6 RID: 422 RVA: 0x00004F80 File Offset: 0x00003180
		[CreateProperty]
		private IEnumerable<T> Elements
		{
			get
			{
				return this.ToArray();
			}
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x060001A7 RID: 423 RVA: 0x00004F88 File Offset: 0x00003188
		public bool IsEmpty
		{
			get
			{
				return this.Length == 0;
			}
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x060001A8 RID: 424 RVA: 0x00004F93 File Offset: 0x00003193
		internal int LengthInBytes
		{
			get
			{
				return this.Length * UnsafeUtility.SizeOf<T>();
			}
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x060001A9 RID: 425 RVA: 0x00004FA4 File Offset: 0x000031A4
		internal unsafe byte* Buffer
		{
			get
			{
				fixed (U* ptr = &this.buffer)
				{
					return (byte*)ptr + FixedList.PaddingBytes<T>();
				}
			}
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x060001AA RID: 426 RVA: 0x00004FC0 File Offset: 0x000031C0
		// (set) Token: 0x060001AB RID: 427 RVA: 0x00002C2B File Offset: 0x00000E2B
		public int Capacity
		{
			get
			{
				return FixedList.Capacity<U, T>();
			}
			set
			{
			}
		}

		// Token: 0x1700003E RID: 62
		public unsafe T this[int index]
		{
			get
			{
				return UnsafeUtility.ReadArrayElement<T>((void*)this.Buffer, CollectionHelper.AssumePositive(index));
			}
			set
			{
				UnsafeUtility.WriteArrayElement<T>((void*)this.Buffer, CollectionHelper.AssumePositive(index), value);
			}
		}

		// Token: 0x060001AE RID: 430 RVA: 0x00004FEE File Offset: 0x000031EE
		public unsafe ref T ElementAt(int index)
		{
			return UnsafeUtility.ArrayElementAsRef<T>((void*)this.Buffer, index);
		}

		// Token: 0x060001AF RID: 431 RVA: 0x00004FFC File Offset: 0x000031FC
		public unsafe override int GetHashCode()
		{
			return (int)CollectionHelper.Hash((void*)this.Buffer, this.LengthInBytes);
		}

		// Token: 0x060001B0 RID: 432 RVA: 0x00005010 File Offset: 0x00003210
		public void Add(in T item)
		{
			int num = this.Length;
			this.Length = num + 1;
			this[num] = item;
		}

		// Token: 0x060001B1 RID: 433 RVA: 0x0000503C File Offset: 0x0000323C
		public unsafe void AddRange(void* ptr, int length)
		{
			for (int i = 0; i < length; i++)
			{
				int num = this.Length;
				this.Length = num + 1;
				this[num] = *(T*)((byte*)ptr + (IntPtr)i * (IntPtr)sizeof(T));
			}
		}

		// Token: 0x060001B2 RID: 434 RVA: 0x0000507E File Offset: 0x0000327E
		public void AddNoResize(in T item)
		{
			this.Add(item);
		}

		// Token: 0x060001B3 RID: 435 RVA: 0x00005087 File Offset: 0x00003287
		public unsafe void AddRangeNoResize(void* ptr, int length)
		{
			this.AddRange(ptr, length);
		}

		// Token: 0x060001B4 RID: 436 RVA: 0x00005091 File Offset: 0x00003291
		public void Clear()
		{
			this.Length = 0;
		}

		// Token: 0x060001B5 RID: 437 RVA: 0x0000509C File Offset: 0x0000329C
		public unsafe void InsertRangeWithBeginEnd(int begin, int end)
		{
			int num = end - begin;
			if (num < 1)
			{
				return;
			}
			int num2 = (int)this.length - begin;
			this.Length += num;
			if (num2 < 1)
			{
				return;
			}
			int num3 = num2 * UnsafeUtility.SizeOf<T>();
			byte* ptr = this.Buffer;
			byte* destination = ptr + end * UnsafeUtility.SizeOf<T>();
			byte* source = ptr + begin * UnsafeUtility.SizeOf<T>();
			UnsafeUtility.MemMove((void*)destination, (void*)source, (long)num3);
		}

		// Token: 0x060001B6 RID: 438 RVA: 0x000050FA File Offset: 0x000032FA
		public void Insert(int index, in T item)
		{
			this.InsertRangeWithBeginEnd(index, index + 1);
			this[index] = item;
		}

		// Token: 0x060001B7 RID: 439 RVA: 0x00005113 File Offset: 0x00003313
		public void RemoveAtSwapBack(int index)
		{
			this.RemoveRangeSwapBack(index, 1);
		}

		// Token: 0x060001B8 RID: 440 RVA: 0x00005120 File Offset: 0x00003320
		public unsafe void RemoveRangeSwapBack(int index, int count)
		{
			if (count > 0)
			{
				int num = math.max(this.Length - count, index + count);
				int num2 = UnsafeUtility.SizeOf<T>();
				void* destination = (void*)(this.Buffer + index * num2);
				void* source = (void*)(this.Buffer + num * num2);
				UnsafeUtility.MemCpy(destination, source, (long)((this.Length - num) * num2));
				this.Length -= count;
			}
		}

		// Token: 0x060001B9 RID: 441 RVA: 0x0000517E File Offset: 0x0000337E
		[Obsolete("RemoveRangeSwapBackWithBeginEnd(begin, end) is deprecated, use RemoveRangeSwapBack(index, count) instead. (RemovedAfter 2021-06-02)", false)]
		public void RemoveRangeSwapBackWithBeginEnd(int begin, int end)
		{
			this.RemoveRangeSwapBack(begin, end - begin);
		}

		// Token: 0x060001BA RID: 442 RVA: 0x0000518A File Offset: 0x0000338A
		public void RemoveAt(int index)
		{
			this.RemoveRange(index, 1);
		}

		// Token: 0x060001BB RID: 443 RVA: 0x00005194 File Offset: 0x00003394
		public unsafe void RemoveRange(int index, int count)
		{
			if (count > 0)
			{
				int num = math.min(index + count, this.Length);
				int num2 = UnsafeUtility.SizeOf<T>();
				void* destination = (void*)(this.Buffer + index * num2);
				void* source = (void*)(this.Buffer + num * num2);
				UnsafeUtility.MemCpy(destination, source, (long)((this.Length - num) * num2));
				this.Length -= count;
			}
		}

		// Token: 0x060001BC RID: 444 RVA: 0x000051F0 File Offset: 0x000033F0
		[Obsolete("RemoveRangeWithBeginEnd(begin, end) is deprecated, use RemoveRange(index, count) instead. (RemovedAfter 2021-06-02)", false)]
		public void RemoveRangeWithBeginEnd(int begin, int end)
		{
			this.RemoveRange(begin, end - begin);
		}

		// Token: 0x060001BD RID: 445 RVA: 0x000051FC File Offset: 0x000033FC
		[NotBurstCompatible]
		public unsafe T[] ToArray()
		{
			T[] array = new T[this.Length];
			byte* source = this.Buffer;
			fixed (T[] array2 = array)
			{
				T* destination;
				if (array == null || array2.Length == 0)
				{
					destination = null;
				}
				else
				{
					destination = &array2[0];
				}
				UnsafeUtility.MemCpy((void*)destination, (void*)source, (long)this.LengthInBytes);
			}
			return array;
		}

		// Token: 0x060001BE RID: 446 RVA: 0x00005243 File Offset: 0x00003443
		public unsafe NativeArray<T> ToNativeArray(AllocatorManager.AllocatorHandle allocator)
		{
			NativeArray<T> nativeArray = CollectionHelper.CreateNativeArray<T>(this.Length, allocator, NativeArrayOptions.UninitializedMemory);
			UnsafeUtility.MemCpy(nativeArray.GetUnsafePtr<T>(), (void*)this.Buffer, (long)this.LengthInBytes);
			return nativeArray;
		}

		// Token: 0x040000AF RID: 175
		[SerializeField]
		internal ushort length;

		// Token: 0x040000B0 RID: 176
		[SerializeField]
		internal U buffer;
	}
}

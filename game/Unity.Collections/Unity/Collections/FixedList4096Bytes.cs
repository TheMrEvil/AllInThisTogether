using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Mathematics;
using UnityEngine;

namespace Unity.Collections
{
	// Token: 0x02000069 RID: 105
	[DebuggerTypeProxy(typeof(FixedList4096BytesDebugView<>))]
	[BurstCompatible(GenericTypeArguments = new Type[]
	{
		typeof(int)
	})]
	[Serializable]
	public struct FixedList4096Bytes<[IsUnmanaged] T> : INativeList<T>, IIndexable<T>, IEnumerable<!0>, IEnumerable, IEquatable<FixedList32Bytes<T>>, IComparable<FixedList32Bytes<T>>, IEquatable<FixedList64Bytes<T>>, IComparable<FixedList64Bytes<T>>, IEquatable<FixedList128Bytes<T>>, IComparable<FixedList128Bytes<T>>, IEquatable<FixedList512Bytes<T>>, IComparable<FixedList512Bytes<T>>, IEquatable<FixedList4096Bytes<T>>, IComparable<FixedList4096Bytes<T>> where T : struct, ValueType
	{
		// Token: 0x17000067 RID: 103
		// (get) Token: 0x060002EF RID: 751 RVA: 0x000081CB File Offset: 0x000063CB
		// (set) Token: 0x060002F0 RID: 752 RVA: 0x000081D3 File Offset: 0x000063D3
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

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x060002F1 RID: 753 RVA: 0x000081DD File Offset: 0x000063DD
		[CreateProperty]
		private IEnumerable<T> Elements
		{
			get
			{
				return this.ToArray();
			}
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x060002F2 RID: 754 RVA: 0x000081E5 File Offset: 0x000063E5
		public bool IsEmpty
		{
			get
			{
				return this.Length == 0;
			}
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x060002F3 RID: 755 RVA: 0x000081F0 File Offset: 0x000063F0
		internal int LengthInBytes
		{
			get
			{
				return this.Length * UnsafeUtility.SizeOf<T>();
			}
		}

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x060002F4 RID: 756 RVA: 0x00008200 File Offset: 0x00006400
		internal unsafe byte* Buffer
		{
			get
			{
				fixed (byte* ptr = &this.buffer.offset0000.byte0000)
				{
					return ptr + FixedList.PaddingBytes<T>();
				}
			}
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x060002F5 RID: 757 RVA: 0x00008226 File Offset: 0x00006426
		// (set) Token: 0x060002F6 RID: 758 RVA: 0x00002C2B File Offset: 0x00000E2B
		public int Capacity
		{
			get
			{
				return FixedList.Capacity<FixedBytes4094, T>();
			}
			set
			{
			}
		}

		// Token: 0x1700006D RID: 109
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

		// Token: 0x060002F9 RID: 761 RVA: 0x00008254 File Offset: 0x00006454
		public unsafe ref T ElementAt(int index)
		{
			return UnsafeUtility.ArrayElementAsRef<T>((void*)this.Buffer, index);
		}

		// Token: 0x060002FA RID: 762 RVA: 0x00008262 File Offset: 0x00006462
		public unsafe override int GetHashCode()
		{
			return (int)CollectionHelper.Hash((void*)this.Buffer, this.LengthInBytes);
		}

		// Token: 0x060002FB RID: 763 RVA: 0x00008278 File Offset: 0x00006478
		public void Add(in T item)
		{
			int num = this.Length;
			this.Length = num + 1;
			this[num] = item;
		}

		// Token: 0x060002FC RID: 764 RVA: 0x000082A4 File Offset: 0x000064A4
		public unsafe void AddRange(void* ptr, int length)
		{
			for (int i = 0; i < length; i++)
			{
				int num = this.Length;
				this.Length = num + 1;
				this[num] = *(T*)((byte*)ptr + (IntPtr)i * (IntPtr)sizeof(T));
			}
		}

		// Token: 0x060002FD RID: 765 RVA: 0x000082E6 File Offset: 0x000064E6
		public void AddNoResize(in T item)
		{
			this.Add(item);
		}

		// Token: 0x060002FE RID: 766 RVA: 0x000082EF File Offset: 0x000064EF
		public unsafe void AddRangeNoResize(void* ptr, int length)
		{
			this.AddRange(ptr, length);
		}

		// Token: 0x060002FF RID: 767 RVA: 0x000082F9 File Offset: 0x000064F9
		public void Clear()
		{
			this.Length = 0;
		}

		// Token: 0x06000300 RID: 768 RVA: 0x00008304 File Offset: 0x00006504
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

		// Token: 0x06000301 RID: 769 RVA: 0x00008362 File Offset: 0x00006562
		public void Insert(int index, in T item)
		{
			this.InsertRangeWithBeginEnd(index, index + 1);
			this[index] = item;
		}

		// Token: 0x06000302 RID: 770 RVA: 0x0000837B File Offset: 0x0000657B
		public void RemoveAtSwapBack(int index)
		{
			this.RemoveRangeSwapBack(index, 1);
		}

		// Token: 0x06000303 RID: 771 RVA: 0x00008388 File Offset: 0x00006588
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

		// Token: 0x06000304 RID: 772 RVA: 0x000083E6 File Offset: 0x000065E6
		[Obsolete("RemoveRangeSwapBackWithBeginEnd(begin, end) is deprecated, use RemoveRangeSwapBack(index, count) instead. (RemovedAfter 2021-06-02)", false)]
		public void RemoveRangeSwapBackWithBeginEnd(int begin, int end)
		{
			this.RemoveRangeSwapBack(begin, end - begin);
		}

		// Token: 0x06000305 RID: 773 RVA: 0x000083F2 File Offset: 0x000065F2
		public void RemoveAt(int index)
		{
			this.RemoveRange(index, 1);
		}

		// Token: 0x06000306 RID: 774 RVA: 0x000083FC File Offset: 0x000065FC
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

		// Token: 0x06000307 RID: 775 RVA: 0x00008458 File Offset: 0x00006658
		[Obsolete("RemoveRangeWithBeginEnd(begin, end) is deprecated, use RemoveRange(index, count) instead. (RemovedAfter 2021-06-02)", false)]
		public void RemoveRangeWithBeginEnd(int begin, int end)
		{
			this.RemoveRange(begin, end - begin);
		}

		// Token: 0x06000308 RID: 776 RVA: 0x00008464 File Offset: 0x00006664
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

		// Token: 0x06000309 RID: 777 RVA: 0x000084AB File Offset: 0x000066AB
		public unsafe NativeArray<T> ToNativeArray(AllocatorManager.AllocatorHandle allocator)
		{
			NativeArray<T> nativeArray = CollectionHelper.CreateNativeArray<T>(this.Length, allocator, NativeArrayOptions.UninitializedMemory);
			UnsafeUtility.MemCpy(nativeArray.GetUnsafePtr<T>(), (void*)this.Buffer, (long)this.LengthInBytes);
			return nativeArray;
		}

		// Token: 0x0600030A RID: 778 RVA: 0x000084D4 File Offset: 0x000066D4
		public unsafe static bool operator ==(in FixedList4096Bytes<T> a, in FixedList32Bytes<T> b)
		{
			if (a.length != b.length)
			{
				return false;
			}
			FixedList4096Bytes<T> fixedList4096Bytes = a;
			void* ptr = (void*)fixedList4096Bytes.Buffer;
			FixedList32Bytes<T> fixedList32Bytes = b;
			void* ptr2 = (void*)fixedList32Bytes.Buffer;
			fixedList4096Bytes = a;
			return UnsafeUtility.MemCmp(ptr, ptr2, (long)fixedList4096Bytes.LengthInBytes) == 0;
		}

		// Token: 0x0600030B RID: 779 RVA: 0x00008524 File Offset: 0x00006724
		public static bool operator !=(in FixedList4096Bytes<T> a, in FixedList32Bytes<T> b)
		{
			return !(a == b);
		}

		// Token: 0x0600030C RID: 780 RVA: 0x00008530 File Offset: 0x00006730
		public unsafe int CompareTo(FixedList32Bytes<T> other)
		{
			fixed (byte* ptr = &this.buffer.offset0000.byte0000)
			{
				byte* ptr2 = ptr;
				byte* ptr3 = &other.buffer.offset0000.byte0000;
				byte* ptr4 = ptr2 + FixedList.PaddingBytes<T>();
				byte* ptr5 = ptr3 + FixedList.PaddingBytes<T>();
				int num = math.min(this.Length, other.Length);
				for (int i = 0; i < num; i++)
				{
					int num2 = UnsafeUtility.MemCmp((void*)(ptr4 + sizeof(T) * i), (void*)(ptr5 + sizeof(T) * i), (long)sizeof(T));
					if (num2 != 0)
					{
						return num2;
					}
				}
				return this.Length.CompareTo(other.Length);
			}
		}

		// Token: 0x0600030D RID: 781 RVA: 0x000085D8 File Offset: 0x000067D8
		public bool Equals(FixedList32Bytes<T> other)
		{
			return this.CompareTo(other) == 0;
		}

		// Token: 0x0600030E RID: 782 RVA: 0x000085E4 File Offset: 0x000067E4
		public FixedList4096Bytes(in FixedList32Bytes<T> other)
		{
			this = default(FixedList4096Bytes<T>);
			this.Initialize(other);
		}

		// Token: 0x0600030F RID: 783 RVA: 0x000085F8 File Offset: 0x000067F8
		internal unsafe int Initialize(in FixedList32Bytes<T> other)
		{
			FixedList32Bytes<T> fixedList32Bytes = other;
			if (fixedList32Bytes.Length > this.Capacity)
			{
				return 1;
			}
			this.length = other.length;
			this.buffer = default(FixedBytes4094);
			void* destination = (void*)this.Buffer;
			fixedList32Bytes = other;
			UnsafeUtility.MemCpy(destination, (void*)fixedList32Bytes.Buffer, (long)this.LengthInBytes);
			return 0;
		}

		// Token: 0x06000310 RID: 784 RVA: 0x00008656 File Offset: 0x00006856
		public static implicit operator FixedList4096Bytes<T>(in FixedList32Bytes<T> other)
		{
			return new FixedList4096Bytes<T>(ref other);
		}

		// Token: 0x06000311 RID: 785 RVA: 0x00008660 File Offset: 0x00006860
		public unsafe static bool operator ==(in FixedList4096Bytes<T> a, in FixedList64Bytes<T> b)
		{
			if (a.length != b.length)
			{
				return false;
			}
			FixedList4096Bytes<T> fixedList4096Bytes = a;
			void* ptr = (void*)fixedList4096Bytes.Buffer;
			FixedList64Bytes<T> fixedList64Bytes = b;
			void* ptr2 = (void*)fixedList64Bytes.Buffer;
			fixedList4096Bytes = a;
			return UnsafeUtility.MemCmp(ptr, ptr2, (long)fixedList4096Bytes.LengthInBytes) == 0;
		}

		// Token: 0x06000312 RID: 786 RVA: 0x000086B0 File Offset: 0x000068B0
		public static bool operator !=(in FixedList4096Bytes<T> a, in FixedList64Bytes<T> b)
		{
			return !(a == b);
		}

		// Token: 0x06000313 RID: 787 RVA: 0x000086BC File Offset: 0x000068BC
		public unsafe int CompareTo(FixedList64Bytes<T> other)
		{
			fixed (byte* ptr = &this.buffer.offset0000.byte0000)
			{
				byte* ptr2 = ptr;
				byte* ptr3 = &other.buffer.offset0000.byte0000;
				byte* ptr4 = ptr2 + FixedList.PaddingBytes<T>();
				byte* ptr5 = ptr3 + FixedList.PaddingBytes<T>();
				int num = math.min(this.Length, other.Length);
				for (int i = 0; i < num; i++)
				{
					int num2 = UnsafeUtility.MemCmp((void*)(ptr4 + sizeof(T) * i), (void*)(ptr5 + sizeof(T) * i), (long)sizeof(T));
					if (num2 != 0)
					{
						return num2;
					}
				}
				return this.Length.CompareTo(other.Length);
			}
		}

		// Token: 0x06000314 RID: 788 RVA: 0x00008764 File Offset: 0x00006964
		public bool Equals(FixedList64Bytes<T> other)
		{
			return this.CompareTo(other) == 0;
		}

		// Token: 0x06000315 RID: 789 RVA: 0x00008770 File Offset: 0x00006970
		public FixedList4096Bytes(in FixedList64Bytes<T> other)
		{
			this = default(FixedList4096Bytes<T>);
			this.Initialize(other);
		}

		// Token: 0x06000316 RID: 790 RVA: 0x00008784 File Offset: 0x00006984
		internal unsafe int Initialize(in FixedList64Bytes<T> other)
		{
			FixedList64Bytes<T> fixedList64Bytes = other;
			if (fixedList64Bytes.Length > this.Capacity)
			{
				return 1;
			}
			this.length = other.length;
			this.buffer = default(FixedBytes4094);
			void* destination = (void*)this.Buffer;
			fixedList64Bytes = other;
			UnsafeUtility.MemCpy(destination, (void*)fixedList64Bytes.Buffer, (long)this.LengthInBytes);
			return 0;
		}

		// Token: 0x06000317 RID: 791 RVA: 0x000087E2 File Offset: 0x000069E2
		public static implicit operator FixedList4096Bytes<T>(in FixedList64Bytes<T> other)
		{
			return new FixedList4096Bytes<T>(ref other);
		}

		// Token: 0x06000318 RID: 792 RVA: 0x000087EC File Offset: 0x000069EC
		public unsafe static bool operator ==(in FixedList4096Bytes<T> a, in FixedList128Bytes<T> b)
		{
			if (a.length != b.length)
			{
				return false;
			}
			FixedList4096Bytes<T> fixedList4096Bytes = a;
			void* ptr = (void*)fixedList4096Bytes.Buffer;
			FixedList128Bytes<T> fixedList128Bytes = b;
			void* ptr2 = (void*)fixedList128Bytes.Buffer;
			fixedList4096Bytes = a;
			return UnsafeUtility.MemCmp(ptr, ptr2, (long)fixedList4096Bytes.LengthInBytes) == 0;
		}

		// Token: 0x06000319 RID: 793 RVA: 0x0000883C File Offset: 0x00006A3C
		public static bool operator !=(in FixedList4096Bytes<T> a, in FixedList128Bytes<T> b)
		{
			return !(a == b);
		}

		// Token: 0x0600031A RID: 794 RVA: 0x00008848 File Offset: 0x00006A48
		public unsafe int CompareTo(FixedList128Bytes<T> other)
		{
			fixed (byte* ptr = &this.buffer.offset0000.byte0000)
			{
				byte* ptr2 = ptr;
				byte* ptr3 = &other.buffer.offset0000.byte0000;
				byte* ptr4 = ptr2 + FixedList.PaddingBytes<T>();
				byte* ptr5 = ptr3 + FixedList.PaddingBytes<T>();
				int num = math.min(this.Length, other.Length);
				for (int i = 0; i < num; i++)
				{
					int num2 = UnsafeUtility.MemCmp((void*)(ptr4 + sizeof(T) * i), (void*)(ptr5 + sizeof(T) * i), (long)sizeof(T));
					if (num2 != 0)
					{
						return num2;
					}
				}
				return this.Length.CompareTo(other.Length);
			}
		}

		// Token: 0x0600031B RID: 795 RVA: 0x000088F0 File Offset: 0x00006AF0
		public bool Equals(FixedList128Bytes<T> other)
		{
			return this.CompareTo(other) == 0;
		}

		// Token: 0x0600031C RID: 796 RVA: 0x000088FC File Offset: 0x00006AFC
		public FixedList4096Bytes(in FixedList128Bytes<T> other)
		{
			this = default(FixedList4096Bytes<T>);
			this.Initialize(other);
		}

		// Token: 0x0600031D RID: 797 RVA: 0x00008910 File Offset: 0x00006B10
		internal unsafe int Initialize(in FixedList128Bytes<T> other)
		{
			FixedList128Bytes<T> fixedList128Bytes = other;
			if (fixedList128Bytes.Length > this.Capacity)
			{
				return 1;
			}
			this.length = other.length;
			this.buffer = default(FixedBytes4094);
			void* destination = (void*)this.Buffer;
			fixedList128Bytes = other;
			UnsafeUtility.MemCpy(destination, (void*)fixedList128Bytes.Buffer, (long)this.LengthInBytes);
			return 0;
		}

		// Token: 0x0600031E RID: 798 RVA: 0x0000896E File Offset: 0x00006B6E
		public static implicit operator FixedList4096Bytes<T>(in FixedList128Bytes<T> other)
		{
			return new FixedList4096Bytes<T>(ref other);
		}

		// Token: 0x0600031F RID: 799 RVA: 0x00008978 File Offset: 0x00006B78
		public unsafe static bool operator ==(in FixedList4096Bytes<T> a, in FixedList512Bytes<T> b)
		{
			if (a.length != b.length)
			{
				return false;
			}
			FixedList4096Bytes<T> fixedList4096Bytes = a;
			void* ptr = (void*)fixedList4096Bytes.Buffer;
			FixedList512Bytes<T> fixedList512Bytes = b;
			void* ptr2 = (void*)fixedList512Bytes.Buffer;
			fixedList4096Bytes = a;
			return UnsafeUtility.MemCmp(ptr, ptr2, (long)fixedList4096Bytes.LengthInBytes) == 0;
		}

		// Token: 0x06000320 RID: 800 RVA: 0x000089C8 File Offset: 0x00006BC8
		public static bool operator !=(in FixedList4096Bytes<T> a, in FixedList512Bytes<T> b)
		{
			return !(a == b);
		}

		// Token: 0x06000321 RID: 801 RVA: 0x000089D4 File Offset: 0x00006BD4
		public unsafe int CompareTo(FixedList512Bytes<T> other)
		{
			fixed (byte* ptr = &this.buffer.offset0000.byte0000)
			{
				byte* ptr2 = ptr;
				byte* ptr3 = &other.buffer.offset0000.byte0000;
				byte* ptr4 = ptr2 + FixedList.PaddingBytes<T>();
				byte* ptr5 = ptr3 + FixedList.PaddingBytes<T>();
				int num = math.min(this.Length, other.Length);
				for (int i = 0; i < num; i++)
				{
					int num2 = UnsafeUtility.MemCmp((void*)(ptr4 + sizeof(T) * i), (void*)(ptr5 + sizeof(T) * i), (long)sizeof(T));
					if (num2 != 0)
					{
						return num2;
					}
				}
				return this.Length.CompareTo(other.Length);
			}
		}

		// Token: 0x06000322 RID: 802 RVA: 0x00008A7C File Offset: 0x00006C7C
		public bool Equals(FixedList512Bytes<T> other)
		{
			return this.CompareTo(other) == 0;
		}

		// Token: 0x06000323 RID: 803 RVA: 0x00008A88 File Offset: 0x00006C88
		public FixedList4096Bytes(in FixedList512Bytes<T> other)
		{
			this = default(FixedList4096Bytes<T>);
			this.Initialize(other);
		}

		// Token: 0x06000324 RID: 804 RVA: 0x00008A9C File Offset: 0x00006C9C
		internal unsafe int Initialize(in FixedList512Bytes<T> other)
		{
			FixedList512Bytes<T> fixedList512Bytes = other;
			if (fixedList512Bytes.Length > this.Capacity)
			{
				return 1;
			}
			this.length = other.length;
			this.buffer = default(FixedBytes4094);
			void* destination = (void*)this.Buffer;
			fixedList512Bytes = other;
			UnsafeUtility.MemCpy(destination, (void*)fixedList512Bytes.Buffer, (long)this.LengthInBytes);
			return 0;
		}

		// Token: 0x06000325 RID: 805 RVA: 0x00008AFA File Offset: 0x00006CFA
		public static implicit operator FixedList4096Bytes<T>(in FixedList512Bytes<T> other)
		{
			return new FixedList4096Bytes<T>(ref other);
		}

		// Token: 0x06000326 RID: 806 RVA: 0x00008B04 File Offset: 0x00006D04
		public unsafe static bool operator ==(in FixedList4096Bytes<T> a, in FixedList4096Bytes<T> b)
		{
			if (a.length != b.length)
			{
				return false;
			}
			FixedList4096Bytes<T> fixedList4096Bytes = a;
			void* ptr = (void*)fixedList4096Bytes.Buffer;
			fixedList4096Bytes = b;
			void* ptr2 = (void*)fixedList4096Bytes.Buffer;
			fixedList4096Bytes = a;
			return UnsafeUtility.MemCmp(ptr, ptr2, (long)fixedList4096Bytes.LengthInBytes) == 0;
		}

		// Token: 0x06000327 RID: 807 RVA: 0x00008B54 File Offset: 0x00006D54
		public static bool operator !=(in FixedList4096Bytes<T> a, in FixedList4096Bytes<T> b)
		{
			return !(a == b);
		}

		// Token: 0x06000328 RID: 808 RVA: 0x00008B60 File Offset: 0x00006D60
		public unsafe int CompareTo(FixedList4096Bytes<T> other)
		{
			fixed (byte* ptr = &this.buffer.offset0000.byte0000)
			{
				byte* ptr2 = ptr;
				byte* ptr3 = &other.buffer.offset0000.byte0000;
				byte* ptr4 = ptr2 + FixedList.PaddingBytes<T>();
				byte* ptr5 = ptr3 + FixedList.PaddingBytes<T>();
				int num = math.min(this.Length, other.Length);
				for (int i = 0; i < num; i++)
				{
					int num2 = UnsafeUtility.MemCmp((void*)(ptr4 + sizeof(T) * i), (void*)(ptr5 + sizeof(T) * i), (long)sizeof(T));
					if (num2 != 0)
					{
						return num2;
					}
				}
				return this.Length.CompareTo(other.Length);
			}
		}

		// Token: 0x06000329 RID: 809 RVA: 0x00008C08 File Offset: 0x00006E08
		public bool Equals(FixedList4096Bytes<T> other)
		{
			return this.CompareTo(other) == 0;
		}

		// Token: 0x0600032A RID: 810 RVA: 0x00008C14 File Offset: 0x00006E14
		[NotBurstCompatible]
		public override bool Equals(object obj)
		{
			if (obj is FixedList32Bytes<T>)
			{
				FixedList32Bytes<T> other = (FixedList32Bytes<T>)obj;
				return this.Equals(other);
			}
			if (obj is FixedList64Bytes<T>)
			{
				FixedList64Bytes<T> other2 = (FixedList64Bytes<T>)obj;
				return this.Equals(other2);
			}
			if (obj is FixedList128Bytes<T>)
			{
				FixedList128Bytes<T> other3 = (FixedList128Bytes<T>)obj;
				return this.Equals(other3);
			}
			if (obj is FixedList512Bytes<T>)
			{
				FixedList512Bytes<T> other4 = (FixedList512Bytes<T>)obj;
				return this.Equals(other4);
			}
			if (obj is FixedList4096Bytes<T>)
			{
				FixedList4096Bytes<T> other5 = (FixedList4096Bytes<T>)obj;
				return this.Equals(other5);
			}
			return false;
		}

		// Token: 0x0600032B RID: 811 RVA: 0x00008C97 File Offset: 0x00006E97
		public FixedList4096Bytes<T>.Enumerator GetEnumerator()
		{
			return new FixedList4096Bytes<T>.Enumerator(ref this);
		}

		// Token: 0x0600032C RID: 812 RVA: 0x00004A71 File Offset: 0x00002C71
		IEnumerator IEnumerable.GetEnumerator()
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600032D RID: 813 RVA: 0x00004A71 File Offset: 0x00002C71
		IEnumerator<T> IEnumerable<!0>.GetEnumerator()
		{
			throw new NotImplementedException();
		}

		// Token: 0x040000C5 RID: 197
		[SerializeField]
		internal ushort length;

		// Token: 0x040000C6 RID: 198
		[SerializeField]
		internal FixedBytes4094 buffer;

		// Token: 0x0200006A RID: 106
		public struct Enumerator : IEnumerator<T>, IEnumerator, IDisposable
		{
			// Token: 0x0600032E RID: 814 RVA: 0x00008C9F File Offset: 0x00006E9F
			public Enumerator(ref FixedList4096Bytes<T> list)
			{
				this.m_List = list;
				this.m_Index = -1;
			}

			// Token: 0x0600032F RID: 815 RVA: 0x00002C2B File Offset: 0x00000E2B
			public void Dispose()
			{
			}

			// Token: 0x06000330 RID: 816 RVA: 0x00008CB4 File Offset: 0x00006EB4
			public bool MoveNext()
			{
				this.m_Index++;
				return this.m_Index < this.m_List.Length;
			}

			// Token: 0x06000331 RID: 817 RVA: 0x00008CD7 File Offset: 0x00006ED7
			public void Reset()
			{
				this.m_Index = -1;
			}

			// Token: 0x1700006E RID: 110
			// (get) Token: 0x06000332 RID: 818 RVA: 0x00008CE0 File Offset: 0x00006EE0
			public T Current
			{
				get
				{
					return this.m_List[this.m_Index];
				}
			}

			// Token: 0x1700006F RID: 111
			// (get) Token: 0x06000333 RID: 819 RVA: 0x00008CF3 File Offset: 0x00006EF3
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x040000C7 RID: 199
			private FixedList4096Bytes<T> m_List;

			// Token: 0x040000C8 RID: 200
			private int m_Index;
		}
	}
}

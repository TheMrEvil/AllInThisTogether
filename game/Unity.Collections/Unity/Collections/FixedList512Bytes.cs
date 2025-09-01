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
	// Token: 0x02000064 RID: 100
	[DebuggerTypeProxy(typeof(FixedList512BytesDebugView<>))]
	[BurstCompatible(GenericTypeArguments = new Type[]
	{
		typeof(int)
	})]
	[Serializable]
	public struct FixedList512Bytes<[IsUnmanaged] T> : INativeList<T>, IIndexable<T>, IEnumerable<T>, IEnumerable, IEquatable<FixedList32Bytes<T>>, IComparable<FixedList32Bytes<T>>, IEquatable<FixedList64Bytes<T>>, IComparable<FixedList64Bytes<T>>, IEquatable<FixedList128Bytes<T>>, IComparable<FixedList128Bytes<T>>, IEquatable<FixedList512Bytes<T>>, IComparable<FixedList512Bytes<T>>, IEquatable<FixedList4096Bytes<T>>, IComparable<FixedList4096Bytes<T>> where T : struct, ValueType
	{
		// Token: 0x1700005D RID: 93
		// (get) Token: 0x060002A4 RID: 676 RVA: 0x0000760F File Offset: 0x0000580F
		// (set) Token: 0x060002A5 RID: 677 RVA: 0x00007617 File Offset: 0x00005817
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

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x060002A6 RID: 678 RVA: 0x00007621 File Offset: 0x00005821
		[CreateProperty]
		private IEnumerable<T> Elements
		{
			get
			{
				return this.ToArray();
			}
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x060002A7 RID: 679 RVA: 0x00007629 File Offset: 0x00005829
		public bool IsEmpty
		{
			get
			{
				return this.Length == 0;
			}
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x060002A8 RID: 680 RVA: 0x00007634 File Offset: 0x00005834
		internal int LengthInBytes
		{
			get
			{
				return this.Length * UnsafeUtility.SizeOf<T>();
			}
		}

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x060002A9 RID: 681 RVA: 0x00007644 File Offset: 0x00005844
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

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x060002AA RID: 682 RVA: 0x0000766A File Offset: 0x0000586A
		// (set) Token: 0x060002AB RID: 683 RVA: 0x00002C2B File Offset: 0x00000E2B
		public int Capacity
		{
			get
			{
				return FixedList.Capacity<FixedBytes510, T>();
			}
			set
			{
			}
		}

		// Token: 0x17000063 RID: 99
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

		// Token: 0x060002AE RID: 686 RVA: 0x00007698 File Offset: 0x00005898
		public unsafe ref T ElementAt(int index)
		{
			return UnsafeUtility.ArrayElementAsRef<T>((void*)this.Buffer, index);
		}

		// Token: 0x060002AF RID: 687 RVA: 0x000076A6 File Offset: 0x000058A6
		public unsafe override int GetHashCode()
		{
			return (int)CollectionHelper.Hash((void*)this.Buffer, this.LengthInBytes);
		}

		// Token: 0x060002B0 RID: 688 RVA: 0x000076BC File Offset: 0x000058BC
		public void Add(in T item)
		{
			int num = this.Length;
			this.Length = num + 1;
			this[num] = item;
		}

		// Token: 0x060002B1 RID: 689 RVA: 0x000076E8 File Offset: 0x000058E8
		public unsafe void AddRange(void* ptr, int length)
		{
			for (int i = 0; i < length; i++)
			{
				int num = this.Length;
				this.Length = num + 1;
				this[num] = *(T*)((byte*)ptr + (IntPtr)i * (IntPtr)sizeof(T));
			}
		}

		// Token: 0x060002B2 RID: 690 RVA: 0x0000772A File Offset: 0x0000592A
		public void AddNoResize(in T item)
		{
			this.Add(item);
		}

		// Token: 0x060002B3 RID: 691 RVA: 0x00007733 File Offset: 0x00005933
		public unsafe void AddRangeNoResize(void* ptr, int length)
		{
			this.AddRange(ptr, length);
		}

		// Token: 0x060002B4 RID: 692 RVA: 0x0000773D File Offset: 0x0000593D
		public void Clear()
		{
			this.Length = 0;
		}

		// Token: 0x060002B5 RID: 693 RVA: 0x00007748 File Offset: 0x00005948
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

		// Token: 0x060002B6 RID: 694 RVA: 0x000077A6 File Offset: 0x000059A6
		public void Insert(int index, in T item)
		{
			this.InsertRangeWithBeginEnd(index, index + 1);
			this[index] = item;
		}

		// Token: 0x060002B7 RID: 695 RVA: 0x000077BF File Offset: 0x000059BF
		public void RemoveAtSwapBack(int index)
		{
			this.RemoveRangeSwapBack(index, 1);
		}

		// Token: 0x060002B8 RID: 696 RVA: 0x000077CC File Offset: 0x000059CC
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

		// Token: 0x060002B9 RID: 697 RVA: 0x0000782A File Offset: 0x00005A2A
		[Obsolete("RemoveRangeSwapBackWithBeginEnd(begin, end) is deprecated, use RemoveRangeSwapBack(index, count) instead. (RemovedAfter 2021-06-02)", false)]
		public void RemoveRangeSwapBackWithBeginEnd(int begin, int end)
		{
			this.RemoveRangeSwapBack(begin, end - begin);
		}

		// Token: 0x060002BA RID: 698 RVA: 0x00007836 File Offset: 0x00005A36
		public void RemoveAt(int index)
		{
			this.RemoveRange(index, 1);
		}

		// Token: 0x060002BB RID: 699 RVA: 0x00007840 File Offset: 0x00005A40
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

		// Token: 0x060002BC RID: 700 RVA: 0x0000789C File Offset: 0x00005A9C
		[Obsolete("RemoveRangeWithBeginEnd(begin, end) is deprecated, use RemoveRange(index, count) instead. (RemovedAfter 2021-06-02)", false)]
		public void RemoveRangeWithBeginEnd(int begin, int end)
		{
			this.RemoveRange(begin, end - begin);
		}

		// Token: 0x060002BD RID: 701 RVA: 0x000078A8 File Offset: 0x00005AA8
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

		// Token: 0x060002BE RID: 702 RVA: 0x000078EF File Offset: 0x00005AEF
		public unsafe NativeArray<T> ToNativeArray(AllocatorManager.AllocatorHandle allocator)
		{
			NativeArray<T> nativeArray = CollectionHelper.CreateNativeArray<T>(this.Length, allocator, NativeArrayOptions.UninitializedMemory);
			UnsafeUtility.MemCpy(nativeArray.GetUnsafePtr<T>(), (void*)this.Buffer, (long)this.LengthInBytes);
			return nativeArray;
		}

		// Token: 0x060002BF RID: 703 RVA: 0x00007918 File Offset: 0x00005B18
		public unsafe static bool operator ==(in FixedList512Bytes<T> a, in FixedList32Bytes<T> b)
		{
			if (a.length != b.length)
			{
				return false;
			}
			FixedList512Bytes<T> fixedList512Bytes = a;
			void* ptr = (void*)fixedList512Bytes.Buffer;
			FixedList32Bytes<T> fixedList32Bytes = b;
			void* ptr2 = (void*)fixedList32Bytes.Buffer;
			fixedList512Bytes = a;
			return UnsafeUtility.MemCmp(ptr, ptr2, (long)fixedList512Bytes.LengthInBytes) == 0;
		}

		// Token: 0x060002C0 RID: 704 RVA: 0x00007968 File Offset: 0x00005B68
		public static bool operator !=(in FixedList512Bytes<T> a, in FixedList32Bytes<T> b)
		{
			return !(a == b);
		}

		// Token: 0x060002C1 RID: 705 RVA: 0x00007974 File Offset: 0x00005B74
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

		// Token: 0x060002C2 RID: 706 RVA: 0x00007A1C File Offset: 0x00005C1C
		public bool Equals(FixedList32Bytes<T> other)
		{
			return this.CompareTo(other) == 0;
		}

		// Token: 0x060002C3 RID: 707 RVA: 0x00007A28 File Offset: 0x00005C28
		public FixedList512Bytes(in FixedList32Bytes<T> other)
		{
			this = default(FixedList512Bytes<T>);
			this.Initialize(other);
		}

		// Token: 0x060002C4 RID: 708 RVA: 0x00007A3C File Offset: 0x00005C3C
		internal unsafe int Initialize(in FixedList32Bytes<T> other)
		{
			FixedList32Bytes<T> fixedList32Bytes = other;
			if (fixedList32Bytes.Length > this.Capacity)
			{
				return 1;
			}
			this.length = other.length;
			this.buffer = default(FixedBytes510);
			void* destination = (void*)this.Buffer;
			fixedList32Bytes = other;
			UnsafeUtility.MemCpy(destination, (void*)fixedList32Bytes.Buffer, (long)this.LengthInBytes);
			return 0;
		}

		// Token: 0x060002C5 RID: 709 RVA: 0x00007A9A File Offset: 0x00005C9A
		public static implicit operator FixedList512Bytes<T>(in FixedList32Bytes<T> other)
		{
			return new FixedList512Bytes<T>(ref other);
		}

		// Token: 0x060002C6 RID: 710 RVA: 0x00007AA4 File Offset: 0x00005CA4
		public unsafe static bool operator ==(in FixedList512Bytes<T> a, in FixedList64Bytes<T> b)
		{
			if (a.length != b.length)
			{
				return false;
			}
			FixedList512Bytes<T> fixedList512Bytes = a;
			void* ptr = (void*)fixedList512Bytes.Buffer;
			FixedList64Bytes<T> fixedList64Bytes = b;
			void* ptr2 = (void*)fixedList64Bytes.Buffer;
			fixedList512Bytes = a;
			return UnsafeUtility.MemCmp(ptr, ptr2, (long)fixedList512Bytes.LengthInBytes) == 0;
		}

		// Token: 0x060002C7 RID: 711 RVA: 0x00007AF4 File Offset: 0x00005CF4
		public static bool operator !=(in FixedList512Bytes<T> a, in FixedList64Bytes<T> b)
		{
			return !(a == b);
		}

		// Token: 0x060002C8 RID: 712 RVA: 0x00007B00 File Offset: 0x00005D00
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

		// Token: 0x060002C9 RID: 713 RVA: 0x00007BA8 File Offset: 0x00005DA8
		public bool Equals(FixedList64Bytes<T> other)
		{
			return this.CompareTo(other) == 0;
		}

		// Token: 0x060002CA RID: 714 RVA: 0x00007BB4 File Offset: 0x00005DB4
		public FixedList512Bytes(in FixedList64Bytes<T> other)
		{
			this = default(FixedList512Bytes<T>);
			this.Initialize(other);
		}

		// Token: 0x060002CB RID: 715 RVA: 0x00007BC8 File Offset: 0x00005DC8
		internal unsafe int Initialize(in FixedList64Bytes<T> other)
		{
			FixedList64Bytes<T> fixedList64Bytes = other;
			if (fixedList64Bytes.Length > this.Capacity)
			{
				return 1;
			}
			this.length = other.length;
			this.buffer = default(FixedBytes510);
			void* destination = (void*)this.Buffer;
			fixedList64Bytes = other;
			UnsafeUtility.MemCpy(destination, (void*)fixedList64Bytes.Buffer, (long)this.LengthInBytes);
			return 0;
		}

		// Token: 0x060002CC RID: 716 RVA: 0x00007C26 File Offset: 0x00005E26
		public static implicit operator FixedList512Bytes<T>(in FixedList64Bytes<T> other)
		{
			return new FixedList512Bytes<T>(ref other);
		}

		// Token: 0x060002CD RID: 717 RVA: 0x00007C30 File Offset: 0x00005E30
		public unsafe static bool operator ==(in FixedList512Bytes<T> a, in FixedList128Bytes<T> b)
		{
			if (a.length != b.length)
			{
				return false;
			}
			FixedList512Bytes<T> fixedList512Bytes = a;
			void* ptr = (void*)fixedList512Bytes.Buffer;
			FixedList128Bytes<T> fixedList128Bytes = b;
			void* ptr2 = (void*)fixedList128Bytes.Buffer;
			fixedList512Bytes = a;
			return UnsafeUtility.MemCmp(ptr, ptr2, (long)fixedList512Bytes.LengthInBytes) == 0;
		}

		// Token: 0x060002CE RID: 718 RVA: 0x00007C80 File Offset: 0x00005E80
		public static bool operator !=(in FixedList512Bytes<T> a, in FixedList128Bytes<T> b)
		{
			return !(a == b);
		}

		// Token: 0x060002CF RID: 719 RVA: 0x00007C8C File Offset: 0x00005E8C
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

		// Token: 0x060002D0 RID: 720 RVA: 0x00007D34 File Offset: 0x00005F34
		public bool Equals(FixedList128Bytes<T> other)
		{
			return this.CompareTo(other) == 0;
		}

		// Token: 0x060002D1 RID: 721 RVA: 0x00007D40 File Offset: 0x00005F40
		public FixedList512Bytes(in FixedList128Bytes<T> other)
		{
			this = default(FixedList512Bytes<T>);
			this.Initialize(other);
		}

		// Token: 0x060002D2 RID: 722 RVA: 0x00007D54 File Offset: 0x00005F54
		internal unsafe int Initialize(in FixedList128Bytes<T> other)
		{
			FixedList128Bytes<T> fixedList128Bytes = other;
			if (fixedList128Bytes.Length > this.Capacity)
			{
				return 1;
			}
			this.length = other.length;
			this.buffer = default(FixedBytes510);
			void* destination = (void*)this.Buffer;
			fixedList128Bytes = other;
			UnsafeUtility.MemCpy(destination, (void*)fixedList128Bytes.Buffer, (long)this.LengthInBytes);
			return 0;
		}

		// Token: 0x060002D3 RID: 723 RVA: 0x00007DB2 File Offset: 0x00005FB2
		public static implicit operator FixedList512Bytes<T>(in FixedList128Bytes<T> other)
		{
			return new FixedList512Bytes<T>(ref other);
		}

		// Token: 0x060002D4 RID: 724 RVA: 0x00007DBC File Offset: 0x00005FBC
		public unsafe static bool operator ==(in FixedList512Bytes<T> a, in FixedList512Bytes<T> b)
		{
			if (a.length != b.length)
			{
				return false;
			}
			FixedList512Bytes<T> fixedList512Bytes = a;
			void* ptr = (void*)fixedList512Bytes.Buffer;
			fixedList512Bytes = b;
			void* ptr2 = (void*)fixedList512Bytes.Buffer;
			fixedList512Bytes = a;
			return UnsafeUtility.MemCmp(ptr, ptr2, (long)fixedList512Bytes.LengthInBytes) == 0;
		}

		// Token: 0x060002D5 RID: 725 RVA: 0x00007E0C File Offset: 0x0000600C
		public static bool operator !=(in FixedList512Bytes<T> a, in FixedList512Bytes<T> b)
		{
			return !(a == b);
		}

		// Token: 0x060002D6 RID: 726 RVA: 0x00007E18 File Offset: 0x00006018
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

		// Token: 0x060002D7 RID: 727 RVA: 0x00007EC0 File Offset: 0x000060C0
		public bool Equals(FixedList512Bytes<T> other)
		{
			return this.CompareTo(other) == 0;
		}

		// Token: 0x060002D8 RID: 728 RVA: 0x00007ECC File Offset: 0x000060CC
		public unsafe static bool operator ==(in FixedList512Bytes<T> a, in FixedList4096Bytes<T> b)
		{
			if (a.length != b.length)
			{
				return false;
			}
			FixedList512Bytes<T> fixedList512Bytes = a;
			void* ptr = (void*)fixedList512Bytes.Buffer;
			FixedList4096Bytes<T> fixedList4096Bytes = b;
			void* ptr2 = (void*)fixedList4096Bytes.Buffer;
			fixedList512Bytes = a;
			return UnsafeUtility.MemCmp(ptr, ptr2, (long)fixedList512Bytes.LengthInBytes) == 0;
		}

		// Token: 0x060002D9 RID: 729 RVA: 0x00007F1C File Offset: 0x0000611C
		public static bool operator !=(in FixedList512Bytes<T> a, in FixedList4096Bytes<T> b)
		{
			return !(a == b);
		}

		// Token: 0x060002DA RID: 730 RVA: 0x00007F28 File Offset: 0x00006128
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

		// Token: 0x060002DB RID: 731 RVA: 0x00007FD0 File Offset: 0x000061D0
		public bool Equals(FixedList4096Bytes<T> other)
		{
			return this.CompareTo(other) == 0;
		}

		// Token: 0x060002DC RID: 732 RVA: 0x00007FDC File Offset: 0x000061DC
		public FixedList512Bytes(in FixedList4096Bytes<T> other)
		{
			this = default(FixedList512Bytes<T>);
			this.Initialize(other);
		}

		// Token: 0x060002DD RID: 733 RVA: 0x00007FF0 File Offset: 0x000061F0
		internal unsafe int Initialize(in FixedList4096Bytes<T> other)
		{
			FixedList4096Bytes<T> fixedList4096Bytes = other;
			if (fixedList4096Bytes.Length > this.Capacity)
			{
				return 1;
			}
			this.length = other.length;
			this.buffer = default(FixedBytes510);
			void* destination = (void*)this.Buffer;
			fixedList4096Bytes = other;
			UnsafeUtility.MemCpy(destination, (void*)fixedList4096Bytes.Buffer, (long)this.LengthInBytes);
			return 0;
		}

		// Token: 0x060002DE RID: 734 RVA: 0x0000804E File Offset: 0x0000624E
		public static implicit operator FixedList512Bytes<T>(in FixedList4096Bytes<T> other)
		{
			return new FixedList512Bytes<T>(ref other);
		}

		// Token: 0x060002DF RID: 735 RVA: 0x00008058 File Offset: 0x00006258
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

		// Token: 0x060002E0 RID: 736 RVA: 0x000080DB File Offset: 0x000062DB
		public FixedList512Bytes<T>.Enumerator GetEnumerator()
		{
			return new FixedList512Bytes<T>.Enumerator(ref this);
		}

		// Token: 0x060002E1 RID: 737 RVA: 0x00004A71 File Offset: 0x00002C71
		IEnumerator IEnumerable.GetEnumerator()
		{
			throw new NotImplementedException();
		}

		// Token: 0x060002E2 RID: 738 RVA: 0x00004A71 File Offset: 0x00002C71
		IEnumerator<T> IEnumerable<!0>.GetEnumerator()
		{
			throw new NotImplementedException();
		}

		// Token: 0x040000C0 RID: 192
		[SerializeField]
		internal ushort length;

		// Token: 0x040000C1 RID: 193
		[SerializeField]
		internal FixedBytes510 buffer;

		// Token: 0x02000065 RID: 101
		public struct Enumerator : IEnumerator<T>, IEnumerator, IDisposable
		{
			// Token: 0x060002E3 RID: 739 RVA: 0x000080E3 File Offset: 0x000062E3
			public Enumerator(ref FixedList512Bytes<T> list)
			{
				this.m_List = list;
				this.m_Index = -1;
			}

			// Token: 0x060002E4 RID: 740 RVA: 0x00002C2B File Offset: 0x00000E2B
			public void Dispose()
			{
			}

			// Token: 0x060002E5 RID: 741 RVA: 0x000080F8 File Offset: 0x000062F8
			public bool MoveNext()
			{
				this.m_Index++;
				return this.m_Index < this.m_List.Length;
			}

			// Token: 0x060002E6 RID: 742 RVA: 0x0000811B File Offset: 0x0000631B
			public void Reset()
			{
				this.m_Index = -1;
			}

			// Token: 0x17000064 RID: 100
			// (get) Token: 0x060002E7 RID: 743 RVA: 0x00008124 File Offset: 0x00006324
			public T Current
			{
				get
				{
					return this.m_List[this.m_Index];
				}
			}

			// Token: 0x17000065 RID: 101
			// (get) Token: 0x060002E8 RID: 744 RVA: 0x00008137 File Offset: 0x00006337
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x040000C2 RID: 194
			private FixedList512Bytes<T> m_List;

			// Token: 0x040000C3 RID: 195
			private int m_Index;
		}
	}
}

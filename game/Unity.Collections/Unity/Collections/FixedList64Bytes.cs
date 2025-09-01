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
	// Token: 0x0200005A RID: 90
	[DebuggerTypeProxy(typeof(FixedList64BytesDebugView<>))]
	[BurstCompatible(GenericTypeArguments = new Type[]
	{
		typeof(int)
	})]
	[Serializable]
	public struct FixedList64Bytes<[IsUnmanaged] T> : INativeList<T>, IIndexable<T>, IEnumerable<!0>, IEnumerable, IEquatable<FixedList32Bytes<T>>, IComparable<FixedList32Bytes<T>>, IEquatable<FixedList64Bytes<T>>, IComparable<FixedList64Bytes<T>>, IEquatable<FixedList128Bytes<T>>, IComparable<FixedList128Bytes<T>>, IEquatable<FixedList512Bytes<T>>, IComparable<FixedList512Bytes<T>>, IEquatable<FixedList4096Bytes<T>>, IComparable<FixedList4096Bytes<T>> where T : struct, ValueType
	{
		// Token: 0x17000049 RID: 73
		// (get) Token: 0x0600020E RID: 526 RVA: 0x00005E97 File Offset: 0x00004097
		// (set) Token: 0x0600020F RID: 527 RVA: 0x00005E9F File Offset: 0x0000409F
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

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x06000210 RID: 528 RVA: 0x00005EA9 File Offset: 0x000040A9
		[CreateProperty]
		private IEnumerable<T> Elements
		{
			get
			{
				return this.ToArray();
			}
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x06000211 RID: 529 RVA: 0x00005EB1 File Offset: 0x000040B1
		public bool IsEmpty
		{
			get
			{
				return this.Length == 0;
			}
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x06000212 RID: 530 RVA: 0x00005EBC File Offset: 0x000040BC
		internal int LengthInBytes
		{
			get
			{
				return this.Length * UnsafeUtility.SizeOf<T>();
			}
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x06000213 RID: 531 RVA: 0x00005ECC File Offset: 0x000040CC
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

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x06000214 RID: 532 RVA: 0x00005EF2 File Offset: 0x000040F2
		// (set) Token: 0x06000215 RID: 533 RVA: 0x00002C2B File Offset: 0x00000E2B
		public int Capacity
		{
			get
			{
				return FixedList.Capacity<FixedBytes62, T>();
			}
			set
			{
			}
		}

		// Token: 0x1700004F RID: 79
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

		// Token: 0x06000218 RID: 536 RVA: 0x00005F20 File Offset: 0x00004120
		public unsafe ref T ElementAt(int index)
		{
			return UnsafeUtility.ArrayElementAsRef<T>((void*)this.Buffer, index);
		}

		// Token: 0x06000219 RID: 537 RVA: 0x00005F2E File Offset: 0x0000412E
		public unsafe override int GetHashCode()
		{
			return (int)CollectionHelper.Hash((void*)this.Buffer, this.LengthInBytes);
		}

		// Token: 0x0600021A RID: 538 RVA: 0x00005F44 File Offset: 0x00004144
		public void Add(in T item)
		{
			int num = this.Length;
			this.Length = num + 1;
			this[num] = item;
		}

		// Token: 0x0600021B RID: 539 RVA: 0x00005F70 File Offset: 0x00004170
		public unsafe void AddRange(void* ptr, int length)
		{
			for (int i = 0; i < length; i++)
			{
				int num = this.Length;
				this.Length = num + 1;
				this[num] = *(T*)((byte*)ptr + (IntPtr)i * (IntPtr)sizeof(T));
			}
		}

		// Token: 0x0600021C RID: 540 RVA: 0x00005FB2 File Offset: 0x000041B2
		public void AddNoResize(in T item)
		{
			this.Add(item);
		}

		// Token: 0x0600021D RID: 541 RVA: 0x00005FBB File Offset: 0x000041BB
		public unsafe void AddRangeNoResize(void* ptr, int length)
		{
			this.AddRange(ptr, length);
		}

		// Token: 0x0600021E RID: 542 RVA: 0x00005FC5 File Offset: 0x000041C5
		public void Clear()
		{
			this.Length = 0;
		}

		// Token: 0x0600021F RID: 543 RVA: 0x00005FD0 File Offset: 0x000041D0
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

		// Token: 0x06000220 RID: 544 RVA: 0x0000602E File Offset: 0x0000422E
		public void Insert(int index, in T item)
		{
			this.InsertRangeWithBeginEnd(index, index + 1);
			this[index] = item;
		}

		// Token: 0x06000221 RID: 545 RVA: 0x00006047 File Offset: 0x00004247
		public void RemoveAtSwapBack(int index)
		{
			this.RemoveRangeSwapBack(index, 1);
		}

		// Token: 0x06000222 RID: 546 RVA: 0x00006054 File Offset: 0x00004254
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

		// Token: 0x06000223 RID: 547 RVA: 0x000060B2 File Offset: 0x000042B2
		[Obsolete("RemoveRangeSwapBackWithBeginEnd(begin, end) is deprecated, use RemoveRangeSwapBack(index, count) instead. (RemovedAfter 2021-06-02)", false)]
		public void RemoveRangeSwapBackWithBeginEnd(int begin, int end)
		{
			this.RemoveRangeSwapBack(begin, end - begin);
		}

		// Token: 0x06000224 RID: 548 RVA: 0x000060BE File Offset: 0x000042BE
		public void RemoveAt(int index)
		{
			this.RemoveRange(index, 1);
		}

		// Token: 0x06000225 RID: 549 RVA: 0x000060C8 File Offset: 0x000042C8
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

		// Token: 0x06000226 RID: 550 RVA: 0x00006124 File Offset: 0x00004324
		[Obsolete("RemoveRangeWithBeginEnd(begin, end) is deprecated, use RemoveRange(index, count) instead. (RemovedAfter 2021-06-02)", false)]
		public void RemoveRangeWithBeginEnd(int begin, int end)
		{
			this.RemoveRange(begin, end - begin);
		}

		// Token: 0x06000227 RID: 551 RVA: 0x00006130 File Offset: 0x00004330
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

		// Token: 0x06000228 RID: 552 RVA: 0x00006177 File Offset: 0x00004377
		public unsafe NativeArray<T> ToNativeArray(AllocatorManager.AllocatorHandle allocator)
		{
			NativeArray<T> nativeArray = CollectionHelper.CreateNativeArray<T>(this.Length, allocator, NativeArrayOptions.UninitializedMemory);
			UnsafeUtility.MemCpy(nativeArray.GetUnsafePtr<T>(), (void*)this.Buffer, (long)this.LengthInBytes);
			return nativeArray;
		}

		// Token: 0x06000229 RID: 553 RVA: 0x000061A0 File Offset: 0x000043A0
		public unsafe static bool operator ==(in FixedList64Bytes<T> a, in FixedList32Bytes<T> b)
		{
			if (a.length != b.length)
			{
				return false;
			}
			FixedList64Bytes<T> fixedList64Bytes = a;
			void* ptr = (void*)fixedList64Bytes.Buffer;
			FixedList32Bytes<T> fixedList32Bytes = b;
			void* ptr2 = (void*)fixedList32Bytes.Buffer;
			fixedList64Bytes = a;
			return UnsafeUtility.MemCmp(ptr, ptr2, (long)fixedList64Bytes.LengthInBytes) == 0;
		}

		// Token: 0x0600022A RID: 554 RVA: 0x000061F0 File Offset: 0x000043F0
		public static bool operator !=(in FixedList64Bytes<T> a, in FixedList32Bytes<T> b)
		{
			return !(a == b);
		}

		// Token: 0x0600022B RID: 555 RVA: 0x000061FC File Offset: 0x000043FC
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

		// Token: 0x0600022C RID: 556 RVA: 0x000062A4 File Offset: 0x000044A4
		public bool Equals(FixedList32Bytes<T> other)
		{
			return this.CompareTo(other) == 0;
		}

		// Token: 0x0600022D RID: 557 RVA: 0x000062B0 File Offset: 0x000044B0
		public FixedList64Bytes(in FixedList32Bytes<T> other)
		{
			this = default(FixedList64Bytes<T>);
			this.Initialize(other);
		}

		// Token: 0x0600022E RID: 558 RVA: 0x000062C4 File Offset: 0x000044C4
		internal unsafe int Initialize(in FixedList32Bytes<T> other)
		{
			FixedList32Bytes<T> fixedList32Bytes = other;
			if (fixedList32Bytes.Length > this.Capacity)
			{
				return 1;
			}
			this.length = other.length;
			this.buffer = default(FixedBytes62);
			void* destination = (void*)this.Buffer;
			fixedList32Bytes = other;
			UnsafeUtility.MemCpy(destination, (void*)fixedList32Bytes.Buffer, (long)this.LengthInBytes);
			return 0;
		}

		// Token: 0x0600022F RID: 559 RVA: 0x00006322 File Offset: 0x00004522
		public static implicit operator FixedList64Bytes<T>(in FixedList32Bytes<T> other)
		{
			return new FixedList64Bytes<T>(ref other);
		}

		// Token: 0x06000230 RID: 560 RVA: 0x0000632C File Offset: 0x0000452C
		public unsafe static bool operator ==(in FixedList64Bytes<T> a, in FixedList64Bytes<T> b)
		{
			if (a.length != b.length)
			{
				return false;
			}
			FixedList64Bytes<T> fixedList64Bytes = a;
			void* ptr = (void*)fixedList64Bytes.Buffer;
			fixedList64Bytes = b;
			void* ptr2 = (void*)fixedList64Bytes.Buffer;
			fixedList64Bytes = a;
			return UnsafeUtility.MemCmp(ptr, ptr2, (long)fixedList64Bytes.LengthInBytes) == 0;
		}

		// Token: 0x06000231 RID: 561 RVA: 0x0000637C File Offset: 0x0000457C
		public static bool operator !=(in FixedList64Bytes<T> a, in FixedList64Bytes<T> b)
		{
			return !(a == b);
		}

		// Token: 0x06000232 RID: 562 RVA: 0x00006388 File Offset: 0x00004588
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

		// Token: 0x06000233 RID: 563 RVA: 0x00006430 File Offset: 0x00004630
		public bool Equals(FixedList64Bytes<T> other)
		{
			return this.CompareTo(other) == 0;
		}

		// Token: 0x06000234 RID: 564 RVA: 0x0000643C File Offset: 0x0000463C
		public unsafe static bool operator ==(in FixedList64Bytes<T> a, in FixedList128Bytes<T> b)
		{
			if (a.length != b.length)
			{
				return false;
			}
			FixedList64Bytes<T> fixedList64Bytes = a;
			void* ptr = (void*)fixedList64Bytes.Buffer;
			FixedList128Bytes<T> fixedList128Bytes = b;
			void* ptr2 = (void*)fixedList128Bytes.Buffer;
			fixedList64Bytes = a;
			return UnsafeUtility.MemCmp(ptr, ptr2, (long)fixedList64Bytes.LengthInBytes) == 0;
		}

		// Token: 0x06000235 RID: 565 RVA: 0x0000648C File Offset: 0x0000468C
		public static bool operator !=(in FixedList64Bytes<T> a, in FixedList128Bytes<T> b)
		{
			return !(a == b);
		}

		// Token: 0x06000236 RID: 566 RVA: 0x00006498 File Offset: 0x00004698
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

		// Token: 0x06000237 RID: 567 RVA: 0x00006540 File Offset: 0x00004740
		public bool Equals(FixedList128Bytes<T> other)
		{
			return this.CompareTo(other) == 0;
		}

		// Token: 0x06000238 RID: 568 RVA: 0x0000654C File Offset: 0x0000474C
		public FixedList64Bytes(in FixedList128Bytes<T> other)
		{
			this = default(FixedList64Bytes<T>);
			this.Initialize(other);
		}

		// Token: 0x06000239 RID: 569 RVA: 0x00006560 File Offset: 0x00004760
		internal unsafe int Initialize(in FixedList128Bytes<T> other)
		{
			FixedList128Bytes<T> fixedList128Bytes = other;
			if (fixedList128Bytes.Length > this.Capacity)
			{
				return 1;
			}
			this.length = other.length;
			this.buffer = default(FixedBytes62);
			void* destination = (void*)this.Buffer;
			fixedList128Bytes = other;
			UnsafeUtility.MemCpy(destination, (void*)fixedList128Bytes.Buffer, (long)this.LengthInBytes);
			return 0;
		}

		// Token: 0x0600023A RID: 570 RVA: 0x000065BE File Offset: 0x000047BE
		public static implicit operator FixedList64Bytes<T>(in FixedList128Bytes<T> other)
		{
			return new FixedList64Bytes<T>(ref other);
		}

		// Token: 0x0600023B RID: 571 RVA: 0x000065C8 File Offset: 0x000047C8
		public unsafe static bool operator ==(in FixedList64Bytes<T> a, in FixedList512Bytes<T> b)
		{
			if (a.length != b.length)
			{
				return false;
			}
			FixedList64Bytes<T> fixedList64Bytes = a;
			void* ptr = (void*)fixedList64Bytes.Buffer;
			FixedList512Bytes<T> fixedList512Bytes = b;
			void* ptr2 = (void*)fixedList512Bytes.Buffer;
			fixedList64Bytes = a;
			return UnsafeUtility.MemCmp(ptr, ptr2, (long)fixedList64Bytes.LengthInBytes) == 0;
		}

		// Token: 0x0600023C RID: 572 RVA: 0x00006618 File Offset: 0x00004818
		public static bool operator !=(in FixedList64Bytes<T> a, in FixedList512Bytes<T> b)
		{
			return !(a == b);
		}

		// Token: 0x0600023D RID: 573 RVA: 0x00006624 File Offset: 0x00004824
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

		// Token: 0x0600023E RID: 574 RVA: 0x000066CC File Offset: 0x000048CC
		public bool Equals(FixedList512Bytes<T> other)
		{
			return this.CompareTo(other) == 0;
		}

		// Token: 0x0600023F RID: 575 RVA: 0x000066D8 File Offset: 0x000048D8
		public FixedList64Bytes(in FixedList512Bytes<T> other)
		{
			this = default(FixedList64Bytes<T>);
			this.Initialize(other);
		}

		// Token: 0x06000240 RID: 576 RVA: 0x000066EC File Offset: 0x000048EC
		internal unsafe int Initialize(in FixedList512Bytes<T> other)
		{
			FixedList512Bytes<T> fixedList512Bytes = other;
			if (fixedList512Bytes.Length > this.Capacity)
			{
				return 1;
			}
			this.length = other.length;
			this.buffer = default(FixedBytes62);
			void* destination = (void*)this.Buffer;
			fixedList512Bytes = other;
			UnsafeUtility.MemCpy(destination, (void*)fixedList512Bytes.Buffer, (long)this.LengthInBytes);
			return 0;
		}

		// Token: 0x06000241 RID: 577 RVA: 0x0000674A File Offset: 0x0000494A
		public static implicit operator FixedList64Bytes<T>(in FixedList512Bytes<T> other)
		{
			return new FixedList64Bytes<T>(ref other);
		}

		// Token: 0x06000242 RID: 578 RVA: 0x00006754 File Offset: 0x00004954
		public unsafe static bool operator ==(in FixedList64Bytes<T> a, in FixedList4096Bytes<T> b)
		{
			if (a.length != b.length)
			{
				return false;
			}
			FixedList64Bytes<T> fixedList64Bytes = a;
			void* ptr = (void*)fixedList64Bytes.Buffer;
			FixedList4096Bytes<T> fixedList4096Bytes = b;
			void* ptr2 = (void*)fixedList4096Bytes.Buffer;
			fixedList64Bytes = a;
			return UnsafeUtility.MemCmp(ptr, ptr2, (long)fixedList64Bytes.LengthInBytes) == 0;
		}

		// Token: 0x06000243 RID: 579 RVA: 0x000067A4 File Offset: 0x000049A4
		public static bool operator !=(in FixedList64Bytes<T> a, in FixedList4096Bytes<T> b)
		{
			return !(a == b);
		}

		// Token: 0x06000244 RID: 580 RVA: 0x000067B0 File Offset: 0x000049B0
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

		// Token: 0x06000245 RID: 581 RVA: 0x00006858 File Offset: 0x00004A58
		public bool Equals(FixedList4096Bytes<T> other)
		{
			return this.CompareTo(other) == 0;
		}

		// Token: 0x06000246 RID: 582 RVA: 0x00006864 File Offset: 0x00004A64
		public FixedList64Bytes(in FixedList4096Bytes<T> other)
		{
			this = default(FixedList64Bytes<T>);
			this.Initialize(other);
		}

		// Token: 0x06000247 RID: 583 RVA: 0x00006878 File Offset: 0x00004A78
		internal unsafe int Initialize(in FixedList4096Bytes<T> other)
		{
			FixedList4096Bytes<T> fixedList4096Bytes = other;
			if (fixedList4096Bytes.Length > this.Capacity)
			{
				return 1;
			}
			this.length = other.length;
			this.buffer = default(FixedBytes62);
			void* destination = (void*)this.Buffer;
			fixedList4096Bytes = other;
			UnsafeUtility.MemCpy(destination, (void*)fixedList4096Bytes.Buffer, (long)this.LengthInBytes);
			return 0;
		}

		// Token: 0x06000248 RID: 584 RVA: 0x000068D6 File Offset: 0x00004AD6
		public static implicit operator FixedList64Bytes<T>(in FixedList4096Bytes<T> other)
		{
			return new FixedList64Bytes<T>(ref other);
		}

		// Token: 0x06000249 RID: 585 RVA: 0x000068E0 File Offset: 0x00004AE0
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

		// Token: 0x0600024A RID: 586 RVA: 0x00006963 File Offset: 0x00004B63
		public FixedList64Bytes<T>.Enumerator GetEnumerator()
		{
			return new FixedList64Bytes<T>.Enumerator(ref this);
		}

		// Token: 0x0600024B RID: 587 RVA: 0x00004A71 File Offset: 0x00002C71
		IEnumerator IEnumerable.GetEnumerator()
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600024C RID: 588 RVA: 0x00004A71 File Offset: 0x00002C71
		IEnumerator<T> IEnumerable<!0>.GetEnumerator()
		{
			throw new NotImplementedException();
		}

		// Token: 0x040000B6 RID: 182
		[SerializeField]
		internal ushort length;

		// Token: 0x040000B7 RID: 183
		[SerializeField]
		internal FixedBytes62 buffer;

		// Token: 0x0200005B RID: 91
		public struct Enumerator : IEnumerator<T>, IEnumerator, IDisposable
		{
			// Token: 0x0600024D RID: 589 RVA: 0x0000696B File Offset: 0x00004B6B
			public Enumerator(ref FixedList64Bytes<T> list)
			{
				this.m_List = list;
				this.m_Index = -1;
			}

			// Token: 0x0600024E RID: 590 RVA: 0x00002C2B File Offset: 0x00000E2B
			public void Dispose()
			{
			}

			// Token: 0x0600024F RID: 591 RVA: 0x00006980 File Offset: 0x00004B80
			public bool MoveNext()
			{
				this.m_Index++;
				return this.m_Index < this.m_List.Length;
			}

			// Token: 0x06000250 RID: 592 RVA: 0x000069A3 File Offset: 0x00004BA3
			public void Reset()
			{
				this.m_Index = -1;
			}

			// Token: 0x17000050 RID: 80
			// (get) Token: 0x06000251 RID: 593 RVA: 0x000069AC File Offset: 0x00004BAC
			public T Current
			{
				get
				{
					return this.m_List[this.m_Index];
				}
			}

			// Token: 0x17000051 RID: 81
			// (get) Token: 0x06000252 RID: 594 RVA: 0x000069BF File Offset: 0x00004BBF
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x040000B8 RID: 184
			private FixedList64Bytes<T> m_List;

			// Token: 0x040000B9 RID: 185
			private int m_Index;
		}
	}
}

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
	// Token: 0x02000055 RID: 85
	[DebuggerTypeProxy(typeof(FixedList32BytesDebugView<>))]
	[BurstCompatible(GenericTypeArguments = new Type[]
	{
		typeof(int)
	})]
	[Serializable]
	public struct FixedList32Bytes<[IsUnmanaged] T> : INativeList<T>, IIndexable<T>, IEnumerable<!0>, IEnumerable, IEquatable<FixedList32Bytes<T>>, IComparable<FixedList32Bytes<T>>, IEquatable<FixedList64Bytes<T>>, IComparable<FixedList64Bytes<T>>, IEquatable<FixedList128Bytes<T>>, IComparable<FixedList128Bytes<T>>, IEquatable<FixedList512Bytes<T>>, IComparable<FixedList512Bytes<T>>, IEquatable<FixedList4096Bytes<T>>, IComparable<FixedList4096Bytes<T>> where T : struct, ValueType
	{
		// Token: 0x1700003F RID: 63
		// (get) Token: 0x060001C3 RID: 451 RVA: 0x000052DB File Offset: 0x000034DB
		// (set) Token: 0x060001C4 RID: 452 RVA: 0x000052E3 File Offset: 0x000034E3
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

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x060001C5 RID: 453 RVA: 0x000052ED File Offset: 0x000034ED
		[CreateProperty]
		private IEnumerable<T> Elements
		{
			get
			{
				return this.ToArray();
			}
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x060001C6 RID: 454 RVA: 0x000052F5 File Offset: 0x000034F5
		public bool IsEmpty
		{
			get
			{
				return this.Length == 0;
			}
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x060001C7 RID: 455 RVA: 0x00005300 File Offset: 0x00003500
		internal int LengthInBytes
		{
			get
			{
				return this.Length * UnsafeUtility.SizeOf<T>();
			}
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x060001C8 RID: 456 RVA: 0x00005310 File Offset: 0x00003510
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

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x060001C9 RID: 457 RVA: 0x00005336 File Offset: 0x00003536
		// (set) Token: 0x060001CA RID: 458 RVA: 0x00002C2B File Offset: 0x00000E2B
		public int Capacity
		{
			get
			{
				return FixedList.Capacity<FixedBytes30, T>();
			}
			set
			{
			}
		}

		// Token: 0x17000045 RID: 69
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

		// Token: 0x060001CD RID: 461 RVA: 0x00005364 File Offset: 0x00003564
		public unsafe ref T ElementAt(int index)
		{
			return UnsafeUtility.ArrayElementAsRef<T>((void*)this.Buffer, index);
		}

		// Token: 0x060001CE RID: 462 RVA: 0x00005372 File Offset: 0x00003572
		public unsafe override int GetHashCode()
		{
			return (int)CollectionHelper.Hash((void*)this.Buffer, this.LengthInBytes);
		}

		// Token: 0x060001CF RID: 463 RVA: 0x00005388 File Offset: 0x00003588
		public void Add(in T item)
		{
			int num = this.Length;
			this.Length = num + 1;
			this[num] = item;
		}

		// Token: 0x060001D0 RID: 464 RVA: 0x000053B4 File Offset: 0x000035B4
		public unsafe void AddRange(void* ptr, int length)
		{
			for (int i = 0; i < length; i++)
			{
				int num = this.Length;
				this.Length = num + 1;
				this[num] = *(T*)((byte*)ptr + (IntPtr)i * (IntPtr)sizeof(T));
			}
		}

		// Token: 0x060001D1 RID: 465 RVA: 0x000053F6 File Offset: 0x000035F6
		public void AddNoResize(in T item)
		{
			this.Add(item);
		}

		// Token: 0x060001D2 RID: 466 RVA: 0x000053FF File Offset: 0x000035FF
		public unsafe void AddRangeNoResize(void* ptr, int length)
		{
			this.AddRange(ptr, length);
		}

		// Token: 0x060001D3 RID: 467 RVA: 0x00005409 File Offset: 0x00003609
		public void Clear()
		{
			this.Length = 0;
		}

		// Token: 0x060001D4 RID: 468 RVA: 0x00005414 File Offset: 0x00003614
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

		// Token: 0x060001D5 RID: 469 RVA: 0x00005472 File Offset: 0x00003672
		public void Insert(int index, in T item)
		{
			this.InsertRangeWithBeginEnd(index, index + 1);
			this[index] = item;
		}

		// Token: 0x060001D6 RID: 470 RVA: 0x0000548B File Offset: 0x0000368B
		public void RemoveAtSwapBack(int index)
		{
			this.RemoveRangeSwapBack(index, 1);
		}

		// Token: 0x060001D7 RID: 471 RVA: 0x00005498 File Offset: 0x00003698
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

		// Token: 0x060001D8 RID: 472 RVA: 0x000054F6 File Offset: 0x000036F6
		[Obsolete("RemoveRangeSwapBackWithBeginEnd(begin, end) is deprecated, use RemoveRangeSwapBack(index, count) instead. (RemovedAfter 2021-06-02)", false)]
		public void RemoveRangeSwapBackWithBeginEnd(int begin, int end)
		{
			this.RemoveRangeSwapBack(begin, end - begin);
		}

		// Token: 0x060001D9 RID: 473 RVA: 0x00005502 File Offset: 0x00003702
		public void RemoveAt(int index)
		{
			this.RemoveRange(index, 1);
		}

		// Token: 0x060001DA RID: 474 RVA: 0x0000550C File Offset: 0x0000370C
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

		// Token: 0x060001DB RID: 475 RVA: 0x00005568 File Offset: 0x00003768
		[Obsolete("RemoveRangeWithBeginEnd(begin, end) is deprecated, use RemoveRange(index, count) instead. (RemovedAfter 2021-06-02)", false)]
		public void RemoveRangeWithBeginEnd(int begin, int end)
		{
			this.RemoveRange(begin, end - begin);
		}

		// Token: 0x060001DC RID: 476 RVA: 0x00005574 File Offset: 0x00003774
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

		// Token: 0x060001DD RID: 477 RVA: 0x000055BB File Offset: 0x000037BB
		public unsafe NativeArray<T> ToNativeArray(AllocatorManager.AllocatorHandle allocator)
		{
			NativeArray<T> nativeArray = CollectionHelper.CreateNativeArray<T>(this.Length, allocator, NativeArrayOptions.UninitializedMemory);
			UnsafeUtility.MemCpy(nativeArray.GetUnsafePtr<T>(), (void*)this.Buffer, (long)this.LengthInBytes);
			return nativeArray;
		}

		// Token: 0x060001DE RID: 478 RVA: 0x000055E4 File Offset: 0x000037E4
		public unsafe static bool operator ==(in FixedList32Bytes<T> a, in FixedList32Bytes<T> b)
		{
			if (a.length != b.length)
			{
				return false;
			}
			FixedList32Bytes<T> fixedList32Bytes = a;
			void* ptr = (void*)fixedList32Bytes.Buffer;
			fixedList32Bytes = b;
			void* ptr2 = (void*)fixedList32Bytes.Buffer;
			fixedList32Bytes = a;
			return UnsafeUtility.MemCmp(ptr, ptr2, (long)fixedList32Bytes.LengthInBytes) == 0;
		}

		// Token: 0x060001DF RID: 479 RVA: 0x00005634 File Offset: 0x00003834
		public static bool operator !=(in FixedList32Bytes<T> a, in FixedList32Bytes<T> b)
		{
			return !(a == b);
		}

		// Token: 0x060001E0 RID: 480 RVA: 0x00005640 File Offset: 0x00003840
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

		// Token: 0x060001E1 RID: 481 RVA: 0x000056E8 File Offset: 0x000038E8
		public bool Equals(FixedList32Bytes<T> other)
		{
			return this.CompareTo(other) == 0;
		}

		// Token: 0x060001E2 RID: 482 RVA: 0x000056F4 File Offset: 0x000038F4
		public unsafe static bool operator ==(in FixedList32Bytes<T> a, in FixedList64Bytes<T> b)
		{
			if (a.length != b.length)
			{
				return false;
			}
			FixedList32Bytes<T> fixedList32Bytes = a;
			void* ptr = (void*)fixedList32Bytes.Buffer;
			FixedList64Bytes<T> fixedList64Bytes = b;
			void* ptr2 = (void*)fixedList64Bytes.Buffer;
			fixedList32Bytes = a;
			return UnsafeUtility.MemCmp(ptr, ptr2, (long)fixedList32Bytes.LengthInBytes) == 0;
		}

		// Token: 0x060001E3 RID: 483 RVA: 0x00005744 File Offset: 0x00003944
		public static bool operator !=(in FixedList32Bytes<T> a, in FixedList64Bytes<T> b)
		{
			return !(a == b);
		}

		// Token: 0x060001E4 RID: 484 RVA: 0x00005750 File Offset: 0x00003950
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

		// Token: 0x060001E5 RID: 485 RVA: 0x000057F8 File Offset: 0x000039F8
		public bool Equals(FixedList64Bytes<T> other)
		{
			return this.CompareTo(other) == 0;
		}

		// Token: 0x060001E6 RID: 486 RVA: 0x00005804 File Offset: 0x00003A04
		public FixedList32Bytes(in FixedList64Bytes<T> other)
		{
			this = default(FixedList32Bytes<T>);
			this.Initialize(other);
		}

		// Token: 0x060001E7 RID: 487 RVA: 0x00005818 File Offset: 0x00003A18
		internal unsafe int Initialize(in FixedList64Bytes<T> other)
		{
			FixedList64Bytes<T> fixedList64Bytes = other;
			if (fixedList64Bytes.Length > this.Capacity)
			{
				return 1;
			}
			this.length = other.length;
			this.buffer = default(FixedBytes30);
			void* destination = (void*)this.Buffer;
			fixedList64Bytes = other;
			UnsafeUtility.MemCpy(destination, (void*)fixedList64Bytes.Buffer, (long)this.LengthInBytes);
			return 0;
		}

		// Token: 0x060001E8 RID: 488 RVA: 0x00005876 File Offset: 0x00003A76
		public static implicit operator FixedList32Bytes<T>(in FixedList64Bytes<T> other)
		{
			return new FixedList32Bytes<T>(ref other);
		}

		// Token: 0x060001E9 RID: 489 RVA: 0x00005880 File Offset: 0x00003A80
		public unsafe static bool operator ==(in FixedList32Bytes<T> a, in FixedList128Bytes<T> b)
		{
			if (a.length != b.length)
			{
				return false;
			}
			FixedList32Bytes<T> fixedList32Bytes = a;
			void* ptr = (void*)fixedList32Bytes.Buffer;
			FixedList128Bytes<T> fixedList128Bytes = b;
			void* ptr2 = (void*)fixedList128Bytes.Buffer;
			fixedList32Bytes = a;
			return UnsafeUtility.MemCmp(ptr, ptr2, (long)fixedList32Bytes.LengthInBytes) == 0;
		}

		// Token: 0x060001EA RID: 490 RVA: 0x000058D0 File Offset: 0x00003AD0
		public static bool operator !=(in FixedList32Bytes<T> a, in FixedList128Bytes<T> b)
		{
			return !(a == b);
		}

		// Token: 0x060001EB RID: 491 RVA: 0x000058DC File Offset: 0x00003ADC
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

		// Token: 0x060001EC RID: 492 RVA: 0x00005984 File Offset: 0x00003B84
		public bool Equals(FixedList128Bytes<T> other)
		{
			return this.CompareTo(other) == 0;
		}

		// Token: 0x060001ED RID: 493 RVA: 0x00005990 File Offset: 0x00003B90
		public FixedList32Bytes(in FixedList128Bytes<T> other)
		{
			this = default(FixedList32Bytes<T>);
			this.Initialize(other);
		}

		// Token: 0x060001EE RID: 494 RVA: 0x000059A4 File Offset: 0x00003BA4
		internal unsafe int Initialize(in FixedList128Bytes<T> other)
		{
			FixedList128Bytes<T> fixedList128Bytes = other;
			if (fixedList128Bytes.Length > this.Capacity)
			{
				return 1;
			}
			this.length = other.length;
			this.buffer = default(FixedBytes30);
			void* destination = (void*)this.Buffer;
			fixedList128Bytes = other;
			UnsafeUtility.MemCpy(destination, (void*)fixedList128Bytes.Buffer, (long)this.LengthInBytes);
			return 0;
		}

		// Token: 0x060001EF RID: 495 RVA: 0x00005A02 File Offset: 0x00003C02
		public static implicit operator FixedList32Bytes<T>(in FixedList128Bytes<T> other)
		{
			return new FixedList32Bytes<T>(ref other);
		}

		// Token: 0x060001F0 RID: 496 RVA: 0x00005A0C File Offset: 0x00003C0C
		public unsafe static bool operator ==(in FixedList32Bytes<T> a, in FixedList512Bytes<T> b)
		{
			if (a.length != b.length)
			{
				return false;
			}
			FixedList32Bytes<T> fixedList32Bytes = a;
			void* ptr = (void*)fixedList32Bytes.Buffer;
			FixedList512Bytes<T> fixedList512Bytes = b;
			void* ptr2 = (void*)fixedList512Bytes.Buffer;
			fixedList32Bytes = a;
			return UnsafeUtility.MemCmp(ptr, ptr2, (long)fixedList32Bytes.LengthInBytes) == 0;
		}

		// Token: 0x060001F1 RID: 497 RVA: 0x00005A5C File Offset: 0x00003C5C
		public static bool operator !=(in FixedList32Bytes<T> a, in FixedList512Bytes<T> b)
		{
			return !(a == b);
		}

		// Token: 0x060001F2 RID: 498 RVA: 0x00005A68 File Offset: 0x00003C68
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

		// Token: 0x060001F3 RID: 499 RVA: 0x00005B10 File Offset: 0x00003D10
		public bool Equals(FixedList512Bytes<T> other)
		{
			return this.CompareTo(other) == 0;
		}

		// Token: 0x060001F4 RID: 500 RVA: 0x00005B1C File Offset: 0x00003D1C
		public FixedList32Bytes(in FixedList512Bytes<T> other)
		{
			this = default(FixedList32Bytes<T>);
			this.Initialize(other);
		}

		// Token: 0x060001F5 RID: 501 RVA: 0x00005B30 File Offset: 0x00003D30
		internal unsafe int Initialize(in FixedList512Bytes<T> other)
		{
			FixedList512Bytes<T> fixedList512Bytes = other;
			if (fixedList512Bytes.Length > this.Capacity)
			{
				return 1;
			}
			this.length = other.length;
			this.buffer = default(FixedBytes30);
			void* destination = (void*)this.Buffer;
			fixedList512Bytes = other;
			UnsafeUtility.MemCpy(destination, (void*)fixedList512Bytes.Buffer, (long)this.LengthInBytes);
			return 0;
		}

		// Token: 0x060001F6 RID: 502 RVA: 0x00005B8E File Offset: 0x00003D8E
		public static implicit operator FixedList32Bytes<T>(in FixedList512Bytes<T> other)
		{
			return new FixedList32Bytes<T>(ref other);
		}

		// Token: 0x060001F7 RID: 503 RVA: 0x00005B98 File Offset: 0x00003D98
		public unsafe static bool operator ==(in FixedList32Bytes<T> a, in FixedList4096Bytes<T> b)
		{
			if (a.length != b.length)
			{
				return false;
			}
			FixedList32Bytes<T> fixedList32Bytes = a;
			void* ptr = (void*)fixedList32Bytes.Buffer;
			FixedList4096Bytes<T> fixedList4096Bytes = b;
			void* ptr2 = (void*)fixedList4096Bytes.Buffer;
			fixedList32Bytes = a;
			return UnsafeUtility.MemCmp(ptr, ptr2, (long)fixedList32Bytes.LengthInBytes) == 0;
		}

		// Token: 0x060001F8 RID: 504 RVA: 0x00005BE8 File Offset: 0x00003DE8
		public static bool operator !=(in FixedList32Bytes<T> a, in FixedList4096Bytes<T> b)
		{
			return !(a == b);
		}

		// Token: 0x060001F9 RID: 505 RVA: 0x00005BF4 File Offset: 0x00003DF4
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

		// Token: 0x060001FA RID: 506 RVA: 0x00005C9C File Offset: 0x00003E9C
		public bool Equals(FixedList4096Bytes<T> other)
		{
			return this.CompareTo(other) == 0;
		}

		// Token: 0x060001FB RID: 507 RVA: 0x00005CA8 File Offset: 0x00003EA8
		public FixedList32Bytes(in FixedList4096Bytes<T> other)
		{
			this = default(FixedList32Bytes<T>);
			this.Initialize(other);
		}

		// Token: 0x060001FC RID: 508 RVA: 0x00005CBC File Offset: 0x00003EBC
		internal unsafe int Initialize(in FixedList4096Bytes<T> other)
		{
			FixedList4096Bytes<T> fixedList4096Bytes = other;
			if (fixedList4096Bytes.Length > this.Capacity)
			{
				return 1;
			}
			this.length = other.length;
			this.buffer = default(FixedBytes30);
			void* destination = (void*)this.Buffer;
			fixedList4096Bytes = other;
			UnsafeUtility.MemCpy(destination, (void*)fixedList4096Bytes.Buffer, (long)this.LengthInBytes);
			return 0;
		}

		// Token: 0x060001FD RID: 509 RVA: 0x00005D1A File Offset: 0x00003F1A
		public static implicit operator FixedList32Bytes<T>(in FixedList4096Bytes<T> other)
		{
			return new FixedList32Bytes<T>(ref other);
		}

		// Token: 0x060001FE RID: 510 RVA: 0x00005D24 File Offset: 0x00003F24
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

		// Token: 0x060001FF RID: 511 RVA: 0x00005DA7 File Offset: 0x00003FA7
		public FixedList32Bytes<T>.Enumerator GetEnumerator()
		{
			return new FixedList32Bytes<T>.Enumerator(ref this);
		}

		// Token: 0x06000200 RID: 512 RVA: 0x00004A71 File Offset: 0x00002C71
		IEnumerator IEnumerable.GetEnumerator()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000201 RID: 513 RVA: 0x00004A71 File Offset: 0x00002C71
		IEnumerator<T> IEnumerable<!0>.GetEnumerator()
		{
			throw new NotImplementedException();
		}

		// Token: 0x040000B1 RID: 177
		[SerializeField]
		internal ushort length;

		// Token: 0x040000B2 RID: 178
		[SerializeField]
		internal FixedBytes30 buffer;

		// Token: 0x02000056 RID: 86
		public struct Enumerator : IEnumerator<T>, IEnumerator, IDisposable
		{
			// Token: 0x06000202 RID: 514 RVA: 0x00005DAF File Offset: 0x00003FAF
			public Enumerator(ref FixedList32Bytes<T> list)
			{
				this.m_List = list;
				this.m_Index = -1;
			}

			// Token: 0x06000203 RID: 515 RVA: 0x00002C2B File Offset: 0x00000E2B
			public void Dispose()
			{
			}

			// Token: 0x06000204 RID: 516 RVA: 0x00005DC4 File Offset: 0x00003FC4
			public bool MoveNext()
			{
				this.m_Index++;
				return this.m_Index < this.m_List.Length;
			}

			// Token: 0x06000205 RID: 517 RVA: 0x00005DE7 File Offset: 0x00003FE7
			public void Reset()
			{
				this.m_Index = -1;
			}

			// Token: 0x17000046 RID: 70
			// (get) Token: 0x06000206 RID: 518 RVA: 0x00005DF0 File Offset: 0x00003FF0
			public T Current
			{
				get
				{
					return this.m_List[this.m_Index];
				}
			}

			// Token: 0x17000047 RID: 71
			// (get) Token: 0x06000207 RID: 519 RVA: 0x00005E03 File Offset: 0x00004003
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x040000B3 RID: 179
			private FixedList32Bytes<T> m_List;

			// Token: 0x040000B4 RID: 180
			private int m_Index;
		}
	}
}

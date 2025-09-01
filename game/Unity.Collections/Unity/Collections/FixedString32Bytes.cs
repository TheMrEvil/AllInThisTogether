using System;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

namespace Unity.Collections
{
	// Token: 0x0200008F RID: 143
	[BurstCompatible]
	[Serializable]
	[StructLayout(LayoutKind.Sequential, Size = 32)]
	public struct FixedString32Bytes : INativeList<byte>, IIndexable<byte>, IUTF8Bytes, IComparable<string>, IEquatable<string>, IComparable<FixedString32Bytes>, IEquatable<FixedString32Bytes>, IComparable<FixedString64Bytes>, IEquatable<FixedString64Bytes>, IComparable<FixedString128Bytes>, IEquatable<FixedString128Bytes>, IComparable<FixedString512Bytes>, IEquatable<FixedString512Bytes>, IComparable<FixedString4096Bytes>, IEquatable<FixedString4096Bytes>
	{
		// Token: 0x17000080 RID: 128
		// (get) Token: 0x06000362 RID: 866 RVA: 0x00009145 File Offset: 0x00007345
		public static int UTF8MaxLengthInBytes
		{
			get
			{
				return 29;
			}
		}

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x06000363 RID: 867 RVA: 0x00009149 File Offset: 0x00007349
		[CreateProperty]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[NotBurstCompatible]
		public string Value
		{
			get
			{
				return this.ToString();
			}
		}

		// Token: 0x06000364 RID: 868 RVA: 0x00009157 File Offset: 0x00007357
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe byte* GetUnsafePtr()
		{
			return (byte*)UnsafeUtility.AddressOf<FixedBytes30>(ref this.bytes);
		}

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x06000365 RID: 869 RVA: 0x00009164 File Offset: 0x00007364
		// (set) Token: 0x06000366 RID: 870 RVA: 0x0000916C File Offset: 0x0000736C
		public unsafe int Length
		{
			get
			{
				return (int)this.utf8LengthInBytes;
			}
			set
			{
				this.utf8LengthInBytes = (ushort)value;
				this.GetUnsafePtr()[this.utf8LengthInBytes] = 0;
			}
		}

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x06000367 RID: 871 RVA: 0x00009145 File Offset: 0x00007345
		// (set) Token: 0x06000368 RID: 872 RVA: 0x00002C2B File Offset: 0x00000E2B
		public int Capacity
		{
			get
			{
				return 29;
			}
			set
			{
			}
		}

		// Token: 0x06000369 RID: 873 RVA: 0x00009188 File Offset: 0x00007388
		public unsafe bool TryResize(int newLength, NativeArrayOptions clearOptions = NativeArrayOptions.ClearMemory)
		{
			if (newLength < 0 || newLength > 29)
			{
				return false;
			}
			if (newLength == (int)this.utf8LengthInBytes)
			{
				return true;
			}
			if (clearOptions == NativeArrayOptions.ClearMemory)
			{
				if (newLength > (int)this.utf8LengthInBytes)
				{
					UnsafeUtility.MemClear((void*)(this.GetUnsafePtr() + this.utf8LengthInBytes), (long)(newLength - (int)this.utf8LengthInBytes));
				}
				else
				{
					UnsafeUtility.MemClear((void*)(this.GetUnsafePtr() + newLength), (long)((int)this.utf8LengthInBytes - newLength));
				}
			}
			this.utf8LengthInBytes = (ushort)newLength;
			this.GetUnsafePtr()[this.utf8LengthInBytes] = 0;
			return true;
		}

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x0600036A RID: 874 RVA: 0x00009203 File Offset: 0x00007403
		public bool IsEmpty
		{
			get
			{
				return this.utf8LengthInBytes == 0;
			}
		}

		// Token: 0x17000085 RID: 133
		public unsafe byte this[int index]
		{
			get
			{
				return this.GetUnsafePtr()[index];
			}
			set
			{
				this.GetUnsafePtr()[index] = value;
			}
		}

		// Token: 0x0600036D RID: 877 RVA: 0x00009225 File Offset: 0x00007425
		public unsafe ref byte ElementAt(int index)
		{
			return ref this.GetUnsafePtr()[index];
		}

		// Token: 0x0600036E RID: 878 RVA: 0x0000922F File Offset: 0x0000742F
		public void Clear()
		{
			this.Length = 0;
		}

		// Token: 0x0600036F RID: 879 RVA: 0x00009238 File Offset: 0x00007438
		public void Add(in byte value)
		{
			int length = this.Length;
			this.Length = length + 1;
			this[length] = value;
		}

		// Token: 0x06000370 RID: 880 RVA: 0x0000925E File Offset: 0x0000745E
		public FixedString32Bytes.Enumerator GetEnumerator()
		{
			return new FixedString32Bytes.Enumerator(this);
		}

		// Token: 0x06000371 RID: 881 RVA: 0x0000926B File Offset: 0x0000746B
		[NotBurstCompatible]
		public int CompareTo(string other)
		{
			return this.ToString().CompareTo(other);
		}

		// Token: 0x06000372 RID: 882 RVA: 0x00009280 File Offset: 0x00007480
		[NotBurstCompatible]
		public unsafe bool Equals(string other)
		{
			int num = (int)this.utf8LengthInBytes;
			int length = other.Length;
			byte* utf8Buffer = (byte*)UnsafeUtilityExtensions.AddressOf<FixedBytes30>(this.bytes);
			char* ptr = other;
			if (ptr != null)
			{
				ptr += RuntimeHelpers.OffsetToStringData / 2;
			}
			return UTF8ArrayUnsafeUtility.StrCmp(utf8Buffer, num, ptr, length) == 0;
		}

		// Token: 0x06000373 RID: 883 RVA: 0x000092C5 File Offset: 0x000074C5
		public ref FixedList32Bytes<byte> AsFixedList()
		{
			return UnsafeUtility.AsRef<FixedList32Bytes<byte>>(UnsafeUtility.AddressOf<FixedString32Bytes>(ref this));
		}

		// Token: 0x06000374 RID: 884 RVA: 0x000092D2 File Offset: 0x000074D2
		[NotBurstCompatible]
		public FixedString32Bytes(string source)
		{
			this = default(FixedString32Bytes);
			this.Initialize(source);
		}

		// Token: 0x06000375 RID: 885 RVA: 0x000092E4 File Offset: 0x000074E4
		[NotBurstCompatible]
		internal unsafe int Initialize(string source)
		{
			this.bytes = default(FixedBytes30);
			this.utf8LengthInBytes = 0;
			fixed (string text = source)
			{
				char* ptr = text;
				if (ptr != null)
				{
					ptr += RuntimeHelpers.OffsetToStringData / 2;
				}
				CopyError copyError = UTF8ArrayUnsafeUtility.Copy(this.GetUnsafePtr(), out this.utf8LengthInBytes, 29, ptr, source.Length);
				if (copyError != CopyError.None)
				{
					return (int)copyError;
				}
				this.Length = (int)this.utf8LengthInBytes;
			}
			return 0;
		}

		// Token: 0x06000376 RID: 886 RVA: 0x00009343 File Offset: 0x00007543
		public FixedString32Bytes(Unicode.Rune rune, int count = 1)
		{
			this = default(FixedString32Bytes);
			this.Initialize(rune, count);
		}

		// Token: 0x06000377 RID: 887 RVA: 0x00009355 File Offset: 0x00007555
		internal int Initialize(Unicode.Rune rune, int count = 1)
		{
			this = default(FixedString32Bytes);
			return (int)ref this.Append(rune, count);
		}

		// Token: 0x06000378 RID: 888 RVA: 0x00009366 File Offset: 0x00007566
		public int CompareTo(FixedString32Bytes other)
		{
			return ref this.CompareTo(other);
		}

		// Token: 0x06000379 RID: 889 RVA: 0x00009370 File Offset: 0x00007570
		public FixedString32Bytes(in FixedString32Bytes other)
		{
			this = default(FixedString32Bytes);
			this.Initialize(other);
		}

		// Token: 0x0600037A RID: 890 RVA: 0x00009384 File Offset: 0x00007584
		internal unsafe int Initialize(in FixedString32Bytes other)
		{
			this.bytes = default(FixedBytes30);
			this.utf8LengthInBytes = 0;
			int length = 0;
			byte* unsafePtr = this.GetUnsafePtr();
			byte* src = (byte*)UnsafeUtilityExtensions.AddressOf<FixedBytes30>(other.bytes);
			ushort srcLength = other.utf8LengthInBytes;
			FormatError formatError = UTF8ArrayUnsafeUtility.AppendUTF8Bytes(unsafePtr, ref length, 29, src, (int)srcLength);
			if (formatError != FormatError.None)
			{
				return (int)formatError;
			}
			this.Length = length;
			return 0;
		}

		// Token: 0x0600037B RID: 891 RVA: 0x000093D8 File Offset: 0x000075D8
		public unsafe static bool operator ==(in FixedString32Bytes a, in FixedString32Bytes b)
		{
			int aLength = (int)a.utf8LengthInBytes;
			int bLength = (int)b.utf8LengthInBytes;
			byte* aBytes = (byte*)UnsafeUtilityExtensions.AddressOf<FixedBytes30>(a.bytes);
			byte* bBytes = (byte*)UnsafeUtilityExtensions.AddressOf<FixedBytes30>(b.bytes);
			return UTF8ArrayUnsafeUtility.EqualsUTF8Bytes(aBytes, aLength, bBytes, bLength);
		}

		// Token: 0x0600037C RID: 892 RVA: 0x00009414 File Offset: 0x00007614
		public static bool operator !=(in FixedString32Bytes a, in FixedString32Bytes b)
		{
			return !(a == b);
		}

		// Token: 0x0600037D RID: 893 RVA: 0x00009420 File Offset: 0x00007620
		public bool Equals(FixedString32Bytes other)
		{
			return this == other;
		}

		// Token: 0x0600037E RID: 894 RVA: 0x0000942A File Offset: 0x0000762A
		public int CompareTo(FixedString64Bytes other)
		{
			return ref this.CompareTo(other);
		}

		// Token: 0x0600037F RID: 895 RVA: 0x00009434 File Offset: 0x00007634
		public FixedString32Bytes(in FixedString64Bytes other)
		{
			this = default(FixedString32Bytes);
			this.Initialize(other);
		}

		// Token: 0x06000380 RID: 896 RVA: 0x00009448 File Offset: 0x00007648
		internal unsafe int Initialize(in FixedString64Bytes other)
		{
			this.bytes = default(FixedBytes30);
			this.utf8LengthInBytes = 0;
			int length = 0;
			byte* unsafePtr = this.GetUnsafePtr();
			byte* src = (byte*)UnsafeUtilityExtensions.AddressOf<FixedBytes62>(other.bytes);
			ushort srcLength = other.utf8LengthInBytes;
			FormatError formatError = UTF8ArrayUnsafeUtility.AppendUTF8Bytes(unsafePtr, ref length, 29, src, (int)srcLength);
			if (formatError != FormatError.None)
			{
				return (int)formatError;
			}
			this.Length = length;
			return 0;
		}

		// Token: 0x06000381 RID: 897 RVA: 0x0000949C File Offset: 0x0000769C
		public unsafe static bool operator ==(in FixedString32Bytes a, in FixedString64Bytes b)
		{
			int aLength = (int)a.utf8LengthInBytes;
			int bLength = (int)b.utf8LengthInBytes;
			byte* aBytes = (byte*)UnsafeUtilityExtensions.AddressOf<FixedBytes30>(a.bytes);
			byte* bBytes = (byte*)UnsafeUtilityExtensions.AddressOf<FixedBytes62>(b.bytes);
			return UTF8ArrayUnsafeUtility.EqualsUTF8Bytes(aBytes, aLength, bBytes, bLength);
		}

		// Token: 0x06000382 RID: 898 RVA: 0x000094D8 File Offset: 0x000076D8
		public static bool operator !=(in FixedString32Bytes a, in FixedString64Bytes b)
		{
			return !(a == b);
		}

		// Token: 0x06000383 RID: 899 RVA: 0x000094E4 File Offset: 0x000076E4
		public bool Equals(FixedString64Bytes other)
		{
			return this == other;
		}

		// Token: 0x06000384 RID: 900 RVA: 0x000094EE File Offset: 0x000076EE
		public static implicit operator FixedString64Bytes(in FixedString32Bytes fs)
		{
			return new FixedString64Bytes(ref fs);
		}

		// Token: 0x06000385 RID: 901 RVA: 0x000094F6 File Offset: 0x000076F6
		public int CompareTo(FixedString128Bytes other)
		{
			return ref this.CompareTo(other);
		}

		// Token: 0x06000386 RID: 902 RVA: 0x00009500 File Offset: 0x00007700
		public FixedString32Bytes(in FixedString128Bytes other)
		{
			this = default(FixedString32Bytes);
			this.Initialize(other);
		}

		// Token: 0x06000387 RID: 903 RVA: 0x00009514 File Offset: 0x00007714
		internal unsafe int Initialize(in FixedString128Bytes other)
		{
			this.bytes = default(FixedBytes30);
			this.utf8LengthInBytes = 0;
			int length = 0;
			byte* unsafePtr = this.GetUnsafePtr();
			byte* src = (byte*)UnsafeUtilityExtensions.AddressOf<FixedBytes126>(other.bytes);
			ushort srcLength = other.utf8LengthInBytes;
			FormatError formatError = UTF8ArrayUnsafeUtility.AppendUTF8Bytes(unsafePtr, ref length, 29, src, (int)srcLength);
			if (formatError != FormatError.None)
			{
				return (int)formatError;
			}
			this.Length = length;
			return 0;
		}

		// Token: 0x06000388 RID: 904 RVA: 0x00009568 File Offset: 0x00007768
		public unsafe static bool operator ==(in FixedString32Bytes a, in FixedString128Bytes b)
		{
			int aLength = (int)a.utf8LengthInBytes;
			int bLength = (int)b.utf8LengthInBytes;
			byte* aBytes = (byte*)UnsafeUtilityExtensions.AddressOf<FixedBytes30>(a.bytes);
			byte* bBytes = (byte*)UnsafeUtilityExtensions.AddressOf<FixedBytes126>(b.bytes);
			return UTF8ArrayUnsafeUtility.EqualsUTF8Bytes(aBytes, aLength, bBytes, bLength);
		}

		// Token: 0x06000389 RID: 905 RVA: 0x000095A4 File Offset: 0x000077A4
		public static bool operator !=(in FixedString32Bytes a, in FixedString128Bytes b)
		{
			return !(a == b);
		}

		// Token: 0x0600038A RID: 906 RVA: 0x000095B0 File Offset: 0x000077B0
		public bool Equals(FixedString128Bytes other)
		{
			return this == other;
		}

		// Token: 0x0600038B RID: 907 RVA: 0x000095BA File Offset: 0x000077BA
		public static implicit operator FixedString128Bytes(in FixedString32Bytes fs)
		{
			return new FixedString128Bytes(ref fs);
		}

		// Token: 0x0600038C RID: 908 RVA: 0x000095C2 File Offset: 0x000077C2
		public int CompareTo(FixedString512Bytes other)
		{
			return ref this.CompareTo(other);
		}

		// Token: 0x0600038D RID: 909 RVA: 0x000095CC File Offset: 0x000077CC
		public FixedString32Bytes(in FixedString512Bytes other)
		{
			this = default(FixedString32Bytes);
			this.Initialize(other);
		}

		// Token: 0x0600038E RID: 910 RVA: 0x000095E0 File Offset: 0x000077E0
		internal unsafe int Initialize(in FixedString512Bytes other)
		{
			this.bytes = default(FixedBytes30);
			this.utf8LengthInBytes = 0;
			int length = 0;
			byte* unsafePtr = this.GetUnsafePtr();
			byte* src = (byte*)UnsafeUtilityExtensions.AddressOf<FixedBytes510>(other.bytes);
			ushort srcLength = other.utf8LengthInBytes;
			FormatError formatError = UTF8ArrayUnsafeUtility.AppendUTF8Bytes(unsafePtr, ref length, 29, src, (int)srcLength);
			if (formatError != FormatError.None)
			{
				return (int)formatError;
			}
			this.Length = length;
			return 0;
		}

		// Token: 0x0600038F RID: 911 RVA: 0x00009634 File Offset: 0x00007834
		public unsafe static bool operator ==(in FixedString32Bytes a, in FixedString512Bytes b)
		{
			int aLength = (int)a.utf8LengthInBytes;
			int bLength = (int)b.utf8LengthInBytes;
			byte* aBytes = (byte*)UnsafeUtilityExtensions.AddressOf<FixedBytes30>(a.bytes);
			byte* bBytes = (byte*)UnsafeUtilityExtensions.AddressOf<FixedBytes510>(b.bytes);
			return UTF8ArrayUnsafeUtility.EqualsUTF8Bytes(aBytes, aLength, bBytes, bLength);
		}

		// Token: 0x06000390 RID: 912 RVA: 0x00009670 File Offset: 0x00007870
		public static bool operator !=(in FixedString32Bytes a, in FixedString512Bytes b)
		{
			return !(a == b);
		}

		// Token: 0x06000391 RID: 913 RVA: 0x0000967C File Offset: 0x0000787C
		public bool Equals(FixedString512Bytes other)
		{
			return this == other;
		}

		// Token: 0x06000392 RID: 914 RVA: 0x00009686 File Offset: 0x00007886
		public static implicit operator FixedString512Bytes(in FixedString32Bytes fs)
		{
			return new FixedString512Bytes(ref fs);
		}

		// Token: 0x06000393 RID: 915 RVA: 0x0000968E File Offset: 0x0000788E
		public int CompareTo(FixedString4096Bytes other)
		{
			return ref this.CompareTo(other);
		}

		// Token: 0x06000394 RID: 916 RVA: 0x00009698 File Offset: 0x00007898
		public FixedString32Bytes(in FixedString4096Bytes other)
		{
			this = default(FixedString32Bytes);
			this.Initialize(other);
		}

		// Token: 0x06000395 RID: 917 RVA: 0x000096AC File Offset: 0x000078AC
		internal unsafe int Initialize(in FixedString4096Bytes other)
		{
			this.bytes = default(FixedBytes30);
			this.utf8LengthInBytes = 0;
			int length = 0;
			byte* unsafePtr = this.GetUnsafePtr();
			byte* src = (byte*)UnsafeUtilityExtensions.AddressOf<FixedBytes4094>(other.bytes);
			ushort srcLength = other.utf8LengthInBytes;
			FormatError formatError = UTF8ArrayUnsafeUtility.AppendUTF8Bytes(unsafePtr, ref length, 29, src, (int)srcLength);
			if (formatError != FormatError.None)
			{
				return (int)formatError;
			}
			this.Length = length;
			return 0;
		}

		// Token: 0x06000396 RID: 918 RVA: 0x00009700 File Offset: 0x00007900
		public unsafe static bool operator ==(in FixedString32Bytes a, in FixedString4096Bytes b)
		{
			int aLength = (int)a.utf8LengthInBytes;
			int bLength = (int)b.utf8LengthInBytes;
			byte* aBytes = (byte*)UnsafeUtilityExtensions.AddressOf<FixedBytes30>(a.bytes);
			byte* bBytes = (byte*)UnsafeUtilityExtensions.AddressOf<FixedBytes4094>(b.bytes);
			return UTF8ArrayUnsafeUtility.EqualsUTF8Bytes(aBytes, aLength, bBytes, bLength);
		}

		// Token: 0x06000397 RID: 919 RVA: 0x0000973C File Offset: 0x0000793C
		public static bool operator !=(in FixedString32Bytes a, in FixedString4096Bytes b)
		{
			return !(a == b);
		}

		// Token: 0x06000398 RID: 920 RVA: 0x00009748 File Offset: 0x00007948
		public bool Equals(FixedString4096Bytes other)
		{
			return this == other;
		}

		// Token: 0x06000399 RID: 921 RVA: 0x00009752 File Offset: 0x00007952
		public static implicit operator FixedString4096Bytes(in FixedString32Bytes fs)
		{
			return new FixedString4096Bytes(ref fs);
		}

		// Token: 0x0600039A RID: 922 RVA: 0x0000975A File Offset: 0x0000795A
		[NotBurstCompatible]
		public static implicit operator FixedString32Bytes(string b)
		{
			return new FixedString32Bytes(b);
		}

		// Token: 0x0600039B RID: 923 RVA: 0x00009762 File Offset: 0x00007962
		[NotBurstCompatible]
		public override string ToString()
		{
			return ref this.ConvertToString<FixedString32Bytes>();
		}

		// Token: 0x0600039C RID: 924 RVA: 0x0000976A File Offset: 0x0000796A
		public override int GetHashCode()
		{
			return ref this.ComputeHashCode<FixedString32Bytes>();
		}

		// Token: 0x0600039D RID: 925 RVA: 0x00009774 File Offset: 0x00007974
		[NotBurstCompatible]
		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}
			string text = obj as string;
			if (text != null)
			{
				return this.Equals(text);
			}
			if (obj is FixedString32Bytes)
			{
				FixedString32Bytes other = (FixedString32Bytes)obj;
				return this.Equals(other);
			}
			if (obj is FixedString64Bytes)
			{
				FixedString64Bytes other2 = (FixedString64Bytes)obj;
				return this.Equals(other2);
			}
			if (obj is FixedString128Bytes)
			{
				FixedString128Bytes other3 = (FixedString128Bytes)obj;
				return this.Equals(other3);
			}
			if (obj is FixedString512Bytes)
			{
				FixedString512Bytes other4 = (FixedString512Bytes)obj;
				return this.Equals(other4);
			}
			if (obj is FixedString4096Bytes)
			{
				FixedString4096Bytes other5 = (FixedString4096Bytes)obj;
				return this.Equals(other5);
			}
			return false;
		}

		// Token: 0x0600039E RID: 926 RVA: 0x00009810 File Offset: 0x00007A10
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		private void CheckIndexInRange(int index)
		{
			if (index < 0)
			{
				throw new IndexOutOfRangeException(string.Format("Index {0} must be positive.", index));
			}
			if (index >= (int)this.utf8LengthInBytes)
			{
				throw new IndexOutOfRangeException(string.Format("Index {0} is out of range in FixedString32Bytes of '{1}' Length.", index, this.utf8LengthInBytes));
			}
		}

		// Token: 0x0600039F RID: 927 RVA: 0x00009861 File Offset: 0x00007A61
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		private void CheckLengthInRange(int length)
		{
			if (length < 0)
			{
				throw new ArgumentOutOfRangeException(string.Format("Length {0} must be positive.", length));
			}
			if (length > 29)
			{
				throw new ArgumentOutOfRangeException(string.Format("Length {0} is out of range in FixedString32Bytes of '{1}' Capacity.", length, 29));
			}
		}

		// Token: 0x060003A0 RID: 928 RVA: 0x0000989F File Offset: 0x00007A9F
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		private void CheckCapacityInRange(int capacity)
		{
			if (capacity > 29)
			{
				throw new ArgumentOutOfRangeException(string.Format("Capacity {0} must be lower than {1}.", capacity, 29));
			}
		}

		// Token: 0x060003A1 RID: 929 RVA: 0x000098C3 File Offset: 0x00007AC3
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		private static void CheckCopyError(CopyError error, string source)
		{
			if (error != CopyError.None)
			{
				throw new ArgumentException(string.Format("FixedString32Bytes: {0} while copying \"{1}\"", error, source));
			}
		}

		// Token: 0x060003A2 RID: 930 RVA: 0x000098DF File Offset: 0x00007ADF
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		private static void CheckFormatError(FormatError error)
		{
			if (error != FormatError.None)
			{
				throw new ArgumentException("Source is too long to fit into fixed string of this size");
			}
		}

		// Token: 0x040000F8 RID: 248
		internal const ushort utf8MaxLengthInBytes = 29;

		// Token: 0x040000F9 RID: 249
		[SerializeField]
		internal ushort utf8LengthInBytes;

		// Token: 0x040000FA RID: 250
		[SerializeField]
		internal FixedBytes30 bytes;

		// Token: 0x02000090 RID: 144
		public struct Enumerator : IEnumerator
		{
			// Token: 0x060003A3 RID: 931 RVA: 0x000098EF File Offset: 0x00007AEF
			public Enumerator(FixedString32Bytes other)
			{
				this.target = other;
				this.offset = 0;
				this.current = default(Unicode.Rune);
			}

			// Token: 0x060003A4 RID: 932 RVA: 0x00002C2B File Offset: 0x00000E2B
			public void Dispose()
			{
			}

			// Token: 0x060003A5 RID: 933 RVA: 0x0000990B File Offset: 0x00007B0B
			public bool MoveNext()
			{
				if (this.offset >= this.target.Length)
				{
					return false;
				}
				Unicode.Utf8ToUcs(out this.current, this.target.GetUnsafePtr(), ref this.offset, this.target.Length);
				return true;
			}

			// Token: 0x060003A6 RID: 934 RVA: 0x0000994B File Offset: 0x00007B4B
			public void Reset()
			{
				this.offset = 0;
				this.current = default(Unicode.Rune);
			}

			// Token: 0x17000086 RID: 134
			// (get) Token: 0x060003A7 RID: 935 RVA: 0x00009960 File Offset: 0x00007B60
			public Unicode.Rune Current
			{
				get
				{
					return this.current;
				}
			}

			// Token: 0x17000087 RID: 135
			// (get) Token: 0x060003A8 RID: 936 RVA: 0x00009968 File Offset: 0x00007B68
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x040000FB RID: 251
			private FixedString32Bytes target;

			// Token: 0x040000FC RID: 252
			private int offset;

			// Token: 0x040000FD RID: 253
			private Unicode.Rune current;
		}
	}
}

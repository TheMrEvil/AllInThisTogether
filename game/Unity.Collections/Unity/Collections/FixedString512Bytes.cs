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
	// Token: 0x0200009B RID: 155
	[BurstCompatible]
	[Serializable]
	[StructLayout(LayoutKind.Sequential, Size = 512)]
	public struct FixedString512Bytes : INativeList<byte>, IIndexable<byte>, IUTF8Bytes, IComparable<string>, IEquatable<string>, IComparable<FixedString32Bytes>, IEquatable<FixedString32Bytes>, IComparable<FixedString64Bytes>, IEquatable<FixedString64Bytes>, IComparable<FixedString128Bytes>, IEquatable<FixedString128Bytes>, IComparable<FixedString512Bytes>, IEquatable<FixedString512Bytes>, IComparable<FixedString4096Bytes>, IEquatable<FixedString4096Bytes>
	{
		// Token: 0x17000098 RID: 152
		// (get) Token: 0x06000434 RID: 1076 RVA: 0x0000A99D File Offset: 0x00008B9D
		public static int UTF8MaxLengthInBytes
		{
			get
			{
				return 509;
			}
		}

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x06000435 RID: 1077 RVA: 0x0000A9A4 File Offset: 0x00008BA4
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

		// Token: 0x06000436 RID: 1078 RVA: 0x0000A9B2 File Offset: 0x00008BB2
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe byte* GetUnsafePtr()
		{
			return (byte*)UnsafeUtility.AddressOf<FixedBytes510>(ref this.bytes);
		}

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x06000437 RID: 1079 RVA: 0x0000A9BF File Offset: 0x00008BBF
		// (set) Token: 0x06000438 RID: 1080 RVA: 0x0000A9C7 File Offset: 0x00008BC7
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

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x06000439 RID: 1081 RVA: 0x0000A99D File Offset: 0x00008B9D
		// (set) Token: 0x0600043A RID: 1082 RVA: 0x00002C2B File Offset: 0x00000E2B
		public int Capacity
		{
			get
			{
				return 509;
			}
			set
			{
			}
		}

		// Token: 0x0600043B RID: 1083 RVA: 0x0000A9E0 File Offset: 0x00008BE0
		public unsafe bool TryResize(int newLength, NativeArrayOptions clearOptions = NativeArrayOptions.ClearMemory)
		{
			if (newLength < 0 || newLength > 509)
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

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x0600043C RID: 1084 RVA: 0x0000AA5E File Offset: 0x00008C5E
		public bool IsEmpty
		{
			get
			{
				return this.utf8LengthInBytes == 0;
			}
		}

		// Token: 0x1700009D RID: 157
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

		// Token: 0x0600043F RID: 1087 RVA: 0x0000AA80 File Offset: 0x00008C80
		public unsafe ref byte ElementAt(int index)
		{
			return ref this.GetUnsafePtr()[index];
		}

		// Token: 0x06000440 RID: 1088 RVA: 0x0000AA8A File Offset: 0x00008C8A
		public void Clear()
		{
			this.Length = 0;
		}

		// Token: 0x06000441 RID: 1089 RVA: 0x0000AA94 File Offset: 0x00008C94
		public void Add(in byte value)
		{
			int length = this.Length;
			this.Length = length + 1;
			this[length] = value;
		}

		// Token: 0x06000442 RID: 1090 RVA: 0x0000AABA File Offset: 0x00008CBA
		public FixedString512Bytes.Enumerator GetEnumerator()
		{
			return new FixedString512Bytes.Enumerator(this);
		}

		// Token: 0x06000443 RID: 1091 RVA: 0x0000AAC7 File Offset: 0x00008CC7
		[NotBurstCompatible]
		public int CompareTo(string other)
		{
			return this.ToString().CompareTo(other);
		}

		// Token: 0x06000444 RID: 1092 RVA: 0x0000AADC File Offset: 0x00008CDC
		[NotBurstCompatible]
		public unsafe bool Equals(string other)
		{
			int num = (int)this.utf8LengthInBytes;
			int length = other.Length;
			byte* utf8Buffer = (byte*)UnsafeUtilityExtensions.AddressOf<FixedBytes510>(this.bytes);
			char* ptr = other;
			if (ptr != null)
			{
				ptr += RuntimeHelpers.OffsetToStringData / 2;
			}
			return UTF8ArrayUnsafeUtility.StrCmp(utf8Buffer, num, ptr, length) == 0;
		}

		// Token: 0x06000445 RID: 1093 RVA: 0x0000AB21 File Offset: 0x00008D21
		public ref FixedList512Bytes<byte> AsFixedList()
		{
			return UnsafeUtility.AsRef<FixedList512Bytes<byte>>(UnsafeUtility.AddressOf<FixedString512Bytes>(ref this));
		}

		// Token: 0x06000446 RID: 1094 RVA: 0x0000AB2E File Offset: 0x00008D2E
		[NotBurstCompatible]
		public FixedString512Bytes(string source)
		{
			this = default(FixedString512Bytes);
			this.Initialize(source);
		}

		// Token: 0x06000447 RID: 1095 RVA: 0x0000AB40 File Offset: 0x00008D40
		[NotBurstCompatible]
		internal unsafe int Initialize(string source)
		{
			this.bytes = default(FixedBytes510);
			this.utf8LengthInBytes = 0;
			fixed (string text = source)
			{
				char* ptr = text;
				if (ptr != null)
				{
					ptr += RuntimeHelpers.OffsetToStringData / 2;
				}
				CopyError copyError = UTF8ArrayUnsafeUtility.Copy(this.GetUnsafePtr(), out this.utf8LengthInBytes, 509, ptr, source.Length);
				if (copyError != CopyError.None)
				{
					return (int)copyError;
				}
				this.Length = (int)this.utf8LengthInBytes;
			}
			return 0;
		}

		// Token: 0x06000448 RID: 1096 RVA: 0x0000ABA2 File Offset: 0x00008DA2
		public FixedString512Bytes(Unicode.Rune rune, int count = 1)
		{
			this = default(FixedString512Bytes);
			this.Initialize(rune, count);
		}

		// Token: 0x06000449 RID: 1097 RVA: 0x0000ABB4 File Offset: 0x00008DB4
		internal int Initialize(Unicode.Rune rune, int count = 1)
		{
			this = default(FixedString512Bytes);
			return (int)ref this.Append(rune, count);
		}

		// Token: 0x0600044A RID: 1098 RVA: 0x0000ABC5 File Offset: 0x00008DC5
		public int CompareTo(FixedString32Bytes other)
		{
			return ref this.CompareTo(other);
		}

		// Token: 0x0600044B RID: 1099 RVA: 0x0000ABCF File Offset: 0x00008DCF
		public FixedString512Bytes(in FixedString32Bytes other)
		{
			this = default(FixedString512Bytes);
			this.Initialize(other);
		}

		// Token: 0x0600044C RID: 1100 RVA: 0x0000ABE0 File Offset: 0x00008DE0
		internal unsafe int Initialize(in FixedString32Bytes other)
		{
			this.bytes = default(FixedBytes510);
			this.utf8LengthInBytes = 0;
			int length = 0;
			byte* unsafePtr = this.GetUnsafePtr();
			byte* src = (byte*)UnsafeUtilityExtensions.AddressOf<FixedBytes30>(other.bytes);
			ushort srcLength = other.utf8LengthInBytes;
			FormatError formatError = UTF8ArrayUnsafeUtility.AppendUTF8Bytes(unsafePtr, ref length, 509, src, (int)srcLength);
			if (formatError != FormatError.None)
			{
				return (int)formatError;
			}
			this.Length = length;
			return 0;
		}

		// Token: 0x0600044D RID: 1101 RVA: 0x0000AC38 File Offset: 0x00008E38
		public unsafe static bool operator ==(in FixedString512Bytes a, in FixedString32Bytes b)
		{
			int aLength = (int)a.utf8LengthInBytes;
			int bLength = (int)b.utf8LengthInBytes;
			byte* aBytes = (byte*)UnsafeUtilityExtensions.AddressOf<FixedBytes510>(a.bytes);
			byte* bBytes = (byte*)UnsafeUtilityExtensions.AddressOf<FixedBytes30>(b.bytes);
			return UTF8ArrayUnsafeUtility.EqualsUTF8Bytes(aBytes, aLength, bBytes, bLength);
		}

		// Token: 0x0600044E RID: 1102 RVA: 0x0000AC74 File Offset: 0x00008E74
		public static bool operator !=(in FixedString512Bytes a, in FixedString32Bytes b)
		{
			return !(a == b);
		}

		// Token: 0x0600044F RID: 1103 RVA: 0x0000AC80 File Offset: 0x00008E80
		public bool Equals(FixedString32Bytes other)
		{
			return this == other;
		}

		// Token: 0x06000450 RID: 1104 RVA: 0x0000AC8A File Offset: 0x00008E8A
		public int CompareTo(FixedString64Bytes other)
		{
			return ref this.CompareTo(other);
		}

		// Token: 0x06000451 RID: 1105 RVA: 0x0000AC94 File Offset: 0x00008E94
		public FixedString512Bytes(in FixedString64Bytes other)
		{
			this = default(FixedString512Bytes);
			this.Initialize(other);
		}

		// Token: 0x06000452 RID: 1106 RVA: 0x0000ACA8 File Offset: 0x00008EA8
		internal unsafe int Initialize(in FixedString64Bytes other)
		{
			this.bytes = default(FixedBytes510);
			this.utf8LengthInBytes = 0;
			int length = 0;
			byte* unsafePtr = this.GetUnsafePtr();
			byte* src = (byte*)UnsafeUtilityExtensions.AddressOf<FixedBytes62>(other.bytes);
			ushort srcLength = other.utf8LengthInBytes;
			FormatError formatError = UTF8ArrayUnsafeUtility.AppendUTF8Bytes(unsafePtr, ref length, 509, src, (int)srcLength);
			if (formatError != FormatError.None)
			{
				return (int)formatError;
			}
			this.Length = length;
			return 0;
		}

		// Token: 0x06000453 RID: 1107 RVA: 0x0000AD00 File Offset: 0x00008F00
		public unsafe static bool operator ==(in FixedString512Bytes a, in FixedString64Bytes b)
		{
			int aLength = (int)a.utf8LengthInBytes;
			int bLength = (int)b.utf8LengthInBytes;
			byte* aBytes = (byte*)UnsafeUtilityExtensions.AddressOf<FixedBytes510>(a.bytes);
			byte* bBytes = (byte*)UnsafeUtilityExtensions.AddressOf<FixedBytes62>(b.bytes);
			return UTF8ArrayUnsafeUtility.EqualsUTF8Bytes(aBytes, aLength, bBytes, bLength);
		}

		// Token: 0x06000454 RID: 1108 RVA: 0x0000AD3C File Offset: 0x00008F3C
		public static bool operator !=(in FixedString512Bytes a, in FixedString64Bytes b)
		{
			return !(a == b);
		}

		// Token: 0x06000455 RID: 1109 RVA: 0x0000AD48 File Offset: 0x00008F48
		public bool Equals(FixedString64Bytes other)
		{
			return this == other;
		}

		// Token: 0x06000456 RID: 1110 RVA: 0x0000AD52 File Offset: 0x00008F52
		public int CompareTo(FixedString128Bytes other)
		{
			return ref this.CompareTo(other);
		}

		// Token: 0x06000457 RID: 1111 RVA: 0x0000AD5C File Offset: 0x00008F5C
		public FixedString512Bytes(in FixedString128Bytes other)
		{
			this = default(FixedString512Bytes);
			this.Initialize(other);
		}

		// Token: 0x06000458 RID: 1112 RVA: 0x0000AD70 File Offset: 0x00008F70
		internal unsafe int Initialize(in FixedString128Bytes other)
		{
			this.bytes = default(FixedBytes510);
			this.utf8LengthInBytes = 0;
			int length = 0;
			byte* unsafePtr = this.GetUnsafePtr();
			byte* src = (byte*)UnsafeUtilityExtensions.AddressOf<FixedBytes126>(other.bytes);
			ushort srcLength = other.utf8LengthInBytes;
			FormatError formatError = UTF8ArrayUnsafeUtility.AppendUTF8Bytes(unsafePtr, ref length, 509, src, (int)srcLength);
			if (formatError != FormatError.None)
			{
				return (int)formatError;
			}
			this.Length = length;
			return 0;
		}

		// Token: 0x06000459 RID: 1113 RVA: 0x0000ADC8 File Offset: 0x00008FC8
		public unsafe static bool operator ==(in FixedString512Bytes a, in FixedString128Bytes b)
		{
			int aLength = (int)a.utf8LengthInBytes;
			int bLength = (int)b.utf8LengthInBytes;
			byte* aBytes = (byte*)UnsafeUtilityExtensions.AddressOf<FixedBytes510>(a.bytes);
			byte* bBytes = (byte*)UnsafeUtilityExtensions.AddressOf<FixedBytes126>(b.bytes);
			return UTF8ArrayUnsafeUtility.EqualsUTF8Bytes(aBytes, aLength, bBytes, bLength);
		}

		// Token: 0x0600045A RID: 1114 RVA: 0x0000AE04 File Offset: 0x00009004
		public static bool operator !=(in FixedString512Bytes a, in FixedString128Bytes b)
		{
			return !(a == b);
		}

		// Token: 0x0600045B RID: 1115 RVA: 0x0000AE10 File Offset: 0x00009010
		public bool Equals(FixedString128Bytes other)
		{
			return this == other;
		}

		// Token: 0x0600045C RID: 1116 RVA: 0x0000AE1A File Offset: 0x0000901A
		public int CompareTo(FixedString512Bytes other)
		{
			return ref this.CompareTo(other);
		}

		// Token: 0x0600045D RID: 1117 RVA: 0x0000AE24 File Offset: 0x00009024
		public FixedString512Bytes(in FixedString512Bytes other)
		{
			this = default(FixedString512Bytes);
			this.Initialize(other);
		}

		// Token: 0x0600045E RID: 1118 RVA: 0x0000AE38 File Offset: 0x00009038
		internal unsafe int Initialize(in FixedString512Bytes other)
		{
			this.bytes = default(FixedBytes510);
			this.utf8LengthInBytes = 0;
			int length = 0;
			byte* unsafePtr = this.GetUnsafePtr();
			byte* src = (byte*)UnsafeUtilityExtensions.AddressOf<FixedBytes510>(other.bytes);
			ushort srcLength = other.utf8LengthInBytes;
			FormatError formatError = UTF8ArrayUnsafeUtility.AppendUTF8Bytes(unsafePtr, ref length, 509, src, (int)srcLength);
			if (formatError != FormatError.None)
			{
				return (int)formatError;
			}
			this.Length = length;
			return 0;
		}

		// Token: 0x0600045F RID: 1119 RVA: 0x0000AE90 File Offset: 0x00009090
		public unsafe static bool operator ==(in FixedString512Bytes a, in FixedString512Bytes b)
		{
			int aLength = (int)a.utf8LengthInBytes;
			int bLength = (int)b.utf8LengthInBytes;
			byte* aBytes = (byte*)UnsafeUtilityExtensions.AddressOf<FixedBytes510>(a.bytes);
			byte* bBytes = (byte*)UnsafeUtilityExtensions.AddressOf<FixedBytes510>(b.bytes);
			return UTF8ArrayUnsafeUtility.EqualsUTF8Bytes(aBytes, aLength, bBytes, bLength);
		}

		// Token: 0x06000460 RID: 1120 RVA: 0x0000AECC File Offset: 0x000090CC
		public static bool operator !=(in FixedString512Bytes a, in FixedString512Bytes b)
		{
			return !(a == b);
		}

		// Token: 0x06000461 RID: 1121 RVA: 0x0000AED8 File Offset: 0x000090D8
		public bool Equals(FixedString512Bytes other)
		{
			return this == other;
		}

		// Token: 0x06000462 RID: 1122 RVA: 0x0000AEE2 File Offset: 0x000090E2
		public int CompareTo(FixedString4096Bytes other)
		{
			return ref this.CompareTo(other);
		}

		// Token: 0x06000463 RID: 1123 RVA: 0x0000AEEC File Offset: 0x000090EC
		public FixedString512Bytes(in FixedString4096Bytes other)
		{
			this = default(FixedString512Bytes);
			this.Initialize(other);
		}

		// Token: 0x06000464 RID: 1124 RVA: 0x0000AF00 File Offset: 0x00009100
		internal unsafe int Initialize(in FixedString4096Bytes other)
		{
			this.bytes = default(FixedBytes510);
			this.utf8LengthInBytes = 0;
			int length = 0;
			byte* unsafePtr = this.GetUnsafePtr();
			byte* src = (byte*)UnsafeUtilityExtensions.AddressOf<FixedBytes4094>(other.bytes);
			ushort srcLength = other.utf8LengthInBytes;
			FormatError formatError = UTF8ArrayUnsafeUtility.AppendUTF8Bytes(unsafePtr, ref length, 509, src, (int)srcLength);
			if (formatError != FormatError.None)
			{
				return (int)formatError;
			}
			this.Length = length;
			return 0;
		}

		// Token: 0x06000465 RID: 1125 RVA: 0x0000AF58 File Offset: 0x00009158
		public unsafe static bool operator ==(in FixedString512Bytes a, in FixedString4096Bytes b)
		{
			int aLength = (int)a.utf8LengthInBytes;
			int bLength = (int)b.utf8LengthInBytes;
			byte* aBytes = (byte*)UnsafeUtilityExtensions.AddressOf<FixedBytes510>(a.bytes);
			byte* bBytes = (byte*)UnsafeUtilityExtensions.AddressOf<FixedBytes4094>(b.bytes);
			return UTF8ArrayUnsafeUtility.EqualsUTF8Bytes(aBytes, aLength, bBytes, bLength);
		}

		// Token: 0x06000466 RID: 1126 RVA: 0x0000AF94 File Offset: 0x00009194
		public static bool operator !=(in FixedString512Bytes a, in FixedString4096Bytes b)
		{
			return !(a == b);
		}

		// Token: 0x06000467 RID: 1127 RVA: 0x0000AFA0 File Offset: 0x000091A0
		public bool Equals(FixedString4096Bytes other)
		{
			return this == other;
		}

		// Token: 0x06000468 RID: 1128 RVA: 0x0000AFAA File Offset: 0x000091AA
		public static implicit operator FixedString4096Bytes(in FixedString512Bytes fs)
		{
			return new FixedString4096Bytes(ref fs);
		}

		// Token: 0x06000469 RID: 1129 RVA: 0x0000AFB2 File Offset: 0x000091B2
		[NotBurstCompatible]
		public static implicit operator FixedString512Bytes(string b)
		{
			return new FixedString512Bytes(b);
		}

		// Token: 0x0600046A RID: 1130 RVA: 0x0000AFBA File Offset: 0x000091BA
		[NotBurstCompatible]
		public override string ToString()
		{
			return ref this.ConvertToString<FixedString512Bytes>();
		}

		// Token: 0x0600046B RID: 1131 RVA: 0x0000AFC2 File Offset: 0x000091C2
		public override int GetHashCode()
		{
			return ref this.ComputeHashCode<FixedString512Bytes>();
		}

		// Token: 0x0600046C RID: 1132 RVA: 0x0000AFCC File Offset: 0x000091CC
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

		// Token: 0x0600046D RID: 1133 RVA: 0x0000B068 File Offset: 0x00009268
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		private void CheckIndexInRange(int index)
		{
			if (index < 0)
			{
				throw new IndexOutOfRangeException(string.Format("Index {0} must be positive.", index));
			}
			if (index >= (int)this.utf8LengthInBytes)
			{
				throw new IndexOutOfRangeException(string.Format("Index {0} is out of range in FixedString512Bytes of '{1}' Length.", index, this.utf8LengthInBytes));
			}
		}

		// Token: 0x0600046E RID: 1134 RVA: 0x0000B0BC File Offset: 0x000092BC
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		private void CheckLengthInRange(int length)
		{
			if (length < 0)
			{
				throw new ArgumentOutOfRangeException(string.Format("Length {0} must be positive.", length));
			}
			if (length > 509)
			{
				throw new ArgumentOutOfRangeException(string.Format("Length {0} is out of range in FixedString512Bytes of '{1}' Capacity.", length, 509));
			}
		}

		// Token: 0x0600046F RID: 1135 RVA: 0x0000B10B File Offset: 0x0000930B
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		private void CheckCapacityInRange(int capacity)
		{
			if (capacity > 509)
			{
				throw new ArgumentOutOfRangeException(string.Format("Capacity {0} must be lower than {1}.", capacity, 509));
			}
		}

		// Token: 0x06000470 RID: 1136 RVA: 0x0000B135 File Offset: 0x00009335
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		private static void CheckCopyError(CopyError error, string source)
		{
			if (error != CopyError.None)
			{
				throw new ArgumentException(string.Format("FixedString512Bytes: {0} while copying \"{1}\"", error, source));
			}
		}

		// Token: 0x06000471 RID: 1137 RVA: 0x000098DF File Offset: 0x00007ADF
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		private static void CheckFormatError(FormatError error)
		{
			if (error != FormatError.None)
			{
				throw new ArgumentException("Source is too long to fit into fixed string of this size");
			}
		}

		// Token: 0x0400015D RID: 349
		internal const ushort utf8MaxLengthInBytes = 509;

		// Token: 0x0400015E RID: 350
		[SerializeField]
		internal ushort utf8LengthInBytes;

		// Token: 0x0400015F RID: 351
		[SerializeField]
		internal FixedBytes510 bytes;

		// Token: 0x0200009C RID: 156
		public struct Enumerator : IEnumerator
		{
			// Token: 0x06000472 RID: 1138 RVA: 0x0000B151 File Offset: 0x00009351
			public Enumerator(FixedString512Bytes other)
			{
				this.target = other;
				this.offset = 0;
				this.current = default(Unicode.Rune);
			}

			// Token: 0x06000473 RID: 1139 RVA: 0x00002C2B File Offset: 0x00000E2B
			public void Dispose()
			{
			}

			// Token: 0x06000474 RID: 1140 RVA: 0x0000B16D File Offset: 0x0000936D
			public bool MoveNext()
			{
				if (this.offset >= this.target.Length)
				{
					return false;
				}
				Unicode.Utf8ToUcs(out this.current, this.target.GetUnsafePtr(), ref this.offset, this.target.Length);
				return true;
			}

			// Token: 0x06000475 RID: 1141 RVA: 0x0000B1AD File Offset: 0x000093AD
			public void Reset()
			{
				this.offset = 0;
				this.current = default(Unicode.Rune);
			}

			// Token: 0x1700009E RID: 158
			// (get) Token: 0x06000476 RID: 1142 RVA: 0x0000B1C2 File Offset: 0x000093C2
			public Unicode.Rune Current
			{
				get
				{
					return this.current;
				}
			}

			// Token: 0x1700009F RID: 159
			// (get) Token: 0x06000477 RID: 1143 RVA: 0x0000B1CA File Offset: 0x000093CA
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x04000160 RID: 352
			private FixedString512Bytes target;

			// Token: 0x04000161 RID: 353
			private int offset;

			// Token: 0x04000162 RID: 354
			private Unicode.Rune current;
		}
	}
}

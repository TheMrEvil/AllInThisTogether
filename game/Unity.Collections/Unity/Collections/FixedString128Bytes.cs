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
	// Token: 0x02000097 RID: 151
	[BurstCompatible]
	[Serializable]
	[StructLayout(LayoutKind.Sequential, Size = 128)]
	public struct FixedString128Bytes : INativeList<byte>, IIndexable<byte>, IUTF8Bytes, IComparable<string>, IEquatable<string>, IComparable<FixedString32Bytes>, IEquatable<FixedString32Bytes>, IComparable<FixedString64Bytes>, IEquatable<FixedString64Bytes>, IComparable<FixedString128Bytes>, IEquatable<FixedString128Bytes>, IComparable<FixedString512Bytes>, IEquatable<FixedString512Bytes>, IComparable<FixedString4096Bytes>, IEquatable<FixedString4096Bytes>
	{
		// Token: 0x17000090 RID: 144
		// (get) Token: 0x060003EF RID: 1007 RVA: 0x0000A18D File Offset: 0x0000838D
		public static int UTF8MaxLengthInBytes
		{
			get
			{
				return 125;
			}
		}

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x060003F0 RID: 1008 RVA: 0x0000A191 File Offset: 0x00008391
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

		// Token: 0x060003F1 RID: 1009 RVA: 0x0000A19F File Offset: 0x0000839F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe byte* GetUnsafePtr()
		{
			return (byte*)UnsafeUtility.AddressOf<FixedBytes126>(ref this.bytes);
		}

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x060003F2 RID: 1010 RVA: 0x0000A1AC File Offset: 0x000083AC
		// (set) Token: 0x060003F3 RID: 1011 RVA: 0x0000A1B4 File Offset: 0x000083B4
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

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x060003F4 RID: 1012 RVA: 0x0000A18D File Offset: 0x0000838D
		// (set) Token: 0x060003F5 RID: 1013 RVA: 0x00002C2B File Offset: 0x00000E2B
		public int Capacity
		{
			get
			{
				return 125;
			}
			set
			{
			}
		}

		// Token: 0x060003F6 RID: 1014 RVA: 0x0000A1D0 File Offset: 0x000083D0
		public unsafe bool TryResize(int newLength, NativeArrayOptions clearOptions = NativeArrayOptions.ClearMemory)
		{
			if (newLength < 0 || newLength > 125)
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

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x060003F7 RID: 1015 RVA: 0x0000A24B File Offset: 0x0000844B
		public bool IsEmpty
		{
			get
			{
				return this.utf8LengthInBytes == 0;
			}
		}

		// Token: 0x17000095 RID: 149
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

		// Token: 0x060003FA RID: 1018 RVA: 0x0000A26D File Offset: 0x0000846D
		public unsafe ref byte ElementAt(int index)
		{
			return ref this.GetUnsafePtr()[index];
		}

		// Token: 0x060003FB RID: 1019 RVA: 0x0000A277 File Offset: 0x00008477
		public void Clear()
		{
			this.Length = 0;
		}

		// Token: 0x060003FC RID: 1020 RVA: 0x0000A280 File Offset: 0x00008480
		public void Add(in byte value)
		{
			int length = this.Length;
			this.Length = length + 1;
			this[length] = value;
		}

		// Token: 0x060003FD RID: 1021 RVA: 0x0000A2A6 File Offset: 0x000084A6
		public FixedString128Bytes.Enumerator GetEnumerator()
		{
			return new FixedString128Bytes.Enumerator(this);
		}

		// Token: 0x060003FE RID: 1022 RVA: 0x0000A2B3 File Offset: 0x000084B3
		[NotBurstCompatible]
		public int CompareTo(string other)
		{
			return this.ToString().CompareTo(other);
		}

		// Token: 0x060003FF RID: 1023 RVA: 0x0000A2C8 File Offset: 0x000084C8
		[NotBurstCompatible]
		public unsafe bool Equals(string other)
		{
			int num = (int)this.utf8LengthInBytes;
			int length = other.Length;
			byte* utf8Buffer = (byte*)UnsafeUtilityExtensions.AddressOf<FixedBytes126>(this.bytes);
			char* ptr = other;
			if (ptr != null)
			{
				ptr += RuntimeHelpers.OffsetToStringData / 2;
			}
			return UTF8ArrayUnsafeUtility.StrCmp(utf8Buffer, num, ptr, length) == 0;
		}

		// Token: 0x06000400 RID: 1024 RVA: 0x0000A30D File Offset: 0x0000850D
		public ref FixedList128Bytes<byte> AsFixedList()
		{
			return UnsafeUtility.AsRef<FixedList128Bytes<byte>>(UnsafeUtility.AddressOf<FixedString128Bytes>(ref this));
		}

		// Token: 0x06000401 RID: 1025 RVA: 0x0000A31A File Offset: 0x0000851A
		[NotBurstCompatible]
		public FixedString128Bytes(string source)
		{
			this = default(FixedString128Bytes);
			this.Initialize(source);
		}

		// Token: 0x06000402 RID: 1026 RVA: 0x0000A32C File Offset: 0x0000852C
		[NotBurstCompatible]
		internal unsafe int Initialize(string source)
		{
			this.bytes = default(FixedBytes126);
			this.utf8LengthInBytes = 0;
			fixed (string text = source)
			{
				char* ptr = text;
				if (ptr != null)
				{
					ptr += RuntimeHelpers.OffsetToStringData / 2;
				}
				CopyError copyError = UTF8ArrayUnsafeUtility.Copy(this.GetUnsafePtr(), out this.utf8LengthInBytes, 125, ptr, source.Length);
				if (copyError != CopyError.None)
				{
					return (int)copyError;
				}
				this.Length = (int)this.utf8LengthInBytes;
			}
			return 0;
		}

		// Token: 0x06000403 RID: 1027 RVA: 0x0000A38B File Offset: 0x0000858B
		public FixedString128Bytes(Unicode.Rune rune, int count = 1)
		{
			this = default(FixedString128Bytes);
			this.Initialize(rune, count);
		}

		// Token: 0x06000404 RID: 1028 RVA: 0x0000A39D File Offset: 0x0000859D
		internal int Initialize(Unicode.Rune rune, int count = 1)
		{
			this = default(FixedString128Bytes);
			return (int)ref this.Append(rune, count);
		}

		// Token: 0x06000405 RID: 1029 RVA: 0x0000A3AE File Offset: 0x000085AE
		public int CompareTo(FixedString32Bytes other)
		{
			return ref this.CompareTo(other);
		}

		// Token: 0x06000406 RID: 1030 RVA: 0x0000A3B8 File Offset: 0x000085B8
		public FixedString128Bytes(in FixedString32Bytes other)
		{
			this = default(FixedString128Bytes);
			this.Initialize(other);
		}

		// Token: 0x06000407 RID: 1031 RVA: 0x0000A3CC File Offset: 0x000085CC
		internal unsafe int Initialize(in FixedString32Bytes other)
		{
			this.bytes = default(FixedBytes126);
			this.utf8LengthInBytes = 0;
			int length = 0;
			byte* unsafePtr = this.GetUnsafePtr();
			byte* src = (byte*)UnsafeUtilityExtensions.AddressOf<FixedBytes30>(other.bytes);
			ushort srcLength = other.utf8LengthInBytes;
			FormatError formatError = UTF8ArrayUnsafeUtility.AppendUTF8Bytes(unsafePtr, ref length, 125, src, (int)srcLength);
			if (formatError != FormatError.None)
			{
				return (int)formatError;
			}
			this.Length = length;
			return 0;
		}

		// Token: 0x06000408 RID: 1032 RVA: 0x0000A420 File Offset: 0x00008620
		public unsafe static bool operator ==(in FixedString128Bytes a, in FixedString32Bytes b)
		{
			int aLength = (int)a.utf8LengthInBytes;
			int bLength = (int)b.utf8LengthInBytes;
			byte* aBytes = (byte*)UnsafeUtilityExtensions.AddressOf<FixedBytes126>(a.bytes);
			byte* bBytes = (byte*)UnsafeUtilityExtensions.AddressOf<FixedBytes30>(b.bytes);
			return UTF8ArrayUnsafeUtility.EqualsUTF8Bytes(aBytes, aLength, bBytes, bLength);
		}

		// Token: 0x06000409 RID: 1033 RVA: 0x0000A45C File Offset: 0x0000865C
		public static bool operator !=(in FixedString128Bytes a, in FixedString32Bytes b)
		{
			return !(a == b);
		}

		// Token: 0x0600040A RID: 1034 RVA: 0x0000A468 File Offset: 0x00008668
		public bool Equals(FixedString32Bytes other)
		{
			return this == other;
		}

		// Token: 0x0600040B RID: 1035 RVA: 0x0000A472 File Offset: 0x00008672
		public int CompareTo(FixedString64Bytes other)
		{
			return ref this.CompareTo(other);
		}

		// Token: 0x0600040C RID: 1036 RVA: 0x0000A47C File Offset: 0x0000867C
		public FixedString128Bytes(in FixedString64Bytes other)
		{
			this = default(FixedString128Bytes);
			this.Initialize(other);
		}

		// Token: 0x0600040D RID: 1037 RVA: 0x0000A490 File Offset: 0x00008690
		internal unsafe int Initialize(in FixedString64Bytes other)
		{
			this.bytes = default(FixedBytes126);
			this.utf8LengthInBytes = 0;
			int length = 0;
			byte* unsafePtr = this.GetUnsafePtr();
			byte* src = (byte*)UnsafeUtilityExtensions.AddressOf<FixedBytes62>(other.bytes);
			ushort srcLength = other.utf8LengthInBytes;
			FormatError formatError = UTF8ArrayUnsafeUtility.AppendUTF8Bytes(unsafePtr, ref length, 125, src, (int)srcLength);
			if (formatError != FormatError.None)
			{
				return (int)formatError;
			}
			this.Length = length;
			return 0;
		}

		// Token: 0x0600040E RID: 1038 RVA: 0x0000A4E4 File Offset: 0x000086E4
		public unsafe static bool operator ==(in FixedString128Bytes a, in FixedString64Bytes b)
		{
			int aLength = (int)a.utf8LengthInBytes;
			int bLength = (int)b.utf8LengthInBytes;
			byte* aBytes = (byte*)UnsafeUtilityExtensions.AddressOf<FixedBytes126>(a.bytes);
			byte* bBytes = (byte*)UnsafeUtilityExtensions.AddressOf<FixedBytes62>(b.bytes);
			return UTF8ArrayUnsafeUtility.EqualsUTF8Bytes(aBytes, aLength, bBytes, bLength);
		}

		// Token: 0x0600040F RID: 1039 RVA: 0x0000A520 File Offset: 0x00008720
		public static bool operator !=(in FixedString128Bytes a, in FixedString64Bytes b)
		{
			return !(a == b);
		}

		// Token: 0x06000410 RID: 1040 RVA: 0x0000A52C File Offset: 0x0000872C
		public bool Equals(FixedString64Bytes other)
		{
			return this == other;
		}

		// Token: 0x06000411 RID: 1041 RVA: 0x0000A536 File Offset: 0x00008736
		public int CompareTo(FixedString128Bytes other)
		{
			return ref this.CompareTo(other);
		}

		// Token: 0x06000412 RID: 1042 RVA: 0x0000A540 File Offset: 0x00008740
		public FixedString128Bytes(in FixedString128Bytes other)
		{
			this = default(FixedString128Bytes);
			this.Initialize(other);
		}

		// Token: 0x06000413 RID: 1043 RVA: 0x0000A554 File Offset: 0x00008754
		internal unsafe int Initialize(in FixedString128Bytes other)
		{
			this.bytes = default(FixedBytes126);
			this.utf8LengthInBytes = 0;
			int length = 0;
			byte* unsafePtr = this.GetUnsafePtr();
			byte* src = (byte*)UnsafeUtilityExtensions.AddressOf<FixedBytes126>(other.bytes);
			ushort srcLength = other.utf8LengthInBytes;
			FormatError formatError = UTF8ArrayUnsafeUtility.AppendUTF8Bytes(unsafePtr, ref length, 125, src, (int)srcLength);
			if (formatError != FormatError.None)
			{
				return (int)formatError;
			}
			this.Length = length;
			return 0;
		}

		// Token: 0x06000414 RID: 1044 RVA: 0x0000A5A8 File Offset: 0x000087A8
		public unsafe static bool operator ==(in FixedString128Bytes a, in FixedString128Bytes b)
		{
			int aLength = (int)a.utf8LengthInBytes;
			int bLength = (int)b.utf8LengthInBytes;
			byte* aBytes = (byte*)UnsafeUtilityExtensions.AddressOf<FixedBytes126>(a.bytes);
			byte* bBytes = (byte*)UnsafeUtilityExtensions.AddressOf<FixedBytes126>(b.bytes);
			return UTF8ArrayUnsafeUtility.EqualsUTF8Bytes(aBytes, aLength, bBytes, bLength);
		}

		// Token: 0x06000415 RID: 1045 RVA: 0x0000A5E4 File Offset: 0x000087E4
		public static bool operator !=(in FixedString128Bytes a, in FixedString128Bytes b)
		{
			return !(a == b);
		}

		// Token: 0x06000416 RID: 1046 RVA: 0x0000A5F0 File Offset: 0x000087F0
		public bool Equals(FixedString128Bytes other)
		{
			return this == other;
		}

		// Token: 0x06000417 RID: 1047 RVA: 0x0000A5FA File Offset: 0x000087FA
		public int CompareTo(FixedString512Bytes other)
		{
			return ref this.CompareTo(other);
		}

		// Token: 0x06000418 RID: 1048 RVA: 0x0000A604 File Offset: 0x00008804
		public FixedString128Bytes(in FixedString512Bytes other)
		{
			this = default(FixedString128Bytes);
			this.Initialize(other);
		}

		// Token: 0x06000419 RID: 1049 RVA: 0x0000A618 File Offset: 0x00008818
		internal unsafe int Initialize(in FixedString512Bytes other)
		{
			this.bytes = default(FixedBytes126);
			this.utf8LengthInBytes = 0;
			int length = 0;
			byte* unsafePtr = this.GetUnsafePtr();
			byte* src = (byte*)UnsafeUtilityExtensions.AddressOf<FixedBytes510>(other.bytes);
			ushort srcLength = other.utf8LengthInBytes;
			FormatError formatError = UTF8ArrayUnsafeUtility.AppendUTF8Bytes(unsafePtr, ref length, 125, src, (int)srcLength);
			if (formatError != FormatError.None)
			{
				return (int)formatError;
			}
			this.Length = length;
			return 0;
		}

		// Token: 0x0600041A RID: 1050 RVA: 0x0000A66C File Offset: 0x0000886C
		public unsafe static bool operator ==(in FixedString128Bytes a, in FixedString512Bytes b)
		{
			int aLength = (int)a.utf8LengthInBytes;
			int bLength = (int)b.utf8LengthInBytes;
			byte* aBytes = (byte*)UnsafeUtilityExtensions.AddressOf<FixedBytes126>(a.bytes);
			byte* bBytes = (byte*)UnsafeUtilityExtensions.AddressOf<FixedBytes510>(b.bytes);
			return UTF8ArrayUnsafeUtility.EqualsUTF8Bytes(aBytes, aLength, bBytes, bLength);
		}

		// Token: 0x0600041B RID: 1051 RVA: 0x0000A6A8 File Offset: 0x000088A8
		public static bool operator !=(in FixedString128Bytes a, in FixedString512Bytes b)
		{
			return !(a == b);
		}

		// Token: 0x0600041C RID: 1052 RVA: 0x0000A6B4 File Offset: 0x000088B4
		public bool Equals(FixedString512Bytes other)
		{
			return this == other;
		}

		// Token: 0x0600041D RID: 1053 RVA: 0x0000A6BE File Offset: 0x000088BE
		public static implicit operator FixedString512Bytes(in FixedString128Bytes fs)
		{
			return new FixedString512Bytes(ref fs);
		}

		// Token: 0x0600041E RID: 1054 RVA: 0x0000A6C6 File Offset: 0x000088C6
		public int CompareTo(FixedString4096Bytes other)
		{
			return ref this.CompareTo(other);
		}

		// Token: 0x0600041F RID: 1055 RVA: 0x0000A6D0 File Offset: 0x000088D0
		public FixedString128Bytes(in FixedString4096Bytes other)
		{
			this = default(FixedString128Bytes);
			this.Initialize(other);
		}

		// Token: 0x06000420 RID: 1056 RVA: 0x0000A6E4 File Offset: 0x000088E4
		internal unsafe int Initialize(in FixedString4096Bytes other)
		{
			this.bytes = default(FixedBytes126);
			this.utf8LengthInBytes = 0;
			int length = 0;
			byte* unsafePtr = this.GetUnsafePtr();
			byte* src = (byte*)UnsafeUtilityExtensions.AddressOf<FixedBytes4094>(other.bytes);
			ushort srcLength = other.utf8LengthInBytes;
			FormatError formatError = UTF8ArrayUnsafeUtility.AppendUTF8Bytes(unsafePtr, ref length, 125, src, (int)srcLength);
			if (formatError != FormatError.None)
			{
				return (int)formatError;
			}
			this.Length = length;
			return 0;
		}

		// Token: 0x06000421 RID: 1057 RVA: 0x0000A738 File Offset: 0x00008938
		public unsafe static bool operator ==(in FixedString128Bytes a, in FixedString4096Bytes b)
		{
			int aLength = (int)a.utf8LengthInBytes;
			int bLength = (int)b.utf8LengthInBytes;
			byte* aBytes = (byte*)UnsafeUtilityExtensions.AddressOf<FixedBytes126>(a.bytes);
			byte* bBytes = (byte*)UnsafeUtilityExtensions.AddressOf<FixedBytes4094>(b.bytes);
			return UTF8ArrayUnsafeUtility.EqualsUTF8Bytes(aBytes, aLength, bBytes, bLength);
		}

		// Token: 0x06000422 RID: 1058 RVA: 0x0000A774 File Offset: 0x00008974
		public static bool operator !=(in FixedString128Bytes a, in FixedString4096Bytes b)
		{
			return !(a == b);
		}

		// Token: 0x06000423 RID: 1059 RVA: 0x0000A780 File Offset: 0x00008980
		public bool Equals(FixedString4096Bytes other)
		{
			return this == other;
		}

		// Token: 0x06000424 RID: 1060 RVA: 0x0000A78A File Offset: 0x0000898A
		public static implicit operator FixedString4096Bytes(in FixedString128Bytes fs)
		{
			return new FixedString4096Bytes(ref fs);
		}

		// Token: 0x06000425 RID: 1061 RVA: 0x0000A792 File Offset: 0x00008992
		[NotBurstCompatible]
		public static implicit operator FixedString128Bytes(string b)
		{
			return new FixedString128Bytes(b);
		}

		// Token: 0x06000426 RID: 1062 RVA: 0x0000A79A File Offset: 0x0000899A
		[NotBurstCompatible]
		public override string ToString()
		{
			return ref this.ConvertToString<FixedString128Bytes>();
		}

		// Token: 0x06000427 RID: 1063 RVA: 0x0000A7A2 File Offset: 0x000089A2
		public override int GetHashCode()
		{
			return ref this.ComputeHashCode<FixedString128Bytes>();
		}

		// Token: 0x06000428 RID: 1064 RVA: 0x0000A7AC File Offset: 0x000089AC
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

		// Token: 0x06000429 RID: 1065 RVA: 0x0000A848 File Offset: 0x00008A48
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		private void CheckIndexInRange(int index)
		{
			if (index < 0)
			{
				throw new IndexOutOfRangeException(string.Format("Index {0} must be positive.", index));
			}
			if (index >= (int)this.utf8LengthInBytes)
			{
				throw new IndexOutOfRangeException(string.Format("Index {0} is out of range in FixedString128Bytes of '{1}' Length.", index, this.utf8LengthInBytes));
			}
		}

		// Token: 0x0600042A RID: 1066 RVA: 0x0000A899 File Offset: 0x00008A99
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		private void CheckLengthInRange(int length)
		{
			if (length < 0)
			{
				throw new ArgumentOutOfRangeException(string.Format("Length {0} must be positive.", length));
			}
			if (length > 125)
			{
				throw new ArgumentOutOfRangeException(string.Format("Length {0} is out of range in FixedString128Bytes of '{1}' Capacity.", length, 125));
			}
		}

		// Token: 0x0600042B RID: 1067 RVA: 0x0000A8D7 File Offset: 0x00008AD7
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		private void CheckCapacityInRange(int capacity)
		{
			if (capacity > 125)
			{
				throw new ArgumentOutOfRangeException(string.Format("Capacity {0} must be lower than {1}.", capacity, 125));
			}
		}

		// Token: 0x0600042C RID: 1068 RVA: 0x0000A8FB File Offset: 0x00008AFB
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		private static void CheckCopyError(CopyError error, string source)
		{
			if (error != CopyError.None)
			{
				throw new ArgumentException(string.Format("FixedString128Bytes: {0} while copying \"{1}\"", error, source));
			}
		}

		// Token: 0x0600042D RID: 1069 RVA: 0x000098DF File Offset: 0x00007ADF
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		private static void CheckFormatError(FormatError error)
		{
			if (error != FormatError.None)
			{
				throw new ArgumentException("Source is too long to fit into fixed string of this size");
			}
		}

		// Token: 0x0400012A RID: 298
		internal const ushort utf8MaxLengthInBytes = 125;

		// Token: 0x0400012B RID: 299
		[SerializeField]
		internal ushort utf8LengthInBytes;

		// Token: 0x0400012C RID: 300
		[SerializeField]
		internal FixedBytes126 bytes;

		// Token: 0x02000098 RID: 152
		public struct Enumerator : IEnumerator
		{
			// Token: 0x0600042E RID: 1070 RVA: 0x0000A917 File Offset: 0x00008B17
			public Enumerator(FixedString128Bytes other)
			{
				this.target = other;
				this.offset = 0;
				this.current = default(Unicode.Rune);
			}

			// Token: 0x0600042F RID: 1071 RVA: 0x00002C2B File Offset: 0x00000E2B
			public void Dispose()
			{
			}

			// Token: 0x06000430 RID: 1072 RVA: 0x0000A933 File Offset: 0x00008B33
			public bool MoveNext()
			{
				if (this.offset >= this.target.Length)
				{
					return false;
				}
				Unicode.Utf8ToUcs(out this.current, this.target.GetUnsafePtr(), ref this.offset, this.target.Length);
				return true;
			}

			// Token: 0x06000431 RID: 1073 RVA: 0x0000A973 File Offset: 0x00008B73
			public void Reset()
			{
				this.offset = 0;
				this.current = default(Unicode.Rune);
			}

			// Token: 0x17000096 RID: 150
			// (get) Token: 0x06000432 RID: 1074 RVA: 0x0000A988 File Offset: 0x00008B88
			public Unicode.Rune Current
			{
				get
				{
					return this.current;
				}
			}

			// Token: 0x17000097 RID: 151
			// (get) Token: 0x06000433 RID: 1075 RVA: 0x0000A990 File Offset: 0x00008B90
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x0400012D RID: 301
			private FixedString128Bytes target;

			// Token: 0x0400012E RID: 302
			private int offset;

			// Token: 0x0400012F RID: 303
			private Unicode.Rune current;
		}
	}
}

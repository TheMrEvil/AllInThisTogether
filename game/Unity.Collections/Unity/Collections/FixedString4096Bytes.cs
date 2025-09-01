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
	// Token: 0x0200009F RID: 159
	[BurstCompatible]
	[Serializable]
	[StructLayout(LayoutKind.Sequential, Size = 4096)]
	public struct FixedString4096Bytes : INativeList<byte>, IIndexable<byte>, IUTF8Bytes, IComparable<string>, IEquatable<string>, IComparable<FixedString32Bytes>, IEquatable<FixedString32Bytes>, IComparable<FixedString64Bytes>, IEquatable<FixedString64Bytes>, IComparable<FixedString128Bytes>, IEquatable<FixedString128Bytes>, IComparable<FixedString512Bytes>, IEquatable<FixedString512Bytes>, IComparable<FixedString4096Bytes>, IEquatable<FixedString4096Bytes>
	{
		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x06000478 RID: 1144 RVA: 0x0000B1D7 File Offset: 0x000093D7
		public static int UTF8MaxLengthInBytes
		{
			get
			{
				return 4093;
			}
		}

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x06000479 RID: 1145 RVA: 0x0000B1DE File Offset: 0x000093DE
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

		// Token: 0x0600047A RID: 1146 RVA: 0x0000B1EC File Offset: 0x000093EC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe byte* GetUnsafePtr()
		{
			return (byte*)UnsafeUtility.AddressOf<FixedBytes4094>(ref this.bytes);
		}

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x0600047B RID: 1147 RVA: 0x0000B1F9 File Offset: 0x000093F9
		// (set) Token: 0x0600047C RID: 1148 RVA: 0x0000B201 File Offset: 0x00009401
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

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x0600047D RID: 1149 RVA: 0x0000B1D7 File Offset: 0x000093D7
		// (set) Token: 0x0600047E RID: 1150 RVA: 0x00002C2B File Offset: 0x00000E2B
		public int Capacity
		{
			get
			{
				return 4093;
			}
			set
			{
			}
		}

		// Token: 0x0600047F RID: 1151 RVA: 0x0000B21C File Offset: 0x0000941C
		public unsafe bool TryResize(int newLength, NativeArrayOptions clearOptions = NativeArrayOptions.ClearMemory)
		{
			if (newLength < 0 || newLength > 4093)
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

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x06000480 RID: 1152 RVA: 0x0000B29A File Offset: 0x0000949A
		public bool IsEmpty
		{
			get
			{
				return this.utf8LengthInBytes == 0;
			}
		}

		// Token: 0x170000A5 RID: 165
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

		// Token: 0x06000483 RID: 1155 RVA: 0x0000B2BC File Offset: 0x000094BC
		public unsafe ref byte ElementAt(int index)
		{
			return ref this.GetUnsafePtr()[index];
		}

		// Token: 0x06000484 RID: 1156 RVA: 0x0000B2C6 File Offset: 0x000094C6
		public void Clear()
		{
			this.Length = 0;
		}

		// Token: 0x06000485 RID: 1157 RVA: 0x0000B2D0 File Offset: 0x000094D0
		public void Add(in byte value)
		{
			int length = this.Length;
			this.Length = length + 1;
			this[length] = value;
		}

		// Token: 0x06000486 RID: 1158 RVA: 0x0000B2F6 File Offset: 0x000094F6
		public FixedString4096Bytes.Enumerator GetEnumerator()
		{
			return new FixedString4096Bytes.Enumerator(this);
		}

		// Token: 0x06000487 RID: 1159 RVA: 0x0000B303 File Offset: 0x00009503
		[NotBurstCompatible]
		public int CompareTo(string other)
		{
			return this.ToString().CompareTo(other);
		}

		// Token: 0x06000488 RID: 1160 RVA: 0x0000B318 File Offset: 0x00009518
		[NotBurstCompatible]
		public unsafe bool Equals(string other)
		{
			int num = (int)this.utf8LengthInBytes;
			int length = other.Length;
			byte* utf8Buffer = (byte*)UnsafeUtilityExtensions.AddressOf<FixedBytes4094>(this.bytes);
			char* ptr = other;
			if (ptr != null)
			{
				ptr += RuntimeHelpers.OffsetToStringData / 2;
			}
			return UTF8ArrayUnsafeUtility.StrCmp(utf8Buffer, num, ptr, length) == 0;
		}

		// Token: 0x06000489 RID: 1161 RVA: 0x0000B35D File Offset: 0x0000955D
		public ref FixedList4096Bytes<byte> AsFixedList()
		{
			return UnsafeUtility.AsRef<FixedList4096Bytes<byte>>(UnsafeUtility.AddressOf<FixedString4096Bytes>(ref this));
		}

		// Token: 0x0600048A RID: 1162 RVA: 0x0000B36A File Offset: 0x0000956A
		[NotBurstCompatible]
		public FixedString4096Bytes(string source)
		{
			this = default(FixedString4096Bytes);
			this.Initialize(source);
		}

		// Token: 0x0600048B RID: 1163 RVA: 0x0000B37C File Offset: 0x0000957C
		[NotBurstCompatible]
		internal unsafe int Initialize(string source)
		{
			this.bytes = default(FixedBytes4094);
			this.utf8LengthInBytes = 0;
			fixed (string text = source)
			{
				char* ptr = text;
				if (ptr != null)
				{
					ptr += RuntimeHelpers.OffsetToStringData / 2;
				}
				CopyError copyError = UTF8ArrayUnsafeUtility.Copy(this.GetUnsafePtr(), out this.utf8LengthInBytes, 4093, ptr, source.Length);
				if (copyError != CopyError.None)
				{
					return (int)copyError;
				}
				this.Length = (int)this.utf8LengthInBytes;
			}
			return 0;
		}

		// Token: 0x0600048C RID: 1164 RVA: 0x0000B3DE File Offset: 0x000095DE
		public FixedString4096Bytes(Unicode.Rune rune, int count = 1)
		{
			this = default(FixedString4096Bytes);
			this.Initialize(rune, count);
		}

		// Token: 0x0600048D RID: 1165 RVA: 0x0000B3F0 File Offset: 0x000095F0
		internal int Initialize(Unicode.Rune rune, int count = 1)
		{
			this = default(FixedString4096Bytes);
			return (int)ref this.Append(rune, count);
		}

		// Token: 0x0600048E RID: 1166 RVA: 0x0000B401 File Offset: 0x00009601
		public int CompareTo(FixedString32Bytes other)
		{
			return ref this.CompareTo(other);
		}

		// Token: 0x0600048F RID: 1167 RVA: 0x0000B40B File Offset: 0x0000960B
		public FixedString4096Bytes(in FixedString32Bytes other)
		{
			this = default(FixedString4096Bytes);
			this.Initialize(other);
		}

		// Token: 0x06000490 RID: 1168 RVA: 0x0000B41C File Offset: 0x0000961C
		internal unsafe int Initialize(in FixedString32Bytes other)
		{
			this.bytes = default(FixedBytes4094);
			this.utf8LengthInBytes = 0;
			int length = 0;
			byte* unsafePtr = this.GetUnsafePtr();
			byte* src = (byte*)UnsafeUtilityExtensions.AddressOf<FixedBytes30>(other.bytes);
			ushort srcLength = other.utf8LengthInBytes;
			FormatError formatError = UTF8ArrayUnsafeUtility.AppendUTF8Bytes(unsafePtr, ref length, 4093, src, (int)srcLength);
			if (formatError != FormatError.None)
			{
				return (int)formatError;
			}
			this.Length = length;
			return 0;
		}

		// Token: 0x06000491 RID: 1169 RVA: 0x0000B474 File Offset: 0x00009674
		public unsafe static bool operator ==(in FixedString4096Bytes a, in FixedString32Bytes b)
		{
			int aLength = (int)a.utf8LengthInBytes;
			int bLength = (int)b.utf8LengthInBytes;
			byte* aBytes = (byte*)UnsafeUtilityExtensions.AddressOf<FixedBytes4094>(a.bytes);
			byte* bBytes = (byte*)UnsafeUtilityExtensions.AddressOf<FixedBytes30>(b.bytes);
			return UTF8ArrayUnsafeUtility.EqualsUTF8Bytes(aBytes, aLength, bBytes, bLength);
		}

		// Token: 0x06000492 RID: 1170 RVA: 0x0000B4B0 File Offset: 0x000096B0
		public static bool operator !=(in FixedString4096Bytes a, in FixedString32Bytes b)
		{
			return !(a == b);
		}

		// Token: 0x06000493 RID: 1171 RVA: 0x0000B4BC File Offset: 0x000096BC
		public bool Equals(FixedString32Bytes other)
		{
			return this == other;
		}

		// Token: 0x06000494 RID: 1172 RVA: 0x0000B4C6 File Offset: 0x000096C6
		public int CompareTo(FixedString64Bytes other)
		{
			return ref this.CompareTo(other);
		}

		// Token: 0x06000495 RID: 1173 RVA: 0x0000B4D0 File Offset: 0x000096D0
		public FixedString4096Bytes(in FixedString64Bytes other)
		{
			this = default(FixedString4096Bytes);
			this.Initialize(other);
		}

		// Token: 0x06000496 RID: 1174 RVA: 0x0000B4E4 File Offset: 0x000096E4
		internal unsafe int Initialize(in FixedString64Bytes other)
		{
			this.bytes = default(FixedBytes4094);
			this.utf8LengthInBytes = 0;
			int length = 0;
			byte* unsafePtr = this.GetUnsafePtr();
			byte* src = (byte*)UnsafeUtilityExtensions.AddressOf<FixedBytes62>(other.bytes);
			ushort srcLength = other.utf8LengthInBytes;
			FormatError formatError = UTF8ArrayUnsafeUtility.AppendUTF8Bytes(unsafePtr, ref length, 4093, src, (int)srcLength);
			if (formatError != FormatError.None)
			{
				return (int)formatError;
			}
			this.Length = length;
			return 0;
		}

		// Token: 0x06000497 RID: 1175 RVA: 0x0000B53C File Offset: 0x0000973C
		public unsafe static bool operator ==(in FixedString4096Bytes a, in FixedString64Bytes b)
		{
			int aLength = (int)a.utf8LengthInBytes;
			int bLength = (int)b.utf8LengthInBytes;
			byte* aBytes = (byte*)UnsafeUtilityExtensions.AddressOf<FixedBytes4094>(a.bytes);
			byte* bBytes = (byte*)UnsafeUtilityExtensions.AddressOf<FixedBytes62>(b.bytes);
			return UTF8ArrayUnsafeUtility.EqualsUTF8Bytes(aBytes, aLength, bBytes, bLength);
		}

		// Token: 0x06000498 RID: 1176 RVA: 0x0000B578 File Offset: 0x00009778
		public static bool operator !=(in FixedString4096Bytes a, in FixedString64Bytes b)
		{
			return !(a == b);
		}

		// Token: 0x06000499 RID: 1177 RVA: 0x0000B584 File Offset: 0x00009784
		public bool Equals(FixedString64Bytes other)
		{
			return this == other;
		}

		// Token: 0x0600049A RID: 1178 RVA: 0x0000B58E File Offset: 0x0000978E
		public int CompareTo(FixedString128Bytes other)
		{
			return ref this.CompareTo(other);
		}

		// Token: 0x0600049B RID: 1179 RVA: 0x0000B598 File Offset: 0x00009798
		public FixedString4096Bytes(in FixedString128Bytes other)
		{
			this = default(FixedString4096Bytes);
			this.Initialize(other);
		}

		// Token: 0x0600049C RID: 1180 RVA: 0x0000B5AC File Offset: 0x000097AC
		internal unsafe int Initialize(in FixedString128Bytes other)
		{
			this.bytes = default(FixedBytes4094);
			this.utf8LengthInBytes = 0;
			int length = 0;
			byte* unsafePtr = this.GetUnsafePtr();
			byte* src = (byte*)UnsafeUtilityExtensions.AddressOf<FixedBytes126>(other.bytes);
			ushort srcLength = other.utf8LengthInBytes;
			FormatError formatError = UTF8ArrayUnsafeUtility.AppendUTF8Bytes(unsafePtr, ref length, 4093, src, (int)srcLength);
			if (formatError != FormatError.None)
			{
				return (int)formatError;
			}
			this.Length = length;
			return 0;
		}

		// Token: 0x0600049D RID: 1181 RVA: 0x0000B604 File Offset: 0x00009804
		public unsafe static bool operator ==(in FixedString4096Bytes a, in FixedString128Bytes b)
		{
			int aLength = (int)a.utf8LengthInBytes;
			int bLength = (int)b.utf8LengthInBytes;
			byte* aBytes = (byte*)UnsafeUtilityExtensions.AddressOf<FixedBytes4094>(a.bytes);
			byte* bBytes = (byte*)UnsafeUtilityExtensions.AddressOf<FixedBytes126>(b.bytes);
			return UTF8ArrayUnsafeUtility.EqualsUTF8Bytes(aBytes, aLength, bBytes, bLength);
		}

		// Token: 0x0600049E RID: 1182 RVA: 0x0000B640 File Offset: 0x00009840
		public static bool operator !=(in FixedString4096Bytes a, in FixedString128Bytes b)
		{
			return !(a == b);
		}

		// Token: 0x0600049F RID: 1183 RVA: 0x0000B64C File Offset: 0x0000984C
		public bool Equals(FixedString128Bytes other)
		{
			return this == other;
		}

		// Token: 0x060004A0 RID: 1184 RVA: 0x0000B656 File Offset: 0x00009856
		public int CompareTo(FixedString512Bytes other)
		{
			return ref this.CompareTo(other);
		}

		// Token: 0x060004A1 RID: 1185 RVA: 0x0000B660 File Offset: 0x00009860
		public FixedString4096Bytes(in FixedString512Bytes other)
		{
			this = default(FixedString4096Bytes);
			this.Initialize(other);
		}

		// Token: 0x060004A2 RID: 1186 RVA: 0x0000B674 File Offset: 0x00009874
		internal unsafe int Initialize(in FixedString512Bytes other)
		{
			this.bytes = default(FixedBytes4094);
			this.utf8LengthInBytes = 0;
			int length = 0;
			byte* unsafePtr = this.GetUnsafePtr();
			byte* src = (byte*)UnsafeUtilityExtensions.AddressOf<FixedBytes510>(other.bytes);
			ushort srcLength = other.utf8LengthInBytes;
			FormatError formatError = UTF8ArrayUnsafeUtility.AppendUTF8Bytes(unsafePtr, ref length, 4093, src, (int)srcLength);
			if (formatError != FormatError.None)
			{
				return (int)formatError;
			}
			this.Length = length;
			return 0;
		}

		// Token: 0x060004A3 RID: 1187 RVA: 0x0000B6CC File Offset: 0x000098CC
		public unsafe static bool operator ==(in FixedString4096Bytes a, in FixedString512Bytes b)
		{
			int aLength = (int)a.utf8LengthInBytes;
			int bLength = (int)b.utf8LengthInBytes;
			byte* aBytes = (byte*)UnsafeUtilityExtensions.AddressOf<FixedBytes4094>(a.bytes);
			byte* bBytes = (byte*)UnsafeUtilityExtensions.AddressOf<FixedBytes510>(b.bytes);
			return UTF8ArrayUnsafeUtility.EqualsUTF8Bytes(aBytes, aLength, bBytes, bLength);
		}

		// Token: 0x060004A4 RID: 1188 RVA: 0x0000B708 File Offset: 0x00009908
		public static bool operator !=(in FixedString4096Bytes a, in FixedString512Bytes b)
		{
			return !(a == b);
		}

		// Token: 0x060004A5 RID: 1189 RVA: 0x0000B714 File Offset: 0x00009914
		public bool Equals(FixedString512Bytes other)
		{
			return this == other;
		}

		// Token: 0x060004A6 RID: 1190 RVA: 0x0000B71E File Offset: 0x0000991E
		public int CompareTo(FixedString4096Bytes other)
		{
			return ref this.CompareTo(other);
		}

		// Token: 0x060004A7 RID: 1191 RVA: 0x0000B728 File Offset: 0x00009928
		public FixedString4096Bytes(in FixedString4096Bytes other)
		{
			this = default(FixedString4096Bytes);
			this.Initialize(other);
		}

		// Token: 0x060004A8 RID: 1192 RVA: 0x0000B73C File Offset: 0x0000993C
		internal unsafe int Initialize(in FixedString4096Bytes other)
		{
			this.bytes = default(FixedBytes4094);
			this.utf8LengthInBytes = 0;
			int length = 0;
			byte* unsafePtr = this.GetUnsafePtr();
			byte* src = (byte*)UnsafeUtilityExtensions.AddressOf<FixedBytes4094>(other.bytes);
			ushort srcLength = other.utf8LengthInBytes;
			FormatError formatError = UTF8ArrayUnsafeUtility.AppendUTF8Bytes(unsafePtr, ref length, 4093, src, (int)srcLength);
			if (formatError != FormatError.None)
			{
				return (int)formatError;
			}
			this.Length = length;
			return 0;
		}

		// Token: 0x060004A9 RID: 1193 RVA: 0x0000B794 File Offset: 0x00009994
		public unsafe static bool operator ==(in FixedString4096Bytes a, in FixedString4096Bytes b)
		{
			int aLength = (int)a.utf8LengthInBytes;
			int bLength = (int)b.utf8LengthInBytes;
			byte* aBytes = (byte*)UnsafeUtilityExtensions.AddressOf<FixedBytes4094>(a.bytes);
			byte* bBytes = (byte*)UnsafeUtilityExtensions.AddressOf<FixedBytes4094>(b.bytes);
			return UTF8ArrayUnsafeUtility.EqualsUTF8Bytes(aBytes, aLength, bBytes, bLength);
		}

		// Token: 0x060004AA RID: 1194 RVA: 0x0000B7D0 File Offset: 0x000099D0
		public static bool operator !=(in FixedString4096Bytes a, in FixedString4096Bytes b)
		{
			return !(a == b);
		}

		// Token: 0x060004AB RID: 1195 RVA: 0x0000B7DC File Offset: 0x000099DC
		public bool Equals(FixedString4096Bytes other)
		{
			return this == other;
		}

		// Token: 0x060004AC RID: 1196 RVA: 0x0000B7E6 File Offset: 0x000099E6
		[NotBurstCompatible]
		public static implicit operator FixedString4096Bytes(string b)
		{
			return new FixedString4096Bytes(b);
		}

		// Token: 0x060004AD RID: 1197 RVA: 0x0000B7EE File Offset: 0x000099EE
		[NotBurstCompatible]
		public override string ToString()
		{
			return ref this.ConvertToString<FixedString4096Bytes>();
		}

		// Token: 0x060004AE RID: 1198 RVA: 0x0000B7F6 File Offset: 0x000099F6
		public override int GetHashCode()
		{
			return ref this.ComputeHashCode<FixedString4096Bytes>();
		}

		// Token: 0x060004AF RID: 1199 RVA: 0x0000B800 File Offset: 0x00009A00
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

		// Token: 0x060004B0 RID: 1200 RVA: 0x0000B89C File Offset: 0x00009A9C
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		private void CheckIndexInRange(int index)
		{
			if (index < 0)
			{
				throw new IndexOutOfRangeException(string.Format("Index {0} must be positive.", index));
			}
			if (index >= (int)this.utf8LengthInBytes)
			{
				throw new IndexOutOfRangeException(string.Format("Index {0} is out of range in FixedString4096Bytes of '{1}' Length.", index, this.utf8LengthInBytes));
			}
		}

		// Token: 0x060004B1 RID: 1201 RVA: 0x0000B8F0 File Offset: 0x00009AF0
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		private void CheckLengthInRange(int length)
		{
			if (length < 0)
			{
				throw new ArgumentOutOfRangeException(string.Format("Length {0} must be positive.", length));
			}
			if (length > 4093)
			{
				throw new ArgumentOutOfRangeException(string.Format("Length {0} is out of range in FixedString4096Bytes of '{1}' Capacity.", length, 4093));
			}
		}

		// Token: 0x060004B2 RID: 1202 RVA: 0x0000B93F File Offset: 0x00009B3F
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		private void CheckCapacityInRange(int capacity)
		{
			if (capacity > 4093)
			{
				throw new ArgumentOutOfRangeException(string.Format("Capacity {0} must be lower than {1}.", capacity, 4093));
			}
		}

		// Token: 0x060004B3 RID: 1203 RVA: 0x0000B969 File Offset: 0x00009B69
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		private static void CheckCopyError(CopyError error, string source)
		{
			if (error != CopyError.None)
			{
				throw new ArgumentException(string.Format("FixedString4096Bytes: {0} while copying \"{1}\"", error, source));
			}
		}

		// Token: 0x060004B4 RID: 1204 RVA: 0x000098DF File Offset: 0x00007ADF
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		private static void CheckFormatError(FormatError error)
		{
			if (error != FormatError.None)
			{
				throw new ArgumentException("Source is too long to fit into fixed string of this size");
			}
		}

		// Token: 0x04000270 RID: 624
		internal const ushort utf8MaxLengthInBytes = 4093;

		// Token: 0x04000271 RID: 625
		[SerializeField]
		internal ushort utf8LengthInBytes;

		// Token: 0x04000272 RID: 626
		[SerializeField]
		internal FixedBytes4094 bytes;

		// Token: 0x020000A0 RID: 160
		public struct Enumerator : IEnumerator
		{
			// Token: 0x060004B5 RID: 1205 RVA: 0x0000B985 File Offset: 0x00009B85
			public Enumerator(FixedString4096Bytes other)
			{
				this.target = other;
				this.offset = 0;
				this.current = default(Unicode.Rune);
			}

			// Token: 0x060004B6 RID: 1206 RVA: 0x00002C2B File Offset: 0x00000E2B
			public void Dispose()
			{
			}

			// Token: 0x060004B7 RID: 1207 RVA: 0x0000B9A1 File Offset: 0x00009BA1
			public bool MoveNext()
			{
				if (this.offset >= this.target.Length)
				{
					return false;
				}
				Unicode.Utf8ToUcs(out this.current, this.target.GetUnsafePtr(), ref this.offset, this.target.Length);
				return true;
			}

			// Token: 0x060004B8 RID: 1208 RVA: 0x0000B9E1 File Offset: 0x00009BE1
			public void Reset()
			{
				this.offset = 0;
				this.current = default(Unicode.Rune);
			}

			// Token: 0x170000A6 RID: 166
			// (get) Token: 0x060004B9 RID: 1209 RVA: 0x0000B9F6 File Offset: 0x00009BF6
			public Unicode.Rune Current
			{
				get
				{
					return this.current;
				}
			}

			// Token: 0x170000A7 RID: 167
			// (get) Token: 0x060004BA RID: 1210 RVA: 0x0000B9FE File Offset: 0x00009BFE
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x04000273 RID: 627
			private FixedString4096Bytes target;

			// Token: 0x04000274 RID: 628
			private int offset;

			// Token: 0x04000275 RID: 629
			private Unicode.Rune current;
		}
	}
}

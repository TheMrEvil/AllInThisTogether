using System;
using Unity.Collections.LowLevel.Unsafe;

namespace Unity.Collections
{
	// Token: 0x020000F0 RID: 240
	[BurstCompatible]
	public static class UTF8ArrayUnsafeUtility
	{
		// Token: 0x060008F1 RID: 2289 RVA: 0x0001ABA0 File Offset: 0x00018DA0
		public unsafe static CopyError Copy(byte* dest, out int destLength, int destUTF8MaxLengthInBytes, char* src, int srcLength)
		{
			if (Unicode.Utf16ToUtf8(src, srcLength, dest, out destLength, destUTF8MaxLengthInBytes) == ConversionError.None)
			{
				return CopyError.None;
			}
			return CopyError.Truncation;
		}

		// Token: 0x060008F2 RID: 2290 RVA: 0x0001ABB4 File Offset: 0x00018DB4
		public unsafe static CopyError Copy(byte* dest, out ushort destLength, ushort destUTF8MaxLengthInBytes, char* src, int srcLength)
		{
			int num;
			bool flag = Unicode.Utf16ToUtf8(src, srcLength, dest, out num, (int)destUTF8MaxLengthInBytes) != ConversionError.None;
			destLength = (ushort)num;
			if (!flag)
			{
				return CopyError.None;
			}
			return CopyError.Truncation;
		}

		// Token: 0x060008F3 RID: 2291 RVA: 0x0001ABD8 File Offset: 0x00018DD8
		public unsafe static CopyError Copy(byte* dest, out int destLength, int destUTF8MaxLengthInBytes, byte* src, int srcLength)
		{
			int num;
			bool flag = Unicode.Utf8ToUtf8(src, srcLength, dest, out num, destUTF8MaxLengthInBytes) != ConversionError.None;
			destLength = num;
			if (!flag)
			{
				return CopyError.None;
			}
			return CopyError.Truncation;
		}

		// Token: 0x060008F4 RID: 2292 RVA: 0x0001ABFC File Offset: 0x00018DFC
		public unsafe static CopyError Copy(byte* dest, out ushort destLength, ushort destUTF8MaxLengthInBytes, byte* src, ushort srcLength)
		{
			int num;
			bool flag = Unicode.Utf8ToUtf8(src, (int)srcLength, dest, out num, (int)destUTF8MaxLengthInBytes) != ConversionError.None;
			destLength = (ushort)num;
			if (!flag)
			{
				return CopyError.None;
			}
			return CopyError.Truncation;
		}

		// Token: 0x060008F5 RID: 2293 RVA: 0x0001AC1E File Offset: 0x00018E1E
		public unsafe static CopyError Copy(char* dest, out int destLength, int destUCS2MaxLengthInChars, byte* src, int srcLength)
		{
			if (Unicode.Utf8ToUtf16(src, srcLength, dest, out destLength, destUCS2MaxLengthInChars) == ConversionError.None)
			{
				return CopyError.None;
			}
			return CopyError.Truncation;
		}

		// Token: 0x060008F6 RID: 2294 RVA: 0x0001AC30 File Offset: 0x00018E30
		public unsafe static CopyError Copy(char* dest, out ushort destLength, ushort destUCS2MaxLengthInChars, byte* src, ushort srcLength)
		{
			int num;
			bool flag = Unicode.Utf8ToUtf16(src, (int)srcLength, dest, out num, (int)destUCS2MaxLengthInChars) != ConversionError.None;
			destLength = (ushort)num;
			if (!flag)
			{
				return CopyError.None;
			}
			return CopyError.Truncation;
		}

		// Token: 0x060008F7 RID: 2295 RVA: 0x0001AC52 File Offset: 0x00018E52
		public unsafe static FormatError AppendUTF8Bytes(byte* dest, ref int destLength, int destCapacity, byte* src, int srcLength)
		{
			if (destLength + srcLength > destCapacity)
			{
				return FormatError.Overflow;
			}
			UnsafeUtility.MemCpy((void*)(dest + destLength), (void*)src, (long)srcLength);
			destLength += srcLength;
			return FormatError.None;
		}

		// Token: 0x060008F8 RID: 2296 RVA: 0x0001AC74 File Offset: 0x00018E74
		public unsafe static CopyError Append(byte* dest, ref ushort destLength, ushort destUTF8MaxLengthInBytes, byte* src, ushort srcLength)
		{
			int num;
			bool flag = Unicode.Utf8ToUtf8(src, (int)srcLength, dest + destLength, out num, (int)(destUTF8MaxLengthInBytes - destLength)) != ConversionError.None;
			destLength += (ushort)num;
			if (!flag)
			{
				return CopyError.None;
			}
			return CopyError.Truncation;
		}

		// Token: 0x060008F9 RID: 2297 RVA: 0x0001ACA0 File Offset: 0x00018EA0
		public unsafe static CopyError Append(byte* dest, ref ushort destLength, ushort destUTF8MaxLengthInBytes, char* src, int srcLength)
		{
			int num;
			bool flag = Unicode.Utf16ToUtf8(src, srcLength, dest + destLength, out num, (int)(destUTF8MaxLengthInBytes - destLength)) != ConversionError.None;
			destLength += (ushort)num;
			if (!flag)
			{
				return CopyError.None;
			}
			return CopyError.Truncation;
		}

		// Token: 0x060008FA RID: 2298 RVA: 0x0001ACCC File Offset: 0x00018ECC
		public unsafe static CopyError Append(char* dest, ref ushort destLength, ushort destUCS2MaxLengthInChars, byte* src, ushort srcLength)
		{
			int num;
			bool flag = Unicode.Utf8ToUtf16(src, (int)srcLength, dest + destLength, out num, (int)(destUCS2MaxLengthInChars - destLength)) != ConversionError.None;
			destLength += (ushort)num;
			if (!flag)
			{
				return CopyError.None;
			}
			return CopyError.Truncation;
		}

		// Token: 0x060008FB RID: 2299 RVA: 0x0001ACFC File Offset: 0x00018EFC
		public unsafe static int StrCmp(byte* utf8BufferA, int utf8LengthInBytesA, byte* utf8BufferB, int utf8LengthInBytesB)
		{
			int num = 0;
			int num2 = 0;
			UTF8ArrayUnsafeUtility.Comparison comparison;
			do
			{
				Unicode.Rune runeA;
				ConversionError errorA = Unicode.Utf8ToUcs(out runeA, utf8BufferA, ref num, utf8LengthInBytesA);
				Unicode.Rune runeB;
				ConversionError errorB = Unicode.Utf8ToUcs(out runeB, utf8BufferB, ref num2, utf8LengthInBytesB);
				comparison = new UTF8ArrayUnsafeUtility.Comparison(runeA, errorA, runeB, errorB);
			}
			while (!comparison.terminates);
			return comparison.result;
		}

		// Token: 0x060008FC RID: 2300 RVA: 0x0001AD44 File Offset: 0x00018F44
		public unsafe static int StrCmp(char* utf16BufferA, int utf16LengthInCharsA, char* utf16BufferB, int utf16LengthInCharsB)
		{
			int num = 0;
			int num2 = 0;
			UTF8ArrayUnsafeUtility.Comparison comparison;
			do
			{
				Unicode.Rune runeA;
				ConversionError errorA = Unicode.Utf16ToUcs(out runeA, utf16BufferA, ref num, utf16LengthInCharsA);
				Unicode.Rune runeB;
				ConversionError errorB = Unicode.Utf16ToUcs(out runeB, utf16BufferB, ref num2, utf16LengthInCharsB);
				comparison = new UTF8ArrayUnsafeUtility.Comparison(runeA, errorA, runeB, errorB);
			}
			while (!comparison.terminates);
			return comparison.result;
		}

		// Token: 0x060008FD RID: 2301 RVA: 0x0001AD8B File Offset: 0x00018F8B
		public unsafe static bool EqualsUTF8Bytes(byte* aBytes, int aLength, byte* bBytes, int bLength)
		{
			return UTF8ArrayUnsafeUtility.StrCmp(aBytes, aLength, bBytes, bLength) == 0;
		}

		// Token: 0x060008FE RID: 2302 RVA: 0x0001AD9C File Offset: 0x00018F9C
		public unsafe static int StrCmp(byte* utf8Buffer, int utf8LengthInBytes, char* utf16Buffer, int utf16LengthInChars)
		{
			int num = 0;
			int num2 = 0;
			UTF8ArrayUnsafeUtility.Comparison comparison;
			do
			{
				Unicode.Rune runeA;
				ConversionError errorA = Unicode.Utf8ToUcs(out runeA, utf8Buffer, ref num, utf8LengthInBytes);
				Unicode.Rune runeB;
				ConversionError errorB = Unicode.Utf16ToUcs(out runeB, utf16Buffer, ref num2, utf16LengthInChars);
				comparison = new UTF8ArrayUnsafeUtility.Comparison(runeA, errorA, runeB, errorB);
			}
			while (!comparison.terminates);
			return comparison.result;
		}

		// Token: 0x060008FF RID: 2303 RVA: 0x0001ADE3 File Offset: 0x00018FE3
		public unsafe static int StrCmp(char* utf16Buffer, int utf16LengthInChars, byte* utf8Buffer, int utf8LengthInBytes)
		{
			return -UTF8ArrayUnsafeUtility.StrCmp(utf8Buffer, utf8LengthInBytes, utf16Buffer, utf16LengthInChars);
		}

		// Token: 0x020000F1 RID: 241
		internal struct Comparison
		{
			// Token: 0x06000900 RID: 2304 RVA: 0x0001ADF0 File Offset: 0x00018FF0
			public Comparison(Unicode.Rune runeA, ConversionError errorA, Unicode.Rune runeB, ConversionError errorB)
			{
				if (errorA != ConversionError.None)
				{
					runeA.value = 0;
				}
				if (errorB != ConversionError.None)
				{
					runeB.value = 0;
				}
				if (runeA.value != runeB.value)
				{
					this.result = runeA.value - runeB.value;
					this.terminates = true;
					return;
				}
				this.result = 0;
				this.terminates = (runeA.value == 0 && runeB.value == 0);
			}

			// Token: 0x040002FC RID: 764
			public bool terminates;

			// Token: 0x040002FD RID: 765
			public int result;
		}
	}
}

using System;
using System.Buffers.Binary;
using System.Runtime.CompilerServices;
using System.Text;

namespace System.Globalization
{
	/// <summary>Retrieves information about a Unicode character. This class cannot be inherited.</summary>
	// Token: 0x02000954 RID: 2388
	public static class CharUnicodeInfo
	{
		// Token: 0x0600540F RID: 21519 RVA: 0x00118CA0 File Offset: 0x00116EA0
		internal static int InternalConvertToUtf32(string s, int index)
		{
			if (index < s.Length - 1)
			{
				int num = (int)(s[index] - '\ud800');
				if (num <= 1023)
				{
					int num2 = (int)(s[index + 1] - '\udc00');
					if (num2 <= 1023)
					{
						return num * 1024 + num2 + 65536;
					}
				}
			}
			return (int)s[index];
		}

		// Token: 0x06005410 RID: 21520 RVA: 0x00118D00 File Offset: 0x00116F00
		internal static int InternalConvertToUtf32(StringBuilder s, int index)
		{
			int num = (int)s[index];
			if (index < s.Length - 1)
			{
				int num2 = num - 55296;
				if (num2 <= 1023)
				{
					int num3 = (int)(s[index + 1] - '\udc00');
					if (num3 <= 1023)
					{
						return num2 * 1024 + num3 + 65536;
					}
				}
			}
			return num;
		}

		// Token: 0x06005411 RID: 21521 RVA: 0x00118D5C File Offset: 0x00116F5C
		internal static int InternalConvertToUtf32(string s, int index, out int charLength)
		{
			charLength = 1;
			if (index < s.Length - 1)
			{
				int num = (int)(s[index] - '\ud800');
				if (num <= 1023)
				{
					int num2 = (int)(s[index + 1] - '\udc00');
					if (num2 <= 1023)
					{
						charLength++;
						return num * 1024 + num2 + 65536;
					}
				}
			}
			return (int)s[index];
		}

		// Token: 0x06005412 RID: 21522 RVA: 0x00118DC4 File Offset: 0x00116FC4
		internal unsafe static double InternalGetNumericValue(int ch)
		{
			int num = ch >> 8;
			if (num >= CharUnicodeInfo.NumericLevel1Index.Length)
			{
				return -1.0;
			}
			num = (int)(*CharUnicodeInfo.NumericLevel1Index[num]);
			num = (int)(*CharUnicodeInfo.NumericLevel2Index[(num << 4) + (ch >> 4 & 15)]);
			num = (int)(*CharUnicodeInfo.NumericLevel3Index[(num << 4) + (ch & 15)]);
			ref byte source = ref Unsafe.AsRef<byte>(CharUnicodeInfo.NumericValues[num * 8]);
			if (BitConverter.IsLittleEndian)
			{
				return Unsafe.ReadUnaligned<double>(ref source);
			}
			return BitConverter.Int64BitsToDouble(BinaryPrimitives.ReverseEndianness(Unsafe.ReadUnaligned<long>(ref source)));
		}

		// Token: 0x06005413 RID: 21523 RVA: 0x00118E64 File Offset: 0x00117064
		internal unsafe static byte InternalGetDigitValues(int ch, int offset)
		{
			int num = ch >> 8;
			if (num < CharUnicodeInfo.NumericLevel1Index.Length)
			{
				num = (int)(*CharUnicodeInfo.NumericLevel1Index[num]);
				num = (int)(*CharUnicodeInfo.NumericLevel2Index[(num << 4) + (ch >> 4 & 15)]);
				num = (int)(*CharUnicodeInfo.NumericLevel3Index[(num << 4) + (ch & 15)]);
				return *CharUnicodeInfo.DigitValues[num * 2 + offset];
			}
			return byte.MaxValue;
		}

		/// <summary>Gets the numeric value associated with the specified character.</summary>
		/// <param name="ch">The Unicode character for which to get the numeric value.</param>
		/// <returns>The numeric value associated with the specified character.  
		///  -or-  
		///  -1, if the specified character is not a numeric character.</returns>
		// Token: 0x06005414 RID: 21524 RVA: 0x00118EDE File Offset: 0x001170DE
		public static double GetNumericValue(char ch)
		{
			return CharUnicodeInfo.InternalGetNumericValue((int)ch);
		}

		/// <summary>Gets the numeric value associated with the character at the specified index of the specified string.</summary>
		/// <param name="s">The <see cref="T:System.String" /> containing the Unicode character for which to get the numeric value.</param>
		/// <param name="index">The index of the Unicode character for which to get the numeric value.</param>
		/// <returns>The numeric value associated with the character at the specified index of the specified string.  
		///  -or-  
		///  -1, if the character at the specified index of the specified string is not a numeric character.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="s" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is outside the range of valid indexes in <paramref name="s" />.</exception>
		// Token: 0x06005415 RID: 21525 RVA: 0x00118EE6 File Offset: 0x001170E6
		public static double GetNumericValue(string s, int index)
		{
			if (s == null)
			{
				throw new ArgumentNullException("s");
			}
			if (index < 0 || index >= s.Length)
			{
				throw new ArgumentOutOfRangeException("index", "Index was out of range. Must be non-negative and less than the size of the collection.");
			}
			return CharUnicodeInfo.InternalGetNumericValue(CharUnicodeInfo.InternalConvertToUtf32(s, index));
		}

		/// <summary>Gets the decimal digit value of the specified numeric character.</summary>
		/// <param name="ch">The Unicode character for which to get the decimal digit value.</param>
		/// <returns>The decimal digit value of the specified numeric character.  
		///  -or-  
		///  -1, if the specified character is not a decimal digit.</returns>
		// Token: 0x06005416 RID: 21526 RVA: 0x00118F1F File Offset: 0x0011711F
		public static int GetDecimalDigitValue(char ch)
		{
			return (int)((sbyte)CharUnicodeInfo.InternalGetDigitValues((int)ch, 0));
		}

		/// <summary>Gets the decimal digit value of the numeric character at the specified index of the specified string.</summary>
		/// <param name="s">The <see cref="T:System.String" /> containing the Unicode character for which to get the decimal digit value.</param>
		/// <param name="index">The index of the Unicode character for which to get the decimal digit value.</param>
		/// <returns>The decimal digit value of the numeric character at the specified index of the specified string.  
		///  -or-  
		///  -1, if the character at the specified index of the specified string is not a decimal digit.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="s" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is outside the range of valid indexes in <paramref name="s" />.</exception>
		// Token: 0x06005417 RID: 21527 RVA: 0x00118F29 File Offset: 0x00117129
		public static int GetDecimalDigitValue(string s, int index)
		{
			if (s == null)
			{
				throw new ArgumentNullException("s");
			}
			if (index < 0 || index >= s.Length)
			{
				throw new ArgumentOutOfRangeException("index", "Index was out of range. Must be non-negative and less than the size of the collection.");
			}
			return (int)((sbyte)CharUnicodeInfo.InternalGetDigitValues(CharUnicodeInfo.InternalConvertToUtf32(s, index), 0));
		}

		/// <summary>Gets the digit value of the specified numeric character.</summary>
		/// <param name="ch">The Unicode character for which to get the digit value.</param>
		/// <returns>The digit value of the specified numeric character.  
		///  -or-  
		///  -1, if the specified character is not a digit.</returns>
		// Token: 0x06005418 RID: 21528 RVA: 0x00118F64 File Offset: 0x00117164
		public static int GetDigitValue(char ch)
		{
			return (int)((sbyte)CharUnicodeInfo.InternalGetDigitValues((int)ch, 1));
		}

		/// <summary>Gets the digit value of the numeric character at the specified index of the specified string.</summary>
		/// <param name="s">The <see cref="T:System.String" /> containing the Unicode character for which to get the digit value.</param>
		/// <param name="index">The index of the Unicode character for which to get the digit value.</param>
		/// <returns>The digit value of the numeric character at the specified index of the specified string.  
		///  -or-  
		///  -1, if the character at the specified index of the specified string is not a digit.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="s" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is outside the range of valid indexes in <paramref name="s" />.</exception>
		// Token: 0x06005419 RID: 21529 RVA: 0x00118F6E File Offset: 0x0011716E
		public static int GetDigitValue(string s, int index)
		{
			if (s == null)
			{
				throw new ArgumentNullException("s");
			}
			if (index < 0 || index >= s.Length)
			{
				throw new ArgumentOutOfRangeException("index", "Index was out of range. Must be non-negative and less than the size of the collection.");
			}
			return (int)((sbyte)CharUnicodeInfo.InternalGetDigitValues(CharUnicodeInfo.InternalConvertToUtf32(s, index), 1));
		}

		/// <summary>Gets the Unicode category of the specified character.</summary>
		/// <param name="ch">The Unicode character for which to get the Unicode category.</param>
		/// <returns>A <see cref="T:System.Globalization.UnicodeCategory" /> value indicating the category of the specified character.</returns>
		// Token: 0x0600541A RID: 21530 RVA: 0x00118FA9 File Offset: 0x001171A9
		public static UnicodeCategory GetUnicodeCategory(char ch)
		{
			return CharUnicodeInfo.GetUnicodeCategory((int)ch);
		}

		/// <summary>Gets the Unicode category of the character at the specified index of the specified string.</summary>
		/// <param name="s">The <see cref="T:System.String" /> containing the Unicode character for which to get the Unicode category.</param>
		/// <param name="index">The index of the Unicode character for which to get the Unicode category.</param>
		/// <returns>A <see cref="T:System.Globalization.UnicodeCategory" /> value indicating the category of the character at the specified index of the specified string.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="s" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is outside the range of valid indexes in <paramref name="s" />.</exception>
		// Token: 0x0600541B RID: 21531 RVA: 0x00118FB1 File Offset: 0x001171B1
		public static UnicodeCategory GetUnicodeCategory(string s, int index)
		{
			if (s == null)
			{
				throw new ArgumentNullException("s");
			}
			if (index >= s.Length)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			return CharUnicodeInfo.InternalGetUnicodeCategory(s, index);
		}

		// Token: 0x0600541C RID: 21532 RVA: 0x00118FDC File Offset: 0x001171DC
		public static UnicodeCategory GetUnicodeCategory(int codePoint)
		{
			return (UnicodeCategory)CharUnicodeInfo.InternalGetCategoryValue(codePoint, 0);
		}

		// Token: 0x0600541D RID: 21533 RVA: 0x00118FE8 File Offset: 0x001171E8
		internal unsafe static byte InternalGetCategoryValue(int ch, int offset)
		{
			int num = (int)(*CharUnicodeInfo.CategoryLevel1Index[ch >> 9]);
			num = (int)Unsafe.ReadUnaligned<ushort>(Unsafe.AsRef<byte>(CharUnicodeInfo.CategoryLevel2Index[(num << 6) + (ch >> 3 & 62)]));
			if (!BitConverter.IsLittleEndian)
			{
				num = (int)BinaryPrimitives.ReverseEndianness((ushort)num);
			}
			num = (int)(*CharUnicodeInfo.CategoryLevel3Index[(num << 4) + (ch & 15)]);
			return *CharUnicodeInfo.CategoriesValue[num * 2 + offset];
		}

		// Token: 0x0600541E RID: 21534 RVA: 0x00119063 File Offset: 0x00117263
		internal static UnicodeCategory InternalGetUnicodeCategory(string value, int index)
		{
			return CharUnicodeInfo.GetUnicodeCategory(CharUnicodeInfo.InternalConvertToUtf32(value, index));
		}

		// Token: 0x0600541F RID: 21535 RVA: 0x00119071 File Offset: 0x00117271
		internal static BidiCategory GetBidiCategory(string s, int index)
		{
			if (s == null)
			{
				throw new ArgumentNullException("s");
			}
			if (index >= s.Length)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			return (BidiCategory)CharUnicodeInfo.InternalGetCategoryValue(CharUnicodeInfo.InternalConvertToUtf32(s, index), 1);
		}

		// Token: 0x06005420 RID: 21536 RVA: 0x001190A2 File Offset: 0x001172A2
		internal static BidiCategory GetBidiCategory(StringBuilder s, int index)
		{
			return (BidiCategory)CharUnicodeInfo.InternalGetCategoryValue(CharUnicodeInfo.InternalConvertToUtf32(s, index), 1);
		}

		// Token: 0x06005421 RID: 21537 RVA: 0x001190B1 File Offset: 0x001172B1
		internal static UnicodeCategory InternalGetUnicodeCategory(string str, int index, out int charLength)
		{
			return CharUnicodeInfo.GetUnicodeCategory(CharUnicodeInfo.InternalConvertToUtf32(str, index, out charLength));
		}

		// Token: 0x06005422 RID: 21538 RVA: 0x001190C0 File Offset: 0x001172C0
		internal static bool IsCombiningCategory(UnicodeCategory uc)
		{
			return uc == UnicodeCategory.NonSpacingMark || uc == UnicodeCategory.SpacingCombiningMark || uc == UnicodeCategory.EnclosingMark;
		}

		// Token: 0x06005423 RID: 21539 RVA: 0x001190D0 File Offset: 0x001172D0
		internal static bool IsWhiteSpace(string s, int index)
		{
			UnicodeCategory unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(s, index);
			return unicodeCategory - UnicodeCategory.SpaceSeparator <= 2;
		}

		// Token: 0x06005424 RID: 21540 RVA: 0x001190F0 File Offset: 0x001172F0
		internal static bool IsWhiteSpace(char c)
		{
			UnicodeCategory unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
			return unicodeCategory - UnicodeCategory.SpaceSeparator <= 2;
		}

		// Token: 0x17000DDD RID: 3549
		// (get) Token: 0x06005425 RID: 21541 RVA: 0x0011910E File Offset: 0x0011730E
		private unsafe static ReadOnlySpan<byte> CategoryLevel1Index
		{
			get
			{
				return new ReadOnlySpan<byte>((void*)(&<PrivateImplementationDetails>.B55F94CD2F415D0279D7A1AF2265C4D9A90CE47F8C900D5D09AD088796210838), 2176);
			}
		}

		// Token: 0x17000DDE RID: 3550
		// (get) Token: 0x06005426 RID: 21542 RVA: 0x0011911F File Offset: 0x0011731F
		private unsafe static ReadOnlySpan<byte> CategoryLevel2Index
		{
			get
			{
				return new ReadOnlySpan<byte>((void*)(&<PrivateImplementationDetails>.9086502742CE7F0595B57A4E5B32901FF4CF97959B92F7E91A435E4765AC1115), 5952);
			}
		}

		// Token: 0x17000DDF RID: 3551
		// (get) Token: 0x06005427 RID: 21543 RVA: 0x00119130 File Offset: 0x00117330
		private unsafe static ReadOnlySpan<byte> CategoryLevel3Index
		{
			get
			{
				return new ReadOnlySpan<byte>((void*)(&<PrivateImplementationDetails>.56073E3CC3FC817690CC306D0DB7EA63EBCB0801359567CA44CA3D3B9BF63854), 10800);
			}
		}

		// Token: 0x17000DE0 RID: 3552
		// (get) Token: 0x06005428 RID: 21544 RVA: 0x00119141 File Offset: 0x00117341
		private unsafe static ReadOnlySpan<byte> CategoriesValue
		{
			get
			{
				return new ReadOnlySpan<byte>((void*)(&<PrivateImplementationDetails>.D6691EE5A533DE7E0859066942261B24D0C836D7EE016D2251377BFEE40FEA15), 172);
			}
		}

		// Token: 0x17000DE1 RID: 3553
		// (get) Token: 0x06005429 RID: 21545 RVA: 0x00119152 File Offset: 0x00117352
		private unsafe static ReadOnlySpan<byte> NumericLevel1Index
		{
			get
			{
				return new ReadOnlySpan<byte>((void*)(&<PrivateImplementationDetails>.765BD07ED3CB498A599FFB48B31E077C45B4C2C37CD1547CEA27E60655CF21B6), 761);
			}
		}

		// Token: 0x17000DE2 RID: 3554
		// (get) Token: 0x0600542A RID: 21546 RVA: 0x00119163 File Offset: 0x00117363
		private unsafe static ReadOnlySpan<byte> NumericLevel2Index
		{
			get
			{
				return new ReadOnlySpan<byte>((void*)(&<PrivateImplementationDetails>.F7D2AD02ED768134B31339AB059D864789E0A60090CC368B3881EB0631BBAF93), 1024);
			}
		}

		// Token: 0x17000DE3 RID: 3555
		// (get) Token: 0x0600542B RID: 21547 RVA: 0x00119174 File Offset: 0x00117374
		private unsafe static ReadOnlySpan<byte> NumericLevel3Index
		{
			get
			{
				return new ReadOnlySpan<byte>((void*)(&<PrivateImplementationDetails>.AB0B9733AAEC4A2806711E41E36D3D0923BAF116156F33445DC2AA58DA5DF877), 1824);
			}
		}

		// Token: 0x17000DE4 RID: 3556
		// (get) Token: 0x0600542C RID: 21548 RVA: 0x00119185 File Offset: 0x00117385
		private unsafe static ReadOnlySpan<byte> NumericValues
		{
			get
			{
				return new ReadOnlySpan<byte>((void*)(&<PrivateImplementationDetails>.692DE452EE427272A5F6154F04360D24165B56693B08F60D93127DEDC12D1DDE), 1320);
			}
		}

		// Token: 0x17000DE5 RID: 3557
		// (get) Token: 0x0600542D RID: 21549 RVA: 0x00119196 File Offset: 0x00117396
		private unsafe static ReadOnlySpan<byte> DigitValues
		{
			get
			{
				return new ReadOnlySpan<byte>((void*)(&<PrivateImplementationDetails>.1A52279427700E21F7E68A077A8F17857A850718317B7228442260DBA2AF68F0), 330);
			}
		}

		// Token: 0x040033B3 RID: 13235
		internal const char HIGH_SURROGATE_START = '\ud800';

		// Token: 0x040033B4 RID: 13236
		internal const char HIGH_SURROGATE_END = '\udbff';

		// Token: 0x040033B5 RID: 13237
		internal const char LOW_SURROGATE_START = '\udc00';

		// Token: 0x040033B6 RID: 13238
		internal const char LOW_SURROGATE_END = '\udfff';

		// Token: 0x040033B7 RID: 13239
		internal const int HIGH_SURROGATE_RANGE = 1023;

		// Token: 0x040033B8 RID: 13240
		internal const int UNICODE_CATEGORY_OFFSET = 0;

		// Token: 0x040033B9 RID: 13241
		internal const int BIDI_CATEGORY_OFFSET = 1;

		// Token: 0x040033BA RID: 13242
		internal const int UNICODE_PLANE01_START = 65536;
	}
}

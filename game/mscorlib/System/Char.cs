using System;
using System.Globalization;
using System.Runtime.Versioning;

namespace System
{
	/// <summary>Represents a character as a UTF-16 code unit.</summary>
	// Token: 0x02000105 RID: 261
	[Serializable]
	public readonly struct Char : IComparable, IComparable<char>, IEquatable<char>, IConvertible
	{
		// Token: 0x060007DD RID: 2013 RVA: 0x00022554 File Offset: 0x00020754
		private static bool IsLatin1(char ch)
		{
			return ch <= 'ÿ';
		}

		// Token: 0x060007DE RID: 2014 RVA: 0x00022561 File Offset: 0x00020761
		private static bool IsAscii(char ch)
		{
			return ch <= '\u007f';
		}

		// Token: 0x060007DF RID: 2015 RVA: 0x0002256B File Offset: 0x0002076B
		private static UnicodeCategory GetLatin1UnicodeCategory(char ch)
		{
			return (UnicodeCategory)char.s_categoryForLatin1[(int)ch];
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		// Token: 0x060007E0 RID: 2016 RVA: 0x00022574 File Offset: 0x00020774
		public override int GetHashCode()
		{
			return (int)(this | (int)this << 16);
		}

		/// <summary>Returns a value that indicates whether this instance is equal to a specified object.</summary>
		/// <param name="obj">An object to compare with this instance or <see langword="null" />.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="obj" /> is an instance of <see cref="T:System.Char" /> and equals the value of this instance; otherwise, <see langword="false" />.</returns>
		// Token: 0x060007E1 RID: 2017 RVA: 0x0002257E File Offset: 0x0002077E
		public override bool Equals(object obj)
		{
			return obj is char && this == (char)obj;
		}

		/// <summary>Returns a value that indicates whether this instance is equal to the specified <see cref="T:System.Char" /> object.</summary>
		/// <param name="obj">An object to compare to this instance.</param>
		/// <returns>
		///   <see langword="true" /> if the <paramref name="obj" /> parameter equals the value of this instance; otherwise, <see langword="false" />.</returns>
		// Token: 0x060007E2 RID: 2018 RVA: 0x00022594 File Offset: 0x00020794
		[NonVersionable]
		public bool Equals(char obj)
		{
			return this == obj;
		}

		/// <summary>Compares this instance to a specified object and indicates whether this instance precedes, follows, or appears in the same position in the sort order as the specified <see cref="T:System.Object" />.</summary>
		/// <param name="value">An object to compare this instance to, or <see langword="null" />.</param>
		/// <returns>A signed number indicating the position of this instance in the sort order in relation to the <paramref name="value" /> parameter.  
		///   Return Value  
		///
		///   Description  
		///
		///   Less than zero  
		///
		///   This instance precedes <paramref name="value" />.  
		///
		///   Zero  
		///
		///   This instance has the same position in the sort order as <paramref name="value" />.  
		///
		///   Greater than zero  
		///
		///   This instance follows <paramref name="value" />.  
		///
		///  -or-  
		///
		///  <paramref name="value" /> is <see langword="null" />.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="value" /> is not a <see cref="T:System.Char" /> object.</exception>
		// Token: 0x060007E3 RID: 2019 RVA: 0x0002259B File Offset: 0x0002079B
		public int CompareTo(object value)
		{
			if (value == null)
			{
				return 1;
			}
			if (!(value is char))
			{
				throw new ArgumentException("Object must be of type Char.");
			}
			return (int)(this - (char)value);
		}

		/// <summary>Compares this instance to a specified <see cref="T:System.Char" /> object and indicates whether this instance precedes, follows, or appears in the same position in the sort order as the specified <see cref="T:System.Char" /> object.</summary>
		/// <param name="value">A <see cref="T:System.Char" /> object to compare.</param>
		/// <returns>A signed number indicating the position of this instance in the sort order in relation to the <paramref name="value" /> parameter.  
		///   Return Value  
		///
		///   Description  
		///
		///   Less than zero  
		///
		///   This instance precedes <paramref name="value" />.  
		///
		///   Zero  
		///
		///   This instance has the same position in the sort order as <paramref name="value" />.  
		///
		///   Greater than zero  
		///
		///   This instance follows <paramref name="value" />.</returns>
		// Token: 0x060007E4 RID: 2020 RVA: 0x000225BE File Offset: 0x000207BE
		public int CompareTo(char value)
		{
			return (int)(this - value);
		}

		/// <summary>Converts the value of this instance to its equivalent string representation.</summary>
		/// <returns>The string representation of the value of this instance.</returns>
		// Token: 0x060007E5 RID: 2021 RVA: 0x000225C4 File Offset: 0x000207C4
		public override string ToString()
		{
			return char.ToString(this);
		}

		/// <summary>Converts the value of this instance to its equivalent string representation using the specified culture-specific format information.</summary>
		/// <param name="provider">(Reserved) An object that supplies culture-specific formatting information.</param>
		/// <returns>The string representation of the value of this instance as specified by <paramref name="provider" />.</returns>
		// Token: 0x060007E6 RID: 2022 RVA: 0x000225C4 File Offset: 0x000207C4
		public string ToString(IFormatProvider provider)
		{
			return char.ToString(this);
		}

		/// <summary>Converts the specified Unicode character to its equivalent string representation.</summary>
		/// <param name="c">The Unicode character to convert.</param>
		/// <returns>The string representation of the value of <paramref name="c" />.</returns>
		// Token: 0x060007E7 RID: 2023 RVA: 0x000225CD File Offset: 0x000207CD
		public static string ToString(char c)
		{
			return string.CreateFromChar(c);
		}

		/// <summary>Converts the value of the specified string to its equivalent Unicode character.</summary>
		/// <param name="s">A string that contains a single character, or <see langword="null" />.</param>
		/// <returns>A Unicode character equivalent to the sole character in <paramref name="s" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="s" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.FormatException">The length of <paramref name="s" /> is not 1.</exception>
		// Token: 0x060007E8 RID: 2024 RVA: 0x000225D5 File Offset: 0x000207D5
		public static char Parse(string s)
		{
			if (s == null)
			{
				throw new ArgumentNullException("s");
			}
			if (s.Length != 1)
			{
				throw new FormatException("String must be exactly one character long.");
			}
			return s[0];
		}

		/// <summary>Converts the value of the specified string to its equivalent Unicode character. A return code indicates whether the conversion succeeded or failed.</summary>
		/// <param name="s">A string that contains a single character, or <see langword="null" />.</param>
		/// <param name="result">When this method returns, contains a Unicode character equivalent to the sole character in <paramref name="s" />, if the conversion succeeded, or an undefined value if the conversion failed. The conversion fails if the <paramref name="s" /> parameter is <see langword="null" /> or the length of <paramref name="s" /> is not 1. This parameter is passed uninitialized.</param>
		/// <returns>
		///   <see langword="true" /> if the <paramref name="s" /> parameter was converted successfully; otherwise, <see langword="false" />.</returns>
		// Token: 0x060007E9 RID: 2025 RVA: 0x00022600 File Offset: 0x00020800
		public static bool TryParse(string s, out char result)
		{
			result = '\0';
			if (s == null)
			{
				return false;
			}
			if (s.Length != 1)
			{
				return false;
			}
			result = s[0];
			return true;
		}

		/// <summary>Indicates whether the specified Unicode character is categorized as a decimal digit.</summary>
		/// <param name="c">The Unicode character to evaluate.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="c" /> is a decimal digit; otherwise, <see langword="false" />.</returns>
		// Token: 0x060007EA RID: 2026 RVA: 0x0002261F File Offset: 0x0002081F
		public static bool IsDigit(char c)
		{
			if (char.IsLatin1(c))
			{
				return c >= '0' && c <= '9';
			}
			return CharUnicodeInfo.GetUnicodeCategory(c) == UnicodeCategory.DecimalDigitNumber;
		}

		// Token: 0x060007EB RID: 2027 RVA: 0x00022642 File Offset: 0x00020842
		internal static bool CheckLetter(UnicodeCategory uc)
		{
			return uc <= UnicodeCategory.OtherLetter;
		}

		/// <summary>Indicates whether the specified Unicode character is categorized as a Unicode letter.</summary>
		/// <param name="c">The Unicode character to evaluate.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="c" /> is a letter; otherwise, <see langword="false" />.</returns>
		// Token: 0x060007EC RID: 2028 RVA: 0x0002264B File Offset: 0x0002084B
		public static bool IsLetter(char c)
		{
			if (!char.IsLatin1(c))
			{
				return char.CheckLetter(CharUnicodeInfo.GetUnicodeCategory(c));
			}
			if (char.IsAscii(c))
			{
				c |= ' ';
				return c >= 'a' && c <= 'z';
			}
			return char.CheckLetter(char.GetLatin1UnicodeCategory(c));
		}

		// Token: 0x060007ED RID: 2029 RVA: 0x0002268B File Offset: 0x0002088B
		private static bool IsWhiteSpaceLatin1(char c)
		{
			return c == ' ' || c - '\t' <= '\u0004' || c == '\u00a0' || c == '\u0085';
		}

		/// <summary>Indicates whether the specified Unicode character is categorized as white space.</summary>
		/// <param name="c">The Unicode character to evaluate.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="c" /> is white space; otherwise, <see langword="false" />.</returns>
		// Token: 0x060007EE RID: 2030 RVA: 0x000226AB File Offset: 0x000208AB
		public static bool IsWhiteSpace(char c)
		{
			if (char.IsLatin1(c))
			{
				return char.IsWhiteSpaceLatin1(c);
			}
			return CharUnicodeInfo.IsWhiteSpace(c);
		}

		/// <summary>Indicates whether the specified Unicode character is categorized as an uppercase letter.</summary>
		/// <param name="c">The Unicode character to evaluate.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="c" /> is an uppercase letter; otherwise, <see langword="false" />.</returns>
		// Token: 0x060007EF RID: 2031 RVA: 0x000226C2 File Offset: 0x000208C2
		public static bool IsUpper(char c)
		{
			if (!char.IsLatin1(c))
			{
				return CharUnicodeInfo.GetUnicodeCategory(c) == UnicodeCategory.UppercaseLetter;
			}
			if (char.IsAscii(c))
			{
				return c >= 'A' && c <= 'Z';
			}
			return char.GetLatin1UnicodeCategory(c) == UnicodeCategory.UppercaseLetter;
		}

		/// <summary>Indicates whether the specified Unicode character is categorized as a lowercase letter.</summary>
		/// <param name="c">The Unicode character to evaluate.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="c" /> is a lowercase letter; otherwise, <see langword="false" />.</returns>
		// Token: 0x060007F0 RID: 2032 RVA: 0x000226F7 File Offset: 0x000208F7
		public static bool IsLower(char c)
		{
			if (!char.IsLatin1(c))
			{
				return CharUnicodeInfo.GetUnicodeCategory(c) == UnicodeCategory.LowercaseLetter;
			}
			if (char.IsAscii(c))
			{
				return c >= 'a' && c <= 'z';
			}
			return char.GetLatin1UnicodeCategory(c) == UnicodeCategory.LowercaseLetter;
		}

		// Token: 0x060007F1 RID: 2033 RVA: 0x0002272C File Offset: 0x0002092C
		internal static bool CheckPunctuation(UnicodeCategory uc)
		{
			return uc - UnicodeCategory.ConnectorPunctuation <= 6;
		}

		/// <summary>Indicates whether the specified Unicode character is categorized as a punctuation mark.</summary>
		/// <param name="c">The Unicode character to evaluate.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="c" /> is a punctuation mark; otherwise, <see langword="false" />.</returns>
		// Token: 0x060007F2 RID: 2034 RVA: 0x00022738 File Offset: 0x00020938
		public static bool IsPunctuation(char c)
		{
			if (char.IsLatin1(c))
			{
				return char.CheckPunctuation(char.GetLatin1UnicodeCategory(c));
			}
			return char.CheckPunctuation(CharUnicodeInfo.GetUnicodeCategory(c));
		}

		// Token: 0x060007F3 RID: 2035 RVA: 0x00022759 File Offset: 0x00020959
		internal static bool CheckLetterOrDigit(UnicodeCategory uc)
		{
			return uc <= UnicodeCategory.OtherLetter || uc == UnicodeCategory.DecimalDigitNumber;
		}

		/// <summary>Indicates whether the specified Unicode character is categorized as a letter or a decimal digit.</summary>
		/// <param name="c">The Unicode character to evaluate.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="c" /> is a letter or a decimal digit; otherwise, <see langword="false" />.</returns>
		// Token: 0x060007F4 RID: 2036 RVA: 0x00022766 File Offset: 0x00020966
		public static bool IsLetterOrDigit(char c)
		{
			if (char.IsLatin1(c))
			{
				return char.CheckLetterOrDigit(char.GetLatin1UnicodeCategory(c));
			}
			return char.CheckLetterOrDigit(CharUnicodeInfo.GetUnicodeCategory(c));
		}

		/// <summary>Converts the value of a specified Unicode character to its uppercase equivalent using specified culture-specific formatting information.</summary>
		/// <param name="c">The Unicode character to convert.</param>
		/// <param name="culture">An object that supplies culture-specific casing rules.</param>
		/// <returns>The uppercase equivalent of <paramref name="c" />, modified according to <paramref name="culture" />, or the unchanged value of <paramref name="c" /> if <paramref name="c" /> is already uppercase, has no uppercase equivalent, or is not alphabetic.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="culture" /> is <see langword="null" />.</exception>
		// Token: 0x060007F5 RID: 2037 RVA: 0x00022787 File Offset: 0x00020987
		public static char ToUpper(char c, CultureInfo culture)
		{
			if (culture == null)
			{
				throw new ArgumentNullException("culture");
			}
			return culture.TextInfo.ToUpper(c);
		}

		/// <summary>Converts the value of a Unicode character to its uppercase equivalent.</summary>
		/// <param name="c">The Unicode character to convert.</param>
		/// <returns>The uppercase equivalent of <paramref name="c" />, or the unchanged value of <paramref name="c" /> if <paramref name="c" /> is already uppercase, has no uppercase equivalent, or is not alphabetic.</returns>
		// Token: 0x060007F6 RID: 2038 RVA: 0x000227A3 File Offset: 0x000209A3
		public static char ToUpper(char c)
		{
			return CultureInfo.CurrentCulture.TextInfo.ToUpper(c);
		}

		/// <summary>Converts the value of a Unicode character to its uppercase equivalent using the casing rules of the invariant culture.</summary>
		/// <param name="c">The Unicode character to convert.</param>
		/// <returns>The uppercase equivalent of the <paramref name="c" /> parameter, or the unchanged value of <paramref name="c" />, if <paramref name="c" /> is already uppercase or not alphabetic.</returns>
		// Token: 0x060007F7 RID: 2039 RVA: 0x000227B5 File Offset: 0x000209B5
		public static char ToUpperInvariant(char c)
		{
			return CultureInfo.InvariantCulture.TextInfo.ToUpper(c);
		}

		/// <summary>Converts the value of a specified Unicode character to its lowercase equivalent using specified culture-specific formatting information.</summary>
		/// <param name="c">The Unicode character to convert.</param>
		/// <param name="culture">An object that supplies culture-specific casing rules.</param>
		/// <returns>The lowercase equivalent of <paramref name="c" />, modified according to <paramref name="culture" />, or the unchanged value of <paramref name="c" />, if <paramref name="c" /> is already lowercase or not alphabetic.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="culture" /> is <see langword="null" />.</exception>
		// Token: 0x060007F8 RID: 2040 RVA: 0x000227C7 File Offset: 0x000209C7
		public static char ToLower(char c, CultureInfo culture)
		{
			if (culture == null)
			{
				throw new ArgumentNullException("culture");
			}
			return culture.TextInfo.ToLower(c);
		}

		/// <summary>Converts the value of a Unicode character to its lowercase equivalent.</summary>
		/// <param name="c">The Unicode character to convert.</param>
		/// <returns>The lowercase equivalent of <paramref name="c" />, or the unchanged value of <paramref name="c" />, if <paramref name="c" /> is already lowercase or not alphabetic.</returns>
		// Token: 0x060007F9 RID: 2041 RVA: 0x000227E3 File Offset: 0x000209E3
		public static char ToLower(char c)
		{
			return CultureInfo.CurrentCulture.TextInfo.ToLower(c);
		}

		/// <summary>Converts the value of a Unicode character to its lowercase equivalent using the casing rules of the invariant culture.</summary>
		/// <param name="c">The Unicode character to convert.</param>
		/// <returns>The lowercase equivalent of the <paramref name="c" /> parameter, or the unchanged value of <paramref name="c" />, if <paramref name="c" /> is already lowercase or not alphabetic.</returns>
		// Token: 0x060007FA RID: 2042 RVA: 0x000227F5 File Offset: 0x000209F5
		public static char ToLowerInvariant(char c)
		{
			return CultureInfo.InvariantCulture.TextInfo.ToLower(c);
		}

		/// <summary>Returns the <see cref="T:System.TypeCode" /> for value type <see cref="T:System.Char" />.</summary>
		/// <returns>The enumerated constant, <see cref="F:System.TypeCode.Char" />.</returns>
		// Token: 0x060007FB RID: 2043 RVA: 0x00022807 File Offset: 0x00020A07
		public TypeCode GetTypeCode()
		{
			return TypeCode.Char;
		}

		/// <summary>Note This conversion is not supported. Attempting to do so throws an <see cref="T:System.InvalidCastException" />.</summary>
		/// <param name="provider">This parameter is ignored.</param>
		/// <returns>This conversion is not supported. No value is returned.</returns>
		/// <exception cref="T:System.InvalidCastException">This conversion is not supported.</exception>
		// Token: 0x060007FC RID: 2044 RVA: 0x0002280A File Offset: 0x00020A0A
		bool IConvertible.ToBoolean(IFormatProvider provider)
		{
			throw new InvalidCastException(SR.Format("Invalid cast from '{0}' to '{1}'.", "Char", "Boolean"));
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToChar(System.IFormatProvider)" />.</summary>
		/// <param name="provider">This parameter is ignored.</param>
		/// <returns>The value of the current <see cref="T:System.Char" /> object unchanged.</returns>
		// Token: 0x060007FD RID: 2045 RVA: 0x00022825 File Offset: 0x00020A25
		char IConvertible.ToChar(IFormatProvider provider)
		{
			return this;
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToSByte(System.IFormatProvider)" />.</summary>
		/// <param name="provider">This parameter is ignored.</param>
		/// <returns>The converted value of the current <see cref="T:System.Char" /> object.</returns>
		// Token: 0x060007FE RID: 2046 RVA: 0x00022829 File Offset: 0x00020A29
		sbyte IConvertible.ToSByte(IFormatProvider provider)
		{
			return Convert.ToSByte(this);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToByte(System.IFormatProvider)" />.</summary>
		/// <param name="provider">This parameter is ignored.</param>
		/// <returns>The converted value of the current <see cref="T:System.Char" /> object.</returns>
		// Token: 0x060007FF RID: 2047 RVA: 0x00022832 File Offset: 0x00020A32
		byte IConvertible.ToByte(IFormatProvider provider)
		{
			return Convert.ToByte(this);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToInt16(System.IFormatProvider)" />.</summary>
		/// <param name="provider">This parameter is ignored.</param>
		/// <returns>The converted value of the current <see cref="T:System.Char" /> object.</returns>
		// Token: 0x06000800 RID: 2048 RVA: 0x0002283B File Offset: 0x00020A3B
		short IConvertible.ToInt16(IFormatProvider provider)
		{
			return Convert.ToInt16(this);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToUInt16(System.IFormatProvider)" />.</summary>
		/// <param name="provider">An <see cref="T:System.IFormatProvider" /> object. (Specify <see langword="null" /> because the <paramref name="provider" /> parameter is ignored.)</param>
		/// <returns>The converted value of the current <see cref="T:System.Char" /> object.</returns>
		// Token: 0x06000801 RID: 2049 RVA: 0x00022844 File Offset: 0x00020A44
		ushort IConvertible.ToUInt16(IFormatProvider provider)
		{
			return Convert.ToUInt16(this);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToInt32(System.IFormatProvider)" />.</summary>
		/// <param name="provider">This parameter is ignored.</param>
		/// <returns>The converted value of the current <see cref="T:System.Char" /> object.</returns>
		// Token: 0x06000802 RID: 2050 RVA: 0x0002284D File Offset: 0x00020A4D
		int IConvertible.ToInt32(IFormatProvider provider)
		{
			return Convert.ToInt32(this);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToUInt32(System.IFormatProvider)" />.</summary>
		/// <param name="provider">An <see cref="T:System.IFormatProvider" /> object. (Specify <see langword="null" /> because the <paramref name="provider" /> parameter is ignored.)</param>
		/// <returns>The converted value of the current <see cref="T:System.Char" /> object.</returns>
		// Token: 0x06000803 RID: 2051 RVA: 0x00022856 File Offset: 0x00020A56
		uint IConvertible.ToUInt32(IFormatProvider provider)
		{
			return Convert.ToUInt32(this);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToInt64(System.IFormatProvider)" />.</summary>
		/// <param name="provider">This parameter is ignored.</param>
		/// <returns>The converted value of the current <see cref="T:System.Char" /> object.</returns>
		// Token: 0x06000804 RID: 2052 RVA: 0x0002285F File Offset: 0x00020A5F
		long IConvertible.ToInt64(IFormatProvider provider)
		{
			return Convert.ToInt64(this);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToUInt64(System.IFormatProvider)" />.</summary>
		/// <param name="provider">An <see cref="T:System.IFormatProvider" /> object. (Specify <see langword="null" /> because the <paramref name="provider" /> parameter is ignored.)</param>
		/// <returns>The converted value of the current <see cref="T:System.Char" /> object.</returns>
		// Token: 0x06000805 RID: 2053 RVA: 0x00022868 File Offset: 0x00020A68
		ulong IConvertible.ToUInt64(IFormatProvider provider)
		{
			return Convert.ToUInt64(this);
		}

		/// <summary>Note This conversion is not supported. Attempting to do so throws an <see cref="T:System.InvalidCastException" />.</summary>
		/// <param name="provider">This parameter is ignored.</param>
		/// <returns>No value is returned.</returns>
		/// <exception cref="T:System.InvalidCastException">This conversion is not supported.</exception>
		// Token: 0x06000806 RID: 2054 RVA: 0x00022871 File Offset: 0x00020A71
		float IConvertible.ToSingle(IFormatProvider provider)
		{
			throw new InvalidCastException(SR.Format("Invalid cast from '{0}' to '{1}'.", "Char", "Single"));
		}

		/// <summary>Note This conversion is not supported. Attempting to do so throws an <see cref="T:System.InvalidCastException" />.</summary>
		/// <param name="provider">This parameter is ignored.</param>
		/// <returns>No value is returned.</returns>
		/// <exception cref="T:System.InvalidCastException">This conversion is not supported.</exception>
		// Token: 0x06000807 RID: 2055 RVA: 0x0002288C File Offset: 0x00020A8C
		double IConvertible.ToDouble(IFormatProvider provider)
		{
			throw new InvalidCastException(SR.Format("Invalid cast from '{0}' to '{1}'.", "Char", "Double"));
		}

		/// <summary>Note This conversion is not supported. Attempting to do so throws an <see cref="T:System.InvalidCastException" />.</summary>
		/// <param name="provider">This parameter is ignored.</param>
		/// <returns>No value is returned.</returns>
		/// <exception cref="T:System.InvalidCastException">This conversion is not supported.</exception>
		// Token: 0x06000808 RID: 2056 RVA: 0x000228A7 File Offset: 0x00020AA7
		decimal IConvertible.ToDecimal(IFormatProvider provider)
		{
			throw new InvalidCastException(SR.Format("Invalid cast from '{0}' to '{1}'.", "Char", "Decimal"));
		}

		/// <summary>Note This conversion is not supported. Attempting to do so throws an <see cref="T:System.InvalidCastException" />.</summary>
		/// <param name="provider">This parameter is ignored.</param>
		/// <returns>No value is returned.</returns>
		/// <exception cref="T:System.InvalidCastException">This conversion is not supported.</exception>
		// Token: 0x06000809 RID: 2057 RVA: 0x000228C2 File Offset: 0x00020AC2
		DateTime IConvertible.ToDateTime(IFormatProvider provider)
		{
			throw new InvalidCastException(SR.Format("Invalid cast from '{0}' to '{1}'.", "Char", "DateTime"));
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToType(System.Type,System.IFormatProvider)" />.</summary>
		/// <param name="type">A <see cref="T:System.Type" /> object.</param>
		/// <param name="provider">An <see cref="T:System.IFormatProvider" /> object.</param>
		/// <returns>An object of the specified type.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="type" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidCastException">The value of the current <see cref="T:System.Char" /> object cannot be converted to the type specified by the <paramref name="type" /> parameter.</exception>
		// Token: 0x0600080A RID: 2058 RVA: 0x000228DD File Offset: 0x00020ADD
		object IConvertible.ToType(Type type, IFormatProvider provider)
		{
			return Convert.DefaultToType(this, type, provider);
		}

		/// <summary>Indicates whether the specified Unicode character is categorized as a control character.</summary>
		/// <param name="c">The Unicode character to evaluate.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="c" /> is a control character; otherwise, <see langword="false" />.</returns>
		// Token: 0x0600080B RID: 2059 RVA: 0x000228ED File Offset: 0x00020AED
		public static bool IsControl(char c)
		{
			if (char.IsLatin1(c))
			{
				return char.GetLatin1UnicodeCategory(c) == UnicodeCategory.Control;
			}
			return CharUnicodeInfo.GetUnicodeCategory(c) == UnicodeCategory.Control;
		}

		/// <summary>Indicates whether the character at the specified position in a specified string is categorized as a control character.</summary>
		/// <param name="s">A string.</param>
		/// <param name="index">The position of the character to evaluate in <paramref name="s" />.</param>
		/// <returns>
		///   <see langword="true" /> if the character at position <paramref name="index" /> in <paramref name="s" /> is a control character; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="s" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero or greater than the last position in <paramref name="s" />.</exception>
		// Token: 0x0600080C RID: 2060 RVA: 0x0002290C File Offset: 0x00020B0C
		public static bool IsControl(string s, int index)
		{
			if (s == null)
			{
				throw new ArgumentNullException("s");
			}
			if (index >= s.Length)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			char ch = s[index];
			if (char.IsLatin1(ch))
			{
				return char.GetLatin1UnicodeCategory(ch) == UnicodeCategory.Control;
			}
			return CharUnicodeInfo.GetUnicodeCategory(s, index) == UnicodeCategory.Control;
		}

		/// <summary>Indicates whether the character at the specified position in a specified string is categorized as a decimal digit.</summary>
		/// <param name="s">A string.</param>
		/// <param name="index">The position of the character to evaluate in <paramref name="s" />.</param>
		/// <returns>
		///   <see langword="true" /> if the character at position <paramref name="index" /> in <paramref name="s" /> is a decimal digit; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="s" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero or greater than the last position in <paramref name="s" />.</exception>
		// Token: 0x0600080D RID: 2061 RVA: 0x00022964 File Offset: 0x00020B64
		public static bool IsDigit(string s, int index)
		{
			if (s == null)
			{
				throw new ArgumentNullException("s");
			}
			if (index >= s.Length)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			char c = s[index];
			if (char.IsLatin1(c))
			{
				return c >= '0' && c <= '9';
			}
			return CharUnicodeInfo.GetUnicodeCategory(s, index) == UnicodeCategory.DecimalDigitNumber;
		}

		/// <summary>Indicates whether the character at the specified position in a specified string is categorized as a Unicode letter.</summary>
		/// <param name="s">A string.</param>
		/// <param name="index">The position of the character to evaluate in <paramref name="s" />.</param>
		/// <returns>
		///   <see langword="true" /> if the character at position <paramref name="index" /> in <paramref name="s" /> is a letter; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="s" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero or greater than the last position in <paramref name="s" />.</exception>
		// Token: 0x0600080E RID: 2062 RVA: 0x000229C0 File Offset: 0x00020BC0
		public static bool IsLetter(string s, int index)
		{
			if (s == null)
			{
				throw new ArgumentNullException("s");
			}
			if (index >= s.Length)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			char c = s[index];
			if (!char.IsLatin1(c))
			{
				return char.CheckLetter(CharUnicodeInfo.GetUnicodeCategory(s, index));
			}
			if (char.IsAscii(c))
			{
				c |= ' ';
				return c >= 'a' && c <= 'z';
			}
			return char.CheckLetter(char.GetLatin1UnicodeCategory(c));
		}

		/// <summary>Indicates whether the character at the specified position in a specified string is categorized as a letter or a decimal digit.</summary>
		/// <param name="s">A string.</param>
		/// <param name="index">The position of the character to evaluate in <paramref name="s" />.</param>
		/// <returns>
		///   <see langword="true" /> if the character at position <paramref name="index" /> in <paramref name="s" /> is a letter or a decimal digit; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="s" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero or greater than the last position in <paramref name="s" />.</exception>
		// Token: 0x0600080F RID: 2063 RVA: 0x00022A38 File Offset: 0x00020C38
		public static bool IsLetterOrDigit(string s, int index)
		{
			if (s == null)
			{
				throw new ArgumentNullException("s");
			}
			if (index >= s.Length)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			char ch = s[index];
			if (char.IsLatin1(ch))
			{
				return char.CheckLetterOrDigit(char.GetLatin1UnicodeCategory(ch));
			}
			return char.CheckLetterOrDigit(CharUnicodeInfo.GetUnicodeCategory(s, index));
		}

		/// <summary>Indicates whether the character at the specified position in a specified string is categorized as a lowercase letter.</summary>
		/// <param name="s">A string.</param>
		/// <param name="index">The position of the character to evaluate in <paramref name="s" />.</param>
		/// <returns>
		///   <see langword="true" /> if the character at position <paramref name="index" /> in <paramref name="s" /> is a lowercase letter; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="s" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero or greater than the last position in <paramref name="s" />.</exception>
		// Token: 0x06000810 RID: 2064 RVA: 0x00022A90 File Offset: 0x00020C90
		public static bool IsLower(string s, int index)
		{
			if (s == null)
			{
				throw new ArgumentNullException("s");
			}
			if (index >= s.Length)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			char c = s[index];
			if (!char.IsLatin1(c))
			{
				return CharUnicodeInfo.GetUnicodeCategory(s, index) == UnicodeCategory.LowercaseLetter;
			}
			if (char.IsAscii(c))
			{
				return c >= 'a' && c <= 'z';
			}
			return char.GetLatin1UnicodeCategory(c) == UnicodeCategory.LowercaseLetter;
		}

		// Token: 0x06000811 RID: 2065 RVA: 0x00022AFB File Offset: 0x00020CFB
		internal static bool CheckNumber(UnicodeCategory uc)
		{
			return uc - UnicodeCategory.DecimalDigitNumber <= 2;
		}

		/// <summary>Indicates whether the specified Unicode character is categorized as a number.</summary>
		/// <param name="c">The Unicode character to evaluate.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="c" /> is a number; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000812 RID: 2066 RVA: 0x00022B06 File Offset: 0x00020D06
		public static bool IsNumber(char c)
		{
			if (!char.IsLatin1(c))
			{
				return char.CheckNumber(CharUnicodeInfo.GetUnicodeCategory(c));
			}
			if (char.IsAscii(c))
			{
				return c >= '0' && c <= '9';
			}
			return char.CheckNumber(char.GetLatin1UnicodeCategory(c));
		}

		/// <summary>Indicates whether the character at the specified position in a specified string is categorized as a number.</summary>
		/// <param name="s">A string.</param>
		/// <param name="index">The position of the character to evaluate in <paramref name="s" />.</param>
		/// <returns>
		///   <see langword="true" /> if the character at position <paramref name="index" /> in <paramref name="s" /> is a number; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="s" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero or greater than the last position in <paramref name="s" />.</exception>
		// Token: 0x06000813 RID: 2067 RVA: 0x00022B40 File Offset: 0x00020D40
		public static bool IsNumber(string s, int index)
		{
			if (s == null)
			{
				throw new ArgumentNullException("s");
			}
			if (index >= s.Length)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			char c = s[index];
			if (!char.IsLatin1(c))
			{
				return char.CheckNumber(CharUnicodeInfo.GetUnicodeCategory(s, index));
			}
			if (char.IsAscii(c))
			{
				return c >= '0' && c <= '9';
			}
			return char.CheckNumber(char.GetLatin1UnicodeCategory(c));
		}

		/// <summary>Indicates whether the character at the specified position in a specified string is categorized as a punctuation mark.</summary>
		/// <param name="s">A string.</param>
		/// <param name="index">The position of the character to evaluate in <paramref name="s" />.</param>
		/// <returns>
		///   <see langword="true" /> if the character at position <paramref name="index" /> in <paramref name="s" /> is a punctuation mark; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="s" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero or greater than the last position in <paramref name="s" />.</exception>
		// Token: 0x06000814 RID: 2068 RVA: 0x00022BB0 File Offset: 0x00020DB0
		public static bool IsPunctuation(string s, int index)
		{
			if (s == null)
			{
				throw new ArgumentNullException("s");
			}
			if (index >= s.Length)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			char ch = s[index];
			if (char.IsLatin1(ch))
			{
				return char.CheckPunctuation(char.GetLatin1UnicodeCategory(ch));
			}
			return char.CheckPunctuation(CharUnicodeInfo.GetUnicodeCategory(s, index));
		}

		// Token: 0x06000815 RID: 2069 RVA: 0x00022C07 File Offset: 0x00020E07
		internal static bool CheckSeparator(UnicodeCategory uc)
		{
			return uc - UnicodeCategory.SpaceSeparator <= 2;
		}

		// Token: 0x06000816 RID: 2070 RVA: 0x00022C13 File Offset: 0x00020E13
		private static bool IsSeparatorLatin1(char c)
		{
			return c == ' ' || c == '\u00a0';
		}

		/// <summary>Indicates whether the specified Unicode character is categorized as a separator character.</summary>
		/// <param name="c">The Unicode character to evaluate.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="c" /> is a separator character; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000817 RID: 2071 RVA: 0x00022C24 File Offset: 0x00020E24
		public static bool IsSeparator(char c)
		{
			if (char.IsLatin1(c))
			{
				return char.IsSeparatorLatin1(c);
			}
			return char.CheckSeparator(CharUnicodeInfo.GetUnicodeCategory(c));
		}

		/// <summary>Indicates whether the character at the specified position in a specified string is categorized as a separator character.</summary>
		/// <param name="s">A string.</param>
		/// <param name="index">The position of the character to evaluate in <paramref name="s" />.</param>
		/// <returns>
		///   <see langword="true" /> if the character at position <paramref name="index" /> in <paramref name="s" /> is a separator character; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="s" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero or greater than the last position in <paramref name="s" />.</exception>
		// Token: 0x06000818 RID: 2072 RVA: 0x00022C40 File Offset: 0x00020E40
		public static bool IsSeparator(string s, int index)
		{
			if (s == null)
			{
				throw new ArgumentNullException("s");
			}
			if (index >= s.Length)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			char c = s[index];
			if (char.IsLatin1(c))
			{
				return char.IsSeparatorLatin1(c);
			}
			return char.CheckSeparator(CharUnicodeInfo.GetUnicodeCategory(s, index));
		}

		/// <summary>Indicates whether the specified character has a surrogate code unit.</summary>
		/// <param name="c">The Unicode character to evaluate.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="c" /> is either a high surrogate or a low surrogate; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000819 RID: 2073 RVA: 0x00022C92 File Offset: 0x00020E92
		public static bool IsSurrogate(char c)
		{
			return c >= '\ud800' && c <= '\udfff';
		}

		/// <summary>Indicates whether the character at the specified position in a specified string has a surrogate code unit.</summary>
		/// <param name="s">A string.</param>
		/// <param name="index">The position of the character to evaluate in <paramref name="s" />.</param>
		/// <returns>
		///   <see langword="true" /> if the character at position <paramref name="index" /> in <paramref name="s" /> is a either a high surrogate or a low surrogate; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="s" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero or greater than the last position in <paramref name="s" />.</exception>
		// Token: 0x0600081A RID: 2074 RVA: 0x00022CA9 File Offset: 0x00020EA9
		public static bool IsSurrogate(string s, int index)
		{
			if (s == null)
			{
				throw new ArgumentNullException("s");
			}
			if (index >= s.Length)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			return char.IsSurrogate(s[index]);
		}

		// Token: 0x0600081B RID: 2075 RVA: 0x00022CD9 File Offset: 0x00020ED9
		internal static bool CheckSymbol(UnicodeCategory uc)
		{
			return uc - UnicodeCategory.MathSymbol <= 3;
		}

		/// <summary>Indicates whether the specified Unicode character is categorized as a symbol character.</summary>
		/// <param name="c">The Unicode character to evaluate.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="c" /> is a symbol character; otherwise, <see langword="false" />.</returns>
		// Token: 0x0600081C RID: 2076 RVA: 0x00022CE5 File Offset: 0x00020EE5
		public static bool IsSymbol(char c)
		{
			if (char.IsLatin1(c))
			{
				return char.CheckSymbol(char.GetLatin1UnicodeCategory(c));
			}
			return char.CheckSymbol(CharUnicodeInfo.GetUnicodeCategory(c));
		}

		/// <summary>Indicates whether the character at the specified position in a specified string is categorized as a symbol character.</summary>
		/// <param name="s">A string.</param>
		/// <param name="index">The position of the character to evaluate in <paramref name="s" />.</param>
		/// <returns>
		///   <see langword="true" /> if the character at position <paramref name="index" /> in <paramref name="s" /> is a symbol character; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="s" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero or greater than the last position in <paramref name="s" />.</exception>
		// Token: 0x0600081D RID: 2077 RVA: 0x00022D08 File Offset: 0x00020F08
		public static bool IsSymbol(string s, int index)
		{
			if (s == null)
			{
				throw new ArgumentNullException("s");
			}
			if (index >= s.Length)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			char ch = s[index];
			if (char.IsLatin1(ch))
			{
				return char.CheckSymbol(char.GetLatin1UnicodeCategory(ch));
			}
			return char.CheckSymbol(CharUnicodeInfo.GetUnicodeCategory(s, index));
		}

		/// <summary>Indicates whether the character at the specified position in a specified string is categorized as an uppercase letter.</summary>
		/// <param name="s">A string.</param>
		/// <param name="index">The position of the character to evaluate in <paramref name="s" />.</param>
		/// <returns>
		///   <see langword="true" /> if the character at position <paramref name="index" /> in <paramref name="s" /> is an uppercase letter; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="s" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero or greater than the last position in <paramref name="s" />.</exception>
		// Token: 0x0600081E RID: 2078 RVA: 0x00022D60 File Offset: 0x00020F60
		public static bool IsUpper(string s, int index)
		{
			if (s == null)
			{
				throw new ArgumentNullException("s");
			}
			if (index >= s.Length)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			char c = s[index];
			if (!char.IsLatin1(c))
			{
				return CharUnicodeInfo.GetUnicodeCategory(s, index) == UnicodeCategory.UppercaseLetter;
			}
			if (char.IsAscii(c))
			{
				return c >= 'A' && c <= 'Z';
			}
			return char.GetLatin1UnicodeCategory(c) == UnicodeCategory.UppercaseLetter;
		}

		/// <summary>Indicates whether the character at the specified position in a specified string is categorized as white space.</summary>
		/// <param name="s">A string.</param>
		/// <param name="index">The position of the character to evaluate in <paramref name="s" />.</param>
		/// <returns>
		///   <see langword="true" /> if the character at position <paramref name="index" /> in <paramref name="s" /> is white space; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="s" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero or greater than the last position in <paramref name="s" />.</exception>
		// Token: 0x0600081F RID: 2079 RVA: 0x00022DCC File Offset: 0x00020FCC
		public static bool IsWhiteSpace(string s, int index)
		{
			if (s == null)
			{
				throw new ArgumentNullException("s");
			}
			if (index >= s.Length)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			if (char.IsLatin1(s[index]))
			{
				return char.IsWhiteSpaceLatin1(s[index]);
			}
			return CharUnicodeInfo.IsWhiteSpace(s, index);
		}

		/// <summary>Categorizes a specified Unicode character into a group identified by one of the <see cref="T:System.Globalization.UnicodeCategory" /> values.</summary>
		/// <param name="c">The Unicode character to categorize.</param>
		/// <returns>A <see cref="T:System.Globalization.UnicodeCategory" /> value that identifies the group that contains <paramref name="c" />.</returns>
		// Token: 0x06000820 RID: 2080 RVA: 0x00022E1D File Offset: 0x0002101D
		public static UnicodeCategory GetUnicodeCategory(char c)
		{
			if (char.IsLatin1(c))
			{
				return char.GetLatin1UnicodeCategory(c);
			}
			return CharUnicodeInfo.GetUnicodeCategory((int)c);
		}

		/// <summary>Categorizes the character at the specified position in a specified string into a group identified by one of the <see cref="T:System.Globalization.UnicodeCategory" /> values.</summary>
		/// <param name="s">A <see cref="T:System.String" />.</param>
		/// <param name="index">The character position in <paramref name="s" />.</param>
		/// <returns>A <see cref="T:System.Globalization.UnicodeCategory" /> enumerated constant that identifies the group that contains the character at position <paramref name="index" /> in <paramref name="s" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="s" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero or greater than the last position in <paramref name="s" />.</exception>
		// Token: 0x06000821 RID: 2081 RVA: 0x00022E34 File Offset: 0x00021034
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
			if (char.IsLatin1(s[index]))
			{
				return char.GetLatin1UnicodeCategory(s[index]);
			}
			return CharUnicodeInfo.InternalGetUnicodeCategory(s, index);
		}

		/// <summary>Converts the specified numeric Unicode character to a double-precision floating point number.</summary>
		/// <param name="c">The Unicode character to convert.</param>
		/// <returns>The numeric value of <paramref name="c" /> if that character represents a number; otherwise, -1.0.</returns>
		// Token: 0x06000822 RID: 2082 RVA: 0x00022E85 File Offset: 0x00021085
		public static double GetNumericValue(char c)
		{
			return CharUnicodeInfo.GetNumericValue(c);
		}

		/// <summary>Converts the numeric Unicode character at the specified position in a specified string to a double-precision floating point number.</summary>
		/// <param name="s">A <see cref="T:System.String" />.</param>
		/// <param name="index">The character position in <paramref name="s" />.</param>
		/// <returns>The numeric value of the character at position <paramref name="index" /> in <paramref name="s" /> if that character represents a number; otherwise, -1.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="s" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero or greater than the last position in <paramref name="s" />.</exception>
		// Token: 0x06000823 RID: 2083 RVA: 0x00022E8D File Offset: 0x0002108D
		public static double GetNumericValue(string s, int index)
		{
			if (s == null)
			{
				throw new ArgumentNullException("s");
			}
			if (index >= s.Length)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			return CharUnicodeInfo.GetNumericValue(s, index);
		}

		/// <summary>Indicates whether the specified <see cref="T:System.Char" /> object is a high surrogate.</summary>
		/// <param name="c">The Unicode character to evaluate.</param>
		/// <returns>
		///   <see langword="true" /> if the numeric value of the <paramref name="c" /> parameter ranges from U+D800 through U+DBFF; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000824 RID: 2084 RVA: 0x00022EB8 File Offset: 0x000210B8
		public static bool IsHighSurrogate(char c)
		{
			return c >= '\ud800' && c <= '\udbff';
		}

		/// <summary>Indicates whether the <see cref="T:System.Char" /> object at the specified position in a string is a high surrogate.</summary>
		/// <param name="s">A string.</param>
		/// <param name="index">The position of the character to evaluate in <paramref name="s" />.</param>
		/// <returns>
		///   <see langword="true" /> if the numeric value of the specified character in the <paramref name="s" /> parameter ranges from U+D800 through U+DBFF; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="s" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is not a position within <paramref name="s" />.</exception>
		// Token: 0x06000825 RID: 2085 RVA: 0x00022ECF File Offset: 0x000210CF
		public static bool IsHighSurrogate(string s, int index)
		{
			if (s == null)
			{
				throw new ArgumentNullException("s");
			}
			if (index < 0 || index >= s.Length)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			return char.IsHighSurrogate(s[index]);
		}

		/// <summary>Indicates whether the specified <see cref="T:System.Char" /> object is a low surrogate.</summary>
		/// <param name="c">The character to evaluate.</param>
		/// <returns>
		///   <see langword="true" /> if the numeric value of the <paramref name="c" /> parameter ranges from U+DC00 through U+DFFF; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000826 RID: 2086 RVA: 0x00022F03 File Offset: 0x00021103
		public static bool IsLowSurrogate(char c)
		{
			return c >= '\udc00' && c <= '\udfff';
		}

		/// <summary>Indicates whether the <see cref="T:System.Char" /> object at the specified position in a string is a low surrogate.</summary>
		/// <param name="s">A string.</param>
		/// <param name="index">The position of the character to evaluate in <paramref name="s" />.</param>
		/// <returns>
		///   <see langword="true" /> if the numeric value of the specified character in the <paramref name="s" /> parameter ranges from U+DC00 through U+DFFF; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="s" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is not a position within <paramref name="s" />.</exception>
		// Token: 0x06000827 RID: 2087 RVA: 0x00022F1A File Offset: 0x0002111A
		public static bool IsLowSurrogate(string s, int index)
		{
			if (s == null)
			{
				throw new ArgumentNullException("s");
			}
			if (index < 0 || index >= s.Length)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			return char.IsLowSurrogate(s[index]);
		}

		/// <summary>Indicates whether two adjacent <see cref="T:System.Char" /> objects at a specified position in a string form a surrogate pair.</summary>
		/// <param name="s">A string.</param>
		/// <param name="index">The starting position of the pair of characters to evaluate within <paramref name="s" />.</param>
		/// <returns>
		///   <see langword="true" /> if the <paramref name="s" /> parameter includes adjacent characters at positions <paramref name="index" /> and <paramref name="index" /> + 1, and the numeric value of the character at position <paramref name="index" /> ranges from U+D800 through U+DBFF, and the numeric value of the character at position <paramref name="index" />+1 ranges from U+DC00 through U+DFFF; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="s" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is not a position within <paramref name="s" />.</exception>
		// Token: 0x06000828 RID: 2088 RVA: 0x00022F50 File Offset: 0x00021150
		public static bool IsSurrogatePair(string s, int index)
		{
			if (s == null)
			{
				throw new ArgumentNullException("s");
			}
			if (index < 0 || index >= s.Length)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			return index + 1 < s.Length && char.IsSurrogatePair(s[index], s[index + 1]);
		}

		/// <summary>Indicates whether the two specified <see cref="T:System.Char" /> objects form a surrogate pair.</summary>
		/// <param name="highSurrogate">The character to evaluate as the high surrogate of a surrogate pair.</param>
		/// <param name="lowSurrogate">The character to evaluate as the low surrogate of a surrogate pair.</param>
		/// <returns>
		///   <see langword="true" /> if the numeric value of the <paramref name="highSurrogate" /> parameter ranges from U+D800 through U+DBFF, and the numeric value of the <paramref name="lowSurrogate" /> parameter ranges from U+DC00 through U+DFFF; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000829 RID: 2089 RVA: 0x00022FA5 File Offset: 0x000211A5
		public static bool IsSurrogatePair(char highSurrogate, char lowSurrogate)
		{
			return highSurrogate >= '\ud800' && highSurrogate <= '\udbff' && lowSurrogate >= '\udc00' && lowSurrogate <= '\udfff';
		}

		/// <summary>Converts the specified Unicode code point into a UTF-16 encoded string.</summary>
		/// <param name="utf32">A 21-bit Unicode code point.</param>
		/// <returns>A string consisting of one <see cref="T:System.Char" /> object or a surrogate pair of <see cref="T:System.Char" /> objects equivalent to the code point specified by the <paramref name="utf32" /> parameter.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="utf32" /> is not a valid 21-bit Unicode code point ranging from U+0 through U+10FFFF, excluding the surrogate pair range from U+D800 through U+DFFF.</exception>
		// Token: 0x0600082A RID: 2090 RVA: 0x00022FD0 File Offset: 0x000211D0
		public unsafe static string ConvertFromUtf32(int utf32)
		{
			if (utf32 < 0 || utf32 > 1114111 || (utf32 >= 55296 && utf32 <= 57343))
			{
				throw new ArgumentOutOfRangeException("utf32", "A valid UTF32 value is between 0x000000 and 0x10ffff, inclusive, and should not include surrogate codepoint values (0x00d800 ~ 0x00dfff).");
			}
			if (utf32 < 65536)
			{
				return char.ToString((char)utf32);
			}
			utf32 -= 65536;
			uint num = 0U;
			char* ptr = (char*)(&num);
			*ptr = (char)(utf32 / 1024 + 55296);
			ptr[1] = (char)(utf32 % 1024 + 56320);
			return new string(ptr, 0, 2);
		}

		/// <summary>Converts the value of a UTF-16 encoded surrogate pair into a Unicode code point.</summary>
		/// <param name="highSurrogate">A high surrogate code unit (that is, a code unit ranging from U+D800 through U+DBFF).</param>
		/// <param name="lowSurrogate">A low surrogate code unit (that is, a code unit ranging from U+DC00 through U+DFFF).</param>
		/// <returns>The 21-bit Unicode code point represented by the <paramref name="highSurrogate" /> and <paramref name="lowSurrogate" /> parameters.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="highSurrogate" /> is not in the range U+D800 through U+DBFF, or <paramref name="lowSurrogate" /> is not in the range U+DC00 through U+DFFF.</exception>
		// Token: 0x0600082B RID: 2091 RVA: 0x00023054 File Offset: 0x00021254
		public static int ConvertToUtf32(char highSurrogate, char lowSurrogate)
		{
			if (!char.IsHighSurrogate(highSurrogate))
			{
				throw new ArgumentOutOfRangeException("highSurrogate", "A valid high surrogate character is between 0xd800 and 0xdbff, inclusive.");
			}
			if (!char.IsLowSurrogate(lowSurrogate))
			{
				throw new ArgumentOutOfRangeException("lowSurrogate", "A valid low surrogate character is between 0xdc00 and 0xdfff, inclusive.");
			}
			return (int)((highSurrogate - '\ud800') * 'Ѐ' + (lowSurrogate - '\udc00')) + 65536;
		}

		/// <summary>Converts the value of a UTF-16 encoded character or surrogate pair at a specified position in a string into a Unicode code point.</summary>
		/// <param name="s">A string that contains a character or surrogate pair.</param>
		/// <param name="index">The index position of the character or surrogate pair in <paramref name="s" />.</param>
		/// <returns>The 21-bit Unicode code point represented by the character or surrogate pair at the position in the <paramref name="s" /> parameter specified by the <paramref name="index" /> parameter.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="s" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is not a position within <paramref name="s" />.</exception>
		/// <exception cref="T:System.ArgumentException">The specified index position contains a surrogate pair, and either the first character in the pair is not a valid high surrogate or the second character in the pair is not a valid low surrogate.</exception>
		// Token: 0x0600082C RID: 2092 RVA: 0x000230AC File Offset: 0x000212AC
		public static int ConvertToUtf32(string s, int index)
		{
			if (s == null)
			{
				throw new ArgumentNullException("s");
			}
			if (index < 0 || index >= s.Length)
			{
				throw new ArgumentOutOfRangeException("index", "Index was out of range. Must be non-negative and less than the size of the collection.");
			}
			int num = (int)(s[index] - '\ud800');
			if (num < 0 || num > 2047)
			{
				return (int)s[index];
			}
			if (num > 1023)
			{
				throw new ArgumentException(SR.Format("Found a low surrogate char without a preceding high surrogate at index: {0}. The input may not be in this encoding, or may not contain valid Unicode (UTF-16) characters.", index), "s");
			}
			if (index >= s.Length - 1)
			{
				throw new ArgumentException(SR.Format("Found a high surrogate char without a following low surrogate at index: {0}. The input may not be in this encoding, or may not contain valid Unicode (UTF-16) characters.", index), "s");
			}
			int num2 = (int)(s[index + 1] - '\udc00');
			if (num2 >= 0 && num2 <= 1023)
			{
				return num * 1024 + num2 + 65536;
			}
			throw new ArgumentException(SR.Format("Found a high surrogate char without a following low surrogate at index: {0}. The input may not be in this encoding, or may not contain valid Unicode (UTF-16) characters.", index), "s");
		}

		// Token: 0x0600082D RID: 2093 RVA: 0x0002319B File Offset: 0x0002139B
		// Note: this type is marked as 'beforefieldinit'.
		static Char()
		{
		}

		// Token: 0x04001078 RID: 4216
		private readonly char m_value;

		/// <summary>Represents the largest possible value of a <see cref="T:System.Char" />. This field is constant.</summary>
		// Token: 0x04001079 RID: 4217
		public const char MaxValue = '￿';

		/// <summary>Represents the smallest possible value of a <see cref="T:System.Char" />. This field is constant.</summary>
		// Token: 0x0400107A RID: 4218
		public const char MinValue = '\0';

		// Token: 0x0400107B RID: 4219
		private static readonly byte[] s_categoryForLatin1 = new byte[]
		{
			14,
			14,
			14,
			14,
			14,
			14,
			14,
			14,
			14,
			14,
			14,
			14,
			14,
			14,
			14,
			14,
			14,
			14,
			14,
			14,
			14,
			14,
			14,
			14,
			14,
			14,
			14,
			14,
			14,
			14,
			14,
			14,
			11,
			24,
			24,
			24,
			26,
			24,
			24,
			24,
			20,
			21,
			24,
			25,
			24,
			19,
			24,
			24,
			8,
			8,
			8,
			8,
			8,
			8,
			8,
			8,
			8,
			8,
			24,
			24,
			25,
			25,
			25,
			24,
			24,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			20,
			24,
			21,
			27,
			18,
			27,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			20,
			25,
			21,
			25,
			14,
			14,
			14,
			14,
			14,
			14,
			14,
			14,
			14,
			14,
			14,
			14,
			14,
			14,
			14,
			14,
			14,
			14,
			14,
			14,
			14,
			14,
			14,
			14,
			14,
			14,
			14,
			14,
			14,
			14,
			14,
			14,
			14,
			11,
			24,
			26,
			26,
			26,
			26,
			28,
			28,
			27,
			28,
			1,
			22,
			25,
			19,
			28,
			27,
			28,
			25,
			10,
			10,
			27,
			1,
			28,
			24,
			27,
			10,
			1,
			23,
			10,
			10,
			10,
			24,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			25,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			25,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1
		};

		// Token: 0x0400107C RID: 4220
		internal const int UNICODE_PLANE00_END = 65535;

		// Token: 0x0400107D RID: 4221
		internal const int UNICODE_PLANE01_START = 65536;

		// Token: 0x0400107E RID: 4222
		internal const int UNICODE_PLANE16_END = 1114111;

		// Token: 0x0400107F RID: 4223
		internal const int HIGH_SURROGATE_START = 55296;

		// Token: 0x04001080 RID: 4224
		internal const int LOW_SURROGATE_END = 57343;
	}
}

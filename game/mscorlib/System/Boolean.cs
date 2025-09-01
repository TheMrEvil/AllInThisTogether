using System;
using System.Runtime.Versioning;

namespace System
{
	/// <summary>Represents a Boolean (<see langword="true" /> or <see langword="false" />) value.</summary>
	// Token: 0x02000102 RID: 258
	[Serializable]
	public readonly struct Boolean : IComparable, IConvertible, IComparable<bool>, IEquatable<bool>
	{
		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>A hash code for the current <see cref="T:System.Boolean" />.</returns>
		// Token: 0x06000798 RID: 1944 RVA: 0x00021FDF File Offset: 0x000201DF
		public override int GetHashCode()
		{
			if (!this)
			{
				return 0;
			}
			return 1;
		}

		/// <summary>Converts the value of this instance to its equivalent string representation (either "True" or "False").</summary>
		/// <returns>"True" (the value of the <see cref="F:System.Boolean.TrueString" /> property) if the value of this instance is <see langword="true" />, or "False" (the value of the <see cref="F:System.Boolean.FalseString" /> property) if the value of this instance is <see langword="false" />.</returns>
		// Token: 0x06000799 RID: 1945 RVA: 0x00021FE8 File Offset: 0x000201E8
		public override string ToString()
		{
			if (!this)
			{
				return "False";
			}
			return "True";
		}

		/// <summary>Converts the value of this instance to its equivalent string representation (either "True" or "False").</summary>
		/// <param name="provider">(Reserved) An <see cref="T:System.IFormatProvider" /> object.</param>
		/// <returns>
		///   <see cref="F:System.Boolean.TrueString" /> if the value of this instance is <see langword="true" />, or <see cref="F:System.Boolean.FalseString" /> if the value of this instance is <see langword="false" />.</returns>
		// Token: 0x0600079A RID: 1946 RVA: 0x00021FF9 File Offset: 0x000201F9
		public string ToString(IFormatProvider provider)
		{
			return this.ToString();
		}

		// Token: 0x0600079B RID: 1947 RVA: 0x00022004 File Offset: 0x00020204
		public bool TryFormat(Span<char> destination, out int charsWritten)
		{
			string text = this ? "True" : "False";
			if (text.AsSpan().TryCopyTo(destination))
			{
				charsWritten = text.Length;
				return true;
			}
			charsWritten = 0;
			return false;
		}

		/// <summary>Returns a value indicating whether this instance is equal to a specified object.</summary>
		/// <param name="obj">An object to compare to this instance.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="obj" /> is a <see cref="T:System.Boolean" /> and has the same value as this instance; otherwise, <see langword="false" />.</returns>
		// Token: 0x0600079C RID: 1948 RVA: 0x00022041 File Offset: 0x00020241
		public override bool Equals(object obj)
		{
			return obj is bool && this == (bool)obj;
		}

		/// <summary>Returns a value indicating whether this instance is equal to a specified <see cref="T:System.Boolean" /> object.</summary>
		/// <param name="obj">A <see cref="T:System.Boolean" /> value to compare to this instance.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="obj" /> has the same value as this instance; otherwise, <see langword="false" />.</returns>
		// Token: 0x0600079D RID: 1949 RVA: 0x00022057 File Offset: 0x00020257
		[NonVersionable]
		public bool Equals(bool obj)
		{
			return this == obj;
		}

		/// <summary>Compares this instance to a specified object and returns an integer that indicates their relationship to one another.</summary>
		/// <param name="obj">An object to compare to this instance, or <see langword="null" />.</param>
		/// <returns>A signed integer that indicates the relative order of this instance and <paramref name="obj" />.  
		///   Return Value  
		///
		///   Condition  
		///
		///   Less than zero  
		///
		///   This instance is <see langword="false" /> and <paramref name="obj" /> is <see langword="true" />.  
		///
		///   Zero  
		///
		///   This instance and <paramref name="obj" /> are equal (either both are <see langword="true" /> or both are <see langword="false" />).  
		///
		///   Greater than zero  
		///
		///   This instance is <see langword="true" /> and <paramref name="obj" /> is <see langword="false" />.  
		///
		///  -or-  
		///
		///  <paramref name="obj" /> is <see langword="null" />.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="obj" /> is not a <see cref="T:System.Boolean" />.</exception>
		// Token: 0x0600079E RID: 1950 RVA: 0x0002205E File Offset: 0x0002025E
		public int CompareTo(object obj)
		{
			if (obj == null)
			{
				return 1;
			}
			if (!(obj is bool))
			{
				throw new ArgumentException("Object must be of type Boolean.");
			}
			if (this == (bool)obj)
			{
				return 0;
			}
			if (!this)
			{
				return -1;
			}
			return 1;
		}

		/// <summary>Compares this instance to a specified <see cref="T:System.Boolean" /> object and returns an integer that indicates their relationship to one another.</summary>
		/// <param name="value">A <see cref="T:System.Boolean" /> object to compare to this instance.</param>
		/// <returns>A signed integer that indicates the relative values of this instance and <paramref name="value" />.  
		///   Return Value  
		///
		///   Condition  
		///
		///   Less than zero  
		///
		///   This instance is <see langword="false" /> and <paramref name="value" /> is <see langword="true" />.  
		///
		///   Zero  
		///
		///   This instance and <paramref name="value" /> are equal (either both are <see langword="true" /> or both are <see langword="false" />).  
		///
		///   Greater than zero  
		///
		///   This instance is <see langword="true" /> and <paramref name="value" /> is <see langword="false" />.</returns>
		// Token: 0x0600079F RID: 1951 RVA: 0x0002208B File Offset: 0x0002028B
		public int CompareTo(bool value)
		{
			if (this == value)
			{
				return 0;
			}
			if (!this)
			{
				return -1;
			}
			return 1;
		}

		/// <summary>Converts the specified string representation of a logical value to its <see cref="T:System.Boolean" /> equivalent.</summary>
		/// <param name="value">A string containing the value to convert.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="value" /> is equivalent to <see cref="F:System.Boolean.TrueString" />; <see langword="false" /> if <paramref name="value" /> is equivalent to <see cref="F:System.Boolean.FalseString" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="value" /> is not equivalent to <see cref="F:System.Boolean.TrueString" /> or <see cref="F:System.Boolean.FalseString" />.</exception>
		// Token: 0x060007A0 RID: 1952 RVA: 0x0002209B File Offset: 0x0002029B
		public static bool Parse(string value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			return bool.Parse(value.AsSpan());
		}

		// Token: 0x060007A1 RID: 1953 RVA: 0x000220B8 File Offset: 0x000202B8
		public static bool Parse(ReadOnlySpan<char> value)
		{
			bool result;
			if (!bool.TryParse(value, out result))
			{
				throw new FormatException("String was not recognized as a valid Boolean.");
			}
			return result;
		}

		/// <summary>Tries to convert the specified string representation of a logical value to its <see cref="T:System.Boolean" /> equivalent. A return value indicates whether the conversion succeeded or failed.</summary>
		/// <param name="value">A string containing the value to convert.</param>
		/// <param name="result">When this method returns, if the conversion succeeded, contains <see langword="true" /> if <paramref name="value" /> is equal to <see cref="F:System.Boolean.TrueString" /> or <see langword="false" /> if <paramref name="value" /> is equal to <see cref="F:System.Boolean.FalseString" />. If the conversion failed, contains <see langword="false" />. The conversion fails if <paramref name="value" /> is <see langword="null" /> or is not equal to the value of either the <see cref="F:System.Boolean.TrueString" /> or <see cref="F:System.Boolean.FalseString" /> field.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="value" /> was converted successfully; otherwise, <see langword="false" />.</returns>
		// Token: 0x060007A2 RID: 1954 RVA: 0x000220DB File Offset: 0x000202DB
		public static bool TryParse(string value, out bool result)
		{
			if (value == null)
			{
				result = false;
				return false;
			}
			return bool.TryParse(value.AsSpan(), out result);
		}

		// Token: 0x060007A3 RID: 1955 RVA: 0x000220F4 File Offset: 0x000202F4
		public static bool TryParse(ReadOnlySpan<char> value, out bool result)
		{
			ReadOnlySpan<char> span = "True".AsSpan();
			if (span.EqualsOrdinalIgnoreCase(value))
			{
				result = true;
				return true;
			}
			ReadOnlySpan<char> span2 = "False".AsSpan();
			if (span2.EqualsOrdinalIgnoreCase(value))
			{
				result = false;
				return true;
			}
			value = bool.TrimWhiteSpaceAndNull(value);
			if (span.EqualsOrdinalIgnoreCase(value))
			{
				result = true;
				return true;
			}
			if (span2.EqualsOrdinalIgnoreCase(value))
			{
				result = false;
				return true;
			}
			result = false;
			return false;
		}

		// Token: 0x060007A4 RID: 1956 RVA: 0x0002215C File Offset: 0x0002035C
		private unsafe static ReadOnlySpan<char> TrimWhiteSpaceAndNull(ReadOnlySpan<char> value)
		{
			int num = 0;
			while (num < value.Length && (char.IsWhiteSpace((char)(*value[num])) || *value[num] == 0))
			{
				num++;
			}
			int num2 = value.Length - 1;
			while (num2 >= num && (char.IsWhiteSpace((char)(*value[num2])) || *value[num2] == 0))
			{
				num2--;
			}
			return value.Slice(num, num2 - num + 1);
		}

		/// <summary>Returns the type code for the <see cref="T:System.Boolean" /> value type.</summary>
		/// <returns>The enumerated constant <see cref="F:System.TypeCode.Boolean" />.</returns>
		// Token: 0x060007A5 RID: 1957 RVA: 0x000221D2 File Offset: 0x000203D2
		public TypeCode GetTypeCode()
		{
			return TypeCode.Boolean;
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToBoolean(System.IFormatProvider)" />.</summary>
		/// <param name="provider">This parameter is ignored.</param>
		/// <returns>
		///   <see langword="true" /> or <see langword="false" />.</returns>
		// Token: 0x060007A6 RID: 1958 RVA: 0x000221D5 File Offset: 0x000203D5
		bool IConvertible.ToBoolean(IFormatProvider provider)
		{
			return this;
		}

		/// <summary>This conversion is not supported. Attempting to use this method throws an <see cref="T:System.InvalidCastException" />.</summary>
		/// <param name="provider">This parameter is ignored.</param>
		/// <returns>This conversion is not supported. No value is returned.</returns>
		/// <exception cref="T:System.InvalidCastException">You attempt to convert a <see cref="T:System.Boolean" /> value to a <see cref="T:System.Char" /> value. This conversion is not supported.</exception>
		// Token: 0x060007A7 RID: 1959 RVA: 0x000221D9 File Offset: 0x000203D9
		char IConvertible.ToChar(IFormatProvider provider)
		{
			throw new InvalidCastException(SR.Format("Invalid cast from '{0}' to '{1}'.", "Boolean", "Char"));
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToSByte(System.IFormatProvider)" />.</summary>
		/// <param name="provider">This parameter is ignored.</param>
		/// <returns>1 if this instance is <see langword="true" />; otherwise, 0.</returns>
		// Token: 0x060007A8 RID: 1960 RVA: 0x000221F4 File Offset: 0x000203F4
		sbyte IConvertible.ToSByte(IFormatProvider provider)
		{
			return Convert.ToSByte(this);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToByte(System.IFormatProvider)" />.</summary>
		/// <param name="provider">This parameter is ignored.</param>
		/// <returns>1 if the value of this instance is <see langword="true" />; otherwise, 0.</returns>
		// Token: 0x060007A9 RID: 1961 RVA: 0x000221FD File Offset: 0x000203FD
		byte IConvertible.ToByte(IFormatProvider provider)
		{
			return Convert.ToByte(this);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToInt16(System.IFormatProvider)" />.</summary>
		/// <param name="provider">This parameter is ignored.</param>
		/// <returns>1 if this instance is <see langword="true" />; otherwise, 0.</returns>
		// Token: 0x060007AA RID: 1962 RVA: 0x00022206 File Offset: 0x00020406
		short IConvertible.ToInt16(IFormatProvider provider)
		{
			return Convert.ToInt16(this);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToUInt16(System.IFormatProvider)" />.</summary>
		/// <param name="provider">This parameter is ignored.</param>
		/// <returns>1 if this instance is <see langword="true" />; otherwise, 0.</returns>
		// Token: 0x060007AB RID: 1963 RVA: 0x0002220F File Offset: 0x0002040F
		ushort IConvertible.ToUInt16(IFormatProvider provider)
		{
			return Convert.ToUInt16(this);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToInt32(System.IFormatProvider)" />.</summary>
		/// <param name="provider">This parameter is ignored.</param>
		/// <returns>1 if this instance is <see langword="true" />; otherwise, 0.</returns>
		// Token: 0x060007AC RID: 1964 RVA: 0x00022218 File Offset: 0x00020418
		int IConvertible.ToInt32(IFormatProvider provider)
		{
			return Convert.ToInt32(this);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToUInt32(System.IFormatProvider)" />.</summary>
		/// <param name="provider">This parameter is ignored.</param>
		/// <returns>1 if this instance is <see langword="true" />; otherwise, 0.</returns>
		// Token: 0x060007AD RID: 1965 RVA: 0x00022221 File Offset: 0x00020421
		uint IConvertible.ToUInt32(IFormatProvider provider)
		{
			return Convert.ToUInt32(this);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToInt64(System.IFormatProvider)" />.</summary>
		/// <param name="provider">This parameter is ignored.</param>
		/// <returns>1 if this instance is <see langword="true" />; otherwise, 0.</returns>
		// Token: 0x060007AE RID: 1966 RVA: 0x0002222A File Offset: 0x0002042A
		long IConvertible.ToInt64(IFormatProvider provider)
		{
			return Convert.ToInt64(this);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToUInt64(System.IFormatProvider)" />.</summary>
		/// <param name="provider">This parameter is ignored.</param>
		/// <returns>1 if this instance is <see langword="true" />; otherwise, 0.</returns>
		// Token: 0x060007AF RID: 1967 RVA: 0x00022233 File Offset: 0x00020433
		ulong IConvertible.ToUInt64(IFormatProvider provider)
		{
			return Convert.ToUInt64(this);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToSingle(System.IFormatProvider)" />.</summary>
		/// <param name="provider">This parameter is ignored.</param>
		/// <returns>1 if this instance is <see langword="true" />; otherwise, 0.</returns>
		// Token: 0x060007B0 RID: 1968 RVA: 0x0002223C File Offset: 0x0002043C
		float IConvertible.ToSingle(IFormatProvider provider)
		{
			return Convert.ToSingle(this);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToDouble(System.IFormatProvider)" />.</summary>
		/// <param name="provider">This parameter is ignored.</param>
		/// <returns>1 if this instance is <see langword="true" />; otherwise, 0.</returns>
		// Token: 0x060007B1 RID: 1969 RVA: 0x00022245 File Offset: 0x00020445
		double IConvertible.ToDouble(IFormatProvider provider)
		{
			return Convert.ToDouble(this);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToDecimal(System.IFormatProvider)" />.</summary>
		/// <param name="provider">This parameter is ignored.</param>
		/// <returns>1 if this instance is <see langword="true" />; otherwise, 0.</returns>
		// Token: 0x060007B2 RID: 1970 RVA: 0x0002224E File Offset: 0x0002044E
		decimal IConvertible.ToDecimal(IFormatProvider provider)
		{
			return Convert.ToDecimal(this);
		}

		/// <summary>This conversion is not supported. Attempting to use this method throws an <see cref="T:System.InvalidCastException" />.</summary>
		/// <param name="provider">This parameter is ignored.</param>
		/// <returns>This conversion is not supported. No value is returned.</returns>
		/// <exception cref="T:System.InvalidCastException">You attempt to convert a <see cref="T:System.Boolean" /> value to a <see cref="T:System.DateTime" /> value. This conversion is not supported.</exception>
		// Token: 0x060007B3 RID: 1971 RVA: 0x00022257 File Offset: 0x00020457
		DateTime IConvertible.ToDateTime(IFormatProvider provider)
		{
			throw new InvalidCastException(SR.Format("Invalid cast from '{0}' to '{1}'.", "Boolean", "DateTime"));
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToType(System.Type,System.IFormatProvider)" />.</summary>
		/// <param name="type">The desired type.</param>
		/// <param name="provider">An <see cref="T:System.IFormatProvider" /> implementation that supplies culture-specific information about the format of the returned value.</param>
		/// <returns>An object of the specified type, with a value that is equivalent to the value of this <see langword="Boolean" /> object.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="type" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidCastException">The requested type conversion is not supported.</exception>
		// Token: 0x060007B4 RID: 1972 RVA: 0x00022272 File Offset: 0x00020472
		object IConvertible.ToType(Type type, IFormatProvider provider)
		{
			return Convert.DefaultToType(this, type, provider);
		}

		// Token: 0x060007B5 RID: 1973 RVA: 0x00022282 File Offset: 0x00020482
		// Note: this type is marked as 'beforefieldinit'.
		static Boolean()
		{
		}

		// Token: 0x0400106D RID: 4205
		private readonly bool m_value;

		// Token: 0x0400106E RID: 4206
		internal const int True = 1;

		// Token: 0x0400106F RID: 4207
		internal const int False = 0;

		// Token: 0x04001070 RID: 4208
		internal const string TrueLiteral = "True";

		// Token: 0x04001071 RID: 4209
		internal const string FalseLiteral = "False";

		/// <summary>Represents the Boolean value <see langword="true" /> as a string. This field is read-only.</summary>
		// Token: 0x04001072 RID: 4210
		public static readonly string TrueString = "True";

		/// <summary>Represents the Boolean value <see langword="false" /> as a string. This field is read-only.</summary>
		// Token: 0x04001073 RID: 4211
		public static readonly string FalseString = "False";
	}
}

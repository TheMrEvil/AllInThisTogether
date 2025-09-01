using System;
using System.Data.Common;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace System.Data.SqlTypes
{
	/// <summary>Represents a 32-bit signed integer to be stored in or retrieved from a database.</summary>
	// Token: 0x02000312 RID: 786
	[XmlSchemaProvider("GetXsdType")]
	[Serializable]
	public struct SqlInt32 : INullable, IComparable, IXmlSerializable
	{
		// Token: 0x0600248A RID: 9354 RVA: 0x000A65CB File Offset: 0x000A47CB
		private SqlInt32(bool fNull)
		{
			this.m_fNotNull = false;
			this.m_value = 0;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlTypes.SqlInt32" /> structure using the supplied integer value.</summary>
		/// <param name="value">The integer to be converted.</param>
		// Token: 0x0600248B RID: 9355 RVA: 0x000A65DB File Offset: 0x000A47DB
		public SqlInt32(int value)
		{
			this.m_value = value;
			this.m_fNotNull = true;
		}

		/// <summary>Indicates whether this <see cref="T:System.Data.SqlTypes.SqlInt32" /> structure is null.</summary>
		/// <returns>This property is <see langword="true" /> if <see cref="P:System.Data.SqlTypes.SqlInt32.Value" /> is null. Otherwise, <see langword="false" />.</returns>
		// Token: 0x17000661 RID: 1633
		// (get) Token: 0x0600248C RID: 9356 RVA: 0x000A65EB File Offset: 0x000A47EB
		public bool IsNull
		{
			get
			{
				return !this.m_fNotNull;
			}
		}

		/// <summary>Gets the value of this <see cref="T:System.Data.SqlTypes.SqlInt32" /> structure. This property is read-only.</summary>
		/// <returns>An integer representing the value of this <see cref="T:System.Data.SqlTypes.SqlInt32" /> structure.</returns>
		/// <exception cref="T:System.Data.SqlTypes.SqlNullValueException">The property contains <see cref="F:System.Data.SqlTypes.SqlInt32.Null" />.</exception>
		// Token: 0x17000662 RID: 1634
		// (get) Token: 0x0600248D RID: 9357 RVA: 0x000A65F6 File Offset: 0x000A47F6
		public int Value
		{
			get
			{
				if (this.IsNull)
				{
					throw new SqlNullValueException();
				}
				return this.m_value;
			}
		}

		/// <summary>Converts the supplied integer to <see cref="T:System.Data.SqlTypes.SqlInt32" />.</summary>
		/// <param name="x">An integer value.</param>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlInt32" /> structure whose Value property is equal to the integer parameter.</returns>
		// Token: 0x0600248E RID: 9358 RVA: 0x000A660C File Offset: 0x000A480C
		public static implicit operator SqlInt32(int x)
		{
			return new SqlInt32(x);
		}

		/// <summary>Converts the supplied <see cref="T:System.Data.SqlTypes.SqlInt32" /> structure to an integer.</summary>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlInt32" /> structure.</param>
		/// <returns>The converted integer value.</returns>
		// Token: 0x0600248F RID: 9359 RVA: 0x000A6614 File Offset: 0x000A4814
		public static explicit operator int(SqlInt32 x)
		{
			return x.Value;
		}

		/// <summary>Converts a <see cref="T:System.Data.SqlTypes.SqlInt32" /> structure to a <see cref="T:System.String" />.</summary>
		/// <returns>A <see cref="T:System.String" /> structure equal to the value of this <see cref="T:System.Data.SqlTypes.SqlInt32" />.</returns>
		// Token: 0x06002490 RID: 9360 RVA: 0x000A661D File Offset: 0x000A481D
		public override string ToString()
		{
			if (!this.IsNull)
			{
				return this.m_value.ToString(null);
			}
			return SQLResource.NullString;
		}

		/// <summary>Converts the <see cref="T:System.String" /> representation of a number to its 32-bit signed integer equivalent.</summary>
		/// <param name="s">The <see cref="T:System.String" /> to be parsed.</param>
		/// <returns>A 32-bit signed integer equivalent to the value that is contained in the specified <see cref="T:System.String" />.</returns>
		// Token: 0x06002491 RID: 9361 RVA: 0x000A6639 File Offset: 0x000A4839
		public static SqlInt32 Parse(string s)
		{
			if (s == SQLResource.NullString)
			{
				return SqlInt32.Null;
			}
			return new SqlInt32(int.Parse(s, null));
		}

		/// <summary>Negates the <see cref="P:System.Data.SqlTypes.SqlInt32.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlInt32" /> operand.</summary>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlInt32" /> structure.</param>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlInt32" /> structure that contains the negated value.</returns>
		// Token: 0x06002492 RID: 9362 RVA: 0x000A665A File Offset: 0x000A485A
		public static SqlInt32 operator -(SqlInt32 x)
		{
			if (!x.IsNull)
			{
				return new SqlInt32(-x.m_value);
			}
			return SqlInt32.Null;
		}

		/// <summary>Performs a bitwise one's complement operation on the specified <see cref="T:System.Data.SqlTypes.SqlInt32" /> structure.</summary>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlInt32" /> structure.</param>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlInt32" /> structure that contains the results of the one's complement operation.</returns>
		// Token: 0x06002493 RID: 9363 RVA: 0x000A6677 File Offset: 0x000A4877
		public static SqlInt32 operator ~(SqlInt32 x)
		{
			if (!x.IsNull)
			{
				return new SqlInt32(~x.m_value);
			}
			return SqlInt32.Null;
		}

		/// <summary>Computes the sum of the two specified <see cref="T:System.Data.SqlTypes.SqlInt32" /> structures.</summary>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlInt32" /> structure.</param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlInt32" /> structure.</param>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlInt32" /> structure whose <see cref="P:System.Data.SqlTypes.SqlInt32.Value" /> property contains the sum of the specified <see cref="T:System.Data.SqlTypes.SqlInt32" /> structures.</returns>
		// Token: 0x06002494 RID: 9364 RVA: 0x000A6694 File Offset: 0x000A4894
		public static SqlInt32 operator +(SqlInt32 x, SqlInt32 y)
		{
			if (x.IsNull || y.IsNull)
			{
				return SqlInt32.Null;
			}
			int num = x.m_value + y.m_value;
			if (SqlInt32.SameSignInt(x.m_value, y.m_value) && !SqlInt32.SameSignInt(x.m_value, num))
			{
				throw new OverflowException(SQLResource.ArithOverflowMessage);
			}
			return new SqlInt32(num);
		}

		/// <summary>Subtracts the second <see cref="T:System.Data.SqlTypes.SqlInt32" /> parameter from the first.</summary>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlInt32" /> structure.</param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlInt32" /> structure.</param>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlInt32" /> structure whose <see cref="P:System.Data.SqlTypes.SqlInt32.Value" /> property contains the results of the subtraction.</returns>
		// Token: 0x06002495 RID: 9365 RVA: 0x000A66FC File Offset: 0x000A48FC
		public static SqlInt32 operator -(SqlInt32 x, SqlInt32 y)
		{
			if (x.IsNull || y.IsNull)
			{
				return SqlInt32.Null;
			}
			int num = x.m_value - y.m_value;
			if (!SqlInt32.SameSignInt(x.m_value, y.m_value) && SqlInt32.SameSignInt(y.m_value, num))
			{
				throw new OverflowException(SQLResource.ArithOverflowMessage);
			}
			return new SqlInt32(num);
		}

		/// <summary>Computes the product of the two <see cref="T:System.Data.SqlTypes.SqlInt32" /> parameters.</summary>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlInt32" /> structure.</param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlInt32" /> structure.</param>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlInt32" /> structure whose <see cref="P:System.Data.SqlTypes.SqlInt32.Value" /> contains the product of the two parameters.</returns>
		// Token: 0x06002496 RID: 9366 RVA: 0x000A6764 File Offset: 0x000A4964
		public static SqlInt32 operator *(SqlInt32 x, SqlInt32 y)
		{
			if (x.IsNull || y.IsNull)
			{
				return SqlInt32.Null;
			}
			long num = (long)x.m_value * (long)y.m_value;
			long num2 = num & SqlInt32.s_lBitNotIntMax;
			if (num2 != 0L && num2 != SqlInt32.s_lBitNotIntMax)
			{
				throw new OverflowException(SQLResource.ArithOverflowMessage);
			}
			return new SqlInt32((int)num);
		}

		/// <summary>Divides the first <see cref="T:System.Data.SqlTypes.SqlInt32" /> parameter from the second.</summary>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlInt32" /> structure.</param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlInt32" /> structure.</param>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlInt32" /> whose <see cref="P:System.Data.SqlTypes.SqlInt32.Value" /> property contains the results of the division.</returns>
		// Token: 0x06002497 RID: 9367 RVA: 0x000A67C0 File Offset: 0x000A49C0
		public static SqlInt32 operator /(SqlInt32 x, SqlInt32 y)
		{
			if (x.IsNull || y.IsNull)
			{
				return SqlInt32.Null;
			}
			if (y.m_value == 0)
			{
				throw new DivideByZeroException(SQLResource.DivideByZeroMessage);
			}
			if ((long)x.m_value == SqlInt32.s_iIntMin && y.m_value == -1)
			{
				throw new OverflowException(SQLResource.ArithOverflowMessage);
			}
			return new SqlInt32(x.m_value / y.m_value);
		}

		/// <summary>Computes the remainder after dividing the first <see cref="T:System.Data.SqlTypes.SqlInt32" /> parameter by the second.</summary>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlInt32" /> structure.</param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlInt32" /> structure.</param>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlInt32" /> structure whose <see cref="P:System.Data.SqlTypes.SqlInt32.Value" /> contains the remainder.</returns>
		// Token: 0x06002498 RID: 9368 RVA: 0x000A682C File Offset: 0x000A4A2C
		public static SqlInt32 operator %(SqlInt32 x, SqlInt32 y)
		{
			if (x.IsNull || y.IsNull)
			{
				return SqlInt32.Null;
			}
			if (y.m_value == 0)
			{
				throw new DivideByZeroException(SQLResource.DivideByZeroMessage);
			}
			if ((long)x.m_value == SqlInt32.s_iIntMin && y.m_value == -1)
			{
				throw new OverflowException(SQLResource.ArithOverflowMessage);
			}
			return new SqlInt32(x.m_value % y.m_value);
		}

		/// <summary>Computes the bitwise AND of its <see cref="T:System.Data.SqlTypes.SqlInt32" /> operands.</summary>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlInt32" /> structure.</param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlInt32" /> structure.</param>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlInt32" /> structure that contains the results of the bitwise AND operation.</returns>
		// Token: 0x06002499 RID: 9369 RVA: 0x000A6898 File Offset: 0x000A4A98
		public static SqlInt32 operator &(SqlInt32 x, SqlInt32 y)
		{
			if (!x.IsNull && !y.IsNull)
			{
				return new SqlInt32(x.m_value & y.m_value);
			}
			return SqlInt32.Null;
		}

		/// <summary>Computes the bitwise OR of the specified <see cref="T:System.Data.SqlTypes.SqlInt32" /> structures.</summary>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlInt32" /> structure.</param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlInt32" /> structure.</param>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlInt32" /> structure that contains the results of the bitwise OR operation.</returns>
		// Token: 0x0600249A RID: 9370 RVA: 0x000A68C4 File Offset: 0x000A4AC4
		public static SqlInt32 operator |(SqlInt32 x, SqlInt32 y)
		{
			if (!x.IsNull && !y.IsNull)
			{
				return new SqlInt32(x.m_value | y.m_value);
			}
			return SqlInt32.Null;
		}

		/// <summary>Performs a bitwise exclusive-OR operation on the specified <see cref="T:System.Data.SqlTypes.SqlInt32" /> structures.</summary>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlInt32" /> structure.</param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlInt32" /> structure.</param>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlInt32" /> structure that contains the results of the bitwise XOR operation.</returns>
		// Token: 0x0600249B RID: 9371 RVA: 0x000A68F0 File Offset: 0x000A4AF0
		public static SqlInt32 operator ^(SqlInt32 x, SqlInt32 y)
		{
			if (!x.IsNull && !y.IsNull)
			{
				return new SqlInt32(x.m_value ^ y.m_value);
			}
			return SqlInt32.Null;
		}

		/// <summary>Converts the supplied <see cref="T:System.Data.SqlTypes.SqlBoolean" /> to <see cref="T:System.Data.SqlTypes.SqlInt32" />.</summary>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure.</param>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlInt32" /> structure whose <see cref="P:System.Data.SqlTypes.SqlInt32.Value" /> property is equal to the <see cref="P:System.Data.SqlTypes.SqlBoolean.ByteValue" /> property of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> parameter.</returns>
		// Token: 0x0600249C RID: 9372 RVA: 0x000A691C File Offset: 0x000A4B1C
		public static explicit operator SqlInt32(SqlBoolean x)
		{
			if (!x.IsNull)
			{
				return new SqlInt32((int)x.ByteValue);
			}
			return SqlInt32.Null;
		}

		/// <summary>Converts the supplied <see cref="T:System.Data.SqlTypes.SqlByte" /> property to <see cref="T:System.Data.SqlTypes.SqlInt32" />.</summary>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlByte" /> structure.</param>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlInt32" /> structure whose <see cref="P:System.Data.SqlTypes.SqlInt32.Value" /> property equals the <see cref="P:System.Data.SqlTypes.SqlByte.Value" /> property of the <see cref="T:System.Data.SqlTypes.SqlByte" /> parameter.</returns>
		// Token: 0x0600249D RID: 9373 RVA: 0x000A6939 File Offset: 0x000A4B39
		public static implicit operator SqlInt32(SqlByte x)
		{
			if (!x.IsNull)
			{
				return new SqlInt32((int)x.Value);
			}
			return SqlInt32.Null;
		}

		/// <summary>Converts the supplied <see cref="T:System.Data.SqlTypes.SqlInt16" /> to <see cref="T:System.Data.SqlTypes.SqlInt32" />.</summary>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlInt16" /> structure.</param>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlInt32" /> structure whose <see cref="P:System.Data.SqlTypes.SqlInt32.Value" /> property equals the <see cref="P:System.Data.SqlTypes.SqlInt16.Value" /> property of the <see cref="T:System.Data.SqlTypes.SqlInt16" /> parameter.</returns>
		// Token: 0x0600249E RID: 9374 RVA: 0x000A6956 File Offset: 0x000A4B56
		public static implicit operator SqlInt32(SqlInt16 x)
		{
			if (!x.IsNull)
			{
				return new SqlInt32((int)x.Value);
			}
			return SqlInt32.Null;
		}

		/// <summary>Converts the supplied <see cref="T:System.Data.SqlTypes.SqlInt64" /> to <see cref="T:System.Data.SqlTypes.SqlInt32" />.</summary>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlInt64" /> structure.</param>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlInt32" /> structure whose <see cref="P:System.Data.SqlTypes.SqlInt32.Value" /> property equals the <see cref="P:System.Data.SqlTypes.SqlInt64.Value" /> property of the <see langword="SqlInt64" /> parameter.</returns>
		// Token: 0x0600249F RID: 9375 RVA: 0x000A6974 File Offset: 0x000A4B74
		public static explicit operator SqlInt32(SqlInt64 x)
		{
			if (x.IsNull)
			{
				return SqlInt32.Null;
			}
			long value = x.Value;
			if (value > 2147483647L || value < -2147483648L)
			{
				throw new OverflowException(SQLResource.ArithOverflowMessage);
			}
			return new SqlInt32((int)value);
		}

		/// <summary>Converts the supplied <see cref="T:System.Data.SqlTypes.SqlSingle" /> to <see cref="T:System.Data.SqlTypes.SqlInt32" />.</summary>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlSingle" /> structure.</param>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlInt32" /> structure whose <see cref="P:System.Data.SqlTypes.SqlInt32.Value" /> property equals the integer part of the <see cref="T:System.Data.SqlTypes.SqlSingle" /> parameter.</returns>
		// Token: 0x060024A0 RID: 9376 RVA: 0x000A69BC File Offset: 0x000A4BBC
		public static explicit operator SqlInt32(SqlSingle x)
		{
			if (x.IsNull)
			{
				return SqlInt32.Null;
			}
			float value = x.Value;
			if (value > 2.1474836E+09f || value < -2.1474836E+09f)
			{
				throw new OverflowException(SQLResource.ArithOverflowMessage);
			}
			return new SqlInt32((int)value);
		}

		/// <summary>Converts the supplied <see cref="T:System.Data.SqlTypes.SqlDouble" /> to <see cref="T:System.Data.SqlTypes.SqlInt32" />.</summary>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlDouble" /> structure.</param>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlInt32" /> structure whose <see cref="P:System.Data.SqlTypes.SqlInt32.Value" /> property equals the integer part of the <see cref="T:System.Data.SqlTypes.SqlDouble" /> parameter.</returns>
		// Token: 0x060024A1 RID: 9377 RVA: 0x000A6A04 File Offset: 0x000A4C04
		public static explicit operator SqlInt32(SqlDouble x)
		{
			if (x.IsNull)
			{
				return SqlInt32.Null;
			}
			double value = x.Value;
			if (value > 2147483647.0 || value < -2147483648.0)
			{
				throw new OverflowException(SQLResource.ArithOverflowMessage);
			}
			return new SqlInt32((int)value);
		}

		/// <summary>Converts the supplied <see cref="T:System.Data.SqlTypes.SqlMoney" /> structure to <see cref="T:System.Data.SqlTypes.SqlInt32" />.</summary>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlMoney" /> structure.</param>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlInt32" /> structure whose <see cref="P:System.Data.SqlTypes.SqlInt32.Value" /> property equals the <see cref="P:System.Data.SqlTypes.SqlMoney.Value" /> property of the <see cref="T:System.Data.SqlTypes.SqlMoney" /> parameter.</returns>
		// Token: 0x060024A2 RID: 9378 RVA: 0x000A6A52 File Offset: 0x000A4C52
		public static explicit operator SqlInt32(SqlMoney x)
		{
			if (!x.IsNull)
			{
				return new SqlInt32(x.ToInt32());
			}
			return SqlInt32.Null;
		}

		/// <summary>Converts the supplied <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure to <see cref="T:System.Data.SqlTypes.SqlInt32" />.</summary>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure.</param>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlInt32" /> structure whose <see cref="P:System.Data.SqlTypes.SqlInt32.Value" /> property equals the <see cref="P:System.Data.SqlTypes.SqlDecimal.Value" /> property of the <see cref="T:System.Data.SqlTypes.SqlDecimal" /> parameter.</returns>
		// Token: 0x060024A3 RID: 9379 RVA: 0x000A6A70 File Offset: 0x000A4C70
		public static explicit operator SqlInt32(SqlDecimal x)
		{
			if (x.IsNull)
			{
				return SqlInt32.Null;
			}
			x.AdjustScale((int)(-(int)x.Scale), true);
			long num = (long)((ulong)x._data1);
			if (!x.IsPositive)
			{
				num = -num;
			}
			if (x._bLen > 1 || num > 2147483647L || num < -2147483648L)
			{
				throw new OverflowException(SQLResource.ConversionOverflowMessage);
			}
			return new SqlInt32((int)num);
		}

		/// <summary>Converts the supplied <see cref="T:System.Data.SqlTypes.SqlString" /> object to <see cref="T:System.Data.SqlTypes.SqlInt32" />.</summary>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlString" /> object.</param>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlInt32" /> structure whose <see cref="P:System.Data.SqlTypes.SqlInt32.Value" /> property equals the value represented by the <see cref="T:System.Data.SqlTypes.SqlString" /> parameter.</returns>
		// Token: 0x060024A4 RID: 9380 RVA: 0x000A6ADD File Offset: 0x000A4CDD
		public static explicit operator SqlInt32(SqlString x)
		{
			if (!x.IsNull)
			{
				return new SqlInt32(int.Parse(x.Value, null));
			}
			return SqlInt32.Null;
		}

		// Token: 0x060024A5 RID: 9381 RVA: 0x000A6B00 File Offset: 0x000A4D00
		private static bool SameSignInt(int x, int y)
		{
			return ((long)(x ^ y) & (long)((ulong)int.MinValue)) == 0L;
		}

		/// <summary>Performs a logical comparison of the two <see cref="T:System.Data.SqlTypes.SqlInt32" /> parameters to determine whether they are equal.</summary>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlInt32" /> structure.</param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlInt32" /> structure.</param>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> that is <see cref="F:System.Data.SqlTypes.SqlBoolean.True" /> if the two instances are equal or <see cref="F:System.Data.SqlTypes.SqlBoolean.False" /> if the two instances are not equal. If either instance of <see cref="T:System.Data.SqlTypes.SqlInt32" /> is null, the <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> will be <see cref="F:System.Data.SqlTypes.SqlBoolean.Null" />.</returns>
		// Token: 0x060024A6 RID: 9382 RVA: 0x000A6B11 File Offset: 0x000A4D11
		public static SqlBoolean operator ==(SqlInt32 x, SqlInt32 y)
		{
			if (!x.IsNull && !y.IsNull)
			{
				return new SqlBoolean(x.m_value == y.m_value);
			}
			return SqlBoolean.Null;
		}

		/// <summary>Performa a logical comparison of the two <see cref="T:System.Data.SqlTypes.SqlInt32" /> parameters to determine whether they are not equal.</summary>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlInt32" /> structure.</param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlInt32" /> structure.</param>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> that is <see cref="F:System.Data.SqlTypes.SqlBoolean.True" /> if the two instances are not equal or <see cref="F:System.Data.SqlTypes.SqlBoolean.False" /> if the two instances are equal. If either instance of <see cref="T:System.Data.SqlTypes.SqlInt32" /> is null, the <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> will be <see cref="F:System.Data.SqlTypes.SqlBoolean.Null" />.</returns>
		// Token: 0x060024A7 RID: 9383 RVA: 0x000A6B3E File Offset: 0x000A4D3E
		public static SqlBoolean operator !=(SqlInt32 x, SqlInt32 y)
		{
			return !(x == y);
		}

		/// <summary>Compares the two <see cref="T:System.Data.SqlTypes.SqlInt32" /> parameters to determine whether the first is less than the second.</summary>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlInt32" /> structure.</param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlInt32" /> structure.</param>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> that is <see cref="F:System.Data.SqlTypes.SqlBoolean.True" /> if the first instance is less than the second instance. Otherwise, <see cref="F:System.Data.SqlTypes.SqlBoolean.False" />. If either instance of <see cref="T:System.Data.SqlTypes.SqlInt32" /> is null, the <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> will be <see cref="F:System.Data.SqlTypes.SqlBoolean.Null" />.</returns>
		// Token: 0x060024A8 RID: 9384 RVA: 0x000A6B4C File Offset: 0x000A4D4C
		public static SqlBoolean operator <(SqlInt32 x, SqlInt32 y)
		{
			if (!x.IsNull && !y.IsNull)
			{
				return new SqlBoolean(x.m_value < y.m_value);
			}
			return SqlBoolean.Null;
		}

		/// <summary>Compares the two <see cref="T:System.Data.SqlTypes.SqlInt32" /> parameters to determine whether the first is greater than the second.</summary>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlInt32" /> structure.</param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlInt32" /> structure.</param>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> that is <see cref="F:System.Data.SqlTypes.SqlBoolean.True" /> if the first instance is greater than the second instance. Otherwise, <see cref="F:System.Data.SqlTypes.SqlBoolean.False" />. If either instance of <see cref="T:System.Data.SqlTypes.SqlInt32" /> is null, the <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> will be <see cref="F:System.Data.SqlTypes.SqlBoolean.Null" />.</returns>
		// Token: 0x060024A9 RID: 9385 RVA: 0x000A6B79 File Offset: 0x000A4D79
		public static SqlBoolean operator >(SqlInt32 x, SqlInt32 y)
		{
			if (!x.IsNull && !y.IsNull)
			{
				return new SqlBoolean(x.m_value > y.m_value);
			}
			return SqlBoolean.Null;
		}

		/// <summary>Compares the two <see cref="T:System.Data.SqlTypes.SqlInt32" /> parameters to determine whether the first is less than or equal to the second.</summary>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlInt32" /> structure.</param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlInt32" /> structure.</param>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> that is <see cref="F:System.Data.SqlTypes.SqlBoolean.True" /> if the first instance is less than or equal to the second instance. Otherwise, <see cref="F:System.Data.SqlTypes.SqlBoolean.False" />. If either instance of <see cref="T:System.Data.SqlTypes.SqlInt32" /> is null, the <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> will be <see cref="F:System.Data.SqlTypes.SqlBoolean.Null" />.</returns>
		// Token: 0x060024AA RID: 9386 RVA: 0x000A6BA6 File Offset: 0x000A4DA6
		public static SqlBoolean operator <=(SqlInt32 x, SqlInt32 y)
		{
			if (!x.IsNull && !y.IsNull)
			{
				return new SqlBoolean(x.m_value <= y.m_value);
			}
			return SqlBoolean.Null;
		}

		/// <summary>Compares the two <see cref="T:System.Data.SqlTypes.SqlInt32" /> parameters to determine whether the first is greater than or equal to the second.</summary>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlInt32" /> structure.</param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlInt32" /> structure.</param>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> that is <see cref="F:System.Data.SqlTypes.SqlBoolean.True" /> if the first instance is greater than or equal to the second instance. Otherwise, <see cref="F:System.Data.SqlTypes.SqlBoolean.False" />. If either instance of <see cref="T:System.Data.SqlTypes.SqlInt32" /> is null, the <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> will be <see cref="F:System.Data.SqlTypes.SqlBoolean.Null" />.</returns>
		// Token: 0x060024AB RID: 9387 RVA: 0x000A6BD6 File Offset: 0x000A4DD6
		public static SqlBoolean operator >=(SqlInt32 x, SqlInt32 y)
		{
			if (!x.IsNull && !y.IsNull)
			{
				return new SqlBoolean(x.m_value >= y.m_value);
			}
			return SqlBoolean.Null;
		}

		/// <summary>Performs a bitwise one's complement operation on the specified <see cref="T:System.Data.SqlTypes.SqlInt32" /> structure.</summary>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlInt32" /> structure.</param>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlInt32" /> structure that contains the results of the one's complement operation.</returns>
		// Token: 0x060024AC RID: 9388 RVA: 0x000A6C06 File Offset: 0x000A4E06
		public static SqlInt32 OnesComplement(SqlInt32 x)
		{
			return ~x;
		}

		/// <summary>Computes the sum of the two specified <see cref="T:System.Data.SqlTypes.SqlInt32" /> structures.</summary>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlInt32" /> structure.</param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlInt32" /> structure.</param>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlInt32" /> structure whose <see cref="P:System.Data.SqlTypes.SqlInt32.Value" /> property contains the sum of the specified <see cref="T:System.Data.SqlTypes.SqlInt32" /> structures.</returns>
		// Token: 0x060024AD RID: 9389 RVA: 0x000A6C0E File Offset: 0x000A4E0E
		public static SqlInt32 Add(SqlInt32 x, SqlInt32 y)
		{
			return x + y;
		}

		/// <summary>Subtracts the second <see cref="T:System.Data.SqlTypes.SqlInt32" /> parameter from the first.</summary>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlInt32" /> structure.</param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlInt32" /> structure.</param>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlInt32" /> structure whose <see cref="P:System.Data.SqlTypes.SqlInt32.Value" /> property contains the results of the subtraction.</returns>
		// Token: 0x060024AE RID: 9390 RVA: 0x000A6C17 File Offset: 0x000A4E17
		public static SqlInt32 Subtract(SqlInt32 x, SqlInt32 y)
		{
			return x - y;
		}

		/// <summary>Computes the product of the two <see cref="T:System.Data.SqlTypes.SqlInt32" /> parameters.</summary>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlInt32" /> structure.</param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlInt32" /> structure.</param>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlInt32" /> structure whose <see cref="P:System.Data.SqlTypes.SqlInt32.Value" /> contains the product of the two parameters.</returns>
		// Token: 0x060024AF RID: 9391 RVA: 0x000A6C20 File Offset: 0x000A4E20
		public static SqlInt32 Multiply(SqlInt32 x, SqlInt32 y)
		{
			return x * y;
		}

		/// <summary>Divides the first <see cref="T:System.Data.SqlTypes.SqlInt32" /> parameter from the second.</summary>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlInt32" /> structure.</param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlInt32" /> structure.</param>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlInt32" /> whose <see cref="P:System.Data.SqlTypes.SqlInt32.Value" /> property contains the results of the division.</returns>
		// Token: 0x060024B0 RID: 9392 RVA: 0x000A6C29 File Offset: 0x000A4E29
		public static SqlInt32 Divide(SqlInt32 x, SqlInt32 y)
		{
			return x / y;
		}

		/// <summary>Computes the remainder after dividing the first <see cref="T:System.Data.SqlTypes.SqlInt32" /> parameter by the second.</summary>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlInt32" /> structure.</param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlInt32" /> structure.</param>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlInt32" /> structure whose <see cref="P:System.Data.SqlTypes.SqlInt32.Value" /> contains the remainder.</returns>
		// Token: 0x060024B1 RID: 9393 RVA: 0x000A6C32 File Offset: 0x000A4E32
		public static SqlInt32 Mod(SqlInt32 x, SqlInt32 y)
		{
			return x % y;
		}

		/// <summary>Divides two <see cref="T:System.Data.SqlTypes.SqlInt32" /> values and returns the remainder.</summary>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlInt32" /> value.</param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlInt32" /> value.</param>
		/// <returns>The remainder left after division is performed on <paramref name="x" /> and <paramref name="y" />.</returns>
		// Token: 0x060024B2 RID: 9394 RVA: 0x000A6C32 File Offset: 0x000A4E32
		public static SqlInt32 Modulus(SqlInt32 x, SqlInt32 y)
		{
			return x % y;
		}

		/// <summary>Computes the bitwise AND of its <see cref="T:System.Data.SqlTypes.SqlInt32" /> operands.</summary>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlInt32" /> structure.</param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlInt32" /> structure.</param>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlInt32" /> structure that contains the results of the bitwise AND operation.</returns>
		// Token: 0x060024B3 RID: 9395 RVA: 0x000A6C3B File Offset: 0x000A4E3B
		public static SqlInt32 BitwiseAnd(SqlInt32 x, SqlInt32 y)
		{
			return x & y;
		}

		/// <summary>Computes the bitwise OR of the specified <see cref="T:System.Data.SqlTypes.SqlInt32" /> structures.</summary>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlInt32" /> structure.</param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlInt32" /> structure.</param>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlInt32" /> structure that contains the results of the bitwise OR operation.</returns>
		// Token: 0x060024B4 RID: 9396 RVA: 0x000A6C44 File Offset: 0x000A4E44
		public static SqlInt32 BitwiseOr(SqlInt32 x, SqlInt32 y)
		{
			return x | y;
		}

		/// <summary>Performs a bitwise exclusive-OR operation on the specified <see cref="T:System.Data.SqlTypes.SqlInt32" /> structures.</summary>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlInt32" /> structure.</param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlInt32" /> structure.</param>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlInt32" /> structure that contains the results of the bitwise XOR operation.</returns>
		// Token: 0x060024B5 RID: 9397 RVA: 0x000A6C4D File Offset: 0x000A4E4D
		public static SqlInt32 Xor(SqlInt32 x, SqlInt32 y)
		{
			return x ^ y;
		}

		/// <summary>Performs a logical comparison of the two <see cref="T:System.Data.SqlTypes.SqlInt32" /> parameters to determine whether they are equal.</summary>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlInt32" /> structure.</param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlInt32" /> structure.</param>
		/// <returns>
		///   <see langword="true" /> if the two values are equal. Otherwise, <see langword="false" />. If either instance is null, then the <see langword="SqlInt32" /> will be null.</returns>
		// Token: 0x060024B6 RID: 9398 RVA: 0x000A6C56 File Offset: 0x000A4E56
		public static SqlBoolean Equals(SqlInt32 x, SqlInt32 y)
		{
			return x == y;
		}

		/// <summary>Performs a logical comparison of the two <see cref="T:System.Data.SqlTypes.SqlInt32" /> parameters to determine whether they are not equal.</summary>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlInt32" /> structure.</param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlInt32" /> structure.</param>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> that is <see cref="F:System.Data.SqlTypes.SqlBoolean.True" /> if the two instances are not equal or <see cref="F:System.Data.SqlTypes.SqlBoolean.False" /> if the two instances are equal. If either instance of <see cref="T:System.Data.SqlTypes.SqlInt32" /> is null, the <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> will be <see cref="F:System.Data.SqlTypes.SqlBoolean.Null" />.</returns>
		// Token: 0x060024B7 RID: 9399 RVA: 0x000A6C5F File Offset: 0x000A4E5F
		public static SqlBoolean NotEquals(SqlInt32 x, SqlInt32 y)
		{
			return x != y;
		}

		/// <summary>Compares the two <see cref="T:System.Data.SqlTypes.SqlInt32" /> parameters to determine whether the first is less than the second.</summary>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlInt32" /> structure.</param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlInt32" /> structure.</param>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> that is <see cref="F:System.Data.SqlTypes.SqlBoolean.True" /> if the first instance is less than the second instance. Otherwise, <see cref="F:System.Data.SqlTypes.SqlBoolean.False" />. If either instance of <see cref="T:System.Data.SqlTypes.SqlInt32" /> is null, the <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> will be <see cref="F:System.Data.SqlTypes.SqlBoolean.Null" />.</returns>
		// Token: 0x060024B8 RID: 9400 RVA: 0x000A6C68 File Offset: 0x000A4E68
		public static SqlBoolean LessThan(SqlInt32 x, SqlInt32 y)
		{
			return x < y;
		}

		/// <summary>Compares the two <see cref="T:System.Data.SqlTypes.SqlInt32" /> parameters to determine whether the first is greater than the second.</summary>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlInt32" /> structure.</param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlInt32" /> structure.</param>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> that is <see cref="F:System.Data.SqlTypes.SqlBoolean.True" /> if the first instance is greater than the second instance. Otherwise, <see cref="F:System.Data.SqlTypes.SqlBoolean.False" />. If either instance of <see cref="T:System.Data.SqlTypes.SqlInt32" /> is null, the <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> will be <see cref="F:System.Data.SqlTypes.SqlBoolean.Null" />.</returns>
		// Token: 0x060024B9 RID: 9401 RVA: 0x000A6C71 File Offset: 0x000A4E71
		public static SqlBoolean GreaterThan(SqlInt32 x, SqlInt32 y)
		{
			return x > y;
		}

		/// <summary>Compares the two <see cref="T:System.Data.SqlTypes.SqlInt32" /> parameters to determine whether the first is less than or equal to the second.</summary>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlInt32" /> structure.</param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlInt32" /> structure.</param>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> that is <see cref="F:System.Data.SqlTypes.SqlBoolean.True" /> if the first instance is less than or equal to the second instance. Otherwise, <see cref="F:System.Data.SqlTypes.SqlBoolean.False" />. If either instance of <see cref="T:System.Data.SqlTypes.SqlInt32" /> is null, the <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> will be <see cref="F:System.Data.SqlTypes.SqlBoolean.Null" />.</returns>
		// Token: 0x060024BA RID: 9402 RVA: 0x000A6C7A File Offset: 0x000A4E7A
		public static SqlBoolean LessThanOrEqual(SqlInt32 x, SqlInt32 y)
		{
			return x <= y;
		}

		/// <summary>Compares the two <see cref="T:System.Data.SqlTypes.SqlInt32" /> parameters to determine whether the first is greater than or equal to the second.</summary>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlInt32" /> structure.</param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlInt32" /> structure.</param>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> that is <see cref="F:System.Data.SqlTypes.SqlBoolean.True" /> if the first instance is greater than or equal to the second instance. Otherwise, <see cref="F:System.Data.SqlTypes.SqlBoolean.False" />. If either instance of <see cref="T:System.Data.SqlTypes.SqlInt32" /> is null, the <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> will be <see cref="F:System.Data.SqlTypes.SqlBoolean.Null" />.</returns>
		// Token: 0x060024BB RID: 9403 RVA: 0x000A6C83 File Offset: 0x000A4E83
		public static SqlBoolean GreaterThanOrEqual(SqlInt32 x, SqlInt32 y)
		{
			return x >= y;
		}

		/// <summary>Converts this <see cref="T:System.Data.SqlTypes.SqlInt32" /> structure to <see cref="T:System.Data.SqlTypes.SqlBoolean" />.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="P:System.Data.SqlTypes.SqlInt32.Value" /> is non-zero; <see langword="false" /> if zero; otherwise Null.</returns>
		// Token: 0x060024BC RID: 9404 RVA: 0x000A6C8C File Offset: 0x000A4E8C
		public SqlBoolean ToSqlBoolean()
		{
			return (SqlBoolean)this;
		}

		/// <summary>Converts this <see cref="T:System.Data.SqlTypes.SqlInt32" /> structure to <see cref="T:System.Data.SqlTypes.SqlByte" />.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlByte" /> structure whose <see langword="Value" /> equals the <see langword="Value" /> of this <see cref="T:System.Data.SqlTypes.SqlInt32" /> structure. If the value of the <see langword="SqlInt32" /> is less than 0 or greater than 255, an <see cref="T:System.OverflowException" /> occurs.</returns>
		// Token: 0x060024BD RID: 9405 RVA: 0x000A6C99 File Offset: 0x000A4E99
		public SqlByte ToSqlByte()
		{
			return (SqlByte)this;
		}

		/// <summary>Converts this <see cref="T:System.Data.SqlTypes.SqlInt32" /> structure to <see cref="T:System.Data.SqlTypes.SqlDouble" />.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlDouble" /> structure equal to the value of this <see cref="T:System.Data.SqlTypes.SqlInt32" />.</returns>
		// Token: 0x060024BE RID: 9406 RVA: 0x000A6CA6 File Offset: 0x000A4EA6
		public SqlDouble ToSqlDouble()
		{
			return this;
		}

		/// <summary>Converts this <see cref="T:System.Data.SqlTypes.SqlInt32" /> structure to <see cref="T:System.Data.SqlTypes.SqlInt16" />.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlInt16" /> structure equal to the value of this <see cref="T:System.Data.SqlTypes.SqlInt32" />.</returns>
		// Token: 0x060024BF RID: 9407 RVA: 0x000A6CB3 File Offset: 0x000A4EB3
		public SqlInt16 ToSqlInt16()
		{
			return (SqlInt16)this;
		}

		/// <summary>Converts this <see cref="T:System.Data.SqlTypes.SqlInt32" /> structure to <see cref="T:System.Data.SqlTypes.SqlInt64" />.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlInt64" /> structure equal to the value of this <see cref="T:System.Data.SqlTypes.SqlInt32" />.</returns>
		// Token: 0x060024C0 RID: 9408 RVA: 0x000A6CC0 File Offset: 0x000A4EC0
		public SqlInt64 ToSqlInt64()
		{
			return this;
		}

		/// <summary>Converts this <see cref="T:System.Data.SqlTypes.SqlInt32" /> structure to <see cref="T:System.Data.SqlTypes.SqlMoney" />.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlMoney" /> structure equal to the value of this <see cref="T:System.Data.SqlTypes.SqlInt32" />.</returns>
		// Token: 0x060024C1 RID: 9409 RVA: 0x000A6CCD File Offset: 0x000A4ECD
		public SqlMoney ToSqlMoney()
		{
			return this;
		}

		/// <summary>Converts this <see cref="T:System.Data.SqlTypes.SqlInt32" /> structure to <see cref="T:System.Data.SqlTypes.SqlDecimal" />.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure equal to the value of this <see cref="T:System.Data.SqlTypes.SqlInt32" />.</returns>
		// Token: 0x060024C2 RID: 9410 RVA: 0x000A6CDA File Offset: 0x000A4EDA
		public SqlDecimal ToSqlDecimal()
		{
			return this;
		}

		/// <summary>Converts this <see cref="T:System.Data.SqlTypes.SqlInt32" /> structure to <see cref="T:System.Data.SqlTypes.SqlSingle" />.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlSingle" /> structure equal to the value of this <see cref="T:System.Data.SqlTypes.SqlInt32" />.</returns>
		// Token: 0x060024C3 RID: 9411 RVA: 0x000A6CE7 File Offset: 0x000A4EE7
		public SqlSingle ToSqlSingle()
		{
			return this;
		}

		/// <summary>Converts this <see cref="T:System.Data.SqlTypes.SqlInt32" /> structure to <see cref="T:System.Data.SqlTypes.SqlString" />.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlString" /> structure equal to the value of this <see cref="T:System.Data.SqlTypes.SqlInt32" />.</returns>
		// Token: 0x060024C4 RID: 9412 RVA: 0x000A6CF4 File Offset: 0x000A4EF4
		public SqlString ToSqlString()
		{
			return (SqlString)this;
		}

		/// <summary>Compares this <see cref="T:System.Data.SqlTypes.SqlInt32" /> instance to the supplied <see cref="T:System.Object" /> and returns an indication of their relative values.</summary>
		/// <param name="value">The <see cref="T:System.Object" /> to be compared.</param>
		/// <returns>A signed number that indicates the relative values of the instance and the object.  
		///   Return value  
		///
		///   Condition  
		///
		///   Less than zero  
		///
		///   This instance is less than the object.  
		///
		///   Zero  
		///
		///   This instance is the same as the object.  
		///
		///   Greater than zero  
		///
		///   This instance is greater than the object  
		///
		///  -or-  
		///
		///  The object is a null reference (<see langword="Nothing" /> in Visual Basic).</returns>
		// Token: 0x060024C5 RID: 9413 RVA: 0x000A6D04 File Offset: 0x000A4F04
		public int CompareTo(object value)
		{
			if (value is SqlInt32)
			{
				SqlInt32 value2 = (SqlInt32)value;
				return this.CompareTo(value2);
			}
			throw ADP.WrongType(value.GetType(), typeof(SqlInt32));
		}

		/// <summary>Compares this <see cref="T:System.Data.SqlTypes.SqlInt32" /> instance to the supplied <see cref="T:System.Data.SqlTypes.SqlInt32" /> and returns an indication of their relative values.</summary>
		/// <param name="value">The <see cref="T:System.Data.SqlTypes.SqlInt32" /> to be compared.</param>
		/// <returns>A signed number that indicates the relative values of the instance and the object.  
		///   Return value  
		///
		///   Condition  
		///
		///   Less than zero  
		///
		///   This instance is less than the object.  
		///
		///   Zero  
		///
		///   This instance is the same as the object.  
		///
		///   Greater than zero  
		///
		///   This instance is greater than the object  
		///
		///  -or-  
		///
		///  The object is a null reference (<see langword="Nothing" /> in Visual Basic)</returns>
		// Token: 0x060024C6 RID: 9414 RVA: 0x000A6D40 File Offset: 0x000A4F40
		public int CompareTo(SqlInt32 value)
		{
			if (this.IsNull)
			{
				if (!value.IsNull)
				{
					return -1;
				}
				return 0;
			}
			else
			{
				if (value.IsNull)
				{
					return 1;
				}
				if (this < value)
				{
					return -1;
				}
				if (this > value)
				{
					return 1;
				}
				return 0;
			}
		}

		/// <summary>Compares the supplied object parameter to the <see cref="P:System.Data.SqlTypes.SqlInt32.Value" /> property of the <see cref="T:System.Data.SqlTypes.SqlInt32" /> object.</summary>
		/// <param name="value">The object to be compared.</param>
		/// <returns>
		///   <see langword="true" /> if object is an instance of <see cref="T:System.Data.SqlTypes.SqlInt32" /> and the two are equal; otherwise <see langword="false" />.</returns>
		// Token: 0x060024C7 RID: 9415 RVA: 0x000A6D98 File Offset: 0x000A4F98
		public override bool Equals(object value)
		{
			if (!(value is SqlInt32))
			{
				return false;
			}
			SqlInt32 y = (SqlInt32)value;
			if (y.IsNull || this.IsNull)
			{
				return y.IsNull && this.IsNull;
			}
			return (this == y).Value;
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		// Token: 0x060024C8 RID: 9416 RVA: 0x000A6DF0 File Offset: 0x000A4FF0
		public override int GetHashCode()
		{
			if (!this.IsNull)
			{
				return this.Value.GetHashCode();
			}
			return 0;
		}

		/// <summary>This member supports the .NET Framework infrastructure and is not intended to be used directly from your code.</summary>
		/// <returns>An <see langword="XmlSchema" />.</returns>
		// Token: 0x060024C9 RID: 9417 RVA: 0x00003E32 File Offset: 0x00002032
		XmlSchema IXmlSerializable.GetSchema()
		{
			return null;
		}

		/// <summary>This member supports the .NET Framework infrastructure and is not intended to be used directly from your code.</summary>
		/// <param name="reader">
		///   <see langword="XmlReader" />
		/// </param>
		// Token: 0x060024CA RID: 9418 RVA: 0x000A6E18 File Offset: 0x000A5018
		void IXmlSerializable.ReadXml(XmlReader reader)
		{
			string attribute = reader.GetAttribute("nil", "http://www.w3.org/2001/XMLSchema-instance");
			if (attribute != null && XmlConvert.ToBoolean(attribute))
			{
				reader.ReadElementString();
				this.m_fNotNull = false;
				return;
			}
			this.m_value = XmlConvert.ToInt32(reader.ReadElementString());
			this.m_fNotNull = true;
		}

		/// <summary>This member supports the .NET Framework infrastructure and is not intended to be used directly from your code.</summary>
		/// <param name="writer">
		///   <see langword="XmlWriter" />
		/// </param>
		// Token: 0x060024CB RID: 9419 RVA: 0x000A6E68 File Offset: 0x000A5068
		void IXmlSerializable.WriteXml(XmlWriter writer)
		{
			if (this.IsNull)
			{
				writer.WriteAttributeString("xsi", "nil", "http://www.w3.org/2001/XMLSchema-instance", "true");
				return;
			}
			writer.WriteString(XmlConvert.ToString(this.m_value));
		}

		/// <summary>Returns the XML Schema definition language (XSD) of the specified <see cref="T:System.Xml.Schema.XmlSchemaSet" />.</summary>
		/// <param name="schemaSet">An <see cref="T:System.Xml.Schema.XmlSchemaSet" />.</param>
		/// <returns>A <see langword="string" /> value that indicates the XSD of the specified <see cref="T:System.Xml.Schema.XmlSchemaSet" />.</returns>
		// Token: 0x060024CC RID: 9420 RVA: 0x000A6E9E File Offset: 0x000A509E
		public static XmlQualifiedName GetXsdType(XmlSchemaSet schemaSet)
		{
			return new XmlQualifiedName("int", "http://www.w3.org/2001/XMLSchema");
		}

		// Token: 0x060024CD RID: 9421 RVA: 0x000A6EB0 File Offset: 0x000A50B0
		// Note: this type is marked as 'beforefieldinit'.
		static SqlInt32()
		{
		}

		// Token: 0x040018B9 RID: 6329
		private bool m_fNotNull;

		// Token: 0x040018BA RID: 6330
		private int m_value;

		// Token: 0x040018BB RID: 6331
		private static readonly long s_iIntMin = -2147483648L;

		// Token: 0x040018BC RID: 6332
		private static readonly long s_lBitNotIntMax = -2147483648L;

		/// <summary>Represents a <see cref="T:System.DBNull" /> that can be assigned to this instance of the <see cref="T:System.Data.SqlTypes.SqlInt32" /> class.</summary>
		// Token: 0x040018BD RID: 6333
		public static readonly SqlInt32 Null = new SqlInt32(true);

		/// <summary>Represents a zero value that can be assigned to the <see cref="P:System.Data.SqlTypes.SqlInt32.Value" /> property of an instance of the <see cref="T:System.Data.SqlTypes.SqlInt32" /> structure.</summary>
		// Token: 0x040018BE RID: 6334
		public static readonly SqlInt32 Zero = new SqlInt32(0);

		/// <summary>A constant representing the smallest possible value of a <see cref="T:System.Data.SqlTypes.SqlInt32" />.</summary>
		// Token: 0x040018BF RID: 6335
		public static readonly SqlInt32 MinValue = new SqlInt32(int.MinValue);

		/// <summary>A constant representing the largest possible value of a <see cref="T:System.Data.SqlTypes.SqlInt32" />.</summary>
		// Token: 0x040018C0 RID: 6336
		public static readonly SqlInt32 MaxValue = new SqlInt32(int.MaxValue);
	}
}

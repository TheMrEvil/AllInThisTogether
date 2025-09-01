using System;
using System.Data.Common;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace System.Data.SqlTypes
{
	/// <summary>Represents a 16-bit signed integer to be stored in or retrieved from a database.</summary>
	// Token: 0x02000311 RID: 785
	[XmlSchemaProvider("GetXsdType")]
	[Serializable]
	public struct SqlInt16 : INullable, IComparable, IXmlSerializable
	{
		// Token: 0x06002447 RID: 9287 RVA: 0x000A5D13 File Offset: 0x000A3F13
		private SqlInt16(bool fNull)
		{
			this.m_fNotNull = false;
			this.m_value = 0;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlTypes.SqlInt16" /> structure using the supplied short integer parameter.</summary>
		/// <param name="value">A short integer.</param>
		// Token: 0x06002448 RID: 9288 RVA: 0x000A5D23 File Offset: 0x000A3F23
		public SqlInt16(short value)
		{
			this.m_value = value;
			this.m_fNotNull = true;
		}

		/// <summary>Indicates whether this <see cref="T:System.Data.SqlTypes.SqlInt16" /> structure is null.</summary>
		/// <returns>
		///   <see langword="true" /> if null. Otherwise, <see langword="false" />. For more information, see Handling Null Values.</returns>
		// Token: 0x1700065F RID: 1631
		// (get) Token: 0x06002449 RID: 9289 RVA: 0x000A5D33 File Offset: 0x000A3F33
		public bool IsNull
		{
			get
			{
				return !this.m_fNotNull;
			}
		}

		/// <summary>Gets the value of this instance of <see cref="T:System.Data.SqlTypes.SqlInt16" /> structure. This property is read-only.</summary>
		/// <returns>A short integer representing the value of this <see cref="T:System.Data.SqlTypes.SqlInt16" /> structure.</returns>
		// Token: 0x17000660 RID: 1632
		// (get) Token: 0x0600244A RID: 9290 RVA: 0x000A5D3E File Offset: 0x000A3F3E
		public short Value
		{
			get
			{
				if (this.m_fNotNull)
				{
					return this.m_value;
				}
				throw new SqlNullValueException();
			}
		}

		/// <summary>Converts the supplied short integer to <see cref="T:System.Data.SqlTypes.SqlInt16" />.</summary>
		/// <param name="x">A short integer value.</param>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlInt16" /> structure with the same value as the specified short integer.</returns>
		// Token: 0x0600244B RID: 9291 RVA: 0x000A5D54 File Offset: 0x000A3F54
		public static implicit operator SqlInt16(short x)
		{
			return new SqlInt16(x);
		}

		/// <summary>Converts the supplied <see cref="T:System.Data.SqlTypes.SqlInt16" /> structure to a short integer.</summary>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlInt16" /> structure.</param>
		/// <returns>A short integer whose value is the Value of the <see cref="T:System.Data.SqlTypes.SqlInt16" /> parameter.</returns>
		// Token: 0x0600244C RID: 9292 RVA: 0x000A5D5C File Offset: 0x000A3F5C
		public static explicit operator short(SqlInt16 x)
		{
			return x.Value;
		}

		/// <summary>Converts a <see cref="T:System.Data.SqlTypes.SqlInt16" /> structure to <see cref="T:System.String" />.</summary>
		/// <returns>A <see cref="T:System.String" /> object representing the <see cref="P:System.Data.SqlTypes.SqlInt16.Value" /> of this instance of <see cref="T:System.Data.SqlTypes.SqlInt16" />.</returns>
		// Token: 0x0600244D RID: 9293 RVA: 0x000A5D65 File Offset: 0x000A3F65
		public override string ToString()
		{
			if (!this.IsNull)
			{
				return this.m_value.ToString(null);
			}
			return SQLResource.NullString;
		}

		/// <summary>Converts the <see cref="T:System.String" /> representation of a number to its 16-bit signed integer equivalent.</summary>
		/// <param name="s">The <see langword="String" /> to be parsed.</param>
		/// <returns>A 16-bit signed integer equivalent to the value that is contained in the specified <see cref="T:System.String" />.</returns>
		// Token: 0x0600244E RID: 9294 RVA: 0x000A5D81 File Offset: 0x000A3F81
		public static SqlInt16 Parse(string s)
		{
			if (s == SQLResource.NullString)
			{
				return SqlInt16.Null;
			}
			return new SqlInt16(short.Parse(s, null));
		}

		/// <summary>The unary minus operator negates the <see cref="P:System.Data.SqlTypes.SqlInt16.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlInt16" /> operand.</summary>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlInt16" /> structure.</param>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlInt16" /> structure that contains the negated value.</returns>
		// Token: 0x0600244F RID: 9295 RVA: 0x000A5DA2 File Offset: 0x000A3FA2
		public static SqlInt16 operator -(SqlInt16 x)
		{
			if (!x.IsNull)
			{
				return new SqlInt16(-x.m_value);
			}
			return SqlInt16.Null;
		}

		/// <summary>The ~ operator performs a bitwise one's complement operation on its <see cref="T:System.Data.SqlTypes.SqlByte" /> operand.</summary>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlInt16" /> structure.</param>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlInt16" /> structure whose <see cref="P:System.Data.SqlTypes.SqlInt16.Value" /> property contains the complement of the specified <see cref="T:System.Data.SqlTypes.SqlInt16" /> structure.</returns>
		// Token: 0x06002450 RID: 9296 RVA: 0x000A5DC0 File Offset: 0x000A3FC0
		public static SqlInt16 operator ~(SqlInt16 x)
		{
			if (!x.IsNull)
			{
				return new SqlInt16(~x.m_value);
			}
			return SqlInt16.Null;
		}

		/// <summary>Computes the sum of the two <see cref="T:System.Data.SqlTypes.SqlInt16" /> operands.</summary>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlInt16" /> structure.</param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlInt16" /> structure.</param>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlInt16" /> structure whose <see cref="P:System.Data.SqlTypes.SqlInt16.Value" /> property contains the sum of the two <see cref="T:System.Data.SqlTypes.SqlInt16" /> operands.</returns>
		// Token: 0x06002451 RID: 9297 RVA: 0x000A5DE0 File Offset: 0x000A3FE0
		public static SqlInt16 operator +(SqlInt16 x, SqlInt16 y)
		{
			if (x.IsNull || y.IsNull)
			{
				return SqlInt16.Null;
			}
			int num = (int)(x.m_value + y.m_value);
			if (((num >> 15 ^ num >> 16) & 1) != 0)
			{
				throw new OverflowException(SQLResource.ArithOverflowMessage);
			}
			return new SqlInt16((short)num);
		}

		/// <summary>Subtracts the second <see cref="T:System.Data.SqlTypes.SqlInt16" /> parameter from the first.</summary>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlInt16" /> structure.</param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlInt16" /> structure.</param>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlInt16" /> structure whose <see cref="P:System.Data.SqlTypes.SqlInt16.Value" /> property contains the results of the subtraction.</returns>
		// Token: 0x06002452 RID: 9298 RVA: 0x000A5E34 File Offset: 0x000A4034
		public static SqlInt16 operator -(SqlInt16 x, SqlInt16 y)
		{
			if (x.IsNull || y.IsNull)
			{
				return SqlInt16.Null;
			}
			int num = (int)(x.m_value - y.m_value);
			if (((num >> 15 ^ num >> 16) & 1) != 0)
			{
				throw new OverflowException(SQLResource.ArithOverflowMessage);
			}
			return new SqlInt16((short)num);
		}

		/// <summary>Computes the product of the two <see cref="T:System.Data.SqlTypes.SqlInt16" /> parameters.</summary>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlInt16" /> structure.</param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlInt16" /> structure.</param>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlInt16" /> structure whose <see cref="P:System.Data.SqlTypes.SqlInt16.Value" /> contains the product of the two parameters.</returns>
		// Token: 0x06002453 RID: 9299 RVA: 0x000A5E88 File Offset: 0x000A4088
		public static SqlInt16 operator *(SqlInt16 x, SqlInt16 y)
		{
			if (x.IsNull || y.IsNull)
			{
				return SqlInt16.Null;
			}
			int num = (int)(x.m_value * y.m_value);
			int num2 = num & SqlInt16.s_MASKI2;
			if (num2 != 0 && num2 != SqlInt16.s_MASKI2)
			{
				throw new OverflowException(SQLResource.ArithOverflowMessage);
			}
			return new SqlInt16((short)num);
		}

		/// <summary>Divides the first <see cref="T:System.Data.SqlTypes.SqlInt16" /> operand by the second.</summary>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlInt16" /> structure.</param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlInt16" /> structure.</param>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlInt16" /> whose <see cref="P:System.Data.SqlTypes.SqlInt16.Value" /> property contains the results of the division.</returns>
		// Token: 0x06002454 RID: 9300 RVA: 0x000A5EE0 File Offset: 0x000A40E0
		public static SqlInt16 operator /(SqlInt16 x, SqlInt16 y)
		{
			if (x.IsNull || y.IsNull)
			{
				return SqlInt16.Null;
			}
			if (y.m_value == 0)
			{
				throw new DivideByZeroException(SQLResource.DivideByZeroMessage);
			}
			if (x.m_value == -32768 && y.m_value == -1)
			{
				throw new OverflowException(SQLResource.ArithOverflowMessage);
			}
			return new SqlInt16(x.m_value / y.m_value);
		}

		/// <summary>Computes the remainder after dividing its first <see cref="T:System.Data.SqlTypes.SqlInt16" /> operand by its second.</summary>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlInt16" /> structure.</param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlInt16" /> structure.</param>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlInt16" /> structure whose <see cref="P:System.Data.SqlTypes.SqlInt16.Value" /> contains the remainder.</returns>
		// Token: 0x06002455 RID: 9301 RVA: 0x000A5F4C File Offset: 0x000A414C
		public static SqlInt16 operator %(SqlInt16 x, SqlInt16 y)
		{
			if (x.IsNull || y.IsNull)
			{
				return SqlInt16.Null;
			}
			if (y.m_value == 0)
			{
				throw new DivideByZeroException(SQLResource.DivideByZeroMessage);
			}
			if (x.m_value == -32768 && y.m_value == -1)
			{
				throw new OverflowException(SQLResource.ArithOverflowMessage);
			}
			return new SqlInt16(x.m_value % y.m_value);
		}

		/// <summary>Computes the bitwise AND of its <see cref="T:System.Data.SqlTypes.SqlInt16" /> operands.</summary>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlInt16" /> structure.</param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlInt16" /> structure.</param>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlInt16" /> structure whose <see cref="P:System.Data.SqlTypes.SqlInt16.Value" /> property contains the results of the bitwise AND.</returns>
		// Token: 0x06002456 RID: 9302 RVA: 0x000A5FB8 File Offset: 0x000A41B8
		public static SqlInt16 operator &(SqlInt16 x, SqlInt16 y)
		{
			if (!x.IsNull && !y.IsNull)
			{
				return new SqlInt16(x.m_value & y.m_value);
			}
			return SqlInt16.Null;
		}

		/// <summary>Computes the bitwise OR of its two <see cref="T:System.Data.SqlTypes.SqlInt16" /> operands.</summary>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlInt16" /> structure.</param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlInt16" /> structure.</param>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlInt16" /> structure whose <see cref="P:System.Data.SqlTypes.SqlInt16.Value" /> property contains the results of the bitwise OR.</returns>
		// Token: 0x06002457 RID: 9303 RVA: 0x000A5FE5 File Offset: 0x000A41E5
		public static SqlInt16 operator |(SqlInt16 x, SqlInt16 y)
		{
			if (!x.IsNull && !y.IsNull)
			{
				return new SqlInt16((short)((ushort)x.m_value | (ushort)y.m_value));
			}
			return SqlInt16.Null;
		}

		/// <summary>Performs a bitwise exclusive-OR operation on the supplied parameters.</summary>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlInt16" /> structure.</param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlInt16" /> structure.</param>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlInt16" /> structure whose <see cref="P:System.Data.SqlTypes.SqlInt16.Value" /> property contains the results of the bitwise XOR.</returns>
		// Token: 0x06002458 RID: 9304 RVA: 0x000A6014 File Offset: 0x000A4214
		public static SqlInt16 operator ^(SqlInt16 x, SqlInt16 y)
		{
			if (!x.IsNull && !y.IsNull)
			{
				return new SqlInt16(x.m_value ^ y.m_value);
			}
			return SqlInt16.Null;
		}

		/// <summary>Converts the supplied <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure to <see cref="T:System.Data.SqlTypes.SqlInt16" />.</summary>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure.</param>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlInt16" /> structure whose <see cref="P:System.Data.SqlTypes.SqlInt16.Value" /> property is equal to the <see cref="P:System.Data.SqlTypes.SqlBoolean.ByteValue" /> property of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> parameter.</returns>
		// Token: 0x06002459 RID: 9305 RVA: 0x000A6041 File Offset: 0x000A4241
		public static explicit operator SqlInt16(SqlBoolean x)
		{
			if (!x.IsNull)
			{
				return new SqlInt16((short)x.ByteValue);
			}
			return SqlInt16.Null;
		}

		/// <summary>Converts the supplied <see cref="T:System.Data.SqlTypes.SqlByte" /> structure to <see cref="T:System.Data.SqlTypes.SqlInt16" />.</summary>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlByte" /> structure.</param>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlInt16" /> structure whose <see cref="P:System.Data.SqlTypes.SqlInt16.Value" /> property is equal to the <see cref="P:System.Data.SqlTypes.SqlByte.Value" /> property of the <see cref="T:System.Data.SqlTypes.SqlByte" /> parameter.</returns>
		// Token: 0x0600245A RID: 9306 RVA: 0x000A605E File Offset: 0x000A425E
		public static implicit operator SqlInt16(SqlByte x)
		{
			if (!x.IsNull)
			{
				return new SqlInt16((short)x.Value);
			}
			return SqlInt16.Null;
		}

		/// <summary>Converts the supplied <see cref="T:System.Data.SqlTypes.SqlInt32" /> structure to <see cref="T:System.Data.SqlTypes.SqlInt16" />.</summary>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlInt32" /> structure.</param>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlInt16" /> structure whose <see cref="P:System.Data.SqlTypes.SqlInt16.Value" /> property is equal to the <see cref="P:System.Data.SqlTypes.SqlInt32.Value" /> of the supplied <see cref="T:System.Data.SqlTypes.SqlInt32" /> parameter.</returns>
		// Token: 0x0600245B RID: 9307 RVA: 0x000A607C File Offset: 0x000A427C
		public static explicit operator SqlInt16(SqlInt32 x)
		{
			if (x.IsNull)
			{
				return SqlInt16.Null;
			}
			int value = x.Value;
			if (value > 32767 || value < -32768)
			{
				throw new OverflowException(SQLResource.ArithOverflowMessage);
			}
			return new SqlInt16((short)value);
		}

		/// <summary>Converts the supplied <see cref="T:System.Data.SqlTypes.SqlInt64" /> structure to <see cref="T:System.Data.SqlTypes.SqlInt16" />.</summary>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlInt64" /> structure.</param>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlInt16" /> structure whose <see cref="P:System.Data.SqlTypes.SqlInt16.Value" /> property is equal to the <see cref="P:System.Data.SqlTypes.SqlInt64.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlInt64" /> parameter.</returns>
		// Token: 0x0600245C RID: 9308 RVA: 0x000A60C4 File Offset: 0x000A42C4
		public static explicit operator SqlInt16(SqlInt64 x)
		{
			if (x.IsNull)
			{
				return SqlInt16.Null;
			}
			long value = x.Value;
			if (value > 32767L || value < -32768L)
			{
				throw new OverflowException(SQLResource.ArithOverflowMessage);
			}
			return new SqlInt16((short)value);
		}

		/// <summary>Converts the supplied <see cref="T:System.Data.SqlTypes.SqlSingle" /> structure to <see cref="T:System.Data.SqlTypes.SqlInt16" />.</summary>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlSingle" /> structure.</param>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlInt16" /> structure whose <see cref="P:System.Data.SqlTypes.SqlInt16.Value" /> property is equal to the integer part of the <see cref="T:System.Data.SqlTypes.SqlSingle" /> parameter.</returns>
		// Token: 0x0600245D RID: 9309 RVA: 0x000A610C File Offset: 0x000A430C
		public static explicit operator SqlInt16(SqlSingle x)
		{
			if (x.IsNull)
			{
				return SqlInt16.Null;
			}
			float value = x.Value;
			if (value < -32768f || value > 32767f)
			{
				throw new OverflowException(SQLResource.ArithOverflowMessage);
			}
			return new SqlInt16((short)value);
		}

		/// <summary>Converts the supplied <see cref="T:System.Data.SqlTypes.SqlDouble" /> structure to <see cref="T:System.Data.SqlTypes.SqlInt16" />.</summary>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlDouble" /> structure.</param>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlInt16" /> structure whose <see cref="P:System.Data.SqlTypes.SqlInt16.Value" /> property is equal to the integer part of the <see cref="T:System.Data.SqlTypes.SqlDouble" /> parameter.</returns>
		// Token: 0x0600245E RID: 9310 RVA: 0x000A6154 File Offset: 0x000A4354
		public static explicit operator SqlInt16(SqlDouble x)
		{
			if (x.IsNull)
			{
				return SqlInt16.Null;
			}
			double value = x.Value;
			if (value < -32768.0 || value > 32767.0)
			{
				throw new OverflowException(SQLResource.ArithOverflowMessage);
			}
			return new SqlInt16((short)value);
		}

		/// <summary>Converts the supplied <see cref="T:System.Data.SqlTypes.SqlMoney" /> structure to <see cref="T:System.Data.SqlTypes.SqlInt16" />.</summary>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlMoney" /> structure.</param>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlInt16" /> structure whose <see cref="P:System.Data.SqlTypes.SqlInt16.Value" /> property is equal to the <see cref="P:System.Data.SqlTypes.SqlMoney.Value" /> property of the <see cref="T:System.Data.SqlTypes.SqlMoney" /> parameter.</returns>
		// Token: 0x0600245F RID: 9311 RVA: 0x000A61A2 File Offset: 0x000A43A2
		public static explicit operator SqlInt16(SqlMoney x)
		{
			if (!x.IsNull)
			{
				return new SqlInt16(checked((short)x.ToInt32()));
			}
			return SqlInt16.Null;
		}

		/// <summary>Converts the supplied <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure to <see cref="T:System.Data.SqlTypes.SqlInt16" />.</summary>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure.</param>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlInt16" /> structure whose <see cref="P:System.Data.SqlTypes.SqlInt16.Value" /> property is equal to the <see cref="P:System.Data.SqlTypes.SqlDecimal.Value" /> property of the <see cref="T:System.Data.SqlTypes.SqlDecimal" /> parameter.</returns>
		// Token: 0x06002460 RID: 9312 RVA: 0x000A61C0 File Offset: 0x000A43C0
		public static explicit operator SqlInt16(SqlDecimal x)
		{
			return (SqlInt16)((SqlInt32)x);
		}

		/// <summary>Converts the supplied <see cref="T:System.Data.SqlTypes.SqlString" /> object to <see cref="T:System.Data.SqlTypes.SqlInt16" />.</summary>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlString" /> object.</param>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlInt16" /> structure whose <see cref="P:System.Data.SqlTypes.SqlInt16.Value" /> property is equal to the value represented by the <see cref="T:System.Data.SqlTypes.SqlString" /> object parameter.</returns>
		// Token: 0x06002461 RID: 9313 RVA: 0x000A61CD File Offset: 0x000A43CD
		public static explicit operator SqlInt16(SqlString x)
		{
			if (!x.IsNull)
			{
				return new SqlInt16(short.Parse(x.Value, null));
			}
			return SqlInt16.Null;
		}

		/// <summary>Performs a logical comparison of two <see cref="T:System.Data.SqlTypes.SqlInt16" /> structures to determine whether they are equal.</summary>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlInt16" /> structure.</param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlInt16" /> structure.</param>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> that is <see cref="F:System.Data.SqlTypes.SqlBoolean.True" /> if the two instances are equal or <see cref="F:System.Data.SqlTypes.SqlBoolean.False" /> if the two instances are not equal. If either instance of <see cref="T:System.Data.SqlTypes.SqlInt16" /> is null, the <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> will be <see cref="F:System.Data.SqlTypes.SqlBoolean.Null" />.</returns>
		// Token: 0x06002462 RID: 9314 RVA: 0x000A61F0 File Offset: 0x000A43F0
		public static SqlBoolean operator ==(SqlInt16 x, SqlInt16 y)
		{
			if (!x.IsNull && !y.IsNull)
			{
				return new SqlBoolean(x.m_value == y.m_value);
			}
			return SqlBoolean.Null;
		}

		/// <summary>Performs a logical comparison of two <see cref="T:System.Data.SqlTypes.SqlInt16" /> structures to determine whether they are not equal.</summary>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlInt16" /> structure.</param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlInt16" /> structure.</param>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> that is <see cref="F:System.Data.SqlTypes.SqlBoolean.True" /> if the two instances are not equal or <see cref="F:System.Data.SqlTypes.SqlBoolean.False" /> if the two instances are equal. If either instance of <see cref="T:System.Data.SqlTypes.SqlInt16" /> is null, the <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> will be <see cref="F:System.Data.SqlTypes.SqlBoolean.Null" />.</returns>
		// Token: 0x06002463 RID: 9315 RVA: 0x000A621D File Offset: 0x000A441D
		public static SqlBoolean operator !=(SqlInt16 x, SqlInt16 y)
		{
			return !(x == y);
		}

		/// <summary>Compares two instances of <see cref="T:System.Data.SqlTypes.SqlInt16" /> to determine whether the first is less than the second.</summary>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlInt16" /> structure.</param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlInt16" /> structure.</param>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> that is <see cref="F:System.Data.SqlTypes.SqlBoolean.True" /> if the first instance is less than the second instance. Otherwise, <see cref="F:System.Data.SqlTypes.SqlBoolean.False" />. If either instance of <see cref="T:System.Data.SqlTypes.SqlInt16" /> is null, the <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> will be <see cref="F:System.Data.SqlTypes.SqlBoolean.Null" />.</returns>
		// Token: 0x06002464 RID: 9316 RVA: 0x000A622B File Offset: 0x000A442B
		public static SqlBoolean operator <(SqlInt16 x, SqlInt16 y)
		{
			if (!x.IsNull && !y.IsNull)
			{
				return new SqlBoolean(x.m_value < y.m_value);
			}
			return SqlBoolean.Null;
		}

		/// <summary>Compares two instances of <see cref="T:System.Data.SqlTypes.SqlInt16" /> to determine whether the first is greater than the second.</summary>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlInt16" /> structure.</param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlInt16" /> structure.</param>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> that is <see cref="F:System.Data.SqlTypes.SqlBoolean.True" /> if the first instance is greater than the second instance. Otherwise, <see cref="F:System.Data.SqlTypes.SqlBoolean.False" />. If either instance of <see cref="T:System.Data.SqlTypes.SqlInt16" /> is null, the <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> will be <see cref="F:System.Data.SqlTypes.SqlBoolean.Null" />.</returns>
		// Token: 0x06002465 RID: 9317 RVA: 0x000A6258 File Offset: 0x000A4458
		public static SqlBoolean operator >(SqlInt16 x, SqlInt16 y)
		{
			if (!x.IsNull && !y.IsNull)
			{
				return new SqlBoolean(x.m_value > y.m_value);
			}
			return SqlBoolean.Null;
		}

		/// <summary>Compares two <see cref="T:System.Data.SqlTypes.SqlInt16" /> structures to determine whether the first is less than or equal to the second.</summary>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlInt16" /> structure.</param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlInt16" /> structure.</param>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> that is <see cref="F:System.Data.SqlTypes.SqlBoolean.True" /> if the first instance is less than or equal to the second instance. Otherwise, <see cref="F:System.Data.SqlTypes.SqlBoolean.False" />. If either instance of <see cref="T:System.Data.SqlTypes.SqlInt16" /> is null, the <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> will be <see cref="F:System.Data.SqlTypes.SqlBoolean.Null" />.</returns>
		// Token: 0x06002466 RID: 9318 RVA: 0x000A6285 File Offset: 0x000A4485
		public static SqlBoolean operator <=(SqlInt16 x, SqlInt16 y)
		{
			if (!x.IsNull && !y.IsNull)
			{
				return new SqlBoolean(x.m_value <= y.m_value);
			}
			return SqlBoolean.Null;
		}

		/// <summary>Compares two <see cref="T:System.Data.SqlTypes.SqlInt16" /> structures to determine whether the first is greater than or equal to the second.</summary>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlInt16" /> structure.</param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlInt16" /> structure.</param>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> that is <see cref="F:System.Data.SqlTypes.SqlBoolean.True" /> if the first instance is greater than or equal to the second instance. Otherwise, <see cref="F:System.Data.SqlTypes.SqlBoolean.False" />. If either instance of <see cref="T:System.Data.SqlTypes.SqlInt16" /> is null, the <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> will be <see cref="F:System.Data.SqlTypes.SqlBoolean.Null" />.</returns>
		// Token: 0x06002467 RID: 9319 RVA: 0x000A62B5 File Offset: 0x000A44B5
		public static SqlBoolean operator >=(SqlInt16 x, SqlInt16 y)
		{
			if (!x.IsNull && !y.IsNull)
			{
				return new SqlBoolean(x.m_value >= y.m_value);
			}
			return SqlBoolean.Null;
		}

		/// <summary>The ~ operator performs a bitwise one's complement operation on its <see cref="T:System.Data.SqlTypes.SqlByte" /> operand.</summary>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlInt16" /> structure.</param>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlInt16" /> structure whose <see cref="P:System.Data.SqlTypes.SqlInt16.Value" /> property contains the complement of the specified <see cref="T:System.Data.SqlTypes.SqlInt16" /> structure.</returns>
		// Token: 0x06002468 RID: 9320 RVA: 0x000A62E5 File Offset: 0x000A44E5
		public static SqlInt16 OnesComplement(SqlInt16 x)
		{
			return ~x;
		}

		/// <summary>Computes the sum of the two <see cref="T:System.Data.SqlTypes.SqlInt16" /> operands.</summary>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlInt16" /> structure.</param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlInt16" /> structure.</param>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlInt16" /> structure whose <see cref="P:System.Data.SqlTypes.SqlInt16.Value" /> property contains the sum of the two <see cref="T:System.Data.SqlTypes.SqlInt16" /> operands.</returns>
		// Token: 0x06002469 RID: 9321 RVA: 0x000A62ED File Offset: 0x000A44ED
		public static SqlInt16 Add(SqlInt16 x, SqlInt16 y)
		{
			return x + y;
		}

		/// <summary>Subtracts the second <see cref="T:System.Data.SqlTypes.SqlInt16" /> parameter from the first.</summary>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlInt16" /> structure.</param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlInt16" /> structure.</param>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlInt16" /> structure whose <see cref="P:System.Data.SqlTypes.SqlInt16.Value" /> property contains the results of the subtraction.</returns>
		// Token: 0x0600246A RID: 9322 RVA: 0x000A62F6 File Offset: 0x000A44F6
		public static SqlInt16 Subtract(SqlInt16 x, SqlInt16 y)
		{
			return x - y;
		}

		/// <summary>Computes the product of the two <see cref="T:System.Data.SqlTypes.SqlInt16" /> parameters.</summary>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlInt16" /> structure.</param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlInt16" /> structure.</param>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlInt16" /> structure whose <see cref="P:System.Data.SqlTypes.SqlInt16.Value" /> contains the product of the two parameters.</returns>
		// Token: 0x0600246B RID: 9323 RVA: 0x000A62FF File Offset: 0x000A44FF
		public static SqlInt16 Multiply(SqlInt16 x, SqlInt16 y)
		{
			return x * y;
		}

		/// <summary>Divides the first <see cref="T:System.Data.SqlTypes.SqlInt16" /> operand by the second.</summary>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlInt16" /> structure.</param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlInt16" /> structure.</param>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlInt16" /> whose <see cref="P:System.Data.SqlTypes.SqlInt16.Value" /> property contains the results of the division.</returns>
		// Token: 0x0600246C RID: 9324 RVA: 0x000A6308 File Offset: 0x000A4508
		public static SqlInt16 Divide(SqlInt16 x, SqlInt16 y)
		{
			return x / y;
		}

		/// <summary>Computes the remainder after dividing its first <see cref="T:System.Data.SqlTypes.SqlInt16" /> operand by its second.</summary>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlInt16" /> structure.</param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlInt16" /> structure.</param>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlInt16" /> structure whose <see cref="P:System.Data.SqlTypes.SqlInt16.Value" /> contains the remainder.</returns>
		// Token: 0x0600246D RID: 9325 RVA: 0x000A6311 File Offset: 0x000A4511
		public static SqlInt16 Mod(SqlInt16 x, SqlInt16 y)
		{
			return x % y;
		}

		/// <summary>Divides two <see cref="T:System.Data.SqlTypes.SqlInt16" /> values and returns the remainder.</summary>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlInt16" /> value.</param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlInt16" /> value.</param>
		/// <returns>The remainder left after division is performed on <paramref name="x" /> and <paramref name="y" />.</returns>
		// Token: 0x0600246E RID: 9326 RVA: 0x000A6311 File Offset: 0x000A4511
		public static SqlInt16 Modulus(SqlInt16 x, SqlInt16 y)
		{
			return x % y;
		}

		/// <summary>Computes the bitwise AND of its <see cref="T:System.Data.SqlTypes.SqlInt16" /> operands.</summary>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlInt16" /> structure.</param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlInt16" /> structure.</param>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlInt16" /> structure whose <see cref="P:System.Data.SqlTypes.SqlInt16.Value" /> property contains the results of the bitwise AND.</returns>
		// Token: 0x0600246F RID: 9327 RVA: 0x000A631A File Offset: 0x000A451A
		public static SqlInt16 BitwiseAnd(SqlInt16 x, SqlInt16 y)
		{
			return x & y;
		}

		/// <summary>Computes the bitwise OR of its two <see cref="T:System.Data.SqlTypes.SqlInt16" /> operands.</summary>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlInt16" /> structure.</param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlInt16" /> structure.</param>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlInt16" /> structure whose <see cref="P:System.Data.SqlTypes.SqlInt16.Value" /> property contains the results of the bitwise OR.</returns>
		// Token: 0x06002470 RID: 9328 RVA: 0x000A6323 File Offset: 0x000A4523
		public static SqlInt16 BitwiseOr(SqlInt16 x, SqlInt16 y)
		{
			return x | y;
		}

		/// <summary>Performs a bitwise exclusive-OR operation on the supplied parameters.</summary>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlInt16" /> structure.</param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlInt16" /> structure.</param>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlInt16" /> structure that contains the results of the XOR operation.</returns>
		// Token: 0x06002471 RID: 9329 RVA: 0x000A632C File Offset: 0x000A452C
		public static SqlInt16 Xor(SqlInt16 x, SqlInt16 y)
		{
			return x ^ y;
		}

		/// <summary>Performs a logical comparison of two <see cref="T:System.Data.SqlTypes.SqlInt16" /> structures to determine whether they are equal.</summary>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlInt16" /> structure.</param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlInt16" /> structure.</param>
		/// <returns>
		///   <see langword="true" /> if the two values are equal. Otherwise, <see langword="false" />. If either instance is null, then the <see langword="SqlInt16" /> will be null.</returns>
		// Token: 0x06002472 RID: 9330 RVA: 0x000A6335 File Offset: 0x000A4535
		public static SqlBoolean Equals(SqlInt16 x, SqlInt16 y)
		{
			return x == y;
		}

		/// <summary>Performs a logical comparison of two <see cref="T:System.Data.SqlTypes.SqlInt16" /> structures to determine whether they are not equal.</summary>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlInt16" /> structure.</param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlInt16" /> structure.</param>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> that is <see cref="F:System.Data.SqlTypes.SqlBoolean.True" /> if the two instances are not equal or <see cref="F:System.Data.SqlTypes.SqlBoolean.False" /> if the two instances are equal. If either instance of <see cref="T:System.Data.SqlTypes.SqlInt16" /> is null, the <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> will be <see cref="F:System.Data.SqlTypes.SqlBoolean.Null" />.</returns>
		// Token: 0x06002473 RID: 9331 RVA: 0x000A633E File Offset: 0x000A453E
		public static SqlBoolean NotEquals(SqlInt16 x, SqlInt16 y)
		{
			return x != y;
		}

		/// <summary>Compares two instances of <see cref="T:System.Data.SqlTypes.SqlInt16" /> to determine whether the first is less than the second.</summary>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlInt16" /> structure.</param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlInt16" /> structure.</param>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> that is <see cref="F:System.Data.SqlTypes.SqlBoolean.True" /> if the first instance is less than the second instance. Otherwise, <see cref="F:System.Data.SqlTypes.SqlBoolean.False" />. If either instance of <see cref="T:System.Data.SqlTypes.SqlInt16" /> is null, the <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> will be <see cref="F:System.Data.SqlTypes.SqlBoolean.Null" />.</returns>
		// Token: 0x06002474 RID: 9332 RVA: 0x000A6347 File Offset: 0x000A4547
		public static SqlBoolean LessThan(SqlInt16 x, SqlInt16 y)
		{
			return x < y;
		}

		/// <summary>Compares two instances of <see cref="T:System.Data.SqlTypes.SqlInt16" /> to determine whether the first is greater than the second.</summary>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlInt16" /> structure.</param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlInt16" /> structure.</param>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> that is <see cref="F:System.Data.SqlTypes.SqlBoolean.True" /> if the first instance is greater than the second instance. Otherwise, <see cref="F:System.Data.SqlTypes.SqlBoolean.False" />. If either instance of <see cref="T:System.Data.SqlTypes.SqlInt16" /> is null, the <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> will be <see cref="F:System.Data.SqlTypes.SqlBoolean.Null" />.</returns>
		// Token: 0x06002475 RID: 9333 RVA: 0x000A6350 File Offset: 0x000A4550
		public static SqlBoolean GreaterThan(SqlInt16 x, SqlInt16 y)
		{
			return x > y;
		}

		/// <summary>Compares two <see cref="T:System.Data.SqlTypes.SqlInt16" /> structures to determine whether the first is less than or equal to the second.</summary>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlInt16" /> structure.</param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlInt16" /> structure.</param>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> that is <see cref="F:System.Data.SqlTypes.SqlBoolean.True" /> if the first instance is less than or equal to the second instance. Otherwise, <see cref="F:System.Data.SqlTypes.SqlBoolean.False" />. If either instance of <see cref="T:System.Data.SqlTypes.SqlInt16" /> is null, the <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> will be <see cref="F:System.Data.SqlTypes.SqlBoolean.Null" />.</returns>
		// Token: 0x06002476 RID: 9334 RVA: 0x000A6359 File Offset: 0x000A4559
		public static SqlBoolean LessThanOrEqual(SqlInt16 x, SqlInt16 y)
		{
			return x <= y;
		}

		/// <summary>Compares two <see cref="T:System.Data.SqlTypes.SqlInt16" /> structures to determine whether the first is greater than or equal to the second.</summary>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlInt16" /> structure.</param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlInt16" /> structure.</param>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> that is <see cref="F:System.Data.SqlTypes.SqlBoolean.True" /> if the first instance is greater than or equal to the second instance. Otherwise, <see cref="F:System.Data.SqlTypes.SqlBoolean.False" />. If either instance of <see cref="T:System.Data.SqlTypes.SqlInt16" /> is null, the <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> will be <see cref="F:System.Data.SqlTypes.SqlBoolean.Null" />.</returns>
		// Token: 0x06002477 RID: 9335 RVA: 0x000A6362 File Offset: 0x000A4562
		public static SqlBoolean GreaterThanOrEqual(SqlInt16 x, SqlInt16 y)
		{
			return x >= y;
		}

		/// <summary>Converts this <see cref="T:System.Data.SqlTypes.SqlInt16" /> structure to <see cref="T:System.Data.SqlTypes.SqlBoolean" />.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="P:System.Data.SqlTypes.SqlInt16.Value" /> is non-zero; <see langword="false" /> if zero; otherwise Null.</returns>
		// Token: 0x06002478 RID: 9336 RVA: 0x000A636B File Offset: 0x000A456B
		public SqlBoolean ToSqlBoolean()
		{
			return (SqlBoolean)this;
		}

		/// <summary>Converts this <see cref="T:System.Data.SqlTypes.SqlInt16" /> structure to <see cref="T:System.Data.SqlTypes.SqlByte" />.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlByte" /> structure whose <see cref="P:System.Data.SqlTypes.SqlInt16.Value" /> equals the <see cref="P:System.Data.SqlTypes.SqlInt16.Value" /> of this <see cref="T:System.Data.SqlTypes.SqlInt16" /> structure. If the value of the <see cref="T:System.Data.SqlTypes.SqlInt16" /> is less than 0 or greater than 255, an <see cref="T:System.OverflowException" /> occurs.</returns>
		// Token: 0x06002479 RID: 9337 RVA: 0x000A6378 File Offset: 0x000A4578
		public SqlByte ToSqlByte()
		{
			return (SqlByte)this;
		}

		/// <summary>Converts this <see cref="T:System.Data.SqlTypes.SqlInt16" /> structure to <see cref="T:System.Data.SqlTypes.SqlDouble" />.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlDouble" /> structure whose <see langword="Value" /> equals the value of this <see cref="T:System.Data.SqlTypes.SqlInt16" /> structure.</returns>
		// Token: 0x0600247A RID: 9338 RVA: 0x000A6385 File Offset: 0x000A4585
		public SqlDouble ToSqlDouble()
		{
			return this;
		}

		/// <summary>Converts this <see cref="T:System.Data.SqlTypes.SqlInt16" /> structure to <see cref="T:System.Data.SqlTypes.SqlInt32" />.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlInt32" /> structure whose <see langword="Value" /> equals the value of this <see cref="T:System.Data.SqlTypes.SqlInt16" /> structure.</returns>
		// Token: 0x0600247B RID: 9339 RVA: 0x000A6392 File Offset: 0x000A4592
		public SqlInt32 ToSqlInt32()
		{
			return this;
		}

		/// <summary>Converts this <see cref="T:System.Data.SqlTypes.SqlInt16" /> structure to <see cref="T:System.Data.SqlTypes.SqlInt64" />.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlInt64" /> structure whose <see langword="Value" /> equals the value of this <see cref="T:System.Data.SqlTypes.SqlInt16" /> structure.</returns>
		// Token: 0x0600247C RID: 9340 RVA: 0x000A639F File Offset: 0x000A459F
		public SqlInt64 ToSqlInt64()
		{
			return this;
		}

		/// <summary>Converts this <see cref="T:System.Data.SqlTypes.SqlInt16" /> structure to <see cref="T:System.Data.SqlTypes.SqlMoney" />.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlMoney" /> structure whose <see langword="Value" /> equals the value of this <see cref="T:System.Data.SqlTypes.SqlInt16" /> structure.</returns>
		// Token: 0x0600247D RID: 9341 RVA: 0x000A63AC File Offset: 0x000A45AC
		public SqlMoney ToSqlMoney()
		{
			return this;
		}

		/// <summary>Converts this <see cref="T:System.Data.SqlTypes.SqlInt16" /> structure to <see cref="T:System.Data.SqlTypes.SqlDecimal" />.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure whose <see langword="Value" /> equals the value of this <see cref="T:System.Data.SqlTypes.SqlInt16" /> structure.</returns>
		// Token: 0x0600247E RID: 9342 RVA: 0x000A63B9 File Offset: 0x000A45B9
		public SqlDecimal ToSqlDecimal()
		{
			return this;
		}

		/// <summary>Converts this <see cref="T:System.Data.SqlTypes.SqlInt16" /> structure to <see cref="T:System.Data.SqlTypes.SqlSingle" />.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlSingle" /> structure whose <see langword="Value" /> equals the value of this <see cref="T:System.Data.SqlTypes.SqlInt16" /> structure.</returns>
		// Token: 0x0600247F RID: 9343 RVA: 0x000A63C6 File Offset: 0x000A45C6
		public SqlSingle ToSqlSingle()
		{
			return this;
		}

		/// <summary>Converts this <see cref="T:System.Data.SqlTypes.SqlInt16" /> structure to <see cref="T:System.Data.SqlTypes.SqlString" />.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlString" /> representing the <see cref="P:System.Data.SqlTypes.SqlInt16.Value" /> of this instance of <see cref="T:System.Data.SqlTypes.SqlInt16" />.</returns>
		// Token: 0x06002480 RID: 9344 RVA: 0x000A63D3 File Offset: 0x000A45D3
		public SqlString ToSqlString()
		{
			return (SqlString)this;
		}

		/// <summary>Compares this <see cref="T:System.Data.SqlTypes.SqlInt16" /> instance to the supplied <see cref="T:System.Object" /> and returns an indication of their relative values.</summary>
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
		///  object is a null reference (<see langword="Nothing" /> in Visual Basic)</returns>
		// Token: 0x06002481 RID: 9345 RVA: 0x000A63E0 File Offset: 0x000A45E0
		public int CompareTo(object value)
		{
			if (value is SqlInt16)
			{
				SqlInt16 value2 = (SqlInt16)value;
				return this.CompareTo(value2);
			}
			throw ADP.WrongType(value.GetType(), typeof(SqlInt16));
		}

		/// <summary>Compares this <see cref="T:System.Data.SqlTypes.SqlInt16" /> instance to the supplied <see cref="T:System.Data.SqlTypes.SqlInt16" /> and returns an indication of their relative values.</summary>
		/// <param name="value">The <see cref="T:System.Data.SqlTypes.SqlInt16" /> to be compared.</param>
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
		// Token: 0x06002482 RID: 9346 RVA: 0x000A641C File Offset: 0x000A461C
		public int CompareTo(SqlInt16 value)
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

		/// <summary>Compares the specified object to the <see cref="P:System.Data.SqlTypes.SqlInt16.Value" /> property of the <see cref="T:System.Data.SqlTypes.SqlInt16" /> object.</summary>
		/// <param name="value">The object to be compared.</param>
		/// <returns>
		///   <see langword="true" /> if object is an instance of <see cref="T:System.Data.SqlTypes.SqlInt16" /> and the two are equal; otherwise <see langword="false" />.</returns>
		// Token: 0x06002483 RID: 9347 RVA: 0x000A6474 File Offset: 0x000A4674
		public override bool Equals(object value)
		{
			if (!(value is SqlInt16))
			{
				return false;
			}
			SqlInt16 y = (SqlInt16)value;
			if (y.IsNull || this.IsNull)
			{
				return y.IsNull && this.IsNull;
			}
			return (this == y).Value;
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		// Token: 0x06002484 RID: 9348 RVA: 0x000A64CC File Offset: 0x000A46CC
		public override int GetHashCode()
		{
			if (!this.IsNull)
			{
				return this.Value.GetHashCode();
			}
			return 0;
		}

		/// <summary>This member supports the .NET Framework infrastructure and is not intended to be used directly from your code.</summary>
		/// <returns>An XML schema consumed by .NET Framework.</returns>
		// Token: 0x06002485 RID: 9349 RVA: 0x00003E32 File Offset: 0x00002032
		XmlSchema IXmlSerializable.GetSchema()
		{
			return null;
		}

		/// <summary>This member supports the .NET Framework infrastructure and is not intended to be used directly from your code.</summary>
		/// <param name="reader">
		///   <see langword="XmlReader" />
		/// </param>
		// Token: 0x06002486 RID: 9350 RVA: 0x000A64F4 File Offset: 0x000A46F4
		void IXmlSerializable.ReadXml(XmlReader reader)
		{
			string attribute = reader.GetAttribute("nil", "http://www.w3.org/2001/XMLSchema-instance");
			if (attribute != null && XmlConvert.ToBoolean(attribute))
			{
				reader.ReadElementString();
				this.m_fNotNull = false;
				return;
			}
			this.m_value = XmlConvert.ToInt16(reader.ReadElementString());
			this.m_fNotNull = true;
		}

		/// <summary>This member supports the .NET Framework infrastructure and is not intended to be used directly from your code.</summary>
		/// <param name="writer">
		///   <see langword="XmlWriter" />
		/// </param>
		// Token: 0x06002487 RID: 9351 RVA: 0x000A6544 File Offset: 0x000A4744
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
		/// <returns>A <see cref="T:System.String" /> value that indicates the XSD of the specified <see cref="T:System.Xml.Schema.XmlSchemaSet" />.</returns>
		// Token: 0x06002488 RID: 9352 RVA: 0x000A657A File Offset: 0x000A477A
		public static XmlQualifiedName GetXsdType(XmlSchemaSet schemaSet)
		{
			return new XmlQualifiedName("short", "http://www.w3.org/2001/XMLSchema");
		}

		// Token: 0x06002489 RID: 9353 RVA: 0x000A658B File Offset: 0x000A478B
		// Note: this type is marked as 'beforefieldinit'.
		static SqlInt16()
		{
		}

		// Token: 0x040018B2 RID: 6322
		private bool m_fNotNull;

		// Token: 0x040018B3 RID: 6323
		private short m_value;

		// Token: 0x040018B4 RID: 6324
		private static readonly int s_MASKI2 = -32768;

		/// <summary>Represents a <see cref="T:System.DBNull" /> that can be assigned to this instance of the <see cref="T:System.Data.SqlTypes.SqlInt16" /> structure.</summary>
		// Token: 0x040018B5 RID: 6325
		public static readonly SqlInt16 Null = new SqlInt16(true);

		/// <summary>Represents a zero value that can be assigned to the <see cref="P:System.Data.SqlTypes.SqlInt16.Value" /> property of an instance of the <see cref="T:System.Data.SqlTypes.SqlInt16" /> structure.</summary>
		// Token: 0x040018B6 RID: 6326
		public static readonly SqlInt16 Zero = new SqlInt16(0);

		/// <summary>A constant representing the smallest possible value of a <see cref="T:System.Data.SqlTypes.SqlInt16" />.</summary>
		// Token: 0x040018B7 RID: 6327
		public static readonly SqlInt16 MinValue = new SqlInt16(short.MinValue);

		/// <summary>A constant representing the largest possible value of a <see cref="T:System.Data.SqlTypes.SqlInt16" />.</summary>
		// Token: 0x040018B8 RID: 6328
		public static readonly SqlInt16 MaxValue = new SqlInt16(short.MaxValue);
	}
}

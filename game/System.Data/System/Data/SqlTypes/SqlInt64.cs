using System;
using System.Data.Common;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace System.Data.SqlTypes
{
	/// <summary>Represents a 64-bit signed integer to be stored in or retrieved from a database.</summary>
	// Token: 0x02000313 RID: 787
	[XmlSchemaProvider("GetXsdType")]
	[Serializable]
	public struct SqlInt64 : INullable, IComparable, IXmlSerializable
	{
		// Token: 0x060024CE RID: 9422 RVA: 0x000A6F07 File Offset: 0x000A5107
		private SqlInt64(bool fNull)
		{
			this.m_fNotNull = false;
			this.m_value = 0L;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlTypes.SqlInt64" /> structure using the supplied long integer.</summary>
		/// <param name="value">A long integer.</param>
		// Token: 0x060024CF RID: 9423 RVA: 0x000A6F18 File Offset: 0x000A5118
		public SqlInt64(long value)
		{
			this.m_value = value;
			this.m_fNotNull = true;
		}

		/// <summary>Gets a Boolean value that indicates whether this <see cref="T:System.Data.SqlTypes.SqlInt64" /> structure is null.</summary>
		/// <returns>
		///   <see langword="true" /> if null. Otherwise, <see langword="false" />.</returns>
		// Token: 0x17000663 RID: 1635
		// (get) Token: 0x060024D0 RID: 9424 RVA: 0x000A6F28 File Offset: 0x000A5128
		public bool IsNull
		{
			get
			{
				return !this.m_fNotNull;
			}
		}

		/// <summary>Gets the value of this <see cref="T:System.Data.SqlTypes.SqlInt64" /> structure. This property is read-only.</summary>
		/// <returns>A long integer representing the value of this <see cref="T:System.Data.SqlTypes.SqlInt64" /> structure.</returns>
		// Token: 0x17000664 RID: 1636
		// (get) Token: 0x060024D1 RID: 9425 RVA: 0x000A6F33 File Offset: 0x000A5133
		public long Value
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

		/// <summary>Converts the long parameter to <see cref="T:System.Data.SqlTypes.SqlInt64" />.</summary>
		/// <param name="x">A long integer value.</param>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlInt64" /> structure whose <see cref="P:System.Data.SqlTypes.SqlInt64.Value" /> equals the value of the long parameter.</returns>
		// Token: 0x060024D2 RID: 9426 RVA: 0x000A6F49 File Offset: 0x000A5149
		public static implicit operator SqlInt64(long x)
		{
			return new SqlInt64(x);
		}

		/// <summary>Converts the <see cref="T:System.Data.SqlTypes.SqlInt64" /> parameter to long.</summary>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlInt64" /> structure.</param>
		/// <returns>A new long value equal to the <see cref="P:System.Data.SqlTypes.SqlInt64.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlInt64" />.</returns>
		// Token: 0x060024D3 RID: 9427 RVA: 0x000A6F51 File Offset: 0x000A5151
		public static explicit operator long(SqlInt64 x)
		{
			return x.Value;
		}

		/// <summary>Converts this instance of <see cref="T:System.Data.SqlTypes.SqlInt64" /> to <see cref="T:System.String" />.</summary>
		/// <returns>A <see cref="T:System.String" /> representing the value of this <see cref="T:System.Data.SqlTypes.SqlInt64" />.</returns>
		// Token: 0x060024D4 RID: 9428 RVA: 0x000A6F5A File Offset: 0x000A515A
		public override string ToString()
		{
			if (!this.IsNull)
			{
				return this.m_value.ToString(null);
			}
			return SQLResource.NullString;
		}

		/// <summary>Converts the <see cref="T:System.String" /> representation of a number to its 64-bit signed integer equivalent.</summary>
		/// <param name="s">The <see cref="T:System.String" /> to be parsed.</param>
		/// <returns>A 64-bit signed integer equivalent to the value that is contained in the specified <see cref="T:System.String" />.</returns>
		// Token: 0x060024D5 RID: 9429 RVA: 0x000A6F76 File Offset: 0x000A5176
		public static SqlInt64 Parse(string s)
		{
			if (s == SQLResource.NullString)
			{
				return SqlInt64.Null;
			}
			return new SqlInt64(long.Parse(s, null));
		}

		/// <summary>The unary minus operator negates the <see cref="P:System.Data.SqlTypes.SqlInt64.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlInt64" /> operand.</summary>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlInt64" /> structure.</param>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlInt64" /> structure whose <see cref="P:System.Data.SqlTypes.SqlInt64.Value" /> is equal to the negated <see cref="P:System.Data.SqlTypes.SqlInt64.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlInt64" /> parameter.</returns>
		// Token: 0x060024D6 RID: 9430 RVA: 0x000A6F97 File Offset: 0x000A5197
		public static SqlInt64 operator -(SqlInt64 x)
		{
			if (!x.IsNull)
			{
				return new SqlInt64(-x.m_value);
			}
			return SqlInt64.Null;
		}

		/// <summary>Performs a bitwise one's complement operation on its <see cref="T:System.Data.SqlTypes.SqlInt64" /> operand.</summary>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlInt64" /> structure.</param>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlInt64" /> structure whose <see cref="P:System.Data.SqlTypes.SqlInt64.Value" /> is equal to the ones complement of the <see cref="T:System.Data.SqlTypes.SqlInt64" /> parameter.</returns>
		// Token: 0x060024D7 RID: 9431 RVA: 0x000A6FB4 File Offset: 0x000A51B4
		public static SqlInt64 operator ~(SqlInt64 x)
		{
			if (!x.IsNull)
			{
				return new SqlInt64(~x.m_value);
			}
			return SqlInt64.Null;
		}

		/// <summary>Computes the sum of the two <see cref="T:System.Data.SqlTypes.SqlInt64" /> parameters.</summary>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlInt64" /> structure.</param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlInt64" /> structure.</param>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlInt64" /> structure whose <see cref="P:System.Data.SqlTypes.SqlInt64.Value" /> is equal to the sum of the two <see cref="T:System.Data.SqlTypes.SqlInt64" /> parameters.</returns>
		// Token: 0x060024D8 RID: 9432 RVA: 0x000A6FD4 File Offset: 0x000A51D4
		public static SqlInt64 operator +(SqlInt64 x, SqlInt64 y)
		{
			if (x.IsNull || y.IsNull)
			{
				return SqlInt64.Null;
			}
			long num = x.m_value + y.m_value;
			if (SqlInt64.SameSignLong(x.m_value, y.m_value) && !SqlInt64.SameSignLong(x.m_value, num))
			{
				throw new OverflowException(SQLResource.ArithOverflowMessage);
			}
			return new SqlInt64(num);
		}

		/// <summary>Subtracts the second <see cref="T:System.Data.SqlTypes.SqlInt64" /> parameter from the first.</summary>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlInt64" /> structure.</param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlInt64" /> structure.</param>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlInt64" /> structure whose <see cref="P:System.Data.SqlTypes.SqlInt64.Value" /> property equals the results of the subtraction operation.</returns>
		// Token: 0x060024D9 RID: 9433 RVA: 0x000A703C File Offset: 0x000A523C
		public static SqlInt64 operator -(SqlInt64 x, SqlInt64 y)
		{
			if (x.IsNull || y.IsNull)
			{
				return SqlInt64.Null;
			}
			long num = x.m_value - y.m_value;
			if (!SqlInt64.SameSignLong(x.m_value, y.m_value) && SqlInt64.SameSignLong(y.m_value, num))
			{
				throw new OverflowException(SQLResource.ArithOverflowMessage);
			}
			return new SqlInt64(num);
		}

		/// <summary>Computes the product of the two <see cref="T:System.Data.SqlTypes.SqlInt64" /> parameters.</summary>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlInt64" /> structure.</param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlInt64" /> structure.</param>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlInt64" /> structure whose <see cref="P:System.Data.SqlTypes.SqlInt64.Value" /> is equal to the product of the two <see cref="T:System.Data.SqlTypes.SqlInt64" /> parameters.</returns>
		// Token: 0x060024DA RID: 9434 RVA: 0x000A70A4 File Offset: 0x000A52A4
		public static SqlInt64 operator *(SqlInt64 x, SqlInt64 y)
		{
			if (x.IsNull || y.IsNull)
			{
				return SqlInt64.Null;
			}
			bool flag = false;
			long num = x.m_value;
			long num2 = y.m_value;
			long num3 = 0L;
			if (num < 0L)
			{
				flag = true;
				num = -num;
			}
			if (num2 < 0L)
			{
				flag = !flag;
				num2 = -num2;
			}
			long num4 = num & SqlInt64.s_lLowIntMask;
			long num5 = num >> 32 & SqlInt64.s_lLowIntMask;
			long num6 = num2 & SqlInt64.s_lLowIntMask;
			long num7 = num2 >> 32 & SqlInt64.s_lLowIntMask;
			if (num5 != 0L && num7 != 0L)
			{
				throw new OverflowException(SQLResource.ArithOverflowMessage);
			}
			long num8 = num4 * num6;
			if (num8 < 0L)
			{
				throw new OverflowException(SQLResource.ArithOverflowMessage);
			}
			if (num5 != 0L)
			{
				num3 = num5 * num6;
				if (num3 < 0L || num3 > 9223372036854775807L)
				{
					throw new OverflowException(SQLResource.ArithOverflowMessage);
				}
			}
			else if (num7 != 0L)
			{
				num3 = num4 * num7;
				if (num3 < 0L || num3 > 9223372036854775807L)
				{
					throw new OverflowException(SQLResource.ArithOverflowMessage);
				}
			}
			num8 += num3 << 32;
			if (num8 < 0L)
			{
				throw new OverflowException(SQLResource.ArithOverflowMessage);
			}
			if (flag)
			{
				num8 = -num8;
			}
			return new SqlInt64(num8);
		}

		/// <summary>Divides the first <see cref="T:System.Data.SqlTypes.SqlInt64" /> parameter by the second.</summary>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlInt64" /> structure.</param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlInt64" /> structure.</param>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlInt64" /> structure whose <see cref="P:System.Data.SqlTypes.SqlInt64.Value" /> property contains the results of the division operation.</returns>
		// Token: 0x060024DB RID: 9435 RVA: 0x000A71C0 File Offset: 0x000A53C0
		public static SqlInt64 operator /(SqlInt64 x, SqlInt64 y)
		{
			if (x.IsNull || y.IsNull)
			{
				return SqlInt64.Null;
			}
			if (y.m_value == 0L)
			{
				throw new DivideByZeroException(SQLResource.DivideByZeroMessage);
			}
			if (x.m_value == -9223372036854775808L && y.m_value == -1L)
			{
				throw new OverflowException(SQLResource.ArithOverflowMessage);
			}
			return new SqlInt64(x.m_value / y.m_value);
		}

		/// <summary>Computes the remainder after dividing the first <see cref="T:System.Data.SqlTypes.SqlInt64" /> parameter by the second.</summary>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlInt64" /> structure.</param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlInt64" /> structure.</param>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlInt64" /> structure whose <see cref="P:System.Data.SqlTypes.SqlInt64.Value" /> property contains the remainder.</returns>
		// Token: 0x060024DC RID: 9436 RVA: 0x000A7230 File Offset: 0x000A5430
		public static SqlInt64 operator %(SqlInt64 x, SqlInt64 y)
		{
			if (x.IsNull || y.IsNull)
			{
				return SqlInt64.Null;
			}
			if (y.m_value == 0L)
			{
				throw new DivideByZeroException(SQLResource.DivideByZeroMessage);
			}
			if (x.m_value == -9223372036854775808L && y.m_value == -1L)
			{
				throw new OverflowException(SQLResource.ArithOverflowMessage);
			}
			return new SqlInt64(x.m_value % y.m_value);
		}

		/// <summary>Computes the bitwise AND of its <see cref="T:System.Data.SqlTypes.SqlInt64" /> operands.</summary>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlInt64" /> structure.</param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlInt64" /> structure.</param>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlInt64" /> structure that contains the results of the bitwise AND operation.</returns>
		// Token: 0x060024DD RID: 9437 RVA: 0x000A72A0 File Offset: 0x000A54A0
		public static SqlInt64 operator &(SqlInt64 x, SqlInt64 y)
		{
			if (!x.IsNull && !y.IsNull)
			{
				return new SqlInt64(x.m_value & y.m_value);
			}
			return SqlInt64.Null;
		}

		/// <summary>Computes the bitwise OR of its two <see cref="T:System.Data.SqlTypes.SqlInt64" /> operands.</summary>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlInt64" /> structure.</param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlInt64" /> structure.</param>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlInt64" /> structure that contains the results of the bitwise OR operation.</returns>
		// Token: 0x060024DE RID: 9438 RVA: 0x000A72CC File Offset: 0x000A54CC
		public static SqlInt64 operator |(SqlInt64 x, SqlInt64 y)
		{
			if (!x.IsNull && !y.IsNull)
			{
				return new SqlInt64(x.m_value | y.m_value);
			}
			return SqlInt64.Null;
		}

		/// <summary>Performs a bitwise exclusive-OR operation on the supplied parameters.</summary>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlInt64" /> structure.</param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlInt64" /> structure.</param>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlInt64" /> structure that contains the results of the bitwise XOR operation.</returns>
		// Token: 0x060024DF RID: 9439 RVA: 0x000A72F8 File Offset: 0x000A54F8
		public static SqlInt64 operator ^(SqlInt64 x, SqlInt64 y)
		{
			if (!x.IsNull && !y.IsNull)
			{
				return new SqlInt64(x.m_value ^ y.m_value);
			}
			return SqlInt64.Null;
		}

		/// <summary>Converts the supplied <see cref="T:System.Data.SqlTypes.SqlBoolean" /> parameter to <see cref="T:System.Data.SqlTypes.SqlInt64" />.</summary>
		/// <param name="x">The <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure to be converted.</param>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlInt64" /> structure whose <see cref="P:System.Data.SqlTypes.SqlInt64.Value" /> property is equal to the <see cref="P:System.Data.SqlTypes.SqlBoolean.ByteValue" /> of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> parameter.</returns>
		// Token: 0x060024E0 RID: 9440 RVA: 0x000A7324 File Offset: 0x000A5524
		public static explicit operator SqlInt64(SqlBoolean x)
		{
			if (!x.IsNull)
			{
				return new SqlInt64((long)((ulong)x.ByteValue));
			}
			return SqlInt64.Null;
		}

		/// <summary>Converts the supplied <see cref="T:System.Data.SqlTypes.SqlByte" /> parameter to <see cref="T:System.Data.SqlTypes.SqlInt64" />.</summary>
		/// <param name="x">The <see cref="T:System.Data.SqlTypes.SqlByte" /> structure to be converted.</param>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlInt64" /> structure whose <see cref="P:System.Data.SqlTypes.SqlInt64.Value" /> property equals the <see cref="P:System.Data.SqlTypes.SqlByte.Value" /> property of the <see cref="T:System.Data.SqlTypes.SqlByte" /> parameter.</returns>
		// Token: 0x060024E1 RID: 9441 RVA: 0x000A7342 File Offset: 0x000A5542
		public static implicit operator SqlInt64(SqlByte x)
		{
			if (!x.IsNull)
			{
				return new SqlInt64((long)((ulong)x.Value));
			}
			return SqlInt64.Null;
		}

		/// <summary>Converts the supplied <see cref="T:System.Data.SqlTypes.SqlInt16" /> parameter to <see cref="T:System.Data.SqlTypes.SqlInt64" />.</summary>
		/// <param name="x">The <see cref="T:System.Data.SqlTypes.SqlInt16" /> structure to be converted.</param>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlInt64" /> structure whose <see cref="P:System.Data.SqlTypes.SqlInt64.Value" /> property equals the <see cref="P:System.Data.SqlTypes.SqlInt16.Value" /> property of the <see cref="T:System.Data.SqlTypes.SqlInt16" /> parameter.</returns>
		// Token: 0x060024E2 RID: 9442 RVA: 0x000A7360 File Offset: 0x000A5560
		public static implicit operator SqlInt64(SqlInt16 x)
		{
			if (!x.IsNull)
			{
				return new SqlInt64((long)x.Value);
			}
			return SqlInt64.Null;
		}

		/// <summary>Converts the supplied <see cref="T:System.Data.SqlTypes.SqlInt32" /> parameter to <see cref="T:System.Data.SqlTypes.SqlInt64" />.</summary>
		/// <param name="x">The <see cref="T:System.Data.SqlTypes.SqlInt32" /> structure to be converted.</param>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlInt64" /> structure whose <see cref="P:System.Data.SqlTypes.SqlInt64.Value" /> property equals the <see cref="P:System.Data.SqlTypes.SqlInt32.Value" /> property of the <see cref="T:System.Data.SqlTypes.SqlInt32" /> parameter.</returns>
		// Token: 0x060024E3 RID: 9443 RVA: 0x000A737E File Offset: 0x000A557E
		public static implicit operator SqlInt64(SqlInt32 x)
		{
			if (!x.IsNull)
			{
				return new SqlInt64((long)x.Value);
			}
			return SqlInt64.Null;
		}

		/// <summary>Converts the supplied <see cref="T:System.Data.SqlTypes.SqlSingle" /> parameter to <see cref="T:System.Data.SqlTypes.SqlInt64" />.</summary>
		/// <param name="x">The <see cref="T:System.Data.SqlTypes.SqlSingle" /> structure to be converted.</param>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlInt64" /> structure whose <see cref="P:System.Data.SqlTypes.SqlInt64.Value" /> property contains the integer part of the <see cref="T:System.Data.SqlTypes.SqlSingle" /> parameter.</returns>
		// Token: 0x060024E4 RID: 9444 RVA: 0x000A739C File Offset: 0x000A559C
		public static explicit operator SqlInt64(SqlSingle x)
		{
			if (x.IsNull)
			{
				return SqlInt64.Null;
			}
			float value = x.Value;
			if (value > 9.223372E+18f || value < -9.223372E+18f)
			{
				throw new OverflowException(SQLResource.ArithOverflowMessage);
			}
			return new SqlInt64((long)value);
		}

		/// <summary>Converts the supplied <see cref="T:System.Data.SqlTypes.SqlDouble" /> structure to <see cref="T:System.Data.SqlTypes.SqlInt64" />.</summary>
		/// <param name="x">The <see cref="T:System.Data.SqlTypes.SqlDouble" /> structure to be converted.</param>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlInt64" /> structure whose <see cref="P:System.Data.SqlTypes.SqlInt64.Value" /> property equals the integer part of the <see cref="T:System.Data.SqlTypes.SqlDouble" /> parameter.</returns>
		// Token: 0x060024E5 RID: 9445 RVA: 0x000A73E4 File Offset: 0x000A55E4
		public static explicit operator SqlInt64(SqlDouble x)
		{
			if (x.IsNull)
			{
				return SqlInt64.Null;
			}
			double value = x.Value;
			if (value > 9.223372036854776E+18 || value < -9.223372036854776E+18)
			{
				throw new OverflowException(SQLResource.ArithOverflowMessage);
			}
			return new SqlInt64((long)value);
		}

		/// <summary>Converts the supplied <see cref="T:System.Data.SqlTypes.SqlMoney" /> parameter to <see cref="T:System.Data.SqlTypes.SqlInt64" />.</summary>
		/// <param name="x">The <see cref="T:System.Data.SqlTypes.SqlMoney" /> structure to be converted.</param>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlInt64" /> structure whose <see cref="P:System.Data.SqlTypes.SqlInt64.Value" /> property equals the integer part of the <see cref="T:System.Data.SqlTypes.SqlMoney" /> parameter.</returns>
		// Token: 0x060024E6 RID: 9446 RVA: 0x000A7432 File Offset: 0x000A5632
		public static explicit operator SqlInt64(SqlMoney x)
		{
			if (!x.IsNull)
			{
				return new SqlInt64(x.ToInt64());
			}
			return SqlInt64.Null;
		}

		/// <summary>Converts the supplied <see cref="T:System.Data.SqlTypes.SqlDecimal" /> parameter to <see cref="T:System.Data.SqlTypes.SqlInt64" />.</summary>
		/// <param name="x">The <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure to be converted.</param>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlInt64" /> structure whose <see cref="P:System.Data.SqlTypes.SqlInt64.Value" /> is equal to the integer part of the <see cref="T:System.Data.SqlTypes.SqlDecimal" /> parameter.</returns>
		// Token: 0x060024E7 RID: 9447 RVA: 0x000A7450 File Offset: 0x000A5650
		public static explicit operator SqlInt64(SqlDecimal x)
		{
			if (x.IsNull)
			{
				return SqlInt64.Null;
			}
			SqlDecimal sqlDecimal = x;
			sqlDecimal.AdjustScale((int)(-(int)sqlDecimal._bScale), false);
			if (sqlDecimal._bLen > 2)
			{
				throw new OverflowException(SQLResource.ConversionOverflowMessage);
			}
			long num2;
			if (sqlDecimal._bLen == 2)
			{
				ulong num = SqlDecimal.DWL(sqlDecimal._data1, sqlDecimal._data2);
				if (num > SqlDecimal.s_llMax && (sqlDecimal.IsPositive || num != 1UL + SqlDecimal.s_llMax))
				{
					throw new OverflowException(SQLResource.ConversionOverflowMessage);
				}
				num2 = (long)num;
			}
			else
			{
				num2 = (long)((ulong)sqlDecimal._data1);
			}
			if (!sqlDecimal.IsPositive)
			{
				num2 = -num2;
			}
			return new SqlInt64(num2);
		}

		/// <summary>Converts the supplied <see cref="T:System.Data.SqlTypes.SqlString" /> parameter to <see cref="T:System.Data.SqlTypes.SqlInt64" />.</summary>
		/// <param name="x">The <see cref="T:System.Data.SqlTypes.SqlString" /> object to be converted.</param>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlInt64" /> whose <see cref="P:System.Data.SqlTypes.SqlInt64.Value" /> is equal to the value represented by the <see cref="T:System.Data.SqlTypes.SqlString" /> parameter.</returns>
		// Token: 0x060024E8 RID: 9448 RVA: 0x000A74F1 File Offset: 0x000A56F1
		public static explicit operator SqlInt64(SqlString x)
		{
			if (!x.IsNull)
			{
				return new SqlInt64(long.Parse(x.Value, null));
			}
			return SqlInt64.Null;
		}

		// Token: 0x060024E9 RID: 9449 RVA: 0x000A7514 File Offset: 0x000A5714
		private static bool SameSignLong(long x, long y)
		{
			return ((x ^ y) & long.MinValue) == 0L;
		}

		/// <summary>Performs a logical comparison of the two <see cref="T:System.Data.SqlTypes.SqlInt64" /> parameters to determine whether they are equal.</summary>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlInt64" /> structure.</param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlInt64" /> structure.</param>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> that is <see cref="F:System.Data.SqlTypes.SqlBoolean.True" /> if the two instances are equal or <see cref="F:System.Data.SqlTypes.SqlBoolean.False" /> if the two instances are not equal. If either instance of <see cref="T:System.Data.SqlTypes.SqlInt64" /> is null, the <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> will be <see cref="F:System.Data.SqlTypes.SqlBoolean.Null" />.</returns>
		// Token: 0x060024EA RID: 9450 RVA: 0x000A7527 File Offset: 0x000A5727
		public static SqlBoolean operator ==(SqlInt64 x, SqlInt64 y)
		{
			if (!x.IsNull && !y.IsNull)
			{
				return new SqlBoolean(x.m_value == y.m_value);
			}
			return SqlBoolean.Null;
		}

		/// <summary>Performs a logical comparison on the two SqlInt64 parameters to determine whether they are not equal.</summary>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlInt64" /> structure.</param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlInt64" /> structure.</param>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> that is <see cref="F:System.Data.SqlTypes.SqlBoolean.True" /> if the two instances are not equal or <see cref="F:System.Data.SqlTypes.SqlBoolean.False" /> if the two instances are equal. If either instance of <see cref="T:System.Data.SqlTypes.SqlInt64" /> is null, the <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> will be <see cref="F:System.Data.SqlTypes.SqlBoolean.Null" />.</returns>
		// Token: 0x060024EB RID: 9451 RVA: 0x000A7554 File Offset: 0x000A5754
		public static SqlBoolean operator !=(SqlInt64 x, SqlInt64 y)
		{
			return !(x == y);
		}

		/// <summary>Performs a logical comparison on the two <see cref="T:System.Data.SqlTypes.SqlInt64" /> parameters to determine whether the first is less than the second.</summary>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlInt64" /> structure.</param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlInt64" /> structure.</param>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> that is <see cref="F:System.Data.SqlTypes.SqlBoolean.True" /> if the first instance is less than the second instance. Otherwise, <see cref="F:System.Data.SqlTypes.SqlBoolean.False" />. If either instance of <see cref="T:System.Data.SqlTypes.SqlInt64" /> is null, the <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> will be <see cref="F:System.Data.SqlTypes.SqlBoolean.Null" />.</returns>
		// Token: 0x060024EC RID: 9452 RVA: 0x000A7562 File Offset: 0x000A5762
		public static SqlBoolean operator <(SqlInt64 x, SqlInt64 y)
		{
			if (!x.IsNull && !y.IsNull)
			{
				return new SqlBoolean(x.m_value < y.m_value);
			}
			return SqlBoolean.Null;
		}

		/// <summary>Performs a logical comparison of the two <see cref="T:System.Data.SqlTypes.SqlInt64" /> parameters to determine whether the first is greater than the second.</summary>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlInt64" /> structure.</param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlInt64" /> structure.</param>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> that is <see cref="F:System.Data.SqlTypes.SqlBoolean.True" /> if the first instance is greater than the second instance. Otherwise, <see cref="F:System.Data.SqlTypes.SqlBoolean.False" />. If either instance of <see cref="T:System.Data.SqlTypes.SqlInt64" /> is null, the <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> will be <see cref="F:System.Data.SqlTypes.SqlBoolean.Null" />.</returns>
		// Token: 0x060024ED RID: 9453 RVA: 0x000A758F File Offset: 0x000A578F
		public static SqlBoolean operator >(SqlInt64 x, SqlInt64 y)
		{
			if (!x.IsNull && !y.IsNull)
			{
				return new SqlBoolean(x.m_value > y.m_value);
			}
			return SqlBoolean.Null;
		}

		/// <summary>Performs a logical comparison on the two <see cref="T:System.Data.SqlTypes.SqlInt64" /> parameters to determine whether the first is less than or equal to the second.</summary>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlInt64" /> structure.</param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlInt64" /> structure.</param>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> that is <see cref="F:System.Data.SqlTypes.SqlBoolean.True" /> if the first instance is less than or equal to the second instance. Otherwise, <see cref="F:System.Data.SqlTypes.SqlBoolean.False" />. If either instance of <see cref="T:System.Data.SqlTypes.SqlInt64" /> is null, the <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> will be <see cref="F:System.Data.SqlTypes.SqlBoolean.Null" />.</returns>
		// Token: 0x060024EE RID: 9454 RVA: 0x000A75BC File Offset: 0x000A57BC
		public static SqlBoolean operator <=(SqlInt64 x, SqlInt64 y)
		{
			if (!x.IsNull && !y.IsNull)
			{
				return new SqlBoolean(x.m_value <= y.m_value);
			}
			return SqlBoolean.Null;
		}

		/// <summary>Performs a logical comparison of the two <see cref="T:System.Data.SqlTypes.SqlInt64" /> parameters to determine whether the first is greater than or equal to the second.</summary>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlInt64" /> structure.</param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlInt64" /> structure.</param>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> that is <see cref="F:System.Data.SqlTypes.SqlBoolean.True" /> if the first instance is greater than or equal to the second instance. Otherwise, <see cref="F:System.Data.SqlTypes.SqlBoolean.False" />. If either instance of <see cref="T:System.Data.SqlTypes.SqlInt64" /> is null, the <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> will be <see cref="F:System.Data.SqlTypes.SqlBoolean.Null" />.</returns>
		// Token: 0x060024EF RID: 9455 RVA: 0x000A75EC File Offset: 0x000A57EC
		public static SqlBoolean operator >=(SqlInt64 x, SqlInt64 y)
		{
			if (!x.IsNull && !y.IsNull)
			{
				return new SqlBoolean(x.m_value >= y.m_value);
			}
			return SqlBoolean.Null;
		}

		/// <summary>Performs a bitwise one's complement operation on its <see cref="T:System.Data.SqlTypes.SqlInt64" /> operand.</summary>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlInt64" /> structure.</param>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlInt64" /> structure whose <see cref="P:System.Data.SqlTypes.SqlInt64.Value" /> is equal to the ones complement of the <see cref="T:System.Data.SqlTypes.SqlInt64" /> parameter.</returns>
		// Token: 0x060024F0 RID: 9456 RVA: 0x000A761C File Offset: 0x000A581C
		public static SqlInt64 OnesComplement(SqlInt64 x)
		{
			return ~x;
		}

		/// <summary>Computes the sum of the two <see cref="T:System.Data.SqlTypes.SqlInt64" /> parameters.</summary>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlInt64" /> structure.</param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlInt64" /> structure.</param>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlInt64" /> structure whose <see cref="P:System.Data.SqlTypes.SqlInt64.Value" /> is equal to the sum of the two <see cref="T:System.Data.SqlTypes.SqlInt64" /> parameters.</returns>
		// Token: 0x060024F1 RID: 9457 RVA: 0x000A7624 File Offset: 0x000A5824
		public static SqlInt64 Add(SqlInt64 x, SqlInt64 y)
		{
			return x + y;
		}

		/// <summary>Subtracts the second <see cref="T:System.Data.SqlTypes.SqlInt64" /> parameter from the first.</summary>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlInt64" /> structure.</param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlInt64" /> structure.</param>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlInt64" /> structure whose <see cref="P:System.Data.SqlTypes.SqlInt64.Value" /> property equals the results of the subtraction operation.</returns>
		// Token: 0x060024F2 RID: 9458 RVA: 0x000A762D File Offset: 0x000A582D
		public static SqlInt64 Subtract(SqlInt64 x, SqlInt64 y)
		{
			return x - y;
		}

		/// <summary>Computes the product of the two <see cref="T:System.Data.SqlTypes.SqlInt64" /> parameters.</summary>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlInt64" /> structure.</param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlInt64" /> structure.</param>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlInt64" /> structure whose <see cref="P:System.Data.SqlTypes.SqlInt64.Value" /> is equal to the product of the two <see cref="T:System.Data.SqlTypes.SqlInt64" /> parameters.</returns>
		// Token: 0x060024F3 RID: 9459 RVA: 0x000A7636 File Offset: 0x000A5836
		public static SqlInt64 Multiply(SqlInt64 x, SqlInt64 y)
		{
			return x * y;
		}

		/// <summary>Divides the first <see cref="T:System.Data.SqlTypes.SqlInt64" /> parameter by the second.</summary>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlInt64" /> structure.</param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlInt64" /> structure.</param>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlInt64" /> structure whose <see cref="P:System.Data.SqlTypes.SqlInt64.Value" /> property contains the results of the division operation.</returns>
		// Token: 0x060024F4 RID: 9460 RVA: 0x000A763F File Offset: 0x000A583F
		public static SqlInt64 Divide(SqlInt64 x, SqlInt64 y)
		{
			return x / y;
		}

		/// <summary>Computes the remainder after dividing the first <see cref="T:System.Data.SqlTypes.SqlInt64" /> parameter by the second.</summary>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlInt64" /> structure.</param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlInt64" /> structure.</param>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlInt64" /> structure whose <see cref="P:System.Data.SqlTypes.SqlInt64.Value" /> property contains the remainder.</returns>
		// Token: 0x060024F5 RID: 9461 RVA: 0x000A7648 File Offset: 0x000A5848
		public static SqlInt64 Mod(SqlInt64 x, SqlInt64 y)
		{
			return x % y;
		}

		/// <summary>Divides two <see cref="T:System.Data.SqlTypes.SqlInt64" /> values and returns the remainder.</summary>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlInt64" /> value.</param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlInt64" /> value.</param>
		/// <returns>The remainder left after division is performed on <paramref name="x" /> and <paramref name="y" />.</returns>
		// Token: 0x060024F6 RID: 9462 RVA: 0x000A7648 File Offset: 0x000A5848
		public static SqlInt64 Modulus(SqlInt64 x, SqlInt64 y)
		{
			return x % y;
		}

		/// <summary>Computes the bitwise AND of its <see cref="T:System.Data.SqlTypes.SqlInt64" /> operands.</summary>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlInt64" /> structure.</param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlInt64" /> structure.</param>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlInt64" /> structure that contains the results of the bitwise AND operation.</returns>
		// Token: 0x060024F7 RID: 9463 RVA: 0x000A7651 File Offset: 0x000A5851
		public static SqlInt64 BitwiseAnd(SqlInt64 x, SqlInt64 y)
		{
			return x & y;
		}

		/// <summary>Computes the bitwise OR of its two <see cref="T:System.Data.SqlTypes.SqlInt64" /> operands.</summary>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlInt64" /> structure.</param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlInt64" /> structure.</param>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlInt64" /> structure that contains the results of the bitwise OR operation.</returns>
		// Token: 0x060024F8 RID: 9464 RVA: 0x000A765A File Offset: 0x000A585A
		public static SqlInt64 BitwiseOr(SqlInt64 x, SqlInt64 y)
		{
			return x | y;
		}

		/// <summary>Performs a bitwise exclusive-OR operation on the supplied parameters.</summary>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlInt64" /> structure.</param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlInt64" /> structure.</param>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlInt64" /> structure that contains the results of the bitwise XOR operation.</returns>
		// Token: 0x060024F9 RID: 9465 RVA: 0x000A7663 File Offset: 0x000A5863
		public static SqlInt64 Xor(SqlInt64 x, SqlInt64 y)
		{
			return x ^ y;
		}

		/// <summary>Performs a logical comparison of the two <see cref="T:System.Data.SqlTypes.SqlInt64" /> parameters to determine whether they are equal.</summary>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlInt64" /> structure.</param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlInt64" /> structure.</param>
		/// <returns>
		///   <see langword="true" /> if the two values are equal. Otherwise, <see langword="false" />. If either instance is null, then the <see langword="SqlInt64" /> will be null.</returns>
		// Token: 0x060024FA RID: 9466 RVA: 0x000A766C File Offset: 0x000A586C
		public static SqlBoolean Equals(SqlInt64 x, SqlInt64 y)
		{
			return x == y;
		}

		/// <summary>Performs a logical comparison on the two SqlInt64 parameters to determine whether they are not equal.</summary>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlInt64" /> structure.</param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlInt64" /> structure.</param>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> that is <see cref="F:System.Data.SqlTypes.SqlBoolean.True" /> if the two instances are not equal or <see cref="F:System.Data.SqlTypes.SqlBoolean.False" /> if the two instances are equal. If either instance of <see cref="T:System.Data.SqlTypes.SqlInt64" /> is null, the <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> will be <see cref="F:System.Data.SqlTypes.SqlBoolean.Null" />.</returns>
		// Token: 0x060024FB RID: 9467 RVA: 0x000A7675 File Offset: 0x000A5875
		public static SqlBoolean NotEquals(SqlInt64 x, SqlInt64 y)
		{
			return x != y;
		}

		/// <summary>Performs a logical comparison on the two <see cref="T:System.Data.SqlTypes.SqlInt64" /> parameters to determine whether the first is less than the second.</summary>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlInt64" /> structure.</param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlInt64" /> structure.</param>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> that is <see cref="F:System.Data.SqlTypes.SqlBoolean.True" /> if the first instance is less than the second instance. Otherwise, <see cref="F:System.Data.SqlTypes.SqlBoolean.False" />. If either instance of <see cref="T:System.Data.SqlTypes.SqlInt64" /> is null, the <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> will be <see cref="F:System.Data.SqlTypes.SqlBoolean.Null" />.</returns>
		// Token: 0x060024FC RID: 9468 RVA: 0x000A767E File Offset: 0x000A587E
		public static SqlBoolean LessThan(SqlInt64 x, SqlInt64 y)
		{
			return x < y;
		}

		/// <summary>Performs a logical comparison of the two <see cref="T:System.Data.SqlTypes.SqlInt64" /> parameters to determine whether the first is greater than the second.</summary>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlInt64" /> structure.</param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlInt64" /> structure.</param>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> that is <see cref="F:System.Data.SqlTypes.SqlBoolean.True" /> if the first instance is greater than the second instance. Otherwise, <see cref="F:System.Data.SqlTypes.SqlBoolean.False" />. If either instance of <see cref="T:System.Data.SqlTypes.SqlInt64" /> is null, the <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> will be <see cref="F:System.Data.SqlTypes.SqlBoolean.Null" />.</returns>
		// Token: 0x060024FD RID: 9469 RVA: 0x000A7687 File Offset: 0x000A5887
		public static SqlBoolean GreaterThan(SqlInt64 x, SqlInt64 y)
		{
			return x > y;
		}

		/// <summary>Performs a logical comparison on the two <see cref="T:System.Data.SqlTypes.SqlInt64" /> parameters to determine whether the first is less than or equal to the second.</summary>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlInt64" /> structure.</param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlInt64" /> structure.</param>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> that is <see cref="F:System.Data.SqlTypes.SqlBoolean.True" /> if the first instance is less than or equal to the second instance. Otherwise, <see cref="F:System.Data.SqlTypes.SqlBoolean.False" />. If either instance of <see cref="T:System.Data.SqlTypes.SqlInt64" /> is null, the <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> will be <see cref="F:System.Data.SqlTypes.SqlBoolean.Null" />.</returns>
		// Token: 0x060024FE RID: 9470 RVA: 0x000A7690 File Offset: 0x000A5890
		public static SqlBoolean LessThanOrEqual(SqlInt64 x, SqlInt64 y)
		{
			return x <= y;
		}

		/// <summary>Performs a logical comparison of the two <see cref="T:System.Data.SqlTypes.SqlInt64" /> parameters to determine whether the first is greater than or equal to the second.</summary>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlInt64" /> structure.</param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlInt64" /> structure.</param>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> that is <see cref="F:System.Data.SqlTypes.SqlBoolean.True" /> if the first instance is greater than or equal to the second instance. Otherwise, <see cref="F:System.Data.SqlTypes.SqlBoolean.False" />. If either instance of <see cref="T:System.Data.SqlTypes.SqlInt64" /> is null, the <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> will be <see cref="F:System.Data.SqlTypes.SqlBoolean.Null" />.</returns>
		// Token: 0x060024FF RID: 9471 RVA: 0x000A7699 File Offset: 0x000A5899
		public static SqlBoolean GreaterThanOrEqual(SqlInt64 x, SqlInt64 y)
		{
			return x >= y;
		}

		/// <summary>Converts this <see cref="T:System.Data.SqlTypes.SqlInt64" /> structure to <see cref="T:System.Data.SqlTypes.SqlBoolean" />.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="P:System.Data.SqlTypes.SqlInt64.Value" /> is non-zero; <see langword="false" /> if zero; otherwise Null.</returns>
		// Token: 0x06002500 RID: 9472 RVA: 0x000A76A2 File Offset: 0x000A58A2
		public SqlBoolean ToSqlBoolean()
		{
			return (SqlBoolean)this;
		}

		/// <summary>Converts this <see cref="T:System.Data.SqlTypes.SqlInt64" /> structure to <see cref="T:System.Data.SqlTypes.SqlByte" />.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlByte" /> structure whose <see langword="Value" /> equals the <see langword="Value" /> of this <see cref="T:System.Data.SqlTypes.SqlInt64" /> structure.</returns>
		// Token: 0x06002501 RID: 9473 RVA: 0x000A76AF File Offset: 0x000A58AF
		public SqlByte ToSqlByte()
		{
			return (SqlByte)this;
		}

		/// <summary>Converts this <see cref="T:System.Data.SqlTypes.SqlInt64" /> structure to <see cref="T:System.Data.SqlTypes.SqlDouble" />.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlDouble" /> equal to the value of this <see cref="T:System.Data.SqlTypes.SqlInt64" />.</returns>
		// Token: 0x06002502 RID: 9474 RVA: 0x000A76BC File Offset: 0x000A58BC
		public SqlDouble ToSqlDouble()
		{
			return this;
		}

		/// <summary>Converts this <see cref="T:System.Data.SqlTypes.SqlInt64" /> structure to <see cref="T:System.Data.SqlTypes.SqlInt16" />.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlInt16" /> equal to the value of this <see cref="T:System.Data.SqlTypes.SqlInt64" />.</returns>
		// Token: 0x06002503 RID: 9475 RVA: 0x000A76C9 File Offset: 0x000A58C9
		public SqlInt16 ToSqlInt16()
		{
			return (SqlInt16)this;
		}

		/// <summary>Converts this <see cref="T:System.Data.SqlTypes.SqlInt64" /> structure to <see cref="T:System.Data.SqlTypes.SqlInt32" />.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlInt64" /> equal to the value of this <see cref="T:System.Data.SqlTypes.SqlInt64" />.</returns>
		// Token: 0x06002504 RID: 9476 RVA: 0x000A76D6 File Offset: 0x000A58D6
		public SqlInt32 ToSqlInt32()
		{
			return (SqlInt32)this;
		}

		/// <summary>Converts this <see cref="T:System.Data.SqlTypes.SqlInt64" /> structure to <see cref="T:System.Data.SqlTypes.SqlMoney" />.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlMoney" /> equal to the value of this <see cref="T:System.Data.SqlTypes.SqlInt64" />.</returns>
		// Token: 0x06002505 RID: 9477 RVA: 0x000A76E3 File Offset: 0x000A58E3
		public SqlMoney ToSqlMoney()
		{
			return this;
		}

		/// <summary>Converts this <see cref="T:System.Data.SqlTypes.SqlInt64" /> structure to <see cref="T:System.Data.SqlTypes.SqlDecimal" />.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlDecimal" /> equal to the value of this <see cref="T:System.Data.SqlTypes.SqlInt64" />.</returns>
		// Token: 0x06002506 RID: 9478 RVA: 0x000A76F0 File Offset: 0x000A58F0
		public SqlDecimal ToSqlDecimal()
		{
			return this;
		}

		/// <summary>Converts this <see cref="T:System.Data.SqlTypes.SqlInt64" /> structure to <see cref="T:System.Data.SqlTypes.SqlSingle" />.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlSingle" /> equal to the value of this <see cref="T:System.Data.SqlTypes.SqlInt64" />.</returns>
		// Token: 0x06002507 RID: 9479 RVA: 0x000A76FD File Offset: 0x000A58FD
		public SqlSingle ToSqlSingle()
		{
			return this;
		}

		/// <summary>Converts this <see cref="T:System.Data.SqlTypes.SqlInt64" /> structure to <see cref="T:System.Data.SqlTypes.SqlString" />.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlString" /> representing the value of this <see cref="T:System.Data.SqlTypes.SqlInt64" />.</returns>
		// Token: 0x06002508 RID: 9480 RVA: 0x000A770A File Offset: 0x000A590A
		public SqlString ToSqlString()
		{
			return (SqlString)this;
		}

		/// <summary>Compares this <see cref="T:System.Data.SqlTypes.SqlInt64" /> instance to the supplied <see cref="T:System.Object" /> and returns an indication of their relative values.</summary>
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
		// Token: 0x06002509 RID: 9481 RVA: 0x000A7718 File Offset: 0x000A5918
		public int CompareTo(object value)
		{
			if (value is SqlInt64)
			{
				SqlInt64 value2 = (SqlInt64)value;
				return this.CompareTo(value2);
			}
			throw ADP.WrongType(value.GetType(), typeof(SqlInt64));
		}

		/// <summary>Compares this <see cref="T:System.Data.SqlTypes.SqlInt64" /> instance to the supplied <see cref="T:System.Data.SqlTypes.SqlInt64" /> and returns an indication of their relative values.</summary>
		/// <param name="value">The <see cref="T:System.Data.SqlTypes.SqlInt64" /> to be compared.</param>
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
		// Token: 0x0600250A RID: 9482 RVA: 0x000A7754 File Offset: 0x000A5954
		public int CompareTo(SqlInt64 value)
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

		/// <summary>Compares the supplied object parameter to the <see cref="P:System.Data.SqlTypes.SqlInt64.Value" /> property of the <see cref="T:System.Data.SqlTypes.SqlInt64" /> object.</summary>
		/// <param name="value">The object to be compared.</param>
		/// <returns>
		///   <see langword="true" /> if object is an instance of <see cref="T:System.Data.SqlTypes.SqlInt64" /> and the two are equal; otherwise <see langword="false" />.</returns>
		// Token: 0x0600250B RID: 9483 RVA: 0x000A77AC File Offset: 0x000A59AC
		public override bool Equals(object value)
		{
			if (!(value is SqlInt64))
			{
				return false;
			}
			SqlInt64 y = (SqlInt64)value;
			if (y.IsNull || this.IsNull)
			{
				return y.IsNull && this.IsNull;
			}
			return (this == y).Value;
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		// Token: 0x0600250C RID: 9484 RVA: 0x000A7804 File Offset: 0x000A5A04
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
		// Token: 0x0600250D RID: 9485 RVA: 0x00003E32 File Offset: 0x00002032
		XmlSchema IXmlSerializable.GetSchema()
		{
			return null;
		}

		/// <summary>This member supports the .NET Framework infrastructure and is not intended to be used directly from your code.</summary>
		/// <param name="reader">
		///   <see langword="XmlReader" />
		/// </param>
		// Token: 0x0600250E RID: 9486 RVA: 0x000A782C File Offset: 0x000A5A2C
		void IXmlSerializable.ReadXml(XmlReader reader)
		{
			string attribute = reader.GetAttribute("nil", "http://www.w3.org/2001/XMLSchema-instance");
			if (attribute != null && XmlConvert.ToBoolean(attribute))
			{
				reader.ReadElementString();
				this.m_fNotNull = false;
				return;
			}
			this.m_value = XmlConvert.ToInt64(reader.ReadElementString());
			this.m_fNotNull = true;
		}

		/// <summary>This member supports the .NET Framework infrastructure and is not intended to be used directly from your code.</summary>
		/// <param name="writer">
		///   <see langword="XmlWriter" />
		/// </param>
		// Token: 0x0600250F RID: 9487 RVA: 0x000A787C File Offset: 0x000A5A7C
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
		/// <returns>A <see langword="string" /> that indicates the XSD of the specified <see cref="T:System.Xml.Schema.XmlSchemaSet" />.</returns>
		// Token: 0x06002510 RID: 9488 RVA: 0x000A78B2 File Offset: 0x000A5AB2
		public static XmlQualifiedName GetXsdType(XmlSchemaSet schemaSet)
		{
			return new XmlQualifiedName("long", "http://www.w3.org/2001/XMLSchema");
		}

		// Token: 0x06002511 RID: 9489 RVA: 0x000A78C4 File Offset: 0x000A5AC4
		// Note: this type is marked as 'beforefieldinit'.
		static SqlInt64()
		{
		}

		// Token: 0x040018C1 RID: 6337
		private bool m_fNotNull;

		// Token: 0x040018C2 RID: 6338
		private long m_value;

		// Token: 0x040018C3 RID: 6339
		private static readonly long s_lLowIntMask = (long)((ulong)-1);

		// Token: 0x040018C4 RID: 6340
		private static readonly long s_lHighIntMask = -4294967296L;

		/// <summary>Represents a <see cref="T:System.DBNull" /> that can be assigned to this instance of the <see cref="T:System.Data.SqlTypes.SqlInt64" /> structure.</summary>
		// Token: 0x040018C5 RID: 6341
		public static readonly SqlInt64 Null = new SqlInt64(true);

		/// <summary>Represents a zero value that can be assigned to the <see cref="P:System.Data.SqlTypes.SqlInt64.Value" /> property of an instance of the <see cref="T:System.Data.SqlTypes.SqlInt64" /> structure.</summary>
		// Token: 0x040018C6 RID: 6342
		public static readonly SqlInt64 Zero = new SqlInt64(0L);

		/// <summary>A constant representing the smallest possible value for a <see cref="T:System.Data.SqlTypes.SqlInt64" /> structure.</summary>
		// Token: 0x040018C7 RID: 6343
		public static readonly SqlInt64 MinValue = new SqlInt64(long.MinValue);

		/// <summary>A constant representing the largest possible value for a <see cref="T:System.Data.SqlTypes.SqlInt64" /> structure.</summary>
		// Token: 0x040018C8 RID: 6344
		public static readonly SqlInt64 MaxValue = new SqlInt64(long.MaxValue);
	}
}

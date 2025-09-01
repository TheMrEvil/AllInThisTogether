using System;
using System.Data.Common;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace System.Data.SqlTypes
{
	/// <summary>Represents an 8-bit unsigned integer, in the range of 0 through 255, to be stored in or retrieved from a database.</summary>
	// Token: 0x02000307 RID: 775
	[XmlSchemaProvider("GetXsdType")]
	[Serializable]
	public struct SqlByte : INullable, IComparable, IXmlSerializable
	{
		// Token: 0x060022A7 RID: 8871 RVA: 0x0009FB12 File Offset: 0x0009DD12
		private SqlByte(bool fNull)
		{
			this.m_fNotNull = false;
			this.m_value = 0;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlTypes.SqlByte" /> structure using the specified byte value.</summary>
		/// <param name="value">A byte value to be stored in the <see cref="P:System.Data.SqlTypes.SqlByte.Value" /> property of the new <see cref="T:System.Data.SqlTypes.SqlByte" /> structure.</param>
		// Token: 0x060022A8 RID: 8872 RVA: 0x0009FB22 File Offset: 0x0009DD22
		public SqlByte(byte value)
		{
			this.m_value = value;
			this.m_fNotNull = true;
		}

		/// <summary>Gets a Boolean value that indicates whether this <see cref="T:System.Data.SqlTypes.SqlByte" /> structure is null.</summary>
		/// <returns>
		///   <see langword="true" /> if null. Otherwise, <see langword="false" />.</returns>
		// Token: 0x17000634 RID: 1588
		// (get) Token: 0x060022A9 RID: 8873 RVA: 0x0009FB32 File Offset: 0x0009DD32
		public bool IsNull
		{
			get
			{
				return !this.m_fNotNull;
			}
		}

		/// <summary>Gets the value of the <see cref="T:System.Data.SqlTypes.SqlByte" /> structure. This property is read-only</summary>
		/// <returns>The value of the <see cref="T:System.Data.SqlTypes.SqlByte" /> structure.</returns>
		// Token: 0x17000635 RID: 1589
		// (get) Token: 0x060022AA RID: 8874 RVA: 0x0009FB3D File Offset: 0x0009DD3D
		public byte Value
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

		/// <summary>Converts the supplied byte value to a <see cref="T:System.Data.SqlTypes.SqlByte" />.</summary>
		/// <param name="x">A byte value to be converted to <see cref="T:System.Data.SqlTypes.SqlByte" />.</param>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlByte" /> structure whose <see cref="P:System.Data.SqlTypes.SqlByte.Value" /> property is equal to the supplied parameter.</returns>
		// Token: 0x060022AB RID: 8875 RVA: 0x0009FB53 File Offset: 0x0009DD53
		public static implicit operator SqlByte(byte x)
		{
			return new SqlByte(x);
		}

		/// <summary>Converts the supplied <see cref="T:System.Data.SqlTypes.SqlByte" /> structure to a byte.</summary>
		/// <param name="x">The <see cref="T:System.Data.SqlTypes.SqlByte" /> structure to be converted to a byte.</param>
		/// <returns>A byte whose value equals the <see cref="P:System.Data.SqlTypes.SqlByte.Value" /> property of the <see cref="T:System.Data.SqlTypes.SqlByte" /> parameter.</returns>
		// Token: 0x060022AC RID: 8876 RVA: 0x0009FB5B File Offset: 0x0009DD5B
		public static explicit operator byte(SqlByte x)
		{
			return x.Value;
		}

		/// <summary>Converts this <see cref="T:System.Data.SqlTypes.SqlByte" /> structure to a <see cref="T:System.String" />.</summary>
		/// <returns>A string that contains the <see cref="P:System.Data.SqlTypes.SqlByte.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlByte" />. If the <see langword="Value" /> is null, the <see langword="String" /> will be a null string.</returns>
		// Token: 0x060022AD RID: 8877 RVA: 0x0009FB64 File Offset: 0x0009DD64
		public override string ToString()
		{
			if (!this.IsNull)
			{
				return this.m_value.ToString(null);
			}
			return SQLResource.NullString;
		}

		/// <summary>Converts the <see cref="T:System.String" /> representation of a number to its 8-bit unsigned integer equivalent.</summary>
		/// <param name="s">The <see langword="String" /> to be parsed.</param>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlByte" /> structure that contains the 8-bit number represented by the <see langword="String" /> parameter.</returns>
		// Token: 0x060022AE RID: 8878 RVA: 0x0009FB80 File Offset: 0x0009DD80
		public static SqlByte Parse(string s)
		{
			if (s == SQLResource.NullString)
			{
				return SqlByte.Null;
			}
			return new SqlByte(byte.Parse(s, null));
		}

		/// <summary>The ones complement operator performs a bitwise one's complement operation on its <see cref="T:System.Data.SqlTypes.SqlByte" /> operand.</summary>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlByte" /> structure.</param>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlByte" /> structure whose <see cref="P:System.Data.SqlTypes.SqlByte.Value" /> property contains the ones complement of the <see cref="T:System.Data.SqlTypes.SqlByte" /> parameter.</returns>
		// Token: 0x060022AF RID: 8879 RVA: 0x0009FBA1 File Offset: 0x0009DDA1
		public static SqlByte operator ~(SqlByte x)
		{
			if (!x.IsNull)
			{
				return new SqlByte(~x.m_value);
			}
			return SqlByte.Null;
		}

		/// <summary>Computes the sum of the two specified <see cref="T:System.Data.SqlTypes.SqlByte" /> structures.</summary>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlByte" /> structure.</param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlByte" /> structure.</param>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlByte" /> whose <see cref="P:System.Data.SqlTypes.SqlByte.Value" /> property contains the sum of the two operands.</returns>
		// Token: 0x060022B0 RID: 8880 RVA: 0x0009FBC0 File Offset: 0x0009DDC0
		public static SqlByte operator +(SqlByte x, SqlByte y)
		{
			if (x.IsNull || y.IsNull)
			{
				return SqlByte.Null;
			}
			int num = (int)(x.m_value + y.m_value);
			if ((num & SqlByte.s_iBitNotByteMax) != 0)
			{
				throw new OverflowException(SQLResource.ArithOverflowMessage);
			}
			return new SqlByte((byte)num);
		}

		/// <summary>Subtracts the second <see cref="T:System.Data.SqlTypes.SqlByte" /> operand from the first.</summary>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlByte" /> structure.</param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlByte" /> structure.</param>
		/// <returns>The results of subtracting the second <see cref="T:System.Data.SqlTypes.SqlByte" /> operand from the first.</returns>
		// Token: 0x060022B1 RID: 8881 RVA: 0x0009FC10 File Offset: 0x0009DE10
		public static SqlByte operator -(SqlByte x, SqlByte y)
		{
			if (x.IsNull || y.IsNull)
			{
				return SqlByte.Null;
			}
			int num = (int)(x.m_value - y.m_value);
			if ((num & SqlByte.s_iBitNotByteMax) != 0)
			{
				throw new OverflowException(SQLResource.ArithOverflowMessage);
			}
			return new SqlByte((byte)num);
		}

		/// <summary>Computes the product of the two <see cref="T:System.Data.SqlTypes.SqlByte" /> operands.</summary>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlByte" /> structure.</param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlByte" /> structure.</param>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlByte" /> structure whose <see cref="P:System.Data.SqlTypes.SqlByte.Value" /> property contains the product of the multiplication.</returns>
		// Token: 0x060022B2 RID: 8882 RVA: 0x0009FC60 File Offset: 0x0009DE60
		public static SqlByte operator *(SqlByte x, SqlByte y)
		{
			if (x.IsNull || y.IsNull)
			{
				return SqlByte.Null;
			}
			int num = (int)(x.m_value * y.m_value);
			if ((num & SqlByte.s_iBitNotByteMax) != 0)
			{
				throw new OverflowException(SQLResource.ArithOverflowMessage);
			}
			return new SqlByte((byte)num);
		}

		/// <summary>Divides its first <see cref="T:System.Data.SqlTypes.SqlByte" /> operand by its second.</summary>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlByte" /> structure.</param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlByte" /> structure.</param>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlByte" /> structure whose <see cref="P:System.Data.SqlTypes.SqlByte.Value" /> property contains the results of the division.</returns>
		// Token: 0x060022B3 RID: 8883 RVA: 0x0009FCAE File Offset: 0x0009DEAE
		public static SqlByte operator /(SqlByte x, SqlByte y)
		{
			if (x.IsNull || y.IsNull)
			{
				return SqlByte.Null;
			}
			if (y.m_value != 0)
			{
				return new SqlByte(x.m_value / y.m_value);
			}
			throw new DivideByZeroException(SQLResource.DivideByZeroMessage);
		}

		/// <summary>Computes the remainder after dividing its first <see cref="T:System.Data.SqlTypes.SqlByte" /> operand by its second.</summary>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlByte" /> structure.</param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlByte" /> structure.</param>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlByte" /> structure whose <see cref="P:System.Data.SqlTypes.SqlByte.Value" /> contains the remainder.</returns>
		// Token: 0x060022B4 RID: 8884 RVA: 0x0009FCEE File Offset: 0x0009DEEE
		public static SqlByte operator %(SqlByte x, SqlByte y)
		{
			if (x.IsNull || y.IsNull)
			{
				return SqlByte.Null;
			}
			if (y.m_value != 0)
			{
				return new SqlByte(x.m_value % y.m_value);
			}
			throw new DivideByZeroException(SQLResource.DivideByZeroMessage);
		}

		/// <summary>Computes the bitwise AND of its <see cref="T:System.Data.SqlTypes.SqlByte" /> operands.</summary>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlByte" /> structure.</param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlByte" /> structure.</param>
		/// <returns>The results of the bitwise AND operation.</returns>
		// Token: 0x060022B5 RID: 8885 RVA: 0x0009FD2E File Offset: 0x0009DF2E
		public static SqlByte operator &(SqlByte x, SqlByte y)
		{
			if (!x.IsNull && !y.IsNull)
			{
				return new SqlByte(x.m_value & y.m_value);
			}
			return SqlByte.Null;
		}

		/// <summary>Computes the bitwise OR of its two <see cref="T:System.Data.SqlTypes.SqlByte" /> operands.</summary>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlByte" /> structure.</param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlByte" /> structure.</param>
		/// <returns>The results of the bitwise OR operation.</returns>
		// Token: 0x060022B6 RID: 8886 RVA: 0x0009FD5B File Offset: 0x0009DF5B
		public static SqlByte operator |(SqlByte x, SqlByte y)
		{
			if (!x.IsNull && !y.IsNull)
			{
				return new SqlByte(x.m_value | y.m_value);
			}
			return SqlByte.Null;
		}

		/// <summary>Performs a bitwise exclusive-OR operation on the supplied parameters.</summary>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlByte" /> structure.</param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlByte" /> structure.</param>
		/// <returns>The results of the bitwise XOR operation.</returns>
		// Token: 0x060022B7 RID: 8887 RVA: 0x0009FD88 File Offset: 0x0009DF88
		public static SqlByte operator ^(SqlByte x, SqlByte y)
		{
			if (!x.IsNull && !y.IsNull)
			{
				return new SqlByte(x.m_value ^ y.m_value);
			}
			return SqlByte.Null;
		}

		/// <summary>Converts the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> parameter to a <see cref="T:System.Data.SqlTypes.SqlByte" />.</summary>
		/// <param name="x">The <see cref="T:System.Data.SqlTypes.SqlBoolean" /> parameter to be converted to a <see cref="T:System.Data.SqlTypes.SqlByte" />.</param>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlByte" /> whose <see cref="P:System.Data.SqlTypes.SqlByte.Value" /> property equals the <see cref="P:System.Data.SqlTypes.SqlBoolean.ByteValue" /> of the supplied <see cref="T:System.Data.SqlTypes.SqlBoolean" /> parameter.</returns>
		// Token: 0x060022B8 RID: 8888 RVA: 0x0009FDB5 File Offset: 0x0009DFB5
		public static explicit operator SqlByte(SqlBoolean x)
		{
			if (!x.IsNull)
			{
				return new SqlByte(x.ByteValue);
			}
			return SqlByte.Null;
		}

		/// <summary>Converts the <see cref="T:System.Data.SqlTypes.SqlMoney" /> parameter to a <see cref="T:System.Data.SqlTypes.SqlByte" />.</summary>
		/// <param name="x">A <see langword="SqlMoney" /> structure.</param>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlByte" /> structure whose <see cref="P:System.Data.SqlTypes.SqlByte.Value" /> property is equal to the <see cref="P:System.Data.SqlTypes.SqlMoney.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlMoney" /> parameter.</returns>
		// Token: 0x060022B9 RID: 8889 RVA: 0x0009FDD2 File Offset: 0x0009DFD2
		public static explicit operator SqlByte(SqlMoney x)
		{
			if (!x.IsNull)
			{
				return new SqlByte(checked((byte)x.ToInt32()));
			}
			return SqlByte.Null;
		}

		/// <summary>Converts the <see cref="T:System.Data.SqlTypes.SqlInt16" /> parameter to a <see cref="T:System.Data.SqlTypes.SqlByte" />.</summary>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlInt16" /> structure.</param>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlByte" /> structure whose <see cref="P:System.Data.SqlTypes.SqlByte.Value" /> property is equal to the <see cref="P:System.Data.SqlTypes.SqlInt16.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlInt16" /> parameter.</returns>
		// Token: 0x060022BA RID: 8890 RVA: 0x0009FDF0 File Offset: 0x0009DFF0
		public static explicit operator SqlByte(SqlInt16 x)
		{
			if (x.IsNull)
			{
				return SqlByte.Null;
			}
			if (x.Value > 255 || x.Value < 0)
			{
				throw new OverflowException(SQLResource.ArithOverflowMessage);
			}
			if (!x.IsNull)
			{
				return new SqlByte((byte)x.Value);
			}
			return SqlByte.Null;
		}

		/// <summary>Converts the supplied <see cref="T:System.Data.SqlTypes.SqlInt32" /> to <see cref="T:System.Data.SqlTypes.SqlByte" />.</summary>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlInt32" /> structure.</param>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlByte" /> structure whose <see cref="P:System.Data.SqlTypes.SqlByte.Value" /> property is equal to the <see cref="P:System.Data.SqlTypes.SqlInt32.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlInt32" /> parameter.</returns>
		// Token: 0x060022BB RID: 8891 RVA: 0x0009FE4C File Offset: 0x0009E04C
		public static explicit operator SqlByte(SqlInt32 x)
		{
			if (x.IsNull)
			{
				return SqlByte.Null;
			}
			if (x.Value > 255 || x.Value < 0)
			{
				throw new OverflowException(SQLResource.ArithOverflowMessage);
			}
			if (!x.IsNull)
			{
				return new SqlByte((byte)x.Value);
			}
			return SqlByte.Null;
		}

		/// <summary>Converts the supplied <see cref="T:System.Data.SqlTypes.SqlInt64" /> to <see cref="T:System.Data.SqlTypes.SqlByte" />.</summary>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlInt64" /> structure.</param>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlByte" /> structure whose <see cref="P:System.Data.SqlTypes.SqlByte.Value" /> property is equal to the <see cref="P:System.Data.SqlTypes.SqlInt64.Value" /> of the <see langword="SqlInt64" /> parameter.</returns>
		// Token: 0x060022BC RID: 8892 RVA: 0x0009FEA8 File Offset: 0x0009E0A8
		public static explicit operator SqlByte(SqlInt64 x)
		{
			if (x.IsNull)
			{
				return SqlByte.Null;
			}
			if (x.Value > 255L || x.Value < 0L)
			{
				throw new OverflowException(SQLResource.ArithOverflowMessage);
			}
			if (!x.IsNull)
			{
				return new SqlByte((byte)x.Value);
			}
			return SqlByte.Null;
		}

		/// <summary>Converts the supplied <see cref="T:System.Data.SqlTypes.SqlSingle" /> structure to <see cref="T:System.Data.SqlTypes.SqlByte" />.</summary>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlSingle" /> structure.</param>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlByte" /> structure whose <see cref="P:System.Data.SqlTypes.SqlByte.Value" /> property is equal to the <see cref="P:System.Data.SqlTypes.SqlSingle.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlSingle" /> parameter.</returns>
		// Token: 0x060022BD RID: 8893 RVA: 0x0009FF08 File Offset: 0x0009E108
		public static explicit operator SqlByte(SqlSingle x)
		{
			if (x.IsNull)
			{
				return SqlByte.Null;
			}
			if (x.Value > 255f || x.Value < 0f)
			{
				throw new OverflowException(SQLResource.ArithOverflowMessage);
			}
			if (!x.IsNull)
			{
				return new SqlByte((byte)x.Value);
			}
			return SqlByte.Null;
		}

		/// <summary>Converts the supplied <see cref="T:System.Data.SqlTypes.SqlDouble" /> to <see cref="T:System.Data.SqlTypes.SqlByte" />.</summary>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlDouble" /> structure.</param>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlByte" /> structure whose <see cref="P:System.Data.SqlTypes.SqlByte.Value" /> property is equal to the <see cref="P:System.Data.SqlTypes.SqlDouble.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlDouble" /> parameter.</returns>
		// Token: 0x060022BE RID: 8894 RVA: 0x0009FF68 File Offset: 0x0009E168
		public static explicit operator SqlByte(SqlDouble x)
		{
			if (x.IsNull)
			{
				return SqlByte.Null;
			}
			if (x.Value > 255.0 || x.Value < 0.0)
			{
				throw new OverflowException(SQLResource.ArithOverflowMessage);
			}
			if (!x.IsNull)
			{
				return new SqlByte((byte)x.Value);
			}
			return SqlByte.Null;
		}

		/// <summary>Converts the supplied <see cref="T:System.Data.SqlTypes.SqlDecimal" /> to <see cref="T:System.Data.SqlTypes.SqlByte" />.</summary>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure.</param>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlByte" /> structure whose <see cref="P:System.Data.SqlTypes.SqlByte.Value" /> property is equal to the <see cref="P:System.Data.SqlTypes.SqlDecimal.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlDecimal" /> parameter.</returns>
		// Token: 0x060022BF RID: 8895 RVA: 0x0009FFCF File Offset: 0x0009E1CF
		public static explicit operator SqlByte(SqlDecimal x)
		{
			return (SqlByte)((SqlInt32)x);
		}

		/// <summary>Converts the supplied <see cref="T:System.Data.SqlTypes.SqlString" /> to <see cref="T:System.Data.SqlTypes.SqlByte" />.</summary>
		/// <param name="x">An instance of the <see langword="SqlString" /> class.</param>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlByte" /> structure whose <see cref="P:System.Data.SqlTypes.SqlByte.Value" /> property is equal to the numeric value represented by the <see cref="T:System.Data.SqlTypes.SqlString" />.</returns>
		// Token: 0x060022C0 RID: 8896 RVA: 0x0009FFDC File Offset: 0x0009E1DC
		public static explicit operator SqlByte(SqlString x)
		{
			if (!x.IsNull)
			{
				return new SqlByte(byte.Parse(x.Value, null));
			}
			return SqlByte.Null;
		}

		/// <summary>Performs a logical comparison of two <see cref="T:System.Data.SqlTypes.SqlByte" /> structures to determine whether they are equal.</summary>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlByte" /> structure.</param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlByte" /> structure.</param>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> that is <see cref="F:System.Data.SqlTypes.SqlBoolean.True" /> if the two instances are equal or <see cref="F:System.Data.SqlTypes.SqlBoolean.False" /> if the two instances are not equal. If either instance of <see cref="T:System.Data.SqlTypes.SqlByte" /> is null, the <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> will be <see cref="F:System.Data.SqlTypes.SqlBoolean.Null" />.</returns>
		// Token: 0x060022C1 RID: 8897 RVA: 0x0009FFFF File Offset: 0x0009E1FF
		public static SqlBoolean operator ==(SqlByte x, SqlByte y)
		{
			if (!x.IsNull && !y.IsNull)
			{
				return new SqlBoolean(x.m_value == y.m_value);
			}
			return SqlBoolean.Null;
		}

		/// <summary>Compares two instances of <see cref="T:System.Data.SqlTypes.SqlByte" /> to determine whether they are not equal.</summary>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlByte" /> structure.</param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlByte" /> structure.</param>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> that is <see cref="F:System.Data.SqlTypes.SqlBoolean.True" /> if the two instances are not equal or <see cref="F:System.Data.SqlTypes.SqlBoolean.False" /> if the two instances are equal. If either instance of <see cref="T:System.Data.SqlTypes.SqlByte" /> is null, the <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> will be <see cref="F:System.Data.SqlTypes.SqlBoolean.Null" />.</returns>
		// Token: 0x060022C2 RID: 8898 RVA: 0x000A002C File Offset: 0x0009E22C
		public static SqlBoolean operator !=(SqlByte x, SqlByte y)
		{
			return !(x == y);
		}

		/// <summary>Compares two instances of <see cref="T:System.Data.SqlTypes.SqlByte" /> to determine whether the first is less than the second.</summary>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlByte" /> structure.</param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlByte" /> structure.</param>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> that is <see cref="F:System.Data.SqlTypes.SqlBoolean.True" /> if the first instance is less than the second instance. Otherwise, <see cref="F:System.Data.SqlTypes.SqlBoolean.False" />. If either instance of <see cref="T:System.Data.SqlTypes.SqlByte" /> is null, the <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> will be <see cref="F:System.Data.SqlTypes.SqlBoolean.Null" />.</returns>
		// Token: 0x060022C3 RID: 8899 RVA: 0x000A003A File Offset: 0x0009E23A
		public static SqlBoolean operator <(SqlByte x, SqlByte y)
		{
			if (!x.IsNull && !y.IsNull)
			{
				return new SqlBoolean(x.m_value < y.m_value);
			}
			return SqlBoolean.Null;
		}

		/// <summary>Compares two instances of <see cref="T:System.Data.SqlTypes.SqlByte" /> to determine whether the first is greater than the second.</summary>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlByte" /> structure.</param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlByte" /> structure.</param>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> that is <see cref="F:System.Data.SqlTypes.SqlBoolean.True" /> if the first instance is greater than the second instance. Otherwise, <see cref="F:System.Data.SqlTypes.SqlBoolean.False" />. If either instance of <see cref="T:System.Data.SqlTypes.SqlByte" /> is null, the <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> will be <see cref="F:System.Data.SqlTypes.SqlBoolean.Null" />.</returns>
		// Token: 0x060022C4 RID: 8900 RVA: 0x000A0067 File Offset: 0x0009E267
		public static SqlBoolean operator >(SqlByte x, SqlByte y)
		{
			if (!x.IsNull && !y.IsNull)
			{
				return new SqlBoolean(x.m_value > y.m_value);
			}
			return SqlBoolean.Null;
		}

		/// <summary>Compares two instances of <see cref="T:System.Data.SqlTypes.SqlByte" /> to determine whether the first is less than or equal to the second.</summary>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlByte" /> structure.</param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlByte" /> structure.</param>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> that is <see cref="F:System.Data.SqlTypes.SqlBoolean.True" /> if the first instance is less than or equal to the second instance. Otherwise, <see cref="F:System.Data.SqlTypes.SqlBoolean.False" />. If either instance of <see cref="T:System.Data.SqlTypes.SqlByte" /> is null, the <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> will be <see cref="F:System.Data.SqlTypes.SqlBoolean.Null" />.</returns>
		// Token: 0x060022C5 RID: 8901 RVA: 0x000A0094 File Offset: 0x0009E294
		public static SqlBoolean operator <=(SqlByte x, SqlByte y)
		{
			if (!x.IsNull && !y.IsNull)
			{
				return new SqlBoolean(x.m_value <= y.m_value);
			}
			return SqlBoolean.Null;
		}

		/// <summary>Compares two instances of <see cref="T:System.Data.SqlTypes.SqlByte" /> to determine whether the first is greater than or equal to the second.</summary>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlByte" /> structure.</param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlByte" /> structure.</param>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> that is <see cref="F:System.Data.SqlTypes.SqlBoolean.True" /> if the first instance is greater than or equal to the second instance. Otherwise, <see cref="F:System.Data.SqlTypes.SqlBoolean.False" />. If either instance of <see cref="T:System.Data.SqlTypes.SqlByte" /> is null, the <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> of the <see langword="SqlBoolean" /> will be <see cref="F:System.Data.SqlTypes.SqlBoolean.Null" />.</returns>
		// Token: 0x060022C6 RID: 8902 RVA: 0x000A00C4 File Offset: 0x0009E2C4
		public static SqlBoolean operator >=(SqlByte x, SqlByte y)
		{
			if (!x.IsNull && !y.IsNull)
			{
				return new SqlBoolean(x.m_value >= y.m_value);
			}
			return SqlBoolean.Null;
		}

		/// <summary>The ones complement operator performs a bitwise one's complement operation on its <see cref="T:System.Data.SqlTypes.SqlByte" /> operand.</summary>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlByte" /> structure.</param>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlByte" /> structure whose <see cref="P:System.Data.SqlTypes.SqlByte.Value" /> property contains the ones complement of the <see cref="T:System.Data.SqlTypes.SqlByte" /> parameter.</returns>
		// Token: 0x060022C7 RID: 8903 RVA: 0x000A00F4 File Offset: 0x0009E2F4
		public static SqlByte OnesComplement(SqlByte x)
		{
			return ~x;
		}

		/// <summary>Computes the sum of the two specified <see cref="T:System.Data.SqlTypes.SqlByte" /> structures.</summary>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlByte" /> structure.</param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlByte" /> structure.</param>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlByte" /> structure whose <see langword="Value" /> property contains the results of the addition.</returns>
		// Token: 0x060022C8 RID: 8904 RVA: 0x000A00FC File Offset: 0x0009E2FC
		public static SqlByte Add(SqlByte x, SqlByte y)
		{
			return x + y;
		}

		/// <summary>Subtracts the second <see cref="T:System.Data.SqlTypes.SqlByte" /> operand from the first.</summary>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlByte" /> structure.</param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlByte" /> structure.</param>
		/// <returns>The results of subtracting the second <see cref="T:System.Data.SqlTypes.SqlByte" /> operand from the first.</returns>
		// Token: 0x060022C9 RID: 8905 RVA: 0x000A0105 File Offset: 0x0009E305
		public static SqlByte Subtract(SqlByte x, SqlByte y)
		{
			return x - y;
		}

		/// <summary>Computes the product of the two <see cref="T:System.Data.SqlTypes.SqlByte" /> operands.</summary>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlByte" /> structure.</param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlByte" /> structure.</param>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlByte" /> structure whose <see cref="P:System.Data.SqlTypes.SqlByte.Value" /> property contains the product of the multiplication.</returns>
		// Token: 0x060022CA RID: 8906 RVA: 0x000A010E File Offset: 0x0009E30E
		public static SqlByte Multiply(SqlByte x, SqlByte y)
		{
			return x * y;
		}

		/// <summary>Divides its first <see cref="T:System.Data.SqlTypes.SqlByte" /> operand by its second.</summary>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlByte" /> structure.</param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlByte" /> structure.</param>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlByte" /> structure whose <see cref="P:System.Data.SqlTypes.SqlByte.Value" /> property contains the results of the division.</returns>
		// Token: 0x060022CB RID: 8907 RVA: 0x000A0117 File Offset: 0x0009E317
		public static SqlByte Divide(SqlByte x, SqlByte y)
		{
			return x / y;
		}

		/// <summary>Computes the remainder after dividing its first <see cref="T:System.Data.SqlTypes.SqlByte" /> operand by its second.</summary>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlByte" /> structure.</param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlByte" /> structure.</param>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlByte" /> structure whose <see cref="P:System.Data.SqlTypes.SqlByte.Value" /> contains the remainder.</returns>
		// Token: 0x060022CC RID: 8908 RVA: 0x000A0120 File Offset: 0x0009E320
		public static SqlByte Mod(SqlByte x, SqlByte y)
		{
			return x % y;
		}

		/// <summary>Divides two <see cref="T:System.Data.SqlTypes.SqlByte" /> values and returns the remainder.</summary>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlByte" />.</param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlByte" />.</param>
		/// <returns>The remainder left after division is performed on <paramref name="x" /> and <paramref name="y" />.</returns>
		// Token: 0x060022CD RID: 8909 RVA: 0x000A0120 File Offset: 0x0009E320
		public static SqlByte Modulus(SqlByte x, SqlByte y)
		{
			return x % y;
		}

		/// <summary>Computes the bitwise AND of its <see cref="T:System.Data.SqlTypes.SqlByte" /> operands.</summary>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlByte" /> structure.</param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlByte" /> structure.</param>
		/// <returns>The results of the bitwise AND operation.</returns>
		// Token: 0x060022CE RID: 8910 RVA: 0x000A0129 File Offset: 0x0009E329
		public static SqlByte BitwiseAnd(SqlByte x, SqlByte y)
		{
			return x & y;
		}

		/// <summary>Computes the bitwise OR of its two <see cref="T:System.Data.SqlTypes.SqlByte" /> operands.</summary>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlByte" /> structure.</param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlByte" /> structure.</param>
		/// <returns>The results of the bitwise OR operation.</returns>
		// Token: 0x060022CF RID: 8911 RVA: 0x000A0132 File Offset: 0x0009E332
		public static SqlByte BitwiseOr(SqlByte x, SqlByte y)
		{
			return x | y;
		}

		/// <summary>Performs a bitwise exclusive-OR operation on the supplied parameters.</summary>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlByte" /> structure.</param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlByte" /> structure.</param>
		/// <returns>The results of the XOR operation.</returns>
		// Token: 0x060022D0 RID: 8912 RVA: 0x000A013B File Offset: 0x0009E33B
		public static SqlByte Xor(SqlByte x, SqlByte y)
		{
			return x ^ y;
		}

		/// <summary>Performs a logical comparison of two <see cref="T:System.Data.SqlTypes.SqlByte" /> structures to determine whether they are equal.</summary>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlByte" /> structure.</param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlByte" /> structure.</param>
		/// <returns>
		///   <see langword="true" /> if the two values are equal. Otherwise, <see langword="false" />. If either instance is null, then the <see langword="SqlByte" /> will be null.</returns>
		// Token: 0x060022D1 RID: 8913 RVA: 0x000A0144 File Offset: 0x0009E344
		public static SqlBoolean Equals(SqlByte x, SqlByte y)
		{
			return x == y;
		}

		/// <summary>Compares two instances of <see cref="T:System.Data.SqlTypes.SqlByte" /> to determine whether they are not equal.</summary>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlByte" /> structure.</param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlByte" /> structure.</param>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> that is <see cref="F:System.Data.SqlTypes.SqlBoolean.True" /> if the two instances are not equal or <see cref="F:System.Data.SqlTypes.SqlBoolean.False" /> if the two instances are equal. If either instance of <see cref="T:System.Data.SqlTypes.SqlByte" /> is null, the <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> will be <see cref="F:System.Data.SqlTypes.SqlBoolean.Null" />.</returns>
		// Token: 0x060022D2 RID: 8914 RVA: 0x000A014D File Offset: 0x0009E34D
		public static SqlBoolean NotEquals(SqlByte x, SqlByte y)
		{
			return x != y;
		}

		/// <summary>Compares two instances of <see cref="T:System.Data.SqlTypes.SqlByte" /> to determine whether the first is less than the second.</summary>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlByte" /> structure.</param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlByte" /> structure.</param>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> that is <see cref="F:System.Data.SqlTypes.SqlBoolean.True" /> if the first instance is less than the second instance. Otherwise, <see cref="F:System.Data.SqlTypes.SqlBoolean.False" />. If either instance of <see cref="T:System.Data.SqlTypes.SqlByte" /> is null, the <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> will be <see cref="F:System.Data.SqlTypes.SqlBoolean.Null" />.</returns>
		// Token: 0x060022D3 RID: 8915 RVA: 0x000A0156 File Offset: 0x0009E356
		public static SqlBoolean LessThan(SqlByte x, SqlByte y)
		{
			return x < y;
		}

		/// <summary>Compares two instances of <see cref="T:System.Data.SqlTypes.SqlByte" /> to determine whether the first is greater than the second.</summary>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlByte" /> structure.</param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlByte" /> structure.</param>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> that is <see cref="F:System.Data.SqlTypes.SqlBoolean.True" /> if the first instance is greater than the second instance. Otherwise, <see cref="F:System.Data.SqlTypes.SqlBoolean.False" />. If either instance of <see cref="T:System.Data.SqlTypes.SqlByte" /> is null, the <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> will be <see cref="F:System.Data.SqlTypes.SqlBoolean.Null" />.</returns>
		// Token: 0x060022D4 RID: 8916 RVA: 0x000A015F File Offset: 0x0009E35F
		public static SqlBoolean GreaterThan(SqlByte x, SqlByte y)
		{
			return x > y;
		}

		/// <summary>Compares two instances of <see cref="T:System.Data.SqlTypes.SqlByte" /> to determine whether the first is less than or equal to the second.</summary>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlByte" /> structure.</param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlByte" /> structure.</param>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> that is <see cref="F:System.Data.SqlTypes.SqlBoolean.True" /> if the first instance is less than or equal to the second instance. Otherwise, <see cref="F:System.Data.SqlTypes.SqlBoolean.False" />. If either instance of <see cref="T:System.Data.SqlTypes.SqlByte" /> is null, the <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> will be <see cref="F:System.Data.SqlTypes.SqlBoolean.Null" />.</returns>
		// Token: 0x060022D5 RID: 8917 RVA: 0x000A0168 File Offset: 0x0009E368
		public static SqlBoolean LessThanOrEqual(SqlByte x, SqlByte y)
		{
			return x <= y;
		}

		/// <summary>Compares two <see cref="T:System.Data.SqlTypes.SqlByte" /> structures to determine whether the first is greater than or equal to the second.</summary>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlByte" /> structure.</param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlByte" /> structure.</param>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> that is <see cref="F:System.Data.SqlTypes.SqlBoolean.True" /> if the first instance is greater than or equal to the second instance. Otherwise, <see cref="F:System.Data.SqlTypes.SqlBoolean.False" />. If either instance of <see cref="T:System.Data.SqlTypes.SqlByte" /> is null, the <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> will be <see cref="F:System.Data.SqlTypes.SqlBoolean.Null" />.</returns>
		// Token: 0x060022D6 RID: 8918 RVA: 0x000A0171 File Offset: 0x0009E371
		public static SqlBoolean GreaterThanOrEqual(SqlByte x, SqlByte y)
		{
			return x >= y;
		}

		/// <summary>Converts this <see cref="T:System.Data.SqlTypes.SqlByte" /> structure to <see cref="T:System.Data.SqlTypes.SqlBoolean" />.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="P:System.Data.SqlTypes.SqlByte.Value" /> is non-zero; <see langword="false" /> if zero; otherwise Null.</returns>
		// Token: 0x060022D7 RID: 8919 RVA: 0x000A017A File Offset: 0x0009E37A
		public SqlBoolean ToSqlBoolean()
		{
			return (SqlBoolean)this;
		}

		/// <summary>Converts this <see cref="T:System.Data.SqlTypes.SqlByte" /> structure to <see cref="T:System.Data.SqlTypes.SqlDouble" />.</summary>
		/// <returns>A <see langword="SqlDouble" /> structure with the same value as this <see cref="T:System.Data.SqlTypes.SqlByte" />.</returns>
		// Token: 0x060022D8 RID: 8920 RVA: 0x000A0187 File Offset: 0x0009E387
		public SqlDouble ToSqlDouble()
		{
			return this;
		}

		/// <summary>Converts this <see cref="T:System.Data.SqlTypes.SqlByte" /> structure to <see cref="T:System.Data.SqlTypes.SqlInt16" />.</summary>
		/// <returns>A <see langword="SqlInt16" /> structure with the same value as this <see cref="T:System.Data.SqlTypes.SqlByte" />.</returns>
		// Token: 0x060022D9 RID: 8921 RVA: 0x000A0194 File Offset: 0x0009E394
		public SqlInt16 ToSqlInt16()
		{
			return this;
		}

		/// <summary>Converts this <see cref="T:System.Data.SqlTypes.SqlByte" /> to <see cref="T:System.Data.SqlTypes.SqlInt32" />.</summary>
		/// <returns>A <see langword="SqlInt32" /> structure with the same value as this <see cref="T:System.Data.SqlTypes.SqlByte" />.</returns>
		// Token: 0x060022DA RID: 8922 RVA: 0x000A01A1 File Offset: 0x0009E3A1
		public SqlInt32 ToSqlInt32()
		{
			return this;
		}

		/// <summary>Converts this <see cref="T:System.Data.SqlTypes.SqlByte" /> structure to <see cref="T:System.Data.SqlTypes.SqlInt64" />.</summary>
		/// <returns>A <see langword="SqlInt64" /> structure who <see cref="P:System.Data.SqlTypes.SqlInt64.Value" /> equals the <see cref="P:System.Data.SqlTypes.SqlByte.Value" /> of this <see cref="T:System.Data.SqlTypes.SqlByte" />.</returns>
		// Token: 0x060022DB RID: 8923 RVA: 0x000A01AE File Offset: 0x0009E3AE
		public SqlInt64 ToSqlInt64()
		{
			return this;
		}

		/// <summary>Converts this <see cref="T:System.Data.SqlTypes.SqlByte" /> structure to <see cref="T:System.Data.SqlTypes.SqlMoney" />.</summary>
		/// <returns>A <see langword="SqlMoney" /> structure whose <see cref="P:System.Data.SqlTypes.SqlMoney.Value" /> equals the <see cref="P:System.Data.SqlTypes.SqlByte.Value" /> of this <see cref="T:System.Data.SqlTypes.SqlByte" /> structure.</returns>
		// Token: 0x060022DC RID: 8924 RVA: 0x000A01BB File Offset: 0x0009E3BB
		public SqlMoney ToSqlMoney()
		{
			return this;
		}

		/// <summary>Converts this <see cref="T:System.Data.SqlTypes.SqlByte" /> structure to <see cref="T:System.Data.SqlTypes.SqlDecimal" />.</summary>
		/// <returns>A <see langword="SqlDecimal" /> structure whose <see cref="P:System.Data.SqlTypes.SqlDecimal.Value" /> equals the <see cref="P:System.Data.SqlTypes.SqlByte.Value" /> of this <see cref="T:System.Data.SqlTypes.SqlByte" /> structure.</returns>
		// Token: 0x060022DD RID: 8925 RVA: 0x000A01C8 File Offset: 0x0009E3C8
		public SqlDecimal ToSqlDecimal()
		{
			return this;
		}

		/// <summary>Converts this <see cref="T:System.Data.SqlTypes.SqlByte" /> structure to <see cref="T:System.Data.SqlTypes.SqlSingle" />.</summary>
		/// <returns>A <see langword="SqlSingle" /> structure that has the same <see cref="P:System.Data.SqlTypes.SqlSingle.Value" /> as this <see cref="T:System.Data.SqlTypes.SqlByte" /> structure.</returns>
		// Token: 0x060022DE RID: 8926 RVA: 0x000A01D5 File Offset: 0x0009E3D5
		public SqlSingle ToSqlSingle()
		{
			return this;
		}

		/// <summary>Converts this instance of <see cref="T:System.Data.SqlTypes.SqlByte" /> to <see cref="T:System.Data.SqlTypes.SqlString" />.</summary>
		/// <returns>A <see langword="SqlString" /> that contains the string representation of the <see cref="T:System.Data.SqlTypes.SqlByte" /> structure's <see cref="P:System.Data.SqlTypes.SqlByte.Value" />.</returns>
		// Token: 0x060022DF RID: 8927 RVA: 0x000A01E2 File Offset: 0x0009E3E2
		public SqlString ToSqlString()
		{
			return (SqlString)this;
		}

		/// <summary>Compares this instance to the supplied <see cref="T:System.Object" /> and returns an indication of their relative values.</summary>
		/// <param name="value">The <see cref="T:System.Object" /> to be compared.</param>
		/// <returns>A signed number that indicates the relative values of the instance and the object.  
		///   Return Value  
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
		// Token: 0x060022E0 RID: 8928 RVA: 0x000A01F0 File Offset: 0x0009E3F0
		public int CompareTo(object value)
		{
			if (value is SqlByte)
			{
				SqlByte value2 = (SqlByte)value;
				return this.CompareTo(value2);
			}
			throw ADP.WrongType(value.GetType(), typeof(SqlByte));
		}

		/// <summary>Compares this instance to the supplied <see cref="T:System.Data.SqlTypes.SqlByte" /> object and returns an indication of their relative values.</summary>
		/// <param name="value">The <see cref="T:System.Data.SqlTypes.SqlByte" /> object to be compared.</param>
		/// <returns>A signed number that indicates the relative values of the instance and the object.  
		///   Return Value  
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
		// Token: 0x060022E1 RID: 8929 RVA: 0x000A022C File Offset: 0x0009E42C
		public int CompareTo(SqlByte value)
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

		/// <summary>Compares the supplied <see cref="T:System.Object" /> parameter to the <see cref="P:System.Data.SqlTypes.SqlByte.Value" /> property of the <see cref="T:System.Data.SqlTypes.SqlByte" /> object.</summary>
		/// <param name="value">The <see cref="T:System.Object" /> to be compared.</param>
		/// <returns>
		///   <see langword="true" /> if object is an instance of <see cref="T:System.Data.SqlTypes.SqlByte" /> and the two are equal; otherwise <see langword="false" />.</returns>
		// Token: 0x060022E2 RID: 8930 RVA: 0x000A0284 File Offset: 0x0009E484
		public override bool Equals(object value)
		{
			if (!(value is SqlByte))
			{
				return false;
			}
			SqlByte y = (SqlByte)value;
			if (y.IsNull || this.IsNull)
			{
				return y.IsNull && this.IsNull;
			}
			return (this == y).Value;
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		// Token: 0x060022E3 RID: 8931 RVA: 0x000A02DC File Offset: 0x0009E4DC
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
		// Token: 0x060022E4 RID: 8932 RVA: 0x00003E32 File Offset: 0x00002032
		XmlSchema IXmlSerializable.GetSchema()
		{
			return null;
		}

		/// <summary>This member supports the .NET Framework infrastructure and is not intended to be used directly from your code.</summary>
		/// <param name="reader">
		///   <see langword="XmlReader" />
		/// </param>
		// Token: 0x060022E5 RID: 8933 RVA: 0x000A0304 File Offset: 0x0009E504
		void IXmlSerializable.ReadXml(XmlReader reader)
		{
			string attribute = reader.GetAttribute("nil", "http://www.w3.org/2001/XMLSchema-instance");
			if (attribute != null && XmlConvert.ToBoolean(attribute))
			{
				reader.ReadElementString();
				this.m_fNotNull = false;
				return;
			}
			this.m_value = XmlConvert.ToByte(reader.ReadElementString());
			this.m_fNotNull = true;
		}

		/// <summary>This member supports the .NET Framework infrastructure and is not intended to be used directly from your code.</summary>
		/// <param name="writer">
		///   <see langword="XmlWriter" />
		/// </param>
		// Token: 0x060022E6 RID: 8934 RVA: 0x000A0354 File Offset: 0x0009E554
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
		/// <param name="schemaSet">A <see cref="T:System.Xml.Schema.XmlSchemaSet" />.</param>
		/// <returns>A <see langword="string" /> value that indicates the XSD of the specified <see cref="T:System.Xml.Schema.XmlSchemaSet" />.</returns>
		// Token: 0x060022E7 RID: 8935 RVA: 0x000A038A File Offset: 0x0009E58A
		public static XmlQualifiedName GetXsdType(XmlSchemaSet schemaSet)
		{
			return new XmlQualifiedName("unsignedByte", "http://www.w3.org/2001/XMLSchema");
		}

		// Token: 0x060022E8 RID: 8936 RVA: 0x000A039B File Offset: 0x0009E59B
		// Note: this type is marked as 'beforefieldinit'.
		static SqlByte()
		{
		}

		// Token: 0x0400182D RID: 6189
		private bool m_fNotNull;

		// Token: 0x0400182E RID: 6190
		private byte m_value;

		// Token: 0x0400182F RID: 6191
		private static readonly int s_iBitNotByteMax = -256;

		/// <summary>Represents a <see cref="T:System.DBNull" /> that can be assigned to this instance of the <see cref="T:System.Data.SqlTypes.SqlByte" /> structure.</summary>
		// Token: 0x04001830 RID: 6192
		public static readonly SqlByte Null = new SqlByte(true);

		/// <summary>Represents a zero value that can be assigned to the <see cref="P:System.Data.SqlTypes.SqlByte.Value" /> property of an instance of the <see cref="T:System.Data.SqlTypes.SqlByte" /> structure.</summary>
		// Token: 0x04001831 RID: 6193
		public static readonly SqlByte Zero = new SqlByte(0);

		/// <summary>A constant representing the smallest possible value of a <see cref="T:System.Data.SqlTypes.SqlByte" />.</summary>
		// Token: 0x04001832 RID: 6194
		public static readonly SqlByte MinValue = new SqlByte(0);

		/// <summary>A constant representing the largest possible value of a <see cref="T:System.Data.SqlTypes.SqlByte" />.</summary>
		// Token: 0x04001833 RID: 6195
		public static readonly SqlByte MaxValue = new SqlByte(byte.MaxValue);
	}
}

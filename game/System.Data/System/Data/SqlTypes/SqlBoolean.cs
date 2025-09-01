using System;
using System.Data.Common;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace System.Data.SqlTypes
{
	/// <summary>Represents an integer value that is either 1 or 0 to be stored in or retrieved from a database.</summary>
	// Token: 0x02000306 RID: 774
	[XmlSchemaProvider("GetXsdType")]
	[Serializable]
	public struct SqlBoolean : INullable, IComparable, IXmlSerializable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure using the supplied Boolean value.</summary>
		/// <param name="value">The value for the new <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure; either <see langword="true" /> or <see langword="false" />.</param>
		// Token: 0x06002269 RID: 8809 RVA: 0x0009F3F9 File Offset: 0x0009D5F9
		public SqlBoolean(bool value)
		{
			this.m_value = (value ? 2 : 1);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure using the specified integer value.</summary>
		/// <param name="value">The integer whose value is to be used for the new <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure.</param>
		// Token: 0x0600226A RID: 8810 RVA: 0x0009F408 File Offset: 0x0009D608
		public SqlBoolean(int value)
		{
			this = new SqlBoolean(value, false);
		}

		// Token: 0x0600226B RID: 8811 RVA: 0x0009F412 File Offset: 0x0009D612
		private SqlBoolean(int value, bool fNull)
		{
			if (fNull)
			{
				this.m_value = 0;
				return;
			}
			this.m_value = ((value != 0) ? 2 : 1);
		}

		/// <summary>Indicates whether this <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure is null.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure is null; otherwise, <see langword="false" />.</returns>
		// Token: 0x1700062F RID: 1583
		// (get) Token: 0x0600226C RID: 8812 RVA: 0x0009F42C File Offset: 0x0009D62C
		public bool IsNull
		{
			get
			{
				return this.m_value == 0;
			}
		}

		/// <summary>Gets the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure's value. This property is read-only.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> is <see cref="F:System.Data.SqlTypes.SqlBoolean.True" />; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.Data.SqlTypes.SqlNullValueException">The property is set to null.</exception>
		// Token: 0x17000630 RID: 1584
		// (get) Token: 0x0600226D RID: 8813 RVA: 0x0009F438 File Offset: 0x0009D638
		public bool Value
		{
			get
			{
				byte value = this.m_value;
				if (value == 1)
				{
					return false;
				}
				if (value == 2)
				{
					return true;
				}
				throw new SqlNullValueException();
			}
		}

		/// <summary>Gets a value that indicates whether the current <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> is <see cref="F:System.Data.SqlTypes.SqlBoolean.True" />.</summary>
		/// <returns>
		///   <see langword="true" /> if <see langword="Value" /> is <see langword="true" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000631 RID: 1585
		// (get) Token: 0x0600226E RID: 8814 RVA: 0x0009F45D File Offset: 0x0009D65D
		public bool IsTrue
		{
			get
			{
				return this.m_value == 2;
			}
		}

		/// <summary>Indicates whether the current <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> is <see cref="F:System.Data.SqlTypes.SqlBoolean.False" />.</summary>
		/// <returns>
		///   <see langword="true" /> if <see langword="Value" /> is <see langword="False" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000632 RID: 1586
		// (get) Token: 0x0600226F RID: 8815 RVA: 0x0009F468 File Offset: 0x0009D668
		public bool IsFalse
		{
			get
			{
				return this.m_value == 1;
			}
		}

		/// <summary>Converts the supplied byte value to a <see cref="T:System.Data.SqlTypes.SqlBoolean" />.</summary>
		/// <param name="x">A byte value to be converted to <see cref="T:System.Data.SqlTypes.SqlBoolean" />.</param>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> value that contains 0 or 1.</returns>
		// Token: 0x06002270 RID: 8816 RVA: 0x0009F473 File Offset: 0x0009D673
		public static implicit operator SqlBoolean(bool x)
		{
			return new SqlBoolean(x);
		}

		/// <summary>Converts a <see cref="T:System.Data.SqlTypes.SqlBoolean" /> to a Boolean.</summary>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> to convert.</param>
		/// <returns>A Boolean set to the <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlBoolean" />.</returns>
		// Token: 0x06002271 RID: 8817 RVA: 0x0009F47B File Offset: 0x0009D67B
		public static explicit operator bool(SqlBoolean x)
		{
			return x.Value;
		}

		/// <summary>Performs a NOT operation on a <see cref="T:System.Data.SqlTypes.SqlBoolean" />.</summary>
		/// <param name="x">The <see cref="T:System.Data.SqlTypes.SqlBoolean" /> on which the NOT operation will be performed.</param>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> with the <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /><see cref="F:System.Data.SqlTypes.SqlBoolean.True" /> if argument was true, <see cref="F:System.Data.SqlTypes.SqlBoolean.Null" /> if argument was null, and <see cref="F:System.Data.SqlTypes.SqlBoolean.False" /> otherwise.</returns>
		// Token: 0x06002272 RID: 8818 RVA: 0x0009F484 File Offset: 0x0009D684
		public static SqlBoolean operator !(SqlBoolean x)
		{
			byte value = x.m_value;
			if (value == 1)
			{
				return SqlBoolean.True;
			}
			if (value == 2)
			{
				return SqlBoolean.False;
			}
			return SqlBoolean.Null;
		}

		/// <summary>The true operator can be used to test the <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> to determine whether it is true.</summary>
		/// <param name="x">The <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure to be tested.</param>
		/// <returns>
		///   <see langword="true" /> if the supplied parameter is <see cref="T:System.Data.SqlTypes.SqlBoolean" /> is <see langword="true" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x06002273 RID: 8819 RVA: 0x0009F4B1 File Offset: 0x0009D6B1
		public static bool operator true(SqlBoolean x)
		{
			return x.IsTrue;
		}

		/// <summary>The false operator can be used to test the <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> to determine whether it is false.</summary>
		/// <param name="x">The <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure to be tested.</param>
		/// <returns>
		///   <see langword="true" /> if the supplied parameter is <see cref="T:System.Data.SqlTypes.SqlBoolean" /> is <see langword="false" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x06002274 RID: 8820 RVA: 0x0009F4BA File Offset: 0x0009D6BA
		public static bool operator false(SqlBoolean x)
		{
			return x.IsFalse;
		}

		/// <summary>Computes the bitwise AND operation of two specified <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structures.</summary>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure.</param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure.</param>
		/// <returns>The result of the logical AND operation.</returns>
		// Token: 0x06002275 RID: 8821 RVA: 0x0009F4C3 File Offset: 0x0009D6C3
		public static SqlBoolean operator &(SqlBoolean x, SqlBoolean y)
		{
			if (x.m_value == 1 || y.m_value == 1)
			{
				return SqlBoolean.False;
			}
			if (x.m_value == 2 && y.m_value == 2)
			{
				return SqlBoolean.True;
			}
			return SqlBoolean.Null;
		}

		/// <summary>Computes the bitwise OR of its operands.</summary>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure.</param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure.</param>
		/// <returns>The results of the logical OR operation.</returns>
		// Token: 0x06002276 RID: 8822 RVA: 0x0009F4FA File Offset: 0x0009D6FA
		public static SqlBoolean operator |(SqlBoolean x, SqlBoolean y)
		{
			if (x.m_value == 2 || y.m_value == 2)
			{
				return SqlBoolean.True;
			}
			if (x.m_value == 1 && y.m_value == 1)
			{
				return SqlBoolean.False;
			}
			return SqlBoolean.Null;
		}

		/// <summary>Gets the value of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure as a byte.</summary>
		/// <returns>A byte representing the value of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure.</returns>
		// Token: 0x17000633 RID: 1587
		// (get) Token: 0x06002277 RID: 8823 RVA: 0x0009F531 File Offset: 0x0009D731
		public byte ByteValue
		{
			get
			{
				if (this.IsNull)
				{
					throw new SqlNullValueException();
				}
				if (this.m_value != 2)
				{
					return 0;
				}
				return 1;
			}
		}

		/// <summary>Converts this <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure to a string.</summary>
		/// <returns>A string that contains the value of the <see cref="T:System.Data.SqlTypes.SqlBoolean" />. If the value is null, the string will contain "null".</returns>
		// Token: 0x06002278 RID: 8824 RVA: 0x0009F550 File Offset: 0x0009D750
		public override string ToString()
		{
			if (!this.IsNull)
			{
				return this.Value.ToString();
			}
			return SQLResource.NullString;
		}

		/// <summary>Converts the specified <see cref="T:System.String" /> representation of a logical value to its <see cref="T:System.Data.SqlTypes.SqlBoolean" /> equivalent.</summary>
		/// <param name="s">The <see cref="T:System.String" /> to be converted.</param>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure that contains the parsed value.</returns>
		// Token: 0x06002279 RID: 8825 RVA: 0x0009F57C File Offset: 0x0009D77C
		public static SqlBoolean Parse(string s)
		{
			if (s == null)
			{
				return new SqlBoolean(bool.Parse(s));
			}
			if (s == SQLResource.NullString)
			{
				return SqlBoolean.Null;
			}
			s = s.TrimStart();
			char c = s[0];
			if (char.IsNumber(c) || '-' == c || '+' == c)
			{
				return new SqlBoolean(int.Parse(s, null));
			}
			return new SqlBoolean(bool.Parse(s));
		}

		/// <summary>Performs a one's complement operation on the supplied <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structures.</summary>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure.</param>
		/// <returns>The one's complement of the supplied <see cref="T:System.Data.SqlTypes.SqlBoolean" />.</returns>
		// Token: 0x0600227A RID: 8826 RVA: 0x0009F5E5 File Offset: 0x0009D7E5
		public static SqlBoolean operator ~(SqlBoolean x)
		{
			return !x;
		}

		/// <summary>Performs a bitwise exclusive-OR (XOR) operation on the supplied parameters.</summary>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure.</param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure.</param>
		/// <returns>The result of the logical XOR operation.</returns>
		// Token: 0x0600227B RID: 8827 RVA: 0x0009F5ED File Offset: 0x0009D7ED
		public static SqlBoolean operator ^(SqlBoolean x, SqlBoolean y)
		{
			if (!x.IsNull && !y.IsNull)
			{
				return new SqlBoolean(x.m_value != y.m_value);
			}
			return SqlBoolean.Null;
		}

		/// <summary>Converts the <see cref="T:System.Data.SqlTypes.SqlByte" /> parameter to a <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure.</summary>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlByte" /> to be converted to a <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure.</param>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure whose value equals the <see cref="P:System.Data.SqlTypes.SqlByte.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlByte" /> parameter.</returns>
		// Token: 0x0600227C RID: 8828 RVA: 0x0009F61D File Offset: 0x0009D81D
		public static explicit operator SqlBoolean(SqlByte x)
		{
			if (!x.IsNull)
			{
				return new SqlBoolean(x.Value > 0);
			}
			return SqlBoolean.Null;
		}

		/// <summary>Converts the <see cref="T:System.Data.SqlTypes.SqlInt16" /> parameter to a <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure.</summary>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlInt16" /> to be converted to a <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure.</param>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure whose value equals the <see cref="P:System.Data.SqlTypes.SqlInt16.Value" /> property of the <see cref="T:System.Data.SqlTypes.SqlInt16" /> parameter.</returns>
		// Token: 0x0600227D RID: 8829 RVA: 0x0009F63D File Offset: 0x0009D83D
		public static explicit operator SqlBoolean(SqlInt16 x)
		{
			if (!x.IsNull)
			{
				return new SqlBoolean(x.Value != 0);
			}
			return SqlBoolean.Null;
		}

		/// <summary>Converts the <see cref="T:System.Data.SqlTypes.SqlInt32" /> parameter to a <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure.</summary>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlInt32" /> to be converted to a <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure.</param>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure whose value equals the <see cref="P:System.Data.SqlTypes.SqlInt32.Value" /> property of the <see cref="T:System.Data.SqlTypes.SqlInt32" /> parameter.</returns>
		// Token: 0x0600227E RID: 8830 RVA: 0x0009F65D File Offset: 0x0009D85D
		public static explicit operator SqlBoolean(SqlInt32 x)
		{
			if (!x.IsNull)
			{
				return new SqlBoolean(x.Value != 0);
			}
			return SqlBoolean.Null;
		}

		/// <summary>Converts the <see cref="T:System.Data.SqlTypes.SqlInt64" /> parameter to a <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure.</summary>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlInt64" /> to be converted to a <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure.</param>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure whose value equals the <see cref="P:System.Data.SqlTypes.SqlInt64.Value" /> property of the <see cref="T:System.Data.SqlTypes.SqlInt64" /> parameter.</returns>
		// Token: 0x0600227F RID: 8831 RVA: 0x0009F67D File Offset: 0x0009D87D
		public static explicit operator SqlBoolean(SqlInt64 x)
		{
			if (!x.IsNull)
			{
				return new SqlBoolean(x.Value != 0L);
			}
			return SqlBoolean.Null;
		}

		/// <summary>Converts the <see cref="T:System.Data.SqlTypes.SqlDouble" /> parameter to a <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure.</summary>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlDouble" /> to be converted to a <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure.</param>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure whose value equals the <see cref="P:System.Data.SqlTypes.SqlDouble.Value" /> property of the <see cref="T:System.Data.SqlTypes.SqlDouble" /> parameter.</returns>
		// Token: 0x06002280 RID: 8832 RVA: 0x0009F69E File Offset: 0x0009D89E
		public static explicit operator SqlBoolean(SqlDouble x)
		{
			if (!x.IsNull)
			{
				return new SqlBoolean(x.Value != 0.0);
			}
			return SqlBoolean.Null;
		}

		/// <summary>Converts the <see cref="T:System.Data.SqlTypes.SqlSingle" /> parameter to a <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure.</summary>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlSingle" /> to be converted to a <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure.</param>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure whose value equals the <see cref="P:System.Data.SqlTypes.SqlSingle.Value" /> property of the <see cref="T:System.Data.SqlTypes.SqlSingle" /> parameter.</returns>
		// Token: 0x06002281 RID: 8833 RVA: 0x0009F6C9 File Offset: 0x0009D8C9
		public static explicit operator SqlBoolean(SqlSingle x)
		{
			if (!x.IsNull)
			{
				return new SqlBoolean((double)x.Value != 0.0);
			}
			return SqlBoolean.Null;
		}

		/// <summary>Converts the <see cref="T:System.Data.SqlTypes.SqlMoney" /> parameter to a <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure.</summary>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlMoney" /> to be converted to a <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure.</param>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlByte" /> structure whose value equals the <see cref="P:System.Data.SqlTypes.SqlMoney.Value" /> property of the <see cref="T:System.Data.SqlTypes.SqlMoney" /> parameter.</returns>
		// Token: 0x06002282 RID: 8834 RVA: 0x0009F6F5 File Offset: 0x0009D8F5
		public static explicit operator SqlBoolean(SqlMoney x)
		{
			if (!x.IsNull)
			{
				return x != SqlMoney.Zero;
			}
			return SqlBoolean.Null;
		}

		/// <summary>Converts the <see cref="T:System.Data.SqlTypes.SqlDecimal" /> parameter to a <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure.</summary>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlDecimal" /> to be converted to a <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure.</param>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlByte" /> structure whose value equals the <see cref="P:System.Data.SqlTypes.SqlDecimal.Value" /> property of the <see cref="T:System.Data.SqlTypes.SqlDecimal" /> parameter.</returns>
		// Token: 0x06002283 RID: 8835 RVA: 0x0009F711 File Offset: 0x0009D911
		public static explicit operator SqlBoolean(SqlDecimal x)
		{
			if (!x.IsNull)
			{
				return new SqlBoolean(x._data1 != 0U || x._data2 != 0U || x._data3 != 0U || x._data4 > 0U);
			}
			return SqlBoolean.Null;
		}

		/// <summary>Converts the <see cref="T:System.Data.SqlTypes.SqlString" /> parameter to a <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure.</summary>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlString" /> to be converted to a <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure.</param>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlByte" /> structure whose value equals the <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> property of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> parameter.</returns>
		// Token: 0x06002284 RID: 8836 RVA: 0x0009F74B File Offset: 0x0009D94B
		public static explicit operator SqlBoolean(SqlString x)
		{
			if (!x.IsNull)
			{
				return SqlBoolean.Parse(x.Value);
			}
			return SqlBoolean.Null;
		}

		/// <summary>Compares two instances of <see cref="T:System.Data.SqlTypes.SqlBoolean" /> for equality.</summary>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlBoolean" />.</param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlBoolean" />.</param>
		/// <returns>
		///   <see cref="F:System.Data.SqlTypes.SqlBoolean.True" /> if the two instances are equal or <see cref="F:System.Data.SqlTypes.SqlBoolean.False" /> if the two instances are not equal. If either instance of <see cref="T:System.Data.SqlTypes.SqlBoolean" /> is null, the <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> will be <see cref="F:System.Data.SqlTypes.SqlBoolean.Null" />.</returns>
		// Token: 0x06002285 RID: 8837 RVA: 0x0009F768 File Offset: 0x0009D968
		public static SqlBoolean operator ==(SqlBoolean x, SqlBoolean y)
		{
			if (!x.IsNull && !y.IsNull)
			{
				return new SqlBoolean(x.m_value == y.m_value);
			}
			return SqlBoolean.Null;
		}

		/// <summary>Compares two instances of <see cref="T:System.Data.SqlTypes.SqlBoolean" /> to determine whether they are not equal.</summary>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlBoolean" />.</param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlBoolean" />.</param>
		/// <returns>
		///   <see cref="F:System.Data.SqlTypes.SqlBoolean.True" /> if the two instances are not equal or <see cref="F:System.Data.SqlTypes.SqlBoolean.False" /> if the two instances are equal. If either instance of <see cref="T:System.Data.SqlTypes.SqlBoolean" /> is null, the <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> will be <see cref="F:System.Data.SqlTypes.SqlBoolean.Null" />.</returns>
		// Token: 0x06002286 RID: 8838 RVA: 0x0009F795 File Offset: 0x0009D995
		public static SqlBoolean operator !=(SqlBoolean x, SqlBoolean y)
		{
			return !(x == y);
		}

		/// <summary>Compares two instances of <see cref="T:System.Data.SqlTypes.SqlBoolean" /> to determine whether the first is less than the second.</summary>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure.</param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure.</param>
		/// <returns>
		///   <see cref="F:System.Data.SqlTypes.SqlBoolean.True" /> if the first instance is less than the second instance; otherwise, <see langword="false" />.</returns>
		// Token: 0x06002287 RID: 8839 RVA: 0x0009F7A3 File Offset: 0x0009D9A3
		public static SqlBoolean operator <(SqlBoolean x, SqlBoolean y)
		{
			if (!x.IsNull && !y.IsNull)
			{
				return new SqlBoolean(x.m_value < y.m_value);
			}
			return SqlBoolean.Null;
		}

		/// <summary>Compares two <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structures to determine whether the first is greater than the second.</summary>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> object.</param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> object.</param>
		/// <returns>
		///   <see cref="F:System.Data.SqlTypes.SqlBoolean.True" /> if the first instance is greater than the second instance; otherwise, <see cref="F:System.Data.SqlTypes.SqlBoolean.False" />.</returns>
		// Token: 0x06002288 RID: 8840 RVA: 0x0009F7D0 File Offset: 0x0009D9D0
		public static SqlBoolean operator >(SqlBoolean x, SqlBoolean y)
		{
			if (!x.IsNull && !y.IsNull)
			{
				return new SqlBoolean(x.m_value > y.m_value);
			}
			return SqlBoolean.Null;
		}

		/// <summary>Compares two instances of <see cref="T:System.Data.SqlTypes.SqlBoolean" /> to determine whether the first is less than or equal to the second.</summary>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure.</param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure.</param>
		/// <returns>
		///   <see cref="F:System.Data.SqlTypes.SqlBoolean.True" /> if the first instance is less than or equal to the second instance; otherwise, <see langword="false" />.</returns>
		// Token: 0x06002289 RID: 8841 RVA: 0x0009F7FD File Offset: 0x0009D9FD
		public static SqlBoolean operator <=(SqlBoolean x, SqlBoolean y)
		{
			if (!x.IsNull && !y.IsNull)
			{
				return new SqlBoolean(x.m_value <= y.m_value);
			}
			return SqlBoolean.Null;
		}

		/// <summary>Compares two <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structures to determine whether the first is greater than or equal to the second.</summary>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure.</param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure.</param>
		/// <returns>
		///   <see cref="F:System.Data.SqlTypes.SqlBoolean.True" /> if the first instance is greater than or equal to the second instance; otherwise, <see langword="false" />.</returns>
		// Token: 0x0600228A RID: 8842 RVA: 0x0009F82D File Offset: 0x0009DA2D
		public static SqlBoolean operator >=(SqlBoolean x, SqlBoolean y)
		{
			if (!x.IsNull && !y.IsNull)
			{
				return new SqlBoolean(x.m_value >= y.m_value);
			}
			return SqlBoolean.Null;
		}

		/// <summary>Performs a one's complement operation on the supplied <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structures.</summary>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure.</param>
		/// <returns>The one's complement of the supplied <see cref="T:System.Data.SqlTypes.SqlBoolean" />.</returns>
		// Token: 0x0600228B RID: 8843 RVA: 0x0009F85D File Offset: 0x0009DA5D
		public static SqlBoolean OnesComplement(SqlBoolean x)
		{
			return ~x;
		}

		/// <summary>Computes the bitwise AND operation of two specified <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structures.</summary>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure.</param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure.</param>
		/// <returns>The result of the logical AND operation.</returns>
		// Token: 0x0600228C RID: 8844 RVA: 0x0009F865 File Offset: 0x0009DA65
		public static SqlBoolean And(SqlBoolean x, SqlBoolean y)
		{
			return x & y;
		}

		/// <summary>Performs a bitwise OR operation on the two specified <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structures.</summary>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure.</param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure.</param>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure whose Value is the result of the bitwise OR operation.</returns>
		// Token: 0x0600228D RID: 8845 RVA: 0x0009F86E File Offset: 0x0009DA6E
		public static SqlBoolean Or(SqlBoolean x, SqlBoolean y)
		{
			return x | y;
		}

		/// <summary>Performs a bitwise exclusive-OR operation on the supplied parameters.</summary>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure.</param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure.</param>
		/// <returns>The result of the logical XOR operation.</returns>
		// Token: 0x0600228E RID: 8846 RVA: 0x0009F877 File Offset: 0x0009DA77
		public static SqlBoolean Xor(SqlBoolean x, SqlBoolean y)
		{
			return x ^ y;
		}

		/// <summary>Compares two <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structures to determine whether they are equal.</summary>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure.</param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure.</param>
		/// <returns>
		///   <see cref="F:System.Data.SqlTypes.SqlBoolean.True" /> if the two instances are equal or <see cref="F:System.Data.SqlTypes.SqlBoolean.False" /> if the two instances are not equal. If either instance of <see cref="T:System.Data.SqlTypes.SqlBoolean" /> is null, the <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> will be <see cref="F:System.Data.SqlTypes.SqlBoolean.Null" />.</returns>
		// Token: 0x0600228F RID: 8847 RVA: 0x0009F880 File Offset: 0x0009DA80
		public static SqlBoolean Equals(SqlBoolean x, SqlBoolean y)
		{
			return x == y;
		}

		/// <summary>Compares two instances of <see cref="T:System.Data.SqlTypes.SqlBoolean" /> for equality.</summary>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure.</param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure.</param>
		/// <returns>
		///   <see cref="F:System.Data.SqlTypes.SqlBoolean.True" /> if the two instances are not equal or <see cref="F:System.Data.SqlTypes.SqlBoolean.False" /> if the two instances are equal. If either instance of <see cref="T:System.Data.SqlTypes.SqlBoolean" /> is null, the <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> will be <see cref="F:System.Data.SqlTypes.SqlBoolean.Null" />.</returns>
		// Token: 0x06002290 RID: 8848 RVA: 0x0009F889 File Offset: 0x0009DA89
		public static SqlBoolean NotEquals(SqlBoolean x, SqlBoolean y)
		{
			return x != y;
		}

		/// <summary>Compares two instances of <see cref="T:System.Data.SqlTypes.SqlBoolean" /> to determine whether the first is greater than the second.</summary>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure.</param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure.</param>
		/// <returns>
		///   <see cref="F:System.Data.SqlTypes.SqlBoolean.True" /> if the first instance is greater than the second instance; otherwise, <see cref="F:System.Data.SqlTypes.SqlBoolean.False" />.</returns>
		// Token: 0x06002291 RID: 8849 RVA: 0x0009F892 File Offset: 0x0009DA92
		public static SqlBoolean GreaterThan(SqlBoolean x, SqlBoolean y)
		{
			return x > y;
		}

		/// <summary>Compares two instances of <see cref="T:System.Data.SqlTypes.SqlBoolean" /> to determine whether the first is less than the second.</summary>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure.</param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure.</param>
		/// <returns>
		///   <see cref="F:System.Data.SqlTypes.SqlBoolean.True" /> if the first instance is less than the second instance; otherwise, <see cref="F:System.Data.SqlTypes.SqlBoolean.False" />.</returns>
		// Token: 0x06002292 RID: 8850 RVA: 0x0009F89B File Offset: 0x0009DA9B
		public static SqlBoolean LessThan(SqlBoolean x, SqlBoolean y)
		{
			return x < y;
		}

		/// <summary>Compares two instances of <see cref="T:System.Data.SqlTypes.SqlBoolean" /> to determine whether the first is greater than or equal to the second.</summary>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure.</param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure.</param>
		/// <returns>
		///   <see cref="F:System.Data.SqlTypes.SqlBoolean.True" /> if the first instance is greater than or equal to the second instance; otherwise, <see langword="false" />.</returns>
		// Token: 0x06002293 RID: 8851 RVA: 0x0009F8A4 File Offset: 0x0009DAA4
		public static SqlBoolean GreaterThanOrEquals(SqlBoolean x, SqlBoolean y)
		{
			return x >= y;
		}

		/// <summary>Compares two instances of <see cref="T:System.Data.SqlTypes.SqlBoolean" /> to determine whether the first is less than or equal to the second.</summary>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure.</param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure.</param>
		/// <returns>
		///   <see cref="F:System.Data.SqlTypes.SqlBoolean.True" /> if the first instance is less than or equal to the second instance; otherwise, <see cref="F:System.Data.SqlTypes.SqlBoolean.False" />.</returns>
		// Token: 0x06002294 RID: 8852 RVA: 0x0009F8AD File Offset: 0x0009DAAD
		public static SqlBoolean LessThanOrEquals(SqlBoolean x, SqlBoolean y)
		{
			return x <= y;
		}

		/// <summary>Converts this <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure to <see cref="T:System.Data.SqlTypes.SqlByte" />.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlByte" /> structure whose value is 1 or 0. If the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure's value equals <see langword="true" />, the new <see cref="T:System.Data.SqlTypes.SqlByte" /> structure's value is 1. Otherwise, the new <see cref="T:System.Data.SqlTypes.SqlByte" /> structure's value is 0.</returns>
		// Token: 0x06002295 RID: 8853 RVA: 0x0009F8B6 File Offset: 0x0009DAB6
		public SqlByte ToSqlByte()
		{
			return (SqlByte)this;
		}

		/// <summary>Converts this <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure to <see cref="T:System.Data.SqlTypes.SqlDouble" />.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlDouble" /> structure whose value is 1 or 0. If the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure's value equals <see langword="true" /> then the new <see cref="T:System.Data.SqlTypes.SqlDouble" /> structure's value is 1. Otherwise, the new <see cref="T:System.Data.SqlTypes.SqlDouble" /> structure's value is 0.</returns>
		// Token: 0x06002296 RID: 8854 RVA: 0x0009F8C3 File Offset: 0x0009DAC3
		public SqlDouble ToSqlDouble()
		{
			return (SqlDouble)this;
		}

		/// <summary>Converts this <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure to <see cref="T:System.Data.SqlTypes.SqlInt16" />.</summary>
		/// <returns>A new <see langword="SqlInt16" /> structure whose value is 1 or 0. If the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure's value equals <see langword="true" /> then the new <see cref="T:System.Data.SqlTypes.SqlInt16" /> structure's value is 1. Otherwise, the new <see langword="SqlInt16" /> structure's value is 0.</returns>
		// Token: 0x06002297 RID: 8855 RVA: 0x0009F8D0 File Offset: 0x0009DAD0
		public SqlInt16 ToSqlInt16()
		{
			return (SqlInt16)this;
		}

		/// <summary>Converts this <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure to <see cref="T:System.Data.SqlTypes.SqlInt32" />.</summary>
		/// <returns>A new <see langword="SqlInt32" /> structure whose value is 1 or 0. If the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure's value equals <see langword="true" />, the new <see cref="T:System.Data.SqlTypes.SqlInt32" /> structure's value is 1. Otherwise, the new <see langword="SqlInt32" /> structure's value is 0.</returns>
		// Token: 0x06002298 RID: 8856 RVA: 0x0009F8DD File Offset: 0x0009DADD
		public SqlInt32 ToSqlInt32()
		{
			return (SqlInt32)this;
		}

		/// <summary>Converts this <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure to <see cref="T:System.Data.SqlTypes.SqlInt64" />.</summary>
		/// <returns>A new <see langword="SqlInt64" /> structure whose value is 1 or 0. If the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure's value equals <see langword="true" />, the new <see cref="T:System.Data.SqlTypes.SqlInt64" /> structure's value is 1. Otherwise, the new <see langword="SqlInt64" /> structure's value is 0.</returns>
		// Token: 0x06002299 RID: 8857 RVA: 0x0009F8EA File Offset: 0x0009DAEA
		public SqlInt64 ToSqlInt64()
		{
			return (SqlInt64)this;
		}

		/// <summary>Converts this <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure to <see cref="T:System.Data.SqlTypes.SqlMoney" />.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlMoney" /> structure whose value is 1 or 0. If the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure's value equals <see langword="true" />, the new <see cref="T:System.Data.SqlTypes.SqlMoney" /> value is 1. If the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure's value equals <see langword="false" />, the new <see cref="T:System.Data.SqlTypes.SqlMoney" /> value is 0. If <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure's value is neither 1 nor 0, the new <see cref="T:System.Data.SqlTypes.SqlMoney" /> value is <see cref="F:System.Data.SqlTypes.SqlMoney.Null" />.</returns>
		// Token: 0x0600229A RID: 8858 RVA: 0x0009F8F7 File Offset: 0x0009DAF7
		public SqlMoney ToSqlMoney()
		{
			return (SqlMoney)this;
		}

		/// <summary>Converts this <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure to <see cref="T:System.Data.SqlTypes.SqlDecimal" />.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure whose value is 1 or 0. If the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure's value equals <see langword="true" /> then the new <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure's value is 1. Otherwise, the new <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure's value is 0.</returns>
		// Token: 0x0600229B RID: 8859 RVA: 0x0009F904 File Offset: 0x0009DB04
		public SqlDecimal ToSqlDecimal()
		{
			return (SqlDecimal)this;
		}

		/// <summary>Converts this <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure to <see cref="T:System.Data.SqlTypes.SqlSingle" />.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlSingle" /> structure whose value is 1 or 0.  
		///  If the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure's value equals true, the new <see cref="T:System.Data.SqlTypes.SqlSingle" /> structure's value is 1; otherwise the new <see cref="T:System.Data.SqlTypes.SqlSingle" /> structure's value is 0.</returns>
		// Token: 0x0600229C RID: 8860 RVA: 0x0009F911 File Offset: 0x0009DB11
		public SqlSingle ToSqlSingle()
		{
			return (SqlSingle)this;
		}

		/// <summary>Converts this <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure to <see cref="T:System.Data.SqlTypes.SqlString" />.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlString" /> structure whose value is 1 or 0. If the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure's value equals <see langword="true" /> then <see cref="T:System.Data.SqlTypes.SqlString" /> structure's value is 1. Otherwise, the new <see cref="T:System.Data.SqlTypes.SqlString" /> structure's value is 0.</returns>
		// Token: 0x0600229D RID: 8861 RVA: 0x0009F91E File Offset: 0x0009DB1E
		public SqlString ToSqlString()
		{
			return (SqlString)this;
		}

		/// <summary>Compares this <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure to a specified object and returns an indication of their relative values.</summary>
		/// <param name="value">An object to compare, or a null reference (<see langword="Nothing" /> in Visual Basic).</param>
		/// <returns>A signed number that indicates the relative values of the instance and value.  
		///   Value  
		///
		///   Description  
		///
		///   A negative integer  
		///
		///   This instance is less than <paramref name="value" />.  
		///
		///   Zero  
		///
		///   This instance is equal to <paramref name="value" />.  
		///
		///   A positive integer  
		///
		///   This instance is greater than <paramref name="value" />.  
		///
		///  -or-  
		///
		///  <paramref name="value" /> is a null reference (<see langword="Nothing" /> in Visual Basic).</returns>
		// Token: 0x0600229E RID: 8862 RVA: 0x0009F92C File Offset: 0x0009DB2C
		public int CompareTo(object value)
		{
			if (value is SqlBoolean)
			{
				SqlBoolean value2 = (SqlBoolean)value;
				return this.CompareTo(value2);
			}
			throw ADP.WrongType(value.GetType(), typeof(SqlBoolean));
		}

		/// <summary>Compares this <see cref="T:System.Data.SqlTypes.SqlBoolean" /> object to the supplied <see cref="T:System.Data.SqlTypes.SqlBoolean" /> object and returns an indication of their relative values.</summary>
		/// <param name="value">A <see cref="T:System.Data.SqlTypes.SqlBoolean" /><see cref="T:System.Data.SqlTypes.SqlBoolean" /> object to compare, or a null reference (<see langword="Nothing" /> in Visual Basic).</param>
		/// <returns>A signed number that indicates the relative values of the instance and value.  
		///   Value  
		///
		///   Description  
		///
		///   A negative integer  
		///
		///   This instance is less than <paramref name="value" />.  
		///
		///   Zero  
		///
		///   This instance is equal to <paramref name="value" />.  
		///
		///   A positive integer  
		///
		///   This instance is greater than <paramref name="value" />.  
		///
		///  -or-  
		///
		///  <paramref name="value" /> is a null reference (<see langword="Nothing" /> in Visual Basic).</returns>
		// Token: 0x0600229F RID: 8863 RVA: 0x0009F968 File Offset: 0x0009DB68
		public int CompareTo(SqlBoolean value)
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
				if (this.ByteValue < value.ByteValue)
				{
					return -1;
				}
				if (this.ByteValue > value.ByteValue)
				{
					return 1;
				}
				return 0;
			}
		}

		/// <summary>Compares the supplied object parameter to the <see cref="T:System.Data.SqlTypes.SqlBoolean" />.</summary>
		/// <param name="value">The object to be compared.</param>
		/// <returns>
		///   <see langword="true" /> if object is an instance of <see cref="T:System.Data.SqlTypes.SqlBoolean" /> and the two are equal; otherwise, <see langword="false" />.</returns>
		// Token: 0x060022A0 RID: 8864 RVA: 0x0009F9B8 File Offset: 0x0009DBB8
		public override bool Equals(object value)
		{
			if (!(value is SqlBoolean))
			{
				return false;
			}
			SqlBoolean y = (SqlBoolean)value;
			if (y.IsNull || this.IsNull)
			{
				return y.IsNull && this.IsNull;
			}
			return (this == y).Value;
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		// Token: 0x060022A1 RID: 8865 RVA: 0x0009FA10 File Offset: 0x0009DC10
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
		// Token: 0x060022A2 RID: 8866 RVA: 0x00003E32 File Offset: 0x00002032
		XmlSchema IXmlSerializable.GetSchema()
		{
			return null;
		}

		/// <summary>This member supports the .NET Framework infrastructure and is not intended to be used directly from your code.</summary>
		/// <param name="reader">
		///   <see langword="XmlReader" />
		/// </param>
		// Token: 0x060022A3 RID: 8867 RVA: 0x0009FA38 File Offset: 0x0009DC38
		void IXmlSerializable.ReadXml(XmlReader reader)
		{
			string attribute = reader.GetAttribute("nil", "http://www.w3.org/2001/XMLSchema-instance");
			if (attribute != null && XmlConvert.ToBoolean(attribute))
			{
				reader.ReadElementString();
				this.m_value = 0;
				return;
			}
			this.m_value = (XmlConvert.ToBoolean(reader.ReadElementString()) ? 2 : 1);
		}

		/// <summary>This member supports the .NET Framework infrastructure and is not intended to be used directly from your code.</summary>
		/// <param name="writer">
		///   <see langword="XmlWriter" />
		/// </param>
		// Token: 0x060022A4 RID: 8868 RVA: 0x0009FA87 File Offset: 0x0009DC87
		void IXmlSerializable.WriteXml(XmlWriter writer)
		{
			if (this.IsNull)
			{
				writer.WriteAttributeString("xsi", "nil", "http://www.w3.org/2001/XMLSchema-instance", "true");
				return;
			}
			writer.WriteString((this.m_value == 2) ? "true" : "false");
		}

		/// <summary>Returns the XML Schema definition language (XSD) of the specified <see cref="T:System.Xml.Schema.XmlSchemaSet" />.</summary>
		/// <param name="schemaSet">A <see cref="T:System.Xml.Schema.XmlSchemaSet" />.</param>
		/// <returns>A <see langword="string" /> value that indicates the XSD of the specified <see cref="T:System.Xml.Schema.XmlSchemaSet" />.</returns>
		// Token: 0x060022A5 RID: 8869 RVA: 0x0009FAC7 File Offset: 0x0009DCC7
		public static XmlQualifiedName GetXsdType(XmlSchemaSet schemaSet)
		{
			return new XmlQualifiedName("boolean", "http://www.w3.org/2001/XMLSchema");
		}

		// Token: 0x060022A6 RID: 8870 RVA: 0x0009FAD8 File Offset: 0x0009DCD8
		// Note: this type is marked as 'beforefieldinit'.
		static SqlBoolean()
		{
		}

		// Token: 0x04001824 RID: 6180
		private byte m_value;

		// Token: 0x04001825 RID: 6181
		private const byte x_Null = 0;

		// Token: 0x04001826 RID: 6182
		private const byte x_False = 1;

		// Token: 0x04001827 RID: 6183
		private const byte x_True = 2;

		/// <summary>Represents a true value that can be assigned to the <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> property of an instance of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure.</summary>
		// Token: 0x04001828 RID: 6184
		public static readonly SqlBoolean True = new SqlBoolean(true);

		/// <summary>Represents a false value that can be assigned to the <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> property of an instance of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure.</summary>
		// Token: 0x04001829 RID: 6185
		public static readonly SqlBoolean False = new SqlBoolean(false);

		/// <summary>Represents <see cref="T:System.DBNull" /> that can be assigned to this instance of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure.</summary>
		// Token: 0x0400182A RID: 6186
		public static readonly SqlBoolean Null = new SqlBoolean(0, true);

		/// <summary>Represents a zero value that can be assigned to the <see cref="P:System.Data.SqlTypes.SqlBoolean.ByteValue" /> property of an instance of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure.</summary>
		// Token: 0x0400182B RID: 6187
		public static readonly SqlBoolean Zero = new SqlBoolean(0);

		/// <summary>Represents a one value that can be assigned to the <see cref="P:System.Data.SqlTypes.SqlBoolean.ByteValue" /> property of an instance of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure.</summary>
		// Token: 0x0400182C RID: 6188
		public static readonly SqlBoolean One = new SqlBoolean(1);
	}
}

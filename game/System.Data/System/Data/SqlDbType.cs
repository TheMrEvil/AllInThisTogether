using System;

namespace System.Data
{
	/// <summary>Specifies SQL Server-specific data type of a field, property, for use in a <see cref="T:System.Data.SqlClient.SqlParameter" />.</summary>
	// Token: 0x02000130 RID: 304
	public enum SqlDbType
	{
		/// <summary>
		///   <see cref="T:System.Int64" />. A 64-bit signed integer.</summary>
		// Token: 0x04000A23 RID: 2595
		BigInt,
		/// <summary>
		///   <see cref="T:System.Array" /> of type <see cref="T:System.Byte" />. A fixed-length stream of binary data ranging between 1 and 8,000 bytes.</summary>
		// Token: 0x04000A24 RID: 2596
		Binary,
		/// <summary>
		///   <see cref="T:System.Boolean" />. An unsigned numeric value that can be 0, 1, or <see langword="null" />.</summary>
		// Token: 0x04000A25 RID: 2597
		Bit,
		/// <summary>
		///   <see cref="T:System.String" />. A fixed-length stream of non-Unicode characters ranging between 1 and 8,000 characters.</summary>
		// Token: 0x04000A26 RID: 2598
		Char,
		/// <summary>
		///   <see cref="T:System.DateTime" />. Date and time data ranging in value from January 1, 1753 to December 31, 9999 to an accuracy of 3.33 milliseconds.</summary>
		// Token: 0x04000A27 RID: 2599
		DateTime,
		/// <summary>
		///   <see cref="T:System.Decimal" />. A fixed precision and scale numeric value between -10 38 -1 and 10 38 -1.</summary>
		// Token: 0x04000A28 RID: 2600
		Decimal,
		/// <summary>
		///   <see cref="T:System.Double" />. A floating point number within the range of -1.79E +308 through 1.79E +308.</summary>
		// Token: 0x04000A29 RID: 2601
		Float,
		/// <summary>
		///   <see cref="T:System.Array" /> of type <see cref="T:System.Byte" />. A variable-length stream of binary data ranging from 0 to 2 31 -1 (or 2,147,483,647) bytes.</summary>
		// Token: 0x04000A2A RID: 2602
		Image,
		/// <summary>
		///   <see cref="T:System.Int32" />. A 32-bit signed integer.</summary>
		// Token: 0x04000A2B RID: 2603
		Int,
		/// <summary>
		///   <see cref="T:System.Decimal" />. A currency value ranging from -2 63 (or -9,223,372,036,854,775,808) to 2 63 -1 (or +9,223,372,036,854,775,807) with an accuracy to a ten-thousandth of a currency unit.</summary>
		// Token: 0x04000A2C RID: 2604
		Money,
		/// <summary>
		///   <see cref="T:System.String" />. A fixed-length stream of Unicode characters ranging between 1 and 4,000 characters.</summary>
		// Token: 0x04000A2D RID: 2605
		NChar,
		/// <summary>
		///   <see cref="T:System.String" />. A variable-length stream of Unicode data with a maximum length of 2 30 - 1 (or 1,073,741,823) characters.</summary>
		// Token: 0x04000A2E RID: 2606
		NText,
		/// <summary>
		///   <see cref="T:System.String" />. A variable-length stream of Unicode characters ranging between 1 and 4,000 characters. Implicit conversion fails if the string is greater than 4,000 characters. Explicitly set the object when working with strings longer than 4,000 characters. Use <see cref="F:System.Data.SqlDbType.NVarChar" /> when the database column is <see langword="nvarchar(max)" />.</summary>
		// Token: 0x04000A2F RID: 2607
		NVarChar,
		/// <summary>
		///   <see cref="T:System.Single" />. A floating point number within the range of -3.40E +38 through 3.40E +38.</summary>
		// Token: 0x04000A30 RID: 2608
		Real,
		/// <summary>
		///   <see cref="T:System.Guid" />. A globally unique identifier (or GUID).</summary>
		// Token: 0x04000A31 RID: 2609
		UniqueIdentifier,
		/// <summary>
		///   <see cref="T:System.DateTime" />. Date and time data ranging in value from January 1, 1900 to June 6, 2079 to an accuracy of one minute.</summary>
		// Token: 0x04000A32 RID: 2610
		SmallDateTime,
		/// <summary>
		///   <see cref="T:System.Int16" />. A 16-bit signed integer.</summary>
		// Token: 0x04000A33 RID: 2611
		SmallInt,
		/// <summary>
		///   <see cref="T:System.Decimal" />. A currency value ranging from -214,748.3648 to +214,748.3647 with an accuracy to a ten-thousandth of a currency unit.</summary>
		// Token: 0x04000A34 RID: 2612
		SmallMoney,
		/// <summary>
		///   <see cref="T:System.String" />. A variable-length stream of non-Unicode data with a maximum length of 2 31 -1 (or 2,147,483,647) characters.</summary>
		// Token: 0x04000A35 RID: 2613
		Text,
		/// <summary>
		///   <see cref="T:System.Array" /> of type <see cref="T:System.Byte" />. Automatically generated binary numbers, which are guaranteed to be unique within a database. <see langword="timestamp" /> is used typically as a mechanism for version-stamping table rows. The storage size is 8 bytes.</summary>
		// Token: 0x04000A36 RID: 2614
		Timestamp,
		/// <summary>
		///   <see cref="T:System.Byte" />. An 8-bit unsigned integer.</summary>
		// Token: 0x04000A37 RID: 2615
		TinyInt,
		/// <summary>
		///   <see cref="T:System.Array" /> of type <see cref="T:System.Byte" />. A variable-length stream of binary data ranging between 1 and 8,000 bytes. Implicit conversion fails if the byte array is greater than 8,000 bytes. Explicitly set the object when working with byte arrays larger than 8,000 bytes.</summary>
		// Token: 0x04000A38 RID: 2616
		VarBinary,
		/// <summary>
		///   <see cref="T:System.String" />. A variable-length stream of non-Unicode characters ranging between 1 and 8,000 characters. Use <see cref="F:System.Data.SqlDbType.VarChar" /> when the database column is <see langword="varchar(max)" />.</summary>
		// Token: 0x04000A39 RID: 2617
		VarChar,
		/// <summary>
		///   <see cref="T:System.Object" />. A special data type that can contain numeric, string, binary, or date data as well as the SQL Server values Empty and Null, which is assumed if no other type is declared.</summary>
		// Token: 0x04000A3A RID: 2618
		Variant,
		/// <summary>An XML value. Obtain the XML as a string using the <see cref="M:System.Data.SqlClient.SqlDataReader.GetValue(System.Int32)" /> method or <see cref="P:System.Data.SqlTypes.SqlXml.Value" /> property, or as an <see cref="T:System.Xml.XmlReader" /> by calling the <see cref="M:System.Data.SqlTypes.SqlXml.CreateReader" /> method.</summary>
		// Token: 0x04000A3B RID: 2619
		Xml = 25,
		/// <summary>A SQL Server user-defined type (UDT).</summary>
		// Token: 0x04000A3C RID: 2620
		Udt = 29,
		/// <summary>A special data type for specifying structured data contained in table-valued parameters.</summary>
		// Token: 0x04000A3D RID: 2621
		Structured,
		/// <summary>Date data ranging in value from January 1,1 AD through December 31, 9999 AD.</summary>
		// Token: 0x04000A3E RID: 2622
		Date,
		/// <summary>Time data based on a 24-hour clock. Time value range is 00:00:00 through 23:59:59.9999999 with an accuracy of 100 nanoseconds. Corresponds to a SQL Server <see langword="time" /> value.</summary>
		// Token: 0x04000A3F RID: 2623
		Time,
		/// <summary>Date and time data. Date value range is from January 1,1 AD through December 31, 9999 AD. Time value range is 00:00:00 through 23:59:59.9999999 with an accuracy of 100 nanoseconds.</summary>
		// Token: 0x04000A40 RID: 2624
		DateTime2,
		/// <summary>Date and time data with time zone awareness. Date value range is from January 1,1 AD through December 31, 9999 AD. Time value range is 00:00:00 through 23:59:59.9999999 with an accuracy of 100 nanoseconds. Time zone value range is -14:00 through +14:00.</summary>
		// Token: 0x04000A41 RID: 2625
		DateTimeOffset
	}
}

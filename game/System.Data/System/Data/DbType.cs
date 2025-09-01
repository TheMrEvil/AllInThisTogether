using System;

namespace System.Data
{
	/// <summary>Specifies the data type of a field, a property, or a <see langword="Parameter" /> object of a .NET Framework data provider.</summary>
	// Token: 0x020000DF RID: 223
	public enum DbType
	{
		/// <summary>A variable-length stream of non-Unicode characters ranging between 1 and 8,000 characters.</summary>
		// Token: 0x04000860 RID: 2144
		AnsiString,
		/// <summary>A variable-length stream of binary data ranging between 1 and 8,000 bytes.</summary>
		// Token: 0x04000861 RID: 2145
		Binary,
		/// <summary>An 8-bit unsigned integer ranging in value from 0 to 255.</summary>
		// Token: 0x04000862 RID: 2146
		Byte,
		/// <summary>A simple type representing Boolean values of <see langword="true" /> or <see langword="false" />.</summary>
		// Token: 0x04000863 RID: 2147
		Boolean,
		/// <summary>A currency value ranging from -2 63 (or -922,337,203,685,477.5808) to 2 63 -1 (or +922,337,203,685,477.5807) with an accuracy to a ten-thousandth of a currency unit.</summary>
		// Token: 0x04000864 RID: 2148
		Currency,
		/// <summary>A type representing a date value.</summary>
		// Token: 0x04000865 RID: 2149
		Date,
		/// <summary>A type representing a date and time value.</summary>
		// Token: 0x04000866 RID: 2150
		DateTime,
		/// <summary>A simple type representing values ranging from 1.0 x 10 -28 to approximately 7.9 x 10 28 with 28-29 significant digits.</summary>
		// Token: 0x04000867 RID: 2151
		Decimal,
		/// <summary>A floating point type representing values ranging from approximately 5.0 x 10 -324 to 1.7 x 10 308 with a precision of 15-16 digits.</summary>
		// Token: 0x04000868 RID: 2152
		Double,
		/// <summary>A globally unique identifier (or GUID).</summary>
		// Token: 0x04000869 RID: 2153
		Guid,
		/// <summary>An integral type representing signed 16-bit integers with values between -32768 and 32767.</summary>
		// Token: 0x0400086A RID: 2154
		Int16,
		/// <summary>An integral type representing signed 32-bit integers with values between -2147483648 and 2147483647.</summary>
		// Token: 0x0400086B RID: 2155
		Int32,
		/// <summary>An integral type representing signed 64-bit integers with values between -9223372036854775808 and 9223372036854775807.</summary>
		// Token: 0x0400086C RID: 2156
		Int64,
		/// <summary>A general type representing any reference or value type not explicitly represented by another <see langword="DbType" /> value.</summary>
		// Token: 0x0400086D RID: 2157
		Object,
		/// <summary>An integral type representing signed 8-bit integers with values between -128 and 127.</summary>
		// Token: 0x0400086E RID: 2158
		SByte,
		/// <summary>A floating point type representing values ranging from approximately 1.5 x 10 -45 to 3.4 x 10 38 with a precision of 7 digits.</summary>
		// Token: 0x0400086F RID: 2159
		Single,
		/// <summary>A type representing Unicode character strings.</summary>
		// Token: 0x04000870 RID: 2160
		String,
		/// <summary>A type representing a SQL Server <see langword="DateTime" /> value. If you want to use a SQL Server <see langword="time" /> value, use <see cref="F:System.Data.SqlDbType.Time" />.</summary>
		// Token: 0x04000871 RID: 2161
		Time,
		/// <summary>An integral type representing unsigned 16-bit integers with values between 0 and 65535.</summary>
		// Token: 0x04000872 RID: 2162
		UInt16,
		/// <summary>An integral type representing unsigned 32-bit integers with values between 0 and 4294967295.</summary>
		// Token: 0x04000873 RID: 2163
		UInt32,
		/// <summary>An integral type representing unsigned 64-bit integers with values between 0 and 18446744073709551615.</summary>
		// Token: 0x04000874 RID: 2164
		UInt64,
		/// <summary>A variable-length numeric value.</summary>
		// Token: 0x04000875 RID: 2165
		VarNumeric,
		/// <summary>A fixed-length stream of non-Unicode characters.</summary>
		// Token: 0x04000876 RID: 2166
		AnsiStringFixedLength,
		/// <summary>A fixed-length string of Unicode characters.</summary>
		// Token: 0x04000877 RID: 2167
		StringFixedLength,
		/// <summary>A parsed representation of an XML document or fragment.</summary>
		// Token: 0x04000878 RID: 2168
		Xml = 25,
		/// <summary>Date and time data. Date value range is from January 1,1 AD through December 31, 9999 AD. Time value range is 00:00:00 through 23:59:59.9999999 with an accuracy of 100 nanoseconds.</summary>
		// Token: 0x04000879 RID: 2169
		DateTime2,
		/// <summary>Date and time data with time zone awareness. Date value range is from January 1,1 AD through December 31, 9999 AD. Time value range is 00:00:00 through 23:59:59.9999999 with an accuracy of 100 nanoseconds. Time zone value range is -14:00 through +14:00.</summary>
		// Token: 0x0400087A RID: 2170
		DateTimeOffset
	}
}

using System;

namespace System.Data.OleDb
{
	/// <summary>Specifies the data type of a field, a property, for use in an <see cref="T:System.Data.OleDb.OleDbParameter" />.</summary>
	// Token: 0x0200015C RID: 348
	public enum OleDbType
	{
		/// <summary>A 64-bit signed integer (DBTYPE_I8). This maps to <see cref="T:System.Int64" />.</summary>
		// Token: 0x04000BAC RID: 2988
		BigInt = 20,
		/// <summary>A stream of binary data (DBTYPE_BYTES). This maps to an <see cref="T:System.Array" /> of type <see cref="T:System.Byte" />.</summary>
		// Token: 0x04000BAD RID: 2989
		Binary = 128,
		/// <summary>A Boolean value (DBTYPE_BOOL). This maps to <see cref="T:System.Boolean" />.</summary>
		// Token: 0x04000BAE RID: 2990
		Boolean = 11,
		/// <summary>A null-terminated character string of Unicode characters (DBTYPE_BSTR). This maps to <see cref="T:System.String" />.</summary>
		// Token: 0x04000BAF RID: 2991
		BSTR = 8,
		/// <summary>A character string (DBTYPE_STR). This maps to <see cref="T:System.String" />.</summary>
		// Token: 0x04000BB0 RID: 2992
		Char = 129,
		/// <summary>A currency value ranging from -2 63 (or -922,337,203,685,477.5808) to 2 63 -1 (or +922,337,203,685,477.5807) with an accuracy to a ten-thousandth of a currency unit (DBTYPE_CY). This maps to <see cref="T:System.Decimal" />.</summary>
		// Token: 0x04000BB1 RID: 2993
		Currency = 6,
		/// <summary>Date data, stored as a double (DBTYPE_DATE). The whole portion is the number of days since December 30, 1899, and the fractional portion is a fraction of a day. This maps to <see cref="T:System.DateTime" />.</summary>
		// Token: 0x04000BB2 RID: 2994
		Date,
		/// <summary>Date data in the format yyyymmdd (DBTYPE_DBDATE). This maps to <see cref="T:System.DateTime" />.</summary>
		// Token: 0x04000BB3 RID: 2995
		DBDate = 133,
		/// <summary>Time data in the format hhmmss (DBTYPE_DBTIME). This maps to <see cref="T:System.TimeSpan" />.</summary>
		// Token: 0x04000BB4 RID: 2996
		DBTime,
		/// <summary>Data and time data in the format yyyymmddhhmmss (DBTYPE_DBTIMESTAMP). This maps to <see cref="T:System.DateTime" />.</summary>
		// Token: 0x04000BB5 RID: 2997
		DBTimeStamp,
		/// <summary>A fixed precision and scale numeric value between -10 38 -1 and 10 38 -1 (DBTYPE_DECIMAL). This maps to <see cref="T:System.Decimal" />.</summary>
		// Token: 0x04000BB6 RID: 2998
		Decimal = 14,
		/// <summary>A floating-point number within the range of -1.79E +308 through 1.79E +308 (DBTYPE_R8). This maps to <see cref="T:System.Double" />.</summary>
		// Token: 0x04000BB7 RID: 2999
		Double = 5,
		/// <summary>No value (DBTYPE_EMPTY).</summary>
		// Token: 0x04000BB8 RID: 3000
		Empty = 0,
		/// <summary>A 32-bit error code (DBTYPE_ERROR). This maps to <see cref="T:System.Exception" />.</summary>
		// Token: 0x04000BB9 RID: 3001
		Error = 10,
		/// <summary>A 64-bit unsigned integer representing the number of 100-nanosecond intervals since January 1, 1601 (DBTYPE_FILETIME). This maps to <see cref="T:System.DateTime" />.</summary>
		// Token: 0x04000BBA RID: 3002
		Filetime = 64,
		/// <summary>A globally unique identifier (or GUID) (DBTYPE_GUID). This maps to <see cref="T:System.Guid" />.</summary>
		// Token: 0x04000BBB RID: 3003
		Guid = 72,
		/// <summary>A pointer to an <see langword="IDispatch" /> interface (DBTYPE_IDISPATCH). This maps to <see cref="T:System.Object" />.</summary>
		// Token: 0x04000BBC RID: 3004
		IDispatch = 9,
		/// <summary>A 32-bit signed integer (DBTYPE_I4). This maps to <see cref="T:System.Int32" />.</summary>
		// Token: 0x04000BBD RID: 3005
		Integer = 3,
		/// <summary>A pointer to an <see langword="IUnknown" /> interface (DBTYPE_UNKNOWN). This maps to <see cref="T:System.Object" />.</summary>
		// Token: 0x04000BBE RID: 3006
		IUnknown = 13,
		/// <summary>A long binary value (<see cref="T:System.Data.OleDb.OleDbParameter" /> only). This maps to an <see cref="T:System.Array" /> of type <see cref="T:System.Byte" />.</summary>
		// Token: 0x04000BBF RID: 3007
		LongVarBinary = 205,
		/// <summary>A long string value (<see cref="T:System.Data.OleDb.OleDbParameter" /> only). This maps to <see cref="T:System.String" />.</summary>
		// Token: 0x04000BC0 RID: 3008
		LongVarChar = 201,
		/// <summary>A long null-terminated Unicode string value (<see cref="T:System.Data.OleDb.OleDbParameter" /> only). This maps to <see cref="T:System.String" />.</summary>
		// Token: 0x04000BC1 RID: 3009
		LongVarWChar = 203,
		/// <summary>An exact numeric value with a fixed precision and scale (DBTYPE_NUMERIC). This maps to <see cref="T:System.Decimal" />.</summary>
		// Token: 0x04000BC2 RID: 3010
		Numeric = 131,
		/// <summary>An automation PROPVARIANT (DBTYPE_PROP_VARIANT). This maps to <see cref="T:System.Object" />.</summary>
		// Token: 0x04000BC3 RID: 3011
		PropVariant = 138,
		/// <summary>A floating-point number within the range of -3.40E +38 through 3.40E +38 (DBTYPE_R4). This maps to <see cref="T:System.Single" />.</summary>
		// Token: 0x04000BC4 RID: 3012
		Single = 4,
		/// <summary>A 16-bit signed integer (DBTYPE_I2). This maps to <see cref="T:System.Int16" />.</summary>
		// Token: 0x04000BC5 RID: 3013
		SmallInt = 2,
		/// <summary>A 8-bit signed integer (DBTYPE_I1). This maps to <see cref="T:System.SByte" />.</summary>
		// Token: 0x04000BC6 RID: 3014
		TinyInt = 16,
		/// <summary>A 64-bit unsigned integer (DBTYPE_UI8). This maps to <see cref="T:System.UInt64" />.</summary>
		// Token: 0x04000BC7 RID: 3015
		UnsignedBigInt = 21,
		/// <summary>A 32-bit unsigned integer (DBTYPE_UI4). This maps to <see cref="T:System.UInt32" />.</summary>
		// Token: 0x04000BC8 RID: 3016
		UnsignedInt = 19,
		/// <summary>A 16-bit unsigned integer (DBTYPE_UI2). This maps to <see cref="T:System.UInt16" />.</summary>
		// Token: 0x04000BC9 RID: 3017
		UnsignedSmallInt = 18,
		/// <summary>A 8-bit unsigned integer (DBTYPE_UI1). This maps to <see cref="T:System.Byte" />.</summary>
		// Token: 0x04000BCA RID: 3018
		UnsignedTinyInt = 17,
		/// <summary>A variable-length stream of binary data (<see cref="T:System.Data.OleDb.OleDbParameter" /> only). This maps to an <see cref="T:System.Array" /> of type <see cref="T:System.Byte" />.</summary>
		// Token: 0x04000BCB RID: 3019
		VarBinary = 204,
		/// <summary>A variable-length stream of non-Unicode characters (<see cref="T:System.Data.OleDb.OleDbParameter" /> only). This maps to <see cref="T:System.String" />.</summary>
		// Token: 0x04000BCC RID: 3020
		VarChar = 200,
		/// <summary>A special data type that can contain numeric, string, binary, or date data, and also the special values Empty and Null (DBTYPE_VARIANT). This type is assumed if no other is specified. This maps to <see cref="T:System.Object" />.</summary>
		// Token: 0x04000BCD RID: 3021
		Variant = 12,
		/// <summary>A variable-length numeric value (<see cref="T:System.Data.OleDb.OleDbParameter" /> only). This maps to <see cref="T:System.Decimal" />.</summary>
		// Token: 0x04000BCE RID: 3022
		VarNumeric = 139,
		/// <summary>A variable-length, null-terminated stream of Unicode characters (<see cref="T:System.Data.OleDb.OleDbParameter" /> only). This maps to <see cref="T:System.String" />.</summary>
		// Token: 0x04000BCF RID: 3023
		VarWChar = 202,
		/// <summary>A null-terminated stream of Unicode characters (DBTYPE_WSTR). This maps to <see cref="T:System.String" />.</summary>
		// Token: 0x04000BD0 RID: 3024
		WChar = 130
	}
}

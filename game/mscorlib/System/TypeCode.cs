using System;

namespace System
{
	/// <summary>Specifies the type of an object.</summary>
	// Token: 0x020001A3 RID: 419
	public enum TypeCode
	{
		/// <summary>A null reference.</summary>
		// Token: 0x04001348 RID: 4936
		Empty,
		/// <summary>A general type representing any reference or value type not explicitly represented by another <see langword="TypeCode" />.</summary>
		// Token: 0x04001349 RID: 4937
		Object,
		/// <summary>A database null (column) value.</summary>
		// Token: 0x0400134A RID: 4938
		DBNull,
		/// <summary>A simple type representing Boolean values of <see langword="true" /> or <see langword="false" />.</summary>
		// Token: 0x0400134B RID: 4939
		Boolean,
		/// <summary>An integral type representing unsigned 16-bit integers with values between 0 and 65535. The set of possible values for the <see cref="F:System.TypeCode.Char" /> type corresponds to the Unicode character set.</summary>
		// Token: 0x0400134C RID: 4940
		Char,
		/// <summary>An integral type representing signed 8-bit integers with values between -128 and 127.</summary>
		// Token: 0x0400134D RID: 4941
		SByte,
		/// <summary>An integral type representing unsigned 8-bit integers with values between 0 and 255.</summary>
		// Token: 0x0400134E RID: 4942
		Byte,
		/// <summary>An integral type representing signed 16-bit integers with values between -32768 and 32767.</summary>
		// Token: 0x0400134F RID: 4943
		Int16,
		/// <summary>An integral type representing unsigned 16-bit integers with values between 0 and 65535.</summary>
		// Token: 0x04001350 RID: 4944
		UInt16,
		/// <summary>An integral type representing signed 32-bit integers with values between -2147483648 and 2147483647.</summary>
		// Token: 0x04001351 RID: 4945
		Int32,
		/// <summary>An integral type representing unsigned 32-bit integers with values between 0 and 4294967295.</summary>
		// Token: 0x04001352 RID: 4946
		UInt32,
		/// <summary>An integral type representing signed 64-bit integers with values between -9223372036854775808 and 9223372036854775807.</summary>
		// Token: 0x04001353 RID: 4947
		Int64,
		/// <summary>An integral type representing unsigned 64-bit integers with values between 0 and 18446744073709551615.</summary>
		// Token: 0x04001354 RID: 4948
		UInt64,
		/// <summary>A floating point type representing values ranging from approximately 1.5 x 10 -45 to 3.4 x 10 38 with a precision of 7 digits.</summary>
		// Token: 0x04001355 RID: 4949
		Single,
		/// <summary>A floating point type representing values ranging from approximately 5.0 x 10 -324 to 1.7 x 10 308 with a precision of 15-16 digits.</summary>
		// Token: 0x04001356 RID: 4950
		Double,
		/// <summary>A simple type representing values ranging from 1.0 x 10 -28 to approximately 7.9 x 10 28 with 28-29 significant digits.</summary>
		// Token: 0x04001357 RID: 4951
		Decimal,
		/// <summary>A type representing a date and time value.</summary>
		// Token: 0x04001358 RID: 4952
		DateTime,
		/// <summary>A sealed class type representing Unicode character strings.</summary>
		// Token: 0x04001359 RID: 4953
		String = 18
	}
}

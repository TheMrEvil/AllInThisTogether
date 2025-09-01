using System;

namespace System.Data.SqlClient
{
	/// <summary>Bitwise flag that specifies one or more options to use with an instance of <see cref="T:System.Data.SqlClient.SqlBulkCopy" />.</summary>
	// Token: 0x020001AC RID: 428
	[Flags]
	public enum SqlBulkCopyOptions
	{
		/// <summary>Use the default values for all options.</summary>
		// Token: 0x04000D66 RID: 3430
		Default = 0,
		/// <summary>Preserve source identity values. When not specified, identity values are assigned by the destination.</summary>
		// Token: 0x04000D67 RID: 3431
		KeepIdentity = 1,
		/// <summary>Check constraints while data is being inserted. By default, constraints are not checked.</summary>
		// Token: 0x04000D68 RID: 3432
		CheckConstraints = 2,
		/// <summary>Obtain a bulk update lock for the duration of the bulk copy operation. When not specified, row locks are used.</summary>
		// Token: 0x04000D69 RID: 3433
		TableLock = 4,
		/// <summary>Preserve null values in the destination table regardless of the settings for default values. When not specified, null values are replaced by default values where applicable.</summary>
		// Token: 0x04000D6A RID: 3434
		KeepNulls = 8,
		/// <summary>When specified, cause the server to fire the insert triggers for the rows being inserted into the database.</summary>
		// Token: 0x04000D6B RID: 3435
		FireTriggers = 16,
		/// <summary>When specified, each batch of the bulk-copy operation will occur within a transaction. If you indicate this option and also provide a <see cref="T:System.Data.SqlClient.SqlTransaction" /> object to the constructor, an <see cref="T:System.ArgumentException" /> occurs.</summary>
		// Token: 0x04000D6C RID: 3436
		UseInternalTransaction = 32,
		/// <summary>When specified, AllowEncryptedValueModifications enables bulk copying of encrypted data between tables or databases, without decrypting the data. Typically, an application would select data from encrypted columns from one table without decrypting the data (the app would connect to the database with the column encryption setting keyword set to disabled) and then would use this option to bulk insert the data, which is still encrypted. For more information, see Always Encrypted.  
		///  Use caution when specifying AllowEncryptedValueModifications as this may lead to corrupting the database because the driver does not check if the data is indeed encrypted, or if it is correctly encrypted using the same encryption type, algorithm and key as the target column.</summary>
		// Token: 0x04000D6D RID: 3437
		AllowEncryptedValueModifications = 64
	}
}

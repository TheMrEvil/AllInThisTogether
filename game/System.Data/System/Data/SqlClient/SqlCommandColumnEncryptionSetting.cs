using System;

namespace System.Data.SqlClient
{
	/// <summary>Specifies how data will be sent and received when reading and writing encrypted columns. Depending on your specific query, performance impact may be reduced by bypassing the Always Encrypted driver's processing when non-encrypted columns are being used. Note that these settings cannot be used to bypass encryption and gain access to plaintext data. For details, see Always Encrypted (Database Engine).</summary>
	// Token: 0x02000285 RID: 645
	public enum SqlCommandColumnEncryptionSetting
	{
		/// <summary>Specifies that the command should default to the Always Encrypted setting in the connection string.</summary>
		// Token: 0x040014B8 RID: 5304
		UseConnectionSetting,
		/// <summary>Enables Always Encrypted for the query.</summary>
		// Token: 0x040014B9 RID: 5305
		Enabled,
		/// <summary>Specifies that only the results of the command should be processed by the Always Encrypted routine in the driver. Use this value when the command has no parameters that require encryption.</summary>
		// Token: 0x040014BA RID: 5306
		ResultSetOnly,
		/// <summary>Disables Always Encrypted for the query.</summary>
		// Token: 0x040014BB RID: 5307
		Disabled
	}
}

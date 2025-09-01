using System;

namespace System.Data.SqlClient
{
	/// <summary>Specifies that Always Encrypted functionality is enabled in a connection. Note that these settings cannot be used to bypass encryption and gain access to plaintext data. For details, see Always Encrypted (Database Engine).</summary>
	// Token: 0x02000283 RID: 643
	public enum SqlConnectionColumnEncryptionSetting
	{
		/// <summary>Specifies the connection does not use Always Encrypted. Should be used if no queries sent over the connection access encrypted columns.</summary>
		// Token: 0x040014AF RID: 5295
		Disabled,
		/// <summary>Enables Always Encrypted functionality for the connection. Query parameters that correspond to encrypted columns will be transparently encrypted and query results from encrypted columns will be transparently decrypted.</summary>
		// Token: 0x040014B0 RID: 5296
		Enabled
	}
}

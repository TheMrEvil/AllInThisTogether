using System;

namespace System.Security.AccessControl
{
	/// <summary>Specifies the cryptographic key operation for which an authorization rule controls access or auditing.</summary>
	// Token: 0x0200051D RID: 1309
	[Flags]
	public enum CryptoKeyRights
	{
		/// <summary>Read the key data.</summary>
		// Token: 0x04002473 RID: 9331
		ReadData = 1,
		/// <summary>Write key data.</summary>
		// Token: 0x04002474 RID: 9332
		WriteData = 2,
		/// <summary>Read extended attributes of the key.</summary>
		// Token: 0x04002475 RID: 9333
		ReadExtendedAttributes = 8,
		/// <summary>Write extended attributes of the key.</summary>
		// Token: 0x04002476 RID: 9334
		WriteExtendedAttributes = 16,
		/// <summary>Read attributes of the key.</summary>
		// Token: 0x04002477 RID: 9335
		ReadAttributes = 128,
		/// <summary>Write attributes of the key.</summary>
		// Token: 0x04002478 RID: 9336
		WriteAttributes = 256,
		/// <summary>Delete the key.</summary>
		// Token: 0x04002479 RID: 9337
		Delete = 65536,
		/// <summary>Read permissions for the key.</summary>
		// Token: 0x0400247A RID: 9338
		ReadPermissions = 131072,
		/// <summary>Change permissions for the key.</summary>
		// Token: 0x0400247B RID: 9339
		ChangePermissions = 262144,
		/// <summary>Take ownership of the key.</summary>
		// Token: 0x0400247C RID: 9340
		TakeOwnership = 524288,
		/// <summary>Use the key for synchronization.</summary>
		// Token: 0x0400247D RID: 9341
		Synchronize = 1048576,
		/// <summary>Full control of the key.</summary>
		// Token: 0x0400247E RID: 9342
		FullControl = 2032027,
		/// <summary>A combination of <see cref="F:System.Security.AccessControl.CryptoKeyRights.GenericRead" /> and <see cref="F:System.Security.AccessControl.CryptoKeyRights.GenericWrite" />.</summary>
		// Token: 0x0400247F RID: 9343
		GenericAll = 268435456,
		/// <summary>Not used.</summary>
		// Token: 0x04002480 RID: 9344
		GenericExecute = 536870912,
		/// <summary>Write the key data, extended attributes of the key, attributes of the key, and permissions for the key.</summary>
		// Token: 0x04002481 RID: 9345
		GenericWrite = 1073741824,
		/// <summary>Read the key data, extended attributes of the key, attributes of the key, and permissions for the key.</summary>
		// Token: 0x04002482 RID: 9346
		GenericRead = -2147483648
	}
}

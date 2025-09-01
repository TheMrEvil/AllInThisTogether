using System;

namespace System.Security.Cryptography.X509Certificates
{
	/// <summary>Defines where and how to import the private key of an X.509 certificate.</summary>
	// Token: 0x020004D8 RID: 1240
	[Flags]
	public enum X509KeyStorageFlags
	{
		/// <summary>The default key set is used.  The user key set is usually the default.</summary>
		// Token: 0x04002291 RID: 8849
		DefaultKeySet = 0,
		/// <summary>Private keys are stored in the current user store rather than the local computer store. This occurs even if the certificate specifies that the keys should go in the local computer store.</summary>
		// Token: 0x04002292 RID: 8850
		UserKeySet = 1,
		/// <summary>Private keys are stored in the local computer store rather than the current user store.</summary>
		// Token: 0x04002293 RID: 8851
		MachineKeySet = 2,
		/// <summary>Imported keys are marked as exportable.</summary>
		// Token: 0x04002294 RID: 8852
		Exportable = 4,
		/// <summary>Notify the user through a dialog box or other method that the key is accessed.  The Cryptographic Service Provider (CSP) in use defines the precise behavior.</summary>
		// Token: 0x04002295 RID: 8853
		UserProtected = 8,
		/// <summary>The key associated with a PFX file is persisted when importing a certificate.</summary>
		// Token: 0x04002296 RID: 8854
		PersistKeySet = 16,
		/// <summary>The key associated with a PFX file is created in memory and not persisted on disk when importing a certificate.</summary>
		// Token: 0x04002297 RID: 8855
		EphemeralKeySet = 32
	}
}

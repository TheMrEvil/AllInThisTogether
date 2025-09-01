using System;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
	/// <summary>Specifies flags that modify the behavior of the cryptographic service providers (CSP).</summary>
	// Token: 0x02000485 RID: 1157
	[Flags]
	[ComVisible(true)]
	[Serializable]
	public enum CspProviderFlags
	{
		/// <summary>Do not specify any settings.</summary>
		// Token: 0x04002146 RID: 8518
		NoFlags = 0,
		/// <summary>Use key information from the computer's key store.</summary>
		// Token: 0x04002147 RID: 8519
		UseMachineKeyStore = 1,
		/// <summary>Use key information from the default key container.</summary>
		// Token: 0x04002148 RID: 8520
		UseDefaultKeyContainer = 2,
		/// <summary>Use key information that cannot be exported.</summary>
		// Token: 0x04002149 RID: 8521
		UseNonExportableKey = 4,
		/// <summary>Use key information from the current key.</summary>
		// Token: 0x0400214A RID: 8522
		UseExistingKey = 8,
		/// <summary>Allow a key to be exported for archival or recovery.</summary>
		// Token: 0x0400214B RID: 8523
		UseArchivableKey = 16,
		/// <summary>Notify the user through a dialog box or another method when certain actions are attempting to use a key.  This flag is not compatible with the <see cref="F:System.Security.Cryptography.CspProviderFlags.NoPrompt" /> flag.</summary>
		// Token: 0x0400214C RID: 8524
		UseUserProtectedKey = 32,
		/// <summary>Prevent the CSP from displaying any user interface (UI) for this context.</summary>
		// Token: 0x0400214D RID: 8525
		NoPrompt = 64,
		/// <summary>Create a temporary key that is released when the associated Rivest-Shamir-Adleman (RSA) object is closed. Do not use this flag if you want your key to be independent of the RSA object.</summary>
		// Token: 0x0400214E RID: 8526
		CreateEphemeralKey = 128
	}
}

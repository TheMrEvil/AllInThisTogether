using System;

namespace System.Net.Security
{
	/// <summary>Indicates the security services requested for an authenticated stream.</summary>
	// Token: 0x02000858 RID: 2136
	public enum ProtectionLevel
	{
		/// <summary>Authentication only.</summary>
		// Token: 0x04002917 RID: 10519
		None,
		/// <summary>Sign data to help ensure the integrity of transmitted data.</summary>
		// Token: 0x04002918 RID: 10520
		Sign,
		/// <summary>Encrypt and sign data to help ensure the confidentiality and integrity of transmitted data.</summary>
		// Token: 0x04002919 RID: 10521
		EncryptAndSign
	}
}

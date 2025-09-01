using System;

namespace System.Security.Cryptography.X509Certificates
{
	/// <summary>Specifies the name of the X.509 certificate store to open.</summary>
	// Token: 0x020002BF RID: 703
	public enum StoreName
	{
		/// <summary>The X.509 certificate store for other users.</summary>
		// Token: 0x04000C62 RID: 3170
		AddressBook = 1,
		/// <summary>The X.509 certificate store for third-party certificate authorities (CAs).</summary>
		// Token: 0x04000C63 RID: 3171
		AuthRoot,
		/// <summary>The X.509 certificate store for intermediate certificate authorities (CAs).</summary>
		// Token: 0x04000C64 RID: 3172
		CertificateAuthority,
		/// <summary>The X.509 certificate store for revoked certificates.</summary>
		// Token: 0x04000C65 RID: 3173
		Disallowed,
		/// <summary>The X.509 certificate store for personal certificates.</summary>
		// Token: 0x04000C66 RID: 3174
		My,
		/// <summary>The X.509 certificate store for trusted root certificate authorities (CAs).</summary>
		// Token: 0x04000C67 RID: 3175
		Root,
		/// <summary>The X.509 certificate store for directly trusted people and resources.</summary>
		// Token: 0x04000C68 RID: 3176
		TrustedPeople,
		/// <summary>The X.509 certificate store for directly trusted publishers.</summary>
		// Token: 0x04000C69 RID: 3177
		TrustedPublisher
	}
}

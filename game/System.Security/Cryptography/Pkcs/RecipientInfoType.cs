using System;

namespace System.Security.Cryptography.Pkcs
{
	/// <summary>The <see cref="T:System.Security.Cryptography.Pkcs.RecipientInfoType" /> enumeration defines the types of recipient information.</summary>
	// Token: 0x02000083 RID: 131
	public enum RecipientInfoType
	{
		/// <summary>The recipient information type is unknown.</summary>
		// Token: 0x0400029F RID: 671
		Unknown,
		/// <summary>Key transport recipient information.</summary>
		// Token: 0x040002A0 RID: 672
		KeyTransport,
		/// <summary>Key agreement recipient information.</summary>
		// Token: 0x040002A1 RID: 673
		KeyAgreement
	}
}

using System;

namespace System.Security.Cryptography.X509Certificates
{
	/// <summary>Specifies the type of selection requested using the <see cref="Overload:System.Security.Cryptography.X509Certificates.X509Certificate2UI.SelectFromCollection" /> method.</summary>
	// Token: 0x02000018 RID: 24
	public enum X509SelectionFlag
	{
		/// <summary>A single selection. The UI allows the user to select one X.509 certificate.</summary>
		// Token: 0x0400013C RID: 316
		SingleSelection,
		/// <summary>A multiple selection. The user can use the SHIFT or CRTL keys to select more than one X.509 certificate.</summary>
		// Token: 0x0400013D RID: 317
		MultiSelection
	}
}

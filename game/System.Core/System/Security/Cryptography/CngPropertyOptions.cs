using System;

namespace System.Security.Cryptography
{
	/// <summary>Specifies Cryptography Next Generation (CNG) key property options.</summary>
	// Token: 0x02000052 RID: 82
	[Flags]
	public enum CngPropertyOptions
	{
		/// <summary>The referenced property has no options.</summary>
		// Token: 0x04000346 RID: 838
		None = 0,
		/// <summary>The property is not specified by CNG. Use this option to avoid future name conflicts with CNG properties.</summary>
		// Token: 0x04000347 RID: 839
		CustomProperty = 1073741824,
		/// <summary>The property should be persisted.</summary>
		// Token: 0x04000348 RID: 840
		Persist = -2147483648
	}
}

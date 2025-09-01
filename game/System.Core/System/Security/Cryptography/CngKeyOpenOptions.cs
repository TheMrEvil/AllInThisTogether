using System;

namespace System.Security.Cryptography
{
	/// <summary>Specifies options for opening a key.</summary>
	// Token: 0x0200004F RID: 79
	[Flags]
	public enum CngKeyOpenOptions
	{
		/// <summary>No key open options are specified.</summary>
		// Token: 0x04000338 RID: 824
		None = 0,
		/// <summary>If the <see cref="F:System.Security.Cryptography.CngKeyOpenOptions.MachineKey" /> value is not specified, a user key is opened instead.</summary>
		// Token: 0x04000339 RID: 825
		UserKey = 0,
		/// <summary>A machine-wide key is opened.</summary>
		// Token: 0x0400033A RID: 826
		MachineKey = 32,
		/// <summary>UI prompting is suppressed.</summary>
		// Token: 0x0400033B RID: 827
		Silent = 64
	}
}

using System;

namespace System.Security.Cryptography
{
	/// <summary>Specifies options used for key creation.</summary>
	// Token: 0x0200004E RID: 78
	[Flags]
	public enum CngKeyCreationOptions
	{
		/// <summary>No key creation options are used.</summary>
		// Token: 0x04000334 RID: 820
		None = 0,
		/// <summary>A machine-wide key is created.</summary>
		// Token: 0x04000335 RID: 821
		MachineKey = 32,
		/// <summary>The existing key is overwritten during key creation.</summary>
		// Token: 0x04000336 RID: 822
		OverwriteExistingKey = 128
	}
}

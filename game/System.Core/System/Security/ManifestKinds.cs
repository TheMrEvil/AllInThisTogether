using System;

namespace System.Security
{
	/// <summary>Represents the type of manifest that the signature information applies to.</summary>
	// Token: 0x0200036B RID: 875
	[Flags]
	public enum ManifestKinds
	{
		/// <summary>The manifest is for an application. </summary>
		// Token: 0x04000CD7 RID: 3287
		Application = 2,
		/// <summary>The manifest is for deployment and application. The is the default value for verifying signatures. </summary>
		// Token: 0x04000CD8 RID: 3288
		ApplicationAndDeployment = 3,
		/// <summary>The manifest is for deployment only.</summary>
		// Token: 0x04000CD9 RID: 3289
		Deployment = 1,
		/// <summary>The manifest is of no particular type. </summary>
		// Token: 0x04000CDA RID: 3290
		None = 0
	}
}

using System;

namespace System.Reflection
{
	/// <summary>Specifies the resource location.</summary>
	// Token: 0x020008C0 RID: 2240
	[Flags]
	public enum ResourceLocation
	{
		/// <summary>Specifies that the resource is contained in another assembly.</summary>
		// Token: 0x04002F2C RID: 12076
		ContainedInAnotherAssembly = 2,
		/// <summary>Specifies that the resource is contained in the manifest file.</summary>
		// Token: 0x04002F2D RID: 12077
		ContainedInManifestFile = 4,
		/// <summary>Specifies an embedded (that is, non-linked) resource.</summary>
		// Token: 0x04002F2E RID: 12078
		Embedded = 1
	}
}

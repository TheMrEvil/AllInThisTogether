using System;

namespace System.Resources
{
	/// <summary>Specifies whether a <see cref="T:System.Resources.ResourceManager" /> object looks for the resources of the app's default culture in the main assembly or in a satellite assembly.</summary>
	// Token: 0x02000862 RID: 2146
	public enum UltimateResourceFallbackLocation
	{
		/// <summary>Fallback resources are located in the main assembly.</summary>
		// Token: 0x04002DC9 RID: 11721
		MainAssembly,
		/// <summary>Fallback resources are located in a satellite assembly.</summary>
		// Token: 0x04002DCA RID: 11722
		Satellite
	}
}

using System;

namespace System.Security.Policy
{
	/// <summary>Specifies how to match versions when locating application trusts in a collection.</summary>
	// Token: 0x020003F8 RID: 1016
	public enum ApplicationVersionMatch
	{
		/// <summary>Match on all versions.</summary>
		// Token: 0x04001F4A RID: 8010
		MatchAllVersions = 1,
		/// <summary>Match on the exact version.</summary>
		// Token: 0x04001F4B RID: 8011
		MatchExactVersion = 0
	}
}

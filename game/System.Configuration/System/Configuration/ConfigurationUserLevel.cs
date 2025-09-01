using System;

namespace System.Configuration
{
	/// <summary>Used to specify which configuration file is to be represented by the Configuration object.</summary>
	// Token: 0x02000035 RID: 53
	public enum ConfigurationUserLevel
	{
		/// <summary>Gets the <see cref="T:System.Configuration.Configuration" /> that applies to all users.</summary>
		// Token: 0x040000D2 RID: 210
		None,
		/// <summary>Gets the roaming <see cref="T:System.Configuration.Configuration" /> that applies to the current user.</summary>
		// Token: 0x040000D3 RID: 211
		PerUserRoaming = 10,
		/// <summary>Gets the local <see cref="T:System.Configuration.Configuration" /> that applies to the current user.</summary>
		// Token: 0x040000D4 RID: 212
		PerUserRoamingAndLocal = 20
	}
}

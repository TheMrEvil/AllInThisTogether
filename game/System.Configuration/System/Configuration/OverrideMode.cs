using System;

namespace System.Configuration
{
	/// <summary>Specifies the override behavior of a configuration element for configuration elements in child directories.</summary>
	// Token: 0x0200008B RID: 139
	public enum OverrideMode
	{
		/// <summary>The configuration setting of the element or group can be overridden by configuration settings that are in child directories.</summary>
		// Token: 0x04000169 RID: 361
		Allow = 1,
		/// <summary>The configuration setting of the element or group cannot be overridden by configuration settings that are in child directories.</summary>
		// Token: 0x0400016A RID: 362
		Deny,
		/// <summary>The configuration setting of the element or group will be overridden by configuration settings that are in child directories if explicitly allowed by a parent element of the current configuration element or group. Permission to override is specified by using the <see langword="OverrideMode" /> attribute.</summary>
		// Token: 0x0400016B RID: 363
		Inherit = 0
	}
}

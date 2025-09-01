using System;

namespace System.Net.NetworkInformation
{
	/// <summary>The scope level for an IPv6 address.</summary>
	// Token: 0x020006FF RID: 1791
	public enum ScopeLevel
	{
		/// <summary>The scope level is not specified.</summary>
		// Token: 0x040021B7 RID: 8631
		None,
		/// <summary>The scope is interface-level.</summary>
		// Token: 0x040021B8 RID: 8632
		Interface,
		/// <summary>The scope is link-level.</summary>
		// Token: 0x040021B9 RID: 8633
		Link,
		/// <summary>The scope is subnet-level.</summary>
		// Token: 0x040021BA RID: 8634
		Subnet,
		/// <summary>The scope is admin-level.</summary>
		// Token: 0x040021BB RID: 8635
		Admin,
		/// <summary>The scope is site-level.</summary>
		// Token: 0x040021BC RID: 8636
		Site,
		/// <summary>The scope is organization-level.</summary>
		// Token: 0x040021BD RID: 8637
		Organization = 8,
		/// <summary>The scope is global.</summary>
		// Token: 0x040021BE RID: 8638
		Global = 14
	}
}

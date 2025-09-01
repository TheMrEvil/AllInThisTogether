using System;

namespace System.ComponentModel
{
	/// <summary>Defines identifiers that indicate the type of a refresh of the Properties window.</summary>
	// Token: 0x02000436 RID: 1078
	public enum RefreshProperties
	{
		/// <summary>No refresh is necessary.</summary>
		// Token: 0x0400109F RID: 4255
		None,
		/// <summary>The properties should be requeried and the view should be refreshed.</summary>
		// Token: 0x040010A0 RID: 4256
		All,
		/// <summary>The view should be refreshed.</summary>
		// Token: 0x040010A1 RID: 4257
		Repaint
	}
}

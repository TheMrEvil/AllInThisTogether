using System;
using System.Runtime.InteropServices;

namespace System.EnterpriseServices
{
	/// <summary>Indicates whether to create a new context based on the current context or on the information in <see cref="T:System.EnterpriseServices.ServiceConfig" />.</summary>
	// Token: 0x0200002F RID: 47
	[ComVisible(false)]
	[Serializable]
	public enum InheritanceOption
	{
		/// <summary>The new context is created from the existing context. <see cref="F:System.EnterpriseServices.InheritanceOption.Inherit" /> is the default value for <see cref="P:System.EnterpriseServices.ServiceConfig.Inheritance" />.</summary>
		// Token: 0x0400005D RID: 93
		Inherit,
		/// <summary>The new context is created from the default context.</summary>
		// Token: 0x0400005E RID: 94
		Ignore
	}
}

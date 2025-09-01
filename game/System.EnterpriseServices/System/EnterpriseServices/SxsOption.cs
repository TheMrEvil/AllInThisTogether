using System;
using System.Runtime.InteropServices;

namespace System.EnterpriseServices
{
	/// <summary>Indicates how side-by-side assemblies are configured for <see cref="T:System.EnterpriseServices.ServiceConfig" />.</summary>
	// Token: 0x0200004D RID: 77
	[ComVisible(false)]
	[Serializable]
	public enum SxsOption
	{
		/// <summary>Side-by-side assemblies are not used within the enclosed context. <see cref="F:System.EnterpriseServices.SxsOption.Ignore" /> is the default setting for <see cref="P:System.EnterpriseServices.ServiceConfig.SxsOption" /> when <see cref="P:System.EnterpriseServices.ServiceConfig.Inheritance" /> is set to <see cref="F:System.EnterpriseServices.InheritanceOption.Ignore" />.</summary>
		// Token: 0x04000089 RID: 137
		Ignore,
		/// <summary>The current side-by-side assembly of the enclosed context is used. <see cref="F:System.EnterpriseServices.SxsOption.Inherit" /> is the default setting for <see cref="P:System.EnterpriseServices.ServiceConfig.SxsOption" /> when <see cref="P:System.EnterpriseServices.ServiceConfig.Inheritance" /> is set to <see cref="F:System.EnterpriseServices.InheritanceOption.Inherit" />.</summary>
		// Token: 0x0400008A RID: 138
		Inherit,
		/// <summary>A new side-by-side assembly is created for the enclosed context.</summary>
		// Token: 0x0400008B RID: 139
		New
	}
}

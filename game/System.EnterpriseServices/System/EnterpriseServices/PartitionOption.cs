using System;
using System.Runtime.InteropServices;

namespace System.EnterpriseServices
{
	/// <summary>Indicates the context in which to run the COM+ partition.</summary>
	// Token: 0x02000036 RID: 54
	[ComVisible(false)]
	[Serializable]
	public enum PartitionOption
	{
		/// <summary>The enclosed context runs in the Global Partition. <see cref="F:System.EnterpriseServices.PartitionOption.Ignore" /> is the default setting for <see cref="P:System.EnterpriseServices.ServiceConfig.PartitionOption" /> when <see cref="P:System.EnterpriseServices.ServiceConfig.Inheritance" /> is set to <see cref="F:System.EnterpriseServices.InheritanceOption.Ignore" />.</summary>
		// Token: 0x04000074 RID: 116
		Ignore,
		/// <summary>The enclosed context runs in the current containing COM+ partition. This is the default setting for <see cref="P:System.EnterpriseServices.ServiceConfig.PartitionOption" /> when <see cref="P:System.EnterpriseServices.ServiceConfig.Inheritance" /> is set to <see cref="F:System.EnterpriseServices.InheritanceOption.Inherit" />.</summary>
		// Token: 0x04000075 RID: 117
		Inherit,
		/// <summary>The enclosed context runs in a COM+ partition that is different from the current containing partition.</summary>
		// Token: 0x04000076 RID: 118
		New
	}
}

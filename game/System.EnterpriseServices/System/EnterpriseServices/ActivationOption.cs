using System;

namespace System.EnterpriseServices
{
	/// <summary>Specifies the manner in which serviced components are activated in the application.</summary>
	// Token: 0x0200000A RID: 10
	[Serializable]
	public enum ActivationOption
	{
		/// <summary>Specifies that serviced components in the marked application are activated in the creator's process.</summary>
		// Token: 0x0400002F RID: 47
		Library,
		/// <summary>Specifies that serviced components in the marked application are activated in a system-provided process.</summary>
		// Token: 0x04000030 RID: 48
		Server
	}
}

using System;
using System.Runtime.InteropServices;

namespace System
{
	/// <summary>Specifies the action that a custom application domain manager takes when initializing a new domain.</summary>
	// Token: 0x02000235 RID: 565
	[ComVisible(true)]
	[Flags]
	public enum AppDomainManagerInitializationOptions
	{
		/// <summary>No initialization action.</summary>
		// Token: 0x04001715 RID: 5909
		None = 0,
		/// <summary>Register the COM callable wrapper for the current <see cref="T:System.AppDomainManager" /> with the unmanaged host.</summary>
		// Token: 0x04001716 RID: 5910
		RegisterWithHost = 1
	}
}

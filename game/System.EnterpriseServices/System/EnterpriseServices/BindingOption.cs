using System;
using System.Runtime.InteropServices;

namespace System.EnterpriseServices
{
	/// <summary>Indicates whether all work submitted by <see cref="T:System.EnterpriseServices.Activity" /> should be bound to only one single-threaded apartment (STA). This enumeration has no impact on the multithreaded apartment (MTA).</summary>
	// Token: 0x02000015 RID: 21
	[ComVisible(false)]
	[Serializable]
	public enum BindingOption
	{
		/// <summary>The work submitted by the activity is not bound to a single STA.</summary>
		// Token: 0x04000048 RID: 72
		NoBinding,
		/// <summary>The work submitted by the activity is bound to a single STA.</summary>
		// Token: 0x04000049 RID: 73
		BindingToPoolThread
	}
}

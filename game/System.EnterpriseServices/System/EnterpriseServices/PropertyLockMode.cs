using System;
using System.Runtime.InteropServices;

namespace System.EnterpriseServices
{
	/// <summary>Specifies the mode for accessing shared properties in the shared property group manager.</summary>
	// Token: 0x02000038 RID: 56
	[ComVisible(false)]
	[Serializable]
	public enum PropertyLockMode
	{
		/// <summary>Locks all the properties in the shared property group for exclusive use by the caller, as long as the caller's current method is executing.</summary>
		// Token: 0x04000078 RID: 120
		Method = 1,
		/// <summary>Locks a property during a get or set, assuring that every get or set operation on a shared property is atomic.</summary>
		// Token: 0x04000079 RID: 121
		SetGet = 0
	}
}

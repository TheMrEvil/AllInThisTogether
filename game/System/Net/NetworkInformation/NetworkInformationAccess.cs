using System;

namespace System.Net.NetworkInformation
{
	/// <summary>Specifies permission to access information about network interfaces and traffic statistics.</summary>
	// Token: 0x020006F5 RID: 1781
	[Flags]
	public enum NetworkInformationAccess
	{
		/// <summary>No access to network information.</summary>
		// Token: 0x04002192 RID: 8594
		None = 0,
		/// <summary>Read access to network information.</summary>
		// Token: 0x04002193 RID: 8595
		Read = 1,
		/// <summary>Ping access to network information.</summary>
		// Token: 0x04002194 RID: 8596
		Ping = 4
	}
}

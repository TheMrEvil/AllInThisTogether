using System;

namespace System.Security.Authentication.ExtendedProtection
{
	/// <summary>The <see cref="T:System.Security.Authentication.ExtendedProtection.ProtectionScenario" /> enumeration specifies the protection scenario enforced by the policy.</summary>
	// Token: 0x020002A9 RID: 681
	public enum ProtectionScenario
	{
		/// <summary>The transport will select between a secure and standard protection scenario depending on the type of channel used. For secure protection, integrated Windows authentication is wrapped in a secure channel and has an exactly matching channel binding token with no Service Provider Name (SPN) validation. For standard protection, integrated Windows authentication is optionally wrapped in a secure channel with an optional channel binding token and SPN validation is required. So if the request comes through a secure channel, the channel binding token (CBT) is checked, otherwise the SPN is checked.</summary>
		// Token: 0x04000BFE RID: 3070
		TransportSelected,
		/// <summary>Integrated Windows authentication is wrapped in a secure channel terminated by a trusted proxy and has a channel binding token with SPN validation required. This requires the presence of a CBT, but the CBT is not checked while the SPN is checked.</summary>
		// Token: 0x04000BFF RID: 3071
		TrustedProxy
	}
}

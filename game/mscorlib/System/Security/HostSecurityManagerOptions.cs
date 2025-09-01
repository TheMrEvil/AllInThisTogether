using System;
using System.Runtime.InteropServices;

namespace System.Security
{
	/// <summary>Specifies the security policy components to be used by the host security manager.</summary>
	// Token: 0x020003E0 RID: 992
	[Flags]
	[ComVisible(true)]
	[Serializable]
	public enum HostSecurityManagerOptions
	{
		/// <summary>Use none of the security policy components.</summary>
		// Token: 0x04001EB8 RID: 7864
		None = 0,
		/// <summary>Use the application domain evidence.</summary>
		// Token: 0x04001EB9 RID: 7865
		HostAppDomainEvidence = 1,
		/// <summary>Use the policy level specified in the <see cref="P:System.Security.HostSecurityManager.DomainPolicy" /> property.</summary>
		// Token: 0x04001EBA RID: 7866
		HostPolicyLevel = 2,
		/// <summary>Use the assembly evidence.</summary>
		// Token: 0x04001EBB RID: 7867
		HostAssemblyEvidence = 4,
		/// <summary>Route calls to the <see cref="M:System.Security.Policy.ApplicationSecurityManager.DetermineApplicationTrust(System.ActivationContext,System.Security.Policy.TrustManagerContext)" /> method to the <see cref="M:System.Security.HostSecurityManager.DetermineApplicationTrust(System.Security.Policy.Evidence,System.Security.Policy.Evidence,System.Security.Policy.TrustManagerContext)" /> method first.</summary>
		// Token: 0x04001EBC RID: 7868
		HostDetermineApplicationTrust = 8,
		/// <summary>Use the <see cref="M:System.Security.HostSecurityManager.ResolvePolicy(System.Security.Policy.Evidence)" /> method to resolve the application evidence.</summary>
		// Token: 0x04001EBD RID: 7869
		HostResolvePolicy = 16,
		/// <summary>Use all security policy components.</summary>
		// Token: 0x04001EBE RID: 7870
		AllFlags = 31
	}
}

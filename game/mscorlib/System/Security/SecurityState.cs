using System;

namespace System.Security
{
	/// <summary>Provides a base class for requesting the security status of an action from the <see cref="T:System.AppDomainManager" /> object.</summary>
	// Token: 0x020003EC RID: 1004
	public abstract class SecurityState
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.SecurityState" /> class.</summary>
		// Token: 0x0600297B RID: 10619 RVA: 0x0000259F File Offset: 0x0000079F
		protected SecurityState()
		{
		}

		/// <summary>When overridden in a derived class, ensures that the state that is represented by <see cref="T:System.Security.SecurityState" /> is available on the host.</summary>
		// Token: 0x0600297C RID: 10620
		public abstract void EnsureState();

		/// <summary>Gets a value that indicates whether the state for this implementation of the <see cref="T:System.Security.SecurityState" /> class is available on the current host.</summary>
		/// <returns>
		///   <see langword="true" /> if the state is available; otherwise, <see langword="false" />.</returns>
		// Token: 0x0600297D RID: 10621 RVA: 0x00096628 File Offset: 0x00094828
		public bool IsStateAvailable()
		{
			AppDomainManager domainManager = AppDomain.CurrentDomain.DomainManager;
			return domainManager != null && domainManager.CheckSecuritySettings(this);
		}
	}
}

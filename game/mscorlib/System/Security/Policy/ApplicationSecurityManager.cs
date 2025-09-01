using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.Security.Policy
{
	/// <summary>Manages trust decisions for manifest-activated applications.</summary>
	// Token: 0x02000402 RID: 1026
	[ComVisible(true)]
	public static class ApplicationSecurityManager
	{
		/// <summary>Gets the current application trust manager.</summary>
		/// <returns>An <see cref="T:System.Security.Policy.IApplicationTrustManager" /> that represents the current trust manager.</returns>
		/// <exception cref="T:System.Security.Policy.PolicyException">The policy on this application does not have a trust manager.</exception>
		// Token: 0x17000524 RID: 1316
		// (get) Token: 0x060029ED RID: 10733 RVA: 0x00098191 File Offset: 0x00096391
		public static IApplicationTrustManager ApplicationTrustManager
		{
			[SecurityPermission(SecurityAction.Demand, ControlPolicy = true)]
			get
			{
				if (ApplicationSecurityManager._appTrustManager == null)
				{
					ApplicationSecurityManager._appTrustManager = new MonoTrustManager();
				}
				return ApplicationSecurityManager._appTrustManager;
			}
		}

		/// <summary>Gets an application trust collection that contains the cached trust decisions for the user.</summary>
		/// <returns>An <see cref="T:System.Security.Policy.ApplicationTrustCollection" /> that contains the cached trust decisions for the user.</returns>
		// Token: 0x17000525 RID: 1317
		// (get) Token: 0x060029EE RID: 10734 RVA: 0x000981A9 File Offset: 0x000963A9
		public static ApplicationTrustCollection UserApplicationTrusts
		{
			get
			{
				if (ApplicationSecurityManager._userAppTrusts == null)
				{
					ApplicationSecurityManager._userAppTrusts = new ApplicationTrustCollection();
				}
				return ApplicationSecurityManager._userAppTrusts;
			}
		}

		/// <summary>Determines whether the user approves the specified application to execute with the requested permission set.</summary>
		/// <param name="activationContext">An <see cref="T:System.ActivationContext" /> identifying the activation context for the application.</param>
		/// <param name="context">A <see cref="T:System.Security.Policy.TrustManagerContext" /> identifying the trust manager context for the application.</param>
		/// <returns>
		///   <see langword="true" /> to execute the specified application; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="activationContext" /> parameter is <see langword="null" />.</exception>
		// Token: 0x060029EF RID: 10735 RVA: 0x000981C1 File Offset: 0x000963C1
		[MonoTODO("Missing application manifest support")]
		[SecurityPermission(SecurityAction.Demand, ControlPolicy = true, ControlEvidence = true)]
		public static bool DetermineApplicationTrust(ActivationContext activationContext, TrustManagerContext context)
		{
			if (activationContext == null)
			{
				throw new NullReferenceException("activationContext");
			}
			return ApplicationSecurityManager.ApplicationTrustManager.DetermineApplicationTrust(activationContext, context).IsApplicationTrustedToRun;
		}

		// Token: 0x04001F59 RID: 8025
		private static IApplicationTrustManager _appTrustManager;

		// Token: 0x04001F5A RID: 8026
		private static ApplicationTrustCollection _userAppTrusts;
	}
}

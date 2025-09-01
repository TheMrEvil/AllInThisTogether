using System;
using System.Reflection;
using System.Runtime.Hosting;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using System.Security.Policy;
using System.Threading;

namespace System
{
	/// <summary>Provides a managed equivalent of an unmanaged host.</summary>
	// Token: 0x02000226 RID: 550
	[ComVisible(true)]
	[SecurityPermission(SecurityAction.InheritanceDemand, Infrastructure = true)]
	[SecurityPermission(SecurityAction.LinkDemand, Infrastructure = true)]
	public class AppDomainManager : MarshalByRefObject
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.AppDomainManager" /> class.</summary>
		// Token: 0x06001881 RID: 6273 RVA: 0x0005D910 File Offset: 0x0005BB10
		public AppDomainManager()
		{
			this._flags = AppDomainManagerInitializationOptions.None;
		}

		/// <summary>Gets the application activator that handles the activation of add-ins and manifest-based applications for the domain.</summary>
		/// <returns>The application activator.</returns>
		// Token: 0x17000285 RID: 645
		// (get) Token: 0x06001882 RID: 6274 RVA: 0x0005D91F File Offset: 0x0005BB1F
		public virtual ApplicationActivator ApplicationActivator
		{
			get
			{
				if (this._activator == null)
				{
					this._activator = new ApplicationActivator();
				}
				return this._activator;
			}
		}

		/// <summary>Gets the entry assembly for an application.</summary>
		/// <returns>The entry assembly for the application.</returns>
		// Token: 0x17000286 RID: 646
		// (get) Token: 0x06001883 RID: 6275 RVA: 0x0005D93A File Offset: 0x0005BB3A
		public virtual Assembly EntryAssembly
		{
			get
			{
				return Assembly.GetEntryAssembly();
			}
		}

		/// <summary>Gets the host execution context manager that manages the flow of the execution context.</summary>
		/// <returns>The host execution context manager.</returns>
		// Token: 0x17000287 RID: 647
		// (get) Token: 0x06001884 RID: 6276 RVA: 0x000479F8 File Offset: 0x00045BF8
		[MonoTODO]
		public virtual HostExecutionContextManager HostExecutionContextManager
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets the host security manager that participates in security decisions for the application domain.</summary>
		/// <returns>The host security manager.</returns>
		// Token: 0x17000288 RID: 648
		// (get) Token: 0x06001885 RID: 6277 RVA: 0x0000AF5E File Offset: 0x0000915E
		public virtual HostSecurityManager HostSecurityManager
		{
			get
			{
				return null;
			}
		}

		/// <summary>Gets the initialization flags for custom application domain managers.</summary>
		/// <returns>A bitwise combination of the enumeration values that describe the initialization action to perform. The default is <see cref="F:System.AppDomainManagerInitializationOptions.None" />.</returns>
		// Token: 0x17000289 RID: 649
		// (get) Token: 0x06001886 RID: 6278 RVA: 0x0005D941 File Offset: 0x0005BB41
		// (set) Token: 0x06001887 RID: 6279 RVA: 0x0005D949 File Offset: 0x0005BB49
		public AppDomainManagerInitializationOptions InitializationFlags
		{
			get
			{
				return this._flags;
			}
			set
			{
				this._flags = value;
			}
		}

		/// <summary>Returns a new or existing application domain.</summary>
		/// <param name="friendlyName">The friendly name of the domain.</param>
		/// <param name="securityInfo">An object that contains evidence mapped through the security policy to establish a top-of-stack permission set.</param>
		/// <param name="appDomainInfo">An object that contains application domain initialization information.</param>
		/// <returns>A new or existing application domain.</returns>
		// Token: 0x06001888 RID: 6280 RVA: 0x0005D954 File Offset: 0x0005BB54
		public virtual AppDomain CreateDomain(string friendlyName, Evidence securityInfo, AppDomainSetup appDomainInfo)
		{
			this.InitializeNewDomain(appDomainInfo);
			AppDomain appDomain = AppDomainManager.CreateDomainHelper(friendlyName, securityInfo, appDomainInfo);
			if ((this.HostSecurityManager.Flags & HostSecurityManagerOptions.HostPolicyLevel) == HostSecurityManagerOptions.HostPolicyLevel)
			{
				PolicyLevel domainPolicy = this.HostSecurityManager.DomainPolicy;
				if (domainPolicy != null)
				{
					appDomain.SetAppDomainPolicy(domainPolicy);
				}
			}
			return appDomain;
		}

		/// <summary>Initializes the new application domain.</summary>
		/// <param name="appDomainInfo">An object that contains application domain initialization information.</param>
		// Token: 0x06001889 RID: 6281 RVA: 0x00004BF9 File Offset: 0x00002DF9
		public virtual void InitializeNewDomain(AppDomainSetup appDomainInfo)
		{
		}

		/// <summary>Indicates whether the specified operation is allowed in the application domain.</summary>
		/// <param name="state">A subclass of <see cref="T:System.Security.SecurityState" /> that identifies the operation whose security status is requested.</param>
		/// <returns>
		///   <see langword="true" /> if the host allows the operation specified by <paramref name="state" /> to be performed in the application domain; otherwise, <see langword="false" />.</returns>
		// Token: 0x0600188A RID: 6282 RVA: 0x0000DDA9 File Offset: 0x0000BFA9
		public virtual bool CheckSecuritySettings(SecurityState state)
		{
			return false;
		}

		/// <summary>Provides a helper method to create an application domain.</summary>
		/// <param name="friendlyName">The friendly name of the domain.</param>
		/// <param name="securityInfo">An object that contains evidence mapped through the security policy to establish a top-of-stack permission set.</param>
		/// <param name="appDomainInfo">An object that contains application domain initialization information.</param>
		/// <returns>A newly created application domain.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="friendlyName" /> is <see langword="null" />.</exception>
		// Token: 0x0600188B RID: 6283 RVA: 0x0005D998 File Offset: 0x0005BB98
		protected static AppDomain CreateDomainHelper(string friendlyName, Evidence securityInfo, AppDomainSetup appDomainInfo)
		{
			return AppDomain.CreateDomain(friendlyName, securityInfo, appDomainInfo);
		}

		// Token: 0x040016B9 RID: 5817
		private ApplicationActivator _activator;

		// Token: 0x040016BA RID: 5818
		private AppDomainManagerInitializationOptions _flags;
	}
}

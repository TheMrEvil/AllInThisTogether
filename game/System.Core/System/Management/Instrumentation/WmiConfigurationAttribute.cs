using System;
using System.Security.Permissions;
using Unity;

namespace System.Management.Instrumentation
{
	/// <summary>The WmiConfiguration attribute indicates that an assembly contains code that implements a WMI provider by using the WMI.NET Provider Extensions model. The attribute accepts parameters that establish the high-level configuration of the implemented WMI provider. Note: the WMI .NET libraries are now considered in final state, and no further development, enhancements, or updates will be available for non-security related issues affecting these libraries. The MI APIs should be used for all new development.</summary>
	// Token: 0x02000389 RID: 905
	[AttributeUsage(AttributeTargets.Assembly)]
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public sealed class WmiConfigurationAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Management.WmiConfigurationAttribute" /> class that specifies the WMI namespace in which the WMI provider will expose classes.</summary>
		/// <param name="scope">The WMI namespace in which the provider will expose classes. For example, "root\MyProviderNamespace".</param>
		// Token: 0x06001B22 RID: 6946 RVA: 0x00003A59 File Offset: 0x00001C59
		public WmiConfigurationAttribute(string scope)
		{
		}

		/// <summary>Gets or sets the hosting group for the WMI provider.</summary>
		/// <returns>A <see cref="T:System.String" /> value that indicates the hosting group for the WMI provider.</returns>
		// Token: 0x170004C0 RID: 1216
		// (get) Token: 0x06001B23 RID: 6947 RVA: 0x0005A05A File Offset: 0x0005825A
		// (set) Token: 0x06001B24 RID: 6948 RVA: 0x00003A59 File Offset: 0x00001C59
		public string HostingGroup
		{
			get
			{
				ThrowStub.ThrowNotSupportedException();
				return null;
			}
			set
			{
			}
		}

		/// <summary>Gets or sets the hosting model for the WMI provider.</summary>
		/// <returns>A <see cref="T:System.Management.Instrumentation.ManagementHostingModel" /> value that indicates the hosting model of the WMI provider.</returns>
		// Token: 0x170004C1 RID: 1217
		// (get) Token: 0x06001B25 RID: 6949 RVA: 0x0005A25C File Offset: 0x0005845C
		// (set) Token: 0x06001B26 RID: 6950 RVA: 0x00003A59 File Offset: 0x00001C59
		public ManagementHostingModel HostingModel
		{
			get
			{
				ThrowStub.ThrowNotSupportedException();
				return ManagementHostingModel.Decoupled;
			}
			set
			{
			}
		}

		/// <summary>Gets or sets a value that specifies whether the WMI provider can impersonate its callers. If the value is false, the provider cannot impersonate, and if the value is true, the provider can impersonate.</summary>
		/// <returns>A Boolean value that indicates whether a provider can or cannot impersonate its callers. If the value is false, the provider cannot impersonate, and if the value is true, the provider can impersonate.</returns>
		// Token: 0x170004C2 RID: 1218
		// (get) Token: 0x06001B27 RID: 6951 RVA: 0x0005A278 File Offset: 0x00058478
		// (set) Token: 0x06001B28 RID: 6952 RVA: 0x00003A59 File Offset: 0x00001C59
		public bool IdentifyLevel
		{
			get
			{
				ThrowStub.ThrowNotSupportedException();
				return default(bool);
			}
			set
			{
			}
		}

		/// <summary>Gets or sets a Security Descriptor Definition Language (SDDL) string that specifies the security descriptor on the namespace in which the provider exposes management objects.</summary>
		/// <returns>An SDDL string that represents the security descriptor on the namespace in which the provider exposes management objects.</returns>
		// Token: 0x170004C3 RID: 1219
		// (get) Token: 0x06001B29 RID: 6953 RVA: 0x0005A05A File Offset: 0x0005825A
		// (set) Token: 0x06001B2A RID: 6954 RVA: 0x00003A59 File Offset: 0x00001C59
		public string NamespaceSecurity
		{
			get
			{
				ThrowStub.ThrowNotSupportedException();
				return null;
			}
			set
			{
			}
		}

		/// <summary>Gets or sets the WMI namespace in which the WMI provider exposes classes.</summary>
		/// <returns>A <see cref="T:System.String" /> value that indicates the namespace in which the WMI provider exposes classes.</returns>
		// Token: 0x170004C4 RID: 1220
		// (get) Token: 0x06001B2B RID: 6955 RVA: 0x0005A05A File Offset: 0x0005825A
		public string Scope
		{
			get
			{
				ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets or sets a security descriptor for the WMI provider. For more information, see the SecurityDescriptor property information in the "__Win32Provider" topic in the MSDN online library at http://www.msdn.com. </summary>
		/// <returns>A <see cref="T:System.String" /> value that contains the security descriptor for the WMI provider.</returns>
		// Token: 0x170004C5 RID: 1221
		// (get) Token: 0x06001B2C RID: 6956 RVA: 0x0005A05A File Offset: 0x0005825A
		// (set) Token: 0x06001B2D RID: 6957 RVA: 0x00003A59 File Offset: 0x00001C59
		public string SecurityRestriction
		{
			get
			{
				ThrowStub.ThrowNotSupportedException();
				return null;
			}
			set
			{
			}
		}
	}
}

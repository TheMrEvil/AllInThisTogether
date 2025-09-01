using System;
using System.Security.Permissions;
using Unity;

namespace System.Management.Instrumentation
{
	/// <summary>The ManagementConfiguration attribute indicates that a property or field represents a read-write WMI property.Note: the WMI .NET libraries are now considered in final state, and no further development, enhancements, or updates will be available for non-security related issues affecting these libraries. The MI APIs should be used for all new development.</summary>
	// Token: 0x0200037D RID: 893
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public sealed class ManagementConfigurationAttribute : ManagementMemberAttribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Management.ManagementConfigurationAttribute" /> class. This is the default constructor.</summary>
		// Token: 0x06001B03 RID: 6915 RVA: 0x00003A59 File Offset: 0x00001C59
		public ManagementConfigurationAttribute()
		{
		}

		/// <summary>Gets or sets the mode of the property, which specifies whether changes to it are applied as soon as possible or when a commit method is called.</summary>
		/// <returns>Returns a <see cref="T:System.Management.Instrumentation.ManagementConfigurationType" /> that indicates whether the WMI property uses <see cref="F:System.Management.Instrumentation.ManagementConfigurationType.Apply" /> or <see cref="F:System.Management.Instrumentation.ManagementConfigurationType.OnCommit" /> mode.</returns>
		// Token: 0x170004B5 RID: 1205
		// (get) Token: 0x06001B04 RID: 6916 RVA: 0x0005A208 File Offset: 0x00058408
		// (set) Token: 0x06001B05 RID: 6917 RVA: 0x00003A59 File Offset: 0x00001C59
		public ManagementConfigurationType Mode
		{
			get
			{
				ThrowStub.ThrowNotSupportedException();
				return ManagementConfigurationType.Apply;
			}
			set
			{
			}
		}

		/// <summary>Gets or sets a value that defines the type of output that the property that is marked with the ManagementConfiguration attribute will return.</summary>
		/// <returns>A <see cref="T:System.Type" /> value representing the type of output that the property marked with the ManagementConfiguration attribute will return.</returns>
		// Token: 0x170004B6 RID: 1206
		// (get) Token: 0x06001B06 RID: 6918 RVA: 0x0005A05A File Offset: 0x0005825A
		// (set) Token: 0x06001B07 RID: 6919 RVA: 0x00003A59 File Offset: 0x00001C59
		public Type Schema
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

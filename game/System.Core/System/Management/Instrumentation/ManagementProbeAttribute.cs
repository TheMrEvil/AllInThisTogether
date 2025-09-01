using System;
using System.Security.Permissions;
using Unity;

namespace System.Management.Instrumentation
{
	/// <summary>The ManagementProbe attribute indicates that a property or field represents a read-only WMI property.Note: the WMI .NET libraries are now considered in final state, and no further development, enhancements, or updates will be available for non-security related issues affecting these libraries. The MI APIs should be used for all new development.</summary>
	// Token: 0x02000385 RID: 901
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public sealed class ManagementProbeAttribute : ManagementMemberAttribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Management.ManagementProbeAttribute" /> class. This is the default constructor for the class.</summary>
		// Token: 0x06001B16 RID: 6934 RVA: 0x00003A59 File Offset: 0x00001C59
		public ManagementProbeAttribute()
		{
		}

		/// <summary>Gets or sets a value that defines the type of output that the property that is marked with the ManagementProbe attribute will output.</summary>
		/// <returns>A <see cref="T:System.Type" /> value that indicates the type of output that the property that is marked with the ManagementProbe attribute will output.</returns>
		// Token: 0x170004BC RID: 1212
		// (get) Token: 0x06001B17 RID: 6935 RVA: 0x0005A05A File Offset: 0x0005825A
		// (set) Token: 0x06001B18 RID: 6936 RVA: 0x00003A59 File Offset: 0x00001C59
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

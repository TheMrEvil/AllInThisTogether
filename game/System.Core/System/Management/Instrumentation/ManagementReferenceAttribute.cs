using System;
using System.Security.Permissions;
using Unity;

namespace System.Management.Instrumentation
{
	/// <summary>The ManagementReferenceAttribute marks a class member, property or method parameter as a reference to another management object or class.Note: the WMI .NET libraries are now considered in final state, and no further development, enhancements, or updates will be available for non-security related issues affecting these libraries. The MI APIs should be used for all new development.</summary>
	// Token: 0x02000386 RID: 902
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public sealed class ManagementReferenceAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Management.ManagementReferenceAttribute" /> class. This is the default constructor.</summary>
		// Token: 0x06001B19 RID: 6937 RVA: 0x00003A59 File Offset: 0x00001C59
		public ManagementReferenceAttribute()
		{
		}

		/// <summary>Gets or sets the name of the referenced type.</summary>
		/// <returns>A string containing the name of the referenced type.</returns>
		// Token: 0x170004BD RID: 1213
		// (get) Token: 0x06001B1A RID: 6938 RVA: 0x0005A05A File Offset: 0x0005825A
		// (set) Token: 0x06001B1B RID: 6939 RVA: 0x00003A59 File Offset: 0x00001C59
		public string Type
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

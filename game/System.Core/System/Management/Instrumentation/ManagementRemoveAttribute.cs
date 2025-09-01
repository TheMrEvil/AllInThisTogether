using System;
using System.Security.Permissions;
using Unity;

namespace System.Management.Instrumentation
{
	/// <summary>The ManagementRemoveAttribute is used to indicate that a method cleans up an instance of a managed entity.Note: the WMI .NET libraries are now considered in final state, and no further development, enhancements, or updates will be available for non-security related issues affecting these libraries. The MI APIs should be used for all new development.</summary>
	// Token: 0x02000387 RID: 903
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public sealed class ManagementRemoveAttribute : ManagementMemberAttribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Management.ManagementRemoveAttribute" /> class. This is the default constructor.</summary>
		// Token: 0x06001B1C RID: 6940 RVA: 0x00003A59 File Offset: 0x00001C59
		public ManagementRemoveAttribute()
		{
		}

		/// <summary>Gets or sets a value that defines the type of output that the object that is marked with the ManagementRemove attribute will output.</summary>
		/// <returns>A <see cref="T:System.Type" /> value that indicates the type of output that the object marked with the Remove attribute will output.</returns>
		// Token: 0x170004BE RID: 1214
		// (get) Token: 0x06001B1D RID: 6941 RVA: 0x0005A05A File Offset: 0x0005825A
		// (set) Token: 0x06001B1E RID: 6942 RVA: 0x00003A59 File Offset: 0x00001C59
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

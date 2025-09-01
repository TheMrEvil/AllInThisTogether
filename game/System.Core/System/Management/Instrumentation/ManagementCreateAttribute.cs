using System;
using System.Security.Permissions;

namespace System.Management.Instrumentation
{
	/// <summary>The ManagementCreateAttribute is used to indicate that a method creates a new instance of a managed entity.Note: the WMI .NET libraries are now considered in final state, and no further development, enhancements, or updates will be available for non-security related issues affecting these libraries. The MI APIs should be used for all new development.</summary>
	// Token: 0x0200037F RID: 895
	[AttributeUsage(AttributeTargets.Constructor | AttributeTargets.Method, AllowMultiple = false)]
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public sealed class ManagementCreateAttribute : ManagementNewInstanceAttribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Management.ManagementCreateAttribute" /> class. This is the default constructor.</summary>
		// Token: 0x06001B08 RID: 6920 RVA: 0x00003A59 File Offset: 0x00001C59
		public ManagementCreateAttribute()
		{
		}
	}
}

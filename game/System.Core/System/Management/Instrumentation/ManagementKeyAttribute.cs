using System;
using System.Security.Permissions;

namespace System.Management.Instrumentation
{
	/// <summary>The ManagementKey attribute identifies the key properties of a WMI class.Note: the WMI .NET libraries are now considered in final state, and no further development, enhancements, or updates will be available for non-security related issues affecting these libraries. The MI APIs should be used for all new development.</summary>
	// Token: 0x02000383 RID: 899
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public sealed class ManagementKeyAttribute : ManagementMemberAttribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Management.ManagementKeyAttribute" />  class. This is the default constructor.</summary>
		// Token: 0x06001B13 RID: 6931 RVA: 0x00003A59 File Offset: 0x00001C59
		public ManagementKeyAttribute()
		{
		}
	}
}

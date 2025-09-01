using System;
using System.Security.Permissions;

namespace System.Management.Instrumentation
{
	/// <summary>The base class for management attributes that have only run-time functionality and no schema representation. The management attribute classes ManagementBindAttribute, ManagementCreateAttribute and ManagementEnumeratorAttribute are all derived from this class.Note: the WMI .NET libraries are now considered in final state, and no further development, enhancements, or updates will be available for non-security related issues affecting these libraries. The MI APIs should be used for all new development.</summary>
	// Token: 0x0200037A RID: 890
	[AttributeUsage(AttributeTargets.Constructor | AttributeTargets.Method, AllowMultiple = false)]
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public abstract class ManagementNewInstanceAttribute : ManagementMemberAttribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Management.ManagementNewInstanceAttribute" /> class. This is the default constructor.</summary>
		// Token: 0x06001AFE RID: 6910 RVA: 0x00003A59 File Offset: 0x00001C59
		protected ManagementNewInstanceAttribute()
		{
		}
	}
}

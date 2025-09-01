using System;
using System.Security.Permissions;
using Unity;

namespace System.Management.Instrumentation
{
	/// <summary>The ManagementBind attribute indicates that a method is used to return the instance of a WMI class associated with a specific key value.Note: the WMI .NET libraries are now considered in final state, and no further development, enhancements, or updates will be available for non-security related issues affecting these libraries. The MI APIs should be used for all new development.</summary>
	// Token: 0x02000379 RID: 889
	[AttributeUsage(AttributeTargets.Constructor | AttributeTargets.Method, AllowMultiple = false)]
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public sealed class ManagementBindAttribute : ManagementNewInstanceAttribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Management.ManagementBindAttribute" /> class. This is the default constructor.</summary>
		// Token: 0x06001AFB RID: 6907 RVA: 0x00003A59 File Offset: 0x00001C59
		public ManagementBindAttribute()
		{
		}

		/// <summary>Gets or sets a value that defines the type of output that the method that is marked with the ManagementEnumerator attribute will output.</summary>
		/// <returns>A <see cref="T:System.Type" /> value that indicates the type of output that the method marked with the <see cref="ManagementBind" /> attribute will output.</returns>
		// Token: 0x170004B3 RID: 1203
		// (get) Token: 0x06001AFC RID: 6908 RVA: 0x0005A05A File Offset: 0x0005825A
		// (set) Token: 0x06001AFD RID: 6909 RVA: 0x00003A59 File Offset: 0x00001C59
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

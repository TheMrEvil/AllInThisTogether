using System;
using System.Security.Permissions;
using Unity;

namespace System.Management.Instrumentation
{
	/// <summary>The ManagementTask attribute indicates that the target method implements a WMI method.Note: the WMI .NET libraries are now considered in final state, and no further development, enhancements, or updates will be available for non-security related issues affecting these libraries. The MI APIs should be used for all new development.</summary>
	// Token: 0x02000388 RID: 904
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public sealed class ManagementTaskAttribute : ManagementMemberAttribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Management.ManagementTaskAttribute" /> class. This is the default constructor.</summary>
		// Token: 0x06001B1F RID: 6943 RVA: 0x00003A59 File Offset: 0x00001C59
		public ManagementTaskAttribute()
		{
		}

		/// <summary>Gets or sets a value that defines the type of output that the method that is marked with the ManagementTask attribute will output.</summary>
		/// <returns>A <see cref="T:System.Type" /> value that indicates the type of output that the method that is marked with the ManagementTask attribute will output.</returns>
		// Token: 0x170004BF RID: 1215
		// (get) Token: 0x06001B20 RID: 6944 RVA: 0x0005A05A File Offset: 0x0005825A
		// (set) Token: 0x06001B21 RID: 6945 RVA: 0x00003A59 File Offset: 0x00001C59
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

using System;
using System.Security.Permissions;
using Unity;

namespace System.Management.Instrumentation
{
	/// <summary>The ManagementEnumerator attribute marks a method that returns all the instances of a WMI class.Note: the WMI .NET libraries are now considered in final state, and no further development, enhancements, or updates will be available for non-security related issues affecting these libraries. The MI APIs should be used for all new development.</summary>
	// Token: 0x02000381 RID: 897
	[AttributeUsage(AttributeTargets.Constructor | AttributeTargets.Method, AllowMultiple = false)]
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public sealed class ManagementEnumeratorAttribute : ManagementNewInstanceAttribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Management.ManagementEnumeratorAttribute" /> class.</summary>
		// Token: 0x06001B10 RID: 6928 RVA: 0x00003A59 File Offset: 0x00001C59
		public ManagementEnumeratorAttribute()
		{
		}

		/// <summary>Gets or sets a value that defines the type of output that the method that is marked with the ManagementEnumerator attribute will output.</summary>
		/// <returns>A <see cref="T:System.Type" /> value that indicates the type of output that the method marked with the <see cref="ManagementEnumerator" /> attribute will output.</returns>
		// Token: 0x170004BA RID: 1210
		// (get) Token: 0x06001B11 RID: 6929 RVA: 0x0005A05A File Offset: 0x0005825A
		// (set) Token: 0x06001B12 RID: 6930 RVA: 0x00003A59 File Offset: 0x00001C59
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

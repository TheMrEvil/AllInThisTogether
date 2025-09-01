using System;
using System.Security.Permissions;
using Unity;

namespace System.Management.Instrumentation
{
	/// <summary>The ManagementName attribute is used to override names exposed through a WMI class.Note: the WMI .NET libraries are now considered in final state, and no further development, enhancements, or updates will be available for non-security related issues affecting these libraries. The MI APIs should be used for all new development.</summary>
	// Token: 0x02000384 RID: 900
	[AttributeUsage(AttributeTargets.Parameter, AllowMultiple = false)]
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public sealed class ManagementNameAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Management.ManagementNameAttribute" /> class that specifies a value for the <see cref="P:System.Management.ManagementNameAttribute.Name" /> property of the class.</summary>
		/// <param name="name">The user-friendly name for the object.</param>
		// Token: 0x06001B14 RID: 6932 RVA: 0x00003A59 File Offset: 0x00001C59
		public ManagementNameAttribute(string name)
		{
		}

		/// <summary>Gets or sets the user-friendly name for an object. The object can be a method parameter or properties marked with the ManagementProbe, ManagementKey, or ManagementConfiguration attributes.</summary>
		/// <returns>A <see cref="T:System.String" /> value that indicates the user friendly name for an object.</returns>
		// Token: 0x170004BB RID: 1211
		// (get) Token: 0x06001B15 RID: 6933 RVA: 0x0005A05A File Offset: 0x0005825A
		public string Name
		{
			get
			{
				ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}
	}
}

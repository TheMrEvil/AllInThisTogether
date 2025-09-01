using System;
using System.Security.Permissions;
using Unity;

namespace System.Management.Instrumentation
{
	/// <summary>This class is used by the WMI.NET Provider Extensions framework. It is the base class for all the management attributes that can be applied to members.Note: the WMI .NET libraries are now considered in final state, and no further development, enhancements, or updates will be available for non-security related issues affecting these libraries. The MI APIs should be used for all new development.</summary>
	// Token: 0x0200037B RID: 891
	[AttributeUsage(AttributeTargets.All)]
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public abstract class ManagementMemberAttribute : Attribute
	{
		/// <summary>Initializes a new instance of <see cref="T:System.Management.ManagementMemberAttribute" /> the class. This is the default constructor.</summary>
		// Token: 0x06001AFF RID: 6911 RVA: 0x00003A59 File Offset: 0x00001C59
		protected ManagementMemberAttribute()
		{
		}

		/// <summary>Gets or sets the name of the management attribute.</summary>
		/// <returns>Returns a string which is the name of the management attribute.</returns>
		// Token: 0x170004B4 RID: 1204
		// (get) Token: 0x06001B00 RID: 6912 RVA: 0x0005A05A File Offset: 0x0005825A
		// (set) Token: 0x06001B01 RID: 6913 RVA: 0x00003A59 File Offset: 0x00001C59
		public string Name
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

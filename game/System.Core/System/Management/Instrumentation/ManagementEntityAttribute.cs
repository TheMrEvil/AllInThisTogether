using System;
using System.Security.Permissions;
using Unity;

namespace System.Management.Instrumentation
{
	/// <summary>The ManagementEntity attribute indicates that a class provides management information exposed through a WMI provider.Note: the WMI .NET libraries are now considered in final state, and no further development, enhancements, or updates will be available for non-security related issues affecting these libraries. The MI APIs should be used for all new development.</summary>
	// Token: 0x02000380 RID: 896
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public sealed class ManagementEntityAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Management.ManagementEntityAttribute" /> class. This is the default constructor.</summary>
		// Token: 0x06001B09 RID: 6921 RVA: 0x00003A59 File Offset: 0x00001C59
		public ManagementEntityAttribute()
		{
		}

		/// <summary>Gets or sets a value that specifies whether the class represents a WMI class in a provider implemented external to the current assembly.</summary>
		/// <returns>A boolean value that is true if the class represents an external WMI class and false otherwise.</returns>
		// Token: 0x170004B7 RID: 1207
		// (get) Token: 0x06001B0A RID: 6922 RVA: 0x0005A224 File Offset: 0x00058424
		// (set) Token: 0x06001B0B RID: 6923 RVA: 0x00003A59 File Offset: 0x00001C59
		public bool External
		{
			get
			{
				ThrowStub.ThrowNotSupportedException();
				return default(bool);
			}
			set
			{
			}
		}

		/// <summary>Gets or sets the name of the WMI class.</summary>
		/// <returns>A string that contains the name of the WMI class.</returns>
		// Token: 0x170004B8 RID: 1208
		// (get) Token: 0x06001B0C RID: 6924 RVA: 0x0005A05A File Offset: 0x0005825A
		// (set) Token: 0x06001B0D RID: 6925 RVA: 0x00003A59 File Offset: 0x00001C59
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

		/// <summary>Specifies whether the associated class represents a singleton WMI class.</summary>
		/// <returns>A boolean value that is true if the class represents a singleton WMI class and false otherwise.</returns>
		// Token: 0x170004B9 RID: 1209
		// (get) Token: 0x06001B0E RID: 6926 RVA: 0x0005A240 File Offset: 0x00058440
		// (set) Token: 0x06001B0F RID: 6927 RVA: 0x00003A59 File Offset: 0x00001C59
		public bool Singleton
		{
			get
			{
				ThrowStub.ThrowNotSupportedException();
				return default(bool);
			}
			set
			{
			}
		}
	}
}

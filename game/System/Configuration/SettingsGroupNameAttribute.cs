using System;

namespace System.Configuration
{
	/// <summary>Specifies a name for application settings property group. This class cannot be inherited.</summary>
	// Token: 0x020001C7 RID: 455
	[AttributeUsage(AttributeTargets.Class)]
	public sealed class SettingsGroupNameAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.SettingsGroupNameAttribute" /> class.</summary>
		/// <param name="groupName">A <see cref="T:System.String" /> containing the name of the application settings property group.</param>
		// Token: 0x06000BEF RID: 3055 RVA: 0x00031E2A File Offset: 0x0003002A
		public SettingsGroupNameAttribute(string groupName)
		{
			this.group_name = groupName;
		}

		/// <summary>Gets the name of the application settings property group.</summary>
		/// <returns>A <see cref="T:System.String" /> containing the name of the application settings property group.</returns>
		// Token: 0x17000202 RID: 514
		// (get) Token: 0x06000BF0 RID: 3056 RVA: 0x00031E39 File Offset: 0x00030039
		public string GroupName
		{
			get
			{
				return this.group_name;
			}
		}

		// Token: 0x04000798 RID: 1944
		private string group_name;
	}
}

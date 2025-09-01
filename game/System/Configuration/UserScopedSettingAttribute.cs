using System;

namespace System.Configuration
{
	/// <summary>Specifies that an application settings group or property contains distinct values for each user of an application. This class cannot be inherited.</summary>
	// Token: 0x020001DD RID: 477
	[AttributeUsage(AttributeTargets.Property)]
	public sealed class UserScopedSettingAttribute : SettingAttribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.UserScopedSettingAttribute" /> class.</summary>
		// Token: 0x06000C62 RID: 3170 RVA: 0x0002DD38 File Offset: 0x0002BF38
		public UserScopedSettingAttribute()
		{
		}
	}
}

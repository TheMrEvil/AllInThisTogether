using System;

namespace System.Configuration
{
	/// <summary>Specifies that an application settings property has a common value for all users of an application. This class cannot be inherited.</summary>
	// Token: 0x02000198 RID: 408
	[AttributeUsage(AttributeTargets.Property)]
	public sealed class ApplicationScopedSettingAttribute : SettingAttribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.ApplicationScopedSettingAttribute" /> class.</summary>
		// Token: 0x06000AAA RID: 2730 RVA: 0x0002DD38 File Offset: 0x0002BF38
		public ApplicationScopedSettingAttribute()
		{
		}
	}
}

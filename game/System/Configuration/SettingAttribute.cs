using System;

namespace System.Configuration
{
	/// <summary>Represents a custom settings attribute used to associate settings information with a settings property.</summary>
	// Token: 0x020001BC RID: 444
	[AttributeUsage(AttributeTargets.Property)]
	public class SettingAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.SettingAttribute" /> class.</summary>
		// Token: 0x06000BA8 RID: 2984 RVA: 0x00003D9F File Offset: 0x00001F9F
		public SettingAttribute()
		{
		}
	}
}

using System;

namespace System.Configuration
{
	/// <summary>Specifies that a settings provider should disable any logic that gets invoked when an application upgrade is detected. This class cannot be inherited.</summary>
	// Token: 0x020001BB RID: 443
	[AttributeUsage(AttributeTargets.Property)]
	public sealed class NoSettingsVersionUpgradeAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.NoSettingsVersionUpgradeAttribute" /> class.</summary>
		// Token: 0x06000BA7 RID: 2983 RVA: 0x00003D9F File Offset: 0x00001F9F
		public NoSettingsVersionUpgradeAttribute()
		{
		}
	}
}

using System;

namespace System.Configuration
{
	/// <summary>Provides a string that describes an application settings property group. This class cannot be inherited.</summary>
	// Token: 0x020001C6 RID: 454
	[AttributeUsage(AttributeTargets.Class)]
	public sealed class SettingsGroupDescriptionAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.SettingsGroupDescriptionAttribute" /> class.</summary>
		/// <param name="description">A <see cref="T:System.String" /> containing the descriptive text for the application settings group.</param>
		// Token: 0x06000BED RID: 3053 RVA: 0x00031E13 File Offset: 0x00030013
		public SettingsGroupDescriptionAttribute(string description)
		{
			this.desc = description;
		}

		/// <summary>The descriptive text for the application settings properties group.</summary>
		/// <returns>A <see cref="T:System.String" /> containing the descriptive text for the application settings group.</returns>
		// Token: 0x17000201 RID: 513
		// (get) Token: 0x06000BEE RID: 3054 RVA: 0x00031E22 File Offset: 0x00030022
		public string Description
		{
			get
			{
				return this.desc;
			}
		}

		// Token: 0x04000797 RID: 1943
		private string desc;
	}
}

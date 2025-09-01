using System;

namespace System.Configuration
{
	/// <summary>Provides a string that describes an individual configuration property. This class cannot be inherited.</summary>
	// Token: 0x020001C5 RID: 453
	[AttributeUsage(AttributeTargets.Property)]
	public sealed class SettingsDescriptionAttribute : Attribute
	{
		/// <summary>Initializes an instance of the <see cref="T:System.Configuration.SettingsDescriptionAttribute" /> class.</summary>
		/// <param name="description">The <see cref="T:System.String" /> used as descriptive text.</param>
		// Token: 0x06000BEB RID: 3051 RVA: 0x00031DFC File Offset: 0x0002FFFC
		public SettingsDescriptionAttribute(string description)
		{
			this.desc = description;
		}

		/// <summary>Gets the descriptive text for the associated configuration property.</summary>
		/// <returns>A <see cref="T:System.String" /> containing the descriptive text for the associated configuration property.</returns>
		// Token: 0x17000200 RID: 512
		// (get) Token: 0x06000BEC RID: 3052 RVA: 0x00031E0B File Offset: 0x0003000B
		public string Description
		{
			get
			{
				return this.desc;
			}
		}

		// Token: 0x04000796 RID: 1942
		private string desc;
	}
}

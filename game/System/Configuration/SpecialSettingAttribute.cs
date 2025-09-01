using System;

namespace System.Configuration
{
	/// <summary>Indicates that an application settings property has a special significance. This class cannot be inherited.</summary>
	// Token: 0x020001DB RID: 475
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Property)]
	public sealed class SpecialSettingAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.SpecialSettingAttribute" /> class.</summary>
		/// <param name="specialSetting">A <see cref="T:System.Configuration.SpecialSetting" /> enumeration value defining the category of the application settings property.</param>
		// Token: 0x06000C5A RID: 3162 RVA: 0x000327D9 File Offset: 0x000309D9
		public SpecialSettingAttribute(SpecialSetting specialSetting)
		{
			this.setting = specialSetting;
		}

		/// <summary>Gets the value describing the special setting category of the application settings property.</summary>
		/// <returns>A <see cref="T:System.Configuration.SpecialSetting" /> enumeration value defining the category of the application settings property.</returns>
		// Token: 0x17000221 RID: 545
		// (get) Token: 0x06000C5B RID: 3163 RVA: 0x000327E8 File Offset: 0x000309E8
		public SpecialSetting SpecialSetting
		{
			get
			{
				return this.setting;
			}
		}

		// Token: 0x040007BC RID: 1980
		private SpecialSetting setting;
	}
}

using System;
using System.Security.Permissions;

namespace System.Diagnostics
{
	/// <summary>Provides a simple on/off switch that controls debugging and tracing output.</summary>
	// Token: 0x02000212 RID: 530
	[SwitchLevel(typeof(bool))]
	public class BooleanSwitch : Switch
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.BooleanSwitch" /> class with the specified display name and description.</summary>
		/// <param name="displayName">The name to display on a user interface.</param>
		/// <param name="description">The description of the switch.</param>
		// Token: 0x06000F47 RID: 3911 RVA: 0x00044ADF File Offset: 0x00042CDF
		public BooleanSwitch(string displayName, string description) : base(displayName, description)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.BooleanSwitch" /> class with the specified display name, description, and default switch value.</summary>
		/// <param name="displayName">The name to display on the user interface.</param>
		/// <param name="description">The description of the switch.</param>
		/// <param name="defaultSwitchValue">The default value of the switch.</param>
		// Token: 0x06000F48 RID: 3912 RVA: 0x00044AE9 File Offset: 0x00042CE9
		public BooleanSwitch(string displayName, string description, string defaultSwitchValue) : base(displayName, description, defaultSwitchValue)
		{
		}

		/// <summary>Gets or sets a value indicating whether the switch is enabled or disabled.</summary>
		/// <returns>
		///   <see langword="true" /> if the switch is enabled; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the correct permission.</exception>
		// Token: 0x1700026E RID: 622
		// (get) Token: 0x06000F49 RID: 3913 RVA: 0x00044AF4 File Offset: 0x00042CF4
		// (set) Token: 0x06000F4A RID: 3914 RVA: 0x00044B01 File Offset: 0x00042D01
		public bool Enabled
		{
			get
			{
				return base.SwitchSetting != 0;
			}
			[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
			set
			{
				base.SwitchSetting = (value ? 1 : 0);
			}
		}

		/// <summary>Determines whether the new value of the <see cref="P:System.Diagnostics.Switch.Value" /> property can be parsed as a Boolean value.</summary>
		// Token: 0x06000F4B RID: 3915 RVA: 0x00044B10 File Offset: 0x00042D10
		protected override void OnValueChanged()
		{
			bool flag;
			if (bool.TryParse(base.Value, out flag))
			{
				base.SwitchSetting = (flag ? 1 : 0);
				return;
			}
			base.OnValueChanged();
		}
	}
}

using System;
using System.ComponentModel;

namespace System.Configuration
{
	/// <summary>Provides data for the <see cref="E:System.Configuration.ApplicationSettingsBase.SettingChanging" /> event.</summary>
	// Token: 0x020001BD RID: 445
	public class SettingChangingEventArgs : CancelEventArgs
	{
		/// <summary>Initializes an instance of the <see cref="T:System.Configuration.SettingChangingEventArgs" /> class.</summary>
		/// <param name="settingName">A <see cref="T:System.String" /> containing the name of the application setting.</param>
		/// <param name="settingClass">A <see cref="T:System.String" /> containing a category description of the setting. Often this parameter is set to the application settings group name.</param>
		/// <param name="settingKey">A <see cref="T:System.String" /> containing the application settings key.</param>
		/// <param name="newValue">An <see cref="T:System.Object" /> that contains the new value to be assigned to the application settings property.</param>
		/// <param name="cancel">
		///   <see langword="true" /> to cancel the event; otherwise, <see langword="false" />.</param>
		// Token: 0x06000BA9 RID: 2985 RVA: 0x000314CE File Offset: 0x0002F6CE
		public SettingChangingEventArgs(string settingName, string settingClass, string settingKey, object newValue, bool cancel) : base(cancel)
		{
			this.settingName = settingName;
			this.settingClass = settingClass;
			this.settingKey = settingKey;
			this.newValue = newValue;
		}

		/// <summary>Gets the name of the application setting associated with the application settings property.</summary>
		/// <returns>A <see cref="T:System.String" /> containing the name of the application setting.</returns>
		// Token: 0x170001ED RID: 493
		// (get) Token: 0x06000BAA RID: 2986 RVA: 0x000314F5 File Offset: 0x0002F6F5
		public string SettingName
		{
			get
			{
				return this.settingName;
			}
		}

		/// <summary>Gets the application settings property category.</summary>
		/// <returns>A <see cref="T:System.String" /> containing a category description of the setting. Typically, this parameter is set to the application settings group name.</returns>
		// Token: 0x170001EE RID: 494
		// (get) Token: 0x06000BAB RID: 2987 RVA: 0x000314FD File Offset: 0x0002F6FD
		public string SettingClass
		{
			get
			{
				return this.settingClass;
			}
		}

		/// <summary>Gets the application settings key associated with the property.</summary>
		/// <returns>A <see cref="T:System.String" /> containing the application settings key.</returns>
		// Token: 0x170001EF RID: 495
		// (get) Token: 0x06000BAC RID: 2988 RVA: 0x00031505 File Offset: 0x0002F705
		public string SettingKey
		{
			get
			{
				return this.settingKey;
			}
		}

		/// <summary>Gets the new value being assigned to the application settings property.</summary>
		/// <returns>An <see cref="T:System.Object" /> that contains the new value to be assigned to the application settings property.</returns>
		// Token: 0x170001F0 RID: 496
		// (get) Token: 0x06000BAD RID: 2989 RVA: 0x0003150D File Offset: 0x0002F70D
		public object NewValue
		{
			get
			{
				return this.newValue;
			}
		}

		// Token: 0x04000786 RID: 1926
		private string settingName;

		// Token: 0x04000787 RID: 1927
		private string settingClass;

		// Token: 0x04000788 RID: 1928
		private string settingKey;

		// Token: 0x04000789 RID: 1929
		private object newValue;
	}
}

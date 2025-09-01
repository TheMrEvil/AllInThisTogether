using System;

namespace System.Configuration
{
	/// <summary>Defines standard functionality for controls or libraries that store and retrieve application settings.</summary>
	// Token: 0x020001B3 RID: 435
	public interface IPersistComponentSettings
	{
		/// <summary>Gets or sets a value indicating whether the control should automatically persist its application settings properties.</summary>
		/// <returns>
		///   <see langword="true" /> if the control should automatically persist its state; otherwise, <see langword="false" />.</returns>
		// Token: 0x170001E4 RID: 484
		// (get) Token: 0x06000B80 RID: 2944
		// (set) Token: 0x06000B81 RID: 2945
		bool SaveSettings { get; set; }

		/// <summary>Gets or sets the value of the application settings key for the current instance of the control.</summary>
		/// <returns>A <see cref="T:System.String" /> containing the settings key for the current instance of the control.</returns>
		// Token: 0x170001E5 RID: 485
		// (get) Token: 0x06000B82 RID: 2946
		// (set) Token: 0x06000B83 RID: 2947
		string SettingsKey { get; set; }

		/// <summary>Reads the control's application settings into their corresponding properties and updates the control's state.</summary>
		// Token: 0x06000B84 RID: 2948
		void LoadComponentSettings();

		/// <summary>Resets the control's application settings properties to their default values.</summary>
		// Token: 0x06000B85 RID: 2949
		void ResetComponentSettings();

		/// <summary>Persists the control's application settings properties.</summary>
		// Token: 0x06000B86 RID: 2950
		void SaveComponentSettings();
	}
}

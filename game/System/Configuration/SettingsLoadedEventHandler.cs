using System;

namespace System.Configuration
{
	/// <summary>Represents the method that will handle the <see cref="E:System.Configuration.ApplicationSettingsBase.SettingsLoaded" /> event.</summary>
	/// <param name="sender">The source of the event, typically the settings class.</param>
	/// <param name="e">A <see cref="T:System.Configuration.SettingsLoadedEventArgs" /> object that contains the event data.</param>
	// Token: 0x020001C9 RID: 457
	// (Invoke) Token: 0x06000BF4 RID: 3060
	public delegate void SettingsLoadedEventHandler(object sender, SettingsLoadedEventArgs e);
}

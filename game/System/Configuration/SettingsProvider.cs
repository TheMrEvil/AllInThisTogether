using System;
using System.Configuration.Provider;

namespace System.Configuration
{
	/// <summary>Acts as a base class for deriving custom settings providers in the application settings architecture.</summary>
	// Token: 0x020001D3 RID: 467
	public abstract class SettingsProvider : ProviderBase
	{
		/// <summary>Initializes an instance of the <see cref="T:System.Configuration.SettingsProvider" /> class.</summary>
		// Token: 0x06000C47 RID: 3143 RVA: 0x000326B7 File Offset: 0x000308B7
		protected SettingsProvider()
		{
		}

		/// <summary>Returns the collection of settings property values for the specified application instance and settings property group.</summary>
		/// <param name="context">A <see cref="T:System.Configuration.SettingsContext" /> describing the current application use.</param>
		/// <param name="collection">A <see cref="T:System.Configuration.SettingsPropertyCollection" /> containing the settings property group whose values are to be retrieved.</param>
		/// <returns>A <see cref="T:System.Configuration.SettingsPropertyValueCollection" /> containing the values for the specified settings property group.</returns>
		// Token: 0x06000C48 RID: 3144
		public abstract SettingsPropertyValueCollection GetPropertyValues(SettingsContext context, SettingsPropertyCollection collection);

		/// <summary>Sets the values of the specified group of property settings.</summary>
		/// <param name="context">A <see cref="T:System.Configuration.SettingsContext" /> describing the current application usage.</param>
		/// <param name="collection">A <see cref="T:System.Configuration.SettingsPropertyValueCollection" /> representing the group of property settings to set.</param>
		// Token: 0x06000C49 RID: 3145
		public abstract void SetPropertyValues(SettingsContext context, SettingsPropertyValueCollection collection);

		/// <summary>Gets or sets the name of the currently running application.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the application's shortened name, which does not contain a full path or extension, for example, <c>SimpleAppSettings</c>.</returns>
		// Token: 0x1700021D RID: 541
		// (get) Token: 0x06000C4A RID: 3146
		// (set) Token: 0x06000C4B RID: 3147
		public abstract string ApplicationName { get; set; }
	}
}

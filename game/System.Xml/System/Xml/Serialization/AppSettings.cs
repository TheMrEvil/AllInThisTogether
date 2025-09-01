using System;
using System.Collections.Specialized;
using System.Configuration;

namespace System.Xml.Serialization
{
	// Token: 0x02000265 RID: 613
	internal static class AppSettings
	{
		// Token: 0x17000439 RID: 1081
		// (get) Token: 0x06001702 RID: 5890 RVA: 0x00088795 File Offset: 0x00086995
		internal static bool? UseLegacySerializerGeneration
		{
			get
			{
				AppSettings.EnsureSettingsLoaded();
				return AppSettings.useLegacySerializerGeneration;
			}
		}

		// Token: 0x06001703 RID: 5891 RVA: 0x000887A4 File Offset: 0x000869A4
		private static void EnsureSettingsLoaded()
		{
			if (!AppSettings.settingsInitalized)
			{
				object obj = AppSettings.appSettingsLock;
				lock (obj)
				{
					if (!AppSettings.settingsInitalized)
					{
						NameValueCollection nameValueCollection = null;
						try
						{
							nameValueCollection = ConfigurationManager.AppSettings;
						}
						catch (ConfigurationErrorsException)
						{
						}
						finally
						{
							bool value;
							if (nameValueCollection == null || !bool.TryParse(nameValueCollection["System:Xml:Serialization:UseLegacySerializerGeneration"], out value))
							{
								AppSettings.useLegacySerializerGeneration = null;
							}
							else
							{
								AppSettings.useLegacySerializerGeneration = new bool?(value);
							}
							AppSettings.settingsInitalized = true;
						}
					}
				}
			}
		}

		// Token: 0x06001704 RID: 5892 RVA: 0x0008884C File Offset: 0x00086A4C
		// Note: this type is marked as 'beforefieldinit'.
		static AppSettings()
		{
		}

		// Token: 0x04001834 RID: 6196
		private const string UseLegacySerializerGenerationAppSettingsString = "System:Xml:Serialization:UseLegacySerializerGeneration";

		// Token: 0x04001835 RID: 6197
		private static bool? useLegacySerializerGeneration;

		// Token: 0x04001836 RID: 6198
		private static volatile bool settingsInitalized = false;

		// Token: 0x04001837 RID: 6199
		private static object appSettingsLock = new object();
	}
}

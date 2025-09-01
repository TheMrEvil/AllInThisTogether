using System;
using System.Collections.Specialized;
using System.Configuration;

namespace System.Runtime.Serialization
{
	// Token: 0x020000AA RID: 170
	internal static class AppSettings
	{
		// Token: 0x17000120 RID: 288
		// (get) Token: 0x06000908 RID: 2312 RVA: 0x00025518 File Offset: 0x00023718
		internal static int MaxMimeParts
		{
			get
			{
				AppSettings.EnsureSettingsLoaded();
				return AppSettings.maxMimeParts;
			}
		}

		// Token: 0x06000909 RID: 2313 RVA: 0x00025524 File Offset: 0x00023724
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
							if (nameValueCollection == null || !int.TryParse(nameValueCollection["microsoft:xmldictionaryreader:maxmimeparts"], out AppSettings.maxMimeParts))
							{
								AppSettings.maxMimeParts = 1000;
							}
							AppSettings.settingsInitalized = true;
						}
					}
				}
			}
		}

		// Token: 0x0600090A RID: 2314 RVA: 0x000255C4 File Offset: 0x000237C4
		// Note: this type is marked as 'beforefieldinit'.
		static AppSettings()
		{
		}

		// Token: 0x040003E7 RID: 999
		internal const string MaxMimePartsAppSettingsString = "microsoft:xmldictionaryreader:maxmimeparts";

		// Token: 0x040003E8 RID: 1000
		private const int DefaultMaxMimeParts = 1000;

		// Token: 0x040003E9 RID: 1001
		private static int maxMimeParts;

		// Token: 0x040003EA RID: 1002
		private static volatile bool settingsInitalized = false;

		// Token: 0x040003EB RID: 1003
		private static object appSettingsLock = new object();
	}
}

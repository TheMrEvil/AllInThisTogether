using System;
using System.Collections.Specialized;

namespace System.Configuration
{
	/// <summary>Provides runtime versions 1.0 and 1.1 support for reading configuration sections and common configuration settings.</summary>
	// Token: 0x020001A6 RID: 422
	public sealed class ConfigurationSettings
	{
		// Token: 0x06000B11 RID: 2833 RVA: 0x0000219B File Offset: 0x0000039B
		private ConfigurationSettings()
		{
		}

		/// <summary>Returns the <see cref="T:System.Configuration.ConfigurationSection" /> object for the passed configuration section name and path.</summary>
		/// <param name="sectionName">A configuration name and path, such as "system.net/settings".</param>
		/// <returns>The <see cref="T:System.Configuration.ConfigurationSection" /> object for the passed configuration section name and path.  
		///
		///  The <see cref="T:System.Configuration.ConfigurationSettings" /> class provides backward compatibility only. You should use the <see cref="T:System.Configuration.ConfigurationManager" /> class or <see cref="T:System.Web.Configuration.WebConfigurationManager" /> class instead.</returns>
		/// <exception cref="T:System.Configuration.ConfigurationException">Unable to retrieve the requested section.</exception>
		// Token: 0x06000B12 RID: 2834 RVA: 0x0002DC32 File Offset: 0x0002BE32
		[Obsolete("This method is obsolete, it has been replaced by System.Configuration!System.Configuration.ConfigurationManager.GetSection")]
		public static object GetConfig(string sectionName)
		{
			return ConfigurationManager.GetSection(sectionName);
		}

		/// <summary>Gets a read-only <see cref="T:System.Collections.Specialized.NameValueCollection" /> of the application settings section of the configuration file.</summary>
		/// <returns>A read-only <see cref="T:System.Collections.Specialized.NameValueCollection" /> of the application settings section from the configuration file.</returns>
		// Token: 0x170001CC RID: 460
		// (get) Token: 0x06000B13 RID: 2835 RVA: 0x0002F348 File Offset: 0x0002D548
		[Obsolete("This property is obsolete.  Please use System.Configuration.ConfigurationManager.AppSettings")]
		public static NameValueCollection AppSettings
		{
			get
			{
				object obj = ConfigurationManager.GetSection("appSettings");
				if (obj == null)
				{
					obj = new NameValueCollection();
				}
				return (NameValueCollection)obj;
			}
		}

		// Token: 0x06000B14 RID: 2836 RVA: 0x0002F370 File Offset: 0x0002D570
		internal static IConfigurationSystem ChangeConfigurationSystem(IConfigurationSystem newSystem)
		{
			if (newSystem == null)
			{
				throw new ArgumentNullException("newSystem");
			}
			object obj = ConfigurationSettings.lockobj;
			IConfigurationSystem result;
			lock (obj)
			{
				IConfigurationSystem configurationSystem = ConfigurationSettings.config;
				ConfigurationSettings.config = newSystem;
				result = configurationSystem;
			}
			return result;
		}

		// Token: 0x06000B15 RID: 2837 RVA: 0x0002F3C4 File Offset: 0x0002D5C4
		// Note: this type is marked as 'beforefieldinit'.
		static ConfigurationSettings()
		{
		}

		// Token: 0x04000742 RID: 1858
		private static IConfigurationSystem config = DefaultConfig.GetInstance();

		// Token: 0x04000743 RID: 1859
		private static object lockobj = new object();
	}
}

using System;

namespace System.Configuration
{
	/// <summary>Provides programmatic access to the connection strings configuration-file section.</summary>
	// Token: 0x0200003A RID: 58
	public sealed class ConnectionStringsSection : ConfigurationSection
	{
		// Token: 0x0600020D RID: 525 RVA: 0x00007657 File Offset: 0x00005857
		static ConnectionStringsSection()
		{
			ConnectionStringsSection._properties.Add(ConnectionStringsSection._propConnectionStrings);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.ConnectionStringsSection" /> class.</summary>
		// Token: 0x0600020E RID: 526 RVA: 0x00002147 File Offset: 0x00000347
		public ConnectionStringsSection()
		{
		}

		/// <summary>Gets a <see cref="T:System.Configuration.ConnectionStringSettingsCollection" /> collection of <see cref="T:System.Configuration.ConnectionStringSettings" /> objects.</summary>
		/// <returns>A <see cref="T:System.Configuration.ConnectionStringSettingsCollection" /> collection of <see cref="T:System.Configuration.ConnectionStringSettings" /> objects.</returns>
		// Token: 0x1700009B RID: 155
		// (get) Token: 0x0600020F RID: 527 RVA: 0x0000768D File Offset: 0x0000588D
		[ConfigurationProperty("", Options = ConfigurationPropertyOptions.IsDefaultCollection)]
		public ConnectionStringSettingsCollection ConnectionStrings
		{
			get
			{
				return (ConnectionStringSettingsCollection)base[ConnectionStringsSection._propConnectionStrings];
			}
		}

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x06000210 RID: 528 RVA: 0x0000769F File Offset: 0x0000589F
		protected internal override ConfigurationPropertyCollection Properties
		{
			get
			{
				return ConnectionStringsSection._properties;
			}
		}

		// Token: 0x06000211 RID: 529 RVA: 0x000076A6 File Offset: 0x000058A6
		protected internal override object GetRuntimeObject()
		{
			return base.GetRuntimeObject();
		}

		// Token: 0x040000DB RID: 219
		private static readonly ConfigurationProperty _propConnectionStrings = new ConfigurationProperty("", typeof(ConnectionStringSettingsCollection), null, ConfigurationPropertyOptions.IsDefaultCollection);

		// Token: 0x040000DC RID: 220
		private static ConfigurationPropertyCollection _properties = new ConfigurationPropertyCollection();
	}
}

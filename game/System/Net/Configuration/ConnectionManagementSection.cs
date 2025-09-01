using System;
using System.Configuration;

namespace System.Net.Configuration
{
	/// <summary>Represents the configuration section for connection management. This class cannot be inherited.</summary>
	// Token: 0x02000765 RID: 1893
	public sealed class ConnectionManagementSection : ConfigurationSection
	{
		// Token: 0x06003BB3 RID: 15283 RVA: 0x000CC9CE File Offset: 0x000CABCE
		static ConnectionManagementSection()
		{
			ConnectionManagementSection.properties.Add(ConnectionManagementSection.connectionManagementProp);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Configuration.ConnectionManagementSection" /> class.</summary>
		// Token: 0x06003BB4 RID: 15284 RVA: 0x0002EA3F File Offset: 0x0002CC3F
		public ConnectionManagementSection()
		{
		}

		/// <summary>Gets the collection of connection management objects in the section.</summary>
		/// <returns>A <see cref="T:System.Net.Configuration.ConnectionManagementElementCollection" /> that contains the connection management information for the local computer.</returns>
		// Token: 0x17000D7E RID: 3454
		// (get) Token: 0x06003BB5 RID: 15285 RVA: 0x000CCA04 File Offset: 0x000CAC04
		[ConfigurationProperty("", Options = ConfigurationPropertyOptions.IsDefaultCollection)]
		public ConnectionManagementElementCollection ConnectionManagement
		{
			get
			{
				return (ConnectionManagementElementCollection)base[ConnectionManagementSection.connectionManagementProp];
			}
		}

		// Token: 0x17000D7F RID: 3455
		// (get) Token: 0x06003BB6 RID: 15286 RVA: 0x000CCA16 File Offset: 0x000CAC16
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return ConnectionManagementSection.properties;
			}
		}

		// Token: 0x04002385 RID: 9093
		private static ConfigurationProperty connectionManagementProp = new ConfigurationProperty("ConnectionManagement", typeof(ConnectionManagementElementCollection), null, ConfigurationPropertyOptions.IsDefaultCollection);

		// Token: 0x04002386 RID: 9094
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();
	}
}

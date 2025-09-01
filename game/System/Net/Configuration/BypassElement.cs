using System;
using System.Configuration;

namespace System.Net.Configuration
{
	/// <summary>Represents the address information for resources that are not retrieved using a proxy server. This class cannot be inherited.</summary>
	// Token: 0x0200075E RID: 1886
	public sealed class BypassElement : ConfigurationElement
	{
		// Token: 0x06003B7D RID: 15229 RVA: 0x000CC48F File Offset: 0x000CA68F
		static BypassElement()
		{
			BypassElement.properties = new ConfigurationPropertyCollection();
			BypassElement.properties.Add(BypassElement.addressProp);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Configuration.BypassElement" /> class.</summary>
		// Token: 0x06003B7E RID: 15230 RVA: 0x00031238 File Offset: 0x0002F438
		public BypassElement()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Configuration.BypassElement" /> class with the specified type information.</summary>
		/// <param name="address">A string that identifies the address of a resource.</param>
		// Token: 0x06003B7F RID: 15231 RVA: 0x000CC4C5 File Offset: 0x000CA6C5
		public BypassElement(string address)
		{
			this.Address = address;
		}

		/// <summary>Gets or sets the addresses of resources that bypass the proxy server.</summary>
		/// <returns>A string that identifies a resource.</returns>
		// Token: 0x17000D73 RID: 3443
		// (get) Token: 0x06003B80 RID: 15232 RVA: 0x000CC4D4 File Offset: 0x000CA6D4
		// (set) Token: 0x06003B81 RID: 15233 RVA: 0x000CC4E6 File Offset: 0x000CA6E6
		[ConfigurationProperty("address", Options = (ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey))]
		public string Address
		{
			get
			{
				return (string)base[BypassElement.addressProp];
			}
			set
			{
				base[BypassElement.addressProp] = value;
			}
		}

		// Token: 0x17000D74 RID: 3444
		// (get) Token: 0x06003B82 RID: 15234 RVA: 0x000CC4F4 File Offset: 0x000CA6F4
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return BypassElement.properties;
			}
		}

		// Token: 0x0400237E RID: 9086
		private static ConfigurationPropertyCollection properties;

		// Token: 0x0400237F RID: 9087
		private static ConfigurationProperty addressProp = new ConfigurationProperty("address", typeof(string), null, ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey);
	}
}

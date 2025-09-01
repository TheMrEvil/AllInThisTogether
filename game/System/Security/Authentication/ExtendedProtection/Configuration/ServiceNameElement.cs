using System;
using System.Configuration;

namespace System.Security.Authentication.ExtendedProtection.Configuration
{
	/// <summary>The <see cref="T:System.Security.Authentication.ExtendedProtection.Configuration.ServiceNameElement" /> class represents a configuration element for a service name used in a <see cref="T:System.Security.Authentication.ExtendedProtection.Configuration.ServiceNameElementCollection" />.</summary>
	// Token: 0x020002AC RID: 684
	public sealed class ServiceNameElement : ConfigurationElement
	{
		// Token: 0x0600154A RID: 5450 RVA: 0x00055C6F File Offset: 0x00053E6F
		static ServiceNameElement()
		{
			ServiceNameElement.properties.Add(ServiceNameElement.name);
		}

		/// <summary>Gets or sets the Service Provider Name (SPN) for this <see cref="T:System.Security.Authentication.ExtendedProtection.Configuration.ServiceNameElement" /> instance.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the representation of SPN for this <see cref="T:System.Security.Authentication.ExtendedProtection.Configuration.ServiceNameElement" /> instance.</returns>
		// Token: 0x17000405 RID: 1029
		// (get) Token: 0x0600154B RID: 5451 RVA: 0x00055CA3 File Offset: 0x00053EA3
		// (set) Token: 0x0600154C RID: 5452 RVA: 0x00055CB5 File Offset: 0x00053EB5
		[ConfigurationProperty("name")]
		public string Name
		{
			get
			{
				return (string)base[ServiceNameElement.name];
			}
			set
			{
				base[ServiceNameElement.name] = value;
			}
		}

		// Token: 0x17000406 RID: 1030
		// (get) Token: 0x0600154D RID: 5453 RVA: 0x00055CC3 File Offset: 0x00053EC3
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return ServiceNameElement.properties;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Authentication.ExtendedProtection.Configuration.ServiceNameElement" /> class.</summary>
		// Token: 0x0600154E RID: 5454 RVA: 0x00031238 File Offset: 0x0002F438
		public ServiceNameElement()
		{
		}

		// Token: 0x04000C04 RID: 3076
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();

		// Token: 0x04000C05 RID: 3077
		private static ConfigurationProperty name = ConfigUtil.BuildProperty(typeof(ServiceNameElement), "Name");
	}
}

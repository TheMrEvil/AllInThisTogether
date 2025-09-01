using System;

namespace System.Configuration
{
	/// <summary>Provides the configuration setting for International Resource Identifier (IRI) processing in the <see cref="T:System.Uri" /> class.</summary>
	// Token: 0x020001B7 RID: 439
	public sealed class IriParsingElement : ConfigurationElement
	{
		// Token: 0x06000B91 RID: 2961 RVA: 0x0003129E File Offset: 0x0002F49E
		static IriParsingElement()
		{
			IriParsingElement.properties = new ConfigurationPropertyCollection();
			IriParsingElement.properties.Add(IriParsingElement.enabled_prop);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.IriParsingElement" /> class.</summary>
		// Token: 0x06000B92 RID: 2962 RVA: 0x00031238 File Offset: 0x0002F438
		public IriParsingElement()
		{
		}

		/// <summary>Gets or sets the value of the <see cref="T:System.Configuration.IriParsingElement" /> configuration setting.</summary>
		/// <returns>A Boolean that indicates if International Resource Identifier (IRI) processing is enabled.</returns>
		// Token: 0x170001E8 RID: 488
		// (get) Token: 0x06000B93 RID: 2963 RVA: 0x000312D9 File Offset: 0x0002F4D9
		// (set) Token: 0x06000B94 RID: 2964 RVA: 0x000312EB File Offset: 0x0002F4EB
		[ConfigurationProperty("enabled", DefaultValue = false, Options = (ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey))]
		public bool Enabled
		{
			get
			{
				return (bool)base[IriParsingElement.enabled_prop];
			}
			set
			{
				base[IriParsingElement.enabled_prop] = value;
			}
		}

		// Token: 0x170001E9 RID: 489
		// (get) Token: 0x06000B95 RID: 2965 RVA: 0x000312FE File Offset: 0x0002F4FE
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return IriParsingElement.properties;
			}
		}

		// Token: 0x06000B96 RID: 2966 RVA: 0x00031308 File Offset: 0x0002F508
		public override bool Equals(object o)
		{
			IriParsingElement iriParsingElement = o as IriParsingElement;
			return iriParsingElement != null && iriParsingElement.Enabled == this.Enabled;
		}

		// Token: 0x06000B97 RID: 2967 RVA: 0x0003132F File Offset: 0x0002F52F
		public override int GetHashCode()
		{
			return Convert.ToInt32(this.Enabled) ^ 127;
		}

		// Token: 0x04000783 RID: 1923
		private static ConfigurationPropertyCollection properties;

		// Token: 0x04000784 RID: 1924
		private static ConfigurationProperty enabled_prop = new ConfigurationProperty("enabled", typeof(bool), false, ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey);
	}
}

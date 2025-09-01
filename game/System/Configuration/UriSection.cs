using System;
using Unity;

namespace System.Configuration
{
	/// <summary>Represents the Uri section within a configuration file.</summary>
	// Token: 0x020001DC RID: 476
	public sealed class UriSection : ConfigurationSection
	{
		// Token: 0x06000C5C RID: 3164 RVA: 0x000327F0 File Offset: 0x000309F0
		static UriSection()
		{
			UriSection.properties = new ConfigurationPropertyCollection();
			UriSection.properties.Add(UriSection.idn_prop);
			UriSection.properties.Add(UriSection.iriParsing_prop);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.UriSection" /> class.</summary>
		// Token: 0x06000C5D RID: 3165 RVA: 0x0002EA3F File Offset: 0x0002CC3F
		public UriSection()
		{
		}

		/// <summary>Gets an <see cref="T:System.Configuration.IdnElement" /> object that contains the configuration setting for International Domain Name (IDN) processing in the <see cref="T:System.Uri" /> class.</summary>
		/// <returns>The configuration setting for International Domain Name (IDN) processing in the <see cref="T:System.Uri" /> class.</returns>
		// Token: 0x17000222 RID: 546
		// (get) Token: 0x06000C5E RID: 3166 RVA: 0x00032859 File Offset: 0x00030A59
		[ConfigurationProperty("idn")]
		public IdnElement Idn
		{
			get
			{
				return (IdnElement)base[UriSection.idn_prop];
			}
		}

		/// <summary>Gets an <see cref="T:System.Configuration.IriParsingElement" /> object that contains the configuration setting for International Resource Identifiers (IRI) parsing in the <see cref="T:System.Uri" /> class.</summary>
		/// <returns>The configuration setting for International Resource Identifiers (IRI) parsing in the <see cref="T:System.Uri" /> class.</returns>
		// Token: 0x17000223 RID: 547
		// (get) Token: 0x06000C5F RID: 3167 RVA: 0x0003286B File Offset: 0x00030A6B
		[ConfigurationProperty("iriParsing")]
		public IriParsingElement IriParsing
		{
			get
			{
				return (IriParsingElement)base[UriSection.iriParsing_prop];
			}
		}

		// Token: 0x17000224 RID: 548
		// (get) Token: 0x06000C60 RID: 3168 RVA: 0x0003287D File Offset: 0x00030A7D
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return UriSection.properties;
			}
		}

		/// <summary>Gets a <see cref="T:System.Configuration.SchemeSettingElementCollection" /> object that contains the configuration settings for scheme parsing in the <see cref="T:System.Uri" /> class.</summary>
		/// <returns>The configuration settings for scheme parsing in the <see cref="T:System.Uri" /> class</returns>
		// Token: 0x17000225 RID: 549
		// (get) Token: 0x06000C61 RID: 3169 RVA: 0x00032884 File Offset: 0x00030A84
		public SchemeSettingElementCollection SchemeSettings
		{
			get
			{
				ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		// Token: 0x040007BD RID: 1981
		private static ConfigurationPropertyCollection properties;

		// Token: 0x040007BE RID: 1982
		private static ConfigurationProperty idn_prop = new ConfigurationProperty("idn", typeof(IdnElement), null);

		// Token: 0x040007BF RID: 1983
		private static ConfigurationProperty iriParsing_prop = new ConfigurationProperty("iriParsing", typeof(IriParsingElement), null);
	}
}

using System;
using System.Configuration;

namespace System.Net.Configuration
{
	/// <summary>Determines whether Internet Protocol version 6 is enabled on the local computer. This class cannot be inherited.</summary>
	// Token: 0x0200076B RID: 1899
	public sealed class Ipv6Element : ConfigurationElement
	{
		// Token: 0x06003BE6 RID: 15334 RVA: 0x000CD308 File Offset: 0x000CB508
		static Ipv6Element()
		{
			Ipv6Element.properties = new ConfigurationPropertyCollection();
			Ipv6Element.properties.Add(Ipv6Element.enabledProp);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Configuration.Ipv6Element" /> class.</summary>
		// Token: 0x06003BE7 RID: 15335 RVA: 0x00031238 File Offset: 0x0002F438
		public Ipv6Element()
		{
		}

		/// <summary>Gets or sets a Boolean value that indicates whether Internet Protocol version 6 is enabled on the local computer.</summary>
		/// <returns>
		///   <see langword="true" /> if IPv6 is enabled; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000D92 RID: 3474
		// (get) Token: 0x06003BE8 RID: 15336 RVA: 0x000CD342 File Offset: 0x000CB542
		// (set) Token: 0x06003BE9 RID: 15337 RVA: 0x000CD354 File Offset: 0x000CB554
		[ConfigurationProperty("enabled", DefaultValue = "False")]
		public bool Enabled
		{
			get
			{
				return (bool)base[Ipv6Element.enabledProp];
			}
			set
			{
				base[Ipv6Element.enabledProp] = value;
			}
		}

		// Token: 0x17000D93 RID: 3475
		// (get) Token: 0x06003BEA RID: 15338 RVA: 0x000CD367 File Offset: 0x000CB567
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return Ipv6Element.properties;
			}
		}

		// Token: 0x04002399 RID: 9113
		private static ConfigurationPropertyCollection properties;

		// Token: 0x0400239A RID: 9114
		private static ConfigurationProperty enabledProp = new ConfigurationProperty("enabled", typeof(bool), false);
	}
}

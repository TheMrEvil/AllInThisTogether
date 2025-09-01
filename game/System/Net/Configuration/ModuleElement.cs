using System;
using System.Configuration;

namespace System.Net.Configuration
{
	/// <summary>Represents the type information for a custom <see cref="T:System.Net.IWebProxy" /> module. This class cannot be inherited.</summary>
	// Token: 0x0200076D RID: 1901
	public sealed class ModuleElement : ConfigurationElement
	{
		// Token: 0x06003BED RID: 15341 RVA: 0x000CD385 File Offset: 0x000CB585
		static ModuleElement()
		{
			ModuleElement.properties = new ConfigurationPropertyCollection();
			ModuleElement.properties.Add(ModuleElement.typeProp);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Configuration.ModuleElement" /> class.</summary>
		// Token: 0x06003BEE RID: 15342 RVA: 0x00031238 File Offset: 0x0002F438
		public ModuleElement()
		{
		}

		// Token: 0x17000D95 RID: 3477
		// (get) Token: 0x06003BEF RID: 15343 RVA: 0x000CD3BA File Offset: 0x000CB5BA
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return ModuleElement.properties;
			}
		}

		/// <summary>Gets or sets the type and assembly information for the current instance.</summary>
		/// <returns>A string that identifies a type that implements the <see cref="T:System.Net.IWebProxy" /> interface or <see langword="null" /> if no value has been specified.</returns>
		// Token: 0x17000D96 RID: 3478
		// (get) Token: 0x06003BF0 RID: 15344 RVA: 0x000CD3C1 File Offset: 0x000CB5C1
		// (set) Token: 0x06003BF1 RID: 15345 RVA: 0x000CD3D3 File Offset: 0x000CB5D3
		[ConfigurationProperty("type")]
		public string Type
		{
			get
			{
				return (string)base[ModuleElement.typeProp];
			}
			set
			{
				base[ModuleElement.typeProp] = value;
			}
		}

		// Token: 0x0400239B RID: 9115
		private static ConfigurationPropertyCollection properties;

		// Token: 0x0400239C RID: 9116
		private static ConfigurationProperty typeProp = new ConfigurationProperty("type", typeof(string), null);
	}
}

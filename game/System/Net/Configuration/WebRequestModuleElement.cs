using System;
using System.ComponentModel;
using System.Configuration;

namespace System.Net.Configuration
{
	/// <summary>Represents a URI prefix and the associated class that handles creating Web requests for the prefix. This class cannot be inherited.</summary>
	// Token: 0x0200077E RID: 1918
	public sealed class WebRequestModuleElement : ConfigurationElement
	{
		// Token: 0x06003C72 RID: 15474 RVA: 0x000CE2FC File Offset: 0x000CC4FC
		static WebRequestModuleElement()
		{
			WebRequestModuleElement.properties = new ConfigurationPropertyCollection();
			WebRequestModuleElement.properties.Add(WebRequestModuleElement.prefixProp);
			WebRequestModuleElement.properties.Add(WebRequestModuleElement.typeProp);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Configuration.WebRequestModuleElement" /> class.</summary>
		// Token: 0x06003C73 RID: 15475 RVA: 0x00031238 File Offset: 0x0002F438
		public WebRequestModuleElement()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Configuration.WebRequestModuleElement" /> class using the specified URI prefix and type information.</summary>
		/// <param name="prefix">A string containing a URI prefix.</param>
		/// <param name="type">A string containing the type and assembly information for the class that handles creating requests for resources that use the <paramref name="prefix" /> URI prefix.</param>
		// Token: 0x06003C74 RID: 15476 RVA: 0x000CE365 File Offset: 0x000CC565
		public WebRequestModuleElement(string prefix, string type)
		{
			base[WebRequestModuleElement.typeProp] = type;
			this.Prefix = prefix;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Configuration.WebRequestModuleElement" /> class using the specified URI prefix and type identifier.</summary>
		/// <param name="prefix">A string containing a URI prefix.</param>
		/// <param name="type">A <see cref="T:System.Type" /> that identifies the class that handles creating requests for resources that use the <paramref name="prefix" /> URI prefix.</param>
		// Token: 0x06003C75 RID: 15477 RVA: 0x000CE380 File Offset: 0x000CC580
		public WebRequestModuleElement(string prefix, Type type) : this(prefix, type.FullName)
		{
		}

		/// <summary>Gets or sets the URI prefix for the current Web request module.</summary>
		/// <returns>A string that contains a URI prefix.</returns>
		// Token: 0x17000DD7 RID: 3543
		// (get) Token: 0x06003C76 RID: 15478 RVA: 0x000CE38F File Offset: 0x000CC58F
		// (set) Token: 0x06003C77 RID: 15479 RVA: 0x000CE3A1 File Offset: 0x000CC5A1
		[ConfigurationProperty("prefix", Options = (ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey))]
		public string Prefix
		{
			get
			{
				return (string)base[WebRequestModuleElement.prefixProp];
			}
			set
			{
				base[WebRequestModuleElement.prefixProp] = value;
			}
		}

		/// <summary>Gets or sets a class that creates Web requests.</summary>
		/// <returns>A <see cref="T:System.Type" /> instance that identifies a Web request module.</returns>
		// Token: 0x17000DD8 RID: 3544
		// (get) Token: 0x06003C78 RID: 15480 RVA: 0x000CE3AF File Offset: 0x000CC5AF
		// (set) Token: 0x06003C79 RID: 15481 RVA: 0x000CE3C6 File Offset: 0x000CC5C6
		[TypeConverter(typeof(TypeConverter))]
		[ConfigurationProperty("type")]
		public Type Type
		{
			get
			{
				return Type.GetType((string)base[WebRequestModuleElement.typeProp]);
			}
			set
			{
				base[WebRequestModuleElement.typeProp] = value.FullName;
			}
		}

		// Token: 0x17000DD9 RID: 3545
		// (get) Token: 0x06003C7A RID: 15482 RVA: 0x000CE3D9 File Offset: 0x000CC5D9
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return WebRequestModuleElement.properties;
			}
		}

		// Token: 0x040023CD RID: 9165
		private static ConfigurationPropertyCollection properties;

		// Token: 0x040023CE RID: 9166
		private static ConfigurationProperty prefixProp = new ConfigurationProperty("prefix", typeof(string), null, ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey);

		// Token: 0x040023CF RID: 9167
		private static ConfigurationProperty typeProp = new ConfigurationProperty("type", typeof(string));
	}
}

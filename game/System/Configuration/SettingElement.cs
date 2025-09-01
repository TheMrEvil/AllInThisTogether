using System;

namespace System.Configuration
{
	/// <summary>Represents a simplified configuration element used for updating elements in the configuration. This class cannot be inherited.</summary>
	// Token: 0x020001BF RID: 447
	public sealed class SettingElement : ConfigurationElement
	{
		// Token: 0x06000BB2 RID: 2994 RVA: 0x00031518 File Offset: 0x0002F718
		static SettingElement()
		{
			SettingElement.properties = new ConfigurationPropertyCollection();
			SettingElement.properties.Add(SettingElement.name_prop);
			SettingElement.properties.Add(SettingElement.serialize_as_prop);
			SettingElement.properties.Add(SettingElement.value_prop);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.SettingElement" /> class.</summary>
		// Token: 0x06000BB3 RID: 2995 RVA: 0x00031238 File Offset: 0x0002F438
		public SettingElement()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.SettingElement" /> class based on supplied parameters.</summary>
		/// <param name="name">The name of the <see cref="T:System.Configuration.SettingElement" /> object.</param>
		/// <param name="serializeAs">A <see cref="T:System.Configuration.SettingsSerializeAs" /> object. This object is an enumeration used as the serialization scheme to store configuration settings.</param>
		// Token: 0x06000BB4 RID: 2996 RVA: 0x000315B1 File Offset: 0x0002F7B1
		public SettingElement(string name, SettingsSerializeAs serializeAs)
		{
			this.Name = name;
			this.SerializeAs = serializeAs;
		}

		/// <summary>Gets or sets the name of the <see cref="T:System.Configuration.SettingElement" /> object.</summary>
		/// <returns>The name of the <see cref="T:System.Configuration.SettingElement" /> object.</returns>
		// Token: 0x170001F1 RID: 497
		// (get) Token: 0x06000BB5 RID: 2997 RVA: 0x000315C7 File Offset: 0x0002F7C7
		// (set) Token: 0x06000BB6 RID: 2998 RVA: 0x000315D9 File Offset: 0x0002F7D9
		[ConfigurationProperty("name", DefaultValue = "", Options = (ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey))]
		public string Name
		{
			get
			{
				return (string)base[SettingElement.name_prop];
			}
			set
			{
				base[SettingElement.name_prop] = value;
			}
		}

		/// <summary>Gets or sets the value of a <see cref="T:System.Configuration.SettingElement" /> object by using a <see cref="T:System.Configuration.SettingValueElement" /> object.</summary>
		/// <returns>A <see cref="T:System.Configuration.SettingValueElement" /> object containing the value of the <see cref="T:System.Configuration.SettingElement" /> object.</returns>
		// Token: 0x170001F2 RID: 498
		// (get) Token: 0x06000BB7 RID: 2999 RVA: 0x000315E7 File Offset: 0x0002F7E7
		// (set) Token: 0x06000BB8 RID: 3000 RVA: 0x000315F9 File Offset: 0x0002F7F9
		[ConfigurationProperty("value", DefaultValue = null, Options = ConfigurationPropertyOptions.IsRequired)]
		public SettingValueElement Value
		{
			get
			{
				return (SettingValueElement)base[SettingElement.value_prop];
			}
			set
			{
				base[SettingElement.value_prop] = value;
			}
		}

		/// <summary>Gets or sets the serialization mechanism used to persist the values of the <see cref="T:System.Configuration.SettingElement" /> object.</summary>
		/// <returns>A <see cref="T:System.Configuration.SettingsSerializeAs" /> object.</returns>
		// Token: 0x170001F3 RID: 499
		// (get) Token: 0x06000BB9 RID: 3001 RVA: 0x00031607 File Offset: 0x0002F807
		// (set) Token: 0x06000BBA RID: 3002 RVA: 0x00031628 File Offset: 0x0002F828
		[ConfigurationProperty("serializeAs", DefaultValue = SettingsSerializeAs.String, Options = ConfigurationPropertyOptions.IsRequired)]
		public SettingsSerializeAs SerializeAs
		{
			get
			{
				if (base[SettingElement.serialize_as_prop] == null)
				{
					return SettingsSerializeAs.String;
				}
				return (SettingsSerializeAs)base[SettingElement.serialize_as_prop];
			}
			set
			{
				base[SettingElement.serialize_as_prop] = value;
			}
		}

		// Token: 0x170001F4 RID: 500
		// (get) Token: 0x06000BBB RID: 3003 RVA: 0x0003163B File Offset: 0x0002F83B
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return SettingElement.properties;
			}
		}

		/// <summary>Compares the current <see cref="T:System.Configuration.SettingElement" /> instance to the specified object.</summary>
		/// <param name="settings">The object to compare with.</param>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Configuration.SettingElement" /> instance is equal to the specified object; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000BBC RID: 3004 RVA: 0x00031644 File Offset: 0x0002F844
		public override bool Equals(object settings)
		{
			SettingElement settingElement = settings as SettingElement;
			return settingElement != null && (settingElement.SerializeAs == this.SerializeAs && settingElement.Value == this.Value) && settingElement.Name == this.Name;
		}

		/// <summary>Gets a unique value representing the <see cref="T:System.Configuration.SettingElement" /> current instance.</summary>
		/// <returns>A unique value representing the <see cref="T:System.Configuration.SettingElement" /> current instance.</returns>
		// Token: 0x06000BBD RID: 3005 RVA: 0x0003168C File Offset: 0x0002F88C
		public override int GetHashCode()
		{
			int num = (int)(this.SerializeAs ^ (SettingsSerializeAs)127);
			if (this.Name != null)
			{
				num += (this.Name.GetHashCode() ^ 127);
			}
			if (this.Value != null)
			{
				num += this.Value.GetHashCode();
			}
			return num;
		}

		// Token: 0x0400078A RID: 1930
		private static ConfigurationPropertyCollection properties;

		// Token: 0x0400078B RID: 1931
		private static ConfigurationProperty name_prop = new ConfigurationProperty("name", typeof(string), string.Empty, ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey);

		// Token: 0x0400078C RID: 1932
		private static ConfigurationProperty serialize_as_prop = new ConfigurationProperty("serializeAs", typeof(SettingsSerializeAs), null, ConfigurationPropertyOptions.IsRequired);

		// Token: 0x0400078D RID: 1933
		private static ConfigurationProperty value_prop = new ConfigurationProperty("value", typeof(SettingValueElement), null, ConfigurationPropertyOptions.IsRequired);
	}
}

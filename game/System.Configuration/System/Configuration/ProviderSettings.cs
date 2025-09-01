using System;
using System.Collections.Specialized;

namespace System.Configuration
{
	/// <summary>Represents the configuration elements associated with a provider.</summary>
	// Token: 0x02000061 RID: 97
	public sealed class ProviderSettings : ConfigurationElement
	{
		// Token: 0x06000320 RID: 800 RVA: 0x00008D58 File Offset: 0x00006F58
		static ProviderSettings()
		{
			ProviderSettings.properties.Add(ProviderSettings.nameProp);
			ProviderSettings.properties.Add(ProviderSettings.typeProp);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.ProviderSettings" /> class.</summary>
		// Token: 0x06000321 RID: 801 RVA: 0x000067BB File Offset: 0x000049BB
		public ProviderSettings()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.ProviderSettings" /> class.</summary>
		/// <param name="name">The name of the provider to configure settings for.</param>
		/// <param name="type">The type of the provider to configure settings for.</param>
		// Token: 0x06000322 RID: 802 RVA: 0x00008DC3 File Offset: 0x00006FC3
		public ProviderSettings(string name, string type)
		{
			this.Name = name;
			this.Type = type;
		}

		// Token: 0x06000323 RID: 803 RVA: 0x00008DD9 File Offset: 0x00006FD9
		protected override bool OnDeserializeUnrecognizedAttribute(string name, string value)
		{
			if (this.parameters == null)
			{
				this.parameters = new ConfigNameValueCollection();
			}
			this.parameters[name] = value;
			this.parameters.ResetModified();
			return true;
		}

		// Token: 0x06000324 RID: 804 RVA: 0x00008E07 File Offset: 0x00007007
		protected internal override bool IsModified()
		{
			return (this.parameters != null && this.parameters.IsModified) || base.IsModified();
		}

		// Token: 0x06000325 RID: 805 RVA: 0x00008E28 File Offset: 0x00007028
		protected internal override void Reset(ConfigurationElement parentElement)
		{
			base.Reset(parentElement);
			ProviderSettings providerSettings = parentElement as ProviderSettings;
			if (providerSettings != null && providerSettings.parameters != null)
			{
				this.parameters = new ConfigNameValueCollection(providerSettings.parameters);
				return;
			}
			this.parameters = null;
		}

		// Token: 0x06000326 RID: 806 RVA: 0x00008E67 File Offset: 0x00007067
		[MonoTODO]
		protected internal override void Unmerge(ConfigurationElement sourceElement, ConfigurationElement parentElement, ConfigurationSaveMode saveMode)
		{
			base.Unmerge(sourceElement, parentElement, saveMode);
		}

		/// <summary>Gets or sets the name of the provider configured by this class.</summary>
		/// <returns>The name of the provider.</returns>
		// Token: 0x170000EB RID: 235
		// (get) Token: 0x06000327 RID: 807 RVA: 0x00008E72 File Offset: 0x00007072
		// (set) Token: 0x06000328 RID: 808 RVA: 0x00008E84 File Offset: 0x00007084
		[ConfigurationProperty("name", Options = (ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey))]
		public string Name
		{
			get
			{
				return (string)base[ProviderSettings.nameProp];
			}
			set
			{
				base[ProviderSettings.nameProp] = value;
			}
		}

		/// <summary>Gets or sets the type of the provider configured by this class.</summary>
		/// <returns>The fully qualified namespace and class name for the type of provider configured by this <see cref="T:System.Configuration.ProviderSettings" /> instance.</returns>
		// Token: 0x170000EC RID: 236
		// (get) Token: 0x06000329 RID: 809 RVA: 0x00008E92 File Offset: 0x00007092
		// (set) Token: 0x0600032A RID: 810 RVA: 0x00008EA4 File Offset: 0x000070A4
		[ConfigurationProperty("type", Options = ConfigurationPropertyOptions.IsRequired)]
		public string Type
		{
			get
			{
				return (string)base[ProviderSettings.typeProp];
			}
			set
			{
				base[ProviderSettings.typeProp] = value;
			}
		}

		// Token: 0x170000ED RID: 237
		// (get) Token: 0x0600032B RID: 811 RVA: 0x00008EB2 File Offset: 0x000070B2
		protected internal override ConfigurationPropertyCollection Properties
		{
			get
			{
				return ProviderSettings.properties;
			}
		}

		/// <summary>Gets a collection of user-defined parameters for the provider.</summary>
		/// <returns>A <see cref="T:System.Collections.Specialized.NameValueCollection" /> of parameters for the provider.</returns>
		// Token: 0x170000EE RID: 238
		// (get) Token: 0x0600032C RID: 812 RVA: 0x00008EB9 File Offset: 0x000070B9
		public NameValueCollection Parameters
		{
			get
			{
				if (this.parameters == null)
				{
					this.parameters = new ConfigNameValueCollection();
				}
				return this.parameters;
			}
		}

		// Token: 0x04000127 RID: 295
		private ConfigNameValueCollection parameters;

		// Token: 0x04000128 RID: 296
		private static ConfigurationProperty nameProp = new ConfigurationProperty("name", typeof(string), null, ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey);

		// Token: 0x04000129 RID: 297
		private static ConfigurationProperty typeProp = new ConfigurationProperty("type", typeof(string), null, ConfigurationPropertyOptions.IsRequired);

		// Token: 0x0400012A RID: 298
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();
	}
}

using System;
using System.Configuration;

namespace System.Xml.Serialization.Configuration
{
	/// <summary>Handles the XML elements used to configure XML serialization. </summary>
	// Token: 0x0200031E RID: 798
	public sealed class XmlSerializerSection : ConfigurationSection
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Serialization.Configuration.XmlSerializerSection" /> class. </summary>
		// Token: 0x060020E4 RID: 8420 RVA: 0x000D1F28 File Offset: 0x000D0128
		public XmlSerializerSection()
		{
			this.properties.Add(this.checkDeserializeAdvances);
			this.properties.Add(this.tempFilesLocation);
			this.properties.Add(this.useLegacySerializerGeneration);
		}

		// Token: 0x17000652 RID: 1618
		// (get) Token: 0x060020E5 RID: 8421 RVA: 0x000D1FDD File Offset: 0x000D01DD
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return this.properties;
			}
		}

		/// <summary>Gets or sets a value that determines whether an additional check of progress of the <see cref="T:System.Xml.Serialization.XmlSerializer" /> is done.</summary>
		/// <returns>
		///     <see langword="true" /> if the check is made; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
		// Token: 0x17000653 RID: 1619
		// (get) Token: 0x060020E6 RID: 8422 RVA: 0x000D1FE5 File Offset: 0x000D01E5
		// (set) Token: 0x060020E7 RID: 8423 RVA: 0x000D1FF8 File Offset: 0x000D01F8
		[ConfigurationProperty("checkDeserializeAdvances", DefaultValue = false)]
		public bool CheckDeserializeAdvances
		{
			get
			{
				return (bool)base[this.checkDeserializeAdvances];
			}
			set
			{
				base[this.checkDeserializeAdvances] = value;
			}
		}

		/// <summary>Returns the location that was specified for the creation of the temporary file.</summary>
		/// <returns>The location that was specified for the creation of the temporary file.</returns>
		// Token: 0x17000654 RID: 1620
		// (get) Token: 0x060020E8 RID: 8424 RVA: 0x000D200C File Offset: 0x000D020C
		// (set) Token: 0x060020E9 RID: 8425 RVA: 0x000D201F File Offset: 0x000D021F
		[ConfigurationProperty("tempFilesLocation", DefaultValue = null)]
		public string TempFilesLocation
		{
			get
			{
				return (string)base[this.tempFilesLocation];
			}
			set
			{
				base[this.tempFilesLocation] = value;
			}
		}

		/// <summary>Gets or sets a value that indicates whether the specified object uses legacy serializer generation.</summary>
		/// <returns>
		///     <see langword="true" /> if the object uses legacy serializer generation; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000655 RID: 1621
		// (get) Token: 0x060020EA RID: 8426 RVA: 0x000D202E File Offset: 0x000D022E
		// (set) Token: 0x060020EB RID: 8427 RVA: 0x000D2041 File Offset: 0x000D0241
		[ConfigurationProperty("useLegacySerializerGeneration", DefaultValue = false)]
		public bool UseLegacySerializerGeneration
		{
			get
			{
				return (bool)base[this.useLegacySerializerGeneration];
			}
			set
			{
				base[this.useLegacySerializerGeneration] = value;
			}
		}

		// Token: 0x04001B71 RID: 7025
		private ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();

		// Token: 0x04001B72 RID: 7026
		private readonly ConfigurationProperty checkDeserializeAdvances = new ConfigurationProperty("checkDeserializeAdvances", typeof(bool), false, ConfigurationPropertyOptions.None);

		// Token: 0x04001B73 RID: 7027
		private readonly ConfigurationProperty tempFilesLocation = new ConfigurationProperty("tempFilesLocation", typeof(string), null, null, new RootedPathValidator(), ConfigurationPropertyOptions.None);

		// Token: 0x04001B74 RID: 7028
		private readonly ConfigurationProperty useLegacySerializerGeneration = new ConfigurationProperty("useLegacySerializerGeneration", typeof(bool), false, ConfigurationPropertyOptions.None);
	}
}

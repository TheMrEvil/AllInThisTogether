using System;
using System.ComponentModel;
using System.Configuration;

namespace System.Xml.Serialization.Configuration
{
	/// <summary>Handles configuration settings for XML serialization of <see cref="T:System.DateTime" /> instances.</summary>
	// Token: 0x02000316 RID: 790
	public sealed class DateTimeSerializationSection : ConfigurationSection
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Serialization.Configuration.DateTimeSerializationSection" /> class.</summary>
		// Token: 0x060020B8 RID: 8376 RVA: 0x000D17FC File Offset: 0x000CF9FC
		public DateTimeSerializationSection()
		{
			this.properties.Add(this.mode);
		}

		// Token: 0x17000644 RID: 1604
		// (get) Token: 0x060020B9 RID: 8377 RVA: 0x000D185C File Offset: 0x000CFA5C
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return this.properties;
			}
		}

		/// <summary>Gets or sets a value that determines the serialization format.</summary>
		/// <returns>One of the <see cref="T:System.Xml.Serialization.Configuration.DateTimeSerializationSection.DateTimeSerializationMode" /> values.</returns>
		// Token: 0x17000645 RID: 1605
		// (get) Token: 0x060020BA RID: 8378 RVA: 0x000D1864 File Offset: 0x000CFA64
		// (set) Token: 0x060020BB RID: 8379 RVA: 0x000D1877 File Offset: 0x000CFA77
		[ConfigurationProperty("mode", DefaultValue = DateTimeSerializationSection.DateTimeSerializationMode.Roundtrip)]
		public DateTimeSerializationSection.DateTimeSerializationMode Mode
		{
			get
			{
				return (DateTimeSerializationSection.DateTimeSerializationMode)base[this.mode];
			}
			set
			{
				base[this.mode] = value;
			}
		}

		// Token: 0x04001B64 RID: 7012
		private ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();

		// Token: 0x04001B65 RID: 7013
		private readonly ConfigurationProperty mode = new ConfigurationProperty("mode", typeof(DateTimeSerializationSection.DateTimeSerializationMode), DateTimeSerializationSection.DateTimeSerializationMode.Roundtrip, new EnumConverter(typeof(DateTimeSerializationSection.DateTimeSerializationMode)), null, ConfigurationPropertyOptions.None);

		/// <summary>Determines XML serialization format of <see cref="T:System.DateTime" /> objects.</summary>
		// Token: 0x02000317 RID: 791
		public enum DateTimeSerializationMode
		{
			/// <summary>Same as <see langword="Roundtrip" />.</summary>
			// Token: 0x04001B67 RID: 7015
			Default,
			/// <summary>The serializer examines individual <see cref="T:System.DateTime" /> instances to determine the serialization format: UTC, local, or unspecified.</summary>
			// Token: 0x04001B68 RID: 7016
			Roundtrip,
			/// <summary>The serializer formats all <see cref="T:System.DateTime" /> objects as local time. This is for version 1.0 and 1.1 compatibility.</summary>
			// Token: 0x04001B69 RID: 7017
			Local
		}
	}
}

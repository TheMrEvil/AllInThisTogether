using System;
using System.Configuration;

namespace System.Xml.Serialization.Configuration
{
	/// <summary>Handles the XML elements used to configure XML serialization.</summary>
	// Token: 0x0200031D RID: 797
	public sealed class SerializationSectionGroup : ConfigurationSectionGroup
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Serialization.Configuration.SerializationSectionGroup" /> class.</summary>
		// Token: 0x060020E0 RID: 8416 RVA: 0x000D1ED8 File Offset: 0x000D00D8
		public SerializationSectionGroup()
		{
		}

		/// <summary>Gets the object that represents the section that contains configuration elements for the <see cref="T:System.Xml.Serialization.XmlSchemaImporter" />.</summary>
		/// <returns>The <see cref="T:System.Xml.Serialization.Configuration.SchemaImporterExtensionsSection" /> that represents the <see langword="schemaImporterExtenstion" /> element in the configuration file.</returns>
		// Token: 0x1700064F RID: 1615
		// (get) Token: 0x060020E1 RID: 8417 RVA: 0x000D1EE0 File Offset: 0x000D00E0
		[ConfigurationProperty("schemaImporterExtensions")]
		public SchemaImporterExtensionsSection SchemaImporterExtensions
		{
			get
			{
				return (SchemaImporterExtensionsSection)base.Sections["schemaImporterExtensions"];
			}
		}

		/// <summary>Gets the object that represents the <see cref="T:System.DateTime" /> serialization configuration element.</summary>
		/// <returns>The <see cref="T:System.Xml.Serialization.Configuration.DateTimeSerializationSection" /> object that represents the configuration element.</returns>
		// Token: 0x17000650 RID: 1616
		// (get) Token: 0x060020E2 RID: 8418 RVA: 0x000D1EF7 File Offset: 0x000D00F7
		[ConfigurationProperty("dateTimeSerialization")]
		public DateTimeSerializationSection DateTimeSerialization
		{
			get
			{
				return (DateTimeSerializationSection)base.Sections["dateTimeSerialization"];
			}
		}

		/// <summary>Gets the object that represents the configuration group for the <see cref="T:System.Xml.Serialization.XmlSerializer" />.</summary>
		/// <returns>The <see cref="T:System.Xml.Serialization.Configuration.XmlSerializerSection" /> that represents the <see cref="T:System.Xml.Serialization.XmlSerializer" />.</returns>
		// Token: 0x17000651 RID: 1617
		// (get) Token: 0x060020E3 RID: 8419 RVA: 0x000D1F0E File Offset: 0x000D010E
		public XmlSerializerSection XmlSerializer
		{
			get
			{
				return (XmlSerializerSection)base.Sections["xmlSerializer"];
			}
		}
	}
}

using System;
using System.IO;

namespace System.Xml.Xsl
{
	// Token: 0x0200032B RID: 811
	internal class QueryReaderSettings
	{
		// Token: 0x06002150 RID: 8528 RVA: 0x000D2AF0 File Offset: 0x000D0CF0
		public QueryReaderSettings(XmlNameTable xmlNameTable)
		{
			this.xmlReaderSettings = new XmlReaderSettings();
			this.xmlReaderSettings.NameTable = xmlNameTable;
			this.xmlReaderSettings.ConformanceLevel = ConformanceLevel.Document;
			this.xmlReaderSettings.XmlResolver = null;
			this.xmlReaderSettings.DtdProcessing = DtdProcessing.Prohibit;
			this.xmlReaderSettings.CloseInput = true;
		}

		// Token: 0x06002151 RID: 8529 RVA: 0x000D2B4C File Offset: 0x000D0D4C
		public QueryReaderSettings(XmlReader reader)
		{
			XmlValidatingReader xmlValidatingReader = reader as XmlValidatingReader;
			if (xmlValidatingReader != null)
			{
				this.validatingReader = true;
				reader = xmlValidatingReader.Impl.Reader;
			}
			this.xmlReaderSettings = reader.Settings;
			if (this.xmlReaderSettings != null)
			{
				this.xmlReaderSettings = this.xmlReaderSettings.Clone();
				this.xmlReaderSettings.NameTable = reader.NameTable;
				this.xmlReaderSettings.CloseInput = true;
				this.xmlReaderSettings.LineNumberOffset = 0;
				this.xmlReaderSettings.LinePositionOffset = 0;
				XmlTextReaderImpl xmlTextReaderImpl = reader as XmlTextReaderImpl;
				if (xmlTextReaderImpl != null)
				{
					this.xmlReaderSettings.XmlResolver = xmlTextReaderImpl.GetResolver();
					return;
				}
			}
			else
			{
				this.xmlNameTable = reader.NameTable;
				XmlTextReader xmlTextReader = reader as XmlTextReader;
				if (xmlTextReader != null)
				{
					XmlTextReaderImpl impl = xmlTextReader.Impl;
					this.entityHandling = impl.EntityHandling;
					this.namespaces = impl.Namespaces;
					this.normalization = impl.Normalization;
					this.prohibitDtd = (impl.DtdProcessing == DtdProcessing.Prohibit);
					this.whitespaceHandling = impl.WhitespaceHandling;
					this.xmlResolver = impl.GetResolver();
					return;
				}
				this.entityHandling = EntityHandling.ExpandEntities;
				this.namespaces = true;
				this.normalization = true;
				this.prohibitDtd = true;
				this.whitespaceHandling = WhitespaceHandling.All;
				this.xmlResolver = null;
			}
		}

		// Token: 0x06002152 RID: 8530 RVA: 0x000D2C8C File Offset: 0x000D0E8C
		public XmlReader CreateReader(Stream stream, string baseUri)
		{
			XmlReader xmlReader;
			if (this.xmlReaderSettings != null)
			{
				xmlReader = XmlReader.Create(stream, this.xmlReaderSettings, baseUri);
			}
			else
			{
				xmlReader = new XmlTextReaderImpl(baseUri, stream, this.xmlNameTable)
				{
					EntityHandling = this.entityHandling,
					Namespaces = this.namespaces,
					Normalization = this.normalization,
					DtdProcessing = (this.prohibitDtd ? DtdProcessing.Prohibit : DtdProcessing.Parse),
					WhitespaceHandling = this.whitespaceHandling,
					XmlResolver = this.xmlResolver
				};
			}
			if (this.validatingReader)
			{
				xmlReader = new XmlValidatingReader(xmlReader);
			}
			return xmlReader;
		}

		// Token: 0x1700067B RID: 1659
		// (get) Token: 0x06002153 RID: 8531 RVA: 0x000D2D1D File Offset: 0x000D0F1D
		public XmlNameTable NameTable
		{
			get
			{
				if (this.xmlReaderSettings == null)
				{
					return this.xmlNameTable;
				}
				return this.xmlReaderSettings.NameTable;
			}
		}

		// Token: 0x04001B8F RID: 7055
		private bool validatingReader;

		// Token: 0x04001B90 RID: 7056
		private XmlReaderSettings xmlReaderSettings;

		// Token: 0x04001B91 RID: 7057
		private XmlNameTable xmlNameTable;

		// Token: 0x04001B92 RID: 7058
		private EntityHandling entityHandling;

		// Token: 0x04001B93 RID: 7059
		private bool namespaces;

		// Token: 0x04001B94 RID: 7060
		private bool normalization;

		// Token: 0x04001B95 RID: 7061
		private bool prohibitDtd;

		// Token: 0x04001B96 RID: 7062
		private WhitespaceHandling whitespaceHandling;

		// Token: 0x04001B97 RID: 7063
		private XmlResolver xmlResolver;
	}
}

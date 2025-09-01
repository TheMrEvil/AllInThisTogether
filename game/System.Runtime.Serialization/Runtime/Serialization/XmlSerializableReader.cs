using System;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace System.Runtime.Serialization
{
	// Token: 0x0200014F RID: 335
	internal class XmlSerializableReader : XmlReader, IXmlLineInfo, IXmlTextParser
	{
		// Token: 0x170003D5 RID: 981
		// (get) Token: 0x06001146 RID: 4422 RVA: 0x0004484F File Offset: 0x00042A4F
		private XmlReader InnerReader
		{
			get
			{
				return this.innerReader;
			}
		}

		// Token: 0x06001147 RID: 4423 RVA: 0x00044858 File Offset: 0x00042A58
		internal void BeginRead(XmlReaderDelegator xmlReader)
		{
			if (xmlReader.NodeType != XmlNodeType.Element)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(XmlObjectSerializerReadContext.CreateUnexpectedStateException(XmlNodeType.Element, xmlReader));
			}
			this.xmlReader = xmlReader;
			this.startDepth = xmlReader.Depth;
			this.innerReader = xmlReader.UnderlyingReader;
			this.isRootEmptyElement = this.InnerReader.IsEmptyElement;
		}

		// Token: 0x06001148 RID: 4424 RVA: 0x000448AC File Offset: 0x00042AAC
		internal void EndRead()
		{
			if (this.isRootEmptyElement)
			{
				this.xmlReader.Read();
				return;
			}
			if (this.xmlReader.IsStartElement() && this.xmlReader.Depth == this.startDepth)
			{
				this.xmlReader.Read();
			}
			while (this.xmlReader.Depth > this.startDepth)
			{
				if (!this.xmlReader.Read())
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(XmlObjectSerializerReadContext.CreateUnexpectedStateException(XmlNodeType.EndElement, this.xmlReader));
				}
			}
		}

		// Token: 0x06001149 RID: 4425 RVA: 0x00044930 File Offset: 0x00042B30
		public override bool Read()
		{
			XmlReader xmlReader = this.InnerReader;
			return (xmlReader.Depth != this.startDepth || (xmlReader.NodeType != XmlNodeType.EndElement && (xmlReader.NodeType != XmlNodeType.Element || !xmlReader.IsEmptyElement))) && xmlReader.Read();
		}

		// Token: 0x0600114A RID: 4426 RVA: 0x00044975 File Offset: 0x00042B75
		public override void Close()
		{
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(XmlObjectSerializer.CreateSerializationException(SR.GetString("This method cannot be called from IXmlSerializable implementations.")));
		}

		// Token: 0x170003D6 RID: 982
		// (get) Token: 0x0600114B RID: 4427 RVA: 0x0004498B File Offset: 0x00042B8B
		public override XmlReaderSettings Settings
		{
			get
			{
				return this.InnerReader.Settings;
			}
		}

		// Token: 0x170003D7 RID: 983
		// (get) Token: 0x0600114C RID: 4428 RVA: 0x00044998 File Offset: 0x00042B98
		public override XmlNodeType NodeType
		{
			get
			{
				return this.InnerReader.NodeType;
			}
		}

		// Token: 0x170003D8 RID: 984
		// (get) Token: 0x0600114D RID: 4429 RVA: 0x000449A5 File Offset: 0x00042BA5
		public override string Name
		{
			get
			{
				return this.InnerReader.Name;
			}
		}

		// Token: 0x170003D9 RID: 985
		// (get) Token: 0x0600114E RID: 4430 RVA: 0x000449B2 File Offset: 0x00042BB2
		public override string LocalName
		{
			get
			{
				return this.InnerReader.LocalName;
			}
		}

		// Token: 0x170003DA RID: 986
		// (get) Token: 0x0600114F RID: 4431 RVA: 0x000449BF File Offset: 0x00042BBF
		public override string NamespaceURI
		{
			get
			{
				return this.InnerReader.NamespaceURI;
			}
		}

		// Token: 0x170003DB RID: 987
		// (get) Token: 0x06001150 RID: 4432 RVA: 0x000449CC File Offset: 0x00042BCC
		public override string Prefix
		{
			get
			{
				return this.InnerReader.Prefix;
			}
		}

		// Token: 0x170003DC RID: 988
		// (get) Token: 0x06001151 RID: 4433 RVA: 0x000449D9 File Offset: 0x00042BD9
		public override bool HasValue
		{
			get
			{
				return this.InnerReader.HasValue;
			}
		}

		// Token: 0x170003DD RID: 989
		// (get) Token: 0x06001152 RID: 4434 RVA: 0x000449E6 File Offset: 0x00042BE6
		public override string Value
		{
			get
			{
				return this.InnerReader.Value;
			}
		}

		// Token: 0x170003DE RID: 990
		// (get) Token: 0x06001153 RID: 4435 RVA: 0x000449F3 File Offset: 0x00042BF3
		public override int Depth
		{
			get
			{
				return this.InnerReader.Depth;
			}
		}

		// Token: 0x170003DF RID: 991
		// (get) Token: 0x06001154 RID: 4436 RVA: 0x00044A00 File Offset: 0x00042C00
		public override string BaseURI
		{
			get
			{
				return this.InnerReader.BaseURI;
			}
		}

		// Token: 0x170003E0 RID: 992
		// (get) Token: 0x06001155 RID: 4437 RVA: 0x00044A0D File Offset: 0x00042C0D
		public override bool IsEmptyElement
		{
			get
			{
				return this.InnerReader.IsEmptyElement;
			}
		}

		// Token: 0x170003E1 RID: 993
		// (get) Token: 0x06001156 RID: 4438 RVA: 0x00044A1A File Offset: 0x00042C1A
		public override bool IsDefault
		{
			get
			{
				return this.InnerReader.IsDefault;
			}
		}

		// Token: 0x170003E2 RID: 994
		// (get) Token: 0x06001157 RID: 4439 RVA: 0x00044A27 File Offset: 0x00042C27
		public override char QuoteChar
		{
			get
			{
				return this.InnerReader.QuoteChar;
			}
		}

		// Token: 0x170003E3 RID: 995
		// (get) Token: 0x06001158 RID: 4440 RVA: 0x00044A34 File Offset: 0x00042C34
		public override XmlSpace XmlSpace
		{
			get
			{
				return this.InnerReader.XmlSpace;
			}
		}

		// Token: 0x170003E4 RID: 996
		// (get) Token: 0x06001159 RID: 4441 RVA: 0x00044A41 File Offset: 0x00042C41
		public override string XmlLang
		{
			get
			{
				return this.InnerReader.XmlLang;
			}
		}

		// Token: 0x170003E5 RID: 997
		// (get) Token: 0x0600115A RID: 4442 RVA: 0x00044A4E File Offset: 0x00042C4E
		public override IXmlSchemaInfo SchemaInfo
		{
			get
			{
				return this.InnerReader.SchemaInfo;
			}
		}

		// Token: 0x170003E6 RID: 998
		// (get) Token: 0x0600115B RID: 4443 RVA: 0x00044A5B File Offset: 0x00042C5B
		public override Type ValueType
		{
			get
			{
				return this.InnerReader.ValueType;
			}
		}

		// Token: 0x170003E7 RID: 999
		// (get) Token: 0x0600115C RID: 4444 RVA: 0x00044A68 File Offset: 0x00042C68
		public override int AttributeCount
		{
			get
			{
				return this.InnerReader.AttributeCount;
			}
		}

		// Token: 0x170003E8 RID: 1000
		public override string this[int i]
		{
			get
			{
				return this.InnerReader[i];
			}
		}

		// Token: 0x170003E9 RID: 1001
		public override string this[string name]
		{
			get
			{
				return this.InnerReader[name];
			}
		}

		// Token: 0x170003EA RID: 1002
		public override string this[string name, string namespaceURI]
		{
			get
			{
				return this.InnerReader[name, namespaceURI];
			}
		}

		// Token: 0x170003EB RID: 1003
		// (get) Token: 0x06001160 RID: 4448 RVA: 0x00044AA0 File Offset: 0x00042CA0
		public override bool EOF
		{
			get
			{
				return this.InnerReader.EOF;
			}
		}

		// Token: 0x170003EC RID: 1004
		// (get) Token: 0x06001161 RID: 4449 RVA: 0x00044AAD File Offset: 0x00042CAD
		public override ReadState ReadState
		{
			get
			{
				return this.InnerReader.ReadState;
			}
		}

		// Token: 0x170003ED RID: 1005
		// (get) Token: 0x06001162 RID: 4450 RVA: 0x00044ABA File Offset: 0x00042CBA
		public override XmlNameTable NameTable
		{
			get
			{
				return this.InnerReader.NameTable;
			}
		}

		// Token: 0x170003EE RID: 1006
		// (get) Token: 0x06001163 RID: 4451 RVA: 0x00044AC7 File Offset: 0x00042CC7
		public override bool CanResolveEntity
		{
			get
			{
				return this.InnerReader.CanResolveEntity;
			}
		}

		// Token: 0x170003EF RID: 1007
		// (get) Token: 0x06001164 RID: 4452 RVA: 0x00044AD4 File Offset: 0x00042CD4
		public override bool CanReadBinaryContent
		{
			get
			{
				return this.InnerReader.CanReadBinaryContent;
			}
		}

		// Token: 0x170003F0 RID: 1008
		// (get) Token: 0x06001165 RID: 4453 RVA: 0x00044AE1 File Offset: 0x00042CE1
		public override bool CanReadValueChunk
		{
			get
			{
				return this.InnerReader.CanReadValueChunk;
			}
		}

		// Token: 0x170003F1 RID: 1009
		// (get) Token: 0x06001166 RID: 4454 RVA: 0x00044AEE File Offset: 0x00042CEE
		public override bool HasAttributes
		{
			get
			{
				return this.InnerReader.HasAttributes;
			}
		}

		// Token: 0x06001167 RID: 4455 RVA: 0x00044AFB File Offset: 0x00042CFB
		public override string GetAttribute(string name)
		{
			return this.InnerReader.GetAttribute(name);
		}

		// Token: 0x06001168 RID: 4456 RVA: 0x00044B09 File Offset: 0x00042D09
		public override string GetAttribute(string name, string namespaceURI)
		{
			return this.InnerReader.GetAttribute(name, namespaceURI);
		}

		// Token: 0x06001169 RID: 4457 RVA: 0x00044B18 File Offset: 0x00042D18
		public override string GetAttribute(int i)
		{
			return this.InnerReader.GetAttribute(i);
		}

		// Token: 0x0600116A RID: 4458 RVA: 0x00044B26 File Offset: 0x00042D26
		public override bool MoveToAttribute(string name)
		{
			return this.InnerReader.MoveToAttribute(name);
		}

		// Token: 0x0600116B RID: 4459 RVA: 0x00044B34 File Offset: 0x00042D34
		public override bool MoveToAttribute(string name, string ns)
		{
			return this.InnerReader.MoveToAttribute(name, ns);
		}

		// Token: 0x0600116C RID: 4460 RVA: 0x00044B43 File Offset: 0x00042D43
		public override void MoveToAttribute(int i)
		{
			this.InnerReader.MoveToAttribute(i);
		}

		// Token: 0x0600116D RID: 4461 RVA: 0x00044B51 File Offset: 0x00042D51
		public override bool MoveToFirstAttribute()
		{
			return this.InnerReader.MoveToFirstAttribute();
		}

		// Token: 0x0600116E RID: 4462 RVA: 0x00044B5E File Offset: 0x00042D5E
		public override bool MoveToNextAttribute()
		{
			return this.InnerReader.MoveToNextAttribute();
		}

		// Token: 0x0600116F RID: 4463 RVA: 0x00044B6B File Offset: 0x00042D6B
		public override bool MoveToElement()
		{
			return this.InnerReader.MoveToElement();
		}

		// Token: 0x06001170 RID: 4464 RVA: 0x00044B78 File Offset: 0x00042D78
		public override string LookupNamespace(string prefix)
		{
			return this.InnerReader.LookupNamespace(prefix);
		}

		// Token: 0x06001171 RID: 4465 RVA: 0x00044B86 File Offset: 0x00042D86
		public override bool ReadAttributeValue()
		{
			return this.InnerReader.ReadAttributeValue();
		}

		// Token: 0x06001172 RID: 4466 RVA: 0x00044B93 File Offset: 0x00042D93
		public override void ResolveEntity()
		{
			this.InnerReader.ResolveEntity();
		}

		// Token: 0x06001173 RID: 4467 RVA: 0x00044BA0 File Offset: 0x00042DA0
		public override bool IsStartElement()
		{
			return this.InnerReader.IsStartElement();
		}

		// Token: 0x06001174 RID: 4468 RVA: 0x00044BAD File Offset: 0x00042DAD
		public override bool IsStartElement(string name)
		{
			return this.InnerReader.IsStartElement(name);
		}

		// Token: 0x06001175 RID: 4469 RVA: 0x00044BBB File Offset: 0x00042DBB
		public override bool IsStartElement(string localname, string ns)
		{
			return this.InnerReader.IsStartElement(localname, ns);
		}

		// Token: 0x06001176 RID: 4470 RVA: 0x00044BCA File Offset: 0x00042DCA
		public override XmlNodeType MoveToContent()
		{
			return this.InnerReader.MoveToContent();
		}

		// Token: 0x06001177 RID: 4471 RVA: 0x00044BD7 File Offset: 0x00042DD7
		public override object ReadContentAsObject()
		{
			return this.InnerReader.ReadContentAsObject();
		}

		// Token: 0x06001178 RID: 4472 RVA: 0x00044BE4 File Offset: 0x00042DE4
		public override bool ReadContentAsBoolean()
		{
			return this.InnerReader.ReadContentAsBoolean();
		}

		// Token: 0x06001179 RID: 4473 RVA: 0x00044BF1 File Offset: 0x00042DF1
		public override DateTime ReadContentAsDateTime()
		{
			return this.InnerReader.ReadContentAsDateTime();
		}

		// Token: 0x0600117A RID: 4474 RVA: 0x00044BFE File Offset: 0x00042DFE
		public override double ReadContentAsDouble()
		{
			return this.InnerReader.ReadContentAsDouble();
		}

		// Token: 0x0600117B RID: 4475 RVA: 0x00044C0B File Offset: 0x00042E0B
		public override int ReadContentAsInt()
		{
			return this.InnerReader.ReadContentAsInt();
		}

		// Token: 0x0600117C RID: 4476 RVA: 0x00044C18 File Offset: 0x00042E18
		public override long ReadContentAsLong()
		{
			return this.InnerReader.ReadContentAsLong();
		}

		// Token: 0x0600117D RID: 4477 RVA: 0x00044C25 File Offset: 0x00042E25
		public override string ReadContentAsString()
		{
			return this.InnerReader.ReadContentAsString();
		}

		// Token: 0x0600117E RID: 4478 RVA: 0x00044C32 File Offset: 0x00042E32
		public override object ReadContentAs(Type returnType, IXmlNamespaceResolver namespaceResolver)
		{
			return this.InnerReader.ReadContentAs(returnType, namespaceResolver);
		}

		// Token: 0x0600117F RID: 4479 RVA: 0x00044C41 File Offset: 0x00042E41
		public override int ReadContentAsBase64(byte[] buffer, int index, int count)
		{
			return this.InnerReader.ReadContentAsBase64(buffer, index, count);
		}

		// Token: 0x06001180 RID: 4480 RVA: 0x00044C51 File Offset: 0x00042E51
		public override int ReadContentAsBinHex(byte[] buffer, int index, int count)
		{
			return this.InnerReader.ReadContentAsBinHex(buffer, index, count);
		}

		// Token: 0x06001181 RID: 4481 RVA: 0x00044C61 File Offset: 0x00042E61
		public override int ReadValueChunk(char[] buffer, int index, int count)
		{
			return this.InnerReader.ReadValueChunk(buffer, index, count);
		}

		// Token: 0x06001182 RID: 4482 RVA: 0x00044C71 File Offset: 0x00042E71
		public override string ReadString()
		{
			return this.InnerReader.ReadString();
		}

		// Token: 0x170003F2 RID: 1010
		// (get) Token: 0x06001183 RID: 4483 RVA: 0x00044C80 File Offset: 0x00042E80
		// (set) Token: 0x06001184 RID: 4484 RVA: 0x00044CB0 File Offset: 0x00042EB0
		bool IXmlTextParser.Normalized
		{
			get
			{
				IXmlTextParser xmlTextParser = this.InnerReader as IXmlTextParser;
				if (xmlTextParser != null)
				{
					return xmlTextParser.Normalized;
				}
				return this.xmlReader.Normalized;
			}
			set
			{
				IXmlTextParser xmlTextParser = this.InnerReader as IXmlTextParser;
				if (xmlTextParser == null)
				{
					this.xmlReader.Normalized = value;
					return;
				}
				xmlTextParser.Normalized = value;
			}
		}

		// Token: 0x170003F3 RID: 1011
		// (get) Token: 0x06001185 RID: 4485 RVA: 0x00044CE0 File Offset: 0x00042EE0
		// (set) Token: 0x06001186 RID: 4486 RVA: 0x00044D10 File Offset: 0x00042F10
		WhitespaceHandling IXmlTextParser.WhitespaceHandling
		{
			get
			{
				IXmlTextParser xmlTextParser = this.InnerReader as IXmlTextParser;
				if (xmlTextParser != null)
				{
					return xmlTextParser.WhitespaceHandling;
				}
				return this.xmlReader.WhitespaceHandling;
			}
			set
			{
				IXmlTextParser xmlTextParser = this.InnerReader as IXmlTextParser;
				if (xmlTextParser == null)
				{
					this.xmlReader.WhitespaceHandling = value;
					return;
				}
				xmlTextParser.WhitespaceHandling = value;
			}
		}

		// Token: 0x06001187 RID: 4487 RVA: 0x00044D40 File Offset: 0x00042F40
		bool IXmlLineInfo.HasLineInfo()
		{
			IXmlLineInfo xmlLineInfo = this.InnerReader as IXmlLineInfo;
			if (xmlLineInfo != null)
			{
				return xmlLineInfo.HasLineInfo();
			}
			return this.xmlReader.HasLineInfo();
		}

		// Token: 0x170003F4 RID: 1012
		// (get) Token: 0x06001188 RID: 4488 RVA: 0x00044D70 File Offset: 0x00042F70
		int IXmlLineInfo.LineNumber
		{
			get
			{
				IXmlLineInfo xmlLineInfo = this.InnerReader as IXmlLineInfo;
				if (xmlLineInfo != null)
				{
					return xmlLineInfo.LineNumber;
				}
				return this.xmlReader.LineNumber;
			}
		}

		// Token: 0x170003F5 RID: 1013
		// (get) Token: 0x06001189 RID: 4489 RVA: 0x00044DA0 File Offset: 0x00042FA0
		int IXmlLineInfo.LinePosition
		{
			get
			{
				IXmlLineInfo xmlLineInfo = this.InnerReader as IXmlLineInfo;
				if (xmlLineInfo != null)
				{
					return xmlLineInfo.LinePosition;
				}
				return this.xmlReader.LinePosition;
			}
		}

		// Token: 0x0600118A RID: 4490 RVA: 0x0001822D File Offset: 0x0001642D
		public XmlSerializableReader()
		{
		}

		// Token: 0x04000728 RID: 1832
		private XmlReaderDelegator xmlReader;

		// Token: 0x04000729 RID: 1833
		private int startDepth;

		// Token: 0x0400072A RID: 1834
		private bool isRootEmptyElement;

		// Token: 0x0400072B RID: 1835
		private XmlReader innerReader;
	}
}

using System;
using System.Xml.Schema;

namespace System.Xml.Serialization
{
	// Token: 0x020002CF RID: 719
	internal class XmlCountingReader : XmlReader, IXmlTextParser, IXmlLineInfo
	{
		// Token: 0x06001B74 RID: 7028 RVA: 0x0009DDBF File Offset: 0x0009BFBF
		internal XmlCountingReader(XmlReader xmlReader)
		{
			if (xmlReader == null)
			{
				throw new ArgumentNullException("xmlReader");
			}
			this.innerReader = xmlReader;
			this.advanceCount = 0;
		}

		// Token: 0x17000561 RID: 1377
		// (get) Token: 0x06001B75 RID: 7029 RVA: 0x0009DDE3 File Offset: 0x0009BFE3
		internal int AdvanceCount
		{
			get
			{
				return this.advanceCount;
			}
		}

		// Token: 0x06001B76 RID: 7030 RVA: 0x0009DDEB File Offset: 0x0009BFEB
		private void IncrementCount()
		{
			if (this.advanceCount == 2147483647)
			{
				this.advanceCount = 0;
				return;
			}
			this.advanceCount++;
		}

		// Token: 0x17000562 RID: 1378
		// (get) Token: 0x06001B77 RID: 7031 RVA: 0x0009DE10 File Offset: 0x0009C010
		public override XmlReaderSettings Settings
		{
			get
			{
				return this.innerReader.Settings;
			}
		}

		// Token: 0x17000563 RID: 1379
		// (get) Token: 0x06001B78 RID: 7032 RVA: 0x0009DE1D File Offset: 0x0009C01D
		public override XmlNodeType NodeType
		{
			get
			{
				return this.innerReader.NodeType;
			}
		}

		// Token: 0x17000564 RID: 1380
		// (get) Token: 0x06001B79 RID: 7033 RVA: 0x0009DE2A File Offset: 0x0009C02A
		public override string Name
		{
			get
			{
				return this.innerReader.Name;
			}
		}

		// Token: 0x17000565 RID: 1381
		// (get) Token: 0x06001B7A RID: 7034 RVA: 0x0009DE37 File Offset: 0x0009C037
		public override string LocalName
		{
			get
			{
				return this.innerReader.LocalName;
			}
		}

		// Token: 0x17000566 RID: 1382
		// (get) Token: 0x06001B7B RID: 7035 RVA: 0x0009DE44 File Offset: 0x0009C044
		public override string NamespaceURI
		{
			get
			{
				return this.innerReader.NamespaceURI;
			}
		}

		// Token: 0x17000567 RID: 1383
		// (get) Token: 0x06001B7C RID: 7036 RVA: 0x0009DE51 File Offset: 0x0009C051
		public override string Prefix
		{
			get
			{
				return this.innerReader.Prefix;
			}
		}

		// Token: 0x17000568 RID: 1384
		// (get) Token: 0x06001B7D RID: 7037 RVA: 0x0009DE5E File Offset: 0x0009C05E
		public override bool HasValue
		{
			get
			{
				return this.innerReader.HasValue;
			}
		}

		// Token: 0x17000569 RID: 1385
		// (get) Token: 0x06001B7E RID: 7038 RVA: 0x0009DE6B File Offset: 0x0009C06B
		public override string Value
		{
			get
			{
				return this.innerReader.Value;
			}
		}

		// Token: 0x1700056A RID: 1386
		// (get) Token: 0x06001B7F RID: 7039 RVA: 0x0009DE78 File Offset: 0x0009C078
		public override int Depth
		{
			get
			{
				return this.innerReader.Depth;
			}
		}

		// Token: 0x1700056B RID: 1387
		// (get) Token: 0x06001B80 RID: 7040 RVA: 0x0009DE85 File Offset: 0x0009C085
		public override string BaseURI
		{
			get
			{
				return this.innerReader.BaseURI;
			}
		}

		// Token: 0x1700056C RID: 1388
		// (get) Token: 0x06001B81 RID: 7041 RVA: 0x0009DE92 File Offset: 0x0009C092
		public override bool IsEmptyElement
		{
			get
			{
				return this.innerReader.IsEmptyElement;
			}
		}

		// Token: 0x1700056D RID: 1389
		// (get) Token: 0x06001B82 RID: 7042 RVA: 0x0009DE9F File Offset: 0x0009C09F
		public override bool IsDefault
		{
			get
			{
				return this.innerReader.IsDefault;
			}
		}

		// Token: 0x1700056E RID: 1390
		// (get) Token: 0x06001B83 RID: 7043 RVA: 0x0009DEAC File Offset: 0x0009C0AC
		public override char QuoteChar
		{
			get
			{
				return this.innerReader.QuoteChar;
			}
		}

		// Token: 0x1700056F RID: 1391
		// (get) Token: 0x06001B84 RID: 7044 RVA: 0x0009DEB9 File Offset: 0x0009C0B9
		public override XmlSpace XmlSpace
		{
			get
			{
				return this.innerReader.XmlSpace;
			}
		}

		// Token: 0x17000570 RID: 1392
		// (get) Token: 0x06001B85 RID: 7045 RVA: 0x0009DEC6 File Offset: 0x0009C0C6
		public override string XmlLang
		{
			get
			{
				return this.innerReader.XmlLang;
			}
		}

		// Token: 0x17000571 RID: 1393
		// (get) Token: 0x06001B86 RID: 7046 RVA: 0x0009DED3 File Offset: 0x0009C0D3
		public override IXmlSchemaInfo SchemaInfo
		{
			get
			{
				return this.innerReader.SchemaInfo;
			}
		}

		// Token: 0x17000572 RID: 1394
		// (get) Token: 0x06001B87 RID: 7047 RVA: 0x0009DEE0 File Offset: 0x0009C0E0
		public override Type ValueType
		{
			get
			{
				return this.innerReader.ValueType;
			}
		}

		// Token: 0x17000573 RID: 1395
		// (get) Token: 0x06001B88 RID: 7048 RVA: 0x0009DEED File Offset: 0x0009C0ED
		public override int AttributeCount
		{
			get
			{
				return this.innerReader.AttributeCount;
			}
		}

		// Token: 0x17000574 RID: 1396
		public override string this[int i]
		{
			get
			{
				return this.innerReader[i];
			}
		}

		// Token: 0x17000575 RID: 1397
		public override string this[string name]
		{
			get
			{
				return this.innerReader[name];
			}
		}

		// Token: 0x17000576 RID: 1398
		public override string this[string name, string namespaceURI]
		{
			get
			{
				return this.innerReader[name, namespaceURI];
			}
		}

		// Token: 0x17000577 RID: 1399
		// (get) Token: 0x06001B8C RID: 7052 RVA: 0x0009DF25 File Offset: 0x0009C125
		public override bool EOF
		{
			get
			{
				return this.innerReader.EOF;
			}
		}

		// Token: 0x17000578 RID: 1400
		// (get) Token: 0x06001B8D RID: 7053 RVA: 0x0009DF32 File Offset: 0x0009C132
		public override ReadState ReadState
		{
			get
			{
				return this.innerReader.ReadState;
			}
		}

		// Token: 0x17000579 RID: 1401
		// (get) Token: 0x06001B8E RID: 7054 RVA: 0x0009DF3F File Offset: 0x0009C13F
		public override XmlNameTable NameTable
		{
			get
			{
				return this.innerReader.NameTable;
			}
		}

		// Token: 0x1700057A RID: 1402
		// (get) Token: 0x06001B8F RID: 7055 RVA: 0x0009DF4C File Offset: 0x0009C14C
		public override bool CanResolveEntity
		{
			get
			{
				return this.innerReader.CanResolveEntity;
			}
		}

		// Token: 0x1700057B RID: 1403
		// (get) Token: 0x06001B90 RID: 7056 RVA: 0x0009DF59 File Offset: 0x0009C159
		public override bool CanReadBinaryContent
		{
			get
			{
				return this.innerReader.CanReadBinaryContent;
			}
		}

		// Token: 0x1700057C RID: 1404
		// (get) Token: 0x06001B91 RID: 7057 RVA: 0x0009DF66 File Offset: 0x0009C166
		public override bool CanReadValueChunk
		{
			get
			{
				return this.innerReader.CanReadValueChunk;
			}
		}

		// Token: 0x1700057D RID: 1405
		// (get) Token: 0x06001B92 RID: 7058 RVA: 0x0009DF73 File Offset: 0x0009C173
		public override bool HasAttributes
		{
			get
			{
				return this.innerReader.HasAttributes;
			}
		}

		// Token: 0x06001B93 RID: 7059 RVA: 0x0009DF80 File Offset: 0x0009C180
		public override void Close()
		{
			this.innerReader.Close();
		}

		// Token: 0x06001B94 RID: 7060 RVA: 0x0009DF8D File Offset: 0x0009C18D
		public override string GetAttribute(string name)
		{
			return this.innerReader.GetAttribute(name);
		}

		// Token: 0x06001B95 RID: 7061 RVA: 0x0009DF9B File Offset: 0x0009C19B
		public override string GetAttribute(string name, string namespaceURI)
		{
			return this.innerReader.GetAttribute(name, namespaceURI);
		}

		// Token: 0x06001B96 RID: 7062 RVA: 0x0009DFAA File Offset: 0x0009C1AA
		public override string GetAttribute(int i)
		{
			return this.innerReader.GetAttribute(i);
		}

		// Token: 0x06001B97 RID: 7063 RVA: 0x0009DFB8 File Offset: 0x0009C1B8
		public override bool MoveToAttribute(string name)
		{
			return this.innerReader.MoveToAttribute(name);
		}

		// Token: 0x06001B98 RID: 7064 RVA: 0x0009DFC6 File Offset: 0x0009C1C6
		public override bool MoveToAttribute(string name, string ns)
		{
			return this.innerReader.MoveToAttribute(name, ns);
		}

		// Token: 0x06001B99 RID: 7065 RVA: 0x0009DFD5 File Offset: 0x0009C1D5
		public override void MoveToAttribute(int i)
		{
			this.innerReader.MoveToAttribute(i);
		}

		// Token: 0x06001B9A RID: 7066 RVA: 0x0009DFE3 File Offset: 0x0009C1E3
		public override bool MoveToFirstAttribute()
		{
			return this.innerReader.MoveToFirstAttribute();
		}

		// Token: 0x06001B9B RID: 7067 RVA: 0x0009DFF0 File Offset: 0x0009C1F0
		public override bool MoveToNextAttribute()
		{
			return this.innerReader.MoveToNextAttribute();
		}

		// Token: 0x06001B9C RID: 7068 RVA: 0x0009DFFD File Offset: 0x0009C1FD
		public override bool MoveToElement()
		{
			return this.innerReader.MoveToElement();
		}

		// Token: 0x06001B9D RID: 7069 RVA: 0x0009E00A File Offset: 0x0009C20A
		public override string LookupNamespace(string prefix)
		{
			return this.innerReader.LookupNamespace(prefix);
		}

		// Token: 0x06001B9E RID: 7070 RVA: 0x0009E018 File Offset: 0x0009C218
		public override bool ReadAttributeValue()
		{
			return this.innerReader.ReadAttributeValue();
		}

		// Token: 0x06001B9F RID: 7071 RVA: 0x0009E025 File Offset: 0x0009C225
		public override void ResolveEntity()
		{
			this.innerReader.ResolveEntity();
		}

		// Token: 0x06001BA0 RID: 7072 RVA: 0x0009E032 File Offset: 0x0009C232
		public override bool IsStartElement()
		{
			return this.innerReader.IsStartElement();
		}

		// Token: 0x06001BA1 RID: 7073 RVA: 0x0009E03F File Offset: 0x0009C23F
		public override bool IsStartElement(string name)
		{
			return this.innerReader.IsStartElement(name);
		}

		// Token: 0x06001BA2 RID: 7074 RVA: 0x0009E04D File Offset: 0x0009C24D
		public override bool IsStartElement(string localname, string ns)
		{
			return this.innerReader.IsStartElement(localname, ns);
		}

		// Token: 0x06001BA3 RID: 7075 RVA: 0x0009E05C File Offset: 0x0009C25C
		public override XmlReader ReadSubtree()
		{
			return this.innerReader.ReadSubtree();
		}

		// Token: 0x06001BA4 RID: 7076 RVA: 0x0009E069 File Offset: 0x0009C269
		public override XmlNodeType MoveToContent()
		{
			return this.innerReader.MoveToContent();
		}

		// Token: 0x06001BA5 RID: 7077 RVA: 0x0009E076 File Offset: 0x0009C276
		public override bool Read()
		{
			this.IncrementCount();
			return this.innerReader.Read();
		}

		// Token: 0x06001BA6 RID: 7078 RVA: 0x0009E089 File Offset: 0x0009C289
		public override void Skip()
		{
			this.IncrementCount();
			this.innerReader.Skip();
		}

		// Token: 0x06001BA7 RID: 7079 RVA: 0x0009E09C File Offset: 0x0009C29C
		public override string ReadInnerXml()
		{
			if (this.innerReader.NodeType != XmlNodeType.Attribute)
			{
				this.IncrementCount();
			}
			return this.innerReader.ReadInnerXml();
		}

		// Token: 0x06001BA8 RID: 7080 RVA: 0x0009E0BD File Offset: 0x0009C2BD
		public override string ReadOuterXml()
		{
			if (this.innerReader.NodeType != XmlNodeType.Attribute)
			{
				this.IncrementCount();
			}
			return this.innerReader.ReadOuterXml();
		}

		// Token: 0x06001BA9 RID: 7081 RVA: 0x0009E0DE File Offset: 0x0009C2DE
		public override object ReadContentAsObject()
		{
			this.IncrementCount();
			return this.innerReader.ReadContentAsObject();
		}

		// Token: 0x06001BAA RID: 7082 RVA: 0x0009E0F1 File Offset: 0x0009C2F1
		public override bool ReadContentAsBoolean()
		{
			this.IncrementCount();
			return this.innerReader.ReadContentAsBoolean();
		}

		// Token: 0x06001BAB RID: 7083 RVA: 0x0009E104 File Offset: 0x0009C304
		public override DateTime ReadContentAsDateTime()
		{
			this.IncrementCount();
			return this.innerReader.ReadContentAsDateTime();
		}

		// Token: 0x06001BAC RID: 7084 RVA: 0x0009E117 File Offset: 0x0009C317
		public override double ReadContentAsDouble()
		{
			this.IncrementCount();
			return this.innerReader.ReadContentAsDouble();
		}

		// Token: 0x06001BAD RID: 7085 RVA: 0x0009E12A File Offset: 0x0009C32A
		public override int ReadContentAsInt()
		{
			this.IncrementCount();
			return this.innerReader.ReadContentAsInt();
		}

		// Token: 0x06001BAE RID: 7086 RVA: 0x0009E13D File Offset: 0x0009C33D
		public override long ReadContentAsLong()
		{
			this.IncrementCount();
			return this.innerReader.ReadContentAsLong();
		}

		// Token: 0x06001BAF RID: 7087 RVA: 0x0009E150 File Offset: 0x0009C350
		public override string ReadContentAsString()
		{
			this.IncrementCount();
			return this.innerReader.ReadContentAsString();
		}

		// Token: 0x06001BB0 RID: 7088 RVA: 0x0009E163 File Offset: 0x0009C363
		public override object ReadContentAs(Type returnType, IXmlNamespaceResolver namespaceResolver)
		{
			this.IncrementCount();
			return this.innerReader.ReadContentAs(returnType, namespaceResolver);
		}

		// Token: 0x06001BB1 RID: 7089 RVA: 0x0009E178 File Offset: 0x0009C378
		public override object ReadElementContentAsObject()
		{
			this.IncrementCount();
			return this.innerReader.ReadElementContentAsObject();
		}

		// Token: 0x06001BB2 RID: 7090 RVA: 0x0009E18B File Offset: 0x0009C38B
		public override object ReadElementContentAsObject(string localName, string namespaceURI)
		{
			this.IncrementCount();
			return this.innerReader.ReadElementContentAsObject(localName, namespaceURI);
		}

		// Token: 0x06001BB3 RID: 7091 RVA: 0x0009E1A0 File Offset: 0x0009C3A0
		public override bool ReadElementContentAsBoolean()
		{
			this.IncrementCount();
			return this.innerReader.ReadElementContentAsBoolean();
		}

		// Token: 0x06001BB4 RID: 7092 RVA: 0x0009E1B3 File Offset: 0x0009C3B3
		public override bool ReadElementContentAsBoolean(string localName, string namespaceURI)
		{
			this.IncrementCount();
			return this.innerReader.ReadElementContentAsBoolean(localName, namespaceURI);
		}

		// Token: 0x06001BB5 RID: 7093 RVA: 0x0009E1C8 File Offset: 0x0009C3C8
		public override DateTime ReadElementContentAsDateTime()
		{
			this.IncrementCount();
			return this.innerReader.ReadElementContentAsDateTime();
		}

		// Token: 0x06001BB6 RID: 7094 RVA: 0x0009E1DB File Offset: 0x0009C3DB
		public override DateTime ReadElementContentAsDateTime(string localName, string namespaceURI)
		{
			this.IncrementCount();
			return this.innerReader.ReadElementContentAsDateTime(localName, namespaceURI);
		}

		// Token: 0x06001BB7 RID: 7095 RVA: 0x0009E1F0 File Offset: 0x0009C3F0
		public override double ReadElementContentAsDouble()
		{
			this.IncrementCount();
			return this.innerReader.ReadElementContentAsDouble();
		}

		// Token: 0x06001BB8 RID: 7096 RVA: 0x0009E203 File Offset: 0x0009C403
		public override double ReadElementContentAsDouble(string localName, string namespaceURI)
		{
			this.IncrementCount();
			return this.innerReader.ReadElementContentAsDouble(localName, namespaceURI);
		}

		// Token: 0x06001BB9 RID: 7097 RVA: 0x0009E218 File Offset: 0x0009C418
		public override int ReadElementContentAsInt()
		{
			this.IncrementCount();
			return this.innerReader.ReadElementContentAsInt();
		}

		// Token: 0x06001BBA RID: 7098 RVA: 0x0009E22B File Offset: 0x0009C42B
		public override int ReadElementContentAsInt(string localName, string namespaceURI)
		{
			this.IncrementCount();
			return this.innerReader.ReadElementContentAsInt(localName, namespaceURI);
		}

		// Token: 0x06001BBB RID: 7099 RVA: 0x0009E240 File Offset: 0x0009C440
		public override long ReadElementContentAsLong()
		{
			this.IncrementCount();
			return this.innerReader.ReadElementContentAsLong();
		}

		// Token: 0x06001BBC RID: 7100 RVA: 0x0009E253 File Offset: 0x0009C453
		public override long ReadElementContentAsLong(string localName, string namespaceURI)
		{
			this.IncrementCount();
			return this.innerReader.ReadElementContentAsLong(localName, namespaceURI);
		}

		// Token: 0x06001BBD RID: 7101 RVA: 0x0009E268 File Offset: 0x0009C468
		public override string ReadElementContentAsString()
		{
			this.IncrementCount();
			return this.innerReader.ReadElementContentAsString();
		}

		// Token: 0x06001BBE RID: 7102 RVA: 0x0009E27B File Offset: 0x0009C47B
		public override string ReadElementContentAsString(string localName, string namespaceURI)
		{
			this.IncrementCount();
			return this.innerReader.ReadElementContentAsString(localName, namespaceURI);
		}

		// Token: 0x06001BBF RID: 7103 RVA: 0x0009E290 File Offset: 0x0009C490
		public override object ReadElementContentAs(Type returnType, IXmlNamespaceResolver namespaceResolver)
		{
			this.IncrementCount();
			return this.innerReader.ReadElementContentAs(returnType, namespaceResolver);
		}

		// Token: 0x06001BC0 RID: 7104 RVA: 0x0009E2A5 File Offset: 0x0009C4A5
		public override object ReadElementContentAs(Type returnType, IXmlNamespaceResolver namespaceResolver, string localName, string namespaceURI)
		{
			this.IncrementCount();
			return this.innerReader.ReadElementContentAs(returnType, namespaceResolver, localName, namespaceURI);
		}

		// Token: 0x06001BC1 RID: 7105 RVA: 0x0009E2BD File Offset: 0x0009C4BD
		public override int ReadContentAsBase64(byte[] buffer, int index, int count)
		{
			this.IncrementCount();
			return this.innerReader.ReadContentAsBase64(buffer, index, count);
		}

		// Token: 0x06001BC2 RID: 7106 RVA: 0x0009E2D3 File Offset: 0x0009C4D3
		public override int ReadElementContentAsBase64(byte[] buffer, int index, int count)
		{
			this.IncrementCount();
			return this.innerReader.ReadElementContentAsBase64(buffer, index, count);
		}

		// Token: 0x06001BC3 RID: 7107 RVA: 0x0009E2E9 File Offset: 0x0009C4E9
		public override int ReadContentAsBinHex(byte[] buffer, int index, int count)
		{
			this.IncrementCount();
			return this.innerReader.ReadContentAsBinHex(buffer, index, count);
		}

		// Token: 0x06001BC4 RID: 7108 RVA: 0x0009E2FF File Offset: 0x0009C4FF
		public override int ReadElementContentAsBinHex(byte[] buffer, int index, int count)
		{
			this.IncrementCount();
			return this.innerReader.ReadElementContentAsBinHex(buffer, index, count);
		}

		// Token: 0x06001BC5 RID: 7109 RVA: 0x0009E315 File Offset: 0x0009C515
		public override int ReadValueChunk(char[] buffer, int index, int count)
		{
			this.IncrementCount();
			return this.innerReader.ReadValueChunk(buffer, index, count);
		}

		// Token: 0x06001BC6 RID: 7110 RVA: 0x0009E32B File Offset: 0x0009C52B
		public override string ReadString()
		{
			this.IncrementCount();
			return this.innerReader.ReadString();
		}

		// Token: 0x06001BC7 RID: 7111 RVA: 0x0009E33E File Offset: 0x0009C53E
		public override void ReadStartElement()
		{
			this.IncrementCount();
			this.innerReader.ReadStartElement();
		}

		// Token: 0x06001BC8 RID: 7112 RVA: 0x0009E351 File Offset: 0x0009C551
		public override void ReadStartElement(string name)
		{
			this.IncrementCount();
			this.innerReader.ReadStartElement(name);
		}

		// Token: 0x06001BC9 RID: 7113 RVA: 0x0009E365 File Offset: 0x0009C565
		public override void ReadStartElement(string localname, string ns)
		{
			this.IncrementCount();
			this.innerReader.ReadStartElement(localname, ns);
		}

		// Token: 0x06001BCA RID: 7114 RVA: 0x0009E37A File Offset: 0x0009C57A
		public override string ReadElementString()
		{
			this.IncrementCount();
			return this.innerReader.ReadElementString();
		}

		// Token: 0x06001BCB RID: 7115 RVA: 0x0009E38D File Offset: 0x0009C58D
		public override string ReadElementString(string name)
		{
			this.IncrementCount();
			return this.innerReader.ReadElementString(name);
		}

		// Token: 0x06001BCC RID: 7116 RVA: 0x0009E3A1 File Offset: 0x0009C5A1
		public override string ReadElementString(string localname, string ns)
		{
			this.IncrementCount();
			return this.innerReader.ReadElementString(localname, ns);
		}

		// Token: 0x06001BCD RID: 7117 RVA: 0x0009E3B6 File Offset: 0x0009C5B6
		public override void ReadEndElement()
		{
			this.IncrementCount();
			this.innerReader.ReadEndElement();
		}

		// Token: 0x06001BCE RID: 7118 RVA: 0x0009E3C9 File Offset: 0x0009C5C9
		public override bool ReadToFollowing(string name)
		{
			this.IncrementCount();
			return this.ReadToFollowing(name);
		}

		// Token: 0x06001BCF RID: 7119 RVA: 0x0009E3D8 File Offset: 0x0009C5D8
		public override bool ReadToFollowing(string localName, string namespaceURI)
		{
			this.IncrementCount();
			return this.innerReader.ReadToFollowing(localName, namespaceURI);
		}

		// Token: 0x06001BD0 RID: 7120 RVA: 0x0009E3ED File Offset: 0x0009C5ED
		public override bool ReadToDescendant(string name)
		{
			this.IncrementCount();
			return this.innerReader.ReadToDescendant(name);
		}

		// Token: 0x06001BD1 RID: 7121 RVA: 0x0009E401 File Offset: 0x0009C601
		public override bool ReadToDescendant(string localName, string namespaceURI)
		{
			this.IncrementCount();
			return this.innerReader.ReadToDescendant(localName, namespaceURI);
		}

		// Token: 0x06001BD2 RID: 7122 RVA: 0x0009E416 File Offset: 0x0009C616
		public override bool ReadToNextSibling(string name)
		{
			this.IncrementCount();
			return this.innerReader.ReadToNextSibling(name);
		}

		// Token: 0x06001BD3 RID: 7123 RVA: 0x0009E42A File Offset: 0x0009C62A
		public override bool ReadToNextSibling(string localName, string namespaceURI)
		{
			this.IncrementCount();
			return this.innerReader.ReadToNextSibling(localName, namespaceURI);
		}

		// Token: 0x06001BD4 RID: 7124 RVA: 0x0009E440 File Offset: 0x0009C640
		protected override void Dispose(bool disposing)
		{
			try
			{
				if (disposing)
				{
					IDisposable disposable = this.innerReader;
					if (disposable != null)
					{
						disposable.Dispose();
					}
				}
			}
			finally
			{
				base.Dispose(disposing);
			}
		}

		// Token: 0x1700057E RID: 1406
		// (get) Token: 0x06001BD5 RID: 7125 RVA: 0x0009E47C File Offset: 0x0009C67C
		// (set) Token: 0x06001BD6 RID: 7126 RVA: 0x0009E4B8 File Offset: 0x0009C6B8
		bool IXmlTextParser.Normalized
		{
			get
			{
				XmlTextReader xmlTextReader = this.innerReader as XmlTextReader;
				if (xmlTextReader == null)
				{
					IXmlTextParser xmlTextParser = this.innerReader as IXmlTextParser;
					return xmlTextParser != null && xmlTextParser.Normalized;
				}
				return xmlTextReader.Normalization;
			}
			set
			{
				XmlTextReader xmlTextReader = this.innerReader as XmlTextReader;
				if (xmlTextReader == null)
				{
					IXmlTextParser xmlTextParser = this.innerReader as IXmlTextParser;
					if (xmlTextParser != null)
					{
						xmlTextParser.Normalized = value;
						return;
					}
				}
				else
				{
					xmlTextReader.Normalization = value;
				}
			}
		}

		// Token: 0x1700057F RID: 1407
		// (get) Token: 0x06001BD7 RID: 7127 RVA: 0x0009E4F4 File Offset: 0x0009C6F4
		// (set) Token: 0x06001BD8 RID: 7128 RVA: 0x0009E530 File Offset: 0x0009C730
		WhitespaceHandling IXmlTextParser.WhitespaceHandling
		{
			get
			{
				XmlTextReader xmlTextReader = this.innerReader as XmlTextReader;
				if (xmlTextReader != null)
				{
					return xmlTextReader.WhitespaceHandling;
				}
				IXmlTextParser xmlTextParser = this.innerReader as IXmlTextParser;
				if (xmlTextParser != null)
				{
					return xmlTextParser.WhitespaceHandling;
				}
				return WhitespaceHandling.None;
			}
			set
			{
				XmlTextReader xmlTextReader = this.innerReader as XmlTextReader;
				if (xmlTextReader == null)
				{
					IXmlTextParser xmlTextParser = this.innerReader as IXmlTextParser;
					if (xmlTextParser != null)
					{
						xmlTextParser.WhitespaceHandling = value;
						return;
					}
				}
				else
				{
					xmlTextReader.WhitespaceHandling = value;
				}
			}
		}

		// Token: 0x06001BD9 RID: 7129 RVA: 0x0009E56C File Offset: 0x0009C76C
		bool IXmlLineInfo.HasLineInfo()
		{
			IXmlLineInfo xmlLineInfo = this.innerReader as IXmlLineInfo;
			return xmlLineInfo != null && xmlLineInfo.HasLineInfo();
		}

		// Token: 0x17000580 RID: 1408
		// (get) Token: 0x06001BDA RID: 7130 RVA: 0x0009E590 File Offset: 0x0009C790
		int IXmlLineInfo.LineNumber
		{
			get
			{
				IXmlLineInfo xmlLineInfo = this.innerReader as IXmlLineInfo;
				if (xmlLineInfo != null)
				{
					return xmlLineInfo.LineNumber;
				}
				return 0;
			}
		}

		// Token: 0x17000581 RID: 1409
		// (get) Token: 0x06001BDB RID: 7131 RVA: 0x0009E5B4 File Offset: 0x0009C7B4
		int IXmlLineInfo.LinePosition
		{
			get
			{
				IXmlLineInfo xmlLineInfo = this.innerReader as IXmlLineInfo;
				if (xmlLineInfo != null)
				{
					return xmlLineInfo.LinePosition;
				}
				return 0;
			}
		}

		// Token: 0x040019E9 RID: 6633
		private XmlReader innerReader;

		// Token: 0x040019EA RID: 6634
		private int advanceCount;
	}
}

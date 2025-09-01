using System;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace System.Xml
{
	// Token: 0x02000061 RID: 97
	internal class XmlAsyncCheckReader : XmlReader
	{
		// Token: 0x17000063 RID: 99
		// (get) Token: 0x060002E5 RID: 741 RVA: 0x000101B7 File Offset: 0x0000E3B7
		internal XmlReader CoreReader
		{
			get
			{
				return this.coreReader;
			}
		}

		// Token: 0x060002E6 RID: 742 RVA: 0x000101C0 File Offset: 0x0000E3C0
		public static XmlAsyncCheckReader CreateAsyncCheckWrapper(XmlReader reader)
		{
			if (reader is IXmlLineInfo)
			{
				if (!(reader is IXmlNamespaceResolver))
				{
					return new XmlAsyncCheckReaderWithLineInfo(reader);
				}
				if (reader is IXmlSchemaInfo)
				{
					return new XmlAsyncCheckReaderWithLineInfoNSSchema(reader);
				}
				return new XmlAsyncCheckReaderWithLineInfoNS(reader);
			}
			else
			{
				if (reader is IXmlNamespaceResolver)
				{
					return new XmlAsyncCheckReaderWithNS(reader);
				}
				return new XmlAsyncCheckReader(reader);
			}
		}

		// Token: 0x060002E7 RID: 743 RVA: 0x0001020F File Offset: 0x0000E40F
		public XmlAsyncCheckReader(XmlReader reader)
		{
			this.coreReader = reader;
		}

		// Token: 0x060002E8 RID: 744 RVA: 0x00010229 File Offset: 0x0000E429
		private void CheckAsync()
		{
			if (!this.lastTask.IsCompleted)
			{
				throw new InvalidOperationException(Res.GetString("An asynchronous operation is already in progress."));
			}
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x060002E9 RID: 745 RVA: 0x00010248 File Offset: 0x0000E448
		public override XmlReaderSettings Settings
		{
			get
			{
				XmlReaderSettings xmlReaderSettings = this.coreReader.Settings;
				if (xmlReaderSettings != null)
				{
					xmlReaderSettings = xmlReaderSettings.Clone();
				}
				else
				{
					xmlReaderSettings = new XmlReaderSettings();
				}
				xmlReaderSettings.Async = true;
				xmlReaderSettings.ReadOnly = true;
				return xmlReaderSettings;
			}
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x060002EA RID: 746 RVA: 0x00010282 File Offset: 0x0000E482
		public override XmlNodeType NodeType
		{
			get
			{
				this.CheckAsync();
				return this.coreReader.NodeType;
			}
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x060002EB RID: 747 RVA: 0x00010295 File Offset: 0x0000E495
		public override string Name
		{
			get
			{
				this.CheckAsync();
				return this.coreReader.Name;
			}
		}

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x060002EC RID: 748 RVA: 0x000102A8 File Offset: 0x0000E4A8
		public override string LocalName
		{
			get
			{
				this.CheckAsync();
				return this.coreReader.LocalName;
			}
		}

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x060002ED RID: 749 RVA: 0x000102BB File Offset: 0x0000E4BB
		public override string NamespaceURI
		{
			get
			{
				this.CheckAsync();
				return this.coreReader.NamespaceURI;
			}
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x060002EE RID: 750 RVA: 0x000102CE File Offset: 0x0000E4CE
		public override string Prefix
		{
			get
			{
				this.CheckAsync();
				return this.coreReader.Prefix;
			}
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x060002EF RID: 751 RVA: 0x000102E1 File Offset: 0x0000E4E1
		public override bool HasValue
		{
			get
			{
				this.CheckAsync();
				return this.coreReader.HasValue;
			}
		}

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x060002F0 RID: 752 RVA: 0x000102F4 File Offset: 0x0000E4F4
		public override string Value
		{
			get
			{
				this.CheckAsync();
				return this.coreReader.Value;
			}
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x060002F1 RID: 753 RVA: 0x00010307 File Offset: 0x0000E507
		public override int Depth
		{
			get
			{
				this.CheckAsync();
				return this.coreReader.Depth;
			}
		}

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x060002F2 RID: 754 RVA: 0x0001031A File Offset: 0x0000E51A
		public override string BaseURI
		{
			get
			{
				this.CheckAsync();
				return this.coreReader.BaseURI;
			}
		}

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x060002F3 RID: 755 RVA: 0x0001032D File Offset: 0x0000E52D
		public override bool IsEmptyElement
		{
			get
			{
				this.CheckAsync();
				return this.coreReader.IsEmptyElement;
			}
		}

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x060002F4 RID: 756 RVA: 0x00010340 File Offset: 0x0000E540
		public override bool IsDefault
		{
			get
			{
				this.CheckAsync();
				return this.coreReader.IsDefault;
			}
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x060002F5 RID: 757 RVA: 0x00010353 File Offset: 0x0000E553
		public override char QuoteChar
		{
			get
			{
				this.CheckAsync();
				return this.coreReader.QuoteChar;
			}
		}

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x060002F6 RID: 758 RVA: 0x00010366 File Offset: 0x0000E566
		public override XmlSpace XmlSpace
		{
			get
			{
				this.CheckAsync();
				return this.coreReader.XmlSpace;
			}
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x060002F7 RID: 759 RVA: 0x00010379 File Offset: 0x0000E579
		public override string XmlLang
		{
			get
			{
				this.CheckAsync();
				return this.coreReader.XmlLang;
			}
		}

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x060002F8 RID: 760 RVA: 0x0001038C File Offset: 0x0000E58C
		public override IXmlSchemaInfo SchemaInfo
		{
			get
			{
				this.CheckAsync();
				return this.coreReader.SchemaInfo;
			}
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x060002F9 RID: 761 RVA: 0x0001039F File Offset: 0x0000E59F
		public override Type ValueType
		{
			get
			{
				this.CheckAsync();
				return this.coreReader.ValueType;
			}
		}

		// Token: 0x060002FA RID: 762 RVA: 0x000103B2 File Offset: 0x0000E5B2
		public override object ReadContentAsObject()
		{
			this.CheckAsync();
			return this.coreReader.ReadContentAsObject();
		}

		// Token: 0x060002FB RID: 763 RVA: 0x000103C5 File Offset: 0x0000E5C5
		public override bool ReadContentAsBoolean()
		{
			this.CheckAsync();
			return this.coreReader.ReadContentAsBoolean();
		}

		// Token: 0x060002FC RID: 764 RVA: 0x000103D8 File Offset: 0x0000E5D8
		public override DateTime ReadContentAsDateTime()
		{
			this.CheckAsync();
			return this.coreReader.ReadContentAsDateTime();
		}

		// Token: 0x060002FD RID: 765 RVA: 0x000103EB File Offset: 0x0000E5EB
		public override double ReadContentAsDouble()
		{
			this.CheckAsync();
			return this.coreReader.ReadContentAsDouble();
		}

		// Token: 0x060002FE RID: 766 RVA: 0x000103FE File Offset: 0x0000E5FE
		public override float ReadContentAsFloat()
		{
			this.CheckAsync();
			return this.coreReader.ReadContentAsFloat();
		}

		// Token: 0x060002FF RID: 767 RVA: 0x00010411 File Offset: 0x0000E611
		public override decimal ReadContentAsDecimal()
		{
			this.CheckAsync();
			return this.coreReader.ReadContentAsDecimal();
		}

		// Token: 0x06000300 RID: 768 RVA: 0x00010424 File Offset: 0x0000E624
		public override int ReadContentAsInt()
		{
			this.CheckAsync();
			return this.coreReader.ReadContentAsInt();
		}

		// Token: 0x06000301 RID: 769 RVA: 0x00010437 File Offset: 0x0000E637
		public override long ReadContentAsLong()
		{
			this.CheckAsync();
			return this.coreReader.ReadContentAsLong();
		}

		// Token: 0x06000302 RID: 770 RVA: 0x0001044A File Offset: 0x0000E64A
		public override string ReadContentAsString()
		{
			this.CheckAsync();
			return this.coreReader.ReadContentAsString();
		}

		// Token: 0x06000303 RID: 771 RVA: 0x0001045D File Offset: 0x0000E65D
		public override object ReadContentAs(Type returnType, IXmlNamespaceResolver namespaceResolver)
		{
			this.CheckAsync();
			return this.coreReader.ReadContentAs(returnType, namespaceResolver);
		}

		// Token: 0x06000304 RID: 772 RVA: 0x00010472 File Offset: 0x0000E672
		public override object ReadElementContentAsObject()
		{
			this.CheckAsync();
			return this.coreReader.ReadElementContentAsObject();
		}

		// Token: 0x06000305 RID: 773 RVA: 0x00010485 File Offset: 0x0000E685
		public override object ReadElementContentAsObject(string localName, string namespaceURI)
		{
			this.CheckAsync();
			return this.coreReader.ReadElementContentAsObject(localName, namespaceURI);
		}

		// Token: 0x06000306 RID: 774 RVA: 0x0001049A File Offset: 0x0000E69A
		public override bool ReadElementContentAsBoolean()
		{
			this.CheckAsync();
			return this.coreReader.ReadElementContentAsBoolean();
		}

		// Token: 0x06000307 RID: 775 RVA: 0x000104AD File Offset: 0x0000E6AD
		public override bool ReadElementContentAsBoolean(string localName, string namespaceURI)
		{
			this.CheckAsync();
			return this.coreReader.ReadElementContentAsBoolean(localName, namespaceURI);
		}

		// Token: 0x06000308 RID: 776 RVA: 0x000104C2 File Offset: 0x0000E6C2
		public override DateTime ReadElementContentAsDateTime()
		{
			this.CheckAsync();
			return this.coreReader.ReadElementContentAsDateTime();
		}

		// Token: 0x06000309 RID: 777 RVA: 0x000104D5 File Offset: 0x0000E6D5
		public override DateTime ReadElementContentAsDateTime(string localName, string namespaceURI)
		{
			this.CheckAsync();
			return this.coreReader.ReadElementContentAsDateTime(localName, namespaceURI);
		}

		// Token: 0x0600030A RID: 778 RVA: 0x000104EA File Offset: 0x0000E6EA
		public override DateTimeOffset ReadContentAsDateTimeOffset()
		{
			this.CheckAsync();
			return this.coreReader.ReadContentAsDateTimeOffset();
		}

		// Token: 0x0600030B RID: 779 RVA: 0x000104FD File Offset: 0x0000E6FD
		public override double ReadElementContentAsDouble()
		{
			this.CheckAsync();
			return this.coreReader.ReadElementContentAsDouble();
		}

		// Token: 0x0600030C RID: 780 RVA: 0x00010510 File Offset: 0x0000E710
		public override double ReadElementContentAsDouble(string localName, string namespaceURI)
		{
			this.CheckAsync();
			return this.coreReader.ReadElementContentAsDouble(localName, namespaceURI);
		}

		// Token: 0x0600030D RID: 781 RVA: 0x00010525 File Offset: 0x0000E725
		public override float ReadElementContentAsFloat()
		{
			this.CheckAsync();
			return this.coreReader.ReadElementContentAsFloat();
		}

		// Token: 0x0600030E RID: 782 RVA: 0x00010538 File Offset: 0x0000E738
		public override float ReadElementContentAsFloat(string localName, string namespaceURI)
		{
			this.CheckAsync();
			return this.coreReader.ReadElementContentAsFloat(localName, namespaceURI);
		}

		// Token: 0x0600030F RID: 783 RVA: 0x0001054D File Offset: 0x0000E74D
		public override decimal ReadElementContentAsDecimal()
		{
			this.CheckAsync();
			return this.coreReader.ReadElementContentAsDecimal();
		}

		// Token: 0x06000310 RID: 784 RVA: 0x00010560 File Offset: 0x0000E760
		public override decimal ReadElementContentAsDecimal(string localName, string namespaceURI)
		{
			this.CheckAsync();
			return this.coreReader.ReadElementContentAsDecimal(localName, namespaceURI);
		}

		// Token: 0x06000311 RID: 785 RVA: 0x00010575 File Offset: 0x0000E775
		public override int ReadElementContentAsInt()
		{
			this.CheckAsync();
			return this.coreReader.ReadElementContentAsInt();
		}

		// Token: 0x06000312 RID: 786 RVA: 0x00010588 File Offset: 0x0000E788
		public override int ReadElementContentAsInt(string localName, string namespaceURI)
		{
			this.CheckAsync();
			return this.coreReader.ReadElementContentAsInt(localName, namespaceURI);
		}

		// Token: 0x06000313 RID: 787 RVA: 0x0001059D File Offset: 0x0000E79D
		public override long ReadElementContentAsLong()
		{
			this.CheckAsync();
			return this.coreReader.ReadElementContentAsLong();
		}

		// Token: 0x06000314 RID: 788 RVA: 0x000105B0 File Offset: 0x0000E7B0
		public override long ReadElementContentAsLong(string localName, string namespaceURI)
		{
			this.CheckAsync();
			return this.coreReader.ReadElementContentAsLong(localName, namespaceURI);
		}

		// Token: 0x06000315 RID: 789 RVA: 0x000105C5 File Offset: 0x0000E7C5
		public override string ReadElementContentAsString()
		{
			this.CheckAsync();
			return this.coreReader.ReadElementContentAsString();
		}

		// Token: 0x06000316 RID: 790 RVA: 0x000105D8 File Offset: 0x0000E7D8
		public override string ReadElementContentAsString(string localName, string namespaceURI)
		{
			this.CheckAsync();
			return this.coreReader.ReadElementContentAsString(localName, namespaceURI);
		}

		// Token: 0x06000317 RID: 791 RVA: 0x000105ED File Offset: 0x0000E7ED
		public override object ReadElementContentAs(Type returnType, IXmlNamespaceResolver namespaceResolver)
		{
			this.CheckAsync();
			return this.coreReader.ReadElementContentAs(returnType, namespaceResolver);
		}

		// Token: 0x06000318 RID: 792 RVA: 0x00010602 File Offset: 0x0000E802
		public override object ReadElementContentAs(Type returnType, IXmlNamespaceResolver namespaceResolver, string localName, string namespaceURI)
		{
			this.CheckAsync();
			return this.coreReader.ReadElementContentAs(returnType, namespaceResolver, localName, namespaceURI);
		}

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x06000319 RID: 793 RVA: 0x0001061A File Offset: 0x0000E81A
		public override int AttributeCount
		{
			get
			{
				this.CheckAsync();
				return this.coreReader.AttributeCount;
			}
		}

		// Token: 0x0600031A RID: 794 RVA: 0x0001062D File Offset: 0x0000E82D
		public override string GetAttribute(string name)
		{
			this.CheckAsync();
			return this.coreReader.GetAttribute(name);
		}

		// Token: 0x0600031B RID: 795 RVA: 0x00010641 File Offset: 0x0000E841
		public override string GetAttribute(string name, string namespaceURI)
		{
			this.CheckAsync();
			return this.coreReader.GetAttribute(name, namespaceURI);
		}

		// Token: 0x0600031C RID: 796 RVA: 0x00010656 File Offset: 0x0000E856
		public override string GetAttribute(int i)
		{
			this.CheckAsync();
			return this.coreReader.GetAttribute(i);
		}

		// Token: 0x17000076 RID: 118
		public override string this[int i]
		{
			get
			{
				this.CheckAsync();
				return this.coreReader[i];
			}
		}

		// Token: 0x17000077 RID: 119
		public override string this[string name]
		{
			get
			{
				this.CheckAsync();
				return this.coreReader[name];
			}
		}

		// Token: 0x17000078 RID: 120
		public override string this[string name, string namespaceURI]
		{
			get
			{
				this.CheckAsync();
				return this.coreReader[name, namespaceURI];
			}
		}

		// Token: 0x06000320 RID: 800 RVA: 0x000106A7 File Offset: 0x0000E8A7
		public override bool MoveToAttribute(string name)
		{
			this.CheckAsync();
			return this.coreReader.MoveToAttribute(name);
		}

		// Token: 0x06000321 RID: 801 RVA: 0x000106BB File Offset: 0x0000E8BB
		public override bool MoveToAttribute(string name, string ns)
		{
			this.CheckAsync();
			return this.coreReader.MoveToAttribute(name, ns);
		}

		// Token: 0x06000322 RID: 802 RVA: 0x000106D0 File Offset: 0x0000E8D0
		public override void MoveToAttribute(int i)
		{
			this.CheckAsync();
			this.coreReader.MoveToAttribute(i);
		}

		// Token: 0x06000323 RID: 803 RVA: 0x000106E4 File Offset: 0x0000E8E4
		public override bool MoveToFirstAttribute()
		{
			this.CheckAsync();
			return this.coreReader.MoveToFirstAttribute();
		}

		// Token: 0x06000324 RID: 804 RVA: 0x000106F7 File Offset: 0x0000E8F7
		public override bool MoveToNextAttribute()
		{
			this.CheckAsync();
			return this.coreReader.MoveToNextAttribute();
		}

		// Token: 0x06000325 RID: 805 RVA: 0x0001070A File Offset: 0x0000E90A
		public override bool MoveToElement()
		{
			this.CheckAsync();
			return this.coreReader.MoveToElement();
		}

		// Token: 0x06000326 RID: 806 RVA: 0x0001071D File Offset: 0x0000E91D
		public override bool ReadAttributeValue()
		{
			this.CheckAsync();
			return this.coreReader.ReadAttributeValue();
		}

		// Token: 0x06000327 RID: 807 RVA: 0x00010730 File Offset: 0x0000E930
		public override bool Read()
		{
			this.CheckAsync();
			return this.coreReader.Read();
		}

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x06000328 RID: 808 RVA: 0x00010743 File Offset: 0x0000E943
		public override bool EOF
		{
			get
			{
				this.CheckAsync();
				return this.coreReader.EOF;
			}
		}

		// Token: 0x06000329 RID: 809 RVA: 0x00010756 File Offset: 0x0000E956
		public override void Close()
		{
			this.CheckAsync();
			this.coreReader.Close();
		}

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x0600032A RID: 810 RVA: 0x00010769 File Offset: 0x0000E969
		public override ReadState ReadState
		{
			get
			{
				this.CheckAsync();
				return this.coreReader.ReadState;
			}
		}

		// Token: 0x0600032B RID: 811 RVA: 0x0001077C File Offset: 0x0000E97C
		public override void Skip()
		{
			this.CheckAsync();
			this.coreReader.Skip();
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x0600032C RID: 812 RVA: 0x0001078F File Offset: 0x0000E98F
		public override XmlNameTable NameTable
		{
			get
			{
				this.CheckAsync();
				return this.coreReader.NameTable;
			}
		}

		// Token: 0x0600032D RID: 813 RVA: 0x000107A2 File Offset: 0x0000E9A2
		public override string LookupNamespace(string prefix)
		{
			this.CheckAsync();
			return this.coreReader.LookupNamespace(prefix);
		}

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x0600032E RID: 814 RVA: 0x000107B6 File Offset: 0x0000E9B6
		public override bool CanResolveEntity
		{
			get
			{
				this.CheckAsync();
				return this.coreReader.CanResolveEntity;
			}
		}

		// Token: 0x0600032F RID: 815 RVA: 0x000107C9 File Offset: 0x0000E9C9
		public override void ResolveEntity()
		{
			this.CheckAsync();
			this.coreReader.ResolveEntity();
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x06000330 RID: 816 RVA: 0x000107DC File Offset: 0x0000E9DC
		public override bool CanReadBinaryContent
		{
			get
			{
				this.CheckAsync();
				return this.coreReader.CanReadBinaryContent;
			}
		}

		// Token: 0x06000331 RID: 817 RVA: 0x000107EF File Offset: 0x0000E9EF
		public override int ReadContentAsBase64(byte[] buffer, int index, int count)
		{
			this.CheckAsync();
			return this.coreReader.ReadContentAsBase64(buffer, index, count);
		}

		// Token: 0x06000332 RID: 818 RVA: 0x00010805 File Offset: 0x0000EA05
		public override int ReadElementContentAsBase64(byte[] buffer, int index, int count)
		{
			this.CheckAsync();
			return this.coreReader.ReadElementContentAsBase64(buffer, index, count);
		}

		// Token: 0x06000333 RID: 819 RVA: 0x0001081B File Offset: 0x0000EA1B
		public override int ReadContentAsBinHex(byte[] buffer, int index, int count)
		{
			this.CheckAsync();
			return this.coreReader.ReadContentAsBinHex(buffer, index, count);
		}

		// Token: 0x06000334 RID: 820 RVA: 0x00010831 File Offset: 0x0000EA31
		public override int ReadElementContentAsBinHex(byte[] buffer, int index, int count)
		{
			this.CheckAsync();
			return this.coreReader.ReadElementContentAsBinHex(buffer, index, count);
		}

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x06000335 RID: 821 RVA: 0x00010847 File Offset: 0x0000EA47
		public override bool CanReadValueChunk
		{
			get
			{
				this.CheckAsync();
				return this.coreReader.CanReadValueChunk;
			}
		}

		// Token: 0x06000336 RID: 822 RVA: 0x0001085A File Offset: 0x0000EA5A
		public override int ReadValueChunk(char[] buffer, int index, int count)
		{
			this.CheckAsync();
			return this.coreReader.ReadValueChunk(buffer, index, count);
		}

		// Token: 0x06000337 RID: 823 RVA: 0x00010870 File Offset: 0x0000EA70
		public override string ReadString()
		{
			this.CheckAsync();
			return this.coreReader.ReadString();
		}

		// Token: 0x06000338 RID: 824 RVA: 0x00010883 File Offset: 0x0000EA83
		public override XmlNodeType MoveToContent()
		{
			this.CheckAsync();
			return this.coreReader.MoveToContent();
		}

		// Token: 0x06000339 RID: 825 RVA: 0x00010896 File Offset: 0x0000EA96
		public override void ReadStartElement()
		{
			this.CheckAsync();
			this.coreReader.ReadStartElement();
		}

		// Token: 0x0600033A RID: 826 RVA: 0x000108A9 File Offset: 0x0000EAA9
		public override void ReadStartElement(string name)
		{
			this.CheckAsync();
			this.coreReader.ReadStartElement(name);
		}

		// Token: 0x0600033B RID: 827 RVA: 0x000108BD File Offset: 0x0000EABD
		public override void ReadStartElement(string localname, string ns)
		{
			this.CheckAsync();
			this.coreReader.ReadStartElement(localname, ns);
		}

		// Token: 0x0600033C RID: 828 RVA: 0x000108D2 File Offset: 0x0000EAD2
		public override string ReadElementString()
		{
			this.CheckAsync();
			return this.coreReader.ReadElementString();
		}

		// Token: 0x0600033D RID: 829 RVA: 0x000108E5 File Offset: 0x0000EAE5
		public override string ReadElementString(string name)
		{
			this.CheckAsync();
			return this.coreReader.ReadElementString(name);
		}

		// Token: 0x0600033E RID: 830 RVA: 0x000108F9 File Offset: 0x0000EAF9
		public override string ReadElementString(string localname, string ns)
		{
			this.CheckAsync();
			return this.coreReader.ReadElementString(localname, ns);
		}

		// Token: 0x0600033F RID: 831 RVA: 0x0001090E File Offset: 0x0000EB0E
		public override void ReadEndElement()
		{
			this.CheckAsync();
			this.coreReader.ReadEndElement();
		}

		// Token: 0x06000340 RID: 832 RVA: 0x00010921 File Offset: 0x0000EB21
		public override bool IsStartElement()
		{
			this.CheckAsync();
			return this.coreReader.IsStartElement();
		}

		// Token: 0x06000341 RID: 833 RVA: 0x00010934 File Offset: 0x0000EB34
		public override bool IsStartElement(string name)
		{
			this.CheckAsync();
			return this.coreReader.IsStartElement(name);
		}

		// Token: 0x06000342 RID: 834 RVA: 0x00010948 File Offset: 0x0000EB48
		public override bool IsStartElement(string localname, string ns)
		{
			this.CheckAsync();
			return this.coreReader.IsStartElement(localname, ns);
		}

		// Token: 0x06000343 RID: 835 RVA: 0x0001095D File Offset: 0x0000EB5D
		public override bool ReadToFollowing(string name)
		{
			this.CheckAsync();
			return this.coreReader.ReadToFollowing(name);
		}

		// Token: 0x06000344 RID: 836 RVA: 0x00010971 File Offset: 0x0000EB71
		public override bool ReadToFollowing(string localName, string namespaceURI)
		{
			this.CheckAsync();
			return this.coreReader.ReadToFollowing(localName, namespaceURI);
		}

		// Token: 0x06000345 RID: 837 RVA: 0x00010986 File Offset: 0x0000EB86
		public override bool ReadToDescendant(string name)
		{
			this.CheckAsync();
			return this.coreReader.ReadToDescendant(name);
		}

		// Token: 0x06000346 RID: 838 RVA: 0x0001099A File Offset: 0x0000EB9A
		public override bool ReadToDescendant(string localName, string namespaceURI)
		{
			this.CheckAsync();
			return this.coreReader.ReadToDescendant(localName, namespaceURI);
		}

		// Token: 0x06000347 RID: 839 RVA: 0x000109AF File Offset: 0x0000EBAF
		public override bool ReadToNextSibling(string name)
		{
			this.CheckAsync();
			return this.coreReader.ReadToNextSibling(name);
		}

		// Token: 0x06000348 RID: 840 RVA: 0x000109C3 File Offset: 0x0000EBC3
		public override bool ReadToNextSibling(string localName, string namespaceURI)
		{
			this.CheckAsync();
			return this.coreReader.ReadToNextSibling(localName, namespaceURI);
		}

		// Token: 0x06000349 RID: 841 RVA: 0x000109D8 File Offset: 0x0000EBD8
		public override string ReadInnerXml()
		{
			this.CheckAsync();
			return this.coreReader.ReadInnerXml();
		}

		// Token: 0x0600034A RID: 842 RVA: 0x000109EB File Offset: 0x0000EBEB
		public override string ReadOuterXml()
		{
			this.CheckAsync();
			return this.coreReader.ReadOuterXml();
		}

		// Token: 0x0600034B RID: 843 RVA: 0x000109FE File Offset: 0x0000EBFE
		public override XmlReader ReadSubtree()
		{
			this.CheckAsync();
			return XmlAsyncCheckReader.CreateAsyncCheckWrapper(this.coreReader.ReadSubtree());
		}

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x0600034C RID: 844 RVA: 0x00010A16 File Offset: 0x0000EC16
		public override bool HasAttributes
		{
			get
			{
				this.CheckAsync();
				return this.coreReader.HasAttributes;
			}
		}

		// Token: 0x0600034D RID: 845 RVA: 0x00010A29 File Offset: 0x0000EC29
		protected override void Dispose(bool disposing)
		{
			this.CheckAsync();
			this.coreReader.Dispose();
		}

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x0600034E RID: 846 RVA: 0x00010A3C File Offset: 0x0000EC3C
		internal override XmlNamespaceManager NamespaceManager
		{
			get
			{
				this.CheckAsync();
				return this.coreReader.NamespaceManager;
			}
		}

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x0600034F RID: 847 RVA: 0x00010A4F File Offset: 0x0000EC4F
		internal override IDtdInfo DtdInfo
		{
			get
			{
				this.CheckAsync();
				return this.coreReader.DtdInfo;
			}
		}

		// Token: 0x06000350 RID: 848 RVA: 0x00010A64 File Offset: 0x0000EC64
		public override Task<string> GetValueAsync()
		{
			this.CheckAsync();
			Task<string> valueAsync = this.coreReader.GetValueAsync();
			this.lastTask = valueAsync;
			return valueAsync;
		}

		// Token: 0x06000351 RID: 849 RVA: 0x00010A8C File Offset: 0x0000EC8C
		public override Task<object> ReadContentAsObjectAsync()
		{
			this.CheckAsync();
			Task<object> result = this.coreReader.ReadContentAsObjectAsync();
			this.lastTask = result;
			return result;
		}

		// Token: 0x06000352 RID: 850 RVA: 0x00010AB4 File Offset: 0x0000ECB4
		public override Task<string> ReadContentAsStringAsync()
		{
			this.CheckAsync();
			Task<string> result = this.coreReader.ReadContentAsStringAsync();
			this.lastTask = result;
			return result;
		}

		// Token: 0x06000353 RID: 851 RVA: 0x00010ADC File Offset: 0x0000ECDC
		public override Task<object> ReadContentAsAsync(Type returnType, IXmlNamespaceResolver namespaceResolver)
		{
			this.CheckAsync();
			Task<object> result = this.coreReader.ReadContentAsAsync(returnType, namespaceResolver);
			this.lastTask = result;
			return result;
		}

		// Token: 0x06000354 RID: 852 RVA: 0x00010B08 File Offset: 0x0000ED08
		public override Task<object> ReadElementContentAsObjectAsync()
		{
			this.CheckAsync();
			Task<object> result = this.coreReader.ReadElementContentAsObjectAsync();
			this.lastTask = result;
			return result;
		}

		// Token: 0x06000355 RID: 853 RVA: 0x00010B30 File Offset: 0x0000ED30
		public override Task<string> ReadElementContentAsStringAsync()
		{
			this.CheckAsync();
			Task<string> result = this.coreReader.ReadElementContentAsStringAsync();
			this.lastTask = result;
			return result;
		}

		// Token: 0x06000356 RID: 854 RVA: 0x00010B58 File Offset: 0x0000ED58
		public override Task<object> ReadElementContentAsAsync(Type returnType, IXmlNamespaceResolver namespaceResolver)
		{
			this.CheckAsync();
			Task<object> result = this.coreReader.ReadElementContentAsAsync(returnType, namespaceResolver);
			this.lastTask = result;
			return result;
		}

		// Token: 0x06000357 RID: 855 RVA: 0x00010B84 File Offset: 0x0000ED84
		public override Task<bool> ReadAsync()
		{
			this.CheckAsync();
			Task<bool> result = this.coreReader.ReadAsync();
			this.lastTask = result;
			return result;
		}

		// Token: 0x06000358 RID: 856 RVA: 0x00010BAC File Offset: 0x0000EDAC
		public override Task SkipAsync()
		{
			this.CheckAsync();
			Task result = this.coreReader.SkipAsync();
			this.lastTask = result;
			return result;
		}

		// Token: 0x06000359 RID: 857 RVA: 0x00010BD4 File Offset: 0x0000EDD4
		public override Task<int> ReadContentAsBase64Async(byte[] buffer, int index, int count)
		{
			this.CheckAsync();
			Task<int> result = this.coreReader.ReadContentAsBase64Async(buffer, index, count);
			this.lastTask = result;
			return result;
		}

		// Token: 0x0600035A RID: 858 RVA: 0x00010C00 File Offset: 0x0000EE00
		public override Task<int> ReadElementContentAsBase64Async(byte[] buffer, int index, int count)
		{
			this.CheckAsync();
			Task<int> result = this.coreReader.ReadElementContentAsBase64Async(buffer, index, count);
			this.lastTask = result;
			return result;
		}

		// Token: 0x0600035B RID: 859 RVA: 0x00010C2C File Offset: 0x0000EE2C
		public override Task<int> ReadContentAsBinHexAsync(byte[] buffer, int index, int count)
		{
			this.CheckAsync();
			Task<int> result = this.coreReader.ReadContentAsBinHexAsync(buffer, index, count);
			this.lastTask = result;
			return result;
		}

		// Token: 0x0600035C RID: 860 RVA: 0x00010C58 File Offset: 0x0000EE58
		public override Task<int> ReadElementContentAsBinHexAsync(byte[] buffer, int index, int count)
		{
			this.CheckAsync();
			Task<int> result = this.coreReader.ReadElementContentAsBinHexAsync(buffer, index, count);
			this.lastTask = result;
			return result;
		}

		// Token: 0x0600035D RID: 861 RVA: 0x00010C84 File Offset: 0x0000EE84
		public override Task<int> ReadValueChunkAsync(char[] buffer, int index, int count)
		{
			this.CheckAsync();
			Task<int> result = this.coreReader.ReadValueChunkAsync(buffer, index, count);
			this.lastTask = result;
			return result;
		}

		// Token: 0x0600035E RID: 862 RVA: 0x00010CB0 File Offset: 0x0000EEB0
		public override Task<XmlNodeType> MoveToContentAsync()
		{
			this.CheckAsync();
			Task<XmlNodeType> result = this.coreReader.MoveToContentAsync();
			this.lastTask = result;
			return result;
		}

		// Token: 0x0600035F RID: 863 RVA: 0x00010CD8 File Offset: 0x0000EED8
		public override Task<string> ReadInnerXmlAsync()
		{
			this.CheckAsync();
			Task<string> result = this.coreReader.ReadInnerXmlAsync();
			this.lastTask = result;
			return result;
		}

		// Token: 0x06000360 RID: 864 RVA: 0x00010D00 File Offset: 0x0000EF00
		public override Task<string> ReadOuterXmlAsync()
		{
			this.CheckAsync();
			Task<string> result = this.coreReader.ReadOuterXmlAsync();
			this.lastTask = result;
			return result;
		}

		// Token: 0x040006AD RID: 1709
		private readonly XmlReader coreReader;

		// Token: 0x040006AE RID: 1710
		private Task lastTask = AsyncHelper.DoneTask;
	}
}

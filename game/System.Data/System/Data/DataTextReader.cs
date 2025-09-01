using System;
using System.Xml;

namespace System.Data
{
	// Token: 0x0200014F RID: 335
	internal sealed class DataTextReader : XmlReader
	{
		// Token: 0x060011D7 RID: 4567 RVA: 0x0005506F File Offset: 0x0005326F
		internal static XmlReader CreateReader(XmlReader xr)
		{
			return new DataTextReader(xr);
		}

		// Token: 0x060011D8 RID: 4568 RVA: 0x00055077 File Offset: 0x00053277
		private DataTextReader(XmlReader input)
		{
			this._xmlreader = input;
		}

		// Token: 0x170002EE RID: 750
		// (get) Token: 0x060011D9 RID: 4569 RVA: 0x00055086 File Offset: 0x00053286
		public override XmlReaderSettings Settings
		{
			get
			{
				return this._xmlreader.Settings;
			}
		}

		// Token: 0x170002EF RID: 751
		// (get) Token: 0x060011DA RID: 4570 RVA: 0x00055093 File Offset: 0x00053293
		public override XmlNodeType NodeType
		{
			get
			{
				return this._xmlreader.NodeType;
			}
		}

		// Token: 0x170002F0 RID: 752
		// (get) Token: 0x060011DB RID: 4571 RVA: 0x000550A0 File Offset: 0x000532A0
		public override string Name
		{
			get
			{
				return this._xmlreader.Name;
			}
		}

		// Token: 0x170002F1 RID: 753
		// (get) Token: 0x060011DC RID: 4572 RVA: 0x000550AD File Offset: 0x000532AD
		public override string LocalName
		{
			get
			{
				return this._xmlreader.LocalName;
			}
		}

		// Token: 0x170002F2 RID: 754
		// (get) Token: 0x060011DD RID: 4573 RVA: 0x000550BA File Offset: 0x000532BA
		public override string NamespaceURI
		{
			get
			{
				return this._xmlreader.NamespaceURI;
			}
		}

		// Token: 0x170002F3 RID: 755
		// (get) Token: 0x060011DE RID: 4574 RVA: 0x000550C7 File Offset: 0x000532C7
		public override string Prefix
		{
			get
			{
				return this._xmlreader.Prefix;
			}
		}

		// Token: 0x170002F4 RID: 756
		// (get) Token: 0x060011DF RID: 4575 RVA: 0x000550D4 File Offset: 0x000532D4
		public override bool HasValue
		{
			get
			{
				return this._xmlreader.HasValue;
			}
		}

		// Token: 0x170002F5 RID: 757
		// (get) Token: 0x060011E0 RID: 4576 RVA: 0x000550E1 File Offset: 0x000532E1
		public override string Value
		{
			get
			{
				return this._xmlreader.Value;
			}
		}

		// Token: 0x170002F6 RID: 758
		// (get) Token: 0x060011E1 RID: 4577 RVA: 0x000550EE File Offset: 0x000532EE
		public override int Depth
		{
			get
			{
				return this._xmlreader.Depth;
			}
		}

		// Token: 0x170002F7 RID: 759
		// (get) Token: 0x060011E2 RID: 4578 RVA: 0x000550FB File Offset: 0x000532FB
		public override string BaseURI
		{
			get
			{
				return this._xmlreader.BaseURI;
			}
		}

		// Token: 0x170002F8 RID: 760
		// (get) Token: 0x060011E3 RID: 4579 RVA: 0x00055108 File Offset: 0x00053308
		public override bool IsEmptyElement
		{
			get
			{
				return this._xmlreader.IsEmptyElement;
			}
		}

		// Token: 0x170002F9 RID: 761
		// (get) Token: 0x060011E4 RID: 4580 RVA: 0x00055115 File Offset: 0x00053315
		public override bool IsDefault
		{
			get
			{
				return this._xmlreader.IsDefault;
			}
		}

		// Token: 0x170002FA RID: 762
		// (get) Token: 0x060011E5 RID: 4581 RVA: 0x00055122 File Offset: 0x00053322
		public override char QuoteChar
		{
			get
			{
				return this._xmlreader.QuoteChar;
			}
		}

		// Token: 0x170002FB RID: 763
		// (get) Token: 0x060011E6 RID: 4582 RVA: 0x0005512F File Offset: 0x0005332F
		public override XmlSpace XmlSpace
		{
			get
			{
				return this._xmlreader.XmlSpace;
			}
		}

		// Token: 0x170002FC RID: 764
		// (get) Token: 0x060011E7 RID: 4583 RVA: 0x0005513C File Offset: 0x0005333C
		public override string XmlLang
		{
			get
			{
				return this._xmlreader.XmlLang;
			}
		}

		// Token: 0x170002FD RID: 765
		// (get) Token: 0x060011E8 RID: 4584 RVA: 0x00055149 File Offset: 0x00053349
		public override int AttributeCount
		{
			get
			{
				return this._xmlreader.AttributeCount;
			}
		}

		// Token: 0x060011E9 RID: 4585 RVA: 0x00055156 File Offset: 0x00053356
		public override string GetAttribute(string name)
		{
			return this._xmlreader.GetAttribute(name);
		}

		// Token: 0x060011EA RID: 4586 RVA: 0x00055164 File Offset: 0x00053364
		public override string GetAttribute(string localName, string namespaceURI)
		{
			return this._xmlreader.GetAttribute(localName, namespaceURI);
		}

		// Token: 0x060011EB RID: 4587 RVA: 0x00055173 File Offset: 0x00053373
		public override string GetAttribute(int i)
		{
			return this._xmlreader.GetAttribute(i);
		}

		// Token: 0x060011EC RID: 4588 RVA: 0x00055181 File Offset: 0x00053381
		public override bool MoveToAttribute(string name)
		{
			return this._xmlreader.MoveToAttribute(name);
		}

		// Token: 0x060011ED RID: 4589 RVA: 0x0005518F File Offset: 0x0005338F
		public override bool MoveToAttribute(string localName, string namespaceURI)
		{
			return this._xmlreader.MoveToAttribute(localName, namespaceURI);
		}

		// Token: 0x060011EE RID: 4590 RVA: 0x0005519E File Offset: 0x0005339E
		public override void MoveToAttribute(int i)
		{
			this._xmlreader.MoveToAttribute(i);
		}

		// Token: 0x060011EF RID: 4591 RVA: 0x000551AC File Offset: 0x000533AC
		public override bool MoveToFirstAttribute()
		{
			return this._xmlreader.MoveToFirstAttribute();
		}

		// Token: 0x060011F0 RID: 4592 RVA: 0x000551B9 File Offset: 0x000533B9
		public override bool MoveToNextAttribute()
		{
			return this._xmlreader.MoveToNextAttribute();
		}

		// Token: 0x060011F1 RID: 4593 RVA: 0x000551C6 File Offset: 0x000533C6
		public override bool MoveToElement()
		{
			return this._xmlreader.MoveToElement();
		}

		// Token: 0x060011F2 RID: 4594 RVA: 0x000551D3 File Offset: 0x000533D3
		public override bool ReadAttributeValue()
		{
			return this._xmlreader.ReadAttributeValue();
		}

		// Token: 0x060011F3 RID: 4595 RVA: 0x000551E0 File Offset: 0x000533E0
		public override bool Read()
		{
			return this._xmlreader.Read();
		}

		// Token: 0x170002FE RID: 766
		// (get) Token: 0x060011F4 RID: 4596 RVA: 0x000551ED File Offset: 0x000533ED
		public override bool EOF
		{
			get
			{
				return this._xmlreader.EOF;
			}
		}

		// Token: 0x060011F5 RID: 4597 RVA: 0x000551FA File Offset: 0x000533FA
		public override void Close()
		{
			this._xmlreader.Close();
		}

		// Token: 0x170002FF RID: 767
		// (get) Token: 0x060011F6 RID: 4598 RVA: 0x00055207 File Offset: 0x00053407
		public override ReadState ReadState
		{
			get
			{
				return this._xmlreader.ReadState;
			}
		}

		// Token: 0x060011F7 RID: 4599 RVA: 0x00055214 File Offset: 0x00053414
		public override void Skip()
		{
			this._xmlreader.Skip();
		}

		// Token: 0x17000300 RID: 768
		// (get) Token: 0x060011F8 RID: 4600 RVA: 0x00055221 File Offset: 0x00053421
		public override XmlNameTable NameTable
		{
			get
			{
				return this._xmlreader.NameTable;
			}
		}

		// Token: 0x060011F9 RID: 4601 RVA: 0x0005522E File Offset: 0x0005342E
		public override string LookupNamespace(string prefix)
		{
			return this._xmlreader.LookupNamespace(prefix);
		}

		// Token: 0x17000301 RID: 769
		// (get) Token: 0x060011FA RID: 4602 RVA: 0x0005523C File Offset: 0x0005343C
		public override bool CanResolveEntity
		{
			get
			{
				return this._xmlreader.CanResolveEntity;
			}
		}

		// Token: 0x060011FB RID: 4603 RVA: 0x00055249 File Offset: 0x00053449
		public override void ResolveEntity()
		{
			this._xmlreader.ResolveEntity();
		}

		// Token: 0x17000302 RID: 770
		// (get) Token: 0x060011FC RID: 4604 RVA: 0x00055256 File Offset: 0x00053456
		public override bool CanReadBinaryContent
		{
			get
			{
				return this._xmlreader.CanReadBinaryContent;
			}
		}

		// Token: 0x060011FD RID: 4605 RVA: 0x00055263 File Offset: 0x00053463
		public override int ReadContentAsBase64(byte[] buffer, int index, int count)
		{
			return this._xmlreader.ReadContentAsBase64(buffer, index, count);
		}

		// Token: 0x060011FE RID: 4606 RVA: 0x00055273 File Offset: 0x00053473
		public override int ReadElementContentAsBase64(byte[] buffer, int index, int count)
		{
			return this._xmlreader.ReadElementContentAsBase64(buffer, index, count);
		}

		// Token: 0x060011FF RID: 4607 RVA: 0x00055283 File Offset: 0x00053483
		public override int ReadContentAsBinHex(byte[] buffer, int index, int count)
		{
			return this._xmlreader.ReadContentAsBinHex(buffer, index, count);
		}

		// Token: 0x06001200 RID: 4608 RVA: 0x00055293 File Offset: 0x00053493
		public override int ReadElementContentAsBinHex(byte[] buffer, int index, int count)
		{
			return this._xmlreader.ReadElementContentAsBinHex(buffer, index, count);
		}

		// Token: 0x17000303 RID: 771
		// (get) Token: 0x06001201 RID: 4609 RVA: 0x000552A3 File Offset: 0x000534A3
		public override bool CanReadValueChunk
		{
			get
			{
				return this._xmlreader.CanReadValueChunk;
			}
		}

		// Token: 0x06001202 RID: 4610 RVA: 0x000552B0 File Offset: 0x000534B0
		public override string ReadString()
		{
			return this._xmlreader.ReadString();
		}

		// Token: 0x04000B92 RID: 2962
		private XmlReader _xmlreader;
	}
}

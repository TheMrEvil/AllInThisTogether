using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace System.Xml
{
	// Token: 0x02000186 RID: 390
	internal class XsdCachingReader : XmlReader, IXmlLineInfo
	{
		// Token: 0x06000DFB RID: 3579 RVA: 0x0005B6D0 File Offset: 0x000598D0
		internal XsdCachingReader(XmlReader reader, IXmlLineInfo lineInfo, CachingEventHandler handlerMethod)
		{
			this.coreReader = reader;
			this.lineInfo = lineInfo;
			this.cacheHandler = handlerMethod;
			this.attributeEvents = new ValidatingReaderNodeData[8];
			this.contentEvents = new ValidatingReaderNodeData[4];
			this.Init();
		}

		// Token: 0x06000DFC RID: 3580 RVA: 0x0005B70C File Offset: 0x0005990C
		private void Init()
		{
			this.coreReaderNameTable = this.coreReader.NameTable;
			this.cacheState = XsdCachingReader.CachingReaderState.Init;
			this.contentIndex = 0;
			this.currentAttrIndex = -1;
			this.currentContentIndex = -1;
			this.attributeCount = 0;
			this.cachedNode = null;
			this.readAhead = false;
			if (this.coreReader.NodeType == XmlNodeType.Element)
			{
				ValidatingReaderNodeData validatingReaderNodeData = this.AddContent(this.coreReader.NodeType);
				validatingReaderNodeData.SetItemData(this.coreReader.LocalName, this.coreReader.Prefix, this.coreReader.NamespaceURI, this.coreReader.Depth);
				validatingReaderNodeData.SetLineInfo(this.lineInfo);
				this.RecordAttributes();
			}
		}

		// Token: 0x06000DFD RID: 3581 RVA: 0x0005B7BD File Offset: 0x000599BD
		internal void Reset(XmlReader reader)
		{
			this.coreReader = reader;
			this.Init();
		}

		// Token: 0x17000214 RID: 532
		// (get) Token: 0x06000DFE RID: 3582 RVA: 0x0005B7CC File Offset: 0x000599CC
		public override XmlReaderSettings Settings
		{
			get
			{
				return this.coreReader.Settings;
			}
		}

		// Token: 0x17000215 RID: 533
		// (get) Token: 0x06000DFF RID: 3583 RVA: 0x0005B7D9 File Offset: 0x000599D9
		public override XmlNodeType NodeType
		{
			get
			{
				return this.cachedNode.NodeType;
			}
		}

		// Token: 0x17000216 RID: 534
		// (get) Token: 0x06000E00 RID: 3584 RVA: 0x0005B7E6 File Offset: 0x000599E6
		public override string Name
		{
			get
			{
				return this.cachedNode.GetAtomizedNameWPrefix(this.coreReaderNameTable);
			}
		}

		// Token: 0x17000217 RID: 535
		// (get) Token: 0x06000E01 RID: 3585 RVA: 0x0005B7F9 File Offset: 0x000599F9
		public override string LocalName
		{
			get
			{
				return this.cachedNode.LocalName;
			}
		}

		// Token: 0x17000218 RID: 536
		// (get) Token: 0x06000E02 RID: 3586 RVA: 0x0005B806 File Offset: 0x00059A06
		public override string NamespaceURI
		{
			get
			{
				return this.cachedNode.Namespace;
			}
		}

		// Token: 0x17000219 RID: 537
		// (get) Token: 0x06000E03 RID: 3587 RVA: 0x0005B813 File Offset: 0x00059A13
		public override string Prefix
		{
			get
			{
				return this.cachedNode.Prefix;
			}
		}

		// Token: 0x1700021A RID: 538
		// (get) Token: 0x06000E04 RID: 3588 RVA: 0x0005B820 File Offset: 0x00059A20
		public override bool HasValue
		{
			get
			{
				return XmlReader.HasValueInternal(this.cachedNode.NodeType);
			}
		}

		// Token: 0x1700021B RID: 539
		// (get) Token: 0x06000E05 RID: 3589 RVA: 0x0005B832 File Offset: 0x00059A32
		public override string Value
		{
			get
			{
				if (!this.returnOriginalStringValues)
				{
					return this.cachedNode.RawValue;
				}
				return this.cachedNode.OriginalStringValue;
			}
		}

		// Token: 0x1700021C RID: 540
		// (get) Token: 0x06000E06 RID: 3590 RVA: 0x0005B853 File Offset: 0x00059A53
		public override int Depth
		{
			get
			{
				return this.cachedNode.Depth;
			}
		}

		// Token: 0x1700021D RID: 541
		// (get) Token: 0x06000E07 RID: 3591 RVA: 0x0005B860 File Offset: 0x00059A60
		public override string BaseURI
		{
			get
			{
				return this.coreReader.BaseURI;
			}
		}

		// Token: 0x1700021E RID: 542
		// (get) Token: 0x06000E08 RID: 3592 RVA: 0x0000D1C5 File Offset: 0x0000B3C5
		public override bool IsEmptyElement
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700021F RID: 543
		// (get) Token: 0x06000E09 RID: 3593 RVA: 0x0000D1C5 File Offset: 0x0000B3C5
		public override bool IsDefault
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000220 RID: 544
		// (get) Token: 0x06000E0A RID: 3594 RVA: 0x0005B86D File Offset: 0x00059A6D
		public override char QuoteChar
		{
			get
			{
				return this.coreReader.QuoteChar;
			}
		}

		// Token: 0x17000221 RID: 545
		// (get) Token: 0x06000E0B RID: 3595 RVA: 0x0005B87A File Offset: 0x00059A7A
		public override XmlSpace XmlSpace
		{
			get
			{
				return this.coreReader.XmlSpace;
			}
		}

		// Token: 0x17000222 RID: 546
		// (get) Token: 0x06000E0C RID: 3596 RVA: 0x0005B887 File Offset: 0x00059A87
		public override string XmlLang
		{
			get
			{
				return this.coreReader.XmlLang;
			}
		}

		// Token: 0x17000223 RID: 547
		// (get) Token: 0x06000E0D RID: 3597 RVA: 0x0005B894 File Offset: 0x00059A94
		public override int AttributeCount
		{
			get
			{
				return this.attributeCount;
			}
		}

		// Token: 0x06000E0E RID: 3598 RVA: 0x0005B89C File Offset: 0x00059A9C
		public override string GetAttribute(string name)
		{
			int num;
			if (name.IndexOf(':') == -1)
			{
				num = this.GetAttributeIndexWithoutPrefix(name);
			}
			else
			{
				num = this.GetAttributeIndexWithPrefix(name);
			}
			if (num < 0)
			{
				return null;
			}
			return this.attributeEvents[num].RawValue;
		}

		// Token: 0x06000E0F RID: 3599 RVA: 0x0005B8DC File Offset: 0x00059ADC
		public override string GetAttribute(string name, string namespaceURI)
		{
			namespaceURI = ((namespaceURI == null) ? string.Empty : this.coreReaderNameTable.Get(namespaceURI));
			name = this.coreReaderNameTable.Get(name);
			for (int i = 0; i < this.attributeCount; i++)
			{
				ValidatingReaderNodeData validatingReaderNodeData = this.attributeEvents[i];
				if (Ref.Equal(validatingReaderNodeData.LocalName, name) && Ref.Equal(validatingReaderNodeData.Namespace, namespaceURI))
				{
					return validatingReaderNodeData.RawValue;
				}
			}
			return null;
		}

		// Token: 0x06000E10 RID: 3600 RVA: 0x0005B94D File Offset: 0x00059B4D
		public override string GetAttribute(int i)
		{
			if (i < 0 || i >= this.attributeCount)
			{
				throw new ArgumentOutOfRangeException("i");
			}
			return this.attributeEvents[i].RawValue;
		}

		// Token: 0x17000224 RID: 548
		public override string this[int i]
		{
			get
			{
				return this.GetAttribute(i);
			}
		}

		// Token: 0x17000225 RID: 549
		public override string this[string name]
		{
			get
			{
				return this.GetAttribute(name);
			}
		}

		// Token: 0x17000226 RID: 550
		public override string this[string name, string namespaceURI]
		{
			get
			{
				return this.GetAttribute(name, namespaceURI);
			}
		}

		// Token: 0x06000E14 RID: 3604 RVA: 0x0005B974 File Offset: 0x00059B74
		public override bool MoveToAttribute(string name)
		{
			int num;
			if (name.IndexOf(':') == -1)
			{
				num = this.GetAttributeIndexWithoutPrefix(name);
			}
			else
			{
				num = this.GetAttributeIndexWithPrefix(name);
			}
			if (num >= 0)
			{
				this.currentAttrIndex = num;
				this.cachedNode = this.attributeEvents[num];
				return true;
			}
			return false;
		}

		// Token: 0x06000E15 RID: 3605 RVA: 0x0005B9BC File Offset: 0x00059BBC
		public override bool MoveToAttribute(string name, string ns)
		{
			ns = ((ns == null) ? string.Empty : this.coreReaderNameTable.Get(ns));
			name = this.coreReaderNameTable.Get(name);
			for (int i = 0; i < this.attributeCount; i++)
			{
				ValidatingReaderNodeData validatingReaderNodeData = this.attributeEvents[i];
				if (Ref.Equal(validatingReaderNodeData.LocalName, name) && Ref.Equal(validatingReaderNodeData.Namespace, ns))
				{
					this.currentAttrIndex = i;
					this.cachedNode = this.attributeEvents[i];
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000E16 RID: 3606 RVA: 0x0005BA3D File Offset: 0x00059C3D
		public override void MoveToAttribute(int i)
		{
			if (i < 0 || i >= this.attributeCount)
			{
				throw new ArgumentOutOfRangeException("i");
			}
			this.currentAttrIndex = i;
			this.cachedNode = this.attributeEvents[i];
		}

		// Token: 0x06000E17 RID: 3607 RVA: 0x0005BA6C File Offset: 0x00059C6C
		public override bool MoveToFirstAttribute()
		{
			if (this.attributeCount == 0)
			{
				return false;
			}
			this.currentAttrIndex = 0;
			this.cachedNode = this.attributeEvents[0];
			return true;
		}

		// Token: 0x06000E18 RID: 3608 RVA: 0x0005BA90 File Offset: 0x00059C90
		public override bool MoveToNextAttribute()
		{
			if (this.currentAttrIndex + 1 < this.attributeCount)
			{
				ValidatingReaderNodeData[] array = this.attributeEvents;
				int num = this.currentAttrIndex + 1;
				this.currentAttrIndex = num;
				this.cachedNode = array[num];
				return true;
			}
			return false;
		}

		// Token: 0x06000E19 RID: 3609 RVA: 0x0005BACE File Offset: 0x00059CCE
		public override bool MoveToElement()
		{
			if (this.cacheState != XsdCachingReader.CachingReaderState.Replay || this.cachedNode.NodeType != XmlNodeType.Attribute)
			{
				return false;
			}
			this.currentContentIndex = 0;
			this.currentAttrIndex = -1;
			this.Read();
			return true;
		}

		// Token: 0x06000E1A RID: 3610 RVA: 0x0005BB00 File Offset: 0x00059D00
		public override bool Read()
		{
			switch (this.cacheState)
			{
			case XsdCachingReader.CachingReaderState.Init:
				this.cacheState = XsdCachingReader.CachingReaderState.Record;
				break;
			case XsdCachingReader.CachingReaderState.Record:
				break;
			case XsdCachingReader.CachingReaderState.Replay:
				if (this.currentContentIndex >= this.contentIndex)
				{
					this.cacheState = XsdCachingReader.CachingReaderState.ReaderClosed;
					this.cacheHandler(this);
					return (this.coreReader.NodeType == XmlNodeType.Element && !this.readAhead) || this.coreReader.Read();
				}
				this.cachedNode = this.contentEvents[this.currentContentIndex];
				if (this.currentContentIndex > 0)
				{
					this.ClearAttributesInfo();
				}
				this.currentContentIndex++;
				return true;
			default:
				return false;
			}
			ValidatingReaderNodeData validatingReaderNodeData = null;
			if (this.coreReader.Read())
			{
				switch (this.coreReader.NodeType)
				{
				case XmlNodeType.Element:
					this.cacheState = XsdCachingReader.CachingReaderState.ReaderClosed;
					return false;
				case XmlNodeType.Text:
				case XmlNodeType.CDATA:
				case XmlNodeType.ProcessingInstruction:
				case XmlNodeType.Comment:
				case XmlNodeType.Whitespace:
				case XmlNodeType.SignificantWhitespace:
					validatingReaderNodeData = this.AddContent(this.coreReader.NodeType);
					validatingReaderNodeData.SetItemData(this.coreReader.Value);
					validatingReaderNodeData.SetLineInfo(this.lineInfo);
					validatingReaderNodeData.Depth = this.coreReader.Depth;
					break;
				case XmlNodeType.EndElement:
					validatingReaderNodeData = this.AddContent(this.coreReader.NodeType);
					validatingReaderNodeData.SetItemData(this.coreReader.LocalName, this.coreReader.Prefix, this.coreReader.NamespaceURI, this.coreReader.Depth);
					validatingReaderNodeData.SetLineInfo(this.lineInfo);
					break;
				}
				this.cachedNode = validatingReaderNodeData;
				return true;
			}
			this.cacheState = XsdCachingReader.CachingReaderState.ReaderClosed;
			return false;
		}

		// Token: 0x06000E1B RID: 3611 RVA: 0x0005BCC0 File Offset: 0x00059EC0
		internal ValidatingReaderNodeData RecordTextNode(string textValue, string originalStringValue, int depth, int lineNo, int linePos)
		{
			ValidatingReaderNodeData validatingReaderNodeData = this.AddContent(XmlNodeType.Text);
			validatingReaderNodeData.SetItemData(textValue, originalStringValue);
			validatingReaderNodeData.SetLineInfo(lineNo, linePos);
			validatingReaderNodeData.Depth = depth;
			return validatingReaderNodeData;
		}

		// Token: 0x06000E1C RID: 3612 RVA: 0x0005BCE4 File Offset: 0x00059EE4
		internal void SwitchTextNodeAndEndElement(string textValue, string originalStringValue)
		{
			ValidatingReaderNodeData validatingReaderNodeData = this.RecordTextNode(textValue, originalStringValue, this.coreReader.Depth + 1, 0, 0);
			int num = this.contentIndex - 2;
			ValidatingReaderNodeData validatingReaderNodeData2 = this.contentEvents[num];
			this.contentEvents[num] = validatingReaderNodeData;
			this.contentEvents[this.contentIndex - 1] = validatingReaderNodeData2;
		}

		// Token: 0x06000E1D RID: 3613 RVA: 0x0005BD34 File Offset: 0x00059F34
		internal void RecordEndElementNode()
		{
			ValidatingReaderNodeData validatingReaderNodeData = this.AddContent(XmlNodeType.EndElement);
			validatingReaderNodeData.SetItemData(this.coreReader.LocalName, this.coreReader.Prefix, this.coreReader.NamespaceURI, this.coreReader.Depth);
			validatingReaderNodeData.SetLineInfo(this.coreReader as IXmlLineInfo);
			if (this.coreReader.IsEmptyElement)
			{
				this.readAhead = true;
			}
		}

		// Token: 0x06000E1E RID: 3614 RVA: 0x0005BD9F File Offset: 0x00059F9F
		internal string ReadOriginalContentAsString()
		{
			this.returnOriginalStringValues = true;
			string result = base.InternalReadContentAsString();
			this.returnOriginalStringValues = false;
			return result;
		}

		// Token: 0x17000227 RID: 551
		// (get) Token: 0x06000E1F RID: 3615 RVA: 0x0005BDB5 File Offset: 0x00059FB5
		public override bool EOF
		{
			get
			{
				return this.cacheState == XsdCachingReader.CachingReaderState.ReaderClosed && this.coreReader.EOF;
			}
		}

		// Token: 0x06000E20 RID: 3616 RVA: 0x0005BDCD File Offset: 0x00059FCD
		public override void Close()
		{
			this.coreReader.Close();
			this.cacheState = XsdCachingReader.CachingReaderState.ReaderClosed;
		}

		// Token: 0x17000228 RID: 552
		// (get) Token: 0x06000E21 RID: 3617 RVA: 0x0005BDE1 File Offset: 0x00059FE1
		public override ReadState ReadState
		{
			get
			{
				return this.coreReader.ReadState;
			}
		}

		// Token: 0x06000E22 RID: 3618 RVA: 0x0005BDF0 File Offset: 0x00059FF0
		public override void Skip()
		{
			XmlNodeType nodeType = this.cachedNode.NodeType;
			if (nodeType != XmlNodeType.Element)
			{
				if (nodeType != XmlNodeType.Attribute)
				{
					this.Read();
					return;
				}
				this.MoveToElement();
			}
			if (this.coreReader.NodeType != XmlNodeType.EndElement && !this.readAhead)
			{
				int num = this.coreReader.Depth - 1;
				while (this.coreReader.Read() && this.coreReader.Depth > num)
				{
				}
			}
			this.coreReader.Read();
			this.cacheState = XsdCachingReader.CachingReaderState.ReaderClosed;
			this.cacheHandler(this);
		}

		// Token: 0x17000229 RID: 553
		// (get) Token: 0x06000E23 RID: 3619 RVA: 0x0005BE83 File Offset: 0x0005A083
		public override XmlNameTable NameTable
		{
			get
			{
				return this.coreReaderNameTable;
			}
		}

		// Token: 0x06000E24 RID: 3620 RVA: 0x0005BE8B File Offset: 0x0005A08B
		public override string LookupNamespace(string prefix)
		{
			return this.coreReader.LookupNamespace(prefix);
		}

		// Token: 0x06000E25 RID: 3621 RVA: 0x0005BE99 File Offset: 0x0005A099
		public override void ResolveEntity()
		{
			throw new InvalidOperationException();
		}

		// Token: 0x06000E26 RID: 3622 RVA: 0x0005BEA0 File Offset: 0x0005A0A0
		public override bool ReadAttributeValue()
		{
			if (this.cachedNode.NodeType != XmlNodeType.Attribute)
			{
				return false;
			}
			this.cachedNode = this.CreateDummyTextNode(this.cachedNode.RawValue, this.cachedNode.Depth + 1);
			return true;
		}

		// Token: 0x06000E27 RID: 3623 RVA: 0x0001222F File Offset: 0x0001042F
		bool IXmlLineInfo.HasLineInfo()
		{
			return true;
		}

		// Token: 0x1700022A RID: 554
		// (get) Token: 0x06000E28 RID: 3624 RVA: 0x0005BED7 File Offset: 0x0005A0D7
		int IXmlLineInfo.LineNumber
		{
			get
			{
				return this.cachedNode.LineNumber;
			}
		}

		// Token: 0x1700022B RID: 555
		// (get) Token: 0x06000E29 RID: 3625 RVA: 0x0005BEE4 File Offset: 0x0005A0E4
		int IXmlLineInfo.LinePosition
		{
			get
			{
				return this.cachedNode.LinePosition;
			}
		}

		// Token: 0x06000E2A RID: 3626 RVA: 0x0005BEF1 File Offset: 0x0005A0F1
		internal void SetToReplayMode()
		{
			this.cacheState = XsdCachingReader.CachingReaderState.Replay;
			this.currentContentIndex = 0;
			this.currentAttrIndex = -1;
			this.Read();
		}

		// Token: 0x06000E2B RID: 3627 RVA: 0x0005BF0F File Offset: 0x0005A10F
		internal XmlReader GetCoreReader()
		{
			return this.coreReader;
		}

		// Token: 0x06000E2C RID: 3628 RVA: 0x0005BF17 File Offset: 0x0005A117
		internal IXmlLineInfo GetLineInfo()
		{
			return this.lineInfo;
		}

		// Token: 0x06000E2D RID: 3629 RVA: 0x0005BF1F File Offset: 0x0005A11F
		private void ClearAttributesInfo()
		{
			this.attributeCount = 0;
			this.currentAttrIndex = -1;
		}

		// Token: 0x06000E2E RID: 3630 RVA: 0x0005BF30 File Offset: 0x0005A130
		private ValidatingReaderNodeData AddAttribute(int attIndex)
		{
			ValidatingReaderNodeData validatingReaderNodeData = this.attributeEvents[attIndex];
			if (validatingReaderNodeData != null)
			{
				validatingReaderNodeData.Clear(XmlNodeType.Attribute);
				return validatingReaderNodeData;
			}
			if (attIndex >= this.attributeEvents.Length - 1)
			{
				ValidatingReaderNodeData[] destinationArray = new ValidatingReaderNodeData[this.attributeEvents.Length * 2];
				Array.Copy(this.attributeEvents, 0, destinationArray, 0, this.attributeEvents.Length);
				this.attributeEvents = destinationArray;
			}
			validatingReaderNodeData = this.attributeEvents[attIndex];
			if (validatingReaderNodeData == null)
			{
				validatingReaderNodeData = new ValidatingReaderNodeData(XmlNodeType.Attribute);
				this.attributeEvents[attIndex] = validatingReaderNodeData;
			}
			return validatingReaderNodeData;
		}

		// Token: 0x06000E2F RID: 3631 RVA: 0x0005BFAC File Offset: 0x0005A1AC
		private ValidatingReaderNodeData AddContent(XmlNodeType nodeType)
		{
			ValidatingReaderNodeData validatingReaderNodeData = this.contentEvents[this.contentIndex];
			if (validatingReaderNodeData != null)
			{
				validatingReaderNodeData.Clear(nodeType);
				this.contentIndex++;
				return validatingReaderNodeData;
			}
			if (this.contentIndex >= this.contentEvents.Length - 1)
			{
				ValidatingReaderNodeData[] destinationArray = new ValidatingReaderNodeData[this.contentEvents.Length * 2];
				Array.Copy(this.contentEvents, 0, destinationArray, 0, this.contentEvents.Length);
				this.contentEvents = destinationArray;
			}
			validatingReaderNodeData = this.contentEvents[this.contentIndex];
			if (validatingReaderNodeData == null)
			{
				validatingReaderNodeData = new ValidatingReaderNodeData(nodeType);
				this.contentEvents[this.contentIndex] = validatingReaderNodeData;
			}
			this.contentIndex++;
			return validatingReaderNodeData;
		}

		// Token: 0x06000E30 RID: 3632 RVA: 0x0005C058 File Offset: 0x0005A258
		private void RecordAttributes()
		{
			this.attributeCount = this.coreReader.AttributeCount;
			if (this.coreReader.MoveToFirstAttribute())
			{
				int num = 0;
				do
				{
					ValidatingReaderNodeData validatingReaderNodeData = this.AddAttribute(num);
					validatingReaderNodeData.SetItemData(this.coreReader.LocalName, this.coreReader.Prefix, this.coreReader.NamespaceURI, this.coreReader.Depth);
					validatingReaderNodeData.SetLineInfo(this.lineInfo);
					validatingReaderNodeData.RawValue = this.coreReader.Value;
					num++;
				}
				while (this.coreReader.MoveToNextAttribute());
				this.coreReader.MoveToElement();
			}
		}

		// Token: 0x06000E31 RID: 3633 RVA: 0x0005C0F8 File Offset: 0x0005A2F8
		private int GetAttributeIndexWithoutPrefix(string name)
		{
			name = this.coreReaderNameTable.Get(name);
			if (name == null)
			{
				return -1;
			}
			for (int i = 0; i < this.attributeCount; i++)
			{
				ValidatingReaderNodeData validatingReaderNodeData = this.attributeEvents[i];
				if (Ref.Equal(validatingReaderNodeData.LocalName, name) && validatingReaderNodeData.Prefix.Length == 0)
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x06000E32 RID: 3634 RVA: 0x0005C150 File Offset: 0x0005A350
		private int GetAttributeIndexWithPrefix(string name)
		{
			name = this.coreReaderNameTable.Get(name);
			if (name == null)
			{
				return -1;
			}
			for (int i = 0; i < this.attributeCount; i++)
			{
				if (Ref.Equal(this.attributeEvents[i].GetAtomizedNameWPrefix(this.coreReaderNameTable), name))
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x06000E33 RID: 3635 RVA: 0x0005C19F File Offset: 0x0005A39F
		private ValidatingReaderNodeData CreateDummyTextNode(string attributeValue, int depth)
		{
			if (this.textNode == null)
			{
				this.textNode = new ValidatingReaderNodeData(XmlNodeType.Text);
			}
			this.textNode.Depth = depth;
			this.textNode.RawValue = attributeValue;
			return this.textNode;
		}

		// Token: 0x06000E34 RID: 3636 RVA: 0x0005C1D3 File Offset: 0x0005A3D3
		public override Task<string> GetValueAsync()
		{
			if (this.returnOriginalStringValues)
			{
				return Task.FromResult<string>(this.cachedNode.OriginalStringValue);
			}
			return Task.FromResult<string>(this.cachedNode.RawValue);
		}

		// Token: 0x06000E35 RID: 3637 RVA: 0x0005C200 File Offset: 0x0005A400
		public override Task<bool> ReadAsync()
		{
			XsdCachingReader.<ReadAsync>d__100 <ReadAsync>d__;
			<ReadAsync>d__.<>4__this = this;
			<ReadAsync>d__.<>t__builder = AsyncTaskMethodBuilder<bool>.Create();
			<ReadAsync>d__.<>1__state = -1;
			<ReadAsync>d__.<>t__builder.Start<XsdCachingReader.<ReadAsync>d__100>(ref <ReadAsync>d__);
			return <ReadAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000E36 RID: 3638 RVA: 0x0005C244 File Offset: 0x0005A444
		public override Task SkipAsync()
		{
			XsdCachingReader.<SkipAsync>d__101 <SkipAsync>d__;
			<SkipAsync>d__.<>4__this = this;
			<SkipAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<SkipAsync>d__.<>1__state = -1;
			<SkipAsync>d__.<>t__builder.Start<XsdCachingReader.<SkipAsync>d__101>(ref <SkipAsync>d__);
			return <SkipAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000E37 RID: 3639 RVA: 0x0005C287 File Offset: 0x0005A487
		internal Task SetToReplayModeAsync()
		{
			this.cacheState = XsdCachingReader.CachingReaderState.Replay;
			this.currentContentIndex = 0;
			this.currentAttrIndex = -1;
			return this.ReadAsync();
		}

		// Token: 0x04000F0E RID: 3854
		private XmlReader coreReader;

		// Token: 0x04000F0F RID: 3855
		private XmlNameTable coreReaderNameTable;

		// Token: 0x04000F10 RID: 3856
		private ValidatingReaderNodeData[] contentEvents;

		// Token: 0x04000F11 RID: 3857
		private ValidatingReaderNodeData[] attributeEvents;

		// Token: 0x04000F12 RID: 3858
		private ValidatingReaderNodeData cachedNode;

		// Token: 0x04000F13 RID: 3859
		private XsdCachingReader.CachingReaderState cacheState;

		// Token: 0x04000F14 RID: 3860
		private int contentIndex;

		// Token: 0x04000F15 RID: 3861
		private int attributeCount;

		// Token: 0x04000F16 RID: 3862
		private bool returnOriginalStringValues;

		// Token: 0x04000F17 RID: 3863
		private CachingEventHandler cacheHandler;

		// Token: 0x04000F18 RID: 3864
		private int currentAttrIndex;

		// Token: 0x04000F19 RID: 3865
		private int currentContentIndex;

		// Token: 0x04000F1A RID: 3866
		private bool readAhead;

		// Token: 0x04000F1B RID: 3867
		private IXmlLineInfo lineInfo;

		// Token: 0x04000F1C RID: 3868
		private ValidatingReaderNodeData textNode;

		// Token: 0x04000F1D RID: 3869
		private const int InitialAttributeCount = 8;

		// Token: 0x04000F1E RID: 3870
		private const int InitialContentCount = 4;

		// Token: 0x02000187 RID: 391
		private enum CachingReaderState
		{
			// Token: 0x04000F20 RID: 3872
			None,
			// Token: 0x04000F21 RID: 3873
			Init,
			// Token: 0x04000F22 RID: 3874
			Record,
			// Token: 0x04000F23 RID: 3875
			Replay,
			// Token: 0x04000F24 RID: 3876
			ReaderClosed,
			// Token: 0x04000F25 RID: 3877
			Error
		}

		// Token: 0x02000188 RID: 392
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <ReadAsync>d__100 : IAsyncStateMachine
		{
			// Token: 0x06000E38 RID: 3640 RVA: 0x0005C2A4 File Offset: 0x0005A4A4
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				XsdCachingReader xsdCachingReader = this.<>4__this;
				bool result;
				try
				{
					ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter awaiter;
					ConfiguredTaskAwaitable<string>.ConfiguredTaskAwaiter awaiter2;
					switch (num)
					{
					case 0:
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						break;
					case 1:
						awaiter2 = this.<>u__2;
						this.<>u__2 = default(ConfiguredTaskAwaitable<string>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_212;
					case 2:
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_317;
					default:
						switch (xsdCachingReader.cacheState)
						{
						case XsdCachingReader.CachingReaderState.Init:
							xsdCachingReader.cacheState = XsdCachingReader.CachingReaderState.Record;
							break;
						case XsdCachingReader.CachingReaderState.Record:
							break;
						case XsdCachingReader.CachingReaderState.Replay:
							if (xsdCachingReader.currentContentIndex < xsdCachingReader.contentIndex)
							{
								xsdCachingReader.cachedNode = xsdCachingReader.contentEvents[xsdCachingReader.currentContentIndex];
								if (xsdCachingReader.currentContentIndex > 0)
								{
									xsdCachingReader.ClearAttributesInfo();
								}
								xsdCachingReader.currentContentIndex++;
								result = true;
								goto IL_376;
							}
							xsdCachingReader.cacheState = XsdCachingReader.CachingReaderState.ReaderClosed;
							xsdCachingReader.cacheHandler(xsdCachingReader);
							if (xsdCachingReader.coreReader.NodeType == XmlNodeType.Element && !xsdCachingReader.readAhead)
							{
								result = true;
								goto IL_376;
							}
							awaiter = xsdCachingReader.coreReader.ReadAsync().ConfigureAwait(false).GetAwaiter();
							if (!awaiter.IsCompleted)
							{
								this.<>1__state = 2;
								this.<>u__1 = awaiter;
								this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter, XsdCachingReader.<ReadAsync>d__100>(ref awaiter, ref this);
								return;
							}
							goto IL_317;
						default:
							result = false;
							goto IL_376;
						}
						this.<recordedNode>5__2 = null;
						awaiter = xsdCachingReader.coreReader.ReadAsync().ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 0;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter, XsdCachingReader.<ReadAsync>d__100>(ref awaiter, ref this);
							return;
						}
						break;
					}
					if (!awaiter.GetResult())
					{
						xsdCachingReader.cacheState = XsdCachingReader.CachingReaderState.ReaderClosed;
						result = false;
						goto IL_376;
					}
					switch (xsdCachingReader.coreReader.NodeType)
					{
					case XmlNodeType.Element:
						xsdCachingReader.cacheState = XsdCachingReader.CachingReaderState.ReaderClosed;
						result = false;
						goto IL_376;
					case XmlNodeType.Attribute:
					case XmlNodeType.EntityReference:
					case XmlNodeType.Entity:
					case XmlNodeType.Document:
					case XmlNodeType.DocumentType:
					case XmlNodeType.DocumentFragment:
					case XmlNodeType.Notation:
						goto IL_256;
					case XmlNodeType.Text:
					case XmlNodeType.CDATA:
					case XmlNodeType.ProcessingInstruction:
					case XmlNodeType.Comment:
					case XmlNodeType.Whitespace:
					case XmlNodeType.SignificantWhitespace:
						this.<recordedNode>5__2 = xsdCachingReader.AddContent(xsdCachingReader.coreReader.NodeType);
						this.<>7__wrap2 = this.<recordedNode>5__2;
						awaiter2 = xsdCachingReader.coreReader.GetValueAsync().ConfigureAwait(false).GetAwaiter();
						if (!awaiter2.IsCompleted)
						{
							this.<>1__state = 1;
							this.<>u__2 = awaiter2;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<string>.ConfiguredTaskAwaiter, XsdCachingReader.<ReadAsync>d__100>(ref awaiter2, ref this);
							return;
						}
						break;
					case XmlNodeType.EndElement:
						this.<recordedNode>5__2 = xsdCachingReader.AddContent(xsdCachingReader.coreReader.NodeType);
						this.<recordedNode>5__2.SetItemData(xsdCachingReader.coreReader.LocalName, xsdCachingReader.coreReader.Prefix, xsdCachingReader.coreReader.NamespaceURI, xsdCachingReader.coreReader.Depth);
						this.<recordedNode>5__2.SetLineInfo(xsdCachingReader.lineInfo);
						goto IL_256;
					default:
						goto IL_256;
					}
					IL_212:
					string result2 = awaiter2.GetResult();
					this.<>7__wrap2.SetItemData(result2);
					this.<>7__wrap2 = null;
					this.<recordedNode>5__2.SetLineInfo(xsdCachingReader.lineInfo);
					this.<recordedNode>5__2.Depth = xsdCachingReader.coreReader.Depth;
					IL_256:
					xsdCachingReader.cachedNode = this.<recordedNode>5__2;
					result = true;
					goto IL_376;
					IL_317:
					result = awaiter.GetResult();
				}
				catch (Exception exception)
				{
					this.<>1__state = -2;
					this.<>t__builder.SetException(exception);
					return;
				}
				IL_376:
				this.<>1__state = -2;
				this.<>t__builder.SetResult(result);
			}

			// Token: 0x06000E39 RID: 3641 RVA: 0x0005C658 File Offset: 0x0005A858
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04000F26 RID: 3878
			public int <>1__state;

			// Token: 0x04000F27 RID: 3879
			public AsyncTaskMethodBuilder<bool> <>t__builder;

			// Token: 0x04000F28 RID: 3880
			public XsdCachingReader <>4__this;

			// Token: 0x04000F29 RID: 3881
			private ValidatingReaderNodeData <recordedNode>5__2;

			// Token: 0x04000F2A RID: 3882
			private ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter <>u__1;

			// Token: 0x04000F2B RID: 3883
			private ValidatingReaderNodeData <>7__wrap2;

			// Token: 0x04000F2C RID: 3884
			private ConfiguredTaskAwaitable<string>.ConfiguredTaskAwaiter <>u__2;
		}

		// Token: 0x02000189 RID: 393
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <SkipAsync>d__101 : IAsyncStateMachine
		{
			// Token: 0x06000E3A RID: 3642 RVA: 0x0005C668 File Offset: 0x0005A868
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				XsdCachingReader xsdCachingReader = this.<>4__this;
				try
				{
					ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter awaiter;
					switch (num)
					{
					case 0:
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_CF;
					case 1:
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_14E;
					case 2:
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_1D2;
					default:
					{
						XmlNodeType nodeType = xsdCachingReader.cachedNode.NodeType;
						if (nodeType != XmlNodeType.Element)
						{
							if (nodeType != XmlNodeType.Attribute)
							{
								awaiter = xsdCachingReader.ReadAsync().ConfigureAwait(false).GetAwaiter();
								if (!awaiter.IsCompleted)
								{
									this.<>1__state = 2;
									this.<>u__1 = awaiter;
									this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter, XsdCachingReader.<SkipAsync>d__101>(ref awaiter, ref this);
									return;
								}
								goto IL_1D2;
							}
							else
							{
								xsdCachingReader.MoveToElement();
							}
						}
						if (xsdCachingReader.coreReader.NodeType == XmlNodeType.EndElement || xsdCachingReader.readAhead)
						{
							goto IL_EB;
						}
						this.<startDepth>5__2 = xsdCachingReader.coreReader.Depth - 1;
						break;
					}
					}
					IL_6C:
					awaiter = xsdCachingReader.coreReader.ReadAsync().ConfigureAwait(false).GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						this.<>1__state = 0;
						this.<>u__1 = awaiter;
						this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter, XsdCachingReader.<SkipAsync>d__101>(ref awaiter, ref this);
						return;
					}
					IL_CF:
					if (awaiter.GetResult() && xsdCachingReader.coreReader.Depth > this.<startDepth>5__2)
					{
						goto IL_6C;
					}
					IL_EB:
					awaiter = xsdCachingReader.coreReader.ReadAsync().ConfigureAwait(false).GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						this.<>1__state = 1;
						this.<>u__1 = awaiter;
						this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter, XsdCachingReader.<SkipAsync>d__101>(ref awaiter, ref this);
						return;
					}
					IL_14E:
					awaiter.GetResult();
					xsdCachingReader.cacheState = XsdCachingReader.CachingReaderState.ReaderClosed;
					xsdCachingReader.cacheHandler(xsdCachingReader);
					goto IL_1DA;
					IL_1D2:
					awaiter.GetResult();
					IL_1DA:;
				}
				catch (Exception exception)
				{
					this.<>1__state = -2;
					this.<>t__builder.SetException(exception);
					return;
				}
				this.<>1__state = -2;
				this.<>t__builder.SetResult();
			}

			// Token: 0x06000E3B RID: 3643 RVA: 0x0005C89C File Offset: 0x0005AA9C
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04000F2D RID: 3885
			public int <>1__state;

			// Token: 0x04000F2E RID: 3886
			public AsyncTaskMethodBuilder <>t__builder;

			// Token: 0x04000F2F RID: 3887
			public XsdCachingReader <>4__this;

			// Token: 0x04000F30 RID: 3888
			private int <startDepth>5__2;

			// Token: 0x04000F31 RID: 3889
			private ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter <>u__1;
		}
	}
}

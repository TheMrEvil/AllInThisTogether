using System;

namespace System.Xml
{
	// Token: 0x0200005E RID: 94
	internal class ValidatingReaderNodeData
	{
		// Token: 0x060002C8 RID: 712 RVA: 0x0000FF8B File Offset: 0x0000E18B
		public ValidatingReaderNodeData()
		{
			this.Clear(XmlNodeType.None);
		}

		// Token: 0x060002C9 RID: 713 RVA: 0x0000FF9A File Offset: 0x0000E19A
		public ValidatingReaderNodeData(XmlNodeType nodeType)
		{
			this.Clear(nodeType);
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x060002CA RID: 714 RVA: 0x0000FFA9 File Offset: 0x0000E1A9
		// (set) Token: 0x060002CB RID: 715 RVA: 0x0000FFB1 File Offset: 0x0000E1B1
		public string LocalName
		{
			get
			{
				return this.localName;
			}
			set
			{
				this.localName = value;
			}
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x060002CC RID: 716 RVA: 0x0000FFBA File Offset: 0x0000E1BA
		// (set) Token: 0x060002CD RID: 717 RVA: 0x0000FFC2 File Offset: 0x0000E1C2
		public string Namespace
		{
			get
			{
				return this.namespaceUri;
			}
			set
			{
				this.namespaceUri = value;
			}
		}

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x060002CE RID: 718 RVA: 0x0000FFCB File Offset: 0x0000E1CB
		// (set) Token: 0x060002CF RID: 719 RVA: 0x0000FFD3 File Offset: 0x0000E1D3
		public string Prefix
		{
			get
			{
				return this.prefix;
			}
			set
			{
				this.prefix = value;
			}
		}

		// Token: 0x060002D0 RID: 720 RVA: 0x0000FFDC File Offset: 0x0000E1DC
		public string GetAtomizedNameWPrefix(XmlNameTable nameTable)
		{
			if (this.nameWPrefix == null)
			{
				if (this.prefix.Length == 0)
				{
					this.nameWPrefix = this.localName;
				}
				else
				{
					this.nameWPrefix = nameTable.Add(this.prefix + ":" + this.localName);
				}
			}
			return this.nameWPrefix;
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x060002D1 RID: 721 RVA: 0x00010034 File Offset: 0x0000E234
		// (set) Token: 0x060002D2 RID: 722 RVA: 0x0001003C File Offset: 0x0000E23C
		public int Depth
		{
			get
			{
				return this.depth;
			}
			set
			{
				this.depth = value;
			}
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x060002D3 RID: 723 RVA: 0x00010045 File Offset: 0x0000E245
		// (set) Token: 0x060002D4 RID: 724 RVA: 0x0001004D File Offset: 0x0000E24D
		public string RawValue
		{
			get
			{
				return this.rawValue;
			}
			set
			{
				this.rawValue = value;
			}
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x060002D5 RID: 725 RVA: 0x00010056 File Offset: 0x0000E256
		// (set) Token: 0x060002D6 RID: 726 RVA: 0x0001005E File Offset: 0x0000E25E
		public string OriginalStringValue
		{
			get
			{
				return this.originalStringValue;
			}
			set
			{
				this.originalStringValue = value;
			}
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x060002D7 RID: 727 RVA: 0x00010067 File Offset: 0x0000E267
		// (set) Token: 0x060002D8 RID: 728 RVA: 0x0001006F File Offset: 0x0000E26F
		public XmlNodeType NodeType
		{
			get
			{
				return this.nodeType;
			}
			set
			{
				this.nodeType = value;
			}
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x060002D9 RID: 729 RVA: 0x00010078 File Offset: 0x0000E278
		// (set) Token: 0x060002DA RID: 730 RVA: 0x00010080 File Offset: 0x0000E280
		public AttributePSVIInfo AttInfo
		{
			get
			{
				return this.attributePSVIInfo;
			}
			set
			{
				this.attributePSVIInfo = value;
			}
		}

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x060002DB RID: 731 RVA: 0x00010089 File Offset: 0x0000E289
		public int LineNumber
		{
			get
			{
				return this.lineNo;
			}
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x060002DC RID: 732 RVA: 0x00010091 File Offset: 0x0000E291
		public int LinePosition
		{
			get
			{
				return this.linePos;
			}
		}

		// Token: 0x060002DD RID: 733 RVA: 0x0001009C File Offset: 0x0000E29C
		internal void Clear(XmlNodeType nodeType)
		{
			this.nodeType = nodeType;
			this.localName = string.Empty;
			this.prefix = string.Empty;
			this.namespaceUri = string.Empty;
			this.rawValue = string.Empty;
			if (this.attributePSVIInfo != null)
			{
				this.attributePSVIInfo.Reset();
			}
			this.nameWPrefix = null;
			this.lineNo = 0;
			this.linePos = 0;
		}

		// Token: 0x060002DE RID: 734 RVA: 0x00010104 File Offset: 0x0000E304
		internal void ClearName()
		{
			this.localName = string.Empty;
			this.prefix = string.Empty;
			this.namespaceUri = string.Empty;
		}

		// Token: 0x060002DF RID: 735 RVA: 0x00010127 File Offset: 0x0000E327
		internal void SetLineInfo(int lineNo, int linePos)
		{
			this.lineNo = lineNo;
			this.linePos = linePos;
		}

		// Token: 0x060002E0 RID: 736 RVA: 0x00010137 File Offset: 0x0000E337
		internal void SetLineInfo(IXmlLineInfo lineInfo)
		{
			if (lineInfo != null)
			{
				this.lineNo = lineInfo.LineNumber;
				this.linePos = lineInfo.LinePosition;
			}
		}

		// Token: 0x060002E1 RID: 737 RVA: 0x00010154 File Offset: 0x0000E354
		internal void SetItemData(string localName, string prefix, string ns, string value)
		{
			this.localName = localName;
			this.prefix = prefix;
			this.namespaceUri = ns;
			this.rawValue = value;
		}

		// Token: 0x060002E2 RID: 738 RVA: 0x00010173 File Offset: 0x0000E373
		internal void SetItemData(string localName, string prefix, string ns, int depth)
		{
			this.localName = localName;
			this.prefix = prefix;
			this.namespaceUri = ns;
			this.depth = depth;
			this.rawValue = string.Empty;
		}

		// Token: 0x060002E3 RID: 739 RVA: 0x0001019D File Offset: 0x0000E39D
		internal void SetItemData(string value)
		{
			this.SetItemData(value, value);
		}

		// Token: 0x060002E4 RID: 740 RVA: 0x000101A7 File Offset: 0x0000E3A7
		internal void SetItemData(string value, string originalStringValue)
		{
			this.rawValue = value;
			this.originalStringValue = originalStringValue;
		}

		// Token: 0x04000698 RID: 1688
		private string localName;

		// Token: 0x04000699 RID: 1689
		private string namespaceUri;

		// Token: 0x0400069A RID: 1690
		private string prefix;

		// Token: 0x0400069B RID: 1691
		private string nameWPrefix;

		// Token: 0x0400069C RID: 1692
		private string rawValue;

		// Token: 0x0400069D RID: 1693
		private string originalStringValue;

		// Token: 0x0400069E RID: 1694
		private int depth;

		// Token: 0x0400069F RID: 1695
		private AttributePSVIInfo attributePSVIInfo;

		// Token: 0x040006A0 RID: 1696
		private XmlNodeType nodeType;

		// Token: 0x040006A1 RID: 1697
		private int lineNo;

		// Token: 0x040006A2 RID: 1698
		private int linePos;
	}
}

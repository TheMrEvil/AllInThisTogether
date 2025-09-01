using System;
using System.Xml.XPath;

namespace MS.Internal.Xml.Cache
{
	// Token: 0x02000670 RID: 1648
	internal struct XPathNode
	{
		// Token: 0x17000CC4 RID: 3268
		// (get) Token: 0x060042CB RID: 17099 RVA: 0x0016CA28 File Offset: 0x0016AC28
		public XPathNodeType NodeType
		{
			get
			{
				return (XPathNodeType)(this._props & 15U);
			}
		}

		// Token: 0x17000CC5 RID: 3269
		// (get) Token: 0x060042CC RID: 17100 RVA: 0x0016CA33 File Offset: 0x0016AC33
		public string Prefix
		{
			get
			{
				return this._info.Prefix;
			}
		}

		// Token: 0x17000CC6 RID: 3270
		// (get) Token: 0x060042CD RID: 17101 RVA: 0x0016CA40 File Offset: 0x0016AC40
		public string LocalName
		{
			get
			{
				return this._info.LocalName;
			}
		}

		// Token: 0x17000CC7 RID: 3271
		// (get) Token: 0x060042CE RID: 17102 RVA: 0x0016CA4D File Offset: 0x0016AC4D
		public string Name
		{
			get
			{
				if (this.Prefix.Length == 0)
				{
					return this.LocalName;
				}
				return this.Prefix + ":" + this.LocalName;
			}
		}

		// Token: 0x17000CC8 RID: 3272
		// (get) Token: 0x060042CF RID: 17103 RVA: 0x0016CA79 File Offset: 0x0016AC79
		public string NamespaceUri
		{
			get
			{
				return this._info.NamespaceUri;
			}
		}

		// Token: 0x17000CC9 RID: 3273
		// (get) Token: 0x060042D0 RID: 17104 RVA: 0x0016CA86 File Offset: 0x0016AC86
		public XPathDocument Document
		{
			get
			{
				return this._info.Document;
			}
		}

		// Token: 0x17000CCA RID: 3274
		// (get) Token: 0x060042D1 RID: 17105 RVA: 0x0016CA93 File Offset: 0x0016AC93
		public string BaseUri
		{
			get
			{
				return this._info.BaseUri;
			}
		}

		// Token: 0x17000CCB RID: 3275
		// (get) Token: 0x060042D2 RID: 17106 RVA: 0x0016CAA0 File Offset: 0x0016ACA0
		public int LineNumber
		{
			get
			{
				return this._info.LineNumberBase + (int)((this._props & 16776192U) >> 10);
			}
		}

		// Token: 0x17000CCC RID: 3276
		// (get) Token: 0x060042D3 RID: 17107 RVA: 0x0016CABD File Offset: 0x0016ACBD
		public int LinePosition
		{
			get
			{
				return this._info.LinePositionBase + (int)this._posOffset;
			}
		}

		// Token: 0x17000CCD RID: 3277
		// (get) Token: 0x060042D4 RID: 17108 RVA: 0x0016CAD1 File Offset: 0x0016ACD1
		public int CollapsedLinePosition
		{
			get
			{
				return this.LinePosition + (int)(this._props >> 24);
			}
		}

		// Token: 0x17000CCE RID: 3278
		// (get) Token: 0x060042D5 RID: 17109 RVA: 0x0016CAE3 File Offset: 0x0016ACE3
		public XPathNodePageInfo PageInfo
		{
			get
			{
				return this._info.PageInfo;
			}
		}

		// Token: 0x060042D6 RID: 17110 RVA: 0x0016CAF0 File Offset: 0x0016ACF0
		public int GetRoot(out XPathNode[] pageNode)
		{
			return this._info.Document.GetRootNode(out pageNode);
		}

		// Token: 0x060042D7 RID: 17111 RVA: 0x0016CB03 File Offset: 0x0016AD03
		public int GetParent(out XPathNode[] pageNode)
		{
			pageNode = this._info.ParentPage;
			return (int)this._idxParent;
		}

		// Token: 0x060042D8 RID: 17112 RVA: 0x0016CB18 File Offset: 0x0016AD18
		public int GetSibling(out XPathNode[] pageNode)
		{
			pageNode = this._info.SiblingPage;
			return (int)this._idxSibling;
		}

		// Token: 0x060042D9 RID: 17113 RVA: 0x0016CB2D File Offset: 0x0016AD2D
		public int GetSimilarElement(out XPathNode[] pageNode)
		{
			pageNode = this._info.SimilarElementPage;
			return (int)this._idxSimilar;
		}

		// Token: 0x060042DA RID: 17114 RVA: 0x0016CB42 File Offset: 0x0016AD42
		public bool NameMatch(string localName, string namespaceName)
		{
			return this._info.LocalName == localName && this._info.NamespaceUri == namespaceName;
		}

		// Token: 0x060042DB RID: 17115 RVA: 0x0016CB65 File Offset: 0x0016AD65
		public bool ElementMatch(string localName, string namespaceName)
		{
			return this.NodeType == XPathNodeType.Element && this._info.LocalName == localName && this._info.NamespaceUri == namespaceName;
		}

		// Token: 0x17000CCF RID: 3279
		// (get) Token: 0x060042DC RID: 17116 RVA: 0x0016CB94 File Offset: 0x0016AD94
		public bool IsXmlNamespaceNode
		{
			get
			{
				string localName = this._info.LocalName;
				return this.NodeType == XPathNodeType.Namespace && localName.Length == 3 && localName == "xml";
			}
		}

		// Token: 0x17000CD0 RID: 3280
		// (get) Token: 0x060042DD RID: 17117 RVA: 0x0016CBCC File Offset: 0x0016ADCC
		public bool HasSibling
		{
			get
			{
				return this._idxSibling > 0;
			}
		}

		// Token: 0x17000CD1 RID: 3281
		// (get) Token: 0x060042DE RID: 17118 RVA: 0x0016CBD7 File Offset: 0x0016ADD7
		public bool HasCollapsedText
		{
			get
			{
				return (this._props & 128U) > 0U;
			}
		}

		// Token: 0x17000CD2 RID: 3282
		// (get) Token: 0x060042DF RID: 17119 RVA: 0x0016CBE8 File Offset: 0x0016ADE8
		public bool HasAttribute
		{
			get
			{
				return (this._props & 16U) > 0U;
			}
		}

		// Token: 0x17000CD3 RID: 3283
		// (get) Token: 0x060042E0 RID: 17120 RVA: 0x0016CBF6 File Offset: 0x0016ADF6
		public bool HasContentChild
		{
			get
			{
				return (this._props & 32U) > 0U;
			}
		}

		// Token: 0x17000CD4 RID: 3284
		// (get) Token: 0x060042E1 RID: 17121 RVA: 0x0016CC04 File Offset: 0x0016AE04
		public bool HasElementChild
		{
			get
			{
				return (this._props & 64U) > 0U;
			}
		}

		// Token: 0x17000CD5 RID: 3285
		// (get) Token: 0x060042E2 RID: 17122 RVA: 0x0016CC14 File Offset: 0x0016AE14
		public bool IsAttrNmsp
		{
			get
			{
				XPathNodeType nodeType = this.NodeType;
				return nodeType == XPathNodeType.Attribute || nodeType == XPathNodeType.Namespace;
			}
		}

		// Token: 0x17000CD6 RID: 3286
		// (get) Token: 0x060042E3 RID: 17123 RVA: 0x0016CC32 File Offset: 0x0016AE32
		public bool IsText
		{
			get
			{
				return XPathNavigator.IsText(this.NodeType);
			}
		}

		// Token: 0x17000CD7 RID: 3287
		// (get) Token: 0x060042E4 RID: 17124 RVA: 0x0016CC3F File Offset: 0x0016AE3F
		// (set) Token: 0x060042E5 RID: 17125 RVA: 0x0016CC50 File Offset: 0x0016AE50
		public bool HasNamespaceDecls
		{
			get
			{
				return (this._props & 512U) > 0U;
			}
			set
			{
				if (value)
				{
					this._props |= 512U;
					return;
				}
				this._props &= 255U;
			}
		}

		// Token: 0x17000CD8 RID: 3288
		// (get) Token: 0x060042E6 RID: 17126 RVA: 0x0016CC7A File Offset: 0x0016AE7A
		public bool AllowShortcutTag
		{
			get
			{
				return (this._props & 256U) > 0U;
			}
		}

		// Token: 0x17000CD9 RID: 3289
		// (get) Token: 0x060042E7 RID: 17127 RVA: 0x0016CC8B File Offset: 0x0016AE8B
		public int LocalNameHashCode
		{
			get
			{
				return this._info.LocalNameHashCode;
			}
		}

		// Token: 0x17000CDA RID: 3290
		// (get) Token: 0x060042E8 RID: 17128 RVA: 0x0016CC98 File Offset: 0x0016AE98
		public string Value
		{
			get
			{
				return this._value;
			}
		}

		// Token: 0x060042E9 RID: 17129 RVA: 0x0016CCA0 File Offset: 0x0016AEA0
		public void Create(XPathNodePageInfo pageInfo)
		{
			this._info = new XPathNodeInfoAtom(pageInfo);
		}

		// Token: 0x060042EA RID: 17130 RVA: 0x0016CCAE File Offset: 0x0016AEAE
		public void Create(XPathNodeInfoAtom info, XPathNodeType xptyp, int idxParent)
		{
			this._info = info;
			this._props = (uint)xptyp;
			this._idxParent = (ushort)idxParent;
		}

		// Token: 0x060042EB RID: 17131 RVA: 0x0016CCC6 File Offset: 0x0016AEC6
		public void SetLineInfoOffsets(int lineNumOffset, int linePosOffset)
		{
			this._props |= (uint)((uint)lineNumOffset << 10);
			this._posOffset = (ushort)linePosOffset;
		}

		// Token: 0x060042EC RID: 17132 RVA: 0x0016CCE1 File Offset: 0x0016AEE1
		public void SetCollapsedLineInfoOffset(int posOffset)
		{
			this._props |= (uint)((uint)posOffset << 24);
		}

		// Token: 0x060042ED RID: 17133 RVA: 0x0016CCF4 File Offset: 0x0016AEF4
		public void SetValue(string value)
		{
			this._value = value;
		}

		// Token: 0x060042EE RID: 17134 RVA: 0x0016CCFD File Offset: 0x0016AEFD
		public void SetEmptyValue(bool allowShortcutTag)
		{
			this._value = string.Empty;
			if (allowShortcutTag)
			{
				this._props |= 256U;
			}
		}

		// Token: 0x060042EF RID: 17135 RVA: 0x0016CD1F File Offset: 0x0016AF1F
		public void SetCollapsedValue(string value)
		{
			this._value = value;
			this._props |= 160U;
		}

		// Token: 0x060042F0 RID: 17136 RVA: 0x0016CD3A File Offset: 0x0016AF3A
		public void SetParentProperties(XPathNodeType xptyp)
		{
			if (xptyp == XPathNodeType.Attribute)
			{
				this._props |= 16U;
				return;
			}
			this._props |= 32U;
			if (xptyp == XPathNodeType.Element)
			{
				this._props |= 64U;
			}
		}

		// Token: 0x060042F1 RID: 17137 RVA: 0x0016CD74 File Offset: 0x0016AF74
		public void SetSibling(XPathNodeInfoTable infoTable, XPathNode[] pageSibling, int idxSibling)
		{
			this._idxSibling = (ushort)idxSibling;
			if (pageSibling != this._info.SiblingPage)
			{
				this._info = infoTable.Create(this._info.LocalName, this._info.NamespaceUri, this._info.Prefix, this._info.BaseUri, this._info.ParentPage, pageSibling, this._info.SimilarElementPage, this._info.Document, this._info.LineNumberBase, this._info.LinePositionBase);
			}
		}

		// Token: 0x060042F2 RID: 17138 RVA: 0x0016CE08 File Offset: 0x0016B008
		public void SetSimilarElement(XPathNodeInfoTable infoTable, XPathNode[] pageSimilar, int idxSimilar)
		{
			this._idxSimilar = (ushort)idxSimilar;
			if (pageSimilar != this._info.SimilarElementPage)
			{
				this._info = infoTable.Create(this._info.LocalName, this._info.NamespaceUri, this._info.Prefix, this._info.BaseUri, this._info.ParentPage, this._info.SiblingPage, pageSimilar, this._info.Document, this._info.LineNumberBase, this._info.LinePositionBase);
			}
		}

		// Token: 0x04002F24 RID: 12068
		private XPathNodeInfoAtom _info;

		// Token: 0x04002F25 RID: 12069
		private ushort _idxSibling;

		// Token: 0x04002F26 RID: 12070
		private ushort _idxParent;

		// Token: 0x04002F27 RID: 12071
		private ushort _idxSimilar;

		// Token: 0x04002F28 RID: 12072
		private ushort _posOffset;

		// Token: 0x04002F29 RID: 12073
		private uint _props;

		// Token: 0x04002F2A RID: 12074
		private string _value;

		// Token: 0x04002F2B RID: 12075
		private const uint NodeTypeMask = 15U;

		// Token: 0x04002F2C RID: 12076
		private const uint HasAttributeBit = 16U;

		// Token: 0x04002F2D RID: 12077
		private const uint HasContentChildBit = 32U;

		// Token: 0x04002F2E RID: 12078
		private const uint HasElementChildBit = 64U;

		// Token: 0x04002F2F RID: 12079
		private const uint HasCollapsedTextBit = 128U;

		// Token: 0x04002F30 RID: 12080
		private const uint AllowShortcutTagBit = 256U;

		// Token: 0x04002F31 RID: 12081
		private const uint HasNmspDeclsBit = 512U;

		// Token: 0x04002F32 RID: 12082
		private const uint LineNumberMask = 16776192U;

		// Token: 0x04002F33 RID: 12083
		private const int LineNumberShift = 10;

		// Token: 0x04002F34 RID: 12084
		private const int CollapsedPositionShift = 24;

		// Token: 0x04002F35 RID: 12085
		public const int MaxLineNumberOffset = 16383;

		// Token: 0x04002F36 RID: 12086
		public const int MaxLinePositionOffset = 65535;

		// Token: 0x04002F37 RID: 12087
		public const int MaxCollapsedPositionOffset = 255;
	}
}

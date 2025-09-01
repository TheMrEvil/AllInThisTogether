using System;
using System.Text;
using System.Xml.XPath;

namespace MS.Internal.Xml.Cache
{
	// Token: 0x02000674 RID: 1652
	internal sealed class XPathNodeInfoAtom : IEquatable<XPathNodeInfoAtom>
	{
		// Token: 0x06004312 RID: 17170 RVA: 0x0016D65A File Offset: 0x0016B85A
		public XPathNodeInfoAtom(XPathNodePageInfo pageInfo)
		{
			this._pageInfo = pageInfo;
		}

		// Token: 0x06004313 RID: 17171 RVA: 0x0016D66C File Offset: 0x0016B86C
		public XPathNodeInfoAtom(string localName, string namespaceUri, string prefix, string baseUri, XPathNode[] pageParent, XPathNode[] pageSibling, XPathNode[] pageSimilar, XPathDocument doc, int lineNumBase, int linePosBase)
		{
			this.Init(localName, namespaceUri, prefix, baseUri, pageParent, pageSibling, pageSimilar, doc, lineNumBase, linePosBase);
		}

		// Token: 0x06004314 RID: 17172 RVA: 0x0016D698 File Offset: 0x0016B898
		public void Init(string localName, string namespaceUri, string prefix, string baseUri, XPathNode[] pageParent, XPathNode[] pageSibling, XPathNode[] pageSimilar, XPathDocument doc, int lineNumBase, int linePosBase)
		{
			this._localName = localName;
			this._namespaceUri = namespaceUri;
			this._prefix = prefix;
			this._baseUri = baseUri;
			this._pageParent = pageParent;
			this._pageSibling = pageSibling;
			this._pageSimilar = pageSimilar;
			this._doc = doc;
			this._lineNumBase = lineNumBase;
			this._linePosBase = linePosBase;
			this._next = null;
			this._pageInfo = null;
			this._hashCode = 0;
			this._localNameHash = 0;
			for (int i = 0; i < this._localName.Length; i++)
			{
				this._localNameHash += (this._localNameHash << 7 ^ (int)this._localName[i]);
			}
		}

		// Token: 0x17000CE1 RID: 3297
		// (get) Token: 0x06004315 RID: 17173 RVA: 0x0016D746 File Offset: 0x0016B946
		public XPathNodePageInfo PageInfo
		{
			get
			{
				return this._pageInfo;
			}
		}

		// Token: 0x17000CE2 RID: 3298
		// (get) Token: 0x06004316 RID: 17174 RVA: 0x0016D74E File Offset: 0x0016B94E
		public string LocalName
		{
			get
			{
				return this._localName;
			}
		}

		// Token: 0x17000CE3 RID: 3299
		// (get) Token: 0x06004317 RID: 17175 RVA: 0x0016D756 File Offset: 0x0016B956
		public string NamespaceUri
		{
			get
			{
				return this._namespaceUri;
			}
		}

		// Token: 0x17000CE4 RID: 3300
		// (get) Token: 0x06004318 RID: 17176 RVA: 0x0016D75E File Offset: 0x0016B95E
		public string Prefix
		{
			get
			{
				return this._prefix;
			}
		}

		// Token: 0x17000CE5 RID: 3301
		// (get) Token: 0x06004319 RID: 17177 RVA: 0x0016D766 File Offset: 0x0016B966
		public string BaseUri
		{
			get
			{
				return this._baseUri;
			}
		}

		// Token: 0x17000CE6 RID: 3302
		// (get) Token: 0x0600431A RID: 17178 RVA: 0x0016D76E File Offset: 0x0016B96E
		public XPathNode[] SiblingPage
		{
			get
			{
				return this._pageSibling;
			}
		}

		// Token: 0x17000CE7 RID: 3303
		// (get) Token: 0x0600431B RID: 17179 RVA: 0x0016D776 File Offset: 0x0016B976
		public XPathNode[] SimilarElementPage
		{
			get
			{
				return this._pageSimilar;
			}
		}

		// Token: 0x17000CE8 RID: 3304
		// (get) Token: 0x0600431C RID: 17180 RVA: 0x0016D77E File Offset: 0x0016B97E
		public XPathNode[] ParentPage
		{
			get
			{
				return this._pageParent;
			}
		}

		// Token: 0x17000CE9 RID: 3305
		// (get) Token: 0x0600431D RID: 17181 RVA: 0x0016D786 File Offset: 0x0016B986
		public XPathDocument Document
		{
			get
			{
				return this._doc;
			}
		}

		// Token: 0x17000CEA RID: 3306
		// (get) Token: 0x0600431E RID: 17182 RVA: 0x0016D78E File Offset: 0x0016B98E
		public int LineNumberBase
		{
			get
			{
				return this._lineNumBase;
			}
		}

		// Token: 0x17000CEB RID: 3307
		// (get) Token: 0x0600431F RID: 17183 RVA: 0x0016D796 File Offset: 0x0016B996
		public int LinePositionBase
		{
			get
			{
				return this._linePosBase;
			}
		}

		// Token: 0x17000CEC RID: 3308
		// (get) Token: 0x06004320 RID: 17184 RVA: 0x0016D79E File Offset: 0x0016B99E
		public int LocalNameHashCode
		{
			get
			{
				return this._localNameHash;
			}
		}

		// Token: 0x17000CED RID: 3309
		// (get) Token: 0x06004321 RID: 17185 RVA: 0x0016D7A6 File Offset: 0x0016B9A6
		// (set) Token: 0x06004322 RID: 17186 RVA: 0x0016D7AE File Offset: 0x0016B9AE
		public XPathNodeInfoAtom Next
		{
			get
			{
				return this._next;
			}
			set
			{
				this._next = value;
			}
		}

		// Token: 0x06004323 RID: 17187 RVA: 0x0016D7B8 File Offset: 0x0016B9B8
		public override int GetHashCode()
		{
			if (this._hashCode == 0)
			{
				int num = this._localNameHash;
				if (this._pageSibling != null)
				{
					num += (num << 7 ^ this._pageSibling[0].PageInfo.PageNumber);
				}
				if (this._pageParent != null)
				{
					num += (num << 7 ^ this._pageParent[0].PageInfo.PageNumber);
				}
				if (this._pageSimilar != null)
				{
					num += (num << 7 ^ this._pageSimilar[0].PageInfo.PageNumber);
				}
				this._hashCode = ((num == 0) ? 1 : num);
			}
			return this._hashCode;
		}

		// Token: 0x06004324 RID: 17188 RVA: 0x0016D859 File Offset: 0x0016BA59
		public override bool Equals(object other)
		{
			return this.Equals(other as XPathNodeInfoAtom);
		}

		// Token: 0x06004325 RID: 17189 RVA: 0x0016D868 File Offset: 0x0016BA68
		public bool Equals(XPathNodeInfoAtom other)
		{
			return this.GetHashCode() == other.GetHashCode() && this._localName == other._localName && this._pageSibling == other._pageSibling && this._namespaceUri == other._namespaceUri && this._pageParent == other._pageParent && this._pageSimilar == other._pageSimilar && this._prefix == other._prefix && this._baseUri == other._baseUri && this._lineNumBase == other._lineNumBase && this._linePosBase == other._linePosBase;
		}

		// Token: 0x06004326 RID: 17190 RVA: 0x0016D908 File Offset: 0x0016BB08
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("hash=");
			stringBuilder.Append(this.GetHashCode());
			stringBuilder.Append(", ");
			if (this._localName.Length != 0)
			{
				stringBuilder.Append('{');
				stringBuilder.Append(this._namespaceUri);
				stringBuilder.Append('}');
				if (this._prefix.Length != 0)
				{
					stringBuilder.Append(this._prefix);
					stringBuilder.Append(':');
				}
				stringBuilder.Append(this._localName);
				stringBuilder.Append(", ");
			}
			if (this._pageParent != null)
			{
				stringBuilder.Append("parent=");
				stringBuilder.Append(this._pageParent[0].PageInfo.PageNumber);
				stringBuilder.Append(", ");
			}
			if (this._pageSibling != null)
			{
				stringBuilder.Append("sibling=");
				stringBuilder.Append(this._pageSibling[0].PageInfo.PageNumber);
				stringBuilder.Append(", ");
			}
			if (this._pageSimilar != null)
			{
				stringBuilder.Append("similar=");
				stringBuilder.Append(this._pageSimilar[0].PageInfo.PageNumber);
				stringBuilder.Append(", ");
			}
			stringBuilder.Append("lineNum=");
			stringBuilder.Append(this._lineNumBase);
			stringBuilder.Append(", ");
			stringBuilder.Append("linePos=");
			stringBuilder.Append(this._linePosBase);
			return stringBuilder.ToString();
		}

		// Token: 0x04002F3E RID: 12094
		private string _localName;

		// Token: 0x04002F3F RID: 12095
		private string _namespaceUri;

		// Token: 0x04002F40 RID: 12096
		private string _prefix;

		// Token: 0x04002F41 RID: 12097
		private string _baseUri;

		// Token: 0x04002F42 RID: 12098
		private XPathNode[] _pageParent;

		// Token: 0x04002F43 RID: 12099
		private XPathNode[] _pageSibling;

		// Token: 0x04002F44 RID: 12100
		private XPathNode[] _pageSimilar;

		// Token: 0x04002F45 RID: 12101
		private XPathDocument _doc;

		// Token: 0x04002F46 RID: 12102
		private int _lineNumBase;

		// Token: 0x04002F47 RID: 12103
		private int _linePosBase;

		// Token: 0x04002F48 RID: 12104
		private int _hashCode;

		// Token: 0x04002F49 RID: 12105
		private int _localNameHash;

		// Token: 0x04002F4A RID: 12106
		private XPathNodeInfoAtom _next;

		// Token: 0x04002F4B RID: 12107
		private XPathNodePageInfo _pageInfo;
	}
}

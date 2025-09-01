using System;
using System.Xml.XPath;

namespace System.Xml
{
	// Token: 0x02000075 RID: 117
	internal sealed class DataDocumentXPathNavigator : XPathNavigator, IHasXmlNode
	{
		// Token: 0x060004D2 RID: 1234 RVA: 0x00010DDF File Offset: 0x0000EFDF
		internal DataDocumentXPathNavigator(XmlDataDocument doc, XmlNode node)
		{
			this._curNode = new XPathNodePointer(this, doc, node);
			this._temp = new XPathNodePointer(this, doc, node);
			this._doc = doc;
		}

		// Token: 0x060004D3 RID: 1235 RVA: 0x00010E0A File Offset: 0x0000F00A
		private DataDocumentXPathNavigator(DataDocumentXPathNavigator other)
		{
			this._curNode = other._curNode.Clone(this);
			this._temp = other._temp.Clone(this);
			this._doc = other._doc;
		}

		// Token: 0x060004D4 RID: 1236 RVA: 0x00010E42 File Offset: 0x0000F042
		public override XPathNavigator Clone()
		{
			return new DataDocumentXPathNavigator(this);
		}

		// Token: 0x170000EE RID: 238
		// (get) Token: 0x060004D5 RID: 1237 RVA: 0x00010E4A File Offset: 0x0000F04A
		internal XPathNodePointer CurNode
		{
			get
			{
				return this._curNode;
			}
		}

		// Token: 0x170000EF RID: 239
		// (get) Token: 0x060004D6 RID: 1238 RVA: 0x00010E52 File Offset: 0x0000F052
		internal XmlDataDocument Document
		{
			get
			{
				return this._doc;
			}
		}

		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x060004D7 RID: 1239 RVA: 0x00010E5A File Offset: 0x0000F05A
		public override XPathNodeType NodeType
		{
			get
			{
				return this._curNode.NodeType;
			}
		}

		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x060004D8 RID: 1240 RVA: 0x00010E67 File Offset: 0x0000F067
		public override string LocalName
		{
			get
			{
				return this._curNode.LocalName;
			}
		}

		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x060004D9 RID: 1241 RVA: 0x00010E74 File Offset: 0x0000F074
		public override string NamespaceURI
		{
			get
			{
				return this._curNode.NamespaceURI;
			}
		}

		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x060004DA RID: 1242 RVA: 0x00010E81 File Offset: 0x0000F081
		public override string Name
		{
			get
			{
				return this._curNode.Name;
			}
		}

		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x060004DB RID: 1243 RVA: 0x00010E8E File Offset: 0x0000F08E
		public override string Prefix
		{
			get
			{
				return this._curNode.Prefix;
			}
		}

		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x060004DC RID: 1244 RVA: 0x00010E9C File Offset: 0x0000F09C
		public override string Value
		{
			get
			{
				XPathNodeType nodeType = this._curNode.NodeType;
				if (nodeType != XPathNodeType.Element && nodeType != XPathNodeType.Root)
				{
					return this._curNode.Value;
				}
				return this._curNode.InnerText;
			}
		}

		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x060004DD RID: 1245 RVA: 0x00010ED3 File Offset: 0x0000F0D3
		public override string BaseURI
		{
			get
			{
				return this._curNode.BaseURI;
			}
		}

		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x060004DE RID: 1246 RVA: 0x00010EE0 File Offset: 0x0000F0E0
		public override string XmlLang
		{
			get
			{
				return this._curNode.XmlLang;
			}
		}

		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x060004DF RID: 1247 RVA: 0x00010EED File Offset: 0x0000F0ED
		public override bool IsEmptyElement
		{
			get
			{
				return this._curNode.IsEmptyElement;
			}
		}

		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x060004E0 RID: 1248 RVA: 0x00010EFA File Offset: 0x0000F0FA
		public override XmlNameTable NameTable
		{
			get
			{
				return this._doc.NameTable;
			}
		}

		// Token: 0x170000FA RID: 250
		// (get) Token: 0x060004E1 RID: 1249 RVA: 0x00010F07 File Offset: 0x0000F107
		public override bool HasAttributes
		{
			get
			{
				return this._curNode.AttributeCount > 0;
			}
		}

		// Token: 0x060004E2 RID: 1250 RVA: 0x00010F18 File Offset: 0x0000F118
		public override string GetAttribute(string localName, string namespaceURI)
		{
			if (this._curNode.NodeType != XPathNodeType.Element)
			{
				return string.Empty;
			}
			this._temp.MoveTo(this._curNode);
			if (!this._temp.MoveToAttribute(localName, namespaceURI))
			{
				return string.Empty;
			}
			return this._temp.Value;
		}

		// Token: 0x060004E3 RID: 1251 RVA: 0x00010F6B File Offset: 0x0000F16B
		public override string GetNamespace(string name)
		{
			return this._curNode.GetNamespace(name);
		}

		// Token: 0x060004E4 RID: 1252 RVA: 0x00010F79 File Offset: 0x0000F179
		public override bool MoveToNamespace(string name)
		{
			return this._curNode.NodeType == XPathNodeType.Element && this._curNode.MoveToNamespace(name);
		}

		// Token: 0x060004E5 RID: 1253 RVA: 0x00010F97 File Offset: 0x0000F197
		public override bool MoveToFirstNamespace(XPathNamespaceScope namespaceScope)
		{
			return this._curNode.NodeType == XPathNodeType.Element && this._curNode.MoveToFirstNamespace(namespaceScope);
		}

		// Token: 0x060004E6 RID: 1254 RVA: 0x00010FB5 File Offset: 0x0000F1B5
		public override bool MoveToNextNamespace(XPathNamespaceScope namespaceScope)
		{
			return this._curNode.NodeType == XPathNodeType.Namespace && this._curNode.MoveToNextNamespace(namespaceScope);
		}

		// Token: 0x060004E7 RID: 1255 RVA: 0x00010FD3 File Offset: 0x0000F1D3
		public override bool MoveToAttribute(string localName, string namespaceURI)
		{
			return this._curNode.NodeType == XPathNodeType.Element && this._curNode.MoveToAttribute(localName, namespaceURI);
		}

		// Token: 0x060004E8 RID: 1256 RVA: 0x00010FF2 File Offset: 0x0000F1F2
		public override bool MoveToFirstAttribute()
		{
			return this._curNode.NodeType == XPathNodeType.Element && this._curNode.MoveToNextAttribute(true);
		}

		// Token: 0x060004E9 RID: 1257 RVA: 0x00011010 File Offset: 0x0000F210
		public override bool MoveToNextAttribute()
		{
			return this._curNode.NodeType == XPathNodeType.Attribute && this._curNode.MoveToNextAttribute(false);
		}

		// Token: 0x060004EA RID: 1258 RVA: 0x0001102E File Offset: 0x0000F22E
		public override bool MoveToNext()
		{
			return this._curNode.NodeType != XPathNodeType.Attribute && this._curNode.MoveToNextSibling();
		}

		// Token: 0x060004EB RID: 1259 RVA: 0x0001104B File Offset: 0x0000F24B
		public override bool MoveToPrevious()
		{
			return this._curNode.NodeType != XPathNodeType.Attribute && this._curNode.MoveToPreviousSibling();
		}

		// Token: 0x060004EC RID: 1260 RVA: 0x00011068 File Offset: 0x0000F268
		public override bool MoveToFirst()
		{
			return this._curNode.NodeType != XPathNodeType.Attribute && this._curNode.MoveToFirst();
		}

		// Token: 0x170000FB RID: 251
		// (get) Token: 0x060004ED RID: 1261 RVA: 0x00011085 File Offset: 0x0000F285
		public override bool HasChildren
		{
			get
			{
				return this._curNode.HasChildren;
			}
		}

		// Token: 0x060004EE RID: 1262 RVA: 0x00011092 File Offset: 0x0000F292
		public override bool MoveToFirstChild()
		{
			return this._curNode.MoveToFirstChild();
		}

		// Token: 0x060004EF RID: 1263 RVA: 0x0001109F File Offset: 0x0000F29F
		public override bool MoveToParent()
		{
			return this._curNode.MoveToParent();
		}

		// Token: 0x060004F0 RID: 1264 RVA: 0x000110AC File Offset: 0x0000F2AC
		public override void MoveToRoot()
		{
			this._curNode.MoveToRoot();
		}

		// Token: 0x060004F1 RID: 1265 RVA: 0x000110BC File Offset: 0x0000F2BC
		public override bool MoveTo(XPathNavigator other)
		{
			if (other != null)
			{
				DataDocumentXPathNavigator dataDocumentXPathNavigator = other as DataDocumentXPathNavigator;
				if (dataDocumentXPathNavigator != null && this._curNode.MoveTo(dataDocumentXPathNavigator.CurNode))
				{
					this._doc = this._curNode.Document;
					return true;
				}
			}
			return false;
		}

		// Token: 0x060004F2 RID: 1266 RVA: 0x00006D64 File Offset: 0x00004F64
		public override bool MoveToId(string id)
		{
			return false;
		}

		// Token: 0x060004F3 RID: 1267 RVA: 0x00011100 File Offset: 0x0000F300
		public override bool IsSamePosition(XPathNavigator other)
		{
			if (other != null)
			{
				DataDocumentXPathNavigator dataDocumentXPathNavigator = other as DataDocumentXPathNavigator;
				if (dataDocumentXPathNavigator != null && this._doc == dataDocumentXPathNavigator.Document && this._curNode.IsSamePosition(dataDocumentXPathNavigator.CurNode))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060004F4 RID: 1268 RVA: 0x0001113E File Offset: 0x0000F33E
		XmlNode IHasXmlNode.GetNode()
		{
			return this._curNode.Node;
		}

		// Token: 0x060004F5 RID: 1269 RVA: 0x0001114C File Offset: 0x0000F34C
		public override XmlNodeOrder ComparePosition(XPathNavigator other)
		{
			if (other == null)
			{
				return XmlNodeOrder.Unknown;
			}
			DataDocumentXPathNavigator dataDocumentXPathNavigator = other as DataDocumentXPathNavigator;
			if (dataDocumentXPathNavigator != null && dataDocumentXPathNavigator.Document == this._doc)
			{
				return this._curNode.ComparePosition(dataDocumentXPathNavigator.CurNode);
			}
			return XmlNodeOrder.Unknown;
		}

		// Token: 0x0400062F RID: 1583
		private readonly XPathNodePointer _curNode;

		// Token: 0x04000630 RID: 1584
		private XmlDataDocument _doc;

		// Token: 0x04000631 RID: 1585
		private readonly XPathNodePointer _temp;
	}
}

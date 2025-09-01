using System;
using System.Collections.Generic;
using System.Threading;
using System.Xml.Linq;

namespace System.Xml.XPath
{
	// Token: 0x02000005 RID: 5
	internal class XNodeNavigator : XPathNavigator, IXmlLineInfo
	{
		// Token: 0x0600000D RID: 13 RVA: 0x0000211D File Offset: 0x0000031D
		public XNodeNavigator(XNode node, XmlNameTable nameTable)
		{
			this._source = node;
			this._nameTable = ((nameTable != null) ? nameTable : XNodeNavigator.CreateNameTable());
		}

		// Token: 0x0600000E RID: 14 RVA: 0x0000213D File Offset: 0x0000033D
		public XNodeNavigator(XNodeNavigator other)
		{
			this._source = other._source;
			this._parent = other._parent;
			this._nameTable = other._nameTable;
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x0600000F RID: 15 RVA: 0x00002169 File Offset: 0x00000369
		public override string BaseURI
		{
			get
			{
				if (this._source != null)
				{
					return this._source.BaseUri;
				}
				if (this._parent != null)
				{
					return this._parent.BaseUri;
				}
				return string.Empty;
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000010 RID: 16 RVA: 0x00002198 File Offset: 0x00000398
		public override bool HasAttributes
		{
			get
			{
				XElement xelement = this._source as XElement;
				if (xelement != null)
				{
					using (IEnumerator<XAttribute> enumerator = xelement.Attributes().GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							if (!enumerator.Current.IsNamespaceDeclaration)
							{
								return true;
							}
						}
					}
					return false;
				}
				return false;
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000011 RID: 17 RVA: 0x000021FC File Offset: 0x000003FC
		public override bool HasChildren
		{
			get
			{
				XContainer xcontainer = this._source as XContainer;
				if (xcontainer != null)
				{
					foreach (XNode n in xcontainer.Nodes())
					{
						if (XNodeNavigator.IsContent(xcontainer, n))
						{
							return true;
						}
					}
					return false;
				}
				return false;
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000012 RID: 18 RVA: 0x00002264 File Offset: 0x00000464
		public override bool IsEmptyElement
		{
			get
			{
				XElement xelement = this._source as XElement;
				return xelement != null && xelement.IsEmpty;
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000013 RID: 19 RVA: 0x00002288 File Offset: 0x00000488
		public override string LocalName
		{
			get
			{
				return this._nameTable.Add(this.GetLocalName());
			}
		}

		// Token: 0x06000014 RID: 20 RVA: 0x0000229C File Offset: 0x0000049C
		private string GetLocalName()
		{
			XElement xelement = this._source as XElement;
			if (xelement != null)
			{
				return xelement.Name.LocalName;
			}
			XAttribute xattribute = this._source as XAttribute;
			if (xattribute != null)
			{
				if (this._parent != null && xattribute.Name.NamespaceName.Length == 0)
				{
					return string.Empty;
				}
				return xattribute.Name.LocalName;
			}
			else
			{
				XProcessingInstruction xprocessingInstruction = this._source as XProcessingInstruction;
				if (xprocessingInstruction != null)
				{
					return xprocessingInstruction.Target;
				}
				return string.Empty;
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000015 RID: 21 RVA: 0x0000231C File Offset: 0x0000051C
		public override string Name
		{
			get
			{
				string prefix = this.GetPrefix();
				if (prefix.Length == 0)
				{
					return this._nameTable.Add(this.GetLocalName());
				}
				return this._nameTable.Add(prefix + ":" + this.GetLocalName());
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000016 RID: 22 RVA: 0x00002366 File Offset: 0x00000566
		public override string NamespaceURI
		{
			get
			{
				return this._nameTable.Add(this.GetNamespaceURI());
			}
		}

		// Token: 0x06000017 RID: 23 RVA: 0x0000237C File Offset: 0x0000057C
		private string GetNamespaceURI()
		{
			XElement xelement = this._source as XElement;
			if (xelement != null)
			{
				return xelement.Name.NamespaceName;
			}
			XAttribute xattribute = this._source as XAttribute;
			if (xattribute == null)
			{
				return string.Empty;
			}
			if (this._parent != null)
			{
				return string.Empty;
			}
			return xattribute.Name.NamespaceName;
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000018 RID: 24 RVA: 0x000023D2 File Offset: 0x000005D2
		public override XmlNameTable NameTable
		{
			get
			{
				return this._nameTable;
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000019 RID: 25 RVA: 0x000023DC File Offset: 0x000005DC
		public override XPathNodeType NodeType
		{
			get
			{
				if (this._source != null)
				{
					switch (this._source.NodeType)
					{
					case XmlNodeType.Element:
						return XPathNodeType.Element;
					case XmlNodeType.Attribute:
						if (!((XAttribute)this._source).IsNamespaceDeclaration)
						{
							return XPathNodeType.Attribute;
						}
						return XPathNodeType.Namespace;
					case XmlNodeType.ProcessingInstruction:
						return XPathNodeType.ProcessingInstruction;
					case XmlNodeType.Comment:
						return XPathNodeType.Comment;
					case XmlNodeType.Document:
						return XPathNodeType.Root;
					}
					return XPathNodeType.Text;
				}
				return XPathNodeType.Text;
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600001A RID: 26 RVA: 0x0000244C File Offset: 0x0000064C
		public override string Prefix
		{
			get
			{
				return this._nameTable.Add(this.GetPrefix());
			}
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00002460 File Offset: 0x00000660
		private string GetPrefix()
		{
			XElement xelement = this._source as XElement;
			if (xelement == null)
			{
				XAttribute xattribute = this._source as XAttribute;
				if (xattribute != null)
				{
					if (this._parent != null)
					{
						return string.Empty;
					}
					string prefixOfNamespace = xattribute.GetPrefixOfNamespace(xattribute.Name.Namespace);
					if (prefixOfNamespace != null)
					{
						return prefixOfNamespace;
					}
				}
				return string.Empty;
			}
			string prefixOfNamespace2 = xelement.GetPrefixOfNamespace(xelement.Name.Namespace);
			if (prefixOfNamespace2 != null)
			{
				return prefixOfNamespace2;
			}
			return string.Empty;
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600001C RID: 28 RVA: 0x000024D2 File Offset: 0x000006D2
		public override object UnderlyingObject
		{
			get
			{
				return this._source;
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600001D RID: 29 RVA: 0x000024DC File Offset: 0x000006DC
		public override string Value
		{
			get
			{
				if (this._source != null)
				{
					switch (this._source.NodeType)
					{
					case XmlNodeType.Element:
						return ((XElement)this._source).Value;
					case XmlNodeType.Attribute:
						return ((XAttribute)this._source).Value;
					case XmlNodeType.Text:
					case XmlNodeType.CDATA:
						return XNodeNavigator.CollectText((XText)this._source);
					case XmlNodeType.ProcessingInstruction:
						return ((XProcessingInstruction)this._source).Data;
					case XmlNodeType.Comment:
						return ((XComment)this._source).Value;
					case XmlNodeType.Document:
					{
						XElement root = ((XDocument)this._source).Root;
						if (root == null)
						{
							return string.Empty;
						}
						return root.Value;
					}
					}
					return string.Empty;
				}
				return string.Empty;
			}
		}

		// Token: 0x0600001E RID: 30 RVA: 0x000025AF File Offset: 0x000007AF
		public override XPathNavigator Clone()
		{
			return new XNodeNavigator(this);
		}

		// Token: 0x0600001F RID: 31 RVA: 0x000025B8 File Offset: 0x000007B8
		public override bool IsSamePosition(XPathNavigator navigator)
		{
			XNodeNavigator xnodeNavigator = navigator as XNodeNavigator;
			return xnodeNavigator != null && XNodeNavigator.IsSamePosition(this, xnodeNavigator);
		}

		// Token: 0x06000020 RID: 32 RVA: 0x000025D8 File Offset: 0x000007D8
		public override bool MoveTo(XPathNavigator navigator)
		{
			XNodeNavigator xnodeNavigator = navigator as XNodeNavigator;
			if (xnodeNavigator != null)
			{
				this._source = xnodeNavigator._source;
				this._parent = xnodeNavigator._parent;
				return true;
			}
			return false;
		}

		// Token: 0x06000021 RID: 33 RVA: 0x0000260C File Offset: 0x0000080C
		public override bool MoveToAttribute(string localName, string namespaceName)
		{
			XElement xelement = this._source as XElement;
			if (xelement != null)
			{
				foreach (XAttribute xattribute in xelement.Attributes())
				{
					if (xattribute.Name.LocalName == localName && xattribute.Name.NamespaceName == namespaceName && !xattribute.IsNamespaceDeclaration)
					{
						this._source = xattribute;
						return true;
					}
				}
				return false;
			}
			return false;
		}

		// Token: 0x06000022 RID: 34 RVA: 0x000026A0 File Offset: 0x000008A0
		public override bool MoveToChild(string localName, string namespaceName)
		{
			XContainer xcontainer = this._source as XContainer;
			if (xcontainer != null)
			{
				foreach (XElement xelement in xcontainer.Elements())
				{
					if (xelement.Name.LocalName == localName && xelement.Name.NamespaceName == namespaceName)
					{
						this._source = xelement;
						return true;
					}
				}
				return false;
			}
			return false;
		}

		// Token: 0x06000023 RID: 35 RVA: 0x0000272C File Offset: 0x0000092C
		public override bool MoveToChild(XPathNodeType type)
		{
			XContainer xcontainer = this._source as XContainer;
			if (xcontainer != null)
			{
				int num = XNodeNavigator.GetElementContentMask(type);
				if ((24 & num) != 0 && xcontainer.GetParent() == null && xcontainer is XDocument)
				{
					num &= -25;
				}
				foreach (XNode xnode in xcontainer.Nodes())
				{
					if ((1 << (int)xnode.NodeType & num) != 0)
					{
						this._source = xnode;
						return true;
					}
				}
				return false;
			}
			return false;
		}

		// Token: 0x06000024 RID: 36 RVA: 0x000027C4 File Offset: 0x000009C4
		public override bool MoveToFirstAttribute()
		{
			XElement xelement = this._source as XElement;
			if (xelement != null)
			{
				foreach (XAttribute xattribute in xelement.Attributes())
				{
					if (!xattribute.IsNamespaceDeclaration)
					{
						this._source = xattribute;
						return true;
					}
				}
				return false;
			}
			return false;
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00002830 File Offset: 0x00000A30
		public override bool MoveToFirstChild()
		{
			XContainer xcontainer = this._source as XContainer;
			if (xcontainer != null)
			{
				foreach (XNode xnode in xcontainer.Nodes())
				{
					if (XNodeNavigator.IsContent(xcontainer, xnode))
					{
						this._source = xnode;
						return true;
					}
				}
				return false;
			}
			return false;
		}

		// Token: 0x06000026 RID: 38 RVA: 0x0000289C File Offset: 0x00000A9C
		public override bool MoveToFirstNamespace(XPathNamespaceScope scope)
		{
			XElement xelement = this._source as XElement;
			if (xelement != null)
			{
				XAttribute xattribute = null;
				switch (scope)
				{
				case XPathNamespaceScope.All:
					xattribute = XNodeNavigator.GetFirstNamespaceDeclarationGlobal(xelement);
					if (xattribute == null)
					{
						xattribute = XNodeNavigator.GetXmlNamespaceDeclaration();
					}
					break;
				case XPathNamespaceScope.ExcludeXml:
					for (xattribute = XNodeNavigator.GetFirstNamespaceDeclarationGlobal(xelement); xattribute != null; xattribute = XNodeNavigator.GetNextNamespaceDeclarationGlobal(xattribute))
					{
						if (!(xattribute.Name.LocalName == "xml"))
						{
							break;
						}
					}
					break;
				case XPathNamespaceScope.Local:
					xattribute = XNodeNavigator.GetFirstNamespaceDeclarationLocal(xelement);
					break;
				}
				if (xattribute != null)
				{
					this._source = xattribute;
					this._parent = xelement;
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00002927 File Offset: 0x00000B27
		public override bool MoveToId(string id)
		{
			throw new NotSupportedException("This XPathNavigator does not support IDs.");
		}

		// Token: 0x06000028 RID: 40 RVA: 0x00002934 File Offset: 0x00000B34
		public override bool MoveToNamespace(string localName)
		{
			XElement xelement = this._source as XElement;
			if (xelement != null)
			{
				if (localName == "xmlns")
				{
					return false;
				}
				if (localName != null && localName.Length == 0)
				{
					localName = "xmlns";
				}
				for (XAttribute xattribute = XNodeNavigator.GetFirstNamespaceDeclarationGlobal(xelement); xattribute != null; xattribute = XNodeNavigator.GetNextNamespaceDeclarationGlobal(xattribute))
				{
					if (xattribute.Name.LocalName == localName)
					{
						this._source = xattribute;
						this._parent = xelement;
						return true;
					}
				}
				if (localName == "xml")
				{
					this._source = XNodeNavigator.GetXmlNamespaceDeclaration();
					this._parent = xelement;
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000029 RID: 41 RVA: 0x000029CC File Offset: 0x00000BCC
		public override bool MoveToNext()
		{
			XNode xnode = this._source as XNode;
			if (xnode != null)
			{
				XContainer parent = xnode.GetParent();
				if (parent != null)
				{
					XNode nextNode;
					for (XNode xnode2 = xnode; xnode2 != null; xnode2 = nextNode)
					{
						nextNode = xnode2.NextNode;
						if (nextNode == null)
						{
							break;
						}
						if (XNodeNavigator.IsContent(parent, nextNode) && (!(xnode2 is XText) || !(nextNode is XText)))
						{
							this._source = nextNode;
							return true;
						}
					}
				}
			}
			return false;
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00002A2C File Offset: 0x00000C2C
		public override bool MoveToNext(string localName, string namespaceName)
		{
			XNode xnode = this._source as XNode;
			if (xnode != null)
			{
				foreach (XElement xelement in xnode.ElementsAfterSelf())
				{
					if (xelement.Name.LocalName == localName && xelement.Name.NamespaceName == namespaceName)
					{
						this._source = xelement;
						return true;
					}
				}
				return false;
			}
			return false;
		}

		// Token: 0x0600002B RID: 43 RVA: 0x00002AB8 File Offset: 0x00000CB8
		public override bool MoveToNext(XPathNodeType type)
		{
			XNode xnode = this._source as XNode;
			if (xnode != null)
			{
				XContainer parent = xnode.GetParent();
				if (parent != null)
				{
					int num = XNodeNavigator.GetElementContentMask(type);
					if ((24 & num) != 0 && parent.GetParent() == null && parent is XDocument)
					{
						num &= -25;
					}
					XNode nextNode;
					for (XNode xnode2 = xnode; xnode2 != null; xnode2 = nextNode)
					{
						nextNode = xnode2.NextNode;
						if ((1 << (int)nextNode.NodeType & num) != 0 && (!(xnode2 is XText) || !(nextNode is XText)))
						{
							this._source = nextNode;
							return true;
						}
					}
				}
			}
			return false;
		}

		// Token: 0x0600002C RID: 44 RVA: 0x00002B40 File Offset: 0x00000D40
		public override bool MoveToNextAttribute()
		{
			XAttribute xattribute = this._source as XAttribute;
			if (xattribute != null && this._parent == null && (XElement)xattribute.GetParent() != null)
			{
				for (XAttribute nextAttribute = xattribute.NextAttribute; nextAttribute != null; nextAttribute = nextAttribute.NextAttribute)
				{
					if (!nextAttribute.IsNamespaceDeclaration)
					{
						this._source = nextAttribute;
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00002B98 File Offset: 0x00000D98
		public override bool MoveToNextNamespace(XPathNamespaceScope scope)
		{
			XAttribute xattribute = this._source as XAttribute;
			if (xattribute != null && this._parent != null && !XNodeNavigator.IsXmlNamespaceDeclaration(xattribute))
			{
				switch (scope)
				{
				case XPathNamespaceScope.All:
					do
					{
						xattribute = XNodeNavigator.GetNextNamespaceDeclarationGlobal(xattribute);
					}
					while (xattribute != null && XNodeNavigator.HasNamespaceDeclarationInScope(xattribute, this._parent));
					if (xattribute == null && !XNodeNavigator.HasNamespaceDeclarationInScope(XNodeNavigator.GetXmlNamespaceDeclaration(), this._parent))
					{
						xattribute = XNodeNavigator.GetXmlNamespaceDeclaration();
					}
					break;
				case XPathNamespaceScope.ExcludeXml:
					do
					{
						xattribute = XNodeNavigator.GetNextNamespaceDeclarationGlobal(xattribute);
						if (xattribute == null)
						{
							break;
						}
					}
					while (xattribute.Name.LocalName == "xml" || XNodeNavigator.HasNamespaceDeclarationInScope(xattribute, this._parent));
					break;
				case XPathNamespaceScope.Local:
					if (xattribute.GetParent() != this._parent)
					{
						return false;
					}
					xattribute = XNodeNavigator.GetNextNamespaceDeclarationLocal(xattribute);
					break;
				}
				if (xattribute != null)
				{
					this._source = xattribute;
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00002C6C File Offset: 0x00000E6C
		public override bool MoveToParent()
		{
			if (this._parent != null)
			{
				this._source = this._parent;
				this._parent = null;
				return true;
			}
			XNode parent = this._source.GetParent();
			if (parent != null)
			{
				this._source = parent;
				return true;
			}
			return false;
		}

		// Token: 0x0600002F RID: 47 RVA: 0x00002CB0 File Offset: 0x00000EB0
		public override bool MoveToPrevious()
		{
			XNode xnode = this._source as XNode;
			if (xnode != null)
			{
				XContainer parent = xnode.GetParent();
				if (parent != null)
				{
					XNode xnode2 = null;
					foreach (XNode xnode3 in parent.Nodes())
					{
						if (xnode3 == xnode)
						{
							if (xnode2 != null)
							{
								this._source = xnode2;
								return true;
							}
							return false;
						}
						else if (XNodeNavigator.IsContent(parent, xnode3))
						{
							xnode2 = xnode3;
						}
					}
					return false;
				}
			}
			return false;
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00002D3C File Offset: 0x00000F3C
		public override XmlReader ReadSubtree()
		{
			XContainer xcontainer = this._source as XContainer;
			if (xcontainer == null)
			{
				throw new InvalidOperationException(SR.Format("This operation is not valid on a node of type {0}.", this.NodeType));
			}
			return xcontainer.CreateReader();
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00002D6C File Offset: 0x00000F6C
		bool IXmlLineInfo.HasLineInfo()
		{
			IXmlLineInfo source = this._source;
			return source != null && source.HasLineInfo();
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000032 RID: 50 RVA: 0x00002D8C File Offset: 0x00000F8C
		int IXmlLineInfo.LineNumber
		{
			get
			{
				IXmlLineInfo source = this._source;
				if (source != null)
				{
					return source.LineNumber;
				}
				return 0;
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000033 RID: 51 RVA: 0x00002DAC File Offset: 0x00000FAC
		int IXmlLineInfo.LinePosition
		{
			get
			{
				IXmlLineInfo source = this._source;
				if (source != null)
				{
					return source.LinePosition;
				}
				return 0;
			}
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00002DCC File Offset: 0x00000FCC
		private static string CollectText(XText n)
		{
			string text = n.Value;
			if (n.GetParent() != null)
			{
				foreach (XNode xnode in n.NodesAfterSelf())
				{
					XText xtext = xnode as XText;
					if (xtext == null)
					{
						break;
					}
					text += xtext.Value;
				}
			}
			return text;
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00002E3C File Offset: 0x0000103C
		private static XmlNameTable CreateNameTable()
		{
			NameTable nameTable = new NameTable();
			nameTable.Add(string.Empty);
			nameTable.Add(XNodeNavigator.xmlnsPrefixNamespace);
			nameTable.Add(XNodeNavigator.xmlPrefixNamespace);
			return nameTable;
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00002E67 File Offset: 0x00001067
		private static bool IsContent(XContainer c, XNode n)
		{
			return c.GetParent() != null || c is XElement || (1 << (int)n.NodeType & 386) != 0;
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00002E8F File Offset: 0x0000108F
		private static bool IsSamePosition(XNodeNavigator n1, XNodeNavigator n2)
		{
			return n1._source == n2._source && n1._source.GetParent() == n2._source.GetParent();
		}

		// Token: 0x06000038 RID: 56 RVA: 0x00002EB9 File Offset: 0x000010B9
		private static bool IsXmlNamespaceDeclaration(XAttribute a)
		{
			return a == XNodeNavigator.GetXmlNamespaceDeclaration();
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00002EC3 File Offset: 0x000010C3
		private static int GetElementContentMask(XPathNodeType type)
		{
			return XNodeNavigator.s_ElementContentMasks[(int)type];
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00002ECC File Offset: 0x000010CC
		private static XAttribute GetFirstNamespaceDeclarationGlobal(XElement e)
		{
			XAttribute firstNamespaceDeclarationLocal;
			for (;;)
			{
				firstNamespaceDeclarationLocal = XNodeNavigator.GetFirstNamespaceDeclarationLocal(e);
				if (firstNamespaceDeclarationLocal != null)
				{
					break;
				}
				e = e.Parent;
				if (e == null)
				{
					goto Block_1;
				}
			}
			return firstNamespaceDeclarationLocal;
			Block_1:
			return null;
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00002EF4 File Offset: 0x000010F4
		private static XAttribute GetFirstNamespaceDeclarationLocal(XElement e)
		{
			foreach (XAttribute xattribute in e.Attributes())
			{
				if (xattribute.IsNamespaceDeclaration)
				{
					return xattribute;
				}
			}
			return null;
		}

		// Token: 0x0600003C RID: 60 RVA: 0x00002F4C File Offset: 0x0000114C
		private static XAttribute GetNextNamespaceDeclarationGlobal(XAttribute a)
		{
			XElement xelement = (XElement)a.GetParent();
			if (xelement == null)
			{
				return null;
			}
			XAttribute nextNamespaceDeclarationLocal = XNodeNavigator.GetNextNamespaceDeclarationLocal(a);
			if (nextNamespaceDeclarationLocal != null)
			{
				return nextNamespaceDeclarationLocal;
			}
			xelement = xelement.Parent;
			if (xelement == null)
			{
				return null;
			}
			return XNodeNavigator.GetFirstNamespaceDeclarationGlobal(xelement);
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00002F88 File Offset: 0x00001188
		private static XAttribute GetNextNamespaceDeclarationLocal(XAttribute a)
		{
			if (a.Parent == null)
			{
				return null;
			}
			for (a = a.NextAttribute; a != null; a = a.NextAttribute)
			{
				if (a.IsNamespaceDeclaration)
				{
					return a;
				}
			}
			return null;
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00002FB4 File Offset: 0x000011B4
		private static XAttribute GetXmlNamespaceDeclaration()
		{
			if (XNodeNavigator.s_XmlNamespaceDeclaration == null)
			{
				Interlocked.CompareExchange<XAttribute>(ref XNodeNavigator.s_XmlNamespaceDeclaration, new XAttribute(XNamespace.Xmlns.GetName("xml"), XNodeNavigator.xmlPrefixNamespace), null);
			}
			return XNodeNavigator.s_XmlNamespaceDeclaration;
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00002FE8 File Offset: 0x000011E8
		private static bool HasNamespaceDeclarationInScope(XAttribute a, XElement e)
		{
			XName name = a.Name;
			while (e != null && e != a.GetParent())
			{
				if (e.Attribute(name) != null)
				{
					return true;
				}
				e = e.Parent;
			}
			return false;
		}

		// Token: 0x06000040 RID: 64 RVA: 0x0000301E File Offset: 0x0000121E
		// Note: this type is marked as 'beforefieldinit'.
		static XNodeNavigator()
		{
		}

		// Token: 0x04000020 RID: 32
		internal static readonly string xmlPrefixNamespace = XNamespace.Xml.NamespaceName;

		// Token: 0x04000021 RID: 33
		internal static readonly string xmlnsPrefixNamespace = XNamespace.Xmlns.NamespaceName;

		// Token: 0x04000022 RID: 34
		private const int DocumentContentMask = 386;

		// Token: 0x04000023 RID: 35
		private static readonly int[] s_ElementContentMasks = new int[]
		{
			0,
			2,
			0,
			0,
			24,
			0,
			0,
			128,
			256,
			410
		};

		// Token: 0x04000024 RID: 36
		private new const int TextMask = 24;

		// Token: 0x04000025 RID: 37
		private static XAttribute s_XmlNamespaceDeclaration;

		// Token: 0x04000026 RID: 38
		private XObject _source;

		// Token: 0x04000027 RID: 39
		private XElement _parent;

		// Token: 0x04000028 RID: 40
		private XmlNameTable _nameTable;
	}
}

using System;

namespace System.Xml.Linq
{
	// Token: 0x02000059 RID: 89
	internal class XNodeReader : XmlReader, IXmlLineInfo
	{
		// Token: 0x0600031B RID: 795 RVA: 0x0000E304 File Offset: 0x0000C504
		internal XNodeReader(XNode node, XmlNameTable nameTable, ReaderOptions options)
		{
			this._source = node;
			this._root = node;
			this._nameTable = ((nameTable != null) ? nameTable : XNodeReader.CreateNameTable());
			this._omitDuplicateNamespaces = ((options & ReaderOptions.OmitDuplicateNamespaces) != ReaderOptions.None);
		}

		// Token: 0x0600031C RID: 796 RVA: 0x0000E33A File Offset: 0x0000C53A
		internal XNodeReader(XNode node, XmlNameTable nameTable) : this(node, nameTable, ((node.GetSaveOptionsFromAnnotations() & SaveOptions.OmitDuplicateNamespaces) != SaveOptions.None) ? ReaderOptions.OmitDuplicateNamespaces : ReaderOptions.None)
		{
		}

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x0600031D RID: 797 RVA: 0x0000E354 File Offset: 0x0000C554
		public override int AttributeCount
		{
			get
			{
				if (!this.IsInteractive)
				{
					return 0;
				}
				int num = 0;
				XElement elementInAttributeScope = this.GetElementInAttributeScope();
				if (elementInAttributeScope != null)
				{
					XAttribute xattribute = elementInAttributeScope.lastAttr;
					if (xattribute != null)
					{
						do
						{
							xattribute = xattribute.next;
							if (!this._omitDuplicateNamespaces || !this.IsDuplicateNamespaceAttribute(xattribute))
							{
								num++;
							}
						}
						while (xattribute != elementInAttributeScope.lastAttr);
					}
				}
				return num;
			}
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x0600031E RID: 798 RVA: 0x0000E3A8 File Offset: 0x0000C5A8
		public override string BaseURI
		{
			get
			{
				XObject xobject = this._source as XObject;
				if (xobject != null)
				{
					return xobject.BaseUri;
				}
				xobject = (this._parent as XObject);
				if (xobject != null)
				{
					return xobject.BaseUri;
				}
				return string.Empty;
			}
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x0600031F RID: 799 RVA: 0x0000E3E8 File Offset: 0x0000C5E8
		public override int Depth
		{
			get
			{
				if (!this.IsInteractive)
				{
					return 0;
				}
				XObject xobject = this._source as XObject;
				if (xobject != null)
				{
					return XNodeReader.GetDepth(xobject);
				}
				xobject = (this._parent as XObject);
				if (xobject != null)
				{
					return XNodeReader.GetDepth(xobject) + 1;
				}
				return 0;
			}
		}

		// Token: 0x06000320 RID: 800 RVA: 0x0000E430 File Offset: 0x0000C630
		private static int GetDepth(XObject o)
		{
			int num = 0;
			while (o.parent != null)
			{
				num++;
				o = o.parent;
			}
			if (o is XDocument)
			{
				num--;
			}
			return num;
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x06000321 RID: 801 RVA: 0x0000E462 File Offset: 0x0000C662
		public override bool EOF
		{
			get
			{
				return this._state == ReadState.EndOfFile;
			}
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x06000322 RID: 802 RVA: 0x0000E470 File Offset: 0x0000C670
		public override bool HasAttributes
		{
			get
			{
				if (!this.IsInteractive)
				{
					return false;
				}
				XElement elementInAttributeScope = this.GetElementInAttributeScope();
				return elementInAttributeScope != null && elementInAttributeScope.lastAttr != null && (!this._omitDuplicateNamespaces || this.GetFirstNonDuplicateNamespaceAttribute(elementInAttributeScope.lastAttr.next) != null);
			}
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x06000323 RID: 803 RVA: 0x0000E4BC File Offset: 0x0000C6BC
		public override bool HasValue
		{
			get
			{
				if (!this.IsInteractive)
				{
					return false;
				}
				XObject xobject = this._source as XObject;
				if (xobject != null)
				{
					switch (xobject.NodeType)
					{
					case XmlNodeType.Attribute:
					case XmlNodeType.Text:
					case XmlNodeType.CDATA:
					case XmlNodeType.ProcessingInstruction:
					case XmlNodeType.Comment:
					case XmlNodeType.DocumentType:
						return true;
					}
					return false;
				}
				return true;
			}
		}

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x06000324 RID: 804 RVA: 0x0000E51C File Offset: 0x0000C71C
		public override bool IsEmptyElement
		{
			get
			{
				if (!this.IsInteractive)
				{
					return false;
				}
				XElement xelement = this._source as XElement;
				return xelement != null && xelement.IsEmpty;
			}
		}

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x06000325 RID: 805 RVA: 0x0000E54A File Offset: 0x0000C74A
		public override string LocalName
		{
			get
			{
				return this._nameTable.Add(this.GetLocalName());
			}
		}

		// Token: 0x06000326 RID: 806 RVA: 0x0000E560 File Offset: 0x0000C760
		private string GetLocalName()
		{
			if (!this.IsInteractive)
			{
				return string.Empty;
			}
			XElement xelement = this._source as XElement;
			if (xelement != null)
			{
				return xelement.Name.LocalName;
			}
			XAttribute xattribute = this._source as XAttribute;
			if (xattribute != null)
			{
				return xattribute.Name.LocalName;
			}
			XProcessingInstruction xprocessingInstruction = this._source as XProcessingInstruction;
			if (xprocessingInstruction != null)
			{
				return xprocessingInstruction.Target;
			}
			XDocumentType xdocumentType = this._source as XDocumentType;
			if (xdocumentType != null)
			{
				return xdocumentType.Name;
			}
			return string.Empty;
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x06000327 RID: 807 RVA: 0x0000E5E4 File Offset: 0x0000C7E4
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

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x06000328 RID: 808 RVA: 0x0000E62E File Offset: 0x0000C82E
		public override string NamespaceURI
		{
			get
			{
				return this._nameTable.Add(this.GetNamespaceURI());
			}
		}

		// Token: 0x06000329 RID: 809 RVA: 0x0000E644 File Offset: 0x0000C844
		private string GetNamespaceURI()
		{
			if (!this.IsInteractive)
			{
				return string.Empty;
			}
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
			string namespaceName = xattribute.Name.NamespaceName;
			if (namespaceName.Length == 0 && xattribute.Name.LocalName == "xmlns")
			{
				return "http://www.w3.org/2000/xmlns/";
			}
			return namespaceName;
		}

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x0600032A RID: 810 RVA: 0x0000E6C1 File Offset: 0x0000C8C1
		public override XmlNameTable NameTable
		{
			get
			{
				return this._nameTable;
			}
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x0600032B RID: 811 RVA: 0x0000E6CC File Offset: 0x0000C8CC
		public override XmlNodeType NodeType
		{
			get
			{
				if (!this.IsInteractive)
				{
					return XmlNodeType.None;
				}
				XObject xobject = this._source as XObject;
				if (xobject != null)
				{
					if (this.IsEndElement)
					{
						return XmlNodeType.EndElement;
					}
					XmlNodeType nodeType = xobject.NodeType;
					if (nodeType != XmlNodeType.Text)
					{
						return nodeType;
					}
					if (xobject.parent != null && xobject.parent.parent == null && xobject.parent is XDocument)
					{
						return XmlNodeType.Whitespace;
					}
					return XmlNodeType.Text;
				}
				else
				{
					if (this._parent is XDocument)
					{
						return XmlNodeType.Whitespace;
					}
					return XmlNodeType.Text;
				}
			}
		}

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x0600032C RID: 812 RVA: 0x0000E742 File Offset: 0x0000C942
		public override string Prefix
		{
			get
			{
				return this._nameTable.Add(this.GetPrefix());
			}
		}

		// Token: 0x0600032D RID: 813 RVA: 0x0000E758 File Offset: 0x0000C958
		private string GetPrefix()
		{
			if (!this.IsInteractive)
			{
				return string.Empty;
			}
			XElement xelement = this._source as XElement;
			if (xelement == null)
			{
				XAttribute xattribute = this._source as XAttribute;
				if (xattribute != null)
				{
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

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x0600032E RID: 814 RVA: 0x0000E7CA File Offset: 0x0000C9CA
		public override ReadState ReadState
		{
			get
			{
				return this._state;
			}
		}

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x0600032F RID: 815 RVA: 0x0000E7D2 File Offset: 0x0000C9D2
		public override XmlReaderSettings Settings
		{
			get
			{
				return new XmlReaderSettings
				{
					CheckCharacters = false
				};
			}
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x06000330 RID: 816 RVA: 0x0000E7E0 File Offset: 0x0000C9E0
		public override string Value
		{
			get
			{
				if (!this.IsInteractive)
				{
					return string.Empty;
				}
				XObject xobject = this._source as XObject;
				if (xobject != null)
				{
					switch (xobject.NodeType)
					{
					case XmlNodeType.Attribute:
						return ((XAttribute)xobject).Value;
					case XmlNodeType.Text:
					case XmlNodeType.CDATA:
						return ((XText)xobject).Value;
					case XmlNodeType.ProcessingInstruction:
						return ((XProcessingInstruction)xobject).Data;
					case XmlNodeType.Comment:
						return ((XComment)xobject).Value;
					case XmlNodeType.DocumentType:
						return ((XDocumentType)xobject).InternalSubset;
					}
					return string.Empty;
				}
				return (string)this._source;
			}
		}

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x06000331 RID: 817 RVA: 0x0000E88C File Offset: 0x0000CA8C
		public override string XmlLang
		{
			get
			{
				if (!this.IsInteractive)
				{
					return string.Empty;
				}
				XElement xelement = this.GetElementInScope();
				if (xelement != null)
				{
					XName name = XNamespace.Xml.GetName("lang");
					XAttribute xattribute;
					for (;;)
					{
						xattribute = xelement.Attribute(name);
						if (xattribute != null)
						{
							break;
						}
						xelement = (xelement.parent as XElement);
						if (xelement == null)
						{
							goto IL_49;
						}
					}
					return xattribute.Value;
				}
				IL_49:
				return string.Empty;
			}
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x06000332 RID: 818 RVA: 0x0000E8E8 File Offset: 0x0000CAE8
		public override XmlSpace XmlSpace
		{
			get
			{
				if (!this.IsInteractive)
				{
					return XmlSpace.None;
				}
				XElement xelement = this.GetElementInScope();
				if (xelement != null)
				{
					XName name = XNamespace.Xml.GetName("space");
					for (;;)
					{
						XAttribute xattribute = xelement.Attribute(name);
						if (xattribute != null)
						{
							string a = xattribute.Value.Trim(XNodeReader.s_WhitespaceChars);
							if (a == "preserve")
							{
								break;
							}
							if (a == "default")
							{
								return XmlSpace.Default;
							}
						}
						xelement = (xelement.parent as XElement);
						if (xelement == null)
						{
							return XmlSpace.None;
						}
					}
					return XmlSpace.Preserve;
				}
				return XmlSpace.None;
			}
		}

		// Token: 0x06000333 RID: 819 RVA: 0x0000E965 File Offset: 0x0000CB65
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.ReadState != ReadState.Closed)
			{
				this.Close();
			}
		}

		// Token: 0x06000334 RID: 820 RVA: 0x0000E979 File Offset: 0x0000CB79
		public override void Close()
		{
			this._source = null;
			this._parent = null;
			this._root = null;
			this._state = ReadState.Closed;
		}

		// Token: 0x06000335 RID: 821 RVA: 0x0000E998 File Offset: 0x0000CB98
		public override string GetAttribute(string name)
		{
			if (!this.IsInteractive)
			{
				return null;
			}
			XElement elementInAttributeScope = this.GetElementInAttributeScope();
			if (elementInAttributeScope != null)
			{
				string b;
				string b2;
				XNodeReader.GetNameInAttributeScope(name, elementInAttributeScope, out b, out b2);
				XAttribute xattribute = elementInAttributeScope.lastAttr;
				if (xattribute != null)
				{
					for (;;)
					{
						xattribute = xattribute.next;
						if (xattribute.Name.LocalName == b && xattribute.Name.NamespaceName == b2)
						{
							break;
						}
						if (xattribute == elementInAttributeScope.lastAttr)
						{
							goto IL_82;
						}
					}
					if (this._omitDuplicateNamespaces && this.IsDuplicateNamespaceAttribute(xattribute))
					{
						return null;
					}
					return xattribute.Value;
				}
				IL_82:
				return null;
			}
			XDocumentType xdocumentType = this._source as XDocumentType;
			if (xdocumentType != null)
			{
				if (name == "PUBLIC")
				{
					return xdocumentType.PublicId;
				}
				if (name == "SYSTEM")
				{
					return xdocumentType.SystemId;
				}
			}
			return null;
		}

		// Token: 0x06000336 RID: 822 RVA: 0x0000EA64 File Offset: 0x0000CC64
		public override string GetAttribute(string localName, string namespaceName)
		{
			if (!this.IsInteractive)
			{
				return null;
			}
			XElement elementInAttributeScope = this.GetElementInAttributeScope();
			if (elementInAttributeScope != null)
			{
				if (localName == "xmlns")
				{
					if (namespaceName != null && namespaceName.Length == 0)
					{
						return null;
					}
					if (namespaceName == "http://www.w3.org/2000/xmlns/")
					{
						namespaceName = string.Empty;
					}
				}
				XAttribute xattribute = elementInAttributeScope.lastAttr;
				if (xattribute != null)
				{
					for (;;)
					{
						xattribute = xattribute.next;
						if (xattribute.Name.LocalName == localName && xattribute.Name.NamespaceName == namespaceName)
						{
							break;
						}
						if (xattribute == elementInAttributeScope.lastAttr)
						{
							goto IL_9F;
						}
					}
					if (this._omitDuplicateNamespaces && this.IsDuplicateNamespaceAttribute(xattribute))
					{
						return null;
					}
					return xattribute.Value;
				}
			}
			IL_9F:
			return null;
		}

		// Token: 0x06000337 RID: 823 RVA: 0x0000EB14 File Offset: 0x0000CD14
		public override string GetAttribute(int index)
		{
			if (!this.IsInteractive)
			{
				return null;
			}
			if (index < 0)
			{
				return null;
			}
			XElement elementInAttributeScope = this.GetElementInAttributeScope();
			if (elementInAttributeScope != null)
			{
				XAttribute xattribute = elementInAttributeScope.lastAttr;
				if (xattribute != null)
				{
					for (;;)
					{
						xattribute = xattribute.next;
						if ((!this._omitDuplicateNamespaces || !this.IsDuplicateNamespaceAttribute(xattribute)) && index-- == 0)
						{
							break;
						}
						if (xattribute == elementInAttributeScope.lastAttr)
						{
							goto IL_54;
						}
					}
					return xattribute.Value;
				}
			}
			IL_54:
			return null;
		}

		// Token: 0x06000338 RID: 824 RVA: 0x0000EB78 File Offset: 0x0000CD78
		public override string LookupNamespace(string prefix)
		{
			if (!this.IsInteractive)
			{
				return null;
			}
			if (prefix == null)
			{
				return null;
			}
			XElement elementInScope = this.GetElementInScope();
			if (elementInScope != null)
			{
				XNamespace xnamespace = (prefix.Length == 0) ? elementInScope.GetDefaultNamespace() : elementInScope.GetNamespaceOfPrefix(prefix);
				if (xnamespace != null)
				{
					return this._nameTable.Add(xnamespace.NamespaceName);
				}
			}
			return null;
		}

		// Token: 0x06000339 RID: 825 RVA: 0x0000EBD4 File Offset: 0x0000CDD4
		public override bool MoveToAttribute(string name)
		{
			if (!this.IsInteractive)
			{
				return false;
			}
			XElement elementInAttributeScope = this.GetElementInAttributeScope();
			if (elementInAttributeScope != null)
			{
				string b;
				string b2;
				XNodeReader.GetNameInAttributeScope(name, elementInAttributeScope, out b, out b2);
				XAttribute xattribute = elementInAttributeScope.lastAttr;
				if (xattribute != null)
				{
					for (;;)
					{
						xattribute = xattribute.next;
						if (xattribute.Name.LocalName == b && xattribute.Name.NamespaceName == b2)
						{
							break;
						}
						if (xattribute == elementInAttributeScope.lastAttr)
						{
							return false;
						}
					}
					if (this._omitDuplicateNamespaces && this.IsDuplicateNamespaceAttribute(xattribute))
					{
						return false;
					}
					this._source = xattribute;
					this._parent = null;
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600033A RID: 826 RVA: 0x0000EC64 File Offset: 0x0000CE64
		public override bool MoveToAttribute(string localName, string namespaceName)
		{
			if (!this.IsInteractive)
			{
				return false;
			}
			XElement elementInAttributeScope = this.GetElementInAttributeScope();
			if (elementInAttributeScope != null)
			{
				if (localName == "xmlns")
				{
					if (namespaceName != null && namespaceName.Length == 0)
					{
						return false;
					}
					if (namespaceName == "http://www.w3.org/2000/xmlns/")
					{
						namespaceName = string.Empty;
					}
				}
				XAttribute xattribute = elementInAttributeScope.lastAttr;
				if (xattribute != null)
				{
					for (;;)
					{
						xattribute = xattribute.next;
						if (xattribute.Name.LocalName == localName && xattribute.Name.NamespaceName == namespaceName)
						{
							break;
						}
						if (xattribute == elementInAttributeScope.lastAttr)
						{
							return false;
						}
					}
					if (this._omitDuplicateNamespaces && this.IsDuplicateNamespaceAttribute(xattribute))
					{
						return false;
					}
					this._source = xattribute;
					this._parent = null;
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600033B RID: 827 RVA: 0x0000ED1C File Offset: 0x0000CF1C
		public override void MoveToAttribute(int index)
		{
			if (!this.IsInteractive)
			{
				return;
			}
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			XElement elementInAttributeScope = this.GetElementInAttributeScope();
			if (elementInAttributeScope != null)
			{
				XAttribute xattribute = elementInAttributeScope.lastAttr;
				if (xattribute != null)
				{
					for (;;)
					{
						xattribute = xattribute.next;
						if ((!this._omitDuplicateNamespaces || !this.IsDuplicateNamespaceAttribute(xattribute)) && index-- == 0)
						{
							break;
						}
						if (xattribute == elementInAttributeScope.lastAttr)
						{
							goto IL_64;
						}
					}
					this._source = xattribute;
					this._parent = null;
					return;
				}
			}
			IL_64:
			throw new ArgumentOutOfRangeException("index");
		}

		// Token: 0x0600033C RID: 828 RVA: 0x0000ED98 File Offset: 0x0000CF98
		public override bool MoveToElement()
		{
			if (!this.IsInteractive)
			{
				return false;
			}
			XAttribute xattribute = this._source as XAttribute;
			if (xattribute == null)
			{
				xattribute = (this._parent as XAttribute);
			}
			if (xattribute != null && xattribute.parent != null)
			{
				this._source = xattribute.parent;
				this._parent = null;
				return true;
			}
			return false;
		}

		// Token: 0x0600033D RID: 829 RVA: 0x0000EDEC File Offset: 0x0000CFEC
		public override bool MoveToFirstAttribute()
		{
			if (!this.IsInteractive)
			{
				return false;
			}
			XElement elementInAttributeScope = this.GetElementInAttributeScope();
			if (elementInAttributeScope != null && elementInAttributeScope.lastAttr != null)
			{
				if (this._omitDuplicateNamespaces)
				{
					object firstNonDuplicateNamespaceAttribute = this.GetFirstNonDuplicateNamespaceAttribute(elementInAttributeScope.lastAttr.next);
					if (firstNonDuplicateNamespaceAttribute == null)
					{
						return false;
					}
					this._source = firstNonDuplicateNamespaceAttribute;
				}
				else
				{
					this._source = elementInAttributeScope.lastAttr.next;
				}
				return true;
			}
			return false;
		}

		// Token: 0x0600033E RID: 830 RVA: 0x0000EE54 File Offset: 0x0000D054
		public override bool MoveToNextAttribute()
		{
			if (!this.IsInteractive)
			{
				return false;
			}
			XElement xelement = this._source as XElement;
			if (xelement != null)
			{
				if (this.IsEndElement)
				{
					return false;
				}
				if (xelement.lastAttr != null)
				{
					if (this._omitDuplicateNamespaces)
					{
						object firstNonDuplicateNamespaceAttribute = this.GetFirstNonDuplicateNamespaceAttribute(xelement.lastAttr.next);
						if (firstNonDuplicateNamespaceAttribute == null)
						{
							return false;
						}
						this._source = firstNonDuplicateNamespaceAttribute;
					}
					else
					{
						this._source = xelement.lastAttr.next;
					}
					return true;
				}
				return false;
			}
			else
			{
				XAttribute xattribute = this._source as XAttribute;
				if (xattribute == null)
				{
					xattribute = (this._parent as XAttribute);
				}
				if (xattribute != null && xattribute.parent != null && ((XElement)xattribute.parent).lastAttr != xattribute)
				{
					if (this._omitDuplicateNamespaces)
					{
						object firstNonDuplicateNamespaceAttribute2 = this.GetFirstNonDuplicateNamespaceAttribute(xattribute.next);
						if (firstNonDuplicateNamespaceAttribute2 == null)
						{
							return false;
						}
						this._source = firstNonDuplicateNamespaceAttribute2;
					}
					else
					{
						this._source = xattribute.next;
					}
					this._parent = null;
					return true;
				}
				return false;
			}
		}

		// Token: 0x0600033F RID: 831 RVA: 0x0000EF3C File Offset: 0x0000D13C
		public override bool Read()
		{
			ReadState state = this._state;
			if (state != ReadState.Initial)
			{
				return state == ReadState.Interactive && this.Read(false);
			}
			this._state = ReadState.Interactive;
			XDocument xdocument = this._source as XDocument;
			return xdocument == null || this.ReadIntoDocument(xdocument);
		}

		// Token: 0x06000340 RID: 832 RVA: 0x0000EF84 File Offset: 0x0000D184
		public override bool ReadAttributeValue()
		{
			if (!this.IsInteractive)
			{
				return false;
			}
			XAttribute xattribute = this._source as XAttribute;
			return xattribute != null && this.ReadIntoAttribute(xattribute);
		}

		// Token: 0x06000341 RID: 833 RVA: 0x0000EFB4 File Offset: 0x0000D1B4
		public override bool ReadToDescendant(string localName, string namespaceName)
		{
			if (!this.IsInteractive)
			{
				return false;
			}
			this.MoveToElement();
			XElement xelement = this._source as XElement;
			if (xelement != null && !xelement.IsEmpty)
			{
				if (this.IsEndElement)
				{
					return false;
				}
				foreach (XElement xelement2 in xelement.Descendants())
				{
					if (xelement2.Name.LocalName == localName && xelement2.Name.NamespaceName == namespaceName)
					{
						this._source = xelement2;
						return true;
					}
				}
				this.IsEndElement = true;
				return false;
			}
			return false;
		}

		// Token: 0x06000342 RID: 834 RVA: 0x0000F068 File Offset: 0x0000D268
		public override bool ReadToFollowing(string localName, string namespaceName)
		{
			while (this.Read())
			{
				XElement xelement = this._source as XElement;
				if (xelement != null && !this.IsEndElement && xelement.Name.LocalName == localName && xelement.Name.NamespaceName == namespaceName)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000343 RID: 835 RVA: 0x0000F0C0 File Offset: 0x0000D2C0
		public override bool ReadToNextSibling(string localName, string namespaceName)
		{
			if (!this.IsInteractive)
			{
				return false;
			}
			this.MoveToElement();
			if (this._source != this._root)
			{
				XNode xnode = this._source as XNode;
				if (xnode != null)
				{
					foreach (XElement xelement in xnode.ElementsAfterSelf())
					{
						if (xelement.Name.LocalName == localName && xelement.Name.NamespaceName == namespaceName)
						{
							this._source = xelement;
							this.IsEndElement = false;
							return true;
						}
					}
					if (xnode.parent is XElement)
					{
						this._source = xnode.parent;
						this.IsEndElement = true;
						return false;
					}
					goto IL_E0;
				}
				if (this._parent is XElement)
				{
					this._source = this._parent;
					this._parent = null;
					this.IsEndElement = true;
					return false;
				}
			}
			IL_E0:
			return this.ReadToEnd();
		}

		// Token: 0x06000344 RID: 836 RVA: 0x000043E9 File Offset: 0x000025E9
		public override void ResolveEntity()
		{
		}

		// Token: 0x06000345 RID: 837 RVA: 0x0000F1C8 File Offset: 0x0000D3C8
		public override void Skip()
		{
			if (!this.IsInteractive)
			{
				return;
			}
			this.Read(true);
		}

		// Token: 0x06000346 RID: 838 RVA: 0x0000F1DC File Offset: 0x0000D3DC
		bool IXmlLineInfo.HasLineInfo()
		{
			if (this.IsEndElement)
			{
				XElement xelement = this._source as XElement;
				if (xelement != null)
				{
					return xelement.Annotation<LineInfoEndElementAnnotation>() != null;
				}
			}
			else
			{
				IXmlLineInfo xmlLineInfo = this._source as IXmlLineInfo;
				if (xmlLineInfo != null)
				{
					return xmlLineInfo.HasLineInfo();
				}
			}
			return false;
		}

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x06000347 RID: 839 RVA: 0x0000F224 File Offset: 0x0000D424
		int IXmlLineInfo.LineNumber
		{
			get
			{
				if (this.IsEndElement)
				{
					XElement xelement = this._source as XElement;
					if (xelement != null)
					{
						LineInfoEndElementAnnotation lineInfoEndElementAnnotation = xelement.Annotation<LineInfoEndElementAnnotation>();
						if (lineInfoEndElementAnnotation != null)
						{
							return lineInfoEndElementAnnotation.lineNumber;
						}
					}
				}
				else
				{
					IXmlLineInfo xmlLineInfo = this._source as IXmlLineInfo;
					if (xmlLineInfo != null)
					{
						return xmlLineInfo.LineNumber;
					}
				}
				return 0;
			}
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x06000348 RID: 840 RVA: 0x0000F270 File Offset: 0x0000D470
		int IXmlLineInfo.LinePosition
		{
			get
			{
				if (this.IsEndElement)
				{
					XElement xelement = this._source as XElement;
					if (xelement != null)
					{
						LineInfoEndElementAnnotation lineInfoEndElementAnnotation = xelement.Annotation<LineInfoEndElementAnnotation>();
						if (lineInfoEndElementAnnotation != null)
						{
							return lineInfoEndElementAnnotation.linePosition;
						}
					}
				}
				else
				{
					IXmlLineInfo xmlLineInfo = this._source as IXmlLineInfo;
					if (xmlLineInfo != null)
					{
						return xmlLineInfo.LinePosition;
					}
				}
				return 0;
			}
		}

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x06000349 RID: 841 RVA: 0x0000F2BC File Offset: 0x0000D4BC
		// (set) Token: 0x0600034A RID: 842 RVA: 0x0000F2CC File Offset: 0x0000D4CC
		private bool IsEndElement
		{
			get
			{
				return this._parent == this._source;
			}
			set
			{
				this._parent = (value ? this._source : null);
			}
		}

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x0600034B RID: 843 RVA: 0x0000F2E0 File Offset: 0x0000D4E0
		private bool IsInteractive
		{
			get
			{
				return this._state == ReadState.Interactive;
			}
		}

		// Token: 0x0600034C RID: 844 RVA: 0x0000F2EB File Offset: 0x0000D4EB
		private static XmlNameTable CreateNameTable()
		{
			NameTable nameTable = new NameTable();
			nameTable.Add(string.Empty);
			nameTable.Add("http://www.w3.org/2000/xmlns/");
			nameTable.Add("http://www.w3.org/XML/1998/namespace");
			return nameTable;
		}

		// Token: 0x0600034D RID: 845 RVA: 0x0000F318 File Offset: 0x0000D518
		private XElement GetElementInAttributeScope()
		{
			XElement xelement = this._source as XElement;
			if (xelement != null)
			{
				if (this.IsEndElement)
				{
					return null;
				}
				return xelement;
			}
			else
			{
				XAttribute xattribute = this._source as XAttribute;
				if (xattribute != null)
				{
					return (XElement)xattribute.parent;
				}
				xattribute = (this._parent as XAttribute);
				if (xattribute != null)
				{
					return (XElement)xattribute.parent;
				}
				return null;
			}
		}

		// Token: 0x0600034E RID: 846 RVA: 0x0000F378 File Offset: 0x0000D578
		private XElement GetElementInScope()
		{
			XElement xelement = this._source as XElement;
			if (xelement != null)
			{
				return xelement;
			}
			XNode xnode = this._source as XNode;
			if (xnode != null)
			{
				return xnode.parent as XElement;
			}
			XAttribute xattribute = this._source as XAttribute;
			if (xattribute != null)
			{
				return (XElement)xattribute.parent;
			}
			xelement = (this._parent as XElement);
			if (xelement != null)
			{
				return xelement;
			}
			xattribute = (this._parent as XAttribute);
			if (xattribute != null)
			{
				return (XElement)xattribute.parent;
			}
			return null;
		}

		// Token: 0x0600034F RID: 847 RVA: 0x0000F3FC File Offset: 0x0000D5FC
		private static void GetNameInAttributeScope(string qualifiedName, XElement e, out string localName, out string namespaceName)
		{
			if (!string.IsNullOrEmpty(qualifiedName))
			{
				int num = qualifiedName.IndexOf(':');
				if (num != 0 && num != qualifiedName.Length - 1)
				{
					if (num == -1)
					{
						localName = qualifiedName;
						namespaceName = string.Empty;
						return;
					}
					XNamespace namespaceOfPrefix = e.GetNamespaceOfPrefix(qualifiedName.Substring(0, num));
					if (namespaceOfPrefix != null)
					{
						localName = qualifiedName.Substring(num + 1, qualifiedName.Length - num - 1);
						namespaceName = namespaceOfPrefix.NamespaceName;
						return;
					}
				}
			}
			localName = null;
			namespaceName = null;
		}

		// Token: 0x06000350 RID: 848 RVA: 0x0000F474 File Offset: 0x0000D674
		private bool Read(bool skipContent)
		{
			XElement xelement = this._source as XElement;
			if (xelement != null)
			{
				if (xelement.IsEmpty || this.IsEndElement || skipContent)
				{
					return this.ReadOverNode(xelement);
				}
				return this.ReadIntoElement(xelement);
			}
			else
			{
				XNode xnode = this._source as XNode;
				if (xnode != null)
				{
					return this.ReadOverNode(xnode);
				}
				XAttribute xattribute = this._source as XAttribute;
				if (xattribute != null)
				{
					return this.ReadOverAttribute(xattribute, skipContent);
				}
				return this.ReadOverText(skipContent);
			}
		}

		// Token: 0x06000351 RID: 849 RVA: 0x0000F4EC File Offset: 0x0000D6EC
		private bool ReadIntoDocument(XDocument d)
		{
			XNode xnode = d.content as XNode;
			if (xnode != null)
			{
				this._source = xnode.next;
				return true;
			}
			string text = d.content as string;
			if (text != null && text.Length > 0)
			{
				this._source = text;
				this._parent = d;
				return true;
			}
			return this.ReadToEnd();
		}

		// Token: 0x06000352 RID: 850 RVA: 0x0000F544 File Offset: 0x0000D744
		private bool ReadIntoElement(XElement e)
		{
			XNode xnode = e.content as XNode;
			if (xnode != null)
			{
				this._source = xnode.next;
				return true;
			}
			string text = e.content as string;
			if (text != null)
			{
				if (text.Length > 0)
				{
					this._source = text;
					this._parent = e;
				}
				else
				{
					this._source = e;
					this.IsEndElement = true;
				}
				return true;
			}
			return this.ReadToEnd();
		}

		// Token: 0x06000353 RID: 851 RVA: 0x0000F5AC File Offset: 0x0000D7AC
		private bool ReadIntoAttribute(XAttribute a)
		{
			this._source = a.value;
			this._parent = a;
			return true;
		}

		// Token: 0x06000354 RID: 852 RVA: 0x0000F5C4 File Offset: 0x0000D7C4
		private bool ReadOverAttribute(XAttribute a, bool skipContent)
		{
			XElement xelement = (XElement)a.parent;
			if (xelement == null)
			{
				return this.ReadToEnd();
			}
			if (xelement.IsEmpty || skipContent)
			{
				return this.ReadOverNode(xelement);
			}
			return this.ReadIntoElement(xelement);
		}

		// Token: 0x06000355 RID: 853 RVA: 0x0000F600 File Offset: 0x0000D800
		private bool ReadOverNode(XNode n)
		{
			if (n == this._root)
			{
				return this.ReadToEnd();
			}
			XNode next = n.next;
			if (next == null || next == n || n == n.parent.content)
			{
				if (n.parent == null || (n.parent.parent == null && n.parent is XDocument))
				{
					return this.ReadToEnd();
				}
				this._source = n.parent;
				this.IsEndElement = true;
			}
			else
			{
				this._source = next;
				this.IsEndElement = false;
			}
			return true;
		}

		// Token: 0x06000356 RID: 854 RVA: 0x0000F688 File Offset: 0x0000D888
		private bool ReadOverText(bool skipContent)
		{
			if (this._parent is XElement)
			{
				this._source = this._parent;
				this._parent = null;
				this.IsEndElement = true;
				return true;
			}
			XAttribute xattribute = this._parent as XAttribute;
			if (xattribute != null)
			{
				this._parent = null;
				return this.ReadOverAttribute(xattribute, skipContent);
			}
			return this.ReadToEnd();
		}

		// Token: 0x06000357 RID: 855 RVA: 0x0000F6E3 File Offset: 0x0000D8E3
		private bool ReadToEnd()
		{
			this._state = ReadState.EndOfFile;
			return false;
		}

		// Token: 0x06000358 RID: 856 RVA: 0x0000F6ED File Offset: 0x0000D8ED
		private bool IsDuplicateNamespaceAttribute(XAttribute candidateAttribute)
		{
			return candidateAttribute.IsNamespaceDeclaration && this.IsDuplicateNamespaceAttributeInner(candidateAttribute);
		}

		// Token: 0x06000359 RID: 857 RVA: 0x0000F700 File Offset: 0x0000D900
		private bool IsDuplicateNamespaceAttributeInner(XAttribute candidateAttribute)
		{
			if (candidateAttribute.Name.LocalName == "xml")
			{
				return true;
			}
			XElement xelement = candidateAttribute.parent as XElement;
			if (xelement == this._root || xelement == null)
			{
				return false;
			}
			for (xelement = (xelement.parent as XElement); xelement != null; xelement = (xelement.parent as XElement))
			{
				XAttribute xattribute = xelement.lastAttr;
				if (xattribute != null)
				{
					while (!(xattribute.name == candidateAttribute.name))
					{
						xattribute = xattribute.next;
						if (xattribute == xelement.lastAttr)
						{
							goto IL_85;
						}
					}
					return xattribute.Value == candidateAttribute.Value;
				}
				IL_85:
				if (xelement == this._root)
				{
					return false;
				}
			}
			return false;
		}

		// Token: 0x0600035A RID: 858 RVA: 0x0000F7B0 File Offset: 0x0000D9B0
		private XAttribute GetFirstNonDuplicateNamespaceAttribute(XAttribute candidate)
		{
			if (!this.IsDuplicateNamespaceAttribute(candidate))
			{
				return candidate;
			}
			XElement xelement = candidate.parent as XElement;
			if (xelement != null && candidate != xelement.lastAttr)
			{
				for (;;)
				{
					candidate = candidate.next;
					if (!this.IsDuplicateNamespaceAttribute(candidate))
					{
						break;
					}
					if (candidate == xelement.lastAttr)
					{
						goto IL_3F;
					}
				}
				return candidate;
			}
			IL_3F:
			return null;
		}

		// Token: 0x0600035B RID: 859 RVA: 0x0000F7FD File Offset: 0x0000D9FD
		// Note: this type is marked as 'beforefieldinit'.
		static XNodeReader()
		{
		}

		// Token: 0x040001C0 RID: 448
		private static readonly char[] s_WhitespaceChars = new char[]
		{
			' ',
			'\t',
			'\n',
			'\r'
		};

		// Token: 0x040001C1 RID: 449
		private object _source;

		// Token: 0x040001C2 RID: 450
		private object _parent;

		// Token: 0x040001C3 RID: 451
		private ReadState _state;

		// Token: 0x040001C4 RID: 452
		private XNode _root;

		// Token: 0x040001C5 RID: 453
		private XmlNameTable _nameTable;

		// Token: 0x040001C6 RID: 454
		private bool _omitDuplicateNamespaces;
	}
}

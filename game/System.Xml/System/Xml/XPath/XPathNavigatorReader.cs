using System;
using System.Collections.Generic;
using System.Xml.Schema;

namespace System.Xml.XPath
{
	// Token: 0x0200025D RID: 605
	internal class XPathNavigatorReader : XmlReader, IXmlNamespaceResolver
	{
		// Token: 0x06001697 RID: 5783 RVA: 0x00087502 File Offset: 0x00085702
		internal static XmlNodeType ToXmlNodeType(XPathNodeType typ)
		{
			return XPathNavigatorReader.convertFromXPathNodeType[(int)typ];
		}

		// Token: 0x1700040A RID: 1034
		// (get) Token: 0x06001698 RID: 5784 RVA: 0x0008750B File Offset: 0x0008570B
		internal object UnderlyingObject
		{
			get
			{
				return this.nav.UnderlyingObject;
			}
		}

		// Token: 0x06001699 RID: 5785 RVA: 0x00087518 File Offset: 0x00085718
		public static XPathNavigatorReader Create(XPathNavigator navToRead)
		{
			XPathNavigator xpathNavigator = navToRead.Clone();
			IXmlLineInfo xli = xpathNavigator as IXmlLineInfo;
			IXmlSchemaInfo xmlSchemaInfo = xpathNavigator as IXmlSchemaInfo;
			if (xmlSchemaInfo == null)
			{
				return new XPathNavigatorReader(xpathNavigator, xli, xmlSchemaInfo);
			}
			return new XPathNavigatorReaderWithSI(xpathNavigator, xli, xmlSchemaInfo);
		}

		// Token: 0x0600169A RID: 5786 RVA: 0x00087550 File Offset: 0x00085750
		protected XPathNavigatorReader(XPathNavigator navToRead, IXmlLineInfo xli, IXmlSchemaInfo xsi)
		{
			this.navToRead = navToRead;
			this.lineInfo = xli;
			this.schemaInfo = xsi;
			this.nav = XmlEmptyNavigator.Singleton;
			this.state = XPathNavigatorReader.State.Initial;
			this.depth = 0;
			this.nodeType = XPathNavigatorReader.ToXmlNodeType(this.nav.NodeType);
		}

		// Token: 0x1700040B RID: 1035
		// (get) Token: 0x0600169B RID: 5787 RVA: 0x000875A7 File Offset: 0x000857A7
		protected bool IsReading
		{
			get
			{
				return this.state > XPathNavigatorReader.State.Initial && this.state < XPathNavigatorReader.State.EOF;
			}
		}

		// Token: 0x1700040C RID: 1036
		// (get) Token: 0x0600169C RID: 5788 RVA: 0x000875BD File Offset: 0x000857BD
		internal override XmlNamespaceManager NamespaceManager
		{
			get
			{
				return XPathNavigator.GetNamespaces(this);
			}
		}

		// Token: 0x1700040D RID: 1037
		// (get) Token: 0x0600169D RID: 5789 RVA: 0x000875C5 File Offset: 0x000857C5
		public override XmlNameTable NameTable
		{
			get
			{
				return this.navToRead.NameTable;
			}
		}

		// Token: 0x0600169E RID: 5790 RVA: 0x000875D2 File Offset: 0x000857D2
		IDictionary<string, string> IXmlNamespaceResolver.GetNamespacesInScope(XmlNamespaceScope scope)
		{
			return this.nav.GetNamespacesInScope(scope);
		}

		// Token: 0x0600169F RID: 5791 RVA: 0x000875E0 File Offset: 0x000857E0
		string IXmlNamespaceResolver.LookupNamespace(string prefix)
		{
			return this.nav.LookupNamespace(prefix);
		}

		// Token: 0x060016A0 RID: 5792 RVA: 0x000875EE File Offset: 0x000857EE
		string IXmlNamespaceResolver.LookupPrefix(string namespaceName)
		{
			return this.nav.LookupPrefix(namespaceName);
		}

		// Token: 0x1700040E RID: 1038
		// (get) Token: 0x060016A1 RID: 5793 RVA: 0x000875FC File Offset: 0x000857FC
		public override XmlReaderSettings Settings
		{
			get
			{
				return new XmlReaderSettings
				{
					NameTable = this.NameTable,
					ConformanceLevel = ConformanceLevel.Fragment,
					CheckCharacters = false,
					ReadOnly = true
				};
			}
		}

		// Token: 0x1700040F RID: 1039
		// (get) Token: 0x060016A2 RID: 5794 RVA: 0x00087624 File Offset: 0x00085824
		public override IXmlSchemaInfo SchemaInfo
		{
			get
			{
				if (this.nodeType == XmlNodeType.Text)
				{
					return null;
				}
				return this.nav.SchemaInfo;
			}
		}

		// Token: 0x17000410 RID: 1040
		// (get) Token: 0x060016A3 RID: 5795 RVA: 0x0008763C File Offset: 0x0008583C
		public override Type ValueType
		{
			get
			{
				return this.nav.ValueType;
			}
		}

		// Token: 0x17000411 RID: 1041
		// (get) Token: 0x060016A4 RID: 5796 RVA: 0x00087649 File Offset: 0x00085849
		public override XmlNodeType NodeType
		{
			get
			{
				return this.nodeType;
			}
		}

		// Token: 0x17000412 RID: 1042
		// (get) Token: 0x060016A5 RID: 5797 RVA: 0x00087651 File Offset: 0x00085851
		public override string NamespaceURI
		{
			get
			{
				if (this.nav.NodeType == XPathNodeType.Namespace)
				{
					return this.NameTable.Add("http://www.w3.org/2000/xmlns/");
				}
				if (this.NodeType == XmlNodeType.Text)
				{
					return string.Empty;
				}
				return this.nav.NamespaceURI;
			}
		}

		// Token: 0x17000413 RID: 1043
		// (get) Token: 0x060016A6 RID: 5798 RVA: 0x0008768C File Offset: 0x0008588C
		public override string LocalName
		{
			get
			{
				if (this.nav.NodeType == XPathNodeType.Namespace && this.nav.LocalName.Length == 0)
				{
					return this.NameTable.Add("xmlns");
				}
				if (this.NodeType == XmlNodeType.Text)
				{
					return string.Empty;
				}
				return this.nav.LocalName;
			}
		}

		// Token: 0x17000414 RID: 1044
		// (get) Token: 0x060016A7 RID: 5799 RVA: 0x000876E4 File Offset: 0x000858E4
		public override string Prefix
		{
			get
			{
				if (this.nav.NodeType == XPathNodeType.Namespace && this.nav.LocalName.Length != 0)
				{
					return this.NameTable.Add("xmlns");
				}
				if (this.NodeType == XmlNodeType.Text)
				{
					return string.Empty;
				}
				return this.nav.Prefix;
			}
		}

		// Token: 0x17000415 RID: 1045
		// (get) Token: 0x060016A8 RID: 5800 RVA: 0x0008773C File Offset: 0x0008593C
		public override string BaseURI
		{
			get
			{
				if (this.state == XPathNavigatorReader.State.Initial)
				{
					return this.navToRead.BaseURI;
				}
				return this.nav.BaseURI;
			}
		}

		// Token: 0x17000416 RID: 1046
		// (get) Token: 0x060016A9 RID: 5801 RVA: 0x0008775D File Offset: 0x0008595D
		public override bool IsEmptyElement
		{
			get
			{
				return this.nav.IsEmptyElement;
			}
		}

		// Token: 0x17000417 RID: 1047
		// (get) Token: 0x060016AA RID: 5802 RVA: 0x0008776C File Offset: 0x0008596C
		public override XmlSpace XmlSpace
		{
			get
			{
				XPathNavigator xpathNavigator = this.nav.Clone();
				for (;;)
				{
					if (xpathNavigator.MoveToAttribute("space", "http://www.w3.org/XML/1998/namespace"))
					{
						string a = XmlConvert.TrimString(xpathNavigator.Value);
						if (a == "default")
						{
							break;
						}
						if (a == "preserve")
						{
							return XmlSpace.Preserve;
						}
						xpathNavigator.MoveToParent();
					}
					if (!xpathNavigator.MoveToParent())
					{
						return XmlSpace.None;
					}
				}
				return XmlSpace.Default;
			}
		}

		// Token: 0x17000418 RID: 1048
		// (get) Token: 0x060016AB RID: 5803 RVA: 0x000877D3 File Offset: 0x000859D3
		public override string XmlLang
		{
			get
			{
				return this.nav.XmlLang;
			}
		}

		// Token: 0x17000419 RID: 1049
		// (get) Token: 0x060016AC RID: 5804 RVA: 0x000877E0 File Offset: 0x000859E0
		public override bool HasValue
		{
			get
			{
				return this.nodeType != XmlNodeType.Element && this.nodeType != XmlNodeType.Document && this.nodeType != XmlNodeType.EndElement && this.nodeType != XmlNodeType.None;
			}
		}

		// Token: 0x1700041A RID: 1050
		// (get) Token: 0x060016AD RID: 5805 RVA: 0x0008780A File Offset: 0x00085A0A
		public override string Value
		{
			get
			{
				if (this.nodeType != XmlNodeType.Element && this.nodeType != XmlNodeType.Document && this.nodeType != XmlNodeType.EndElement && this.nodeType != XmlNodeType.None)
				{
					return this.nav.Value;
				}
				return string.Empty;
			}
		}

		// Token: 0x060016AE RID: 5806 RVA: 0x00087844 File Offset: 0x00085A44
		private XPathNavigator GetElemNav()
		{
			switch (this.state)
			{
			case XPathNavigatorReader.State.Content:
				return this.nav.Clone();
			case XPathNavigatorReader.State.Attribute:
			case XPathNavigatorReader.State.AttrVal:
			{
				XPathNavigator xpathNavigator = this.nav.Clone();
				if (xpathNavigator.MoveToParent())
				{
					return xpathNavigator;
				}
				break;
			}
			case XPathNavigatorReader.State.InReadBinary:
			{
				this.state = this.savedState;
				XPathNavigator elemNav = this.GetElemNav();
				this.state = XPathNavigatorReader.State.InReadBinary;
				return elemNav;
			}
			}
			return null;
		}

		// Token: 0x060016AF RID: 5807 RVA: 0x000878B4 File Offset: 0x00085AB4
		private XPathNavigator GetElemNav(out int depth)
		{
			XPathNavigator xpathNavigator = null;
			switch (this.state)
			{
			case XPathNavigatorReader.State.Content:
				if (this.nodeType == XmlNodeType.Element)
				{
					xpathNavigator = this.nav.Clone();
				}
				depth = this.depth;
				return xpathNavigator;
			case XPathNavigatorReader.State.Attribute:
				xpathNavigator = this.nav.Clone();
				xpathNavigator.MoveToParent();
				depth = this.depth - 1;
				return xpathNavigator;
			case XPathNavigatorReader.State.AttrVal:
				xpathNavigator = this.nav.Clone();
				xpathNavigator.MoveToParent();
				depth = this.depth - 2;
				return xpathNavigator;
			case XPathNavigatorReader.State.InReadBinary:
				this.state = this.savedState;
				xpathNavigator = this.GetElemNav(out depth);
				this.state = XPathNavigatorReader.State.InReadBinary;
				return xpathNavigator;
			}
			depth = this.depth;
			return xpathNavigator;
		}

		// Token: 0x060016B0 RID: 5808 RVA: 0x0008796B File Offset: 0x00085B6B
		private void MoveToAttr(XPathNavigator nav, int depth)
		{
			this.nav.MoveTo(nav);
			this.depth = depth;
			this.nodeType = XmlNodeType.Attribute;
			this.state = XPathNavigatorReader.State.Attribute;
		}

		// Token: 0x1700041B RID: 1051
		// (get) Token: 0x060016B1 RID: 5809 RVA: 0x00087990 File Offset: 0x00085B90
		public override int AttributeCount
		{
			get
			{
				if (this.attrCount < 0)
				{
					XPathNavigator elemNav = this.GetElemNav();
					int num = 0;
					if (elemNav != null)
					{
						if (elemNav.MoveToFirstNamespace(XPathNamespaceScope.Local))
						{
							do
							{
								num++;
							}
							while (elemNav.MoveToNextNamespace(XPathNamespaceScope.Local));
							elemNav.MoveToParent();
						}
						if (elemNav.MoveToFirstAttribute())
						{
							do
							{
								num++;
							}
							while (elemNav.MoveToNextAttribute());
						}
					}
					this.attrCount = num;
				}
				return this.attrCount;
			}
		}

		// Token: 0x060016B2 RID: 5810 RVA: 0x000879F0 File Offset: 0x00085BF0
		public override string GetAttribute(string name)
		{
			XPathNavigator xpathNavigator = this.nav;
			XPathNodeType xpathNodeType = xpathNavigator.NodeType;
			if (xpathNodeType != XPathNodeType.Element)
			{
				if (xpathNodeType != XPathNodeType.Attribute)
				{
					return null;
				}
				xpathNavigator = xpathNavigator.Clone();
				if (!xpathNavigator.MoveToParent())
				{
					return null;
				}
			}
			string text;
			string text2;
			ValidateNames.SplitQName(name, out text, out text2);
			if (text.Length == 0)
			{
				if (text2 == "xmlns")
				{
					return xpathNavigator.GetNamespace(string.Empty);
				}
				if (xpathNavigator == this.nav)
				{
					xpathNavigator = xpathNavigator.Clone();
				}
				if (xpathNavigator.MoveToAttribute(text2, string.Empty))
				{
					return xpathNavigator.Value;
				}
			}
			else
			{
				if (text == "xmlns")
				{
					return xpathNavigator.GetNamespace(text2);
				}
				if (xpathNavigator == this.nav)
				{
					xpathNavigator = xpathNavigator.Clone();
				}
				if (xpathNavigator.MoveToFirstAttribute())
				{
					while (!(xpathNavigator.LocalName == text2) || !(xpathNavigator.Prefix == text))
					{
						if (!xpathNavigator.MoveToNextAttribute())
						{
							goto IL_D1;
						}
					}
					return xpathNavigator.Value;
				}
			}
			IL_D1:
			return null;
		}

		// Token: 0x060016B3 RID: 5811 RVA: 0x00087AD0 File Offset: 0x00085CD0
		public override string GetAttribute(string localName, string namespaceURI)
		{
			if (localName == null)
			{
				throw new ArgumentNullException("localName");
			}
			XPathNavigator xpathNavigator = this.nav;
			XPathNodeType xpathNodeType = xpathNavigator.NodeType;
			if (xpathNodeType != XPathNodeType.Element)
			{
				if (xpathNodeType != XPathNodeType.Attribute)
				{
					return null;
				}
				xpathNavigator = xpathNavigator.Clone();
				if (!xpathNavigator.MoveToParent())
				{
					return null;
				}
			}
			if (namespaceURI == "http://www.w3.org/2000/xmlns/")
			{
				if (localName == "xmlns")
				{
					localName = string.Empty;
				}
				return xpathNavigator.GetNamespace(localName);
			}
			if (namespaceURI == null)
			{
				namespaceURI = string.Empty;
			}
			if (xpathNavigator == this.nav)
			{
				xpathNavigator = xpathNavigator.Clone();
			}
			if (xpathNavigator.MoveToAttribute(localName, namespaceURI))
			{
				return xpathNavigator.Value;
			}
			return null;
		}

		// Token: 0x060016B4 RID: 5812 RVA: 0x00087B6C File Offset: 0x00085D6C
		private static string GetNamespaceByIndex(XPathNavigator nav, int index, out int count)
		{
			string value = nav.Value;
			string result = null;
			if (nav.MoveToNextNamespace(XPathNamespaceScope.Local))
			{
				result = XPathNavigatorReader.GetNamespaceByIndex(nav, index, out count);
			}
			else
			{
				count = 0;
			}
			if (count == index)
			{
				result = value;
			}
			count++;
			return result;
		}

		// Token: 0x060016B5 RID: 5813 RVA: 0x00087BA8 File Offset: 0x00085DA8
		public override string GetAttribute(int index)
		{
			if (index >= 0)
			{
				XPathNavigator elemNav = this.GetElemNav();
				if (elemNav != null)
				{
					if (elemNav.MoveToFirstNamespace(XPathNamespaceScope.Local))
					{
						int num;
						string namespaceByIndex = XPathNavigatorReader.GetNamespaceByIndex(elemNav, index, out num);
						if (namespaceByIndex != null)
						{
							return namespaceByIndex;
						}
						index -= num;
						elemNav.MoveToParent();
					}
					if (elemNav.MoveToFirstAttribute())
					{
						while (index != 0)
						{
							index--;
							if (!elemNav.MoveToNextAttribute())
							{
								goto IL_51;
							}
						}
						return elemNav.Value;
					}
				}
			}
			IL_51:
			throw new ArgumentOutOfRangeException("index");
		}

		// Token: 0x060016B6 RID: 5814 RVA: 0x00087C10 File Offset: 0x00085E10
		public override bool MoveToAttribute(string localName, string namespaceName)
		{
			if (localName == null)
			{
				throw new ArgumentNullException("localName");
			}
			int num = this.depth;
			XPathNavigator elemNav = this.GetElemNav(out num);
			if (elemNav != null)
			{
				if (namespaceName == "http://www.w3.org/2000/xmlns/")
				{
					if (localName == "xmlns")
					{
						localName = string.Empty;
					}
					if (!elemNav.MoveToFirstNamespace(XPathNamespaceScope.Local))
					{
						return false;
					}
					while (!(elemNav.LocalName == localName))
					{
						if (!elemNav.MoveToNextNamespace(XPathNamespaceScope.Local))
						{
							return false;
						}
					}
				}
				else
				{
					if (namespaceName == null)
					{
						namespaceName = string.Empty;
					}
					if (!elemNav.MoveToAttribute(localName, namespaceName))
					{
						return false;
					}
				}
				if (this.state == XPathNavigatorReader.State.InReadBinary)
				{
					this.readBinaryHelper.Finish();
					this.state = this.savedState;
				}
				this.MoveToAttr(elemNav, num + 1);
				return true;
			}
			return false;
		}

		// Token: 0x060016B7 RID: 5815 RVA: 0x00087CC4 File Offset: 0x00085EC4
		public override bool MoveToFirstAttribute()
		{
			int num;
			XPathNavigator elemNav = this.GetElemNav(out num);
			if (elemNav != null)
			{
				if (elemNav.MoveToFirstNamespace(XPathNamespaceScope.Local))
				{
					while (elemNav.MoveToNextNamespace(XPathNamespaceScope.Local))
					{
					}
				}
				else if (!elemNav.MoveToFirstAttribute())
				{
					return false;
				}
				if (this.state == XPathNavigatorReader.State.InReadBinary)
				{
					this.readBinaryHelper.Finish();
					this.state = this.savedState;
				}
				this.MoveToAttr(elemNav, num + 1);
				return true;
			}
			return false;
		}

		// Token: 0x060016B8 RID: 5816 RVA: 0x00087D28 File Offset: 0x00085F28
		public override bool MoveToNextAttribute()
		{
			switch (this.state)
			{
			case XPathNavigatorReader.State.Content:
				return this.MoveToFirstAttribute();
			case XPathNavigatorReader.State.Attribute:
			{
				if (XPathNodeType.Attribute == this.nav.NodeType)
				{
					return this.nav.MoveToNextAttribute();
				}
				XPathNavigator xpathNavigator = this.nav.Clone();
				if (!xpathNavigator.MoveToParent())
				{
					return false;
				}
				if (!xpathNavigator.MoveToFirstNamespace(XPathNamespaceScope.Local))
				{
					return false;
				}
				if (!xpathNavigator.IsSamePosition(this.nav))
				{
					XPathNavigator xpathNavigator2 = xpathNavigator.Clone();
					while (xpathNavigator.MoveToNextNamespace(XPathNamespaceScope.Local))
					{
						if (xpathNavigator.IsSamePosition(this.nav))
						{
							this.nav.MoveTo(xpathNavigator2);
							return true;
						}
						xpathNavigator2.MoveTo(xpathNavigator);
					}
					return false;
				}
				xpathNavigator.MoveToParent();
				if (!xpathNavigator.MoveToFirstAttribute())
				{
					return false;
				}
				this.nav.MoveTo(xpathNavigator);
				return true;
			}
			case XPathNavigatorReader.State.AttrVal:
				this.depth--;
				this.state = XPathNavigatorReader.State.Attribute;
				if (!this.MoveToNextAttribute())
				{
					this.depth++;
					this.state = XPathNavigatorReader.State.AttrVal;
					return false;
				}
				this.nodeType = XmlNodeType.Attribute;
				return true;
			case XPathNavigatorReader.State.InReadBinary:
				this.state = this.savedState;
				if (!this.MoveToNextAttribute())
				{
					this.state = XPathNavigatorReader.State.InReadBinary;
					return false;
				}
				this.readBinaryHelper.Finish();
				return true;
			}
			return false;
		}

		// Token: 0x060016B9 RID: 5817 RVA: 0x00087E70 File Offset: 0x00086070
		public override bool MoveToAttribute(string name)
		{
			int num;
			XPathNavigator elemNav = this.GetElemNav(out num);
			if (elemNav == null)
			{
				return false;
			}
			string text;
			string empty;
			ValidateNames.SplitQName(name, out text, out empty);
			bool flag;
			if ((flag = (text.Length == 0 && empty == "xmlns")) || text == "xmlns")
			{
				if (flag)
				{
					empty = string.Empty;
				}
				if (elemNav.MoveToFirstNamespace(XPathNamespaceScope.Local))
				{
					while (!(elemNav.LocalName == empty))
					{
						if (!elemNav.MoveToNextNamespace(XPathNamespaceScope.Local))
						{
							return false;
						}
					}
					goto IL_B5;
				}
			}
			else if (text.Length == 0)
			{
				if (elemNav.MoveToAttribute(empty, string.Empty))
				{
					goto IL_B5;
				}
			}
			else if (elemNav.MoveToFirstAttribute())
			{
				while (!(elemNav.LocalName == empty) || !(elemNav.Prefix == text))
				{
					if (!elemNav.MoveToNextAttribute())
					{
						return false;
					}
				}
				goto IL_B5;
			}
			return false;
			IL_B5:
			if (this.state == XPathNavigatorReader.State.InReadBinary)
			{
				this.readBinaryHelper.Finish();
				this.state = this.savedState;
			}
			this.MoveToAttr(elemNav, num + 1);
			return true;
		}

		// Token: 0x060016BA RID: 5818 RVA: 0x00087F60 File Offset: 0x00086160
		public override bool MoveToElement()
		{
			XPathNavigatorReader.State state = this.state;
			if (state - XPathNavigatorReader.State.Attribute > 1)
			{
				if (state == XPathNavigatorReader.State.InReadBinary)
				{
					this.state = this.savedState;
					if (!this.MoveToElement())
					{
						this.state = XPathNavigatorReader.State.InReadBinary;
						return false;
					}
					this.readBinaryHelper.Finish();
				}
				return false;
			}
			if (!this.nav.MoveToParent())
			{
				return false;
			}
			this.depth--;
			if (this.state == XPathNavigatorReader.State.AttrVal)
			{
				this.depth--;
			}
			this.state = XPathNavigatorReader.State.Content;
			this.nodeType = XmlNodeType.Element;
			return true;
		}

		// Token: 0x1700041C RID: 1052
		// (get) Token: 0x060016BB RID: 5819 RVA: 0x00087FED File Offset: 0x000861ED
		public override bool EOF
		{
			get
			{
				return this.state == XPathNavigatorReader.State.EOF;
			}
		}

		// Token: 0x1700041D RID: 1053
		// (get) Token: 0x060016BC RID: 5820 RVA: 0x00087FF8 File Offset: 0x000861F8
		public override ReadState ReadState
		{
			get
			{
				switch (this.state)
				{
				case XPathNavigatorReader.State.Initial:
					return ReadState.Initial;
				case XPathNavigatorReader.State.Content:
				case XPathNavigatorReader.State.EndElement:
				case XPathNavigatorReader.State.Attribute:
				case XPathNavigatorReader.State.AttrVal:
				case XPathNavigatorReader.State.InReadBinary:
					return ReadState.Interactive;
				case XPathNavigatorReader.State.EOF:
					return ReadState.EndOfFile;
				case XPathNavigatorReader.State.Closed:
					return ReadState.Closed;
				default:
					return ReadState.Error;
				}
			}
		}

		// Token: 0x060016BD RID: 5821 RVA: 0x0000BB08 File Offset: 0x00009D08
		public override void ResolveEntity()
		{
			throw new InvalidOperationException(Res.GetString("Operation is not valid due to the current state of the object."));
		}

		// Token: 0x060016BE RID: 5822 RVA: 0x00088040 File Offset: 0x00086240
		public override bool ReadAttributeValue()
		{
			if (this.state == XPathNavigatorReader.State.InReadBinary)
			{
				this.readBinaryHelper.Finish();
				this.state = this.savedState;
			}
			if (this.state == XPathNavigatorReader.State.Attribute)
			{
				this.state = XPathNavigatorReader.State.AttrVal;
				this.nodeType = XmlNodeType.Text;
				this.depth++;
				return true;
			}
			return false;
		}

		// Token: 0x1700041E RID: 1054
		// (get) Token: 0x060016BF RID: 5823 RVA: 0x0001222F File Offset: 0x0001042F
		public override bool CanReadBinaryContent
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060016C0 RID: 5824 RVA: 0x00088098 File Offset: 0x00086298
		public override int ReadContentAsBase64(byte[] buffer, int index, int count)
		{
			if (this.ReadState != ReadState.Interactive)
			{
				return 0;
			}
			if (this.state != XPathNavigatorReader.State.InReadBinary)
			{
				this.readBinaryHelper = ReadContentAsBinaryHelper.CreateOrReset(this.readBinaryHelper, this);
				this.savedState = this.state;
			}
			this.state = this.savedState;
			int result = this.readBinaryHelper.ReadContentAsBase64(buffer, index, count);
			this.savedState = this.state;
			this.state = XPathNavigatorReader.State.InReadBinary;
			return result;
		}

		// Token: 0x060016C1 RID: 5825 RVA: 0x00088104 File Offset: 0x00086304
		public override int ReadContentAsBinHex(byte[] buffer, int index, int count)
		{
			if (this.ReadState != ReadState.Interactive)
			{
				return 0;
			}
			if (this.state != XPathNavigatorReader.State.InReadBinary)
			{
				this.readBinaryHelper = ReadContentAsBinaryHelper.CreateOrReset(this.readBinaryHelper, this);
				this.savedState = this.state;
			}
			this.state = this.savedState;
			int result = this.readBinaryHelper.ReadContentAsBinHex(buffer, index, count);
			this.savedState = this.state;
			this.state = XPathNavigatorReader.State.InReadBinary;
			return result;
		}

		// Token: 0x060016C2 RID: 5826 RVA: 0x00088170 File Offset: 0x00086370
		public override int ReadElementContentAsBase64(byte[] buffer, int index, int count)
		{
			if (this.ReadState != ReadState.Interactive)
			{
				return 0;
			}
			if (this.state != XPathNavigatorReader.State.InReadBinary)
			{
				this.readBinaryHelper = ReadContentAsBinaryHelper.CreateOrReset(this.readBinaryHelper, this);
				this.savedState = this.state;
			}
			this.state = this.savedState;
			int result = this.readBinaryHelper.ReadElementContentAsBase64(buffer, index, count);
			this.savedState = this.state;
			this.state = XPathNavigatorReader.State.InReadBinary;
			return result;
		}

		// Token: 0x060016C3 RID: 5827 RVA: 0x000881DC File Offset: 0x000863DC
		public override int ReadElementContentAsBinHex(byte[] buffer, int index, int count)
		{
			if (this.ReadState != ReadState.Interactive)
			{
				return 0;
			}
			if (this.state != XPathNavigatorReader.State.InReadBinary)
			{
				this.readBinaryHelper = ReadContentAsBinaryHelper.CreateOrReset(this.readBinaryHelper, this);
				this.savedState = this.state;
			}
			this.state = this.savedState;
			int result = this.readBinaryHelper.ReadElementContentAsBinHex(buffer, index, count);
			this.savedState = this.state;
			this.state = XPathNavigatorReader.State.InReadBinary;
			return result;
		}

		// Token: 0x060016C4 RID: 5828 RVA: 0x000875E0 File Offset: 0x000857E0
		public override string LookupNamespace(string prefix)
		{
			return this.nav.LookupNamespace(prefix);
		}

		// Token: 0x1700041F RID: 1055
		// (get) Token: 0x060016C5 RID: 5829 RVA: 0x00088248 File Offset: 0x00086448
		public override int Depth
		{
			get
			{
				return this.depth;
			}
		}

		// Token: 0x060016C6 RID: 5830 RVA: 0x00088250 File Offset: 0x00086450
		public override bool Read()
		{
			this.attrCount = -1;
			switch (this.state)
			{
			case XPathNavigatorReader.State.Initial:
				this.nav = this.navToRead;
				this.state = XPathNavigatorReader.State.Content;
				if (this.nav.NodeType == XPathNodeType.Root)
				{
					if (!this.nav.MoveToFirstChild())
					{
						this.SetEOF();
						return false;
					}
					this.readEntireDocument = true;
				}
				else if (XPathNodeType.Attribute == this.nav.NodeType)
				{
					this.state = XPathNavigatorReader.State.Attribute;
				}
				this.nodeType = XPathNavigatorReader.ToXmlNodeType(this.nav.NodeType);
				return true;
			case XPathNavigatorReader.State.Content:
				break;
			case XPathNavigatorReader.State.EndElement:
				goto IL_114;
			case XPathNavigatorReader.State.Attribute:
			case XPathNavigatorReader.State.AttrVal:
				if (!this.nav.MoveToParent())
				{
					this.SetEOF();
					return false;
				}
				this.nodeType = XPathNavigatorReader.ToXmlNodeType(this.nav.NodeType);
				this.depth--;
				if (this.state == XPathNavigatorReader.State.AttrVal)
				{
					this.depth--;
				}
				break;
			case XPathNavigatorReader.State.InReadBinary:
				this.state = this.savedState;
				this.readBinaryHelper.Finish();
				return this.Read();
			case XPathNavigatorReader.State.EOF:
			case XPathNavigatorReader.State.Closed:
			case XPathNavigatorReader.State.Error:
				return false;
			default:
				return true;
			}
			if (this.nav.MoveToFirstChild())
			{
				this.nodeType = XPathNavigatorReader.ToXmlNodeType(this.nav.NodeType);
				this.depth++;
				this.state = XPathNavigatorReader.State.Content;
				return true;
			}
			if (this.nodeType == XmlNodeType.Element && !this.nav.IsEmptyElement)
			{
				this.nodeType = XmlNodeType.EndElement;
				this.state = XPathNavigatorReader.State.EndElement;
				return true;
			}
			IL_114:
			if (this.depth == 0 && !this.readEntireDocument)
			{
				this.SetEOF();
				return false;
			}
			if (this.nav.MoveToNext())
			{
				this.nodeType = XPathNavigatorReader.ToXmlNodeType(this.nav.NodeType);
				this.state = XPathNavigatorReader.State.Content;
			}
			else
			{
				if (this.depth <= 0 || !this.nav.MoveToParent())
				{
					this.SetEOF();
					return false;
				}
				this.nodeType = XmlNodeType.EndElement;
				this.state = XPathNavigatorReader.State.EndElement;
				this.depth--;
			}
			return true;
		}

		// Token: 0x060016C7 RID: 5831 RVA: 0x0008846C File Offset: 0x0008666C
		public override void Close()
		{
			this.nav = XmlEmptyNavigator.Singleton;
			this.nodeType = XmlNodeType.None;
			this.state = XPathNavigatorReader.State.Closed;
			this.depth = 0;
		}

		// Token: 0x060016C8 RID: 5832 RVA: 0x0008848E File Offset: 0x0008668E
		private void SetEOF()
		{
			this.nav = XmlEmptyNavigator.Singleton;
			this.nodeType = XmlNodeType.None;
			this.state = XPathNavigatorReader.State.EOF;
			this.depth = 0;
		}

		// Token: 0x060016C9 RID: 5833 RVA: 0x000884B0 File Offset: 0x000866B0
		// Note: this type is marked as 'beforefieldinit'.
		static XPathNavigatorReader()
		{
		}

		// Token: 0x0400180C RID: 6156
		private XPathNavigator nav;

		// Token: 0x0400180D RID: 6157
		private XPathNavigator navToRead;

		// Token: 0x0400180E RID: 6158
		private int depth;

		// Token: 0x0400180F RID: 6159
		private XPathNavigatorReader.State state;

		// Token: 0x04001810 RID: 6160
		private XmlNodeType nodeType;

		// Token: 0x04001811 RID: 6161
		private int attrCount;

		// Token: 0x04001812 RID: 6162
		private bool readEntireDocument;

		// Token: 0x04001813 RID: 6163
		protected IXmlLineInfo lineInfo;

		// Token: 0x04001814 RID: 6164
		protected IXmlSchemaInfo schemaInfo;

		// Token: 0x04001815 RID: 6165
		private ReadContentAsBinaryHelper readBinaryHelper;

		// Token: 0x04001816 RID: 6166
		private XPathNavigatorReader.State savedState;

		// Token: 0x04001817 RID: 6167
		internal const string space = "space";

		// Token: 0x04001818 RID: 6168
		internal static XmlNodeType[] convertFromXPathNodeType = new XmlNodeType[]
		{
			XmlNodeType.Document,
			XmlNodeType.Element,
			XmlNodeType.Attribute,
			XmlNodeType.Attribute,
			XmlNodeType.Text,
			XmlNodeType.SignificantWhitespace,
			XmlNodeType.Whitespace,
			XmlNodeType.ProcessingInstruction,
			XmlNodeType.Comment,
			XmlNodeType.None
		};

		// Token: 0x0200025E RID: 606
		private enum State
		{
			// Token: 0x0400181A RID: 6170
			Initial,
			// Token: 0x0400181B RID: 6171
			Content,
			// Token: 0x0400181C RID: 6172
			EndElement,
			// Token: 0x0400181D RID: 6173
			Attribute,
			// Token: 0x0400181E RID: 6174
			AttrVal,
			// Token: 0x0400181F RID: 6175
			InReadBinary,
			// Token: 0x04001820 RID: 6176
			EOF,
			// Token: 0x04001821 RID: 6177
			Closed,
			// Token: 0x04001822 RID: 6178
			Error
		}
	}
}

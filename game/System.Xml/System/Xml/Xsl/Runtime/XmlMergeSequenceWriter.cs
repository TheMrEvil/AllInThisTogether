using System;
using System.Xml.XPath;

namespace System.Xml.Xsl.Runtime
{
	// Token: 0x02000485 RID: 1157
	internal class XmlMergeSequenceWriter : XmlSequenceWriter
	{
		// Token: 0x06002D55 RID: 11605 RVA: 0x0010953A File Offset: 0x0010773A
		public XmlMergeSequenceWriter(XmlRawWriter xwrt)
		{
			this.xwrt = xwrt;
			this.lastItemWasAtomic = false;
		}

		// Token: 0x06002D56 RID: 11606 RVA: 0x00109550 File Offset: 0x00107750
		public override XmlRawWriter StartTree(XPathNodeType rootType, IXmlNamespaceResolver nsResolver, XmlNameTable nameTable)
		{
			if (rootType == XPathNodeType.Attribute || rootType == XPathNodeType.Namespace)
			{
				throw new XslTransformException("XmlWriter cannot process the sequence returned by the query, because it contains an attribute or namespace node.", new string[]
				{
					string.Empty
				});
			}
			this.xwrt.NamespaceResolver = nsResolver;
			return this.xwrt;
		}

		// Token: 0x06002D57 RID: 11607 RVA: 0x00109585 File Offset: 0x00107785
		public override void EndTree()
		{
			this.lastItemWasAtomic = false;
		}

		// Token: 0x06002D58 RID: 11608 RVA: 0x00109590 File Offset: 0x00107790
		public override void WriteItem(XPathItem item)
		{
			if (!item.IsNode)
			{
				this.WriteString(item.Value);
				return;
			}
			XPathNavigator xpathNavigator = item as XPathNavigator;
			if (xpathNavigator.NodeType == XPathNodeType.Attribute || xpathNavigator.NodeType == XPathNodeType.Namespace)
			{
				throw new XslTransformException("XmlWriter cannot process the sequence returned by the query, because it contains an attribute or namespace node.", new string[]
				{
					string.Empty
				});
			}
			this.CopyNode(xpathNavigator);
			this.lastItemWasAtomic = false;
		}

		// Token: 0x06002D59 RID: 11609 RVA: 0x001095F2 File Offset: 0x001077F2
		private void WriteString(string value)
		{
			if (this.lastItemWasAtomic)
			{
				this.xwrt.WriteWhitespace(" ");
			}
			else
			{
				this.lastItemWasAtomic = true;
			}
			this.xwrt.WriteString(value);
		}

		// Token: 0x06002D5A RID: 11610 RVA: 0x00109624 File Offset: 0x00107824
		private void CopyNode(XPathNavigator nav)
		{
			int num = 0;
			for (;;)
			{
				IL_02:
				if (this.CopyShallowNode(nav))
				{
					if (nav.NodeType == XPathNodeType.Element)
					{
						if (nav.MoveToFirstAttribute())
						{
							do
							{
								this.CopyShallowNode(nav);
							}
							while (nav.MoveToNextAttribute());
							nav.MoveToParent();
						}
						XPathNamespaceScope xpathNamespaceScope = (num == 0) ? XPathNamespaceScope.ExcludeXml : XPathNamespaceScope.Local;
						if (nav.MoveToFirstNamespace(xpathNamespaceScope))
						{
							this.CopyNamespaces(nav, xpathNamespaceScope);
							nav.MoveToParent();
						}
						this.xwrt.StartElementContent();
					}
					if (nav.MoveToFirstChild())
					{
						num++;
						continue;
					}
					if (nav.NodeType == XPathNodeType.Element)
					{
						this.xwrt.WriteEndElement(nav.Prefix, nav.LocalName, nav.NamespaceURI);
					}
				}
				while (num != 0)
				{
					if (nav.MoveToNext())
					{
						goto IL_02;
					}
					num--;
					nav.MoveToParent();
					if (nav.NodeType == XPathNodeType.Element)
					{
						this.xwrt.WriteFullEndElement(nav.Prefix, nav.LocalName, nav.NamespaceURI);
					}
				}
				break;
			}
		}

		// Token: 0x06002D5B RID: 11611 RVA: 0x00109708 File Offset: 0x00107908
		private bool CopyShallowNode(XPathNavigator nav)
		{
			bool result = false;
			switch (nav.NodeType)
			{
			case XPathNodeType.Root:
				result = true;
				break;
			case XPathNodeType.Element:
				this.xwrt.WriteStartElement(nav.Prefix, nav.LocalName, nav.NamespaceURI);
				result = true;
				break;
			case XPathNodeType.Attribute:
				this.xwrt.WriteStartAttribute(nav.Prefix, nav.LocalName, nav.NamespaceURI);
				this.xwrt.WriteString(nav.Value);
				this.xwrt.WriteEndAttribute();
				break;
			case XPathNodeType.Namespace:
				this.xwrt.WriteNamespaceDeclaration(nav.LocalName, nav.Value);
				break;
			case XPathNodeType.Text:
				this.xwrt.WriteString(nav.Value);
				break;
			case XPathNodeType.SignificantWhitespace:
			case XPathNodeType.Whitespace:
				this.xwrt.WriteWhitespace(nav.Value);
				break;
			case XPathNodeType.ProcessingInstruction:
				this.xwrt.WriteProcessingInstruction(nav.LocalName, nav.Value);
				break;
			case XPathNodeType.Comment:
				this.xwrt.WriteComment(nav.Value);
				break;
			}
			return result;
		}

		// Token: 0x06002D5C RID: 11612 RVA: 0x0010981C File Offset: 0x00107A1C
		private void CopyNamespaces(XPathNavigator nav, XPathNamespaceScope nsScope)
		{
			string localName = nav.LocalName;
			string value = nav.Value;
			if (nav.MoveToNextNamespace(nsScope))
			{
				this.CopyNamespaces(nav, nsScope);
			}
			this.xwrt.WriteNamespaceDeclaration(localName, value);
		}

		// Token: 0x04002324 RID: 8996
		private XmlRawWriter xwrt;

		// Token: 0x04002325 RID: 8997
		private bool lastItemWasAtomic;
	}
}

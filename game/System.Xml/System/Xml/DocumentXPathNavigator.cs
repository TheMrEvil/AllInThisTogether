using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Schema;
using System.Xml.XPath;

namespace System.Xml
{
	// Token: 0x020001A4 RID: 420
	internal sealed class DocumentXPathNavigator : XPathNavigator, IHasXmlNode
	{
		// Token: 0x06000F15 RID: 3861 RVA: 0x0006347C File Offset: 0x0006167C
		public DocumentXPathNavigator(XmlDocument document, XmlNode node)
		{
			this.document = document;
			this.ResetPosition(node);
		}

		// Token: 0x06000F16 RID: 3862 RVA: 0x00063492 File Offset: 0x00061692
		public DocumentXPathNavigator(DocumentXPathNavigator other)
		{
			this.document = other.document;
			this.source = other.source;
			this.attributeIndex = other.attributeIndex;
			this.namespaceParent = other.namespaceParent;
		}

		// Token: 0x06000F17 RID: 3863 RVA: 0x000634CA File Offset: 0x000616CA
		public override XPathNavigator Clone()
		{
			return new DocumentXPathNavigator(this);
		}

		// Token: 0x06000F18 RID: 3864 RVA: 0x000634D4 File Offset: 0x000616D4
		public override void SetValue(string value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			XmlNode xmlNode = this.source;
			switch (xmlNode.NodeType)
			{
			case XmlNodeType.Element:
			case XmlNodeType.ProcessingInstruction:
			case XmlNodeType.Comment:
				break;
			case XmlNodeType.Attribute:
				if (!((XmlAttribute)xmlNode).IsNamespace)
				{
					xmlNode.InnerText = value;
					return;
				}
				goto IL_B8;
			case XmlNodeType.Text:
			case XmlNodeType.CDATA:
			case XmlNodeType.Whitespace:
			case XmlNodeType.SignificantWhitespace:
			{
				this.CalibrateText();
				xmlNode = this.source;
				XmlNode xmlNode2 = this.TextEnd(xmlNode);
				if (xmlNode != xmlNode2)
				{
					if (xmlNode.IsReadOnly)
					{
						throw new InvalidOperationException(Res.GetString("This node is read-only. It cannot be modified."));
					}
					DocumentXPathNavigator.DeleteToFollowingSibling(xmlNode.NextSibling, xmlNode2);
				}
				break;
			}
			case XmlNodeType.EntityReference:
			case XmlNodeType.Entity:
			case XmlNodeType.Document:
			case XmlNodeType.DocumentType:
			case XmlNodeType.DocumentFragment:
			case XmlNodeType.Notation:
				goto IL_B8;
			default:
				goto IL_B8;
			}
			xmlNode.InnerText = value;
			return;
			IL_B8:
			throw new InvalidOperationException(Res.GetString("Operation is not valid due to the current position of the navigator."));
		}

		// Token: 0x17000257 RID: 599
		// (get) Token: 0x06000F19 RID: 3865 RVA: 0x000635A8 File Offset: 0x000617A8
		public override XmlNameTable NameTable
		{
			get
			{
				return this.document.NameTable;
			}
		}

		// Token: 0x17000258 RID: 600
		// (get) Token: 0x06000F1A RID: 3866 RVA: 0x000635B5 File Offset: 0x000617B5
		public override XPathNodeType NodeType
		{
			get
			{
				this.CalibrateText();
				return this.source.XPNodeType;
			}
		}

		// Token: 0x17000259 RID: 601
		// (get) Token: 0x06000F1B RID: 3867 RVA: 0x000635C8 File Offset: 0x000617C8
		public override string LocalName
		{
			get
			{
				return this.source.XPLocalName;
			}
		}

		// Token: 0x1700025A RID: 602
		// (get) Token: 0x06000F1C RID: 3868 RVA: 0x000635D8 File Offset: 0x000617D8
		public override string NamespaceURI
		{
			get
			{
				XmlAttribute xmlAttribute = this.source as XmlAttribute;
				if (xmlAttribute != null && xmlAttribute.IsNamespace)
				{
					return string.Empty;
				}
				return this.source.NamespaceURI;
			}
		}

		// Token: 0x1700025B RID: 603
		// (get) Token: 0x06000F1D RID: 3869 RVA: 0x00063610 File Offset: 0x00061810
		public override string Name
		{
			get
			{
				XmlNodeType nodeType = this.source.NodeType;
				if (nodeType != XmlNodeType.Element)
				{
					if (nodeType != XmlNodeType.Attribute)
					{
						if (nodeType != XmlNodeType.ProcessingInstruction)
						{
							return string.Empty;
						}
					}
					else
					{
						if (!((XmlAttribute)this.source).IsNamespace)
						{
							return this.source.Name;
						}
						string localName = this.source.LocalName;
						if (Ref.Equal(localName, this.document.strXmlns))
						{
							return string.Empty;
						}
						return localName;
					}
				}
				return this.source.Name;
			}
		}

		// Token: 0x1700025C RID: 604
		// (get) Token: 0x06000F1E RID: 3870 RVA: 0x0006368C File Offset: 0x0006188C
		public override string Prefix
		{
			get
			{
				XmlAttribute xmlAttribute = this.source as XmlAttribute;
				if (xmlAttribute != null && xmlAttribute.IsNamespace)
				{
					return string.Empty;
				}
				return this.source.Prefix;
			}
		}

		// Token: 0x1700025D RID: 605
		// (get) Token: 0x06000F1F RID: 3871 RVA: 0x000636C4 File Offset: 0x000618C4
		public override string Value
		{
			get
			{
				XmlNodeType nodeType = this.source.NodeType;
				if (nodeType != XmlNodeType.Element)
				{
					if (nodeType - XmlNodeType.Text > 1)
					{
						switch (nodeType)
						{
						case XmlNodeType.Document:
							return this.ValueDocument;
						case XmlNodeType.DocumentFragment:
							goto IL_39;
						case XmlNodeType.Whitespace:
						case XmlNodeType.SignificantWhitespace:
							goto IL_4C;
						}
						return this.source.Value;
					}
					IL_4C:
					return this.ValueText;
				}
				IL_39:
				return this.source.InnerText;
			}
		}

		// Token: 0x1700025E RID: 606
		// (get) Token: 0x06000F20 RID: 3872 RVA: 0x00063730 File Offset: 0x00061930
		private string ValueDocument
		{
			get
			{
				XmlElement documentElement = this.document.DocumentElement;
				if (documentElement != null)
				{
					return documentElement.InnerText;
				}
				return string.Empty;
			}
		}

		// Token: 0x1700025F RID: 607
		// (get) Token: 0x06000F21 RID: 3873 RVA: 0x00063758 File Offset: 0x00061958
		private string ValueText
		{
			get
			{
				this.CalibrateText();
				string text = this.source.Value;
				XmlNode xmlNode = this.NextSibling(this.source);
				if (xmlNode != null && xmlNode.IsText)
				{
					StringBuilder stringBuilder = new StringBuilder(text);
					do
					{
						stringBuilder.Append(xmlNode.Value);
						xmlNode = this.NextSibling(xmlNode);
					}
					while (xmlNode != null && xmlNode.IsText);
					text = stringBuilder.ToString();
				}
				return text;
			}
		}

		// Token: 0x17000260 RID: 608
		// (get) Token: 0x06000F22 RID: 3874 RVA: 0x000637BE File Offset: 0x000619BE
		public override string BaseURI
		{
			get
			{
				return this.source.BaseURI;
			}
		}

		// Token: 0x17000261 RID: 609
		// (get) Token: 0x06000F23 RID: 3875 RVA: 0x000637CC File Offset: 0x000619CC
		public override bool IsEmptyElement
		{
			get
			{
				XmlElement xmlElement = this.source as XmlElement;
				return xmlElement != null && xmlElement.IsEmpty;
			}
		}

		// Token: 0x17000262 RID: 610
		// (get) Token: 0x06000F24 RID: 3876 RVA: 0x000637F0 File Offset: 0x000619F0
		public override string XmlLang
		{
			get
			{
				return this.source.XmlLang;
			}
		}

		// Token: 0x17000263 RID: 611
		// (get) Token: 0x06000F25 RID: 3877 RVA: 0x000637FD File Offset: 0x000619FD
		public override object UnderlyingObject
		{
			get
			{
				this.CalibrateText();
				return this.source;
			}
		}

		// Token: 0x17000264 RID: 612
		// (get) Token: 0x06000F26 RID: 3878 RVA: 0x0006380C File Offset: 0x00061A0C
		public override bool HasAttributes
		{
			get
			{
				XmlElement xmlElement = this.source as XmlElement;
				if (xmlElement != null && xmlElement.HasAttributes)
				{
					XmlAttributeCollection attributes = xmlElement.Attributes;
					for (int i = 0; i < attributes.Count; i++)
					{
						if (!attributes[i].IsNamespace)
						{
							return true;
						}
					}
				}
				return false;
			}
		}

		// Token: 0x06000F27 RID: 3879 RVA: 0x00063859 File Offset: 0x00061A59
		public override string GetAttribute(string localName, string namespaceURI)
		{
			return this.source.GetXPAttribute(localName, namespaceURI);
		}

		// Token: 0x06000F28 RID: 3880 RVA: 0x00063868 File Offset: 0x00061A68
		public override bool MoveToAttribute(string localName, string namespaceURI)
		{
			XmlElement xmlElement = this.source as XmlElement;
			if (xmlElement != null && xmlElement.HasAttributes)
			{
				XmlAttributeCollection attributes = xmlElement.Attributes;
				int i = 0;
				while (i < attributes.Count)
				{
					XmlAttribute xmlAttribute = attributes[i];
					if (xmlAttribute.LocalName == localName && xmlAttribute.NamespaceURI == namespaceURI)
					{
						if (!xmlAttribute.IsNamespace)
						{
							this.source = xmlAttribute;
							this.attributeIndex = i;
							return true;
						}
						return false;
					}
					else
					{
						i++;
					}
				}
			}
			return false;
		}

		// Token: 0x06000F29 RID: 3881 RVA: 0x000638E4 File Offset: 0x00061AE4
		public override bool MoveToFirstAttribute()
		{
			XmlElement xmlElement = this.source as XmlElement;
			if (xmlElement != null && xmlElement.HasAttributes)
			{
				XmlAttributeCollection attributes = xmlElement.Attributes;
				for (int i = 0; i < attributes.Count; i++)
				{
					XmlAttribute xmlAttribute = attributes[i];
					if (!xmlAttribute.IsNamespace)
					{
						this.source = xmlAttribute;
						this.attributeIndex = i;
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06000F2A RID: 3882 RVA: 0x00063944 File Offset: 0x00061B44
		public override bool MoveToNextAttribute()
		{
			XmlAttribute xmlAttribute = this.source as XmlAttribute;
			if (xmlAttribute == null || xmlAttribute.IsNamespace)
			{
				return false;
			}
			XmlAttributeCollection xmlAttributeCollection;
			if (!DocumentXPathNavigator.CheckAttributePosition(xmlAttribute, out xmlAttributeCollection, this.attributeIndex) && !DocumentXPathNavigator.ResetAttributePosition(xmlAttribute, xmlAttributeCollection, out this.attributeIndex))
			{
				return false;
			}
			for (int i = this.attributeIndex + 1; i < xmlAttributeCollection.Count; i++)
			{
				xmlAttribute = xmlAttributeCollection[i];
				if (!xmlAttribute.IsNamespace)
				{
					this.source = xmlAttribute;
					this.attributeIndex = i;
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000F2B RID: 3883 RVA: 0x000639C4 File Offset: 0x00061BC4
		public override string GetNamespace(string name)
		{
			XmlNode xmlNode = this.source;
			while (xmlNode != null && xmlNode.NodeType != XmlNodeType.Element)
			{
				XmlAttribute xmlAttribute = xmlNode as XmlAttribute;
				if (xmlAttribute != null)
				{
					xmlNode = xmlAttribute.OwnerElement;
				}
				else
				{
					xmlNode = xmlNode.ParentNode;
				}
			}
			XmlElement xmlElement = xmlNode as XmlElement;
			if (xmlElement != null)
			{
				string localName;
				if (name != null && name.Length != 0)
				{
					localName = name;
				}
				else
				{
					localName = this.document.strXmlns;
				}
				string strReservedXmlns = this.document.strReservedXmlns;
				XmlAttribute attributeNode;
				for (;;)
				{
					attributeNode = xmlElement.GetAttributeNode(localName, strReservedXmlns);
					if (attributeNode != null)
					{
						break;
					}
					xmlElement = (xmlElement.ParentNode as XmlElement);
					if (xmlElement == null)
					{
						goto IL_87;
					}
				}
				return attributeNode.Value;
			}
			IL_87:
			if (name == this.document.strXml)
			{
				return this.document.strReservedXml;
			}
			if (name == this.document.strXmlns)
			{
				return this.document.strReservedXmlns;
			}
			return string.Empty;
		}

		// Token: 0x06000F2C RID: 3884 RVA: 0x00063A9C File Offset: 0x00061C9C
		public override bool MoveToNamespace(string name)
		{
			if (name == this.document.strXmlns)
			{
				return false;
			}
			XmlElement xmlElement = this.source as XmlElement;
			if (xmlElement != null)
			{
				string localName;
				if (name != null && name.Length != 0)
				{
					localName = name;
				}
				else
				{
					localName = this.document.strXmlns;
				}
				string strReservedXmlns = this.document.strReservedXmlns;
				XmlAttribute attributeNode;
				for (;;)
				{
					attributeNode = xmlElement.GetAttributeNode(localName, strReservedXmlns);
					if (attributeNode != null)
					{
						break;
					}
					xmlElement = (xmlElement.ParentNode as XmlElement);
					if (xmlElement == null)
					{
						goto Block_6;
					}
				}
				this.namespaceParent = (XmlElement)this.source;
				this.source = attributeNode;
				return true;
				Block_6:
				if (name == this.document.strXml)
				{
					this.namespaceParent = (XmlElement)this.source;
					this.source = this.document.NamespaceXml;
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000F2D RID: 3885 RVA: 0x00063B64 File Offset: 0x00061D64
		public override bool MoveToFirstNamespace(XPathNamespaceScope scope)
		{
			XmlElement xmlElement = this.source as XmlElement;
			if (xmlElement == null)
			{
				return false;
			}
			int maxValue = int.MaxValue;
			switch (scope)
			{
			case XPathNamespaceScope.All:
			{
				XmlAttributeCollection attributes = xmlElement.Attributes;
				if (!DocumentXPathNavigator.MoveToFirstNamespaceGlobal(ref attributes, ref maxValue))
				{
					this.source = this.document.NamespaceXml;
				}
				else
				{
					this.source = attributes[maxValue];
					this.attributeIndex = maxValue;
				}
				this.namespaceParent = xmlElement;
				break;
			}
			case XPathNamespaceScope.ExcludeXml:
			{
				XmlAttributeCollection attributes = xmlElement.Attributes;
				if (!DocumentXPathNavigator.MoveToFirstNamespaceGlobal(ref attributes, ref maxValue))
				{
					return false;
				}
				XmlAttribute xmlAttribute = attributes[maxValue];
				while (Ref.Equal(xmlAttribute.LocalName, this.document.strXml))
				{
					if (!DocumentXPathNavigator.MoveToNextNamespaceGlobal(ref attributes, ref maxValue))
					{
						return false;
					}
					xmlAttribute = attributes[maxValue];
				}
				this.source = xmlAttribute;
				this.attributeIndex = maxValue;
				this.namespaceParent = xmlElement;
				break;
			}
			case XPathNamespaceScope.Local:
			{
				if (!xmlElement.HasAttributes)
				{
					return false;
				}
				XmlAttributeCollection attributes = xmlElement.Attributes;
				if (!DocumentXPathNavigator.MoveToFirstNamespaceLocal(attributes, ref maxValue))
				{
					return false;
				}
				this.source = attributes[maxValue];
				this.attributeIndex = maxValue;
				this.namespaceParent = xmlElement;
				break;
			}
			default:
				return false;
			}
			return true;
		}

		// Token: 0x06000F2E RID: 3886 RVA: 0x00063C84 File Offset: 0x00061E84
		private static bool MoveToFirstNamespaceLocal(XmlAttributeCollection attributes, ref int index)
		{
			for (int i = attributes.Count - 1; i >= 0; i--)
			{
				if (attributes[i].IsNamespace)
				{
					index = i;
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000F2F RID: 3887 RVA: 0x00063CB8 File Offset: 0x00061EB8
		private static bool MoveToFirstNamespaceGlobal(ref XmlAttributeCollection attributes, ref int index)
		{
			if (DocumentXPathNavigator.MoveToFirstNamespaceLocal(attributes, ref index))
			{
				return true;
			}
			for (XmlElement xmlElement = attributes.parent.ParentNode as XmlElement; xmlElement != null; xmlElement = (xmlElement.ParentNode as XmlElement))
			{
				if (xmlElement.HasAttributes)
				{
					attributes = xmlElement.Attributes;
					if (DocumentXPathNavigator.MoveToFirstNamespaceLocal(attributes, ref index))
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06000F30 RID: 3888 RVA: 0x00063D14 File Offset: 0x00061F14
		public override bool MoveToNextNamespace(XPathNamespaceScope scope)
		{
			XmlAttribute xmlAttribute = this.source as XmlAttribute;
			if (xmlAttribute == null || !xmlAttribute.IsNamespace)
			{
				return false;
			}
			int num = this.attributeIndex;
			XmlAttributeCollection xmlAttributeCollection;
			if (!DocumentXPathNavigator.CheckAttributePosition(xmlAttribute, out xmlAttributeCollection, num) && !DocumentXPathNavigator.ResetAttributePosition(xmlAttribute, xmlAttributeCollection, out num))
			{
				return false;
			}
			switch (scope)
			{
			case XPathNamespaceScope.All:
				while (DocumentXPathNavigator.MoveToNextNamespaceGlobal(ref xmlAttributeCollection, ref num))
				{
					xmlAttribute = xmlAttributeCollection[num];
					if (!this.PathHasDuplicateNamespace(xmlAttribute.OwnerElement, this.namespaceParent, xmlAttribute.LocalName))
					{
						this.source = xmlAttribute;
						this.attributeIndex = num;
						return true;
					}
				}
				if (this.PathHasDuplicateNamespace(null, this.namespaceParent, this.document.strXml))
				{
					return false;
				}
				this.source = this.document.NamespaceXml;
				return true;
			case XPathNamespaceScope.ExcludeXml:
				while (DocumentXPathNavigator.MoveToNextNamespaceGlobal(ref xmlAttributeCollection, ref num))
				{
					xmlAttribute = xmlAttributeCollection[num];
					string localName = xmlAttribute.LocalName;
					if (!this.PathHasDuplicateNamespace(xmlAttribute.OwnerElement, this.namespaceParent, localName) && !Ref.Equal(localName, this.document.strXml))
					{
						this.source = xmlAttribute;
						this.attributeIndex = num;
						return true;
					}
				}
				return false;
			case XPathNamespaceScope.Local:
				if (xmlAttribute.OwnerElement != this.namespaceParent)
				{
					return false;
				}
				if (!DocumentXPathNavigator.MoveToNextNamespaceLocal(xmlAttributeCollection, ref num))
				{
					return false;
				}
				this.source = xmlAttributeCollection[num];
				this.attributeIndex = num;
				break;
			default:
				return false;
			}
			return true;
		}

		// Token: 0x06000F31 RID: 3889 RVA: 0x00063E68 File Offset: 0x00062068
		private static bool MoveToNextNamespaceLocal(XmlAttributeCollection attributes, ref int index)
		{
			for (int i = index - 1; i >= 0; i--)
			{
				if (attributes[i].IsNamespace)
				{
					index = i;
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000F32 RID: 3890 RVA: 0x00063E98 File Offset: 0x00062098
		private static bool MoveToNextNamespaceGlobal(ref XmlAttributeCollection attributes, ref int index)
		{
			if (DocumentXPathNavigator.MoveToNextNamespaceLocal(attributes, ref index))
			{
				return true;
			}
			for (XmlElement xmlElement = attributes.parent.ParentNode as XmlElement; xmlElement != null; xmlElement = (xmlElement.ParentNode as XmlElement))
			{
				if (xmlElement.HasAttributes)
				{
					attributes = xmlElement.Attributes;
					if (DocumentXPathNavigator.MoveToFirstNamespaceLocal(attributes, ref index))
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06000F33 RID: 3891 RVA: 0x00063EF4 File Offset: 0x000620F4
		private bool PathHasDuplicateNamespace(XmlElement top, XmlElement bottom, string localName)
		{
			string strReservedXmlns = this.document.strReservedXmlns;
			while (bottom != null && bottom != top)
			{
				if (bottom.GetAttributeNode(localName, strReservedXmlns) != null)
				{
					return true;
				}
				bottom = (bottom.ParentNode as XmlElement);
			}
			return false;
		}

		// Token: 0x06000F34 RID: 3892 RVA: 0x00063F30 File Offset: 0x00062130
		public override string LookupNamespace(string prefix)
		{
			string text = base.LookupNamespace(prefix);
			if (text != null)
			{
				text = this.NameTable.Add(text);
			}
			return text;
		}

		// Token: 0x06000F35 RID: 3893 RVA: 0x00063F58 File Offset: 0x00062158
		public override bool MoveToNext()
		{
			XmlNode xmlNode = this.NextSibling(this.source);
			if (xmlNode == null)
			{
				return false;
			}
			if (xmlNode.IsText && this.source.IsText)
			{
				xmlNode = this.NextSibling(this.TextEnd(xmlNode));
				if (xmlNode == null)
				{
					return false;
				}
			}
			XmlNode parent = this.ParentNode(xmlNode);
			while (!DocumentXPathNavigator.IsValidChild(parent, xmlNode))
			{
				xmlNode = this.NextSibling(xmlNode);
				if (xmlNode == null)
				{
					return false;
				}
			}
			this.source = xmlNode;
			return true;
		}

		// Token: 0x06000F36 RID: 3894 RVA: 0x00063FC8 File Offset: 0x000621C8
		public override bool MoveToPrevious()
		{
			XmlNode xmlNode = this.PreviousSibling(this.source);
			if (xmlNode == null)
			{
				return false;
			}
			if (xmlNode.IsText)
			{
				if (this.source.IsText)
				{
					xmlNode = this.PreviousSibling(this.TextStart(xmlNode));
					if (xmlNode == null)
					{
						return false;
					}
				}
				else
				{
					xmlNode = this.TextStart(xmlNode);
				}
			}
			XmlNode parent = this.ParentNode(xmlNode);
			while (!DocumentXPathNavigator.IsValidChild(parent, xmlNode))
			{
				xmlNode = this.PreviousSibling(xmlNode);
				if (xmlNode == null)
				{
					return false;
				}
			}
			this.source = xmlNode;
			return true;
		}

		// Token: 0x06000F37 RID: 3895 RVA: 0x00064040 File Offset: 0x00062240
		public override bool MoveToFirst()
		{
			if (this.source.NodeType == XmlNodeType.Attribute)
			{
				return false;
			}
			XmlNode xmlNode = this.ParentNode(this.source);
			if (xmlNode == null)
			{
				return false;
			}
			XmlNode xmlNode2 = this.FirstChild(xmlNode);
			while (!DocumentXPathNavigator.IsValidChild(xmlNode, xmlNode2))
			{
				xmlNode2 = this.NextSibling(xmlNode2);
				if (xmlNode2 == null)
				{
					return false;
				}
			}
			this.source = xmlNode2;
			return true;
		}

		// Token: 0x06000F38 RID: 3896 RVA: 0x00064098 File Offset: 0x00062298
		public override bool MoveToFirstChild()
		{
			XmlNodeType nodeType = this.source.NodeType;
			XmlNode xmlNode;
			if (nodeType != XmlNodeType.Element)
			{
				if (nodeType != XmlNodeType.Document && nodeType != XmlNodeType.DocumentFragment)
				{
					return false;
				}
				xmlNode = this.FirstChild(this.source);
				if (xmlNode == null)
				{
					return false;
				}
				while (!DocumentXPathNavigator.IsValidChild(this.source, xmlNode))
				{
					xmlNode = this.NextSibling(xmlNode);
					if (xmlNode == null)
					{
						return false;
					}
				}
			}
			else
			{
				xmlNode = this.FirstChild(this.source);
				if (xmlNode == null)
				{
					return false;
				}
			}
			this.source = xmlNode;
			return true;
		}

		// Token: 0x06000F39 RID: 3897 RVA: 0x0006410C File Offset: 0x0006230C
		public override bool MoveToParent()
		{
			XmlNode xmlNode = this.ParentNode(this.source);
			if (xmlNode != null)
			{
				this.source = xmlNode;
				return true;
			}
			XmlAttribute xmlAttribute = this.source as XmlAttribute;
			if (xmlAttribute != null)
			{
				xmlNode = (xmlAttribute.IsNamespace ? this.namespaceParent : xmlAttribute.OwnerElement);
				if (xmlNode != null)
				{
					this.source = xmlNode;
					this.namespaceParent = null;
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000F3A RID: 3898 RVA: 0x0006416C File Offset: 0x0006236C
		public override void MoveToRoot()
		{
			for (;;)
			{
				XmlNode xmlNode = this.source.ParentNode;
				if (xmlNode == null)
				{
					XmlAttribute xmlAttribute = this.source as XmlAttribute;
					if (xmlAttribute == null)
					{
						break;
					}
					xmlNode = (xmlAttribute.IsNamespace ? this.namespaceParent : xmlAttribute.OwnerElement);
					if (xmlNode == null)
					{
						break;
					}
				}
				this.source = xmlNode;
			}
			this.namespaceParent = null;
		}

		// Token: 0x06000F3B RID: 3899 RVA: 0x000641C4 File Offset: 0x000623C4
		public override bool MoveTo(XPathNavigator other)
		{
			DocumentXPathNavigator documentXPathNavigator = other as DocumentXPathNavigator;
			if (documentXPathNavigator != null && this.document == documentXPathNavigator.document)
			{
				this.source = documentXPathNavigator.source;
				this.attributeIndex = documentXPathNavigator.attributeIndex;
				this.namespaceParent = documentXPathNavigator.namespaceParent;
				return true;
			}
			return false;
		}

		// Token: 0x06000F3C RID: 3900 RVA: 0x00064210 File Offset: 0x00062410
		public override bool MoveToId(string id)
		{
			XmlElement elementById = this.document.GetElementById(id);
			if (elementById != null)
			{
				this.source = elementById;
				this.namespaceParent = null;
				return true;
			}
			return false;
		}

		// Token: 0x06000F3D RID: 3901 RVA: 0x00064240 File Offset: 0x00062440
		public override bool MoveToChild(string localName, string namespaceUri)
		{
			if (this.source.NodeType == XmlNodeType.Attribute)
			{
				return false;
			}
			XmlNode xmlNode = this.FirstChild(this.source);
			if (xmlNode != null)
			{
				while (xmlNode.NodeType != XmlNodeType.Element || !(xmlNode.LocalName == localName) || !(xmlNode.NamespaceURI == namespaceUri))
				{
					xmlNode = this.NextSibling(xmlNode);
					if (xmlNode == null)
					{
						return false;
					}
				}
				this.source = xmlNode;
				return true;
			}
			return false;
		}

		// Token: 0x06000F3E RID: 3902 RVA: 0x000642A8 File Offset: 0x000624A8
		public override bool MoveToChild(XPathNodeType type)
		{
			if (this.source.NodeType == XmlNodeType.Attribute)
			{
				return false;
			}
			XmlNode xmlNode = this.FirstChild(this.source);
			if (xmlNode != null)
			{
				int contentKindMask = XPathNavigator.GetContentKindMask(type);
				if (contentKindMask == 0)
				{
					return false;
				}
				while ((1 << (int)xmlNode.XPNodeType & contentKindMask) == 0)
				{
					xmlNode = this.NextSibling(xmlNode);
					if (xmlNode == null)
					{
						return false;
					}
				}
				this.source = xmlNode;
				return true;
			}
			return false;
		}

		// Token: 0x06000F3F RID: 3903 RVA: 0x00064308 File Offset: 0x00062508
		public override bool MoveToFollowing(string localName, string namespaceUri, XPathNavigator end)
		{
			XmlNode xmlNode = null;
			DocumentXPathNavigator documentXPathNavigator = end as DocumentXPathNavigator;
			if (documentXPathNavigator != null)
			{
				if (this.document != documentXPathNavigator.document)
				{
					return false;
				}
				if (documentXPathNavigator.source.NodeType == XmlNodeType.Attribute)
				{
					documentXPathNavigator = (DocumentXPathNavigator)documentXPathNavigator.Clone();
					if (!documentXPathNavigator.MoveToNonDescendant())
					{
						return false;
					}
				}
				xmlNode = documentXPathNavigator.source;
			}
			XmlNode xmlNode2 = this.source;
			if (xmlNode2.NodeType == XmlNodeType.Attribute)
			{
				xmlNode2 = ((XmlAttribute)xmlNode2).OwnerElement;
				if (xmlNode2 == null)
				{
					return false;
				}
			}
			for (;;)
			{
				XmlNode firstChild = xmlNode2.FirstChild;
				if (firstChild != null)
				{
					xmlNode2 = firstChild;
				}
				else
				{
					XmlNode nextSibling;
					for (;;)
					{
						nextSibling = xmlNode2.NextSibling;
						if (nextSibling != null)
						{
							break;
						}
						XmlNode parentNode = xmlNode2.ParentNode;
						if (parentNode == null)
						{
							return false;
						}
						xmlNode2 = parentNode;
					}
					xmlNode2 = nextSibling;
				}
				if (xmlNode2 == xmlNode)
				{
					return false;
				}
				if (xmlNode2.NodeType == XmlNodeType.Element && !(xmlNode2.LocalName != localName) && !(xmlNode2.NamespaceURI != namespaceUri))
				{
					goto Block_13;
				}
			}
			return false;
			Block_13:
			this.source = xmlNode2;
			return true;
		}

		// Token: 0x06000F40 RID: 3904 RVA: 0x000643E4 File Offset: 0x000625E4
		public override bool MoveToFollowing(XPathNodeType type, XPathNavigator end)
		{
			XmlNode xmlNode = null;
			DocumentXPathNavigator documentXPathNavigator = end as DocumentXPathNavigator;
			if (documentXPathNavigator != null)
			{
				if (this.document != documentXPathNavigator.document)
				{
					return false;
				}
				if (documentXPathNavigator.source.NodeType == XmlNodeType.Attribute)
				{
					documentXPathNavigator = (DocumentXPathNavigator)documentXPathNavigator.Clone();
					if (!documentXPathNavigator.MoveToNonDescendant())
					{
						return false;
					}
				}
				xmlNode = documentXPathNavigator.source;
			}
			int contentKindMask = XPathNavigator.GetContentKindMask(type);
			if (contentKindMask == 0)
			{
				return false;
			}
			XmlNode xmlNode2 = this.source;
			XmlNodeType nodeType = xmlNode2.NodeType;
			if (nodeType != XmlNodeType.Attribute)
			{
				if (nodeType - XmlNodeType.Text <= 1 || nodeType - XmlNodeType.Whitespace <= 1)
				{
					xmlNode2 = this.TextEnd(xmlNode2);
				}
			}
			else
			{
				xmlNode2 = ((XmlAttribute)xmlNode2).OwnerElement;
				if (xmlNode2 == null)
				{
					return false;
				}
			}
			for (;;)
			{
				XmlNode firstChild = xmlNode2.FirstChild;
				if (firstChild != null)
				{
					xmlNode2 = firstChild;
				}
				else
				{
					XmlNode nextSibling;
					for (;;)
					{
						nextSibling = xmlNode2.NextSibling;
						if (nextSibling != null)
						{
							break;
						}
						XmlNode parentNode = xmlNode2.ParentNode;
						if (parentNode == null)
						{
							return false;
						}
						xmlNode2 = parentNode;
					}
					xmlNode2 = nextSibling;
				}
				if (xmlNode2 == xmlNode)
				{
					return false;
				}
				if ((1 << (int)xmlNode2.XPNodeType & contentKindMask) != 0)
				{
					goto Block_14;
				}
			}
			return false;
			Block_14:
			this.source = xmlNode2;
			return true;
		}

		// Token: 0x06000F41 RID: 3905 RVA: 0x000644D4 File Offset: 0x000626D4
		public override bool MoveToNext(string localName, string namespaceUri)
		{
			XmlNode xmlNode = this.NextSibling(this.source);
			if (xmlNode == null)
			{
				return false;
			}
			while (xmlNode.NodeType != XmlNodeType.Element || !(xmlNode.LocalName == localName) || !(xmlNode.NamespaceURI == namespaceUri))
			{
				xmlNode = this.NextSibling(xmlNode);
				if (xmlNode == null)
				{
					return false;
				}
			}
			this.source = xmlNode;
			return true;
		}

		// Token: 0x06000F42 RID: 3906 RVA: 0x00064530 File Offset: 0x00062730
		public override bool MoveToNext(XPathNodeType type)
		{
			XmlNode xmlNode = this.NextSibling(this.source);
			if (xmlNode == null)
			{
				return false;
			}
			if (xmlNode.IsText && this.source.IsText)
			{
				xmlNode = this.NextSibling(this.TextEnd(xmlNode));
				if (xmlNode == null)
				{
					return false;
				}
			}
			int contentKindMask = XPathNavigator.GetContentKindMask(type);
			if (contentKindMask == 0)
			{
				return false;
			}
			while ((1 << (int)xmlNode.XPNodeType & contentKindMask) == 0)
			{
				xmlNode = this.NextSibling(xmlNode);
				if (xmlNode == null)
				{
					return false;
				}
			}
			this.source = xmlNode;
			return true;
		}

		// Token: 0x17000265 RID: 613
		// (get) Token: 0x06000F43 RID: 3907 RVA: 0x000645A8 File Offset: 0x000627A8
		public override bool HasChildren
		{
			get
			{
				XmlNodeType nodeType = this.source.NodeType;
				if (nodeType == XmlNodeType.Element)
				{
					return this.FirstChild(this.source) != null;
				}
				if (nodeType != XmlNodeType.Document && nodeType != XmlNodeType.DocumentFragment)
				{
					return false;
				}
				XmlNode xmlNode = this.FirstChild(this.source);
				if (xmlNode == null)
				{
					return false;
				}
				while (!DocumentXPathNavigator.IsValidChild(this.source, xmlNode))
				{
					xmlNode = this.NextSibling(xmlNode);
					if (xmlNode == null)
					{
						return false;
					}
				}
				return true;
			}
		}

		// Token: 0x06000F44 RID: 3908 RVA: 0x00064618 File Offset: 0x00062818
		public override bool IsSamePosition(XPathNavigator other)
		{
			DocumentXPathNavigator documentXPathNavigator = other as DocumentXPathNavigator;
			if (documentXPathNavigator != null)
			{
				this.CalibrateText();
				documentXPathNavigator.CalibrateText();
				return this.source == documentXPathNavigator.source && this.namespaceParent == documentXPathNavigator.namespaceParent;
			}
			return false;
		}

		// Token: 0x06000F45 RID: 3909 RVA: 0x0006465C File Offset: 0x0006285C
		public override bool IsDescendant(XPathNavigator other)
		{
			DocumentXPathNavigator documentXPathNavigator = other as DocumentXPathNavigator;
			return documentXPathNavigator != null && DocumentXPathNavigator.IsDescendant(this.source, documentXPathNavigator.source);
		}

		// Token: 0x17000266 RID: 614
		// (get) Token: 0x06000F46 RID: 3910 RVA: 0x00064686 File Offset: 0x00062886
		public override IXmlSchemaInfo SchemaInfo
		{
			get
			{
				return this.source.SchemaInfo;
			}
		}

		// Token: 0x06000F47 RID: 3911 RVA: 0x00064694 File Offset: 0x00062894
		public override bool CheckValidity(XmlSchemaSet schemas, ValidationEventHandler validationEventHandler)
		{
			XmlDocument xmlDocument;
			if (this.source.NodeType == XmlNodeType.Document)
			{
				xmlDocument = (XmlDocument)this.source;
			}
			else
			{
				xmlDocument = this.source.OwnerDocument;
				if (schemas != null)
				{
					throw new ArgumentException(Res.GetString("An XmlSchemaSet is only allowed as a parameter on the Root node.", null));
				}
			}
			if (schemas == null && xmlDocument != null)
			{
				schemas = xmlDocument.Schemas;
			}
			if (schemas == null || schemas.Count == 0)
			{
				throw new InvalidOperationException(Res.GetString("The XmlSchemaSet on the document is either null or has no schemas in it. Provide schema information before calling Validate."));
			}
			return new DocumentSchemaValidator(xmlDocument, schemas, validationEventHandler)
			{
				PsviAugmentation = false
			}.Validate(this.source);
		}

		// Token: 0x06000F48 RID: 3912 RVA: 0x00064724 File Offset: 0x00062924
		private static XmlNode OwnerNode(XmlNode node)
		{
			XmlNode parentNode = node.ParentNode;
			if (parentNode != null)
			{
				return parentNode;
			}
			XmlAttribute xmlAttribute = node as XmlAttribute;
			if (xmlAttribute != null)
			{
				return xmlAttribute.OwnerElement;
			}
			return null;
		}

		// Token: 0x06000F49 RID: 3913 RVA: 0x00064750 File Offset: 0x00062950
		private static int GetDepth(XmlNode node)
		{
			int num = 0;
			for (XmlNode node2 = DocumentXPathNavigator.OwnerNode(node); node2 != null; node2 = DocumentXPathNavigator.OwnerNode(node2))
			{
				num++;
			}
			return num;
		}

		// Token: 0x06000F4A RID: 3914 RVA: 0x00064778 File Offset: 0x00062978
		private XmlNodeOrder Compare(XmlNode node1, XmlNode node2)
		{
			if (node1.XPNodeType == XPathNodeType.Attribute)
			{
				if (node2.XPNodeType == XPathNodeType.Attribute)
				{
					XmlElement ownerElement = ((XmlAttribute)node1).OwnerElement;
					if (ownerElement.HasAttributes)
					{
						XmlAttributeCollection attributes = ownerElement.Attributes;
						for (int i = 0; i < attributes.Count; i++)
						{
							XmlAttribute xmlAttribute = attributes[i];
							if (xmlAttribute == node1)
							{
								return XmlNodeOrder.Before;
							}
							if (xmlAttribute == node2)
							{
								return XmlNodeOrder.After;
							}
						}
					}
					return XmlNodeOrder.Unknown;
				}
				return XmlNodeOrder.Before;
			}
			else
			{
				if (node2.XPNodeType == XPathNodeType.Attribute)
				{
					return XmlNodeOrder.After;
				}
				XmlNode nextSibling = node1.NextSibling;
				while (nextSibling != null && nextSibling != node2)
				{
					nextSibling = nextSibling.NextSibling;
				}
				if (nextSibling == null)
				{
					return XmlNodeOrder.After;
				}
				return XmlNodeOrder.Before;
			}
		}

		// Token: 0x06000F4B RID: 3915 RVA: 0x00064808 File Offset: 0x00062A08
		public override XmlNodeOrder ComparePosition(XPathNavigator other)
		{
			DocumentXPathNavigator documentXPathNavigator = other as DocumentXPathNavigator;
			if (documentXPathNavigator == null)
			{
				return XmlNodeOrder.Unknown;
			}
			this.CalibrateText();
			documentXPathNavigator.CalibrateText();
			if (this.source == documentXPathNavigator.source && this.namespaceParent == documentXPathNavigator.namespaceParent)
			{
				return XmlNodeOrder.Same;
			}
			if (this.namespaceParent != null || documentXPathNavigator.namespaceParent != null)
			{
				return base.ComparePosition(other);
			}
			XmlNode xmlNode = this.source;
			XmlNode xmlNode2 = documentXPathNavigator.source;
			XmlNode xmlNode3 = DocumentXPathNavigator.OwnerNode(xmlNode);
			XmlNode xmlNode4 = DocumentXPathNavigator.OwnerNode(xmlNode2);
			if (xmlNode3 != xmlNode4)
			{
				int num = DocumentXPathNavigator.GetDepth(xmlNode);
				int num2 = DocumentXPathNavigator.GetDepth(xmlNode2);
				if (num2 > num)
				{
					while (xmlNode2 != null && num2 > num)
					{
						xmlNode2 = DocumentXPathNavigator.OwnerNode(xmlNode2);
						num2--;
					}
					if (xmlNode == xmlNode2)
					{
						return XmlNodeOrder.Before;
					}
					xmlNode4 = DocumentXPathNavigator.OwnerNode(xmlNode2);
				}
				else if (num > num2)
				{
					while (xmlNode != null && num > num2)
					{
						xmlNode = DocumentXPathNavigator.OwnerNode(xmlNode);
						num--;
					}
					if (xmlNode == xmlNode2)
					{
						return XmlNodeOrder.After;
					}
					xmlNode3 = DocumentXPathNavigator.OwnerNode(xmlNode);
				}
				while (xmlNode3 != null && xmlNode4 != null)
				{
					if (xmlNode3 == xmlNode4)
					{
						return this.Compare(xmlNode, xmlNode2);
					}
					xmlNode = xmlNode3;
					xmlNode2 = xmlNode4;
					xmlNode3 = DocumentXPathNavigator.OwnerNode(xmlNode);
					xmlNode4 = DocumentXPathNavigator.OwnerNode(xmlNode2);
				}
				return XmlNodeOrder.Unknown;
			}
			if (xmlNode3 == null)
			{
				return XmlNodeOrder.Unknown;
			}
			return this.Compare(xmlNode, xmlNode2);
		}

		// Token: 0x06000F4C RID: 3916 RVA: 0x00064928 File Offset: 0x00062B28
		XmlNode IHasXmlNode.GetNode()
		{
			return this.source;
		}

		// Token: 0x06000F4D RID: 3917 RVA: 0x00064930 File Offset: 0x00062B30
		public override XPathNodeIterator SelectDescendants(string localName, string namespaceURI, bool matchSelf)
		{
			string text = this.document.NameTable.Get(namespaceURI);
			if (text == null || this.source.NodeType == XmlNodeType.Attribute)
			{
				return new DocumentXPathNodeIterator_Empty(this);
			}
			string text2 = this.document.NameTable.Get(localName);
			if (text2 == null)
			{
				return new DocumentXPathNodeIterator_Empty(this);
			}
			if (text2.Length == 0)
			{
				if (matchSelf)
				{
					return new DocumentXPathNodeIterator_ElemChildren_AndSelf_NoLocalName(this, text);
				}
				return new DocumentXPathNodeIterator_ElemChildren_NoLocalName(this, text);
			}
			else
			{
				if (matchSelf)
				{
					return new DocumentXPathNodeIterator_ElemChildren_AndSelf(this, text2, text);
				}
				return new DocumentXPathNodeIterator_ElemChildren(this, text2, text);
			}
		}

		// Token: 0x06000F4E RID: 3918 RVA: 0x000649B4 File Offset: 0x00062BB4
		public override XPathNodeIterator SelectDescendants(XPathNodeType nt, bool includeSelf)
		{
			if (nt != XPathNodeType.Element)
			{
				return base.SelectDescendants(nt, includeSelf);
			}
			XmlNodeType nodeType = this.source.NodeType;
			if (nodeType != XmlNodeType.Document && nodeType != XmlNodeType.Element)
			{
				return new DocumentXPathNodeIterator_Empty(this);
			}
			if (includeSelf)
			{
				return new DocumentXPathNodeIterator_AllElemChildren_AndSelf(this);
			}
			return new DocumentXPathNodeIterator_AllElemChildren(this);
		}

		// Token: 0x17000267 RID: 615
		// (get) Token: 0x06000F4F RID: 3919 RVA: 0x0001222F File Offset: 0x0001042F
		public override bool CanEdit
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000F50 RID: 3920 RVA: 0x000649FC File Offset: 0x00062BFC
		public override XmlWriter PrependChild()
		{
			XmlNodeType nodeType = this.source.NodeType;
			if (nodeType != XmlNodeType.Element && nodeType != XmlNodeType.Document && nodeType != XmlNodeType.DocumentFragment)
			{
				throw new InvalidOperationException(Res.GetString("Operation is not valid due to the current position of the navigator."));
			}
			DocumentXmlWriter documentXmlWriter = new DocumentXmlWriter(DocumentXmlWriterType.PrependChild, this.source, this.document);
			documentXmlWriter.NamespaceManager = DocumentXPathNavigator.GetNamespaceManager(this.source, this.document);
			return new XmlWellFormedWriter(documentXmlWriter, documentXmlWriter.Settings);
		}

		// Token: 0x06000F51 RID: 3921 RVA: 0x00064A6C File Offset: 0x00062C6C
		public override XmlWriter AppendChild()
		{
			XmlNodeType nodeType = this.source.NodeType;
			if (nodeType != XmlNodeType.Element && nodeType != XmlNodeType.Document && nodeType != XmlNodeType.DocumentFragment)
			{
				throw new InvalidOperationException(Res.GetString("Operation is not valid due to the current position of the navigator."));
			}
			DocumentXmlWriter documentXmlWriter = new DocumentXmlWriter(DocumentXmlWriterType.AppendChild, this.source, this.document);
			documentXmlWriter.NamespaceManager = DocumentXPathNavigator.GetNamespaceManager(this.source, this.document);
			return new XmlWellFormedWriter(documentXmlWriter, documentXmlWriter.Settings);
		}

		// Token: 0x06000F52 RID: 3922 RVA: 0x00064ADC File Offset: 0x00062CDC
		public override XmlWriter InsertAfter()
		{
			XmlNode xmlNode = this.source;
			switch (xmlNode.NodeType)
			{
			case XmlNodeType.Attribute:
			case XmlNodeType.Document:
			case XmlNodeType.DocumentFragment:
				throw new InvalidOperationException(Res.GetString("Operation is not valid due to the current position of the navigator."));
			case XmlNodeType.Text:
			case XmlNodeType.CDATA:
			case XmlNodeType.Whitespace:
			case XmlNodeType.SignificantWhitespace:
				xmlNode = this.TextEnd(xmlNode);
				break;
			}
			DocumentXmlWriter documentXmlWriter = new DocumentXmlWriter(DocumentXmlWriterType.InsertSiblingAfter, xmlNode, this.document);
			documentXmlWriter.NamespaceManager = DocumentXPathNavigator.GetNamespaceManager(xmlNode.ParentNode, this.document);
			return new XmlWellFormedWriter(documentXmlWriter, documentXmlWriter.Settings);
		}

		// Token: 0x06000F53 RID: 3923 RVA: 0x00064B80 File Offset: 0x00062D80
		public override XmlWriter InsertBefore()
		{
			switch (this.source.NodeType)
			{
			case XmlNodeType.Attribute:
			case XmlNodeType.Document:
			case XmlNodeType.DocumentFragment:
				throw new InvalidOperationException(Res.GetString("Operation is not valid due to the current position of the navigator."));
			case XmlNodeType.Text:
			case XmlNodeType.CDATA:
			case XmlNodeType.Whitespace:
			case XmlNodeType.SignificantWhitespace:
				this.CalibrateText();
				break;
			}
			DocumentXmlWriter documentXmlWriter = new DocumentXmlWriter(DocumentXmlWriterType.InsertSiblingBefore, this.source, this.document);
			documentXmlWriter.NamespaceManager = DocumentXPathNavigator.GetNamespaceManager(this.source.ParentNode, this.document);
			return new XmlWellFormedWriter(documentXmlWriter, documentXmlWriter.Settings);
		}

		// Token: 0x06000F54 RID: 3924 RVA: 0x00064C28 File Offset: 0x00062E28
		public override XmlWriter CreateAttributes()
		{
			if (this.source.NodeType != XmlNodeType.Element)
			{
				throw new InvalidOperationException(Res.GetString("Operation is not valid due to the current position of the navigator."));
			}
			DocumentXmlWriter documentXmlWriter = new DocumentXmlWriter(DocumentXmlWriterType.AppendAttribute, this.source, this.document);
			documentXmlWriter.NamespaceManager = DocumentXPathNavigator.GetNamespaceManager(this.source, this.document);
			return new XmlWellFormedWriter(documentXmlWriter, documentXmlWriter.Settings);
		}

		// Token: 0x06000F55 RID: 3925 RVA: 0x00064C8C File Offset: 0x00062E8C
		public override XmlWriter ReplaceRange(XPathNavigator lastSiblingToReplace)
		{
			DocumentXPathNavigator documentXPathNavigator = lastSiblingToReplace as DocumentXPathNavigator;
			if (documentXPathNavigator != null)
			{
				this.CalibrateText();
				documentXPathNavigator.CalibrateText();
				XmlNode xmlNode = this.source;
				XmlNode xmlNode2 = documentXPathNavigator.source;
				if (xmlNode == xmlNode2)
				{
					switch (xmlNode.NodeType)
					{
					case XmlNodeType.Attribute:
					case XmlNodeType.Document:
					case XmlNodeType.DocumentFragment:
						throw new InvalidOperationException(Res.GetString("Operation is not valid due to the current position of the navigator."));
					case XmlNodeType.Text:
					case XmlNodeType.CDATA:
					case XmlNodeType.Whitespace:
					case XmlNodeType.SignificantWhitespace:
						xmlNode2 = documentXPathNavigator.TextEnd(xmlNode2);
						break;
					}
				}
				else
				{
					if (xmlNode2.IsText)
					{
						xmlNode2 = documentXPathNavigator.TextEnd(xmlNode2);
					}
					if (!DocumentXPathNavigator.IsFollowingSibling(xmlNode, xmlNode2))
					{
						throw new InvalidOperationException(Res.GetString("Operation is not valid due to the current position of the navigator."));
					}
				}
				DocumentXmlWriter documentXmlWriter = new DocumentXmlWriter(DocumentXmlWriterType.ReplaceToFollowingSibling, xmlNode, this.document);
				documentXmlWriter.NamespaceManager = DocumentXPathNavigator.GetNamespaceManager(xmlNode.ParentNode, this.document);
				documentXmlWriter.Navigator = this;
				documentXmlWriter.EndNode = xmlNode2;
				return new XmlWellFormedWriter(documentXmlWriter, documentXmlWriter.Settings);
			}
			if (lastSiblingToReplace == null)
			{
				throw new ArgumentNullException("lastSiblingToReplace");
			}
			throw new NotSupportedException();
		}

		// Token: 0x06000F56 RID: 3926 RVA: 0x00064DA0 File Offset: 0x00062FA0
		public override void DeleteRange(XPathNavigator lastSiblingToDelete)
		{
			DocumentXPathNavigator documentXPathNavigator = lastSiblingToDelete as DocumentXPathNavigator;
			if (documentXPathNavigator != null)
			{
				this.CalibrateText();
				documentXPathNavigator.CalibrateText();
				XmlNode xmlNode = this.source;
				XmlNode xmlNode2 = documentXPathNavigator.source;
				if (xmlNode == xmlNode2)
				{
					XmlNode xmlNode3;
					switch (xmlNode.NodeType)
					{
					case XmlNodeType.Element:
					case XmlNodeType.ProcessingInstruction:
					case XmlNodeType.Comment:
						break;
					case XmlNodeType.Attribute:
					{
						XmlAttribute xmlAttribute = (XmlAttribute)xmlNode;
						if (xmlAttribute.IsNamespace)
						{
							goto IL_E1;
						}
						xmlNode3 = DocumentXPathNavigator.OwnerNode(xmlAttribute);
						DocumentXPathNavigator.DeleteAttribute(xmlAttribute, this.attributeIndex);
						if (xmlNode3 != null)
						{
							this.ResetPosition(xmlNode3);
							return;
						}
						return;
					}
					case XmlNodeType.Text:
					case XmlNodeType.CDATA:
					case XmlNodeType.Whitespace:
					case XmlNodeType.SignificantWhitespace:
						xmlNode2 = documentXPathNavigator.TextEnd(xmlNode2);
						break;
					case XmlNodeType.EntityReference:
					case XmlNodeType.Entity:
					case XmlNodeType.Document:
					case XmlNodeType.DocumentType:
					case XmlNodeType.DocumentFragment:
					case XmlNodeType.Notation:
						goto IL_E1;
					default:
						goto IL_E1;
					}
					xmlNode3 = DocumentXPathNavigator.OwnerNode(xmlNode);
					DocumentXPathNavigator.DeleteToFollowingSibling(xmlNode, xmlNode2);
					if (xmlNode3 != null)
					{
						this.ResetPosition(xmlNode3);
						return;
					}
					return;
					IL_E1:
					throw new InvalidOperationException(Res.GetString("Operation is not valid due to the current position of the navigator."));
				}
				if (xmlNode2.IsText)
				{
					xmlNode2 = documentXPathNavigator.TextEnd(xmlNode2);
				}
				if (!DocumentXPathNavigator.IsFollowingSibling(xmlNode, xmlNode2))
				{
					throw new InvalidOperationException(Res.GetString("Operation is not valid due to the current position of the navigator."));
				}
				XmlNode xmlNode4 = DocumentXPathNavigator.OwnerNode(xmlNode);
				DocumentXPathNavigator.DeleteToFollowingSibling(xmlNode, xmlNode2);
				if (xmlNode4 != null)
				{
					this.ResetPosition(xmlNode4);
				}
				return;
			}
			if (lastSiblingToDelete == null)
			{
				throw new ArgumentNullException("lastSiblingToDelete");
			}
			throw new NotSupportedException();
		}

		// Token: 0x06000F57 RID: 3927 RVA: 0x00064EE4 File Offset: 0x000630E4
		public override void DeleteSelf()
		{
			XmlNode xmlNode = this.source;
			XmlNode end = xmlNode;
			XmlNode xmlNode2;
			switch (xmlNode.NodeType)
			{
			case XmlNodeType.Element:
			case XmlNodeType.ProcessingInstruction:
			case XmlNodeType.Comment:
				break;
			case XmlNodeType.Attribute:
			{
				XmlAttribute xmlAttribute = (XmlAttribute)xmlNode;
				if (xmlAttribute.IsNamespace)
				{
					goto IL_AF;
				}
				xmlNode2 = DocumentXPathNavigator.OwnerNode(xmlAttribute);
				DocumentXPathNavigator.DeleteAttribute(xmlAttribute, this.attributeIndex);
				if (xmlNode2 != null)
				{
					this.ResetPosition(xmlNode2);
					return;
				}
				return;
			}
			case XmlNodeType.Text:
			case XmlNodeType.CDATA:
			case XmlNodeType.Whitespace:
			case XmlNodeType.SignificantWhitespace:
				this.CalibrateText();
				xmlNode = this.source;
				end = this.TextEnd(xmlNode);
				break;
			case XmlNodeType.EntityReference:
			case XmlNodeType.Entity:
			case XmlNodeType.Document:
			case XmlNodeType.DocumentType:
			case XmlNodeType.DocumentFragment:
			case XmlNodeType.Notation:
				goto IL_AF;
			default:
				goto IL_AF;
			}
			xmlNode2 = DocumentXPathNavigator.OwnerNode(xmlNode);
			DocumentXPathNavigator.DeleteToFollowingSibling(xmlNode, end);
			if (xmlNode2 != null)
			{
				this.ResetPosition(xmlNode2);
				return;
			}
			return;
			IL_AF:
			throw new InvalidOperationException(Res.GetString("Operation is not valid due to the current position of the navigator."));
		}

		// Token: 0x06000F58 RID: 3928 RVA: 0x00064FB0 File Offset: 0x000631B0
		private static void DeleteAttribute(XmlAttribute attribute, int index)
		{
			XmlAttributeCollection xmlAttributeCollection;
			if (!DocumentXPathNavigator.CheckAttributePosition(attribute, out xmlAttributeCollection, index) && !DocumentXPathNavigator.ResetAttributePosition(attribute, xmlAttributeCollection, out index))
			{
				throw new InvalidOperationException(Res.GetString("The current position of the navigator is missing a valid parent."));
			}
			if (attribute.IsReadOnly)
			{
				throw new InvalidOperationException(Res.GetString("This node is read-only. It cannot be modified."));
			}
			xmlAttributeCollection.RemoveAt(index);
		}

		// Token: 0x06000F59 RID: 3929 RVA: 0x00065004 File Offset: 0x00063204
		internal static void DeleteToFollowingSibling(XmlNode node, XmlNode end)
		{
			XmlNode parentNode = node.ParentNode;
			if (parentNode == null)
			{
				throw new InvalidOperationException(Res.GetString("The current position of the navigator is missing a valid parent."));
			}
			if (node.IsReadOnly || end.IsReadOnly)
			{
				throw new InvalidOperationException(Res.GetString("This node is read-only. It cannot be modified."));
			}
			while (node != end)
			{
				XmlNode oldChild = node;
				node = node.NextSibling;
				parentNode.RemoveChild(oldChild);
			}
			parentNode.RemoveChild(node);
		}

		// Token: 0x06000F5A RID: 3930 RVA: 0x0006506C File Offset: 0x0006326C
		private static XmlNamespaceManager GetNamespaceManager(XmlNode node, XmlDocument document)
		{
			XmlNamespaceManager xmlNamespaceManager = new XmlNamespaceManager(document.NameTable);
			List<XmlElement> list = new List<XmlElement>();
			while (node != null)
			{
				XmlElement xmlElement = node as XmlElement;
				if (xmlElement != null && xmlElement.HasAttributes)
				{
					list.Add(xmlElement);
				}
				node = node.ParentNode;
			}
			for (int i = list.Count - 1; i >= 0; i--)
			{
				xmlNamespaceManager.PushScope();
				XmlAttributeCollection attributes = list[i].Attributes;
				for (int j = 0; j < attributes.Count; j++)
				{
					XmlAttribute xmlAttribute = attributes[j];
					if (xmlAttribute.IsNamespace)
					{
						string prefix = (xmlAttribute.Prefix.Length == 0) ? string.Empty : xmlAttribute.LocalName;
						xmlNamespaceManager.AddNamespace(prefix, xmlAttribute.Value);
					}
				}
			}
			return xmlNamespaceManager;
		}

		// Token: 0x06000F5B RID: 3931 RVA: 0x00065130 File Offset: 0x00063330
		internal void ResetPosition(XmlNode node)
		{
			this.source = node;
			XmlAttribute xmlAttribute = node as XmlAttribute;
			if (xmlAttribute != null)
			{
				XmlElement ownerElement = xmlAttribute.OwnerElement;
				if (ownerElement != null)
				{
					DocumentXPathNavigator.ResetAttributePosition(xmlAttribute, ownerElement.Attributes, out this.attributeIndex);
					if (xmlAttribute.IsNamespace)
					{
						this.namespaceParent = ownerElement;
					}
				}
			}
		}

		// Token: 0x06000F5C RID: 3932 RVA: 0x0006517C File Offset: 0x0006337C
		private static bool ResetAttributePosition(XmlAttribute attribute, XmlAttributeCollection attributes, out int index)
		{
			if (attributes != null)
			{
				for (int i = 0; i < attributes.Count; i++)
				{
					if (attribute == attributes[i])
					{
						index = i;
						return true;
					}
				}
			}
			index = 0;
			return false;
		}

		// Token: 0x06000F5D RID: 3933 RVA: 0x000651B0 File Offset: 0x000633B0
		private static bool CheckAttributePosition(XmlAttribute attribute, out XmlAttributeCollection attributes, int index)
		{
			XmlElement ownerElement = attribute.OwnerElement;
			if (ownerElement != null)
			{
				attributes = ownerElement.Attributes;
				if (index >= 0 && index < attributes.Count && attribute == attributes[index])
				{
					return true;
				}
			}
			else
			{
				attributes = null;
			}
			return false;
		}

		// Token: 0x06000F5E RID: 3934 RVA: 0x000651F0 File Offset: 0x000633F0
		private void CalibrateText()
		{
			for (XmlNode node = this.PreviousText(this.source); node != null; node = this.PreviousText(node))
			{
				this.ResetPosition(node);
			}
		}

		// Token: 0x06000F5F RID: 3935 RVA: 0x00065220 File Offset: 0x00063420
		private XmlNode ParentNode(XmlNode node)
		{
			XmlNode parentNode = node.ParentNode;
			if (!this.document.HasEntityReferences)
			{
				return parentNode;
			}
			return this.ParentNodeTail(parentNode);
		}

		// Token: 0x06000F60 RID: 3936 RVA: 0x0006524A File Offset: 0x0006344A
		private XmlNode ParentNodeTail(XmlNode parent)
		{
			while (parent != null && parent.NodeType == XmlNodeType.EntityReference)
			{
				parent = parent.ParentNode;
			}
			return parent;
		}

		// Token: 0x06000F61 RID: 3937 RVA: 0x00065264 File Offset: 0x00063464
		private XmlNode FirstChild(XmlNode node)
		{
			XmlNode firstChild = node.FirstChild;
			if (!this.document.HasEntityReferences)
			{
				return firstChild;
			}
			return this.FirstChildTail(firstChild);
		}

		// Token: 0x06000F62 RID: 3938 RVA: 0x0006528E File Offset: 0x0006348E
		private XmlNode FirstChildTail(XmlNode child)
		{
			while (child != null && child.NodeType == XmlNodeType.EntityReference)
			{
				child = child.FirstChild;
			}
			return child;
		}

		// Token: 0x06000F63 RID: 3939 RVA: 0x000652A8 File Offset: 0x000634A8
		private XmlNode NextSibling(XmlNode node)
		{
			XmlNode nextSibling = node.NextSibling;
			if (!this.document.HasEntityReferences)
			{
				return nextSibling;
			}
			return this.NextSiblingTail(node, nextSibling);
		}

		// Token: 0x06000F64 RID: 3940 RVA: 0x000652D3 File Offset: 0x000634D3
		private XmlNode NextSiblingTail(XmlNode node, XmlNode sibling)
		{
			while (sibling == null)
			{
				node = node.ParentNode;
				if (node == null || node.NodeType != XmlNodeType.EntityReference)
				{
					return null;
				}
				sibling = node.NextSibling;
			}
			while (sibling != null && sibling.NodeType == XmlNodeType.EntityReference)
			{
				sibling = sibling.FirstChild;
			}
			return sibling;
		}

		// Token: 0x06000F65 RID: 3941 RVA: 0x00065310 File Offset: 0x00063510
		private XmlNode PreviousSibling(XmlNode node)
		{
			XmlNode previousSibling = node.PreviousSibling;
			if (!this.document.HasEntityReferences)
			{
				return previousSibling;
			}
			return this.PreviousSiblingTail(node, previousSibling);
		}

		// Token: 0x06000F66 RID: 3942 RVA: 0x0006533B File Offset: 0x0006353B
		private XmlNode PreviousSiblingTail(XmlNode node, XmlNode sibling)
		{
			while (sibling == null)
			{
				node = node.ParentNode;
				if (node == null || node.NodeType != XmlNodeType.EntityReference)
				{
					return null;
				}
				sibling = node.PreviousSibling;
			}
			while (sibling != null && sibling.NodeType == XmlNodeType.EntityReference)
			{
				sibling = sibling.LastChild;
			}
			return sibling;
		}

		// Token: 0x06000F67 RID: 3943 RVA: 0x00065378 File Offset: 0x00063578
		private XmlNode PreviousText(XmlNode node)
		{
			XmlNode previousText = node.PreviousText;
			if (!this.document.HasEntityReferences)
			{
				return previousText;
			}
			return this.PreviousTextTail(node, previousText);
		}

		// Token: 0x06000F68 RID: 3944 RVA: 0x000653A4 File Offset: 0x000635A4
		private XmlNode PreviousTextTail(XmlNode node, XmlNode text)
		{
			if (text != null)
			{
				return text;
			}
			if (!node.IsText)
			{
				return null;
			}
			XmlNode xmlNode;
			for (xmlNode = node.PreviousSibling; xmlNode == null; xmlNode = node.PreviousSibling)
			{
				node = node.ParentNode;
				if (node == null || node.NodeType != XmlNodeType.EntityReference)
				{
					return null;
				}
			}
			while (xmlNode != null)
			{
				XmlNodeType nodeType = xmlNode.NodeType;
				if (nodeType - XmlNodeType.Text > 1)
				{
					if (nodeType == XmlNodeType.EntityReference)
					{
						xmlNode = xmlNode.LastChild;
						continue;
					}
					if (nodeType - XmlNodeType.Whitespace > 1)
					{
						return null;
					}
				}
				return xmlNode;
			}
			return null;
		}

		// Token: 0x06000F69 RID: 3945 RVA: 0x00065416 File Offset: 0x00063616
		internal static bool IsFollowingSibling(XmlNode left, XmlNode right)
		{
			do
			{
				left = left.NextSibling;
				if (left == null)
				{
					return false;
				}
			}
			while (left != right);
			return true;
		}

		// Token: 0x06000F6A RID: 3946 RVA: 0x0006542C File Offset: 0x0006362C
		private static bool IsDescendant(XmlNode top, XmlNode bottom)
		{
			do
			{
				XmlNode xmlNode = bottom.ParentNode;
				if (xmlNode == null)
				{
					XmlAttribute xmlAttribute = bottom as XmlAttribute;
					if (xmlAttribute == null)
					{
						return false;
					}
					xmlNode = xmlAttribute.OwnerElement;
					if (xmlNode == null)
					{
						return false;
					}
				}
				bottom = xmlNode;
			}
			while (top != bottom);
			return true;
		}

		// Token: 0x06000F6B RID: 3947 RVA: 0x00065464 File Offset: 0x00063664
		private static bool IsValidChild(XmlNode parent, XmlNode child)
		{
			XmlNodeType nodeType = parent.NodeType;
			if (nodeType != XmlNodeType.Element)
			{
				if (nodeType != XmlNodeType.Document)
				{
					if (nodeType == XmlNodeType.DocumentFragment)
					{
						XmlNodeType nodeType2 = child.NodeType;
						switch (nodeType2)
						{
						case XmlNodeType.Element:
						case XmlNodeType.Text:
						case XmlNodeType.CDATA:
						case XmlNodeType.ProcessingInstruction:
						case XmlNodeType.Comment:
							break;
						case XmlNodeType.Attribute:
						case XmlNodeType.EntityReference:
						case XmlNodeType.Entity:
							return false;
						default:
							if (nodeType2 - XmlNodeType.Whitespace > 1)
							{
								return false;
							}
							break;
						}
						return true;
					}
				}
				else
				{
					XmlNodeType nodeType2 = child.NodeType;
					if (nodeType2 == XmlNodeType.Element || nodeType2 - XmlNodeType.ProcessingInstruction <= 1)
					{
						return true;
					}
				}
				return false;
			}
			return true;
		}

		// Token: 0x06000F6C RID: 3948 RVA: 0x000654D8 File Offset: 0x000636D8
		private XmlNode TextStart(XmlNode node)
		{
			XmlNode result;
			do
			{
				result = node;
				node = this.PreviousSibling(node);
			}
			while (node != null && node.IsText);
			return result;
		}

		// Token: 0x06000F6D RID: 3949 RVA: 0x000654FC File Offset: 0x000636FC
		private XmlNode TextEnd(XmlNode node)
		{
			XmlNode result;
			do
			{
				result = node;
				node = this.NextSibling(node);
			}
			while (node != null && node.IsText);
			return result;
		}

		// Token: 0x04000FF7 RID: 4087
		private XmlDocument document;

		// Token: 0x04000FF8 RID: 4088
		private XmlNode source;

		// Token: 0x04000FF9 RID: 4089
		private int attributeIndex;

		// Token: 0x04000FFA RID: 4090
		private XmlElement namespaceParent;
	}
}

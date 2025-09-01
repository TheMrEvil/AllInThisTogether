using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Schema;

namespace System.Xml
{
	// Token: 0x020001D5 RID: 469
	internal class XmlNodeReaderNavigator
	{
		// Token: 0x0600124A RID: 4682 RVA: 0x0006E520 File Offset: 0x0006C720
		public XmlNodeReaderNavigator(XmlNode node)
		{
			this.curNode = node;
			this.logNode = node;
			XmlNodeType nodeType = this.curNode.NodeType;
			if (nodeType == XmlNodeType.Attribute)
			{
				this.elemNode = null;
				this.attrIndex = -1;
				this.bCreatedOnAttribute = true;
			}
			else
			{
				this.elemNode = node;
				this.attrIndex = -1;
				this.bCreatedOnAttribute = false;
			}
			if (nodeType == XmlNodeType.Document)
			{
				this.doc = (XmlDocument)this.curNode;
			}
			else
			{
				this.doc = node.OwnerDocument;
			}
			this.nameTable = this.doc.NameTable;
			this.nAttrInd = -1;
			this.nDeclarationAttrCount = -1;
			this.nDocTypeAttrCount = -1;
			this.bOnAttrVal = false;
			this.bLogOnAttrVal = false;
		}

		// Token: 0x17000351 RID: 849
		// (get) Token: 0x0600124B RID: 4683 RVA: 0x0006E634 File Offset: 0x0006C834
		public XmlNodeType NodeType
		{
			get
			{
				XmlNodeType nodeType = this.curNode.NodeType;
				if (this.nAttrInd == -1)
				{
					return nodeType;
				}
				if (this.bOnAttrVal)
				{
					return XmlNodeType.Text;
				}
				return XmlNodeType.Attribute;
			}
		}

		// Token: 0x17000352 RID: 850
		// (get) Token: 0x0600124C RID: 4684 RVA: 0x0006E663 File Offset: 0x0006C863
		public string NamespaceURI
		{
			get
			{
				return this.curNode.NamespaceURI;
			}
		}

		// Token: 0x17000353 RID: 851
		// (get) Token: 0x0600124D RID: 4685 RVA: 0x0006E670 File Offset: 0x0006C870
		public string Name
		{
			get
			{
				if (this.nAttrInd != -1)
				{
					if (this.bOnAttrVal)
					{
						return string.Empty;
					}
					if (this.curNode.NodeType == XmlNodeType.XmlDeclaration)
					{
						return this.decNodeAttributes[this.nAttrInd].name;
					}
					return this.docTypeNodeAttributes[this.nAttrInd].name;
				}
				else
				{
					if (this.IsLocalNameEmpty(this.curNode.NodeType))
					{
						return string.Empty;
					}
					return this.curNode.Name;
				}
			}
		}

		// Token: 0x17000354 RID: 852
		// (get) Token: 0x0600124E RID: 4686 RVA: 0x0006E6F5 File Offset: 0x0006C8F5
		public string LocalName
		{
			get
			{
				if (this.nAttrInd != -1)
				{
					return this.Name;
				}
				if (this.IsLocalNameEmpty(this.curNode.NodeType))
				{
					return string.Empty;
				}
				return this.curNode.LocalName;
			}
		}

		// Token: 0x17000355 RID: 853
		// (get) Token: 0x0600124F RID: 4687 RVA: 0x0006E72B File Offset: 0x0006C92B
		internal bool IsOnAttrVal
		{
			get
			{
				return this.bOnAttrVal;
			}
		}

		// Token: 0x17000356 RID: 854
		// (get) Token: 0x06001250 RID: 4688 RVA: 0x0006E733 File Offset: 0x0006C933
		internal XmlNode OwnerElementNode
		{
			get
			{
				if (this.bCreatedOnAttribute)
				{
					return null;
				}
				return this.elemNode;
			}
		}

		// Token: 0x17000357 RID: 855
		// (get) Token: 0x06001251 RID: 4689 RVA: 0x0006E745 File Offset: 0x0006C945
		internal bool CreatedOnAttribute
		{
			get
			{
				return this.bCreatedOnAttribute;
			}
		}

		// Token: 0x06001252 RID: 4690 RVA: 0x0006E750 File Offset: 0x0006C950
		private bool IsLocalNameEmpty(XmlNodeType nt)
		{
			switch (nt)
			{
			case XmlNodeType.None:
			case XmlNodeType.Text:
			case XmlNodeType.CDATA:
			case XmlNodeType.Comment:
			case XmlNodeType.Document:
			case XmlNodeType.DocumentFragment:
			case XmlNodeType.Whitespace:
			case XmlNodeType.SignificantWhitespace:
			case XmlNodeType.EndElement:
			case XmlNodeType.EndEntity:
				return true;
			case XmlNodeType.Element:
			case XmlNodeType.Attribute:
			case XmlNodeType.EntityReference:
			case XmlNodeType.Entity:
			case XmlNodeType.ProcessingInstruction:
			case XmlNodeType.DocumentType:
			case XmlNodeType.Notation:
			case XmlNodeType.XmlDeclaration:
				return false;
			default:
				return true;
			}
		}

		// Token: 0x17000358 RID: 856
		// (get) Token: 0x06001253 RID: 4691 RVA: 0x0006E7B2 File Offset: 0x0006C9B2
		public string Prefix
		{
			get
			{
				return this.curNode.Prefix;
			}
		}

		// Token: 0x17000359 RID: 857
		// (get) Token: 0x06001254 RID: 4692 RVA: 0x0006E7BF File Offset: 0x0006C9BF
		public bool HasValue
		{
			get
			{
				return this.nAttrInd != -1 || (this.curNode.Value != null || this.curNode.NodeType == XmlNodeType.DocumentType);
			}
		}

		// Token: 0x1700035A RID: 858
		// (get) Token: 0x06001255 RID: 4693 RVA: 0x0006E7EC File Offset: 0x0006C9EC
		public string Value
		{
			get
			{
				XmlNodeType nodeType = this.curNode.NodeType;
				if (this.nAttrInd != -1)
				{
					if (this.curNode.NodeType == XmlNodeType.XmlDeclaration)
					{
						return this.decNodeAttributes[this.nAttrInd].value;
					}
					return this.docTypeNodeAttributes[this.nAttrInd].value;
				}
				else
				{
					string text;
					if (nodeType == XmlNodeType.DocumentType)
					{
						text = ((XmlDocumentType)this.curNode).InternalSubset;
					}
					else if (nodeType == XmlNodeType.XmlDeclaration)
					{
						StringBuilder stringBuilder = new StringBuilder(string.Empty);
						if (this.nDeclarationAttrCount == -1)
						{
							this.InitDecAttr();
						}
						for (int i = 0; i < this.nDeclarationAttrCount; i++)
						{
							stringBuilder.Append(this.decNodeAttributes[i].name + "=\"" + this.decNodeAttributes[i].value + "\"");
							if (i != this.nDeclarationAttrCount - 1)
							{
								stringBuilder.Append(" ");
							}
						}
						text = stringBuilder.ToString();
					}
					else
					{
						text = this.curNode.Value;
					}
					if (text != null)
					{
						return text;
					}
					return string.Empty;
				}
			}
		}

		// Token: 0x1700035B RID: 859
		// (get) Token: 0x06001256 RID: 4694 RVA: 0x0006E909 File Offset: 0x0006CB09
		public string BaseURI
		{
			get
			{
				return this.curNode.BaseURI;
			}
		}

		// Token: 0x1700035C RID: 860
		// (get) Token: 0x06001257 RID: 4695 RVA: 0x0006E916 File Offset: 0x0006CB16
		public XmlSpace XmlSpace
		{
			get
			{
				return this.curNode.XmlSpace;
			}
		}

		// Token: 0x1700035D RID: 861
		// (get) Token: 0x06001258 RID: 4696 RVA: 0x0006E923 File Offset: 0x0006CB23
		public string XmlLang
		{
			get
			{
				return this.curNode.XmlLang;
			}
		}

		// Token: 0x1700035E RID: 862
		// (get) Token: 0x06001259 RID: 4697 RVA: 0x0006E930 File Offset: 0x0006CB30
		public bool IsEmptyElement
		{
			get
			{
				return this.curNode.NodeType == XmlNodeType.Element && ((XmlElement)this.curNode).IsEmpty;
			}
		}

		// Token: 0x1700035F RID: 863
		// (get) Token: 0x0600125A RID: 4698 RVA: 0x0006E952 File Offset: 0x0006CB52
		public bool IsDefault
		{
			get
			{
				return this.curNode.NodeType == XmlNodeType.Attribute && !((XmlAttribute)this.curNode).Specified;
			}
		}

		// Token: 0x17000360 RID: 864
		// (get) Token: 0x0600125B RID: 4699 RVA: 0x0006E977 File Offset: 0x0006CB77
		public IXmlSchemaInfo SchemaInfo
		{
			get
			{
				return this.curNode.SchemaInfo;
			}
		}

		// Token: 0x17000361 RID: 865
		// (get) Token: 0x0600125C RID: 4700 RVA: 0x0006E984 File Offset: 0x0006CB84
		public XmlNameTable NameTable
		{
			get
			{
				return this.nameTable;
			}
		}

		// Token: 0x17000362 RID: 866
		// (get) Token: 0x0600125D RID: 4701 RVA: 0x0006E98C File Offset: 0x0006CB8C
		public int AttributeCount
		{
			get
			{
				if (this.bCreatedOnAttribute)
				{
					return 0;
				}
				XmlNodeType nodeType = this.curNode.NodeType;
				if (nodeType == XmlNodeType.Element)
				{
					return ((XmlElement)this.curNode).Attributes.Count;
				}
				if (nodeType == XmlNodeType.Attribute || (this.bOnAttrVal && nodeType != XmlNodeType.XmlDeclaration && nodeType != XmlNodeType.DocumentType))
				{
					return this.elemNode.Attributes.Count;
				}
				if (nodeType == XmlNodeType.XmlDeclaration)
				{
					if (this.nDeclarationAttrCount != -1)
					{
						return this.nDeclarationAttrCount;
					}
					this.InitDecAttr();
					return this.nDeclarationAttrCount;
				}
				else
				{
					if (nodeType != XmlNodeType.DocumentType)
					{
						return 0;
					}
					if (this.nDocTypeAttrCount != -1)
					{
						return this.nDocTypeAttrCount;
					}
					this.InitDocTypeAttr();
					return this.nDocTypeAttrCount;
				}
			}
		}

		// Token: 0x0600125E RID: 4702 RVA: 0x0006EA35 File Offset: 0x0006CC35
		private void CheckIndexCondition(int attributeIndex)
		{
			if (attributeIndex < 0 || attributeIndex >= this.AttributeCount)
			{
				throw new ArgumentOutOfRangeException("attributeIndex");
			}
		}

		// Token: 0x0600125F RID: 4703 RVA: 0x0006EA50 File Offset: 0x0006CC50
		private void InitDecAttr()
		{
			int num = 0;
			string text = this.doc.Version;
			if (text != null && text.Length != 0)
			{
				this.decNodeAttributes[num].name = "version";
				this.decNodeAttributes[num].value = text;
				num++;
			}
			text = this.doc.Encoding;
			if (text != null && text.Length != 0)
			{
				this.decNodeAttributes[num].name = "encoding";
				this.decNodeAttributes[num].value = text;
				num++;
			}
			text = this.doc.Standalone;
			if (text != null && text.Length != 0)
			{
				this.decNodeAttributes[num].name = "standalone";
				this.decNodeAttributes[num].value = text;
				num++;
			}
			this.nDeclarationAttrCount = num;
		}

		// Token: 0x06001260 RID: 4704 RVA: 0x0006EB2F File Offset: 0x0006CD2F
		public string GetDeclarationAttr(XmlDeclaration decl, string name)
		{
			if (name == "version")
			{
				return decl.Version;
			}
			if (name == "encoding")
			{
				return decl.Encoding;
			}
			if (name == "standalone")
			{
				return decl.Standalone;
			}
			return null;
		}

		// Token: 0x06001261 RID: 4705 RVA: 0x0006EB6E File Offset: 0x0006CD6E
		public string GetDeclarationAttr(int i)
		{
			if (this.nDeclarationAttrCount == -1)
			{
				this.InitDecAttr();
			}
			return this.decNodeAttributes[i].value;
		}

		// Token: 0x06001262 RID: 4706 RVA: 0x0006EB90 File Offset: 0x0006CD90
		public int GetDecAttrInd(string name)
		{
			if (this.nDeclarationAttrCount == -1)
			{
				this.InitDecAttr();
			}
			for (int i = 0; i < this.nDeclarationAttrCount; i++)
			{
				if (this.decNodeAttributes[i].name == name)
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x06001263 RID: 4707 RVA: 0x0006EBDC File Offset: 0x0006CDDC
		private void InitDocTypeAttr()
		{
			int num = 0;
			XmlDocumentType documentType = this.doc.DocumentType;
			if (documentType == null)
			{
				this.nDocTypeAttrCount = 0;
				return;
			}
			string text = documentType.PublicId;
			if (text != null)
			{
				this.docTypeNodeAttributes[num].name = "PUBLIC";
				this.docTypeNodeAttributes[num].value = text;
				num++;
			}
			text = documentType.SystemId;
			if (text != null)
			{
				this.docTypeNodeAttributes[num].name = "SYSTEM";
				this.docTypeNodeAttributes[num].value = text;
				num++;
			}
			this.nDocTypeAttrCount = num;
		}

		// Token: 0x06001264 RID: 4708 RVA: 0x0006EC75 File Offset: 0x0006CE75
		public string GetDocumentTypeAttr(XmlDocumentType docType, string name)
		{
			if (name == "PUBLIC")
			{
				return docType.PublicId;
			}
			if (name == "SYSTEM")
			{
				return docType.SystemId;
			}
			return null;
		}

		// Token: 0x06001265 RID: 4709 RVA: 0x0006ECA0 File Offset: 0x0006CEA0
		public string GetDocumentTypeAttr(int i)
		{
			if (this.nDocTypeAttrCount == -1)
			{
				this.InitDocTypeAttr();
			}
			return this.docTypeNodeAttributes[i].value;
		}

		// Token: 0x06001266 RID: 4710 RVA: 0x0006ECC4 File Offset: 0x0006CEC4
		public int GetDocTypeAttrInd(string name)
		{
			if (this.nDocTypeAttrCount == -1)
			{
				this.InitDocTypeAttr();
			}
			for (int i = 0; i < this.nDocTypeAttrCount; i++)
			{
				if (this.docTypeNodeAttributes[i].name == name)
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x06001267 RID: 4711 RVA: 0x0006ED10 File Offset: 0x0006CF10
		private string GetAttributeFromElement(XmlElement elem, string name)
		{
			XmlAttribute attributeNode = elem.GetAttributeNode(name);
			if (attributeNode != null)
			{
				return attributeNode.Value;
			}
			return null;
		}

		// Token: 0x06001268 RID: 4712 RVA: 0x0006ED30 File Offset: 0x0006CF30
		public string GetAttribute(string name)
		{
			if (this.bCreatedOnAttribute)
			{
				return null;
			}
			XmlNodeType nodeType = this.curNode.NodeType;
			if (nodeType <= XmlNodeType.Attribute)
			{
				if (nodeType == XmlNodeType.Element)
				{
					return this.GetAttributeFromElement((XmlElement)this.curNode, name);
				}
				if (nodeType == XmlNodeType.Attribute)
				{
					return this.GetAttributeFromElement((XmlElement)this.elemNode, name);
				}
			}
			else
			{
				if (nodeType == XmlNodeType.DocumentType)
				{
					return this.GetDocumentTypeAttr((XmlDocumentType)this.curNode, name);
				}
				if (nodeType == XmlNodeType.XmlDeclaration)
				{
					return this.GetDeclarationAttr((XmlDeclaration)this.curNode, name);
				}
			}
			return null;
		}

		// Token: 0x06001269 RID: 4713 RVA: 0x0006EDBC File Offset: 0x0006CFBC
		private string GetAttributeFromElement(XmlElement elem, string name, string ns)
		{
			XmlAttribute attributeNode = elem.GetAttributeNode(name, ns);
			if (attributeNode != null)
			{
				return attributeNode.Value;
			}
			return null;
		}

		// Token: 0x0600126A RID: 4714 RVA: 0x0006EDE0 File Offset: 0x0006CFE0
		public string GetAttribute(string name, string ns)
		{
			if (this.bCreatedOnAttribute)
			{
				return null;
			}
			XmlNodeType nodeType = this.curNode.NodeType;
			if (nodeType <= XmlNodeType.Attribute)
			{
				if (nodeType == XmlNodeType.Element)
				{
					return this.GetAttributeFromElement((XmlElement)this.curNode, name, ns);
				}
				if (nodeType == XmlNodeType.Attribute)
				{
					return this.GetAttributeFromElement((XmlElement)this.elemNode, name, ns);
				}
			}
			else if (nodeType != XmlNodeType.DocumentType)
			{
				if (nodeType == XmlNodeType.XmlDeclaration)
				{
					if (ns.Length != 0)
					{
						return null;
					}
					return this.GetDeclarationAttr((XmlDeclaration)this.curNode, name);
				}
			}
			else
			{
				if (ns.Length != 0)
				{
					return null;
				}
				return this.GetDocumentTypeAttr((XmlDocumentType)this.curNode, name);
			}
			return null;
		}

		// Token: 0x0600126B RID: 4715 RVA: 0x0006EE80 File Offset: 0x0006D080
		public string GetAttribute(int attributeIndex)
		{
			if (this.bCreatedOnAttribute)
			{
				return null;
			}
			XmlNodeType nodeType = this.curNode.NodeType;
			if (nodeType <= XmlNodeType.Attribute)
			{
				if (nodeType == XmlNodeType.Element)
				{
					this.CheckIndexCondition(attributeIndex);
					return ((XmlElement)this.curNode).Attributes[attributeIndex].Value;
				}
				if (nodeType == XmlNodeType.Attribute)
				{
					this.CheckIndexCondition(attributeIndex);
					return ((XmlElement)this.elemNode).Attributes[attributeIndex].Value;
				}
			}
			else
			{
				if (nodeType == XmlNodeType.DocumentType)
				{
					this.CheckIndexCondition(attributeIndex);
					return this.GetDocumentTypeAttr(attributeIndex);
				}
				if (nodeType == XmlNodeType.XmlDeclaration)
				{
					this.CheckIndexCondition(attributeIndex);
					return this.GetDeclarationAttr(attributeIndex);
				}
			}
			throw new ArgumentOutOfRangeException("attributeIndex");
		}

		// Token: 0x0600126C RID: 4716 RVA: 0x0006EF2B File Offset: 0x0006D12B
		public void LogMove(int level)
		{
			this.logNode = this.curNode;
			this.nLogLevel = level;
			this.nLogAttrInd = this.nAttrInd;
			this.logAttrIndex = this.attrIndex;
			this.bLogOnAttrVal = this.bOnAttrVal;
		}

		// Token: 0x0600126D RID: 4717 RVA: 0x0006EF64 File Offset: 0x0006D164
		public void RollBackMove(ref int level)
		{
			this.curNode = this.logNode;
			level = this.nLogLevel;
			this.nAttrInd = this.nLogAttrInd;
			this.attrIndex = this.logAttrIndex;
			this.bOnAttrVal = this.bLogOnAttrVal;
		}

		// Token: 0x17000363 RID: 867
		// (get) Token: 0x0600126E RID: 4718 RVA: 0x0006EFA0 File Offset: 0x0006D1A0
		private bool IsOnDeclOrDocType
		{
			get
			{
				XmlNodeType nodeType = this.curNode.NodeType;
				return nodeType == XmlNodeType.XmlDeclaration || nodeType == XmlNodeType.DocumentType;
			}
		}

		// Token: 0x0600126F RID: 4719 RVA: 0x0006EFC8 File Offset: 0x0006D1C8
		public void ResetToAttribute(ref int level)
		{
			if (this.bCreatedOnAttribute)
			{
				return;
			}
			if (this.bOnAttrVal)
			{
				if (this.IsOnDeclOrDocType)
				{
					level -= 2;
				}
				else
				{
					while (this.curNode.NodeType != XmlNodeType.Attribute && (this.curNode = this.curNode.ParentNode) != null)
					{
						level--;
					}
				}
				this.bOnAttrVal = false;
			}
		}

		// Token: 0x06001270 RID: 4720 RVA: 0x0006F028 File Offset: 0x0006D228
		public void ResetMove(ref int level, ref XmlNodeType nt)
		{
			this.LogMove(level);
			if (this.bCreatedOnAttribute)
			{
				return;
			}
			if (this.nAttrInd != -1)
			{
				if (this.bOnAttrVal)
				{
					level--;
					this.bOnAttrVal = false;
				}
				this.nLogAttrInd = this.nAttrInd;
				level--;
				this.nAttrInd = -1;
				nt = this.curNode.NodeType;
				return;
			}
			if (this.bOnAttrVal && this.curNode.NodeType != XmlNodeType.Attribute)
			{
				this.ResetToAttribute(ref level);
			}
			if (this.curNode.NodeType == XmlNodeType.Attribute)
			{
				this.curNode = ((XmlAttribute)this.curNode).OwnerElement;
				this.attrIndex = -1;
				level--;
				nt = XmlNodeType.Element;
			}
			if (this.curNode.NodeType == XmlNodeType.Element)
			{
				this.elemNode = this.curNode;
			}
		}

		// Token: 0x06001271 RID: 4721 RVA: 0x0006F0F6 File Offset: 0x0006D2F6
		public bool MoveToAttribute(string name)
		{
			return this.MoveToAttribute(name, string.Empty);
		}

		// Token: 0x06001272 RID: 4722 RVA: 0x0006F104 File Offset: 0x0006D304
		private bool MoveToAttributeFromElement(XmlElement elem, string name, string ns)
		{
			XmlAttribute attributeNode;
			if (ns.Length == 0)
			{
				attributeNode = elem.GetAttributeNode(name);
			}
			else
			{
				attributeNode = elem.GetAttributeNode(name, ns);
			}
			if (attributeNode != null)
			{
				this.bOnAttrVal = false;
				this.elemNode = elem;
				this.curNode = attributeNode;
				this.attrIndex = elem.Attributes.FindNodeOffsetNS(attributeNode);
				if (this.attrIndex != -1)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06001273 RID: 4723 RVA: 0x0006F164 File Offset: 0x0006D364
		public bool MoveToAttribute(string name, string namespaceURI)
		{
			if (this.bCreatedOnAttribute)
			{
				return false;
			}
			XmlNodeType nodeType = this.curNode.NodeType;
			if (nodeType == XmlNodeType.Element)
			{
				return this.MoveToAttributeFromElement((XmlElement)this.curNode, name, namespaceURI);
			}
			if (nodeType == XmlNodeType.Attribute)
			{
				return this.MoveToAttributeFromElement((XmlElement)this.elemNode, name, namespaceURI);
			}
			if (nodeType == XmlNodeType.XmlDeclaration && namespaceURI.Length == 0)
			{
				if ((this.nAttrInd = this.GetDecAttrInd(name)) != -1)
				{
					this.bOnAttrVal = false;
					return true;
				}
			}
			else if (nodeType == XmlNodeType.DocumentType && namespaceURI.Length == 0 && (this.nAttrInd = this.GetDocTypeAttrInd(name)) != -1)
			{
				this.bOnAttrVal = false;
				return true;
			}
			return false;
		}

		// Token: 0x06001274 RID: 4724 RVA: 0x0006F20C File Offset: 0x0006D40C
		public void MoveToAttribute(int attributeIndex)
		{
			if (this.bCreatedOnAttribute)
			{
				return;
			}
			XmlNodeType nodeType = this.curNode.NodeType;
			if (nodeType <= XmlNodeType.Attribute)
			{
				if (nodeType != XmlNodeType.Element)
				{
					if (nodeType != XmlNodeType.Attribute)
					{
						return;
					}
					this.CheckIndexCondition(attributeIndex);
					XmlAttribute xmlAttribute = ((XmlElement)this.elemNode).Attributes[attributeIndex];
					if (xmlAttribute != null)
					{
						this.curNode = xmlAttribute;
						this.attrIndex = attributeIndex;
						return;
					}
				}
				else
				{
					this.CheckIndexCondition(attributeIndex);
					XmlAttribute xmlAttribute = ((XmlElement)this.curNode).Attributes[attributeIndex];
					if (xmlAttribute != null)
					{
						this.elemNode = this.curNode;
						this.curNode = xmlAttribute;
						this.attrIndex = attributeIndex;
						return;
					}
				}
			}
			else
			{
				if (nodeType != XmlNodeType.DocumentType && nodeType != XmlNodeType.XmlDeclaration)
				{
					return;
				}
				this.CheckIndexCondition(attributeIndex);
				this.nAttrInd = attributeIndex;
			}
		}

		// Token: 0x06001275 RID: 4725 RVA: 0x0006F2C4 File Offset: 0x0006D4C4
		public bool MoveToNextAttribute(ref int level)
		{
			if (this.bCreatedOnAttribute)
			{
				return false;
			}
			XmlNodeType nodeType = this.curNode.NodeType;
			if (nodeType != XmlNodeType.Attribute)
			{
				if (nodeType == XmlNodeType.Element)
				{
					if (this.curNode.Attributes.Count > 0)
					{
						level++;
						this.elemNode = this.curNode;
						this.curNode = this.curNode.Attributes[0];
						this.attrIndex = 0;
						return true;
					}
				}
				else if (nodeType == XmlNodeType.XmlDeclaration)
				{
					if (this.nDeclarationAttrCount == -1)
					{
						this.InitDecAttr();
					}
					this.nAttrInd++;
					if (this.nAttrInd < this.nDeclarationAttrCount)
					{
						if (this.nAttrInd == 0)
						{
							level++;
						}
						this.bOnAttrVal = false;
						return true;
					}
					this.nAttrInd--;
				}
				else if (nodeType == XmlNodeType.DocumentType)
				{
					if (this.nDocTypeAttrCount == -1)
					{
						this.InitDocTypeAttr();
					}
					this.nAttrInd++;
					if (this.nAttrInd < this.nDocTypeAttrCount)
					{
						if (this.nAttrInd == 0)
						{
							level++;
						}
						this.bOnAttrVal = false;
						return true;
					}
					this.nAttrInd--;
				}
				return false;
			}
			if (this.attrIndex >= this.elemNode.Attributes.Count - 1)
			{
				return false;
			}
			XmlAttributeCollection attributes = this.elemNode.Attributes;
			int i = this.attrIndex + 1;
			this.attrIndex = i;
			this.curNode = attributes[i];
			return true;
		}

		// Token: 0x06001276 RID: 4726 RVA: 0x0006F42C File Offset: 0x0006D62C
		public bool MoveToParent()
		{
			XmlNode parentNode = this.curNode.ParentNode;
			if (parentNode != null)
			{
				this.curNode = parentNode;
				if (!this.bOnAttrVal)
				{
					this.attrIndex = 0;
				}
				return true;
			}
			return false;
		}

		// Token: 0x06001277 RID: 4727 RVA: 0x0006F464 File Offset: 0x0006D664
		public bool MoveToFirstChild()
		{
			XmlNode firstChild = this.curNode.FirstChild;
			if (firstChild != null)
			{
				this.curNode = firstChild;
				if (!this.bOnAttrVal)
				{
					this.attrIndex = -1;
				}
				return true;
			}
			return false;
		}

		// Token: 0x06001278 RID: 4728 RVA: 0x0006F49C File Offset: 0x0006D69C
		private bool MoveToNextSibling(XmlNode node)
		{
			XmlNode nextSibling = node.NextSibling;
			if (nextSibling != null)
			{
				this.curNode = nextSibling;
				if (!this.bOnAttrVal)
				{
					this.attrIndex = -1;
				}
				return true;
			}
			return false;
		}

		// Token: 0x06001279 RID: 4729 RVA: 0x0006F4CC File Offset: 0x0006D6CC
		public bool MoveToNext()
		{
			if (this.curNode.NodeType != XmlNodeType.Attribute)
			{
				return this.MoveToNextSibling(this.curNode);
			}
			return this.MoveToNextSibling(this.elemNode);
		}

		// Token: 0x0600127A RID: 4730 RVA: 0x0006F4F8 File Offset: 0x0006D6F8
		public bool MoveToElement()
		{
			if (this.bCreatedOnAttribute)
			{
				return false;
			}
			XmlNodeType nodeType = this.curNode.NodeType;
			if (nodeType != XmlNodeType.Attribute)
			{
				if (nodeType == XmlNodeType.DocumentType || nodeType == XmlNodeType.XmlDeclaration)
				{
					if (this.nAttrInd != -1)
					{
						this.nAttrInd = -1;
						return true;
					}
				}
			}
			else if (this.elemNode != null)
			{
				this.curNode = this.elemNode;
				this.attrIndex = -1;
				return true;
			}
			return false;
		}

		// Token: 0x0600127B RID: 4731 RVA: 0x0006F55C File Offset: 0x0006D75C
		public string LookupNamespace(string prefix)
		{
			if (this.bCreatedOnAttribute)
			{
				return null;
			}
			if (prefix == "xmlns")
			{
				return this.nameTable.Add("http://www.w3.org/2000/xmlns/");
			}
			if (prefix == "xml")
			{
				return this.nameTable.Add("http://www.w3.org/XML/1998/namespace");
			}
			if (prefix == null)
			{
				prefix = string.Empty;
			}
			string name;
			if (prefix.Length == 0)
			{
				name = "xmlns";
			}
			else
			{
				name = "xmlns:" + prefix;
			}
			for (XmlNode xmlNode = this.curNode; xmlNode != null; xmlNode = xmlNode.ParentNode)
			{
				if (xmlNode.NodeType == XmlNodeType.Element)
				{
					XmlElement xmlElement = (XmlElement)xmlNode;
					if (xmlElement.HasAttributes)
					{
						XmlAttribute attributeNode = xmlElement.GetAttributeNode(name);
						if (attributeNode != null)
						{
							return attributeNode.Value;
						}
					}
				}
				else if (xmlNode.NodeType == XmlNodeType.Attribute)
				{
					xmlNode = ((XmlAttribute)xmlNode).OwnerElement;
					continue;
				}
			}
			if (prefix.Length == 0)
			{
				return string.Empty;
			}
			return null;
		}

		// Token: 0x0600127C RID: 4732 RVA: 0x0006F638 File Offset: 0x0006D838
		internal string DefaultLookupNamespace(string prefix)
		{
			if (!this.bCreatedOnAttribute)
			{
				if (prefix == "xmlns")
				{
					return this.nameTable.Add("http://www.w3.org/2000/xmlns/");
				}
				if (prefix == "xml")
				{
					return this.nameTable.Add("http://www.w3.org/XML/1998/namespace");
				}
				if (prefix == string.Empty)
				{
					return this.nameTable.Add(string.Empty);
				}
			}
			return null;
		}

		// Token: 0x0600127D RID: 4733 RVA: 0x0006F6A8 File Offset: 0x0006D8A8
		internal string LookupPrefix(string namespaceName)
		{
			if (this.bCreatedOnAttribute || namespaceName == null)
			{
				return null;
			}
			if (namespaceName == "http://www.w3.org/2000/xmlns/")
			{
				return this.nameTable.Add("xmlns");
			}
			if (namespaceName == "http://www.w3.org/XML/1998/namespace")
			{
				return this.nameTable.Add("xml");
			}
			if (namespaceName == string.Empty)
			{
				return string.Empty;
			}
			for (XmlNode xmlNode = this.curNode; xmlNode != null; xmlNode = xmlNode.ParentNode)
			{
				if (xmlNode.NodeType == XmlNodeType.Element)
				{
					XmlElement xmlElement = (XmlElement)xmlNode;
					if (xmlElement.HasAttributes)
					{
						XmlAttributeCollection attributes = xmlElement.Attributes;
						for (int i = 0; i < attributes.Count; i++)
						{
							XmlAttribute xmlAttribute = attributes[i];
							if (xmlAttribute.Value == namespaceName)
							{
								if (xmlAttribute.Prefix.Length == 0 && xmlAttribute.LocalName == "xmlns")
								{
									if (this.LookupNamespace(string.Empty) == namespaceName)
									{
										return string.Empty;
									}
								}
								else if (xmlAttribute.Prefix == "xmlns")
								{
									string localName = xmlAttribute.LocalName;
									if (this.LookupNamespace(localName) == namespaceName)
									{
										return this.nameTable.Add(localName);
									}
								}
							}
						}
					}
				}
				else if (xmlNode.NodeType == XmlNodeType.Attribute)
				{
					xmlNode = ((XmlAttribute)xmlNode).OwnerElement;
					continue;
				}
			}
			return null;
		}

		// Token: 0x0600127E RID: 4734 RVA: 0x0006F80C File Offset: 0x0006DA0C
		internal IDictionary<string, string> GetNamespacesInScope(XmlNamespaceScope scope)
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			if (this.bCreatedOnAttribute)
			{
				return dictionary;
			}
			for (XmlNode xmlNode = this.curNode; xmlNode != null; xmlNode = xmlNode.ParentNode)
			{
				if (xmlNode.NodeType == XmlNodeType.Element)
				{
					XmlElement xmlElement = (XmlElement)xmlNode;
					if (xmlElement.HasAttributes)
					{
						XmlAttributeCollection attributes = xmlElement.Attributes;
						for (int i = 0; i < attributes.Count; i++)
						{
							XmlAttribute xmlAttribute = attributes[i];
							if (xmlAttribute.LocalName == "xmlns" && xmlAttribute.Prefix.Length == 0)
							{
								if (!dictionary.ContainsKey(string.Empty))
								{
									dictionary.Add(this.nameTable.Add(string.Empty), this.nameTable.Add(xmlAttribute.Value));
								}
							}
							else if (xmlAttribute.Prefix == "xmlns")
							{
								string localName = xmlAttribute.LocalName;
								if (!dictionary.ContainsKey(localName))
								{
									dictionary.Add(this.nameTable.Add(localName), this.nameTable.Add(xmlAttribute.Value));
								}
							}
						}
					}
					if (scope == XmlNamespaceScope.Local)
					{
						break;
					}
				}
				else if (xmlNode.NodeType == XmlNodeType.Attribute)
				{
					xmlNode = ((XmlAttribute)xmlNode).OwnerElement;
					continue;
				}
			}
			if (scope != XmlNamespaceScope.Local)
			{
				if (dictionary.ContainsKey(string.Empty) && dictionary[string.Empty] == string.Empty)
				{
					dictionary.Remove(string.Empty);
				}
				if (scope == XmlNamespaceScope.All)
				{
					dictionary.Add(this.nameTable.Add("xml"), this.nameTable.Add("http://www.w3.org/XML/1998/namespace"));
				}
			}
			return dictionary;
		}

		// Token: 0x0600127F RID: 4735 RVA: 0x0006F9AC File Offset: 0x0006DBAC
		public bool ReadAttributeValue(ref int level, ref bool bResolveEntity, ref XmlNodeType nt)
		{
			if (this.nAttrInd == -1)
			{
				if (this.curNode.NodeType == XmlNodeType.Attribute)
				{
					XmlNode firstChild = this.curNode.FirstChild;
					if (firstChild != null)
					{
						this.curNode = firstChild;
						nt = this.curNode.NodeType;
						level++;
						this.bOnAttrVal = true;
						return true;
					}
				}
				else if (this.bOnAttrVal)
				{
					if (this.curNode.NodeType == XmlNodeType.EntityReference & bResolveEntity)
					{
						this.curNode = this.curNode.FirstChild;
						nt = this.curNode.NodeType;
						level++;
						bResolveEntity = false;
						return true;
					}
					XmlNode nextSibling = this.curNode.NextSibling;
					if (nextSibling == null)
					{
						XmlNode parentNode = this.curNode.ParentNode;
						if (parentNode != null && parentNode.NodeType == XmlNodeType.EntityReference)
						{
							this.curNode = parentNode;
							nt = XmlNodeType.EndEntity;
							level--;
							return true;
						}
					}
					if (nextSibling != null)
					{
						this.curNode = nextSibling;
						nt = this.curNode.NodeType;
						return true;
					}
					return false;
				}
				return false;
			}
			if (!this.bOnAttrVal)
			{
				this.bOnAttrVal = true;
				level++;
				nt = XmlNodeType.Text;
				return true;
			}
			return false;
		}

		// Token: 0x17000364 RID: 868
		// (get) Token: 0x06001280 RID: 4736 RVA: 0x0006FAC0 File Offset: 0x0006DCC0
		public XmlDocument Document
		{
			get
			{
				return this.doc;
			}
		}

		// Token: 0x040010BF RID: 4287
		private XmlNode curNode;

		// Token: 0x040010C0 RID: 4288
		private XmlNode elemNode;

		// Token: 0x040010C1 RID: 4289
		private XmlNode logNode;

		// Token: 0x040010C2 RID: 4290
		private int attrIndex;

		// Token: 0x040010C3 RID: 4291
		private int logAttrIndex;

		// Token: 0x040010C4 RID: 4292
		private XmlNameTable nameTable;

		// Token: 0x040010C5 RID: 4293
		private XmlDocument doc;

		// Token: 0x040010C6 RID: 4294
		private int nAttrInd;

		// Token: 0x040010C7 RID: 4295
		private const string strPublicID = "PUBLIC";

		// Token: 0x040010C8 RID: 4296
		private const string strSystemID = "SYSTEM";

		// Token: 0x040010C9 RID: 4297
		private const string strVersion = "version";

		// Token: 0x040010CA RID: 4298
		private const string strStandalone = "standalone";

		// Token: 0x040010CB RID: 4299
		private const string strEncoding = "encoding";

		// Token: 0x040010CC RID: 4300
		private int nDeclarationAttrCount;

		// Token: 0x040010CD RID: 4301
		private int nDocTypeAttrCount;

		// Token: 0x040010CE RID: 4302
		private int nLogLevel;

		// Token: 0x040010CF RID: 4303
		private int nLogAttrInd;

		// Token: 0x040010D0 RID: 4304
		private bool bLogOnAttrVal;

		// Token: 0x040010D1 RID: 4305
		private bool bCreatedOnAttribute;

		// Token: 0x040010D2 RID: 4306
		internal XmlNodeReaderNavigator.VirtualAttribute[] decNodeAttributes = new XmlNodeReaderNavigator.VirtualAttribute[]
		{
			new XmlNodeReaderNavigator.VirtualAttribute(null, null),
			new XmlNodeReaderNavigator.VirtualAttribute(null, null),
			new XmlNodeReaderNavigator.VirtualAttribute(null, null)
		};

		// Token: 0x040010D3 RID: 4307
		internal XmlNodeReaderNavigator.VirtualAttribute[] docTypeNodeAttributes = new XmlNodeReaderNavigator.VirtualAttribute[]
		{
			new XmlNodeReaderNavigator.VirtualAttribute(null, null),
			new XmlNodeReaderNavigator.VirtualAttribute(null, null)
		};

		// Token: 0x040010D4 RID: 4308
		private bool bOnAttrVal;

		// Token: 0x020001D6 RID: 470
		internal struct VirtualAttribute
		{
			// Token: 0x06001281 RID: 4737 RVA: 0x0006FAC8 File Offset: 0x0006DCC8
			internal VirtualAttribute(string name, string value)
			{
				this.name = name;
				this.value = value;
			}

			// Token: 0x040010D5 RID: 4309
			internal string name;

			// Token: 0x040010D6 RID: 4310
			internal string value;
		}
	}
}

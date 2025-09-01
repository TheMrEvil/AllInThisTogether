using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Schema;
using System.Xml.XPath;

namespace System.Xml
{
	// Token: 0x020001A3 RID: 419
	internal sealed class DocumentSchemaValidator : IXmlNamespaceResolver
	{
		// Token: 0x06000EFB RID: 3835 RVA: 0x00062424 File Offset: 0x00060624
		public DocumentSchemaValidator(XmlDocument ownerDocument, XmlSchemaSet schemas, ValidationEventHandler eventHandler)
		{
			this.schemas = schemas;
			this.eventHandler = eventHandler;
			this.document = ownerDocument;
			this.internalEventHandler = new ValidationEventHandler(this.InternalValidationCallBack);
			this.nameTable = this.document.NameTable;
			this.nsManager = new XmlNamespaceManager(this.nameTable);
			this.nodeValueGetter = new XmlValueGetter(this.GetNodeValue);
			this.psviAugmentation = true;
			this.NsXmlNs = this.nameTable.Add("http://www.w3.org/2000/xmlns/");
			this.NsXsi = this.nameTable.Add("http://www.w3.org/2001/XMLSchema-instance");
			this.XsiType = this.nameTable.Add("type");
			this.XsiNil = this.nameTable.Add("nil");
		}

		// Token: 0x17000255 RID: 597
		// (get) Token: 0x06000EFC RID: 3836 RVA: 0x000624F1 File Offset: 0x000606F1
		// (set) Token: 0x06000EFD RID: 3837 RVA: 0x000624F9 File Offset: 0x000606F9
		public bool PsviAugmentation
		{
			get
			{
				return this.psviAugmentation;
			}
			set
			{
				this.psviAugmentation = value;
			}
		}

		// Token: 0x06000EFE RID: 3838 RVA: 0x00062504 File Offset: 0x00060704
		public bool Validate(XmlNode nodeToValidate)
		{
			XmlSchemaObject xmlSchemaObject = null;
			XmlSchemaValidationFlags xmlSchemaValidationFlags = XmlSchemaValidationFlags.AllowXmlAttributes;
			this.startNode = nodeToValidate;
			XmlNodeType nodeType = nodeToValidate.NodeType;
			if (nodeType <= XmlNodeType.Attribute)
			{
				if (nodeType != XmlNodeType.Element)
				{
					if (nodeType == XmlNodeType.Attribute)
					{
						if (nodeToValidate.XPNodeType != XPathNodeType.Namespace)
						{
							xmlSchemaObject = nodeToValidate.SchemaInfo.SchemaAttribute;
							if (xmlSchemaObject != null)
							{
								goto IL_10F;
							}
							xmlSchemaObject = this.FindSchemaInfo(nodeToValidate as XmlAttribute);
							if (xmlSchemaObject == null)
							{
								throw new XmlSchemaValidationException("Schema information could not be found for the node passed into Validate. The node may be invalid in its current position. Navigate to the ancestor that has schema information, then call Validate again.", null, nodeToValidate);
							}
							goto IL_10F;
						}
					}
				}
				else
				{
					IXmlSchemaInfo xmlSchemaInfo = nodeToValidate.SchemaInfo;
					XmlSchemaElement schemaElement = xmlSchemaInfo.SchemaElement;
					if (schemaElement != null)
					{
						if (!schemaElement.RefName.IsEmpty)
						{
							xmlSchemaObject = this.schemas.GlobalElements[schemaElement.QualifiedName];
							goto IL_10F;
						}
						xmlSchemaObject = schemaElement;
						goto IL_10F;
					}
					else
					{
						xmlSchemaObject = xmlSchemaInfo.SchemaType;
						if (xmlSchemaObject != null)
						{
							goto IL_10F;
						}
						if (nodeToValidate.ParentNode.NodeType == XmlNodeType.Document)
						{
							nodeToValidate = nodeToValidate.ParentNode;
							goto IL_10F;
						}
						xmlSchemaObject = this.FindSchemaInfo(nodeToValidate as XmlElement);
						if (xmlSchemaObject == null)
						{
							throw new XmlSchemaValidationException("Schema information could not be found for the node passed into Validate. The node may be invalid in its current position. Navigate to the ancestor that has schema information, then call Validate again.", null, nodeToValidate);
						}
						goto IL_10F;
					}
				}
			}
			else
			{
				if (nodeType == XmlNodeType.Document)
				{
					xmlSchemaValidationFlags |= XmlSchemaValidationFlags.ProcessIdentityConstraints;
					goto IL_10F;
				}
				if (nodeType == XmlNodeType.DocumentFragment)
				{
					goto IL_10F;
				}
			}
			throw new InvalidOperationException(Res.GetString("Validate method can be called only on nodes of type Document, DocumentFragment, Element, or Attribute.", null));
			IL_10F:
			this.isValid = true;
			this.CreateValidator(xmlSchemaObject, xmlSchemaValidationFlags);
			if (this.psviAugmentation)
			{
				if (this.schemaInfo == null)
				{
					this.schemaInfo = new XmlSchemaInfo();
				}
				this.attributeSchemaInfo = new XmlSchemaInfo();
			}
			this.ValidateNode(nodeToValidate);
			this.validator.EndValidation();
			return this.isValid;
		}

		// Token: 0x06000EFF RID: 3839 RVA: 0x00062670 File Offset: 0x00060870
		public IDictionary<string, string> GetNamespacesInScope(XmlNamespaceScope scope)
		{
			IDictionary<string, string> namespacesInScope = this.nsManager.GetNamespacesInScope(scope);
			if (scope != XmlNamespaceScope.Local)
			{
				XmlNode xmlNode = this.startNode;
				while (xmlNode != null)
				{
					XmlNodeType nodeType = xmlNode.NodeType;
					if (nodeType != XmlNodeType.Element)
					{
						if (nodeType != XmlNodeType.Attribute)
						{
							xmlNode = xmlNode.ParentNode;
						}
						else
						{
							xmlNode = ((XmlAttribute)xmlNode).OwnerElement;
						}
					}
					else
					{
						XmlElement xmlElement = (XmlElement)xmlNode;
						if (xmlElement.HasAttributes)
						{
							XmlAttributeCollection attributes = xmlElement.Attributes;
							for (int i = 0; i < attributes.Count; i++)
							{
								XmlAttribute xmlAttribute = attributes[i];
								if (Ref.Equal(xmlAttribute.NamespaceURI, this.document.strReservedXmlns))
								{
									if (xmlAttribute.Prefix.Length == 0)
									{
										if (!namespacesInScope.ContainsKey(string.Empty))
										{
											namespacesInScope.Add(string.Empty, xmlAttribute.Value);
										}
									}
									else if (!namespacesInScope.ContainsKey(xmlAttribute.LocalName))
									{
										namespacesInScope.Add(xmlAttribute.LocalName, xmlAttribute.Value);
									}
								}
							}
						}
						xmlNode = xmlNode.ParentNode;
					}
				}
			}
			return namespacesInScope;
		}

		// Token: 0x06000F00 RID: 3840 RVA: 0x00062784 File Offset: 0x00060984
		public string LookupNamespace(string prefix)
		{
			string text = this.nsManager.LookupNamespace(prefix);
			if (text == null)
			{
				text = this.startNode.GetNamespaceOfPrefixStrict(prefix);
			}
			return text;
		}

		// Token: 0x06000F01 RID: 3841 RVA: 0x000627B0 File Offset: 0x000609B0
		public string LookupPrefix(string namespaceName)
		{
			string text = this.nsManager.LookupPrefix(namespaceName);
			if (text == null)
			{
				text = this.startNode.GetPrefixOfNamespaceStrict(namespaceName);
			}
			return text;
		}

		// Token: 0x17000256 RID: 598
		// (get) Token: 0x06000F02 RID: 3842 RVA: 0x000627DB File Offset: 0x000609DB
		private IXmlNamespaceResolver NamespaceResolver
		{
			get
			{
				if (this.startNode == this.document)
				{
					return this.nsManager;
				}
				return this;
			}
		}

		// Token: 0x06000F03 RID: 3843 RVA: 0x000627F4 File Offset: 0x000609F4
		private void CreateValidator(XmlSchemaObject partialValidationType, XmlSchemaValidationFlags validationFlags)
		{
			this.validator = new XmlSchemaValidator(this.nameTable, this.schemas, this.NamespaceResolver, validationFlags);
			this.validator.SourceUri = XmlConvert.ToUri(this.document.BaseURI);
			this.validator.XmlResolver = null;
			this.validator.ValidationEventHandler += this.internalEventHandler;
			this.validator.ValidationEventSender = this;
			if (partialValidationType != null)
			{
				this.validator.Initialize(partialValidationType);
				return;
			}
			this.validator.Initialize();
		}

		// Token: 0x06000F04 RID: 3844 RVA: 0x00062880 File Offset: 0x00060A80
		private void ValidateNode(XmlNode node)
		{
			this.currentNode = node;
			switch (this.currentNode.NodeType)
			{
			case XmlNodeType.Element:
				this.ValidateElement();
				return;
			case XmlNodeType.Attribute:
			{
				XmlAttribute xmlAttribute = this.currentNode as XmlAttribute;
				this.validator.ValidateAttribute(xmlAttribute.LocalName, xmlAttribute.NamespaceURI, this.nodeValueGetter, this.attributeSchemaInfo);
				if (this.psviAugmentation)
				{
					xmlAttribute.XmlName = this.document.AddAttrXmlName(xmlAttribute.Prefix, xmlAttribute.LocalName, xmlAttribute.NamespaceURI, this.attributeSchemaInfo);
					return;
				}
				return;
			}
			case XmlNodeType.Text:
				this.validator.ValidateText(this.nodeValueGetter);
				return;
			case XmlNodeType.CDATA:
				this.validator.ValidateText(this.nodeValueGetter);
				return;
			case XmlNodeType.EntityReference:
			case XmlNodeType.DocumentFragment:
				for (XmlNode xmlNode = node.FirstChild; xmlNode != null; xmlNode = xmlNode.NextSibling)
				{
					this.ValidateNode(xmlNode);
				}
				return;
			case XmlNodeType.ProcessingInstruction:
			case XmlNodeType.Comment:
				return;
			case XmlNodeType.Document:
			{
				XmlElement documentElement = ((XmlDocument)node).DocumentElement;
				if (documentElement == null)
				{
					throw new InvalidOperationException(Res.GetString("Invalid XML document. {0}", new object[]
					{
						Res.GetString("The document does not have a root element.")
					}));
				}
				this.ValidateNode(documentElement);
				return;
			}
			case XmlNodeType.Whitespace:
			case XmlNodeType.SignificantWhitespace:
				this.validator.ValidateWhitespace(this.nodeValueGetter);
				return;
			}
			string name = "Unexpected XmlNodeType: '{0}'.";
			object[] args = new string[]
			{
				this.currentNode.NodeType.ToString()
			};
			throw new InvalidOperationException(Res.GetString(name, args));
		}

		// Token: 0x06000F05 RID: 3845 RVA: 0x00062A14 File Offset: 0x00060C14
		private void ValidateElement()
		{
			this.nsManager.PushScope();
			XmlElement xmlElement = this.currentNode as XmlElement;
			XmlAttributeCollection attributes = xmlElement.Attributes;
			string xsiNil = null;
			string xsiType = null;
			for (int i = 0; i < attributes.Count; i++)
			{
				XmlAttribute xmlAttribute = attributes[i];
				string namespaceURI = xmlAttribute.NamespaceURI;
				string localName = xmlAttribute.LocalName;
				if (Ref.Equal(namespaceURI, this.NsXsi))
				{
					if (Ref.Equal(localName, this.XsiType))
					{
						xsiType = xmlAttribute.Value;
					}
					else if (Ref.Equal(localName, this.XsiNil))
					{
						xsiNil = xmlAttribute.Value;
					}
				}
				else if (Ref.Equal(namespaceURI, this.NsXmlNs))
				{
					this.nsManager.AddNamespace((xmlAttribute.Prefix.Length == 0) ? string.Empty : xmlAttribute.LocalName, xmlAttribute.Value);
				}
			}
			this.validator.ValidateElement(xmlElement.LocalName, xmlElement.NamespaceURI, this.schemaInfo, xsiType, xsiNil, null, null);
			this.ValidateAttributes(xmlElement);
			this.validator.ValidateEndOfAttributes(this.schemaInfo);
			for (XmlNode xmlNode = xmlElement.FirstChild; xmlNode != null; xmlNode = xmlNode.NextSibling)
			{
				this.ValidateNode(xmlNode);
			}
			this.currentNode = xmlElement;
			this.validator.ValidateEndElement(this.schemaInfo);
			if (this.psviAugmentation)
			{
				xmlElement.XmlName = this.document.AddXmlName(xmlElement.Prefix, xmlElement.LocalName, xmlElement.NamespaceURI, this.schemaInfo);
				if (this.schemaInfo.IsDefault)
				{
					XmlText newChild = this.document.CreateTextNode(this.schemaInfo.SchemaElement.ElementDecl.DefaultValueRaw);
					xmlElement.AppendChild(newChild);
				}
			}
			this.nsManager.PopScope();
		}

		// Token: 0x06000F06 RID: 3846 RVA: 0x00062BDC File Offset: 0x00060DDC
		private void ValidateAttributes(XmlElement elementNode)
		{
			XmlAttributeCollection attributes = elementNode.Attributes;
			for (int i = 0; i < attributes.Count; i++)
			{
				XmlAttribute xmlAttribute = attributes[i];
				this.currentNode = xmlAttribute;
				if (!Ref.Equal(xmlAttribute.NamespaceURI, this.NsXmlNs))
				{
					this.validator.ValidateAttribute(xmlAttribute.LocalName, xmlAttribute.NamespaceURI, this.nodeValueGetter, this.attributeSchemaInfo);
					if (this.psviAugmentation)
					{
						xmlAttribute.XmlName = this.document.AddAttrXmlName(xmlAttribute.Prefix, xmlAttribute.LocalName, xmlAttribute.NamespaceURI, this.attributeSchemaInfo);
					}
				}
			}
			if (this.psviAugmentation)
			{
				if (this.defaultAttributes == null)
				{
					this.defaultAttributes = new ArrayList();
				}
				else
				{
					this.defaultAttributes.Clear();
				}
				this.validator.GetUnspecifiedDefaultAttributes(this.defaultAttributes);
				for (int j = 0; j < this.defaultAttributes.Count; j++)
				{
					XmlSchemaAttribute xmlSchemaAttribute = this.defaultAttributes[j] as XmlSchemaAttribute;
					XmlQualifiedName qualifiedName = xmlSchemaAttribute.QualifiedName;
					XmlAttribute xmlAttribute = this.document.CreateDefaultAttribute(this.GetDefaultPrefix(qualifiedName.Namespace), qualifiedName.Name, qualifiedName.Namespace);
					this.SetDefaultAttributeSchemaInfo(xmlSchemaAttribute);
					xmlAttribute.XmlName = this.document.AddAttrXmlName(xmlAttribute.Prefix, xmlAttribute.LocalName, xmlAttribute.NamespaceURI, this.attributeSchemaInfo);
					xmlAttribute.AppendChild(this.document.CreateTextNode(xmlSchemaAttribute.AttDef.DefaultValueRaw));
					attributes.Append(xmlAttribute);
					XmlUnspecifiedAttribute xmlUnspecifiedAttribute = xmlAttribute as XmlUnspecifiedAttribute;
					if (xmlUnspecifiedAttribute != null)
					{
						xmlUnspecifiedAttribute.SetSpecified(false);
					}
				}
			}
		}

		// Token: 0x06000F07 RID: 3847 RVA: 0x00062D88 File Offset: 0x00060F88
		private void SetDefaultAttributeSchemaInfo(XmlSchemaAttribute schemaAttribute)
		{
			this.attributeSchemaInfo.Clear();
			this.attributeSchemaInfo.IsDefault = true;
			this.attributeSchemaInfo.IsNil = false;
			this.attributeSchemaInfo.SchemaType = schemaAttribute.AttributeSchemaType;
			this.attributeSchemaInfo.SchemaAttribute = schemaAttribute;
			SchemaAttDef attDef = schemaAttribute.AttDef;
			if (attDef.Datatype.Variety == XmlSchemaDatatypeVariety.Union)
			{
				XsdSimpleValue xsdSimpleValue = attDef.DefaultValueTyped as XsdSimpleValue;
				this.attributeSchemaInfo.MemberType = xsdSimpleValue.XmlType;
			}
			this.attributeSchemaInfo.Validity = XmlSchemaValidity.Valid;
		}

		// Token: 0x06000F08 RID: 3848 RVA: 0x00062E14 File Offset: 0x00061014
		private string GetDefaultPrefix(string attributeNS)
		{
			IEnumerable<KeyValuePair<string, string>> namespacesInScope = this.NamespaceResolver.GetNamespacesInScope(XmlNamespaceScope.All);
			string text = null;
			attributeNS = this.nameTable.Add(attributeNS);
			foreach (KeyValuePair<string, string> keyValuePair in namespacesInScope)
			{
				if (this.nameTable.Add(keyValuePair.Value) == attributeNS)
				{
					text = keyValuePair.Key;
					if (text.Length != 0)
					{
						return text;
					}
				}
			}
			return text;
		}

		// Token: 0x06000F09 RID: 3849 RVA: 0x00062E9C File Offset: 0x0006109C
		private object GetNodeValue()
		{
			return this.currentNode.Value;
		}

		// Token: 0x06000F0A RID: 3850 RVA: 0x00062EAC File Offset: 0x000610AC
		private XmlSchemaObject FindSchemaInfo(XmlElement elementToValidate)
		{
			this.isPartialTreeValid = true;
			int num = 0;
			XmlNode parentNode = elementToValidate.ParentNode;
			IXmlSchemaInfo xmlSchemaInfo;
			do
			{
				xmlSchemaInfo = parentNode.SchemaInfo;
				if (xmlSchemaInfo.SchemaElement != null || xmlSchemaInfo.SchemaType != null)
				{
					break;
				}
				this.CheckNodeSequenceCapacity(num);
				this.nodeSequenceToValidate[num++] = parentNode;
				parentNode = parentNode.ParentNode;
			}
			while (parentNode != null);
			if (parentNode == null)
			{
				num--;
				this.nodeSequenceToValidate[num] = null;
				return this.GetTypeFromAncestors(elementToValidate, null, num);
			}
			this.CheckNodeSequenceCapacity(num);
			this.nodeSequenceToValidate[num++] = parentNode;
			XmlSchemaObject xmlSchemaObject = xmlSchemaInfo.SchemaElement;
			if (xmlSchemaObject == null)
			{
				xmlSchemaObject = xmlSchemaInfo.SchemaType;
			}
			return this.GetTypeFromAncestors(elementToValidate, xmlSchemaObject, num);
		}

		// Token: 0x06000F0B RID: 3851 RVA: 0x00062F48 File Offset: 0x00061148
		private void CheckNodeSequenceCapacity(int currentIndex)
		{
			if (this.nodeSequenceToValidate == null)
			{
				this.nodeSequenceToValidate = new XmlNode[4];
				return;
			}
			if (currentIndex >= this.nodeSequenceToValidate.Length - 1)
			{
				XmlNode[] destinationArray = new XmlNode[this.nodeSequenceToValidate.Length * 2];
				Array.Copy(this.nodeSequenceToValidate, 0, destinationArray, 0, this.nodeSequenceToValidate.Length);
				this.nodeSequenceToValidate = destinationArray;
			}
		}

		// Token: 0x06000F0C RID: 3852 RVA: 0x00062FA4 File Offset: 0x000611A4
		private XmlSchemaAttribute FindSchemaInfo(XmlAttribute attributeToValidate)
		{
			XmlElement ownerElement = attributeToValidate.OwnerElement;
			XmlSchemaObject schemaObject = this.FindSchemaInfo(ownerElement);
			XmlSchemaComplexType complexType = this.GetComplexType(schemaObject);
			if (complexType == null)
			{
				return null;
			}
			XmlQualifiedName xmlQualifiedName = new XmlQualifiedName(attributeToValidate.LocalName, attributeToValidate.NamespaceURI);
			XmlSchemaAttribute xmlSchemaAttribute = complexType.AttributeUses[xmlQualifiedName] as XmlSchemaAttribute;
			if (xmlSchemaAttribute == null)
			{
				XmlSchemaAnyAttribute attributeWildcard = complexType.AttributeWildcard;
				if (attributeWildcard != null && attributeWildcard.NamespaceList.Allows(xmlQualifiedName))
				{
					xmlSchemaAttribute = (this.schemas.GlobalAttributes[xmlQualifiedName] as XmlSchemaAttribute);
				}
			}
			return xmlSchemaAttribute;
		}

		// Token: 0x06000F0D RID: 3853 RVA: 0x0006302C File Offset: 0x0006122C
		private XmlSchemaObject GetTypeFromAncestors(XmlElement elementToValidate, XmlSchemaObject ancestorType, int ancestorsCount)
		{
			this.validator = this.CreateTypeFinderValidator(ancestorType);
			this.schemaInfo = new XmlSchemaInfo();
			int num = ancestorsCount - 1;
			bool flag = this.AncestorTypeHasWildcard(ancestorType);
			for (int i = num; i >= 0; i--)
			{
				XmlNode xmlNode = this.nodeSequenceToValidate[i];
				XmlElement xmlElement = xmlNode as XmlElement;
				this.ValidateSingleElement(xmlElement, false, this.schemaInfo);
				if (!flag)
				{
					xmlElement.XmlName = this.document.AddXmlName(xmlElement.Prefix, xmlElement.LocalName, xmlElement.NamespaceURI, this.schemaInfo);
					flag = this.AncestorTypeHasWildcard(this.schemaInfo.SchemaElement);
				}
				this.validator.ValidateEndOfAttributes(null);
				if (i > 0)
				{
					this.ValidateChildrenTillNextAncestor(xmlNode, this.nodeSequenceToValidate[i - 1]);
				}
				else
				{
					this.ValidateChildrenTillNextAncestor(xmlNode, elementToValidate);
				}
			}
			this.ValidateSingleElement(elementToValidate, false, this.schemaInfo);
			XmlSchemaObject xmlSchemaObject;
			if (this.schemaInfo.SchemaElement != null)
			{
				xmlSchemaObject = this.schemaInfo.SchemaElement;
			}
			else
			{
				xmlSchemaObject = this.schemaInfo.SchemaType;
			}
			if (xmlSchemaObject == null)
			{
				if (this.validator.CurrentProcessContents == XmlSchemaContentProcessing.Skip)
				{
					if (this.isPartialTreeValid)
					{
						return XmlSchemaComplexType.AnyTypeSkip;
					}
				}
				else if (this.validator.CurrentProcessContents == XmlSchemaContentProcessing.Lax)
				{
					return XmlSchemaComplexType.AnyType;
				}
			}
			return xmlSchemaObject;
		}

		// Token: 0x06000F0E RID: 3854 RVA: 0x00063168 File Offset: 0x00061368
		private bool AncestorTypeHasWildcard(XmlSchemaObject ancestorType)
		{
			XmlSchemaComplexType complexType = this.GetComplexType(ancestorType);
			return ancestorType != null && complexType.HasWildCard;
		}

		// Token: 0x06000F0F RID: 3855 RVA: 0x00063188 File Offset: 0x00061388
		private XmlSchemaComplexType GetComplexType(XmlSchemaObject schemaObject)
		{
			if (schemaObject == null)
			{
				return null;
			}
			XmlSchemaElement xmlSchemaElement = schemaObject as XmlSchemaElement;
			XmlSchemaComplexType result;
			if (xmlSchemaElement != null)
			{
				result = (xmlSchemaElement.ElementSchemaType as XmlSchemaComplexType);
			}
			else
			{
				result = (schemaObject as XmlSchemaComplexType);
			}
			return result;
		}

		// Token: 0x06000F10 RID: 3856 RVA: 0x000631BC File Offset: 0x000613BC
		private void ValidateSingleElement(XmlElement elementNode, bool skipToEnd, XmlSchemaInfo newSchemaInfo)
		{
			this.nsManager.PushScope();
			XmlAttributeCollection attributes = elementNode.Attributes;
			string xsiNil = null;
			string xsiType = null;
			for (int i = 0; i < attributes.Count; i++)
			{
				XmlAttribute xmlAttribute = attributes[i];
				string namespaceURI = xmlAttribute.NamespaceURI;
				string localName = xmlAttribute.LocalName;
				if (Ref.Equal(namespaceURI, this.NsXsi))
				{
					if (Ref.Equal(localName, this.XsiType))
					{
						xsiType = xmlAttribute.Value;
					}
					else if (Ref.Equal(localName, this.XsiNil))
					{
						xsiNil = xmlAttribute.Value;
					}
				}
				else if (Ref.Equal(namespaceURI, this.NsXmlNs))
				{
					this.nsManager.AddNamespace((xmlAttribute.Prefix.Length == 0) ? string.Empty : xmlAttribute.LocalName, xmlAttribute.Value);
				}
			}
			this.validator.ValidateElement(elementNode.LocalName, elementNode.NamespaceURI, newSchemaInfo, xsiType, xsiNil, null, null);
			if (skipToEnd)
			{
				this.validator.ValidateEndOfAttributes(newSchemaInfo);
				this.validator.SkipToEndElement(newSchemaInfo);
				this.nsManager.PopScope();
			}
		}

		// Token: 0x06000F11 RID: 3857 RVA: 0x000632D4 File Offset: 0x000614D4
		private void ValidateChildrenTillNextAncestor(XmlNode parentNode, XmlNode childToStopAt)
		{
			XmlNode xmlNode = parentNode.FirstChild;
			while (xmlNode != null && xmlNode != childToStopAt)
			{
				switch (xmlNode.NodeType)
				{
				case XmlNodeType.Element:
					this.ValidateSingleElement(xmlNode as XmlElement, true, null);
					break;
				case XmlNodeType.Attribute:
				case XmlNodeType.Entity:
				case XmlNodeType.Document:
				case XmlNodeType.DocumentType:
				case XmlNodeType.DocumentFragment:
				case XmlNodeType.Notation:
					goto IL_9C;
				case XmlNodeType.Text:
				case XmlNodeType.CDATA:
					this.validator.ValidateText(xmlNode.Value);
					break;
				case XmlNodeType.EntityReference:
					this.ValidateChildrenTillNextAncestor(xmlNode, childToStopAt);
					break;
				case XmlNodeType.ProcessingInstruction:
				case XmlNodeType.Comment:
					break;
				case XmlNodeType.Whitespace:
				case XmlNodeType.SignificantWhitespace:
					this.validator.ValidateWhitespace(xmlNode.Value);
					break;
				default:
					goto IL_9C;
				}
				xmlNode = xmlNode.NextSibling;
				continue;
				IL_9C:
				string name = "Unexpected XmlNodeType: '{0}'.";
				object[] args = new string[]
				{
					this.currentNode.NodeType.ToString()
				};
				throw new InvalidOperationException(Res.GetString(name, args));
			}
		}

		// Token: 0x06000F12 RID: 3858 RVA: 0x000633C0 File Offset: 0x000615C0
		private XmlSchemaValidator CreateTypeFinderValidator(XmlSchemaObject partialValidationType)
		{
			XmlSchemaValidator xmlSchemaValidator = new XmlSchemaValidator(this.document.NameTable, this.document.Schemas, this.nsManager, XmlSchemaValidationFlags.None);
			xmlSchemaValidator.ValidationEventHandler += this.TypeFinderCallBack;
			if (partialValidationType != null)
			{
				xmlSchemaValidator.Initialize(partialValidationType);
			}
			else
			{
				xmlSchemaValidator.Initialize();
			}
			return xmlSchemaValidator;
		}

		// Token: 0x06000F13 RID: 3859 RVA: 0x00063415 File Offset: 0x00061615
		private void TypeFinderCallBack(object sender, ValidationEventArgs arg)
		{
			if (arg.Severity == XmlSeverityType.Error)
			{
				this.isPartialTreeValid = false;
			}
		}

		// Token: 0x06000F14 RID: 3860 RVA: 0x00063428 File Offset: 0x00061628
		private void InternalValidationCallBack(object sender, ValidationEventArgs arg)
		{
			if (arg.Severity == XmlSeverityType.Error)
			{
				this.isValid = false;
			}
			XmlSchemaValidationException ex = arg.Exception as XmlSchemaValidationException;
			ex.SetSourceObject(this.currentNode);
			if (this.eventHandler != null)
			{
				this.eventHandler(sender, arg);
				return;
			}
			if (arg.Severity == XmlSeverityType.Error)
			{
				throw ex;
			}
		}

		// Token: 0x04000FE2 RID: 4066
		private XmlSchemaValidator validator;

		// Token: 0x04000FE3 RID: 4067
		private XmlSchemaSet schemas;

		// Token: 0x04000FE4 RID: 4068
		private XmlNamespaceManager nsManager;

		// Token: 0x04000FE5 RID: 4069
		private XmlNameTable nameTable;

		// Token: 0x04000FE6 RID: 4070
		private ArrayList defaultAttributes;

		// Token: 0x04000FE7 RID: 4071
		private XmlValueGetter nodeValueGetter;

		// Token: 0x04000FE8 RID: 4072
		private XmlSchemaInfo attributeSchemaInfo;

		// Token: 0x04000FE9 RID: 4073
		private XmlSchemaInfo schemaInfo;

		// Token: 0x04000FEA RID: 4074
		private ValidationEventHandler eventHandler;

		// Token: 0x04000FEB RID: 4075
		private ValidationEventHandler internalEventHandler;

		// Token: 0x04000FEC RID: 4076
		private XmlNode startNode;

		// Token: 0x04000FED RID: 4077
		private XmlNode currentNode;

		// Token: 0x04000FEE RID: 4078
		private XmlDocument document;

		// Token: 0x04000FEF RID: 4079
		private XmlNode[] nodeSequenceToValidate;

		// Token: 0x04000FF0 RID: 4080
		private bool isPartialTreeValid;

		// Token: 0x04000FF1 RID: 4081
		private bool psviAugmentation;

		// Token: 0x04000FF2 RID: 4082
		private bool isValid;

		// Token: 0x04000FF3 RID: 4083
		private string NsXmlNs;

		// Token: 0x04000FF4 RID: 4084
		private string NsXsi;

		// Token: 0x04000FF5 RID: 4085
		private string XsiType;

		// Token: 0x04000FF6 RID: 4086
		private string XsiNil;
	}
}

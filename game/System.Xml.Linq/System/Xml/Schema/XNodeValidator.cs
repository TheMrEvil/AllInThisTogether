using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;

namespace System.Xml.Schema
{
	// Token: 0x0200000C RID: 12
	internal class XNodeValidator
	{
		// Token: 0x06000059 RID: 89 RVA: 0x0000351C File Offset: 0x0000171C
		public XNodeValidator(XmlSchemaSet schemas, ValidationEventHandler validationEventHandler)
		{
			this.schemas = schemas;
			this.validationEventHandler = validationEventHandler;
			XNamespace xnamespace = XNamespace.Get("http://www.w3.org/2001/XMLSchema-instance");
			this.xsiTypeName = xnamespace.GetName("type");
			this.xsiNilName = xnamespace.GetName("nil");
		}

		// Token: 0x0600005A RID: 90 RVA: 0x0000356C File Offset: 0x0000176C
		public void Validate(XObject source, XmlSchemaObject partialValidationType, bool addSchemaInfo)
		{
			this.source = source;
			this.addSchemaInfo = addSchemaInfo;
			XmlSchemaValidationFlags xmlSchemaValidationFlags = XmlSchemaValidationFlags.AllowXmlAttributes;
			XmlNodeType nodeType = source.NodeType;
			if (nodeType != XmlNodeType.Element)
			{
				if (nodeType != XmlNodeType.Attribute)
				{
					if (nodeType == XmlNodeType.Document)
					{
						source = ((XDocument)source).Root;
						if (source == null)
						{
							throw new InvalidOperationException(SR.Format("The root element is missing.", Array.Empty<object>()));
						}
						xmlSchemaValidationFlags |= XmlSchemaValidationFlags.ProcessIdentityConstraints;
						goto IL_90;
					}
				}
				else if (!((XAttribute)source).IsNamespaceDeclaration)
				{
					if (source.Parent == null)
					{
						throw new InvalidOperationException(SR.Format("The parent is missing.", Array.Empty<object>()));
					}
					goto IL_90;
				}
				throw new InvalidOperationException(SR.Format("This operation is not valid on a node of type {0}.", nodeType));
			}
			IL_90:
			this.namespaceManager = new XmlNamespaceManager(this.schemas.NameTable);
			this.PushAncestorsAndSelf(source.Parent);
			this.validator = new XmlSchemaValidator(this.schemas.NameTable, this.schemas, this.namespaceManager, xmlSchemaValidationFlags);
			this.validator.ValidationEventHandler += this.ValidationCallback;
			this.validator.XmlResolver = null;
			if (partialValidationType != null)
			{
				this.validator.Initialize(partialValidationType);
			}
			else
			{
				this.validator.Initialize();
			}
			IXmlLineInfo originalLineInfo = this.SaveLineInfo(source);
			if (nodeType == XmlNodeType.Attribute)
			{
				this.ValidateAttribute((XAttribute)source);
			}
			else
			{
				this.ValidateElement((XElement)source);
			}
			this.validator.EndValidation();
			this.RestoreLineInfo(originalLineInfo);
		}

		// Token: 0x0600005B RID: 91 RVA: 0x000036C8 File Offset: 0x000018C8
		private XmlSchemaInfo GetDefaultAttributeSchemaInfo(XmlSchemaAttribute sa)
		{
			XmlSchemaInfo xmlSchemaInfo = new XmlSchemaInfo();
			xmlSchemaInfo.IsDefault = true;
			xmlSchemaInfo.IsNil = false;
			xmlSchemaInfo.SchemaAttribute = sa;
			XmlSchemaSimpleType attributeSchemaType = sa.AttributeSchemaType;
			xmlSchemaInfo.SchemaType = attributeSchemaType;
			if (attributeSchemaType.Datatype.Variety == XmlSchemaDatatypeVariety.Union)
			{
				string defaultValue = this.GetDefaultValue(sa);
				foreach (XmlSchemaSimpleType xmlSchemaSimpleType in ((XmlSchemaSimpleTypeUnion)attributeSchemaType.Content).BaseMemberTypes)
				{
					object obj = null;
					try
					{
						obj = xmlSchemaSimpleType.Datatype.ParseValue(defaultValue, this.schemas.NameTable, this.namespaceManager);
					}
					catch (XmlSchemaException)
					{
					}
					if (obj != null)
					{
						xmlSchemaInfo.MemberType = xmlSchemaSimpleType;
						break;
					}
				}
			}
			xmlSchemaInfo.Validity = XmlSchemaValidity.Valid;
			return xmlSchemaInfo;
		}

		// Token: 0x0600005C RID: 92 RVA: 0x0000378C File Offset: 0x0000198C
		private string GetDefaultValue(XmlSchemaAttribute sa)
		{
			XmlQualifiedName refName = sa.RefName;
			if (!refName.IsEmpty)
			{
				sa = (this.schemas.GlobalAttributes[refName] as XmlSchemaAttribute);
				if (sa == null)
				{
					return null;
				}
			}
			string fixedValue = sa.FixedValue;
			if (fixedValue != null)
			{
				return fixedValue;
			}
			return sa.DefaultValue;
		}

		// Token: 0x0600005D RID: 93 RVA: 0x000037D8 File Offset: 0x000019D8
		private string GetDefaultValue(XmlSchemaElement se)
		{
			XmlQualifiedName refName = se.RefName;
			if (!refName.IsEmpty)
			{
				se = (this.schemas.GlobalElements[refName] as XmlSchemaElement);
				if (se == null)
				{
					return null;
				}
			}
			string fixedValue = se.FixedValue;
			if (fixedValue != null)
			{
				return fixedValue;
			}
			return se.DefaultValue;
		}

		// Token: 0x0600005E RID: 94 RVA: 0x00003824 File Offset: 0x00001A24
		private void ReplaceSchemaInfo(XObject o, XmlSchemaInfo schemaInfo)
		{
			if (this.schemaInfos == null)
			{
				this.schemaInfos = new Dictionary<XmlSchemaInfo, XmlSchemaInfo>(new XmlSchemaInfoEqualityComparer());
			}
			XmlSchemaInfo xmlSchemaInfo = o.Annotation<XmlSchemaInfo>();
			if (xmlSchemaInfo != null)
			{
				if (!this.schemaInfos.ContainsKey(xmlSchemaInfo))
				{
					this.schemaInfos.Add(xmlSchemaInfo, xmlSchemaInfo);
				}
				o.RemoveAnnotations<XmlSchemaInfo>();
			}
			if (!this.schemaInfos.TryGetValue(schemaInfo, out xmlSchemaInfo))
			{
				xmlSchemaInfo = schemaInfo;
				this.schemaInfos.Add(xmlSchemaInfo, xmlSchemaInfo);
			}
			o.AddAnnotation(xmlSchemaInfo);
		}

		// Token: 0x0600005F RID: 95 RVA: 0x0000389C File Offset: 0x00001A9C
		private void PushAncestorsAndSelf(XElement e)
		{
			while (e != null)
			{
				XAttribute xattribute = e.lastAttr;
				if (xattribute != null)
				{
					do
					{
						xattribute = xattribute.next;
						if (xattribute.IsNamespaceDeclaration)
						{
							string text = xattribute.Name.LocalName;
							if (text == "xmlns")
							{
								text = string.Empty;
							}
							if (!this.namespaceManager.HasNamespace(text))
							{
								this.namespaceManager.AddNamespace(text, xattribute.Value);
							}
						}
					}
					while (xattribute != e.lastAttr);
				}
				e = (e.parent as XElement);
			}
		}

		// Token: 0x06000060 RID: 96 RVA: 0x0000391C File Offset: 0x00001B1C
		private void PushElement(XElement e, ref string xsiType, ref string xsiNil)
		{
			this.namespaceManager.PushScope();
			XAttribute xattribute = e.lastAttr;
			if (xattribute != null)
			{
				do
				{
					xattribute = xattribute.next;
					if (xattribute.IsNamespaceDeclaration)
					{
						string text = xattribute.Name.LocalName;
						if (text == "xmlns")
						{
							text = string.Empty;
						}
						this.namespaceManager.AddNamespace(text, xattribute.Value);
					}
					else
					{
						XName name = xattribute.Name;
						if (name == this.xsiTypeName)
						{
							xsiType = xattribute.Value;
						}
						else if (name == this.xsiNilName)
						{
							xsiNil = xattribute.Value;
						}
					}
				}
				while (xattribute != e.lastAttr);
			}
		}

		// Token: 0x06000061 RID: 97 RVA: 0x000039C1 File Offset: 0x00001BC1
		private IXmlLineInfo SaveLineInfo(XObject source)
		{
			IXmlLineInfo lineInfoProvider = this.validator.LineInfoProvider;
			this.validator.LineInfoProvider = source;
			return lineInfoProvider;
		}

		// Token: 0x06000062 RID: 98 RVA: 0x000039DA File Offset: 0x00001BDA
		private void RestoreLineInfo(IXmlLineInfo originalLineInfo)
		{
			this.validator.LineInfoProvider = originalLineInfo;
		}

		// Token: 0x06000063 RID: 99 RVA: 0x000039E8 File Offset: 0x00001BE8
		private void ValidateAttribute(XAttribute a)
		{
			IXmlLineInfo originalLineInfo = this.SaveLineInfo(a);
			XmlSchemaInfo schemaInfo = this.addSchemaInfo ? new XmlSchemaInfo() : null;
			this.source = a;
			this.validator.ValidateAttribute(a.Name.LocalName, a.Name.NamespaceName, a.Value, schemaInfo);
			if (this.addSchemaInfo)
			{
				this.ReplaceSchemaInfo(a, schemaInfo);
			}
			this.RestoreLineInfo(originalLineInfo);
		}

		// Token: 0x06000064 RID: 100 RVA: 0x00003A58 File Offset: 0x00001C58
		private void ValidateAttributes(XElement e)
		{
			XAttribute xattribute = e.lastAttr;
			IXmlLineInfo originalLineInfo = this.SaveLineInfo(xattribute);
			if (xattribute != null)
			{
				do
				{
					xattribute = xattribute.next;
					if (!xattribute.IsNamespaceDeclaration)
					{
						this.ValidateAttribute(xattribute);
					}
				}
				while (xattribute != e.lastAttr);
				this.source = e;
			}
			if (this.addSchemaInfo)
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
				foreach (object obj in this.defaultAttributes)
				{
					XmlSchemaAttribute xmlSchemaAttribute = (XmlSchemaAttribute)obj;
					xattribute = new XAttribute(XNamespace.Get(xmlSchemaAttribute.QualifiedName.Namespace).GetName(xmlSchemaAttribute.QualifiedName.Name), this.GetDefaultValue(xmlSchemaAttribute));
					this.ReplaceSchemaInfo(xattribute, this.GetDefaultAttributeSchemaInfo(xmlSchemaAttribute));
					e.Add(xattribute);
				}
			}
			this.RestoreLineInfo(originalLineInfo);
		}

		// Token: 0x06000065 RID: 101 RVA: 0x00003B6C File Offset: 0x00001D6C
		private void ValidateElement(XElement e)
		{
			XmlSchemaInfo xmlSchemaInfo = this.addSchemaInfo ? new XmlSchemaInfo() : null;
			string xsiType = null;
			string xsiNil = null;
			this.PushElement(e, ref xsiType, ref xsiNil);
			IXmlLineInfo originalLineInfo = this.SaveLineInfo(e);
			this.source = e;
			this.validator.ValidateElement(e.Name.LocalName, e.Name.NamespaceName, xmlSchemaInfo, xsiType, xsiNil, null, null);
			this.ValidateAttributes(e);
			this.validator.ValidateEndOfAttributes(xmlSchemaInfo);
			this.ValidateNodes(e);
			this.validator.ValidateEndElement(xmlSchemaInfo);
			if (this.addSchemaInfo)
			{
				if (xmlSchemaInfo.Validity == XmlSchemaValidity.Valid && xmlSchemaInfo.IsDefault)
				{
					e.Value = this.GetDefaultValue(xmlSchemaInfo.SchemaElement);
				}
				this.ReplaceSchemaInfo(e, xmlSchemaInfo);
			}
			this.RestoreLineInfo(originalLineInfo);
			this.namespaceManager.PopScope();
		}

		// Token: 0x06000066 RID: 102 RVA: 0x00003C3C File Offset: 0x00001E3C
		private void ValidateNodes(XElement e)
		{
			XNode xnode = e.content as XNode;
			IXmlLineInfo originalLineInfo = this.SaveLineInfo(xnode);
			if (xnode != null)
			{
				do
				{
					xnode = xnode.next;
					XElement xelement = xnode as XElement;
					if (xelement != null)
					{
						this.ValidateElement(xelement);
					}
					else
					{
						XText xtext = xnode as XText;
						if (xtext != null)
						{
							string value = xtext.Value;
							if (value.Length > 0)
							{
								this.validator.LineInfoProvider = xtext;
								this.validator.ValidateText(value);
							}
						}
					}
				}
				while (xnode != e.content);
				this.source = e;
			}
			else
			{
				string text = e.content as string;
				if (text != null && text.Length > 0)
				{
					this.validator.ValidateText(text);
				}
			}
			this.RestoreLineInfo(originalLineInfo);
		}

		// Token: 0x06000067 RID: 103 RVA: 0x00003CF0 File Offset: 0x00001EF0
		private void ValidationCallback(object sender, ValidationEventArgs e)
		{
			if (this.validationEventHandler != null)
			{
				this.validationEventHandler(this.source, e);
				return;
			}
			if (e.Severity == XmlSeverityType.Error)
			{
				throw e.Exception;
			}
		}

		// Token: 0x04000034 RID: 52
		private XmlSchemaSet schemas;

		// Token: 0x04000035 RID: 53
		private ValidationEventHandler validationEventHandler;

		// Token: 0x04000036 RID: 54
		private XObject source;

		// Token: 0x04000037 RID: 55
		private bool addSchemaInfo;

		// Token: 0x04000038 RID: 56
		private XmlNamespaceManager namespaceManager;

		// Token: 0x04000039 RID: 57
		private XmlSchemaValidator validator;

		// Token: 0x0400003A RID: 58
		private Dictionary<XmlSchemaInfo, XmlSchemaInfo> schemaInfos;

		// Token: 0x0400003B RID: 59
		private ArrayList defaultAttributes;

		// Token: 0x0400003C RID: 60
		private XName xsiTypeName;

		// Token: 0x0400003D RID: 61
		private XName xsiNilName;
	}
}

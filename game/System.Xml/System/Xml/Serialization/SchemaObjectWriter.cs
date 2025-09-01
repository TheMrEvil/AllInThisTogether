using System;
using System.Collections;
using System.Text;
using System.Xml.Schema;

namespace System.Xml.Serialization
{
	// Token: 0x020002A9 RID: 681
	internal class SchemaObjectWriter
	{
		// Token: 0x06001987 RID: 6535 RVA: 0x00091F40 File Offset: 0x00090140
		private void WriteIndent()
		{
			for (int i = 0; i < this.indentLevel; i++)
			{
				this.w.Append(" ");
			}
		}

		// Token: 0x06001988 RID: 6536 RVA: 0x00091F70 File Offset: 0x00090170
		protected void WriteAttribute(string localName, string ns, string value)
		{
			if (value == null || value.Length == 0)
			{
				return;
			}
			this.w.Append(",");
			this.w.Append(ns);
			if (ns != null && ns.Length != 0)
			{
				this.w.Append(":");
			}
			this.w.Append(localName);
			this.w.Append("=");
			this.w.Append(value);
		}

		// Token: 0x06001989 RID: 6537 RVA: 0x00091FEE File Offset: 0x000901EE
		protected void WriteAttribute(string localName, string ns, XmlQualifiedName value)
		{
			if (value.IsEmpty)
			{
				return;
			}
			this.WriteAttribute(localName, ns, value.ToString());
		}

		// Token: 0x0600198A RID: 6538 RVA: 0x00092007 File Offset: 0x00090207
		protected void WriteStartElement(string name)
		{
			this.NewLine();
			this.indentLevel++;
			this.w.Append("[");
			this.w.Append(name);
		}

		// Token: 0x0600198B RID: 6539 RVA: 0x0009203B File Offset: 0x0009023B
		protected void WriteEndElement()
		{
			this.w.Append("]");
			this.indentLevel--;
		}

		// Token: 0x0600198C RID: 6540 RVA: 0x0009205C File Offset: 0x0009025C
		protected void NewLine()
		{
			this.w.Append(Environment.NewLine);
			this.WriteIndent();
		}

		// Token: 0x0600198D RID: 6541 RVA: 0x00092075 File Offset: 0x00090275
		protected string GetString()
		{
			return this.w.ToString();
		}

		// Token: 0x0600198E RID: 6542 RVA: 0x00092082 File Offset: 0x00090282
		private void WriteAttribute(XmlAttribute a)
		{
			if (a.Value != null)
			{
				this.WriteAttribute(a.Name, a.NamespaceURI, a.Value);
			}
		}

		// Token: 0x0600198F RID: 6543 RVA: 0x000920A4 File Offset: 0x000902A4
		private void WriteAttributes(XmlAttribute[] a, XmlSchemaObject o)
		{
			if (a == null)
			{
				return;
			}
			ArrayList arrayList = new ArrayList();
			for (int i = 0; i < a.Length; i++)
			{
				arrayList.Add(a[i]);
			}
			arrayList.Sort(new XmlAttributeComparer());
			for (int j = 0; j < arrayList.Count; j++)
			{
				XmlAttribute a2 = (XmlAttribute)arrayList[j];
				this.WriteAttribute(a2);
			}
		}

		// Token: 0x06001990 RID: 6544 RVA: 0x00092104 File Offset: 0x00090304
		internal static string ToString(NamespaceList list)
		{
			if (list == null)
			{
				return null;
			}
			switch (list.Type)
			{
			case NamespaceList.ListType.Any:
				return "##any";
			case NamespaceList.ListType.Other:
				return "##other";
			case NamespaceList.ListType.Set:
			{
				ArrayList arrayList = new ArrayList();
				foreach (object obj in list.Enumerate)
				{
					string value = (string)obj;
					arrayList.Add(value);
				}
				arrayList.Sort();
				StringBuilder stringBuilder = new StringBuilder();
				bool flag = true;
				foreach (object obj2 in arrayList)
				{
					string text = (string)obj2;
					if (flag)
					{
						flag = false;
					}
					else
					{
						stringBuilder.Append(" ");
					}
					if (text.Length == 0)
					{
						stringBuilder.Append("##local");
					}
					else
					{
						stringBuilder.Append(text);
					}
				}
				return stringBuilder.ToString();
			}
			default:
				return list.ToString();
			}
		}

		// Token: 0x06001991 RID: 6545 RVA: 0x00092230 File Offset: 0x00090430
		internal string WriteXmlSchemaObject(XmlSchemaObject o)
		{
			if (o == null)
			{
				return string.Empty;
			}
			this.Write3_XmlSchemaObject(o);
			return this.GetString();
		}

		// Token: 0x06001992 RID: 6546 RVA: 0x00092248 File Offset: 0x00090448
		private void WriteSortedItems(XmlSchemaObjectCollection items)
		{
			if (items == null)
			{
				return;
			}
			ArrayList arrayList = new ArrayList();
			for (int i = 0; i < items.Count; i++)
			{
				arrayList.Add(items[i]);
			}
			arrayList.Sort(new XmlSchemaObjectComparer());
			for (int j = 0; j < arrayList.Count; j++)
			{
				this.Write3_XmlSchemaObject((XmlSchemaObject)arrayList[j]);
			}
		}

		// Token: 0x06001993 RID: 6547 RVA: 0x000922AC File Offset: 0x000904AC
		private void Write1_XmlSchemaAttribute(XmlSchemaAttribute o)
		{
			if (o == null)
			{
				return;
			}
			this.WriteStartElement("attribute");
			this.WriteAttribute("id", "", o.Id);
			this.WriteAttributes(o.UnhandledAttributes, o);
			this.WriteAttribute("default", "", o.DefaultValue);
			this.WriteAttribute("fixed", "", o.FixedValue);
			if (o.Parent != null && !(o.Parent is XmlSchema))
			{
				if (o.QualifiedName != null && !o.QualifiedName.IsEmpty && o.QualifiedName.Namespace != null && o.QualifiedName.Namespace.Length != 0)
				{
					this.WriteAttribute("form", "", "qualified");
				}
				else
				{
					this.WriteAttribute("form", "", "unqualified");
				}
			}
			this.WriteAttribute("name", "", o.Name);
			if (!o.RefName.IsEmpty)
			{
				this.WriteAttribute("ref", "", o.RefName);
			}
			else if (!o.SchemaTypeName.IsEmpty)
			{
				this.WriteAttribute("type", "", o.SchemaTypeName);
			}
			XmlSchemaUse v = (o.Use == XmlSchemaUse.None) ? XmlSchemaUse.Optional : o.Use;
			this.WriteAttribute("use", "", this.Write30_XmlSchemaUse(v));
			this.Write5_XmlSchemaAnnotation(o.Annotation);
			this.Write9_XmlSchemaSimpleType(o.SchemaType);
			this.WriteEndElement();
		}

		// Token: 0x06001994 RID: 6548 RVA: 0x00092438 File Offset: 0x00090638
		private void Write3_XmlSchemaObject(XmlSchemaObject o)
		{
			if (o == null)
			{
				return;
			}
			Type type = o.GetType();
			if (type == typeof(XmlSchemaComplexType))
			{
				this.Write35_XmlSchemaComplexType((XmlSchemaComplexType)o);
				return;
			}
			if (type == typeof(XmlSchemaSimpleType))
			{
				this.Write9_XmlSchemaSimpleType((XmlSchemaSimpleType)o);
				return;
			}
			if (type == typeof(XmlSchemaElement))
			{
				this.Write46_XmlSchemaElement((XmlSchemaElement)o);
				return;
			}
			if (type == typeof(XmlSchemaAppInfo))
			{
				this.Write7_XmlSchemaAppInfo((XmlSchemaAppInfo)o);
				return;
			}
			if (type == typeof(XmlSchemaDocumentation))
			{
				this.Write6_XmlSchemaDocumentation((XmlSchemaDocumentation)o);
				return;
			}
			if (type == typeof(XmlSchemaAnnotation))
			{
				this.Write5_XmlSchemaAnnotation((XmlSchemaAnnotation)o);
				return;
			}
			if (type == typeof(XmlSchemaGroup))
			{
				this.Write57_XmlSchemaGroup((XmlSchemaGroup)o);
				return;
			}
			if (type == typeof(XmlSchemaXPath))
			{
				this.Write49_XmlSchemaXPath("xpath", "", (XmlSchemaXPath)o);
				return;
			}
			if (type == typeof(XmlSchemaIdentityConstraint))
			{
				this.Write48_XmlSchemaIdentityConstraint((XmlSchemaIdentityConstraint)o);
				return;
			}
			if (type == typeof(XmlSchemaUnique))
			{
				this.Write51_XmlSchemaUnique((XmlSchemaUnique)o);
				return;
			}
			if (type == typeof(XmlSchemaKeyref))
			{
				this.Write50_XmlSchemaKeyref((XmlSchemaKeyref)o);
				return;
			}
			if (type == typeof(XmlSchemaKey))
			{
				this.Write47_XmlSchemaKey((XmlSchemaKey)o);
				return;
			}
			if (type == typeof(XmlSchemaGroupRef))
			{
				this.Write55_XmlSchemaGroupRef((XmlSchemaGroupRef)o);
				return;
			}
			if (type == typeof(XmlSchemaAny))
			{
				this.Write53_XmlSchemaAny((XmlSchemaAny)o);
				return;
			}
			if (type == typeof(XmlSchemaSequence))
			{
				this.Write54_XmlSchemaSequence((XmlSchemaSequence)o);
				return;
			}
			if (type == typeof(XmlSchemaChoice))
			{
				this.Write52_XmlSchemaChoice((XmlSchemaChoice)o);
				return;
			}
			if (type == typeof(XmlSchemaAll))
			{
				this.Write43_XmlSchemaAll((XmlSchemaAll)o);
				return;
			}
			if (type == typeof(XmlSchemaComplexContentRestriction))
			{
				this.Write56_XmlSchemaComplexContentRestriction((XmlSchemaComplexContentRestriction)o);
				return;
			}
			if (type == typeof(XmlSchemaComplexContentExtension))
			{
				this.Write42_XmlSchemaComplexContentExtension((XmlSchemaComplexContentExtension)o);
				return;
			}
			if (type == typeof(XmlSchemaSimpleContentRestriction))
			{
				this.Write40_XmlSchemaSimpleContentRestriction((XmlSchemaSimpleContentRestriction)o);
				return;
			}
			if (type == typeof(XmlSchemaSimpleContentExtension))
			{
				this.Write38_XmlSchemaSimpleContentExtension((XmlSchemaSimpleContentExtension)o);
				return;
			}
			if (type == typeof(XmlSchemaComplexContent))
			{
				this.Write41_XmlSchemaComplexContent((XmlSchemaComplexContent)o);
				return;
			}
			if (type == typeof(XmlSchemaSimpleContent))
			{
				this.Write36_XmlSchemaSimpleContent((XmlSchemaSimpleContent)o);
				return;
			}
			if (type == typeof(XmlSchemaAnyAttribute))
			{
				this.Write33_XmlSchemaAnyAttribute((XmlSchemaAnyAttribute)o);
				return;
			}
			if (type == typeof(XmlSchemaAttributeGroupRef))
			{
				this.Write32_XmlSchemaAttributeGroupRef((XmlSchemaAttributeGroupRef)o);
				return;
			}
			if (type == typeof(XmlSchemaAttributeGroup))
			{
				this.Write31_XmlSchemaAttributeGroup((XmlSchemaAttributeGroup)o);
				return;
			}
			if (type == typeof(XmlSchemaSimpleTypeRestriction))
			{
				this.Write15_XmlSchemaSimpleTypeRestriction((XmlSchemaSimpleTypeRestriction)o);
				return;
			}
			if (type == typeof(XmlSchemaSimpleTypeList))
			{
				this.Write14_XmlSchemaSimpleTypeList((XmlSchemaSimpleTypeList)o);
				return;
			}
			if (type == typeof(XmlSchemaSimpleTypeUnion))
			{
				this.Write12_XmlSchemaSimpleTypeUnion((XmlSchemaSimpleTypeUnion)o);
				return;
			}
			if (type == typeof(XmlSchemaAttribute))
			{
				this.Write1_XmlSchemaAttribute((XmlSchemaAttribute)o);
				return;
			}
		}

		// Token: 0x06001995 RID: 6549 RVA: 0x000927FC File Offset: 0x000909FC
		private void Write5_XmlSchemaAnnotation(XmlSchemaAnnotation o)
		{
			if (o == null)
			{
				return;
			}
			this.WriteStartElement("annotation");
			this.WriteAttribute("id", "", o.Id);
			this.WriteAttributes(o.UnhandledAttributes, o);
			XmlSchemaObjectCollection items = o.Items;
			if (items != null)
			{
				for (int i = 0; i < items.Count; i++)
				{
					XmlSchemaObject xmlSchemaObject = items[i];
					if (xmlSchemaObject is XmlSchemaAppInfo)
					{
						this.Write7_XmlSchemaAppInfo((XmlSchemaAppInfo)xmlSchemaObject);
					}
					else if (xmlSchemaObject is XmlSchemaDocumentation)
					{
						this.Write6_XmlSchemaDocumentation((XmlSchemaDocumentation)xmlSchemaObject);
					}
				}
			}
			this.WriteEndElement();
		}

		// Token: 0x06001996 RID: 6550 RVA: 0x00092890 File Offset: 0x00090A90
		private void Write6_XmlSchemaDocumentation(XmlSchemaDocumentation o)
		{
			if (o == null)
			{
				return;
			}
			this.WriteStartElement("documentation");
			this.WriteAttribute("source", "", o.Source);
			this.WriteAttribute("lang", "http://www.w3.org/XML/1998/namespace", o.Language);
			XmlNode[] markup = o.Markup;
			if (markup != null)
			{
				foreach (XmlNode xmlNode in markup)
				{
					this.WriteStartElement("node");
					this.WriteAttribute("xml", "", xmlNode.OuterXml);
				}
			}
			this.WriteEndElement();
		}

		// Token: 0x06001997 RID: 6551 RVA: 0x0009291C File Offset: 0x00090B1C
		private void Write7_XmlSchemaAppInfo(XmlSchemaAppInfo o)
		{
			if (o == null)
			{
				return;
			}
			this.WriteStartElement("appinfo");
			this.WriteAttribute("source", "", o.Source);
			XmlNode[] markup = o.Markup;
			if (markup != null)
			{
				foreach (XmlNode xmlNode in markup)
				{
					this.WriteStartElement("node");
					this.WriteAttribute("xml", "", xmlNode.OuterXml);
				}
			}
			this.WriteEndElement();
		}

		// Token: 0x06001998 RID: 6552 RVA: 0x00092994 File Offset: 0x00090B94
		private void Write9_XmlSchemaSimpleType(XmlSchemaSimpleType o)
		{
			if (o == null)
			{
				return;
			}
			this.WriteStartElement("simpleType");
			this.WriteAttribute("id", "", o.Id);
			this.WriteAttributes(o.UnhandledAttributes, o);
			this.WriteAttribute("name", "", o.Name);
			this.WriteAttribute("final", "", this.Write11_XmlSchemaDerivationMethod(o.FinalResolved));
			this.Write5_XmlSchemaAnnotation(o.Annotation);
			if (o.Content is XmlSchemaSimpleTypeUnion)
			{
				this.Write12_XmlSchemaSimpleTypeUnion((XmlSchemaSimpleTypeUnion)o.Content);
			}
			else if (o.Content is XmlSchemaSimpleTypeRestriction)
			{
				this.Write15_XmlSchemaSimpleTypeRestriction((XmlSchemaSimpleTypeRestriction)o.Content);
			}
			else if (o.Content is XmlSchemaSimpleTypeList)
			{
				this.Write14_XmlSchemaSimpleTypeList((XmlSchemaSimpleTypeList)o.Content);
			}
			this.WriteEndElement();
		}

		// Token: 0x06001999 RID: 6553 RVA: 0x00092A75 File Offset: 0x00090C75
		private string Write11_XmlSchemaDerivationMethod(XmlSchemaDerivationMethod v)
		{
			return v.ToString();
		}

		// Token: 0x0600199A RID: 6554 RVA: 0x00092A84 File Offset: 0x00090C84
		private void Write12_XmlSchemaSimpleTypeUnion(XmlSchemaSimpleTypeUnion o)
		{
			if (o == null)
			{
				return;
			}
			this.WriteStartElement("union");
			this.WriteAttribute("id", "", o.Id);
			this.WriteAttributes(o.UnhandledAttributes, o);
			if (o.MemberTypes != null)
			{
				ArrayList arrayList = new ArrayList();
				for (int i = 0; i < o.MemberTypes.Length; i++)
				{
					arrayList.Add(o.MemberTypes[i]);
				}
				arrayList.Sort(new QNameComparer());
				this.w.Append(",");
				this.w.Append("memberTypes=");
				for (int j = 0; j < arrayList.Count; j++)
				{
					XmlQualifiedName xmlQualifiedName = (XmlQualifiedName)arrayList[j];
					this.w.Append(xmlQualifiedName.ToString());
					this.w.Append(",");
				}
			}
			this.Write5_XmlSchemaAnnotation(o.Annotation);
			this.WriteSortedItems(o.BaseTypes);
			this.WriteEndElement();
		}

		// Token: 0x0600199B RID: 6555 RVA: 0x00092B84 File Offset: 0x00090D84
		private void Write14_XmlSchemaSimpleTypeList(XmlSchemaSimpleTypeList o)
		{
			if (o == null)
			{
				return;
			}
			this.WriteStartElement("list");
			this.WriteAttribute("id", "", o.Id);
			this.WriteAttributes(o.UnhandledAttributes, o);
			if (!o.ItemTypeName.IsEmpty)
			{
				this.WriteAttribute("itemType", "", o.ItemTypeName);
			}
			this.Write5_XmlSchemaAnnotation(o.Annotation);
			this.Write9_XmlSchemaSimpleType(o.ItemType);
			this.WriteEndElement();
		}

		// Token: 0x0600199C RID: 6556 RVA: 0x00092C04 File Offset: 0x00090E04
		private void Write15_XmlSchemaSimpleTypeRestriction(XmlSchemaSimpleTypeRestriction o)
		{
			if (o == null)
			{
				return;
			}
			this.WriteStartElement("restriction");
			this.WriteAttribute("id", "", o.Id);
			this.WriteAttributes(o.UnhandledAttributes, o);
			if (!o.BaseTypeName.IsEmpty)
			{
				this.WriteAttribute("base", "", o.BaseTypeName);
			}
			this.Write5_XmlSchemaAnnotation(o.Annotation);
			this.Write9_XmlSchemaSimpleType(o.BaseType);
			this.WriteFacets(o.Facets);
			this.WriteEndElement();
		}

		// Token: 0x0600199D RID: 6557 RVA: 0x00092C90 File Offset: 0x00090E90
		private void WriteFacets(XmlSchemaObjectCollection facets)
		{
			if (facets == null)
			{
				return;
			}
			ArrayList arrayList = new ArrayList();
			for (int i = 0; i < facets.Count; i++)
			{
				arrayList.Add(facets[i]);
			}
			arrayList.Sort(new XmlFacetComparer());
			for (int j = 0; j < arrayList.Count; j++)
			{
				XmlSchemaObject xmlSchemaObject = (XmlSchemaObject)arrayList[j];
				if (xmlSchemaObject is XmlSchemaMinExclusiveFacet)
				{
					this.Write_XmlSchemaFacet("minExclusive", (XmlSchemaFacet)xmlSchemaObject);
				}
				else if (xmlSchemaObject is XmlSchemaMaxInclusiveFacet)
				{
					this.Write_XmlSchemaFacet("maxInclusive", (XmlSchemaFacet)xmlSchemaObject);
				}
				else if (xmlSchemaObject is XmlSchemaMaxExclusiveFacet)
				{
					this.Write_XmlSchemaFacet("maxExclusive", (XmlSchemaFacet)xmlSchemaObject);
				}
				else if (xmlSchemaObject is XmlSchemaMinInclusiveFacet)
				{
					this.Write_XmlSchemaFacet("minInclusive", (XmlSchemaFacet)xmlSchemaObject);
				}
				else if (xmlSchemaObject is XmlSchemaLengthFacet)
				{
					this.Write_XmlSchemaFacet("length", (XmlSchemaFacet)xmlSchemaObject);
				}
				else if (xmlSchemaObject is XmlSchemaEnumerationFacet)
				{
					this.Write_XmlSchemaFacet("enumeration", (XmlSchemaFacet)xmlSchemaObject);
				}
				else if (xmlSchemaObject is XmlSchemaMinLengthFacet)
				{
					this.Write_XmlSchemaFacet("minLength", (XmlSchemaFacet)xmlSchemaObject);
				}
				else if (xmlSchemaObject is XmlSchemaPatternFacet)
				{
					this.Write_XmlSchemaFacet("pattern", (XmlSchemaFacet)xmlSchemaObject);
				}
				else if (xmlSchemaObject is XmlSchemaTotalDigitsFacet)
				{
					this.Write_XmlSchemaFacet("totalDigits", (XmlSchemaFacet)xmlSchemaObject);
				}
				else if (xmlSchemaObject is XmlSchemaMaxLengthFacet)
				{
					this.Write_XmlSchemaFacet("maxLength", (XmlSchemaFacet)xmlSchemaObject);
				}
				else if (xmlSchemaObject is XmlSchemaWhiteSpaceFacet)
				{
					this.Write_XmlSchemaFacet("whiteSpace", (XmlSchemaFacet)xmlSchemaObject);
				}
				else if (xmlSchemaObject is XmlSchemaFractionDigitsFacet)
				{
					this.Write_XmlSchemaFacet("fractionDigit", (XmlSchemaFacet)xmlSchemaObject);
				}
			}
		}

		// Token: 0x0600199E RID: 6558 RVA: 0x00092E4C File Offset: 0x0009104C
		private void Write_XmlSchemaFacet(string name, XmlSchemaFacet o)
		{
			if (o == null)
			{
				return;
			}
			this.WriteStartElement(name);
			this.WriteAttribute("id", "", o.Id);
			this.WriteAttribute("value", "", o.Value);
			if (o.IsFixed)
			{
				this.WriteAttribute("fixed", "", XmlConvert.ToString(o.IsFixed));
			}
			this.WriteAttributes(o.UnhandledAttributes, o);
			this.Write5_XmlSchemaAnnotation(o.Annotation);
			this.WriteEndElement();
		}

		// Token: 0x0600199F RID: 6559 RVA: 0x00092ED4 File Offset: 0x000910D4
		private string Write30_XmlSchemaUse(XmlSchemaUse v)
		{
			string result = null;
			switch (v)
			{
			case XmlSchemaUse.Optional:
				result = "optional";
				break;
			case XmlSchemaUse.Prohibited:
				result = "prohibited";
				break;
			case XmlSchemaUse.Required:
				result = "required";
				break;
			}
			return result;
		}

		// Token: 0x060019A0 RID: 6560 RVA: 0x00092F10 File Offset: 0x00091110
		private void Write31_XmlSchemaAttributeGroup(XmlSchemaAttributeGroup o)
		{
			if (o == null)
			{
				return;
			}
			this.WriteStartElement("attributeGroup");
			this.WriteAttribute("id", "", o.Id);
			this.WriteAttribute("name", "", o.Name);
			this.WriteAttributes(o.UnhandledAttributes, o);
			this.Write5_XmlSchemaAnnotation(o.Annotation);
			this.WriteSortedItems(o.Attributes);
			this.Write33_XmlSchemaAnyAttribute(o.AnyAttribute);
			this.WriteEndElement();
		}

		// Token: 0x060019A1 RID: 6561 RVA: 0x00092F90 File Offset: 0x00091190
		private void Write32_XmlSchemaAttributeGroupRef(XmlSchemaAttributeGroupRef o)
		{
			if (o == null)
			{
				return;
			}
			this.WriteStartElement("attributeGroup");
			this.WriteAttribute("id", "", o.Id);
			if (!o.RefName.IsEmpty)
			{
				this.WriteAttribute("ref", "", o.RefName);
			}
			this.WriteAttributes(o.UnhandledAttributes, o);
			this.Write5_XmlSchemaAnnotation(o.Annotation);
			this.WriteEndElement();
		}

		// Token: 0x060019A2 RID: 6562 RVA: 0x00093004 File Offset: 0x00091204
		private void Write33_XmlSchemaAnyAttribute(XmlSchemaAnyAttribute o)
		{
			if (o == null)
			{
				return;
			}
			this.WriteStartElement("anyAttribute");
			this.WriteAttribute("id", "", o.Id);
			this.WriteAttribute("namespace", "", SchemaObjectWriter.ToString(o.NamespaceList));
			XmlSchemaContentProcessing v = (o.ProcessContents == XmlSchemaContentProcessing.None) ? XmlSchemaContentProcessing.Strict : o.ProcessContents;
			this.WriteAttribute("processContents", "", this.Write34_XmlSchemaContentProcessing(v));
			this.WriteAttributes(o.UnhandledAttributes, o);
			this.Write5_XmlSchemaAnnotation(o.Annotation);
			this.WriteEndElement();
		}

		// Token: 0x060019A3 RID: 6563 RVA: 0x0009309C File Offset: 0x0009129C
		private string Write34_XmlSchemaContentProcessing(XmlSchemaContentProcessing v)
		{
			string result = null;
			switch (v)
			{
			case XmlSchemaContentProcessing.Skip:
				result = "skip";
				break;
			case XmlSchemaContentProcessing.Lax:
				result = "lax";
				break;
			case XmlSchemaContentProcessing.Strict:
				result = "strict";
				break;
			}
			return result;
		}

		// Token: 0x060019A4 RID: 6564 RVA: 0x000930D8 File Offset: 0x000912D8
		private void Write35_XmlSchemaComplexType(XmlSchemaComplexType o)
		{
			if (o == null)
			{
				return;
			}
			this.WriteStartElement("complexType");
			this.WriteAttribute("id", "", o.Id);
			this.WriteAttribute("name", "", o.Name);
			this.WriteAttribute("final", "", this.Write11_XmlSchemaDerivationMethod(o.FinalResolved));
			if (o.IsAbstract)
			{
				this.WriteAttribute("abstract", "", XmlConvert.ToString(o.IsAbstract));
			}
			this.WriteAttribute("block", "", this.Write11_XmlSchemaDerivationMethod(o.BlockResolved));
			if (o.IsMixed)
			{
				this.WriteAttribute("mixed", "", XmlConvert.ToString(o.IsMixed));
			}
			this.WriteAttributes(o.UnhandledAttributes, o);
			this.Write5_XmlSchemaAnnotation(o.Annotation);
			if (o.ContentModel is XmlSchemaComplexContent)
			{
				this.Write41_XmlSchemaComplexContent((XmlSchemaComplexContent)o.ContentModel);
			}
			else if (o.ContentModel is XmlSchemaSimpleContent)
			{
				this.Write36_XmlSchemaSimpleContent((XmlSchemaSimpleContent)o.ContentModel);
			}
			if (o.Particle is XmlSchemaSequence)
			{
				this.Write54_XmlSchemaSequence((XmlSchemaSequence)o.Particle);
			}
			else if (o.Particle is XmlSchemaGroupRef)
			{
				this.Write55_XmlSchemaGroupRef((XmlSchemaGroupRef)o.Particle);
			}
			else if (o.Particle is XmlSchemaChoice)
			{
				this.Write52_XmlSchemaChoice((XmlSchemaChoice)o.Particle);
			}
			else if (o.Particle is XmlSchemaAll)
			{
				this.Write43_XmlSchemaAll((XmlSchemaAll)o.Particle);
			}
			this.WriteSortedItems(o.Attributes);
			this.Write33_XmlSchemaAnyAttribute(o.AnyAttribute);
			this.WriteEndElement();
		}

		// Token: 0x060019A5 RID: 6565 RVA: 0x00093294 File Offset: 0x00091494
		private void Write36_XmlSchemaSimpleContent(XmlSchemaSimpleContent o)
		{
			if (o == null)
			{
				return;
			}
			this.WriteStartElement("simpleContent");
			this.WriteAttribute("id", "", o.Id);
			this.WriteAttributes(o.UnhandledAttributes, o);
			this.Write5_XmlSchemaAnnotation(o.Annotation);
			if (o.Content is XmlSchemaSimpleContentRestriction)
			{
				this.Write40_XmlSchemaSimpleContentRestriction((XmlSchemaSimpleContentRestriction)o.Content);
			}
			else if (o.Content is XmlSchemaSimpleContentExtension)
			{
				this.Write38_XmlSchemaSimpleContentExtension((XmlSchemaSimpleContentExtension)o.Content);
			}
			this.WriteEndElement();
		}

		// Token: 0x060019A6 RID: 6566 RVA: 0x00093324 File Offset: 0x00091524
		private void Write38_XmlSchemaSimpleContentExtension(XmlSchemaSimpleContentExtension o)
		{
			if (o == null)
			{
				return;
			}
			this.WriteStartElement("extension");
			this.WriteAttribute("id", "", o.Id);
			this.WriteAttributes(o.UnhandledAttributes, o);
			if (!o.BaseTypeName.IsEmpty)
			{
				this.WriteAttribute("base", "", o.BaseTypeName);
			}
			this.Write5_XmlSchemaAnnotation(o.Annotation);
			this.WriteSortedItems(o.Attributes);
			this.Write33_XmlSchemaAnyAttribute(o.AnyAttribute);
			this.WriteEndElement();
		}

		// Token: 0x060019A7 RID: 6567 RVA: 0x000933B0 File Offset: 0x000915B0
		private void Write40_XmlSchemaSimpleContentRestriction(XmlSchemaSimpleContentRestriction o)
		{
			if (o == null)
			{
				return;
			}
			this.WriteStartElement("restriction");
			this.WriteAttribute("id", "", o.Id);
			this.WriteAttributes(o.UnhandledAttributes, o);
			if (!o.BaseTypeName.IsEmpty)
			{
				this.WriteAttribute("base", "", o.BaseTypeName);
			}
			this.Write5_XmlSchemaAnnotation(o.Annotation);
			this.Write9_XmlSchemaSimpleType(o.BaseType);
			this.WriteFacets(o.Facets);
			this.WriteSortedItems(o.Attributes);
			this.Write33_XmlSchemaAnyAttribute(o.AnyAttribute);
			this.WriteEndElement();
		}

		// Token: 0x060019A8 RID: 6568 RVA: 0x00093454 File Offset: 0x00091654
		private void Write41_XmlSchemaComplexContent(XmlSchemaComplexContent o)
		{
			if (o == null)
			{
				return;
			}
			this.WriteStartElement("complexContent");
			this.WriteAttribute("id", "", o.Id);
			this.WriteAttribute("mixed", "", XmlConvert.ToString(o.IsMixed));
			this.WriteAttributes(o.UnhandledAttributes, o);
			this.Write5_XmlSchemaAnnotation(o.Annotation);
			if (o.Content is XmlSchemaComplexContentRestriction)
			{
				this.Write56_XmlSchemaComplexContentRestriction((XmlSchemaComplexContentRestriction)o.Content);
			}
			else if (o.Content is XmlSchemaComplexContentExtension)
			{
				this.Write42_XmlSchemaComplexContentExtension((XmlSchemaComplexContentExtension)o.Content);
			}
			this.WriteEndElement();
		}

		// Token: 0x060019A9 RID: 6569 RVA: 0x00093500 File Offset: 0x00091700
		private void Write42_XmlSchemaComplexContentExtension(XmlSchemaComplexContentExtension o)
		{
			if (o == null)
			{
				return;
			}
			this.WriteStartElement("extension");
			this.WriteAttribute("id", "", o.Id);
			this.WriteAttributes(o.UnhandledAttributes, o);
			if (!o.BaseTypeName.IsEmpty)
			{
				this.WriteAttribute("base", "", o.BaseTypeName);
			}
			this.Write5_XmlSchemaAnnotation(o.Annotation);
			if (o.Particle is XmlSchemaSequence)
			{
				this.Write54_XmlSchemaSequence((XmlSchemaSequence)o.Particle);
			}
			else if (o.Particle is XmlSchemaGroupRef)
			{
				this.Write55_XmlSchemaGroupRef((XmlSchemaGroupRef)o.Particle);
			}
			else if (o.Particle is XmlSchemaChoice)
			{
				this.Write52_XmlSchemaChoice((XmlSchemaChoice)o.Particle);
			}
			else if (o.Particle is XmlSchemaAll)
			{
				this.Write43_XmlSchemaAll((XmlSchemaAll)o.Particle);
			}
			this.WriteSortedItems(o.Attributes);
			this.Write33_XmlSchemaAnyAttribute(o.AnyAttribute);
			this.WriteEndElement();
		}

		// Token: 0x060019AA RID: 6570 RVA: 0x0009360C File Offset: 0x0009180C
		private void Write43_XmlSchemaAll(XmlSchemaAll o)
		{
			if (o == null)
			{
				return;
			}
			this.WriteStartElement("all");
			this.WriteAttribute("id", "", o.Id);
			this.WriteAttribute("minOccurs", "", XmlConvert.ToString(o.MinOccurs));
			this.WriteAttribute("maxOccurs", "", (o.MaxOccurs == decimal.MaxValue) ? "unbounded" : XmlConvert.ToString(o.MaxOccurs));
			this.WriteAttributes(o.UnhandledAttributes, o);
			this.Write5_XmlSchemaAnnotation(o.Annotation);
			this.WriteSortedItems(o.Items);
			this.WriteEndElement();
		}

		// Token: 0x060019AB RID: 6571 RVA: 0x000936C0 File Offset: 0x000918C0
		private void Write46_XmlSchemaElement(XmlSchemaElement o)
		{
			if (o == null)
			{
				return;
			}
			o.GetType();
			this.WriteStartElement("element");
			this.WriteAttribute("id", "", o.Id);
			this.WriteAttribute("minOccurs", "", XmlConvert.ToString(o.MinOccurs));
			this.WriteAttribute("maxOccurs", "", (o.MaxOccurs == decimal.MaxValue) ? "unbounded" : XmlConvert.ToString(o.MaxOccurs));
			if (o.IsAbstract)
			{
				this.WriteAttribute("abstract", "", XmlConvert.ToString(o.IsAbstract));
			}
			this.WriteAttribute("block", "", this.Write11_XmlSchemaDerivationMethod(o.BlockResolved));
			this.WriteAttribute("default", "", o.DefaultValue);
			this.WriteAttribute("final", "", this.Write11_XmlSchemaDerivationMethod(o.FinalResolved));
			this.WriteAttribute("fixed", "", o.FixedValue);
			if (o.Parent != null && !(o.Parent is XmlSchema))
			{
				if (o.QualifiedName != null && !o.QualifiedName.IsEmpty && o.QualifiedName.Namespace != null && o.QualifiedName.Namespace.Length != 0)
				{
					this.WriteAttribute("form", "", "qualified");
				}
				else
				{
					this.WriteAttribute("form", "", "unqualified");
				}
			}
			if (o.Name != null && o.Name.Length != 0)
			{
				this.WriteAttribute("name", "", o.Name);
			}
			if (o.IsNillable)
			{
				this.WriteAttribute("nillable", "", XmlConvert.ToString(o.IsNillable));
			}
			if (!o.SubstitutionGroup.IsEmpty)
			{
				this.WriteAttribute("substitutionGroup", "", o.SubstitutionGroup);
			}
			if (!o.RefName.IsEmpty)
			{
				this.WriteAttribute("ref", "", o.RefName);
			}
			else if (!o.SchemaTypeName.IsEmpty)
			{
				this.WriteAttribute("type", "", o.SchemaTypeName);
			}
			this.WriteAttributes(o.UnhandledAttributes, o);
			this.Write5_XmlSchemaAnnotation(o.Annotation);
			if (o.SchemaType is XmlSchemaComplexType)
			{
				this.Write35_XmlSchemaComplexType((XmlSchemaComplexType)o.SchemaType);
			}
			else if (o.SchemaType is XmlSchemaSimpleType)
			{
				this.Write9_XmlSchemaSimpleType((XmlSchemaSimpleType)o.SchemaType);
			}
			this.WriteSortedItems(o.Constraints);
			this.WriteEndElement();
		}

		// Token: 0x060019AC RID: 6572 RVA: 0x00093974 File Offset: 0x00091B74
		private void Write47_XmlSchemaKey(XmlSchemaKey o)
		{
			if (o == null)
			{
				return;
			}
			o.GetType();
			this.WriteStartElement("key");
			this.WriteAttribute("id", "", o.Id);
			this.WriteAttribute("name", "", o.Name);
			this.WriteAttributes(o.UnhandledAttributes, o);
			this.Write5_XmlSchemaAnnotation(o.Annotation);
			this.Write49_XmlSchemaXPath("selector", "", o.Selector);
			XmlSchemaObjectCollection fields = o.Fields;
			if (fields != null)
			{
				for (int i = 0; i < fields.Count; i++)
				{
					this.Write49_XmlSchemaXPath("field", "", (XmlSchemaXPath)fields[i]);
				}
			}
			this.WriteEndElement();
		}

		// Token: 0x060019AD RID: 6573 RVA: 0x00093A30 File Offset: 0x00091C30
		private void Write48_XmlSchemaIdentityConstraint(XmlSchemaIdentityConstraint o)
		{
			if (o == null)
			{
				return;
			}
			Type type = o.GetType();
			if (type == typeof(XmlSchemaUnique))
			{
				this.Write51_XmlSchemaUnique((XmlSchemaUnique)o);
				return;
			}
			if (type == typeof(XmlSchemaKeyref))
			{
				this.Write50_XmlSchemaKeyref((XmlSchemaKeyref)o);
				return;
			}
			if (type == typeof(XmlSchemaKey))
			{
				this.Write47_XmlSchemaKey((XmlSchemaKey)o);
				return;
			}
		}

		// Token: 0x060019AE RID: 6574 RVA: 0x00093AA8 File Offset: 0x00091CA8
		private void Write49_XmlSchemaXPath(string name, string ns, XmlSchemaXPath o)
		{
			if (o == null)
			{
				return;
			}
			this.WriteStartElement(name);
			this.WriteAttribute("id", "", o.Id);
			this.WriteAttribute("xpath", "", o.XPath);
			this.WriteAttributes(o.UnhandledAttributes, o);
			this.Write5_XmlSchemaAnnotation(o.Annotation);
			this.WriteEndElement();
		}

		// Token: 0x060019AF RID: 6575 RVA: 0x00093B0C File Offset: 0x00091D0C
		private void Write50_XmlSchemaKeyref(XmlSchemaKeyref o)
		{
			if (o == null)
			{
				return;
			}
			o.GetType();
			this.WriteStartElement("keyref");
			this.WriteAttribute("id", "", o.Id);
			this.WriteAttribute("name", "", o.Name);
			this.WriteAttributes(o.UnhandledAttributes, o);
			this.WriteAttribute("refer", "", o.Refer);
			this.Write5_XmlSchemaAnnotation(o.Annotation);
			this.Write49_XmlSchemaXPath("selector", "", o.Selector);
			XmlSchemaObjectCollection fields = o.Fields;
			if (fields != null)
			{
				for (int i = 0; i < fields.Count; i++)
				{
					this.Write49_XmlSchemaXPath("field", "", (XmlSchemaXPath)fields[i]);
				}
			}
			this.WriteEndElement();
		}

		// Token: 0x060019B0 RID: 6576 RVA: 0x00093BE0 File Offset: 0x00091DE0
		private void Write51_XmlSchemaUnique(XmlSchemaUnique o)
		{
			if (o == null)
			{
				return;
			}
			o.GetType();
			this.WriteStartElement("unique");
			this.WriteAttribute("id", "", o.Id);
			this.WriteAttribute("name", "", o.Name);
			this.WriteAttributes(o.UnhandledAttributes, o);
			this.Write5_XmlSchemaAnnotation(o.Annotation);
			this.Write49_XmlSchemaXPath("selector", "", o.Selector);
			XmlSchemaObjectCollection fields = o.Fields;
			if (fields != null)
			{
				for (int i = 0; i < fields.Count; i++)
				{
					this.Write49_XmlSchemaXPath("field", "", (XmlSchemaXPath)fields[i]);
				}
			}
			this.WriteEndElement();
		}

		// Token: 0x060019B1 RID: 6577 RVA: 0x00093C9C File Offset: 0x00091E9C
		private void Write52_XmlSchemaChoice(XmlSchemaChoice o)
		{
			if (o == null)
			{
				return;
			}
			o.GetType();
			this.WriteStartElement("choice");
			this.WriteAttribute("id", "", o.Id);
			this.WriteAttribute("minOccurs", "", XmlConvert.ToString(o.MinOccurs));
			this.WriteAttribute("maxOccurs", "", (o.MaxOccurs == decimal.MaxValue) ? "unbounded" : XmlConvert.ToString(o.MaxOccurs));
			this.WriteAttributes(o.UnhandledAttributes, o);
			this.Write5_XmlSchemaAnnotation(o.Annotation);
			this.WriteSortedItems(o.Items);
			this.WriteEndElement();
		}

		// Token: 0x060019B2 RID: 6578 RVA: 0x00093D54 File Offset: 0x00091F54
		private void Write53_XmlSchemaAny(XmlSchemaAny o)
		{
			if (o == null)
			{
				return;
			}
			this.WriteStartElement("any");
			this.WriteAttribute("id", "", o.Id);
			this.WriteAttribute("minOccurs", "", XmlConvert.ToString(o.MinOccurs));
			this.WriteAttribute("maxOccurs", "", (o.MaxOccurs == decimal.MaxValue) ? "unbounded" : XmlConvert.ToString(o.MaxOccurs));
			this.WriteAttribute("namespace", "", SchemaObjectWriter.ToString(o.NamespaceList));
			XmlSchemaContentProcessing v = (o.ProcessContents == XmlSchemaContentProcessing.None) ? XmlSchemaContentProcessing.Strict : o.ProcessContents;
			this.WriteAttribute("processContents", "", this.Write34_XmlSchemaContentProcessing(v));
			this.WriteAttributes(o.UnhandledAttributes, o);
			this.Write5_XmlSchemaAnnotation(o.Annotation);
			this.WriteEndElement();
		}

		// Token: 0x060019B3 RID: 6579 RVA: 0x00093E40 File Offset: 0x00092040
		private void Write54_XmlSchemaSequence(XmlSchemaSequence o)
		{
			if (o == null)
			{
				return;
			}
			this.WriteStartElement("sequence");
			this.WriteAttribute("id", "", o.Id);
			this.WriteAttribute("minOccurs", "", XmlConvert.ToString(o.MinOccurs));
			this.WriteAttribute("maxOccurs", "", (o.MaxOccurs == decimal.MaxValue) ? "unbounded" : XmlConvert.ToString(o.MaxOccurs));
			this.WriteAttributes(o.UnhandledAttributes, o);
			this.Write5_XmlSchemaAnnotation(o.Annotation);
			XmlSchemaObjectCollection items = o.Items;
			if (items != null)
			{
				for (int i = 0; i < items.Count; i++)
				{
					XmlSchemaObject xmlSchemaObject = items[i];
					if (xmlSchemaObject is XmlSchemaAny)
					{
						this.Write53_XmlSchemaAny((XmlSchemaAny)xmlSchemaObject);
					}
					else if (xmlSchemaObject is XmlSchemaSequence)
					{
						this.Write54_XmlSchemaSequence((XmlSchemaSequence)xmlSchemaObject);
					}
					else if (xmlSchemaObject is XmlSchemaChoice)
					{
						this.Write52_XmlSchemaChoice((XmlSchemaChoice)xmlSchemaObject);
					}
					else if (xmlSchemaObject is XmlSchemaElement)
					{
						this.Write46_XmlSchemaElement((XmlSchemaElement)xmlSchemaObject);
					}
					else if (xmlSchemaObject is XmlSchemaGroupRef)
					{
						this.Write55_XmlSchemaGroupRef((XmlSchemaGroupRef)xmlSchemaObject);
					}
				}
			}
			this.WriteEndElement();
		}

		// Token: 0x060019B4 RID: 6580 RVA: 0x00093F7C File Offset: 0x0009217C
		private void Write55_XmlSchemaGroupRef(XmlSchemaGroupRef o)
		{
			if (o == null)
			{
				return;
			}
			this.WriteStartElement("group");
			this.WriteAttribute("id", "", o.Id);
			this.WriteAttribute("minOccurs", "", XmlConvert.ToString(o.MinOccurs));
			this.WriteAttribute("maxOccurs", "", (o.MaxOccurs == decimal.MaxValue) ? "unbounded" : XmlConvert.ToString(o.MaxOccurs));
			if (!o.RefName.IsEmpty)
			{
				this.WriteAttribute("ref", "", o.RefName);
			}
			this.WriteAttributes(o.UnhandledAttributes, o);
			this.Write5_XmlSchemaAnnotation(o.Annotation);
			this.WriteEndElement();
		}

		// Token: 0x060019B5 RID: 6581 RVA: 0x00094044 File Offset: 0x00092244
		private void Write56_XmlSchemaComplexContentRestriction(XmlSchemaComplexContentRestriction o)
		{
			if (o == null)
			{
				return;
			}
			this.WriteStartElement("restriction");
			this.WriteAttribute("id", "", o.Id);
			this.WriteAttributes(o.UnhandledAttributes, o);
			if (!o.BaseTypeName.IsEmpty)
			{
				this.WriteAttribute("base", "", o.BaseTypeName);
			}
			this.Write5_XmlSchemaAnnotation(o.Annotation);
			if (o.Particle is XmlSchemaSequence)
			{
				this.Write54_XmlSchemaSequence((XmlSchemaSequence)o.Particle);
			}
			else if (o.Particle is XmlSchemaGroupRef)
			{
				this.Write55_XmlSchemaGroupRef((XmlSchemaGroupRef)o.Particle);
			}
			else if (o.Particle is XmlSchemaChoice)
			{
				this.Write52_XmlSchemaChoice((XmlSchemaChoice)o.Particle);
			}
			else if (o.Particle is XmlSchemaAll)
			{
				this.Write43_XmlSchemaAll((XmlSchemaAll)o.Particle);
			}
			this.WriteSortedItems(o.Attributes);
			this.Write33_XmlSchemaAnyAttribute(o.AnyAttribute);
			this.WriteEndElement();
		}

		// Token: 0x060019B6 RID: 6582 RVA: 0x00094150 File Offset: 0x00092350
		private void Write57_XmlSchemaGroup(XmlSchemaGroup o)
		{
			if (o == null)
			{
				return;
			}
			this.WriteStartElement("group");
			this.WriteAttribute("id", "", o.Id);
			this.WriteAttribute("name", "", o.Name);
			this.WriteAttributes(o.UnhandledAttributes, o);
			this.Write5_XmlSchemaAnnotation(o.Annotation);
			if (o.Particle is XmlSchemaSequence)
			{
				this.Write54_XmlSchemaSequence((XmlSchemaSequence)o.Particle);
			}
			else if (o.Particle is XmlSchemaChoice)
			{
				this.Write52_XmlSchemaChoice((XmlSchemaChoice)o.Particle);
			}
			else if (o.Particle is XmlSchemaAll)
			{
				this.Write43_XmlSchemaAll((XmlSchemaAll)o.Particle);
			}
			this.WriteEndElement();
		}

		// Token: 0x060019B7 RID: 6583 RVA: 0x00094215 File Offset: 0x00092415
		public SchemaObjectWriter()
		{
		}

		// Token: 0x0400193C RID: 6460
		private StringBuilder w = new StringBuilder();

		// Token: 0x0400193D RID: 6461
		private int indentLevel = -1;
	}
}

using System;
using System.Collections;
using System.Xml.Schema;

namespace System.Xml.Serialization
{
	// Token: 0x0200027F RID: 639
	internal class SchemaGraph
	{
		// Token: 0x06001828 RID: 6184 RVA: 0x0008DC9C File Offset: 0x0008BE9C
		internal SchemaGraph(Hashtable scope, XmlSchemas schemas)
		{
			this.scope = scope;
			schemas.Compile(null, false);
			this.schemas = schemas;
			this.items = 0;
			foreach (object obj in schemas)
			{
				XmlSchema xmlSchema = (XmlSchema)obj;
				this.items += xmlSchema.Items.Count;
				foreach (XmlSchemaObject item in xmlSchema.Items)
				{
					this.Depends(item);
				}
			}
		}

		// Token: 0x06001829 RID: 6185 RVA: 0x0008DD7C File Offset: 0x0008BF7C
		internal ArrayList GetItems()
		{
			return new ArrayList(this.scope.Keys);
		}

		// Token: 0x0600182A RID: 6186 RVA: 0x0008DD90 File Offset: 0x0008BF90
		internal void AddRef(ArrayList list, XmlSchemaObject o)
		{
			if (o == null)
			{
				return;
			}
			if (this.schemas.IsReference(o))
			{
				return;
			}
			if (o.Parent is XmlSchema)
			{
				if (((XmlSchema)o.Parent).TargetNamespace == "http://www.w3.org/2001/XMLSchema")
				{
					return;
				}
				if (list.Contains(o))
				{
					return;
				}
				list.Add(o);
			}
		}

		// Token: 0x0600182B RID: 6187 RVA: 0x0008DDEC File Offset: 0x0008BFEC
		internal ArrayList Depends(XmlSchemaObject item)
		{
			if (!(item.Parent is XmlSchema))
			{
				return this.empty;
			}
			if (this.scope[item] != null)
			{
				return (ArrayList)this.scope[item];
			}
			ArrayList arrayList = new ArrayList();
			this.Depends(item, arrayList);
			this.scope.Add(item, arrayList);
			return arrayList;
		}

		// Token: 0x0600182C RID: 6188 RVA: 0x0008DE4C File Offset: 0x0008C04C
		internal void Depends(XmlSchemaObject item, ArrayList refs)
		{
			if (item == null || this.scope[item] != null)
			{
				return;
			}
			Type type = item.GetType();
			if (typeof(XmlSchemaType).IsAssignableFrom(type))
			{
				XmlQualifiedName xmlQualifiedName = XmlQualifiedName.Empty;
				XmlSchemaType xmlSchemaType = null;
				XmlSchemaParticle xmlSchemaParticle = null;
				XmlSchemaObjectCollection xmlSchemaObjectCollection = null;
				if (item is XmlSchemaComplexType)
				{
					XmlSchemaComplexType xmlSchemaComplexType = (XmlSchemaComplexType)item;
					if (xmlSchemaComplexType.ContentModel != null)
					{
						XmlSchemaContent content = xmlSchemaComplexType.ContentModel.Content;
						if (content is XmlSchemaComplexContentRestriction)
						{
							xmlQualifiedName = ((XmlSchemaComplexContentRestriction)content).BaseTypeName;
							xmlSchemaObjectCollection = ((XmlSchemaComplexContentRestriction)content).Attributes;
						}
						else if (content is XmlSchemaSimpleContentRestriction)
						{
							XmlSchemaSimpleContentRestriction xmlSchemaSimpleContentRestriction = (XmlSchemaSimpleContentRestriction)content;
							if (xmlSchemaSimpleContentRestriction.BaseType != null)
							{
								xmlSchemaType = xmlSchemaSimpleContentRestriction.BaseType;
							}
							else
							{
								xmlQualifiedName = xmlSchemaSimpleContentRestriction.BaseTypeName;
							}
							xmlSchemaObjectCollection = xmlSchemaSimpleContentRestriction.Attributes;
						}
						else if (content is XmlSchemaComplexContentExtension)
						{
							XmlSchemaComplexContentExtension xmlSchemaComplexContentExtension = (XmlSchemaComplexContentExtension)content;
							xmlSchemaObjectCollection = xmlSchemaComplexContentExtension.Attributes;
							xmlSchemaParticle = xmlSchemaComplexContentExtension.Particle;
							xmlQualifiedName = xmlSchemaComplexContentExtension.BaseTypeName;
						}
						else if (content is XmlSchemaSimpleContentExtension)
						{
							XmlSchemaSimpleContentExtension xmlSchemaSimpleContentExtension = (XmlSchemaSimpleContentExtension)content;
							xmlSchemaObjectCollection = xmlSchemaSimpleContentExtension.Attributes;
							xmlQualifiedName = xmlSchemaSimpleContentExtension.BaseTypeName;
						}
					}
					else
					{
						xmlSchemaObjectCollection = xmlSchemaComplexType.Attributes;
						xmlSchemaParticle = xmlSchemaComplexType.Particle;
					}
					if (xmlSchemaParticle is XmlSchemaGroupRef)
					{
						XmlSchemaGroupRef xmlSchemaGroupRef = (XmlSchemaGroupRef)xmlSchemaParticle;
						xmlSchemaParticle = ((XmlSchemaGroup)this.schemas.Find(xmlSchemaGroupRef.RefName, typeof(XmlSchemaGroup), false)).Particle;
					}
					else if (xmlSchemaParticle is XmlSchemaGroupBase)
					{
						xmlSchemaParticle = (XmlSchemaGroupBase)xmlSchemaParticle;
					}
				}
				else if (item is XmlSchemaSimpleType)
				{
					XmlSchemaSimpleTypeContent content2 = ((XmlSchemaSimpleType)item).Content;
					if (content2 is XmlSchemaSimpleTypeRestriction)
					{
						xmlSchemaType = ((XmlSchemaSimpleTypeRestriction)content2).BaseType;
						xmlQualifiedName = ((XmlSchemaSimpleTypeRestriction)content2).BaseTypeName;
					}
					else if (content2 is XmlSchemaSimpleTypeList)
					{
						XmlSchemaSimpleTypeList xmlSchemaSimpleTypeList = (XmlSchemaSimpleTypeList)content2;
						if (xmlSchemaSimpleTypeList.ItemTypeName != null && !xmlSchemaSimpleTypeList.ItemTypeName.IsEmpty)
						{
							xmlQualifiedName = xmlSchemaSimpleTypeList.ItemTypeName;
						}
						if (xmlSchemaSimpleTypeList.ItemType != null)
						{
							xmlSchemaType = xmlSchemaSimpleTypeList.ItemType;
						}
					}
					else if (content2 is XmlSchemaSimpleTypeRestriction)
					{
						xmlQualifiedName = ((XmlSchemaSimpleTypeRestriction)content2).BaseTypeName;
					}
					else if (type == typeof(XmlSchemaSimpleTypeUnion))
					{
						XmlQualifiedName[] memberTypes = ((XmlSchemaSimpleTypeUnion)item).MemberTypes;
						if (memberTypes != null)
						{
							for (int i = 0; i < memberTypes.Length; i++)
							{
								XmlSchemaType o = (XmlSchemaType)this.schemas.Find(memberTypes[i], typeof(XmlSchemaType), false);
								this.AddRef(refs, o);
							}
						}
					}
				}
				if (xmlSchemaType == null && !xmlQualifiedName.IsEmpty && xmlQualifiedName.Namespace != "http://www.w3.org/2001/XMLSchema")
				{
					xmlSchemaType = (XmlSchemaType)this.schemas.Find(xmlQualifiedName, typeof(XmlSchemaType), false);
				}
				if (xmlSchemaType != null)
				{
					this.AddRef(refs, xmlSchemaType);
				}
				if (xmlSchemaParticle != null)
				{
					this.Depends(xmlSchemaParticle, refs);
				}
				if (xmlSchemaObjectCollection != null)
				{
					for (int j = 0; j < xmlSchemaObjectCollection.Count; j++)
					{
						this.Depends(xmlSchemaObjectCollection[j], refs);
					}
				}
			}
			else if (type == typeof(XmlSchemaElement))
			{
				XmlSchemaElement xmlSchemaElement = (XmlSchemaElement)item;
				if (!xmlSchemaElement.SubstitutionGroup.IsEmpty && xmlSchemaElement.SubstitutionGroup.Namespace != "http://www.w3.org/2001/XMLSchema")
				{
					XmlSchemaElement o2 = (XmlSchemaElement)this.schemas.Find(xmlSchemaElement.SubstitutionGroup, typeof(XmlSchemaElement), false);
					this.AddRef(refs, o2);
				}
				if (!xmlSchemaElement.RefName.IsEmpty)
				{
					xmlSchemaElement = (XmlSchemaElement)this.schemas.Find(xmlSchemaElement.RefName, typeof(XmlSchemaElement), false);
					this.AddRef(refs, xmlSchemaElement);
				}
				else if (!xmlSchemaElement.SchemaTypeName.IsEmpty)
				{
					XmlSchemaType o3 = (XmlSchemaType)this.schemas.Find(xmlSchemaElement.SchemaTypeName, typeof(XmlSchemaType), false);
					this.AddRef(refs, o3);
				}
				else
				{
					this.Depends(xmlSchemaElement.SchemaType, refs);
				}
			}
			else if (type == typeof(XmlSchemaGroup))
			{
				this.Depends(((XmlSchemaGroup)item).Particle);
			}
			else if (type == typeof(XmlSchemaGroupRef))
			{
				XmlSchemaGroup o4 = (XmlSchemaGroup)this.schemas.Find(((XmlSchemaGroupRef)item).RefName, typeof(XmlSchemaGroup), false);
				this.AddRef(refs, o4);
			}
			else
			{
				if (typeof(XmlSchemaGroupBase).IsAssignableFrom(type))
				{
					using (XmlSchemaObjectEnumerator enumerator = ((XmlSchemaGroupBase)item).Items.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							XmlSchemaObject item2 = enumerator.Current;
							this.Depends(item2, refs);
						}
						goto IL_614;
					}
				}
				if (type == typeof(XmlSchemaAttributeGroupRef))
				{
					XmlSchemaAttributeGroup o5 = (XmlSchemaAttributeGroup)this.schemas.Find(((XmlSchemaAttributeGroupRef)item).RefName, typeof(XmlSchemaAttributeGroup), false);
					this.AddRef(refs, o5);
				}
				else
				{
					if (type == typeof(XmlSchemaAttributeGroup))
					{
						using (XmlSchemaObjectEnumerator enumerator = ((XmlSchemaAttributeGroup)item).Attributes.GetEnumerator())
						{
							while (enumerator.MoveNext())
							{
								XmlSchemaObject item3 = enumerator.Current;
								this.Depends(item3, refs);
							}
							goto IL_614;
						}
					}
					if (type == typeof(XmlSchemaAttribute))
					{
						XmlSchemaAttribute xmlSchemaAttribute = (XmlSchemaAttribute)item;
						if (!xmlSchemaAttribute.RefName.IsEmpty)
						{
							xmlSchemaAttribute = (XmlSchemaAttribute)this.schemas.Find(xmlSchemaAttribute.RefName, typeof(XmlSchemaAttribute), false);
							this.AddRef(refs, xmlSchemaAttribute);
						}
						else if (!xmlSchemaAttribute.SchemaTypeName.IsEmpty)
						{
							XmlSchemaType o6 = (XmlSchemaType)this.schemas.Find(xmlSchemaAttribute.SchemaTypeName, typeof(XmlSchemaType), false);
							this.AddRef(refs, o6);
						}
						else
						{
							this.Depends(xmlSchemaAttribute.SchemaType, refs);
						}
					}
				}
			}
			IL_614:
			if (typeof(XmlSchemaAnnotated).IsAssignableFrom(type))
			{
				XmlAttribute[] unhandledAttributes = ((XmlSchemaAnnotated)item).UnhandledAttributes;
				if (unhandledAttributes != null)
				{
					foreach (XmlAttribute xmlAttribute in unhandledAttributes)
					{
						if (xmlAttribute.LocalName == "arrayType" && xmlAttribute.NamespaceURI == "http://schemas.xmlsoap.org/wsdl/")
						{
							string text;
							XmlQualifiedName name = TypeScope.ParseWsdlArrayType(xmlAttribute.Value, out text, item);
							XmlSchemaType o7 = (XmlSchemaType)this.schemas.Find(name, typeof(XmlSchemaType), false);
							this.AddRef(refs, o7);
						}
					}
				}
			}
		}

		// Token: 0x040018AB RID: 6315
		private ArrayList empty = new ArrayList();

		// Token: 0x040018AC RID: 6316
		private XmlSchemas schemas;

		// Token: 0x040018AD RID: 6317
		private Hashtable scope;

		// Token: 0x040018AE RID: 6318
		private int items;
	}
}

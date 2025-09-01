using System;
using System.Collections;
using System.Reflection;
using System.Text;
using System.Xml.Schema;

namespace System.Xml.Serialization
{
	// Token: 0x02000294 RID: 660
	internal class SerializableMapping : SpecialMapping
	{
		// Token: 0x060018EF RID: 6383 RVA: 0x0008F73A File Offset: 0x0008D93A
		internal SerializableMapping()
		{
		}

		// Token: 0x060018F0 RID: 6384 RVA: 0x0008F749 File Offset: 0x0008D949
		internal SerializableMapping(MethodInfo getSchemaMethod, bool any, string ns)
		{
			this.getSchemaMethod = getSchemaMethod;
			this.any = any;
			base.Namespace = ns;
			this.needSchema = (getSchemaMethod != null);
		}

		// Token: 0x060018F1 RID: 6385 RVA: 0x0008F77A File Offset: 0x0008D97A
		internal SerializableMapping(XmlQualifiedName xsiType, XmlSchemaSet schemas)
		{
			this.xsiType = xsiType;
			this.schemas = schemas;
			base.TypeName = xsiType.Name;
			base.Namespace = xsiType.Namespace;
			this.needSchema = false;
		}

		// Token: 0x060018F2 RID: 6386 RVA: 0x0008F7B8 File Offset: 0x0008D9B8
		internal void SetBaseMapping(SerializableMapping mapping)
		{
			this.baseMapping = mapping;
			if (this.baseMapping != null)
			{
				this.nextDerivedMapping = this.baseMapping.derivedMappings;
				this.baseMapping.derivedMappings = this;
				if (this == this.nextDerivedMapping)
				{
					throw new InvalidOperationException(Res.GetString("Circular reference in derivation of IXmlSerializable type '{0}'.", new object[]
					{
						base.TypeDesc.FullName
					}));
				}
			}
		}

		// Token: 0x170004BE RID: 1214
		// (get) Token: 0x060018F3 RID: 6387 RVA: 0x0008F820 File Offset: 0x0008DA20
		internal bool IsAny
		{
			get
			{
				if (this.any)
				{
					return true;
				}
				if (this.getSchemaMethod == null)
				{
					return false;
				}
				if (this.needSchema && typeof(XmlSchemaType).IsAssignableFrom(this.getSchemaMethod.ReturnType))
				{
					return false;
				}
				this.RetrieveSerializableSchema();
				return this.any;
			}
		}

		// Token: 0x170004BF RID: 1215
		// (get) Token: 0x060018F4 RID: 6388 RVA: 0x0008F87C File Offset: 0x0008DA7C
		internal string NamespaceList
		{
			get
			{
				this.RetrieveSerializableSchema();
				if (this.namespaces == null)
				{
					if (this.schemas != null)
					{
						StringBuilder stringBuilder = new StringBuilder();
						foreach (object obj in this.schemas.Schemas())
						{
							XmlSchema xmlSchema = (XmlSchema)obj;
							if (xmlSchema.TargetNamespace != null && xmlSchema.TargetNamespace.Length > 0)
							{
								if (stringBuilder.Length > 0)
								{
									stringBuilder.Append(" ");
								}
								stringBuilder.Append(xmlSchema.TargetNamespace);
							}
						}
						this.namespaces = stringBuilder.ToString();
					}
					else
					{
						this.namespaces = string.Empty;
					}
				}
				return this.namespaces;
			}
		}

		// Token: 0x170004C0 RID: 1216
		// (get) Token: 0x060018F5 RID: 6389 RVA: 0x0008F94C File Offset: 0x0008DB4C
		internal SerializableMapping DerivedMappings
		{
			get
			{
				return this.derivedMappings;
			}
		}

		// Token: 0x170004C1 RID: 1217
		// (get) Token: 0x060018F6 RID: 6390 RVA: 0x0008F954 File Offset: 0x0008DB54
		internal SerializableMapping NextDerivedMapping
		{
			get
			{
				return this.nextDerivedMapping;
			}
		}

		// Token: 0x170004C2 RID: 1218
		// (get) Token: 0x060018F7 RID: 6391 RVA: 0x0008F95C File Offset: 0x0008DB5C
		// (set) Token: 0x060018F8 RID: 6392 RVA: 0x0008F964 File Offset: 0x0008DB64
		internal SerializableMapping Next
		{
			get
			{
				return this.next;
			}
			set
			{
				this.next = value;
			}
		}

		// Token: 0x170004C3 RID: 1219
		// (get) Token: 0x060018F9 RID: 6393 RVA: 0x0008F96D File Offset: 0x0008DB6D
		// (set) Token: 0x060018FA RID: 6394 RVA: 0x0008F975 File Offset: 0x0008DB75
		internal Type Type
		{
			get
			{
				return this.type;
			}
			set
			{
				this.type = value;
			}
		}

		// Token: 0x170004C4 RID: 1220
		// (get) Token: 0x060018FB RID: 6395 RVA: 0x0008F97E File Offset: 0x0008DB7E
		internal XmlSchemaSet Schemas
		{
			get
			{
				this.RetrieveSerializableSchema();
				return this.schemas;
			}
		}

		// Token: 0x170004C5 RID: 1221
		// (get) Token: 0x060018FC RID: 6396 RVA: 0x0008F98C File Offset: 0x0008DB8C
		internal XmlSchema Schema
		{
			get
			{
				this.RetrieveSerializableSchema();
				return this.schema;
			}
		}

		// Token: 0x170004C6 RID: 1222
		// (get) Token: 0x060018FD RID: 6397 RVA: 0x0008F99C File Offset: 0x0008DB9C
		internal XmlQualifiedName XsiType
		{
			get
			{
				if (!this.needSchema)
				{
					return this.xsiType;
				}
				if (this.getSchemaMethod == null)
				{
					return null;
				}
				if (typeof(XmlSchemaType).IsAssignableFrom(this.getSchemaMethod.ReturnType))
				{
					return null;
				}
				this.RetrieveSerializableSchema();
				return this.xsiType;
			}
		}

		// Token: 0x170004C7 RID: 1223
		// (get) Token: 0x060018FE RID: 6398 RVA: 0x0008F9F2 File Offset: 0x0008DBF2
		internal XmlSchemaType XsdType
		{
			get
			{
				this.RetrieveSerializableSchema();
				return this.xsdType;
			}
		}

		// Token: 0x060018FF RID: 6399 RVA: 0x0008FA00 File Offset: 0x0008DC00
		internal static void ValidationCallbackWithErrorCode(object sender, ValidationEventArgs args)
		{
			if (args.Severity == XmlSeverityType.Error)
			{
				throw new InvalidOperationException(Res.GetString("Schema type information provided by {0} is invalid: {1}", new object[]
				{
					typeof(IXmlSerializable).Name,
					args.Message
				}));
			}
		}

		// Token: 0x06001900 RID: 6400 RVA: 0x0008FA3C File Offset: 0x0008DC3C
		internal void CheckDuplicateElement(XmlSchemaElement element, string elementNs)
		{
			if (element == null)
			{
				return;
			}
			if (element.Parent == null || !(element.Parent is XmlSchema))
			{
				return;
			}
			XmlSchemaObjectTable xmlSchemaObjectTable;
			if (this.Schema != null && this.Schema.TargetNamespace == elementNs)
			{
				XmlSchemas.Preprocess(this.Schema);
				xmlSchemaObjectTable = this.Schema.Elements;
			}
			else
			{
				if (this.Schemas == null)
				{
					return;
				}
				xmlSchemaObjectTable = this.Schemas.GlobalElements;
			}
			foreach (object obj in xmlSchemaObjectTable.Values)
			{
				XmlSchemaElement xmlSchemaElement = (XmlSchemaElement)obj;
				if (xmlSchemaElement.Name == element.Name && xmlSchemaElement.QualifiedName.Namespace == elementNs)
				{
					if (this.Match(xmlSchemaElement, element))
					{
						break;
					}
					throw new InvalidOperationException(Res.GetString("Cannot reconcile schema for '{0}'. Please use [XmlRoot] attribute to change default name or namespace of the top-level element to avoid duplicate element declarations: element name='{1}' namespace='{2}'.", new object[]
					{
						this.getSchemaMethod.DeclaringType.FullName,
						xmlSchemaElement.Name,
						elementNs
					}));
				}
			}
		}

		// Token: 0x06001901 RID: 6401 RVA: 0x0008FB60 File Offset: 0x0008DD60
		private bool Match(XmlSchemaElement e1, XmlSchemaElement e2)
		{
			return e1.IsNillable == e2.IsNillable && !(e1.RefName != e2.RefName) && e1.SchemaType == e2.SchemaType && !(e1.SchemaTypeName != e2.SchemaTypeName) && !(e1.MinOccurs != e2.MinOccurs) && !(e1.MaxOccurs != e2.MaxOccurs) && e1.IsAbstract == e2.IsAbstract && !(e1.DefaultValue != e2.DefaultValue) && !(e1.SubstitutionGroup != e2.SubstitutionGroup);
		}

		// Token: 0x06001902 RID: 6402 RVA: 0x0008FC1C File Offset: 0x0008DE1C
		private void RetrieveSerializableSchema()
		{
			if (this.needSchema)
			{
				this.needSchema = false;
				if (this.getSchemaMethod != null)
				{
					if (this.schemas == null)
					{
						this.schemas = new XmlSchemaSet();
					}
					object obj = this.getSchemaMethod.Invoke(null, new object[]
					{
						this.schemas
					});
					this.xsiType = XmlQualifiedName.Empty;
					if (obj != null)
					{
						if (typeof(XmlSchemaType).IsAssignableFrom(this.getSchemaMethod.ReturnType))
						{
							this.xsdType = (XmlSchemaType)obj;
							this.xsiType = this.xsdType.QualifiedName;
						}
						else
						{
							if (!typeof(XmlQualifiedName).IsAssignableFrom(this.getSchemaMethod.ReturnType))
							{
								throw new InvalidOperationException(Res.GetString("Method {0}.{1}() specified by {2} has invalid signature: return type must be compatible with {3}.", new object[]
								{
									this.type.Name,
									this.getSchemaMethod.Name,
									typeof(XmlSchemaProviderAttribute).Name,
									typeof(XmlQualifiedName).FullName
								}));
							}
							this.xsiType = (XmlQualifiedName)obj;
							if (this.xsiType.IsEmpty)
							{
								throw new InvalidOperationException(Res.GetString("{0}.{1}() must return a valid type name.", new object[]
								{
									this.type.FullName,
									this.getSchemaMethod.Name
								}));
							}
						}
					}
					else
					{
						this.any = true;
					}
					this.schemas.ValidationEventHandler += SerializableMapping.ValidationCallbackWithErrorCode;
					this.schemas.Compile();
					if (!this.xsiType.IsEmpty && this.xsiType.Namespace != "http://www.w3.org/2001/XMLSchema")
					{
						ArrayList arrayList = (ArrayList)this.schemas.Schemas(this.xsiType.Namespace);
						if (arrayList.Count == 0)
						{
							throw new InvalidOperationException(Res.GetString("Missing schema targetNamespace=\"{0}\".", new object[]
							{
								this.xsiType.Namespace
							}));
						}
						if (arrayList.Count > 1)
						{
							throw new InvalidOperationException(Res.GetString("Multiple schemas with targetNamespace='{0}' returned by {1}.{2}().  Please use only the main (parent) schema, and add the others to the schema Includes.", new object[]
							{
								this.xsiType.Namespace,
								this.getSchemaMethod.DeclaringType.FullName,
								this.getSchemaMethod.Name
							}));
						}
						XmlSchema xmlSchema = (XmlSchema)arrayList[0];
						if (xmlSchema == null)
						{
							throw new InvalidOperationException(Res.GetString("Missing schema targetNamespace=\"{0}\".", new object[]
							{
								this.xsiType.Namespace
							}));
						}
						this.xsdType = (XmlSchemaType)xmlSchema.SchemaTypes[this.xsiType];
						if (this.xsdType == null)
						{
							throw new InvalidOperationException(Res.GetString("{0}.{1}() must return a valid type name. Type '{2}' cannot be found in the targetNamespace='{3}'.", new object[]
							{
								this.getSchemaMethod.DeclaringType.FullName,
								this.getSchemaMethod.Name,
								this.xsiType.Name,
								this.xsiType.Namespace
							}));
						}
						this.xsdType = ((this.xsdType.Redefined != null) ? this.xsdType.Redefined : this.xsdType);
						return;
					}
				}
				else
				{
					IXmlSerializable xmlSerializable = (IXmlSerializable)Activator.CreateInstance(this.type);
					this.schema = xmlSerializable.GetSchema();
					if (this.schema != null && (this.schema.Id == null || this.schema.Id.Length == 0))
					{
						throw new InvalidOperationException(Res.GetString("Schema Id is missing. The schema returned from {0}.GetSchema() must have an Id.", new object[]
						{
							this.type.FullName
						}));
					}
				}
			}
		}

		// Token: 0x040018F6 RID: 6390
		private XmlSchema schema;

		// Token: 0x040018F7 RID: 6391
		private Type type;

		// Token: 0x040018F8 RID: 6392
		private bool needSchema = true;

		// Token: 0x040018F9 RID: 6393
		private MethodInfo getSchemaMethod;

		// Token: 0x040018FA RID: 6394
		private XmlQualifiedName xsiType;

		// Token: 0x040018FB RID: 6395
		private XmlSchemaType xsdType;

		// Token: 0x040018FC RID: 6396
		private XmlSchemaSet schemas;

		// Token: 0x040018FD RID: 6397
		private bool any;

		// Token: 0x040018FE RID: 6398
		private string namespaces;

		// Token: 0x040018FF RID: 6399
		private SerializableMapping baseMapping;

		// Token: 0x04001900 RID: 6400
		private SerializableMapping derivedMappings;

		// Token: 0x04001901 RID: 6401
		private SerializableMapping nextDerivedMapping;

		// Token: 0x04001902 RID: 6402
		private SerializableMapping next;
	}
}

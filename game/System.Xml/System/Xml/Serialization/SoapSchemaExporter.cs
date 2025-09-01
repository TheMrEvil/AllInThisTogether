using System;
using System.Collections;
using System.Xml.Schema;

namespace System.Xml.Serialization
{
	/// <summary>Populates <see cref="T:System.Xml.Schema.XmlSchema" /> objects with XML Schema data type definitions for .NET Framework types that are serialized using SOAP encoding.</summary>
	// Token: 0x020002B4 RID: 692
	public class SoapSchemaExporter
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Serialization.SoapSchemaExporter" /> class, which supplies the collection of <see cref="T:System.Xml.Schema.XmlSchema" /> objects to which XML Schema element declarations are to be added.</summary>
		/// <param name="schemas">A collection of <see cref="T:System.Xml.Schema.XmlSchema" /> objects to which element declarations obtained from type mappings are to be added.</param>
		// Token: 0x06001A1C RID: 6684 RVA: 0x0009657E File Offset: 0x0009477E
		public SoapSchemaExporter(XmlSchemas schemas)
		{
			this.schemas = schemas;
		}

		/// <summary>Adds to the applicable <see cref="T:System.Xml.Schema.XmlSchema" /> object a data type definition for a .NET Framework type.</summary>
		/// <param name="xmlTypeMapping">An internal mapping between a .NET Framework type and an XML Schema element.</param>
		// Token: 0x06001A1D RID: 6685 RVA: 0x00096598 File Offset: 0x00094798
		public void ExportTypeMapping(XmlTypeMapping xmlTypeMapping)
		{
			this.CheckScope(xmlTypeMapping.Scope);
			this.ExportTypeMapping(xmlTypeMapping.Mapping, null);
		}

		/// <summary>Adds to the applicable <see cref="T:System.Xml.Schema.XmlSchema" /> object a data type definition for each of the element parts of a SOAP-encoded message definition.</summary>
		/// <param name="xmlMembersMapping">Internal .NET Framework type mappings for the element parts of a WSDL message definition.</param>
		// Token: 0x06001A1E RID: 6686 RVA: 0x000965B4 File Offset: 0x000947B4
		public void ExportMembersMapping(XmlMembersMapping xmlMembersMapping)
		{
			this.ExportMembersMapping(xmlMembersMapping, false);
		}

		/// <summary>Adds to the applicable <see cref="T:System.Xml.Schema.XmlSchema" /> object a data type definition for each of the element parts of a SOAP-encoded message definition.</summary>
		/// <param name="xmlMembersMapping">Internal .NET Framework type mappings for the element parts of a WSDL message definition.</param>
		/// <param name="exportEnclosingType">
		///       <see langword="true" /> to export a type definition for the parent element of the WSDL parts; otherwise, <see langword="false" />.</param>
		// Token: 0x06001A1F RID: 6687 RVA: 0x000965C0 File Offset: 0x000947C0
		public void ExportMembersMapping(XmlMembersMapping xmlMembersMapping, bool exportEnclosingType)
		{
			this.CheckScope(xmlMembersMapping.Scope);
			MembersMapping membersMapping = (MembersMapping)xmlMembersMapping.Accessor.Mapping;
			if (exportEnclosingType)
			{
				this.ExportTypeMapping(membersMapping, null);
				return;
			}
			foreach (MemberMapping memberMapping in membersMapping.Members)
			{
				if (memberMapping.Elements.Length != 0)
				{
					this.ExportTypeMapping(memberMapping.Elements[0].Mapping, null);
				}
			}
		}

		// Token: 0x06001A20 RID: 6688 RVA: 0x0009662E File Offset: 0x0009482E
		private void CheckScope(TypeScope scope)
		{
			if (this.scope == null)
			{
				this.scope = scope;
				return;
			}
			if (this.scope != scope)
			{
				throw new InvalidOperationException(Res.GetString("Exported mappings must come from the same importer."));
			}
		}

		// Token: 0x170004FC RID: 1276
		// (get) Token: 0x06001A21 RID: 6689 RVA: 0x00096659 File Offset: 0x00094859
		internal XmlDocument Document
		{
			get
			{
				if (this.document == null)
				{
					this.document = new XmlDocument();
				}
				return this.document;
			}
		}

		// Token: 0x06001A22 RID: 6690 RVA: 0x00096674 File Offset: 0x00094874
		private void CheckForDuplicateType(string newTypeName, string newNamespace)
		{
			XmlSchema xmlSchema = this.schemas[newNamespace];
			if (xmlSchema != null)
			{
				foreach (XmlSchemaObject xmlSchemaObject in xmlSchema.Items)
				{
					XmlSchemaType xmlSchemaType = xmlSchemaObject as XmlSchemaType;
					if (xmlSchemaType != null && xmlSchemaType.Name == newTypeName)
					{
						throw new InvalidOperationException(Res.GetString("A type with the name {0} has already been added in namespace {1}.", new object[]
						{
							newTypeName,
							newNamespace
						}));
					}
				}
			}
		}

		// Token: 0x06001A23 RID: 6691 RVA: 0x00096708 File Offset: 0x00094908
		private void AddSchemaItem(XmlSchemaObject item, string ns, string referencingNs)
		{
			if (!this.SchemaContainsItem(item, ns))
			{
				XmlSchema xmlSchema = this.schemas[ns];
				if (xmlSchema == null)
				{
					xmlSchema = new XmlSchema();
					xmlSchema.TargetNamespace = ((ns == null || ns.Length == 0) ? null : ns);
					xmlSchema.ElementFormDefault = XmlSchemaForm.Qualified;
					this.schemas.Add(xmlSchema);
				}
				xmlSchema.Items.Add(item);
			}
			if (referencingNs != null)
			{
				this.AddSchemaImport(ns, referencingNs);
			}
		}

		// Token: 0x06001A24 RID: 6692 RVA: 0x00096778 File Offset: 0x00094978
		private void AddSchemaImport(string ns, string referencingNs)
		{
			if (referencingNs == null || ns == null)
			{
				return;
			}
			if (ns == referencingNs)
			{
				return;
			}
			XmlSchema xmlSchema = this.schemas[referencingNs];
			if (xmlSchema == null)
			{
				throw new InvalidOperationException(Res.GetString("Missing schema targetNamespace=\"{0}\".", new object[]
				{
					referencingNs
				}));
			}
			if (ns != null && ns.Length > 0 && this.FindImport(xmlSchema, ns) == null)
			{
				XmlSchemaImport xmlSchemaImport = new XmlSchemaImport();
				xmlSchemaImport.Namespace = ns;
				xmlSchema.Includes.Add(xmlSchemaImport);
			}
		}

		// Token: 0x06001A25 RID: 6693 RVA: 0x000967F0 File Offset: 0x000949F0
		private bool SchemaContainsItem(XmlSchemaObject item, string ns)
		{
			XmlSchema xmlSchema = this.schemas[ns];
			return xmlSchema != null && xmlSchema.Items.Contains(item);
		}

		// Token: 0x06001A26 RID: 6694 RVA: 0x0009681C File Offset: 0x00094A1C
		private XmlSchemaImport FindImport(XmlSchema schema, string ns)
		{
			foreach (object obj in schema.Includes)
			{
				if (obj is XmlSchemaImport)
				{
					XmlSchemaImport xmlSchemaImport = (XmlSchemaImport)obj;
					if (xmlSchemaImport.Namespace == ns)
					{
						return xmlSchemaImport;
					}
				}
			}
			return null;
		}

		// Token: 0x06001A27 RID: 6695 RVA: 0x00096890 File Offset: 0x00094A90
		private XmlQualifiedName ExportTypeMapping(TypeMapping mapping, string ns)
		{
			if (mapping is ArrayMapping)
			{
				return this.ExportArrayMapping((ArrayMapping)mapping, ns);
			}
			if (mapping is EnumMapping)
			{
				return this.ExportEnumMapping((EnumMapping)mapping, ns);
			}
			if (mapping is PrimitiveMapping)
			{
				PrimitiveMapping primitiveMapping = (PrimitiveMapping)mapping;
				if (primitiveMapping.TypeDesc.IsXsdType)
				{
					return this.ExportPrimitiveMapping(primitiveMapping);
				}
				return this.ExportNonXsdPrimitiveMapping(primitiveMapping, ns);
			}
			else
			{
				if (mapping is StructMapping)
				{
					return this.ExportStructMapping((StructMapping)mapping, ns);
				}
				if (mapping is NullableMapping)
				{
					return this.ExportTypeMapping(((NullableMapping)mapping).BaseMapping, ns);
				}
				if (mapping is MembersMapping)
				{
					return this.ExportMembersMapping((MembersMapping)mapping, ns);
				}
				throw new ArgumentException(Res.GetString("Internal error."), "mapping");
			}
		}

		// Token: 0x06001A28 RID: 6696 RVA: 0x00096954 File Offset: 0x00094B54
		private XmlQualifiedName ExportNonXsdPrimitiveMapping(PrimitiveMapping mapping, string ns)
		{
			XmlSchemaType dataType = mapping.TypeDesc.DataType;
			if (!this.SchemaContainsItem(dataType, "http://microsoft.com/wsdl/types/"))
			{
				this.AddSchemaItem(dataType, "http://microsoft.com/wsdl/types/", ns);
			}
			else
			{
				this.AddSchemaImport("http://microsoft.com/wsdl/types/", ns);
			}
			return new XmlQualifiedName(mapping.TypeDesc.DataType.Name, "http://microsoft.com/wsdl/types/");
		}

		// Token: 0x06001A29 RID: 6697 RVA: 0x000969B0 File Offset: 0x00094BB0
		private XmlQualifiedName ExportPrimitiveMapping(PrimitiveMapping mapping)
		{
			return new XmlQualifiedName(mapping.TypeDesc.DataType.Name, "http://www.w3.org/2001/XMLSchema");
		}

		// Token: 0x06001A2A RID: 6698 RVA: 0x000969CC File Offset: 0x00094BCC
		private XmlQualifiedName ExportArrayMapping(ArrayMapping mapping, string ns)
		{
			while (mapping.Next != null)
			{
				mapping = mapping.Next;
			}
			if ((XmlSchemaComplexType)this.types[mapping] == null)
			{
				this.CheckForDuplicateType(mapping.TypeName, mapping.Namespace);
				XmlSchemaComplexType xmlSchemaComplexType = new XmlSchemaComplexType();
				xmlSchemaComplexType.Name = mapping.TypeName;
				this.types.Add(mapping, xmlSchemaComplexType);
				this.AddSchemaItem(xmlSchemaComplexType, mapping.Namespace, ns);
				this.AddSchemaImport("http://schemas.xmlsoap.org/soap/encoding/", mapping.Namespace);
				this.AddSchemaImport("http://schemas.xmlsoap.org/wsdl/", mapping.Namespace);
				XmlSchemaComplexContentRestriction xmlSchemaComplexContentRestriction = new XmlSchemaComplexContentRestriction();
				XmlQualifiedName xmlQualifiedName = this.ExportTypeMapping(mapping.Elements[0].Mapping, mapping.Namespace);
				if (xmlQualifiedName.IsEmpty)
				{
					xmlQualifiedName = new XmlQualifiedName("anyType", "http://www.w3.org/2001/XMLSchema");
				}
				XmlSchemaAttribute xmlSchemaAttribute = new XmlSchemaAttribute();
				xmlSchemaAttribute.RefName = SoapSchemaExporter.ArrayTypeQName;
				xmlSchemaAttribute.UnhandledAttributes = new XmlAttribute[]
				{
					new XmlAttribute("wsdl", "arrayType", "http://schemas.xmlsoap.org/wsdl/", this.Document)
					{
						Value = xmlQualifiedName.Namespace + ":" + xmlQualifiedName.Name + "[]"
					}
				};
				xmlSchemaComplexContentRestriction.Attributes.Add(xmlSchemaAttribute);
				xmlSchemaComplexContentRestriction.BaseTypeName = SoapSchemaExporter.ArrayQName;
				xmlSchemaComplexType.ContentModel = new XmlSchemaComplexContent
				{
					Content = xmlSchemaComplexContentRestriction
				};
				if (xmlQualifiedName.Namespace != "http://www.w3.org/2001/XMLSchema")
				{
					this.AddSchemaImport(xmlQualifiedName.Namespace, mapping.Namespace);
				}
			}
			else
			{
				this.AddSchemaImport(mapping.Namespace, ns);
			}
			return new XmlQualifiedName(mapping.TypeName, mapping.Namespace);
		}

		// Token: 0x06001A2B RID: 6699 RVA: 0x00096B70 File Offset: 0x00094D70
		private void ExportElementAccessors(XmlSchemaGroupBase group, ElementAccessor[] accessors, bool repeats, bool valueTypeOptional, string ns)
		{
			if (accessors.Length == 0)
			{
				return;
			}
			if (accessors.Length == 1)
			{
				this.ExportElementAccessor(group, accessors[0], repeats, valueTypeOptional, ns);
				return;
			}
			XmlSchemaChoice xmlSchemaChoice = new XmlSchemaChoice();
			xmlSchemaChoice.MaxOccurs = (repeats ? decimal.MaxValue : 1m);
			xmlSchemaChoice.MinOccurs = (repeats ? 0 : 1);
			for (int i = 0; i < accessors.Length; i++)
			{
				this.ExportElementAccessor(xmlSchemaChoice, accessors[i], false, valueTypeOptional, ns);
			}
			if (xmlSchemaChoice.Items.Count > 0)
			{
				group.Items.Add(xmlSchemaChoice);
			}
		}

		// Token: 0x06001A2C RID: 6700 RVA: 0x00096C04 File Offset: 0x00094E04
		private void ExportElementAccessor(XmlSchemaGroupBase group, ElementAccessor accessor, bool repeats, bool valueTypeOptional, string ns)
		{
			XmlSchemaElement xmlSchemaElement = new XmlSchemaElement();
			xmlSchemaElement.MinOccurs = ((repeats || valueTypeOptional) ? 0 : 1);
			xmlSchemaElement.MaxOccurs = (repeats ? decimal.MaxValue : 1m);
			xmlSchemaElement.Name = accessor.Name;
			xmlSchemaElement.IsNillable = (accessor.IsNullable || accessor.Mapping is NullableMapping);
			xmlSchemaElement.Form = XmlSchemaForm.Unqualified;
			xmlSchemaElement.SchemaTypeName = this.ExportTypeMapping(accessor.Mapping, accessor.Namespace);
			group.Items.Add(xmlSchemaElement);
		}

		// Token: 0x06001A2D RID: 6701 RVA: 0x00096C9D File Offset: 0x00094E9D
		private XmlQualifiedName ExportRootMapping(StructMapping mapping)
		{
			if (!this.exportedRoot)
			{
				this.exportedRoot = true;
				this.ExportDerivedMappings(mapping);
			}
			return new XmlQualifiedName("anyType", "http://www.w3.org/2001/XMLSchema");
		}

		// Token: 0x06001A2E RID: 6702 RVA: 0x00096CC4 File Offset: 0x00094EC4
		private XmlQualifiedName ExportStructMapping(StructMapping mapping, string ns)
		{
			if (mapping.TypeDesc.IsRoot)
			{
				return this.ExportRootMapping(mapping);
			}
			XmlSchemaComplexType xmlSchemaComplexType = (XmlSchemaComplexType)this.types[mapping];
			if (xmlSchemaComplexType == null)
			{
				if (!mapping.IncludeInSchema)
				{
					throw new InvalidOperationException(Res.GetString("The type {0} may not be exported to a schema because the IncludeInSchema property of the SoapType attribute is 'false'.", new object[]
					{
						mapping.TypeDesc.Name
					}));
				}
				this.CheckForDuplicateType(mapping.TypeName, mapping.Namespace);
				xmlSchemaComplexType = new XmlSchemaComplexType();
				xmlSchemaComplexType.Name = mapping.TypeName;
				this.types.Add(mapping, xmlSchemaComplexType);
				this.AddSchemaItem(xmlSchemaComplexType, mapping.Namespace, ns);
				xmlSchemaComplexType.IsAbstract = mapping.TypeDesc.IsAbstract;
				if (mapping.BaseMapping != null && mapping.BaseMapping.IncludeInSchema)
				{
					XmlSchemaComplexContentExtension xmlSchemaComplexContentExtension = new XmlSchemaComplexContentExtension();
					xmlSchemaComplexContentExtension.BaseTypeName = this.ExportStructMapping(mapping.BaseMapping, mapping.Namespace);
					xmlSchemaComplexType.ContentModel = new XmlSchemaComplexContent
					{
						Content = xmlSchemaComplexContentExtension
					};
				}
				this.ExportTypeMembers(xmlSchemaComplexType, mapping.Members, mapping.Namespace);
				this.ExportDerivedMappings(mapping);
			}
			else
			{
				this.AddSchemaImport(mapping.Namespace, ns);
			}
			return new XmlQualifiedName(xmlSchemaComplexType.Name, mapping.Namespace);
		}

		// Token: 0x06001A2F RID: 6703 RVA: 0x00096DFC File Offset: 0x00094FFC
		private XmlQualifiedName ExportMembersMapping(MembersMapping mapping, string ns)
		{
			XmlSchemaComplexType xmlSchemaComplexType = (XmlSchemaComplexType)this.types[mapping];
			if (xmlSchemaComplexType == null)
			{
				this.CheckForDuplicateType(mapping.TypeName, mapping.Namespace);
				xmlSchemaComplexType = new XmlSchemaComplexType();
				xmlSchemaComplexType.Name = mapping.TypeName;
				this.types.Add(mapping, xmlSchemaComplexType);
				this.AddSchemaItem(xmlSchemaComplexType, mapping.Namespace, ns);
				this.ExportTypeMembers(xmlSchemaComplexType, mapping.Members, mapping.Namespace);
			}
			else
			{
				this.AddSchemaImport(mapping.Namespace, ns);
			}
			return new XmlQualifiedName(xmlSchemaComplexType.Name, mapping.Namespace);
		}

		// Token: 0x06001A30 RID: 6704 RVA: 0x00096E90 File Offset: 0x00095090
		private void ExportTypeMembers(XmlSchemaComplexType type, MemberMapping[] members, string ns)
		{
			XmlSchemaGroupBase xmlSchemaGroupBase = new XmlSchemaSequence();
			foreach (MemberMapping memberMapping in members)
			{
				if (memberMapping.Elements.Length != 0)
				{
					bool valueTypeOptional = memberMapping.CheckSpecified != SpecifiedAccessor.None || memberMapping.CheckShouldPersist || !memberMapping.TypeDesc.IsValueType;
					this.ExportElementAccessors(xmlSchemaGroupBase, memberMapping.Elements, false, valueTypeOptional, ns);
				}
			}
			if (xmlSchemaGroupBase.Items.Count > 0)
			{
				if (type.ContentModel != null)
				{
					if (type.ContentModel.Content is XmlSchemaComplexContentExtension)
					{
						((XmlSchemaComplexContentExtension)type.ContentModel.Content).Particle = xmlSchemaGroupBase;
						return;
					}
					if (type.ContentModel.Content is XmlSchemaComplexContentRestriction)
					{
						((XmlSchemaComplexContentRestriction)type.ContentModel.Content).Particle = xmlSchemaGroupBase;
						return;
					}
					throw new InvalidOperationException(Res.GetString("Invalid content {0}.", new object[]
					{
						type.ContentModel.Content.GetType().Name
					}));
				}
				else
				{
					type.Particle = xmlSchemaGroupBase;
				}
			}
		}

		// Token: 0x06001A31 RID: 6705 RVA: 0x00096F94 File Offset: 0x00095194
		private void ExportDerivedMappings(StructMapping mapping)
		{
			for (StructMapping structMapping = mapping.DerivedMappings; structMapping != null; structMapping = structMapping.NextDerivedMapping)
			{
				if (structMapping.IncludeInSchema)
				{
					this.ExportStructMapping(structMapping, mapping.TypeDesc.IsRoot ? null : mapping.Namespace);
				}
			}
		}

		// Token: 0x06001A32 RID: 6706 RVA: 0x00096FDC File Offset: 0x000951DC
		private XmlQualifiedName ExportEnumMapping(EnumMapping mapping, string ns)
		{
			if ((XmlSchemaSimpleType)this.types[mapping] == null)
			{
				this.CheckForDuplicateType(mapping.TypeName, mapping.Namespace);
				XmlSchemaSimpleType xmlSchemaSimpleType = new XmlSchemaSimpleType();
				xmlSchemaSimpleType.Name = mapping.TypeName;
				this.types.Add(mapping, xmlSchemaSimpleType);
				this.AddSchemaItem(xmlSchemaSimpleType, mapping.Namespace, ns);
				XmlSchemaSimpleTypeRestriction xmlSchemaSimpleTypeRestriction = new XmlSchemaSimpleTypeRestriction();
				xmlSchemaSimpleTypeRestriction.BaseTypeName = new XmlQualifiedName("string", "http://www.w3.org/2001/XMLSchema");
				for (int i = 0; i < mapping.Constants.Length; i++)
				{
					ConstantMapping constantMapping = mapping.Constants[i];
					XmlSchemaEnumerationFacet xmlSchemaEnumerationFacet = new XmlSchemaEnumerationFacet();
					xmlSchemaEnumerationFacet.Value = constantMapping.XmlName;
					xmlSchemaSimpleTypeRestriction.Facets.Add(xmlSchemaEnumerationFacet);
				}
				if (!mapping.IsFlags)
				{
					xmlSchemaSimpleType.Content = xmlSchemaSimpleTypeRestriction;
				}
				else
				{
					xmlSchemaSimpleType.Content = new XmlSchemaSimpleTypeList
					{
						ItemType = new XmlSchemaSimpleType
						{
							Content = xmlSchemaSimpleTypeRestriction
						}
					};
				}
			}
			else
			{
				this.AddSchemaImport(mapping.Namespace, ns);
			}
			return new XmlQualifiedName(mapping.TypeName, mapping.Namespace);
		}

		// Token: 0x06001A33 RID: 6707 RVA: 0x000970F1 File Offset: 0x000952F1
		// Note: this type is marked as 'beforefieldinit'.
		static SoapSchemaExporter()
		{
		}

		// Token: 0x04001959 RID: 6489
		internal const XmlSchemaForm elementFormDefault = XmlSchemaForm.Qualified;

		// Token: 0x0400195A RID: 6490
		private XmlSchemas schemas;

		// Token: 0x0400195B RID: 6491
		private Hashtable types = new Hashtable();

		// Token: 0x0400195C RID: 6492
		private bool exportedRoot;

		// Token: 0x0400195D RID: 6493
		private TypeScope scope;

		// Token: 0x0400195E RID: 6494
		private XmlDocument document;

		// Token: 0x0400195F RID: 6495
		private static XmlQualifiedName ArrayQName = new XmlQualifiedName("Array", "http://schemas.xmlsoap.org/soap/encoding/");

		// Token: 0x04001960 RID: 6496
		private static XmlQualifiedName ArrayTypeQName = new XmlQualifiedName("arrayType", "http://schemas.xmlsoap.org/soap/encoding/");
	}
}

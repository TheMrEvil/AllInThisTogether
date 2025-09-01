using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Runtime.Diagnostics;
using System.Runtime.Serialization.Diagnostics;
using System.Security;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace System.Runtime.Serialization
{
	// Token: 0x02000124 RID: 292
	internal class SchemaExporter
	{
		// Token: 0x06000E6E RID: 3694 RVA: 0x000378EA File Offset: 0x00035AEA
		internal SchemaExporter(XmlSchemaSet schemas, DataContractSet dataContractSet)
		{
			this.schemas = schemas;
			this.dataContractSet = dataContractSet;
		}

		// Token: 0x1700032E RID: 814
		// (get) Token: 0x06000E6F RID: 3695 RVA: 0x00037900 File Offset: 0x00035B00
		private XmlSchemaSet Schemas
		{
			get
			{
				return this.schemas;
			}
		}

		// Token: 0x1700032F RID: 815
		// (get) Token: 0x06000E70 RID: 3696 RVA: 0x00037908 File Offset: 0x00035B08
		private XmlDocument XmlDoc
		{
			get
			{
				if (this.xmlDoc == null)
				{
					this.xmlDoc = new XmlDocument();
				}
				return this.xmlDoc;
			}
		}

		// Token: 0x06000E71 RID: 3697 RVA: 0x00037924 File Offset: 0x00035B24
		internal void Export()
		{
			try
			{
				this.ExportSerializationSchema();
				foreach (KeyValuePair<XmlQualifiedName, DataContract> keyValuePair in this.dataContractSet)
				{
					DataContract value = keyValuePair.Value;
					if (!this.dataContractSet.IsContractProcessed(value))
					{
						this.ExportDataContract(value);
						this.dataContractSet.SetContractProcessed(value);
					}
				}
			}
			finally
			{
				this.xmlDoc = null;
				this.dataContractSet = null;
			}
		}

		// Token: 0x06000E72 RID: 3698 RVA: 0x000379B4 File Offset: 0x00035BB4
		private void ExportSerializationSchema()
		{
			if (!this.Schemas.Contains("http://schemas.microsoft.com/2003/10/Serialization/"))
			{
				XmlSchema xmlSchema = XmlSchema.Read(new XmlTextReader(new StringReader("<?xml version='1.0' encoding='utf-8'?>\n<xs:schema elementFormDefault='qualified' attributeFormDefault='qualified' xmlns:tns='http://schemas.microsoft.com/2003/10/Serialization/' targetNamespace='http://schemas.microsoft.com/2003/10/Serialization/' xmlns:xs='http://www.w3.org/2001/XMLSchema'>\n  <xs:element name='anyType' nillable='true' type='xs:anyType' />\n  <xs:element name='anyURI' nillable='true' type='xs:anyURI' />\n  <xs:element name='base64Binary' nillable='true' type='xs:base64Binary' />\n  <xs:element name='boolean' nillable='true' type='xs:boolean' />\n  <xs:element name='byte' nillable='true' type='xs:byte' />\n  <xs:element name='dateTime' nillable='true' type='xs:dateTime' />\n  <xs:element name='decimal' nillable='true' type='xs:decimal' />\n  <xs:element name='double' nillable='true' type='xs:double' />\n  <xs:element name='float' nillable='true' type='xs:float' />\n  <xs:element name='int' nillable='true' type='xs:int' />\n  <xs:element name='long' nillable='true' type='xs:long' />\n  <xs:element name='QName' nillable='true' type='xs:QName' />\n  <xs:element name='short' nillable='true' type='xs:short' />\n  <xs:element name='string' nillable='true' type='xs:string' />\n  <xs:element name='unsignedByte' nillable='true' type='xs:unsignedByte' />\n  <xs:element name='unsignedInt' nillable='true' type='xs:unsignedInt' />\n  <xs:element name='unsignedLong' nillable='true' type='xs:unsignedLong' />\n  <xs:element name='unsignedShort' nillable='true' type='xs:unsignedShort' />\n  <xs:element name='char' nillable='true' type='tns:char' />\n  <xs:simpleType name='char'>\n    <xs:restriction base='xs:int'/>\n  </xs:simpleType>  \n  <xs:element name='duration' nillable='true' type='tns:duration' />\n  <xs:simpleType name='duration'>\n    <xs:restriction base='xs:duration'>\n      <xs:pattern value='\\-?P(\\d*D)?(T(\\d*H)?(\\d*M)?(\\d*(\\.\\d*)?S)?)?' />\n      <xs:minInclusive value='-P10675199DT2H48M5.4775808S' />\n      <xs:maxInclusive value='P10675199DT2H48M5.4775807S' />\n    </xs:restriction>\n  </xs:simpleType>\n  <xs:element name='guid' nillable='true' type='tns:guid' />\n  <xs:simpleType name='guid'>\n    <xs:restriction base='xs:string'>\n      <xs:pattern value='[\\da-fA-F]{8}-[\\da-fA-F]{4}-[\\da-fA-F]{4}-[\\da-fA-F]{4}-[\\da-fA-F]{12}' />\n    </xs:restriction>\n  </xs:simpleType>\n  <xs:attribute name='FactoryType' type='xs:QName' />\n  <xs:attribute name='Id' type='xs:ID' />\n  <xs:attribute name='Ref' type='xs:IDREF' />\n</xs:schema>\n"))
				{
					DtdProcessing = DtdProcessing.Prohibit
				}, null);
				if (xmlSchema == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("Could not read serialization schema for '{0}' namespace.", new object[]
					{
						"http://schemas.microsoft.com/2003/10/Serialization/"
					})));
				}
				this.Schemas.Add(xmlSchema);
			}
		}

		// Token: 0x06000E73 RID: 3699 RVA: 0x00037A24 File Offset: 0x00035C24
		private void ExportDataContract(DataContract dataContract)
		{
			if (dataContract.IsBuiltInDataContract)
			{
				return;
			}
			if (dataContract is XmlDataContract)
			{
				this.ExportXmlDataContract((XmlDataContract)dataContract);
				return;
			}
			XmlSchema schema = this.GetSchema(dataContract.StableName.Namespace);
			if (dataContract is ClassDataContract)
			{
				ClassDataContract classDataContract = (ClassDataContract)dataContract;
				if (classDataContract.IsISerializable)
				{
					this.ExportISerializableDataContract(classDataContract, schema);
				}
				else
				{
					this.ExportClassDataContract(classDataContract, schema);
				}
			}
			else if (dataContract is CollectionDataContract)
			{
				this.ExportCollectionDataContract((CollectionDataContract)dataContract, schema);
			}
			else if (dataContract is EnumDataContract)
			{
				this.ExportEnumDataContract((EnumDataContract)dataContract, schema);
			}
			this.ExportTopLevelElement(dataContract, schema);
			this.Schemas.Reprocess(schema);
		}

		// Token: 0x06000E74 RID: 3700 RVA: 0x00037AD0 File Offset: 0x00035CD0
		private XmlSchemaElement ExportTopLevelElement(DataContract dataContract, XmlSchema schema)
		{
			if (schema == null || dataContract.StableName.Namespace != dataContract.TopLevelElementNamespace.Value)
			{
				schema = this.GetSchema(dataContract.TopLevelElementNamespace.Value);
			}
			XmlSchemaElement xmlSchemaElement = new XmlSchemaElement();
			xmlSchemaElement.Name = dataContract.TopLevelElementName.Value;
			this.SetElementType(xmlSchemaElement, dataContract, schema);
			xmlSchemaElement.IsNillable = true;
			schema.Items.Add(xmlSchemaElement);
			return xmlSchemaElement;
		}

		// Token: 0x06000E75 RID: 3701 RVA: 0x00037B48 File Offset: 0x00035D48
		private void ExportClassDataContract(ClassDataContract classDataContract, XmlSchema schema)
		{
			XmlSchemaComplexType xmlSchemaComplexType = new XmlSchemaComplexType();
			xmlSchemaComplexType.Name = classDataContract.StableName.Name;
			schema.Items.Add(xmlSchemaComplexType);
			XmlElement xmlElement = null;
			if (classDataContract.UnderlyingType.IsGenericType)
			{
				xmlElement = this.ExportGenericInfo(classDataContract.UnderlyingType, "GenericType", "http://schemas.microsoft.com/2003/10/Serialization/");
			}
			XmlSchemaSequence xmlSchemaSequence = new XmlSchemaSequence();
			for (int i = 0; i < classDataContract.Members.Count; i++)
			{
				DataMember dataMember = classDataContract.Members[i];
				XmlSchemaElement xmlSchemaElement = new XmlSchemaElement();
				xmlSchemaElement.Name = dataMember.Name;
				XmlElement xmlElement2 = null;
				DataContract memberTypeDataContract = this.dataContractSet.GetMemberTypeDataContract(dataMember);
				if (this.CheckIfMemberHasConflict(dataMember))
				{
					xmlSchemaElement.SchemaTypeName = SchemaExporter.AnytypeQualifiedName;
					xmlElement2 = this.ExportActualType(memberTypeDataContract.StableName);
					SchemaHelper.AddSchemaImport(memberTypeDataContract.StableName.Namespace, schema);
				}
				else
				{
					this.SetElementType(xmlSchemaElement, memberTypeDataContract, schema);
				}
				SchemaHelper.AddElementForm(xmlSchemaElement, schema);
				if (dataMember.IsNullable)
				{
					xmlSchemaElement.IsNillable = true;
				}
				if (!dataMember.IsRequired)
				{
					xmlSchemaElement.MinOccurs = 0m;
				}
				xmlSchemaElement.Annotation = SchemaExporter.GetSchemaAnnotation(new XmlNode[]
				{
					xmlElement2,
					this.ExportSurrogateData(dataMember),
					this.ExportEmitDefaultValue(dataMember)
				});
				xmlSchemaSequence.Items.Add(xmlSchemaElement);
			}
			XmlElement xmlElement3 = null;
			if (classDataContract.BaseContract != null)
			{
				XmlSchemaComplexContentExtension xmlSchemaComplexContentExtension = this.CreateTypeContent(xmlSchemaComplexType, classDataContract.BaseContract.StableName, schema);
				xmlSchemaComplexContentExtension.Particle = xmlSchemaSequence;
				if (classDataContract.IsReference && !classDataContract.BaseContract.IsReference)
				{
					this.AddReferenceAttributes(xmlSchemaComplexContentExtension.Attributes, schema);
				}
			}
			else
			{
				xmlSchemaComplexType.Particle = xmlSchemaSequence;
				if (classDataContract.IsValueType)
				{
					xmlElement3 = this.GetAnnotationMarkup(SchemaExporter.IsValueTypeName, XmlConvert.ToString(classDataContract.IsValueType), schema);
				}
				if (classDataContract.IsReference)
				{
					this.AddReferenceAttributes(xmlSchemaComplexType.Attributes, schema);
				}
			}
			xmlSchemaComplexType.Annotation = SchemaExporter.GetSchemaAnnotation(new XmlNode[]
			{
				xmlElement,
				this.ExportSurrogateData(classDataContract),
				xmlElement3
			});
		}

		// Token: 0x06000E76 RID: 3702 RVA: 0x00037D53 File Offset: 0x00035F53
		private void AddReferenceAttributes(XmlSchemaObjectCollection attributes, XmlSchema schema)
		{
			SchemaHelper.AddSchemaImport("http://schemas.microsoft.com/2003/10/Serialization/", schema);
			schema.Namespaces.Add("ser", "http://schemas.microsoft.com/2003/10/Serialization/");
			attributes.Add(SchemaExporter.IdAttribute);
			attributes.Add(SchemaExporter.RefAttribute);
		}

		// Token: 0x06000E77 RID: 3703 RVA: 0x00037D90 File Offset: 0x00035F90
		private void SetElementType(XmlSchemaElement element, DataContract dataContract, XmlSchema schema)
		{
			XmlDataContract xmlDataContract = dataContract as XmlDataContract;
			if (xmlDataContract != null && xmlDataContract.IsAnonymous)
			{
				element.SchemaType = xmlDataContract.XsdType;
				return;
			}
			element.SchemaTypeName = dataContract.StableName;
			if (element.SchemaTypeName.Namespace.Equals("http://schemas.microsoft.com/2003/10/Serialization/"))
			{
				schema.Namespaces.Add("ser", "http://schemas.microsoft.com/2003/10/Serialization/");
			}
			SchemaHelper.AddSchemaImport(dataContract.StableName.Namespace, schema);
		}

		// Token: 0x06000E78 RID: 3704 RVA: 0x00037E08 File Offset: 0x00036008
		private bool CheckIfMemberHasConflict(DataMember dataMember)
		{
			if (dataMember.HasConflictingNameAndType)
			{
				return true;
			}
			for (DataMember conflictingMember = dataMember.ConflictingMember; conflictingMember != null; conflictingMember = conflictingMember.ConflictingMember)
			{
				if (conflictingMember.HasConflictingNameAndType)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000E79 RID: 3705 RVA: 0x00037E40 File Offset: 0x00036040
		private XmlElement ExportEmitDefaultValue(DataMember dataMember)
		{
			if (dataMember.EmitDefaultValue)
			{
				return null;
			}
			XmlElement xmlElement = this.XmlDoc.CreateElement(SchemaExporter.DefaultValueAnnotation.Name, SchemaExporter.DefaultValueAnnotation.Namespace);
			XmlAttribute xmlAttribute = this.XmlDoc.CreateAttribute("EmitDefaultValue");
			xmlAttribute.Value = "false";
			xmlElement.Attributes.Append(xmlAttribute);
			return xmlElement;
		}

		// Token: 0x06000E7A RID: 3706 RVA: 0x00037E9F File Offset: 0x0003609F
		private XmlElement ExportActualType(XmlQualifiedName typeName)
		{
			return SchemaExporter.ExportActualType(typeName, this.XmlDoc);
		}

		// Token: 0x06000E7B RID: 3707 RVA: 0x00037EB0 File Offset: 0x000360B0
		private static XmlElement ExportActualType(XmlQualifiedName typeName, XmlDocument xmlDoc)
		{
			XmlElement xmlElement = xmlDoc.CreateElement(SchemaExporter.ActualTypeAnnotationName.Name, SchemaExporter.ActualTypeAnnotationName.Namespace);
			XmlAttribute xmlAttribute = xmlDoc.CreateAttribute("Name");
			xmlAttribute.Value = typeName.Name;
			xmlElement.Attributes.Append(xmlAttribute);
			XmlAttribute xmlAttribute2 = xmlDoc.CreateAttribute("Namespace");
			xmlAttribute2.Value = typeName.Namespace;
			xmlElement.Attributes.Append(xmlAttribute2);
			return xmlElement;
		}

		// Token: 0x06000E7C RID: 3708 RVA: 0x00037F24 File Offset: 0x00036124
		private XmlElement ExportGenericInfo(Type clrType, string elementName, string elementNs)
		{
			int num = 0;
			Type type;
			while (CollectionDataContract.IsCollection(clrType, out type) && DataContract.GetBuiltInDataContract(clrType) == null && !CollectionDataContract.IsCollectionDataContract(clrType))
			{
				clrType = type;
				num++;
			}
			Type[] array = null;
			IList<int> list = null;
			if (clrType.IsGenericType)
			{
				array = clrType.GetGenericArguments();
				string text;
				if (clrType.DeclaringType == null)
				{
					text = clrType.Name;
				}
				else
				{
					int num2 = (clrType.Namespace == null) ? 0 : clrType.Namespace.Length;
					if (num2 > 0)
					{
						num2++;
					}
					text = DataContract.GetClrTypeFullName(clrType).Substring(num2).Replace('+', '.');
				}
				int num3 = text.IndexOf('[');
				if (num3 >= 0)
				{
					text = text.Substring(0, num3);
				}
				list = DataContract.GetDataContractNameForGenericName(text, null);
				clrType = clrType.GetGenericTypeDefinition();
			}
			XmlQualifiedName xmlQualifiedName = DataContract.GetStableName(clrType);
			if (num > 0)
			{
				string text2 = xmlQualifiedName.Name;
				for (int i = 0; i < num; i++)
				{
					text2 = "ArrayOf" + text2;
				}
				xmlQualifiedName = new XmlQualifiedName(text2, DataContract.GetCollectionNamespace(xmlQualifiedName.Namespace));
			}
			XmlElement xmlElement = this.XmlDoc.CreateElement(elementName, elementNs);
			XmlAttribute xmlAttribute = this.XmlDoc.CreateAttribute("Name");
			xmlAttribute.Value = ((array != null) ? XmlConvert.DecodeName(xmlQualifiedName.Name) : xmlQualifiedName.Name);
			xmlElement.Attributes.Append(xmlAttribute);
			XmlAttribute xmlAttribute2 = this.XmlDoc.CreateAttribute("Namespace");
			xmlAttribute2.Value = xmlQualifiedName.Namespace;
			xmlElement.Attributes.Append(xmlAttribute2);
			if (array != null)
			{
				int num4 = 0;
				int num5 = 0;
				foreach (int num6 in list)
				{
					int j = 0;
					while (j < num6)
					{
						XmlElement xmlElement2 = this.ExportGenericInfo(array[num4], "GenericParameter", "http://schemas.microsoft.com/2003/10/Serialization/");
						if (num5 > 0)
						{
							XmlAttribute xmlAttribute3 = this.XmlDoc.CreateAttribute("NestedLevel");
							xmlAttribute3.Value = num5.ToString(CultureInfo.InvariantCulture);
							xmlElement2.Attributes.Append(xmlAttribute3);
						}
						xmlElement.AppendChild(xmlElement2);
						j++;
						num4++;
					}
					num5++;
				}
				if (list[num5 - 1] == 0)
				{
					XmlAttribute xmlAttribute4 = this.XmlDoc.CreateAttribute("NestedLevel");
					xmlAttribute4.Value = list.Count.ToString(CultureInfo.InvariantCulture);
					xmlElement.Attributes.Append(xmlAttribute4);
				}
			}
			return xmlElement;
		}

		// Token: 0x06000E7D RID: 3709 RVA: 0x000381BC File Offset: 0x000363BC
		private XmlElement ExportSurrogateData(object key)
		{
			object surrogateData = this.dataContractSet.GetSurrogateData(key);
			if (surrogateData == null)
			{
				return null;
			}
			StringWriter stringWriter = new StringWriter(CultureInfo.InvariantCulture);
			XmlWriter xmlWriter = XmlWriter.Create(stringWriter, new XmlWriterSettings
			{
				OmitXmlDeclaration = true
			});
			Collection<Type> collection = new Collection<Type>();
			DataContractSurrogateCaller.GetKnownCustomDataTypes(this.dataContractSet.DataContractSurrogate, collection);
			new DataContractSerializer(Globals.TypeOfObject, SchemaExporter.SurrogateDataAnnotationName.Name, SchemaExporter.SurrogateDataAnnotationName.Namespace, collection, int.MaxValue, false, true, null).WriteObject(xmlWriter, surrogateData);
			xmlWriter.Flush();
			return (XmlElement)this.XmlDoc.ReadNode(XmlReader.Create(new StringReader(stringWriter.ToString())));
		}

		// Token: 0x06000E7E RID: 3710 RVA: 0x0003826C File Offset: 0x0003646C
		private void ExportCollectionDataContract(CollectionDataContract collectionDataContract, XmlSchema schema)
		{
			XmlSchemaComplexType xmlSchemaComplexType = new XmlSchemaComplexType();
			xmlSchemaComplexType.Name = collectionDataContract.StableName.Name;
			schema.Items.Add(xmlSchemaComplexType);
			XmlElement xmlElement = null;
			XmlElement xmlElement2 = null;
			if (collectionDataContract.UnderlyingType.IsGenericType && CollectionDataContract.IsCollectionDataContract(collectionDataContract.UnderlyingType))
			{
				xmlElement = this.ExportGenericInfo(collectionDataContract.UnderlyingType, "GenericType", "http://schemas.microsoft.com/2003/10/Serialization/");
			}
			if (collectionDataContract.IsDictionary)
			{
				xmlElement2 = this.ExportIsDictionary();
			}
			xmlSchemaComplexType.Annotation = SchemaExporter.GetSchemaAnnotation(new XmlNode[]
			{
				xmlElement2,
				xmlElement,
				this.ExportSurrogateData(collectionDataContract)
			});
			XmlSchemaSequence xmlSchemaSequence = new XmlSchemaSequence();
			XmlSchemaElement xmlSchemaElement = new XmlSchemaElement();
			xmlSchemaElement.Name = collectionDataContract.ItemName;
			xmlSchemaElement.MinOccurs = 0m;
			xmlSchemaElement.MaxOccursString = "unbounded";
			if (collectionDataContract.IsDictionary)
			{
				ClassDataContract classDataContract = collectionDataContract.ItemContract as ClassDataContract;
				XmlSchemaComplexType xmlSchemaComplexType2 = new XmlSchemaComplexType();
				XmlSchemaSequence xmlSchemaSequence2 = new XmlSchemaSequence();
				foreach (DataMember dataMember in classDataContract.Members)
				{
					XmlSchemaElement xmlSchemaElement2 = new XmlSchemaElement();
					xmlSchemaElement2.Name = dataMember.Name;
					this.SetElementType(xmlSchemaElement2, this.dataContractSet.GetMemberTypeDataContract(dataMember), schema);
					SchemaHelper.AddElementForm(xmlSchemaElement2, schema);
					if (dataMember.IsNullable)
					{
						xmlSchemaElement2.IsNillable = true;
					}
					xmlSchemaElement2.Annotation = SchemaExporter.GetSchemaAnnotation(new XmlNode[]
					{
						this.ExportSurrogateData(dataMember)
					});
					xmlSchemaSequence2.Items.Add(xmlSchemaElement2);
				}
				xmlSchemaComplexType2.Particle = xmlSchemaSequence2;
				xmlSchemaElement.SchemaType = xmlSchemaComplexType2;
			}
			else
			{
				if (collectionDataContract.IsItemTypeNullable)
				{
					xmlSchemaElement.IsNillable = true;
				}
				DataContract itemTypeDataContract = this.dataContractSet.GetItemTypeDataContract(collectionDataContract);
				this.SetElementType(xmlSchemaElement, itemTypeDataContract, schema);
			}
			SchemaHelper.AddElementForm(xmlSchemaElement, schema);
			xmlSchemaSequence.Items.Add(xmlSchemaElement);
			xmlSchemaComplexType.Particle = xmlSchemaSequence;
			if (collectionDataContract.IsReference)
			{
				this.AddReferenceAttributes(xmlSchemaComplexType.Attributes, schema);
			}
		}

		// Token: 0x06000E7F RID: 3711 RVA: 0x0003847C File Offset: 0x0003667C
		private XmlElement ExportIsDictionary()
		{
			XmlElement xmlElement = this.XmlDoc.CreateElement(SchemaExporter.IsDictionaryAnnotationName.Name, SchemaExporter.IsDictionaryAnnotationName.Namespace);
			xmlElement.InnerText = "true";
			return xmlElement;
		}

		// Token: 0x06000E80 RID: 3712 RVA: 0x000384A8 File Offset: 0x000366A8
		private void ExportEnumDataContract(EnumDataContract enumDataContract, XmlSchema schema)
		{
			XmlSchemaSimpleType xmlSchemaSimpleType = new XmlSchemaSimpleType();
			xmlSchemaSimpleType.Name = enumDataContract.StableName.Name;
			XmlElement xmlElement = (enumDataContract.BaseContractName == SchemaExporter.DefaultEnumBaseTypeName) ? null : this.ExportActualType(enumDataContract.BaseContractName);
			xmlSchemaSimpleType.Annotation = SchemaExporter.GetSchemaAnnotation(new XmlNode[]
			{
				xmlElement,
				this.ExportSurrogateData(enumDataContract)
			});
			schema.Items.Add(xmlSchemaSimpleType);
			XmlSchemaSimpleTypeRestriction xmlSchemaSimpleTypeRestriction = new XmlSchemaSimpleTypeRestriction();
			xmlSchemaSimpleTypeRestriction.BaseTypeName = SchemaExporter.StringQualifiedName;
			SchemaHelper.AddSchemaImport(enumDataContract.BaseContractName.Namespace, schema);
			if (enumDataContract.Values != null)
			{
				for (int i = 0; i < enumDataContract.Values.Count; i++)
				{
					XmlSchemaEnumerationFacet xmlSchemaEnumerationFacet = new XmlSchemaEnumerationFacet();
					xmlSchemaEnumerationFacet.Value = enumDataContract.Members[i].Name;
					if (enumDataContract.Values[i] != SchemaExporter.GetDefaultEnumValue(enumDataContract.IsFlags, i))
					{
						xmlSchemaEnumerationFacet.Annotation = this.GetSchemaAnnotation(SchemaExporter.EnumerationValueAnnotationName, enumDataContract.GetStringFromEnumValue(enumDataContract.Values[i]), schema);
					}
					xmlSchemaSimpleTypeRestriction.Facets.Add(xmlSchemaEnumerationFacet);
				}
			}
			if (enumDataContract.IsFlags)
			{
				xmlSchemaSimpleType.Content = new XmlSchemaSimpleTypeList
				{
					ItemType = new XmlSchemaSimpleType
					{
						Content = xmlSchemaSimpleTypeRestriction
					}
				};
				return;
			}
			xmlSchemaSimpleType.Content = xmlSchemaSimpleTypeRestriction;
		}

		// Token: 0x06000E81 RID: 3713 RVA: 0x00038600 File Offset: 0x00036800
		internal static long GetDefaultEnumValue(bool isFlags, int index)
		{
			if (!isFlags)
			{
				return (long)index;
			}
			return (long)Math.Pow(2.0, (double)index);
		}

		// Token: 0x06000E82 RID: 3714 RVA: 0x0003861C File Offset: 0x0003681C
		private void ExportISerializableDataContract(ClassDataContract dataContract, XmlSchema schema)
		{
			XmlSchemaComplexType xmlSchemaComplexType = new XmlSchemaComplexType();
			xmlSchemaComplexType.Name = dataContract.StableName.Name;
			schema.Items.Add(xmlSchemaComplexType);
			XmlElement xmlElement = null;
			if (dataContract.UnderlyingType.IsGenericType)
			{
				xmlElement = this.ExportGenericInfo(dataContract.UnderlyingType, "GenericType", "http://schemas.microsoft.com/2003/10/Serialization/");
			}
			XmlElement xmlElement2 = null;
			if (dataContract.BaseContract != null)
			{
				this.CreateTypeContent(xmlSchemaComplexType, dataContract.BaseContract.StableName, schema);
			}
			else
			{
				schema.Namespaces.Add("ser", "http://schemas.microsoft.com/2003/10/Serialization/");
				xmlSchemaComplexType.Particle = SchemaExporter.ISerializableSequence;
				XmlSchemaAttribute iserializableFactoryTypeAttribute = SchemaExporter.ISerializableFactoryTypeAttribute;
				xmlSchemaComplexType.Attributes.Add(iserializableFactoryTypeAttribute);
				SchemaHelper.AddSchemaImport(SchemaExporter.ISerializableFactoryTypeAttribute.RefName.Namespace, schema);
				if (dataContract.IsValueType)
				{
					xmlElement2 = this.GetAnnotationMarkup(SchemaExporter.IsValueTypeName, XmlConvert.ToString(dataContract.IsValueType), schema);
				}
			}
			xmlSchemaComplexType.Annotation = SchemaExporter.GetSchemaAnnotation(new XmlNode[]
			{
				xmlElement,
				this.ExportSurrogateData(dataContract),
				xmlElement2
			});
		}

		// Token: 0x06000E83 RID: 3715 RVA: 0x00038720 File Offset: 0x00036920
		private XmlSchemaComplexContentExtension CreateTypeContent(XmlSchemaComplexType type, XmlQualifiedName baseTypeName, XmlSchema schema)
		{
			SchemaHelper.AddSchemaImport(baseTypeName.Namespace, schema);
			XmlSchemaComplexContentExtension xmlSchemaComplexContentExtension = new XmlSchemaComplexContentExtension();
			xmlSchemaComplexContentExtension.BaseTypeName = baseTypeName;
			type.ContentModel = new XmlSchemaComplexContent();
			type.ContentModel.Content = xmlSchemaComplexContentExtension;
			return xmlSchemaComplexContentExtension;
		}

		// Token: 0x06000E84 RID: 3716 RVA: 0x00038760 File Offset: 0x00036960
		private void ExportXmlDataContract(XmlDataContract dataContract)
		{
			Type underlyingType = dataContract.UnderlyingType;
			XmlQualifiedName xmlQualifiedName;
			XmlSchemaType schemaType;
			bool flag;
			if (!SchemaExporter.IsSpecialXmlType(underlyingType, out xmlQualifiedName, out schemaType, out flag) && !SchemaExporter.InvokeSchemaProviderMethod(underlyingType, this.schemas, out xmlQualifiedName, out schemaType, out flag))
			{
				SchemaExporter.InvokeGetSchemaMethod(underlyingType, this.schemas, xmlQualifiedName);
			}
			if (flag)
			{
				xmlQualifiedName.Equals(dataContract.StableName);
				XmlSchema schema;
				if (SchemaHelper.GetSchemaElement(this.Schemas, new XmlQualifiedName(dataContract.TopLevelElementName.Value, dataContract.TopLevelElementNamespace.Value), out schema) == null)
				{
					this.ExportTopLevelElement(dataContract, schema).IsNillable = dataContract.IsTopLevelElementNullable;
					SchemaExporter.ReprocessAll(this.schemas);
				}
				bool flag2 = schemaType != null;
				schemaType = SchemaHelper.GetSchemaType(this.schemas, xmlQualifiedName, out schema);
				if (!flag2 && schemaType == null && xmlQualifiedName.Namespace != "http://www.w3.org/2001/XMLSchema")
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidDataContractException(SR.GetString("Schema type '{0}' is missing and required for '{1}' type.", new object[]
					{
						xmlQualifiedName,
						DataContract.GetClrTypeFullName(underlyingType)
					})));
				}
				if (schemaType != null)
				{
					schemaType.Annotation = SchemaExporter.GetSchemaAnnotation(new XmlNode[]
					{
						this.ExportSurrogateData(dataContract),
						dataContract.IsValueType ? this.GetAnnotationMarkup(SchemaExporter.IsValueTypeName, XmlConvert.ToString(dataContract.IsValueType), schema) : null
					});
					return;
				}
				if (DiagnosticUtility.ShouldTraceVerbose)
				{
					TraceUtility.Trace(TraceEventType.Verbose, 196622, SR.GetString("XSD export annotation failed"), new StringTraceRecord("Type", xmlQualifiedName.Namespace + ":" + xmlQualifiedName.Name));
				}
			}
		}

		// Token: 0x06000E85 RID: 3717 RVA: 0x000388D0 File Offset: 0x00036AD0
		private static void ReprocessAll(XmlSchemaSet schemas)
		{
			Hashtable hashtable = new Hashtable();
			Hashtable hashtable2 = new Hashtable();
			XmlSchema[] array = new XmlSchema[schemas.Count];
			schemas.CopyTo(array, 0);
			foreach (XmlSchema xmlSchema in array)
			{
				XmlSchemaObject[] array2 = new XmlSchemaObject[xmlSchema.Items.Count];
				xmlSchema.Items.CopyTo(array2, 0);
				int j = 0;
				while (j < array2.Length)
				{
					XmlSchemaObject xmlSchemaObject = array2[j];
					Hashtable hashtable3;
					XmlQualifiedName xmlQualifiedName;
					if (xmlSchemaObject is XmlSchemaElement)
					{
						hashtable3 = hashtable;
						xmlQualifiedName = new XmlQualifiedName(((XmlSchemaElement)xmlSchemaObject).Name, xmlSchema.TargetNamespace);
						goto IL_AE;
					}
					if (xmlSchemaObject is XmlSchemaType)
					{
						hashtable3 = hashtable2;
						xmlQualifiedName = new XmlQualifiedName(((XmlSchemaType)xmlSchemaObject).Name, xmlSchema.TargetNamespace);
						goto IL_AE;
					}
					IL_134:
					j++;
					continue;
					IL_AE:
					if (hashtable3[xmlQualifiedName] != null)
					{
						if (DiagnosticUtility.ShouldTraceWarning)
						{
							Dictionary<string, string> dictionary = new Dictionary<string, string>(2)
							{
								{
									"ItemType",
									xmlSchemaObject.ToString()
								},
								{
									"Name",
									xmlQualifiedName.Namespace + ":" + xmlQualifiedName.Name
								}
							};
							TraceUtility.Trace(TraceEventType.Warning, 196624, SR.GetString("XSD export duplicate items"), new DictionaryTraceRecord(dictionary));
						}
						xmlSchema.Items.Remove(xmlSchemaObject);
						goto IL_134;
					}
					hashtable3.Add(xmlQualifiedName, xmlSchemaObject);
					goto IL_134;
				}
				schemas.Reprocess(xmlSchema);
			}
		}

		// Token: 0x06000E86 RID: 3718 RVA: 0x00038A38 File Offset: 0x00036C38
		internal static void GetXmlTypeInfo(Type type, out XmlQualifiedName stableName, out XmlSchemaType xsdType, out bool hasRoot)
		{
			if (SchemaExporter.IsSpecialXmlType(type, out stableName, out xsdType, out hasRoot))
			{
				return;
			}
			SchemaExporter.InvokeSchemaProviderMethod(type, new XmlSchemaSet
			{
				XmlResolver = null
			}, out stableName, out xsdType, out hasRoot);
			if (stableName.Name == null || stableName.Name.Length == 0)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidDataContractException(SR.GetString("XML data contract Name for type '{0}' cannot be set to null or empty string.", new object[]
				{
					DataContract.GetClrTypeFullName(type)
				})));
			}
		}

		// Token: 0x06000E87 RID: 3719 RVA: 0x00038AA4 File Offset: 0x00036CA4
		private static bool InvokeSchemaProviderMethod(Type clrType, XmlSchemaSet schemas, out XmlQualifiedName stableName, out XmlSchemaType xsdType, out bool hasRoot)
		{
			xsdType = null;
			hasRoot = true;
			object[] customAttributes = clrType.GetCustomAttributes(Globals.TypeOfXmlSchemaProviderAttribute, false);
			if (customAttributes == null || customAttributes.Length == 0)
			{
				stableName = DataContract.GetDefaultStableName(clrType);
				return false;
			}
			XmlSchemaProviderAttribute xmlSchemaProviderAttribute = (XmlSchemaProviderAttribute)customAttributes[0];
			if (xmlSchemaProviderAttribute.IsAny)
			{
				xsdType = SchemaExporter.CreateAnyElementType();
				hasRoot = false;
			}
			string methodName = xmlSchemaProviderAttribute.MethodName;
			if (methodName == null || methodName.Length == 0)
			{
				if (!xmlSchemaProviderAttribute.IsAny)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidDataContractException(SR.GetString("Type '{0}' cannot have MethodName on XmlSchemaProviderAttribute attribute set to null or empty string.", new object[]
					{
						DataContract.GetClrTypeFullName(clrType)
					})));
				}
				stableName = DataContract.GetDefaultStableName(clrType);
			}
			else
			{
				MethodInfo method = clrType.GetMethod(methodName, BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic, null, new Type[]
				{
					typeof(XmlSchemaSet)
				}, null);
				if (method == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidDataContractException(SR.GetString("Type '{0}' does not have a static method '{1}' that takes a parameter of type 'System.Xml.Schema.XmlSchemaSet' as specified by the XmlSchemaProviderAttribute attribute.", new object[]
					{
						DataContract.GetClrTypeFullName(clrType),
						methodName
					})));
				}
				if (!Globals.TypeOfXmlQualifiedName.IsAssignableFrom(method.ReturnType) && !Globals.TypeOfXmlSchemaType.IsAssignableFrom(method.ReturnType))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidDataContractException(SR.GetString("Method '{0}.{1}()' returns '{2}'. The return type must be compatible with '{3}'.", new object[]
					{
						DataContract.GetClrTypeFullName(clrType),
						methodName,
						DataContract.GetClrTypeFullName(method.ReturnType),
						DataContract.GetClrTypeFullName(Globals.TypeOfXmlQualifiedName),
						typeof(XmlSchemaType)
					})));
				}
				object obj = method.Invoke(null, new object[]
				{
					schemas
				});
				if (xmlSchemaProviderAttribute.IsAny)
				{
					if (obj != null)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidDataContractException(SR.GetString("Method '{0}.{1}()' returns a non-null value. The return value must be null since IsAny=true.", new object[]
						{
							DataContract.GetClrTypeFullName(clrType),
							methodName
						})));
					}
					stableName = DataContract.GetDefaultStableName(clrType);
				}
				else if (obj == null)
				{
					xsdType = SchemaExporter.CreateAnyElementType();
					hasRoot = false;
					stableName = DataContract.GetDefaultStableName(clrType);
				}
				else
				{
					XmlSchemaType xmlSchemaType = obj as XmlSchemaType;
					if (xmlSchemaType != null)
					{
						string name = xmlSchemaType.Name;
						string text = null;
						if (name == null || name.Length == 0)
						{
							DataContract.GetDefaultStableName(DataContract.GetClrTypeFullName(clrType), out name, out text);
							stableName = new XmlQualifiedName(name, text);
							xmlSchemaType.Annotation = SchemaExporter.GetSchemaAnnotation(new XmlNode[]
							{
								SchemaExporter.ExportActualType(stableName, new XmlDocument())
							});
							xsdType = xmlSchemaType;
						}
						else
						{
							foreach (object obj2 in schemas.Schemas())
							{
								XmlSchema xmlSchema = (XmlSchema)obj2;
								using (XmlSchemaObjectEnumerator enumerator2 = xmlSchema.Items.GetEnumerator())
								{
									while (enumerator2.MoveNext())
									{
										if (enumerator2.Current == xmlSchemaType)
										{
											text = xmlSchema.TargetNamespace;
											if (text == null)
											{
												text = string.Empty;
												break;
											}
											break;
										}
									}
								}
								if (text != null)
								{
									break;
								}
							}
							if (text == null)
							{
								throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidDataContractException(SR.GetString("Schema type '{0}' is missing and required for '{1}' type.", new object[]
								{
									name,
									DataContract.GetClrTypeFullName(clrType)
								})));
							}
							stableName = new XmlQualifiedName(name, text);
						}
					}
					else
					{
						stableName = (XmlQualifiedName)obj;
					}
				}
			}
			return true;
		}

		// Token: 0x06000E88 RID: 3720 RVA: 0x00038DD4 File Offset: 0x00036FD4
		private static void InvokeGetSchemaMethod(Type clrType, XmlSchemaSet schemas, XmlQualifiedName stableName)
		{
			XmlSchema schema = ((IXmlSerializable)Activator.CreateInstance(clrType)).GetSchema();
			if (schema == null)
			{
				SchemaExporter.AddDefaultDatasetType(schemas, stableName.Name, stableName.Namespace);
				return;
			}
			if (schema.Id == null || schema.Id.Length == 0)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidDataContractException(SR.GetString("On type '{0}', the return value from GetSchema method was invalid.", new object[]
				{
					DataContract.GetClrTypeFullName(clrType)
				})));
			}
			SchemaExporter.AddDefaultTypedDatasetType(schemas, schema, stableName.Name, stableName.Namespace);
		}

		// Token: 0x06000E89 RID: 3721 RVA: 0x00038E54 File Offset: 0x00037054
		internal static void AddDefaultXmlType(XmlSchemaSet schemas, string localName, string ns)
		{
			XmlSchemaComplexType xmlSchemaComplexType = SchemaExporter.CreateAnyType();
			xmlSchemaComplexType.Name = localName;
			XmlSchema schema = SchemaHelper.GetSchema(ns, schemas);
			schema.Items.Add(xmlSchemaComplexType);
			schemas.Reprocess(schema);
		}

		// Token: 0x06000E8A RID: 3722 RVA: 0x00038E8C File Offset: 0x0003708C
		private static XmlSchemaComplexType CreateAnyType()
		{
			XmlSchemaComplexType xmlSchemaComplexType = new XmlSchemaComplexType();
			xmlSchemaComplexType.IsMixed = true;
			xmlSchemaComplexType.Particle = new XmlSchemaSequence();
			XmlSchemaAny xmlSchemaAny = new XmlSchemaAny();
			xmlSchemaAny.MinOccurs = 0m;
			xmlSchemaAny.MaxOccurs = decimal.MaxValue;
			xmlSchemaAny.ProcessContents = XmlSchemaContentProcessing.Lax;
			((XmlSchemaSequence)xmlSchemaComplexType.Particle).Items.Add(xmlSchemaAny);
			xmlSchemaComplexType.AnyAttribute = new XmlSchemaAnyAttribute();
			return xmlSchemaComplexType;
		}

		// Token: 0x06000E8B RID: 3723 RVA: 0x00038EFC File Offset: 0x000370FC
		private static XmlSchemaComplexType CreateAnyElementType()
		{
			XmlSchemaComplexType xmlSchemaComplexType = new XmlSchemaComplexType();
			xmlSchemaComplexType.IsMixed = false;
			xmlSchemaComplexType.Particle = new XmlSchemaSequence();
			XmlSchemaAny xmlSchemaAny = new XmlSchemaAny();
			xmlSchemaAny.MinOccurs = 0m;
			xmlSchemaAny.ProcessContents = XmlSchemaContentProcessing.Lax;
			((XmlSchemaSequence)xmlSchemaComplexType.Particle).Items.Add(xmlSchemaAny);
			return xmlSchemaComplexType;
		}

		// Token: 0x06000E8C RID: 3724 RVA: 0x00038F50 File Offset: 0x00037150
		internal static bool IsSpecialXmlType(Type type, out XmlQualifiedName typeName, out XmlSchemaType xsdType, out bool hasRoot)
		{
			xsdType = null;
			hasRoot = true;
			if (type == Globals.TypeOfXmlElement || type == Globals.TypeOfXmlNodeArray)
			{
				string name;
				if (type == Globals.TypeOfXmlElement)
				{
					xsdType = SchemaExporter.CreateAnyElementType();
					name = "XmlElement";
					hasRoot = false;
				}
				else
				{
					xsdType = SchemaExporter.CreateAnyType();
					name = "ArrayOfXmlNode";
					hasRoot = true;
				}
				typeName = new XmlQualifiedName(name, DataContract.GetDefaultStableNamespace(type));
				return true;
			}
			typeName = null;
			return false;
		}

		// Token: 0x06000E8D RID: 3725 RVA: 0x00038FC4 File Offset: 0x000371C4
		private static void AddDefaultDatasetType(XmlSchemaSet schemas, string localName, string ns)
		{
			XmlSchemaComplexType xmlSchemaComplexType = new XmlSchemaComplexType();
			xmlSchemaComplexType.Name = localName;
			xmlSchemaComplexType.Particle = new XmlSchemaSequence();
			XmlSchemaElement xmlSchemaElement = new XmlSchemaElement();
			xmlSchemaElement.RefName = new XmlQualifiedName("schema", "http://www.w3.org/2001/XMLSchema");
			((XmlSchemaSequence)xmlSchemaComplexType.Particle).Items.Add(xmlSchemaElement);
			XmlSchemaAny item = new XmlSchemaAny();
			((XmlSchemaSequence)xmlSchemaComplexType.Particle).Items.Add(item);
			XmlSchema schema = SchemaHelper.GetSchema(ns, schemas);
			schema.Items.Add(xmlSchemaComplexType);
			schemas.Reprocess(schema);
		}

		// Token: 0x06000E8E RID: 3726 RVA: 0x00039058 File Offset: 0x00037258
		private static void AddDefaultTypedDatasetType(XmlSchemaSet schemas, XmlSchema datasetSchema, string localName, string ns)
		{
			XmlSchemaComplexType xmlSchemaComplexType = new XmlSchemaComplexType();
			xmlSchemaComplexType.Name = localName;
			xmlSchemaComplexType.Particle = new XmlSchemaSequence();
			XmlSchemaAny xmlSchemaAny = new XmlSchemaAny();
			xmlSchemaAny.Namespace = ((datasetSchema.TargetNamespace == null) ? string.Empty : datasetSchema.TargetNamespace);
			((XmlSchemaSequence)xmlSchemaComplexType.Particle).Items.Add(xmlSchemaAny);
			schemas.Add(datasetSchema);
			XmlSchema schema = SchemaHelper.GetSchema(ns, schemas);
			schema.Items.Add(xmlSchemaComplexType);
			schemas.Reprocess(datasetSchema);
			schemas.Reprocess(schema);
		}

		// Token: 0x06000E8F RID: 3727 RVA: 0x000390E4 File Offset: 0x000372E4
		private XmlSchemaAnnotation GetSchemaAnnotation(XmlQualifiedName annotationQualifiedName, string innerText, XmlSchema schema)
		{
			XmlSchemaAnnotation xmlSchemaAnnotation = new XmlSchemaAnnotation();
			XmlSchemaAppInfo xmlSchemaAppInfo = new XmlSchemaAppInfo();
			XmlElement annotationMarkup = this.GetAnnotationMarkup(annotationQualifiedName, innerText, schema);
			xmlSchemaAppInfo.Markup = new XmlNode[]
			{
				annotationMarkup
			};
			xmlSchemaAnnotation.Items.Add(xmlSchemaAppInfo);
			return xmlSchemaAnnotation;
		}

		// Token: 0x06000E90 RID: 3728 RVA: 0x00039128 File Offset: 0x00037328
		private static XmlSchemaAnnotation GetSchemaAnnotation(params XmlNode[] nodes)
		{
			if (nodes == null || nodes.Length == 0)
			{
				return null;
			}
			bool flag = false;
			for (int i = 0; i < nodes.Length; i++)
			{
				if (nodes[i] != null)
				{
					flag = true;
					break;
				}
			}
			if (!flag)
			{
				return null;
			}
			XmlSchemaAnnotation xmlSchemaAnnotation = new XmlSchemaAnnotation();
			XmlSchemaAppInfo xmlSchemaAppInfo = new XmlSchemaAppInfo();
			xmlSchemaAnnotation.Items.Add(xmlSchemaAppInfo);
			xmlSchemaAppInfo.Markup = nodes;
			return xmlSchemaAnnotation;
		}

		// Token: 0x06000E91 RID: 3729 RVA: 0x0003917B File Offset: 0x0003737B
		private XmlElement GetAnnotationMarkup(XmlQualifiedName annotationQualifiedName, string innerText, XmlSchema schema)
		{
			XmlElement xmlElement = this.XmlDoc.CreateElement(annotationQualifiedName.Name, annotationQualifiedName.Namespace);
			SchemaHelper.AddSchemaImport(annotationQualifiedName.Namespace, schema);
			xmlElement.InnerText = innerText;
			return xmlElement;
		}

		// Token: 0x06000E92 RID: 3730 RVA: 0x000391A7 File Offset: 0x000373A7
		private XmlSchema GetSchema(string ns)
		{
			return SchemaHelper.GetSchema(ns, this.Schemas);
		}

		// Token: 0x17000330 RID: 816
		// (get) Token: 0x06000E93 RID: 3731 RVA: 0x000391B5 File Offset: 0x000373B5
		internal static XmlSchemaSequence ISerializableSequence
		{
			get
			{
				return new XmlSchemaSequence
				{
					Items = 
					{
						SchemaExporter.ISerializableWildcardElement
					}
				};
			}
		}

		// Token: 0x17000331 RID: 817
		// (get) Token: 0x06000E94 RID: 3732 RVA: 0x000391CD File Offset: 0x000373CD
		internal static XmlSchemaAny ISerializableWildcardElement
		{
			get
			{
				return new XmlSchemaAny
				{
					MinOccurs = 0m,
					MaxOccursString = "unbounded",
					Namespace = "##local",
					ProcessContents = XmlSchemaContentProcessing.Skip
				};
			}
		}

		// Token: 0x17000332 RID: 818
		// (get) Token: 0x06000E95 RID: 3733 RVA: 0x000391FC File Offset: 0x000373FC
		internal static XmlQualifiedName AnytypeQualifiedName
		{
			[SecuritySafeCritical]
			get
			{
				if (SchemaExporter.anytypeQualifiedName == null)
				{
					SchemaExporter.anytypeQualifiedName = new XmlQualifiedName("anyType", "http://www.w3.org/2001/XMLSchema");
				}
				return SchemaExporter.anytypeQualifiedName;
			}
		}

		// Token: 0x17000333 RID: 819
		// (get) Token: 0x06000E96 RID: 3734 RVA: 0x00039224 File Offset: 0x00037424
		internal static XmlQualifiedName StringQualifiedName
		{
			[SecuritySafeCritical]
			get
			{
				if (SchemaExporter.stringQualifiedName == null)
				{
					SchemaExporter.stringQualifiedName = new XmlQualifiedName("string", "http://www.w3.org/2001/XMLSchema");
				}
				return SchemaExporter.stringQualifiedName;
			}
		}

		// Token: 0x17000334 RID: 820
		// (get) Token: 0x06000E97 RID: 3735 RVA: 0x0003924C File Offset: 0x0003744C
		internal static XmlQualifiedName DefaultEnumBaseTypeName
		{
			[SecuritySafeCritical]
			get
			{
				if (SchemaExporter.defaultEnumBaseTypeName == null)
				{
					SchemaExporter.defaultEnumBaseTypeName = new XmlQualifiedName("int", "http://www.w3.org/2001/XMLSchema");
				}
				return SchemaExporter.defaultEnumBaseTypeName;
			}
		}

		// Token: 0x17000335 RID: 821
		// (get) Token: 0x06000E98 RID: 3736 RVA: 0x00039274 File Offset: 0x00037474
		internal static XmlQualifiedName EnumerationValueAnnotationName
		{
			[SecuritySafeCritical]
			get
			{
				if (SchemaExporter.enumerationValueAnnotationName == null)
				{
					SchemaExporter.enumerationValueAnnotationName = new XmlQualifiedName("EnumerationValue", "http://schemas.microsoft.com/2003/10/Serialization/");
				}
				return SchemaExporter.enumerationValueAnnotationName;
			}
		}

		// Token: 0x17000336 RID: 822
		// (get) Token: 0x06000E99 RID: 3737 RVA: 0x0003929C File Offset: 0x0003749C
		internal static XmlQualifiedName SurrogateDataAnnotationName
		{
			[SecuritySafeCritical]
			get
			{
				if (SchemaExporter.surrogateDataAnnotationName == null)
				{
					SchemaExporter.surrogateDataAnnotationName = new XmlQualifiedName("Surrogate", "http://schemas.microsoft.com/2003/10/Serialization/");
				}
				return SchemaExporter.surrogateDataAnnotationName;
			}
		}

		// Token: 0x17000337 RID: 823
		// (get) Token: 0x06000E9A RID: 3738 RVA: 0x000392C4 File Offset: 0x000374C4
		internal static XmlQualifiedName DefaultValueAnnotation
		{
			[SecuritySafeCritical]
			get
			{
				if (SchemaExporter.defaultValueAnnotation == null)
				{
					SchemaExporter.defaultValueAnnotation = new XmlQualifiedName("DefaultValue", "http://schemas.microsoft.com/2003/10/Serialization/");
				}
				return SchemaExporter.defaultValueAnnotation;
			}
		}

		// Token: 0x17000338 RID: 824
		// (get) Token: 0x06000E9B RID: 3739 RVA: 0x000392EC File Offset: 0x000374EC
		internal static XmlQualifiedName ActualTypeAnnotationName
		{
			[SecuritySafeCritical]
			get
			{
				if (SchemaExporter.actualTypeAnnotationName == null)
				{
					SchemaExporter.actualTypeAnnotationName = new XmlQualifiedName("ActualType", "http://schemas.microsoft.com/2003/10/Serialization/");
				}
				return SchemaExporter.actualTypeAnnotationName;
			}
		}

		// Token: 0x17000339 RID: 825
		// (get) Token: 0x06000E9C RID: 3740 RVA: 0x00039314 File Offset: 0x00037514
		internal static XmlQualifiedName IsDictionaryAnnotationName
		{
			[SecuritySafeCritical]
			get
			{
				if (SchemaExporter.isDictionaryAnnotationName == null)
				{
					SchemaExporter.isDictionaryAnnotationName = new XmlQualifiedName("IsDictionary", "http://schemas.microsoft.com/2003/10/Serialization/");
				}
				return SchemaExporter.isDictionaryAnnotationName;
			}
		}

		// Token: 0x1700033A RID: 826
		// (get) Token: 0x06000E9D RID: 3741 RVA: 0x0003933C File Offset: 0x0003753C
		internal static XmlQualifiedName IsValueTypeName
		{
			[SecuritySafeCritical]
			get
			{
				if (SchemaExporter.isValueTypeName == null)
				{
					SchemaExporter.isValueTypeName = new XmlQualifiedName("IsValueType", "http://schemas.microsoft.com/2003/10/Serialization/");
				}
				return SchemaExporter.isValueTypeName;
			}
		}

		// Token: 0x1700033B RID: 827
		// (get) Token: 0x06000E9E RID: 3742 RVA: 0x00039364 File Offset: 0x00037564
		internal static XmlSchemaAttribute ISerializableFactoryTypeAttribute
		{
			get
			{
				return new XmlSchemaAttribute
				{
					RefName = new XmlQualifiedName("FactoryType", "http://schemas.microsoft.com/2003/10/Serialization/")
				};
			}
		}

		// Token: 0x1700033C RID: 828
		// (get) Token: 0x06000E9F RID: 3743 RVA: 0x00039380 File Offset: 0x00037580
		internal static XmlSchemaAttribute RefAttribute
		{
			get
			{
				return new XmlSchemaAttribute
				{
					RefName = Globals.RefQualifiedName
				};
			}
		}

		// Token: 0x1700033D RID: 829
		// (get) Token: 0x06000EA0 RID: 3744 RVA: 0x00039392 File Offset: 0x00037592
		internal static XmlSchemaAttribute IdAttribute
		{
			get
			{
				return new XmlSchemaAttribute
				{
					RefName = Globals.IdQualifiedName
				};
			}
		}

		// Token: 0x0400065F RID: 1631
		private XmlSchemaSet schemas;

		// Token: 0x04000660 RID: 1632
		private XmlDocument xmlDoc;

		// Token: 0x04000661 RID: 1633
		private DataContractSet dataContractSet;

		// Token: 0x04000662 RID: 1634
		[SecurityCritical]
		private static XmlQualifiedName anytypeQualifiedName;

		// Token: 0x04000663 RID: 1635
		[SecurityCritical]
		private static XmlQualifiedName stringQualifiedName;

		// Token: 0x04000664 RID: 1636
		[SecurityCritical]
		private static XmlQualifiedName defaultEnumBaseTypeName;

		// Token: 0x04000665 RID: 1637
		[SecurityCritical]
		private static XmlQualifiedName enumerationValueAnnotationName;

		// Token: 0x04000666 RID: 1638
		[SecurityCritical]
		private static XmlQualifiedName surrogateDataAnnotationName;

		// Token: 0x04000667 RID: 1639
		[SecurityCritical]
		private static XmlQualifiedName defaultValueAnnotation;

		// Token: 0x04000668 RID: 1640
		[SecurityCritical]
		private static XmlQualifiedName actualTypeAnnotationName;

		// Token: 0x04000669 RID: 1641
		[SecurityCritical]
		private static XmlQualifiedName isDictionaryAnnotationName;

		// Token: 0x0400066A RID: 1642
		[SecurityCritical]
		private static XmlQualifiedName isValueTypeName;
	}
}

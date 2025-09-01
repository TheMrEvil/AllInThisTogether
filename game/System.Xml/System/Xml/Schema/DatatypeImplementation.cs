using System;
using System.Collections;

namespace System.Xml.Schema
{
	// Token: 0x0200050F RID: 1295
	internal abstract class DatatypeImplementation : XmlSchemaDatatype
	{
		// Token: 0x06003488 RID: 13448 RVA: 0x001295C4 File Offset: 0x001277C4
		static DatatypeImplementation()
		{
			DatatypeImplementation[] array = new DatatypeImplementation[13];
			array[0] = DatatypeImplementation.c_string;
			array[1] = DatatypeImplementation.c_ID;
			array[2] = DatatypeImplementation.c_IDREF;
			array[3] = DatatypeImplementation.c_IDREFS;
			array[4] = DatatypeImplementation.c_ENTITY;
			array[5] = DatatypeImplementation.c_ENTITIES;
			array[6] = DatatypeImplementation.c_NMTOKEN;
			array[7] = DatatypeImplementation.c_NMTOKENS;
			array[8] = DatatypeImplementation.c_NOTATION;
			array[9] = DatatypeImplementation.c_ENUMERATION;
			array[10] = DatatypeImplementation.c_QNameXdr;
			array[11] = DatatypeImplementation.c_NCName;
			DatatypeImplementation.c_tokenizedTypes = array;
			DatatypeImplementation[] array2 = new DatatypeImplementation[13];
			array2[0] = DatatypeImplementation.c_string;
			array2[1] = DatatypeImplementation.c_ID;
			array2[2] = DatatypeImplementation.c_IDREF;
			array2[3] = DatatypeImplementation.c_IDREFS;
			array2[4] = DatatypeImplementation.c_ENTITY;
			array2[5] = DatatypeImplementation.c_ENTITIES;
			array2[6] = DatatypeImplementation.c_NMTOKEN;
			array2[7] = DatatypeImplementation.c_NMTOKENS;
			array2[8] = DatatypeImplementation.c_NOTATION;
			array2[9] = DatatypeImplementation.c_ENUMERATION;
			array2[10] = DatatypeImplementation.c_QName;
			array2[11] = DatatypeImplementation.c_NCName;
			DatatypeImplementation.c_tokenizedTypesXsd = array2;
			DatatypeImplementation.c_XdrTypes = new DatatypeImplementation.SchemaDatatypeMap[]
			{
				new DatatypeImplementation.SchemaDatatypeMap("bin.base64", DatatypeImplementation.c_base64Binary),
				new DatatypeImplementation.SchemaDatatypeMap("bin.hex", DatatypeImplementation.c_hexBinary),
				new DatatypeImplementation.SchemaDatatypeMap("boolean", DatatypeImplementation.c_boolean),
				new DatatypeImplementation.SchemaDatatypeMap("char", DatatypeImplementation.c_char),
				new DatatypeImplementation.SchemaDatatypeMap("date", DatatypeImplementation.c_date),
				new DatatypeImplementation.SchemaDatatypeMap("dateTime", DatatypeImplementation.c_dateTimeNoTz),
				new DatatypeImplementation.SchemaDatatypeMap("dateTime.tz", DatatypeImplementation.c_dateTimeTz),
				new DatatypeImplementation.SchemaDatatypeMap("decimal", DatatypeImplementation.c_decimal),
				new DatatypeImplementation.SchemaDatatypeMap("entities", DatatypeImplementation.c_ENTITIES),
				new DatatypeImplementation.SchemaDatatypeMap("entity", DatatypeImplementation.c_ENTITY),
				new DatatypeImplementation.SchemaDatatypeMap("enumeration", DatatypeImplementation.c_ENUMERATION),
				new DatatypeImplementation.SchemaDatatypeMap("fixed.14.4", DatatypeImplementation.c_fixed),
				new DatatypeImplementation.SchemaDatatypeMap("float", DatatypeImplementation.c_doubleXdr),
				new DatatypeImplementation.SchemaDatatypeMap("float.ieee.754.32", DatatypeImplementation.c_floatXdr),
				new DatatypeImplementation.SchemaDatatypeMap("float.ieee.754.64", DatatypeImplementation.c_doubleXdr),
				new DatatypeImplementation.SchemaDatatypeMap("i1", DatatypeImplementation.c_byte),
				new DatatypeImplementation.SchemaDatatypeMap("i2", DatatypeImplementation.c_short),
				new DatatypeImplementation.SchemaDatatypeMap("i4", DatatypeImplementation.c_int),
				new DatatypeImplementation.SchemaDatatypeMap("i8", DatatypeImplementation.c_long),
				new DatatypeImplementation.SchemaDatatypeMap("id", DatatypeImplementation.c_ID),
				new DatatypeImplementation.SchemaDatatypeMap("idref", DatatypeImplementation.c_IDREF),
				new DatatypeImplementation.SchemaDatatypeMap("idrefs", DatatypeImplementation.c_IDREFS),
				new DatatypeImplementation.SchemaDatatypeMap("int", DatatypeImplementation.c_int),
				new DatatypeImplementation.SchemaDatatypeMap("nmtoken", DatatypeImplementation.c_NMTOKEN),
				new DatatypeImplementation.SchemaDatatypeMap("nmtokens", DatatypeImplementation.c_NMTOKENS),
				new DatatypeImplementation.SchemaDatatypeMap("notation", DatatypeImplementation.c_NOTATION),
				new DatatypeImplementation.SchemaDatatypeMap("number", DatatypeImplementation.c_doubleXdr),
				new DatatypeImplementation.SchemaDatatypeMap("r4", DatatypeImplementation.c_floatXdr),
				new DatatypeImplementation.SchemaDatatypeMap("r8", DatatypeImplementation.c_doubleXdr),
				new DatatypeImplementation.SchemaDatatypeMap("string", DatatypeImplementation.c_string),
				new DatatypeImplementation.SchemaDatatypeMap("time", DatatypeImplementation.c_timeNoTz),
				new DatatypeImplementation.SchemaDatatypeMap("time.tz", DatatypeImplementation.c_timeTz),
				new DatatypeImplementation.SchemaDatatypeMap("ui1", DatatypeImplementation.c_unsignedByte),
				new DatatypeImplementation.SchemaDatatypeMap("ui2", DatatypeImplementation.c_unsignedShort),
				new DatatypeImplementation.SchemaDatatypeMap("ui4", DatatypeImplementation.c_unsignedInt),
				new DatatypeImplementation.SchemaDatatypeMap("ui8", DatatypeImplementation.c_unsignedLong),
				new DatatypeImplementation.SchemaDatatypeMap("uri", DatatypeImplementation.c_anyURI),
				new DatatypeImplementation.SchemaDatatypeMap("uuid", DatatypeImplementation.c_uuid)
			};
			DatatypeImplementation.c_XsdTypes = new DatatypeImplementation.SchemaDatatypeMap[]
			{
				new DatatypeImplementation.SchemaDatatypeMap("ENTITIES", DatatypeImplementation.c_ENTITIES, 11),
				new DatatypeImplementation.SchemaDatatypeMap("ENTITY", DatatypeImplementation.c_ENTITY, 11),
				new DatatypeImplementation.SchemaDatatypeMap("ID", DatatypeImplementation.c_ID, 5),
				new DatatypeImplementation.SchemaDatatypeMap("IDREF", DatatypeImplementation.c_IDREF, 5),
				new DatatypeImplementation.SchemaDatatypeMap("IDREFS", DatatypeImplementation.c_IDREFS, 11),
				new DatatypeImplementation.SchemaDatatypeMap("NCName", DatatypeImplementation.c_NCName, 9),
				new DatatypeImplementation.SchemaDatatypeMap("NMTOKEN", DatatypeImplementation.c_NMTOKEN, 40),
				new DatatypeImplementation.SchemaDatatypeMap("NMTOKENS", DatatypeImplementation.c_NMTOKENS, 11),
				new DatatypeImplementation.SchemaDatatypeMap("NOTATION", DatatypeImplementation.c_NOTATION, 11),
				new DatatypeImplementation.SchemaDatatypeMap("Name", DatatypeImplementation.c_Name, 40),
				new DatatypeImplementation.SchemaDatatypeMap("QName", DatatypeImplementation.c_QName, 11),
				new DatatypeImplementation.SchemaDatatypeMap("anySimpleType", DatatypeImplementation.c_anySimpleType, -1),
				new DatatypeImplementation.SchemaDatatypeMap("anyURI", DatatypeImplementation.c_anyURI, 11),
				new DatatypeImplementation.SchemaDatatypeMap("base64Binary", DatatypeImplementation.c_base64Binary, 11),
				new DatatypeImplementation.SchemaDatatypeMap("boolean", DatatypeImplementation.c_boolean, 11),
				new DatatypeImplementation.SchemaDatatypeMap("byte", DatatypeImplementation.c_byte, 37),
				new DatatypeImplementation.SchemaDatatypeMap("date", DatatypeImplementation.c_date, 11),
				new DatatypeImplementation.SchemaDatatypeMap("dateTime", DatatypeImplementation.c_dateTime, 11),
				new DatatypeImplementation.SchemaDatatypeMap("decimal", DatatypeImplementation.c_decimal, 11),
				new DatatypeImplementation.SchemaDatatypeMap("double", DatatypeImplementation.c_double, 11),
				new DatatypeImplementation.SchemaDatatypeMap("duration", DatatypeImplementation.c_duration, 11),
				new DatatypeImplementation.SchemaDatatypeMap("float", DatatypeImplementation.c_float, 11),
				new DatatypeImplementation.SchemaDatatypeMap("gDay", DatatypeImplementation.c_day, 11),
				new DatatypeImplementation.SchemaDatatypeMap("gMonth", DatatypeImplementation.c_month, 11),
				new DatatypeImplementation.SchemaDatatypeMap("gMonthDay", DatatypeImplementation.c_monthDay, 11),
				new DatatypeImplementation.SchemaDatatypeMap("gYear", DatatypeImplementation.c_year, 11),
				new DatatypeImplementation.SchemaDatatypeMap("gYearMonth", DatatypeImplementation.c_yearMonth, 11),
				new DatatypeImplementation.SchemaDatatypeMap("hexBinary", DatatypeImplementation.c_hexBinary, 11),
				new DatatypeImplementation.SchemaDatatypeMap("int", DatatypeImplementation.c_int, 31),
				new DatatypeImplementation.SchemaDatatypeMap("integer", DatatypeImplementation.c_integer, 18),
				new DatatypeImplementation.SchemaDatatypeMap("language", DatatypeImplementation.c_language, 40),
				new DatatypeImplementation.SchemaDatatypeMap("long", DatatypeImplementation.c_long, 29),
				new DatatypeImplementation.SchemaDatatypeMap("negativeInteger", DatatypeImplementation.c_negativeInteger, 34),
				new DatatypeImplementation.SchemaDatatypeMap("nonNegativeInteger", DatatypeImplementation.c_nonNegativeInteger, 29),
				new DatatypeImplementation.SchemaDatatypeMap("nonPositiveInteger", DatatypeImplementation.c_nonPositiveInteger, 29),
				new DatatypeImplementation.SchemaDatatypeMap("normalizedString", DatatypeImplementation.c_normalizedString, 38),
				new DatatypeImplementation.SchemaDatatypeMap("positiveInteger", DatatypeImplementation.c_positiveInteger, 33),
				new DatatypeImplementation.SchemaDatatypeMap("short", DatatypeImplementation.c_short, 28),
				new DatatypeImplementation.SchemaDatatypeMap("string", DatatypeImplementation.c_string, 11),
				new DatatypeImplementation.SchemaDatatypeMap("time", DatatypeImplementation.c_time, 11),
				new DatatypeImplementation.SchemaDatatypeMap("token", DatatypeImplementation.c_token, 35),
				new DatatypeImplementation.SchemaDatatypeMap("unsignedByte", DatatypeImplementation.c_unsignedByte, 44),
				new DatatypeImplementation.SchemaDatatypeMap("unsignedInt", DatatypeImplementation.c_unsignedInt, 43),
				new DatatypeImplementation.SchemaDatatypeMap("unsignedLong", DatatypeImplementation.c_unsignedLong, 33),
				new DatatypeImplementation.SchemaDatatypeMap("unsignedShort", DatatypeImplementation.c_unsignedShort, 42)
			};
			DatatypeImplementation.CreateBuiltinTypes();
		}

		// Token: 0x17000954 RID: 2388
		// (get) Token: 0x06003489 RID: 13449 RVA: 0x0012A062 File Offset: 0x00128262
		internal static XmlSchemaSimpleType AnySimpleType
		{
			get
			{
				return DatatypeImplementation.anySimpleType;
			}
		}

		// Token: 0x17000955 RID: 2389
		// (get) Token: 0x0600348A RID: 13450 RVA: 0x0012A069 File Offset: 0x00128269
		internal static XmlSchemaSimpleType AnyAtomicType
		{
			get
			{
				return DatatypeImplementation.anyAtomicType;
			}
		}

		// Token: 0x17000956 RID: 2390
		// (get) Token: 0x0600348B RID: 13451 RVA: 0x0012A070 File Offset: 0x00128270
		internal static XmlSchemaSimpleType UntypedAtomicType
		{
			get
			{
				return DatatypeImplementation.untypedAtomicType;
			}
		}

		// Token: 0x17000957 RID: 2391
		// (get) Token: 0x0600348C RID: 13452 RVA: 0x0012A077 File Offset: 0x00128277
		internal static XmlSchemaSimpleType YearMonthDurationType
		{
			get
			{
				return DatatypeImplementation.yearMonthDurationType;
			}
		}

		// Token: 0x17000958 RID: 2392
		// (get) Token: 0x0600348D RID: 13453 RVA: 0x0012A07E File Offset: 0x0012827E
		internal static XmlSchemaSimpleType DayTimeDurationType
		{
			get
			{
				return DatatypeImplementation.dayTimeDurationType;
			}
		}

		// Token: 0x0600348E RID: 13454 RVA: 0x0012A085 File Offset: 0x00128285
		internal new static DatatypeImplementation FromXmlTokenizedType(XmlTokenizedType token)
		{
			return DatatypeImplementation.c_tokenizedTypes[(int)token];
		}

		// Token: 0x0600348F RID: 13455 RVA: 0x0012A08E File Offset: 0x0012828E
		internal new static DatatypeImplementation FromXmlTokenizedTypeXsd(XmlTokenizedType token)
		{
			return DatatypeImplementation.c_tokenizedTypesXsd[(int)token];
		}

		// Token: 0x06003490 RID: 13456 RVA: 0x0012A098 File Offset: 0x00128298
		internal new static DatatypeImplementation FromXdrName(string name)
		{
			int num = Array.BinarySearch(DatatypeImplementation.c_XdrTypes, name, null);
			if (num >= 0)
			{
				return (DatatypeImplementation)DatatypeImplementation.c_XdrTypes[num];
			}
			return null;
		}

		// Token: 0x06003491 RID: 13457 RVA: 0x0012A0C4 File Offset: 0x001282C4
		private static DatatypeImplementation FromTypeName(string name)
		{
			int num = Array.BinarySearch(DatatypeImplementation.c_XsdTypes, name, null);
			if (num >= 0)
			{
				return (DatatypeImplementation)DatatypeImplementation.c_XsdTypes[num];
			}
			return null;
		}

		// Token: 0x06003492 RID: 13458 RVA: 0x0012A0F0 File Offset: 0x001282F0
		internal static XmlSchemaSimpleType StartBuiltinType(XmlQualifiedName qname, XmlSchemaDatatype dataType)
		{
			XmlSchemaSimpleType xmlSchemaSimpleType = new XmlSchemaSimpleType();
			xmlSchemaSimpleType.SetQualifiedName(qname);
			xmlSchemaSimpleType.SetDatatype(dataType);
			xmlSchemaSimpleType.ElementDecl = new SchemaElementDecl(dataType);
			xmlSchemaSimpleType.ElementDecl.SchemaType = xmlSchemaSimpleType;
			return xmlSchemaSimpleType;
		}

		// Token: 0x06003493 RID: 13459 RVA: 0x0012A12C File Offset: 0x0012832C
		internal static void FinishBuiltinType(XmlSchemaSimpleType derivedType, XmlSchemaSimpleType baseType)
		{
			derivedType.SetBaseSchemaType(baseType);
			derivedType.SetDerivedBy(XmlSchemaDerivationMethod.Restriction);
			if (derivedType.Datatype.Variety == XmlSchemaDatatypeVariety.Atomic)
			{
				derivedType.Content = new XmlSchemaSimpleTypeRestriction
				{
					BaseTypeName = baseType.QualifiedName
				};
			}
			if (derivedType.Datatype.Variety == XmlSchemaDatatypeVariety.List)
			{
				XmlSchemaSimpleTypeList xmlSchemaSimpleTypeList = new XmlSchemaSimpleTypeList();
				derivedType.SetDerivedBy(XmlSchemaDerivationMethod.List);
				XmlTypeCode typeCode = derivedType.Datatype.TypeCode;
				if (typeCode != XmlTypeCode.NmToken)
				{
					if (typeCode != XmlTypeCode.Idref)
					{
						if (typeCode == XmlTypeCode.Entity)
						{
							xmlSchemaSimpleTypeList.ItemType = (xmlSchemaSimpleTypeList.BaseItemType = DatatypeImplementation.enumToTypeCode[39]);
						}
					}
					else
					{
						xmlSchemaSimpleTypeList.ItemType = (xmlSchemaSimpleTypeList.BaseItemType = DatatypeImplementation.enumToTypeCode[38]);
					}
				}
				else
				{
					xmlSchemaSimpleTypeList.ItemType = (xmlSchemaSimpleTypeList.BaseItemType = DatatypeImplementation.enumToTypeCode[34]);
				}
				derivedType.Content = xmlSchemaSimpleTypeList;
			}
		}

		// Token: 0x06003494 RID: 13460 RVA: 0x0012A1F8 File Offset: 0x001283F8
		internal static void CreateBuiltinTypes()
		{
			DatatypeImplementation.SchemaDatatypeMap schemaDatatypeMap = DatatypeImplementation.c_XsdTypes[11];
			XmlQualifiedName xmlQualifiedName = new XmlQualifiedName(schemaDatatypeMap.Name, "http://www.w3.org/2001/XMLSchema");
			DatatypeImplementation datatypeImplementation = DatatypeImplementation.FromTypeName(xmlQualifiedName.Name);
			DatatypeImplementation.anySimpleType = DatatypeImplementation.StartBuiltinType(xmlQualifiedName, datatypeImplementation);
			datatypeImplementation.parentSchemaType = DatatypeImplementation.anySimpleType;
			DatatypeImplementation.builtinTypes.Add(xmlQualifiedName, DatatypeImplementation.anySimpleType);
			for (int i = 0; i < DatatypeImplementation.c_XsdTypes.Length; i++)
			{
				if (i != 11)
				{
					schemaDatatypeMap = DatatypeImplementation.c_XsdTypes[i];
					xmlQualifiedName = new XmlQualifiedName(schemaDatatypeMap.Name, "http://www.w3.org/2001/XMLSchema");
					datatypeImplementation = DatatypeImplementation.FromTypeName(xmlQualifiedName.Name);
					XmlSchemaSimpleType xmlSchemaSimpleType = DatatypeImplementation.StartBuiltinType(xmlQualifiedName, datatypeImplementation);
					datatypeImplementation.parentSchemaType = xmlSchemaSimpleType;
					DatatypeImplementation.builtinTypes.Add(xmlQualifiedName, xmlSchemaSimpleType);
					if (datatypeImplementation.variety == XmlSchemaDatatypeVariety.Atomic)
					{
						DatatypeImplementation.enumToTypeCode[(int)datatypeImplementation.TypeCode] = xmlSchemaSimpleType;
					}
				}
			}
			for (int j = 0; j < DatatypeImplementation.c_XsdTypes.Length; j++)
			{
				if (j != 11)
				{
					schemaDatatypeMap = DatatypeImplementation.c_XsdTypes[j];
					XmlSchemaSimpleType derivedType = (XmlSchemaSimpleType)DatatypeImplementation.builtinTypes[new XmlQualifiedName(schemaDatatypeMap.Name, "http://www.w3.org/2001/XMLSchema")];
					if (schemaDatatypeMap.ParentIndex == 11)
					{
						DatatypeImplementation.FinishBuiltinType(derivedType, DatatypeImplementation.anySimpleType);
					}
					else
					{
						XmlSchemaSimpleType xmlSchemaSimpleType2 = (XmlSchemaSimpleType)DatatypeImplementation.builtinTypes[new XmlQualifiedName(DatatypeImplementation.c_XsdTypes[schemaDatatypeMap.ParentIndex].Name, "http://www.w3.org/2001/XMLSchema")];
						DatatypeImplementation.FinishBuiltinType(derivedType, xmlSchemaSimpleType2);
					}
				}
			}
			xmlQualifiedName = new XmlQualifiedName("anyAtomicType", "http://www.w3.org/2003/11/xpath-datatypes");
			DatatypeImplementation.anyAtomicType = DatatypeImplementation.StartBuiltinType(xmlQualifiedName, DatatypeImplementation.c_anyAtomicType);
			DatatypeImplementation.c_anyAtomicType.parentSchemaType = DatatypeImplementation.anyAtomicType;
			DatatypeImplementation.FinishBuiltinType(DatatypeImplementation.anyAtomicType, DatatypeImplementation.anySimpleType);
			DatatypeImplementation.builtinTypes.Add(xmlQualifiedName, DatatypeImplementation.anyAtomicType);
			DatatypeImplementation.enumToTypeCode[10] = DatatypeImplementation.anyAtomicType;
			xmlQualifiedName = new XmlQualifiedName("untypedAtomic", "http://www.w3.org/2003/11/xpath-datatypes");
			DatatypeImplementation.untypedAtomicType = DatatypeImplementation.StartBuiltinType(xmlQualifiedName, DatatypeImplementation.c_untypedAtomicType);
			DatatypeImplementation.c_untypedAtomicType.parentSchemaType = DatatypeImplementation.untypedAtomicType;
			DatatypeImplementation.FinishBuiltinType(DatatypeImplementation.untypedAtomicType, DatatypeImplementation.anyAtomicType);
			DatatypeImplementation.builtinTypes.Add(xmlQualifiedName, DatatypeImplementation.untypedAtomicType);
			DatatypeImplementation.enumToTypeCode[11] = DatatypeImplementation.untypedAtomicType;
			xmlQualifiedName = new XmlQualifiedName("yearMonthDuration", "http://www.w3.org/2003/11/xpath-datatypes");
			DatatypeImplementation.yearMonthDurationType = DatatypeImplementation.StartBuiltinType(xmlQualifiedName, DatatypeImplementation.c_yearMonthDuration);
			DatatypeImplementation.c_yearMonthDuration.parentSchemaType = DatatypeImplementation.yearMonthDurationType;
			DatatypeImplementation.FinishBuiltinType(DatatypeImplementation.yearMonthDurationType, DatatypeImplementation.enumToTypeCode[17]);
			DatatypeImplementation.builtinTypes.Add(xmlQualifiedName, DatatypeImplementation.yearMonthDurationType);
			DatatypeImplementation.enumToTypeCode[53] = DatatypeImplementation.yearMonthDurationType;
			xmlQualifiedName = new XmlQualifiedName("dayTimeDuration", "http://www.w3.org/2003/11/xpath-datatypes");
			DatatypeImplementation.dayTimeDurationType = DatatypeImplementation.StartBuiltinType(xmlQualifiedName, DatatypeImplementation.c_dayTimeDuration);
			DatatypeImplementation.c_dayTimeDuration.parentSchemaType = DatatypeImplementation.dayTimeDurationType;
			DatatypeImplementation.FinishBuiltinType(DatatypeImplementation.dayTimeDurationType, DatatypeImplementation.enumToTypeCode[17]);
			DatatypeImplementation.builtinTypes.Add(xmlQualifiedName, DatatypeImplementation.dayTimeDurationType);
			DatatypeImplementation.enumToTypeCode[54] = DatatypeImplementation.dayTimeDurationType;
		}

		// Token: 0x06003495 RID: 13461 RVA: 0x0012A4CF File Offset: 0x001286CF
		internal static XmlSchemaSimpleType GetSimpleTypeFromTypeCode(XmlTypeCode typeCode)
		{
			return DatatypeImplementation.enumToTypeCode[(int)typeCode];
		}

		// Token: 0x06003496 RID: 13462 RVA: 0x0012A4D8 File Offset: 0x001286D8
		internal static XmlSchemaSimpleType GetSimpleTypeFromXsdType(XmlQualifiedName qname)
		{
			return (XmlSchemaSimpleType)DatatypeImplementation.builtinTypes[qname];
		}

		// Token: 0x06003497 RID: 13463 RVA: 0x0012A4EC File Offset: 0x001286EC
		internal static XmlSchemaSimpleType GetNormalizedStringTypeV1Compat()
		{
			if (DatatypeImplementation.normalizedStringTypeV1Compat == null)
			{
				XmlSchemaSimpleType xmlSchemaSimpleType = DatatypeImplementation.GetSimpleTypeFromTypeCode(XmlTypeCode.NormalizedString).Clone() as XmlSchemaSimpleType;
				xmlSchemaSimpleType.SetDatatype(DatatypeImplementation.c_normalizedStringV1Compat);
				xmlSchemaSimpleType.ElementDecl = new SchemaElementDecl(DatatypeImplementation.c_normalizedStringV1Compat);
				xmlSchemaSimpleType.ElementDecl.SchemaType = xmlSchemaSimpleType;
				DatatypeImplementation.normalizedStringTypeV1Compat = xmlSchemaSimpleType;
			}
			return DatatypeImplementation.normalizedStringTypeV1Compat;
		}

		// Token: 0x06003498 RID: 13464 RVA: 0x0012A54C File Offset: 0x0012874C
		internal static XmlSchemaSimpleType GetTokenTypeV1Compat()
		{
			if (DatatypeImplementation.tokenTypeV1Compat == null)
			{
				XmlSchemaSimpleType xmlSchemaSimpleType = DatatypeImplementation.GetSimpleTypeFromTypeCode(XmlTypeCode.Token).Clone() as XmlSchemaSimpleType;
				xmlSchemaSimpleType.SetDatatype(DatatypeImplementation.c_tokenV1Compat);
				xmlSchemaSimpleType.ElementDecl = new SchemaElementDecl(DatatypeImplementation.c_tokenV1Compat);
				xmlSchemaSimpleType.ElementDecl.SchemaType = xmlSchemaSimpleType;
				DatatypeImplementation.tokenTypeV1Compat = xmlSchemaSimpleType;
			}
			return DatatypeImplementation.tokenTypeV1Compat;
		}

		// Token: 0x06003499 RID: 13465 RVA: 0x0012A5AA File Offset: 0x001287AA
		internal static XmlSchemaSimpleType[] GetBuiltInTypes()
		{
			return DatatypeImplementation.enumToTypeCode;
		}

		// Token: 0x0600349A RID: 13466 RVA: 0x0012A5B4 File Offset: 0x001287B4
		internal static XmlTypeCode GetPrimitiveTypeCode(XmlTypeCode typeCode)
		{
			XmlSchemaSimpleType xmlSchemaSimpleType = DatatypeImplementation.enumToTypeCode[(int)typeCode];
			while (xmlSchemaSimpleType.BaseXmlSchemaType != DatatypeImplementation.AnySimpleType)
			{
				xmlSchemaSimpleType = (xmlSchemaSimpleType.BaseXmlSchemaType as XmlSchemaSimpleType);
			}
			return xmlSchemaSimpleType.TypeCode;
		}

		// Token: 0x0600349B RID: 13467 RVA: 0x0012A5EA File Offset: 0x001287EA
		internal override XmlSchemaDatatype DeriveByRestriction(XmlSchemaObjectCollection facets, XmlNameTable nameTable, XmlSchemaType schemaType)
		{
			DatatypeImplementation datatypeImplementation = (DatatypeImplementation)base.MemberwiseClone();
			datatypeImplementation.restriction = this.FacetsChecker.ConstructRestriction(this, facets, nameTable);
			datatypeImplementation.baseType = this;
			datatypeImplementation.parentSchemaType = schemaType;
			datatypeImplementation.valueConverter = null;
			return datatypeImplementation;
		}

		// Token: 0x0600349C RID: 13468 RVA: 0x0012A620 File Offset: 0x00128820
		internal override XmlSchemaDatatype DeriveByList(XmlSchemaType schemaType)
		{
			return this.DeriveByList(0, schemaType);
		}

		// Token: 0x0600349D RID: 13469 RVA: 0x0012A62C File Offset: 0x0012882C
		internal XmlSchemaDatatype DeriveByList(int minSize, XmlSchemaType schemaType)
		{
			if (this.variety == XmlSchemaDatatypeVariety.List)
			{
				throw new XmlSchemaException("A list data type must be derived from an atomic or union data type.", string.Empty);
			}
			if (this.variety == XmlSchemaDatatypeVariety.Union && !((Datatype_union)this).HasAtomicMembers())
			{
				throw new XmlSchemaException("A list data type must be derived from an atomic or union data type.", string.Empty);
			}
			return new Datatype_List(this, minSize)
			{
				variety = XmlSchemaDatatypeVariety.List,
				restriction = null,
				baseType = DatatypeImplementation.c_anySimpleType,
				parentSchemaType = schemaType
			};
		}

		// Token: 0x0600349E RID: 13470 RVA: 0x0012A69F File Offset: 0x0012889F
		internal new static DatatypeImplementation DeriveByUnion(XmlSchemaSimpleType[] types, XmlSchemaType schemaType)
		{
			return new Datatype_union(types)
			{
				baseType = DatatypeImplementation.c_anySimpleType,
				variety = XmlSchemaDatatypeVariety.Union,
				parentSchemaType = schemaType
			};
		}

		// Token: 0x0600349F RID: 13471 RVA: 0x0000B528 File Offset: 0x00009728
		internal override void VerifySchemaValid(XmlSchemaObjectTable notations, XmlSchemaObject caller)
		{
		}

		// Token: 0x060034A0 RID: 13472 RVA: 0x0012A6C0 File Offset: 0x001288C0
		public override bool IsDerivedFrom(XmlSchemaDatatype datatype)
		{
			if (datatype == null)
			{
				return false;
			}
			for (DatatypeImplementation datatypeImplementation = this; datatypeImplementation != null; datatypeImplementation = datatypeImplementation.baseType)
			{
				if (datatypeImplementation == datatype)
				{
					return true;
				}
			}
			if (((DatatypeImplementation)datatype).baseType == null)
			{
				Type type = base.GetType();
				Type type2 = datatype.GetType();
				return type2 == type || type.IsSubclassOf(type2);
			}
			if (datatype.Variety == XmlSchemaDatatypeVariety.Union && !datatype.HasLexicalFacets && !datatype.HasValueFacets && this.variety != XmlSchemaDatatypeVariety.Union)
			{
				return ((Datatype_union)datatype).IsUnionBaseOf(this);
			}
			return (this.variety == XmlSchemaDatatypeVariety.Union || this.variety == XmlSchemaDatatypeVariety.List) && this.restriction == null && datatype == DatatypeImplementation.anySimpleType.Datatype;
		}

		// Token: 0x060034A1 RID: 13473 RVA: 0x0012A76C File Offset: 0x0012896C
		internal override bool IsEqual(object o1, object o2)
		{
			return this.Compare(o1, o2) == 0;
		}

		// Token: 0x060034A2 RID: 13474 RVA: 0x0012A77C File Offset: 0x0012897C
		internal override bool IsComparable(XmlSchemaDatatype dtype)
		{
			XmlTypeCode typeCode = this.TypeCode;
			XmlTypeCode typeCode2 = dtype.TypeCode;
			return typeCode == typeCode2 || DatatypeImplementation.GetPrimitiveTypeCode(typeCode) == DatatypeImplementation.GetPrimitiveTypeCode(typeCode2) || (this.IsDerivedFrom(dtype) || dtype.IsDerivedFrom(this));
		}

		// Token: 0x060034A3 RID: 13475 RVA: 0x0001DA42 File Offset: 0x0001BC42
		internal virtual XmlValueConverter CreateValueConverter(XmlSchemaType schemaType)
		{
			return null;
		}

		// Token: 0x17000959 RID: 2393
		// (get) Token: 0x060034A4 RID: 13476 RVA: 0x0012A7C2 File Offset: 0x001289C2
		internal override FacetsChecker FacetsChecker
		{
			get
			{
				return DatatypeImplementation.miscFacetsChecker;
			}
		}

		// Token: 0x1700095A RID: 2394
		// (get) Token: 0x060034A5 RID: 13477 RVA: 0x0012A7C9 File Offset: 0x001289C9
		internal override XmlValueConverter ValueConverter
		{
			get
			{
				if (this.valueConverter == null)
				{
					this.valueConverter = this.CreateValueConverter(this.parentSchemaType);
				}
				return this.valueConverter;
			}
		}

		// Token: 0x1700095B RID: 2395
		// (get) Token: 0x060034A6 RID: 13478 RVA: 0x0000B2C4 File Offset: 0x000094C4
		public override XmlTokenizedType TokenizedType
		{
			get
			{
				return XmlTokenizedType.None;
			}
		}

		// Token: 0x1700095C RID: 2396
		// (get) Token: 0x060034A7 RID: 13479 RVA: 0x0001E52D File Offset: 0x0001C72D
		public override Type ValueType
		{
			get
			{
				return typeof(string);
			}
		}

		// Token: 0x1700095D RID: 2397
		// (get) Token: 0x060034A8 RID: 13480 RVA: 0x0012A7EB File Offset: 0x001289EB
		public override XmlSchemaDatatypeVariety Variety
		{
			get
			{
				return this.variety;
			}
		}

		// Token: 0x1700095E RID: 2398
		// (get) Token: 0x060034A9 RID: 13481 RVA: 0x0000D1C5 File Offset: 0x0000B3C5
		public override XmlTypeCode TypeCode
		{
			get
			{
				return XmlTypeCode.None;
			}
		}

		// Token: 0x1700095F RID: 2399
		// (get) Token: 0x060034AA RID: 13482 RVA: 0x0012A7F3 File Offset: 0x001289F3
		// (set) Token: 0x060034AB RID: 13483 RVA: 0x0012A7FB File Offset: 0x001289FB
		internal override RestrictionFacets Restriction
		{
			get
			{
				return this.restriction;
			}
			set
			{
				this.restriction = value;
			}
		}

		// Token: 0x17000960 RID: 2400
		// (get) Token: 0x060034AC RID: 13484 RVA: 0x0012A804 File Offset: 0x00128A04
		internal override bool HasLexicalFacets
		{
			get
			{
				RestrictionFlags restrictionFlags = (this.restriction != null) ? this.restriction.Flags : ((RestrictionFlags)0);
				return restrictionFlags != (RestrictionFlags)0 && (restrictionFlags & (RestrictionFlags.Pattern | RestrictionFlags.WhiteSpace | RestrictionFlags.TotalDigits | RestrictionFlags.FractionDigits)) != (RestrictionFlags)0;
			}
		}

		// Token: 0x17000961 RID: 2401
		// (get) Token: 0x060034AD RID: 13485 RVA: 0x0012A838 File Offset: 0x00128A38
		internal override bool HasValueFacets
		{
			get
			{
				RestrictionFlags restrictionFlags = (this.restriction != null) ? this.restriction.Flags : ((RestrictionFlags)0);
				return restrictionFlags != (RestrictionFlags)0 && (restrictionFlags & (RestrictionFlags.Length | RestrictionFlags.MinLength | RestrictionFlags.MaxLength | RestrictionFlags.Enumeration | RestrictionFlags.MaxInclusive | RestrictionFlags.MaxExclusive | RestrictionFlags.MinInclusive | RestrictionFlags.MinExclusive | RestrictionFlags.TotalDigits | RestrictionFlags.FractionDigits)) != (RestrictionFlags)0;
			}
		}

		// Token: 0x17000962 RID: 2402
		// (get) Token: 0x060034AE RID: 13486 RVA: 0x0012A86B File Offset: 0x00128A6B
		protected DatatypeImplementation Base
		{
			get
			{
				return this.baseType;
			}
		}

		// Token: 0x17000963 RID: 2403
		// (get) Token: 0x060034AF RID: 13487
		internal abstract Type ListValueType { get; }

		// Token: 0x17000964 RID: 2404
		// (get) Token: 0x060034B0 RID: 13488
		internal abstract RestrictionFlags ValidRestrictionFlags { get; }

		// Token: 0x17000965 RID: 2405
		// (get) Token: 0x060034B1 RID: 13489 RVA: 0x0000D1C5 File Offset: 0x0000B3C5
		internal override XmlSchemaWhiteSpace BuiltInWhitespaceFacet
		{
			get
			{
				return XmlSchemaWhiteSpace.Preserve;
			}
		}

		// Token: 0x060034B2 RID: 13490 RVA: 0x0012A873 File Offset: 0x00128A73
		internal override object ParseValue(string s, Type typDest, XmlNameTable nameTable, IXmlNamespaceResolver nsmgr)
		{
			return this.ValueConverter.ChangeType(this.ParseValue(s, nameTable, nsmgr), typDest, nsmgr);
		}

		// Token: 0x060034B3 RID: 13491 RVA: 0x0012A890 File Offset: 0x00128A90
		public override object ParseValue(string s, XmlNameTable nameTable, IXmlNamespaceResolver nsmgr)
		{
			object obj;
			Exception ex = this.TryParseValue(s, nameTable, nsmgr, out obj);
			if (ex != null)
			{
				throw new XmlSchemaException("The value '{0}' is invalid according to its schema type '{1}' - {2}", new string[]
				{
					s,
					this.GetTypeName(),
					ex.Message
				}, ex, null, 0, 0, null);
			}
			if (this.Variety == XmlSchemaDatatypeVariety.Union)
			{
				return (obj as XsdSimpleValue).TypedValue;
			}
			return obj;
		}

		// Token: 0x060034B4 RID: 13492 RVA: 0x0012A8F0 File Offset: 0x00128AF0
		internal override object ParseValue(string s, XmlNameTable nameTable, IXmlNamespaceResolver nsmgr, bool createAtomicValue)
		{
			if (!createAtomicValue)
			{
				return this.ParseValue(s, nameTable, nsmgr);
			}
			object result;
			Exception ex = this.TryParseValue(s, nameTable, nsmgr, out result);
			if (ex != null)
			{
				throw new XmlSchemaException("The value '{0}' is invalid according to its schema type '{1}' - {2}", new string[]
				{
					s,
					this.GetTypeName(),
					ex.Message
				}, ex, null, 0, 0, null);
			}
			return result;
		}

		// Token: 0x060034B5 RID: 13493 RVA: 0x0012A948 File Offset: 0x00128B48
		internal override Exception TryParseValue(object value, XmlNameTable nameTable, IXmlNamespaceResolver namespaceResolver, out object typedValue)
		{
			Exception ex = null;
			typedValue = null;
			if (value == null)
			{
				return new ArgumentNullException("value");
			}
			string text = value as string;
			if (text != null)
			{
				return this.TryParseValue(text, nameTable, namespaceResolver, out typedValue);
			}
			try
			{
				object obj = value;
				if (value.GetType() != this.ValueType)
				{
					obj = this.ValueConverter.ChangeType(value, this.ValueType, namespaceResolver);
				}
				if (this.HasLexicalFacets)
				{
					string text2 = (string)this.ValueConverter.ChangeType(value, typeof(string), namespaceResolver);
					ex = this.FacetsChecker.CheckLexicalFacets(ref text2, this);
					if (ex != null)
					{
						return ex;
					}
				}
				if (this.HasValueFacets)
				{
					ex = this.FacetsChecker.CheckValueFacets(obj, this);
					if (ex != null)
					{
						return ex;
					}
				}
				typedValue = obj;
				return null;
			}
			catch (FormatException ex)
			{
			}
			catch (InvalidCastException ex)
			{
			}
			catch (OverflowException ex)
			{
			}
			catch (ArgumentException ex)
			{
			}
			return ex;
		}

		// Token: 0x060034B6 RID: 13494 RVA: 0x0012AA4C File Offset: 0x00128C4C
		internal string GetTypeName()
		{
			XmlSchemaType xmlSchemaType = this.parentSchemaType;
			string result;
			if (xmlSchemaType == null || xmlSchemaType.QualifiedName.IsEmpty)
			{
				result = base.TypeCodeString;
			}
			else
			{
				result = xmlSchemaType.QualifiedName.ToString();
			}
			return result;
		}

		// Token: 0x060034B7 RID: 13495 RVA: 0x0012AA88 File Offset: 0x00128C88
		protected int Compare(byte[] value1, byte[] value2)
		{
			int num = value1.Length;
			if (num != value2.Length)
			{
				return -1;
			}
			for (int i = 0; i < num; i++)
			{
				if (value1[i] != value2[i])
				{
					return -1;
				}
			}
			return 0;
		}

		// Token: 0x060034B8 RID: 13496 RVA: 0x0012AAB8 File Offset: 0x00128CB8
		protected DatatypeImplementation()
		{
		}

		// Token: 0x04002728 RID: 10024
		private XmlSchemaDatatypeVariety variety;

		// Token: 0x04002729 RID: 10025
		private RestrictionFacets restriction;

		// Token: 0x0400272A RID: 10026
		private DatatypeImplementation baseType;

		// Token: 0x0400272B RID: 10027
		private XmlValueConverter valueConverter;

		// Token: 0x0400272C RID: 10028
		private XmlSchemaType parentSchemaType;

		// Token: 0x0400272D RID: 10029
		private static Hashtable builtinTypes = new Hashtable();

		// Token: 0x0400272E RID: 10030
		private static XmlSchemaSimpleType[] enumToTypeCode = new XmlSchemaSimpleType[55];

		// Token: 0x0400272F RID: 10031
		private static XmlSchemaSimpleType anySimpleType;

		// Token: 0x04002730 RID: 10032
		private static XmlSchemaSimpleType anyAtomicType;

		// Token: 0x04002731 RID: 10033
		private static XmlSchemaSimpleType untypedAtomicType;

		// Token: 0x04002732 RID: 10034
		private static XmlSchemaSimpleType yearMonthDurationType;

		// Token: 0x04002733 RID: 10035
		private static XmlSchemaSimpleType dayTimeDurationType;

		// Token: 0x04002734 RID: 10036
		private static volatile XmlSchemaSimpleType normalizedStringTypeV1Compat;

		// Token: 0x04002735 RID: 10037
		private static volatile XmlSchemaSimpleType tokenTypeV1Compat;

		// Token: 0x04002736 RID: 10038
		private const int anySimpleTypeIndex = 11;

		// Token: 0x04002737 RID: 10039
		internal static XmlQualifiedName QnAnySimpleType = new XmlQualifiedName("anySimpleType", "http://www.w3.org/2001/XMLSchema");

		// Token: 0x04002738 RID: 10040
		internal static XmlQualifiedName QnAnyType = new XmlQualifiedName("anyType", "http://www.w3.org/2001/XMLSchema");

		// Token: 0x04002739 RID: 10041
		internal static FacetsChecker stringFacetsChecker = new StringFacetsChecker();

		// Token: 0x0400273A RID: 10042
		internal static FacetsChecker miscFacetsChecker = new MiscFacetsChecker();

		// Token: 0x0400273B RID: 10043
		internal static FacetsChecker numeric2FacetsChecker = new Numeric2FacetsChecker();

		// Token: 0x0400273C RID: 10044
		internal static FacetsChecker binaryFacetsChecker = new BinaryFacetsChecker();

		// Token: 0x0400273D RID: 10045
		internal static FacetsChecker dateTimeFacetsChecker = new DateTimeFacetsChecker();

		// Token: 0x0400273E RID: 10046
		internal static FacetsChecker durationFacetsChecker = new DurationFacetsChecker();

		// Token: 0x0400273F RID: 10047
		internal static FacetsChecker listFacetsChecker = new ListFacetsChecker();

		// Token: 0x04002740 RID: 10048
		internal static FacetsChecker qnameFacetsChecker = new QNameFacetsChecker();

		// Token: 0x04002741 RID: 10049
		internal static FacetsChecker unionFacetsChecker = new UnionFacetsChecker();

		// Token: 0x04002742 RID: 10050
		private static readonly DatatypeImplementation c_anySimpleType = new Datatype_anySimpleType();

		// Token: 0x04002743 RID: 10051
		private static readonly DatatypeImplementation c_anyURI = new Datatype_anyURI();

		// Token: 0x04002744 RID: 10052
		private static readonly DatatypeImplementation c_base64Binary = new Datatype_base64Binary();

		// Token: 0x04002745 RID: 10053
		private static readonly DatatypeImplementation c_boolean = new Datatype_boolean();

		// Token: 0x04002746 RID: 10054
		private static readonly DatatypeImplementation c_byte = new Datatype_byte();

		// Token: 0x04002747 RID: 10055
		private static readonly DatatypeImplementation c_char = new Datatype_char();

		// Token: 0x04002748 RID: 10056
		private static readonly DatatypeImplementation c_date = new Datatype_date();

		// Token: 0x04002749 RID: 10057
		private static readonly DatatypeImplementation c_dateTime = new Datatype_dateTime();

		// Token: 0x0400274A RID: 10058
		private static readonly DatatypeImplementation c_dateTimeNoTz = new Datatype_dateTimeNoTimeZone();

		// Token: 0x0400274B RID: 10059
		private static readonly DatatypeImplementation c_dateTimeTz = new Datatype_dateTimeTimeZone();

		// Token: 0x0400274C RID: 10060
		private static readonly DatatypeImplementation c_day = new Datatype_day();

		// Token: 0x0400274D RID: 10061
		private static readonly DatatypeImplementation c_decimal = new Datatype_decimal();

		// Token: 0x0400274E RID: 10062
		private static readonly DatatypeImplementation c_double = new Datatype_double();

		// Token: 0x0400274F RID: 10063
		private static readonly DatatypeImplementation c_doubleXdr = new Datatype_doubleXdr();

		// Token: 0x04002750 RID: 10064
		private static readonly DatatypeImplementation c_duration = new Datatype_duration();

		// Token: 0x04002751 RID: 10065
		private static readonly DatatypeImplementation c_ENTITY = new Datatype_ENTITY();

		// Token: 0x04002752 RID: 10066
		private static readonly DatatypeImplementation c_ENTITIES = (DatatypeImplementation)DatatypeImplementation.c_ENTITY.DeriveByList(1, null);

		// Token: 0x04002753 RID: 10067
		private static readonly DatatypeImplementation c_ENUMERATION = new Datatype_ENUMERATION();

		// Token: 0x04002754 RID: 10068
		private static readonly DatatypeImplementation c_fixed = new Datatype_fixed();

		// Token: 0x04002755 RID: 10069
		private static readonly DatatypeImplementation c_float = new Datatype_float();

		// Token: 0x04002756 RID: 10070
		private static readonly DatatypeImplementation c_floatXdr = new Datatype_floatXdr();

		// Token: 0x04002757 RID: 10071
		private static readonly DatatypeImplementation c_hexBinary = new Datatype_hexBinary();

		// Token: 0x04002758 RID: 10072
		private static readonly DatatypeImplementation c_ID = new Datatype_ID();

		// Token: 0x04002759 RID: 10073
		private static readonly DatatypeImplementation c_IDREF = new Datatype_IDREF();

		// Token: 0x0400275A RID: 10074
		private static readonly DatatypeImplementation c_IDREFS = (DatatypeImplementation)DatatypeImplementation.c_IDREF.DeriveByList(1, null);

		// Token: 0x0400275B RID: 10075
		private static readonly DatatypeImplementation c_int = new Datatype_int();

		// Token: 0x0400275C RID: 10076
		private static readonly DatatypeImplementation c_integer = new Datatype_integer();

		// Token: 0x0400275D RID: 10077
		private static readonly DatatypeImplementation c_language = new Datatype_language();

		// Token: 0x0400275E RID: 10078
		private static readonly DatatypeImplementation c_long = new Datatype_long();

		// Token: 0x0400275F RID: 10079
		private static readonly DatatypeImplementation c_month = new Datatype_month();

		// Token: 0x04002760 RID: 10080
		private static readonly DatatypeImplementation c_monthDay = new Datatype_monthDay();

		// Token: 0x04002761 RID: 10081
		private static readonly DatatypeImplementation c_Name = new Datatype_Name();

		// Token: 0x04002762 RID: 10082
		private static readonly DatatypeImplementation c_NCName = new Datatype_NCName();

		// Token: 0x04002763 RID: 10083
		private static readonly DatatypeImplementation c_negativeInteger = new Datatype_negativeInteger();

		// Token: 0x04002764 RID: 10084
		private static readonly DatatypeImplementation c_NMTOKEN = new Datatype_NMTOKEN();

		// Token: 0x04002765 RID: 10085
		private static readonly DatatypeImplementation c_NMTOKENS = (DatatypeImplementation)DatatypeImplementation.c_NMTOKEN.DeriveByList(1, null);

		// Token: 0x04002766 RID: 10086
		private static readonly DatatypeImplementation c_nonNegativeInteger = new Datatype_nonNegativeInteger();

		// Token: 0x04002767 RID: 10087
		private static readonly DatatypeImplementation c_nonPositiveInteger = new Datatype_nonPositiveInteger();

		// Token: 0x04002768 RID: 10088
		private static readonly DatatypeImplementation c_normalizedString = new Datatype_normalizedString();

		// Token: 0x04002769 RID: 10089
		private static readonly DatatypeImplementation c_NOTATION = new Datatype_NOTATION();

		// Token: 0x0400276A RID: 10090
		private static readonly DatatypeImplementation c_positiveInteger = new Datatype_positiveInteger();

		// Token: 0x0400276B RID: 10091
		private static readonly DatatypeImplementation c_QName = new Datatype_QName();

		// Token: 0x0400276C RID: 10092
		private static readonly DatatypeImplementation c_QNameXdr = new Datatype_QNameXdr();

		// Token: 0x0400276D RID: 10093
		private static readonly DatatypeImplementation c_short = new Datatype_short();

		// Token: 0x0400276E RID: 10094
		private static readonly DatatypeImplementation c_string = new Datatype_string();

		// Token: 0x0400276F RID: 10095
		private static readonly DatatypeImplementation c_time = new Datatype_time();

		// Token: 0x04002770 RID: 10096
		private static readonly DatatypeImplementation c_timeNoTz = new Datatype_timeNoTimeZone();

		// Token: 0x04002771 RID: 10097
		private static readonly DatatypeImplementation c_timeTz = new Datatype_timeTimeZone();

		// Token: 0x04002772 RID: 10098
		private static readonly DatatypeImplementation c_token = new Datatype_token();

		// Token: 0x04002773 RID: 10099
		private static readonly DatatypeImplementation c_unsignedByte = new Datatype_unsignedByte();

		// Token: 0x04002774 RID: 10100
		private static readonly DatatypeImplementation c_unsignedInt = new Datatype_unsignedInt();

		// Token: 0x04002775 RID: 10101
		private static readonly DatatypeImplementation c_unsignedLong = new Datatype_unsignedLong();

		// Token: 0x04002776 RID: 10102
		private static readonly DatatypeImplementation c_unsignedShort = new Datatype_unsignedShort();

		// Token: 0x04002777 RID: 10103
		private static readonly DatatypeImplementation c_uuid = new Datatype_uuid();

		// Token: 0x04002778 RID: 10104
		private static readonly DatatypeImplementation c_year = new Datatype_year();

		// Token: 0x04002779 RID: 10105
		private static readonly DatatypeImplementation c_yearMonth = new Datatype_yearMonth();

		// Token: 0x0400277A RID: 10106
		internal static readonly DatatypeImplementation c_normalizedStringV1Compat = new Datatype_normalizedStringV1Compat();

		// Token: 0x0400277B RID: 10107
		internal static readonly DatatypeImplementation c_tokenV1Compat = new Datatype_tokenV1Compat();

		// Token: 0x0400277C RID: 10108
		private static readonly DatatypeImplementation c_anyAtomicType = new Datatype_anyAtomicType();

		// Token: 0x0400277D RID: 10109
		private static readonly DatatypeImplementation c_dayTimeDuration = new Datatype_dayTimeDuration();

		// Token: 0x0400277E RID: 10110
		private static readonly DatatypeImplementation c_untypedAtomicType = new Datatype_untypedAtomicType();

		// Token: 0x0400277F RID: 10111
		private static readonly DatatypeImplementation c_yearMonthDuration = new Datatype_yearMonthDuration();

		// Token: 0x04002780 RID: 10112
		private static readonly DatatypeImplementation[] c_tokenizedTypes;

		// Token: 0x04002781 RID: 10113
		private static readonly DatatypeImplementation[] c_tokenizedTypesXsd;

		// Token: 0x04002782 RID: 10114
		private static readonly DatatypeImplementation.SchemaDatatypeMap[] c_XdrTypes;

		// Token: 0x04002783 RID: 10115
		private static readonly DatatypeImplementation.SchemaDatatypeMap[] c_XsdTypes;

		// Token: 0x02000510 RID: 1296
		private class SchemaDatatypeMap : IComparable
		{
			// Token: 0x060034B9 RID: 13497 RVA: 0x0012AAC0 File Offset: 0x00128CC0
			internal SchemaDatatypeMap(string name, DatatypeImplementation type)
			{
				this.name = name;
				this.type = type;
			}

			// Token: 0x060034BA RID: 13498 RVA: 0x0012AAD6 File Offset: 0x00128CD6
			internal SchemaDatatypeMap(string name, DatatypeImplementation type, int parentIndex)
			{
				this.name = name;
				this.type = type;
				this.parentIndex = parentIndex;
			}

			// Token: 0x060034BB RID: 13499 RVA: 0x0012AAF3 File Offset: 0x00128CF3
			public static explicit operator DatatypeImplementation(DatatypeImplementation.SchemaDatatypeMap sdm)
			{
				return sdm.type;
			}

			// Token: 0x17000966 RID: 2406
			// (get) Token: 0x060034BC RID: 13500 RVA: 0x0012AAFB File Offset: 0x00128CFB
			public string Name
			{
				get
				{
					return this.name;
				}
			}

			// Token: 0x17000967 RID: 2407
			// (get) Token: 0x060034BD RID: 13501 RVA: 0x0012AB03 File Offset: 0x00128D03
			public int ParentIndex
			{
				get
				{
					return this.parentIndex;
				}
			}

			// Token: 0x060034BE RID: 13502 RVA: 0x0012AB0B File Offset: 0x00128D0B
			public int CompareTo(object obj)
			{
				return string.Compare(this.name, (string)obj, StringComparison.Ordinal);
			}

			// Token: 0x04002784 RID: 10116
			private string name;

			// Token: 0x04002785 RID: 10117
			private DatatypeImplementation type;

			// Token: 0x04002786 RID: 10118
			private int parentIndex;
		}
	}
}

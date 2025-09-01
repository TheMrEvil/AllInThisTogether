using System;

namespace System.Xml.Schema
{
	// Token: 0x02000578 RID: 1400
	internal sealed class SchemaNames
	{
		// Token: 0x17000A8F RID: 2703
		// (get) Token: 0x0600384B RID: 14411 RVA: 0x00140B8F File Offset: 0x0013ED8F
		public XmlNameTable NameTable
		{
			get
			{
				return this.nameTable;
			}
		}

		// Token: 0x0600384C RID: 14412 RVA: 0x00140B98 File Offset: 0x0013ED98
		public SchemaNames(XmlNameTable nameTable)
		{
			this.nameTable = nameTable;
			this.NsDataType = nameTable.Add("urn:schemas-microsoft-com:datatypes");
			this.NsDataTypeAlias = nameTable.Add("uuid:C2F41010-65B3-11D1-A29F-00AA00C14882");
			this.NsDataTypeOld = nameTable.Add("urn:uuid:C2F41010-65B3-11D1-A29F-00AA00C14882/");
			this.NsXml = nameTable.Add("http://www.w3.org/XML/1998/namespace");
			this.NsXmlNs = nameTable.Add("http://www.w3.org/2000/xmlns/");
			this.NsXdr = nameTable.Add("urn:schemas-microsoft-com:xml-data");
			this.NsXdrAlias = nameTable.Add("uuid:BDC6E3F0-6DA3-11D1-A2A3-00AA00C14882");
			this.NsXs = nameTable.Add("http://www.w3.org/2001/XMLSchema");
			this.NsXsi = nameTable.Add("http://www.w3.org/2001/XMLSchema-instance");
			this.XsiType = nameTable.Add("type");
			this.XsiNil = nameTable.Add("nil");
			this.XsiSchemaLocation = nameTable.Add("schemaLocation");
			this.XsiNoNamespaceSchemaLocation = nameTable.Add("noNamespaceSchemaLocation");
			this.XsdSchema = nameTable.Add("schema");
			this.XdrSchema = nameTable.Add("Schema");
			this.QnPCData = new XmlQualifiedName(nameTable.Add("#PCDATA"));
			this.QnXml = new XmlQualifiedName(nameTable.Add("xml"));
			this.QnXmlNs = new XmlQualifiedName(nameTable.Add("xmlns"), this.NsXmlNs);
			this.QnDtDt = new XmlQualifiedName(nameTable.Add("dt"), this.NsDataType);
			this.QnXmlLang = new XmlQualifiedName(nameTable.Add("lang"), this.NsXml);
			this.QnName = new XmlQualifiedName(nameTable.Add("name"));
			this.QnType = new XmlQualifiedName(nameTable.Add("type"));
			this.QnMaxOccurs = new XmlQualifiedName(nameTable.Add("maxOccurs"));
			this.QnMinOccurs = new XmlQualifiedName(nameTable.Add("minOccurs"));
			this.QnInfinite = new XmlQualifiedName(nameTable.Add("*"));
			this.QnModel = new XmlQualifiedName(nameTable.Add("model"));
			this.QnOpen = new XmlQualifiedName(nameTable.Add("open"));
			this.QnClosed = new XmlQualifiedName(nameTable.Add("closed"));
			this.QnContent = new XmlQualifiedName(nameTable.Add("content"));
			this.QnMixed = new XmlQualifiedName(nameTable.Add("mixed"));
			this.QnEmpty = new XmlQualifiedName(nameTable.Add("empty"));
			this.QnEltOnly = new XmlQualifiedName(nameTable.Add("eltOnly"));
			this.QnTextOnly = new XmlQualifiedName(nameTable.Add("textOnly"));
			this.QnOrder = new XmlQualifiedName(nameTable.Add("order"));
			this.QnSeq = new XmlQualifiedName(nameTable.Add("seq"));
			this.QnOne = new XmlQualifiedName(nameTable.Add("one"));
			this.QnMany = new XmlQualifiedName(nameTable.Add("many"));
			this.QnRequired = new XmlQualifiedName(nameTable.Add("required"));
			this.QnYes = new XmlQualifiedName(nameTable.Add("yes"));
			this.QnNo = new XmlQualifiedName(nameTable.Add("no"));
			this.QnString = new XmlQualifiedName(nameTable.Add("string"));
			this.QnID = new XmlQualifiedName(nameTable.Add("id"));
			this.QnIDRef = new XmlQualifiedName(nameTable.Add("idref"));
			this.QnIDRefs = new XmlQualifiedName(nameTable.Add("idrefs"));
			this.QnEntity = new XmlQualifiedName(nameTable.Add("entity"));
			this.QnEntities = new XmlQualifiedName(nameTable.Add("entities"));
			this.QnNmToken = new XmlQualifiedName(nameTable.Add("nmtoken"));
			this.QnNmTokens = new XmlQualifiedName(nameTable.Add("nmtokens"));
			this.QnEnumeration = new XmlQualifiedName(nameTable.Add("enumeration"));
			this.QnDefault = new XmlQualifiedName(nameTable.Add("default"));
			this.QnTargetNamespace = new XmlQualifiedName(nameTable.Add("targetNamespace"));
			this.QnVersion = new XmlQualifiedName(nameTable.Add("version"));
			this.QnFinalDefault = new XmlQualifiedName(nameTable.Add("finalDefault"));
			this.QnBlockDefault = new XmlQualifiedName(nameTable.Add("blockDefault"));
			this.QnFixed = new XmlQualifiedName(nameTable.Add("fixed"));
			this.QnAbstract = new XmlQualifiedName(nameTable.Add("abstract"));
			this.QnBlock = new XmlQualifiedName(nameTable.Add("block"));
			this.QnSubstitutionGroup = new XmlQualifiedName(nameTable.Add("substitutionGroup"));
			this.QnFinal = new XmlQualifiedName(nameTable.Add("final"));
			this.QnNillable = new XmlQualifiedName(nameTable.Add("nillable"));
			this.QnRef = new XmlQualifiedName(nameTable.Add("ref"));
			this.QnBase = new XmlQualifiedName(nameTable.Add("base"));
			this.QnDerivedBy = new XmlQualifiedName(nameTable.Add("derivedBy"));
			this.QnNamespace = new XmlQualifiedName(nameTable.Add("namespace"));
			this.QnProcessContents = new XmlQualifiedName(nameTable.Add("processContents"));
			this.QnRefer = new XmlQualifiedName(nameTable.Add("refer"));
			this.QnPublic = new XmlQualifiedName(nameTable.Add("public"));
			this.QnSystem = new XmlQualifiedName(nameTable.Add("system"));
			this.QnSchemaLocation = new XmlQualifiedName(nameTable.Add("schemaLocation"));
			this.QnValue = new XmlQualifiedName(nameTable.Add("value"));
			this.QnUse = new XmlQualifiedName(nameTable.Add("use"));
			this.QnForm = new XmlQualifiedName(nameTable.Add("form"));
			this.QnAttributeFormDefault = new XmlQualifiedName(nameTable.Add("attributeFormDefault"));
			this.QnElementFormDefault = new XmlQualifiedName(nameTable.Add("elementFormDefault"));
			this.QnSource = new XmlQualifiedName(nameTable.Add("source"));
			this.QnMemberTypes = new XmlQualifiedName(nameTable.Add("memberTypes"));
			this.QnItemType = new XmlQualifiedName(nameTable.Add("itemType"));
			this.QnXPath = new XmlQualifiedName(nameTable.Add("xpath"));
			this.QnXdrSchema = new XmlQualifiedName(this.XdrSchema, this.NsXdr);
			this.QnXdrElementType = new XmlQualifiedName(nameTable.Add("ElementType"), this.NsXdr);
			this.QnXdrElement = new XmlQualifiedName(nameTable.Add("element"), this.NsXdr);
			this.QnXdrGroup = new XmlQualifiedName(nameTable.Add("group"), this.NsXdr);
			this.QnXdrAttributeType = new XmlQualifiedName(nameTable.Add("AttributeType"), this.NsXdr);
			this.QnXdrAttribute = new XmlQualifiedName(nameTable.Add("attribute"), this.NsXdr);
			this.QnXdrDataType = new XmlQualifiedName(nameTable.Add("datatype"), this.NsXdr);
			this.QnXdrDescription = new XmlQualifiedName(nameTable.Add("description"), this.NsXdr);
			this.QnXdrExtends = new XmlQualifiedName(nameTable.Add("extends"), this.NsXdr);
			this.QnXdrAliasSchema = new XmlQualifiedName(nameTable.Add("Schema"), this.NsDataTypeAlias);
			this.QnDtType = new XmlQualifiedName(nameTable.Add("type"), this.NsDataType);
			this.QnDtValues = new XmlQualifiedName(nameTable.Add("values"), this.NsDataType);
			this.QnDtMaxLength = new XmlQualifiedName(nameTable.Add("maxLength"), this.NsDataType);
			this.QnDtMinLength = new XmlQualifiedName(nameTable.Add("minLength"), this.NsDataType);
			this.QnDtMax = new XmlQualifiedName(nameTable.Add("max"), this.NsDataType);
			this.QnDtMin = new XmlQualifiedName(nameTable.Add("min"), this.NsDataType);
			this.QnDtMinExclusive = new XmlQualifiedName(nameTable.Add("minExclusive"), this.NsDataType);
			this.QnDtMaxExclusive = new XmlQualifiedName(nameTable.Add("maxExclusive"), this.NsDataType);
			this.QnXsdSchema = new XmlQualifiedName(this.XsdSchema, this.NsXs);
			this.QnXsdAnnotation = new XmlQualifiedName(nameTable.Add("annotation"), this.NsXs);
			this.QnXsdInclude = new XmlQualifiedName(nameTable.Add("include"), this.NsXs);
			this.QnXsdImport = new XmlQualifiedName(nameTable.Add("import"), this.NsXs);
			this.QnXsdElement = new XmlQualifiedName(nameTable.Add("element"), this.NsXs);
			this.QnXsdAttribute = new XmlQualifiedName(nameTable.Add("attribute"), this.NsXs);
			this.QnXsdAttributeGroup = new XmlQualifiedName(nameTable.Add("attributeGroup"), this.NsXs);
			this.QnXsdAnyAttribute = new XmlQualifiedName(nameTable.Add("anyAttribute"), this.NsXs);
			this.QnXsdGroup = new XmlQualifiedName(nameTable.Add("group"), this.NsXs);
			this.QnXsdAll = new XmlQualifiedName(nameTable.Add("all"), this.NsXs);
			this.QnXsdChoice = new XmlQualifiedName(nameTable.Add("choice"), this.NsXs);
			this.QnXsdSequence = new XmlQualifiedName(nameTable.Add("sequence"), this.NsXs);
			this.QnXsdAny = new XmlQualifiedName(nameTable.Add("any"), this.NsXs);
			this.QnXsdNotation = new XmlQualifiedName(nameTable.Add("notation"), this.NsXs);
			this.QnXsdSimpleType = new XmlQualifiedName(nameTable.Add("simpleType"), this.NsXs);
			this.QnXsdComplexType = new XmlQualifiedName(nameTable.Add("complexType"), this.NsXs);
			this.QnXsdUnique = new XmlQualifiedName(nameTable.Add("unique"), this.NsXs);
			this.QnXsdKey = new XmlQualifiedName(nameTable.Add("key"), this.NsXs);
			this.QnXsdKeyRef = new XmlQualifiedName(nameTable.Add("keyref"), this.NsXs);
			this.QnXsdSelector = new XmlQualifiedName(nameTable.Add("selector"), this.NsXs);
			this.QnXsdField = new XmlQualifiedName(nameTable.Add("field"), this.NsXs);
			this.QnXsdMinExclusive = new XmlQualifiedName(nameTable.Add("minExclusive"), this.NsXs);
			this.QnXsdMinInclusive = new XmlQualifiedName(nameTable.Add("minInclusive"), this.NsXs);
			this.QnXsdMaxInclusive = new XmlQualifiedName(nameTable.Add("maxInclusive"), this.NsXs);
			this.QnXsdMaxExclusive = new XmlQualifiedName(nameTable.Add("maxExclusive"), this.NsXs);
			this.QnXsdTotalDigits = new XmlQualifiedName(nameTable.Add("totalDigits"), this.NsXs);
			this.QnXsdFractionDigits = new XmlQualifiedName(nameTable.Add("fractionDigits"), this.NsXs);
			this.QnXsdLength = new XmlQualifiedName(nameTable.Add("length"), this.NsXs);
			this.QnXsdMinLength = new XmlQualifiedName(nameTable.Add("minLength"), this.NsXs);
			this.QnXsdMaxLength = new XmlQualifiedName(nameTable.Add("maxLength"), this.NsXs);
			this.QnXsdEnumeration = new XmlQualifiedName(nameTable.Add("enumeration"), this.NsXs);
			this.QnXsdPattern = new XmlQualifiedName(nameTable.Add("pattern"), this.NsXs);
			this.QnXsdDocumentation = new XmlQualifiedName(nameTable.Add("documentation"), this.NsXs);
			this.QnXsdAppinfo = new XmlQualifiedName(nameTable.Add("appinfo"), this.NsXs);
			this.QnXsdComplexContent = new XmlQualifiedName(nameTable.Add("complexContent"), this.NsXs);
			this.QnXsdSimpleContent = new XmlQualifiedName(nameTable.Add("simpleContent"), this.NsXs);
			this.QnXsdRestriction = new XmlQualifiedName(nameTable.Add("restriction"), this.NsXs);
			this.QnXsdExtension = new XmlQualifiedName(nameTable.Add("extension"), this.NsXs);
			this.QnXsdUnion = new XmlQualifiedName(nameTable.Add("union"), this.NsXs);
			this.QnXsdList = new XmlQualifiedName(nameTable.Add("list"), this.NsXs);
			this.QnXsdWhiteSpace = new XmlQualifiedName(nameTable.Add("whiteSpace"), this.NsXs);
			this.QnXsdRedefine = new XmlQualifiedName(nameTable.Add("redefine"), this.NsXs);
			this.QnXsdAnyType = new XmlQualifiedName(nameTable.Add("anyType"), this.NsXs);
			this.CreateTokenToQNameTable();
		}

		// Token: 0x0600384D RID: 14413 RVA: 0x001418E4 File Offset: 0x0013FAE4
		public void CreateTokenToQNameTable()
		{
			this.TokenToQName[1] = this.QnName;
			this.TokenToQName[2] = this.QnType;
			this.TokenToQName[3] = this.QnMaxOccurs;
			this.TokenToQName[4] = this.QnMinOccurs;
			this.TokenToQName[5] = this.QnInfinite;
			this.TokenToQName[6] = this.QnModel;
			this.TokenToQName[7] = this.QnOpen;
			this.TokenToQName[8] = this.QnClosed;
			this.TokenToQName[9] = this.QnContent;
			this.TokenToQName[10] = this.QnMixed;
			this.TokenToQName[11] = this.QnEmpty;
			this.TokenToQName[12] = this.QnEltOnly;
			this.TokenToQName[13] = this.QnTextOnly;
			this.TokenToQName[14] = this.QnOrder;
			this.TokenToQName[15] = this.QnSeq;
			this.TokenToQName[16] = this.QnOne;
			this.TokenToQName[17] = this.QnMany;
			this.TokenToQName[18] = this.QnRequired;
			this.TokenToQName[19] = this.QnYes;
			this.TokenToQName[20] = this.QnNo;
			this.TokenToQName[21] = this.QnString;
			this.TokenToQName[22] = this.QnID;
			this.TokenToQName[23] = this.QnIDRef;
			this.TokenToQName[24] = this.QnIDRefs;
			this.TokenToQName[25] = this.QnEntity;
			this.TokenToQName[26] = this.QnEntities;
			this.TokenToQName[27] = this.QnNmToken;
			this.TokenToQName[28] = this.QnNmTokens;
			this.TokenToQName[29] = this.QnEnumeration;
			this.TokenToQName[30] = this.QnDefault;
			this.TokenToQName[31] = this.QnXdrSchema;
			this.TokenToQName[32] = this.QnXdrElementType;
			this.TokenToQName[33] = this.QnXdrElement;
			this.TokenToQName[34] = this.QnXdrGroup;
			this.TokenToQName[35] = this.QnXdrAttributeType;
			this.TokenToQName[36] = this.QnXdrAttribute;
			this.TokenToQName[37] = this.QnXdrDataType;
			this.TokenToQName[38] = this.QnXdrDescription;
			this.TokenToQName[39] = this.QnXdrExtends;
			this.TokenToQName[40] = this.QnXdrAliasSchema;
			this.TokenToQName[41] = this.QnDtType;
			this.TokenToQName[42] = this.QnDtValues;
			this.TokenToQName[43] = this.QnDtMaxLength;
			this.TokenToQName[44] = this.QnDtMinLength;
			this.TokenToQName[45] = this.QnDtMax;
			this.TokenToQName[46] = this.QnDtMin;
			this.TokenToQName[47] = this.QnDtMinExclusive;
			this.TokenToQName[48] = this.QnDtMaxExclusive;
			this.TokenToQName[49] = this.QnTargetNamespace;
			this.TokenToQName[50] = this.QnVersion;
			this.TokenToQName[51] = this.QnFinalDefault;
			this.TokenToQName[52] = this.QnBlockDefault;
			this.TokenToQName[53] = this.QnFixed;
			this.TokenToQName[54] = this.QnAbstract;
			this.TokenToQName[55] = this.QnBlock;
			this.TokenToQName[56] = this.QnSubstitutionGroup;
			this.TokenToQName[57] = this.QnFinal;
			this.TokenToQName[58] = this.QnNillable;
			this.TokenToQName[59] = this.QnRef;
			this.TokenToQName[60] = this.QnBase;
			this.TokenToQName[61] = this.QnDerivedBy;
			this.TokenToQName[62] = this.QnNamespace;
			this.TokenToQName[63] = this.QnProcessContents;
			this.TokenToQName[64] = this.QnRefer;
			this.TokenToQName[65] = this.QnPublic;
			this.TokenToQName[66] = this.QnSystem;
			this.TokenToQName[67] = this.QnSchemaLocation;
			this.TokenToQName[68] = this.QnValue;
			this.TokenToQName[119] = this.QnItemType;
			this.TokenToQName[120] = this.QnMemberTypes;
			this.TokenToQName[121] = this.QnXPath;
			this.TokenToQName[74] = this.QnXsdSchema;
			this.TokenToQName[75] = this.QnXsdAnnotation;
			this.TokenToQName[76] = this.QnXsdInclude;
			this.TokenToQName[77] = this.QnXsdImport;
			this.TokenToQName[78] = this.QnXsdElement;
			this.TokenToQName[79] = this.QnXsdAttribute;
			this.TokenToQName[80] = this.QnXsdAttributeGroup;
			this.TokenToQName[81] = this.QnXsdAnyAttribute;
			this.TokenToQName[82] = this.QnXsdGroup;
			this.TokenToQName[83] = this.QnXsdAll;
			this.TokenToQName[84] = this.QnXsdChoice;
			this.TokenToQName[85] = this.QnXsdSequence;
			this.TokenToQName[86] = this.QnXsdAny;
			this.TokenToQName[87] = this.QnXsdNotation;
			this.TokenToQName[88] = this.QnXsdSimpleType;
			this.TokenToQName[89] = this.QnXsdComplexType;
			this.TokenToQName[90] = this.QnXsdUnique;
			this.TokenToQName[91] = this.QnXsdKey;
			this.TokenToQName[92] = this.QnXsdKeyRef;
			this.TokenToQName[93] = this.QnXsdSelector;
			this.TokenToQName[94] = this.QnXsdField;
			this.TokenToQName[95] = this.QnXsdMinExclusive;
			this.TokenToQName[96] = this.QnXsdMinInclusive;
			this.TokenToQName[97] = this.QnXsdMaxExclusive;
			this.TokenToQName[98] = this.QnXsdMaxInclusive;
			this.TokenToQName[99] = this.QnXsdTotalDigits;
			this.TokenToQName[100] = this.QnXsdFractionDigits;
			this.TokenToQName[101] = this.QnXsdLength;
			this.TokenToQName[102] = this.QnXsdMinLength;
			this.TokenToQName[103] = this.QnXsdMaxLength;
			this.TokenToQName[104] = this.QnXsdEnumeration;
			this.TokenToQName[105] = this.QnXsdPattern;
			this.TokenToQName[117] = this.QnXsdWhiteSpace;
			this.TokenToQName[106] = this.QnXsdDocumentation;
			this.TokenToQName[107] = this.QnXsdAppinfo;
			this.TokenToQName[108] = this.QnXsdComplexContent;
			this.TokenToQName[110] = this.QnXsdRestriction;
			this.TokenToQName[113] = this.QnXsdRestriction;
			this.TokenToQName[115] = this.QnXsdRestriction;
			this.TokenToQName[109] = this.QnXsdExtension;
			this.TokenToQName[112] = this.QnXsdExtension;
			this.TokenToQName[111] = this.QnXsdSimpleContent;
			this.TokenToQName[116] = this.QnXsdUnion;
			this.TokenToQName[114] = this.QnXsdList;
			this.TokenToQName[118] = this.QnXsdRedefine;
			this.TokenToQName[69] = this.QnSource;
			this.TokenToQName[72] = this.QnUse;
			this.TokenToQName[73] = this.QnForm;
			this.TokenToQName[71] = this.QnElementFormDefault;
			this.TokenToQName[70] = this.QnAttributeFormDefault;
			this.TokenToQName[122] = this.QnXmlLang;
			this.TokenToQName[0] = XmlQualifiedName.Empty;
		}

		// Token: 0x0600384E RID: 14414 RVA: 0x0014201C File Offset: 0x0014021C
		public SchemaType SchemaTypeFromRoot(string localName, string ns)
		{
			if (this.IsXSDRoot(localName, ns))
			{
				return SchemaType.XSD;
			}
			if (this.IsXDRRoot(localName, XmlSchemaDatatype.XdrCanonizeUri(ns, this.nameTable, this)))
			{
				return SchemaType.XDR;
			}
			return SchemaType.None;
		}

		// Token: 0x0600384F RID: 14415 RVA: 0x00142043 File Offset: 0x00140243
		public bool IsXSDRoot(string localName, string ns)
		{
			return localName == this.XsdSchema && ns == this.NsXs;
		}

		// Token: 0x06003850 RID: 14416 RVA: 0x00142061 File Offset: 0x00140261
		public bool IsXDRRoot(string localName, string ns)
		{
			return localName == this.XdrSchema && ns == this.NsXdr;
		}

		// Token: 0x06003851 RID: 14417 RVA: 0x0014207F File Offset: 0x0014027F
		public XmlQualifiedName GetName(SchemaNames.Token token)
		{
			return this.TokenToQName[(int)token];
		}

		// Token: 0x040028E6 RID: 10470
		private XmlNameTable nameTable;

		// Token: 0x040028E7 RID: 10471
		public string NsDataType;

		// Token: 0x040028E8 RID: 10472
		public string NsDataTypeAlias;

		// Token: 0x040028E9 RID: 10473
		public string NsDataTypeOld;

		// Token: 0x040028EA RID: 10474
		public string NsXml;

		// Token: 0x040028EB RID: 10475
		public string NsXmlNs;

		// Token: 0x040028EC RID: 10476
		public string NsXdr;

		// Token: 0x040028ED RID: 10477
		public string NsXdrAlias;

		// Token: 0x040028EE RID: 10478
		public string NsXs;

		// Token: 0x040028EF RID: 10479
		public string NsXsi;

		// Token: 0x040028F0 RID: 10480
		public string XsiType;

		// Token: 0x040028F1 RID: 10481
		public string XsiNil;

		// Token: 0x040028F2 RID: 10482
		public string XsiSchemaLocation;

		// Token: 0x040028F3 RID: 10483
		public string XsiNoNamespaceSchemaLocation;

		// Token: 0x040028F4 RID: 10484
		public string XsdSchema;

		// Token: 0x040028F5 RID: 10485
		public string XdrSchema;

		// Token: 0x040028F6 RID: 10486
		public XmlQualifiedName QnPCData;

		// Token: 0x040028F7 RID: 10487
		public XmlQualifiedName QnXml;

		// Token: 0x040028F8 RID: 10488
		public XmlQualifiedName QnXmlNs;

		// Token: 0x040028F9 RID: 10489
		public XmlQualifiedName QnDtDt;

		// Token: 0x040028FA RID: 10490
		public XmlQualifiedName QnXmlLang;

		// Token: 0x040028FB RID: 10491
		public XmlQualifiedName QnName;

		// Token: 0x040028FC RID: 10492
		public XmlQualifiedName QnType;

		// Token: 0x040028FD RID: 10493
		public XmlQualifiedName QnMaxOccurs;

		// Token: 0x040028FE RID: 10494
		public XmlQualifiedName QnMinOccurs;

		// Token: 0x040028FF RID: 10495
		public XmlQualifiedName QnInfinite;

		// Token: 0x04002900 RID: 10496
		public XmlQualifiedName QnModel;

		// Token: 0x04002901 RID: 10497
		public XmlQualifiedName QnOpen;

		// Token: 0x04002902 RID: 10498
		public XmlQualifiedName QnClosed;

		// Token: 0x04002903 RID: 10499
		public XmlQualifiedName QnContent;

		// Token: 0x04002904 RID: 10500
		public XmlQualifiedName QnMixed;

		// Token: 0x04002905 RID: 10501
		public XmlQualifiedName QnEmpty;

		// Token: 0x04002906 RID: 10502
		public XmlQualifiedName QnEltOnly;

		// Token: 0x04002907 RID: 10503
		public XmlQualifiedName QnTextOnly;

		// Token: 0x04002908 RID: 10504
		public XmlQualifiedName QnOrder;

		// Token: 0x04002909 RID: 10505
		public XmlQualifiedName QnSeq;

		// Token: 0x0400290A RID: 10506
		public XmlQualifiedName QnOne;

		// Token: 0x0400290B RID: 10507
		public XmlQualifiedName QnMany;

		// Token: 0x0400290C RID: 10508
		public XmlQualifiedName QnRequired;

		// Token: 0x0400290D RID: 10509
		public XmlQualifiedName QnYes;

		// Token: 0x0400290E RID: 10510
		public XmlQualifiedName QnNo;

		// Token: 0x0400290F RID: 10511
		public XmlQualifiedName QnString;

		// Token: 0x04002910 RID: 10512
		public XmlQualifiedName QnID;

		// Token: 0x04002911 RID: 10513
		public XmlQualifiedName QnIDRef;

		// Token: 0x04002912 RID: 10514
		public XmlQualifiedName QnIDRefs;

		// Token: 0x04002913 RID: 10515
		public XmlQualifiedName QnEntity;

		// Token: 0x04002914 RID: 10516
		public XmlQualifiedName QnEntities;

		// Token: 0x04002915 RID: 10517
		public XmlQualifiedName QnNmToken;

		// Token: 0x04002916 RID: 10518
		public XmlQualifiedName QnNmTokens;

		// Token: 0x04002917 RID: 10519
		public XmlQualifiedName QnEnumeration;

		// Token: 0x04002918 RID: 10520
		public XmlQualifiedName QnDefault;

		// Token: 0x04002919 RID: 10521
		public XmlQualifiedName QnXdrSchema;

		// Token: 0x0400291A RID: 10522
		public XmlQualifiedName QnXdrElementType;

		// Token: 0x0400291B RID: 10523
		public XmlQualifiedName QnXdrElement;

		// Token: 0x0400291C RID: 10524
		public XmlQualifiedName QnXdrGroup;

		// Token: 0x0400291D RID: 10525
		public XmlQualifiedName QnXdrAttributeType;

		// Token: 0x0400291E RID: 10526
		public XmlQualifiedName QnXdrAttribute;

		// Token: 0x0400291F RID: 10527
		public XmlQualifiedName QnXdrDataType;

		// Token: 0x04002920 RID: 10528
		public XmlQualifiedName QnXdrDescription;

		// Token: 0x04002921 RID: 10529
		public XmlQualifiedName QnXdrExtends;

		// Token: 0x04002922 RID: 10530
		public XmlQualifiedName QnXdrAliasSchema;

		// Token: 0x04002923 RID: 10531
		public XmlQualifiedName QnDtType;

		// Token: 0x04002924 RID: 10532
		public XmlQualifiedName QnDtValues;

		// Token: 0x04002925 RID: 10533
		public XmlQualifiedName QnDtMaxLength;

		// Token: 0x04002926 RID: 10534
		public XmlQualifiedName QnDtMinLength;

		// Token: 0x04002927 RID: 10535
		public XmlQualifiedName QnDtMax;

		// Token: 0x04002928 RID: 10536
		public XmlQualifiedName QnDtMin;

		// Token: 0x04002929 RID: 10537
		public XmlQualifiedName QnDtMinExclusive;

		// Token: 0x0400292A RID: 10538
		public XmlQualifiedName QnDtMaxExclusive;

		// Token: 0x0400292B RID: 10539
		public XmlQualifiedName QnTargetNamespace;

		// Token: 0x0400292C RID: 10540
		public XmlQualifiedName QnVersion;

		// Token: 0x0400292D RID: 10541
		public XmlQualifiedName QnFinalDefault;

		// Token: 0x0400292E RID: 10542
		public XmlQualifiedName QnBlockDefault;

		// Token: 0x0400292F RID: 10543
		public XmlQualifiedName QnFixed;

		// Token: 0x04002930 RID: 10544
		public XmlQualifiedName QnAbstract;

		// Token: 0x04002931 RID: 10545
		public XmlQualifiedName QnBlock;

		// Token: 0x04002932 RID: 10546
		public XmlQualifiedName QnSubstitutionGroup;

		// Token: 0x04002933 RID: 10547
		public XmlQualifiedName QnFinal;

		// Token: 0x04002934 RID: 10548
		public XmlQualifiedName QnNillable;

		// Token: 0x04002935 RID: 10549
		public XmlQualifiedName QnRef;

		// Token: 0x04002936 RID: 10550
		public XmlQualifiedName QnBase;

		// Token: 0x04002937 RID: 10551
		public XmlQualifiedName QnDerivedBy;

		// Token: 0x04002938 RID: 10552
		public XmlQualifiedName QnNamespace;

		// Token: 0x04002939 RID: 10553
		public XmlQualifiedName QnProcessContents;

		// Token: 0x0400293A RID: 10554
		public XmlQualifiedName QnRefer;

		// Token: 0x0400293B RID: 10555
		public XmlQualifiedName QnPublic;

		// Token: 0x0400293C RID: 10556
		public XmlQualifiedName QnSystem;

		// Token: 0x0400293D RID: 10557
		public XmlQualifiedName QnSchemaLocation;

		// Token: 0x0400293E RID: 10558
		public XmlQualifiedName QnValue;

		// Token: 0x0400293F RID: 10559
		public XmlQualifiedName QnUse;

		// Token: 0x04002940 RID: 10560
		public XmlQualifiedName QnForm;

		// Token: 0x04002941 RID: 10561
		public XmlQualifiedName QnElementFormDefault;

		// Token: 0x04002942 RID: 10562
		public XmlQualifiedName QnAttributeFormDefault;

		// Token: 0x04002943 RID: 10563
		public XmlQualifiedName QnItemType;

		// Token: 0x04002944 RID: 10564
		public XmlQualifiedName QnMemberTypes;

		// Token: 0x04002945 RID: 10565
		public XmlQualifiedName QnXPath;

		// Token: 0x04002946 RID: 10566
		public XmlQualifiedName QnXsdSchema;

		// Token: 0x04002947 RID: 10567
		public XmlQualifiedName QnXsdAnnotation;

		// Token: 0x04002948 RID: 10568
		public XmlQualifiedName QnXsdInclude;

		// Token: 0x04002949 RID: 10569
		public XmlQualifiedName QnXsdImport;

		// Token: 0x0400294A RID: 10570
		public XmlQualifiedName QnXsdElement;

		// Token: 0x0400294B RID: 10571
		public XmlQualifiedName QnXsdAttribute;

		// Token: 0x0400294C RID: 10572
		public XmlQualifiedName QnXsdAttributeGroup;

		// Token: 0x0400294D RID: 10573
		public XmlQualifiedName QnXsdAnyAttribute;

		// Token: 0x0400294E RID: 10574
		public XmlQualifiedName QnXsdGroup;

		// Token: 0x0400294F RID: 10575
		public XmlQualifiedName QnXsdAll;

		// Token: 0x04002950 RID: 10576
		public XmlQualifiedName QnXsdChoice;

		// Token: 0x04002951 RID: 10577
		public XmlQualifiedName QnXsdSequence;

		// Token: 0x04002952 RID: 10578
		public XmlQualifiedName QnXsdAny;

		// Token: 0x04002953 RID: 10579
		public XmlQualifiedName QnXsdNotation;

		// Token: 0x04002954 RID: 10580
		public XmlQualifiedName QnXsdSimpleType;

		// Token: 0x04002955 RID: 10581
		public XmlQualifiedName QnXsdComplexType;

		// Token: 0x04002956 RID: 10582
		public XmlQualifiedName QnXsdUnique;

		// Token: 0x04002957 RID: 10583
		public XmlQualifiedName QnXsdKey;

		// Token: 0x04002958 RID: 10584
		public XmlQualifiedName QnXsdKeyRef;

		// Token: 0x04002959 RID: 10585
		public XmlQualifiedName QnXsdSelector;

		// Token: 0x0400295A RID: 10586
		public XmlQualifiedName QnXsdField;

		// Token: 0x0400295B RID: 10587
		public XmlQualifiedName QnXsdMinExclusive;

		// Token: 0x0400295C RID: 10588
		public XmlQualifiedName QnXsdMinInclusive;

		// Token: 0x0400295D RID: 10589
		public XmlQualifiedName QnXsdMaxInclusive;

		// Token: 0x0400295E RID: 10590
		public XmlQualifiedName QnXsdMaxExclusive;

		// Token: 0x0400295F RID: 10591
		public XmlQualifiedName QnXsdTotalDigits;

		// Token: 0x04002960 RID: 10592
		public XmlQualifiedName QnXsdFractionDigits;

		// Token: 0x04002961 RID: 10593
		public XmlQualifiedName QnXsdLength;

		// Token: 0x04002962 RID: 10594
		public XmlQualifiedName QnXsdMinLength;

		// Token: 0x04002963 RID: 10595
		public XmlQualifiedName QnXsdMaxLength;

		// Token: 0x04002964 RID: 10596
		public XmlQualifiedName QnXsdEnumeration;

		// Token: 0x04002965 RID: 10597
		public XmlQualifiedName QnXsdPattern;

		// Token: 0x04002966 RID: 10598
		public XmlQualifiedName QnXsdDocumentation;

		// Token: 0x04002967 RID: 10599
		public XmlQualifiedName QnXsdAppinfo;

		// Token: 0x04002968 RID: 10600
		public XmlQualifiedName QnSource;

		// Token: 0x04002969 RID: 10601
		public XmlQualifiedName QnXsdComplexContent;

		// Token: 0x0400296A RID: 10602
		public XmlQualifiedName QnXsdSimpleContent;

		// Token: 0x0400296B RID: 10603
		public XmlQualifiedName QnXsdRestriction;

		// Token: 0x0400296C RID: 10604
		public XmlQualifiedName QnXsdExtension;

		// Token: 0x0400296D RID: 10605
		public XmlQualifiedName QnXsdUnion;

		// Token: 0x0400296E RID: 10606
		public XmlQualifiedName QnXsdList;

		// Token: 0x0400296F RID: 10607
		public XmlQualifiedName QnXsdWhiteSpace;

		// Token: 0x04002970 RID: 10608
		public XmlQualifiedName QnXsdRedefine;

		// Token: 0x04002971 RID: 10609
		public XmlQualifiedName QnXsdAnyType;

		// Token: 0x04002972 RID: 10610
		internal XmlQualifiedName[] TokenToQName = new XmlQualifiedName[123];

		// Token: 0x02000579 RID: 1401
		public enum Token
		{
			// Token: 0x04002974 RID: 10612
			Empty,
			// Token: 0x04002975 RID: 10613
			SchemaName,
			// Token: 0x04002976 RID: 10614
			SchemaType,
			// Token: 0x04002977 RID: 10615
			SchemaMaxOccurs,
			// Token: 0x04002978 RID: 10616
			SchemaMinOccurs,
			// Token: 0x04002979 RID: 10617
			SchemaInfinite,
			// Token: 0x0400297A RID: 10618
			SchemaModel,
			// Token: 0x0400297B RID: 10619
			SchemaOpen,
			// Token: 0x0400297C RID: 10620
			SchemaClosed,
			// Token: 0x0400297D RID: 10621
			SchemaContent,
			// Token: 0x0400297E RID: 10622
			SchemaMixed,
			// Token: 0x0400297F RID: 10623
			SchemaEmpty,
			// Token: 0x04002980 RID: 10624
			SchemaElementOnly,
			// Token: 0x04002981 RID: 10625
			SchemaTextOnly,
			// Token: 0x04002982 RID: 10626
			SchemaOrder,
			// Token: 0x04002983 RID: 10627
			SchemaSeq,
			// Token: 0x04002984 RID: 10628
			SchemaOne,
			// Token: 0x04002985 RID: 10629
			SchemaMany,
			// Token: 0x04002986 RID: 10630
			SchemaRequired,
			// Token: 0x04002987 RID: 10631
			SchemaYes,
			// Token: 0x04002988 RID: 10632
			SchemaNo,
			// Token: 0x04002989 RID: 10633
			SchemaString,
			// Token: 0x0400298A RID: 10634
			SchemaId,
			// Token: 0x0400298B RID: 10635
			SchemaIdref,
			// Token: 0x0400298C RID: 10636
			SchemaIdrefs,
			// Token: 0x0400298D RID: 10637
			SchemaEntity,
			// Token: 0x0400298E RID: 10638
			SchemaEntities,
			// Token: 0x0400298F RID: 10639
			SchemaNmtoken,
			// Token: 0x04002990 RID: 10640
			SchemaNmtokens,
			// Token: 0x04002991 RID: 10641
			SchemaEnumeration,
			// Token: 0x04002992 RID: 10642
			SchemaDefault,
			// Token: 0x04002993 RID: 10643
			XdrRoot,
			// Token: 0x04002994 RID: 10644
			XdrElementType,
			// Token: 0x04002995 RID: 10645
			XdrElement,
			// Token: 0x04002996 RID: 10646
			XdrGroup,
			// Token: 0x04002997 RID: 10647
			XdrAttributeType,
			// Token: 0x04002998 RID: 10648
			XdrAttribute,
			// Token: 0x04002999 RID: 10649
			XdrDatatype,
			// Token: 0x0400299A RID: 10650
			XdrDescription,
			// Token: 0x0400299B RID: 10651
			XdrExtends,
			// Token: 0x0400299C RID: 10652
			SchemaXdrRootAlias,
			// Token: 0x0400299D RID: 10653
			SchemaDtType,
			// Token: 0x0400299E RID: 10654
			SchemaDtValues,
			// Token: 0x0400299F RID: 10655
			SchemaDtMaxLength,
			// Token: 0x040029A0 RID: 10656
			SchemaDtMinLength,
			// Token: 0x040029A1 RID: 10657
			SchemaDtMax,
			// Token: 0x040029A2 RID: 10658
			SchemaDtMin,
			// Token: 0x040029A3 RID: 10659
			SchemaDtMinExclusive,
			// Token: 0x040029A4 RID: 10660
			SchemaDtMaxExclusive,
			// Token: 0x040029A5 RID: 10661
			SchemaTargetNamespace,
			// Token: 0x040029A6 RID: 10662
			SchemaVersion,
			// Token: 0x040029A7 RID: 10663
			SchemaFinalDefault,
			// Token: 0x040029A8 RID: 10664
			SchemaBlockDefault,
			// Token: 0x040029A9 RID: 10665
			SchemaFixed,
			// Token: 0x040029AA RID: 10666
			SchemaAbstract,
			// Token: 0x040029AB RID: 10667
			SchemaBlock,
			// Token: 0x040029AC RID: 10668
			SchemaSubstitutionGroup,
			// Token: 0x040029AD RID: 10669
			SchemaFinal,
			// Token: 0x040029AE RID: 10670
			SchemaNillable,
			// Token: 0x040029AF RID: 10671
			SchemaRef,
			// Token: 0x040029B0 RID: 10672
			SchemaBase,
			// Token: 0x040029B1 RID: 10673
			SchemaDerivedBy,
			// Token: 0x040029B2 RID: 10674
			SchemaNamespace,
			// Token: 0x040029B3 RID: 10675
			SchemaProcessContents,
			// Token: 0x040029B4 RID: 10676
			SchemaRefer,
			// Token: 0x040029B5 RID: 10677
			SchemaPublic,
			// Token: 0x040029B6 RID: 10678
			SchemaSystem,
			// Token: 0x040029B7 RID: 10679
			SchemaSchemaLocation,
			// Token: 0x040029B8 RID: 10680
			SchemaValue,
			// Token: 0x040029B9 RID: 10681
			SchemaSource,
			// Token: 0x040029BA RID: 10682
			SchemaAttributeFormDefault,
			// Token: 0x040029BB RID: 10683
			SchemaElementFormDefault,
			// Token: 0x040029BC RID: 10684
			SchemaUse,
			// Token: 0x040029BD RID: 10685
			SchemaForm,
			// Token: 0x040029BE RID: 10686
			XsdSchema,
			// Token: 0x040029BF RID: 10687
			XsdAnnotation,
			// Token: 0x040029C0 RID: 10688
			XsdInclude,
			// Token: 0x040029C1 RID: 10689
			XsdImport,
			// Token: 0x040029C2 RID: 10690
			XsdElement,
			// Token: 0x040029C3 RID: 10691
			XsdAttribute,
			// Token: 0x040029C4 RID: 10692
			xsdAttributeGroup,
			// Token: 0x040029C5 RID: 10693
			XsdAnyAttribute,
			// Token: 0x040029C6 RID: 10694
			XsdGroup,
			// Token: 0x040029C7 RID: 10695
			XsdAll,
			// Token: 0x040029C8 RID: 10696
			XsdChoice,
			// Token: 0x040029C9 RID: 10697
			XsdSequence,
			// Token: 0x040029CA RID: 10698
			XsdAny,
			// Token: 0x040029CB RID: 10699
			XsdNotation,
			// Token: 0x040029CC RID: 10700
			XsdSimpleType,
			// Token: 0x040029CD RID: 10701
			XsdComplexType,
			// Token: 0x040029CE RID: 10702
			XsdUnique,
			// Token: 0x040029CF RID: 10703
			XsdKey,
			// Token: 0x040029D0 RID: 10704
			XsdKeyref,
			// Token: 0x040029D1 RID: 10705
			XsdSelector,
			// Token: 0x040029D2 RID: 10706
			XsdField,
			// Token: 0x040029D3 RID: 10707
			XsdMinExclusive,
			// Token: 0x040029D4 RID: 10708
			XsdMinInclusive,
			// Token: 0x040029D5 RID: 10709
			XsdMaxExclusive,
			// Token: 0x040029D6 RID: 10710
			XsdMaxInclusive,
			// Token: 0x040029D7 RID: 10711
			XsdTotalDigits,
			// Token: 0x040029D8 RID: 10712
			XsdFractionDigits,
			// Token: 0x040029D9 RID: 10713
			XsdLength,
			// Token: 0x040029DA RID: 10714
			XsdMinLength,
			// Token: 0x040029DB RID: 10715
			XsdMaxLength,
			// Token: 0x040029DC RID: 10716
			XsdEnumeration,
			// Token: 0x040029DD RID: 10717
			XsdPattern,
			// Token: 0x040029DE RID: 10718
			XsdDocumentation,
			// Token: 0x040029DF RID: 10719
			XsdAppInfo,
			// Token: 0x040029E0 RID: 10720
			XsdComplexContent,
			// Token: 0x040029E1 RID: 10721
			XsdComplexContentExtension,
			// Token: 0x040029E2 RID: 10722
			XsdComplexContentRestriction,
			// Token: 0x040029E3 RID: 10723
			XsdSimpleContent,
			// Token: 0x040029E4 RID: 10724
			XsdSimpleContentExtension,
			// Token: 0x040029E5 RID: 10725
			XsdSimpleContentRestriction,
			// Token: 0x040029E6 RID: 10726
			XsdSimpleTypeList,
			// Token: 0x040029E7 RID: 10727
			XsdSimpleTypeRestriction,
			// Token: 0x040029E8 RID: 10728
			XsdSimpleTypeUnion,
			// Token: 0x040029E9 RID: 10729
			XsdWhitespace,
			// Token: 0x040029EA RID: 10730
			XsdRedefine,
			// Token: 0x040029EB RID: 10731
			SchemaItemType,
			// Token: 0x040029EC RID: 10732
			SchemaMemberTypes,
			// Token: 0x040029ED RID: 10733
			SchemaXPath,
			// Token: 0x040029EE RID: 10734
			XmlLang
		}
	}
}

using System;
using System.Collections;

namespace System.Xml.Schema
{
	// Token: 0x020005FD RID: 1533
	internal sealed class XsdBuilder : SchemaBuilder
	{
		// Token: 0x06003EC0 RID: 16064 RVA: 0x0015B574 File Offset: 0x00159774
		internal XsdBuilder(XmlReader reader, XmlNamespaceManager curmgr, XmlSchema schema, XmlNameTable nameTable, SchemaNames schemaNames, ValidationEventHandler eventhandler)
		{
			this.reader = reader;
			this.schema = schema;
			this.xso = schema;
			this.namespaceManager = new XsdBuilder.BuilderNamespaceManager(curmgr, reader);
			this.validationEventHandler = eventhandler;
			this.nameTable = nameTable;
			this.schemaNames = schemaNames;
			this.stateHistory = new HWStack(10);
			this.currentEntry = XsdBuilder.SchemaEntries[0];
			this.positionInfo = PositionInfo.GetPositionInfo(reader);
		}

		// Token: 0x06003EC1 RID: 16065 RVA: 0x0015B60C File Offset: 0x0015980C
		internal override bool ProcessElement(string prefix, string name, string ns)
		{
			XmlQualifiedName xmlQualifiedName = new XmlQualifiedName(name, ns);
			if (this.GetNextState(xmlQualifiedName))
			{
				this.Push();
				this.xso = null;
				this.currentEntry.InitFunc(this, null);
				this.RecordPosition();
				return true;
			}
			if (!this.IsSkipableElement(xmlQualifiedName))
			{
				this.SendValidationEvent("The '{0}' element is not supported in this context.", xmlQualifiedName.ToString());
			}
			return false;
		}

		// Token: 0x06003EC2 RID: 16066 RVA: 0x0015B670 File Offset: 0x00159870
		internal override void ProcessAttribute(string prefix, string name, string ns, string value)
		{
			XmlQualifiedName xmlQualifiedName = new XmlQualifiedName(name, ns);
			if (this.currentEntry.Attributes != null)
			{
				for (int i = 0; i < this.currentEntry.Attributes.Length; i++)
				{
					XsdBuilder.XsdAttributeEntry xsdAttributeEntry = this.currentEntry.Attributes[i];
					if (this.schemaNames.TokenToQName[(int)xsdAttributeEntry.Attribute].Equals(xmlQualifiedName))
					{
						try
						{
							xsdAttributeEntry.BuildFunc(this, value);
						}
						catch (XmlSchemaException ex)
						{
							ex.SetSource(this.reader.BaseURI, this.positionInfo.LineNumber, this.positionInfo.LinePosition);
							this.SendValidationEvent("The value for the '{0}' attribute is invalid - {1}", new string[]
							{
								name,
								ex.Message
							}, XmlSeverityType.Error);
						}
						return;
					}
				}
			}
			if (!(ns != this.schemaNames.NsXs) || ns.Length == 0)
			{
				this.SendValidationEvent("The '{0}' attribute is not supported in this context.", xmlQualifiedName.ToString());
				return;
			}
			if (ns == this.schemaNames.NsXmlNs)
			{
				if (this.namespaces == null)
				{
					this.namespaces = new Hashtable();
				}
				this.namespaces.Add((name == this.schemaNames.QnXmlNs.Name) ? string.Empty : name, value);
				return;
			}
			XmlAttribute xmlAttribute = new XmlAttribute(prefix, name, ns, this.schema.Document);
			xmlAttribute.Value = value;
			this.unhandledAttributes.Add(xmlAttribute);
		}

		// Token: 0x06003EC3 RID: 16067 RVA: 0x0015B7F8 File Offset: 0x001599F8
		internal override bool IsContentParsed()
		{
			return this.currentEntry.ParseContent;
		}

		// Token: 0x06003EC4 RID: 16068 RVA: 0x0015B805 File Offset: 0x00159A05
		internal override void ProcessMarkup(XmlNode[] markup)
		{
			this.markup = markup;
		}

		// Token: 0x06003EC5 RID: 16069 RVA: 0x0015B80E File Offset: 0x00159A0E
		internal override void ProcessCData(string value)
		{
			this.SendValidationEvent("The following text is not allowed in this context: '{0}'.", value);
		}

		// Token: 0x06003EC6 RID: 16070 RVA: 0x0015B81C File Offset: 0x00159A1C
		internal override void StartChildren()
		{
			if (this.xso != null)
			{
				if (this.namespaces != null && this.namespaces.Count > 0)
				{
					this.xso.Namespaces.Namespaces = this.namespaces;
					this.namespaces = null;
				}
				if (this.unhandledAttributes.Count != 0)
				{
					this.xso.SetUnhandledAttributes((XmlAttribute[])this.unhandledAttributes.ToArray(typeof(XmlAttribute)));
					this.unhandledAttributes.Clear();
				}
			}
		}

		// Token: 0x06003EC7 RID: 16071 RVA: 0x0015B8A1 File Offset: 0x00159AA1
		internal override void EndChildren()
		{
			if (this.currentEntry.EndChildFunc != null)
			{
				this.currentEntry.EndChildFunc(this);
			}
			this.Pop();
		}

		// Token: 0x06003EC8 RID: 16072 RVA: 0x0015B8C8 File Offset: 0x00159AC8
		private void Push()
		{
			this.stateHistory.Push();
			this.stateHistory[this.stateHistory.Length - 1] = this.currentEntry;
			this.containerStack.Push(this.GetContainer(this.currentEntry.CurrentState));
			this.currentEntry = this.nextEntry;
			if (this.currentEntry.Name != SchemaNames.Token.XsdAnnotation)
			{
				this.hasChild = false;
			}
		}

		// Token: 0x06003EC9 RID: 16073 RVA: 0x0015B93D File Offset: 0x00159B3D
		private void Pop()
		{
			this.currentEntry = (XsdBuilder.XsdEntry)this.stateHistory.Pop();
			this.SetContainer(this.currentEntry.CurrentState, this.containerStack.Pop());
			this.hasChild = true;
		}

		// Token: 0x17000BF5 RID: 3061
		// (get) Token: 0x06003ECA RID: 16074 RVA: 0x0015B978 File Offset: 0x00159B78
		private SchemaNames.Token CurrentElement
		{
			get
			{
				return this.currentEntry.Name;
			}
		}

		// Token: 0x17000BF6 RID: 3062
		// (get) Token: 0x06003ECB RID: 16075 RVA: 0x0015B985 File Offset: 0x00159B85
		private SchemaNames.Token ParentElement
		{
			get
			{
				return ((XsdBuilder.XsdEntry)this.stateHistory[this.stateHistory.Length - 1]).Name;
			}
		}

		// Token: 0x17000BF7 RID: 3063
		// (get) Token: 0x06003ECC RID: 16076 RVA: 0x0015B9A9 File Offset: 0x00159BA9
		private XmlSchemaObject ParentContainer
		{
			get
			{
				return (XmlSchemaObject)this.containerStack.Peek();
			}
		}

		// Token: 0x06003ECD RID: 16077 RVA: 0x0015B9BC File Offset: 0x00159BBC
		private XmlSchemaObject GetContainer(XsdBuilder.State state)
		{
			XmlSchemaObject result = null;
			switch (state)
			{
			case XsdBuilder.State.Schema:
				result = this.schema;
				break;
			case XsdBuilder.State.Annotation:
				result = this.annotation;
				break;
			case XsdBuilder.State.Include:
				result = this.include;
				break;
			case XsdBuilder.State.Import:
				result = this.import;
				break;
			case XsdBuilder.State.Element:
				result = this.element;
				break;
			case XsdBuilder.State.Attribute:
				result = this.attribute;
				break;
			case XsdBuilder.State.AttributeGroup:
				result = this.attributeGroup;
				break;
			case XsdBuilder.State.AttributeGroupRef:
				result = this.attributeGroupRef;
				break;
			case XsdBuilder.State.AnyAttribute:
				result = this.anyAttribute;
				break;
			case XsdBuilder.State.Group:
				result = this.group;
				break;
			case XsdBuilder.State.GroupRef:
				result = this.groupRef;
				break;
			case XsdBuilder.State.All:
				result = this.all;
				break;
			case XsdBuilder.State.Choice:
				result = this.choice;
				break;
			case XsdBuilder.State.Sequence:
				result = this.sequence;
				break;
			case XsdBuilder.State.Any:
				result = this.anyElement;
				break;
			case XsdBuilder.State.Notation:
				result = this.notation;
				break;
			case XsdBuilder.State.SimpleType:
				result = this.simpleType;
				break;
			case XsdBuilder.State.ComplexType:
				result = this.complexType;
				break;
			case XsdBuilder.State.ComplexContent:
				result = this.complexContent;
				break;
			case XsdBuilder.State.ComplexContentRestriction:
				result = this.complexContentRestriction;
				break;
			case XsdBuilder.State.ComplexContentExtension:
				result = this.complexContentExtension;
				break;
			case XsdBuilder.State.SimpleContent:
				result = this.simpleContent;
				break;
			case XsdBuilder.State.SimpleContentExtension:
				result = this.simpleContentExtension;
				break;
			case XsdBuilder.State.SimpleContentRestriction:
				result = this.simpleContentRestriction;
				break;
			case XsdBuilder.State.SimpleTypeUnion:
				result = this.simpleTypeUnion;
				break;
			case XsdBuilder.State.SimpleTypeList:
				result = this.simpleTypeList;
				break;
			case XsdBuilder.State.SimpleTypeRestriction:
				result = this.simpleTypeRestriction;
				break;
			case XsdBuilder.State.Unique:
			case XsdBuilder.State.Key:
			case XsdBuilder.State.KeyRef:
				result = this.identityConstraint;
				break;
			case XsdBuilder.State.Selector:
			case XsdBuilder.State.Field:
				result = this.xpath;
				break;
			case XsdBuilder.State.MinExclusive:
			case XsdBuilder.State.MinInclusive:
			case XsdBuilder.State.MaxExclusive:
			case XsdBuilder.State.MaxInclusive:
			case XsdBuilder.State.TotalDigits:
			case XsdBuilder.State.FractionDigits:
			case XsdBuilder.State.Length:
			case XsdBuilder.State.MinLength:
			case XsdBuilder.State.MaxLength:
			case XsdBuilder.State.Enumeration:
			case XsdBuilder.State.Pattern:
			case XsdBuilder.State.WhiteSpace:
				result = this.facet;
				break;
			case XsdBuilder.State.AppInfo:
				result = this.appInfo;
				break;
			case XsdBuilder.State.Documentation:
				result = this.documentation;
				break;
			case XsdBuilder.State.Redefine:
				result = this.redefine;
				break;
			}
			return result;
		}

		// Token: 0x06003ECE RID: 16078 RVA: 0x0015BBF4 File Offset: 0x00159DF4
		private void SetContainer(XsdBuilder.State state, object container)
		{
			switch (state)
			{
			case XsdBuilder.State.Root:
			case XsdBuilder.State.Schema:
				break;
			case XsdBuilder.State.Annotation:
				this.annotation = (XmlSchemaAnnotation)container;
				return;
			case XsdBuilder.State.Include:
				this.include = (XmlSchemaInclude)container;
				return;
			case XsdBuilder.State.Import:
				this.import = (XmlSchemaImport)container;
				return;
			case XsdBuilder.State.Element:
				this.element = (XmlSchemaElement)container;
				return;
			case XsdBuilder.State.Attribute:
				this.attribute = (XmlSchemaAttribute)container;
				return;
			case XsdBuilder.State.AttributeGroup:
				this.attributeGroup = (XmlSchemaAttributeGroup)container;
				return;
			case XsdBuilder.State.AttributeGroupRef:
				this.attributeGroupRef = (XmlSchemaAttributeGroupRef)container;
				return;
			case XsdBuilder.State.AnyAttribute:
				this.anyAttribute = (XmlSchemaAnyAttribute)container;
				return;
			case XsdBuilder.State.Group:
				this.group = (XmlSchemaGroup)container;
				return;
			case XsdBuilder.State.GroupRef:
				this.groupRef = (XmlSchemaGroupRef)container;
				return;
			case XsdBuilder.State.All:
				this.all = (XmlSchemaAll)container;
				return;
			case XsdBuilder.State.Choice:
				this.choice = (XmlSchemaChoice)container;
				return;
			case XsdBuilder.State.Sequence:
				this.sequence = (XmlSchemaSequence)container;
				return;
			case XsdBuilder.State.Any:
				this.anyElement = (XmlSchemaAny)container;
				return;
			case XsdBuilder.State.Notation:
				this.notation = (XmlSchemaNotation)container;
				return;
			case XsdBuilder.State.SimpleType:
				this.simpleType = (XmlSchemaSimpleType)container;
				return;
			case XsdBuilder.State.ComplexType:
				this.complexType = (XmlSchemaComplexType)container;
				return;
			case XsdBuilder.State.ComplexContent:
				this.complexContent = (XmlSchemaComplexContent)container;
				return;
			case XsdBuilder.State.ComplexContentRestriction:
				this.complexContentRestriction = (XmlSchemaComplexContentRestriction)container;
				return;
			case XsdBuilder.State.ComplexContentExtension:
				this.complexContentExtension = (XmlSchemaComplexContentExtension)container;
				return;
			case XsdBuilder.State.SimpleContent:
				this.simpleContent = (XmlSchemaSimpleContent)container;
				return;
			case XsdBuilder.State.SimpleContentExtension:
				this.simpleContentExtension = (XmlSchemaSimpleContentExtension)container;
				return;
			case XsdBuilder.State.SimpleContentRestriction:
				this.simpleContentRestriction = (XmlSchemaSimpleContentRestriction)container;
				return;
			case XsdBuilder.State.SimpleTypeUnion:
				this.simpleTypeUnion = (XmlSchemaSimpleTypeUnion)container;
				return;
			case XsdBuilder.State.SimpleTypeList:
				this.simpleTypeList = (XmlSchemaSimpleTypeList)container;
				return;
			case XsdBuilder.State.SimpleTypeRestriction:
				this.simpleTypeRestriction = (XmlSchemaSimpleTypeRestriction)container;
				return;
			case XsdBuilder.State.Unique:
			case XsdBuilder.State.Key:
			case XsdBuilder.State.KeyRef:
				this.identityConstraint = (XmlSchemaIdentityConstraint)container;
				return;
			case XsdBuilder.State.Selector:
			case XsdBuilder.State.Field:
				this.xpath = (XmlSchemaXPath)container;
				return;
			case XsdBuilder.State.MinExclusive:
			case XsdBuilder.State.MinInclusive:
			case XsdBuilder.State.MaxExclusive:
			case XsdBuilder.State.MaxInclusive:
			case XsdBuilder.State.TotalDigits:
			case XsdBuilder.State.FractionDigits:
			case XsdBuilder.State.Length:
			case XsdBuilder.State.MinLength:
			case XsdBuilder.State.MaxLength:
			case XsdBuilder.State.Enumeration:
			case XsdBuilder.State.Pattern:
			case XsdBuilder.State.WhiteSpace:
				this.facet = (XmlSchemaFacet)container;
				return;
			case XsdBuilder.State.AppInfo:
				this.appInfo = (XmlSchemaAppInfo)container;
				return;
			case XsdBuilder.State.Documentation:
				this.documentation = (XmlSchemaDocumentation)container;
				return;
			case XsdBuilder.State.Redefine:
				this.redefine = (XmlSchemaRedefine)container;
				break;
			default:
				return;
			}
		}

		// Token: 0x06003ECF RID: 16079 RVA: 0x0015BE67 File Offset: 0x0015A067
		private static void BuildAnnotated_Id(XsdBuilder builder, string value)
		{
			builder.xso.IdAttribute = value;
		}

		// Token: 0x06003ED0 RID: 16080 RVA: 0x0015BE75 File Offset: 0x0015A075
		private static void BuildSchema_AttributeFormDefault(XsdBuilder builder, string value)
		{
			builder.schema.AttributeFormDefault = (XmlSchemaForm)builder.ParseEnum(value, "attributeFormDefault", XsdBuilder.FormStringValues);
		}

		// Token: 0x06003ED1 RID: 16081 RVA: 0x0015BE93 File Offset: 0x0015A093
		private static void BuildSchema_ElementFormDefault(XsdBuilder builder, string value)
		{
			builder.schema.ElementFormDefault = (XmlSchemaForm)builder.ParseEnum(value, "elementFormDefault", XsdBuilder.FormStringValues);
		}

		// Token: 0x06003ED2 RID: 16082 RVA: 0x0015BEB1 File Offset: 0x0015A0B1
		private static void BuildSchema_TargetNamespace(XsdBuilder builder, string value)
		{
			builder.schema.TargetNamespace = value;
		}

		// Token: 0x06003ED3 RID: 16083 RVA: 0x0015BEBF File Offset: 0x0015A0BF
		private static void BuildSchema_Version(XsdBuilder builder, string value)
		{
			builder.schema.Version = value;
		}

		// Token: 0x06003ED4 RID: 16084 RVA: 0x0015BECD File Offset: 0x0015A0CD
		private static void BuildSchema_FinalDefault(XsdBuilder builder, string value)
		{
			builder.schema.FinalDefault = (XmlSchemaDerivationMethod)builder.ParseBlockFinalEnum(value, "finalDefault");
		}

		// Token: 0x06003ED5 RID: 16085 RVA: 0x0015BEE6 File Offset: 0x0015A0E6
		private static void BuildSchema_BlockDefault(XsdBuilder builder, string value)
		{
			builder.schema.BlockDefault = (XmlSchemaDerivationMethod)builder.ParseBlockFinalEnum(value, "blockDefault");
		}

		// Token: 0x06003ED6 RID: 16086 RVA: 0x0015BEFF File Offset: 0x0015A0FF
		private static void InitSchema(XsdBuilder builder, string value)
		{
			builder.canIncludeImport = true;
			builder.xso = builder.schema;
		}

		// Token: 0x06003ED7 RID: 16087 RVA: 0x0015BF14 File Offset: 0x0015A114
		private static void InitInclude(XsdBuilder builder, string value)
		{
			if (!builder.canIncludeImport)
			{
				builder.SendValidationEvent("The 'include' element cannot appear at this location.", null);
			}
			builder.xso = (builder.include = new XmlSchemaInclude());
			builder.schema.Includes.Add(builder.include);
		}

		// Token: 0x06003ED8 RID: 16088 RVA: 0x0015BF60 File Offset: 0x0015A160
		private static void BuildInclude_SchemaLocation(XsdBuilder builder, string value)
		{
			builder.include.SchemaLocation = value;
		}

		// Token: 0x06003ED9 RID: 16089 RVA: 0x0015BF70 File Offset: 0x0015A170
		private static void InitImport(XsdBuilder builder, string value)
		{
			if (!builder.canIncludeImport)
			{
				builder.SendValidationEvent("The 'import' element cannot appear at this location.", null);
			}
			builder.xso = (builder.import = new XmlSchemaImport());
			builder.schema.Includes.Add(builder.import);
		}

		// Token: 0x06003EDA RID: 16090 RVA: 0x0015BFBC File Offset: 0x0015A1BC
		private static void BuildImport_Namespace(XsdBuilder builder, string value)
		{
			builder.import.Namespace = value;
		}

		// Token: 0x06003EDB RID: 16091 RVA: 0x0015BFCA File Offset: 0x0015A1CA
		private static void BuildImport_SchemaLocation(XsdBuilder builder, string value)
		{
			builder.import.SchemaLocation = value;
		}

		// Token: 0x06003EDC RID: 16092 RVA: 0x0015BFD8 File Offset: 0x0015A1D8
		private static void InitRedefine(XsdBuilder builder, string value)
		{
			if (!builder.canIncludeImport)
			{
				builder.SendValidationEvent("The 'redefine' element cannot appear at this location.", null);
			}
			builder.xso = (builder.redefine = new XmlSchemaRedefine());
			builder.schema.Includes.Add(builder.redefine);
		}

		// Token: 0x06003EDD RID: 16093 RVA: 0x0015C024 File Offset: 0x0015A224
		private static void BuildRedefine_SchemaLocation(XsdBuilder builder, string value)
		{
			builder.redefine.SchemaLocation = value;
		}

		// Token: 0x06003EDE RID: 16094 RVA: 0x0015C032 File Offset: 0x0015A232
		private static void EndRedefine(XsdBuilder builder)
		{
			builder.canIncludeImport = true;
		}

		// Token: 0x06003EDF RID: 16095 RVA: 0x0015C03C File Offset: 0x0015A23C
		private static void InitAttribute(XsdBuilder builder, string value)
		{
			builder.xso = (builder.attribute = new XmlSchemaAttribute());
			if (builder.ParentElement == SchemaNames.Token.XsdSchema)
			{
				builder.schema.Items.Add(builder.attribute);
			}
			else
			{
				builder.AddAttribute(builder.attribute);
			}
			builder.canIncludeImport = false;
		}

		// Token: 0x06003EE0 RID: 16096 RVA: 0x0015C093 File Offset: 0x0015A293
		private static void BuildAttribute_Default(XsdBuilder builder, string value)
		{
			builder.attribute.DefaultValue = value;
		}

		// Token: 0x06003EE1 RID: 16097 RVA: 0x0015C0A1 File Offset: 0x0015A2A1
		private static void BuildAttribute_Fixed(XsdBuilder builder, string value)
		{
			builder.attribute.FixedValue = value;
		}

		// Token: 0x06003EE2 RID: 16098 RVA: 0x0015C0AF File Offset: 0x0015A2AF
		private static void BuildAttribute_Form(XsdBuilder builder, string value)
		{
			builder.attribute.Form = (XmlSchemaForm)builder.ParseEnum(value, "form", XsdBuilder.FormStringValues);
		}

		// Token: 0x06003EE3 RID: 16099 RVA: 0x0015C0CD File Offset: 0x0015A2CD
		private static void BuildAttribute_Use(XsdBuilder builder, string value)
		{
			builder.attribute.Use = (XmlSchemaUse)builder.ParseEnum(value, "use", XsdBuilder.UseStringValues);
		}

		// Token: 0x06003EE4 RID: 16100 RVA: 0x0015C0EB File Offset: 0x0015A2EB
		private static void BuildAttribute_Ref(XsdBuilder builder, string value)
		{
			builder.attribute.RefName = builder.ParseQName(value, "ref");
		}

		// Token: 0x06003EE5 RID: 16101 RVA: 0x0015C104 File Offset: 0x0015A304
		private static void BuildAttribute_Name(XsdBuilder builder, string value)
		{
			builder.attribute.Name = value;
		}

		// Token: 0x06003EE6 RID: 16102 RVA: 0x0015C112 File Offset: 0x0015A312
		private static void BuildAttribute_Type(XsdBuilder builder, string value)
		{
			builder.attribute.SchemaTypeName = builder.ParseQName(value, "type");
		}

		// Token: 0x06003EE7 RID: 16103 RVA: 0x0015C12C File Offset: 0x0015A32C
		private static void InitElement(XsdBuilder builder, string value)
		{
			builder.xso = (builder.element = new XmlSchemaElement());
			builder.canIncludeImport = false;
			SchemaNames.Token parentElement = builder.ParentElement;
			if (parentElement == SchemaNames.Token.XsdSchema)
			{
				builder.schema.Items.Add(builder.element);
				return;
			}
			switch (parentElement)
			{
			case SchemaNames.Token.XsdAll:
				builder.all.Items.Add(builder.element);
				return;
			case SchemaNames.Token.XsdChoice:
				builder.choice.Items.Add(builder.element);
				return;
			case SchemaNames.Token.XsdSequence:
				builder.sequence.Items.Add(builder.element);
				return;
			default:
				return;
			}
		}

		// Token: 0x06003EE8 RID: 16104 RVA: 0x0015C1D5 File Offset: 0x0015A3D5
		private static void BuildElement_Abstract(XsdBuilder builder, string value)
		{
			builder.element.IsAbstract = builder.ParseBoolean(value, "abstract");
		}

		// Token: 0x06003EE9 RID: 16105 RVA: 0x0015C1EE File Offset: 0x0015A3EE
		private static void BuildElement_Block(XsdBuilder builder, string value)
		{
			builder.element.Block = (XmlSchemaDerivationMethod)builder.ParseBlockFinalEnum(value, "block");
		}

		// Token: 0x06003EEA RID: 16106 RVA: 0x0015C207 File Offset: 0x0015A407
		private static void BuildElement_Default(XsdBuilder builder, string value)
		{
			builder.element.DefaultValue = value;
		}

		// Token: 0x06003EEB RID: 16107 RVA: 0x0015C215 File Offset: 0x0015A415
		private static void BuildElement_Form(XsdBuilder builder, string value)
		{
			builder.element.Form = (XmlSchemaForm)builder.ParseEnum(value, "form", XsdBuilder.FormStringValues);
		}

		// Token: 0x06003EEC RID: 16108 RVA: 0x0015C233 File Offset: 0x0015A433
		private static void BuildElement_SubstitutionGroup(XsdBuilder builder, string value)
		{
			builder.element.SubstitutionGroup = builder.ParseQName(value, "substitutionGroup");
		}

		// Token: 0x06003EED RID: 16109 RVA: 0x0015C24C File Offset: 0x0015A44C
		private static void BuildElement_Final(XsdBuilder builder, string value)
		{
			builder.element.Final = (XmlSchemaDerivationMethod)builder.ParseBlockFinalEnum(value, "final");
		}

		// Token: 0x06003EEE RID: 16110 RVA: 0x0015C265 File Offset: 0x0015A465
		private static void BuildElement_Fixed(XsdBuilder builder, string value)
		{
			builder.element.FixedValue = value;
		}

		// Token: 0x06003EEF RID: 16111 RVA: 0x0015C273 File Offset: 0x0015A473
		private static void BuildElement_MaxOccurs(XsdBuilder builder, string value)
		{
			builder.SetMaxOccurs(builder.element, value);
		}

		// Token: 0x06003EF0 RID: 16112 RVA: 0x0015C282 File Offset: 0x0015A482
		private static void BuildElement_MinOccurs(XsdBuilder builder, string value)
		{
			builder.SetMinOccurs(builder.element, value);
		}

		// Token: 0x06003EF1 RID: 16113 RVA: 0x0015C291 File Offset: 0x0015A491
		private static void BuildElement_Name(XsdBuilder builder, string value)
		{
			builder.element.Name = value;
		}

		// Token: 0x06003EF2 RID: 16114 RVA: 0x0015C29F File Offset: 0x0015A49F
		private static void BuildElement_Nillable(XsdBuilder builder, string value)
		{
			builder.element.IsNillable = builder.ParseBoolean(value, "nillable");
		}

		// Token: 0x06003EF3 RID: 16115 RVA: 0x0015C2B8 File Offset: 0x0015A4B8
		private static void BuildElement_Ref(XsdBuilder builder, string value)
		{
			builder.element.RefName = builder.ParseQName(value, "ref");
		}

		// Token: 0x06003EF4 RID: 16116 RVA: 0x0015C2D1 File Offset: 0x0015A4D1
		private static void BuildElement_Type(XsdBuilder builder, string value)
		{
			builder.element.SchemaTypeName = builder.ParseQName(value, "type");
		}

		// Token: 0x06003EF5 RID: 16117 RVA: 0x0015C2EC File Offset: 0x0015A4EC
		private static void InitSimpleType(XsdBuilder builder, string value)
		{
			builder.xso = (builder.simpleType = new XmlSchemaSimpleType());
			SchemaNames.Token parentElement = builder.ParentElement;
			if (parentElement <= SchemaNames.Token.XsdElement)
			{
				if (parentElement == SchemaNames.Token.XsdSchema)
				{
					builder.canIncludeImport = false;
					builder.schema.Items.Add(builder.simpleType);
					return;
				}
				if (parentElement != SchemaNames.Token.XsdElement)
				{
					return;
				}
				if (builder.element.SchemaType != null)
				{
					builder.SendValidationEvent("'{0}' is a duplicate XSD element.", "simpleType");
				}
				if (builder.element.Constraints.Count != 0)
				{
					builder.SendValidationEvent("'simpleType' or 'complexType' cannot follow 'unique', 'key' or 'keyref'.", null);
				}
				builder.element.SchemaType = builder.simpleType;
				return;
			}
			else
			{
				if (parentElement != SchemaNames.Token.XsdAttribute)
				{
					switch (parentElement)
					{
					case SchemaNames.Token.XsdSimpleContentRestriction:
						if (builder.simpleContentRestriction.BaseType != null)
						{
							builder.SendValidationEvent("'{0}' is a duplicate XSD element.", "simpleType");
						}
						if (builder.simpleContentRestriction.Attributes.Count != 0 || builder.simpleContentRestriction.AnyAttribute != null || builder.simpleContentRestriction.Facets.Count != 0)
						{
							builder.SendValidationEvent("'simpleType' should be the first child of restriction.", null);
						}
						builder.simpleContentRestriction.BaseType = builder.simpleType;
						return;
					case SchemaNames.Token.XsdSimpleTypeList:
						if (builder.simpleTypeList.ItemType != null)
						{
							builder.SendValidationEvent("'{0}' is a duplicate XSD element.", "simpleType");
						}
						builder.simpleTypeList.ItemType = builder.simpleType;
						return;
					case SchemaNames.Token.XsdSimpleTypeRestriction:
						if (builder.simpleTypeRestriction.BaseType != null)
						{
							builder.SendValidationEvent("'{0}' is a duplicate XSD element.", "simpleType");
						}
						builder.simpleTypeRestriction.BaseType = builder.simpleType;
						return;
					case SchemaNames.Token.XsdSimpleTypeUnion:
						builder.simpleTypeUnion.BaseTypes.Add(builder.simpleType);
						break;
					case SchemaNames.Token.XsdWhitespace:
						break;
					case SchemaNames.Token.XsdRedefine:
						builder.redefine.Items.Add(builder.simpleType);
						return;
					default:
						return;
					}
					return;
				}
				if (builder.attribute.SchemaType != null)
				{
					builder.SendValidationEvent("'{0}' is a duplicate XSD element.", "simpleType");
				}
				builder.attribute.SchemaType = builder.simpleType;
				return;
			}
		}

		// Token: 0x06003EF6 RID: 16118 RVA: 0x0015C4E2 File Offset: 0x0015A6E2
		private static void BuildSimpleType_Name(XsdBuilder builder, string value)
		{
			builder.simpleType.Name = value;
		}

		// Token: 0x06003EF7 RID: 16119 RVA: 0x0015C4F0 File Offset: 0x0015A6F0
		private static void BuildSimpleType_Final(XsdBuilder builder, string value)
		{
			builder.simpleType.Final = (XmlSchemaDerivationMethod)builder.ParseBlockFinalEnum(value, "final");
		}

		// Token: 0x06003EF8 RID: 16120 RVA: 0x0015C50C File Offset: 0x0015A70C
		private static void InitSimpleTypeUnion(XsdBuilder builder, string value)
		{
			if (builder.simpleType.Content != null)
			{
				builder.SendValidationEvent("'simpleType' should have only one child 'union', 'list', or 'restriction'.", null);
			}
			builder.xso = (builder.simpleTypeUnion = new XmlSchemaSimpleTypeUnion());
			builder.simpleType.Content = builder.simpleTypeUnion;
		}

		// Token: 0x06003EF9 RID: 16121 RVA: 0x0015C558 File Offset: 0x0015A758
		private static void BuildSimpleTypeUnion_MemberTypes(XsdBuilder builder, string value)
		{
			XmlSchemaDatatype xmlSchemaDatatype = XmlSchemaDatatype.FromXmlTokenizedTypeXsd(XmlTokenizedType.QName).DeriveByList(null);
			try
			{
				builder.simpleTypeUnion.MemberTypes = (XmlQualifiedName[])xmlSchemaDatatype.ParseValue(value, builder.nameTable, builder.namespaceManager);
			}
			catch (XmlSchemaException ex)
			{
				ex.SetSource(builder.reader.BaseURI, builder.positionInfo.LineNumber, builder.positionInfo.LinePosition);
				builder.SendValidationEvent(ex);
			}
		}

		// Token: 0x06003EFA RID: 16122 RVA: 0x0015C5DC File Offset: 0x0015A7DC
		private static void InitSimpleTypeList(XsdBuilder builder, string value)
		{
			if (builder.simpleType.Content != null)
			{
				builder.SendValidationEvent("'simpleType' should have only one child 'union', 'list', or 'restriction'.", null);
			}
			builder.xso = (builder.simpleTypeList = new XmlSchemaSimpleTypeList());
			builder.simpleType.Content = builder.simpleTypeList;
		}

		// Token: 0x06003EFB RID: 16123 RVA: 0x0015C627 File Offset: 0x0015A827
		private static void BuildSimpleTypeList_ItemType(XsdBuilder builder, string value)
		{
			builder.simpleTypeList.ItemTypeName = builder.ParseQName(value, "itemType");
		}

		// Token: 0x06003EFC RID: 16124 RVA: 0x0015C640 File Offset: 0x0015A840
		private static void InitSimpleTypeRestriction(XsdBuilder builder, string value)
		{
			if (builder.simpleType.Content != null)
			{
				builder.SendValidationEvent("'simpleType' should have only one child 'union', 'list', or 'restriction'.", null);
			}
			builder.xso = (builder.simpleTypeRestriction = new XmlSchemaSimpleTypeRestriction());
			builder.simpleType.Content = builder.simpleTypeRestriction;
		}

		// Token: 0x06003EFD RID: 16125 RVA: 0x0015C68B File Offset: 0x0015A88B
		private static void BuildSimpleTypeRestriction_Base(XsdBuilder builder, string value)
		{
			builder.simpleTypeRestriction.BaseTypeName = builder.ParseQName(value, "base");
		}

		// Token: 0x06003EFE RID: 16126 RVA: 0x0015C6A4 File Offset: 0x0015A8A4
		private static void InitComplexType(XsdBuilder builder, string value)
		{
			builder.xso = (builder.complexType = new XmlSchemaComplexType());
			SchemaNames.Token parentElement = builder.ParentElement;
			if (parentElement == SchemaNames.Token.XsdSchema)
			{
				builder.canIncludeImport = false;
				builder.schema.Items.Add(builder.complexType);
				return;
			}
			if (parentElement == SchemaNames.Token.XsdElement)
			{
				if (builder.element.SchemaType != null)
				{
					builder.SendValidationEvent("The '{0}' element already exists in the content model.", "complexType");
				}
				if (builder.element.Constraints.Count != 0)
				{
					builder.SendValidationEvent("'simpleType' or 'complexType' cannot follow 'unique', 'key' or 'keyref'.", null);
				}
				builder.element.SchemaType = builder.complexType;
				return;
			}
			if (parentElement != SchemaNames.Token.XsdRedefine)
			{
				return;
			}
			builder.redefine.Items.Add(builder.complexType);
		}

		// Token: 0x06003EFF RID: 16127 RVA: 0x0015C75F File Offset: 0x0015A95F
		private static void BuildComplexType_Abstract(XsdBuilder builder, string value)
		{
			builder.complexType.IsAbstract = builder.ParseBoolean(value, "abstract");
		}

		// Token: 0x06003F00 RID: 16128 RVA: 0x0015C778 File Offset: 0x0015A978
		private static void BuildComplexType_Block(XsdBuilder builder, string value)
		{
			builder.complexType.Block = (XmlSchemaDerivationMethod)builder.ParseBlockFinalEnum(value, "block");
		}

		// Token: 0x06003F01 RID: 16129 RVA: 0x0015C791 File Offset: 0x0015A991
		private static void BuildComplexType_Final(XsdBuilder builder, string value)
		{
			builder.complexType.Final = (XmlSchemaDerivationMethod)builder.ParseBlockFinalEnum(value, "final");
		}

		// Token: 0x06003F02 RID: 16130 RVA: 0x0015C7AA File Offset: 0x0015A9AA
		private static void BuildComplexType_Mixed(XsdBuilder builder, string value)
		{
			builder.complexType.IsMixed = builder.ParseBoolean(value, "mixed");
		}

		// Token: 0x06003F03 RID: 16131 RVA: 0x0015C7C3 File Offset: 0x0015A9C3
		private static void BuildComplexType_Name(XsdBuilder builder, string value)
		{
			builder.complexType.Name = value;
		}

		// Token: 0x06003F04 RID: 16132 RVA: 0x0015C7D4 File Offset: 0x0015A9D4
		private static void InitComplexContent(XsdBuilder builder, string value)
		{
			if (builder.complexType.ContentModel != null || builder.complexType.Particle != null || builder.complexType.Attributes.Count != 0 || builder.complexType.AnyAttribute != null)
			{
				builder.SendValidationEvent("The content model of a complex type must consist of 'annotation' (if present); followed by zero or one of the following: 'simpleContent', 'complexContent', 'group', 'choice', 'sequence', or 'all'; followed by zero or more 'attribute' or 'attributeGroup'; followed by zero or one 'anyAttribute'.", "complexContent");
			}
			builder.xso = (builder.complexContent = new XmlSchemaComplexContent());
			builder.complexType.ContentModel = builder.complexContent;
		}

		// Token: 0x06003F05 RID: 16133 RVA: 0x0015C84F File Offset: 0x0015AA4F
		private static void BuildComplexContent_Mixed(XsdBuilder builder, string value)
		{
			builder.complexContent.IsMixed = builder.ParseBoolean(value, "mixed");
		}

		// Token: 0x06003F06 RID: 16134 RVA: 0x0015C868 File Offset: 0x0015AA68
		private static void InitComplexContentExtension(XsdBuilder builder, string value)
		{
			if (builder.complexContent.Content != null)
			{
				builder.SendValidationEvent("Complex content restriction or extension should consist of zero or one of 'group', 'choice', 'sequence', or 'all'; followed by zero or more 'attribute' or 'attributeGroup'; followed by zero or one 'anyAttribute'.", "extension");
			}
			builder.xso = (builder.complexContentExtension = new XmlSchemaComplexContentExtension());
			builder.complexContent.Content = builder.complexContentExtension;
		}

		// Token: 0x06003F07 RID: 16135 RVA: 0x0015C8B7 File Offset: 0x0015AAB7
		private static void BuildComplexContentExtension_Base(XsdBuilder builder, string value)
		{
			builder.complexContentExtension.BaseTypeName = builder.ParseQName(value, "base");
		}

		// Token: 0x06003F08 RID: 16136 RVA: 0x0015C8D0 File Offset: 0x0015AAD0
		private static void InitComplexContentRestriction(XsdBuilder builder, string value)
		{
			builder.xso = (builder.complexContentRestriction = new XmlSchemaComplexContentRestriction());
			builder.complexContent.Content = builder.complexContentRestriction;
		}

		// Token: 0x06003F09 RID: 16137 RVA: 0x0015C902 File Offset: 0x0015AB02
		private static void BuildComplexContentRestriction_Base(XsdBuilder builder, string value)
		{
			builder.complexContentRestriction.BaseTypeName = builder.ParseQName(value, "base");
		}

		// Token: 0x06003F0A RID: 16138 RVA: 0x0015C91C File Offset: 0x0015AB1C
		private static void InitSimpleContent(XsdBuilder builder, string value)
		{
			if (builder.complexType.ContentModel != null || builder.complexType.Particle != null || builder.complexType.Attributes.Count != 0 || builder.complexType.AnyAttribute != null)
			{
				builder.SendValidationEvent("The content model of a complex type must consist of 'annotation' (if present); followed by zero or one of the following: 'simpleContent', 'complexContent', 'group', 'choice', 'sequence', or 'all'; followed by zero or more 'attribute' or 'attributeGroup'; followed by zero or one 'anyAttribute'.", "simpleContent");
			}
			builder.xso = (builder.simpleContent = new XmlSchemaSimpleContent());
			builder.complexType.ContentModel = builder.simpleContent;
		}

		// Token: 0x06003F0B RID: 16139 RVA: 0x0015C998 File Offset: 0x0015AB98
		private static void InitSimpleContentExtension(XsdBuilder builder, string value)
		{
			if (builder.simpleContent.Content != null)
			{
				builder.SendValidationEvent("The '{0}' element already exists in the content model.", "extension");
			}
			builder.xso = (builder.simpleContentExtension = new XmlSchemaSimpleContentExtension());
			builder.simpleContent.Content = builder.simpleContentExtension;
		}

		// Token: 0x06003F0C RID: 16140 RVA: 0x0015C9E7 File Offset: 0x0015ABE7
		private static void BuildSimpleContentExtension_Base(XsdBuilder builder, string value)
		{
			builder.simpleContentExtension.BaseTypeName = builder.ParseQName(value, "base");
		}

		// Token: 0x06003F0D RID: 16141 RVA: 0x0015CA00 File Offset: 0x0015AC00
		private static void InitSimpleContentRestriction(XsdBuilder builder, string value)
		{
			if (builder.simpleContent.Content != null)
			{
				builder.SendValidationEvent("The '{0}' element already exists in the content model.", "restriction");
			}
			builder.xso = (builder.simpleContentRestriction = new XmlSchemaSimpleContentRestriction());
			builder.simpleContent.Content = builder.simpleContentRestriction;
		}

		// Token: 0x06003F0E RID: 16142 RVA: 0x0015CA4F File Offset: 0x0015AC4F
		private static void BuildSimpleContentRestriction_Base(XsdBuilder builder, string value)
		{
			builder.simpleContentRestriction.BaseTypeName = builder.ParseQName(value, "base");
		}

		// Token: 0x06003F0F RID: 16143 RVA: 0x0015CA68 File Offset: 0x0015AC68
		private static void InitAttributeGroup(XsdBuilder builder, string value)
		{
			builder.canIncludeImport = false;
			builder.xso = (builder.attributeGroup = new XmlSchemaAttributeGroup());
			SchemaNames.Token parentElement = builder.ParentElement;
			if (parentElement == SchemaNames.Token.XsdSchema)
			{
				builder.schema.Items.Add(builder.attributeGroup);
				return;
			}
			if (parentElement != SchemaNames.Token.XsdRedefine)
			{
				return;
			}
			builder.redefine.Items.Add(builder.attributeGroup);
		}

		// Token: 0x06003F10 RID: 16144 RVA: 0x0015CAD1 File Offset: 0x0015ACD1
		private static void BuildAttributeGroup_Name(XsdBuilder builder, string value)
		{
			builder.attributeGroup.Name = value;
		}

		// Token: 0x06003F11 RID: 16145 RVA: 0x0015CAE0 File Offset: 0x0015ACE0
		private static void InitAttributeGroupRef(XsdBuilder builder, string value)
		{
			builder.xso = (builder.attributeGroupRef = new XmlSchemaAttributeGroupRef());
			builder.AddAttribute(builder.attributeGroupRef);
		}

		// Token: 0x06003F12 RID: 16146 RVA: 0x0015CB0D File Offset: 0x0015AD0D
		private static void BuildAttributeGroupRef_Ref(XsdBuilder builder, string value)
		{
			builder.attributeGroupRef.RefName = builder.ParseQName(value, "ref");
		}

		// Token: 0x06003F13 RID: 16147 RVA: 0x0015CB28 File Offset: 0x0015AD28
		private static void InitAnyAttribute(XsdBuilder builder, string value)
		{
			builder.xso = (builder.anyAttribute = new XmlSchemaAnyAttribute());
			SchemaNames.Token parentElement = builder.ParentElement;
			if (parentElement != SchemaNames.Token.xsdAttributeGroup)
			{
				if (parentElement == SchemaNames.Token.XsdComplexType)
				{
					if (builder.complexType.ContentModel != null)
					{
						builder.SendValidationEvent("'{0}' and content model are mutually exclusive.", "anyAttribute");
					}
					if (builder.complexType.AnyAttribute != null)
					{
						builder.SendValidationEvent("The '{0}' element already exists in the content model.", "anyAttribute");
					}
					builder.complexType.AnyAttribute = builder.anyAttribute;
					return;
				}
				switch (parentElement)
				{
				case SchemaNames.Token.XsdComplexContentExtension:
					if (builder.complexContentExtension.AnyAttribute != null)
					{
						builder.SendValidationEvent("The '{0}' element already exists in the content model.", "anyAttribute");
					}
					builder.complexContentExtension.AnyAttribute = builder.anyAttribute;
					return;
				case SchemaNames.Token.XsdComplexContentRestriction:
					if (builder.complexContentRestriction.AnyAttribute != null)
					{
						builder.SendValidationEvent("The '{0}' element already exists in the content model.", "anyAttribute");
					}
					builder.complexContentRestriction.AnyAttribute = builder.anyAttribute;
					return;
				case SchemaNames.Token.XsdSimpleContent:
					break;
				case SchemaNames.Token.XsdSimpleContentExtension:
					if (builder.simpleContentExtension.AnyAttribute != null)
					{
						builder.SendValidationEvent("The '{0}' element already exists in the content model.", "anyAttribute");
					}
					builder.simpleContentExtension.AnyAttribute = builder.anyAttribute;
					return;
				case SchemaNames.Token.XsdSimpleContentRestriction:
					if (builder.simpleContentRestriction.AnyAttribute != null)
					{
						builder.SendValidationEvent("The '{0}' element already exists in the content model.", "anyAttribute");
					}
					builder.simpleContentRestriction.AnyAttribute = builder.anyAttribute;
					return;
				default:
					return;
				}
			}
			else
			{
				if (builder.attributeGroup.AnyAttribute != null)
				{
					builder.SendValidationEvent("The '{0}' element already exists in the content model.", "anyAttribute");
				}
				builder.attributeGroup.AnyAttribute = builder.anyAttribute;
			}
		}

		// Token: 0x06003F14 RID: 16148 RVA: 0x0015CCB1 File Offset: 0x0015AEB1
		private static void BuildAnyAttribute_Namespace(XsdBuilder builder, string value)
		{
			builder.anyAttribute.Namespace = value;
		}

		// Token: 0x06003F15 RID: 16149 RVA: 0x0015CCBF File Offset: 0x0015AEBF
		private static void BuildAnyAttribute_ProcessContents(XsdBuilder builder, string value)
		{
			builder.anyAttribute.ProcessContents = (XmlSchemaContentProcessing)builder.ParseEnum(value, "processContents", XsdBuilder.ProcessContentsStringValues);
		}

		// Token: 0x06003F16 RID: 16150 RVA: 0x0015CCE0 File Offset: 0x0015AEE0
		private static void InitGroup(XsdBuilder builder, string value)
		{
			builder.xso = (builder.group = new XmlSchemaGroup());
			builder.canIncludeImport = false;
			SchemaNames.Token parentElement = builder.ParentElement;
			if (parentElement == SchemaNames.Token.XsdSchema)
			{
				builder.schema.Items.Add(builder.group);
				return;
			}
			if (parentElement != SchemaNames.Token.XsdRedefine)
			{
				return;
			}
			builder.redefine.Items.Add(builder.group);
		}

		// Token: 0x06003F17 RID: 16151 RVA: 0x0015CD49 File Offset: 0x0015AF49
		private static void BuildGroup_Name(XsdBuilder builder, string value)
		{
			builder.group.Name = value;
		}

		// Token: 0x06003F18 RID: 16152 RVA: 0x0015CD58 File Offset: 0x0015AF58
		private static void InitGroupRef(XsdBuilder builder, string value)
		{
			builder.xso = (builder.particle = (builder.groupRef = new XmlSchemaGroupRef()));
			builder.AddParticle(builder.groupRef);
		}

		// Token: 0x06003F19 RID: 16153 RVA: 0x0015CD8E File Offset: 0x0015AF8E
		private static void BuildParticle_MaxOccurs(XsdBuilder builder, string value)
		{
			builder.SetMaxOccurs(builder.particle, value);
		}

		// Token: 0x06003F1A RID: 16154 RVA: 0x0015CD9D File Offset: 0x0015AF9D
		private static void BuildParticle_MinOccurs(XsdBuilder builder, string value)
		{
			builder.SetMinOccurs(builder.particle, value);
		}

		// Token: 0x06003F1B RID: 16155 RVA: 0x0015CDAC File Offset: 0x0015AFAC
		private static void BuildGroupRef_Ref(XsdBuilder builder, string value)
		{
			builder.groupRef.RefName = builder.ParseQName(value, "ref");
		}

		// Token: 0x06003F1C RID: 16156 RVA: 0x0015CDC8 File Offset: 0x0015AFC8
		private static void InitAll(XsdBuilder builder, string value)
		{
			builder.xso = (builder.particle = (builder.all = new XmlSchemaAll()));
			builder.AddParticle(builder.all);
		}

		// Token: 0x06003F1D RID: 16157 RVA: 0x0015CE00 File Offset: 0x0015B000
		private static void InitChoice(XsdBuilder builder, string value)
		{
			builder.xso = (builder.particle = (builder.choice = new XmlSchemaChoice()));
			builder.AddParticle(builder.choice);
		}

		// Token: 0x06003F1E RID: 16158 RVA: 0x0015CE38 File Offset: 0x0015B038
		private static void InitSequence(XsdBuilder builder, string value)
		{
			builder.xso = (builder.particle = (builder.sequence = new XmlSchemaSequence()));
			builder.AddParticle(builder.sequence);
		}

		// Token: 0x06003F1F RID: 16159 RVA: 0x0015CE70 File Offset: 0x0015B070
		private static void InitAny(XsdBuilder builder, string value)
		{
			builder.xso = (builder.particle = (builder.anyElement = new XmlSchemaAny()));
			builder.AddParticle(builder.anyElement);
		}

		// Token: 0x06003F20 RID: 16160 RVA: 0x0015CEA6 File Offset: 0x0015B0A6
		private static void BuildAny_Namespace(XsdBuilder builder, string value)
		{
			builder.anyElement.Namespace = value;
		}

		// Token: 0x06003F21 RID: 16161 RVA: 0x0015CEB4 File Offset: 0x0015B0B4
		private static void BuildAny_ProcessContents(XsdBuilder builder, string value)
		{
			builder.anyElement.ProcessContents = (XmlSchemaContentProcessing)builder.ParseEnum(value, "processContents", XsdBuilder.ProcessContentsStringValues);
		}

		// Token: 0x06003F22 RID: 16162 RVA: 0x0015CED4 File Offset: 0x0015B0D4
		private static void InitNotation(XsdBuilder builder, string value)
		{
			builder.xso = (builder.notation = new XmlSchemaNotation());
			builder.canIncludeImport = false;
			builder.schema.Items.Add(builder.notation);
		}

		// Token: 0x06003F23 RID: 16163 RVA: 0x0015CF13 File Offset: 0x0015B113
		private static void BuildNotation_Name(XsdBuilder builder, string value)
		{
			builder.notation.Name = value;
		}

		// Token: 0x06003F24 RID: 16164 RVA: 0x0015CF21 File Offset: 0x0015B121
		private static void BuildNotation_Public(XsdBuilder builder, string value)
		{
			builder.notation.Public = value;
		}

		// Token: 0x06003F25 RID: 16165 RVA: 0x0015CF2F File Offset: 0x0015B12F
		private static void BuildNotation_System(XsdBuilder builder, string value)
		{
			builder.notation.System = value;
		}

		// Token: 0x06003F26 RID: 16166 RVA: 0x0015CF40 File Offset: 0x0015B140
		private static void InitFacet(XsdBuilder builder, string value)
		{
			switch (builder.CurrentElement)
			{
			case SchemaNames.Token.XsdMinExclusive:
				builder.facet = new XmlSchemaMinExclusiveFacet();
				break;
			case SchemaNames.Token.XsdMinInclusive:
				builder.facet = new XmlSchemaMinInclusiveFacet();
				break;
			case SchemaNames.Token.XsdMaxExclusive:
				builder.facet = new XmlSchemaMaxExclusiveFacet();
				break;
			case SchemaNames.Token.XsdMaxInclusive:
				builder.facet = new XmlSchemaMaxInclusiveFacet();
				break;
			case SchemaNames.Token.XsdTotalDigits:
				builder.facet = new XmlSchemaTotalDigitsFacet();
				break;
			case SchemaNames.Token.XsdFractionDigits:
				builder.facet = new XmlSchemaFractionDigitsFacet();
				break;
			case SchemaNames.Token.XsdLength:
				builder.facet = new XmlSchemaLengthFacet();
				break;
			case SchemaNames.Token.XsdMinLength:
				builder.facet = new XmlSchemaMinLengthFacet();
				break;
			case SchemaNames.Token.XsdMaxLength:
				builder.facet = new XmlSchemaMaxLengthFacet();
				break;
			case SchemaNames.Token.XsdEnumeration:
				builder.facet = new XmlSchemaEnumerationFacet();
				break;
			case SchemaNames.Token.XsdPattern:
				builder.facet = new XmlSchemaPatternFacet();
				break;
			case SchemaNames.Token.XsdWhitespace:
				builder.facet = new XmlSchemaWhiteSpaceFacet();
				break;
			}
			builder.xso = builder.facet;
			if (SchemaNames.Token.XsdSimpleTypeRestriction == builder.ParentElement)
			{
				builder.simpleTypeRestriction.Facets.Add(builder.facet);
				return;
			}
			if (builder.simpleContentRestriction.Attributes.Count != 0 || builder.simpleContentRestriction.AnyAttribute != null)
			{
				builder.SendValidationEvent("Facet should go before 'attribute', 'attributeGroup', or 'anyAttribute'.", null);
			}
			builder.simpleContentRestriction.Facets.Add(builder.facet);
		}

		// Token: 0x06003F27 RID: 16167 RVA: 0x0015D0CE File Offset: 0x0015B2CE
		private static void BuildFacet_Fixed(XsdBuilder builder, string value)
		{
			builder.facet.IsFixed = builder.ParseBoolean(value, "fixed");
		}

		// Token: 0x06003F28 RID: 16168 RVA: 0x0015D0E7 File Offset: 0x0015B2E7
		private static void BuildFacet_Value(XsdBuilder builder, string value)
		{
			builder.facet.Value = value;
		}

		// Token: 0x06003F29 RID: 16169 RVA: 0x0015D0F8 File Offset: 0x0015B2F8
		private static void InitIdentityConstraint(XsdBuilder builder, string value)
		{
			if (!builder.element.RefName.IsEmpty)
			{
				builder.SendValidationEvent("When the ref attribute is present, the type attribute and complexType, simpleType, key, keyref, and unique elements cannot be present.", null);
			}
			switch (builder.CurrentElement)
			{
			case SchemaNames.Token.XsdUnique:
				builder.xso = (builder.identityConstraint = new XmlSchemaUnique());
				break;
			case SchemaNames.Token.XsdKey:
				builder.xso = (builder.identityConstraint = new XmlSchemaKey());
				break;
			case SchemaNames.Token.XsdKeyref:
				builder.xso = (builder.identityConstraint = new XmlSchemaKeyref());
				break;
			}
			builder.element.Constraints.Add(builder.identityConstraint);
		}

		// Token: 0x06003F2A RID: 16170 RVA: 0x0015D198 File Offset: 0x0015B398
		private static void BuildIdentityConstraint_Name(XsdBuilder builder, string value)
		{
			builder.identityConstraint.Name = value;
		}

		// Token: 0x06003F2B RID: 16171 RVA: 0x0015D1A6 File Offset: 0x0015B3A6
		private static void BuildIdentityConstraint_Refer(XsdBuilder builder, string value)
		{
			if (builder.identityConstraint is XmlSchemaKeyref)
			{
				((XmlSchemaKeyref)builder.identityConstraint).Refer = builder.ParseQName(value, "refer");
				return;
			}
			builder.SendValidationEvent("The '{0}' attribute is not supported in this context.", "refer");
		}

		// Token: 0x06003F2C RID: 16172 RVA: 0x0015D1E4 File Offset: 0x0015B3E4
		private static void InitSelector(XsdBuilder builder, string value)
		{
			builder.xso = (builder.xpath = new XmlSchemaXPath());
			if (builder.identityConstraint.Selector == null)
			{
				builder.identityConstraint.Selector = builder.xpath;
				return;
			}
			builder.SendValidationEvent("Selector cannot appear twice in one identity constraint.", builder.identityConstraint.Name);
		}

		// Token: 0x06003F2D RID: 16173 RVA: 0x0015D23A File Offset: 0x0015B43A
		private static void BuildSelector_XPath(XsdBuilder builder, string value)
		{
			builder.xpath.XPath = value;
		}

		// Token: 0x06003F2E RID: 16174 RVA: 0x0015D248 File Offset: 0x0015B448
		private static void InitField(XsdBuilder builder, string value)
		{
			builder.xso = (builder.xpath = new XmlSchemaXPath());
			if (builder.identityConstraint.Selector == null)
			{
				builder.SendValidationEvent("Cannot define fields before selector.", builder.identityConstraint.Name);
			}
			builder.identityConstraint.Fields.Add(builder.xpath);
		}

		// Token: 0x06003F2F RID: 16175 RVA: 0x0015D23A File Offset: 0x0015B43A
		private static void BuildField_XPath(XsdBuilder builder, string value)
		{
			builder.xpath.XPath = value;
		}

		// Token: 0x06003F30 RID: 16176 RVA: 0x0015D2A4 File Offset: 0x0015B4A4
		private static void InitAnnotation(XsdBuilder builder, string value)
		{
			if (builder.hasChild && builder.ParentElement != SchemaNames.Token.XsdSchema && builder.ParentElement != SchemaNames.Token.XsdRedefine)
			{
				builder.SendValidationEvent("The 'annotation' element cannot appear at this location.", null);
			}
			builder.xso = (builder.annotation = new XmlSchemaAnnotation());
			builder.ParentContainer.AddAnnotation(builder.annotation);
		}

		// Token: 0x06003F31 RID: 16177 RVA: 0x0015D300 File Offset: 0x0015B500
		private static void InitAppinfo(XsdBuilder builder, string value)
		{
			builder.xso = (builder.appInfo = new XmlSchemaAppInfo());
			builder.annotation.Items.Add(builder.appInfo);
			builder.markup = new XmlNode[0];
		}

		// Token: 0x06003F32 RID: 16178 RVA: 0x0015D344 File Offset: 0x0015B544
		private static void BuildAppinfo_Source(XsdBuilder builder, string value)
		{
			builder.appInfo.Source = XsdBuilder.ParseUriReference(value);
		}

		// Token: 0x06003F33 RID: 16179 RVA: 0x0015D357 File Offset: 0x0015B557
		private static void EndAppinfo(XsdBuilder builder)
		{
			builder.appInfo.Markup = builder.markup;
		}

		// Token: 0x06003F34 RID: 16180 RVA: 0x0015D36C File Offset: 0x0015B56C
		private static void InitDocumentation(XsdBuilder builder, string value)
		{
			builder.xso = (builder.documentation = new XmlSchemaDocumentation());
			builder.annotation.Items.Add(builder.documentation);
			builder.markup = new XmlNode[0];
		}

		// Token: 0x06003F35 RID: 16181 RVA: 0x0015D3B0 File Offset: 0x0015B5B0
		private static void BuildDocumentation_Source(XsdBuilder builder, string value)
		{
			builder.documentation.Source = XsdBuilder.ParseUriReference(value);
		}

		// Token: 0x06003F36 RID: 16182 RVA: 0x0015D3C4 File Offset: 0x0015B5C4
		private static void BuildDocumentation_XmlLang(XsdBuilder builder, string value)
		{
			try
			{
				builder.documentation.Language = value;
			}
			catch (XmlSchemaException ex)
			{
				ex.SetSource(builder.reader.BaseURI, builder.positionInfo.LineNumber, builder.positionInfo.LinePosition);
				builder.SendValidationEvent(ex);
			}
		}

		// Token: 0x06003F37 RID: 16183 RVA: 0x0015D420 File Offset: 0x0015B620
		private static void EndDocumentation(XsdBuilder builder)
		{
			builder.documentation.Markup = builder.markup;
		}

		// Token: 0x06003F38 RID: 16184 RVA: 0x0015D434 File Offset: 0x0015B634
		private void AddAttribute(XmlSchemaObject value)
		{
			SchemaNames.Token parentElement = this.ParentElement;
			if (parentElement != SchemaNames.Token.xsdAttributeGroup)
			{
				if (parentElement == SchemaNames.Token.XsdComplexType)
				{
					if (this.complexType.ContentModel != null)
					{
						this.SendValidationEvent("'{0}' and content model are mutually exclusive.", "attribute");
					}
					if (this.complexType.AnyAttribute != null)
					{
						this.SendValidationEvent("'anyAttribute' must be the last child.", null);
					}
					this.complexType.Attributes.Add(value);
					return;
				}
				switch (parentElement)
				{
				case SchemaNames.Token.XsdComplexContentExtension:
					if (this.complexContentExtension.AnyAttribute != null)
					{
						this.SendValidationEvent("'anyAttribute' must be the last child.", null);
					}
					this.complexContentExtension.Attributes.Add(value);
					return;
				case SchemaNames.Token.XsdComplexContentRestriction:
					if (this.complexContentRestriction.AnyAttribute != null)
					{
						this.SendValidationEvent("'anyAttribute' must be the last child.", null);
					}
					this.complexContentRestriction.Attributes.Add(value);
					return;
				case SchemaNames.Token.XsdSimpleContent:
					break;
				case SchemaNames.Token.XsdSimpleContentExtension:
					if (this.simpleContentExtension.AnyAttribute != null)
					{
						this.SendValidationEvent("'anyAttribute' must be the last child.", null);
					}
					this.simpleContentExtension.Attributes.Add(value);
					return;
				case SchemaNames.Token.XsdSimpleContentRestriction:
					if (this.simpleContentRestriction.AnyAttribute != null)
					{
						this.SendValidationEvent("'anyAttribute' must be the last child.", null);
					}
					this.simpleContentRestriction.Attributes.Add(value);
					return;
				default:
					return;
				}
			}
			else
			{
				if (this.attributeGroup.AnyAttribute != null)
				{
					this.SendValidationEvent("'anyAttribute' must be the last child.", null);
				}
				this.attributeGroup.Attributes.Add(value);
			}
		}

		// Token: 0x06003F39 RID: 16185 RVA: 0x0015D598 File Offset: 0x0015B798
		private void AddParticle(XmlSchemaParticle particle)
		{
			SchemaNames.Token parentElement = this.ParentElement;
			if (parentElement <= SchemaNames.Token.XsdSequence)
			{
				if (parentElement == SchemaNames.Token.XsdGroup)
				{
					if (this.group.Particle != null)
					{
						this.SendValidationEvent("The content model can only have one of the following; 'all', 'choice', or 'sequence'.", "particle");
					}
					this.group.Particle = (XmlSchemaGroupBase)particle;
					return;
				}
				if (parentElement - SchemaNames.Token.XsdChoice > 1)
				{
					return;
				}
				((XmlSchemaGroupBase)this.ParentContainer).Items.Add(particle);
				return;
			}
			else
			{
				if (parentElement == SchemaNames.Token.XsdComplexType)
				{
					if (this.complexType.ContentModel != null || this.complexType.Attributes.Count != 0 || this.complexType.AnyAttribute != null || this.complexType.Particle != null)
					{
						this.SendValidationEvent("The content model of a complex type must consist of 'annotation' (if present); followed by zero or one of the following: 'simpleContent', 'complexContent', 'group', 'choice', 'sequence', or 'all'; followed by zero or more 'attribute' or 'attributeGroup'; followed by zero or one 'anyAttribute'.", "complexType");
					}
					this.complexType.Particle = particle;
					return;
				}
				if (parentElement == SchemaNames.Token.XsdComplexContentExtension)
				{
					if (this.complexContentExtension.Particle != null || this.complexContentExtension.Attributes.Count != 0 || this.complexContentExtension.AnyAttribute != null)
					{
						this.SendValidationEvent("Complex content restriction or extension should consist of zero or one of 'group', 'choice', 'sequence', or 'all'; followed by zero or more 'attribute' or 'attributeGroup'; followed by zero or one 'anyAttribute'.", "ComplexContentExtension");
					}
					this.complexContentExtension.Particle = particle;
					return;
				}
				if (parentElement != SchemaNames.Token.XsdComplexContentRestriction)
				{
					return;
				}
				if (this.complexContentRestriction.Particle != null || this.complexContentRestriction.Attributes.Count != 0 || this.complexContentRestriction.AnyAttribute != null)
				{
					this.SendValidationEvent("Complex content restriction or extension should consist of zero or one of 'group', 'choice', 'sequence', or 'all'; followed by zero or more 'attribute' or 'attributeGroup'; followed by zero or one 'anyAttribute'.", "ComplexContentExtension");
				}
				this.complexContentRestriction.Particle = particle;
				return;
			}
		}

		// Token: 0x06003F3A RID: 16186 RVA: 0x0015D708 File Offset: 0x0015B908
		private bool GetNextState(XmlQualifiedName qname)
		{
			if (this.currentEntry.NextStates != null)
			{
				for (int i = 0; i < this.currentEntry.NextStates.Length; i++)
				{
					int num = (int)this.currentEntry.NextStates[i];
					if (this.schemaNames.TokenToQName[(int)XsdBuilder.SchemaEntries[num].Name].Equals(qname))
					{
						this.nextEntry = XsdBuilder.SchemaEntries[num];
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06003F3B RID: 16187 RVA: 0x0015D778 File Offset: 0x0015B978
		private bool IsSkipableElement(XmlQualifiedName qname)
		{
			return this.CurrentElement == SchemaNames.Token.XsdDocumentation || this.CurrentElement == SchemaNames.Token.XsdAppInfo;
		}

		// Token: 0x06003F3C RID: 16188 RVA: 0x0015D790 File Offset: 0x0015B990
		private void SetMinOccurs(XmlSchemaParticle particle, string value)
		{
			try
			{
				particle.MinOccursString = value;
			}
			catch (Exception)
			{
				this.SendValidationEvent("The value for the 'minOccurs' attribute must be xsd:nonNegativeInteger.", null);
			}
		}

		// Token: 0x06003F3D RID: 16189 RVA: 0x0015D7C8 File Offset: 0x0015B9C8
		private void SetMaxOccurs(XmlSchemaParticle particle, string value)
		{
			try
			{
				particle.MaxOccursString = value;
			}
			catch (Exception)
			{
				this.SendValidationEvent("The value for the 'maxOccurs' attribute must be xsd:nonNegativeInteger or 'unbounded'.", null);
			}
		}

		// Token: 0x06003F3E RID: 16190 RVA: 0x0015D800 File Offset: 0x0015BA00
		private bool ParseBoolean(string value, string attributeName)
		{
			bool result;
			try
			{
				result = XmlConvert.ToBoolean(value);
			}
			catch (Exception)
			{
				this.SendValidationEvent("'{1}' is an invalid value for the '{0}' attribute.", attributeName, value, null);
				result = false;
			}
			return result;
		}

		// Token: 0x06003F3F RID: 16191 RVA: 0x0015D83C File Offset: 0x0015BA3C
		private int ParseEnum(string value, string attributeName, string[] values)
		{
			string text = value.Trim();
			for (int i = 0; i < values.Length; i++)
			{
				if (values[i] == text)
				{
					return i + 1;
				}
			}
			this.SendValidationEvent("'{1}' is an invalid value for the '{0}' attribute.", attributeName, text, null);
			return 0;
		}

		// Token: 0x06003F40 RID: 16192 RVA: 0x0015D87C File Offset: 0x0015BA7C
		private XmlQualifiedName ParseQName(string value, string attributeName)
		{
			XmlQualifiedName result;
			try
			{
				value = XmlComplianceUtil.NonCDataNormalize(value);
				string text;
				result = XmlQualifiedName.Parse(value, this.namespaceManager, out text);
			}
			catch (Exception)
			{
				this.SendValidationEvent("'{1}' is an invalid value for the '{0}' attribute.", attributeName, value, null);
				result = XmlQualifiedName.Empty;
			}
			return result;
		}

		// Token: 0x06003F41 RID: 16193 RVA: 0x0015D8CC File Offset: 0x0015BACC
		private int ParseBlockFinalEnum(string value, string attributeName)
		{
			int num = 0;
			string[] array = XmlConvert.SplitString(value);
			for (int i = 0; i < array.Length; i++)
			{
				bool flag = false;
				int j = 0;
				while (j < XsdBuilder.DerivationMethodStrings.Length)
				{
					if (array[i] == XsdBuilder.DerivationMethodStrings[j])
					{
						if ((num & XsdBuilder.DerivationMethodValues[j]) != 0 && (num & XsdBuilder.DerivationMethodValues[j]) != XsdBuilder.DerivationMethodValues[j])
						{
							this.SendValidationEvent("'{1}' is an invalid value for the '{0}' attribute.", attributeName, value, null);
							return 0;
						}
						num |= XsdBuilder.DerivationMethodValues[j];
						flag = true;
						break;
					}
					else
					{
						j++;
					}
				}
				if (!flag)
				{
					this.SendValidationEvent("'{1}' is an invalid value for the '{0}' attribute.", attributeName, value, null);
					return 0;
				}
				if (num == 255 && value.Length > 4)
				{
					this.SendValidationEvent("'{1}' is an invalid value for the '{0}' attribute.", attributeName, value, null);
					return 0;
				}
			}
			return num;
		}

		// Token: 0x06003F42 RID: 16194 RVA: 0x00002068 File Offset: 0x00000268
		private static string ParseUriReference(string s)
		{
			return s;
		}

		// Token: 0x06003F43 RID: 16195 RVA: 0x0015D994 File Offset: 0x0015BB94
		private void SendValidationEvent(string code, string arg0, string arg1, string arg2)
		{
			this.SendValidationEvent(new XmlSchemaException(code, new string[]
			{
				arg0,
				arg1,
				arg2
			}, this.reader.BaseURI, this.positionInfo.LineNumber, this.positionInfo.LinePosition));
		}

		// Token: 0x06003F44 RID: 16196 RVA: 0x0015D9E1 File Offset: 0x0015BBE1
		private void SendValidationEvent(string code, string msg)
		{
			this.SendValidationEvent(new XmlSchemaException(code, msg, this.reader.BaseURI, this.positionInfo.LineNumber, this.positionInfo.LinePosition));
		}

		// Token: 0x06003F45 RID: 16197 RVA: 0x0015DA11 File Offset: 0x0015BC11
		private void SendValidationEvent(string code, string[] args, XmlSeverityType severity)
		{
			this.SendValidationEvent(new XmlSchemaException(code, args, this.reader.BaseURI, this.positionInfo.LineNumber, this.positionInfo.LinePosition), severity);
		}

		// Token: 0x06003F46 RID: 16198 RVA: 0x0015DA44 File Offset: 0x0015BC44
		private void SendValidationEvent(XmlSchemaException e, XmlSeverityType severity)
		{
			XmlSchema xmlSchema = this.schema;
			int errorCount = xmlSchema.ErrorCount;
			xmlSchema.ErrorCount = errorCount + 1;
			e.SetSchemaObject(this.schema);
			if (this.validationEventHandler != null)
			{
				this.validationEventHandler(null, new ValidationEventArgs(e, severity));
				return;
			}
			if (severity == XmlSeverityType.Error)
			{
				throw e;
			}
		}

		// Token: 0x06003F47 RID: 16199 RVA: 0x0015DA93 File Offset: 0x0015BC93
		private void SendValidationEvent(XmlSchemaException e)
		{
			this.SendValidationEvent(e, XmlSeverityType.Error);
		}

		// Token: 0x06003F48 RID: 16200 RVA: 0x0015DAA0 File Offset: 0x0015BCA0
		private void RecordPosition()
		{
			this.xso.SourceUri = this.reader.BaseURI;
			this.xso.LineNumber = this.positionInfo.LineNumber;
			this.xso.LinePosition = this.positionInfo.LinePosition;
			if (this.xso != this.schema)
			{
				this.xso.Parent = this.ParentContainer;
			}
		}

		// Token: 0x06003F49 RID: 16201 RVA: 0x0015DB10 File Offset: 0x0015BD10
		// Note: this type is marked as 'beforefieldinit'.
		static XsdBuilder()
		{
		}

		// Token: 0x04002C99 RID: 11417
		private const int STACK_INCREMENT = 10;

		// Token: 0x04002C9A RID: 11418
		private static readonly XsdBuilder.State[] SchemaElement = new XsdBuilder.State[]
		{
			XsdBuilder.State.Schema
		};

		// Token: 0x04002C9B RID: 11419
		private static readonly XsdBuilder.State[] SchemaSubelements = new XsdBuilder.State[]
		{
			XsdBuilder.State.Annotation,
			XsdBuilder.State.Include,
			XsdBuilder.State.Import,
			XsdBuilder.State.Redefine,
			XsdBuilder.State.ComplexType,
			XsdBuilder.State.SimpleType,
			XsdBuilder.State.Element,
			XsdBuilder.State.Attribute,
			XsdBuilder.State.AttributeGroup,
			XsdBuilder.State.Group,
			XsdBuilder.State.Notation
		};

		// Token: 0x04002C9C RID: 11420
		private static readonly XsdBuilder.State[] AttributeSubelements = new XsdBuilder.State[]
		{
			XsdBuilder.State.Annotation,
			XsdBuilder.State.SimpleType
		};

		// Token: 0x04002C9D RID: 11421
		private static readonly XsdBuilder.State[] ElementSubelements = new XsdBuilder.State[]
		{
			XsdBuilder.State.Annotation,
			XsdBuilder.State.SimpleType,
			XsdBuilder.State.ComplexType,
			XsdBuilder.State.Unique,
			XsdBuilder.State.Key,
			XsdBuilder.State.KeyRef
		};

		// Token: 0x04002C9E RID: 11422
		private static readonly XsdBuilder.State[] ComplexTypeSubelements = new XsdBuilder.State[]
		{
			XsdBuilder.State.Annotation,
			XsdBuilder.State.SimpleContent,
			XsdBuilder.State.ComplexContent,
			XsdBuilder.State.GroupRef,
			XsdBuilder.State.All,
			XsdBuilder.State.Choice,
			XsdBuilder.State.Sequence,
			XsdBuilder.State.Attribute,
			XsdBuilder.State.AttributeGroupRef,
			XsdBuilder.State.AnyAttribute
		};

		// Token: 0x04002C9F RID: 11423
		private static readonly XsdBuilder.State[] SimpleContentSubelements = new XsdBuilder.State[]
		{
			XsdBuilder.State.Annotation,
			XsdBuilder.State.SimpleContentRestriction,
			XsdBuilder.State.SimpleContentExtension
		};

		// Token: 0x04002CA0 RID: 11424
		private static readonly XsdBuilder.State[] SimpleContentExtensionSubelements = new XsdBuilder.State[]
		{
			XsdBuilder.State.Annotation,
			XsdBuilder.State.Attribute,
			XsdBuilder.State.AttributeGroupRef,
			XsdBuilder.State.AnyAttribute
		};

		// Token: 0x04002CA1 RID: 11425
		private static readonly XsdBuilder.State[] SimpleContentRestrictionSubelements = new XsdBuilder.State[]
		{
			XsdBuilder.State.Annotation,
			XsdBuilder.State.SimpleType,
			XsdBuilder.State.Enumeration,
			XsdBuilder.State.Length,
			XsdBuilder.State.MaxExclusive,
			XsdBuilder.State.MaxInclusive,
			XsdBuilder.State.MaxLength,
			XsdBuilder.State.MinExclusive,
			XsdBuilder.State.MinInclusive,
			XsdBuilder.State.MinLength,
			XsdBuilder.State.Pattern,
			XsdBuilder.State.TotalDigits,
			XsdBuilder.State.FractionDigits,
			XsdBuilder.State.WhiteSpace,
			XsdBuilder.State.Attribute,
			XsdBuilder.State.AttributeGroupRef,
			XsdBuilder.State.AnyAttribute
		};

		// Token: 0x04002CA2 RID: 11426
		private static readonly XsdBuilder.State[] ComplexContentSubelements = new XsdBuilder.State[]
		{
			XsdBuilder.State.Annotation,
			XsdBuilder.State.ComplexContentRestriction,
			XsdBuilder.State.ComplexContentExtension
		};

		// Token: 0x04002CA3 RID: 11427
		private static readonly XsdBuilder.State[] ComplexContentExtensionSubelements = new XsdBuilder.State[]
		{
			XsdBuilder.State.Annotation,
			XsdBuilder.State.GroupRef,
			XsdBuilder.State.All,
			XsdBuilder.State.Choice,
			XsdBuilder.State.Sequence,
			XsdBuilder.State.Attribute,
			XsdBuilder.State.AttributeGroupRef,
			XsdBuilder.State.AnyAttribute
		};

		// Token: 0x04002CA4 RID: 11428
		private static readonly XsdBuilder.State[] ComplexContentRestrictionSubelements = new XsdBuilder.State[]
		{
			XsdBuilder.State.Annotation,
			XsdBuilder.State.GroupRef,
			XsdBuilder.State.All,
			XsdBuilder.State.Choice,
			XsdBuilder.State.Sequence,
			XsdBuilder.State.Attribute,
			XsdBuilder.State.AttributeGroupRef,
			XsdBuilder.State.AnyAttribute
		};

		// Token: 0x04002CA5 RID: 11429
		private static readonly XsdBuilder.State[] SimpleTypeSubelements = new XsdBuilder.State[]
		{
			XsdBuilder.State.Annotation,
			XsdBuilder.State.SimpleTypeList,
			XsdBuilder.State.SimpleTypeRestriction,
			XsdBuilder.State.SimpleTypeUnion
		};

		// Token: 0x04002CA6 RID: 11430
		private static readonly XsdBuilder.State[] SimpleTypeRestrictionSubelements = new XsdBuilder.State[]
		{
			XsdBuilder.State.Annotation,
			XsdBuilder.State.SimpleType,
			XsdBuilder.State.Enumeration,
			XsdBuilder.State.Length,
			XsdBuilder.State.MaxExclusive,
			XsdBuilder.State.MaxInclusive,
			XsdBuilder.State.MaxLength,
			XsdBuilder.State.MinExclusive,
			XsdBuilder.State.MinInclusive,
			XsdBuilder.State.MinLength,
			XsdBuilder.State.Pattern,
			XsdBuilder.State.TotalDigits,
			XsdBuilder.State.FractionDigits,
			XsdBuilder.State.WhiteSpace
		};

		// Token: 0x04002CA7 RID: 11431
		private static readonly XsdBuilder.State[] SimpleTypeListSubelements = new XsdBuilder.State[]
		{
			XsdBuilder.State.Annotation,
			XsdBuilder.State.SimpleType
		};

		// Token: 0x04002CA8 RID: 11432
		private static readonly XsdBuilder.State[] SimpleTypeUnionSubelements = new XsdBuilder.State[]
		{
			XsdBuilder.State.Annotation,
			XsdBuilder.State.SimpleType
		};

		// Token: 0x04002CA9 RID: 11433
		private static readonly XsdBuilder.State[] RedefineSubelements = new XsdBuilder.State[]
		{
			XsdBuilder.State.Annotation,
			XsdBuilder.State.AttributeGroup,
			XsdBuilder.State.ComplexType,
			XsdBuilder.State.Group,
			XsdBuilder.State.SimpleType
		};

		// Token: 0x04002CAA RID: 11434
		private static readonly XsdBuilder.State[] AttributeGroupSubelements = new XsdBuilder.State[]
		{
			XsdBuilder.State.Annotation,
			XsdBuilder.State.Attribute,
			XsdBuilder.State.AttributeGroupRef,
			XsdBuilder.State.AnyAttribute
		};

		// Token: 0x04002CAB RID: 11435
		private static readonly XsdBuilder.State[] GroupSubelements = new XsdBuilder.State[]
		{
			XsdBuilder.State.Annotation,
			XsdBuilder.State.All,
			XsdBuilder.State.Choice,
			XsdBuilder.State.Sequence
		};

		// Token: 0x04002CAC RID: 11436
		private static readonly XsdBuilder.State[] AllSubelements = new XsdBuilder.State[]
		{
			XsdBuilder.State.Annotation,
			XsdBuilder.State.Element
		};

		// Token: 0x04002CAD RID: 11437
		private static readonly XsdBuilder.State[] ChoiceSequenceSubelements = new XsdBuilder.State[]
		{
			XsdBuilder.State.Annotation,
			XsdBuilder.State.Element,
			XsdBuilder.State.GroupRef,
			XsdBuilder.State.Choice,
			XsdBuilder.State.Sequence,
			XsdBuilder.State.Any
		};

		// Token: 0x04002CAE RID: 11438
		private static readonly XsdBuilder.State[] IdentityConstraintSubelements = new XsdBuilder.State[]
		{
			XsdBuilder.State.Annotation,
			XsdBuilder.State.Selector,
			XsdBuilder.State.Field
		};

		// Token: 0x04002CAF RID: 11439
		private static readonly XsdBuilder.State[] AnnotationSubelements = new XsdBuilder.State[]
		{
			XsdBuilder.State.AppInfo,
			XsdBuilder.State.Documentation
		};

		// Token: 0x04002CB0 RID: 11440
		private static readonly XsdBuilder.State[] AnnotatedSubelements = new XsdBuilder.State[]
		{
			XsdBuilder.State.Annotation
		};

		// Token: 0x04002CB1 RID: 11441
		private static readonly XsdBuilder.XsdAttributeEntry[] SchemaAttributes = new XsdBuilder.XsdAttributeEntry[]
		{
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaAttributeFormDefault, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildSchema_AttributeFormDefault)),
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaElementFormDefault, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildSchema_ElementFormDefault)),
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaTargetNamespace, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildSchema_TargetNamespace)),
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaId, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildAnnotated_Id)),
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaVersion, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildSchema_Version)),
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaFinalDefault, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildSchema_FinalDefault)),
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaBlockDefault, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildSchema_BlockDefault))
		};

		// Token: 0x04002CB2 RID: 11442
		private static readonly XsdBuilder.XsdAttributeEntry[] AttributeAttributes = new XsdBuilder.XsdAttributeEntry[]
		{
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaDefault, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildAttribute_Default)),
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaFixed, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildAttribute_Fixed)),
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaForm, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildAttribute_Form)),
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaId, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildAnnotated_Id)),
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaName, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildAttribute_Name)),
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaRef, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildAttribute_Ref)),
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaType, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildAttribute_Type)),
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaUse, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildAttribute_Use))
		};

		// Token: 0x04002CB3 RID: 11443
		private static readonly XsdBuilder.XsdAttributeEntry[] ElementAttributes = new XsdBuilder.XsdAttributeEntry[]
		{
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaAbstract, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildElement_Abstract)),
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaBlock, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildElement_Block)),
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaDefault, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildElement_Default)),
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaFinal, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildElement_Final)),
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaFixed, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildElement_Fixed)),
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaForm, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildElement_Form)),
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaId, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildAnnotated_Id)),
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaMaxOccurs, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildElement_MaxOccurs)),
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaMinOccurs, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildElement_MinOccurs)),
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaName, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildElement_Name)),
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaNillable, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildElement_Nillable)),
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaRef, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildElement_Ref)),
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaSubstitutionGroup, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildElement_SubstitutionGroup)),
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaType, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildElement_Type))
		};

		// Token: 0x04002CB4 RID: 11444
		private static readonly XsdBuilder.XsdAttributeEntry[] ComplexTypeAttributes = new XsdBuilder.XsdAttributeEntry[]
		{
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaAbstract, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildComplexType_Abstract)),
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaBlock, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildComplexType_Block)),
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaFinal, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildComplexType_Final)),
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaId, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildAnnotated_Id)),
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaMixed, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildComplexType_Mixed)),
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaName, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildComplexType_Name))
		};

		// Token: 0x04002CB5 RID: 11445
		private static readonly XsdBuilder.XsdAttributeEntry[] SimpleContentAttributes = new XsdBuilder.XsdAttributeEntry[]
		{
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaId, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildAnnotated_Id))
		};

		// Token: 0x04002CB6 RID: 11446
		private static readonly XsdBuilder.XsdAttributeEntry[] SimpleContentExtensionAttributes = new XsdBuilder.XsdAttributeEntry[]
		{
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaBase, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildSimpleContentExtension_Base)),
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaId, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildAnnotated_Id))
		};

		// Token: 0x04002CB7 RID: 11447
		private static readonly XsdBuilder.XsdAttributeEntry[] SimpleContentRestrictionAttributes = new XsdBuilder.XsdAttributeEntry[]
		{
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaBase, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildSimpleContentRestriction_Base)),
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaId, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildAnnotated_Id))
		};

		// Token: 0x04002CB8 RID: 11448
		private static readonly XsdBuilder.XsdAttributeEntry[] ComplexContentAttributes = new XsdBuilder.XsdAttributeEntry[]
		{
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaId, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildAnnotated_Id)),
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaMixed, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildComplexContent_Mixed))
		};

		// Token: 0x04002CB9 RID: 11449
		private static readonly XsdBuilder.XsdAttributeEntry[] ComplexContentExtensionAttributes = new XsdBuilder.XsdAttributeEntry[]
		{
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaBase, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildComplexContentExtension_Base)),
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaId, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildAnnotated_Id))
		};

		// Token: 0x04002CBA RID: 11450
		private static readonly XsdBuilder.XsdAttributeEntry[] ComplexContentRestrictionAttributes = new XsdBuilder.XsdAttributeEntry[]
		{
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaBase, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildComplexContentRestriction_Base)),
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaId, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildAnnotated_Id))
		};

		// Token: 0x04002CBB RID: 11451
		private static readonly XsdBuilder.XsdAttributeEntry[] SimpleTypeAttributes = new XsdBuilder.XsdAttributeEntry[]
		{
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaId, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildAnnotated_Id)),
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaFinal, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildSimpleType_Final)),
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaName, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildSimpleType_Name))
		};

		// Token: 0x04002CBC RID: 11452
		private static readonly XsdBuilder.XsdAttributeEntry[] SimpleTypeRestrictionAttributes = new XsdBuilder.XsdAttributeEntry[]
		{
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaBase, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildSimpleTypeRestriction_Base)),
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaId, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildAnnotated_Id))
		};

		// Token: 0x04002CBD RID: 11453
		private static readonly XsdBuilder.XsdAttributeEntry[] SimpleTypeUnionAttributes = new XsdBuilder.XsdAttributeEntry[]
		{
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaId, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildAnnotated_Id)),
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaMemberTypes, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildSimpleTypeUnion_MemberTypes))
		};

		// Token: 0x04002CBE RID: 11454
		private static readonly XsdBuilder.XsdAttributeEntry[] SimpleTypeListAttributes = new XsdBuilder.XsdAttributeEntry[]
		{
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaId, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildAnnotated_Id)),
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaItemType, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildSimpleTypeList_ItemType))
		};

		// Token: 0x04002CBF RID: 11455
		private static readonly XsdBuilder.XsdAttributeEntry[] AttributeGroupAttributes = new XsdBuilder.XsdAttributeEntry[]
		{
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaId, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildAnnotated_Id)),
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaName, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildAttributeGroup_Name))
		};

		// Token: 0x04002CC0 RID: 11456
		private static readonly XsdBuilder.XsdAttributeEntry[] AttributeGroupRefAttributes = new XsdBuilder.XsdAttributeEntry[]
		{
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaId, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildAnnotated_Id)),
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaRef, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildAttributeGroupRef_Ref))
		};

		// Token: 0x04002CC1 RID: 11457
		private static readonly XsdBuilder.XsdAttributeEntry[] GroupAttributes = new XsdBuilder.XsdAttributeEntry[]
		{
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaId, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildAnnotated_Id)),
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaName, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildGroup_Name))
		};

		// Token: 0x04002CC2 RID: 11458
		private static readonly XsdBuilder.XsdAttributeEntry[] GroupRefAttributes = new XsdBuilder.XsdAttributeEntry[]
		{
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaId, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildAnnotated_Id)),
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaMaxOccurs, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildParticle_MaxOccurs)),
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaMinOccurs, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildParticle_MinOccurs)),
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaRef, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildGroupRef_Ref))
		};

		// Token: 0x04002CC3 RID: 11459
		private static readonly XsdBuilder.XsdAttributeEntry[] ParticleAttributes = new XsdBuilder.XsdAttributeEntry[]
		{
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaId, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildAnnotated_Id)),
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaMaxOccurs, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildParticle_MaxOccurs)),
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaMinOccurs, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildParticle_MinOccurs))
		};

		// Token: 0x04002CC4 RID: 11460
		private static readonly XsdBuilder.XsdAttributeEntry[] AnyAttributes = new XsdBuilder.XsdAttributeEntry[]
		{
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaId, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildAnnotated_Id)),
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaMaxOccurs, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildParticle_MaxOccurs)),
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaMinOccurs, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildParticle_MinOccurs)),
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaNamespace, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildAny_Namespace)),
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaProcessContents, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildAny_ProcessContents))
		};

		// Token: 0x04002CC5 RID: 11461
		private static readonly XsdBuilder.XsdAttributeEntry[] IdentityConstraintAttributes = new XsdBuilder.XsdAttributeEntry[]
		{
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaId, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildAnnotated_Id)),
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaName, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildIdentityConstraint_Name)),
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaRefer, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildIdentityConstraint_Refer))
		};

		// Token: 0x04002CC6 RID: 11462
		private static readonly XsdBuilder.XsdAttributeEntry[] SelectorAttributes = new XsdBuilder.XsdAttributeEntry[]
		{
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaId, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildAnnotated_Id)),
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaXPath, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildSelector_XPath))
		};

		// Token: 0x04002CC7 RID: 11463
		private static readonly XsdBuilder.XsdAttributeEntry[] FieldAttributes = new XsdBuilder.XsdAttributeEntry[]
		{
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaId, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildAnnotated_Id)),
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaXPath, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildField_XPath))
		};

		// Token: 0x04002CC8 RID: 11464
		private static readonly XsdBuilder.XsdAttributeEntry[] NotationAttributes = new XsdBuilder.XsdAttributeEntry[]
		{
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaId, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildAnnotated_Id)),
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaName, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildNotation_Name)),
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaPublic, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildNotation_Public)),
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaSystem, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildNotation_System))
		};

		// Token: 0x04002CC9 RID: 11465
		private static readonly XsdBuilder.XsdAttributeEntry[] IncludeAttributes = new XsdBuilder.XsdAttributeEntry[]
		{
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaId, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildAnnotated_Id)),
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaSchemaLocation, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildInclude_SchemaLocation))
		};

		// Token: 0x04002CCA RID: 11466
		private static readonly XsdBuilder.XsdAttributeEntry[] ImportAttributes = new XsdBuilder.XsdAttributeEntry[]
		{
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaId, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildAnnotated_Id)),
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaNamespace, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildImport_Namespace)),
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaSchemaLocation, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildImport_SchemaLocation))
		};

		// Token: 0x04002CCB RID: 11467
		private static readonly XsdBuilder.XsdAttributeEntry[] FacetAttributes = new XsdBuilder.XsdAttributeEntry[]
		{
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaId, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildAnnotated_Id)),
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaFixed, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildFacet_Fixed)),
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaValue, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildFacet_Value))
		};

		// Token: 0x04002CCC RID: 11468
		private static readonly XsdBuilder.XsdAttributeEntry[] AnyAttributeAttributes = new XsdBuilder.XsdAttributeEntry[]
		{
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaId, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildAnnotated_Id)),
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaNamespace, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildAnyAttribute_Namespace)),
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaProcessContents, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildAnyAttribute_ProcessContents))
		};

		// Token: 0x04002CCD RID: 11469
		private static readonly XsdBuilder.XsdAttributeEntry[] DocumentationAttributes = new XsdBuilder.XsdAttributeEntry[]
		{
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaSource, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildDocumentation_Source)),
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.XmlLang, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildDocumentation_XmlLang))
		};

		// Token: 0x04002CCE RID: 11470
		private static readonly XsdBuilder.XsdAttributeEntry[] AppinfoAttributes = new XsdBuilder.XsdAttributeEntry[]
		{
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaSource, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildAppinfo_Source))
		};

		// Token: 0x04002CCF RID: 11471
		private static readonly XsdBuilder.XsdAttributeEntry[] RedefineAttributes = new XsdBuilder.XsdAttributeEntry[]
		{
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaId, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildAnnotated_Id)),
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaSchemaLocation, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildRedefine_SchemaLocation))
		};

		// Token: 0x04002CD0 RID: 11472
		private static readonly XsdBuilder.XsdAttributeEntry[] AnnotationAttributes = new XsdBuilder.XsdAttributeEntry[]
		{
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaId, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildAnnotated_Id))
		};

		// Token: 0x04002CD1 RID: 11473
		private static readonly XsdBuilder.XsdEntry[] SchemaEntries = new XsdBuilder.XsdEntry[]
		{
			new XsdBuilder.XsdEntry(SchemaNames.Token.Empty, XsdBuilder.State.Root, XsdBuilder.SchemaElement, null, null, null, true),
			new XsdBuilder.XsdEntry(SchemaNames.Token.XsdSchema, XsdBuilder.State.Schema, XsdBuilder.SchemaSubelements, XsdBuilder.SchemaAttributes, new XsdBuilder.XsdInitFunction(XsdBuilder.InitSchema), null, true),
			new XsdBuilder.XsdEntry(SchemaNames.Token.XsdAnnotation, XsdBuilder.State.Annotation, XsdBuilder.AnnotationSubelements, XsdBuilder.AnnotationAttributes, new XsdBuilder.XsdInitFunction(XsdBuilder.InitAnnotation), null, true),
			new XsdBuilder.XsdEntry(SchemaNames.Token.XsdInclude, XsdBuilder.State.Include, XsdBuilder.AnnotatedSubelements, XsdBuilder.IncludeAttributes, new XsdBuilder.XsdInitFunction(XsdBuilder.InitInclude), null, true),
			new XsdBuilder.XsdEntry(SchemaNames.Token.XsdImport, XsdBuilder.State.Import, XsdBuilder.AnnotatedSubelements, XsdBuilder.ImportAttributes, new XsdBuilder.XsdInitFunction(XsdBuilder.InitImport), null, true),
			new XsdBuilder.XsdEntry(SchemaNames.Token.XsdElement, XsdBuilder.State.Element, XsdBuilder.ElementSubelements, XsdBuilder.ElementAttributes, new XsdBuilder.XsdInitFunction(XsdBuilder.InitElement), null, true),
			new XsdBuilder.XsdEntry(SchemaNames.Token.XsdAttribute, XsdBuilder.State.Attribute, XsdBuilder.AttributeSubelements, XsdBuilder.AttributeAttributes, new XsdBuilder.XsdInitFunction(XsdBuilder.InitAttribute), null, true),
			new XsdBuilder.XsdEntry(SchemaNames.Token.xsdAttributeGroup, XsdBuilder.State.AttributeGroup, XsdBuilder.AttributeGroupSubelements, XsdBuilder.AttributeGroupAttributes, new XsdBuilder.XsdInitFunction(XsdBuilder.InitAttributeGroup), null, true),
			new XsdBuilder.XsdEntry(SchemaNames.Token.xsdAttributeGroup, XsdBuilder.State.AttributeGroupRef, XsdBuilder.AnnotatedSubelements, XsdBuilder.AttributeGroupRefAttributes, new XsdBuilder.XsdInitFunction(XsdBuilder.InitAttributeGroupRef), null, true),
			new XsdBuilder.XsdEntry(SchemaNames.Token.XsdAnyAttribute, XsdBuilder.State.AnyAttribute, XsdBuilder.AnnotatedSubelements, XsdBuilder.AnyAttributeAttributes, new XsdBuilder.XsdInitFunction(XsdBuilder.InitAnyAttribute), null, true),
			new XsdBuilder.XsdEntry(SchemaNames.Token.XsdGroup, XsdBuilder.State.Group, XsdBuilder.GroupSubelements, XsdBuilder.GroupAttributes, new XsdBuilder.XsdInitFunction(XsdBuilder.InitGroup), null, true),
			new XsdBuilder.XsdEntry(SchemaNames.Token.XsdGroup, XsdBuilder.State.GroupRef, XsdBuilder.AnnotatedSubelements, XsdBuilder.GroupRefAttributes, new XsdBuilder.XsdInitFunction(XsdBuilder.InitGroupRef), null, true),
			new XsdBuilder.XsdEntry(SchemaNames.Token.XsdAll, XsdBuilder.State.All, XsdBuilder.AllSubelements, XsdBuilder.ParticleAttributes, new XsdBuilder.XsdInitFunction(XsdBuilder.InitAll), null, true),
			new XsdBuilder.XsdEntry(SchemaNames.Token.XsdChoice, XsdBuilder.State.Choice, XsdBuilder.ChoiceSequenceSubelements, XsdBuilder.ParticleAttributes, new XsdBuilder.XsdInitFunction(XsdBuilder.InitChoice), null, true),
			new XsdBuilder.XsdEntry(SchemaNames.Token.XsdSequence, XsdBuilder.State.Sequence, XsdBuilder.ChoiceSequenceSubelements, XsdBuilder.ParticleAttributes, new XsdBuilder.XsdInitFunction(XsdBuilder.InitSequence), null, true),
			new XsdBuilder.XsdEntry(SchemaNames.Token.XsdAny, XsdBuilder.State.Any, XsdBuilder.AnnotatedSubelements, XsdBuilder.AnyAttributes, new XsdBuilder.XsdInitFunction(XsdBuilder.InitAny), null, true),
			new XsdBuilder.XsdEntry(SchemaNames.Token.XsdNotation, XsdBuilder.State.Notation, XsdBuilder.AnnotatedSubelements, XsdBuilder.NotationAttributes, new XsdBuilder.XsdInitFunction(XsdBuilder.InitNotation), null, true),
			new XsdBuilder.XsdEntry(SchemaNames.Token.XsdSimpleType, XsdBuilder.State.SimpleType, XsdBuilder.SimpleTypeSubelements, XsdBuilder.SimpleTypeAttributes, new XsdBuilder.XsdInitFunction(XsdBuilder.InitSimpleType), null, true),
			new XsdBuilder.XsdEntry(SchemaNames.Token.XsdComplexType, XsdBuilder.State.ComplexType, XsdBuilder.ComplexTypeSubelements, XsdBuilder.ComplexTypeAttributes, new XsdBuilder.XsdInitFunction(XsdBuilder.InitComplexType), null, true),
			new XsdBuilder.XsdEntry(SchemaNames.Token.XsdComplexContent, XsdBuilder.State.ComplexContent, XsdBuilder.ComplexContentSubelements, XsdBuilder.ComplexContentAttributes, new XsdBuilder.XsdInitFunction(XsdBuilder.InitComplexContent), null, true),
			new XsdBuilder.XsdEntry(SchemaNames.Token.XsdComplexContentRestriction, XsdBuilder.State.ComplexContentRestriction, XsdBuilder.ComplexContentRestrictionSubelements, XsdBuilder.ComplexContentRestrictionAttributes, new XsdBuilder.XsdInitFunction(XsdBuilder.InitComplexContentRestriction), null, true),
			new XsdBuilder.XsdEntry(SchemaNames.Token.XsdComplexContentExtension, XsdBuilder.State.ComplexContentExtension, XsdBuilder.ComplexContentExtensionSubelements, XsdBuilder.ComplexContentExtensionAttributes, new XsdBuilder.XsdInitFunction(XsdBuilder.InitComplexContentExtension), null, true),
			new XsdBuilder.XsdEntry(SchemaNames.Token.XsdSimpleContent, XsdBuilder.State.SimpleContent, XsdBuilder.SimpleContentSubelements, XsdBuilder.SimpleContentAttributes, new XsdBuilder.XsdInitFunction(XsdBuilder.InitSimpleContent), null, true),
			new XsdBuilder.XsdEntry(SchemaNames.Token.XsdSimpleContentExtension, XsdBuilder.State.SimpleContentExtension, XsdBuilder.SimpleContentExtensionSubelements, XsdBuilder.SimpleContentExtensionAttributes, new XsdBuilder.XsdInitFunction(XsdBuilder.InitSimpleContentExtension), null, true),
			new XsdBuilder.XsdEntry(SchemaNames.Token.XsdSimpleContentRestriction, XsdBuilder.State.SimpleContentRestriction, XsdBuilder.SimpleContentRestrictionSubelements, XsdBuilder.SimpleContentRestrictionAttributes, new XsdBuilder.XsdInitFunction(XsdBuilder.InitSimpleContentRestriction), null, true),
			new XsdBuilder.XsdEntry(SchemaNames.Token.XsdSimpleTypeUnion, XsdBuilder.State.SimpleTypeUnion, XsdBuilder.SimpleTypeUnionSubelements, XsdBuilder.SimpleTypeUnionAttributes, new XsdBuilder.XsdInitFunction(XsdBuilder.InitSimpleTypeUnion), null, true),
			new XsdBuilder.XsdEntry(SchemaNames.Token.XsdSimpleTypeList, XsdBuilder.State.SimpleTypeList, XsdBuilder.SimpleTypeListSubelements, XsdBuilder.SimpleTypeListAttributes, new XsdBuilder.XsdInitFunction(XsdBuilder.InitSimpleTypeList), null, true),
			new XsdBuilder.XsdEntry(SchemaNames.Token.XsdSimpleTypeRestriction, XsdBuilder.State.SimpleTypeRestriction, XsdBuilder.SimpleTypeRestrictionSubelements, XsdBuilder.SimpleTypeRestrictionAttributes, new XsdBuilder.XsdInitFunction(XsdBuilder.InitSimpleTypeRestriction), null, true),
			new XsdBuilder.XsdEntry(SchemaNames.Token.XsdUnique, XsdBuilder.State.Unique, XsdBuilder.IdentityConstraintSubelements, XsdBuilder.IdentityConstraintAttributes, new XsdBuilder.XsdInitFunction(XsdBuilder.InitIdentityConstraint), null, true),
			new XsdBuilder.XsdEntry(SchemaNames.Token.XsdKey, XsdBuilder.State.Key, XsdBuilder.IdentityConstraintSubelements, XsdBuilder.IdentityConstraintAttributes, new XsdBuilder.XsdInitFunction(XsdBuilder.InitIdentityConstraint), null, true),
			new XsdBuilder.XsdEntry(SchemaNames.Token.XsdKeyref, XsdBuilder.State.KeyRef, XsdBuilder.IdentityConstraintSubelements, XsdBuilder.IdentityConstraintAttributes, new XsdBuilder.XsdInitFunction(XsdBuilder.InitIdentityConstraint), null, true),
			new XsdBuilder.XsdEntry(SchemaNames.Token.XsdSelector, XsdBuilder.State.Selector, XsdBuilder.AnnotatedSubelements, XsdBuilder.SelectorAttributes, new XsdBuilder.XsdInitFunction(XsdBuilder.InitSelector), null, true),
			new XsdBuilder.XsdEntry(SchemaNames.Token.XsdField, XsdBuilder.State.Field, XsdBuilder.AnnotatedSubelements, XsdBuilder.FieldAttributes, new XsdBuilder.XsdInitFunction(XsdBuilder.InitField), null, true),
			new XsdBuilder.XsdEntry(SchemaNames.Token.XsdMinExclusive, XsdBuilder.State.MinExclusive, XsdBuilder.AnnotatedSubelements, XsdBuilder.FacetAttributes, new XsdBuilder.XsdInitFunction(XsdBuilder.InitFacet), null, true),
			new XsdBuilder.XsdEntry(SchemaNames.Token.XsdMinInclusive, XsdBuilder.State.MinInclusive, XsdBuilder.AnnotatedSubelements, XsdBuilder.FacetAttributes, new XsdBuilder.XsdInitFunction(XsdBuilder.InitFacet), null, true),
			new XsdBuilder.XsdEntry(SchemaNames.Token.XsdMaxExclusive, XsdBuilder.State.MaxExclusive, XsdBuilder.AnnotatedSubelements, XsdBuilder.FacetAttributes, new XsdBuilder.XsdInitFunction(XsdBuilder.InitFacet), null, true),
			new XsdBuilder.XsdEntry(SchemaNames.Token.XsdMaxInclusive, XsdBuilder.State.MaxInclusive, XsdBuilder.AnnotatedSubelements, XsdBuilder.FacetAttributes, new XsdBuilder.XsdInitFunction(XsdBuilder.InitFacet), null, true),
			new XsdBuilder.XsdEntry(SchemaNames.Token.XsdTotalDigits, XsdBuilder.State.TotalDigits, XsdBuilder.AnnotatedSubelements, XsdBuilder.FacetAttributes, new XsdBuilder.XsdInitFunction(XsdBuilder.InitFacet), null, true),
			new XsdBuilder.XsdEntry(SchemaNames.Token.XsdFractionDigits, XsdBuilder.State.FractionDigits, XsdBuilder.AnnotatedSubelements, XsdBuilder.FacetAttributes, new XsdBuilder.XsdInitFunction(XsdBuilder.InitFacet), null, true),
			new XsdBuilder.XsdEntry(SchemaNames.Token.XsdLength, XsdBuilder.State.Length, XsdBuilder.AnnotatedSubelements, XsdBuilder.FacetAttributes, new XsdBuilder.XsdInitFunction(XsdBuilder.InitFacet), null, true),
			new XsdBuilder.XsdEntry(SchemaNames.Token.XsdMinLength, XsdBuilder.State.MinLength, XsdBuilder.AnnotatedSubelements, XsdBuilder.FacetAttributes, new XsdBuilder.XsdInitFunction(XsdBuilder.InitFacet), null, true),
			new XsdBuilder.XsdEntry(SchemaNames.Token.XsdMaxLength, XsdBuilder.State.MaxLength, XsdBuilder.AnnotatedSubelements, XsdBuilder.FacetAttributes, new XsdBuilder.XsdInitFunction(XsdBuilder.InitFacet), null, true),
			new XsdBuilder.XsdEntry(SchemaNames.Token.XsdEnumeration, XsdBuilder.State.Enumeration, XsdBuilder.AnnotatedSubelements, XsdBuilder.FacetAttributes, new XsdBuilder.XsdInitFunction(XsdBuilder.InitFacet), null, true),
			new XsdBuilder.XsdEntry(SchemaNames.Token.XsdPattern, XsdBuilder.State.Pattern, XsdBuilder.AnnotatedSubelements, XsdBuilder.FacetAttributes, new XsdBuilder.XsdInitFunction(XsdBuilder.InitFacet), null, true),
			new XsdBuilder.XsdEntry(SchemaNames.Token.XsdWhitespace, XsdBuilder.State.WhiteSpace, XsdBuilder.AnnotatedSubelements, XsdBuilder.FacetAttributes, new XsdBuilder.XsdInitFunction(XsdBuilder.InitFacet), null, true),
			new XsdBuilder.XsdEntry(SchemaNames.Token.XsdAppInfo, XsdBuilder.State.AppInfo, null, XsdBuilder.AppinfoAttributes, new XsdBuilder.XsdInitFunction(XsdBuilder.InitAppinfo), new XsdBuilder.XsdEndChildFunction(XsdBuilder.EndAppinfo), false),
			new XsdBuilder.XsdEntry(SchemaNames.Token.XsdDocumentation, XsdBuilder.State.Documentation, null, XsdBuilder.DocumentationAttributes, new XsdBuilder.XsdInitFunction(XsdBuilder.InitDocumentation), new XsdBuilder.XsdEndChildFunction(XsdBuilder.EndDocumentation), false),
			new XsdBuilder.XsdEntry(SchemaNames.Token.XsdRedefine, XsdBuilder.State.Redefine, XsdBuilder.RedefineSubelements, XsdBuilder.RedefineAttributes, new XsdBuilder.XsdInitFunction(XsdBuilder.InitRedefine), new XsdBuilder.XsdEndChildFunction(XsdBuilder.EndRedefine), true)
		};

		// Token: 0x04002CD2 RID: 11474
		private static readonly int[] DerivationMethodValues = new int[]
		{
			1,
			2,
			4,
			8,
			16,
			255
		};

		// Token: 0x04002CD3 RID: 11475
		private static readonly string[] DerivationMethodStrings = new string[]
		{
			"substitution",
			"extension",
			"restriction",
			"list",
			"union",
			"#all"
		};

		// Token: 0x04002CD4 RID: 11476
		private static readonly string[] FormStringValues = new string[]
		{
			"qualified",
			"unqualified"
		};

		// Token: 0x04002CD5 RID: 11477
		private static readonly string[] UseStringValues = new string[]
		{
			"optional",
			"prohibited",
			"required"
		};

		// Token: 0x04002CD6 RID: 11478
		private static readonly string[] ProcessContentsStringValues = new string[]
		{
			"skip",
			"lax",
			"strict"
		};

		// Token: 0x04002CD7 RID: 11479
		private XmlReader reader;

		// Token: 0x04002CD8 RID: 11480
		private PositionInfo positionInfo;

		// Token: 0x04002CD9 RID: 11481
		private XsdBuilder.XsdEntry currentEntry;

		// Token: 0x04002CDA RID: 11482
		private XsdBuilder.XsdEntry nextEntry;

		// Token: 0x04002CDB RID: 11483
		private bool hasChild;

		// Token: 0x04002CDC RID: 11484
		private HWStack stateHistory = new HWStack(10);

		// Token: 0x04002CDD RID: 11485
		private Stack containerStack = new Stack();

		// Token: 0x04002CDE RID: 11486
		private XmlNameTable nameTable;

		// Token: 0x04002CDF RID: 11487
		private SchemaNames schemaNames;

		// Token: 0x04002CE0 RID: 11488
		private XmlNamespaceManager namespaceManager;

		// Token: 0x04002CE1 RID: 11489
		private bool canIncludeImport;

		// Token: 0x04002CE2 RID: 11490
		private XmlSchema schema;

		// Token: 0x04002CE3 RID: 11491
		private XmlSchemaObject xso;

		// Token: 0x04002CE4 RID: 11492
		private XmlSchemaElement element;

		// Token: 0x04002CE5 RID: 11493
		private XmlSchemaAny anyElement;

		// Token: 0x04002CE6 RID: 11494
		private XmlSchemaAttribute attribute;

		// Token: 0x04002CE7 RID: 11495
		private XmlSchemaAnyAttribute anyAttribute;

		// Token: 0x04002CE8 RID: 11496
		private XmlSchemaComplexType complexType;

		// Token: 0x04002CE9 RID: 11497
		private XmlSchemaSimpleType simpleType;

		// Token: 0x04002CEA RID: 11498
		private XmlSchemaComplexContent complexContent;

		// Token: 0x04002CEB RID: 11499
		private XmlSchemaComplexContentExtension complexContentExtension;

		// Token: 0x04002CEC RID: 11500
		private XmlSchemaComplexContentRestriction complexContentRestriction;

		// Token: 0x04002CED RID: 11501
		private XmlSchemaSimpleContent simpleContent;

		// Token: 0x04002CEE RID: 11502
		private XmlSchemaSimpleContentExtension simpleContentExtension;

		// Token: 0x04002CEF RID: 11503
		private XmlSchemaSimpleContentRestriction simpleContentRestriction;

		// Token: 0x04002CF0 RID: 11504
		private XmlSchemaSimpleTypeUnion simpleTypeUnion;

		// Token: 0x04002CF1 RID: 11505
		private XmlSchemaSimpleTypeList simpleTypeList;

		// Token: 0x04002CF2 RID: 11506
		private XmlSchemaSimpleTypeRestriction simpleTypeRestriction;

		// Token: 0x04002CF3 RID: 11507
		private XmlSchemaGroup group;

		// Token: 0x04002CF4 RID: 11508
		private XmlSchemaGroupRef groupRef;

		// Token: 0x04002CF5 RID: 11509
		private XmlSchemaAll all;

		// Token: 0x04002CF6 RID: 11510
		private XmlSchemaChoice choice;

		// Token: 0x04002CF7 RID: 11511
		private XmlSchemaSequence sequence;

		// Token: 0x04002CF8 RID: 11512
		private XmlSchemaParticle particle;

		// Token: 0x04002CF9 RID: 11513
		private XmlSchemaAttributeGroup attributeGroup;

		// Token: 0x04002CFA RID: 11514
		private XmlSchemaAttributeGroupRef attributeGroupRef;

		// Token: 0x04002CFB RID: 11515
		private XmlSchemaNotation notation;

		// Token: 0x04002CFC RID: 11516
		private XmlSchemaIdentityConstraint identityConstraint;

		// Token: 0x04002CFD RID: 11517
		private XmlSchemaXPath xpath;

		// Token: 0x04002CFE RID: 11518
		private XmlSchemaInclude include;

		// Token: 0x04002CFF RID: 11519
		private XmlSchemaImport import;

		// Token: 0x04002D00 RID: 11520
		private XmlSchemaAnnotation annotation;

		// Token: 0x04002D01 RID: 11521
		private XmlSchemaAppInfo appInfo;

		// Token: 0x04002D02 RID: 11522
		private XmlSchemaDocumentation documentation;

		// Token: 0x04002D03 RID: 11523
		private XmlSchemaFacet facet;

		// Token: 0x04002D04 RID: 11524
		private XmlNode[] markup;

		// Token: 0x04002D05 RID: 11525
		private XmlSchemaRedefine redefine;

		// Token: 0x04002D06 RID: 11526
		private ValidationEventHandler validationEventHandler;

		// Token: 0x04002D07 RID: 11527
		private ArrayList unhandledAttributes = new ArrayList();

		// Token: 0x04002D08 RID: 11528
		private Hashtable namespaces;

		// Token: 0x020005FE RID: 1534
		private enum State
		{
			// Token: 0x04002D0A RID: 11530
			Root,
			// Token: 0x04002D0B RID: 11531
			Schema,
			// Token: 0x04002D0C RID: 11532
			Annotation,
			// Token: 0x04002D0D RID: 11533
			Include,
			// Token: 0x04002D0E RID: 11534
			Import,
			// Token: 0x04002D0F RID: 11535
			Element,
			// Token: 0x04002D10 RID: 11536
			Attribute,
			// Token: 0x04002D11 RID: 11537
			AttributeGroup,
			// Token: 0x04002D12 RID: 11538
			AttributeGroupRef,
			// Token: 0x04002D13 RID: 11539
			AnyAttribute,
			// Token: 0x04002D14 RID: 11540
			Group,
			// Token: 0x04002D15 RID: 11541
			GroupRef,
			// Token: 0x04002D16 RID: 11542
			All,
			// Token: 0x04002D17 RID: 11543
			Choice,
			// Token: 0x04002D18 RID: 11544
			Sequence,
			// Token: 0x04002D19 RID: 11545
			Any,
			// Token: 0x04002D1A RID: 11546
			Notation,
			// Token: 0x04002D1B RID: 11547
			SimpleType,
			// Token: 0x04002D1C RID: 11548
			ComplexType,
			// Token: 0x04002D1D RID: 11549
			ComplexContent,
			// Token: 0x04002D1E RID: 11550
			ComplexContentRestriction,
			// Token: 0x04002D1F RID: 11551
			ComplexContentExtension,
			// Token: 0x04002D20 RID: 11552
			SimpleContent,
			// Token: 0x04002D21 RID: 11553
			SimpleContentExtension,
			// Token: 0x04002D22 RID: 11554
			SimpleContentRestriction,
			// Token: 0x04002D23 RID: 11555
			SimpleTypeUnion,
			// Token: 0x04002D24 RID: 11556
			SimpleTypeList,
			// Token: 0x04002D25 RID: 11557
			SimpleTypeRestriction,
			// Token: 0x04002D26 RID: 11558
			Unique,
			// Token: 0x04002D27 RID: 11559
			Key,
			// Token: 0x04002D28 RID: 11560
			KeyRef,
			// Token: 0x04002D29 RID: 11561
			Selector,
			// Token: 0x04002D2A RID: 11562
			Field,
			// Token: 0x04002D2B RID: 11563
			MinExclusive,
			// Token: 0x04002D2C RID: 11564
			MinInclusive,
			// Token: 0x04002D2D RID: 11565
			MaxExclusive,
			// Token: 0x04002D2E RID: 11566
			MaxInclusive,
			// Token: 0x04002D2F RID: 11567
			TotalDigits,
			// Token: 0x04002D30 RID: 11568
			FractionDigits,
			// Token: 0x04002D31 RID: 11569
			Length,
			// Token: 0x04002D32 RID: 11570
			MinLength,
			// Token: 0x04002D33 RID: 11571
			MaxLength,
			// Token: 0x04002D34 RID: 11572
			Enumeration,
			// Token: 0x04002D35 RID: 11573
			Pattern,
			// Token: 0x04002D36 RID: 11574
			WhiteSpace,
			// Token: 0x04002D37 RID: 11575
			AppInfo,
			// Token: 0x04002D38 RID: 11576
			Documentation,
			// Token: 0x04002D39 RID: 11577
			Redefine
		}

		// Token: 0x020005FF RID: 1535
		// (Invoke) Token: 0x06003F4B RID: 16203
		private delegate void XsdBuildFunction(XsdBuilder builder, string value);

		// Token: 0x02000600 RID: 1536
		// (Invoke) Token: 0x06003F4F RID: 16207
		private delegate void XsdInitFunction(XsdBuilder builder, string value);

		// Token: 0x02000601 RID: 1537
		// (Invoke) Token: 0x06003F53 RID: 16211
		private delegate void XsdEndChildFunction(XsdBuilder builder);

		// Token: 0x02000602 RID: 1538
		private sealed class XsdAttributeEntry
		{
			// Token: 0x06003F56 RID: 16214 RVA: 0x0015EEAA File Offset: 0x0015D0AA
			public XsdAttributeEntry(SchemaNames.Token a, XsdBuilder.XsdBuildFunction build)
			{
				this.Attribute = a;
				this.BuildFunc = build;
			}

			// Token: 0x04002D3A RID: 11578
			public SchemaNames.Token Attribute;

			// Token: 0x04002D3B RID: 11579
			public XsdBuilder.XsdBuildFunction BuildFunc;
		}

		// Token: 0x02000603 RID: 1539
		private sealed class XsdEntry
		{
			// Token: 0x06003F57 RID: 16215 RVA: 0x0015EEC0 File Offset: 0x0015D0C0
			public XsdEntry(SchemaNames.Token n, XsdBuilder.State state, XsdBuilder.State[] nextStates, XsdBuilder.XsdAttributeEntry[] attributes, XsdBuilder.XsdInitFunction init, XsdBuilder.XsdEndChildFunction end, bool parseContent)
			{
				this.Name = n;
				this.CurrentState = state;
				this.NextStates = nextStates;
				this.Attributes = attributes;
				this.InitFunc = init;
				this.EndChildFunc = end;
				this.ParseContent = parseContent;
			}

			// Token: 0x04002D3C RID: 11580
			public SchemaNames.Token Name;

			// Token: 0x04002D3D RID: 11581
			public XsdBuilder.State CurrentState;

			// Token: 0x04002D3E RID: 11582
			public XsdBuilder.State[] NextStates;

			// Token: 0x04002D3F RID: 11583
			public XsdBuilder.XsdAttributeEntry[] Attributes;

			// Token: 0x04002D40 RID: 11584
			public XsdBuilder.XsdInitFunction InitFunc;

			// Token: 0x04002D41 RID: 11585
			public XsdBuilder.XsdEndChildFunction EndChildFunc;

			// Token: 0x04002D42 RID: 11586
			public bool ParseContent;
		}

		// Token: 0x02000604 RID: 1540
		private class BuilderNamespaceManager : XmlNamespaceManager
		{
			// Token: 0x06003F58 RID: 16216 RVA: 0x0015EEFD File Offset: 0x0015D0FD
			public BuilderNamespaceManager(XmlNamespaceManager nsMgr, XmlReader reader)
			{
				this.nsMgr = nsMgr;
				this.reader = reader;
			}

			// Token: 0x06003F59 RID: 16217 RVA: 0x0015EF14 File Offset: 0x0015D114
			public override string LookupNamespace(string prefix)
			{
				string text = this.nsMgr.LookupNamespace(prefix);
				if (text == null)
				{
					text = this.reader.LookupNamespace(prefix);
				}
				return text;
			}

			// Token: 0x04002D43 RID: 11587
			private XmlNamespaceManager nsMgr;

			// Token: 0x04002D44 RID: 11588
			private XmlReader reader;
		}
	}
}

using System;
using System.Collections;
using System.ComponentModel;
using System.IO;
using System.Threading;
using System.Xml.Serialization;
using System.Xml.XmlConfiguration;

namespace System.Xml.Schema
{
	/// <summary>An in-memory representation of an XML Schema, as specified in the World Wide Web Consortium (W3C) XML Schema Part 1: Structures and XML Schema Part 2: Datatypes specifications.</summary>
	// Token: 0x02000591 RID: 1425
	[XmlRoot("schema", Namespace = "http://www.w3.org/2001/XMLSchema")]
	public class XmlSchema : XmlSchemaObject
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Schema.XmlSchema" /> class.</summary>
		// Token: 0x06003959 RID: 14681 RVA: 0x0014B234 File Offset: 0x00149434
		public XmlSchema()
		{
		}

		/// <summary>Reads an XML Schema from the supplied <see cref="T:System.IO.TextReader" />.</summary>
		/// <param name="reader">The <see langword="TextReader" /> containing the XML Schema to read. </param>
		/// <param name="validationEventHandler">The validation event handler that receives information about the XML Schema syntax errors. </param>
		/// <returns>The <see cref="T:System.Xml.Schema.XmlSchema" /> object representing the XML Schema.</returns>
		/// <exception cref="T:System.Xml.Schema.XmlSchemaException">An <see cref="T:System.Xml.Schema.XmlSchemaException" /> is raised if no <see cref="T:System.Xml.Schema.ValidationEventHandler" /> is specified.</exception>
		// Token: 0x0600395A RID: 14682 RVA: 0x0014B2C7 File Offset: 0x001494C7
		public static XmlSchema Read(TextReader reader, ValidationEventHandler validationEventHandler)
		{
			return XmlSchema.Read(new XmlTextReader(reader), validationEventHandler);
		}

		/// <summary>Reads an XML Schema  from the supplied stream.</summary>
		/// <param name="stream">The supplied data stream. </param>
		/// <param name="validationEventHandler">The validation event handler that receives information about XML Schema syntax errors. </param>
		/// <returns>The <see cref="T:System.Xml.Schema.XmlSchema" /> object representing the XML Schema.</returns>
		/// <exception cref="T:System.Xml.Schema.XmlSchemaException">An <see cref="T:System.Xml.Schema.XmlSchemaException" /> is raised if no <see cref="T:System.Xml.Schema.ValidationEventHandler" /> is specified.</exception>
		// Token: 0x0600395B RID: 14683 RVA: 0x0014B2D5 File Offset: 0x001494D5
		public static XmlSchema Read(Stream stream, ValidationEventHandler validationEventHandler)
		{
			return XmlSchema.Read(new XmlTextReader(stream), validationEventHandler);
		}

		/// <summary>Reads an XML Schema from the supplied <see cref="T:System.Xml.XmlReader" />.</summary>
		/// <param name="reader">The <see langword="XmlReader" /> containing the XML Schema to read. </param>
		/// <param name="validationEventHandler">The validation event handler that receives information about the XML Schema syntax errors. </param>
		/// <returns>The <see cref="T:System.Xml.Schema.XmlSchema" /> object representing the XML Schema.</returns>
		/// <exception cref="T:System.Xml.Schema.XmlSchemaException">An <see cref="T:System.Xml.Schema.XmlSchemaException" /> is raised if no <see cref="T:System.Xml.Schema.ValidationEventHandler" /> is specified.</exception>
		// Token: 0x0600395C RID: 14684 RVA: 0x0014B2E4 File Offset: 0x001494E4
		public static XmlSchema Read(XmlReader reader, ValidationEventHandler validationEventHandler)
		{
			XmlNameTable xmlNameTable = reader.NameTable;
			Parser parser = new Parser(SchemaType.XSD, xmlNameTable, new SchemaNames(xmlNameTable), validationEventHandler);
			try
			{
				parser.Parse(reader, null);
			}
			catch (XmlSchemaException ex)
			{
				if (validationEventHandler != null)
				{
					validationEventHandler(null, new ValidationEventArgs(ex));
					return null;
				}
				throw ex;
			}
			return parser.XmlSchema;
		}

		/// <summary>Writes the XML Schema to the supplied data stream.</summary>
		/// <param name="stream">The supplied data stream. </param>
		// Token: 0x0600395D RID: 14685 RVA: 0x0014B344 File Offset: 0x00149544
		public void Write(Stream stream)
		{
			this.Write(stream, null);
		}

		/// <summary>Writes the XML Schema to the supplied <see cref="T:System.IO.Stream" /> using the <see cref="T:System.Xml.XmlNamespaceManager" /> specified.</summary>
		/// <param name="stream">The supplied data stream. </param>
		/// <param name="namespaceManager">The <see cref="T:System.Xml.XmlNamespaceManager" />.</param>
		// Token: 0x0600395E RID: 14686 RVA: 0x0014B350 File Offset: 0x00149550
		public void Write(Stream stream, XmlNamespaceManager namespaceManager)
		{
			this.Write(new XmlTextWriter(stream, null)
			{
				Formatting = Formatting.Indented
			}, namespaceManager);
		}

		/// <summary>Writes the XML Schema to the supplied <see cref="T:System.IO.TextWriter" />.</summary>
		/// <param name="writer">The <see cref="T:System.IO.TextWriter" /> to write to.</param>
		// Token: 0x0600395F RID: 14687 RVA: 0x0014B374 File Offset: 0x00149574
		public void Write(TextWriter writer)
		{
			this.Write(writer, null);
		}

		/// <summary>Writes the XML Schema to the supplied <see cref="T:System.IO.TextWriter" />.</summary>
		/// <param name="writer">The <see cref="T:System.IO.TextWriter" /> to write to.</param>
		/// <param name="namespaceManager">The <see cref="T:System.Xml.XmlNamespaceManager" />. </param>
		// Token: 0x06003960 RID: 14688 RVA: 0x0014B380 File Offset: 0x00149580
		public void Write(TextWriter writer, XmlNamespaceManager namespaceManager)
		{
			this.Write(new XmlTextWriter(writer)
			{
				Formatting = Formatting.Indented
			}, namespaceManager);
		}

		/// <summary>Writes the XML Schema to the supplied <see cref="T:System.Xml.XmlWriter" />.</summary>
		/// <param name="writer">The <see cref="T:System.Xml.XmlWriter" /> to write to. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="writer" /> parameter is null.</exception>
		// Token: 0x06003961 RID: 14689 RVA: 0x0014B3A3 File Offset: 0x001495A3
		public void Write(XmlWriter writer)
		{
			this.Write(writer, null);
		}

		/// <summary>Writes the XML Schema to the supplied <see cref="T:System.Xml.XmlWriter" />.</summary>
		/// <param name="writer">The <see cref="T:System.Xml.XmlWriter" /> to write to.</param>
		/// <param name="namespaceManager">The <see cref="T:System.Xml.XmlNamespaceManager" />. </param>
		// Token: 0x06003962 RID: 14690 RVA: 0x0014B3B0 File Offset: 0x001495B0
		public void Write(XmlWriter writer, XmlNamespaceManager namespaceManager)
		{
			XmlSerializer xmlSerializer = new XmlSerializer(typeof(XmlSchema));
			XmlSerializerNamespaces xmlSerializerNamespaces;
			if (namespaceManager != null)
			{
				xmlSerializerNamespaces = new XmlSerializerNamespaces();
				bool flag = false;
				if (base.Namespaces != null)
				{
					flag = (base.Namespaces.Namespaces["xs"] != null || base.Namespaces.Namespaces.ContainsValue("http://www.w3.org/2001/XMLSchema"));
				}
				if (!flag && namespaceManager.LookupPrefix("http://www.w3.org/2001/XMLSchema") == null && namespaceManager.LookupNamespace("xs") == null)
				{
					xmlSerializerNamespaces.Add("xs", "http://www.w3.org/2001/XMLSchema");
				}
				using (IEnumerator enumerator = namespaceManager.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						object obj = enumerator.Current;
						string text = (string)obj;
						if (text != "xml" && text != "xmlns")
						{
							xmlSerializerNamespaces.Add(text, namespaceManager.LookupNamespace(text));
						}
					}
					goto IL_17B;
				}
			}
			if (base.Namespaces != null && base.Namespaces.Count > 0)
			{
				Hashtable namespaces = base.Namespaces.Namespaces;
				if (namespaces["xs"] == null && !namespaces.ContainsValue("http://www.w3.org/2001/XMLSchema"))
				{
					namespaces.Add("xs", "http://www.w3.org/2001/XMLSchema");
				}
				xmlSerializerNamespaces = base.Namespaces;
			}
			else
			{
				xmlSerializerNamespaces = new XmlSerializerNamespaces();
				xmlSerializerNamespaces.Add("xs", "http://www.w3.org/2001/XMLSchema");
				if (this.targetNs != null && this.targetNs.Length != 0)
				{
					xmlSerializerNamespaces.Add("tns", this.targetNs);
				}
			}
			IL_17B:
			xmlSerializer.Serialize(writer, this, xmlSerializerNamespaces);
		}

		/// <summary>Compiles the XML Schema Object Model (SOM) into schema information for validation. Used to check the syntactic and semantic structure of the programmatically built SOM. Semantic validation checking is performed during compilation.</summary>
		/// <param name="validationEventHandler">The validation event handler that receives information about XML Schema validation errors. </param>
		// Token: 0x06003963 RID: 14691 RVA: 0x0014B554 File Offset: 0x00149754
		[Obsolete("Use System.Xml.Schema.XmlSchemaSet for schema compilation and validation. http://go.microsoft.com/fwlink/?linkid=14202")]
		public void Compile(ValidationEventHandler validationEventHandler)
		{
			SchemaInfo schemaInfo = new SchemaInfo();
			schemaInfo.SchemaType = SchemaType.XSD;
			this.CompileSchema(null, XmlReaderSection.CreateDefaultResolver(), schemaInfo, null, validationEventHandler, this.NameTable, false);
		}

		/// <summary>Compiles the XML Schema Object Model (SOM) into schema information for validation. Used to check the syntactic and semantic structure of the programmatically built SOM. Semantic validation checking is performed during compilation.</summary>
		/// <param name="validationEventHandler">The validation event handler that receives information about the XML Schema validation errors. </param>
		/// <param name="resolver">The <see langword="XmlResolver" /> used to resolve namespaces referenced in <see langword="include" /> and <see langword="import" /> elements. </param>
		// Token: 0x06003964 RID: 14692 RVA: 0x0014B588 File Offset: 0x00149788
		[Obsolete("Use System.Xml.Schema.XmlSchemaSet for schema compilation and validation. http://go.microsoft.com/fwlink/?linkid=14202")]
		public void Compile(ValidationEventHandler validationEventHandler, XmlResolver resolver)
		{
			this.CompileSchema(null, resolver, new SchemaInfo
			{
				SchemaType = SchemaType.XSD
			}, null, validationEventHandler, this.NameTable, false);
		}

		// Token: 0x06003965 RID: 14693 RVA: 0x0014B5B8 File Offset: 0x001497B8
		internal bool CompileSchema(XmlSchemaCollection xsc, XmlResolver resolver, SchemaInfo schemaInfo, string ns, ValidationEventHandler validationEventHandler, XmlNameTable nameTable, bool CompileContentModel)
		{
			bool result;
			lock (this)
			{
				if (!new SchemaCollectionPreprocessor(nameTable, null, validationEventHandler)
				{
					XmlResolver = resolver
				}.Execute(this, ns, true, xsc))
				{
					result = false;
				}
				else
				{
					SchemaCollectionCompiler schemaCollectionCompiler = new SchemaCollectionCompiler(nameTable, validationEventHandler);
					this.isCompiled = schemaCollectionCompiler.Execute(this, schemaInfo, CompileContentModel);
					this.SetIsCompiled(this.isCompiled);
					result = this.isCompiled;
				}
			}
			return result;
		}

		// Token: 0x06003966 RID: 14694 RVA: 0x0014B63C File Offset: 0x0014983C
		internal void CompileSchemaInSet(XmlNameTable nameTable, ValidationEventHandler eventHandler, XmlSchemaCompilationSettings compilationSettings)
		{
			Compiler compiler = new Compiler(nameTable, eventHandler, null, compilationSettings);
			compiler.Prepare(this, true);
			this.isCompiledBySet = compiler.Compile();
		}

		/// <summary>Gets or sets the form for attributes declared in the target namespace of the schema.</summary>
		/// <returns>The <see cref="T:System.Xml.Schema.XmlSchemaForm" /> value that indicates if attributes from the target namespace are required to be qualified with the namespace prefix. The default is <see cref="F:System.Xml.Schema.XmlSchemaForm.None" />.</returns>
		// Token: 0x17000AA4 RID: 2724
		// (get) Token: 0x06003967 RID: 14695 RVA: 0x0014B667 File Offset: 0x00149867
		// (set) Token: 0x06003968 RID: 14696 RVA: 0x0014B66F File Offset: 0x0014986F
		[DefaultValue(XmlSchemaForm.None)]
		[XmlAttribute("attributeFormDefault")]
		public XmlSchemaForm AttributeFormDefault
		{
			get
			{
				return this.attributeFormDefault;
			}
			set
			{
				this.attributeFormDefault = value;
			}
		}

		/// <summary>Gets or sets the <see langword="blockDefault" /> attribute which sets the default value of the <see langword="block" /> attribute on element and complex types in the <see langword="targetNamespace" /> of the schema.</summary>
		/// <returns>An <see cref="T:System.Xml.Schema.XmlSchemaDerivationMethod" /> value representing the different methods for preventing derivation. The default value is <see langword="XmlSchemaDerivationMethod.None" />.</returns>
		// Token: 0x17000AA5 RID: 2725
		// (get) Token: 0x06003969 RID: 14697 RVA: 0x0014B678 File Offset: 0x00149878
		// (set) Token: 0x0600396A RID: 14698 RVA: 0x0014B680 File Offset: 0x00149880
		[XmlAttribute("blockDefault")]
		[DefaultValue(XmlSchemaDerivationMethod.None)]
		public XmlSchemaDerivationMethod BlockDefault
		{
			get
			{
				return this.blockDefault;
			}
			set
			{
				this.blockDefault = value;
			}
		}

		/// <summary>Gets or sets the <see langword="finalDefault" /> attribute which sets the default value of the <see langword="final" /> attribute on elements and complex types in the target namespace of the schema.</summary>
		/// <returns>An <see cref="T:System.Xml.Schema.XmlSchemaDerivationMethod" /> value representing the different methods for preventing derivation. The default value is <see langword="XmlSchemaDerivationMethod.None" />.</returns>
		// Token: 0x17000AA6 RID: 2726
		// (get) Token: 0x0600396B RID: 14699 RVA: 0x0014B689 File Offset: 0x00149889
		// (set) Token: 0x0600396C RID: 14700 RVA: 0x0014B691 File Offset: 0x00149891
		[XmlAttribute("finalDefault")]
		[DefaultValue(XmlSchemaDerivationMethod.None)]
		public XmlSchemaDerivationMethod FinalDefault
		{
			get
			{
				return this.finalDefault;
			}
			set
			{
				this.finalDefault = value;
			}
		}

		/// <summary>Gets or sets the form for elements declared in the target namespace of the schema.</summary>
		/// <returns>The <see cref="T:System.Xml.Schema.XmlSchemaForm" /> value that indicates if elements from the target namespace are required to be qualified with the namespace prefix. The default is <see cref="F:System.Xml.Schema.XmlSchemaForm.None" />.</returns>
		// Token: 0x17000AA7 RID: 2727
		// (get) Token: 0x0600396D RID: 14701 RVA: 0x0014B69A File Offset: 0x0014989A
		// (set) Token: 0x0600396E RID: 14702 RVA: 0x0014B6A2 File Offset: 0x001498A2
		[XmlAttribute("elementFormDefault")]
		[DefaultValue(XmlSchemaForm.None)]
		public XmlSchemaForm ElementFormDefault
		{
			get
			{
				return this.elementFormDefault;
			}
			set
			{
				this.elementFormDefault = value;
			}
		}

		/// <summary>Gets or sets the Uniform Resource Identifier (URI) of the schema target namespace.</summary>
		/// <returns>The schema target namespace.</returns>
		// Token: 0x17000AA8 RID: 2728
		// (get) Token: 0x0600396F RID: 14703 RVA: 0x0014B6AB File Offset: 0x001498AB
		// (set) Token: 0x06003970 RID: 14704 RVA: 0x0014B6B3 File Offset: 0x001498B3
		[XmlAttribute("targetNamespace", DataType = "anyURI")]
		public string TargetNamespace
		{
			get
			{
				return this.targetNs;
			}
			set
			{
				this.targetNs = value;
			}
		}

		/// <summary>Gets or sets the version of the schema.</summary>
		/// <returns>The version of the schema. The default value is <see langword="String.Empty" />.</returns>
		// Token: 0x17000AA9 RID: 2729
		// (get) Token: 0x06003971 RID: 14705 RVA: 0x0014B6BC File Offset: 0x001498BC
		// (set) Token: 0x06003972 RID: 14706 RVA: 0x0014B6C4 File Offset: 0x001498C4
		[XmlAttribute("version", DataType = "token")]
		public string Version
		{
			get
			{
				return this.version;
			}
			set
			{
				this.version = value;
			}
		}

		/// <summary>Gets the collection of included and imported schemas.</summary>
		/// <returns>An <see cref="T:System.Xml.Schema.XmlSchemaObjectCollection" /> of the included and imported schemas.</returns>
		// Token: 0x17000AAA RID: 2730
		// (get) Token: 0x06003973 RID: 14707 RVA: 0x0014B6CD File Offset: 0x001498CD
		[XmlElement("include", typeof(XmlSchemaInclude))]
		[XmlElement("import", typeof(XmlSchemaImport))]
		[XmlElement("redefine", typeof(XmlSchemaRedefine))]
		public XmlSchemaObjectCollection Includes
		{
			get
			{
				return this.includes;
			}
		}

		/// <summary>Gets the collection of schema elements in the schema and is used to add new element types at the <see langword="schema" /> element level.</summary>
		/// <returns>An <see cref="T:System.Xml.Schema.XmlSchemaObjectCollection" /> of schema elements in the schema.</returns>
		// Token: 0x17000AAB RID: 2731
		// (get) Token: 0x06003974 RID: 14708 RVA: 0x0014B6D5 File Offset: 0x001498D5
		[XmlElement("attributeGroup", typeof(XmlSchemaAttributeGroup))]
		[XmlElement("element", typeof(XmlSchemaElement))]
		[XmlElement("group", typeof(XmlSchemaGroup))]
		[XmlElement("attribute", typeof(XmlSchemaAttribute))]
		[XmlElement("simpleType", typeof(XmlSchemaSimpleType))]
		[XmlElement("notation", typeof(XmlSchemaNotation))]
		[XmlElement("complexType", typeof(XmlSchemaComplexType))]
		[XmlElement("annotation", typeof(XmlSchemaAnnotation))]
		public XmlSchemaObjectCollection Items
		{
			get
			{
				return this.items;
			}
		}

		/// <summary>Indicates if the schema has been compiled.</summary>
		/// <returns>
		///     <see langword="true" /> if schema has been compiled, otherwise, <see langword="false" />. The default value is <see langword="false" />.</returns>
		// Token: 0x17000AAC RID: 2732
		// (get) Token: 0x06003975 RID: 14709 RVA: 0x0014B6DD File Offset: 0x001498DD
		[XmlIgnore]
		public bool IsCompiled
		{
			get
			{
				return this.isCompiled || this.isCompiledBySet;
			}
		}

		// Token: 0x17000AAD RID: 2733
		// (get) Token: 0x06003976 RID: 14710 RVA: 0x0014B6EF File Offset: 0x001498EF
		// (set) Token: 0x06003977 RID: 14711 RVA: 0x0014B6F7 File Offset: 0x001498F7
		[XmlIgnore]
		internal bool IsCompiledBySet
		{
			get
			{
				return this.isCompiledBySet;
			}
			set
			{
				this.isCompiledBySet = value;
			}
		}

		// Token: 0x17000AAE RID: 2734
		// (get) Token: 0x06003978 RID: 14712 RVA: 0x0014B700 File Offset: 0x00149900
		// (set) Token: 0x06003979 RID: 14713 RVA: 0x0014B708 File Offset: 0x00149908
		[XmlIgnore]
		internal bool IsPreprocessed
		{
			get
			{
				return this.isPreprocessed;
			}
			set
			{
				this.isPreprocessed = value;
			}
		}

		// Token: 0x17000AAF RID: 2735
		// (get) Token: 0x0600397A RID: 14714 RVA: 0x0014B711 File Offset: 0x00149911
		// (set) Token: 0x0600397B RID: 14715 RVA: 0x0014B719 File Offset: 0x00149919
		[XmlIgnore]
		internal bool IsRedefined
		{
			get
			{
				return this.isRedefined;
			}
			set
			{
				this.isRedefined = value;
			}
		}

		/// <summary>Gets the post-schema-compilation value for all the attributes in the schema.</summary>
		/// <returns>An <see cref="T:System.Xml.Schema.XmlSchemaObjectTable" /> collection of all the attributes in the schema.</returns>
		// Token: 0x17000AB0 RID: 2736
		// (get) Token: 0x0600397C RID: 14716 RVA: 0x0014B722 File Offset: 0x00149922
		[XmlIgnore]
		public XmlSchemaObjectTable Attributes
		{
			get
			{
				if (this.attributes == null)
				{
					this.attributes = new XmlSchemaObjectTable();
				}
				return this.attributes;
			}
		}

		/// <summary>Gets the post-schema-compilation value of all the global attribute groups in the schema.</summary>
		/// <returns>An <see cref="T:System.Xml.Schema.XmlSchemaObjectTable" /> collection of all the global attribute groups in the schema.</returns>
		// Token: 0x17000AB1 RID: 2737
		// (get) Token: 0x0600397D RID: 14717 RVA: 0x0014B73D File Offset: 0x0014993D
		[XmlIgnore]
		public XmlSchemaObjectTable AttributeGroups
		{
			get
			{
				if (this.attributeGroups == null)
				{
					this.attributeGroups = new XmlSchemaObjectTable();
				}
				return this.attributeGroups;
			}
		}

		/// <summary>Gets the post-schema-compilation value of all schema types in the schema.</summary>
		/// <returns>An <see cref="T:System.Xml.Schema.XmlSchemaObjectCollection" /> of all schema types in the schema.</returns>
		// Token: 0x17000AB2 RID: 2738
		// (get) Token: 0x0600397E RID: 14718 RVA: 0x0014B758 File Offset: 0x00149958
		[XmlIgnore]
		public XmlSchemaObjectTable SchemaTypes
		{
			get
			{
				if (this.types == null)
				{
					this.types = new XmlSchemaObjectTable();
				}
				return this.types;
			}
		}

		/// <summary>Gets the post-schema-compilation value for all the elements in the schema.</summary>
		/// <returns>An <see cref="T:System.Xml.Schema.XmlSchemaObjectTable" /> collection of all the elements in the schema.</returns>
		// Token: 0x17000AB3 RID: 2739
		// (get) Token: 0x0600397F RID: 14719 RVA: 0x0014B773 File Offset: 0x00149973
		[XmlIgnore]
		public XmlSchemaObjectTable Elements
		{
			get
			{
				if (this.elements == null)
				{
					this.elements = new XmlSchemaObjectTable();
				}
				return this.elements;
			}
		}

		/// <summary>Gets or sets the string ID.</summary>
		/// <returns>The ID of the string. The default value is <see langword="String.Empty" />.</returns>
		// Token: 0x17000AB4 RID: 2740
		// (get) Token: 0x06003980 RID: 14720 RVA: 0x0014B78E File Offset: 0x0014998E
		// (set) Token: 0x06003981 RID: 14721 RVA: 0x0014B796 File Offset: 0x00149996
		[XmlAttribute("id", DataType = "ID")]
		public string Id
		{
			get
			{
				return this.id;
			}
			set
			{
				this.id = value;
			}
		}

		/// <summary>Gets and sets the qualified attributes which do not belong to the schema target namespace.</summary>
		/// <returns>An array of qualified <see cref="T:System.Xml.XmlAttribute" /> objects that do not belong to the schema target namespace.</returns>
		// Token: 0x17000AB5 RID: 2741
		// (get) Token: 0x06003982 RID: 14722 RVA: 0x0014B79F File Offset: 0x0014999F
		// (set) Token: 0x06003983 RID: 14723 RVA: 0x0014B7A7 File Offset: 0x001499A7
		[XmlAnyAttribute]
		public XmlAttribute[] UnhandledAttributes
		{
			get
			{
				return this.moreAttributes;
			}
			set
			{
				this.moreAttributes = value;
			}
		}

		/// <summary>Gets the post-schema-compilation value of all the groups in the schema.</summary>
		/// <returns>An <see cref="T:System.Xml.Schema.XmlSchemaObjectTable" /> collection of all the groups in the schema.</returns>
		// Token: 0x17000AB6 RID: 2742
		// (get) Token: 0x06003984 RID: 14724 RVA: 0x0014B7B0 File Offset: 0x001499B0
		[XmlIgnore]
		public XmlSchemaObjectTable Groups
		{
			get
			{
				return this.groups;
			}
		}

		/// <summary>Gets the post-schema-compilation value for all notations in the schema.</summary>
		/// <returns>An <see cref="T:System.Xml.Schema.XmlSchemaObjectTable" /> collection of all notations in the schema.</returns>
		// Token: 0x17000AB7 RID: 2743
		// (get) Token: 0x06003985 RID: 14725 RVA: 0x0014B7B8 File Offset: 0x001499B8
		[XmlIgnore]
		public XmlSchemaObjectTable Notations
		{
			get
			{
				return this.notations;
			}
		}

		// Token: 0x17000AB8 RID: 2744
		// (get) Token: 0x06003986 RID: 14726 RVA: 0x0014B7C0 File Offset: 0x001499C0
		[XmlIgnore]
		internal XmlSchemaObjectTable IdentityConstraints
		{
			get
			{
				return this.identityConstraints;
			}
		}

		// Token: 0x17000AB9 RID: 2745
		// (get) Token: 0x06003987 RID: 14727 RVA: 0x0014B7C8 File Offset: 0x001499C8
		// (set) Token: 0x06003988 RID: 14728 RVA: 0x0014B7D0 File Offset: 0x001499D0
		[XmlIgnore]
		internal Uri BaseUri
		{
			get
			{
				return this.baseUri;
			}
			set
			{
				this.baseUri = value;
			}
		}

		// Token: 0x17000ABA RID: 2746
		// (get) Token: 0x06003989 RID: 14729 RVA: 0x0014B7D9 File Offset: 0x001499D9
		[XmlIgnore]
		internal int SchemaId
		{
			get
			{
				if (this.schemaId == -1)
				{
					this.schemaId = Interlocked.Increment(ref XmlSchema.globalIdCounter);
				}
				return this.schemaId;
			}
		}

		// Token: 0x17000ABB RID: 2747
		// (get) Token: 0x0600398A RID: 14730 RVA: 0x0014B7FA File Offset: 0x001499FA
		// (set) Token: 0x0600398B RID: 14731 RVA: 0x0014B802 File Offset: 0x00149A02
		[XmlIgnore]
		internal bool IsChameleon
		{
			get
			{
				return this.isChameleon;
			}
			set
			{
				this.isChameleon = value;
			}
		}

		// Token: 0x17000ABC RID: 2748
		// (get) Token: 0x0600398C RID: 14732 RVA: 0x0014B80B File Offset: 0x00149A0B
		[XmlIgnore]
		internal Hashtable Ids
		{
			get
			{
				return this.ids;
			}
		}

		// Token: 0x17000ABD RID: 2749
		// (get) Token: 0x0600398D RID: 14733 RVA: 0x0014B813 File Offset: 0x00149A13
		[XmlIgnore]
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

		// Token: 0x17000ABE RID: 2750
		// (get) Token: 0x0600398E RID: 14734 RVA: 0x0014B82E File Offset: 0x00149A2E
		// (set) Token: 0x0600398F RID: 14735 RVA: 0x0014B836 File Offset: 0x00149A36
		[XmlIgnore]
		internal int ErrorCount
		{
			get
			{
				return this.errorCount;
			}
			set
			{
				this.errorCount = value;
			}
		}

		// Token: 0x06003990 RID: 14736 RVA: 0x0014B840 File Offset: 0x00149A40
		internal new XmlSchema Clone()
		{
			XmlSchema xmlSchema = new XmlSchema();
			xmlSchema.attributeFormDefault = this.attributeFormDefault;
			xmlSchema.elementFormDefault = this.elementFormDefault;
			xmlSchema.blockDefault = this.blockDefault;
			xmlSchema.finalDefault = this.finalDefault;
			xmlSchema.targetNs = this.targetNs;
			xmlSchema.version = this.version;
			xmlSchema.includes = this.includes;
			xmlSchema.Namespaces = base.Namespaces;
			xmlSchema.items = this.items;
			xmlSchema.BaseUri = this.BaseUri;
			SchemaCollectionCompiler.Cleanup(xmlSchema);
			return xmlSchema;
		}

		// Token: 0x06003991 RID: 14737 RVA: 0x0014B8D0 File Offset: 0x00149AD0
		internal XmlSchema DeepClone()
		{
			XmlSchema xmlSchema = new XmlSchema();
			xmlSchema.attributeFormDefault = this.attributeFormDefault;
			xmlSchema.elementFormDefault = this.elementFormDefault;
			xmlSchema.blockDefault = this.blockDefault;
			xmlSchema.finalDefault = this.finalDefault;
			xmlSchema.targetNs = this.targetNs;
			xmlSchema.version = this.version;
			xmlSchema.isPreprocessed = this.isPreprocessed;
			for (int i = 0; i < this.items.Count; i++)
			{
				XmlSchemaComplexType xmlSchemaComplexType;
				XmlSchemaObject item;
				XmlSchemaElement xmlSchemaElement;
				XmlSchemaGroup xmlSchemaGroup;
				if ((xmlSchemaComplexType = (this.items[i] as XmlSchemaComplexType)) != null)
				{
					item = xmlSchemaComplexType.Clone(this);
				}
				else if ((xmlSchemaElement = (this.items[i] as XmlSchemaElement)) != null)
				{
					item = xmlSchemaElement.Clone(this);
				}
				else if ((xmlSchemaGroup = (this.items[i] as XmlSchemaGroup)) != null)
				{
					item = xmlSchemaGroup.Clone(this);
				}
				else
				{
					item = this.items[i].Clone();
				}
				xmlSchema.Items.Add(item);
			}
			for (int j = 0; j < this.includes.Count; j++)
			{
				XmlSchemaExternal item2 = (XmlSchemaExternal)this.includes[j].Clone();
				xmlSchema.Includes.Add(item2);
			}
			xmlSchema.Namespaces = base.Namespaces;
			xmlSchema.BaseUri = this.BaseUri;
			return xmlSchema;
		}

		// Token: 0x17000ABF RID: 2751
		// (get) Token: 0x06003992 RID: 14738 RVA: 0x0014BA2D File Offset: 0x00149C2D
		// (set) Token: 0x06003993 RID: 14739 RVA: 0x0014BA35 File Offset: 0x00149C35
		[XmlIgnore]
		internal override string IdAttribute
		{
			get
			{
				return this.Id;
			}
			set
			{
				this.Id = value;
			}
		}

		// Token: 0x06003994 RID: 14740 RVA: 0x0014BA3E File Offset: 0x00149C3E
		internal void SetIsCompiled(bool isCompiled)
		{
			this.isCompiled = isCompiled;
		}

		// Token: 0x06003995 RID: 14741 RVA: 0x0014B7A7 File Offset: 0x001499A7
		internal override void SetUnhandledAttributes(XmlAttribute[] moreAttributes)
		{
			this.moreAttributes = moreAttributes;
		}

		// Token: 0x06003996 RID: 14742 RVA: 0x0014BA47 File Offset: 0x00149C47
		internal override void AddAnnotation(XmlSchemaAnnotation annotation)
		{
			this.items.Add(annotation);
		}

		// Token: 0x17000AC0 RID: 2752
		// (get) Token: 0x06003997 RID: 14743 RVA: 0x0014BA56 File Offset: 0x00149C56
		internal XmlNameTable NameTable
		{
			get
			{
				if (this.nameTable == null)
				{
					this.nameTable = new NameTable();
				}
				return this.nameTable;
			}
		}

		// Token: 0x17000AC1 RID: 2753
		// (get) Token: 0x06003998 RID: 14744 RVA: 0x0014BA71 File Offset: 0x00149C71
		internal ArrayList ImportedSchemas
		{
			get
			{
				if (this.importedSchemas == null)
				{
					this.importedSchemas = new ArrayList();
				}
				return this.importedSchemas;
			}
		}

		// Token: 0x17000AC2 RID: 2754
		// (get) Token: 0x06003999 RID: 14745 RVA: 0x0014BA8C File Offset: 0x00149C8C
		internal ArrayList ImportedNamespaces
		{
			get
			{
				if (this.importedNamespaces == null)
				{
					this.importedNamespaces = new ArrayList();
				}
				return this.importedNamespaces;
			}
		}

		// Token: 0x0600399A RID: 14746 RVA: 0x0014BAA8 File Offset: 0x00149CA8
		internal void GetExternalSchemasList(IList extList, XmlSchema schema)
		{
			if (extList.Contains(schema))
			{
				return;
			}
			extList.Add(schema);
			for (int i = 0; i < schema.Includes.Count; i++)
			{
				XmlSchemaExternal xmlSchemaExternal = (XmlSchemaExternal)schema.Includes[i];
				if (xmlSchemaExternal.Schema != null)
				{
					this.GetExternalSchemasList(extList, xmlSchemaExternal.Schema);
				}
			}
		}

		// Token: 0x0600399B RID: 14747 RVA: 0x0014BB04 File Offset: 0x00149D04
		// Note: this type is marked as 'beforefieldinit'.
		static XmlSchema()
		{
		}

		/// <summary>The XML schema namespace. This field is constant.</summary>
		// Token: 0x04002AA6 RID: 10918
		public const string Namespace = "http://www.w3.org/2001/XMLSchema";

		/// <summary>The XML schema instance namespace. This field is constant. </summary>
		// Token: 0x04002AA7 RID: 10919
		public const string InstanceNamespace = "http://www.w3.org/2001/XMLSchema-instance";

		// Token: 0x04002AA8 RID: 10920
		private XmlSchemaForm attributeFormDefault;

		// Token: 0x04002AA9 RID: 10921
		private XmlSchemaForm elementFormDefault;

		// Token: 0x04002AAA RID: 10922
		private XmlSchemaDerivationMethod blockDefault = XmlSchemaDerivationMethod.None;

		// Token: 0x04002AAB RID: 10923
		private XmlSchemaDerivationMethod finalDefault = XmlSchemaDerivationMethod.None;

		// Token: 0x04002AAC RID: 10924
		private string targetNs;

		// Token: 0x04002AAD RID: 10925
		private string version;

		// Token: 0x04002AAE RID: 10926
		private XmlSchemaObjectCollection includes = new XmlSchemaObjectCollection();

		// Token: 0x04002AAF RID: 10927
		private XmlSchemaObjectCollection items = new XmlSchemaObjectCollection();

		// Token: 0x04002AB0 RID: 10928
		private string id;

		// Token: 0x04002AB1 RID: 10929
		private XmlAttribute[] moreAttributes;

		// Token: 0x04002AB2 RID: 10930
		private bool isCompiled;

		// Token: 0x04002AB3 RID: 10931
		private bool isCompiledBySet;

		// Token: 0x04002AB4 RID: 10932
		private bool isPreprocessed;

		// Token: 0x04002AB5 RID: 10933
		private bool isRedefined;

		// Token: 0x04002AB6 RID: 10934
		private int errorCount;

		// Token: 0x04002AB7 RID: 10935
		private XmlSchemaObjectTable attributes;

		// Token: 0x04002AB8 RID: 10936
		private XmlSchemaObjectTable attributeGroups = new XmlSchemaObjectTable();

		// Token: 0x04002AB9 RID: 10937
		private XmlSchemaObjectTable elements = new XmlSchemaObjectTable();

		// Token: 0x04002ABA RID: 10938
		private XmlSchemaObjectTable types = new XmlSchemaObjectTable();

		// Token: 0x04002ABB RID: 10939
		private XmlSchemaObjectTable groups = new XmlSchemaObjectTable();

		// Token: 0x04002ABC RID: 10940
		private XmlSchemaObjectTable notations = new XmlSchemaObjectTable();

		// Token: 0x04002ABD RID: 10941
		private XmlSchemaObjectTable identityConstraints = new XmlSchemaObjectTable();

		// Token: 0x04002ABE RID: 10942
		private static int globalIdCounter = -1;

		// Token: 0x04002ABF RID: 10943
		private ArrayList importedSchemas;

		// Token: 0x04002AC0 RID: 10944
		private ArrayList importedNamespaces;

		// Token: 0x04002AC1 RID: 10945
		private int schemaId = -1;

		// Token: 0x04002AC2 RID: 10946
		private Uri baseUri;

		// Token: 0x04002AC3 RID: 10947
		private bool isChameleon;

		// Token: 0x04002AC4 RID: 10948
		private Hashtable ids = new Hashtable();

		// Token: 0x04002AC5 RID: 10949
		private XmlDocument document;

		// Token: 0x04002AC6 RID: 10950
		private XmlNameTable nameTable;
	}
}

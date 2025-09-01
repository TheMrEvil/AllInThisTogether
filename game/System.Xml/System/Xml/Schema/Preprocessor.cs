using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace System.Xml.Schema
{
	// Token: 0x0200056A RID: 1386
	internal sealed class Preprocessor : BaseProcessor
	{
		// Token: 0x060036F4 RID: 14068 RVA: 0x0013501C File Offset: 0x0013321C
		public Preprocessor(XmlNameTable nameTable, SchemaNames schemaNames, ValidationEventHandler eventHandler) : this(nameTable, schemaNames, eventHandler, new XmlSchemaCompilationSettings())
		{
		}

		// Token: 0x060036F5 RID: 14069 RVA: 0x0013502C File Offset: 0x0013322C
		public Preprocessor(XmlNameTable nameTable, SchemaNames schemaNames, ValidationEventHandler eventHandler, XmlSchemaCompilationSettings compilationSettings) : base(nameTable, schemaNames, eventHandler, compilationSettings)
		{
			this.referenceNamespaces = new Hashtable();
			this.processedExternals = new Hashtable();
			this.lockList = new SortedList();
		}

		// Token: 0x060036F6 RID: 14070 RVA: 0x0013505C File Offset: 0x0013325C
		public bool Execute(XmlSchema schema, string targetNamespace, bool loadExternals)
		{
			this.rootSchema = schema;
			this.Xmlns = base.NameTable.Add("xmlns");
			this.NsXsi = base.NameTable.Add("http://www.w3.org/2001/XMLSchema-instance");
			this.rootSchema.ImportedSchemas.Clear();
			this.rootSchema.ImportedNamespaces.Clear();
			if (this.rootSchema.BaseUri != null && this.schemaLocations[this.rootSchema.BaseUri] == null)
			{
				this.schemaLocations.Add(this.rootSchema.BaseUri, this.rootSchema);
			}
			if (this.rootSchema.TargetNamespace != null)
			{
				if (targetNamespace == null)
				{
					targetNamespace = this.rootSchema.TargetNamespace;
				}
				else if (targetNamespace != this.rootSchema.TargetNamespace)
				{
					base.SendValidationEvent("The targetNamespace parameter '{0}' should be the same value as the targetNamespace '{1}' of the schema.", targetNamespace, this.rootSchema.TargetNamespace, this.rootSchema);
				}
			}
			else if (targetNamespace != null && targetNamespace.Length != 0)
			{
				this.rootSchema = this.GetChameleonSchema(targetNamespace, this.rootSchema);
			}
			if (loadExternals && this.xmlResolver != null)
			{
				this.LoadExternals(this.rootSchema);
			}
			this.BuildSchemaList(this.rootSchema);
			int i = 0;
			try
			{
				for (i = 0; i < this.lockList.Count; i++)
				{
					XmlSchema xmlSchema = (XmlSchema)this.lockList.GetByIndex(i);
					Monitor.Enter(xmlSchema);
					xmlSchema.IsProcessing = false;
				}
				this.rootSchemaForRedefine = this.rootSchema;
				this.Preprocess(this.rootSchema, targetNamespace, this.rootSchema.ImportedSchemas);
				if (this.redefinedList != null)
				{
					for (int j = 0; j < this.redefinedList.Count; j++)
					{
						this.PreprocessRedefine((RedefineEntry)this.redefinedList[j]);
					}
				}
			}
			finally
			{
				if (i == this.lockList.Count)
				{
					i--;
				}
				while (i >= 0)
				{
					XmlSchema xmlSchema = (XmlSchema)this.lockList.GetByIndex(i);
					xmlSchema.IsProcessing = false;
					if (xmlSchema == Preprocessor.GetBuildInSchema())
					{
						Monitor.Exit(xmlSchema);
					}
					else
					{
						xmlSchema.IsCompiledBySet = false;
						xmlSchema.IsPreprocessed = !base.HasErrors;
						Monitor.Exit(xmlSchema);
					}
					i--;
				}
			}
			this.rootSchema.IsPreprocessed = !base.HasErrors;
			return !base.HasErrors;
		}

		// Token: 0x060036F7 RID: 14071 RVA: 0x001352B8 File Offset: 0x001334B8
		private void Cleanup(XmlSchema schema)
		{
			if (schema == Preprocessor.GetBuildInSchema())
			{
				return;
			}
			schema.Attributes.Clear();
			schema.AttributeGroups.Clear();
			schema.SchemaTypes.Clear();
			schema.Elements.Clear();
			schema.Groups.Clear();
			schema.Notations.Clear();
			schema.Ids.Clear();
			schema.IdentityConstraints.Clear();
			schema.IsRedefined = false;
			schema.IsCompiledBySet = false;
		}

		// Token: 0x060036F8 RID: 14072 RVA: 0x00135334 File Offset: 0x00133534
		private void CleanupRedefine(XmlSchemaExternal include)
		{
			XmlSchemaRedefine xmlSchemaRedefine = include as XmlSchemaRedefine;
			xmlSchemaRedefine.AttributeGroups.Clear();
			xmlSchemaRedefine.Groups.Clear();
			xmlSchemaRedefine.SchemaTypes.Clear();
		}

		// Token: 0x17000A2D RID: 2605
		// (set) Token: 0x060036F9 RID: 14073 RVA: 0x0013535C File Offset: 0x0013355C
		internal XmlResolver XmlResolver
		{
			set
			{
				this.xmlResolver = value;
			}
		}

		// Token: 0x17000A2E RID: 2606
		// (get) Token: 0x060036FA RID: 14074 RVA: 0x00135365 File Offset: 0x00133565
		// (set) Token: 0x060036FB RID: 14075 RVA: 0x0013538C File Offset: 0x0013358C
		internal XmlReaderSettings ReaderSettings
		{
			get
			{
				if (this.readerSettings == null)
				{
					this.readerSettings = new XmlReaderSettings();
					this.readerSettings.DtdProcessing = DtdProcessing.Prohibit;
				}
				return this.readerSettings;
			}
			set
			{
				this.readerSettings = value;
			}
		}

		// Token: 0x17000A2F RID: 2607
		// (set) Token: 0x060036FC RID: 14076 RVA: 0x00135395 File Offset: 0x00133595
		internal Hashtable SchemaLocations
		{
			set
			{
				this.schemaLocations = value;
			}
		}

		// Token: 0x17000A30 RID: 2608
		// (set) Token: 0x060036FD RID: 14077 RVA: 0x0013539E File Offset: 0x0013359E
		internal Hashtable ChameleonSchemas
		{
			set
			{
				this.chameleonSchemas = value;
			}
		}

		// Token: 0x17000A31 RID: 2609
		// (get) Token: 0x060036FE RID: 14078 RVA: 0x001353A7 File Offset: 0x001335A7
		internal XmlSchema RootSchema
		{
			get
			{
				return this.rootSchema;
			}
		}

		// Token: 0x060036FF RID: 14079 RVA: 0x001353B0 File Offset: 0x001335B0
		private void BuildSchemaList(XmlSchema schema)
		{
			if (this.lockList.Contains(schema.SchemaId))
			{
				return;
			}
			this.lockList.Add(schema.SchemaId, schema);
			for (int i = 0; i < schema.Includes.Count; i++)
			{
				XmlSchemaExternal xmlSchemaExternal = (XmlSchemaExternal)schema.Includes[i];
				if (xmlSchemaExternal.Schema != null)
				{
					this.BuildSchemaList(xmlSchemaExternal.Schema);
				}
			}
		}

		// Token: 0x06003700 RID: 14080 RVA: 0x0013542C File Offset: 0x0013362C
		private void LoadExternals(XmlSchema schema)
		{
			if (schema.IsProcessing)
			{
				return;
			}
			schema.IsProcessing = true;
			for (int i = 0; i < schema.Includes.Count; i++)
			{
				XmlSchemaExternal xmlSchemaExternal = (XmlSchemaExternal)schema.Includes[i];
				XmlSchema xmlSchema = xmlSchemaExternal.Schema;
				if (xmlSchema != null)
				{
					Uri baseUri = xmlSchema.BaseUri;
					if (baseUri != null && this.schemaLocations[baseUri] == null)
					{
						this.schemaLocations.Add(baseUri, xmlSchema);
					}
					this.LoadExternals(xmlSchema);
				}
				else
				{
					string schemaLocation = xmlSchemaExternal.SchemaLocation;
					Uri uri = null;
					Exception innerException = null;
					if (schemaLocation != null)
					{
						try
						{
							uri = this.ResolveSchemaLocationUri(schema, schemaLocation);
						}
						catch (Exception ex)
						{
							uri = null;
							innerException = ex;
						}
					}
					if (xmlSchemaExternal.Compositor == Compositor.Import)
					{
						XmlSchemaImport xmlSchemaImport = xmlSchemaExternal as XmlSchemaImport;
						string text = (xmlSchemaImport.Namespace != null) ? xmlSchemaImport.Namespace : string.Empty;
						if (!schema.ImportedNamespaces.Contains(text))
						{
							schema.ImportedNamespaces.Add(text);
						}
						if (text == "http://www.w3.org/XML/1998/namespace" && uri == null)
						{
							xmlSchemaExternal.Schema = Preprocessor.GetBuildInSchema();
							goto IL_38A;
						}
					}
					if (uri == null)
					{
						if (schemaLocation != null)
						{
							base.SendValidationEvent(new XmlSchemaException("Cannot resolve the 'schemaLocation' attribute.", null, innerException, xmlSchemaExternal.SourceUri, xmlSchemaExternal.LineNumber, xmlSchemaExternal.LinePosition, xmlSchemaExternal), XmlSeverityType.Warning);
						}
					}
					else if (this.schemaLocations[uri] == null)
					{
						object obj = null;
						try
						{
							obj = this.GetSchemaEntity(uri);
						}
						catch (Exception innerException)
						{
							obj = null;
						}
						if (obj != null)
						{
							xmlSchemaExternal.BaseUri = uri;
							Type type = obj.GetType();
							if (typeof(XmlSchema).IsAssignableFrom(type))
							{
								xmlSchemaExternal.Schema = (XmlSchema)obj;
								this.schemaLocations.Add(uri, xmlSchemaExternal.Schema);
								this.LoadExternals(xmlSchemaExternal.Schema);
								goto IL_38A;
							}
							XmlReader xmlReader = null;
							if (type.IsSubclassOf(typeof(Stream)))
							{
								this.readerSettings.CloseInput = true;
								this.readerSettings.XmlResolver = this.xmlResolver;
								xmlReader = XmlReader.Create((Stream)obj, this.readerSettings, uri.ToString());
							}
							else if (type.IsSubclassOf(typeof(XmlReader)))
							{
								xmlReader = (XmlReader)obj;
							}
							else if (type.IsSubclassOf(typeof(TextReader)))
							{
								this.readerSettings.CloseInput = true;
								this.readerSettings.XmlResolver = this.xmlResolver;
								xmlReader = XmlReader.Create((TextReader)obj, this.readerSettings, uri.ToString());
							}
							if (xmlReader == null)
							{
								base.SendValidationEvent("Cannot resolve the 'schemaLocation' attribute.", xmlSchemaExternal, XmlSeverityType.Warning);
								goto IL_38A;
							}
							try
							{
								Parser parser = new Parser(SchemaType.XSD, base.NameTable, base.SchemaNames, base.EventHandler);
								parser.Parse(xmlReader, null);
								while (xmlReader.Read())
								{
								}
								xmlSchema = parser.XmlSchema;
								xmlSchemaExternal.Schema = xmlSchema;
								this.schemaLocations.Add(uri, xmlSchema);
								this.LoadExternals(xmlSchema);
								goto IL_38A;
							}
							catch (XmlSchemaException ex2)
							{
								base.SendValidationEvent("Cannot load the schema from the location '{0}' - {1}", schemaLocation, ex2.Message, ex2.SourceUri, ex2.LineNumber, ex2.LinePosition);
								goto IL_38A;
							}
							catch (Exception innerException2)
							{
								base.SendValidationEvent(new XmlSchemaException("Cannot resolve the 'schemaLocation' attribute.", null, innerException2, xmlSchemaExternal.SourceUri, xmlSchemaExternal.LineNumber, xmlSchemaExternal.LinePosition, xmlSchemaExternal), XmlSeverityType.Warning);
								goto IL_38A;
							}
							finally
							{
								xmlReader.Close();
							}
						}
						base.SendValidationEvent(new XmlSchemaException("Cannot resolve the 'schemaLocation' attribute.", null, innerException, xmlSchemaExternal.SourceUri, xmlSchemaExternal.LineNumber, xmlSchemaExternal.LinePosition, xmlSchemaExternal), XmlSeverityType.Warning);
					}
					else
					{
						xmlSchemaExternal.Schema = (XmlSchema)this.schemaLocations[uri];
					}
				}
				IL_38A:;
			}
		}

		// Token: 0x06003701 RID: 14081 RVA: 0x00135818 File Offset: 0x00133A18
		internal static XmlSchema GetBuildInSchema()
		{
			if (Preprocessor.builtInSchemaForXmlNS == null)
			{
				XmlSchema xmlSchema = new XmlSchema();
				xmlSchema.TargetNamespace = "http://www.w3.org/XML/1998/namespace";
				xmlSchema.Namespaces.Add("xml", "http://www.w3.org/XML/1998/namespace");
				XmlSchemaAttribute xmlSchemaAttribute = new XmlSchemaAttribute();
				xmlSchemaAttribute.Name = "lang";
				xmlSchemaAttribute.SchemaTypeName = new XmlQualifiedName("language", "http://www.w3.org/2001/XMLSchema");
				xmlSchema.Items.Add(xmlSchemaAttribute);
				XmlSchemaAttribute xmlSchemaAttribute2 = new XmlSchemaAttribute();
				xmlSchemaAttribute2.Name = "base";
				xmlSchemaAttribute2.SchemaTypeName = new XmlQualifiedName("anyURI", "http://www.w3.org/2001/XMLSchema");
				xmlSchema.Items.Add(xmlSchemaAttribute2);
				XmlSchemaAttribute xmlSchemaAttribute3 = new XmlSchemaAttribute();
				xmlSchemaAttribute3.Name = "space";
				XmlSchemaSimpleType xmlSchemaSimpleType = new XmlSchemaSimpleType();
				XmlSchemaSimpleTypeRestriction xmlSchemaSimpleTypeRestriction = new XmlSchemaSimpleTypeRestriction();
				xmlSchemaSimpleTypeRestriction.BaseTypeName = new XmlQualifiedName("NCName", "http://www.w3.org/2001/XMLSchema");
				XmlSchemaEnumerationFacet xmlSchemaEnumerationFacet = new XmlSchemaEnumerationFacet();
				xmlSchemaEnumerationFacet.Value = "default";
				xmlSchemaSimpleTypeRestriction.Facets.Add(xmlSchemaEnumerationFacet);
				XmlSchemaEnumerationFacet xmlSchemaEnumerationFacet2 = new XmlSchemaEnumerationFacet();
				xmlSchemaEnumerationFacet2.Value = "preserve";
				xmlSchemaSimpleTypeRestriction.Facets.Add(xmlSchemaEnumerationFacet2);
				xmlSchemaSimpleType.Content = xmlSchemaSimpleTypeRestriction;
				xmlSchemaAttribute3.SchemaType = xmlSchemaSimpleType;
				xmlSchemaAttribute3.DefaultValue = "preserve";
				xmlSchema.Items.Add(xmlSchemaAttribute3);
				XmlSchemaAttributeGroup xmlSchemaAttributeGroup = new XmlSchemaAttributeGroup();
				xmlSchemaAttributeGroup.Name = "specialAttrs";
				XmlSchemaAttribute xmlSchemaAttribute4 = new XmlSchemaAttribute();
				xmlSchemaAttribute4.RefName = new XmlQualifiedName("lang", "http://www.w3.org/XML/1998/namespace");
				xmlSchemaAttributeGroup.Attributes.Add(xmlSchemaAttribute4);
				XmlSchemaAttribute xmlSchemaAttribute5 = new XmlSchemaAttribute();
				xmlSchemaAttribute5.RefName = new XmlQualifiedName("space", "http://www.w3.org/XML/1998/namespace");
				xmlSchemaAttributeGroup.Attributes.Add(xmlSchemaAttribute5);
				XmlSchemaAttribute xmlSchemaAttribute6 = new XmlSchemaAttribute();
				xmlSchemaAttribute6.RefName = new XmlQualifiedName("base", "http://www.w3.org/XML/1998/namespace");
				xmlSchemaAttributeGroup.Attributes.Add(xmlSchemaAttribute6);
				xmlSchema.Items.Add(xmlSchemaAttributeGroup);
				xmlSchema.IsPreprocessed = true;
				xmlSchema.CompileSchemaInSet(new NameTable(), null, null);
				Interlocked.CompareExchange<XmlSchema>(ref Preprocessor.builtInSchemaForXmlNS, xmlSchema, null);
			}
			return Preprocessor.builtInSchemaForXmlNS;
		}

		// Token: 0x06003702 RID: 14082 RVA: 0x00135A28 File Offset: 0x00133C28
		private void BuildRefNamespaces(XmlSchema schema)
		{
			this.referenceNamespaces.Clear();
			this.referenceNamespaces.Add("http://www.w3.org/2001/XMLSchema", "http://www.w3.org/2001/XMLSchema");
			for (int i = 0; i < schema.Includes.Count; i++)
			{
				XmlSchemaExternal xmlSchemaExternal = (XmlSchemaExternal)schema.Includes[i];
				if (xmlSchemaExternal is XmlSchemaImport)
				{
					string text = (xmlSchemaExternal as XmlSchemaImport).Namespace;
					if (text == null)
					{
						text = string.Empty;
					}
					if (this.referenceNamespaces[text] == null)
					{
						this.referenceNamespaces.Add(text, text);
					}
				}
			}
			string empty = schema.TargetNamespace;
			if (empty == null)
			{
				empty = string.Empty;
			}
			if (this.referenceNamespaces[empty] == null)
			{
				this.referenceNamespaces.Add(empty, empty);
			}
		}

		// Token: 0x06003703 RID: 14083 RVA: 0x00135AE0 File Offset: 0x00133CE0
		private void ParseUri(string uri, string code, XmlSchemaObject sourceSchemaObject)
		{
			try
			{
				XmlConvert.ToUri(uri);
			}
			catch (FormatException innerException)
			{
				base.SendValidationEvent(code, new string[]
				{
					uri
				}, innerException, sourceSchemaObject);
			}
		}

		// Token: 0x06003704 RID: 14084 RVA: 0x00135B1C File Offset: 0x00133D1C
		private void Preprocess(XmlSchema schema, string targetNamespace, ArrayList imports)
		{
			if (schema.IsProcessing)
			{
				return;
			}
			schema.IsProcessing = true;
			string text = schema.TargetNamespace;
			if (text != null)
			{
				text = (schema.TargetNamespace = base.NameTable.Add(text));
				if (text.Length == 0)
				{
					base.SendValidationEvent("The targetNamespace attribute cannot have empty string as its value.", schema);
				}
				else
				{
					this.ParseUri(text, "The Namespace '{0}' is an invalid URI.", schema);
				}
			}
			if (schema.Version != null)
			{
				XmlSchemaDatatype datatype = DatatypeImplementation.GetSimpleTypeFromTypeCode(XmlTypeCode.Token).Datatype;
				object obj;
				Exception ex = datatype.TryParseValue(schema.Version, null, null, out obj);
				if (ex != null)
				{
					base.SendValidationEvent("The '{0}' attribute is invalid - The value '{1}' is invalid according to its datatype '{2}' - {3}", new string[]
					{
						"version",
						schema.Version,
						datatype.TypeCodeString,
						ex.Message
					}, ex, schema);
				}
				else
				{
					schema.Version = (string)obj;
				}
			}
			this.Cleanup(schema);
			int i = 0;
			while (i < schema.Includes.Count)
			{
				XmlSchemaExternal xmlSchemaExternal = (XmlSchemaExternal)schema.Includes[i];
				XmlSchema xmlSchema = xmlSchemaExternal.Schema;
				this.SetParent(xmlSchemaExternal, schema);
				this.PreprocessAnnotation(xmlSchemaExternal);
				string schemaLocation = xmlSchemaExternal.SchemaLocation;
				if (schemaLocation != null)
				{
					this.ParseUri(schemaLocation, "The SchemaLocation '{0}' is an invalid URI.", xmlSchemaExternal);
				}
				else if ((xmlSchemaExternal.Compositor == Compositor.Include || xmlSchemaExternal.Compositor == Compositor.Redefine) && xmlSchema == null)
				{
					base.SendValidationEvent("The required attribute '{0}' is missing.", "schemaLocation", xmlSchemaExternal);
				}
				switch (xmlSchemaExternal.Compositor)
				{
				case Compositor.Include:
					if (xmlSchemaExternal.Schema != null)
					{
						goto IL_23A;
					}
					break;
				case Compositor.Import:
				{
					XmlSchemaImport xmlSchemaImport = xmlSchemaExternal as XmlSchemaImport;
					string @namespace = xmlSchemaImport.Namespace;
					if (@namespace == schema.TargetNamespace)
					{
						base.SendValidationEvent("Namespace attribute of an import must not match the real value of the enclosing targetNamespace of the <schema>.", xmlSchemaExternal);
					}
					if (xmlSchema != null)
					{
						if (@namespace != xmlSchema.TargetNamespace)
						{
							base.SendValidationEvent("The namespace attribute '{0}' of an import should be the same value as the targetNamespace '{1}' of the imported schema.", @namespace, xmlSchema.TargetNamespace, xmlSchemaImport);
						}
						XmlSchema xmlSchema2 = this.rootSchemaForRedefine;
						this.rootSchemaForRedefine = xmlSchema;
						this.Preprocess(xmlSchema, @namespace, imports);
						this.rootSchemaForRedefine = xmlSchema2;
					}
					else if (@namespace != null)
					{
						if (@namespace.Length == 0)
						{
							base.SendValidationEvent("The namespace attribute cannot have empty string as its value.", @namespace, xmlSchemaExternal);
						}
						else
						{
							this.ParseUri(@namespace, "The Namespace '{0}' is an invalid URI.", xmlSchemaExternal);
						}
					}
					break;
				}
				case Compositor.Redefine:
					if (xmlSchema != null)
					{
						this.CleanupRedefine(xmlSchemaExternal);
						goto IL_23A;
					}
					break;
				default:
					goto IL_23A;
				}
				IL_2A1:
				i++;
				continue;
				IL_23A:
				if (xmlSchema.TargetNamespace != null)
				{
					if (schema.TargetNamespace != xmlSchema.TargetNamespace)
					{
						base.SendValidationEvent("The targetNamespace '{0}' of included/redefined schema should be the same as the targetNamespace '{1}' of the including schema.", xmlSchema.TargetNamespace, schema.TargetNamespace, xmlSchemaExternal);
					}
				}
				else if (targetNamespace != null && targetNamespace.Length != 0)
				{
					xmlSchema = this.GetChameleonSchema(targetNamespace, xmlSchema);
					xmlSchemaExternal.Schema = xmlSchema;
				}
				this.Preprocess(xmlSchema, schema.TargetNamespace, imports);
				goto IL_2A1;
			}
			this.currentSchema = schema;
			this.BuildRefNamespaces(schema);
			this.ValidateIdAttribute(schema);
			this.targetNamespace = ((targetNamespace == null) ? string.Empty : targetNamespace);
			this.SetSchemaDefaults(schema);
			this.processedExternals.Clear();
			int j = 0;
			while (j < schema.Includes.Count)
			{
				XmlSchemaExternal xmlSchemaExternal2 = (XmlSchemaExternal)schema.Includes[j];
				XmlSchema schema2 = xmlSchemaExternal2.Schema;
				if (schema2 != null)
				{
					switch (xmlSchemaExternal2.Compositor)
					{
					case Compositor.Include:
						if (this.processedExternals[schema2] == null)
						{
							this.processedExternals.Add(schema2, xmlSchemaExternal2);
							this.CopyIncludedComponents(schema2, schema);
							goto IL_492;
						}
						break;
					case Compositor.Import:
					{
						if (schema2 == this.rootSchema)
						{
							goto IL_492;
						}
						XmlSchemaImport xmlSchemaImport2 = xmlSchemaExternal2 as XmlSchemaImport;
						string text2 = (xmlSchemaImport2.Namespace != null) ? xmlSchemaImport2.Namespace : string.Empty;
						if (!imports.Contains(schema2))
						{
							imports.Add(schema2);
						}
						if (!this.rootSchema.ImportedNamespaces.Contains(text2))
						{
							this.rootSchema.ImportedNamespaces.Add(text2);
							goto IL_492;
						}
						goto IL_492;
					}
					case Compositor.Redefine:
						if (this.redefinedList == null)
						{
							this.redefinedList = new ArrayList();
						}
						this.redefinedList.Add(new RedefineEntry(xmlSchemaExternal2 as XmlSchemaRedefine, this.rootSchemaForRedefine));
						if (this.processedExternals[schema2] == null)
						{
							this.processedExternals.Add(schema2, xmlSchemaExternal2);
							this.CopyIncludedComponents(schema2, schema);
							goto IL_492;
						}
						break;
					default:
						goto IL_492;
					}
				}
				else
				{
					if (xmlSchemaExternal2.Compositor != Compositor.Redefine)
					{
						goto IL_492;
					}
					XmlSchemaRedefine xmlSchemaRedefine = xmlSchemaExternal2 as XmlSchemaRedefine;
					if (xmlSchemaRedefine.BaseUri == null)
					{
						for (int k = 0; k < xmlSchemaRedefine.Items.Count; k++)
						{
							if (!(xmlSchemaRedefine.Items[k] is XmlSchemaAnnotation))
							{
								base.SendValidationEvent("'SchemaLocation' must successfully resolve if <redefine> contains any child other than <annotation>.", xmlSchemaRedefine);
								break;
							}
						}
						goto IL_492;
					}
					goto IL_492;
				}
				IL_499:
				j++;
				continue;
				IL_492:
				this.ValidateIdAttribute(xmlSchemaExternal2);
				goto IL_499;
			}
			List<XmlSchemaObject> list = new List<XmlSchemaObject>();
			XmlSchemaObjectCollection items = schema.Items;
			for (int l = 0; l < items.Count; l++)
			{
				this.SetParent(items[l], schema);
				XmlSchemaAttribute xmlSchemaAttribute = items[l] as XmlSchemaAttribute;
				if (xmlSchemaAttribute != null)
				{
					this.PreprocessAttribute(xmlSchemaAttribute);
					base.AddToTable(schema.Attributes, xmlSchemaAttribute.QualifiedName, xmlSchemaAttribute);
				}
				else if (items[l] is XmlSchemaAttributeGroup)
				{
					XmlSchemaAttributeGroup xmlSchemaAttributeGroup = (XmlSchemaAttributeGroup)items[l];
					this.PreprocessAttributeGroup(xmlSchemaAttributeGroup);
					base.AddToTable(schema.AttributeGroups, xmlSchemaAttributeGroup.QualifiedName, xmlSchemaAttributeGroup);
				}
				else if (items[l] is XmlSchemaComplexType)
				{
					XmlSchemaComplexType xmlSchemaComplexType = (XmlSchemaComplexType)items[l];
					this.PreprocessComplexType(xmlSchemaComplexType, false);
					base.AddToTable(schema.SchemaTypes, xmlSchemaComplexType.QualifiedName, xmlSchemaComplexType);
				}
				else if (items[l] is XmlSchemaSimpleType)
				{
					XmlSchemaSimpleType xmlSchemaSimpleType = (XmlSchemaSimpleType)items[l];
					this.PreprocessSimpleType(xmlSchemaSimpleType, false);
					base.AddToTable(schema.SchemaTypes, xmlSchemaSimpleType.QualifiedName, xmlSchemaSimpleType);
				}
				else if (items[l] is XmlSchemaElement)
				{
					XmlSchemaElement xmlSchemaElement = (XmlSchemaElement)items[l];
					this.PreprocessElement(xmlSchemaElement);
					base.AddToTable(schema.Elements, xmlSchemaElement.QualifiedName, xmlSchemaElement);
				}
				else if (items[l] is XmlSchemaGroup)
				{
					XmlSchemaGroup xmlSchemaGroup = (XmlSchemaGroup)items[l];
					this.PreprocessGroup(xmlSchemaGroup);
					base.AddToTable(schema.Groups, xmlSchemaGroup.QualifiedName, xmlSchemaGroup);
				}
				else if (items[l] is XmlSchemaNotation)
				{
					XmlSchemaNotation xmlSchemaNotation = (XmlSchemaNotation)items[l];
					this.PreprocessNotation(xmlSchemaNotation);
					base.AddToTable(schema.Notations, xmlSchemaNotation.QualifiedName, xmlSchemaNotation);
				}
				else if (items[l] is XmlSchemaAnnotation)
				{
					this.PreprocessAnnotation(items[l] as XmlSchemaAnnotation);
				}
				else
				{
					base.SendValidationEvent("The schema items collection cannot contain an object of type 'XmlSchemaInclude', 'XmlSchemaImport', or 'XmlSchemaRedefine'.", items[l]);
					list.Add(items[l]);
				}
			}
			for (int m = 0; m < list.Count; m++)
			{
				schema.Items.Remove(list[m]);
			}
		}

		// Token: 0x06003705 RID: 14085 RVA: 0x00136248 File Offset: 0x00134448
		private void CopyIncludedComponents(XmlSchema includedSchema, XmlSchema schema)
		{
			foreach (object obj in includedSchema.Elements.Values)
			{
				XmlSchemaElement xmlSchemaElement = (XmlSchemaElement)obj;
				base.AddToTable(schema.Elements, xmlSchemaElement.QualifiedName, xmlSchemaElement);
			}
			foreach (object obj2 in includedSchema.Attributes.Values)
			{
				XmlSchemaAttribute xmlSchemaAttribute = (XmlSchemaAttribute)obj2;
				base.AddToTable(schema.Attributes, xmlSchemaAttribute.QualifiedName, xmlSchemaAttribute);
			}
			foreach (object obj3 in includedSchema.Groups.Values)
			{
				XmlSchemaGroup xmlSchemaGroup = (XmlSchemaGroup)obj3;
				base.AddToTable(schema.Groups, xmlSchemaGroup.QualifiedName, xmlSchemaGroup);
			}
			foreach (object obj4 in includedSchema.AttributeGroups.Values)
			{
				XmlSchemaAttributeGroup xmlSchemaAttributeGroup = (XmlSchemaAttributeGroup)obj4;
				base.AddToTable(schema.AttributeGroups, xmlSchemaAttributeGroup.QualifiedName, xmlSchemaAttributeGroup);
			}
			foreach (object obj5 in includedSchema.SchemaTypes.Values)
			{
				XmlSchemaType xmlSchemaType = (XmlSchemaType)obj5;
				base.AddToTable(schema.SchemaTypes, xmlSchemaType.QualifiedName, xmlSchemaType);
			}
			foreach (object obj6 in includedSchema.Notations.Values)
			{
				XmlSchemaNotation xmlSchemaNotation = (XmlSchemaNotation)obj6;
				base.AddToTable(schema.Notations, xmlSchemaNotation.QualifiedName, xmlSchemaNotation);
			}
		}

		// Token: 0x06003706 RID: 14086 RVA: 0x0013647C File Offset: 0x0013467C
		private void PreprocessRedefine(RedefineEntry redefineEntry)
		{
			XmlSchemaRedefine redefine = redefineEntry.redefine;
			XmlSchema schema = redefine.Schema;
			this.currentSchema = Preprocessor.GetParentSchema(redefine);
			this.SetSchemaDefaults(this.currentSchema);
			if (schema.IsRedefined)
			{
				base.SendValidationEvent("Multiple redefines of the same schema will be ignored.", redefine, XmlSeverityType.Warning);
				return;
			}
			schema.IsRedefined = true;
			XmlSchema schemaToUpdate = redefineEntry.schemaToUpdate;
			ArrayList arrayList = new ArrayList();
			this.GetIncludedSet(schema, arrayList);
			string @namespace = (schemaToUpdate.TargetNamespace == null) ? string.Empty : schemaToUpdate.TargetNamespace;
			XmlSchemaObjectCollection items = redefine.Items;
			for (int i = 0; i < items.Count; i++)
			{
				this.SetParent(items[i], redefine);
				XmlSchemaGroup xmlSchemaGroup = items[i] as XmlSchemaGroup;
				if (xmlSchemaGroup != null)
				{
					this.PreprocessGroup(xmlSchemaGroup);
					xmlSchemaGroup.QualifiedName.SetNamespace(@namespace);
					if (redefine.Groups[xmlSchemaGroup.QualifiedName] != null)
					{
						base.SendValidationEvent("Double redefine for group.", xmlSchemaGroup);
					}
					else
					{
						base.AddToTable(redefine.Groups, xmlSchemaGroup.QualifiedName, xmlSchemaGroup);
						XmlSchemaGroup xmlSchemaGroup2 = (XmlSchemaGroup)schemaToUpdate.Groups[xmlSchemaGroup.QualifiedName];
						XmlSchema parentSchema = Preprocessor.GetParentSchema(xmlSchemaGroup2);
						if (xmlSchemaGroup2 == null || (parentSchema != schema && !arrayList.Contains(parentSchema)))
						{
							base.SendValidationEvent("Cannot find a {0} with name '{1}' to redefine.", "<group>", xmlSchemaGroup.QualifiedName.ToString(), xmlSchemaGroup);
						}
						else
						{
							xmlSchemaGroup.Redefined = xmlSchemaGroup2;
							schemaToUpdate.Groups.Insert(xmlSchemaGroup.QualifiedName, xmlSchemaGroup);
							this.CheckRefinedGroup(xmlSchemaGroup);
						}
					}
				}
				else if (items[i] is XmlSchemaAttributeGroup)
				{
					XmlSchemaAttributeGroup xmlSchemaAttributeGroup = (XmlSchemaAttributeGroup)items[i];
					this.PreprocessAttributeGroup(xmlSchemaAttributeGroup);
					xmlSchemaAttributeGroup.QualifiedName.SetNamespace(@namespace);
					if (redefine.AttributeGroups[xmlSchemaAttributeGroup.QualifiedName] != null)
					{
						base.SendValidationEvent("Double redefine for attribute group.", xmlSchemaAttributeGroup);
					}
					else
					{
						base.AddToTable(redefine.AttributeGroups, xmlSchemaAttributeGroup.QualifiedName, xmlSchemaAttributeGroup);
						XmlSchemaAttributeGroup xmlSchemaAttributeGroup2 = (XmlSchemaAttributeGroup)schemaToUpdate.AttributeGroups[xmlSchemaAttributeGroup.QualifiedName];
						XmlSchema parentSchema2 = Preprocessor.GetParentSchema(xmlSchemaAttributeGroup2);
						if (xmlSchemaAttributeGroup2 == null || (parentSchema2 != schema && !arrayList.Contains(parentSchema2)))
						{
							base.SendValidationEvent("Cannot find a {0} with name '{1}' to redefine.", "<attributeGroup>", xmlSchemaAttributeGroup.QualifiedName.ToString(), xmlSchemaAttributeGroup);
						}
						else
						{
							xmlSchemaAttributeGroup.Redefined = xmlSchemaAttributeGroup2;
							schemaToUpdate.AttributeGroups.Insert(xmlSchemaAttributeGroup.QualifiedName, xmlSchemaAttributeGroup);
							this.CheckRefinedAttributeGroup(xmlSchemaAttributeGroup);
						}
					}
				}
				else if (items[i] is XmlSchemaComplexType)
				{
					XmlSchemaComplexType xmlSchemaComplexType = (XmlSchemaComplexType)items[i];
					this.PreprocessComplexType(xmlSchemaComplexType, false);
					xmlSchemaComplexType.QualifiedName.SetNamespace(@namespace);
					if (redefine.SchemaTypes[xmlSchemaComplexType.QualifiedName] != null)
					{
						base.SendValidationEvent("Double redefine for complex type.", xmlSchemaComplexType);
					}
					else
					{
						base.AddToTable(redefine.SchemaTypes, xmlSchemaComplexType.QualifiedName, xmlSchemaComplexType);
						XmlSchemaType xmlSchemaType = (XmlSchemaType)schemaToUpdate.SchemaTypes[xmlSchemaComplexType.QualifiedName];
						XmlSchema parentSchema3 = Preprocessor.GetParentSchema(xmlSchemaType);
						if (xmlSchemaType == null || (parentSchema3 != schema && !arrayList.Contains(parentSchema3)))
						{
							base.SendValidationEvent("Cannot find a {0} with name '{1}' to redefine.", "<complexType>", xmlSchemaComplexType.QualifiedName.ToString(), xmlSchemaComplexType);
						}
						else if (xmlSchemaType is XmlSchemaComplexType)
						{
							xmlSchemaComplexType.Redefined = xmlSchemaType;
							schemaToUpdate.SchemaTypes.Insert(xmlSchemaComplexType.QualifiedName, xmlSchemaComplexType);
							this.CheckRefinedComplexType(xmlSchemaComplexType);
						}
						else
						{
							base.SendValidationEvent("Cannot redefine a simple type as complex type.", xmlSchemaComplexType);
						}
					}
				}
				else if (items[i] is XmlSchemaSimpleType)
				{
					XmlSchemaSimpleType xmlSchemaSimpleType = (XmlSchemaSimpleType)items[i];
					this.PreprocessSimpleType(xmlSchemaSimpleType, false);
					xmlSchemaSimpleType.QualifiedName.SetNamespace(@namespace);
					if (redefine.SchemaTypes[xmlSchemaSimpleType.QualifiedName] != null)
					{
						base.SendValidationEvent("Double redefine for simple type.", xmlSchemaSimpleType);
					}
					else
					{
						base.AddToTable(redefine.SchemaTypes, xmlSchemaSimpleType.QualifiedName, xmlSchemaSimpleType);
						XmlSchemaType xmlSchemaType2 = (XmlSchemaType)schemaToUpdate.SchemaTypes[xmlSchemaSimpleType.QualifiedName];
						XmlSchema parentSchema4 = Preprocessor.GetParentSchema(xmlSchemaType2);
						if (xmlSchemaType2 == null || (parentSchema4 != schema && !arrayList.Contains(parentSchema4)))
						{
							base.SendValidationEvent("Cannot find a {0} with name '{1}' to redefine.", "<simpleType>", xmlSchemaSimpleType.QualifiedName.ToString(), xmlSchemaSimpleType);
						}
						else if (xmlSchemaType2 is XmlSchemaSimpleType)
						{
							xmlSchemaSimpleType.Redefined = xmlSchemaType2;
							schemaToUpdate.SchemaTypes.Insert(xmlSchemaSimpleType.QualifiedName, xmlSchemaSimpleType);
							this.CheckRefinedSimpleType(xmlSchemaSimpleType);
						}
						else
						{
							base.SendValidationEvent("Cannot redefine a complex type as simple type.", xmlSchemaSimpleType);
						}
					}
				}
			}
		}

		// Token: 0x06003707 RID: 14087 RVA: 0x00136928 File Offset: 0x00134B28
		private void GetIncludedSet(XmlSchema schema, ArrayList includesList)
		{
			if (includesList.Contains(schema))
			{
				return;
			}
			includesList.Add(schema);
			for (int i = 0; i < schema.Includes.Count; i++)
			{
				XmlSchemaExternal xmlSchemaExternal = (XmlSchemaExternal)schema.Includes[i];
				if ((xmlSchemaExternal.Compositor == Compositor.Include || xmlSchemaExternal.Compositor == Compositor.Redefine) && xmlSchemaExternal.Schema != null)
				{
					this.GetIncludedSet(xmlSchemaExternal.Schema, includesList);
				}
			}
		}

		// Token: 0x06003708 RID: 14088 RVA: 0x00136998 File Offset: 0x00134B98
		internal static XmlSchema GetParentSchema(XmlSchemaObject currentSchemaObject)
		{
			XmlSchema xmlSchema = null;
			while (xmlSchema == null && currentSchemaObject != null)
			{
				currentSchemaObject = currentSchemaObject.Parent;
				xmlSchema = (currentSchemaObject as XmlSchema);
			}
			return xmlSchema;
		}

		// Token: 0x06003709 RID: 14089 RVA: 0x001369C0 File Offset: 0x00134BC0
		private void SetSchemaDefaults(XmlSchema schema)
		{
			if (schema.BlockDefault == XmlSchemaDerivationMethod.All)
			{
				this.blockDefault = XmlSchemaDerivationMethod.All;
			}
			else if (schema.BlockDefault == XmlSchemaDerivationMethod.None)
			{
				this.blockDefault = XmlSchemaDerivationMethod.Empty;
			}
			else
			{
				if ((schema.BlockDefault & ~(XmlSchemaDerivationMethod.Substitution | XmlSchemaDerivationMethod.Extension | XmlSchemaDerivationMethod.Restriction)) != XmlSchemaDerivationMethod.Empty)
				{
					base.SendValidationEvent("The values 'list' and 'union' are invalid for the blockDefault attribute.", schema);
				}
				this.blockDefault = (schema.BlockDefault & (XmlSchemaDerivationMethod.Substitution | XmlSchemaDerivationMethod.Extension | XmlSchemaDerivationMethod.Restriction));
			}
			if (schema.FinalDefault == XmlSchemaDerivationMethod.All)
			{
				this.finalDefault = XmlSchemaDerivationMethod.All;
			}
			else if (schema.FinalDefault == XmlSchemaDerivationMethod.None)
			{
				this.finalDefault = XmlSchemaDerivationMethod.Empty;
			}
			else
			{
				if ((schema.FinalDefault & ~(XmlSchemaDerivationMethod.Extension | XmlSchemaDerivationMethod.Restriction | XmlSchemaDerivationMethod.List | XmlSchemaDerivationMethod.Union)) != XmlSchemaDerivationMethod.Empty)
				{
					base.SendValidationEvent("The value 'substitution' is invalid for the finalDefault attribute.", schema);
				}
				this.finalDefault = (schema.FinalDefault & (XmlSchemaDerivationMethod.Extension | XmlSchemaDerivationMethod.Restriction | XmlSchemaDerivationMethod.List | XmlSchemaDerivationMethod.Union));
			}
			this.elementFormDefault = schema.ElementFormDefault;
			if (this.elementFormDefault == XmlSchemaForm.None)
			{
				this.elementFormDefault = XmlSchemaForm.Unqualified;
			}
			this.attributeFormDefault = schema.AttributeFormDefault;
			if (this.attributeFormDefault == XmlSchemaForm.None)
			{
				this.attributeFormDefault = XmlSchemaForm.Unqualified;
			}
		}

		// Token: 0x0600370A RID: 14090 RVA: 0x00136AB0 File Offset: 0x00134CB0
		private int CountGroupSelfReference(XmlSchemaObjectCollection items, XmlQualifiedName name, XmlSchemaGroup redefined)
		{
			int num = 0;
			for (int i = 0; i < items.Count; i++)
			{
				XmlSchemaGroupRef xmlSchemaGroupRef = items[i] as XmlSchemaGroupRef;
				if (xmlSchemaGroupRef != null)
				{
					if (xmlSchemaGroupRef.RefName == name)
					{
						xmlSchemaGroupRef.Redefined = redefined;
						if (xmlSchemaGroupRef.MinOccurs != 1m || xmlSchemaGroupRef.MaxOccurs != 1m)
						{
							base.SendValidationEvent("When group is redefined, the real value of both minOccurs and maxOccurs attribute must be 1 (or absent).", xmlSchemaGroupRef);
						}
						num++;
					}
				}
				else if (items[i] is XmlSchemaGroupBase)
				{
					num += this.CountGroupSelfReference(((XmlSchemaGroupBase)items[i]).Items, name, redefined);
				}
				if (num > 1)
				{
					break;
				}
			}
			return num;
		}

		// Token: 0x0600370B RID: 14091 RVA: 0x00136B60 File Offset: 0x00134D60
		private void CheckRefinedGroup(XmlSchemaGroup group)
		{
			int num = 0;
			if (group.Particle != null)
			{
				num = this.CountGroupSelfReference(group.Particle.Items, group.QualifiedName, group.Redefined);
			}
			if (num > 1)
			{
				base.SendValidationEvent("Multiple self-reference within a group is redefined.", group);
			}
			group.SelfReferenceCount = num;
		}

		// Token: 0x0600370C RID: 14092 RVA: 0x00136BAC File Offset: 0x00134DAC
		private void CheckRefinedAttributeGroup(XmlSchemaAttributeGroup attributeGroup)
		{
			int num = 0;
			for (int i = 0; i < attributeGroup.Attributes.Count; i++)
			{
				XmlSchemaAttributeGroupRef xmlSchemaAttributeGroupRef = attributeGroup.Attributes[i] as XmlSchemaAttributeGroupRef;
				if (xmlSchemaAttributeGroupRef != null && xmlSchemaAttributeGroupRef.RefName == attributeGroup.QualifiedName)
				{
					num++;
				}
			}
			if (num > 1)
			{
				base.SendValidationEvent("Multiple self-reference within an attribute group is redefined.", attributeGroup);
			}
			attributeGroup.SelfReferenceCount = num;
		}

		// Token: 0x0600370D RID: 14093 RVA: 0x00136C14 File Offset: 0x00134E14
		private void CheckRefinedSimpleType(XmlSchemaSimpleType stype)
		{
			if (stype.Content != null && stype.Content is XmlSchemaSimpleTypeRestriction && ((XmlSchemaSimpleTypeRestriction)stype.Content).BaseTypeName == stype.QualifiedName)
			{
				return;
			}
			base.SendValidationEvent("If type is being redefined, the base type has to be self-referenced.", stype);
		}

		// Token: 0x0600370E RID: 14094 RVA: 0x00136C60 File Offset: 0x00134E60
		private void CheckRefinedComplexType(XmlSchemaComplexType ctype)
		{
			if (ctype.ContentModel != null)
			{
				XmlQualifiedName baseTypeName;
				if (ctype.ContentModel is XmlSchemaComplexContent)
				{
					XmlSchemaComplexContent xmlSchemaComplexContent = (XmlSchemaComplexContent)ctype.ContentModel;
					if (xmlSchemaComplexContent.Content is XmlSchemaComplexContentRestriction)
					{
						baseTypeName = ((XmlSchemaComplexContentRestriction)xmlSchemaComplexContent.Content).BaseTypeName;
					}
					else
					{
						baseTypeName = ((XmlSchemaComplexContentExtension)xmlSchemaComplexContent.Content).BaseTypeName;
					}
				}
				else
				{
					XmlSchemaSimpleContent xmlSchemaSimpleContent = (XmlSchemaSimpleContent)ctype.ContentModel;
					if (xmlSchemaSimpleContent.Content is XmlSchemaSimpleContentRestriction)
					{
						baseTypeName = ((XmlSchemaSimpleContentRestriction)xmlSchemaSimpleContent.Content).BaseTypeName;
					}
					else
					{
						baseTypeName = ((XmlSchemaSimpleContentExtension)xmlSchemaSimpleContent.Content).BaseTypeName;
					}
				}
				if (baseTypeName == ctype.QualifiedName)
				{
					return;
				}
			}
			base.SendValidationEvent("If type is being redefined, the base type has to be self-referenced.", ctype);
		}

		// Token: 0x0600370F RID: 14095 RVA: 0x00136D1C File Offset: 0x00134F1C
		private void PreprocessAttribute(XmlSchemaAttribute attribute)
		{
			if (attribute.Name != null)
			{
				this.ValidateNameAttribute(attribute);
				attribute.SetQualifiedName(new XmlQualifiedName(attribute.Name, this.targetNamespace));
			}
			else
			{
				base.SendValidationEvent("The required attribute '{0}' is missing.", "name", attribute);
			}
			if (attribute.Use != XmlSchemaUse.None)
			{
				base.SendValidationEvent("The '{0}' attribute cannot be present.", "use", attribute);
			}
			if (attribute.Form != XmlSchemaForm.None)
			{
				base.SendValidationEvent("The '{0}' attribute cannot be present.", "form", attribute);
			}
			this.PreprocessAttributeContent(attribute);
			this.ValidateIdAttribute(attribute);
		}

		// Token: 0x06003710 RID: 14096 RVA: 0x00136DA4 File Offset: 0x00134FA4
		private void PreprocessLocalAttribute(XmlSchemaAttribute attribute)
		{
			if (attribute.Name != null)
			{
				this.ValidateNameAttribute(attribute);
				this.PreprocessAttributeContent(attribute);
				attribute.SetQualifiedName(new XmlQualifiedName(attribute.Name, (attribute.Form == XmlSchemaForm.Qualified || (attribute.Form == XmlSchemaForm.None && this.attributeFormDefault == XmlSchemaForm.Qualified)) ? this.targetNamespace : null));
			}
			else
			{
				this.PreprocessAnnotation(attribute);
				if (attribute.RefName.IsEmpty)
				{
					base.SendValidationEvent("For attribute '{0}', either the name or the ref attribute must be present, but not both.", "???", attribute);
				}
				else
				{
					this.ValidateQNameAttribute(attribute, "ref", attribute.RefName);
				}
				if (!attribute.SchemaTypeName.IsEmpty || attribute.SchemaType != null || attribute.Form != XmlSchemaForm.None)
				{
					base.SendValidationEvent("If ref is present, all of 'simpleType', 'form', 'type', and 'use' must be absent.", attribute);
				}
				attribute.SetQualifiedName(attribute.RefName);
			}
			this.ValidateIdAttribute(attribute);
		}

		// Token: 0x06003711 RID: 14097 RVA: 0x00136E74 File Offset: 0x00135074
		private void PreprocessAttributeContent(XmlSchemaAttribute attribute)
		{
			this.PreprocessAnnotation(attribute);
			if (Ref.Equal(this.currentSchema.TargetNamespace, this.NsXsi))
			{
				base.SendValidationEvent("The target namespace of an attribute declaration, whether local or global, must not match http://www.w3.org/2001/XMLSchema-instance.", attribute);
			}
			if (!attribute.RefName.IsEmpty)
			{
				base.SendValidationEvent("The '{0}' attribute cannot be present.", "ref", attribute);
			}
			if (attribute.DefaultValue != null && attribute.FixedValue != null)
			{
				base.SendValidationEvent("The fixed and default attributes cannot both be present.", attribute);
			}
			if (attribute.DefaultValue != null && attribute.Use != XmlSchemaUse.Optional && attribute.Use != XmlSchemaUse.None)
			{
				base.SendValidationEvent("The 'use' attribute must be optional (or absent) if the default attribute is present.", attribute);
			}
			if (attribute.Name == this.Xmlns)
			{
				base.SendValidationEvent("The value 'xmlns' cannot be used as the name of an attribute declaration.", attribute);
			}
			if (attribute.SchemaType != null)
			{
				this.SetParent(attribute.SchemaType, attribute);
				if (!attribute.SchemaTypeName.IsEmpty)
				{
					base.SendValidationEvent("The type attribute cannot be present with either simpleType or complexType.", attribute);
				}
				this.PreprocessSimpleType(attribute.SchemaType, true);
			}
			if (!attribute.SchemaTypeName.IsEmpty)
			{
				this.ValidateQNameAttribute(attribute, "type", attribute.SchemaTypeName);
			}
		}

		// Token: 0x06003712 RID: 14098 RVA: 0x00136F84 File Offset: 0x00135184
		private void PreprocessAttributeGroup(XmlSchemaAttributeGroup attributeGroup)
		{
			if (attributeGroup.Name != null)
			{
				this.ValidateNameAttribute(attributeGroup);
				attributeGroup.SetQualifiedName(new XmlQualifiedName(attributeGroup.Name, this.targetNamespace));
			}
			else
			{
				base.SendValidationEvent("The required attribute '{0}' is missing.", "name", attributeGroup);
			}
			this.PreprocessAttributes(attributeGroup.Attributes, attributeGroup.AnyAttribute, attributeGroup);
			this.PreprocessAnnotation(attributeGroup);
			this.ValidateIdAttribute(attributeGroup);
		}

		// Token: 0x06003713 RID: 14099 RVA: 0x00136FEC File Offset: 0x001351EC
		private void PreprocessElement(XmlSchemaElement element)
		{
			if (element.Name != null)
			{
				this.ValidateNameAttribute(element);
				element.SetQualifiedName(new XmlQualifiedName(element.Name, this.targetNamespace));
			}
			else
			{
				base.SendValidationEvent("The required attribute '{0}' is missing.", "name", element);
			}
			this.PreprocessElementContent(element);
			if (element.Final == XmlSchemaDerivationMethod.All)
			{
				element.SetFinalResolved(XmlSchemaDerivationMethod.All);
			}
			else if (element.Final == XmlSchemaDerivationMethod.None)
			{
				if (this.finalDefault == XmlSchemaDerivationMethod.All)
				{
					element.SetFinalResolved(XmlSchemaDerivationMethod.All);
				}
				else
				{
					element.SetFinalResolved(this.finalDefault & (XmlSchemaDerivationMethod.Extension | XmlSchemaDerivationMethod.Restriction));
				}
			}
			else
			{
				if ((element.Final & ~(XmlSchemaDerivationMethod.Extension | XmlSchemaDerivationMethod.Restriction)) != XmlSchemaDerivationMethod.Empty)
				{
					base.SendValidationEvent("The values 'substitution', 'list', and 'union' are invalid for the final attribute on element.", element);
				}
				element.SetFinalResolved(element.Final & (XmlSchemaDerivationMethod.Extension | XmlSchemaDerivationMethod.Restriction));
			}
			if (element.Form != XmlSchemaForm.None)
			{
				base.SendValidationEvent("The '{0}' attribute cannot be present.", "form", element);
			}
			if (element.MinOccursString != null)
			{
				base.SendValidationEvent("The '{0}' attribute cannot be present.", "minOccurs", element);
			}
			if (element.MaxOccursString != null)
			{
				base.SendValidationEvent("The '{0}' attribute cannot be present.", "maxOccurs", element);
			}
			if (!element.SubstitutionGroup.IsEmpty)
			{
				this.ValidateQNameAttribute(element, "type", element.SubstitutionGroup);
			}
			this.ValidateIdAttribute(element);
		}

		// Token: 0x06003714 RID: 14100 RVA: 0x00137120 File Offset: 0x00135320
		private void PreprocessLocalElement(XmlSchemaElement element)
		{
			if (element.Name != null)
			{
				this.ValidateNameAttribute(element);
				this.PreprocessElementContent(element);
				element.SetQualifiedName(new XmlQualifiedName(element.Name, (element.Form == XmlSchemaForm.Qualified || (element.Form == XmlSchemaForm.None && this.elementFormDefault == XmlSchemaForm.Qualified)) ? this.targetNamespace : null));
			}
			else
			{
				this.PreprocessAnnotation(element);
				if (element.RefName.IsEmpty)
				{
					base.SendValidationEvent("For element declaration, either the name or the ref attribute must be present.", element);
				}
				else
				{
					this.ValidateQNameAttribute(element, "ref", element.RefName);
				}
				if (!element.SchemaTypeName.IsEmpty || element.HasAbstractAttribute || element.Block != XmlSchemaDerivationMethod.None || element.SchemaType != null || element.HasConstraints || element.DefaultValue != null || element.Form != XmlSchemaForm.None || element.FixedValue != null || element.HasNillableAttribute)
				{
					base.SendValidationEvent("If ref is present, all of <complexType>, <simpleType>, <key>, <keyref>, <unique>, nillable, default, fixed, form, block, and type must be absent.", element);
				}
				if (element.DefaultValue != null && element.FixedValue != null)
				{
					base.SendValidationEvent("The fixed and default attributes cannot both be present.", element);
				}
				element.SetQualifiedName(element.RefName);
			}
			if (element.MinOccurs > element.MaxOccurs)
			{
				element.MinOccurs = 0m;
				base.SendValidationEvent("minOccurs value cannot be greater than maxOccurs value.", element);
			}
			if (element.HasAbstractAttribute)
			{
				base.SendValidationEvent("The '{0}' attribute cannot be present.", "abstract", element);
			}
			if (element.Final != XmlSchemaDerivationMethod.None)
			{
				base.SendValidationEvent("The '{0}' attribute cannot be present.", "final", element);
			}
			if (!element.SubstitutionGroup.IsEmpty)
			{
				base.SendValidationEvent("The '{0}' attribute cannot be present.", "substitutionGroup", element);
			}
			this.ValidateIdAttribute(element);
		}

		// Token: 0x06003715 RID: 14101 RVA: 0x001372BC File Offset: 0x001354BC
		private void PreprocessElementContent(XmlSchemaElement element)
		{
			this.PreprocessAnnotation(element);
			if (!element.RefName.IsEmpty)
			{
				base.SendValidationEvent("The '{0}' attribute cannot be present.", "ref", element);
			}
			if (element.Block == XmlSchemaDerivationMethod.All)
			{
				element.SetBlockResolved(XmlSchemaDerivationMethod.All);
			}
			else if (element.Block == XmlSchemaDerivationMethod.None)
			{
				if (this.blockDefault == XmlSchemaDerivationMethod.All)
				{
					element.SetBlockResolved(XmlSchemaDerivationMethod.All);
				}
				else
				{
					element.SetBlockResolved(this.blockDefault & (XmlSchemaDerivationMethod.Substitution | XmlSchemaDerivationMethod.Extension | XmlSchemaDerivationMethod.Restriction));
				}
			}
			else
			{
				if ((element.Block & ~(XmlSchemaDerivationMethod.Substitution | XmlSchemaDerivationMethod.Extension | XmlSchemaDerivationMethod.Restriction)) != XmlSchemaDerivationMethod.Empty)
				{
					base.SendValidationEvent("The values 'list' and 'union' are invalid for the block attribute on element.", element);
				}
				element.SetBlockResolved(element.Block & (XmlSchemaDerivationMethod.Substitution | XmlSchemaDerivationMethod.Extension | XmlSchemaDerivationMethod.Restriction));
			}
			if (element.SchemaType != null)
			{
				this.SetParent(element.SchemaType, element);
				if (!element.SchemaTypeName.IsEmpty)
				{
					base.SendValidationEvent("The type attribute cannot be present with either simpleType or complexType.", element);
				}
				if (element.SchemaType is XmlSchemaComplexType)
				{
					this.PreprocessComplexType((XmlSchemaComplexType)element.SchemaType, true);
				}
				else
				{
					this.PreprocessSimpleType((XmlSchemaSimpleType)element.SchemaType, true);
				}
			}
			if (!element.SchemaTypeName.IsEmpty)
			{
				this.ValidateQNameAttribute(element, "type", element.SchemaTypeName);
			}
			if (element.DefaultValue != null && element.FixedValue != null)
			{
				base.SendValidationEvent("The fixed and default attributes cannot both be present.", element);
			}
			for (int i = 0; i < element.Constraints.Count; i++)
			{
				XmlSchemaIdentityConstraint xmlSchemaIdentityConstraint = (XmlSchemaIdentityConstraint)element.Constraints[i];
				this.SetParent(xmlSchemaIdentityConstraint, element);
				this.PreprocessIdentityConstraint(xmlSchemaIdentityConstraint);
			}
		}

		// Token: 0x06003716 RID: 14102 RVA: 0x00137438 File Offset: 0x00135638
		private void PreprocessIdentityConstraint(XmlSchemaIdentityConstraint constraint)
		{
			bool flag = true;
			this.PreprocessAnnotation(constraint);
			if (constraint.Name != null)
			{
				this.ValidateNameAttribute(constraint);
				constraint.SetQualifiedName(new XmlQualifiedName(constraint.Name, this.targetNamespace));
			}
			else
			{
				base.SendValidationEvent("The required attribute '{0}' is missing.", "name", constraint);
				flag = false;
			}
			if (this.rootSchema.IdentityConstraints[constraint.QualifiedName] != null)
			{
				base.SendValidationEvent("The identity constraint '{0}' has already been declared.", constraint.QualifiedName.ToString(), constraint);
				flag = false;
			}
			else
			{
				this.rootSchema.IdentityConstraints.Add(constraint.QualifiedName, constraint);
			}
			if (constraint.Selector == null)
			{
				base.SendValidationEvent("Selector must be present.", constraint);
				flag = false;
			}
			if (constraint.Fields.Count == 0)
			{
				base.SendValidationEvent("At least one field must be present.", constraint);
				flag = false;
			}
			if (constraint is XmlSchemaKeyref)
			{
				XmlSchemaKeyref xmlSchemaKeyref = (XmlSchemaKeyref)constraint;
				if (xmlSchemaKeyref.Refer.IsEmpty)
				{
					base.SendValidationEvent("The referring attribute must be present.", constraint);
					flag = false;
				}
				else
				{
					this.ValidateQNameAttribute(xmlSchemaKeyref, "refer", xmlSchemaKeyref.Refer);
				}
			}
			if (flag)
			{
				this.ValidateIdAttribute(constraint);
				this.ValidateIdAttribute(constraint.Selector);
				this.SetParent(constraint.Selector, constraint);
				for (int i = 0; i < constraint.Fields.Count; i++)
				{
					this.SetParent(constraint.Fields[i], constraint);
					this.ValidateIdAttribute(constraint.Fields[i]);
				}
			}
		}

		// Token: 0x06003717 RID: 14103 RVA: 0x001375A0 File Offset: 0x001357A0
		private void PreprocessSimpleType(XmlSchemaSimpleType simpleType, bool local)
		{
			if (local)
			{
				if (simpleType.Name != null)
				{
					base.SendValidationEvent("The '{0}' attribute cannot be present.", "name", simpleType);
				}
			}
			else
			{
				if (simpleType.Name != null)
				{
					this.ValidateNameAttribute(simpleType);
					simpleType.SetQualifiedName(new XmlQualifiedName(simpleType.Name, this.targetNamespace));
				}
				else
				{
					base.SendValidationEvent("The required attribute '{0}' is missing.", "name", simpleType);
				}
				if (simpleType.Final == XmlSchemaDerivationMethod.All)
				{
					simpleType.SetFinalResolved(XmlSchemaDerivationMethod.All);
				}
				else if (simpleType.Final == XmlSchemaDerivationMethod.None)
				{
					if (this.finalDefault == XmlSchemaDerivationMethod.All)
					{
						simpleType.SetFinalResolved(XmlSchemaDerivationMethod.All);
					}
					else
					{
						simpleType.SetFinalResolved(this.finalDefault & (XmlSchemaDerivationMethod.Extension | XmlSchemaDerivationMethod.Restriction | XmlSchemaDerivationMethod.List | XmlSchemaDerivationMethod.Union));
					}
				}
				else
				{
					if ((simpleType.Final & ~(XmlSchemaDerivationMethod.Extension | XmlSchemaDerivationMethod.Restriction | XmlSchemaDerivationMethod.List | XmlSchemaDerivationMethod.Union)) != XmlSchemaDerivationMethod.Empty)
					{
						base.SendValidationEvent("The values 'substitution' and 'extension' are invalid for the final attribute on simpleType.", simpleType);
					}
					simpleType.SetFinalResolved(simpleType.Final & (XmlSchemaDerivationMethod.Extension | XmlSchemaDerivationMethod.Restriction | XmlSchemaDerivationMethod.List | XmlSchemaDerivationMethod.Union));
				}
			}
			if (simpleType.Content == null)
			{
				base.SendValidationEvent("SimpleType content is missing.", simpleType);
			}
			else if (simpleType.Content is XmlSchemaSimpleTypeRestriction)
			{
				XmlSchemaSimpleTypeRestriction xmlSchemaSimpleTypeRestriction = (XmlSchemaSimpleTypeRestriction)simpleType.Content;
				this.SetParent(xmlSchemaSimpleTypeRestriction, simpleType);
				for (int i = 0; i < xmlSchemaSimpleTypeRestriction.Facets.Count; i++)
				{
					this.SetParent(xmlSchemaSimpleTypeRestriction.Facets[i], xmlSchemaSimpleTypeRestriction);
				}
				if (xmlSchemaSimpleTypeRestriction.BaseType != null)
				{
					if (!xmlSchemaSimpleTypeRestriction.BaseTypeName.IsEmpty)
					{
						base.SendValidationEvent("SimpleType restriction should have either the base attribute or a simpleType child, but not both.", xmlSchemaSimpleTypeRestriction);
					}
					this.PreprocessSimpleType(xmlSchemaSimpleTypeRestriction.BaseType, true);
				}
				else if (xmlSchemaSimpleTypeRestriction.BaseTypeName.IsEmpty)
				{
					base.SendValidationEvent("SimpleType restriction should have either the base attribute or a simpleType child to indicate the base type for the derivation.", xmlSchemaSimpleTypeRestriction);
				}
				else
				{
					this.ValidateQNameAttribute(xmlSchemaSimpleTypeRestriction, "base", xmlSchemaSimpleTypeRestriction.BaseTypeName);
				}
				this.PreprocessAnnotation(xmlSchemaSimpleTypeRestriction);
				this.ValidateIdAttribute(xmlSchemaSimpleTypeRestriction);
			}
			else if (simpleType.Content is XmlSchemaSimpleTypeList)
			{
				XmlSchemaSimpleTypeList xmlSchemaSimpleTypeList = (XmlSchemaSimpleTypeList)simpleType.Content;
				this.SetParent(xmlSchemaSimpleTypeList, simpleType);
				if (xmlSchemaSimpleTypeList.ItemType != null)
				{
					if (!xmlSchemaSimpleTypeList.ItemTypeName.IsEmpty)
					{
						base.SendValidationEvent("SimpleType list should have either the itemType attribute or a simpleType child, but not both.", xmlSchemaSimpleTypeList);
					}
					this.SetParent(xmlSchemaSimpleTypeList.ItemType, xmlSchemaSimpleTypeList);
					this.PreprocessSimpleType(xmlSchemaSimpleTypeList.ItemType, true);
				}
				else if (xmlSchemaSimpleTypeList.ItemTypeName.IsEmpty)
				{
					base.SendValidationEvent("SimpleType list should have either the itemType attribute or a simpleType child to indicate the itemType of the list.", xmlSchemaSimpleTypeList);
				}
				else
				{
					this.ValidateQNameAttribute(xmlSchemaSimpleTypeList, "itemType", xmlSchemaSimpleTypeList.ItemTypeName);
				}
				this.PreprocessAnnotation(xmlSchemaSimpleTypeList);
				this.ValidateIdAttribute(xmlSchemaSimpleTypeList);
			}
			else
			{
				XmlSchemaSimpleTypeUnion xmlSchemaSimpleTypeUnion = (XmlSchemaSimpleTypeUnion)simpleType.Content;
				this.SetParent(xmlSchemaSimpleTypeUnion, simpleType);
				int num = xmlSchemaSimpleTypeUnion.BaseTypes.Count;
				if (xmlSchemaSimpleTypeUnion.MemberTypes != null)
				{
					num += xmlSchemaSimpleTypeUnion.MemberTypes.Length;
					XmlQualifiedName[] memberTypes = xmlSchemaSimpleTypeUnion.MemberTypes;
					for (int j = 0; j < memberTypes.Length; j++)
					{
						this.ValidateQNameAttribute(xmlSchemaSimpleTypeUnion, "memberTypes", memberTypes[j]);
					}
				}
				if (num == 0)
				{
					base.SendValidationEvent("Either the memberTypes attribute must be non-empty or there must be at least one simpleType child.", xmlSchemaSimpleTypeUnion);
				}
				for (int k = 0; k < xmlSchemaSimpleTypeUnion.BaseTypes.Count; k++)
				{
					XmlSchemaSimpleType xmlSchemaSimpleType = (XmlSchemaSimpleType)xmlSchemaSimpleTypeUnion.BaseTypes[k];
					this.SetParent(xmlSchemaSimpleType, xmlSchemaSimpleTypeUnion);
					this.PreprocessSimpleType(xmlSchemaSimpleType, true);
				}
				this.PreprocessAnnotation(xmlSchemaSimpleTypeUnion);
				this.ValidateIdAttribute(xmlSchemaSimpleTypeUnion);
			}
			this.ValidateIdAttribute(simpleType);
		}

		// Token: 0x06003718 RID: 14104 RVA: 0x001378C0 File Offset: 0x00135AC0
		private void PreprocessComplexType(XmlSchemaComplexType complexType, bool local)
		{
			if (local)
			{
				if (complexType.Name != null)
				{
					base.SendValidationEvent("The '{0}' attribute cannot be present.", "name", complexType);
				}
			}
			else
			{
				if (complexType.Name != null)
				{
					this.ValidateNameAttribute(complexType);
					complexType.SetQualifiedName(new XmlQualifiedName(complexType.Name, this.targetNamespace));
				}
				else
				{
					base.SendValidationEvent("The required attribute '{0}' is missing.", "name", complexType);
				}
				if (complexType.Block == XmlSchemaDerivationMethod.All)
				{
					complexType.SetBlockResolved(XmlSchemaDerivationMethod.All);
				}
				else if (complexType.Block == XmlSchemaDerivationMethod.None)
				{
					complexType.SetBlockResolved(this.blockDefault & (XmlSchemaDerivationMethod.Extension | XmlSchemaDerivationMethod.Restriction));
				}
				else
				{
					if ((complexType.Block & ~(XmlSchemaDerivationMethod.Extension | XmlSchemaDerivationMethod.Restriction)) != XmlSchemaDerivationMethod.Empty)
					{
						base.SendValidationEvent("The values 'substitution', 'list', and 'union' are invalid for the block attribute on complexType.", complexType);
					}
					complexType.SetBlockResolved(complexType.Block & (XmlSchemaDerivationMethod.Extension | XmlSchemaDerivationMethod.Restriction));
				}
				if (complexType.Final == XmlSchemaDerivationMethod.All)
				{
					complexType.SetFinalResolved(XmlSchemaDerivationMethod.All);
				}
				else if (complexType.Final == XmlSchemaDerivationMethod.None)
				{
					if (this.finalDefault == XmlSchemaDerivationMethod.All)
					{
						complexType.SetFinalResolved(XmlSchemaDerivationMethod.All);
					}
					else
					{
						complexType.SetFinalResolved(this.finalDefault & (XmlSchemaDerivationMethod.Extension | XmlSchemaDerivationMethod.Restriction));
					}
				}
				else
				{
					if ((complexType.Final & ~(XmlSchemaDerivationMethod.Extension | XmlSchemaDerivationMethod.Restriction)) != XmlSchemaDerivationMethod.Empty)
					{
						base.SendValidationEvent("The values 'substitution', 'list', and 'union' are invalid for the final attribute on complexType.", complexType);
					}
					complexType.SetFinalResolved(complexType.Final & (XmlSchemaDerivationMethod.Extension | XmlSchemaDerivationMethod.Restriction));
				}
			}
			if (complexType.ContentModel != null)
			{
				this.SetParent(complexType.ContentModel, complexType);
				this.PreprocessAnnotation(complexType.ContentModel);
				if (complexType.Particle == null)
				{
					XmlSchemaObjectCollection attributes = complexType.Attributes;
				}
				if (complexType.ContentModel is XmlSchemaSimpleContent)
				{
					XmlSchemaSimpleContent xmlSchemaSimpleContent = (XmlSchemaSimpleContent)complexType.ContentModel;
					if (xmlSchemaSimpleContent.Content == null)
					{
						if (complexType.QualifiedName == XmlQualifiedName.Empty)
						{
							base.SendValidationEvent("'restriction' or 'extension' child is required for complexType with simpleContent or complexContent child.", complexType);
						}
						else
						{
							base.SendValidationEvent("'restriction' or 'extension' child is required for complexType '{0}' in namespace '{1}', because it has a simpleContent or complexContent child.", complexType.QualifiedName.Name, complexType.QualifiedName.Namespace, complexType);
						}
					}
					else
					{
						this.SetParent(xmlSchemaSimpleContent.Content, xmlSchemaSimpleContent);
						this.PreprocessAnnotation(xmlSchemaSimpleContent.Content);
						if (xmlSchemaSimpleContent.Content is XmlSchemaSimpleContentExtension)
						{
							XmlSchemaSimpleContentExtension xmlSchemaSimpleContentExtension = (XmlSchemaSimpleContentExtension)xmlSchemaSimpleContent.Content;
							if (xmlSchemaSimpleContentExtension.BaseTypeName.IsEmpty)
							{
								base.SendValidationEvent("The '{0}' attribute is either invalid or missing.", "base", xmlSchemaSimpleContentExtension);
							}
							else
							{
								this.ValidateQNameAttribute(xmlSchemaSimpleContentExtension, "base", xmlSchemaSimpleContentExtension.BaseTypeName);
							}
							this.PreprocessAttributes(xmlSchemaSimpleContentExtension.Attributes, xmlSchemaSimpleContentExtension.AnyAttribute, xmlSchemaSimpleContentExtension);
							this.ValidateIdAttribute(xmlSchemaSimpleContentExtension);
						}
						else
						{
							XmlSchemaSimpleContentRestriction xmlSchemaSimpleContentRestriction = (XmlSchemaSimpleContentRestriction)xmlSchemaSimpleContent.Content;
							if (xmlSchemaSimpleContentRestriction.BaseTypeName.IsEmpty)
							{
								base.SendValidationEvent("The '{0}' attribute is either invalid or missing.", "base", xmlSchemaSimpleContentRestriction);
							}
							else
							{
								this.ValidateQNameAttribute(xmlSchemaSimpleContentRestriction, "base", xmlSchemaSimpleContentRestriction.BaseTypeName);
							}
							if (xmlSchemaSimpleContentRestriction.BaseType != null)
							{
								this.SetParent(xmlSchemaSimpleContentRestriction.BaseType, xmlSchemaSimpleContentRestriction);
								this.PreprocessSimpleType(xmlSchemaSimpleContentRestriction.BaseType, true);
							}
							this.PreprocessAttributes(xmlSchemaSimpleContentRestriction.Attributes, xmlSchemaSimpleContentRestriction.AnyAttribute, xmlSchemaSimpleContentRestriction);
							this.ValidateIdAttribute(xmlSchemaSimpleContentRestriction);
						}
					}
					this.ValidateIdAttribute(xmlSchemaSimpleContent);
				}
				else
				{
					XmlSchemaComplexContent xmlSchemaComplexContent = (XmlSchemaComplexContent)complexType.ContentModel;
					if (xmlSchemaComplexContent.Content == null)
					{
						if (complexType.QualifiedName == XmlQualifiedName.Empty)
						{
							base.SendValidationEvent("'restriction' or 'extension' child is required for complexType with simpleContent or complexContent child.", complexType);
						}
						else
						{
							base.SendValidationEvent("'restriction' or 'extension' child is required for complexType '{0}' in namespace '{1}', because it has a simpleContent or complexContent child.", complexType.QualifiedName.Name, complexType.QualifiedName.Namespace, complexType);
						}
					}
					else
					{
						if (!xmlSchemaComplexContent.HasMixedAttribute && complexType.IsMixed)
						{
							xmlSchemaComplexContent.IsMixed = true;
						}
						this.SetParent(xmlSchemaComplexContent.Content, xmlSchemaComplexContent);
						this.PreprocessAnnotation(xmlSchemaComplexContent.Content);
						if (xmlSchemaComplexContent.Content is XmlSchemaComplexContentExtension)
						{
							XmlSchemaComplexContentExtension xmlSchemaComplexContentExtension = (XmlSchemaComplexContentExtension)xmlSchemaComplexContent.Content;
							if (xmlSchemaComplexContentExtension.BaseTypeName.IsEmpty)
							{
								base.SendValidationEvent("The '{0}' attribute is either invalid or missing.", "base", xmlSchemaComplexContentExtension);
							}
							else
							{
								this.ValidateQNameAttribute(xmlSchemaComplexContentExtension, "base", xmlSchemaComplexContentExtension.BaseTypeName);
							}
							if (xmlSchemaComplexContentExtension.Particle != null)
							{
								this.SetParent(xmlSchemaComplexContentExtension.Particle, xmlSchemaComplexContentExtension);
								this.PreprocessParticle(xmlSchemaComplexContentExtension.Particle);
							}
							this.PreprocessAttributes(xmlSchemaComplexContentExtension.Attributes, xmlSchemaComplexContentExtension.AnyAttribute, xmlSchemaComplexContentExtension);
							this.ValidateIdAttribute(xmlSchemaComplexContentExtension);
						}
						else
						{
							XmlSchemaComplexContentRestriction xmlSchemaComplexContentRestriction = (XmlSchemaComplexContentRestriction)xmlSchemaComplexContent.Content;
							if (xmlSchemaComplexContentRestriction.BaseTypeName.IsEmpty)
							{
								base.SendValidationEvent("The '{0}' attribute is either invalid or missing.", "base", xmlSchemaComplexContentRestriction);
							}
							else
							{
								this.ValidateQNameAttribute(xmlSchemaComplexContentRestriction, "base", xmlSchemaComplexContentRestriction.BaseTypeName);
							}
							if (xmlSchemaComplexContentRestriction.Particle != null)
							{
								this.SetParent(xmlSchemaComplexContentRestriction.Particle, xmlSchemaComplexContentRestriction);
								this.PreprocessParticle(xmlSchemaComplexContentRestriction.Particle);
							}
							this.PreprocessAttributes(xmlSchemaComplexContentRestriction.Attributes, xmlSchemaComplexContentRestriction.AnyAttribute, xmlSchemaComplexContentRestriction);
							this.ValidateIdAttribute(xmlSchemaComplexContentRestriction);
						}
						this.ValidateIdAttribute(xmlSchemaComplexContent);
					}
				}
			}
			else
			{
				if (complexType.Particle != null)
				{
					this.SetParent(complexType.Particle, complexType);
					this.PreprocessParticle(complexType.Particle);
				}
				this.PreprocessAttributes(complexType.Attributes, complexType.AnyAttribute, complexType);
			}
			this.ValidateIdAttribute(complexType);
		}

		// Token: 0x06003719 RID: 14105 RVA: 0x00137D98 File Offset: 0x00135F98
		private void PreprocessGroup(XmlSchemaGroup group)
		{
			if (group.Name != null)
			{
				this.ValidateNameAttribute(group);
				group.SetQualifiedName(new XmlQualifiedName(group.Name, this.targetNamespace));
			}
			else
			{
				base.SendValidationEvent("The required attribute '{0}' is missing.", "name", group);
			}
			if (group.Particle == null)
			{
				base.SendValidationEvent("'sequence', 'choice', or 'all' child is required.", group);
				return;
			}
			if (group.Particle.MinOccursString != null)
			{
				base.SendValidationEvent("The '{0}' attribute cannot be present.", "minOccurs", group.Particle);
			}
			if (group.Particle.MaxOccursString != null)
			{
				base.SendValidationEvent("The '{0}' attribute cannot be present.", "maxOccurs", group.Particle);
			}
			this.PreprocessParticle(group.Particle);
			this.PreprocessAnnotation(group);
			this.ValidateIdAttribute(group);
		}

		// Token: 0x0600371A RID: 14106 RVA: 0x00137E54 File Offset: 0x00136054
		private void PreprocessNotation(XmlSchemaNotation notation)
		{
			if (notation.Name != null)
			{
				this.ValidateNameAttribute(notation);
				notation.QualifiedName = new XmlQualifiedName(notation.Name, this.targetNamespace);
			}
			else
			{
				base.SendValidationEvent("The required attribute '{0}' is missing.", "name", notation);
			}
			if (notation.Public == null && notation.System == null)
			{
				base.SendValidationEvent("NOTATION must have either the Public or System attribute present.", notation);
			}
			else
			{
				if (notation.Public != null)
				{
					try
					{
						XmlConvert.VerifyTOKEN(notation.Public);
					}
					catch (XmlException innerException)
					{
						base.SendValidationEvent("Public attribute '{0}' is an invalid URI.", new string[]
						{
							notation.Public
						}, innerException, notation);
					}
				}
				if (notation.System != null)
				{
					this.ParseUri(notation.System, "System attribute '{0}' is an invalid URI.", notation);
				}
			}
			this.PreprocessAnnotation(notation);
			this.ValidateIdAttribute(notation);
		}

		// Token: 0x0600371B RID: 14107 RVA: 0x00137F28 File Offset: 0x00136128
		private void PreprocessParticle(XmlSchemaParticle particle)
		{
			if (particle is XmlSchemaAll)
			{
				if (particle.MinOccurs != 0m && particle.MinOccurs != 1m)
				{
					particle.MinOccurs = 1m;
					base.SendValidationEvent("'all' must have 'minOccurs' value of 0 or 1.", particle);
				}
				if (particle.MaxOccurs != 1m)
				{
					particle.MaxOccurs = 1m;
					base.SendValidationEvent("'all' must have {max occurs}=1.", particle);
				}
				XmlSchemaObjectCollection items = ((XmlSchemaAll)particle).Items;
				for (int i = 0; i < items.Count; i++)
				{
					XmlSchemaElement xmlSchemaElement = (XmlSchemaElement)items[i];
					if (xmlSchemaElement.MaxOccurs != 0m && xmlSchemaElement.MaxOccurs != 1m)
					{
						xmlSchemaElement.MaxOccurs = 1m;
						base.SendValidationEvent("The {max occurs} of all the particles in the {particles} of an all group must be 0 or 1.", xmlSchemaElement);
					}
					this.SetParent(xmlSchemaElement, particle);
					this.PreprocessLocalElement(xmlSchemaElement);
				}
			}
			else
			{
				if (particle.MinOccurs > particle.MaxOccurs)
				{
					particle.MinOccurs = particle.MaxOccurs;
					base.SendValidationEvent("minOccurs value cannot be greater than maxOccurs value.", particle);
				}
				if (particle is XmlSchemaChoice)
				{
					XmlSchemaObjectCollection items = ((XmlSchemaChoice)particle).Items;
					for (int j = 0; j < items.Count; j++)
					{
						this.SetParent(items[j], particle);
						XmlSchemaElement xmlSchemaElement2 = items[j] as XmlSchemaElement;
						if (xmlSchemaElement2 != null)
						{
							this.PreprocessLocalElement(xmlSchemaElement2);
						}
						else
						{
							this.PreprocessParticle((XmlSchemaParticle)items[j]);
						}
					}
				}
				else if (particle is XmlSchemaSequence)
				{
					XmlSchemaObjectCollection items = ((XmlSchemaSequence)particle).Items;
					for (int k = 0; k < items.Count; k++)
					{
						this.SetParent(items[k], particle);
						XmlSchemaElement xmlSchemaElement3 = items[k] as XmlSchemaElement;
						if (xmlSchemaElement3 != null)
						{
							this.PreprocessLocalElement(xmlSchemaElement3);
						}
						else
						{
							this.PreprocessParticle((XmlSchemaParticle)items[k]);
						}
					}
				}
				else if (particle is XmlSchemaGroupRef)
				{
					XmlSchemaGroupRef xmlSchemaGroupRef = (XmlSchemaGroupRef)particle;
					if (xmlSchemaGroupRef.RefName.IsEmpty)
					{
						base.SendValidationEvent("The '{0}' attribute is either invalid or missing.", "ref", xmlSchemaGroupRef);
					}
					else
					{
						this.ValidateQNameAttribute(xmlSchemaGroupRef, "ref", xmlSchemaGroupRef.RefName);
					}
				}
				else if (particle is XmlSchemaAny)
				{
					try
					{
						((XmlSchemaAny)particle).BuildNamespaceList(this.targetNamespace);
					}
					catch (FormatException ex)
					{
						base.SendValidationEvent("The value of the namespace attribute of the element or attribute wildcard is invalid - {0}", new string[]
						{
							ex.Message
						}, ex, particle);
					}
				}
			}
			this.PreprocessAnnotation(particle);
			this.ValidateIdAttribute(particle);
		}

		// Token: 0x0600371C RID: 14108 RVA: 0x001381C0 File Offset: 0x001363C0
		private void PreprocessAttributes(XmlSchemaObjectCollection attributes, XmlSchemaAnyAttribute anyAttribute, XmlSchemaObject parent)
		{
			for (int i = 0; i < attributes.Count; i++)
			{
				this.SetParent(attributes[i], parent);
				XmlSchemaAttribute xmlSchemaAttribute = attributes[i] as XmlSchemaAttribute;
				if (xmlSchemaAttribute != null)
				{
					this.PreprocessLocalAttribute(xmlSchemaAttribute);
				}
				else
				{
					XmlSchemaAttributeGroupRef xmlSchemaAttributeGroupRef = (XmlSchemaAttributeGroupRef)attributes[i];
					if (xmlSchemaAttributeGroupRef.RefName.IsEmpty)
					{
						base.SendValidationEvent("The '{0}' attribute is either invalid or missing.", "ref", xmlSchemaAttributeGroupRef);
					}
					else
					{
						this.ValidateQNameAttribute(xmlSchemaAttributeGroupRef, "ref", xmlSchemaAttributeGroupRef.RefName);
					}
					this.PreprocessAnnotation(attributes[i]);
					this.ValidateIdAttribute(attributes[i]);
				}
			}
			if (anyAttribute != null)
			{
				try
				{
					this.SetParent(anyAttribute, parent);
					this.PreprocessAnnotation(anyAttribute);
					anyAttribute.BuildNamespaceList(this.targetNamespace);
				}
				catch (FormatException ex)
				{
					base.SendValidationEvent("The value of the namespace attribute of the element or attribute wildcard is invalid - {0}", new string[]
					{
						ex.Message
					}, ex, anyAttribute);
				}
				this.ValidateIdAttribute(anyAttribute);
			}
		}

		// Token: 0x0600371D RID: 14109 RVA: 0x001382BC File Offset: 0x001364BC
		private void ValidateIdAttribute(XmlSchemaObject xso)
		{
			if (xso.IdAttribute != null)
			{
				try
				{
					xso.IdAttribute = base.NameTable.Add(XmlConvert.VerifyNCName(xso.IdAttribute));
				}
				catch (XmlException ex)
				{
					base.SendValidationEvent("Invalid 'id' attribute value: {0}", new string[]
					{
						ex.Message
					}, ex, xso);
					return;
				}
				catch (ArgumentNullException)
				{
					base.SendValidationEvent("Invalid 'id' attribute value: {0}", Res.GetString("Value cannot be null."), xso);
					return;
				}
				try
				{
					this.currentSchema.Ids.Add(xso.IdAttribute, xso);
				}
				catch (ArgumentException)
				{
					base.SendValidationEvent("Duplicate ID attribute.", xso);
				}
			}
		}

		// Token: 0x0600371E RID: 14110 RVA: 0x00138378 File Offset: 0x00136578
		private void ValidateNameAttribute(XmlSchemaObject xso)
		{
			string text = xso.NameAttribute;
			if (text == null || text.Length == 0)
			{
				base.SendValidationEvent("Invalid 'name' attribute value '{0}': '{1}'.", null, Res.GetString("Value cannot be null."), xso);
			}
			text = XmlComplianceUtil.NonCDataNormalize(text);
			int num = ValidateNames.ParseNCName(text, 0);
			if (num != text.Length)
			{
				string[] array = XmlException.BuildCharExceptionArgs(text, num);
				string @string = Res.GetString("The '{0}' character, hexadecimal value {1}, at position {2} within the name, cannot be included in a name.", new object[]
				{
					array[0],
					array[1],
					num
				});
				base.SendValidationEvent("Invalid 'name' attribute value '{0}': '{1}'.", text, @string, xso);
				return;
			}
			xso.NameAttribute = base.NameTable.Add(text);
		}

		// Token: 0x0600371F RID: 14111 RVA: 0x00138418 File Offset: 0x00136618
		private void ValidateQNameAttribute(XmlSchemaObject xso, string attributeName, XmlQualifiedName value)
		{
			try
			{
				value.Verify();
				value.Atomize(base.NameTable);
				if (this.currentSchema.IsChameleon && value.Namespace.Length == 0)
				{
					value.SetNamespace(this.currentSchema.TargetNamespace);
				}
				if (this.referenceNamespaces[value.Namespace] == null)
				{
					base.SendValidationEvent("Namespace '{0}' is not available to be referenced in this schema.", value.Namespace, xso, XmlSeverityType.Warning);
				}
			}
			catch (FormatException ex)
			{
				base.SendValidationEvent("Invalid '{0}' attribute: '{1}'.", new string[]
				{
					attributeName,
					ex.Message
				}, ex, xso);
			}
			catch (XmlException ex2)
			{
				base.SendValidationEvent("Invalid '{0}' attribute: '{1}'.", new string[]
				{
					attributeName,
					ex2.Message
				}, ex2, xso);
			}
		}

		// Token: 0x06003720 RID: 14112 RVA: 0x001384EC File Offset: 0x001366EC
		private Uri ResolveSchemaLocationUri(XmlSchema enclosingSchema, string location)
		{
			if (location.Length == 0)
			{
				return null;
			}
			return this.xmlResolver.ResolveUri(enclosingSchema.BaseUri, location);
		}

		// Token: 0x06003721 RID: 14113 RVA: 0x0013850A File Offset: 0x0013670A
		private object GetSchemaEntity(Uri ruri)
		{
			return this.xmlResolver.GetEntity(ruri, null, null);
		}

		// Token: 0x06003722 RID: 14114 RVA: 0x0013851C File Offset: 0x0013671C
		private XmlSchema GetChameleonSchema(string targetNamespace, XmlSchema schema)
		{
			ChameleonKey key = new ChameleonKey(targetNamespace, schema);
			XmlSchema xmlSchema = (XmlSchema)this.chameleonSchemas[key];
			if (xmlSchema == null)
			{
				xmlSchema = schema.DeepClone();
				xmlSchema.IsChameleon = true;
				xmlSchema.TargetNamespace = targetNamespace;
				this.chameleonSchemas.Add(key, xmlSchema);
				xmlSchema.SourceUri = schema.SourceUri;
				schema.IsProcessing = false;
			}
			return xmlSchema;
		}

		// Token: 0x06003723 RID: 14115 RVA: 0x0013857C File Offset: 0x0013677C
		private void SetParent(XmlSchemaObject child, XmlSchemaObject parent)
		{
			child.Parent = parent;
		}

		// Token: 0x06003724 RID: 14116 RVA: 0x00138588 File Offset: 0x00136788
		private void PreprocessAnnotation(XmlSchemaObject schemaObject)
		{
			if (schemaObject is XmlSchemaAnnotated)
			{
				XmlSchemaAnnotation annotation = (schemaObject as XmlSchemaAnnotated).Annotation;
				if (annotation != null)
				{
					this.PreprocessAnnotation(annotation);
					annotation.Parent = schemaObject;
				}
			}
		}

		// Token: 0x06003725 RID: 14117 RVA: 0x001385BC File Offset: 0x001367BC
		private void PreprocessAnnotation(XmlSchemaAnnotation annotation)
		{
			this.ValidateIdAttribute(annotation);
			for (int i = 0; i < annotation.Items.Count; i++)
			{
				annotation.Items[i].Parent = annotation;
			}
		}

		// Token: 0x04002859 RID: 10329
		private string Xmlns;

		// Token: 0x0400285A RID: 10330
		private string NsXsi;

		// Token: 0x0400285B RID: 10331
		private string targetNamespace;

		// Token: 0x0400285C RID: 10332
		private XmlSchema rootSchema;

		// Token: 0x0400285D RID: 10333
		private XmlSchema currentSchema;

		// Token: 0x0400285E RID: 10334
		private XmlSchemaForm elementFormDefault;

		// Token: 0x0400285F RID: 10335
		private XmlSchemaForm attributeFormDefault;

		// Token: 0x04002860 RID: 10336
		private XmlSchemaDerivationMethod blockDefault;

		// Token: 0x04002861 RID: 10337
		private XmlSchemaDerivationMethod finalDefault;

		// Token: 0x04002862 RID: 10338
		private Hashtable schemaLocations;

		// Token: 0x04002863 RID: 10339
		private Hashtable chameleonSchemas;

		// Token: 0x04002864 RID: 10340
		private Hashtable referenceNamespaces;

		// Token: 0x04002865 RID: 10341
		private Hashtable processedExternals;

		// Token: 0x04002866 RID: 10342
		private SortedList lockList;

		// Token: 0x04002867 RID: 10343
		private XmlReaderSettings readerSettings;

		// Token: 0x04002868 RID: 10344
		private XmlSchema rootSchemaForRedefine;

		// Token: 0x04002869 RID: 10345
		private ArrayList redefinedList;

		// Token: 0x0400286A RID: 10346
		private static XmlSchema builtInSchemaForXmlNS;

		// Token: 0x0400286B RID: 10347
		private const XmlSchemaDerivationMethod schemaBlockDefaultAllowed = XmlSchemaDerivationMethod.Substitution | XmlSchemaDerivationMethod.Extension | XmlSchemaDerivationMethod.Restriction;

		// Token: 0x0400286C RID: 10348
		private const XmlSchemaDerivationMethod schemaFinalDefaultAllowed = XmlSchemaDerivationMethod.Extension | XmlSchemaDerivationMethod.Restriction | XmlSchemaDerivationMethod.List | XmlSchemaDerivationMethod.Union;

		// Token: 0x0400286D RID: 10349
		private const XmlSchemaDerivationMethod elementBlockAllowed = XmlSchemaDerivationMethod.Substitution | XmlSchemaDerivationMethod.Extension | XmlSchemaDerivationMethod.Restriction;

		// Token: 0x0400286E RID: 10350
		private const XmlSchemaDerivationMethod elementFinalAllowed = XmlSchemaDerivationMethod.Extension | XmlSchemaDerivationMethod.Restriction;

		// Token: 0x0400286F RID: 10351
		private const XmlSchemaDerivationMethod simpleTypeFinalAllowed = XmlSchemaDerivationMethod.Extension | XmlSchemaDerivationMethod.Restriction | XmlSchemaDerivationMethod.List | XmlSchemaDerivationMethod.Union;

		// Token: 0x04002870 RID: 10352
		private const XmlSchemaDerivationMethod complexTypeBlockAllowed = XmlSchemaDerivationMethod.Extension | XmlSchemaDerivationMethod.Restriction;

		// Token: 0x04002871 RID: 10353
		private const XmlSchemaDerivationMethod complexTypeFinalAllowed = XmlSchemaDerivationMethod.Extension | XmlSchemaDerivationMethod.Restriction;

		// Token: 0x04002872 RID: 10354
		private XmlResolver xmlResolver;
	}
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;

namespace System.Xml.Schema
{
	/// <summary>Contains a cache of XML Schema definition language (XSD) schemas.</summary>
	// Token: 0x020005D9 RID: 1497
	public class XmlSchemaSet
	{
		// Token: 0x17000BA9 RID: 2985
		// (get) Token: 0x06003BE4 RID: 15332 RVA: 0x0014F00C File Offset: 0x0014D20C
		internal object InternalSyncObject
		{
			get
			{
				if (this.internalSyncObject == null)
				{
					object value = new object();
					Interlocked.CompareExchange<object>(ref this.internalSyncObject, value, null);
				}
				return this.internalSyncObject;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Schema.XmlSchemaSet" /> class.</summary>
		// Token: 0x06003BE5 RID: 15333 RVA: 0x0014F03B File Offset: 0x0014D23B
		public XmlSchemaSet() : this(new NameTable())
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Schema.XmlSchemaSet" /> class with the specified <see cref="T:System.Xml.XmlNameTable" />.</summary>
		/// <param name="nameTable">The <see cref="T:System.Xml.XmlNameTable" /> object to use.</param>
		/// <exception cref="T:System.ArgumentNullException">The <see cref="T:System.Xml.XmlNameTable" /> object passed as a parameter is <see langword="null" />.</exception>
		// Token: 0x06003BE6 RID: 15334 RVA: 0x0014F048 File Offset: 0x0014D248
		public XmlSchemaSet(XmlNameTable nameTable)
		{
			if (nameTable == null)
			{
				throw new ArgumentNullException("nameTable");
			}
			this.nameTable = nameTable;
			this.schemas = new SortedList();
			this.schemaLocations = new Hashtable();
			this.chameleonSchemas = new Hashtable();
			this.targetNamespaces = new Hashtable();
			this.internalEventHandler = new ValidationEventHandler(this.InternalValidationCallback);
			this.eventHandler = this.internalEventHandler;
			this.readerSettings = new XmlReaderSettings();
			if (this.readerSettings.GetXmlResolver() == null)
			{
				this.readerSettings.XmlResolver = new XmlUrlResolver();
				this.readerSettings.IsXmlResolverSet = false;
			}
			this.readerSettings.NameTable = nameTable;
			this.readerSettings.DtdProcessing = DtdProcessing.Prohibit;
			this.compilationSettings = new XmlSchemaCompilationSettings();
			this.cachedCompiledInfo = new SchemaInfo();
			this.compileAll = true;
		}

		/// <summary>Gets the default <see cref="T:System.Xml.XmlNameTable" /> used by the <see cref="T:System.Xml.Schema.XmlSchemaSet" /> when loading new XML Schema definition language (XSD) schemas.</summary>
		/// <returns>A table of atomized string objects.</returns>
		// Token: 0x17000BAA RID: 2986
		// (get) Token: 0x06003BE7 RID: 15335 RVA: 0x0014F123 File Offset: 0x0014D323
		public XmlNameTable NameTable
		{
			get
			{
				return this.nameTable;
			}
		}

		/// <summary>Specifies an event handler for receiving information about XML Schema definition language (XSD) schema validation errors.</summary>
		// Token: 0x14000010 RID: 16
		// (add) Token: 0x06003BE8 RID: 15336 RVA: 0x0014F12C File Offset: 0x0014D32C
		// (remove) Token: 0x06003BE9 RID: 15337 RVA: 0x0014F180 File Offset: 0x0014D380
		public event ValidationEventHandler ValidationEventHandler
		{
			add
			{
				this.eventHandler = (ValidationEventHandler)Delegate.Remove(this.eventHandler, this.internalEventHandler);
				this.eventHandler = (ValidationEventHandler)Delegate.Combine(this.eventHandler, value);
				if (this.eventHandler == null)
				{
					this.eventHandler = this.internalEventHandler;
				}
			}
			remove
			{
				this.eventHandler = (ValidationEventHandler)Delegate.Remove(this.eventHandler, value);
				if (this.eventHandler == null)
				{
					this.eventHandler = this.internalEventHandler;
				}
			}
		}

		/// <summary>Gets a value that indicates whether the XML Schema definition language (XSD) schemas in the <see cref="T:System.Xml.Schema.XmlSchemaSet" /> have been compiled.</summary>
		/// <returns>
		///     <see langword="true" /> if the schemas in the <see cref="T:System.Xml.Schema.XmlSchemaSet" /> have been compiled since the last time a schema was added or removed from the <see cref="T:System.Xml.Schema.XmlSchemaSet" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000BAB RID: 2987
		// (get) Token: 0x06003BEA RID: 15338 RVA: 0x0014F1AD File Offset: 0x0014D3AD
		public bool IsCompiled
		{
			get
			{
				return this.isCompiled;
			}
		}

		/// <summary>Sets the <see cref="T:System.Xml.XmlResolver" /> used to resolve namespaces or locations referenced in include and import elements of a schema.</summary>
		/// <returns>The <see cref="T:System.Xml.XmlResolver" /> used to resolve namespaces or locations referenced in include and import elements of a schema.</returns>
		// Token: 0x17000BAC RID: 2988
		// (set) Token: 0x06003BEB RID: 15339 RVA: 0x0014F1B5 File Offset: 0x0014D3B5
		public XmlResolver XmlResolver
		{
			set
			{
				this.readerSettings.XmlResolver = value;
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Xml.Schema.XmlSchemaCompilationSettings" /> for the <see cref="T:System.Xml.Schema.XmlSchemaSet" />.</summary>
		/// <returns>The <see cref="T:System.Xml.Schema.XmlSchemaCompilationSettings" /> for the <see cref="T:System.Xml.Schema.XmlSchemaSet" />. The default is an <see cref="T:System.Xml.Schema.XmlSchemaCompilationSettings" /> instance with the <see cref="P:System.Xml.Schema.XmlSchemaCompilationSettings.EnableUpaCheck" /> property set to <see langword="true" />.</returns>
		// Token: 0x17000BAD RID: 2989
		// (get) Token: 0x06003BEC RID: 15340 RVA: 0x0014F1C3 File Offset: 0x0014D3C3
		// (set) Token: 0x06003BED RID: 15341 RVA: 0x0014F1CB File Offset: 0x0014D3CB
		public XmlSchemaCompilationSettings CompilationSettings
		{
			get
			{
				return this.compilationSettings;
			}
			set
			{
				this.compilationSettings = value;
			}
		}

		/// <summary>Gets the number of logical XML Schema definition language (XSD) schemas in the <see cref="T:System.Xml.Schema.XmlSchemaSet" />.</summary>
		/// <returns>The number of logical schemas in the <see cref="T:System.Xml.Schema.XmlSchemaSet" />.</returns>
		// Token: 0x17000BAE RID: 2990
		// (get) Token: 0x06003BEE RID: 15342 RVA: 0x0014F1D4 File Offset: 0x0014D3D4
		public int Count
		{
			get
			{
				return this.schemas.Count;
			}
		}

		/// <summary>Gets all the global elements in all the XML Schema definition language (XSD) schemas in the <see cref="T:System.Xml.Schema.XmlSchemaSet" />.</summary>
		/// <returns>The collection of global elements.</returns>
		// Token: 0x17000BAF RID: 2991
		// (get) Token: 0x06003BEF RID: 15343 RVA: 0x0014F1E1 File Offset: 0x0014D3E1
		public XmlSchemaObjectTable GlobalElements
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

		/// <summary>Gets all the global attributes in all the XML Schema definition language (XSD) schemas in the <see cref="T:System.Xml.Schema.XmlSchemaSet" />.</summary>
		/// <returns>The collection of global attributes.</returns>
		// Token: 0x17000BB0 RID: 2992
		// (get) Token: 0x06003BF0 RID: 15344 RVA: 0x0014F1FC File Offset: 0x0014D3FC
		public XmlSchemaObjectTable GlobalAttributes
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

		/// <summary>Gets all of the global simple and complex types in all the XML Schema definition language (XSD) schemas in the <see cref="T:System.Xml.Schema.XmlSchemaSet" />.</summary>
		/// <returns>The collection of global simple and complex types.</returns>
		// Token: 0x17000BB1 RID: 2993
		// (get) Token: 0x06003BF1 RID: 15345 RVA: 0x0014F217 File Offset: 0x0014D417
		public XmlSchemaObjectTable GlobalTypes
		{
			get
			{
				if (this.schemaTypes == null)
				{
					this.schemaTypes = new XmlSchemaObjectTable();
				}
				return this.schemaTypes;
			}
		}

		// Token: 0x17000BB2 RID: 2994
		// (get) Token: 0x06003BF2 RID: 15346 RVA: 0x0014F232 File Offset: 0x0014D432
		internal XmlSchemaObjectTable SubstitutionGroups
		{
			get
			{
				if (this.substitutionGroups == null)
				{
					this.substitutionGroups = new XmlSchemaObjectTable();
				}
				return this.substitutionGroups;
			}
		}

		// Token: 0x17000BB3 RID: 2995
		// (get) Token: 0x06003BF3 RID: 15347 RVA: 0x0014F24D File Offset: 0x0014D44D
		internal Hashtable SchemaLocations
		{
			get
			{
				return this.schemaLocations;
			}
		}

		// Token: 0x17000BB4 RID: 2996
		// (get) Token: 0x06003BF4 RID: 15348 RVA: 0x0014F255 File Offset: 0x0014D455
		internal XmlSchemaObjectTable TypeExtensions
		{
			get
			{
				if (this.typeExtensions == null)
				{
					this.typeExtensions = new XmlSchemaObjectTable();
				}
				return this.typeExtensions;
			}
		}

		/// <summary>Adds the XML Schema definition language (XSD) schema at the URL specified to the <see cref="T:System.Xml.Schema.XmlSchemaSet" />.</summary>
		/// <param name="targetNamespace">The schema targetNamespace property, or <see langword="null" /> to use the targetNamespace specified in the schema.</param>
		/// <param name="schemaUri">The URL that specifies the schema to load.</param>
		/// <returns>An <see cref="T:System.Xml.Schema.XmlSchema" /> object if the schema is valid. If the schema is not valid and a <see cref="T:System.Xml.Schema.ValidationEventHandler" /> is specified, then <see langword="null" /> is returned and the appropriate validation event is raised. Otherwise, an <see cref="T:System.Xml.Schema.XmlSchemaException" /> is thrown.</returns>
		/// <exception cref="T:System.Xml.Schema.XmlSchemaException">The schema is not valid.</exception>
		/// <exception cref="T:System.ArgumentNullException">The URL passed as a parameter is <see langword="null" /> or <see cref="F:System.String.Empty" />.</exception>
		// Token: 0x06003BF5 RID: 15349 RVA: 0x0014F270 File Offset: 0x0014D470
		public XmlSchema Add(string targetNamespace, string schemaUri)
		{
			if (schemaUri == null || schemaUri.Length == 0)
			{
				throw new ArgumentNullException("schemaUri");
			}
			if (targetNamespace != null)
			{
				targetNamespace = XmlComplianceUtil.CDataNormalize(targetNamespace);
			}
			XmlSchema result = null;
			object obj = this.InternalSyncObject;
			lock (obj)
			{
				XmlResolver xmlResolver = this.readerSettings.GetXmlResolver();
				if (xmlResolver == null)
				{
					xmlResolver = new XmlUrlResolver();
				}
				Uri schemaUri2 = xmlResolver.ResolveUri(null, schemaUri);
				if (this.IsSchemaLoaded(schemaUri2, targetNamespace, out result))
				{
					return result;
				}
				XmlReader xmlReader = XmlReader.Create(schemaUri, this.readerSettings);
				try
				{
					result = this.Add(targetNamespace, this.ParseSchema(targetNamespace, xmlReader));
					while (xmlReader.Read())
					{
					}
				}
				finally
				{
					xmlReader.Close();
				}
			}
			return result;
		}

		/// <summary>Adds the XML Schema definition language (XSD) schema contained in the <see cref="T:System.Xml.XmlReader" /> to the <see cref="T:System.Xml.Schema.XmlSchemaSet" />.</summary>
		/// <param name="targetNamespace">The schema targetNamespace property, or <see langword="null" /> to use the targetNamespace specified in the schema.</param>
		/// <param name="schemaDocument">The <see cref="T:System.Xml.XmlReader" /> object.</param>
		/// <returns>An <see cref="T:System.Xml.Schema.XmlSchema" /> object if the schema is valid. If the schema is not valid and a <see cref="T:System.Xml.Schema.ValidationEventHandler" /> is specified, then <see langword="null" /> is returned and the appropriate validation event is raised. Otherwise, an <see cref="T:System.Xml.Schema.XmlSchemaException" /> is thrown.</returns>
		/// <exception cref="T:System.Xml.Schema.XmlSchemaException">The schema is not valid.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <see cref="T:System.Xml.XmlReader" /> object passed as a parameter is <see langword="null" />.</exception>
		// Token: 0x06003BF6 RID: 15350 RVA: 0x0014F340 File Offset: 0x0014D540
		public XmlSchema Add(string targetNamespace, XmlReader schemaDocument)
		{
			if (schemaDocument == null)
			{
				throw new ArgumentNullException("schemaDocument");
			}
			if (targetNamespace != null)
			{
				targetNamespace = XmlComplianceUtil.CDataNormalize(targetNamespace);
			}
			object obj = this.InternalSyncObject;
			XmlSchema result;
			lock (obj)
			{
				XmlSchema xmlSchema = null;
				Uri schemaUri = new Uri(schemaDocument.BaseURI, UriKind.RelativeOrAbsolute);
				if (this.IsSchemaLoaded(schemaUri, targetNamespace, out xmlSchema))
				{
					result = xmlSchema;
				}
				else
				{
					DtdProcessing dtdProcessing = this.readerSettings.DtdProcessing;
					this.SetDtdProcessing(schemaDocument);
					xmlSchema = this.Add(targetNamespace, this.ParseSchema(targetNamespace, schemaDocument));
					this.readerSettings.DtdProcessing = dtdProcessing;
					result = xmlSchema;
				}
			}
			return result;
		}

		/// <summary>Adds all the XML Schema definition language (XSD) schemas in the given <see cref="T:System.Xml.Schema.XmlSchemaSet" /> to the <see cref="T:System.Xml.Schema.XmlSchemaSet" />.</summary>
		/// <param name="schemas">The <see cref="T:System.Xml.Schema.XmlSchemaSet" /> object.</param>
		/// <exception cref="T:System.Xml.Schema.XmlSchemaException">A schema in the <see cref="T:System.Xml.Schema.XmlSchemaSet" /> is not valid.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <see cref="T:System.Xml.Schema.XmlSchemaSet" /> object passed as a parameter is <see langword="null" />.</exception>
		// Token: 0x06003BF7 RID: 15351 RVA: 0x0014F3EC File Offset: 0x0014D5EC
		public void Add(XmlSchemaSet schemas)
		{
			if (schemas == null)
			{
				throw new ArgumentNullException("schemas");
			}
			if (this == schemas)
			{
				return;
			}
			bool flag = false;
			bool flag2 = false;
			try
			{
				for (;;)
				{
					Monitor.TryEnter(this.InternalSyncObject, ref flag);
					if (flag)
					{
						Monitor.TryEnter(schemas.InternalSyncObject, ref flag2);
						if (flag2)
						{
							break;
						}
						Monitor.Exit(this.InternalSyncObject);
						flag = false;
						Thread.Yield();
					}
				}
				if (schemas.IsCompiled)
				{
					this.CopyFromCompiledSet(schemas);
				}
				else
				{
					bool flag3 = false;
					foreach (object obj in schemas.SortedSchemas.Values)
					{
						XmlSchema xmlSchema = (XmlSchema)obj;
						string text = xmlSchema.TargetNamespace;
						if (text == null)
						{
							text = string.Empty;
						}
						if (!this.schemas.ContainsKey(xmlSchema.SchemaId) && this.FindSchemaByNSAndUrl(xmlSchema.BaseUri, text, null) == null && this.Add(xmlSchema.TargetNamespace, xmlSchema) == null)
						{
							flag3 = true;
							break;
						}
					}
					if (flag3)
					{
						foreach (object obj2 in schemas.SortedSchemas.Values)
						{
							XmlSchema xmlSchema2 = (XmlSchema)obj2;
							this.schemas.Remove(xmlSchema2.SchemaId);
							this.schemaLocations.Remove(xmlSchema2.BaseUri);
						}
					}
				}
			}
			finally
			{
				if (flag)
				{
					Monitor.Exit(this.InternalSyncObject);
				}
				if (flag2)
				{
					Monitor.Exit(schemas.InternalSyncObject);
				}
			}
		}

		/// <summary>Adds the given <see cref="T:System.Xml.Schema.XmlSchema" /> to the <see cref="T:System.Xml.Schema.XmlSchemaSet" />.</summary>
		/// <param name="schema">The <see cref="T:System.Xml.Schema.XmlSchema" /> object to add to the <see cref="T:System.Xml.Schema.XmlSchemaSet" />.</param>
		/// <returns>An <see cref="T:System.Xml.Schema.XmlSchema" /> object if the schema is valid. If the schema is not valid and a <see cref="T:System.Xml.Schema.ValidationEventHandler" /> is specified, then <see langword="null" /> is returned and the appropriate validation event is raised. Otherwise, an <see cref="T:System.Xml.Schema.XmlSchemaException" /> is thrown.</returns>
		/// <exception cref="T:System.Xml.Schema.XmlSchemaException">The schema is not valid.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <see cref="T:System.Xml.Schema.XmlSchema" /> object passed as a parameter is <see langword="null" />.</exception>
		// Token: 0x06003BF8 RID: 15352 RVA: 0x0014F5C8 File Offset: 0x0014D7C8
		public XmlSchema Add(XmlSchema schema)
		{
			if (schema == null)
			{
				throw new ArgumentNullException("schema");
			}
			object obj = this.InternalSyncObject;
			XmlSchema result;
			lock (obj)
			{
				if (this.schemas.ContainsKey(schema.SchemaId))
				{
					result = schema;
				}
				else
				{
					result = this.Add(schema.TargetNamespace, schema);
				}
			}
			return result;
		}

		/// <summary>Removes the specified XML Schema definition language (XSD) schema from the <see cref="T:System.Xml.Schema.XmlSchemaSet" />.</summary>
		/// <param name="schema">The <see cref="T:System.Xml.Schema.XmlSchema" /> object to remove from the <see cref="T:System.Xml.Schema.XmlSchemaSet" />.</param>
		/// <returns>The <see cref="T:System.Xml.Schema.XmlSchema" /> object removed from the <see cref="T:System.Xml.Schema.XmlSchemaSet" /> or <see langword="null" /> if the schema was not found in the <see cref="T:System.Xml.Schema.XmlSchemaSet" />.</returns>
		/// <exception cref="T:System.Xml.Schema.XmlSchemaException">The schema is not a valid schema.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <see cref="T:System.Xml.Schema.XmlSchema" /> passed as a parameter is <see langword="null" />.</exception>
		// Token: 0x06003BF9 RID: 15353 RVA: 0x0014F63C File Offset: 0x0014D83C
		public XmlSchema Remove(XmlSchema schema)
		{
			return this.Remove(schema, true);
		}

		/// <summary>Removes the specified XML Schema definition language (XSD) schema and all the schemas it imports from the <see cref="T:System.Xml.Schema.XmlSchemaSet" />.</summary>
		/// <param name="schemaToRemove">The <see cref="T:System.Xml.Schema.XmlSchema" /> object to remove from the <see cref="T:System.Xml.Schema.XmlSchemaSet" />.</param>
		/// <returns>
		///     <see langword="true" /> if the <see cref="T:System.Xml.Schema.XmlSchema" /> object and all its imports were successfully removed; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <see cref="T:System.Xml.Schema.XmlSchema" /> passed as a parameter is <see langword="null" />.</exception>
		// Token: 0x06003BFA RID: 15354 RVA: 0x0014F648 File Offset: 0x0014D848
		public bool RemoveRecursive(XmlSchema schemaToRemove)
		{
			if (schemaToRemove == null)
			{
				throw new ArgumentNullException("schemaToRemove");
			}
			if (!this.schemas.ContainsKey(schemaToRemove.SchemaId))
			{
				return false;
			}
			object obj = this.InternalSyncObject;
			lock (obj)
			{
				if (this.schemas.ContainsKey(schemaToRemove.SchemaId))
				{
					Hashtable hashtable = new Hashtable();
					hashtable.Add(this.GetTargetNamespace(schemaToRemove), schemaToRemove);
					for (int i = 0; i < schemaToRemove.ImportedNamespaces.Count; i++)
					{
						string text = (string)schemaToRemove.ImportedNamespaces[i];
						if (hashtable[text] == null)
						{
							hashtable.Add(text, text);
						}
					}
					ArrayList arrayList = new ArrayList();
					for (int j = 0; j < this.schemas.Count; j++)
					{
						XmlSchema xmlSchema = (XmlSchema)this.schemas.GetByIndex(j);
						if (xmlSchema != schemaToRemove && !schemaToRemove.ImportedSchemas.Contains(xmlSchema))
						{
							arrayList.Add(xmlSchema);
						}
					}
					for (int k = 0; k < arrayList.Count; k++)
					{
						XmlSchema xmlSchema = (XmlSchema)arrayList[k];
						if (xmlSchema.ImportedNamespaces.Count > 0)
						{
							foreach (object obj2 in hashtable.Keys)
							{
								string item = (string)obj2;
								if (xmlSchema.ImportedNamespaces.Contains(item))
								{
									this.SendValidationEvent(new XmlSchemaException("The schema could not be removed because other schemas in the set have dependencies on this schema or its imports.", string.Empty), XmlSeverityType.Warning);
									return false;
								}
							}
						}
					}
					this.Remove(schemaToRemove, true);
					for (int l = 0; l < schemaToRemove.ImportedSchemas.Count; l++)
					{
						XmlSchema schema = (XmlSchema)schemaToRemove.ImportedSchemas[l];
						this.Remove(schema, true);
					}
					return true;
				}
			}
			return false;
		}

		/// <summary>Indicates whether an XML Schema definition language (XSD) schema with the specified target namespace URI is in the <see cref="T:System.Xml.Schema.XmlSchemaSet" />.</summary>
		/// <param name="targetNamespace">The schema targetNamespace property.</param>
		/// <returns>
		///     <see langword="true" /> if a schema with the specified target namespace URI is in the <see cref="T:System.Xml.Schema.XmlSchemaSet" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x06003BFB RID: 15355 RVA: 0x0014F884 File Offset: 0x0014DA84
		public bool Contains(string targetNamespace)
		{
			if (targetNamespace == null)
			{
				targetNamespace = string.Empty;
			}
			return this.targetNamespaces[targetNamespace] != null;
		}

		/// <summary>Indicates whether the specified XML Schema definition language (XSD) <see cref="T:System.Xml.Schema.XmlSchema" /> object is in the <see cref="T:System.Xml.Schema.XmlSchemaSet" />.</summary>
		/// <param name="schema">The <see cref="T:System.Xml.Schema.XmlSchema" /> object.</param>
		/// <returns>
		///     <see langword="true" /> if the <see cref="T:System.Xml.Schema.XmlSchema" /> object is in the <see cref="T:System.Xml.Schema.XmlSchemaSet" />; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <see cref="T:System.Xml.Schema.XmlSchemaSet" /> passed as a parameter is <see langword="null" />.</exception>
		// Token: 0x06003BFC RID: 15356 RVA: 0x0014F89F File Offset: 0x0014DA9F
		public bool Contains(XmlSchema schema)
		{
			if (schema == null)
			{
				throw new ArgumentNullException("schema");
			}
			return this.schemas.ContainsValue(schema);
		}

		/// <summary>Compiles the XML Schema definition language (XSD) schemas added to the <see cref="T:System.Xml.Schema.XmlSchemaSet" /> into one logical schema.</summary>
		/// <exception cref="T:System.Xml.Schema.XmlSchemaException">An error occurred when validating and compiling the schemas in the <see cref="T:System.Xml.Schema.XmlSchemaSet" />.</exception>
		// Token: 0x06003BFD RID: 15357 RVA: 0x0014F8BC File Offset: 0x0014DABC
		public void Compile()
		{
			if (this.isCompiled)
			{
				return;
			}
			if (this.schemas.Count == 0)
			{
				this.ClearTables();
				this.cachedCompiledInfo = new SchemaInfo();
				this.isCompiled = true;
				this.compileAll = false;
				return;
			}
			object obj = this.InternalSyncObject;
			lock (obj)
			{
				if (!this.isCompiled)
				{
					Compiler compiler = new Compiler(this.nameTable, this.eventHandler, this.schemaForSchema, this.compilationSettings);
					SchemaInfo schemaInfo = new SchemaInfo();
					int i = 0;
					if (!this.compileAll)
					{
						compiler.ImportAllCompiledSchemas(this);
					}
					try
					{
						XmlSchema buildInSchema = Preprocessor.GetBuildInSchema();
						i = 0;
						while (i < this.schemas.Count)
						{
							XmlSchema xmlSchema = (XmlSchema)this.schemas.GetByIndex(i);
							Monitor.Enter(xmlSchema);
							if (!xmlSchema.IsPreprocessed)
							{
								this.SendValidationEvent(new XmlSchemaException("All schemas in the set should be successfully preprocessed prior to compilation.", string.Empty), XmlSeverityType.Error);
								this.isCompiled = false;
								return;
							}
							if (!xmlSchema.IsCompiledBySet)
							{
								goto IL_FD;
							}
							if (this.compileAll)
							{
								if (xmlSchema != buildInSchema)
								{
									goto IL_FD;
								}
								compiler.Prepare(xmlSchema, false);
							}
							IL_106:
							i++;
							continue;
							IL_FD:
							compiler.Prepare(xmlSchema, true);
							goto IL_106;
						}
						this.isCompiled = compiler.Execute(this, schemaInfo);
						if (this.isCompiled)
						{
							if (!this.compileAll)
							{
								schemaInfo.Add(this.cachedCompiledInfo, this.eventHandler);
							}
							this.compileAll = false;
							this.cachedCompiledInfo = schemaInfo;
						}
					}
					finally
					{
						if (i == this.schemas.Count)
						{
							i--;
						}
						for (int j = i; j >= 0; j--)
						{
							XmlSchema xmlSchema2 = (XmlSchema)this.schemas.GetByIndex(j);
							if (xmlSchema2 == Preprocessor.GetBuildInSchema())
							{
								Monitor.Exit(xmlSchema2);
							}
							else
							{
								xmlSchema2.IsCompiledBySet = this.isCompiled;
								Monitor.Exit(xmlSchema2);
							}
						}
					}
				}
			}
		}

		/// <summary>Reprocesses an XML Schema definition language (XSD) schema that already exists in the <see cref="T:System.Xml.Schema.XmlSchemaSet" />.</summary>
		/// <param name="schema">The schema to reprocess.</param>
		/// <returns>An <see cref="T:System.Xml.Schema.XmlSchema" /> object if the schema is a valid schema. If the schema is not valid and a <see cref="T:System.Xml.Schema.ValidationEventHandler" /> is specified, <see langword="null" /> is returned and the appropriate validation event is raised. Otherwise, an <see cref="T:System.Xml.Schema.XmlSchemaException" /> is thrown.</returns>
		/// <exception cref="T:System.Xml.Schema.XmlSchemaException">The schema is not valid.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <see cref="T:System.Xml.Schema.XmlSchema" /> object passed as a parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The <see cref="T:System.Xml.Schema.XmlSchema" /> object passed as a parameter does not already exist in the <see cref="T:System.Xml.Schema.XmlSchemaSet" />.</exception>
		// Token: 0x06003BFE RID: 15358 RVA: 0x0014FACC File Offset: 0x0014DCCC
		public XmlSchema Reprocess(XmlSchema schema)
		{
			if (schema == null)
			{
				throw new ArgumentNullException("schema");
			}
			if (!this.schemas.ContainsKey(schema.SchemaId))
			{
				throw new ArgumentException(Res.GetString("Schema does not exist in the set."), "schema");
			}
			XmlSchema xmlSchema = schema;
			object obj = this.InternalSyncObject;
			XmlSchema result;
			lock (obj)
			{
				this.RemoveSchemaFromGlobalTables(schema);
				this.RemoveSchemaFromCaches(schema);
				if (schema.BaseUri != null)
				{
					this.schemaLocations.Remove(schema.BaseUri);
				}
				string targetNamespace = this.GetTargetNamespace(schema);
				if (this.Schemas(targetNamespace).Count == 0)
				{
					this.targetNamespaces.Remove(targetNamespace);
				}
				this.isCompiled = false;
				this.compileAll = true;
				if (schema.ErrorCount != 0)
				{
					result = xmlSchema;
				}
				else if (this.PreprocessSchema(ref schema, schema.TargetNamespace))
				{
					if (this.targetNamespaces[targetNamespace] == null)
					{
						this.targetNamespaces.Add(targetNamespace, targetNamespace);
					}
					if (this.schemaForSchema == null && targetNamespace == "http://www.w3.org/2001/XMLSchema" && schema.SchemaTypes[DatatypeImplementation.QnAnyType] != null)
					{
						this.schemaForSchema = schema;
					}
					for (int i = 0; i < schema.ImportedSchemas.Count; i++)
					{
						XmlSchema xmlSchema2 = (XmlSchema)schema.ImportedSchemas[i];
						if (!this.schemas.ContainsKey(xmlSchema2.SchemaId))
						{
							this.schemas.Add(xmlSchema2.SchemaId, xmlSchema2);
						}
						targetNamespace = this.GetTargetNamespace(xmlSchema2);
						if (this.targetNamespaces[targetNamespace] == null)
						{
							this.targetNamespaces.Add(targetNamespace, targetNamespace);
						}
						if (this.schemaForSchema == null && targetNamespace == "http://www.w3.org/2001/XMLSchema" && schema.SchemaTypes[DatatypeImplementation.QnAnyType] != null)
						{
							this.schemaForSchema = schema;
						}
					}
					result = schema;
				}
				else
				{
					result = xmlSchema;
				}
			}
			return result;
		}

		/// <summary>Copies all the <see cref="T:System.Xml.Schema.XmlSchema" /> objects from the <see cref="T:System.Xml.Schema.XmlSchemaSet" /> to the given array, starting at the given index.</summary>
		/// <param name="schemas">The array to copy the objects to.</param>
		/// <param name="index">The index in the array where copying will begin.</param>
		// Token: 0x06003BFF RID: 15359 RVA: 0x0014FCDC File Offset: 0x0014DEDC
		public void CopyTo(XmlSchema[] schemas, int index)
		{
			if (schemas == null)
			{
				throw new ArgumentNullException("schemas");
			}
			if (index < 0 || index > schemas.Length - 1)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			this.schemas.Values.CopyTo(schemas, index);
		}

		/// <summary>Returns a collection of all the XML Schema definition language (XSD) schemas in the <see cref="T:System.Xml.Schema.XmlSchemaSet" />.</summary>
		/// <returns>An <see cref="T:System.Collections.ICollection" /> object containing all the schemas that have been added to the <see cref="T:System.Xml.Schema.XmlSchemaSet" />. If no schemas have been added to the <see cref="T:System.Xml.Schema.XmlSchemaSet" />, an empty <see cref="T:System.Collections.ICollection" /> object is returned.</returns>
		// Token: 0x06003C00 RID: 15360 RVA: 0x0014FD15 File Offset: 0x0014DF15
		public ICollection Schemas()
		{
			return this.schemas.Values;
		}

		/// <summary>Returns a collection of all the XML Schema definition language (XSD) schemas in the <see cref="T:System.Xml.Schema.XmlSchemaSet" /> that belong to the given namespace.</summary>
		/// <param name="targetNamespace">The schema targetNamespace property.</param>
		/// <returns>An <see cref="T:System.Collections.ICollection" /> object containing all the schemas that have been added to the <see cref="T:System.Xml.Schema.XmlSchemaSet" /> that belong to the given namespace. If no schemas have been added to the <see cref="T:System.Xml.Schema.XmlSchemaSet" />, an empty <see cref="T:System.Collections.ICollection" /> object is returned.</returns>
		// Token: 0x06003C01 RID: 15361 RVA: 0x0014FD24 File Offset: 0x0014DF24
		public ICollection Schemas(string targetNamespace)
		{
			ArrayList arrayList = new ArrayList();
			if (targetNamespace == null)
			{
				targetNamespace = string.Empty;
			}
			for (int i = 0; i < this.schemas.Count; i++)
			{
				XmlSchema xmlSchema = (XmlSchema)this.schemas.GetByIndex(i);
				if (this.GetTargetNamespace(xmlSchema) == targetNamespace)
				{
					arrayList.Add(xmlSchema);
				}
			}
			return arrayList;
		}

		// Token: 0x06003C02 RID: 15362 RVA: 0x0014FD81 File Offset: 0x0014DF81
		private XmlSchema Add(string targetNamespace, XmlSchema schema)
		{
			if (schema == null || schema.ErrorCount != 0)
			{
				return null;
			}
			if (this.PreprocessSchema(ref schema, targetNamespace))
			{
				this.AddSchemaToSet(schema);
				this.isCompiled = false;
				return schema;
			}
			return null;
		}

		// Token: 0x06003C03 RID: 15363 RVA: 0x0014FDAC File Offset: 0x0014DFAC
		internal void Add(string targetNamespace, XmlReader reader, Hashtable validatedNamespaces)
		{
			if (reader == null)
			{
				throw new ArgumentNullException("reader");
			}
			if (targetNamespace == null)
			{
				targetNamespace = string.Empty;
			}
			if (validatedNamespaces[targetNamespace] != null)
			{
				if (this.FindSchemaByNSAndUrl(new Uri(reader.BaseURI, UriKind.RelativeOrAbsolute), targetNamespace, null) != null)
				{
					return;
				}
				throw new XmlSchemaException("An element or attribute information item has already been validated from the '{0}' namespace. It is an error if 'xsi:schemaLocation', 'xsi:noNamespaceSchemaLocation', or an inline schema occurs for that namespace.", targetNamespace);
			}
			else
			{
				XmlSchema xmlSchema;
				if (this.IsSchemaLoaded(new Uri(reader.BaseURI, UriKind.RelativeOrAbsolute), targetNamespace, out xmlSchema))
				{
					return;
				}
				xmlSchema = this.ParseSchema(targetNamespace, reader);
				DictionaryEntry[] array = new DictionaryEntry[this.schemaLocations.Count];
				this.schemaLocations.CopyTo(array, 0);
				this.Add(targetNamespace, xmlSchema);
				if (xmlSchema.ImportedSchemas.Count > 0)
				{
					for (int i = 0; i < xmlSchema.ImportedSchemas.Count; i++)
					{
						XmlSchema xmlSchema2 = (XmlSchema)xmlSchema.ImportedSchemas[i];
						string text = xmlSchema2.TargetNamespace;
						if (text == null)
						{
							text = string.Empty;
						}
						if (validatedNamespaces[text] != null && this.FindSchemaByNSAndUrl(xmlSchema2.BaseUri, text, array) == null)
						{
							this.RemoveRecursive(xmlSchema);
							throw new XmlSchemaException("An element or attribute information item has already been validated from the '{0}' namespace. It is an error if 'xsi:schemaLocation', 'xsi:noNamespaceSchemaLocation', or an inline schema occurs for that namespace.", text);
						}
					}
				}
				return;
			}
		}

		// Token: 0x06003C04 RID: 15364 RVA: 0x0014FEBC File Offset: 0x0014E0BC
		internal XmlSchema FindSchemaByNSAndUrl(Uri schemaUri, string ns, DictionaryEntry[] locationsTable)
		{
			if (schemaUri == null || schemaUri.OriginalString.Length == 0)
			{
				return null;
			}
			XmlSchema xmlSchema = null;
			if (locationsTable == null)
			{
				xmlSchema = (XmlSchema)this.schemaLocations[schemaUri];
			}
			else
			{
				for (int i = 0; i < locationsTable.Length; i++)
				{
					if (schemaUri.Equals(locationsTable[i].Key))
					{
						xmlSchema = (XmlSchema)locationsTable[i].Value;
						break;
					}
				}
			}
			if (xmlSchema != null)
			{
				string a = (xmlSchema.TargetNamespace == null) ? string.Empty : xmlSchema.TargetNamespace;
				if (a == ns)
				{
					return xmlSchema;
				}
				if (a == string.Empty)
				{
					ChameleonKey key = new ChameleonKey(ns, xmlSchema);
					xmlSchema = (XmlSchema)this.chameleonSchemas[key];
				}
				else
				{
					xmlSchema = null;
				}
			}
			return xmlSchema;
		}

		// Token: 0x06003C05 RID: 15365 RVA: 0x0014FF80 File Offset: 0x0014E180
		private void SetDtdProcessing(XmlReader reader)
		{
			if (reader.Settings != null)
			{
				this.readerSettings.DtdProcessing = reader.Settings.DtdProcessing;
				return;
			}
			XmlTextReader xmlTextReader = reader as XmlTextReader;
			if (xmlTextReader != null)
			{
				this.readerSettings.DtdProcessing = xmlTextReader.DtdProcessing;
			}
		}

		// Token: 0x06003C06 RID: 15366 RVA: 0x0014FFC8 File Offset: 0x0014E1C8
		private void AddSchemaToSet(XmlSchema schema)
		{
			this.schemas.Add(schema.SchemaId, schema);
			string targetNamespace = this.GetTargetNamespace(schema);
			if (this.targetNamespaces[targetNamespace] == null)
			{
				this.targetNamespaces.Add(targetNamespace, targetNamespace);
			}
			if (this.schemaForSchema == null && targetNamespace == "http://www.w3.org/2001/XMLSchema" && schema.SchemaTypes[DatatypeImplementation.QnAnyType] != null)
			{
				this.schemaForSchema = schema;
			}
			for (int i = 0; i < schema.ImportedSchemas.Count; i++)
			{
				XmlSchema xmlSchema = (XmlSchema)schema.ImportedSchemas[i];
				if (!this.schemas.ContainsKey(xmlSchema.SchemaId))
				{
					this.schemas.Add(xmlSchema.SchemaId, xmlSchema);
				}
				targetNamespace = this.GetTargetNamespace(xmlSchema);
				if (this.targetNamespaces[targetNamespace] == null)
				{
					this.targetNamespaces.Add(targetNamespace, targetNamespace);
				}
				if (this.schemaForSchema == null && targetNamespace == "http://www.w3.org/2001/XMLSchema" && schema.SchemaTypes[DatatypeImplementation.QnAnyType] != null)
				{
					this.schemaForSchema = schema;
				}
			}
		}

		// Token: 0x06003C07 RID: 15367 RVA: 0x001500EC File Offset: 0x0014E2EC
		private void ProcessNewSubstitutionGroups(XmlSchemaObjectTable substitutionGroupsTable, bool resolve)
		{
			foreach (object obj in substitutionGroupsTable.Values)
			{
				XmlSchemaSubstitutionGroup xmlSchemaSubstitutionGroup = (XmlSchemaSubstitutionGroup)obj;
				if (resolve)
				{
					this.ResolveSubstitutionGroup(xmlSchemaSubstitutionGroup, substitutionGroupsTable);
				}
				XmlQualifiedName examplar = xmlSchemaSubstitutionGroup.Examplar;
				XmlSchemaSubstitutionGroup xmlSchemaSubstitutionGroup2 = (XmlSchemaSubstitutionGroup)this.substitutionGroups[examplar];
				if (xmlSchemaSubstitutionGroup2 != null)
				{
					for (int i = 0; i < xmlSchemaSubstitutionGroup.Members.Count; i++)
					{
						if (!xmlSchemaSubstitutionGroup2.Members.Contains(xmlSchemaSubstitutionGroup.Members[i]))
						{
							xmlSchemaSubstitutionGroup2.Members.Add(xmlSchemaSubstitutionGroup.Members[i]);
						}
					}
				}
				else
				{
					this.AddToTable(this.substitutionGroups, examplar, xmlSchemaSubstitutionGroup);
				}
			}
		}

		// Token: 0x06003C08 RID: 15368 RVA: 0x001501CC File Offset: 0x0014E3CC
		private void ResolveSubstitutionGroup(XmlSchemaSubstitutionGroup substitutionGroup, XmlSchemaObjectTable substTable)
		{
			List<XmlSchemaElement> list = null;
			XmlSchemaElement xmlSchemaElement = (XmlSchemaElement)this.elements[substitutionGroup.Examplar];
			if (substitutionGroup.Members.Contains(xmlSchemaElement))
			{
				return;
			}
			for (int i = 0; i < substitutionGroup.Members.Count; i++)
			{
				XmlSchemaElement xmlSchemaElement2 = (XmlSchemaElement)substitutionGroup.Members[i];
				XmlSchemaSubstitutionGroup xmlSchemaSubstitutionGroup = (XmlSchemaSubstitutionGroup)substTable[xmlSchemaElement2.QualifiedName];
				if (xmlSchemaSubstitutionGroup != null)
				{
					this.ResolveSubstitutionGroup(xmlSchemaSubstitutionGroup, substTable);
					for (int j = 0; j < xmlSchemaSubstitutionGroup.Members.Count; j++)
					{
						XmlSchemaElement xmlSchemaElement3 = (XmlSchemaElement)xmlSchemaSubstitutionGroup.Members[j];
						if (xmlSchemaElement3 != xmlSchemaElement2)
						{
							if (list == null)
							{
								list = new List<XmlSchemaElement>();
							}
							list.Add(xmlSchemaElement3);
						}
					}
				}
			}
			if (list != null)
			{
				for (int k = 0; k < list.Count; k++)
				{
					substitutionGroup.Members.Add(list[k]);
				}
			}
			substitutionGroup.Members.Add(xmlSchemaElement);
		}

		// Token: 0x06003C09 RID: 15369 RVA: 0x001502CC File Offset: 0x0014E4CC
		internal XmlSchema Remove(XmlSchema schema, bool forceCompile)
		{
			if (schema == null)
			{
				throw new ArgumentNullException("schema");
			}
			object obj = this.InternalSyncObject;
			lock (obj)
			{
				if (this.schemas.ContainsKey(schema.SchemaId))
				{
					if (forceCompile)
					{
						this.RemoveSchemaFromGlobalTables(schema);
						this.RemoveSchemaFromCaches(schema);
					}
					this.schemas.Remove(schema.SchemaId);
					if (schema.BaseUri != null)
					{
						this.schemaLocations.Remove(schema.BaseUri);
					}
					string targetNamespace = this.GetTargetNamespace(schema);
					if (this.Schemas(targetNamespace).Count == 0)
					{
						this.targetNamespaces.Remove(targetNamespace);
					}
					if (forceCompile)
					{
						this.isCompiled = false;
						this.compileAll = true;
					}
					return schema;
				}
			}
			return null;
		}

		// Token: 0x06003C0A RID: 15370 RVA: 0x001503AC File Offset: 0x0014E5AC
		private void ClearTables()
		{
			this.GlobalElements.Clear();
			this.GlobalAttributes.Clear();
			this.GlobalTypes.Clear();
			this.SubstitutionGroups.Clear();
			this.TypeExtensions.Clear();
		}

		// Token: 0x06003C0B RID: 15371 RVA: 0x001503E8 File Offset: 0x0014E5E8
		internal bool PreprocessSchema(ref XmlSchema schema, string targetNamespace)
		{
			Preprocessor preprocessor = new Preprocessor(this.nameTable, this.GetSchemaNames(this.nameTable), this.eventHandler, this.compilationSettings);
			preprocessor.XmlResolver = this.readerSettings.GetXmlResolver_CheckConfig();
			preprocessor.ReaderSettings = this.readerSettings;
			preprocessor.SchemaLocations = this.schemaLocations;
			preprocessor.ChameleonSchemas = this.chameleonSchemas;
			bool result = preprocessor.Execute(schema, targetNamespace, true);
			schema = preprocessor.RootSchema;
			return result;
		}

		// Token: 0x06003C0C RID: 15372 RVA: 0x00150460 File Offset: 0x0014E660
		internal XmlSchema ParseSchema(string targetNamespace, XmlReader reader)
		{
			XmlNameTable nt = reader.NameTable;
			SchemaNames schemaNames = this.GetSchemaNames(nt);
			Parser parser = new Parser(SchemaType.XSD, nt, schemaNames, this.eventHandler);
			parser.XmlResolver = this.readerSettings.GetXmlResolver_CheckConfig();
			try
			{
				parser.Parse(reader, targetNamespace);
			}
			catch (XmlSchemaException e)
			{
				this.SendValidationEvent(e, XmlSeverityType.Error);
				return null;
			}
			return parser.XmlSchema;
		}

		// Token: 0x06003C0D RID: 15373 RVA: 0x001504D0 File Offset: 0x0014E6D0
		internal void CopyFromCompiledSet(XmlSchemaSet otherSet)
		{
			SortedList sortedSchemas = otherSet.SortedSchemas;
			bool flag = this.schemas.Count == 0;
			ArrayList arrayList = new ArrayList();
			SchemaInfo schemaInfo = new SchemaInfo();
			for (int i = 0; i < sortedSchemas.Count; i++)
			{
				XmlSchema xmlSchema = (XmlSchema)sortedSchemas.GetByIndex(i);
				Uri baseUri = xmlSchema.BaseUri;
				if (this.schemas.ContainsKey(xmlSchema.SchemaId) || (baseUri != null && baseUri.OriginalString.Length != 0 && this.schemaLocations[baseUri] != null))
				{
					arrayList.Add(xmlSchema);
				}
				else
				{
					this.schemas.Add(xmlSchema.SchemaId, xmlSchema);
					if (baseUri != null && baseUri.OriginalString.Length != 0)
					{
						this.schemaLocations.Add(baseUri, xmlSchema);
					}
					string targetNamespace = this.GetTargetNamespace(xmlSchema);
					if (this.targetNamespaces[targetNamespace] == null)
					{
						this.targetNamespaces.Add(targetNamespace, targetNamespace);
					}
				}
			}
			this.VerifyTables();
			foreach (object obj in otherSet.GlobalElements.Values)
			{
				XmlSchemaElement xmlSchemaElement = (XmlSchemaElement)obj;
				if (!this.AddToTable(this.elements, xmlSchemaElement.QualifiedName, xmlSchemaElement))
				{
					goto IL_26E;
				}
			}
			foreach (object obj2 in otherSet.GlobalAttributes.Values)
			{
				XmlSchemaAttribute xmlSchemaAttribute = (XmlSchemaAttribute)obj2;
				if (!this.AddToTable(this.attributes, xmlSchemaAttribute.QualifiedName, xmlSchemaAttribute))
				{
					goto IL_26E;
				}
			}
			foreach (object obj3 in otherSet.GlobalTypes.Values)
			{
				XmlSchemaType xmlSchemaType = (XmlSchemaType)obj3;
				if (!this.AddToTable(this.schemaTypes, xmlSchemaType.QualifiedName, xmlSchemaType))
				{
					goto IL_26E;
				}
			}
			this.ProcessNewSubstitutionGroups(otherSet.SubstitutionGroups, false);
			schemaInfo.Add(this.cachedCompiledInfo, this.eventHandler);
			schemaInfo.Add(otherSet.CompiledInfo, this.eventHandler);
			this.cachedCompiledInfo = schemaInfo;
			if (flag)
			{
				this.isCompiled = true;
				this.compileAll = false;
			}
			return;
			IL_26E:
			foreach (object obj4 in sortedSchemas.Values)
			{
				XmlSchema xmlSchema2 = (XmlSchema)obj4;
				if (!arrayList.Contains(xmlSchema2))
				{
					this.Remove(xmlSchema2, false);
				}
			}
			foreach (object obj5 in otherSet.GlobalElements.Values)
			{
				XmlSchemaElement xmlSchemaElement2 = (XmlSchemaElement)obj5;
				if (!arrayList.Contains((XmlSchema)xmlSchemaElement2.Parent))
				{
					this.elements.Remove(xmlSchemaElement2.QualifiedName);
				}
			}
			foreach (object obj6 in otherSet.GlobalAttributes.Values)
			{
				XmlSchemaAttribute xmlSchemaAttribute2 = (XmlSchemaAttribute)obj6;
				if (!arrayList.Contains((XmlSchema)xmlSchemaAttribute2.Parent))
				{
					this.attributes.Remove(xmlSchemaAttribute2.QualifiedName);
				}
			}
			foreach (object obj7 in otherSet.GlobalTypes.Values)
			{
				XmlSchemaType xmlSchemaType2 = (XmlSchemaType)obj7;
				if (!arrayList.Contains((XmlSchema)xmlSchemaType2.Parent))
				{
					this.schemaTypes.Remove(xmlSchemaType2.QualifiedName);
				}
			}
		}

		// Token: 0x17000BB5 RID: 2997
		// (get) Token: 0x06003C0E RID: 15374 RVA: 0x0015092C File Offset: 0x0014EB2C
		internal SchemaInfo CompiledInfo
		{
			get
			{
				return this.cachedCompiledInfo;
			}
		}

		// Token: 0x17000BB6 RID: 2998
		// (get) Token: 0x06003C0F RID: 15375 RVA: 0x00150934 File Offset: 0x0014EB34
		internal XmlReaderSettings ReaderSettings
		{
			get
			{
				return this.readerSettings;
			}
		}

		// Token: 0x06003C10 RID: 15376 RVA: 0x0015093C File Offset: 0x0014EB3C
		internal XmlResolver GetResolver()
		{
			return this.readerSettings.GetXmlResolver_CheckConfig();
		}

		// Token: 0x06003C11 RID: 15377 RVA: 0x00150949 File Offset: 0x0014EB49
		internal ValidationEventHandler GetEventHandler()
		{
			return this.eventHandler;
		}

		// Token: 0x06003C12 RID: 15378 RVA: 0x00150951 File Offset: 0x0014EB51
		internal SchemaNames GetSchemaNames(XmlNameTable nt)
		{
			if (this.nameTable != nt)
			{
				return new SchemaNames(nt);
			}
			if (this.schemaNames == null)
			{
				this.schemaNames = new SchemaNames(this.nameTable);
			}
			return this.schemaNames;
		}

		// Token: 0x06003C13 RID: 15379 RVA: 0x00150984 File Offset: 0x0014EB84
		internal bool IsSchemaLoaded(Uri schemaUri, string targetNamespace, out XmlSchema schema)
		{
			schema = null;
			if (targetNamespace == null)
			{
				targetNamespace = string.Empty;
			}
			if (this.GetSchemaByUri(schemaUri, out schema))
			{
				if (!this.schemas.ContainsKey(schema.SchemaId) || (targetNamespace.Length != 0 && !(targetNamespace == schema.TargetNamespace)))
				{
					if (schema.TargetNamespace == null)
					{
						XmlSchema xmlSchema = this.FindSchemaByNSAndUrl(schemaUri, targetNamespace, null);
						if (xmlSchema != null && this.schemas.ContainsKey(xmlSchema.SchemaId))
						{
							schema = xmlSchema;
						}
						else
						{
							schema = this.Add(targetNamespace, schema);
						}
					}
					else if (targetNamespace.Length != 0 && targetNamespace != schema.TargetNamespace)
					{
						this.SendValidationEvent(new XmlSchemaException("The targetNamespace parameter '{0}' should be the same value as the targetNamespace '{1}' of the schema.", new string[]
						{
							targetNamespace,
							schema.TargetNamespace
						}), XmlSeverityType.Error);
						schema = null;
					}
					else
					{
						this.AddSchemaToSet(schema);
					}
				}
				return true;
			}
			return false;
		}

		// Token: 0x06003C14 RID: 15380 RVA: 0x00150A6D File Offset: 0x0014EC6D
		internal bool GetSchemaByUri(Uri schemaUri, out XmlSchema schema)
		{
			schema = null;
			if (schemaUri == null || schemaUri.OriginalString.Length == 0)
			{
				return false;
			}
			schema = (XmlSchema)this.schemaLocations[schemaUri];
			return schema != null;
		}

		// Token: 0x06003C15 RID: 15381 RVA: 0x00150AA4 File Offset: 0x0014ECA4
		internal string GetTargetNamespace(XmlSchema schema)
		{
			if (schema.TargetNamespace != null)
			{
				return schema.TargetNamespace;
			}
			return string.Empty;
		}

		// Token: 0x17000BB7 RID: 2999
		// (get) Token: 0x06003C16 RID: 15382 RVA: 0x00150ABA File Offset: 0x0014ECBA
		internal SortedList SortedSchemas
		{
			get
			{
				return this.schemas;
			}
		}

		// Token: 0x17000BB8 RID: 3000
		// (get) Token: 0x06003C17 RID: 15383 RVA: 0x00150AC2 File Offset: 0x0014ECC2
		internal bool CompileAll
		{
			get
			{
				return this.compileAll;
			}
		}

		// Token: 0x06003C18 RID: 15384 RVA: 0x00150ACC File Offset: 0x0014ECCC
		private void RemoveSchemaFromCaches(XmlSchema schema)
		{
			List<XmlSchema> list = new List<XmlSchema>();
			schema.GetExternalSchemasList(list, schema);
			for (int i = 0; i < list.Count; i++)
			{
				if (list[i].BaseUri != null && list[i].BaseUri.OriginalString.Length != 0)
				{
					this.schemaLocations.Remove(list[i].BaseUri);
				}
				IEnumerable keys = this.chameleonSchemas.Keys;
				ArrayList arrayList = new ArrayList();
				foreach (object obj in keys)
				{
					ChameleonKey chameleonKey = (ChameleonKey)obj;
					if (chameleonKey.chameleonLocation.Equals(list[i].BaseUri) && (chameleonKey.originalSchema == null || chameleonKey.originalSchema == list[i]))
					{
						arrayList.Add(chameleonKey);
					}
				}
				for (int j = 0; j < arrayList.Count; j++)
				{
					this.chameleonSchemas.Remove(arrayList[j]);
				}
			}
		}

		// Token: 0x06003C19 RID: 15385 RVA: 0x00150BFC File Offset: 0x0014EDFC
		private void RemoveSchemaFromGlobalTables(XmlSchema schema)
		{
			if (this.schemas.Count == 0)
			{
				return;
			}
			this.VerifyTables();
			foreach (object obj in schema.Elements.Values)
			{
				XmlSchemaElement xmlSchemaElement = (XmlSchemaElement)obj;
				if ((XmlSchemaElement)this.elements[xmlSchemaElement.QualifiedName] == xmlSchemaElement)
				{
					this.elements.Remove(xmlSchemaElement.QualifiedName);
				}
			}
			foreach (object obj2 in schema.Attributes.Values)
			{
				XmlSchemaAttribute xmlSchemaAttribute = (XmlSchemaAttribute)obj2;
				if ((XmlSchemaAttribute)this.attributes[xmlSchemaAttribute.QualifiedName] == xmlSchemaAttribute)
				{
					this.attributes.Remove(xmlSchemaAttribute.QualifiedName);
				}
			}
			foreach (object obj3 in schema.SchemaTypes.Values)
			{
				XmlSchemaType xmlSchemaType = (XmlSchemaType)obj3;
				if ((XmlSchemaType)this.schemaTypes[xmlSchemaType.QualifiedName] == xmlSchemaType)
				{
					this.schemaTypes.Remove(xmlSchemaType.QualifiedName);
				}
			}
		}

		// Token: 0x06003C1A RID: 15386 RVA: 0x00150D78 File Offset: 0x0014EF78
		private bool AddToTable(XmlSchemaObjectTable table, XmlQualifiedName qname, XmlSchemaObject item)
		{
			if (qname.Name.Length == 0)
			{
				return true;
			}
			XmlSchemaObject xmlSchemaObject = table[qname];
			if (xmlSchemaObject == null)
			{
				table.Add(qname, item);
				return true;
			}
			if (xmlSchemaObject == item || xmlSchemaObject.SourceUri == item.SourceUri)
			{
				return true;
			}
			string res = string.Empty;
			if (item is XmlSchemaComplexType)
			{
				res = "The complexType '{0}' has already been declared.";
			}
			else if (item is XmlSchemaSimpleType)
			{
				res = "The simpleType '{0}' has already been declared.";
			}
			else if (item is XmlSchemaElement)
			{
				res = "The global element '{0}' has already been declared.";
			}
			else if (item is XmlSchemaAttribute)
			{
				if (qname.Namespace == "http://www.w3.org/XML/1998/namespace")
				{
					XmlSchemaObject xmlSchemaObject2 = Preprocessor.GetBuildInSchema().Attributes[qname];
					if (xmlSchemaObject == xmlSchemaObject2)
					{
						table.Insert(qname, item);
						return true;
					}
					if (item == xmlSchemaObject2)
					{
						return true;
					}
				}
				res = "The global attribute '{0}' has already been declared.";
			}
			this.SendValidationEvent(new XmlSchemaException(res, qname.ToString()), XmlSeverityType.Error);
			return false;
		}

		// Token: 0x06003C1B RID: 15387 RVA: 0x00150E54 File Offset: 0x0014F054
		private void VerifyTables()
		{
			if (this.elements == null)
			{
				this.elements = new XmlSchemaObjectTable();
			}
			if (this.attributes == null)
			{
				this.attributes = new XmlSchemaObjectTable();
			}
			if (this.schemaTypes == null)
			{
				this.schemaTypes = new XmlSchemaObjectTable();
			}
			if (this.substitutionGroups == null)
			{
				this.substitutionGroups = new XmlSchemaObjectTable();
			}
		}

		// Token: 0x06003C1C RID: 15388 RVA: 0x00150EAD File Offset: 0x0014F0AD
		private void InternalValidationCallback(object sender, ValidationEventArgs e)
		{
			if (e.Severity == XmlSeverityType.Error)
			{
				throw e.Exception;
			}
		}

		// Token: 0x06003C1D RID: 15389 RVA: 0x00150EBE File Offset: 0x0014F0BE
		private void SendValidationEvent(XmlSchemaException e, XmlSeverityType severity)
		{
			if (this.eventHandler != null)
			{
				this.eventHandler(this, new ValidationEventArgs(e, severity));
				return;
			}
			throw e;
		}

		// Token: 0x04002BA8 RID: 11176
		private XmlNameTable nameTable;

		// Token: 0x04002BA9 RID: 11177
		private SchemaNames schemaNames;

		// Token: 0x04002BAA RID: 11178
		private SortedList schemas;

		// Token: 0x04002BAB RID: 11179
		private ValidationEventHandler internalEventHandler;

		// Token: 0x04002BAC RID: 11180
		private ValidationEventHandler eventHandler;

		// Token: 0x04002BAD RID: 11181
		private bool isCompiled;

		// Token: 0x04002BAE RID: 11182
		private Hashtable schemaLocations;

		// Token: 0x04002BAF RID: 11183
		private Hashtable chameleonSchemas;

		// Token: 0x04002BB0 RID: 11184
		private Hashtable targetNamespaces;

		// Token: 0x04002BB1 RID: 11185
		private bool compileAll;

		// Token: 0x04002BB2 RID: 11186
		private SchemaInfo cachedCompiledInfo;

		// Token: 0x04002BB3 RID: 11187
		private XmlReaderSettings readerSettings;

		// Token: 0x04002BB4 RID: 11188
		private XmlSchema schemaForSchema;

		// Token: 0x04002BB5 RID: 11189
		private XmlSchemaCompilationSettings compilationSettings;

		// Token: 0x04002BB6 RID: 11190
		internal XmlSchemaObjectTable elements;

		// Token: 0x04002BB7 RID: 11191
		internal XmlSchemaObjectTable attributes;

		// Token: 0x04002BB8 RID: 11192
		internal XmlSchemaObjectTable schemaTypes;

		// Token: 0x04002BB9 RID: 11193
		internal XmlSchemaObjectTable substitutionGroups;

		// Token: 0x04002BBA RID: 11194
		private XmlSchemaObjectTable typeExtensions;

		// Token: 0x04002BBB RID: 11195
		private object internalSyncObject;
	}
}

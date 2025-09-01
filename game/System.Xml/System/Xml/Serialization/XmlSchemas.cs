using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Xml.Schema;

namespace System.Xml.Serialization
{
	/// <summary>Represents the collection of XML schemas.</summary>
	// Token: 0x020002E6 RID: 742
	public class XmlSchemas : CollectionBase, IEnumerable<XmlSchema>, IEnumerable
	{
		/// <summary>Gets or sets the <see cref="T:System.Xml.Schema.XmlSchema" /> object at the specified index. </summary>
		/// <param name="index">The index of the item to retrieve.</param>
		/// <returns>The specified <see cref="T:System.Xml.Schema.XmlSchema" />.</returns>
		// Token: 0x170005BD RID: 1469
		public XmlSchema this[int index]
		{
			get
			{
				return (XmlSchema)base.List[index];
			}
			set
			{
				base.List[index] = value;
			}
		}

		/// <summary>Gets a specified <see cref="T:System.Xml.Schema.XmlSchema" /> object that represents the XML schema associated with the specified namespace.</summary>
		/// <param name="ns">The namespace of the specified object.</param>
		/// <returns>The specified <see cref="T:System.Xml.Schema.XmlSchema" /> object.</returns>
		// Token: 0x170005BE RID: 1470
		public XmlSchema this[string ns]
		{
			get
			{
				IList list = (IList)this.SchemaSet.Schemas(ns);
				if (list.Count == 0)
				{
					return null;
				}
				if (list.Count == 1)
				{
					return (XmlSchema)list[0];
				}
				throw new InvalidOperationException(Res.GetString("There are more then one schema with targetNamespace='{0}'.", new object[]
				{
					ns
				}));
			}
		}

		/// <summary>Gets a collection of schemas that belong to the same namespace.</summary>
		/// <param name="ns">The namespace of the schemas to retrieve.</param>
		/// <returns>An <see cref="T:System.Collections.IList" /> implementation that contains the schemas.</returns>
		// Token: 0x06001D16 RID: 7446 RVA: 0x000A9E3C File Offset: 0x000A803C
		public IList GetSchemas(string ns)
		{
			return (IList)this.SchemaSet.Schemas(ns);
		}

		// Token: 0x170005BF RID: 1471
		// (get) Token: 0x06001D17 RID: 7447 RVA: 0x000A9E4F File Offset: 0x000A804F
		internal SchemaObjectCache Cache
		{
			get
			{
				if (this.cache == null)
				{
					this.cache = new SchemaObjectCache();
				}
				return this.cache;
			}
		}

		// Token: 0x170005C0 RID: 1472
		// (get) Token: 0x06001D18 RID: 7448 RVA: 0x000A9E6A File Offset: 0x000A806A
		internal Hashtable MergedSchemas
		{
			get
			{
				if (this.mergedSchemas == null)
				{
					this.mergedSchemas = new Hashtable();
				}
				return this.mergedSchemas;
			}
		}

		// Token: 0x170005C1 RID: 1473
		// (get) Token: 0x06001D19 RID: 7449 RVA: 0x000A9E85 File Offset: 0x000A8085
		internal Hashtable References
		{
			get
			{
				if (this.references == null)
				{
					this.references = new Hashtable();
				}
				return this.references;
			}
		}

		// Token: 0x170005C2 RID: 1474
		// (get) Token: 0x06001D1A RID: 7450 RVA: 0x000A9EA0 File Offset: 0x000A80A0
		internal XmlSchemaSet SchemaSet
		{
			get
			{
				if (this.schemaSet == null)
				{
					this.schemaSet = new XmlSchemaSet();
					this.schemaSet.XmlResolver = null;
					this.schemaSet.ValidationEventHandler += XmlSchemas.IgnoreCompileErrors;
				}
				return this.schemaSet;
			}
		}

		// Token: 0x06001D1B RID: 7451 RVA: 0x000A9EDE File Offset: 0x000A80DE
		internal int Add(XmlSchema schema, bool delay)
		{
			if (delay)
			{
				if (this.delayedSchemas[schema] == null)
				{
					this.delayedSchemas.Add(schema, schema);
				}
				return -1;
			}
			return this.Add(schema);
		}

		/// <summary>Adds an object to the end of the collection.</summary>
		/// <param name="schema">The <see cref="T:System.Xml.Schema.XmlSchema" /> object to be added to the collection of objects. </param>
		/// <returns>The index at which the <see cref="T:System.Xml.Schema.XmlSchema" /> is added.</returns>
		// Token: 0x06001D1C RID: 7452 RVA: 0x000A9F07 File Offset: 0x000A8107
		public int Add(XmlSchema schema)
		{
			if (base.List.Contains(schema))
			{
				return base.List.IndexOf(schema);
			}
			return base.List.Add(schema);
		}

		/// <summary>Adds an <see cref="T:System.Xml.Schema.XmlSchema" /> object that represents an assembly reference to the collection.</summary>
		/// <param name="schema">The <see cref="T:System.Xml.Schema.XmlSchema" /> to add.</param>
		/// <param name="baseUri">The <see cref="T:System.Uri" /> of the schema object.</param>
		/// <returns>The index at which the <see cref="T:System.Xml.Schema.XmlSchema" /> is added.</returns>
		// Token: 0x06001D1D RID: 7453 RVA: 0x000A9F30 File Offset: 0x000A8130
		public int Add(XmlSchema schema, Uri baseUri)
		{
			if (base.List.Contains(schema))
			{
				return base.List.IndexOf(schema);
			}
			if (baseUri != null)
			{
				schema.BaseUri = baseUri;
			}
			return base.List.Add(schema);
		}

		/// <summary>Adds an instance of the <see cref="T:System.Xml.Serialization.XmlSchemas" /> class to the end of the collection.</summary>
		/// <param name="schemas">The <see cref="T:System.Xml.Serialization.XmlSchemas" /> object to be added to the end of the collection. </param>
		// Token: 0x06001D1E RID: 7454 RVA: 0x000A9F6C File Offset: 0x000A816C
		public void Add(XmlSchemas schemas)
		{
			foreach (object obj in schemas)
			{
				XmlSchema schema = (XmlSchema)obj;
				this.Add(schema);
			}
		}

		/// <summary>Adds an <see cref="T:System.Xml.Schema.XmlSchema" /> object that represents an assembly reference to the collection.</summary>
		/// <param name="schema">The <see cref="T:System.Xml.Schema.XmlSchema" /> to add.</param>
		// Token: 0x06001D1F RID: 7455 RVA: 0x000A9FC4 File Offset: 0x000A81C4
		public void AddReference(XmlSchema schema)
		{
			this.References[schema] = schema;
		}

		/// <summary>Inserts a schema into the <see cref="T:System.Xml.Serialization.XmlSchemas" /> at the specified index.</summary>
		/// <param name="index">The zero-based index at which <paramref name="schema" /> should be inserted. </param>
		/// <param name="schema">The <see cref="T:System.Xml.Schema.XmlSchema" /> object to be inserted. </param>
		// Token: 0x06001D20 RID: 7456 RVA: 0x0009B530 File Offset: 0x00099730
		public void Insert(int index, XmlSchema schema)
		{
			base.List.Insert(index, schema);
		}

		/// <summary>Searches for the specified schema and returns the zero-based index of the first occurrence within the entire <see cref="T:System.Xml.Serialization.XmlSchemas" />.</summary>
		/// <param name="schema">The <see cref="T:System.Xml.Schema.XmlSchema" /> to locate. </param>
		/// <returns>The zero-based index of the first occurrence of the value within the entire <see cref="T:System.Xml.Serialization.XmlSchemas" />, if found; otherwise, -1.</returns>
		// Token: 0x06001D21 RID: 7457 RVA: 0x0009B53F File Offset: 0x0009973F
		public int IndexOf(XmlSchema schema)
		{
			return base.List.IndexOf(schema);
		}

		/// <summary>Determines whether the <see cref="T:System.Xml.Serialization.XmlSchemas" /> contains a specific schema.</summary>
		/// <param name="schema">The <see cref="T:System.Xml.Schema.XmlSchema" /> object to locate. </param>
		/// <returns>
		///     <see langword="true" />, if the collection contains the specified item; otherwise, <see langword="false" />.</returns>
		// Token: 0x06001D22 RID: 7458 RVA: 0x0009B54D File Offset: 0x0009974D
		public bool Contains(XmlSchema schema)
		{
			return base.List.Contains(schema);
		}

		/// <summary>Returns a value that indicates whether the collection contains an <see cref="T:System.Xml.Schema.XmlSchema" /> object that belongs to the specified namespace.</summary>
		/// <param name="targetNamespace">The namespace of the item to check for.</param>
		/// <returns>
		///     <see langword="true" /> if the item is found; otherwise, <see langword="false" />.</returns>
		// Token: 0x06001D23 RID: 7459 RVA: 0x000A9FD3 File Offset: 0x000A81D3
		public bool Contains(string targetNamespace)
		{
			return this.SchemaSet.Contains(targetNamespace);
		}

		/// <summary>Removes the first occurrence of a specific schema from the <see cref="T:System.Xml.Serialization.XmlSchemas" />.</summary>
		/// <param name="schema">The <see cref="T:System.Xml.Schema.XmlSchema" /> to remove. </param>
		// Token: 0x06001D24 RID: 7460 RVA: 0x0009B55B File Offset: 0x0009975B
		public void Remove(XmlSchema schema)
		{
			base.List.Remove(schema);
		}

		/// <summary>Copies the entire <see cref="T:System.Xml.Serialization.XmlSchemas" /> to a compatible one-dimensional <see cref="T:System.Array" />, which starts at the specified index of the target array.</summary>
		/// <param name="array">The one-dimensional <see cref="T:System.Array" /> that is the destination of the schemas copied from <see cref="T:System.Xml.Serialization.XmlSchemas" />. The <see cref="T:System.Array" /> must have zero-based indexing. </param>
		/// <param name="index">A 32-bit integer that represents the index in the array where copying begins.</param>
		// Token: 0x06001D25 RID: 7461 RVA: 0x0009B569 File Offset: 0x00099769
		public void CopyTo(XmlSchema[] array, int index)
		{
			base.List.CopyTo(array, index);
		}

		/// <summary>Performs additional custom processes before inserting a new element into the <see cref="T:System.Xml.Serialization.XmlSchemas" /> instance.</summary>
		/// <param name="index">The zero-based index at which to insert <paramref name="value" />. </param>
		/// <param name="value">The new value of the element at <paramref name="index" />. </param>
		// Token: 0x06001D26 RID: 7462 RVA: 0x000A9FE1 File Offset: 0x000A81E1
		protected override void OnInsert(int index, object value)
		{
			this.AddName((XmlSchema)value);
		}

		/// <summary>Performs additional custom processes when removing an element from the <see cref="T:System.Xml.Serialization.XmlSchemas" /> instance.</summary>
		/// <param name="index">The zero-based index at which <paramref name="value" /> can be found. </param>
		/// <param name="value">The value of the element to remove at <paramref name="index" />. </param>
		// Token: 0x06001D27 RID: 7463 RVA: 0x000A9FEF File Offset: 0x000A81EF
		protected override void OnRemove(int index, object value)
		{
			this.RemoveName((XmlSchema)value);
		}

		/// <summary>Performs additional custom processes when clearing the contents of the <see cref="T:System.Xml.Serialization.XmlSchemas" /> instance.</summary>
		// Token: 0x06001D28 RID: 7464 RVA: 0x000A9FFD File Offset: 0x000A81FD
		protected override void OnClear()
		{
			this.schemaSet = null;
		}

		/// <summary>Performs additional custom processes before setting a value in the <see cref="T:System.Xml.Serialization.XmlSchemas" /> instance.</summary>
		/// <param name="index">The zero-based index at which <paramref name="oldValue" /> can be found. </param>
		/// <param name="oldValue">The value to replace with <paramref name="newValue" />. </param>
		/// <param name="newValue">The new value of the element at <paramref name="index" />. </param>
		// Token: 0x06001D29 RID: 7465 RVA: 0x000AA006 File Offset: 0x000A8206
		protected override void OnSet(int index, object oldValue, object newValue)
		{
			this.RemoveName((XmlSchema)oldValue);
			this.AddName((XmlSchema)newValue);
		}

		// Token: 0x06001D2A RID: 7466 RVA: 0x000AA020 File Offset: 0x000A8220
		private void AddName(XmlSchema schema)
		{
			if (this.isCompiled)
			{
				throw new InvalidOperationException(Res.GetString("Cannot add schema to compiled schemas collection."));
			}
			if (this.SchemaSet.Contains(schema))
			{
				this.SchemaSet.Reprocess(schema);
				return;
			}
			this.Prepare(schema);
			this.SchemaSet.Add(schema);
		}

		// Token: 0x06001D2B RID: 7467 RVA: 0x000AA078 File Offset: 0x000A8278
		private void Prepare(XmlSchema schema)
		{
			ArrayList arrayList = new ArrayList();
			string targetNamespace = schema.TargetNamespace;
			foreach (XmlSchemaObject xmlSchemaObject in schema.Includes)
			{
				XmlSchemaExternal xmlSchemaExternal = (XmlSchemaExternal)xmlSchemaObject;
				if (xmlSchemaExternal is XmlSchemaImport && targetNamespace == ((XmlSchemaImport)xmlSchemaExternal).Namespace)
				{
					arrayList.Add(xmlSchemaExternal);
				}
			}
			foreach (object obj in arrayList)
			{
				XmlSchemaObject item = (XmlSchemaObject)obj;
				schema.Includes.Remove(item);
			}
		}

		// Token: 0x06001D2C RID: 7468 RVA: 0x000AA150 File Offset: 0x000A8350
		private void RemoveName(XmlSchema schema)
		{
			this.SchemaSet.Remove(schema);
		}

		/// <summary>Locates in one of the XML schemas an <see cref="T:System.Xml.Schema.XmlSchemaObject" /> of the specified name and type. </summary>
		/// <param name="name">An <see cref="T:System.Xml.XmlQualifiedName" /> that specifies a fully qualified name with a namespace used to locate an <see cref="T:System.Xml.Schema.XmlSchema" /> object in the collection.</param>
		/// <param name="type">The <see cref="T:System.Type" /> of the object to find. Possible types include: <see cref="T:System.Xml.Schema.XmlSchemaGroup" />, <see cref="T:System.Xml.Schema.XmlSchemaAttributeGroup" />, <see cref="T:System.Xml.Schema.XmlSchemaElement" />, <see cref="T:System.Xml.Schema.XmlSchemaAttribute" />, and <see cref="T:System.Xml.Schema.XmlSchemaNotation" />.</param>
		/// <returns>An <see cref="T:System.Xml.Schema.XmlSchemaObject" /> instance, such as an <see cref="T:System.Xml.Schema.XmlSchemaElement" /> or <see cref="T:System.Xml.Schema.XmlSchemaAttribute" />.</returns>
		// Token: 0x06001D2D RID: 7469 RVA: 0x000AA15F File Offset: 0x000A835F
		public object Find(XmlQualifiedName name, Type type)
		{
			return this.Find(name, type, true);
		}

		// Token: 0x06001D2E RID: 7470 RVA: 0x000AA16C File Offset: 0x000A836C
		internal object Find(XmlQualifiedName name, Type type, bool checkCache)
		{
			if (!this.IsCompiled)
			{
				foreach (object obj in base.List)
				{
					XmlSchemas.Preprocess((XmlSchema)obj);
				}
			}
			IList list = (IList)this.SchemaSet.Schemas(name.Namespace);
			if (list == null)
			{
				return null;
			}
			foreach (object obj2 in list)
			{
				XmlSchema xmlSchema = (XmlSchema)obj2;
				XmlSchemas.Preprocess(xmlSchema);
				XmlSchemaObject xmlSchemaObject = null;
				if (typeof(XmlSchemaType).IsAssignableFrom(type))
				{
					xmlSchemaObject = xmlSchema.SchemaTypes[name];
					if (xmlSchemaObject == null)
					{
						continue;
					}
					if (!type.IsAssignableFrom(xmlSchemaObject.GetType()))
					{
						continue;
					}
				}
				else if (type == typeof(XmlSchemaGroup))
				{
					xmlSchemaObject = xmlSchema.Groups[name];
				}
				else if (type == typeof(XmlSchemaAttributeGroup))
				{
					xmlSchemaObject = xmlSchema.AttributeGroups[name];
				}
				else if (type == typeof(XmlSchemaElement))
				{
					xmlSchemaObject = xmlSchema.Elements[name];
				}
				else if (type == typeof(XmlSchemaAttribute))
				{
					xmlSchemaObject = xmlSchema.Attributes[name];
				}
				else if (type == typeof(XmlSchemaNotation))
				{
					xmlSchemaObject = xmlSchema.Notations[name];
				}
				if (xmlSchemaObject != null && this.shareTypes && checkCache && !this.IsReference(xmlSchemaObject))
				{
					xmlSchemaObject = this.Cache.AddItem(xmlSchemaObject, name, this);
				}
				if (xmlSchemaObject != null)
				{
					return xmlSchemaObject;
				}
			}
			return null;
		}

		// Token: 0x06001D2F RID: 7471 RVA: 0x000AA36C File Offset: 0x000A856C
		IEnumerator<XmlSchema> IEnumerable<XmlSchema>.GetEnumerator()
		{
			return new XmlSchemaEnumerator(this);
		}

		// Token: 0x06001D30 RID: 7472 RVA: 0x000AA374 File Offset: 0x000A8574
		internal static void Preprocess(XmlSchema schema)
		{
			if (!schema.IsPreprocessed)
			{
				try
				{
					NameTable nameTable = new NameTable();
					new Preprocessor(nameTable, new SchemaNames(nameTable), null)
					{
						SchemaLocations = new Hashtable()
					}.Execute(schema, schema.TargetNamespace, false);
				}
				catch (XmlSchemaException ex)
				{
					throw XmlSchemas.CreateValidationException(ex, ex.Message);
				}
			}
		}

		/// <summary>Static method that determines whether the specified XML schema contains a custom <see langword="IsDataSet" /> attribute set to <see langword="true" />, or its equivalent. </summary>
		/// <param name="schema">The XML schema to check for an <see langword="IsDataSet" /> attribute with a <see langword="true" /> value.</param>
		/// <returns>
		///     <see langword="true" /> if the specified schema exists; otherwise, <see langword="false" />.</returns>
		// Token: 0x06001D31 RID: 7473 RVA: 0x000AA3D4 File Offset: 0x000A85D4
		public static bool IsDataSet(XmlSchema schema)
		{
			foreach (XmlSchemaObject xmlSchemaObject in schema.Items)
			{
				if (xmlSchemaObject is XmlSchemaElement)
				{
					XmlSchemaElement xmlSchemaElement = (XmlSchemaElement)xmlSchemaObject;
					if (xmlSchemaElement.UnhandledAttributes != null)
					{
						foreach (XmlAttribute xmlAttribute in xmlSchemaElement.UnhandledAttributes)
						{
							if (xmlAttribute.LocalName == "IsDataSet" && xmlAttribute.NamespaceURI == "urn:schemas-microsoft-com:xml-msdata" && (xmlAttribute.Value == "True" || xmlAttribute.Value == "true" || xmlAttribute.Value == "1"))
							{
								return true;
							}
						}
					}
				}
			}
			return false;
		}

		// Token: 0x06001D32 RID: 7474 RVA: 0x000AA4D0 File Offset: 0x000A86D0
		private void Merge(XmlSchema schema)
		{
			if (this.MergedSchemas[schema] != null)
			{
				return;
			}
			IList list = (IList)this.SchemaSet.Schemas(schema.TargetNamespace);
			if (list != null && list.Count > 0)
			{
				this.MergedSchemas.Add(schema, schema);
				this.Merge(list, schema);
				return;
			}
			this.Add(schema);
			this.MergedSchemas.Add(schema, schema);
		}

		// Token: 0x06001D33 RID: 7475 RVA: 0x000AA53C File Offset: 0x000A873C
		private void AddImport(IList schemas, string ns)
		{
			foreach (object obj in schemas)
			{
				XmlSchema xmlSchema = (XmlSchema)obj;
				bool flag = true;
				foreach (XmlSchemaObject xmlSchemaObject in xmlSchema.Includes)
				{
					XmlSchemaExternal xmlSchemaExternal = (XmlSchemaExternal)xmlSchemaObject;
					if (xmlSchemaExternal is XmlSchemaImport && ((XmlSchemaImport)xmlSchemaExternal).Namespace == ns)
					{
						flag = false;
						break;
					}
				}
				if (flag)
				{
					XmlSchemaImport xmlSchemaImport = new XmlSchemaImport();
					xmlSchemaImport.Namespace = ns;
					xmlSchema.Includes.Add(xmlSchemaImport);
				}
			}
		}

		// Token: 0x06001D34 RID: 7476 RVA: 0x000AA61C File Offset: 0x000A881C
		private void Merge(IList originals, XmlSchema schema)
		{
			foreach (object obj in originals)
			{
				XmlSchema xmlSchema = (XmlSchema)obj;
				if (schema == xmlSchema)
				{
					return;
				}
			}
			foreach (XmlSchemaObject xmlSchemaObject in schema.Includes)
			{
				XmlSchemaExternal xmlSchemaExternal = (XmlSchemaExternal)xmlSchemaObject;
				if (xmlSchemaExternal is XmlSchemaImport)
				{
					xmlSchemaExternal.SchemaLocation = null;
					if (xmlSchemaExternal.Schema != null)
					{
						this.Merge(xmlSchemaExternal.Schema);
					}
					else
					{
						this.AddImport(originals, ((XmlSchemaImport)xmlSchemaExternal).Namespace);
					}
				}
				else if (xmlSchemaExternal.Schema == null)
				{
					if (xmlSchemaExternal.SchemaLocation != null)
					{
						throw new InvalidOperationException(Res.GetString("Schema attribute schemaLocation='{1}' is not supported on objects of type {0}.  Please set {0}.Schema property.", new object[]
						{
							base.GetType().Name,
							xmlSchemaExternal.SchemaLocation
						}));
					}
				}
				else
				{
					xmlSchemaExternal.SchemaLocation = null;
					this.Merge(originals, xmlSchemaExternal.Schema);
				}
			}
			bool[] array = new bool[schema.Items.Count];
			int num = 0;
			for (int i = 0; i < schema.Items.Count; i++)
			{
				XmlSchemaObject xmlSchemaObject2 = schema.Items[i];
				XmlSchemaObject xmlSchemaObject3 = this.Find(xmlSchemaObject2, originals);
				if (xmlSchemaObject3 != null)
				{
					if (!this.Cache.Match(xmlSchemaObject3, xmlSchemaObject2, this.shareTypes))
					{
						throw new InvalidOperationException(XmlSchemas.MergeFailedMessage(xmlSchemaObject2, xmlSchemaObject3, schema.TargetNamespace));
					}
					array[i] = true;
					num++;
				}
			}
			if (num != schema.Items.Count)
			{
				XmlSchema xmlSchema2 = (XmlSchema)originals[0];
				for (int j = 0; j < schema.Items.Count; j++)
				{
					if (!array[j])
					{
						xmlSchema2.Items.Add(schema.Items[j]);
					}
				}
				xmlSchema2.IsPreprocessed = false;
				XmlSchemas.Preprocess(xmlSchema2);
			}
		}

		// Token: 0x06001D35 RID: 7477 RVA: 0x000AA840 File Offset: 0x000A8A40
		private static string ItemName(XmlSchemaObject o)
		{
			if (o is XmlSchemaNotation)
			{
				return ((XmlSchemaNotation)o).Name;
			}
			if (o is XmlSchemaGroup)
			{
				return ((XmlSchemaGroup)o).Name;
			}
			if (o is XmlSchemaElement)
			{
				return ((XmlSchemaElement)o).Name;
			}
			if (o is XmlSchemaType)
			{
				return ((XmlSchemaType)o).Name;
			}
			if (o is XmlSchemaAttributeGroup)
			{
				return ((XmlSchemaAttributeGroup)o).Name;
			}
			if (o is XmlSchemaAttribute)
			{
				return ((XmlSchemaAttribute)o).Name;
			}
			return null;
		}

		// Token: 0x06001D36 RID: 7478 RVA: 0x000AA8C8 File Offset: 0x000A8AC8
		internal static XmlQualifiedName GetParentName(XmlSchemaObject item)
		{
			while (item.Parent != null)
			{
				if (item.Parent is XmlSchemaType)
				{
					XmlSchemaType xmlSchemaType = (XmlSchemaType)item.Parent;
					if (xmlSchemaType.Name != null && xmlSchemaType.Name.Length != 0)
					{
						return xmlSchemaType.QualifiedName;
					}
				}
				item = item.Parent;
			}
			return XmlQualifiedName.Empty;
		}

		// Token: 0x06001D37 RID: 7479 RVA: 0x000AA924 File Offset: 0x000A8B24
		private static string GetSchemaItem(XmlSchemaObject o, string ns, string details)
		{
			if (o == null)
			{
				return null;
			}
			while (o.Parent != null && !(o.Parent is XmlSchema))
			{
				o = o.Parent;
			}
			if (ns == null || ns.Length == 0)
			{
				XmlSchemaObject xmlSchemaObject = o;
				while (xmlSchemaObject.Parent != null)
				{
					xmlSchemaObject = xmlSchemaObject.Parent;
				}
				if (xmlSchemaObject is XmlSchema)
				{
					ns = ((XmlSchema)xmlSchemaObject).TargetNamespace;
				}
			}
			string @string;
			if (o is XmlSchemaNotation)
			{
				@string = Res.GetString("Schema item '{1}' named '{2}' from namespace '{0}'. {3}", new object[]
				{
					ns,
					"notation",
					((XmlSchemaNotation)o).Name,
					details
				});
			}
			else if (o is XmlSchemaGroup)
			{
				@string = Res.GetString("Schema item '{1}' named '{2}' from namespace '{0}'. {3}", new object[]
				{
					ns,
					"group",
					((XmlSchemaGroup)o).Name,
					details
				});
			}
			else if (o is XmlSchemaElement)
			{
				XmlSchemaElement xmlSchemaElement = (XmlSchemaElement)o;
				if (xmlSchemaElement.Name == null || xmlSchemaElement.Name.Length == 0)
				{
					XmlQualifiedName parentName = XmlSchemas.GetParentName(o);
					@string = Res.GetString("Element reference '{0}' declared in schema type '{1}' from namespace '{2}'.", new object[]
					{
						xmlSchemaElement.RefName.ToString(),
						parentName.Name,
						parentName.Namespace
					});
				}
				else
				{
					@string = Res.GetString("Schema item '{1}' named '{2}' from namespace '{0}'. {3}", new object[]
					{
						ns,
						"element",
						xmlSchemaElement.Name,
						details
					});
				}
			}
			else if (o is XmlSchemaType)
			{
				string name = "Schema item '{1}' named '{2}' from namespace '{0}'. {3}";
				object[] array = new object[4];
				array[0] = ns;
				array[1] = ((o.GetType() == typeof(XmlSchemaSimpleType)) ? "simpleType" : "complexType");
				array[2] = ((XmlSchemaType)o).Name;
				@string = Res.GetString(name, array);
			}
			else if (o is XmlSchemaAttributeGroup)
			{
				@string = Res.GetString("Schema item '{1}' named '{2}' from namespace '{0}'. {3}", new object[]
				{
					ns,
					"attributeGroup",
					((XmlSchemaAttributeGroup)o).Name,
					details
				});
			}
			else if (o is XmlSchemaAttribute)
			{
				XmlSchemaAttribute xmlSchemaAttribute = (XmlSchemaAttribute)o;
				if (xmlSchemaAttribute.Name == null || xmlSchemaAttribute.Name.Length == 0)
				{
					XmlQualifiedName parentName2 = XmlSchemas.GetParentName(o);
					return Res.GetString("Attribute reference '{0}' declared in schema type '{1}' from namespace '{2}'.", new object[]
					{
						xmlSchemaAttribute.RefName.ToString(),
						parentName2.Name,
						parentName2.Namespace
					});
				}
				@string = Res.GetString("Schema item '{1}' named '{2}' from namespace '{0}'. {3}", new object[]
				{
					ns,
					"attribute",
					xmlSchemaAttribute.Name,
					details
				});
			}
			else if (o is XmlSchemaContent)
			{
				XmlQualifiedName parentName3 = XmlSchemas.GetParentName(o);
				string name2 = "Check content definition of schema type '{0}' from namespace '{1}'. {2}";
				object[] array2 = new object[3];
				array2[0] = parentName3.Name;
				array2[1] = parentName3.Namespace;
				@string = Res.GetString(name2, array2);
			}
			else if (o is XmlSchemaExternal)
			{
				string text = (o is XmlSchemaImport) ? "import" : ((o is XmlSchemaInclude) ? "include" : ((o is XmlSchemaRedefine) ? "redefine" : o.GetType().Name));
				@string = Res.GetString("Schema item '{1}' from namespace '{0}'. {2}", new object[]
				{
					ns,
					text,
					details
				});
			}
			else if (o is XmlSchema)
			{
				@string = Res.GetString("Schema with targetNamespace='{0}' has invalid syntax. {1}", new object[]
				{
					ns,
					details
				});
			}
			else
			{
				@string = Res.GetString("Schema item '{1}' named '{2}' from namespace '{0}'. {3}", new object[]
				{
					ns,
					o.GetType().Name,
					null,
					details
				});
			}
			return @string;
		}

		// Token: 0x06001D38 RID: 7480 RVA: 0x000AACA4 File Offset: 0x000A8EA4
		private static string Dump(XmlSchemaObject o)
		{
			XmlWriterSettings xmlWriterSettings = new XmlWriterSettings();
			xmlWriterSettings.OmitXmlDeclaration = true;
			xmlWriterSettings.Indent = true;
			XmlSerializer xmlSerializer = new XmlSerializer(o.GetType());
			StringWriter stringWriter = new StringWriter(CultureInfo.InvariantCulture);
			XmlWriter xmlWriter = XmlWriter.Create(stringWriter, xmlWriterSettings);
			XmlSerializerNamespaces xmlSerializerNamespaces = new XmlSerializerNamespaces();
			xmlSerializerNamespaces.Add("xs", "http://www.w3.org/2001/XMLSchema");
			xmlSerializer.Serialize(xmlWriter, o, xmlSerializerNamespaces);
			return stringWriter.ToString();
		}

		// Token: 0x06001D39 RID: 7481 RVA: 0x000AAD08 File Offset: 0x000A8F08
		private static string MergeFailedMessage(XmlSchemaObject src, XmlSchemaObject dest, string ns)
		{
			return Res.GetString("Cannot merge schemas with targetNamespace='{0}'. Several mismatched declarations were found: {1}", new object[]
			{
				ns,
				XmlSchemas.GetSchemaItem(src, ns, null)
			}) + "\r\n" + XmlSchemas.Dump(src) + "\r\n" + XmlSchemas.Dump(dest);
		}

		// Token: 0x06001D3A RID: 7482 RVA: 0x000AAD54 File Offset: 0x000A8F54
		internal XmlSchemaObject Find(XmlSchemaObject o, IList originals)
		{
			string text = XmlSchemas.ItemName(o);
			if (text == null)
			{
				return null;
			}
			Type type = o.GetType();
			foreach (object obj in originals)
			{
				foreach (XmlSchemaObject xmlSchemaObject in ((XmlSchema)obj).Items)
				{
					if (xmlSchemaObject.GetType() == type && text == XmlSchemas.ItemName(xmlSchemaObject))
					{
						return xmlSchemaObject;
					}
				}
			}
			return null;
		}

		/// <summary>Gets a value that indicates whether the schemas have been compiled.</summary>
		/// <returns>
		///     <see langword="true" />, if the schemas have been compiled; otherwise, <see langword="false" />.</returns>
		// Token: 0x170005C3 RID: 1475
		// (get) Token: 0x06001D3B RID: 7483 RVA: 0x000AAE20 File Offset: 0x000A9020
		public bool IsCompiled
		{
			get
			{
				return this.isCompiled;
			}
		}

		/// <summary>Processes the element and attribute names in the XML schemas and, optionally, validates the XML schemas. </summary>
		/// <param name="handler">A <see cref="T:System.Xml.Schema.ValidationEventHandler" /> that specifies the callback method that handles errors and warnings during XML Schema validation, if the strict parameter is set to <see langword="true" />.</param>
		/// <param name="fullCompile">
		///       <see langword="true" /> to validate the XML schemas in the collection using the <see cref="M:System.Xml.Serialization.XmlSchemas.Compile(System.Xml.Schema.ValidationEventHandler,System.Boolean)" /> method of the <see cref="T:System.Xml.Serialization.XmlSchemas" /> class; otherwise, <see langword="false" />.</param>
		// Token: 0x06001D3C RID: 7484 RVA: 0x000AAE28 File Offset: 0x000A9028
		public void Compile(ValidationEventHandler handler, bool fullCompile)
		{
			if (this.isCompiled)
			{
				return;
			}
			foreach (object obj in this.delayedSchemas.Values)
			{
				XmlSchema schema = (XmlSchema)obj;
				this.Merge(schema);
			}
			this.delayedSchemas.Clear();
			if (fullCompile)
			{
				this.schemaSet = new XmlSchemaSet();
				this.schemaSet.XmlResolver = null;
				this.schemaSet.ValidationEventHandler += handler;
				foreach (object obj2 in this.References.Values)
				{
					XmlSchema schema2 = (XmlSchema)obj2;
					this.schemaSet.Add(schema2);
				}
				int num = this.schemaSet.Count;
				foreach (object obj3 in base.List)
				{
					XmlSchema schema3 = (XmlSchema)obj3;
					if (!this.SchemaSet.Contains(schema3))
					{
						this.schemaSet.Add(schema3);
						num++;
					}
				}
				if (!this.SchemaSet.Contains("http://www.w3.org/2001/XMLSchema"))
				{
					this.AddReference(XmlSchemas.XsdSchema);
					this.schemaSet.Add(XmlSchemas.XsdSchema);
					num++;
				}
				if (!this.SchemaSet.Contains("http://www.w3.org/XML/1998/namespace"))
				{
					this.AddReference(XmlSchemas.XmlSchema);
					this.schemaSet.Add(XmlSchemas.XmlSchema);
					num++;
				}
				this.schemaSet.Compile();
				this.schemaSet.ValidationEventHandler -= handler;
				this.isCompiled = (this.schemaSet.IsCompiled && num == this.schemaSet.Count);
				return;
			}
			try
			{
				NameTable nameTable = new NameTable();
				Preprocessor preprocessor = new Preprocessor(nameTable, new SchemaNames(nameTable), null);
				preprocessor.XmlResolver = null;
				preprocessor.SchemaLocations = new Hashtable();
				preprocessor.ChameleonSchemas = new Hashtable();
				foreach (object obj4 in this.SchemaSet.Schemas())
				{
					XmlSchema xmlSchema = (XmlSchema)obj4;
					preprocessor.Execute(xmlSchema, xmlSchema.TargetNamespace, true);
				}
			}
			catch (XmlSchemaException ex)
			{
				throw XmlSchemas.CreateValidationException(ex, ex.Message);
			}
		}

		// Token: 0x06001D3D RID: 7485 RVA: 0x000AB0D0 File Offset: 0x000A92D0
		internal static Exception CreateValidationException(XmlSchemaException exception, string message)
		{
			XmlSchemaObject xmlSchemaObject = exception.SourceSchemaObject;
			if (exception.LineNumber == 0 && exception.LinePosition == 0)
			{
				throw new InvalidOperationException(XmlSchemas.GetSchemaItem(xmlSchemaObject, null, message), exception);
			}
			string text = null;
			if (xmlSchemaObject != null)
			{
				while (xmlSchemaObject.Parent != null)
				{
					xmlSchemaObject = xmlSchemaObject.Parent;
				}
				if (xmlSchemaObject is XmlSchema)
				{
					text = ((XmlSchema)xmlSchemaObject).TargetNamespace;
				}
			}
			throw new InvalidOperationException(Res.GetString("Schema with targetNamespace='{0}' has invalid syntax. {1} Line {2}, position {3}.", new object[]
			{
				text,
				message,
				exception.LineNumber,
				exception.LinePosition
			}), exception);
		}

		// Token: 0x06001D3E RID: 7486 RVA: 0x0000B528 File Offset: 0x00009728
		internal static void IgnoreCompileErrors(object sender, ValidationEventArgs args)
		{
		}

		// Token: 0x170005C4 RID: 1476
		// (get) Token: 0x06001D3F RID: 7487 RVA: 0x000AB167 File Offset: 0x000A9367
		internal static XmlSchema XsdSchema
		{
			get
			{
				if (XmlSchemas.xsd == null)
				{
					XmlSchemas.xsd = XmlSchemas.CreateFakeXsdSchema("http://www.w3.org/2001/XMLSchema", "schema");
				}
				return XmlSchemas.xsd;
			}
		}

		// Token: 0x170005C5 RID: 1477
		// (get) Token: 0x06001D40 RID: 7488 RVA: 0x000AB18F File Offset: 0x000A938F
		internal static XmlSchema XmlSchema
		{
			get
			{
				if (XmlSchemas.xml == null)
				{
					XmlSchemas.xml = XmlSchema.Read(new StringReader("<?xml version='1.0' encoding='UTF-8' ?> \n<xs:schema targetNamespace='http://www.w3.org/XML/1998/namespace' xmlns:xs='http://www.w3.org/2001/XMLSchema' xml:lang='en'>\n <xs:attribute name='lang' type='xs:language'/>\n <xs:attribute name='space'>\n  <xs:simpleType>\n   <xs:restriction base='xs:NCName'>\n    <xs:enumeration value='default'/>\n    <xs:enumeration value='preserve'/>\n   </xs:restriction>\n  </xs:simpleType>\n </xs:attribute>\n <xs:attribute name='base' type='xs:anyURI'/>\n <xs:attribute name='id' type='xs:ID' />\n <xs:attributeGroup name='specialAttrs'>\n  <xs:attribute ref='xml:base'/>\n  <xs:attribute ref='xml:lang'/>\n  <xs:attribute ref='xml:space'/>\n </xs:attributeGroup>\n</xs:schema>"), null);
				}
				return XmlSchemas.xml;
			}
		}

		// Token: 0x06001D41 RID: 7489 RVA: 0x000AB1B8 File Offset: 0x000A93B8
		private static XmlSchema CreateFakeXsdSchema(string ns, string name)
		{
			XmlSchema xmlSchema = new XmlSchema();
			xmlSchema.TargetNamespace = ns;
			XmlSchemaElement xmlSchemaElement = new XmlSchemaElement();
			xmlSchemaElement.Name = name;
			XmlSchemaComplexType schemaType = new XmlSchemaComplexType();
			xmlSchemaElement.SchemaType = schemaType;
			xmlSchema.Items.Add(xmlSchemaElement);
			return xmlSchema;
		}

		// Token: 0x06001D42 RID: 7490 RVA: 0x000AB1F8 File Offset: 0x000A93F8
		internal void SetCache(SchemaObjectCache cache, bool shareTypes)
		{
			this.shareTypes = shareTypes;
			this.cache = cache;
			if (shareTypes)
			{
				cache.GenerateSchemaGraph(this);
			}
		}

		// Token: 0x06001D43 RID: 7491 RVA: 0x000AB214 File Offset: 0x000A9414
		internal bool IsReference(XmlSchemaObject type)
		{
			XmlSchemaObject xmlSchemaObject = type;
			while (xmlSchemaObject.Parent != null)
			{
				xmlSchemaObject = xmlSchemaObject.Parent;
			}
			return this.References.Contains(xmlSchemaObject);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Serialization.XmlSchemas" /> class. </summary>
		// Token: 0x06001D44 RID: 7492 RVA: 0x000AB240 File Offset: 0x000A9440
		public XmlSchemas()
		{
		}

		// Token: 0x04001A38 RID: 6712
		private XmlSchemaSet schemaSet;

		// Token: 0x04001A39 RID: 6713
		private Hashtable references;

		// Token: 0x04001A3A RID: 6714
		private SchemaObjectCache cache;

		// Token: 0x04001A3B RID: 6715
		private bool shareTypes;

		// Token: 0x04001A3C RID: 6716
		private Hashtable mergedSchemas;

		// Token: 0x04001A3D RID: 6717
		internal Hashtable delayedSchemas = new Hashtable();

		// Token: 0x04001A3E RID: 6718
		private bool isCompiled;

		// Token: 0x04001A3F RID: 6719
		private static volatile XmlSchema xsd;

		// Token: 0x04001A40 RID: 6720
		private static volatile XmlSchema xml;

		// Token: 0x04001A41 RID: 6721
		internal const string xmlSchema = "<?xml version='1.0' encoding='UTF-8' ?> \n<xs:schema targetNamespace='http://www.w3.org/XML/1998/namespace' xmlns:xs='http://www.w3.org/2001/XMLSchema' xml:lang='en'>\n <xs:attribute name='lang' type='xs:language'/>\n <xs:attribute name='space'>\n  <xs:simpleType>\n   <xs:restriction base='xs:NCName'>\n    <xs:enumeration value='default'/>\n    <xs:enumeration value='preserve'/>\n   </xs:restriction>\n  </xs:simpleType>\n </xs:attribute>\n <xs:attribute name='base' type='xs:anyURI'/>\n <xs:attribute name='id' type='xs:ID' />\n <xs:attributeGroup name='specialAttrs'>\n  <xs:attribute ref='xml:base'/>\n  <xs:attribute ref='xml:lang'/>\n  <xs:attribute ref='xml:space'/>\n </xs:attributeGroup>\n</xs:schema>";
	}
}

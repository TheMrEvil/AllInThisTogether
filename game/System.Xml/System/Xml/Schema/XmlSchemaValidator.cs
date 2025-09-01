using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Xml.XmlConfiguration;

namespace System.Xml.Schema
{
	/// <summary>Represents an XML Schema Definition Language (XSD) Schema validation engine. The <see cref="T:System.Xml.Schema.XmlSchemaValidator" /> class cannot be inherited.</summary>
	// Token: 0x020005EB RID: 1515
	public sealed class XmlSchemaValidator
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Schema.XmlSchemaValidator" /> class.</summary>
		/// <param name="nameTable">An <see cref="T:System.Xml.XmlNameTable" /> object containing element and attribute names as atomized strings.</param>
		/// <param name="schemas">An <see cref="T:System.Xml.Schema.XmlSchemaSet" /> object containing the XML Schema Definition Language (XSD) schemas used for validation.</param>
		/// <param name="namespaceResolver">An <see cref="T:System.Xml.IXmlNamespaceResolver" /> object used for resolving namespaces encountered during validation.</param>
		/// <param name="validationFlags">An <see cref="T:System.Xml.Schema.XmlSchemaValidationFlags" /> value specifying schema validation options.</param>
		/// <exception cref="T:System.ArgumentNullException">One or more of the parameters specified are <see langword="null" />.</exception>
		/// <exception cref="T:System.Xml.Schema.XmlSchemaException">An error occurred while compiling schemas in the <see cref="T:System.Xml.Schema.XmlSchemaSet" /> parameter.</exception>
		// Token: 0x06003C8E RID: 15502 RVA: 0x0015157C File Offset: 0x0014F77C
		public XmlSchemaValidator(XmlNameTable nameTable, XmlSchemaSet schemas, IXmlNamespaceResolver namespaceResolver, XmlSchemaValidationFlags validationFlags)
		{
			if (nameTable == null)
			{
				throw new ArgumentNullException("nameTable");
			}
			if (schemas == null)
			{
				throw new ArgumentNullException("schemas");
			}
			if (namespaceResolver == null)
			{
				throw new ArgumentNullException("namespaceResolver");
			}
			this.nameTable = nameTable;
			this.nsResolver = namespaceResolver;
			this.validationFlags = validationFlags;
			if ((validationFlags & XmlSchemaValidationFlags.ProcessInlineSchema) != XmlSchemaValidationFlags.None || (validationFlags & XmlSchemaValidationFlags.ProcessSchemaLocation) != XmlSchemaValidationFlags.None)
			{
				this.schemaSet = new XmlSchemaSet(nameTable);
				this.schemaSet.ValidationEventHandler += schemas.GetEventHandler();
				this.schemaSet.CompilationSettings = schemas.CompilationSettings;
				this.schemaSet.XmlResolver = schemas.GetResolver();
				this.schemaSet.Add(schemas);
				this.validatedNamespaces = new Hashtable();
			}
			else
			{
				this.schemaSet = schemas;
			}
			this.Init();
		}

		// Token: 0x06003C8F RID: 15503 RVA: 0x0015165C File Offset: 0x0014F85C
		private void Init()
		{
			this.validationStack = new HWStack(10);
			this.attPresence = new Hashtable();
			this.Push(XmlQualifiedName.Empty);
			this.dummyPositionInfo = new PositionInfo();
			this.positionInfo = this.dummyPositionInfo;
			this.validationEventSender = this;
			this.currentState = ValidatorState.None;
			this.textValue = new StringBuilder(100);
			this.xmlResolver = XmlReaderSection.CreateDefaultResolver();
			this.contextQName = new XmlQualifiedName();
			this.Reset();
			this.RecompileSchemaSet();
			this.NsXs = this.nameTable.Add("http://www.w3.org/2001/XMLSchema");
			this.NsXsi = this.nameTable.Add("http://www.w3.org/2001/XMLSchema-instance");
			this.NsXmlNs = this.nameTable.Add("http://www.w3.org/2000/xmlns/");
			this.NsXml = this.nameTable.Add("http://www.w3.org/XML/1998/namespace");
			this.xsiTypeString = this.nameTable.Add("type");
			this.xsiNilString = this.nameTable.Add("nil");
			this.xsiSchemaLocationString = this.nameTable.Add("schemaLocation");
			this.xsiNoNamespaceSchemaLocationString = this.nameTable.Add("noNamespaceSchemaLocation");
		}

		// Token: 0x06003C90 RID: 15504 RVA: 0x00151790 File Offset: 0x0014F990
		private void Reset()
		{
			this.isRoot = true;
			this.rootHasSchema = true;
			while (this.validationStack.Length > 1)
			{
				this.validationStack.Pop();
			}
			this.startIDConstraint = -1;
			this.partialValidationType = null;
			if (this.IDs != null)
			{
				this.IDs.Clear();
			}
			if (this.ProcessSchemaHints)
			{
				this.validatedNamespaces.Clear();
			}
		}

		/// <summary>Sets the <see cref="T:System.Xml.XmlResolver" /> object used to resolve xs:import and xs:include elements as well as xsi:schemaLocation and xsi:noNamespaceSchemaLocation attributes.</summary>
		/// <returns>An <see cref="T:System.Xml.XmlResolver" /> object; the default is an <see cref="T:System.Xml.XmlUrlResolver" /> object.</returns>
		// Token: 0x17000BE1 RID: 3041
		// (set) Token: 0x06003C91 RID: 15505 RVA: 0x001517FB File Offset: 0x0014F9FB
		public XmlResolver XmlResolver
		{
			set
			{
				this.xmlResolver = value;
			}
		}

		/// <summary>Gets or sets the line number information for the XML node being validated.</summary>
		/// <returns>An <see cref="T:System.Xml.IXmlLineInfo" /> object.</returns>
		// Token: 0x17000BE2 RID: 3042
		// (get) Token: 0x06003C92 RID: 15506 RVA: 0x00151804 File Offset: 0x0014FA04
		// (set) Token: 0x06003C93 RID: 15507 RVA: 0x0015180C File Offset: 0x0014FA0C
		public IXmlLineInfo LineInfoProvider
		{
			get
			{
				return this.positionInfo;
			}
			set
			{
				if (value == null)
				{
					this.positionInfo = this.dummyPositionInfo;
					return;
				}
				this.positionInfo = value;
			}
		}

		/// <summary>Gets or sets the source URI for the XML node being validated.</summary>
		/// <returns>A <see cref="T:System.Uri" /> object representing the source URI for the XML node being validated; the default is <see langword="null" />.</returns>
		// Token: 0x17000BE3 RID: 3043
		// (get) Token: 0x06003C94 RID: 15508 RVA: 0x00151825 File Offset: 0x0014FA25
		// (set) Token: 0x06003C95 RID: 15509 RVA: 0x0015182D File Offset: 0x0014FA2D
		public Uri SourceUri
		{
			get
			{
				return this.sourceUri;
			}
			set
			{
				this.sourceUri = value;
				this.sourceUriString = this.sourceUri.ToString();
			}
		}

		/// <summary>Gets or sets the object sent as the sender object of a validation event.</summary>
		/// <returns>An <see cref="T:System.Object" />; the default is this <see cref="T:System.Xml.Schema.XmlSchemaValidator" /> object.</returns>
		// Token: 0x17000BE4 RID: 3044
		// (get) Token: 0x06003C96 RID: 15510 RVA: 0x00151847 File Offset: 0x0014FA47
		// (set) Token: 0x06003C97 RID: 15511 RVA: 0x0015184F File Offset: 0x0014FA4F
		public object ValidationEventSender
		{
			get
			{
				return this.validationEventSender;
			}
			set
			{
				this.validationEventSender = value;
			}
		}

		/// <summary>The <see cref="T:System.Xml.Schema.ValidationEventHandler" /> that receives schema validation warnings and errors encountered during schema validation.</summary>
		// Token: 0x14000011 RID: 17
		// (add) Token: 0x06003C98 RID: 15512 RVA: 0x00151858 File Offset: 0x0014FA58
		// (remove) Token: 0x06003C99 RID: 15513 RVA: 0x00151871 File Offset: 0x0014FA71
		public event ValidationEventHandler ValidationEventHandler
		{
			add
			{
				this.eventHandler = (ValidationEventHandler)Delegate.Combine(this.eventHandler, value);
			}
			remove
			{
				this.eventHandler = (ValidationEventHandler)Delegate.Remove(this.eventHandler, value);
			}
		}

		/// <summary>Adds an XML Schema Definition Language (XSD) schema to the set of schemas used for validation.</summary>
		/// <param name="schema">An <see cref="T:System.Xml.Schema.XmlSchema" /> object to add to the set of schemas used for validation.</param>
		/// <exception cref="T:System.ArgumentNullException">The <see cref="T:System.Xml.Schema.XmlSchema" /> parameter specified is <see langword="null" />.</exception>
		/// <exception cref="T:System.Xml.Schema.XmlSchemaValidationException">The target namespace of the <see cref="T:System.Xml.Schema.XmlSchema" /> parameter matches that of any element or attribute already encountered by the <see cref="T:System.Xml.Schema.XmlSchemaValidator" /> object.</exception>
		/// <exception cref="T:System.Xml.Schema.XmlSchemaException">The <see cref="T:System.Xml.Schema.XmlSchema" /> parameter is invalid.</exception>
		// Token: 0x06003C9A RID: 15514 RVA: 0x0015188C File Offset: 0x0014FA8C
		public void AddSchema(XmlSchema schema)
		{
			if (schema == null)
			{
				throw new ArgumentNullException("schema");
			}
			if ((this.validationFlags & XmlSchemaValidationFlags.ProcessInlineSchema) == XmlSchemaValidationFlags.None)
			{
				return;
			}
			string text = schema.TargetNamespace;
			if (text == null)
			{
				text = string.Empty;
			}
			Hashtable schemaLocations = this.schemaSet.SchemaLocations;
			DictionaryEntry[] array = new DictionaryEntry[schemaLocations.Count];
			schemaLocations.CopyTo(array, 0);
			if (this.validatedNamespaces[text] != null && this.schemaSet.FindSchemaByNSAndUrl(schema.BaseUri, text, array) == null)
			{
				this.SendValidationEvent("An element or attribute information item has already been validated from the '{0}' namespace. It is an error if 'xsi:schemaLocation', 'xsi:noNamespaceSchemaLocation', or an inline schema occurs for that namespace.", text, XmlSeverityType.Error);
			}
			if (schema.ErrorCount == 0)
			{
				try
				{
					this.schemaSet.Add(schema);
					this.RecompileSchemaSet();
				}
				catch (XmlSchemaException ex)
				{
					this.SendValidationEvent("Cannot load the schema for the namespace '{0}' - {1}", new string[]
					{
						schema.BaseUri.ToString(),
						ex.Message
					}, ex);
				}
				for (int i = 0; i < schema.ImportedSchemas.Count; i++)
				{
					XmlSchema xmlSchema = (XmlSchema)schema.ImportedSchemas[i];
					text = xmlSchema.TargetNamespace;
					if (text == null)
					{
						text = string.Empty;
					}
					if (this.validatedNamespaces[text] != null && this.schemaSet.FindSchemaByNSAndUrl(xmlSchema.BaseUri, text, array) == null)
					{
						this.SendValidationEvent("An element or attribute information item has already been validated from the '{0}' namespace. It is an error if 'xsi:schemaLocation', 'xsi:noNamespaceSchemaLocation', or an inline schema occurs for that namespace.", text, XmlSeverityType.Error);
						this.schemaSet.RemoveRecursive(schema);
						return;
					}
				}
			}
		}

		/// <summary>Initializes the state of the <see cref="T:System.Xml.Schema.XmlSchemaValidator" /> object.</summary>
		/// <exception cref="T:System.InvalidOperationException">Calling the <see cref="Overload:System.Xml.Schema.XmlSchemaValidator.Initialize" /> method is valid immediately after the construction of an <see cref="T:System.Xml.Schema.XmlSchemaValidator" /> object or after a call to <see cref="M:System.Xml.Schema.XmlSchemaValidator.EndValidation" /> only.</exception>
		// Token: 0x06003C9B RID: 15515 RVA: 0x001519E8 File Offset: 0x0014FBE8
		public void Initialize()
		{
			if (this.currentState != ValidatorState.None && this.currentState != ValidatorState.Finish)
			{
				string name = "The transition from the '{0}' method to the '{1}' method is not allowed.";
				object[] args = new string[]
				{
					XmlSchemaValidator.MethodNames[(int)this.currentState],
					XmlSchemaValidator.MethodNames[1]
				};
				throw new InvalidOperationException(Res.GetString(name, args));
			}
			this.currentState = ValidatorState.Start;
			this.Reset();
		}

		/// <summary>Initializes the state of the <see cref="T:System.Xml.Schema.XmlSchemaValidator" /> object using the <see cref="T:System.Xml.Schema.XmlSchemaObject" /> specified for partial validation.</summary>
		/// <param name="partialValidationType">An <see cref="T:System.Xml.Schema.XmlSchemaElement" />, <see cref="T:System.Xml.Schema.XmlSchemaAttribute" />, or <see cref="T:System.Xml.Schema.XmlSchemaType" /> object used to initialize the validation context of the <see cref="T:System.Xml.Schema.XmlSchemaValidator" /> object for partial validation.</param>
		/// <exception cref="T:System.InvalidOperationException">Calling the <see cref="Overload:System.Xml.Schema.XmlSchemaValidator.Initialize" /> method is valid immediately after the construction of an <see cref="T:System.Xml.Schema.XmlSchemaValidator" /> object or after a call to <see cref="M:System.Xml.Schema.XmlSchemaValidator.EndValidation" /> only.</exception>
		/// <exception cref="T:System.ArgumentException">The <see cref="T:System.Xml.Schema.XmlSchemaObject" /> parameter is not an <see cref="T:System.Xml.Schema.XmlSchemaElement" />, <see cref="T:System.Xml.Schema.XmlSchemaAttribute" />, or <see cref="T:System.Xml.Schema.XmlSchemaType" /> object.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <see cref="T:System.Xml.Schema.XmlSchemaObject" /> parameter cannot be <see langword="null" />.</exception>
		// Token: 0x06003C9C RID: 15516 RVA: 0x00151A48 File Offset: 0x0014FC48
		public void Initialize(XmlSchemaObject partialValidationType)
		{
			if (this.currentState != ValidatorState.None && this.currentState != ValidatorState.Finish)
			{
				string name = "The transition from the '{0}' method to the '{1}' method is not allowed.";
				object[] args = new string[]
				{
					XmlSchemaValidator.MethodNames[(int)this.currentState],
					XmlSchemaValidator.MethodNames[1]
				};
				throw new InvalidOperationException(Res.GetString(name, args));
			}
			if (partialValidationType == null)
			{
				throw new ArgumentNullException("partialValidationType");
			}
			if (!(partialValidationType is XmlSchemaElement) && !(partialValidationType is XmlSchemaAttribute) && !(partialValidationType is XmlSchemaType))
			{
				throw new ArgumentException(Res.GetString("The partial validation type has to be 'XmlSchemaElement', 'XmlSchemaAttribute', or 'XmlSchemaType'."));
			}
			this.currentState = ValidatorState.Start;
			this.Reset();
			this.partialValidationType = partialValidationType;
		}

		/// <summary>Validates the element in the current context.</summary>
		/// <param name="localName">The local name of the element to validate.</param>
		/// <param name="namespaceUri">The namespace URI of the element to validate.</param>
		/// <param name="schemaInfo">An <see cref="T:System.Xml.Schema.XmlSchemaInfo" /> object whose properties are set on successful validation of the element's name. This parameter can be <see langword="null" />.</param>
		/// <exception cref="T:System.Xml.Schema.XmlSchemaValidationException">The element's name is not valid in the current context.</exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="Overload:System.Xml.Schema.XmlSchemaValidator.ValidateElement" /> method was not called in the correct sequence. For example, the <see cref="Overload:System.Xml.Schema.XmlSchemaValidator.ValidateElement" /> method is called after calling <see cref="Overload:System.Xml.Schema.XmlSchemaValidator.ValidateAttribute" />.</exception>
		// Token: 0x06003C9D RID: 15517 RVA: 0x00151AE2 File Offset: 0x0014FCE2
		public void ValidateElement(string localName, string namespaceUri, XmlSchemaInfo schemaInfo)
		{
			this.ValidateElement(localName, namespaceUri, schemaInfo, null, null, null, null);
		}

		/// <summary>Validates the element in the current context with the xsi:Type, xsi:Nil, xsi:SchemaLocation, and xsi:NoNamespaceSchemaLocation attribute values specified.</summary>
		/// <param name="localName">The local name of the element to validate.</param>
		/// <param name="namespaceUri">The namespace URI of the element to validate.</param>
		/// <param name="schemaInfo">An <see cref="T:System.Xml.Schema.XmlSchemaInfo" /> object whose properties are set on successful validation of the element's name. This parameter can be <see langword="null" />.</param>
		/// <param name="xsiType">The xsi:Type attribute value of the element. This parameter can be <see langword="null" />.</param>
		/// <param name="xsiNil">The xsi:Nil attribute value of the element. This parameter can be <see langword="null" />.</param>
		/// <param name="xsiSchemaLocation">The xsi:SchemaLocation attribute value of the element. This parameter can be <see langword="null" />.</param>
		/// <param name="xsiNoNamespaceSchemaLocation">The xsi:NoNamespaceSchemaLocation attribute value of the element. This parameter can be <see langword="null" />.</param>
		/// <exception cref="T:System.Xml.Schema.XmlSchemaValidationException">The element's name is not valid in the current context.</exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="Overload:System.Xml.Schema.XmlSchemaValidator.ValidateElement" /> method was not called in the correct sequence. For example, the <see cref="Overload:System.Xml.Schema.XmlSchemaValidator.ValidateElement" /> method is called after calling <see cref="Overload:System.Xml.Schema.XmlSchemaValidator.ValidateAttribute" />.</exception>
		// Token: 0x06003C9E RID: 15518 RVA: 0x00151AF4 File Offset: 0x0014FCF4
		public void ValidateElement(string localName, string namespaceUri, XmlSchemaInfo schemaInfo, string xsiType, string xsiNil, string xsiSchemaLocation, string xsiNoNamespaceSchemaLocation)
		{
			if (localName == null)
			{
				throw new ArgumentNullException("localName");
			}
			if (namespaceUri == null)
			{
				throw new ArgumentNullException("namespaceUri");
			}
			this.CheckStateTransition(ValidatorState.Element, XmlSchemaValidator.MethodNames[4]);
			this.ClearPSVI();
			this.contextQName.Init(localName, namespaceUri);
			XmlQualifiedName xmlQualifiedName = this.contextQName;
			bool flag;
			object particle = this.ValidateElementContext(xmlQualifiedName, out flag);
			SchemaElementDecl schemaElementDecl = this.FastGetElementDecl(xmlQualifiedName, particle);
			this.Push(xmlQualifiedName);
			if (flag)
			{
				this.context.Validity = XmlSchemaValidity.Invalid;
			}
			if ((this.validationFlags & XmlSchemaValidationFlags.ProcessSchemaLocation) != XmlSchemaValidationFlags.None && this.xmlResolver != null)
			{
				this.ProcessSchemaLocations(xsiSchemaLocation, xsiNoNamespaceSchemaLocation);
			}
			if (this.processContents != XmlSchemaContentProcessing.Skip)
			{
				if (schemaElementDecl == null && this.partialValidationType == null)
				{
					schemaElementDecl = this.compiledSchemaInfo.GetElementDecl(xmlQualifiedName);
				}
				bool declFound = schemaElementDecl != null;
				if (xsiType != null || xsiNil != null)
				{
					schemaElementDecl = this.CheckXsiTypeAndNil(schemaElementDecl, xsiType, xsiNil, ref declFound);
				}
				if (schemaElementDecl == null)
				{
					this.ThrowDeclNotFoundWarningOrError(declFound);
				}
			}
			this.context.ElementDecl = schemaElementDecl;
			XmlSchemaElement schemaElement = null;
			XmlSchemaType schemaType = null;
			if (schemaElementDecl != null)
			{
				this.CheckElementProperties();
				this.attPresence.Clear();
				this.context.NeedValidateChildren = (this.processContents != XmlSchemaContentProcessing.Skip);
				this.ValidateStartElementIdentityConstraints();
				schemaElementDecl.ContentValidator.InitValidation(this.context);
				schemaType = schemaElementDecl.SchemaType;
				schemaElement = this.GetSchemaElement();
			}
			if (schemaInfo != null)
			{
				schemaInfo.SchemaType = schemaType;
				schemaInfo.SchemaElement = schemaElement;
				schemaInfo.IsNil = this.context.IsNill;
				schemaInfo.Validity = this.context.Validity;
			}
			if (this.ProcessSchemaHints && this.validatedNamespaces[namespaceUri] == null)
			{
				this.validatedNamespaces.Add(namespaceUri, namespaceUri);
			}
			if (this.isRoot)
			{
				this.isRoot = false;
			}
		}

		/// <summary>Validates the attribute name, namespace URI, and value in the current element context.</summary>
		/// <param name="localName">The local name of the attribute to validate.</param>
		/// <param name="namespaceUri">The namespace URI of the attribute to validate.</param>
		/// <param name="attributeValue">The value of the attribute to validate.</param>
		/// <param name="schemaInfo">An <see cref="T:System.Xml.Schema.XmlSchemaInfo" /> object whose properties are set on successful validation of the attribute. This parameter can be <see langword="null" />.</param>
		/// <returns>The validated attribute's value.</returns>
		/// <exception cref="T:System.Xml.Schema.XmlSchemaValidationException">The attribute is not valid in the current element context.</exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="Overload:System.Xml.Schema.XmlSchemaValidator.ValidateAttribute" /> method was not called in the correct sequence. For example, calling <see cref="Overload:System.Xml.Schema.XmlSchemaValidator.ValidateAttribute" /> after calling <see cref="M:System.Xml.Schema.XmlSchemaValidator.ValidateEndOfAttributes(System.Xml.Schema.XmlSchemaInfo)" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">One or more of the parameters specified are <see langword="null" />.</exception>
		// Token: 0x06003C9F RID: 15519 RVA: 0x00151C9D File Offset: 0x0014FE9D
		public object ValidateAttribute(string localName, string namespaceUri, string attributeValue, XmlSchemaInfo schemaInfo)
		{
			if (attributeValue == null)
			{
				throw new ArgumentNullException("attributeValue");
			}
			return this.ValidateAttribute(localName, namespaceUri, null, attributeValue, schemaInfo);
		}

		/// <summary>Validates the attribute name, namespace URI, and value in the current element context.</summary>
		/// <param name="localName">The local name of the attribute to validate.</param>
		/// <param name="namespaceUri">The namespace URI of the attribute to validate.</param>
		/// <param name="attributeValue">An <see cref="T:System.Xml.Schema.XmlValueGetter" /><see langword="delegate" /> used to pass the attribute's value as a Common Language Runtime (CLR) type compatible with the XML Schema Definition Language (XSD) type of the attribute.</param>
		/// <param name="schemaInfo">An <see cref="T:System.Xml.Schema.XmlSchemaInfo" /> object whose properties are set on successful validation of the attribute. This parameter and can be <see langword="null" />.</param>
		/// <returns>The validated attribute's value.</returns>
		/// <exception cref="T:System.Xml.Schema.XmlSchemaValidationException">The attribute is not valid in the current element context.</exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="Overload:System.Xml.Schema.XmlSchemaValidator.ValidateAttribute" /> method was not called in the correct sequence. For example, calling <see cref="Overload:System.Xml.Schema.XmlSchemaValidator.ValidateAttribute" /> after calling <see cref="M:System.Xml.Schema.XmlSchemaValidator.ValidateEndOfAttributes(System.Xml.Schema.XmlSchemaInfo)" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">One or more of the parameters specified are <see langword="null" />.</exception>
		// Token: 0x06003CA0 RID: 15520 RVA: 0x00151CB9 File Offset: 0x0014FEB9
		public object ValidateAttribute(string localName, string namespaceUri, XmlValueGetter attributeValue, XmlSchemaInfo schemaInfo)
		{
			if (attributeValue == null)
			{
				throw new ArgumentNullException("attributeValue");
			}
			return this.ValidateAttribute(localName, namespaceUri, attributeValue, null, schemaInfo);
		}

		// Token: 0x06003CA1 RID: 15521 RVA: 0x00151CD8 File Offset: 0x0014FED8
		private object ValidateAttribute(string lName, string ns, XmlValueGetter attributeValueGetter, string attributeStringValue, XmlSchemaInfo schemaInfo)
		{
			if (lName == null)
			{
				throw new ArgumentNullException("localName");
			}
			if (ns == null)
			{
				throw new ArgumentNullException("namespaceUri");
			}
			ValidatorState validatorState = (this.validationStack.Length > 1) ? ValidatorState.Attribute : ValidatorState.TopLevelAttribute;
			this.CheckStateTransition(validatorState, XmlSchemaValidator.MethodNames[(int)validatorState]);
			object obj = null;
			this.attrValid = true;
			XmlSchemaValidity validity = XmlSchemaValidity.NotKnown;
			XmlSchemaAttribute xmlSchemaAttribute = null;
			XmlSchemaSimpleType memberType = null;
			ns = this.nameTable.Add(ns);
			if (Ref.Equal(ns, this.NsXmlNs))
			{
				return null;
			}
			SchemaAttDef schemaAttDef = null;
			SchemaElementDecl elementDecl = this.context.ElementDecl;
			XmlQualifiedName xmlQualifiedName = new XmlQualifiedName(lName, ns);
			if (this.attPresence[xmlQualifiedName] != null)
			{
				this.SendValidationEvent("The '{0}' attribute has already been validated and is a duplicate attribute.", xmlQualifiedName.ToString());
				if (schemaInfo != null)
				{
					schemaInfo.Clear();
				}
				return null;
			}
			if (!Ref.Equal(ns, this.NsXsi))
			{
				XmlSchemaObject xmlSchemaObject = (this.currentState == ValidatorState.TopLevelAttribute) ? this.partialValidationType : null;
				AttributeMatchState attributeMatchState;
				schemaAttDef = this.compiledSchemaInfo.GetAttributeXsd(elementDecl, xmlQualifiedName, xmlSchemaObject, out attributeMatchState);
				switch (attributeMatchState)
				{
				case AttributeMatchState.AttributeFound:
					break;
				case AttributeMatchState.AnyIdAttributeFound:
					if (this.wildID != null)
					{
						this.SendValidationEvent("It is an error if more than one attribute whose type is xs:ID or is derived from xs:ID, matches an attribute wildcard on an element.", string.Empty);
						goto IL_3F5;
					}
					this.wildID = schemaAttDef;
					if ((elementDecl.SchemaType as XmlSchemaComplexType).ContainsIdAttribute(false))
					{
						this.SendValidationEvent("It is an error if there is a member of the attribute uses of a type definition with type xs:ID or derived from xs:ID and another attribute with type xs:ID matches an attribute wildcard.", string.Empty);
						goto IL_3F5;
					}
					break;
				case AttributeMatchState.UndeclaredElementAndAttribute:
					if ((schemaAttDef = this.CheckIsXmlAttribute(xmlQualifiedName)) == null)
					{
						if (elementDecl == null && this.processContents == XmlSchemaContentProcessing.Strict && xmlQualifiedName.Namespace.Length != 0 && this.compiledSchemaInfo.Contains(xmlQualifiedName.Namespace))
						{
							this.attrValid = false;
							this.SendValidationEvent("The '{0}' attribute is not declared.", xmlQualifiedName.ToString());
							goto IL_3F5;
						}
						if (this.processContents != XmlSchemaContentProcessing.Skip)
						{
							this.SendValidationEvent("Could not find schema information for the attribute '{0}'.", xmlQualifiedName.ToString(), XmlSeverityType.Warning);
							goto IL_3F5;
						}
						goto IL_3F5;
					}
					break;
				case AttributeMatchState.UndeclaredAttribute:
					if ((schemaAttDef = this.CheckIsXmlAttribute(xmlQualifiedName)) == null)
					{
						this.attrValid = false;
						this.SendValidationEvent("The '{0}' attribute is not declared.", xmlQualifiedName.ToString());
						goto IL_3F5;
					}
					break;
				case AttributeMatchState.AnyAttributeLax:
					this.SendValidationEvent("Could not find schema information for the attribute '{0}'.", xmlQualifiedName.ToString(), XmlSeverityType.Warning);
					goto IL_3F5;
				case AttributeMatchState.AnyAttributeSkip:
					goto IL_3F5;
				case AttributeMatchState.ProhibitedAnyAttribute:
					if ((schemaAttDef = this.CheckIsXmlAttribute(xmlQualifiedName)) == null)
					{
						this.attrValid = false;
						this.SendValidationEvent("The '{0}' attribute is not allowed.", xmlQualifiedName.ToString());
						goto IL_3F5;
					}
					break;
				case AttributeMatchState.ProhibitedAttribute:
					this.attrValid = false;
					this.SendValidationEvent("The '{0}' attribute is not allowed.", xmlQualifiedName.ToString());
					goto IL_3F5;
				case AttributeMatchState.AttributeNameMismatch:
					this.attrValid = false;
					this.SendValidationEvent("The attribute name '{0}' does not match the name '{1}' of the 'XmlSchemaAttribute' set as a partial validation type.", new string[]
					{
						xmlQualifiedName.ToString(),
						((XmlSchemaAttribute)xmlSchemaObject).QualifiedName.ToString()
					});
					goto IL_3F5;
				case AttributeMatchState.ValidateAttributeInvalidCall:
					this.currentState = ValidatorState.Start;
					this.attrValid = false;
					this.SendValidationEvent("If the partial validation type is 'XmlSchemaElement' or 'XmlSchemaType', the 'ValidateAttribute' method cannot be called.", string.Empty);
					goto IL_3F5;
				default:
					goto IL_3F5;
				}
				xmlSchemaAttribute = schemaAttDef.SchemaAttribute;
				if (elementDecl != null)
				{
					this.attPresence.Add(xmlQualifiedName, schemaAttDef);
				}
				object obj2;
				if (attributeValueGetter != null)
				{
					obj2 = attributeValueGetter();
				}
				else
				{
					obj2 = attributeStringValue;
				}
				obj = this.CheckAttributeValue(obj2, schemaAttDef);
				XmlSchemaDatatype datatype = schemaAttDef.Datatype;
				if (datatype.Variety == XmlSchemaDatatypeVariety.Union && obj != null)
				{
					XsdSimpleValue xsdSimpleValue = obj as XsdSimpleValue;
					memberType = xsdSimpleValue.XmlType;
					datatype = xsdSimpleValue.XmlType.Datatype;
					obj = xsdSimpleValue.TypedValue;
				}
				this.CheckTokenizedTypes(datatype, obj, true);
				if (this.HasIdentityConstraints)
				{
					this.AttributeIdentityConstraints(xmlQualifiedName.Name, xmlQualifiedName.Namespace, obj, obj2.ToString(), datatype);
				}
			}
			else
			{
				lName = this.nameTable.Add(lName);
				if (Ref.Equal(lName, this.xsiTypeString) || Ref.Equal(lName, this.xsiNilString) || Ref.Equal(lName, this.xsiSchemaLocationString) || Ref.Equal(lName, this.xsiNoNamespaceSchemaLocationString))
				{
					this.attPresence.Add(xmlQualifiedName, SchemaAttDef.Empty);
				}
				else
				{
					this.attrValid = false;
					this.SendValidationEvent("The attribute '{0}' does not match one of the four allowed attributes in the 'xsi' namespace.", xmlQualifiedName.ToString());
				}
			}
			IL_3F5:
			if (!this.attrValid)
			{
				validity = XmlSchemaValidity.Invalid;
			}
			else if (schemaAttDef != null)
			{
				validity = XmlSchemaValidity.Valid;
			}
			if (schemaInfo != null)
			{
				schemaInfo.SchemaAttribute = xmlSchemaAttribute;
				schemaInfo.SchemaType = ((xmlSchemaAttribute == null) ? null : xmlSchemaAttribute.AttributeSchemaType);
				schemaInfo.MemberType = memberType;
				schemaInfo.IsDefault = false;
				schemaInfo.Validity = validity;
			}
			if (this.ProcessSchemaHints && this.validatedNamespaces[ns] == null)
			{
				this.validatedNamespaces.Add(ns, ns);
			}
			return obj;
		}

		/// <summary>Validates identity constraints on the default attributes and populates the <see cref="T:System.Collections.ArrayList" /> specified with <see cref="T:System.Xml.Schema.XmlSchemaAttribute" /> objects for any attributes with default values that have not been previously validated using the <see cref="Overload:System.Xml.Schema.XmlSchemaValidator.ValidateAttribute" /> method in the element context. </summary>
		/// <param name="defaultAttributes">An <see cref="T:System.Collections.ArrayList" /> to populate with <see cref="T:System.Xml.Schema.XmlSchemaAttribute" /> objects for any attributes not yet encountered during validation in the element context.</param>
		// Token: 0x06003CA2 RID: 15522 RVA: 0x00152148 File Offset: 0x00150348
		public void GetUnspecifiedDefaultAttributes(ArrayList defaultAttributes)
		{
			if (defaultAttributes == null)
			{
				throw new ArgumentNullException("defaultAttributes");
			}
			this.CheckStateTransition(ValidatorState.Attribute, "GetUnspecifiedDefaultAttributes");
			this.GetUnspecifiedDefaultAttributes(defaultAttributes, false);
		}

		/// <summary>Verifies whether all the required attributes in the element context are present and prepares the <see cref="T:System.Xml.Schema.XmlSchemaValidator" /> object to validate the child content of the element.</summary>
		/// <param name="schemaInfo">An <see cref="T:System.Xml.Schema.XmlSchemaInfo" /> object whose properties are set on successful verification that all the required attributes in the element context are present. This parameter can be <see langword="null" />.</param>
		/// <exception cref="T:System.Xml.Schema.XmlSchemaValidationException">One or more of the required attributes in the current element context were not found.</exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="M:System.Xml.Schema.XmlSchemaValidator.ValidateEndOfAttributes(System.Xml.Schema.XmlSchemaInfo)" /> method was not called in the correct sequence. For example, calling <see cref="M:System.Xml.Schema.XmlSchemaValidator.ValidateEndOfAttributes(System.Xml.Schema.XmlSchemaInfo)" /> after calling <see cref="M:System.Xml.Schema.XmlSchemaValidator.SkipToEndElement(System.Xml.Schema.XmlSchemaInfo)" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">One or more of the parameters specified are <see langword="null" />.</exception>
		// Token: 0x06003CA3 RID: 15523 RVA: 0x0015216C File Offset: 0x0015036C
		public void ValidateEndOfAttributes(XmlSchemaInfo schemaInfo)
		{
			this.CheckStateTransition(ValidatorState.EndOfAttributes, XmlSchemaValidator.MethodNames[6]);
			SchemaElementDecl elementDecl = this.context.ElementDecl;
			if (elementDecl != null && elementDecl.HasRequiredAttribute)
			{
				this.context.CheckRequiredAttribute = false;
				this.CheckRequiredAttributes(elementDecl);
			}
			if (schemaInfo != null)
			{
				schemaInfo.Validity = this.context.Validity;
			}
		}

		/// <summary>Validates whether the text <see langword="string" /> specified is allowed in the current element context, and accumulates the text for validation if the current element has simple content.</summary>
		/// <param name="elementValue">A text <see langword="string" /> to validate in the current element context.</param>
		/// <exception cref="T:System.Xml.Schema.XmlSchemaValidationException">The text <see langword="string" /> specified is not allowed in the current element context.</exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="Overload:System.Xml.Schema.XmlSchemaValidator.ValidateText" /> method was not called in the correct sequence. For example, the <see cref="Overload:System.Xml.Schema.XmlSchemaValidator.ValidateText" /> method is called after calling <see cref="Overload:System.Xml.Schema.XmlSchemaValidator.ValidateAttribute" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">The text <see langword="string" /> parameter cannot be <see langword="null" />.</exception>
		// Token: 0x06003CA4 RID: 15524 RVA: 0x001521C5 File Offset: 0x001503C5
		public void ValidateText(string elementValue)
		{
			if (elementValue == null)
			{
				throw new ArgumentNullException("elementValue");
			}
			this.ValidateText(elementValue, null);
		}

		/// <summary>Validates whether the text returned by the <see cref="T:System.Xml.Schema.XmlValueGetter" /> object specified is allowed in the current element context, and accumulates the text for validation if the current element has simple content.</summary>
		/// <param name="elementValue">An <see cref="T:System.Xml.Schema.XmlValueGetter" /><see langword="delegate" /> used to pass the text value as a Common Language Runtime (CLR) type compatible with the XML Schema Definition Language (XSD) type of the attribute.</param>
		/// <exception cref="T:System.Xml.Schema.XmlSchemaValidationException">The text <see langword="string" /> specified is not allowed in the current element context.</exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="Overload:System.Xml.Schema.XmlSchemaValidator.ValidateText" /> method was not called in the correct sequence. For example, the <see cref="Overload:System.Xml.Schema.XmlSchemaValidator.ValidateText" /> method is called after calling <see cref="Overload:System.Xml.Schema.XmlSchemaValidator.ValidateAttribute" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">The text <see langword="string" /> parameter cannot be <see langword="null" />.</exception>
		// Token: 0x06003CA5 RID: 15525 RVA: 0x001521DD File Offset: 0x001503DD
		public void ValidateText(XmlValueGetter elementValue)
		{
			if (elementValue == null)
			{
				throw new ArgumentNullException("elementValue");
			}
			this.ValidateText(null, elementValue);
		}

		// Token: 0x06003CA6 RID: 15526 RVA: 0x001521F8 File Offset: 0x001503F8
		private void ValidateText(string elementStringValue, XmlValueGetter elementValueGetter)
		{
			ValidatorState validatorState = (this.validationStack.Length > 1) ? ValidatorState.Text : ValidatorState.TopLevelTextOrWS;
			this.CheckStateTransition(validatorState, XmlSchemaValidator.MethodNames[(int)validatorState]);
			if (this.context.NeedValidateChildren)
			{
				if (this.context.IsNill)
				{
					this.SendValidationEvent("Element '{0}' must have no character or element children.", XmlSchemaValidator.QNameString(this.context.LocalName, this.context.Namespace));
					return;
				}
				switch (this.context.ElementDecl.ContentValidator.ContentType)
				{
				case XmlSchemaContentType.TextOnly:
					if (elementValueGetter != null)
					{
						this.SaveTextValue(elementValueGetter());
						return;
					}
					this.SaveTextValue(elementStringValue);
					return;
				case XmlSchemaContentType.Empty:
					this.SendValidationEvent("The element cannot contain text. Content model is empty.", string.Empty);
					return;
				case XmlSchemaContentType.ElementOnly:
				{
					string str = (elementValueGetter != null) ? elementValueGetter().ToString() : elementStringValue;
					if (!this.xmlCharType.IsOnlyWhitespace(str))
					{
						ArrayList arrayList = this.context.ElementDecl.ContentValidator.ExpectedParticles(this.context, false, this.schemaSet);
						if (arrayList == null || arrayList.Count == 0)
						{
							this.SendValidationEvent("The element {0} cannot contain text.", XmlSchemaValidator.BuildElementName(this.context.LocalName, this.context.Namespace));
							return;
						}
						this.SendValidationEvent("The element {0} cannot contain text. List of possible elements expected: {1}.", new string[]
						{
							XmlSchemaValidator.BuildElementName(this.context.LocalName, this.context.Namespace),
							XmlSchemaValidator.PrintExpectedElements(arrayList, true)
						});
						return;
					}
					break;
				}
				case XmlSchemaContentType.Mixed:
					if (this.context.ElementDecl.DefaultValueTyped != null)
					{
						if (elementValueGetter != null)
						{
							this.SaveTextValue(elementValueGetter());
							return;
						}
						this.SaveTextValue(elementStringValue);
					}
					break;
				default:
					return;
				}
			}
		}

		/// <summary>Validates whether the white space in the <see langword="string" /> specified is allowed in the current element context, and accumulates the white space for validation if the current element has simple content.</summary>
		/// <param name="elementValue">A white space <see langword="string" /> to validate in the current element context.</param>
		/// <exception cref="T:System.Xml.Schema.XmlSchemaValidationException">White space is not allowed in the current element context.</exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="Overload:System.Xml.Schema.XmlSchemaValidator.ValidateWhitespace" /> method was not called in the correct sequence. For example, if the <see cref="Overload:System.Xml.Schema.XmlSchemaValidator.ValidateWhitespace" /> method is called after calling <see cref="Overload:System.Xml.Schema.XmlSchemaValidator.ValidateAttribute" />.</exception>
		// Token: 0x06003CA7 RID: 15527 RVA: 0x0015239B File Offset: 0x0015059B
		public void ValidateWhitespace(string elementValue)
		{
			if (elementValue == null)
			{
				throw new ArgumentNullException("elementValue");
			}
			this.ValidateWhitespace(elementValue, null);
		}

		/// <summary>Validates whether the white space returned by the <see cref="T:System.Xml.Schema.XmlValueGetter" /> object specified is allowed in the current element context, and accumulates the white space for validation if the current element has simple content.</summary>
		/// <param name="elementValue">An <see cref="T:System.Xml.Schema.XmlValueGetter" /><see langword="delegate" /> used to pass the white space value as a Common Language Runtime (CLR) type compatible with the XML Schema Definition Language (XSD) type of the attribute.</param>
		/// <exception cref="T:System.Xml.Schema.XmlSchemaValidationException">White space is not allowed in the current element context.</exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="Overload:System.Xml.Schema.XmlSchemaValidator.ValidateWhitespace" /> method was not called in the correct sequence. For example, if the <see cref="Overload:System.Xml.Schema.XmlSchemaValidator.ValidateWhitespace" /> method is called after calling <see cref="Overload:System.Xml.Schema.XmlSchemaValidator.ValidateAttribute" />.</exception>
		// Token: 0x06003CA8 RID: 15528 RVA: 0x001523B3 File Offset: 0x001505B3
		public void ValidateWhitespace(XmlValueGetter elementValue)
		{
			if (elementValue == null)
			{
				throw new ArgumentNullException("elementValue");
			}
			this.ValidateWhitespace(null, elementValue);
		}

		// Token: 0x06003CA9 RID: 15529 RVA: 0x001523CC File Offset: 0x001505CC
		private void ValidateWhitespace(string elementStringValue, XmlValueGetter elementValueGetter)
		{
			ValidatorState validatorState = (this.validationStack.Length > 1) ? ValidatorState.Whitespace : ValidatorState.TopLevelTextOrWS;
			this.CheckStateTransition(validatorState, XmlSchemaValidator.MethodNames[(int)validatorState]);
			if (this.context.NeedValidateChildren)
			{
				if (this.context.IsNill)
				{
					this.SendValidationEvent("Element '{0}' must have no character or element children.", XmlSchemaValidator.QNameString(this.context.LocalName, this.context.Namespace));
				}
				switch (this.context.ElementDecl.ContentValidator.ContentType)
				{
				case XmlSchemaContentType.TextOnly:
					if (elementValueGetter != null)
					{
						this.SaveTextValue(elementValueGetter());
						return;
					}
					this.SaveTextValue(elementStringValue);
					return;
				case XmlSchemaContentType.Empty:
					this.SendValidationEvent("The element cannot contain white space. Content model is empty.", string.Empty);
					return;
				case XmlSchemaContentType.ElementOnly:
					break;
				case XmlSchemaContentType.Mixed:
					if (this.context.ElementDecl.DefaultValueTyped != null)
					{
						if (elementValueGetter != null)
						{
							this.SaveTextValue(elementValueGetter());
							return;
						}
						this.SaveTextValue(elementStringValue);
					}
					break;
				default:
					return;
				}
			}
		}

		/// <summary>Verifies if the text content of the element is valid according to its data type for elements with simple content, and verifies if the content of the current element is complete for elements with complex content.</summary>
		/// <param name="schemaInfo">An <see cref="T:System.Xml.Schema.XmlSchemaInfo" /> object whose properties are set on successful validation of the element. This parameter can be <see langword="null" />.</param>
		/// <returns>The parsed, typed text value of the element if the element has simple content.</returns>
		/// <exception cref="T:System.Xml.Schema.XmlSchemaValidationException">The element's content is not valid.</exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="Overload:System.Xml.Schema.XmlSchemaValidator.ValidateEndElement" /> method was not called in the correct sequence. For example, if the <see cref="Overload:System.Xml.Schema.XmlSchemaValidator.ValidateEndElement" /> method is called after calling <see cref="M:System.Xml.Schema.XmlSchemaValidator.SkipToEndElement(System.Xml.Schema.XmlSchemaInfo)" />.</exception>
		// Token: 0x06003CAA RID: 15530 RVA: 0x001524BC File Offset: 0x001506BC
		public object ValidateEndElement(XmlSchemaInfo schemaInfo)
		{
			return this.InternalValidateEndElement(schemaInfo, null);
		}

		/// <summary>Verifies if the text content of the element specified is valid according to its data type.</summary>
		/// <param name="schemaInfo">An <see cref="T:System.Xml.Schema.XmlSchemaInfo" /> object whose properties are set on successful validation of the text content of the element. This parameter can be <see langword="null" />.</param>
		/// <param name="typedValue">The typed text content of the element.</param>
		/// <returns>The parsed, typed simple content of the element.</returns>
		/// <exception cref="T:System.Xml.Schema.XmlSchemaValidationException">The element's text content is not valid.</exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="Overload:System.Xml.Schema.XmlSchemaValidator.ValidateEndElement" /> method was not called in the correct sequence (for example, if the <see cref="Overload:System.Xml.Schema.XmlSchemaValidator.ValidateEndElement" /> method is called after calling <see cref="M:System.Xml.Schema.XmlSchemaValidator.SkipToEndElement(System.Xml.Schema.XmlSchemaInfo)" />), calls to the <see cref="Overload:System.Xml.Schema.XmlSchemaValidator.ValidateText" /> method have been previously made, or the element has complex content.</exception>
		/// <exception cref="T:System.ArgumentNullException">The typed text content parameter cannot be <see langword="null" />.</exception>
		// Token: 0x06003CAB RID: 15531 RVA: 0x001524C6 File Offset: 0x001506C6
		public object ValidateEndElement(XmlSchemaInfo schemaInfo, object typedValue)
		{
			if (typedValue == null)
			{
				throw new ArgumentNullException("typedValue");
			}
			if (this.textValue.Length > 0)
			{
				throw new InvalidOperationException(Res.GetString("It is invalid to call the 'ValidateEndElement' overload that takes in a 'typedValue' after 'ValidateText' or 'ValidateWhitespace' methods have been called."));
			}
			return this.InternalValidateEndElement(schemaInfo, typedValue);
		}

		/// <summary>Skips validation of the current element content and prepares the <see cref="T:System.Xml.Schema.XmlSchemaValidator" /> object to validate content in the parent element's context.</summary>
		/// <param name="schemaInfo">An <see cref="T:System.Xml.Schema.XmlSchemaInfo" /> object whose properties are set if the current element content is successfully skipped. This parameter can be <see langword="null" />.</param>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="M:System.Xml.Schema.XmlSchemaValidator.SkipToEndElement(System.Xml.Schema.XmlSchemaInfo)" /> method was not called in the correct sequence. For example, calling <see cref="M:System.Xml.Schema.XmlSchemaValidator.SkipToEndElement(System.Xml.Schema.XmlSchemaInfo)" /> after calling <see cref="M:System.Xml.Schema.XmlSchemaValidator.SkipToEndElement(System.Xml.Schema.XmlSchemaInfo)" />.</exception>
		// Token: 0x06003CAC RID: 15532 RVA: 0x001524FC File Offset: 0x001506FC
		public void SkipToEndElement(XmlSchemaInfo schemaInfo)
		{
			if (this.validationStack.Length <= 1)
			{
				throw new InvalidOperationException(Res.GetString("The call to the '{0}' method does not match a corresponding call to 'ValidateElement' method.", new object[]
				{
					XmlSchemaValidator.MethodNames[10]
				}));
			}
			this.CheckStateTransition(ValidatorState.SkipToEndElement, XmlSchemaValidator.MethodNames[10]);
			if (schemaInfo != null)
			{
				SchemaElementDecl elementDecl = this.context.ElementDecl;
				if (elementDecl != null)
				{
					schemaInfo.SchemaType = elementDecl.SchemaType;
					schemaInfo.SchemaElement = this.GetSchemaElement();
				}
				else
				{
					schemaInfo.SchemaType = null;
					schemaInfo.SchemaElement = null;
				}
				schemaInfo.MemberType = null;
				schemaInfo.IsNil = this.context.IsNill;
				schemaInfo.IsDefault = this.context.IsDefault;
				schemaInfo.Validity = this.context.Validity;
			}
			this.context.ValidationSkipped = true;
			this.currentState = ValidatorState.SkipToEndElement;
			this.Pop();
		}

		/// <summary>Ends validation and checks identity constraints for the entire XML document.</summary>
		/// <exception cref="T:System.Xml.Schema.XmlSchemaValidationException">An identity constraint error was found in the XML document.</exception>
		// Token: 0x06003CAD RID: 15533 RVA: 0x001525D6 File Offset: 0x001507D6
		public void EndValidation()
		{
			if (this.validationStack.Length > 1)
			{
				throw new InvalidOperationException(Res.GetString("The 'EndValidation' method cannot not be called when all the elements have not been validated. 'ValidateEndElement' calls corresponding to 'ValidateElement' calls might be missing."));
			}
			this.CheckStateTransition(ValidatorState.Finish, XmlSchemaValidator.MethodNames[11]);
			this.CheckForwardRefs();
		}

		/// <summary>Returns the expected particles in the current element context.</summary>
		/// <returns>An array of <see cref="T:System.Xml.Schema.XmlSchemaParticle" /> objects or an empty array if there are no expected particles.</returns>
		// Token: 0x06003CAE RID: 15534 RVA: 0x0015260C File Offset: 0x0015080C
		public XmlSchemaParticle[] GetExpectedParticles()
		{
			if (this.currentState != ValidatorState.Start && this.currentState != ValidatorState.TopLevelTextOrWS)
			{
				if (this.context.ElementDecl != null)
				{
					ArrayList arrayList = this.context.ElementDecl.ContentValidator.ExpectedParticles(this.context, false, this.schemaSet);
					if (arrayList != null)
					{
						return arrayList.ToArray(typeof(XmlSchemaParticle)) as XmlSchemaParticle[];
					}
				}
				return XmlSchemaValidator.EmptyParticleArray;
			}
			if (this.partialValidationType == null)
			{
				ICollection values = this.schemaSet.GlobalElements.Values;
				ArrayList arrayList2 = new ArrayList(values.Count);
				foreach (object obj in values)
				{
					ContentValidator.AddParticleToExpected((XmlSchemaElement)obj, this.schemaSet, arrayList2, true);
				}
				return arrayList2.ToArray(typeof(XmlSchemaParticle)) as XmlSchemaParticle[];
			}
			XmlSchemaElement xmlSchemaElement = this.partialValidationType as XmlSchemaElement;
			if (xmlSchemaElement != null)
			{
				return new XmlSchemaParticle[]
				{
					xmlSchemaElement
				};
			}
			return XmlSchemaValidator.EmptyParticleArray;
		}

		/// <summary>Returns the expected attributes for the current element context.</summary>
		/// <returns>An array of <see cref="T:System.Xml.Schema.XmlSchemaAttribute" /> objects or an empty array if there are no expected attributes.</returns>
		// Token: 0x06003CAF RID: 15535 RVA: 0x00152724 File Offset: 0x00150924
		public XmlSchemaAttribute[] GetExpectedAttributes()
		{
			if (this.currentState == ValidatorState.Element || this.currentState == ValidatorState.Attribute)
			{
				SchemaElementDecl elementDecl = this.context.ElementDecl;
				ArrayList arrayList = new ArrayList();
				if (elementDecl != null)
				{
					foreach (SchemaAttDef schemaAttDef in elementDecl.AttDefs.Values)
					{
						if (this.attPresence[schemaAttDef.Name] == null)
						{
							arrayList.Add(schemaAttDef.SchemaAttribute);
						}
					}
				}
				if (this.nsResolver.LookupPrefix(this.NsXsi) != null)
				{
					this.AddXsiAttributes(arrayList);
				}
				return arrayList.ToArray(typeof(XmlSchemaAttribute)) as XmlSchemaAttribute[];
			}
			if (this.currentState == ValidatorState.Start && this.partialValidationType != null)
			{
				XmlSchemaAttribute xmlSchemaAttribute = this.partialValidationType as XmlSchemaAttribute;
				if (xmlSchemaAttribute != null)
				{
					return new XmlSchemaAttribute[]
					{
						xmlSchemaAttribute
					};
				}
			}
			return XmlSchemaValidator.EmptyAttributeArray;
		}

		// Token: 0x06003CB0 RID: 15536 RVA: 0x00152824 File Offset: 0x00150A24
		internal void GetUnspecifiedDefaultAttributes(ArrayList defaultAttributes, bool createNodeData)
		{
			this.currentState = ValidatorState.Attribute;
			SchemaElementDecl elementDecl = this.context.ElementDecl;
			if (elementDecl != null && elementDecl.HasDefaultAttribute)
			{
				for (int i = 0; i < elementDecl.DefaultAttDefs.Count; i++)
				{
					SchemaAttDef schemaAttDef = (SchemaAttDef)elementDecl.DefaultAttDefs[i];
					if (!this.attPresence.Contains(schemaAttDef.Name) && schemaAttDef.DefaultValueTyped != null)
					{
						string text = this.nameTable.Add(schemaAttDef.Name.Namespace);
						string text2 = string.Empty;
						if (text.Length > 0)
						{
							text2 = this.GetDefaultAttributePrefix(text);
							if (text2 == null || text2.Length == 0)
							{
								this.SendValidationEvent("Default attribute '{0}' for element '{1}' could not be applied as the attribute namespace is not mapped to a prefix in the instance document.", new string[]
								{
									schemaAttDef.Name.ToString(),
									XmlSchemaValidator.QNameString(this.context.LocalName, this.context.Namespace)
								});
								goto IL_242;
							}
						}
						XmlSchemaDatatype datatype = schemaAttDef.Datatype;
						if (createNodeData)
						{
							ValidatingReaderNodeData validatingReaderNodeData = new ValidatingReaderNodeData();
							validatingReaderNodeData.LocalName = this.nameTable.Add(schemaAttDef.Name.Name);
							validatingReaderNodeData.Namespace = text;
							validatingReaderNodeData.Prefix = this.nameTable.Add(text2);
							validatingReaderNodeData.NodeType = XmlNodeType.Attribute;
							AttributePSVIInfo attributePSVIInfo = new AttributePSVIInfo();
							XmlSchemaInfo attributeSchemaInfo = attributePSVIInfo.attributeSchemaInfo;
							if (schemaAttDef.Datatype.Variety == XmlSchemaDatatypeVariety.Union)
							{
								XsdSimpleValue xsdSimpleValue = schemaAttDef.DefaultValueTyped as XsdSimpleValue;
								attributeSchemaInfo.MemberType = xsdSimpleValue.XmlType;
								datatype = xsdSimpleValue.XmlType.Datatype;
								attributePSVIInfo.typedAttributeValue = xsdSimpleValue.TypedValue;
							}
							else
							{
								attributePSVIInfo.typedAttributeValue = schemaAttDef.DefaultValueTyped;
							}
							attributeSchemaInfo.IsDefault = true;
							attributeSchemaInfo.Validity = XmlSchemaValidity.Valid;
							attributeSchemaInfo.SchemaType = schemaAttDef.SchemaType;
							attributeSchemaInfo.SchemaAttribute = schemaAttDef.SchemaAttribute;
							validatingReaderNodeData.RawValue = attributeSchemaInfo.XmlType.ValueConverter.ToString(attributePSVIInfo.typedAttributeValue);
							validatingReaderNodeData.AttInfo = attributePSVIInfo;
							defaultAttributes.Add(validatingReaderNodeData);
						}
						else
						{
							defaultAttributes.Add(schemaAttDef.SchemaAttribute);
						}
						this.CheckTokenizedTypes(datatype, schemaAttDef.DefaultValueTyped, true);
						if (this.HasIdentityConstraints)
						{
							this.AttributeIdentityConstraints(schemaAttDef.Name.Name, schemaAttDef.Name.Namespace, schemaAttDef.DefaultValueTyped, schemaAttDef.DefaultValueRaw, datatype);
						}
					}
					IL_242:;
				}
			}
		}

		// Token: 0x17000BE5 RID: 3045
		// (get) Token: 0x06003CB1 RID: 15537 RVA: 0x00152A88 File Offset: 0x00150C88
		internal XmlSchemaSet SchemaSet
		{
			get
			{
				return this.schemaSet;
			}
		}

		// Token: 0x17000BE6 RID: 3046
		// (get) Token: 0x06003CB2 RID: 15538 RVA: 0x00152A90 File Offset: 0x00150C90
		internal XmlSchemaValidationFlags ValidationFlags
		{
			get
			{
				return this.validationFlags;
			}
		}

		// Token: 0x17000BE7 RID: 3047
		// (get) Token: 0x06003CB3 RID: 15539 RVA: 0x00152A98 File Offset: 0x00150C98
		internal XmlSchemaContentType CurrentContentType
		{
			get
			{
				if (this.context.ElementDecl == null)
				{
					return XmlSchemaContentType.Empty;
				}
				return this.context.ElementDecl.ContentValidator.ContentType;
			}
		}

		// Token: 0x17000BE8 RID: 3048
		// (get) Token: 0x06003CB4 RID: 15540 RVA: 0x00152ABE File Offset: 0x00150CBE
		internal XmlSchemaContentProcessing CurrentProcessContents
		{
			get
			{
				return this.processContents;
			}
		}

		// Token: 0x06003CB5 RID: 15541 RVA: 0x00152AC6 File Offset: 0x00150CC6
		internal void SetDtdSchemaInfo(IDtdInfo dtdSchemaInfo)
		{
			this.dtdSchemaInfo = dtdSchemaInfo;
			this.checkEntity = true;
		}

		// Token: 0x17000BE9 RID: 3049
		// (get) Token: 0x06003CB6 RID: 15542 RVA: 0x00152AD6 File Offset: 0x00150CD6
		private bool StrictlyAssessed
		{
			get
			{
				return (this.processContents == XmlSchemaContentProcessing.Strict || this.processContents == XmlSchemaContentProcessing.Lax) && this.context.ElementDecl != null && !this.context.ValidationSkipped;
			}
		}

		// Token: 0x17000BEA RID: 3050
		// (get) Token: 0x06003CB7 RID: 15543 RVA: 0x00152B07 File Offset: 0x00150D07
		private bool HasSchema
		{
			get
			{
				if (this.isRoot)
				{
					this.isRoot = false;
					if (!this.compiledSchemaInfo.Contains(this.context.Namespace))
					{
						this.rootHasSchema = false;
					}
				}
				return this.rootHasSchema;
			}
		}

		// Token: 0x06003CB8 RID: 15544 RVA: 0x00152B3D File Offset: 0x00150D3D
		internal string GetConcatenatedValue()
		{
			return this.textValue.ToString();
		}

		// Token: 0x06003CB9 RID: 15545 RVA: 0x00152B4C File Offset: 0x00150D4C
		private object InternalValidateEndElement(XmlSchemaInfo schemaInfo, object typedValue)
		{
			if (this.validationStack.Length <= 1)
			{
				throw new InvalidOperationException(Res.GetString("The call to the '{0}' method does not match a corresponding call to 'ValidateElement' method.", new object[]
				{
					XmlSchemaValidator.MethodNames[9]
				}));
			}
			this.CheckStateTransition(ValidatorState.EndElement, XmlSchemaValidator.MethodNames[9]);
			SchemaElementDecl elementDecl = this.context.ElementDecl;
			XmlSchemaSimpleType xmlSchemaSimpleType = null;
			XmlSchemaType schemaType = null;
			XmlSchemaElement schemaElement = null;
			string text = string.Empty;
			if (elementDecl != null)
			{
				if (this.context.CheckRequiredAttribute && elementDecl.HasRequiredAttribute)
				{
					this.CheckRequiredAttributes(elementDecl);
				}
				if (!this.context.IsNill && this.context.NeedValidateChildren)
				{
					switch (elementDecl.ContentValidator.ContentType)
					{
					case XmlSchemaContentType.TextOnly:
						if (typedValue == null)
						{
							text = this.textValue.ToString();
							typedValue = this.ValidateAtomicValue(text, out xmlSchemaSimpleType);
						}
						else
						{
							typedValue = this.ValidateAtomicValue(typedValue, out xmlSchemaSimpleType);
						}
						break;
					case XmlSchemaContentType.ElementOnly:
						if (typedValue != null)
						{
							throw new InvalidOperationException(Res.GetString("It is invalid to call the 'ValidateEndElement' overload that takes in a 'typedValue' for elements with complex content."));
						}
						break;
					case XmlSchemaContentType.Mixed:
						if (elementDecl.DefaultValueTyped != null && typedValue == null)
						{
							text = this.textValue.ToString();
							typedValue = this.CheckMixedValueConstraint(text);
						}
						break;
					}
					if (!elementDecl.ContentValidator.CompleteValidation(this.context))
					{
						XmlSchemaValidator.CompleteValidationError(this.context, this.eventHandler, this.nsResolver, this.sourceUriString, this.positionInfo.LineNumber, this.positionInfo.LinePosition, this.schemaSet);
						this.context.Validity = XmlSchemaValidity.Invalid;
					}
				}
				if (this.HasIdentityConstraints)
				{
					XmlSchemaType xmlSchemaType = (xmlSchemaSimpleType == null) ? elementDecl.SchemaType : xmlSchemaSimpleType;
					this.EndElementIdentityConstraints(typedValue, text, xmlSchemaType.Datatype);
				}
				schemaType = elementDecl.SchemaType;
				schemaElement = this.GetSchemaElement();
			}
			if (schemaInfo != null)
			{
				schemaInfo.SchemaType = schemaType;
				schemaInfo.SchemaElement = schemaElement;
				schemaInfo.MemberType = xmlSchemaSimpleType;
				schemaInfo.IsNil = this.context.IsNill;
				schemaInfo.IsDefault = this.context.IsDefault;
				if (this.context.Validity == XmlSchemaValidity.NotKnown && this.StrictlyAssessed)
				{
					this.context.Validity = XmlSchemaValidity.Valid;
				}
				schemaInfo.Validity = this.context.Validity;
			}
			this.Pop();
			return typedValue;
		}

		// Token: 0x06003CBA RID: 15546 RVA: 0x00152D7C File Offset: 0x00150F7C
		private void ProcessSchemaLocations(string xsiSchemaLocation, string xsiNoNamespaceSchemaLocation)
		{
			bool flag = false;
			if (xsiNoNamespaceSchemaLocation != null)
			{
				flag = true;
				this.LoadSchema(string.Empty, xsiNoNamespaceSchemaLocation);
			}
			if (xsiSchemaLocation != null)
			{
				object obj;
				Exception ex = XmlSchemaValidator.dtStringArray.TryParseValue(xsiSchemaLocation, this.nameTable, this.nsResolver, out obj);
				if (ex != null)
				{
					this.SendValidationEvent("The attribute '{0}' has an invalid value '{1}' according to its schema type '{2}' - {3}", new string[]
					{
						"schemaLocation",
						xsiSchemaLocation,
						XmlSchemaValidator.dtStringArray.TypeCodeString,
						ex.Message
					}, ex);
					return;
				}
				string[] array = (string[])obj;
				flag = true;
				try
				{
					for (int i = 0; i < array.Length - 1; i += 2)
					{
						this.LoadSchema(array[i], array[i + 1]);
					}
				}
				catch (XmlSchemaException e)
				{
					this.SendValidationEvent(e);
				}
			}
			if (flag)
			{
				this.RecompileSchemaSet();
			}
		}

		// Token: 0x06003CBB RID: 15547 RVA: 0x00152E48 File Offset: 0x00151048
		private object ValidateElementContext(XmlQualifiedName elementName, out bool invalidElementInContext)
		{
			object obj = null;
			int num = 0;
			invalidElementInContext = false;
			if (this.context.NeedValidateChildren)
			{
				if (this.context.IsNill)
				{
					this.SendValidationEvent("Element '{0}' must have no character or element children.", XmlSchemaValidator.QNameString(this.context.LocalName, this.context.Namespace));
					return null;
				}
				if (this.context.ElementDecl.ContentValidator.ContentType == XmlSchemaContentType.Mixed && this.context.ElementDecl.Presence == SchemaDeclBase.Use.Fixed)
				{
					this.SendValidationEvent("Although the '{0}' element's content type is mixed, it cannot have element children, because it has a fixed value constraint in the schema.", XmlSchemaValidator.QNameString(this.context.LocalName, this.context.Namespace));
					return null;
				}
				XmlQualifiedName xmlQualifiedName = elementName;
				bool flag = false;
				for (;;)
				{
					obj = this.context.ElementDecl.ContentValidator.ValidateElement(xmlQualifiedName, this.context, out num);
					if (obj != null)
					{
						goto IL_111;
					}
					if (num == -2)
					{
						break;
					}
					flag = true;
					XmlSchemaElement substitutionGroupHead = this.GetSubstitutionGroupHead(xmlQualifiedName);
					if (substitutionGroupHead == null)
					{
						goto IL_111;
					}
					xmlQualifiedName = substitutionGroupHead.QualifiedName;
				}
				this.SendValidationEvent("Element '{0}' cannot appear more than once if content model type is \"all\".", elementName.ToString());
				invalidElementInContext = true;
				this.processContents = (this.context.ProcessContents = XmlSchemaContentProcessing.Skip);
				return null;
				IL_111:
				if (flag)
				{
					XmlSchemaElement xmlSchemaElement = obj as XmlSchemaElement;
					if (xmlSchemaElement == null)
					{
						obj = null;
					}
					else if (xmlSchemaElement.RefName.IsEmpty)
					{
						this.SendValidationEvent("The element {0} cannot substitute for a local element {1} expected in that position.", XmlSchemaValidator.BuildElementName(elementName), XmlSchemaValidator.BuildElementName(xmlSchemaElement.QualifiedName));
						invalidElementInContext = true;
						this.processContents = (this.context.ProcessContents = XmlSchemaContentProcessing.Skip);
					}
					else
					{
						obj = this.compiledSchemaInfo.GetElement(elementName);
						this.context.NeedValidateChildren = true;
					}
				}
				if (obj == null)
				{
					XmlSchemaValidator.ElementValidationError(elementName, this.context, this.eventHandler, this.nsResolver, this.sourceUriString, this.positionInfo.LineNumber, this.positionInfo.LinePosition, this.schemaSet);
					invalidElementInContext = true;
					this.processContents = (this.context.ProcessContents = XmlSchemaContentProcessing.Skip);
				}
			}
			return obj;
		}

		// Token: 0x06003CBC RID: 15548 RVA: 0x00153034 File Offset: 0x00151234
		private XmlSchemaElement GetSubstitutionGroupHead(XmlQualifiedName member)
		{
			XmlSchemaElement element = this.compiledSchemaInfo.GetElement(member);
			if (element != null)
			{
				XmlQualifiedName substitutionGroup = element.SubstitutionGroup;
				if (!substitutionGroup.IsEmpty)
				{
					XmlSchemaElement element2 = this.compiledSchemaInfo.GetElement(substitutionGroup);
					if (element2 != null)
					{
						if ((element2.BlockResolved & XmlSchemaDerivationMethod.Substitution) != XmlSchemaDerivationMethod.Empty)
						{
							this.SendValidationEvent("Element '{0}' cannot substitute in place of head element '{1}' because it has block='substitution'.", new string[]
							{
								member.ToString(),
								substitutionGroup.ToString()
							});
							return null;
						}
						if (!XmlSchemaType.IsDerivedFrom(element.ElementSchemaType, element2.ElementSchemaType, element2.BlockResolved))
						{
							this.SendValidationEvent("Member element {0}'s type cannot be derived by restriction or extension from head element {1}'s type, because it has block='restriction' or 'extension'.", new string[]
							{
								member.ToString(),
								substitutionGroup.ToString()
							});
							return null;
						}
						return element2;
					}
				}
			}
			return null;
		}

		// Token: 0x06003CBD RID: 15549 RVA: 0x001530E4 File Offset: 0x001512E4
		private object ValidateAtomicValue(string stringValue, out XmlSchemaSimpleType memberType)
		{
			object obj = null;
			memberType = null;
			SchemaElementDecl elementDecl = this.context.ElementDecl;
			if (!this.context.IsNill)
			{
				if (stringValue.Length == 0 && elementDecl.DefaultValueTyped != null)
				{
					SchemaElementDecl elementDeclBeforeXsi = this.context.ElementDeclBeforeXsi;
					if (elementDeclBeforeXsi != null && elementDeclBeforeXsi != elementDecl)
					{
						if (elementDecl.Datatype.TryParseValue(elementDecl.DefaultValueRaw, this.nameTable, this.nsResolver, out obj) != null)
						{
							this.SendValidationEvent("The default value '{0}' of element '{1}' is invalid according to the type specified by xsi:type.", new string[]
							{
								elementDecl.DefaultValueRaw,
								XmlSchemaValidator.QNameString(this.context.LocalName, this.context.Namespace)
							});
						}
						else
						{
							this.context.IsDefault = true;
						}
					}
					else
					{
						this.context.IsDefault = true;
						obj = elementDecl.DefaultValueTyped;
					}
				}
				else
				{
					obj = this.CheckElementValue(stringValue);
				}
				XsdSimpleValue xsdSimpleValue = obj as XsdSimpleValue;
				XmlSchemaDatatype datatype = elementDecl.Datatype;
				if (xsdSimpleValue != null)
				{
					memberType = xsdSimpleValue.XmlType;
					obj = xsdSimpleValue.TypedValue;
					datatype = memberType.Datatype;
				}
				this.CheckTokenizedTypes(datatype, obj, false);
			}
			return obj;
		}

		// Token: 0x06003CBE RID: 15550 RVA: 0x001531F8 File Offset: 0x001513F8
		private object ValidateAtomicValue(object parsedValue, out XmlSchemaSimpleType memberType)
		{
			memberType = null;
			SchemaElementDecl elementDecl = this.context.ElementDecl;
			object obj = null;
			if (!this.context.IsNill)
			{
				SchemaDeclBase schemaDeclBase = elementDecl;
				XmlSchemaDatatype datatype = elementDecl.Datatype;
				Exception ex = datatype.TryParseValue(parsedValue, this.nameTable, this.nsResolver, out obj);
				if (ex != null)
				{
					string text = parsedValue as string;
					if (text == null)
					{
						text = XmlSchemaDatatype.ConcatenatedToString(parsedValue);
					}
					this.SendValidationEvent("The '{0}' element is invalid - The value '{1}' is invalid according to its datatype '{2}' - {3}", new string[]
					{
						XmlSchemaValidator.QNameString(this.context.LocalName, this.context.Namespace),
						text,
						this.GetTypeName(schemaDeclBase),
						ex.Message
					}, ex);
					return null;
				}
				if (!schemaDeclBase.CheckValue(obj))
				{
					this.SendValidationEvent("The value of the '{0}' element does not equal its fixed value.", XmlSchemaValidator.QNameString(this.context.LocalName, this.context.Namespace));
				}
				if (datatype.Variety == XmlSchemaDatatypeVariety.Union)
				{
					XsdSimpleValue xsdSimpleValue = obj as XsdSimpleValue;
					memberType = xsdSimpleValue.XmlType;
					obj = xsdSimpleValue.TypedValue;
					datatype = memberType.Datatype;
				}
				this.CheckTokenizedTypes(datatype, obj, false);
			}
			return obj;
		}

		// Token: 0x06003CBF RID: 15551 RVA: 0x00153310 File Offset: 0x00151510
		private string GetTypeName(SchemaDeclBase decl)
		{
			string text = decl.SchemaType.QualifiedName.ToString();
			if (text.Length == 0)
			{
				text = decl.Datatype.TypeCodeString;
			}
			return text;
		}

		// Token: 0x06003CC0 RID: 15552 RVA: 0x00153344 File Offset: 0x00151544
		private void SaveTextValue(object value)
		{
			string value2 = value.ToString();
			this.textValue.Append(value2);
		}

		// Token: 0x06003CC1 RID: 15553 RVA: 0x00153368 File Offset: 0x00151568
		private void Push(XmlQualifiedName elementName)
		{
			this.context = (ValidationState)this.validationStack.Push();
			if (this.context == null)
			{
				this.context = new ValidationState();
				this.validationStack.AddToTop(this.context);
			}
			this.context.LocalName = elementName.Name;
			this.context.Namespace = elementName.Namespace;
			this.context.HasMatched = false;
			this.context.IsNill = false;
			this.context.IsDefault = false;
			this.context.CheckRequiredAttribute = true;
			this.context.ValidationSkipped = false;
			this.context.Validity = XmlSchemaValidity.NotKnown;
			this.context.NeedValidateChildren = false;
			this.context.ProcessContents = this.processContents;
			this.context.ElementDeclBeforeXsi = null;
			this.context.Constr = null;
		}

		// Token: 0x06003CC2 RID: 15554 RVA: 0x00153450 File Offset: 0x00151650
		private void Pop()
		{
			ValidationState validationState = (ValidationState)this.validationStack.Pop();
			if (this.startIDConstraint == this.validationStack.Length)
			{
				this.startIDConstraint = -1;
			}
			this.context = (ValidationState)this.validationStack.Peek();
			if (validationState.Validity == XmlSchemaValidity.Invalid)
			{
				this.context.Validity = XmlSchemaValidity.Invalid;
			}
			if (validationState.ValidationSkipped)
			{
				this.context.ValidationSkipped = true;
			}
			this.processContents = this.context.ProcessContents;
		}

		// Token: 0x06003CC3 RID: 15555 RVA: 0x001534D8 File Offset: 0x001516D8
		private void AddXsiAttributes(ArrayList attList)
		{
			XmlSchemaValidator.BuildXsiAttributes();
			if (this.attPresence[XmlSchemaValidator.xsiTypeSO.QualifiedName] == null)
			{
				attList.Add(XmlSchemaValidator.xsiTypeSO);
			}
			if (this.attPresence[XmlSchemaValidator.xsiNilSO.QualifiedName] == null)
			{
				attList.Add(XmlSchemaValidator.xsiNilSO);
			}
			if (this.attPresence[XmlSchemaValidator.xsiSLSO.QualifiedName] == null)
			{
				attList.Add(XmlSchemaValidator.xsiSLSO);
			}
			if (this.attPresence[XmlSchemaValidator.xsiNoNsSLSO.QualifiedName] == null)
			{
				attList.Add(XmlSchemaValidator.xsiNoNsSLSO);
			}
		}

		// Token: 0x06003CC4 RID: 15556 RVA: 0x00153578 File Offset: 0x00151778
		private SchemaElementDecl FastGetElementDecl(XmlQualifiedName elementName, object particle)
		{
			SchemaElementDecl schemaElementDecl = null;
			if (particle != null)
			{
				XmlSchemaElement xmlSchemaElement = particle as XmlSchemaElement;
				if (xmlSchemaElement != null)
				{
					schemaElementDecl = xmlSchemaElement.ElementDecl;
				}
				else
				{
					XmlSchemaAny xmlSchemaAny = (XmlSchemaAny)particle;
					this.processContents = xmlSchemaAny.ProcessContentsCorrect;
				}
			}
			if (schemaElementDecl == null && this.processContents != XmlSchemaContentProcessing.Skip)
			{
				if (this.isRoot && this.partialValidationType != null)
				{
					if (this.partialValidationType is XmlSchemaElement)
					{
						XmlSchemaElement xmlSchemaElement2 = (XmlSchemaElement)this.partialValidationType;
						if (elementName.Equals(xmlSchemaElement2.QualifiedName))
						{
							schemaElementDecl = xmlSchemaElement2.ElementDecl;
						}
						else
						{
							this.SendValidationEvent("The element name '{0}' does not match the name '{1}' of the 'XmlSchemaElement' set as a partial validation type.", elementName.ToString(), xmlSchemaElement2.QualifiedName.ToString());
						}
					}
					else if (this.partialValidationType is XmlSchemaType)
					{
						schemaElementDecl = ((XmlSchemaType)this.partialValidationType).ElementDecl;
					}
					else
					{
						this.SendValidationEvent("If the partial validation type is 'XmlSchemaAttribute', the 'ValidateElement' method cannot be called.", string.Empty);
					}
				}
				else
				{
					schemaElementDecl = this.compiledSchemaInfo.GetElementDecl(elementName);
				}
			}
			return schemaElementDecl;
		}

		// Token: 0x06003CC5 RID: 15557 RVA: 0x00153668 File Offset: 0x00151868
		private SchemaElementDecl CheckXsiTypeAndNil(SchemaElementDecl elementDecl, string xsiType, string xsiNil, ref bool declFound)
		{
			XmlQualifiedName xmlQualifiedName = XmlQualifiedName.Empty;
			if (xsiType != null)
			{
				object obj = null;
				Exception ex = XmlSchemaValidator.dtQName.TryParseValue(xsiType, this.nameTable, this.nsResolver, out obj);
				if (ex != null)
				{
					this.SendValidationEvent("The attribute '{0}' has an invalid value '{1}' according to its schema type '{2}' - {3}", new string[]
					{
						"type",
						xsiType,
						XmlSchemaValidator.dtQName.TypeCodeString,
						ex.Message
					}, ex);
				}
				else
				{
					xmlQualifiedName = (obj as XmlQualifiedName);
				}
			}
			if (elementDecl != null)
			{
				if (elementDecl.IsNillable)
				{
					if (xsiNil != null)
					{
						this.context.IsNill = XmlConvert.ToBoolean(xsiNil);
						if (this.context.IsNill && elementDecl.Presence == SchemaDeclBase.Use.Fixed)
						{
							this.SendValidationEvent("There must be no fixed value when an attribute is 'xsi:nil' and has a value of 'true'.");
						}
					}
				}
				else if (xsiNil != null)
				{
					this.SendValidationEvent("If the 'nillable' attribute is false in the schema, the 'xsi:nil' attribute must not be present in the instance.");
				}
			}
			if (xmlQualifiedName.IsEmpty)
			{
				if (elementDecl != null && elementDecl.IsAbstract)
				{
					this.SendValidationEvent("The element '{0}' is abstract or its type is abstract.", XmlSchemaValidator.QNameString(this.context.LocalName, this.context.Namespace));
					elementDecl = null;
				}
			}
			else
			{
				SchemaElementDecl schemaElementDecl = this.compiledSchemaInfo.GetTypeDecl(xmlQualifiedName);
				XmlSeverityType severity = XmlSeverityType.Warning;
				if (this.HasSchema && this.processContents == XmlSchemaContentProcessing.Strict)
				{
					severity = XmlSeverityType.Error;
				}
				if (schemaElementDecl == null && xmlQualifiedName.Namespace == this.NsXs)
				{
					XmlSchemaType xmlSchemaType = DatatypeImplementation.GetSimpleTypeFromXsdType(xmlQualifiedName);
					if (xmlSchemaType == null)
					{
						xmlSchemaType = XmlSchemaType.GetBuiltInComplexType(xmlQualifiedName);
					}
					if (xmlSchemaType != null)
					{
						schemaElementDecl = xmlSchemaType.ElementDecl;
					}
				}
				if (schemaElementDecl == null)
				{
					this.SendValidationEvent("This is an invalid xsi:type '{0}'.", xmlQualifiedName.ToString(), severity);
					elementDecl = null;
				}
				else
				{
					declFound = true;
					if (schemaElementDecl.IsAbstract)
					{
						this.SendValidationEvent("The xsi:type '{0}' cannot be abstract.", xmlQualifiedName.ToString(), severity);
						elementDecl = null;
					}
					else if (elementDecl != null && !XmlSchemaType.IsDerivedFrom(schemaElementDecl.SchemaType, elementDecl.SchemaType, elementDecl.Block))
					{
						this.SendValidationEvent("The xsi:type attribute value '{0}' is not valid for the element '{1}', either because it is not a type validly derived from the type in the schema, or because it has xsi:type derivation blocked.", new string[]
						{
							xmlQualifiedName.ToString(),
							XmlSchemaValidator.QNameString(this.context.LocalName, this.context.Namespace)
						});
						elementDecl = null;
					}
					else
					{
						if (elementDecl != null)
						{
							schemaElementDecl = schemaElementDecl.Clone();
							schemaElementDecl.Constraints = elementDecl.Constraints;
							schemaElementDecl.DefaultValueRaw = elementDecl.DefaultValueRaw;
							schemaElementDecl.DefaultValueTyped = elementDecl.DefaultValueTyped;
							schemaElementDecl.Block = elementDecl.Block;
						}
						this.context.ElementDeclBeforeXsi = elementDecl;
						elementDecl = schemaElementDecl;
					}
				}
			}
			return elementDecl;
		}

		// Token: 0x06003CC6 RID: 15558 RVA: 0x001538B4 File Offset: 0x00151AB4
		private void ThrowDeclNotFoundWarningOrError(bool declFound)
		{
			if (declFound)
			{
				this.processContents = (this.context.ProcessContents = XmlSchemaContentProcessing.Skip);
				this.context.NeedValidateChildren = false;
				return;
			}
			if (this.HasSchema && this.processContents == XmlSchemaContentProcessing.Strict)
			{
				this.processContents = (this.context.ProcessContents = XmlSchemaContentProcessing.Skip);
				this.context.NeedValidateChildren = false;
				this.SendValidationEvent("The '{0}' element is not declared.", XmlSchemaValidator.QNameString(this.context.LocalName, this.context.Namespace));
				return;
			}
			this.SendValidationEvent("Could not find schema information for the element '{0}'.", XmlSchemaValidator.QNameString(this.context.LocalName, this.context.Namespace), XmlSeverityType.Warning);
		}

		// Token: 0x06003CC7 RID: 15559 RVA: 0x00153966 File Offset: 0x00151B66
		private void CheckElementProperties()
		{
			if (this.context.ElementDecl.IsAbstract)
			{
				this.SendValidationEvent("The element '{0}' is abstract or its type is abstract.", XmlSchemaValidator.QNameString(this.context.LocalName, this.context.Namespace));
			}
		}

		// Token: 0x06003CC8 RID: 15560 RVA: 0x001539A0 File Offset: 0x00151BA0
		private void ValidateStartElementIdentityConstraints()
		{
			if (this.ProcessIdentityConstraints && this.context.ElementDecl.Constraints != null)
			{
				this.AddIdentityConstraints();
			}
			if (this.HasIdentityConstraints)
			{
				this.ElementIdentityConstraints();
			}
		}

		// Token: 0x06003CC9 RID: 15561 RVA: 0x001539D0 File Offset: 0x00151BD0
		private SchemaAttDef CheckIsXmlAttribute(XmlQualifiedName attQName)
		{
			SchemaAttDef result = null;
			if (Ref.Equal(attQName.Namespace, this.NsXml) && (this.validationFlags & XmlSchemaValidationFlags.AllowXmlAttributes) != XmlSchemaValidationFlags.None)
			{
				if (!this.compiledSchemaInfo.Contains(this.NsXml))
				{
					this.AddXmlNamespaceSchema();
				}
				this.compiledSchemaInfo.AttributeDecls.TryGetValue(attQName, out result);
			}
			return result;
		}

		// Token: 0x06003CCA RID: 15562 RVA: 0x00153A2C File Offset: 0x00151C2C
		private void AddXmlNamespaceSchema()
		{
			XmlSchemaSet xmlSchemaSet = new XmlSchemaSet();
			xmlSchemaSet.Add(Preprocessor.GetBuildInSchema());
			xmlSchemaSet.Compile();
			this.schemaSet.Add(xmlSchemaSet);
			this.RecompileSchemaSet();
		}

		// Token: 0x06003CCB RID: 15563 RVA: 0x00153A64 File Offset: 0x00151C64
		internal object CheckMixedValueConstraint(string elementValue)
		{
			SchemaElementDecl elementDecl = this.context.ElementDecl;
			if (this.context.IsNill)
			{
				return null;
			}
			if (elementValue.Length == 0)
			{
				this.context.IsDefault = true;
				return elementDecl.DefaultValueTyped;
			}
			if (elementDecl.Presence == SchemaDeclBase.Use.Fixed && !elementValue.Equals(elementDecl.DefaultValueRaw))
			{
				this.SendValidationEvent("The value of the '{0}' element does not equal its fixed value.", elementDecl.Name.ToString());
			}
			return elementValue;
		}

		// Token: 0x06003CCC RID: 15564 RVA: 0x00153AD8 File Offset: 0x00151CD8
		private void LoadSchema(string uri, string url)
		{
			XmlReader xmlReader = null;
			try
			{
				Uri uri2 = this.xmlResolver.ResolveUri(this.sourceUri, url);
				Stream input = (Stream)this.xmlResolver.GetEntity(uri2, null, null);
				XmlReaderSettings readerSettings = this.schemaSet.ReaderSettings;
				readerSettings.CloseInput = true;
				readerSettings.XmlResolver = this.xmlResolver;
				xmlReader = XmlReader.Create(input, readerSettings, uri2.ToString());
				this.schemaSet.Add(uri, xmlReader, this.validatedNamespaces);
				while (xmlReader.Read())
				{
				}
			}
			catch (XmlSchemaException ex)
			{
				this.SendValidationEvent("Cannot load the schema for the namespace '{0}' - {1}", new string[]
				{
					uri,
					ex.Message
				}, ex);
			}
			catch (Exception ex2)
			{
				this.SendValidationEvent("Cannot load the schema for the namespace '{0}' - {1}", new string[]
				{
					uri,
					ex2.Message
				}, ex2, XmlSeverityType.Warning);
			}
			finally
			{
				if (xmlReader != null)
				{
					xmlReader.Close();
				}
			}
		}

		// Token: 0x06003CCD RID: 15565 RVA: 0x00153BD0 File Offset: 0x00151DD0
		internal void RecompileSchemaSet()
		{
			if (!this.schemaSet.IsCompiled)
			{
				try
				{
					this.schemaSet.Compile();
				}
				catch (XmlSchemaException e)
				{
					this.SendValidationEvent(e);
				}
			}
			this.compiledSchemaInfo = this.schemaSet.CompiledInfo;
		}

		// Token: 0x06003CCE RID: 15566 RVA: 0x00153C24 File Offset: 0x00151E24
		private void ProcessTokenizedType(XmlTokenizedType ttype, string name, bool attrValue)
		{
			switch (ttype)
			{
			case XmlTokenizedType.ID:
				if (this.ProcessIdentityConstraints)
				{
					if (this.FindId(name) != null)
					{
						if (attrValue)
						{
							this.attrValid = false;
						}
						this.SendValidationEvent("'{0}' is already used as an ID.", name);
						return;
					}
					if (this.IDs == null)
					{
						this.IDs = new Hashtable();
					}
					this.IDs.Add(name, this.context.LocalName);
					return;
				}
				break;
			case XmlTokenizedType.IDREF:
				if (this.ProcessIdentityConstraints && this.FindId(name) == null)
				{
					this.idRefListHead = new IdRefNode(this.idRefListHead, name, this.positionInfo.LineNumber, this.positionInfo.LinePosition);
					return;
				}
				break;
			case XmlTokenizedType.IDREFS:
				break;
			case XmlTokenizedType.ENTITY:
				this.ProcessEntity(name);
				break;
			default:
				return;
			}
		}

		// Token: 0x06003CCF RID: 15567 RVA: 0x00153CE4 File Offset: 0x00151EE4
		private object CheckAttributeValue(object value, SchemaAttDef attdef)
		{
			object obj = null;
			XmlSchemaDatatype datatype = attdef.Datatype;
			string text = value as string;
			Exception ex;
			if (text != null)
			{
				ex = datatype.TryParseValue(text, this.nameTable, this.nsResolver, out obj);
				if (ex != null)
				{
					goto IL_78;
				}
			}
			else
			{
				ex = datatype.TryParseValue(value, this.nameTable, this.nsResolver, out obj);
				if (ex != null)
				{
					goto IL_78;
				}
			}
			if (!attdef.CheckValue(obj))
			{
				this.attrValid = false;
				this.SendValidationEvent("The value of the '{0}' attribute does not equal its fixed value.", attdef.Name.ToString());
			}
			return obj;
			IL_78:
			this.attrValid = false;
			if (text == null)
			{
				text = XmlSchemaDatatype.ConcatenatedToString(value);
			}
			this.SendValidationEvent("The '{0}' attribute is invalid - The value '{1}' is invalid according to its datatype '{2}' - {3}", new string[]
			{
				attdef.Name.ToString(),
				text,
				this.GetTypeName(attdef),
				ex.Message
			}, ex);
			return null;
		}

		// Token: 0x06003CD0 RID: 15568 RVA: 0x00153DB4 File Offset: 0x00151FB4
		private object CheckElementValue(string stringValue)
		{
			object obj = null;
			SchemaDeclBase elementDecl = this.context.ElementDecl;
			Exception ex = elementDecl.Datatype.TryParseValue(stringValue, this.nameTable, this.nsResolver, out obj);
			if (ex != null)
			{
				this.SendValidationEvent("The '{0}' element is invalid - The value '{1}' is invalid according to its datatype '{2}' - {3}", new string[]
				{
					XmlSchemaValidator.QNameString(this.context.LocalName, this.context.Namespace),
					stringValue,
					this.GetTypeName(elementDecl),
					ex.Message
				}, ex);
				return null;
			}
			if (!elementDecl.CheckValue(obj))
			{
				this.SendValidationEvent("The value of the '{0}' element does not equal its fixed value.", XmlSchemaValidator.QNameString(this.context.LocalName, this.context.Namespace));
			}
			return obj;
		}

		// Token: 0x06003CD1 RID: 15569 RVA: 0x00153E68 File Offset: 0x00152068
		private void CheckTokenizedTypes(XmlSchemaDatatype dtype, object typedValue, bool attrValue)
		{
			if (typedValue == null)
			{
				return;
			}
			XmlTokenizedType tokenizedType = dtype.TokenizedType;
			if (tokenizedType == XmlTokenizedType.ENTITY || tokenizedType == XmlTokenizedType.ID || tokenizedType == XmlTokenizedType.IDREF)
			{
				if (dtype.Variety == XmlSchemaDatatypeVariety.List)
				{
					string[] array = (string[])typedValue;
					for (int i = 0; i < array.Length; i++)
					{
						this.ProcessTokenizedType(dtype.TokenizedType, array[i], attrValue);
					}
					return;
				}
				this.ProcessTokenizedType(dtype.TokenizedType, (string)typedValue, attrValue);
			}
		}

		// Token: 0x06003CD2 RID: 15570 RVA: 0x00153ECE File Offset: 0x001520CE
		private object FindId(string name)
		{
			if (this.IDs != null)
			{
				return this.IDs[name];
			}
			return null;
		}

		// Token: 0x06003CD3 RID: 15571 RVA: 0x00153EE8 File Offset: 0x001520E8
		private void CheckForwardRefs()
		{
			IdRefNode next;
			for (IdRefNode idRefNode = this.idRefListHead; idRefNode != null; idRefNode = next)
			{
				if (this.FindId(idRefNode.Id) == null)
				{
					this.SendValidationEvent(new XmlSchemaValidationException("Reference to undeclared ID is '{0}'.", idRefNode.Id, this.sourceUriString, idRefNode.LineNo, idRefNode.LinePos), XmlSeverityType.Error);
				}
				next = idRefNode.Next;
				idRefNode.Next = null;
			}
			this.idRefListHead = null;
		}

		// Token: 0x17000BEB RID: 3051
		// (get) Token: 0x06003CD4 RID: 15572 RVA: 0x00153F4D File Offset: 0x0015214D
		private bool HasIdentityConstraints
		{
			get
			{
				return this.ProcessIdentityConstraints && this.startIDConstraint != -1;
			}
		}

		// Token: 0x17000BEC RID: 3052
		// (get) Token: 0x06003CD5 RID: 15573 RVA: 0x00153F65 File Offset: 0x00152165
		internal bool ProcessIdentityConstraints
		{
			get
			{
				return (this.validationFlags & XmlSchemaValidationFlags.ProcessIdentityConstraints) > XmlSchemaValidationFlags.None;
			}
		}

		// Token: 0x17000BED RID: 3053
		// (get) Token: 0x06003CD6 RID: 15574 RVA: 0x00153F72 File Offset: 0x00152172
		internal bool ReportValidationWarnings
		{
			get
			{
				return (this.validationFlags & XmlSchemaValidationFlags.ReportValidationWarnings) > XmlSchemaValidationFlags.None;
			}
		}

		// Token: 0x17000BEE RID: 3054
		// (get) Token: 0x06003CD7 RID: 15575 RVA: 0x00153F7F File Offset: 0x0015217F
		internal bool ProcessInlineSchema
		{
			get
			{
				return (this.validationFlags & XmlSchemaValidationFlags.ProcessInlineSchema) > XmlSchemaValidationFlags.None;
			}
		}

		// Token: 0x17000BEF RID: 3055
		// (get) Token: 0x06003CD8 RID: 15576 RVA: 0x00153F8C File Offset: 0x0015218C
		internal bool ProcessSchemaLocation
		{
			get
			{
				return (this.validationFlags & XmlSchemaValidationFlags.ProcessSchemaLocation) > XmlSchemaValidationFlags.None;
			}
		}

		// Token: 0x17000BF0 RID: 3056
		// (get) Token: 0x06003CD9 RID: 15577 RVA: 0x00153F99 File Offset: 0x00152199
		internal bool ProcessSchemaHints
		{
			get
			{
				return (this.validationFlags & XmlSchemaValidationFlags.ProcessInlineSchema) != XmlSchemaValidationFlags.None || (this.validationFlags & XmlSchemaValidationFlags.ProcessSchemaLocation) > XmlSchemaValidationFlags.None;
			}
		}

		// Token: 0x06003CDA RID: 15578 RVA: 0x00153FB4 File Offset: 0x001521B4
		private void CheckStateTransition(ValidatorState toState, string methodName)
		{
			if (XmlSchemaValidator.ValidStates[(int)this.currentState, (int)toState])
			{
				this.currentState = toState;
				return;
			}
			object[] args;
			if (this.currentState == ValidatorState.None)
			{
				string name = "It is invalid to call the '{0}' method in the current state of the validator. The '{1}' method must be called before proceeding with validation.";
				args = new string[]
				{
					methodName,
					XmlSchemaValidator.MethodNames[1]
				};
				throw new InvalidOperationException(Res.GetString(name, args));
			}
			string name2 = "The transition from the '{0}' method to the '{1}' method is not allowed.";
			args = new string[]
			{
				XmlSchemaValidator.MethodNames[(int)this.currentState],
				methodName
			};
			throw new InvalidOperationException(Res.GetString(name2, args));
		}

		// Token: 0x06003CDB RID: 15579 RVA: 0x00154034 File Offset: 0x00152234
		private void ClearPSVI()
		{
			if (this.textValue != null)
			{
				this.textValue.Length = 0;
			}
			this.attPresence.Clear();
			this.wildID = null;
		}

		// Token: 0x06003CDC RID: 15580 RVA: 0x0015405C File Offset: 0x0015225C
		private void CheckRequiredAttributes(SchemaElementDecl currentElementDecl)
		{
			foreach (SchemaAttDef schemaAttDef in currentElementDecl.AttDefs.Values)
			{
				if (this.attPresence[schemaAttDef.Name] == null && (schemaAttDef.Presence == SchemaDeclBase.Use.Required || schemaAttDef.Presence == SchemaDeclBase.Use.RequiredFixed))
				{
					this.SendValidationEvent("The required attribute '{0}' is missing.", schemaAttDef.Name.ToString());
				}
			}
		}

		// Token: 0x06003CDD RID: 15581 RVA: 0x001540E8 File Offset: 0x001522E8
		private XmlSchemaElement GetSchemaElement()
		{
			SchemaElementDecl elementDeclBeforeXsi = this.context.ElementDeclBeforeXsi;
			SchemaElementDecl elementDecl = this.context.ElementDecl;
			if (elementDeclBeforeXsi != null && elementDeclBeforeXsi.SchemaElement != null)
			{
				XmlSchemaElement xmlSchemaElement = (XmlSchemaElement)elementDeclBeforeXsi.SchemaElement.Clone(null);
				xmlSchemaElement.SchemaTypeName = XmlQualifiedName.Empty;
				xmlSchemaElement.SchemaType = elementDecl.SchemaType;
				xmlSchemaElement.SetElementType(elementDecl.SchemaType);
				xmlSchemaElement.ElementDecl = elementDecl;
				return xmlSchemaElement;
			}
			return elementDecl.SchemaElement;
		}

		// Token: 0x06003CDE RID: 15582 RVA: 0x0015415C File Offset: 0x0015235C
		internal string GetDefaultAttributePrefix(string attributeNS)
		{
			IEnumerable<KeyValuePair<string, string>> namespacesInScope = this.nsResolver.GetNamespacesInScope(XmlNamespaceScope.All);
			string text = null;
			foreach (KeyValuePair<string, string> keyValuePair in namespacesInScope)
			{
				if (Ref.Equal(this.nameTable.Add(keyValuePair.Value), attributeNS))
				{
					text = keyValuePair.Key;
					if (text.Length != 0)
					{
						return text;
					}
				}
			}
			return text;
		}

		// Token: 0x06003CDF RID: 15583 RVA: 0x001541DC File Offset: 0x001523DC
		private void AddIdentityConstraints()
		{
			SchemaElementDecl elementDecl = this.context.ElementDecl;
			this.context.Constr = new ConstraintStruct[elementDecl.Constraints.Length];
			int num = 0;
			for (int i = 0; i < elementDecl.Constraints.Length; i++)
			{
				this.context.Constr[num++] = new ConstraintStruct(elementDecl.Constraints[i]);
			}
			for (int j = 0; j < this.context.Constr.Length; j++)
			{
				if (this.context.Constr[j].constraint.Role == CompiledIdentityConstraint.ConstraintRole.Keyref)
				{
					bool flag = false;
					for (int k = this.validationStack.Length - 1; k >= ((this.startIDConstraint >= 0) ? this.startIDConstraint : (this.validationStack.Length - 1)); k--)
					{
						if (((ValidationState)this.validationStack[k]).Constr != null)
						{
							ConstraintStruct[] constr = ((ValidationState)this.validationStack[k]).Constr;
							for (int l = 0; l < constr.Length; l++)
							{
								if (constr[l].constraint.name == this.context.Constr[j].constraint.refer)
								{
									flag = true;
									if (constr[l].keyrefTable == null)
									{
										constr[l].keyrefTable = new Hashtable();
									}
									this.context.Constr[j].qualifiedTable = constr[l].keyrefTable;
									break;
								}
							}
							if (flag)
							{
								break;
							}
						}
					}
					if (!flag)
					{
						this.SendValidationEvent("The Keyref '{0}' cannot find the referred key or unique in scope.", XmlSchemaValidator.QNameString(this.context.LocalName, this.context.Namespace));
					}
				}
			}
			if (this.startIDConstraint == -1)
			{
				this.startIDConstraint = this.validationStack.Length - 1;
			}
		}

		// Token: 0x06003CE0 RID: 15584 RVA: 0x001543B8 File Offset: 0x001525B8
		private void ElementIdentityConstraints()
		{
			SchemaElementDecl elementDecl = this.context.ElementDecl;
			string localName = this.context.LocalName;
			string @namespace = this.context.Namespace;
			for (int i = this.startIDConstraint; i < this.validationStack.Length; i++)
			{
				if (((ValidationState)this.validationStack[i]).Constr != null)
				{
					ConstraintStruct[] constr = ((ValidationState)this.validationStack[i]).Constr;
					for (int j = 0; j < constr.Length; j++)
					{
						if (constr[j].axisSelector.MoveToStartElement(localName, @namespace))
						{
							constr[j].axisSelector.PushKS(this.positionInfo.LineNumber, this.positionInfo.LinePosition);
						}
						for (int k = 0; k < constr[j].axisFields.Count; k++)
						{
							LocatedActiveAxis locatedActiveAxis = (LocatedActiveAxis)constr[j].axisFields[k];
							if (locatedActiveAxis.MoveToStartElement(localName, @namespace) && elementDecl != null)
							{
								if (elementDecl.Datatype == null || elementDecl.ContentValidator.ContentType == XmlSchemaContentType.Mixed)
								{
									this.SendValidationEvent("The field '{0}' is expecting an element or attribute with simple type or simple content.", localName);
								}
								else
								{
									locatedActiveAxis.isMatched = true;
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x06003CE1 RID: 15585 RVA: 0x00154500 File Offset: 0x00152700
		private void AttributeIdentityConstraints(string name, string ns, object obj, string sobj, XmlSchemaDatatype datatype)
		{
			for (int i = this.startIDConstraint; i < this.validationStack.Length; i++)
			{
				if (((ValidationState)this.validationStack[i]).Constr != null)
				{
					ConstraintStruct[] constr = ((ValidationState)this.validationStack[i]).Constr;
					for (int j = 0; j < constr.Length; j++)
					{
						for (int k = 0; k < constr[j].axisFields.Count; k++)
						{
							LocatedActiveAxis locatedActiveAxis = (LocatedActiveAxis)constr[j].axisFields[k];
							if (locatedActiveAxis.MoveToAttribute(name, ns))
							{
								if (locatedActiveAxis.Ks[locatedActiveAxis.Column] != null)
								{
									this.SendValidationEvent("The field '{0}' is expecting at the most one value.", name);
								}
								else
								{
									locatedActiveAxis.Ks[locatedActiveAxis.Column] = new TypedObject(obj, sobj, datatype);
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x06003CE2 RID: 15586 RVA: 0x001545EC File Offset: 0x001527EC
		private void EndElementIdentityConstraints(object typedValue, string stringValue, XmlSchemaDatatype datatype)
		{
			string localName = this.context.LocalName;
			string @namespace = this.context.Namespace;
			for (int i = this.validationStack.Length - 1; i >= this.startIDConstraint; i--)
			{
				if (((ValidationState)this.validationStack[i]).Constr != null)
				{
					ConstraintStruct[] constr = ((ValidationState)this.validationStack[i]).Constr;
					for (int j = 0; j < constr.Length; j++)
					{
						for (int k = 0; k < constr[j].axisFields.Count; k++)
						{
							LocatedActiveAxis locatedActiveAxis = (LocatedActiveAxis)constr[j].axisFields[k];
							if (locatedActiveAxis.isMatched)
							{
								locatedActiveAxis.isMatched = false;
								if (locatedActiveAxis.Ks[locatedActiveAxis.Column] != null)
								{
									this.SendValidationEvent("The field '{0}' is expecting at the most one value.", localName);
								}
								else if (LocalAppContextSwitches.IgnoreEmptyKeySequences)
								{
									if (typedValue != null && stringValue.Length != 0)
									{
										locatedActiveAxis.Ks[locatedActiveAxis.Column] = new TypedObject(typedValue, stringValue, datatype);
									}
								}
								else if (typedValue != null)
								{
									locatedActiveAxis.Ks[locatedActiveAxis.Column] = new TypedObject(typedValue, stringValue, datatype);
								}
							}
							locatedActiveAxis.EndElement(localName, @namespace);
						}
						if (constr[j].axisSelector.EndElement(localName, @namespace))
						{
							KeySequence keySequence = constr[j].axisSelector.PopKS();
							switch (constr[j].constraint.Role)
							{
							case CompiledIdentityConstraint.ConstraintRole.Unique:
								if (keySequence.IsQualified())
								{
									if (constr[j].qualifiedTable.Contains(keySequence))
									{
										this.SendValidationEvent(new XmlSchemaValidationException("There is a duplicate key sequence '{0}' for the '{1}' key or unique identity constraint.", new string[]
										{
											keySequence.ToString(),
											constr[j].constraint.name.ToString()
										}, this.sourceUriString, keySequence.PosLine, keySequence.PosCol));
									}
									else
									{
										constr[j].qualifiedTable.Add(keySequence, keySequence);
									}
								}
								break;
							case CompiledIdentityConstraint.ConstraintRole.Key:
								if (!keySequence.IsQualified())
								{
									this.SendValidationEvent(new XmlSchemaValidationException("The identity constraint '{0}' validation has failed. Either a key is missing or the existing key has an empty node.", constr[j].constraint.name.ToString(), this.sourceUriString, keySequence.PosLine, keySequence.PosCol));
								}
								else if (constr[j].qualifiedTable.Contains(keySequence))
								{
									this.SendValidationEvent(new XmlSchemaValidationException("There is a duplicate key sequence '{0}' for the '{1}' key or unique identity constraint.", new string[]
									{
										keySequence.ToString(),
										constr[j].constraint.name.ToString()
									}, this.sourceUriString, keySequence.PosLine, keySequence.PosCol));
								}
								else
								{
									constr[j].qualifiedTable.Add(keySequence, keySequence);
								}
								break;
							case CompiledIdentityConstraint.ConstraintRole.Keyref:
								if (constr[j].qualifiedTable != null && keySequence.IsQualified() && !constr[j].qualifiedTable.Contains(keySequence))
								{
									constr[j].qualifiedTable.Add(keySequence, keySequence);
								}
								break;
							}
						}
					}
				}
			}
			ConstraintStruct[] constr2 = ((ValidationState)this.validationStack[this.validationStack.Length - 1]).Constr;
			if (constr2 != null)
			{
				for (int l = 0; l < constr2.Length; l++)
				{
					if (constr2[l].constraint.Role != CompiledIdentityConstraint.ConstraintRole.Keyref && constr2[l].keyrefTable != null)
					{
						foreach (object obj in constr2[l].keyrefTable.Keys)
						{
							KeySequence keySequence2 = (KeySequence)obj;
							if (!constr2[l].qualifiedTable.Contains(keySequence2))
							{
								this.SendValidationEvent(new XmlSchemaValidationException("The key sequence '{0}' in '{1}' Keyref fails to refer to some key.", new string[]
								{
									keySequence2.ToString(),
									constr2[l].constraint.name.ToString()
								}, this.sourceUriString, keySequence2.PosLine, keySequence2.PosCol));
							}
						}
					}
				}
			}
		}

		// Token: 0x06003CE3 RID: 15587 RVA: 0x00154A28 File Offset: 0x00152C28
		private static void BuildXsiAttributes()
		{
			if (XmlSchemaValidator.xsiTypeSO == null)
			{
				XmlSchemaAttribute xmlSchemaAttribute = new XmlSchemaAttribute();
				xmlSchemaAttribute.Name = "type";
				xmlSchemaAttribute.SetQualifiedName(new XmlQualifiedName("type", "http://www.w3.org/2001/XMLSchema-instance"));
				xmlSchemaAttribute.SetAttributeType(XmlSchemaType.GetBuiltInSimpleType(XmlTypeCode.QName));
				Interlocked.CompareExchange<XmlSchemaAttribute>(ref XmlSchemaValidator.xsiTypeSO, xmlSchemaAttribute, null);
			}
			if (XmlSchemaValidator.xsiNilSO == null)
			{
				XmlSchemaAttribute xmlSchemaAttribute2 = new XmlSchemaAttribute();
				xmlSchemaAttribute2.Name = "nil";
				xmlSchemaAttribute2.SetQualifiedName(new XmlQualifiedName("nil", "http://www.w3.org/2001/XMLSchema-instance"));
				xmlSchemaAttribute2.SetAttributeType(XmlSchemaType.GetBuiltInSimpleType(XmlTypeCode.Boolean));
				Interlocked.CompareExchange<XmlSchemaAttribute>(ref XmlSchemaValidator.xsiNilSO, xmlSchemaAttribute2, null);
			}
			if (XmlSchemaValidator.xsiSLSO == null)
			{
				XmlSchemaSimpleType builtInSimpleType = XmlSchemaType.GetBuiltInSimpleType(XmlTypeCode.String);
				XmlSchemaAttribute xmlSchemaAttribute3 = new XmlSchemaAttribute();
				xmlSchemaAttribute3.Name = "schemaLocation";
				xmlSchemaAttribute3.SetQualifiedName(new XmlQualifiedName("schemaLocation", "http://www.w3.org/2001/XMLSchema-instance"));
				xmlSchemaAttribute3.SetAttributeType(builtInSimpleType);
				Interlocked.CompareExchange<XmlSchemaAttribute>(ref XmlSchemaValidator.xsiSLSO, xmlSchemaAttribute3, null);
			}
			if (XmlSchemaValidator.xsiNoNsSLSO == null)
			{
				XmlSchemaSimpleType builtInSimpleType2 = XmlSchemaType.GetBuiltInSimpleType(XmlTypeCode.String);
				XmlSchemaAttribute xmlSchemaAttribute4 = new XmlSchemaAttribute();
				xmlSchemaAttribute4.Name = "noNamespaceSchemaLocation";
				xmlSchemaAttribute4.SetQualifiedName(new XmlQualifiedName("noNamespaceSchemaLocation", "http://www.w3.org/2001/XMLSchema-instance"));
				xmlSchemaAttribute4.SetAttributeType(builtInSimpleType2);
				Interlocked.CompareExchange<XmlSchemaAttribute>(ref XmlSchemaValidator.xsiNoNsSLSO, xmlSchemaAttribute4, null);
			}
		}

		// Token: 0x06003CE4 RID: 15588 RVA: 0x00154B5C File Offset: 0x00152D5C
		internal static void ElementValidationError(XmlQualifiedName name, ValidationState context, ValidationEventHandler eventHandler, object sender, string sourceUri, int lineNo, int linePos, XmlSchemaSet schemaSet)
		{
			if (context.ElementDecl != null)
			{
				ContentValidator contentValidator = context.ElementDecl.ContentValidator;
				XmlSchemaContentType contentType = contentValidator.ContentType;
				if (contentType == XmlSchemaContentType.ElementOnly || (contentType == XmlSchemaContentType.Mixed && contentValidator != ContentValidator.Mixed && contentValidator != ContentValidator.Any))
				{
					bool flag = schemaSet != null;
					ArrayList arrayList;
					if (flag)
					{
						arrayList = contentValidator.ExpectedParticles(context, false, schemaSet);
					}
					else
					{
						arrayList = contentValidator.ExpectedElements(context, false);
					}
					if (arrayList == null || arrayList.Count == 0)
					{
						if (context.TooComplex)
						{
							XmlSchemaValidator.SendValidationEvent(eventHandler, sender, new XmlSchemaValidationException("The element {0} has invalid child element {1} - {2}", new string[]
							{
								XmlSchemaValidator.BuildElementName(context.LocalName, context.Namespace),
								XmlSchemaValidator.BuildElementName(name),
								Res.GetString("Content model validation resulted in a large number of states, possibly due to large occurrence ranges. Therefore, content model may not be validated accurately.")
							}, sourceUri, lineNo, linePos), XmlSeverityType.Error);
							return;
						}
						XmlSchemaValidator.SendValidationEvent(eventHandler, sender, new XmlSchemaValidationException("The element {0} has invalid child element {1}.", new string[]
						{
							XmlSchemaValidator.BuildElementName(context.LocalName, context.Namespace),
							XmlSchemaValidator.BuildElementName(name)
						}, sourceUri, lineNo, linePos), XmlSeverityType.Error);
						return;
					}
					else
					{
						if (context.TooComplex)
						{
							XmlSchemaValidator.SendValidationEvent(eventHandler, sender, new XmlSchemaValidationException("The element {0} has invalid child element {1}. List of possible elements expected: {2}. {3}", new string[]
							{
								XmlSchemaValidator.BuildElementName(context.LocalName, context.Namespace),
								XmlSchemaValidator.BuildElementName(name),
								XmlSchemaValidator.PrintExpectedElements(arrayList, flag),
								Res.GetString("Content model validation resulted in a large number of states, possibly due to large occurrence ranges. Therefore, content model may not be validated accurately.")
							}, sourceUri, lineNo, linePos), XmlSeverityType.Error);
							return;
						}
						XmlSchemaValidator.SendValidationEvent(eventHandler, sender, new XmlSchemaValidationException("The element {0} has invalid child element {1}. List of possible elements expected: {2}.", new string[]
						{
							XmlSchemaValidator.BuildElementName(context.LocalName, context.Namespace),
							XmlSchemaValidator.BuildElementName(name),
							XmlSchemaValidator.PrintExpectedElements(arrayList, flag)
						}, sourceUri, lineNo, linePos), XmlSeverityType.Error);
						return;
					}
				}
				else
				{
					if (contentType == XmlSchemaContentType.Empty)
					{
						XmlSchemaValidator.SendValidationEvent(eventHandler, sender, new XmlSchemaValidationException("The element '{0}' cannot contain child element '{1}' because the parent element's content model is empty.", new string[]
						{
							XmlSchemaValidator.QNameString(context.LocalName, context.Namespace),
							name.ToString()
						}, sourceUri, lineNo, linePos), XmlSeverityType.Error);
						return;
					}
					if (!contentValidator.IsOpen)
					{
						XmlSchemaValidator.SendValidationEvent(eventHandler, sender, new XmlSchemaValidationException("The element '{0}' cannot contain child element '{1}' because the parent element's content model is text only.", new string[]
						{
							XmlSchemaValidator.QNameString(context.LocalName, context.Namespace),
							name.ToString()
						}, sourceUri, lineNo, linePos), XmlSeverityType.Error);
					}
				}
			}
		}

		// Token: 0x06003CE5 RID: 15589 RVA: 0x00154D88 File Offset: 0x00152F88
		internal static void CompleteValidationError(ValidationState context, ValidationEventHandler eventHandler, object sender, string sourceUri, int lineNo, int linePos, XmlSchemaSet schemaSet)
		{
			ArrayList arrayList = null;
			bool flag = schemaSet != null;
			if (context.ElementDecl != null)
			{
				if (flag)
				{
					arrayList = context.ElementDecl.ContentValidator.ExpectedParticles(context, true, schemaSet);
				}
				else
				{
					arrayList = context.ElementDecl.ContentValidator.ExpectedElements(context, true);
				}
			}
			if (arrayList == null || arrayList.Count == 0)
			{
				if (context.TooComplex)
				{
					XmlSchemaValidator.SendValidationEvent(eventHandler, sender, new XmlSchemaValidationException("The element {0} has incomplete content - {2}", new string[]
					{
						XmlSchemaValidator.BuildElementName(context.LocalName, context.Namespace),
						Res.GetString("Content model validation resulted in a large number of states, possibly due to large occurrence ranges. Therefore, content model may not be validated accurately.")
					}, sourceUri, lineNo, linePos), XmlSeverityType.Error);
				}
				XmlSchemaValidator.SendValidationEvent(eventHandler, sender, new XmlSchemaValidationException("The element {0} has incomplete content.", XmlSchemaValidator.BuildElementName(context.LocalName, context.Namespace), sourceUri, lineNo, linePos), XmlSeverityType.Error);
				return;
			}
			if (context.TooComplex)
			{
				XmlSchemaValidator.SendValidationEvent(eventHandler, sender, new XmlSchemaValidationException("The element {0} has incomplete content. List of possible elements expected: {1}. {2}", new string[]
				{
					XmlSchemaValidator.BuildElementName(context.LocalName, context.Namespace),
					XmlSchemaValidator.PrintExpectedElements(arrayList, flag),
					Res.GetString("Content model validation resulted in a large number of states, possibly due to large occurrence ranges. Therefore, content model may not be validated accurately.")
				}, sourceUri, lineNo, linePos), XmlSeverityType.Error);
				return;
			}
			XmlSchemaValidator.SendValidationEvent(eventHandler, sender, new XmlSchemaValidationException("The element {0} has incomplete content. List of possible elements expected: {1}.", new string[]
			{
				XmlSchemaValidator.BuildElementName(context.LocalName, context.Namespace),
				XmlSchemaValidator.PrintExpectedElements(arrayList, flag)
			}, sourceUri, lineNo, linePos), XmlSeverityType.Error);
		}

		// Token: 0x06003CE6 RID: 15590 RVA: 0x00154ED8 File Offset: 0x001530D8
		internal static string PrintExpectedElements(ArrayList expected, bool getParticles)
		{
			if (getParticles)
			{
				string name = "{0}as well as";
				object[] args = new string[]
				{
					" "
				};
				string @string = Res.GetString(name, args);
				XmlSchemaParticle xmlSchemaParticle = null;
				ArrayList arrayList = new ArrayList();
				StringBuilder stringBuilder = new StringBuilder();
				if (expected.Count == 1)
				{
					xmlSchemaParticle = (expected[0] as XmlSchemaParticle);
				}
				else
				{
					for (int i = 1; i < expected.Count; i++)
					{
						XmlSchemaParticle xmlSchemaParticle2 = expected[i - 1] as XmlSchemaParticle;
						xmlSchemaParticle = (expected[i] as XmlSchemaParticle);
						XmlQualifiedName qualifiedName = xmlSchemaParticle2.GetQualifiedName();
						if (qualifiedName.Namespace != xmlSchemaParticle.GetQualifiedName().Namespace)
						{
							arrayList.Add(qualifiedName);
							XmlSchemaValidator.PrintNamesWithNS(arrayList, stringBuilder);
							arrayList.Clear();
							stringBuilder.Append(@string);
						}
						else
						{
							arrayList.Add(qualifiedName);
						}
					}
				}
				arrayList.Add(xmlSchemaParticle.GetQualifiedName());
				XmlSchemaValidator.PrintNamesWithNS(arrayList, stringBuilder);
				return stringBuilder.ToString();
			}
			return XmlSchemaValidator.PrintNames(expected);
		}

		// Token: 0x06003CE7 RID: 15591 RVA: 0x00154FCC File Offset: 0x001531CC
		private static string PrintNames(ArrayList expected)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("'");
			stringBuilder.Append(expected[0].ToString());
			for (int i = 1; i < expected.Count; i++)
			{
				stringBuilder.Append(" ");
				stringBuilder.Append(expected[i].ToString());
			}
			stringBuilder.Append("'");
			return stringBuilder.ToString();
		}

		// Token: 0x06003CE8 RID: 15592 RVA: 0x00155040 File Offset: 0x00153240
		private static void PrintNamesWithNS(ArrayList expected, StringBuilder builder)
		{
			XmlQualifiedName xmlQualifiedName = expected[0] as XmlQualifiedName;
			if (expected.Count == 1)
			{
				if (xmlQualifiedName.Name == "*")
				{
					XmlSchemaValidator.EnumerateAny(builder, xmlQualifiedName.Namespace);
					return;
				}
				if (xmlQualifiedName.Namespace.Length != 0)
				{
					builder.Append(Res.GetString("'{0}' in namespace '{1}'", new object[]
					{
						xmlQualifiedName.Name,
						xmlQualifiedName.Namespace
					}));
					return;
				}
				builder.Append(Res.GetString("'{0}'", new object[]
				{
					xmlQualifiedName.Name
				}));
				return;
			}
			else
			{
				bool flag = false;
				bool flag2 = true;
				StringBuilder stringBuilder = new StringBuilder();
				for (int i = 0; i < expected.Count; i++)
				{
					xmlQualifiedName = (expected[i] as XmlQualifiedName);
					if (xmlQualifiedName.Name == "*")
					{
						flag = true;
					}
					else
					{
						if (flag2)
						{
							flag2 = false;
						}
						else
						{
							stringBuilder.Append(", ");
						}
						stringBuilder.Append(xmlQualifiedName.Name);
					}
				}
				if (flag)
				{
					stringBuilder.Append(", ");
					stringBuilder.Append(Res.GetString("any element"));
					return;
				}
				if (xmlQualifiedName.Namespace.Length != 0)
				{
					builder.Append(Res.GetString("'{0}' in namespace '{1}'", new object[]
					{
						stringBuilder.ToString(),
						xmlQualifiedName.Namespace
					}));
					return;
				}
				builder.Append(Res.GetString("'{0}'", new object[]
				{
					stringBuilder.ToString()
				}));
				return;
			}
		}

		// Token: 0x06003CE9 RID: 15593 RVA: 0x001551B8 File Offset: 0x001533B8
		private static void EnumerateAny(StringBuilder builder, string namespaces)
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (namespaces == "##any" || namespaces == "##other")
			{
				stringBuilder.Append(namespaces);
			}
			else
			{
				string[] array = XmlConvert.SplitString(namespaces);
				stringBuilder.Append(array[0]);
				for (int i = 1; i < array.Length; i++)
				{
					stringBuilder.Append(", ");
					stringBuilder.Append(array[i]);
				}
			}
			builder.Append(Res.GetString("any element in namespace '{0}'", new object[]
			{
				stringBuilder.ToString()
			}));
		}

		// Token: 0x06003CEA RID: 15594 RVA: 0x00155244 File Offset: 0x00153444
		internal static string QNameString(string localName, string ns)
		{
			if (ns.Length == 0)
			{
				return localName;
			}
			return ns + ":" + localName;
		}

		// Token: 0x06003CEB RID: 15595 RVA: 0x0015525C File Offset: 0x0015345C
		internal static string BuildElementName(XmlQualifiedName qname)
		{
			return XmlSchemaValidator.BuildElementName(qname.Name, qname.Namespace);
		}

		// Token: 0x06003CEC RID: 15596 RVA: 0x0015526F File Offset: 0x0015346F
		internal static string BuildElementName(string localName, string ns)
		{
			if (ns.Length != 0)
			{
				return Res.GetString("'{0}' in namespace '{1}'", new object[]
				{
					localName,
					ns
				});
			}
			return Res.GetString("'{0}'", new object[]
			{
				localName
			});
		}

		// Token: 0x06003CED RID: 15597 RVA: 0x001552A8 File Offset: 0x001534A8
		private void ProcessEntity(string name)
		{
			if (!this.checkEntity)
			{
				return;
			}
			IDtdEntityInfo dtdEntityInfo = null;
			if (this.dtdSchemaInfo != null)
			{
				dtdEntityInfo = this.dtdSchemaInfo.LookupEntity(name);
			}
			if (dtdEntityInfo == null)
			{
				this.SendValidationEvent("Reference to an undeclared entity, '{0}'.", name);
				return;
			}
			if (dtdEntityInfo.IsUnparsedEntity)
			{
				this.SendValidationEvent("Reference to an unparsed entity, '{0}'.", name);
			}
		}

		// Token: 0x06003CEE RID: 15598 RVA: 0x001552F9 File Offset: 0x001534F9
		private void SendValidationEvent(string code)
		{
			this.SendValidationEvent(code, string.Empty);
		}

		// Token: 0x06003CEF RID: 15599 RVA: 0x00155307 File Offset: 0x00153507
		private void SendValidationEvent(string code, string[] args)
		{
			this.SendValidationEvent(new XmlSchemaValidationException(code, args, this.sourceUriString, this.positionInfo.LineNumber, this.positionInfo.LinePosition));
		}

		// Token: 0x06003CF0 RID: 15600 RVA: 0x00155332 File Offset: 0x00153532
		private void SendValidationEvent(string code, string arg)
		{
			this.SendValidationEvent(new XmlSchemaValidationException(code, arg, this.sourceUriString, this.positionInfo.LineNumber, this.positionInfo.LinePosition));
		}

		// Token: 0x06003CF1 RID: 15601 RVA: 0x0015535D File Offset: 0x0015355D
		private void SendValidationEvent(string code, string arg1, string arg2)
		{
			this.SendValidationEvent(new XmlSchemaValidationException(code, new string[]
			{
				arg1,
				arg2
			}, this.sourceUriString, this.positionInfo.LineNumber, this.positionInfo.LinePosition));
		}

		// Token: 0x06003CF2 RID: 15602 RVA: 0x00155395 File Offset: 0x00153595
		private void SendValidationEvent(string code, string[] args, Exception innerException, XmlSeverityType severity)
		{
			if (severity != XmlSeverityType.Warning || this.ReportValidationWarnings)
			{
				this.SendValidationEvent(new XmlSchemaValidationException(code, args, innerException, this.sourceUriString, this.positionInfo.LineNumber, this.positionInfo.LinePosition), severity);
			}
		}

		// Token: 0x06003CF3 RID: 15603 RVA: 0x001553D0 File Offset: 0x001535D0
		private void SendValidationEvent(string code, string[] args, Exception innerException)
		{
			this.SendValidationEvent(new XmlSchemaValidationException(code, args, innerException, this.sourceUriString, this.positionInfo.LineNumber, this.positionInfo.LinePosition), XmlSeverityType.Error);
		}

		// Token: 0x06003CF4 RID: 15604 RVA: 0x001553FD File Offset: 0x001535FD
		private void SendValidationEvent(XmlSchemaValidationException e)
		{
			this.SendValidationEvent(e, XmlSeverityType.Error);
		}

		// Token: 0x06003CF5 RID: 15605 RVA: 0x00155407 File Offset: 0x00153607
		private void SendValidationEvent(XmlSchemaException e)
		{
			this.SendValidationEvent(new XmlSchemaValidationException(e.GetRes, e.Args, e.SourceUri, e.LineNumber, e.LinePosition), XmlSeverityType.Error);
		}

		// Token: 0x06003CF6 RID: 15606 RVA: 0x00155433 File Offset: 0x00153633
		private void SendValidationEvent(string code, string msg, XmlSeverityType severity)
		{
			if (severity != XmlSeverityType.Warning || this.ReportValidationWarnings)
			{
				this.SendValidationEvent(new XmlSchemaValidationException(code, msg, this.sourceUriString, this.positionInfo.LineNumber, this.positionInfo.LinePosition), severity);
			}
		}

		// Token: 0x06003CF7 RID: 15607 RVA: 0x0015546C File Offset: 0x0015366C
		private void SendValidationEvent(XmlSchemaValidationException e, XmlSeverityType severity)
		{
			bool flag = false;
			if (severity == XmlSeverityType.Error)
			{
				flag = true;
				this.context.Validity = XmlSchemaValidity.Invalid;
			}
			if (!flag)
			{
				if (this.ReportValidationWarnings && this.eventHandler != null)
				{
					this.eventHandler(this.validationEventSender, new ValidationEventArgs(e, severity));
				}
				return;
			}
			if (this.eventHandler != null)
			{
				this.eventHandler(this.validationEventSender, new ValidationEventArgs(e, severity));
				return;
			}
			throw e;
		}

		// Token: 0x06003CF8 RID: 15608 RVA: 0x001554DA File Offset: 0x001536DA
		internal static void SendValidationEvent(ValidationEventHandler eventHandler, object sender, XmlSchemaValidationException e, XmlSeverityType severity)
		{
			if (eventHandler != null)
			{
				eventHandler(sender, new ValidationEventArgs(e, severity));
				return;
			}
			if (severity == XmlSeverityType.Error)
			{
				throw e;
			}
		}

		// Token: 0x06003CF9 RID: 15609 RVA: 0x001554F4 File Offset: 0x001536F4
		// Note: this type is marked as 'beforefieldinit'.
		static XmlSchemaValidator()
		{
		}

		// Token: 0x04002BFA RID: 11258
		private XmlSchemaSet schemaSet;

		// Token: 0x04002BFB RID: 11259
		private XmlSchemaValidationFlags validationFlags;

		// Token: 0x04002BFC RID: 11260
		private int startIDConstraint = -1;

		// Token: 0x04002BFD RID: 11261
		private const int STACK_INCREMENT = 10;

		// Token: 0x04002BFE RID: 11262
		private bool isRoot;

		// Token: 0x04002BFF RID: 11263
		private bool rootHasSchema;

		// Token: 0x04002C00 RID: 11264
		private bool attrValid;

		// Token: 0x04002C01 RID: 11265
		private bool checkEntity;

		// Token: 0x04002C02 RID: 11266
		private SchemaInfo compiledSchemaInfo;

		// Token: 0x04002C03 RID: 11267
		private IDtdInfo dtdSchemaInfo;

		// Token: 0x04002C04 RID: 11268
		private Hashtable validatedNamespaces;

		// Token: 0x04002C05 RID: 11269
		private HWStack validationStack;

		// Token: 0x04002C06 RID: 11270
		private ValidationState context;

		// Token: 0x04002C07 RID: 11271
		private ValidatorState currentState;

		// Token: 0x04002C08 RID: 11272
		private Hashtable attPresence;

		// Token: 0x04002C09 RID: 11273
		private SchemaAttDef wildID;

		// Token: 0x04002C0A RID: 11274
		private Hashtable IDs;

		// Token: 0x04002C0B RID: 11275
		private IdRefNode idRefListHead;

		// Token: 0x04002C0C RID: 11276
		private XmlQualifiedName contextQName;

		// Token: 0x04002C0D RID: 11277
		private string NsXs;

		// Token: 0x04002C0E RID: 11278
		private string NsXsi;

		// Token: 0x04002C0F RID: 11279
		private string NsXmlNs;

		// Token: 0x04002C10 RID: 11280
		private string NsXml;

		// Token: 0x04002C11 RID: 11281
		private XmlSchemaObject partialValidationType;

		// Token: 0x04002C12 RID: 11282
		private StringBuilder textValue;

		// Token: 0x04002C13 RID: 11283
		private ValidationEventHandler eventHandler;

		// Token: 0x04002C14 RID: 11284
		private object validationEventSender;

		// Token: 0x04002C15 RID: 11285
		private XmlNameTable nameTable;

		// Token: 0x04002C16 RID: 11286
		private IXmlLineInfo positionInfo;

		// Token: 0x04002C17 RID: 11287
		private IXmlLineInfo dummyPositionInfo;

		// Token: 0x04002C18 RID: 11288
		private XmlResolver xmlResolver;

		// Token: 0x04002C19 RID: 11289
		private Uri sourceUri;

		// Token: 0x04002C1A RID: 11290
		private string sourceUriString;

		// Token: 0x04002C1B RID: 11291
		private IXmlNamespaceResolver nsResolver;

		// Token: 0x04002C1C RID: 11292
		private XmlSchemaContentProcessing processContents = XmlSchemaContentProcessing.Strict;

		// Token: 0x04002C1D RID: 11293
		private static XmlSchemaAttribute xsiTypeSO;

		// Token: 0x04002C1E RID: 11294
		private static XmlSchemaAttribute xsiNilSO;

		// Token: 0x04002C1F RID: 11295
		private static XmlSchemaAttribute xsiSLSO;

		// Token: 0x04002C20 RID: 11296
		private static XmlSchemaAttribute xsiNoNsSLSO;

		// Token: 0x04002C21 RID: 11297
		private string xsiTypeString;

		// Token: 0x04002C22 RID: 11298
		private string xsiNilString;

		// Token: 0x04002C23 RID: 11299
		private string xsiSchemaLocationString;

		// Token: 0x04002C24 RID: 11300
		private string xsiNoNamespaceSchemaLocationString;

		// Token: 0x04002C25 RID: 11301
		private static readonly XmlSchemaDatatype dtQName = XmlSchemaDatatype.FromXmlTokenizedTypeXsd(XmlTokenizedType.QName);

		// Token: 0x04002C26 RID: 11302
		private static readonly XmlSchemaDatatype dtCDATA = XmlSchemaDatatype.FromXmlTokenizedType(XmlTokenizedType.CDATA);

		// Token: 0x04002C27 RID: 11303
		private static readonly XmlSchemaDatatype dtStringArray = XmlSchemaValidator.dtCDATA.DeriveByList(null);

		// Token: 0x04002C28 RID: 11304
		private const string Quote = "'";

		// Token: 0x04002C29 RID: 11305
		private static XmlSchemaParticle[] EmptyParticleArray = new XmlSchemaParticle[0];

		// Token: 0x04002C2A RID: 11306
		private static XmlSchemaAttribute[] EmptyAttributeArray = new XmlSchemaAttribute[0];

		// Token: 0x04002C2B RID: 11307
		private XmlCharType xmlCharType = XmlCharType.Instance;

		// Token: 0x04002C2C RID: 11308
		internal static bool[,] ValidStates = new bool[,]
		{
			{
				true,
				true,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false
			},
			{
				false,
				true,
				true,
				true,
				true,
				false,
				false,
				false,
				false,
				false,
				false,
				true
			},
			{
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				true
			},
			{
				false,
				false,
				false,
				true,
				true,
				false,
				false,
				false,
				false,
				false,
				false,
				true
			},
			{
				false,
				false,
				false,
				true,
				false,
				true,
				true,
				false,
				false,
				true,
				true,
				false
			},
			{
				false,
				false,
				false,
				false,
				false,
				true,
				true,
				false,
				false,
				true,
				true,
				false
			},
			{
				false,
				false,
				false,
				false,
				true,
				false,
				false,
				true,
				true,
				true,
				true,
				false
			},
			{
				false,
				false,
				false,
				false,
				true,
				false,
				false,
				true,
				true,
				true,
				true,
				false
			},
			{
				false,
				false,
				false,
				false,
				true,
				false,
				false,
				true,
				true,
				true,
				true,
				false
			},
			{
				false,
				false,
				false,
				true,
				true,
				false,
				false,
				true,
				true,
				true,
				true,
				true
			},
			{
				false,
				false,
				false,
				true,
				true,
				false,
				false,
				true,
				true,
				true,
				true,
				true
			},
			{
				false,
				true,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false
			}
		};

		// Token: 0x04002C2D RID: 11309
		private static string[] MethodNames = new string[]
		{
			"None",
			"Initialize",
			"top-level ValidateAttribute",
			"top-level ValidateText or ValidateWhitespace",
			"ValidateElement",
			"ValidateAttribute",
			"ValidateEndOfAttributes",
			"ValidateText",
			"ValidateWhitespace",
			"ValidateEndElement",
			"SkipToEndElement",
			"EndValidation"
		};
	}
}

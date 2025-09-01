using System;
using System.Collections;
using System.Text;

namespace System.Xml.Schema
{
	// Token: 0x020004E8 RID: 1256
	internal class BaseValidator
	{
		// Token: 0x0600338F RID: 13199 RVA: 0x00125D00 File Offset: 0x00123F00
		public BaseValidator(BaseValidator other)
		{
			this.reader = other.reader;
			this.schemaCollection = other.schemaCollection;
			this.eventHandling = other.eventHandling;
			this.nameTable = other.nameTable;
			this.schemaNames = other.schemaNames;
			this.positionInfo = other.positionInfo;
			this.xmlResolver = other.xmlResolver;
			this.baseUri = other.baseUri;
			this.elementName = other.elementName;
		}

		// Token: 0x06003390 RID: 13200 RVA: 0x00125D7F File Offset: 0x00123F7F
		public BaseValidator(XmlValidatingReaderImpl reader, XmlSchemaCollection schemaCollection, IValidationEventHandling eventHandling)
		{
			this.reader = reader;
			this.schemaCollection = schemaCollection;
			this.eventHandling = eventHandling;
			this.nameTable = reader.NameTable;
			this.positionInfo = PositionInfo.GetPositionInfo(reader);
			this.elementName = new XmlQualifiedName();
		}

		// Token: 0x17000915 RID: 2325
		// (get) Token: 0x06003391 RID: 13201 RVA: 0x00125DBF File Offset: 0x00123FBF
		public XmlValidatingReaderImpl Reader
		{
			get
			{
				return this.reader;
			}
		}

		// Token: 0x17000916 RID: 2326
		// (get) Token: 0x06003392 RID: 13202 RVA: 0x00125DC7 File Offset: 0x00123FC7
		public XmlSchemaCollection SchemaCollection
		{
			get
			{
				return this.schemaCollection;
			}
		}

		// Token: 0x17000917 RID: 2327
		// (get) Token: 0x06003393 RID: 13203 RVA: 0x00125DCF File Offset: 0x00123FCF
		public XmlNameTable NameTable
		{
			get
			{
				return this.nameTable;
			}
		}

		// Token: 0x17000918 RID: 2328
		// (get) Token: 0x06003394 RID: 13204 RVA: 0x00125DD8 File Offset: 0x00123FD8
		public SchemaNames SchemaNames
		{
			get
			{
				if (this.schemaNames != null)
				{
					return this.schemaNames;
				}
				if (this.schemaCollection != null)
				{
					this.schemaNames = this.schemaCollection.GetSchemaNames(this.nameTable);
				}
				else
				{
					this.schemaNames = new SchemaNames(this.nameTable);
				}
				return this.schemaNames;
			}
		}

		// Token: 0x17000919 RID: 2329
		// (get) Token: 0x06003395 RID: 13205 RVA: 0x00125E2C File Offset: 0x0012402C
		public PositionInfo PositionInfo
		{
			get
			{
				return this.positionInfo;
			}
		}

		// Token: 0x1700091A RID: 2330
		// (get) Token: 0x06003396 RID: 13206 RVA: 0x00125E34 File Offset: 0x00124034
		// (set) Token: 0x06003397 RID: 13207 RVA: 0x00125E3C File Offset: 0x0012403C
		public XmlResolver XmlResolver
		{
			get
			{
				return this.xmlResolver;
			}
			set
			{
				this.xmlResolver = value;
			}
		}

		// Token: 0x1700091B RID: 2331
		// (get) Token: 0x06003398 RID: 13208 RVA: 0x00125E45 File Offset: 0x00124045
		// (set) Token: 0x06003399 RID: 13209 RVA: 0x00125E4D File Offset: 0x0012404D
		public Uri BaseUri
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

		// Token: 0x1700091C RID: 2332
		// (get) Token: 0x0600339A RID: 13210 RVA: 0x00125E56 File Offset: 0x00124056
		public ValidationEventHandler EventHandler
		{
			get
			{
				return (ValidationEventHandler)this.eventHandling.EventHandler;
			}
		}

		// Token: 0x1700091D RID: 2333
		// (get) Token: 0x0600339B RID: 13211 RVA: 0x00125E68 File Offset: 0x00124068
		// (set) Token: 0x0600339C RID: 13212 RVA: 0x00125E70 File Offset: 0x00124070
		public SchemaInfo SchemaInfo
		{
			get
			{
				return this.schemaInfo;
			}
			set
			{
				this.schemaInfo = value;
			}
		}

		// Token: 0x1700091E RID: 2334
		// (get) Token: 0x0600339D RID: 13213 RVA: 0x00125E68 File Offset: 0x00124068
		// (set) Token: 0x0600339E RID: 13214 RVA: 0x00125E7C File Offset: 0x0012407C
		public IDtdInfo DtdInfo
		{
			get
			{
				return this.schemaInfo;
			}
			set
			{
				SchemaInfo schemaInfo = value as SchemaInfo;
				if (schemaInfo == null)
				{
					throw new XmlException("An internal error has occurred.", string.Empty);
				}
				this.schemaInfo = schemaInfo;
			}
		}

		// Token: 0x1700091F RID: 2335
		// (get) Token: 0x0600339F RID: 13215 RVA: 0x0000D1C5 File Offset: 0x0000B3C5
		public virtual bool PreserveWhitespace
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060033A0 RID: 13216 RVA: 0x0000B528 File Offset: 0x00009728
		public virtual void Validate()
		{
		}

		// Token: 0x060033A1 RID: 13217 RVA: 0x0000B528 File Offset: 0x00009728
		public virtual void CompleteValidation()
		{
		}

		// Token: 0x060033A2 RID: 13218 RVA: 0x0001DA42 File Offset: 0x0001BC42
		public virtual object FindId(string name)
		{
			return null;
		}

		// Token: 0x060033A3 RID: 13219 RVA: 0x00125EAC File Offset: 0x001240AC
		public void ValidateText()
		{
			if (this.context.NeedValidateChildren)
			{
				if (this.context.IsNill)
				{
					this.SendValidationEvent("Element '{0}' must have no character or element children.", XmlSchemaValidator.QNameString(this.context.LocalName, this.context.Namespace));
					return;
				}
				ContentValidator contentValidator = this.context.ElementDecl.ContentValidator;
				XmlSchemaContentType contentType = contentValidator.ContentType;
				if (contentType == XmlSchemaContentType.ElementOnly)
				{
					ArrayList arrayList = contentValidator.ExpectedElements(this.context, false);
					if (arrayList == null)
					{
						this.SendValidationEvent("The element {0} cannot contain text.", XmlSchemaValidator.BuildElementName(this.context.LocalName, this.context.Namespace));
					}
					else
					{
						this.SendValidationEvent("The element {0} cannot contain text. List of possible elements expected: {1}.", new string[]
						{
							XmlSchemaValidator.BuildElementName(this.context.LocalName, this.context.Namespace),
							XmlSchemaValidator.PrintExpectedElements(arrayList, false)
						});
					}
				}
				else if (contentType == XmlSchemaContentType.Empty)
				{
					this.SendValidationEvent("The element cannot contain text. Content model is empty.", string.Empty);
				}
				if (this.checkDatatype)
				{
					this.SaveTextValue(this.reader.Value);
				}
			}
		}

		// Token: 0x060033A4 RID: 13220 RVA: 0x00125FBC File Offset: 0x001241BC
		public void ValidateWhitespace()
		{
			if (this.context.NeedValidateChildren)
			{
				int contentType = (int)this.context.ElementDecl.ContentValidator.ContentType;
				if (this.context.IsNill)
				{
					this.SendValidationEvent("Element '{0}' must have no character or element children.", XmlSchemaValidator.QNameString(this.context.LocalName, this.context.Namespace));
				}
				if (contentType == 1)
				{
					this.SendValidationEvent("The element cannot contain white space. Content model is empty.", string.Empty);
				}
				if (this.checkDatatype)
				{
					this.SaveTextValue(this.reader.Value);
				}
			}
		}

		// Token: 0x060033A5 RID: 13221 RVA: 0x0012604C File Offset: 0x0012424C
		private void SaveTextValue(string value)
		{
			if (this.textString.Length == 0)
			{
				this.textString = value;
				return;
			}
			if (!this.hasSibling)
			{
				this.textValue.Append(this.textString);
				this.hasSibling = true;
			}
			this.textValue.Append(value);
		}

		// Token: 0x060033A6 RID: 13222 RVA: 0x0012609C File Offset: 0x0012429C
		protected void SendValidationEvent(string code)
		{
			this.SendValidationEvent(code, string.Empty);
		}

		// Token: 0x060033A7 RID: 13223 RVA: 0x001260AA File Offset: 0x001242AA
		protected void SendValidationEvent(string code, string[] args)
		{
			this.SendValidationEvent(new XmlSchemaException(code, args, this.reader.BaseURI, this.positionInfo.LineNumber, this.positionInfo.LinePosition));
		}

		// Token: 0x060033A8 RID: 13224 RVA: 0x001260DA File Offset: 0x001242DA
		protected void SendValidationEvent(string code, string arg)
		{
			this.SendValidationEvent(new XmlSchemaException(code, arg, this.reader.BaseURI, this.positionInfo.LineNumber, this.positionInfo.LinePosition));
		}

		// Token: 0x060033A9 RID: 13225 RVA: 0x0012610A File Offset: 0x0012430A
		protected void SendValidationEvent(string code, string arg1, string arg2)
		{
			this.SendValidationEvent(new XmlSchemaException(code, new string[]
			{
				arg1,
				arg2
			}, this.reader.BaseURI, this.positionInfo.LineNumber, this.positionInfo.LinePosition));
		}

		// Token: 0x060033AA RID: 13226 RVA: 0x00126147 File Offset: 0x00124347
		protected void SendValidationEvent(XmlSchemaException e)
		{
			this.SendValidationEvent(e, XmlSeverityType.Error);
		}

		// Token: 0x060033AB RID: 13227 RVA: 0x00126151 File Offset: 0x00124351
		protected void SendValidationEvent(string code, string msg, XmlSeverityType severity)
		{
			this.SendValidationEvent(new XmlSchemaException(code, msg, this.reader.BaseURI, this.positionInfo.LineNumber, this.positionInfo.LinePosition), severity);
		}

		// Token: 0x060033AC RID: 13228 RVA: 0x00126182 File Offset: 0x00124382
		protected void SendValidationEvent(string code, string[] args, XmlSeverityType severity)
		{
			this.SendValidationEvent(new XmlSchemaException(code, args, this.reader.BaseURI, this.positionInfo.LineNumber, this.positionInfo.LinePosition), severity);
		}

		// Token: 0x060033AD RID: 13229 RVA: 0x001261B3 File Offset: 0x001243B3
		protected void SendValidationEvent(XmlSchemaException e, XmlSeverityType severity)
		{
			if (this.eventHandling != null)
			{
				this.eventHandling.SendEvent(e, severity);
				return;
			}
			if (severity == XmlSeverityType.Error)
			{
				throw e;
			}
		}

		// Token: 0x060033AE RID: 13230 RVA: 0x001261D0 File Offset: 0x001243D0
		protected static void ProcessEntity(SchemaInfo sinfo, string name, object sender, ValidationEventHandler eventhandler, string baseUri, int lineNumber, int linePosition)
		{
			XmlSchemaException ex = null;
			SchemaEntity schemaEntity;
			if (!sinfo.GeneralEntities.TryGetValue(new XmlQualifiedName(name), out schemaEntity))
			{
				ex = new XmlSchemaException("Reference to an undeclared entity, '{0}'.", name, baseUri, lineNumber, linePosition);
			}
			else if (schemaEntity.NData.IsEmpty)
			{
				ex = new XmlSchemaException("Reference to an unparsed entity, '{0}'.", name, baseUri, lineNumber, linePosition);
			}
			if (ex == null)
			{
				return;
			}
			if (eventhandler != null)
			{
				eventhandler(sender, new ValidationEventArgs(ex));
				return;
			}
			throw ex;
		}

		// Token: 0x060033AF RID: 13231 RVA: 0x00126240 File Offset: 0x00124440
		protected static void ProcessEntity(SchemaInfo sinfo, string name, IValidationEventHandling eventHandling, string baseUriStr, int lineNumber, int linePosition)
		{
			string text = null;
			SchemaEntity schemaEntity;
			if (!sinfo.GeneralEntities.TryGetValue(new XmlQualifiedName(name), out schemaEntity))
			{
				text = "Reference to an undeclared entity, '{0}'.";
			}
			else if (schemaEntity.NData.IsEmpty)
			{
				text = "Reference to an unparsed entity, '{0}'.";
			}
			if (text == null)
			{
				return;
			}
			XmlSchemaException ex = new XmlSchemaException(text, name, baseUriStr, lineNumber, linePosition);
			if (eventHandling != null)
			{
				eventHandling.SendEvent(ex, XmlSeverityType.Error);
				return;
			}
			throw ex;
		}

		// Token: 0x060033B0 RID: 13232 RVA: 0x001262A0 File Offset: 0x001244A0
		public static BaseValidator CreateInstance(ValidationType valType, XmlValidatingReaderImpl reader, XmlSchemaCollection schemaCollection, IValidationEventHandling eventHandling, bool processIdentityConstraints)
		{
			switch (valType)
			{
			case ValidationType.None:
				return new BaseValidator(reader, schemaCollection, eventHandling);
			case ValidationType.Auto:
				return new AutoValidator(reader, schemaCollection, eventHandling);
			case ValidationType.DTD:
				return new DtdValidator(reader, eventHandling, processIdentityConstraints);
			case ValidationType.XDR:
				return new XdrValidator(reader, schemaCollection, eventHandling);
			case ValidationType.Schema:
				return new XsdValidator(reader, schemaCollection, eventHandling);
			default:
				return null;
			}
		}

		// Token: 0x0400268C RID: 9868
		private XmlSchemaCollection schemaCollection;

		// Token: 0x0400268D RID: 9869
		private IValidationEventHandling eventHandling;

		// Token: 0x0400268E RID: 9870
		private XmlNameTable nameTable;

		// Token: 0x0400268F RID: 9871
		private SchemaNames schemaNames;

		// Token: 0x04002690 RID: 9872
		private PositionInfo positionInfo;

		// Token: 0x04002691 RID: 9873
		private XmlResolver xmlResolver;

		// Token: 0x04002692 RID: 9874
		private Uri baseUri;

		// Token: 0x04002693 RID: 9875
		protected SchemaInfo schemaInfo;

		// Token: 0x04002694 RID: 9876
		protected XmlValidatingReaderImpl reader;

		// Token: 0x04002695 RID: 9877
		protected XmlQualifiedName elementName;

		// Token: 0x04002696 RID: 9878
		protected ValidationState context;

		// Token: 0x04002697 RID: 9879
		protected StringBuilder textValue;

		// Token: 0x04002698 RID: 9880
		protected string textString;

		// Token: 0x04002699 RID: 9881
		protected bool hasSibling;

		// Token: 0x0400269A RID: 9882
		protected bool checkDatatype;
	}
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace System.Xml
{
	// Token: 0x0200018C RID: 396
	internal class XsdValidatingReader : XmlReader, IXmlSchemaInfo, IXmlLineInfo, IXmlNamespaceResolver
	{
		// Token: 0x06000E42 RID: 3650 RVA: 0x0005C8E8 File Offset: 0x0005AAE8
		internal XsdValidatingReader(XmlReader reader, XmlResolver xmlResolver, XmlReaderSettings readerSettings, XmlSchemaObject partialValidationType)
		{
			this.coreReader = reader;
			this.coreReaderNSResolver = (reader as IXmlNamespaceResolver);
			this.lineInfo = (reader as IXmlLineInfo);
			this.coreReaderNameTable = this.coreReader.NameTable;
			if (this.coreReaderNSResolver == null)
			{
				this.nsManager = new XmlNamespaceManager(this.coreReaderNameTable);
				this.manageNamespaces = true;
			}
			this.thisNSResolver = this;
			this.xmlResolver = xmlResolver;
			this.processInlineSchema = ((readerSettings.ValidationFlags & XmlSchemaValidationFlags.ProcessInlineSchema) > XmlSchemaValidationFlags.None);
			this.Init();
			this.SetupValidator(readerSettings, reader, partialValidationType);
			this.validationEvent = readerSettings.GetEventHandler();
		}

		// Token: 0x06000E43 RID: 3651 RVA: 0x0005C991 File Offset: 0x0005AB91
		internal XsdValidatingReader(XmlReader reader, XmlResolver xmlResolver, XmlReaderSettings readerSettings) : this(reader, xmlResolver, readerSettings, null)
		{
		}

		// Token: 0x06000E44 RID: 3652 RVA: 0x0005C9A0 File Offset: 0x0005ABA0
		private void Init()
		{
			this.validationState = XsdValidatingReader.ValidatingReaderState.Init;
			this.defaultAttributes = new ArrayList();
			this.currentAttrIndex = -1;
			this.attributePSVINodes = new AttributePSVIInfo[8];
			this.valueGetter = new XmlValueGetter(this.GetStringValue);
			XsdValidatingReader.TypeOfString = typeof(string);
			this.xmlSchemaInfo = new XmlSchemaInfo();
			this.NsXmlNs = this.coreReaderNameTable.Add("http://www.w3.org/2000/xmlns/");
			this.NsXs = this.coreReaderNameTable.Add("http://www.w3.org/2001/XMLSchema");
			this.NsXsi = this.coreReaderNameTable.Add("http://www.w3.org/2001/XMLSchema-instance");
			this.XsiType = this.coreReaderNameTable.Add("type");
			this.XsiNil = this.coreReaderNameTable.Add("nil");
			this.XsiSchemaLocation = this.coreReaderNameTable.Add("schemaLocation");
			this.XsiNoNamespaceSchemaLocation = this.coreReaderNameTable.Add("noNamespaceSchemaLocation");
			this.XsdSchema = this.coreReaderNameTable.Add("schema");
		}

		// Token: 0x06000E45 RID: 3653 RVA: 0x0005CAB0 File Offset: 0x0005ACB0
		private void SetupValidator(XmlReaderSettings readerSettings, XmlReader reader, XmlSchemaObject partialValidationType)
		{
			this.validator = new XmlSchemaValidator(this.coreReaderNameTable, readerSettings.Schemas, this.thisNSResolver, readerSettings.ValidationFlags);
			this.validator.XmlResolver = this.xmlResolver;
			this.validator.SourceUri = XmlConvert.ToUri(reader.BaseURI);
			this.validator.ValidationEventSender = this;
			this.validator.ValidationEventHandler += readerSettings.GetEventHandler();
			this.validator.LineInfoProvider = this.lineInfo;
			if (this.validator.ProcessSchemaHints)
			{
				this.validator.SchemaSet.ReaderSettings.DtdProcessing = readerSettings.DtdProcessing;
			}
			this.validator.SetDtdSchemaInfo(reader.DtdInfo);
			if (partialValidationType != null)
			{
				this.validator.Initialize(partialValidationType);
				return;
			}
			this.validator.Initialize();
		}

		// Token: 0x1700022C RID: 556
		// (get) Token: 0x06000E46 RID: 3654 RVA: 0x0005CB8C File Offset: 0x0005AD8C
		public override XmlReaderSettings Settings
		{
			get
			{
				XmlReaderSettings xmlReaderSettings = this.coreReader.Settings;
				if (xmlReaderSettings != null)
				{
					xmlReaderSettings = xmlReaderSettings.Clone();
				}
				if (xmlReaderSettings == null)
				{
					xmlReaderSettings = new XmlReaderSettings();
				}
				xmlReaderSettings.Schemas = this.validator.SchemaSet;
				xmlReaderSettings.ValidationType = ValidationType.Schema;
				xmlReaderSettings.ValidationFlags = this.validator.ValidationFlags;
				xmlReaderSettings.ReadOnly = true;
				return xmlReaderSettings;
			}
		}

		// Token: 0x1700022D RID: 557
		// (get) Token: 0x06000E47 RID: 3655 RVA: 0x0005CBEC File Offset: 0x0005ADEC
		public override XmlNodeType NodeType
		{
			get
			{
				if (this.validationState < XsdValidatingReader.ValidatingReaderState.None)
				{
					return this.cachedNode.NodeType;
				}
				XmlNodeType nodeType = this.coreReader.NodeType;
				if (nodeType == XmlNodeType.Whitespace && (this.validator.CurrentContentType == XmlSchemaContentType.TextOnly || this.validator.CurrentContentType == XmlSchemaContentType.Mixed))
				{
					return XmlNodeType.SignificantWhitespace;
				}
				return nodeType;
			}
		}

		// Token: 0x1700022E RID: 558
		// (get) Token: 0x06000E48 RID: 3656 RVA: 0x0005CC40 File Offset: 0x0005AE40
		public override string Name
		{
			get
			{
				if (this.validationState != XsdValidatingReader.ValidatingReaderState.OnDefaultAttribute)
				{
					return this.coreReader.Name;
				}
				string defaultAttributePrefix = this.validator.GetDefaultAttributePrefix(this.cachedNode.Namespace);
				if (defaultAttributePrefix != null && defaultAttributePrefix.Length != 0)
				{
					return string.Concat(new string[]
					{
						defaultAttributePrefix + ":" + this.cachedNode.LocalName
					});
				}
				return this.cachedNode.LocalName;
			}
		}

		// Token: 0x1700022F RID: 559
		// (get) Token: 0x06000E49 RID: 3657 RVA: 0x0005CCB4 File Offset: 0x0005AEB4
		public override string LocalName
		{
			get
			{
				if (this.validationState < XsdValidatingReader.ValidatingReaderState.None)
				{
					return this.cachedNode.LocalName;
				}
				return this.coreReader.LocalName;
			}
		}

		// Token: 0x17000230 RID: 560
		// (get) Token: 0x06000E4A RID: 3658 RVA: 0x0005CCD6 File Offset: 0x0005AED6
		public override string NamespaceURI
		{
			get
			{
				if (this.validationState < XsdValidatingReader.ValidatingReaderState.None)
				{
					return this.cachedNode.Namespace;
				}
				return this.coreReader.NamespaceURI;
			}
		}

		// Token: 0x17000231 RID: 561
		// (get) Token: 0x06000E4B RID: 3659 RVA: 0x0005CCF8 File Offset: 0x0005AEF8
		public override string Prefix
		{
			get
			{
				if (this.validationState < XsdValidatingReader.ValidatingReaderState.None)
				{
					return this.cachedNode.Prefix;
				}
				return this.coreReader.Prefix;
			}
		}

		// Token: 0x17000232 RID: 562
		// (get) Token: 0x06000E4C RID: 3660 RVA: 0x0005CD1A File Offset: 0x0005AF1A
		public override bool HasValue
		{
			get
			{
				return this.validationState < XsdValidatingReader.ValidatingReaderState.None || this.coreReader.HasValue;
			}
		}

		// Token: 0x17000233 RID: 563
		// (get) Token: 0x06000E4D RID: 3661 RVA: 0x0005CD32 File Offset: 0x0005AF32
		public override string Value
		{
			get
			{
				if (this.validationState < XsdValidatingReader.ValidatingReaderState.None)
				{
					return this.cachedNode.RawValue;
				}
				return this.coreReader.Value;
			}
		}

		// Token: 0x17000234 RID: 564
		// (get) Token: 0x06000E4E RID: 3662 RVA: 0x0005CD54 File Offset: 0x0005AF54
		public override int Depth
		{
			get
			{
				if (this.validationState < XsdValidatingReader.ValidatingReaderState.None)
				{
					return this.cachedNode.Depth;
				}
				return this.coreReader.Depth;
			}
		}

		// Token: 0x17000235 RID: 565
		// (get) Token: 0x06000E4F RID: 3663 RVA: 0x0005CD76 File Offset: 0x0005AF76
		public override string BaseURI
		{
			get
			{
				return this.coreReader.BaseURI;
			}
		}

		// Token: 0x17000236 RID: 566
		// (get) Token: 0x06000E50 RID: 3664 RVA: 0x0005CD83 File Offset: 0x0005AF83
		public override bool IsEmptyElement
		{
			get
			{
				return this.coreReader.IsEmptyElement;
			}
		}

		// Token: 0x17000237 RID: 567
		// (get) Token: 0x06000E51 RID: 3665 RVA: 0x0005CD90 File Offset: 0x0005AF90
		public override bool IsDefault
		{
			get
			{
				return this.validationState == XsdValidatingReader.ValidatingReaderState.OnDefaultAttribute || this.coreReader.IsDefault;
			}
		}

		// Token: 0x17000238 RID: 568
		// (get) Token: 0x06000E52 RID: 3666 RVA: 0x0005CDA8 File Offset: 0x0005AFA8
		public override char QuoteChar
		{
			get
			{
				return this.coreReader.QuoteChar;
			}
		}

		// Token: 0x17000239 RID: 569
		// (get) Token: 0x06000E53 RID: 3667 RVA: 0x0005CDB5 File Offset: 0x0005AFB5
		public override XmlSpace XmlSpace
		{
			get
			{
				return this.coreReader.XmlSpace;
			}
		}

		// Token: 0x1700023A RID: 570
		// (get) Token: 0x06000E54 RID: 3668 RVA: 0x0005CDC2 File Offset: 0x0005AFC2
		public override string XmlLang
		{
			get
			{
				return this.coreReader.XmlLang;
			}
		}

		// Token: 0x1700023B RID: 571
		// (get) Token: 0x06000E55 RID: 3669 RVA: 0x00002068 File Offset: 0x00000268
		public override IXmlSchemaInfo SchemaInfo
		{
			get
			{
				return this;
			}
		}

		// Token: 0x1700023C RID: 572
		// (get) Token: 0x06000E56 RID: 3670 RVA: 0x0005CDD0 File Offset: 0x0005AFD0
		public override Type ValueType
		{
			get
			{
				XmlNodeType nodeType = this.NodeType;
				if (nodeType != XmlNodeType.Element)
				{
					if (nodeType != XmlNodeType.Attribute)
					{
						if (nodeType != XmlNodeType.EndElement)
						{
							goto IL_62;
						}
					}
					else
					{
						if (this.attributePSVI != null && this.AttributeSchemaInfo.ContentType == XmlSchemaContentType.TextOnly)
						{
							return this.AttributeSchemaInfo.SchemaType.Datatype.ValueType;
						}
						goto IL_62;
					}
				}
				if (this.xmlSchemaInfo.ContentType == XmlSchemaContentType.TextOnly)
				{
					return this.xmlSchemaInfo.SchemaType.Datatype.ValueType;
				}
				IL_62:
				return XsdValidatingReader.TypeOfString;
			}
		}

		// Token: 0x06000E57 RID: 3671 RVA: 0x0005CE46 File Offset: 0x0005B046
		public override object ReadContentAsObject()
		{
			if (!XmlReader.CanReadContentAs(this.NodeType))
			{
				throw base.CreateReadContentAsException("ReadContentAsObject");
			}
			return this.InternalReadContentAsObject(true);
		}

		// Token: 0x06000E58 RID: 3672 RVA: 0x0005CE68 File Offset: 0x0005B068
		public override bool ReadContentAsBoolean()
		{
			if (!XmlReader.CanReadContentAs(this.NodeType))
			{
				throw base.CreateReadContentAsException("ReadContentAsBoolean");
			}
			object value = this.InternalReadContentAsObject();
			XmlSchemaType xmlSchemaType = (this.NodeType == XmlNodeType.Attribute) ? this.AttributeXmlType : this.ElementXmlType;
			bool result;
			try
			{
				if (xmlSchemaType != null)
				{
					result = xmlSchemaType.ValueConverter.ToBoolean(value);
				}
				else
				{
					result = XmlUntypedConverter.Untyped.ToBoolean(value);
				}
			}
			catch (InvalidCastException innerException)
			{
				throw new XmlException("Content cannot be converted to the type {0}.", "Boolean", innerException, this);
			}
			catch (FormatException innerException2)
			{
				throw new XmlException("Content cannot be converted to the type {0}.", "Boolean", innerException2, this);
			}
			catch (OverflowException innerException3)
			{
				throw new XmlException("Content cannot be converted to the type {0}.", "Boolean", innerException3, this);
			}
			return result;
		}

		// Token: 0x06000E59 RID: 3673 RVA: 0x0005CF34 File Offset: 0x0005B134
		public override DateTime ReadContentAsDateTime()
		{
			if (!XmlReader.CanReadContentAs(this.NodeType))
			{
				throw base.CreateReadContentAsException("ReadContentAsDateTime");
			}
			object value = this.InternalReadContentAsObject();
			XmlSchemaType xmlSchemaType = (this.NodeType == XmlNodeType.Attribute) ? this.AttributeXmlType : this.ElementXmlType;
			DateTime result;
			try
			{
				if (xmlSchemaType != null)
				{
					result = xmlSchemaType.ValueConverter.ToDateTime(value);
				}
				else
				{
					result = XmlUntypedConverter.Untyped.ToDateTime(value);
				}
			}
			catch (InvalidCastException innerException)
			{
				throw new XmlException("Content cannot be converted to the type {0}.", "DateTime", innerException, this);
			}
			catch (FormatException innerException2)
			{
				throw new XmlException("Content cannot be converted to the type {0}.", "DateTime", innerException2, this);
			}
			catch (OverflowException innerException3)
			{
				throw new XmlException("Content cannot be converted to the type {0}.", "DateTime", innerException3, this);
			}
			return result;
		}

		// Token: 0x06000E5A RID: 3674 RVA: 0x0005D000 File Offset: 0x0005B200
		public override double ReadContentAsDouble()
		{
			if (!XmlReader.CanReadContentAs(this.NodeType))
			{
				throw base.CreateReadContentAsException("ReadContentAsDouble");
			}
			object value = this.InternalReadContentAsObject();
			XmlSchemaType xmlSchemaType = (this.NodeType == XmlNodeType.Attribute) ? this.AttributeXmlType : this.ElementXmlType;
			double result;
			try
			{
				if (xmlSchemaType != null)
				{
					result = xmlSchemaType.ValueConverter.ToDouble(value);
				}
				else
				{
					result = XmlUntypedConverter.Untyped.ToDouble(value);
				}
			}
			catch (InvalidCastException innerException)
			{
				throw new XmlException("Content cannot be converted to the type {0}.", "Double", innerException, this);
			}
			catch (FormatException innerException2)
			{
				throw new XmlException("Content cannot be converted to the type {0}.", "Double", innerException2, this);
			}
			catch (OverflowException innerException3)
			{
				throw new XmlException("Content cannot be converted to the type {0}.", "Double", innerException3, this);
			}
			return result;
		}

		// Token: 0x06000E5B RID: 3675 RVA: 0x0005D0CC File Offset: 0x0005B2CC
		public override float ReadContentAsFloat()
		{
			if (!XmlReader.CanReadContentAs(this.NodeType))
			{
				throw base.CreateReadContentAsException("ReadContentAsFloat");
			}
			object value = this.InternalReadContentAsObject();
			XmlSchemaType xmlSchemaType = (this.NodeType == XmlNodeType.Attribute) ? this.AttributeXmlType : this.ElementXmlType;
			float result;
			try
			{
				if (xmlSchemaType != null)
				{
					result = xmlSchemaType.ValueConverter.ToSingle(value);
				}
				else
				{
					result = XmlUntypedConverter.Untyped.ToSingle(value);
				}
			}
			catch (InvalidCastException innerException)
			{
				throw new XmlException("Content cannot be converted to the type {0}.", "Float", innerException, this);
			}
			catch (FormatException innerException2)
			{
				throw new XmlException("Content cannot be converted to the type {0}.", "Float", innerException2, this);
			}
			catch (OverflowException innerException3)
			{
				throw new XmlException("Content cannot be converted to the type {0}.", "Float", innerException3, this);
			}
			return result;
		}

		// Token: 0x06000E5C RID: 3676 RVA: 0x0005D198 File Offset: 0x0005B398
		public override decimal ReadContentAsDecimal()
		{
			if (!XmlReader.CanReadContentAs(this.NodeType))
			{
				throw base.CreateReadContentAsException("ReadContentAsDecimal");
			}
			object value = this.InternalReadContentAsObject();
			XmlSchemaType xmlSchemaType = (this.NodeType == XmlNodeType.Attribute) ? this.AttributeXmlType : this.ElementXmlType;
			decimal result;
			try
			{
				if (xmlSchemaType != null)
				{
					result = xmlSchemaType.ValueConverter.ToDecimal(value);
				}
				else
				{
					result = XmlUntypedConverter.Untyped.ToDecimal(value);
				}
			}
			catch (InvalidCastException innerException)
			{
				throw new XmlException("Content cannot be converted to the type {0}.", "Decimal", innerException, this);
			}
			catch (FormatException innerException2)
			{
				throw new XmlException("Content cannot be converted to the type {0}.", "Decimal", innerException2, this);
			}
			catch (OverflowException innerException3)
			{
				throw new XmlException("Content cannot be converted to the type {0}.", "Decimal", innerException3, this);
			}
			return result;
		}

		// Token: 0x06000E5D RID: 3677 RVA: 0x0005D264 File Offset: 0x0005B464
		public override int ReadContentAsInt()
		{
			if (!XmlReader.CanReadContentAs(this.NodeType))
			{
				throw base.CreateReadContentAsException("ReadContentAsInt");
			}
			object value = this.InternalReadContentAsObject();
			XmlSchemaType xmlSchemaType = (this.NodeType == XmlNodeType.Attribute) ? this.AttributeXmlType : this.ElementXmlType;
			int result;
			try
			{
				if (xmlSchemaType != null)
				{
					result = xmlSchemaType.ValueConverter.ToInt32(value);
				}
				else
				{
					result = XmlUntypedConverter.Untyped.ToInt32(value);
				}
			}
			catch (InvalidCastException innerException)
			{
				throw new XmlException("Content cannot be converted to the type {0}.", "Int", innerException, this);
			}
			catch (FormatException innerException2)
			{
				throw new XmlException("Content cannot be converted to the type {0}.", "Int", innerException2, this);
			}
			catch (OverflowException innerException3)
			{
				throw new XmlException("Content cannot be converted to the type {0}.", "Int", innerException3, this);
			}
			return result;
		}

		// Token: 0x06000E5E RID: 3678 RVA: 0x0005D330 File Offset: 0x0005B530
		public override long ReadContentAsLong()
		{
			if (!XmlReader.CanReadContentAs(this.NodeType))
			{
				throw base.CreateReadContentAsException("ReadContentAsLong");
			}
			object value = this.InternalReadContentAsObject();
			XmlSchemaType xmlSchemaType = (this.NodeType == XmlNodeType.Attribute) ? this.AttributeXmlType : this.ElementXmlType;
			long result;
			try
			{
				if (xmlSchemaType != null)
				{
					result = xmlSchemaType.ValueConverter.ToInt64(value);
				}
				else
				{
					result = XmlUntypedConverter.Untyped.ToInt64(value);
				}
			}
			catch (InvalidCastException innerException)
			{
				throw new XmlException("Content cannot be converted to the type {0}.", "Long", innerException, this);
			}
			catch (FormatException innerException2)
			{
				throw new XmlException("Content cannot be converted to the type {0}.", "Long", innerException2, this);
			}
			catch (OverflowException innerException3)
			{
				throw new XmlException("Content cannot be converted to the type {0}.", "Long", innerException3, this);
			}
			return result;
		}

		// Token: 0x06000E5F RID: 3679 RVA: 0x0005D3FC File Offset: 0x0005B5FC
		public override string ReadContentAsString()
		{
			if (!XmlReader.CanReadContentAs(this.NodeType))
			{
				throw base.CreateReadContentAsException("ReadContentAsString");
			}
			object obj = this.InternalReadContentAsObject();
			XmlSchemaType xmlSchemaType = (this.NodeType == XmlNodeType.Attribute) ? this.AttributeXmlType : this.ElementXmlType;
			string result;
			try
			{
				if (xmlSchemaType != null)
				{
					result = xmlSchemaType.ValueConverter.ToString(obj);
				}
				else
				{
					result = (obj as string);
				}
			}
			catch (InvalidCastException innerException)
			{
				throw new XmlException("Content cannot be converted to the type {0}.", "String", innerException, this);
			}
			catch (FormatException innerException2)
			{
				throw new XmlException("Content cannot be converted to the type {0}.", "String", innerException2, this);
			}
			catch (OverflowException innerException3)
			{
				throw new XmlException("Content cannot be converted to the type {0}.", "String", innerException3, this);
			}
			return result;
		}

		// Token: 0x06000E60 RID: 3680 RVA: 0x0005D4C4 File Offset: 0x0005B6C4
		public override object ReadContentAs(Type returnType, IXmlNamespaceResolver namespaceResolver)
		{
			if (!XmlReader.CanReadContentAs(this.NodeType))
			{
				throw base.CreateReadContentAsException("ReadContentAs");
			}
			string text;
			object value = this.InternalReadContentAsObject(false, out text);
			XmlSchemaType xmlSchemaType = (this.NodeType == XmlNodeType.Attribute) ? this.AttributeXmlType : this.ElementXmlType;
			object result;
			try
			{
				if (xmlSchemaType != null)
				{
					if (returnType == typeof(DateTimeOffset) && xmlSchemaType.Datatype is Datatype_dateTimeBase)
					{
						value = text;
					}
					result = xmlSchemaType.ValueConverter.ChangeType(value, returnType);
				}
				else
				{
					result = XmlUntypedConverter.Untyped.ChangeType(value, returnType, namespaceResolver);
				}
			}
			catch (FormatException innerException)
			{
				throw new XmlException("Content cannot be converted to the type {0}.", returnType.ToString(), innerException, this);
			}
			catch (InvalidCastException innerException2)
			{
				throw new XmlException("Content cannot be converted to the type {0}.", returnType.ToString(), innerException2, this);
			}
			catch (OverflowException innerException3)
			{
				throw new XmlException("Content cannot be converted to the type {0}.", returnType.ToString(), innerException3, this);
			}
			return result;
		}

		// Token: 0x06000E61 RID: 3681 RVA: 0x0005D5BC File Offset: 0x0005B7BC
		public override object ReadElementContentAsObject()
		{
			if (this.NodeType != XmlNodeType.Element)
			{
				throw base.CreateReadElementContentAsException("ReadElementContentAsObject");
			}
			XmlSchemaType xmlSchemaType;
			return this.InternalReadElementContentAsObject(out xmlSchemaType, true);
		}

		// Token: 0x06000E62 RID: 3682 RVA: 0x0005D5E8 File Offset: 0x0005B7E8
		public override bool ReadElementContentAsBoolean()
		{
			if (this.NodeType != XmlNodeType.Element)
			{
				throw base.CreateReadElementContentAsException("ReadElementContentAsBoolean");
			}
			XmlSchemaType xmlSchemaType;
			object value = this.InternalReadElementContentAsObject(out xmlSchemaType);
			bool result;
			try
			{
				if (xmlSchemaType != null)
				{
					result = xmlSchemaType.ValueConverter.ToBoolean(value);
				}
				else
				{
					result = XmlUntypedConverter.Untyped.ToBoolean(value);
				}
			}
			catch (FormatException innerException)
			{
				throw new XmlException("Content cannot be converted to the type {0}.", "Boolean", innerException, this);
			}
			catch (InvalidCastException innerException2)
			{
				throw new XmlException("Content cannot be converted to the type {0}.", "Boolean", innerException2, this);
			}
			catch (OverflowException innerException3)
			{
				throw new XmlException("Content cannot be converted to the type {0}.", "Boolean", innerException3, this);
			}
			return result;
		}

		// Token: 0x06000E63 RID: 3683 RVA: 0x0005D69C File Offset: 0x0005B89C
		public override DateTime ReadElementContentAsDateTime()
		{
			if (this.NodeType != XmlNodeType.Element)
			{
				throw base.CreateReadElementContentAsException("ReadElementContentAsDateTime");
			}
			XmlSchemaType xmlSchemaType;
			object value = this.InternalReadElementContentAsObject(out xmlSchemaType);
			DateTime result;
			try
			{
				if (xmlSchemaType != null)
				{
					result = xmlSchemaType.ValueConverter.ToDateTime(value);
				}
				else
				{
					result = XmlUntypedConverter.Untyped.ToDateTime(value);
				}
			}
			catch (FormatException innerException)
			{
				throw new XmlException("Content cannot be converted to the type {0}.", "DateTime", innerException, this);
			}
			catch (InvalidCastException innerException2)
			{
				throw new XmlException("Content cannot be converted to the type {0}.", "DateTime", innerException2, this);
			}
			catch (OverflowException innerException3)
			{
				throw new XmlException("Content cannot be converted to the type {0}.", "DateTime", innerException3, this);
			}
			return result;
		}

		// Token: 0x06000E64 RID: 3684 RVA: 0x0005D750 File Offset: 0x0005B950
		public override double ReadElementContentAsDouble()
		{
			if (this.NodeType != XmlNodeType.Element)
			{
				throw base.CreateReadElementContentAsException("ReadElementContentAsDouble");
			}
			XmlSchemaType xmlSchemaType;
			object value = this.InternalReadElementContentAsObject(out xmlSchemaType);
			double result;
			try
			{
				if (xmlSchemaType != null)
				{
					result = xmlSchemaType.ValueConverter.ToDouble(value);
				}
				else
				{
					result = XmlUntypedConverter.Untyped.ToDouble(value);
				}
			}
			catch (FormatException innerException)
			{
				throw new XmlException("Content cannot be converted to the type {0}.", "Double", innerException, this);
			}
			catch (InvalidCastException innerException2)
			{
				throw new XmlException("Content cannot be converted to the type {0}.", "Double", innerException2, this);
			}
			catch (OverflowException innerException3)
			{
				throw new XmlException("Content cannot be converted to the type {0}.", "Double", innerException3, this);
			}
			return result;
		}

		// Token: 0x06000E65 RID: 3685 RVA: 0x0005D804 File Offset: 0x0005BA04
		public override float ReadElementContentAsFloat()
		{
			if (this.NodeType != XmlNodeType.Element)
			{
				throw base.CreateReadElementContentAsException("ReadElementContentAsFloat");
			}
			XmlSchemaType xmlSchemaType;
			object value = this.InternalReadElementContentAsObject(out xmlSchemaType);
			float result;
			try
			{
				if (xmlSchemaType != null)
				{
					result = xmlSchemaType.ValueConverter.ToSingle(value);
				}
				else
				{
					result = XmlUntypedConverter.Untyped.ToSingle(value);
				}
			}
			catch (FormatException innerException)
			{
				throw new XmlException("Content cannot be converted to the type {0}.", "Float", innerException, this);
			}
			catch (InvalidCastException innerException2)
			{
				throw new XmlException("Content cannot be converted to the type {0}.", "Float", innerException2, this);
			}
			catch (OverflowException innerException3)
			{
				throw new XmlException("Content cannot be converted to the type {0}.", "Float", innerException3, this);
			}
			return result;
		}

		// Token: 0x06000E66 RID: 3686 RVA: 0x0005D8B8 File Offset: 0x0005BAB8
		public override decimal ReadElementContentAsDecimal()
		{
			if (this.NodeType != XmlNodeType.Element)
			{
				throw base.CreateReadElementContentAsException("ReadElementContentAsDecimal");
			}
			XmlSchemaType xmlSchemaType;
			object value = this.InternalReadElementContentAsObject(out xmlSchemaType);
			decimal result;
			try
			{
				if (xmlSchemaType != null)
				{
					result = xmlSchemaType.ValueConverter.ToDecimal(value);
				}
				else
				{
					result = XmlUntypedConverter.Untyped.ToDecimal(value);
				}
			}
			catch (FormatException innerException)
			{
				throw new XmlException("Content cannot be converted to the type {0}.", "Decimal", innerException, this);
			}
			catch (InvalidCastException innerException2)
			{
				throw new XmlException("Content cannot be converted to the type {0}.", "Decimal", innerException2, this);
			}
			catch (OverflowException innerException3)
			{
				throw new XmlException("Content cannot be converted to the type {0}.", "Decimal", innerException3, this);
			}
			return result;
		}

		// Token: 0x06000E67 RID: 3687 RVA: 0x0005D96C File Offset: 0x0005BB6C
		public override int ReadElementContentAsInt()
		{
			if (this.NodeType != XmlNodeType.Element)
			{
				throw base.CreateReadElementContentAsException("ReadElementContentAsInt");
			}
			XmlSchemaType xmlSchemaType;
			object value = this.InternalReadElementContentAsObject(out xmlSchemaType);
			int result;
			try
			{
				if (xmlSchemaType != null)
				{
					result = xmlSchemaType.ValueConverter.ToInt32(value);
				}
				else
				{
					result = XmlUntypedConverter.Untyped.ToInt32(value);
				}
			}
			catch (FormatException innerException)
			{
				throw new XmlException("Content cannot be converted to the type {0}.", "Int", innerException, this);
			}
			catch (InvalidCastException innerException2)
			{
				throw new XmlException("Content cannot be converted to the type {0}.", "Int", innerException2, this);
			}
			catch (OverflowException innerException3)
			{
				throw new XmlException("Content cannot be converted to the type {0}.", "Int", innerException3, this);
			}
			return result;
		}

		// Token: 0x06000E68 RID: 3688 RVA: 0x0005DA20 File Offset: 0x0005BC20
		public override long ReadElementContentAsLong()
		{
			if (this.NodeType != XmlNodeType.Element)
			{
				throw base.CreateReadElementContentAsException("ReadElementContentAsLong");
			}
			XmlSchemaType xmlSchemaType;
			object value = this.InternalReadElementContentAsObject(out xmlSchemaType);
			long result;
			try
			{
				if (xmlSchemaType != null)
				{
					result = xmlSchemaType.ValueConverter.ToInt64(value);
				}
				else
				{
					result = XmlUntypedConverter.Untyped.ToInt64(value);
				}
			}
			catch (FormatException innerException)
			{
				throw new XmlException("Content cannot be converted to the type {0}.", "Long", innerException, this);
			}
			catch (InvalidCastException innerException2)
			{
				throw new XmlException("Content cannot be converted to the type {0}.", "Long", innerException2, this);
			}
			catch (OverflowException innerException3)
			{
				throw new XmlException("Content cannot be converted to the type {0}.", "Long", innerException3, this);
			}
			return result;
		}

		// Token: 0x06000E69 RID: 3689 RVA: 0x0005DAD4 File Offset: 0x0005BCD4
		public override string ReadElementContentAsString()
		{
			if (this.NodeType != XmlNodeType.Element)
			{
				throw base.CreateReadElementContentAsException("ReadElementContentAsString");
			}
			XmlSchemaType xmlSchemaType;
			object obj = this.InternalReadElementContentAsObject(out xmlSchemaType);
			string result;
			try
			{
				if (xmlSchemaType != null)
				{
					result = xmlSchemaType.ValueConverter.ToString(obj);
				}
				else
				{
					result = (obj as string);
				}
			}
			catch (InvalidCastException innerException)
			{
				throw new XmlException("Content cannot be converted to the type {0}.", "String", innerException, this);
			}
			catch (FormatException innerException2)
			{
				throw new XmlException("Content cannot be converted to the type {0}.", "String", innerException2, this);
			}
			catch (OverflowException innerException3)
			{
				throw new XmlException("Content cannot be converted to the type {0}.", "String", innerException3, this);
			}
			return result;
		}

		// Token: 0x06000E6A RID: 3690 RVA: 0x0005DB80 File Offset: 0x0005BD80
		public override object ReadElementContentAs(Type returnType, IXmlNamespaceResolver namespaceResolver)
		{
			if (this.NodeType != XmlNodeType.Element)
			{
				throw base.CreateReadElementContentAsException("ReadElementContentAs");
			}
			XmlSchemaType xmlSchemaType;
			string text;
			object value = this.InternalReadElementContentAsObject(out xmlSchemaType, false, out text);
			object result;
			try
			{
				if (xmlSchemaType != null)
				{
					if (returnType == typeof(DateTimeOffset) && xmlSchemaType.Datatype is Datatype_dateTimeBase)
					{
						value = text;
					}
					result = xmlSchemaType.ValueConverter.ChangeType(value, returnType, namespaceResolver);
				}
				else
				{
					result = XmlUntypedConverter.Untyped.ChangeType(value, returnType, namespaceResolver);
				}
			}
			catch (FormatException innerException)
			{
				throw new XmlException("Content cannot be converted to the type {0}.", returnType.ToString(), innerException, this);
			}
			catch (InvalidCastException innerException2)
			{
				throw new XmlException("Content cannot be converted to the type {0}.", returnType.ToString(), innerException2, this);
			}
			catch (OverflowException innerException3)
			{
				throw new XmlException("Content cannot be converted to the type {0}.", returnType.ToString(), innerException3, this);
			}
			return result;
		}

		// Token: 0x1700023D RID: 573
		// (get) Token: 0x06000E6B RID: 3691 RVA: 0x0005DC60 File Offset: 0x0005BE60
		public override int AttributeCount
		{
			get
			{
				return this.attributeCount;
			}
		}

		// Token: 0x06000E6C RID: 3692 RVA: 0x0005DC68 File Offset: 0x0005BE68
		public override string GetAttribute(string name)
		{
			string text = this.coreReader.GetAttribute(name);
			if (text == null && this.attributeCount > 0)
			{
				ValidatingReaderNodeData defaultAttribute = this.GetDefaultAttribute(name, false);
				if (defaultAttribute != null)
				{
					text = defaultAttribute.RawValue;
				}
			}
			return text;
		}

		// Token: 0x06000E6D RID: 3693 RVA: 0x0005DCA4 File Offset: 0x0005BEA4
		public override string GetAttribute(string name, string namespaceURI)
		{
			string attribute = this.coreReader.GetAttribute(name, namespaceURI);
			if (attribute == null && this.attributeCount > 0)
			{
				namespaceURI = ((namespaceURI == null) ? string.Empty : this.coreReaderNameTable.Get(namespaceURI));
				name = this.coreReaderNameTable.Get(name);
				if (name == null || namespaceURI == null)
				{
					return null;
				}
				ValidatingReaderNodeData defaultAttribute = this.GetDefaultAttribute(name, namespaceURI, false);
				if (defaultAttribute != null)
				{
					return defaultAttribute.RawValue;
				}
			}
			return attribute;
		}

		// Token: 0x06000E6E RID: 3694 RVA: 0x0005DD10 File Offset: 0x0005BF10
		public override string GetAttribute(int i)
		{
			if (this.attributeCount == 0)
			{
				return null;
			}
			if (i < this.coreReaderAttributeCount)
			{
				return this.coreReader.GetAttribute(i);
			}
			int index = i - this.coreReaderAttributeCount;
			return ((ValidatingReaderNodeData)this.defaultAttributes[index]).RawValue;
		}

		// Token: 0x06000E6F RID: 3695 RVA: 0x0005DD5C File Offset: 0x0005BF5C
		public override bool MoveToAttribute(string name)
		{
			if (!this.coreReader.MoveToAttribute(name))
			{
				if (this.attributeCount > 0)
				{
					ValidatingReaderNodeData defaultAttribute = this.GetDefaultAttribute(name, true);
					if (defaultAttribute != null)
					{
						this.validationState = XsdValidatingReader.ValidatingReaderState.OnDefaultAttribute;
						this.attributePSVI = defaultAttribute.AttInfo;
						this.cachedNode = defaultAttribute;
						goto IL_57;
					}
				}
				return false;
			}
			this.validationState = XsdValidatingReader.ValidatingReaderState.OnAttribute;
			this.attributePSVI = this.GetAttributePSVI(name);
			IL_57:
			if (this.validationState == XsdValidatingReader.ValidatingReaderState.OnReadBinaryContent)
			{
				this.readBinaryHelper.Finish();
				this.validationState = this.savedState;
			}
			return true;
		}

		// Token: 0x06000E70 RID: 3696 RVA: 0x0005DDE4 File Offset: 0x0005BFE4
		public override bool MoveToAttribute(string name, string ns)
		{
			name = this.coreReaderNameTable.Get(name);
			ns = ((ns != null) ? this.coreReaderNameTable.Get(ns) : string.Empty);
			if (name == null || ns == null)
			{
				return false;
			}
			if (this.coreReader.MoveToAttribute(name, ns))
			{
				this.validationState = XsdValidatingReader.ValidatingReaderState.OnAttribute;
				if (this.inlineSchemaParser == null)
				{
					this.attributePSVI = this.GetAttributePSVI(name, ns);
				}
				else
				{
					this.attributePSVI = null;
				}
			}
			else
			{
				ValidatingReaderNodeData defaultAttribute = this.GetDefaultAttribute(name, ns, true);
				if (defaultAttribute == null)
				{
					return false;
				}
				this.attributePSVI = defaultAttribute.AttInfo;
				this.cachedNode = defaultAttribute;
				this.validationState = XsdValidatingReader.ValidatingReaderState.OnDefaultAttribute;
			}
			if (this.validationState == XsdValidatingReader.ValidatingReaderState.OnReadBinaryContent)
			{
				this.readBinaryHelper.Finish();
				this.validationState = this.savedState;
			}
			return true;
		}

		// Token: 0x06000E71 RID: 3697 RVA: 0x0005DEA4 File Offset: 0x0005C0A4
		public override void MoveToAttribute(int i)
		{
			if (i < 0 || i >= this.attributeCount)
			{
				throw new ArgumentOutOfRangeException("i");
			}
			this.currentAttrIndex = i;
			if (i < this.coreReaderAttributeCount)
			{
				this.coreReader.MoveToAttribute(i);
				if (this.inlineSchemaParser == null)
				{
					this.attributePSVI = this.attributePSVINodes[i];
				}
				else
				{
					this.attributePSVI = null;
				}
				this.validationState = XsdValidatingReader.ValidatingReaderState.OnAttribute;
			}
			else
			{
				int index = i - this.coreReaderAttributeCount;
				this.cachedNode = (ValidatingReaderNodeData)this.defaultAttributes[index];
				this.attributePSVI = this.cachedNode.AttInfo;
				this.validationState = XsdValidatingReader.ValidatingReaderState.OnDefaultAttribute;
			}
			if (this.validationState == XsdValidatingReader.ValidatingReaderState.OnReadBinaryContent)
			{
				this.readBinaryHelper.Finish();
				this.validationState = this.savedState;
			}
		}

		// Token: 0x06000E72 RID: 3698 RVA: 0x0005DF68 File Offset: 0x0005C168
		public override bool MoveToFirstAttribute()
		{
			if (this.coreReader.MoveToFirstAttribute())
			{
				this.currentAttrIndex = 0;
				if (this.inlineSchemaParser == null)
				{
					this.attributePSVI = this.attributePSVINodes[0];
				}
				else
				{
					this.attributePSVI = null;
				}
				this.validationState = XsdValidatingReader.ValidatingReaderState.OnAttribute;
			}
			else
			{
				if (this.defaultAttributes.Count <= 0)
				{
					return false;
				}
				this.cachedNode = (ValidatingReaderNodeData)this.defaultAttributes[0];
				this.attributePSVI = this.cachedNode.AttInfo;
				this.currentAttrIndex = 0;
				this.validationState = XsdValidatingReader.ValidatingReaderState.OnDefaultAttribute;
			}
			if (this.validationState == XsdValidatingReader.ValidatingReaderState.OnReadBinaryContent)
			{
				this.readBinaryHelper.Finish();
				this.validationState = this.savedState;
			}
			return true;
		}

		// Token: 0x06000E73 RID: 3699 RVA: 0x0005E01C File Offset: 0x0005C21C
		public override bool MoveToNextAttribute()
		{
			if (this.currentAttrIndex + 1 < this.coreReaderAttributeCount)
			{
				this.coreReader.MoveToNextAttribute();
				this.currentAttrIndex++;
				if (this.inlineSchemaParser == null)
				{
					this.attributePSVI = this.attributePSVINodes[this.currentAttrIndex];
				}
				else
				{
					this.attributePSVI = null;
				}
				this.validationState = XsdValidatingReader.ValidatingReaderState.OnAttribute;
			}
			else
			{
				if (this.currentAttrIndex + 1 >= this.attributeCount)
				{
					return false;
				}
				int num = this.currentAttrIndex + 1;
				this.currentAttrIndex = num;
				int index = num - this.coreReaderAttributeCount;
				this.cachedNode = (ValidatingReaderNodeData)this.defaultAttributes[index];
				this.attributePSVI = this.cachedNode.AttInfo;
				this.validationState = XsdValidatingReader.ValidatingReaderState.OnDefaultAttribute;
			}
			if (this.validationState == XsdValidatingReader.ValidatingReaderState.OnReadBinaryContent)
			{
				this.readBinaryHelper.Finish();
				this.validationState = this.savedState;
			}
			return true;
		}

		// Token: 0x06000E74 RID: 3700 RVA: 0x0005E0FD File Offset: 0x0005C2FD
		public override bool MoveToElement()
		{
			if (this.coreReader.MoveToElement() || this.validationState < XsdValidatingReader.ValidatingReaderState.None)
			{
				this.currentAttrIndex = -1;
				this.validationState = XsdValidatingReader.ValidatingReaderState.ClearAttributes;
				return true;
			}
			return false;
		}

		// Token: 0x06000E75 RID: 3701 RVA: 0x0005E128 File Offset: 0x0005C328
		public override bool Read()
		{
			switch (this.validationState)
			{
			case XsdValidatingReader.ValidatingReaderState.OnReadAttributeValue:
			case XsdValidatingReader.ValidatingReaderState.OnDefaultAttribute:
			case XsdValidatingReader.ValidatingReaderState.OnAttribute:
			case XsdValidatingReader.ValidatingReaderState.ClearAttributes:
				this.ClearAttributesInfo();
				if (this.inlineSchemaParser != null)
				{
					this.validationState = XsdValidatingReader.ValidatingReaderState.ParseInlineSchema;
					goto IL_7C;
				}
				this.validationState = XsdValidatingReader.ValidatingReaderState.Read;
				break;
			case XsdValidatingReader.ValidatingReaderState.None:
				return false;
			case XsdValidatingReader.ValidatingReaderState.Init:
				this.validationState = XsdValidatingReader.ValidatingReaderState.Read;
				if (this.coreReader.ReadState == ReadState.Interactive)
				{
					this.ProcessReaderEvent();
					return true;
				}
				break;
			case XsdValidatingReader.ValidatingReaderState.Read:
				break;
			case XsdValidatingReader.ValidatingReaderState.ParseInlineSchema:
				goto IL_7C;
			case XsdValidatingReader.ValidatingReaderState.ReadAhead:
				this.ClearAttributesInfo();
				this.ProcessReaderEvent();
				this.validationState = XsdValidatingReader.ValidatingReaderState.Read;
				return true;
			case XsdValidatingReader.ValidatingReaderState.OnReadBinaryContent:
				this.validationState = this.savedState;
				this.readBinaryHelper.Finish();
				return this.Read();
			case XsdValidatingReader.ValidatingReaderState.ReaderClosed:
			case XsdValidatingReader.ValidatingReaderState.EOF:
				return false;
			default:
				return false;
			}
			if (this.coreReader.Read())
			{
				this.ProcessReaderEvent();
				return true;
			}
			this.validator.EndValidation();
			if (this.coreReader.EOF)
			{
				this.validationState = XsdValidatingReader.ValidatingReaderState.EOF;
			}
			return false;
			IL_7C:
			this.ProcessInlineSchema();
			return true;
		}

		// Token: 0x1700023E RID: 574
		// (get) Token: 0x06000E76 RID: 3702 RVA: 0x0005E22F File Offset: 0x0005C42F
		public override bool EOF
		{
			get
			{
				return this.coreReader.EOF;
			}
		}

		// Token: 0x06000E77 RID: 3703 RVA: 0x0005E23C File Offset: 0x0005C43C
		public override void Close()
		{
			this.coreReader.Close();
			this.validationState = XsdValidatingReader.ValidatingReaderState.ReaderClosed;
		}

		// Token: 0x1700023F RID: 575
		// (get) Token: 0x06000E78 RID: 3704 RVA: 0x0005E250 File Offset: 0x0005C450
		public override ReadState ReadState
		{
			get
			{
				if (this.validationState != XsdValidatingReader.ValidatingReaderState.Init)
				{
					return this.coreReader.ReadState;
				}
				return ReadState.Initial;
			}
		}

		// Token: 0x06000E79 RID: 3705 RVA: 0x0005E268 File Offset: 0x0005C468
		public override void Skip()
		{
			int depth = this.Depth;
			XmlNodeType nodeType = this.NodeType;
			if (nodeType != XmlNodeType.Element)
			{
				if (nodeType != XmlNodeType.Attribute)
				{
					goto IL_81;
				}
				this.MoveToElement();
			}
			if (!this.coreReader.IsEmptyElement)
			{
				bool flag = true;
				if ((this.xmlSchemaInfo.IsUnionType || this.xmlSchemaInfo.IsDefault) && this.coreReader is XsdCachingReader)
				{
					flag = false;
				}
				this.coreReader.Skip();
				this.validationState = XsdValidatingReader.ValidatingReaderState.ReadAhead;
				if (flag)
				{
					this.validator.SkipToEndElement(this.xmlSchemaInfo);
				}
			}
			IL_81:
			this.Read();
		}

		// Token: 0x17000240 RID: 576
		// (get) Token: 0x06000E7A RID: 3706 RVA: 0x0005E2FD File Offset: 0x0005C4FD
		public override XmlNameTable NameTable
		{
			get
			{
				return this.coreReaderNameTable;
			}
		}

		// Token: 0x06000E7B RID: 3707 RVA: 0x0005E305 File Offset: 0x0005C505
		public override string LookupNamespace(string prefix)
		{
			return this.thisNSResolver.LookupNamespace(prefix);
		}

		// Token: 0x06000E7C RID: 3708 RVA: 0x0005BE99 File Offset: 0x0005A099
		public override void ResolveEntity()
		{
			throw new InvalidOperationException();
		}

		// Token: 0x06000E7D RID: 3709 RVA: 0x0005E314 File Offset: 0x0005C514
		public override bool ReadAttributeValue()
		{
			if (this.validationState == XsdValidatingReader.ValidatingReaderState.OnReadBinaryContent)
			{
				this.readBinaryHelper.Finish();
				this.validationState = this.savedState;
			}
			if (this.NodeType != XmlNodeType.Attribute)
			{
				return false;
			}
			if (this.validationState == XsdValidatingReader.ValidatingReaderState.OnDefaultAttribute)
			{
				this.cachedNode = this.CreateDummyTextNode(this.cachedNode.RawValue, this.cachedNode.Depth + 1);
				this.validationState = XsdValidatingReader.ValidatingReaderState.OnReadAttributeValue;
				return true;
			}
			return this.coreReader.ReadAttributeValue();
		}

		// Token: 0x17000241 RID: 577
		// (get) Token: 0x06000E7E RID: 3710 RVA: 0x0001222F File Offset: 0x0001042F
		public override bool CanReadBinaryContent
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000E7F RID: 3711 RVA: 0x0005E390 File Offset: 0x0005C590
		public override int ReadContentAsBase64(byte[] buffer, int index, int count)
		{
			if (this.ReadState != ReadState.Interactive)
			{
				return 0;
			}
			if (this.validationState != XsdValidatingReader.ValidatingReaderState.OnReadBinaryContent)
			{
				this.readBinaryHelper = ReadContentAsBinaryHelper.CreateOrReset(this.readBinaryHelper, this);
				this.savedState = this.validationState;
			}
			this.validationState = this.savedState;
			int result = this.readBinaryHelper.ReadContentAsBase64(buffer, index, count);
			this.savedState = this.validationState;
			this.validationState = XsdValidatingReader.ValidatingReaderState.OnReadBinaryContent;
			return result;
		}

		// Token: 0x06000E80 RID: 3712 RVA: 0x0005E3FC File Offset: 0x0005C5FC
		public override int ReadContentAsBinHex(byte[] buffer, int index, int count)
		{
			if (this.ReadState != ReadState.Interactive)
			{
				return 0;
			}
			if (this.validationState != XsdValidatingReader.ValidatingReaderState.OnReadBinaryContent)
			{
				this.readBinaryHelper = ReadContentAsBinaryHelper.CreateOrReset(this.readBinaryHelper, this);
				this.savedState = this.validationState;
			}
			this.validationState = this.savedState;
			int result = this.readBinaryHelper.ReadContentAsBinHex(buffer, index, count);
			this.savedState = this.validationState;
			this.validationState = XsdValidatingReader.ValidatingReaderState.OnReadBinaryContent;
			return result;
		}

		// Token: 0x06000E81 RID: 3713 RVA: 0x0005E468 File Offset: 0x0005C668
		public override int ReadElementContentAsBase64(byte[] buffer, int index, int count)
		{
			if (this.ReadState != ReadState.Interactive)
			{
				return 0;
			}
			if (this.validationState != XsdValidatingReader.ValidatingReaderState.OnReadBinaryContent)
			{
				this.readBinaryHelper = ReadContentAsBinaryHelper.CreateOrReset(this.readBinaryHelper, this);
				this.savedState = this.validationState;
			}
			this.validationState = this.savedState;
			int result = this.readBinaryHelper.ReadElementContentAsBase64(buffer, index, count);
			this.savedState = this.validationState;
			this.validationState = XsdValidatingReader.ValidatingReaderState.OnReadBinaryContent;
			return result;
		}

		// Token: 0x06000E82 RID: 3714 RVA: 0x0005E4D4 File Offset: 0x0005C6D4
		public override int ReadElementContentAsBinHex(byte[] buffer, int index, int count)
		{
			if (this.ReadState != ReadState.Interactive)
			{
				return 0;
			}
			if (this.validationState != XsdValidatingReader.ValidatingReaderState.OnReadBinaryContent)
			{
				this.readBinaryHelper = ReadContentAsBinaryHelper.CreateOrReset(this.readBinaryHelper, this);
				this.savedState = this.validationState;
			}
			this.validationState = this.savedState;
			int result = this.readBinaryHelper.ReadElementContentAsBinHex(buffer, index, count);
			this.savedState = this.validationState;
			this.validationState = XsdValidatingReader.ValidatingReaderState.OnReadBinaryContent;
			return result;
		}

		// Token: 0x17000242 RID: 578
		// (get) Token: 0x06000E83 RID: 3715 RVA: 0x0005E540 File Offset: 0x0005C740
		bool IXmlSchemaInfo.IsDefault
		{
			get
			{
				XmlNodeType nodeType = this.NodeType;
				if (nodeType != XmlNodeType.Element)
				{
					if (nodeType != XmlNodeType.Attribute)
					{
						if (nodeType == XmlNodeType.EndElement)
						{
							return this.xmlSchemaInfo.IsDefault;
						}
					}
					else if (this.attributePSVI != null)
					{
						return this.AttributeSchemaInfo.IsDefault;
					}
					return false;
				}
				if (!this.coreReader.IsEmptyElement)
				{
					this.GetIsDefault();
				}
				return this.xmlSchemaInfo.IsDefault;
			}
		}

		// Token: 0x17000243 RID: 579
		// (get) Token: 0x06000E84 RID: 3716 RVA: 0x0005E5A4 File Offset: 0x0005C7A4
		bool IXmlSchemaInfo.IsNil
		{
			get
			{
				XmlNodeType nodeType = this.NodeType;
				return (nodeType == XmlNodeType.Element || nodeType == XmlNodeType.EndElement) && this.xmlSchemaInfo.IsNil;
			}
		}

		// Token: 0x17000244 RID: 580
		// (get) Token: 0x06000E85 RID: 3717 RVA: 0x0005E5D0 File Offset: 0x0005C7D0
		XmlSchemaValidity IXmlSchemaInfo.Validity
		{
			get
			{
				XmlNodeType nodeType = this.NodeType;
				if (nodeType != XmlNodeType.Element)
				{
					if (nodeType != XmlNodeType.Attribute)
					{
						if (nodeType == XmlNodeType.EndElement)
						{
							return this.xmlSchemaInfo.Validity;
						}
					}
					else if (this.attributePSVI != null)
					{
						return this.AttributeSchemaInfo.Validity;
					}
					return XmlSchemaValidity.NotKnown;
				}
				if (this.coreReader.IsEmptyElement)
				{
					return this.xmlSchemaInfo.Validity;
				}
				if (this.xmlSchemaInfo.Validity == XmlSchemaValidity.Valid)
				{
					return XmlSchemaValidity.NotKnown;
				}
				return this.xmlSchemaInfo.Validity;
			}
		}

		// Token: 0x17000245 RID: 581
		// (get) Token: 0x06000E86 RID: 3718 RVA: 0x0005E64C File Offset: 0x0005C84C
		XmlSchemaSimpleType IXmlSchemaInfo.MemberType
		{
			get
			{
				XmlNodeType nodeType = this.NodeType;
				if (nodeType == XmlNodeType.Element)
				{
					if (!this.coreReader.IsEmptyElement)
					{
						this.GetMemberType();
					}
					return this.xmlSchemaInfo.MemberType;
				}
				if (nodeType != XmlNodeType.Attribute)
				{
					if (nodeType != XmlNodeType.EndElement)
					{
						return null;
					}
					return this.xmlSchemaInfo.MemberType;
				}
				else
				{
					if (this.attributePSVI != null)
					{
						return this.AttributeSchemaInfo.MemberType;
					}
					return null;
				}
			}
		}

		// Token: 0x17000246 RID: 582
		// (get) Token: 0x06000E87 RID: 3719 RVA: 0x0005E6B4 File Offset: 0x0005C8B4
		XmlSchemaType IXmlSchemaInfo.SchemaType
		{
			get
			{
				XmlNodeType nodeType = this.NodeType;
				if (nodeType != XmlNodeType.Element)
				{
					if (nodeType != XmlNodeType.Attribute)
					{
						if (nodeType != XmlNodeType.EndElement)
						{
							return null;
						}
					}
					else
					{
						if (this.attributePSVI != null)
						{
							return this.AttributeSchemaInfo.SchemaType;
						}
						return null;
					}
				}
				return this.xmlSchemaInfo.SchemaType;
			}
		}

		// Token: 0x17000247 RID: 583
		// (get) Token: 0x06000E88 RID: 3720 RVA: 0x0005E6F8 File Offset: 0x0005C8F8
		XmlSchemaElement IXmlSchemaInfo.SchemaElement
		{
			get
			{
				if (this.NodeType == XmlNodeType.Element || this.NodeType == XmlNodeType.EndElement)
				{
					return this.xmlSchemaInfo.SchemaElement;
				}
				return null;
			}
		}

		// Token: 0x17000248 RID: 584
		// (get) Token: 0x06000E89 RID: 3721 RVA: 0x0005E71A File Offset: 0x0005C91A
		XmlSchemaAttribute IXmlSchemaInfo.SchemaAttribute
		{
			get
			{
				if (this.NodeType == XmlNodeType.Attribute && this.attributePSVI != null)
				{
					return this.AttributeSchemaInfo.SchemaAttribute;
				}
				return null;
			}
		}

		// Token: 0x06000E8A RID: 3722 RVA: 0x0001222F File Offset: 0x0001042F
		public bool HasLineInfo()
		{
			return true;
		}

		// Token: 0x17000249 RID: 585
		// (get) Token: 0x06000E8B RID: 3723 RVA: 0x0005E73A File Offset: 0x0005C93A
		public int LineNumber
		{
			get
			{
				if (this.lineInfo != null)
				{
					return this.lineInfo.LineNumber;
				}
				return 0;
			}
		}

		// Token: 0x1700024A RID: 586
		// (get) Token: 0x06000E8C RID: 3724 RVA: 0x0005E751 File Offset: 0x0005C951
		public int LinePosition
		{
			get
			{
				if (this.lineInfo != null)
				{
					return this.lineInfo.LinePosition;
				}
				return 0;
			}
		}

		// Token: 0x06000E8D RID: 3725 RVA: 0x0005E768 File Offset: 0x0005C968
		IDictionary<string, string> IXmlNamespaceResolver.GetNamespacesInScope(XmlNamespaceScope scope)
		{
			if (this.coreReaderNSResolver != null)
			{
				return this.coreReaderNSResolver.GetNamespacesInScope(scope);
			}
			return this.nsManager.GetNamespacesInScope(scope);
		}

		// Token: 0x06000E8E RID: 3726 RVA: 0x0005E78B File Offset: 0x0005C98B
		string IXmlNamespaceResolver.LookupNamespace(string prefix)
		{
			if (this.coreReaderNSResolver != null)
			{
				return this.coreReaderNSResolver.LookupNamespace(prefix);
			}
			return this.nsManager.LookupNamespace(prefix);
		}

		// Token: 0x06000E8F RID: 3727 RVA: 0x0005E7AE File Offset: 0x0005C9AE
		string IXmlNamespaceResolver.LookupPrefix(string namespaceName)
		{
			if (this.coreReaderNSResolver != null)
			{
				return this.coreReaderNSResolver.LookupPrefix(namespaceName);
			}
			return this.nsManager.LookupPrefix(namespaceName);
		}

		// Token: 0x06000E90 RID: 3728 RVA: 0x0005E7D1 File Offset: 0x0005C9D1
		private object GetStringValue()
		{
			return this.coreReader.Value;
		}

		// Token: 0x1700024B RID: 587
		// (get) Token: 0x06000E91 RID: 3729 RVA: 0x0005E7DE File Offset: 0x0005C9DE
		private XmlSchemaType ElementXmlType
		{
			get
			{
				return this.xmlSchemaInfo.XmlType;
			}
		}

		// Token: 0x1700024C RID: 588
		// (get) Token: 0x06000E92 RID: 3730 RVA: 0x0005E7EB File Offset: 0x0005C9EB
		private XmlSchemaType AttributeXmlType
		{
			get
			{
				if (this.attributePSVI != null)
				{
					return this.AttributeSchemaInfo.XmlType;
				}
				return null;
			}
		}

		// Token: 0x1700024D RID: 589
		// (get) Token: 0x06000E93 RID: 3731 RVA: 0x0005E802 File Offset: 0x0005CA02
		private XmlSchemaInfo AttributeSchemaInfo
		{
			get
			{
				return this.attributePSVI.attributeSchemaInfo;
			}
		}

		// Token: 0x06000E94 RID: 3732 RVA: 0x0005E810 File Offset: 0x0005CA10
		private void ProcessReaderEvent()
		{
			if (this.replayCache)
			{
				return;
			}
			switch (this.coreReader.NodeType)
			{
			case XmlNodeType.Element:
				this.ProcessElementEvent();
				return;
			case XmlNodeType.Attribute:
			case XmlNodeType.Entity:
			case XmlNodeType.ProcessingInstruction:
			case XmlNodeType.Comment:
			case XmlNodeType.Document:
			case XmlNodeType.DocumentFragment:
			case XmlNodeType.Notation:
				break;
			case XmlNodeType.Text:
			case XmlNodeType.CDATA:
				this.validator.ValidateText(new XmlValueGetter(this.GetStringValue));
				return;
			case XmlNodeType.EntityReference:
				throw new InvalidOperationException();
			case XmlNodeType.DocumentType:
				this.validator.SetDtdSchemaInfo(this.coreReader.DtdInfo);
				break;
			case XmlNodeType.Whitespace:
			case XmlNodeType.SignificantWhitespace:
				this.validator.ValidateWhitespace(new XmlValueGetter(this.GetStringValue));
				return;
			case XmlNodeType.EndElement:
				this.ProcessEndElementEvent();
				return;
			default:
				return;
			}
		}

		// Token: 0x06000E95 RID: 3733 RVA: 0x0005E8D4 File Offset: 0x0005CAD4
		private void ProcessElementEvent()
		{
			if (!this.processInlineSchema || !this.IsXSDRoot(this.coreReader.LocalName, this.coreReader.NamespaceURI) || this.coreReader.Depth <= 0)
			{
				this.atomicValue = null;
				this.originalAtomicValueString = null;
				this.xmlSchemaInfo.Clear();
				if (this.manageNamespaces)
				{
					this.nsManager.PushScope();
				}
				string xsiSchemaLocation = null;
				string xsiNoNamespaceSchemaLocation = null;
				string xsiNil = null;
				string xsiType = null;
				if (this.coreReader.MoveToFirstAttribute())
				{
					do
					{
						string namespaceURI = this.coreReader.NamespaceURI;
						string localName = this.coreReader.LocalName;
						if (Ref.Equal(namespaceURI, this.NsXsi))
						{
							if (Ref.Equal(localName, this.XsiSchemaLocation))
							{
								xsiSchemaLocation = this.coreReader.Value;
							}
							else if (Ref.Equal(localName, this.XsiNoNamespaceSchemaLocation))
							{
								xsiNoNamespaceSchemaLocation = this.coreReader.Value;
							}
							else if (Ref.Equal(localName, this.XsiType))
							{
								xsiType = this.coreReader.Value;
							}
							else if (Ref.Equal(localName, this.XsiNil))
							{
								xsiNil = this.coreReader.Value;
							}
						}
						if (this.manageNamespaces && Ref.Equal(this.coreReader.NamespaceURI, this.NsXmlNs))
						{
							this.nsManager.AddNamespace((this.coreReader.Prefix.Length == 0) ? string.Empty : this.coreReader.LocalName, this.coreReader.Value);
						}
					}
					while (this.coreReader.MoveToNextAttribute());
					this.coreReader.MoveToElement();
				}
				this.validator.ValidateElement(this.coreReader.LocalName, this.coreReader.NamespaceURI, this.xmlSchemaInfo, xsiType, xsiNil, xsiSchemaLocation, xsiNoNamespaceSchemaLocation);
				this.ValidateAttributes();
				this.validator.ValidateEndOfAttributes(this.xmlSchemaInfo);
				if (this.coreReader.IsEmptyElement)
				{
					this.ProcessEndElementEvent();
				}
				this.validationState = XsdValidatingReader.ValidatingReaderState.ClearAttributes;
				return;
			}
			this.xmlSchemaInfo.Clear();
			this.attributeCount = (this.coreReaderAttributeCount = this.coreReader.AttributeCount);
			if (!this.coreReader.IsEmptyElement)
			{
				this.inlineSchemaParser = new Parser(SchemaType.XSD, this.coreReaderNameTable, this.validator.SchemaSet.GetSchemaNames(this.coreReaderNameTable), this.validationEvent);
				this.inlineSchemaParser.StartParsing(this.coreReader, null);
				this.inlineSchemaParser.ParseReaderNode();
				this.validationState = XsdValidatingReader.ValidatingReaderState.ParseInlineSchema;
				return;
			}
			this.validationState = XsdValidatingReader.ValidatingReaderState.ClearAttributes;
		}

		// Token: 0x06000E96 RID: 3734 RVA: 0x0005EB5C File Offset: 0x0005CD5C
		private void ProcessEndElementEvent()
		{
			this.atomicValue = this.validator.ValidateEndElement(this.xmlSchemaInfo);
			this.originalAtomicValueString = this.GetOriginalAtomicValueStringOfElement();
			if (this.xmlSchemaInfo.IsDefault)
			{
				int depth = this.coreReader.Depth;
				this.coreReader = this.GetCachingReader();
				this.cachingReader.RecordTextNode(this.xmlSchemaInfo.XmlType.ValueConverter.ToString(this.atomicValue), this.originalAtomicValueString, depth + 1, 0, 0);
				this.cachingReader.RecordEndElementNode();
				this.cachingReader.SetToReplayMode();
				this.replayCache = true;
				return;
			}
			if (this.manageNamespaces)
			{
				this.nsManager.PopScope();
			}
		}

		// Token: 0x06000E97 RID: 3735 RVA: 0x0005EC18 File Offset: 0x0005CE18
		private void ValidateAttributes()
		{
			this.attributeCount = (this.coreReaderAttributeCount = this.coreReader.AttributeCount);
			int num = 0;
			bool flag = false;
			if (this.coreReader.MoveToFirstAttribute())
			{
				do
				{
					string localName = this.coreReader.LocalName;
					string namespaceURI = this.coreReader.NamespaceURI;
					AttributePSVIInfo attributePSVIInfo = this.AddAttributePSVI(num);
					attributePSVIInfo.localName = localName;
					attributePSVIInfo.namespaceUri = namespaceURI;
					if (namespaceURI == this.NsXmlNs)
					{
						num++;
					}
					else
					{
						attributePSVIInfo.typedAttributeValue = this.validator.ValidateAttribute(localName, namespaceURI, this.valueGetter, attributePSVIInfo.attributeSchemaInfo);
						if (!flag)
						{
							flag = (attributePSVIInfo.attributeSchemaInfo.Validity == XmlSchemaValidity.Invalid);
						}
						num++;
					}
				}
				while (this.coreReader.MoveToNextAttribute());
			}
			this.coreReader.MoveToElement();
			if (flag)
			{
				this.xmlSchemaInfo.Validity = XmlSchemaValidity.Invalid;
			}
			this.validator.GetUnspecifiedDefaultAttributes(this.defaultAttributes, true);
			this.attributeCount += this.defaultAttributes.Count;
		}

		// Token: 0x06000E98 RID: 3736 RVA: 0x0005ED21 File Offset: 0x0005CF21
		private void ClearAttributesInfo()
		{
			this.attributeCount = 0;
			this.coreReaderAttributeCount = 0;
			this.currentAttrIndex = -1;
			this.defaultAttributes.Clear();
			this.attributePSVI = null;
		}

		// Token: 0x06000E99 RID: 3737 RVA: 0x0005ED4C File Offset: 0x0005CF4C
		private AttributePSVIInfo GetAttributePSVI(string name)
		{
			if (this.inlineSchemaParser != null)
			{
				return null;
			}
			string text;
			string text2;
			ValidateNames.SplitQName(name, out text, out text2);
			text = this.coreReaderNameTable.Add(text);
			text2 = this.coreReaderNameTable.Add(text2);
			string ns;
			if (text.Length == 0)
			{
				ns = string.Empty;
			}
			else
			{
				ns = this.thisNSResolver.LookupNamespace(text);
			}
			return this.GetAttributePSVI(text2, ns);
		}

		// Token: 0x06000E9A RID: 3738 RVA: 0x0005EDAC File Offset: 0x0005CFAC
		private AttributePSVIInfo GetAttributePSVI(string localName, string ns)
		{
			for (int i = 0; i < this.coreReaderAttributeCount; i++)
			{
				AttributePSVIInfo attributePSVIInfo = this.attributePSVINodes[i];
				if (attributePSVIInfo != null && Ref.Equal(localName, attributePSVIInfo.localName) && Ref.Equal(ns, attributePSVIInfo.namespaceUri))
				{
					this.currentAttrIndex = i;
					return attributePSVIInfo;
				}
			}
			return null;
		}

		// Token: 0x06000E9B RID: 3739 RVA: 0x0005EE00 File Offset: 0x0005D000
		private ValidatingReaderNodeData GetDefaultAttribute(string name, bool updatePosition)
		{
			string text;
			string text2;
			ValidateNames.SplitQName(name, out text, out text2);
			text = this.coreReaderNameTable.Add(text);
			text2 = this.coreReaderNameTable.Add(text2);
			string ns;
			if (text.Length == 0)
			{
				ns = string.Empty;
			}
			else
			{
				ns = this.thisNSResolver.LookupNamespace(text);
			}
			return this.GetDefaultAttribute(text2, ns, updatePosition);
		}

		// Token: 0x06000E9C RID: 3740 RVA: 0x0005EE58 File Offset: 0x0005D058
		private ValidatingReaderNodeData GetDefaultAttribute(string attrLocalName, string ns, bool updatePosition)
		{
			for (int i = 0; i < this.defaultAttributes.Count; i++)
			{
				ValidatingReaderNodeData validatingReaderNodeData = (ValidatingReaderNodeData)this.defaultAttributes[i];
				if (Ref.Equal(validatingReaderNodeData.LocalName, attrLocalName) && Ref.Equal(validatingReaderNodeData.Namespace, ns))
				{
					if (updatePosition)
					{
						this.currentAttrIndex = this.coreReader.AttributeCount + i;
					}
					return validatingReaderNodeData;
				}
			}
			return null;
		}

		// Token: 0x06000E9D RID: 3741 RVA: 0x0005EEC4 File Offset: 0x0005D0C4
		private AttributePSVIInfo AddAttributePSVI(int attIndex)
		{
			AttributePSVIInfo attributePSVIInfo = this.attributePSVINodes[attIndex];
			if (attributePSVIInfo != null)
			{
				attributePSVIInfo.Reset();
				return attributePSVIInfo;
			}
			if (attIndex >= this.attributePSVINodes.Length - 1)
			{
				AttributePSVIInfo[] destinationArray = new AttributePSVIInfo[this.attributePSVINodes.Length * 2];
				Array.Copy(this.attributePSVINodes, 0, destinationArray, 0, this.attributePSVINodes.Length);
				this.attributePSVINodes = destinationArray;
			}
			attributePSVIInfo = this.attributePSVINodes[attIndex];
			if (attributePSVIInfo == null)
			{
				attributePSVIInfo = new AttributePSVIInfo();
				this.attributePSVINodes[attIndex] = attributePSVIInfo;
			}
			return attributePSVIInfo;
		}

		// Token: 0x06000E9E RID: 3742 RVA: 0x0005EF3B File Offset: 0x0005D13B
		private bool IsXSDRoot(string localName, string ns)
		{
			return Ref.Equal(ns, this.NsXs) && Ref.Equal(localName, this.XsdSchema);
		}

		// Token: 0x06000E9F RID: 3743 RVA: 0x0005EF5C File Offset: 0x0005D15C
		private void ProcessInlineSchema()
		{
			if (this.coreReader.Read())
			{
				if (this.coreReader.NodeType == XmlNodeType.Element)
				{
					this.attributeCount = (this.coreReaderAttributeCount = this.coreReader.AttributeCount);
				}
				else
				{
					this.ClearAttributesInfo();
				}
				if (!this.inlineSchemaParser.ParseReaderNode())
				{
					this.inlineSchemaParser.FinishParsing();
					XmlSchema xmlSchema = this.inlineSchemaParser.XmlSchema;
					this.validator.AddSchema(xmlSchema);
					this.inlineSchemaParser = null;
					this.validationState = XsdValidatingReader.ValidatingReaderState.Read;
				}
			}
		}

		// Token: 0x06000EA0 RID: 3744 RVA: 0x0005EFE5 File Offset: 0x0005D1E5
		private object InternalReadContentAsObject()
		{
			return this.InternalReadContentAsObject(false);
		}

		// Token: 0x06000EA1 RID: 3745 RVA: 0x0005EFF0 File Offset: 0x0005D1F0
		private object InternalReadContentAsObject(bool unwrapTypedValue)
		{
			string text;
			return this.InternalReadContentAsObject(unwrapTypedValue, out text);
		}

		// Token: 0x06000EA2 RID: 3746 RVA: 0x0005F008 File Offset: 0x0005D208
		private object InternalReadContentAsObject(bool unwrapTypedValue, out string originalStringValue)
		{
			XmlNodeType nodeType = this.NodeType;
			if (nodeType == XmlNodeType.Attribute)
			{
				originalStringValue = this.Value;
				if (this.attributePSVI != null && this.attributePSVI.typedAttributeValue != null)
				{
					if (this.validationState == XsdValidatingReader.ValidatingReaderState.OnDefaultAttribute)
					{
						XmlSchemaAttribute schemaAttribute = this.attributePSVI.attributeSchemaInfo.SchemaAttribute;
						originalStringValue = ((schemaAttribute.DefaultValue != null) ? schemaAttribute.DefaultValue : schemaAttribute.FixedValue);
					}
					return this.ReturnBoxedValue(this.attributePSVI.typedAttributeValue, this.AttributeSchemaInfo.XmlType, unwrapTypedValue);
				}
				return this.Value;
			}
			else if (nodeType == XmlNodeType.EndElement)
			{
				if (this.atomicValue != null)
				{
					originalStringValue = this.originalAtomicValueString;
					return this.atomicValue;
				}
				originalStringValue = string.Empty;
				return string.Empty;
			}
			else
			{
				if (this.validator.CurrentContentType == XmlSchemaContentType.TextOnly)
				{
					object result = this.ReturnBoxedValue(this.ReadTillEndElement(), this.xmlSchemaInfo.XmlType, unwrapTypedValue);
					originalStringValue = this.originalAtomicValueString;
					return result;
				}
				XsdCachingReader xsdCachingReader = this.coreReader as XsdCachingReader;
				if (xsdCachingReader != null)
				{
					originalStringValue = xsdCachingReader.ReadOriginalContentAsString();
				}
				else
				{
					originalStringValue = base.InternalReadContentAsString();
				}
				return originalStringValue;
			}
		}

		// Token: 0x06000EA3 RID: 3747 RVA: 0x0005F10E File Offset: 0x0005D30E
		private object InternalReadElementContentAsObject(out XmlSchemaType xmlType)
		{
			return this.InternalReadElementContentAsObject(out xmlType, false);
		}

		// Token: 0x06000EA4 RID: 3748 RVA: 0x0005F118 File Offset: 0x0005D318
		private object InternalReadElementContentAsObject(out XmlSchemaType xmlType, bool unwrapTypedValue)
		{
			string text;
			return this.InternalReadElementContentAsObject(out xmlType, unwrapTypedValue, out text);
		}

		// Token: 0x06000EA5 RID: 3749 RVA: 0x0005F130 File Offset: 0x0005D330
		private object InternalReadElementContentAsObject(out XmlSchemaType xmlType, bool unwrapTypedValue, out string originalString)
		{
			xmlType = null;
			object result;
			if (this.IsEmptyElement)
			{
				if (this.xmlSchemaInfo.ContentType == XmlSchemaContentType.TextOnly)
				{
					result = this.ReturnBoxedValue(this.atomicValue, this.xmlSchemaInfo.XmlType, unwrapTypedValue);
				}
				else
				{
					result = this.atomicValue;
				}
				originalString = this.originalAtomicValueString;
				xmlType = this.ElementXmlType;
				this.Read();
				return result;
			}
			this.Read();
			if (this.NodeType == XmlNodeType.EndElement)
			{
				if (this.xmlSchemaInfo.IsDefault)
				{
					if (this.xmlSchemaInfo.ContentType == XmlSchemaContentType.TextOnly)
					{
						result = this.ReturnBoxedValue(this.atomicValue, this.xmlSchemaInfo.XmlType, unwrapTypedValue);
					}
					else
					{
						result = this.atomicValue;
					}
					originalString = this.originalAtomicValueString;
				}
				else
				{
					result = string.Empty;
					originalString = string.Empty;
				}
			}
			else
			{
				if (this.NodeType == XmlNodeType.Element)
				{
					throw new XmlException("ReadElementContentAs() methods cannot be called on an element that has child elements.", string.Empty, this);
				}
				result = this.InternalReadContentAsObject(unwrapTypedValue, out originalString);
				if (this.NodeType != XmlNodeType.EndElement)
				{
					throw new XmlException("ReadElementContentAs() methods cannot be called on an element that has child elements.", string.Empty, this);
				}
			}
			xmlType = this.ElementXmlType;
			this.Read();
			return result;
		}

		// Token: 0x06000EA6 RID: 3750 RVA: 0x0005F248 File Offset: 0x0005D448
		private object ReadTillEndElement()
		{
			if (this.atomicValue == null)
			{
				while (this.coreReader.Read())
				{
					if (!this.replayCache)
					{
						switch (this.coreReader.NodeType)
						{
						case XmlNodeType.Element:
							this.ProcessReaderEvent();
							goto IL_10B;
						case XmlNodeType.Text:
						case XmlNodeType.CDATA:
							this.validator.ValidateText(new XmlValueGetter(this.GetStringValue));
							break;
						case XmlNodeType.Whitespace:
						case XmlNodeType.SignificantWhitespace:
							this.validator.ValidateWhitespace(new XmlValueGetter(this.GetStringValue));
							break;
						case XmlNodeType.EndElement:
							this.atomicValue = this.validator.ValidateEndElement(this.xmlSchemaInfo);
							this.originalAtomicValueString = this.GetOriginalAtomicValueStringOfElement();
							if (this.manageNamespaces)
							{
								this.nsManager.PopScope();
								goto IL_10B;
							}
							goto IL_10B;
						}
					}
				}
			}
			else
			{
				if (this.atomicValue == this)
				{
					this.atomicValue = null;
				}
				this.SwitchReader();
			}
			IL_10B:
			return this.atomicValue;
		}

		// Token: 0x06000EA7 RID: 3751 RVA: 0x0005F368 File Offset: 0x0005D568
		private void SwitchReader()
		{
			XsdCachingReader xsdCachingReader = this.coreReader as XsdCachingReader;
			if (xsdCachingReader != null)
			{
				this.coreReader = xsdCachingReader.GetCoreReader();
			}
			this.replayCache = false;
		}

		// Token: 0x06000EA8 RID: 3752 RVA: 0x0005F398 File Offset: 0x0005D598
		private void ReadAheadForMemberType()
		{
			while (this.coreReader.Read())
			{
				switch (this.coreReader.NodeType)
				{
				case XmlNodeType.Text:
				case XmlNodeType.CDATA:
					this.validator.ValidateText(new XmlValueGetter(this.GetStringValue));
					break;
				case XmlNodeType.Whitespace:
				case XmlNodeType.SignificantWhitespace:
					this.validator.ValidateWhitespace(new XmlValueGetter(this.GetStringValue));
					break;
				case XmlNodeType.EndElement:
					this.atomicValue = this.validator.ValidateEndElement(this.xmlSchemaInfo);
					this.originalAtomicValueString = this.GetOriginalAtomicValueStringOfElement();
					if (this.atomicValue == null)
					{
						this.atomicValue = this;
						return;
					}
					if (this.xmlSchemaInfo.IsDefault)
					{
						this.cachingReader.SwitchTextNodeAndEndElement(this.xmlSchemaInfo.XmlType.ValueConverter.ToString(this.atomicValue), this.originalAtomicValueString);
						return;
					}
					return;
				}
			}
		}

		// Token: 0x06000EA9 RID: 3753 RVA: 0x0005F4B4 File Offset: 0x0005D6B4
		private void GetIsDefault()
		{
			if (!(this.coreReader is XsdCachingReader) && this.xmlSchemaInfo.HasDefaultValue)
			{
				this.coreReader = this.GetCachingReader();
				if (this.xmlSchemaInfo.IsUnionType && !this.xmlSchemaInfo.IsNil)
				{
					this.ReadAheadForMemberType();
				}
				else if (this.coreReader.Read())
				{
					switch (this.coreReader.NodeType)
					{
					case XmlNodeType.Text:
					case XmlNodeType.CDATA:
						this.validator.ValidateText(new XmlValueGetter(this.GetStringValue));
						break;
					case XmlNodeType.Whitespace:
					case XmlNodeType.SignificantWhitespace:
						this.validator.ValidateWhitespace(new XmlValueGetter(this.GetStringValue));
						break;
					case XmlNodeType.EndElement:
						this.atomicValue = this.validator.ValidateEndElement(this.xmlSchemaInfo);
						this.originalAtomicValueString = this.GetOriginalAtomicValueStringOfElement();
						if (this.xmlSchemaInfo.IsDefault)
						{
							this.cachingReader.SwitchTextNodeAndEndElement(this.xmlSchemaInfo.XmlType.ValueConverter.ToString(this.atomicValue), this.originalAtomicValueString);
						}
						break;
					}
				}
				this.cachingReader.SetToReplayMode();
				this.replayCache = true;
			}
		}

		// Token: 0x06000EAA RID: 3754 RVA: 0x0005F618 File Offset: 0x0005D818
		private void GetMemberType()
		{
			if (this.xmlSchemaInfo.MemberType != null || this.atomicValue == this)
			{
				return;
			}
			if (!(this.coreReader is XsdCachingReader) && this.xmlSchemaInfo.IsUnionType && !this.xmlSchemaInfo.IsNil)
			{
				this.coreReader = this.GetCachingReader();
				this.ReadAheadForMemberType();
				this.cachingReader.SetToReplayMode();
				this.replayCache = true;
			}
		}

		// Token: 0x06000EAB RID: 3755 RVA: 0x0005F688 File Offset: 0x0005D888
		private object ReturnBoxedValue(object typedValue, XmlSchemaType xmlType, bool unWrap)
		{
			if (typedValue != null)
			{
				if (unWrap && xmlType.Datatype.Variety == XmlSchemaDatatypeVariety.List && (xmlType.Datatype as Datatype_List).ItemType.Variety == XmlSchemaDatatypeVariety.Union)
				{
					typedValue = xmlType.ValueConverter.ChangeType(typedValue, xmlType.Datatype.ValueType, this.thisNSResolver);
				}
				return typedValue;
			}
			typedValue = this.validator.GetConcatenatedValue();
			return typedValue;
		}

		// Token: 0x06000EAC RID: 3756 RVA: 0x0005F6F0 File Offset: 0x0005D8F0
		private XsdCachingReader GetCachingReader()
		{
			if (this.cachingReader == null)
			{
				this.cachingReader = new XsdCachingReader(this.coreReader, this.lineInfo, new CachingEventHandler(this.CachingCallBack));
			}
			else
			{
				this.cachingReader.Reset(this.coreReader);
			}
			this.lineInfo = this.cachingReader;
			return this.cachingReader;
		}

		// Token: 0x06000EAD RID: 3757 RVA: 0x0005F74D File Offset: 0x0005D94D
		internal ValidatingReaderNodeData CreateDummyTextNode(string attributeValue, int depth)
		{
			if (this.textNode == null)
			{
				this.textNode = new ValidatingReaderNodeData(XmlNodeType.Text);
			}
			this.textNode.Depth = depth;
			this.textNode.RawValue = attributeValue;
			return this.textNode;
		}

		// Token: 0x06000EAE RID: 3758 RVA: 0x0005F781 File Offset: 0x0005D981
		internal void CachingCallBack(XsdCachingReader cachingReader)
		{
			this.coreReader = cachingReader.GetCoreReader();
			this.lineInfo = cachingReader.GetLineInfo();
			this.replayCache = false;
		}

		// Token: 0x06000EAF RID: 3759 RVA: 0x0005F7A4 File Offset: 0x0005D9A4
		private string GetOriginalAtomicValueStringOfElement()
		{
			if (!this.xmlSchemaInfo.IsDefault)
			{
				return this.validator.GetConcatenatedValue();
			}
			XmlSchemaElement schemaElement = this.xmlSchemaInfo.SchemaElement;
			if (schemaElement == null)
			{
				return string.Empty;
			}
			if (schemaElement.DefaultValue == null)
			{
				return schemaElement.FixedValue;
			}
			return schemaElement.DefaultValue;
		}

		// Token: 0x06000EB0 RID: 3760 RVA: 0x0005F7F4 File Offset: 0x0005D9F4
		public override Task<string> GetValueAsync()
		{
			if (this.validationState < XsdValidatingReader.ValidatingReaderState.None)
			{
				return Task.FromResult<string>(this.cachedNode.RawValue);
			}
			return this.coreReader.GetValueAsync();
		}

		// Token: 0x06000EB1 RID: 3761 RVA: 0x0005F81B File Offset: 0x0005DA1B
		public override Task<object> ReadContentAsObjectAsync()
		{
			if (!XmlReader.CanReadContentAs(this.NodeType))
			{
				throw base.CreateReadContentAsException("ReadContentAsObject");
			}
			return this.InternalReadContentAsObjectAsync(true);
		}

		// Token: 0x06000EB2 RID: 3762 RVA: 0x0005F840 File Offset: 0x0005DA40
		public override Task<string> ReadContentAsStringAsync()
		{
			XsdValidatingReader.<ReadContentAsStringAsync>d__187 <ReadContentAsStringAsync>d__;
			<ReadContentAsStringAsync>d__.<>4__this = this;
			<ReadContentAsStringAsync>d__.<>t__builder = AsyncTaskMethodBuilder<string>.Create();
			<ReadContentAsStringAsync>d__.<>1__state = -1;
			<ReadContentAsStringAsync>d__.<>t__builder.Start<XsdValidatingReader.<ReadContentAsStringAsync>d__187>(ref <ReadContentAsStringAsync>d__);
			return <ReadContentAsStringAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000EB3 RID: 3763 RVA: 0x0005F884 File Offset: 0x0005DA84
		public override Task<object> ReadContentAsAsync(Type returnType, IXmlNamespaceResolver namespaceResolver)
		{
			XsdValidatingReader.<ReadContentAsAsync>d__188 <ReadContentAsAsync>d__;
			<ReadContentAsAsync>d__.<>4__this = this;
			<ReadContentAsAsync>d__.returnType = returnType;
			<ReadContentAsAsync>d__.namespaceResolver = namespaceResolver;
			<ReadContentAsAsync>d__.<>t__builder = AsyncTaskMethodBuilder<object>.Create();
			<ReadContentAsAsync>d__.<>1__state = -1;
			<ReadContentAsAsync>d__.<>t__builder.Start<XsdValidatingReader.<ReadContentAsAsync>d__188>(ref <ReadContentAsAsync>d__);
			return <ReadContentAsAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000EB4 RID: 3764 RVA: 0x0005F8D8 File Offset: 0x0005DAD8
		public override Task<object> ReadElementContentAsObjectAsync()
		{
			XsdValidatingReader.<ReadElementContentAsObjectAsync>d__189 <ReadElementContentAsObjectAsync>d__;
			<ReadElementContentAsObjectAsync>d__.<>4__this = this;
			<ReadElementContentAsObjectAsync>d__.<>t__builder = AsyncTaskMethodBuilder<object>.Create();
			<ReadElementContentAsObjectAsync>d__.<>1__state = -1;
			<ReadElementContentAsObjectAsync>d__.<>t__builder.Start<XsdValidatingReader.<ReadElementContentAsObjectAsync>d__189>(ref <ReadElementContentAsObjectAsync>d__);
			return <ReadElementContentAsObjectAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000EB5 RID: 3765 RVA: 0x0005F91C File Offset: 0x0005DB1C
		public override Task<string> ReadElementContentAsStringAsync()
		{
			XsdValidatingReader.<ReadElementContentAsStringAsync>d__190 <ReadElementContentAsStringAsync>d__;
			<ReadElementContentAsStringAsync>d__.<>4__this = this;
			<ReadElementContentAsStringAsync>d__.<>t__builder = AsyncTaskMethodBuilder<string>.Create();
			<ReadElementContentAsStringAsync>d__.<>1__state = -1;
			<ReadElementContentAsStringAsync>d__.<>t__builder.Start<XsdValidatingReader.<ReadElementContentAsStringAsync>d__190>(ref <ReadElementContentAsStringAsync>d__);
			return <ReadElementContentAsStringAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000EB6 RID: 3766 RVA: 0x0005F960 File Offset: 0x0005DB60
		public override Task<object> ReadElementContentAsAsync(Type returnType, IXmlNamespaceResolver namespaceResolver)
		{
			XsdValidatingReader.<ReadElementContentAsAsync>d__191 <ReadElementContentAsAsync>d__;
			<ReadElementContentAsAsync>d__.<>4__this = this;
			<ReadElementContentAsAsync>d__.returnType = returnType;
			<ReadElementContentAsAsync>d__.namespaceResolver = namespaceResolver;
			<ReadElementContentAsAsync>d__.<>t__builder = AsyncTaskMethodBuilder<object>.Create();
			<ReadElementContentAsAsync>d__.<>1__state = -1;
			<ReadElementContentAsAsync>d__.<>t__builder.Start<XsdValidatingReader.<ReadElementContentAsAsync>d__191>(ref <ReadElementContentAsAsync>d__);
			return <ReadElementContentAsAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000EB7 RID: 3767 RVA: 0x0005F9B4 File Offset: 0x0005DBB4
		private Task<bool> ReadAsync_Read(Task<bool> task)
		{
			if (!task.IsSuccess())
			{
				return this._ReadAsync_Read(task);
			}
			if (task.Result)
			{
				return this.ProcessReaderEventAsync().ReturnTaskBoolWhenFinish(true);
			}
			this.validator.EndValidation();
			if (this.coreReader.EOF)
			{
				this.validationState = XsdValidatingReader.ValidatingReaderState.EOF;
			}
			return AsyncHelper.DoneTaskFalse;
		}

		// Token: 0x06000EB8 RID: 3768 RVA: 0x0005FA0C File Offset: 0x0005DC0C
		private Task<bool> _ReadAsync_Read(Task<bool> task)
		{
			XsdValidatingReader.<_ReadAsync_Read>d__193 <_ReadAsync_Read>d__;
			<_ReadAsync_Read>d__.<>4__this = this;
			<_ReadAsync_Read>d__.task = task;
			<_ReadAsync_Read>d__.<>t__builder = AsyncTaskMethodBuilder<bool>.Create();
			<_ReadAsync_Read>d__.<>1__state = -1;
			<_ReadAsync_Read>d__.<>t__builder.Start<XsdValidatingReader.<_ReadAsync_Read>d__193>(ref <_ReadAsync_Read>d__);
			return <_ReadAsync_Read>d__.<>t__builder.Task;
		}

		// Token: 0x06000EB9 RID: 3769 RVA: 0x0005FA57 File Offset: 0x0005DC57
		private Task<bool> ReadAsync_ReadAhead(Task task)
		{
			if (task.IsSuccess())
			{
				this.validationState = XsdValidatingReader.ValidatingReaderState.Read;
				return AsyncHelper.DoneTaskTrue;
			}
			return this._ReadAsync_ReadAhead(task);
		}

		// Token: 0x06000EBA RID: 3770 RVA: 0x0005FA78 File Offset: 0x0005DC78
		private Task<bool> _ReadAsync_ReadAhead(Task task)
		{
			XsdValidatingReader.<_ReadAsync_ReadAhead>d__195 <_ReadAsync_ReadAhead>d__;
			<_ReadAsync_ReadAhead>d__.<>4__this = this;
			<_ReadAsync_ReadAhead>d__.task = task;
			<_ReadAsync_ReadAhead>d__.<>t__builder = AsyncTaskMethodBuilder<bool>.Create();
			<_ReadAsync_ReadAhead>d__.<>1__state = -1;
			<_ReadAsync_ReadAhead>d__.<>t__builder.Start<XsdValidatingReader.<_ReadAsync_ReadAhead>d__195>(ref <_ReadAsync_ReadAhead>d__);
			return <_ReadAsync_ReadAhead>d__.<>t__builder.Task;
		}

		// Token: 0x06000EBB RID: 3771 RVA: 0x0005FAC4 File Offset: 0x0005DCC4
		public override Task<bool> ReadAsync()
		{
			switch (this.validationState)
			{
			case XsdValidatingReader.ValidatingReaderState.OnReadAttributeValue:
			case XsdValidatingReader.ValidatingReaderState.OnDefaultAttribute:
			case XsdValidatingReader.ValidatingReaderState.OnAttribute:
			case XsdValidatingReader.ValidatingReaderState.ClearAttributes:
				this.ClearAttributesInfo();
				if (this.inlineSchemaParser != null)
				{
					this.validationState = XsdValidatingReader.ValidatingReaderState.ParseInlineSchema;
					goto IL_59;
				}
				this.validationState = XsdValidatingReader.ValidatingReaderState.Read;
				break;
			case XsdValidatingReader.ValidatingReaderState.None:
				goto IL_F0;
			case XsdValidatingReader.ValidatingReaderState.Init:
				this.validationState = XsdValidatingReader.ValidatingReaderState.Read;
				if (this.coreReader.ReadState == ReadState.Interactive)
				{
					return this.ProcessReaderEventAsync().ReturnTaskBoolWhenFinish(true);
				}
				break;
			case XsdValidatingReader.ValidatingReaderState.Read:
				break;
			case XsdValidatingReader.ValidatingReaderState.ParseInlineSchema:
				goto IL_59;
			case XsdValidatingReader.ValidatingReaderState.ReadAhead:
			{
				this.ClearAttributesInfo();
				Task task = this.ProcessReaderEventAsync();
				return this.ReadAsync_ReadAhead(task);
			}
			case XsdValidatingReader.ValidatingReaderState.OnReadBinaryContent:
				this.validationState = this.savedState;
				return this.readBinaryHelper.FinishAsync().CallBoolTaskFuncWhenFinish(new Func<Task<bool>>(this.ReadAsync));
			case XsdValidatingReader.ValidatingReaderState.ReaderClosed:
			case XsdValidatingReader.ValidatingReaderState.EOF:
				return AsyncHelper.DoneTaskFalse;
			default:
				goto IL_F0;
			}
			Task<bool> task2 = this.coreReader.ReadAsync();
			return this.ReadAsync_Read(task2);
			IL_59:
			return this.ProcessInlineSchemaAsync().ReturnTaskBoolWhenFinish(true);
			IL_F0:
			return AsyncHelper.DoneTaskFalse;
		}

		// Token: 0x06000EBC RID: 3772 RVA: 0x0005FBC8 File Offset: 0x0005DDC8
		public override Task SkipAsync()
		{
			XsdValidatingReader.<SkipAsync>d__197 <SkipAsync>d__;
			<SkipAsync>d__.<>4__this = this;
			<SkipAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<SkipAsync>d__.<>1__state = -1;
			<SkipAsync>d__.<>t__builder.Start<XsdValidatingReader.<SkipAsync>d__197>(ref <SkipAsync>d__);
			return <SkipAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000EBD RID: 3773 RVA: 0x0005FC0C File Offset: 0x0005DE0C
		public override Task<int> ReadContentAsBase64Async(byte[] buffer, int index, int count)
		{
			XsdValidatingReader.<ReadContentAsBase64Async>d__198 <ReadContentAsBase64Async>d__;
			<ReadContentAsBase64Async>d__.<>4__this = this;
			<ReadContentAsBase64Async>d__.buffer = buffer;
			<ReadContentAsBase64Async>d__.index = index;
			<ReadContentAsBase64Async>d__.count = count;
			<ReadContentAsBase64Async>d__.<>t__builder = AsyncTaskMethodBuilder<int>.Create();
			<ReadContentAsBase64Async>d__.<>1__state = -1;
			<ReadContentAsBase64Async>d__.<>t__builder.Start<XsdValidatingReader.<ReadContentAsBase64Async>d__198>(ref <ReadContentAsBase64Async>d__);
			return <ReadContentAsBase64Async>d__.<>t__builder.Task;
		}

		// Token: 0x06000EBE RID: 3774 RVA: 0x0005FC68 File Offset: 0x0005DE68
		public override Task<int> ReadContentAsBinHexAsync(byte[] buffer, int index, int count)
		{
			XsdValidatingReader.<ReadContentAsBinHexAsync>d__199 <ReadContentAsBinHexAsync>d__;
			<ReadContentAsBinHexAsync>d__.<>4__this = this;
			<ReadContentAsBinHexAsync>d__.buffer = buffer;
			<ReadContentAsBinHexAsync>d__.index = index;
			<ReadContentAsBinHexAsync>d__.count = count;
			<ReadContentAsBinHexAsync>d__.<>t__builder = AsyncTaskMethodBuilder<int>.Create();
			<ReadContentAsBinHexAsync>d__.<>1__state = -1;
			<ReadContentAsBinHexAsync>d__.<>t__builder.Start<XsdValidatingReader.<ReadContentAsBinHexAsync>d__199>(ref <ReadContentAsBinHexAsync>d__);
			return <ReadContentAsBinHexAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000EBF RID: 3775 RVA: 0x0005FCC4 File Offset: 0x0005DEC4
		public override Task<int> ReadElementContentAsBase64Async(byte[] buffer, int index, int count)
		{
			XsdValidatingReader.<ReadElementContentAsBase64Async>d__200 <ReadElementContentAsBase64Async>d__;
			<ReadElementContentAsBase64Async>d__.<>4__this = this;
			<ReadElementContentAsBase64Async>d__.buffer = buffer;
			<ReadElementContentAsBase64Async>d__.index = index;
			<ReadElementContentAsBase64Async>d__.count = count;
			<ReadElementContentAsBase64Async>d__.<>t__builder = AsyncTaskMethodBuilder<int>.Create();
			<ReadElementContentAsBase64Async>d__.<>1__state = -1;
			<ReadElementContentAsBase64Async>d__.<>t__builder.Start<XsdValidatingReader.<ReadElementContentAsBase64Async>d__200>(ref <ReadElementContentAsBase64Async>d__);
			return <ReadElementContentAsBase64Async>d__.<>t__builder.Task;
		}

		// Token: 0x06000EC0 RID: 3776 RVA: 0x0005FD20 File Offset: 0x0005DF20
		public override Task<int> ReadElementContentAsBinHexAsync(byte[] buffer, int index, int count)
		{
			XsdValidatingReader.<ReadElementContentAsBinHexAsync>d__201 <ReadElementContentAsBinHexAsync>d__;
			<ReadElementContentAsBinHexAsync>d__.<>4__this = this;
			<ReadElementContentAsBinHexAsync>d__.buffer = buffer;
			<ReadElementContentAsBinHexAsync>d__.index = index;
			<ReadElementContentAsBinHexAsync>d__.count = count;
			<ReadElementContentAsBinHexAsync>d__.<>t__builder = AsyncTaskMethodBuilder<int>.Create();
			<ReadElementContentAsBinHexAsync>d__.<>1__state = -1;
			<ReadElementContentAsBinHexAsync>d__.<>t__builder.Start<XsdValidatingReader.<ReadElementContentAsBinHexAsync>d__201>(ref <ReadElementContentAsBinHexAsync>d__);
			return <ReadElementContentAsBinHexAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000EC1 RID: 3777 RVA: 0x0005FD7C File Offset: 0x0005DF7C
		private Task ProcessReaderEventAsync()
		{
			if (this.replayCache)
			{
				return AsyncHelper.DoneTask;
			}
			switch (this.coreReader.NodeType)
			{
			case XmlNodeType.Element:
				return this.ProcessElementEventAsync();
			case XmlNodeType.Text:
			case XmlNodeType.CDATA:
				this.validator.ValidateText(new XmlValueGetter(this.GetStringValue));
				break;
			case XmlNodeType.EntityReference:
				throw new InvalidOperationException();
			case XmlNodeType.DocumentType:
				this.validator.SetDtdSchemaInfo(this.coreReader.DtdInfo);
				break;
			case XmlNodeType.Whitespace:
			case XmlNodeType.SignificantWhitespace:
				this.validator.ValidateWhitespace(new XmlValueGetter(this.GetStringValue));
				break;
			case XmlNodeType.EndElement:
				return this.ProcessEndElementEventAsync();
			}
			return AsyncHelper.DoneTask;
		}

		// Token: 0x06000EC2 RID: 3778 RVA: 0x0005FE4C File Offset: 0x0005E04C
		private Task ProcessElementEventAsync()
		{
			XsdValidatingReader.<ProcessElementEventAsync>d__203 <ProcessElementEventAsync>d__;
			<ProcessElementEventAsync>d__.<>4__this = this;
			<ProcessElementEventAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<ProcessElementEventAsync>d__.<>1__state = -1;
			<ProcessElementEventAsync>d__.<>t__builder.Start<XsdValidatingReader.<ProcessElementEventAsync>d__203>(ref <ProcessElementEventAsync>d__);
			return <ProcessElementEventAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000EC3 RID: 3779 RVA: 0x0005FE90 File Offset: 0x0005E090
		private Task ProcessEndElementEventAsync()
		{
			XsdValidatingReader.<ProcessEndElementEventAsync>d__204 <ProcessEndElementEventAsync>d__;
			<ProcessEndElementEventAsync>d__.<>4__this = this;
			<ProcessEndElementEventAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<ProcessEndElementEventAsync>d__.<>1__state = -1;
			<ProcessEndElementEventAsync>d__.<>t__builder.Start<XsdValidatingReader.<ProcessEndElementEventAsync>d__204>(ref <ProcessEndElementEventAsync>d__);
			return <ProcessEndElementEventAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000EC4 RID: 3780 RVA: 0x0005FED4 File Offset: 0x0005E0D4
		private Task ProcessInlineSchemaAsync()
		{
			XsdValidatingReader.<ProcessInlineSchemaAsync>d__205 <ProcessInlineSchemaAsync>d__;
			<ProcessInlineSchemaAsync>d__.<>4__this = this;
			<ProcessInlineSchemaAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<ProcessInlineSchemaAsync>d__.<>1__state = -1;
			<ProcessInlineSchemaAsync>d__.<>t__builder.Start<XsdValidatingReader.<ProcessInlineSchemaAsync>d__205>(ref <ProcessInlineSchemaAsync>d__);
			return <ProcessInlineSchemaAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000EC5 RID: 3781 RVA: 0x0005FF17 File Offset: 0x0005E117
		private Task<object> InternalReadContentAsObjectAsync()
		{
			return this.InternalReadContentAsObjectAsync(false);
		}

		// Token: 0x06000EC6 RID: 3782 RVA: 0x0005FF20 File Offset: 0x0005E120
		private Task<object> InternalReadContentAsObjectAsync(bool unwrapTypedValue)
		{
			XsdValidatingReader.<InternalReadContentAsObjectAsync>d__207 <InternalReadContentAsObjectAsync>d__;
			<InternalReadContentAsObjectAsync>d__.<>4__this = this;
			<InternalReadContentAsObjectAsync>d__.unwrapTypedValue = unwrapTypedValue;
			<InternalReadContentAsObjectAsync>d__.<>t__builder = AsyncTaskMethodBuilder<object>.Create();
			<InternalReadContentAsObjectAsync>d__.<>1__state = -1;
			<InternalReadContentAsObjectAsync>d__.<>t__builder.Start<XsdValidatingReader.<InternalReadContentAsObjectAsync>d__207>(ref <InternalReadContentAsObjectAsync>d__);
			return <InternalReadContentAsObjectAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000EC7 RID: 3783 RVA: 0x0005FF6C File Offset: 0x0005E16C
		private Task<Tuple<string, object>> InternalReadContentAsObjectTupleAsync(bool unwrapTypedValue)
		{
			XsdValidatingReader.<InternalReadContentAsObjectTupleAsync>d__208 <InternalReadContentAsObjectTupleAsync>d__;
			<InternalReadContentAsObjectTupleAsync>d__.<>4__this = this;
			<InternalReadContentAsObjectTupleAsync>d__.unwrapTypedValue = unwrapTypedValue;
			<InternalReadContentAsObjectTupleAsync>d__.<>t__builder = AsyncTaskMethodBuilder<Tuple<string, object>>.Create();
			<InternalReadContentAsObjectTupleAsync>d__.<>1__state = -1;
			<InternalReadContentAsObjectTupleAsync>d__.<>t__builder.Start<XsdValidatingReader.<InternalReadContentAsObjectTupleAsync>d__208>(ref <InternalReadContentAsObjectTupleAsync>d__);
			return <InternalReadContentAsObjectTupleAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000EC8 RID: 3784 RVA: 0x0005FFB7 File Offset: 0x0005E1B7
		private Task<Tuple<XmlSchemaType, object>> InternalReadElementContentAsObjectAsync()
		{
			return this.InternalReadElementContentAsObjectAsync(false);
		}

		// Token: 0x06000EC9 RID: 3785 RVA: 0x0005FFC0 File Offset: 0x0005E1C0
		private Task<Tuple<XmlSchemaType, object>> InternalReadElementContentAsObjectAsync(bool unwrapTypedValue)
		{
			XsdValidatingReader.<InternalReadElementContentAsObjectAsync>d__210 <InternalReadElementContentAsObjectAsync>d__;
			<InternalReadElementContentAsObjectAsync>d__.<>4__this = this;
			<InternalReadElementContentAsObjectAsync>d__.unwrapTypedValue = unwrapTypedValue;
			<InternalReadElementContentAsObjectAsync>d__.<>t__builder = AsyncTaskMethodBuilder<Tuple<XmlSchemaType, object>>.Create();
			<InternalReadElementContentAsObjectAsync>d__.<>1__state = -1;
			<InternalReadElementContentAsObjectAsync>d__.<>t__builder.Start<XsdValidatingReader.<InternalReadElementContentAsObjectAsync>d__210>(ref <InternalReadElementContentAsObjectAsync>d__);
			return <InternalReadElementContentAsObjectAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000ECA RID: 3786 RVA: 0x0006000C File Offset: 0x0005E20C
		private Task<Tuple<XmlSchemaType, string, object>> InternalReadElementContentAsObjectTupleAsync(bool unwrapTypedValue)
		{
			XsdValidatingReader.<InternalReadElementContentAsObjectTupleAsync>d__211 <InternalReadElementContentAsObjectTupleAsync>d__;
			<InternalReadElementContentAsObjectTupleAsync>d__.<>4__this = this;
			<InternalReadElementContentAsObjectTupleAsync>d__.unwrapTypedValue = unwrapTypedValue;
			<InternalReadElementContentAsObjectTupleAsync>d__.<>t__builder = AsyncTaskMethodBuilder<Tuple<XmlSchemaType, string, object>>.Create();
			<InternalReadElementContentAsObjectTupleAsync>d__.<>1__state = -1;
			<InternalReadElementContentAsObjectTupleAsync>d__.<>t__builder.Start<XsdValidatingReader.<InternalReadElementContentAsObjectTupleAsync>d__211>(ref <InternalReadElementContentAsObjectTupleAsync>d__);
			return <InternalReadElementContentAsObjectTupleAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000ECB RID: 3787 RVA: 0x00060058 File Offset: 0x0005E258
		private Task<object> ReadTillEndElementAsync()
		{
			XsdValidatingReader.<ReadTillEndElementAsync>d__212 <ReadTillEndElementAsync>d__;
			<ReadTillEndElementAsync>d__.<>4__this = this;
			<ReadTillEndElementAsync>d__.<>t__builder = AsyncTaskMethodBuilder<object>.Create();
			<ReadTillEndElementAsync>d__.<>1__state = -1;
			<ReadTillEndElementAsync>d__.<>t__builder.Start<XsdValidatingReader.<ReadTillEndElementAsync>d__212>(ref <ReadTillEndElementAsync>d__);
			return <ReadTillEndElementAsync>d__.<>t__builder.Task;
		}

		// Token: 0x04000F36 RID: 3894
		private XmlReader coreReader;

		// Token: 0x04000F37 RID: 3895
		private IXmlNamespaceResolver coreReaderNSResolver;

		// Token: 0x04000F38 RID: 3896
		private IXmlNamespaceResolver thisNSResolver;

		// Token: 0x04000F39 RID: 3897
		private XmlSchemaValidator validator;

		// Token: 0x04000F3A RID: 3898
		private XmlResolver xmlResolver;

		// Token: 0x04000F3B RID: 3899
		private ValidationEventHandler validationEvent;

		// Token: 0x04000F3C RID: 3900
		private XsdValidatingReader.ValidatingReaderState validationState;

		// Token: 0x04000F3D RID: 3901
		private XmlValueGetter valueGetter;

		// Token: 0x04000F3E RID: 3902
		private XmlNamespaceManager nsManager;

		// Token: 0x04000F3F RID: 3903
		private bool manageNamespaces;

		// Token: 0x04000F40 RID: 3904
		private bool processInlineSchema;

		// Token: 0x04000F41 RID: 3905
		private bool replayCache;

		// Token: 0x04000F42 RID: 3906
		private ValidatingReaderNodeData cachedNode;

		// Token: 0x04000F43 RID: 3907
		private AttributePSVIInfo attributePSVI;

		// Token: 0x04000F44 RID: 3908
		private int attributeCount;

		// Token: 0x04000F45 RID: 3909
		private int coreReaderAttributeCount;

		// Token: 0x04000F46 RID: 3910
		private int currentAttrIndex;

		// Token: 0x04000F47 RID: 3911
		private AttributePSVIInfo[] attributePSVINodes;

		// Token: 0x04000F48 RID: 3912
		private ArrayList defaultAttributes;

		// Token: 0x04000F49 RID: 3913
		private Parser inlineSchemaParser;

		// Token: 0x04000F4A RID: 3914
		private object atomicValue;

		// Token: 0x04000F4B RID: 3915
		private XmlSchemaInfo xmlSchemaInfo;

		// Token: 0x04000F4C RID: 3916
		private string originalAtomicValueString;

		// Token: 0x04000F4D RID: 3917
		private XmlNameTable coreReaderNameTable;

		// Token: 0x04000F4E RID: 3918
		private XsdCachingReader cachingReader;

		// Token: 0x04000F4F RID: 3919
		private ValidatingReaderNodeData textNode;

		// Token: 0x04000F50 RID: 3920
		private string NsXmlNs;

		// Token: 0x04000F51 RID: 3921
		private string NsXs;

		// Token: 0x04000F52 RID: 3922
		private string NsXsi;

		// Token: 0x04000F53 RID: 3923
		private string XsiType;

		// Token: 0x04000F54 RID: 3924
		private string XsiNil;

		// Token: 0x04000F55 RID: 3925
		private string XsdSchema;

		// Token: 0x04000F56 RID: 3926
		private string XsiSchemaLocation;

		// Token: 0x04000F57 RID: 3927
		private string XsiNoNamespaceSchemaLocation;

		// Token: 0x04000F58 RID: 3928
		private XmlCharType xmlCharType = XmlCharType.Instance;

		// Token: 0x04000F59 RID: 3929
		private IXmlLineInfo lineInfo;

		// Token: 0x04000F5A RID: 3930
		private ReadContentAsBinaryHelper readBinaryHelper;

		// Token: 0x04000F5B RID: 3931
		private XsdValidatingReader.ValidatingReaderState savedState;

		// Token: 0x04000F5C RID: 3932
		private const int InitialAttributeCount = 8;

		// Token: 0x04000F5D RID: 3933
		private static volatile Type TypeOfString;

		// Token: 0x0200018D RID: 397
		private enum ValidatingReaderState
		{
			// Token: 0x04000F5F RID: 3935
			None,
			// Token: 0x04000F60 RID: 3936
			Init,
			// Token: 0x04000F61 RID: 3937
			Read,
			// Token: 0x04000F62 RID: 3938
			OnDefaultAttribute = -1,
			// Token: 0x04000F63 RID: 3939
			OnReadAttributeValue = -2,
			// Token: 0x04000F64 RID: 3940
			OnAttribute = 3,
			// Token: 0x04000F65 RID: 3941
			ClearAttributes,
			// Token: 0x04000F66 RID: 3942
			ParseInlineSchema,
			// Token: 0x04000F67 RID: 3943
			ReadAhead,
			// Token: 0x04000F68 RID: 3944
			OnReadBinaryContent,
			// Token: 0x04000F69 RID: 3945
			ReaderClosed,
			// Token: 0x04000F6A RID: 3946
			EOF,
			// Token: 0x04000F6B RID: 3947
			Error
		}

		// Token: 0x0200018E RID: 398
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <ReadContentAsStringAsync>d__187 : IAsyncStateMachine
		{
			// Token: 0x06000ECC RID: 3788 RVA: 0x0006009C File Offset: 0x0005E29C
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				XsdValidatingReader xsdValidatingReader = this.<>4__this;
				string result2;
				try
				{
					ConfiguredTaskAwaitable<object>.ConfiguredTaskAwaiter awaiter;
					if (num != 0)
					{
						if (!XmlReader.CanReadContentAs(xsdValidatingReader.NodeType))
						{
							throw xsdValidatingReader.CreateReadContentAsException("ReadContentAsString");
						}
						awaiter = xsdValidatingReader.InternalReadContentAsObjectAsync().ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 0;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<object>.ConfiguredTaskAwaiter, XsdValidatingReader.<ReadContentAsStringAsync>d__187>(ref awaiter, ref this);
							return;
						}
					}
					else
					{
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable<object>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
					}
					object result = awaiter.GetResult();
					XmlSchemaType xmlSchemaType = (xsdValidatingReader.NodeType == XmlNodeType.Attribute) ? xsdValidatingReader.AttributeXmlType : xsdValidatingReader.ElementXmlType;
					try
					{
						if (xmlSchemaType != null)
						{
							result2 = xmlSchemaType.ValueConverter.ToString(result);
						}
						else
						{
							result2 = (result as string);
						}
					}
					catch (InvalidCastException innerException)
					{
						throw new XmlException("Content cannot be converted to the type {0}.", "String", innerException, xsdValidatingReader);
					}
					catch (FormatException innerException2)
					{
						throw new XmlException("Content cannot be converted to the type {0}.", "String", innerException2, xsdValidatingReader);
					}
					catch (OverflowException innerException3)
					{
						throw new XmlException("Content cannot be converted to the type {0}.", "String", innerException3, xsdValidatingReader);
					}
				}
				catch (Exception exception)
				{
					this.<>1__state = -2;
					this.<>t__builder.SetException(exception);
					return;
				}
				this.<>1__state = -2;
				this.<>t__builder.SetResult(result2);
			}

			// Token: 0x06000ECD RID: 3789 RVA: 0x00060214 File Offset: 0x0005E414
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04000F6C RID: 3948
			public int <>1__state;

			// Token: 0x04000F6D RID: 3949
			public AsyncTaskMethodBuilder<string> <>t__builder;

			// Token: 0x04000F6E RID: 3950
			public XsdValidatingReader <>4__this;

			// Token: 0x04000F6F RID: 3951
			private ConfiguredTaskAwaitable<object>.ConfiguredTaskAwaiter <>u__1;
		}

		// Token: 0x0200018F RID: 399
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <ReadContentAsAsync>d__188 : IAsyncStateMachine
		{
			// Token: 0x06000ECE RID: 3790 RVA: 0x00060224 File Offset: 0x0005E424
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				XsdValidatingReader xsdValidatingReader = this.<>4__this;
				object result2;
				try
				{
					ConfiguredTaskAwaitable<Tuple<string, object>>.ConfiguredTaskAwaiter awaiter;
					if (num != 0)
					{
						if (!XmlReader.CanReadContentAs(xsdValidatingReader.NodeType))
						{
							throw xsdValidatingReader.CreateReadContentAsException("ReadContentAs");
						}
						awaiter = xsdValidatingReader.InternalReadContentAsObjectTupleAsync(false).ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 0;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<Tuple<string, object>>.ConfiguredTaskAwaiter, XsdValidatingReader.<ReadContentAsAsync>d__188>(ref awaiter, ref this);
							return;
						}
					}
					else
					{
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable<Tuple<string, object>>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
					}
					Tuple<string, object> result = awaiter.GetResult();
					string item = result.Item1;
					object value = result.Item2;
					XmlSchemaType xmlSchemaType = (xsdValidatingReader.NodeType == XmlNodeType.Attribute) ? xsdValidatingReader.AttributeXmlType : xsdValidatingReader.ElementXmlType;
					try
					{
						if (xmlSchemaType != null)
						{
							if (this.returnType == typeof(DateTimeOffset) && xmlSchemaType.Datatype is Datatype_dateTimeBase)
							{
								value = item;
							}
							result2 = xmlSchemaType.ValueConverter.ChangeType(value, this.returnType);
						}
						else
						{
							result2 = XmlUntypedConverter.Untyped.ChangeType(value, this.returnType, this.namespaceResolver);
						}
					}
					catch (FormatException innerException)
					{
						throw new XmlException("Content cannot be converted to the type {0}.", this.returnType.ToString(), innerException, xsdValidatingReader);
					}
					catch (InvalidCastException innerException2)
					{
						throw new XmlException("Content cannot be converted to the type {0}.", this.returnType.ToString(), innerException2, xsdValidatingReader);
					}
					catch (OverflowException innerException3)
					{
						throw new XmlException("Content cannot be converted to the type {0}.", this.returnType.ToString(), innerException3, xsdValidatingReader);
					}
				}
				catch (Exception exception)
				{
					this.<>1__state = -2;
					this.<>t__builder.SetException(exception);
					return;
				}
				this.<>1__state = -2;
				this.<>t__builder.SetResult(result2);
			}

			// Token: 0x06000ECF RID: 3791 RVA: 0x00060430 File Offset: 0x0005E630
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04000F70 RID: 3952
			public int <>1__state;

			// Token: 0x04000F71 RID: 3953
			public AsyncTaskMethodBuilder<object> <>t__builder;

			// Token: 0x04000F72 RID: 3954
			public XsdValidatingReader <>4__this;

			// Token: 0x04000F73 RID: 3955
			public Type returnType;

			// Token: 0x04000F74 RID: 3956
			public IXmlNamespaceResolver namespaceResolver;

			// Token: 0x04000F75 RID: 3957
			private ConfiguredTaskAwaitable<Tuple<string, object>>.ConfiguredTaskAwaiter <>u__1;
		}

		// Token: 0x02000190 RID: 400
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <ReadElementContentAsObjectAsync>d__189 : IAsyncStateMachine
		{
			// Token: 0x06000ED0 RID: 3792 RVA: 0x00060440 File Offset: 0x0005E640
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				XsdValidatingReader xsdValidatingReader = this.<>4__this;
				object item;
				try
				{
					ConfiguredTaskAwaitable<Tuple<XmlSchemaType, object>>.ConfiguredTaskAwaiter awaiter;
					if (num != 0)
					{
						if (xsdValidatingReader.NodeType != XmlNodeType.Element)
						{
							throw xsdValidatingReader.CreateReadElementContentAsException("ReadElementContentAsObject");
						}
						awaiter = xsdValidatingReader.InternalReadElementContentAsObjectAsync(true).ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 0;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<Tuple<XmlSchemaType, object>>.ConfiguredTaskAwaiter, XsdValidatingReader.<ReadElementContentAsObjectAsync>d__189>(ref awaiter, ref this);
							return;
						}
					}
					else
					{
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable<Tuple<XmlSchemaType, object>>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
					}
					item = awaiter.GetResult().Item2;
				}
				catch (Exception exception)
				{
					this.<>1__state = -2;
					this.<>t__builder.SetException(exception);
					return;
				}
				this.<>1__state = -2;
				this.<>t__builder.SetResult(item);
			}

			// Token: 0x06000ED1 RID: 3793 RVA: 0x0006051C File Offset: 0x0005E71C
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04000F76 RID: 3958
			public int <>1__state;

			// Token: 0x04000F77 RID: 3959
			public AsyncTaskMethodBuilder<object> <>t__builder;

			// Token: 0x04000F78 RID: 3960
			public XsdValidatingReader <>4__this;

			// Token: 0x04000F79 RID: 3961
			private ConfiguredTaskAwaitable<Tuple<XmlSchemaType, object>>.ConfiguredTaskAwaiter <>u__1;
		}

		// Token: 0x02000191 RID: 401
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <ReadElementContentAsStringAsync>d__190 : IAsyncStateMachine
		{
			// Token: 0x06000ED2 RID: 3794 RVA: 0x0006052C File Offset: 0x0005E72C
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				XsdValidatingReader xsdValidatingReader = this.<>4__this;
				string result2;
				try
				{
					ConfiguredTaskAwaitable<Tuple<XmlSchemaType, object>>.ConfiguredTaskAwaiter awaiter;
					if (num != 0)
					{
						if (xsdValidatingReader.NodeType != XmlNodeType.Element)
						{
							throw xsdValidatingReader.CreateReadElementContentAsException("ReadElementContentAsString");
						}
						awaiter = xsdValidatingReader.InternalReadElementContentAsObjectAsync().ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 0;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<Tuple<XmlSchemaType, object>>.ConfiguredTaskAwaiter, XsdValidatingReader.<ReadElementContentAsStringAsync>d__190>(ref awaiter, ref this);
							return;
						}
					}
					else
					{
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable<Tuple<XmlSchemaType, object>>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
					}
					Tuple<XmlSchemaType, object> result = awaiter.GetResult();
					XmlSchemaType item = result.Item1;
					object item2 = result.Item2;
					try
					{
						if (item != null)
						{
							result2 = item.ValueConverter.ToString(item2);
						}
						else
						{
							result2 = (item2 as string);
						}
					}
					catch (InvalidCastException innerException)
					{
						throw new XmlException("Content cannot be converted to the type {0}.", "String", innerException, xsdValidatingReader);
					}
					catch (FormatException innerException2)
					{
						throw new XmlException("Content cannot be converted to the type {0}.", "String", innerException2, xsdValidatingReader);
					}
					catch (OverflowException innerException3)
					{
						throw new XmlException("Content cannot be converted to the type {0}.", "String", innerException3, xsdValidatingReader);
					}
				}
				catch (Exception exception)
				{
					this.<>1__state = -2;
					this.<>t__builder.SetException(exception);
					return;
				}
				this.<>1__state = -2;
				this.<>t__builder.SetResult(result2);
			}

			// Token: 0x06000ED3 RID: 3795 RVA: 0x00060694 File Offset: 0x0005E894
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04000F7A RID: 3962
			public int <>1__state;

			// Token: 0x04000F7B RID: 3963
			public AsyncTaskMethodBuilder<string> <>t__builder;

			// Token: 0x04000F7C RID: 3964
			public XsdValidatingReader <>4__this;

			// Token: 0x04000F7D RID: 3965
			private ConfiguredTaskAwaitable<Tuple<XmlSchemaType, object>>.ConfiguredTaskAwaiter <>u__1;
		}

		// Token: 0x02000192 RID: 402
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <ReadElementContentAsAsync>d__191 : IAsyncStateMachine
		{
			// Token: 0x06000ED4 RID: 3796 RVA: 0x000606A4 File Offset: 0x0005E8A4
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				XsdValidatingReader xsdValidatingReader = this.<>4__this;
				object result2;
				try
				{
					ConfiguredTaskAwaitable<Tuple<XmlSchemaType, string, object>>.ConfiguredTaskAwaiter awaiter;
					if (num != 0)
					{
						if (xsdValidatingReader.NodeType != XmlNodeType.Element)
						{
							throw xsdValidatingReader.CreateReadElementContentAsException("ReadElementContentAs");
						}
						awaiter = xsdValidatingReader.InternalReadElementContentAsObjectTupleAsync(false).ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 0;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<Tuple<XmlSchemaType, string, object>>.ConfiguredTaskAwaiter, XsdValidatingReader.<ReadElementContentAsAsync>d__191>(ref awaiter, ref this);
							return;
						}
					}
					else
					{
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable<Tuple<XmlSchemaType, string, object>>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
					}
					Tuple<XmlSchemaType, string, object> result = awaiter.GetResult();
					XmlSchemaType item = result.Item1;
					string item2 = result.Item2;
					object value = result.Item3;
					try
					{
						if (item != null)
						{
							if (this.returnType == typeof(DateTimeOffset) && item.Datatype is Datatype_dateTimeBase)
							{
								value = item2;
							}
							result2 = item.ValueConverter.ChangeType(value, this.returnType, this.namespaceResolver);
						}
						else
						{
							result2 = XmlUntypedConverter.Untyped.ChangeType(value, this.returnType, this.namespaceResolver);
						}
					}
					catch (FormatException innerException)
					{
						throw new XmlException("Content cannot be converted to the type {0}.", this.returnType.ToString(), innerException, xsdValidatingReader);
					}
					catch (InvalidCastException innerException2)
					{
						throw new XmlException("Content cannot be converted to the type {0}.", this.returnType.ToString(), innerException2, xsdValidatingReader);
					}
					catch (OverflowException innerException3)
					{
						throw new XmlException("Content cannot be converted to the type {0}.", this.returnType.ToString(), innerException3, xsdValidatingReader);
					}
				}
				catch (Exception exception)
				{
					this.<>1__state = -2;
					this.<>t__builder.SetException(exception);
					return;
				}
				this.<>1__state = -2;
				this.<>t__builder.SetResult(result2);
			}

			// Token: 0x06000ED5 RID: 3797 RVA: 0x000608A0 File Offset: 0x0005EAA0
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04000F7E RID: 3966
			public int <>1__state;

			// Token: 0x04000F7F RID: 3967
			public AsyncTaskMethodBuilder<object> <>t__builder;

			// Token: 0x04000F80 RID: 3968
			public XsdValidatingReader <>4__this;

			// Token: 0x04000F81 RID: 3969
			public Type returnType;

			// Token: 0x04000F82 RID: 3970
			public IXmlNamespaceResolver namespaceResolver;

			// Token: 0x04000F83 RID: 3971
			private ConfiguredTaskAwaitable<Tuple<XmlSchemaType, string, object>>.ConfiguredTaskAwaiter <>u__1;
		}

		// Token: 0x02000193 RID: 403
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <_ReadAsync_Read>d__193 : IAsyncStateMachine
		{
			// Token: 0x06000ED6 RID: 3798 RVA: 0x000608B0 File Offset: 0x0005EAB0
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				XsdValidatingReader xsdValidatingReader = this.<>4__this;
				bool result;
				try
				{
					ConfiguredTaskAwaitable.ConfiguredTaskAwaiter awaiter;
					ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter awaiter2;
					if (num != 0)
					{
						if (num == 1)
						{
							awaiter = this.<>u__2;
							this.<>u__2 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
							this.<>1__state = -1;
							goto IL_DD;
						}
						awaiter2 = this.task.ConfigureAwait(false).GetAwaiter();
						if (!awaiter2.IsCompleted)
						{
							this.<>1__state = 0;
							this.<>u__1 = awaiter2;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter, XsdValidatingReader.<_ReadAsync_Read>d__193>(ref awaiter2, ref this);
							return;
						}
					}
					else
					{
						awaiter2 = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
					}
					if (!awaiter2.GetResult())
					{
						xsdValidatingReader.validator.EndValidation();
						if (xsdValidatingReader.coreReader.EOF)
						{
							xsdValidatingReader.validationState = XsdValidatingReader.ValidatingReaderState.EOF;
						}
						result = false;
						goto IL_125;
					}
					awaiter = xsdValidatingReader.ProcessReaderEventAsync().ConfigureAwait(false).GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						this.<>1__state = 1;
						this.<>u__2 = awaiter;
						this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XsdValidatingReader.<_ReadAsync_Read>d__193>(ref awaiter, ref this);
						return;
					}
					IL_DD:
					awaiter.GetResult();
					result = true;
				}
				catch (Exception exception)
				{
					this.<>1__state = -2;
					this.<>t__builder.SetException(exception);
					return;
				}
				IL_125:
				this.<>1__state = -2;
				this.<>t__builder.SetResult(result);
			}

			// Token: 0x06000ED7 RID: 3799 RVA: 0x00060A08 File Offset: 0x0005EC08
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04000F84 RID: 3972
			public int <>1__state;

			// Token: 0x04000F85 RID: 3973
			public AsyncTaskMethodBuilder<bool> <>t__builder;

			// Token: 0x04000F86 RID: 3974
			public Task<bool> task;

			// Token: 0x04000F87 RID: 3975
			public XsdValidatingReader <>4__this;

			// Token: 0x04000F88 RID: 3976
			private ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter <>u__1;

			// Token: 0x04000F89 RID: 3977
			private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter <>u__2;
		}

		// Token: 0x02000194 RID: 404
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <_ReadAsync_ReadAhead>d__195 : IAsyncStateMachine
		{
			// Token: 0x06000ED8 RID: 3800 RVA: 0x00060A18 File Offset: 0x0005EC18
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				XsdValidatingReader xsdValidatingReader = this.<>4__this;
				bool result;
				try
				{
					ConfiguredTaskAwaitable.ConfiguredTaskAwaiter awaiter;
					if (num != 0)
					{
						awaiter = this.task.ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 0;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XsdValidatingReader.<_ReadAsync_ReadAhead>d__195>(ref awaiter, ref this);
							return;
						}
					}
					else
					{
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
					}
					awaiter.GetResult();
					xsdValidatingReader.validationState = XsdValidatingReader.ValidatingReaderState.Read;
					result = true;
				}
				catch (Exception exception)
				{
					this.<>1__state = -2;
					this.<>t__builder.SetException(exception);
					return;
				}
				this.<>1__state = -2;
				this.<>t__builder.SetResult(result);
			}

			// Token: 0x06000ED9 RID: 3801 RVA: 0x00060AE0 File Offset: 0x0005ECE0
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04000F8A RID: 3978
			public int <>1__state;

			// Token: 0x04000F8B RID: 3979
			public AsyncTaskMethodBuilder<bool> <>t__builder;

			// Token: 0x04000F8C RID: 3980
			public Task task;

			// Token: 0x04000F8D RID: 3981
			public XsdValidatingReader <>4__this;

			// Token: 0x04000F8E RID: 3982
			private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter <>u__1;
		}

		// Token: 0x02000195 RID: 405
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <SkipAsync>d__197 : IAsyncStateMachine
		{
			// Token: 0x06000EDA RID: 3802 RVA: 0x00060AF0 File Offset: 0x0005ECF0
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				XsdValidatingReader xsdValidatingReader = this.<>4__this;
				try
				{
					ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter awaiter;
					ConfiguredTaskAwaitable.ConfiguredTaskAwaiter awaiter2;
					if (num != 0)
					{
						if (num == 1)
						{
							awaiter = this.<>u__2;
							this.<>u__2 = default(ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter);
							this.<>1__state = -1;
							goto IL_174;
						}
						int depth = xsdValidatingReader.Depth;
						XmlNodeType nodeType = xsdValidatingReader.NodeType;
						if (nodeType != XmlNodeType.Element)
						{
							if (nodeType != XmlNodeType.Attribute)
							{
								goto IL_116;
							}
							xsdValidatingReader.MoveToElement();
						}
						if (xsdValidatingReader.coreReader.IsEmptyElement)
						{
							goto IL_116;
						}
						this.<callSkipToEndElem>5__2 = true;
						if ((xsdValidatingReader.xmlSchemaInfo.IsUnionType || xsdValidatingReader.xmlSchemaInfo.IsDefault) && xsdValidatingReader.coreReader is XsdCachingReader)
						{
							this.<callSkipToEndElem>5__2 = false;
						}
						awaiter2 = xsdValidatingReader.coreReader.SkipAsync().ConfigureAwait(false).GetAwaiter();
						if (!awaiter2.IsCompleted)
						{
							this.<>1__state = 0;
							this.<>u__1 = awaiter2;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XsdValidatingReader.<SkipAsync>d__197>(ref awaiter2, ref this);
							return;
						}
					}
					else
					{
						awaiter2 = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
					}
					awaiter2.GetResult();
					xsdValidatingReader.validationState = XsdValidatingReader.ValidatingReaderState.ReadAhead;
					if (this.<callSkipToEndElem>5__2)
					{
						xsdValidatingReader.validator.SkipToEndElement(xsdValidatingReader.xmlSchemaInfo);
					}
					IL_116:
					awaiter = xsdValidatingReader.ReadAsync().ConfigureAwait(false).GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						this.<>1__state = 1;
						this.<>u__2 = awaiter;
						this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter, XsdValidatingReader.<SkipAsync>d__197>(ref awaiter, ref this);
						return;
					}
					IL_174:
					awaiter.GetResult();
				}
				catch (Exception exception)
				{
					this.<>1__state = -2;
					this.<>t__builder.SetException(exception);
					return;
				}
				this.<>1__state = -2;
				this.<>t__builder.SetResult();
			}

			// Token: 0x06000EDB RID: 3803 RVA: 0x00060CC4 File Offset: 0x0005EEC4
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04000F8F RID: 3983
			public int <>1__state;

			// Token: 0x04000F90 RID: 3984
			public AsyncTaskMethodBuilder <>t__builder;

			// Token: 0x04000F91 RID: 3985
			public XsdValidatingReader <>4__this;

			// Token: 0x04000F92 RID: 3986
			private bool <callSkipToEndElem>5__2;

			// Token: 0x04000F93 RID: 3987
			private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter <>u__1;

			// Token: 0x04000F94 RID: 3988
			private ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter <>u__2;
		}

		// Token: 0x02000196 RID: 406
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <ReadContentAsBase64Async>d__198 : IAsyncStateMachine
		{
			// Token: 0x06000EDC RID: 3804 RVA: 0x00060CD4 File Offset: 0x0005EED4
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				XsdValidatingReader xsdValidatingReader = this.<>4__this;
				int result;
				try
				{
					ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter awaiter;
					if (num != 0)
					{
						if (xsdValidatingReader.ReadState != ReadState.Interactive)
						{
							result = 0;
							goto IL_FF;
						}
						if (xsdValidatingReader.validationState != XsdValidatingReader.ValidatingReaderState.OnReadBinaryContent)
						{
							xsdValidatingReader.readBinaryHelper = ReadContentAsBinaryHelper.CreateOrReset(xsdValidatingReader.readBinaryHelper, xsdValidatingReader);
							xsdValidatingReader.savedState = xsdValidatingReader.validationState;
						}
						xsdValidatingReader.validationState = xsdValidatingReader.savedState;
						awaiter = xsdValidatingReader.readBinaryHelper.ReadContentAsBase64Async(this.buffer, this.index, this.count).ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 0;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter, XsdValidatingReader.<ReadContentAsBase64Async>d__198>(ref awaiter, ref this);
							return;
						}
					}
					else
					{
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
					}
					int result2 = awaiter.GetResult();
					xsdValidatingReader.savedState = xsdValidatingReader.validationState;
					xsdValidatingReader.validationState = XsdValidatingReader.ValidatingReaderState.OnReadBinaryContent;
					result = result2;
				}
				catch (Exception exception)
				{
					this.<>1__state = -2;
					this.<>t__builder.SetException(exception);
					return;
				}
				IL_FF:
				this.<>1__state = -2;
				this.<>t__builder.SetResult(result);
			}

			// Token: 0x06000EDD RID: 3805 RVA: 0x00060E04 File Offset: 0x0005F004
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04000F95 RID: 3989
			public int <>1__state;

			// Token: 0x04000F96 RID: 3990
			public AsyncTaskMethodBuilder<int> <>t__builder;

			// Token: 0x04000F97 RID: 3991
			public XsdValidatingReader <>4__this;

			// Token: 0x04000F98 RID: 3992
			public byte[] buffer;

			// Token: 0x04000F99 RID: 3993
			public int index;

			// Token: 0x04000F9A RID: 3994
			public int count;

			// Token: 0x04000F9B RID: 3995
			private ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter <>u__1;
		}

		// Token: 0x02000197 RID: 407
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <ReadContentAsBinHexAsync>d__199 : IAsyncStateMachine
		{
			// Token: 0x06000EDE RID: 3806 RVA: 0x00060E14 File Offset: 0x0005F014
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				XsdValidatingReader xsdValidatingReader = this.<>4__this;
				int result;
				try
				{
					ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter awaiter;
					if (num != 0)
					{
						if (xsdValidatingReader.ReadState != ReadState.Interactive)
						{
							result = 0;
							goto IL_FF;
						}
						if (xsdValidatingReader.validationState != XsdValidatingReader.ValidatingReaderState.OnReadBinaryContent)
						{
							xsdValidatingReader.readBinaryHelper = ReadContentAsBinaryHelper.CreateOrReset(xsdValidatingReader.readBinaryHelper, xsdValidatingReader);
							xsdValidatingReader.savedState = xsdValidatingReader.validationState;
						}
						xsdValidatingReader.validationState = xsdValidatingReader.savedState;
						awaiter = xsdValidatingReader.readBinaryHelper.ReadContentAsBinHexAsync(this.buffer, this.index, this.count).ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 0;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter, XsdValidatingReader.<ReadContentAsBinHexAsync>d__199>(ref awaiter, ref this);
							return;
						}
					}
					else
					{
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
					}
					int result2 = awaiter.GetResult();
					xsdValidatingReader.savedState = xsdValidatingReader.validationState;
					xsdValidatingReader.validationState = XsdValidatingReader.ValidatingReaderState.OnReadBinaryContent;
					result = result2;
				}
				catch (Exception exception)
				{
					this.<>1__state = -2;
					this.<>t__builder.SetException(exception);
					return;
				}
				IL_FF:
				this.<>1__state = -2;
				this.<>t__builder.SetResult(result);
			}

			// Token: 0x06000EDF RID: 3807 RVA: 0x00060F44 File Offset: 0x0005F144
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04000F9C RID: 3996
			public int <>1__state;

			// Token: 0x04000F9D RID: 3997
			public AsyncTaskMethodBuilder<int> <>t__builder;

			// Token: 0x04000F9E RID: 3998
			public XsdValidatingReader <>4__this;

			// Token: 0x04000F9F RID: 3999
			public byte[] buffer;

			// Token: 0x04000FA0 RID: 4000
			public int index;

			// Token: 0x04000FA1 RID: 4001
			public int count;

			// Token: 0x04000FA2 RID: 4002
			private ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter <>u__1;
		}

		// Token: 0x02000198 RID: 408
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <ReadElementContentAsBase64Async>d__200 : IAsyncStateMachine
		{
			// Token: 0x06000EE0 RID: 3808 RVA: 0x00060F54 File Offset: 0x0005F154
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				XsdValidatingReader xsdValidatingReader = this.<>4__this;
				int result;
				try
				{
					ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter awaiter;
					if (num != 0)
					{
						if (xsdValidatingReader.ReadState != ReadState.Interactive)
						{
							result = 0;
							goto IL_FF;
						}
						if (xsdValidatingReader.validationState != XsdValidatingReader.ValidatingReaderState.OnReadBinaryContent)
						{
							xsdValidatingReader.readBinaryHelper = ReadContentAsBinaryHelper.CreateOrReset(xsdValidatingReader.readBinaryHelper, xsdValidatingReader);
							xsdValidatingReader.savedState = xsdValidatingReader.validationState;
						}
						xsdValidatingReader.validationState = xsdValidatingReader.savedState;
						awaiter = xsdValidatingReader.readBinaryHelper.ReadElementContentAsBase64Async(this.buffer, this.index, this.count).ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 0;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter, XsdValidatingReader.<ReadElementContentAsBase64Async>d__200>(ref awaiter, ref this);
							return;
						}
					}
					else
					{
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
					}
					int result2 = awaiter.GetResult();
					xsdValidatingReader.savedState = xsdValidatingReader.validationState;
					xsdValidatingReader.validationState = XsdValidatingReader.ValidatingReaderState.OnReadBinaryContent;
					result = result2;
				}
				catch (Exception exception)
				{
					this.<>1__state = -2;
					this.<>t__builder.SetException(exception);
					return;
				}
				IL_FF:
				this.<>1__state = -2;
				this.<>t__builder.SetResult(result);
			}

			// Token: 0x06000EE1 RID: 3809 RVA: 0x00061084 File Offset: 0x0005F284
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04000FA3 RID: 4003
			public int <>1__state;

			// Token: 0x04000FA4 RID: 4004
			public AsyncTaskMethodBuilder<int> <>t__builder;

			// Token: 0x04000FA5 RID: 4005
			public XsdValidatingReader <>4__this;

			// Token: 0x04000FA6 RID: 4006
			public byte[] buffer;

			// Token: 0x04000FA7 RID: 4007
			public int index;

			// Token: 0x04000FA8 RID: 4008
			public int count;

			// Token: 0x04000FA9 RID: 4009
			private ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter <>u__1;
		}

		// Token: 0x02000199 RID: 409
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <ReadElementContentAsBinHexAsync>d__201 : IAsyncStateMachine
		{
			// Token: 0x06000EE2 RID: 3810 RVA: 0x00061094 File Offset: 0x0005F294
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				XsdValidatingReader xsdValidatingReader = this.<>4__this;
				int result;
				try
				{
					ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter awaiter;
					if (num != 0)
					{
						if (xsdValidatingReader.ReadState != ReadState.Interactive)
						{
							result = 0;
							goto IL_FF;
						}
						if (xsdValidatingReader.validationState != XsdValidatingReader.ValidatingReaderState.OnReadBinaryContent)
						{
							xsdValidatingReader.readBinaryHelper = ReadContentAsBinaryHelper.CreateOrReset(xsdValidatingReader.readBinaryHelper, xsdValidatingReader);
							xsdValidatingReader.savedState = xsdValidatingReader.validationState;
						}
						xsdValidatingReader.validationState = xsdValidatingReader.savedState;
						awaiter = xsdValidatingReader.readBinaryHelper.ReadElementContentAsBinHexAsync(this.buffer, this.index, this.count).ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 0;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter, XsdValidatingReader.<ReadElementContentAsBinHexAsync>d__201>(ref awaiter, ref this);
							return;
						}
					}
					else
					{
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
					}
					int result2 = awaiter.GetResult();
					xsdValidatingReader.savedState = xsdValidatingReader.validationState;
					xsdValidatingReader.validationState = XsdValidatingReader.ValidatingReaderState.OnReadBinaryContent;
					result = result2;
				}
				catch (Exception exception)
				{
					this.<>1__state = -2;
					this.<>t__builder.SetException(exception);
					return;
				}
				IL_FF:
				this.<>1__state = -2;
				this.<>t__builder.SetResult(result);
			}

			// Token: 0x06000EE3 RID: 3811 RVA: 0x000611C4 File Offset: 0x0005F3C4
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04000FAA RID: 4010
			public int <>1__state;

			// Token: 0x04000FAB RID: 4011
			public AsyncTaskMethodBuilder<int> <>t__builder;

			// Token: 0x04000FAC RID: 4012
			public XsdValidatingReader <>4__this;

			// Token: 0x04000FAD RID: 4013
			public byte[] buffer;

			// Token: 0x04000FAE RID: 4014
			public int index;

			// Token: 0x04000FAF RID: 4015
			public int count;

			// Token: 0x04000FB0 RID: 4016
			private ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter <>u__1;
		}

		// Token: 0x0200019A RID: 410
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <ProcessElementEventAsync>d__203 : IAsyncStateMachine
		{
			// Token: 0x06000EE4 RID: 3812 RVA: 0x000611D4 File Offset: 0x0005F3D4
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				XsdValidatingReader xsdValidatingReader = this.<>4__this;
				try
				{
					ConfiguredTaskAwaitable.ConfiguredTaskAwaiter awaiter;
					if (num != 0)
					{
						if (num != 1)
						{
							if (xsdValidatingReader.processInlineSchema && xsdValidatingReader.IsXSDRoot(xsdValidatingReader.coreReader.LocalName, xsdValidatingReader.coreReader.NamespaceURI) && xsdValidatingReader.coreReader.Depth > 0)
							{
								xsdValidatingReader.xmlSchemaInfo.Clear();
								xsdValidatingReader.attributeCount = (xsdValidatingReader.coreReaderAttributeCount = xsdValidatingReader.coreReader.AttributeCount);
								if (xsdValidatingReader.coreReader.IsEmptyElement)
								{
									xsdValidatingReader.validationState = XsdValidatingReader.ValidatingReaderState.ClearAttributes;
									goto IL_365;
								}
								xsdValidatingReader.inlineSchemaParser = new Parser(SchemaType.XSD, xsdValidatingReader.coreReaderNameTable, xsdValidatingReader.validator.SchemaSet.GetSchemaNames(xsdValidatingReader.coreReaderNameTable), xsdValidatingReader.validationEvent);
								awaiter = xsdValidatingReader.inlineSchemaParser.StartParsingAsync(xsdValidatingReader.coreReader, null).ConfigureAwait(false).GetAwaiter();
								if (!awaiter.IsCompleted)
								{
									this.<>1__state = 0;
									this.<>u__1 = awaiter;
									this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XsdValidatingReader.<ProcessElementEventAsync>d__203>(ref awaiter, ref this);
									return;
								}
								goto IL_125;
							}
							else
							{
								xsdValidatingReader.atomicValue = null;
								xsdValidatingReader.originalAtomicValueString = null;
								xsdValidatingReader.xmlSchemaInfo.Clear();
								if (xsdValidatingReader.manageNamespaces)
								{
									xsdValidatingReader.nsManager.PushScope();
								}
								string xsiSchemaLocation = null;
								string xsiNoNamespaceSchemaLocation = null;
								string xsiNil = null;
								string xsiType = null;
								if (xsdValidatingReader.coreReader.MoveToFirstAttribute())
								{
									do
									{
										string namespaceURI = xsdValidatingReader.coreReader.NamespaceURI;
										string localName = xsdValidatingReader.coreReader.LocalName;
										if (Ref.Equal(namespaceURI, xsdValidatingReader.NsXsi))
										{
											if (Ref.Equal(localName, xsdValidatingReader.XsiSchemaLocation))
											{
												xsiSchemaLocation = xsdValidatingReader.coreReader.Value;
											}
											else if (Ref.Equal(localName, xsdValidatingReader.XsiNoNamespaceSchemaLocation))
											{
												xsiNoNamespaceSchemaLocation = xsdValidatingReader.coreReader.Value;
											}
											else if (Ref.Equal(localName, xsdValidatingReader.XsiType))
											{
												xsiType = xsdValidatingReader.coreReader.Value;
											}
											else if (Ref.Equal(localName, xsdValidatingReader.XsiNil))
											{
												xsiNil = xsdValidatingReader.coreReader.Value;
											}
										}
										if (xsdValidatingReader.manageNamespaces && Ref.Equal(xsdValidatingReader.coreReader.NamespaceURI, xsdValidatingReader.NsXmlNs))
										{
											xsdValidatingReader.nsManager.AddNamespace((xsdValidatingReader.coreReader.Prefix.Length == 0) ? string.Empty : xsdValidatingReader.coreReader.LocalName, xsdValidatingReader.coreReader.Value);
										}
									}
									while (xsdValidatingReader.coreReader.MoveToNextAttribute());
									xsdValidatingReader.coreReader.MoveToElement();
								}
								xsdValidatingReader.validator.ValidateElement(xsdValidatingReader.coreReader.LocalName, xsdValidatingReader.coreReader.NamespaceURI, xsdValidatingReader.xmlSchemaInfo, xsiType, xsiNil, xsiSchemaLocation, xsiNoNamespaceSchemaLocation);
								xsdValidatingReader.ValidateAttributes();
								xsdValidatingReader.validator.ValidateEndOfAttributes(xsdValidatingReader.xmlSchemaInfo);
								if (!xsdValidatingReader.coreReader.IsEmptyElement)
								{
									goto IL_35E;
								}
								awaiter = xsdValidatingReader.ProcessEndElementEventAsync().ConfigureAwait(false).GetAwaiter();
								if (!awaiter.IsCompleted)
								{
									this.<>1__state = 1;
									this.<>u__1 = awaiter;
									this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XsdValidatingReader.<ProcessElementEventAsync>d__203>(ref awaiter, ref this);
									return;
								}
							}
						}
						else
						{
							awaiter = this.<>u__1;
							this.<>u__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
							this.<>1__state = -1;
						}
						awaiter.GetResult();
						IL_35E:
						xsdValidatingReader.validationState = XsdValidatingReader.ValidatingReaderState.ClearAttributes;
						goto IL_365;
					}
					awaiter = this.<>u__1;
					this.<>u__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
					this.<>1__state = -1;
					IL_125:
					awaiter.GetResult();
					xsdValidatingReader.inlineSchemaParser.ParseReaderNode();
					xsdValidatingReader.validationState = XsdValidatingReader.ValidatingReaderState.ParseInlineSchema;
					IL_365:;
				}
				catch (Exception exception)
				{
					this.<>1__state = -2;
					this.<>t__builder.SetException(exception);
					return;
				}
				this.<>1__state = -2;
				this.<>t__builder.SetResult();
			}

			// Token: 0x06000EE5 RID: 3813 RVA: 0x00061590 File Offset: 0x0005F790
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04000FB1 RID: 4017
			public int <>1__state;

			// Token: 0x04000FB2 RID: 4018
			public AsyncTaskMethodBuilder <>t__builder;

			// Token: 0x04000FB3 RID: 4019
			public XsdValidatingReader <>4__this;

			// Token: 0x04000FB4 RID: 4020
			private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter <>u__1;
		}

		// Token: 0x0200019B RID: 411
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <ProcessEndElementEventAsync>d__204 : IAsyncStateMachine
		{
			// Token: 0x06000EE6 RID: 3814 RVA: 0x000615A0 File Offset: 0x0005F7A0
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				XsdValidatingReader xsdValidatingReader = this.<>4__this;
				try
				{
					ConfiguredTaskAwaitable.ConfiguredTaskAwaiter awaiter;
					if (num != 0)
					{
						xsdValidatingReader.atomicValue = xsdValidatingReader.validator.ValidateEndElement(xsdValidatingReader.xmlSchemaInfo);
						xsdValidatingReader.originalAtomicValueString = xsdValidatingReader.GetOriginalAtomicValueStringOfElement();
						if (xsdValidatingReader.xmlSchemaInfo.IsDefault)
						{
							int depth = xsdValidatingReader.coreReader.Depth;
							xsdValidatingReader.coreReader = xsdValidatingReader.GetCachingReader();
							xsdValidatingReader.cachingReader.RecordTextNode(xsdValidatingReader.xmlSchemaInfo.XmlType.ValueConverter.ToString(xsdValidatingReader.atomicValue), xsdValidatingReader.originalAtomicValueString, depth + 1, 0, 0);
							xsdValidatingReader.cachingReader.RecordEndElementNode();
							awaiter = xsdValidatingReader.cachingReader.SetToReplayModeAsync().ConfigureAwait(false).GetAwaiter();
							if (!awaiter.IsCompleted)
							{
								this.<>1__state = 0;
								this.<>u__1 = awaiter;
								this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XsdValidatingReader.<ProcessEndElementEventAsync>d__204>(ref awaiter, ref this);
								return;
							}
						}
						else
						{
							if (xsdValidatingReader.manageNamespaces)
							{
								xsdValidatingReader.nsManager.PopScope();
								goto IL_120;
							}
							goto IL_120;
						}
					}
					else
					{
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
					}
					awaiter.GetResult();
					xsdValidatingReader.replayCache = true;
					IL_120:;
				}
				catch (Exception exception)
				{
					this.<>1__state = -2;
					this.<>t__builder.SetException(exception);
					return;
				}
				this.<>1__state = -2;
				this.<>t__builder.SetResult();
			}

			// Token: 0x06000EE7 RID: 3815 RVA: 0x00061718 File Offset: 0x0005F918
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04000FB5 RID: 4021
			public int <>1__state;

			// Token: 0x04000FB6 RID: 4022
			public AsyncTaskMethodBuilder <>t__builder;

			// Token: 0x04000FB7 RID: 4023
			public XsdValidatingReader <>4__this;

			// Token: 0x04000FB8 RID: 4024
			private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter <>u__1;
		}

		// Token: 0x0200019C RID: 412
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <ProcessInlineSchemaAsync>d__205 : IAsyncStateMachine
		{
			// Token: 0x06000EE8 RID: 3816 RVA: 0x00061728 File Offset: 0x0005F928
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				XsdValidatingReader xsdValidatingReader = this.<>4__this;
				try
				{
					ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter awaiter;
					if (num != 0)
					{
						awaiter = xsdValidatingReader.coreReader.ReadAsync().ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 0;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter, XsdValidatingReader.<ProcessInlineSchemaAsync>d__205>(ref awaiter, ref this);
							return;
						}
					}
					else
					{
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
					}
					if (awaiter.GetResult())
					{
						if (xsdValidatingReader.coreReader.NodeType == XmlNodeType.Element)
						{
							xsdValidatingReader.attributeCount = (xsdValidatingReader.coreReaderAttributeCount = xsdValidatingReader.coreReader.AttributeCount);
						}
						else
						{
							xsdValidatingReader.ClearAttributesInfo();
						}
						if (!xsdValidatingReader.inlineSchemaParser.ParseReaderNode())
						{
							xsdValidatingReader.inlineSchemaParser.FinishParsing();
							XmlSchema xmlSchema = xsdValidatingReader.inlineSchemaParser.XmlSchema;
							xsdValidatingReader.validator.AddSchema(xmlSchema);
							xsdValidatingReader.inlineSchemaParser = null;
							xsdValidatingReader.validationState = XsdValidatingReader.ValidatingReaderState.Read;
						}
					}
				}
				catch (Exception exception)
				{
					this.<>1__state = -2;
					this.<>t__builder.SetException(exception);
					return;
				}
				this.<>1__state = -2;
				this.<>t__builder.SetResult();
			}

			// Token: 0x06000EE9 RID: 3817 RVA: 0x00061864 File Offset: 0x0005FA64
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04000FB9 RID: 4025
			public int <>1__state;

			// Token: 0x04000FBA RID: 4026
			public AsyncTaskMethodBuilder <>t__builder;

			// Token: 0x04000FBB RID: 4027
			public XsdValidatingReader <>4__this;

			// Token: 0x04000FBC RID: 4028
			private ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter <>u__1;
		}

		// Token: 0x0200019D RID: 413
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <InternalReadContentAsObjectAsync>d__207 : IAsyncStateMachine
		{
			// Token: 0x06000EEA RID: 3818 RVA: 0x00061874 File Offset: 0x0005FA74
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				XsdValidatingReader xsdValidatingReader = this.<>4__this;
				object item;
				try
				{
					ConfiguredTaskAwaitable<Tuple<string, object>>.ConfiguredTaskAwaiter awaiter;
					if (num != 0)
					{
						awaiter = xsdValidatingReader.InternalReadContentAsObjectTupleAsync(this.unwrapTypedValue).ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 0;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<Tuple<string, object>>.ConfiguredTaskAwaiter, XsdValidatingReader.<InternalReadContentAsObjectAsync>d__207>(ref awaiter, ref this);
							return;
						}
					}
					else
					{
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable<Tuple<string, object>>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
					}
					item = awaiter.GetResult().Item2;
				}
				catch (Exception exception)
				{
					this.<>1__state = -2;
					this.<>t__builder.SetException(exception);
					return;
				}
				this.<>1__state = -2;
				this.<>t__builder.SetResult(item);
			}

			// Token: 0x06000EEB RID: 3819 RVA: 0x00061940 File Offset: 0x0005FB40
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04000FBD RID: 4029
			public int <>1__state;

			// Token: 0x04000FBE RID: 4030
			public AsyncTaskMethodBuilder<object> <>t__builder;

			// Token: 0x04000FBF RID: 4031
			public XsdValidatingReader <>4__this;

			// Token: 0x04000FC0 RID: 4032
			public bool unwrapTypedValue;

			// Token: 0x04000FC1 RID: 4033
			private ConfiguredTaskAwaitable<Tuple<string, object>>.ConfiguredTaskAwaiter <>u__1;
		}

		// Token: 0x0200019E RID: 414
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <InternalReadContentAsObjectTupleAsync>d__208 : IAsyncStateMachine
		{
			// Token: 0x06000EEC RID: 3820 RVA: 0x00061950 File Offset: 0x0005FB50
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				XsdValidatingReader xsdValidatingReader = this.<>4__this;
				Tuple<string, object> result;
				try
				{
					string text;
					ConfiguredTaskAwaitable<object>.ConfiguredTaskAwaiter awaiter;
					if (num != 0)
					{
						ConfiguredTaskAwaitable<string>.ConfiguredTaskAwaiter awaiter2;
						if (num != 1)
						{
							XmlNodeType nodeType = xsdValidatingReader.NodeType;
							if (nodeType == XmlNodeType.Attribute)
							{
								text = xsdValidatingReader.Value;
								if (xsdValidatingReader.attributePSVI != null && xsdValidatingReader.attributePSVI.typedAttributeValue != null)
								{
									if (xsdValidatingReader.validationState == XsdValidatingReader.ValidatingReaderState.OnDefaultAttribute)
									{
										XmlSchemaAttribute schemaAttribute = xsdValidatingReader.attributePSVI.attributeSchemaInfo.SchemaAttribute;
										text = ((schemaAttribute.DefaultValue != null) ? schemaAttribute.DefaultValue : schemaAttribute.FixedValue);
									}
									result = new Tuple<string, object>(text, xsdValidatingReader.ReturnBoxedValue(xsdValidatingReader.attributePSVI.typedAttributeValue, xsdValidatingReader.AttributeSchemaInfo.XmlType, this.unwrapTypedValue));
									goto IL_248;
								}
								result = new Tuple<string, object>(text, xsdValidatingReader.Value);
								goto IL_248;
							}
							else if (nodeType == XmlNodeType.EndElement)
							{
								if (xsdValidatingReader.atomicValue != null)
								{
									text = xsdValidatingReader.originalAtomicValueString;
									result = new Tuple<string, object>(text, xsdValidatingReader.atomicValue);
									goto IL_248;
								}
								text = string.Empty;
								result = new Tuple<string, object>(text, string.Empty);
								goto IL_248;
							}
							else if (xsdValidatingReader.validator.CurrentContentType == XmlSchemaContentType.TextOnly)
							{
								awaiter = xsdValidatingReader.ReadTillEndElementAsync().ConfigureAwait(false).GetAwaiter();
								if (!awaiter.IsCompleted)
								{
									this.<>1__state = 0;
									this.<>u__1 = awaiter;
									this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<object>.ConfiguredTaskAwaiter, XsdValidatingReader.<InternalReadContentAsObjectTupleAsync>d__208>(ref awaiter, ref this);
									return;
								}
								goto IL_16B;
							}
							else
							{
								XsdCachingReader xsdCachingReader = xsdValidatingReader.coreReader as XsdCachingReader;
								if (xsdCachingReader != null)
								{
									text = xsdCachingReader.ReadOriginalContentAsString();
									goto IL_225;
								}
								awaiter2 = xsdValidatingReader.InternalReadContentAsStringAsync().ConfigureAwait(false).GetAwaiter();
								if (!awaiter2.IsCompleted)
								{
									this.<>1__state = 1;
									this.<>u__2 = awaiter2;
									this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<string>.ConfiguredTaskAwaiter, XsdValidatingReader.<InternalReadContentAsObjectTupleAsync>d__208>(ref awaiter2, ref this);
									return;
								}
							}
						}
						else
						{
							awaiter2 = this.<>u__2;
							this.<>u__2 = default(ConfiguredTaskAwaitable<string>.ConfiguredTaskAwaiter);
							this.<>1__state = -1;
						}
						text = awaiter2.GetResult();
						IL_225:
						result = new Tuple<string, object>(text, text);
						goto IL_248;
					}
					awaiter = this.<>u__1;
					this.<>u__1 = default(ConfiguredTaskAwaitable<object>.ConfiguredTaskAwaiter);
					this.<>1__state = -1;
					IL_16B:
					object result2 = awaiter.GetResult();
					object item = xsdValidatingReader.ReturnBoxedValue(result2, xsdValidatingReader.xmlSchemaInfo.XmlType, this.unwrapTypedValue);
					text = xsdValidatingReader.originalAtomicValueString;
					result = new Tuple<string, object>(text, item);
				}
				catch (Exception exception)
				{
					this.<>1__state = -2;
					this.<>t__builder.SetException(exception);
					return;
				}
				IL_248:
				this.<>1__state = -2;
				this.<>t__builder.SetResult(result);
			}

			// Token: 0x06000EED RID: 3821 RVA: 0x00061BD8 File Offset: 0x0005FDD8
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04000FC2 RID: 4034
			public int <>1__state;

			// Token: 0x04000FC3 RID: 4035
			public AsyncTaskMethodBuilder<Tuple<string, object>> <>t__builder;

			// Token: 0x04000FC4 RID: 4036
			public XsdValidatingReader <>4__this;

			// Token: 0x04000FC5 RID: 4037
			public bool unwrapTypedValue;

			// Token: 0x04000FC6 RID: 4038
			private ConfiguredTaskAwaitable<object>.ConfiguredTaskAwaiter <>u__1;

			// Token: 0x04000FC7 RID: 4039
			private ConfiguredTaskAwaitable<string>.ConfiguredTaskAwaiter <>u__2;
		}

		// Token: 0x0200019F RID: 415
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <InternalReadElementContentAsObjectAsync>d__210 : IAsyncStateMachine
		{
			// Token: 0x06000EEE RID: 3822 RVA: 0x00061BE8 File Offset: 0x0005FDE8
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				XsdValidatingReader xsdValidatingReader = this.<>4__this;
				Tuple<XmlSchemaType, object> result2;
				try
				{
					ConfiguredTaskAwaitable<Tuple<XmlSchemaType, string, object>>.ConfiguredTaskAwaiter awaiter;
					if (num != 0)
					{
						awaiter = xsdValidatingReader.InternalReadElementContentAsObjectTupleAsync(this.unwrapTypedValue).ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 0;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<Tuple<XmlSchemaType, string, object>>.ConfiguredTaskAwaiter, XsdValidatingReader.<InternalReadElementContentAsObjectAsync>d__210>(ref awaiter, ref this);
							return;
						}
					}
					else
					{
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable<Tuple<XmlSchemaType, string, object>>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
					}
					Tuple<XmlSchemaType, string, object> result = awaiter.GetResult();
					result2 = new Tuple<XmlSchemaType, object>(result.Item1, result.Item3);
				}
				catch (Exception exception)
				{
					this.<>1__state = -2;
					this.<>t__builder.SetException(exception);
					return;
				}
				this.<>1__state = -2;
				this.<>t__builder.SetResult(result2);
			}

			// Token: 0x06000EEF RID: 3823 RVA: 0x00061CC4 File Offset: 0x0005FEC4
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04000FC8 RID: 4040
			public int <>1__state;

			// Token: 0x04000FC9 RID: 4041
			public AsyncTaskMethodBuilder<Tuple<XmlSchemaType, object>> <>t__builder;

			// Token: 0x04000FCA RID: 4042
			public XsdValidatingReader <>4__this;

			// Token: 0x04000FCB RID: 4043
			public bool unwrapTypedValue;

			// Token: 0x04000FCC RID: 4044
			private ConfiguredTaskAwaitable<Tuple<XmlSchemaType, string, object>>.ConfiguredTaskAwaiter <>u__1;
		}

		// Token: 0x020001A0 RID: 416
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <InternalReadElementContentAsObjectTupleAsync>d__211 : IAsyncStateMachine
		{
			// Token: 0x06000EF0 RID: 3824 RVA: 0x00061CD4 File Offset: 0x0005FED4
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				XsdValidatingReader xsdValidatingReader = this.<>4__this;
				Tuple<XmlSchemaType, string, object> result;
				try
				{
					ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter awaiter;
					ConfiguredTaskAwaitable<Tuple<string, object>>.ConfiguredTaskAwaiter awaiter2;
					switch (num)
					{
					case 0:
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						break;
					case 1:
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_174;
					case 2:
						awaiter2 = this.<>u__2;
						this.<>u__2 = default(ConfiguredTaskAwaitable<Tuple<string, object>>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_27E;
					case 3:
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_326;
					default:
						this.<typedValue>5__4 = null;
						this.<xmlType>5__2 = null;
						if (xsdValidatingReader.IsEmptyElement)
						{
							if (xsdValidatingReader.xmlSchemaInfo.ContentType == XmlSchemaContentType.TextOnly)
							{
								this.<typedValue>5__4 = xsdValidatingReader.ReturnBoxedValue(xsdValidatingReader.atomicValue, xsdValidatingReader.xmlSchemaInfo.XmlType, this.unwrapTypedValue);
							}
							else
							{
								this.<typedValue>5__4 = xsdValidatingReader.atomicValue;
							}
							this.<originalString>5__3 = xsdValidatingReader.originalAtomicValueString;
							this.<xmlType>5__2 = xsdValidatingReader.ElementXmlType;
							awaiter = xsdValidatingReader.ReadAsync().ConfigureAwait(false).GetAwaiter();
							if (!awaiter.IsCompleted)
							{
								this.<>1__state = 0;
								this.<>u__1 = awaiter;
								this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter, XsdValidatingReader.<InternalReadElementContentAsObjectTupleAsync>d__211>(ref awaiter, ref this);
								return;
							}
						}
						else
						{
							awaiter = xsdValidatingReader.ReadAsync().ConfigureAwait(false).GetAwaiter();
							if (!awaiter.IsCompleted)
							{
								this.<>1__state = 1;
								this.<>u__1 = awaiter;
								this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter, XsdValidatingReader.<InternalReadElementContentAsObjectTupleAsync>d__211>(ref awaiter, ref this);
								return;
							}
							goto IL_174;
						}
						break;
					}
					awaiter.GetResult();
					result = new Tuple<XmlSchemaType, string, object>(this.<xmlType>5__2, this.<originalString>5__3, this.<typedValue>5__4);
					goto IL_376;
					IL_174:
					awaiter.GetResult();
					if (xsdValidatingReader.NodeType == XmlNodeType.EndElement)
					{
						if (xsdValidatingReader.xmlSchemaInfo.IsDefault)
						{
							if (xsdValidatingReader.xmlSchemaInfo.ContentType == XmlSchemaContentType.TextOnly)
							{
								this.<typedValue>5__4 = xsdValidatingReader.ReturnBoxedValue(xsdValidatingReader.atomicValue, xsdValidatingReader.xmlSchemaInfo.XmlType, this.unwrapTypedValue);
							}
							else
							{
								this.<typedValue>5__4 = xsdValidatingReader.atomicValue;
							}
							this.<originalString>5__3 = xsdValidatingReader.originalAtomicValueString;
							goto IL_2BC;
						}
						this.<typedValue>5__4 = string.Empty;
						this.<originalString>5__3 = string.Empty;
						goto IL_2BC;
					}
					else
					{
						if (xsdValidatingReader.NodeType == XmlNodeType.Element)
						{
							throw new XmlException("ReadElementContentAs() methods cannot be called on an element that has child elements.", string.Empty, xsdValidatingReader);
						}
						awaiter2 = xsdValidatingReader.InternalReadContentAsObjectTupleAsync(this.unwrapTypedValue).ConfigureAwait(false).GetAwaiter();
						if (!awaiter2.IsCompleted)
						{
							this.<>1__state = 2;
							this.<>u__2 = awaiter2;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<Tuple<string, object>>.ConfiguredTaskAwaiter, XsdValidatingReader.<InternalReadElementContentAsObjectTupleAsync>d__211>(ref awaiter2, ref this);
							return;
						}
					}
					IL_27E:
					Tuple<string, object> result2 = awaiter2.GetResult();
					this.<originalString>5__3 = result2.Item1;
					this.<typedValue>5__4 = result2.Item2;
					if (xsdValidatingReader.NodeType != XmlNodeType.EndElement)
					{
						throw new XmlException("ReadElementContentAs() methods cannot be called on an element that has child elements.", string.Empty, xsdValidatingReader);
					}
					IL_2BC:
					this.<xmlType>5__2 = xsdValidatingReader.ElementXmlType;
					awaiter = xsdValidatingReader.ReadAsync().ConfigureAwait(false).GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						this.<>1__state = 3;
						this.<>u__1 = awaiter;
						this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter, XsdValidatingReader.<InternalReadElementContentAsObjectTupleAsync>d__211>(ref awaiter, ref this);
						return;
					}
					IL_326:
					awaiter.GetResult();
					result = new Tuple<XmlSchemaType, string, object>(this.<xmlType>5__2, this.<originalString>5__3, this.<typedValue>5__4);
				}
				catch (Exception exception)
				{
					this.<>1__state = -2;
					this.<xmlType>5__2 = null;
					this.<originalString>5__3 = null;
					this.<typedValue>5__4 = null;
					this.<>t__builder.SetException(exception);
					return;
				}
				IL_376:
				this.<>1__state = -2;
				this.<xmlType>5__2 = null;
				this.<originalString>5__3 = null;
				this.<typedValue>5__4 = null;
				this.<>t__builder.SetResult(result);
			}

			// Token: 0x06000EF1 RID: 3825 RVA: 0x0006209C File Offset: 0x0006029C
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04000FCD RID: 4045
			public int <>1__state;

			// Token: 0x04000FCE RID: 4046
			public AsyncTaskMethodBuilder<Tuple<XmlSchemaType, string, object>> <>t__builder;

			// Token: 0x04000FCF RID: 4047
			public XsdValidatingReader <>4__this;

			// Token: 0x04000FD0 RID: 4048
			public bool unwrapTypedValue;

			// Token: 0x04000FD1 RID: 4049
			private XmlSchemaType <xmlType>5__2;

			// Token: 0x04000FD2 RID: 4050
			private string <originalString>5__3;

			// Token: 0x04000FD3 RID: 4051
			private object <typedValue>5__4;

			// Token: 0x04000FD4 RID: 4052
			private ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter <>u__1;

			// Token: 0x04000FD5 RID: 4053
			private ConfiguredTaskAwaitable<Tuple<string, object>>.ConfiguredTaskAwaiter <>u__2;
		}

		// Token: 0x020001A1 RID: 417
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <ReadTillEndElementAsync>d__212 : IAsyncStateMachine
		{
			// Token: 0x06000EF2 RID: 3826 RVA: 0x000620AC File Offset: 0x000602AC
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				XsdValidatingReader xsdValidatingReader = this.<>4__this;
				object atomicValue;
				try
				{
					ConfiguredTaskAwaitable.ConfiguredTaskAwaiter awaiter2;
					if (num != 0)
					{
						ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter awaiter;
						if (num == 1)
						{
							awaiter = this.<>u__2;
							this.<>u__2 = default(ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter);
							this.<>1__state = -1;
							goto IL_1CC;
						}
						if (xsdValidatingReader.atomicValue != null)
						{
							if (xsdValidatingReader.atomicValue == xsdValidatingReader)
							{
								xsdValidatingReader.atomicValue = null;
							}
							xsdValidatingReader.SwitchReader();
							goto IL_1F0;
						}
						IL_169:
						awaiter = xsdValidatingReader.coreReader.ReadAsync().ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 1;
							this.<>u__2 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter, XsdValidatingReader.<ReadTillEndElementAsync>d__212>(ref awaiter, ref this);
							return;
						}
						IL_1CC:
						if (!awaiter.GetResult())
						{
							goto IL_1F0;
						}
						if (xsdValidatingReader.replayCache)
						{
							goto IL_169;
						}
						switch (xsdValidatingReader.coreReader.NodeType)
						{
						case XmlNodeType.Element:
							awaiter2 = xsdValidatingReader.ProcessReaderEventAsync().ConfigureAwait(false).GetAwaiter();
							if (!awaiter2.IsCompleted)
							{
								this.<>1__state = 0;
								this.<>u__1 = awaiter2;
								this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XsdValidatingReader.<ReadTillEndElementAsync>d__212>(ref awaiter2, ref this);
								return;
							}
							break;
						case XmlNodeType.Attribute:
						case XmlNodeType.EntityReference:
						case XmlNodeType.Entity:
						case XmlNodeType.ProcessingInstruction:
						case XmlNodeType.Comment:
						case XmlNodeType.Document:
						case XmlNodeType.DocumentType:
						case XmlNodeType.DocumentFragment:
						case XmlNodeType.Notation:
							goto IL_169;
						case XmlNodeType.Text:
						case XmlNodeType.CDATA:
							xsdValidatingReader.validator.ValidateText(new XmlValueGetter(xsdValidatingReader.GetStringValue));
							goto IL_169;
						case XmlNodeType.Whitespace:
						case XmlNodeType.SignificantWhitespace:
							xsdValidatingReader.validator.ValidateWhitespace(new XmlValueGetter(xsdValidatingReader.GetStringValue));
							goto IL_169;
						case XmlNodeType.EndElement:
							xsdValidatingReader.atomicValue = xsdValidatingReader.validator.ValidateEndElement(xsdValidatingReader.xmlSchemaInfo);
							xsdValidatingReader.originalAtomicValueString = xsdValidatingReader.GetOriginalAtomicValueStringOfElement();
							if (xsdValidatingReader.manageNamespaces)
							{
								xsdValidatingReader.nsManager.PopScope();
								goto IL_1F0;
							}
							goto IL_1F0;
						default:
							goto IL_169;
						}
					}
					else
					{
						awaiter2 = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
					}
					awaiter2.GetResult();
					IL_1F0:
					atomicValue = xsdValidatingReader.atomicValue;
				}
				catch (Exception exception)
				{
					this.<>1__state = -2;
					this.<>t__builder.SetException(exception);
					return;
				}
				this.<>1__state = -2;
				this.<>t__builder.SetResult(atomicValue);
			}

			// Token: 0x06000EF3 RID: 3827 RVA: 0x000622FC File Offset: 0x000604FC
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04000FD6 RID: 4054
			public int <>1__state;

			// Token: 0x04000FD7 RID: 4055
			public AsyncTaskMethodBuilder<object> <>t__builder;

			// Token: 0x04000FD8 RID: 4056
			public XsdValidatingReader <>4__this;

			// Token: 0x04000FD9 RID: 4057
			private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter <>u__1;

			// Token: 0x04000FDA RID: 4058
			private ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter <>u__2;
		}
	}
}

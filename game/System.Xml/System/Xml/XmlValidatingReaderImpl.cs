using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace System.Xml
{
	// Token: 0x02000140 RID: 320
	internal sealed class XmlValidatingReaderImpl : XmlReader, IXmlLineInfo, IXmlNamespaceResolver
	{
		// Token: 0x06000B8F RID: 2959 RVA: 0x0004D154 File Offset: 0x0004B354
		internal XmlValidatingReaderImpl(XmlReader reader)
		{
			XmlAsyncCheckReader xmlAsyncCheckReader = reader as XmlAsyncCheckReader;
			if (xmlAsyncCheckReader != null)
			{
				reader = xmlAsyncCheckReader.CoreReader;
			}
			this.outerReader = this;
			this.coreReader = reader;
			this.coreReaderNSResolver = (reader as IXmlNamespaceResolver);
			this.coreReaderImpl = (reader as XmlTextReaderImpl);
			if (this.coreReaderImpl == null)
			{
				XmlTextReader xmlTextReader = reader as XmlTextReader;
				if (xmlTextReader != null)
				{
					this.coreReaderImpl = xmlTextReader.Impl;
				}
			}
			if (this.coreReaderImpl == null)
			{
				throw new ArgumentException(Res.GetString("The XmlReader passed in to construct this XmlValidatingReaderImpl must be an instance of a System.Xml.XmlTextReader."), "reader");
			}
			this.coreReaderImpl.EntityHandling = EntityHandling.ExpandEntities;
			this.coreReaderImpl.XmlValidatingReaderCompatibilityMode = true;
			this.processIdentityConstraints = true;
			this.schemaCollection = new XmlSchemaCollection(this.coreReader.NameTable);
			this.schemaCollection.XmlResolver = this.GetResolver();
			this.eventHandling = new XmlValidatingReaderImpl.ValidationEventHandling(this);
			this.coreReaderImpl.ValidationEventHandling = this.eventHandling;
			this.coreReaderImpl.OnDefaultAttributeUse = new XmlTextReaderImpl.OnDefaultAttributeUseDelegate(this.ValidateDefaultAttributeOnUse);
			this.validationType = ValidationType.Auto;
			this.SetupValidation(ValidationType.Auto);
		}

		// Token: 0x06000B90 RID: 2960 RVA: 0x0004D26C File Offset: 0x0004B46C
		internal XmlValidatingReaderImpl(string xmlFragment, XmlNodeType fragType, XmlParserContext context) : this(new XmlTextReader(xmlFragment, fragType, context))
		{
			if (this.coreReader.BaseURI.Length > 0)
			{
				this.validator.BaseUri = this.GetResolver().ResolveUri(null, this.coreReader.BaseURI);
			}
			if (context != null)
			{
				this.parsingFunction = XmlValidatingReaderImpl.ParsingFunction.ParseDtdFromContext;
				this.parserContext = context;
			}
		}

		// Token: 0x06000B91 RID: 2961 RVA: 0x0004D2D0 File Offset: 0x0004B4D0
		internal XmlValidatingReaderImpl(Stream xmlFragment, XmlNodeType fragType, XmlParserContext context) : this(new XmlTextReader(xmlFragment, fragType, context))
		{
			if (this.coreReader.BaseURI.Length > 0)
			{
				this.validator.BaseUri = this.GetResolver().ResolveUri(null, this.coreReader.BaseURI);
			}
			if (context != null)
			{
				this.parsingFunction = XmlValidatingReaderImpl.ParsingFunction.ParseDtdFromContext;
				this.parserContext = context;
			}
		}

		// Token: 0x06000B92 RID: 2962 RVA: 0x0004D334 File Offset: 0x0004B534
		internal XmlValidatingReaderImpl(XmlReader reader, ValidationEventHandler settingsEventHandler, bool processIdentityConstraints)
		{
			XmlAsyncCheckReader xmlAsyncCheckReader = reader as XmlAsyncCheckReader;
			if (xmlAsyncCheckReader != null)
			{
				reader = xmlAsyncCheckReader.CoreReader;
			}
			this.outerReader = this;
			this.coreReader = reader;
			this.coreReaderImpl = (reader as XmlTextReaderImpl);
			if (this.coreReaderImpl == null)
			{
				XmlTextReader xmlTextReader = reader as XmlTextReader;
				if (xmlTextReader != null)
				{
					this.coreReaderImpl = xmlTextReader.Impl;
				}
			}
			if (this.coreReaderImpl == null)
			{
				throw new ArgumentException(Res.GetString("The XmlReader passed in to construct this XmlValidatingReaderImpl must be an instance of a System.Xml.XmlTextReader."), "reader");
			}
			this.coreReaderImpl.XmlValidatingReaderCompatibilityMode = true;
			this.coreReaderNSResolver = (reader as IXmlNamespaceResolver);
			this.processIdentityConstraints = processIdentityConstraints;
			this.schemaCollection = new XmlSchemaCollection(this.coreReader.NameTable);
			this.schemaCollection.XmlResolver = this.GetResolver();
			this.eventHandling = new XmlValidatingReaderImpl.ValidationEventHandling(this);
			if (settingsEventHandler != null)
			{
				this.eventHandling.AddHandler(settingsEventHandler);
			}
			this.coreReaderImpl.ValidationEventHandling = this.eventHandling;
			this.coreReaderImpl.OnDefaultAttributeUse = new XmlTextReaderImpl.OnDefaultAttributeUseDelegate(this.ValidateDefaultAttributeOnUse);
			this.validationType = ValidationType.DTD;
			this.SetupValidation(ValidationType.DTD);
		}

		// Token: 0x170001A6 RID: 422
		// (get) Token: 0x06000B93 RID: 2963 RVA: 0x0004D44C File Offset: 0x0004B64C
		public override XmlReaderSettings Settings
		{
			get
			{
				XmlReaderSettings xmlReaderSettings;
				if (this.coreReaderImpl.V1Compat)
				{
					xmlReaderSettings = null;
				}
				else
				{
					xmlReaderSettings = this.coreReader.Settings;
				}
				if (xmlReaderSettings != null)
				{
					xmlReaderSettings = xmlReaderSettings.Clone();
				}
				else
				{
					xmlReaderSettings = new XmlReaderSettings();
				}
				xmlReaderSettings.ValidationType = ValidationType.DTD;
				if (!this.processIdentityConstraints)
				{
					xmlReaderSettings.ValidationFlags &= ~XmlSchemaValidationFlags.ProcessIdentityConstraints;
				}
				xmlReaderSettings.ReadOnly = true;
				return xmlReaderSettings;
			}
		}

		// Token: 0x170001A7 RID: 423
		// (get) Token: 0x06000B94 RID: 2964 RVA: 0x0004D4AE File Offset: 0x0004B6AE
		public override XmlNodeType NodeType
		{
			get
			{
				return this.coreReader.NodeType;
			}
		}

		// Token: 0x170001A8 RID: 424
		// (get) Token: 0x06000B95 RID: 2965 RVA: 0x0004D4BB File Offset: 0x0004B6BB
		public override string Name
		{
			get
			{
				return this.coreReader.Name;
			}
		}

		// Token: 0x170001A9 RID: 425
		// (get) Token: 0x06000B96 RID: 2966 RVA: 0x0004D4C8 File Offset: 0x0004B6C8
		public override string LocalName
		{
			get
			{
				return this.coreReader.LocalName;
			}
		}

		// Token: 0x170001AA RID: 426
		// (get) Token: 0x06000B97 RID: 2967 RVA: 0x0004D4D5 File Offset: 0x0004B6D5
		public override string NamespaceURI
		{
			get
			{
				return this.coreReader.NamespaceURI;
			}
		}

		// Token: 0x170001AB RID: 427
		// (get) Token: 0x06000B98 RID: 2968 RVA: 0x0004D4E2 File Offset: 0x0004B6E2
		public override string Prefix
		{
			get
			{
				return this.coreReader.Prefix;
			}
		}

		// Token: 0x170001AC RID: 428
		// (get) Token: 0x06000B99 RID: 2969 RVA: 0x0004D4EF File Offset: 0x0004B6EF
		public override bool HasValue
		{
			get
			{
				return this.coreReader.HasValue;
			}
		}

		// Token: 0x170001AD RID: 429
		// (get) Token: 0x06000B9A RID: 2970 RVA: 0x0004D4FC File Offset: 0x0004B6FC
		public override string Value
		{
			get
			{
				return this.coreReader.Value;
			}
		}

		// Token: 0x170001AE RID: 430
		// (get) Token: 0x06000B9B RID: 2971 RVA: 0x0004D509 File Offset: 0x0004B709
		public override int Depth
		{
			get
			{
				return this.coreReader.Depth;
			}
		}

		// Token: 0x170001AF RID: 431
		// (get) Token: 0x06000B9C RID: 2972 RVA: 0x0004D516 File Offset: 0x0004B716
		public override string BaseURI
		{
			get
			{
				return this.coreReader.BaseURI;
			}
		}

		// Token: 0x170001B0 RID: 432
		// (get) Token: 0x06000B9D RID: 2973 RVA: 0x0004D523 File Offset: 0x0004B723
		public override bool IsEmptyElement
		{
			get
			{
				return this.coreReader.IsEmptyElement;
			}
		}

		// Token: 0x170001B1 RID: 433
		// (get) Token: 0x06000B9E RID: 2974 RVA: 0x0004D530 File Offset: 0x0004B730
		public override bool IsDefault
		{
			get
			{
				return this.coreReader.IsDefault;
			}
		}

		// Token: 0x170001B2 RID: 434
		// (get) Token: 0x06000B9F RID: 2975 RVA: 0x0004D53D File Offset: 0x0004B73D
		public override char QuoteChar
		{
			get
			{
				return this.coreReader.QuoteChar;
			}
		}

		// Token: 0x170001B3 RID: 435
		// (get) Token: 0x06000BA0 RID: 2976 RVA: 0x0004D54A File Offset: 0x0004B74A
		public override XmlSpace XmlSpace
		{
			get
			{
				return this.coreReader.XmlSpace;
			}
		}

		// Token: 0x170001B4 RID: 436
		// (get) Token: 0x06000BA1 RID: 2977 RVA: 0x0004D557 File Offset: 0x0004B757
		public override string XmlLang
		{
			get
			{
				return this.coreReader.XmlLang;
			}
		}

		// Token: 0x170001B5 RID: 437
		// (get) Token: 0x06000BA2 RID: 2978 RVA: 0x0004D564 File Offset: 0x0004B764
		public override ReadState ReadState
		{
			get
			{
				if (this.parsingFunction != XmlValidatingReaderImpl.ParsingFunction.Init)
				{
					return this.coreReader.ReadState;
				}
				return ReadState.Initial;
			}
		}

		// Token: 0x170001B6 RID: 438
		// (get) Token: 0x06000BA3 RID: 2979 RVA: 0x0004D57C File Offset: 0x0004B77C
		public override bool EOF
		{
			get
			{
				return this.coreReader.EOF;
			}
		}

		// Token: 0x170001B7 RID: 439
		// (get) Token: 0x06000BA4 RID: 2980 RVA: 0x0004D589 File Offset: 0x0004B789
		public override XmlNameTable NameTable
		{
			get
			{
				return this.coreReader.NameTable;
			}
		}

		// Token: 0x170001B8 RID: 440
		// (get) Token: 0x06000BA5 RID: 2981 RVA: 0x0004D596 File Offset: 0x0004B796
		internal Encoding Encoding
		{
			get
			{
				return this.coreReaderImpl.Encoding;
			}
		}

		// Token: 0x170001B9 RID: 441
		// (get) Token: 0x06000BA6 RID: 2982 RVA: 0x0004D5A3 File Offset: 0x0004B7A3
		public override int AttributeCount
		{
			get
			{
				return this.coreReader.AttributeCount;
			}
		}

		// Token: 0x06000BA7 RID: 2983 RVA: 0x0004D5B0 File Offset: 0x0004B7B0
		public override string GetAttribute(string name)
		{
			return this.coreReader.GetAttribute(name);
		}

		// Token: 0x06000BA8 RID: 2984 RVA: 0x0004D5BE File Offset: 0x0004B7BE
		public override string GetAttribute(string localName, string namespaceURI)
		{
			return this.coreReader.GetAttribute(localName, namespaceURI);
		}

		// Token: 0x06000BA9 RID: 2985 RVA: 0x0004D5CD File Offset: 0x0004B7CD
		public override string GetAttribute(int i)
		{
			return this.coreReader.GetAttribute(i);
		}

		// Token: 0x06000BAA RID: 2986 RVA: 0x0004D5DB File Offset: 0x0004B7DB
		public override bool MoveToAttribute(string name)
		{
			if (!this.coreReader.MoveToAttribute(name))
			{
				return false;
			}
			this.parsingFunction = XmlValidatingReaderImpl.ParsingFunction.Read;
			return true;
		}

		// Token: 0x06000BAB RID: 2987 RVA: 0x0004D5F5 File Offset: 0x0004B7F5
		public override bool MoveToAttribute(string localName, string namespaceURI)
		{
			if (!this.coreReader.MoveToAttribute(localName, namespaceURI))
			{
				return false;
			}
			this.parsingFunction = XmlValidatingReaderImpl.ParsingFunction.Read;
			return true;
		}

		// Token: 0x06000BAC RID: 2988 RVA: 0x0004D610 File Offset: 0x0004B810
		public override void MoveToAttribute(int i)
		{
			this.coreReader.MoveToAttribute(i);
			this.parsingFunction = XmlValidatingReaderImpl.ParsingFunction.Read;
		}

		// Token: 0x06000BAD RID: 2989 RVA: 0x0004D625 File Offset: 0x0004B825
		public override bool MoveToFirstAttribute()
		{
			if (!this.coreReader.MoveToFirstAttribute())
			{
				return false;
			}
			this.parsingFunction = XmlValidatingReaderImpl.ParsingFunction.Read;
			return true;
		}

		// Token: 0x06000BAE RID: 2990 RVA: 0x0004D63E File Offset: 0x0004B83E
		public override bool MoveToNextAttribute()
		{
			if (!this.coreReader.MoveToNextAttribute())
			{
				return false;
			}
			this.parsingFunction = XmlValidatingReaderImpl.ParsingFunction.Read;
			return true;
		}

		// Token: 0x06000BAF RID: 2991 RVA: 0x0004D657 File Offset: 0x0004B857
		public override bool MoveToElement()
		{
			if (!this.coreReader.MoveToElement())
			{
				return false;
			}
			this.parsingFunction = XmlValidatingReaderImpl.ParsingFunction.Read;
			return true;
		}

		// Token: 0x06000BB0 RID: 2992 RVA: 0x0004D670 File Offset: 0x0004B870
		public override bool Read()
		{
			switch (this.parsingFunction)
			{
			case XmlValidatingReaderImpl.ParsingFunction.Read:
				break;
			case XmlValidatingReaderImpl.ParsingFunction.Init:
				this.parsingFunction = XmlValidatingReaderImpl.ParsingFunction.Read;
				if (this.coreReader.ReadState == ReadState.Interactive)
				{
					this.ProcessCoreReaderEvent();
					return true;
				}
				break;
			case XmlValidatingReaderImpl.ParsingFunction.ParseDtdFromContext:
				this.parsingFunction = XmlValidatingReaderImpl.ParsingFunction.Read;
				this.ParseDtdFromParserContext();
				break;
			case XmlValidatingReaderImpl.ParsingFunction.ResolveEntityInternally:
				this.parsingFunction = XmlValidatingReaderImpl.ParsingFunction.Read;
				this.ResolveEntityInternally();
				break;
			case XmlValidatingReaderImpl.ParsingFunction.InReadBinaryContent:
				this.parsingFunction = XmlValidatingReaderImpl.ParsingFunction.Read;
				this.readBinaryHelper.Finish();
				break;
			case XmlValidatingReaderImpl.ParsingFunction.ReaderClosed:
			case XmlValidatingReaderImpl.ParsingFunction.Error:
				return false;
			default:
				return false;
			}
			if (this.coreReader.Read())
			{
				this.ProcessCoreReaderEvent();
				return true;
			}
			this.validator.CompleteValidation();
			return false;
		}

		// Token: 0x06000BB1 RID: 2993 RVA: 0x0004D71C File Offset: 0x0004B91C
		public override void Close()
		{
			this.coreReader.Close();
			this.parsingFunction = XmlValidatingReaderImpl.ParsingFunction.ReaderClosed;
		}

		// Token: 0x06000BB2 RID: 2994 RVA: 0x0004D730 File Offset: 0x0004B930
		public override string LookupNamespace(string prefix)
		{
			return this.coreReaderImpl.LookupNamespace(prefix);
		}

		// Token: 0x06000BB3 RID: 2995 RVA: 0x0004D73E File Offset: 0x0004B93E
		public override bool ReadAttributeValue()
		{
			if (this.parsingFunction == XmlValidatingReaderImpl.ParsingFunction.InReadBinaryContent)
			{
				this.parsingFunction = XmlValidatingReaderImpl.ParsingFunction.Read;
				this.readBinaryHelper.Finish();
			}
			if (!this.coreReader.ReadAttributeValue())
			{
				return false;
			}
			this.parsingFunction = XmlValidatingReaderImpl.ParsingFunction.Read;
			return true;
		}

		// Token: 0x170001BA RID: 442
		// (get) Token: 0x06000BB4 RID: 2996 RVA: 0x0001222F File Offset: 0x0001042F
		public override bool CanReadBinaryContent
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000BB5 RID: 2997 RVA: 0x0004D774 File Offset: 0x0004B974
		public override int ReadContentAsBase64(byte[] buffer, int index, int count)
		{
			if (this.ReadState != ReadState.Interactive)
			{
				return 0;
			}
			if (this.parsingFunction != XmlValidatingReaderImpl.ParsingFunction.InReadBinaryContent)
			{
				this.readBinaryHelper = ReadContentAsBinaryHelper.CreateOrReset(this.readBinaryHelper, this.outerReader);
			}
			this.parsingFunction = XmlValidatingReaderImpl.ParsingFunction.Read;
			int result = this.readBinaryHelper.ReadContentAsBase64(buffer, index, count);
			this.parsingFunction = XmlValidatingReaderImpl.ParsingFunction.InReadBinaryContent;
			return result;
		}

		// Token: 0x06000BB6 RID: 2998 RVA: 0x0004D7C8 File Offset: 0x0004B9C8
		public override int ReadContentAsBinHex(byte[] buffer, int index, int count)
		{
			if (this.ReadState != ReadState.Interactive)
			{
				return 0;
			}
			if (this.parsingFunction != XmlValidatingReaderImpl.ParsingFunction.InReadBinaryContent)
			{
				this.readBinaryHelper = ReadContentAsBinaryHelper.CreateOrReset(this.readBinaryHelper, this.outerReader);
			}
			this.parsingFunction = XmlValidatingReaderImpl.ParsingFunction.Read;
			int result = this.readBinaryHelper.ReadContentAsBinHex(buffer, index, count);
			this.parsingFunction = XmlValidatingReaderImpl.ParsingFunction.InReadBinaryContent;
			return result;
		}

		// Token: 0x06000BB7 RID: 2999 RVA: 0x0004D81C File Offset: 0x0004BA1C
		public override int ReadElementContentAsBase64(byte[] buffer, int index, int count)
		{
			if (this.ReadState != ReadState.Interactive)
			{
				return 0;
			}
			if (this.parsingFunction != XmlValidatingReaderImpl.ParsingFunction.InReadBinaryContent)
			{
				this.readBinaryHelper = ReadContentAsBinaryHelper.CreateOrReset(this.readBinaryHelper, this.outerReader);
			}
			this.parsingFunction = XmlValidatingReaderImpl.ParsingFunction.Read;
			int result = this.readBinaryHelper.ReadElementContentAsBase64(buffer, index, count);
			this.parsingFunction = XmlValidatingReaderImpl.ParsingFunction.InReadBinaryContent;
			return result;
		}

		// Token: 0x06000BB8 RID: 3000 RVA: 0x0004D870 File Offset: 0x0004BA70
		public override int ReadElementContentAsBinHex(byte[] buffer, int index, int count)
		{
			if (this.ReadState != ReadState.Interactive)
			{
				return 0;
			}
			if (this.parsingFunction != XmlValidatingReaderImpl.ParsingFunction.InReadBinaryContent)
			{
				this.readBinaryHelper = ReadContentAsBinaryHelper.CreateOrReset(this.readBinaryHelper, this.outerReader);
			}
			this.parsingFunction = XmlValidatingReaderImpl.ParsingFunction.Read;
			int result = this.readBinaryHelper.ReadElementContentAsBinHex(buffer, index, count);
			this.parsingFunction = XmlValidatingReaderImpl.ParsingFunction.InReadBinaryContent;
			return result;
		}

		// Token: 0x170001BB RID: 443
		// (get) Token: 0x06000BB9 RID: 3001 RVA: 0x0001222F File Offset: 0x0001042F
		public override bool CanResolveEntity
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000BBA RID: 3002 RVA: 0x0004D8C4 File Offset: 0x0004BAC4
		public override void ResolveEntity()
		{
			if (this.parsingFunction == XmlValidatingReaderImpl.ParsingFunction.ResolveEntityInternally)
			{
				this.parsingFunction = XmlValidatingReaderImpl.ParsingFunction.Read;
			}
			this.coreReader.ResolveEntity();
		}

		// Token: 0x170001BC RID: 444
		// (get) Token: 0x06000BBB RID: 3003 RVA: 0x0004D8E1 File Offset: 0x0004BAE1
		// (set) Token: 0x06000BBC RID: 3004 RVA: 0x0004D8E9 File Offset: 0x0004BAE9
		internal XmlReader OuterReader
		{
			get
			{
				return this.outerReader;
			}
			set
			{
				this.outerReader = value;
			}
		}

		// Token: 0x06000BBD RID: 3005 RVA: 0x0004D8F2 File Offset: 0x0004BAF2
		internal void MoveOffEntityReference()
		{
			if (this.outerReader.NodeType == XmlNodeType.EntityReference && this.parsingFunction != XmlValidatingReaderImpl.ParsingFunction.ResolveEntityInternally && !this.outerReader.Read())
			{
				throw new InvalidOperationException(Res.GetString("Operation is not valid due to the current state of the object."));
			}
		}

		// Token: 0x06000BBE RID: 3006 RVA: 0x0004D928 File Offset: 0x0004BB28
		public override string ReadString()
		{
			this.MoveOffEntityReference();
			return base.ReadString();
		}

		// Token: 0x06000BBF RID: 3007 RVA: 0x0001222F File Offset: 0x0001042F
		public bool HasLineInfo()
		{
			return true;
		}

		// Token: 0x170001BD RID: 445
		// (get) Token: 0x06000BC0 RID: 3008 RVA: 0x0004D936 File Offset: 0x0004BB36
		public int LineNumber
		{
			get
			{
				return ((IXmlLineInfo)this.coreReader).LineNumber;
			}
		}

		// Token: 0x170001BE RID: 446
		// (get) Token: 0x06000BC1 RID: 3009 RVA: 0x0004D948 File Offset: 0x0004BB48
		public int LinePosition
		{
			get
			{
				return ((IXmlLineInfo)this.coreReader).LinePosition;
			}
		}

		// Token: 0x06000BC2 RID: 3010 RVA: 0x0004D95A File Offset: 0x0004BB5A
		IDictionary<string, string> IXmlNamespaceResolver.GetNamespacesInScope(XmlNamespaceScope scope)
		{
			return this.GetNamespacesInScope(scope);
		}

		// Token: 0x06000BC3 RID: 3011 RVA: 0x00028DA7 File Offset: 0x00026FA7
		string IXmlNamespaceResolver.LookupNamespace(string prefix)
		{
			return this.LookupNamespace(prefix);
		}

		// Token: 0x06000BC4 RID: 3012 RVA: 0x0004D963 File Offset: 0x0004BB63
		string IXmlNamespaceResolver.LookupPrefix(string namespaceName)
		{
			return this.LookupPrefix(namespaceName);
		}

		// Token: 0x06000BC5 RID: 3013 RVA: 0x0004D96C File Offset: 0x0004BB6C
		internal IDictionary<string, string> GetNamespacesInScope(XmlNamespaceScope scope)
		{
			return this.coreReaderNSResolver.GetNamespacesInScope(scope);
		}

		// Token: 0x06000BC6 RID: 3014 RVA: 0x0004D97A File Offset: 0x0004BB7A
		internal string LookupPrefix(string namespaceName)
		{
			return this.coreReaderNSResolver.LookupPrefix(namespaceName);
		}

		// Token: 0x14000003 RID: 3
		// (add) Token: 0x06000BC7 RID: 3015 RVA: 0x0004D988 File Offset: 0x0004BB88
		// (remove) Token: 0x06000BC8 RID: 3016 RVA: 0x0004D996 File Offset: 0x0004BB96
		internal event ValidationEventHandler ValidationEventHandler
		{
			add
			{
				this.eventHandling.AddHandler(value);
			}
			remove
			{
				this.eventHandling.RemoveHandler(value);
			}
		}

		// Token: 0x170001BF RID: 447
		// (get) Token: 0x06000BC9 RID: 3017 RVA: 0x0004D9A4 File Offset: 0x0004BBA4
		internal object SchemaType
		{
			get
			{
				if (this.validationType == ValidationType.None)
				{
					return null;
				}
				XmlSchemaType xmlSchemaType = this.coreReaderImpl.InternalSchemaType as XmlSchemaType;
				if (xmlSchemaType != null && xmlSchemaType.QualifiedName.Namespace == "http://www.w3.org/2001/XMLSchema")
				{
					return xmlSchemaType.Datatype;
				}
				return this.coreReaderImpl.InternalSchemaType;
			}
		}

		// Token: 0x170001C0 RID: 448
		// (get) Token: 0x06000BCA RID: 3018 RVA: 0x0004D9F8 File Offset: 0x0004BBF8
		internal XmlReader Reader
		{
			get
			{
				return this.coreReader;
			}
		}

		// Token: 0x170001C1 RID: 449
		// (get) Token: 0x06000BCB RID: 3019 RVA: 0x0004DA00 File Offset: 0x0004BC00
		internal XmlTextReaderImpl ReaderImpl
		{
			get
			{
				return this.coreReaderImpl;
			}
		}

		// Token: 0x170001C2 RID: 450
		// (get) Token: 0x06000BCC RID: 3020 RVA: 0x0004DA08 File Offset: 0x0004BC08
		// (set) Token: 0x06000BCD RID: 3021 RVA: 0x0004DA10 File Offset: 0x0004BC10
		internal ValidationType ValidationType
		{
			get
			{
				return this.validationType;
			}
			set
			{
				if (this.ReadState != ReadState.Initial)
				{
					throw new InvalidOperationException(Res.GetString("Operation is not valid due to the current state of the object."));
				}
				this.validationType = value;
				this.SetupValidation(value);
			}
		}

		// Token: 0x170001C3 RID: 451
		// (get) Token: 0x06000BCE RID: 3022 RVA: 0x0004DA38 File Offset: 0x0004BC38
		internal XmlSchemaCollection Schemas
		{
			get
			{
				return this.schemaCollection;
			}
		}

		// Token: 0x170001C4 RID: 452
		// (get) Token: 0x06000BCF RID: 3023 RVA: 0x0004DA40 File Offset: 0x0004BC40
		// (set) Token: 0x06000BD0 RID: 3024 RVA: 0x0004DA4D File Offset: 0x0004BC4D
		internal EntityHandling EntityHandling
		{
			get
			{
				return this.coreReaderImpl.EntityHandling;
			}
			set
			{
				this.coreReaderImpl.EntityHandling = value;
			}
		}

		// Token: 0x170001C5 RID: 453
		// (set) Token: 0x06000BD1 RID: 3025 RVA: 0x0004DA5B File Offset: 0x0004BC5B
		internal XmlResolver XmlResolver
		{
			set
			{
				this.coreReaderImpl.XmlResolver = value;
				this.validator.XmlResolver = value;
				this.schemaCollection.XmlResolver = value;
			}
		}

		// Token: 0x170001C6 RID: 454
		// (get) Token: 0x06000BD2 RID: 3026 RVA: 0x0004DA81 File Offset: 0x0004BC81
		// (set) Token: 0x06000BD3 RID: 3027 RVA: 0x0004DA8E File Offset: 0x0004BC8E
		internal bool Namespaces
		{
			get
			{
				return this.coreReaderImpl.Namespaces;
			}
			set
			{
				this.coreReaderImpl.Namespaces = value;
			}
		}

		// Token: 0x06000BD4 RID: 3028 RVA: 0x0004DA9C File Offset: 0x0004BC9C
		public object ReadTypedValue()
		{
			if (this.validationType == ValidationType.None)
			{
				return null;
			}
			XmlNodeType nodeType = this.outerReader.NodeType;
			if (nodeType != XmlNodeType.Element)
			{
				if (nodeType == XmlNodeType.Attribute)
				{
					return this.coreReaderImpl.InternalTypedValue;
				}
				if (nodeType == XmlNodeType.EndElement)
				{
					return null;
				}
				if (this.coreReaderImpl.V1Compat)
				{
					return null;
				}
				return this.Value;
			}
			else
			{
				if (this.SchemaType == null)
				{
					return null;
				}
				if (((this.SchemaType is XmlSchemaDatatype) ? ((XmlSchemaDatatype)this.SchemaType) : ((XmlSchemaType)this.SchemaType).Datatype) != null)
				{
					if (!this.outerReader.IsEmptyElement)
					{
						while (this.outerReader.Read())
						{
							XmlNodeType nodeType2 = this.outerReader.NodeType;
							if (nodeType2 != XmlNodeType.CDATA && nodeType2 != XmlNodeType.Text && nodeType2 != XmlNodeType.Whitespace && nodeType2 != XmlNodeType.SignificantWhitespace && nodeType2 != XmlNodeType.Comment && nodeType2 != XmlNodeType.ProcessingInstruction)
							{
								if (this.outerReader.NodeType != XmlNodeType.EndElement)
								{
									throw new XmlException("'{0}' is an invalid XmlNodeType.", this.outerReader.NodeType.ToString());
								}
								goto IL_F3;
							}
						}
						throw new InvalidOperationException(Res.GetString("Operation is not valid due to the current state of the object."));
					}
					IL_F3:
					return this.coreReaderImpl.InternalTypedValue;
				}
				return null;
			}
		}

		// Token: 0x06000BD5 RID: 3029 RVA: 0x0004DBC4 File Offset: 0x0004BDC4
		private void ParseDtdFromParserContext()
		{
			if (this.parserContext.DocTypeName == null || this.parserContext.DocTypeName.Length == 0)
			{
				return;
			}
			IDtdParser dtdParser = DtdParser.Create();
			XmlTextReaderImpl.DtdParserProxy adapter = new XmlTextReaderImpl.DtdParserProxy(this.coreReaderImpl);
			IDtdInfo dtdInfo = dtdParser.ParseFreeFloatingDtd(this.parserContext.BaseURI, this.parserContext.DocTypeName, this.parserContext.PublicId, this.parserContext.SystemId, this.parserContext.InternalSubset, adapter);
			this.coreReaderImpl.SetDtdInfo(dtdInfo);
			this.ValidateDtd();
		}

		// Token: 0x06000BD6 RID: 3030 RVA: 0x0004DC54 File Offset: 0x0004BE54
		private void ValidateDtd()
		{
			IDtdInfo dtdInfo = this.coreReaderImpl.DtdInfo;
			if (dtdInfo != null)
			{
				switch (this.validationType)
				{
				case ValidationType.None:
				case ValidationType.DTD:
					break;
				case ValidationType.Auto:
					this.SetupValidation(ValidationType.DTD);
					break;
				default:
					return;
				}
				this.validator.DtdInfo = dtdInfo;
			}
		}

		// Token: 0x06000BD7 RID: 3031 RVA: 0x0004DCA0 File Offset: 0x0004BEA0
		private void ResolveEntityInternally()
		{
			int depth = this.coreReader.Depth;
			this.outerReader.ResolveEntity();
			while (this.outerReader.Read() && this.coreReader.Depth > depth)
			{
			}
		}

		// Token: 0x06000BD8 RID: 3032 RVA: 0x0004DCE0 File Offset: 0x0004BEE0
		private void SetupValidation(ValidationType valType)
		{
			this.validator = BaseValidator.CreateInstance(valType, this, this.schemaCollection, this.eventHandling, this.processIdentityConstraints);
			XmlResolver resolver = this.GetResolver();
			this.validator.XmlResolver = resolver;
			if (this.outerReader.BaseURI.Length > 0)
			{
				this.validator.BaseUri = ((resolver == null) ? new Uri(this.outerReader.BaseURI, UriKind.RelativeOrAbsolute) : resolver.ResolveUri(null, this.outerReader.BaseURI));
			}
			this.coreReaderImpl.ValidationEventHandling = ((this.validationType == ValidationType.None) ? null : this.eventHandling);
		}

		// Token: 0x06000BD9 RID: 3033 RVA: 0x0004DD84 File Offset: 0x0004BF84
		private XmlResolver GetResolver()
		{
			XmlResolver resolver = this.coreReaderImpl.GetResolver();
			if (resolver == null && !this.coreReaderImpl.IsResolverSet && !XmlReaderSettings.EnableLegacyXmlSettings())
			{
				if (XmlValidatingReaderImpl.s_tempResolver == null)
				{
					XmlValidatingReaderImpl.s_tempResolver = new XmlUrlResolver();
				}
				return XmlValidatingReaderImpl.s_tempResolver;
			}
			return resolver;
		}

		// Token: 0x06000BDA RID: 3034 RVA: 0x0004DDCC File Offset: 0x0004BFCC
		private void ProcessCoreReaderEvent()
		{
			XmlNodeType nodeType = this.coreReader.NodeType;
			if (nodeType != XmlNodeType.EntityReference)
			{
				if (nodeType == XmlNodeType.DocumentType)
				{
					this.ValidateDtd();
					return;
				}
				if (nodeType == XmlNodeType.Whitespace && (this.coreReader.Depth > 0 || this.coreReaderImpl.FragmentType != XmlNodeType.Document) && this.validator.PreserveWhitespace)
				{
					this.coreReaderImpl.ChangeCurrentNodeType(XmlNodeType.SignificantWhitespace);
				}
			}
			else
			{
				this.parsingFunction = XmlValidatingReaderImpl.ParsingFunction.ResolveEntityInternally;
			}
			this.coreReaderImpl.InternalSchemaType = null;
			this.coreReaderImpl.InternalTypedValue = null;
			this.validator.Validate();
		}

		// Token: 0x06000BDB RID: 3035 RVA: 0x0004DE5D File Offset: 0x0004C05D
		internal void Close(bool closeStream)
		{
			this.coreReaderImpl.Close(closeStream);
			this.parsingFunction = XmlValidatingReaderImpl.ParsingFunction.ReaderClosed;
		}

		// Token: 0x170001C7 RID: 455
		// (get) Token: 0x06000BDC RID: 3036 RVA: 0x0004DE72 File Offset: 0x0004C072
		// (set) Token: 0x06000BDD RID: 3037 RVA: 0x0004DE7A File Offset: 0x0004C07A
		internal BaseValidator Validator
		{
			get
			{
				return this.validator;
			}
			set
			{
				this.validator = value;
			}
		}

		// Token: 0x170001C8 RID: 456
		// (get) Token: 0x06000BDE RID: 3038 RVA: 0x0004DE83 File Offset: 0x0004C083
		internal override XmlNamespaceManager NamespaceManager
		{
			get
			{
				return this.coreReaderImpl.NamespaceManager;
			}
		}

		// Token: 0x170001C9 RID: 457
		// (get) Token: 0x06000BDF RID: 3039 RVA: 0x0004DE90 File Offset: 0x0004C090
		internal bool StandAlone
		{
			get
			{
				return this.coreReaderImpl.StandAlone;
			}
		}

		// Token: 0x170001CA RID: 458
		// (set) Token: 0x06000BE0 RID: 3040 RVA: 0x0004DE9D File Offset: 0x0004C09D
		internal object SchemaTypeObject
		{
			set
			{
				this.coreReaderImpl.InternalSchemaType = value;
			}
		}

		// Token: 0x170001CB RID: 459
		// (get) Token: 0x06000BE1 RID: 3041 RVA: 0x0004DEAB File Offset: 0x0004C0AB
		// (set) Token: 0x06000BE2 RID: 3042 RVA: 0x0004DEB8 File Offset: 0x0004C0B8
		internal object TypedValueObject
		{
			get
			{
				return this.coreReaderImpl.InternalTypedValue;
			}
			set
			{
				this.coreReaderImpl.InternalTypedValue = value;
			}
		}

		// Token: 0x170001CC RID: 460
		// (get) Token: 0x06000BE3 RID: 3043 RVA: 0x0004DEC6 File Offset: 0x0004C0C6
		internal bool Normalization
		{
			get
			{
				return this.coreReaderImpl.Normalization;
			}
		}

		// Token: 0x06000BE4 RID: 3044 RVA: 0x0004DED3 File Offset: 0x0004C0D3
		internal bool AddDefaultAttribute(SchemaAttDef attdef)
		{
			return this.coreReaderImpl.AddDefaultAttributeNonDtd(attdef);
		}

		// Token: 0x170001CD RID: 461
		// (get) Token: 0x06000BE5 RID: 3045 RVA: 0x0004DEE1 File Offset: 0x0004C0E1
		internal override IDtdInfo DtdInfo
		{
			get
			{
				return this.coreReaderImpl.DtdInfo;
			}
		}

		// Token: 0x06000BE6 RID: 3046 RVA: 0x0004DEF0 File Offset: 0x0004C0F0
		internal void ValidateDefaultAttributeOnUse(IDtdDefaultAttributeInfo defaultAttribute, XmlTextReaderImpl coreReader)
		{
			SchemaAttDef schemaAttDef = defaultAttribute as SchemaAttDef;
			if (schemaAttDef == null)
			{
				return;
			}
			if (!schemaAttDef.DefaultValueChecked)
			{
				SchemaInfo schemaInfo = coreReader.DtdInfo as SchemaInfo;
				if (schemaInfo == null)
				{
					return;
				}
				DtdValidator.CheckDefaultValue(schemaAttDef, schemaInfo, this.eventHandling, coreReader.BaseURI);
			}
		}

		// Token: 0x06000BE7 RID: 3047 RVA: 0x0004DF33 File Offset: 0x0004C133
		public override Task<string> GetValueAsync()
		{
			return this.coreReader.GetValueAsync();
		}

		// Token: 0x06000BE8 RID: 3048 RVA: 0x0004DF40 File Offset: 0x0004C140
		public override Task<bool> ReadAsync()
		{
			XmlValidatingReaderImpl.<ReadAsync>d__145 <ReadAsync>d__;
			<ReadAsync>d__.<>4__this = this;
			<ReadAsync>d__.<>t__builder = AsyncTaskMethodBuilder<bool>.Create();
			<ReadAsync>d__.<>1__state = -1;
			<ReadAsync>d__.<>t__builder.Start<XmlValidatingReaderImpl.<ReadAsync>d__145>(ref <ReadAsync>d__);
			return <ReadAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000BE9 RID: 3049 RVA: 0x0004DF84 File Offset: 0x0004C184
		public override Task<int> ReadContentAsBase64Async(byte[] buffer, int index, int count)
		{
			XmlValidatingReaderImpl.<ReadContentAsBase64Async>d__146 <ReadContentAsBase64Async>d__;
			<ReadContentAsBase64Async>d__.<>4__this = this;
			<ReadContentAsBase64Async>d__.buffer = buffer;
			<ReadContentAsBase64Async>d__.index = index;
			<ReadContentAsBase64Async>d__.count = count;
			<ReadContentAsBase64Async>d__.<>t__builder = AsyncTaskMethodBuilder<int>.Create();
			<ReadContentAsBase64Async>d__.<>1__state = -1;
			<ReadContentAsBase64Async>d__.<>t__builder.Start<XmlValidatingReaderImpl.<ReadContentAsBase64Async>d__146>(ref <ReadContentAsBase64Async>d__);
			return <ReadContentAsBase64Async>d__.<>t__builder.Task;
		}

		// Token: 0x06000BEA RID: 3050 RVA: 0x0004DFE0 File Offset: 0x0004C1E0
		public override Task<int> ReadContentAsBinHexAsync(byte[] buffer, int index, int count)
		{
			XmlValidatingReaderImpl.<ReadContentAsBinHexAsync>d__147 <ReadContentAsBinHexAsync>d__;
			<ReadContentAsBinHexAsync>d__.<>4__this = this;
			<ReadContentAsBinHexAsync>d__.buffer = buffer;
			<ReadContentAsBinHexAsync>d__.index = index;
			<ReadContentAsBinHexAsync>d__.count = count;
			<ReadContentAsBinHexAsync>d__.<>t__builder = AsyncTaskMethodBuilder<int>.Create();
			<ReadContentAsBinHexAsync>d__.<>1__state = -1;
			<ReadContentAsBinHexAsync>d__.<>t__builder.Start<XmlValidatingReaderImpl.<ReadContentAsBinHexAsync>d__147>(ref <ReadContentAsBinHexAsync>d__);
			return <ReadContentAsBinHexAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000BEB RID: 3051 RVA: 0x0004E03C File Offset: 0x0004C23C
		public override Task<int> ReadElementContentAsBase64Async(byte[] buffer, int index, int count)
		{
			XmlValidatingReaderImpl.<ReadElementContentAsBase64Async>d__148 <ReadElementContentAsBase64Async>d__;
			<ReadElementContentAsBase64Async>d__.<>4__this = this;
			<ReadElementContentAsBase64Async>d__.buffer = buffer;
			<ReadElementContentAsBase64Async>d__.index = index;
			<ReadElementContentAsBase64Async>d__.count = count;
			<ReadElementContentAsBase64Async>d__.<>t__builder = AsyncTaskMethodBuilder<int>.Create();
			<ReadElementContentAsBase64Async>d__.<>1__state = -1;
			<ReadElementContentAsBase64Async>d__.<>t__builder.Start<XmlValidatingReaderImpl.<ReadElementContentAsBase64Async>d__148>(ref <ReadElementContentAsBase64Async>d__);
			return <ReadElementContentAsBase64Async>d__.<>t__builder.Task;
		}

		// Token: 0x06000BEC RID: 3052 RVA: 0x0004E098 File Offset: 0x0004C298
		public override Task<int> ReadElementContentAsBinHexAsync(byte[] buffer, int index, int count)
		{
			XmlValidatingReaderImpl.<ReadElementContentAsBinHexAsync>d__149 <ReadElementContentAsBinHexAsync>d__;
			<ReadElementContentAsBinHexAsync>d__.<>4__this = this;
			<ReadElementContentAsBinHexAsync>d__.buffer = buffer;
			<ReadElementContentAsBinHexAsync>d__.index = index;
			<ReadElementContentAsBinHexAsync>d__.count = count;
			<ReadElementContentAsBinHexAsync>d__.<>t__builder = AsyncTaskMethodBuilder<int>.Create();
			<ReadElementContentAsBinHexAsync>d__.<>1__state = -1;
			<ReadElementContentAsBinHexAsync>d__.<>t__builder.Start<XmlValidatingReaderImpl.<ReadElementContentAsBinHexAsync>d__149>(ref <ReadElementContentAsBinHexAsync>d__);
			return <ReadElementContentAsBinHexAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000BED RID: 3053 RVA: 0x0004E0F4 File Offset: 0x0004C2F4
		internal Task MoveOffEntityReferenceAsync()
		{
			XmlValidatingReaderImpl.<MoveOffEntityReferenceAsync>d__150 <MoveOffEntityReferenceAsync>d__;
			<MoveOffEntityReferenceAsync>d__.<>4__this = this;
			<MoveOffEntityReferenceAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<MoveOffEntityReferenceAsync>d__.<>1__state = -1;
			<MoveOffEntityReferenceAsync>d__.<>t__builder.Start<XmlValidatingReaderImpl.<MoveOffEntityReferenceAsync>d__150>(ref <MoveOffEntityReferenceAsync>d__);
			return <MoveOffEntityReferenceAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000BEE RID: 3054 RVA: 0x0004E138 File Offset: 0x0004C338
		public Task<object> ReadTypedValueAsync()
		{
			XmlValidatingReaderImpl.<ReadTypedValueAsync>d__151 <ReadTypedValueAsync>d__;
			<ReadTypedValueAsync>d__.<>4__this = this;
			<ReadTypedValueAsync>d__.<>t__builder = AsyncTaskMethodBuilder<object>.Create();
			<ReadTypedValueAsync>d__.<>1__state = -1;
			<ReadTypedValueAsync>d__.<>t__builder.Start<XmlValidatingReaderImpl.<ReadTypedValueAsync>d__151>(ref <ReadTypedValueAsync>d__);
			return <ReadTypedValueAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000BEF RID: 3055 RVA: 0x0004E17C File Offset: 0x0004C37C
		private Task ParseDtdFromParserContextAsync()
		{
			XmlValidatingReaderImpl.<ParseDtdFromParserContextAsync>d__152 <ParseDtdFromParserContextAsync>d__;
			<ParseDtdFromParserContextAsync>d__.<>4__this = this;
			<ParseDtdFromParserContextAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<ParseDtdFromParserContextAsync>d__.<>1__state = -1;
			<ParseDtdFromParserContextAsync>d__.<>t__builder.Start<XmlValidatingReaderImpl.<ParseDtdFromParserContextAsync>d__152>(ref <ParseDtdFromParserContextAsync>d__);
			return <ParseDtdFromParserContextAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000BF0 RID: 3056 RVA: 0x0004E1C0 File Offset: 0x0004C3C0
		private Task ResolveEntityInternallyAsync()
		{
			XmlValidatingReaderImpl.<ResolveEntityInternallyAsync>d__153 <ResolveEntityInternallyAsync>d__;
			<ResolveEntityInternallyAsync>d__.<>4__this = this;
			<ResolveEntityInternallyAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<ResolveEntityInternallyAsync>d__.<>1__state = -1;
			<ResolveEntityInternallyAsync>d__.<>t__builder.Start<XmlValidatingReaderImpl.<ResolveEntityInternallyAsync>d__153>(ref <ResolveEntityInternallyAsync>d__);
			return <ResolveEntityInternallyAsync>d__.<>t__builder.Task;
		}

		// Token: 0x04000D24 RID: 3364
		private XmlReader coreReader;

		// Token: 0x04000D25 RID: 3365
		private XmlTextReaderImpl coreReaderImpl;

		// Token: 0x04000D26 RID: 3366
		private IXmlNamespaceResolver coreReaderNSResolver;

		// Token: 0x04000D27 RID: 3367
		private ValidationType validationType;

		// Token: 0x04000D28 RID: 3368
		private BaseValidator validator;

		// Token: 0x04000D29 RID: 3369
		private XmlSchemaCollection schemaCollection;

		// Token: 0x04000D2A RID: 3370
		private bool processIdentityConstraints;

		// Token: 0x04000D2B RID: 3371
		private XmlValidatingReaderImpl.ParsingFunction parsingFunction = XmlValidatingReaderImpl.ParsingFunction.Init;

		// Token: 0x04000D2C RID: 3372
		private XmlValidatingReaderImpl.ValidationEventHandling eventHandling;

		// Token: 0x04000D2D RID: 3373
		private XmlParserContext parserContext;

		// Token: 0x04000D2E RID: 3374
		private ReadContentAsBinaryHelper readBinaryHelper;

		// Token: 0x04000D2F RID: 3375
		private XmlReader outerReader;

		// Token: 0x04000D30 RID: 3376
		private static XmlResolver s_tempResolver;

		// Token: 0x02000141 RID: 321
		private enum ParsingFunction
		{
			// Token: 0x04000D32 RID: 3378
			Read,
			// Token: 0x04000D33 RID: 3379
			Init,
			// Token: 0x04000D34 RID: 3380
			ParseDtdFromContext,
			// Token: 0x04000D35 RID: 3381
			ResolveEntityInternally,
			// Token: 0x04000D36 RID: 3382
			InReadBinaryContent,
			// Token: 0x04000D37 RID: 3383
			ReaderClosed,
			// Token: 0x04000D38 RID: 3384
			Error,
			// Token: 0x04000D39 RID: 3385
			None
		}

		// Token: 0x02000142 RID: 322
		internal class ValidationEventHandling : IValidationEventHandling
		{
			// Token: 0x06000BF1 RID: 3057 RVA: 0x0004E203 File Offset: 0x0004C403
			internal ValidationEventHandling(XmlValidatingReaderImpl reader)
			{
				this.reader = reader;
			}

			// Token: 0x170001CE RID: 462
			// (get) Token: 0x06000BF2 RID: 3058 RVA: 0x0004E212 File Offset: 0x0004C412
			object IValidationEventHandling.EventHandler
			{
				get
				{
					return this.eventHandler;
				}
			}

			// Token: 0x06000BF3 RID: 3059 RVA: 0x0004E21A File Offset: 0x0004C41A
			void IValidationEventHandling.SendEvent(Exception exception, XmlSeverityType severity)
			{
				if (this.eventHandler != null)
				{
					this.eventHandler(this.reader, new ValidationEventArgs((XmlSchemaException)exception, severity));
					return;
				}
				if (this.reader.ValidationType != ValidationType.None && severity == XmlSeverityType.Error)
				{
					throw exception;
				}
			}

			// Token: 0x06000BF4 RID: 3060 RVA: 0x0004E254 File Offset: 0x0004C454
			internal void AddHandler(ValidationEventHandler handler)
			{
				this.eventHandler = (ValidationEventHandler)Delegate.Combine(this.eventHandler, handler);
			}

			// Token: 0x06000BF5 RID: 3061 RVA: 0x0004E26D File Offset: 0x0004C46D
			internal void RemoveHandler(ValidationEventHandler handler)
			{
				this.eventHandler = (ValidationEventHandler)Delegate.Remove(this.eventHandler, handler);
			}

			// Token: 0x04000D3A RID: 3386
			private XmlValidatingReaderImpl reader;

			// Token: 0x04000D3B RID: 3387
			private ValidationEventHandler eventHandler;
		}

		// Token: 0x02000143 RID: 323
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <ReadAsync>d__145 : IAsyncStateMachine
		{
			// Token: 0x06000BF6 RID: 3062 RVA: 0x0004E288 File Offset: 0x0004C488
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				XmlValidatingReaderImpl xmlValidatingReaderImpl = this.<>4__this;
				bool result;
				try
				{
					ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter awaiter;
					ConfiguredTaskAwaitable.ConfiguredTaskAwaiter awaiter2;
					switch (num)
					{
					case 0:
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_B8;
					case 1:
						awaiter2 = this.<>u__2;
						this.<>u__2 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_148;
					case 2:
						awaiter2 = this.<>u__2;
						this.<>u__2 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_1E8;
					case 3:
						awaiter2 = this.<>u__2;
						this.<>u__2 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_25E;
					default:
						switch (xmlValidatingReaderImpl.parsingFunction)
						{
						case XmlValidatingReaderImpl.ParsingFunction.Read:
							break;
						case XmlValidatingReaderImpl.ParsingFunction.Init:
							xmlValidatingReaderImpl.parsingFunction = XmlValidatingReaderImpl.ParsingFunction.Read;
							if (xmlValidatingReaderImpl.coreReader.ReadState == ReadState.Interactive)
							{
								xmlValidatingReaderImpl.ProcessCoreReaderEvent();
								result = true;
								goto IL_287;
							}
							break;
						case XmlValidatingReaderImpl.ParsingFunction.ParseDtdFromContext:
							xmlValidatingReaderImpl.parsingFunction = XmlValidatingReaderImpl.ParsingFunction.Read;
							awaiter2 = xmlValidatingReaderImpl.ParseDtdFromParserContextAsync().ConfigureAwait(false).GetAwaiter();
							if (!awaiter2.IsCompleted)
							{
								this.<>1__state = 1;
								this.<>u__2 = awaiter2;
								this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlValidatingReaderImpl.<ReadAsync>d__145>(ref awaiter2, ref this);
								return;
							}
							goto IL_148;
						case XmlValidatingReaderImpl.ParsingFunction.ResolveEntityInternally:
							xmlValidatingReaderImpl.parsingFunction = XmlValidatingReaderImpl.ParsingFunction.Read;
							awaiter2 = xmlValidatingReaderImpl.ResolveEntityInternallyAsync().ConfigureAwait(false).GetAwaiter();
							if (!awaiter2.IsCompleted)
							{
								this.<>1__state = 2;
								this.<>u__2 = awaiter2;
								this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlValidatingReaderImpl.<ReadAsync>d__145>(ref awaiter2, ref this);
								return;
							}
							goto IL_1E8;
						case XmlValidatingReaderImpl.ParsingFunction.InReadBinaryContent:
							xmlValidatingReaderImpl.parsingFunction = XmlValidatingReaderImpl.ParsingFunction.Read;
							awaiter2 = xmlValidatingReaderImpl.readBinaryHelper.FinishAsync().ConfigureAwait(false).GetAwaiter();
							if (!awaiter2.IsCompleted)
							{
								this.<>1__state = 3;
								this.<>u__2 = awaiter2;
								this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlValidatingReaderImpl.<ReadAsync>d__145>(ref awaiter2, ref this);
								return;
							}
							goto IL_25E;
						case XmlValidatingReaderImpl.ParsingFunction.ReaderClosed:
						case XmlValidatingReaderImpl.ParsingFunction.Error:
							result = false;
							goto IL_287;
						default:
							result = false;
							goto IL_287;
						}
						break;
					}
					IL_52:
					awaiter = xmlValidatingReaderImpl.coreReader.ReadAsync().ConfigureAwait(false).GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						this.<>1__state = 0;
						this.<>u__1 = awaiter;
						this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter, XmlValidatingReaderImpl.<ReadAsync>d__145>(ref awaiter, ref this);
						return;
					}
					IL_B8:
					if (awaiter.GetResult())
					{
						xmlValidatingReaderImpl.ProcessCoreReaderEvent();
						result = true;
						goto IL_287;
					}
					xmlValidatingReaderImpl.validator.CompleteValidation();
					result = false;
					goto IL_287;
					IL_148:
					awaiter2.GetResult();
					goto IL_52;
					IL_1E8:
					awaiter2.GetResult();
					goto IL_52;
					IL_25E:
					awaiter2.GetResult();
					goto IL_52;
				}
				catch (Exception exception)
				{
					this.<>1__state = -2;
					this.<>t__builder.SetException(exception);
					return;
				}
				IL_287:
				this.<>1__state = -2;
				this.<>t__builder.SetResult(result);
			}

			// Token: 0x06000BF7 RID: 3063 RVA: 0x0004E54C File Offset: 0x0004C74C
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04000D3C RID: 3388
			public int <>1__state;

			// Token: 0x04000D3D RID: 3389
			public AsyncTaskMethodBuilder<bool> <>t__builder;

			// Token: 0x04000D3E RID: 3390
			public XmlValidatingReaderImpl <>4__this;

			// Token: 0x04000D3F RID: 3391
			private ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter <>u__1;

			// Token: 0x04000D40 RID: 3392
			private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter <>u__2;
		}

		// Token: 0x02000144 RID: 324
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <ReadContentAsBase64Async>d__146 : IAsyncStateMachine
		{
			// Token: 0x06000BF8 RID: 3064 RVA: 0x0004E55C File Offset: 0x0004C75C
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				XmlValidatingReaderImpl xmlValidatingReaderImpl = this.<>4__this;
				int result;
				try
				{
					ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter awaiter;
					if (num != 0)
					{
						if (xmlValidatingReaderImpl.ReadState != ReadState.Interactive)
						{
							result = 0;
							goto IL_E7;
						}
						if (xmlValidatingReaderImpl.parsingFunction != XmlValidatingReaderImpl.ParsingFunction.InReadBinaryContent)
						{
							xmlValidatingReaderImpl.readBinaryHelper = ReadContentAsBinaryHelper.CreateOrReset(xmlValidatingReaderImpl.readBinaryHelper, xmlValidatingReaderImpl.outerReader);
						}
						xmlValidatingReaderImpl.parsingFunction = XmlValidatingReaderImpl.ParsingFunction.Read;
						awaiter = xmlValidatingReaderImpl.readBinaryHelper.ReadContentAsBase64Async(this.buffer, this.index, this.count).ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 0;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter, XmlValidatingReaderImpl.<ReadContentAsBase64Async>d__146>(ref awaiter, ref this);
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
					xmlValidatingReaderImpl.parsingFunction = XmlValidatingReaderImpl.ParsingFunction.InReadBinaryContent;
					result = result2;
				}
				catch (Exception exception)
				{
					this.<>1__state = -2;
					this.<>t__builder.SetException(exception);
					return;
				}
				IL_E7:
				this.<>1__state = -2;
				this.<>t__builder.SetResult(result);
			}

			// Token: 0x06000BF9 RID: 3065 RVA: 0x0004E674 File Offset: 0x0004C874
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04000D41 RID: 3393
			public int <>1__state;

			// Token: 0x04000D42 RID: 3394
			public AsyncTaskMethodBuilder<int> <>t__builder;

			// Token: 0x04000D43 RID: 3395
			public XmlValidatingReaderImpl <>4__this;

			// Token: 0x04000D44 RID: 3396
			public byte[] buffer;

			// Token: 0x04000D45 RID: 3397
			public int index;

			// Token: 0x04000D46 RID: 3398
			public int count;

			// Token: 0x04000D47 RID: 3399
			private ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter <>u__1;
		}

		// Token: 0x02000145 RID: 325
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <ReadContentAsBinHexAsync>d__147 : IAsyncStateMachine
		{
			// Token: 0x06000BFA RID: 3066 RVA: 0x0004E684 File Offset: 0x0004C884
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				XmlValidatingReaderImpl xmlValidatingReaderImpl = this.<>4__this;
				int result;
				try
				{
					ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter awaiter;
					if (num != 0)
					{
						if (xmlValidatingReaderImpl.ReadState != ReadState.Interactive)
						{
							result = 0;
							goto IL_E7;
						}
						if (xmlValidatingReaderImpl.parsingFunction != XmlValidatingReaderImpl.ParsingFunction.InReadBinaryContent)
						{
							xmlValidatingReaderImpl.readBinaryHelper = ReadContentAsBinaryHelper.CreateOrReset(xmlValidatingReaderImpl.readBinaryHelper, xmlValidatingReaderImpl.outerReader);
						}
						xmlValidatingReaderImpl.parsingFunction = XmlValidatingReaderImpl.ParsingFunction.Read;
						awaiter = xmlValidatingReaderImpl.readBinaryHelper.ReadContentAsBinHexAsync(this.buffer, this.index, this.count).ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 0;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter, XmlValidatingReaderImpl.<ReadContentAsBinHexAsync>d__147>(ref awaiter, ref this);
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
					xmlValidatingReaderImpl.parsingFunction = XmlValidatingReaderImpl.ParsingFunction.InReadBinaryContent;
					result = result2;
				}
				catch (Exception exception)
				{
					this.<>1__state = -2;
					this.<>t__builder.SetException(exception);
					return;
				}
				IL_E7:
				this.<>1__state = -2;
				this.<>t__builder.SetResult(result);
			}

			// Token: 0x06000BFB RID: 3067 RVA: 0x0004E79C File Offset: 0x0004C99C
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04000D48 RID: 3400
			public int <>1__state;

			// Token: 0x04000D49 RID: 3401
			public AsyncTaskMethodBuilder<int> <>t__builder;

			// Token: 0x04000D4A RID: 3402
			public XmlValidatingReaderImpl <>4__this;

			// Token: 0x04000D4B RID: 3403
			public byte[] buffer;

			// Token: 0x04000D4C RID: 3404
			public int index;

			// Token: 0x04000D4D RID: 3405
			public int count;

			// Token: 0x04000D4E RID: 3406
			private ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter <>u__1;
		}

		// Token: 0x02000146 RID: 326
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <ReadElementContentAsBase64Async>d__148 : IAsyncStateMachine
		{
			// Token: 0x06000BFC RID: 3068 RVA: 0x0004E7AC File Offset: 0x0004C9AC
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				XmlValidatingReaderImpl xmlValidatingReaderImpl = this.<>4__this;
				int result;
				try
				{
					ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter awaiter;
					if (num != 0)
					{
						if (xmlValidatingReaderImpl.ReadState != ReadState.Interactive)
						{
							result = 0;
							goto IL_E7;
						}
						if (xmlValidatingReaderImpl.parsingFunction != XmlValidatingReaderImpl.ParsingFunction.InReadBinaryContent)
						{
							xmlValidatingReaderImpl.readBinaryHelper = ReadContentAsBinaryHelper.CreateOrReset(xmlValidatingReaderImpl.readBinaryHelper, xmlValidatingReaderImpl.outerReader);
						}
						xmlValidatingReaderImpl.parsingFunction = XmlValidatingReaderImpl.ParsingFunction.Read;
						awaiter = xmlValidatingReaderImpl.readBinaryHelper.ReadElementContentAsBase64Async(this.buffer, this.index, this.count).ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 0;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter, XmlValidatingReaderImpl.<ReadElementContentAsBase64Async>d__148>(ref awaiter, ref this);
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
					xmlValidatingReaderImpl.parsingFunction = XmlValidatingReaderImpl.ParsingFunction.InReadBinaryContent;
					result = result2;
				}
				catch (Exception exception)
				{
					this.<>1__state = -2;
					this.<>t__builder.SetException(exception);
					return;
				}
				IL_E7:
				this.<>1__state = -2;
				this.<>t__builder.SetResult(result);
			}

			// Token: 0x06000BFD RID: 3069 RVA: 0x0004E8C4 File Offset: 0x0004CAC4
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04000D4F RID: 3407
			public int <>1__state;

			// Token: 0x04000D50 RID: 3408
			public AsyncTaskMethodBuilder<int> <>t__builder;

			// Token: 0x04000D51 RID: 3409
			public XmlValidatingReaderImpl <>4__this;

			// Token: 0x04000D52 RID: 3410
			public byte[] buffer;

			// Token: 0x04000D53 RID: 3411
			public int index;

			// Token: 0x04000D54 RID: 3412
			public int count;

			// Token: 0x04000D55 RID: 3413
			private ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter <>u__1;
		}

		// Token: 0x02000147 RID: 327
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <ReadElementContentAsBinHexAsync>d__149 : IAsyncStateMachine
		{
			// Token: 0x06000BFE RID: 3070 RVA: 0x0004E8D4 File Offset: 0x0004CAD4
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				XmlValidatingReaderImpl xmlValidatingReaderImpl = this.<>4__this;
				int result;
				try
				{
					ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter awaiter;
					if (num != 0)
					{
						if (xmlValidatingReaderImpl.ReadState != ReadState.Interactive)
						{
							result = 0;
							goto IL_E7;
						}
						if (xmlValidatingReaderImpl.parsingFunction != XmlValidatingReaderImpl.ParsingFunction.InReadBinaryContent)
						{
							xmlValidatingReaderImpl.readBinaryHelper = ReadContentAsBinaryHelper.CreateOrReset(xmlValidatingReaderImpl.readBinaryHelper, xmlValidatingReaderImpl.outerReader);
						}
						xmlValidatingReaderImpl.parsingFunction = XmlValidatingReaderImpl.ParsingFunction.Read;
						awaiter = xmlValidatingReaderImpl.readBinaryHelper.ReadElementContentAsBinHexAsync(this.buffer, this.index, this.count).ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 0;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter, XmlValidatingReaderImpl.<ReadElementContentAsBinHexAsync>d__149>(ref awaiter, ref this);
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
					xmlValidatingReaderImpl.parsingFunction = XmlValidatingReaderImpl.ParsingFunction.InReadBinaryContent;
					result = result2;
				}
				catch (Exception exception)
				{
					this.<>1__state = -2;
					this.<>t__builder.SetException(exception);
					return;
				}
				IL_E7:
				this.<>1__state = -2;
				this.<>t__builder.SetResult(result);
			}

			// Token: 0x06000BFF RID: 3071 RVA: 0x0004E9EC File Offset: 0x0004CBEC
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04000D56 RID: 3414
			public int <>1__state;

			// Token: 0x04000D57 RID: 3415
			public AsyncTaskMethodBuilder<int> <>t__builder;

			// Token: 0x04000D58 RID: 3416
			public XmlValidatingReaderImpl <>4__this;

			// Token: 0x04000D59 RID: 3417
			public byte[] buffer;

			// Token: 0x04000D5A RID: 3418
			public int index;

			// Token: 0x04000D5B RID: 3419
			public int count;

			// Token: 0x04000D5C RID: 3420
			private ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter <>u__1;
		}

		// Token: 0x02000148 RID: 328
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <MoveOffEntityReferenceAsync>d__150 : IAsyncStateMachine
		{
			// Token: 0x06000C00 RID: 3072 RVA: 0x0004E9FC File Offset: 0x0004CBFC
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				XmlValidatingReaderImpl xmlValidatingReaderImpl = this.<>4__this;
				try
				{
					ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter awaiter;
					if (num != 0)
					{
						if (xmlValidatingReaderImpl.outerReader.NodeType != XmlNodeType.EntityReference || xmlValidatingReaderImpl.parsingFunction == XmlValidatingReaderImpl.ParsingFunction.ResolveEntityInternally)
						{
							goto IL_A3;
						}
						awaiter = xmlValidatingReaderImpl.outerReader.ReadAsync().ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 0;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter, XmlValidatingReaderImpl.<MoveOffEntityReferenceAsync>d__150>(ref awaiter, ref this);
							return;
						}
					}
					else
					{
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
					}
					if (!awaiter.GetResult())
					{
						throw new InvalidOperationException(Res.GetString("Operation is not valid due to the current state of the object."));
					}
					IL_A3:;
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

			// Token: 0x06000C01 RID: 3073 RVA: 0x0004EAEC File Offset: 0x0004CCEC
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04000D5D RID: 3421
			public int <>1__state;

			// Token: 0x04000D5E RID: 3422
			public AsyncTaskMethodBuilder <>t__builder;

			// Token: 0x04000D5F RID: 3423
			public XmlValidatingReaderImpl <>4__this;

			// Token: 0x04000D60 RID: 3424
			private ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter <>u__1;
		}

		// Token: 0x02000149 RID: 329
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <ReadTypedValueAsync>d__151 : IAsyncStateMachine
		{
			// Token: 0x06000C02 RID: 3074 RVA: 0x0004EAFC File Offset: 0x0004CCFC
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				XmlValidatingReaderImpl xmlValidatingReaderImpl = this.<>4__this;
				object result;
				try
				{
					if (num != 0)
					{
						ConfiguredTaskAwaitable<string>.ConfiguredTaskAwaiter awaiter;
						if (num != 1)
						{
							if (xmlValidatingReaderImpl.validationType == ValidationType.None)
							{
								result = null;
								goto IL_250;
							}
							XmlNodeType nodeType = xmlValidatingReaderImpl.outerReader.NodeType;
							if (nodeType != XmlNodeType.Element)
							{
								if (nodeType == XmlNodeType.Attribute)
								{
									result = xmlValidatingReaderImpl.coreReaderImpl.InternalTypedValue;
									goto IL_250;
								}
								if (nodeType == XmlNodeType.EndElement)
								{
									result = null;
									goto IL_250;
								}
								if (xmlValidatingReaderImpl.coreReaderImpl.V1Compat)
								{
									result = null;
									goto IL_250;
								}
								awaiter = xmlValidatingReaderImpl.GetValueAsync().ConfigureAwait(false).GetAwaiter();
								if (!awaiter.IsCompleted)
								{
									this.<>1__state = 1;
									this.<>u__2 = awaiter;
									this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<string>.ConfiguredTaskAwaiter, XmlValidatingReaderImpl.<ReadTypedValueAsync>d__151>(ref awaiter, ref this);
									return;
								}
							}
							else
							{
								if (xmlValidatingReaderImpl.SchemaType == null)
								{
									result = null;
									goto IL_250;
								}
								if (((xmlValidatingReaderImpl.SchemaType is XmlSchemaDatatype) ? ((XmlSchemaDatatype)xmlValidatingReaderImpl.SchemaType) : ((XmlSchemaType)xmlValidatingReaderImpl.SchemaType).Datatype) == null)
								{
									result = null;
									goto IL_250;
								}
								if (!xmlValidatingReaderImpl.outerReader.IsEmptyElement)
								{
									goto IL_AA;
								}
								goto IL_19C;
							}
						}
						else
						{
							awaiter = this.<>u__2;
							this.<>u__2 = default(ConfiguredTaskAwaitable<string>.ConfiguredTaskAwaiter);
							this.<>1__state = -1;
						}
						result = awaiter.GetResult();
						goto IL_250;
					}
					ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter awaiter2 = this.<>u__1;
					this.<>u__1 = default(ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter);
					this.<>1__state = -1;
					goto IL_110;
					IL_AA:
					awaiter2 = xmlValidatingReaderImpl.outerReader.ReadAsync().ConfigureAwait(false).GetAwaiter();
					if (!awaiter2.IsCompleted)
					{
						this.<>1__state = 0;
						this.<>u__1 = awaiter2;
						this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter, XmlValidatingReaderImpl.<ReadTypedValueAsync>d__151>(ref awaiter2, ref this);
						return;
					}
					IL_110:
					if (!awaiter2.GetResult())
					{
						throw new InvalidOperationException(Res.GetString("Operation is not valid due to the current state of the object."));
					}
					XmlNodeType nodeType2 = xmlValidatingReaderImpl.outerReader.NodeType;
					if (nodeType2 == XmlNodeType.CDATA || nodeType2 == XmlNodeType.Text || nodeType2 == XmlNodeType.Whitespace || nodeType2 == XmlNodeType.SignificantWhitespace || nodeType2 == XmlNodeType.Comment || nodeType2 == XmlNodeType.ProcessingInstruction)
					{
						goto IL_AA;
					}
					if (xmlValidatingReaderImpl.outerReader.NodeType != XmlNodeType.EndElement)
					{
						throw new XmlException("'{0}' is an invalid XmlNodeType.", xmlValidatingReaderImpl.outerReader.NodeType.ToString());
					}
					IL_19C:
					result = xmlValidatingReaderImpl.coreReaderImpl.InternalTypedValue;
				}
				catch (Exception exception)
				{
					this.<>1__state = -2;
					this.<>t__builder.SetException(exception);
					return;
				}
				IL_250:
				this.<>1__state = -2;
				this.<>t__builder.SetResult(result);
			}

			// Token: 0x06000C03 RID: 3075 RVA: 0x0004ED8C File Offset: 0x0004CF8C
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04000D61 RID: 3425
			public int <>1__state;

			// Token: 0x04000D62 RID: 3426
			public AsyncTaskMethodBuilder<object> <>t__builder;

			// Token: 0x04000D63 RID: 3427
			public XmlValidatingReaderImpl <>4__this;

			// Token: 0x04000D64 RID: 3428
			private ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter <>u__1;

			// Token: 0x04000D65 RID: 3429
			private ConfiguredTaskAwaitable<string>.ConfiguredTaskAwaiter <>u__2;
		}

		// Token: 0x0200014A RID: 330
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <ParseDtdFromParserContextAsync>d__152 : IAsyncStateMachine
		{
			// Token: 0x06000C04 RID: 3076 RVA: 0x0004ED9C File Offset: 0x0004CF9C
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				XmlValidatingReaderImpl xmlValidatingReaderImpl = this.<>4__this;
				try
				{
					ConfiguredTaskAwaitable<IDtdInfo>.ConfiguredTaskAwaiter awaiter;
					if (num != 0)
					{
						if (xmlValidatingReaderImpl.parserContext.DocTypeName == null || xmlValidatingReaderImpl.parserContext.DocTypeName.Length == 0)
						{
							goto IL_113;
						}
						IDtdParser dtdParser = DtdParser.Create();
						XmlTextReaderImpl.DtdParserProxy adapter = new XmlTextReaderImpl.DtdParserProxy(xmlValidatingReaderImpl.coreReaderImpl);
						awaiter = dtdParser.ParseFreeFloatingDtdAsync(xmlValidatingReaderImpl.parserContext.BaseURI, xmlValidatingReaderImpl.parserContext.DocTypeName, xmlValidatingReaderImpl.parserContext.PublicId, xmlValidatingReaderImpl.parserContext.SystemId, xmlValidatingReaderImpl.parserContext.InternalSubset, adapter).ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 0;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<IDtdInfo>.ConfiguredTaskAwaiter, XmlValidatingReaderImpl.<ParseDtdFromParserContextAsync>d__152>(ref awaiter, ref this);
							return;
						}
					}
					else
					{
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable<IDtdInfo>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
					}
					IDtdInfo result = awaiter.GetResult();
					xmlValidatingReaderImpl.coreReaderImpl.SetDtdInfo(result);
					xmlValidatingReaderImpl.ValidateDtd();
				}
				catch (Exception exception)
				{
					this.<>1__state = -2;
					this.<>t__builder.SetException(exception);
					return;
				}
				IL_113:
				this.<>1__state = -2;
				this.<>t__builder.SetResult();
			}

			// Token: 0x06000C05 RID: 3077 RVA: 0x0004EEE0 File Offset: 0x0004D0E0
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04000D66 RID: 3430
			public int <>1__state;

			// Token: 0x04000D67 RID: 3431
			public AsyncTaskMethodBuilder <>t__builder;

			// Token: 0x04000D68 RID: 3432
			public XmlValidatingReaderImpl <>4__this;

			// Token: 0x04000D69 RID: 3433
			private ConfiguredTaskAwaitable<IDtdInfo>.ConfiguredTaskAwaiter <>u__1;
		}

		// Token: 0x0200014B RID: 331
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <ResolveEntityInternallyAsync>d__153 : IAsyncStateMachine
		{
			// Token: 0x06000C06 RID: 3078 RVA: 0x0004EEF0 File Offset: 0x0004D0F0
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				XmlValidatingReaderImpl xmlValidatingReaderImpl = this.<>4__this;
				try
				{
					ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter awaiter;
					if (num == 0)
					{
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_8C;
					}
					this.<initialDepth>5__2 = xmlValidatingReaderImpl.coreReader.Depth;
					xmlValidatingReaderImpl.outerReader.ResolveEntity();
					IL_2D:
					awaiter = xmlValidatingReaderImpl.outerReader.ReadAsync().ConfigureAwait(false).GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						this.<>1__state = 0;
						this.<>u__1 = awaiter;
						this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter, XmlValidatingReaderImpl.<ResolveEntityInternallyAsync>d__153>(ref awaiter, ref this);
						return;
					}
					IL_8C:
					if (awaiter.GetResult() && xmlValidatingReaderImpl.coreReader.Depth > this.<initialDepth>5__2)
					{
						goto IL_2D;
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

			// Token: 0x06000C07 RID: 3079 RVA: 0x0004EFE4 File Offset: 0x0004D1E4
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04000D6A RID: 3434
			public int <>1__state;

			// Token: 0x04000D6B RID: 3435
			public AsyncTaskMethodBuilder <>t__builder;

			// Token: 0x04000D6C RID: 3436
			public XmlValidatingReaderImpl <>4__this;

			// Token: 0x04000D6D RID: 3437
			private int <initialDepth>5__2;

			// Token: 0x04000D6E RID: 3438
			private ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter <>u__1;
		}
	}
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Xml.XmlConfiguration;

namespace System.Xml.Schema
{
	// Token: 0x02000582 RID: 1410
	internal sealed class XdrBuilder : SchemaBuilder
	{
		// Token: 0x060038B1 RID: 14513 RVA: 0x00147304 File Offset: 0x00145504
		internal XdrBuilder(XmlReader reader, XmlNamespaceManager curmgr, SchemaInfo sinfo, string targetNamspace, XmlNameTable nameTable, SchemaNames schemaNames, ValidationEventHandler eventhandler)
		{
			this._SchemaInfo = sinfo;
			this._TargetNamespace = targetNamspace;
			this._reader = reader;
			this._CurNsMgr = curmgr;
			this.validationEventHandler = eventhandler;
			this._StateHistory = new HWStack(10);
			this._ElementDef = new XdrBuilder.ElementContent();
			this._AttributeDef = new XdrBuilder.AttributeContent();
			this._GroupStack = new HWStack(10);
			this._GroupDef = new XdrBuilder.GroupContent();
			this._NameTable = nameTable;
			this._SchemaNames = schemaNames;
			this._CurState = XdrBuilder.S_SchemaEntries[0];
			this.positionInfo = PositionInfo.GetPositionInfo(this._reader);
			this.xmlResolver = XmlReaderSection.CreateDefaultResolver();
		}

		// Token: 0x060038B2 RID: 14514 RVA: 0x001473BC File Offset: 0x001455BC
		internal override bool ProcessElement(string prefix, string name, string ns)
		{
			XmlQualifiedName xmlQualifiedName = new XmlQualifiedName(name, XmlSchemaDatatype.XdrCanonizeUri(ns, this._NameTable, this._SchemaNames));
			if (this.GetNextState(xmlQualifiedName))
			{
				this.Push();
				if (this._CurState._InitFunc != null)
				{
					this._CurState._InitFunc(this, xmlQualifiedName);
				}
				return true;
			}
			if (!this.IsSkipableElement(xmlQualifiedName))
			{
				this.SendValidationEvent("The '{0}' element is not supported in this context.", XmlQualifiedName.ToString(name, prefix));
			}
			return false;
		}

		// Token: 0x060038B3 RID: 14515 RVA: 0x00147430 File Offset: 0x00145630
		internal override void ProcessAttribute(string prefix, string name, string ns, string value)
		{
			XmlQualifiedName xmlQualifiedName = new XmlQualifiedName(name, XmlSchemaDatatype.XdrCanonizeUri(ns, this._NameTable, this._SchemaNames));
			int i = 0;
			while (i < this._CurState._Attributes.Length)
			{
				XdrBuilder.XdrAttributeEntry xdrAttributeEntry = this._CurState._Attributes[i];
				if (this._SchemaNames.TokenToQName[(int)xdrAttributeEntry._Attribute].Equals(xmlQualifiedName))
				{
					XdrBuilder.XdrBuildFunction buildFunc = xdrAttributeEntry._BuildFunc;
					if (xdrAttributeEntry._Datatype.TokenizedType == XmlTokenizedType.QName)
					{
						string text;
						XmlQualifiedName xmlQualifiedName2 = XmlQualifiedName.Parse(value, this._CurNsMgr, out text);
						xmlQualifiedName2.Atomize(this._NameTable);
						if (text.Length != 0)
						{
							if (xdrAttributeEntry._Attribute != SchemaNames.Token.SchemaType)
							{
								throw new XmlException("This is an unexpected token. The expected token is '{0}'.", "NAME");
							}
						}
						else if (this.IsGlobal(xdrAttributeEntry._SchemaFlags))
						{
							xmlQualifiedName2 = new XmlQualifiedName(xmlQualifiedName2.Name, this._TargetNamespace);
						}
						else
						{
							xmlQualifiedName2 = new XmlQualifiedName(xmlQualifiedName2.Name);
						}
						buildFunc(this, xmlQualifiedName2, text);
						return;
					}
					buildFunc(this, xdrAttributeEntry._Datatype.ParseValue(value, this._NameTable, this._CurNsMgr), string.Empty);
					return;
				}
				else
				{
					i++;
				}
			}
			if (ns == this._SchemaNames.NsXmlNs && XdrBuilder.IsXdrSchema(value))
			{
				this.LoadSchema(value);
				return;
			}
			if (!this.IsSkipableAttribute(xmlQualifiedName))
			{
				this.SendValidationEvent("The '{0}' attribute is not supported in this context.", XmlQualifiedName.ToString(xmlQualifiedName.Name, prefix));
			}
		}

		// Token: 0x17000A96 RID: 2710
		// (set) Token: 0x060038B4 RID: 14516 RVA: 0x0014759A File Offset: 0x0014579A
		internal XmlResolver XmlResolver
		{
			set
			{
				this.xmlResolver = value;
			}
		}

		// Token: 0x060038B5 RID: 14517 RVA: 0x001475A4 File Offset: 0x001457A4
		private bool LoadSchema(string uri)
		{
			if (this.xmlResolver == null)
			{
				return false;
			}
			uri = this._NameTable.Add(uri);
			if (this._SchemaInfo.TargetNamespaces.ContainsKey(uri))
			{
				return false;
			}
			SchemaInfo schemaInfo = null;
			Uri baseUri = this.xmlResolver.ResolveUri(null, this._reader.BaseURI);
			XmlReader xmlReader = null;
			try
			{
				Uri uri2 = this.xmlResolver.ResolveUri(baseUri, uri.Substring("x-schema:".Length));
				Stream input = (Stream)this.xmlResolver.GetEntity(uri2, null, null);
				xmlReader = new XmlTextReader(uri2.ToString(), input, this._NameTable);
				schemaInfo = new SchemaInfo();
				Parser parser = new Parser(SchemaType.XDR, this._NameTable, this._SchemaNames, this.validationEventHandler);
				parser.XmlResolver = this.xmlResolver;
				parser.Parse(xmlReader, uri);
				schemaInfo = parser.XdrSchema;
			}
			catch (XmlException ex)
			{
				this.SendValidationEvent("Cannot load the schema for the namespace '{0}' - {1}", new string[]
				{
					uri,
					ex.Message
				}, XmlSeverityType.Warning);
				schemaInfo = null;
			}
			finally
			{
				if (xmlReader != null)
				{
					xmlReader.Close();
				}
			}
			if (schemaInfo != null && schemaInfo.ErrorCount == 0)
			{
				this._SchemaInfo.Add(schemaInfo, this.validationEventHandler);
				return true;
			}
			return false;
		}

		// Token: 0x060038B6 RID: 14518 RVA: 0x001476E8 File Offset: 0x001458E8
		internal static bool IsXdrSchema(string uri)
		{
			return uri.Length >= "x-schema:".Length && string.Compare(uri, 0, "x-schema:", 0, "x-schema:".Length, StringComparison.Ordinal) == 0 && !uri.StartsWith("x-schema:#", StringComparison.Ordinal);
		}

		// Token: 0x060038B7 RID: 14519 RVA: 0x0001222F File Offset: 0x0001042F
		internal override bool IsContentParsed()
		{
			return true;
		}

		// Token: 0x060038B8 RID: 14520 RVA: 0x0000BB08 File Offset: 0x00009D08
		internal override void ProcessMarkup(XmlNode[] markup)
		{
			throw new InvalidOperationException(Res.GetString("Operation is not valid due to the current state of the object."));
		}

		// Token: 0x060038B9 RID: 14521 RVA: 0x00147727 File Offset: 0x00145927
		internal override void ProcessCData(string value)
		{
			if (this._CurState._AllowText)
			{
				this._Text = value;
				return;
			}
			this.SendValidationEvent("The following text is not allowed in this context: '{0}'.", value);
		}

		// Token: 0x060038BA RID: 14522 RVA: 0x0014774A File Offset: 0x0014594A
		internal override void StartChildren()
		{
			if (this._CurState._BeginChildFunc != null)
			{
				this._CurState._BeginChildFunc(this);
			}
		}

		// Token: 0x060038BB RID: 14523 RVA: 0x0014776A File Offset: 0x0014596A
		internal override void EndChildren()
		{
			if (this._CurState._EndChildFunc != null)
			{
				this._CurState._EndChildFunc(this);
			}
			this.Pop();
		}

		// Token: 0x060038BC RID: 14524 RVA: 0x00147790 File Offset: 0x00145990
		private void Push()
		{
			this._StateHistory.Push();
			this._StateHistory[this._StateHistory.Length - 1] = this._CurState;
			this._CurState = this._NextState;
		}

		// Token: 0x060038BD RID: 14525 RVA: 0x001477C8 File Offset: 0x001459C8
		private void Pop()
		{
			this._CurState = (XdrBuilder.XdrEntry)this._StateHistory.Pop();
		}

		// Token: 0x060038BE RID: 14526 RVA: 0x001477E0 File Offset: 0x001459E0
		private void PushGroupInfo()
		{
			this._GroupStack.Push();
			this._GroupStack[this._GroupStack.Length - 1] = XdrBuilder.GroupContent.Copy(this._GroupDef);
		}

		// Token: 0x060038BF RID: 14527 RVA: 0x00147811 File Offset: 0x00145A11
		private void PopGroupInfo()
		{
			this._GroupDef = (XdrBuilder.GroupContent)this._GroupStack.Pop();
		}

		// Token: 0x060038C0 RID: 14528 RVA: 0x00147829 File Offset: 0x00145A29
		private static void XDR_InitRoot(XdrBuilder builder, object obj)
		{
			builder._SchemaInfo.SchemaType = SchemaType.XDR;
			builder._ElementDef._ElementDecl = null;
			builder._ElementDef._AttDefList = null;
			builder._AttributeDef._AttDef = null;
		}

		// Token: 0x060038C1 RID: 14529 RVA: 0x0014785B File Offset: 0x00145A5B
		private static void XDR_BuildRoot_Name(XdrBuilder builder, object obj, string prefix)
		{
			builder._XdrName = (string)obj;
			builder._XdrPrefix = prefix;
		}

		// Token: 0x060038C2 RID: 14530 RVA: 0x0000B528 File Offset: 0x00009728
		private static void XDR_BuildRoot_ID(XdrBuilder builder, object obj, string prefix)
		{
		}

		// Token: 0x060038C3 RID: 14531 RVA: 0x00147870 File Offset: 0x00145A70
		private static void XDR_BeginRoot(XdrBuilder builder)
		{
			if (builder._TargetNamespace == null)
			{
				if (builder._XdrName != null)
				{
					builder._TargetNamespace = builder._NameTable.Add("x-schema:#" + builder._XdrName);
				}
				else
				{
					builder._TargetNamespace = string.Empty;
				}
			}
			builder._SchemaInfo.TargetNamespaces.Add(builder._TargetNamespace, true);
		}

		// Token: 0x060038C4 RID: 14532 RVA: 0x001478D4 File Offset: 0x00145AD4
		private static void XDR_EndRoot(XdrBuilder builder)
		{
			while (builder._UndefinedAttributeTypes != null)
			{
				XmlQualifiedName xmlQualifiedName = builder._UndefinedAttributeTypes._TypeName;
				if (xmlQualifiedName.Namespace.Length == 0)
				{
					xmlQualifiedName = new XmlQualifiedName(xmlQualifiedName.Name, builder._TargetNamespace);
				}
				SchemaAttDef schemaAttDef;
				if (builder._SchemaInfo.AttributeDecls.TryGetValue(xmlQualifiedName, out schemaAttDef))
				{
					builder._UndefinedAttributeTypes._Attdef = schemaAttDef.Clone();
					builder._UndefinedAttributeTypes._Attdef.Name = xmlQualifiedName;
					builder.XDR_CheckAttributeDefault(builder._UndefinedAttributeTypes, builder._UndefinedAttributeTypes._Attdef);
				}
				else
				{
					builder.SendValidationEvent("The '{0}' attribute is not declared.", xmlQualifiedName.Name);
				}
				builder._UndefinedAttributeTypes = builder._UndefinedAttributeTypes._Next;
			}
			foreach (object obj in builder._UndeclaredElements.Values)
			{
				SchemaElementDecl schemaElementDecl = (SchemaElementDecl)obj;
				builder.SendValidationEvent("The '{0}' element is not declared.", XmlQualifiedName.ToString(schemaElementDecl.Name.Name, schemaElementDecl.Prefix));
			}
		}

		// Token: 0x060038C5 RID: 14533 RVA: 0x001479FC File Offset: 0x00145BFC
		private static void XDR_InitElementType(XdrBuilder builder, object obj)
		{
			builder._ElementDef._ElementDecl = new SchemaElementDecl();
			builder._contentValidator = new ParticleContentValidator(XmlSchemaContentType.Mixed);
			builder._contentValidator.IsOpen = true;
			builder._ElementDef._ContentAttr = 0;
			builder._ElementDef._OrderAttr = 0;
			builder._ElementDef._MasterGroupRequired = false;
			builder._ElementDef._ExistTerminal = false;
			builder._ElementDef._AllowDataType = true;
			builder._ElementDef._HasDataType = false;
			builder._ElementDef._EnumerationRequired = false;
			builder._ElementDef._AttDefList = new Hashtable();
			builder._ElementDef._MaxLength = uint.MaxValue;
			builder._ElementDef._MinLength = uint.MaxValue;
		}

		// Token: 0x060038C6 RID: 14534 RVA: 0x00147AB0 File Offset: 0x00145CB0
		private static void XDR_BuildElementType_Name(XdrBuilder builder, object obj, string prefix)
		{
			XmlQualifiedName xmlQualifiedName = (XmlQualifiedName)obj;
			if (builder._SchemaInfo.ElementDecls.ContainsKey(xmlQualifiedName))
			{
				builder.SendValidationEvent("The '{0}' element has already been declared.", XmlQualifiedName.ToString(xmlQualifiedName.Name, prefix));
			}
			builder._ElementDef._ElementDecl.Name = xmlQualifiedName;
			builder._ElementDef._ElementDecl.Prefix = prefix;
			builder._SchemaInfo.ElementDecls.Add(xmlQualifiedName, builder._ElementDef._ElementDecl);
			if (builder._UndeclaredElements[xmlQualifiedName] != null)
			{
				builder._UndeclaredElements.Remove(xmlQualifiedName);
			}
		}

		// Token: 0x060038C7 RID: 14535 RVA: 0x00147B46 File Offset: 0x00145D46
		private static void XDR_BuildElementType_Content(XdrBuilder builder, object obj, string prefix)
		{
			builder._ElementDef._ContentAttr = builder.GetContent((XmlQualifiedName)obj);
		}

		// Token: 0x060038C8 RID: 14536 RVA: 0x00147B5F File Offset: 0x00145D5F
		private static void XDR_BuildElementType_Model(XdrBuilder builder, object obj, string prefix)
		{
			builder._contentValidator.IsOpen = builder.GetModel((XmlQualifiedName)obj);
		}

		// Token: 0x060038C9 RID: 14537 RVA: 0x00147B78 File Offset: 0x00145D78
		private static void XDR_BuildElementType_Order(XdrBuilder builder, object obj, string prefix)
		{
			builder._ElementDef._OrderAttr = (builder._GroupDef._Order = builder.GetOrder((XmlQualifiedName)obj));
		}

		// Token: 0x060038CA RID: 14538 RVA: 0x00147BAC File Offset: 0x00145DAC
		private static void XDR_BuildElementType_DtType(XdrBuilder builder, object obj, string prefix)
		{
			builder._ElementDef._HasDataType = true;
			string text = ((string)obj).Trim();
			if (text.Length == 0)
			{
				builder.SendValidationEvent("The DataType value cannot be empty.");
				return;
			}
			XmlSchemaDatatype xmlSchemaDatatype = XmlSchemaDatatype.FromXdrName(text);
			if (xmlSchemaDatatype == null)
			{
				builder.SendValidationEvent("Reference to an unknown data type, '{0}'.", text);
			}
			builder._ElementDef._ElementDecl.Datatype = xmlSchemaDatatype;
		}

		// Token: 0x060038CB RID: 14539 RVA: 0x00147C0C File Offset: 0x00145E0C
		private static void XDR_BuildElementType_DtValues(XdrBuilder builder, object obj, string prefix)
		{
			builder._ElementDef._EnumerationRequired = true;
			builder._ElementDef._ElementDecl.Values = new List<string>((string[])obj);
		}

		// Token: 0x060038CC RID: 14540 RVA: 0x00147C35 File Offset: 0x00145E35
		private static void XDR_BuildElementType_DtMaxLength(XdrBuilder builder, object obj, string prefix)
		{
			XdrBuilder.ParseDtMaxLength(ref builder._ElementDef._MaxLength, obj, builder);
		}

		// Token: 0x060038CD RID: 14541 RVA: 0x00147C49 File Offset: 0x00145E49
		private static void XDR_BuildElementType_DtMinLength(XdrBuilder builder, object obj, string prefix)
		{
			XdrBuilder.ParseDtMinLength(ref builder._ElementDef._MinLength, obj, builder);
		}

		// Token: 0x060038CE RID: 14542 RVA: 0x00147C60 File Offset: 0x00145E60
		private static void XDR_BeginElementType(XdrBuilder builder)
		{
			string text = null;
			string msg = null;
			if (builder._ElementDef._ElementDecl.Name.IsEmpty)
			{
				text = "The '{0}' attribute is either invalid or missing.";
				msg = "name";
			}
			else
			{
				if (builder._ElementDef._HasDataType)
				{
					if (!builder._ElementDef._AllowDataType)
					{
						text = "Content must be \"textOnly\" when using DataType on an ElementType.";
						goto IL_1F4;
					}
					builder._ElementDef._ContentAttr = 2;
				}
				else if (builder._ElementDef._ContentAttr == 0)
				{
					switch (builder._ElementDef._OrderAttr)
					{
					case 0:
						builder._ElementDef._ContentAttr = 3;
						builder._ElementDef._OrderAttr = 1;
						break;
					case 1:
						builder._ElementDef._ContentAttr = 3;
						break;
					case 2:
						builder._ElementDef._ContentAttr = 4;
						break;
					case 3:
						builder._ElementDef._ContentAttr = 4;
						break;
					}
				}
				bool isOpen = builder._contentValidator.IsOpen;
				XdrBuilder.ElementContent elementDef = builder._ElementDef;
				switch (builder._ElementDef._ContentAttr)
				{
				case 1:
					builder._ElementDef._ElementDecl.ContentValidator = ContentValidator.Empty;
					builder._contentValidator = null;
					break;
				case 2:
					builder._ElementDef._ElementDecl.ContentValidator = ContentValidator.TextOnly;
					builder._GroupDef._Order = 1;
					builder._contentValidator = null;
					break;
				case 3:
					if (elementDef._OrderAttr != 0 && elementDef._OrderAttr != 1)
					{
						text = "The order must be many when content is mixed.";
						goto IL_1F4;
					}
					builder._GroupDef._Order = 1;
					elementDef._MasterGroupRequired = true;
					builder._contentValidator.IsOpen = isOpen;
					break;
				case 4:
					builder._contentValidator = new ParticleContentValidator(XmlSchemaContentType.ElementOnly);
					if (elementDef._OrderAttr == 0)
					{
						builder._GroupDef._Order = 2;
					}
					elementDef._MasterGroupRequired = true;
					builder._contentValidator.IsOpen = isOpen;
					break;
				}
				if (elementDef._ContentAttr == 3 || elementDef._ContentAttr == 4)
				{
					builder._contentValidator.Start();
					builder._contentValidator.OpenGroup();
				}
			}
			IL_1F4:
			if (text != null)
			{
				builder.SendValidationEvent(text, msg);
			}
		}

		// Token: 0x060038CF RID: 14543 RVA: 0x00147E6C File Offset: 0x0014606C
		private static void XDR_EndElementType(XdrBuilder builder)
		{
			SchemaElementDecl elementDecl = builder._ElementDef._ElementDecl;
			if (builder._UndefinedAttributeTypes != null && builder._ElementDef._AttDefList != null)
			{
				XdrBuilder.DeclBaseInfo declBaseInfo = builder._UndefinedAttributeTypes;
				XdrBuilder.DeclBaseInfo declBaseInfo2 = declBaseInfo;
				while (declBaseInfo != null)
				{
					SchemaAttDef schemaAttDef = null;
					if (declBaseInfo._ElementDecl == elementDecl)
					{
						XmlQualifiedName typeName = declBaseInfo._TypeName;
						schemaAttDef = (SchemaAttDef)builder._ElementDef._AttDefList[typeName];
						if (schemaAttDef != null)
						{
							declBaseInfo._Attdef = schemaAttDef.Clone();
							declBaseInfo._Attdef.Name = typeName;
							builder.XDR_CheckAttributeDefault(declBaseInfo, schemaAttDef);
							if (declBaseInfo == builder._UndefinedAttributeTypes)
							{
								declBaseInfo = (builder._UndefinedAttributeTypes = declBaseInfo._Next);
								declBaseInfo2 = declBaseInfo;
							}
							else
							{
								declBaseInfo2._Next = declBaseInfo._Next;
								declBaseInfo = declBaseInfo2._Next;
							}
						}
					}
					if (schemaAttDef == null)
					{
						if (declBaseInfo != builder._UndefinedAttributeTypes)
						{
							declBaseInfo2 = declBaseInfo2._Next;
						}
						declBaseInfo = declBaseInfo._Next;
					}
				}
			}
			if (builder._ElementDef._MasterGroupRequired)
			{
				builder._contentValidator.CloseGroup();
				if (!builder._ElementDef._ExistTerminal)
				{
					if (builder._contentValidator.IsOpen)
					{
						builder._ElementDef._ElementDecl.ContentValidator = ContentValidator.Any;
						builder._contentValidator = null;
					}
					else if (builder._ElementDef._ContentAttr != 3)
					{
						builder.SendValidationEvent("There is a missing element.");
					}
				}
				else if (builder._GroupDef._Order == 1)
				{
					builder._contentValidator.AddStar();
				}
			}
			if (elementDecl.Datatype != null)
			{
				XmlTokenizedType tokenizedType = elementDecl.Datatype.TokenizedType;
				if (tokenizedType == XmlTokenizedType.ENUMERATION && !builder._ElementDef._EnumerationRequired)
				{
					builder.SendValidationEvent("The dt:values attribute is missing.");
				}
				if (tokenizedType != XmlTokenizedType.ENUMERATION && builder._ElementDef._EnumerationRequired)
				{
					builder.SendValidationEvent("Data type should be enumeration when the values attribute is present.");
				}
			}
			XdrBuilder.CompareMinMaxLength(builder._ElementDef._MinLength, builder._ElementDef._MaxLength, builder);
			elementDecl.MaxLength = (long)((ulong)builder._ElementDef._MaxLength);
			elementDecl.MinLength = (long)((ulong)builder._ElementDef._MinLength);
			if (builder._contentValidator != null)
			{
				builder._ElementDef._ElementDecl.ContentValidator = builder._contentValidator.Finish(true);
				builder._contentValidator = null;
			}
			builder._ElementDef._ElementDecl = null;
			builder._ElementDef._AttDefList = null;
		}

		// Token: 0x060038D0 RID: 14544 RVA: 0x001480A4 File Offset: 0x001462A4
		private static void XDR_InitAttributeType(XdrBuilder builder, object obj)
		{
			XdrBuilder.AttributeContent attributeDef = builder._AttributeDef;
			attributeDef._AttDef = new SchemaAttDef(XmlQualifiedName.Empty, null);
			attributeDef._Required = false;
			attributeDef._Prefix = null;
			attributeDef._Default = null;
			attributeDef._MinVal = 0U;
			attributeDef._MaxVal = 1U;
			attributeDef._EnumerationRequired = false;
			attributeDef._HasDataType = false;
			attributeDef._Global = (builder._StateHistory.Length == 2);
			attributeDef._MaxLength = uint.MaxValue;
			attributeDef._MinLength = uint.MaxValue;
		}

		// Token: 0x060038D1 RID: 14545 RVA: 0x0014811C File Offset: 0x0014631C
		private static void XDR_BuildAttributeType_Name(XdrBuilder builder, object obj, string prefix)
		{
			XmlQualifiedName xmlQualifiedName = (XmlQualifiedName)obj;
			builder._AttributeDef._Name = xmlQualifiedName;
			builder._AttributeDef._Prefix = prefix;
			builder._AttributeDef._AttDef.Name = xmlQualifiedName;
			if (builder._ElementDef._ElementDecl != null)
			{
				if (builder._ElementDef._AttDefList[xmlQualifiedName] == null)
				{
					builder._ElementDef._AttDefList.Add(xmlQualifiedName, builder._AttributeDef._AttDef);
					return;
				}
				builder.SendValidationEvent("The '{0}' attribute has already been declared for this ElementType.", XmlQualifiedName.ToString(xmlQualifiedName.Name, prefix));
				return;
			}
			else
			{
				xmlQualifiedName = new XmlQualifiedName(xmlQualifiedName.Name, builder._TargetNamespace);
				builder._AttributeDef._AttDef.Name = xmlQualifiedName;
				if (!builder._SchemaInfo.AttributeDecls.ContainsKey(xmlQualifiedName))
				{
					builder._SchemaInfo.AttributeDecls.Add(xmlQualifiedName, builder._AttributeDef._AttDef);
					return;
				}
				builder.SendValidationEvent("The '{0}' attribute has already been declared for this ElementType.", XmlQualifiedName.ToString(xmlQualifiedName.Name, prefix));
				return;
			}
		}

		// Token: 0x060038D2 RID: 14546 RVA: 0x00148218 File Offset: 0x00146418
		private static void XDR_BuildAttributeType_Required(XdrBuilder builder, object obj, string prefix)
		{
			builder._AttributeDef._Required = XdrBuilder.IsYes(obj, builder);
		}

		// Token: 0x060038D3 RID: 14547 RVA: 0x0014822C File Offset: 0x0014642C
		private static void XDR_BuildAttributeType_Default(XdrBuilder builder, object obj, string prefix)
		{
			builder._AttributeDef._Default = obj;
		}

		// Token: 0x060038D4 RID: 14548 RVA: 0x0014823C File Offset: 0x0014643C
		private static void XDR_BuildAttributeType_DtType(XdrBuilder builder, object obj, string prefix)
		{
			XmlQualifiedName xmlQualifiedName = (XmlQualifiedName)obj;
			builder._AttributeDef._HasDataType = true;
			builder._AttributeDef._AttDef.Datatype = builder.CheckDatatype(xmlQualifiedName.Name);
		}

		// Token: 0x060038D5 RID: 14549 RVA: 0x00148278 File Offset: 0x00146478
		private static void XDR_BuildAttributeType_DtValues(XdrBuilder builder, object obj, string prefix)
		{
			builder._AttributeDef._EnumerationRequired = true;
			builder._AttributeDef._AttDef.Values = new List<string>((string[])obj);
		}

		// Token: 0x060038D6 RID: 14550 RVA: 0x001482A1 File Offset: 0x001464A1
		private static void XDR_BuildAttributeType_DtMaxLength(XdrBuilder builder, object obj, string prefix)
		{
			XdrBuilder.ParseDtMaxLength(ref builder._AttributeDef._MaxLength, obj, builder);
		}

		// Token: 0x060038D7 RID: 14551 RVA: 0x001482B5 File Offset: 0x001464B5
		private static void XDR_BuildAttributeType_DtMinLength(XdrBuilder builder, object obj, string prefix)
		{
			XdrBuilder.ParseDtMinLength(ref builder._AttributeDef._MinLength, obj, builder);
		}

		// Token: 0x060038D8 RID: 14552 RVA: 0x001482C9 File Offset: 0x001464C9
		private static void XDR_BeginAttributeType(XdrBuilder builder)
		{
			if (builder._AttributeDef._Name.IsEmpty)
			{
				builder.SendValidationEvent("The '{0}' attribute is either invalid or missing.");
			}
		}

		// Token: 0x060038D9 RID: 14553 RVA: 0x001482E8 File Offset: 0x001464E8
		private static void XDR_EndAttributeType(XdrBuilder builder)
		{
			string text = null;
			if (builder._AttributeDef._HasDataType && builder._AttributeDef._AttDef.Datatype != null)
			{
				XmlTokenizedType tokenizedType = builder._AttributeDef._AttDef.Datatype.TokenizedType;
				if (tokenizedType == XmlTokenizedType.ENUMERATION && !builder._AttributeDef._EnumerationRequired)
				{
					text = "The dt:values attribute is missing.";
					goto IL_164;
				}
				if (tokenizedType != XmlTokenizedType.ENUMERATION && builder._AttributeDef._EnumerationRequired)
				{
					text = "Data type should be enumeration when the values attribute is present.";
					goto IL_164;
				}
				if (builder._AttributeDef._Default != null && tokenizedType == XmlTokenizedType.ID)
				{
					text = "An attribute or element of type xs:ID or derived from xs:ID, should not have a value constraint.";
					goto IL_164;
				}
			}
			else
			{
				builder._AttributeDef._AttDef.Datatype = XmlSchemaDatatype.FromXmlTokenizedType(XmlTokenizedType.CDATA);
			}
			XdrBuilder.CompareMinMaxLength(builder._AttributeDef._MinLength, builder._AttributeDef._MaxLength, builder);
			builder._AttributeDef._AttDef.MaxLength = (long)((ulong)builder._AttributeDef._MaxLength);
			builder._AttributeDef._AttDef.MinLength = (long)((ulong)builder._AttributeDef._MinLength);
			if (builder._AttributeDef._Default != null)
			{
				builder._AttributeDef._AttDef.DefaultValueRaw = (builder._AttributeDef._AttDef.DefaultValueExpanded = (string)builder._AttributeDef._Default);
				builder.CheckDefaultAttValue(builder._AttributeDef._AttDef);
			}
			builder.SetAttributePresence(builder._AttributeDef._AttDef, builder._AttributeDef._Required);
			IL_164:
			if (text != null)
			{
				builder.SendValidationEvent(text);
			}
		}

		// Token: 0x060038DA RID: 14554 RVA: 0x00148464 File Offset: 0x00146664
		private static void XDR_InitElement(XdrBuilder builder, object obj)
		{
			if (builder._ElementDef._HasDataType || builder._ElementDef._ContentAttr == 1 || builder._ElementDef._ContentAttr == 2)
			{
				builder.SendValidationEvent("Element is not allowed when the content is empty or textOnly.");
			}
			builder._ElementDef._AllowDataType = false;
			builder._ElementDef._HasType = false;
			builder._ElementDef._MinVal = 1U;
			builder._ElementDef._MaxVal = 1U;
		}

		// Token: 0x060038DB RID: 14555 RVA: 0x001484D8 File Offset: 0x001466D8
		private static void XDR_BuildElement_Type(XdrBuilder builder, object obj, string prefix)
		{
			XmlQualifiedName xmlQualifiedName = (XmlQualifiedName)obj;
			if (!builder._SchemaInfo.ElementDecls.ContainsKey(xmlQualifiedName) && (SchemaElementDecl)builder._UndeclaredElements[xmlQualifiedName] == null)
			{
				SchemaElementDecl value = new SchemaElementDecl(xmlQualifiedName, prefix);
				builder._UndeclaredElements.Add(xmlQualifiedName, value);
			}
			builder._ElementDef._HasType = true;
			if (builder._ElementDef._ExistTerminal)
			{
				builder.AddOrder();
			}
			else
			{
				builder._ElementDef._ExistTerminal = true;
			}
			builder._contentValidator.AddName(xmlQualifiedName, null);
		}

		// Token: 0x060038DC RID: 14556 RVA: 0x00148563 File Offset: 0x00146763
		private static void XDR_BuildElement_MinOccurs(XdrBuilder builder, object obj, string prefix)
		{
			builder._ElementDef._MinVal = XdrBuilder.ParseMinOccurs(obj, builder);
		}

		// Token: 0x060038DD RID: 14557 RVA: 0x00148577 File Offset: 0x00146777
		private static void XDR_BuildElement_MaxOccurs(XdrBuilder builder, object obj, string prefix)
		{
			builder._ElementDef._MaxVal = XdrBuilder.ParseMaxOccurs(obj, builder);
		}

		// Token: 0x060038DE RID: 14558 RVA: 0x0014858B File Offset: 0x0014678B
		private static void XDR_EndElement(XdrBuilder builder)
		{
			if (builder._ElementDef._HasType)
			{
				XdrBuilder.HandleMinMax(builder._contentValidator, builder._ElementDef._MinVal, builder._ElementDef._MaxVal);
				return;
			}
			builder.SendValidationEvent("The '{0}' attribute is either invalid or missing.");
		}

		// Token: 0x060038DF RID: 14559 RVA: 0x001485C7 File Offset: 0x001467C7
		private static void XDR_InitAttribute(XdrBuilder builder, object obj)
		{
			if (builder._BaseDecl == null)
			{
				builder._BaseDecl = new XdrBuilder.DeclBaseInfo();
			}
			builder._BaseDecl._MinOccurs = 0U;
		}

		// Token: 0x060038E0 RID: 14560 RVA: 0x001485E8 File Offset: 0x001467E8
		private static void XDR_BuildAttribute_Type(XdrBuilder builder, object obj, string prefix)
		{
			builder._BaseDecl._TypeName = (XmlQualifiedName)obj;
			builder._BaseDecl._Prefix = prefix;
		}

		// Token: 0x060038E1 RID: 14561 RVA: 0x00148607 File Offset: 0x00146807
		private static void XDR_BuildAttribute_Required(XdrBuilder builder, object obj, string prefix)
		{
			if (XdrBuilder.IsYes(obj, builder))
			{
				builder._BaseDecl._MinOccurs = 1U;
			}
		}

		// Token: 0x060038E2 RID: 14562 RVA: 0x0014861E File Offset: 0x0014681E
		private static void XDR_BuildAttribute_Default(XdrBuilder builder, object obj, string prefix)
		{
			builder._BaseDecl._Default = obj;
		}

		// Token: 0x060038E3 RID: 14563 RVA: 0x0014862C File Offset: 0x0014682C
		private static void XDR_BeginAttribute(XdrBuilder builder)
		{
			if (builder._BaseDecl._TypeName.IsEmpty)
			{
				builder.SendValidationEvent("The '{0}' attribute is either invalid or missing.");
			}
			SchemaAttDef schemaAttDef = null;
			XmlQualifiedName typeName = builder._BaseDecl._TypeName;
			string prefix = builder._BaseDecl._Prefix;
			if (builder._ElementDef._AttDefList != null)
			{
				schemaAttDef = (SchemaAttDef)builder._ElementDef._AttDefList[typeName];
			}
			if (schemaAttDef == null)
			{
				XmlQualifiedName key = typeName;
				if (prefix.Length == 0)
				{
					key = new XmlQualifiedName(typeName.Name, builder._TargetNamespace);
				}
				SchemaAttDef schemaAttDef2;
				if (builder._SchemaInfo.AttributeDecls.TryGetValue(key, out schemaAttDef2))
				{
					schemaAttDef = schemaAttDef2.Clone();
					schemaAttDef.Name = typeName;
				}
				else if (prefix.Length != 0)
				{
					builder.SendValidationEvent("The '{0}' attribute is not declared.", XmlQualifiedName.ToString(typeName.Name, prefix));
				}
			}
			if (schemaAttDef != null)
			{
				builder.XDR_CheckAttributeDefault(builder._BaseDecl, schemaAttDef);
			}
			else
			{
				schemaAttDef = new SchemaAttDef(typeName, prefix);
				builder._UndefinedAttributeTypes = new XdrBuilder.DeclBaseInfo
				{
					_Checking = true,
					_Attdef = schemaAttDef,
					_TypeName = builder._BaseDecl._TypeName,
					_ElementDecl = builder._ElementDef._ElementDecl,
					_MinOccurs = builder._BaseDecl._MinOccurs,
					_Default = builder._BaseDecl._Default,
					_Next = builder._UndefinedAttributeTypes
				};
			}
			builder._ElementDef._ElementDecl.AddAttDef(schemaAttDef);
		}

		// Token: 0x060038E4 RID: 14564 RVA: 0x00148797 File Offset: 0x00146997
		private static void XDR_EndAttribute(XdrBuilder builder)
		{
			builder._BaseDecl.Reset();
		}

		// Token: 0x060038E5 RID: 14565 RVA: 0x001487A4 File Offset: 0x001469A4
		private static void XDR_InitGroup(XdrBuilder builder, object obj)
		{
			if (builder._ElementDef._ContentAttr == 1 || builder._ElementDef._ContentAttr == 2)
			{
				builder.SendValidationEvent("The group is not allowed when ElementType has empty or textOnly content.");
			}
			builder.PushGroupInfo();
			builder._GroupDef._MinVal = 1U;
			builder._GroupDef._MaxVal = 1U;
			builder._GroupDef._HasMaxAttr = false;
			builder._GroupDef._HasMinAttr = false;
			if (builder._ElementDef._ExistTerminal)
			{
				builder.AddOrder();
			}
			builder._ElementDef._ExistTerminal = false;
			builder._contentValidator.OpenGroup();
		}

		// Token: 0x060038E6 RID: 14566 RVA: 0x00148838 File Offset: 0x00146A38
		private static void XDR_BuildGroup_Order(XdrBuilder builder, object obj, string prefix)
		{
			builder._GroupDef._Order = builder.GetOrder((XmlQualifiedName)obj);
			if (builder._ElementDef._ContentAttr == 3 && builder._GroupDef._Order != 1)
			{
				builder.SendValidationEvent("The order must be many when content is mixed.");
			}
		}

		// Token: 0x060038E7 RID: 14567 RVA: 0x00148878 File Offset: 0x00146A78
		private static void XDR_BuildGroup_MinOccurs(XdrBuilder builder, object obj, string prefix)
		{
			builder._GroupDef._MinVal = XdrBuilder.ParseMinOccurs(obj, builder);
			builder._GroupDef._HasMinAttr = true;
		}

		// Token: 0x060038E8 RID: 14568 RVA: 0x00148898 File Offset: 0x00146A98
		private static void XDR_BuildGroup_MaxOccurs(XdrBuilder builder, object obj, string prefix)
		{
			builder._GroupDef._MaxVal = XdrBuilder.ParseMaxOccurs(obj, builder);
			builder._GroupDef._HasMaxAttr = true;
		}

		// Token: 0x060038E9 RID: 14569 RVA: 0x001488B8 File Offset: 0x00146AB8
		private static void XDR_EndGroup(XdrBuilder builder)
		{
			if (!builder._ElementDef._ExistTerminal)
			{
				builder.SendValidationEvent("There is a missing element.");
			}
			builder._contentValidator.CloseGroup();
			if (builder._GroupDef._Order == 1)
			{
				builder._contentValidator.AddStar();
			}
			if (1 == builder._GroupDef._Order && builder._GroupDef._HasMaxAttr && builder._GroupDef._MaxVal != 4294967295U)
			{
				builder.SendValidationEvent("When the order is many, the maxOccurs attribute must have a value of '*'.");
			}
			XdrBuilder.HandleMinMax(builder._contentValidator, builder._GroupDef._MinVal, builder._GroupDef._MaxVal);
			builder.PopGroupInfo();
		}

		// Token: 0x060038EA RID: 14570 RVA: 0x0014895C File Offset: 0x00146B5C
		private static void XDR_InitElementDtType(XdrBuilder builder, object obj)
		{
			if (builder._ElementDef._HasDataType)
			{
				builder.SendValidationEvent("Data type has already been declared.");
			}
			if (!builder._ElementDef._AllowDataType)
			{
				builder.SendValidationEvent("Content must be \"textOnly\" when using DataType on an ElementType.");
			}
		}

		// Token: 0x060038EB RID: 14571 RVA: 0x00148990 File Offset: 0x00146B90
		private static void XDR_EndElementDtType(XdrBuilder builder)
		{
			if (!builder._ElementDef._HasDataType)
			{
				builder.SendValidationEvent("The '{0}' attribute is either invalid or missing.");
			}
			builder._ElementDef._ElementDecl.ContentValidator = ContentValidator.TextOnly;
			builder._ElementDef._ContentAttr = 2;
			builder._ElementDef._MasterGroupRequired = false;
			builder._contentValidator = null;
		}

		// Token: 0x060038EC RID: 14572 RVA: 0x001489E9 File Offset: 0x00146BE9
		private static void XDR_InitAttributeDtType(XdrBuilder builder, object obj)
		{
			if (builder._AttributeDef._HasDataType)
			{
				builder.SendValidationEvent("Data type has already been declared.");
			}
		}

		// Token: 0x060038ED RID: 14573 RVA: 0x00148A04 File Offset: 0x00146C04
		private static void XDR_EndAttributeDtType(XdrBuilder builder)
		{
			string text = null;
			if (!builder._AttributeDef._HasDataType)
			{
				text = "The '{0}' attribute is either invalid or missing.";
			}
			else if (builder._AttributeDef._AttDef.Datatype != null)
			{
				XmlTokenizedType tokenizedType = builder._AttributeDef._AttDef.Datatype.TokenizedType;
				if (tokenizedType == XmlTokenizedType.ENUMERATION && !builder._AttributeDef._EnumerationRequired)
				{
					text = "The dt:values attribute is missing.";
				}
				else if (tokenizedType != XmlTokenizedType.ENUMERATION && builder._AttributeDef._EnumerationRequired)
				{
					text = "Data type should be enumeration when the values attribute is present.";
				}
			}
			if (text != null)
			{
				builder.SendValidationEvent(text);
			}
		}

		// Token: 0x060038EE RID: 14574 RVA: 0x00148A8C File Offset: 0x00146C8C
		private bool GetNextState(XmlQualifiedName qname)
		{
			if (this._CurState._NextStates != null)
			{
				for (int i = 0; i < this._CurState._NextStates.Length; i++)
				{
					if (this._SchemaNames.TokenToQName[(int)XdrBuilder.S_SchemaEntries[this._CurState._NextStates[i]]._Name].Equals(qname))
					{
						this._NextState = XdrBuilder.S_SchemaEntries[this._CurState._NextStates[i]];
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x060038EF RID: 14575 RVA: 0x00148B08 File Offset: 0x00146D08
		private bool IsSkipableElement(XmlQualifiedName qname)
		{
			string @namespace = qname.Namespace;
			return (@namespace != null && !Ref.Equal(@namespace, this._SchemaNames.NsXdr)) || (this._SchemaNames.TokenToQName[38].Equals(qname) || this._SchemaNames.TokenToQName[39].Equals(qname));
		}

		// Token: 0x060038F0 RID: 14576 RVA: 0x00148B64 File Offset: 0x00146D64
		private bool IsSkipableAttribute(XmlQualifiedName qname)
		{
			string @namespace = qname.Namespace;
			return (@namespace.Length != 0 && !Ref.Equal(@namespace, this._SchemaNames.NsXdr) && !Ref.Equal(@namespace, this._SchemaNames.NsDataType)) || (Ref.Equal(@namespace, this._SchemaNames.NsDataType) && this._CurState._Name == SchemaNames.Token.XdrDatatype && (this._SchemaNames.QnDtMax.Equals(qname) || this._SchemaNames.QnDtMin.Equals(qname) || this._SchemaNames.QnDtMaxExclusive.Equals(qname) || this._SchemaNames.QnDtMinExclusive.Equals(qname)));
		}

		// Token: 0x060038F1 RID: 14577 RVA: 0x00148C1C File Offset: 0x00146E1C
		private int GetOrder(XmlQualifiedName qname)
		{
			int result = 0;
			if (this._SchemaNames.TokenToQName[15].Equals(qname))
			{
				result = 2;
			}
			else if (this._SchemaNames.TokenToQName[16].Equals(qname))
			{
				result = 3;
			}
			else if (this._SchemaNames.TokenToQName[17].Equals(qname))
			{
				result = 1;
			}
			else
			{
				this.SendValidationEvent("The order attribute must have a value of 'seq', 'one', or 'many', not '{0}'.", qname.Name);
			}
			return result;
		}

		// Token: 0x060038F2 RID: 14578 RVA: 0x00148C8C File Offset: 0x00146E8C
		private void AddOrder()
		{
			switch (this._GroupDef._Order)
			{
			case 1:
			case 3:
				this._contentValidator.AddChoice();
				return;
			case 2:
				this._contentValidator.AddSequence();
				return;
			}
			throw new XmlException("This is an unexpected token. The expected token is '{0}'.", "NAME");
		}

		// Token: 0x060038F3 RID: 14579 RVA: 0x00148CE8 File Offset: 0x00146EE8
		private static bool IsYes(object obj, XdrBuilder builder)
		{
			XmlQualifiedName xmlQualifiedName = (XmlQualifiedName)obj;
			bool result = false;
			if (xmlQualifiedName.Name == "yes")
			{
				result = true;
			}
			else if (xmlQualifiedName.Name != "no")
			{
				builder.SendValidationEvent("The required attribute must have a value of yes or no.");
			}
			return result;
		}

		// Token: 0x060038F4 RID: 14580 RVA: 0x00148D34 File Offset: 0x00146F34
		private static uint ParseMinOccurs(object obj, XdrBuilder builder)
		{
			uint num = 1U;
			if (!XdrBuilder.ParseInteger((string)obj, ref num) || (num != 0U && num != 1U))
			{
				builder.SendValidationEvent("The minOccurs attribute must have a value of 0 or 1.");
			}
			return num;
		}

		// Token: 0x060038F5 RID: 14581 RVA: 0x00148D68 File Offset: 0x00146F68
		private static uint ParseMaxOccurs(object obj, XdrBuilder builder)
		{
			uint maxValue = uint.MaxValue;
			string text = (string)obj;
			if (!text.Equals("*") && (!XdrBuilder.ParseInteger(text, ref maxValue) || (maxValue != 4294967295U && maxValue != 1U)))
			{
				builder.SendValidationEvent("The maxOccurs attribute must have a value of 1 or *.");
			}
			return maxValue;
		}

		// Token: 0x060038F6 RID: 14582 RVA: 0x00148DA9 File Offset: 0x00146FA9
		private static void HandleMinMax(ParticleContentValidator pContent, uint cMin, uint cMax)
		{
			if (pContent != null)
			{
				if (cMax == 4294967295U)
				{
					if (cMin == 0U)
					{
						pContent.AddStar();
						return;
					}
					pContent.AddPlus();
					return;
				}
				else if (cMin == 0U)
				{
					pContent.AddQMark();
				}
			}
		}

		// Token: 0x060038F7 RID: 14583 RVA: 0x00148DCC File Offset: 0x00146FCC
		private static void ParseDtMaxLength(ref uint cVal, object obj, XdrBuilder builder)
		{
			if (4294967295U != cVal)
			{
				builder.SendValidationEvent("The value of maxLength has already been declared.");
			}
			if (!XdrBuilder.ParseInteger((string)obj, ref cVal) || cVal < 0U)
			{
				builder.SendValidationEvent("The value '{0}' is invalid for dt:maxLength.", obj.ToString());
			}
		}

		// Token: 0x060038F8 RID: 14584 RVA: 0x00148E02 File Offset: 0x00147002
		private static void ParseDtMinLength(ref uint cVal, object obj, XdrBuilder builder)
		{
			if (4294967295U != cVal)
			{
				builder.SendValidationEvent("The value of minLength has already been declared.");
			}
			if (!XdrBuilder.ParseInteger((string)obj, ref cVal) || cVal < 0U)
			{
				builder.SendValidationEvent("The value '{0}' is invalid for dt:minLength.", obj.ToString());
			}
		}

		// Token: 0x060038F9 RID: 14585 RVA: 0x00148E38 File Offset: 0x00147038
		private static void CompareMinMaxLength(uint cMin, uint cMax, XdrBuilder builder)
		{
			if (cMin != 4294967295U && cMax != 4294967295U && cMin > cMax)
			{
				builder.SendValidationEvent("The maxLength value must be equal to or greater than the minLength value.");
			}
		}

		// Token: 0x060038FA RID: 14586 RVA: 0x00148E51 File Offset: 0x00147051
		private static bool ParseInteger(string str, ref uint n)
		{
			return uint.TryParse(str, NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite, NumberFormatInfo.InvariantInfo, out n);
		}

		// Token: 0x060038FB RID: 14587 RVA: 0x00148E60 File Offset: 0x00147060
		private void XDR_CheckAttributeDefault(XdrBuilder.DeclBaseInfo decl, SchemaAttDef pAttdef)
		{
			if ((decl._Default != null || pAttdef.DefaultValueTyped != null) && decl._Default != null)
			{
				pAttdef.DefaultValueRaw = (pAttdef.DefaultValueExpanded = (string)decl._Default);
				this.CheckDefaultAttValue(pAttdef);
			}
			this.SetAttributePresence(pAttdef, 1U == decl._MinOccurs);
		}

		// Token: 0x060038FC RID: 14588 RVA: 0x00148EB8 File Offset: 0x001470B8
		private void SetAttributePresence(SchemaAttDef pAttdef, bool fRequired)
		{
			if (SchemaDeclBase.Use.Fixed != pAttdef.Presence)
			{
				if (fRequired || SchemaDeclBase.Use.Required == pAttdef.Presence)
				{
					if (pAttdef.DefaultValueTyped != null)
					{
						pAttdef.Presence = SchemaDeclBase.Use.Fixed;
						return;
					}
					pAttdef.Presence = SchemaDeclBase.Use.Required;
					return;
				}
				else
				{
					if (pAttdef.DefaultValueTyped != null)
					{
						pAttdef.Presence = SchemaDeclBase.Use.Default;
						return;
					}
					pAttdef.Presence = SchemaDeclBase.Use.Implied;
				}
			}
		}

		// Token: 0x060038FD RID: 14589 RVA: 0x00148F0C File Offset: 0x0014710C
		private int GetContent(XmlQualifiedName qname)
		{
			int result = 0;
			if (this._SchemaNames.TokenToQName[11].Equals(qname))
			{
				result = 1;
				this._ElementDef._AllowDataType = false;
			}
			else if (this._SchemaNames.TokenToQName[12].Equals(qname))
			{
				result = 4;
				this._ElementDef._AllowDataType = false;
			}
			else if (this._SchemaNames.TokenToQName[10].Equals(qname))
			{
				result = 3;
				this._ElementDef._AllowDataType = false;
			}
			else if (this._SchemaNames.TokenToQName[13].Equals(qname))
			{
				result = 2;
			}
			else
			{
				this.SendValidationEvent("The content attribute must have a value of 'textOnly', 'eltOnly', 'mixed', or 'empty', not '{0}'.", qname.Name);
			}
			return result;
		}

		// Token: 0x060038FE RID: 14590 RVA: 0x00148FBC File Offset: 0x001471BC
		private bool GetModel(XmlQualifiedName qname)
		{
			bool result = false;
			if (this._SchemaNames.TokenToQName[7].Equals(qname))
			{
				result = true;
			}
			else if (this._SchemaNames.TokenToQName[8].Equals(qname))
			{
				result = false;
			}
			else
			{
				this.SendValidationEvent("The model attribute must have a value of open or closed, not '{0}'.", qname.Name);
			}
			return result;
		}

		// Token: 0x060038FF RID: 14591 RVA: 0x00149010 File Offset: 0x00147210
		private XmlSchemaDatatype CheckDatatype(string str)
		{
			XmlSchemaDatatype xmlSchemaDatatype = XmlSchemaDatatype.FromXdrName(str);
			if (xmlSchemaDatatype == null)
			{
				this.SendValidationEvent("Reference to an unknown data type, '{0}'.", str);
			}
			else if (xmlSchemaDatatype.TokenizedType == XmlTokenizedType.ID && !this._AttributeDef._Global)
			{
				if (this._ElementDef._ElementDecl.IsIdDeclared)
				{
					this.SendValidationEvent("The attribute of type ID is already declared on the '{0}' element.", XmlQualifiedName.ToString(this._ElementDef._ElementDecl.Name.Name, this._ElementDef._ElementDecl.Prefix));
				}
				this._ElementDef._ElementDecl.IsIdDeclared = true;
			}
			return xmlSchemaDatatype;
		}

		// Token: 0x06003900 RID: 14592 RVA: 0x001490A4 File Offset: 0x001472A4
		private void CheckDefaultAttValue(SchemaAttDef attDef)
		{
			XdrValidator.CheckDefaultValue(attDef.DefaultValueRaw.Trim(), attDef, this._SchemaInfo, this._CurNsMgr, this._NameTable, null, this.validationEventHandler, this._reader.BaseURI, this.positionInfo.LineNumber, this.positionInfo.LinePosition);
		}

		// Token: 0x06003901 RID: 14593 RVA: 0x001490FC File Offset: 0x001472FC
		private bool IsGlobal(int flags)
		{
			return flags == 256;
		}

		// Token: 0x06003902 RID: 14594 RVA: 0x00149106 File Offset: 0x00147306
		private void SendValidationEvent(string code, string[] args, XmlSeverityType severity)
		{
			this.SendValidationEvent(new XmlSchemaException(code, args, this._reader.BaseURI, this.positionInfo.LineNumber, this.positionInfo.LinePosition), severity);
		}

		// Token: 0x06003903 RID: 14595 RVA: 0x00149137 File Offset: 0x00147337
		private void SendValidationEvent(string code)
		{
			this.SendValidationEvent(code, string.Empty);
		}

		// Token: 0x06003904 RID: 14596 RVA: 0x00149145 File Offset: 0x00147345
		private void SendValidationEvent(string code, string msg)
		{
			this.SendValidationEvent(new XmlSchemaException(code, msg, this._reader.BaseURI, this.positionInfo.LineNumber, this.positionInfo.LinePosition), XmlSeverityType.Error);
		}

		// Token: 0x06003905 RID: 14597 RVA: 0x00149178 File Offset: 0x00147378
		private void SendValidationEvent(XmlSchemaException e, XmlSeverityType severity)
		{
			SchemaInfo schemaInfo = this._SchemaInfo;
			int errorCount = schemaInfo.ErrorCount;
			schemaInfo.ErrorCount = errorCount + 1;
			if (this.validationEventHandler != null)
			{
				this.validationEventHandler(this, new ValidationEventArgs(e, severity));
				return;
			}
			if (severity == XmlSeverityType.Error)
			{
				throw e;
			}
		}

		// Token: 0x06003906 RID: 14598 RVA: 0x001491BC File Offset: 0x001473BC
		// Note: this type is marked as 'beforefieldinit'.
		static XdrBuilder()
		{
		}

		// Token: 0x04002A20 RID: 10784
		private const int XdrSchema = 1;

		// Token: 0x04002A21 RID: 10785
		private const int XdrElementType = 2;

		// Token: 0x04002A22 RID: 10786
		private const int XdrAttributeType = 3;

		// Token: 0x04002A23 RID: 10787
		private const int XdrElement = 4;

		// Token: 0x04002A24 RID: 10788
		private const int XdrAttribute = 5;

		// Token: 0x04002A25 RID: 10789
		private const int XdrGroup = 6;

		// Token: 0x04002A26 RID: 10790
		private const int XdrElementDatatype = 7;

		// Token: 0x04002A27 RID: 10791
		private const int XdrAttributeDatatype = 8;

		// Token: 0x04002A28 RID: 10792
		private const int SchemaFlagsNs = 256;

		// Token: 0x04002A29 RID: 10793
		private const int StackIncrement = 10;

		// Token: 0x04002A2A RID: 10794
		private const int SchemaOrderNone = 0;

		// Token: 0x04002A2B RID: 10795
		private const int SchemaOrderMany = 1;

		// Token: 0x04002A2C RID: 10796
		private const int SchemaOrderSequence = 2;

		// Token: 0x04002A2D RID: 10797
		private const int SchemaOrderChoice = 3;

		// Token: 0x04002A2E RID: 10798
		private const int SchemaOrderAll = 4;

		// Token: 0x04002A2F RID: 10799
		private const int SchemaContentNone = 0;

		// Token: 0x04002A30 RID: 10800
		private const int SchemaContentEmpty = 1;

		// Token: 0x04002A31 RID: 10801
		private const int SchemaContentText = 2;

		// Token: 0x04002A32 RID: 10802
		private const int SchemaContentMixed = 3;

		// Token: 0x04002A33 RID: 10803
		private const int SchemaContentElement = 4;

		// Token: 0x04002A34 RID: 10804
		private static readonly int[] S_XDR_Root_Element = new int[]
		{
			1
		};

		// Token: 0x04002A35 RID: 10805
		private static readonly int[] S_XDR_Root_SubElements = new int[]
		{
			2,
			3
		};

		// Token: 0x04002A36 RID: 10806
		private static readonly int[] S_XDR_ElementType_SubElements = new int[]
		{
			4,
			6,
			3,
			5,
			7
		};

		// Token: 0x04002A37 RID: 10807
		private static readonly int[] S_XDR_AttributeType_SubElements = new int[]
		{
			8
		};

		// Token: 0x04002A38 RID: 10808
		private static readonly int[] S_XDR_Group_SubElements = new int[]
		{
			4,
			6
		};

		// Token: 0x04002A39 RID: 10809
		private static readonly XdrBuilder.XdrAttributeEntry[] S_XDR_Root_Attributes = new XdrBuilder.XdrAttributeEntry[]
		{
			new XdrBuilder.XdrAttributeEntry(SchemaNames.Token.SchemaName, XmlTokenizedType.CDATA, new XdrBuilder.XdrBuildFunction(XdrBuilder.XDR_BuildRoot_Name)),
			new XdrBuilder.XdrAttributeEntry(SchemaNames.Token.SchemaId, XmlTokenizedType.QName, new XdrBuilder.XdrBuildFunction(XdrBuilder.XDR_BuildRoot_ID))
		};

		// Token: 0x04002A3A RID: 10810
		private static readonly XdrBuilder.XdrAttributeEntry[] S_XDR_ElementType_Attributes = new XdrBuilder.XdrAttributeEntry[]
		{
			new XdrBuilder.XdrAttributeEntry(SchemaNames.Token.SchemaName, XmlTokenizedType.QName, 256, new XdrBuilder.XdrBuildFunction(XdrBuilder.XDR_BuildElementType_Name)),
			new XdrBuilder.XdrAttributeEntry(SchemaNames.Token.SchemaContent, XmlTokenizedType.QName, new XdrBuilder.XdrBuildFunction(XdrBuilder.XDR_BuildElementType_Content)),
			new XdrBuilder.XdrAttributeEntry(SchemaNames.Token.SchemaModel, XmlTokenizedType.QName, new XdrBuilder.XdrBuildFunction(XdrBuilder.XDR_BuildElementType_Model)),
			new XdrBuilder.XdrAttributeEntry(SchemaNames.Token.SchemaOrder, XmlTokenizedType.QName, new XdrBuilder.XdrBuildFunction(XdrBuilder.XDR_BuildElementType_Order)),
			new XdrBuilder.XdrAttributeEntry(SchemaNames.Token.SchemaDtType, XmlTokenizedType.CDATA, new XdrBuilder.XdrBuildFunction(XdrBuilder.XDR_BuildElementType_DtType)),
			new XdrBuilder.XdrAttributeEntry(SchemaNames.Token.SchemaDtValues, XmlTokenizedType.NMTOKENS, new XdrBuilder.XdrBuildFunction(XdrBuilder.XDR_BuildElementType_DtValues)),
			new XdrBuilder.XdrAttributeEntry(SchemaNames.Token.SchemaDtMaxLength, XmlTokenizedType.CDATA, new XdrBuilder.XdrBuildFunction(XdrBuilder.XDR_BuildElementType_DtMaxLength)),
			new XdrBuilder.XdrAttributeEntry(SchemaNames.Token.SchemaDtMinLength, XmlTokenizedType.CDATA, new XdrBuilder.XdrBuildFunction(XdrBuilder.XDR_BuildElementType_DtMinLength))
		};

		// Token: 0x04002A3B RID: 10811
		private static readonly XdrBuilder.XdrAttributeEntry[] S_XDR_AttributeType_Attributes = new XdrBuilder.XdrAttributeEntry[]
		{
			new XdrBuilder.XdrAttributeEntry(SchemaNames.Token.SchemaName, XmlTokenizedType.QName, new XdrBuilder.XdrBuildFunction(XdrBuilder.XDR_BuildAttributeType_Name)),
			new XdrBuilder.XdrAttributeEntry(SchemaNames.Token.SchemaRequired, XmlTokenizedType.QName, new XdrBuilder.XdrBuildFunction(XdrBuilder.XDR_BuildAttributeType_Required)),
			new XdrBuilder.XdrAttributeEntry(SchemaNames.Token.SchemaDefault, XmlTokenizedType.CDATA, new XdrBuilder.XdrBuildFunction(XdrBuilder.XDR_BuildAttributeType_Default)),
			new XdrBuilder.XdrAttributeEntry(SchemaNames.Token.SchemaDtType, XmlTokenizedType.QName, new XdrBuilder.XdrBuildFunction(XdrBuilder.XDR_BuildAttributeType_DtType)),
			new XdrBuilder.XdrAttributeEntry(SchemaNames.Token.SchemaDtValues, XmlTokenizedType.NMTOKENS, new XdrBuilder.XdrBuildFunction(XdrBuilder.XDR_BuildAttributeType_DtValues)),
			new XdrBuilder.XdrAttributeEntry(SchemaNames.Token.SchemaDtMaxLength, XmlTokenizedType.CDATA, new XdrBuilder.XdrBuildFunction(XdrBuilder.XDR_BuildAttributeType_DtMaxLength)),
			new XdrBuilder.XdrAttributeEntry(SchemaNames.Token.SchemaDtMinLength, XmlTokenizedType.CDATA, new XdrBuilder.XdrBuildFunction(XdrBuilder.XDR_BuildAttributeType_DtMinLength))
		};

		// Token: 0x04002A3C RID: 10812
		private static readonly XdrBuilder.XdrAttributeEntry[] S_XDR_Element_Attributes = new XdrBuilder.XdrAttributeEntry[]
		{
			new XdrBuilder.XdrAttributeEntry(SchemaNames.Token.SchemaType, XmlTokenizedType.QName, 256, new XdrBuilder.XdrBuildFunction(XdrBuilder.XDR_BuildElement_Type)),
			new XdrBuilder.XdrAttributeEntry(SchemaNames.Token.SchemaMinOccurs, XmlTokenizedType.CDATA, new XdrBuilder.XdrBuildFunction(XdrBuilder.XDR_BuildElement_MinOccurs)),
			new XdrBuilder.XdrAttributeEntry(SchemaNames.Token.SchemaMaxOccurs, XmlTokenizedType.CDATA, new XdrBuilder.XdrBuildFunction(XdrBuilder.XDR_BuildElement_MaxOccurs))
		};

		// Token: 0x04002A3D RID: 10813
		private static readonly XdrBuilder.XdrAttributeEntry[] S_XDR_Attribute_Attributes = new XdrBuilder.XdrAttributeEntry[]
		{
			new XdrBuilder.XdrAttributeEntry(SchemaNames.Token.SchemaType, XmlTokenizedType.QName, new XdrBuilder.XdrBuildFunction(XdrBuilder.XDR_BuildAttribute_Type)),
			new XdrBuilder.XdrAttributeEntry(SchemaNames.Token.SchemaRequired, XmlTokenizedType.QName, new XdrBuilder.XdrBuildFunction(XdrBuilder.XDR_BuildAttribute_Required)),
			new XdrBuilder.XdrAttributeEntry(SchemaNames.Token.SchemaDefault, XmlTokenizedType.CDATA, new XdrBuilder.XdrBuildFunction(XdrBuilder.XDR_BuildAttribute_Default))
		};

		// Token: 0x04002A3E RID: 10814
		private static readonly XdrBuilder.XdrAttributeEntry[] S_XDR_Group_Attributes = new XdrBuilder.XdrAttributeEntry[]
		{
			new XdrBuilder.XdrAttributeEntry(SchemaNames.Token.SchemaOrder, XmlTokenizedType.QName, new XdrBuilder.XdrBuildFunction(XdrBuilder.XDR_BuildGroup_Order)),
			new XdrBuilder.XdrAttributeEntry(SchemaNames.Token.SchemaMinOccurs, XmlTokenizedType.CDATA, new XdrBuilder.XdrBuildFunction(XdrBuilder.XDR_BuildGroup_MinOccurs)),
			new XdrBuilder.XdrAttributeEntry(SchemaNames.Token.SchemaMaxOccurs, XmlTokenizedType.CDATA, new XdrBuilder.XdrBuildFunction(XdrBuilder.XDR_BuildGroup_MaxOccurs))
		};

		// Token: 0x04002A3F RID: 10815
		private static readonly XdrBuilder.XdrAttributeEntry[] S_XDR_ElementDataType_Attributes = new XdrBuilder.XdrAttributeEntry[]
		{
			new XdrBuilder.XdrAttributeEntry(SchemaNames.Token.SchemaDtType, XmlTokenizedType.CDATA, new XdrBuilder.XdrBuildFunction(XdrBuilder.XDR_BuildElementType_DtType)),
			new XdrBuilder.XdrAttributeEntry(SchemaNames.Token.SchemaDtValues, XmlTokenizedType.NMTOKENS, new XdrBuilder.XdrBuildFunction(XdrBuilder.XDR_BuildElementType_DtValues)),
			new XdrBuilder.XdrAttributeEntry(SchemaNames.Token.SchemaDtMaxLength, XmlTokenizedType.CDATA, new XdrBuilder.XdrBuildFunction(XdrBuilder.XDR_BuildElementType_DtMaxLength)),
			new XdrBuilder.XdrAttributeEntry(SchemaNames.Token.SchemaDtMinLength, XmlTokenizedType.CDATA, new XdrBuilder.XdrBuildFunction(XdrBuilder.XDR_BuildElementType_DtMinLength))
		};

		// Token: 0x04002A40 RID: 10816
		private static readonly XdrBuilder.XdrAttributeEntry[] S_XDR_AttributeDataType_Attributes = new XdrBuilder.XdrAttributeEntry[]
		{
			new XdrBuilder.XdrAttributeEntry(SchemaNames.Token.SchemaDtType, XmlTokenizedType.QName, new XdrBuilder.XdrBuildFunction(XdrBuilder.XDR_BuildAttributeType_DtType)),
			new XdrBuilder.XdrAttributeEntry(SchemaNames.Token.SchemaDtValues, XmlTokenizedType.NMTOKENS, new XdrBuilder.XdrBuildFunction(XdrBuilder.XDR_BuildAttributeType_DtValues)),
			new XdrBuilder.XdrAttributeEntry(SchemaNames.Token.SchemaDtMaxLength, XmlTokenizedType.CDATA, new XdrBuilder.XdrBuildFunction(XdrBuilder.XDR_BuildAttributeType_DtMaxLength)),
			new XdrBuilder.XdrAttributeEntry(SchemaNames.Token.SchemaDtMinLength, XmlTokenizedType.CDATA, new XdrBuilder.XdrBuildFunction(XdrBuilder.XDR_BuildAttributeType_DtMinLength))
		};

		// Token: 0x04002A41 RID: 10817
		private static readonly XdrBuilder.XdrEntry[] S_SchemaEntries = new XdrBuilder.XdrEntry[]
		{
			new XdrBuilder.XdrEntry(SchemaNames.Token.Empty, XdrBuilder.S_XDR_Root_Element, null, null, null, null, false),
			new XdrBuilder.XdrEntry(SchemaNames.Token.XdrRoot, XdrBuilder.S_XDR_Root_SubElements, XdrBuilder.S_XDR_Root_Attributes, new XdrBuilder.XdrInitFunction(XdrBuilder.XDR_InitRoot), new XdrBuilder.XdrBeginChildFunction(XdrBuilder.XDR_BeginRoot), new XdrBuilder.XdrEndChildFunction(XdrBuilder.XDR_EndRoot), false),
			new XdrBuilder.XdrEntry(SchemaNames.Token.XdrElementType, XdrBuilder.S_XDR_ElementType_SubElements, XdrBuilder.S_XDR_ElementType_Attributes, new XdrBuilder.XdrInitFunction(XdrBuilder.XDR_InitElementType), new XdrBuilder.XdrBeginChildFunction(XdrBuilder.XDR_BeginElementType), new XdrBuilder.XdrEndChildFunction(XdrBuilder.XDR_EndElementType), false),
			new XdrBuilder.XdrEntry(SchemaNames.Token.XdrAttributeType, XdrBuilder.S_XDR_AttributeType_SubElements, XdrBuilder.S_XDR_AttributeType_Attributes, new XdrBuilder.XdrInitFunction(XdrBuilder.XDR_InitAttributeType), new XdrBuilder.XdrBeginChildFunction(XdrBuilder.XDR_BeginAttributeType), new XdrBuilder.XdrEndChildFunction(XdrBuilder.XDR_EndAttributeType), false),
			new XdrBuilder.XdrEntry(SchemaNames.Token.XdrElement, null, XdrBuilder.S_XDR_Element_Attributes, new XdrBuilder.XdrInitFunction(XdrBuilder.XDR_InitElement), null, new XdrBuilder.XdrEndChildFunction(XdrBuilder.XDR_EndElement), false),
			new XdrBuilder.XdrEntry(SchemaNames.Token.XdrAttribute, null, XdrBuilder.S_XDR_Attribute_Attributes, new XdrBuilder.XdrInitFunction(XdrBuilder.XDR_InitAttribute), new XdrBuilder.XdrBeginChildFunction(XdrBuilder.XDR_BeginAttribute), new XdrBuilder.XdrEndChildFunction(XdrBuilder.XDR_EndAttribute), false),
			new XdrBuilder.XdrEntry(SchemaNames.Token.XdrGroup, XdrBuilder.S_XDR_Group_SubElements, XdrBuilder.S_XDR_Group_Attributes, new XdrBuilder.XdrInitFunction(XdrBuilder.XDR_InitGroup), null, new XdrBuilder.XdrEndChildFunction(XdrBuilder.XDR_EndGroup), false),
			new XdrBuilder.XdrEntry(SchemaNames.Token.XdrDatatype, null, XdrBuilder.S_XDR_ElementDataType_Attributes, new XdrBuilder.XdrInitFunction(XdrBuilder.XDR_InitElementDtType), null, new XdrBuilder.XdrEndChildFunction(XdrBuilder.XDR_EndElementDtType), true),
			new XdrBuilder.XdrEntry(SchemaNames.Token.XdrDatatype, null, XdrBuilder.S_XDR_AttributeDataType_Attributes, new XdrBuilder.XdrInitFunction(XdrBuilder.XDR_InitAttributeDtType), null, new XdrBuilder.XdrEndChildFunction(XdrBuilder.XDR_EndAttributeDtType), true)
		};

		// Token: 0x04002A42 RID: 10818
		private SchemaInfo _SchemaInfo;

		// Token: 0x04002A43 RID: 10819
		private string _TargetNamespace;

		// Token: 0x04002A44 RID: 10820
		private XmlReader _reader;

		// Token: 0x04002A45 RID: 10821
		private PositionInfo positionInfo;

		// Token: 0x04002A46 RID: 10822
		private ParticleContentValidator _contentValidator;

		// Token: 0x04002A47 RID: 10823
		private XdrBuilder.XdrEntry _CurState;

		// Token: 0x04002A48 RID: 10824
		private XdrBuilder.XdrEntry _NextState;

		// Token: 0x04002A49 RID: 10825
		private HWStack _StateHistory;

		// Token: 0x04002A4A RID: 10826
		private HWStack _GroupStack;

		// Token: 0x04002A4B RID: 10827
		private string _XdrName;

		// Token: 0x04002A4C RID: 10828
		private string _XdrPrefix;

		// Token: 0x04002A4D RID: 10829
		private XdrBuilder.ElementContent _ElementDef;

		// Token: 0x04002A4E RID: 10830
		private XdrBuilder.GroupContent _GroupDef;

		// Token: 0x04002A4F RID: 10831
		private XdrBuilder.AttributeContent _AttributeDef;

		// Token: 0x04002A50 RID: 10832
		private XdrBuilder.DeclBaseInfo _UndefinedAttributeTypes;

		// Token: 0x04002A51 RID: 10833
		private XdrBuilder.DeclBaseInfo _BaseDecl;

		// Token: 0x04002A52 RID: 10834
		private XmlNameTable _NameTable;

		// Token: 0x04002A53 RID: 10835
		private SchemaNames _SchemaNames;

		// Token: 0x04002A54 RID: 10836
		private XmlNamespaceManager _CurNsMgr;

		// Token: 0x04002A55 RID: 10837
		private string _Text;

		// Token: 0x04002A56 RID: 10838
		private ValidationEventHandler validationEventHandler;

		// Token: 0x04002A57 RID: 10839
		private Hashtable _UndeclaredElements = new Hashtable();

		// Token: 0x04002A58 RID: 10840
		private const string x_schema = "x-schema:";

		// Token: 0x04002A59 RID: 10841
		private XmlResolver xmlResolver;

		// Token: 0x02000583 RID: 1411
		private sealed class DeclBaseInfo
		{
			// Token: 0x06003907 RID: 14599 RVA: 0x00149741 File Offset: 0x00147941
			internal DeclBaseInfo()
			{
				this.Reset();
			}

			// Token: 0x06003908 RID: 14600 RVA: 0x00149750 File Offset: 0x00147950
			internal void Reset()
			{
				this._Name = XmlQualifiedName.Empty;
				this._Prefix = null;
				this._TypeName = XmlQualifiedName.Empty;
				this._TypePrefix = null;
				this._Default = null;
				this._Revises = null;
				this._MaxOccurs = 1U;
				this._MinOccurs = 1U;
				this._Checking = false;
				this._ElementDecl = null;
				this._Next = null;
				this._Attdef = null;
			}

			// Token: 0x04002A5A RID: 10842
			internal XmlQualifiedName _Name;

			// Token: 0x04002A5B RID: 10843
			internal string _Prefix;

			// Token: 0x04002A5C RID: 10844
			internal XmlQualifiedName _TypeName;

			// Token: 0x04002A5D RID: 10845
			internal string _TypePrefix;

			// Token: 0x04002A5E RID: 10846
			internal object _Default;

			// Token: 0x04002A5F RID: 10847
			internal object _Revises;

			// Token: 0x04002A60 RID: 10848
			internal uint _MaxOccurs;

			// Token: 0x04002A61 RID: 10849
			internal uint _MinOccurs;

			// Token: 0x04002A62 RID: 10850
			internal bool _Checking;

			// Token: 0x04002A63 RID: 10851
			internal SchemaElementDecl _ElementDecl;

			// Token: 0x04002A64 RID: 10852
			internal SchemaAttDef _Attdef;

			// Token: 0x04002A65 RID: 10853
			internal XdrBuilder.DeclBaseInfo _Next;
		}

		// Token: 0x02000584 RID: 1412
		private sealed class GroupContent
		{
			// Token: 0x06003909 RID: 14601 RVA: 0x001497B9 File Offset: 0x001479B9
			internal static void Copy(XdrBuilder.GroupContent from, XdrBuilder.GroupContent to)
			{
				to._MinVal = from._MinVal;
				to._MaxVal = from._MaxVal;
				to._Order = from._Order;
			}

			// Token: 0x0600390A RID: 14602 RVA: 0x001497E0 File Offset: 0x001479E0
			internal static XdrBuilder.GroupContent Copy(XdrBuilder.GroupContent other)
			{
				XdrBuilder.GroupContent groupContent = new XdrBuilder.GroupContent();
				XdrBuilder.GroupContent.Copy(other, groupContent);
				return groupContent;
			}

			// Token: 0x0600390B RID: 14603 RVA: 0x0000216B File Offset: 0x0000036B
			public GroupContent()
			{
			}

			// Token: 0x04002A66 RID: 10854
			internal uint _MinVal;

			// Token: 0x04002A67 RID: 10855
			internal uint _MaxVal;

			// Token: 0x04002A68 RID: 10856
			internal bool _HasMaxAttr;

			// Token: 0x04002A69 RID: 10857
			internal bool _HasMinAttr;

			// Token: 0x04002A6A RID: 10858
			internal int _Order;
		}

		// Token: 0x02000585 RID: 1413
		private sealed class ElementContent
		{
			// Token: 0x0600390C RID: 14604 RVA: 0x0000216B File Offset: 0x0000036B
			public ElementContent()
			{
			}

			// Token: 0x04002A6B RID: 10859
			internal SchemaElementDecl _ElementDecl;

			// Token: 0x04002A6C RID: 10860
			internal int _ContentAttr;

			// Token: 0x04002A6D RID: 10861
			internal int _OrderAttr;

			// Token: 0x04002A6E RID: 10862
			internal bool _MasterGroupRequired;

			// Token: 0x04002A6F RID: 10863
			internal bool _ExistTerminal;

			// Token: 0x04002A70 RID: 10864
			internal bool _AllowDataType;

			// Token: 0x04002A71 RID: 10865
			internal bool _HasDataType;

			// Token: 0x04002A72 RID: 10866
			internal bool _HasType;

			// Token: 0x04002A73 RID: 10867
			internal bool _EnumerationRequired;

			// Token: 0x04002A74 RID: 10868
			internal uint _MinVal;

			// Token: 0x04002A75 RID: 10869
			internal uint _MaxVal;

			// Token: 0x04002A76 RID: 10870
			internal uint _MaxLength;

			// Token: 0x04002A77 RID: 10871
			internal uint _MinLength;

			// Token: 0x04002A78 RID: 10872
			internal Hashtable _AttDefList;
		}

		// Token: 0x02000586 RID: 1414
		private sealed class AttributeContent
		{
			// Token: 0x0600390D RID: 14605 RVA: 0x0000216B File Offset: 0x0000036B
			public AttributeContent()
			{
			}

			// Token: 0x04002A79 RID: 10873
			internal SchemaAttDef _AttDef;

			// Token: 0x04002A7A RID: 10874
			internal XmlQualifiedName _Name;

			// Token: 0x04002A7B RID: 10875
			internal string _Prefix;

			// Token: 0x04002A7C RID: 10876
			internal bool _Required;

			// Token: 0x04002A7D RID: 10877
			internal uint _MinVal;

			// Token: 0x04002A7E RID: 10878
			internal uint _MaxVal;

			// Token: 0x04002A7F RID: 10879
			internal uint _MaxLength;

			// Token: 0x04002A80 RID: 10880
			internal uint _MinLength;

			// Token: 0x04002A81 RID: 10881
			internal bool _EnumerationRequired;

			// Token: 0x04002A82 RID: 10882
			internal bool _HasDataType;

			// Token: 0x04002A83 RID: 10883
			internal bool _Global;

			// Token: 0x04002A84 RID: 10884
			internal object _Default;
		}

		// Token: 0x02000587 RID: 1415
		// (Invoke) Token: 0x0600390F RID: 14607
		private delegate void XdrBuildFunction(XdrBuilder builder, object obj, string prefix);

		// Token: 0x02000588 RID: 1416
		// (Invoke) Token: 0x06003913 RID: 14611
		private delegate void XdrInitFunction(XdrBuilder builder, object obj);

		// Token: 0x02000589 RID: 1417
		// (Invoke) Token: 0x06003917 RID: 14615
		private delegate void XdrBeginChildFunction(XdrBuilder builder);

		// Token: 0x0200058A RID: 1418
		// (Invoke) Token: 0x0600391B RID: 14619
		private delegate void XdrEndChildFunction(XdrBuilder builder);

		// Token: 0x0200058B RID: 1419
		private sealed class XdrAttributeEntry
		{
			// Token: 0x0600391E RID: 14622 RVA: 0x001497FB File Offset: 0x001479FB
			internal XdrAttributeEntry(SchemaNames.Token a, XmlTokenizedType ttype, XdrBuilder.XdrBuildFunction build)
			{
				this._Attribute = a;
				this._Datatype = XmlSchemaDatatype.FromXmlTokenizedType(ttype);
				this._SchemaFlags = 0;
				this._BuildFunc = build;
			}

			// Token: 0x0600391F RID: 14623 RVA: 0x00149824 File Offset: 0x00147A24
			internal XdrAttributeEntry(SchemaNames.Token a, XmlTokenizedType ttype, int schemaFlags, XdrBuilder.XdrBuildFunction build)
			{
				this._Attribute = a;
				this._Datatype = XmlSchemaDatatype.FromXmlTokenizedType(ttype);
				this._SchemaFlags = schemaFlags;
				this._BuildFunc = build;
			}

			// Token: 0x04002A85 RID: 10885
			internal SchemaNames.Token _Attribute;

			// Token: 0x04002A86 RID: 10886
			internal int _SchemaFlags;

			// Token: 0x04002A87 RID: 10887
			internal XmlSchemaDatatype _Datatype;

			// Token: 0x04002A88 RID: 10888
			internal XdrBuilder.XdrBuildFunction _BuildFunc;
		}

		// Token: 0x0200058C RID: 1420
		private sealed class XdrEntry
		{
			// Token: 0x06003920 RID: 14624 RVA: 0x0014984E File Offset: 0x00147A4E
			internal XdrEntry(SchemaNames.Token n, int[] states, XdrBuilder.XdrAttributeEntry[] attributes, XdrBuilder.XdrInitFunction init, XdrBuilder.XdrBeginChildFunction begin, XdrBuilder.XdrEndChildFunction end, bool fText)
			{
				this._Name = n;
				this._NextStates = states;
				this._Attributes = attributes;
				this._InitFunc = init;
				this._BeginChildFunc = begin;
				this._EndChildFunc = end;
				this._AllowText = fText;
			}

			// Token: 0x04002A89 RID: 10889
			internal SchemaNames.Token _Name;

			// Token: 0x04002A8A RID: 10890
			internal int[] _NextStates;

			// Token: 0x04002A8B RID: 10891
			internal XdrBuilder.XdrAttributeEntry[] _Attributes;

			// Token: 0x04002A8C RID: 10892
			internal XdrBuilder.XdrInitFunction _InitFunc;

			// Token: 0x04002A8D RID: 10893
			internal XdrBuilder.XdrBeginChildFunction _BeginChildFunc;

			// Token: 0x04002A8E RID: 10894
			internal XdrBuilder.XdrEndChildFunction _EndChildFunc;

			// Token: 0x04002A8F RID: 10895
			internal bool _AllowText;
		}
	}
}

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace System.Xml
{
	// Token: 0x020001EA RID: 490
	internal class DtdParser : IDtdParser
	{
		// Token: 0x06001337 RID: 4919 RVA: 0x0000B528 File Offset: 0x00009728
		static DtdParser()
		{
		}

		// Token: 0x06001338 RID: 4920 RVA: 0x00071150 File Offset: 0x0006F350
		private DtdParser()
		{
		}

		// Token: 0x06001339 RID: 4921 RVA: 0x000711BD File Offset: 0x0006F3BD
		internal static IDtdParser Create()
		{
			return new DtdParser();
		}

		// Token: 0x0600133A RID: 4922 RVA: 0x000711C4 File Offset: 0x0006F3C4
		private void Initialize(IDtdParserAdapter readerAdapter)
		{
			this.readerAdapter = readerAdapter;
			this.readerAdapterWithValidation = (readerAdapter as IDtdParserAdapterWithValidation);
			this.nameTable = readerAdapter.NameTable;
			IDtdParserAdapterWithValidation dtdParserAdapterWithValidation = readerAdapter as IDtdParserAdapterWithValidation;
			if (dtdParserAdapterWithValidation != null)
			{
				this.validate = dtdParserAdapterWithValidation.DtdValidation;
			}
			IDtdParserAdapterV1 dtdParserAdapterV = readerAdapter as IDtdParserAdapterV1;
			if (dtdParserAdapterV != null)
			{
				this.v1Compat = dtdParserAdapterV.V1CompatibilityMode;
				this.normalize = dtdParserAdapterV.Normalization;
				this.supportNamespaces = dtdParserAdapterV.Namespaces;
			}
			this.schemaInfo = new SchemaInfo();
			this.schemaInfo.SchemaType = SchemaType.DTD;
			this.stringBuilder = new StringBuilder();
			Uri baseUri = readerAdapter.BaseUri;
			if (baseUri != null)
			{
				this.documentBaseUri = baseUri.ToString();
			}
			this.freeFloatingDtd = false;
		}

		// Token: 0x0600133B RID: 4923 RVA: 0x0007127C File Offset: 0x0006F47C
		private void InitializeFreeFloatingDtd(string baseUri, string docTypeName, string publicId, string systemId, string internalSubset, IDtdParserAdapter adapter)
		{
			this.Initialize(adapter);
			if (docTypeName == null || docTypeName.Length == 0)
			{
				throw XmlConvert.CreateInvalidNameArgumentException(docTypeName, "docTypeName");
			}
			XmlConvert.VerifyName(docTypeName);
			int num = docTypeName.IndexOf(':');
			if (num == -1)
			{
				this.schemaInfo.DocTypeName = new XmlQualifiedName(this.nameTable.Add(docTypeName));
			}
			else
			{
				this.schemaInfo.DocTypeName = new XmlQualifiedName(this.nameTable.Add(docTypeName.Substring(0, num)), this.nameTable.Add(docTypeName.Substring(num + 1)));
			}
			if (systemId != null && systemId.Length > 0)
			{
				int invCharPos;
				if ((invCharPos = this.xmlCharType.IsOnlyCharData(systemId)) >= 0)
				{
					this.ThrowInvalidChar(this.curPos, systemId, invCharPos);
				}
				this.systemId = systemId;
			}
			if (publicId != null && publicId.Length > 0)
			{
				int invCharPos;
				if ((invCharPos = this.xmlCharType.IsPublicId(publicId)) >= 0)
				{
					this.ThrowInvalidChar(this.curPos, publicId, invCharPos);
				}
				this.publicId = publicId;
			}
			if (internalSubset != null && internalSubset.Length > 0)
			{
				this.readerAdapter.PushInternalDtd(baseUri, internalSubset);
				this.hasFreeFloatingInternalSubset = true;
			}
			Uri baseUri2 = this.readerAdapter.BaseUri;
			if (baseUri2 != null)
			{
				this.documentBaseUri = baseUri2.ToString();
			}
			this.freeFloatingDtd = true;
		}

		// Token: 0x0600133C RID: 4924 RVA: 0x000713C5 File Offset: 0x0006F5C5
		IDtdInfo IDtdParser.ParseInternalDtd(IDtdParserAdapter adapter, bool saveInternalSubset)
		{
			this.Initialize(adapter);
			this.Parse(saveInternalSubset);
			return this.schemaInfo;
		}

		// Token: 0x0600133D RID: 4925 RVA: 0x000713DB File Offset: 0x0006F5DB
		IDtdInfo IDtdParser.ParseFreeFloatingDtd(string baseUri, string docTypeName, string publicId, string systemId, string internalSubset, IDtdParserAdapter adapter)
		{
			this.InitializeFreeFloatingDtd(baseUri, docTypeName, publicId, systemId, internalSubset, adapter);
			this.Parse(false);
			return this.schemaInfo;
		}

		// Token: 0x170003AF RID: 943
		// (get) Token: 0x0600133E RID: 4926 RVA: 0x000713F9 File Offset: 0x0006F5F9
		private bool ParsingInternalSubset
		{
			get
			{
				return this.externalEntitiesDepth == 0;
			}
		}

		// Token: 0x170003B0 RID: 944
		// (get) Token: 0x0600133F RID: 4927 RVA: 0x00071404 File Offset: 0x0006F604
		private bool IgnoreEntityReferences
		{
			get
			{
				return this.scanningFunction == DtdParser.ScanningFunction.CondSection3;
			}
		}

		// Token: 0x170003B1 RID: 945
		// (get) Token: 0x06001340 RID: 4928 RVA: 0x00071410 File Offset: 0x0006F610
		private bool SaveInternalSubsetValue
		{
			get
			{
				return this.readerAdapter.EntityStackLength == 0 && this.internalSubsetValueSb != null;
			}
		}

		// Token: 0x170003B2 RID: 946
		// (get) Token: 0x06001341 RID: 4929 RVA: 0x0007142A File Offset: 0x0006F62A
		private bool ParsingTopLevelMarkup
		{
			get
			{
				return this.scanningFunction == DtdParser.ScanningFunction.SubsetContent || (this.scanningFunction == DtdParser.ScanningFunction.ParamEntitySpace && this.savedScanningFunction == DtdParser.ScanningFunction.SubsetContent);
			}
		}

		// Token: 0x170003B3 RID: 947
		// (get) Token: 0x06001342 RID: 4930 RVA: 0x0007144B File Offset: 0x0006F64B
		private bool SupportNamespaces
		{
			get
			{
				return this.supportNamespaces;
			}
		}

		// Token: 0x170003B4 RID: 948
		// (get) Token: 0x06001343 RID: 4931 RVA: 0x00071453 File Offset: 0x0006F653
		private bool Normalize
		{
			get
			{
				return this.normalize;
			}
		}

		// Token: 0x06001344 RID: 4932 RVA: 0x0007145C File Offset: 0x0006F65C
		private void Parse(bool saveInternalSubset)
		{
			if (this.freeFloatingDtd)
			{
				this.ParseFreeFloatingDtd();
			}
			else
			{
				this.ParseInDocumentDtd(saveInternalSubset);
			}
			this.schemaInfo.Finish();
			if (this.validate && this.undeclaredNotations != null)
			{
				foreach (DtdParser.UndeclaredNotation undeclaredNotation in this.undeclaredNotations.Values)
				{
					for (DtdParser.UndeclaredNotation undeclaredNotation2 = undeclaredNotation; undeclaredNotation2 != null; undeclaredNotation2 = undeclaredNotation2.next)
					{
						this.SendValidationEvent(XmlSeverityType.Error, new XmlSchemaException("The '{0}' notation is not declared.", undeclaredNotation.name, this.BaseUriStr, undeclaredNotation.lineNo, undeclaredNotation.linePos));
					}
				}
			}
		}

		// Token: 0x06001345 RID: 4933 RVA: 0x00071518 File Offset: 0x0006F718
		private void ParseInDocumentDtd(bool saveInternalSubset)
		{
			this.LoadParsingBuffer();
			this.scanningFunction = DtdParser.ScanningFunction.QName;
			this.nextScaningFunction = DtdParser.ScanningFunction.Doctype1;
			if (this.GetToken(false) != DtdParser.Token.QName)
			{
				this.OnUnexpectedError();
			}
			this.schemaInfo.DocTypeName = this.GetNameQualified(true);
			DtdParser.Token token = this.GetToken(false);
			if (token == DtdParser.Token.SYSTEM || token == DtdParser.Token.PUBLIC)
			{
				this.ParseExternalId(token, DtdParser.Token.DOCTYPE, out this.publicId, out this.systemId);
				token = this.GetToken(false);
			}
			if (token != DtdParser.Token.GreaterThan)
			{
				if (token == DtdParser.Token.LeftBracket)
				{
					if (saveInternalSubset)
					{
						this.SaveParsingBuffer();
						this.internalSubsetValueSb = new StringBuilder();
					}
					this.ParseInternalSubset();
				}
				else
				{
					this.OnUnexpectedError();
				}
			}
			this.SaveParsingBuffer();
			if (this.systemId != null && this.systemId.Length > 0)
			{
				this.ParseExternalSubset();
			}
		}

		// Token: 0x06001346 RID: 4934 RVA: 0x000715D9 File Offset: 0x0006F7D9
		private void ParseFreeFloatingDtd()
		{
			if (this.hasFreeFloatingInternalSubset)
			{
				this.LoadParsingBuffer();
				this.ParseInternalSubset();
				this.SaveParsingBuffer();
			}
			if (this.systemId != null && this.systemId.Length > 0)
			{
				this.ParseExternalSubset();
			}
		}

		// Token: 0x06001347 RID: 4935 RVA: 0x00071611 File Offset: 0x0006F811
		private void ParseInternalSubset()
		{
			this.ParseSubset();
		}

		// Token: 0x06001348 RID: 4936 RVA: 0x0007161C File Offset: 0x0006F81C
		private void ParseExternalSubset()
		{
			if (!this.readerAdapter.PushExternalSubset(this.systemId, this.publicId))
			{
				return;
			}
			Uri baseUri = this.readerAdapter.BaseUri;
			if (baseUri != null)
			{
				this.externalDtdBaseUri = baseUri.ToString();
			}
			this.externalEntitiesDepth++;
			this.LoadParsingBuffer();
			this.ParseSubset();
		}

		// Token: 0x06001349 RID: 4937 RVA: 0x00071680 File Offset: 0x0006F880
		private void ParseSubset()
		{
			for (;;)
			{
				DtdParser.Token token = this.GetToken(false);
				int num = this.currentEntityId;
				switch (token)
				{
				case DtdParser.Token.AttlistDecl:
					this.ParseAttlistDecl();
					break;
				case DtdParser.Token.ElementDecl:
					this.ParseElementDecl();
					break;
				case DtdParser.Token.EntityDecl:
					this.ParseEntityDecl();
					break;
				case DtdParser.Token.NotationDecl:
					this.ParseNotationDecl();
					break;
				case DtdParser.Token.Comment:
					this.ParseComment();
					break;
				case DtdParser.Token.PI:
					this.ParsePI();
					break;
				case DtdParser.Token.CondSectionStart:
					if (this.ParsingInternalSubset)
					{
						this.Throw(this.curPos - 3, "A conditional section is not allowed in an internal subset.");
					}
					this.ParseCondSection();
					num = this.currentEntityId;
					break;
				case DtdParser.Token.CondSectionEnd:
					if (this.condSectionDepth > 0)
					{
						this.condSectionDepth--;
						if (this.validate && this.currentEntityId != this.condSectionEntityIds[this.condSectionDepth])
						{
							this.SendValidationEvent(this.curPos, XmlSeverityType.Error, "The parameter entity replacement text must nest properly within markup declarations.", string.Empty);
						}
					}
					else
					{
						this.Throw(this.curPos - 3, "']]>' is not expected.");
					}
					break;
				case DtdParser.Token.Eof:
					goto IL_1A9;
				default:
					if (token == DtdParser.Token.RightBracket)
					{
						goto IL_126;
					}
					break;
				}
				if (this.currentEntityId != num)
				{
					if (this.validate)
					{
						this.SendValidationEvent(this.curPos, XmlSeverityType.Error, "The parameter entity replacement text must nest properly within markup declarations.", string.Empty);
					}
					else if (!this.v1Compat)
					{
						this.Throw(this.curPos, "The parameter entity replacement text must nest properly within markup declarations.");
					}
				}
			}
			IL_126:
			if (this.ParsingInternalSubset)
			{
				if (this.condSectionDepth != 0)
				{
					this.Throw(this.curPos, "There is an unclosed conditional section.");
				}
				if (this.internalSubsetValueSb != null)
				{
					this.SaveParsingBuffer(this.curPos - 1);
					this.schemaInfo.InternalDtdSubset = this.internalSubsetValueSb.ToString();
					this.internalSubsetValueSb = null;
				}
				if (this.GetToken(false) != DtdParser.Token.GreaterThan)
				{
					this.ThrowUnexpectedToken(this.curPos, ">");
					return;
				}
			}
			else
			{
				this.Throw(this.curPos, "Expected DTD markup was not found.");
			}
			return;
			IL_1A9:
			if (this.ParsingInternalSubset && !this.freeFloatingDtd)
			{
				this.Throw(this.curPos, "Incomplete DTD content.");
			}
			if (this.condSectionDepth != 0)
			{
				this.Throw(this.curPos, "There is an unclosed conditional section.");
			}
		}

		// Token: 0x0600134A RID: 4938 RVA: 0x000718C4 File Offset: 0x0006FAC4
		private void ParseAttlistDecl()
		{
			if (this.GetToken(true) == DtdParser.Token.QName)
			{
				XmlQualifiedName nameQualified = this.GetNameQualified(true);
				SchemaElementDecl schemaElementDecl;
				if (!this.schemaInfo.ElementDecls.TryGetValue(nameQualified, out schemaElementDecl) && !this.schemaInfo.UndeclaredElementDecls.TryGetValue(nameQualified, out schemaElementDecl))
				{
					schemaElementDecl = new SchemaElementDecl(nameQualified, nameQualified.Namespace);
					this.schemaInfo.UndeclaredElementDecls.Add(nameQualified, schemaElementDecl);
				}
				SchemaAttDef schemaAttDef = null;
				DtdParser.Token token;
				for (;;)
				{
					token = this.GetToken(false);
					if (token != DtdParser.Token.QName)
					{
						break;
					}
					XmlQualifiedName nameQualified2 = this.GetNameQualified(true);
					schemaAttDef = new SchemaAttDef(nameQualified2, nameQualified2.Namespace);
					schemaAttDef.IsDeclaredInExternal = !this.ParsingInternalSubset;
					schemaAttDef.LineNumber = this.LineNo;
					schemaAttDef.LinePosition = this.LinePos - (this.curPos - this.tokenStartPos);
					bool flag = schemaElementDecl.GetAttDef(schemaAttDef.Name) != null;
					this.ParseAttlistType(schemaAttDef, schemaElementDecl, flag);
					this.ParseAttlistDefault(schemaAttDef, flag);
					if (schemaAttDef.Prefix.Length > 0 && schemaAttDef.Prefix.Equals("xml"))
					{
						if (schemaAttDef.Name.Name == "space")
						{
							if (this.v1Compat)
							{
								string text = schemaAttDef.DefaultValueExpanded.Trim();
								if (text.Equals("preserve") || text.Equals("default"))
								{
									schemaAttDef.Reserved = SchemaAttDef.Reserve.XmlSpace;
								}
							}
							else
							{
								schemaAttDef.Reserved = SchemaAttDef.Reserve.XmlSpace;
								if (schemaAttDef.TokenizedType != XmlTokenizedType.ENUMERATION)
								{
									this.Throw("Enumeration data type required.", string.Empty, schemaAttDef.LineNumber, schemaAttDef.LinePosition);
								}
								if (this.validate)
								{
									schemaAttDef.CheckXmlSpace(this.readerAdapterWithValidation.ValidationEventHandling);
								}
							}
						}
						else if (schemaAttDef.Name.Name == "lang")
						{
							schemaAttDef.Reserved = SchemaAttDef.Reserve.XmlLang;
						}
					}
					if (!flag)
					{
						schemaElementDecl.AddAttDef(schemaAttDef);
					}
				}
				if (token == DtdParser.Token.GreaterThan)
				{
					if (this.v1Compat && schemaAttDef != null && schemaAttDef.Prefix.Length > 0 && schemaAttDef.Prefix.Equals("xml") && schemaAttDef.Name.Name == "space")
					{
						schemaAttDef.Reserved = SchemaAttDef.Reserve.XmlSpace;
						if (schemaAttDef.Datatype.TokenizedType != XmlTokenizedType.ENUMERATION)
						{
							this.Throw("Enumeration data type required.", string.Empty, schemaAttDef.LineNumber, schemaAttDef.LinePosition);
						}
						if (this.validate)
						{
							schemaAttDef.CheckXmlSpace(this.readerAdapterWithValidation.ValidationEventHandling);
						}
					}
					return;
				}
			}
			this.OnUnexpectedError();
		}

		// Token: 0x0600134B RID: 4939 RVA: 0x00071B48 File Offset: 0x0006FD48
		private void ParseAttlistType(SchemaAttDef attrDef, SchemaElementDecl elementDecl, bool ignoreErrors)
		{
			DtdParser.Token token = this.GetToken(true);
			if (token != DtdParser.Token.CDATA)
			{
				elementDecl.HasNonCDataAttribute = true;
			}
			if (this.IsAttributeValueType(token))
			{
				attrDef.TokenizedType = (XmlTokenizedType)token;
				attrDef.SchemaType = XmlSchemaType.GetBuiltInSimpleType(attrDef.Datatype.TypeCode);
				if (token == DtdParser.Token.ID)
				{
					if (this.validate && elementDecl.IsIdDeclared)
					{
						SchemaAttDef attDef = elementDecl.GetAttDef(attrDef.Name);
						if ((attDef == null || attDef.Datatype.TokenizedType != XmlTokenizedType.ID) && !ignoreErrors)
						{
							this.SendValidationEvent(XmlSeverityType.Error, "The attribute of type ID is already declared on the '{0}' element.", elementDecl.Name.ToString());
						}
					}
					elementDecl.IsIdDeclared = true;
					return;
				}
				if (token != DtdParser.Token.NOTATION)
				{
					return;
				}
				if (this.validate)
				{
					if (elementDecl.IsNotationDeclared && !ignoreErrors)
					{
						this.SendValidationEvent(this.curPos - 8, XmlSeverityType.Error, "No element type can have more than one NOTATION attribute specified.", elementDecl.Name.ToString());
					}
					else
					{
						if (elementDecl.ContentValidator != null && elementDecl.ContentValidator.ContentType == XmlSchemaContentType.Empty && !ignoreErrors)
						{
							this.SendValidationEvent(this.curPos - 8, XmlSeverityType.Error, "An attribute of type NOTATION must not be declared on an element declared EMPTY.", elementDecl.Name.ToString());
						}
						elementDecl.IsNotationDeclared = true;
					}
				}
				if (this.GetToken(true) == DtdParser.Token.LeftParen && this.GetToken(false) == DtdParser.Token.Name)
				{
					do
					{
						string nameString = this.GetNameString();
						if (!this.schemaInfo.Notations.ContainsKey(nameString))
						{
							this.AddUndeclaredNotation(nameString);
						}
						if (this.validate && !this.v1Compat && attrDef.Values != null && attrDef.Values.Contains(nameString) && !ignoreErrors)
						{
							this.SendValidationEvent(XmlSeverityType.Error, new XmlSchemaException("'{0}' is a duplicate notation value.", nameString, this.BaseUriStr, this.LineNo, this.LinePos));
						}
						attrDef.AddValue(nameString);
						DtdParser.Token token2 = this.GetToken(false);
						if (token2 == DtdParser.Token.RightParen)
						{
							return;
						}
						if (token2 != DtdParser.Token.Or)
						{
							break;
						}
					}
					while (this.GetToken(false) == DtdParser.Token.Name);
				}
			}
			else if (token == DtdParser.Token.LeftParen)
			{
				attrDef.TokenizedType = XmlTokenizedType.ENUMERATION;
				attrDef.SchemaType = XmlSchemaType.GetBuiltInSimpleType(attrDef.Datatype.TypeCode);
				if (this.GetToken(false) == DtdParser.Token.Nmtoken)
				{
					attrDef.AddValue(this.GetNameString());
					for (;;)
					{
						DtdParser.Token token2 = this.GetToken(false);
						if (token2 == DtdParser.Token.RightParen)
						{
							break;
						}
						if (token2 != DtdParser.Token.Or || this.GetToken(false) != DtdParser.Token.Nmtoken)
						{
							goto IL_280;
						}
						string nmtokenString = this.GetNmtokenString();
						if (this.validate && !this.v1Compat && attrDef.Values != null && attrDef.Values.Contains(nmtokenString) && !ignoreErrors)
						{
							this.SendValidationEvent(XmlSeverityType.Error, new XmlSchemaException("'{0}' is a duplicate enumeration value.", nmtokenString, this.BaseUriStr, this.LineNo, this.LinePos));
						}
						attrDef.AddValue(nmtokenString);
					}
					return;
				}
			}
			IL_280:
			this.OnUnexpectedError();
		}

		// Token: 0x0600134C RID: 4940 RVA: 0x00071DDC File Offset: 0x0006FFDC
		private void ParseAttlistDefault(SchemaAttDef attrDef, bool ignoreErrors)
		{
			DtdParser.Token token = this.GetToken(true);
			switch (token)
			{
			case DtdParser.Token.REQUIRED:
				attrDef.Presence = SchemaDeclBase.Use.Required;
				return;
			case DtdParser.Token.IMPLIED:
				attrDef.Presence = SchemaDeclBase.Use.Implied;
				return;
			case DtdParser.Token.FIXED:
				attrDef.Presence = SchemaDeclBase.Use.Fixed;
				if (this.GetToken(true) != DtdParser.Token.Literal)
				{
					goto IL_CF;
				}
				break;
			default:
				if (token != DtdParser.Token.Literal)
				{
					goto IL_CF;
				}
				break;
			}
			if (this.validate && attrDef.Datatype.TokenizedType == XmlTokenizedType.ID && !ignoreErrors)
			{
				this.SendValidationEvent(this.curPos, XmlSeverityType.Error, "An attribute of type ID must have a declared default of either #IMPLIED or #REQUIRED.", string.Empty);
			}
			if (attrDef.TokenizedType != XmlTokenizedType.CDATA)
			{
				attrDef.DefaultValueExpanded = this.GetValueWithStrippedSpaces();
			}
			else
			{
				attrDef.DefaultValueExpanded = this.GetValue();
			}
			attrDef.ValueLineNumber = this.literalLineInfo.lineNo;
			attrDef.ValueLinePosition = this.literalLineInfo.linePos + 1;
			DtdValidator.SetDefaultTypedValue(attrDef, this.readerAdapter);
			return;
			IL_CF:
			this.OnUnexpectedError();
		}

		// Token: 0x0600134D RID: 4941 RVA: 0x00071EC0 File Offset: 0x000700C0
		private void ParseElementDecl()
		{
			if (this.GetToken(true) == DtdParser.Token.QName)
			{
				SchemaElementDecl schemaElementDecl = null;
				XmlQualifiedName nameQualified = this.GetNameQualified(true);
				if (this.schemaInfo.ElementDecls.TryGetValue(nameQualified, out schemaElementDecl))
				{
					if (this.validate)
					{
						this.SendValidationEvent(this.curPos - nameQualified.Name.Length, XmlSeverityType.Error, "The '{0}' element has already been declared.", this.GetNameString());
					}
				}
				else
				{
					if (this.schemaInfo.UndeclaredElementDecls.TryGetValue(nameQualified, out schemaElementDecl))
					{
						this.schemaInfo.UndeclaredElementDecls.Remove(nameQualified);
					}
					else
					{
						schemaElementDecl = new SchemaElementDecl(nameQualified, nameQualified.Namespace);
					}
					this.schemaInfo.ElementDecls.Add(nameQualified, schemaElementDecl);
				}
				schemaElementDecl.IsDeclaredInExternal = !this.ParsingInternalSubset;
				DtdParser.Token token = this.GetToken(true);
				if (token != DtdParser.Token.LeftParen)
				{
					if (token != DtdParser.Token.ANY)
					{
						if (token != DtdParser.Token.EMPTY)
						{
							goto IL_181;
						}
						schemaElementDecl.ContentValidator = ContentValidator.Empty;
					}
					else
					{
						schemaElementDecl.ContentValidator = ContentValidator.Any;
					}
				}
				else
				{
					int startParenEntityId = this.currentEntityId;
					DtdParser.Token token2 = this.GetToken(false);
					if (token2 != DtdParser.Token.None)
					{
						if (token2 != DtdParser.Token.PCDATA)
						{
							goto IL_181;
						}
						ParticleContentValidator particleContentValidator = new ParticleContentValidator(XmlSchemaContentType.Mixed);
						particleContentValidator.Start();
						particleContentValidator.OpenGroup();
						this.ParseElementMixedContent(particleContentValidator, startParenEntityId);
						schemaElementDecl.ContentValidator = particleContentValidator.Finish(true);
					}
					else
					{
						ParticleContentValidator particleContentValidator2 = new ParticleContentValidator(XmlSchemaContentType.ElementOnly);
						particleContentValidator2.Start();
						particleContentValidator2.OpenGroup();
						this.ParseElementOnlyContent(particleContentValidator2, startParenEntityId);
						schemaElementDecl.ContentValidator = particleContentValidator2.Finish(true);
					}
				}
				if (this.GetToken(false) != DtdParser.Token.GreaterThan)
				{
					this.ThrowUnexpectedToken(this.curPos, ">");
				}
				return;
			}
			IL_181:
			this.OnUnexpectedError();
		}

		// Token: 0x0600134E RID: 4942 RVA: 0x00072054 File Offset: 0x00070254
		private void ParseElementOnlyContent(ParticleContentValidator pcv, int startParenEntityId)
		{
			Stack<DtdParser.ParseElementOnlyContent_LocalFrame> stack = new Stack<DtdParser.ParseElementOnlyContent_LocalFrame>();
			DtdParser.ParseElementOnlyContent_LocalFrame parseElementOnlyContent_LocalFrame = new DtdParser.ParseElementOnlyContent_LocalFrame(startParenEntityId);
			stack.Push(parseElementOnlyContent_LocalFrame);
			for (;;)
			{
				DtdParser.Token token = this.GetToken(false);
				if (token != DtdParser.Token.QName)
				{
					if (token == DtdParser.Token.LeftParen)
					{
						pcv.OpenGroup();
						parseElementOnlyContent_LocalFrame = new DtdParser.ParseElementOnlyContent_LocalFrame(this.currentEntityId);
						stack.Push(parseElementOnlyContent_LocalFrame);
						continue;
					}
					if (token != DtdParser.Token.GreaterThan)
					{
						goto IL_148;
					}
					this.Throw(this.curPos, "Invalid content model.");
					goto IL_14E;
				}
				else
				{
					pcv.AddName(this.GetNameQualified(true), null);
					this.ParseHowMany(pcv);
				}
				IL_78:
				token = this.GetToken(false);
				switch (token)
				{
				case DtdParser.Token.RightParen:
					pcv.CloseGroup();
					if (this.validate && this.currentEntityId != parseElementOnlyContent_LocalFrame.startParenEntityId)
					{
						this.SendValidationEvent(this.curPos, XmlSeverityType.Error, "The parameter entity replacement text must nest properly within markup declarations.", string.Empty);
					}
					this.ParseHowMany(pcv);
					break;
				case DtdParser.Token.GreaterThan:
					this.Throw(this.curPos, "Invalid content model.");
					break;
				case DtdParser.Token.Or:
					if (parseElementOnlyContent_LocalFrame.parsingSchema == DtdParser.Token.Comma)
					{
						this.Throw(this.curPos, "Invalid content model.");
					}
					pcv.AddChoice();
					parseElementOnlyContent_LocalFrame.parsingSchema = DtdParser.Token.Or;
					continue;
				default:
					if (token == DtdParser.Token.Comma)
					{
						if (parseElementOnlyContent_LocalFrame.parsingSchema == DtdParser.Token.Or)
						{
							this.Throw(this.curPos, "Invalid content model.");
						}
						pcv.AddSequence();
						parseElementOnlyContent_LocalFrame.parsingSchema = DtdParser.Token.Comma;
						continue;
					}
					goto IL_148;
				}
				IL_14E:
				stack.Pop();
				if (stack.Count > 0)
				{
					parseElementOnlyContent_LocalFrame = stack.Peek();
					goto IL_78;
				}
				break;
				IL_148:
				this.OnUnexpectedError();
				goto IL_14E;
			}
		}

		// Token: 0x0600134F RID: 4943 RVA: 0x000721CC File Offset: 0x000703CC
		private void ParseHowMany(ParticleContentValidator pcv)
		{
			switch (this.GetToken(false))
			{
			case DtdParser.Token.Star:
				pcv.AddStar();
				return;
			case DtdParser.Token.QMark:
				pcv.AddQMark();
				return;
			case DtdParser.Token.Plus:
				pcv.AddPlus();
				return;
			default:
				return;
			}
		}

		// Token: 0x06001350 RID: 4944 RVA: 0x0007220C File Offset: 0x0007040C
		private void ParseElementMixedContent(ParticleContentValidator pcv, int startParenEntityId)
		{
			bool flag = false;
			int num = -1;
			int num2 = this.currentEntityId;
			for (;;)
			{
				DtdParser.Token token = this.GetToken(false);
				if (token == DtdParser.Token.RightParen)
				{
					break;
				}
				if (token == DtdParser.Token.Or)
				{
					if (!flag)
					{
						flag = true;
					}
					else
					{
						pcv.AddChoice();
					}
					if (this.validate)
					{
						num = this.currentEntityId;
						if (num2 < num)
						{
							this.SendValidationEvent(this.curPos, XmlSeverityType.Error, "The parameter entity replacement text must nest properly within markup declarations.", string.Empty);
						}
					}
					if (this.GetToken(false) == DtdParser.Token.QName)
					{
						XmlQualifiedName nameQualified = this.GetNameQualified(true);
						if (pcv.Exists(nameQualified) && this.validate)
						{
							this.SendValidationEvent(XmlSeverityType.Error, "The '{0}' element already exists in the content model.", nameQualified.ToString());
						}
						pcv.AddName(nameQualified, null);
						if (!this.validate)
						{
							continue;
						}
						num2 = this.currentEntityId;
						if (num2 < num)
						{
							this.SendValidationEvent(this.curPos, XmlSeverityType.Error, "The parameter entity replacement text must nest properly within markup declarations.", string.Empty);
							continue;
						}
						continue;
					}
				}
				this.OnUnexpectedError();
			}
			pcv.CloseGroup();
			if (this.validate && this.currentEntityId != startParenEntityId)
			{
				this.SendValidationEvent(this.curPos, XmlSeverityType.Error, "The parameter entity replacement text must nest properly within markup declarations.", string.Empty);
			}
			if (this.GetToken(false) == DtdParser.Token.Star && flag)
			{
				pcv.AddStar();
				return;
			}
			if (flag)
			{
				this.ThrowUnexpectedToken(this.curPos, "*");
			}
		}

		// Token: 0x06001351 RID: 4945 RVA: 0x0007234C File Offset: 0x0007054C
		private void ParseEntityDecl()
		{
			bool flag = false;
			DtdParser.Token token = this.GetToken(true);
			if (token != DtdParser.Token.Name)
			{
				if (token != DtdParser.Token.Percent)
				{
					goto IL_1D6;
				}
				flag = true;
				if (this.GetToken(true) != DtdParser.Token.Name)
				{
					goto IL_1D6;
				}
			}
			XmlQualifiedName nameQualified = this.GetNameQualified(false);
			SchemaEntity schemaEntity = new SchemaEntity(nameQualified, flag);
			schemaEntity.BaseURI = this.BaseUriStr;
			schemaEntity.DeclaredURI = ((this.externalDtdBaseUri.Length == 0) ? this.documentBaseUri : this.externalDtdBaseUri);
			if (flag)
			{
				if (!this.schemaInfo.ParameterEntities.ContainsKey(nameQualified))
				{
					this.schemaInfo.ParameterEntities.Add(nameQualified, schemaEntity);
				}
			}
			else if (!this.schemaInfo.GeneralEntities.ContainsKey(nameQualified))
			{
				this.schemaInfo.GeneralEntities.Add(nameQualified, schemaEntity);
			}
			schemaEntity.DeclaredInExternal = !this.ParsingInternalSubset;
			schemaEntity.ParsingInProgress = true;
			DtdParser.Token token2 = this.GetToken(true);
			if (token2 - DtdParser.Token.PUBLIC > 1)
			{
				if (token2 != DtdParser.Token.Literal)
				{
					goto IL_1D6;
				}
				schemaEntity.Text = this.GetValue();
				schemaEntity.Line = this.literalLineInfo.lineNo;
				schemaEntity.Pos = this.literalLineInfo.linePos;
			}
			else
			{
				string pubid;
				string url;
				this.ParseExternalId(token2, DtdParser.Token.EntityDecl, out pubid, out url);
				schemaEntity.IsExternal = true;
				schemaEntity.Url = url;
				schemaEntity.Pubid = pubid;
				if (this.GetToken(false) == DtdParser.Token.NData)
				{
					if (flag)
					{
						this.ThrowUnexpectedToken(this.curPos - 5, ">");
					}
					if (!this.whitespaceSeen)
					{
						this.Throw(this.curPos - 5, "'{0}' is an unexpected token. Expecting white space.", "NDATA");
					}
					if (this.GetToken(true) != DtdParser.Token.Name)
					{
						goto IL_1D6;
					}
					schemaEntity.NData = this.GetNameQualified(false);
					string name = schemaEntity.NData.Name;
					if (!this.schemaInfo.Notations.ContainsKey(name))
					{
						this.AddUndeclaredNotation(name);
					}
				}
			}
			if (this.GetToken(false) == DtdParser.Token.GreaterThan)
			{
				schemaEntity.ParsingInProgress = false;
				return;
			}
			IL_1D6:
			this.OnUnexpectedError();
		}

		// Token: 0x06001352 RID: 4946 RVA: 0x00072538 File Offset: 0x00070738
		private void ParseNotationDecl()
		{
			if (this.GetToken(true) != DtdParser.Token.Name)
			{
				this.OnUnexpectedError();
			}
			XmlQualifiedName nameQualified = this.GetNameQualified(false);
			SchemaNotation schemaNotation = null;
			if (!this.schemaInfo.Notations.ContainsKey(nameQualified.Name))
			{
				if (this.undeclaredNotations != null)
				{
					this.undeclaredNotations.Remove(nameQualified.Name);
				}
				schemaNotation = new SchemaNotation(nameQualified);
				this.schemaInfo.Notations.Add(schemaNotation.Name.Name, schemaNotation);
			}
			else if (this.validate)
			{
				this.SendValidationEvent(this.curPos - nameQualified.Name.Length, XmlSeverityType.Error, "The notation '{0}' has already been declared.", nameQualified.Name);
			}
			DtdParser.Token token = this.GetToken(true);
			if (token == DtdParser.Token.SYSTEM || token == DtdParser.Token.PUBLIC)
			{
				string pubid;
				string systemLiteral;
				this.ParseExternalId(token, DtdParser.Token.NOTATION, out pubid, out systemLiteral);
				if (schemaNotation != null)
				{
					schemaNotation.SystemLiteral = systemLiteral;
					schemaNotation.Pubid = pubid;
				}
			}
			else
			{
				this.OnUnexpectedError();
			}
			if (this.GetToken(false) != DtdParser.Token.GreaterThan)
			{
				this.OnUnexpectedError();
			}
		}

		// Token: 0x06001353 RID: 4947 RVA: 0x0007262C File Offset: 0x0007082C
		private void AddUndeclaredNotation(string notationName)
		{
			if (this.undeclaredNotations == null)
			{
				this.undeclaredNotations = new Dictionary<string, DtdParser.UndeclaredNotation>();
			}
			DtdParser.UndeclaredNotation undeclaredNotation = new DtdParser.UndeclaredNotation(notationName, this.LineNo, this.LinePos - notationName.Length);
			DtdParser.UndeclaredNotation undeclaredNotation2;
			if (this.undeclaredNotations.TryGetValue(notationName, out undeclaredNotation2))
			{
				undeclaredNotation.next = undeclaredNotation2.next;
				undeclaredNotation2.next = undeclaredNotation;
				return;
			}
			this.undeclaredNotations.Add(notationName, undeclaredNotation);
		}

		// Token: 0x06001354 RID: 4948 RVA: 0x00072698 File Offset: 0x00070898
		private void ParseComment()
		{
			this.SaveParsingBuffer();
			try
			{
				if (this.SaveInternalSubsetValue)
				{
					this.readerAdapter.ParseComment(this.internalSubsetValueSb);
					this.internalSubsetValueSb.Append("-->");
				}
				else
				{
					this.readerAdapter.ParseComment(null);
				}
			}
			catch (XmlException ex)
			{
				if (!(ex.ResString == "Unexpected end of file while parsing {0} has occurred.") || this.currentEntityId == 0)
				{
					throw;
				}
				this.SendValidationEvent(XmlSeverityType.Error, "The parameter entity replacement text must nest properly within markup declarations.", null);
			}
			this.LoadParsingBuffer();
		}

		// Token: 0x06001355 RID: 4949 RVA: 0x00072728 File Offset: 0x00070928
		private void ParsePI()
		{
			this.SaveParsingBuffer();
			if (this.SaveInternalSubsetValue)
			{
				this.readerAdapter.ParsePI(this.internalSubsetValueSb);
				this.internalSubsetValueSb.Append("?>");
			}
			else
			{
				this.readerAdapter.ParsePI(null);
			}
			this.LoadParsingBuffer();
		}

		// Token: 0x06001356 RID: 4950 RVA: 0x0007277C File Offset: 0x0007097C
		private void ParseCondSection()
		{
			int num = this.currentEntityId;
			DtdParser.Token token = this.GetToken(false);
			if (token != DtdParser.Token.IGNORE)
			{
				if (token == DtdParser.Token.INCLUDE && this.GetToken(false) == DtdParser.Token.LeftBracket)
				{
					if (this.validate && num != this.currentEntityId)
					{
						this.SendValidationEvent(this.curPos, XmlSeverityType.Error, "The parameter entity replacement text must nest properly within markup declarations.", string.Empty);
					}
					if (this.validate)
					{
						if (this.condSectionEntityIds == null)
						{
							this.condSectionEntityIds = new int[2];
						}
						else if (this.condSectionEntityIds.Length == this.condSectionDepth)
						{
							int[] destinationArray = new int[this.condSectionEntityIds.Length * 2];
							Array.Copy(this.condSectionEntityIds, 0, destinationArray, 0, this.condSectionEntityIds.Length);
							this.condSectionEntityIds = destinationArray;
						}
						this.condSectionEntityIds[this.condSectionDepth] = num;
					}
					this.condSectionDepth++;
					return;
				}
			}
			else if (this.GetToken(false) == DtdParser.Token.LeftBracket)
			{
				if (this.validate && num != this.currentEntityId)
				{
					this.SendValidationEvent(this.curPos, XmlSeverityType.Error, "The parameter entity replacement text must nest properly within markup declarations.", string.Empty);
				}
				if (this.GetToken(false) == DtdParser.Token.CondSectionEnd)
				{
					if (this.validate && num != this.currentEntityId)
					{
						this.SendValidationEvent(this.curPos, XmlSeverityType.Error, "The parameter entity replacement text must nest properly within markup declarations.", string.Empty);
						return;
					}
					return;
				}
			}
			this.OnUnexpectedError();
		}

		// Token: 0x06001357 RID: 4951 RVA: 0x000728C4 File Offset: 0x00070AC4
		private void ParseExternalId(DtdParser.Token idTokenType, DtdParser.Token declType, out string publicId, out string systemId)
		{
			LineInfo keywordLineInfo = new LineInfo(this.LineNo, this.LinePos - 6);
			publicId = null;
			systemId = null;
			if (this.GetToken(true) != DtdParser.Token.Literal)
			{
				this.ThrowUnexpectedToken(this.curPos, "\"", "'");
			}
			if (idTokenType == DtdParser.Token.SYSTEM)
			{
				systemId = this.GetValue();
				if (systemId.IndexOf('#') >= 0)
				{
					this.Throw(this.curPos - systemId.Length - 1, "Fragment identifier '{0}' cannot be part of the system identifier '{1}'.", new string[]
					{
						systemId.Substring(systemId.IndexOf('#')),
						systemId
					});
				}
				if (declType == DtdParser.Token.DOCTYPE && !this.freeFloatingDtd)
				{
					this.literalLineInfo.linePos = this.literalLineInfo.linePos + 1;
					this.readerAdapter.OnSystemId(systemId, keywordLineInfo, this.literalLineInfo);
					return;
				}
			}
			else
			{
				publicId = this.GetValue();
				int num;
				if ((num = this.xmlCharType.IsPublicId(publicId)) >= 0)
				{
					this.ThrowInvalidChar(this.curPos - 1 - publicId.Length + num, publicId, num);
				}
				if (declType == DtdParser.Token.DOCTYPE && !this.freeFloatingDtd)
				{
					this.literalLineInfo.linePos = this.literalLineInfo.linePos + 1;
					this.readerAdapter.OnPublicId(publicId, keywordLineInfo, this.literalLineInfo);
					if (this.GetToken(false) == DtdParser.Token.Literal)
					{
						if (!this.whitespaceSeen)
						{
							this.Throw("'{0}' is an unexpected token. Expecting white space.", new string(this.literalQuoteChar, 1), this.literalLineInfo.lineNo, this.literalLineInfo.linePos);
						}
						systemId = this.GetValue();
						this.literalLineInfo.linePos = this.literalLineInfo.linePos + 1;
						this.readerAdapter.OnSystemId(systemId, keywordLineInfo, this.literalLineInfo);
						return;
					}
					this.ThrowUnexpectedToken(this.curPos, "\"", "'");
					return;
				}
				else
				{
					if (this.GetToken(false) == DtdParser.Token.Literal)
					{
						if (!this.whitespaceSeen)
						{
							this.Throw("'{0}' is an unexpected token. Expecting white space.", new string(this.literalQuoteChar, 1), this.literalLineInfo.lineNo, this.literalLineInfo.linePos);
						}
						systemId = this.GetValue();
						return;
					}
					if (declType != DtdParser.Token.NOTATION)
					{
						this.ThrowUnexpectedToken(this.curPos, "\"", "'");
					}
				}
			}
		}

		// Token: 0x06001358 RID: 4952 RVA: 0x00072AF8 File Offset: 0x00070CF8
		private DtdParser.Token GetToken(bool needWhiteSpace)
		{
			this.whitespaceSeen = false;
			for (;;)
			{
				char c = this.chars[this.curPos];
				if (c <= '\r')
				{
					if (c != '\0')
					{
						switch (c)
						{
						case '\t':
							goto IL_14D;
						case '\n':
							this.whitespaceSeen = true;
							this.curPos++;
							this.readerAdapter.OnNewLine(this.curPos);
							continue;
						case '\r':
							this.whitespaceSeen = true;
							if (this.chars[this.curPos + 1] == '\n')
							{
								if (this.Normalize)
								{
									this.SaveParsingBuffer();
									IDtdParserAdapter dtdParserAdapter = this.readerAdapter;
									int currentPosition = dtdParserAdapter.CurrentPosition;
									dtdParserAdapter.CurrentPosition = currentPosition + 1;
								}
								this.curPos += 2;
							}
							else
							{
								if (this.curPos + 1 >= this.charsUsed && !this.readerAdapter.IsEof)
								{
									goto IL_388;
								}
								this.chars[this.curPos] = '\n';
								this.curPos++;
							}
							this.readerAdapter.OnNewLine(this.curPos);
							continue;
						}
						break;
					}
					if (this.curPos != this.charsUsed)
					{
						this.ThrowInvalidChar(this.chars, this.charsUsed, this.curPos);
						goto IL_388;
					}
					goto IL_388;
				}
				else if (c != ' ')
				{
					if (c != '%')
					{
						break;
					}
					if (this.charsUsed - this.curPos < 2)
					{
						goto IL_388;
					}
					if (this.xmlCharType.IsWhiteSpace(this.chars[this.curPos + 1]))
					{
						break;
					}
					if (this.IgnoreEntityReferences)
					{
						this.curPos++;
						continue;
					}
					this.HandleEntityReference(true, false, false);
					continue;
				}
				IL_14D:
				this.whitespaceSeen = true;
				this.curPos++;
				continue;
				IL_388:
				if ((this.readerAdapter.IsEof || this.ReadData() == 0) && !this.HandleEntityEnd(false))
				{
					if (this.scanningFunction == DtdParser.ScanningFunction.SubsetContent)
					{
						return DtdParser.Token.Eof;
					}
					this.Throw(this.curPos, "Incomplete DTD content.");
				}
			}
			if (needWhiteSpace && !this.whitespaceSeen && this.scanningFunction != DtdParser.ScanningFunction.ParamEntitySpace)
			{
				this.Throw(this.curPos, "'{0}' is an unexpected token. Expecting white space.", this.ParseUnexpectedToken(this.curPos));
			}
			this.tokenStartPos = this.curPos;
			for (;;)
			{
				switch (this.scanningFunction)
				{
				case DtdParser.ScanningFunction.SubsetContent:
					goto IL_2A9;
				case DtdParser.ScanningFunction.Name:
					goto IL_294;
				case DtdParser.ScanningFunction.QName:
					goto IL_29B;
				case DtdParser.ScanningFunction.Nmtoken:
					goto IL_2A2;
				case DtdParser.ScanningFunction.Doctype1:
					goto IL_2B0;
				case DtdParser.ScanningFunction.Doctype2:
					goto IL_2B7;
				case DtdParser.ScanningFunction.Element1:
					goto IL_2BE;
				case DtdParser.ScanningFunction.Element2:
					goto IL_2C5;
				case DtdParser.ScanningFunction.Element3:
					goto IL_2CC;
				case DtdParser.ScanningFunction.Element4:
					goto IL_2D3;
				case DtdParser.ScanningFunction.Element5:
					goto IL_2DA;
				case DtdParser.ScanningFunction.Element6:
					goto IL_2E1;
				case DtdParser.ScanningFunction.Element7:
					goto IL_2E8;
				case DtdParser.ScanningFunction.Attlist1:
					goto IL_2EF;
				case DtdParser.ScanningFunction.Attlist2:
					goto IL_2F6;
				case DtdParser.ScanningFunction.Attlist3:
					goto IL_2FD;
				case DtdParser.ScanningFunction.Attlist4:
					goto IL_304;
				case DtdParser.ScanningFunction.Attlist5:
					goto IL_30B;
				case DtdParser.ScanningFunction.Attlist6:
					goto IL_312;
				case DtdParser.ScanningFunction.Attlist7:
					goto IL_319;
				case DtdParser.ScanningFunction.Entity1:
					goto IL_33C;
				case DtdParser.ScanningFunction.Entity2:
					goto IL_343;
				case DtdParser.ScanningFunction.Entity3:
					goto IL_34A;
				case DtdParser.ScanningFunction.Notation1:
					goto IL_320;
				case DtdParser.ScanningFunction.CondSection1:
					goto IL_351;
				case DtdParser.ScanningFunction.CondSection2:
					goto IL_358;
				case DtdParser.ScanningFunction.CondSection3:
					goto IL_35F;
				case DtdParser.ScanningFunction.SystemId:
					goto IL_327;
				case DtdParser.ScanningFunction.PublicId1:
					goto IL_32E;
				case DtdParser.ScanningFunction.PublicId2:
					goto IL_335;
				case DtdParser.ScanningFunction.ClosingTag:
					goto IL_366;
				case DtdParser.ScanningFunction.ParamEntitySpace:
					this.whitespaceSeen = true;
					this.scanningFunction = this.savedScanningFunction;
					continue;
				}
				break;
			}
			return DtdParser.Token.None;
			IL_294:
			return this.ScanNameExpected();
			IL_29B:
			return this.ScanQNameExpected();
			IL_2A2:
			return this.ScanNmtokenExpected();
			IL_2A9:
			return this.ScanSubsetContent();
			IL_2B0:
			return this.ScanDoctype1();
			IL_2B7:
			return this.ScanDoctype2();
			IL_2BE:
			return this.ScanElement1();
			IL_2C5:
			return this.ScanElement2();
			IL_2CC:
			return this.ScanElement3();
			IL_2D3:
			return this.ScanElement4();
			IL_2DA:
			return this.ScanElement5();
			IL_2E1:
			return this.ScanElement6();
			IL_2E8:
			return this.ScanElement7();
			IL_2EF:
			return this.ScanAttlist1();
			IL_2F6:
			return this.ScanAttlist2();
			IL_2FD:
			return this.ScanAttlist3();
			IL_304:
			return this.ScanAttlist4();
			IL_30B:
			return this.ScanAttlist5();
			IL_312:
			return this.ScanAttlist6();
			IL_319:
			return this.ScanAttlist7();
			IL_320:
			return this.ScanNotation1();
			IL_327:
			return this.ScanSystemId();
			IL_32E:
			return this.ScanPublicId1();
			IL_335:
			return this.ScanPublicId2();
			IL_33C:
			return this.ScanEntity1();
			IL_343:
			return this.ScanEntity2();
			IL_34A:
			return this.ScanEntity3();
			IL_351:
			return this.ScanCondSection1();
			IL_358:
			return this.ScanCondSection2();
			IL_35F:
			return this.ScanCondSection3();
			IL_366:
			return this.ScanClosingTag();
		}

		// Token: 0x06001359 RID: 4953 RVA: 0x00072ED4 File Offset: 0x000710D4
		private DtdParser.Token ScanSubsetContent()
		{
			for (;;)
			{
				char c = this.chars[this.curPos];
				if (c != '<')
				{
					if (c == ']')
					{
						if (this.charsUsed - this.curPos < 2 && !this.readerAdapter.IsEof)
						{
							goto IL_513;
						}
						if (this.chars[this.curPos + 1] != ']')
						{
							goto Block_40;
						}
						if (this.charsUsed - this.curPos < 3 && !this.readerAdapter.IsEof)
						{
							goto IL_513;
						}
						if (this.chars[this.curPos + 1] == ']' && this.chars[this.curPos + 2] == '>')
						{
							goto Block_43;
						}
					}
					if (this.charsUsed - this.curPos != 0)
					{
						this.Throw(this.curPos, "Expected DTD markup was not found.");
					}
				}
				else
				{
					char c2 = this.chars[this.curPos + 1];
					if (c2 != '!')
					{
						if (c2 == '?')
						{
							goto IL_41B;
						}
						if (this.charsUsed - this.curPos >= 2)
						{
							goto Block_38;
						}
					}
					else
					{
						char c3 = this.chars[this.curPos + 2];
						if (c3 <= 'A')
						{
							if (c3 != '-')
							{
								if (c3 == 'A')
								{
									if (this.charsUsed - this.curPos >= 9)
									{
										goto Block_22;
									}
									goto IL_513;
								}
							}
							else
							{
								if (this.chars[this.curPos + 3] == '-')
								{
									goto Block_35;
								}
								if (this.charsUsed - this.curPos >= 4)
								{
									this.Throw(this.curPos, "Expected DTD markup was not found.");
									goto IL_513;
								}
								goto IL_513;
							}
						}
						else if (c3 != 'E')
						{
							if (c3 != 'N')
							{
								if (c3 == '[')
								{
									goto IL_38A;
								}
							}
							else
							{
								if (this.charsUsed - this.curPos >= 10)
								{
									goto Block_28;
								}
								goto IL_513;
							}
						}
						else if (this.chars[this.curPos + 3] == 'L')
						{
							if (this.charsUsed - this.curPos >= 9)
							{
								break;
							}
							goto IL_513;
						}
						else if (this.chars[this.curPos + 3] == 'N')
						{
							if (this.charsUsed - this.curPos >= 8)
							{
								goto Block_17;
							}
							goto IL_513;
						}
						else
						{
							if (this.charsUsed - this.curPos >= 4)
							{
								goto Block_21;
							}
							goto IL_513;
						}
						if (this.charsUsed - this.curPos >= 3)
						{
							this.Throw(this.curPos + 2, "Expected DTD markup was not found.");
						}
					}
				}
				IL_513:
				if (this.ReadData() == 0)
				{
					this.Throw(this.charsUsed, "Incomplete DTD content.");
				}
			}
			if (this.chars[this.curPos + 4] != 'E' || this.chars[this.curPos + 5] != 'M' || this.chars[this.curPos + 6] != 'E' || this.chars[this.curPos + 7] != 'N' || this.chars[this.curPos + 8] != 'T')
			{
				this.Throw(this.curPos, "Expected DTD markup was not found.");
			}
			this.curPos += 9;
			this.scanningFunction = DtdParser.ScanningFunction.QName;
			this.nextScaningFunction = DtdParser.ScanningFunction.Element1;
			return DtdParser.Token.ElementDecl;
			Block_17:
			if (this.chars[this.curPos + 4] != 'T' || this.chars[this.curPos + 5] != 'I' || this.chars[this.curPos + 6] != 'T' || this.chars[this.curPos + 7] != 'Y')
			{
				this.Throw(this.curPos, "Expected DTD markup was not found.");
			}
			this.curPos += 8;
			this.scanningFunction = DtdParser.ScanningFunction.Entity1;
			return DtdParser.Token.EntityDecl;
			Block_21:
			this.Throw(this.curPos, "Expected DTD markup was not found.");
			return DtdParser.Token.None;
			Block_22:
			if (this.chars[this.curPos + 3] != 'T' || this.chars[this.curPos + 4] != 'T' || this.chars[this.curPos + 5] != 'L' || this.chars[this.curPos + 6] != 'I' || this.chars[this.curPos + 7] != 'S' || this.chars[this.curPos + 8] != 'T')
			{
				this.Throw(this.curPos, "Expected DTD markup was not found.");
			}
			this.curPos += 9;
			this.scanningFunction = DtdParser.ScanningFunction.QName;
			this.nextScaningFunction = DtdParser.ScanningFunction.Attlist1;
			return DtdParser.Token.AttlistDecl;
			Block_28:
			if (this.chars[this.curPos + 3] != 'O' || this.chars[this.curPos + 4] != 'T' || this.chars[this.curPos + 5] != 'A' || this.chars[this.curPos + 6] != 'T' || this.chars[this.curPos + 7] != 'I' || this.chars[this.curPos + 8] != 'O' || this.chars[this.curPos + 9] != 'N')
			{
				this.Throw(this.curPos, "Expected DTD markup was not found.");
			}
			this.curPos += 10;
			this.scanningFunction = DtdParser.ScanningFunction.Name;
			this.nextScaningFunction = DtdParser.ScanningFunction.Notation1;
			return DtdParser.Token.NotationDecl;
			IL_38A:
			this.curPos += 3;
			this.scanningFunction = DtdParser.ScanningFunction.CondSection1;
			return DtdParser.Token.CondSectionStart;
			Block_35:
			this.curPos += 4;
			return DtdParser.Token.Comment;
			IL_41B:
			this.curPos += 2;
			return DtdParser.Token.PI;
			Block_38:
			this.Throw(this.curPos, "Expected DTD markup was not found.");
			return DtdParser.Token.None;
			Block_40:
			this.curPos++;
			this.scanningFunction = DtdParser.ScanningFunction.ClosingTag;
			return DtdParser.Token.RightBracket;
			Block_43:
			this.curPos += 3;
			return DtdParser.Token.CondSectionEnd;
		}

		// Token: 0x0600135A RID: 4954 RVA: 0x00073414 File Offset: 0x00071614
		private DtdParser.Token ScanNameExpected()
		{
			this.ScanName();
			this.scanningFunction = this.nextScaningFunction;
			return DtdParser.Token.Name;
		}

		// Token: 0x0600135B RID: 4955 RVA: 0x0007342A File Offset: 0x0007162A
		private DtdParser.Token ScanQNameExpected()
		{
			this.ScanQName();
			this.scanningFunction = this.nextScaningFunction;
			return DtdParser.Token.QName;
		}

		// Token: 0x0600135C RID: 4956 RVA: 0x00073440 File Offset: 0x00071640
		private DtdParser.Token ScanNmtokenExpected()
		{
			this.ScanNmtoken();
			this.scanningFunction = this.nextScaningFunction;
			return DtdParser.Token.Nmtoken;
		}

		// Token: 0x0600135D RID: 4957 RVA: 0x00073458 File Offset: 0x00071658
		private DtdParser.Token ScanDoctype1()
		{
			char c = this.chars[this.curPos];
			if (c <= 'P')
			{
				if (c == '>')
				{
					this.curPos++;
					this.scanningFunction = DtdParser.ScanningFunction.SubsetContent;
					return DtdParser.Token.GreaterThan;
				}
				if (c == 'P')
				{
					if (!this.EatPublicKeyword())
					{
						this.Throw(this.curPos, "Expecting external ID, '[' or '>'.");
					}
					this.nextScaningFunction = DtdParser.ScanningFunction.Doctype2;
					this.scanningFunction = DtdParser.ScanningFunction.PublicId1;
					return DtdParser.Token.PUBLIC;
				}
			}
			else
			{
				if (c == 'S')
				{
					if (!this.EatSystemKeyword())
					{
						this.Throw(this.curPos, "Expecting external ID, '[' or '>'.");
					}
					this.nextScaningFunction = DtdParser.ScanningFunction.Doctype2;
					this.scanningFunction = DtdParser.ScanningFunction.SystemId;
					return DtdParser.Token.SYSTEM;
				}
				if (c == '[')
				{
					this.curPos++;
					this.scanningFunction = DtdParser.ScanningFunction.SubsetContent;
					return DtdParser.Token.LeftBracket;
				}
			}
			this.Throw(this.curPos, "Expecting external ID, '[' or '>'.");
			return DtdParser.Token.None;
		}

		// Token: 0x0600135E RID: 4958 RVA: 0x00073534 File Offset: 0x00071734
		private DtdParser.Token ScanDoctype2()
		{
			char c = this.chars[this.curPos];
			if (c == '>')
			{
				this.curPos++;
				this.scanningFunction = DtdParser.ScanningFunction.SubsetContent;
				return DtdParser.Token.GreaterThan;
			}
			if (c == '[')
			{
				this.curPos++;
				this.scanningFunction = DtdParser.ScanningFunction.SubsetContent;
				return DtdParser.Token.LeftBracket;
			}
			this.Throw(this.curPos, "Expecting an internal subset or the end of the DOCTYPE declaration.");
			return DtdParser.Token.None;
		}

		// Token: 0x0600135F RID: 4959 RVA: 0x0007359C File Offset: 0x0007179C
		private DtdParser.Token ScanClosingTag()
		{
			if (this.chars[this.curPos] != '>')
			{
				this.ThrowUnexpectedToken(this.curPos, ">");
			}
			this.curPos++;
			this.scanningFunction = DtdParser.ScanningFunction.SubsetContent;
			return DtdParser.Token.GreaterThan;
		}

		// Token: 0x06001360 RID: 4960 RVA: 0x000735D8 File Offset: 0x000717D8
		private DtdParser.Token ScanElement1()
		{
			for (;;)
			{
				char c = this.chars[this.curPos];
				if (c != '(')
				{
					if (c != 'A')
					{
						if (c != 'E')
						{
							goto IL_10A;
						}
						if (this.charsUsed - this.curPos >= 5)
						{
							if (this.chars[this.curPos + 1] == 'M' && this.chars[this.curPos + 2] == 'P' && this.chars[this.curPos + 3] == 'T' && this.chars[this.curPos + 4] == 'Y')
							{
								goto Block_7;
							}
							goto IL_10A;
						}
					}
					else if (this.charsUsed - this.curPos >= 3)
					{
						if (this.chars[this.curPos + 1] == 'N' && this.chars[this.curPos + 2] == 'Y')
						{
							goto Block_10;
						}
						goto IL_10A;
					}
					IL_11B:
					if (this.ReadData() == 0)
					{
						this.Throw(this.curPos, "Incomplete DTD content.");
						continue;
					}
					continue;
					IL_10A:
					this.Throw(this.curPos, "Invalid content model.");
					goto IL_11B;
				}
				break;
			}
			this.scanningFunction = DtdParser.ScanningFunction.Element2;
			this.curPos++;
			return DtdParser.Token.LeftParen;
			Block_7:
			this.curPos += 5;
			this.scanningFunction = DtdParser.ScanningFunction.ClosingTag;
			return DtdParser.Token.EMPTY;
			Block_10:
			this.curPos += 3;
			this.scanningFunction = DtdParser.ScanningFunction.ClosingTag;
			return DtdParser.Token.ANY;
		}

		// Token: 0x06001361 RID: 4961 RVA: 0x00073720 File Offset: 0x00071920
		private DtdParser.Token ScanElement2()
		{
			if (this.chars[this.curPos] == '#')
			{
				while (this.charsUsed - this.curPos < 7)
				{
					if (this.ReadData() == 0)
					{
						this.Throw(this.curPos, "Incomplete DTD content.");
					}
				}
				if (this.chars[this.curPos + 1] == 'P' && this.chars[this.curPos + 2] == 'C' && this.chars[this.curPos + 3] == 'D' && this.chars[this.curPos + 4] == 'A' && this.chars[this.curPos + 5] == 'T' && this.chars[this.curPos + 6] == 'A')
				{
					this.curPos += 7;
					this.scanningFunction = DtdParser.ScanningFunction.Element6;
					return DtdParser.Token.PCDATA;
				}
				this.Throw(this.curPos + 1, "Expecting 'PCDATA'.");
			}
			this.scanningFunction = DtdParser.ScanningFunction.Element3;
			return DtdParser.Token.None;
		}

		// Token: 0x06001362 RID: 4962 RVA: 0x00073814 File Offset: 0x00071A14
		private DtdParser.Token ScanElement3()
		{
			char c = this.chars[this.curPos];
			if (c == '(')
			{
				this.curPos++;
				return DtdParser.Token.LeftParen;
			}
			if (c != '>')
			{
				this.ScanQName();
				this.scanningFunction = DtdParser.ScanningFunction.Element4;
				return DtdParser.Token.QName;
			}
			this.curPos++;
			this.scanningFunction = DtdParser.ScanningFunction.SubsetContent;
			return DtdParser.Token.GreaterThan;
		}

		// Token: 0x06001363 RID: 4963 RVA: 0x00073874 File Offset: 0x00071A74
		private DtdParser.Token ScanElement4()
		{
			this.scanningFunction = DtdParser.ScanningFunction.Element5;
			char c = this.chars[this.curPos];
			DtdParser.Token result;
			if (c != '*')
			{
				if (c != '+')
				{
					if (c != '?')
					{
						return DtdParser.Token.None;
					}
					result = DtdParser.Token.QMark;
				}
				else
				{
					result = DtdParser.Token.Plus;
				}
			}
			else
			{
				result = DtdParser.Token.Star;
			}
			if (this.whitespaceSeen)
			{
				this.Throw(this.curPos, "White space not allowed before '?', '*', or '+'.");
			}
			this.curPos++;
			return result;
		}

		// Token: 0x06001364 RID: 4964 RVA: 0x000738E4 File Offset: 0x00071AE4
		private DtdParser.Token ScanElement5()
		{
			char c = this.chars[this.curPos];
			if (c <= ',')
			{
				if (c == ')')
				{
					this.curPos++;
					this.scanningFunction = DtdParser.ScanningFunction.Element4;
					return DtdParser.Token.RightParen;
				}
				if (c == ',')
				{
					this.curPos++;
					this.scanningFunction = DtdParser.ScanningFunction.Element3;
					return DtdParser.Token.Comma;
				}
			}
			else
			{
				if (c == '>')
				{
					this.curPos++;
					this.scanningFunction = DtdParser.ScanningFunction.SubsetContent;
					return DtdParser.Token.GreaterThan;
				}
				if (c == '|')
				{
					this.curPos++;
					this.scanningFunction = DtdParser.ScanningFunction.Element3;
					return DtdParser.Token.Or;
				}
			}
			this.Throw(this.curPos, "Expecting '?', '*', or '+'.");
			return DtdParser.Token.None;
		}

		// Token: 0x06001365 RID: 4965 RVA: 0x00073990 File Offset: 0x00071B90
		private DtdParser.Token ScanElement6()
		{
			char c = this.chars[this.curPos];
			if (c == ')')
			{
				this.curPos++;
				this.scanningFunction = DtdParser.ScanningFunction.Element7;
				return DtdParser.Token.RightParen;
			}
			if (c != '|')
			{
				this.ThrowUnexpectedToken(this.curPos, ")", "|");
				return DtdParser.Token.None;
			}
			this.curPos++;
			this.nextScaningFunction = DtdParser.ScanningFunction.Element6;
			this.scanningFunction = DtdParser.ScanningFunction.QName;
			return DtdParser.Token.Or;
		}

		// Token: 0x06001366 RID: 4966 RVA: 0x00073A08 File Offset: 0x00071C08
		private DtdParser.Token ScanElement7()
		{
			this.scanningFunction = DtdParser.ScanningFunction.ClosingTag;
			if (this.chars[this.curPos] == '*' && !this.whitespaceSeen)
			{
				this.curPos++;
				return DtdParser.Token.Star;
			}
			return DtdParser.Token.None;
		}

		// Token: 0x06001367 RID: 4967 RVA: 0x00073A40 File Offset: 0x00071C40
		private DtdParser.Token ScanAttlist1()
		{
			if (this.chars[this.curPos] == '>')
			{
				this.curPos++;
				this.scanningFunction = DtdParser.ScanningFunction.SubsetContent;
				return DtdParser.Token.GreaterThan;
			}
			if (!this.whitespaceSeen)
			{
				this.Throw(this.curPos, "'{0}' is an unexpected token. Expecting white space.", this.ParseUnexpectedToken(this.curPos));
			}
			this.ScanQName();
			this.scanningFunction = DtdParser.ScanningFunction.Attlist2;
			return DtdParser.Token.QName;
		}

		// Token: 0x06001368 RID: 4968 RVA: 0x00073AAC File Offset: 0x00071CAC
		private DtdParser.Token ScanAttlist2()
		{
			for (;;)
			{
				char c = this.chars[this.curPos];
				if (c <= 'C')
				{
					if (c == '(')
					{
						break;
					}
					if (c != 'C')
					{
						goto IL_44E;
					}
					if (this.charsUsed - this.curPos >= 5)
					{
						goto Block_6;
					}
				}
				else if (c != 'E')
				{
					if (c != 'I')
					{
						if (c != 'N')
						{
							goto IL_44E;
						}
						if (this.charsUsed - this.curPos >= 8 || this.readerAdapter.IsEof)
						{
							char c2 = this.chars[this.curPos + 1];
							if (c2 == 'M')
							{
								goto IL_390;
							}
							if (c2 == 'O')
							{
								goto Block_24;
							}
							this.Throw(this.curPos, "'{0}' is an invalid attribute type.");
						}
					}
					else if (this.charsUsed - this.curPos >= 6)
					{
						goto Block_17;
					}
				}
				else if (this.charsUsed - this.curPos >= 9)
				{
					this.scanningFunction = DtdParser.ScanningFunction.Attlist6;
					if (this.chars[this.curPos + 1] != 'N' || this.chars[this.curPos + 2] != 'T' || this.chars[this.curPos + 3] != 'I' || this.chars[this.curPos + 4] != 'T')
					{
						this.Throw(this.curPos, "'{0}' is an invalid attribute type.");
					}
					char c2 = this.chars[this.curPos + 5];
					if (c2 == 'I')
					{
						goto IL_17C;
					}
					if (c2 == 'Y')
					{
						goto IL_1C3;
					}
					this.Throw(this.curPos, "'{0}' is an invalid attribute type.");
				}
				IL_45F:
				if (this.ReadData() == 0)
				{
					this.Throw(this.curPos, "Incomplete DTD content.");
					continue;
				}
				continue;
				IL_44E:
				this.Throw(this.curPos, "'{0}' is an invalid attribute type.");
				goto IL_45F;
			}
			this.curPos++;
			this.scanningFunction = DtdParser.ScanningFunction.Nmtoken;
			this.nextScaningFunction = DtdParser.ScanningFunction.Attlist5;
			return DtdParser.Token.LeftParen;
			Block_6:
			if (this.chars[this.curPos + 1] != 'D' || this.chars[this.curPos + 2] != 'A' || this.chars[this.curPos + 3] != 'T' || this.chars[this.curPos + 4] != 'A')
			{
				this.Throw(this.curPos, "Invalid attribute type.");
			}
			this.curPos += 5;
			this.scanningFunction = DtdParser.ScanningFunction.Attlist6;
			return DtdParser.Token.CDATA;
			IL_17C:
			if (this.chars[this.curPos + 6] != 'E' || this.chars[this.curPos + 7] != 'S')
			{
				this.Throw(this.curPos, "'{0}' is an invalid attribute type.");
			}
			this.curPos += 8;
			return DtdParser.Token.ENTITIES;
			IL_1C3:
			this.curPos += 6;
			return DtdParser.Token.ENTITY;
			Block_17:
			this.scanningFunction = DtdParser.ScanningFunction.Attlist6;
			if (this.chars[this.curPos + 1] != 'D')
			{
				this.Throw(this.curPos, "'{0}' is an invalid attribute type.");
			}
			if (this.chars[this.curPos + 2] != 'R')
			{
				this.curPos += 2;
				return DtdParser.Token.ID;
			}
			if (this.chars[this.curPos + 3] != 'E' || this.chars[this.curPos + 4] != 'F')
			{
				this.Throw(this.curPos, "'{0}' is an invalid attribute type.");
			}
			if (this.chars[this.curPos + 5] != 'S')
			{
				this.curPos += 5;
				return DtdParser.Token.IDREF;
			}
			this.curPos += 6;
			return DtdParser.Token.IDREFS;
			Block_24:
			if (this.chars[this.curPos + 2] != 'T' || this.chars[this.curPos + 3] != 'A' || this.chars[this.curPos + 4] != 'T' || this.chars[this.curPos + 5] != 'I' || this.chars[this.curPos + 6] != 'O' || this.chars[this.curPos + 7] != 'N')
			{
				this.Throw(this.curPos, "'{0}' is an invalid attribute type.");
			}
			this.curPos += 8;
			this.scanningFunction = DtdParser.ScanningFunction.Attlist3;
			return DtdParser.Token.NOTATION;
			IL_390:
			if (this.chars[this.curPos + 2] != 'T' || this.chars[this.curPos + 3] != 'O' || this.chars[this.curPos + 4] != 'K' || this.chars[this.curPos + 5] != 'E' || this.chars[this.curPos + 6] != 'N')
			{
				this.Throw(this.curPos, "'{0}' is an invalid attribute type.");
			}
			this.scanningFunction = DtdParser.ScanningFunction.Attlist6;
			if (this.chars[this.curPos + 7] == 'S')
			{
				this.curPos += 8;
				return DtdParser.Token.NMTOKENS;
			}
			this.curPos += 7;
			return DtdParser.Token.NMTOKEN;
		}

		// Token: 0x06001369 RID: 4969 RVA: 0x00073F38 File Offset: 0x00072138
		private DtdParser.Token ScanAttlist3()
		{
			if (this.chars[this.curPos] == '(')
			{
				this.curPos++;
				this.scanningFunction = DtdParser.ScanningFunction.Name;
				this.nextScaningFunction = DtdParser.ScanningFunction.Attlist4;
				return DtdParser.Token.LeftParen;
			}
			this.ThrowUnexpectedToken(this.curPos, "(");
			return DtdParser.Token.None;
		}

		// Token: 0x0600136A RID: 4970 RVA: 0x00073F8C File Offset: 0x0007218C
		private DtdParser.Token ScanAttlist4()
		{
			char c = this.chars[this.curPos];
			if (c == ')')
			{
				this.curPos++;
				this.scanningFunction = DtdParser.ScanningFunction.Attlist6;
				return DtdParser.Token.RightParen;
			}
			if (c != '|')
			{
				this.ThrowUnexpectedToken(this.curPos, ")", "|");
				return DtdParser.Token.None;
			}
			this.curPos++;
			this.scanningFunction = DtdParser.ScanningFunction.Name;
			this.nextScaningFunction = DtdParser.ScanningFunction.Attlist4;
			return DtdParser.Token.Or;
		}

		// Token: 0x0600136B RID: 4971 RVA: 0x00074004 File Offset: 0x00072204
		private DtdParser.Token ScanAttlist5()
		{
			char c = this.chars[this.curPos];
			if (c == ')')
			{
				this.curPos++;
				this.scanningFunction = DtdParser.ScanningFunction.Attlist6;
				return DtdParser.Token.RightParen;
			}
			if (c != '|')
			{
				this.ThrowUnexpectedToken(this.curPos, ")", "|");
				return DtdParser.Token.None;
			}
			this.curPos++;
			this.scanningFunction = DtdParser.ScanningFunction.Nmtoken;
			this.nextScaningFunction = DtdParser.ScanningFunction.Attlist5;
			return DtdParser.Token.Or;
		}

		// Token: 0x0600136C RID: 4972 RVA: 0x0007407C File Offset: 0x0007227C
		private DtdParser.Token ScanAttlist6()
		{
			for (;;)
			{
				char c = this.chars[this.curPos];
				if (c == '"')
				{
					break;
				}
				if (c != '#')
				{
					if (c == '\'')
					{
						break;
					}
					this.Throw(this.curPos, "Expecting an attribute type.");
				}
				else if (this.charsUsed - this.curPos >= 6)
				{
					char c2 = this.chars[this.curPos + 1];
					if (c2 == 'F')
					{
						goto IL_1E1;
					}
					if (c2 != 'I')
					{
						if (c2 == 'R')
						{
							if (this.charsUsed - this.curPos >= 9)
							{
								goto Block_6;
							}
						}
						else
						{
							this.Throw(this.curPos, "Expecting an attribute type.");
						}
					}
					else if (this.charsUsed - this.curPos >= 8)
					{
						goto Block_13;
					}
				}
				if (this.ReadData() == 0)
				{
					this.Throw(this.curPos, "Incomplete DTD content.");
				}
			}
			this.ScanLiteral(DtdParser.LiteralType.AttributeValue);
			this.scanningFunction = DtdParser.ScanningFunction.Attlist1;
			return DtdParser.Token.Literal;
			Block_6:
			if (this.chars[this.curPos + 2] != 'E' || this.chars[this.curPos + 3] != 'Q' || this.chars[this.curPos + 4] != 'U' || this.chars[this.curPos + 5] != 'I' || this.chars[this.curPos + 6] != 'R' || this.chars[this.curPos + 7] != 'E' || this.chars[this.curPos + 8] != 'D')
			{
				this.Throw(this.curPos, "Expecting an attribute type.");
			}
			this.curPos += 9;
			this.scanningFunction = DtdParser.ScanningFunction.Attlist1;
			return DtdParser.Token.REQUIRED;
			Block_13:
			if (this.chars[this.curPos + 2] != 'M' || this.chars[this.curPos + 3] != 'P' || this.chars[this.curPos + 4] != 'L' || this.chars[this.curPos + 5] != 'I' || this.chars[this.curPos + 6] != 'E' || this.chars[this.curPos + 7] != 'D')
			{
				this.Throw(this.curPos, "Expecting an attribute type.");
			}
			this.curPos += 8;
			this.scanningFunction = DtdParser.ScanningFunction.Attlist1;
			return DtdParser.Token.IMPLIED;
			IL_1E1:
			if (this.chars[this.curPos + 2] != 'I' || this.chars[this.curPos + 3] != 'X' || this.chars[this.curPos + 4] != 'E' || this.chars[this.curPos + 5] != 'D')
			{
				this.Throw(this.curPos, "Expecting an attribute type.");
			}
			this.curPos += 6;
			this.scanningFunction = DtdParser.ScanningFunction.Attlist7;
			return DtdParser.Token.FIXED;
		}

		// Token: 0x0600136D RID: 4973 RVA: 0x00074324 File Offset: 0x00072524
		private DtdParser.Token ScanAttlist7()
		{
			char c = this.chars[this.curPos];
			if (c == '"' || c == '\'')
			{
				this.ScanLiteral(DtdParser.LiteralType.AttributeValue);
				this.scanningFunction = DtdParser.ScanningFunction.Attlist1;
				return DtdParser.Token.Literal;
			}
			this.ThrowUnexpectedToken(this.curPos, "\"", "'");
			return DtdParser.Token.None;
		}

		// Token: 0x0600136E RID: 4974 RVA: 0x00074374 File Offset: 0x00072574
		private DtdParser.Token ScanLiteral(DtdParser.LiteralType literalType)
		{
			char c = this.chars[this.curPos];
			char value = (literalType == DtdParser.LiteralType.AttributeValue) ? ' ' : '\n';
			int num = this.currentEntityId;
			this.literalLineInfo.Set(this.LineNo, this.LinePos);
			this.curPos++;
			this.tokenStartPos = this.curPos;
			this.stringBuilder.Length = 0;
			for (;;)
			{
				if ((this.xmlCharType.charProperties[(int)this.chars[this.curPos]] & 128) == 0 || this.chars[this.curPos] == '%')
				{
					if (this.chars[this.curPos] == c && this.currentEntityId == num)
					{
						break;
					}
					int num2 = this.curPos - this.tokenStartPos;
					if (num2 > 0)
					{
						this.stringBuilder.Append(this.chars, this.tokenStartPos, num2);
						this.tokenStartPos = this.curPos;
					}
					char c2 = this.chars[this.curPos];
					if (c2 <= '\'')
					{
						switch (c2)
						{
						case '\t':
							if (literalType == DtdParser.LiteralType.AttributeValue && this.Normalize)
							{
								this.stringBuilder.Append(' ');
								this.tokenStartPos++;
							}
							this.curPos++;
							continue;
						case '\n':
							this.curPos++;
							if (this.Normalize)
							{
								this.stringBuilder.Append(value);
								this.tokenStartPos = this.curPos;
							}
							this.readerAdapter.OnNewLine(this.curPos);
							continue;
						case '\v':
						case '\f':
							goto IL_54D;
						case '\r':
							if (this.chars[this.curPos + 1] == '\n')
							{
								if (this.Normalize)
								{
									if (literalType == DtdParser.LiteralType.AttributeValue)
									{
										this.stringBuilder.Append(this.readerAdapter.IsEntityEolNormalized ? "  " : " ");
									}
									else
									{
										this.stringBuilder.Append(this.readerAdapter.IsEntityEolNormalized ? "\r\n" : "\n");
									}
									this.tokenStartPos = this.curPos + 2;
									this.SaveParsingBuffer();
									IDtdParserAdapter dtdParserAdapter = this.readerAdapter;
									int currentPosition = dtdParserAdapter.CurrentPosition;
									dtdParserAdapter.CurrentPosition = currentPosition + 1;
								}
								this.curPos += 2;
							}
							else
							{
								if (this.curPos + 1 == this.charsUsed)
								{
									goto IL_5CF;
								}
								this.curPos++;
								if (this.Normalize)
								{
									this.stringBuilder.Append(value);
									this.tokenStartPos = this.curPos;
								}
							}
							this.readerAdapter.OnNewLine(this.curPos);
							continue;
						default:
							switch (c2)
							{
							case '"':
							case '\'':
								break;
							case '#':
							case '$':
								goto IL_54D;
							case '%':
								if (literalType != DtdParser.LiteralType.EntityReplText)
								{
									this.curPos++;
									continue;
								}
								this.HandleEntityReference(true, true, literalType == DtdParser.LiteralType.AttributeValue);
								this.tokenStartPos = this.curPos;
								continue;
							case '&':
								if (literalType == DtdParser.LiteralType.SystemOrPublicID)
								{
									this.curPos++;
									continue;
								}
								if (this.curPos + 1 == this.charsUsed)
								{
									goto IL_5CF;
								}
								if (this.chars[this.curPos + 1] == '#')
								{
									this.SaveParsingBuffer();
									int num3 = this.readerAdapter.ParseNumericCharRef(this.SaveInternalSubsetValue ? this.internalSubsetValueSb : null);
									this.LoadParsingBuffer();
									this.stringBuilder.Append(this.chars, this.curPos, num3 - this.curPos);
									this.readerAdapter.CurrentPosition = num3;
									this.tokenStartPos = num3;
									this.curPos = num3;
									continue;
								}
								this.SaveParsingBuffer();
								if (literalType == DtdParser.LiteralType.AttributeValue)
								{
									int num4 = this.readerAdapter.ParseNamedCharRef(true, this.SaveInternalSubsetValue ? this.internalSubsetValueSb : null);
									this.LoadParsingBuffer();
									if (num4 >= 0)
									{
										this.stringBuilder.Append(this.chars, this.curPos, num4 - this.curPos);
										this.readerAdapter.CurrentPosition = num4;
										this.tokenStartPos = num4;
										this.curPos = num4;
										continue;
									}
									this.HandleEntityReference(false, true, true);
									this.tokenStartPos = this.curPos;
									continue;
								}
								else
								{
									int num5 = this.readerAdapter.ParseNamedCharRef(false, null);
									this.LoadParsingBuffer();
									if (num5 >= 0)
									{
										this.tokenStartPos = this.curPos;
										this.curPos = num5;
										continue;
									}
									this.stringBuilder.Append('&');
									this.curPos++;
									this.tokenStartPos = this.curPos;
									XmlQualifiedName entityName = this.ScanEntityName();
									this.VerifyEntityReference(entityName, false, false, false);
									continue;
								}
								break;
							default:
								goto IL_54D;
							}
							break;
						}
					}
					else
					{
						if (c2 == '<')
						{
							if (literalType == DtdParser.LiteralType.AttributeValue)
							{
								this.Throw(this.curPos, "'{0}', hexadecimal value {1}, is an invalid attribute character.", XmlException.BuildCharExceptionArgs('<', '\0'));
							}
							this.curPos++;
							continue;
						}
						if (c2 != '>')
						{
							goto IL_54D;
						}
					}
					this.curPos++;
					continue;
					IL_54D:
					if (this.curPos != this.charsUsed)
					{
						if (!XmlCharType.IsHighSurrogate((int)this.chars[this.curPos]))
						{
							goto IL_5B4;
						}
						if (this.curPos + 1 != this.charsUsed)
						{
							this.curPos++;
							if (XmlCharType.IsLowSurrogate((int)this.chars[this.curPos]))
							{
								this.curPos++;
								continue;
							}
							goto IL_5B4;
						}
					}
					IL_5CF:
					if ((this.readerAdapter.IsEof || this.ReadData() == 0) && (literalType == DtdParser.LiteralType.SystemOrPublicID || !this.HandleEntityEnd(true)))
					{
						this.Throw(this.curPos, "There is an unclosed literal string.");
					}
					this.tokenStartPos = this.curPos;
				}
				else
				{
					this.curPos++;
				}
			}
			if (this.stringBuilder.Length > 0)
			{
				this.stringBuilder.Append(this.chars, this.tokenStartPos, this.curPos - this.tokenStartPos);
			}
			this.curPos++;
			this.literalQuoteChar = c;
			return DtdParser.Token.Literal;
			IL_5B4:
			this.ThrowInvalidChar(this.chars, this.charsUsed, this.curPos);
			return DtdParser.Token.None;
		}

		// Token: 0x0600136F RID: 4975 RVA: 0x00074994 File Offset: 0x00072B94
		private XmlQualifiedName ScanEntityName()
		{
			try
			{
				this.ScanName();
			}
			catch (XmlException ex)
			{
				this.Throw("An error occurred while parsing EntityName.", string.Empty, ex.LineNumber, ex.LinePosition);
			}
			if (this.chars[this.curPos] != ';')
			{
				this.ThrowUnexpectedToken(this.curPos, ";");
			}
			XmlQualifiedName nameQualified = this.GetNameQualified(false);
			this.curPos++;
			return nameQualified;
		}

		// Token: 0x06001370 RID: 4976 RVA: 0x00074A10 File Offset: 0x00072C10
		private DtdParser.Token ScanNotation1()
		{
			char c = this.chars[this.curPos];
			if (c == 'P')
			{
				if (!this.EatPublicKeyword())
				{
					this.Throw(this.curPos, "Expecting external ID, '[' or '>'.");
				}
				this.nextScaningFunction = DtdParser.ScanningFunction.ClosingTag;
				this.scanningFunction = DtdParser.ScanningFunction.PublicId1;
				return DtdParser.Token.PUBLIC;
			}
			if (c != 'S')
			{
				this.Throw(this.curPos, "Expecting a system identifier or a public identifier.");
				return DtdParser.Token.None;
			}
			if (!this.EatSystemKeyword())
			{
				this.Throw(this.curPos, "Expecting external ID, '[' or '>'.");
			}
			this.nextScaningFunction = DtdParser.ScanningFunction.ClosingTag;
			this.scanningFunction = DtdParser.ScanningFunction.SystemId;
			return DtdParser.Token.SYSTEM;
		}

		// Token: 0x06001371 RID: 4977 RVA: 0x00074AA4 File Offset: 0x00072CA4
		private DtdParser.Token ScanSystemId()
		{
			if (this.chars[this.curPos] != '"' && this.chars[this.curPos] != '\'')
			{
				this.ThrowUnexpectedToken(this.curPos, "\"", "'");
			}
			this.ScanLiteral(DtdParser.LiteralType.SystemOrPublicID);
			this.scanningFunction = this.nextScaningFunction;
			return DtdParser.Token.Literal;
		}

		// Token: 0x06001372 RID: 4978 RVA: 0x00074B00 File Offset: 0x00072D00
		private DtdParser.Token ScanEntity1()
		{
			if (this.chars[this.curPos] == '%')
			{
				this.curPos++;
				this.nextScaningFunction = DtdParser.ScanningFunction.Entity2;
				this.scanningFunction = DtdParser.ScanningFunction.Name;
				return DtdParser.Token.Percent;
			}
			this.ScanName();
			this.scanningFunction = DtdParser.ScanningFunction.Entity2;
			return DtdParser.Token.Name;
		}

		// Token: 0x06001373 RID: 4979 RVA: 0x00074B50 File Offset: 0x00072D50
		private DtdParser.Token ScanEntity2()
		{
			char c = this.chars[this.curPos];
			if (c <= '\'')
			{
				if (c == '"' || c == '\'')
				{
					this.ScanLiteral(DtdParser.LiteralType.EntityReplText);
					this.scanningFunction = DtdParser.ScanningFunction.ClosingTag;
					return DtdParser.Token.Literal;
				}
			}
			else
			{
				if (c == 'P')
				{
					if (!this.EatPublicKeyword())
					{
						this.Throw(this.curPos, "Expecting external ID, '[' or '>'.");
					}
					this.nextScaningFunction = DtdParser.ScanningFunction.Entity3;
					this.scanningFunction = DtdParser.ScanningFunction.PublicId1;
					return DtdParser.Token.PUBLIC;
				}
				if (c == 'S')
				{
					if (!this.EatSystemKeyword())
					{
						this.Throw(this.curPos, "Expecting external ID, '[' or '>'.");
					}
					this.nextScaningFunction = DtdParser.ScanningFunction.Entity3;
					this.scanningFunction = DtdParser.ScanningFunction.SystemId;
					return DtdParser.Token.SYSTEM;
				}
			}
			this.Throw(this.curPos, "Expecting an external identifier or an entity value.");
			return DtdParser.Token.None;
		}

		// Token: 0x06001374 RID: 4980 RVA: 0x00074C08 File Offset: 0x00072E08
		private DtdParser.Token ScanEntity3()
		{
			if (this.chars[this.curPos] == 'N')
			{
				while (this.charsUsed - this.curPos < 5)
				{
					if (this.ReadData() == 0)
					{
						goto IL_9A;
					}
				}
				if (this.chars[this.curPos + 1] == 'D' && this.chars[this.curPos + 2] == 'A' && this.chars[this.curPos + 3] == 'T' && this.chars[this.curPos + 4] == 'A')
				{
					this.curPos += 5;
					this.scanningFunction = DtdParser.ScanningFunction.Name;
					this.nextScaningFunction = DtdParser.ScanningFunction.ClosingTag;
					return DtdParser.Token.NData;
				}
			}
			IL_9A:
			this.scanningFunction = DtdParser.ScanningFunction.ClosingTag;
			return DtdParser.Token.None;
		}

		// Token: 0x06001375 RID: 4981 RVA: 0x00074CBC File Offset: 0x00072EBC
		private DtdParser.Token ScanPublicId1()
		{
			if (this.chars[this.curPos] != '"' && this.chars[this.curPos] != '\'')
			{
				this.ThrowUnexpectedToken(this.curPos, "\"", "'");
			}
			this.ScanLiteral(DtdParser.LiteralType.SystemOrPublicID);
			this.scanningFunction = DtdParser.ScanningFunction.PublicId2;
			return DtdParser.Token.Literal;
		}

		// Token: 0x06001376 RID: 4982 RVA: 0x00074D14 File Offset: 0x00072F14
		private DtdParser.Token ScanPublicId2()
		{
			if (this.chars[this.curPos] != '"' && this.chars[this.curPos] != '\'')
			{
				this.scanningFunction = this.nextScaningFunction;
				return DtdParser.Token.None;
			}
			this.ScanLiteral(DtdParser.LiteralType.SystemOrPublicID);
			this.scanningFunction = this.nextScaningFunction;
			return DtdParser.Token.Literal;
		}

		// Token: 0x06001377 RID: 4983 RVA: 0x00074D68 File Offset: 0x00072F68
		private DtdParser.Token ScanCondSection1()
		{
			if (this.chars[this.curPos] != 'I')
			{
				this.Throw(this.curPos, "Conditional sections must specify the keyword 'IGNORE' or 'INCLUDE'.");
			}
			this.curPos++;
			for (;;)
			{
				if (this.charsUsed - this.curPos >= 5)
				{
					char c = this.chars[this.curPos];
					if (c == 'G')
					{
						goto IL_121;
					}
					if (c != 'N')
					{
						goto IL_1AA;
					}
					if (this.charsUsed - this.curPos >= 6)
					{
						break;
					}
				}
				if (this.ReadData() == 0)
				{
					this.Throw(this.curPos, "Incomplete DTD content.");
				}
			}
			if (this.chars[this.curPos + 1] == 'C' && this.chars[this.curPos + 2] == 'L' && this.chars[this.curPos + 3] == 'U' && this.chars[this.curPos + 4] == 'D' && this.chars[this.curPos + 5] == 'E' && !this.xmlCharType.IsNameSingleChar(this.chars[this.curPos + 6]))
			{
				this.nextScaningFunction = DtdParser.ScanningFunction.SubsetContent;
				this.scanningFunction = DtdParser.ScanningFunction.CondSection2;
				this.curPos += 6;
				return DtdParser.Token.INCLUDE;
			}
			goto IL_1AA;
			IL_121:
			if (this.chars[this.curPos + 1] == 'N' && this.chars[this.curPos + 2] == 'O' && this.chars[this.curPos + 3] == 'R' && this.chars[this.curPos + 4] == 'E' && !this.xmlCharType.IsNameSingleChar(this.chars[this.curPos + 5]))
			{
				this.nextScaningFunction = DtdParser.ScanningFunction.CondSection3;
				this.scanningFunction = DtdParser.ScanningFunction.CondSection2;
				this.curPos += 5;
				return DtdParser.Token.IGNORE;
			}
			IL_1AA:
			this.Throw(this.curPos - 1, "Conditional sections must specify the keyword 'IGNORE' or 'INCLUDE'.");
			return DtdParser.Token.None;
		}

		// Token: 0x06001378 RID: 4984 RVA: 0x00074F55 File Offset: 0x00073155
		private DtdParser.Token ScanCondSection2()
		{
			if (this.chars[this.curPos] != '[')
			{
				this.ThrowUnexpectedToken(this.curPos, "[");
			}
			this.curPos++;
			this.scanningFunction = this.nextScaningFunction;
			return DtdParser.Token.LeftBracket;
		}

		// Token: 0x06001379 RID: 4985 RVA: 0x00074F98 File Offset: 0x00073198
		private DtdParser.Token ScanCondSection3()
		{
			int num = 0;
			for (;;)
			{
				if ((this.xmlCharType.charProperties[(int)this.chars[this.curPos]] & 64) == 0 || this.chars[this.curPos] == ']')
				{
					char c = this.chars[this.curPos];
					if (c <= '&')
					{
						switch (c)
						{
						case '\t':
							break;
						case '\n':
							this.curPos++;
							this.readerAdapter.OnNewLine(this.curPos);
							continue;
						case '\v':
						case '\f':
							goto IL_21A;
						case '\r':
							if (this.chars[this.curPos + 1] == '\n')
							{
								this.curPos += 2;
							}
							else
							{
								if (this.curPos + 1 >= this.charsUsed && !this.readerAdapter.IsEof)
								{
									goto IL_29C;
								}
								this.curPos++;
							}
							this.readerAdapter.OnNewLine(this.curPos);
							continue;
						default:
							if (c != '"' && c != '&')
							{
								goto IL_21A;
							}
							break;
						}
					}
					else if (c != '\'')
					{
						if (c != '<')
						{
							if (c != ']')
							{
								goto IL_21A;
							}
							if (this.charsUsed - this.curPos < 3)
							{
								goto IL_29C;
							}
							if (this.chars[this.curPos + 1] != ']' || this.chars[this.curPos + 2] != '>')
							{
								this.curPos++;
								continue;
							}
							if (num > 0)
							{
								num--;
								this.curPos += 3;
								continue;
							}
							break;
						}
						else
						{
							if (this.charsUsed - this.curPos < 3)
							{
								goto IL_29C;
							}
							if (this.chars[this.curPos + 1] != '!' || this.chars[this.curPos + 2] != '[')
							{
								this.curPos++;
								continue;
							}
							num++;
							this.curPos += 3;
							continue;
						}
					}
					this.curPos++;
					continue;
					IL_21A:
					if (this.curPos != this.charsUsed)
					{
						if (!XmlCharType.IsHighSurrogate((int)this.chars[this.curPos]))
						{
							goto IL_281;
						}
						if (this.curPos + 1 != this.charsUsed)
						{
							this.curPos++;
							if (XmlCharType.IsLowSurrogate((int)this.chars[this.curPos]))
							{
								this.curPos++;
								continue;
							}
							goto IL_281;
						}
					}
					IL_29C:
					if (this.readerAdapter.IsEof || this.ReadData() == 0)
					{
						if (this.HandleEntityEnd(false))
						{
							continue;
						}
						this.Throw(this.curPos, "There is an unclosed conditional section.");
					}
					this.tokenStartPos = this.curPos;
				}
				else
				{
					this.curPos++;
				}
			}
			this.curPos += 3;
			this.scanningFunction = DtdParser.ScanningFunction.SubsetContent;
			return DtdParser.Token.CondSectionEnd;
			IL_281:
			this.ThrowInvalidChar(this.chars, this.charsUsed, this.curPos);
			return DtdParser.Token.None;
		}

		// Token: 0x0600137A RID: 4986 RVA: 0x00075283 File Offset: 0x00073483
		private void ScanName()
		{
			this.ScanQName(false);
		}

		// Token: 0x0600137B RID: 4987 RVA: 0x0007528C File Offset: 0x0007348C
		private void ScanQName()
		{
			this.ScanQName(this.SupportNamespaces);
		}

		// Token: 0x0600137C RID: 4988 RVA: 0x0007529C File Offset: 0x0007349C
		private void ScanQName(bool isQName)
		{
			this.tokenStartPos = this.curPos;
			int num = -1;
			for (;;)
			{
				if ((this.xmlCharType.charProperties[(int)this.chars[this.curPos]] & 4) != 0 || this.chars[this.curPos] == ':')
				{
					this.curPos++;
				}
				else if (this.curPos + 1 >= this.charsUsed)
				{
					if (this.ReadDataInName())
					{
						continue;
					}
					this.Throw(this.curPos, "Unexpected end of file while parsing {0} has occurred.", "Name");
				}
				else
				{
					this.Throw(this.curPos, "Name cannot begin with the '{0}' character, hexadecimal value {1}.", XmlException.BuildCharExceptionArgs(this.chars, this.charsUsed, this.curPos));
				}
				for (;;)
				{
					if ((this.xmlCharType.charProperties[(int)this.chars[this.curPos]] & 8) != 0)
					{
						this.curPos++;
					}
					else if (this.chars[this.curPos] == ':')
					{
						if (isQName)
						{
							break;
						}
						this.curPos++;
					}
					else
					{
						if (this.curPos != this.charsUsed)
						{
							goto IL_173;
						}
						if (!this.ReadDataInName())
						{
							goto Block_9;
						}
					}
				}
				if (num != -1)
				{
					this.Throw(this.curPos, "The '{0}' character, hexadecimal value {1}, cannot be included in a name.", XmlException.BuildCharExceptionArgs(':', '\0'));
				}
				num = this.curPos - this.tokenStartPos;
				this.curPos++;
			}
			Block_9:
			if (this.tokenStartPos == this.curPos)
			{
				this.Throw(this.curPos, "Unexpected end of file while parsing {0} has occurred.", "Name");
			}
			IL_173:
			this.colonPos = ((num == -1) ? -1 : (this.tokenStartPos + num));
		}

		// Token: 0x0600137D RID: 4989 RVA: 0x00075434 File Offset: 0x00073634
		private bool ReadDataInName()
		{
			int num = this.curPos - this.tokenStartPos;
			this.curPos = this.tokenStartPos;
			bool result = this.ReadData() != 0;
			this.tokenStartPos = this.curPos;
			this.curPos += num;
			return result;
		}

		// Token: 0x0600137E RID: 4990 RVA: 0x00075480 File Offset: 0x00073680
		private void ScanNmtoken()
		{
			this.tokenStartPos = this.curPos;
			int num;
			for (;;)
			{
				if ((this.xmlCharType.charProperties[(int)this.chars[this.curPos]] & 8) != 0 || this.chars[this.curPos] == ':')
				{
					this.curPos++;
				}
				else
				{
					if (this.curPos < this.charsUsed)
					{
						break;
					}
					num = this.curPos - this.tokenStartPos;
					this.curPos = this.tokenStartPos;
					if (this.ReadData() == 0)
					{
						if (num > 0)
						{
							goto Block_5;
						}
						this.Throw(this.curPos, "Unexpected end of file while parsing {0} has occurred.", "NmToken");
					}
					this.tokenStartPos = this.curPos;
					this.curPos += num;
				}
			}
			if (this.curPos - this.tokenStartPos == 0)
			{
				this.Throw(this.curPos, "The '{0}' character, hexadecimal value {1}, cannot be included in a name.", XmlException.BuildCharExceptionArgs(this.chars, this.charsUsed, this.curPos));
			}
			return;
			Block_5:
			this.tokenStartPos = this.curPos;
			this.curPos += num;
		}

		// Token: 0x0600137F RID: 4991 RVA: 0x00075594 File Offset: 0x00073794
		private bool EatPublicKeyword()
		{
			while (this.charsUsed - this.curPos < 6)
			{
				if (this.ReadData() == 0)
				{
					return false;
				}
			}
			if (this.chars[this.curPos + 1] != 'U' || this.chars[this.curPos + 2] != 'B' || this.chars[this.curPos + 3] != 'L' || this.chars[this.curPos + 4] != 'I' || this.chars[this.curPos + 5] != 'C')
			{
				return false;
			}
			this.curPos += 6;
			return true;
		}

		// Token: 0x06001380 RID: 4992 RVA: 0x00075630 File Offset: 0x00073830
		private bool EatSystemKeyword()
		{
			while (this.charsUsed - this.curPos < 6)
			{
				if (this.ReadData() == 0)
				{
					return false;
				}
			}
			if (this.chars[this.curPos + 1] != 'Y' || this.chars[this.curPos + 2] != 'S' || this.chars[this.curPos + 3] != 'T' || this.chars[this.curPos + 4] != 'E' || this.chars[this.curPos + 5] != 'M')
			{
				return false;
			}
			this.curPos += 6;
			return true;
		}

		// Token: 0x06001381 RID: 4993 RVA: 0x000756CC File Offset: 0x000738CC
		private XmlQualifiedName GetNameQualified(bool canHavePrefix)
		{
			if (this.colonPos == -1)
			{
				return new XmlQualifiedName(this.nameTable.Add(this.chars, this.tokenStartPos, this.curPos - this.tokenStartPos));
			}
			if (canHavePrefix)
			{
				return new XmlQualifiedName(this.nameTable.Add(this.chars, this.colonPos + 1, this.curPos - this.colonPos - 1), this.nameTable.Add(this.chars, this.tokenStartPos, this.colonPos - this.tokenStartPos));
			}
			this.Throw(this.tokenStartPos, "'{0}' is an unqualified name and cannot contain the character ':'.", this.GetNameString());
			return null;
		}

		// Token: 0x06001382 RID: 4994 RVA: 0x00075779 File Offset: 0x00073979
		private string GetNameString()
		{
			return new string(this.chars, this.tokenStartPos, this.curPos - this.tokenStartPos);
		}

		// Token: 0x06001383 RID: 4995 RVA: 0x00075799 File Offset: 0x00073999
		private string GetNmtokenString()
		{
			return this.GetNameString();
		}

		// Token: 0x06001384 RID: 4996 RVA: 0x000757A1 File Offset: 0x000739A1
		private string GetValue()
		{
			if (this.stringBuilder.Length == 0)
			{
				return new string(this.chars, this.tokenStartPos, this.curPos - this.tokenStartPos - 1);
			}
			return this.stringBuilder.ToString();
		}

		// Token: 0x06001385 RID: 4997 RVA: 0x000757DC File Offset: 0x000739DC
		private string GetValueWithStrippedSpaces()
		{
			return DtdParser.StripSpaces((this.stringBuilder.Length == 0) ? new string(this.chars, this.tokenStartPos, this.curPos - this.tokenStartPos - 1) : this.stringBuilder.ToString());
		}

		// Token: 0x06001386 RID: 4998 RVA: 0x00075828 File Offset: 0x00073A28
		private int ReadData()
		{
			this.SaveParsingBuffer();
			int result = this.readerAdapter.ReadData();
			this.LoadParsingBuffer();
			return result;
		}

		// Token: 0x06001387 RID: 4999 RVA: 0x00075841 File Offset: 0x00073A41
		private void LoadParsingBuffer()
		{
			this.chars = this.readerAdapter.ParsingBuffer;
			this.charsUsed = this.readerAdapter.ParsingBufferLength;
			this.curPos = this.readerAdapter.CurrentPosition;
		}

		// Token: 0x06001388 RID: 5000 RVA: 0x00075876 File Offset: 0x00073A76
		private void SaveParsingBuffer()
		{
			this.SaveParsingBuffer(this.curPos);
		}

		// Token: 0x06001389 RID: 5001 RVA: 0x00075884 File Offset: 0x00073A84
		private void SaveParsingBuffer(int internalSubsetValueEndPos)
		{
			if (this.SaveInternalSubsetValue)
			{
				int currentPosition = this.readerAdapter.CurrentPosition;
				if (internalSubsetValueEndPos - currentPosition > 0)
				{
					this.internalSubsetValueSb.Append(this.chars, currentPosition, internalSubsetValueEndPos - currentPosition);
				}
			}
			this.readerAdapter.CurrentPosition = this.curPos;
		}

		// Token: 0x0600138A RID: 5002 RVA: 0x000758D2 File Offset: 0x00073AD2
		private bool HandleEntityReference(bool paramEntity, bool inLiteral, bool inAttribute)
		{
			this.curPos++;
			return this.HandleEntityReference(this.ScanEntityName(), paramEntity, inLiteral, inAttribute);
		}

		// Token: 0x0600138B RID: 5003 RVA: 0x000758F4 File Offset: 0x00073AF4
		private bool HandleEntityReference(XmlQualifiedName entityName, bool paramEntity, bool inLiteral, bool inAttribute)
		{
			this.SaveParsingBuffer();
			if (paramEntity && this.ParsingInternalSubset && !this.ParsingTopLevelMarkup)
			{
				this.Throw(this.curPos - entityName.Name.Length - 1, "A parameter entity reference is not allowed in internal markup.");
			}
			SchemaEntity schemaEntity = this.VerifyEntityReference(entityName, paramEntity, true, inAttribute);
			if (schemaEntity == null)
			{
				return false;
			}
			if (schemaEntity.ParsingInProgress)
			{
				this.Throw(this.curPos - entityName.Name.Length - 1, paramEntity ? "Parameter entity '{0}' references itself." : "General entity '{0}' references itself.", entityName.Name);
			}
			int num;
			if (schemaEntity.IsExternal)
			{
				if (!this.readerAdapter.PushEntity(schemaEntity, out num))
				{
					return false;
				}
				this.externalEntitiesDepth++;
			}
			else
			{
				if (schemaEntity.Text.Length == 0)
				{
					return false;
				}
				if (!this.readerAdapter.PushEntity(schemaEntity, out num))
				{
					return false;
				}
			}
			this.currentEntityId = num;
			if (paramEntity && !inLiteral && this.scanningFunction != DtdParser.ScanningFunction.ParamEntitySpace)
			{
				this.savedScanningFunction = this.scanningFunction;
				this.scanningFunction = DtdParser.ScanningFunction.ParamEntitySpace;
			}
			this.LoadParsingBuffer();
			return true;
		}

		// Token: 0x0600138C RID: 5004 RVA: 0x00075A00 File Offset: 0x00073C00
		private bool HandleEntityEnd(bool inLiteral)
		{
			this.SaveParsingBuffer();
			IDtdEntityInfo dtdEntityInfo;
			if (!this.readerAdapter.PopEntity(out dtdEntityInfo, out this.currentEntityId))
			{
				return false;
			}
			this.LoadParsingBuffer();
			if (dtdEntityInfo == null)
			{
				if (this.scanningFunction == DtdParser.ScanningFunction.ParamEntitySpace)
				{
					this.scanningFunction = this.savedScanningFunction;
				}
				return false;
			}
			if (dtdEntityInfo.IsExternal)
			{
				this.externalEntitiesDepth--;
			}
			if (!inLiteral && this.scanningFunction != DtdParser.ScanningFunction.ParamEntitySpace)
			{
				this.savedScanningFunction = this.scanningFunction;
				this.scanningFunction = DtdParser.ScanningFunction.ParamEntitySpace;
			}
			return true;
		}

		// Token: 0x0600138D RID: 5005 RVA: 0x00075A84 File Offset: 0x00073C84
		private SchemaEntity VerifyEntityReference(XmlQualifiedName entityName, bool paramEntity, bool mustBeDeclared, bool inAttribute)
		{
			SchemaEntity schemaEntity;
			if (paramEntity)
			{
				this.schemaInfo.ParameterEntities.TryGetValue(entityName, out schemaEntity);
			}
			else
			{
				this.schemaInfo.GeneralEntities.TryGetValue(entityName, out schemaEntity);
			}
			if (schemaEntity == null)
			{
				if (paramEntity)
				{
					if (this.validate)
					{
						this.SendValidationEvent(this.curPos - entityName.Name.Length - 1, XmlSeverityType.Error, "Reference to undeclared parameter entity '{0}'.", entityName.Name);
					}
				}
				else if (mustBeDeclared)
				{
					if (!this.ParsingInternalSubset)
					{
						if (this.validate)
						{
							this.SendValidationEvent(this.curPos - entityName.Name.Length - 1, XmlSeverityType.Error, "Reference to undeclared entity '{0}'.", entityName.Name);
						}
					}
					else
					{
						this.Throw(this.curPos - entityName.Name.Length - 1, "Reference to undeclared entity '{0}'.", entityName.Name);
					}
				}
				return null;
			}
			if (!schemaEntity.NData.IsEmpty)
			{
				this.Throw(this.curPos - entityName.Name.Length - 1, "Reference to unparsed entity '{0}'.", entityName.Name);
			}
			if (inAttribute && schemaEntity.IsExternal)
			{
				this.Throw(this.curPos - entityName.Name.Length - 1, "External entity '{0}' reference cannot appear in the attribute value.", entityName.Name);
			}
			return schemaEntity;
		}

		// Token: 0x0600138E RID: 5006 RVA: 0x00075BC0 File Offset: 0x00073DC0
		private void SendValidationEvent(int pos, XmlSeverityType severity, string code, string arg)
		{
			this.SendValidationEvent(severity, new XmlSchemaException(code, arg, this.BaseUriStr, this.LineNo, this.LinePos + (pos - this.curPos)));
		}

		// Token: 0x0600138F RID: 5007 RVA: 0x00075BF7 File Offset: 0x00073DF7
		private void SendValidationEvent(XmlSeverityType severity, string code, string arg)
		{
			this.SendValidationEvent(severity, new XmlSchemaException(code, arg, this.BaseUriStr, this.LineNo, this.LinePos));
		}

		// Token: 0x06001390 RID: 5008 RVA: 0x00075C1C File Offset: 0x00073E1C
		private void SendValidationEvent(XmlSeverityType severity, XmlSchemaException e)
		{
			IValidationEventHandling validationEventHandling = this.readerAdapterWithValidation.ValidationEventHandling;
			if (validationEventHandling != null)
			{
				validationEventHandling.SendEvent(e, severity);
			}
		}

		// Token: 0x06001391 RID: 5009 RVA: 0x00075C40 File Offset: 0x00073E40
		private bool IsAttributeValueType(DtdParser.Token token)
		{
			return token >= DtdParser.Token.CDATA && token <= DtdParser.Token.NOTATION;
		}

		// Token: 0x170003B5 RID: 949
		// (get) Token: 0x06001392 RID: 5010 RVA: 0x00075C4F File Offset: 0x00073E4F
		private int LineNo
		{
			get
			{
				return this.readerAdapter.LineNo;
			}
		}

		// Token: 0x170003B6 RID: 950
		// (get) Token: 0x06001393 RID: 5011 RVA: 0x00075C5C File Offset: 0x00073E5C
		private int LinePos
		{
			get
			{
				return this.curPos - this.readerAdapter.LineStartPosition;
			}
		}

		// Token: 0x170003B7 RID: 951
		// (get) Token: 0x06001394 RID: 5012 RVA: 0x00075C70 File Offset: 0x00073E70
		private string BaseUriStr
		{
			get
			{
				Uri baseUri = this.readerAdapter.BaseUri;
				if (!(baseUri != null))
				{
					return string.Empty;
				}
				return baseUri.ToString();
			}
		}

		// Token: 0x06001395 RID: 5013 RVA: 0x00075C9E File Offset: 0x00073E9E
		private void OnUnexpectedError()
		{
			this.Throw(this.curPos, "An internal error has occurred.");
		}

		// Token: 0x06001396 RID: 5014 RVA: 0x00075CB1 File Offset: 0x00073EB1
		private void Throw(int curPos, string res)
		{
			this.Throw(curPos, res, string.Empty);
		}

		// Token: 0x06001397 RID: 5015 RVA: 0x00075CC0 File Offset: 0x00073EC0
		private void Throw(int curPos, string res, string arg)
		{
			this.curPos = curPos;
			Uri baseUri = this.readerAdapter.BaseUri;
			this.readerAdapter.Throw(new XmlException(res, arg, this.LineNo, this.LinePos, (baseUri == null) ? null : baseUri.ToString()));
		}

		// Token: 0x06001398 RID: 5016 RVA: 0x00075D10 File Offset: 0x00073F10
		private void Throw(int curPos, string res, string[] args)
		{
			this.curPos = curPos;
			Uri baseUri = this.readerAdapter.BaseUri;
			this.readerAdapter.Throw(new XmlException(res, args, this.LineNo, this.LinePos, (baseUri == null) ? null : baseUri.ToString()));
		}

		// Token: 0x06001399 RID: 5017 RVA: 0x00075D60 File Offset: 0x00073F60
		private void Throw(string res, string arg, int lineNo, int linePos)
		{
			Uri baseUri = this.readerAdapter.BaseUri;
			this.readerAdapter.Throw(new XmlException(res, arg, lineNo, linePos, (baseUri == null) ? null : baseUri.ToString()));
		}

		// Token: 0x0600139A RID: 5018 RVA: 0x00075DA0 File Offset: 0x00073FA0
		private void ThrowInvalidChar(int pos, string data, int invCharPos)
		{
			this.Throw(pos, "'{0}', hexadecimal value {1}, is an invalid character.", XmlException.BuildCharExceptionArgs(data, invCharPos));
		}

		// Token: 0x0600139B RID: 5019 RVA: 0x00075DB5 File Offset: 0x00073FB5
		private void ThrowInvalidChar(char[] data, int length, int invCharPos)
		{
			this.Throw(invCharPos, "'{0}', hexadecimal value {1}, is an invalid character.", XmlException.BuildCharExceptionArgs(data, length, invCharPos));
		}

		// Token: 0x0600139C RID: 5020 RVA: 0x00075DCB File Offset: 0x00073FCB
		private void ThrowUnexpectedToken(int pos, string expectedToken)
		{
			this.ThrowUnexpectedToken(pos, expectedToken, null);
		}

		// Token: 0x0600139D RID: 5021 RVA: 0x00075DD8 File Offset: 0x00073FD8
		private void ThrowUnexpectedToken(int pos, string expectedToken1, string expectedToken2)
		{
			string text = this.ParseUnexpectedToken(pos);
			if (expectedToken2 != null)
			{
				this.Throw(this.curPos, "'{0}' is an unexpected token. The expected token is '{1}' or '{2}'.", new string[]
				{
					text,
					expectedToken1,
					expectedToken2
				});
				return;
			}
			this.Throw(this.curPos, "'{0}' is an unexpected token. The expected token is '{1}'.", new string[]
			{
				text,
				expectedToken1
			});
		}

		// Token: 0x0600139E RID: 5022 RVA: 0x00075E34 File Offset: 0x00074034
		private string ParseUnexpectedToken(int startPos)
		{
			if (this.xmlCharType.IsNCNameSingleChar(this.chars[startPos]))
			{
				int num = startPos;
				while (this.xmlCharType.IsNCNameSingleChar(this.chars[num]))
				{
					num++;
				}
				int num2 = num - startPos;
				return new string(this.chars, startPos, (num2 > 0) ? num2 : 1);
			}
			return new string(this.chars, startPos, 1);
		}

		// Token: 0x0600139F RID: 5023 RVA: 0x00075E9C File Offset: 0x0007409C
		internal static string StripSpaces(string value)
		{
			int length = value.Length;
			if (length <= 0)
			{
				return string.Empty;
			}
			int num = 0;
			StringBuilder stringBuilder = null;
			while (value[num] == ' ')
			{
				num++;
				if (num == length)
				{
					return " ";
				}
			}
			int i;
			for (i = num; i < length; i++)
			{
				if (value[i] == ' ')
				{
					int num2 = i + 1;
					while (num2 < length && value[num2] == ' ')
					{
						num2++;
					}
					if (num2 == length)
					{
						if (stringBuilder == null)
						{
							return value.Substring(num, i - num);
						}
						stringBuilder.Append(value, num, i - num);
						return stringBuilder.ToString();
					}
					else if (num2 > i + 1)
					{
						if (stringBuilder == null)
						{
							stringBuilder = new StringBuilder(length);
						}
						stringBuilder.Append(value, num, i - num + 1);
						num = num2;
						i = num2 - 1;
					}
				}
			}
			if (stringBuilder != null)
			{
				if (i > num)
				{
					stringBuilder.Append(value, num, i - num);
				}
				return stringBuilder.ToString();
			}
			if (num != 0)
			{
				return value.Substring(num, length - num);
			}
			return value;
		}

		// Token: 0x060013A0 RID: 5024 RVA: 0x00075F84 File Offset: 0x00074184
		Task<IDtdInfo> IDtdParser.ParseInternalDtdAsync(IDtdParserAdapter adapter, bool saveInternalSubset)
		{
			DtdParser.<System-Xml-IDtdParser-ParseInternalDtdAsync>d__153 <System-Xml-IDtdParser-ParseInternalDtdAsync>d__;
			<System-Xml-IDtdParser-ParseInternalDtdAsync>d__.<>4__this = this;
			<System-Xml-IDtdParser-ParseInternalDtdAsync>d__.adapter = adapter;
			<System-Xml-IDtdParser-ParseInternalDtdAsync>d__.saveInternalSubset = saveInternalSubset;
			<System-Xml-IDtdParser-ParseInternalDtdAsync>d__.<>t__builder = AsyncTaskMethodBuilder<IDtdInfo>.Create();
			<System-Xml-IDtdParser-ParseInternalDtdAsync>d__.<>1__state = -1;
			<System-Xml-IDtdParser-ParseInternalDtdAsync>d__.<>t__builder.Start<DtdParser.<System-Xml-IDtdParser-ParseInternalDtdAsync>d__153>(ref <System-Xml-IDtdParser-ParseInternalDtdAsync>d__);
			return <System-Xml-IDtdParser-ParseInternalDtdAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060013A1 RID: 5025 RVA: 0x00075FD8 File Offset: 0x000741D8
		Task<IDtdInfo> IDtdParser.ParseFreeFloatingDtdAsync(string baseUri, string docTypeName, string publicId, string systemId, string internalSubset, IDtdParserAdapter adapter)
		{
			DtdParser.<System-Xml-IDtdParser-ParseFreeFloatingDtdAsync>d__154 <System-Xml-IDtdParser-ParseFreeFloatingDtdAsync>d__;
			<System-Xml-IDtdParser-ParseFreeFloatingDtdAsync>d__.<>4__this = this;
			<System-Xml-IDtdParser-ParseFreeFloatingDtdAsync>d__.baseUri = baseUri;
			<System-Xml-IDtdParser-ParseFreeFloatingDtdAsync>d__.docTypeName = docTypeName;
			<System-Xml-IDtdParser-ParseFreeFloatingDtdAsync>d__.publicId = publicId;
			<System-Xml-IDtdParser-ParseFreeFloatingDtdAsync>d__.systemId = systemId;
			<System-Xml-IDtdParser-ParseFreeFloatingDtdAsync>d__.internalSubset = internalSubset;
			<System-Xml-IDtdParser-ParseFreeFloatingDtdAsync>d__.adapter = adapter;
			<System-Xml-IDtdParser-ParseFreeFloatingDtdAsync>d__.<>t__builder = AsyncTaskMethodBuilder<IDtdInfo>.Create();
			<System-Xml-IDtdParser-ParseFreeFloatingDtdAsync>d__.<>1__state = -1;
			<System-Xml-IDtdParser-ParseFreeFloatingDtdAsync>d__.<>t__builder.Start<DtdParser.<System-Xml-IDtdParser-ParseFreeFloatingDtdAsync>d__154>(ref <System-Xml-IDtdParser-ParseFreeFloatingDtdAsync>d__);
			return <System-Xml-IDtdParser-ParseFreeFloatingDtdAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060013A2 RID: 5026 RVA: 0x00076050 File Offset: 0x00074250
		private Task ParseAsync(bool saveInternalSubset)
		{
			DtdParser.<ParseAsync>d__155 <ParseAsync>d__;
			<ParseAsync>d__.<>4__this = this;
			<ParseAsync>d__.saveInternalSubset = saveInternalSubset;
			<ParseAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<ParseAsync>d__.<>1__state = -1;
			<ParseAsync>d__.<>t__builder.Start<DtdParser.<ParseAsync>d__155>(ref <ParseAsync>d__);
			return <ParseAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060013A3 RID: 5027 RVA: 0x0007609C File Offset: 0x0007429C
		private Task ParseInDocumentDtdAsync(bool saveInternalSubset)
		{
			DtdParser.<ParseInDocumentDtdAsync>d__156 <ParseInDocumentDtdAsync>d__;
			<ParseInDocumentDtdAsync>d__.<>4__this = this;
			<ParseInDocumentDtdAsync>d__.saveInternalSubset = saveInternalSubset;
			<ParseInDocumentDtdAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<ParseInDocumentDtdAsync>d__.<>1__state = -1;
			<ParseInDocumentDtdAsync>d__.<>t__builder.Start<DtdParser.<ParseInDocumentDtdAsync>d__156>(ref <ParseInDocumentDtdAsync>d__);
			return <ParseInDocumentDtdAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060013A4 RID: 5028 RVA: 0x000760E8 File Offset: 0x000742E8
		private Task ParseFreeFloatingDtdAsync()
		{
			DtdParser.<ParseFreeFloatingDtdAsync>d__157 <ParseFreeFloatingDtdAsync>d__;
			<ParseFreeFloatingDtdAsync>d__.<>4__this = this;
			<ParseFreeFloatingDtdAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<ParseFreeFloatingDtdAsync>d__.<>1__state = -1;
			<ParseFreeFloatingDtdAsync>d__.<>t__builder.Start<DtdParser.<ParseFreeFloatingDtdAsync>d__157>(ref <ParseFreeFloatingDtdAsync>d__);
			return <ParseFreeFloatingDtdAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060013A5 RID: 5029 RVA: 0x0007612B File Offset: 0x0007432B
		private Task ParseInternalSubsetAsync()
		{
			return this.ParseSubsetAsync();
		}

		// Token: 0x060013A6 RID: 5030 RVA: 0x00076134 File Offset: 0x00074334
		private Task ParseExternalSubsetAsync()
		{
			DtdParser.<ParseExternalSubsetAsync>d__159 <ParseExternalSubsetAsync>d__;
			<ParseExternalSubsetAsync>d__.<>4__this = this;
			<ParseExternalSubsetAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<ParseExternalSubsetAsync>d__.<>1__state = -1;
			<ParseExternalSubsetAsync>d__.<>t__builder.Start<DtdParser.<ParseExternalSubsetAsync>d__159>(ref <ParseExternalSubsetAsync>d__);
			return <ParseExternalSubsetAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060013A7 RID: 5031 RVA: 0x00076178 File Offset: 0x00074378
		private Task ParseSubsetAsync()
		{
			DtdParser.<ParseSubsetAsync>d__160 <ParseSubsetAsync>d__;
			<ParseSubsetAsync>d__.<>4__this = this;
			<ParseSubsetAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<ParseSubsetAsync>d__.<>1__state = -1;
			<ParseSubsetAsync>d__.<>t__builder.Start<DtdParser.<ParseSubsetAsync>d__160>(ref <ParseSubsetAsync>d__);
			return <ParseSubsetAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060013A8 RID: 5032 RVA: 0x000761BC File Offset: 0x000743BC
		private Task ParseAttlistDeclAsync()
		{
			DtdParser.<ParseAttlistDeclAsync>d__161 <ParseAttlistDeclAsync>d__;
			<ParseAttlistDeclAsync>d__.<>4__this = this;
			<ParseAttlistDeclAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<ParseAttlistDeclAsync>d__.<>1__state = -1;
			<ParseAttlistDeclAsync>d__.<>t__builder.Start<DtdParser.<ParseAttlistDeclAsync>d__161>(ref <ParseAttlistDeclAsync>d__);
			return <ParseAttlistDeclAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060013A9 RID: 5033 RVA: 0x00076200 File Offset: 0x00074400
		private Task ParseAttlistTypeAsync(SchemaAttDef attrDef, SchemaElementDecl elementDecl, bool ignoreErrors)
		{
			DtdParser.<ParseAttlistTypeAsync>d__162 <ParseAttlistTypeAsync>d__;
			<ParseAttlistTypeAsync>d__.<>4__this = this;
			<ParseAttlistTypeAsync>d__.attrDef = attrDef;
			<ParseAttlistTypeAsync>d__.elementDecl = elementDecl;
			<ParseAttlistTypeAsync>d__.ignoreErrors = ignoreErrors;
			<ParseAttlistTypeAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<ParseAttlistTypeAsync>d__.<>1__state = -1;
			<ParseAttlistTypeAsync>d__.<>t__builder.Start<DtdParser.<ParseAttlistTypeAsync>d__162>(ref <ParseAttlistTypeAsync>d__);
			return <ParseAttlistTypeAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060013AA RID: 5034 RVA: 0x0007625C File Offset: 0x0007445C
		private Task ParseAttlistDefaultAsync(SchemaAttDef attrDef, bool ignoreErrors)
		{
			DtdParser.<ParseAttlistDefaultAsync>d__163 <ParseAttlistDefaultAsync>d__;
			<ParseAttlistDefaultAsync>d__.<>4__this = this;
			<ParseAttlistDefaultAsync>d__.attrDef = attrDef;
			<ParseAttlistDefaultAsync>d__.ignoreErrors = ignoreErrors;
			<ParseAttlistDefaultAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<ParseAttlistDefaultAsync>d__.<>1__state = -1;
			<ParseAttlistDefaultAsync>d__.<>t__builder.Start<DtdParser.<ParseAttlistDefaultAsync>d__163>(ref <ParseAttlistDefaultAsync>d__);
			return <ParseAttlistDefaultAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060013AB RID: 5035 RVA: 0x000762B0 File Offset: 0x000744B0
		private Task ParseElementDeclAsync()
		{
			DtdParser.<ParseElementDeclAsync>d__164 <ParseElementDeclAsync>d__;
			<ParseElementDeclAsync>d__.<>4__this = this;
			<ParseElementDeclAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<ParseElementDeclAsync>d__.<>1__state = -1;
			<ParseElementDeclAsync>d__.<>t__builder.Start<DtdParser.<ParseElementDeclAsync>d__164>(ref <ParseElementDeclAsync>d__);
			return <ParseElementDeclAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060013AC RID: 5036 RVA: 0x000762F4 File Offset: 0x000744F4
		private Task ParseElementOnlyContentAsync(ParticleContentValidator pcv, int startParenEntityId)
		{
			DtdParser.<ParseElementOnlyContentAsync>d__165 <ParseElementOnlyContentAsync>d__;
			<ParseElementOnlyContentAsync>d__.<>4__this = this;
			<ParseElementOnlyContentAsync>d__.pcv = pcv;
			<ParseElementOnlyContentAsync>d__.startParenEntityId = startParenEntityId;
			<ParseElementOnlyContentAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<ParseElementOnlyContentAsync>d__.<>1__state = -1;
			<ParseElementOnlyContentAsync>d__.<>t__builder.Start<DtdParser.<ParseElementOnlyContentAsync>d__165>(ref <ParseElementOnlyContentAsync>d__);
			return <ParseElementOnlyContentAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060013AD RID: 5037 RVA: 0x00076348 File Offset: 0x00074548
		private Task ParseHowManyAsync(ParticleContentValidator pcv)
		{
			DtdParser.<ParseHowManyAsync>d__166 <ParseHowManyAsync>d__;
			<ParseHowManyAsync>d__.<>4__this = this;
			<ParseHowManyAsync>d__.pcv = pcv;
			<ParseHowManyAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<ParseHowManyAsync>d__.<>1__state = -1;
			<ParseHowManyAsync>d__.<>t__builder.Start<DtdParser.<ParseHowManyAsync>d__166>(ref <ParseHowManyAsync>d__);
			return <ParseHowManyAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060013AE RID: 5038 RVA: 0x00076394 File Offset: 0x00074594
		private Task ParseElementMixedContentAsync(ParticleContentValidator pcv, int startParenEntityId)
		{
			DtdParser.<ParseElementMixedContentAsync>d__167 <ParseElementMixedContentAsync>d__;
			<ParseElementMixedContentAsync>d__.<>4__this = this;
			<ParseElementMixedContentAsync>d__.pcv = pcv;
			<ParseElementMixedContentAsync>d__.startParenEntityId = startParenEntityId;
			<ParseElementMixedContentAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<ParseElementMixedContentAsync>d__.<>1__state = -1;
			<ParseElementMixedContentAsync>d__.<>t__builder.Start<DtdParser.<ParseElementMixedContentAsync>d__167>(ref <ParseElementMixedContentAsync>d__);
			return <ParseElementMixedContentAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060013AF RID: 5039 RVA: 0x000763E8 File Offset: 0x000745E8
		private Task ParseEntityDeclAsync()
		{
			DtdParser.<ParseEntityDeclAsync>d__168 <ParseEntityDeclAsync>d__;
			<ParseEntityDeclAsync>d__.<>4__this = this;
			<ParseEntityDeclAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<ParseEntityDeclAsync>d__.<>1__state = -1;
			<ParseEntityDeclAsync>d__.<>t__builder.Start<DtdParser.<ParseEntityDeclAsync>d__168>(ref <ParseEntityDeclAsync>d__);
			return <ParseEntityDeclAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060013B0 RID: 5040 RVA: 0x0007642C File Offset: 0x0007462C
		private Task ParseNotationDeclAsync()
		{
			DtdParser.<ParseNotationDeclAsync>d__169 <ParseNotationDeclAsync>d__;
			<ParseNotationDeclAsync>d__.<>4__this = this;
			<ParseNotationDeclAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<ParseNotationDeclAsync>d__.<>1__state = -1;
			<ParseNotationDeclAsync>d__.<>t__builder.Start<DtdParser.<ParseNotationDeclAsync>d__169>(ref <ParseNotationDeclAsync>d__);
			return <ParseNotationDeclAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060013B1 RID: 5041 RVA: 0x00076470 File Offset: 0x00074670
		private Task ParseCommentAsync()
		{
			DtdParser.<ParseCommentAsync>d__170 <ParseCommentAsync>d__;
			<ParseCommentAsync>d__.<>4__this = this;
			<ParseCommentAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<ParseCommentAsync>d__.<>1__state = -1;
			<ParseCommentAsync>d__.<>t__builder.Start<DtdParser.<ParseCommentAsync>d__170>(ref <ParseCommentAsync>d__);
			return <ParseCommentAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060013B2 RID: 5042 RVA: 0x000764B4 File Offset: 0x000746B4
		private Task ParsePIAsync()
		{
			DtdParser.<ParsePIAsync>d__171 <ParsePIAsync>d__;
			<ParsePIAsync>d__.<>4__this = this;
			<ParsePIAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<ParsePIAsync>d__.<>1__state = -1;
			<ParsePIAsync>d__.<>t__builder.Start<DtdParser.<ParsePIAsync>d__171>(ref <ParsePIAsync>d__);
			return <ParsePIAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060013B3 RID: 5043 RVA: 0x000764F8 File Offset: 0x000746F8
		private Task ParseCondSectionAsync()
		{
			DtdParser.<ParseCondSectionAsync>d__172 <ParseCondSectionAsync>d__;
			<ParseCondSectionAsync>d__.<>4__this = this;
			<ParseCondSectionAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<ParseCondSectionAsync>d__.<>1__state = -1;
			<ParseCondSectionAsync>d__.<>t__builder.Start<DtdParser.<ParseCondSectionAsync>d__172>(ref <ParseCondSectionAsync>d__);
			return <ParseCondSectionAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060013B4 RID: 5044 RVA: 0x0007653C File Offset: 0x0007473C
		private Task<Tuple<string, string>> ParseExternalIdAsync(DtdParser.Token idTokenType, DtdParser.Token declType)
		{
			DtdParser.<ParseExternalIdAsync>d__173 <ParseExternalIdAsync>d__;
			<ParseExternalIdAsync>d__.<>4__this = this;
			<ParseExternalIdAsync>d__.idTokenType = idTokenType;
			<ParseExternalIdAsync>d__.declType = declType;
			<ParseExternalIdAsync>d__.<>t__builder = AsyncTaskMethodBuilder<Tuple<string, string>>.Create();
			<ParseExternalIdAsync>d__.<>1__state = -1;
			<ParseExternalIdAsync>d__.<>t__builder.Start<DtdParser.<ParseExternalIdAsync>d__173>(ref <ParseExternalIdAsync>d__);
			return <ParseExternalIdAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060013B5 RID: 5045 RVA: 0x00076590 File Offset: 0x00074790
		private Task<DtdParser.Token> GetTokenAsync(bool needWhiteSpace)
		{
			DtdParser.<GetTokenAsync>d__174 <GetTokenAsync>d__;
			<GetTokenAsync>d__.<>4__this = this;
			<GetTokenAsync>d__.needWhiteSpace = needWhiteSpace;
			<GetTokenAsync>d__.<>t__builder = AsyncTaskMethodBuilder<DtdParser.Token>.Create();
			<GetTokenAsync>d__.<>1__state = -1;
			<GetTokenAsync>d__.<>t__builder.Start<DtdParser.<GetTokenAsync>d__174>(ref <GetTokenAsync>d__);
			return <GetTokenAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060013B6 RID: 5046 RVA: 0x000765DC File Offset: 0x000747DC
		private Task<DtdParser.Token> ScanSubsetContentAsync()
		{
			DtdParser.<ScanSubsetContentAsync>d__175 <ScanSubsetContentAsync>d__;
			<ScanSubsetContentAsync>d__.<>4__this = this;
			<ScanSubsetContentAsync>d__.<>t__builder = AsyncTaskMethodBuilder<DtdParser.Token>.Create();
			<ScanSubsetContentAsync>d__.<>1__state = -1;
			<ScanSubsetContentAsync>d__.<>t__builder.Start<DtdParser.<ScanSubsetContentAsync>d__175>(ref <ScanSubsetContentAsync>d__);
			return <ScanSubsetContentAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060013B7 RID: 5047 RVA: 0x00076620 File Offset: 0x00074820
		private Task<DtdParser.Token> ScanNameExpectedAsync()
		{
			DtdParser.<ScanNameExpectedAsync>d__176 <ScanNameExpectedAsync>d__;
			<ScanNameExpectedAsync>d__.<>4__this = this;
			<ScanNameExpectedAsync>d__.<>t__builder = AsyncTaskMethodBuilder<DtdParser.Token>.Create();
			<ScanNameExpectedAsync>d__.<>1__state = -1;
			<ScanNameExpectedAsync>d__.<>t__builder.Start<DtdParser.<ScanNameExpectedAsync>d__176>(ref <ScanNameExpectedAsync>d__);
			return <ScanNameExpectedAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060013B8 RID: 5048 RVA: 0x00076664 File Offset: 0x00074864
		private Task<DtdParser.Token> ScanQNameExpectedAsync()
		{
			DtdParser.<ScanQNameExpectedAsync>d__177 <ScanQNameExpectedAsync>d__;
			<ScanQNameExpectedAsync>d__.<>4__this = this;
			<ScanQNameExpectedAsync>d__.<>t__builder = AsyncTaskMethodBuilder<DtdParser.Token>.Create();
			<ScanQNameExpectedAsync>d__.<>1__state = -1;
			<ScanQNameExpectedAsync>d__.<>t__builder.Start<DtdParser.<ScanQNameExpectedAsync>d__177>(ref <ScanQNameExpectedAsync>d__);
			return <ScanQNameExpectedAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060013B9 RID: 5049 RVA: 0x000766A8 File Offset: 0x000748A8
		private Task<DtdParser.Token> ScanNmtokenExpectedAsync()
		{
			DtdParser.<ScanNmtokenExpectedAsync>d__178 <ScanNmtokenExpectedAsync>d__;
			<ScanNmtokenExpectedAsync>d__.<>4__this = this;
			<ScanNmtokenExpectedAsync>d__.<>t__builder = AsyncTaskMethodBuilder<DtdParser.Token>.Create();
			<ScanNmtokenExpectedAsync>d__.<>1__state = -1;
			<ScanNmtokenExpectedAsync>d__.<>t__builder.Start<DtdParser.<ScanNmtokenExpectedAsync>d__178>(ref <ScanNmtokenExpectedAsync>d__);
			return <ScanNmtokenExpectedAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060013BA RID: 5050 RVA: 0x000766EC File Offset: 0x000748EC
		private Task<DtdParser.Token> ScanDoctype1Async()
		{
			DtdParser.<ScanDoctype1Async>d__179 <ScanDoctype1Async>d__;
			<ScanDoctype1Async>d__.<>4__this = this;
			<ScanDoctype1Async>d__.<>t__builder = AsyncTaskMethodBuilder<DtdParser.Token>.Create();
			<ScanDoctype1Async>d__.<>1__state = -1;
			<ScanDoctype1Async>d__.<>t__builder.Start<DtdParser.<ScanDoctype1Async>d__179>(ref <ScanDoctype1Async>d__);
			return <ScanDoctype1Async>d__.<>t__builder.Task;
		}

		// Token: 0x060013BB RID: 5051 RVA: 0x00076730 File Offset: 0x00074930
		private Task<DtdParser.Token> ScanElement1Async()
		{
			DtdParser.<ScanElement1Async>d__180 <ScanElement1Async>d__;
			<ScanElement1Async>d__.<>4__this = this;
			<ScanElement1Async>d__.<>t__builder = AsyncTaskMethodBuilder<DtdParser.Token>.Create();
			<ScanElement1Async>d__.<>1__state = -1;
			<ScanElement1Async>d__.<>t__builder.Start<DtdParser.<ScanElement1Async>d__180>(ref <ScanElement1Async>d__);
			return <ScanElement1Async>d__.<>t__builder.Task;
		}

		// Token: 0x060013BC RID: 5052 RVA: 0x00076774 File Offset: 0x00074974
		private Task<DtdParser.Token> ScanElement2Async()
		{
			DtdParser.<ScanElement2Async>d__181 <ScanElement2Async>d__;
			<ScanElement2Async>d__.<>4__this = this;
			<ScanElement2Async>d__.<>t__builder = AsyncTaskMethodBuilder<DtdParser.Token>.Create();
			<ScanElement2Async>d__.<>1__state = -1;
			<ScanElement2Async>d__.<>t__builder.Start<DtdParser.<ScanElement2Async>d__181>(ref <ScanElement2Async>d__);
			return <ScanElement2Async>d__.<>t__builder.Task;
		}

		// Token: 0x060013BD RID: 5053 RVA: 0x000767B8 File Offset: 0x000749B8
		private Task<DtdParser.Token> ScanElement3Async()
		{
			DtdParser.<ScanElement3Async>d__182 <ScanElement3Async>d__;
			<ScanElement3Async>d__.<>4__this = this;
			<ScanElement3Async>d__.<>t__builder = AsyncTaskMethodBuilder<DtdParser.Token>.Create();
			<ScanElement3Async>d__.<>1__state = -1;
			<ScanElement3Async>d__.<>t__builder.Start<DtdParser.<ScanElement3Async>d__182>(ref <ScanElement3Async>d__);
			return <ScanElement3Async>d__.<>t__builder.Task;
		}

		// Token: 0x060013BE RID: 5054 RVA: 0x000767FC File Offset: 0x000749FC
		private Task<DtdParser.Token> ScanAttlist1Async()
		{
			DtdParser.<ScanAttlist1Async>d__183 <ScanAttlist1Async>d__;
			<ScanAttlist1Async>d__.<>4__this = this;
			<ScanAttlist1Async>d__.<>t__builder = AsyncTaskMethodBuilder<DtdParser.Token>.Create();
			<ScanAttlist1Async>d__.<>1__state = -1;
			<ScanAttlist1Async>d__.<>t__builder.Start<DtdParser.<ScanAttlist1Async>d__183>(ref <ScanAttlist1Async>d__);
			return <ScanAttlist1Async>d__.<>t__builder.Task;
		}

		// Token: 0x060013BF RID: 5055 RVA: 0x00076840 File Offset: 0x00074A40
		private Task<DtdParser.Token> ScanAttlist2Async()
		{
			DtdParser.<ScanAttlist2Async>d__184 <ScanAttlist2Async>d__;
			<ScanAttlist2Async>d__.<>4__this = this;
			<ScanAttlist2Async>d__.<>t__builder = AsyncTaskMethodBuilder<DtdParser.Token>.Create();
			<ScanAttlist2Async>d__.<>1__state = -1;
			<ScanAttlist2Async>d__.<>t__builder.Start<DtdParser.<ScanAttlist2Async>d__184>(ref <ScanAttlist2Async>d__);
			return <ScanAttlist2Async>d__.<>t__builder.Task;
		}

		// Token: 0x060013C0 RID: 5056 RVA: 0x00076884 File Offset: 0x00074A84
		private Task<DtdParser.Token> ScanAttlist6Async()
		{
			DtdParser.<ScanAttlist6Async>d__185 <ScanAttlist6Async>d__;
			<ScanAttlist6Async>d__.<>4__this = this;
			<ScanAttlist6Async>d__.<>t__builder = AsyncTaskMethodBuilder<DtdParser.Token>.Create();
			<ScanAttlist6Async>d__.<>1__state = -1;
			<ScanAttlist6Async>d__.<>t__builder.Start<DtdParser.<ScanAttlist6Async>d__185>(ref <ScanAttlist6Async>d__);
			return <ScanAttlist6Async>d__.<>t__builder.Task;
		}

		// Token: 0x060013C1 RID: 5057 RVA: 0x000768C8 File Offset: 0x00074AC8
		private Task<DtdParser.Token> ScanLiteralAsync(DtdParser.LiteralType literalType)
		{
			DtdParser.<ScanLiteralAsync>d__186 <ScanLiteralAsync>d__;
			<ScanLiteralAsync>d__.<>4__this = this;
			<ScanLiteralAsync>d__.literalType = literalType;
			<ScanLiteralAsync>d__.<>t__builder = AsyncTaskMethodBuilder<DtdParser.Token>.Create();
			<ScanLiteralAsync>d__.<>1__state = -1;
			<ScanLiteralAsync>d__.<>t__builder.Start<DtdParser.<ScanLiteralAsync>d__186>(ref <ScanLiteralAsync>d__);
			return <ScanLiteralAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060013C2 RID: 5058 RVA: 0x00076914 File Offset: 0x00074B14
		private Task<DtdParser.Token> ScanNotation1Async()
		{
			DtdParser.<ScanNotation1Async>d__187 <ScanNotation1Async>d__;
			<ScanNotation1Async>d__.<>4__this = this;
			<ScanNotation1Async>d__.<>t__builder = AsyncTaskMethodBuilder<DtdParser.Token>.Create();
			<ScanNotation1Async>d__.<>1__state = -1;
			<ScanNotation1Async>d__.<>t__builder.Start<DtdParser.<ScanNotation1Async>d__187>(ref <ScanNotation1Async>d__);
			return <ScanNotation1Async>d__.<>t__builder.Task;
		}

		// Token: 0x060013C3 RID: 5059 RVA: 0x00076958 File Offset: 0x00074B58
		private Task<DtdParser.Token> ScanSystemIdAsync()
		{
			DtdParser.<ScanSystemIdAsync>d__188 <ScanSystemIdAsync>d__;
			<ScanSystemIdAsync>d__.<>4__this = this;
			<ScanSystemIdAsync>d__.<>t__builder = AsyncTaskMethodBuilder<DtdParser.Token>.Create();
			<ScanSystemIdAsync>d__.<>1__state = -1;
			<ScanSystemIdAsync>d__.<>t__builder.Start<DtdParser.<ScanSystemIdAsync>d__188>(ref <ScanSystemIdAsync>d__);
			return <ScanSystemIdAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060013C4 RID: 5060 RVA: 0x0007699C File Offset: 0x00074B9C
		private Task<DtdParser.Token> ScanEntity1Async()
		{
			DtdParser.<ScanEntity1Async>d__189 <ScanEntity1Async>d__;
			<ScanEntity1Async>d__.<>4__this = this;
			<ScanEntity1Async>d__.<>t__builder = AsyncTaskMethodBuilder<DtdParser.Token>.Create();
			<ScanEntity1Async>d__.<>1__state = -1;
			<ScanEntity1Async>d__.<>t__builder.Start<DtdParser.<ScanEntity1Async>d__189>(ref <ScanEntity1Async>d__);
			return <ScanEntity1Async>d__.<>t__builder.Task;
		}

		// Token: 0x060013C5 RID: 5061 RVA: 0x000769E0 File Offset: 0x00074BE0
		private Task<DtdParser.Token> ScanEntity2Async()
		{
			DtdParser.<ScanEntity2Async>d__190 <ScanEntity2Async>d__;
			<ScanEntity2Async>d__.<>4__this = this;
			<ScanEntity2Async>d__.<>t__builder = AsyncTaskMethodBuilder<DtdParser.Token>.Create();
			<ScanEntity2Async>d__.<>1__state = -1;
			<ScanEntity2Async>d__.<>t__builder.Start<DtdParser.<ScanEntity2Async>d__190>(ref <ScanEntity2Async>d__);
			return <ScanEntity2Async>d__.<>t__builder.Task;
		}

		// Token: 0x060013C6 RID: 5062 RVA: 0x00076A24 File Offset: 0x00074C24
		private Task<DtdParser.Token> ScanEntity3Async()
		{
			DtdParser.<ScanEntity3Async>d__191 <ScanEntity3Async>d__;
			<ScanEntity3Async>d__.<>4__this = this;
			<ScanEntity3Async>d__.<>t__builder = AsyncTaskMethodBuilder<DtdParser.Token>.Create();
			<ScanEntity3Async>d__.<>1__state = -1;
			<ScanEntity3Async>d__.<>t__builder.Start<DtdParser.<ScanEntity3Async>d__191>(ref <ScanEntity3Async>d__);
			return <ScanEntity3Async>d__.<>t__builder.Task;
		}

		// Token: 0x060013C7 RID: 5063 RVA: 0x00076A68 File Offset: 0x00074C68
		private Task<DtdParser.Token> ScanPublicId1Async()
		{
			DtdParser.<ScanPublicId1Async>d__192 <ScanPublicId1Async>d__;
			<ScanPublicId1Async>d__.<>4__this = this;
			<ScanPublicId1Async>d__.<>t__builder = AsyncTaskMethodBuilder<DtdParser.Token>.Create();
			<ScanPublicId1Async>d__.<>1__state = -1;
			<ScanPublicId1Async>d__.<>t__builder.Start<DtdParser.<ScanPublicId1Async>d__192>(ref <ScanPublicId1Async>d__);
			return <ScanPublicId1Async>d__.<>t__builder.Task;
		}

		// Token: 0x060013C8 RID: 5064 RVA: 0x00076AAC File Offset: 0x00074CAC
		private Task<DtdParser.Token> ScanPublicId2Async()
		{
			DtdParser.<ScanPublicId2Async>d__193 <ScanPublicId2Async>d__;
			<ScanPublicId2Async>d__.<>4__this = this;
			<ScanPublicId2Async>d__.<>t__builder = AsyncTaskMethodBuilder<DtdParser.Token>.Create();
			<ScanPublicId2Async>d__.<>1__state = -1;
			<ScanPublicId2Async>d__.<>t__builder.Start<DtdParser.<ScanPublicId2Async>d__193>(ref <ScanPublicId2Async>d__);
			return <ScanPublicId2Async>d__.<>t__builder.Task;
		}

		// Token: 0x060013C9 RID: 5065 RVA: 0x00076AF0 File Offset: 0x00074CF0
		private Task<DtdParser.Token> ScanCondSection1Async()
		{
			DtdParser.<ScanCondSection1Async>d__194 <ScanCondSection1Async>d__;
			<ScanCondSection1Async>d__.<>4__this = this;
			<ScanCondSection1Async>d__.<>t__builder = AsyncTaskMethodBuilder<DtdParser.Token>.Create();
			<ScanCondSection1Async>d__.<>1__state = -1;
			<ScanCondSection1Async>d__.<>t__builder.Start<DtdParser.<ScanCondSection1Async>d__194>(ref <ScanCondSection1Async>d__);
			return <ScanCondSection1Async>d__.<>t__builder.Task;
		}

		// Token: 0x060013CA RID: 5066 RVA: 0x00076B34 File Offset: 0x00074D34
		private Task<DtdParser.Token> ScanCondSection3Async()
		{
			DtdParser.<ScanCondSection3Async>d__195 <ScanCondSection3Async>d__;
			<ScanCondSection3Async>d__.<>4__this = this;
			<ScanCondSection3Async>d__.<>t__builder = AsyncTaskMethodBuilder<DtdParser.Token>.Create();
			<ScanCondSection3Async>d__.<>1__state = -1;
			<ScanCondSection3Async>d__.<>t__builder.Start<DtdParser.<ScanCondSection3Async>d__195>(ref <ScanCondSection3Async>d__);
			return <ScanCondSection3Async>d__.<>t__builder.Task;
		}

		// Token: 0x060013CB RID: 5067 RVA: 0x00076B77 File Offset: 0x00074D77
		private Task ScanNameAsync()
		{
			return this.ScanQNameAsync(false);
		}

		// Token: 0x060013CC RID: 5068 RVA: 0x00076B80 File Offset: 0x00074D80
		private Task ScanQNameAsync()
		{
			return this.ScanQNameAsync(this.SupportNamespaces);
		}

		// Token: 0x060013CD RID: 5069 RVA: 0x00076B90 File Offset: 0x00074D90
		private Task ScanQNameAsync(bool isQName)
		{
			DtdParser.<ScanQNameAsync>d__198 <ScanQNameAsync>d__;
			<ScanQNameAsync>d__.<>4__this = this;
			<ScanQNameAsync>d__.isQName = isQName;
			<ScanQNameAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<ScanQNameAsync>d__.<>1__state = -1;
			<ScanQNameAsync>d__.<>t__builder.Start<DtdParser.<ScanQNameAsync>d__198>(ref <ScanQNameAsync>d__);
			return <ScanQNameAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060013CE RID: 5070 RVA: 0x00076BDC File Offset: 0x00074DDC
		private Task<bool> ReadDataInNameAsync()
		{
			DtdParser.<ReadDataInNameAsync>d__199 <ReadDataInNameAsync>d__;
			<ReadDataInNameAsync>d__.<>4__this = this;
			<ReadDataInNameAsync>d__.<>t__builder = AsyncTaskMethodBuilder<bool>.Create();
			<ReadDataInNameAsync>d__.<>1__state = -1;
			<ReadDataInNameAsync>d__.<>t__builder.Start<DtdParser.<ReadDataInNameAsync>d__199>(ref <ReadDataInNameAsync>d__);
			return <ReadDataInNameAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060013CF RID: 5071 RVA: 0x00076C20 File Offset: 0x00074E20
		private Task ScanNmtokenAsync()
		{
			DtdParser.<ScanNmtokenAsync>d__200 <ScanNmtokenAsync>d__;
			<ScanNmtokenAsync>d__.<>4__this = this;
			<ScanNmtokenAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<ScanNmtokenAsync>d__.<>1__state = -1;
			<ScanNmtokenAsync>d__.<>t__builder.Start<DtdParser.<ScanNmtokenAsync>d__200>(ref <ScanNmtokenAsync>d__);
			return <ScanNmtokenAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060013D0 RID: 5072 RVA: 0x00076C64 File Offset: 0x00074E64
		private Task<bool> EatPublicKeywordAsync()
		{
			DtdParser.<EatPublicKeywordAsync>d__201 <EatPublicKeywordAsync>d__;
			<EatPublicKeywordAsync>d__.<>4__this = this;
			<EatPublicKeywordAsync>d__.<>t__builder = AsyncTaskMethodBuilder<bool>.Create();
			<EatPublicKeywordAsync>d__.<>1__state = -1;
			<EatPublicKeywordAsync>d__.<>t__builder.Start<DtdParser.<EatPublicKeywordAsync>d__201>(ref <EatPublicKeywordAsync>d__);
			return <EatPublicKeywordAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060013D1 RID: 5073 RVA: 0x00076CA8 File Offset: 0x00074EA8
		private Task<bool> EatSystemKeywordAsync()
		{
			DtdParser.<EatSystemKeywordAsync>d__202 <EatSystemKeywordAsync>d__;
			<EatSystemKeywordAsync>d__.<>4__this = this;
			<EatSystemKeywordAsync>d__.<>t__builder = AsyncTaskMethodBuilder<bool>.Create();
			<EatSystemKeywordAsync>d__.<>1__state = -1;
			<EatSystemKeywordAsync>d__.<>t__builder.Start<DtdParser.<EatSystemKeywordAsync>d__202>(ref <EatSystemKeywordAsync>d__);
			return <EatSystemKeywordAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060013D2 RID: 5074 RVA: 0x00076CEC File Offset: 0x00074EEC
		private Task<int> ReadDataAsync()
		{
			DtdParser.<ReadDataAsync>d__203 <ReadDataAsync>d__;
			<ReadDataAsync>d__.<>4__this = this;
			<ReadDataAsync>d__.<>t__builder = AsyncTaskMethodBuilder<int>.Create();
			<ReadDataAsync>d__.<>1__state = -1;
			<ReadDataAsync>d__.<>t__builder.Start<DtdParser.<ReadDataAsync>d__203>(ref <ReadDataAsync>d__);
			return <ReadDataAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060013D3 RID: 5075 RVA: 0x00076D2F File Offset: 0x00074F2F
		private Task<bool> HandleEntityReferenceAsync(bool paramEntity, bool inLiteral, bool inAttribute)
		{
			this.curPos++;
			return this.HandleEntityReferenceAsync(this.ScanEntityName(), paramEntity, inLiteral, inAttribute);
		}

		// Token: 0x060013D4 RID: 5076 RVA: 0x00076D50 File Offset: 0x00074F50
		private Task<bool> HandleEntityReferenceAsync(XmlQualifiedName entityName, bool paramEntity, bool inLiteral, bool inAttribute)
		{
			DtdParser.<HandleEntityReferenceAsync>d__205 <HandleEntityReferenceAsync>d__;
			<HandleEntityReferenceAsync>d__.<>4__this = this;
			<HandleEntityReferenceAsync>d__.entityName = entityName;
			<HandleEntityReferenceAsync>d__.paramEntity = paramEntity;
			<HandleEntityReferenceAsync>d__.inLiteral = inLiteral;
			<HandleEntityReferenceAsync>d__.inAttribute = inAttribute;
			<HandleEntityReferenceAsync>d__.<>t__builder = AsyncTaskMethodBuilder<bool>.Create();
			<HandleEntityReferenceAsync>d__.<>1__state = -1;
			<HandleEntityReferenceAsync>d__.<>t__builder.Start<DtdParser.<HandleEntityReferenceAsync>d__205>(ref <HandleEntityReferenceAsync>d__);
			return <HandleEntityReferenceAsync>d__.<>t__builder.Task;
		}

		// Token: 0x040010F5 RID: 4341
		private IDtdParserAdapter readerAdapter;

		// Token: 0x040010F6 RID: 4342
		private IDtdParserAdapterWithValidation readerAdapterWithValidation;

		// Token: 0x040010F7 RID: 4343
		private XmlNameTable nameTable;

		// Token: 0x040010F8 RID: 4344
		private SchemaInfo schemaInfo;

		// Token: 0x040010F9 RID: 4345
		private XmlCharType xmlCharType = XmlCharType.Instance;

		// Token: 0x040010FA RID: 4346
		private string systemId = string.Empty;

		// Token: 0x040010FB RID: 4347
		private string publicId = string.Empty;

		// Token: 0x040010FC RID: 4348
		private bool normalize = true;

		// Token: 0x040010FD RID: 4349
		private bool validate;

		// Token: 0x040010FE RID: 4350
		private bool supportNamespaces = true;

		// Token: 0x040010FF RID: 4351
		private bool v1Compat;

		// Token: 0x04001100 RID: 4352
		private char[] chars;

		// Token: 0x04001101 RID: 4353
		private int charsUsed;

		// Token: 0x04001102 RID: 4354
		private int curPos;

		// Token: 0x04001103 RID: 4355
		private DtdParser.ScanningFunction scanningFunction;

		// Token: 0x04001104 RID: 4356
		private DtdParser.ScanningFunction nextScaningFunction;

		// Token: 0x04001105 RID: 4357
		private DtdParser.ScanningFunction savedScanningFunction;

		// Token: 0x04001106 RID: 4358
		private bool whitespaceSeen;

		// Token: 0x04001107 RID: 4359
		private int tokenStartPos;

		// Token: 0x04001108 RID: 4360
		private int colonPos;

		// Token: 0x04001109 RID: 4361
		private StringBuilder internalSubsetValueSb;

		// Token: 0x0400110A RID: 4362
		private int externalEntitiesDepth;

		// Token: 0x0400110B RID: 4363
		private int currentEntityId;

		// Token: 0x0400110C RID: 4364
		private bool freeFloatingDtd;

		// Token: 0x0400110D RID: 4365
		private bool hasFreeFloatingInternalSubset;

		// Token: 0x0400110E RID: 4366
		private StringBuilder stringBuilder;

		// Token: 0x0400110F RID: 4367
		private int condSectionDepth;

		// Token: 0x04001110 RID: 4368
		private LineInfo literalLineInfo = new LineInfo(0, 0);

		// Token: 0x04001111 RID: 4369
		private char literalQuoteChar = '"';

		// Token: 0x04001112 RID: 4370
		private string documentBaseUri = string.Empty;

		// Token: 0x04001113 RID: 4371
		private string externalDtdBaseUri = string.Empty;

		// Token: 0x04001114 RID: 4372
		private Dictionary<string, DtdParser.UndeclaredNotation> undeclaredNotations;

		// Token: 0x04001115 RID: 4373
		private int[] condSectionEntityIds;

		// Token: 0x04001116 RID: 4374
		private const int CondSectionEntityIdsInitialSize = 2;

		// Token: 0x020001EB RID: 491
		private enum Token
		{
			// Token: 0x04001118 RID: 4376
			CDATA,
			// Token: 0x04001119 RID: 4377
			ID,
			// Token: 0x0400111A RID: 4378
			IDREF,
			// Token: 0x0400111B RID: 4379
			IDREFS,
			// Token: 0x0400111C RID: 4380
			ENTITY,
			// Token: 0x0400111D RID: 4381
			ENTITIES,
			// Token: 0x0400111E RID: 4382
			NMTOKEN,
			// Token: 0x0400111F RID: 4383
			NMTOKENS,
			// Token: 0x04001120 RID: 4384
			NOTATION,
			// Token: 0x04001121 RID: 4385
			None,
			// Token: 0x04001122 RID: 4386
			PERef,
			// Token: 0x04001123 RID: 4387
			AttlistDecl,
			// Token: 0x04001124 RID: 4388
			ElementDecl,
			// Token: 0x04001125 RID: 4389
			EntityDecl,
			// Token: 0x04001126 RID: 4390
			NotationDecl,
			// Token: 0x04001127 RID: 4391
			Comment,
			// Token: 0x04001128 RID: 4392
			PI,
			// Token: 0x04001129 RID: 4393
			CondSectionStart,
			// Token: 0x0400112A RID: 4394
			CondSectionEnd,
			// Token: 0x0400112B RID: 4395
			Eof,
			// Token: 0x0400112C RID: 4396
			REQUIRED,
			// Token: 0x0400112D RID: 4397
			IMPLIED,
			// Token: 0x0400112E RID: 4398
			FIXED,
			// Token: 0x0400112F RID: 4399
			QName,
			// Token: 0x04001130 RID: 4400
			Name,
			// Token: 0x04001131 RID: 4401
			Nmtoken,
			// Token: 0x04001132 RID: 4402
			Quote,
			// Token: 0x04001133 RID: 4403
			LeftParen,
			// Token: 0x04001134 RID: 4404
			RightParen,
			// Token: 0x04001135 RID: 4405
			GreaterThan,
			// Token: 0x04001136 RID: 4406
			Or,
			// Token: 0x04001137 RID: 4407
			LeftBracket,
			// Token: 0x04001138 RID: 4408
			RightBracket,
			// Token: 0x04001139 RID: 4409
			PUBLIC,
			// Token: 0x0400113A RID: 4410
			SYSTEM,
			// Token: 0x0400113B RID: 4411
			Literal,
			// Token: 0x0400113C RID: 4412
			DOCTYPE,
			// Token: 0x0400113D RID: 4413
			NData,
			// Token: 0x0400113E RID: 4414
			Percent,
			// Token: 0x0400113F RID: 4415
			Star,
			// Token: 0x04001140 RID: 4416
			QMark,
			// Token: 0x04001141 RID: 4417
			Plus,
			// Token: 0x04001142 RID: 4418
			PCDATA,
			// Token: 0x04001143 RID: 4419
			Comma,
			// Token: 0x04001144 RID: 4420
			ANY,
			// Token: 0x04001145 RID: 4421
			EMPTY,
			// Token: 0x04001146 RID: 4422
			IGNORE,
			// Token: 0x04001147 RID: 4423
			INCLUDE
		}

		// Token: 0x020001EC RID: 492
		private enum ScanningFunction
		{
			// Token: 0x04001149 RID: 4425
			SubsetContent,
			// Token: 0x0400114A RID: 4426
			Name,
			// Token: 0x0400114B RID: 4427
			QName,
			// Token: 0x0400114C RID: 4428
			Nmtoken,
			// Token: 0x0400114D RID: 4429
			Doctype1,
			// Token: 0x0400114E RID: 4430
			Doctype2,
			// Token: 0x0400114F RID: 4431
			Element1,
			// Token: 0x04001150 RID: 4432
			Element2,
			// Token: 0x04001151 RID: 4433
			Element3,
			// Token: 0x04001152 RID: 4434
			Element4,
			// Token: 0x04001153 RID: 4435
			Element5,
			// Token: 0x04001154 RID: 4436
			Element6,
			// Token: 0x04001155 RID: 4437
			Element7,
			// Token: 0x04001156 RID: 4438
			Attlist1,
			// Token: 0x04001157 RID: 4439
			Attlist2,
			// Token: 0x04001158 RID: 4440
			Attlist3,
			// Token: 0x04001159 RID: 4441
			Attlist4,
			// Token: 0x0400115A RID: 4442
			Attlist5,
			// Token: 0x0400115B RID: 4443
			Attlist6,
			// Token: 0x0400115C RID: 4444
			Attlist7,
			// Token: 0x0400115D RID: 4445
			Entity1,
			// Token: 0x0400115E RID: 4446
			Entity2,
			// Token: 0x0400115F RID: 4447
			Entity3,
			// Token: 0x04001160 RID: 4448
			Notation1,
			// Token: 0x04001161 RID: 4449
			CondSection1,
			// Token: 0x04001162 RID: 4450
			CondSection2,
			// Token: 0x04001163 RID: 4451
			CondSection3,
			// Token: 0x04001164 RID: 4452
			Literal,
			// Token: 0x04001165 RID: 4453
			SystemId,
			// Token: 0x04001166 RID: 4454
			PublicId1,
			// Token: 0x04001167 RID: 4455
			PublicId2,
			// Token: 0x04001168 RID: 4456
			ClosingTag,
			// Token: 0x04001169 RID: 4457
			ParamEntitySpace,
			// Token: 0x0400116A RID: 4458
			None
		}

		// Token: 0x020001ED RID: 493
		private enum LiteralType
		{
			// Token: 0x0400116C RID: 4460
			AttributeValue,
			// Token: 0x0400116D RID: 4461
			EntityReplText,
			// Token: 0x0400116E RID: 4462
			SystemOrPublicID
		}

		// Token: 0x020001EE RID: 494
		private class UndeclaredNotation
		{
			// Token: 0x060013D5 RID: 5077 RVA: 0x00076DB4 File Offset: 0x00074FB4
			internal UndeclaredNotation(string name, int lineNo, int linePos)
			{
				this.name = name;
				this.lineNo = lineNo;
				this.linePos = linePos;
				this.next = null;
			}

			// Token: 0x0400116F RID: 4463
			internal string name;

			// Token: 0x04001170 RID: 4464
			internal int lineNo;

			// Token: 0x04001171 RID: 4465
			internal int linePos;

			// Token: 0x04001172 RID: 4466
			internal DtdParser.UndeclaredNotation next;
		}

		// Token: 0x020001EF RID: 495
		private class ParseElementOnlyContent_LocalFrame
		{
			// Token: 0x060013D6 RID: 5078 RVA: 0x00076DD8 File Offset: 0x00074FD8
			public ParseElementOnlyContent_LocalFrame(int startParentEntityIdParam)
			{
				this.startParenEntityId = startParentEntityIdParam;
				this.parsingSchema = DtdParser.Token.None;
			}

			// Token: 0x04001173 RID: 4467
			public int startParenEntityId;

			// Token: 0x04001174 RID: 4468
			public DtdParser.Token parsingSchema;
		}

		// Token: 0x020001F0 RID: 496
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <System-Xml-IDtdParser-ParseInternalDtdAsync>d__153 : IAsyncStateMachine
		{
			// Token: 0x060013D7 RID: 5079 RVA: 0x00076DF0 File Offset: 0x00074FF0
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				DtdParser dtdParser = this.<>4__this;
				IDtdInfo schemaInfo;
				try
				{
					ConfiguredTaskAwaitable.ConfiguredTaskAwaiter awaiter;
					if (num != 0)
					{
						dtdParser.Initialize(this.adapter);
						awaiter = dtdParser.ParseAsync(this.saveInternalSubset).ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 0;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, DtdParser.<System-Xml-IDtdParser-ParseInternalDtdAsync>d__153>(ref awaiter, ref this);
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
					schemaInfo = dtdParser.schemaInfo;
				}
				catch (Exception exception)
				{
					this.<>1__state = -2;
					this.<>t__builder.SetException(exception);
					return;
				}
				this.<>1__state = -2;
				this.<>t__builder.SetResult(schemaInfo);
			}

			// Token: 0x060013D8 RID: 5080 RVA: 0x00076EC8 File Offset: 0x000750C8
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04001175 RID: 4469
			public int <>1__state;

			// Token: 0x04001176 RID: 4470
			public AsyncTaskMethodBuilder<IDtdInfo> <>t__builder;

			// Token: 0x04001177 RID: 4471
			public DtdParser <>4__this;

			// Token: 0x04001178 RID: 4472
			public IDtdParserAdapter adapter;

			// Token: 0x04001179 RID: 4473
			public bool saveInternalSubset;

			// Token: 0x0400117A RID: 4474
			private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter <>u__1;
		}

		// Token: 0x020001F1 RID: 497
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <System-Xml-IDtdParser-ParseFreeFloatingDtdAsync>d__154 : IAsyncStateMachine
		{
			// Token: 0x060013D9 RID: 5081 RVA: 0x00076ED8 File Offset: 0x000750D8
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				DtdParser dtdParser = this.<>4__this;
				IDtdInfo schemaInfo;
				try
				{
					ConfiguredTaskAwaitable.ConfiguredTaskAwaiter awaiter;
					if (num != 0)
					{
						dtdParser.InitializeFreeFloatingDtd(this.baseUri, this.docTypeName, this.publicId, this.systemId, this.internalSubset, this.adapter);
						awaiter = dtdParser.ParseAsync(false).ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 0;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, DtdParser.<System-Xml-IDtdParser-ParseFreeFloatingDtdAsync>d__154>(ref awaiter, ref this);
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
					schemaInfo = dtdParser.schemaInfo;
				}
				catch (Exception exception)
				{
					this.<>1__state = -2;
					this.<>t__builder.SetException(exception);
					return;
				}
				this.<>1__state = -2;
				this.<>t__builder.SetResult(schemaInfo);
			}

			// Token: 0x060013DA RID: 5082 RVA: 0x00076FCC File Offset: 0x000751CC
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x0400117B RID: 4475
			public int <>1__state;

			// Token: 0x0400117C RID: 4476
			public AsyncTaskMethodBuilder<IDtdInfo> <>t__builder;

			// Token: 0x0400117D RID: 4477
			public DtdParser <>4__this;

			// Token: 0x0400117E RID: 4478
			public string baseUri;

			// Token: 0x0400117F RID: 4479
			public string docTypeName;

			// Token: 0x04001180 RID: 4480
			public string publicId;

			// Token: 0x04001181 RID: 4481
			public string systemId;

			// Token: 0x04001182 RID: 4482
			public string internalSubset;

			// Token: 0x04001183 RID: 4483
			public IDtdParserAdapter adapter;

			// Token: 0x04001184 RID: 4484
			private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter <>u__1;
		}

		// Token: 0x020001F2 RID: 498
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <ParseAsync>d__155 : IAsyncStateMachine
		{
			// Token: 0x060013DB RID: 5083 RVA: 0x00076FDC File Offset: 0x000751DC
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				DtdParser dtdParser = this.<>4__this;
				try
				{
					ConfiguredTaskAwaitable.ConfiguredTaskAwaiter awaiter;
					if (num != 0)
					{
						if (num != 1)
						{
							if (dtdParser.freeFloatingDtd)
							{
								awaiter = dtdParser.ParseFreeFloatingDtdAsync().ConfigureAwait(false).GetAwaiter();
								if (!awaiter.IsCompleted)
								{
									num = (this.<>1__state = 0);
									this.<>u__1 = awaiter;
									this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, DtdParser.<ParseAsync>d__155>(ref awaiter, ref this);
									return;
								}
								goto IL_7D;
							}
							else
							{
								awaiter = dtdParser.ParseInDocumentDtdAsync(this.saveInternalSubset).ConfigureAwait(false).GetAwaiter();
								if (!awaiter.IsCompleted)
								{
									num = (this.<>1__state = 1);
									this.<>u__1 = awaiter;
									this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, DtdParser.<ParseAsync>d__155>(ref awaiter, ref this);
									return;
								}
							}
						}
						else
						{
							awaiter = this.<>u__1;
							this.<>u__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
							num = (this.<>1__state = -1);
						}
						awaiter.GetResult();
						goto IL_F0;
					}
					awaiter = this.<>u__1;
					this.<>u__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
					num = (this.<>1__state = -1);
					IL_7D:
					awaiter.GetResult();
					IL_F0:
					dtdParser.schemaInfo.Finish();
					if (dtdParser.validate && dtdParser.undeclaredNotations != null)
					{
						Dictionary<string, DtdParser.UndeclaredNotation>.ValueCollection.Enumerator enumerator = dtdParser.undeclaredNotations.Values.GetEnumerator();
						try
						{
							while (enumerator.MoveNext())
							{
								DtdParser.UndeclaredNotation undeclaredNotation = enumerator.Current;
								for (DtdParser.UndeclaredNotation undeclaredNotation2 = undeclaredNotation; undeclaredNotation2 != null; undeclaredNotation2 = undeclaredNotation2.next)
								{
									dtdParser.SendValidationEvent(XmlSeverityType.Error, new XmlSchemaException("The '{0}' notation is not declared.", undeclaredNotation.name, dtdParser.BaseUriStr, undeclaredNotation.lineNo, undeclaredNotation.linePos));
								}
							}
						}
						finally
						{
							if (num < 0)
							{
								((IDisposable)enumerator).Dispose();
							}
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

			// Token: 0x060013DC RID: 5084 RVA: 0x000771D4 File Offset: 0x000753D4
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04001185 RID: 4485
			public int <>1__state;

			// Token: 0x04001186 RID: 4486
			public AsyncTaskMethodBuilder <>t__builder;

			// Token: 0x04001187 RID: 4487
			public DtdParser <>4__this;

			// Token: 0x04001188 RID: 4488
			public bool saveInternalSubset;

			// Token: 0x04001189 RID: 4489
			private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter <>u__1;
		}

		// Token: 0x020001F3 RID: 499
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <ParseInDocumentDtdAsync>d__156 : IAsyncStateMachine
		{
			// Token: 0x060013DD RID: 5085 RVA: 0x000771E4 File Offset: 0x000753E4
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				DtdParser dtdParser = this.<>4__this;
				try
				{
					ConfiguredTaskAwaitable<DtdParser.Token>.ConfiguredTaskAwaiter awaiter;
					ConfiguredTaskAwaitable<Tuple<string, string>>.ConfiguredTaskAwaiter awaiter2;
					ConfiguredTaskAwaitable.ConfiguredTaskAwaiter awaiter3;
					switch (num)
					{
					case 0:
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable<DtdParser.Token>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						break;
					case 1:
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable<DtdParser.Token>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_121;
					case 2:
						awaiter2 = this.<>u__2;
						this.<>u__2 = default(ConfiguredTaskAwaitable<Tuple<string, string>>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_19A;
					case 3:
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable<DtdParser.Token>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_21C;
					case 4:
						awaiter3 = this.<>u__3;
						this.<>u__3 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_2AE;
					case 5:
						awaiter3 = this.<>u__3;
						this.<>u__3 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_337;
					default:
						dtdParser.LoadParsingBuffer();
						dtdParser.scanningFunction = DtdParser.ScanningFunction.QName;
						dtdParser.nextScaningFunction = DtdParser.ScanningFunction.Doctype1;
						awaiter = dtdParser.GetTokenAsync(false).ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 0;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<DtdParser.Token>.ConfiguredTaskAwaiter, DtdParser.<ParseInDocumentDtdAsync>d__156>(ref awaiter, ref this);
							return;
						}
						break;
					}
					if (awaiter.GetResult() != DtdParser.Token.QName)
					{
						dtdParser.OnUnexpectedError();
					}
					dtdParser.schemaInfo.DocTypeName = dtdParser.GetNameQualified(true);
					awaiter = dtdParser.GetTokenAsync(false).ConfigureAwait(false).GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						this.<>1__state = 1;
						this.<>u__1 = awaiter;
						this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<DtdParser.Token>.ConfiguredTaskAwaiter, DtdParser.<ParseInDocumentDtdAsync>d__156>(ref awaiter, ref this);
						return;
					}
					IL_121:
					DtdParser.Token result = awaiter.GetResult();
					if (result != DtdParser.Token.SYSTEM && result != DtdParser.Token.PUBLIC)
					{
						goto IL_224;
					}
					awaiter2 = dtdParser.ParseExternalIdAsync(result, DtdParser.Token.DOCTYPE).ConfigureAwait(false).GetAwaiter();
					if (!awaiter2.IsCompleted)
					{
						this.<>1__state = 2;
						this.<>u__2 = awaiter2;
						this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<Tuple<string, string>>.ConfiguredTaskAwaiter, DtdParser.<ParseInDocumentDtdAsync>d__156>(ref awaiter2, ref this);
						return;
					}
					IL_19A:
					Tuple<string, string> result2 = awaiter2.GetResult();
					dtdParser.publicId = result2.Item1;
					dtdParser.systemId = result2.Item2;
					awaiter = dtdParser.GetTokenAsync(false).ConfigureAwait(false).GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						this.<>1__state = 3;
						this.<>u__1 = awaiter;
						this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<DtdParser.Token>.ConfiguredTaskAwaiter, DtdParser.<ParseInDocumentDtdAsync>d__156>(ref awaiter, ref this);
						return;
					}
					IL_21C:
					result = awaiter.GetResult();
					IL_224:
					if (result == DtdParser.Token.GreaterThan)
					{
						goto IL_2BD;
					}
					if (result != DtdParser.Token.LeftBracket)
					{
						dtdParser.OnUnexpectedError();
						goto IL_2BD;
					}
					if (this.saveInternalSubset)
					{
						dtdParser.SaveParsingBuffer();
						dtdParser.internalSubsetValueSb = new StringBuilder();
					}
					awaiter3 = dtdParser.ParseInternalSubsetAsync().ConfigureAwait(false).GetAwaiter();
					if (!awaiter3.IsCompleted)
					{
						this.<>1__state = 4;
						this.<>u__3 = awaiter3;
						this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, DtdParser.<ParseInDocumentDtdAsync>d__156>(ref awaiter3, ref this);
						return;
					}
					IL_2AE:
					awaiter3.GetResult();
					IL_2BD:
					dtdParser.SaveParsingBuffer();
					if (dtdParser.systemId == null || dtdParser.systemId.Length <= 0)
					{
						goto IL_33E;
					}
					awaiter3 = dtdParser.ParseExternalSubsetAsync().ConfigureAwait(false).GetAwaiter();
					if (!awaiter3.IsCompleted)
					{
						this.<>1__state = 5;
						this.<>u__3 = awaiter3;
						this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, DtdParser.<ParseInDocumentDtdAsync>d__156>(ref awaiter3, ref this);
						return;
					}
					IL_337:
					awaiter3.GetResult();
					IL_33E:;
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

			// Token: 0x060013DE RID: 5086 RVA: 0x0007757C File Offset: 0x0007577C
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x0400118A RID: 4490
			public int <>1__state;

			// Token: 0x0400118B RID: 4491
			public AsyncTaskMethodBuilder <>t__builder;

			// Token: 0x0400118C RID: 4492
			public DtdParser <>4__this;

			// Token: 0x0400118D RID: 4493
			public bool saveInternalSubset;

			// Token: 0x0400118E RID: 4494
			private ConfiguredTaskAwaitable<DtdParser.Token>.ConfiguredTaskAwaiter <>u__1;

			// Token: 0x0400118F RID: 4495
			private ConfiguredTaskAwaitable<Tuple<string, string>>.ConfiguredTaskAwaiter <>u__2;

			// Token: 0x04001190 RID: 4496
			private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter <>u__3;
		}

		// Token: 0x020001F4 RID: 500
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <ParseFreeFloatingDtdAsync>d__157 : IAsyncStateMachine
		{
			// Token: 0x060013DF RID: 5087 RVA: 0x0007758C File Offset: 0x0007578C
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				DtdParser dtdParser = this.<>4__this;
				try
				{
					ConfiguredTaskAwaitable.ConfiguredTaskAwaiter awaiter;
					if (num != 0)
					{
						if (num == 1)
						{
							awaiter = this.<>u__1;
							this.<>u__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
							this.<>1__state = -1;
							goto IL_100;
						}
						if (!dtdParser.hasFreeFloatingInternalSubset)
						{
							goto IL_90;
						}
						dtdParser.LoadParsingBuffer();
						awaiter = dtdParser.ParseInternalSubsetAsync().ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 0;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, DtdParser.<ParseFreeFloatingDtdAsync>d__157>(ref awaiter, ref this);
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
					dtdParser.SaveParsingBuffer();
					IL_90:
					if (dtdParser.systemId == null || dtdParser.systemId.Length <= 0)
					{
						goto IL_107;
					}
					awaiter = dtdParser.ParseExternalSubsetAsync().ConfigureAwait(false).GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						this.<>1__state = 1;
						this.<>u__1 = awaiter;
						this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, DtdParser.<ParseFreeFloatingDtdAsync>d__157>(ref awaiter, ref this);
						return;
					}
					IL_100:
					awaiter.GetResult();
					IL_107:;
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

			// Token: 0x060013E0 RID: 5088 RVA: 0x000776E0 File Offset: 0x000758E0
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04001191 RID: 4497
			public int <>1__state;

			// Token: 0x04001192 RID: 4498
			public AsyncTaskMethodBuilder <>t__builder;

			// Token: 0x04001193 RID: 4499
			public DtdParser <>4__this;

			// Token: 0x04001194 RID: 4500
			private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter <>u__1;
		}

		// Token: 0x020001F5 RID: 501
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <ParseExternalSubsetAsync>d__159 : IAsyncStateMachine
		{
			// Token: 0x060013E1 RID: 5089 RVA: 0x000776F0 File Offset: 0x000758F0
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				DtdParser dtdParser = this.<>4__this;
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
							goto IL_128;
						}
						awaiter2 = dtdParser.readerAdapter.PushExternalSubsetAsync(dtdParser.systemId, dtdParser.publicId).ConfigureAwait(false).GetAwaiter();
						if (!awaiter2.IsCompleted)
						{
							this.<>1__state = 0;
							this.<>u__1 = awaiter2;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter, DtdParser.<ParseExternalSubsetAsync>d__159>(ref awaiter2, ref this);
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
						goto IL_14A;
					}
					Uri baseUri = dtdParser.readerAdapter.BaseUri;
					if (baseUri != null)
					{
						dtdParser.externalDtdBaseUri = baseUri.ToString();
					}
					dtdParser.externalEntitiesDepth++;
					dtdParser.LoadParsingBuffer();
					awaiter = dtdParser.ParseSubsetAsync().ConfigureAwait(false).GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						this.<>1__state = 1;
						this.<>u__2 = awaiter;
						this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, DtdParser.<ParseExternalSubsetAsync>d__159>(ref awaiter, ref this);
						return;
					}
					IL_128:
					awaiter.GetResult();
				}
				catch (Exception exception)
				{
					this.<>1__state = -2;
					this.<>t__builder.SetException(exception);
					return;
				}
				IL_14A:
				this.<>1__state = -2;
				this.<>t__builder.SetResult();
			}

			// Token: 0x060013E2 RID: 5090 RVA: 0x00077878 File Offset: 0x00075A78
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04001195 RID: 4501
			public int <>1__state;

			// Token: 0x04001196 RID: 4502
			public AsyncTaskMethodBuilder <>t__builder;

			// Token: 0x04001197 RID: 4503
			public DtdParser <>4__this;

			// Token: 0x04001198 RID: 4504
			private ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter <>u__1;

			// Token: 0x04001199 RID: 4505
			private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter <>u__2;
		}

		// Token: 0x020001F6 RID: 502
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <ParseSubsetAsync>d__160 : IAsyncStateMachine
		{
			// Token: 0x060013E3 RID: 5091 RVA: 0x00077888 File Offset: 0x00075A88
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				DtdParser dtdParser = this.<>4__this;
				try
				{
					ConfiguredTaskAwaitable<DtdParser.Token>.ConfiguredTaskAwaiter awaiter;
					ConfiguredTaskAwaitable.ConfiguredTaskAwaiter awaiter2;
					switch (num)
					{
					case 0:
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable<DtdParser.Token>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						break;
					case 1:
						awaiter2 = this.<>u__2;
						this.<>u__2 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_146;
					case 2:
						awaiter2 = this.<>u__2;
						this.<>u__2 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_1B3;
					case 3:
						awaiter2 = this.<>u__2;
						this.<>u__2 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_220;
					case 4:
						awaiter2 = this.<>u__2;
						this.<>u__2 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_28D;
					case 5:
						awaiter2 = this.<>u__2;
						this.<>u__2 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_2FA;
					case 6:
						awaiter2 = this.<>u__2;
						this.<>u__2 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_367;
					case 7:
						awaiter2 = this.<>u__2;
						this.<>u__2 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_3EF;
					case 8:
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable<DtdParser.Token>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_52B;
					default:
						IL_38:
						awaiter = dtdParser.GetTokenAsync(false).ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 0;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<DtdParser.Token>.ConfiguredTaskAwaiter, DtdParser.<ParseSubsetAsync>d__160>(ref awaiter, ref this);
							return;
						}
						break;
					}
					DtdParser.Token result = awaiter.GetResult();
					this.<startTagEntityId>5__2 = dtdParser.currentEntityId;
					switch (result)
					{
					case DtdParser.Token.AttlistDecl:
						awaiter2 = dtdParser.ParseAttlistDeclAsync().ConfigureAwait(false).GetAwaiter();
						if (!awaiter2.IsCompleted)
						{
							this.<>1__state = 1;
							this.<>u__2 = awaiter2;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, DtdParser.<ParseSubsetAsync>d__160>(ref awaiter2, ref this);
							return;
						}
						break;
					case DtdParser.Token.ElementDecl:
						awaiter2 = dtdParser.ParseElementDeclAsync().ConfigureAwait(false).GetAwaiter();
						if (!awaiter2.IsCompleted)
						{
							this.<>1__state = 2;
							this.<>u__2 = awaiter2;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, DtdParser.<ParseSubsetAsync>d__160>(ref awaiter2, ref this);
							return;
						}
						goto IL_1B3;
					case DtdParser.Token.EntityDecl:
						awaiter2 = dtdParser.ParseEntityDeclAsync().ConfigureAwait(false).GetAwaiter();
						if (!awaiter2.IsCompleted)
						{
							this.<>1__state = 3;
							this.<>u__2 = awaiter2;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, DtdParser.<ParseSubsetAsync>d__160>(ref awaiter2, ref this);
							return;
						}
						goto IL_220;
					case DtdParser.Token.NotationDecl:
						awaiter2 = dtdParser.ParseNotationDeclAsync().ConfigureAwait(false).GetAwaiter();
						if (!awaiter2.IsCompleted)
						{
							this.<>1__state = 4;
							this.<>u__2 = awaiter2;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, DtdParser.<ParseSubsetAsync>d__160>(ref awaiter2, ref this);
							return;
						}
						goto IL_28D;
					case DtdParser.Token.Comment:
						awaiter2 = dtdParser.ParseCommentAsync().ConfigureAwait(false).GetAwaiter();
						if (!awaiter2.IsCompleted)
						{
							this.<>1__state = 5;
							this.<>u__2 = awaiter2;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, DtdParser.<ParseSubsetAsync>d__160>(ref awaiter2, ref this);
							return;
						}
						goto IL_2FA;
					case DtdParser.Token.PI:
						awaiter2 = dtdParser.ParsePIAsync().ConfigureAwait(false).GetAwaiter();
						if (!awaiter2.IsCompleted)
						{
							this.<>1__state = 6;
							this.<>u__2 = awaiter2;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, DtdParser.<ParseSubsetAsync>d__160>(ref awaiter2, ref this);
							return;
						}
						goto IL_367;
					case DtdParser.Token.CondSectionStart:
						if (dtdParser.ParsingInternalSubset)
						{
							dtdParser.Throw(dtdParser.curPos - 3, "A conditional section is not allowed in an internal subset.");
						}
						awaiter2 = dtdParser.ParseCondSectionAsync().ConfigureAwait(false).GetAwaiter();
						if (!awaiter2.IsCompleted)
						{
							this.<>1__state = 7;
							this.<>u__2 = awaiter2;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, DtdParser.<ParseSubsetAsync>d__160>(ref awaiter2, ref this);
							return;
						}
						goto IL_3EF;
					case DtdParser.Token.CondSectionEnd:
						if (dtdParser.condSectionDepth <= 0)
						{
							dtdParser.Throw(dtdParser.curPos - 3, "']]>' is not expected.");
							goto IL_59B;
						}
						dtdParser.condSectionDepth--;
						if (dtdParser.validate && dtdParser.currentEntityId != dtdParser.condSectionEntityIds[dtdParser.condSectionDepth])
						{
							dtdParser.SendValidationEvent(dtdParser.curPos, XmlSeverityType.Error, "The parameter entity replacement text must nest properly within markup declarations.", string.Empty);
							goto IL_59B;
						}
						goto IL_59B;
					case DtdParser.Token.Eof:
						if (dtdParser.ParsingInternalSubset && !dtdParser.freeFloatingDtd)
						{
							dtdParser.Throw(dtdParser.curPos, "Incomplete DTD content.");
						}
						if (dtdParser.condSectionDepth != 0)
						{
							dtdParser.Throw(dtdParser.curPos, "There is an unclosed conditional section.");
						}
						goto IL_60A;
					default:
						if (result != DtdParser.Token.RightBracket)
						{
							goto IL_59B;
						}
						if (!dtdParser.ParsingInternalSubset)
						{
							dtdParser.Throw(dtdParser.curPos, "Expected DTD markup was not found.");
							goto IL_55A;
						}
						if (dtdParser.condSectionDepth != 0)
						{
							dtdParser.Throw(dtdParser.curPos, "There is an unclosed conditional section.");
						}
						if (dtdParser.internalSubsetValueSb != null)
						{
							dtdParser.SaveParsingBuffer(dtdParser.curPos - 1);
							dtdParser.schemaInfo.InternalDtdSubset = dtdParser.internalSubsetValueSb.ToString();
							dtdParser.internalSubsetValueSb = null;
						}
						awaiter = dtdParser.GetTokenAsync(false).ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 8;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<DtdParser.Token>.ConfiguredTaskAwaiter, DtdParser.<ParseSubsetAsync>d__160>(ref awaiter, ref this);
							return;
						}
						goto IL_52B;
					}
					IL_146:
					awaiter2.GetResult();
					goto IL_59B;
					IL_1B3:
					awaiter2.GetResult();
					goto IL_59B;
					IL_220:
					awaiter2.GetResult();
					goto IL_59B;
					IL_28D:
					awaiter2.GetResult();
					goto IL_59B;
					IL_2FA:
					awaiter2.GetResult();
					goto IL_59B;
					IL_367:
					awaiter2.GetResult();
					goto IL_59B;
					IL_3EF:
					awaiter2.GetResult();
					this.<startTagEntityId>5__2 = dtdParser.currentEntityId;
					goto IL_59B;
					IL_52B:
					if (awaiter.GetResult() != DtdParser.Token.GreaterThan)
					{
						dtdParser.ThrowUnexpectedToken(dtdParser.curPos, ">");
					}
					IL_55A:
					goto IL_60A;
					IL_59B:
					if (dtdParser.currentEntityId == this.<startTagEntityId>5__2)
					{
						goto IL_38;
					}
					if (dtdParser.validate)
					{
						dtdParser.SendValidationEvent(dtdParser.curPos, XmlSeverityType.Error, "The parameter entity replacement text must nest properly within markup declarations.", string.Empty);
						goto IL_38;
					}
					if (!dtdParser.v1Compat)
					{
						dtdParser.Throw(dtdParser.curPos, "The parameter entity replacement text must nest properly within markup declarations.");
						goto IL_38;
					}
					goto IL_38;
				}
				catch (Exception exception)
				{
					this.<>1__state = -2;
					this.<>t__builder.SetException(exception);
					return;
				}
				IL_60A:
				this.<>1__state = -2;
				this.<>t__builder.SetResult();
			}

			// Token: 0x060013E4 RID: 5092 RVA: 0x00077ED0 File Offset: 0x000760D0
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x0400119A RID: 4506
			public int <>1__state;

			// Token: 0x0400119B RID: 4507
			public AsyncTaskMethodBuilder <>t__builder;

			// Token: 0x0400119C RID: 4508
			public DtdParser <>4__this;

			// Token: 0x0400119D RID: 4509
			private int <startTagEntityId>5__2;

			// Token: 0x0400119E RID: 4510
			private ConfiguredTaskAwaitable<DtdParser.Token>.ConfiguredTaskAwaiter <>u__1;

			// Token: 0x0400119F RID: 4511
			private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter <>u__2;
		}

		// Token: 0x020001F7 RID: 503
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <ParseAttlistDeclAsync>d__161 : IAsyncStateMachine
		{
			// Token: 0x060013E5 RID: 5093 RVA: 0x00077EE0 File Offset: 0x000760E0
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				DtdParser dtdParser = this.<>4__this;
				try
				{
					ConfiguredTaskAwaitable<DtdParser.Token>.ConfiguredTaskAwaiter awaiter;
					ConfiguredTaskAwaitable.ConfiguredTaskAwaiter awaiter2;
					switch (num)
					{
					case 0:
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable<DtdParser.Token>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						break;
					case 1:
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable<DtdParser.Token>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_15A;
					case 2:
						awaiter2 = this.<>u__2;
						this.<>u__2 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_337;
					case 3:
						awaiter2 = this.<>u__2;
						this.<>u__2 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_3AB;
					default:
						awaiter = dtdParser.GetTokenAsync(true).ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 0;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<DtdParser.Token>.ConfiguredTaskAwaiter, DtdParser.<ParseAttlistDeclAsync>d__161>(ref awaiter, ref this);
							return;
						}
						break;
					}
					if (awaiter.GetResult() != DtdParser.Token.QName)
					{
						goto IL_4F5;
					}
					XmlQualifiedName nameQualified = dtdParser.GetNameQualified(true);
					if (!dtdParser.schemaInfo.ElementDecls.TryGetValue(nameQualified, out this.<elementDecl>5__2) && !dtdParser.schemaInfo.UndeclaredElementDecls.TryGetValue(nameQualified, out this.<elementDecl>5__2))
					{
						this.<elementDecl>5__2 = new SchemaElementDecl(nameQualified, nameQualified.Namespace);
						dtdParser.schemaInfo.UndeclaredElementDecls.Add(nameQualified, this.<elementDecl>5__2);
					}
					this.<attrDef>5__3 = null;
					IL_FB:
					awaiter = dtdParser.GetTokenAsync(false).ConfigureAwait(false).GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						this.<>1__state = 1;
						this.<>u__1 = awaiter;
						this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<DtdParser.Token>.ConfiguredTaskAwaiter, DtdParser.<ParseAttlistDeclAsync>d__161>(ref awaiter, ref this);
						return;
					}
					IL_15A:
					DtdParser.Token result = awaiter.GetResult();
					if (result != DtdParser.Token.QName)
					{
						if (result != DtdParser.Token.GreaterThan)
						{
							goto IL_4F5;
						}
						if (dtdParser.v1Compat && this.<attrDef>5__3 != null && this.<attrDef>5__3.Prefix.Length > 0 && this.<attrDef>5__3.Prefix.Equals("xml") && this.<attrDef>5__3.Name.Name == "space")
						{
							this.<attrDef>5__3.Reserved = SchemaAttDef.Reserve.XmlSpace;
							if (this.<attrDef>5__3.Datatype.TokenizedType != XmlTokenizedType.ENUMERATION)
							{
								dtdParser.Throw("Enumeration data type required.", string.Empty, this.<attrDef>5__3.LineNumber, this.<attrDef>5__3.LinePosition);
							}
							if (dtdParser.validate)
							{
								this.<attrDef>5__3.CheckXmlSpace(dtdParser.readerAdapterWithValidation.ValidationEventHandling);
							}
						}
						goto IL_524;
					}
					else
					{
						XmlQualifiedName nameQualified2 = dtdParser.GetNameQualified(true);
						this.<attrDef>5__3 = new SchemaAttDef(nameQualified2, nameQualified2.Namespace);
						this.<attrDef>5__3.IsDeclaredInExternal = !dtdParser.ParsingInternalSubset;
						this.<attrDef>5__3.LineNumber = dtdParser.LineNo;
						this.<attrDef>5__3.LinePosition = dtdParser.LinePos - (dtdParser.curPos - dtdParser.tokenStartPos);
						this.<attrDefAlreadyExists>5__4 = (this.<elementDecl>5__2.GetAttDef(this.<attrDef>5__3.Name) != null);
						awaiter2 = dtdParser.ParseAttlistTypeAsync(this.<attrDef>5__3, this.<elementDecl>5__2, this.<attrDefAlreadyExists>5__4).ConfigureAwait(false).GetAwaiter();
						if (!awaiter2.IsCompleted)
						{
							this.<>1__state = 2;
							this.<>u__2 = awaiter2;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, DtdParser.<ParseAttlistDeclAsync>d__161>(ref awaiter2, ref this);
							return;
						}
					}
					IL_337:
					awaiter2.GetResult();
					awaiter2 = dtdParser.ParseAttlistDefaultAsync(this.<attrDef>5__3, this.<attrDefAlreadyExists>5__4).ConfigureAwait(false).GetAwaiter();
					if (!awaiter2.IsCompleted)
					{
						this.<>1__state = 3;
						this.<>u__2 = awaiter2;
						this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, DtdParser.<ParseAttlistDeclAsync>d__161>(ref awaiter2, ref this);
						return;
					}
					IL_3AB:
					awaiter2.GetResult();
					if (this.<attrDef>5__3.Prefix.Length > 0 && this.<attrDef>5__3.Prefix.Equals("xml"))
					{
						if (this.<attrDef>5__3.Name.Name == "space")
						{
							if (dtdParser.v1Compat)
							{
								string text = this.<attrDef>5__3.DefaultValueExpanded.Trim();
								if (text.Equals("preserve") || text.Equals("default"))
								{
									this.<attrDef>5__3.Reserved = SchemaAttDef.Reserve.XmlSpace;
								}
							}
							else
							{
								this.<attrDef>5__3.Reserved = SchemaAttDef.Reserve.XmlSpace;
								if (this.<attrDef>5__3.TokenizedType != XmlTokenizedType.ENUMERATION)
								{
									dtdParser.Throw("Enumeration data type required.", string.Empty, this.<attrDef>5__3.LineNumber, this.<attrDef>5__3.LinePosition);
								}
								if (dtdParser.validate)
								{
									this.<attrDef>5__3.CheckXmlSpace(dtdParser.readerAdapterWithValidation.ValidationEventHandling);
								}
							}
						}
						else if (this.<attrDef>5__3.Name.Name == "lang")
						{
							this.<attrDef>5__3.Reserved = SchemaAttDef.Reserve.XmlLang;
						}
					}
					if (!this.<attrDefAlreadyExists>5__4)
					{
						this.<elementDecl>5__2.AddAttDef(this.<attrDef>5__3);
						goto IL_FB;
					}
					goto IL_FB;
					IL_4F5:
					dtdParser.OnUnexpectedError();
				}
				catch (Exception exception)
				{
					this.<>1__state = -2;
					this.<elementDecl>5__2 = null;
					this.<attrDef>5__3 = null;
					this.<>t__builder.SetException(exception);
					return;
				}
				IL_524:
				this.<>1__state = -2;
				this.<elementDecl>5__2 = null;
				this.<attrDef>5__3 = null;
				this.<>t__builder.SetResult();
			}

			// Token: 0x060013E6 RID: 5094 RVA: 0x00078450 File Offset: 0x00076650
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x040011A0 RID: 4512
			public int <>1__state;

			// Token: 0x040011A1 RID: 4513
			public AsyncTaskMethodBuilder <>t__builder;

			// Token: 0x040011A2 RID: 4514
			public DtdParser <>4__this;

			// Token: 0x040011A3 RID: 4515
			private SchemaElementDecl <elementDecl>5__2;

			// Token: 0x040011A4 RID: 4516
			private SchemaAttDef <attrDef>5__3;

			// Token: 0x040011A5 RID: 4517
			private ConfiguredTaskAwaitable<DtdParser.Token>.ConfiguredTaskAwaiter <>u__1;

			// Token: 0x040011A6 RID: 4518
			private bool <attrDefAlreadyExists>5__4;

			// Token: 0x040011A7 RID: 4519
			private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter <>u__2;
		}

		// Token: 0x020001F8 RID: 504
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <ParseAttlistTypeAsync>d__162 : IAsyncStateMachine
		{
			// Token: 0x060013E7 RID: 5095 RVA: 0x00078460 File Offset: 0x00076660
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				DtdParser dtdParser = this.<>4__this;
				try
				{
					ConfiguredTaskAwaitable<DtdParser.Token>.ConfiguredTaskAwaiter awaiter;
					switch (num)
					{
					case 0:
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable<DtdParser.Token>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						break;
					case 1:
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable<DtdParser.Token>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_25E;
					case 2:
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable<DtdParser.Token>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_2CB;
					case 3:
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable<DtdParser.Token>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_3C7;
					case 4:
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable<DtdParser.Token>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_43E;
					case 5:
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable<DtdParser.Token>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_4EA;
					case 6:
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable<DtdParser.Token>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_568;
					case 7:
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable<DtdParser.Token>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_5E2;
					default:
						awaiter = dtdParser.GetTokenAsync(true).ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 0;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<DtdParser.Token>.ConfiguredTaskAwaiter, DtdParser.<ParseAttlistTypeAsync>d__162>(ref awaiter, ref this);
							return;
						}
						break;
					}
					DtdParser.Token result = awaiter.GetResult();
					if (result != DtdParser.Token.CDATA)
					{
						this.elementDecl.HasNonCDataAttribute = true;
					}
					if (dtdParser.IsAttributeValueType(result))
					{
						this.attrDef.TokenizedType = (XmlTokenizedType)result;
						this.attrDef.SchemaType = XmlSchemaType.GetBuiltInSimpleType(this.attrDef.Datatype.TypeCode);
						if (result != DtdParser.Token.ID)
						{
							if (result == DtdParser.Token.NOTATION)
							{
								if (dtdParser.validate)
								{
									if (this.elementDecl.IsNotationDeclared && !this.ignoreErrors)
									{
										dtdParser.SendValidationEvent(dtdParser.curPos - 8, XmlSeverityType.Error, "No element type can have more than one NOTATION attribute specified.", this.elementDecl.Name.ToString());
									}
									else
									{
										if (this.elementDecl.ContentValidator != null && this.elementDecl.ContentValidator.ContentType == XmlSchemaContentType.Empty && !this.ignoreErrors)
										{
											dtdParser.SendValidationEvent(dtdParser.curPos - 8, XmlSeverityType.Error, "An attribute of type NOTATION must not be declared on an element declared EMPTY.", this.elementDecl.Name.ToString());
										}
										this.elementDecl.IsNotationDeclared = true;
									}
								}
								awaiter = dtdParser.GetTokenAsync(true).ConfigureAwait(false).GetAwaiter();
								if (!awaiter.IsCompleted)
								{
									this.<>1__state = 1;
									this.<>u__1 = awaiter;
									this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<DtdParser.Token>.ConfiguredTaskAwaiter, DtdParser.<ParseAttlistTypeAsync>d__162>(ref awaiter, ref this);
									return;
								}
								goto IL_25E;
							}
						}
						else
						{
							if (dtdParser.validate && this.elementDecl.IsIdDeclared)
							{
								SchemaAttDef attDef = this.elementDecl.GetAttDef(this.attrDef.Name);
								if ((attDef == null || attDef.Datatype.TokenizedType != XmlTokenizedType.ID) && !this.ignoreErrors)
								{
									dtdParser.SendValidationEvent(XmlSeverityType.Error, "The attribute of type ID is already declared on the '{0}' element.", this.elementDecl.Name.ToString());
								}
							}
							this.elementDecl.IsIdDeclared = true;
						}
						goto IL_688;
					}
					if (result != DtdParser.Token.LeftParen)
					{
						goto IL_667;
					}
					this.attrDef.TokenizedType = XmlTokenizedType.ENUMERATION;
					this.attrDef.SchemaType = XmlSchemaType.GetBuiltInSimpleType(this.attrDef.Datatype.TypeCode);
					awaiter = dtdParser.GetTokenAsync(false).ConfigureAwait(false).GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						this.<>1__state = 5;
						this.<>u__1 = awaiter;
						this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<DtdParser.Token>.ConfiguredTaskAwaiter, DtdParser.<ParseAttlistTypeAsync>d__162>(ref awaiter, ref this);
						return;
					}
					goto IL_4EA;
					IL_25E:
					if (awaiter.GetResult() != DtdParser.Token.LeftParen)
					{
						goto IL_667;
					}
					awaiter = dtdParser.GetTokenAsync(false).ConfigureAwait(false).GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						this.<>1__state = 2;
						this.<>u__1 = awaiter;
						this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<DtdParser.Token>.ConfiguredTaskAwaiter, DtdParser.<ParseAttlistTypeAsync>d__162>(ref awaiter, ref this);
						return;
					}
					IL_2CB:
					if (awaiter.GetResult() != DtdParser.Token.Name)
					{
						goto IL_667;
					}
					IL_2D9:
					string nameString = dtdParser.GetNameString();
					if (!dtdParser.schemaInfo.Notations.ContainsKey(nameString))
					{
						dtdParser.AddUndeclaredNotation(nameString);
					}
					if (dtdParser.validate && !dtdParser.v1Compat && this.attrDef.Values != null && this.attrDef.Values.Contains(nameString) && !this.ignoreErrors)
					{
						dtdParser.SendValidationEvent(XmlSeverityType.Error, new XmlSchemaException("'{0}' is a duplicate notation value.", nameString, dtdParser.BaseUriStr, dtdParser.LineNo, dtdParser.LinePos));
					}
					this.attrDef.AddValue(nameString);
					awaiter = dtdParser.GetTokenAsync(false).ConfigureAwait(false).GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						this.<>1__state = 3;
						this.<>u__1 = awaiter;
						this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<DtdParser.Token>.ConfiguredTaskAwaiter, DtdParser.<ParseAttlistTypeAsync>d__162>(ref awaiter, ref this);
						return;
					}
					IL_3C7:
					DtdParser.Token result2 = awaiter.GetResult();
					if (result2 == DtdParser.Token.RightParen)
					{
						goto IL_688;
					}
					if (result2 != DtdParser.Token.Or)
					{
						goto IL_667;
					}
					awaiter = dtdParser.GetTokenAsync(false).ConfigureAwait(false).GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						this.<>1__state = 4;
						this.<>u__1 = awaiter;
						this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<DtdParser.Token>.ConfiguredTaskAwaiter, DtdParser.<ParseAttlistTypeAsync>d__162>(ref awaiter, ref this);
						return;
					}
					IL_43E:
					if (awaiter.GetResult() != DtdParser.Token.Name)
					{
						goto IL_667;
					}
					goto IL_2D9;
					IL_4EA:
					if (awaiter.GetResult() != DtdParser.Token.Nmtoken)
					{
						goto IL_667;
					}
					this.attrDef.AddValue(dtdParser.GetNameString());
					IL_509:
					awaiter = dtdParser.GetTokenAsync(false).ConfigureAwait(false).GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						this.<>1__state = 6;
						this.<>u__1 = awaiter;
						this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<DtdParser.Token>.ConfiguredTaskAwaiter, DtdParser.<ParseAttlistTypeAsync>d__162>(ref awaiter, ref this);
						return;
					}
					IL_568:
					result2 = awaiter.GetResult();
					if (result2 == DtdParser.Token.RightParen)
					{
						goto IL_688;
					}
					if (result2 != DtdParser.Token.Or)
					{
						goto IL_667;
					}
					awaiter = dtdParser.GetTokenAsync(false).ConfigureAwait(false).GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						this.<>1__state = 7;
						this.<>u__1 = awaiter;
						this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<DtdParser.Token>.ConfiguredTaskAwaiter, DtdParser.<ParseAttlistTypeAsync>d__162>(ref awaiter, ref this);
						return;
					}
					IL_5E2:
					if (awaiter.GetResult() == DtdParser.Token.Nmtoken)
					{
						string nmtokenString = dtdParser.GetNmtokenString();
						if (dtdParser.validate && !dtdParser.v1Compat && this.attrDef.Values != null && this.attrDef.Values.Contains(nmtokenString) && !this.ignoreErrors)
						{
							dtdParser.SendValidationEvent(XmlSeverityType.Error, new XmlSchemaException("'{0}' is a duplicate enumeration value.", nmtokenString, dtdParser.BaseUriStr, dtdParser.LineNo, dtdParser.LinePos));
						}
						this.attrDef.AddValue(nmtokenString);
						goto IL_509;
					}
					IL_667:
					dtdParser.OnUnexpectedError();
				}
				catch (Exception exception)
				{
					this.<>1__state = -2;
					this.<>t__builder.SetException(exception);
					return;
				}
				IL_688:
				this.<>1__state = -2;
				this.<>t__builder.SetResult();
			}

			// Token: 0x060013E8 RID: 5096 RVA: 0x00078B24 File Offset: 0x00076D24
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x040011A8 RID: 4520
			public int <>1__state;

			// Token: 0x040011A9 RID: 4521
			public AsyncTaskMethodBuilder <>t__builder;

			// Token: 0x040011AA RID: 4522
			public DtdParser <>4__this;

			// Token: 0x040011AB RID: 4523
			public SchemaElementDecl elementDecl;

			// Token: 0x040011AC RID: 4524
			public SchemaAttDef attrDef;

			// Token: 0x040011AD RID: 4525
			public bool ignoreErrors;

			// Token: 0x040011AE RID: 4526
			private ConfiguredTaskAwaitable<DtdParser.Token>.ConfiguredTaskAwaiter <>u__1;
		}

		// Token: 0x020001F9 RID: 505
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <ParseAttlistDefaultAsync>d__163 : IAsyncStateMachine
		{
			// Token: 0x060013E9 RID: 5097 RVA: 0x00078B34 File Offset: 0x00076D34
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				DtdParser dtdParser = this.<>4__this;
				try
				{
					ConfiguredTaskAwaitable<DtdParser.Token>.ConfiguredTaskAwaiter awaiter;
					if (num != 0)
					{
						if (num == 1)
						{
							awaiter = this.<>u__1;
							this.<>u__1 = default(ConfiguredTaskAwaitable<DtdParser.Token>.ConfiguredTaskAwaiter);
							this.<>1__state = -1;
							goto IL_12E;
						}
						awaiter = dtdParser.GetTokenAsync(true).ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 0;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<DtdParser.Token>.ConfiguredTaskAwaiter, DtdParser.<ParseAttlistDefaultAsync>d__163>(ref awaiter, ref this);
							return;
						}
					}
					else
					{
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable<DtdParser.Token>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
					}
					DtdParser.Token result = awaiter.GetResult();
					switch (result)
					{
					case DtdParser.Token.REQUIRED:
						this.attrDef.Presence = SchemaDeclBase.Use.Required;
						goto IL_209;
					case DtdParser.Token.IMPLIED:
						this.attrDef.Presence = SchemaDeclBase.Use.Implied;
						goto IL_209;
					case DtdParser.Token.FIXED:
						this.attrDef.Presence = SchemaDeclBase.Use.Fixed;
						awaiter = dtdParser.GetTokenAsync(true).ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 1;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<DtdParser.Token>.ConfiguredTaskAwaiter, DtdParser.<ParseAttlistDefaultAsync>d__163>(ref awaiter, ref this);
							return;
						}
						break;
					default:
						if (result != DtdParser.Token.Literal)
						{
							goto IL_1E8;
						}
						goto IL_13C;
					}
					IL_12E:
					if (awaiter.GetResult() != DtdParser.Token.Literal)
					{
						goto IL_1E8;
					}
					IL_13C:
					if (dtdParser.validate && this.attrDef.Datatype.TokenizedType == XmlTokenizedType.ID && !this.ignoreErrors)
					{
						dtdParser.SendValidationEvent(dtdParser.curPos, XmlSeverityType.Error, "An attribute of type ID must have a declared default of either #IMPLIED or #REQUIRED.", string.Empty);
					}
					if (this.attrDef.TokenizedType != XmlTokenizedType.CDATA)
					{
						this.attrDef.DefaultValueExpanded = dtdParser.GetValueWithStrippedSpaces();
					}
					else
					{
						this.attrDef.DefaultValueExpanded = dtdParser.GetValue();
					}
					this.attrDef.ValueLineNumber = dtdParser.literalLineInfo.lineNo;
					this.attrDef.ValueLinePosition = dtdParser.literalLineInfo.linePos + 1;
					DtdValidator.SetDefaultTypedValue(this.attrDef, dtdParser.readerAdapter);
					goto IL_209;
					IL_1E8:
					dtdParser.OnUnexpectedError();
				}
				catch (Exception exception)
				{
					this.<>1__state = -2;
					this.<>t__builder.SetException(exception);
					return;
				}
				IL_209:
				this.<>1__state = -2;
				this.<>t__builder.SetResult();
			}

			// Token: 0x060013EA RID: 5098 RVA: 0x00078D7C File Offset: 0x00076F7C
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x040011AF RID: 4527
			public int <>1__state;

			// Token: 0x040011B0 RID: 4528
			public AsyncTaskMethodBuilder <>t__builder;

			// Token: 0x040011B1 RID: 4529
			public DtdParser <>4__this;

			// Token: 0x040011B2 RID: 4530
			public SchemaAttDef attrDef;

			// Token: 0x040011B3 RID: 4531
			public bool ignoreErrors;

			// Token: 0x040011B4 RID: 4532
			private ConfiguredTaskAwaitable<DtdParser.Token>.ConfiguredTaskAwaiter <>u__1;
		}

		// Token: 0x020001FA RID: 506
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <ParseElementDeclAsync>d__164 : IAsyncStateMachine
		{
			// Token: 0x060013EB RID: 5099 RVA: 0x00078D8C File Offset: 0x00076F8C
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				DtdParser dtdParser = this.<>4__this;
				try
				{
					ConfiguredTaskAwaitable<DtdParser.Token>.ConfiguredTaskAwaiter awaiter;
					ConfiguredTaskAwaitable.ConfiguredTaskAwaiter awaiter2;
					switch (num)
					{
					case 0:
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable<DtdParser.Token>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						break;
					case 1:
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable<DtdParser.Token>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_1B8;
					case 2:
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable<DtdParser.Token>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_26B;
					case 3:
						awaiter2 = this.<>u__2;
						this.<>u__2 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_315;
					case 4:
						awaiter2 = this.<>u__2;
						this.<>u__2 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_3CE;
					case 5:
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable<DtdParser.Token>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_448;
					default:
						awaiter = dtdParser.GetTokenAsync(true).ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 0;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<DtdParser.Token>.ConfiguredTaskAwaiter, DtdParser.<ParseElementDeclAsync>d__164>(ref awaiter, ref this);
							return;
						}
						break;
					}
					if (awaiter.GetResult() != DtdParser.Token.QName)
					{
						goto IL_466;
					}
					this.<elementDecl>5__2 = null;
					XmlQualifiedName nameQualified = dtdParser.GetNameQualified(true);
					if (dtdParser.schemaInfo.ElementDecls.TryGetValue(nameQualified, out this.<elementDecl>5__2))
					{
						if (dtdParser.validate)
						{
							dtdParser.SendValidationEvent(dtdParser.curPos - nameQualified.Name.Length, XmlSeverityType.Error, "The '{0}' element has already been declared.", dtdParser.GetNameString());
						}
					}
					else
					{
						if (dtdParser.schemaInfo.UndeclaredElementDecls.TryGetValue(nameQualified, out this.<elementDecl>5__2))
						{
							dtdParser.schemaInfo.UndeclaredElementDecls.Remove(nameQualified);
						}
						else
						{
							this.<elementDecl>5__2 = new SchemaElementDecl(nameQualified, nameQualified.Namespace);
						}
						dtdParser.schemaInfo.ElementDecls.Add(nameQualified, this.<elementDecl>5__2);
					}
					this.<elementDecl>5__2.IsDeclaredInExternal = !dtdParser.ParsingInternalSubset;
					awaiter = dtdParser.GetTokenAsync(true).ConfigureAwait(false).GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						this.<>1__state = 1;
						this.<>u__1 = awaiter;
						this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<DtdParser.Token>.ConfiguredTaskAwaiter, DtdParser.<ParseElementDeclAsync>d__164>(ref awaiter, ref this);
						return;
					}
					IL_1B8:
					DtdParser.Token result = awaiter.GetResult();
					if (result != DtdParser.Token.LeftParen)
					{
						if (result == DtdParser.Token.ANY)
						{
							this.<elementDecl>5__2.ContentValidator = ContentValidator.Any;
							goto IL_3EC;
						}
						if (result == DtdParser.Token.EMPTY)
						{
							this.<elementDecl>5__2.ContentValidator = ContentValidator.Empty;
							goto IL_3EC;
						}
						goto IL_466;
					}
					else
					{
						this.<startParenEntityId>5__3 = dtdParser.currentEntityId;
						awaiter = dtdParser.GetTokenAsync(false).ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 2;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<DtdParser.Token>.ConfiguredTaskAwaiter, DtdParser.<ParseElementDeclAsync>d__164>(ref awaiter, ref this);
							return;
						}
					}
					IL_26B:
					DtdParser.Token result2 = awaiter.GetResult();
					if (result2 != DtdParser.Token.None)
					{
						if (result2 != DtdParser.Token.PCDATA)
						{
							goto IL_466;
						}
						this.<pcv>5__4 = new ParticleContentValidator(XmlSchemaContentType.Mixed);
						this.<pcv>5__4.Start();
						this.<pcv>5__4.OpenGroup();
						awaiter2 = dtdParser.ParseElementMixedContentAsync(this.<pcv>5__4, this.<startParenEntityId>5__3).ConfigureAwait(false).GetAwaiter();
						if (!awaiter2.IsCompleted)
						{
							this.<>1__state = 3;
							this.<>u__2 = awaiter2;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, DtdParser.<ParseElementDeclAsync>d__164>(ref awaiter2, ref this);
							return;
						}
					}
					else
					{
						this.<pcv>5__4 = null;
						this.<pcv>5__4 = new ParticleContentValidator(XmlSchemaContentType.ElementOnly);
						this.<pcv>5__4.Start();
						this.<pcv>5__4.OpenGroup();
						awaiter2 = dtdParser.ParseElementOnlyContentAsync(this.<pcv>5__4, this.<startParenEntityId>5__3).ConfigureAwait(false).GetAwaiter();
						if (!awaiter2.IsCompleted)
						{
							this.<>1__state = 4;
							this.<>u__2 = awaiter2;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, DtdParser.<ParseElementDeclAsync>d__164>(ref awaiter2, ref this);
							return;
						}
						goto IL_3CE;
					}
					IL_315:
					awaiter2.GetResult();
					this.<elementDecl>5__2.ContentValidator = this.<pcv>5__4.Finish(true);
					goto IL_3EC;
					IL_3CE:
					awaiter2.GetResult();
					this.<elementDecl>5__2.ContentValidator = this.<pcv>5__4.Finish(true);
					IL_3EC:
					awaiter = dtdParser.GetTokenAsync(false).ConfigureAwait(false).GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						this.<>1__state = 5;
						this.<>u__1 = awaiter;
						this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<DtdParser.Token>.ConfiguredTaskAwaiter, DtdParser.<ParseElementDeclAsync>d__164>(ref awaiter, ref this);
						return;
					}
					IL_448:
					if (awaiter.GetResult() != DtdParser.Token.GreaterThan)
					{
						dtdParser.ThrowUnexpectedToken(dtdParser.curPos, ">");
					}
					goto IL_48E;
					IL_466:
					dtdParser.OnUnexpectedError();
				}
				catch (Exception exception)
				{
					this.<>1__state = -2;
					this.<elementDecl>5__2 = null;
					this.<>t__builder.SetException(exception);
					return;
				}
				IL_48E:
				this.<>1__state = -2;
				this.<elementDecl>5__2 = null;
				this.<>t__builder.SetResult();
			}

			// Token: 0x060013EC RID: 5100 RVA: 0x00079260 File Offset: 0x00077460
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x040011B5 RID: 4533
			public int <>1__state;

			// Token: 0x040011B6 RID: 4534
			public AsyncTaskMethodBuilder <>t__builder;

			// Token: 0x040011B7 RID: 4535
			public DtdParser <>4__this;

			// Token: 0x040011B8 RID: 4536
			private SchemaElementDecl <elementDecl>5__2;

			// Token: 0x040011B9 RID: 4537
			private ConfiguredTaskAwaitable<DtdParser.Token>.ConfiguredTaskAwaiter <>u__1;

			// Token: 0x040011BA RID: 4538
			private int <startParenEntityId>5__3;

			// Token: 0x040011BB RID: 4539
			private ParticleContentValidator <pcv>5__4;

			// Token: 0x040011BC RID: 4540
			private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter <>u__2;
		}

		// Token: 0x020001FB RID: 507
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <ParseElementOnlyContentAsync>d__165 : IAsyncStateMachine
		{
			// Token: 0x060013ED RID: 5101 RVA: 0x00079270 File Offset: 0x00077470
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				DtdParser dtdParser = this.<>4__this;
				try
				{
					ConfiguredTaskAwaitable<DtdParser.Token>.ConfiguredTaskAwaiter awaiter;
					ConfiguredTaskAwaitable.ConfiguredTaskAwaiter awaiter2;
					switch (num)
					{
					case 0:
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable<DtdParser.Token>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_B0;
					case 1:
						awaiter2 = this.<>u__2;
						this.<>u__2 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_14C;
					case 2:
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable<DtdParser.Token>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_1FC;
					case 3:
						awaiter2 = this.<>u__2;
						this.<>u__2 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_33F;
					default:
						this.<localFrames>5__2 = new Stack<DtdParser.ParseElementOnlyContent_LocalFrame>();
						this.<currentFrame>5__3 = new DtdParser.ParseElementOnlyContent_LocalFrame(this.startParenEntityId);
						this.<localFrames>5__2.Push(this.<currentFrame>5__3);
						break;
					}
					IL_51:
					awaiter = dtdParser.GetTokenAsync(false).ConfigureAwait(false).GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						this.<>1__state = 0;
						this.<>u__1 = awaiter;
						this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<DtdParser.Token>.ConfiguredTaskAwaiter, DtdParser.<ParseElementOnlyContentAsync>d__165>(ref awaiter, ref this);
						return;
					}
					IL_B0:
					DtdParser.Token result = awaiter.GetResult();
					if (result != DtdParser.Token.QName)
					{
						if (result == DtdParser.Token.LeftParen)
						{
							this.pcv.OpenGroup();
							this.<currentFrame>5__3 = new DtdParser.ParseElementOnlyContent_LocalFrame(dtdParser.currentEntityId);
							this.<localFrames>5__2.Push(this.<currentFrame>5__3);
							goto IL_51;
						}
						if (result != DtdParser.Token.GreaterThan)
						{
							goto IL_35B;
						}
						dtdParser.Throw(dtdParser.curPos, "Invalid content model.");
						goto IL_361;
					}
					else
					{
						this.pcv.AddName(dtdParser.GetNameQualified(true), null);
						awaiter2 = dtdParser.ParseHowManyAsync(this.pcv).ConfigureAwait(false).GetAwaiter();
						if (!awaiter2.IsCompleted)
						{
							this.<>1__state = 1;
							this.<>u__2 = awaiter2;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, DtdParser.<ParseElementOnlyContentAsync>d__165>(ref awaiter2, ref this);
							return;
						}
					}
					IL_14C:
					awaiter2.GetResult();
					IL_19D:
					awaiter = dtdParser.GetTokenAsync(false).ConfigureAwait(false).GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						this.<>1__state = 2;
						this.<>u__1 = awaiter;
						this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<DtdParser.Token>.ConfiguredTaskAwaiter, DtdParser.<ParseElementOnlyContentAsync>d__165>(ref awaiter, ref this);
						return;
					}
					IL_1FC:
					result = awaiter.GetResult();
					switch (result)
					{
					case DtdParser.Token.RightParen:
						this.pcv.CloseGroup();
						if (dtdParser.validate && dtdParser.currentEntityId != this.<currentFrame>5__3.startParenEntityId)
						{
							dtdParser.SendValidationEvent(dtdParser.curPos, XmlSeverityType.Error, "The parameter entity replacement text must nest properly within markup declarations.", string.Empty);
						}
						awaiter2 = dtdParser.ParseHowManyAsync(this.pcv).ConfigureAwait(false).GetAwaiter();
						if (!awaiter2.IsCompleted)
						{
							this.<>1__state = 3;
							this.<>u__2 = awaiter2;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, DtdParser.<ParseElementOnlyContentAsync>d__165>(ref awaiter2, ref this);
							return;
						}
						break;
					case DtdParser.Token.GreaterThan:
						dtdParser.Throw(dtdParser.curPos, "Invalid content model.");
						goto IL_361;
					case DtdParser.Token.Or:
						if (this.<currentFrame>5__3.parsingSchema == DtdParser.Token.Comma)
						{
							dtdParser.Throw(dtdParser.curPos, "Invalid content model.");
						}
						this.pcv.AddChoice();
						this.<currentFrame>5__3.parsingSchema = DtdParser.Token.Or;
						goto IL_51;
					default:
						if (result == DtdParser.Token.Comma)
						{
							if (this.<currentFrame>5__3.parsingSchema == DtdParser.Token.Or)
							{
								dtdParser.Throw(dtdParser.curPos, "Invalid content model.");
							}
							this.pcv.AddSequence();
							this.<currentFrame>5__3.parsingSchema = DtdParser.Token.Comma;
							goto IL_51;
						}
						goto IL_35B;
					}
					IL_33F:
					awaiter2.GetResult();
					goto IL_361;
					IL_35B:
					dtdParser.OnUnexpectedError();
					IL_361:
					this.<localFrames>5__2.Pop();
					if (this.<localFrames>5__2.Count > 0)
					{
						this.<currentFrame>5__3 = this.<localFrames>5__2.Peek();
						goto IL_19D;
					}
				}
				catch (Exception exception)
				{
					this.<>1__state = -2;
					this.<localFrames>5__2 = null;
					this.<currentFrame>5__3 = null;
					this.<>t__builder.SetException(exception);
					return;
				}
				this.<>1__state = -2;
				this.<localFrames>5__2 = null;
				this.<currentFrame>5__3 = null;
				this.<>t__builder.SetResult();
			}

			// Token: 0x060013EE RID: 5102 RVA: 0x00079674 File Offset: 0x00077874
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x040011BD RID: 4541
			public int <>1__state;

			// Token: 0x040011BE RID: 4542
			public AsyncTaskMethodBuilder <>t__builder;

			// Token: 0x040011BF RID: 4543
			public int startParenEntityId;

			// Token: 0x040011C0 RID: 4544
			public DtdParser <>4__this;

			// Token: 0x040011C1 RID: 4545
			public ParticleContentValidator pcv;

			// Token: 0x040011C2 RID: 4546
			private Stack<DtdParser.ParseElementOnlyContent_LocalFrame> <localFrames>5__2;

			// Token: 0x040011C3 RID: 4547
			private DtdParser.ParseElementOnlyContent_LocalFrame <currentFrame>5__3;

			// Token: 0x040011C4 RID: 4548
			private ConfiguredTaskAwaitable<DtdParser.Token>.ConfiguredTaskAwaiter <>u__1;

			// Token: 0x040011C5 RID: 4549
			private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter <>u__2;
		}

		// Token: 0x020001FC RID: 508
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <ParseHowManyAsync>d__166 : IAsyncStateMachine
		{
			// Token: 0x060013EF RID: 5103 RVA: 0x00079684 File Offset: 0x00077884
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				DtdParser dtdParser = this.<>4__this;
				try
				{
					ConfiguredTaskAwaitable<DtdParser.Token>.ConfiguredTaskAwaiter awaiter;
					if (num != 0)
					{
						awaiter = dtdParser.GetTokenAsync(false).ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 0;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<DtdParser.Token>.ConfiguredTaskAwaiter, DtdParser.<ParseHowManyAsync>d__166>(ref awaiter, ref this);
							return;
						}
					}
					else
					{
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable<DtdParser.Token>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
					}
					switch (awaiter.GetResult())
					{
					case DtdParser.Token.Star:
						this.pcv.AddStar();
						break;
					case DtdParser.Token.QMark:
						this.pcv.AddQMark();
						break;
					case DtdParser.Token.Plus:
						this.pcv.AddPlus();
						break;
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

			// Token: 0x060013F0 RID: 5104 RVA: 0x00079784 File Offset: 0x00077984
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x040011C6 RID: 4550
			public int <>1__state;

			// Token: 0x040011C7 RID: 4551
			public AsyncTaskMethodBuilder <>t__builder;

			// Token: 0x040011C8 RID: 4552
			public DtdParser <>4__this;

			// Token: 0x040011C9 RID: 4553
			public ParticleContentValidator pcv;

			// Token: 0x040011CA RID: 4554
			private ConfiguredTaskAwaitable<DtdParser.Token>.ConfiguredTaskAwaiter <>u__1;
		}

		// Token: 0x020001FD RID: 509
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <ParseElementMixedContentAsync>d__167 : IAsyncStateMachine
		{
			// Token: 0x060013F1 RID: 5105 RVA: 0x00079794 File Offset: 0x00077994
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				DtdParser dtdParser = this.<>4__this;
				try
				{
					ConfiguredTaskAwaitable<DtdParser.Token>.ConfiguredTaskAwaiter awaiter;
					switch (num)
					{
					case 0:
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable<DtdParser.Token>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_9C;
					case 1:
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable<DtdParser.Token>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_150;
					case 2:
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable<DtdParser.Token>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_246;
					default:
						this.<hasNames>5__2 = false;
						this.<connectorEntityId>5__3 = -1;
						this.<contentEntityId>5__4 = dtdParser.currentEntityId;
						break;
					}
					IL_3A:
					awaiter = dtdParser.GetTokenAsync(false).ConfigureAwait(false).GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						this.<>1__state = 0;
						this.<>u__1 = awaiter;
						this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<DtdParser.Token>.ConfiguredTaskAwaiter, DtdParser.<ParseElementMixedContentAsync>d__167>(ref awaiter, ref this);
						return;
					}
					IL_9C:
					DtdParser.Token result = awaiter.GetResult();
					if (result != DtdParser.Token.RightParen)
					{
						if (result != DtdParser.Token.Or)
						{
							goto IL_2D5;
						}
						if (!this.<hasNames>5__2)
						{
							this.<hasNames>5__2 = true;
						}
						else
						{
							this.pcv.AddChoice();
						}
						if (dtdParser.validate)
						{
							this.<connectorEntityId>5__3 = dtdParser.currentEntityId;
							if (this.<contentEntityId>5__4 < this.<connectorEntityId>5__3)
							{
								dtdParser.SendValidationEvent(dtdParser.curPos, XmlSeverityType.Error, "The parameter entity replacement text must nest properly within markup declarations.", string.Empty);
							}
						}
						awaiter = dtdParser.GetTokenAsync(false).ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 2;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<DtdParser.Token>.ConfiguredTaskAwaiter, DtdParser.<ParseElementMixedContentAsync>d__167>(ref awaiter, ref this);
							return;
						}
						goto IL_246;
					}
					else
					{
						this.pcv.CloseGroup();
						if (dtdParser.validate && dtdParser.currentEntityId != this.startParenEntityId)
						{
							dtdParser.SendValidationEvent(dtdParser.curPos, XmlSeverityType.Error, "The parameter entity replacement text must nest properly within markup declarations.", string.Empty);
						}
						awaiter = dtdParser.GetTokenAsync(false).ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 1;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<DtdParser.Token>.ConfiguredTaskAwaiter, DtdParser.<ParseElementMixedContentAsync>d__167>(ref awaiter, ref this);
							return;
						}
					}
					IL_150:
					if (awaiter.GetResult() == DtdParser.Token.Star & this.<hasNames>5__2)
					{
						this.pcv.AddStar();
					}
					else if (this.<hasNames>5__2)
					{
						dtdParser.ThrowUnexpectedToken(dtdParser.curPos, "*");
					}
					goto IL_2F9;
					IL_246:
					if (awaiter.GetResult() == DtdParser.Token.QName)
					{
						XmlQualifiedName nameQualified = dtdParser.GetNameQualified(true);
						if (this.pcv.Exists(nameQualified) && dtdParser.validate)
						{
							dtdParser.SendValidationEvent(XmlSeverityType.Error, "The '{0}' element already exists in the content model.", nameQualified.ToString());
						}
						this.pcv.AddName(nameQualified, null);
						if (!dtdParser.validate)
						{
							goto IL_3A;
						}
						this.<contentEntityId>5__4 = dtdParser.currentEntityId;
						if (this.<contentEntityId>5__4 < this.<connectorEntityId>5__3)
						{
							dtdParser.SendValidationEvent(dtdParser.curPos, XmlSeverityType.Error, "The parameter entity replacement text must nest properly within markup declarations.", string.Empty);
							goto IL_3A;
						}
						goto IL_3A;
					}
					IL_2D5:
					dtdParser.OnUnexpectedError();
					goto IL_3A;
				}
				catch (Exception exception)
				{
					this.<>1__state = -2;
					this.<>t__builder.SetException(exception);
					return;
				}
				IL_2F9:
				this.<>1__state = -2;
				this.<>t__builder.SetResult();
			}

			// Token: 0x060013F2 RID: 5106 RVA: 0x00079ACC File Offset: 0x00077CCC
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x040011CB RID: 4555
			public int <>1__state;

			// Token: 0x040011CC RID: 4556
			public AsyncTaskMethodBuilder <>t__builder;

			// Token: 0x040011CD RID: 4557
			public DtdParser <>4__this;

			// Token: 0x040011CE RID: 4558
			public ParticleContentValidator pcv;

			// Token: 0x040011CF RID: 4559
			public int startParenEntityId;

			// Token: 0x040011D0 RID: 4560
			private bool <hasNames>5__2;

			// Token: 0x040011D1 RID: 4561
			private int <connectorEntityId>5__3;

			// Token: 0x040011D2 RID: 4562
			private int <contentEntityId>5__4;

			// Token: 0x040011D3 RID: 4563
			private ConfiguredTaskAwaitable<DtdParser.Token>.ConfiguredTaskAwaiter <>u__1;
		}

		// Token: 0x020001FE RID: 510
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <ParseEntityDeclAsync>d__168 : IAsyncStateMachine
		{
			// Token: 0x060013F3 RID: 5107 RVA: 0x00079ADC File Offset: 0x00077CDC
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				DtdParser dtdParser = this.<>4__this;
				try
				{
					ConfiguredTaskAwaitable<DtdParser.Token>.ConfiguredTaskAwaiter awaiter;
					ConfiguredTaskAwaitable<Tuple<string, string>>.ConfiguredTaskAwaiter awaiter2;
					switch (num)
					{
					case 0:
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable<DtdParser.Token>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						break;
					case 1:
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable<DtdParser.Token>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_124;
					case 2:
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable<DtdParser.Token>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_263;
					case 3:
						awaiter2 = this.<>u__2;
						this.<>u__2 = default(ConfiguredTaskAwaitable<Tuple<string, string>>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_2E3;
					case 4:
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable<DtdParser.Token>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_381;
					case 5:
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable<DtdParser.Token>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_42C;
					case 6:
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable<DtdParser.Token>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_518;
					default:
						this.<isParamEntity>5__2 = false;
						this.<entity>5__3 = null;
						awaiter = dtdParser.GetTokenAsync(true).ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 0;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<DtdParser.Token>.ConfiguredTaskAwaiter, DtdParser.<ParseEntityDeclAsync>d__168>(ref awaiter, ref this);
							return;
						}
						break;
					}
					DtdParser.Token result = awaiter.GetResult();
					if (result == DtdParser.Token.Name)
					{
						goto IL_132;
					}
					if (result != DtdParser.Token.Percent)
					{
						goto IL_531;
					}
					this.<isParamEntity>5__2 = true;
					awaiter = dtdParser.GetTokenAsync(true).ConfigureAwait(false).GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						this.<>1__state = 1;
						this.<>u__1 = awaiter;
						this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<DtdParser.Token>.ConfiguredTaskAwaiter, DtdParser.<ParseEntityDeclAsync>d__168>(ref awaiter, ref this);
						return;
					}
					IL_124:
					if (awaiter.GetResult() != DtdParser.Token.Name)
					{
						goto IL_531;
					}
					IL_132:
					XmlQualifiedName nameQualified = dtdParser.GetNameQualified(false);
					this.<entity>5__3 = new SchemaEntity(nameQualified, this.<isParamEntity>5__2);
					this.<entity>5__3.BaseURI = dtdParser.BaseUriStr;
					this.<entity>5__3.DeclaredURI = ((dtdParser.externalDtdBaseUri.Length == 0) ? dtdParser.documentBaseUri : dtdParser.externalDtdBaseUri);
					if (this.<isParamEntity>5__2)
					{
						if (!dtdParser.schemaInfo.ParameterEntities.ContainsKey(nameQualified))
						{
							dtdParser.schemaInfo.ParameterEntities.Add(nameQualified, this.<entity>5__3);
						}
					}
					else if (!dtdParser.schemaInfo.GeneralEntities.ContainsKey(nameQualified))
					{
						dtdParser.schemaInfo.GeneralEntities.Add(nameQualified, this.<entity>5__3);
					}
					this.<entity>5__3.DeclaredInExternal = !dtdParser.ParsingInternalSubset;
					this.<entity>5__3.ParsingInProgress = true;
					awaiter = dtdParser.GetTokenAsync(true).ConfigureAwait(false).GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						this.<>1__state = 2;
						this.<>u__1 = awaiter;
						this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<DtdParser.Token>.ConfiguredTaskAwaiter, DtdParser.<ParseEntityDeclAsync>d__168>(ref awaiter, ref this);
						return;
					}
					IL_263:
					DtdParser.Token result2 = awaiter.GetResult();
					if (result2 - DtdParser.Token.PUBLIC > 1)
					{
						if (result2 != DtdParser.Token.Literal)
						{
							goto IL_531;
						}
						this.<entity>5__3.Text = dtdParser.GetValue();
						this.<entity>5__3.Line = dtdParser.literalLineInfo.lineNo;
						this.<entity>5__3.Pos = dtdParser.literalLineInfo.linePos;
						goto IL_4B9;
					}
					else
					{
						awaiter2 = dtdParser.ParseExternalIdAsync(result2, DtdParser.Token.EntityDecl).ConfigureAwait(false).GetAwaiter();
						if (!awaiter2.IsCompleted)
						{
							this.<>1__state = 3;
							this.<>u__2 = awaiter2;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<Tuple<string, string>>.ConfiguredTaskAwaiter, DtdParser.<ParseEntityDeclAsync>d__168>(ref awaiter2, ref this);
							return;
						}
					}
					IL_2E3:
					Tuple<string, string> result3 = awaiter2.GetResult();
					string item = result3.Item1;
					string item2 = result3.Item2;
					this.<entity>5__3.IsExternal = true;
					this.<entity>5__3.Url = item2;
					this.<entity>5__3.Pubid = item;
					awaiter = dtdParser.GetTokenAsync(false).ConfigureAwait(false).GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						this.<>1__state = 4;
						this.<>u__1 = awaiter;
						this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<DtdParser.Token>.ConfiguredTaskAwaiter, DtdParser.<ParseEntityDeclAsync>d__168>(ref awaiter, ref this);
						return;
					}
					IL_381:
					if (awaiter.GetResult() != DtdParser.Token.NData)
					{
						goto IL_4B9;
					}
					if (this.<isParamEntity>5__2)
					{
						dtdParser.ThrowUnexpectedToken(dtdParser.curPos - 5, ">");
					}
					if (!dtdParser.whitespaceSeen)
					{
						dtdParser.Throw(dtdParser.curPos - 5, "'{0}' is an unexpected token. Expecting white space.", "NDATA");
					}
					awaiter = dtdParser.GetTokenAsync(true).ConfigureAwait(false).GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						this.<>1__state = 5;
						this.<>u__1 = awaiter;
						this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<DtdParser.Token>.ConfiguredTaskAwaiter, DtdParser.<ParseEntityDeclAsync>d__168>(ref awaiter, ref this);
						return;
					}
					IL_42C:
					if (awaiter.GetResult() != DtdParser.Token.Name)
					{
						goto IL_531;
					}
					this.<entity>5__3.NData = dtdParser.GetNameQualified(false);
					string name = this.<entity>5__3.NData.Name;
					if (!dtdParser.schemaInfo.Notations.ContainsKey(name))
					{
						dtdParser.AddUndeclaredNotation(name);
					}
					IL_4B9:
					awaiter = dtdParser.GetTokenAsync(false).ConfigureAwait(false).GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						this.<>1__state = 6;
						this.<>u__1 = awaiter;
						this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<DtdParser.Token>.ConfiguredTaskAwaiter, DtdParser.<ParseEntityDeclAsync>d__168>(ref awaiter, ref this);
						return;
					}
					IL_518:
					if (awaiter.GetResult() == DtdParser.Token.GreaterThan)
					{
						this.<entity>5__3.ParsingInProgress = false;
						goto IL_559;
					}
					IL_531:
					dtdParser.OnUnexpectedError();
				}
				catch (Exception exception)
				{
					this.<>1__state = -2;
					this.<entity>5__3 = null;
					this.<>t__builder.SetException(exception);
					return;
				}
				IL_559:
				this.<>1__state = -2;
				this.<entity>5__3 = null;
				this.<>t__builder.SetResult();
			}

			// Token: 0x060013F4 RID: 5108 RVA: 0x0007A078 File Offset: 0x00078278
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x040011D4 RID: 4564
			public int <>1__state;

			// Token: 0x040011D5 RID: 4565
			public AsyncTaskMethodBuilder <>t__builder;

			// Token: 0x040011D6 RID: 4566
			public DtdParser <>4__this;

			// Token: 0x040011D7 RID: 4567
			private bool <isParamEntity>5__2;

			// Token: 0x040011D8 RID: 4568
			private SchemaEntity <entity>5__3;

			// Token: 0x040011D9 RID: 4569
			private ConfiguredTaskAwaitable<DtdParser.Token>.ConfiguredTaskAwaiter <>u__1;

			// Token: 0x040011DA RID: 4570
			private ConfiguredTaskAwaitable<Tuple<string, string>>.ConfiguredTaskAwaiter <>u__2;
		}

		// Token: 0x020001FF RID: 511
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <ParseNotationDeclAsync>d__169 : IAsyncStateMachine
		{
			// Token: 0x060013F5 RID: 5109 RVA: 0x0007A088 File Offset: 0x00078288
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				DtdParser dtdParser = this.<>4__this;
				try
				{
					ConfiguredTaskAwaitable<DtdParser.Token>.ConfiguredTaskAwaiter awaiter;
					ConfiguredTaskAwaitable<Tuple<string, string>>.ConfiguredTaskAwaiter awaiter2;
					switch (num)
					{
					case 0:
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable<DtdParser.Token>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						break;
					case 1:
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable<DtdParser.Token>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_19A;
					case 2:
						awaiter2 = this.<>u__2;
						this.<>u__2 = default(ConfiguredTaskAwaitable<Tuple<string, string>>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_212;
					case 3:
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable<DtdParser.Token>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_2B1;
					default:
						awaiter = dtdParser.GetTokenAsync(true).ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 0;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<DtdParser.Token>.ConfiguredTaskAwaiter, DtdParser.<ParseNotationDeclAsync>d__169>(ref awaiter, ref this);
							return;
						}
						break;
					}
					if (awaiter.GetResult() != DtdParser.Token.Name)
					{
						dtdParser.OnUnexpectedError();
					}
					XmlQualifiedName nameQualified = dtdParser.GetNameQualified(false);
					this.<notation>5__2 = null;
					if (!dtdParser.schemaInfo.Notations.ContainsKey(nameQualified.Name))
					{
						if (dtdParser.undeclaredNotations != null)
						{
							dtdParser.undeclaredNotations.Remove(nameQualified.Name);
						}
						this.<notation>5__2 = new SchemaNotation(nameQualified);
						dtdParser.schemaInfo.Notations.Add(this.<notation>5__2.Name.Name, this.<notation>5__2);
					}
					else if (dtdParser.validate)
					{
						dtdParser.SendValidationEvent(dtdParser.curPos - nameQualified.Name.Length, XmlSeverityType.Error, "The notation '{0}' has already been declared.", nameQualified.Name);
					}
					awaiter = dtdParser.GetTokenAsync(true).ConfigureAwait(false).GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						this.<>1__state = 1;
						this.<>u__1 = awaiter;
						this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<DtdParser.Token>.ConfiguredTaskAwaiter, DtdParser.<ParseNotationDeclAsync>d__169>(ref awaiter, ref this);
						return;
					}
					IL_19A:
					DtdParser.Token result = awaiter.GetResult();
					if (result != DtdParser.Token.SYSTEM && result != DtdParser.Token.PUBLIC)
					{
						dtdParser.OnUnexpectedError();
						goto IL_252;
					}
					awaiter2 = dtdParser.ParseExternalIdAsync(result, DtdParser.Token.NOTATION).ConfigureAwait(false).GetAwaiter();
					if (!awaiter2.IsCompleted)
					{
						this.<>1__state = 2;
						this.<>u__2 = awaiter2;
						this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<Tuple<string, string>>.ConfiguredTaskAwaiter, DtdParser.<ParseNotationDeclAsync>d__169>(ref awaiter2, ref this);
						return;
					}
					IL_212:
					Tuple<string, string> result2 = awaiter2.GetResult();
					string item = result2.Item1;
					string item2 = result2.Item2;
					if (this.<notation>5__2 != null)
					{
						this.<notation>5__2.SystemLiteral = item2;
						this.<notation>5__2.Pubid = item;
					}
					IL_252:
					awaiter = dtdParser.GetTokenAsync(false).ConfigureAwait(false).GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						this.<>1__state = 3;
						this.<>u__1 = awaiter;
						this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<DtdParser.Token>.ConfiguredTaskAwaiter, DtdParser.<ParseNotationDeclAsync>d__169>(ref awaiter, ref this);
						return;
					}
					IL_2B1:
					if (awaiter.GetResult() != DtdParser.Token.GreaterThan)
					{
						dtdParser.OnUnexpectedError();
					}
				}
				catch (Exception exception)
				{
					this.<>1__state = -2;
					this.<notation>5__2 = null;
					this.<>t__builder.SetException(exception);
					return;
				}
				this.<>1__state = -2;
				this.<notation>5__2 = null;
				this.<>t__builder.SetResult();
			}

			// Token: 0x060013F6 RID: 5110 RVA: 0x0007A3B0 File Offset: 0x000785B0
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x040011DB RID: 4571
			public int <>1__state;

			// Token: 0x040011DC RID: 4572
			public AsyncTaskMethodBuilder <>t__builder;

			// Token: 0x040011DD RID: 4573
			public DtdParser <>4__this;

			// Token: 0x040011DE RID: 4574
			private SchemaNotation <notation>5__2;

			// Token: 0x040011DF RID: 4575
			private ConfiguredTaskAwaitable<DtdParser.Token>.ConfiguredTaskAwaiter <>u__1;

			// Token: 0x040011E0 RID: 4576
			private ConfiguredTaskAwaitable<Tuple<string, string>>.ConfiguredTaskAwaiter <>u__2;
		}

		// Token: 0x02000200 RID: 512
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <ParseCommentAsync>d__170 : IAsyncStateMachine
		{
			// Token: 0x060013F7 RID: 5111 RVA: 0x0007A3C0 File Offset: 0x000785C0
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				DtdParser dtdParser = this.<>4__this;
				try
				{
					if (num > 1)
					{
						dtdParser.SaveParsingBuffer();
					}
					try
					{
						ConfiguredTaskAwaitable.ConfiguredTaskAwaiter awaiter;
						if (num != 0)
						{
							if (num != 1)
							{
								if (dtdParser.SaveInternalSubsetValue)
								{
									awaiter = dtdParser.readerAdapter.ParseCommentAsync(dtdParser.internalSubsetValueSb).ConfigureAwait(false).GetAwaiter();
									if (!awaiter.IsCompleted)
									{
										this.<>1__state = 0;
										this.<>u__1 = awaiter;
										this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, DtdParser.<ParseCommentAsync>d__170>(ref awaiter, ref this);
										return;
									}
									goto IL_96;
								}
								else
								{
									awaiter = dtdParser.readerAdapter.ParseCommentAsync(null).ConfigureAwait(false).GetAwaiter();
									if (!awaiter.IsCompleted)
									{
										this.<>1__state = 1;
										this.<>u__1 = awaiter;
										this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, DtdParser.<ParseCommentAsync>d__170>(ref awaiter, ref this);
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
							goto IL_11A;
						}
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						IL_96:
						awaiter.GetResult();
						dtdParser.internalSubsetValueSb.Append("-->");
						IL_11A:;
					}
					catch (XmlException ex)
					{
						if (!(ex.ResString == "Unexpected end of file while parsing {0} has occurred.") || dtdParser.currentEntityId == 0)
						{
							throw;
						}
						dtdParser.SendValidationEvent(XmlSeverityType.Error, "The parameter entity replacement text must nest properly within markup declarations.", null);
					}
					dtdParser.LoadParsingBuffer();
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

			// Token: 0x060013F8 RID: 5112 RVA: 0x0007A580 File Offset: 0x00078780
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x040011E1 RID: 4577
			public int <>1__state;

			// Token: 0x040011E2 RID: 4578
			public AsyncTaskMethodBuilder <>t__builder;

			// Token: 0x040011E3 RID: 4579
			public DtdParser <>4__this;

			// Token: 0x040011E4 RID: 4580
			private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter <>u__1;
		}

		// Token: 0x02000201 RID: 513
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <ParsePIAsync>d__171 : IAsyncStateMachine
		{
			// Token: 0x060013F9 RID: 5113 RVA: 0x0007A590 File Offset: 0x00078790
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				DtdParser dtdParser = this.<>4__this;
				try
				{
					ConfiguredTaskAwaitable.ConfiguredTaskAwaiter awaiter;
					if (num != 0)
					{
						if (num != 1)
						{
							dtdParser.SaveParsingBuffer();
							if (dtdParser.SaveInternalSubsetValue)
							{
								awaiter = dtdParser.readerAdapter.ParsePIAsync(dtdParser.internalSubsetValueSb).ConfigureAwait(false).GetAwaiter();
								if (!awaiter.IsCompleted)
								{
									this.<>1__state = 0;
									this.<>u__1 = awaiter;
									this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, DtdParser.<ParsePIAsync>d__171>(ref awaiter, ref this);
									return;
								}
								goto IL_91;
							}
							else
							{
								awaiter = dtdParser.readerAdapter.ParsePIAsync(null).ConfigureAwait(false).GetAwaiter();
								if (!awaiter.IsCompleted)
								{
									this.<>1__state = 1;
									this.<>u__1 = awaiter;
									this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, DtdParser.<ParsePIAsync>d__171>(ref awaiter, ref this);
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
						goto IL_112;
					}
					awaiter = this.<>u__1;
					this.<>u__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
					this.<>1__state = -1;
					IL_91:
					awaiter.GetResult();
					dtdParser.internalSubsetValueSb.Append("?>");
					IL_112:
					dtdParser.LoadParsingBuffer();
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

			// Token: 0x060013FA RID: 5114 RVA: 0x0007A700 File Offset: 0x00078900
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x040011E5 RID: 4581
			public int <>1__state;

			// Token: 0x040011E6 RID: 4582
			public AsyncTaskMethodBuilder <>t__builder;

			// Token: 0x040011E7 RID: 4583
			public DtdParser <>4__this;

			// Token: 0x040011E8 RID: 4584
			private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter <>u__1;
		}

		// Token: 0x02000202 RID: 514
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <ParseCondSectionAsync>d__172 : IAsyncStateMachine
		{
			// Token: 0x060013FB RID: 5115 RVA: 0x0007A710 File Offset: 0x00078910
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				DtdParser dtdParser = this.<>4__this;
				try
				{
					ConfiguredTaskAwaitable<DtdParser.Token>.ConfiguredTaskAwaiter awaiter;
					switch (num)
					{
					case 0:
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable<DtdParser.Token>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						break;
					case 1:
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable<DtdParser.Token>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_106;
					case 2:
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable<DtdParser.Token>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_224;
					case 3:
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable<DtdParser.Token>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_2BE;
					default:
						this.<csEntityId>5__2 = dtdParser.currentEntityId;
						awaiter = dtdParser.GetTokenAsync(false).ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 0;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<DtdParser.Token>.ConfiguredTaskAwaiter, DtdParser.<ParseCondSectionAsync>d__172>(ref awaiter, ref this);
							return;
						}
						break;
					}
					DtdParser.Token result = awaiter.GetResult();
					if (result != DtdParser.Token.IGNORE)
					{
						if (result != DtdParser.Token.INCLUDE)
						{
							goto IL_2F8;
						}
						awaiter = dtdParser.GetTokenAsync(false).ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 1;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<DtdParser.Token>.ConfiguredTaskAwaiter, DtdParser.<ParseCondSectionAsync>d__172>(ref awaiter, ref this);
							return;
						}
					}
					else
					{
						awaiter = dtdParser.GetTokenAsync(false).ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 2;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<DtdParser.Token>.ConfiguredTaskAwaiter, DtdParser.<ParseCondSectionAsync>d__172>(ref awaiter, ref this);
							return;
						}
						goto IL_224;
					}
					IL_106:
					if (awaiter.GetResult() == DtdParser.Token.LeftBracket)
					{
						if (dtdParser.validate && this.<csEntityId>5__2 != dtdParser.currentEntityId)
						{
							dtdParser.SendValidationEvent(dtdParser.curPos, XmlSeverityType.Error, "The parameter entity replacement text must nest properly within markup declarations.", string.Empty);
						}
						if (dtdParser.validate)
						{
							if (dtdParser.condSectionEntityIds == null)
							{
								dtdParser.condSectionEntityIds = new int[2];
							}
							else if (dtdParser.condSectionEntityIds.Length == dtdParser.condSectionDepth)
							{
								int[] array = new int[dtdParser.condSectionEntityIds.Length * 2];
								Array.Copy(dtdParser.condSectionEntityIds, 0, array, 0, dtdParser.condSectionEntityIds.Length);
								dtdParser.condSectionEntityIds = array;
							}
							dtdParser.condSectionEntityIds[dtdParser.condSectionDepth] = this.<csEntityId>5__2;
						}
						dtdParser.condSectionDepth++;
						goto IL_2FE;
					}
					goto IL_2F8;
					IL_224:
					if (awaiter.GetResult() != DtdParser.Token.LeftBracket)
					{
						goto IL_2F8;
					}
					if (dtdParser.validate && this.<csEntityId>5__2 != dtdParser.currentEntityId)
					{
						dtdParser.SendValidationEvent(dtdParser.curPos, XmlSeverityType.Error, "The parameter entity replacement text must nest properly within markup declarations.", string.Empty);
					}
					awaiter = dtdParser.GetTokenAsync(false).ConfigureAwait(false).GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						this.<>1__state = 3;
						this.<>u__1 = awaiter;
						this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<DtdParser.Token>.ConfiguredTaskAwaiter, DtdParser.<ParseCondSectionAsync>d__172>(ref awaiter, ref this);
						return;
					}
					IL_2BE:
					if (awaiter.GetResult() == DtdParser.Token.CondSectionEnd)
					{
						if (dtdParser.validate && this.<csEntityId>5__2 != dtdParser.currentEntityId)
						{
							dtdParser.SendValidationEvent(dtdParser.curPos, XmlSeverityType.Error, "The parameter entity replacement text must nest properly within markup declarations.", string.Empty);
							goto IL_2FE;
						}
						goto IL_2FE;
					}
					IL_2F8:
					dtdParser.OnUnexpectedError();
					IL_2FE:;
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

			// Token: 0x060013FC RID: 5116 RVA: 0x0007AA68 File Offset: 0x00078C68
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x040011E9 RID: 4585
			public int <>1__state;

			// Token: 0x040011EA RID: 4586
			public AsyncTaskMethodBuilder <>t__builder;

			// Token: 0x040011EB RID: 4587
			public DtdParser <>4__this;

			// Token: 0x040011EC RID: 4588
			private int <csEntityId>5__2;

			// Token: 0x040011ED RID: 4589
			private ConfiguredTaskAwaitable<DtdParser.Token>.ConfiguredTaskAwaiter <>u__1;
		}

		// Token: 0x02000203 RID: 515
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <ParseExternalIdAsync>d__173 : IAsyncStateMachine
		{
			// Token: 0x060013FD RID: 5117 RVA: 0x0007AA78 File Offset: 0x00078C78
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				DtdParser dtdParser = this.<>4__this;
				Tuple<string, string> result;
				try
				{
					ConfiguredTaskAwaitable<DtdParser.Token>.ConfiguredTaskAwaiter awaiter;
					switch (num)
					{
					case 0:
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable<DtdParser.Token>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						break;
					case 1:
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable<DtdParser.Token>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_26F;
					case 2:
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable<DtdParser.Token>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_367;
					default:
						this.<keywordLineInfo>5__4 = new LineInfo(dtdParser.LineNo, dtdParser.LinePos - 6);
						this.<publicId>5__2 = null;
						this.<systemId>5__3 = null;
						awaiter = dtdParser.GetTokenAsync(true).ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 0;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<DtdParser.Token>.ConfiguredTaskAwaiter, DtdParser.<ParseExternalIdAsync>d__173>(ref awaiter, ref this);
							return;
						}
						break;
					}
					if (awaiter.GetResult() != DtdParser.Token.Literal)
					{
						dtdParser.ThrowUnexpectedToken(dtdParser.curPos, "\"", "'");
					}
					if (this.idTokenType == DtdParser.Token.SYSTEM)
					{
						this.<systemId>5__3 = dtdParser.GetValue();
						if (this.<systemId>5__3.IndexOf('#') >= 0)
						{
							dtdParser.Throw(dtdParser.curPos - this.<systemId>5__3.Length - 1, "Fragment identifier '{0}' cannot be part of the system identifier '{1}'.", new string[]
							{
								this.<systemId>5__3.Substring(this.<systemId>5__3.IndexOf('#')),
								this.<systemId>5__3
							});
						}
						if (this.declType == DtdParser.Token.DOCTYPE && !dtdParser.freeFloatingDtd)
						{
							DtdParser dtdParser2 = dtdParser;
							dtdParser2.literalLineInfo.linePos = dtdParser2.literalLineInfo.linePos + 1;
							dtdParser.readerAdapter.OnSystemId(this.<systemId>5__3, this.<keywordLineInfo>5__4, dtdParser.literalLineInfo);
							goto IL_3D4;
						}
						goto IL_3D4;
					}
					else
					{
						this.<publicId>5__2 = dtdParser.GetValue();
						int num2;
						if ((num2 = dtdParser.xmlCharType.IsPublicId(this.<publicId>5__2)) >= 0)
						{
							dtdParser.ThrowInvalidChar(dtdParser.curPos - 1 - this.<publicId>5__2.Length + num2, this.<publicId>5__2, num2);
						}
						if (this.declType == DtdParser.Token.DOCTYPE && !dtdParser.freeFloatingDtd)
						{
							DtdParser dtdParser3 = dtdParser;
							dtdParser3.literalLineInfo.linePos = dtdParser3.literalLineInfo.linePos + 1;
							dtdParser.readerAdapter.OnPublicId(this.<publicId>5__2, this.<keywordLineInfo>5__4, dtdParser.literalLineInfo);
							awaiter = dtdParser.GetTokenAsync(false).ConfigureAwait(false).GetAwaiter();
							if (!awaiter.IsCompleted)
							{
								this.<>1__state = 1;
								this.<>u__1 = awaiter;
								this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<DtdParser.Token>.ConfiguredTaskAwaiter, DtdParser.<ParseExternalIdAsync>d__173>(ref awaiter, ref this);
								return;
							}
						}
						else
						{
							awaiter = dtdParser.GetTokenAsync(false).ConfigureAwait(false).GetAwaiter();
							if (!awaiter.IsCompleted)
							{
								this.<>1__state = 2;
								this.<>u__1 = awaiter;
								this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<DtdParser.Token>.ConfiguredTaskAwaiter, DtdParser.<ParseExternalIdAsync>d__173>(ref awaiter, ref this);
								return;
							}
							goto IL_367;
						}
					}
					IL_26F:
					if (awaiter.GetResult() == DtdParser.Token.Literal)
					{
						if (!dtdParser.whitespaceSeen)
						{
							dtdParser.Throw("'{0}' is an unexpected token. Expecting white space.", new string(dtdParser.literalQuoteChar, 1), dtdParser.literalLineInfo.lineNo, dtdParser.literalLineInfo.linePos);
						}
						this.<systemId>5__3 = dtdParser.GetValue();
						DtdParser dtdParser4 = dtdParser;
						dtdParser4.literalLineInfo.linePos = dtdParser4.literalLineInfo.linePos + 1;
						dtdParser.readerAdapter.OnSystemId(this.<systemId>5__3, this.<keywordLineInfo>5__4, dtdParser.literalLineInfo);
						goto IL_3D4;
					}
					dtdParser.ThrowUnexpectedToken(dtdParser.curPos, "\"", "'");
					goto IL_3D4;
					IL_367:
					if (awaiter.GetResult() == DtdParser.Token.Literal)
					{
						if (!dtdParser.whitespaceSeen)
						{
							dtdParser.Throw("'{0}' is an unexpected token. Expecting white space.", new string(dtdParser.literalQuoteChar, 1), dtdParser.literalLineInfo.lineNo, dtdParser.literalLineInfo.linePos);
						}
						this.<systemId>5__3 = dtdParser.GetValue();
					}
					else if (this.declType != DtdParser.Token.NOTATION)
					{
						dtdParser.ThrowUnexpectedToken(dtdParser.curPos, "\"", "'");
					}
					IL_3D4:
					result = new Tuple<string, string>(this.<publicId>5__2, this.<systemId>5__3);
				}
				catch (Exception exception)
				{
					this.<>1__state = -2;
					this.<publicId>5__2 = null;
					this.<systemId>5__3 = null;
					this.<>t__builder.SetException(exception);
					return;
				}
				this.<>1__state = -2;
				this.<publicId>5__2 = null;
				this.<systemId>5__3 = null;
				this.<>t__builder.SetResult(result);
			}

			// Token: 0x060013FE RID: 5118 RVA: 0x0007AED4 File Offset: 0x000790D4
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x040011EE RID: 4590
			public int <>1__state;

			// Token: 0x040011EF RID: 4591
			public AsyncTaskMethodBuilder<Tuple<string, string>> <>t__builder;

			// Token: 0x040011F0 RID: 4592
			public DtdParser <>4__this;

			// Token: 0x040011F1 RID: 4593
			public DtdParser.Token idTokenType;

			// Token: 0x040011F2 RID: 4594
			public DtdParser.Token declType;

			// Token: 0x040011F3 RID: 4595
			private string <publicId>5__2;

			// Token: 0x040011F4 RID: 4596
			private string <systemId>5__3;

			// Token: 0x040011F5 RID: 4597
			private LineInfo <keywordLineInfo>5__4;

			// Token: 0x040011F6 RID: 4598
			private ConfiguredTaskAwaitable<DtdParser.Token>.ConfiguredTaskAwaiter <>u__1;
		}

		// Token: 0x02000204 RID: 516
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <GetTokenAsync>d__174 : IAsyncStateMachine
		{
			// Token: 0x060013FF RID: 5119 RVA: 0x0007AEE4 File Offset: 0x000790E4
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				DtdParser dtdParser = this.<>4__this;
				DtdParser.Token result;
				try
				{
					ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter awaiter;
					ConfiguredTaskAwaitable<DtdParser.Token>.ConfiguredTaskAwaiter awaiter2;
					ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter awaiter3;
					switch (num)
					{
					case 0:
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_286;
					case 1:
						awaiter2 = this.<>u__2;
						this.<>u__2 = default(ConfiguredTaskAwaitable<DtdParser.Token>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_3CF;
					case 2:
						awaiter2 = this.<>u__2;
						this.<>u__2 = default(ConfiguredTaskAwaitable<DtdParser.Token>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_43D;
					case 3:
						awaiter2 = this.<>u__2;
						this.<>u__2 = default(ConfiguredTaskAwaitable<DtdParser.Token>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_4AB;
					case 4:
						awaiter2 = this.<>u__2;
						this.<>u__2 = default(ConfiguredTaskAwaitable<DtdParser.Token>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_519;
					case 5:
						awaiter2 = this.<>u__2;
						this.<>u__2 = default(ConfiguredTaskAwaitable<DtdParser.Token>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_587;
					case 6:
						awaiter2 = this.<>u__2;
						this.<>u__2 = default(ConfiguredTaskAwaitable<DtdParser.Token>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_601;
					case 7:
						awaiter2 = this.<>u__2;
						this.<>u__2 = default(ConfiguredTaskAwaitable<DtdParser.Token>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_66F;
					case 8:
						awaiter2 = this.<>u__2;
						this.<>u__2 = default(ConfiguredTaskAwaitable<DtdParser.Token>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_6DD;
					case 9:
						awaiter2 = this.<>u__2;
						this.<>u__2 = default(ConfiguredTaskAwaitable<DtdParser.Token>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_77C;
					case 10:
						awaiter2 = this.<>u__2;
						this.<>u__2 = default(ConfiguredTaskAwaitable<DtdParser.Token>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_7EB;
					case 11:
						awaiter2 = this.<>u__2;
						this.<>u__2 = default(ConfiguredTaskAwaitable<DtdParser.Token>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_87E;
					case 12:
						awaiter2 = this.<>u__2;
						this.<>u__2 = default(ConfiguredTaskAwaitable<DtdParser.Token>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_8F9;
					case 13:
						awaiter2 = this.<>u__2;
						this.<>u__2 = default(ConfiguredTaskAwaitable<DtdParser.Token>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_968;
					case 14:
						awaiter2 = this.<>u__2;
						this.<>u__2 = default(ConfiguredTaskAwaitable<DtdParser.Token>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_9D7;
					case 15:
						awaiter2 = this.<>u__2;
						this.<>u__2 = default(ConfiguredTaskAwaitable<DtdParser.Token>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_A46;
					case 16:
						awaiter2 = this.<>u__2;
						this.<>u__2 = default(ConfiguredTaskAwaitable<DtdParser.Token>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_AB5;
					case 17:
						awaiter2 = this.<>u__2;
						this.<>u__2 = default(ConfiguredTaskAwaitable<DtdParser.Token>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_B24;
					case 18:
						awaiter2 = this.<>u__2;
						this.<>u__2 = default(ConfiguredTaskAwaitable<DtdParser.Token>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_B93;
					case 19:
						awaiter2 = this.<>u__2;
						this.<>u__2 = default(ConfiguredTaskAwaitable<DtdParser.Token>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_C02;
					case 20:
						awaiter2 = this.<>u__2;
						this.<>u__2 = default(ConfiguredTaskAwaitable<DtdParser.Token>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_C7D;
					case 21:
						awaiter3 = this.<>u__3;
						this.<>u__3 = default(ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_D29;
					default:
						dtdParser.whitespaceSeen = false;
						break;
					}
					for (;;)
					{
						IL_73:
						char c = dtdParser.chars[dtdParser.curPos];
						if (c <= '\r')
						{
							if (c != '\0')
							{
								switch (c)
								{
								case '\t':
									goto IL_1BB;
								case '\n':
									dtdParser.whitespaceSeen = true;
									dtdParser.curPos++;
									dtdParser.readerAdapter.OnNewLine(dtdParser.curPos);
									continue;
								case '\r':
									dtdParser.whitespaceSeen = true;
									if (dtdParser.chars[dtdParser.curPos + 1] == '\n')
									{
										if (dtdParser.Normalize)
										{
											dtdParser.SaveParsingBuffer();
											IDtdParserAdapter readerAdapter = dtdParser.readerAdapter;
											int currentPosition = readerAdapter.CurrentPosition;
											readerAdapter.CurrentPosition = currentPosition + 1;
										}
										dtdParser.curPos += 2;
									}
									else
									{
										if (dtdParser.curPos + 1 >= dtdParser.charsUsed && !dtdParser.readerAdapter.IsEof)
										{
											goto IL_CB6;
										}
										dtdParser.chars[dtdParser.curPos] = '\n';
										dtdParser.curPos++;
									}
									dtdParser.readerAdapter.OnNewLine(dtdParser.curPos);
									continue;
								}
								break;
							}
							goto IL_C0;
						}
						else if (c != ' ')
						{
							if (c != '%')
							{
								break;
							}
							if (dtdParser.charsUsed - dtdParser.curPos < 2)
							{
								goto IL_CB6;
							}
							if (dtdParser.xmlCharType.IsWhiteSpace(dtdParser.chars[dtdParser.curPos + 1]))
							{
								break;
							}
							if (dtdParser.IgnoreEntityReferences)
							{
								dtdParser.curPos++;
								continue;
							}
							goto IL_222;
						}
						IL_1BB:
						dtdParser.whitespaceSeen = true;
						dtdParser.curPos++;
					}
					goto IL_293;
					IL_C0:
					if (dtdParser.curPos != dtdParser.charsUsed)
					{
						dtdParser.ThrowInvalidChar(dtdParser.chars, dtdParser.charsUsed, dtdParser.curPos);
						goto IL_CB6;
					}
					goto IL_CB6;
					IL_222:
					awaiter = dtdParser.HandleEntityReferenceAsync(true, false, false).ConfigureAwait(false).GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						this.<>1__state = 0;
						this.<>u__1 = awaiter;
						this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter, DtdParser.<GetTokenAsync>d__174>(ref awaiter, ref this);
						return;
					}
					goto IL_286;
					IL_293:
					if (this.needWhiteSpace && !dtdParser.whitespaceSeen && dtdParser.scanningFunction != DtdParser.ScanningFunction.ParamEntitySpace)
					{
						dtdParser.Throw(dtdParser.curPos, "'{0}' is an unexpected token. Expecting white space.", dtdParser.ParseUnexpectedToken(dtdParser.curPos));
					}
					dtdParser.tokenStartPos = dtdParser.curPos;
					for (;;)
					{
						switch (dtdParser.scanningFunction)
						{
						case DtdParser.ScanningFunction.SubsetContent:
							goto IL_4B8;
						case DtdParser.ScanningFunction.Name:
							goto IL_36E;
						case DtdParser.ScanningFunction.QName:
							goto IL_3DC;
						case DtdParser.ScanningFunction.Nmtoken:
							goto IL_44A;
						case DtdParser.ScanningFunction.Doctype1:
							goto IL_526;
						case DtdParser.ScanningFunction.Doctype2:
							goto IL_594;
						case DtdParser.ScanningFunction.Element1:
							goto IL_5A0;
						case DtdParser.ScanningFunction.Element2:
							goto IL_60E;
						case DtdParser.ScanningFunction.Element3:
							goto IL_67C;
						case DtdParser.ScanningFunction.Element4:
							goto IL_6EA;
						case DtdParser.ScanningFunction.Element5:
							goto IL_6F6;
						case DtdParser.ScanningFunction.Element6:
							goto IL_702;
						case DtdParser.ScanningFunction.Element7:
							goto IL_70E;
						case DtdParser.ScanningFunction.Attlist1:
							goto IL_71A;
						case DtdParser.ScanningFunction.Attlist2:
							goto IL_789;
						case DtdParser.ScanningFunction.Attlist3:
							goto IL_7F8;
						case DtdParser.ScanningFunction.Attlist4:
							goto IL_804;
						case DtdParser.ScanningFunction.Attlist5:
							goto IL_810;
						case DtdParser.ScanningFunction.Attlist6:
							goto IL_81C;
						case DtdParser.ScanningFunction.Attlist7:
							goto IL_88B;
						case DtdParser.ScanningFunction.Entity1:
							goto IL_A53;
						case DtdParser.ScanningFunction.Entity2:
							goto IL_AC2;
						case DtdParser.ScanningFunction.Entity3:
							goto IL_B31;
						case DtdParser.ScanningFunction.Notation1:
							goto IL_897;
						case DtdParser.ScanningFunction.CondSection1:
							goto IL_BA0;
						case DtdParser.ScanningFunction.CondSection2:
							goto IL_C0F;
						case DtdParser.ScanningFunction.CondSection3:
							goto IL_C1B;
						case DtdParser.ScanningFunction.SystemId:
							goto IL_906;
						case DtdParser.ScanningFunction.PublicId1:
							goto IL_975;
						case DtdParser.ScanningFunction.PublicId2:
							goto IL_9E4;
						case DtdParser.ScanningFunction.ClosingTag:
							goto IL_C8A;
						case DtdParser.ScanningFunction.ParamEntitySpace:
							dtdParser.whitespaceSeen = true;
							dtdParser.scanningFunction = dtdParser.savedScanningFunction;
							continue;
						}
						break;
					}
					goto IL_CAE;
					IL_36E:
					awaiter2 = dtdParser.ScanNameExpectedAsync().ConfigureAwait(false).GetAwaiter();
					if (!awaiter2.IsCompleted)
					{
						this.<>1__state = 1;
						this.<>u__2 = awaiter2;
						this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<DtdParser.Token>.ConfiguredTaskAwaiter, DtdParser.<GetTokenAsync>d__174>(ref awaiter2, ref this);
						return;
					}
					goto IL_3CF;
					IL_3DC:
					awaiter2 = dtdParser.ScanQNameExpectedAsync().ConfigureAwait(false).GetAwaiter();
					if (!awaiter2.IsCompleted)
					{
						this.<>1__state = 2;
						this.<>u__2 = awaiter2;
						this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<DtdParser.Token>.ConfiguredTaskAwaiter, DtdParser.<GetTokenAsync>d__174>(ref awaiter2, ref this);
						return;
					}
					goto IL_43D;
					IL_44A:
					awaiter2 = dtdParser.ScanNmtokenExpectedAsync().ConfigureAwait(false).GetAwaiter();
					if (!awaiter2.IsCompleted)
					{
						this.<>1__state = 3;
						this.<>u__2 = awaiter2;
						this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<DtdParser.Token>.ConfiguredTaskAwaiter, DtdParser.<GetTokenAsync>d__174>(ref awaiter2, ref this);
						return;
					}
					goto IL_4AB;
					IL_4B8:
					awaiter2 = dtdParser.ScanSubsetContentAsync().ConfigureAwait(false).GetAwaiter();
					if (!awaiter2.IsCompleted)
					{
						this.<>1__state = 4;
						this.<>u__2 = awaiter2;
						this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<DtdParser.Token>.ConfiguredTaskAwaiter, DtdParser.<GetTokenAsync>d__174>(ref awaiter2, ref this);
						return;
					}
					goto IL_519;
					IL_526:
					awaiter2 = dtdParser.ScanDoctype1Async().ConfigureAwait(false).GetAwaiter();
					if (!awaiter2.IsCompleted)
					{
						this.<>1__state = 5;
						this.<>u__2 = awaiter2;
						this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<DtdParser.Token>.ConfiguredTaskAwaiter, DtdParser.<GetTokenAsync>d__174>(ref awaiter2, ref this);
						return;
					}
					goto IL_587;
					IL_594:
					result = dtdParser.ScanDoctype2();
					goto IL_D84;
					IL_5A0:
					awaiter2 = dtdParser.ScanElement1Async().ConfigureAwait(false).GetAwaiter();
					if (!awaiter2.IsCompleted)
					{
						this.<>1__state = 6;
						this.<>u__2 = awaiter2;
						this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<DtdParser.Token>.ConfiguredTaskAwaiter, DtdParser.<GetTokenAsync>d__174>(ref awaiter2, ref this);
						return;
					}
					goto IL_601;
					IL_60E:
					awaiter2 = dtdParser.ScanElement2Async().ConfigureAwait(false).GetAwaiter();
					if (!awaiter2.IsCompleted)
					{
						this.<>1__state = 7;
						this.<>u__2 = awaiter2;
						this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<DtdParser.Token>.ConfiguredTaskAwaiter, DtdParser.<GetTokenAsync>d__174>(ref awaiter2, ref this);
						return;
					}
					goto IL_66F;
					IL_67C:
					awaiter2 = dtdParser.ScanElement3Async().ConfigureAwait(false).GetAwaiter();
					if (!awaiter2.IsCompleted)
					{
						this.<>1__state = 8;
						this.<>u__2 = awaiter2;
						this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<DtdParser.Token>.ConfiguredTaskAwaiter, DtdParser.<GetTokenAsync>d__174>(ref awaiter2, ref this);
						return;
					}
					goto IL_6DD;
					IL_6EA:
					result = dtdParser.ScanElement4();
					goto IL_D84;
					IL_6F6:
					result = dtdParser.ScanElement5();
					goto IL_D84;
					IL_702:
					result = dtdParser.ScanElement6();
					goto IL_D84;
					IL_70E:
					result = dtdParser.ScanElement7();
					goto IL_D84;
					IL_71A:
					awaiter2 = dtdParser.ScanAttlist1Async().ConfigureAwait(false).GetAwaiter();
					if (!awaiter2.IsCompleted)
					{
						this.<>1__state = 9;
						this.<>u__2 = awaiter2;
						this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<DtdParser.Token>.ConfiguredTaskAwaiter, DtdParser.<GetTokenAsync>d__174>(ref awaiter2, ref this);
						return;
					}
					goto IL_77C;
					IL_789:
					awaiter2 = dtdParser.ScanAttlist2Async().ConfigureAwait(false).GetAwaiter();
					if (!awaiter2.IsCompleted)
					{
						this.<>1__state = 10;
						this.<>u__2 = awaiter2;
						this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<DtdParser.Token>.ConfiguredTaskAwaiter, DtdParser.<GetTokenAsync>d__174>(ref awaiter2, ref this);
						return;
					}
					goto IL_7EB;
					IL_7F8:
					result = dtdParser.ScanAttlist3();
					goto IL_D84;
					IL_804:
					result = dtdParser.ScanAttlist4();
					goto IL_D84;
					IL_810:
					result = dtdParser.ScanAttlist5();
					goto IL_D84;
					IL_81C:
					awaiter2 = dtdParser.ScanAttlist6Async().ConfigureAwait(false).GetAwaiter();
					if (!awaiter2.IsCompleted)
					{
						this.<>1__state = 11;
						this.<>u__2 = awaiter2;
						this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<DtdParser.Token>.ConfiguredTaskAwaiter, DtdParser.<GetTokenAsync>d__174>(ref awaiter2, ref this);
						return;
					}
					goto IL_87E;
					IL_88B:
					result = dtdParser.ScanAttlist7();
					goto IL_D84;
					IL_897:
					awaiter2 = dtdParser.ScanNotation1Async().ConfigureAwait(false).GetAwaiter();
					if (!awaiter2.IsCompleted)
					{
						this.<>1__state = 12;
						this.<>u__2 = awaiter2;
						this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<DtdParser.Token>.ConfiguredTaskAwaiter, DtdParser.<GetTokenAsync>d__174>(ref awaiter2, ref this);
						return;
					}
					goto IL_8F9;
					IL_906:
					awaiter2 = dtdParser.ScanSystemIdAsync().ConfigureAwait(false).GetAwaiter();
					if (!awaiter2.IsCompleted)
					{
						this.<>1__state = 13;
						this.<>u__2 = awaiter2;
						this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<DtdParser.Token>.ConfiguredTaskAwaiter, DtdParser.<GetTokenAsync>d__174>(ref awaiter2, ref this);
						return;
					}
					goto IL_968;
					IL_975:
					awaiter2 = dtdParser.ScanPublicId1Async().ConfigureAwait(false).GetAwaiter();
					if (!awaiter2.IsCompleted)
					{
						this.<>1__state = 14;
						this.<>u__2 = awaiter2;
						this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<DtdParser.Token>.ConfiguredTaskAwaiter, DtdParser.<GetTokenAsync>d__174>(ref awaiter2, ref this);
						return;
					}
					goto IL_9D7;
					IL_9E4:
					awaiter2 = dtdParser.ScanPublicId2Async().ConfigureAwait(false).GetAwaiter();
					if (!awaiter2.IsCompleted)
					{
						this.<>1__state = 15;
						this.<>u__2 = awaiter2;
						this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<DtdParser.Token>.ConfiguredTaskAwaiter, DtdParser.<GetTokenAsync>d__174>(ref awaiter2, ref this);
						return;
					}
					goto IL_A46;
					IL_A53:
					awaiter2 = dtdParser.ScanEntity1Async().ConfigureAwait(false).GetAwaiter();
					if (!awaiter2.IsCompleted)
					{
						this.<>1__state = 16;
						this.<>u__2 = awaiter2;
						this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<DtdParser.Token>.ConfiguredTaskAwaiter, DtdParser.<GetTokenAsync>d__174>(ref awaiter2, ref this);
						return;
					}
					goto IL_AB5;
					IL_AC2:
					awaiter2 = dtdParser.ScanEntity2Async().ConfigureAwait(false).GetAwaiter();
					if (!awaiter2.IsCompleted)
					{
						this.<>1__state = 17;
						this.<>u__2 = awaiter2;
						this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<DtdParser.Token>.ConfiguredTaskAwaiter, DtdParser.<GetTokenAsync>d__174>(ref awaiter2, ref this);
						return;
					}
					goto IL_B24;
					IL_B31:
					awaiter2 = dtdParser.ScanEntity3Async().ConfigureAwait(false).GetAwaiter();
					if (!awaiter2.IsCompleted)
					{
						this.<>1__state = 18;
						this.<>u__2 = awaiter2;
						this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<DtdParser.Token>.ConfiguredTaskAwaiter, DtdParser.<GetTokenAsync>d__174>(ref awaiter2, ref this);
						return;
					}
					goto IL_B93;
					IL_BA0:
					awaiter2 = dtdParser.ScanCondSection1Async().ConfigureAwait(false).GetAwaiter();
					if (!awaiter2.IsCompleted)
					{
						this.<>1__state = 19;
						this.<>u__2 = awaiter2;
						this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<DtdParser.Token>.ConfiguredTaskAwaiter, DtdParser.<GetTokenAsync>d__174>(ref awaiter2, ref this);
						return;
					}
					goto IL_C02;
					IL_C0F:
					result = dtdParser.ScanCondSection2();
					goto IL_D84;
					IL_C1B:
					awaiter2 = dtdParser.ScanCondSection3Async().ConfigureAwait(false).GetAwaiter();
					if (!awaiter2.IsCompleted)
					{
						this.<>1__state = 20;
						this.<>u__2 = awaiter2;
						this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<DtdParser.Token>.ConfiguredTaskAwaiter, DtdParser.<GetTokenAsync>d__174>(ref awaiter2, ref this);
						return;
					}
					goto IL_C7D;
					IL_C8A:
					result = dtdParser.ScanClosingTag();
					goto IL_D84;
					IL_CAE:
					result = DtdParser.Token.None;
					goto IL_D84;
					IL_CB6:
					bool flag = dtdParser.readerAdapter.IsEof;
					if (flag)
					{
						goto IL_D35;
					}
					awaiter3 = dtdParser.ReadDataAsync().ConfigureAwait(false).GetAwaiter();
					if (!awaiter3.IsCompleted)
					{
						this.<>1__state = 21;
						this.<>u__3 = awaiter3;
						this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter, DtdParser.<GetTokenAsync>d__174>(ref awaiter3, ref this);
						return;
					}
					goto IL_D29;
					IL_286:
					awaiter.GetResult();
					goto IL_73;
					IL_3CF:
					result = awaiter2.GetResult();
					goto IL_D84;
					IL_43D:
					result = awaiter2.GetResult();
					goto IL_D84;
					IL_4AB:
					result = awaiter2.GetResult();
					goto IL_D84;
					IL_519:
					result = awaiter2.GetResult();
					goto IL_D84;
					IL_587:
					result = awaiter2.GetResult();
					goto IL_D84;
					IL_601:
					result = awaiter2.GetResult();
					goto IL_D84;
					IL_66F:
					result = awaiter2.GetResult();
					goto IL_D84;
					IL_6DD:
					result = awaiter2.GetResult();
					goto IL_D84;
					IL_77C:
					result = awaiter2.GetResult();
					goto IL_D84;
					IL_7EB:
					result = awaiter2.GetResult();
					goto IL_D84;
					IL_87E:
					result = awaiter2.GetResult();
					goto IL_D84;
					IL_8F9:
					result = awaiter2.GetResult();
					goto IL_D84;
					IL_968:
					result = awaiter2.GetResult();
					goto IL_D84;
					IL_9D7:
					result = awaiter2.GetResult();
					goto IL_D84;
					IL_A46:
					result = awaiter2.GetResult();
					goto IL_D84;
					IL_AB5:
					result = awaiter2.GetResult();
					goto IL_D84;
					IL_B24:
					result = awaiter2.GetResult();
					goto IL_D84;
					IL_B93:
					result = awaiter2.GetResult();
					goto IL_D84;
					IL_C02:
					result = awaiter2.GetResult();
					goto IL_D84;
					IL_C7D:
					result = awaiter2.GetResult();
					goto IL_D84;
					IL_D29:
					flag = (awaiter3.GetResult() == 0);
					IL_D35:
					if (!flag || dtdParser.HandleEntityEnd(false))
					{
						goto IL_73;
					}
					if (dtdParser.scanningFunction != DtdParser.ScanningFunction.SubsetContent)
					{
						dtdParser.Throw(dtdParser.curPos, "Incomplete DTD content.");
						goto IL_73;
					}
					result = DtdParser.Token.Eof;
				}
				catch (Exception exception)
				{
					this.<>1__state = -2;
					this.<>t__builder.SetException(exception);
					return;
				}
				IL_D84:
				this.<>1__state = -2;
				this.<>t__builder.SetResult(result);
			}

			// Token: 0x06001400 RID: 5120 RVA: 0x0007BCA8 File Offset: 0x00079EA8
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x040011F7 RID: 4599
			public int <>1__state;

			// Token: 0x040011F8 RID: 4600
			public AsyncTaskMethodBuilder<DtdParser.Token> <>t__builder;

			// Token: 0x040011F9 RID: 4601
			public DtdParser <>4__this;

			// Token: 0x040011FA RID: 4602
			public bool needWhiteSpace;

			// Token: 0x040011FB RID: 4603
			private ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter <>u__1;

			// Token: 0x040011FC RID: 4604
			private ConfiguredTaskAwaitable<DtdParser.Token>.ConfiguredTaskAwaiter <>u__2;

			// Token: 0x040011FD RID: 4605
			private ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter <>u__3;
		}

		// Token: 0x02000205 RID: 517
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <ScanSubsetContentAsync>d__175 : IAsyncStateMachine
		{
			// Token: 0x06001401 RID: 5121 RVA: 0x0007BCB8 File Offset: 0x00079EB8
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				DtdParser dtdParser = this.<>4__this;
				DtdParser.Token result;
				try
				{
					ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter awaiter;
					if (num == 0)
					{
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_5C6;
					}
					IL_14:
					char c = dtdParser.chars[dtdParser.curPos];
					if (c != '<')
					{
						if (c == ']')
						{
							if (dtdParser.charsUsed - dtdParser.curPos < 2 && !dtdParser.readerAdapter.IsEof)
							{
								goto IL_568;
							}
							if (dtdParser.chars[dtdParser.curPos + 1] != ']')
							{
								dtdParser.curPos++;
								dtdParser.scanningFunction = DtdParser.ScanningFunction.ClosingTag;
								result = DtdParser.Token.RightBracket;
								goto IL_601;
							}
							if (dtdParser.charsUsed - dtdParser.curPos < 3 && !dtdParser.readerAdapter.IsEof)
							{
								goto IL_568;
							}
							if (dtdParser.chars[dtdParser.curPos + 1] == ']' && dtdParser.chars[dtdParser.curPos + 2] == '>')
							{
								dtdParser.curPos += 3;
								result = DtdParser.Token.CondSectionEnd;
								goto IL_601;
							}
						}
						if (dtdParser.charsUsed - dtdParser.curPos != 0)
						{
							dtdParser.Throw(dtdParser.curPos, "Expected DTD markup was not found.");
						}
					}
					else
					{
						char c2 = dtdParser.chars[dtdParser.curPos + 1];
						if (c2 != '!')
						{
							if (c2 == '?')
							{
								dtdParser.curPos += 2;
								result = DtdParser.Token.PI;
								goto IL_601;
							}
							if (dtdParser.charsUsed - dtdParser.curPos >= 2)
							{
								dtdParser.Throw(dtdParser.curPos, "Expected DTD markup was not found.");
								result = DtdParser.Token.None;
								goto IL_601;
							}
						}
						else
						{
							char c3 = dtdParser.chars[dtdParser.curPos + 2];
							if (c3 <= 'A')
							{
								if (c3 != '-')
								{
									if (c3 == 'A')
									{
										if (dtdParser.charsUsed - dtdParser.curPos >= 9)
										{
											if (dtdParser.chars[dtdParser.curPos + 3] != 'T' || dtdParser.chars[dtdParser.curPos + 4] != 'T' || dtdParser.chars[dtdParser.curPos + 5] != 'L' || dtdParser.chars[dtdParser.curPos + 6] != 'I' || dtdParser.chars[dtdParser.curPos + 7] != 'S' || dtdParser.chars[dtdParser.curPos + 8] != 'T')
											{
												dtdParser.Throw(dtdParser.curPos, "Expected DTD markup was not found.");
											}
											dtdParser.curPos += 9;
											dtdParser.scanningFunction = DtdParser.ScanningFunction.QName;
											dtdParser.nextScaningFunction = DtdParser.ScanningFunction.Attlist1;
											result = DtdParser.Token.AttlistDecl;
											goto IL_601;
										}
										goto IL_568;
									}
								}
								else
								{
									if (dtdParser.chars[dtdParser.curPos + 3] == '-')
									{
										dtdParser.curPos += 4;
										result = DtdParser.Token.Comment;
										goto IL_601;
									}
									if (dtdParser.charsUsed - dtdParser.curPos >= 4)
									{
										dtdParser.Throw(dtdParser.curPos, "Expected DTD markup was not found.");
										goto IL_568;
									}
									goto IL_568;
								}
							}
							else if (c3 != 'E')
							{
								if (c3 != 'N')
								{
									if (c3 == '[')
									{
										dtdParser.curPos += 3;
										dtdParser.scanningFunction = DtdParser.ScanningFunction.CondSection1;
										result = DtdParser.Token.CondSectionStart;
										goto IL_601;
									}
								}
								else
								{
									if (dtdParser.charsUsed - dtdParser.curPos >= 10)
									{
										if (dtdParser.chars[dtdParser.curPos + 3] != 'O' || dtdParser.chars[dtdParser.curPos + 4] != 'T' || dtdParser.chars[dtdParser.curPos + 5] != 'A' || dtdParser.chars[dtdParser.curPos + 6] != 'T' || dtdParser.chars[dtdParser.curPos + 7] != 'I' || dtdParser.chars[dtdParser.curPos + 8] != 'O' || dtdParser.chars[dtdParser.curPos + 9] != 'N')
										{
											dtdParser.Throw(dtdParser.curPos, "Expected DTD markup was not found.");
										}
										dtdParser.curPos += 10;
										dtdParser.scanningFunction = DtdParser.ScanningFunction.Name;
										dtdParser.nextScaningFunction = DtdParser.ScanningFunction.Notation1;
										result = DtdParser.Token.NotationDecl;
										goto IL_601;
									}
									goto IL_568;
								}
							}
							else if (dtdParser.chars[dtdParser.curPos + 3] == 'L')
							{
								if (dtdParser.charsUsed - dtdParser.curPos >= 9)
								{
									if (dtdParser.chars[dtdParser.curPos + 4] != 'E' || dtdParser.chars[dtdParser.curPos + 5] != 'M' || dtdParser.chars[dtdParser.curPos + 6] != 'E' || dtdParser.chars[dtdParser.curPos + 7] != 'N' || dtdParser.chars[dtdParser.curPos + 8] != 'T')
									{
										dtdParser.Throw(dtdParser.curPos, "Expected DTD markup was not found.");
									}
									dtdParser.curPos += 9;
									dtdParser.scanningFunction = DtdParser.ScanningFunction.QName;
									dtdParser.nextScaningFunction = DtdParser.ScanningFunction.Element1;
									result = DtdParser.Token.ElementDecl;
									goto IL_601;
								}
								goto IL_568;
							}
							else if (dtdParser.chars[dtdParser.curPos + 3] == 'N')
							{
								if (dtdParser.charsUsed - dtdParser.curPos >= 8)
								{
									if (dtdParser.chars[dtdParser.curPos + 4] != 'T' || dtdParser.chars[dtdParser.curPos + 5] != 'I' || dtdParser.chars[dtdParser.curPos + 6] != 'T' || dtdParser.chars[dtdParser.curPos + 7] != 'Y')
									{
										dtdParser.Throw(dtdParser.curPos, "Expected DTD markup was not found.");
									}
									dtdParser.curPos += 8;
									dtdParser.scanningFunction = DtdParser.ScanningFunction.Entity1;
									result = DtdParser.Token.EntityDecl;
									goto IL_601;
								}
								goto IL_568;
							}
							else
							{
								if (dtdParser.charsUsed - dtdParser.curPos >= 4)
								{
									dtdParser.Throw(dtdParser.curPos, "Expected DTD markup was not found.");
									result = DtdParser.Token.None;
									goto IL_601;
								}
								goto IL_568;
							}
							if (dtdParser.charsUsed - dtdParser.curPos >= 3)
							{
								dtdParser.Throw(dtdParser.curPos + 2, "Expected DTD markup was not found.");
							}
						}
					}
					IL_568:
					awaiter = dtdParser.ReadDataAsync().ConfigureAwait(false).GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						this.<>1__state = 0;
						this.<>u__1 = awaiter;
						this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter, DtdParser.<ScanSubsetContentAsync>d__175>(ref awaiter, ref this);
						return;
					}
					IL_5C6:
					if (awaiter.GetResult() == 0)
					{
						dtdParser.Throw(dtdParser.charsUsed, "Incomplete DTD content.");
						goto IL_14;
					}
					goto IL_14;
				}
				catch (Exception exception)
				{
					this.<>1__state = -2;
					this.<>t__builder.SetException(exception);
					return;
				}
				IL_601:
				this.<>1__state = -2;
				this.<>t__builder.SetResult(result);
			}

			// Token: 0x06001402 RID: 5122 RVA: 0x0007C2F8 File Offset: 0x0007A4F8
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x040011FE RID: 4606
			public int <>1__state;

			// Token: 0x040011FF RID: 4607
			public AsyncTaskMethodBuilder<DtdParser.Token> <>t__builder;

			// Token: 0x04001200 RID: 4608
			public DtdParser <>4__this;

			// Token: 0x04001201 RID: 4609
			private ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter <>u__1;
		}

		// Token: 0x02000206 RID: 518
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <ScanNameExpectedAsync>d__176 : IAsyncStateMachine
		{
			// Token: 0x06001403 RID: 5123 RVA: 0x0007C308 File Offset: 0x0007A508
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				DtdParser dtdParser = this.<>4__this;
				DtdParser.Token result;
				try
				{
					ConfiguredTaskAwaitable.ConfiguredTaskAwaiter awaiter;
					if (num != 0)
					{
						awaiter = dtdParser.ScanNameAsync().ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 0;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, DtdParser.<ScanNameExpectedAsync>d__176>(ref awaiter, ref this);
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
					dtdParser.scanningFunction = dtdParser.nextScaningFunction;
					result = DtdParser.Token.Name;
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

			// Token: 0x06001404 RID: 5124 RVA: 0x0007C3D8 File Offset: 0x0007A5D8
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04001202 RID: 4610
			public int <>1__state;

			// Token: 0x04001203 RID: 4611
			public AsyncTaskMethodBuilder<DtdParser.Token> <>t__builder;

			// Token: 0x04001204 RID: 4612
			public DtdParser <>4__this;

			// Token: 0x04001205 RID: 4613
			private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter <>u__1;
		}

		// Token: 0x02000207 RID: 519
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <ScanQNameExpectedAsync>d__177 : IAsyncStateMachine
		{
			// Token: 0x06001405 RID: 5125 RVA: 0x0007C3E8 File Offset: 0x0007A5E8
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				DtdParser dtdParser = this.<>4__this;
				DtdParser.Token result;
				try
				{
					ConfiguredTaskAwaitable.ConfiguredTaskAwaiter awaiter;
					if (num != 0)
					{
						awaiter = dtdParser.ScanQNameAsync().ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 0;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, DtdParser.<ScanQNameExpectedAsync>d__177>(ref awaiter, ref this);
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
					dtdParser.scanningFunction = dtdParser.nextScaningFunction;
					result = DtdParser.Token.QName;
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

			// Token: 0x06001406 RID: 5126 RVA: 0x0007C4B8 File Offset: 0x0007A6B8
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04001206 RID: 4614
			public int <>1__state;

			// Token: 0x04001207 RID: 4615
			public AsyncTaskMethodBuilder<DtdParser.Token> <>t__builder;

			// Token: 0x04001208 RID: 4616
			public DtdParser <>4__this;

			// Token: 0x04001209 RID: 4617
			private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter <>u__1;
		}

		// Token: 0x02000208 RID: 520
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <ScanNmtokenExpectedAsync>d__178 : IAsyncStateMachine
		{
			// Token: 0x06001407 RID: 5127 RVA: 0x0007C4C8 File Offset: 0x0007A6C8
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				DtdParser dtdParser = this.<>4__this;
				DtdParser.Token result;
				try
				{
					ConfiguredTaskAwaitable.ConfiguredTaskAwaiter awaiter;
					if (num != 0)
					{
						awaiter = dtdParser.ScanNmtokenAsync().ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 0;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, DtdParser.<ScanNmtokenExpectedAsync>d__178>(ref awaiter, ref this);
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
					dtdParser.scanningFunction = dtdParser.nextScaningFunction;
					result = DtdParser.Token.Nmtoken;
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

			// Token: 0x06001408 RID: 5128 RVA: 0x0007C598 File Offset: 0x0007A798
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x0400120A RID: 4618
			public int <>1__state;

			// Token: 0x0400120B RID: 4619
			public AsyncTaskMethodBuilder<DtdParser.Token> <>t__builder;

			// Token: 0x0400120C RID: 4620
			public DtdParser <>4__this;

			// Token: 0x0400120D RID: 4621
			private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter <>u__1;
		}

		// Token: 0x02000209 RID: 521
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <ScanDoctype1Async>d__179 : IAsyncStateMachine
		{
			// Token: 0x06001409 RID: 5129 RVA: 0x0007C5A8 File Offset: 0x0007A7A8
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				DtdParser dtdParser = this.<>4__this;
				DtdParser.Token result;
				try
				{
					ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter awaiter;
					if (num != 0)
					{
						if (num != 1)
						{
							char c = dtdParser.chars[dtdParser.curPos];
							if (c <= 'P')
							{
								if (c == '>')
								{
									dtdParser.curPos++;
									dtdParser.scanningFunction = DtdParser.ScanningFunction.SubsetContent;
									result = DtdParser.Token.GreaterThan;
									goto IL_1D9;
								}
								if (c == 'P')
								{
									awaiter = dtdParser.EatPublicKeywordAsync().ConfigureAwait(false).GetAwaiter();
									if (!awaiter.IsCompleted)
									{
										this.<>1__state = 0;
										this.<>u__1 = awaiter;
										this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter, DtdParser.<ScanDoctype1Async>d__179>(ref awaiter, ref this);
										return;
									}
									goto IL_B6;
								}
							}
							else if (c != 'S')
							{
								if (c == '[')
								{
									dtdParser.curPos++;
									dtdParser.scanningFunction = DtdParser.ScanningFunction.SubsetContent;
									result = DtdParser.Token.LeftBracket;
									goto IL_1D9;
								}
							}
							else
							{
								awaiter = dtdParser.EatSystemKeywordAsync().ConfigureAwait(false).GetAwaiter();
								if (!awaiter.IsCompleted)
								{
									this.<>1__state = 1;
									this.<>u__1 = awaiter;
									this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter, DtdParser.<ScanDoctype1Async>d__179>(ref awaiter, ref this);
									return;
								}
								goto IL_148;
							}
							dtdParser.Throw(dtdParser.curPos, "Expecting external ID, '[' or '>'.");
							result = DtdParser.Token.None;
							goto IL_1D9;
						}
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						IL_148:
						if (!awaiter.GetResult())
						{
							dtdParser.Throw(dtdParser.curPos, "Expecting external ID, '[' or '>'.");
						}
						dtdParser.nextScaningFunction = DtdParser.ScanningFunction.Doctype2;
						dtdParser.scanningFunction = DtdParser.ScanningFunction.SystemId;
						result = DtdParser.Token.SYSTEM;
						goto IL_1D9;
					}
					awaiter = this.<>u__1;
					this.<>u__1 = default(ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter);
					this.<>1__state = -1;
					IL_B6:
					if (!awaiter.GetResult())
					{
						dtdParser.Throw(dtdParser.curPos, "Expecting external ID, '[' or '>'.");
					}
					dtdParser.nextScaningFunction = DtdParser.ScanningFunction.Doctype2;
					dtdParser.scanningFunction = DtdParser.ScanningFunction.PublicId1;
					result = DtdParser.Token.PUBLIC;
				}
				catch (Exception exception)
				{
					this.<>1__state = -2;
					this.<>t__builder.SetException(exception);
					return;
				}
				IL_1D9:
				this.<>1__state = -2;
				this.<>t__builder.SetResult(result);
			}

			// Token: 0x0600140A RID: 5130 RVA: 0x0007C7C0 File Offset: 0x0007A9C0
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x0400120E RID: 4622
			public int <>1__state;

			// Token: 0x0400120F RID: 4623
			public AsyncTaskMethodBuilder<DtdParser.Token> <>t__builder;

			// Token: 0x04001210 RID: 4624
			public DtdParser <>4__this;

			// Token: 0x04001211 RID: 4625
			private ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter <>u__1;
		}

		// Token: 0x0200020A RID: 522
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <ScanElement1Async>d__180 : IAsyncStateMachine
		{
			// Token: 0x0600140B RID: 5131 RVA: 0x0007C7D0 File Offset: 0x0007A9D0
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				DtdParser dtdParser = this.<>4__this;
				DtdParser.Token result;
				try
				{
					ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter awaiter;
					if (num == 0)
					{
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_19F;
					}
					IL_14:
					char c = dtdParser.chars[dtdParser.curPos];
					if (c == '(')
					{
						dtdParser.scanningFunction = DtdParser.ScanningFunction.Element2;
						dtdParser.curPos++;
						result = DtdParser.Token.LeftParen;
						goto IL_1DA;
					}
					if (c != 'A')
					{
						if (c == 'E')
						{
							if (dtdParser.charsUsed - dtdParser.curPos < 5)
							{
								goto IL_141;
							}
							if (dtdParser.chars[dtdParser.curPos + 1] == 'M' && dtdParser.chars[dtdParser.curPos + 2] == 'P' && dtdParser.chars[dtdParser.curPos + 3] == 'T' && dtdParser.chars[dtdParser.curPos + 4] == 'Y')
							{
								dtdParser.curPos += 5;
								dtdParser.scanningFunction = DtdParser.ScanningFunction.ClosingTag;
								result = DtdParser.Token.EMPTY;
								goto IL_1DA;
							}
						}
					}
					else
					{
						if (dtdParser.charsUsed - dtdParser.curPos < 3)
						{
							goto IL_141;
						}
						if (dtdParser.chars[dtdParser.curPos + 1] == 'N' && dtdParser.chars[dtdParser.curPos + 2] == 'Y')
						{
							dtdParser.curPos += 3;
							dtdParser.scanningFunction = DtdParser.ScanningFunction.ClosingTag;
							result = DtdParser.Token.ANY;
							goto IL_1DA;
						}
					}
					dtdParser.Throw(dtdParser.curPos, "Invalid content model.");
					IL_141:
					awaiter = dtdParser.ReadDataAsync().ConfigureAwait(false).GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						this.<>1__state = 0;
						this.<>u__1 = awaiter;
						this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter, DtdParser.<ScanElement1Async>d__180>(ref awaiter, ref this);
						return;
					}
					IL_19F:
					if (awaiter.GetResult() == 0)
					{
						dtdParser.Throw(dtdParser.curPos, "Incomplete DTD content.");
						goto IL_14;
					}
					goto IL_14;
				}
				catch (Exception exception)
				{
					this.<>1__state = -2;
					this.<>t__builder.SetException(exception);
					return;
				}
				IL_1DA:
				this.<>1__state = -2;
				this.<>t__builder.SetResult(result);
			}

			// Token: 0x0600140C RID: 5132 RVA: 0x0007C9E8 File Offset: 0x0007ABE8
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04001212 RID: 4626
			public int <>1__state;

			// Token: 0x04001213 RID: 4627
			public AsyncTaskMethodBuilder<DtdParser.Token> <>t__builder;

			// Token: 0x04001214 RID: 4628
			public DtdParser <>4__this;

			// Token: 0x04001215 RID: 4629
			private ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter <>u__1;
		}

		// Token: 0x0200020B RID: 523
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <ScanElement2Async>d__181 : IAsyncStateMachine
		{
			// Token: 0x0600140D RID: 5133 RVA: 0x0007C9F8 File Offset: 0x0007ABF8
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				DtdParser dtdParser = this.<>4__this;
				DtdParser.Token result;
				try
				{
					ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter awaiter;
					if (num != 0)
					{
						if (dtdParser.chars[dtdParser.curPos] == '#')
						{
							goto IL_9F;
						}
						goto IL_152;
					}
					else
					{
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
					}
					IL_85:
					if (awaiter.GetResult() == 0)
					{
						dtdParser.Throw(dtdParser.curPos, "Incomplete DTD content.");
					}
					IL_9F:
					if (dtdParser.charsUsed - dtdParser.curPos >= 7)
					{
						if (dtdParser.chars[dtdParser.curPos + 1] == 'P' && dtdParser.chars[dtdParser.curPos + 2] == 'C' && dtdParser.chars[dtdParser.curPos + 3] == 'D' && dtdParser.chars[dtdParser.curPos + 4] == 'A' && dtdParser.chars[dtdParser.curPos + 5] == 'T' && dtdParser.chars[dtdParser.curPos + 6] == 'A')
						{
							dtdParser.curPos += 7;
							dtdParser.scanningFunction = DtdParser.ScanningFunction.Element6;
							result = DtdParser.Token.PCDATA;
							goto IL_177;
						}
						dtdParser.Throw(dtdParser.curPos + 1, "Expecting 'PCDATA'.");
					}
					else
					{
						awaiter = dtdParser.ReadDataAsync().ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 0;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter, DtdParser.<ScanElement2Async>d__181>(ref awaiter, ref this);
							return;
						}
						goto IL_85;
					}
					IL_152:
					dtdParser.scanningFunction = DtdParser.ScanningFunction.Element3;
					result = DtdParser.Token.None;
				}
				catch (Exception exception)
				{
					this.<>1__state = -2;
					this.<>t__builder.SetException(exception);
					return;
				}
				IL_177:
				this.<>1__state = -2;
				this.<>t__builder.SetResult(result);
			}

			// Token: 0x0600140E RID: 5134 RVA: 0x0007CBAC File Offset: 0x0007ADAC
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04001216 RID: 4630
			public int <>1__state;

			// Token: 0x04001217 RID: 4631
			public AsyncTaskMethodBuilder<DtdParser.Token> <>t__builder;

			// Token: 0x04001218 RID: 4632
			public DtdParser <>4__this;

			// Token: 0x04001219 RID: 4633
			private ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter <>u__1;
		}

		// Token: 0x0200020C RID: 524
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <ScanElement3Async>d__182 : IAsyncStateMachine
		{
			// Token: 0x0600140F RID: 5135 RVA: 0x0007CBBC File Offset: 0x0007ADBC
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				DtdParser dtdParser = this.<>4__this;
				DtdParser.Token result;
				try
				{
					ConfiguredTaskAwaitable.ConfiguredTaskAwaiter awaiter;
					if (num != 0)
					{
						char c = dtdParser.chars[dtdParser.curPos];
						if (c == '(')
						{
							dtdParser.curPos++;
							result = DtdParser.Token.LeftParen;
							goto IL_EC;
						}
						if (c == '>')
						{
							dtdParser.curPos++;
							dtdParser.scanningFunction = DtdParser.ScanningFunction.SubsetContent;
							result = DtdParser.Token.GreaterThan;
							goto IL_EC;
						}
						awaiter = dtdParser.ScanQNameAsync().ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 0;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, DtdParser.<ScanElement3Async>d__182>(ref awaiter, ref this);
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
					dtdParser.scanningFunction = DtdParser.ScanningFunction.Element4;
					result = DtdParser.Token.QName;
				}
				catch (Exception exception)
				{
					this.<>1__state = -2;
					this.<>t__builder.SetException(exception);
					return;
				}
				IL_EC:
				this.<>1__state = -2;
				this.<>t__builder.SetResult(result);
			}

			// Token: 0x06001410 RID: 5136 RVA: 0x0007CCDC File Offset: 0x0007AEDC
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x0400121A RID: 4634
			public int <>1__state;

			// Token: 0x0400121B RID: 4635
			public AsyncTaskMethodBuilder<DtdParser.Token> <>t__builder;

			// Token: 0x0400121C RID: 4636
			public DtdParser <>4__this;

			// Token: 0x0400121D RID: 4637
			private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter <>u__1;
		}

		// Token: 0x0200020D RID: 525
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <ScanAttlist1Async>d__183 : IAsyncStateMachine
		{
			// Token: 0x06001411 RID: 5137 RVA: 0x0007CCEC File Offset: 0x0007AEEC
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				DtdParser dtdParser = this.<>4__this;
				DtdParser.Token result;
				try
				{
					ConfiguredTaskAwaitable.ConfiguredTaskAwaiter awaiter;
					if (num != 0)
					{
						if (dtdParser.chars[dtdParser.curPos] == '>')
						{
							dtdParser.curPos++;
							dtdParser.scanningFunction = DtdParser.ScanningFunction.SubsetContent;
							result = DtdParser.Token.GreaterThan;
							goto IL_EF;
						}
						if (!dtdParser.whitespaceSeen)
						{
							dtdParser.Throw(dtdParser.curPos, "'{0}' is an unexpected token. Expecting white space.", dtdParser.ParseUnexpectedToken(dtdParser.curPos));
						}
						awaiter = dtdParser.ScanQNameAsync().ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 0;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, DtdParser.<ScanAttlist1Async>d__183>(ref awaiter, ref this);
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
					dtdParser.scanningFunction = DtdParser.ScanningFunction.Attlist2;
					result = DtdParser.Token.QName;
				}
				catch (Exception exception)
				{
					this.<>1__state = -2;
					this.<>t__builder.SetException(exception);
					return;
				}
				IL_EF:
				this.<>1__state = -2;
				this.<>t__builder.SetResult(result);
			}

			// Token: 0x06001412 RID: 5138 RVA: 0x0007CE0C File Offset: 0x0007B00C
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x0400121E RID: 4638
			public int <>1__state;

			// Token: 0x0400121F RID: 4639
			public AsyncTaskMethodBuilder<DtdParser.Token> <>t__builder;

			// Token: 0x04001220 RID: 4640
			public DtdParser <>4__this;

			// Token: 0x04001221 RID: 4641
			private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter <>u__1;
		}

		// Token: 0x0200020E RID: 526
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <ScanAttlist2Async>d__184 : IAsyncStateMachine
		{
			// Token: 0x06001413 RID: 5139 RVA: 0x0007CE1C File Offset: 0x0007B01C
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				DtdParser dtdParser = this.<>4__this;
				DtdParser.Token result;
				try
				{
					ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter awaiter;
					if (num == 0)
					{
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_509;
					}
					IL_14:
					char c = dtdParser.chars[dtdParser.curPos];
					if (c <= 'C')
					{
						if (c == '(')
						{
							dtdParser.curPos++;
							dtdParser.scanningFunction = DtdParser.ScanningFunction.Nmtoken;
							dtdParser.nextScaningFunction = DtdParser.ScanningFunction.Attlist5;
							result = DtdParser.Token.LeftParen;
							goto IL_544;
						}
						if (c == 'C')
						{
							if (dtdParser.charsUsed - dtdParser.curPos >= 5)
							{
								if (dtdParser.chars[dtdParser.curPos + 1] != 'D' || dtdParser.chars[dtdParser.curPos + 2] != 'A' || dtdParser.chars[dtdParser.curPos + 3] != 'T' || dtdParser.chars[dtdParser.curPos + 4] != 'A')
								{
									dtdParser.Throw(dtdParser.curPos, "Invalid attribute type.");
								}
								dtdParser.curPos += 5;
								dtdParser.scanningFunction = DtdParser.ScanningFunction.Attlist6;
								result = DtdParser.Token.CDATA;
								goto IL_544;
							}
							goto IL_4AB;
						}
					}
					else if (c != 'E')
					{
						if (c != 'I')
						{
							if (c == 'N')
							{
								if (dtdParser.charsUsed - dtdParser.curPos < 8 && !dtdParser.readerAdapter.IsEof)
								{
									goto IL_4AB;
								}
								char c2 = dtdParser.chars[dtdParser.curPos + 1];
								if (c2 != 'M')
								{
									if (c2 == 'O')
									{
										if (dtdParser.chars[dtdParser.curPos + 2] != 'T' || dtdParser.chars[dtdParser.curPos + 3] != 'A' || dtdParser.chars[dtdParser.curPos + 4] != 'T' || dtdParser.chars[dtdParser.curPos + 5] != 'I' || dtdParser.chars[dtdParser.curPos + 6] != 'O' || dtdParser.chars[dtdParser.curPos + 7] != 'N')
										{
											dtdParser.Throw(dtdParser.curPos, "'{0}' is an invalid attribute type.");
										}
										dtdParser.curPos += 8;
										dtdParser.scanningFunction = DtdParser.ScanningFunction.Attlist3;
										result = DtdParser.Token.NOTATION;
										goto IL_544;
									}
									dtdParser.Throw(dtdParser.curPos, "'{0}' is an invalid attribute type.");
									goto IL_4AB;
								}
								else
								{
									if (dtdParser.chars[dtdParser.curPos + 2] != 'T' || dtdParser.chars[dtdParser.curPos + 3] != 'O' || dtdParser.chars[dtdParser.curPos + 4] != 'K' || dtdParser.chars[dtdParser.curPos + 5] != 'E' || dtdParser.chars[dtdParser.curPos + 6] != 'N')
									{
										dtdParser.Throw(dtdParser.curPos, "'{0}' is an invalid attribute type.");
									}
									dtdParser.scanningFunction = DtdParser.ScanningFunction.Attlist6;
									if (dtdParser.chars[dtdParser.curPos + 7] == 'S')
									{
										dtdParser.curPos += 8;
										result = DtdParser.Token.NMTOKENS;
										goto IL_544;
									}
									dtdParser.curPos += 7;
									result = DtdParser.Token.NMTOKEN;
									goto IL_544;
								}
							}
						}
						else
						{
							if (dtdParser.charsUsed - dtdParser.curPos < 6)
							{
								goto IL_4AB;
							}
							dtdParser.scanningFunction = DtdParser.ScanningFunction.Attlist6;
							if (dtdParser.chars[dtdParser.curPos + 1] != 'D')
							{
								dtdParser.Throw(dtdParser.curPos, "'{0}' is an invalid attribute type.");
							}
							if (dtdParser.chars[dtdParser.curPos + 2] != 'R')
							{
								dtdParser.curPos += 2;
								result = DtdParser.Token.ID;
								goto IL_544;
							}
							if (dtdParser.chars[dtdParser.curPos + 3] != 'E' || dtdParser.chars[dtdParser.curPos + 4] != 'F')
							{
								dtdParser.Throw(dtdParser.curPos, "'{0}' is an invalid attribute type.");
							}
							if (dtdParser.chars[dtdParser.curPos + 5] != 'S')
							{
								dtdParser.curPos += 5;
								result = DtdParser.Token.IDREF;
								goto IL_544;
							}
							dtdParser.curPos += 6;
							result = DtdParser.Token.IDREFS;
							goto IL_544;
						}
					}
					else
					{
						if (dtdParser.charsUsed - dtdParser.curPos < 9)
						{
							goto IL_4AB;
						}
						dtdParser.scanningFunction = DtdParser.ScanningFunction.Attlist6;
						if (dtdParser.chars[dtdParser.curPos + 1] != 'N' || dtdParser.chars[dtdParser.curPos + 2] != 'T' || dtdParser.chars[dtdParser.curPos + 3] != 'I' || dtdParser.chars[dtdParser.curPos + 4] != 'T')
						{
							dtdParser.Throw(dtdParser.curPos, "'{0}' is an invalid attribute type.");
						}
						char c2 = dtdParser.chars[dtdParser.curPos + 5];
						if (c2 == 'I')
						{
							if (dtdParser.chars[dtdParser.curPos + 6] != 'E' || dtdParser.chars[dtdParser.curPos + 7] != 'S')
							{
								dtdParser.Throw(dtdParser.curPos, "'{0}' is an invalid attribute type.");
							}
							dtdParser.curPos += 8;
							result = DtdParser.Token.ENTITIES;
							goto IL_544;
						}
						if (c2 != 'Y')
						{
							dtdParser.Throw(dtdParser.curPos, "'{0}' is an invalid attribute type.");
							goto IL_4AB;
						}
						dtdParser.curPos += 6;
						result = DtdParser.Token.ENTITY;
						goto IL_544;
					}
					dtdParser.Throw(dtdParser.curPos, "'{0}' is an invalid attribute type.");
					IL_4AB:
					awaiter = dtdParser.ReadDataAsync().ConfigureAwait(false).GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						this.<>1__state = 0;
						this.<>u__1 = awaiter;
						this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter, DtdParser.<ScanAttlist2Async>d__184>(ref awaiter, ref this);
						return;
					}
					IL_509:
					if (awaiter.GetResult() == 0)
					{
						dtdParser.Throw(dtdParser.curPos, "Incomplete DTD content.");
						goto IL_14;
					}
					goto IL_14;
				}
				catch (Exception exception)
				{
					this.<>1__state = -2;
					this.<>t__builder.SetException(exception);
					return;
				}
				IL_544:
				this.<>1__state = -2;
				this.<>t__builder.SetResult(result);
			}

			// Token: 0x06001414 RID: 5140 RVA: 0x0007D3A0 File Offset: 0x0007B5A0
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04001222 RID: 4642
			public int <>1__state;

			// Token: 0x04001223 RID: 4643
			public AsyncTaskMethodBuilder<DtdParser.Token> <>t__builder;

			// Token: 0x04001224 RID: 4644
			public DtdParser <>4__this;

			// Token: 0x04001225 RID: 4645
			private ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter <>u__1;
		}

		// Token: 0x0200020F RID: 527
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <ScanAttlist6Async>d__185 : IAsyncStateMachine
		{
			// Token: 0x06001415 RID: 5141 RVA: 0x0007D3B0 File Offset: 0x0007B5B0
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				DtdParser dtdParser = this.<>4__this;
				DtdParser.Token result;
				try
				{
					ConfiguredTaskAwaitable<DtdParser.Token>.ConfiguredTaskAwaiter awaiter2;
					if (num != 0)
					{
						ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter awaiter;
						if (num == 1)
						{
							awaiter = this.<>u__2;
							this.<>u__2 = default(ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter);
							this.<>1__state = -1;
							goto IL_36E;
						}
						IL_18:
						char c = dtdParser.chars[dtdParser.curPos];
						if (c != '"')
						{
							if (c != '#')
							{
								if (c == '\'')
								{
									goto IL_3B;
								}
								dtdParser.Throw(dtdParser.curPos, "Expecting an attribute type.");
							}
							else if (dtdParser.charsUsed - dtdParser.curPos >= 6)
							{
								char c2 = dtdParser.chars[dtdParser.curPos + 1];
								if (c2 == 'F')
								{
									if (dtdParser.chars[dtdParser.curPos + 2] != 'I' || dtdParser.chars[dtdParser.curPos + 3] != 'X' || dtdParser.chars[dtdParser.curPos + 4] != 'E' || dtdParser.chars[dtdParser.curPos + 5] != 'D')
									{
										dtdParser.Throw(dtdParser.curPos, "Expecting an attribute type.");
									}
									dtdParser.curPos += 6;
									dtdParser.scanningFunction = DtdParser.ScanningFunction.Attlist7;
									result = DtdParser.Token.FIXED;
									goto IL_3A9;
								}
								if (c2 != 'I')
								{
									if (c2 == 'R')
									{
										if (dtdParser.charsUsed - dtdParser.curPos >= 9)
										{
											if (dtdParser.chars[dtdParser.curPos + 2] != 'E' || dtdParser.chars[dtdParser.curPos + 3] != 'Q' || dtdParser.chars[dtdParser.curPos + 4] != 'U' || dtdParser.chars[dtdParser.curPos + 5] != 'I' || dtdParser.chars[dtdParser.curPos + 6] != 'R' || dtdParser.chars[dtdParser.curPos + 7] != 'E' || dtdParser.chars[dtdParser.curPos + 8] != 'D')
											{
												dtdParser.Throw(dtdParser.curPos, "Expecting an attribute type.");
											}
											dtdParser.curPos += 9;
											dtdParser.scanningFunction = DtdParser.ScanningFunction.Attlist1;
											result = DtdParser.Token.REQUIRED;
											goto IL_3A9;
										}
									}
									else
									{
										dtdParser.Throw(dtdParser.curPos, "Expecting an attribute type.");
									}
								}
								else if (dtdParser.charsUsed - dtdParser.curPos >= 8)
								{
									if (dtdParser.chars[dtdParser.curPos + 2] != 'M' || dtdParser.chars[dtdParser.curPos + 3] != 'P' || dtdParser.chars[dtdParser.curPos + 4] != 'L' || dtdParser.chars[dtdParser.curPos + 5] != 'I' || dtdParser.chars[dtdParser.curPos + 6] != 'E' || dtdParser.chars[dtdParser.curPos + 7] != 'D')
									{
										dtdParser.Throw(dtdParser.curPos, "Expecting an attribute type.");
									}
									dtdParser.curPos += 8;
									dtdParser.scanningFunction = DtdParser.ScanningFunction.Attlist1;
									result = DtdParser.Token.IMPLIED;
									goto IL_3A9;
								}
							}
							awaiter = dtdParser.ReadDataAsync().ConfigureAwait(false).GetAwaiter();
							if (!awaiter.IsCompleted)
							{
								this.<>1__state = 1;
								this.<>u__2 = awaiter;
								this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter, DtdParser.<ScanAttlist6Async>d__185>(ref awaiter, ref this);
								return;
							}
							goto IL_36E;
						}
						IL_3B:
						awaiter2 = dtdParser.ScanLiteralAsync(DtdParser.LiteralType.AttributeValue).ConfigureAwait(false).GetAwaiter();
						if (!awaiter2.IsCompleted)
						{
							this.<>1__state = 0;
							this.<>u__1 = awaiter2;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<DtdParser.Token>.ConfiguredTaskAwaiter, DtdParser.<ScanAttlist6Async>d__185>(ref awaiter2, ref this);
							return;
						}
						goto IL_9D;
						IL_36E:
						if (awaiter.GetResult() == 0)
						{
							dtdParser.Throw(dtdParser.curPos, "Incomplete DTD content.");
							goto IL_18;
						}
						goto IL_18;
					}
					else
					{
						awaiter2 = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable<DtdParser.Token>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
					}
					IL_9D:
					awaiter2.GetResult();
					dtdParser.scanningFunction = DtdParser.ScanningFunction.Attlist1;
					result = DtdParser.Token.Literal;
				}
				catch (Exception exception)
				{
					this.<>1__state = -2;
					this.<>t__builder.SetException(exception);
					return;
				}
				IL_3A9:
				this.<>1__state = -2;
				this.<>t__builder.SetResult(result);
			}

			// Token: 0x06001416 RID: 5142 RVA: 0x0007D798 File Offset: 0x0007B998
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04001226 RID: 4646
			public int <>1__state;

			// Token: 0x04001227 RID: 4647
			public AsyncTaskMethodBuilder<DtdParser.Token> <>t__builder;

			// Token: 0x04001228 RID: 4648
			public DtdParser <>4__this;

			// Token: 0x04001229 RID: 4649
			private ConfiguredTaskAwaitable<DtdParser.Token>.ConfiguredTaskAwaiter <>u__1;

			// Token: 0x0400122A RID: 4650
			private ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter <>u__2;
		}

		// Token: 0x02000210 RID: 528
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <ScanLiteralAsync>d__186 : IAsyncStateMachine
		{
			// Token: 0x06001417 RID: 5143 RVA: 0x0007D7A8 File Offset: 0x0007B9A8
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				DtdParser dtdParser = this.<>4__this;
				DtdParser.Token result;
				try
				{
					ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter awaiter;
					ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter awaiter2;
					switch (num)
					{
					case 0:
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_45B;
					case 1:
						awaiter2 = this.<>u__2;
						this.<>u__2 = default(ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_536;
					case 2:
						awaiter2 = this.<>u__2;
						this.<>u__2 = default(ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_611;
					case 3:
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_6CC;
					case 4:
						awaiter2 = this.<>u__2;
						this.<>u__2 = default(ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_74D;
					case 5:
						awaiter2 = this.<>u__2;
						this.<>u__2 = default(ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_8B4;
					default:
						this.<quoteChar>5__2 = dtdParser.chars[dtdParser.curPos];
						this.<replaceChar>5__3 = ((this.literalType == DtdParser.LiteralType.AttributeValue) ? ' ' : '\n');
						this.<startQuoteEntityId>5__4 = dtdParser.currentEntityId;
						dtdParser.literalLineInfo.Set(dtdParser.LineNo, dtdParser.LinePos);
						dtdParser.curPos++;
						dtdParser.tokenStartPos = dtdParser.curPos;
						dtdParser.stringBuilder.Length = 0;
						break;
					}
					for (;;)
					{
						IL_AC:
						if ((dtdParser.xmlCharType.charProperties[(int)dtdParser.chars[dtdParser.curPos]] & 128) == 0 || dtdParser.chars[dtdParser.curPos] == '%')
						{
							if (dtdParser.chars[dtdParser.curPos] == this.<quoteChar>5__2 && dtdParser.currentEntityId == this.<startQuoteEntityId>5__4)
							{
								break;
							}
							int num2 = dtdParser.curPos - dtdParser.tokenStartPos;
							if (num2 > 0)
							{
								dtdParser.stringBuilder.Append(dtdParser.chars, dtdParser.tokenStartPos, num2);
								dtdParser.tokenStartPos = dtdParser.curPos;
							}
							char c = dtdParser.chars[dtdParser.curPos];
							if (c <= '\'')
							{
								switch (c)
								{
								case '\t':
									if (this.literalType == DtdParser.LiteralType.AttributeValue && dtdParser.Normalize)
									{
										dtdParser.stringBuilder.Append(' ');
										dtdParser.tokenStartPos++;
									}
									dtdParser.curPos++;
									continue;
								case '\n':
									dtdParser.curPos++;
									if (dtdParser.Normalize)
									{
										dtdParser.stringBuilder.Append(this.<replaceChar>5__3);
										dtdParser.tokenStartPos = dtdParser.curPos;
									}
									dtdParser.readerAdapter.OnNewLine(dtdParser.curPos);
									continue;
								case '\v':
								case '\f':
									goto IL_7BB;
								case '\r':
									if (dtdParser.chars[dtdParser.curPos + 1] == '\n')
									{
										if (dtdParser.Normalize)
										{
											if (this.literalType == DtdParser.LiteralType.AttributeValue)
											{
												dtdParser.stringBuilder.Append(dtdParser.readerAdapter.IsEntityEolNormalized ? "  " : " ");
											}
											else
											{
												dtdParser.stringBuilder.Append(dtdParser.readerAdapter.IsEntityEolNormalized ? "\r\n" : "\n");
											}
											dtdParser.tokenStartPos = dtdParser.curPos + 2;
											dtdParser.SaveParsingBuffer();
											IDtdParserAdapter readerAdapter = dtdParser.readerAdapter;
											int currentPosition = readerAdapter.CurrentPosition;
											readerAdapter.CurrentPosition = currentPosition + 1;
										}
										dtdParser.curPos += 2;
									}
									else
									{
										if (dtdParser.curPos + 1 == dtdParser.charsUsed)
										{
											goto IL_842;
										}
										dtdParser.curPos++;
										if (dtdParser.Normalize)
										{
											dtdParser.stringBuilder.Append(this.<replaceChar>5__3);
											dtdParser.tokenStartPos = dtdParser.curPos;
										}
									}
									dtdParser.readerAdapter.OnNewLine(dtdParser.curPos);
									continue;
								default:
									switch (c)
									{
									case '"':
									case '\'':
										break;
									case '#':
									case '$':
										goto IL_7BB;
									case '%':
										if (this.literalType != DtdParser.LiteralType.EntityReplText)
										{
											dtdParser.curPos++;
											continue;
										}
										goto IL_3EF;
									case '&':
										if (this.literalType == DtdParser.LiteralType.SystemOrPublicID)
										{
											dtdParser.curPos++;
											continue;
										}
										goto IL_490;
									default:
										goto IL_7BB;
									}
									break;
								}
							}
							else
							{
								if (c == '<')
								{
									if (this.literalType == DtdParser.LiteralType.AttributeValue)
									{
										dtdParser.Throw(dtdParser.curPos, "'{0}', hexadecimal value {1}, is an invalid attribute character.", XmlException.BuildCharExceptionArgs('<', '\0'));
									}
									dtdParser.curPos++;
									continue;
								}
								if (c != '>')
								{
									goto IL_7BB;
								}
							}
							dtdParser.curPos++;
							continue;
							IL_7BB:
							if (dtdParser.curPos == dtdParser.charsUsed)
							{
								goto IL_842;
							}
							if (!XmlCharType.IsHighSurrogate((int)dtdParser.chars[dtdParser.curPos]))
							{
								goto IL_822;
							}
							if (dtdParser.curPos + 1 == dtdParser.charsUsed)
							{
								goto IL_842;
							}
							dtdParser.curPos++;
							if (!XmlCharType.IsLowSurrogate((int)dtdParser.chars[dtdParser.curPos]))
							{
								goto IL_822;
							}
							dtdParser.curPos++;
						}
						else
						{
							dtdParser.curPos++;
						}
					}
					if (dtdParser.stringBuilder.Length > 0)
					{
						dtdParser.stringBuilder.Append(dtdParser.chars, dtdParser.tokenStartPos, dtdParser.curPos - dtdParser.tokenStartPos);
					}
					dtdParser.curPos++;
					dtdParser.literalQuoteChar = this.<quoteChar>5__2;
					result = DtdParser.Token.Literal;
					goto IL_911;
					IL_3EF:
					awaiter = dtdParser.HandleEntityReferenceAsync(true, true, this.literalType == DtdParser.LiteralType.AttributeValue).ConfigureAwait(false).GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						this.<>1__state = 0;
						this.<>u__1 = awaiter;
						this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter, DtdParser.<ScanLiteralAsync>d__186>(ref awaiter, ref this);
						return;
					}
					goto IL_45B;
					IL_490:
					if (dtdParser.curPos + 1 == dtdParser.charsUsed)
					{
						goto IL_842;
					}
					if (dtdParser.chars[dtdParser.curPos + 1] == '#')
					{
						dtdParser.SaveParsingBuffer();
						awaiter2 = dtdParser.readerAdapter.ParseNumericCharRefAsync(dtdParser.SaveInternalSubsetValue ? dtdParser.internalSubsetValueSb : null).ConfigureAwait(false).GetAwaiter();
						if (!awaiter2.IsCompleted)
						{
							this.<>1__state = 1;
							this.<>u__2 = awaiter2;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter, DtdParser.<ScanLiteralAsync>d__186>(ref awaiter2, ref this);
							return;
						}
						goto IL_536;
					}
					else
					{
						dtdParser.SaveParsingBuffer();
						if (this.literalType == DtdParser.LiteralType.AttributeValue)
						{
							awaiter2 = dtdParser.readerAdapter.ParseNamedCharRefAsync(true, dtdParser.SaveInternalSubsetValue ? dtdParser.internalSubsetValueSb : null).ConfigureAwait(false).GetAwaiter();
							if (!awaiter2.IsCompleted)
							{
								this.<>1__state = 2;
								this.<>u__2 = awaiter2;
								this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter, DtdParser.<ScanLiteralAsync>d__186>(ref awaiter2, ref this);
								return;
							}
							goto IL_611;
						}
						else
						{
							awaiter2 = dtdParser.readerAdapter.ParseNamedCharRefAsync(false, null).ConfigureAwait(false).GetAwaiter();
							if (!awaiter2.IsCompleted)
							{
								this.<>1__state = 4;
								this.<>u__2 = awaiter2;
								this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter, DtdParser.<ScanLiteralAsync>d__186>(ref awaiter2, ref this);
								return;
							}
							goto IL_74D;
						}
					}
					IL_822:
					dtdParser.ThrowInvalidChar(dtdParser.chars, dtdParser.charsUsed, dtdParser.curPos);
					result = DtdParser.Token.None;
					goto IL_911;
					IL_842:
					bool flag = dtdParser.readerAdapter.IsEof;
					if (flag)
					{
						goto IL_8C0;
					}
					awaiter2 = dtdParser.ReadDataAsync().ConfigureAwait(false).GetAwaiter();
					if (!awaiter2.IsCompleted)
					{
						this.<>1__state = 5;
						this.<>u__2 = awaiter2;
						this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter, DtdParser.<ScanLiteralAsync>d__186>(ref awaiter2, ref this);
						return;
					}
					goto IL_8B4;
					IL_45B:
					awaiter.GetResult();
					dtdParser.tokenStartPos = dtdParser.curPos;
					goto IL_AC;
					IL_536:
					int result2 = awaiter2.GetResult();
					dtdParser.LoadParsingBuffer();
					dtdParser.stringBuilder.Append(dtdParser.chars, dtdParser.curPos, result2 - dtdParser.curPos);
					dtdParser.readerAdapter.CurrentPosition = result2;
					dtdParser.tokenStartPos = result2;
					dtdParser.curPos = result2;
					goto IL_AC;
					IL_611:
					int result3 = awaiter2.GetResult();
					dtdParser.LoadParsingBuffer();
					if (result3 >= 0)
					{
						dtdParser.stringBuilder.Append(dtdParser.chars, dtdParser.curPos, result3 - dtdParser.curPos);
						dtdParser.readerAdapter.CurrentPosition = result3;
						dtdParser.tokenStartPos = result3;
						dtdParser.curPos = result3;
						goto IL_AC;
					}
					awaiter = dtdParser.HandleEntityReferenceAsync(false, true, true).ConfigureAwait(false).GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						this.<>1__state = 3;
						this.<>u__1 = awaiter;
						this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter, DtdParser.<ScanLiteralAsync>d__186>(ref awaiter, ref this);
						return;
					}
					IL_6CC:
					awaiter.GetResult();
					dtdParser.tokenStartPos = dtdParser.curPos;
					goto IL_AC;
					IL_74D:
					int result4 = awaiter2.GetResult();
					dtdParser.LoadParsingBuffer();
					if (result4 >= 0)
					{
						dtdParser.tokenStartPos = dtdParser.curPos;
						dtdParser.curPos = result4;
						goto IL_AC;
					}
					dtdParser.stringBuilder.Append('&');
					dtdParser.curPos++;
					dtdParser.tokenStartPos = dtdParser.curPos;
					XmlQualifiedName entityName = dtdParser.ScanEntityName();
					dtdParser.VerifyEntityReference(entityName, false, false, false);
					goto IL_AC;
					IL_8B4:
					flag = (awaiter2.GetResult() == 0);
					IL_8C0:
					if (flag && (this.literalType == DtdParser.LiteralType.SystemOrPublicID || !dtdParser.HandleEntityEnd(true)))
					{
						dtdParser.Throw(dtdParser.curPos, "There is an unclosed literal string.");
					}
					dtdParser.tokenStartPos = dtdParser.curPos;
					goto IL_AC;
				}
				catch (Exception exception)
				{
					this.<>1__state = -2;
					this.<>t__builder.SetException(exception);
					return;
				}
				IL_911:
				this.<>1__state = -2;
				this.<>t__builder.SetResult(result);
			}

			// Token: 0x06001418 RID: 5144 RVA: 0x0007E0F8 File Offset: 0x0007C2F8
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x0400122B RID: 4651
			public int <>1__state;

			// Token: 0x0400122C RID: 4652
			public AsyncTaskMethodBuilder<DtdParser.Token> <>t__builder;

			// Token: 0x0400122D RID: 4653
			public DtdParser <>4__this;

			// Token: 0x0400122E RID: 4654
			public DtdParser.LiteralType literalType;

			// Token: 0x0400122F RID: 4655
			private char <quoteChar>5__2;

			// Token: 0x04001230 RID: 4656
			private char <replaceChar>5__3;

			// Token: 0x04001231 RID: 4657
			private int <startQuoteEntityId>5__4;

			// Token: 0x04001232 RID: 4658
			private ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter <>u__1;

			// Token: 0x04001233 RID: 4659
			private ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter <>u__2;
		}

		// Token: 0x02000211 RID: 529
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <ScanNotation1Async>d__187 : IAsyncStateMachine
		{
			// Token: 0x06001419 RID: 5145 RVA: 0x0007E108 File Offset: 0x0007C308
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				DtdParser dtdParser = this.<>4__this;
				DtdParser.Token result;
				try
				{
					ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter awaiter;
					if (num != 0)
					{
						if (num != 1)
						{
							char c = dtdParser.chars[dtdParser.curPos];
							if (c != 'P')
							{
								if (c != 'S')
								{
									dtdParser.Throw(dtdParser.curPos, "Expecting a system identifier or a public identifier.");
									result = DtdParser.Token.None;
									goto IL_18A;
								}
								awaiter = dtdParser.EatSystemKeywordAsync().ConfigureAwait(false).GetAwaiter();
								if (!awaiter.IsCompleted)
								{
									this.<>1__state = 1;
									this.<>u__1 = awaiter;
									this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter, DtdParser.<ScanNotation1Async>d__187>(ref awaiter, ref this);
									return;
								}
							}
							else
							{
								awaiter = dtdParser.EatPublicKeywordAsync().ConfigureAwait(false).GetAwaiter();
								if (!awaiter.IsCompleted)
								{
									this.<>1__state = 0;
									this.<>u__1 = awaiter;
									this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter, DtdParser.<ScanNotation1Async>d__187>(ref awaiter, ref this);
									return;
								}
								goto IL_99;
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
							dtdParser.Throw(dtdParser.curPos, "Expecting external ID, '[' or '>'.");
						}
						dtdParser.nextScaningFunction = DtdParser.ScanningFunction.ClosingTag;
						dtdParser.scanningFunction = DtdParser.ScanningFunction.SystemId;
						result = DtdParser.Token.SYSTEM;
						goto IL_18A;
					}
					awaiter = this.<>u__1;
					this.<>u__1 = default(ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter);
					this.<>1__state = -1;
					IL_99:
					if (!awaiter.GetResult())
					{
						dtdParser.Throw(dtdParser.curPos, "Expecting external ID, '[' or '>'.");
					}
					dtdParser.nextScaningFunction = DtdParser.ScanningFunction.ClosingTag;
					dtdParser.scanningFunction = DtdParser.ScanningFunction.PublicId1;
					result = DtdParser.Token.PUBLIC;
				}
				catch (Exception exception)
				{
					this.<>1__state = -2;
					this.<>t__builder.SetException(exception);
					return;
				}
				IL_18A:
				this.<>1__state = -2;
				this.<>t__builder.SetResult(result);
			}

			// Token: 0x0600141A RID: 5146 RVA: 0x0007E2D0 File Offset: 0x0007C4D0
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04001234 RID: 4660
			public int <>1__state;

			// Token: 0x04001235 RID: 4661
			public AsyncTaskMethodBuilder<DtdParser.Token> <>t__builder;

			// Token: 0x04001236 RID: 4662
			public DtdParser <>4__this;

			// Token: 0x04001237 RID: 4663
			private ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter <>u__1;
		}

		// Token: 0x02000212 RID: 530
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <ScanSystemIdAsync>d__188 : IAsyncStateMachine
		{
			// Token: 0x0600141B RID: 5147 RVA: 0x0007E2E0 File Offset: 0x0007C4E0
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				DtdParser dtdParser = this.<>4__this;
				DtdParser.Token result;
				try
				{
					ConfiguredTaskAwaitable<DtdParser.Token>.ConfiguredTaskAwaiter awaiter;
					if (num != 0)
					{
						if (dtdParser.chars[dtdParser.curPos] != '"' && dtdParser.chars[dtdParser.curPos] != '\'')
						{
							dtdParser.ThrowUnexpectedToken(dtdParser.curPos, "\"", "'");
						}
						awaiter = dtdParser.ScanLiteralAsync(DtdParser.LiteralType.SystemOrPublicID).ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 0;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<DtdParser.Token>.ConfiguredTaskAwaiter, DtdParser.<ScanSystemIdAsync>d__188>(ref awaiter, ref this);
							return;
						}
					}
					else
					{
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable<DtdParser.Token>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
					}
					awaiter.GetResult();
					dtdParser.scanningFunction = dtdParser.nextScaningFunction;
					result = DtdParser.Token.Literal;
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

			// Token: 0x0600141C RID: 5148 RVA: 0x0007E3E8 File Offset: 0x0007C5E8
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04001238 RID: 4664
			public int <>1__state;

			// Token: 0x04001239 RID: 4665
			public AsyncTaskMethodBuilder<DtdParser.Token> <>t__builder;

			// Token: 0x0400123A RID: 4666
			public DtdParser <>4__this;

			// Token: 0x0400123B RID: 4667
			private ConfiguredTaskAwaitable<DtdParser.Token>.ConfiguredTaskAwaiter <>u__1;
		}

		// Token: 0x02000213 RID: 531
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <ScanEntity1Async>d__189 : IAsyncStateMachine
		{
			// Token: 0x0600141D RID: 5149 RVA: 0x0007E3F8 File Offset: 0x0007C5F8
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				DtdParser dtdParser = this.<>4__this;
				DtdParser.Token result;
				try
				{
					ConfiguredTaskAwaitable.ConfiguredTaskAwaiter awaiter;
					if (num != 0)
					{
						if (dtdParser.chars[dtdParser.curPos] == '%')
						{
							dtdParser.curPos++;
							dtdParser.nextScaningFunction = DtdParser.ScanningFunction.Entity2;
							dtdParser.scanningFunction = DtdParser.ScanningFunction.Name;
							result = DtdParser.Token.Percent;
							goto IL_CF;
						}
						awaiter = dtdParser.ScanNameAsync().ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 0;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, DtdParser.<ScanEntity1Async>d__189>(ref awaiter, ref this);
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
					dtdParser.scanningFunction = DtdParser.ScanningFunction.Entity2;
					result = DtdParser.Token.Name;
				}
				catch (Exception exception)
				{
					this.<>1__state = -2;
					this.<>t__builder.SetException(exception);
					return;
				}
				IL_CF:
				this.<>1__state = -2;
				this.<>t__builder.SetResult(result);
			}

			// Token: 0x0600141E RID: 5150 RVA: 0x0007E4F8 File Offset: 0x0007C6F8
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x0400123C RID: 4668
			public int <>1__state;

			// Token: 0x0400123D RID: 4669
			public AsyncTaskMethodBuilder<DtdParser.Token> <>t__builder;

			// Token: 0x0400123E RID: 4670
			public DtdParser <>4__this;

			// Token: 0x0400123F RID: 4671
			private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter <>u__1;
		}

		// Token: 0x02000214 RID: 532
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <ScanEntity2Async>d__190 : IAsyncStateMachine
		{
			// Token: 0x0600141F RID: 5151 RVA: 0x0007E508 File Offset: 0x0007C708
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				DtdParser dtdParser = this.<>4__this;
				DtdParser.Token result;
				try
				{
					ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter awaiter;
					ConfiguredTaskAwaitable<DtdParser.Token>.ConfiguredTaskAwaiter awaiter2;
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
						goto IL_14E;
					case 2:
						awaiter2 = this.<>u__2;
						this.<>u__2 = default(ConfiguredTaskAwaitable<DtdParser.Token>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_1DF;
					default:
					{
						char c = dtdParser.chars[dtdParser.curPos];
						if (c <= '\'')
						{
							if (c == '"' || c == '\'')
							{
								awaiter2 = dtdParser.ScanLiteralAsync(DtdParser.LiteralType.EntityReplText).ConfigureAwait(false).GetAwaiter();
								if (!awaiter2.IsCompleted)
								{
									this.<>1__state = 2;
									this.<>u__2 = awaiter2;
									this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<DtdParser.Token>.ConfiguredTaskAwaiter, DtdParser.<ScanEntity2Async>d__190>(ref awaiter2, ref this);
									return;
								}
								goto IL_1DF;
							}
						}
						else if (c != 'P')
						{
							if (c == 'S')
							{
								awaiter = dtdParser.EatSystemKeywordAsync().ConfigureAwait(false).GetAwaiter();
								if (!awaiter.IsCompleted)
								{
									this.<>1__state = 1;
									this.<>u__1 = awaiter;
									this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter, DtdParser.<ScanEntity2Async>d__190>(ref awaiter, ref this);
									return;
								}
								goto IL_14E;
							}
						}
						else
						{
							awaiter = dtdParser.EatPublicKeywordAsync().ConfigureAwait(false).GetAwaiter();
							if (!awaiter.IsCompleted)
							{
								this.<>1__state = 0;
								this.<>u__1 = awaiter;
								this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter, DtdParser.<ScanEntity2Async>d__190>(ref awaiter, ref this);
								return;
							}
							break;
						}
						dtdParser.Throw(dtdParser.curPos, "Expecting an external identifier or an entity value.");
						result = DtdParser.Token.None;
						goto IL_223;
					}
					}
					if (!awaiter.GetResult())
					{
						dtdParser.Throw(dtdParser.curPos, "Expecting external ID, '[' or '>'.");
					}
					dtdParser.nextScaningFunction = DtdParser.ScanningFunction.Entity3;
					dtdParser.scanningFunction = DtdParser.ScanningFunction.PublicId1;
					result = DtdParser.Token.PUBLIC;
					goto IL_223;
					IL_14E:
					if (!awaiter.GetResult())
					{
						dtdParser.Throw(dtdParser.curPos, "Expecting external ID, '[' or '>'.");
					}
					dtdParser.nextScaningFunction = DtdParser.ScanningFunction.Entity3;
					dtdParser.scanningFunction = DtdParser.ScanningFunction.SystemId;
					result = DtdParser.Token.SYSTEM;
					goto IL_223;
					IL_1DF:
					awaiter2.GetResult();
					dtdParser.scanningFunction = DtdParser.ScanningFunction.ClosingTag;
					result = DtdParser.Token.Literal;
				}
				catch (Exception exception)
				{
					this.<>1__state = -2;
					this.<>t__builder.SetException(exception);
					return;
				}
				IL_223:
				this.<>1__state = -2;
				this.<>t__builder.SetResult(result);
			}

			// Token: 0x06001420 RID: 5152 RVA: 0x0007E768 File Offset: 0x0007C968
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04001240 RID: 4672
			public int <>1__state;

			// Token: 0x04001241 RID: 4673
			public AsyncTaskMethodBuilder<DtdParser.Token> <>t__builder;

			// Token: 0x04001242 RID: 4674
			public DtdParser <>4__this;

			// Token: 0x04001243 RID: 4675
			private ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter <>u__1;

			// Token: 0x04001244 RID: 4676
			private ConfiguredTaskAwaitable<DtdParser.Token>.ConfiguredTaskAwaiter <>u__2;
		}

		// Token: 0x02000215 RID: 533
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <ScanEntity3Async>d__191 : IAsyncStateMachine
		{
			// Token: 0x06001421 RID: 5153 RVA: 0x0007E778 File Offset: 0x0007C978
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				DtdParser dtdParser = this.<>4__this;
				DtdParser.Token result;
				try
				{
					ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter awaiter;
					if (num != 0)
					{
						if (dtdParser.chars[dtdParser.curPos] == 'N')
						{
							goto IL_8E;
						}
						goto IL_10C;
					}
					else
					{
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
					}
					IL_85:
					if (awaiter.GetResult() == 0)
					{
						goto IL_10C;
					}
					IL_8E:
					if (dtdParser.charsUsed - dtdParser.curPos >= 5)
					{
						if (dtdParser.chars[dtdParser.curPos + 1] == 'D' && dtdParser.chars[dtdParser.curPos + 2] == 'A' && dtdParser.chars[dtdParser.curPos + 3] == 'T' && dtdParser.chars[dtdParser.curPos + 4] == 'A')
						{
							dtdParser.curPos += 5;
							dtdParser.scanningFunction = DtdParser.ScanningFunction.Name;
							dtdParser.nextScaningFunction = DtdParser.ScanningFunction.ClosingTag;
							result = DtdParser.Token.NData;
							goto IL_132;
						}
					}
					else
					{
						awaiter = dtdParser.ReadDataAsync().ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 0;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter, DtdParser.<ScanEntity3Async>d__191>(ref awaiter, ref this);
							return;
						}
						goto IL_85;
					}
					IL_10C:
					dtdParser.scanningFunction = DtdParser.ScanningFunction.ClosingTag;
					result = DtdParser.Token.None;
				}
				catch (Exception exception)
				{
					this.<>1__state = -2;
					this.<>t__builder.SetException(exception);
					return;
				}
				IL_132:
				this.<>1__state = -2;
				this.<>t__builder.SetResult(result);
			}

			// Token: 0x06001422 RID: 5154 RVA: 0x0007E8E8 File Offset: 0x0007CAE8
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04001245 RID: 4677
			public int <>1__state;

			// Token: 0x04001246 RID: 4678
			public AsyncTaskMethodBuilder<DtdParser.Token> <>t__builder;

			// Token: 0x04001247 RID: 4679
			public DtdParser <>4__this;

			// Token: 0x04001248 RID: 4680
			private ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter <>u__1;
		}

		// Token: 0x02000216 RID: 534
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <ScanPublicId1Async>d__192 : IAsyncStateMachine
		{
			// Token: 0x06001423 RID: 5155 RVA: 0x0007E8F8 File Offset: 0x0007CAF8
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				DtdParser dtdParser = this.<>4__this;
				DtdParser.Token result;
				try
				{
					ConfiguredTaskAwaitable<DtdParser.Token>.ConfiguredTaskAwaiter awaiter;
					if (num != 0)
					{
						if (dtdParser.chars[dtdParser.curPos] != '"' && dtdParser.chars[dtdParser.curPos] != '\'')
						{
							dtdParser.ThrowUnexpectedToken(dtdParser.curPos, "\"", "'");
						}
						awaiter = dtdParser.ScanLiteralAsync(DtdParser.LiteralType.SystemOrPublicID).ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 0;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<DtdParser.Token>.ConfiguredTaskAwaiter, DtdParser.<ScanPublicId1Async>d__192>(ref awaiter, ref this);
							return;
						}
					}
					else
					{
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable<DtdParser.Token>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
					}
					awaiter.GetResult();
					dtdParser.scanningFunction = DtdParser.ScanningFunction.PublicId2;
					result = DtdParser.Token.Literal;
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

			// Token: 0x06001424 RID: 5156 RVA: 0x0007E9FC File Offset: 0x0007CBFC
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04001249 RID: 4681
			public int <>1__state;

			// Token: 0x0400124A RID: 4682
			public AsyncTaskMethodBuilder<DtdParser.Token> <>t__builder;

			// Token: 0x0400124B RID: 4683
			public DtdParser <>4__this;

			// Token: 0x0400124C RID: 4684
			private ConfiguredTaskAwaitable<DtdParser.Token>.ConfiguredTaskAwaiter <>u__1;
		}

		// Token: 0x02000217 RID: 535
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <ScanPublicId2Async>d__193 : IAsyncStateMachine
		{
			// Token: 0x06001425 RID: 5157 RVA: 0x0007EA0C File Offset: 0x0007CC0C
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				DtdParser dtdParser = this.<>4__this;
				DtdParser.Token result;
				try
				{
					ConfiguredTaskAwaitable<DtdParser.Token>.ConfiguredTaskAwaiter awaiter;
					if (num != 0)
					{
						if (dtdParser.chars[dtdParser.curPos] != '"' && dtdParser.chars[dtdParser.curPos] != '\'')
						{
							dtdParser.scanningFunction = dtdParser.nextScaningFunction;
							result = DtdParser.Token.None;
							goto IL_D5;
						}
						awaiter = dtdParser.ScanLiteralAsync(DtdParser.LiteralType.SystemOrPublicID).ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 0;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<DtdParser.Token>.ConfiguredTaskAwaiter, DtdParser.<ScanPublicId2Async>d__193>(ref awaiter, ref this);
							return;
						}
					}
					else
					{
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable<DtdParser.Token>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
					}
					awaiter.GetResult();
					dtdParser.scanningFunction = dtdParser.nextScaningFunction;
					result = DtdParser.Token.Literal;
				}
				catch (Exception exception)
				{
					this.<>1__state = -2;
					this.<>t__builder.SetException(exception);
					return;
				}
				IL_D5:
				this.<>1__state = -2;
				this.<>t__builder.SetResult(result);
			}

			// Token: 0x06001426 RID: 5158 RVA: 0x0007EB14 File Offset: 0x0007CD14
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x0400124D RID: 4685
			public int <>1__state;

			// Token: 0x0400124E RID: 4686
			public AsyncTaskMethodBuilder<DtdParser.Token> <>t__builder;

			// Token: 0x0400124F RID: 4687
			public DtdParser <>4__this;

			// Token: 0x04001250 RID: 4688
			private ConfiguredTaskAwaitable<DtdParser.Token>.ConfiguredTaskAwaiter <>u__1;
		}

		// Token: 0x02000218 RID: 536
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <ScanCondSection1Async>d__194 : IAsyncStateMachine
		{
			// Token: 0x06001427 RID: 5159 RVA: 0x0007EB24 File Offset: 0x0007CD24
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				DtdParser dtdParser = this.<>4__this;
				DtdParser.Token result;
				try
				{
					ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter awaiter;
					if (num == 0)
					{
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_241;
					}
					if (dtdParser.chars[dtdParser.curPos] != 'I')
					{
						dtdParser.Throw(dtdParser.curPos, "Conditional sections must specify the keyword 'IGNORE' or 'INCLUDE'.");
					}
					dtdParser.curPos++;
					IL_44:
					if (dtdParser.charsUsed - dtdParser.curPos >= 5)
					{
						char c = dtdParser.chars[dtdParser.curPos];
						if (c != 'G')
						{
							if (c == 'N')
							{
								if (dtdParser.charsUsed - dtdParser.curPos < 6)
								{
									goto IL_1E3;
								}
								if (dtdParser.chars[dtdParser.curPos + 1] == 'C' && dtdParser.chars[dtdParser.curPos + 2] == 'L' && dtdParser.chars[dtdParser.curPos + 3] == 'U' && dtdParser.chars[dtdParser.curPos + 4] == 'D' && dtdParser.chars[dtdParser.curPos + 5] == 'E' && !dtdParser.xmlCharType.IsNameSingleChar(dtdParser.chars[dtdParser.curPos + 6]))
								{
									dtdParser.nextScaningFunction = DtdParser.ScanningFunction.SubsetContent;
									dtdParser.scanningFunction = DtdParser.ScanningFunction.CondSection2;
									dtdParser.curPos += 6;
									result = DtdParser.Token.INCLUDE;
									goto IL_27C;
								}
							}
						}
						else if (dtdParser.chars[dtdParser.curPos + 1] == 'N' && dtdParser.chars[dtdParser.curPos + 2] == 'O' && dtdParser.chars[dtdParser.curPos + 3] == 'R' && dtdParser.chars[dtdParser.curPos + 4] == 'E' && !dtdParser.xmlCharType.IsNameSingleChar(dtdParser.chars[dtdParser.curPos + 5]))
						{
							dtdParser.nextScaningFunction = DtdParser.ScanningFunction.CondSection3;
							dtdParser.scanningFunction = DtdParser.ScanningFunction.CondSection2;
							dtdParser.curPos += 5;
							result = DtdParser.Token.IGNORE;
							goto IL_27C;
						}
						dtdParser.Throw(dtdParser.curPos - 1, "Conditional sections must specify the keyword 'IGNORE' or 'INCLUDE'.");
						result = DtdParser.Token.None;
						goto IL_27C;
					}
					IL_1E3:
					awaiter = dtdParser.ReadDataAsync().ConfigureAwait(false).GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						this.<>1__state = 0;
						this.<>u__1 = awaiter;
						this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter, DtdParser.<ScanCondSection1Async>d__194>(ref awaiter, ref this);
						return;
					}
					IL_241:
					if (awaiter.GetResult() == 0)
					{
						dtdParser.Throw(dtdParser.curPos, "Incomplete DTD content.");
						goto IL_44;
					}
					goto IL_44;
				}
				catch (Exception exception)
				{
					this.<>1__state = -2;
					this.<>t__builder.SetException(exception);
					return;
				}
				IL_27C:
				this.<>1__state = -2;
				this.<>t__builder.SetResult(result);
			}

			// Token: 0x06001428 RID: 5160 RVA: 0x0007EDE0 File Offset: 0x0007CFE0
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04001251 RID: 4689
			public int <>1__state;

			// Token: 0x04001252 RID: 4690
			public AsyncTaskMethodBuilder<DtdParser.Token> <>t__builder;

			// Token: 0x04001253 RID: 4691
			public DtdParser <>4__this;

			// Token: 0x04001254 RID: 4692
			private ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter <>u__1;
		}

		// Token: 0x02000219 RID: 537
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <ScanCondSection3Async>d__195 : IAsyncStateMachine
		{
			// Token: 0x06001429 RID: 5161 RVA: 0x0007EDF0 File Offset: 0x0007CFF0
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				DtdParser dtdParser = this.<>4__this;
				DtdParser.Token result;
				try
				{
					ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter awaiter;
					if (num == 0)
					{
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_352;
					}
					this.<ignoreSectionDepth>5__2 = 0;
					for (;;)
					{
						IL_2B:
						if ((dtdParser.xmlCharType.charProperties[(int)dtdParser.chars[dtdParser.curPos]] & 64) == 0 || dtdParser.chars[dtdParser.curPos] == ']')
						{
							char c = dtdParser.chars[dtdParser.curPos];
							if (c <= '&')
							{
								switch (c)
								{
								case '\t':
									break;
								case '\n':
									dtdParser.curPos++;
									dtdParser.readerAdapter.OnNewLine(dtdParser.curPos);
									continue;
								case '\v':
								case '\f':
									goto IL_259;
								case '\r':
									if (dtdParser.chars[dtdParser.curPos + 1] == '\n')
									{
										dtdParser.curPos += 2;
									}
									else
									{
										if (dtdParser.curPos + 1 >= dtdParser.charsUsed && !dtdParser.readerAdapter.IsEof)
										{
											goto IL_2E0;
										}
										dtdParser.curPos++;
									}
									dtdParser.readerAdapter.OnNewLine(dtdParser.curPos);
									continue;
								default:
									if (c != '"' && c != '&')
									{
										goto IL_259;
									}
									break;
								}
							}
							else if (c != '\'')
							{
								if (c != '<')
								{
									if (c != ']')
									{
										goto IL_259;
									}
									if (dtdParser.charsUsed - dtdParser.curPos < 3)
									{
										goto IL_2E0;
									}
									if (dtdParser.chars[dtdParser.curPos + 1] != ']' || dtdParser.chars[dtdParser.curPos + 2] != '>')
									{
										dtdParser.curPos++;
										continue;
									}
									if (this.<ignoreSectionDepth>5__2 > 0)
									{
										int num2 = this.<ignoreSectionDepth>5__2;
										this.<ignoreSectionDepth>5__2 = num2 - 1;
										dtdParser.curPos += 3;
										continue;
									}
									break;
								}
								else
								{
									if (dtdParser.charsUsed - dtdParser.curPos < 3)
									{
										goto IL_2E0;
									}
									if (dtdParser.chars[dtdParser.curPos + 1] != '!' || dtdParser.chars[dtdParser.curPos + 2] != '[')
									{
										dtdParser.curPos++;
										continue;
									}
									int num2 = this.<ignoreSectionDepth>5__2;
									this.<ignoreSectionDepth>5__2 = num2 + 1;
									dtdParser.curPos += 3;
									continue;
								}
							}
							dtdParser.curPos++;
							continue;
							IL_259:
							if (dtdParser.curPos == dtdParser.charsUsed)
							{
								goto IL_2E0;
							}
							if (!XmlCharType.IsHighSurrogate((int)dtdParser.chars[dtdParser.curPos]))
							{
								goto IL_2C0;
							}
							if (dtdParser.curPos + 1 == dtdParser.charsUsed)
							{
								goto IL_2E0;
							}
							dtdParser.curPos++;
							if (!XmlCharType.IsLowSurrogate((int)dtdParser.chars[dtdParser.curPos]))
							{
								goto IL_2C0;
							}
							dtdParser.curPos++;
						}
						else
						{
							dtdParser.curPos++;
						}
					}
					dtdParser.curPos += 3;
					dtdParser.scanningFunction = DtdParser.ScanningFunction.SubsetContent;
					result = DtdParser.Token.CondSectionEnd;
					goto IL_3A9;
					IL_2C0:
					dtdParser.ThrowInvalidChar(dtdParser.chars, dtdParser.charsUsed, dtdParser.curPos);
					result = DtdParser.Token.None;
					goto IL_3A9;
					IL_2E0:
					bool flag = dtdParser.readerAdapter.IsEof;
					if (flag)
					{
						goto IL_35E;
					}
					awaiter = dtdParser.ReadDataAsync().ConfigureAwait(false).GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						this.<>1__state = 0;
						this.<>u__1 = awaiter;
						this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter, DtdParser.<ScanCondSection3Async>d__195>(ref awaiter, ref this);
						return;
					}
					IL_352:
					flag = (awaiter.GetResult() == 0);
					IL_35E:
					if (flag)
					{
						if (dtdParser.HandleEntityEnd(false))
						{
							goto IL_2B;
						}
						dtdParser.Throw(dtdParser.curPos, "There is an unclosed conditional section.");
					}
					dtdParser.tokenStartPos = dtdParser.curPos;
					goto IL_2B;
				}
				catch (Exception exception)
				{
					this.<>1__state = -2;
					this.<>t__builder.SetException(exception);
					return;
				}
				IL_3A9:
				this.<>1__state = -2;
				this.<>t__builder.SetResult(result);
			}

			// Token: 0x0600142A RID: 5162 RVA: 0x0007F1D8 File Offset: 0x0007D3D8
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04001255 RID: 4693
			public int <>1__state;

			// Token: 0x04001256 RID: 4694
			public AsyncTaskMethodBuilder<DtdParser.Token> <>t__builder;

			// Token: 0x04001257 RID: 4695
			public DtdParser <>4__this;

			// Token: 0x04001258 RID: 4696
			private int <ignoreSectionDepth>5__2;

			// Token: 0x04001259 RID: 4697
			private ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter <>u__1;
		}

		// Token: 0x0200021A RID: 538
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <ScanQNameAsync>d__198 : IAsyncStateMachine
		{
			// Token: 0x0600142B RID: 5163 RVA: 0x0007F1E8 File Offset: 0x0007D3E8
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				DtdParser dtdParser = this.<>4__this;
				try
				{
					ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter awaiter;
					if (num == 0)
					{
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_10E;
					}
					if (num != 1)
					{
						dtdParser.tokenStartPos = dtdParser.curPos;
						this.<colonOffset>5__2 = -1;
						goto IL_2E;
					}
					awaiter = this.<>u__1;
					this.<>u__1 = default(ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter);
					this.<>1__state = -1;
					goto IL_240;
					for (;;)
					{
						IL_130:
						if ((dtdParser.xmlCharType.charProperties[(int)dtdParser.chars[dtdParser.curPos]] & 8) != 0)
						{
							dtdParser.curPos++;
						}
						else
						{
							if (dtdParser.chars[dtdParser.curPos] != ':')
							{
								goto IL_1D1;
							}
							if (this.isQName)
							{
								break;
							}
							dtdParser.curPos++;
						}
					}
					if (this.<colonOffset>5__2 != -1)
					{
						dtdParser.Throw(dtdParser.curPos, "The '{0}' character, hexadecimal value {1}, cannot be included in a name.", XmlException.BuildCharExceptionArgs(':', '\0'));
					}
					this.<colonOffset>5__2 = dtdParser.curPos - dtdParser.tokenStartPos;
					dtdParser.curPos++;
					goto IL_2E;
					IL_1D1:
					if (dtdParser.curPos != dtdParser.charsUsed)
					{
						goto IL_270;
					}
					awaiter = dtdParser.ReadDataInNameAsync().ConfigureAwait(false).GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						this.<>1__state = 1;
						this.<>u__1 = awaiter;
						this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter, DtdParser.<ScanQNameAsync>d__198>(ref awaiter, ref this);
						return;
					}
					goto IL_240;
					IL_2E:
					bool flag = false;
					if ((dtdParser.xmlCharType.charProperties[(int)dtdParser.chars[dtdParser.curPos]] & 4) != 0 || dtdParser.chars[dtdParser.curPos] == ':')
					{
						dtdParser.curPos++;
					}
					else if (dtdParser.curPos + 1 >= dtdParser.charsUsed)
					{
						flag = true;
					}
					else
					{
						dtdParser.Throw(dtdParser.curPos, "Name cannot begin with the '{0}' character, hexadecimal value {1}.", XmlException.BuildCharExceptionArgs(dtdParser.chars, dtdParser.charsUsed, dtdParser.curPos));
					}
					if (!flag)
					{
						goto IL_130;
					}
					awaiter = dtdParser.ReadDataInNameAsync().ConfigureAwait(false).GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						this.<>1__state = 0;
						this.<>u__1 = awaiter;
						this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter, DtdParser.<ScanQNameAsync>d__198>(ref awaiter, ref this);
						return;
					}
					IL_10E:
					if (!awaiter.GetResult())
					{
						dtdParser.Throw(dtdParser.curPos, "Unexpected end of file while parsing {0} has occurred.", "Name");
						goto IL_130;
					}
					goto IL_2E;
					IL_240:
					if (awaiter.GetResult())
					{
						goto IL_130;
					}
					if (dtdParser.tokenStartPos == dtdParser.curPos)
					{
						dtdParser.Throw(dtdParser.curPos, "Unexpected end of file while parsing {0} has occurred.", "Name");
					}
					IL_270:
					dtdParser.colonPos = ((this.<colonOffset>5__2 == -1) ? -1 : (dtdParser.tokenStartPos + this.<colonOffset>5__2));
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

			// Token: 0x0600142C RID: 5164 RVA: 0x0007F4D0 File Offset: 0x0007D6D0
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x0400125A RID: 4698
			public int <>1__state;

			// Token: 0x0400125B RID: 4699
			public AsyncTaskMethodBuilder <>t__builder;

			// Token: 0x0400125C RID: 4700
			public DtdParser <>4__this;

			// Token: 0x0400125D RID: 4701
			public bool isQName;

			// Token: 0x0400125E RID: 4702
			private int <colonOffset>5__2;

			// Token: 0x0400125F RID: 4703
			private ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter <>u__1;
		}

		// Token: 0x0200021B RID: 539
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <ReadDataInNameAsync>d__199 : IAsyncStateMachine
		{
			// Token: 0x0600142D RID: 5165 RVA: 0x0007F4E0 File Offset: 0x0007D6E0
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				DtdParser dtdParser = this.<>4__this;
				bool result;
				try
				{
					ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter awaiter;
					if (num != 0)
					{
						this.<offset>5__2 = dtdParser.curPos - dtdParser.tokenStartPos;
						dtdParser.curPos = dtdParser.tokenStartPos;
						awaiter = dtdParser.ReadDataAsync().ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 0;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter, DtdParser.<ReadDataInNameAsync>d__199>(ref awaiter, ref this);
							return;
						}
					}
					else
					{
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
					}
					bool flag = awaiter.GetResult() != 0;
					dtdParser.tokenStartPos = dtdParser.curPos;
					dtdParser.curPos += this.<offset>5__2;
					result = flag;
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

			// Token: 0x0600142E RID: 5166 RVA: 0x0007F5E4 File Offset: 0x0007D7E4
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04001260 RID: 4704
			public int <>1__state;

			// Token: 0x04001261 RID: 4705
			public AsyncTaskMethodBuilder<bool> <>t__builder;

			// Token: 0x04001262 RID: 4706
			public DtdParser <>4__this;

			// Token: 0x04001263 RID: 4707
			private int <offset>5__2;

			// Token: 0x04001264 RID: 4708
			private ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter <>u__1;
		}

		// Token: 0x0200021C RID: 540
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <ScanNmtokenAsync>d__200 : IAsyncStateMachine
		{
			// Token: 0x0600142F RID: 5167 RVA: 0x0007F5F4 File Offset: 0x0007D7F4
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				DtdParser dtdParser = this.<>4__this;
				try
				{
					ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter awaiter;
					if (num == 0)
					{
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_124;
					}
					dtdParser.tokenStartPos = dtdParser.curPos;
					IL_20:
					while ((dtdParser.xmlCharType.charProperties[(int)dtdParser.chars[dtdParser.curPos]] & 8) != 0 || dtdParser.chars[dtdParser.curPos] == ':')
					{
						dtdParser.curPos++;
					}
					if (dtdParser.curPos < dtdParser.charsUsed)
					{
						if (dtdParser.curPos - dtdParser.tokenStartPos == 0)
						{
							dtdParser.Throw(dtdParser.curPos, "The '{0}' character, hexadecimal value {1}, cannot be included in a name.", XmlException.BuildCharExceptionArgs(dtdParser.chars, dtdParser.charsUsed, dtdParser.curPos));
						}
						goto IL_1AA;
					}
					this.<len>5__2 = dtdParser.curPos - dtdParser.tokenStartPos;
					dtdParser.curPos = dtdParser.tokenStartPos;
					awaiter = dtdParser.ReadDataAsync().ConfigureAwait(false).GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						this.<>1__state = 0;
						this.<>u__1 = awaiter;
						this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter, DtdParser.<ScanNmtokenAsync>d__200>(ref awaiter, ref this);
						return;
					}
					IL_124:
					if (awaiter.GetResult() == 0)
					{
						if (this.<len>5__2 > 0)
						{
							dtdParser.tokenStartPos = dtdParser.curPos;
							dtdParser.curPos += this.<len>5__2;
							goto IL_1AA;
						}
						dtdParser.Throw(dtdParser.curPos, "Unexpected end of file while parsing {0} has occurred.", "NmToken");
					}
					dtdParser.tokenStartPos = dtdParser.curPos;
					dtdParser.curPos += this.<len>5__2;
					goto IL_20;
				}
				catch (Exception exception)
				{
					this.<>1__state = -2;
					this.<>t__builder.SetException(exception);
					return;
				}
				IL_1AA:
				this.<>1__state = -2;
				this.<>t__builder.SetResult();
			}

			// Token: 0x06001430 RID: 5168 RVA: 0x0007F7DC File Offset: 0x0007D9DC
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04001265 RID: 4709
			public int <>1__state;

			// Token: 0x04001266 RID: 4710
			public AsyncTaskMethodBuilder <>t__builder;

			// Token: 0x04001267 RID: 4711
			public DtdParser <>4__this;

			// Token: 0x04001268 RID: 4712
			private int <len>5__2;

			// Token: 0x04001269 RID: 4713
			private ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter <>u__1;
		}

		// Token: 0x0200021D RID: 541
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <EatPublicKeywordAsync>d__201 : IAsyncStateMachine
		{
			// Token: 0x06001431 RID: 5169 RVA: 0x0007F7EC File Offset: 0x0007D9EC
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				DtdParser dtdParser = this.<>4__this;
				bool result;
				try
				{
					if (num != 0)
					{
						goto IL_81;
					}
					ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter awaiter = this.<>u__1;
					this.<>u__1 = default(ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter);
					this.<>1__state = -1;
					IL_71:
					if (awaiter.GetResult() == 0)
					{
						result = false;
						goto IL_11F;
					}
					IL_81:
					if (dtdParser.charsUsed - dtdParser.curPos >= 6)
					{
						if (dtdParser.chars[dtdParser.curPos + 1] != 'U' || dtdParser.chars[dtdParser.curPos + 2] != 'B' || dtdParser.chars[dtdParser.curPos + 3] != 'L' || dtdParser.chars[dtdParser.curPos + 4] != 'I' || dtdParser.chars[dtdParser.curPos + 5] != 'C')
						{
							result = false;
						}
						else
						{
							dtdParser.curPos += 6;
							result = true;
						}
					}
					else
					{
						awaiter = dtdParser.ReadDataAsync().ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 0;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter, DtdParser.<EatPublicKeywordAsync>d__201>(ref awaiter, ref this);
							return;
						}
						goto IL_71;
					}
				}
				catch (Exception exception)
				{
					this.<>1__state = -2;
					this.<>t__builder.SetException(exception);
					return;
				}
				IL_11F:
				this.<>1__state = -2;
				this.<>t__builder.SetResult(result);
			}

			// Token: 0x06001432 RID: 5170 RVA: 0x0007F93C File Offset: 0x0007DB3C
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x0400126A RID: 4714
			public int <>1__state;

			// Token: 0x0400126B RID: 4715
			public AsyncTaskMethodBuilder<bool> <>t__builder;

			// Token: 0x0400126C RID: 4716
			public DtdParser <>4__this;

			// Token: 0x0400126D RID: 4717
			private ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter <>u__1;
		}

		// Token: 0x0200021E RID: 542
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <EatSystemKeywordAsync>d__202 : IAsyncStateMachine
		{
			// Token: 0x06001433 RID: 5171 RVA: 0x0007F94C File Offset: 0x0007DB4C
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				DtdParser dtdParser = this.<>4__this;
				bool result;
				try
				{
					if (num != 0)
					{
						goto IL_81;
					}
					ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter awaiter = this.<>u__1;
					this.<>u__1 = default(ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter);
					this.<>1__state = -1;
					IL_71:
					if (awaiter.GetResult() == 0)
					{
						result = false;
						goto IL_11F;
					}
					IL_81:
					if (dtdParser.charsUsed - dtdParser.curPos >= 6)
					{
						if (dtdParser.chars[dtdParser.curPos + 1] != 'Y' || dtdParser.chars[dtdParser.curPos + 2] != 'S' || dtdParser.chars[dtdParser.curPos + 3] != 'T' || dtdParser.chars[dtdParser.curPos + 4] != 'E' || dtdParser.chars[dtdParser.curPos + 5] != 'M')
						{
							result = false;
						}
						else
						{
							dtdParser.curPos += 6;
							result = true;
						}
					}
					else
					{
						awaiter = dtdParser.ReadDataAsync().ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 0;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter, DtdParser.<EatSystemKeywordAsync>d__202>(ref awaiter, ref this);
							return;
						}
						goto IL_71;
					}
				}
				catch (Exception exception)
				{
					this.<>1__state = -2;
					this.<>t__builder.SetException(exception);
					return;
				}
				IL_11F:
				this.<>1__state = -2;
				this.<>t__builder.SetResult(result);
			}

			// Token: 0x06001434 RID: 5172 RVA: 0x0007FA9C File Offset: 0x0007DC9C
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x0400126E RID: 4718
			public int <>1__state;

			// Token: 0x0400126F RID: 4719
			public AsyncTaskMethodBuilder<bool> <>t__builder;

			// Token: 0x04001270 RID: 4720
			public DtdParser <>4__this;

			// Token: 0x04001271 RID: 4721
			private ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter <>u__1;
		}

		// Token: 0x0200021F RID: 543
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <ReadDataAsync>d__203 : IAsyncStateMachine
		{
			// Token: 0x06001435 RID: 5173 RVA: 0x0007FAAC File Offset: 0x0007DCAC
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				DtdParser dtdParser = this.<>4__this;
				int result2;
				try
				{
					ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter awaiter;
					if (num != 0)
					{
						dtdParser.SaveParsingBuffer();
						awaiter = dtdParser.readerAdapter.ReadDataAsync().ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 0;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter, DtdParser.<ReadDataAsync>d__203>(ref awaiter, ref this);
							return;
						}
					}
					else
					{
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
					}
					int result = awaiter.GetResult();
					dtdParser.LoadParsingBuffer();
					result2 = result;
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

			// Token: 0x06001436 RID: 5174 RVA: 0x0007FB80 File Offset: 0x0007DD80
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04001272 RID: 4722
			public int <>1__state;

			// Token: 0x04001273 RID: 4723
			public AsyncTaskMethodBuilder<int> <>t__builder;

			// Token: 0x04001274 RID: 4724
			public DtdParser <>4__this;

			// Token: 0x04001275 RID: 4725
			private ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter <>u__1;
		}

		// Token: 0x02000220 RID: 544
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <HandleEntityReferenceAsync>d__205 : IAsyncStateMachine
		{
			// Token: 0x06001437 RID: 5175 RVA: 0x0007FB90 File Offset: 0x0007DD90
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				DtdParser dtdParser = this.<>4__this;
				bool result;
				try
				{
					ConfiguredTaskAwaitable<Tuple<int, bool>>.ConfiguredTaskAwaiter awaiter;
					int item;
					if (num != 0)
					{
						if (num != 1)
						{
							dtdParser.SaveParsingBuffer();
							if (this.paramEntity && dtdParser.ParsingInternalSubset && !dtdParser.ParsingTopLevelMarkup)
							{
								dtdParser.Throw(dtdParser.curPos - this.entityName.Name.Length - 1, "A parameter entity reference is not allowed in internal markup.");
							}
							SchemaEntity schemaEntity = dtdParser.VerifyEntityReference(this.entityName, this.paramEntity, true, this.inAttribute);
							if (schemaEntity == null)
							{
								result = false;
								goto IL_257;
							}
							if (schemaEntity.ParsingInProgress)
							{
								dtdParser.Throw(dtdParser.curPos - this.entityName.Name.Length - 1, this.paramEntity ? "Parameter entity '{0}' references itself." : "General entity '{0}' references itself.", this.entityName.Name);
							}
							if (schemaEntity.IsExternal)
							{
								awaiter = dtdParser.readerAdapter.PushEntityAsync(schemaEntity).ConfigureAwait(false).GetAwaiter();
								if (!awaiter.IsCompleted)
								{
									this.<>1__state = 0;
									this.<>u__1 = awaiter;
									this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<Tuple<int, bool>>.ConfiguredTaskAwaiter, DtdParser.<HandleEntityReferenceAsync>d__205>(ref awaiter, ref this);
									return;
								}
								goto IL_139;
							}
							else
							{
								if (schemaEntity.Text.Length == 0)
								{
									result = false;
									goto IL_257;
								}
								awaiter = dtdParser.readerAdapter.PushEntityAsync(schemaEntity).ConfigureAwait(false).GetAwaiter();
								if (!awaiter.IsCompleted)
								{
									this.<>1__state = 1;
									this.<>u__1 = awaiter;
									this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<Tuple<int, bool>>.ConfiguredTaskAwaiter, DtdParser.<HandleEntityReferenceAsync>d__205>(ref awaiter, ref this);
									return;
								}
							}
						}
						else
						{
							awaiter = this.<>u__1;
							this.<>u__1 = default(ConfiguredTaskAwaitable<Tuple<int, bool>>.ConfiguredTaskAwaiter);
							this.<>1__state = -1;
						}
						Tuple<int, bool> result2 = awaiter.GetResult();
						item = result2.Item1;
						if (!result2.Item2)
						{
							result = false;
							goto IL_257;
						}
						goto IL_1FE;
					}
					else
					{
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable<Tuple<int, bool>>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
					}
					IL_139:
					Tuple<int, bool> result3 = awaiter.GetResult();
					item = result3.Item1;
					if (!result3.Item2)
					{
						result = false;
						goto IL_257;
					}
					dtdParser.externalEntitiesDepth++;
					IL_1FE:
					dtdParser.currentEntityId = item;
					if (this.paramEntity && !this.inLiteral && dtdParser.scanningFunction != DtdParser.ScanningFunction.ParamEntitySpace)
					{
						dtdParser.savedScanningFunction = dtdParser.scanningFunction;
						dtdParser.scanningFunction = DtdParser.ScanningFunction.ParamEntitySpace;
					}
					dtdParser.LoadParsingBuffer();
					result = true;
				}
				catch (Exception exception)
				{
					this.<>1__state = -2;
					this.<>t__builder.SetException(exception);
					return;
				}
				IL_257:
				this.<>1__state = -2;
				this.<>t__builder.SetResult(result);
			}

			// Token: 0x06001438 RID: 5176 RVA: 0x0007FE24 File Offset: 0x0007E024
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04001276 RID: 4726
			public int <>1__state;

			// Token: 0x04001277 RID: 4727
			public AsyncTaskMethodBuilder<bool> <>t__builder;

			// Token: 0x04001278 RID: 4728
			public DtdParser <>4__this;

			// Token: 0x04001279 RID: 4729
			public bool paramEntity;

			// Token: 0x0400127A RID: 4730
			public XmlQualifiedName entityName;

			// Token: 0x0400127B RID: 4731
			public bool inAttribute;

			// Token: 0x0400127C RID: 4732
			public bool inLiteral;

			// Token: 0x0400127D RID: 4733
			private ConfiguredTaskAwaitable<Tuple<int, bool>>.ConfiguredTaskAwaiter <>u__1;
		}
	}
}

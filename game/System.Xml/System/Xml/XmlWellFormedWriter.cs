using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace System.Xml
{
	// Token: 0x0200014C RID: 332
	internal class XmlWellFormedWriter : XmlWriter
	{
		// Token: 0x06000C08 RID: 3080 RVA: 0x0004EFF4 File Offset: 0x0004D1F4
		internal XmlWellFormedWriter(XmlWriter writer, XmlWriterSettings settings)
		{
			this.writer = writer;
			this.rawWriter = (writer as XmlRawWriter);
			this.predefinedNamespaces = (writer as IXmlNamespaceResolver);
			if (this.rawWriter != null)
			{
				this.rawWriter.NamespaceResolver = new XmlWellFormedWriter.NamespaceResolverProxy(this);
			}
			this.checkCharacters = settings.CheckCharacters;
			this.omitDuplNamespaces = ((settings.NamespaceHandling & NamespaceHandling.OmitDuplicates) > NamespaceHandling.Default);
			this.writeEndDocumentOnClose = settings.WriteEndDocumentOnClose;
			this.conformanceLevel = settings.ConformanceLevel;
			this.stateTable = ((this.conformanceLevel == ConformanceLevel.Document) ? XmlWellFormedWriter.StateTableDocument : XmlWellFormedWriter.StateTableAuto);
			this.currentState = XmlWellFormedWriter.State.Start;
			this.nsStack = new XmlWellFormedWriter.Namespace[8];
			this.nsStack[0].Set("xmlns", "http://www.w3.org/2000/xmlns/", XmlWellFormedWriter.NamespaceKind.Special);
			this.nsStack[1].Set("xml", "http://www.w3.org/XML/1998/namespace", XmlWellFormedWriter.NamespaceKind.Special);
			if (this.predefinedNamespaces == null)
			{
				this.nsStack[2].Set(string.Empty, string.Empty, XmlWellFormedWriter.NamespaceKind.Implied);
			}
			else
			{
				string text = this.predefinedNamespaces.LookupNamespace(string.Empty);
				this.nsStack[2].Set(string.Empty, (text == null) ? string.Empty : text, XmlWellFormedWriter.NamespaceKind.Implied);
			}
			this.nsTop = 2;
			this.elemScopeStack = new XmlWellFormedWriter.ElementScope[8];
			this.elemScopeStack[0].Set(string.Empty, string.Empty, string.Empty, this.nsTop);
			this.elemScopeStack[0].xmlSpace = XmlSpace.None;
			this.elemScopeStack[0].xmlLang = null;
			this.elemTop = 0;
			this.attrStack = new XmlWellFormedWriter.AttrName[8];
			this.hasher = new SecureStringHasher();
		}

		// Token: 0x170001CF RID: 463
		// (get) Token: 0x06000C09 RID: 3081 RVA: 0x0004F1B9 File Offset: 0x0004D3B9
		public override WriteState WriteState
		{
			get
			{
				if (this.currentState <= XmlWellFormedWriter.State.Error)
				{
					return XmlWellFormedWriter.state2WriteState[(int)this.currentState];
				}
				return WriteState.Error;
			}
		}

		// Token: 0x170001D0 RID: 464
		// (get) Token: 0x06000C0A RID: 3082 RVA: 0x0004F1D4 File Offset: 0x0004D3D4
		public override XmlWriterSettings Settings
		{
			get
			{
				XmlWriterSettings settings = this.writer.Settings;
				settings.ReadOnly = false;
				settings.ConformanceLevel = this.conformanceLevel;
				if (this.omitDuplNamespaces)
				{
					settings.NamespaceHandling |= NamespaceHandling.OmitDuplicates;
				}
				settings.WriteEndDocumentOnClose = this.writeEndDocumentOnClose;
				settings.ReadOnly = true;
				return settings;
			}
		}

		// Token: 0x06000C0B RID: 3083 RVA: 0x0004F22A File Offset: 0x0004D42A
		public override void WriteStartDocument()
		{
			this.WriteStartDocumentImpl(XmlStandalone.Omit);
		}

		// Token: 0x06000C0C RID: 3084 RVA: 0x0004F233 File Offset: 0x0004D433
		public override void WriteStartDocument(bool standalone)
		{
			this.WriteStartDocumentImpl(standalone ? XmlStandalone.Yes : XmlStandalone.No);
		}

		// Token: 0x06000C0D RID: 3085 RVA: 0x0004F244 File Offset: 0x0004D444
		public override void WriteEndDocument()
		{
			try
			{
				while (this.elemTop > 0)
				{
					this.WriteEndElement();
				}
				int num = (int)this.currentState;
				this.AdvanceState(XmlWellFormedWriter.Token.EndDocument);
				if (num != 7)
				{
					throw new ArgumentException(Res.GetString("Document does not have a root element."));
				}
				if (this.rawWriter == null)
				{
					this.writer.WriteEndDocument();
				}
			}
			catch
			{
				this.currentState = XmlWellFormedWriter.State.Error;
				throw;
			}
		}

		// Token: 0x06000C0E RID: 3086 RVA: 0x0004F2B4 File Offset: 0x0004D4B4
		public override void WriteDocType(string name, string pubid, string sysid, string subset)
		{
			try
			{
				if (name == null || name.Length == 0)
				{
					throw new ArgumentException(Res.GetString("The empty string '' is not a valid name."));
				}
				XmlConvert.VerifyQName(name, ExceptionType.XmlException);
				if (this.conformanceLevel == ConformanceLevel.Fragment)
				{
					throw new InvalidOperationException(Res.GetString("DTD is not allowed in XML fragments."));
				}
				this.AdvanceState(XmlWellFormedWriter.Token.Dtd);
				if (this.dtdWritten)
				{
					this.currentState = XmlWellFormedWriter.State.Error;
					throw new InvalidOperationException(Res.GetString("The DTD has already been written out."));
				}
				if (this.conformanceLevel == ConformanceLevel.Auto)
				{
					this.conformanceLevel = ConformanceLevel.Document;
					this.stateTable = XmlWellFormedWriter.StateTableDocument;
				}
				if (this.checkCharacters)
				{
					int invCharIndex;
					if (pubid != null && (invCharIndex = this.xmlCharType.IsPublicId(pubid)) >= 0)
					{
						string name2 = "'{0}', hexadecimal value {1}, is an invalid character.";
						object[] args = XmlException.BuildCharExceptionArgs(pubid, invCharIndex);
						throw new ArgumentException(Res.GetString(name2, args), "pubid");
					}
					if (sysid != null && (invCharIndex = this.xmlCharType.IsOnlyCharData(sysid)) >= 0)
					{
						string name3 = "'{0}', hexadecimal value {1}, is an invalid character.";
						object[] args = XmlException.BuildCharExceptionArgs(sysid, invCharIndex);
						throw new ArgumentException(Res.GetString(name3, args), "sysid");
					}
					if (subset != null && (invCharIndex = this.xmlCharType.IsOnlyCharData(subset)) >= 0)
					{
						string name4 = "'{0}', hexadecimal value {1}, is an invalid character.";
						object[] args = XmlException.BuildCharExceptionArgs(subset, invCharIndex);
						throw new ArgumentException(Res.GetString(name4, args), "subset");
					}
				}
				this.writer.WriteDocType(name, pubid, sysid, subset);
				this.dtdWritten = true;
			}
			catch
			{
				this.currentState = XmlWellFormedWriter.State.Error;
				throw;
			}
		}

		// Token: 0x06000C0F RID: 3087 RVA: 0x0004F424 File Offset: 0x0004D624
		public override void WriteStartElement(string prefix, string localName, string ns)
		{
			try
			{
				if (localName == null || localName.Length == 0)
				{
					throw new ArgumentException(Res.GetString("The empty string '' is not a valid local name."));
				}
				this.CheckNCName(localName);
				this.AdvanceState(XmlWellFormedWriter.Token.StartElement);
				if (prefix == null)
				{
					if (ns != null)
					{
						prefix = this.LookupPrefix(ns);
					}
					if (prefix == null)
					{
						prefix = string.Empty;
					}
				}
				else if (prefix.Length > 0)
				{
					this.CheckNCName(prefix);
					if (ns == null)
					{
						ns = this.LookupNamespace(prefix);
					}
					if (ns == null || (ns != null && ns.Length == 0))
					{
						throw new ArgumentException(Res.GetString("Cannot use a prefix with an empty namespace."));
					}
				}
				if (ns == null)
				{
					ns = this.LookupNamespace(prefix);
					if (ns == null)
					{
						ns = string.Empty;
					}
				}
				if (this.elemTop == 0 && this.rawWriter != null)
				{
					this.rawWriter.OnRootElement(this.conformanceLevel);
				}
				this.writer.WriteStartElement(prefix, localName, ns);
				int num = this.elemTop + 1;
				this.elemTop = num;
				int num2 = num;
				if (num2 == this.elemScopeStack.Length)
				{
					XmlWellFormedWriter.ElementScope[] destinationArray = new XmlWellFormedWriter.ElementScope[num2 * 2];
					Array.Copy(this.elemScopeStack, destinationArray, num2);
					this.elemScopeStack = destinationArray;
				}
				this.elemScopeStack[num2].Set(prefix, localName, ns, this.nsTop);
				this.PushNamespaceImplicit(prefix, ns);
				if (this.attrCount >= 14)
				{
					this.attrHashTable.Clear();
				}
				this.attrCount = 0;
			}
			catch
			{
				this.currentState = XmlWellFormedWriter.State.Error;
				throw;
			}
		}

		// Token: 0x06000C10 RID: 3088 RVA: 0x0004F598 File Offset: 0x0004D798
		public override void WriteEndElement()
		{
			try
			{
				this.AdvanceState(XmlWellFormedWriter.Token.EndElement);
				int num = this.elemTop;
				if (num == 0)
				{
					throw new XmlException("There was no XML start tag open.", string.Empty);
				}
				if (this.rawWriter != null)
				{
					this.elemScopeStack[num].WriteEndElement(this.rawWriter);
				}
				else
				{
					this.writer.WriteEndElement();
				}
				int prevNSTop = this.elemScopeStack[num].prevNSTop;
				if (this.useNsHashtable && prevNSTop < this.nsTop)
				{
					this.PopNamespaces(prevNSTop + 1, this.nsTop);
				}
				this.nsTop = prevNSTop;
				if ((this.elemTop = num - 1) == 0)
				{
					if (this.conformanceLevel == ConformanceLevel.Document)
					{
						this.currentState = XmlWellFormedWriter.State.AfterRootEle;
					}
					else
					{
						this.currentState = XmlWellFormedWriter.State.TopLevel;
					}
				}
			}
			catch
			{
				this.currentState = XmlWellFormedWriter.State.Error;
				throw;
			}
		}

		// Token: 0x06000C11 RID: 3089 RVA: 0x0004F670 File Offset: 0x0004D870
		public override void WriteFullEndElement()
		{
			try
			{
				this.AdvanceState(XmlWellFormedWriter.Token.EndElement);
				int num = this.elemTop;
				if (num == 0)
				{
					throw new XmlException("There was no XML start tag open.", string.Empty);
				}
				if (this.rawWriter != null)
				{
					this.elemScopeStack[num].WriteFullEndElement(this.rawWriter);
				}
				else
				{
					this.writer.WriteFullEndElement();
				}
				int prevNSTop = this.elemScopeStack[num].prevNSTop;
				if (this.useNsHashtable && prevNSTop < this.nsTop)
				{
					this.PopNamespaces(prevNSTop + 1, this.nsTop);
				}
				this.nsTop = prevNSTop;
				if ((this.elemTop = num - 1) == 0)
				{
					if (this.conformanceLevel == ConformanceLevel.Document)
					{
						this.currentState = XmlWellFormedWriter.State.AfterRootEle;
					}
					else
					{
						this.currentState = XmlWellFormedWriter.State.TopLevel;
					}
				}
			}
			catch
			{
				this.currentState = XmlWellFormedWriter.State.Error;
				throw;
			}
		}

		// Token: 0x06000C12 RID: 3090 RVA: 0x0004F748 File Offset: 0x0004D948
		public override void WriteStartAttribute(string prefix, string localName, string namespaceName)
		{
			try
			{
				if (localName == null || localName.Length == 0)
				{
					if (!(prefix == "xmlns"))
					{
						throw new ArgumentException(Res.GetString("The empty string '' is not a valid local name."));
					}
					localName = "xmlns";
					prefix = string.Empty;
				}
				this.CheckNCName(localName);
				this.AdvanceState(XmlWellFormedWriter.Token.StartAttribute);
				if (prefix == null)
				{
					if (namespaceName != null && (!(localName == "xmlns") || !(namespaceName == "http://www.w3.org/2000/xmlns/")))
					{
						prefix = this.LookupPrefix(namespaceName);
					}
					if (prefix == null)
					{
						prefix = string.Empty;
					}
				}
				if (namespaceName == null)
				{
					if (prefix != null && prefix.Length > 0)
					{
						namespaceName = this.LookupNamespace(prefix);
					}
					if (namespaceName == null)
					{
						namespaceName = string.Empty;
					}
				}
				if (prefix.Length == 0)
				{
					if (localName[0] == 'x' && localName == "xmlns")
					{
						if (namespaceName.Length > 0 && namespaceName != "http://www.w3.org/2000/xmlns/")
						{
							throw new ArgumentException(Res.GetString("Prefix \"xmlns\" is reserved for use by XML."));
						}
						this.curDeclPrefix = string.Empty;
						this.SetSpecialAttribute(XmlWellFormedWriter.SpecialAttribute.DefaultXmlns);
						goto IL_224;
					}
					else if (namespaceName.Length > 0)
					{
						prefix = this.LookupPrefix(namespaceName);
						if (prefix == null || prefix.Length == 0)
						{
							prefix = this.GeneratePrefix();
						}
					}
				}
				else
				{
					if (prefix[0] == 'x')
					{
						if (prefix == "xmlns")
						{
							if (namespaceName.Length > 0 && namespaceName != "http://www.w3.org/2000/xmlns/")
							{
								throw new ArgumentException(Res.GetString("Prefix \"xmlns\" is reserved for use by XML."));
							}
							this.curDeclPrefix = localName;
							this.SetSpecialAttribute(XmlWellFormedWriter.SpecialAttribute.PrefixedXmlns);
							goto IL_224;
						}
						else if (prefix == "xml")
						{
							if (namespaceName.Length > 0 && namespaceName != "http://www.w3.org/XML/1998/namespace")
							{
								throw new ArgumentException(Res.GetString("Prefix \"xml\" is reserved for use by XML and can be mapped only to namespace name \"http://www.w3.org/XML/1998/namespace\"."));
							}
							if (localName == "space")
							{
								this.SetSpecialAttribute(XmlWellFormedWriter.SpecialAttribute.XmlSpace);
								goto IL_224;
							}
							if (localName == "lang")
							{
								this.SetSpecialAttribute(XmlWellFormedWriter.SpecialAttribute.XmlLang);
								goto IL_224;
							}
						}
					}
					this.CheckNCName(prefix);
					if (namespaceName.Length == 0)
					{
						prefix = string.Empty;
					}
					else
					{
						string text = this.LookupLocalNamespace(prefix);
						if (text != null && text != namespaceName)
						{
							prefix = this.GeneratePrefix();
						}
					}
				}
				if (prefix.Length != 0)
				{
					this.PushNamespaceImplicit(prefix, namespaceName);
				}
				IL_224:
				this.AddAttribute(prefix, localName, namespaceName);
				if (this.specAttr == XmlWellFormedWriter.SpecialAttribute.No)
				{
					this.writer.WriteStartAttribute(prefix, localName, namespaceName);
				}
			}
			catch
			{
				this.currentState = XmlWellFormedWriter.State.Error;
				throw;
			}
		}

		// Token: 0x06000C13 RID: 3091 RVA: 0x0004F9C4 File Offset: 0x0004DBC4
		public override void WriteEndAttribute()
		{
			try
			{
				this.AdvanceState(XmlWellFormedWriter.Token.EndAttribute);
				if (this.specAttr != XmlWellFormedWriter.SpecialAttribute.No)
				{
					switch (this.specAttr)
					{
					case XmlWellFormedWriter.SpecialAttribute.DefaultXmlns:
					{
						string stringValue = this.attrValueCache.StringValue;
						if (this.PushNamespaceExplicit(string.Empty, stringValue))
						{
							if (this.rawWriter != null)
							{
								if (this.rawWriter.SupportsNamespaceDeclarationInChunks)
								{
									this.rawWriter.WriteStartNamespaceDeclaration(string.Empty);
									this.attrValueCache.Replay(this.rawWriter);
									this.rawWriter.WriteEndNamespaceDeclaration();
								}
								else
								{
									this.rawWriter.WriteNamespaceDeclaration(string.Empty, stringValue);
								}
							}
							else
							{
								this.writer.WriteStartAttribute(string.Empty, "xmlns", "http://www.w3.org/2000/xmlns/");
								this.attrValueCache.Replay(this.writer);
								this.writer.WriteEndAttribute();
							}
						}
						this.curDeclPrefix = null;
						break;
					}
					case XmlWellFormedWriter.SpecialAttribute.PrefixedXmlns:
					{
						string stringValue = this.attrValueCache.StringValue;
						if (stringValue.Length == 0)
						{
							throw new ArgumentException(Res.GetString("Cannot use a prefix with an empty namespace."));
						}
						if (stringValue == "http://www.w3.org/2000/xmlns/" || (stringValue == "http://www.w3.org/XML/1998/namespace" && this.curDeclPrefix != "xml"))
						{
							throw new ArgumentException(Res.GetString("Cannot bind to the reserved namespace."));
						}
						if (this.PushNamespaceExplicit(this.curDeclPrefix, stringValue))
						{
							if (this.rawWriter != null)
							{
								if (this.rawWriter.SupportsNamespaceDeclarationInChunks)
								{
									this.rawWriter.WriteStartNamespaceDeclaration(this.curDeclPrefix);
									this.attrValueCache.Replay(this.rawWriter);
									this.rawWriter.WriteEndNamespaceDeclaration();
								}
								else
								{
									this.rawWriter.WriteNamespaceDeclaration(this.curDeclPrefix, stringValue);
								}
							}
							else
							{
								this.writer.WriteStartAttribute("xmlns", this.curDeclPrefix, "http://www.w3.org/2000/xmlns/");
								this.attrValueCache.Replay(this.writer);
								this.writer.WriteEndAttribute();
							}
						}
						this.curDeclPrefix = null;
						break;
					}
					case XmlWellFormedWriter.SpecialAttribute.XmlSpace:
					{
						this.attrValueCache.Trim();
						string stringValue = this.attrValueCache.StringValue;
						if (stringValue == "default")
						{
							this.elemScopeStack[this.elemTop].xmlSpace = XmlSpace.Default;
						}
						else
						{
							if (!(stringValue == "preserve"))
							{
								throw new ArgumentException(Res.GetString("'{0}' is an invalid xml:space value.", new object[]
								{
									stringValue
								}));
							}
							this.elemScopeStack[this.elemTop].xmlSpace = XmlSpace.Preserve;
						}
						this.writer.WriteStartAttribute("xml", "space", "http://www.w3.org/XML/1998/namespace");
						this.attrValueCache.Replay(this.writer);
						this.writer.WriteEndAttribute();
						break;
					}
					case XmlWellFormedWriter.SpecialAttribute.XmlLang:
					{
						string stringValue = this.attrValueCache.StringValue;
						this.elemScopeStack[this.elemTop].xmlLang = stringValue;
						this.writer.WriteStartAttribute("xml", "lang", "http://www.w3.org/XML/1998/namespace");
						this.attrValueCache.Replay(this.writer);
						this.writer.WriteEndAttribute();
						break;
					}
					}
					this.specAttr = XmlWellFormedWriter.SpecialAttribute.No;
					this.attrValueCache.Clear();
				}
				else
				{
					this.writer.WriteEndAttribute();
				}
			}
			catch
			{
				this.currentState = XmlWellFormedWriter.State.Error;
				throw;
			}
		}

		// Token: 0x06000C14 RID: 3092 RVA: 0x0004FD20 File Offset: 0x0004DF20
		public override void WriteCData(string text)
		{
			try
			{
				if (text == null)
				{
					text = string.Empty;
				}
				this.AdvanceState(XmlWellFormedWriter.Token.CData);
				this.writer.WriteCData(text);
			}
			catch
			{
				this.currentState = XmlWellFormedWriter.State.Error;
				throw;
			}
		}

		// Token: 0x06000C15 RID: 3093 RVA: 0x0004FD68 File Offset: 0x0004DF68
		public override void WriteComment(string text)
		{
			try
			{
				if (text == null)
				{
					text = string.Empty;
				}
				this.AdvanceState(XmlWellFormedWriter.Token.Comment);
				this.writer.WriteComment(text);
			}
			catch
			{
				this.currentState = XmlWellFormedWriter.State.Error;
				throw;
			}
		}

		// Token: 0x06000C16 RID: 3094 RVA: 0x0004FDB0 File Offset: 0x0004DFB0
		public override void WriteProcessingInstruction(string name, string text)
		{
			try
			{
				if (name == null || name.Length == 0)
				{
					throw new ArgumentException(Res.GetString("The empty string '' is not a valid name."));
				}
				this.CheckNCName(name);
				if (text == null)
				{
					text = string.Empty;
				}
				if (name.Length == 3 && string.Compare(name, "xml", StringComparison.OrdinalIgnoreCase) == 0)
				{
					if (this.currentState != XmlWellFormedWriter.State.Start)
					{
						throw new ArgumentException(Res.GetString((this.conformanceLevel == ConformanceLevel.Document) ? "Cannot write XML declaration. WriteStartDocument method has already written it." : "Cannot write XML declaration. XML declaration can be only at the beginning of the document."));
					}
					this.xmlDeclFollows = true;
					this.AdvanceState(XmlWellFormedWriter.Token.PI);
					if (this.rawWriter != null)
					{
						this.rawWriter.WriteXmlDeclaration(text);
					}
					else
					{
						this.writer.WriteProcessingInstruction(name, text);
					}
				}
				else
				{
					this.AdvanceState(XmlWellFormedWriter.Token.PI);
					this.writer.WriteProcessingInstruction(name, text);
				}
			}
			catch
			{
				this.currentState = XmlWellFormedWriter.State.Error;
				throw;
			}
		}

		// Token: 0x06000C17 RID: 3095 RVA: 0x0004FE8C File Offset: 0x0004E08C
		public override void WriteEntityRef(string name)
		{
			try
			{
				if (name == null || name.Length == 0)
				{
					throw new ArgumentException(Res.GetString("The empty string '' is not a valid name."));
				}
				this.CheckNCName(name);
				this.AdvanceState(XmlWellFormedWriter.Token.Text);
				if (this.SaveAttrValue)
				{
					this.attrValueCache.WriteEntityRef(name);
				}
				else
				{
					this.writer.WriteEntityRef(name);
				}
			}
			catch
			{
				this.currentState = XmlWellFormedWriter.State.Error;
				throw;
			}
		}

		// Token: 0x06000C18 RID: 3096 RVA: 0x0004FF04 File Offset: 0x0004E104
		public override void WriteCharEntity(char ch)
		{
			try
			{
				if (char.IsSurrogate(ch))
				{
					throw new ArgumentException(Res.GetString("The surrogate pair is invalid. Missing a low surrogate character."));
				}
				this.AdvanceState(XmlWellFormedWriter.Token.Text);
				if (this.SaveAttrValue)
				{
					this.attrValueCache.WriteCharEntity(ch);
				}
				else
				{
					this.writer.WriteCharEntity(ch);
				}
			}
			catch
			{
				this.currentState = XmlWellFormedWriter.State.Error;
				throw;
			}
		}

		// Token: 0x06000C19 RID: 3097 RVA: 0x0004FF70 File Offset: 0x0004E170
		public override void WriteSurrogateCharEntity(char lowChar, char highChar)
		{
			try
			{
				if (!char.IsSurrogatePair(highChar, lowChar))
				{
					throw XmlConvert.CreateInvalidSurrogatePairException(lowChar, highChar);
				}
				this.AdvanceState(XmlWellFormedWriter.Token.Text);
				if (this.SaveAttrValue)
				{
					this.attrValueCache.WriteSurrogateCharEntity(lowChar, highChar);
				}
				else
				{
					this.writer.WriteSurrogateCharEntity(lowChar, highChar);
				}
			}
			catch
			{
				this.currentState = XmlWellFormedWriter.State.Error;
				throw;
			}
		}

		// Token: 0x06000C1A RID: 3098 RVA: 0x0004FFD8 File Offset: 0x0004E1D8
		public override void WriteWhitespace(string ws)
		{
			try
			{
				if (ws == null)
				{
					ws = string.Empty;
				}
				if (!XmlCharType.Instance.IsOnlyWhitespace(ws))
				{
					throw new ArgumentException(Res.GetString("Only white space characters should be used."));
				}
				this.AdvanceState(XmlWellFormedWriter.Token.Whitespace);
				if (this.SaveAttrValue)
				{
					this.attrValueCache.WriteWhitespace(ws);
				}
				else
				{
					this.writer.WriteWhitespace(ws);
				}
			}
			catch
			{
				this.currentState = XmlWellFormedWriter.State.Error;
				throw;
			}
		}

		// Token: 0x06000C1B RID: 3099 RVA: 0x00050058 File Offset: 0x0004E258
		public override void WriteString(string text)
		{
			try
			{
				if (text != null)
				{
					this.AdvanceState(XmlWellFormedWriter.Token.Text);
					if (this.SaveAttrValue)
					{
						this.attrValueCache.WriteString(text);
					}
					else
					{
						this.writer.WriteString(text);
					}
				}
			}
			catch
			{
				this.currentState = XmlWellFormedWriter.State.Error;
				throw;
			}
		}

		// Token: 0x06000C1C RID: 3100 RVA: 0x000500B4 File Offset: 0x0004E2B4
		public override void WriteChars(char[] buffer, int index, int count)
		{
			try
			{
				if (buffer == null)
				{
					throw new ArgumentNullException("buffer");
				}
				if (index < 0)
				{
					throw new ArgumentOutOfRangeException("index");
				}
				if (count < 0)
				{
					throw new ArgumentOutOfRangeException("count");
				}
				if (count > buffer.Length - index)
				{
					throw new ArgumentOutOfRangeException("count");
				}
				this.AdvanceState(XmlWellFormedWriter.Token.Text);
				if (this.SaveAttrValue)
				{
					this.attrValueCache.WriteChars(buffer, index, count);
				}
				else
				{
					this.writer.WriteChars(buffer, index, count);
				}
			}
			catch
			{
				this.currentState = XmlWellFormedWriter.State.Error;
				throw;
			}
		}

		// Token: 0x06000C1D RID: 3101 RVA: 0x0005014C File Offset: 0x0004E34C
		public override void WriteRaw(char[] buffer, int index, int count)
		{
			try
			{
				if (buffer == null)
				{
					throw new ArgumentNullException("buffer");
				}
				if (index < 0)
				{
					throw new ArgumentOutOfRangeException("index");
				}
				if (count < 0)
				{
					throw new ArgumentOutOfRangeException("count");
				}
				if (count > buffer.Length - index)
				{
					throw new ArgumentOutOfRangeException("count");
				}
				this.AdvanceState(XmlWellFormedWriter.Token.RawData);
				if (this.SaveAttrValue)
				{
					this.attrValueCache.WriteRaw(buffer, index, count);
				}
				else
				{
					this.writer.WriteRaw(buffer, index, count);
				}
			}
			catch
			{
				this.currentState = XmlWellFormedWriter.State.Error;
				throw;
			}
		}

		// Token: 0x06000C1E RID: 3102 RVA: 0x000501E4 File Offset: 0x0004E3E4
		public override void WriteRaw(string data)
		{
			try
			{
				if (data != null)
				{
					this.AdvanceState(XmlWellFormedWriter.Token.RawData);
					if (this.SaveAttrValue)
					{
						this.attrValueCache.WriteRaw(data);
					}
					else
					{
						this.writer.WriteRaw(data);
					}
				}
			}
			catch
			{
				this.currentState = XmlWellFormedWriter.State.Error;
				throw;
			}
		}

		// Token: 0x06000C1F RID: 3103 RVA: 0x00050240 File Offset: 0x0004E440
		public override void WriteBase64(byte[] buffer, int index, int count)
		{
			try
			{
				if (buffer == null)
				{
					throw new ArgumentNullException("buffer");
				}
				if (index < 0)
				{
					throw new ArgumentOutOfRangeException("index");
				}
				if (count < 0)
				{
					throw new ArgumentOutOfRangeException("count");
				}
				if (count > buffer.Length - index)
				{
					throw new ArgumentOutOfRangeException("count");
				}
				this.AdvanceState(XmlWellFormedWriter.Token.Base64);
				this.writer.WriteBase64(buffer, index, count);
			}
			catch
			{
				this.currentState = XmlWellFormedWriter.State.Error;
				throw;
			}
		}

		// Token: 0x06000C20 RID: 3104 RVA: 0x000502C0 File Offset: 0x0004E4C0
		public override void Close()
		{
			if (this.currentState != XmlWellFormedWriter.State.Closed)
			{
				try
				{
					if (this.writeEndDocumentOnClose)
					{
						while (this.currentState != XmlWellFormedWriter.State.Error)
						{
							if (this.elemTop <= 0)
							{
								break;
							}
							this.WriteEndElement();
						}
					}
					else if (this.currentState != XmlWellFormedWriter.State.Error && this.elemTop > 0)
					{
						try
						{
							this.AdvanceState(XmlWellFormedWriter.Token.EndElement);
						}
						catch
						{
							this.currentState = XmlWellFormedWriter.State.Error;
							throw;
						}
					}
					if (this.InBase64 && this.rawWriter != null)
					{
						this.rawWriter.WriteEndBase64();
					}
					this.writer.Flush();
				}
				finally
				{
					try
					{
						if (this.rawWriter != null)
						{
							this.rawWriter.Close(this.WriteState);
						}
						else
						{
							this.writer.Close();
						}
					}
					finally
					{
						this.currentState = XmlWellFormedWriter.State.Closed;
					}
				}
			}
		}

		// Token: 0x06000C21 RID: 3105 RVA: 0x000503AC File Offset: 0x0004E5AC
		public override void Flush()
		{
			try
			{
				this.writer.Flush();
			}
			catch
			{
				this.currentState = XmlWellFormedWriter.State.Error;
				throw;
			}
		}

		// Token: 0x06000C22 RID: 3106 RVA: 0x000503E4 File Offset: 0x0004E5E4
		public override string LookupPrefix(string ns)
		{
			string result;
			try
			{
				if (ns == null)
				{
					throw new ArgumentNullException("ns");
				}
				for (int i = this.nsTop; i >= 0; i--)
				{
					if (this.nsStack[i].namespaceUri == ns)
					{
						string prefix = this.nsStack[i].prefix;
						for (i++; i <= this.nsTop; i++)
						{
							if (this.nsStack[i].prefix == prefix)
							{
								return null;
							}
						}
						return prefix;
					}
				}
				result = ((this.predefinedNamespaces != null) ? this.predefinedNamespaces.LookupPrefix(ns) : null);
			}
			catch
			{
				this.currentState = XmlWellFormedWriter.State.Error;
				throw;
			}
			return result;
		}

		// Token: 0x170001D1 RID: 465
		// (get) Token: 0x06000C23 RID: 3107 RVA: 0x000504A8 File Offset: 0x0004E6A8
		public override XmlSpace XmlSpace
		{
			get
			{
				int num = this.elemTop;
				while (num >= 0 && this.elemScopeStack[num].xmlSpace == (XmlSpace)(-1))
				{
					num--;
				}
				return this.elemScopeStack[num].xmlSpace;
			}
		}

		// Token: 0x170001D2 RID: 466
		// (get) Token: 0x06000C24 RID: 3108 RVA: 0x000504EC File Offset: 0x0004E6EC
		public override string XmlLang
		{
			get
			{
				int num = this.elemTop;
				while (num > 0 && this.elemScopeStack[num].xmlLang == null)
				{
					num--;
				}
				return this.elemScopeStack[num].xmlLang;
			}
		}

		// Token: 0x06000C25 RID: 3109 RVA: 0x00050530 File Offset: 0x0004E730
		public override void WriteQualifiedName(string localName, string ns)
		{
			try
			{
				if (localName == null || localName.Length == 0)
				{
					throw new ArgumentException(Res.GetString("The empty string '' is not a valid local name."));
				}
				this.CheckNCName(localName);
				this.AdvanceState(XmlWellFormedWriter.Token.Text);
				string text = string.Empty;
				if (ns != null && ns.Length != 0)
				{
					text = this.LookupPrefix(ns);
					if (text == null)
					{
						if (this.currentState != XmlWellFormedWriter.State.Attribute)
						{
							throw new ArgumentException(Res.GetString("The '{0}' namespace is not defined.", new object[]
							{
								ns
							}));
						}
						text = this.GeneratePrefix();
						this.PushNamespaceImplicit(text, ns);
					}
				}
				if (this.SaveAttrValue || this.rawWriter == null)
				{
					if (text.Length != 0)
					{
						this.WriteString(text);
						this.WriteString(":");
					}
					this.WriteString(localName);
				}
				else
				{
					this.rawWriter.WriteQualifiedName(text, localName, ns);
				}
			}
			catch
			{
				this.currentState = XmlWellFormedWriter.State.Error;
				throw;
			}
		}

		// Token: 0x06000C26 RID: 3110 RVA: 0x00050614 File Offset: 0x0004E814
		public override void WriteValue(bool value)
		{
			try
			{
				this.AdvanceState(XmlWellFormedWriter.Token.AtomicValue);
				this.writer.WriteValue(value);
			}
			catch
			{
				this.currentState = XmlWellFormedWriter.State.Error;
				throw;
			}
		}

		// Token: 0x06000C27 RID: 3111 RVA: 0x00050654 File Offset: 0x0004E854
		public override void WriteValue(DateTime value)
		{
			try
			{
				this.AdvanceState(XmlWellFormedWriter.Token.AtomicValue);
				this.writer.WriteValue(value);
			}
			catch
			{
				this.currentState = XmlWellFormedWriter.State.Error;
				throw;
			}
		}

		// Token: 0x06000C28 RID: 3112 RVA: 0x00050694 File Offset: 0x0004E894
		public override void WriteValue(DateTimeOffset value)
		{
			try
			{
				this.AdvanceState(XmlWellFormedWriter.Token.AtomicValue);
				this.writer.WriteValue(value);
			}
			catch
			{
				this.currentState = XmlWellFormedWriter.State.Error;
				throw;
			}
		}

		// Token: 0x06000C29 RID: 3113 RVA: 0x000506D4 File Offset: 0x0004E8D4
		public override void WriteValue(double value)
		{
			try
			{
				this.AdvanceState(XmlWellFormedWriter.Token.AtomicValue);
				this.writer.WriteValue(value);
			}
			catch
			{
				this.currentState = XmlWellFormedWriter.State.Error;
				throw;
			}
		}

		// Token: 0x06000C2A RID: 3114 RVA: 0x00050714 File Offset: 0x0004E914
		public override void WriteValue(float value)
		{
			try
			{
				this.AdvanceState(XmlWellFormedWriter.Token.AtomicValue);
				this.writer.WriteValue(value);
			}
			catch
			{
				this.currentState = XmlWellFormedWriter.State.Error;
				throw;
			}
		}

		// Token: 0x06000C2B RID: 3115 RVA: 0x00050754 File Offset: 0x0004E954
		public override void WriteValue(decimal value)
		{
			try
			{
				this.AdvanceState(XmlWellFormedWriter.Token.AtomicValue);
				this.writer.WriteValue(value);
			}
			catch
			{
				this.currentState = XmlWellFormedWriter.State.Error;
				throw;
			}
		}

		// Token: 0x06000C2C RID: 3116 RVA: 0x00050794 File Offset: 0x0004E994
		public override void WriteValue(int value)
		{
			try
			{
				this.AdvanceState(XmlWellFormedWriter.Token.AtomicValue);
				this.writer.WriteValue(value);
			}
			catch
			{
				this.currentState = XmlWellFormedWriter.State.Error;
				throw;
			}
		}

		// Token: 0x06000C2D RID: 3117 RVA: 0x000507D4 File Offset: 0x0004E9D4
		public override void WriteValue(long value)
		{
			try
			{
				this.AdvanceState(XmlWellFormedWriter.Token.AtomicValue);
				this.writer.WriteValue(value);
			}
			catch
			{
				this.currentState = XmlWellFormedWriter.State.Error;
				throw;
			}
		}

		// Token: 0x06000C2E RID: 3118 RVA: 0x00050814 File Offset: 0x0004EA14
		public override void WriteValue(string value)
		{
			try
			{
				if (value != null)
				{
					if (this.SaveAttrValue)
					{
						this.AdvanceState(XmlWellFormedWriter.Token.Text);
						this.attrValueCache.WriteValue(value);
					}
					else
					{
						this.AdvanceState(XmlWellFormedWriter.Token.AtomicValue);
						this.writer.WriteValue(value);
					}
				}
			}
			catch
			{
				this.currentState = XmlWellFormedWriter.State.Error;
				throw;
			}
		}

		// Token: 0x06000C2F RID: 3119 RVA: 0x00050878 File Offset: 0x0004EA78
		public override void WriteValue(object value)
		{
			try
			{
				if (this.SaveAttrValue && value is string)
				{
					this.AdvanceState(XmlWellFormedWriter.Token.Text);
					this.attrValueCache.WriteValue((string)value);
				}
				else
				{
					this.AdvanceState(XmlWellFormedWriter.Token.AtomicValue);
					this.writer.WriteValue(value);
				}
			}
			catch
			{
				this.currentState = XmlWellFormedWriter.State.Error;
				throw;
			}
		}

		// Token: 0x06000C30 RID: 3120 RVA: 0x000508E4 File Offset: 0x0004EAE4
		public override void WriteBinHex(byte[] buffer, int index, int count)
		{
			if (this.IsClosedOrErrorState)
			{
				throw new InvalidOperationException(Res.GetString("The Writer is closed or in error state."));
			}
			try
			{
				this.AdvanceState(XmlWellFormedWriter.Token.Text);
				base.WriteBinHex(buffer, index, count);
			}
			catch
			{
				this.currentState = XmlWellFormedWriter.State.Error;
				throw;
			}
		}

		// Token: 0x170001D3 RID: 467
		// (get) Token: 0x06000C31 RID: 3121 RVA: 0x00050938 File Offset: 0x0004EB38
		internal XmlWriter InnerWriter
		{
			get
			{
				return this.writer;
			}
		}

		// Token: 0x170001D4 RID: 468
		// (get) Token: 0x06000C32 RID: 3122 RVA: 0x00050940 File Offset: 0x0004EB40
		internal XmlRawWriter RawWriter
		{
			get
			{
				return this.rawWriter;
			}
		}

		// Token: 0x170001D5 RID: 469
		// (get) Token: 0x06000C33 RID: 3123 RVA: 0x00050948 File Offset: 0x0004EB48
		private bool SaveAttrValue
		{
			get
			{
				return this.specAttr > XmlWellFormedWriter.SpecialAttribute.No;
			}
		}

		// Token: 0x170001D6 RID: 470
		// (get) Token: 0x06000C34 RID: 3124 RVA: 0x00050953 File Offset: 0x0004EB53
		private bool InBase64
		{
			get
			{
				return this.currentState == XmlWellFormedWriter.State.B64Content || this.currentState == XmlWellFormedWriter.State.B64Attribute || this.currentState == XmlWellFormedWriter.State.RootLevelB64Attr;
			}
		}

		// Token: 0x06000C35 RID: 3125 RVA: 0x00050974 File Offset: 0x0004EB74
		private void SetSpecialAttribute(XmlWellFormedWriter.SpecialAttribute special)
		{
			this.specAttr = special;
			if (XmlWellFormedWriter.State.Attribute == this.currentState)
			{
				this.currentState = XmlWellFormedWriter.State.SpecialAttr;
			}
			else if (XmlWellFormedWriter.State.RootLevelAttr == this.currentState)
			{
				this.currentState = XmlWellFormedWriter.State.RootLevelSpecAttr;
			}
			if (this.attrValueCache == null)
			{
				this.attrValueCache = new XmlWellFormedWriter.AttributeValueCache();
			}
		}

		// Token: 0x06000C36 RID: 3126 RVA: 0x000509C0 File Offset: 0x0004EBC0
		private void WriteStartDocumentImpl(XmlStandalone standalone)
		{
			try
			{
				this.AdvanceState(XmlWellFormedWriter.Token.StartDocument);
				if (this.conformanceLevel == ConformanceLevel.Auto)
				{
					this.conformanceLevel = ConformanceLevel.Document;
					this.stateTable = XmlWellFormedWriter.StateTableDocument;
				}
				else if (this.conformanceLevel == ConformanceLevel.Fragment)
				{
					throw new InvalidOperationException(Res.GetString("WriteStartDocument cannot be called on writers created with ConformanceLevel.Fragment."));
				}
				if (this.rawWriter != null)
				{
					if (!this.xmlDeclFollows)
					{
						this.rawWriter.WriteXmlDeclaration(standalone);
					}
				}
				else
				{
					this.writer.WriteStartDocument();
				}
			}
			catch
			{
				this.currentState = XmlWellFormedWriter.State.Error;
				throw;
			}
		}

		// Token: 0x06000C37 RID: 3127 RVA: 0x00050A50 File Offset: 0x0004EC50
		private void StartFragment()
		{
			this.conformanceLevel = ConformanceLevel.Fragment;
		}

		// Token: 0x06000C38 RID: 3128 RVA: 0x00050A5C File Offset: 0x0004EC5C
		private void PushNamespaceImplicit(string prefix, string ns)
		{
			int num = this.LookupNamespaceIndex(prefix);
			XmlWellFormedWriter.NamespaceKind kind;
			if (num != -1)
			{
				if (num > this.elemScopeStack[this.elemTop].prevNSTop)
				{
					if (this.nsStack[num].namespaceUri != ns)
					{
						throw new XmlException("The prefix '{0}' cannot be redefined from '{1}' to '{2}' within the same start element tag.", new string[]
						{
							prefix,
							this.nsStack[num].namespaceUri,
							ns
						});
					}
					return;
				}
				else if (this.nsStack[num].kind == XmlWellFormedWriter.NamespaceKind.Special)
				{
					if (!(prefix == "xml"))
					{
						throw new ArgumentException(Res.GetString("Prefix \"xmlns\" is reserved for use by XML."));
					}
					if (ns != this.nsStack[num].namespaceUri)
					{
						throw new ArgumentException(Res.GetString("Prefix \"xml\" is reserved for use by XML and can be mapped only to namespace name \"http://www.w3.org/XML/1998/namespace\"."));
					}
					kind = XmlWellFormedWriter.NamespaceKind.Implied;
				}
				else
				{
					kind = ((this.nsStack[num].namespaceUri == ns) ? XmlWellFormedWriter.NamespaceKind.Implied : XmlWellFormedWriter.NamespaceKind.NeedToWrite);
				}
			}
			else
			{
				if ((ns == "http://www.w3.org/XML/1998/namespace" && prefix != "xml") || (ns == "http://www.w3.org/2000/xmlns/" && prefix != "xmlns"))
				{
					throw new ArgumentException(Res.GetString("Prefix '{0}' cannot be mapped to namespace name reserved for \"xml\" or \"xmlns\".", new object[]
					{
						prefix
					}));
				}
				if (this.predefinedNamespaces != null)
				{
					kind = ((this.predefinedNamespaces.LookupNamespace(prefix) == ns) ? XmlWellFormedWriter.NamespaceKind.Implied : XmlWellFormedWriter.NamespaceKind.NeedToWrite);
				}
				else
				{
					kind = XmlWellFormedWriter.NamespaceKind.NeedToWrite;
				}
			}
			this.AddNamespace(prefix, ns, kind);
		}

		// Token: 0x06000C39 RID: 3129 RVA: 0x00050BD8 File Offset: 0x0004EDD8
		private bool PushNamespaceExplicit(string prefix, string ns)
		{
			bool result = true;
			int num = this.LookupNamespaceIndex(prefix);
			if (num != -1)
			{
				if (num > this.elemScopeStack[this.elemTop].prevNSTop)
				{
					if (this.nsStack[num].namespaceUri != ns)
					{
						throw new XmlException("The prefix '{0}' cannot be redefined from '{1}' to '{2}' within the same start element tag.", new string[]
						{
							prefix,
							this.nsStack[num].namespaceUri,
							ns
						});
					}
					XmlWellFormedWriter.NamespaceKind kind = this.nsStack[num].kind;
					if (kind == XmlWellFormedWriter.NamespaceKind.Written)
					{
						throw XmlWellFormedWriter.DupAttrException((prefix.Length == 0) ? string.Empty : "xmlns", (prefix.Length == 0) ? "xmlns" : prefix);
					}
					if (this.omitDuplNamespaces && kind != XmlWellFormedWriter.NamespaceKind.NeedToWrite)
					{
						result = false;
					}
					this.nsStack[num].kind = XmlWellFormedWriter.NamespaceKind.Written;
					return result;
				}
				else if (this.nsStack[num].namespaceUri == ns && this.omitDuplNamespaces)
				{
					result = false;
				}
			}
			else if (this.predefinedNamespaces != null && this.predefinedNamespaces.LookupNamespace(prefix) == ns && this.omitDuplNamespaces)
			{
				result = false;
			}
			if ((ns == "http://www.w3.org/XML/1998/namespace" && prefix != "xml") || (ns == "http://www.w3.org/2000/xmlns/" && prefix != "xmlns"))
			{
				throw new ArgumentException(Res.GetString("Prefix '{0}' cannot be mapped to namespace name reserved for \"xml\" or \"xmlns\".", new object[]
				{
					prefix
				}));
			}
			if (prefix.Length > 0 && prefix[0] == 'x')
			{
				if (prefix == "xml")
				{
					if (ns != "http://www.w3.org/XML/1998/namespace")
					{
						throw new ArgumentException(Res.GetString("Prefix \"xml\" is reserved for use by XML and can be mapped only to namespace name \"http://www.w3.org/XML/1998/namespace\"."));
					}
				}
				else if (prefix == "xmlns")
				{
					throw new ArgumentException(Res.GetString("Prefix \"xmlns\" is reserved for use by XML."));
				}
			}
			this.AddNamespace(prefix, ns, XmlWellFormedWriter.NamespaceKind.Written);
			return result;
		}

		// Token: 0x06000C3A RID: 3130 RVA: 0x00050DB8 File Offset: 0x0004EFB8
		private void AddNamespace(string prefix, string ns, XmlWellFormedWriter.NamespaceKind kind)
		{
			int num = this.nsTop + 1;
			this.nsTop = num;
			int num2 = num;
			if (num2 == this.nsStack.Length)
			{
				XmlWellFormedWriter.Namespace[] destinationArray = new XmlWellFormedWriter.Namespace[num2 * 2];
				Array.Copy(this.nsStack, destinationArray, num2);
				this.nsStack = destinationArray;
			}
			this.nsStack[num2].Set(prefix, ns, kind);
			if (this.useNsHashtable)
			{
				this.AddToNamespaceHashtable(this.nsTop);
				return;
			}
			if (this.nsTop == 16)
			{
				this.nsHashtable = new Dictionary<string, int>(this.hasher);
				for (int i = 0; i <= this.nsTop; i++)
				{
					this.AddToNamespaceHashtable(i);
				}
				this.useNsHashtable = true;
			}
		}

		// Token: 0x06000C3B RID: 3131 RVA: 0x00050E64 File Offset: 0x0004F064
		private void AddToNamespaceHashtable(int namespaceIndex)
		{
			string prefix = this.nsStack[namespaceIndex].prefix;
			int prevNsIndex;
			if (this.nsHashtable.TryGetValue(prefix, out prevNsIndex))
			{
				this.nsStack[namespaceIndex].prevNsIndex = prevNsIndex;
			}
			this.nsHashtable[prefix] = namespaceIndex;
		}

		// Token: 0x06000C3C RID: 3132 RVA: 0x00050EB4 File Offset: 0x0004F0B4
		private int LookupNamespaceIndex(string prefix)
		{
			if (this.useNsHashtable)
			{
				int result;
				if (this.nsHashtable.TryGetValue(prefix, out result))
				{
					return result;
				}
			}
			else
			{
				for (int i = this.nsTop; i >= 0; i--)
				{
					if (this.nsStack[i].prefix == prefix)
					{
						return i;
					}
				}
			}
			return -1;
		}

		// Token: 0x06000C3D RID: 3133 RVA: 0x00050F08 File Offset: 0x0004F108
		private void PopNamespaces(int indexFrom, int indexTo)
		{
			for (int i = indexTo; i >= indexFrom; i--)
			{
				if (this.nsStack[i].prevNsIndex == -1)
				{
					this.nsHashtable.Remove(this.nsStack[i].prefix);
				}
				else
				{
					this.nsHashtable[this.nsStack[i].prefix] = this.nsStack[i].prevNsIndex;
				}
			}
		}

		// Token: 0x06000C3E RID: 3134 RVA: 0x00050F84 File Offset: 0x0004F184
		private static XmlException DupAttrException(string prefix, string localName)
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (prefix.Length > 0)
			{
				stringBuilder.Append(prefix);
				stringBuilder.Append(':');
			}
			stringBuilder.Append(localName);
			return new XmlException("'{0}' is a duplicate attribute name.", stringBuilder.ToString());
		}

		// Token: 0x06000C3F RID: 3135 RVA: 0x00050FCC File Offset: 0x0004F1CC
		private void AdvanceState(XmlWellFormedWriter.Token token)
		{
			if (this.currentState < XmlWellFormedWriter.State.Closed)
			{
				XmlWellFormedWriter.State state;
				for (;;)
				{
					state = this.stateTable[(int)(((int)token << 4) + (int)this.currentState)];
					if (state < XmlWellFormedWriter.State.Error)
					{
						break;
					}
					if (state != XmlWellFormedWriter.State.Error)
					{
						switch (state)
						{
						case XmlWellFormedWriter.State.StartContent:
							goto IL_E3;
						case XmlWellFormedWriter.State.StartContentEle:
							goto IL_F0;
						case XmlWellFormedWriter.State.StartContentB64:
							goto IL_FD;
						case XmlWellFormedWriter.State.StartDoc:
							goto IL_10A;
						case XmlWellFormedWriter.State.StartDocEle:
							goto IL_117;
						case XmlWellFormedWriter.State.EndAttrSEle:
							goto IL_124;
						case XmlWellFormedWriter.State.EndAttrEEle:
							goto IL_137;
						case XmlWellFormedWriter.State.EndAttrSCont:
							goto IL_14A;
						case XmlWellFormedWriter.State.EndAttrSAttr:
							goto IL_15D;
						case XmlWellFormedWriter.State.PostB64Cont:
							if (this.rawWriter != null)
							{
								this.rawWriter.WriteEndBase64();
							}
							this.currentState = XmlWellFormedWriter.State.Content;
							continue;
						case XmlWellFormedWriter.State.PostB64Attr:
							if (this.rawWriter != null)
							{
								this.rawWriter.WriteEndBase64();
							}
							this.currentState = XmlWellFormedWriter.State.Attribute;
							continue;
						case XmlWellFormedWriter.State.PostB64RootAttr:
							if (this.rawWriter != null)
							{
								this.rawWriter.WriteEndBase64();
							}
							this.currentState = XmlWellFormedWriter.State.RootLevelAttr;
							continue;
						case XmlWellFormedWriter.State.StartFragEle:
							goto IL_1C8;
						case XmlWellFormedWriter.State.StartFragCont:
							goto IL_1D2;
						case XmlWellFormedWriter.State.StartFragB64:
							goto IL_1DC;
						case XmlWellFormedWriter.State.StartRootLevelAttr:
							goto IL_1E6;
						}
						break;
					}
					goto IL_D1;
				}
				goto IL_1EF;
				IL_D1:
				this.ThrowInvalidStateTransition(token, this.currentState);
				goto IL_1EF;
				IL_E3:
				this.StartElementContent();
				state = XmlWellFormedWriter.State.Content;
				goto IL_1EF;
				IL_F0:
				this.StartElementContent();
				state = XmlWellFormedWriter.State.Element;
				goto IL_1EF;
				IL_FD:
				this.StartElementContent();
				state = XmlWellFormedWriter.State.B64Content;
				goto IL_1EF;
				IL_10A:
				this.WriteStartDocument();
				state = XmlWellFormedWriter.State.Document;
				goto IL_1EF;
				IL_117:
				this.WriteStartDocument();
				state = XmlWellFormedWriter.State.Element;
				goto IL_1EF;
				IL_124:
				this.WriteEndAttribute();
				this.StartElementContent();
				state = XmlWellFormedWriter.State.Element;
				goto IL_1EF;
				IL_137:
				this.WriteEndAttribute();
				this.StartElementContent();
				state = XmlWellFormedWriter.State.Content;
				goto IL_1EF;
				IL_14A:
				this.WriteEndAttribute();
				this.StartElementContent();
				state = XmlWellFormedWriter.State.Content;
				goto IL_1EF;
				IL_15D:
				this.WriteEndAttribute();
				state = XmlWellFormedWriter.State.Attribute;
				goto IL_1EF;
				IL_1C8:
				this.StartFragment();
				state = XmlWellFormedWriter.State.Element;
				goto IL_1EF;
				IL_1D2:
				this.StartFragment();
				state = XmlWellFormedWriter.State.Content;
				goto IL_1EF;
				IL_1DC:
				this.StartFragment();
				state = XmlWellFormedWriter.State.B64Content;
				goto IL_1EF;
				IL_1E6:
				this.WriteEndAttribute();
				state = XmlWellFormedWriter.State.RootLevelAttr;
				IL_1EF:
				this.currentState = state;
				return;
			}
			if (this.currentState == XmlWellFormedWriter.State.Closed || this.currentState == XmlWellFormedWriter.State.Error)
			{
				throw new InvalidOperationException(Res.GetString("The Writer is closed or in error state."));
			}
			throw new InvalidOperationException(Res.GetString("Token {0} in state {1} would result in an invalid XML document.", new object[]
			{
				XmlWellFormedWriter.tokenName[(int)token],
				XmlWellFormedWriter.GetStateName(this.currentState)
			}));
		}

		// Token: 0x06000C40 RID: 3136 RVA: 0x000511D0 File Offset: 0x0004F3D0
		private void StartElementContent()
		{
			int prevNSTop = this.elemScopeStack[this.elemTop].prevNSTop;
			for (int i = this.nsTop; i > prevNSTop; i--)
			{
				if (this.nsStack[i].kind == XmlWellFormedWriter.NamespaceKind.NeedToWrite)
				{
					this.nsStack[i].WriteDecl(this.writer, this.rawWriter);
				}
			}
			if (this.rawWriter != null)
			{
				this.rawWriter.StartElementContent();
			}
		}

		// Token: 0x06000C41 RID: 3137 RVA: 0x00051249 File Offset: 0x0004F449
		private static string GetStateName(XmlWellFormedWriter.State state)
		{
			if (state >= XmlWellFormedWriter.State.Error)
			{
				return "Error";
			}
			return XmlWellFormedWriter.stateName[(int)state];
		}

		// Token: 0x06000C42 RID: 3138 RVA: 0x00051260 File Offset: 0x0004F460
		internal string LookupNamespace(string prefix)
		{
			for (int i = this.nsTop; i >= 0; i--)
			{
				if (this.nsStack[i].prefix == prefix)
				{
					return this.nsStack[i].namespaceUri;
				}
			}
			if (this.predefinedNamespaces == null)
			{
				return null;
			}
			return this.predefinedNamespaces.LookupNamespace(prefix);
		}

		// Token: 0x06000C43 RID: 3139 RVA: 0x000512C0 File Offset: 0x0004F4C0
		private string LookupLocalNamespace(string prefix)
		{
			for (int i = this.nsTop; i > this.elemScopeStack[this.elemTop].prevNSTop; i--)
			{
				if (this.nsStack[i].prefix == prefix)
				{
					return this.nsStack[i].namespaceUri;
				}
			}
			return null;
		}

		// Token: 0x06000C44 RID: 3140 RVA: 0x00051320 File Offset: 0x0004F520
		private string GeneratePrefix()
		{
			string text = "p" + (this.nsTop - 2).ToString("d", CultureInfo.InvariantCulture);
			if (this.LookupNamespace(text) == null)
			{
				return text;
			}
			int num = 0;
			string text2;
			do
			{
				text2 = text + num.ToString(CultureInfo.InvariantCulture);
				num++;
			}
			while (this.LookupNamespace(text2) != null);
			return text2;
		}

		// Token: 0x06000C45 RID: 3141 RVA: 0x00051380 File Offset: 0x0004F580
		private void CheckNCName(string ncname)
		{
			int length = ncname.Length;
			if ((this.xmlCharType.charProperties[(int)ncname[0]] & 4) != 0)
			{
				for (int i = 1; i < length; i++)
				{
					if ((this.xmlCharType.charProperties[(int)ncname[i]] & 8) == 0)
					{
						throw XmlWellFormedWriter.InvalidCharsException(ncname, i);
					}
				}
				return;
			}
			throw XmlWellFormedWriter.InvalidCharsException(ncname, 0);
		}

		// Token: 0x06000C46 RID: 3142 RVA: 0x000513E0 File Offset: 0x0004F5E0
		private static Exception InvalidCharsException(string name, int badCharIndex)
		{
			string[] array = XmlException.BuildCharExceptionArgs(name, badCharIndex);
			string[] array2 = new string[]
			{
				name,
				array[0],
				array[1]
			};
			string name2 = "Invalid name character in '{0}'. The '{1}' character, hexadecimal value {2}, cannot be included in a name.";
			object[] args = array2;
			return new ArgumentException(Res.GetString(name2, args));
		}

		// Token: 0x06000C47 RID: 3143 RVA: 0x00051420 File Offset: 0x0004F620
		private void ThrowInvalidStateTransition(XmlWellFormedWriter.Token token, XmlWellFormedWriter.State currentState)
		{
			string @string = Res.GetString("Token {0} in state {1} would result in an invalid XML document.", new object[]
			{
				XmlWellFormedWriter.tokenName[(int)token],
				XmlWellFormedWriter.GetStateName(currentState)
			});
			if ((currentState == XmlWellFormedWriter.State.Start || currentState == XmlWellFormedWriter.State.AfterRootEle) && this.conformanceLevel == ConformanceLevel.Document)
			{
				throw new InvalidOperationException(@string + " " + Res.GetString("Make sure that the ConformanceLevel setting is set to ConformanceLevel.Fragment or ConformanceLevel.Auto if you want to write an XML fragment."));
			}
			throw new InvalidOperationException(@string);
		}

		// Token: 0x170001D7 RID: 471
		// (get) Token: 0x06000C48 RID: 3144 RVA: 0x00051482 File Offset: 0x0004F682
		private bool IsClosedOrErrorState
		{
			get
			{
				return this.currentState >= XmlWellFormedWriter.State.Closed;
			}
		}

		// Token: 0x06000C49 RID: 3145 RVA: 0x00051494 File Offset: 0x0004F694
		private void AddAttribute(string prefix, string localName, string namespaceName)
		{
			int num = this.attrCount;
			this.attrCount = num + 1;
			int num2 = num;
			if (num2 == this.attrStack.Length)
			{
				XmlWellFormedWriter.AttrName[] destinationArray = new XmlWellFormedWriter.AttrName[num2 * 2];
				Array.Copy(this.attrStack, destinationArray, num2);
				this.attrStack = destinationArray;
			}
			this.attrStack[num2].Set(prefix, localName, namespaceName);
			if (this.attrCount < 14)
			{
				for (int i = 0; i < num2; i++)
				{
					if (this.attrStack[i].IsDuplicate(prefix, localName, namespaceName))
					{
						throw XmlWellFormedWriter.DupAttrException(prefix, localName);
					}
				}
				return;
			}
			if (this.attrCount == 14)
			{
				if (this.attrHashTable == null)
				{
					this.attrHashTable = new Dictionary<string, int>(this.hasher);
				}
				for (int j = 0; j < num2; j++)
				{
					this.AddToAttrHashTable(j);
				}
			}
			this.AddToAttrHashTable(num2);
			for (int k = this.attrStack[num2].prev; k > 0; k = this.attrStack[k].prev)
			{
				k--;
				if (this.attrStack[k].IsDuplicate(prefix, localName, namespaceName))
				{
					throw XmlWellFormedWriter.DupAttrException(prefix, localName);
				}
			}
		}

		// Token: 0x06000C4A RID: 3146 RVA: 0x000515BC File Offset: 0x0004F7BC
		private void AddToAttrHashTable(int attributeIndex)
		{
			string localName = this.attrStack[attributeIndex].localName;
			int count = this.attrHashTable.Count;
			this.attrHashTable[localName] = 0;
			if (count != this.attrHashTable.Count)
			{
				return;
			}
			int num = attributeIndex - 1;
			while (num >= 0 && !(this.attrStack[num].localName == localName))
			{
				num--;
			}
			this.attrStack[attributeIndex].prev = num + 1;
		}

		// Token: 0x06000C4B RID: 3147 RVA: 0x0005163C File Offset: 0x0004F83C
		public override Task WriteStartDocumentAsync()
		{
			return this.WriteStartDocumentImplAsync(XmlStandalone.Omit);
		}

		// Token: 0x06000C4C RID: 3148 RVA: 0x00051645 File Offset: 0x0004F845
		public override Task WriteStartDocumentAsync(bool standalone)
		{
			return this.WriteStartDocumentImplAsync(standalone ? XmlStandalone.Yes : XmlStandalone.No);
		}

		// Token: 0x06000C4D RID: 3149 RVA: 0x00051654 File Offset: 0x0004F854
		public override Task WriteEndDocumentAsync()
		{
			XmlWellFormedWriter.<WriteEndDocumentAsync>d__115 <WriteEndDocumentAsync>d__;
			<WriteEndDocumentAsync>d__.<>4__this = this;
			<WriteEndDocumentAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<WriteEndDocumentAsync>d__.<>1__state = -1;
			<WriteEndDocumentAsync>d__.<>t__builder.Start<XmlWellFormedWriter.<WriteEndDocumentAsync>d__115>(ref <WriteEndDocumentAsync>d__);
			return <WriteEndDocumentAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000C4E RID: 3150 RVA: 0x00051698 File Offset: 0x0004F898
		public override Task WriteDocTypeAsync(string name, string pubid, string sysid, string subset)
		{
			XmlWellFormedWriter.<WriteDocTypeAsync>d__116 <WriteDocTypeAsync>d__;
			<WriteDocTypeAsync>d__.<>4__this = this;
			<WriteDocTypeAsync>d__.name = name;
			<WriteDocTypeAsync>d__.pubid = pubid;
			<WriteDocTypeAsync>d__.sysid = sysid;
			<WriteDocTypeAsync>d__.subset = subset;
			<WriteDocTypeAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<WriteDocTypeAsync>d__.<>1__state = -1;
			<WriteDocTypeAsync>d__.<>t__builder.Start<XmlWellFormedWriter.<WriteDocTypeAsync>d__116>(ref <WriteDocTypeAsync>d__);
			return <WriteDocTypeAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000C4F RID: 3151 RVA: 0x000516FC File Offset: 0x0004F8FC
		private Task TryReturnTask(Task task)
		{
			if (task.IsSuccess())
			{
				return AsyncHelper.DoneTask;
			}
			return this._TryReturnTask(task);
		}

		// Token: 0x06000C50 RID: 3152 RVA: 0x00051714 File Offset: 0x0004F914
		private Task _TryReturnTask(Task task)
		{
			XmlWellFormedWriter.<_TryReturnTask>d__118 <_TryReturnTask>d__;
			<_TryReturnTask>d__.<>4__this = this;
			<_TryReturnTask>d__.task = task;
			<_TryReturnTask>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<_TryReturnTask>d__.<>1__state = -1;
			<_TryReturnTask>d__.<>t__builder.Start<XmlWellFormedWriter.<_TryReturnTask>d__118>(ref <_TryReturnTask>d__);
			return <_TryReturnTask>d__.<>t__builder.Task;
		}

		// Token: 0x06000C51 RID: 3153 RVA: 0x0005175F File Offset: 0x0004F95F
		private Task SequenceRun(Task task, Func<Task> nextTaskFun)
		{
			if (task.IsSuccess())
			{
				return this.TryReturnTask(nextTaskFun());
			}
			return this._SequenceRun(task, nextTaskFun);
		}

		// Token: 0x06000C52 RID: 3154 RVA: 0x00051780 File Offset: 0x0004F980
		private Task _SequenceRun(Task task, Func<Task> nextTaskFun)
		{
			XmlWellFormedWriter.<_SequenceRun>d__120 <_SequenceRun>d__;
			<_SequenceRun>d__.<>4__this = this;
			<_SequenceRun>d__.task = task;
			<_SequenceRun>d__.nextTaskFun = nextTaskFun;
			<_SequenceRun>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<_SequenceRun>d__.<>1__state = -1;
			<_SequenceRun>d__.<>t__builder.Start<XmlWellFormedWriter.<_SequenceRun>d__120>(ref <_SequenceRun>d__);
			return <_SequenceRun>d__.<>t__builder.Task;
		}

		// Token: 0x06000C53 RID: 3155 RVA: 0x000517D4 File Offset: 0x0004F9D4
		public override Task WriteStartElementAsync(string prefix, string localName, string ns)
		{
			Task result;
			try
			{
				if (localName == null || localName.Length == 0)
				{
					throw new ArgumentException(Res.GetString("The empty string '' is not a valid local name."));
				}
				this.CheckNCName(localName);
				Task task = this.AdvanceStateAsync(XmlWellFormedWriter.Token.StartElement);
				if (task.IsSuccess())
				{
					result = this.WriteStartElementAsync_NoAdvanceState(prefix, localName, ns);
				}
				else
				{
					result = this.WriteStartElementAsync_NoAdvanceState(task, prefix, localName, ns);
				}
			}
			catch
			{
				this.currentState = XmlWellFormedWriter.State.Error;
				throw;
			}
			return result;
		}

		// Token: 0x06000C54 RID: 3156 RVA: 0x00051848 File Offset: 0x0004FA48
		private Task WriteStartElementAsync_NoAdvanceState(string prefix, string localName, string ns)
		{
			Task result;
			try
			{
				if (prefix == null)
				{
					if (ns != null)
					{
						prefix = this.LookupPrefix(ns);
					}
					if (prefix == null)
					{
						prefix = string.Empty;
					}
				}
				else if (prefix.Length > 0)
				{
					this.CheckNCName(prefix);
					if (ns == null)
					{
						ns = this.LookupNamespace(prefix);
					}
					if (ns == null || (ns != null && ns.Length == 0))
					{
						throw new ArgumentException(Res.GetString("Cannot use a prefix with an empty namespace."));
					}
				}
				if (ns == null)
				{
					ns = this.LookupNamespace(prefix);
					if (ns == null)
					{
						ns = string.Empty;
					}
				}
				if (this.elemTop == 0 && this.rawWriter != null)
				{
					this.rawWriter.OnRootElement(this.conformanceLevel);
				}
				Task task = this.writer.WriteStartElementAsync(prefix, localName, ns);
				if (task.IsSuccess())
				{
					this.WriteStartElementAsync_FinishWrite(prefix, localName, ns);
					result = AsyncHelper.DoneTask;
				}
				else
				{
					result = this.WriteStartElementAsync_FinishWrite(task, prefix, localName, ns);
				}
			}
			catch
			{
				this.currentState = XmlWellFormedWriter.State.Error;
				throw;
			}
			return result;
		}

		// Token: 0x06000C55 RID: 3157 RVA: 0x00051934 File Offset: 0x0004FB34
		private Task WriteStartElementAsync_NoAdvanceState(Task task, string prefix, string localName, string ns)
		{
			XmlWellFormedWriter.<WriteStartElementAsync_NoAdvanceState>d__123 <WriteStartElementAsync_NoAdvanceState>d__;
			<WriteStartElementAsync_NoAdvanceState>d__.<>4__this = this;
			<WriteStartElementAsync_NoAdvanceState>d__.task = task;
			<WriteStartElementAsync_NoAdvanceState>d__.prefix = prefix;
			<WriteStartElementAsync_NoAdvanceState>d__.localName = localName;
			<WriteStartElementAsync_NoAdvanceState>d__.ns = ns;
			<WriteStartElementAsync_NoAdvanceState>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<WriteStartElementAsync_NoAdvanceState>d__.<>1__state = -1;
			<WriteStartElementAsync_NoAdvanceState>d__.<>t__builder.Start<XmlWellFormedWriter.<WriteStartElementAsync_NoAdvanceState>d__123>(ref <WriteStartElementAsync_NoAdvanceState>d__);
			return <WriteStartElementAsync_NoAdvanceState>d__.<>t__builder.Task;
		}

		// Token: 0x06000C56 RID: 3158 RVA: 0x00051998 File Offset: 0x0004FB98
		private void WriteStartElementAsync_FinishWrite(string prefix, string localName, string ns)
		{
			try
			{
				int num = this.elemTop + 1;
				this.elemTop = num;
				int num2 = num;
				if (num2 == this.elemScopeStack.Length)
				{
					XmlWellFormedWriter.ElementScope[] destinationArray = new XmlWellFormedWriter.ElementScope[num2 * 2];
					Array.Copy(this.elemScopeStack, destinationArray, num2);
					this.elemScopeStack = destinationArray;
				}
				this.elemScopeStack[num2].Set(prefix, localName, ns, this.nsTop);
				this.PushNamespaceImplicit(prefix, ns);
				if (this.attrCount >= 14)
				{
					this.attrHashTable.Clear();
				}
				this.attrCount = 0;
			}
			catch
			{
				this.currentState = XmlWellFormedWriter.State.Error;
				throw;
			}
		}

		// Token: 0x06000C57 RID: 3159 RVA: 0x00051A3C File Offset: 0x0004FC3C
		private Task WriteStartElementAsync_FinishWrite(Task t, string prefix, string localName, string ns)
		{
			XmlWellFormedWriter.<WriteStartElementAsync_FinishWrite>d__125 <WriteStartElementAsync_FinishWrite>d__;
			<WriteStartElementAsync_FinishWrite>d__.<>4__this = this;
			<WriteStartElementAsync_FinishWrite>d__.t = t;
			<WriteStartElementAsync_FinishWrite>d__.prefix = prefix;
			<WriteStartElementAsync_FinishWrite>d__.localName = localName;
			<WriteStartElementAsync_FinishWrite>d__.ns = ns;
			<WriteStartElementAsync_FinishWrite>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<WriteStartElementAsync_FinishWrite>d__.<>1__state = -1;
			<WriteStartElementAsync_FinishWrite>d__.<>t__builder.Start<XmlWellFormedWriter.<WriteStartElementAsync_FinishWrite>d__125>(ref <WriteStartElementAsync_FinishWrite>d__);
			return <WriteStartElementAsync_FinishWrite>d__.<>t__builder.Task;
		}

		// Token: 0x06000C58 RID: 3160 RVA: 0x00051AA0 File Offset: 0x0004FCA0
		public override Task WriteEndElementAsync()
		{
			Task result;
			try
			{
				Task task = this.AdvanceStateAsync(XmlWellFormedWriter.Token.EndElement);
				result = this.SequenceRun(task, new Func<Task>(this.WriteEndElementAsync_NoAdvanceState));
			}
			catch
			{
				this.currentState = XmlWellFormedWriter.State.Error;
				throw;
			}
			return result;
		}

		// Token: 0x06000C59 RID: 3161 RVA: 0x00051AE8 File Offset: 0x0004FCE8
		private Task WriteEndElementAsync_NoAdvanceState()
		{
			Task result;
			try
			{
				int num = this.elemTop;
				if (num == 0)
				{
					throw new XmlException("There was no XML start tag open.", string.Empty);
				}
				Task task;
				if (this.rawWriter != null)
				{
					task = this.elemScopeStack[num].WriteEndElementAsync(this.rawWriter);
				}
				else
				{
					task = this.writer.WriteEndElementAsync();
				}
				result = this.SequenceRun(task, new Func<Task>(this.WriteEndElementAsync_FinishWrite));
			}
			catch
			{
				this.currentState = XmlWellFormedWriter.State.Error;
				throw;
			}
			return result;
		}

		// Token: 0x06000C5A RID: 3162 RVA: 0x00051B70 File Offset: 0x0004FD70
		private Task WriteEndElementAsync_FinishWrite()
		{
			try
			{
				int num = this.elemTop;
				int prevNSTop = this.elemScopeStack[num].prevNSTop;
				if (this.useNsHashtable && prevNSTop < this.nsTop)
				{
					this.PopNamespaces(prevNSTop + 1, this.nsTop);
				}
				this.nsTop = prevNSTop;
				if ((this.elemTop = num - 1) == 0)
				{
					if (this.conformanceLevel == ConformanceLevel.Document)
					{
						this.currentState = XmlWellFormedWriter.State.AfterRootEle;
					}
					else
					{
						this.currentState = XmlWellFormedWriter.State.TopLevel;
					}
				}
			}
			catch
			{
				this.currentState = XmlWellFormedWriter.State.Error;
				throw;
			}
			return AsyncHelper.DoneTask;
		}

		// Token: 0x06000C5B RID: 3163 RVA: 0x00051C08 File Offset: 0x0004FE08
		public override Task WriteFullEndElementAsync()
		{
			Task result;
			try
			{
				Task task = this.AdvanceStateAsync(XmlWellFormedWriter.Token.EndElement);
				result = this.SequenceRun(task, new Func<Task>(this.WriteFullEndElementAsync_NoAdvanceState));
			}
			catch
			{
				this.currentState = XmlWellFormedWriter.State.Error;
				throw;
			}
			return result;
		}

		// Token: 0x06000C5C RID: 3164 RVA: 0x00051C50 File Offset: 0x0004FE50
		private Task WriteFullEndElementAsync_NoAdvanceState()
		{
			Task result;
			try
			{
				int num = this.elemTop;
				if (num == 0)
				{
					throw new XmlException("There was no XML start tag open.", string.Empty);
				}
				Task task;
				if (this.rawWriter != null)
				{
					task = this.elemScopeStack[num].WriteFullEndElementAsync(this.rawWriter);
				}
				else
				{
					task = this.writer.WriteFullEndElementAsync();
				}
				result = this.SequenceRun(task, new Func<Task>(this.WriteEndElementAsync_FinishWrite));
			}
			catch
			{
				this.currentState = XmlWellFormedWriter.State.Error;
				throw;
			}
			return result;
		}

		// Token: 0x06000C5D RID: 3165 RVA: 0x00051CD8 File Offset: 0x0004FED8
		protected internal override Task WriteStartAttributeAsync(string prefix, string localName, string namespaceName)
		{
			Task result;
			try
			{
				if (localName == null || localName.Length == 0)
				{
					if (!(prefix == "xmlns"))
					{
						throw new ArgumentException(Res.GetString("The empty string '' is not a valid local name."));
					}
					localName = "xmlns";
					prefix = string.Empty;
				}
				this.CheckNCName(localName);
				Task task = this.AdvanceStateAsync(XmlWellFormedWriter.Token.StartAttribute);
				if (task.IsSuccess())
				{
					result = this.WriteStartAttributeAsync_NoAdvanceState(prefix, localName, namespaceName);
				}
				else
				{
					result = this.WriteStartAttributeAsync_NoAdvanceState(task, prefix, localName, namespaceName);
				}
			}
			catch
			{
				this.currentState = XmlWellFormedWriter.State.Error;
				throw;
			}
			return result;
		}

		// Token: 0x06000C5E RID: 3166 RVA: 0x00051D6C File Offset: 0x0004FF6C
		private Task WriteStartAttributeAsync_NoAdvanceState(string prefix, string localName, string namespaceName)
		{
			Task result;
			try
			{
				if (prefix == null)
				{
					if (namespaceName != null && (!(localName == "xmlns") || !(namespaceName == "http://www.w3.org/2000/xmlns/")))
					{
						prefix = this.LookupPrefix(namespaceName);
					}
					if (prefix == null)
					{
						prefix = string.Empty;
					}
				}
				if (namespaceName == null)
				{
					if (prefix != null && prefix.Length > 0)
					{
						namespaceName = this.LookupNamespace(prefix);
					}
					if (namespaceName == null)
					{
						namespaceName = string.Empty;
					}
				}
				if (prefix.Length == 0)
				{
					if (localName[0] == 'x' && localName == "xmlns")
					{
						if (namespaceName.Length > 0 && namespaceName != "http://www.w3.org/2000/xmlns/")
						{
							throw new ArgumentException(Res.GetString("Prefix \"xmlns\" is reserved for use by XML."));
						}
						this.curDeclPrefix = string.Empty;
						this.SetSpecialAttribute(XmlWellFormedWriter.SpecialAttribute.DefaultXmlns);
						goto IL_1DE;
					}
					else if (namespaceName.Length > 0)
					{
						prefix = this.LookupPrefix(namespaceName);
						if (prefix == null || prefix.Length == 0)
						{
							prefix = this.GeneratePrefix();
						}
					}
				}
				else
				{
					if (prefix[0] == 'x')
					{
						if (prefix == "xmlns")
						{
							if (namespaceName.Length > 0 && namespaceName != "http://www.w3.org/2000/xmlns/")
							{
								throw new ArgumentException(Res.GetString("Prefix \"xmlns\" is reserved for use by XML."));
							}
							this.curDeclPrefix = localName;
							this.SetSpecialAttribute(XmlWellFormedWriter.SpecialAttribute.PrefixedXmlns);
							goto IL_1DE;
						}
						else if (prefix == "xml")
						{
							if (namespaceName.Length > 0 && namespaceName != "http://www.w3.org/XML/1998/namespace")
							{
								throw new ArgumentException(Res.GetString("Prefix \"xml\" is reserved for use by XML and can be mapped only to namespace name \"http://www.w3.org/XML/1998/namespace\"."));
							}
							if (localName == "space")
							{
								this.SetSpecialAttribute(XmlWellFormedWriter.SpecialAttribute.XmlSpace);
								goto IL_1DE;
							}
							if (localName == "lang")
							{
								this.SetSpecialAttribute(XmlWellFormedWriter.SpecialAttribute.XmlLang);
								goto IL_1DE;
							}
						}
					}
					this.CheckNCName(prefix);
					if (namespaceName.Length == 0)
					{
						prefix = string.Empty;
					}
					else
					{
						string text = this.LookupLocalNamespace(prefix);
						if (text != null && text != namespaceName)
						{
							prefix = this.GeneratePrefix();
						}
					}
				}
				if (prefix.Length != 0)
				{
					this.PushNamespaceImplicit(prefix, namespaceName);
				}
				IL_1DE:
				this.AddAttribute(prefix, localName, namespaceName);
				if (this.specAttr == XmlWellFormedWriter.SpecialAttribute.No)
				{
					result = this.TryReturnTask(this.writer.WriteStartAttributeAsync(prefix, localName, namespaceName));
				}
				else
				{
					result = AsyncHelper.DoneTask;
				}
			}
			catch
			{
				this.currentState = XmlWellFormedWriter.State.Error;
				throw;
			}
			return result;
		}

		// Token: 0x06000C5F RID: 3167 RVA: 0x00051FB0 File Offset: 0x000501B0
		private Task WriteStartAttributeAsync_NoAdvanceState(Task task, string prefix, string localName, string namespaceName)
		{
			XmlWellFormedWriter.<WriteStartAttributeAsync_NoAdvanceState>d__133 <WriteStartAttributeAsync_NoAdvanceState>d__;
			<WriteStartAttributeAsync_NoAdvanceState>d__.<>4__this = this;
			<WriteStartAttributeAsync_NoAdvanceState>d__.task = task;
			<WriteStartAttributeAsync_NoAdvanceState>d__.prefix = prefix;
			<WriteStartAttributeAsync_NoAdvanceState>d__.localName = localName;
			<WriteStartAttributeAsync_NoAdvanceState>d__.namespaceName = namespaceName;
			<WriteStartAttributeAsync_NoAdvanceState>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<WriteStartAttributeAsync_NoAdvanceState>d__.<>1__state = -1;
			<WriteStartAttributeAsync_NoAdvanceState>d__.<>t__builder.Start<XmlWellFormedWriter.<WriteStartAttributeAsync_NoAdvanceState>d__133>(ref <WriteStartAttributeAsync_NoAdvanceState>d__);
			return <WriteStartAttributeAsync_NoAdvanceState>d__.<>t__builder.Task;
		}

		// Token: 0x06000C60 RID: 3168 RVA: 0x00052014 File Offset: 0x00050214
		protected internal override Task WriteEndAttributeAsync()
		{
			Task result;
			try
			{
				Task task = this.AdvanceStateAsync(XmlWellFormedWriter.Token.EndAttribute);
				result = this.SequenceRun(task, new Func<Task>(this.WriteEndAttributeAsync_NoAdvance));
			}
			catch
			{
				this.currentState = XmlWellFormedWriter.State.Error;
				throw;
			}
			return result;
		}

		// Token: 0x06000C61 RID: 3169 RVA: 0x0005205C File Offset: 0x0005025C
		private Task WriteEndAttributeAsync_NoAdvance()
		{
			Task result;
			try
			{
				if (this.specAttr != XmlWellFormedWriter.SpecialAttribute.No)
				{
					result = this.WriteEndAttributeAsync_SepcialAtt();
				}
				else
				{
					result = this.TryReturnTask(this.writer.WriteEndAttributeAsync());
				}
			}
			catch
			{
				this.currentState = XmlWellFormedWriter.State.Error;
				throw;
			}
			return result;
		}

		// Token: 0x06000C62 RID: 3170 RVA: 0x000520AC File Offset: 0x000502AC
		private Task WriteEndAttributeAsync_SepcialAtt()
		{
			XmlWellFormedWriter.<WriteEndAttributeAsync_SepcialAtt>d__136 <WriteEndAttributeAsync_SepcialAtt>d__;
			<WriteEndAttributeAsync_SepcialAtt>d__.<>4__this = this;
			<WriteEndAttributeAsync_SepcialAtt>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<WriteEndAttributeAsync_SepcialAtt>d__.<>1__state = -1;
			<WriteEndAttributeAsync_SepcialAtt>d__.<>t__builder.Start<XmlWellFormedWriter.<WriteEndAttributeAsync_SepcialAtt>d__136>(ref <WriteEndAttributeAsync_SepcialAtt>d__);
			return <WriteEndAttributeAsync_SepcialAtt>d__.<>t__builder.Task;
		}

		// Token: 0x06000C63 RID: 3171 RVA: 0x000520F0 File Offset: 0x000502F0
		public override Task WriteCDataAsync(string text)
		{
			XmlWellFormedWriter.<WriteCDataAsync>d__137 <WriteCDataAsync>d__;
			<WriteCDataAsync>d__.<>4__this = this;
			<WriteCDataAsync>d__.text = text;
			<WriteCDataAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<WriteCDataAsync>d__.<>1__state = -1;
			<WriteCDataAsync>d__.<>t__builder.Start<XmlWellFormedWriter.<WriteCDataAsync>d__137>(ref <WriteCDataAsync>d__);
			return <WriteCDataAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000C64 RID: 3172 RVA: 0x0005213C File Offset: 0x0005033C
		public override Task WriteCommentAsync(string text)
		{
			XmlWellFormedWriter.<WriteCommentAsync>d__138 <WriteCommentAsync>d__;
			<WriteCommentAsync>d__.<>4__this = this;
			<WriteCommentAsync>d__.text = text;
			<WriteCommentAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<WriteCommentAsync>d__.<>1__state = -1;
			<WriteCommentAsync>d__.<>t__builder.Start<XmlWellFormedWriter.<WriteCommentAsync>d__138>(ref <WriteCommentAsync>d__);
			return <WriteCommentAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000C65 RID: 3173 RVA: 0x00052188 File Offset: 0x00050388
		public override Task WriteProcessingInstructionAsync(string name, string text)
		{
			XmlWellFormedWriter.<WriteProcessingInstructionAsync>d__139 <WriteProcessingInstructionAsync>d__;
			<WriteProcessingInstructionAsync>d__.<>4__this = this;
			<WriteProcessingInstructionAsync>d__.name = name;
			<WriteProcessingInstructionAsync>d__.text = text;
			<WriteProcessingInstructionAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<WriteProcessingInstructionAsync>d__.<>1__state = -1;
			<WriteProcessingInstructionAsync>d__.<>t__builder.Start<XmlWellFormedWriter.<WriteProcessingInstructionAsync>d__139>(ref <WriteProcessingInstructionAsync>d__);
			return <WriteProcessingInstructionAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000C66 RID: 3174 RVA: 0x000521DC File Offset: 0x000503DC
		public override Task WriteEntityRefAsync(string name)
		{
			XmlWellFormedWriter.<WriteEntityRefAsync>d__140 <WriteEntityRefAsync>d__;
			<WriteEntityRefAsync>d__.<>4__this = this;
			<WriteEntityRefAsync>d__.name = name;
			<WriteEntityRefAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<WriteEntityRefAsync>d__.<>1__state = -1;
			<WriteEntityRefAsync>d__.<>t__builder.Start<XmlWellFormedWriter.<WriteEntityRefAsync>d__140>(ref <WriteEntityRefAsync>d__);
			return <WriteEntityRefAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000C67 RID: 3175 RVA: 0x00052228 File Offset: 0x00050428
		public override Task WriteCharEntityAsync(char ch)
		{
			XmlWellFormedWriter.<WriteCharEntityAsync>d__141 <WriteCharEntityAsync>d__;
			<WriteCharEntityAsync>d__.<>4__this = this;
			<WriteCharEntityAsync>d__.ch = ch;
			<WriteCharEntityAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<WriteCharEntityAsync>d__.<>1__state = -1;
			<WriteCharEntityAsync>d__.<>t__builder.Start<XmlWellFormedWriter.<WriteCharEntityAsync>d__141>(ref <WriteCharEntityAsync>d__);
			return <WriteCharEntityAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000C68 RID: 3176 RVA: 0x00052274 File Offset: 0x00050474
		public override Task WriteSurrogateCharEntityAsync(char lowChar, char highChar)
		{
			XmlWellFormedWriter.<WriteSurrogateCharEntityAsync>d__142 <WriteSurrogateCharEntityAsync>d__;
			<WriteSurrogateCharEntityAsync>d__.<>4__this = this;
			<WriteSurrogateCharEntityAsync>d__.lowChar = lowChar;
			<WriteSurrogateCharEntityAsync>d__.highChar = highChar;
			<WriteSurrogateCharEntityAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<WriteSurrogateCharEntityAsync>d__.<>1__state = -1;
			<WriteSurrogateCharEntityAsync>d__.<>t__builder.Start<XmlWellFormedWriter.<WriteSurrogateCharEntityAsync>d__142>(ref <WriteSurrogateCharEntityAsync>d__);
			return <WriteSurrogateCharEntityAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000C69 RID: 3177 RVA: 0x000522C8 File Offset: 0x000504C8
		public override Task WriteWhitespaceAsync(string ws)
		{
			XmlWellFormedWriter.<WriteWhitespaceAsync>d__143 <WriteWhitespaceAsync>d__;
			<WriteWhitespaceAsync>d__.<>4__this = this;
			<WriteWhitespaceAsync>d__.ws = ws;
			<WriteWhitespaceAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<WriteWhitespaceAsync>d__.<>1__state = -1;
			<WriteWhitespaceAsync>d__.<>t__builder.Start<XmlWellFormedWriter.<WriteWhitespaceAsync>d__143>(ref <WriteWhitespaceAsync>d__);
			return <WriteWhitespaceAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000C6A RID: 3178 RVA: 0x00052314 File Offset: 0x00050514
		public override Task WriteStringAsync(string text)
		{
			Task result;
			try
			{
				if (text == null)
				{
					result = AsyncHelper.DoneTask;
				}
				else
				{
					Task task = this.AdvanceStateAsync(XmlWellFormedWriter.Token.Text);
					if (task.IsSuccess())
					{
						result = this.WriteStringAsync_NoAdvanceState(text);
					}
					else
					{
						result = this.WriteStringAsync_NoAdvanceState(task, text);
					}
				}
			}
			catch
			{
				this.currentState = XmlWellFormedWriter.State.Error;
				throw;
			}
			return result;
		}

		// Token: 0x06000C6B RID: 3179 RVA: 0x00052370 File Offset: 0x00050570
		private Task WriteStringAsync_NoAdvanceState(string text)
		{
			Task result;
			try
			{
				if (this.SaveAttrValue)
				{
					this.attrValueCache.WriteString(text);
					result = AsyncHelper.DoneTask;
				}
				else
				{
					result = this.TryReturnTask(this.writer.WriteStringAsync(text));
				}
			}
			catch
			{
				this.currentState = XmlWellFormedWriter.State.Error;
				throw;
			}
			return result;
		}

		// Token: 0x06000C6C RID: 3180 RVA: 0x000523CC File Offset: 0x000505CC
		private Task WriteStringAsync_NoAdvanceState(Task task, string text)
		{
			XmlWellFormedWriter.<WriteStringAsync_NoAdvanceState>d__146 <WriteStringAsync_NoAdvanceState>d__;
			<WriteStringAsync_NoAdvanceState>d__.<>4__this = this;
			<WriteStringAsync_NoAdvanceState>d__.task = task;
			<WriteStringAsync_NoAdvanceState>d__.text = text;
			<WriteStringAsync_NoAdvanceState>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<WriteStringAsync_NoAdvanceState>d__.<>1__state = -1;
			<WriteStringAsync_NoAdvanceState>d__.<>t__builder.Start<XmlWellFormedWriter.<WriteStringAsync_NoAdvanceState>d__146>(ref <WriteStringAsync_NoAdvanceState>d__);
			return <WriteStringAsync_NoAdvanceState>d__.<>t__builder.Task;
		}

		// Token: 0x06000C6D RID: 3181 RVA: 0x00052420 File Offset: 0x00050620
		public override Task WriteCharsAsync(char[] buffer, int index, int count)
		{
			XmlWellFormedWriter.<WriteCharsAsync>d__147 <WriteCharsAsync>d__;
			<WriteCharsAsync>d__.<>4__this = this;
			<WriteCharsAsync>d__.buffer = buffer;
			<WriteCharsAsync>d__.index = index;
			<WriteCharsAsync>d__.count = count;
			<WriteCharsAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<WriteCharsAsync>d__.<>1__state = -1;
			<WriteCharsAsync>d__.<>t__builder.Start<XmlWellFormedWriter.<WriteCharsAsync>d__147>(ref <WriteCharsAsync>d__);
			return <WriteCharsAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000C6E RID: 3182 RVA: 0x0005247C File Offset: 0x0005067C
		public override Task WriteRawAsync(char[] buffer, int index, int count)
		{
			XmlWellFormedWriter.<WriteRawAsync>d__148 <WriteRawAsync>d__;
			<WriteRawAsync>d__.<>4__this = this;
			<WriteRawAsync>d__.buffer = buffer;
			<WriteRawAsync>d__.index = index;
			<WriteRawAsync>d__.count = count;
			<WriteRawAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<WriteRawAsync>d__.<>1__state = -1;
			<WriteRawAsync>d__.<>t__builder.Start<XmlWellFormedWriter.<WriteRawAsync>d__148>(ref <WriteRawAsync>d__);
			return <WriteRawAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000C6F RID: 3183 RVA: 0x000524D8 File Offset: 0x000506D8
		public override Task WriteRawAsync(string data)
		{
			XmlWellFormedWriter.<WriteRawAsync>d__149 <WriteRawAsync>d__;
			<WriteRawAsync>d__.<>4__this = this;
			<WriteRawAsync>d__.data = data;
			<WriteRawAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<WriteRawAsync>d__.<>1__state = -1;
			<WriteRawAsync>d__.<>t__builder.Start<XmlWellFormedWriter.<WriteRawAsync>d__149>(ref <WriteRawAsync>d__);
			return <WriteRawAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000C70 RID: 3184 RVA: 0x00052524 File Offset: 0x00050724
		public override Task WriteBase64Async(byte[] buffer, int index, int count)
		{
			Task result;
			try
			{
				if (buffer == null)
				{
					throw new ArgumentNullException("buffer");
				}
				if (index < 0)
				{
					throw new ArgumentOutOfRangeException("index");
				}
				if (count < 0)
				{
					throw new ArgumentOutOfRangeException("count");
				}
				if (count > buffer.Length - index)
				{
					throw new ArgumentOutOfRangeException("count");
				}
				Task task = this.AdvanceStateAsync(XmlWellFormedWriter.Token.Base64);
				if (task.IsSuccess())
				{
					result = this.TryReturnTask(this.writer.WriteBase64Async(buffer, index, count));
				}
				else
				{
					result = this.WriteBase64Async_NoAdvanceState(task, buffer, index, count);
				}
			}
			catch
			{
				this.currentState = XmlWellFormedWriter.State.Error;
				throw;
			}
			return result;
		}

		// Token: 0x06000C71 RID: 3185 RVA: 0x000525C4 File Offset: 0x000507C4
		private Task WriteBase64Async_NoAdvanceState(Task task, byte[] buffer, int index, int count)
		{
			XmlWellFormedWriter.<WriteBase64Async_NoAdvanceState>d__151 <WriteBase64Async_NoAdvanceState>d__;
			<WriteBase64Async_NoAdvanceState>d__.<>4__this = this;
			<WriteBase64Async_NoAdvanceState>d__.task = task;
			<WriteBase64Async_NoAdvanceState>d__.buffer = buffer;
			<WriteBase64Async_NoAdvanceState>d__.index = index;
			<WriteBase64Async_NoAdvanceState>d__.count = count;
			<WriteBase64Async_NoAdvanceState>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<WriteBase64Async_NoAdvanceState>d__.<>1__state = -1;
			<WriteBase64Async_NoAdvanceState>d__.<>t__builder.Start<XmlWellFormedWriter.<WriteBase64Async_NoAdvanceState>d__151>(ref <WriteBase64Async_NoAdvanceState>d__);
			return <WriteBase64Async_NoAdvanceState>d__.<>t__builder.Task;
		}

		// Token: 0x06000C72 RID: 3186 RVA: 0x00052628 File Offset: 0x00050828
		public override Task FlushAsync()
		{
			XmlWellFormedWriter.<FlushAsync>d__152 <FlushAsync>d__;
			<FlushAsync>d__.<>4__this = this;
			<FlushAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<FlushAsync>d__.<>1__state = -1;
			<FlushAsync>d__.<>t__builder.Start<XmlWellFormedWriter.<FlushAsync>d__152>(ref <FlushAsync>d__);
			return <FlushAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000C73 RID: 3187 RVA: 0x0005266C File Offset: 0x0005086C
		public override Task WriteQualifiedNameAsync(string localName, string ns)
		{
			XmlWellFormedWriter.<WriteQualifiedNameAsync>d__153 <WriteQualifiedNameAsync>d__;
			<WriteQualifiedNameAsync>d__.<>4__this = this;
			<WriteQualifiedNameAsync>d__.localName = localName;
			<WriteQualifiedNameAsync>d__.ns = ns;
			<WriteQualifiedNameAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<WriteQualifiedNameAsync>d__.<>1__state = -1;
			<WriteQualifiedNameAsync>d__.<>t__builder.Start<XmlWellFormedWriter.<WriteQualifiedNameAsync>d__153>(ref <WriteQualifiedNameAsync>d__);
			return <WriteQualifiedNameAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000C74 RID: 3188 RVA: 0x000526C0 File Offset: 0x000508C0
		public override Task WriteBinHexAsync(byte[] buffer, int index, int count)
		{
			XmlWellFormedWriter.<WriteBinHexAsync>d__154 <WriteBinHexAsync>d__;
			<WriteBinHexAsync>d__.<>4__this = this;
			<WriteBinHexAsync>d__.buffer = buffer;
			<WriteBinHexAsync>d__.index = index;
			<WriteBinHexAsync>d__.count = count;
			<WriteBinHexAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<WriteBinHexAsync>d__.<>1__state = -1;
			<WriteBinHexAsync>d__.<>t__builder.Start<XmlWellFormedWriter.<WriteBinHexAsync>d__154>(ref <WriteBinHexAsync>d__);
			return <WriteBinHexAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000C75 RID: 3189 RVA: 0x0005271C File Offset: 0x0005091C
		private Task WriteStartDocumentImplAsync(XmlStandalone standalone)
		{
			XmlWellFormedWriter.<WriteStartDocumentImplAsync>d__155 <WriteStartDocumentImplAsync>d__;
			<WriteStartDocumentImplAsync>d__.<>4__this = this;
			<WriteStartDocumentImplAsync>d__.standalone = standalone;
			<WriteStartDocumentImplAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<WriteStartDocumentImplAsync>d__.<>1__state = -1;
			<WriteStartDocumentImplAsync>d__.<>t__builder.Start<XmlWellFormedWriter.<WriteStartDocumentImplAsync>d__155>(ref <WriteStartDocumentImplAsync>d__);
			return <WriteStartDocumentImplAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000C76 RID: 3190 RVA: 0x00052767 File Offset: 0x00050967
		private Task AdvanceStateAsync_ReturnWhenFinish(Task task, XmlWellFormedWriter.State newState)
		{
			if (task.IsSuccess())
			{
				this.currentState = newState;
				return AsyncHelper.DoneTask;
			}
			return this._AdvanceStateAsync_ReturnWhenFinish(task, newState);
		}

		// Token: 0x06000C77 RID: 3191 RVA: 0x00052788 File Offset: 0x00050988
		private Task _AdvanceStateAsync_ReturnWhenFinish(Task task, XmlWellFormedWriter.State newState)
		{
			XmlWellFormedWriter.<_AdvanceStateAsync_ReturnWhenFinish>d__157 <_AdvanceStateAsync_ReturnWhenFinish>d__;
			<_AdvanceStateAsync_ReturnWhenFinish>d__.<>4__this = this;
			<_AdvanceStateAsync_ReturnWhenFinish>d__.task = task;
			<_AdvanceStateAsync_ReturnWhenFinish>d__.newState = newState;
			<_AdvanceStateAsync_ReturnWhenFinish>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<_AdvanceStateAsync_ReturnWhenFinish>d__.<>1__state = -1;
			<_AdvanceStateAsync_ReturnWhenFinish>d__.<>t__builder.Start<XmlWellFormedWriter.<_AdvanceStateAsync_ReturnWhenFinish>d__157>(ref <_AdvanceStateAsync_ReturnWhenFinish>d__);
			return <_AdvanceStateAsync_ReturnWhenFinish>d__.<>t__builder.Task;
		}

		// Token: 0x06000C78 RID: 3192 RVA: 0x000527DB File Offset: 0x000509DB
		private Task AdvanceStateAsync_ContinueWhenFinish(Task task, XmlWellFormedWriter.State newState, XmlWellFormedWriter.Token token)
		{
			if (task.IsSuccess())
			{
				this.currentState = newState;
				return this.AdvanceStateAsync(token);
			}
			return this._AdvanceStateAsync_ContinueWhenFinish(task, newState, token);
		}

		// Token: 0x06000C79 RID: 3193 RVA: 0x00052800 File Offset: 0x00050A00
		private Task _AdvanceStateAsync_ContinueWhenFinish(Task task, XmlWellFormedWriter.State newState, XmlWellFormedWriter.Token token)
		{
			XmlWellFormedWriter.<_AdvanceStateAsync_ContinueWhenFinish>d__159 <_AdvanceStateAsync_ContinueWhenFinish>d__;
			<_AdvanceStateAsync_ContinueWhenFinish>d__.<>4__this = this;
			<_AdvanceStateAsync_ContinueWhenFinish>d__.task = task;
			<_AdvanceStateAsync_ContinueWhenFinish>d__.newState = newState;
			<_AdvanceStateAsync_ContinueWhenFinish>d__.token = token;
			<_AdvanceStateAsync_ContinueWhenFinish>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<_AdvanceStateAsync_ContinueWhenFinish>d__.<>1__state = -1;
			<_AdvanceStateAsync_ContinueWhenFinish>d__.<>t__builder.Start<XmlWellFormedWriter.<_AdvanceStateAsync_ContinueWhenFinish>d__159>(ref <_AdvanceStateAsync_ContinueWhenFinish>d__);
			return <_AdvanceStateAsync_ContinueWhenFinish>d__.<>t__builder.Task;
		}

		// Token: 0x06000C7A RID: 3194 RVA: 0x0005285C File Offset: 0x00050A5C
		private Task AdvanceStateAsync(XmlWellFormedWriter.Token token)
		{
			if (this.currentState < XmlWellFormedWriter.State.Closed)
			{
				XmlWellFormedWriter.State state;
				for (;;)
				{
					state = this.stateTable[(int)(((int)token << 4) + (int)this.currentState)];
					if (state < XmlWellFormedWriter.State.Error)
					{
						break;
					}
					if (state != XmlWellFormedWriter.State.Error)
					{
						switch (state)
						{
						case XmlWellFormedWriter.State.StartContent:
							goto IL_E3;
						case XmlWellFormedWriter.State.StartContentEle:
							goto IL_F1;
						case XmlWellFormedWriter.State.StartContentB64:
							goto IL_FF;
						case XmlWellFormedWriter.State.StartDoc:
							goto IL_10D;
						case XmlWellFormedWriter.State.StartDocEle:
							goto IL_11B;
						case XmlWellFormedWriter.State.EndAttrSEle:
							goto IL_129;
						case XmlWellFormedWriter.State.EndAttrEEle:
							goto IL_14B;
						case XmlWellFormedWriter.State.EndAttrSCont:
							goto IL_16D;
						case XmlWellFormedWriter.State.EndAttrSAttr:
							goto IL_18F;
						case XmlWellFormedWriter.State.PostB64Cont:
							if (this.rawWriter != null)
							{
								goto Block_6;
							}
							this.currentState = XmlWellFormedWriter.State.Content;
							continue;
						case XmlWellFormedWriter.State.PostB64Attr:
							if (this.rawWriter != null)
							{
								goto Block_7;
							}
							this.currentState = XmlWellFormedWriter.State.Attribute;
							continue;
						case XmlWellFormedWriter.State.PostB64RootAttr:
							if (this.rawWriter != null)
							{
								goto Block_8;
							}
							this.currentState = XmlWellFormedWriter.State.RootLevelAttr;
							continue;
						case XmlWellFormedWriter.State.StartFragEle:
							goto IL_217;
						case XmlWellFormedWriter.State.StartFragCont:
							goto IL_221;
						case XmlWellFormedWriter.State.StartFragB64:
							goto IL_22B;
						case XmlWellFormedWriter.State.StartRootLevelAttr:
							goto IL_235;
						}
						break;
					}
					goto IL_D1;
				}
				goto IL_244;
				IL_D1:
				this.ThrowInvalidStateTransition(token, this.currentState);
				goto IL_244;
				IL_E3:
				return this.AdvanceStateAsync_ReturnWhenFinish(this.StartElementContentAsync(), XmlWellFormedWriter.State.Content);
				IL_F1:
				return this.AdvanceStateAsync_ReturnWhenFinish(this.StartElementContentAsync(), XmlWellFormedWriter.State.Element);
				IL_FF:
				return this.AdvanceStateAsync_ReturnWhenFinish(this.StartElementContentAsync(), XmlWellFormedWriter.State.B64Content);
				IL_10D:
				return this.AdvanceStateAsync_ReturnWhenFinish(this.WriteStartDocumentAsync(), XmlWellFormedWriter.State.Document);
				IL_11B:
				return this.AdvanceStateAsync_ReturnWhenFinish(this.WriteStartDocumentAsync(), XmlWellFormedWriter.State.Element);
				IL_129:
				Task task = this.SequenceRun(this.WriteEndAttributeAsync(), new Func<Task>(this.StartElementContentAsync));
				return this.AdvanceStateAsync_ReturnWhenFinish(task, XmlWellFormedWriter.State.Element);
				IL_14B:
				task = this.SequenceRun(this.WriteEndAttributeAsync(), new Func<Task>(this.StartElementContentAsync));
				return this.AdvanceStateAsync_ReturnWhenFinish(task, XmlWellFormedWriter.State.Content);
				IL_16D:
				task = this.SequenceRun(this.WriteEndAttributeAsync(), new Func<Task>(this.StartElementContentAsync));
				return this.AdvanceStateAsync_ReturnWhenFinish(task, XmlWellFormedWriter.State.Content);
				IL_18F:
				return this.AdvanceStateAsync_ReturnWhenFinish(this.WriteEndAttributeAsync(), XmlWellFormedWriter.State.Attribute);
				Block_6:
				return this.AdvanceStateAsync_ContinueWhenFinish(this.rawWriter.WriteEndBase64Async(), XmlWellFormedWriter.State.Content, token);
				Block_7:
				return this.AdvanceStateAsync_ContinueWhenFinish(this.rawWriter.WriteEndBase64Async(), XmlWellFormedWriter.State.Attribute, token);
				Block_8:
				return this.AdvanceStateAsync_ContinueWhenFinish(this.rawWriter.WriteEndBase64Async(), XmlWellFormedWriter.State.RootLevelAttr, token);
				IL_217:
				this.StartFragment();
				state = XmlWellFormedWriter.State.Element;
				goto IL_244;
				IL_221:
				this.StartFragment();
				state = XmlWellFormedWriter.State.Content;
				goto IL_244;
				IL_22B:
				this.StartFragment();
				state = XmlWellFormedWriter.State.B64Content;
				goto IL_244;
				IL_235:
				return this.AdvanceStateAsync_ReturnWhenFinish(this.WriteEndAttributeAsync(), XmlWellFormedWriter.State.RootLevelAttr);
				IL_244:
				this.currentState = state;
				return AsyncHelper.DoneTask;
			}
			if (this.currentState == XmlWellFormedWriter.State.Closed || this.currentState == XmlWellFormedWriter.State.Error)
			{
				throw new InvalidOperationException(Res.GetString("The Writer is closed or in error state."));
			}
			throw new InvalidOperationException(Res.GetString("Token {0} in state {1} would result in an invalid XML document.", new object[]
			{
				XmlWellFormedWriter.tokenName[(int)token],
				XmlWellFormedWriter.GetStateName(this.currentState)
			}));
		}

		// Token: 0x06000C7B RID: 3195 RVA: 0x00052ABC File Offset: 0x00050CBC
		private Task StartElementContentAsync_WithNS()
		{
			XmlWellFormedWriter.<StartElementContentAsync_WithNS>d__161 <StartElementContentAsync_WithNS>d__;
			<StartElementContentAsync_WithNS>d__.<>4__this = this;
			<StartElementContentAsync_WithNS>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<StartElementContentAsync_WithNS>d__.<>1__state = -1;
			<StartElementContentAsync_WithNS>d__.<>t__builder.Start<XmlWellFormedWriter.<StartElementContentAsync_WithNS>d__161>(ref <StartElementContentAsync_WithNS>d__);
			return <StartElementContentAsync_WithNS>d__.<>t__builder.Task;
		}

		// Token: 0x06000C7C RID: 3196 RVA: 0x00052AFF File Offset: 0x00050CFF
		private Task StartElementContentAsync()
		{
			if (this.nsTop > this.elemScopeStack[this.elemTop].prevNSTop)
			{
				return this.StartElementContentAsync_WithNS();
			}
			if (this.rawWriter != null)
			{
				this.rawWriter.StartElementContent();
			}
			return AsyncHelper.DoneTask;
		}

		// Token: 0x06000C7D RID: 3197 RVA: 0x00052B40 File Offset: 0x00050D40
		// Note: this type is marked as 'beforefieldinit'.
		static XmlWellFormedWriter()
		{
		}

		// Token: 0x06000C7E RID: 3198 RVA: 0x00052CBE File Offset: 0x00050EBE
		[CompilerGenerated]
		[DebuggerHidden]
		private Task <>n__0(byte[] buffer, int index, int count)
		{
			return base.WriteBinHexAsync(buffer, index, count);
		}

		// Token: 0x04000D6F RID: 3439
		private XmlWriter writer;

		// Token: 0x04000D70 RID: 3440
		private XmlRawWriter rawWriter;

		// Token: 0x04000D71 RID: 3441
		private IXmlNamespaceResolver predefinedNamespaces;

		// Token: 0x04000D72 RID: 3442
		private XmlWellFormedWriter.Namespace[] nsStack;

		// Token: 0x04000D73 RID: 3443
		private int nsTop;

		// Token: 0x04000D74 RID: 3444
		private Dictionary<string, int> nsHashtable;

		// Token: 0x04000D75 RID: 3445
		private bool useNsHashtable;

		// Token: 0x04000D76 RID: 3446
		private XmlWellFormedWriter.ElementScope[] elemScopeStack;

		// Token: 0x04000D77 RID: 3447
		private int elemTop;

		// Token: 0x04000D78 RID: 3448
		private XmlWellFormedWriter.AttrName[] attrStack;

		// Token: 0x04000D79 RID: 3449
		private int attrCount;

		// Token: 0x04000D7A RID: 3450
		private Dictionary<string, int> attrHashTable;

		// Token: 0x04000D7B RID: 3451
		private XmlWellFormedWriter.SpecialAttribute specAttr;

		// Token: 0x04000D7C RID: 3452
		private XmlWellFormedWriter.AttributeValueCache attrValueCache;

		// Token: 0x04000D7D RID: 3453
		private string curDeclPrefix;

		// Token: 0x04000D7E RID: 3454
		private XmlWellFormedWriter.State[] stateTable;

		// Token: 0x04000D7F RID: 3455
		private XmlWellFormedWriter.State currentState;

		// Token: 0x04000D80 RID: 3456
		private bool checkCharacters;

		// Token: 0x04000D81 RID: 3457
		private bool omitDuplNamespaces;

		// Token: 0x04000D82 RID: 3458
		private bool writeEndDocumentOnClose;

		// Token: 0x04000D83 RID: 3459
		private ConformanceLevel conformanceLevel;

		// Token: 0x04000D84 RID: 3460
		private bool dtdWritten;

		// Token: 0x04000D85 RID: 3461
		private bool xmlDeclFollows;

		// Token: 0x04000D86 RID: 3462
		private XmlCharType xmlCharType = XmlCharType.Instance;

		// Token: 0x04000D87 RID: 3463
		private SecureStringHasher hasher;

		// Token: 0x04000D88 RID: 3464
		private const int ElementStackInitialSize = 8;

		// Token: 0x04000D89 RID: 3465
		private const int NamespaceStackInitialSize = 8;

		// Token: 0x04000D8A RID: 3466
		private const int AttributeArrayInitialSize = 8;

		// Token: 0x04000D8B RID: 3467
		private const int MaxAttrDuplWalkCount = 14;

		// Token: 0x04000D8C RID: 3468
		private const int MaxNamespacesWalkCount = 16;

		// Token: 0x04000D8D RID: 3469
		internal static readonly string[] stateName = new string[]
		{
			"Start",
			"TopLevel",
			"Document",
			"Element Start Tag",
			"Element Content",
			"Element Content",
			"Attribute",
			"EndRootElement",
			"Attribute",
			"Special Attribute",
			"End Document",
			"Root Level Attribute Value",
			"Root Level Special Attribute Value",
			"Root Level Base64 Attribute Value",
			"After Root Level Attribute",
			"Closed",
			"Error"
		};

		// Token: 0x04000D8E RID: 3470
		internal static readonly string[] tokenName = new string[]
		{
			"StartDocument",
			"EndDocument",
			"PI",
			"Comment",
			"DTD",
			"StartElement",
			"EndElement",
			"StartAttribute",
			"EndAttribute",
			"Text",
			"CDATA",
			"Atomic value",
			"Base64",
			"RawData",
			"Whitespace"
		};

		// Token: 0x04000D8F RID: 3471
		private static WriteState[] state2WriteState = new WriteState[]
		{
			WriteState.Start,
			WriteState.Prolog,
			WriteState.Prolog,
			WriteState.Element,
			WriteState.Content,
			WriteState.Content,
			WriteState.Attribute,
			WriteState.Content,
			WriteState.Attribute,
			WriteState.Attribute,
			WriteState.Content,
			WriteState.Attribute,
			WriteState.Attribute,
			WriteState.Attribute,
			WriteState.Attribute,
			WriteState.Closed,
			WriteState.Error
		};

		// Token: 0x04000D90 RID: 3472
		private static readonly XmlWellFormedWriter.State[] StateTableDocument = new XmlWellFormedWriter.State[]
		{
			XmlWellFormedWriter.State.Document,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.PostB64Cont,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.PostB64Cont,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.EndDocument,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.StartDoc,
			XmlWellFormedWriter.State.TopLevel,
			XmlWellFormedWriter.State.Document,
			XmlWellFormedWriter.State.StartContent,
			XmlWellFormedWriter.State.Content,
			XmlWellFormedWriter.State.PostB64Cont,
			XmlWellFormedWriter.State.PostB64Attr,
			XmlWellFormedWriter.State.AfterRootEle,
			XmlWellFormedWriter.State.EndAttrSCont,
			XmlWellFormedWriter.State.EndAttrSCont,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.StartDoc,
			XmlWellFormedWriter.State.TopLevel,
			XmlWellFormedWriter.State.Document,
			XmlWellFormedWriter.State.StartContent,
			XmlWellFormedWriter.State.Content,
			XmlWellFormedWriter.State.PostB64Cont,
			XmlWellFormedWriter.State.PostB64Attr,
			XmlWellFormedWriter.State.AfterRootEle,
			XmlWellFormedWriter.State.EndAttrSCont,
			XmlWellFormedWriter.State.EndAttrSCont,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.StartDoc,
			XmlWellFormedWriter.State.TopLevel,
			XmlWellFormedWriter.State.Document,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.PostB64Cont,
			XmlWellFormedWriter.State.PostB64Attr,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.StartDocEle,
			XmlWellFormedWriter.State.Element,
			XmlWellFormedWriter.State.Element,
			XmlWellFormedWriter.State.StartContentEle,
			XmlWellFormedWriter.State.Element,
			XmlWellFormedWriter.State.PostB64Cont,
			XmlWellFormedWriter.State.PostB64Attr,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.EndAttrSEle,
			XmlWellFormedWriter.State.EndAttrSEle,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.StartContent,
			XmlWellFormedWriter.State.Content,
			XmlWellFormedWriter.State.PostB64Cont,
			XmlWellFormedWriter.State.PostB64Attr,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.EndAttrEEle,
			XmlWellFormedWriter.State.EndAttrEEle,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Attribute,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.PostB64Cont,
			XmlWellFormedWriter.State.PostB64Attr,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.EndAttrSAttr,
			XmlWellFormedWriter.State.EndAttrSAttr,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.PostB64Cont,
			XmlWellFormedWriter.State.PostB64Attr,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Element,
			XmlWellFormedWriter.State.Element,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.StartContent,
			XmlWellFormedWriter.State.Content,
			XmlWellFormedWriter.State.PostB64Cont,
			XmlWellFormedWriter.State.PostB64Attr,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Attribute,
			XmlWellFormedWriter.State.SpecialAttr,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.StartContent,
			XmlWellFormedWriter.State.Content,
			XmlWellFormedWriter.State.PostB64Cont,
			XmlWellFormedWriter.State.PostB64Attr,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.EndAttrSCont,
			XmlWellFormedWriter.State.EndAttrSCont,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.StartContent,
			XmlWellFormedWriter.State.Content,
			XmlWellFormedWriter.State.PostB64Cont,
			XmlWellFormedWriter.State.PostB64Attr,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Attribute,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.StartContentB64,
			XmlWellFormedWriter.State.B64Content,
			XmlWellFormedWriter.State.B64Content,
			XmlWellFormedWriter.State.B64Attribute,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.B64Attribute,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.StartDoc,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Document,
			XmlWellFormedWriter.State.StartContent,
			XmlWellFormedWriter.State.Content,
			XmlWellFormedWriter.State.PostB64Cont,
			XmlWellFormedWriter.State.PostB64Attr,
			XmlWellFormedWriter.State.AfterRootEle,
			XmlWellFormedWriter.State.Attribute,
			XmlWellFormedWriter.State.SpecialAttr,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.StartDoc,
			XmlWellFormedWriter.State.TopLevel,
			XmlWellFormedWriter.State.Document,
			XmlWellFormedWriter.State.StartContent,
			XmlWellFormedWriter.State.Content,
			XmlWellFormedWriter.State.PostB64Cont,
			XmlWellFormedWriter.State.PostB64Attr,
			XmlWellFormedWriter.State.AfterRootEle,
			XmlWellFormedWriter.State.Attribute,
			XmlWellFormedWriter.State.SpecialAttr,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error
		};

		// Token: 0x04000D91 RID: 3473
		private static readonly XmlWellFormedWriter.State[] StateTableAuto = new XmlWellFormedWriter.State[]
		{
			XmlWellFormedWriter.State.Document,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.PostB64Cont,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.PostB64Cont,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.EndDocument,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.TopLevel,
			XmlWellFormedWriter.State.TopLevel,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.StartContent,
			XmlWellFormedWriter.State.Content,
			XmlWellFormedWriter.State.PostB64Cont,
			XmlWellFormedWriter.State.PostB64Attr,
			XmlWellFormedWriter.State.AfterRootEle,
			XmlWellFormedWriter.State.EndAttrSCont,
			XmlWellFormedWriter.State.EndAttrSCont,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.TopLevel,
			XmlWellFormedWriter.State.TopLevel,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.StartContent,
			XmlWellFormedWriter.State.Content,
			XmlWellFormedWriter.State.PostB64Cont,
			XmlWellFormedWriter.State.PostB64Attr,
			XmlWellFormedWriter.State.AfterRootEle,
			XmlWellFormedWriter.State.EndAttrSCont,
			XmlWellFormedWriter.State.EndAttrSCont,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.StartDoc,
			XmlWellFormedWriter.State.TopLevel,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.PostB64Cont,
			XmlWellFormedWriter.State.PostB64Attr,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.StartFragEle,
			XmlWellFormedWriter.State.Element,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.StartContentEle,
			XmlWellFormedWriter.State.Element,
			XmlWellFormedWriter.State.PostB64Cont,
			XmlWellFormedWriter.State.PostB64Attr,
			XmlWellFormedWriter.State.Element,
			XmlWellFormedWriter.State.EndAttrSEle,
			XmlWellFormedWriter.State.EndAttrSEle,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.StartContent,
			XmlWellFormedWriter.State.Content,
			XmlWellFormedWriter.State.PostB64Cont,
			XmlWellFormedWriter.State.PostB64Attr,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.EndAttrEEle,
			XmlWellFormedWriter.State.EndAttrEEle,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.RootLevelAttr,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Attribute,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.PostB64Cont,
			XmlWellFormedWriter.State.PostB64Attr,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.EndAttrSAttr,
			XmlWellFormedWriter.State.EndAttrSAttr,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.StartRootLevelAttr,
			XmlWellFormedWriter.State.StartRootLevelAttr,
			XmlWellFormedWriter.State.PostB64RootAttr,
			XmlWellFormedWriter.State.RootLevelAttr,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.PostB64Cont,
			XmlWellFormedWriter.State.PostB64Attr,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Element,
			XmlWellFormedWriter.State.Element,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.AfterRootLevelAttr,
			XmlWellFormedWriter.State.AfterRootLevelAttr,
			XmlWellFormedWriter.State.PostB64RootAttr,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.StartFragCont,
			XmlWellFormedWriter.State.StartFragCont,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.StartContent,
			XmlWellFormedWriter.State.Content,
			XmlWellFormedWriter.State.PostB64Cont,
			XmlWellFormedWriter.State.PostB64Attr,
			XmlWellFormedWriter.State.Content,
			XmlWellFormedWriter.State.Attribute,
			XmlWellFormedWriter.State.SpecialAttr,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.RootLevelAttr,
			XmlWellFormedWriter.State.RootLevelSpecAttr,
			XmlWellFormedWriter.State.PostB64RootAttr,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.StartFragCont,
			XmlWellFormedWriter.State.StartFragCont,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.StartContent,
			XmlWellFormedWriter.State.Content,
			XmlWellFormedWriter.State.PostB64Cont,
			XmlWellFormedWriter.State.PostB64Attr,
			XmlWellFormedWriter.State.Content,
			XmlWellFormedWriter.State.EndAttrSCont,
			XmlWellFormedWriter.State.EndAttrSCont,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.StartFragCont,
			XmlWellFormedWriter.State.StartFragCont,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.StartContent,
			XmlWellFormedWriter.State.Content,
			XmlWellFormedWriter.State.PostB64Cont,
			XmlWellFormedWriter.State.PostB64Attr,
			XmlWellFormedWriter.State.Content,
			XmlWellFormedWriter.State.Attribute,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.RootLevelAttr,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.PostB64RootAttr,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.StartFragB64,
			XmlWellFormedWriter.State.StartFragB64,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.StartContentB64,
			XmlWellFormedWriter.State.B64Content,
			XmlWellFormedWriter.State.B64Content,
			XmlWellFormedWriter.State.B64Attribute,
			XmlWellFormedWriter.State.B64Content,
			XmlWellFormedWriter.State.B64Attribute,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.RootLevelB64Attr,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.RootLevelB64Attr,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.StartFragCont,
			XmlWellFormedWriter.State.TopLevel,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.StartContent,
			XmlWellFormedWriter.State.Content,
			XmlWellFormedWriter.State.PostB64Cont,
			XmlWellFormedWriter.State.PostB64Attr,
			XmlWellFormedWriter.State.Content,
			XmlWellFormedWriter.State.Attribute,
			XmlWellFormedWriter.State.SpecialAttr,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.RootLevelAttr,
			XmlWellFormedWriter.State.RootLevelSpecAttr,
			XmlWellFormedWriter.State.PostB64RootAttr,
			XmlWellFormedWriter.State.AfterRootLevelAttr,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.TopLevel,
			XmlWellFormedWriter.State.TopLevel,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.StartContent,
			XmlWellFormedWriter.State.Content,
			XmlWellFormedWriter.State.PostB64Cont,
			XmlWellFormedWriter.State.PostB64Attr,
			XmlWellFormedWriter.State.AfterRootEle,
			XmlWellFormedWriter.State.Attribute,
			XmlWellFormedWriter.State.SpecialAttr,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.RootLevelAttr,
			XmlWellFormedWriter.State.RootLevelSpecAttr,
			XmlWellFormedWriter.State.PostB64RootAttr,
			XmlWellFormedWriter.State.AfterRootLevelAttr,
			XmlWellFormedWriter.State.Error
		};

		// Token: 0x0200014D RID: 333
		private enum State
		{
			// Token: 0x04000D93 RID: 3475
			Start,
			// Token: 0x04000D94 RID: 3476
			TopLevel,
			// Token: 0x04000D95 RID: 3477
			Document,
			// Token: 0x04000D96 RID: 3478
			Element,
			// Token: 0x04000D97 RID: 3479
			Content,
			// Token: 0x04000D98 RID: 3480
			B64Content,
			// Token: 0x04000D99 RID: 3481
			B64Attribute,
			// Token: 0x04000D9A RID: 3482
			AfterRootEle,
			// Token: 0x04000D9B RID: 3483
			Attribute,
			// Token: 0x04000D9C RID: 3484
			SpecialAttr,
			// Token: 0x04000D9D RID: 3485
			EndDocument,
			// Token: 0x04000D9E RID: 3486
			RootLevelAttr,
			// Token: 0x04000D9F RID: 3487
			RootLevelSpecAttr,
			// Token: 0x04000DA0 RID: 3488
			RootLevelB64Attr,
			// Token: 0x04000DA1 RID: 3489
			AfterRootLevelAttr,
			// Token: 0x04000DA2 RID: 3490
			Closed,
			// Token: 0x04000DA3 RID: 3491
			Error,
			// Token: 0x04000DA4 RID: 3492
			StartContent = 101,
			// Token: 0x04000DA5 RID: 3493
			StartContentEle,
			// Token: 0x04000DA6 RID: 3494
			StartContentB64,
			// Token: 0x04000DA7 RID: 3495
			StartDoc,
			// Token: 0x04000DA8 RID: 3496
			StartDocEle = 106,
			// Token: 0x04000DA9 RID: 3497
			EndAttrSEle,
			// Token: 0x04000DAA RID: 3498
			EndAttrEEle,
			// Token: 0x04000DAB RID: 3499
			EndAttrSCont,
			// Token: 0x04000DAC RID: 3500
			EndAttrSAttr = 111,
			// Token: 0x04000DAD RID: 3501
			PostB64Cont,
			// Token: 0x04000DAE RID: 3502
			PostB64Attr,
			// Token: 0x04000DAF RID: 3503
			PostB64RootAttr,
			// Token: 0x04000DB0 RID: 3504
			StartFragEle,
			// Token: 0x04000DB1 RID: 3505
			StartFragCont,
			// Token: 0x04000DB2 RID: 3506
			StartFragB64,
			// Token: 0x04000DB3 RID: 3507
			StartRootLevelAttr
		}

		// Token: 0x0200014E RID: 334
		private enum Token
		{
			// Token: 0x04000DB5 RID: 3509
			StartDocument,
			// Token: 0x04000DB6 RID: 3510
			EndDocument,
			// Token: 0x04000DB7 RID: 3511
			PI,
			// Token: 0x04000DB8 RID: 3512
			Comment,
			// Token: 0x04000DB9 RID: 3513
			Dtd,
			// Token: 0x04000DBA RID: 3514
			StartElement,
			// Token: 0x04000DBB RID: 3515
			EndElement,
			// Token: 0x04000DBC RID: 3516
			StartAttribute,
			// Token: 0x04000DBD RID: 3517
			EndAttribute,
			// Token: 0x04000DBE RID: 3518
			Text,
			// Token: 0x04000DBF RID: 3519
			CData,
			// Token: 0x04000DC0 RID: 3520
			AtomicValue,
			// Token: 0x04000DC1 RID: 3521
			Base64,
			// Token: 0x04000DC2 RID: 3522
			RawData,
			// Token: 0x04000DC3 RID: 3523
			Whitespace
		}

		// Token: 0x0200014F RID: 335
		private class NamespaceResolverProxy : IXmlNamespaceResolver
		{
			// Token: 0x06000C7F RID: 3199 RVA: 0x00052CC9 File Offset: 0x00050EC9
			internal NamespaceResolverProxy(XmlWellFormedWriter wfWriter)
			{
				this.wfWriter = wfWriter;
			}

			// Token: 0x06000C80 RID: 3200 RVA: 0x0000349C File Offset: 0x0000169C
			IDictionary<string, string> IXmlNamespaceResolver.GetNamespacesInScope(XmlNamespaceScope scope)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000C81 RID: 3201 RVA: 0x00052CD8 File Offset: 0x00050ED8
			string IXmlNamespaceResolver.LookupNamespace(string prefix)
			{
				return this.wfWriter.LookupNamespace(prefix);
			}

			// Token: 0x06000C82 RID: 3202 RVA: 0x00052CE6 File Offset: 0x00050EE6
			string IXmlNamespaceResolver.LookupPrefix(string namespaceName)
			{
				return this.wfWriter.LookupPrefix(namespaceName);
			}

			// Token: 0x04000DC4 RID: 3524
			private XmlWellFormedWriter wfWriter;
		}

		// Token: 0x02000150 RID: 336
		private struct ElementScope
		{
			// Token: 0x06000C83 RID: 3203 RVA: 0x00052CF4 File Offset: 0x00050EF4
			internal void Set(string prefix, string localName, string namespaceUri, int prevNSTop)
			{
				this.prevNSTop = prevNSTop;
				this.prefix = prefix;
				this.namespaceUri = namespaceUri;
				this.localName = localName;
				this.xmlSpace = (XmlSpace)(-1);
				this.xmlLang = null;
			}

			// Token: 0x06000C84 RID: 3204 RVA: 0x00052D21 File Offset: 0x00050F21
			internal void WriteEndElement(XmlRawWriter rawWriter)
			{
				rawWriter.WriteEndElement(this.prefix, this.localName, this.namespaceUri);
			}

			// Token: 0x06000C85 RID: 3205 RVA: 0x00052D3B File Offset: 0x00050F3B
			internal void WriteFullEndElement(XmlRawWriter rawWriter)
			{
				rawWriter.WriteFullEndElement(this.prefix, this.localName, this.namespaceUri);
			}

			// Token: 0x06000C86 RID: 3206 RVA: 0x00052D55 File Offset: 0x00050F55
			internal Task WriteEndElementAsync(XmlRawWriter rawWriter)
			{
				return rawWriter.WriteEndElementAsync(this.prefix, this.localName, this.namespaceUri);
			}

			// Token: 0x06000C87 RID: 3207 RVA: 0x00052D6F File Offset: 0x00050F6F
			internal Task WriteFullEndElementAsync(XmlRawWriter rawWriter)
			{
				return rawWriter.WriteFullEndElementAsync(this.prefix, this.localName, this.namespaceUri);
			}

			// Token: 0x04000DC5 RID: 3525
			internal int prevNSTop;

			// Token: 0x04000DC6 RID: 3526
			internal string prefix;

			// Token: 0x04000DC7 RID: 3527
			internal string localName;

			// Token: 0x04000DC8 RID: 3528
			internal string namespaceUri;

			// Token: 0x04000DC9 RID: 3529
			internal XmlSpace xmlSpace;

			// Token: 0x04000DCA RID: 3530
			internal string xmlLang;
		}

		// Token: 0x02000151 RID: 337
		private enum NamespaceKind
		{
			// Token: 0x04000DCC RID: 3532
			Written,
			// Token: 0x04000DCD RID: 3533
			NeedToWrite,
			// Token: 0x04000DCE RID: 3534
			Implied,
			// Token: 0x04000DCF RID: 3535
			Special
		}

		// Token: 0x02000152 RID: 338
		private struct Namespace
		{
			// Token: 0x06000C88 RID: 3208 RVA: 0x00052D89 File Offset: 0x00050F89
			internal void Set(string prefix, string namespaceUri, XmlWellFormedWriter.NamespaceKind kind)
			{
				this.prefix = prefix;
				this.namespaceUri = namespaceUri;
				this.kind = kind;
				this.prevNsIndex = -1;
			}

			// Token: 0x06000C89 RID: 3209 RVA: 0x00052DA8 File Offset: 0x00050FA8
			internal void WriteDecl(XmlWriter writer, XmlRawWriter rawWriter)
			{
				if (rawWriter != null)
				{
					rawWriter.WriteNamespaceDeclaration(this.prefix, this.namespaceUri);
					return;
				}
				if (this.prefix.Length == 0)
				{
					writer.WriteStartAttribute(string.Empty, "xmlns", "http://www.w3.org/2000/xmlns/");
				}
				else
				{
					writer.WriteStartAttribute("xmlns", this.prefix, "http://www.w3.org/2000/xmlns/");
				}
				writer.WriteString(this.namespaceUri);
				writer.WriteEndAttribute();
			}

			// Token: 0x06000C8A RID: 3210 RVA: 0x00052E18 File Offset: 0x00051018
			internal Task WriteDeclAsync(XmlWriter writer, XmlRawWriter rawWriter)
			{
				XmlWellFormedWriter.Namespace.<WriteDeclAsync>d__6 <WriteDeclAsync>d__;
				<WriteDeclAsync>d__.<>4__this = this;
				<WriteDeclAsync>d__.writer = writer;
				<WriteDeclAsync>d__.rawWriter = rawWriter;
				<WriteDeclAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
				<WriteDeclAsync>d__.<>1__state = -1;
				<WriteDeclAsync>d__.<>t__builder.Start<XmlWellFormedWriter.Namespace.<WriteDeclAsync>d__6>(ref <WriteDeclAsync>d__);
				return <WriteDeclAsync>d__.<>t__builder.Task;
			}

			// Token: 0x04000DD0 RID: 3536
			internal string prefix;

			// Token: 0x04000DD1 RID: 3537
			internal string namespaceUri;

			// Token: 0x04000DD2 RID: 3538
			internal XmlWellFormedWriter.NamespaceKind kind;

			// Token: 0x04000DD3 RID: 3539
			internal int prevNsIndex;

			// Token: 0x02000153 RID: 339
			[CompilerGenerated]
			[StructLayout(LayoutKind.Auto)]
			private struct <WriteDeclAsync>d__6 : IAsyncStateMachine
			{
				// Token: 0x06000C8B RID: 3211 RVA: 0x00052E70 File Offset: 0x00051070
				void IAsyncStateMachine.MoveNext()
				{
					int num = this.<>1__state;
					try
					{
						ConfiguredTaskAwaitable.ConfiguredTaskAwaiter awaiter;
						switch (num)
						{
						case 0:
							awaiter = this.<>u__1;
							this.<>u__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
							this.<>1__state = -1;
							break;
						case 1:
							awaiter = this.<>u__1;
							this.<>u__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
							this.<>1__state = -1;
							goto IL_133;
						case 2:
							awaiter = this.<>u__1;
							this.<>u__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
							this.<>1__state = -1;
							goto IL_1B3;
						case 3:
							awaiter = this.<>u__1;
							this.<>u__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
							this.<>1__state = -1;
							goto IL_227;
						case 4:
							awaiter = this.<>u__1;
							this.<>u__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
							this.<>1__state = -1;
							goto IL_28D;
						default:
							if (this.rawWriter != null)
							{
								awaiter = this.rawWriter.WriteNamespaceDeclarationAsync(this.<>4__this.prefix, this.<>4__this.namespaceUri).ConfigureAwait(false).GetAwaiter();
								if (!awaiter.IsCompleted)
								{
									this.<>1__state = 0;
									this.<>u__1 = awaiter;
									this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlWellFormedWriter.Namespace.<WriteDeclAsync>d__6>(ref awaiter, ref this);
									return;
								}
							}
							else if (this.<>4__this.prefix.Length == 0)
							{
								awaiter = this.writer.WriteStartAttributeAsync(string.Empty, "xmlns", "http://www.w3.org/2000/xmlns/").ConfigureAwait(false).GetAwaiter();
								if (!awaiter.IsCompleted)
								{
									this.<>1__state = 1;
									this.<>u__1 = awaiter;
									this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlWellFormedWriter.Namespace.<WriteDeclAsync>d__6>(ref awaiter, ref this);
									return;
								}
								goto IL_133;
							}
							else
							{
								awaiter = this.writer.WriteStartAttributeAsync("xmlns", this.<>4__this.prefix, "http://www.w3.org/2000/xmlns/").ConfigureAwait(false).GetAwaiter();
								if (!awaiter.IsCompleted)
								{
									this.<>1__state = 2;
									this.<>u__1 = awaiter;
									this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlWellFormedWriter.Namespace.<WriteDeclAsync>d__6>(ref awaiter, ref this);
									return;
								}
								goto IL_1B3;
							}
							break;
						}
						awaiter.GetResult();
						goto IL_294;
						IL_133:
						awaiter.GetResult();
						goto IL_1BA;
						IL_1B3:
						awaiter.GetResult();
						IL_1BA:
						awaiter = this.writer.WriteStringAsync(this.<>4__this.namespaceUri).ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 3;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlWellFormedWriter.Namespace.<WriteDeclAsync>d__6>(ref awaiter, ref this);
							return;
						}
						IL_227:
						awaiter.GetResult();
						awaiter = this.writer.WriteEndAttributeAsync().ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 4;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlWellFormedWriter.Namespace.<WriteDeclAsync>d__6>(ref awaiter, ref this);
							return;
						}
						IL_28D:
						awaiter.GetResult();
						IL_294:;
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

				// Token: 0x06000C8C RID: 3212 RVA: 0x0005315C File Offset: 0x0005135C
				[DebuggerHidden]
				void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
				{
					this.<>t__builder.SetStateMachine(stateMachine);
				}

				// Token: 0x04000DD4 RID: 3540
				public int <>1__state;

				// Token: 0x04000DD5 RID: 3541
				public AsyncTaskMethodBuilder <>t__builder;

				// Token: 0x04000DD6 RID: 3542
				public XmlRawWriter rawWriter;

				// Token: 0x04000DD7 RID: 3543
				public XmlWellFormedWriter.Namespace <>4__this;

				// Token: 0x04000DD8 RID: 3544
				public XmlWriter writer;

				// Token: 0x04000DD9 RID: 3545
				private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter <>u__1;
			}
		}

		// Token: 0x02000154 RID: 340
		private struct AttrName
		{
			// Token: 0x06000C8D RID: 3213 RVA: 0x0005316A File Offset: 0x0005136A
			internal void Set(string prefix, string localName, string namespaceUri)
			{
				this.prefix = prefix;
				this.namespaceUri = namespaceUri;
				this.localName = localName;
				this.prev = 0;
			}

			// Token: 0x06000C8E RID: 3214 RVA: 0x00053188 File Offset: 0x00051388
			internal bool IsDuplicate(string prefix, string localName, string namespaceUri)
			{
				return this.localName == localName && (this.prefix == prefix || this.namespaceUri == namespaceUri);
			}

			// Token: 0x04000DDA RID: 3546
			internal string prefix;

			// Token: 0x04000DDB RID: 3547
			internal string namespaceUri;

			// Token: 0x04000DDC RID: 3548
			internal string localName;

			// Token: 0x04000DDD RID: 3549
			internal int prev;
		}

		// Token: 0x02000155 RID: 341
		private enum SpecialAttribute
		{
			// Token: 0x04000DDF RID: 3551
			No,
			// Token: 0x04000DE0 RID: 3552
			DefaultXmlns,
			// Token: 0x04000DE1 RID: 3553
			PrefixedXmlns,
			// Token: 0x04000DE2 RID: 3554
			XmlSpace,
			// Token: 0x04000DE3 RID: 3555
			XmlLang
		}

		// Token: 0x02000156 RID: 342
		private class AttributeValueCache
		{
			// Token: 0x170001D8 RID: 472
			// (get) Token: 0x06000C8F RID: 3215 RVA: 0x000531B6 File Offset: 0x000513B6
			internal string StringValue
			{
				get
				{
					if (this.singleStringValue != null)
					{
						return this.singleStringValue;
					}
					return this.stringValue.ToString();
				}
			}

			// Token: 0x06000C90 RID: 3216 RVA: 0x000531D4 File Offset: 0x000513D4
			internal void WriteEntityRef(string name)
			{
				if (this.singleStringValue != null)
				{
					this.StartComplexValue();
				}
				if (!(name == "lt"))
				{
					if (!(name == "gt"))
					{
						if (!(name == "quot"))
						{
							if (!(name == "apos"))
							{
								if (!(name == "amp"))
								{
									this.stringValue.Append('&');
									this.stringValue.Append(name);
									this.stringValue.Append(';');
								}
								else
								{
									this.stringValue.Append('&');
								}
							}
							else
							{
								this.stringValue.Append('\'');
							}
						}
						else
						{
							this.stringValue.Append('"');
						}
					}
					else
					{
						this.stringValue.Append('>');
					}
				}
				else
				{
					this.stringValue.Append('<');
				}
				this.AddItem(XmlWellFormedWriter.AttributeValueCache.ItemType.EntityRef, name);
			}

			// Token: 0x06000C91 RID: 3217 RVA: 0x000532B3 File Offset: 0x000514B3
			internal void WriteCharEntity(char ch)
			{
				if (this.singleStringValue != null)
				{
					this.StartComplexValue();
				}
				this.stringValue.Append(ch);
				this.AddItem(XmlWellFormedWriter.AttributeValueCache.ItemType.CharEntity, ch);
			}

			// Token: 0x06000C92 RID: 3218 RVA: 0x000532DD File Offset: 0x000514DD
			internal void WriteSurrogateCharEntity(char lowChar, char highChar)
			{
				if (this.singleStringValue != null)
				{
					this.StartComplexValue();
				}
				this.stringValue.Append(highChar);
				this.stringValue.Append(lowChar);
				this.AddItem(XmlWellFormedWriter.AttributeValueCache.ItemType.SurrogateCharEntity, new char[]
				{
					lowChar,
					highChar
				});
			}

			// Token: 0x06000C93 RID: 3219 RVA: 0x0005331C File Offset: 0x0005151C
			internal void WriteWhitespace(string ws)
			{
				if (this.singleStringValue != null)
				{
					this.StartComplexValue();
				}
				this.stringValue.Append(ws);
				this.AddItem(XmlWellFormedWriter.AttributeValueCache.ItemType.Whitespace, ws);
			}

			// Token: 0x06000C94 RID: 3220 RVA: 0x00053341 File Offset: 0x00051541
			internal void WriteString(string text)
			{
				if (this.singleStringValue != null)
				{
					this.StartComplexValue();
				}
				else if (this.lastItem == -1)
				{
					this.singleStringValue = text;
					return;
				}
				this.stringValue.Append(text);
				this.AddItem(XmlWellFormedWriter.AttributeValueCache.ItemType.String, text);
			}

			// Token: 0x06000C95 RID: 3221 RVA: 0x00053379 File Offset: 0x00051579
			internal void WriteChars(char[] buffer, int index, int count)
			{
				if (this.singleStringValue != null)
				{
					this.StartComplexValue();
				}
				this.stringValue.Append(buffer, index, count);
				this.AddItem(XmlWellFormedWriter.AttributeValueCache.ItemType.StringChars, new XmlWellFormedWriter.AttributeValueCache.BufferChunk(buffer, index, count));
			}

			// Token: 0x06000C96 RID: 3222 RVA: 0x000533A7 File Offset: 0x000515A7
			internal void WriteRaw(char[] buffer, int index, int count)
			{
				if (this.singleStringValue != null)
				{
					this.StartComplexValue();
				}
				this.stringValue.Append(buffer, index, count);
				this.AddItem(XmlWellFormedWriter.AttributeValueCache.ItemType.RawChars, new XmlWellFormedWriter.AttributeValueCache.BufferChunk(buffer, index, count));
			}

			// Token: 0x06000C97 RID: 3223 RVA: 0x000533D5 File Offset: 0x000515D5
			internal void WriteRaw(string data)
			{
				if (this.singleStringValue != null)
				{
					this.StartComplexValue();
				}
				this.stringValue.Append(data);
				this.AddItem(XmlWellFormedWriter.AttributeValueCache.ItemType.Raw, data);
			}

			// Token: 0x06000C98 RID: 3224 RVA: 0x000533FA File Offset: 0x000515FA
			internal void WriteValue(string value)
			{
				if (this.singleStringValue != null)
				{
					this.StartComplexValue();
				}
				this.stringValue.Append(value);
				this.AddItem(XmlWellFormedWriter.AttributeValueCache.ItemType.ValueString, value);
			}

			// Token: 0x06000C99 RID: 3225 RVA: 0x00053420 File Offset: 0x00051620
			internal void Replay(XmlWriter writer)
			{
				if (this.singleStringValue != null)
				{
					writer.WriteString(this.singleStringValue);
					return;
				}
				for (int i = this.firstItem; i <= this.lastItem; i++)
				{
					XmlWellFormedWriter.AttributeValueCache.Item item = this.items[i];
					switch (item.type)
					{
					case XmlWellFormedWriter.AttributeValueCache.ItemType.EntityRef:
						writer.WriteEntityRef((string)item.data);
						break;
					case XmlWellFormedWriter.AttributeValueCache.ItemType.CharEntity:
						writer.WriteCharEntity((char)item.data);
						break;
					case XmlWellFormedWriter.AttributeValueCache.ItemType.SurrogateCharEntity:
					{
						char[] array = (char[])item.data;
						writer.WriteSurrogateCharEntity(array[0], array[1]);
						break;
					}
					case XmlWellFormedWriter.AttributeValueCache.ItemType.Whitespace:
						writer.WriteWhitespace((string)item.data);
						break;
					case XmlWellFormedWriter.AttributeValueCache.ItemType.String:
						writer.WriteString((string)item.data);
						break;
					case XmlWellFormedWriter.AttributeValueCache.ItemType.StringChars:
					{
						XmlWellFormedWriter.AttributeValueCache.BufferChunk bufferChunk = (XmlWellFormedWriter.AttributeValueCache.BufferChunk)item.data;
						writer.WriteChars(bufferChunk.buffer, bufferChunk.index, bufferChunk.count);
						break;
					}
					case XmlWellFormedWriter.AttributeValueCache.ItemType.Raw:
						writer.WriteRaw((string)item.data);
						break;
					case XmlWellFormedWriter.AttributeValueCache.ItemType.RawChars:
					{
						XmlWellFormedWriter.AttributeValueCache.BufferChunk bufferChunk = (XmlWellFormedWriter.AttributeValueCache.BufferChunk)item.data;
						writer.WriteChars(bufferChunk.buffer, bufferChunk.index, bufferChunk.count);
						break;
					}
					case XmlWellFormedWriter.AttributeValueCache.ItemType.ValueString:
						writer.WriteValue((string)item.data);
						break;
					}
				}
			}

			// Token: 0x06000C9A RID: 3226 RVA: 0x00053584 File Offset: 0x00051784
			internal void Trim()
			{
				if (this.singleStringValue != null)
				{
					this.singleStringValue = XmlConvert.TrimString(this.singleStringValue);
					return;
				}
				string text = this.stringValue.ToString();
				string text2 = XmlConvert.TrimString(text);
				if (text != text2)
				{
					this.stringValue = new StringBuilder(text2);
				}
				XmlCharType instance = XmlCharType.Instance;
				int num = this.firstItem;
				while (num == this.firstItem && num <= this.lastItem)
				{
					XmlWellFormedWriter.AttributeValueCache.Item item = this.items[num];
					switch (item.type)
					{
					case XmlWellFormedWriter.AttributeValueCache.ItemType.Whitespace:
						this.firstItem++;
						break;
					case XmlWellFormedWriter.AttributeValueCache.ItemType.String:
					case XmlWellFormedWriter.AttributeValueCache.ItemType.Raw:
					case XmlWellFormedWriter.AttributeValueCache.ItemType.ValueString:
						item.data = XmlConvert.TrimStringStart((string)item.data);
						if (((string)item.data).Length == 0)
						{
							this.firstItem++;
						}
						break;
					case XmlWellFormedWriter.AttributeValueCache.ItemType.StringChars:
					case XmlWellFormedWriter.AttributeValueCache.ItemType.RawChars:
					{
						XmlWellFormedWriter.AttributeValueCache.BufferChunk bufferChunk = (XmlWellFormedWriter.AttributeValueCache.BufferChunk)item.data;
						int num2 = bufferChunk.index + bufferChunk.count;
						while (bufferChunk.index < num2 && instance.IsWhiteSpace(bufferChunk.buffer[bufferChunk.index]))
						{
							bufferChunk.index++;
							bufferChunk.count--;
						}
						if (bufferChunk.index == num2)
						{
							this.firstItem++;
						}
						break;
					}
					}
					num++;
				}
				num = this.lastItem;
				while (num == this.lastItem && num >= this.firstItem)
				{
					XmlWellFormedWriter.AttributeValueCache.Item item2 = this.items[num];
					switch (item2.type)
					{
					case XmlWellFormedWriter.AttributeValueCache.ItemType.Whitespace:
						this.lastItem--;
						break;
					case XmlWellFormedWriter.AttributeValueCache.ItemType.String:
					case XmlWellFormedWriter.AttributeValueCache.ItemType.Raw:
					case XmlWellFormedWriter.AttributeValueCache.ItemType.ValueString:
						item2.data = XmlConvert.TrimStringEnd((string)item2.data);
						if (((string)item2.data).Length == 0)
						{
							this.lastItem--;
						}
						break;
					case XmlWellFormedWriter.AttributeValueCache.ItemType.StringChars:
					case XmlWellFormedWriter.AttributeValueCache.ItemType.RawChars:
					{
						XmlWellFormedWriter.AttributeValueCache.BufferChunk bufferChunk2 = (XmlWellFormedWriter.AttributeValueCache.BufferChunk)item2.data;
						while (bufferChunk2.count > 0 && instance.IsWhiteSpace(bufferChunk2.buffer[bufferChunk2.index + bufferChunk2.count - 1]))
						{
							bufferChunk2.count--;
						}
						if (bufferChunk2.count == 0)
						{
							this.lastItem--;
						}
						break;
					}
					}
					num--;
				}
			}

			// Token: 0x06000C9B RID: 3227 RVA: 0x00053809 File Offset: 0x00051A09
			internal void Clear()
			{
				this.singleStringValue = null;
				this.lastItem = -1;
				this.firstItem = 0;
				this.stringValue.Length = 0;
			}

			// Token: 0x06000C9C RID: 3228 RVA: 0x0005382C File Offset: 0x00051A2C
			private void StartComplexValue()
			{
				this.stringValue.Append(this.singleStringValue);
				this.AddItem(XmlWellFormedWriter.AttributeValueCache.ItemType.String, this.singleStringValue);
				this.singleStringValue = null;
			}

			// Token: 0x06000C9D RID: 3229 RVA: 0x00053854 File Offset: 0x00051A54
			private void AddItem(XmlWellFormedWriter.AttributeValueCache.ItemType type, object data)
			{
				int num = this.lastItem + 1;
				if (this.items == null)
				{
					this.items = new XmlWellFormedWriter.AttributeValueCache.Item[4];
				}
				else if (this.items.Length == num)
				{
					XmlWellFormedWriter.AttributeValueCache.Item[] destinationArray = new XmlWellFormedWriter.AttributeValueCache.Item[num * 2];
					Array.Copy(this.items, destinationArray, num);
					this.items = destinationArray;
				}
				if (this.items[num] == null)
				{
					this.items[num] = new XmlWellFormedWriter.AttributeValueCache.Item();
				}
				this.items[num].Set(type, data);
				this.lastItem = num;
			}

			// Token: 0x06000C9E RID: 3230 RVA: 0x000538D8 File Offset: 0x00051AD8
			internal Task ReplayAsync(XmlWriter writer)
			{
				XmlWellFormedWriter.AttributeValueCache.<ReplayAsync>d__24 <ReplayAsync>d__;
				<ReplayAsync>d__.<>4__this = this;
				<ReplayAsync>d__.writer = writer;
				<ReplayAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
				<ReplayAsync>d__.<>1__state = -1;
				<ReplayAsync>d__.<>t__builder.Start<XmlWellFormedWriter.AttributeValueCache.<ReplayAsync>d__24>(ref <ReplayAsync>d__);
				return <ReplayAsync>d__.<>t__builder.Task;
			}

			// Token: 0x06000C9F RID: 3231 RVA: 0x00053923 File Offset: 0x00051B23
			public AttributeValueCache()
			{
			}

			// Token: 0x04000DE4 RID: 3556
			private StringBuilder stringValue = new StringBuilder();

			// Token: 0x04000DE5 RID: 3557
			private string singleStringValue;

			// Token: 0x04000DE6 RID: 3558
			private XmlWellFormedWriter.AttributeValueCache.Item[] items;

			// Token: 0x04000DE7 RID: 3559
			private int firstItem;

			// Token: 0x04000DE8 RID: 3560
			private int lastItem = -1;

			// Token: 0x02000157 RID: 343
			private enum ItemType
			{
				// Token: 0x04000DEA RID: 3562
				EntityRef,
				// Token: 0x04000DEB RID: 3563
				CharEntity,
				// Token: 0x04000DEC RID: 3564
				SurrogateCharEntity,
				// Token: 0x04000DED RID: 3565
				Whitespace,
				// Token: 0x04000DEE RID: 3566
				String,
				// Token: 0x04000DEF RID: 3567
				StringChars,
				// Token: 0x04000DF0 RID: 3568
				Raw,
				// Token: 0x04000DF1 RID: 3569
				RawChars,
				// Token: 0x04000DF2 RID: 3570
				ValueString
			}

			// Token: 0x02000158 RID: 344
			private class Item
			{
				// Token: 0x06000CA0 RID: 3232 RVA: 0x0000216B File Offset: 0x0000036B
				internal Item()
				{
				}

				// Token: 0x06000CA1 RID: 3233 RVA: 0x0005393D File Offset: 0x00051B3D
				internal void Set(XmlWellFormedWriter.AttributeValueCache.ItemType type, object data)
				{
					this.type = type;
					this.data = data;
				}

				// Token: 0x04000DF3 RID: 3571
				internal XmlWellFormedWriter.AttributeValueCache.ItemType type;

				// Token: 0x04000DF4 RID: 3572
				internal object data;
			}

			// Token: 0x02000159 RID: 345
			private class BufferChunk
			{
				// Token: 0x06000CA2 RID: 3234 RVA: 0x0005394D File Offset: 0x00051B4D
				internal BufferChunk(char[] buffer, int index, int count)
				{
					this.buffer = buffer;
					this.index = index;
					this.count = count;
				}

				// Token: 0x04000DF5 RID: 3573
				internal char[] buffer;

				// Token: 0x04000DF6 RID: 3574
				internal int index;

				// Token: 0x04000DF7 RID: 3575
				internal int count;
			}

			// Token: 0x0200015A RID: 346
			[CompilerGenerated]
			[StructLayout(LayoutKind.Auto)]
			private struct <ReplayAsync>d__24 : IAsyncStateMachine
			{
				// Token: 0x06000CA3 RID: 3235 RVA: 0x0005396C File Offset: 0x00051B6C
				void IAsyncStateMachine.MoveNext()
				{
					int num = this.<>1__state;
					XmlWellFormedWriter.AttributeValueCache attributeValueCache = this.<>4__this;
					try
					{
						ConfiguredTaskAwaitable.ConfiguredTaskAwaiter awaiter;
						switch (num)
						{
						case 0:
							awaiter = this.<>u__1;
							this.<>u__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
							this.<>1__state = -1;
							break;
						case 1:
							awaiter = this.<>u__1;
							this.<>u__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
							this.<>1__state = -1;
							goto IL_181;
						case 2:
							awaiter = this.<>u__1;
							this.<>u__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
							this.<>1__state = -1;
							goto IL_1FC;
						case 3:
							awaiter = this.<>u__1;
							this.<>u__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
							this.<>1__state = -1;
							goto IL_281;
						case 4:
							awaiter = this.<>u__1;
							this.<>u__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
							this.<>1__state = -1;
							goto IL_2FC;
						case 5:
							awaiter = this.<>u__1;
							this.<>u__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
							this.<>1__state = -1;
							goto IL_377;
						case 6:
							awaiter = this.<>u__1;
							this.<>u__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
							this.<>1__state = -1;
							goto IL_405;
						case 7:
							awaiter = this.<>u__1;
							this.<>u__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
							this.<>1__state = -1;
							goto IL_480;
						case 8:
							awaiter = this.<>u__1;
							this.<>u__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
							this.<>1__state = -1;
							goto IL_50E;
						case 9:
							awaiter = this.<>u__1;
							this.<>u__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
							this.<>1__state = -1;
							goto IL_584;
						default:
							if (attributeValueCache.singleStringValue == null)
							{
								this.<i>5__2 = attributeValueCache.firstItem;
								goto IL_59D;
							}
							awaiter = this.writer.WriteStringAsync(attributeValueCache.singleStringValue).ConfigureAwait(false).GetAwaiter();
							if (!awaiter.IsCompleted)
							{
								this.<>1__state = 0;
								this.<>u__1 = awaiter;
								this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlWellFormedWriter.AttributeValueCache.<ReplayAsync>d__24>(ref awaiter, ref this);
								return;
							}
							break;
						}
						awaiter.GetResult();
						goto IL_5C9;
						IL_181:
						awaiter.GetResult();
						goto IL_58B;
						IL_1FC:
						awaiter.GetResult();
						goto IL_58B;
						IL_281:
						awaiter.GetResult();
						goto IL_58B;
						IL_2FC:
						awaiter.GetResult();
						goto IL_58B;
						IL_377:
						awaiter.GetResult();
						goto IL_58B;
						IL_405:
						awaiter.GetResult();
						goto IL_58B;
						IL_480:
						awaiter.GetResult();
						goto IL_58B;
						IL_50E:
						awaiter.GetResult();
						goto IL_58B;
						IL_584:
						awaiter.GetResult();
						IL_58B:
						int num2 = this.<i>5__2;
						this.<i>5__2 = num2 + 1;
						IL_59D:
						if (this.<i>5__2 <= attributeValueCache.lastItem)
						{
							XmlWellFormedWriter.AttributeValueCache.Item item = attributeValueCache.items[this.<i>5__2];
							switch (item.type)
							{
							case XmlWellFormedWriter.AttributeValueCache.ItemType.EntityRef:
								awaiter = this.writer.WriteEntityRefAsync((string)item.data).ConfigureAwait(false).GetAwaiter();
								if (!awaiter.IsCompleted)
								{
									this.<>1__state = 1;
									this.<>u__1 = awaiter;
									this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlWellFormedWriter.AttributeValueCache.<ReplayAsync>d__24>(ref awaiter, ref this);
									return;
								}
								goto IL_181;
							case XmlWellFormedWriter.AttributeValueCache.ItemType.CharEntity:
								awaiter = this.writer.WriteCharEntityAsync((char)item.data).ConfigureAwait(false).GetAwaiter();
								if (!awaiter.IsCompleted)
								{
									this.<>1__state = 2;
									this.<>u__1 = awaiter;
									this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlWellFormedWriter.AttributeValueCache.<ReplayAsync>d__24>(ref awaiter, ref this);
									return;
								}
								goto IL_1FC;
							case XmlWellFormedWriter.AttributeValueCache.ItemType.SurrogateCharEntity:
							{
								char[] array = (char[])item.data;
								awaiter = this.writer.WriteSurrogateCharEntityAsync(array[0], array[1]).ConfigureAwait(false).GetAwaiter();
								if (!awaiter.IsCompleted)
								{
									this.<>1__state = 3;
									this.<>u__1 = awaiter;
									this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlWellFormedWriter.AttributeValueCache.<ReplayAsync>d__24>(ref awaiter, ref this);
									return;
								}
								goto IL_281;
							}
							case XmlWellFormedWriter.AttributeValueCache.ItemType.Whitespace:
								awaiter = this.writer.WriteWhitespaceAsync((string)item.data).ConfigureAwait(false).GetAwaiter();
								if (!awaiter.IsCompleted)
								{
									this.<>1__state = 4;
									this.<>u__1 = awaiter;
									this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlWellFormedWriter.AttributeValueCache.<ReplayAsync>d__24>(ref awaiter, ref this);
									return;
								}
								goto IL_2FC;
							case XmlWellFormedWriter.AttributeValueCache.ItemType.String:
								awaiter = this.writer.WriteStringAsync((string)item.data).ConfigureAwait(false).GetAwaiter();
								if (!awaiter.IsCompleted)
								{
									this.<>1__state = 5;
									this.<>u__1 = awaiter;
									this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlWellFormedWriter.AttributeValueCache.<ReplayAsync>d__24>(ref awaiter, ref this);
									return;
								}
								goto IL_377;
							case XmlWellFormedWriter.AttributeValueCache.ItemType.StringChars:
							{
								XmlWellFormedWriter.AttributeValueCache.BufferChunk bufferChunk = (XmlWellFormedWriter.AttributeValueCache.BufferChunk)item.data;
								awaiter = this.writer.WriteCharsAsync(bufferChunk.buffer, bufferChunk.index, bufferChunk.count).ConfigureAwait(false).GetAwaiter();
								if (!awaiter.IsCompleted)
								{
									this.<>1__state = 6;
									this.<>u__1 = awaiter;
									this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlWellFormedWriter.AttributeValueCache.<ReplayAsync>d__24>(ref awaiter, ref this);
									return;
								}
								goto IL_405;
							}
							case XmlWellFormedWriter.AttributeValueCache.ItemType.Raw:
								awaiter = this.writer.WriteRawAsync((string)item.data).ConfigureAwait(false).GetAwaiter();
								if (!awaiter.IsCompleted)
								{
									this.<>1__state = 7;
									this.<>u__1 = awaiter;
									this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlWellFormedWriter.AttributeValueCache.<ReplayAsync>d__24>(ref awaiter, ref this);
									return;
								}
								goto IL_480;
							case XmlWellFormedWriter.AttributeValueCache.ItemType.RawChars:
							{
								XmlWellFormedWriter.AttributeValueCache.BufferChunk bufferChunk = (XmlWellFormedWriter.AttributeValueCache.BufferChunk)item.data;
								awaiter = this.writer.WriteCharsAsync(bufferChunk.buffer, bufferChunk.index, bufferChunk.count).ConfigureAwait(false).GetAwaiter();
								if (!awaiter.IsCompleted)
								{
									this.<>1__state = 8;
									this.<>u__1 = awaiter;
									this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlWellFormedWriter.AttributeValueCache.<ReplayAsync>d__24>(ref awaiter, ref this);
									return;
								}
								goto IL_50E;
							}
							case XmlWellFormedWriter.AttributeValueCache.ItemType.ValueString:
								awaiter = this.writer.WriteStringAsync((string)item.data).ConfigureAwait(false).GetAwaiter();
								if (!awaiter.IsCompleted)
								{
									this.<>1__state = 9;
									this.<>u__1 = awaiter;
									this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlWellFormedWriter.AttributeValueCache.<ReplayAsync>d__24>(ref awaiter, ref this);
									return;
								}
								goto IL_584;
							default:
								goto IL_58B;
							}
						}
					}
					catch (Exception exception)
					{
						this.<>1__state = -2;
						this.<>t__builder.SetException(exception);
						return;
					}
					IL_5C9:
					this.<>1__state = -2;
					this.<>t__builder.SetResult();
				}

				// Token: 0x06000CA4 RID: 3236 RVA: 0x00053F74 File Offset: 0x00052174
				[DebuggerHidden]
				void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
				{
					this.<>t__builder.SetStateMachine(stateMachine);
				}

				// Token: 0x04000DF8 RID: 3576
				public int <>1__state;

				// Token: 0x04000DF9 RID: 3577
				public AsyncTaskMethodBuilder <>t__builder;

				// Token: 0x04000DFA RID: 3578
				public XmlWellFormedWriter.AttributeValueCache <>4__this;

				// Token: 0x04000DFB RID: 3579
				public XmlWriter writer;

				// Token: 0x04000DFC RID: 3580
				private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter <>u__1;

				// Token: 0x04000DFD RID: 3581
				private int <i>5__2;
			}
		}

		// Token: 0x0200015B RID: 347
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <WriteEndDocumentAsync>d__115 : IAsyncStateMachine
		{
			// Token: 0x06000CA5 RID: 3237 RVA: 0x00053F84 File Offset: 0x00052184
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				XmlWellFormedWriter xmlWellFormedWriter = this.<>4__this;
				try
				{
					try
					{
						ConfiguredTaskAwaitable.ConfiguredTaskAwaiter awaiter;
						switch (num)
						{
						case 0:
							awaiter = this.<>u__1;
							this.<>u__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
							this.<>1__state = -1;
							break;
						case 1:
							awaiter = this.<>u__1;
							this.<>u__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
							this.<>1__state = -1;
							goto IL_FE;
						case 2:
							awaiter = this.<>u__1;
							this.<>u__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
							this.<>1__state = -1;
							goto IL_185;
						default:
							goto IL_8B;
						}
						IL_84:
						awaiter.GetResult();
						IL_8B:
						if (xmlWellFormedWriter.elemTop <= 0)
						{
							this.<prevState>5__2 = xmlWellFormedWriter.currentState;
							awaiter = xmlWellFormedWriter.AdvanceStateAsync(XmlWellFormedWriter.Token.EndDocument).ConfigureAwait(false).GetAwaiter();
							if (!awaiter.IsCompleted)
							{
								this.<>1__state = 1;
								this.<>u__1 = awaiter;
								this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlWellFormedWriter.<WriteEndDocumentAsync>d__115>(ref awaiter, ref this);
								return;
							}
						}
						else
						{
							awaiter = xmlWellFormedWriter.WriteEndElementAsync().ConfigureAwait(false).GetAwaiter();
							if (!awaiter.IsCompleted)
							{
								this.<>1__state = 0;
								this.<>u__1 = awaiter;
								this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlWellFormedWriter.<WriteEndDocumentAsync>d__115>(ref awaiter, ref this);
								return;
							}
							goto IL_84;
						}
						IL_FE:
						awaiter.GetResult();
						if (this.<prevState>5__2 != XmlWellFormedWriter.State.AfterRootEle)
						{
							throw new ArgumentException(Res.GetString("Document does not have a root element."));
						}
						if (xmlWellFormedWriter.rawWriter != null)
						{
							goto IL_18C;
						}
						awaiter = xmlWellFormedWriter.writer.WriteEndDocumentAsync().ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 2;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlWellFormedWriter.<WriteEndDocumentAsync>d__115>(ref awaiter, ref this);
							return;
						}
						IL_185:
						awaiter.GetResult();
						IL_18C:;
					}
					catch
					{
						xmlWellFormedWriter.currentState = XmlWellFormedWriter.State.Error;
						throw;
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

			// Token: 0x06000CA6 RID: 3238 RVA: 0x0005418C File Offset: 0x0005238C
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04000DFE RID: 3582
			public int <>1__state;

			// Token: 0x04000DFF RID: 3583
			public AsyncTaskMethodBuilder <>t__builder;

			// Token: 0x04000E00 RID: 3584
			public XmlWellFormedWriter <>4__this;

			// Token: 0x04000E01 RID: 3585
			private XmlWellFormedWriter.State <prevState>5__2;

			// Token: 0x04000E02 RID: 3586
			private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter <>u__1;
		}

		// Token: 0x0200015C RID: 348
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <WriteDocTypeAsync>d__116 : IAsyncStateMachine
		{
			// Token: 0x06000CA7 RID: 3239 RVA: 0x0005419C File Offset: 0x0005239C
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				XmlWellFormedWriter xmlWellFormedWriter = this.<>4__this;
				try
				{
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
								goto IL_257;
							}
							if (this.name == null || this.name.Length == 0)
							{
								throw new ArgumentException(Res.GetString("The empty string '' is not a valid name."));
							}
							XmlConvert.VerifyQName(this.name, ExceptionType.XmlException);
							if (xmlWellFormedWriter.conformanceLevel == ConformanceLevel.Fragment)
							{
								throw new InvalidOperationException(Res.GetString("DTD is not allowed in XML fragments."));
							}
							awaiter = xmlWellFormedWriter.AdvanceStateAsync(XmlWellFormedWriter.Token.Dtd).ConfigureAwait(false).GetAwaiter();
							if (!awaiter.IsCompleted)
							{
								this.<>1__state = 0;
								this.<>u__1 = awaiter;
								this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlWellFormedWriter.<WriteDocTypeAsync>d__116>(ref awaiter, ref this);
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
						if (xmlWellFormedWriter.dtdWritten)
						{
							xmlWellFormedWriter.currentState = XmlWellFormedWriter.State.Error;
							throw new InvalidOperationException(Res.GetString("The DTD has already been written out."));
						}
						if (xmlWellFormedWriter.conformanceLevel == ConformanceLevel.Auto)
						{
							xmlWellFormedWriter.conformanceLevel = ConformanceLevel.Document;
							xmlWellFormedWriter.stateTable = XmlWellFormedWriter.StateTableDocument;
						}
						if (xmlWellFormedWriter.checkCharacters)
						{
							int invCharIndex;
							if (this.pubid != null && (invCharIndex = xmlWellFormedWriter.xmlCharType.IsPublicId(this.pubid)) >= 0)
							{
								string text = "'{0}', hexadecimal value {1}, is an invalid character.";
								object[] args = XmlException.BuildCharExceptionArgs(this.pubid, invCharIndex);
								throw new ArgumentException(Res.GetString(text, args), "pubid");
							}
							if (this.sysid != null && (invCharIndex = xmlWellFormedWriter.xmlCharType.IsOnlyCharData(this.sysid)) >= 0)
							{
								string text2 = "'{0}', hexadecimal value {1}, is an invalid character.";
								object[] args = XmlException.BuildCharExceptionArgs(this.sysid, invCharIndex);
								throw new ArgumentException(Res.GetString(text2, args), "sysid");
							}
							if (this.subset != null && (invCharIndex = xmlWellFormedWriter.xmlCharType.IsOnlyCharData(this.subset)) >= 0)
							{
								string text3 = "'{0}', hexadecimal value {1}, is an invalid character.";
								object[] args = XmlException.BuildCharExceptionArgs(this.subset, invCharIndex);
								throw new ArgumentException(Res.GetString(text3, args), "subset");
							}
						}
						awaiter = xmlWellFormedWriter.writer.WriteDocTypeAsync(this.name, this.pubid, this.sysid, this.subset).ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 1;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlWellFormedWriter.<WriteDocTypeAsync>d__116>(ref awaiter, ref this);
							return;
						}
						IL_257:
						awaiter.GetResult();
						xmlWellFormedWriter.dtdWritten = true;
					}
					catch
					{
						xmlWellFormedWriter.currentState = XmlWellFormedWriter.State.Error;
						throw;
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

			// Token: 0x06000CA8 RID: 3240 RVA: 0x00054480 File Offset: 0x00052680
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04000E03 RID: 3587
			public int <>1__state;

			// Token: 0x04000E04 RID: 3588
			public AsyncTaskMethodBuilder <>t__builder;

			// Token: 0x04000E05 RID: 3589
			public string name;

			// Token: 0x04000E06 RID: 3590
			public XmlWellFormedWriter <>4__this;

			// Token: 0x04000E07 RID: 3591
			public string pubid;

			// Token: 0x04000E08 RID: 3592
			public string sysid;

			// Token: 0x04000E09 RID: 3593
			public string subset;

			// Token: 0x04000E0A RID: 3594
			private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter <>u__1;
		}

		// Token: 0x0200015D RID: 349
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <_TryReturnTask>d__118 : IAsyncStateMachine
		{
			// Token: 0x06000CA9 RID: 3241 RVA: 0x00054490 File Offset: 0x00052690
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				XmlWellFormedWriter xmlWellFormedWriter = this.<>4__this;
				try
				{
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
								this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlWellFormedWriter.<_TryReturnTask>d__118>(ref awaiter, ref this);
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
					}
					catch
					{
						xmlWellFormedWriter.currentState = XmlWellFormedWriter.State.Error;
						throw;
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

			// Token: 0x06000CAA RID: 3242 RVA: 0x0005456C File Offset: 0x0005276C
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04000E0B RID: 3595
			public int <>1__state;

			// Token: 0x04000E0C RID: 3596
			public AsyncTaskMethodBuilder <>t__builder;

			// Token: 0x04000E0D RID: 3597
			public Task task;

			// Token: 0x04000E0E RID: 3598
			public XmlWellFormedWriter <>4__this;

			// Token: 0x04000E0F RID: 3599
			private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter <>u__1;
		}

		// Token: 0x0200015E RID: 350
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <_SequenceRun>d__120 : IAsyncStateMachine
		{
			// Token: 0x06000CAB RID: 3243 RVA: 0x0005457C File Offset: 0x0005277C
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				XmlWellFormedWriter xmlWellFormedWriter = this.<>4__this;
				try
				{
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
								goto IL_E0;
							}
							awaiter = this.task.ConfigureAwait(false).GetAwaiter();
							if (!awaiter.IsCompleted)
							{
								this.<>1__state = 0;
								this.<>u__1 = awaiter;
								this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlWellFormedWriter.<_SequenceRun>d__120>(ref awaiter, ref this);
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
						awaiter = this.nextTaskFun().ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 1;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlWellFormedWriter.<_SequenceRun>d__120>(ref awaiter, ref this);
							return;
						}
						IL_E0:
						awaiter.GetResult();
					}
					catch
					{
						xmlWellFormedWriter.currentState = XmlWellFormedWriter.State.Error;
						throw;
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

			// Token: 0x06000CAC RID: 3244 RVA: 0x000546C8 File Offset: 0x000528C8
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04000E10 RID: 3600
			public int <>1__state;

			// Token: 0x04000E11 RID: 3601
			public AsyncTaskMethodBuilder <>t__builder;

			// Token: 0x04000E12 RID: 3602
			public Task task;

			// Token: 0x04000E13 RID: 3603
			public Func<Task> nextTaskFun;

			// Token: 0x04000E14 RID: 3604
			public XmlWellFormedWriter <>4__this;

			// Token: 0x04000E15 RID: 3605
			private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter <>u__1;
		}

		// Token: 0x0200015F RID: 351
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <WriteStartElementAsync_NoAdvanceState>d__123 : IAsyncStateMachine
		{
			// Token: 0x06000CAD RID: 3245 RVA: 0x000546D8 File Offset: 0x000528D8
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				XmlWellFormedWriter xmlWellFormedWriter = this.<>4__this;
				try
				{
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
								goto IL_ED;
							}
							awaiter = this.task.ConfigureAwait(false).GetAwaiter();
							if (!awaiter.IsCompleted)
							{
								this.<>1__state = 0;
								this.<>u__1 = awaiter;
								this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlWellFormedWriter.<WriteStartElementAsync_NoAdvanceState>d__123>(ref awaiter, ref this);
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
						awaiter = xmlWellFormedWriter.WriteStartElementAsync_NoAdvanceState(this.prefix, this.localName, this.ns).ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 1;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlWellFormedWriter.<WriteStartElementAsync_NoAdvanceState>d__123>(ref awaiter, ref this);
							return;
						}
						IL_ED:
						awaiter.GetResult();
					}
					catch
					{
						xmlWellFormedWriter.currentState = XmlWellFormedWriter.State.Error;
						throw;
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

			// Token: 0x06000CAE RID: 3246 RVA: 0x00054830 File Offset: 0x00052A30
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04000E16 RID: 3606
			public int <>1__state;

			// Token: 0x04000E17 RID: 3607
			public AsyncTaskMethodBuilder <>t__builder;

			// Token: 0x04000E18 RID: 3608
			public Task task;

			// Token: 0x04000E19 RID: 3609
			public XmlWellFormedWriter <>4__this;

			// Token: 0x04000E1A RID: 3610
			public string prefix;

			// Token: 0x04000E1B RID: 3611
			public string localName;

			// Token: 0x04000E1C RID: 3612
			public string ns;

			// Token: 0x04000E1D RID: 3613
			private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter <>u__1;
		}

		// Token: 0x02000160 RID: 352
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <WriteStartElementAsync_FinishWrite>d__125 : IAsyncStateMachine
		{
			// Token: 0x06000CAF RID: 3247 RVA: 0x00054840 File Offset: 0x00052A40
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				XmlWellFormedWriter xmlWellFormedWriter = this.<>4__this;
				try
				{
					try
					{
						ConfiguredTaskAwaitable.ConfiguredTaskAwaiter awaiter;
						if (num != 0)
						{
							awaiter = this.t.ConfigureAwait(false).GetAwaiter();
							if (!awaiter.IsCompleted)
							{
								this.<>1__state = 0;
								this.<>u__1 = awaiter;
								this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlWellFormedWriter.<WriteStartElementAsync_FinishWrite>d__125>(ref awaiter, ref this);
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
						xmlWellFormedWriter.WriteStartElementAsync_FinishWrite(this.prefix, this.localName, this.ns);
					}
					catch
					{
						xmlWellFormedWriter.currentState = XmlWellFormedWriter.State.Error;
						throw;
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

			// Token: 0x06000CB0 RID: 3248 RVA: 0x00054934 File Offset: 0x00052B34
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04000E1E RID: 3614
			public int <>1__state;

			// Token: 0x04000E1F RID: 3615
			public AsyncTaskMethodBuilder <>t__builder;

			// Token: 0x04000E20 RID: 3616
			public Task t;

			// Token: 0x04000E21 RID: 3617
			public XmlWellFormedWriter <>4__this;

			// Token: 0x04000E22 RID: 3618
			public string prefix;

			// Token: 0x04000E23 RID: 3619
			public string localName;

			// Token: 0x04000E24 RID: 3620
			public string ns;

			// Token: 0x04000E25 RID: 3621
			private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter <>u__1;
		}

		// Token: 0x02000161 RID: 353
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <WriteStartAttributeAsync_NoAdvanceState>d__133 : IAsyncStateMachine
		{
			// Token: 0x06000CB1 RID: 3249 RVA: 0x00054944 File Offset: 0x00052B44
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				XmlWellFormedWriter xmlWellFormedWriter = this.<>4__this;
				try
				{
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
								goto IL_ED;
							}
							awaiter = this.task.ConfigureAwait(false).GetAwaiter();
							if (!awaiter.IsCompleted)
							{
								this.<>1__state = 0;
								this.<>u__1 = awaiter;
								this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlWellFormedWriter.<WriteStartAttributeAsync_NoAdvanceState>d__133>(ref awaiter, ref this);
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
						awaiter = xmlWellFormedWriter.WriteStartAttributeAsync_NoAdvanceState(this.prefix, this.localName, this.namespaceName).ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 1;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlWellFormedWriter.<WriteStartAttributeAsync_NoAdvanceState>d__133>(ref awaiter, ref this);
							return;
						}
						IL_ED:
						awaiter.GetResult();
					}
					catch
					{
						xmlWellFormedWriter.currentState = XmlWellFormedWriter.State.Error;
						throw;
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

			// Token: 0x06000CB2 RID: 3250 RVA: 0x00054A9C File Offset: 0x00052C9C
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04000E26 RID: 3622
			public int <>1__state;

			// Token: 0x04000E27 RID: 3623
			public AsyncTaskMethodBuilder <>t__builder;

			// Token: 0x04000E28 RID: 3624
			public Task task;

			// Token: 0x04000E29 RID: 3625
			public XmlWellFormedWriter <>4__this;

			// Token: 0x04000E2A RID: 3626
			public string prefix;

			// Token: 0x04000E2B RID: 3627
			public string localName;

			// Token: 0x04000E2C RID: 3628
			public string namespaceName;

			// Token: 0x04000E2D RID: 3629
			private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter <>u__1;
		}

		// Token: 0x02000162 RID: 354
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <WriteEndAttributeAsync_SepcialAtt>d__136 : IAsyncStateMachine
		{
			// Token: 0x06000CB3 RID: 3251 RVA: 0x00054AAC File Offset: 0x00052CAC
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				XmlWellFormedWriter xmlWellFormedWriter = this.<>4__this;
				try
				{
					try
					{
						ConfiguredTaskAwaitable.ConfiguredTaskAwaiter awaiter;
						switch (num)
						{
						case 0:
							awaiter = this.<>u__1;
							this.<>u__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
							this.<>1__state = -1;
							break;
						case 1:
							awaiter = this.<>u__1;
							this.<>u__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
							this.<>1__state = -1;
							goto IL_1A4;
						case 2:
							awaiter = this.<>u__1;
							this.<>u__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
							this.<>1__state = -1;
							goto IL_211;
						case 3:
							awaiter = this.<>u__1;
							this.<>u__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
							this.<>1__state = -1;
							goto IL_289;
						case 4:
							awaiter = this.<>u__1;
							this.<>u__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
							this.<>1__state = -1;
							goto IL_30A;
						case 5:
							awaiter = this.<>u__1;
							this.<>u__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
							this.<>1__state = -1;
							goto IL_37D;
						case 6:
							awaiter = this.<>u__1;
							this.<>u__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
							this.<>1__state = -1;
							goto IL_3EA;
						case 7:
							awaiter = this.<>u__1;
							this.<>u__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
							this.<>1__state = -1;
							goto IL_4F6;
						case 8:
							awaiter = this.<>u__1;
							this.<>u__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
							this.<>1__state = -1;
							goto IL_569;
						case 9:
							awaiter = this.<>u__1;
							this.<>u__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
							this.<>1__state = -1;
							goto IL_5D7;
						case 10:
							awaiter = this.<>u__1;
							this.<>u__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
							this.<>1__state = -1;
							goto IL_651;
						case 11:
							awaiter = this.<>u__1;
							this.<>u__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
							this.<>1__state = -1;
							goto IL_6D4;
						case 12:
							awaiter = this.<>u__1;
							this.<>u__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
							this.<>1__state = -1;
							goto IL_748;
						case 13:
							awaiter = this.<>u__1;
							this.<>u__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
							this.<>1__state = -1;
							goto IL_7B6;
						case 14:
							awaiter = this.<>u__1;
							this.<>u__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
							this.<>1__state = -1;
							goto IL_8BC;
						case 15:
							awaiter = this.<>u__1;
							this.<>u__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
							this.<>1__state = -1;
							goto IL_930;
						case 16:
							awaiter = this.<>u__1;
							this.<>u__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
							this.<>1__state = -1;
							goto IL_99E;
						case 17:
							awaiter = this.<>u__1;
							this.<>u__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
							this.<>1__state = -1;
							goto IL_A43;
						case 18:
							awaiter = this.<>u__1;
							this.<>u__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
							this.<>1__state = -1;
							goto IL_AB7;
						case 19:
							awaiter = this.<>u__1;
							this.<>u__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
							this.<>1__state = -1;
							goto IL_B22;
						default:
							switch (xmlWellFormedWriter.specAttr)
							{
							case XmlWellFormedWriter.SpecialAttribute.DefaultXmlns:
							{
								string stringValue = xmlWellFormedWriter.attrValueCache.StringValue;
								if (!xmlWellFormedWriter.PushNamespaceExplicit(string.Empty, stringValue))
								{
									goto IL_3F1;
								}
								if (xmlWellFormedWriter.rawWriter != null)
								{
									if (xmlWellFormedWriter.rawWriter.SupportsNamespaceDeclarationInChunks)
									{
										awaiter = xmlWellFormedWriter.rawWriter.WriteStartNamespaceDeclarationAsync(string.Empty).ConfigureAwait(false).GetAwaiter();
										if (!awaiter.IsCompleted)
										{
											this.<>1__state = 0;
											this.<>u__1 = awaiter;
											this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlWellFormedWriter.<WriteEndAttributeAsync_SepcialAtt>d__136>(ref awaiter, ref this);
											return;
										}
									}
									else
									{
										awaiter = xmlWellFormedWriter.rawWriter.WriteNamespaceDeclarationAsync(string.Empty, stringValue).ConfigureAwait(false).GetAwaiter();
										if (!awaiter.IsCompleted)
										{
											this.<>1__state = 3;
											this.<>u__1 = awaiter;
											this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlWellFormedWriter.<WriteEndAttributeAsync_SepcialAtt>d__136>(ref awaiter, ref this);
											return;
										}
										goto IL_289;
									}
								}
								else
								{
									awaiter = xmlWellFormedWriter.writer.WriteStartAttributeAsync(string.Empty, "xmlns", "http://www.w3.org/2000/xmlns/").ConfigureAwait(false).GetAwaiter();
									if (!awaiter.IsCompleted)
									{
										this.<>1__state = 4;
										this.<>u__1 = awaiter;
										this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlWellFormedWriter.<WriteEndAttributeAsync_SepcialAtt>d__136>(ref awaiter, ref this);
										return;
									}
									goto IL_30A;
								}
								break;
							}
							case XmlWellFormedWriter.SpecialAttribute.PrefixedXmlns:
							{
								string stringValue = xmlWellFormedWriter.attrValueCache.StringValue;
								if (stringValue.Length == 0)
								{
									throw new ArgumentException(Res.GetString("Cannot use a prefix with an empty namespace."));
								}
								if (stringValue == "http://www.w3.org/2000/xmlns/" || (stringValue == "http://www.w3.org/XML/1998/namespace" && xmlWellFormedWriter.curDeclPrefix != "xml"))
								{
									throw new ArgumentException(Res.GetString("Cannot bind to the reserved namespace."));
								}
								if (!xmlWellFormedWriter.PushNamespaceExplicit(xmlWellFormedWriter.curDeclPrefix, stringValue))
								{
									goto IL_7BD;
								}
								if (xmlWellFormedWriter.rawWriter != null)
								{
									if (xmlWellFormedWriter.rawWriter.SupportsNamespaceDeclarationInChunks)
									{
										awaiter = xmlWellFormedWriter.rawWriter.WriteStartNamespaceDeclarationAsync(xmlWellFormedWriter.curDeclPrefix).ConfigureAwait(false).GetAwaiter();
										if (!awaiter.IsCompleted)
										{
											this.<>1__state = 7;
											this.<>u__1 = awaiter;
											this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlWellFormedWriter.<WriteEndAttributeAsync_SepcialAtt>d__136>(ref awaiter, ref this);
											return;
										}
										goto IL_4F6;
									}
									else
									{
										awaiter = xmlWellFormedWriter.rawWriter.WriteNamespaceDeclarationAsync(xmlWellFormedWriter.curDeclPrefix, stringValue).ConfigureAwait(false).GetAwaiter();
										if (!awaiter.IsCompleted)
										{
											this.<>1__state = 10;
											this.<>u__1 = awaiter;
											this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlWellFormedWriter.<WriteEndAttributeAsync_SepcialAtt>d__136>(ref awaiter, ref this);
											return;
										}
										goto IL_651;
									}
								}
								else
								{
									awaiter = xmlWellFormedWriter.writer.WriteStartAttributeAsync("xmlns", xmlWellFormedWriter.curDeclPrefix, "http://www.w3.org/2000/xmlns/").ConfigureAwait(false).GetAwaiter();
									if (!awaiter.IsCompleted)
									{
										this.<>1__state = 11;
										this.<>u__1 = awaiter;
										this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlWellFormedWriter.<WriteEndAttributeAsync_SepcialAtt>d__136>(ref awaiter, ref this);
										return;
									}
									goto IL_6D4;
								}
								break;
							}
							case XmlWellFormedWriter.SpecialAttribute.XmlSpace:
							{
								xmlWellFormedWriter.attrValueCache.Trim();
								string stringValue = xmlWellFormedWriter.attrValueCache.StringValue;
								if (stringValue == "default")
								{
									xmlWellFormedWriter.elemScopeStack[xmlWellFormedWriter.elemTop].xmlSpace = XmlSpace.Default;
								}
								else
								{
									if (!(stringValue == "preserve"))
									{
										throw new ArgumentException(Res.GetString("'{0}' is an invalid xml:space value.", new object[]
										{
											stringValue
										}));
									}
									xmlWellFormedWriter.elemScopeStack[xmlWellFormedWriter.elemTop].xmlSpace = XmlSpace.Preserve;
								}
								awaiter = xmlWellFormedWriter.writer.WriteStartAttributeAsync("xml", "space", "http://www.w3.org/XML/1998/namespace").ConfigureAwait(false).GetAwaiter();
								if (!awaiter.IsCompleted)
								{
									this.<>1__state = 14;
									this.<>u__1 = awaiter;
									this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlWellFormedWriter.<WriteEndAttributeAsync_SepcialAtt>d__136>(ref awaiter, ref this);
									return;
								}
								goto IL_8BC;
							}
							case XmlWellFormedWriter.SpecialAttribute.XmlLang:
							{
								string stringValue = xmlWellFormedWriter.attrValueCache.StringValue;
								xmlWellFormedWriter.elemScopeStack[xmlWellFormedWriter.elemTop].xmlLang = stringValue;
								awaiter = xmlWellFormedWriter.writer.WriteStartAttributeAsync("xml", "lang", "http://www.w3.org/XML/1998/namespace").ConfigureAwait(false).GetAwaiter();
								if (!awaiter.IsCompleted)
								{
									this.<>1__state = 17;
									this.<>u__1 = awaiter;
									this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlWellFormedWriter.<WriteEndAttributeAsync_SepcialAtt>d__136>(ref awaiter, ref this);
									return;
								}
								goto IL_A43;
							}
							default:
								goto IL_B29;
							}
							break;
						}
						awaiter.GetResult();
						awaiter = xmlWellFormedWriter.attrValueCache.ReplayAsync(xmlWellFormedWriter.rawWriter).ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 1;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlWellFormedWriter.<WriteEndAttributeAsync_SepcialAtt>d__136>(ref awaiter, ref this);
							return;
						}
						IL_1A4:
						awaiter.GetResult();
						awaiter = xmlWellFormedWriter.rawWriter.WriteEndNamespaceDeclarationAsync().ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 2;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlWellFormedWriter.<WriteEndAttributeAsync_SepcialAtt>d__136>(ref awaiter, ref this);
							return;
						}
						IL_211:
						awaiter.GetResult();
						goto IL_3F1;
						IL_289:
						awaiter.GetResult();
						goto IL_3F1;
						IL_30A:
						awaiter.GetResult();
						awaiter = xmlWellFormedWriter.attrValueCache.ReplayAsync(xmlWellFormedWriter.writer).ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 5;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlWellFormedWriter.<WriteEndAttributeAsync_SepcialAtt>d__136>(ref awaiter, ref this);
							return;
						}
						IL_37D:
						awaiter.GetResult();
						awaiter = xmlWellFormedWriter.writer.WriteEndAttributeAsync().ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 6;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlWellFormedWriter.<WriteEndAttributeAsync_SepcialAtt>d__136>(ref awaiter, ref this);
							return;
						}
						IL_3EA:
						awaiter.GetResult();
						IL_3F1:
						xmlWellFormedWriter.curDeclPrefix = null;
						goto IL_B29;
						IL_4F6:
						awaiter.GetResult();
						awaiter = xmlWellFormedWriter.attrValueCache.ReplayAsync(xmlWellFormedWriter.rawWriter).ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 8;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlWellFormedWriter.<WriteEndAttributeAsync_SepcialAtt>d__136>(ref awaiter, ref this);
							return;
						}
						IL_569:
						awaiter.GetResult();
						awaiter = xmlWellFormedWriter.rawWriter.WriteEndNamespaceDeclarationAsync().ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 9;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlWellFormedWriter.<WriteEndAttributeAsync_SepcialAtt>d__136>(ref awaiter, ref this);
							return;
						}
						IL_5D7:
						awaiter.GetResult();
						goto IL_7BD;
						IL_651:
						awaiter.GetResult();
						goto IL_7BD;
						IL_6D4:
						awaiter.GetResult();
						awaiter = xmlWellFormedWriter.attrValueCache.ReplayAsync(xmlWellFormedWriter.writer).ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 12;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlWellFormedWriter.<WriteEndAttributeAsync_SepcialAtt>d__136>(ref awaiter, ref this);
							return;
						}
						IL_748:
						awaiter.GetResult();
						awaiter = xmlWellFormedWriter.writer.WriteEndAttributeAsync().ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 13;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlWellFormedWriter.<WriteEndAttributeAsync_SepcialAtt>d__136>(ref awaiter, ref this);
							return;
						}
						IL_7B6:
						awaiter.GetResult();
						IL_7BD:
						xmlWellFormedWriter.curDeclPrefix = null;
						goto IL_B29;
						IL_8BC:
						awaiter.GetResult();
						awaiter = xmlWellFormedWriter.attrValueCache.ReplayAsync(xmlWellFormedWriter.writer).ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 15;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlWellFormedWriter.<WriteEndAttributeAsync_SepcialAtt>d__136>(ref awaiter, ref this);
							return;
						}
						IL_930:
						awaiter.GetResult();
						awaiter = xmlWellFormedWriter.writer.WriteEndAttributeAsync().ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 16;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlWellFormedWriter.<WriteEndAttributeAsync_SepcialAtt>d__136>(ref awaiter, ref this);
							return;
						}
						IL_99E:
						awaiter.GetResult();
						goto IL_B29;
						IL_A43:
						awaiter.GetResult();
						awaiter = xmlWellFormedWriter.attrValueCache.ReplayAsync(xmlWellFormedWriter.writer).ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 18;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlWellFormedWriter.<WriteEndAttributeAsync_SepcialAtt>d__136>(ref awaiter, ref this);
							return;
						}
						IL_AB7:
						awaiter.GetResult();
						awaiter = xmlWellFormedWriter.writer.WriteEndAttributeAsync().ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 19;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlWellFormedWriter.<WriteEndAttributeAsync_SepcialAtt>d__136>(ref awaiter, ref this);
							return;
						}
						IL_B22:
						awaiter.GetResult();
						IL_B29:
						xmlWellFormedWriter.specAttr = XmlWellFormedWriter.SpecialAttribute.No;
						xmlWellFormedWriter.attrValueCache.Clear();
					}
					catch
					{
						xmlWellFormedWriter.currentState = XmlWellFormedWriter.State.Error;
						throw;
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

			// Token: 0x06000CB4 RID: 3252 RVA: 0x00055664 File Offset: 0x00053864
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04000E2E RID: 3630
			public int <>1__state;

			// Token: 0x04000E2F RID: 3631
			public AsyncTaskMethodBuilder <>t__builder;

			// Token: 0x04000E30 RID: 3632
			public XmlWellFormedWriter <>4__this;

			// Token: 0x04000E31 RID: 3633
			private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter <>u__1;
		}

		// Token: 0x02000163 RID: 355
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <WriteCDataAsync>d__137 : IAsyncStateMachine
		{
			// Token: 0x06000CB5 RID: 3253 RVA: 0x00055674 File Offset: 0x00053874
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				XmlWellFormedWriter xmlWellFormedWriter = this.<>4__this;
				try
				{
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
								goto IL_FB;
							}
							if (this.text == null)
							{
								this.text = string.Empty;
							}
							awaiter = xmlWellFormedWriter.AdvanceStateAsync(XmlWellFormedWriter.Token.CData).ConfigureAwait(false).GetAwaiter();
							if (!awaiter.IsCompleted)
							{
								this.<>1__state = 0;
								this.<>u__1 = awaiter;
								this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlWellFormedWriter.<WriteCDataAsync>d__137>(ref awaiter, ref this);
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
						awaiter = xmlWellFormedWriter.writer.WriteCDataAsync(this.text).ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 1;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlWellFormedWriter.<WriteCDataAsync>d__137>(ref awaiter, ref this);
							return;
						}
						IL_FB:
						awaiter.GetResult();
					}
					catch
					{
						xmlWellFormedWriter.currentState = XmlWellFormedWriter.State.Error;
						throw;
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

			// Token: 0x06000CB6 RID: 3254 RVA: 0x000557F4 File Offset: 0x000539F4
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04000E32 RID: 3634
			public int <>1__state;

			// Token: 0x04000E33 RID: 3635
			public AsyncTaskMethodBuilder <>t__builder;

			// Token: 0x04000E34 RID: 3636
			public string text;

			// Token: 0x04000E35 RID: 3637
			public XmlWellFormedWriter <>4__this;

			// Token: 0x04000E36 RID: 3638
			private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter <>u__1;
		}

		// Token: 0x02000164 RID: 356
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <WriteCommentAsync>d__138 : IAsyncStateMachine
		{
			// Token: 0x06000CB7 RID: 3255 RVA: 0x00055804 File Offset: 0x00053A04
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				XmlWellFormedWriter xmlWellFormedWriter = this.<>4__this;
				try
				{
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
								goto IL_FA;
							}
							if (this.text == null)
							{
								this.text = string.Empty;
							}
							awaiter = xmlWellFormedWriter.AdvanceStateAsync(XmlWellFormedWriter.Token.Comment).ConfigureAwait(false).GetAwaiter();
							if (!awaiter.IsCompleted)
							{
								this.<>1__state = 0;
								this.<>u__1 = awaiter;
								this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlWellFormedWriter.<WriteCommentAsync>d__138>(ref awaiter, ref this);
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
						awaiter = xmlWellFormedWriter.writer.WriteCommentAsync(this.text).ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 1;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlWellFormedWriter.<WriteCommentAsync>d__138>(ref awaiter, ref this);
							return;
						}
						IL_FA:
						awaiter.GetResult();
					}
					catch
					{
						xmlWellFormedWriter.currentState = XmlWellFormedWriter.State.Error;
						throw;
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

			// Token: 0x06000CB8 RID: 3256 RVA: 0x00055984 File Offset: 0x00053B84
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04000E37 RID: 3639
			public int <>1__state;

			// Token: 0x04000E38 RID: 3640
			public AsyncTaskMethodBuilder <>t__builder;

			// Token: 0x04000E39 RID: 3641
			public string text;

			// Token: 0x04000E3A RID: 3642
			public XmlWellFormedWriter <>4__this;

			// Token: 0x04000E3B RID: 3643
			private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter <>u__1;
		}

		// Token: 0x02000165 RID: 357
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <WriteProcessingInstructionAsync>d__139 : IAsyncStateMachine
		{
			// Token: 0x06000CB9 RID: 3257 RVA: 0x00055994 File Offset: 0x00053B94
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				XmlWellFormedWriter xmlWellFormedWriter = this.<>4__this;
				try
				{
					try
					{
						ConfiguredTaskAwaitable.ConfiguredTaskAwaiter awaiter;
						switch (num)
						{
						case 0:
							awaiter = this.<>u__1;
							this.<>u__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
							this.<>1__state = -1;
							break;
						case 1:
							awaiter = this.<>u__1;
							this.<>u__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
							this.<>1__state = -1;
							goto IL_19C;
						case 2:
							awaiter = this.<>u__1;
							this.<>u__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
							this.<>1__state = -1;
							goto IL_216;
						case 3:
							awaiter = this.<>u__1;
							this.<>u__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
							this.<>1__state = -1;
							goto IL_280;
						case 4:
							awaiter = this.<>u__1;
							this.<>u__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
							this.<>1__state = -1;
							goto IL_2F2;
						default:
							if (this.name == null || this.name.Length == 0)
							{
								throw new ArgumentException(Res.GetString("The empty string '' is not a valid name."));
							}
							xmlWellFormedWriter.CheckNCName(this.name);
							if (this.text == null)
							{
								this.text = string.Empty;
							}
							if (this.name.Length == 3 && string.Compare(this.name, "xml", StringComparison.OrdinalIgnoreCase) == 0)
							{
								if (xmlWellFormedWriter.currentState != XmlWellFormedWriter.State.Start)
								{
									throw new ArgumentException(Res.GetString((xmlWellFormedWriter.conformanceLevel == ConformanceLevel.Document) ? "Cannot write XML declaration. WriteStartDocument method has already written it." : "Cannot write XML declaration. XML declaration can be only at the beginning of the document."));
								}
								xmlWellFormedWriter.xmlDeclFollows = true;
								awaiter = xmlWellFormedWriter.AdvanceStateAsync(XmlWellFormedWriter.Token.PI).ConfigureAwait(false).GetAwaiter();
								if (!awaiter.IsCompleted)
								{
									this.<>1__state = 0;
									this.<>u__1 = awaiter;
									this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlWellFormedWriter.<WriteProcessingInstructionAsync>d__139>(ref awaiter, ref this);
									return;
								}
							}
							else
							{
								awaiter = xmlWellFormedWriter.AdvanceStateAsync(XmlWellFormedWriter.Token.PI).ConfigureAwait(false).GetAwaiter();
								if (!awaiter.IsCompleted)
								{
									this.<>1__state = 3;
									this.<>u__1 = awaiter;
									this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlWellFormedWriter.<WriteProcessingInstructionAsync>d__139>(ref awaiter, ref this);
									return;
								}
								goto IL_280;
							}
							break;
						}
						awaiter.GetResult();
						if (xmlWellFormedWriter.rawWriter != null)
						{
							awaiter = xmlWellFormedWriter.rawWriter.WriteXmlDeclarationAsync(this.text).ConfigureAwait(false).GetAwaiter();
							if (!awaiter.IsCompleted)
							{
								this.<>1__state = 1;
								this.<>u__1 = awaiter;
								this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlWellFormedWriter.<WriteProcessingInstructionAsync>d__139>(ref awaiter, ref this);
								return;
							}
						}
						else
						{
							awaiter = xmlWellFormedWriter.writer.WriteProcessingInstructionAsync(this.name, this.text).ConfigureAwait(false).GetAwaiter();
							if (!awaiter.IsCompleted)
							{
								this.<>1__state = 2;
								this.<>u__1 = awaiter;
								this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlWellFormedWriter.<WriteProcessingInstructionAsync>d__139>(ref awaiter, ref this);
								return;
							}
							goto IL_216;
						}
						IL_19C:
						awaiter.GetResult();
						goto IL_2F9;
						IL_216:
						awaiter.GetResult();
						goto IL_2F9;
						IL_280:
						awaiter.GetResult();
						awaiter = xmlWellFormedWriter.writer.WriteProcessingInstructionAsync(this.name, this.text).ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 4;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlWellFormedWriter.<WriteProcessingInstructionAsync>d__139>(ref awaiter, ref this);
							return;
						}
						IL_2F2:
						awaiter.GetResult();
						IL_2F9:;
					}
					catch
					{
						xmlWellFormedWriter.currentState = XmlWellFormedWriter.State.Error;
						throw;
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

			// Token: 0x06000CBA RID: 3258 RVA: 0x00055D0C File Offset: 0x00053F0C
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04000E3C RID: 3644
			public int <>1__state;

			// Token: 0x04000E3D RID: 3645
			public AsyncTaskMethodBuilder <>t__builder;

			// Token: 0x04000E3E RID: 3646
			public string name;

			// Token: 0x04000E3F RID: 3647
			public XmlWellFormedWriter <>4__this;

			// Token: 0x04000E40 RID: 3648
			public string text;

			// Token: 0x04000E41 RID: 3649
			private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter <>u__1;
		}

		// Token: 0x02000166 RID: 358
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <WriteEntityRefAsync>d__140 : IAsyncStateMachine
		{
			// Token: 0x06000CBB RID: 3259 RVA: 0x00055D1C File Offset: 0x00053F1C
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				XmlWellFormedWriter xmlWellFormedWriter = this.<>4__this;
				try
				{
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
								goto IL_134;
							}
							if (this.name == null || this.name.Length == 0)
							{
								throw new ArgumentException(Res.GetString("The empty string '' is not a valid name."));
							}
							xmlWellFormedWriter.CheckNCName(this.name);
							awaiter = xmlWellFormedWriter.AdvanceStateAsync(XmlWellFormedWriter.Token.Text).ConfigureAwait(false).GetAwaiter();
							if (!awaiter.IsCompleted)
							{
								this.<>1__state = 0;
								this.<>u__1 = awaiter;
								this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlWellFormedWriter.<WriteEntityRefAsync>d__140>(ref awaiter, ref this);
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
						if (xmlWellFormedWriter.SaveAttrValue)
						{
							xmlWellFormedWriter.attrValueCache.WriteEntityRef(this.name);
							goto IL_13B;
						}
						awaiter = xmlWellFormedWriter.writer.WriteEntityRefAsync(this.name).ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 1;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlWellFormedWriter.<WriteEntityRefAsync>d__140>(ref awaiter, ref this);
							return;
						}
						IL_134:
						awaiter.GetResult();
						IL_13B:;
					}
					catch
					{
						xmlWellFormedWriter.currentState = XmlWellFormedWriter.State.Error;
						throw;
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

			// Token: 0x06000CBC RID: 3260 RVA: 0x00055ED4 File Offset: 0x000540D4
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04000E42 RID: 3650
			public int <>1__state;

			// Token: 0x04000E43 RID: 3651
			public AsyncTaskMethodBuilder <>t__builder;

			// Token: 0x04000E44 RID: 3652
			public string name;

			// Token: 0x04000E45 RID: 3653
			public XmlWellFormedWriter <>4__this;

			// Token: 0x04000E46 RID: 3654
			private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter <>u__1;
		}

		// Token: 0x02000167 RID: 359
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <WriteCharEntityAsync>d__141 : IAsyncStateMachine
		{
			// Token: 0x06000CBD RID: 3261 RVA: 0x00055EE4 File Offset: 0x000540E4
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				XmlWellFormedWriter xmlWellFormedWriter = this.<>4__this;
				try
				{
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
								goto IL_120;
							}
							if (char.IsSurrogate(this.ch))
							{
								throw new ArgumentException(Res.GetString("The surrogate pair is invalid. Missing a low surrogate character."));
							}
							awaiter = xmlWellFormedWriter.AdvanceStateAsync(XmlWellFormedWriter.Token.Text).ConfigureAwait(false).GetAwaiter();
							if (!awaiter.IsCompleted)
							{
								this.<>1__state = 0;
								this.<>u__1 = awaiter;
								this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlWellFormedWriter.<WriteCharEntityAsync>d__141>(ref awaiter, ref this);
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
						if (xmlWellFormedWriter.SaveAttrValue)
						{
							xmlWellFormedWriter.attrValueCache.WriteCharEntity(this.ch);
							goto IL_127;
						}
						awaiter = xmlWellFormedWriter.writer.WriteCharEntityAsync(this.ch).ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 1;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlWellFormedWriter.<WriteCharEntityAsync>d__141>(ref awaiter, ref this);
							return;
						}
						IL_120:
						awaiter.GetResult();
						IL_127:;
					}
					catch
					{
						xmlWellFormedWriter.currentState = XmlWellFormedWriter.State.Error;
						throw;
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

			// Token: 0x06000CBE RID: 3262 RVA: 0x00056088 File Offset: 0x00054288
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04000E47 RID: 3655
			public int <>1__state;

			// Token: 0x04000E48 RID: 3656
			public AsyncTaskMethodBuilder <>t__builder;

			// Token: 0x04000E49 RID: 3657
			public char ch;

			// Token: 0x04000E4A RID: 3658
			public XmlWellFormedWriter <>4__this;

			// Token: 0x04000E4B RID: 3659
			private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter <>u__1;
		}

		// Token: 0x02000168 RID: 360
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <WriteSurrogateCharEntityAsync>d__142 : IAsyncStateMachine
		{
			// Token: 0x06000CBF RID: 3263 RVA: 0x00056098 File Offset: 0x00054298
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				XmlWellFormedWriter xmlWellFormedWriter = this.<>4__this;
				try
				{
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
								goto IL_134;
							}
							if (!char.IsSurrogatePair(this.highChar, this.lowChar))
							{
								throw XmlConvert.CreateInvalidSurrogatePairException(this.lowChar, this.highChar);
							}
							awaiter = xmlWellFormedWriter.AdvanceStateAsync(XmlWellFormedWriter.Token.Text).ConfigureAwait(false).GetAwaiter();
							if (!awaiter.IsCompleted)
							{
								this.<>1__state = 0;
								this.<>u__1 = awaiter;
								this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlWellFormedWriter.<WriteSurrogateCharEntityAsync>d__142>(ref awaiter, ref this);
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
						if (xmlWellFormedWriter.SaveAttrValue)
						{
							xmlWellFormedWriter.attrValueCache.WriteSurrogateCharEntity(this.lowChar, this.highChar);
							goto IL_13B;
						}
						awaiter = xmlWellFormedWriter.writer.WriteSurrogateCharEntityAsync(this.lowChar, this.highChar).ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 1;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlWellFormedWriter.<WriteSurrogateCharEntityAsync>d__142>(ref awaiter, ref this);
							return;
						}
						IL_134:
						awaiter.GetResult();
						IL_13B:;
					}
					catch
					{
						xmlWellFormedWriter.currentState = XmlWellFormedWriter.State.Error;
						throw;
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

			// Token: 0x06000CC0 RID: 3264 RVA: 0x00056250 File Offset: 0x00054450
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04000E4C RID: 3660
			public int <>1__state;

			// Token: 0x04000E4D RID: 3661
			public AsyncTaskMethodBuilder <>t__builder;

			// Token: 0x04000E4E RID: 3662
			public char highChar;

			// Token: 0x04000E4F RID: 3663
			public char lowChar;

			// Token: 0x04000E50 RID: 3664
			public XmlWellFormedWriter <>4__this;

			// Token: 0x04000E51 RID: 3665
			private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter <>u__1;
		}

		// Token: 0x02000169 RID: 361
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <WriteWhitespaceAsync>d__143 : IAsyncStateMachine
		{
			// Token: 0x06000CC1 RID: 3265 RVA: 0x00056260 File Offset: 0x00054460
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				XmlWellFormedWriter xmlWellFormedWriter = this.<>4__this;
				try
				{
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
								goto IL_140;
							}
							if (this.ws == null)
							{
								this.ws = string.Empty;
							}
							if (!XmlCharType.Instance.IsOnlyWhitespace(this.ws))
							{
								throw new ArgumentException(Res.GetString("Only white space characters should be used."));
							}
							awaiter = xmlWellFormedWriter.AdvanceStateAsync(XmlWellFormedWriter.Token.Whitespace).ConfigureAwait(false).GetAwaiter();
							if (!awaiter.IsCompleted)
							{
								this.<>1__state = 0;
								this.<>u__1 = awaiter;
								this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlWellFormedWriter.<WriteWhitespaceAsync>d__143>(ref awaiter, ref this);
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
						if (xmlWellFormedWriter.SaveAttrValue)
						{
							xmlWellFormedWriter.attrValueCache.WriteWhitespace(this.ws);
							goto IL_147;
						}
						awaiter = xmlWellFormedWriter.writer.WriteWhitespaceAsync(this.ws).ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 1;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlWellFormedWriter.<WriteWhitespaceAsync>d__143>(ref awaiter, ref this);
							return;
						}
						IL_140:
						awaiter.GetResult();
						IL_147:;
					}
					catch
					{
						xmlWellFormedWriter.currentState = XmlWellFormedWriter.State.Error;
						throw;
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

			// Token: 0x06000CC2 RID: 3266 RVA: 0x00056424 File Offset: 0x00054624
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04000E52 RID: 3666
			public int <>1__state;

			// Token: 0x04000E53 RID: 3667
			public AsyncTaskMethodBuilder <>t__builder;

			// Token: 0x04000E54 RID: 3668
			public string ws;

			// Token: 0x04000E55 RID: 3669
			public XmlWellFormedWriter <>4__this;

			// Token: 0x04000E56 RID: 3670
			private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter <>u__1;
		}

		// Token: 0x0200016A RID: 362
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <WriteStringAsync_NoAdvanceState>d__146 : IAsyncStateMachine
		{
			// Token: 0x06000CC3 RID: 3267 RVA: 0x00056434 File Offset: 0x00054634
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				XmlWellFormedWriter xmlWellFormedWriter = this.<>4__this;
				try
				{
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
								goto IL_E1;
							}
							awaiter = this.task.ConfigureAwait(false).GetAwaiter();
							if (!awaiter.IsCompleted)
							{
								this.<>1__state = 0;
								this.<>u__1 = awaiter;
								this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlWellFormedWriter.<WriteStringAsync_NoAdvanceState>d__146>(ref awaiter, ref this);
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
						awaiter = xmlWellFormedWriter.WriteStringAsync_NoAdvanceState(this.text).ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 1;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlWellFormedWriter.<WriteStringAsync_NoAdvanceState>d__146>(ref awaiter, ref this);
							return;
						}
						IL_E1:
						awaiter.GetResult();
					}
					catch
					{
						xmlWellFormedWriter.currentState = XmlWellFormedWriter.State.Error;
						throw;
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

			// Token: 0x06000CC4 RID: 3268 RVA: 0x00056580 File Offset: 0x00054780
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04000E57 RID: 3671
			public int <>1__state;

			// Token: 0x04000E58 RID: 3672
			public AsyncTaskMethodBuilder <>t__builder;

			// Token: 0x04000E59 RID: 3673
			public Task task;

			// Token: 0x04000E5A RID: 3674
			public XmlWellFormedWriter <>4__this;

			// Token: 0x04000E5B RID: 3675
			public string text;

			// Token: 0x04000E5C RID: 3676
			private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter <>u__1;
		}

		// Token: 0x0200016B RID: 363
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <WriteCharsAsync>d__147 : IAsyncStateMachine
		{
			// Token: 0x06000CC5 RID: 3269 RVA: 0x00056590 File Offset: 0x00054790
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				XmlWellFormedWriter xmlWellFormedWriter = this.<>4__this;
				try
				{
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
								goto IL_17B;
							}
							if (this.buffer == null)
							{
								throw new ArgumentNullException("buffer");
							}
							if (this.index < 0)
							{
								throw new ArgumentOutOfRangeException("index");
							}
							if (this.count < 0)
							{
								throw new ArgumentOutOfRangeException("count");
							}
							if (this.count > this.buffer.Length - this.index)
							{
								throw new ArgumentOutOfRangeException("count");
							}
							awaiter = xmlWellFormedWriter.AdvanceStateAsync(XmlWellFormedWriter.Token.Text).ConfigureAwait(false).GetAwaiter();
							if (!awaiter.IsCompleted)
							{
								this.<>1__state = 0;
								this.<>u__1 = awaiter;
								this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlWellFormedWriter.<WriteCharsAsync>d__147>(ref awaiter, ref this);
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
						if (xmlWellFormedWriter.SaveAttrValue)
						{
							xmlWellFormedWriter.attrValueCache.WriteChars(this.buffer, this.index, this.count);
							goto IL_182;
						}
						awaiter = xmlWellFormedWriter.writer.WriteCharsAsync(this.buffer, this.index, this.count).ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 1;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlWellFormedWriter.<WriteCharsAsync>d__147>(ref awaiter, ref this);
							return;
						}
						IL_17B:
						awaiter.GetResult();
						IL_182:;
					}
					catch
					{
						xmlWellFormedWriter.currentState = XmlWellFormedWriter.State.Error;
						throw;
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

			// Token: 0x06000CC6 RID: 3270 RVA: 0x00056790 File Offset: 0x00054990
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04000E5D RID: 3677
			public int <>1__state;

			// Token: 0x04000E5E RID: 3678
			public AsyncTaskMethodBuilder <>t__builder;

			// Token: 0x04000E5F RID: 3679
			public char[] buffer;

			// Token: 0x04000E60 RID: 3680
			public int index;

			// Token: 0x04000E61 RID: 3681
			public int count;

			// Token: 0x04000E62 RID: 3682
			public XmlWellFormedWriter <>4__this;

			// Token: 0x04000E63 RID: 3683
			private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter <>u__1;
		}

		// Token: 0x0200016C RID: 364
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <WriteRawAsync>d__148 : IAsyncStateMachine
		{
			// Token: 0x06000CC7 RID: 3271 RVA: 0x000567A0 File Offset: 0x000549A0
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				XmlWellFormedWriter xmlWellFormedWriter = this.<>4__this;
				try
				{
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
								goto IL_17B;
							}
							if (this.buffer == null)
							{
								throw new ArgumentNullException("buffer");
							}
							if (this.index < 0)
							{
								throw new ArgumentOutOfRangeException("index");
							}
							if (this.count < 0)
							{
								throw new ArgumentOutOfRangeException("count");
							}
							if (this.count > this.buffer.Length - this.index)
							{
								throw new ArgumentOutOfRangeException("count");
							}
							awaiter = xmlWellFormedWriter.AdvanceStateAsync(XmlWellFormedWriter.Token.RawData).ConfigureAwait(false).GetAwaiter();
							if (!awaiter.IsCompleted)
							{
								this.<>1__state = 0;
								this.<>u__1 = awaiter;
								this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlWellFormedWriter.<WriteRawAsync>d__148>(ref awaiter, ref this);
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
						if (xmlWellFormedWriter.SaveAttrValue)
						{
							xmlWellFormedWriter.attrValueCache.WriteRaw(this.buffer, this.index, this.count);
							goto IL_182;
						}
						awaiter = xmlWellFormedWriter.writer.WriteRawAsync(this.buffer, this.index, this.count).ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 1;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlWellFormedWriter.<WriteRawAsync>d__148>(ref awaiter, ref this);
							return;
						}
						IL_17B:
						awaiter.GetResult();
						IL_182:;
					}
					catch
					{
						xmlWellFormedWriter.currentState = XmlWellFormedWriter.State.Error;
						throw;
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

			// Token: 0x06000CC8 RID: 3272 RVA: 0x000569A0 File Offset: 0x00054BA0
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04000E64 RID: 3684
			public int <>1__state;

			// Token: 0x04000E65 RID: 3685
			public AsyncTaskMethodBuilder <>t__builder;

			// Token: 0x04000E66 RID: 3686
			public char[] buffer;

			// Token: 0x04000E67 RID: 3687
			public int index;

			// Token: 0x04000E68 RID: 3688
			public int count;

			// Token: 0x04000E69 RID: 3689
			public XmlWellFormedWriter <>4__this;

			// Token: 0x04000E6A RID: 3690
			private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter <>u__1;
		}

		// Token: 0x0200016D RID: 365
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <WriteRawAsync>d__149 : IAsyncStateMachine
		{
			// Token: 0x06000CC9 RID: 3273 RVA: 0x000569B0 File Offset: 0x00054BB0
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				XmlWellFormedWriter xmlWellFormedWriter = this.<>4__this;
				try
				{
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
								goto IL_110;
							}
							if (this.data == null)
							{
								goto IL_13F;
							}
							awaiter = xmlWellFormedWriter.AdvanceStateAsync(XmlWellFormedWriter.Token.RawData).ConfigureAwait(false).GetAwaiter();
							if (!awaiter.IsCompleted)
							{
								this.<>1__state = 0;
								this.<>u__1 = awaiter;
								this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlWellFormedWriter.<WriteRawAsync>d__149>(ref awaiter, ref this);
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
						if (xmlWellFormedWriter.SaveAttrValue)
						{
							xmlWellFormedWriter.attrValueCache.WriteRaw(this.data);
							goto IL_117;
						}
						awaiter = xmlWellFormedWriter.writer.WriteRawAsync(this.data).ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 1;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlWellFormedWriter.<WriteRawAsync>d__149>(ref awaiter, ref this);
							return;
						}
						IL_110:
						awaiter.GetResult();
						IL_117:;
					}
					catch
					{
						xmlWellFormedWriter.currentState = XmlWellFormedWriter.State.Error;
						throw;
					}
				}
				catch (Exception exception)
				{
					this.<>1__state = -2;
					this.<>t__builder.SetException(exception);
					return;
				}
				IL_13F:
				this.<>1__state = -2;
				this.<>t__builder.SetResult();
			}

			// Token: 0x06000CCA RID: 3274 RVA: 0x00056B44 File Offset: 0x00054D44
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04000E6B RID: 3691
			public int <>1__state;

			// Token: 0x04000E6C RID: 3692
			public AsyncTaskMethodBuilder <>t__builder;

			// Token: 0x04000E6D RID: 3693
			public string data;

			// Token: 0x04000E6E RID: 3694
			public XmlWellFormedWriter <>4__this;

			// Token: 0x04000E6F RID: 3695
			private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter <>u__1;
		}

		// Token: 0x0200016E RID: 366
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <WriteBase64Async_NoAdvanceState>d__151 : IAsyncStateMachine
		{
			// Token: 0x06000CCB RID: 3275 RVA: 0x00056B54 File Offset: 0x00054D54
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				XmlWellFormedWriter xmlWellFormedWriter = this.<>4__this;
				try
				{
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
								goto IL_F2;
							}
							awaiter = this.task.ConfigureAwait(false).GetAwaiter();
							if (!awaiter.IsCompleted)
							{
								this.<>1__state = 0;
								this.<>u__1 = awaiter;
								this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlWellFormedWriter.<WriteBase64Async_NoAdvanceState>d__151>(ref awaiter, ref this);
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
						awaiter = xmlWellFormedWriter.writer.WriteBase64Async(this.buffer, this.index, this.count).ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 1;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlWellFormedWriter.<WriteBase64Async_NoAdvanceState>d__151>(ref awaiter, ref this);
							return;
						}
						IL_F2:
						awaiter.GetResult();
					}
					catch
					{
						xmlWellFormedWriter.currentState = XmlWellFormedWriter.State.Error;
						throw;
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

			// Token: 0x06000CCC RID: 3276 RVA: 0x00056CB4 File Offset: 0x00054EB4
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04000E70 RID: 3696
			public int <>1__state;

			// Token: 0x04000E71 RID: 3697
			public AsyncTaskMethodBuilder <>t__builder;

			// Token: 0x04000E72 RID: 3698
			public Task task;

			// Token: 0x04000E73 RID: 3699
			public XmlWellFormedWriter <>4__this;

			// Token: 0x04000E74 RID: 3700
			public byte[] buffer;

			// Token: 0x04000E75 RID: 3701
			public int index;

			// Token: 0x04000E76 RID: 3702
			public int count;

			// Token: 0x04000E77 RID: 3703
			private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter <>u__1;
		}

		// Token: 0x0200016F RID: 367
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <FlushAsync>d__152 : IAsyncStateMachine
		{
			// Token: 0x06000CCD RID: 3277 RVA: 0x00056CC4 File Offset: 0x00054EC4
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				XmlWellFormedWriter xmlWellFormedWriter = this.<>4__this;
				try
				{
					try
					{
						ConfiguredTaskAwaitable.ConfiguredTaskAwaiter awaiter;
						if (num != 0)
						{
							awaiter = xmlWellFormedWriter.writer.FlushAsync().ConfigureAwait(false).GetAwaiter();
							if (!awaiter.IsCompleted)
							{
								this.<>1__state = 0;
								this.<>u__1 = awaiter;
								this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlWellFormedWriter.<FlushAsync>d__152>(ref awaiter, ref this);
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
					}
					catch
					{
						xmlWellFormedWriter.currentState = XmlWellFormedWriter.State.Error;
						throw;
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

			// Token: 0x06000CCE RID: 3278 RVA: 0x00056DA4 File Offset: 0x00054FA4
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04000E78 RID: 3704
			public int <>1__state;

			// Token: 0x04000E79 RID: 3705
			public AsyncTaskMethodBuilder <>t__builder;

			// Token: 0x04000E7A RID: 3706
			public XmlWellFormedWriter <>4__this;

			// Token: 0x04000E7B RID: 3707
			private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter <>u__1;
		}

		// Token: 0x02000170 RID: 368
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <WriteQualifiedNameAsync>d__153 : IAsyncStateMachine
		{
			// Token: 0x06000CCF RID: 3279 RVA: 0x00056DB4 File Offset: 0x00054FB4
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				XmlWellFormedWriter xmlWellFormedWriter = this.<>4__this;
				try
				{
					try
					{
						ConfiguredTaskAwaitable.ConfiguredTaskAwaiter awaiter;
						switch (num)
						{
						case 0:
							awaiter = this.<>u__1;
							this.<>u__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
							this.<>1__state = -1;
							break;
						case 1:
							awaiter = this.<>u__1;
							this.<>u__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
							this.<>1__state = -1;
							goto IL_1A9;
						case 2:
							awaiter = this.<>u__1;
							this.<>u__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
							this.<>1__state = -1;
							goto IL_213;
						case 3:
							awaiter = this.<>u__1;
							this.<>u__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
							this.<>1__state = -1;
							goto IL_27E;
						case 4:
							awaiter = this.<>u__1;
							this.<>u__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
							this.<>1__state = -1;
							goto IL_2F4;
						default:
							if (this.localName == null || this.localName.Length == 0)
							{
								throw new ArgumentException(Res.GetString("The empty string '' is not a valid local name."));
							}
							xmlWellFormedWriter.CheckNCName(this.localName);
							awaiter = xmlWellFormedWriter.AdvanceStateAsync(XmlWellFormedWriter.Token.Text).ConfigureAwait(false).GetAwaiter();
							if (!awaiter.IsCompleted)
							{
								this.<>1__state = 0;
								this.<>u__1 = awaiter;
								this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlWellFormedWriter.<WriteQualifiedNameAsync>d__153>(ref awaiter, ref this);
								return;
							}
							break;
						}
						awaiter.GetResult();
						string text = string.Empty;
						if (this.ns != null && this.ns.Length != 0)
						{
							text = xmlWellFormedWriter.LookupPrefix(this.ns);
							if (text == null)
							{
								if (xmlWellFormedWriter.currentState != XmlWellFormedWriter.State.Attribute)
								{
									throw new ArgumentException(Res.GetString("The '{0}' namespace is not defined.", new object[]
									{
										this.ns
									}));
								}
								text = xmlWellFormedWriter.GeneratePrefix();
								xmlWellFormedWriter.PushNamespaceImplicit(text, this.ns);
							}
						}
						if (xmlWellFormedWriter.SaveAttrValue || xmlWellFormedWriter.rawWriter == null)
						{
							if (text.Length == 0)
							{
								goto IL_21A;
							}
							awaiter = xmlWellFormedWriter.WriteStringAsync(text).ConfigureAwait(false).GetAwaiter();
							if (!awaiter.IsCompleted)
							{
								this.<>1__state = 1;
								this.<>u__1 = awaiter;
								this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlWellFormedWriter.<WriteQualifiedNameAsync>d__153>(ref awaiter, ref this);
								return;
							}
						}
						else
						{
							awaiter = xmlWellFormedWriter.rawWriter.WriteQualifiedNameAsync(text, this.localName, this.ns).ConfigureAwait(false).GetAwaiter();
							if (!awaiter.IsCompleted)
							{
								this.<>1__state = 4;
								this.<>u__1 = awaiter;
								this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlWellFormedWriter.<WriteQualifiedNameAsync>d__153>(ref awaiter, ref this);
								return;
							}
							goto IL_2F4;
						}
						IL_1A9:
						awaiter.GetResult();
						awaiter = xmlWellFormedWriter.WriteStringAsync(":").ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 2;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlWellFormedWriter.<WriteQualifiedNameAsync>d__153>(ref awaiter, ref this);
							return;
						}
						IL_213:
						awaiter.GetResult();
						IL_21A:
						awaiter = xmlWellFormedWriter.WriteStringAsync(this.localName).ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 3;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlWellFormedWriter.<WriteQualifiedNameAsync>d__153>(ref awaiter, ref this);
							return;
						}
						IL_27E:
						awaiter.GetResult();
						goto IL_2FB;
						IL_2F4:
						awaiter.GetResult();
						IL_2FB:;
					}
					catch
					{
						xmlWellFormedWriter.currentState = XmlWellFormedWriter.State.Error;
						throw;
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

			// Token: 0x06000CD0 RID: 3280 RVA: 0x0005712C File Offset: 0x0005532C
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04000E7C RID: 3708
			public int <>1__state;

			// Token: 0x04000E7D RID: 3709
			public AsyncTaskMethodBuilder <>t__builder;

			// Token: 0x04000E7E RID: 3710
			public string localName;

			// Token: 0x04000E7F RID: 3711
			public XmlWellFormedWriter <>4__this;

			// Token: 0x04000E80 RID: 3712
			public string ns;

			// Token: 0x04000E81 RID: 3713
			private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter <>u__1;
		}

		// Token: 0x02000171 RID: 369
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <WriteBinHexAsync>d__154 : IAsyncStateMachine
		{
			// Token: 0x06000CD1 RID: 3281 RVA: 0x0005713C File Offset: 0x0005533C
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				XmlWellFormedWriter xmlWellFormedWriter = this.<>4__this;
				try
				{
					if (num > 1 && xmlWellFormedWriter.IsClosedOrErrorState)
					{
						throw new InvalidOperationException(Res.GetString("The Writer is closed or in error state."));
					}
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
								goto IL_107;
							}
							awaiter = xmlWellFormedWriter.AdvanceStateAsync(XmlWellFormedWriter.Token.Text).ConfigureAwait(false).GetAwaiter();
							if (!awaiter.IsCompleted)
							{
								this.<>1__state = 0;
								this.<>u__1 = awaiter;
								this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlWellFormedWriter.<WriteBinHexAsync>d__154>(ref awaiter, ref this);
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
						awaiter = xmlWellFormedWriter.<>n__0(this.buffer, this.index, this.count).ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 1;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlWellFormedWriter.<WriteBinHexAsync>d__154>(ref awaiter, ref this);
							return;
						}
						IL_107:
						awaiter.GetResult();
					}
					catch
					{
						xmlWellFormedWriter.currentState = XmlWellFormedWriter.State.Error;
						throw;
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

			// Token: 0x06000CD2 RID: 3282 RVA: 0x000572C8 File Offset: 0x000554C8
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04000E82 RID: 3714
			public int <>1__state;

			// Token: 0x04000E83 RID: 3715
			public AsyncTaskMethodBuilder <>t__builder;

			// Token: 0x04000E84 RID: 3716
			public XmlWellFormedWriter <>4__this;

			// Token: 0x04000E85 RID: 3717
			public byte[] buffer;

			// Token: 0x04000E86 RID: 3718
			public int index;

			// Token: 0x04000E87 RID: 3719
			public int count;

			// Token: 0x04000E88 RID: 3720
			private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter <>u__1;
		}

		// Token: 0x02000172 RID: 370
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <WriteStartDocumentImplAsync>d__155 : IAsyncStateMachine
		{
			// Token: 0x06000CD3 RID: 3283 RVA: 0x000572D8 File Offset: 0x000554D8
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				XmlWellFormedWriter xmlWellFormedWriter = this.<>4__this;
				try
				{
					try
					{
						ConfiguredTaskAwaitable.ConfiguredTaskAwaiter awaiter;
						switch (num)
						{
						case 0:
							awaiter = this.<>u__1;
							this.<>u__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
							this.<>1__state = -1;
							break;
						case 1:
							awaiter = this.<>u__1;
							this.<>u__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
							this.<>1__state = -1;
							goto IL_13A;
						case 2:
							awaiter = this.<>u__1;
							this.<>u__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
							this.<>1__state = -1;
							goto IL_1A2;
						default:
							awaiter = xmlWellFormedWriter.AdvanceStateAsync(XmlWellFormedWriter.Token.StartDocument).ConfigureAwait(false).GetAwaiter();
							if (!awaiter.IsCompleted)
							{
								this.<>1__state = 0;
								this.<>u__1 = awaiter;
								this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlWellFormedWriter.<WriteStartDocumentImplAsync>d__155>(ref awaiter, ref this);
								return;
							}
							break;
						}
						awaiter.GetResult();
						if (xmlWellFormedWriter.conformanceLevel == ConformanceLevel.Auto)
						{
							xmlWellFormedWriter.conformanceLevel = ConformanceLevel.Document;
							xmlWellFormedWriter.stateTable = XmlWellFormedWriter.StateTableDocument;
						}
						else if (xmlWellFormedWriter.conformanceLevel == ConformanceLevel.Fragment)
						{
							throw new InvalidOperationException(Res.GetString("WriteStartDocument cannot be called on writers created with ConformanceLevel.Fragment."));
						}
						if (xmlWellFormedWriter.rawWriter != null)
						{
							if (xmlWellFormedWriter.xmlDeclFollows)
							{
								goto IL_1A9;
							}
							awaiter = xmlWellFormedWriter.rawWriter.WriteXmlDeclarationAsync(this.standalone).ConfigureAwait(false).GetAwaiter();
							if (!awaiter.IsCompleted)
							{
								this.<>1__state = 1;
								this.<>u__1 = awaiter;
								this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlWellFormedWriter.<WriteStartDocumentImplAsync>d__155>(ref awaiter, ref this);
								return;
							}
						}
						else
						{
							awaiter = xmlWellFormedWriter.writer.WriteStartDocumentAsync().ConfigureAwait(false).GetAwaiter();
							if (!awaiter.IsCompleted)
							{
								this.<>1__state = 2;
								this.<>u__1 = awaiter;
								this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlWellFormedWriter.<WriteStartDocumentImplAsync>d__155>(ref awaiter, ref this);
								return;
							}
							goto IL_1A2;
						}
						IL_13A:
						awaiter.GetResult();
						goto IL_1A9;
						IL_1A2:
						awaiter.GetResult();
						IL_1A9:;
					}
					catch
					{
						xmlWellFormedWriter.currentState = XmlWellFormedWriter.State.Error;
						throw;
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

			// Token: 0x06000CD4 RID: 3284 RVA: 0x00057500 File Offset: 0x00055700
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04000E89 RID: 3721
			public int <>1__state;

			// Token: 0x04000E8A RID: 3722
			public AsyncTaskMethodBuilder <>t__builder;

			// Token: 0x04000E8B RID: 3723
			public XmlWellFormedWriter <>4__this;

			// Token: 0x04000E8C RID: 3724
			public XmlStandalone standalone;

			// Token: 0x04000E8D RID: 3725
			private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter <>u__1;
		}

		// Token: 0x02000173 RID: 371
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <_AdvanceStateAsync_ReturnWhenFinish>d__157 : IAsyncStateMachine
		{
			// Token: 0x06000CD5 RID: 3285 RVA: 0x00057510 File Offset: 0x00055710
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				XmlWellFormedWriter xmlWellFormedWriter = this.<>4__this;
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
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlWellFormedWriter.<_AdvanceStateAsync_ReturnWhenFinish>d__157>(ref awaiter, ref this);
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
					xmlWellFormedWriter.currentState = this.newState;
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

			// Token: 0x06000CD6 RID: 3286 RVA: 0x000575DC File Offset: 0x000557DC
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04000E8E RID: 3726
			public int <>1__state;

			// Token: 0x04000E8F RID: 3727
			public AsyncTaskMethodBuilder <>t__builder;

			// Token: 0x04000E90 RID: 3728
			public Task task;

			// Token: 0x04000E91 RID: 3729
			public XmlWellFormedWriter <>4__this;

			// Token: 0x04000E92 RID: 3730
			public XmlWellFormedWriter.State newState;

			// Token: 0x04000E93 RID: 3731
			private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter <>u__1;
		}

		// Token: 0x02000174 RID: 372
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <_AdvanceStateAsync_ContinueWhenFinish>d__159 : IAsyncStateMachine
		{
			// Token: 0x06000CD7 RID: 3287 RVA: 0x000575EC File Offset: 0x000557EC
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				XmlWellFormedWriter xmlWellFormedWriter = this.<>4__this;
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
							goto IL_E8;
						}
						awaiter = this.task.ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 0;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlWellFormedWriter.<_AdvanceStateAsync_ContinueWhenFinish>d__159>(ref awaiter, ref this);
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
					xmlWellFormedWriter.currentState = this.newState;
					awaiter = xmlWellFormedWriter.AdvanceStateAsync(this.token).ConfigureAwait(false).GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						this.<>1__state = 1;
						this.<>u__1 = awaiter;
						this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlWellFormedWriter.<_AdvanceStateAsync_ContinueWhenFinish>d__159>(ref awaiter, ref this);
						return;
					}
					IL_E8:
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

			// Token: 0x06000CD8 RID: 3288 RVA: 0x00057728 File Offset: 0x00055928
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04000E94 RID: 3732
			public int <>1__state;

			// Token: 0x04000E95 RID: 3733
			public AsyncTaskMethodBuilder <>t__builder;

			// Token: 0x04000E96 RID: 3734
			public Task task;

			// Token: 0x04000E97 RID: 3735
			public XmlWellFormedWriter <>4__this;

			// Token: 0x04000E98 RID: 3736
			public XmlWellFormedWriter.State newState;

			// Token: 0x04000E99 RID: 3737
			public XmlWellFormedWriter.Token token;

			// Token: 0x04000E9A RID: 3738
			private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter <>u__1;
		}

		// Token: 0x02000175 RID: 373
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <StartElementContentAsync_WithNS>d__161 : IAsyncStateMachine
		{
			// Token: 0x06000CD9 RID: 3289 RVA: 0x00057738 File Offset: 0x00055938
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				XmlWellFormedWriter xmlWellFormedWriter = this.<>4__this;
				try
				{
					if (num != 0)
					{
						this.<start>5__2 = xmlWellFormedWriter.elemScopeStack[xmlWellFormedWriter.elemTop].prevNSTop;
						this.<i>5__3 = xmlWellFormedWriter.nsTop;
						goto IL_EF;
					}
					ConfiguredTaskAwaitable.ConfiguredTaskAwaiter awaiter = this.<>u__1;
					this.<>u__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
					this.<>1__state = -1;
					IL_D6:
					awaiter.GetResult();
					IL_DD:
					int num2 = this.<i>5__3;
					this.<i>5__3 = num2 - 1;
					IL_EF:
					if (this.<i>5__3 <= this.<start>5__2)
					{
						if (xmlWellFormedWriter.rawWriter != null)
						{
							xmlWellFormedWriter.rawWriter.StartElementContent();
						}
					}
					else
					{
						if (xmlWellFormedWriter.nsStack[this.<i>5__3].kind != XmlWellFormedWriter.NamespaceKind.NeedToWrite)
						{
							goto IL_DD;
						}
						awaiter = xmlWellFormedWriter.nsStack[this.<i>5__3].WriteDeclAsync(xmlWellFormedWriter.writer, xmlWellFormedWriter.rawWriter).ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 0;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlWellFormedWriter.<StartElementContentAsync_WithNS>d__161>(ref awaiter, ref this);
							return;
						}
						goto IL_D6;
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

			// Token: 0x06000CDA RID: 3290 RVA: 0x000578A4 File Offset: 0x00055AA4
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04000E9B RID: 3739
			public int <>1__state;

			// Token: 0x04000E9C RID: 3740
			public AsyncTaskMethodBuilder <>t__builder;

			// Token: 0x04000E9D RID: 3741
			public XmlWellFormedWriter <>4__this;

			// Token: 0x04000E9E RID: 3742
			private int <start>5__2;

			// Token: 0x04000E9F RID: 3743
			private int <i>5__3;

			// Token: 0x04000EA0 RID: 3744
			private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter <>u__1;
		}
	}
}

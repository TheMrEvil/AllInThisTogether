using System;
using System.Globalization;
using System.IO;
using System.Runtime;
using System.Runtime.Serialization;
using System.Text;

namespace System.Xml
{
	// Token: 0x02000040 RID: 64
	internal abstract class XmlBaseWriter : XmlDictionaryWriter, IFragmentCapableXmlDictionaryWriter
	{
		// Token: 0x0600021F RID: 543 RVA: 0x00009B6B File Offset: 0x00007D6B
		protected XmlBaseWriter()
		{
			this.nsMgr = new XmlBaseWriter.NamespaceManager();
			this.writeState = WriteState.Start;
			this.documentState = XmlBaseWriter.DocumentState.None;
		}

		// Token: 0x06000220 RID: 544 RVA: 0x00009B8C File Offset: 0x00007D8C
		protected void SetOutput(XmlStreamNodeWriter writer)
		{
			this.inList = false;
			this.writer = writer;
			this.nodeWriter = writer;
			this.writeState = WriteState.Start;
			this.documentState = XmlBaseWriter.DocumentState.None;
			this.nsMgr.Clear();
			if (this.depth != 0)
			{
				this.elements = null;
				this.depth = 0;
			}
			this.attributeLocalName = null;
			this.attributeValue = null;
			this.oldWriter = null;
			this.oldStream = null;
		}

		// Token: 0x06000221 RID: 545 RVA: 0x00009BF9 File Offset: 0x00007DF9
		public override void Flush()
		{
			if (this.IsClosed)
			{
				this.ThrowClosed();
			}
			this.writer.Flush();
		}

		// Token: 0x06000222 RID: 546 RVA: 0x00009C14 File Offset: 0x00007E14
		public override void Close()
		{
			if (this.IsClosed)
			{
				return;
			}
			try
			{
				this.FinishDocument();
				this.AutoComplete(WriteState.Closed);
				this.writer.Flush();
			}
			finally
			{
				this.nsMgr.Close();
				if (this.depth != 0)
				{
					this.elements = null;
					this.depth = 0;
				}
				this.attributeValue = null;
				this.attributeLocalName = null;
				this.nodeWriter.Close();
				if (this.signingWriter != null)
				{
					this.signingWriter.Close();
				}
				if (this.textFragmentWriter != null)
				{
					this.textFragmentWriter.Close();
				}
				this.oldWriter = null;
				this.oldStream = null;
			}
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x06000223 RID: 547 RVA: 0x00009CC4 File Offset: 0x00007EC4
		protected bool IsClosed
		{
			get
			{
				return this.writeState == WriteState.Closed;
			}
		}

		// Token: 0x06000224 RID: 548 RVA: 0x00009CCF File Offset: 0x00007ECF
		protected void ThrowClosed()
		{
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(System.Runtime.Serialization.SR.GetString("The XmlWriter is closed.")));
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x06000225 RID: 549 RVA: 0x00009CE5 File Offset: 0x00007EE5
		private static BinHexEncoding BinHexEncoding
		{
			get
			{
				if (XmlBaseWriter.binhexEncoding == null)
				{
					XmlBaseWriter.binhexEncoding = new BinHexEncoding();
				}
				return XmlBaseWriter.binhexEncoding;
			}
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x06000226 RID: 550 RVA: 0x00009CFD File Offset: 0x00007EFD
		public override string XmlLang
		{
			get
			{
				return this.nsMgr.XmlLang;
			}
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x06000227 RID: 551 RVA: 0x00009D0A File Offset: 0x00007F0A
		public override XmlSpace XmlSpace
		{
			get
			{
				return this.nsMgr.XmlSpace;
			}
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x06000228 RID: 552 RVA: 0x00009D17 File Offset: 0x00007F17
		public override WriteState WriteState
		{
			get
			{
				return this.writeState;
			}
		}

		// Token: 0x06000229 RID: 553 RVA: 0x00009D20 File Offset: 0x00007F20
		public override void WriteXmlnsAttribute(string prefix, string ns)
		{
			if (this.IsClosed)
			{
				this.ThrowClosed();
			}
			if (ns == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("ns");
			}
			if (this.writeState != WriteState.Element)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(System.Runtime.Serialization.SR.GetString("'{0}' cannot be called while WriteState is '{1}'.", new object[]
				{
					"WriteXmlnsAttribute",
					this.WriteState.ToString()
				})));
			}
			if (prefix == null)
			{
				prefix = this.nsMgr.LookupPrefix(ns);
				if (prefix == null)
				{
					this.GeneratePrefix(ns, null);
					return;
				}
			}
			else
			{
				this.nsMgr.AddNamespaceIfNotDeclared(prefix, ns, null);
			}
		}

		// Token: 0x0600022A RID: 554 RVA: 0x00009DB8 File Offset: 0x00007FB8
		public override void WriteXmlnsAttribute(string prefix, XmlDictionaryString ns)
		{
			if (this.IsClosed)
			{
				this.ThrowClosed();
			}
			if (ns == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("ns");
			}
			if (this.writeState != WriteState.Element)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(System.Runtime.Serialization.SR.GetString("'{0}' cannot be called while WriteState is '{1}'.", new object[]
				{
					"WriteXmlnsAttribute",
					this.WriteState.ToString()
				})));
			}
			if (prefix == null)
			{
				prefix = this.nsMgr.LookupPrefix(ns.Value);
				if (prefix == null)
				{
					this.GeneratePrefix(ns.Value, ns);
					return;
				}
			}
			else
			{
				this.nsMgr.AddNamespaceIfNotDeclared(prefix, ns.Value, ns);
			}
		}

		// Token: 0x0600022B RID: 555 RVA: 0x00009E60 File Offset: 0x00008060
		private void StartAttribute(ref string prefix, string localName, string ns, XmlDictionaryString xNs)
		{
			if (this.IsClosed)
			{
				this.ThrowClosed();
			}
			if (this.writeState == WriteState.Attribute)
			{
				this.WriteEndAttribute();
			}
			if (localName == null || (localName.Length == 0 && prefix != "xmlns"))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("localName"));
			}
			if (this.writeState != WriteState.Element)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(System.Runtime.Serialization.SR.GetString("'{0}' cannot be called while WriteState is '{1}'.", new object[]
				{
					"WriteStartAttribute",
					this.WriteState.ToString()
				})));
			}
			if (prefix == null)
			{
				if (ns == "http://www.w3.org/2000/xmlns/" && localName != "xmlns")
				{
					prefix = "xmlns";
				}
				else if (ns == "http://www.w3.org/XML/1998/namespace")
				{
					prefix = "xml";
				}
				else
				{
					prefix = string.Empty;
				}
			}
			if (prefix.Length == 0 && localName == "xmlns")
			{
				prefix = "xmlns";
				localName = string.Empty;
			}
			this.isXmlnsAttribute = false;
			this.isXmlAttribute = false;
			if (prefix == "xml")
			{
				if (ns != null && ns != "http://www.w3.org/XML/1998/namespace")
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentException(System.Runtime.Serialization.SR.GetString("The prefix '{0}' is bound to the namespace '{1}' and cannot be changed to '{2}'.", new object[]
					{
						"xml",
						"http://www.w3.org/XML/1998/namespace",
						ns
					}), "ns"));
				}
				this.isXmlAttribute = true;
				this.attributeValue = string.Empty;
				this.attributeLocalName = localName;
			}
			else if (prefix == "xmlns")
			{
				if (ns != null && ns != "http://www.w3.org/2000/xmlns/")
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentException(System.Runtime.Serialization.SR.GetString("The prefix '{0}' is bound to the namespace '{1}' and cannot be changed to '{2}'.", new object[]
					{
						"xmlns",
						"http://www.w3.org/2000/xmlns/",
						ns
					}), "ns"));
				}
				this.isXmlnsAttribute = true;
				this.attributeValue = string.Empty;
				this.attributeLocalName = localName;
			}
			else if (ns == null)
			{
				if (prefix.Length == 0)
				{
					ns = string.Empty;
				}
				else
				{
					ns = this.nsMgr.LookupNamespace(prefix);
					if (ns == null)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentException(System.Runtime.Serialization.SR.GetString("The prefix '{0}' is not defined.", new object[]
						{
							prefix
						}), "prefix"));
					}
				}
			}
			else if (ns.Length == 0)
			{
				if (prefix.Length != 0)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentException(System.Runtime.Serialization.SR.GetString("The empty namespace requires a null or empty prefix."), "prefix"));
				}
			}
			else if (prefix.Length == 0)
			{
				prefix = this.nsMgr.LookupAttributePrefix(ns);
				if (prefix == null)
				{
					if (ns.Length == "http://www.w3.org/2000/xmlns/".Length && ns == "http://www.w3.org/2000/xmlns/")
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentException(System.Runtime.Serialization.SR.GetString("The namespace '{1}' can only be bound to the prefix '{0}'.", new object[]
						{
							"xmlns",
							ns
						})));
					}
					if (ns.Length == "http://www.w3.org/XML/1998/namespace".Length && ns == "http://www.w3.org/XML/1998/namespace")
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentException(System.Runtime.Serialization.SR.GetString("The namespace '{1}' can only be bound to the prefix '{0}'.", new object[]
						{
							"xml",
							ns
						})));
					}
					prefix = this.GeneratePrefix(ns, xNs);
				}
			}
			else
			{
				this.nsMgr.AddNamespaceIfNotDeclared(prefix, ns, xNs);
			}
			this.writeState = WriteState.Attribute;
		}

		// Token: 0x0600022C RID: 556 RVA: 0x0000A19D File Offset: 0x0000839D
		public override void WriteStartAttribute(string prefix, string localName, string namespaceUri)
		{
			this.StartAttribute(ref prefix, localName, namespaceUri, null);
			if (!this.isXmlnsAttribute)
			{
				this.writer.WriteStartAttribute(prefix, localName);
			}
		}

		// Token: 0x0600022D RID: 557 RVA: 0x0000A1BF File Offset: 0x000083BF
		public override void WriteStartAttribute(string prefix, XmlDictionaryString localName, XmlDictionaryString namespaceUri)
		{
			this.StartAttribute(ref prefix, (localName != null) ? localName.Value : null, (namespaceUri != null) ? namespaceUri.Value : null, namespaceUri);
			if (!this.isXmlnsAttribute)
			{
				this.writer.WriteStartAttribute(prefix, localName);
			}
		}

		// Token: 0x0600022E RID: 558 RVA: 0x0000A1F8 File Offset: 0x000083F8
		public override void WriteEndAttribute()
		{
			if (this.IsClosed)
			{
				this.ThrowClosed();
			}
			if (this.writeState != WriteState.Attribute)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(System.Runtime.Serialization.SR.GetString("'{0}' cannot be called while WriteState is '{1}'.", new object[]
				{
					"WriteEndAttribute",
					this.WriteState.ToString()
				})));
			}
			this.FlushBase64();
			try
			{
				if (this.isXmlAttribute)
				{
					if (this.attributeLocalName == "lang")
					{
						this.nsMgr.AddLangAttribute(this.attributeValue);
					}
					else if (this.attributeLocalName == "space")
					{
						if (this.attributeValue == "preserve")
						{
							this.nsMgr.AddSpaceAttribute(XmlSpace.Preserve);
						}
						else
						{
							if (!(this.attributeValue == "default"))
							{
								throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentException(System.Runtime.Serialization.SR.GetString("'{0}' is not a valid xml:space value. Valid values are 'default' and 'preserve'.", new object[]
								{
									this.attributeValue
								})));
							}
							this.nsMgr.AddSpaceAttribute(XmlSpace.Default);
						}
					}
					this.isXmlAttribute = false;
					this.attributeLocalName = null;
					this.attributeValue = null;
				}
				if (this.isXmlnsAttribute)
				{
					this.nsMgr.AddNamespaceIfNotDeclared(this.attributeLocalName, this.attributeValue, null);
					this.isXmlnsAttribute = false;
					this.attributeLocalName = null;
					this.attributeValue = null;
				}
				else
				{
					this.writer.WriteEndAttribute();
				}
			}
			finally
			{
				this.writeState = WriteState.Element;
			}
		}

		// Token: 0x0600022F RID: 559 RVA: 0x0000A374 File Offset: 0x00008574
		public override void WriteComment(string text)
		{
			if (this.IsClosed)
			{
				this.ThrowClosed();
			}
			if (this.writeState == WriteState.Attribute)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(System.Runtime.Serialization.SR.GetString("'{0}' cannot be called while WriteState is '{1}'.", new object[]
				{
					"WriteComment",
					this.WriteState.ToString()
				})));
			}
			if (text == null)
			{
				text = string.Empty;
			}
			else if (text.IndexOf("--", StringComparison.Ordinal) != -1 || (text.Length > 0 && text[text.Length - 1] == '-'))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentException(System.Runtime.Serialization.SR.GetString("XML comments cannot contain '--' or end with '-'."), "text"));
			}
			this.StartComment();
			this.FlushBase64();
			this.writer.WriteComment(text);
			this.EndComment();
		}

		// Token: 0x06000230 RID: 560 RVA: 0x0000A440 File Offset: 0x00008640
		public override void WriteFullEndElement()
		{
			if (this.IsClosed)
			{
				this.ThrowClosed();
			}
			if (this.writeState == WriteState.Attribute)
			{
				this.WriteEndAttribute();
			}
			if (this.writeState != WriteState.Element && this.writeState != WriteState.Content)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(System.Runtime.Serialization.SR.GetString("'{0}' cannot be called while WriteState is '{1}'.", new object[]
				{
					"WriteFullEndElement",
					this.WriteState.ToString()
				})));
			}
			this.AutoComplete(WriteState.Content);
			this.WriteEndElement();
		}

		// Token: 0x06000231 RID: 561 RVA: 0x0000A4C4 File Offset: 0x000086C4
		public override void WriteCData(string text)
		{
			if (this.IsClosed)
			{
				this.ThrowClosed();
			}
			if (this.writeState == WriteState.Attribute)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(System.Runtime.Serialization.SR.GetString("'{0}' cannot be called while WriteState is '{1}'.", new object[]
				{
					"WriteCData",
					this.WriteState.ToString()
				})));
			}
			if (text == null)
			{
				text = string.Empty;
			}
			if (text.Length > 0)
			{
				this.StartContent();
				this.FlushBase64();
				this.writer.WriteCData(text);
				this.EndContent();
			}
		}

		// Token: 0x06000232 RID: 562 RVA: 0x0000A553 File Offset: 0x00008753
		public override void WriteDocType(string name, string pubid, string sysid, string subset)
		{
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotSupportedException(System.Runtime.Serialization.SR.GetString("This XmlWriter implementation does not support the '{0}' method.", new object[]
			{
				"WriteDocType"
			})));
		}

		// Token: 0x06000233 RID: 563 RVA: 0x0000A578 File Offset: 0x00008778
		private void StartElement(ref string prefix, string localName, string ns, XmlDictionaryString xNs)
		{
			if (this.IsClosed)
			{
				this.ThrowClosed();
			}
			if (this.documentState == XmlBaseWriter.DocumentState.Epilog)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(System.Runtime.Serialization.SR.GetString("Only one root element is permitted per document.")));
			}
			if (localName == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("localName"));
			}
			if (localName.Length == 0)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentException(System.Runtime.Serialization.SR.GetString("The empty string is not a valid local name."), "localName"));
			}
			if (this.writeState == WriteState.Attribute)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(System.Runtime.Serialization.SR.GetString("'{0}' cannot be called while WriteState is '{1}'.", new object[]
				{
					"WriteStartElement",
					this.WriteState.ToString()
				})));
			}
			this.FlushBase64();
			this.AutoComplete(WriteState.Element);
			XmlBaseWriter.Element element = this.EnterScope();
			if (ns == null)
			{
				if (prefix == null)
				{
					prefix = string.Empty;
				}
				ns = this.nsMgr.LookupNamespace(prefix);
				if (ns == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentException(System.Runtime.Serialization.SR.GetString("The prefix '{0}' is not defined.", new object[]
					{
						prefix
					}), "prefix"));
				}
			}
			else if (prefix == null)
			{
				prefix = this.nsMgr.LookupPrefix(ns);
				if (prefix == null)
				{
					prefix = string.Empty;
					this.nsMgr.AddNamespace(string.Empty, ns, xNs);
				}
			}
			else
			{
				this.nsMgr.AddNamespaceIfNotDeclared(prefix, ns, xNs);
			}
			element.Prefix = prefix;
			element.LocalName = localName;
		}

		// Token: 0x06000234 RID: 564 RVA: 0x0000A6D3 File Offset: 0x000088D3
		public override void WriteStartElement(string prefix, string localName, string namespaceUri)
		{
			this.StartElement(ref prefix, localName, namespaceUri, null);
			this.writer.WriteStartElement(prefix, localName);
		}

		// Token: 0x06000235 RID: 565 RVA: 0x0000A6ED File Offset: 0x000088ED
		public override void WriteStartElement(string prefix, XmlDictionaryString localName, XmlDictionaryString namespaceUri)
		{
			this.StartElement(ref prefix, (localName != null) ? localName.Value : null, (namespaceUri != null) ? namespaceUri.Value : null, namespaceUri);
			this.writer.WriteStartElement(prefix, localName);
		}

		// Token: 0x06000236 RID: 566 RVA: 0x0000A720 File Offset: 0x00008920
		public override void WriteEndElement()
		{
			if (this.IsClosed)
			{
				this.ThrowClosed();
			}
			if (this.depth == 0)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(System.Runtime.Serialization.SR.GetString("Cannot call '{0}' while Depth is '{1}'.", new object[]
				{
					"WriteEndElement",
					this.depth.ToString(CultureInfo.InvariantCulture)
				})));
			}
			if (this.writeState == WriteState.Attribute)
			{
				this.WriteEndAttribute();
			}
			this.FlushBase64();
			if (this.writeState == WriteState.Element)
			{
				this.nsMgr.DeclareNamespaces(this.writer);
				this.writer.WriteEndStartElement(true);
			}
			else
			{
				XmlBaseWriter.Element element = this.elements[this.depth];
				this.writer.WriteEndElement(element.Prefix, element.LocalName);
			}
			this.ExitScope();
			this.writeState = WriteState.Content;
		}

		// Token: 0x06000237 RID: 567 RVA: 0x0000A7E8 File Offset: 0x000089E8
		private XmlBaseWriter.Element EnterScope()
		{
			this.nsMgr.EnterScope();
			this.depth++;
			if (this.elements == null)
			{
				this.elements = new XmlBaseWriter.Element[4];
			}
			else if (this.elements.Length == this.depth)
			{
				XmlBaseWriter.Element[] destinationArray = new XmlBaseWriter.Element[this.depth * 2];
				Array.Copy(this.elements, destinationArray, this.depth);
				this.elements = destinationArray;
			}
			XmlBaseWriter.Element element = this.elements[this.depth];
			if (element == null)
			{
				element = new XmlBaseWriter.Element();
				this.elements[this.depth] = element;
			}
			return element;
		}

		// Token: 0x06000238 RID: 568 RVA: 0x0000A884 File Offset: 0x00008A84
		private void ExitScope()
		{
			this.elements[this.depth].Clear();
			this.depth--;
			if (this.depth == 0 && this.documentState == XmlBaseWriter.DocumentState.Document)
			{
				this.documentState = XmlBaseWriter.DocumentState.Epilog;
			}
			this.nsMgr.ExitScope();
		}

		// Token: 0x06000239 RID: 569 RVA: 0x0000A8D4 File Offset: 0x00008AD4
		protected void FlushElement()
		{
			if (this.writeState == WriteState.Element)
			{
				this.AutoComplete(WriteState.Content);
			}
		}

		// Token: 0x0600023A RID: 570 RVA: 0x0000A8E6 File Offset: 0x00008AE6
		protected void StartComment()
		{
			this.FlushElement();
		}

		// Token: 0x0600023B RID: 571 RVA: 0x0000A8EE File Offset: 0x00008AEE
		protected void EndComment()
		{
		}

		// Token: 0x0600023C RID: 572 RVA: 0x0000A8F0 File Offset: 0x00008AF0
		protected void StartContent()
		{
			this.FlushElement();
			if (this.depth == 0)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(System.Runtime.Serialization.SR.GetString("Text cannot be written outside the root element.")));
			}
		}

		// Token: 0x0600023D RID: 573 RVA: 0x0000A915 File Offset: 0x00008B15
		protected void StartContent(char ch)
		{
			this.FlushElement();
			if (this.depth == 0)
			{
				this.VerifyWhitespace(ch);
			}
		}

		// Token: 0x0600023E RID: 574 RVA: 0x0000A92C File Offset: 0x00008B2C
		protected void StartContent(string s)
		{
			this.FlushElement();
			if (this.depth == 0)
			{
				this.VerifyWhitespace(s);
			}
		}

		// Token: 0x0600023F RID: 575 RVA: 0x0000A943 File Offset: 0x00008B43
		protected void StartContent(char[] chars, int offset, int count)
		{
			this.FlushElement();
			if (this.depth == 0)
			{
				this.VerifyWhitespace(chars, offset, count);
			}
		}

		// Token: 0x06000240 RID: 576 RVA: 0x0000A95C File Offset: 0x00008B5C
		private void VerifyWhitespace(char ch)
		{
			if (!this.IsWhitespace(ch))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(System.Runtime.Serialization.SR.GetString("Text cannot be written outside the root element.")));
			}
		}

		// Token: 0x06000241 RID: 577 RVA: 0x0000A97C File Offset: 0x00008B7C
		private void VerifyWhitespace(string s)
		{
			for (int i = 0; i < s.Length; i++)
			{
				if (!this.IsWhitespace(s[i]))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(System.Runtime.Serialization.SR.GetString("Text cannot be written outside the root element.")));
				}
			}
		}

		// Token: 0x06000242 RID: 578 RVA: 0x0000A9C0 File Offset: 0x00008BC0
		private void VerifyWhitespace(char[] chars, int offset, int count)
		{
			for (int i = 0; i < count; i++)
			{
				if (!this.IsWhitespace(chars[offset + i]))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(System.Runtime.Serialization.SR.GetString("Text cannot be written outside the root element.")));
				}
			}
		}

		// Token: 0x06000243 RID: 579 RVA: 0x0000A9FB File Offset: 0x00008BFB
		private bool IsWhitespace(char ch)
		{
			return ch == ' ' || ch == '\n' || ch == '\r' || ch == 't';
		}

		// Token: 0x06000244 RID: 580 RVA: 0x0000A8EE File Offset: 0x00008AEE
		protected void EndContent()
		{
		}

		// Token: 0x06000245 RID: 581 RVA: 0x0000AA13 File Offset: 0x00008C13
		private void AutoComplete(WriteState writeState)
		{
			if (this.writeState == WriteState.Element)
			{
				this.EndStartElement();
			}
			this.writeState = writeState;
		}

		// Token: 0x06000246 RID: 582 RVA: 0x0000AA2B File Offset: 0x00008C2B
		private void EndStartElement()
		{
			this.nsMgr.DeclareNamespaces(this.writer);
			this.writer.WriteEndStartElement(false);
		}

		// Token: 0x06000247 RID: 583 RVA: 0x0000AA4A File Offset: 0x00008C4A
		public override string LookupPrefix(string ns)
		{
			if (ns == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("ns"));
			}
			if (this.IsClosed)
			{
				this.ThrowClosed();
			}
			return this.nsMgr.LookupPrefix(ns);
		}

		// Token: 0x06000248 RID: 584 RVA: 0x0000AA79 File Offset: 0x00008C79
		internal string LookupNamespace(string prefix)
		{
			if (prefix == null)
			{
				return null;
			}
			return this.nsMgr.LookupNamespace(prefix);
		}

		// Token: 0x06000249 RID: 585 RVA: 0x0000AA8C File Offset: 0x00008C8C
		private string GetQualifiedNamePrefix(string namespaceUri, XmlDictionaryString xNs)
		{
			string text = this.nsMgr.LookupPrefix(namespaceUri);
			if (text == null)
			{
				if (this.writeState != WriteState.Attribute)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentException(System.Runtime.Serialization.SR.GetString("The namespace '{0}' is not defined.", new object[]
					{
						namespaceUri
					}), "namespaceUri"));
				}
				text = this.GeneratePrefix(namespaceUri, xNs);
			}
			return text;
		}

		// Token: 0x0600024A RID: 586 RVA: 0x0000AAE0 File Offset: 0x00008CE0
		public override void WriteQualifiedName(string localName, string namespaceUri)
		{
			if (this.IsClosed)
			{
				this.ThrowClosed();
			}
			if (localName == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("localName"));
			}
			if (localName.Length == 0)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentException(System.Runtime.Serialization.SR.GetString("The empty string is not a valid local name."), "localName"));
			}
			if (namespaceUri == null)
			{
				namespaceUri = string.Empty;
			}
			string qualifiedNamePrefix = this.GetQualifiedNamePrefix(namespaceUri, null);
			if (qualifiedNamePrefix.Length != 0)
			{
				this.WriteString(qualifiedNamePrefix);
				this.WriteString(":");
			}
			this.WriteString(localName);
		}

		// Token: 0x0600024B RID: 587 RVA: 0x0000AB64 File Offset: 0x00008D64
		public override void WriteQualifiedName(XmlDictionaryString localName, XmlDictionaryString namespaceUri)
		{
			if (this.IsClosed)
			{
				this.ThrowClosed();
			}
			if (localName == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("localName"));
			}
			if (localName.Value.Length == 0)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentException(System.Runtime.Serialization.SR.GetString("The empty string is not a valid local name."), "localName"));
			}
			if (namespaceUri == null)
			{
				namespaceUri = XmlDictionaryString.Empty;
			}
			string qualifiedNamePrefix = this.GetQualifiedNamePrefix(namespaceUri.Value, namespaceUri);
			this.FlushBase64();
			if (this.attributeValue != null)
			{
				this.WriteAttributeText(qualifiedNamePrefix + ":" + namespaceUri.Value);
			}
			if (!this.isXmlnsAttribute)
			{
				this.StartContent();
				this.writer.WriteQualifiedName(qualifiedNamePrefix, localName);
				this.EndContent();
			}
		}

		// Token: 0x0600024C RID: 588 RVA: 0x0000AC18 File Offset: 0x00008E18
		public override void WriteStartDocument()
		{
			if (this.IsClosed)
			{
				this.ThrowClosed();
			}
			if (this.writeState != WriteState.Start)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(System.Runtime.Serialization.SR.GetString("'{0}' cannot be called while WriteState is '{1}'.", new object[]
				{
					"WriteStartDocument",
					this.WriteState.ToString()
				})));
			}
			this.writeState = WriteState.Prolog;
			this.documentState = XmlBaseWriter.DocumentState.Document;
			this.writer.WriteDeclaration();
		}

		// Token: 0x0600024D RID: 589 RVA: 0x0000AC8E File Offset: 0x00008E8E
		public override void WriteStartDocument(bool standalone)
		{
			if (this.IsClosed)
			{
				this.ThrowClosed();
			}
			this.WriteStartDocument();
		}

		// Token: 0x0600024E RID: 590 RVA: 0x0000ACA4 File Offset: 0x00008EA4
		public override void WriteProcessingInstruction(string name, string text)
		{
			if (this.IsClosed)
			{
				this.ThrowClosed();
			}
			if (name != "xml")
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentException(System.Runtime.Serialization.SR.GetString("Processing instructions (other than the XML declaration) and DTDs are not supported."), "name"));
			}
			if (this.writeState != WriteState.Start)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(System.Runtime.Serialization.SR.GetString("XML declaration can only be written at the beginning of the document.")));
			}
			this.writer.WriteDeclaration();
		}

		// Token: 0x0600024F RID: 591 RVA: 0x0000AD0E File Offset: 0x00008F0E
		private void FinishDocument()
		{
			if (this.writeState == WriteState.Attribute)
			{
				this.WriteEndAttribute();
			}
			while (this.depth > 0)
			{
				this.WriteEndElement();
			}
		}

		// Token: 0x06000250 RID: 592 RVA: 0x0000AD30 File Offset: 0x00008F30
		public override void WriteEndDocument()
		{
			if (this.IsClosed)
			{
				this.ThrowClosed();
			}
			if (this.writeState == WriteState.Start || this.writeState == WriteState.Prolog)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(System.Runtime.Serialization.SR.GetString("The document does not have a root element.")));
			}
			this.FinishDocument();
			this.writeState = WriteState.Start;
			this.documentState = XmlBaseWriter.DocumentState.End;
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x06000251 RID: 593 RVA: 0x0000AD85 File Offset: 0x00008F85
		// (set) Token: 0x06000252 RID: 594 RVA: 0x0000AD92 File Offset: 0x00008F92
		protected int NamespaceBoundary
		{
			get
			{
				return this.nsMgr.NamespaceBoundary;
			}
			set
			{
				this.nsMgr.NamespaceBoundary = value;
			}
		}

		// Token: 0x06000253 RID: 595 RVA: 0x0000ADA0 File Offset: 0x00008FA0
		public override void WriteEntityRef(string name)
		{
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotSupportedException(System.Runtime.Serialization.SR.GetString("This XmlWriter implementation does not support the '{0}' method.", new object[]
			{
				"WriteEntityRef"
			})));
		}

		// Token: 0x06000254 RID: 596 RVA: 0x0000ADC4 File Offset: 0x00008FC4
		public override void WriteName(string name)
		{
			if (this.IsClosed)
			{
				this.ThrowClosed();
			}
			this.WriteString(name);
		}

		// Token: 0x06000255 RID: 597 RVA: 0x0000ADDB File Offset: 0x00008FDB
		public override void WriteNmToken(string name)
		{
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotSupportedException(System.Runtime.Serialization.SR.GetString("This XmlWriter implementation does not support the '{0}' method.", new object[]
			{
				"WriteNmToken"
			})));
		}

		// Token: 0x06000256 RID: 598 RVA: 0x0000AE00 File Offset: 0x00009000
		public override void WriteWhitespace(string whitespace)
		{
			if (this.IsClosed)
			{
				this.ThrowClosed();
			}
			if (whitespace == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("whitespace");
			}
			foreach (char c in whitespace)
			{
				if (c != ' ' && c != '\t' && c != '\n' && c != '\r')
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentException(System.Runtime.Serialization.SR.GetString("Only white space characters can be written with this method."), "whitespace"));
				}
			}
			this.WriteString(whitespace);
		}

		// Token: 0x06000257 RID: 599 RVA: 0x0000AE78 File Offset: 0x00009078
		public override void WriteString(string value)
		{
			if (this.IsClosed)
			{
				this.ThrowClosed();
			}
			if (value == null)
			{
				value = string.Empty;
			}
			if (value.Length > 0 || this.inList)
			{
				this.FlushBase64();
				if (this.attributeValue != null)
				{
					this.WriteAttributeText(value);
				}
				if (!this.isXmlnsAttribute)
				{
					this.StartContent(value);
					this.writer.WriteEscapedText(value);
					this.EndContent();
				}
			}
		}

		// Token: 0x06000258 RID: 600 RVA: 0x0000AEE4 File Offset: 0x000090E4
		public override void WriteString(XmlDictionaryString value)
		{
			if (this.IsClosed)
			{
				this.ThrowClosed();
			}
			if (value == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("value");
			}
			if (value.Value.Length > 0)
			{
				this.FlushBase64();
				if (this.attributeValue != null)
				{
					this.WriteAttributeText(value.Value);
				}
				if (!this.isXmlnsAttribute)
				{
					this.StartContent(value.Value);
					this.writer.WriteEscapedText(value);
					this.EndContent();
				}
			}
		}

		// Token: 0x06000259 RID: 601 RVA: 0x0000AF5C File Offset: 0x0000915C
		public override void WriteChars(char[] chars, int offset, int count)
		{
			if (this.IsClosed)
			{
				this.ThrowClosed();
			}
			if (chars == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("chars"));
			}
			if (offset < 0)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("offset", System.Runtime.Serialization.SR.GetString("The value of this argument must be non-negative.")));
			}
			if (count < 0)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("count", System.Runtime.Serialization.SR.GetString("The value of this argument must be non-negative.")));
			}
			if (count > chars.Length - offset)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("count", System.Runtime.Serialization.SR.GetString("The specified size exceeds the remaining buffer space ({0} bytes).", new object[]
				{
					chars.Length - offset
				})));
			}
			if (count > 0)
			{
				this.FlushBase64();
				if (this.attributeValue != null)
				{
					this.WriteAttributeText(new string(chars, offset, count));
				}
				if (!this.isXmlnsAttribute)
				{
					this.StartContent(chars, offset, count);
					this.writer.WriteEscapedText(chars, offset, count);
					this.EndContent();
				}
			}
		}

		// Token: 0x0600025A RID: 602 RVA: 0x0000B040 File Offset: 0x00009240
		public override void WriteRaw(string value)
		{
			if (this.IsClosed)
			{
				this.ThrowClosed();
			}
			if (value == null)
			{
				value = string.Empty;
			}
			if (value.Length > 0)
			{
				this.FlushBase64();
				if (this.attributeValue != null)
				{
					this.WriteAttributeText(value);
				}
				if (!this.isXmlnsAttribute)
				{
					this.StartContent(value);
					this.writer.WriteText(value);
					this.EndContent();
				}
			}
		}

		// Token: 0x0600025B RID: 603 RVA: 0x0000B0A4 File Offset: 0x000092A4
		public override void WriteRaw(char[] chars, int offset, int count)
		{
			if (this.IsClosed)
			{
				this.ThrowClosed();
			}
			if (chars == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("chars"));
			}
			if (offset < 0)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("offset", System.Runtime.Serialization.SR.GetString("The value of this argument must be non-negative.")));
			}
			if (count < 0)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("count", System.Runtime.Serialization.SR.GetString("The value of this argument must be non-negative.")));
			}
			if (count > chars.Length - offset)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("count", System.Runtime.Serialization.SR.GetString("The specified size exceeds the remaining buffer space ({0} bytes).", new object[]
				{
					chars.Length - offset
				})));
			}
			if (count > 0)
			{
				this.FlushBase64();
				if (this.attributeValue != null)
				{
					this.WriteAttributeText(new string(chars, offset, count));
				}
				if (!this.isXmlnsAttribute)
				{
					this.StartContent(chars, offset, count);
					this.writer.WriteText(chars, offset, count);
					this.EndContent();
				}
			}
		}

		// Token: 0x0600025C RID: 604 RVA: 0x0000B188 File Offset: 0x00009388
		public override void WriteCharEntity(char ch)
		{
			if (this.IsClosed)
			{
				this.ThrowClosed();
			}
			if (ch >= '\ud800' && ch <= '\udfff')
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentException(System.Runtime.Serialization.SR.GetString("The surrogate pair is invalid. Missing a low surrogate character."), "ch"));
			}
			if (this.attributeValue != null)
			{
				this.WriteAttributeText(ch.ToString());
			}
			if (!this.isXmlnsAttribute)
			{
				this.StartContent(ch);
				this.FlushBase64();
				this.writer.WriteCharEntity((int)ch);
				this.EndContent();
			}
		}

		// Token: 0x0600025D RID: 605 RVA: 0x0000B20C File Offset: 0x0000940C
		public override void WriteSurrogateCharEntity(char lowChar, char highChar)
		{
			if (this.IsClosed)
			{
				this.ThrowClosed();
			}
			SurrogateChar surrogateChar = new SurrogateChar(lowChar, highChar);
			if (this.attributeValue != null)
			{
				char[] value = new char[]
				{
					highChar,
					lowChar
				};
				this.WriteAttributeText(new string(value));
			}
			if (!this.isXmlnsAttribute)
			{
				this.StartContent();
				this.FlushBase64();
				this.writer.WriteCharEntity(surrogateChar.Char);
				this.EndContent();
			}
		}

		// Token: 0x0600025E RID: 606 RVA: 0x0000B280 File Offset: 0x00009480
		public override void WriteValue(object value)
		{
			if (this.IsClosed)
			{
				this.ThrowClosed();
			}
			if (value == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("value"));
			}
			if (value is object[])
			{
				this.WriteValue((object[])value);
				return;
			}
			if (value is Array)
			{
				this.WriteValue((Array)value);
				return;
			}
			if (value is IStreamProvider)
			{
				this.WriteValue((IStreamProvider)value);
				return;
			}
			this.WritePrimitiveValue(value);
		}

		// Token: 0x0600025F RID: 607 RVA: 0x0000B2F4 File Offset: 0x000094F4
		protected void WritePrimitiveValue(object value)
		{
			if (this.IsClosed)
			{
				this.ThrowClosed();
			}
			if (value == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("value"));
			}
			if (value is ulong)
			{
				this.WriteValue((ulong)value);
				return;
			}
			if (value is string)
			{
				this.WriteValue((string)value);
				return;
			}
			if (value is int)
			{
				this.WriteValue((int)value);
				return;
			}
			if (value is long)
			{
				this.WriteValue((long)value);
				return;
			}
			if (value is bool)
			{
				this.WriteValue((bool)value);
				return;
			}
			if (value is double)
			{
				this.WriteValue((double)value);
				return;
			}
			if (value is DateTime)
			{
				this.WriteValue((DateTime)value);
				return;
			}
			if (value is float)
			{
				this.WriteValue((float)value);
				return;
			}
			if (value is decimal)
			{
				this.WriteValue((decimal)value);
				return;
			}
			if (value is XmlDictionaryString)
			{
				this.WriteValue((XmlDictionaryString)value);
				return;
			}
			if (value is UniqueId)
			{
				this.WriteValue((UniqueId)value);
				return;
			}
			if (value is Guid)
			{
				this.WriteValue((Guid)value);
				return;
			}
			if (value is TimeSpan)
			{
				this.WriteValue((TimeSpan)value);
				return;
			}
			if (value.GetType().IsArray)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentException(System.Runtime.Serialization.SR.GetString("Nested arrays are not supported."), "value"));
			}
			base.WriteValue(value);
		}

		// Token: 0x06000260 RID: 608 RVA: 0x0000ADC4 File Offset: 0x00008FC4
		public override void WriteValue(string value)
		{
			if (this.IsClosed)
			{
				this.ThrowClosed();
			}
			this.WriteString(value);
		}

		// Token: 0x06000261 RID: 609 RVA: 0x0000B464 File Offset: 0x00009664
		public override void WriteValue(int value)
		{
			if (this.IsClosed)
			{
				this.ThrowClosed();
			}
			this.FlushBase64();
			if (this.attributeValue != null)
			{
				this.WriteAttributeText(XmlConverter.ToString(value));
			}
			if (!this.isXmlnsAttribute)
			{
				this.StartContent();
				this.writer.WriteInt32Text(value);
				this.EndContent();
			}
		}

		// Token: 0x06000262 RID: 610 RVA: 0x0000B4BC File Offset: 0x000096BC
		public override void WriteValue(long value)
		{
			if (this.IsClosed)
			{
				this.ThrowClosed();
			}
			this.FlushBase64();
			if (this.attributeValue != null)
			{
				this.WriteAttributeText(XmlConverter.ToString(value));
			}
			if (!this.isXmlnsAttribute)
			{
				this.StartContent();
				this.writer.WriteInt64Text(value);
				this.EndContent();
			}
		}

		// Token: 0x06000263 RID: 611 RVA: 0x0000B514 File Offset: 0x00009714
		private void WriteValue(ulong value)
		{
			if (this.IsClosed)
			{
				this.ThrowClosed();
			}
			this.FlushBase64();
			if (this.attributeValue != null)
			{
				this.WriteAttributeText(XmlConverter.ToString(value));
			}
			if (!this.isXmlnsAttribute)
			{
				this.StartContent();
				this.writer.WriteUInt64Text(value);
				this.EndContent();
			}
		}

		// Token: 0x06000264 RID: 612 RVA: 0x0000B56C File Offset: 0x0000976C
		public override void WriteValue(bool value)
		{
			if (this.IsClosed)
			{
				this.ThrowClosed();
			}
			this.FlushBase64();
			if (this.attributeValue != null)
			{
				this.WriteAttributeText(XmlConverter.ToString(value));
			}
			if (!this.isXmlnsAttribute)
			{
				this.StartContent();
				this.writer.WriteBoolText(value);
				this.EndContent();
			}
		}

		// Token: 0x06000265 RID: 613 RVA: 0x0000B5C4 File Offset: 0x000097C4
		public override void WriteValue(decimal value)
		{
			if (this.IsClosed)
			{
				this.ThrowClosed();
			}
			this.FlushBase64();
			if (this.attributeValue != null)
			{
				this.WriteAttributeText(XmlConverter.ToString(value));
			}
			if (!this.isXmlnsAttribute)
			{
				this.StartContent();
				this.writer.WriteDecimalText(value);
				this.EndContent();
			}
		}

		// Token: 0x06000266 RID: 614 RVA: 0x0000B61C File Offset: 0x0000981C
		public override void WriteValue(float value)
		{
			if (this.IsClosed)
			{
				this.ThrowClosed();
			}
			this.FlushBase64();
			if (this.attributeValue != null)
			{
				this.WriteAttributeText(XmlConverter.ToString(value));
			}
			if (!this.isXmlnsAttribute)
			{
				this.StartContent();
				this.writer.WriteFloatText(value);
				this.EndContent();
			}
		}

		// Token: 0x06000267 RID: 615 RVA: 0x0000B674 File Offset: 0x00009874
		public override void WriteValue(double value)
		{
			if (this.IsClosed)
			{
				this.ThrowClosed();
			}
			this.FlushBase64();
			if (this.attributeValue != null)
			{
				this.WriteAttributeText(XmlConverter.ToString(value));
			}
			if (!this.isXmlnsAttribute)
			{
				this.StartContent();
				this.writer.WriteDoubleText(value);
				this.EndContent();
			}
		}

		// Token: 0x06000268 RID: 616 RVA: 0x0000B6C9 File Offset: 0x000098C9
		public override void WriteValue(XmlDictionaryString value)
		{
			this.WriteString(value);
		}

		// Token: 0x06000269 RID: 617 RVA: 0x0000B6D4 File Offset: 0x000098D4
		public override void WriteValue(DateTime value)
		{
			if (this.IsClosed)
			{
				this.ThrowClosed();
			}
			this.FlushBase64();
			if (this.attributeValue != null)
			{
				this.WriteAttributeText(XmlConverter.ToString(value));
			}
			if (!this.isXmlnsAttribute)
			{
				this.StartContent();
				this.writer.WriteDateTimeText(value);
				this.EndContent();
			}
		}

		// Token: 0x0600026A RID: 618 RVA: 0x0000B72C File Offset: 0x0000992C
		public override void WriteValue(UniqueId value)
		{
			if (this.IsClosed)
			{
				this.ThrowClosed();
			}
			if (value == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("value");
			}
			this.FlushBase64();
			if (this.attributeValue != null)
			{
				this.WriteAttributeText(XmlConverter.ToString(value));
			}
			if (!this.isXmlnsAttribute)
			{
				this.StartContent();
				this.writer.WriteUniqueIdText(value);
				this.EndContent();
			}
		}

		// Token: 0x0600026B RID: 619 RVA: 0x0000B798 File Offset: 0x00009998
		public override void WriteValue(Guid value)
		{
			if (this.IsClosed)
			{
				this.ThrowClosed();
			}
			this.FlushBase64();
			if (this.attributeValue != null)
			{
				this.WriteAttributeText(XmlConverter.ToString(value));
			}
			if (!this.isXmlnsAttribute)
			{
				this.StartContent();
				this.writer.WriteGuidText(value);
				this.EndContent();
			}
		}

		// Token: 0x0600026C RID: 620 RVA: 0x0000B7F0 File Offset: 0x000099F0
		public override void WriteValue(TimeSpan value)
		{
			if (this.IsClosed)
			{
				this.ThrowClosed();
			}
			this.FlushBase64();
			if (this.attributeValue != null)
			{
				this.WriteAttributeText(XmlConverter.ToString(value));
			}
			if (!this.isXmlnsAttribute)
			{
				this.StartContent();
				this.writer.WriteTimeSpanText(value);
				this.EndContent();
			}
		}

		// Token: 0x0600026D RID: 621 RVA: 0x0000B848 File Offset: 0x00009A48
		public override void WriteBase64(byte[] buffer, int offset, int count)
		{
			if (this.IsClosed)
			{
				this.ThrowClosed();
			}
			this.EnsureBufferBounds(buffer, offset, count);
			if (count > 0)
			{
				if (this.trailByteCount > 0)
				{
					while (this.trailByteCount < 3 && count > 0)
					{
						byte[] array = this.trailBytes;
						int num = this.trailByteCount;
						this.trailByteCount = num + 1;
						array[num] = buffer[offset++];
						count--;
					}
				}
				int num2 = this.trailByteCount + count;
				int num3 = num2 - num2 % 3;
				if (this.trailBytes == null)
				{
					this.trailBytes = new byte[3];
				}
				if (num3 >= 3)
				{
					if (this.attributeValue != null)
					{
						this.WriteAttributeText(XmlConverter.Base64Encoding.GetString(this.trailBytes, 0, this.trailByteCount));
						this.WriteAttributeText(XmlConverter.Base64Encoding.GetString(buffer, offset, num3 - this.trailByteCount));
					}
					if (!this.isXmlnsAttribute)
					{
						this.StartContent();
						this.writer.WriteBase64Text(this.trailBytes, this.trailByteCount, buffer, offset, num3 - this.trailByteCount);
						this.EndContent();
					}
					this.trailByteCount = num2 - num3;
					if (this.trailByteCount > 0)
					{
						int num4 = offset + count - this.trailByteCount;
						for (int i = 0; i < this.trailByteCount; i++)
						{
							this.trailBytes[i] = buffer[num4++];
						}
						return;
					}
				}
				else
				{
					Buffer.BlockCopy(buffer, offset, this.trailBytes, this.trailByteCount, count);
					this.trailByteCount += count;
				}
			}
		}

		// Token: 0x0600026E RID: 622 RVA: 0x0000B9B3 File Offset: 0x00009BB3
		internal override IAsyncResult BeginWriteBase64(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
		{
			if (this.IsClosed)
			{
				this.ThrowClosed();
			}
			this.EnsureBufferBounds(buffer, offset, count);
			return new XmlBaseWriter.WriteBase64AsyncResult(buffer, offset, count, this, callback, state);
		}

		// Token: 0x0600026F RID: 623 RVA: 0x0000B9D9 File Offset: 0x00009BD9
		internal override void EndWriteBase64(IAsyncResult result)
		{
			XmlBaseWriter.WriteBase64AsyncResult.End(result);
		}

		// Token: 0x06000270 RID: 624 RVA: 0x0000B9E1 File Offset: 0x00009BE1
		internal override AsyncCompletionResult WriteBase64Async(AsyncEventArgs<XmlWriteBase64AsyncArguments> state)
		{
			if (this.nodeWriterAsyncHelper == null)
			{
				this.nodeWriterAsyncHelper = new XmlBaseWriter.XmlBaseWriterNodeWriterAsyncHelper(this);
			}
			this.nodeWriterAsyncHelper.SetArguments(state);
			if (this.nodeWriterAsyncHelper.StartAsync() == AsyncCompletionResult.Completed)
			{
				return AsyncCompletionResult.Completed;
			}
			return AsyncCompletionResult.Queued;
		}

		// Token: 0x06000271 RID: 625 RVA: 0x0000BA14 File Offset: 0x00009C14
		public override void WriteBinHex(byte[] buffer, int offset, int count)
		{
			if (this.IsClosed)
			{
				this.ThrowClosed();
			}
			this.EnsureBufferBounds(buffer, offset, count);
			this.WriteRaw(XmlBaseWriter.BinHexEncoding.GetString(buffer, offset, count));
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x06000272 RID: 626 RVA: 0x000066E8 File Offset: 0x000048E8
		public override bool CanCanonicalize
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x06000273 RID: 627 RVA: 0x0000BA40 File Offset: 0x00009C40
		protected bool Signing
		{
			get
			{
				return this.writer == this.signingWriter;
			}
		}

		// Token: 0x06000274 RID: 628 RVA: 0x0000BA50 File Offset: 0x00009C50
		public override void StartCanonicalization(Stream stream, bool includeComments, string[] inclusivePrefixes)
		{
			if (this.IsClosed)
			{
				this.ThrowClosed();
			}
			if (this.Signing)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(System.Runtime.Serialization.SR.GetString("XML canonicalization started")));
			}
			this.FlushElement();
			if (this.signingWriter == null)
			{
				this.signingWriter = this.CreateSigningNodeWriter();
			}
			this.signingWriter.SetOutput(this.writer, stream, includeComments, inclusivePrefixes);
			this.writer = this.signingWriter;
			this.SignScope(this.signingWriter.CanonicalWriter);
		}

		// Token: 0x06000275 RID: 629 RVA: 0x0000BAD4 File Offset: 0x00009CD4
		public override void EndCanonicalization()
		{
			if (this.IsClosed)
			{
				this.ThrowClosed();
			}
			if (!this.Signing)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(System.Runtime.Serialization.SR.GetString("XML canonicalization was not started.")));
			}
			this.signingWriter.Flush();
			this.writer = this.signingWriter.NodeWriter;
		}

		// Token: 0x06000276 RID: 630
		protected abstract XmlSigningNodeWriter CreateSigningNodeWriter();

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x06000277 RID: 631 RVA: 0x000066E8 File Offset: 0x000048E8
		public virtual bool CanFragment
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000278 RID: 632 RVA: 0x0000BB28 File Offset: 0x00009D28
		public void StartFragment(Stream stream, bool generateSelfContainedTextFragment)
		{
			if (!this.CanFragment)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotSupportedException());
			}
			if (this.IsClosed)
			{
				this.ThrowClosed();
			}
			if (stream == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("stream"));
			}
			if (this.oldStream != null || this.oldWriter != null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException());
			}
			if (this.WriteState == WriteState.Attribute)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(System.Runtime.Serialization.SR.GetString("'{0}' cannot be called while WriteState is '{1}'.", new object[]
				{
					"StartFragment",
					this.WriteState.ToString()
				})));
			}
			this.FlushElement();
			this.writer.Flush();
			this.oldNamespaceBoundary = this.NamespaceBoundary;
			XmlStreamNodeWriter xmlStreamNodeWriter = null;
			if (generateSelfContainedTextFragment)
			{
				this.NamespaceBoundary = this.depth + 1;
				if (this.textFragmentWriter == null)
				{
					this.textFragmentWriter = new XmlUTF8NodeWriter();
				}
				this.textFragmentWriter.SetOutput(stream, false, Encoding.UTF8);
				xmlStreamNodeWriter = this.textFragmentWriter;
			}
			if (this.Signing)
			{
				if (xmlStreamNodeWriter != null)
				{
					this.oldWriter = this.signingWriter.NodeWriter;
					this.signingWriter.NodeWriter = xmlStreamNodeWriter;
					return;
				}
				this.oldStream = ((XmlStreamNodeWriter)this.signingWriter.NodeWriter).Stream;
				((XmlStreamNodeWriter)this.signingWriter.NodeWriter).Stream = stream;
				return;
			}
			else
			{
				if (xmlStreamNodeWriter != null)
				{
					this.oldWriter = this.writer;
					this.writer = xmlStreamNodeWriter;
					return;
				}
				this.oldStream = this.nodeWriter.Stream;
				this.nodeWriter.Stream = stream;
				return;
			}
		}

		// Token: 0x06000279 RID: 633 RVA: 0x0000BCB4 File Offset: 0x00009EB4
		public void EndFragment()
		{
			if (this.IsClosed)
			{
				this.ThrowClosed();
			}
			if (this.oldStream == null && this.oldWriter == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException());
			}
			if (this.WriteState == WriteState.Attribute)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(System.Runtime.Serialization.SR.GetString("'{0}' cannot be called while WriteState is '{1}'.", new object[]
				{
					"EndFragment",
					this.WriteState.ToString()
				})));
			}
			this.FlushElement();
			this.writer.Flush();
			if (this.Signing)
			{
				if (this.oldWriter != null)
				{
					this.signingWriter.NodeWriter = this.oldWriter;
				}
				else
				{
					((XmlStreamNodeWriter)this.signingWriter.NodeWriter).Stream = this.oldStream;
				}
			}
			else if (this.oldWriter != null)
			{
				this.writer = this.oldWriter;
			}
			else
			{
				this.nodeWriter.Stream = this.oldStream;
			}
			this.NamespaceBoundary = this.oldNamespaceBoundary;
			this.oldWriter = null;
			this.oldStream = null;
		}

		// Token: 0x0600027A RID: 634 RVA: 0x0000BDC0 File Offset: 0x00009FC0
		public void WriteFragment(byte[] buffer, int offset, int count)
		{
			if (!this.CanFragment)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotSupportedException());
			}
			if (this.IsClosed)
			{
				this.ThrowClosed();
			}
			if (buffer == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("buffer"));
			}
			if (offset < 0)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("offset", System.Runtime.Serialization.SR.GetString("The value of this argument must be non-negative.")));
			}
			if (count < 0)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("count", System.Runtime.Serialization.SR.GetString("The value of this argument must be non-negative.")));
			}
			if (count > buffer.Length - offset)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("count", System.Runtime.Serialization.SR.GetString("The specified size exceeds the remaining buffer space ({0} bytes).", new object[]
				{
					buffer.Length - offset
				})));
			}
			if (this.WriteState == WriteState.Attribute)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(System.Runtime.Serialization.SR.GetString("'{0}' cannot be called while WriteState is '{1}'.", new object[]
				{
					"WriteFragment",
					this.WriteState.ToString()
				})));
			}
			if (this.writer != this.nodeWriter)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException());
			}
			this.FlushElement();
			this.FlushBase64();
			this.nodeWriter.Flush();
			this.nodeWriter.Stream.Write(buffer, offset, count);
		}

		// Token: 0x0600027B RID: 635 RVA: 0x0000BEF8 File Offset: 0x0000A0F8
		private void FlushBase64()
		{
			if (this.trailByteCount > 0)
			{
				this.FlushTrailBytes();
			}
		}

		// Token: 0x0600027C RID: 636 RVA: 0x0000BF0C File Offset: 0x0000A10C
		private void FlushTrailBytes()
		{
			if (this.attributeValue != null)
			{
				this.WriteAttributeText(XmlConverter.Base64Encoding.GetString(this.trailBytes, 0, this.trailByteCount));
			}
			if (!this.isXmlnsAttribute)
			{
				this.StartContent();
				this.writer.WriteBase64Text(this.trailBytes, this.trailByteCount, this.trailBytes, 0, 0);
				this.EndContent();
			}
			this.trailByteCount = 0;
		}

		// Token: 0x0600027D RID: 637 RVA: 0x0000BF78 File Offset: 0x0000A178
		private void WriteValue(object[] array)
		{
			this.FlushBase64();
			this.StartContent();
			this.writer.WriteStartListText();
			this.inList = true;
			for (int i = 0; i < array.Length; i++)
			{
				if (i != 0)
				{
					this.writer.WriteListSeparator();
				}
				this.WritePrimitiveValue(array[i]);
			}
			this.inList = false;
			this.writer.WriteEndListText();
			this.EndContent();
		}

		// Token: 0x0600027E RID: 638 RVA: 0x0000BFE0 File Offset: 0x0000A1E0
		private void WriteValue(Array array)
		{
			this.FlushBase64();
			this.StartContent();
			this.writer.WriteStartListText();
			this.inList = true;
			for (int i = 0; i < array.Length; i++)
			{
				if (i != 0)
				{
					this.writer.WriteListSeparator();
				}
				this.WritePrimitiveValue(array.GetValue(i));
			}
			this.inList = false;
			this.writer.WriteEndListText();
			this.EndContent();
		}

		// Token: 0x0600027F RID: 639 RVA: 0x0000C050 File Offset: 0x0000A250
		protected void StartArray(int count)
		{
			this.FlushBase64();
			if (this.documentState == XmlBaseWriter.DocumentState.Epilog)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(System.Runtime.Serialization.SR.GetString("Only one root element is permitted per document.")));
			}
			if (this.documentState == XmlBaseWriter.DocumentState.Document && count > 1 && this.depth == 0)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(System.Runtime.Serialization.SR.GetString("Only one root element is permitted per document.")));
			}
			if (this.writeState == WriteState.Attribute)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(System.Runtime.Serialization.SR.GetString("'{0}' cannot be called while WriteState is '{1}'.", new object[]
				{
					"WriteStartElement",
					this.WriteState.ToString()
				})));
			}
			this.AutoComplete(WriteState.Content);
		}

		// Token: 0x06000280 RID: 640 RVA: 0x0000A8EE File Offset: 0x00008AEE
		protected void EndArray()
		{
		}

		// Token: 0x06000281 RID: 641 RVA: 0x0000C0F8 File Offset: 0x0000A2F8
		private void EnsureBufferBounds(byte[] buffer, int offset, int count)
		{
			if (buffer == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("buffer");
			}
			if (offset < 0)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("offset", System.Runtime.Serialization.SR.GetString("The value of this argument must be non-negative.")));
			}
			if (count < 0)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("count", System.Runtime.Serialization.SR.GetString("The value of this argument must be non-negative.")));
			}
			if (count > buffer.Length - offset)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("count", System.Runtime.Serialization.SR.GetString("The specified size exceeds the remaining buffer space ({0} bytes).", new object[]
				{
					buffer.Length - offset
				})));
			}
		}

		// Token: 0x06000282 RID: 642 RVA: 0x0000C184 File Offset: 0x0000A384
		private string GeneratePrefix(string ns, XmlDictionaryString xNs)
		{
			if (this.writeState != WriteState.Element && this.writeState != WriteState.Attribute)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(System.Runtime.Serialization.SR.GetString("A prefix cannot be defined while WriteState is '{0}'.", new object[]
				{
					this.WriteState.ToString()
				})));
			}
			string text = this.nsMgr.AddNamespace(ns, xNs);
			if (text != null)
			{
				return text;
			}
			do
			{
				XmlBaseWriter.Element element = this.elements[this.depth];
				int prefixId = element.PrefixId;
				element.PrefixId = prefixId + 1;
				int num = prefixId;
				text = "d" + this.depth.ToString(CultureInfo.InvariantCulture) + "p" + num.ToString(CultureInfo.InvariantCulture);
			}
			while (this.nsMgr.LookupNamespace(text) != null);
			this.nsMgr.AddNamespace(text, ns, xNs);
			return text;
		}

		// Token: 0x06000283 RID: 643 RVA: 0x0000C24F File Offset: 0x0000A44F
		protected void SignScope(XmlCanonicalWriter signingWriter)
		{
			this.nsMgr.Sign(signingWriter);
		}

		// Token: 0x06000284 RID: 644 RVA: 0x0000C25D File Offset: 0x0000A45D
		private void WriteAttributeText(string value)
		{
			if (this.attributeValue.Length == 0)
			{
				this.attributeValue = value;
				return;
			}
			this.attributeValue += value;
		}

		// Token: 0x06000285 RID: 645 RVA: 0x0000C288 File Offset: 0x0000A488
		// Note: this type is marked as 'beforefieldinit'.
		static XmlBaseWriter()
		{
		}

		// Token: 0x04000107 RID: 263
		private XmlNodeWriter writer;

		// Token: 0x04000108 RID: 264
		private XmlBaseWriter.NamespaceManager nsMgr;

		// Token: 0x04000109 RID: 265
		private XmlBaseWriter.Element[] elements;

		// Token: 0x0400010A RID: 266
		private int depth;

		// Token: 0x0400010B RID: 267
		private string attributeLocalName;

		// Token: 0x0400010C RID: 268
		private string attributeValue;

		// Token: 0x0400010D RID: 269
		private bool isXmlAttribute;

		// Token: 0x0400010E RID: 270
		private bool isXmlnsAttribute;

		// Token: 0x0400010F RID: 271
		private WriteState writeState;

		// Token: 0x04000110 RID: 272
		private XmlBaseWriter.DocumentState documentState;

		// Token: 0x04000111 RID: 273
		private byte[] trailBytes;

		// Token: 0x04000112 RID: 274
		private int trailByteCount;

		// Token: 0x04000113 RID: 275
		private XmlStreamNodeWriter nodeWriter;

		// Token: 0x04000114 RID: 276
		private XmlSigningNodeWriter signingWriter;

		// Token: 0x04000115 RID: 277
		private XmlUTF8NodeWriter textFragmentWriter;

		// Token: 0x04000116 RID: 278
		private XmlNodeWriter oldWriter;

		// Token: 0x04000117 RID: 279
		private Stream oldStream;

		// Token: 0x04000118 RID: 280
		private int oldNamespaceBoundary;

		// Token: 0x04000119 RID: 281
		private bool inList;

		// Token: 0x0400011A RID: 282
		private const string xmlnsNamespace = "http://www.w3.org/2000/xmlns/";

		// Token: 0x0400011B RID: 283
		private const string xmlNamespace = "http://www.w3.org/XML/1998/namespace";

		// Token: 0x0400011C RID: 284
		private static BinHexEncoding binhexEncoding;

		// Token: 0x0400011D RID: 285
		private static string[] prefixes = new string[]
		{
			"a",
			"b",
			"c",
			"d",
			"e",
			"f",
			"g",
			"h",
			"i",
			"j",
			"k",
			"l",
			"m",
			"n",
			"o",
			"p",
			"q",
			"r",
			"s",
			"t",
			"u",
			"v",
			"w",
			"x",
			"y",
			"z"
		};

		// Token: 0x0400011E RID: 286
		private XmlBaseWriter.XmlBaseWriterNodeWriterAsyncHelper nodeWriterAsyncHelper;

		// Token: 0x02000041 RID: 65
		private class WriteBase64AsyncResult : AsyncResult
		{
			// Token: 0x06000286 RID: 646 RVA: 0x0000C384 File Offset: 0x0000A584
			public WriteBase64AsyncResult(byte[] buffer, int offset, int count, XmlBaseWriter writer, AsyncCallback callback, object state) : base(callback, state)
			{
				this.writer = writer;
				this.buffer = buffer;
				this.offset = offset;
				this.count = count;
				bool flag = true;
				if (this.count > 0)
				{
					if (writer.trailByteCount > 0)
					{
						while (writer.trailByteCount < 3 && this.count > 0)
						{
							byte[] trailBytes = writer.trailBytes;
							int trailByteCount = writer.trailByteCount;
							writer.trailByteCount = trailByteCount + 1;
							int num = trailByteCount;
							trailByteCount = this.offset;
							this.offset = trailByteCount + 1;
							trailBytes[num] = buffer[trailByteCount];
							this.count--;
						}
					}
					this.totalByteCount = writer.trailByteCount + this.count;
					this.actualByteCount = this.totalByteCount - this.totalByteCount % 3;
					if (writer.trailBytes == null)
					{
						writer.trailBytes = new byte[3];
					}
					if (this.actualByteCount >= 3)
					{
						if (writer.attributeValue != null)
						{
							writer.WriteAttributeText(XmlConverter.Base64Encoding.GetString(writer.trailBytes, 0, writer.trailByteCount));
							writer.WriteAttributeText(XmlConverter.Base64Encoding.GetString(buffer, this.offset, this.actualByteCount - writer.trailByteCount));
						}
						flag = this.HandleWriteBase64Text(null);
					}
					else
					{
						Buffer.BlockCopy(buffer, this.offset, writer.trailBytes, writer.trailByteCount, this.count);
						writer.trailByteCount += this.count;
					}
				}
				if (flag)
				{
					base.Complete(true);
				}
			}

			// Token: 0x06000287 RID: 647 RVA: 0x0000C4FF File Offset: 0x0000A6FF
			private static bool OnComplete(IAsyncResult result)
			{
				return ((XmlBaseWriter.WriteBase64AsyncResult)result.AsyncState).HandleWriteBase64Text(result);
			}

			// Token: 0x06000288 RID: 648 RVA: 0x0000C514 File Offset: 0x0000A714
			private bool HandleWriteBase64Text(IAsyncResult result)
			{
				if (!this.writer.isXmlnsAttribute)
				{
					if (result == null)
					{
						this.writer.StartContent();
						result = this.writer.writer.BeginWriteBase64Text(this.writer.trailBytes, this.writer.trailByteCount, this.buffer, this.offset, this.actualByteCount - this.writer.trailByteCount, base.PrepareAsyncCompletion(XmlBaseWriter.WriteBase64AsyncResult.onComplete), this);
						if (!result.CompletedSynchronously)
						{
							return false;
						}
					}
					this.writer.writer.EndWriteBase64Text(result);
					this.writer.EndContent();
				}
				this.writer.trailByteCount = this.totalByteCount - this.actualByteCount;
				if (this.writer.trailByteCount > 0)
				{
					int num = this.offset + this.count - this.writer.trailByteCount;
					for (int i = 0; i < this.writer.trailByteCount; i++)
					{
						this.writer.trailBytes[i] = this.buffer[num++];
					}
				}
				return true;
			}

			// Token: 0x06000289 RID: 649 RVA: 0x0000C627 File Offset: 0x0000A827
			public static void End(IAsyncResult result)
			{
				AsyncResult.End<XmlBaseWriter.WriteBase64AsyncResult>(result);
			}

			// Token: 0x0600028A RID: 650 RVA: 0x0000C630 File Offset: 0x0000A830
			// Note: this type is marked as 'beforefieldinit'.
			static WriteBase64AsyncResult()
			{
			}

			// Token: 0x0400011F RID: 287
			private static AsyncResult.AsyncCompletion onComplete = new AsyncResult.AsyncCompletion(XmlBaseWriter.WriteBase64AsyncResult.OnComplete);

			// Token: 0x04000120 RID: 288
			private XmlBaseWriter writer;

			// Token: 0x04000121 RID: 289
			private byte[] buffer;

			// Token: 0x04000122 RID: 290
			private int offset;

			// Token: 0x04000123 RID: 291
			private int count;

			// Token: 0x04000124 RID: 292
			private int actualByteCount;

			// Token: 0x04000125 RID: 293
			private int totalByteCount;
		}

		// Token: 0x02000042 RID: 66
		private class Element
		{
			// Token: 0x1700005C RID: 92
			// (get) Token: 0x0600028B RID: 651 RVA: 0x0000C643 File Offset: 0x0000A843
			// (set) Token: 0x0600028C RID: 652 RVA: 0x0000C64B File Offset: 0x0000A84B
			public string Prefix
			{
				get
				{
					return this.prefix;
				}
				set
				{
					this.prefix = value;
				}
			}

			// Token: 0x1700005D RID: 93
			// (get) Token: 0x0600028D RID: 653 RVA: 0x0000C654 File Offset: 0x0000A854
			// (set) Token: 0x0600028E RID: 654 RVA: 0x0000C65C File Offset: 0x0000A85C
			public string LocalName
			{
				get
				{
					return this.localName;
				}
				set
				{
					this.localName = value;
				}
			}

			// Token: 0x1700005E RID: 94
			// (get) Token: 0x0600028F RID: 655 RVA: 0x0000C665 File Offset: 0x0000A865
			// (set) Token: 0x06000290 RID: 656 RVA: 0x0000C66D File Offset: 0x0000A86D
			public int PrefixId
			{
				get
				{
					return this.prefixId;
				}
				set
				{
					this.prefixId = value;
				}
			}

			// Token: 0x06000291 RID: 657 RVA: 0x0000C676 File Offset: 0x0000A876
			public void Clear()
			{
				this.prefix = null;
				this.localName = null;
				this.prefixId = 0;
			}

			// Token: 0x06000292 RID: 658 RVA: 0x0000222F File Offset: 0x0000042F
			public Element()
			{
			}

			// Token: 0x04000126 RID: 294
			private string prefix;

			// Token: 0x04000127 RID: 295
			private string localName;

			// Token: 0x04000128 RID: 296
			private int prefixId;
		}

		// Token: 0x02000043 RID: 67
		private enum DocumentState : byte
		{
			// Token: 0x0400012A RID: 298
			None,
			// Token: 0x0400012B RID: 299
			Document,
			// Token: 0x0400012C RID: 300
			Epilog,
			// Token: 0x0400012D RID: 301
			End
		}

		// Token: 0x02000044 RID: 68
		private class NamespaceManager
		{
			// Token: 0x06000293 RID: 659 RVA: 0x0000C690 File Offset: 0x0000A890
			public NamespaceManager()
			{
				this.defaultNamespace = new XmlBaseWriter.NamespaceManager.Namespace();
				this.defaultNamespace.Depth = 0;
				this.defaultNamespace.Prefix = string.Empty;
				this.defaultNamespace.Uri = string.Empty;
				this.defaultNamespace.UriDictionaryString = null;
			}

			// Token: 0x1700005F RID: 95
			// (get) Token: 0x06000294 RID: 660 RVA: 0x0000C6E6 File Offset: 0x0000A8E6
			public string XmlLang
			{
				get
				{
					return this.lang;
				}
			}

			// Token: 0x17000060 RID: 96
			// (get) Token: 0x06000295 RID: 661 RVA: 0x0000C6EE File Offset: 0x0000A8EE
			public XmlSpace XmlSpace
			{
				get
				{
					return this.space;
				}
			}

			// Token: 0x06000296 RID: 662 RVA: 0x0000C6F8 File Offset: 0x0000A8F8
			public void Clear()
			{
				if (this.namespaces == null)
				{
					this.namespaces = new XmlBaseWriter.NamespaceManager.Namespace[4];
					this.namespaces[0] = this.defaultNamespace;
				}
				this.nsCount = 1;
				this.nsTop = 0;
				this.depth = 0;
				this.attributeCount = 0;
				this.space = XmlSpace.None;
				this.lang = null;
				this.lastNameSpace = null;
				this.namespaceBoundary = 0;
			}

			// Token: 0x17000061 RID: 97
			// (get) Token: 0x06000297 RID: 663 RVA: 0x0000C75F File Offset: 0x0000A95F
			// (set) Token: 0x06000298 RID: 664 RVA: 0x0000C768 File Offset: 0x0000A968
			public int NamespaceBoundary
			{
				get
				{
					return this.namespaceBoundary;
				}
				set
				{
					int num = 0;
					while (num < this.nsCount && this.namespaces[num].Depth < value)
					{
						num++;
					}
					this.nsTop = num;
					this.namespaceBoundary = value;
					this.lastNameSpace = null;
				}
			}

			// Token: 0x06000299 RID: 665 RVA: 0x0000C7AC File Offset: 0x0000A9AC
			public void Close()
			{
				if (this.depth == 0)
				{
					if (this.namespaces != null && this.namespaces.Length > 32)
					{
						this.namespaces = null;
					}
					if (this.attributes != null && this.attributes.Length > 4)
					{
						this.attributes = null;
					}
				}
				else
				{
					this.namespaces = null;
					this.attributes = null;
				}
				this.lang = null;
			}

			// Token: 0x0600029A RID: 666 RVA: 0x0000C810 File Offset: 0x0000AA10
			public void DeclareNamespaces(XmlNodeWriter writer)
			{
				for (int i = this.nsCount; i > 0; i--)
				{
					if (this.namespaces[i - 1].Depth != this.depth)
					{
						IL_65:
						while (i < this.nsCount)
						{
							XmlBaseWriter.NamespaceManager.Namespace @namespace = this.namespaces[i];
							if (@namespace.UriDictionaryString != null)
							{
								writer.WriteXmlnsAttribute(@namespace.Prefix, @namespace.UriDictionaryString);
							}
							else
							{
								writer.WriteXmlnsAttribute(@namespace.Prefix, @namespace.Uri);
							}
							i++;
						}
						return;
					}
				}
				goto IL_65;
			}

			// Token: 0x0600029B RID: 667 RVA: 0x0000C88B File Offset: 0x0000AA8B
			public void EnterScope()
			{
				this.depth++;
			}

			// Token: 0x0600029C RID: 668 RVA: 0x0000C89C File Offset: 0x0000AA9C
			public void ExitScope()
			{
				while (this.nsCount > 0)
				{
					XmlBaseWriter.NamespaceManager.Namespace @namespace = this.namespaces[this.nsCount - 1];
					if (@namespace.Depth != this.depth)
					{
						IL_99:
						while (this.attributeCount > 0)
						{
							XmlBaseWriter.NamespaceManager.XmlAttribute xmlAttribute = this.attributes[this.attributeCount - 1];
							if (xmlAttribute.Depth != this.depth)
							{
								break;
							}
							this.space = xmlAttribute.XmlSpace;
							this.lang = xmlAttribute.XmlLang;
							xmlAttribute.Clear();
							this.attributeCount--;
						}
						this.depth--;
						return;
					}
					if (this.lastNameSpace == @namespace)
					{
						this.lastNameSpace = null;
					}
					@namespace.Clear();
					this.nsCount--;
				}
				goto IL_99;
			}

			// Token: 0x0600029D RID: 669 RVA: 0x0000C959 File Offset: 0x0000AB59
			public void AddLangAttribute(string lang)
			{
				this.AddAttribute();
				this.lang = lang;
			}

			// Token: 0x0600029E RID: 670 RVA: 0x0000C968 File Offset: 0x0000AB68
			public void AddSpaceAttribute(XmlSpace space)
			{
				this.AddAttribute();
				this.space = space;
			}

			// Token: 0x0600029F RID: 671 RVA: 0x0000C978 File Offset: 0x0000AB78
			private void AddAttribute()
			{
				if (this.attributes == null)
				{
					this.attributes = new XmlBaseWriter.NamespaceManager.XmlAttribute[1];
				}
				else if (this.attributes.Length == this.attributeCount)
				{
					XmlBaseWriter.NamespaceManager.XmlAttribute[] destinationArray = new XmlBaseWriter.NamespaceManager.XmlAttribute[this.attributeCount * 2];
					Array.Copy(this.attributes, destinationArray, this.attributeCount);
					this.attributes = destinationArray;
				}
				XmlBaseWriter.NamespaceManager.XmlAttribute xmlAttribute = this.attributes[this.attributeCount];
				if (xmlAttribute == null)
				{
					xmlAttribute = new XmlBaseWriter.NamespaceManager.XmlAttribute();
					this.attributes[this.attributeCount] = xmlAttribute;
				}
				xmlAttribute.XmlLang = this.lang;
				xmlAttribute.XmlSpace = this.space;
				xmlAttribute.Depth = this.depth;
				this.attributeCount++;
			}

			// Token: 0x060002A0 RID: 672 RVA: 0x0000CA2C File Offset: 0x0000AC2C
			public string AddNamespace(string uri, XmlDictionaryString uriDictionaryString)
			{
				if (uri.Length == 0)
				{
					this.AddNamespaceIfNotDeclared(string.Empty, uri, uriDictionaryString);
					return string.Empty;
				}
				for (int i = 0; i < XmlBaseWriter.prefixes.Length; i++)
				{
					string text = XmlBaseWriter.prefixes[i];
					bool flag = false;
					for (int j = this.nsCount - 1; j >= this.nsTop; j--)
					{
						if (this.namespaces[j].Prefix == text)
						{
							flag = true;
							break;
						}
					}
					if (!flag)
					{
						this.AddNamespace(text, uri, uriDictionaryString);
						return text;
					}
				}
				return null;
			}

			// Token: 0x060002A1 RID: 673 RVA: 0x0000CAB0 File Offset: 0x0000ACB0
			public void AddNamespaceIfNotDeclared(string prefix, string uri, XmlDictionaryString uriDictionaryString)
			{
				if (this.LookupNamespace(prefix) != uri)
				{
					this.AddNamespace(prefix, uri, uriDictionaryString);
				}
			}

			// Token: 0x060002A2 RID: 674 RVA: 0x0000CACC File Offset: 0x0000ACCC
			public void AddNamespace(string prefix, string uri, XmlDictionaryString uriDictionaryString)
			{
				if (prefix.Length >= 3 && ((int)prefix[0] & -33) == 88 && ((int)prefix[1] & -33) == 77 && ((int)prefix[2] & -33) == 76)
				{
					if (prefix == "xml" && uri == "http://www.w3.org/XML/1998/namespace")
					{
						return;
					}
					if (prefix == "xmlns" && uri == "http://www.w3.org/2000/xmlns/")
					{
						return;
					}
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentException(System.Runtime.Serialization.SR.GetString("Prefixes beginning with \"xml\" (regardless of casing) are reserved for use by XML."), "prefix"));
				}
				else
				{
					int i = this.nsCount - 1;
					XmlBaseWriter.NamespaceManager.Namespace @namespace;
					while (i >= 0)
					{
						@namespace = this.namespaces[i];
						if (@namespace.Depth != this.depth)
						{
							break;
						}
						if (@namespace.Prefix == prefix)
						{
							if (@namespace.Uri == uri)
							{
								return;
							}
							throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentException(System.Runtime.Serialization.SR.GetString("The prefix '{0}' is bound to the namespace '{1}' and cannot be changed to '{2}'.", new object[]
							{
								prefix,
								@namespace.Uri,
								uri
							}), "prefix"));
						}
						else
						{
							i--;
						}
					}
					if (prefix.Length != 0 && uri.Length == 0)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentException(System.Runtime.Serialization.SR.GetString("The empty namespace requires a null or empty prefix."), "prefix"));
					}
					if (uri.Length == "http://www.w3.org/2000/xmlns/".Length && uri == "http://www.w3.org/2000/xmlns/")
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentException(System.Runtime.Serialization.SR.GetString("The namespace '{1}' can only be bound to the prefix '{0}'.", new object[]
						{
							"xmlns",
							uri
						})));
					}
					if (uri.Length == "http://www.w3.org/XML/1998/namespace".Length && uri[18] == 'X' && uri == "http://www.w3.org/XML/1998/namespace")
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentException(System.Runtime.Serialization.SR.GetString("The namespace '{1}' can only be bound to the prefix '{0}'.", new object[]
						{
							"xml",
							uri
						})));
					}
					if (this.namespaces.Length == this.nsCount)
					{
						XmlBaseWriter.NamespaceManager.Namespace[] destinationArray = new XmlBaseWriter.NamespaceManager.Namespace[this.nsCount * 2];
						Array.Copy(this.namespaces, destinationArray, this.nsCount);
						this.namespaces = destinationArray;
					}
					@namespace = this.namespaces[this.nsCount];
					if (@namespace == null)
					{
						@namespace = new XmlBaseWriter.NamespaceManager.Namespace();
						this.namespaces[this.nsCount] = @namespace;
					}
					@namespace.Depth = this.depth;
					@namespace.Prefix = prefix;
					@namespace.Uri = uri;
					@namespace.UriDictionaryString = uriDictionaryString;
					this.nsCount++;
					this.lastNameSpace = null;
					return;
				}
			}

			// Token: 0x060002A3 RID: 675 RVA: 0x0000CD28 File Offset: 0x0000AF28
			public string LookupPrefix(string ns)
			{
				if (this.lastNameSpace != null && this.lastNameSpace.Uri == ns)
				{
					return this.lastNameSpace.Prefix;
				}
				int num = this.nsCount;
				for (int i = num - 1; i >= this.nsTop; i--)
				{
					XmlBaseWriter.NamespaceManager.Namespace @namespace = this.namespaces[i];
					if (@namespace.Uri == ns)
					{
						string prefix = @namespace.Prefix;
						bool flag = false;
						for (int j = i + 1; j < num; j++)
						{
							if (this.namespaces[j].Prefix == prefix)
							{
								flag = true;
								break;
							}
						}
						if (!flag)
						{
							this.lastNameSpace = @namespace;
							return prefix;
						}
					}
				}
				for (int k = num - 1; k >= this.nsTop; k--)
				{
					XmlBaseWriter.NamespaceManager.Namespace namespace2 = this.namespaces[k];
					if (namespace2.Uri == ns)
					{
						string prefix2 = namespace2.Prefix;
						bool flag2 = false;
						for (int l = k + 1; l < num; l++)
						{
							if (this.namespaces[l].Prefix == prefix2)
							{
								flag2 = true;
								break;
							}
						}
						if (!flag2)
						{
							this.lastNameSpace = namespace2;
							return prefix2;
						}
					}
				}
				if (ns.Length == 0)
				{
					bool flag3 = true;
					for (int m = num - 1; m >= this.nsTop; m--)
					{
						if (this.namespaces[m].Prefix.Length == 0)
						{
							flag3 = false;
							break;
						}
					}
					if (flag3)
					{
						return string.Empty;
					}
				}
				if (ns == "http://www.w3.org/2000/xmlns/")
				{
					return "xmlns";
				}
				if (ns == "http://www.w3.org/XML/1998/namespace")
				{
					return "xml";
				}
				return null;
			}

			// Token: 0x060002A4 RID: 676 RVA: 0x0000CEB4 File Offset: 0x0000B0B4
			public string LookupAttributePrefix(string ns)
			{
				if (this.lastNameSpace != null && this.lastNameSpace.Uri == ns && this.lastNameSpace.Prefix.Length != 0)
				{
					return this.lastNameSpace.Prefix;
				}
				int num = this.nsCount;
				for (int i = num - 1; i >= this.nsTop; i--)
				{
					XmlBaseWriter.NamespaceManager.Namespace @namespace = this.namespaces[i];
					if (@namespace.Uri == ns)
					{
						string prefix = @namespace.Prefix;
						if (prefix.Length != 0)
						{
							bool flag = false;
							for (int j = i + 1; j < num; j++)
							{
								if (this.namespaces[j].Prefix == prefix)
								{
									flag = true;
									break;
								}
							}
							if (!flag)
							{
								this.lastNameSpace = @namespace;
								return prefix;
							}
						}
					}
				}
				for (int k = num - 1; k >= this.nsTop; k--)
				{
					XmlBaseWriter.NamespaceManager.Namespace namespace2 = this.namespaces[k];
					if (namespace2.Uri == ns)
					{
						string prefix2 = namespace2.Prefix;
						if (prefix2.Length != 0)
						{
							bool flag2 = false;
							for (int l = k + 1; l < num; l++)
							{
								if (this.namespaces[l].Prefix == prefix2)
								{
									flag2 = true;
									break;
								}
							}
							if (!flag2)
							{
								this.lastNameSpace = namespace2;
								return prefix2;
							}
						}
					}
				}
				if (ns.Length == 0)
				{
					return string.Empty;
				}
				return null;
			}

			// Token: 0x060002A5 RID: 677 RVA: 0x0000D008 File Offset: 0x0000B208
			public string LookupNamespace(string prefix)
			{
				int num = this.nsCount;
				if (prefix.Length == 0)
				{
					for (int i = num - 1; i >= this.nsTop; i--)
					{
						XmlBaseWriter.NamespaceManager.Namespace @namespace = this.namespaces[i];
						if (@namespace.Prefix.Length == 0)
						{
							return @namespace.Uri;
						}
					}
					return string.Empty;
				}
				if (prefix.Length == 1)
				{
					char c = prefix[0];
					for (int j = num - 1; j >= this.nsTop; j--)
					{
						XmlBaseWriter.NamespaceManager.Namespace namespace2 = this.namespaces[j];
						if (namespace2.PrefixChar == c)
						{
							return namespace2.Uri;
						}
					}
					return null;
				}
				for (int k = num - 1; k >= this.nsTop; k--)
				{
					XmlBaseWriter.NamespaceManager.Namespace namespace3 = this.namespaces[k];
					if (namespace3.Prefix == prefix)
					{
						return namespace3.Uri;
					}
				}
				if (prefix == "xmlns")
				{
					return "http://www.w3.org/2000/xmlns/";
				}
				if (prefix == "xml")
				{
					return "http://www.w3.org/XML/1998/namespace";
				}
				return null;
			}

			// Token: 0x060002A6 RID: 678 RVA: 0x0000D104 File Offset: 0x0000B304
			public void Sign(XmlCanonicalWriter signingWriter)
			{
				int num = this.nsCount;
				for (int i = 1; i < num; i++)
				{
					XmlBaseWriter.NamespaceManager.Namespace @namespace = this.namespaces[i];
					bool flag = false;
					int num2 = i + 1;
					while (num2 < num && !flag)
					{
						flag = (@namespace.Prefix == this.namespaces[num2].Prefix);
						num2++;
					}
					if (!flag)
					{
						signingWriter.WriteXmlnsAttribute(@namespace.Prefix, @namespace.Uri);
					}
				}
			}

			// Token: 0x0400012E RID: 302
			private XmlBaseWriter.NamespaceManager.Namespace[] namespaces;

			// Token: 0x0400012F RID: 303
			private XmlBaseWriter.NamespaceManager.Namespace lastNameSpace;

			// Token: 0x04000130 RID: 304
			private int nsCount;

			// Token: 0x04000131 RID: 305
			private int depth;

			// Token: 0x04000132 RID: 306
			private XmlBaseWriter.NamespaceManager.XmlAttribute[] attributes;

			// Token: 0x04000133 RID: 307
			private int attributeCount;

			// Token: 0x04000134 RID: 308
			private XmlSpace space;

			// Token: 0x04000135 RID: 309
			private string lang;

			// Token: 0x04000136 RID: 310
			private int namespaceBoundary;

			// Token: 0x04000137 RID: 311
			private int nsTop;

			// Token: 0x04000138 RID: 312
			private XmlBaseWriter.NamespaceManager.Namespace defaultNamespace;

			// Token: 0x02000045 RID: 69
			private class XmlAttribute
			{
				// Token: 0x060002A7 RID: 679 RVA: 0x0000222F File Offset: 0x0000042F
				public XmlAttribute()
				{
				}

				// Token: 0x17000062 RID: 98
				// (get) Token: 0x060002A8 RID: 680 RVA: 0x0000D173 File Offset: 0x0000B373
				// (set) Token: 0x060002A9 RID: 681 RVA: 0x0000D17B File Offset: 0x0000B37B
				public int Depth
				{
					get
					{
						return this.depth;
					}
					set
					{
						this.depth = value;
					}
				}

				// Token: 0x17000063 RID: 99
				// (get) Token: 0x060002AA RID: 682 RVA: 0x0000D184 File Offset: 0x0000B384
				// (set) Token: 0x060002AB RID: 683 RVA: 0x0000D18C File Offset: 0x0000B38C
				public string XmlLang
				{
					get
					{
						return this.lang;
					}
					set
					{
						this.lang = value;
					}
				}

				// Token: 0x17000064 RID: 100
				// (get) Token: 0x060002AC RID: 684 RVA: 0x0000D195 File Offset: 0x0000B395
				// (set) Token: 0x060002AD RID: 685 RVA: 0x0000D19D File Offset: 0x0000B39D
				public XmlSpace XmlSpace
				{
					get
					{
						return this.space;
					}
					set
					{
						this.space = value;
					}
				}

				// Token: 0x060002AE RID: 686 RVA: 0x0000D1A6 File Offset: 0x0000B3A6
				public void Clear()
				{
					this.lang = null;
				}

				// Token: 0x04000139 RID: 313
				private XmlSpace space;

				// Token: 0x0400013A RID: 314
				private string lang;

				// Token: 0x0400013B RID: 315
				private int depth;
			}

			// Token: 0x02000046 RID: 70
			private class Namespace
			{
				// Token: 0x060002AF RID: 687 RVA: 0x0000222F File Offset: 0x0000042F
				public Namespace()
				{
				}

				// Token: 0x060002B0 RID: 688 RVA: 0x0000D1AF File Offset: 0x0000B3AF
				public void Clear()
				{
					this.prefix = null;
					this.prefixChar = '\0';
					this.ns = null;
					this.xNs = null;
					this.depth = 0;
				}

				// Token: 0x17000065 RID: 101
				// (get) Token: 0x060002B1 RID: 689 RVA: 0x0000D1D4 File Offset: 0x0000B3D4
				// (set) Token: 0x060002B2 RID: 690 RVA: 0x0000D1DC File Offset: 0x0000B3DC
				public int Depth
				{
					get
					{
						return this.depth;
					}
					set
					{
						this.depth = value;
					}
				}

				// Token: 0x17000066 RID: 102
				// (get) Token: 0x060002B3 RID: 691 RVA: 0x0000D1E5 File Offset: 0x0000B3E5
				public char PrefixChar
				{
					get
					{
						return this.prefixChar;
					}
				}

				// Token: 0x17000067 RID: 103
				// (get) Token: 0x060002B4 RID: 692 RVA: 0x0000D1ED File Offset: 0x0000B3ED
				// (set) Token: 0x060002B5 RID: 693 RVA: 0x0000D1F5 File Offset: 0x0000B3F5
				public string Prefix
				{
					get
					{
						return this.prefix;
					}
					set
					{
						if (value.Length == 1)
						{
							this.prefixChar = value[0];
						}
						else
						{
							this.prefixChar = '\0';
						}
						this.prefix = value;
					}
				}

				// Token: 0x17000068 RID: 104
				// (get) Token: 0x060002B6 RID: 694 RVA: 0x0000D21D File Offset: 0x0000B41D
				// (set) Token: 0x060002B7 RID: 695 RVA: 0x0000D225 File Offset: 0x0000B425
				public string Uri
				{
					get
					{
						return this.ns;
					}
					set
					{
						this.ns = value;
					}
				}

				// Token: 0x17000069 RID: 105
				// (get) Token: 0x060002B8 RID: 696 RVA: 0x0000D22E File Offset: 0x0000B42E
				// (set) Token: 0x060002B9 RID: 697 RVA: 0x0000D236 File Offset: 0x0000B436
				public XmlDictionaryString UriDictionaryString
				{
					get
					{
						return this.xNs;
					}
					set
					{
						this.xNs = value;
					}
				}

				// Token: 0x0400013C RID: 316
				private string prefix;

				// Token: 0x0400013D RID: 317
				private string ns;

				// Token: 0x0400013E RID: 318
				private XmlDictionaryString xNs;

				// Token: 0x0400013F RID: 319
				private int depth;

				// Token: 0x04000140 RID: 320
				private char prefixChar;
			}
		}

		// Token: 0x02000047 RID: 71
		private class XmlBaseWriterNodeWriterAsyncHelper
		{
			// Token: 0x060002BA RID: 698 RVA: 0x0000D23F File Offset: 0x0000B43F
			public XmlBaseWriterNodeWriterAsyncHelper(XmlBaseWriter writer)
			{
				this.writer = writer;
			}

			// Token: 0x060002BB RID: 699 RVA: 0x0000D24E File Offset: 0x0000B44E
			public void SetArguments(AsyncEventArgs<XmlWriteBase64AsyncArguments> inputState)
			{
				this.inputState = inputState;
				this.buffer = inputState.Arguments.Buffer;
				this.offset = inputState.Arguments.Offset;
				this.count = inputState.Arguments.Count;
			}

			// Token: 0x060002BC RID: 700 RVA: 0x0000D28C File Offset: 0x0000B48C
			public AsyncCompletionResult StartAsync()
			{
				bool flag = true;
				if (this.count > 0)
				{
					if (this.writer.trailByteCount > 0)
					{
						while (this.writer.trailByteCount < 3 && this.count > 0)
						{
							byte[] trailBytes = this.writer.trailBytes;
							XmlBaseWriter xmlBaseWriter = this.writer;
							int trailByteCount = xmlBaseWriter.trailByteCount;
							xmlBaseWriter.trailByteCount = trailByteCount + 1;
							int num = trailByteCount;
							byte[] array = this.buffer;
							trailByteCount = this.offset;
							this.offset = trailByteCount + 1;
							trailBytes[num] = array[trailByteCount];
							this.count--;
						}
					}
					this.totalByteCount = this.writer.trailByteCount + this.count;
					this.actualByteCount = this.totalByteCount - this.totalByteCount % 3;
					if (this.writer.trailBytes == null)
					{
						this.writer.trailBytes = new byte[3];
					}
					if (this.actualByteCount >= 3)
					{
						if (this.writer.attributeValue != null)
						{
							this.writer.WriteAttributeText(XmlConverter.Base64Encoding.GetString(this.writer.trailBytes, 0, this.writer.trailByteCount));
							this.writer.WriteAttributeText(XmlConverter.Base64Encoding.GetString(this.buffer, this.offset, this.actualByteCount - this.writer.trailByteCount));
						}
						flag = this.HandleWriteBase64Text(false);
					}
					else
					{
						Buffer.BlockCopy(this.buffer, this.offset, this.writer.trailBytes, this.writer.trailByteCount, this.count);
						this.writer.trailByteCount += this.count;
					}
				}
				if (flag)
				{
					this.Clear();
					return AsyncCompletionResult.Completed;
				}
				return AsyncCompletionResult.Queued;
			}

			// Token: 0x060002BD RID: 701 RVA: 0x0000D434 File Offset: 0x0000B634
			private static void OnWriteComplete(IAsyncEventArgs asyncEventArgs)
			{
				bool flag = false;
				Exception exception = null;
				XmlBaseWriter.XmlBaseWriterNodeWriterAsyncHelper xmlBaseWriterNodeWriterAsyncHelper = (XmlBaseWriter.XmlBaseWriterNodeWriterAsyncHelper)asyncEventArgs.AsyncState;
				AsyncEventArgs<XmlWriteBase64AsyncArguments> asyncEventArgs2 = xmlBaseWriterNodeWriterAsyncHelper.inputState;
				try
				{
					if (asyncEventArgs.Exception != null)
					{
						exception = asyncEventArgs.Exception;
						flag = true;
					}
					else
					{
						flag = xmlBaseWriterNodeWriterAsyncHelper.HandleWriteBase64Text(true);
					}
				}
				catch (Exception ex)
				{
					if (Fx.IsFatal(ex))
					{
						throw;
					}
					exception = ex;
					flag = true;
				}
				if (flag)
				{
					xmlBaseWriterNodeWriterAsyncHelper.Clear();
					asyncEventArgs2.Complete(false, exception);
				}
			}

			// Token: 0x060002BE RID: 702 RVA: 0x0000D4A8 File Offset: 0x0000B6A8
			private bool HandleWriteBase64Text(bool isAsyncCallback)
			{
				if (!this.writer.isXmlnsAttribute)
				{
					if (!isAsyncCallback)
					{
						if (this.nodeWriterAsyncState == null)
						{
							this.nodeWriterAsyncState = new AsyncEventArgs<XmlNodeWriterWriteBase64TextArgs>();
							this.nodeWriterArgs = new XmlNodeWriterWriteBase64TextArgs();
						}
						if (XmlBaseWriter.XmlBaseWriterNodeWriterAsyncHelper.onWriteComplete == null)
						{
							XmlBaseWriter.XmlBaseWriterNodeWriterAsyncHelper.onWriteComplete = new AsyncEventArgsCallback(XmlBaseWriter.XmlBaseWriterNodeWriterAsyncHelper.OnWriteComplete);
						}
						this.writer.StartContent();
						this.nodeWriterArgs.TrailBuffer = this.writer.trailBytes;
						this.nodeWriterArgs.TrailCount = this.writer.trailByteCount;
						this.nodeWriterArgs.Buffer = this.buffer;
						this.nodeWriterArgs.Offset = this.offset;
						this.nodeWriterArgs.Count = this.actualByteCount - this.writer.trailByteCount;
						this.nodeWriterAsyncState.Set(XmlBaseWriter.XmlBaseWriterNodeWriterAsyncHelper.onWriteComplete, this.nodeWriterArgs, this);
						if (this.writer.writer.WriteBase64TextAsync(this.nodeWriterAsyncState) != AsyncCompletionResult.Completed)
						{
							return false;
						}
						this.nodeWriterAsyncState.Complete(true);
					}
					this.writer.EndContent();
				}
				this.writer.trailByteCount = this.totalByteCount - this.actualByteCount;
				if (this.writer.trailByteCount > 0)
				{
					int num = this.offset + this.count - this.writer.trailByteCount;
					for (int i = 0; i < this.writer.trailByteCount; i++)
					{
						this.writer.trailBytes[i] = this.buffer[num++];
					}
				}
				return true;
			}

			// Token: 0x060002BF RID: 703 RVA: 0x0000D630 File Offset: 0x0000B830
			private void Clear()
			{
				this.inputState = null;
				this.buffer = null;
				this.offset = 0;
				this.count = 0;
				this.actualByteCount = 0;
				this.totalByteCount = 0;
			}

			// Token: 0x04000141 RID: 321
			private static AsyncEventArgsCallback onWriteComplete;

			// Token: 0x04000142 RID: 322
			private XmlBaseWriter writer;

			// Token: 0x04000143 RID: 323
			private byte[] buffer;

			// Token: 0x04000144 RID: 324
			private int offset;

			// Token: 0x04000145 RID: 325
			private int count;

			// Token: 0x04000146 RID: 326
			private int actualByteCount;

			// Token: 0x04000147 RID: 327
			private int totalByteCount;

			// Token: 0x04000148 RID: 328
			private AsyncEventArgs<XmlNodeWriterWriteBase64TextArgs> nodeWriterAsyncState;

			// Token: 0x04000149 RID: 329
			private XmlNodeWriterWriteBase64TextArgs nodeWriterArgs;

			// Token: 0x0400014A RID: 330
			private AsyncEventArgs<XmlWriteBase64AsyncArguments> inputState;
		}
	}
}

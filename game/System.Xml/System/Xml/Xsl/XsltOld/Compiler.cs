using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Security;
using System.Security.Permissions;
using System.Security.Policy;
using System.Text;
using System.Threading;
using System.Xml.XPath;
using System.Xml.Xsl.Runtime;
using System.Xml.Xsl.Xslt;
using System.Xml.Xsl.XsltOld.Debugger;
using Microsoft.CSharp;
using Microsoft.VisualBasic;
using MS.Internal.Xml.XPath;

namespace System.Xml.Xsl.XsltOld
{
	// Token: 0x02000360 RID: 864
	internal class Compiler
	{
		// Token: 0x1700070C RID: 1804
		// (get) Token: 0x0600237B RID: 9083 RVA: 0x000DC2FA File Offset: 0x000DA4FA
		internal KeywordsTable Atoms
		{
			get
			{
				return this.atoms;
			}
		}

		// Token: 0x1700070D RID: 1805
		// (get) Token: 0x0600237C RID: 9084 RVA: 0x000DC302 File Offset: 0x000DA502
		// (set) Token: 0x0600237D RID: 9085 RVA: 0x000DC30A File Offset: 0x000DA50A
		internal int Stylesheetid
		{
			get
			{
				return this.stylesheetid;
			}
			set
			{
				this.stylesheetid = value;
			}
		}

		// Token: 0x1700070E RID: 1806
		// (get) Token: 0x0600237E RID: 9086 RVA: 0x000DC313 File Offset: 0x000DA513
		internal NavigatorInput Document
		{
			get
			{
				return this.input;
			}
		}

		// Token: 0x1700070F RID: 1807
		// (get) Token: 0x0600237F RID: 9087 RVA: 0x000DC313 File Offset: 0x000DA513
		internal NavigatorInput Input
		{
			get
			{
				return this.input;
			}
		}

		// Token: 0x06002380 RID: 9088 RVA: 0x000DC31B File Offset: 0x000DA51B
		internal bool Advance()
		{
			return this.Document.Advance();
		}

		// Token: 0x06002381 RID: 9089 RVA: 0x000DC328 File Offset: 0x000DA528
		internal bool Recurse()
		{
			return this.Document.Recurse();
		}

		// Token: 0x06002382 RID: 9090 RVA: 0x000DC335 File Offset: 0x000DA535
		internal bool ToParent()
		{
			return this.Document.ToParent();
		}

		// Token: 0x17000710 RID: 1808
		// (get) Token: 0x06002383 RID: 9091 RVA: 0x000DC342 File Offset: 0x000DA542
		internal Stylesheet CompiledStylesheet
		{
			get
			{
				return this.stylesheet;
			}
		}

		// Token: 0x17000711 RID: 1809
		// (get) Token: 0x06002384 RID: 9092 RVA: 0x000DC34A File Offset: 0x000DA54A
		// (set) Token: 0x06002385 RID: 9093 RVA: 0x000DC352 File Offset: 0x000DA552
		internal RootAction RootAction
		{
			get
			{
				return this.rootAction;
			}
			set
			{
				this.rootAction = value;
				this.currentTemplate = this.rootAction;
			}
		}

		// Token: 0x17000712 RID: 1810
		// (get) Token: 0x06002386 RID: 9094 RVA: 0x000DC367 File Offset: 0x000DA567
		internal List<TheQuery> QueryStore
		{
			get
			{
				return this.queryStore;
			}
		}

		// Token: 0x17000713 RID: 1811
		// (get) Token: 0x06002387 RID: 9095 RVA: 0x0001DA42 File Offset: 0x0001BC42
		public virtual IXsltDebugger Debugger
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06002388 RID: 9096 RVA: 0x000DC36F File Offset: 0x000DA56F
		internal string GetUnicRtfId()
		{
			this.rtfCount++;
			return this.rtfCount.ToString(CultureInfo.InvariantCulture);
		}

		// Token: 0x06002389 RID: 9097 RVA: 0x000DC390 File Offset: 0x000DA590
		internal void Compile(NavigatorInput input, XmlResolver xmlResolver, Evidence evidence)
		{
			evidence = null;
			this.xmlResolver = xmlResolver;
			this.PushInputDocument(input);
			this.rootScope = this.scopeManager.PushScope();
			this.queryStore = new List<TheQuery>();
			try
			{
				this.rootStylesheet = new Stylesheet();
				this.PushStylesheet(this.rootStylesheet);
				try
				{
					this.CreateRootAction();
				}
				catch (XsltCompileException)
				{
					throw;
				}
				catch (Exception inner)
				{
					throw new XsltCompileException(inner, this.Input.BaseURI, this.Input.LineNumber, this.Input.LinePosition);
				}
				this.stylesheet.ProcessTemplates();
				this.rootAction.PorcessAttributeSets(this.rootStylesheet);
				this.stylesheet.SortWhiteSpace();
				this.CompileScript(evidence);
				if (evidence != null)
				{
					this.rootAction.permissions = SecurityManager.GetStandardSandbox(evidence);
				}
				if (this.globalNamespaceAliasTable != null)
				{
					this.stylesheet.ReplaceNamespaceAlias(this);
					this.rootAction.ReplaceNamespaceAlias(this);
				}
			}
			finally
			{
				this.PopInputDocument();
			}
		}

		// Token: 0x17000714 RID: 1812
		// (get) Token: 0x0600238A RID: 9098 RVA: 0x000DC4A8 File Offset: 0x000DA6A8
		// (set) Token: 0x0600238B RID: 9099 RVA: 0x000DC4BA File Offset: 0x000DA6BA
		internal bool ForwardCompatibility
		{
			get
			{
				return this.scopeManager.CurrentScope.ForwardCompatibility;
			}
			set
			{
				this.scopeManager.CurrentScope.ForwardCompatibility = value;
			}
		}

		// Token: 0x17000715 RID: 1813
		// (get) Token: 0x0600238C RID: 9100 RVA: 0x000DC4CD File Offset: 0x000DA6CD
		// (set) Token: 0x0600238D RID: 9101 RVA: 0x000DC4DF File Offset: 0x000DA6DF
		internal bool CanHaveApplyImports
		{
			get
			{
				return this.scopeManager.CurrentScope.CanHaveApplyImports;
			}
			set
			{
				this.scopeManager.CurrentScope.CanHaveApplyImports = value;
			}
		}

		// Token: 0x0600238E RID: 9102 RVA: 0x000DC4F4 File Offset: 0x000DA6F4
		internal void InsertExtensionNamespace(string value)
		{
			string[] array = this.ResolvePrefixes(value);
			if (array != null)
			{
				this.scopeManager.InsertExtensionNamespaces(array);
			}
		}

		// Token: 0x0600238F RID: 9103 RVA: 0x000DC518 File Offset: 0x000DA718
		internal void InsertExcludedNamespace(string value)
		{
			string[] array = this.ResolvePrefixes(value);
			if (array != null)
			{
				this.scopeManager.InsertExcludedNamespaces(array);
			}
		}

		// Token: 0x06002390 RID: 9104 RVA: 0x000DC53C File Offset: 0x000DA73C
		internal void InsertExtensionNamespace()
		{
			this.InsertExtensionNamespace(this.Input.Navigator.GetAttribute(this.Input.Atoms.ExtensionElementPrefixes, this.Input.Atoms.UriXsl));
		}

		// Token: 0x06002391 RID: 9105 RVA: 0x000DC574 File Offset: 0x000DA774
		internal void InsertExcludedNamespace()
		{
			this.InsertExcludedNamespace(this.Input.Navigator.GetAttribute(this.Input.Atoms.ExcludeResultPrefixes, this.Input.Atoms.UriXsl));
		}

		// Token: 0x06002392 RID: 9106 RVA: 0x000DC5AC File Offset: 0x000DA7AC
		internal bool IsExtensionNamespace(string nspace)
		{
			return this.scopeManager.IsExtensionNamespace(nspace);
		}

		// Token: 0x06002393 RID: 9107 RVA: 0x000DC5BA File Offset: 0x000DA7BA
		internal bool IsExcludedNamespace(string nspace)
		{
			return this.scopeManager.IsExcludedNamespace(nspace);
		}

		// Token: 0x06002394 RID: 9108 RVA: 0x000DC5C8 File Offset: 0x000DA7C8
		internal void PushLiteralScope()
		{
			this.PushNamespaceScope();
			string attribute = this.Input.Navigator.GetAttribute(this.Atoms.Version, this.Atoms.UriXsl);
			if (attribute.Length != 0)
			{
				this.ForwardCompatibility = (attribute != "1.0");
			}
		}

		// Token: 0x06002395 RID: 9109 RVA: 0x000DC61C File Offset: 0x000DA81C
		internal void PushNamespaceScope()
		{
			this.scopeManager.PushScope();
			NavigatorInput navigatorInput = this.Input;
			if (navigatorInput.MoveToFirstNamespace())
			{
				do
				{
					this.scopeManager.PushNamespace(navigatorInput.LocalName, navigatorInput.Value);
				}
				while (navigatorInput.MoveToNextNamespace());
				navigatorInput.ToParent();
			}
		}

		// Token: 0x17000716 RID: 1814
		// (get) Token: 0x06002396 RID: 9110 RVA: 0x000DC66A File Offset: 0x000DA86A
		protected InputScopeManager ScopeManager
		{
			get
			{
				return this.scopeManager;
			}
		}

		// Token: 0x06002397 RID: 9111 RVA: 0x000DC672 File Offset: 0x000DA872
		internal virtual void PopScope()
		{
			this.currentTemplate.ReleaseVariableSlots(this.scopeManager.CurrentScope.GetVeriablesCount());
			this.scopeManager.PopScope();
		}

		// Token: 0x06002398 RID: 9112 RVA: 0x000DC69A File Offset: 0x000DA89A
		internal InputScopeManager CloneScopeManager()
		{
			return this.scopeManager.Clone();
		}

		// Token: 0x06002399 RID: 9113 RVA: 0x000DC6A8 File Offset: 0x000DA8A8
		internal int InsertVariable(VariableAction variable)
		{
			InputScope variableScope;
			if (variable.IsGlobal)
			{
				variableScope = this.rootScope;
			}
			else
			{
				variableScope = this.scopeManager.VariableScope;
			}
			VariableAction variableAction = variableScope.ResolveVariable(variable.Name);
			if (variableAction != null)
			{
				if (!variableAction.IsGlobal)
				{
					throw XsltException.Create("Variable or parameter '{0}' was duplicated within the same scope.", new string[]
					{
						variable.NameStr
					});
				}
				if (variable.IsGlobal)
				{
					if (variable.Stylesheetid == variableAction.Stylesheetid)
					{
						throw XsltException.Create("Variable or parameter '{0}' was duplicated within the same scope.", new string[]
						{
							variable.NameStr
						});
					}
					if (variable.Stylesheetid < variableAction.Stylesheetid)
					{
						variableScope.InsertVariable(variable);
						return variableAction.VarKey;
					}
					return -1;
				}
			}
			variableScope.InsertVariable(variable);
			return this.currentTemplate.AllocateVariableSlot();
		}

		// Token: 0x0600239A RID: 9114 RVA: 0x000DC764 File Offset: 0x000DA964
		internal void AddNamespaceAlias(string StylesheetURI, NamespaceInfo AliasInfo)
		{
			if (this.globalNamespaceAliasTable == null)
			{
				this.globalNamespaceAliasTable = new Hashtable();
			}
			NamespaceInfo namespaceInfo = this.globalNamespaceAliasTable[StylesheetURI] as NamespaceInfo;
			if (namespaceInfo == null || AliasInfo.stylesheetId <= namespaceInfo.stylesheetId)
			{
				this.globalNamespaceAliasTable[StylesheetURI] = AliasInfo;
			}
		}

		// Token: 0x0600239B RID: 9115 RVA: 0x000DC7B4 File Offset: 0x000DA9B4
		internal bool IsNamespaceAlias(string StylesheetURI)
		{
			return this.globalNamespaceAliasTable != null && this.globalNamespaceAliasTable.Contains(StylesheetURI);
		}

		// Token: 0x0600239C RID: 9116 RVA: 0x000DC7CC File Offset: 0x000DA9CC
		internal NamespaceInfo FindNamespaceAlias(string StylesheetURI)
		{
			if (this.globalNamespaceAliasTable != null)
			{
				return (NamespaceInfo)this.globalNamespaceAliasTable[StylesheetURI];
			}
			return null;
		}

		// Token: 0x0600239D RID: 9117 RVA: 0x000DC7E9 File Offset: 0x000DA9E9
		internal string ResolveXmlNamespace(string prefix)
		{
			return this.scopeManager.ResolveXmlNamespace(prefix);
		}

		// Token: 0x0600239E RID: 9118 RVA: 0x000DC7F7 File Offset: 0x000DA9F7
		internal string ResolveXPathNamespace(string prefix)
		{
			return this.scopeManager.ResolveXPathNamespace(prefix);
		}

		// Token: 0x17000717 RID: 1815
		// (get) Token: 0x0600239F RID: 9119 RVA: 0x000DC805 File Offset: 0x000DAA05
		internal string DefaultNamespace
		{
			get
			{
				return this.scopeManager.DefaultNamespace;
			}
		}

		// Token: 0x060023A0 RID: 9120 RVA: 0x000DC812 File Offset: 0x000DAA12
		internal void InsertKey(XmlQualifiedName name, int MatchKey, int UseKey)
		{
			this.rootAction.InsertKey(name, MatchKey, UseKey);
		}

		// Token: 0x060023A1 RID: 9121 RVA: 0x000DC822 File Offset: 0x000DAA22
		internal void AddDecimalFormat(XmlQualifiedName name, DecimalFormat formatinfo)
		{
			this.rootAction.AddDecimalFormat(name, formatinfo);
		}

		// Token: 0x060023A2 RID: 9122 RVA: 0x000DC834 File Offset: 0x000DAA34
		private string[] ResolvePrefixes(string tokens)
		{
			if (tokens == null || tokens.Length == 0)
			{
				return null;
			}
			string[] array = XmlConvert.SplitString(tokens);
			try
			{
				for (int i = 0; i < array.Length; i++)
				{
					string text = array[i];
					array[i] = this.scopeManager.ResolveXmlNamespace((text == "#default") ? string.Empty : text);
				}
			}
			catch (XsltException)
			{
				if (!this.ForwardCompatibility)
				{
					throw;
				}
				return null;
			}
			return array;
		}

		// Token: 0x060023A3 RID: 9123 RVA: 0x000DC8B0 File Offset: 0x000DAAB0
		internal bool GetYesNo(string value)
		{
			if (value == "yes")
			{
				return true;
			}
			if (value == "no")
			{
				return false;
			}
			throw XsltException.Create("'{1}' is an invalid value for the '{0}' attribute.", new string[]
			{
				this.Input.LocalName,
				value
			});
		}

		// Token: 0x060023A4 RID: 9124 RVA: 0x000DC900 File Offset: 0x000DAB00
		internal string GetSingleAttribute(string attributeAtom)
		{
			NavigatorInput navigatorInput = this.Input;
			string localName = navigatorInput.LocalName;
			string text = null;
			if (navigatorInput.MoveToFirstAttribute())
			{
				string localName2;
				for (;;)
				{
					string namespaceURI = navigatorInput.NamespaceURI;
					localName2 = navigatorInput.LocalName;
					if (namespaceURI.Length == 0)
					{
						if (Ref.Equal(localName2, attributeAtom))
						{
							text = navigatorInput.Value;
						}
						else if (!this.ForwardCompatibility)
						{
							break;
						}
					}
					if (!navigatorInput.MoveToNextAttribute())
					{
						goto Block_4;
					}
				}
				throw XsltException.Create("'{0}' is an invalid attribute for the '{1}' element.", new string[]
				{
					localName2,
					localName
				});
				Block_4:
				navigatorInput.ToParent();
			}
			if (text == null)
			{
				throw XsltException.Create("Missing mandatory attribute '{0}'.", new string[]
				{
					attributeAtom
				});
			}
			return text;
		}

		// Token: 0x060023A5 RID: 9125 RVA: 0x000DC99C File Offset: 0x000DAB9C
		internal XmlQualifiedName CreateXPathQName(string qname)
		{
			string prefix;
			string name;
			PrefixQName.ParseQualifiedName(qname, out prefix, out name);
			return new XmlQualifiedName(name, this.scopeManager.ResolveXPathNamespace(prefix));
		}

		// Token: 0x060023A6 RID: 9126 RVA: 0x000DC9C8 File Offset: 0x000DABC8
		internal XmlQualifiedName CreateXmlQName(string qname)
		{
			string prefix;
			string name;
			PrefixQName.ParseQualifiedName(qname, out prefix, out name);
			return new XmlQualifiedName(name, this.scopeManager.ResolveXmlNamespace(prefix));
		}

		// Token: 0x060023A7 RID: 9127 RVA: 0x000DC9F4 File Offset: 0x000DABF4
		internal static XPathDocument LoadDocument(XmlTextReaderImpl reader)
		{
			reader.EntityHandling = EntityHandling.ExpandEntities;
			reader.XmlValidatingReaderCompatibilityMode = true;
			XPathDocument result;
			try
			{
				result = new XPathDocument(reader, XmlSpace.Preserve);
			}
			finally
			{
				reader.Close();
			}
			return result;
		}

		// Token: 0x060023A8 RID: 9128 RVA: 0x000DCA34 File Offset: 0x000DAC34
		private void AddDocumentURI(string href)
		{
			this.documentURIs.Add(href, null);
		}

		// Token: 0x060023A9 RID: 9129 RVA: 0x000DCA43 File Offset: 0x000DAC43
		private void RemoveDocumentURI(string href)
		{
			this.documentURIs.Remove(href);
		}

		// Token: 0x060023AA RID: 9130 RVA: 0x000DCA51 File Offset: 0x000DAC51
		internal bool IsCircularReference(string href)
		{
			return this.documentURIs.Contains(href);
		}

		// Token: 0x060023AB RID: 9131 RVA: 0x000DCA60 File Offset: 0x000DAC60
		internal Uri ResolveUri(string relativeUri)
		{
			string baseURI = this.Input.BaseURI;
			Uri uri = this.xmlResolver.ResolveUri((baseURI.Length != 0) ? this.xmlResolver.ResolveUri(null, baseURI) : null, relativeUri);
			if (uri == null)
			{
				throw XsltException.Create("Cannot resolve the referenced document '{0}'.", new string[]
				{
					relativeUri
				});
			}
			return uri;
		}

		// Token: 0x060023AC RID: 9132 RVA: 0x000DCAC0 File Offset: 0x000DACC0
		internal NavigatorInput ResolveDocument(Uri absoluteUri)
		{
			object entity = this.xmlResolver.GetEntity(absoluteUri, null, null);
			string text = absoluteUri.ToString();
			if (entity is Stream)
			{
				return new NavigatorInput(Compiler.LoadDocument(new XmlTextReaderImpl(text, (Stream)entity)
				{
					XmlResolver = this.xmlResolver
				}).CreateNavigator(), text, this.rootScope);
			}
			if (entity is XPathNavigator)
			{
				return new NavigatorInput((XPathNavigator)entity, text, this.rootScope);
			}
			throw XsltException.Create("Cannot resolve the referenced document '{0}'.", new string[]
			{
				text
			});
		}

		// Token: 0x060023AD RID: 9133 RVA: 0x000DCB4C File Offset: 0x000DAD4C
		internal void PushInputDocument(NavigatorInput newInput)
		{
			string href = newInput.Href;
			this.AddDocumentURI(href);
			newInput.Next = this.input;
			this.input = newInput;
			this.atoms = this.input.Atoms;
			this.scopeManager = this.input.InputScopeManager;
		}

		// Token: 0x060023AE RID: 9134 RVA: 0x000DCB9C File Offset: 0x000DAD9C
		internal void PopInputDocument()
		{
			NavigatorInput navigatorInput = this.input;
			this.input = navigatorInput.Next;
			navigatorInput.Next = null;
			if (this.input != null)
			{
				this.atoms = this.input.Atoms;
				this.scopeManager = this.input.InputScopeManager;
			}
			else
			{
				this.atoms = null;
				this.scopeManager = null;
			}
			this.RemoveDocumentURI(navigatorInput.Href);
			navigatorInput.Close();
		}

		// Token: 0x060023AF RID: 9135 RVA: 0x000DCC0F File Offset: 0x000DAE0F
		internal void PushStylesheet(Stylesheet stylesheet)
		{
			if (this.stylesheets == null)
			{
				this.stylesheets = new Stack();
			}
			this.stylesheets.Push(stylesheet);
			this.stylesheet = stylesheet;
		}

		// Token: 0x060023B0 RID: 9136 RVA: 0x000DCC37 File Offset: 0x000DAE37
		internal Stylesheet PopStylesheet()
		{
			Stylesheet result = (Stylesheet)this.stylesheets.Pop();
			this.stylesheet = (Stylesheet)this.stylesheets.Peek();
			return result;
		}

		// Token: 0x060023B1 RID: 9137 RVA: 0x000DCC5F File Offset: 0x000DAE5F
		internal void AddAttributeSet(AttributeSetAction attributeSet)
		{
			this.stylesheet.AddAttributeSet(attributeSet);
		}

		// Token: 0x060023B2 RID: 9138 RVA: 0x000DCC6D File Offset: 0x000DAE6D
		internal void AddTemplate(TemplateAction template)
		{
			this.stylesheet.AddTemplate(template);
		}

		// Token: 0x060023B3 RID: 9139 RVA: 0x000DCC7B File Offset: 0x000DAE7B
		internal void BeginTemplate(TemplateAction template)
		{
			this.currentTemplate = template;
			this.currentMode = template.Mode;
			this.CanHaveApplyImports = (template.MatchKey != -1);
		}

		// Token: 0x060023B4 RID: 9140 RVA: 0x000DCCA2 File Offset: 0x000DAEA2
		internal void EndTemplate()
		{
			this.currentTemplate = this.rootAction;
		}

		// Token: 0x17000718 RID: 1816
		// (get) Token: 0x060023B5 RID: 9141 RVA: 0x000DCCB0 File Offset: 0x000DAEB0
		internal XmlQualifiedName CurrentMode
		{
			get
			{
				return this.currentMode;
			}
		}

		// Token: 0x060023B6 RID: 9142 RVA: 0x000DCCB8 File Offset: 0x000DAEB8
		internal int AddQuery(string xpathQuery)
		{
			return this.AddQuery(xpathQuery, true, true, false);
		}

		// Token: 0x060023B7 RID: 9143 RVA: 0x000DCCC4 File Offset: 0x000DAEC4
		internal int AddQuery(string xpathQuery, bool allowVar, bool allowKey, bool isPattern)
		{
			CompiledXpathExpr compiledQuery;
			try
			{
				compiledQuery = new CompiledXpathExpr(isPattern ? this.queryBuilder.BuildPatternQuery(xpathQuery, allowVar, allowKey) : this.queryBuilder.Build(xpathQuery, allowVar, allowKey), xpathQuery, false);
			}
			catch (XPathException inner)
			{
				if (!this.ForwardCompatibility)
				{
					throw XsltException.Create("'{0}' is an invalid XPath expression.", new string[]
					{
						xpathQuery
					}, inner);
				}
				compiledQuery = new Compiler.ErrorXPathExpression(xpathQuery, this.Input.BaseURI, this.Input.LineNumber, this.Input.LinePosition);
			}
			this.queryStore.Add(new TheQuery(compiledQuery, this.scopeManager));
			return this.queryStore.Count - 1;
		}

		// Token: 0x060023B8 RID: 9144 RVA: 0x000DCD7C File Offset: 0x000DAF7C
		internal int AddStringQuery(string xpathQuery)
		{
			string xpathQuery2 = XmlCharType.Instance.IsOnlyWhitespace(xpathQuery) ? xpathQuery : ("string(" + xpathQuery + ")");
			return this.AddQuery(xpathQuery2);
		}

		// Token: 0x060023B9 RID: 9145 RVA: 0x000DCDB4 File Offset: 0x000DAFB4
		internal int AddBooleanQuery(string xpathQuery)
		{
			string xpathQuery2 = XmlCharType.Instance.IsOnlyWhitespace(xpathQuery) ? xpathQuery : ("boolean(" + xpathQuery + ")");
			return this.AddQuery(xpathQuery2);
		}

		// Token: 0x060023BA RID: 9146 RVA: 0x000DCDEC File Offset: 0x000DAFEC
		private static string GenerateUniqueClassName()
		{
			return "ScriptClass_" + Interlocked.Increment(ref Compiler.scriptClassCounter).ToString();
		}

		// Token: 0x060023BB RID: 9147 RVA: 0x000DCE18 File Offset: 0x000DB018
		internal void AddScript(string source, ScriptingLanguage lang, string ns, string fileName, int lineNumber)
		{
			Compiler.ValidateExtensionNamespace(ns);
			for (ScriptingLanguage scriptingLanguage = ScriptingLanguage.JScript; scriptingLanguage <= ScriptingLanguage.CSharp; scriptingLanguage++)
			{
				Hashtable hashtable = this._typeDeclsByLang[(int)scriptingLanguage];
				if (lang == scriptingLanguage)
				{
					CodeTypeDeclaration codeTypeDeclaration = (CodeTypeDeclaration)hashtable[ns];
					if (codeTypeDeclaration == null)
					{
						codeTypeDeclaration = new CodeTypeDeclaration(Compiler.GenerateUniqueClassName());
						codeTypeDeclaration.TypeAttributes = TypeAttributes.Public;
						hashtable.Add(ns, codeTypeDeclaration);
					}
					CodeSnippetTypeMember codeSnippetTypeMember = new CodeSnippetTypeMember(source);
					if (lineNumber > 0)
					{
						codeSnippetTypeMember.LinePragma = new CodeLinePragma(fileName, lineNumber);
						this.scriptFiles.Add(fileName);
					}
					codeTypeDeclaration.Members.Add(codeSnippetTypeMember);
				}
				else if (hashtable.Contains(ns))
				{
					throw XsltException.Create("All script blocks implementing the namespace '{0}' must use the same language.", new string[]
					{
						ns
					});
				}
			}
		}

		// Token: 0x060023BC RID: 9148 RVA: 0x000DCECA File Offset: 0x000DB0CA
		private static void ValidateExtensionNamespace(string nsUri)
		{
			if (nsUri.Length == 0 || nsUri == "http://www.w3.org/1999/XSL/Transform")
			{
				throw XsltException.Create("Extension namespace cannot be 'null' or an XSLT namespace URI.", Array.Empty<string>());
			}
			XmlConvert.ToUri(nsUri);
		}

		// Token: 0x060023BD RID: 9149 RVA: 0x000DCEF8 File Offset: 0x000DB0F8
		private void FixCompilerError(CompilerError e)
		{
			foreach (object obj in this.scriptFiles)
			{
				string b = (string)obj;
				if (e.FileName == b)
				{
					return;
				}
			}
			e.FileName = string.Empty;
		}

		// Token: 0x060023BE RID: 9150 RVA: 0x000DCF68 File Offset: 0x000DB168
		private CodeDomProvider ChooseCodeDomProvider(ScriptingLanguage lang)
		{
			if (lang == ScriptingLanguage.JScript)
			{
				return (CodeDomProvider)Activator.CreateInstance(Type.GetType("Microsoft.JScript.JScriptCodeProvider, Microsoft.JScript, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"), BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.CreateInstance, null, null, null);
			}
			if (lang != ScriptingLanguage.VisualBasic)
			{
				return new CSharpCodeProvider();
			}
			return new VBCodeProvider();
		}

		// Token: 0x060023BF RID: 9151 RVA: 0x000DCF9C File Offset: 0x000DB19C
		private void CompileScript(Evidence evidence)
		{
			for (ScriptingLanguage scriptingLanguage = ScriptingLanguage.JScript; scriptingLanguage <= ScriptingLanguage.CSharp; scriptingLanguage++)
			{
				int num = (int)scriptingLanguage;
				if (this._typeDeclsByLang[num].Count > 0)
				{
					this.CompileAssembly(scriptingLanguage, this._typeDeclsByLang[num], scriptingLanguage.ToString(), evidence);
				}
			}
		}

		// Token: 0x060023C0 RID: 9152 RVA: 0x000DCFE4 File Offset: 0x000DB1E4
		[PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
		private void CompileAssembly(ScriptingLanguage lang, Hashtable typeDecls, string nsName, Evidence evidence)
		{
			nsName = "Microsoft.Xslt.CompiledScripts." + nsName;
			CodeNamespace codeNamespace = new CodeNamespace(nsName);
			foreach (string nameSpace in Compiler._defaultNamespaces)
			{
				codeNamespace.Imports.Add(new CodeNamespaceImport(nameSpace));
			}
			if (lang == ScriptingLanguage.VisualBasic)
			{
				codeNamespace.Imports.Add(new CodeNamespaceImport("Microsoft.VisualBasic"));
			}
			foreach (object obj in typeDecls.Values)
			{
				CodeTypeDeclaration value = (CodeTypeDeclaration)obj;
				codeNamespace.Types.Add(value);
			}
			CodeCompileUnit codeCompileUnit = new CodeCompileUnit();
			codeCompileUnit.Namespaces.Add(codeNamespace);
			codeCompileUnit.UserData["AllowLateBound"] = true;
			codeCompileUnit.UserData["RequireVariableDeclaration"] = false;
			codeCompileUnit.AssemblyCustomAttributes.Add(new CodeAttributeDeclaration(new CodeTypeReference(typeof(SecurityRulesAttribute)), new CodeAttributeArgument[]
			{
				new CodeAttributeArgument(new CodeFieldReferenceExpression(new CodeTypeReferenceExpression(typeof(SecurityRuleSet)), "Level1"))
			}));
			CompilerParameters compilerParameters = new CompilerParameters();
			try
			{
				new SecurityPermission(SecurityPermissionFlag.ControlEvidence).Assert();
				try
				{
					compilerParameters.GenerateInMemory = true;
					compilerParameters.Evidence = evidence;
					compilerParameters.ReferencedAssemblies.Add(typeof(XPathNavigator).Module.FullyQualifiedName);
					compilerParameters.ReferencedAssemblies.Add("System.dll");
					if (lang == ScriptingLanguage.VisualBasic)
					{
						compilerParameters.ReferencedAssemblies.Add("microsoft.visualbasic.dll");
					}
				}
				finally
				{
					CodeAccessPermission.RevertAssert();
				}
			}
			catch
			{
				throw;
			}
			CompilerResults compilerResults = this.ChooseCodeDomProvider(lang).CompileAssemblyFromDom(compilerParameters, new CodeCompileUnit[]
			{
				codeCompileUnit
			});
			if (compilerResults.Errors.HasErrors)
			{
				StringWriter stringWriter = new StringWriter(CultureInfo.InvariantCulture);
				foreach (object obj2 in compilerResults.Errors)
				{
					CompilerError compilerError = (CompilerError)obj2;
					this.FixCompilerError(compilerError);
					stringWriter.WriteLine(compilerError.ToString());
				}
				throw XsltException.Create("Script compile errors:\n{0}", new string[]
				{
					stringWriter.ToString()
				});
			}
			Assembly compiledAssembly = compilerResults.CompiledAssembly;
			foreach (object obj3 in typeDecls)
			{
				DictionaryEntry dictionaryEntry = (DictionaryEntry)obj3;
				string key = (string)dictionaryEntry.Key;
				CodeTypeDeclaration codeTypeDeclaration = (CodeTypeDeclaration)dictionaryEntry.Value;
				this.stylesheet.ScriptObjectTypes.Add(key, compiledAssembly.GetType(nsName + "." + codeTypeDeclaration.Name));
			}
		}

		// Token: 0x060023C1 RID: 9153 RVA: 0x000DD2FC File Offset: 0x000DB4FC
		public string GetNsAlias(ref string prefix)
		{
			if (prefix == "#default")
			{
				prefix = string.Empty;
				return this.DefaultNamespace;
			}
			if (!PrefixQName.ValidatePrefix(prefix))
			{
				throw XsltException.Create("'{1}' is an invalid value for the '{0}' attribute.", new string[]
				{
					this.input.LocalName,
					prefix
				});
			}
			return this.ResolveXPathNamespace(prefix);
		}

		// Token: 0x060023C2 RID: 9154 RVA: 0x000DD35C File Offset: 0x000DB55C
		private static void getTextLex(string avt, ref int start, StringBuilder lex)
		{
			int length = avt.Length;
			int i;
			for (i = start; i < length; i++)
			{
				char c = avt[i];
				if (c == '{')
				{
					if (i + 1 >= length || avt[i + 1] != '{')
					{
						break;
					}
					i++;
				}
				else if (c == '}')
				{
					if (i + 1 >= length || avt[i + 1] != '}')
					{
						throw XsltException.Create("Right curly brace in the attribute value template '{0}' must be doubled.", new string[]
						{
							avt
						});
					}
					i++;
				}
				lex.Append(c);
			}
			start = i;
		}

		// Token: 0x060023C3 RID: 9155 RVA: 0x000DD3E4 File Offset: 0x000DB5E4
		private static void getXPathLex(string avt, ref int start, StringBuilder lex)
		{
			int length = avt.Length;
			int num = 0;
			for (int i = start + 1; i < length; i++)
			{
				char c = avt[i];
				switch (num)
				{
				case 0:
					if (c <= '\'')
					{
						if (c != '"')
						{
							if (c == '\'')
							{
								num = 1;
							}
						}
						else
						{
							num = 2;
						}
					}
					else
					{
						if (c == '{')
						{
							throw XsltException.Create("AVT cannot be nested in AVT '{0}'.", new string[]
							{
								avt
							});
						}
						if (c == '}')
						{
							i++;
							if (i == start + 2)
							{
								throw XsltException.Create("XPath Expression in AVT cannot be empty: '{0}'.", new string[]
								{
									avt
								});
							}
							lex.Append(avt, start + 1, i - start - 2);
							start = i;
							return;
						}
					}
					break;
				case 1:
					if (c == '\'')
					{
						num = 0;
					}
					break;
				case 2:
					if (c == '"')
					{
						num = 0;
					}
					break;
				}
			}
			throw XsltException.Create((num == 0) ? "The braces are not closed in AVT expression '{0}'." : "The literal in AVT expression is not correctly closed '{0}'.", new string[]
			{
				avt
			});
		}

		// Token: 0x060023C4 RID: 9156 RVA: 0x000DD4CC File Offset: 0x000DB6CC
		private static bool GetNextAvtLex(string avt, ref int start, StringBuilder lex, out bool isAvt)
		{
			isAvt = false;
			if (start == avt.Length)
			{
				return false;
			}
			lex.Length = 0;
			Compiler.getTextLex(avt, ref start, lex);
			if (lex.Length == 0)
			{
				isAvt = true;
				Compiler.getXPathLex(avt, ref start, lex);
			}
			return true;
		}

		// Token: 0x060023C5 RID: 9157 RVA: 0x000DD500 File Offset: 0x000DB700
		internal ArrayList CompileAvt(string avtText, out bool constant)
		{
			ArrayList arrayList = new ArrayList();
			constant = true;
			int num = 0;
			bool flag;
			while (Compiler.GetNextAvtLex(avtText, ref num, this.AvtStringBuilder, out flag))
			{
				string text = this.AvtStringBuilder.ToString();
				if (flag)
				{
					arrayList.Add(new AvtEvent(this.AddStringQuery(text)));
					constant = false;
				}
				else
				{
					arrayList.Add(new TextEvent(text));
				}
			}
			return arrayList;
		}

		// Token: 0x060023C6 RID: 9158 RVA: 0x000DD564 File Offset: 0x000DB764
		internal ArrayList CompileAvt(string avtText)
		{
			bool flag;
			return this.CompileAvt(avtText, out flag);
		}

		// Token: 0x060023C7 RID: 9159 RVA: 0x000DD57A File Offset: 0x000DB77A
		public virtual ApplyImportsAction CreateApplyImportsAction()
		{
			ApplyImportsAction applyImportsAction = new ApplyImportsAction();
			applyImportsAction.Compile(this);
			return applyImportsAction;
		}

		// Token: 0x060023C8 RID: 9160 RVA: 0x000DD588 File Offset: 0x000DB788
		public virtual ApplyTemplatesAction CreateApplyTemplatesAction()
		{
			ApplyTemplatesAction applyTemplatesAction = new ApplyTemplatesAction();
			applyTemplatesAction.Compile(this);
			return applyTemplatesAction;
		}

		// Token: 0x060023C9 RID: 9161 RVA: 0x000DD596 File Offset: 0x000DB796
		public virtual AttributeAction CreateAttributeAction()
		{
			AttributeAction attributeAction = new AttributeAction();
			attributeAction.Compile(this);
			return attributeAction;
		}

		// Token: 0x060023CA RID: 9162 RVA: 0x000DD5A4 File Offset: 0x000DB7A4
		public virtual AttributeSetAction CreateAttributeSetAction()
		{
			AttributeSetAction attributeSetAction = new AttributeSetAction();
			attributeSetAction.Compile(this);
			return attributeSetAction;
		}

		// Token: 0x060023CB RID: 9163 RVA: 0x000DD5B2 File Offset: 0x000DB7B2
		public virtual CallTemplateAction CreateCallTemplateAction()
		{
			CallTemplateAction callTemplateAction = new CallTemplateAction();
			callTemplateAction.Compile(this);
			return callTemplateAction;
		}

		// Token: 0x060023CC RID: 9164 RVA: 0x000DD5C0 File Offset: 0x000DB7C0
		public virtual ChooseAction CreateChooseAction()
		{
			ChooseAction chooseAction = new ChooseAction();
			chooseAction.Compile(this);
			return chooseAction;
		}

		// Token: 0x060023CD RID: 9165 RVA: 0x000DD5CE File Offset: 0x000DB7CE
		public virtual CommentAction CreateCommentAction()
		{
			CommentAction commentAction = new CommentAction();
			commentAction.Compile(this);
			return commentAction;
		}

		// Token: 0x060023CE RID: 9166 RVA: 0x000DD5DC File Offset: 0x000DB7DC
		public virtual CopyAction CreateCopyAction()
		{
			CopyAction copyAction = new CopyAction();
			copyAction.Compile(this);
			return copyAction;
		}

		// Token: 0x060023CF RID: 9167 RVA: 0x000DD5EA File Offset: 0x000DB7EA
		public virtual CopyOfAction CreateCopyOfAction()
		{
			CopyOfAction copyOfAction = new CopyOfAction();
			copyOfAction.Compile(this);
			return copyOfAction;
		}

		// Token: 0x060023D0 RID: 9168 RVA: 0x000DD5F8 File Offset: 0x000DB7F8
		public virtual ElementAction CreateElementAction()
		{
			ElementAction elementAction = new ElementAction();
			elementAction.Compile(this);
			return elementAction;
		}

		// Token: 0x060023D1 RID: 9169 RVA: 0x000DD606 File Offset: 0x000DB806
		public virtual ForEachAction CreateForEachAction()
		{
			ForEachAction forEachAction = new ForEachAction();
			forEachAction.Compile(this);
			return forEachAction;
		}

		// Token: 0x060023D2 RID: 9170 RVA: 0x000DD614 File Offset: 0x000DB814
		public virtual IfAction CreateIfAction(IfAction.ConditionType type)
		{
			IfAction ifAction = new IfAction(type);
			ifAction.Compile(this);
			return ifAction;
		}

		// Token: 0x060023D3 RID: 9171 RVA: 0x000DD623 File Offset: 0x000DB823
		public virtual MessageAction CreateMessageAction()
		{
			MessageAction messageAction = new MessageAction();
			messageAction.Compile(this);
			return messageAction;
		}

		// Token: 0x060023D4 RID: 9172 RVA: 0x000DD631 File Offset: 0x000DB831
		public virtual NewInstructionAction CreateNewInstructionAction()
		{
			NewInstructionAction newInstructionAction = new NewInstructionAction();
			newInstructionAction.Compile(this);
			return newInstructionAction;
		}

		// Token: 0x060023D5 RID: 9173 RVA: 0x000DD63F File Offset: 0x000DB83F
		public virtual NumberAction CreateNumberAction()
		{
			NumberAction numberAction = new NumberAction();
			numberAction.Compile(this);
			return numberAction;
		}

		// Token: 0x060023D6 RID: 9174 RVA: 0x000DD64D File Offset: 0x000DB84D
		public virtual ProcessingInstructionAction CreateProcessingInstructionAction()
		{
			ProcessingInstructionAction processingInstructionAction = new ProcessingInstructionAction();
			processingInstructionAction.Compile(this);
			return processingInstructionAction;
		}

		// Token: 0x060023D7 RID: 9175 RVA: 0x000DD65B File Offset: 0x000DB85B
		public virtual void CreateRootAction()
		{
			this.RootAction = new RootAction();
			this.RootAction.Compile(this);
		}

		// Token: 0x060023D8 RID: 9176 RVA: 0x000DD674 File Offset: 0x000DB874
		public virtual SortAction CreateSortAction()
		{
			SortAction sortAction = new SortAction();
			sortAction.Compile(this);
			return sortAction;
		}

		// Token: 0x060023D9 RID: 9177 RVA: 0x000DD682 File Offset: 0x000DB882
		public virtual TemplateAction CreateTemplateAction()
		{
			TemplateAction templateAction = new TemplateAction();
			templateAction.Compile(this);
			return templateAction;
		}

		// Token: 0x060023DA RID: 9178 RVA: 0x000DD690 File Offset: 0x000DB890
		public virtual TemplateAction CreateSingleTemplateAction()
		{
			TemplateAction templateAction = new TemplateAction();
			templateAction.CompileSingle(this);
			return templateAction;
		}

		// Token: 0x060023DB RID: 9179 RVA: 0x000DD69E File Offset: 0x000DB89E
		public virtual TextAction CreateTextAction()
		{
			TextAction textAction = new TextAction();
			textAction.Compile(this);
			return textAction;
		}

		// Token: 0x060023DC RID: 9180 RVA: 0x000DD6AC File Offset: 0x000DB8AC
		public virtual UseAttributeSetsAction CreateUseAttributeSetsAction()
		{
			UseAttributeSetsAction useAttributeSetsAction = new UseAttributeSetsAction();
			useAttributeSetsAction.Compile(this);
			return useAttributeSetsAction;
		}

		// Token: 0x060023DD RID: 9181 RVA: 0x000DD6BA File Offset: 0x000DB8BA
		public virtual ValueOfAction CreateValueOfAction()
		{
			ValueOfAction valueOfAction = new ValueOfAction();
			valueOfAction.Compile(this);
			return valueOfAction;
		}

		// Token: 0x060023DE RID: 9182 RVA: 0x000DD6C8 File Offset: 0x000DB8C8
		public virtual VariableAction CreateVariableAction(VariableType type)
		{
			VariableAction variableAction = new VariableAction(type);
			variableAction.Compile(this);
			if (variableAction.VarKey != -1)
			{
				return variableAction;
			}
			return null;
		}

		// Token: 0x060023DF RID: 9183 RVA: 0x000DD6EF File Offset: 0x000DB8EF
		public virtual WithParamAction CreateWithParamAction()
		{
			WithParamAction withParamAction = new WithParamAction();
			withParamAction.Compile(this);
			return withParamAction;
		}

		// Token: 0x060023E0 RID: 9184 RVA: 0x000DD6FD File Offset: 0x000DB8FD
		public virtual BeginEvent CreateBeginEvent()
		{
			return new BeginEvent(this);
		}

		// Token: 0x060023E1 RID: 9185 RVA: 0x000DD705 File Offset: 0x000DB905
		public virtual TextEvent CreateTextEvent()
		{
			return new TextEvent(this);
		}

		// Token: 0x060023E2 RID: 9186 RVA: 0x000DD710 File Offset: 0x000DB910
		public XsltException UnexpectedKeyword()
		{
			XPathNavigator xpathNavigator = this.Input.Navigator.Clone();
			string name = xpathNavigator.Name;
			xpathNavigator.MoveToParent();
			string name2 = xpathNavigator.Name;
			return XsltException.Create("'{0}' cannot be a child of the '{1}' element.", new string[]
			{
				name,
				name2
			});
		}

		// Token: 0x060023E3 RID: 9187 RVA: 0x000DD75C File Offset: 0x000DB95C
		public Compiler()
		{
		}

		// Token: 0x060023E4 RID: 9188 RVA: 0x000DD7C0 File Offset: 0x000DB9C0
		// Note: this type is marked as 'beforefieldinit'.
		static Compiler()
		{
		}

		// Token: 0x04001C9F RID: 7327
		internal const int InvalidQueryKey = -1;

		// Token: 0x04001CA0 RID: 7328
		internal const double RootPriority = 0.5;

		// Token: 0x04001CA1 RID: 7329
		internal StringBuilder AvtStringBuilder = new StringBuilder();

		// Token: 0x04001CA2 RID: 7330
		private int stylesheetid;

		// Token: 0x04001CA3 RID: 7331
		private InputScope rootScope;

		// Token: 0x04001CA4 RID: 7332
		private XmlResolver xmlResolver;

		// Token: 0x04001CA5 RID: 7333
		private TemplateBaseAction currentTemplate;

		// Token: 0x04001CA6 RID: 7334
		private XmlQualifiedName currentMode;

		// Token: 0x04001CA7 RID: 7335
		private Hashtable globalNamespaceAliasTable;

		// Token: 0x04001CA8 RID: 7336
		private Stack stylesheets;

		// Token: 0x04001CA9 RID: 7337
		private HybridDictionary documentURIs = new HybridDictionary();

		// Token: 0x04001CAA RID: 7338
		private NavigatorInput input;

		// Token: 0x04001CAB RID: 7339
		private KeywordsTable atoms;

		// Token: 0x04001CAC RID: 7340
		private InputScopeManager scopeManager;

		// Token: 0x04001CAD RID: 7341
		internal Stylesheet stylesheet;

		// Token: 0x04001CAE RID: 7342
		internal Stylesheet rootStylesheet;

		// Token: 0x04001CAF RID: 7343
		private RootAction rootAction;

		// Token: 0x04001CB0 RID: 7344
		private List<TheQuery> queryStore;

		// Token: 0x04001CB1 RID: 7345
		private QueryBuilder queryBuilder = new QueryBuilder();

		// Token: 0x04001CB2 RID: 7346
		private int rtfCount;

		// Token: 0x04001CB3 RID: 7347
		public bool AllowBuiltInMode;

		// Token: 0x04001CB4 RID: 7348
		public static XmlQualifiedName BuiltInMode = new XmlQualifiedName("*", string.Empty);

		// Token: 0x04001CB5 RID: 7349
		private Hashtable[] _typeDeclsByLang = new Hashtable[]
		{
			new Hashtable(),
			new Hashtable(),
			new Hashtable()
		};

		// Token: 0x04001CB6 RID: 7350
		private ArrayList scriptFiles = new ArrayList();

		// Token: 0x04001CB7 RID: 7351
		private static string[] _defaultNamespaces = new string[]
		{
			"System",
			"System.Collections",
			"System.Text",
			"System.Text.RegularExpressions",
			"System.Xml",
			"System.Xml.Xsl",
			"System.Xml.XPath"
		};

		// Token: 0x04001CB8 RID: 7352
		private static int scriptClassCounter = 0;

		// Token: 0x02000361 RID: 865
		internal class ErrorXPathExpression : CompiledXpathExpr
		{
			// Token: 0x060023E5 RID: 9189 RVA: 0x000DD82A File Offset: 0x000DBA2A
			public ErrorXPathExpression(string expression, string baseUri, int lineNumber, int linePosition) : base(null, expression, false)
			{
				this.baseUri = baseUri;
				this.lineNumber = lineNumber;
				this.linePosition = linePosition;
			}

			// Token: 0x060023E6 RID: 9190 RVA: 0x00002068 File Offset: 0x00000268
			public override XPathExpression Clone()
			{
				return this;
			}

			// Token: 0x060023E7 RID: 9191 RVA: 0x000DD84B File Offset: 0x000DBA4B
			public override void CheckErrors()
			{
				throw new XsltException("'{0}' is an invalid XPath expression.", new string[]
				{
					this.Expression
				}, this.baseUri, this.linePosition, this.lineNumber, null);
			}

			// Token: 0x04001CB9 RID: 7353
			private string baseUri;

			// Token: 0x04001CBA RID: 7354
			private int lineNumber;

			// Token: 0x04001CBB RID: 7355
			private int linePosition;
		}
	}
}

using System;
using System.Xml.XPath;

namespace System.Xml.Xsl.XsltOld
{
	// Token: 0x0200038F RID: 911
	internal class InputScopeManager
	{
		// Token: 0x060024F0 RID: 9456 RVA: 0x000E105A File Offset: 0x000DF25A
		public InputScopeManager(XPathNavigator navigator, InputScope rootScope)
		{
			this.navigator = navigator;
			this.scopeStack = rootScope;
		}

		// Token: 0x17000731 RID: 1841
		// (get) Token: 0x060024F1 RID: 9457 RVA: 0x000E107B File Offset: 0x000DF27B
		internal InputScope CurrentScope
		{
			get
			{
				return this.scopeStack;
			}
		}

		// Token: 0x17000732 RID: 1842
		// (get) Token: 0x060024F2 RID: 9458 RVA: 0x000E1083 File Offset: 0x000DF283
		internal InputScope VariableScope
		{
			get
			{
				return this.scopeStack.Parent;
			}
		}

		// Token: 0x060024F3 RID: 9459 RVA: 0x000E1090 File Offset: 0x000DF290
		internal InputScopeManager Clone()
		{
			return new InputScopeManager(this.navigator, null)
			{
				scopeStack = this.scopeStack,
				defaultNS = this.defaultNS
			};
		}

		// Token: 0x17000733 RID: 1843
		// (get) Token: 0x060024F4 RID: 9460 RVA: 0x000E10B6 File Offset: 0x000DF2B6
		public XPathNavigator Navigator
		{
			get
			{
				return this.navigator;
			}
		}

		// Token: 0x060024F5 RID: 9461 RVA: 0x000E10BE File Offset: 0x000DF2BE
		internal InputScope PushScope()
		{
			this.scopeStack = new InputScope(this.scopeStack);
			return this.scopeStack;
		}

		// Token: 0x060024F6 RID: 9462 RVA: 0x000E10D8 File Offset: 0x000DF2D8
		internal void PopScope()
		{
			if (this.scopeStack == null)
			{
				return;
			}
			for (NamespaceDecl namespaceDecl = this.scopeStack.Scopes; namespaceDecl != null; namespaceDecl = namespaceDecl.Next)
			{
				this.defaultNS = namespaceDecl.PrevDefaultNsUri;
			}
			this.scopeStack = this.scopeStack.Parent;
		}

		// Token: 0x060024F7 RID: 9463 RVA: 0x000E1123 File Offset: 0x000DF323
		internal void PushNamespace(string prefix, string nspace)
		{
			this.scopeStack.AddNamespace(prefix, nspace, this.defaultNS);
			if (prefix == null || prefix.Length == 0)
			{
				this.defaultNS = nspace;
			}
		}

		// Token: 0x17000734 RID: 1844
		// (get) Token: 0x060024F8 RID: 9464 RVA: 0x000E114B File Offset: 0x000DF34B
		public string DefaultNamespace
		{
			get
			{
				return this.defaultNS;
			}
		}

		// Token: 0x060024F9 RID: 9465 RVA: 0x000E1154 File Offset: 0x000DF354
		private string ResolveNonEmptyPrefix(string prefix)
		{
			if (prefix == "xml")
			{
				return "http://www.w3.org/XML/1998/namespace";
			}
			if (prefix == "xmlns")
			{
				return "http://www.w3.org/2000/xmlns/";
			}
			for (InputScope parent = this.scopeStack; parent != null; parent = parent.Parent)
			{
				string text = parent.ResolveNonAtom(prefix);
				if (text != null)
				{
					return text;
				}
			}
			throw XsltException.Create("Prefix '{0}' is not defined.", new string[]
			{
				prefix
			});
		}

		// Token: 0x060024FA RID: 9466 RVA: 0x000E11BB File Offset: 0x000DF3BB
		public string ResolveXmlNamespace(string prefix)
		{
			if (prefix.Length == 0)
			{
				return this.defaultNS;
			}
			return this.ResolveNonEmptyPrefix(prefix);
		}

		// Token: 0x060024FB RID: 9467 RVA: 0x000E11D3 File Offset: 0x000DF3D3
		public string ResolveXPathNamespace(string prefix)
		{
			if (prefix.Length == 0)
			{
				return string.Empty;
			}
			return this.ResolveNonEmptyPrefix(prefix);
		}

		// Token: 0x060024FC RID: 9468 RVA: 0x000E11EC File Offset: 0x000DF3EC
		internal void InsertExtensionNamespaces(string[] nsList)
		{
			for (int i = 0; i < nsList.Length; i++)
			{
				this.scopeStack.InsertExtensionNamespace(nsList[i]);
			}
		}

		// Token: 0x060024FD RID: 9469 RVA: 0x000E1218 File Offset: 0x000DF418
		internal bool IsExtensionNamespace(string nspace)
		{
			for (InputScope parent = this.scopeStack; parent != null; parent = parent.Parent)
			{
				if (parent.IsExtensionNamespace(nspace))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060024FE RID: 9470 RVA: 0x000E1244 File Offset: 0x000DF444
		internal void InsertExcludedNamespaces(string[] nsList)
		{
			for (int i = 0; i < nsList.Length; i++)
			{
				this.scopeStack.InsertExcludedNamespace(nsList[i]);
			}
		}

		// Token: 0x060024FF RID: 9471 RVA: 0x000E1270 File Offset: 0x000DF470
		internal bool IsExcludedNamespace(string nspace)
		{
			for (InputScope parent = this.scopeStack; parent != null; parent = parent.Parent)
			{
				if (parent.IsExcludedNamespace(nspace))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x04001D29 RID: 7465
		private InputScope scopeStack;

		// Token: 0x04001D2A RID: 7466
		private string defaultNS = string.Empty;

		// Token: 0x04001D2B RID: 7467
		private XPathNavigator navigator;
	}
}

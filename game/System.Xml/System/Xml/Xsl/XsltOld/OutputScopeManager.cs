using System;
using System.Globalization;

namespace System.Xml.Xsl.XsltOld
{
	// Token: 0x0200039B RID: 923
	internal class OutputScopeManager
	{
		// Token: 0x17000758 RID: 1880
		// (get) Token: 0x06002563 RID: 9571 RVA: 0x000E2A0A File Offset: 0x000E0C0A
		internal string DefaultNamespace
		{
			get
			{
				return this.defaultNS;
			}
		}

		// Token: 0x17000759 RID: 1881
		// (get) Token: 0x06002564 RID: 9572 RVA: 0x000E2A12 File Offset: 0x000E0C12
		internal OutputScope CurrentElementScope
		{
			get
			{
				return (OutputScope)this.elementScopesStack.Peek();
			}
		}

		// Token: 0x1700075A RID: 1882
		// (get) Token: 0x06002565 RID: 9573 RVA: 0x000E2A24 File Offset: 0x000E0C24
		internal XmlSpace XmlSpace
		{
			get
			{
				return this.CurrentElementScope.Space;
			}
		}

		// Token: 0x1700075B RID: 1883
		// (get) Token: 0x06002566 RID: 9574 RVA: 0x000E2A31 File Offset: 0x000E0C31
		internal string XmlLang
		{
			get
			{
				return this.CurrentElementScope.Lang;
			}
		}

		// Token: 0x06002567 RID: 9575 RVA: 0x000E2A40 File Offset: 0x000E0C40
		internal OutputScopeManager(XmlNameTable nameTable, OutKeywords atoms)
		{
			this.elementScopesStack = new HWStack(10);
			this.nameTable = nameTable;
			this.atoms = atoms;
			this.defaultNS = this.atoms.Empty;
			OutputScope outputScope = (OutputScope)this.elementScopesStack.Push();
			if (outputScope == null)
			{
				outputScope = new OutputScope();
				this.elementScopesStack.AddToTop(outputScope);
			}
			outputScope.Init(string.Empty, string.Empty, string.Empty, XmlSpace.None, string.Empty, false);
		}

		// Token: 0x06002568 RID: 9576 RVA: 0x000E2AC1 File Offset: 0x000E0CC1
		internal void PushNamespace(string prefix, string nspace)
		{
			this.CurrentElementScope.AddNamespace(prefix, nspace, this.defaultNS);
			if (prefix == null || prefix.Length == 0)
			{
				this.defaultNS = nspace;
			}
		}

		// Token: 0x06002569 RID: 9577 RVA: 0x000E2AEC File Offset: 0x000E0CEC
		internal void PushScope(string name, string nspace, string prefix)
		{
			OutputScope currentElementScope = this.CurrentElementScope;
			OutputScope outputScope = (OutputScope)this.elementScopesStack.Push();
			if (outputScope == null)
			{
				outputScope = new OutputScope();
				this.elementScopesStack.AddToTop(outputScope);
			}
			outputScope.Init(name, nspace, prefix, currentElementScope.Space, currentElementScope.Lang, currentElementScope.Mixed);
		}

		// Token: 0x0600256A RID: 9578 RVA: 0x000E2B44 File Offset: 0x000E0D44
		internal void PopScope()
		{
			for (NamespaceDecl namespaceDecl = ((OutputScope)this.elementScopesStack.Pop()).Scopes; namespaceDecl != null; namespaceDecl = namespaceDecl.Next)
			{
				this.defaultNS = namespaceDecl.PrevDefaultNsUri;
			}
		}

		// Token: 0x0600256B RID: 9579 RVA: 0x000E2B80 File Offset: 0x000E0D80
		internal string ResolveNamespace(string prefix)
		{
			bool flag;
			return this.ResolveNamespace(prefix, out flag);
		}

		// Token: 0x0600256C RID: 9580 RVA: 0x000E2B98 File Offset: 0x000E0D98
		internal string ResolveNamespace(string prefix, out bool thisScope)
		{
			thisScope = true;
			if (prefix == null || prefix.Length == 0)
			{
				return this.defaultNS;
			}
			if (Ref.Equal(prefix, this.atoms.Xml))
			{
				return this.atoms.XmlNamespace;
			}
			if (Ref.Equal(prefix, this.atoms.Xmlns))
			{
				return this.atoms.XmlnsNamespace;
			}
			for (int i = this.elementScopesStack.Length - 1; i >= 0; i--)
			{
				string text = ((OutputScope)this.elementScopesStack[i]).ResolveAtom(prefix);
				if (text != null)
				{
					thisScope = (i == this.elementScopesStack.Length - 1);
					return text;
				}
			}
			return null;
		}

		// Token: 0x0600256D RID: 9581 RVA: 0x000E2C40 File Offset: 0x000E0E40
		internal bool FindPrefix(string nspace, out string prefix)
		{
			int num = this.elementScopesStack.Length - 1;
			while (0 <= num)
			{
				OutputScope outputScope = (OutputScope)this.elementScopesStack[num];
				string text = null;
				if (outputScope.FindPrefix(nspace, out text))
				{
					string text2 = this.ResolveNamespace(text);
					if (text2 != null && Ref.Equal(text2, nspace))
					{
						prefix = text;
						return true;
					}
					break;
				}
				else
				{
					num--;
				}
			}
			prefix = null;
			return false;
		}

		// Token: 0x0600256E RID: 9582 RVA: 0x000E2CA0 File Offset: 0x000E0EA0
		internal string GeneratePrefix(string format)
		{
			string array;
			do
			{
				IFormatProvider invariantCulture = CultureInfo.InvariantCulture;
				int num = this.prefixIndex;
				this.prefixIndex = num + 1;
				array = string.Format(invariantCulture, format, num);
			}
			while (this.nameTable.Get(array) != null);
			return this.nameTable.Add(array);
		}

		// Token: 0x04001D70 RID: 7536
		private const int STACK_INCREMENT = 10;

		// Token: 0x04001D71 RID: 7537
		private HWStack elementScopesStack;

		// Token: 0x04001D72 RID: 7538
		private string defaultNS;

		// Token: 0x04001D73 RID: 7539
		private OutKeywords atoms;

		// Token: 0x04001D74 RID: 7540
		private XmlNameTable nameTable;

		// Token: 0x04001D75 RID: 7541
		private int prefixIndex;
	}
}

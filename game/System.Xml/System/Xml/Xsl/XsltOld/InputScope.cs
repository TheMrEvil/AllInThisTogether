using System;
using System.Collections;

namespace System.Xml.Xsl.XsltOld
{
	// Token: 0x0200038E RID: 910
	internal class InputScope : DocumentScope
	{
		// Token: 0x1700072D RID: 1837
		// (get) Token: 0x060024E0 RID: 9440 RVA: 0x000E0EC8 File Offset: 0x000DF0C8
		internal InputScope Parent
		{
			get
			{
				return this.parent;
			}
		}

		// Token: 0x1700072E RID: 1838
		// (get) Token: 0x060024E1 RID: 9441 RVA: 0x000E0ED0 File Offset: 0x000DF0D0
		internal Hashtable Variables
		{
			get
			{
				return this.variables;
			}
		}

		// Token: 0x1700072F RID: 1839
		// (get) Token: 0x060024E2 RID: 9442 RVA: 0x000E0ED8 File Offset: 0x000DF0D8
		// (set) Token: 0x060024E3 RID: 9443 RVA: 0x000E0EE0 File Offset: 0x000DF0E0
		internal bool ForwardCompatibility
		{
			get
			{
				return this.forwardCompatibility;
			}
			set
			{
				this.forwardCompatibility = value;
			}
		}

		// Token: 0x17000730 RID: 1840
		// (get) Token: 0x060024E4 RID: 9444 RVA: 0x000E0EE9 File Offset: 0x000DF0E9
		// (set) Token: 0x060024E5 RID: 9445 RVA: 0x000E0EF1 File Offset: 0x000DF0F1
		internal bool CanHaveApplyImports
		{
			get
			{
				return this.canHaveApplyImports;
			}
			set
			{
				this.canHaveApplyImports = value;
			}
		}

		// Token: 0x060024E6 RID: 9446 RVA: 0x000E0EFA File Offset: 0x000DF0FA
		internal InputScope(InputScope parent)
		{
			this.Init(parent);
		}

		// Token: 0x060024E7 RID: 9447 RVA: 0x000E0F09 File Offset: 0x000DF109
		internal void Init(InputScope parent)
		{
			this.scopes = null;
			this.parent = parent;
			if (this.parent != null)
			{
				this.forwardCompatibility = this.parent.forwardCompatibility;
				this.canHaveApplyImports = this.parent.canHaveApplyImports;
			}
		}

		// Token: 0x060024E8 RID: 9448 RVA: 0x000E0F43 File Offset: 0x000DF143
		internal void InsertExtensionNamespace(string nspace)
		{
			if (this.extensionNamespaces == null)
			{
				this.extensionNamespaces = new Hashtable();
			}
			this.extensionNamespaces[nspace] = null;
		}

		// Token: 0x060024E9 RID: 9449 RVA: 0x000E0F65 File Offset: 0x000DF165
		internal bool IsExtensionNamespace(string nspace)
		{
			return this.extensionNamespaces != null && this.extensionNamespaces.Contains(nspace);
		}

		// Token: 0x060024EA RID: 9450 RVA: 0x000E0F7D File Offset: 0x000DF17D
		internal void InsertExcludedNamespace(string nspace)
		{
			if (this.excludedNamespaces == null)
			{
				this.excludedNamespaces = new Hashtable();
			}
			this.excludedNamespaces[nspace] = null;
		}

		// Token: 0x060024EB RID: 9451 RVA: 0x000E0F9F File Offset: 0x000DF19F
		internal bool IsExcludedNamespace(string nspace)
		{
			return this.excludedNamespaces != null && this.excludedNamespaces.Contains(nspace);
		}

		// Token: 0x060024EC RID: 9452 RVA: 0x000E0FB7 File Offset: 0x000DF1B7
		internal void InsertVariable(VariableAction variable)
		{
			if (this.variables == null)
			{
				this.variables = new Hashtable();
			}
			this.variables[variable.Name] = variable;
		}

		// Token: 0x060024ED RID: 9453 RVA: 0x000E0FDE File Offset: 0x000DF1DE
		internal int GetVeriablesCount()
		{
			if (this.variables == null)
			{
				return 0;
			}
			return this.variables.Count;
		}

		// Token: 0x060024EE RID: 9454 RVA: 0x000E0FF8 File Offset: 0x000DF1F8
		public VariableAction ResolveVariable(XmlQualifiedName qname)
		{
			for (InputScope inputScope = this; inputScope != null; inputScope = inputScope.Parent)
			{
				if (inputScope.Variables != null)
				{
					VariableAction variableAction = (VariableAction)inputScope.Variables[qname];
					if (variableAction != null)
					{
						return variableAction;
					}
				}
			}
			return null;
		}

		// Token: 0x060024EF RID: 9455 RVA: 0x000E1034 File Offset: 0x000DF234
		public VariableAction ResolveGlobalVariable(XmlQualifiedName qname)
		{
			InputScope inputScope = null;
			for (InputScope inputScope2 = this; inputScope2 != null; inputScope2 = inputScope2.Parent)
			{
				inputScope = inputScope2;
			}
			return inputScope.ResolveVariable(qname);
		}

		// Token: 0x04001D23 RID: 7459
		private InputScope parent;

		// Token: 0x04001D24 RID: 7460
		private bool forwardCompatibility;

		// Token: 0x04001D25 RID: 7461
		private bool canHaveApplyImports;

		// Token: 0x04001D26 RID: 7462
		private Hashtable variables;

		// Token: 0x04001D27 RID: 7463
		private Hashtable extensionNamespaces;

		// Token: 0x04001D28 RID: 7464
		private Hashtable excludedNamespaces;
	}
}

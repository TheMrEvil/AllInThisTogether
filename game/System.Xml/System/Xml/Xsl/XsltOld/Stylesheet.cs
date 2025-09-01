using System;
using System.Collections;
using System.Xml.XPath;

namespace System.Xml.Xsl.XsltOld
{
	// Token: 0x020003AD RID: 941
	internal class Stylesheet
	{
		// Token: 0x17000793 RID: 1939
		// (get) Token: 0x06002678 RID: 9848 RVA: 0x000E732C File Offset: 0x000E552C
		internal bool Whitespace
		{
			get
			{
				return this.whitespace;
			}
		}

		// Token: 0x17000794 RID: 1940
		// (get) Token: 0x06002679 RID: 9849 RVA: 0x000E7334 File Offset: 0x000E5534
		internal ArrayList Imports
		{
			get
			{
				return this.imports;
			}
		}

		// Token: 0x17000795 RID: 1941
		// (get) Token: 0x0600267A RID: 9850 RVA: 0x000E733C File Offset: 0x000E553C
		internal Hashtable AttributeSetTable
		{
			get
			{
				return this.attributeSetTable;
			}
		}

		// Token: 0x0600267B RID: 9851 RVA: 0x000E7344 File Offset: 0x000E5544
		internal void AddSpace(Compiler compiler, string query, double Priority, bool PreserveSpace)
		{
			Stylesheet.WhitespaceElement whitespaceElement;
			if (this.queryKeyTable != null)
			{
				if (this.queryKeyTable.Contains(query))
				{
					whitespaceElement = (Stylesheet.WhitespaceElement)this.queryKeyTable[query];
					whitespaceElement.ReplaceValue(PreserveSpace);
					return;
				}
			}
			else
			{
				this.queryKeyTable = new Hashtable();
				this.whitespaceList = new ArrayList();
			}
			whitespaceElement = new Stylesheet.WhitespaceElement(compiler.AddQuery(query), Priority, PreserveSpace);
			this.queryKeyTable[query] = whitespaceElement;
			this.whitespaceList.Add(whitespaceElement);
		}

		// Token: 0x0600267C RID: 9852 RVA: 0x000E73C4 File Offset: 0x000E55C4
		internal void SortWhiteSpace()
		{
			if (this.queryKeyTable != null)
			{
				for (int i = 0; i < this.whitespaceList.Count; i++)
				{
					for (int j = this.whitespaceList.Count - 1; j > i; j--)
					{
						Stylesheet.WhitespaceElement whitespaceElement = (Stylesheet.WhitespaceElement)this.whitespaceList[j - 1];
						Stylesheet.WhitespaceElement whitespaceElement2 = (Stylesheet.WhitespaceElement)this.whitespaceList[j];
						if (whitespaceElement2.Priority < whitespaceElement.Priority)
						{
							this.whitespaceList[j - 1] = whitespaceElement2;
							this.whitespaceList[j] = whitespaceElement;
						}
					}
				}
				this.whitespace = true;
			}
			if (this.imports != null)
			{
				for (int k = this.imports.Count - 1; k >= 0; k--)
				{
					Stylesheet stylesheet = (Stylesheet)this.imports[k];
					if (stylesheet.Whitespace)
					{
						stylesheet.SortWhiteSpace();
						this.whitespace = true;
					}
				}
			}
		}

		// Token: 0x0600267D RID: 9853 RVA: 0x000E74B0 File Offset: 0x000E56B0
		internal bool PreserveWhiteSpace(Processor proc, XPathNavigator node)
		{
			if (this.whitespaceList != null)
			{
				int num = this.whitespaceList.Count - 1;
				while (0 <= num)
				{
					Stylesheet.WhitespaceElement whitespaceElement = (Stylesheet.WhitespaceElement)this.whitespaceList[num];
					if (proc.Matches(node, whitespaceElement.Key))
					{
						return whitespaceElement.PreserveSpace;
					}
					num--;
				}
			}
			if (this.imports != null)
			{
				for (int i = this.imports.Count - 1; i >= 0; i--)
				{
					if (!((Stylesheet)this.imports[i]).PreserveWhiteSpace(proc, node))
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x0600267E RID: 9854 RVA: 0x000E7544 File Offset: 0x000E5744
		internal void AddAttributeSet(AttributeSetAction attributeSet)
		{
			if (this.attributeSetTable == null)
			{
				this.attributeSetTable = new Hashtable();
			}
			if (!this.attributeSetTable.ContainsKey(attributeSet.Name))
			{
				this.attributeSetTable[attributeSet.Name] = attributeSet;
				return;
			}
			((AttributeSetAction)this.attributeSetTable[attributeSet.Name]).Merge(attributeSet);
		}

		// Token: 0x0600267F RID: 9855 RVA: 0x000E75A8 File Offset: 0x000E57A8
		internal void AddTemplate(TemplateAction template)
		{
			XmlQualifiedName xmlQualifiedName = template.Mode;
			if (template.Name != null)
			{
				if (this.templateNameTable.ContainsKey(template.Name))
				{
					throw XsltException.Create("'{0}' is a duplicate template name.", new string[]
					{
						template.Name.ToString()
					});
				}
				this.templateNameTable[template.Name] = template;
			}
			if (template.MatchKey != -1)
			{
				if (this.modeManagers == null)
				{
					this.modeManagers = new Hashtable();
				}
				if (xmlQualifiedName == null)
				{
					xmlQualifiedName = XmlQualifiedName.Empty;
				}
				TemplateManager templateManager = (TemplateManager)this.modeManagers[xmlQualifiedName];
				if (templateManager == null)
				{
					templateManager = new TemplateManager(this, xmlQualifiedName);
					this.modeManagers[xmlQualifiedName] = templateManager;
					if (xmlQualifiedName.IsEmpty)
					{
						this.templates = templateManager;
					}
				}
				int templateId = this.templateCount + 1;
				this.templateCount = templateId;
				template.TemplateId = templateId;
				templateManager.AddTemplate(template);
			}
		}

		// Token: 0x06002680 RID: 9856 RVA: 0x000E7694 File Offset: 0x000E5894
		internal void ProcessTemplates()
		{
			if (this.modeManagers != null)
			{
				IDictionaryEnumerator enumerator = this.modeManagers.GetEnumerator();
				while (enumerator.MoveNext())
				{
					((TemplateManager)enumerator.Value).ProcessTemplates();
				}
			}
			if (this.imports != null)
			{
				for (int i = this.imports.Count - 1; i >= 0; i--)
				{
					((Stylesheet)this.imports[i]).ProcessTemplates();
				}
			}
		}

		// Token: 0x06002681 RID: 9857 RVA: 0x000E7708 File Offset: 0x000E5908
		internal void ReplaceNamespaceAlias(Compiler compiler)
		{
			if (this.modeManagers != null)
			{
				IDictionaryEnumerator enumerator = this.modeManagers.GetEnumerator();
				while (enumerator.MoveNext())
				{
					TemplateManager templateManager = (TemplateManager)enumerator.Value;
					if (templateManager.templates != null)
					{
						for (int i = 0; i < templateManager.templates.Count; i++)
						{
							((TemplateAction)templateManager.templates[i]).ReplaceNamespaceAlias(compiler);
						}
					}
				}
			}
			if (this.templateNameTable != null)
			{
				IDictionaryEnumerator enumerator2 = this.templateNameTable.GetEnumerator();
				while (enumerator2.MoveNext())
				{
					((TemplateAction)enumerator2.Value).ReplaceNamespaceAlias(compiler);
				}
			}
			if (this.imports != null)
			{
				for (int j = this.imports.Count - 1; j >= 0; j--)
				{
					((Stylesheet)this.imports[j]).ReplaceNamespaceAlias(compiler);
				}
			}
		}

		// Token: 0x06002682 RID: 9858 RVA: 0x000E77E0 File Offset: 0x000E59E0
		internal TemplateAction FindTemplate(Processor processor, XPathNavigator navigator, XmlQualifiedName mode)
		{
			TemplateAction templateAction = null;
			if (this.modeManagers != null)
			{
				TemplateManager templateManager = (TemplateManager)this.modeManagers[mode];
				if (templateManager != null)
				{
					templateAction = templateManager.FindTemplate(processor, navigator);
				}
			}
			if (templateAction == null)
			{
				templateAction = this.FindTemplateImports(processor, navigator, mode);
			}
			return templateAction;
		}

		// Token: 0x06002683 RID: 9859 RVA: 0x000E7824 File Offset: 0x000E5A24
		internal TemplateAction FindTemplateImports(Processor processor, XPathNavigator navigator, XmlQualifiedName mode)
		{
			TemplateAction templateAction = null;
			if (this.imports != null)
			{
				for (int i = this.imports.Count - 1; i >= 0; i--)
				{
					templateAction = ((Stylesheet)this.imports[i]).FindTemplate(processor, navigator, mode);
					if (templateAction != null)
					{
						return templateAction;
					}
				}
			}
			return templateAction;
		}

		// Token: 0x06002684 RID: 9860 RVA: 0x000E7874 File Offset: 0x000E5A74
		internal TemplateAction FindTemplate(Processor processor, XPathNavigator navigator)
		{
			TemplateAction templateAction = null;
			if (this.templates != null)
			{
				templateAction = this.templates.FindTemplate(processor, navigator);
			}
			if (templateAction == null)
			{
				templateAction = this.FindTemplateImports(processor, navigator);
			}
			return templateAction;
		}

		// Token: 0x06002685 RID: 9861 RVA: 0x000E78A8 File Offset: 0x000E5AA8
		internal TemplateAction FindTemplate(XmlQualifiedName name)
		{
			TemplateAction templateAction = null;
			if (this.templateNameTable != null)
			{
				templateAction = (TemplateAction)this.templateNameTable[name];
			}
			if (templateAction == null && this.imports != null)
			{
				for (int i = this.imports.Count - 1; i >= 0; i--)
				{
					templateAction = ((Stylesheet)this.imports[i]).FindTemplate(name);
					if (templateAction != null)
					{
						return templateAction;
					}
				}
			}
			return templateAction;
		}

		// Token: 0x06002686 RID: 9862 RVA: 0x000E7914 File Offset: 0x000E5B14
		internal TemplateAction FindTemplateImports(Processor processor, XPathNavigator navigator)
		{
			TemplateAction templateAction = null;
			if (this.imports != null)
			{
				for (int i = this.imports.Count - 1; i >= 0; i--)
				{
					templateAction = ((Stylesheet)this.imports[i]).FindTemplate(processor, navigator);
					if (templateAction != null)
					{
						return templateAction;
					}
				}
			}
			return templateAction;
		}

		// Token: 0x17000796 RID: 1942
		// (get) Token: 0x06002687 RID: 9863 RVA: 0x000E7962 File Offset: 0x000E5B62
		internal Hashtable ScriptObjectTypes
		{
			get
			{
				return this.scriptObjectTypes;
			}
		}

		// Token: 0x06002688 RID: 9864 RVA: 0x000E796A File Offset: 0x000E5B6A
		public Stylesheet()
		{
		}

		// Token: 0x04001E4D RID: 7757
		private ArrayList imports = new ArrayList();

		// Token: 0x04001E4E RID: 7758
		private Hashtable modeManagers;

		// Token: 0x04001E4F RID: 7759
		private Hashtable templateNameTable = new Hashtable();

		// Token: 0x04001E50 RID: 7760
		private Hashtable attributeSetTable;

		// Token: 0x04001E51 RID: 7761
		private int templateCount;

		// Token: 0x04001E52 RID: 7762
		private Hashtable queryKeyTable;

		// Token: 0x04001E53 RID: 7763
		private ArrayList whitespaceList;

		// Token: 0x04001E54 RID: 7764
		private bool whitespace;

		// Token: 0x04001E55 RID: 7765
		private Hashtable scriptObjectTypes = new Hashtable();

		// Token: 0x04001E56 RID: 7766
		private TemplateManager templates;

		// Token: 0x020003AE RID: 942
		private class WhitespaceElement
		{
			// Token: 0x17000797 RID: 1943
			// (get) Token: 0x06002689 RID: 9865 RVA: 0x000E7993 File Offset: 0x000E5B93
			internal double Priority
			{
				get
				{
					return this.priority;
				}
			}

			// Token: 0x17000798 RID: 1944
			// (get) Token: 0x0600268A RID: 9866 RVA: 0x000E799B File Offset: 0x000E5B9B
			internal int Key
			{
				get
				{
					return this.key;
				}
			}

			// Token: 0x17000799 RID: 1945
			// (get) Token: 0x0600268B RID: 9867 RVA: 0x000E79A3 File Offset: 0x000E5BA3
			internal bool PreserveSpace
			{
				get
				{
					return this.preserveSpace;
				}
			}

			// Token: 0x0600268C RID: 9868 RVA: 0x000E79AB File Offset: 0x000E5BAB
			internal WhitespaceElement(int Key, double priority, bool PreserveSpace)
			{
				this.key = Key;
				this.priority = priority;
				this.preserveSpace = PreserveSpace;
			}

			// Token: 0x0600268D RID: 9869 RVA: 0x000E79C8 File Offset: 0x000E5BC8
			internal void ReplaceValue(bool PreserveSpace)
			{
				this.preserveSpace = PreserveSpace;
			}

			// Token: 0x04001E57 RID: 7767
			private int key;

			// Token: 0x04001E58 RID: 7768
			private double priority;

			// Token: 0x04001E59 RID: 7769
			private bool preserveSpace;
		}
	}
}

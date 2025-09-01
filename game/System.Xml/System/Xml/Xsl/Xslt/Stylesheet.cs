using System;
using System.Collections.Generic;
using System.Xml.Xsl.Qil;

namespace System.Xml.Xsl.Xslt
{
	// Token: 0x020003F8 RID: 1016
	internal class Stylesheet : StylesheetLevel
	{
		// Token: 0x0600287C RID: 10364 RVA: 0x000F3DE8 File Offset: 0x000F1FE8
		public void AddTemplateMatch(Template template, QilLoop filter)
		{
			List<TemplateMatch> list;
			if (!this.TemplateMatches.TryGetValue(template.Mode, out list))
			{
				list = (this.TemplateMatches[template.Mode] = new List<TemplateMatch>());
			}
			list.Add(new TemplateMatch(template, filter));
		}

		// Token: 0x0600287D RID: 10365 RVA: 0x000F3E34 File Offset: 0x000F2034
		public void SortTemplateMatches()
		{
			foreach (QilName key in this.TemplateMatches.Keys)
			{
				this.TemplateMatches[key].Sort(TemplateMatch.Comparer);
			}
		}

		// Token: 0x0600287E RID: 10366 RVA: 0x000F3E9C File Offset: 0x000F209C
		public Stylesheet(Compiler compiler, int importPrecedence)
		{
			this.compiler = compiler;
			this.importPrecedence = importPrecedence;
			this.WhitespaceRules[0] = new List<WhitespaceRule>();
			this.WhitespaceRules[1] = new List<WhitespaceRule>();
			this.WhitespaceRules[2] = new List<WhitespaceRule>();
		}

		// Token: 0x170007D6 RID: 2006
		// (get) Token: 0x0600287F RID: 10367 RVA: 0x000F3F27 File Offset: 0x000F2127
		public int ImportPrecedence
		{
			get
			{
				return this.importPrecedence;
			}
		}

		// Token: 0x06002880 RID: 10368 RVA: 0x000F3F2F File Offset: 0x000F212F
		public void AddWhitespaceRule(int index, WhitespaceRule rule)
		{
			this.WhitespaceRules[index].Add(rule);
		}

		// Token: 0x06002881 RID: 10369 RVA: 0x000F3F40 File Offset: 0x000F2140
		public bool AddVarPar(VarPar var)
		{
			using (List<XslNode>.Enumerator enumerator = this.GlobalVarPars.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.Name.Equals(var.Name))
					{
						return this.compiler.AllGlobalVarPars.ContainsKey(var.Name);
					}
				}
			}
			this.GlobalVarPars.Add(var);
			return true;
		}

		// Token: 0x06002882 RID: 10370 RVA: 0x000F3FC8 File Offset: 0x000F21C8
		public bool AddTemplate(Template template)
		{
			template.ImportPrecedence = this.importPrecedence;
			int num = this.orderNumber;
			this.orderNumber = num + 1;
			template.OrderNumber = num;
			this.compiler.AllTemplates.Add(template);
			if (template.Name != null)
			{
				Template template2;
				if (!this.compiler.NamedTemplates.TryGetValue(template.Name, out template2))
				{
					this.compiler.NamedTemplates[template.Name] = template;
				}
				else if (template2.ImportPrecedence == template.ImportPrecedence)
				{
					return false;
				}
			}
			if (template.Match != null)
			{
				this.Templates.Add(template);
			}
			return true;
		}

		// Token: 0x04002000 RID: 8192
		private Compiler compiler;

		// Token: 0x04002001 RID: 8193
		public List<Uri> ImportHrefs = new List<Uri>();

		// Token: 0x04002002 RID: 8194
		public List<XslNode> GlobalVarPars = new List<XslNode>();

		// Token: 0x04002003 RID: 8195
		public Dictionary<QilName, AttributeSet> AttributeSets = new Dictionary<QilName, AttributeSet>();

		// Token: 0x04002004 RID: 8196
		private int importPrecedence;

		// Token: 0x04002005 RID: 8197
		private int orderNumber;

		// Token: 0x04002006 RID: 8198
		public List<WhitespaceRule>[] WhitespaceRules = new List<WhitespaceRule>[3];

		// Token: 0x04002007 RID: 8199
		public List<Template> Templates = new List<Template>();

		// Token: 0x04002008 RID: 8200
		public Dictionary<QilName, List<TemplateMatch>> TemplateMatches = new Dictionary<QilName, List<TemplateMatch>>();
	}
}

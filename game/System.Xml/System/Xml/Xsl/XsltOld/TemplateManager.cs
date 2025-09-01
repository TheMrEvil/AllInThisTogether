using System;
using System.Collections;
using System.Xml.XPath;

namespace System.Xml.Xsl.XsltOld
{
	// Token: 0x020003B3 RID: 947
	internal class TemplateManager
	{
		// Token: 0x1700079F RID: 1951
		// (get) Token: 0x060026A6 RID: 9894 RVA: 0x000E8081 File Offset: 0x000E6281
		internal XmlQualifiedName Mode
		{
			get
			{
				return this.mode;
			}
		}

		// Token: 0x060026A7 RID: 9895 RVA: 0x000E8089 File Offset: 0x000E6289
		internal TemplateManager(Stylesheet stylesheet, XmlQualifiedName mode)
		{
			this.mode = mode;
			this.stylesheet = stylesheet;
		}

		// Token: 0x060026A8 RID: 9896 RVA: 0x000E809F File Offset: 0x000E629F
		internal void AddTemplate(TemplateAction template)
		{
			if (this.templates == null)
			{
				this.templates = new ArrayList();
			}
			this.templates.Add(template);
		}

		// Token: 0x060026A9 RID: 9897 RVA: 0x000E80C1 File Offset: 0x000E62C1
		internal void ProcessTemplates()
		{
			if (this.templates != null)
			{
				this.templates.Sort(TemplateManager.s_TemplateComparer);
			}
		}

		// Token: 0x060026AA RID: 9898 RVA: 0x000E80DC File Offset: 0x000E62DC
		internal TemplateAction FindTemplate(Processor processor, XPathNavigator navigator)
		{
			if (this.templates == null)
			{
				return null;
			}
			for (int i = this.templates.Count - 1; i >= 0; i--)
			{
				TemplateAction templateAction = (TemplateAction)this.templates[i];
				int matchKey = templateAction.MatchKey;
				if (matchKey != -1 && processor.Matches(navigator, matchKey))
				{
					return templateAction;
				}
			}
			return null;
		}

		// Token: 0x060026AB RID: 9899 RVA: 0x000E8135 File Offset: 0x000E6335
		// Note: this type is marked as 'beforefieldinit'.
		static TemplateManager()
		{
		}

		// Token: 0x04001E64 RID: 7780
		private XmlQualifiedName mode;

		// Token: 0x04001E65 RID: 7781
		internal ArrayList templates;

		// Token: 0x04001E66 RID: 7782
		private Stylesheet stylesheet;

		// Token: 0x04001E67 RID: 7783
		private static TemplateManager.TemplateComparer s_TemplateComparer = new TemplateManager.TemplateComparer();

		// Token: 0x020003B4 RID: 948
		private class TemplateComparer : IComparer
		{
			// Token: 0x060026AC RID: 9900 RVA: 0x000E8144 File Offset: 0x000E6344
			public int Compare(object x, object y)
			{
				TemplateAction templateAction = (TemplateAction)x;
				TemplateAction templateAction2 = (TemplateAction)y;
				if (templateAction.Priority == templateAction2.Priority)
				{
					return templateAction.TemplateId - templateAction2.TemplateId;
				}
				if (templateAction.Priority <= templateAction2.Priority)
				{
					return -1;
				}
				return 1;
			}

			// Token: 0x060026AD RID: 9901 RVA: 0x0000216B File Offset: 0x0000036B
			public TemplateComparer()
			{
			}
		}
	}
}

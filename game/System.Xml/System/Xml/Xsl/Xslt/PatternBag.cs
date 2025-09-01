using System;
using System.Collections.Generic;
using System.Xml.Xsl.Qil;

namespace System.Xml.Xsl.Xslt
{
	// Token: 0x020003EB RID: 1003
	internal class PatternBag
	{
		// Token: 0x060027BE RID: 10174 RVA: 0x000EC626 File Offset: 0x000EA826
		public void Clear()
		{
			this.FixedNamePatterns.Clear();
			this.FixedNamePatternsNames.Clear();
			this.NonFixedNamePatterns.Clear();
		}

		// Token: 0x060027BF RID: 10175 RVA: 0x000EC64C File Offset: 0x000EA84C
		public void Add(Pattern pattern)
		{
			QilName qname = pattern.Match.QName;
			List<Pattern> list;
			if (qname == null)
			{
				list = this.NonFixedNamePatterns;
			}
			else if (!this.FixedNamePatterns.TryGetValue(qname, out list))
			{
				this.FixedNamePatternsNames.Add(qname);
				list = (this.FixedNamePatterns[qname] = new List<Pattern>());
			}
			list.Add(pattern);
		}

		// Token: 0x060027C0 RID: 10176 RVA: 0x000EC6AF File Offset: 0x000EA8AF
		public PatternBag()
		{
		}

		// Token: 0x04001F98 RID: 8088
		public Dictionary<QilName, List<Pattern>> FixedNamePatterns = new Dictionary<QilName, List<Pattern>>();

		// Token: 0x04001F99 RID: 8089
		public List<QilName> FixedNamePatternsNames = new List<QilName>();

		// Token: 0x04001F9A RID: 8090
		public List<Pattern> NonFixedNamePatterns = new List<Pattern>();
	}
}

using System;

namespace System.Xml.Xsl.Xslt
{
	// Token: 0x020003EA RID: 1002
	internal struct Pattern
	{
		// Token: 0x060027BD RID: 10173 RVA: 0x000EC616 File Offset: 0x000EA816
		public Pattern(TemplateMatch match, int priority)
		{
			this.Match = match;
			this.Priority = priority;
		}

		// Token: 0x04001F96 RID: 8086
		public readonly TemplateMatch Match;

		// Token: 0x04001F97 RID: 8087
		public readonly int Priority;
	}
}

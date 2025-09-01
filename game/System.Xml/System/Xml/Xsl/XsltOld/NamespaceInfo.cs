using System;

namespace System.Xml.Xsl.XsltOld
{
	// Token: 0x02000362 RID: 866
	internal class NamespaceInfo
	{
		// Token: 0x060023E8 RID: 9192 RVA: 0x000DD879 File Offset: 0x000DBA79
		internal NamespaceInfo(string prefix, string nameSpace, int stylesheetId)
		{
			this.prefix = prefix;
			this.nameSpace = nameSpace;
			this.stylesheetId = stylesheetId;
		}

		// Token: 0x04001CBC RID: 7356
		internal string prefix;

		// Token: 0x04001CBD RID: 7357
		internal string nameSpace;

		// Token: 0x04001CBE RID: 7358
		internal int stylesheetId;
	}
}

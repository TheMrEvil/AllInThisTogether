using System;

namespace System.Xml.Xsl.Xslt
{
	// Token: 0x020003DA RID: 986
	internal class NsAlias
	{
		// Token: 0x06002764 RID: 10084 RVA: 0x000EAC4F File Offset: 0x000E8E4F
		public NsAlias(string resultNsUri, string resultPrefix, int importPrecedence)
		{
			this.ResultNsUri = resultNsUri;
			this.ResultPrefix = resultPrefix;
			this.ImportPrecedence = importPrecedence;
		}

		// Token: 0x04001EE1 RID: 7905
		public readonly string ResultNsUri;

		// Token: 0x04001EE2 RID: 7906
		public readonly string ResultPrefix;

		// Token: 0x04001EE3 RID: 7907
		public readonly int ImportPrecedence;
	}
}

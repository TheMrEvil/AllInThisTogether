using System;

namespace System.Xml.Xsl
{
	// Token: 0x02000326 RID: 806
	internal interface ISourceLineInfo
	{
		// Token: 0x1700066A RID: 1642
		// (get) Token: 0x06002121 RID: 8481
		string Uri { get; }

		// Token: 0x1700066B RID: 1643
		// (get) Token: 0x06002122 RID: 8482
		bool IsNoSource { get; }

		// Token: 0x1700066C RID: 1644
		// (get) Token: 0x06002123 RID: 8483
		Location Start { get; }

		// Token: 0x1700066D RID: 1645
		// (get) Token: 0x06002124 RID: 8484
		Location End { get; }
	}
}

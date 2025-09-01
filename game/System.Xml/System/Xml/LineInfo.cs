using System;

namespace System.Xml
{
	// Token: 0x020001E6 RID: 486
	internal struct LineInfo
	{
		// Token: 0x0600132A RID: 4906 RVA: 0x00070D59 File Offset: 0x0006EF59
		public LineInfo(int lineNo, int linePos)
		{
			this.lineNo = lineNo;
			this.linePos = linePos;
		}

		// Token: 0x0600132B RID: 4907 RVA: 0x00070D59 File Offset: 0x0006EF59
		public void Set(int lineNo, int linePos)
		{
			this.lineNo = lineNo;
			this.linePos = linePos;
		}

		// Token: 0x040010EC RID: 4332
		internal int lineNo;

		// Token: 0x040010ED RID: 4333
		internal int linePos;
	}
}

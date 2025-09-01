using System;

namespace System.Xml.Xsl
{
	// Token: 0x0200032A RID: 810
	internal struct StringPair
	{
		// Token: 0x0600214D RID: 8525 RVA: 0x000D2ACD File Offset: 0x000D0CCD
		public StringPair(string left, string right)
		{
			this.left = left;
			this.right = right;
		}

		// Token: 0x17000679 RID: 1657
		// (get) Token: 0x0600214E RID: 8526 RVA: 0x000D2ADD File Offset: 0x000D0CDD
		public string Left
		{
			get
			{
				return this.left;
			}
		}

		// Token: 0x1700067A RID: 1658
		// (get) Token: 0x0600214F RID: 8527 RVA: 0x000D2AE5 File Offset: 0x000D0CE5
		public string Right
		{
			get
			{
				return this.right;
			}
		}

		// Token: 0x04001B8D RID: 7053
		private string left;

		// Token: 0x04001B8E RID: 7054
		private string right;
	}
}

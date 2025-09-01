using System;

namespace Mono.Xml
{
	// Token: 0x02000065 RID: 101
	internal class SmallXmlParserException : SystemException
	{
		// Token: 0x06000172 RID: 370 RVA: 0x0000584B File Offset: 0x00003A4B
		public SmallXmlParserException(string msg, int line, int column) : base(string.Format("{0}. At ({1},{2})", msg, line, column))
		{
			this.line = line;
			this.column = column;
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000173 RID: 371 RVA: 0x00005878 File Offset: 0x00003A78
		public int Line
		{
			get
			{
				return this.line;
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000174 RID: 372 RVA: 0x00005880 File Offset: 0x00003A80
		public int Column
		{
			get
			{
				return this.column;
			}
		}

		// Token: 0x04000E23 RID: 3619
		private int line;

		// Token: 0x04000E24 RID: 3620
		private int column;
	}
}

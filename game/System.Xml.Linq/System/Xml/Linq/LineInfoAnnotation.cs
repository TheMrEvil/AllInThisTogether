using System;

namespace System.Xml.Linq
{
	// Token: 0x02000018 RID: 24
	internal class LineInfoAnnotation
	{
		// Token: 0x060000CB RID: 203 RVA: 0x000050DB File Offset: 0x000032DB
		public LineInfoAnnotation(int lineNumber, int linePosition)
		{
			this.lineNumber = lineNumber;
			this.linePosition = linePosition;
		}

		// Token: 0x04000085 RID: 133
		internal int lineNumber;

		// Token: 0x04000086 RID: 134
		internal int linePosition;
	}
}

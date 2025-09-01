using System;

namespace System.Xml.Linq
{
	// Token: 0x0200005D RID: 93
	internal class XObjectChangeAnnotation
	{
		// Token: 0x06000388 RID: 904 RVA: 0x00003E36 File Offset: 0x00002036
		public XObjectChangeAnnotation()
		{
		}

		// Token: 0x040001D7 RID: 471
		internal EventHandler<XObjectChangeEventArgs> changing;

		// Token: 0x040001D8 RID: 472
		internal EventHandler<XObjectChangeEventArgs> changed;
	}
}

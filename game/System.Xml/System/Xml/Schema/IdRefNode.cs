using System;

namespace System.Xml.Schema
{
	// Token: 0x020005EA RID: 1514
	internal class IdRefNode
	{
		// Token: 0x06003C8D RID: 15501 RVA: 0x00151555 File Offset: 0x0014F755
		internal IdRefNode(IdRefNode next, string id, int lineNo, int linePos)
		{
			this.Id = id;
			this.LineNo = lineNo;
			this.LinePos = linePos;
			this.Next = next;
		}

		// Token: 0x04002BF6 RID: 11254
		internal string Id;

		// Token: 0x04002BF7 RID: 11255
		internal int LineNo;

		// Token: 0x04002BF8 RID: 11256
		internal int LinePos;

		// Token: 0x04002BF9 RID: 11257
		internal IdRefNode Next;
	}
}

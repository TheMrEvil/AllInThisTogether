using System;
using System.ComponentModel;

namespace System.Xml.Xsl.Runtime
{
	// Token: 0x02000444 RID: 1092
	[EditorBrowsable(EditorBrowsableState.Never)]
	public enum SetIteratorResult
	{
		// Token: 0x04002211 RID: 8721
		NoMoreNodes,
		// Token: 0x04002212 RID: 8722
		InitRightIterator,
		// Token: 0x04002213 RID: 8723
		NeedLeftNode,
		// Token: 0x04002214 RID: 8724
		NeedRightNode,
		// Token: 0x04002215 RID: 8725
		HaveCurrentNode
	}
}

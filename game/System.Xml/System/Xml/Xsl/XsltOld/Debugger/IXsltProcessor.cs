using System;

namespace System.Xml.Xsl.XsltOld.Debugger
{
	// Token: 0x020003D1 RID: 977
	internal interface IXsltProcessor
	{
		// Token: 0x170007BF RID: 1983
		// (get) Token: 0x0600273E RID: 10046
		int StackDepth { get; }

		// Token: 0x0600273F RID: 10047
		IStackFrame GetStackFrame(int depth);
	}
}

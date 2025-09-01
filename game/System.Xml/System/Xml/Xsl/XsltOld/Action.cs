using System;

namespace System.Xml.Xsl.XsltOld
{
	// Token: 0x0200034F RID: 847
	internal abstract class Action
	{
		// Token: 0x06002300 RID: 8960
		internal abstract void Execute(Processor processor, ActionFrame frame);

		// Token: 0x06002301 RID: 8961 RVA: 0x0000B528 File Offset: 0x00009728
		internal virtual void ReplaceNamespaceAlias(Compiler compiler)
		{
		}

		// Token: 0x06002302 RID: 8962 RVA: 0x000DAB8C File Offset: 0x000D8D8C
		internal virtual DbgData GetDbgData(ActionFrame frame)
		{
			return DbgData.Empty;
		}

		// Token: 0x06002303 RID: 8963 RVA: 0x0000216B File Offset: 0x0000036B
		protected Action()
		{
		}

		// Token: 0x04001C5F RID: 7263
		internal const int Initialized = 0;

		// Token: 0x04001C60 RID: 7264
		internal const int Finished = -1;
	}
}

using System;

namespace System.Xml.Xsl.XsltOld
{
	// Token: 0x02000388 RID: 904
	internal abstract class Event
	{
		// Token: 0x060024BE RID: 9406 RVA: 0x0000B528 File Offset: 0x00009728
		public virtual void ReplaceNamespaceAlias(Compiler compiler)
		{
		}

		// Token: 0x060024BF RID: 9407
		public abstract bool Output(Processor processor, ActionFrame frame);

		// Token: 0x060024C0 RID: 9408 RVA: 0x000E021F File Offset: 0x000DE41F
		internal void OnInstructionExecute(Processor processor)
		{
			processor.OnInstructionExecute();
		}

		// Token: 0x17000722 RID: 1826
		// (get) Token: 0x060024C1 RID: 9409 RVA: 0x000DAB8C File Offset: 0x000D8D8C
		internal virtual DbgData DbgData
		{
			get
			{
				return DbgData.Empty;
			}
		}

		// Token: 0x060024C2 RID: 9410 RVA: 0x0000216B File Offset: 0x0000036B
		protected Event()
		{
		}
	}
}

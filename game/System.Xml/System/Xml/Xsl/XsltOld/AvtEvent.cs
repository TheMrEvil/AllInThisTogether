using System;

namespace System.Xml.Xsl.XsltOld
{
	// Token: 0x02000357 RID: 855
	internal sealed class AvtEvent : TextEvent
	{
		// Token: 0x0600234C RID: 9036 RVA: 0x000DB9BB File Offset: 0x000D9BBB
		public AvtEvent(int key)
		{
			this.key = key;
		}

		// Token: 0x0600234D RID: 9037 RVA: 0x000DB9CA File Offset: 0x000D9BCA
		public override bool Output(Processor processor, ActionFrame frame)
		{
			return processor.TextEvent(processor.EvaluateString(frame, this.key));
		}

		// Token: 0x0600234E RID: 9038 RVA: 0x000DB9DF File Offset: 0x000D9BDF
		public override string Evaluate(Processor processor, ActionFrame frame)
		{
			return processor.EvaluateString(frame, this.key);
		}

		// Token: 0x04001C80 RID: 7296
		private int key;
	}
}

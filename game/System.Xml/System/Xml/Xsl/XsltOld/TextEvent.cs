using System;

namespace System.Xml.Xsl.XsltOld
{
	// Token: 0x020003B6 RID: 950
	internal class TextEvent : Event
	{
		// Token: 0x060026B3 RID: 9907 RVA: 0x000E8273 File Offset: 0x000E6473
		protected TextEvent()
		{
		}

		// Token: 0x060026B4 RID: 9908 RVA: 0x000E827B File Offset: 0x000E647B
		public TextEvent(string text)
		{
			this.text = text;
		}

		// Token: 0x060026B5 RID: 9909 RVA: 0x000E828C File Offset: 0x000E648C
		public TextEvent(Compiler compiler)
		{
			NavigatorInput input = compiler.Input;
			this.text = input.Value;
		}

		// Token: 0x060026B6 RID: 9910 RVA: 0x000E82B2 File Offset: 0x000E64B2
		public override bool Output(Processor processor, ActionFrame frame)
		{
			return processor.TextEvent(this.text);
		}

		// Token: 0x060026B7 RID: 9911 RVA: 0x000E82C0 File Offset: 0x000E64C0
		public virtual string Evaluate(Processor processor, ActionFrame frame)
		{
			return this.text;
		}

		// Token: 0x04001E6A RID: 7786
		private string text;
	}
}

using System;

namespace System.Xml.Xsl.XsltOld
{
	// Token: 0x020003BC RID: 956
	internal class BuiltInRuleTextAction : Action
	{
		// Token: 0x060026CF RID: 9935 RVA: 0x000E866C File Offset: 0x000E686C
		internal override void Execute(Processor processor, ActionFrame frame)
		{
			int state = frame.State;
			if (state != 0)
			{
				if (state != 2)
				{
					return;
				}
				processor.TextEvent(frame.StoredOutput);
				frame.Finished();
				return;
			}
			else
			{
				string text = processor.ValueOf(frame.NodeSet.Current);
				if (processor.TextEvent(text, false))
				{
					frame.Finished();
					return;
				}
				frame.StoredOutput = text;
				frame.State = 2;
				return;
			}
		}

		// Token: 0x060026D0 RID: 9936 RVA: 0x000DC2C5 File Offset: 0x000DA4C5
		public BuiltInRuleTextAction()
		{
		}

		// Token: 0x04001E77 RID: 7799
		private const int ResultStored = 2;
	}
}

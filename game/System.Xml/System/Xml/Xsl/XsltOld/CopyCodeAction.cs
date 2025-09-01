using System;
using System.Collections;

namespace System.Xml.Xsl.XsltOld
{
	// Token: 0x02000366 RID: 870
	internal class CopyCodeAction : Action
	{
		// Token: 0x06002412 RID: 9234 RVA: 0x000DF050 File Offset: 0x000DD250
		internal CopyCodeAction()
		{
			this.copyEvents = new ArrayList();
		}

		// Token: 0x06002413 RID: 9235 RVA: 0x000DF063 File Offset: 0x000DD263
		internal void AddEvent(Event copyEvent)
		{
			this.copyEvents.Add(copyEvent);
		}

		// Token: 0x06002414 RID: 9236 RVA: 0x000DF072 File Offset: 0x000DD272
		internal void AddEvents(ArrayList copyEvents)
		{
			this.copyEvents.AddRange(copyEvents);
		}

		// Token: 0x06002415 RID: 9237 RVA: 0x000DF080 File Offset: 0x000DD280
		internal override void ReplaceNamespaceAlias(Compiler compiler)
		{
			int count = this.copyEvents.Count;
			for (int i = 0; i < count; i++)
			{
				((Event)this.copyEvents[i]).ReplaceNamespaceAlias(compiler);
			}
		}

		// Token: 0x06002416 RID: 9238 RVA: 0x000DF0BC File Offset: 0x000DD2BC
		internal override void Execute(Processor processor, ActionFrame frame)
		{
			int state = frame.State;
			if (state != 0)
			{
				if (state != 2)
				{
					return;
				}
			}
			else
			{
				frame.Counter = 0;
				frame.State = 2;
			}
			while (processor.CanContinue && ((Event)this.copyEvents[frame.Counter]).Output(processor, frame))
			{
				if (frame.IncrementCounter() >= this.copyEvents.Count)
				{
					frame.Finished();
					return;
				}
			}
		}

		// Token: 0x06002417 RID: 9239 RVA: 0x000DF129 File Offset: 0x000DD329
		internal override DbgData GetDbgData(ActionFrame frame)
		{
			return ((Event)this.copyEvents[frame.Counter]).DbgData;
		}

		// Token: 0x04001CCF RID: 7375
		private const int Outputting = 2;

		// Token: 0x04001CD0 RID: 7376
		private ArrayList copyEvents;
	}
}

using System;
using System.Collections;
using System.Text;

namespace System.Xml.Xsl.XsltOld
{
	// Token: 0x02000356 RID: 854
	internal sealed class Avt
	{
		// Token: 0x06002347 RID: 9031 RVA: 0x000DB8D7 File Offset: 0x000D9AD7
		private Avt(string constAvt)
		{
			this.constAvt = constAvt;
		}

		// Token: 0x06002348 RID: 9032 RVA: 0x000DB8E8 File Offset: 0x000D9AE8
		private Avt(ArrayList eventList)
		{
			this.events = new TextEvent[eventList.Count];
			for (int i = 0; i < eventList.Count; i++)
			{
				this.events[i] = (TextEvent)eventList[i];
			}
		}

		// Token: 0x17000703 RID: 1795
		// (get) Token: 0x06002349 RID: 9033 RVA: 0x000DB931 File Offset: 0x000D9B31
		public bool IsConstant
		{
			get
			{
				return this.events == null;
			}
		}

		// Token: 0x0600234A RID: 9034 RVA: 0x000DB93C File Offset: 0x000D9B3C
		internal string Evaluate(Processor processor, ActionFrame frame)
		{
			if (this.IsConstant)
			{
				return this.constAvt;
			}
			StringBuilder sharedStringBuilder = processor.GetSharedStringBuilder();
			for (int i = 0; i < this.events.Length; i++)
			{
				sharedStringBuilder.Append(this.events[i].Evaluate(processor, frame));
			}
			processor.ReleaseSharedStringBuilder();
			return sharedStringBuilder.ToString();
		}

		// Token: 0x0600234B RID: 9035 RVA: 0x000DB994 File Offset: 0x000D9B94
		internal static Avt CompileAvt(Compiler compiler, string avtText)
		{
			bool flag;
			ArrayList eventList = compiler.CompileAvt(avtText, out flag);
			if (!flag)
			{
				return new Avt(eventList);
			}
			return new Avt(avtText);
		}

		// Token: 0x04001C7E RID: 7294
		private string constAvt;

		// Token: 0x04001C7F RID: 7295
		private TextEvent[] events;
	}
}

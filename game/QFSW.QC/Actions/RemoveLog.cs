using System;

namespace QFSW.QC.Actions
{
	// Token: 0x02000076 RID: 118
	public class RemoveLog : ICommandAction
	{
		// Token: 0x1700005D RID: 93
		// (get) Token: 0x06000270 RID: 624 RVA: 0x0000A887 File Offset: 0x00008A87
		public bool IsFinished
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x06000271 RID: 625 RVA: 0x0000A88A File Offset: 0x00008A8A
		public bool StartsIdle
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000272 RID: 626 RVA: 0x0000A88D File Offset: 0x00008A8D
		public void Start(ActionContext context)
		{
		}

		// Token: 0x06000273 RID: 627 RVA: 0x0000A88F File Offset: 0x00008A8F
		public void Finalize(ActionContext context)
		{
			context.Console.RemoveLogTrace();
		}

		// Token: 0x06000274 RID: 628 RVA: 0x0000A89C File Offset: 0x00008A9C
		public RemoveLog()
		{
		}
	}
}

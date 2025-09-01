using System;

namespace QFSW.QC.Actions
{
	// Token: 0x0200007E RID: 126
	public class WaitWhile : ICommandAction
	{
		// Token: 0x17000065 RID: 101
		// (get) Token: 0x0600028B RID: 651 RVA: 0x0000AA30 File Offset: 0x00008C30
		public bool IsFinished
		{
			get
			{
				return this._condition();
			}
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x0600028C RID: 652 RVA: 0x0000AA3D File Offset: 0x00008C3D
		public bool StartsIdle
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600028D RID: 653 RVA: 0x0000AA40 File Offset: 0x00008C40
		public WaitWhile(Func<bool> condition)
		{
			this._condition = condition;
		}

		// Token: 0x0600028E RID: 654 RVA: 0x0000AA4F File Offset: 0x00008C4F
		public void Start(ActionContext context)
		{
		}

		// Token: 0x0600028F RID: 655 RVA: 0x0000AA51 File Offset: 0x00008C51
		public void Finalize(ActionContext context)
		{
		}

		// Token: 0x04000166 RID: 358
		private readonly Func<bool> _condition;
	}
}

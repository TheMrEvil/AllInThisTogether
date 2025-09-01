using System;
using System.Collections.Generic;

namespace QFSW.QC.Actions
{
	// Token: 0x02000070 RID: 112
	public class Composite : ICommandAction
	{
		// Token: 0x17000053 RID: 83
		// (get) Token: 0x0600024E RID: 590 RVA: 0x0000A660 File Offset: 0x00008860
		public bool IsFinished
		{
			get
			{
				return this._actions.Execute(this._context) == ActionState.Complete;
			}
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x0600024F RID: 591 RVA: 0x0000A676 File Offset: 0x00008876
		public bool StartsIdle
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000250 RID: 592 RVA: 0x0000A679 File Offset: 0x00008879
		public Composite(IEnumerator<ICommandAction> actions)
		{
			this._actions = actions;
		}

		// Token: 0x06000251 RID: 593 RVA: 0x0000A688 File Offset: 0x00008888
		public Composite(IEnumerable<ICommandAction> actions) : this(actions.GetEnumerator())
		{
		}

		// Token: 0x06000252 RID: 594 RVA: 0x0000A696 File Offset: 0x00008896
		public void Start(ActionContext context)
		{
			this._context = context;
		}

		// Token: 0x06000253 RID: 595 RVA: 0x0000A69F File Offset: 0x0000889F
		public void Finalize(ActionContext context)
		{
		}

		// Token: 0x0400014F RID: 335
		private ActionContext _context;

		// Token: 0x04000150 RID: 336
		private readonly IEnumerator<ICommandAction> _actions;
	}
}

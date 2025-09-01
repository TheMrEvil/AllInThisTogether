using System;

namespace QFSW.QC.Actions
{
	// Token: 0x02000071 RID: 113
	public class Custom : ICommandAction
	{
		// Token: 0x06000254 RID: 596 RVA: 0x0000A6A1 File Offset: 0x000088A1
		public Custom(Func<bool> isFinished, Func<bool> startsIdle, Action<ActionContext> start, Action<ActionContext> finalize)
		{
			this._isFinished = isFinished;
			this._startsIdle = startsIdle;
			this._start = start;
			this._finalize = finalize;
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x06000255 RID: 597 RVA: 0x0000A6C6 File Offset: 0x000088C6
		public bool IsFinished
		{
			get
			{
				return this._isFinished();
			}
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x06000256 RID: 598 RVA: 0x0000A6D3 File Offset: 0x000088D3
		public bool StartsIdle
		{
			get
			{
				return this._startsIdle();
			}
		}

		// Token: 0x06000257 RID: 599 RVA: 0x0000A6E0 File Offset: 0x000088E0
		public void Start(ActionContext context)
		{
			this._start(context);
		}

		// Token: 0x06000258 RID: 600 RVA: 0x0000A6EE File Offset: 0x000088EE
		public void Finalize(ActionContext context)
		{
			this._finalize(context);
		}

		// Token: 0x04000151 RID: 337
		private readonly Func<bool> _isFinished;

		// Token: 0x04000152 RID: 338
		private readonly Func<bool> _startsIdle;

		// Token: 0x04000153 RID: 339
		private readonly Action<ActionContext> _start;

		// Token: 0x04000154 RID: 340
		private readonly Action<ActionContext> _finalize;
	}
}

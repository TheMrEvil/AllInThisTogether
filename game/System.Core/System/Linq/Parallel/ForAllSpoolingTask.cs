using System;

namespace System.Linq.Parallel
{
	// Token: 0x020001E3 RID: 483
	internal class ForAllSpoolingTask<TInputOutput, TIgnoreKey> : SpoolingTaskBase
	{
		// Token: 0x06000BE9 RID: 3049 RVA: 0x00029F5B File Offset: 0x0002815B
		internal ForAllSpoolingTask(int taskIndex, QueryTaskGroupState groupState, QueryOperatorEnumerator<TInputOutput, TIgnoreKey> source) : base(taskIndex, groupState)
		{
			this._source = source;
		}

		// Token: 0x06000BEA RID: 3050 RVA: 0x00029F6C File Offset: 0x0002816C
		protected override void SpoolingWork()
		{
			TInputOutput tinputOutput = default(TInputOutput);
			TIgnoreKey tignoreKey = default(TIgnoreKey);
			while (this._source.MoveNext(ref tinputOutput, ref tignoreKey))
			{
			}
		}

		// Token: 0x06000BEB RID: 3051 RVA: 0x00029F9A File Offset: 0x0002819A
		protected override void SpoolingFinally()
		{
			base.SpoolingFinally();
			this._source.Dispose();
		}

		// Token: 0x04000875 RID: 2165
		private QueryOperatorEnumerator<TInputOutput, TIgnoreKey> _source;
	}
}

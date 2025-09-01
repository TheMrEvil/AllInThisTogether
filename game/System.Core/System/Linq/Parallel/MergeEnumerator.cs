using System;
using System.Collections;
using System.Collections.Generic;

namespace System.Linq.Parallel
{
	// Token: 0x02000105 RID: 261
	internal abstract class MergeEnumerator<TInputOutput> : IEnumerator<!0>, IDisposable, IEnumerator
	{
		// Token: 0x060008A6 RID: 2214 RVA: 0x0001DC94 File Offset: 0x0001BE94
		protected MergeEnumerator(QueryTaskGroupState taskGroupState)
		{
			this._taskGroupState = taskGroupState;
		}

		// Token: 0x170000FB RID: 251
		// (get) Token: 0x060008A7 RID: 2215
		public abstract TInputOutput Current { get; }

		// Token: 0x060008A8 RID: 2216
		public abstract bool MoveNext();

		// Token: 0x170000FC RID: 252
		// (get) Token: 0x060008A9 RID: 2217 RVA: 0x0001DCA3 File Offset: 0x0001BEA3
		object IEnumerator.Current
		{
			get
			{
				return ((IEnumerator<!0>)this).Current;
			}
		}

		// Token: 0x060008AA RID: 2218 RVA: 0x00003A59 File Offset: 0x00001C59
		public virtual void Reset()
		{
		}

		// Token: 0x060008AB RID: 2219 RVA: 0x0001DCB0 File Offset: 0x0001BEB0
		public virtual void Dispose()
		{
			if (!this._taskGroupState.IsAlreadyEnded)
			{
				this._taskGroupState.QueryEnd(true);
			}
		}

		// Token: 0x04000607 RID: 1543
		protected QueryTaskGroupState _taskGroupState;
	}
}

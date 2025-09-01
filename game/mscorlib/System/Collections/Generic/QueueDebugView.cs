using System;
using System.Diagnostics;

namespace System.Collections.Generic
{
	// Token: 0x02000AB2 RID: 2738
	internal sealed class QueueDebugView<T>
	{
		// Token: 0x060061F7 RID: 25079 RVA: 0x0014799A File Offset: 0x00145B9A
		public QueueDebugView(Queue<T> queue)
		{
			if (queue == null)
			{
				throw new ArgumentNullException("queue");
			}
			this._queue = queue;
		}

		// Token: 0x1700116A RID: 4458
		// (get) Token: 0x060061F8 RID: 25080 RVA: 0x001479B7 File Offset: 0x00145BB7
		[DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
		public T[] Items
		{
			get
			{
				return this._queue.ToArray();
			}
		}

		// Token: 0x04003A1B RID: 14875
		private readonly Queue<T> _queue;
	}
}

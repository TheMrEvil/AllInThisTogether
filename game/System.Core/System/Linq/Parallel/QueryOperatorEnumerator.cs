using System;
using System.Collections;
using System.Collections.Generic;

namespace System.Linq.Parallel
{
	// Token: 0x02000189 RID: 393
	internal abstract class QueryOperatorEnumerator<TElement, TKey>
	{
		// Token: 0x06000A76 RID: 2678
		internal abstract bool MoveNext(ref TElement currentElement, ref TKey currentKey);

		// Token: 0x06000A77 RID: 2679 RVA: 0x0002529E File Offset: 0x0002349E
		public void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x06000A78 RID: 2680 RVA: 0x00003A59 File Offset: 0x00001C59
		protected virtual void Dispose(bool disposing)
		{
		}

		// Token: 0x06000A79 RID: 2681 RVA: 0x00003A59 File Offset: 0x00001C59
		internal virtual void Reset()
		{
		}

		// Token: 0x06000A7A RID: 2682 RVA: 0x000252A7 File Offset: 0x000234A7
		internal IEnumerator<TElement> AsClassicEnumerator()
		{
			return new QueryOperatorEnumerator<TElement, TKey>.QueryOperatorClassicEnumerator(this);
		}

		// Token: 0x06000A7B RID: 2683 RVA: 0x00002162 File Offset: 0x00000362
		protected QueryOperatorEnumerator()
		{
		}

		// Token: 0x0200018A RID: 394
		private class QueryOperatorClassicEnumerator : IEnumerator<!0>, IDisposable, IEnumerator
		{
			// Token: 0x06000A7C RID: 2684 RVA: 0x000252AF File Offset: 0x000234AF
			internal QueryOperatorClassicEnumerator(QueryOperatorEnumerator<TElement, TKey> operatorEnumerator)
			{
				this._operatorEnumerator = operatorEnumerator;
			}

			// Token: 0x06000A7D RID: 2685 RVA: 0x000252C0 File Offset: 0x000234C0
			public bool MoveNext()
			{
				TKey tkey = default(TKey);
				return this._operatorEnumerator.MoveNext(ref this._current, ref tkey);
			}

			// Token: 0x17000127 RID: 295
			// (get) Token: 0x06000A7E RID: 2686 RVA: 0x000252E8 File Offset: 0x000234E8
			public TElement Current
			{
				get
				{
					return this._current;
				}
			}

			// Token: 0x17000128 RID: 296
			// (get) Token: 0x06000A7F RID: 2687 RVA: 0x000252F0 File Offset: 0x000234F0
			object IEnumerator.Current
			{
				get
				{
					return this._current;
				}
			}

			// Token: 0x06000A80 RID: 2688 RVA: 0x000252FD File Offset: 0x000234FD
			public void Dispose()
			{
				this._operatorEnumerator.Dispose();
				this._operatorEnumerator = null;
			}

			// Token: 0x06000A81 RID: 2689 RVA: 0x00025311 File Offset: 0x00023511
			public void Reset()
			{
				this._operatorEnumerator.Reset();
			}

			// Token: 0x0400074D RID: 1869
			private QueryOperatorEnumerator<TElement, TKey> _operatorEnumerator;

			// Token: 0x0400074E RID: 1870
			private TElement _current;
		}
	}
}

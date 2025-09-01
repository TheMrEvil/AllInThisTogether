using System;

namespace System.Linq.Parallel
{
	// Token: 0x020001C7 RID: 455
	internal class SortQueryOperatorEnumerator<TInputOutput, TKey, TSortKey> : QueryOperatorEnumerator<TInputOutput, TSortKey>
	{
		// Token: 0x06000B84 RID: 2948 RVA: 0x0002849B File Offset: 0x0002669B
		internal SortQueryOperatorEnumerator(QueryOperatorEnumerator<TInputOutput, TKey> source, Func<TInputOutput, TSortKey> keySelector)
		{
			this._source = source;
			this._keySelector = keySelector;
		}

		// Token: 0x06000B85 RID: 2949 RVA: 0x000284B4 File Offset: 0x000266B4
		internal override bool MoveNext(ref TInputOutput currentElement, ref TSortKey currentKey)
		{
			TKey tkey = default(TKey);
			if (!this._source.MoveNext(ref currentElement, ref tkey))
			{
				return false;
			}
			currentKey = this._keySelector(currentElement);
			return true;
		}

		// Token: 0x06000B86 RID: 2950 RVA: 0x000284F3 File Offset: 0x000266F3
		protected override void Dispose(bool disposing)
		{
			this._source.Dispose();
		}

		// Token: 0x04000805 RID: 2053
		private readonly QueryOperatorEnumerator<TInputOutput, TKey> _source;

		// Token: 0x04000806 RID: 2054
		private readonly Func<TInputOutput, TSortKey> _keySelector;
	}
}

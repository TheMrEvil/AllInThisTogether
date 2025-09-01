using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace System.Data
{
	// Token: 0x0200000F RID: 15
	internal class SortExpressionBuilder<T> : IComparer<List<object>>
	{
		// Token: 0x0600004E RID: 78 RVA: 0x00002E08 File Offset: 0x00001008
		internal void Add(Func<T, object> keySelector, Comparison<object> compare, bool isOrderBy)
		{
			if (isOrderBy)
			{
				this._currentSelector = this._selectors.AddFirst(keySelector);
				this._currentComparer = this._comparers.AddFirst(compare);
				return;
			}
			this._currentSelector = this._selectors.AddAfter(this._currentSelector, keySelector);
			this._currentComparer = this._comparers.AddAfter(this._currentComparer, compare);
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00002E70 File Offset: 0x00001070
		public List<object> Select(T row)
		{
			List<object> list = new List<object>();
			foreach (Func<T, object> func in this._selectors)
			{
				list.Add(func(row));
			}
			return list;
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00002ED0 File Offset: 0x000010D0
		public int Compare(List<object> a, List<object> b)
		{
			int num = 0;
			foreach (Comparison<object> comparison in this._comparers)
			{
				int num2 = comparison(a[num], b[num]);
				if (num2 != 0)
				{
					return num2;
				}
				num++;
			}
			return 0;
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000051 RID: 81 RVA: 0x00002F40 File Offset: 0x00001140
		internal int Count
		{
			get
			{
				return this._selectors.Count;
			}
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00002F50 File Offset: 0x00001150
		internal SortExpressionBuilder<T> Clone()
		{
			SortExpressionBuilder<T> sortExpressionBuilder = new SortExpressionBuilder<T>();
			foreach (Func<T, object> func in this._selectors)
			{
				if (func == this._currentSelector.Value)
				{
					sortExpressionBuilder._currentSelector = sortExpressionBuilder._selectors.AddLast(func);
				}
				else
				{
					sortExpressionBuilder._selectors.AddLast(func);
				}
			}
			foreach (Comparison<object> comparison in this._comparers)
			{
				if (comparison == this._currentComparer.Value)
				{
					sortExpressionBuilder._currentComparer = sortExpressionBuilder._comparers.AddLast(comparison);
				}
				else
				{
					sortExpressionBuilder._comparers.AddLast(comparison);
				}
			}
			return sortExpressionBuilder;
		}

		// Token: 0x06000053 RID: 83 RVA: 0x0000304C File Offset: 0x0000124C
		internal SortExpressionBuilder<TResult> CloneCast<TResult>()
		{
			SortExpressionBuilder<TResult> sortExpressionBuilder = new SortExpressionBuilder<TResult>();
			using (LinkedList<Func<T, object>>.Enumerator enumerator = this._selectors.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					Func<T, object> selector = enumerator.Current;
					if (selector == this._currentSelector.Value)
					{
						sortExpressionBuilder._currentSelector = sortExpressionBuilder._selectors.AddLast((TResult r) => selector((T)((object)r)));
					}
					else
					{
						sortExpressionBuilder._selectors.AddLast((TResult r) => selector((T)((object)r)));
					}
				}
			}
			foreach (Comparison<object> comparison in this._comparers)
			{
				if (comparison == this._currentComparer.Value)
				{
					sortExpressionBuilder._currentComparer = sortExpressionBuilder._comparers.AddLast(comparison);
				}
				else
				{
					sortExpressionBuilder._comparers.AddLast(comparison);
				}
			}
			return sortExpressionBuilder;
		}

		// Token: 0x06000054 RID: 84 RVA: 0x0000316C File Offset: 0x0000136C
		public SortExpressionBuilder()
		{
		}

		// Token: 0x04000041 RID: 65
		private LinkedList<Func<T, object>> _selectors = new LinkedList<Func<T, object>>();

		// Token: 0x04000042 RID: 66
		private LinkedList<Comparison<object>> _comparers = new LinkedList<Comparison<object>>();

		// Token: 0x04000043 RID: 67
		private LinkedListNode<Func<T, object>> _currentSelector;

		// Token: 0x04000044 RID: 68
		private LinkedListNode<Comparison<object>> _currentComparer;

		// Token: 0x02000010 RID: 16
		[CompilerGenerated]
		private sealed class <>c__DisplayClass10_0<TResult>
		{
			// Token: 0x06000055 RID: 85 RVA: 0x000021DF File Offset: 0x000003DF
			public <>c__DisplayClass10_0()
			{
			}

			// Token: 0x06000056 RID: 86 RVA: 0x0000318A File Offset: 0x0000138A
			internal object <CloneCast>b__0(TResult r)
			{
				return this.selector((T)((object)r));
			}

			// Token: 0x06000057 RID: 87 RVA: 0x0000318A File Offset: 0x0000138A
			internal object <CloneCast>b__1(TResult r)
			{
				return this.selector((T)((object)r));
			}

			// Token: 0x04000045 RID: 69
			public Func<T, object> selector;
		}
	}
}

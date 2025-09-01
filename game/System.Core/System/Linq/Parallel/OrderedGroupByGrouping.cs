using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace System.Linq.Parallel
{
	// Token: 0x020001AB RID: 427
	internal class OrderedGroupByGrouping<TGroupKey, TOrderKey, TElement> : IGrouping<TGroupKey, TElement>, IEnumerable<TElement>, IEnumerable
	{
		// Token: 0x06000B16 RID: 2838 RVA: 0x00026F57 File Offset: 0x00025157
		internal OrderedGroupByGrouping(TGroupKey groupKey, IComparer<TOrderKey> orderComparer)
		{
			this._groupKey = groupKey;
			this._values = new GrowingArray<TElement>();
			this._orderKeys = new GrowingArray<TOrderKey>();
			this._orderComparer = orderComparer;
			this._wrappedComparer = new OrderedGroupByGrouping<TGroupKey, TOrderKey, TElement>.KeyAndValuesComparer(this._orderComparer);
		}

		// Token: 0x17000143 RID: 323
		// (get) Token: 0x06000B17 RID: 2839 RVA: 0x00026F94 File Offset: 0x00025194
		TGroupKey IGrouping<!0, !2>.Key
		{
			get
			{
				return this._groupKey;
			}
		}

		// Token: 0x06000B18 RID: 2840 RVA: 0x00026F9C File Offset: 0x0002519C
		IEnumerator<TElement> IEnumerable<!2>.GetEnumerator()
		{
			int valueCount = this._values.Count;
			TElement[] valueArray = this._values.InternalArray;
			int num;
			for (int i = 0; i < valueCount; i = num + 1)
			{
				yield return valueArray[i];
				num = i;
			}
			yield break;
		}

		// Token: 0x06000B19 RID: 2841 RVA: 0x00026FAB File Offset: 0x000251AB
		IEnumerator IEnumerable.GetEnumerator()
		{
			return ((IEnumerable<!2>)this).GetEnumerator();
		}

		// Token: 0x06000B1A RID: 2842 RVA: 0x00026FB3 File Offset: 0x000251B3
		internal void Add(TElement value, TOrderKey orderKey)
		{
			this._values.Add(value);
			this._orderKeys.Add(orderKey);
		}

		// Token: 0x06000B1B RID: 2843 RVA: 0x00026FD0 File Offset: 0x000251D0
		internal void DoneAdding()
		{
			List<KeyValuePair<TOrderKey, TElement>> list = new List<KeyValuePair<TOrderKey, TElement>>();
			for (int i = 0; i < this._orderKeys.InternalArray.Length; i++)
			{
				list.Add(new KeyValuePair<TOrderKey, TElement>(this._orderKeys.InternalArray[i], this._values.InternalArray[i]));
			}
			list.Sort(0, this._values.Count, this._wrappedComparer);
			for (int j = 0; j < this._values.InternalArray.Length; j++)
			{
				this._orderKeys.InternalArray[j] = list[j].Key;
				this._values.InternalArray[j] = list[j].Value;
			}
		}

		// Token: 0x040007AC RID: 1964
		private TGroupKey _groupKey;

		// Token: 0x040007AD RID: 1965
		private GrowingArray<TElement> _values;

		// Token: 0x040007AE RID: 1966
		private GrowingArray<TOrderKey> _orderKeys;

		// Token: 0x040007AF RID: 1967
		private IComparer<TOrderKey> _orderComparer;

		// Token: 0x040007B0 RID: 1968
		private OrderedGroupByGrouping<TGroupKey, TOrderKey, TElement>.KeyAndValuesComparer _wrappedComparer;

		// Token: 0x020001AC RID: 428
		private class KeyAndValuesComparer : IComparer<KeyValuePair<TOrderKey, TElement>>
		{
			// Token: 0x06000B1C RID: 2844 RVA: 0x00027098 File Offset: 0x00025298
			public KeyAndValuesComparer(IComparer<TOrderKey> comparer)
			{
				this.myComparer = comparer;
			}

			// Token: 0x06000B1D RID: 2845 RVA: 0x000270A7 File Offset: 0x000252A7
			public int Compare(KeyValuePair<TOrderKey, TElement> x, KeyValuePair<TOrderKey, TElement> y)
			{
				return this.myComparer.Compare(x.Key, y.Key);
			}

			// Token: 0x040007B1 RID: 1969
			private IComparer<TOrderKey> myComparer;
		}

		// Token: 0x020001AD RID: 429
		[CompilerGenerated]
		private sealed class <System-Collections-Generic-IEnumerable<TElement>-GetEnumerator>d__8 : IEnumerator<TElement>, IDisposable, IEnumerator
		{
			// Token: 0x06000B1E RID: 2846 RVA: 0x000270C2 File Offset: 0x000252C2
			[DebuggerHidden]
			public <System-Collections-Generic-IEnumerable<TElement>-GetEnumerator>d__8(int <>1__state)
			{
				this.<>1__state = <>1__state;
			}

			// Token: 0x06000B1F RID: 2847 RVA: 0x00003A59 File Offset: 0x00001C59
			[DebuggerHidden]
			void IDisposable.Dispose()
			{
			}

			// Token: 0x06000B20 RID: 2848 RVA: 0x000270D4 File Offset: 0x000252D4
			bool IEnumerator.MoveNext()
			{
				int num = this.<>1__state;
				OrderedGroupByGrouping<TGroupKey, TOrderKey, TElement> orderedGroupByGrouping = this;
				if (num != 0)
				{
					if (num != 1)
					{
						return false;
					}
					this.<>1__state = -1;
					int num2 = i;
					i = num2 + 1;
				}
				else
				{
					this.<>1__state = -1;
					valueCount = orderedGroupByGrouping._values.Count;
					valueArray = orderedGroupByGrouping._values.InternalArray;
					i = 0;
				}
				if (i >= valueCount)
				{
					return false;
				}
				this.<>2__current = valueArray[i];
				this.<>1__state = 1;
				return true;
			}

			// Token: 0x17000144 RID: 324
			// (get) Token: 0x06000B21 RID: 2849 RVA: 0x00027170 File Offset: 0x00025370
			TElement IEnumerator<!2>.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x06000B22 RID: 2850 RVA: 0x000080E3 File Offset: 0x000062E3
			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x17000145 RID: 325
			// (get) Token: 0x06000B23 RID: 2851 RVA: 0x00027178 File Offset: 0x00025378
			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x040007B2 RID: 1970
			private int <>1__state;

			// Token: 0x040007B3 RID: 1971
			private TElement <>2__current;

			// Token: 0x040007B4 RID: 1972
			public OrderedGroupByGrouping<TGroupKey, TOrderKey, TElement> <>4__this;

			// Token: 0x040007B5 RID: 1973
			private int <valueCount>5__2;

			// Token: 0x040007B6 RID: 1974
			private TElement[] <valueArray>5__3;

			// Token: 0x040007B7 RID: 1975
			private int <i>5__4;
		}
	}
}

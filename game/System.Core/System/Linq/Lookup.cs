using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Unity;

namespace System.Linq
{
	/// <summary>Represents a collection of keys each mapped to one or more values.</summary>
	/// <typeparam name="TKey">The type of the keys in the <see cref="T:System.Linq.Lookup`2" />.</typeparam>
	/// <typeparam name="TElement">The type of the elements of each <see cref="T:System.Collections.Generic.IEnumerable`1" /> value in the <see cref="T:System.Linq.Lookup`2" />.</typeparam>
	// Token: 0x020000D8 RID: 216
	[DebuggerDisplay("Count = {Count}")]
	[DebuggerTypeProxy(typeof(SystemLinq_LookupDebugView<, >))]
	public class Lookup<TKey, TElement> : ILookup<TKey, TElement>, IEnumerable<IGrouping<TKey, TElement>>, IEnumerable, IIListProvider<IGrouping<TKey, TElement>>
	{
		// Token: 0x060007AB RID: 1963 RVA: 0x0001B020 File Offset: 0x00019220
		internal static Lookup<TKey, TElement> Create<TSource>(IEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TElement> elementSelector, IEqualityComparer<TKey> comparer)
		{
			Lookup<TKey, TElement> lookup = new Lookup<TKey, TElement>(comparer);
			foreach (TSource arg in source)
			{
				lookup.GetGrouping(keySelector(arg), true).Add(elementSelector(arg));
			}
			return lookup;
		}

		// Token: 0x060007AC RID: 1964 RVA: 0x0001B084 File Offset: 0x00019284
		internal static Lookup<TKey, TElement> Create(IEnumerable<TElement> source, Func<TElement, TKey> keySelector, IEqualityComparer<TKey> comparer)
		{
			Lookup<TKey, TElement> lookup = new Lookup<TKey, TElement>(comparer);
			foreach (TElement telement in source)
			{
				lookup.GetGrouping(keySelector(telement), true).Add(telement);
			}
			return lookup;
		}

		// Token: 0x060007AD RID: 1965 RVA: 0x0001B0E4 File Offset: 0x000192E4
		internal static Lookup<TKey, TElement> CreateForJoin(IEnumerable<TElement> source, Func<TElement, TKey> keySelector, IEqualityComparer<TKey> comparer)
		{
			Lookup<TKey, TElement> lookup = new Lookup<TKey, TElement>(comparer);
			foreach (TElement telement in source)
			{
				TKey tkey = keySelector(telement);
				if (tkey != null)
				{
					lookup.GetGrouping(tkey, true).Add(telement);
				}
			}
			return lookup;
		}

		// Token: 0x060007AE RID: 1966 RVA: 0x0001B14C File Offset: 0x0001934C
		private Lookup(IEqualityComparer<TKey> comparer)
		{
			this._comparer = (comparer ?? EqualityComparer<TKey>.Default);
			this._groupings = new Grouping<TKey, TElement>[7];
		}

		/// <summary>Gets the number of key/value collection pairs in the <see cref="T:System.Linq.Lookup`2" />.</summary>
		/// <returns>The number of key/value collection pairs in the <see cref="T:System.Linq.Lookup`2" />.</returns>
		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x060007AF RID: 1967 RVA: 0x0001B170 File Offset: 0x00019370
		public int Count
		{
			get
			{
				return this._count;
			}
		}

		/// <summary>Gets the collection of values indexed by the specified key.</summary>
		/// <param name="key">The key of the desired collection of values.</param>
		/// <returns>The collection of values indexed by the specified key.</returns>
		// Token: 0x170000E2 RID: 226
		public IEnumerable<TElement> this[TKey key]
		{
			get
			{
				Grouping<TKey, TElement> grouping = this.GetGrouping(key, false);
				if (grouping != null)
				{
					return grouping;
				}
				return Array.Empty<TElement>();
			}
		}

		/// <summary>Determines whether a specified key is in the <see cref="T:System.Linq.Lookup`2" />.</summary>
		/// <param name="key">The key to find in the <see cref="T:System.Linq.Lookup`2" />.</param>
		/// <returns>
		///     <see langword="true" /> if <paramref name="key" /> is in the <see cref="T:System.Linq.Lookup`2" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x060007B1 RID: 1969 RVA: 0x0001B198 File Offset: 0x00019398
		public bool Contains(TKey key)
		{
			return this.GetGrouping(key, false) != null;
		}

		/// <summary>Returns a generic enumerator that iterates through the <see cref="T:System.Linq.Lookup`2" />.</summary>
		/// <returns>An enumerator for the <see cref="T:System.Linq.Lookup`2" />.</returns>
		// Token: 0x060007B2 RID: 1970 RVA: 0x0001B1A5 File Offset: 0x000193A5
		public IEnumerator<IGrouping<TKey, TElement>> GetEnumerator()
		{
			Grouping<TKey, TElement> g = this._lastGrouping;
			if (g != null)
			{
				do
				{
					g = g._next;
					yield return g;
				}
				while (g != this._lastGrouping);
			}
			yield break;
		}

		// Token: 0x060007B3 RID: 1971 RVA: 0x0001B1B4 File Offset: 0x000193B4
		IGrouping<TKey, TElement>[] IIListProvider<IGrouping<!0, !1>>.ToArray()
		{
			IGrouping<TKey, TElement>[] array = new IGrouping<!0, !1>[this._count];
			int num = 0;
			Grouping<TKey, TElement> grouping = this._lastGrouping;
			if (grouping != null)
			{
				do
				{
					grouping = grouping._next;
					array[num] = grouping;
					num++;
				}
				while (grouping != this._lastGrouping);
			}
			return array;
		}

		// Token: 0x060007B4 RID: 1972 RVA: 0x0001B1F4 File Offset: 0x000193F4
		internal TResult[] ToArray<TResult>(Func<TKey, IEnumerable<TElement>, TResult> resultSelector)
		{
			TResult[] array = new TResult[this._count];
			int num = 0;
			Grouping<TKey, TElement> grouping = this._lastGrouping;
			if (grouping != null)
			{
				do
				{
					grouping = grouping._next;
					grouping.Trim();
					array[num] = resultSelector(grouping._key, grouping._elements);
					num++;
				}
				while (grouping != this._lastGrouping);
			}
			return array;
		}

		// Token: 0x060007B5 RID: 1973 RVA: 0x0001B250 File Offset: 0x00019450
		List<IGrouping<TKey, TElement>> IIListProvider<IGrouping<!0, !1>>.ToList()
		{
			List<IGrouping<TKey, TElement>> list = new List<IGrouping<TKey, TElement>>(this._count);
			Grouping<TKey, TElement> grouping = this._lastGrouping;
			if (grouping != null)
			{
				do
				{
					grouping = grouping._next;
					list.Add(grouping);
				}
				while (grouping != this._lastGrouping);
			}
			return list;
		}

		// Token: 0x060007B6 RID: 1974 RVA: 0x0001B28C File Offset: 0x0001948C
		internal List<TResult> ToList<TResult>(Func<TKey, IEnumerable<TElement>, TResult> resultSelector)
		{
			List<TResult> list = new List<TResult>(this._count);
			Grouping<TKey, TElement> grouping = this._lastGrouping;
			if (grouping != null)
			{
				do
				{
					grouping = grouping._next;
					grouping.Trim();
					list.Add(resultSelector(grouping._key, grouping._elements));
				}
				while (grouping != this._lastGrouping);
			}
			return list;
		}

		// Token: 0x060007B7 RID: 1975 RVA: 0x0001B170 File Offset: 0x00019370
		int IIListProvider<IGrouping<!0, !1>>.GetCount(bool onlyIfCheap)
		{
			return this._count;
		}

		/// <summary>Applies a transform function to each key and its associated values and returns the results.</summary>
		/// <param name="resultSelector">A function to project a result value from each key and its associated values.</param>
		/// <typeparam name="TResult">The type of the result values produced by <paramref name="resultSelector" />.</typeparam>
		/// <returns>A collection that contains one value for each key/value collection pair in the <see cref="T:System.Linq.Lookup`2" />.</returns>
		// Token: 0x060007B8 RID: 1976 RVA: 0x0001B2DE File Offset: 0x000194DE
		public IEnumerable<TResult> ApplyResultSelector<TResult>(Func<TKey, IEnumerable<TElement>, TResult> resultSelector)
		{
			Grouping<TKey, TElement> g = this._lastGrouping;
			if (g != null)
			{
				do
				{
					g = g._next;
					g.Trim();
					yield return resultSelector(g._key, g._elements);
				}
				while (g != this._lastGrouping);
			}
			yield break;
		}

		/// <summary>Returns an enumerator that iterates through the <see cref="T:System.Linq.Lookup`2" />. This class cannot be inherited.</summary>
		/// <returns>An enumerator for the <see cref="T:System.Linq.Lookup`2" />.</returns>
		// Token: 0x060007B9 RID: 1977 RVA: 0x0001B2F5 File Offset: 0x000194F5
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x060007BA RID: 1978 RVA: 0x0001B2FD File Offset: 0x000194FD
		private int InternalGetHashCode(TKey key)
		{
			if (key != null)
			{
				return this._comparer.GetHashCode(key) & int.MaxValue;
			}
			return 0;
		}

		// Token: 0x060007BB RID: 1979 RVA: 0x0001B31C File Offset: 0x0001951C
		internal Grouping<TKey, TElement> GetGrouping(TKey key, bool create)
		{
			int num = this.InternalGetHashCode(key);
			for (Grouping<TKey, TElement> grouping = this._groupings[num % this._groupings.Length]; grouping != null; grouping = grouping._hashNext)
			{
				if (grouping._hashCode == num && this._comparer.Equals(grouping._key, key))
				{
					return grouping;
				}
			}
			if (create)
			{
				if (this._count == this._groupings.Length)
				{
					this.Resize();
				}
				int num2 = num % this._groupings.Length;
				Grouping<TKey, TElement> grouping2 = new Grouping<TKey, TElement>();
				grouping2._key = key;
				grouping2._hashCode = num;
				grouping2._elements = new TElement[1];
				grouping2._hashNext = this._groupings[num2];
				this._groupings[num2] = grouping2;
				if (this._lastGrouping == null)
				{
					grouping2._next = grouping2;
				}
				else
				{
					grouping2._next = this._lastGrouping._next;
					this._lastGrouping._next = grouping2;
				}
				this._lastGrouping = grouping2;
				this._count++;
				return grouping2;
			}
			return null;
		}

		// Token: 0x060007BC RID: 1980 RVA: 0x0001B414 File Offset: 0x00019614
		private void Resize()
		{
			int num = checked(this._count * 2 + 1);
			Grouping<TKey, TElement>[] array = new Grouping<TKey, TElement>[num];
			Grouping<TKey, TElement> grouping = this._lastGrouping;
			do
			{
				grouping = grouping._next;
				int num2 = grouping._hashCode % num;
				grouping._hashNext = array[num2];
				array[num2] = grouping;
			}
			while (grouping != this._lastGrouping);
			this._groupings = array;
		}

		// Token: 0x060007BD RID: 1981 RVA: 0x0000235B File Offset: 0x0000055B
		internal Lookup()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x0400058C RID: 1420
		private readonly IEqualityComparer<TKey> _comparer;

		// Token: 0x0400058D RID: 1421
		private Grouping<TKey, TElement>[] _groupings;

		// Token: 0x0400058E RID: 1422
		private Grouping<TKey, TElement> _lastGrouping;

		// Token: 0x0400058F RID: 1423
		private int _count;

		// Token: 0x020000D9 RID: 217
		[CompilerGenerated]
		private sealed class <GetEnumerator>d__13 : IEnumerator<IGrouping<!0, !1>>, IDisposable, IEnumerator
		{
			// Token: 0x060007BE RID: 1982 RVA: 0x0001B467 File Offset: 0x00019667
			[DebuggerHidden]
			public <GetEnumerator>d__13(int <>1__state)
			{
				this.<>1__state = <>1__state;
			}

			// Token: 0x060007BF RID: 1983 RVA: 0x00003A59 File Offset: 0x00001C59
			[DebuggerHidden]
			void IDisposable.Dispose()
			{
			}

			// Token: 0x060007C0 RID: 1984 RVA: 0x0001B478 File Offset: 0x00019678
			bool IEnumerator.MoveNext()
			{
				int num = this.<>1__state;
				Lookup<TKey, TElement> lookup = this;
				if (num != 0)
				{
					if (num != 1)
					{
						return false;
					}
					this.<>1__state = -1;
					if (g == lookup._lastGrouping)
					{
						return false;
					}
				}
				else
				{
					this.<>1__state = -1;
					g = lookup._lastGrouping;
					if (g == null)
					{
						return false;
					}
				}
				g = g._next;
				this.<>2__current = g;
				this.<>1__state = 1;
				return true;
			}

			// Token: 0x170000E3 RID: 227
			// (get) Token: 0x060007C1 RID: 1985 RVA: 0x0001B4F3 File Offset: 0x000196F3
			IGrouping<TKey, TElement> IEnumerator<IGrouping<!0, !1>>.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x060007C2 RID: 1986 RVA: 0x000080E3 File Offset: 0x000062E3
			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x170000E4 RID: 228
			// (get) Token: 0x060007C3 RID: 1987 RVA: 0x0001B4F3 File Offset: 0x000196F3
			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x04000590 RID: 1424
			private int <>1__state;

			// Token: 0x04000591 RID: 1425
			private IGrouping<TKey, TElement> <>2__current;

			// Token: 0x04000592 RID: 1426
			public Lookup<TKey, TElement> <>4__this;

			// Token: 0x04000593 RID: 1427
			private Grouping<TKey, TElement> <g>5__2;
		}

		// Token: 0x020000DA RID: 218
		[CompilerGenerated]
		private sealed class <ApplyResultSelector>d__19<TResult> : IEnumerable<!2>, IEnumerable, IEnumerator<!2>, IDisposable, IEnumerator
		{
			// Token: 0x060007C4 RID: 1988 RVA: 0x0001B4FB File Offset: 0x000196FB
			[DebuggerHidden]
			public <ApplyResultSelector>d__19(int <>1__state)
			{
				this.<>1__state = <>1__state;
				this.<>l__initialThreadId = Environment.CurrentManagedThreadId;
			}

			// Token: 0x060007C5 RID: 1989 RVA: 0x00003A59 File Offset: 0x00001C59
			[DebuggerHidden]
			void IDisposable.Dispose()
			{
			}

			// Token: 0x060007C6 RID: 1990 RVA: 0x0001B518 File Offset: 0x00019718
			bool IEnumerator.MoveNext()
			{
				int num = this.<>1__state;
				Lookup<TKey, TElement> lookup = this;
				if (num != 0)
				{
					if (num != 1)
					{
						return false;
					}
					this.<>1__state = -1;
					if (g == lookup._lastGrouping)
					{
						return false;
					}
				}
				else
				{
					this.<>1__state = -1;
					g = lookup._lastGrouping;
					if (g == null)
					{
						return false;
					}
				}
				g = g._next;
				g.Trim();
				this.<>2__current = resultSelector(g._key, g._elements);
				this.<>1__state = 1;
				return true;
			}

			// Token: 0x170000E5 RID: 229
			// (get) Token: 0x060007C7 RID: 1991 RVA: 0x0001B5B9 File Offset: 0x000197B9
			TResult IEnumerator<!2>.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x060007C8 RID: 1992 RVA: 0x000080E3 File Offset: 0x000062E3
			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x170000E6 RID: 230
			// (get) Token: 0x060007C9 RID: 1993 RVA: 0x0001B5C1 File Offset: 0x000197C1
			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x060007CA RID: 1994 RVA: 0x0001B5D0 File Offset: 0x000197D0
			[DebuggerHidden]
			IEnumerator<TResult> IEnumerable<!2>.GetEnumerator()
			{
				Lookup<TKey, TElement>.<ApplyResultSelector>d__19<TResult> <ApplyResultSelector>d__;
				if (this.<>1__state == -2 && this.<>l__initialThreadId == Environment.CurrentManagedThreadId)
				{
					this.<>1__state = 0;
					<ApplyResultSelector>d__ = this;
				}
				else
				{
					<ApplyResultSelector>d__ = new Lookup<TKey, TElement>.<ApplyResultSelector>d__19<TResult>(0);
					<ApplyResultSelector>d__.<>4__this = this;
				}
				<ApplyResultSelector>d__.resultSelector = resultSelector;
				return <ApplyResultSelector>d__;
			}

			// Token: 0x060007CB RID: 1995 RVA: 0x0001B61F File Offset: 0x0001981F
			[DebuggerHidden]
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.System.Collections.Generic.IEnumerable<TResult>.GetEnumerator();
			}

			// Token: 0x04000594 RID: 1428
			private int <>1__state;

			// Token: 0x04000595 RID: 1429
			private TResult <>2__current;

			// Token: 0x04000596 RID: 1430
			private int <>l__initialThreadId;

			// Token: 0x04000597 RID: 1431
			public Lookup<TKey, TElement> <>4__this;

			// Token: 0x04000598 RID: 1432
			private Func<TKey, IEnumerable<TElement>, TResult> resultSelector;

			// Token: 0x04000599 RID: 1433
			public Func<TKey, IEnumerable<TElement>, TResult> <>3__resultSelector;

			// Token: 0x0400059A RID: 1434
			private Grouping<TKey, TElement> <g>5__2;
		}
	}
}

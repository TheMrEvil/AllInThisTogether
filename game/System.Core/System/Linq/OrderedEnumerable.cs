using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace System.Linq
{
	// Token: 0x020000DC RID: 220
	internal abstract class OrderedEnumerable<TElement> : IOrderedEnumerable<!0>, IEnumerable<!0>, IEnumerable, IPartition<TElement>, IIListProvider<TElement>
	{
		// Token: 0x060007CD RID: 1997 RVA: 0x0001B627 File Offset: 0x00019827
		private int[] SortedMap(Buffer<TElement> buffer)
		{
			return this.GetEnumerableSorter().Sort(buffer._items, buffer._count);
		}

		// Token: 0x060007CE RID: 1998 RVA: 0x0001B640 File Offset: 0x00019840
		private int[] SortedMap(Buffer<TElement> buffer, int minIdx, int maxIdx)
		{
			return this.GetEnumerableSorter().Sort(buffer._items, buffer._count, minIdx, maxIdx);
		}

		// Token: 0x060007CF RID: 1999 RVA: 0x0001B65B File Offset: 0x0001985B
		public IEnumerator<TElement> GetEnumerator()
		{
			Buffer<TElement> buffer = new Buffer<TElement>(this._source);
			if (buffer._count > 0)
			{
				int[] map = this.SortedMap(buffer);
				int num;
				for (int i = 0; i < buffer._count; i = num + 1)
				{
					yield return buffer._items[map[i]];
					num = i;
				}
				map = null;
			}
			yield break;
		}

		// Token: 0x060007D0 RID: 2000 RVA: 0x0001B66C File Offset: 0x0001986C
		public TElement[] ToArray()
		{
			Buffer<TElement> buffer = new Buffer<TElement>(this._source);
			int count = buffer._count;
			if (count == 0)
			{
				return buffer._items;
			}
			TElement[] array = new TElement[count];
			int[] array2 = this.SortedMap(buffer);
			for (int num = 0; num != array.Length; num++)
			{
				array[num] = buffer._items[array2[num]];
			}
			return array;
		}

		// Token: 0x060007D1 RID: 2001 RVA: 0x0001B6D0 File Offset: 0x000198D0
		public List<TElement> ToList()
		{
			Buffer<TElement> buffer = new Buffer<TElement>(this._source);
			int count = buffer._count;
			List<TElement> list = new List<TElement>(count);
			if (count > 0)
			{
				int[] array = this.SortedMap(buffer);
				for (int num = 0; num != count; num++)
				{
					list.Add(buffer._items[array[num]]);
				}
			}
			return list;
		}

		// Token: 0x060007D2 RID: 2002 RVA: 0x0001B72C File Offset: 0x0001992C
		public int GetCount(bool onlyIfCheap)
		{
			IIListProvider<TElement> iilistProvider = this._source as IIListProvider<TElement>;
			if (iilistProvider != null)
			{
				return iilistProvider.GetCount(onlyIfCheap);
			}
			if (onlyIfCheap && !(this._source is ICollection<!0>) && !(this._source is ICollection))
			{
				return -1;
			}
			return this._source.Count<TElement>();
		}

		// Token: 0x060007D3 RID: 2003 RVA: 0x0001B77A File Offset: 0x0001997A
		internal IEnumerator<TElement> GetEnumerator(int minIdx, int maxIdx)
		{
			Buffer<TElement> buffer = new Buffer<TElement>(this._source);
			int count = buffer._count;
			if (count > minIdx)
			{
				if (count <= maxIdx)
				{
					maxIdx = count - 1;
				}
				if (minIdx == maxIdx)
				{
					yield return this.GetEnumerableSorter().ElementAt(buffer._items, count, minIdx);
				}
				else
				{
					int[] map = this.SortedMap(buffer, minIdx, maxIdx);
					while (minIdx <= maxIdx)
					{
						yield return buffer._items[map[minIdx]];
						int num = minIdx + 1;
						minIdx = num;
					}
					map = null;
				}
			}
			yield break;
		}

		// Token: 0x060007D4 RID: 2004 RVA: 0x0001B798 File Offset: 0x00019998
		internal TElement[] ToArray(int minIdx, int maxIdx)
		{
			Buffer<TElement> buffer = new Buffer<TElement>(this._source);
			int count = buffer._count;
			if (count <= minIdx)
			{
				return Array.Empty<TElement>();
			}
			if (count <= maxIdx)
			{
				maxIdx = count - 1;
			}
			if (minIdx == maxIdx)
			{
				return new TElement[]
				{
					this.GetEnumerableSorter().ElementAt(buffer._items, count, minIdx)
				};
			}
			int[] array = this.SortedMap(buffer, minIdx, maxIdx);
			TElement[] array2 = new TElement[maxIdx - minIdx + 1];
			int num = 0;
			while (minIdx <= maxIdx)
			{
				array2[num] = buffer._items[array[minIdx]];
				num++;
				minIdx++;
			}
			return array2;
		}

		// Token: 0x060007D5 RID: 2005 RVA: 0x0001B834 File Offset: 0x00019A34
		internal List<TElement> ToList(int minIdx, int maxIdx)
		{
			Buffer<TElement> buffer = new Buffer<TElement>(this._source);
			int count = buffer._count;
			if (count <= minIdx)
			{
				return new List<TElement>();
			}
			if (count <= maxIdx)
			{
				maxIdx = count - 1;
			}
			if (minIdx == maxIdx)
			{
				return new List<TElement>(1)
				{
					this.GetEnumerableSorter().ElementAt(buffer._items, count, minIdx)
				};
			}
			int[] array = this.SortedMap(buffer, minIdx, maxIdx);
			List<TElement> list = new List<TElement>(maxIdx - minIdx + 1);
			while (minIdx <= maxIdx)
			{
				list.Add(buffer._items[array[minIdx]]);
				minIdx++;
			}
			return list;
		}

		// Token: 0x060007D6 RID: 2006 RVA: 0x0001B8C4 File Offset: 0x00019AC4
		internal int GetCount(int minIdx, int maxIdx, bool onlyIfCheap)
		{
			int count = this.GetCount(onlyIfCheap);
			if (count <= 0)
			{
				return count;
			}
			if (count <= minIdx)
			{
				return 0;
			}
			return ((count <= maxIdx) ? count : (maxIdx + 1)) - minIdx;
		}

		// Token: 0x060007D7 RID: 2007 RVA: 0x0001B8F1 File Offset: 0x00019AF1
		private EnumerableSorter<TElement> GetEnumerableSorter()
		{
			return this.GetEnumerableSorter(null);
		}

		// Token: 0x060007D8 RID: 2008
		internal abstract EnumerableSorter<TElement> GetEnumerableSorter(EnumerableSorter<TElement> next);

		// Token: 0x060007D9 RID: 2009 RVA: 0x0001B8FA File Offset: 0x00019AFA
		private CachingComparer<TElement> GetComparer()
		{
			return this.GetComparer(null);
		}

		// Token: 0x060007DA RID: 2010
		internal abstract CachingComparer<TElement> GetComparer(CachingComparer<TElement> childComparer);

		// Token: 0x060007DB RID: 2011 RVA: 0x0001B903 File Offset: 0x00019B03
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x060007DC RID: 2012 RVA: 0x0001B90B File Offset: 0x00019B0B
		IOrderedEnumerable<TElement> IOrderedEnumerable<!0>.CreateOrderedEnumerable<TKey>(Func<TElement, TKey> keySelector, IComparer<TKey> comparer, bool descending)
		{
			return new OrderedEnumerable<TElement, TKey>(this._source, keySelector, comparer, descending, this);
		}

		// Token: 0x060007DD RID: 2013 RVA: 0x0001B91C File Offset: 0x00019B1C
		public IPartition<TElement> Skip(int count)
		{
			return new OrderedPartition<TElement>(this, count, int.MaxValue);
		}

		// Token: 0x060007DE RID: 2014 RVA: 0x0001B92A File Offset: 0x00019B2A
		public IPartition<TElement> Take(int count)
		{
			return new OrderedPartition<TElement>(this, 0, count - 1);
		}

		// Token: 0x060007DF RID: 2015 RVA: 0x0001B938 File Offset: 0x00019B38
		public TElement TryGetElementAt(int index, out bool found)
		{
			if (index == 0)
			{
				return this.TryGetFirst(out found);
			}
			if (index > 0)
			{
				Buffer<TElement> buffer = new Buffer<TElement>(this._source);
				int count = buffer._count;
				if (index < count)
				{
					found = true;
					return this.GetEnumerableSorter().ElementAt(buffer._items, count, index);
				}
			}
			found = false;
			return default(TElement);
		}

		// Token: 0x060007E0 RID: 2016 RVA: 0x0001B990 File Offset: 0x00019B90
		public TElement TryGetFirst(out bool found)
		{
			CachingComparer<TElement> comparer = this.GetComparer();
			TElement telement;
			using (IEnumerator<TElement> enumerator = this._source.GetEnumerator())
			{
				if (!enumerator.MoveNext())
				{
					found = false;
					telement = default(TElement);
					telement = telement;
				}
				else
				{
					TElement telement2 = enumerator.Current;
					comparer.SetElement(telement2);
					while (enumerator.MoveNext())
					{
						TElement telement3 = enumerator.Current;
						if (comparer.Compare(telement3, true) < 0)
						{
							telement2 = telement3;
						}
					}
					found = true;
					telement = telement2;
				}
			}
			return telement;
		}

		// Token: 0x060007E1 RID: 2017 RVA: 0x0001BA18 File Offset: 0x00019C18
		public TElement TryGetFirst(Func<TElement, bool> predicate, out bool found)
		{
			CachingComparer<TElement> comparer = this.GetComparer();
			TElement telement3;
			using (IEnumerator<TElement> enumerator = this._source.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					TElement telement = enumerator.Current;
					if (predicate(telement))
					{
						comparer.SetElement(telement);
						while (enumerator.MoveNext())
						{
							TElement telement2 = enumerator.Current;
							if (predicate(telement2) && comparer.Compare(telement2, true) < 0)
							{
								telement = telement2;
							}
						}
						found = true;
						return telement;
					}
				}
				found = false;
				telement3 = default(TElement);
				telement3 = telement3;
			}
			return telement3;
		}

		// Token: 0x060007E2 RID: 2018 RVA: 0x0001BAB4 File Offset: 0x00019CB4
		public TElement TryGetLast(out bool found)
		{
			TElement telement;
			using (IEnumerator<TElement> enumerator = this._source.GetEnumerator())
			{
				if (!enumerator.MoveNext())
				{
					found = false;
					telement = default(TElement);
					telement = telement;
				}
				else
				{
					CachingComparer<TElement> comparer = this.GetComparer();
					TElement telement2 = enumerator.Current;
					comparer.SetElement(telement2);
					while (enumerator.MoveNext())
					{
						TElement telement3 = enumerator.Current;
						if (comparer.Compare(telement3, false) >= 0)
						{
							telement2 = telement3;
						}
					}
					found = true;
					telement = telement2;
				}
			}
			return telement;
		}

		// Token: 0x060007E3 RID: 2019 RVA: 0x0001BB3C File Offset: 0x00019D3C
		public TElement TryGetLast(int minIdx, int maxIdx, out bool found)
		{
			Buffer<TElement> buffer = new Buffer<TElement>(this._source);
			int count = buffer._count;
			if (minIdx >= count)
			{
				found = false;
				return default(TElement);
			}
			found = true;
			if (maxIdx >= count - 1)
			{
				return this.Last(buffer);
			}
			return this.GetEnumerableSorter().ElementAt(buffer._items, count, maxIdx);
		}

		// Token: 0x060007E4 RID: 2020 RVA: 0x0001BB94 File Offset: 0x00019D94
		private TElement Last(Buffer<TElement> buffer)
		{
			CachingComparer<TElement> comparer = this.GetComparer();
			TElement[] items = buffer._items;
			int count = buffer._count;
			TElement telement = items[0];
			comparer.SetElement(telement);
			for (int num = 1; num != count; num++)
			{
				TElement telement2 = items[num];
				if (comparer.Compare(telement2, false) >= 0)
				{
					telement = telement2;
				}
			}
			return telement;
		}

		// Token: 0x060007E5 RID: 2021 RVA: 0x0001BBF0 File Offset: 0x00019DF0
		public TElement TryGetLast(Func<TElement, bool> predicate, out bool found)
		{
			CachingComparer<TElement> comparer = this.GetComparer();
			TElement telement3;
			using (IEnumerator<TElement> enumerator = this._source.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					TElement telement = enumerator.Current;
					if (predicate(telement))
					{
						comparer.SetElement(telement);
						while (enumerator.MoveNext())
						{
							TElement telement2 = enumerator.Current;
							if (predicate(telement2) && comparer.Compare(telement2, false) >= 0)
							{
								telement = telement2;
							}
						}
						found = true;
						return telement;
					}
				}
				found = false;
				telement3 = default(TElement);
				telement3 = telement3;
			}
			return telement3;
		}

		// Token: 0x060007E6 RID: 2022 RVA: 0x00002162 File Offset: 0x00000362
		protected OrderedEnumerable()
		{
		}

		// Token: 0x0400059B RID: 1435
		internal IEnumerable<TElement> _source;

		// Token: 0x020000DD RID: 221
		[CompilerGenerated]
		private sealed class <GetEnumerator>d__3 : IEnumerator<!0>, IDisposable, IEnumerator
		{
			// Token: 0x060007E7 RID: 2023 RVA: 0x0001BC8C File Offset: 0x00019E8C
			[DebuggerHidden]
			public <GetEnumerator>d__3(int <>1__state)
			{
				this.<>1__state = <>1__state;
			}

			// Token: 0x060007E8 RID: 2024 RVA: 0x00003A59 File Offset: 0x00001C59
			[DebuggerHidden]
			void IDisposable.Dispose()
			{
			}

			// Token: 0x060007E9 RID: 2025 RVA: 0x0001BC9C File Offset: 0x00019E9C
			bool IEnumerator.MoveNext()
			{
				int num = this.<>1__state;
				OrderedEnumerable<TElement> orderedEnumerable = this;
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
					buffer = new Buffer<TElement>(orderedEnumerable._source);
					if (buffer._count <= 0)
					{
						return false;
					}
					map = orderedEnumerable.SortedMap(buffer);
					i = 0;
				}
				if (i < buffer._count)
				{
					this.<>2__current = buffer._items[map[i]];
					this.<>1__state = 1;
					return true;
				}
				map = null;
				return false;
			}

			// Token: 0x170000E7 RID: 231
			// (get) Token: 0x060007EA RID: 2026 RVA: 0x0001BD5F File Offset: 0x00019F5F
			TElement IEnumerator<!0>.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x060007EB RID: 2027 RVA: 0x000080E3 File Offset: 0x000062E3
			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x170000E8 RID: 232
			// (get) Token: 0x060007EC RID: 2028 RVA: 0x0001BD67 File Offset: 0x00019F67
			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x0400059C RID: 1436
			private int <>1__state;

			// Token: 0x0400059D RID: 1437
			private TElement <>2__current;

			// Token: 0x0400059E RID: 1438
			public OrderedEnumerable<TElement> <>4__this;

			// Token: 0x0400059F RID: 1439
			private Buffer<TElement> <buffer>5__2;

			// Token: 0x040005A0 RID: 1440
			private int[] <map>5__3;

			// Token: 0x040005A1 RID: 1441
			private int <i>5__4;
		}

		// Token: 0x020000DE RID: 222
		[CompilerGenerated]
		private sealed class <GetEnumerator>d__7 : IEnumerator<!0>, IDisposable, IEnumerator
		{
			// Token: 0x060007ED RID: 2029 RVA: 0x0001BD74 File Offset: 0x00019F74
			[DebuggerHidden]
			public <GetEnumerator>d__7(int <>1__state)
			{
				this.<>1__state = <>1__state;
			}

			// Token: 0x060007EE RID: 2030 RVA: 0x00003A59 File Offset: 0x00001C59
			[DebuggerHidden]
			void IDisposable.Dispose()
			{
			}

			// Token: 0x060007EF RID: 2031 RVA: 0x0001BD84 File Offset: 0x00019F84
			bool IEnumerator.MoveNext()
			{
				int num = this.<>1__state;
				OrderedEnumerable<TElement> orderedEnumerable = this;
				switch (num)
				{
				case 0:
				{
					this.<>1__state = -1;
					buffer = new Buffer<TElement>(orderedEnumerable._source);
					int count = buffer._count;
					if (count <= minIdx)
					{
						return false;
					}
					if (count <= maxIdx)
					{
						maxIdx = count - 1;
					}
					if (minIdx == maxIdx)
					{
						this.<>2__current = orderedEnumerable.GetEnumerableSorter().ElementAt(buffer._items, count, minIdx);
						this.<>1__state = 1;
						return true;
					}
					map = orderedEnumerable.SortedMap(buffer, minIdx, maxIdx);
					break;
				}
				case 1:
					this.<>1__state = -1;
					return false;
				case 2:
				{
					this.<>1__state = -1;
					int num2 = minIdx + 1;
					minIdx = num2;
					break;
				}
				default:
					return false;
				}
				if (minIdx <= maxIdx)
				{
					this.<>2__current = buffer._items[map[minIdx]];
					this.<>1__state = 2;
					return true;
				}
				map = null;
				return false;
			}

			// Token: 0x170000E9 RID: 233
			// (get) Token: 0x060007F0 RID: 2032 RVA: 0x0001BEB1 File Offset: 0x0001A0B1
			TElement IEnumerator<!0>.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x060007F1 RID: 2033 RVA: 0x000080E3 File Offset: 0x000062E3
			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x170000EA RID: 234
			// (get) Token: 0x060007F2 RID: 2034 RVA: 0x0001BEB9 File Offset: 0x0001A0B9
			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x040005A2 RID: 1442
			private int <>1__state;

			// Token: 0x040005A3 RID: 1443
			private TElement <>2__current;

			// Token: 0x040005A4 RID: 1444
			public OrderedEnumerable<TElement> <>4__this;

			// Token: 0x040005A5 RID: 1445
			public int minIdx;

			// Token: 0x040005A6 RID: 1446
			public int maxIdx;

			// Token: 0x040005A7 RID: 1447
			private Buffer<TElement> <buffer>5__2;

			// Token: 0x040005A8 RID: 1448
			private int[] <map>5__3;
		}
	}
}

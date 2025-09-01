using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace System.Linq.Parallel
{
	// Token: 0x0200018B RID: 395
	internal abstract class QueryResults<T> : IList<T>, ICollection<T>, IEnumerable<!0>, IEnumerable
	{
		// Token: 0x06000A82 RID: 2690
		internal abstract void GivePartitionedStream(IPartitionedStreamRecipient<T> recipient);

		// Token: 0x17000129 RID: 297
		// (get) Token: 0x06000A83 RID: 2691 RVA: 0x000023D1 File Offset: 0x000005D1
		internal virtual bool IsIndexible
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000A84 RID: 2692 RVA: 0x000080E3 File Offset: 0x000062E3
		internal virtual T GetElement(int index)
		{
			throw new NotSupportedException();
		}

		// Token: 0x1700012A RID: 298
		// (get) Token: 0x06000A85 RID: 2693 RVA: 0x000080E3 File Offset: 0x000062E3
		internal virtual int ElementsCount
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x06000A86 RID: 2694 RVA: 0x000080E3 File Offset: 0x000062E3
		int IList<!0>.IndexOf(T item)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000A87 RID: 2695 RVA: 0x000080E3 File Offset: 0x000062E3
		void IList<!0>.Insert(int index, T item)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000A88 RID: 2696 RVA: 0x000080E3 File Offset: 0x000062E3
		void IList<!0>.RemoveAt(int index)
		{
			throw new NotSupportedException();
		}

		// Token: 0x1700012B RID: 299
		public T this[int index]
		{
			get
			{
				return this.GetElement(index);
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x06000A8B RID: 2699 RVA: 0x000080E3 File Offset: 0x000062E3
		void ICollection<!0>.Add(T item)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000A8C RID: 2700 RVA: 0x000080E3 File Offset: 0x000062E3
		void ICollection<!0>.Clear()
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000A8D RID: 2701 RVA: 0x000080E3 File Offset: 0x000062E3
		bool ICollection<!0>.Contains(T item)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000A8E RID: 2702 RVA: 0x000080E3 File Offset: 0x000062E3
		void ICollection<!0>.CopyTo(T[] array, int arrayIndex)
		{
			throw new NotSupportedException();
		}

		// Token: 0x1700012C RID: 300
		// (get) Token: 0x06000A8F RID: 2703 RVA: 0x00025327 File Offset: 0x00023527
		public int Count
		{
			get
			{
				return this.ElementsCount;
			}
		}

		// Token: 0x1700012D RID: 301
		// (get) Token: 0x06000A90 RID: 2704 RVA: 0x00007E1D File Offset: 0x0000601D
		bool ICollection<!0>.IsReadOnly
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000A91 RID: 2705 RVA: 0x000080E3 File Offset: 0x000062E3
		bool ICollection<!0>.Remove(T item)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000A92 RID: 2706 RVA: 0x0002532F File Offset: 0x0002352F
		IEnumerator<T> IEnumerable<!0>.GetEnumerator()
		{
			int num;
			for (int index = 0; index < this.Count; index = num + 1)
			{
				yield return this[index];
				num = index;
			}
			yield break;
		}

		// Token: 0x06000A93 RID: 2707 RVA: 0x0000817A File Offset: 0x0000637A
		IEnumerator IEnumerable.GetEnumerator()
		{
			return ((IEnumerable<!0>)this).GetEnumerator();
		}

		// Token: 0x06000A94 RID: 2708 RVA: 0x00002162 File Offset: 0x00000362
		protected QueryResults()
		{
		}

		// Token: 0x0200018C RID: 396
		[CompilerGenerated]
		private sealed class <System-Collections-Generic-IEnumerable<T>-GetEnumerator>d__21 : IEnumerator<!0>, IDisposable, IEnumerator
		{
			// Token: 0x06000A95 RID: 2709 RVA: 0x0002533E File Offset: 0x0002353E
			[DebuggerHidden]
			public <System-Collections-Generic-IEnumerable<T>-GetEnumerator>d__21(int <>1__state)
			{
				this.<>1__state = <>1__state;
			}

			// Token: 0x06000A96 RID: 2710 RVA: 0x00003A59 File Offset: 0x00001C59
			[DebuggerHidden]
			void IDisposable.Dispose()
			{
			}

			// Token: 0x06000A97 RID: 2711 RVA: 0x00025350 File Offset: 0x00023550
			bool IEnumerator.MoveNext()
			{
				int num = this.<>1__state;
				QueryResults<T> queryResults = this;
				if (num != 0)
				{
					if (num != 1)
					{
						return false;
					}
					this.<>1__state = -1;
					int num2 = index;
					index = num2 + 1;
				}
				else
				{
					this.<>1__state = -1;
					index = 0;
				}
				if (index >= queryResults.Count)
				{
					return false;
				}
				this.<>2__current = queryResults[index];
				this.<>1__state = 1;
				return true;
			}

			// Token: 0x1700012E RID: 302
			// (get) Token: 0x06000A98 RID: 2712 RVA: 0x000253C5 File Offset: 0x000235C5
			T IEnumerator<!0>.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x06000A99 RID: 2713 RVA: 0x000080E3 File Offset: 0x000062E3
			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x1700012F RID: 303
			// (get) Token: 0x06000A9A RID: 2714 RVA: 0x000253CD File Offset: 0x000235CD
			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x0400074F RID: 1871
			private int <>1__state;

			// Token: 0x04000750 RID: 1872
			private T <>2__current;

			// Token: 0x04000751 RID: 1873
			public QueryResults<T> <>4__this;

			// Token: 0x04000752 RID: 1874
			private int <index>5__2;
		}
	}
}

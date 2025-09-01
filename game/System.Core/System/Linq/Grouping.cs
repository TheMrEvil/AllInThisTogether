using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace System.Linq
{
	// Token: 0x020000D1 RID: 209
	[DebuggerDisplay("Key = {Key}")]
	[DebuggerTypeProxy(typeof(SystemLinq_GroupingDebugView<, >))]
	internal class Grouping<TKey, TElement> : IGrouping<!0, !1>, IEnumerable<!1>, IEnumerable, IList<TElement>, ICollection<TElement>
	{
		// Token: 0x06000778 RID: 1912 RVA: 0x00002162 File Offset: 0x00000362
		internal Grouping()
		{
		}

		// Token: 0x06000779 RID: 1913 RVA: 0x0001AA98 File Offset: 0x00018C98
		internal void Add(TElement element)
		{
			if (this._elements.Length == this._count)
			{
				Array.Resize<TElement>(ref this._elements, checked(this._count * 2));
			}
			this._elements[this._count] = element;
			this._count++;
		}

		// Token: 0x0600077A RID: 1914 RVA: 0x0001AAE8 File Offset: 0x00018CE8
		internal void Trim()
		{
			if (this._elements.Length != this._count)
			{
				Array.Resize<TElement>(ref this._elements, this._count);
			}
		}

		// Token: 0x0600077B RID: 1915 RVA: 0x0001AB0B File Offset: 0x00018D0B
		public IEnumerator<TElement> GetEnumerator()
		{
			int num;
			for (int i = 0; i < this._count; i = num + 1)
			{
				yield return this._elements[i];
				num = i;
			}
			yield break;
		}

		// Token: 0x0600077C RID: 1916 RVA: 0x0001AB1A File Offset: 0x00018D1A
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x0600077D RID: 1917 RVA: 0x0001AB22 File Offset: 0x00018D22
		public TKey Key
		{
			get
			{
				return this._key;
			}
		}

		// Token: 0x170000DA RID: 218
		// (get) Token: 0x0600077E RID: 1918 RVA: 0x0001AB2A File Offset: 0x00018D2A
		int ICollection<!1>.Count
		{
			get
			{
				return this._count;
			}
		}

		// Token: 0x170000DB RID: 219
		// (get) Token: 0x0600077F RID: 1919 RVA: 0x00007E1D File Offset: 0x0000601D
		bool ICollection<!1>.IsReadOnly
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000780 RID: 1920 RVA: 0x0001585F File Offset: 0x00013A5F
		void ICollection<!1>.Add(TElement item)
		{
			throw Error.NotSupported();
		}

		// Token: 0x06000781 RID: 1921 RVA: 0x0001585F File Offset: 0x00013A5F
		void ICollection<!1>.Clear()
		{
			throw Error.NotSupported();
		}

		// Token: 0x06000782 RID: 1922 RVA: 0x0001AB32 File Offset: 0x00018D32
		bool ICollection<!1>.Contains(TElement item)
		{
			return Array.IndexOf<TElement>(this._elements, item, 0, this._count) >= 0;
		}

		// Token: 0x06000783 RID: 1923 RVA: 0x0001AB4D File Offset: 0x00018D4D
		void ICollection<!1>.CopyTo(TElement[] array, int arrayIndex)
		{
			Array.Copy(this._elements, 0, array, arrayIndex, this._count);
		}

		// Token: 0x06000784 RID: 1924 RVA: 0x0001585F File Offset: 0x00013A5F
		bool ICollection<!1>.Remove(TElement item)
		{
			throw Error.NotSupported();
		}

		// Token: 0x06000785 RID: 1925 RVA: 0x0001AB63 File Offset: 0x00018D63
		int IList<!1>.IndexOf(TElement item)
		{
			return Array.IndexOf<TElement>(this._elements, item, 0, this._count);
		}

		// Token: 0x06000786 RID: 1926 RVA: 0x0001585F File Offset: 0x00013A5F
		void IList<!1>.Insert(int index, TElement item)
		{
			throw Error.NotSupported();
		}

		// Token: 0x06000787 RID: 1927 RVA: 0x0001585F File Offset: 0x00013A5F
		void IList<!1>.RemoveAt(int index)
		{
			throw Error.NotSupported();
		}

		// Token: 0x170000DC RID: 220
		TElement IList<!1>.this[int index]
		{
			get
			{
				if (index < 0 || index >= this._count)
				{
					throw Error.ArgumentOutOfRange("index");
				}
				return this._elements[index];
			}
			set
			{
				throw Error.NotSupported();
			}
		}

		// Token: 0x04000572 RID: 1394
		internal TKey _key;

		// Token: 0x04000573 RID: 1395
		internal int _hashCode;

		// Token: 0x04000574 RID: 1396
		internal TElement[] _elements;

		// Token: 0x04000575 RID: 1397
		internal int _count;

		// Token: 0x04000576 RID: 1398
		internal Grouping<TKey, TElement> _hashNext;

		// Token: 0x04000577 RID: 1399
		internal Grouping<TKey, TElement> _next;

		// Token: 0x020000D2 RID: 210
		[CompilerGenerated]
		private sealed class <GetEnumerator>d__9 : IEnumerator<!1>, IDisposable, IEnumerator
		{
			// Token: 0x0600078A RID: 1930 RVA: 0x0001AB9E File Offset: 0x00018D9E
			[DebuggerHidden]
			public <GetEnumerator>d__9(int <>1__state)
			{
				this.<>1__state = <>1__state;
			}

			// Token: 0x0600078B RID: 1931 RVA: 0x00003A59 File Offset: 0x00001C59
			[DebuggerHidden]
			void IDisposable.Dispose()
			{
			}

			// Token: 0x0600078C RID: 1932 RVA: 0x0001ABB0 File Offset: 0x00018DB0
			bool IEnumerator.MoveNext()
			{
				int num = this.<>1__state;
				Grouping<TKey, TElement> grouping = this;
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
					i = 0;
				}
				if (i >= grouping._count)
				{
					return false;
				}
				this.<>2__current = grouping._elements[i];
				this.<>1__state = 1;
				return true;
			}

			// Token: 0x170000DD RID: 221
			// (get) Token: 0x0600078D RID: 1933 RVA: 0x0001AC2A File Offset: 0x00018E2A
			TElement IEnumerator<!1>.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x0600078E RID: 1934 RVA: 0x000080E3 File Offset: 0x000062E3
			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x170000DE RID: 222
			// (get) Token: 0x0600078F RID: 1935 RVA: 0x0001AC32 File Offset: 0x00018E32
			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x04000578 RID: 1400
			private int <>1__state;

			// Token: 0x04000579 RID: 1401
			private TElement <>2__current;

			// Token: 0x0400057A RID: 1402
			public Grouping<TKey, TElement> <>4__this;

			// Token: 0x0400057B RID: 1403
			private int <i>5__2;
		}
	}
}

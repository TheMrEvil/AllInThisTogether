using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace System.Linq.Parallel
{
	// Token: 0x020001F4 RID: 500
	internal class Lookup<TKey, TElement> : ILookup<TKey, TElement>, IEnumerable<IGrouping<TKey, TElement>>, IEnumerable
	{
		// Token: 0x06000C43 RID: 3139 RVA: 0x0002AF53 File Offset: 0x00029153
		internal Lookup(IEqualityComparer<TKey> comparer)
		{
			this._comparer = comparer;
			this._dict = new Dictionary<TKey, IGrouping<TKey, TElement>>(this._comparer);
		}

		// Token: 0x17000172 RID: 370
		// (get) Token: 0x06000C44 RID: 3140 RVA: 0x0002AF74 File Offset: 0x00029174
		public int Count
		{
			get
			{
				int num = this._dict.Count;
				if (this._defaultKeyGrouping != null)
				{
					num++;
				}
				return num;
			}
		}

		// Token: 0x17000173 RID: 371
		public IEnumerable<TElement> this[TKey key]
		{
			get
			{
				if (this._comparer.Equals(key, default(TKey)))
				{
					if (this._defaultKeyGrouping != null)
					{
						return this._defaultKeyGrouping;
					}
					return Enumerable.Empty<TElement>();
				}
				else
				{
					IGrouping<TKey, TElement> result;
					if (this._dict.TryGetValue(key, out result))
					{
						return result;
					}
					return Enumerable.Empty<TElement>();
				}
			}
		}

		// Token: 0x06000C46 RID: 3142 RVA: 0x0002AFEC File Offset: 0x000291EC
		public bool Contains(TKey key)
		{
			if (this._comparer.Equals(key, default(TKey)))
			{
				return this._defaultKeyGrouping != null;
			}
			return this._dict.ContainsKey(key);
		}

		// Token: 0x06000C47 RID: 3143 RVA: 0x0002B028 File Offset: 0x00029228
		internal void Add(IGrouping<TKey, TElement> grouping)
		{
			if (this._comparer.Equals(grouping.Key, default(TKey)))
			{
				this._defaultKeyGrouping = grouping;
				return;
			}
			this._dict.Add(grouping.Key, grouping);
		}

		// Token: 0x06000C48 RID: 3144 RVA: 0x0002B06B File Offset: 0x0002926B
		public IEnumerator<IGrouping<TKey, TElement>> GetEnumerator()
		{
			foreach (IGrouping<TKey, TElement> grouping in this._dict.Values)
			{
				yield return grouping;
			}
			IEnumerator<IGrouping<TKey, TElement>> enumerator = null;
			if (this._defaultKeyGrouping != null)
			{
				yield return this._defaultKeyGrouping;
			}
			yield break;
			yield break;
		}

		// Token: 0x06000C49 RID: 3145 RVA: 0x0002B07A File Offset: 0x0002927A
		IEnumerator IEnumerable.GetEnumerator()
		{
			return ((IEnumerable<IGrouping<TKey, TElement>>)this).GetEnumerator();
		}

		// Token: 0x040008AC RID: 2220
		private IDictionary<TKey, IGrouping<TKey, TElement>> _dict;

		// Token: 0x040008AD RID: 2221
		private IEqualityComparer<TKey> _comparer;

		// Token: 0x040008AE RID: 2222
		private IGrouping<TKey, TElement> _defaultKeyGrouping;

		// Token: 0x020001F5 RID: 501
		[CompilerGenerated]
		private sealed class <GetEnumerator>d__10 : IEnumerator<IGrouping<TKey, TElement>>, IDisposable, IEnumerator
		{
			// Token: 0x06000C4A RID: 3146 RVA: 0x0002B082 File Offset: 0x00029282
			[DebuggerHidden]
			public <GetEnumerator>d__10(int <>1__state)
			{
				this.<>1__state = <>1__state;
			}

			// Token: 0x06000C4B RID: 3147 RVA: 0x0002B094 File Offset: 0x00029294
			[DebuggerHidden]
			void IDisposable.Dispose()
			{
				int num = this.<>1__state;
				if (num == -3 || num == 1)
				{
					try
					{
					}
					finally
					{
						this.<>m__Finally1();
					}
				}
			}

			// Token: 0x06000C4C RID: 3148 RVA: 0x0002B0CC File Offset: 0x000292CC
			bool IEnumerator.MoveNext()
			{
				bool result;
				try
				{
					int num = this.<>1__state;
					Lookup<TKey, TElement> lookup = this;
					switch (num)
					{
					case 0:
						this.<>1__state = -1;
						enumerator = lookup._dict.Values.GetEnumerator();
						this.<>1__state = -3;
						break;
					case 1:
						this.<>1__state = -3;
						break;
					case 2:
						this.<>1__state = -1;
						goto IL_B4;
					default:
						return false;
					}
					if (enumerator.MoveNext())
					{
						IGrouping<TKey, TElement> grouping = enumerator.Current;
						this.<>2__current = grouping;
						this.<>1__state = 1;
						return true;
					}
					this.<>m__Finally1();
					enumerator = null;
					if (lookup._defaultKeyGrouping != null)
					{
						this.<>2__current = lookup._defaultKeyGrouping;
						this.<>1__state = 2;
						return true;
					}
					IL_B4:
					result = false;
				}
				catch
				{
					this.System.IDisposable.Dispose();
					throw;
				}
				return result;
			}

			// Token: 0x06000C4D RID: 3149 RVA: 0x0002B1AC File Offset: 0x000293AC
			private void <>m__Finally1()
			{
				this.<>1__state = -1;
				if (enumerator != null)
				{
					enumerator.Dispose();
				}
			}

			// Token: 0x17000174 RID: 372
			// (get) Token: 0x06000C4E RID: 3150 RVA: 0x0002B1C8 File Offset: 0x000293C8
			IGrouping<TKey, TElement> IEnumerator<IGrouping<!0, !1>>.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x06000C4F RID: 3151 RVA: 0x000080E3 File Offset: 0x000062E3
			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x17000175 RID: 373
			// (get) Token: 0x06000C50 RID: 3152 RVA: 0x0002B1C8 File Offset: 0x000293C8
			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x040008AF RID: 2223
			private int <>1__state;

			// Token: 0x040008B0 RID: 2224
			private IGrouping<TKey, TElement> <>2__current;

			// Token: 0x040008B1 RID: 2225
			public Lookup<TKey, TElement> <>4__this;

			// Token: 0x040008B2 RID: 2226
			private IEnumerator<IGrouping<TKey, TElement>> <>7__wrap1;
		}
	}
}

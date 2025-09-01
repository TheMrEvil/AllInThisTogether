using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Unity;

namespace System.Text.RegularExpressions
{
	/// <summary>Represents the set of successful matches found by iteratively applying a regular expression pattern to the input string.</summary>
	// Token: 0x020001F2 RID: 498
	[DebuggerDisplay("Count = {Count}")]
	[DebuggerTypeProxy(typeof(CollectionDebuggerProxy<Match>))]
	[Serializable]
	public class MatchCollection : IList<Match>, ICollection<Match>, IEnumerable<Match>, IEnumerable, IReadOnlyList<Match>, IReadOnlyCollection<Match>, IList, ICollection
	{
		// Token: 0x06000D36 RID: 3382 RVA: 0x00036298 File Offset: 0x00034498
		internal MatchCollection(Regex regex, string input, int beginning, int length, int startat)
		{
			if (startat < 0 || startat > input.Length)
			{
				throw new ArgumentOutOfRangeException("startat", "Start index cannot be less than 0 or greater than input length.");
			}
			this._regex = regex;
			this._input = input;
			this._beginning = beginning;
			this._length = length;
			this._startat = startat;
			this._prevlen = -1;
			this._matches = new List<Match>();
			this._done = false;
		}

		/// <summary>Gets a value that indicates whether the collection is read only.</summary>
		/// <returns>
		///   <see langword="true" /> in all cases.</returns>
		// Token: 0x1700024B RID: 587
		// (get) Token: 0x06000D37 RID: 3383 RVA: 0x0000390E File Offset: 0x00001B0E
		public bool IsReadOnly
		{
			get
			{
				return true;
			}
		}

		/// <summary>Gets the number of matches.</summary>
		/// <returns>The number of matches.</returns>
		/// <exception cref="T:System.Text.RegularExpressions.RegexMatchTimeoutException">A time-out occurred.</exception>
		// Token: 0x1700024C RID: 588
		// (get) Token: 0x06000D38 RID: 3384 RVA: 0x00036308 File Offset: 0x00034508
		public int Count
		{
			get
			{
				this.EnsureInitialized();
				return this._matches.Count;
			}
		}

		/// <summary>Gets an individual member of the collection.</summary>
		/// <param name="i">Index into the <see cref="T:System.Text.RegularExpressions.Match" /> collection.</param>
		/// <returns>The captured substring at position <paramref name="i" /> in the collection.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="i" /> is less than 0 or greater than or equal to <see cref="P:System.Text.RegularExpressions.MatchCollection.Count" />.</exception>
		/// <exception cref="T:System.Text.RegularExpressions.RegexMatchTimeoutException">A time-out occurred.</exception>
		// Token: 0x1700024D RID: 589
		public virtual Match this[int i]
		{
			get
			{
				if (i < 0)
				{
					throw new ArgumentOutOfRangeException("i");
				}
				Match match = this.GetMatch(i);
				if (match == null)
				{
					throw new ArgumentOutOfRangeException("i");
				}
				return match;
			}
		}

		/// <summary>Provides an enumerator that iterates through the collection.</summary>
		/// <returns>An object that contains all <see cref="T:System.Text.RegularExpressions.Match" /> objects within the <see cref="T:System.Text.RegularExpressions.MatchCollection" />.</returns>
		/// <exception cref="T:System.Text.RegularExpressions.RegexMatchTimeoutException">A time-out occurred.</exception>
		// Token: 0x06000D3A RID: 3386 RVA: 0x00036341 File Offset: 0x00034541
		public IEnumerator GetEnumerator()
		{
			return new MatchCollection.Enumerator(this);
		}

		// Token: 0x06000D3B RID: 3387 RVA: 0x00036341 File Offset: 0x00034541
		IEnumerator<Match> IEnumerable<Match>.GetEnumerator()
		{
			return new MatchCollection.Enumerator(this);
		}

		// Token: 0x06000D3C RID: 3388 RVA: 0x0003634C File Offset: 0x0003454C
		private Match GetMatch(int i)
		{
			if (this._matches.Count > i)
			{
				return this._matches[i];
			}
			if (this._done)
			{
				return null;
			}
			for (;;)
			{
				Match match = this._regex.Run(false, this._prevlen, this._input, this._beginning, this._length, this._startat);
				if (!match.Success)
				{
					break;
				}
				this._matches.Add(match);
				this._prevlen = match.Length;
				this._startat = match._textpos;
				if (this._matches.Count > i)
				{
					return match;
				}
			}
			this._done = true;
			return null;
		}

		// Token: 0x06000D3D RID: 3389 RVA: 0x000363ED File Offset: 0x000345ED
		private void EnsureInitialized()
		{
			if (!this._done)
			{
				this.GetMatch(int.MaxValue);
			}
		}

		/// <summary>Gets a value indicating whether access to the collection is synchronized (thread-safe).</summary>
		/// <returns>
		///   <see langword="false" /> in all cases.</returns>
		// Token: 0x1700024E RID: 590
		// (get) Token: 0x06000D3E RID: 3390 RVA: 0x00003062 File Offset: 0x00001262
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets an object that can be used to synchronize access to the collection.</summary>
		/// <returns>An object that can be used to synchronize access to the collection. This property always returns the object itself.</returns>
		// Token: 0x1700024F RID: 591
		// (get) Token: 0x06000D3F RID: 3391 RVA: 0x000075E1 File Offset: 0x000057E1
		public object SyncRoot
		{
			get
			{
				return this;
			}
		}

		/// <summary>Copies all the elements of the collection to the given array starting at the given index.</summary>
		/// <param name="array">The array the collection is to be copied into.</param>
		/// <param name="arrayIndex">The position in the array where copying is to begin.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="array" /> is a multi-dimensional array.</exception>
		/// <exception cref="T:System.IndexOutOfRangeException">
		///   <paramref name="arrayIndex" /> is outside the bounds of <paramref name="array" />.  
		/// -or-  
		/// <paramref name="arrayIndex" /> plus <see cref="P:System.Text.RegularExpressions.MatchCollection.Count" /> is outside the bounds of <paramref name="array" />.</exception>
		/// <exception cref="T:System.Text.RegularExpressions.RegexMatchTimeoutException">A time-out occurred.</exception>
		// Token: 0x06000D40 RID: 3392 RVA: 0x00036403 File Offset: 0x00034603
		public void CopyTo(Array array, int arrayIndex)
		{
			this.EnsureInitialized();
			((ICollection)this._matches).CopyTo(array, arrayIndex);
		}

		// Token: 0x06000D41 RID: 3393 RVA: 0x00036418 File Offset: 0x00034618
		public void CopyTo(Match[] array, int arrayIndex)
		{
			this.EnsureInitialized();
			this._matches.CopyTo(array, arrayIndex);
		}

		// Token: 0x06000D42 RID: 3394 RVA: 0x0003642D File Offset: 0x0003462D
		int IList<Match>.IndexOf(Match item)
		{
			this.EnsureInitialized();
			return this._matches.IndexOf(item);
		}

		// Token: 0x06000D43 RID: 3395 RVA: 0x0003577D File Offset: 0x0003397D
		void IList<Match>.Insert(int index, Match item)
		{
			throw new NotSupportedException("Collection is read-only.");
		}

		// Token: 0x06000D44 RID: 3396 RVA: 0x0003577D File Offset: 0x0003397D
		void IList<Match>.RemoveAt(int index)
		{
			throw new NotSupportedException("Collection is read-only.");
		}

		// Token: 0x17000250 RID: 592
		Match IList<Match>.this[int index]
		{
			get
			{
				return this[index];
			}
			set
			{
				throw new NotSupportedException("Collection is read-only.");
			}
		}

		// Token: 0x06000D47 RID: 3399 RVA: 0x0003577D File Offset: 0x0003397D
		void ICollection<Match>.Add(Match item)
		{
			throw new NotSupportedException("Collection is read-only.");
		}

		// Token: 0x06000D48 RID: 3400 RVA: 0x0003577D File Offset: 0x0003397D
		void ICollection<Match>.Clear()
		{
			throw new NotSupportedException("Collection is read-only.");
		}

		// Token: 0x06000D49 RID: 3401 RVA: 0x0003644A File Offset: 0x0003464A
		bool ICollection<Match>.Contains(Match item)
		{
			this.EnsureInitialized();
			return this._matches.Contains(item);
		}

		// Token: 0x06000D4A RID: 3402 RVA: 0x0003577D File Offset: 0x0003397D
		bool ICollection<Match>.Remove(Match item)
		{
			throw new NotSupportedException("Collection is read-only.");
		}

		// Token: 0x06000D4B RID: 3403 RVA: 0x0003577D File Offset: 0x0003397D
		int IList.Add(object value)
		{
			throw new NotSupportedException("Collection is read-only.");
		}

		// Token: 0x06000D4C RID: 3404 RVA: 0x0003577D File Offset: 0x0003397D
		void IList.Clear()
		{
			throw new NotSupportedException("Collection is read-only.");
		}

		// Token: 0x06000D4D RID: 3405 RVA: 0x0003645E File Offset: 0x0003465E
		bool IList.Contains(object value)
		{
			return value is Match && ((ICollection<Match>)this).Contains((Match)value);
		}

		// Token: 0x06000D4E RID: 3406 RVA: 0x00036476 File Offset: 0x00034676
		int IList.IndexOf(object value)
		{
			if (!(value is Match))
			{
				return -1;
			}
			return ((IList<Match>)this).IndexOf((Match)value);
		}

		// Token: 0x06000D4F RID: 3407 RVA: 0x0003577D File Offset: 0x0003397D
		void IList.Insert(int index, object value)
		{
			throw new NotSupportedException("Collection is read-only.");
		}

		// Token: 0x17000251 RID: 593
		// (get) Token: 0x06000D50 RID: 3408 RVA: 0x0000390E File Offset: 0x00001B0E
		bool IList.IsFixedSize
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000D51 RID: 3409 RVA: 0x0003577D File Offset: 0x0003397D
		void IList.Remove(object value)
		{
			throw new NotSupportedException("Collection is read-only.");
		}

		// Token: 0x06000D52 RID: 3410 RVA: 0x0003577D File Offset: 0x0003397D
		void IList.RemoveAt(int index)
		{
			throw new NotSupportedException("Collection is read-only.");
		}

		// Token: 0x17000252 RID: 594
		object IList.this[int index]
		{
			get
			{
				return this[index];
			}
			set
			{
				throw new NotSupportedException("Collection is read-only.");
			}
		}

		// Token: 0x06000D55 RID: 3413 RVA: 0x00013BCA File Offset: 0x00011DCA
		internal MatchCollection()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x040007F6 RID: 2038
		private readonly Regex _regex;

		// Token: 0x040007F7 RID: 2039
		private readonly List<Match> _matches;

		// Token: 0x040007F8 RID: 2040
		private bool _done;

		// Token: 0x040007F9 RID: 2041
		private readonly string _input;

		// Token: 0x040007FA RID: 2042
		private readonly int _beginning;

		// Token: 0x040007FB RID: 2043
		private readonly int _length;

		// Token: 0x040007FC RID: 2044
		private int _startat;

		// Token: 0x040007FD RID: 2045
		private int _prevlen;

		// Token: 0x020001F3 RID: 499
		[Serializable]
		private sealed class Enumerator : IEnumerator<Match>, IDisposable, IEnumerator
		{
			// Token: 0x06000D56 RID: 3414 RVA: 0x0003648E File Offset: 0x0003468E
			internal Enumerator(MatchCollection collection)
			{
				this._collection = collection;
				this._index = -1;
			}

			// Token: 0x06000D57 RID: 3415 RVA: 0x000364A4 File Offset: 0x000346A4
			public bool MoveNext()
			{
				if (this._index == -2)
				{
					return false;
				}
				this._index++;
				if (this._collection.GetMatch(this._index) == null)
				{
					this._index = -2;
					return false;
				}
				return true;
			}

			// Token: 0x17000253 RID: 595
			// (get) Token: 0x06000D58 RID: 3416 RVA: 0x000364DE File Offset: 0x000346DE
			public Match Current
			{
				get
				{
					if (this._index < 0)
					{
						throw new InvalidOperationException("Enumeration has either not started or has already finished.");
					}
					return this._collection.GetMatch(this._index);
				}
			}

			// Token: 0x17000254 RID: 596
			// (get) Token: 0x06000D59 RID: 3417 RVA: 0x00036505 File Offset: 0x00034705
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x06000D5A RID: 3418 RVA: 0x0003650D File Offset: 0x0003470D
			void IEnumerator.Reset()
			{
				this._index = -1;
			}

			// Token: 0x06000D5B RID: 3419 RVA: 0x00003917 File Offset: 0x00001B17
			void IDisposable.Dispose()
			{
			}

			// Token: 0x040007FE RID: 2046
			private readonly MatchCollection _collection;

			// Token: 0x040007FF RID: 2047
			private int _index;
		}
	}
}

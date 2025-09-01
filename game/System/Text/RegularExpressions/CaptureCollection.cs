using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Unity;

namespace System.Text.RegularExpressions
{
	/// <summary>Represents the set of captures made by a single capturing group.</summary>
	// Token: 0x020001E8 RID: 488
	[DebuggerTypeProxy(typeof(CollectionDebuggerProxy<Capture>))]
	[DebuggerDisplay("Count = {Count}")]
	public class CaptureCollection : IList<Capture>, ICollection<Capture>, IEnumerable<Capture>, IEnumerable, IReadOnlyList<Capture>, IReadOnlyCollection<Capture>, IList, ICollection
	{
		// Token: 0x06000CC5 RID: 3269 RVA: 0x0003559B File Offset: 0x0003379B
		internal CaptureCollection(Group group)
		{
			this._group = group;
			this._capcount = this._group._capcount;
		}

		/// <summary>Gets a value that indicates whether the collection is read only.</summary>
		/// <returns>
		///   <see langword="true" /> in all cases.</returns>
		// Token: 0x1700022F RID: 559
		// (get) Token: 0x06000CC6 RID: 3270 RVA: 0x0000390E File Offset: 0x00001B0E
		public bool IsReadOnly
		{
			get
			{
				return true;
			}
		}

		/// <summary>Gets the number of substrings captured by the group.</summary>
		/// <returns>The number of items in the <see cref="T:System.Text.RegularExpressions.CaptureCollection" />.</returns>
		// Token: 0x17000230 RID: 560
		// (get) Token: 0x06000CC7 RID: 3271 RVA: 0x000355BB File Offset: 0x000337BB
		public int Count
		{
			get
			{
				return this._capcount;
			}
		}

		/// <summary>Gets an individual member of the collection.</summary>
		/// <param name="i">Index into the capture collection.</param>
		/// <returns>The captured substring at position <paramref name="i" /> in the collection.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="i" /> is less than 0 or greater than <see cref="P:System.Text.RegularExpressions.CaptureCollection.Count" />.</exception>
		// Token: 0x17000231 RID: 561
		public Capture this[int i]
		{
			get
			{
				return this.GetCapture(i);
			}
		}

		/// <summary>Provides an enumerator that iterates through the collection.</summary>
		/// <returns>An object that contains all <see cref="T:System.Text.RegularExpressions.Capture" /> objects within the <see cref="T:System.Text.RegularExpressions.CaptureCollection" />.</returns>
		// Token: 0x06000CC9 RID: 3273 RVA: 0x000355CC File Offset: 0x000337CC
		public IEnumerator GetEnumerator()
		{
			return new CaptureCollection.Enumerator(this);
		}

		// Token: 0x06000CCA RID: 3274 RVA: 0x000355CC File Offset: 0x000337CC
		IEnumerator<Capture> IEnumerable<Capture>.GetEnumerator()
		{
			return new CaptureCollection.Enumerator(this);
		}

		// Token: 0x06000CCB RID: 3275 RVA: 0x000355D4 File Offset: 0x000337D4
		private Capture GetCapture(int i)
		{
			if (i == this._capcount - 1 && i >= 0)
			{
				return this._group;
			}
			if (i >= this._capcount || i < 0)
			{
				throw new ArgumentOutOfRangeException("i");
			}
			if (this._captures == null)
			{
				this.ForceInitialized();
			}
			return this._captures[i];
		}

		// Token: 0x06000CCC RID: 3276 RVA: 0x00035628 File Offset: 0x00033828
		internal void ForceInitialized()
		{
			this._captures = new Capture[this._capcount];
			for (int i = 0; i < this._capcount - 1; i++)
			{
				this._captures[i] = new Capture(this._group.Text, this._group._caps[i * 2], this._group._caps[i * 2 + 1]);
			}
		}

		/// <summary>Gets a value that indicates whether access to the collection is synchronized (thread-safe).</summary>
		/// <returns>
		///   <see langword="false" /> in all cases.</returns>
		// Token: 0x17000232 RID: 562
		// (get) Token: 0x06000CCD RID: 3277 RVA: 0x00003062 File Offset: 0x00001262
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets an object that can be used to synchronize access to the collection.</summary>
		/// <returns>An object that can be used to synchronize access to the collection.</returns>
		// Token: 0x17000233 RID: 563
		// (get) Token: 0x06000CCE RID: 3278 RVA: 0x00035691 File Offset: 0x00033891
		public object SyncRoot
		{
			get
			{
				return this._group;
			}
		}

		/// <summary>Copies all the elements of the collection to the given array beginning at the given index.</summary>
		/// <param name="array">The array the collection is to be copied into.</param>
		/// <param name="arrayIndex">The position in the destination array where copying is to begin.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="arrayIndex" /> is outside the bounds of <paramref name="array" />.  
		/// -or-  
		/// <paramref name="arrayIndex" /> plus <see cref="P:System.Text.RegularExpressions.CaptureCollection.Count" /> is outside the bounds of <paramref name="array" />.</exception>
		// Token: 0x06000CCF RID: 3279 RVA: 0x0003569C File Offset: 0x0003389C
		public void CopyTo(Array array, int arrayIndex)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			int num = arrayIndex;
			for (int i = 0; i < this.Count; i++)
			{
				array.SetValue(this[i], num);
				num++;
			}
		}

		// Token: 0x06000CD0 RID: 3280 RVA: 0x000356DC File Offset: 0x000338DC
		public void CopyTo(Capture[] array, int arrayIndex)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (arrayIndex < 0 || arrayIndex > array.Length)
			{
				throw new ArgumentOutOfRangeException("arrayIndex");
			}
			if (array.Length - arrayIndex < this.Count)
			{
				throw new ArgumentException("Destination array is not long enough to copy all the items in the collection. Check array index and length.");
			}
			int num = arrayIndex;
			for (int i = 0; i < this.Count; i++)
			{
				array[num] = this[i];
				num++;
			}
		}

		// Token: 0x06000CD1 RID: 3281 RVA: 0x00035748 File Offset: 0x00033948
		int IList<Capture>.IndexOf(Capture item)
		{
			for (int i = 0; i < this.Count; i++)
			{
				if (EqualityComparer<Capture>.Default.Equals(this[i], item))
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x06000CD2 RID: 3282 RVA: 0x0003577D File Offset: 0x0003397D
		void IList<Capture>.Insert(int index, Capture item)
		{
			throw new NotSupportedException("Collection is read-only.");
		}

		// Token: 0x06000CD3 RID: 3283 RVA: 0x0003577D File Offset: 0x0003397D
		void IList<Capture>.RemoveAt(int index)
		{
			throw new NotSupportedException("Collection is read-only.");
		}

		// Token: 0x17000234 RID: 564
		Capture IList<Capture>.this[int index]
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

		// Token: 0x06000CD6 RID: 3286 RVA: 0x0003577D File Offset: 0x0003397D
		void ICollection<Capture>.Add(Capture item)
		{
			throw new NotSupportedException("Collection is read-only.");
		}

		// Token: 0x06000CD7 RID: 3287 RVA: 0x0003577D File Offset: 0x0003397D
		void ICollection<Capture>.Clear()
		{
			throw new NotSupportedException("Collection is read-only.");
		}

		// Token: 0x06000CD8 RID: 3288 RVA: 0x00035792 File Offset: 0x00033992
		bool ICollection<Capture>.Contains(Capture item)
		{
			return ((IList<Capture>)this).IndexOf(item) >= 0;
		}

		// Token: 0x06000CD9 RID: 3289 RVA: 0x0003577D File Offset: 0x0003397D
		bool ICollection<Capture>.Remove(Capture item)
		{
			throw new NotSupportedException("Collection is read-only.");
		}

		// Token: 0x06000CDA RID: 3290 RVA: 0x0003577D File Offset: 0x0003397D
		int IList.Add(object value)
		{
			throw new NotSupportedException("Collection is read-only.");
		}

		// Token: 0x06000CDB RID: 3291 RVA: 0x0003577D File Offset: 0x0003397D
		void IList.Clear()
		{
			throw new NotSupportedException("Collection is read-only.");
		}

		// Token: 0x06000CDC RID: 3292 RVA: 0x000357A1 File Offset: 0x000339A1
		bool IList.Contains(object value)
		{
			return value is Capture && ((ICollection<Capture>)this).Contains((Capture)value);
		}

		// Token: 0x06000CDD RID: 3293 RVA: 0x000357B9 File Offset: 0x000339B9
		int IList.IndexOf(object value)
		{
			if (!(value is Capture))
			{
				return -1;
			}
			return ((IList<Capture>)this).IndexOf((Capture)value);
		}

		// Token: 0x06000CDE RID: 3294 RVA: 0x0003577D File Offset: 0x0003397D
		void IList.Insert(int index, object value)
		{
			throw new NotSupportedException("Collection is read-only.");
		}

		// Token: 0x17000235 RID: 565
		// (get) Token: 0x06000CDF RID: 3295 RVA: 0x0000390E File Offset: 0x00001B0E
		bool IList.IsFixedSize
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000CE0 RID: 3296 RVA: 0x0003577D File Offset: 0x0003397D
		void IList.Remove(object value)
		{
			throw new NotSupportedException("Collection is read-only.");
		}

		// Token: 0x06000CE1 RID: 3297 RVA: 0x0003577D File Offset: 0x0003397D
		void IList.RemoveAt(int index)
		{
			throw new NotSupportedException("Collection is read-only.");
		}

		// Token: 0x17000236 RID: 566
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

		// Token: 0x06000CE4 RID: 3300 RVA: 0x00013BCA File Offset: 0x00011DCA
		internal CaptureCollection()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x040007D5 RID: 2005
		private readonly Group _group;

		// Token: 0x040007D6 RID: 2006
		private readonly int _capcount;

		// Token: 0x040007D7 RID: 2007
		private Capture[] _captures;

		// Token: 0x020001E9 RID: 489
		[Serializable]
		private sealed class Enumerator : IEnumerator<Capture>, IDisposable, IEnumerator
		{
			// Token: 0x06000CE5 RID: 3301 RVA: 0x000357D1 File Offset: 0x000339D1
			internal Enumerator(CaptureCollection collection)
			{
				this._collection = collection;
				this._index = -1;
			}

			// Token: 0x06000CE6 RID: 3302 RVA: 0x000357E8 File Offset: 0x000339E8
			public bool MoveNext()
			{
				int count = this._collection.Count;
				if (this._index >= count)
				{
					return false;
				}
				this._index++;
				return this._index < count;
			}

			// Token: 0x17000237 RID: 567
			// (get) Token: 0x06000CE7 RID: 3303 RVA: 0x00035823 File Offset: 0x00033A23
			public Capture Current
			{
				get
				{
					if (this._index < 0 || this._index >= this._collection.Count)
					{
						throw new InvalidOperationException("Enumeration has either not started or has already finished.");
					}
					return this._collection[this._index];
				}
			}

			// Token: 0x17000238 RID: 568
			// (get) Token: 0x06000CE8 RID: 3304 RVA: 0x0003585D File Offset: 0x00033A5D
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x06000CE9 RID: 3305 RVA: 0x00035865 File Offset: 0x00033A65
			void IEnumerator.Reset()
			{
				this._index = -1;
			}

			// Token: 0x06000CEA RID: 3306 RVA: 0x00003917 File Offset: 0x00001B17
			void IDisposable.Dispose()
			{
			}

			// Token: 0x040007D8 RID: 2008
			private readonly CaptureCollection _collection;

			// Token: 0x040007D9 RID: 2009
			private int _index;
		}
	}
}

using System;

namespace System.Collections.Generic
{
	// Token: 0x02000AAB RID: 2731
	internal class LowLevelDictionary<TKey, TValue>
	{
		// Token: 0x060061BE RID: 25022 RVA: 0x00146C5F File Offset: 0x00144E5F
		public LowLevelDictionary() : this(17, new LowLevelDictionary<TKey, TValue>.DefaultComparer<TKey>())
		{
		}

		// Token: 0x060061BF RID: 25023 RVA: 0x00146C6E File Offset: 0x00144E6E
		public LowLevelDictionary(int capacity) : this(capacity, new LowLevelDictionary<TKey, TValue>.DefaultComparer<TKey>())
		{
		}

		// Token: 0x060061C0 RID: 25024 RVA: 0x00146C7C File Offset: 0x00144E7C
		public LowLevelDictionary(IEqualityComparer<TKey> comparer) : this(17, comparer)
		{
		}

		// Token: 0x060061C1 RID: 25025 RVA: 0x00146C87 File Offset: 0x00144E87
		public LowLevelDictionary(int capacity, IEqualityComparer<TKey> comparer)
		{
			this._comparer = comparer;
			this.Clear(capacity);
		}

		// Token: 0x17001163 RID: 4451
		// (get) Token: 0x060061C2 RID: 25026 RVA: 0x00146C9D File Offset: 0x00144E9D
		public int Count
		{
			get
			{
				return this._numEntries;
			}
		}

		// Token: 0x17001164 RID: 4452
		public TValue this[TKey key]
		{
			get
			{
				if (key == null)
				{
					throw new ArgumentNullException("key");
				}
				LowLevelDictionary<TKey, TValue>.Entry entry = this.Find(key);
				if (entry == null)
				{
					throw new KeyNotFoundException(SR.Format("The given key '{0}' was not present in the dictionary.", key.ToString()));
				}
				return entry._value;
			}
			set
			{
				if (key == null)
				{
					throw new ArgumentNullException("key");
				}
				this._version++;
				LowLevelDictionary<TKey, TValue>.Entry entry = this.Find(key);
				if (entry != null)
				{
					entry._value = value;
					return;
				}
				this.UncheckedAdd(key, value);
			}
		}

		// Token: 0x060061C5 RID: 25029 RVA: 0x00146D40 File Offset: 0x00144F40
		public bool TryGetValue(TKey key, out TValue value)
		{
			value = default(TValue);
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			LowLevelDictionary<TKey, TValue>.Entry entry = this.Find(key);
			if (entry != null)
			{
				value = entry._value;
				return true;
			}
			return false;
		}

		// Token: 0x060061C6 RID: 25030 RVA: 0x00146D84 File Offset: 0x00144F84
		public void Add(TKey key, TValue value)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			if (this.Find(key) != null)
			{
				throw new ArgumentException(SR.Format("An item with the same key has already been added. Key: {0}", key));
			}
			this._version++;
			this.UncheckedAdd(key, value);
		}

		// Token: 0x060061C7 RID: 25031 RVA: 0x00146DDA File Offset: 0x00144FDA
		public void Clear(int capacity = 17)
		{
			this._version++;
			this._buckets = new LowLevelDictionary<TKey, TValue>.Entry[capacity];
			this._numEntries = 0;
		}

		// Token: 0x060061C8 RID: 25032 RVA: 0x00146E00 File Offset: 0x00145000
		public bool Remove(TKey key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			int bucket = this.GetBucket(key, 0);
			LowLevelDictionary<TKey, TValue>.Entry entry = null;
			for (LowLevelDictionary<TKey, TValue>.Entry entry2 = this._buckets[bucket]; entry2 != null; entry2 = entry2._next)
			{
				if (this._comparer.Equals(key, entry2._key))
				{
					if (entry == null)
					{
						this._buckets[bucket] = entry2._next;
					}
					else
					{
						entry._next = entry2._next;
					}
					this._version++;
					this._numEntries--;
					return true;
				}
				entry = entry2;
			}
			return false;
		}

		// Token: 0x060061C9 RID: 25033 RVA: 0x00146E94 File Offset: 0x00145094
		private LowLevelDictionary<TKey, TValue>.Entry Find(TKey key)
		{
			int bucket = this.GetBucket(key, 0);
			for (LowLevelDictionary<TKey, TValue>.Entry entry = this._buckets[bucket]; entry != null; entry = entry._next)
			{
				if (this._comparer.Equals(key, entry._key))
				{
					return entry;
				}
			}
			return null;
		}

		// Token: 0x060061CA RID: 25034 RVA: 0x00146ED8 File Offset: 0x001450D8
		private LowLevelDictionary<TKey, TValue>.Entry UncheckedAdd(TKey key, TValue value)
		{
			LowLevelDictionary<TKey, TValue>.Entry entry = new LowLevelDictionary<TKey, TValue>.Entry();
			entry._key = key;
			entry._value = value;
			int bucket = this.GetBucket(key, 0);
			entry._next = this._buckets[bucket];
			this._buckets[bucket] = entry;
			this._numEntries++;
			if (this._numEntries > this._buckets.Length * 2)
			{
				this.ExpandBuckets();
			}
			return entry;
		}

		// Token: 0x060061CB RID: 25035 RVA: 0x00146F40 File Offset: 0x00145140
		private void ExpandBuckets()
		{
			try
			{
				int num = this._buckets.Length * 2 + 1;
				LowLevelDictionary<TKey, TValue>.Entry[] array = new LowLevelDictionary<TKey, TValue>.Entry[num];
				for (int i = 0; i < this._buckets.Length; i++)
				{
					LowLevelDictionary<TKey, TValue>.Entry next;
					for (LowLevelDictionary<TKey, TValue>.Entry entry = this._buckets[i]; entry != null; entry = next)
					{
						next = entry._next;
						int bucket = this.GetBucket(entry._key, num);
						entry._next = array[bucket];
						array[bucket] = entry;
					}
				}
				this._buckets = array;
			}
			catch (OutOfMemoryException)
			{
			}
		}

		// Token: 0x060061CC RID: 25036 RVA: 0x00146FC4 File Offset: 0x001451C4
		private int GetBucket(TKey key, int numBuckets = 0)
		{
			return (this._comparer.GetHashCode(key) & int.MaxValue) % ((numBuckets == 0) ? this._buckets.Length : numBuckets);
		}

		// Token: 0x04003A06 RID: 14854
		private const int DefaultSize = 17;

		// Token: 0x04003A07 RID: 14855
		private LowLevelDictionary<TKey, TValue>.Entry[] _buckets;

		// Token: 0x04003A08 RID: 14856
		private int _numEntries;

		// Token: 0x04003A09 RID: 14857
		private int _version;

		// Token: 0x04003A0A RID: 14858
		private IEqualityComparer<TKey> _comparer;

		// Token: 0x02000AAC RID: 2732
		private sealed class Entry
		{
			// Token: 0x060061CD RID: 25037 RVA: 0x0000259F File Offset: 0x0000079F
			public Entry()
			{
			}

			// Token: 0x04003A0B RID: 14859
			public TKey _key;

			// Token: 0x04003A0C RID: 14860
			public TValue _value;

			// Token: 0x04003A0D RID: 14861
			public LowLevelDictionary<TKey, TValue>.Entry _next;
		}

		// Token: 0x02000AAD RID: 2733
		private sealed class DefaultComparer<T> : IEqualityComparer<T>
		{
			// Token: 0x060061CE RID: 25038 RVA: 0x00146FE8 File Offset: 0x001451E8
			public bool Equals(T x, T y)
			{
				if (x == null)
				{
					return y == null;
				}
				IEquatable<T> equatable = x as IEquatable<T>;
				if (equatable != null)
				{
					return equatable.Equals(y);
				}
				return x.Equals(y);
			}

			// Token: 0x060061CF RID: 25039 RVA: 0x0014702F File Offset: 0x0014522F
			public int GetHashCode(T obj)
			{
				return obj.GetHashCode();
			}

			// Token: 0x060061D0 RID: 25040 RVA: 0x0000259F File Offset: 0x0000079F
			public DefaultComparer()
			{
			}
		}
	}
}

using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading;

namespace System.Collections.Generic
{
	/// <summary>Represents a collection of key/value pairs that are sorted by key based on the associated <see cref="T:System.Collections.Generic.IComparer`1" /> implementation.</summary>
	/// <typeparam name="TKey">The type of keys in the collection.</typeparam>
	/// <typeparam name="TValue">The type of values in the collection.</typeparam>
	// Token: 0x020004DB RID: 1243
	[DebuggerDisplay("Count = {Count}")]
	[DebuggerTypeProxy(typeof(IDictionaryDebugView<, >))]
	[Serializable]
	public class SortedList<TKey, TValue> : IDictionary<TKey, TValue>, ICollection<KeyValuePair<TKey, TValue>>, IEnumerable<KeyValuePair<TKey, TValue>>, IEnumerable, IDictionary, ICollection, IReadOnlyDictionary<!0, !1>, IReadOnlyCollection<KeyValuePair<TKey, TValue>>
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Generic.SortedList`2" /> class that is empty, has the default initial capacity, and uses the default <see cref="T:System.Collections.Generic.IComparer`1" />.</summary>
		// Token: 0x06002845 RID: 10309 RVA: 0x0008AB93 File Offset: 0x00088D93
		public SortedList()
		{
			this.keys = Array.Empty<TKey>();
			this.values = Array.Empty<TValue>();
			this._size = 0;
			this.comparer = Comparer<TKey>.Default;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Generic.SortedList`2" /> class that is empty, has the specified initial capacity, and uses the default <see cref="T:System.Collections.Generic.IComparer`1" />.</summary>
		/// <param name="capacity">The initial number of elements that the <see cref="T:System.Collections.Generic.SortedList`2" /> can contain.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="capacity" /> is less than zero.</exception>
		// Token: 0x06002846 RID: 10310 RVA: 0x0008ABC4 File Offset: 0x00088DC4
		public SortedList(int capacity)
		{
			if (capacity < 0)
			{
				throw new ArgumentOutOfRangeException("capacity", capacity, "Non-negative number required.");
			}
			this.keys = new TKey[capacity];
			this.values = new TValue[capacity];
			this.comparer = Comparer<TKey>.Default;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Generic.SortedList`2" /> class that is empty, has the default initial capacity, and uses the specified <see cref="T:System.Collections.Generic.IComparer`1" />.</summary>
		/// <param name="comparer">The <see cref="T:System.Collections.Generic.IComparer`1" /> implementation to use when comparing keys.  
		///  -or-  
		///  <see langword="null" /> to use the default <see cref="T:System.Collections.Generic.Comparer`1" /> for the type of the key.</param>
		// Token: 0x06002847 RID: 10311 RVA: 0x0008AC14 File Offset: 0x00088E14
		public SortedList(IComparer<TKey> comparer) : this()
		{
			if (comparer != null)
			{
				this.comparer = comparer;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Generic.SortedList`2" /> class that is empty, has the specified initial capacity, and uses the specified <see cref="T:System.Collections.Generic.IComparer`1" />.</summary>
		/// <param name="capacity">The initial number of elements that the <see cref="T:System.Collections.Generic.SortedList`2" /> can contain.</param>
		/// <param name="comparer">The <see cref="T:System.Collections.Generic.IComparer`1" /> implementation to use when comparing keys.  
		///  -or-  
		///  <see langword="null" /> to use the default <see cref="T:System.Collections.Generic.Comparer`1" /> for the type of the key.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="capacity" /> is less than zero.</exception>
		// Token: 0x06002848 RID: 10312 RVA: 0x0008AC26 File Offset: 0x00088E26
		public SortedList(int capacity, IComparer<TKey> comparer) : this(comparer)
		{
			this.Capacity = capacity;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Generic.SortedList`2" /> class that contains elements copied from the specified <see cref="T:System.Collections.Generic.IDictionary`2" />, has sufficient capacity to accommodate the number of elements copied, and uses the default <see cref="T:System.Collections.Generic.IComparer`1" />.</summary>
		/// <param name="dictionary">The <see cref="T:System.Collections.Generic.IDictionary`2" /> whose elements are copied to the new <see cref="T:System.Collections.Generic.SortedList`2" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="dictionary" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="dictionary" /> contains one or more duplicate keys.</exception>
		// Token: 0x06002849 RID: 10313 RVA: 0x0008AC36 File Offset: 0x00088E36
		public SortedList(IDictionary<TKey, TValue> dictionary) : this(dictionary, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Generic.SortedList`2" /> class that contains elements copied from the specified <see cref="T:System.Collections.Generic.IDictionary`2" />, has sufficient capacity to accommodate the number of elements copied, and uses the specified <see cref="T:System.Collections.Generic.IComparer`1" />.</summary>
		/// <param name="dictionary">The <see cref="T:System.Collections.Generic.IDictionary`2" /> whose elements are copied to the new <see cref="T:System.Collections.Generic.SortedList`2" />.</param>
		/// <param name="comparer">The <see cref="T:System.Collections.Generic.IComparer`1" /> implementation to use when comparing keys.  
		///  -or-  
		///  <see langword="null" /> to use the default <see cref="T:System.Collections.Generic.Comparer`1" /> for the type of the key.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="dictionary" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="dictionary" /> contains one or more duplicate keys.</exception>
		// Token: 0x0600284A RID: 10314 RVA: 0x0008AC40 File Offset: 0x00088E40
		public SortedList(IDictionary<TKey, TValue> dictionary, IComparer<TKey> comparer) : this((dictionary != null) ? dictionary.Count : 0, comparer)
		{
			if (dictionary == null)
			{
				throw new ArgumentNullException("dictionary");
			}
			int count = dictionary.Count;
			if (count != 0)
			{
				TKey[] array = this.keys;
				dictionary.Keys.CopyTo(array, 0);
				dictionary.Values.CopyTo(this.values, 0);
				if (count > 1)
				{
					comparer = this.Comparer;
					Array.Sort<TKey, TValue>(array, this.values, comparer);
					for (int num = 1; num != array.Length; num++)
					{
						if (comparer.Compare(array[num - 1], array[num]) == 0)
						{
							throw new ArgumentException(SR.Format("An item with the same key has already been added. Key: {0}", array[num]));
						}
					}
				}
			}
			this._size = count;
		}

		/// <summary>Adds an element with the specified key and value into the <see cref="T:System.Collections.Generic.SortedList`2" />.</summary>
		/// <param name="key">The key of the element to add.</param>
		/// <param name="value">The value of the element to add. The value can be <see langword="null" /> for reference types.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">An element with the same key already exists in the <see cref="T:System.Collections.Generic.SortedList`2" />.</exception>
		// Token: 0x0600284B RID: 10315 RVA: 0x0008AD04 File Offset: 0x00088F04
		public void Add(TKey key, TValue value)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			int num = Array.BinarySearch<TKey>(this.keys, 0, this._size, key, this.comparer);
			if (num >= 0)
			{
				throw new ArgumentException(SR.Format("An item with the same key has already been added. Key: {0}", key), "key");
			}
			this.Insert(~num, key, value);
		}

		// Token: 0x0600284C RID: 10316 RVA: 0x0008AD67 File Offset: 0x00088F67
		void ICollection<KeyValuePair<!0, !1>>.Add(KeyValuePair<TKey, TValue> keyValuePair)
		{
			this.Add(keyValuePair.Key, keyValuePair.Value);
		}

		// Token: 0x0600284D RID: 10317 RVA: 0x0008AD80 File Offset: 0x00088F80
		bool ICollection<KeyValuePair<!0, !1>>.Contains(KeyValuePair<TKey, TValue> keyValuePair)
		{
			int num = this.IndexOfKey(keyValuePair.Key);
			return num >= 0 && EqualityComparer<TValue>.Default.Equals(this.values[num], keyValuePair.Value);
		}

		// Token: 0x0600284E RID: 10318 RVA: 0x0008ADC4 File Offset: 0x00088FC4
		bool ICollection<KeyValuePair<!0, !1>>.Remove(KeyValuePair<TKey, TValue> keyValuePair)
		{
			int num = this.IndexOfKey(keyValuePair.Key);
			if (num >= 0 && EqualityComparer<TValue>.Default.Equals(this.values[num], keyValuePair.Value))
			{
				this.RemoveAt(num);
				return true;
			}
			return false;
		}

		/// <summary>Gets or sets the number of elements that the <see cref="T:System.Collections.Generic.SortedList`2" /> can contain.</summary>
		/// <returns>The number of elements that the <see cref="T:System.Collections.Generic.SortedList`2" /> can contain.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <see cref="P:System.Collections.Generic.SortedList`2.Capacity" /> is set to a value that is less than <see cref="P:System.Collections.Generic.SortedList`2.Count" />.</exception>
		/// <exception cref="T:System.OutOfMemoryException">There is not enough memory available on the system.</exception>
		// Token: 0x17000846 RID: 2118
		// (get) Token: 0x0600284F RID: 10319 RVA: 0x0008AE0C File Offset: 0x0008900C
		// (set) Token: 0x06002850 RID: 10320 RVA: 0x0008AE18 File Offset: 0x00089018
		public int Capacity
		{
			get
			{
				return this.keys.Length;
			}
			set
			{
				if (value != this.keys.Length)
				{
					if (value < this._size)
					{
						throw new ArgumentOutOfRangeException("value", value, "capacity was less than the current size.");
					}
					if (value > 0)
					{
						TKey[] destinationArray = new TKey[value];
						TValue[] destinationArray2 = new TValue[value];
						if (this._size > 0)
						{
							Array.Copy(this.keys, 0, destinationArray, 0, this._size);
							Array.Copy(this.values, 0, destinationArray2, 0, this._size);
						}
						this.keys = destinationArray;
						this.values = destinationArray2;
						return;
					}
					this.keys = Array.Empty<TKey>();
					this.values = Array.Empty<TValue>();
				}
			}
		}

		/// <summary>Gets the <see cref="T:System.Collections.Generic.IComparer`1" /> for the sorted list.</summary>
		/// <returns>The <see cref="T:System.IComparable`1" /> for the current <see cref="T:System.Collections.Generic.SortedList`2" />.</returns>
		// Token: 0x17000847 RID: 2119
		// (get) Token: 0x06002851 RID: 10321 RVA: 0x0008AEBA File Offset: 0x000890BA
		public IComparer<TKey> Comparer
		{
			get
			{
				return this.comparer;
			}
		}

		/// <summary>Adds an element with the provided key and value to the <see cref="T:System.Collections.IDictionary" />.</summary>
		/// <param name="key">The <see cref="T:System.Object" /> to use as the key of the element to add.</param>
		/// <param name="value">The <see cref="T:System.Object" /> to use as the value of the element to add.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="key" /> is of a type that is not assignable to the key type <paramref name="TKey" /> of the <see cref="T:System.Collections.IDictionary" />.  
		/// -or-  
		/// <paramref name="value" /> is of a type that is not assignable to the value type <paramref name="TValue" /> of the <see cref="T:System.Collections.IDictionary" />.  
		/// -or-  
		/// An element with the same key already exists in the <see cref="T:System.Collections.IDictionary" />.</exception>
		// Token: 0x06002852 RID: 10322 RVA: 0x0008AEC4 File Offset: 0x000890C4
		void IDictionary.Add(object key, object value)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			if (value == null && default(TValue) != null)
			{
				throw new ArgumentNullException("value");
			}
			if (!(key is TKey))
			{
				throw new ArgumentException(SR.Format("The value '{0}' is not of type '{1}' and cannot be used in this generic collection.", key, typeof(TKey)), "key");
			}
			if (!(value is TValue) && value != null)
			{
				throw new ArgumentException(SR.Format("The value '{0}' is not of type '{1}' and cannot be used in this generic collection.", value, typeof(TValue)), "value");
			}
			this.Add((TKey)((object)key), (TValue)((object)value));
		}

		/// <summary>Gets the number of key/value pairs contained in the <see cref="T:System.Collections.Generic.SortedList`2" />.</summary>
		/// <returns>The number of key/value pairs contained in the <see cref="T:System.Collections.Generic.SortedList`2" />.</returns>
		// Token: 0x17000848 RID: 2120
		// (get) Token: 0x06002853 RID: 10323 RVA: 0x0008AF62 File Offset: 0x00089162
		public int Count
		{
			get
			{
				return this._size;
			}
		}

		/// <summary>Gets a collection containing the keys in the <see cref="T:System.Collections.Generic.SortedList`2" />, in sorted order.</summary>
		/// <returns>A <see cref="T:System.Collections.Generic.IList`1" /> containing the keys in the <see cref="T:System.Collections.Generic.SortedList`2" />.</returns>
		// Token: 0x17000849 RID: 2121
		// (get) Token: 0x06002854 RID: 10324 RVA: 0x0008AF6A File Offset: 0x0008916A
		public IList<TKey> Keys
		{
			get
			{
				return this.GetKeyListHelper();
			}
		}

		// Token: 0x1700084A RID: 2122
		// (get) Token: 0x06002855 RID: 10325 RVA: 0x0008AF6A File Offset: 0x0008916A
		ICollection<TKey> IDictionary<!0, !1>.Keys
		{
			get
			{
				return this.GetKeyListHelper();
			}
		}

		/// <summary>Gets an <see cref="T:System.Collections.ICollection" /> containing the keys of the <see cref="T:System.Collections.IDictionary" />.</summary>
		/// <returns>An <see cref="T:System.Collections.ICollection" /> containing the keys of the <see cref="T:System.Collections.IDictionary" />.</returns>
		// Token: 0x1700084B RID: 2123
		// (get) Token: 0x06002856 RID: 10326 RVA: 0x0008AF6A File Offset: 0x0008916A
		ICollection IDictionary.Keys
		{
			get
			{
				return this.GetKeyListHelper();
			}
		}

		// Token: 0x1700084C RID: 2124
		// (get) Token: 0x06002857 RID: 10327 RVA: 0x0008AF6A File Offset: 0x0008916A
		IEnumerable<TKey> IReadOnlyDictionary<!0, !1>.Keys
		{
			get
			{
				return this.GetKeyListHelper();
			}
		}

		/// <summary>Gets a collection containing the values in the <see cref="T:System.Collections.Generic.SortedList`2" />.</summary>
		/// <returns>A <see cref="T:System.Collections.Generic.IList`1" /> containing the values in the <see cref="T:System.Collections.Generic.SortedList`2" />.</returns>
		// Token: 0x1700084D RID: 2125
		// (get) Token: 0x06002858 RID: 10328 RVA: 0x0008AF72 File Offset: 0x00089172
		public IList<TValue> Values
		{
			get
			{
				return this.GetValueListHelper();
			}
		}

		// Token: 0x1700084E RID: 2126
		// (get) Token: 0x06002859 RID: 10329 RVA: 0x0008AF72 File Offset: 0x00089172
		ICollection<TValue> IDictionary<!0, !1>.Values
		{
			get
			{
				return this.GetValueListHelper();
			}
		}

		/// <summary>Gets an <see cref="T:System.Collections.ICollection" /> containing the values in the <see cref="T:System.Collections.IDictionary" />.</summary>
		/// <returns>An <see cref="T:System.Collections.ICollection" /> containing the values in the <see cref="T:System.Collections.IDictionary" />.</returns>
		// Token: 0x1700084F RID: 2127
		// (get) Token: 0x0600285A RID: 10330 RVA: 0x0008AF72 File Offset: 0x00089172
		ICollection IDictionary.Values
		{
			get
			{
				return this.GetValueListHelper();
			}
		}

		// Token: 0x17000850 RID: 2128
		// (get) Token: 0x0600285B RID: 10331 RVA: 0x0008AF72 File Offset: 0x00089172
		IEnumerable<TValue> IReadOnlyDictionary<!0, !1>.Values
		{
			get
			{
				return this.GetValueListHelper();
			}
		}

		// Token: 0x0600285C RID: 10332 RVA: 0x0008AF7A File Offset: 0x0008917A
		private SortedList<TKey, TValue>.KeyList GetKeyListHelper()
		{
			if (this.keyList == null)
			{
				this.keyList = new SortedList<TKey, TValue>.KeyList(this);
			}
			return this.keyList;
		}

		// Token: 0x0600285D RID: 10333 RVA: 0x0008AF96 File Offset: 0x00089196
		private SortedList<TKey, TValue>.ValueList GetValueListHelper()
		{
			if (this.valueList == null)
			{
				this.valueList = new SortedList<TKey, TValue>.ValueList(this);
			}
			return this.valueList;
		}

		// Token: 0x17000851 RID: 2129
		// (get) Token: 0x0600285E RID: 10334 RVA: 0x00003062 File Offset: 0x00001262
		bool ICollection<KeyValuePair<!0, !1>>.IsReadOnly
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Collections.IDictionary" /> is read-only.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Collections.IDictionary" /> is read-only; otherwise, <see langword="false" />.  In the default implementation of <see cref="T:System.Collections.Generic.SortedList`2" />, this property always returns <see langword="false" />.</returns>
		// Token: 0x17000852 RID: 2130
		// (get) Token: 0x0600285F RID: 10335 RVA: 0x00003062 File Offset: 0x00001262
		bool IDictionary.IsReadOnly
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Collections.IDictionary" /> has a fixed size.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Collections.IDictionary" /> has a fixed size; otherwise, <see langword="false" />.  In the default implementation of <see cref="T:System.Collections.Generic.SortedList`2" />, this property always returns <see langword="false" />.</returns>
		// Token: 0x17000853 RID: 2131
		// (get) Token: 0x06002860 RID: 10336 RVA: 0x00003062 File Offset: 0x00001262
		bool IDictionary.IsFixedSize
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets a value indicating whether access to the <see cref="T:System.Collections.ICollection" /> is synchronized (thread safe).</summary>
		/// <returns>
		///   <see langword="true" /> if access to the <see cref="T:System.Collections.ICollection" /> is synchronized (thread safe); otherwise, <see langword="false" />.  In the default implementation of <see cref="T:System.Collections.Generic.SortedList`2" />, this property always returns <see langword="false" />.</returns>
		// Token: 0x17000854 RID: 2132
		// (get) Token: 0x06002861 RID: 10337 RVA: 0x00003062 File Offset: 0x00001262
		bool ICollection.IsSynchronized
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets an object that can be used to synchronize access to the <see cref="T:System.Collections.ICollection" />.</summary>
		/// <returns>An object that can be used to synchronize access to the <see cref="T:System.Collections.ICollection" />.  In the default implementation of <see cref="T:System.Collections.Generic.SortedList`2" />, this property always returns the current instance.</returns>
		// Token: 0x17000855 RID: 2133
		// (get) Token: 0x06002862 RID: 10338 RVA: 0x0008AFB2 File Offset: 0x000891B2
		object ICollection.SyncRoot
		{
			get
			{
				if (this._syncRoot == null)
				{
					Interlocked.CompareExchange(ref this._syncRoot, new object(), null);
				}
				return this._syncRoot;
			}
		}

		/// <summary>Removes all elements from the <see cref="T:System.Collections.Generic.SortedList`2" />.</summary>
		// Token: 0x06002863 RID: 10339 RVA: 0x0008AFD4 File Offset: 0x000891D4
		public void Clear()
		{
			this.version++;
			if (RuntimeHelpers.IsReferenceOrContainsReferences<TKey>())
			{
				Array.Clear(this.keys, 0, this._size);
			}
			if (RuntimeHelpers.IsReferenceOrContainsReferences<TValue>())
			{
				Array.Clear(this.values, 0, this._size);
			}
			this._size = 0;
		}

		/// <summary>Determines whether the <see cref="T:System.Collections.IDictionary" /> contains an element with the specified key.</summary>
		/// <param name="key">The key to locate in the <see cref="T:System.Collections.IDictionary" />.</param>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Collections.IDictionary" /> contains an element with the key; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is <see langword="null" />.</exception>
		// Token: 0x06002864 RID: 10340 RVA: 0x0008B028 File Offset: 0x00089228
		bool IDictionary.Contains(object key)
		{
			return SortedList<TKey, TValue>.IsCompatibleKey(key) && this.ContainsKey((TKey)((object)key));
		}

		/// <summary>Determines whether the <see cref="T:System.Collections.Generic.SortedList`2" /> contains a specific key.</summary>
		/// <param name="key">The key to locate in the <see cref="T:System.Collections.Generic.SortedList`2" />.</param>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Collections.Generic.SortedList`2" /> contains an element with the specified key; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is <see langword="null" />.</exception>
		// Token: 0x06002865 RID: 10341 RVA: 0x0008B040 File Offset: 0x00089240
		public bool ContainsKey(TKey key)
		{
			return this.IndexOfKey(key) >= 0;
		}

		/// <summary>Determines whether the <see cref="T:System.Collections.Generic.SortedList`2" /> contains a specific value.</summary>
		/// <param name="value">The value to locate in the <see cref="T:System.Collections.Generic.SortedList`2" />. The value can be <see langword="null" /> for reference types.</param>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Collections.Generic.SortedList`2" /> contains an element with the specified value; otherwise, <see langword="false" />.</returns>
		// Token: 0x06002866 RID: 10342 RVA: 0x0008B04F File Offset: 0x0008924F
		public bool ContainsValue(TValue value)
		{
			return this.IndexOfValue(value) >= 0;
		}

		// Token: 0x06002867 RID: 10343 RVA: 0x0008B060 File Offset: 0x00089260
		void ICollection<KeyValuePair<!0, !1>>.CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (arrayIndex < 0 || arrayIndex > array.Length)
			{
				throw new ArgumentOutOfRangeException("arrayIndex", arrayIndex, "Index was out of range. Must be non-negative and less than the size of the collection.");
			}
			if (array.Length - arrayIndex < this.Count)
			{
				throw new ArgumentException("Destination array is not long enough to copy all the items in the collection. Check array index and length.");
			}
			for (int i = 0; i < this.Count; i++)
			{
				KeyValuePair<TKey, TValue> keyValuePair = new KeyValuePair<TKey, TValue>(this.keys[i], this.values[i]);
				array[arrayIndex + i] = keyValuePair;
			}
		}

		/// <summary>Copies the elements of the <see cref="T:System.Collections.ICollection" /> to an <see cref="T:System.Array" />, starting at a particular <see cref="T:System.Array" /> index.</summary>
		/// <param name="array">The one-dimensional <see cref="T:System.Array" /> that is the destination of the elements copied from <see cref="T:System.Collections.ICollection" />. The <see cref="T:System.Array" /> must have zero-based indexing.</param>
		/// <param name="arrayIndex">The zero-based index in <paramref name="array" /> at which copying begins.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="arrayIndex" /> is less than zero.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="array" /> is multidimensional.  
		/// -or-  
		/// <paramref name="array" /> does not have zero-based indexing.  
		/// -or-  
		/// The number of elements in the source <see cref="T:System.Collections.ICollection" /> is greater than the available space from <paramref name="arrayIndex" /> to the end of the destination <paramref name="array" />.  
		/// -or-  
		/// The type of the source <see cref="T:System.Collections.ICollection" /> cannot be cast automatically to the type of the destination <paramref name="array" />.</exception>
		// Token: 0x06002868 RID: 10344 RVA: 0x0008B0F0 File Offset: 0x000892F0
		void ICollection.CopyTo(Array array, int index)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (array.Rank != 1)
			{
				throw new ArgumentException("Only single dimensional arrays are supported for the requested action.", "array");
			}
			if (array.GetLowerBound(0) != 0)
			{
				throw new ArgumentException("The lower bound of target array must be zero.", "array");
			}
			if (index < 0 || index > array.Length)
			{
				throw new ArgumentOutOfRangeException("index", index, "Index was out of range. Must be non-negative and less than the size of the collection.");
			}
			if (array.Length - index < this.Count)
			{
				throw new ArgumentException("Destination array is not long enough to copy all the items in the collection. Check array index and length.");
			}
			KeyValuePair<TKey, TValue>[] array2 = array as KeyValuePair<TKey, TValue>[];
			if (array2 != null)
			{
				for (int i = 0; i < this.Count; i++)
				{
					array2[i + index] = new KeyValuePair<TKey, TValue>(this.keys[i], this.values[i]);
				}
				return;
			}
			object[] array3 = array as object[];
			if (array3 == null)
			{
				throw new ArgumentException("Target array type is not compatible with the type of items in the collection.", "array");
			}
			try
			{
				for (int j = 0; j < this.Count; j++)
				{
					array3[j + index] = new KeyValuePair<TKey, TValue>(this.keys[j], this.values[j]);
				}
			}
			catch (ArrayTypeMismatchException)
			{
				throw new ArgumentException("Target array type is not compatible with the type of items in the collection.", "array");
			}
		}

		// Token: 0x06002869 RID: 10345 RVA: 0x0008B234 File Offset: 0x00089434
		private void EnsureCapacity(int min)
		{
			int num = (this.keys.Length == 0) ? 4 : (this.keys.Length * 2);
			if (num > 2146435071)
			{
				num = 2146435071;
			}
			if (num < min)
			{
				num = min;
			}
			this.Capacity = num;
		}

		// Token: 0x0600286A RID: 10346 RVA: 0x0008B273 File Offset: 0x00089473
		private TValue GetByIndex(int index)
		{
			if (index < 0 || index >= this._size)
			{
				throw new ArgumentOutOfRangeException("index", index, "Index was out of range. Must be non-negative and less than the size of the collection.");
			}
			return this.values[index];
		}

		/// <summary>Returns an enumerator that iterates through the <see cref="T:System.Collections.Generic.SortedList`2" />.</summary>
		/// <returns>An <see cref="T:System.Collections.Generic.IEnumerator`1" /> of type <see cref="T:System.Collections.Generic.KeyValuePair`2" /> for the <see cref="T:System.Collections.Generic.SortedList`2" />.</returns>
		// Token: 0x0600286B RID: 10347 RVA: 0x0008B2A4 File Offset: 0x000894A4
		public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
		{
			return new SortedList<TKey, TValue>.Enumerator(this, 1);
		}

		// Token: 0x0600286C RID: 10348 RVA: 0x0008B2A4 File Offset: 0x000894A4
		IEnumerator<KeyValuePair<TKey, TValue>> IEnumerable<KeyValuePair<!0, !1>>.GetEnumerator()
		{
			return new SortedList<TKey, TValue>.Enumerator(this, 1);
		}

		/// <summary>Returns an <see cref="T:System.Collections.IDictionaryEnumerator" /> for the <see cref="T:System.Collections.IDictionary" />.</summary>
		/// <returns>An <see cref="T:System.Collections.IDictionaryEnumerator" /> for the <see cref="T:System.Collections.IDictionary" />.</returns>
		// Token: 0x0600286D RID: 10349 RVA: 0x0008B2B2 File Offset: 0x000894B2
		IDictionaryEnumerator IDictionary.GetEnumerator()
		{
			return new SortedList<TKey, TValue>.Enumerator(this, 2);
		}

		/// <summary>Returns an enumerator that iterates through a collection.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> that can be used to iterate through the collection.</returns>
		// Token: 0x0600286E RID: 10350 RVA: 0x0008B2A4 File Offset: 0x000894A4
		IEnumerator IEnumerable.GetEnumerator()
		{
			return new SortedList<TKey, TValue>.Enumerator(this, 1);
		}

		// Token: 0x0600286F RID: 10351 RVA: 0x0008B2C0 File Offset: 0x000894C0
		private TKey GetKey(int index)
		{
			if (index < 0 || index >= this._size)
			{
				throw new ArgumentOutOfRangeException("index", index, "Index was out of range. Must be non-negative and less than the size of the collection.");
			}
			return this.keys[index];
		}

		/// <summary>Gets or sets the value associated with the specified key.</summary>
		/// <param name="key">The key whose value to get or set.</param>
		/// <returns>The value associated with the specified key. If the specified key is not found, a get operation throws a <see cref="T:System.Collections.Generic.KeyNotFoundException" /> and a set operation creates a new element using the specified key.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.Collections.Generic.KeyNotFoundException">The property is retrieved and <paramref name="key" /> does not exist in the collection.</exception>
		// Token: 0x17000856 RID: 2134
		public TValue this[TKey key]
		{
			get
			{
				int num = this.IndexOfKey(key);
				if (num >= 0)
				{
					return this.values[num];
				}
				throw new KeyNotFoundException(SR.Format("The given key '{0}' was not present in the dictionary.", key.ToString()));
			}
			set
			{
				if (key == null)
				{
					throw new ArgumentNullException("key");
				}
				int num = Array.BinarySearch<TKey>(this.keys, 0, this._size, key, this.comparer);
				if (num >= 0)
				{
					this.values[num] = value;
					this.version++;
					return;
				}
				this.Insert(~num, key, value);
			}
		}

		/// <summary>Gets or sets the element with the specified key.</summary>
		/// <param name="key">The key of the element to get or set.</param>
		/// <returns>The element with the specified key, or <see langword="null" /> if <paramref name="key" /> is not in the dictionary or <paramref name="key" /> is of a type that is not assignable to the key type <paramref name="TKey" /> of the <see cref="T:System.Collections.Generic.SortedList`2" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">A value is being assigned, and <paramref name="key" /> is of a type that is not assignable to the key type <paramref name="TKey" /> of the <see cref="T:System.Collections.Generic.SortedList`2" />.  
		///  -or-  
		///  A value is being assigned, and <paramref name="value" /> is of a type that is not assignable to the value type <paramref name="TValue" /> of the <see cref="T:System.Collections.Generic.SortedList`2" />.</exception>
		// Token: 0x17000857 RID: 2135
		object IDictionary.this[object key]
		{
			get
			{
				if (SortedList<TKey, TValue>.IsCompatibleKey(key))
				{
					int num = this.IndexOfKey((TKey)((object)key));
					if (num >= 0)
					{
						return this.values[num];
					}
				}
				return null;
			}
			set
			{
				if (!SortedList<TKey, TValue>.IsCompatibleKey(key))
				{
					throw new ArgumentNullException("key");
				}
				if (value == null && default(TValue) != null)
				{
					throw new ArgumentNullException("value");
				}
				TKey key2 = (TKey)((object)key);
				try
				{
					this[key2] = (TValue)((object)value);
				}
				catch (InvalidCastException)
				{
					throw new ArgumentException(SR.Format("The value '{0}' is not of type '{1}' and cannot be used in this generic collection.", value, typeof(TValue)), "value");
				}
			}
		}

		/// <summary>Searches for the specified key and returns the zero-based index within the entire <see cref="T:System.Collections.Generic.SortedList`2" />.</summary>
		/// <param name="key">The key to locate in the <see cref="T:System.Collections.Generic.SortedList`2" />.</param>
		/// <returns>The zero-based index of <paramref name="key" /> within the entire <see cref="T:System.Collections.Generic.SortedList`2" />, if found; otherwise, -1.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is <see langword="null" />.</exception>
		// Token: 0x06002874 RID: 10356 RVA: 0x0008B460 File Offset: 0x00089660
		public int IndexOfKey(TKey key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			int num = Array.BinarySearch<TKey>(this.keys, 0, this._size, key, this.comparer);
			if (num < 0)
			{
				return -1;
			}
			return num;
		}

		/// <summary>Searches for the specified value and returns the zero-based index of the first occurrence within the entire <see cref="T:System.Collections.Generic.SortedList`2" />.</summary>
		/// <param name="value">The value to locate in the <see cref="T:System.Collections.Generic.SortedList`2" />.  The value can be <see langword="null" /> for reference types.</param>
		/// <returns>The zero-based index of the first occurrence of <paramref name="value" /> within the entire <see cref="T:System.Collections.Generic.SortedList`2" />, if found; otherwise, -1.</returns>
		// Token: 0x06002875 RID: 10357 RVA: 0x0008B4A1 File Offset: 0x000896A1
		public int IndexOfValue(TValue value)
		{
			return Array.IndexOf<TValue>(this.values, value, 0, this._size);
		}

		// Token: 0x06002876 RID: 10358 RVA: 0x0008B4B8 File Offset: 0x000896B8
		private void Insert(int index, TKey key, TValue value)
		{
			if (this._size == this.keys.Length)
			{
				this.EnsureCapacity(this._size + 1);
			}
			if (index < this._size)
			{
				Array.Copy(this.keys, index, this.keys, index + 1, this._size - index);
				Array.Copy(this.values, index, this.values, index + 1, this._size - index);
			}
			this.keys[index] = key;
			this.values[index] = value;
			this._size++;
			this.version++;
		}

		/// <summary>Gets the value associated with the specified key.</summary>
		/// <param name="key">The key whose value to get.</param>
		/// <param name="value">When this method returns, the value associated with the specified key, if the key is found; otherwise, the default value for the type of the <paramref name="value" /> parameter. This parameter is passed uninitialized.</param>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Collections.Generic.SortedList`2" /> contains an element with the specified key; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is <see langword="null" />.</exception>
		// Token: 0x06002877 RID: 10359 RVA: 0x0008B55C File Offset: 0x0008975C
		public bool TryGetValue(TKey key, out TValue value)
		{
			int num = this.IndexOfKey(key);
			if (num >= 0)
			{
				value = this.values[num];
				return true;
			}
			value = default(TValue);
			return false;
		}

		/// <summary>Removes the element at the specified index of the <see cref="T:System.Collections.Generic.SortedList`2" />.</summary>
		/// <param name="index">The zero-based index of the element to remove.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero.  
		/// -or-  
		/// <paramref name="index" /> is equal to or greater than <see cref="P:System.Collections.Generic.SortedList`2.Count" />.</exception>
		// Token: 0x06002878 RID: 10360 RVA: 0x0008B594 File Offset: 0x00089794
		public void RemoveAt(int index)
		{
			if (index < 0 || index >= this._size)
			{
				throw new ArgumentOutOfRangeException("index", index, "Index was out of range. Must be non-negative and less than the size of the collection.");
			}
			this._size--;
			if (index < this._size)
			{
				Array.Copy(this.keys, index + 1, this.keys, index, this._size - index);
				Array.Copy(this.values, index + 1, this.values, index, this._size - index);
			}
			if (RuntimeHelpers.IsReferenceOrContainsReferences<TKey>())
			{
				this.keys[this._size] = default(TKey);
			}
			if (RuntimeHelpers.IsReferenceOrContainsReferences<TValue>())
			{
				this.values[this._size] = default(TValue);
			}
			this.version++;
		}

		/// <summary>Removes the element with the specified key from the <see cref="T:System.Collections.Generic.SortedList`2" />.</summary>
		/// <param name="key">The key of the element to remove.</param>
		/// <returns>
		///   <see langword="true" /> if the element is successfully removed; otherwise, <see langword="false" />.  This method also returns <see langword="false" /> if <paramref name="key" /> was not found in the original <see cref="T:System.Collections.Generic.SortedList`2" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is <see langword="null" />.</exception>
		// Token: 0x06002879 RID: 10361 RVA: 0x0008B668 File Offset: 0x00089868
		public bool Remove(TKey key)
		{
			int num = this.IndexOfKey(key);
			if (num >= 0)
			{
				this.RemoveAt(num);
			}
			return num >= 0;
		}

		/// <summary>Removes the element with the specified key from the <see cref="T:System.Collections.IDictionary" />.</summary>
		/// <param name="key">The key of the element to remove.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is <see langword="null" />.</exception>
		// Token: 0x0600287A RID: 10362 RVA: 0x0008B68F File Offset: 0x0008988F
		void IDictionary.Remove(object key)
		{
			if (SortedList<TKey, TValue>.IsCompatibleKey(key))
			{
				this.Remove((TKey)((object)key));
			}
		}

		/// <summary>Sets the capacity to the actual number of elements in the <see cref="T:System.Collections.Generic.SortedList`2" />, if that number is less than 90 percent of current capacity.</summary>
		// Token: 0x0600287B RID: 10363 RVA: 0x0008B6A8 File Offset: 0x000898A8
		public void TrimExcess()
		{
			int num = (int)((double)this.keys.Length * 0.9);
			if (this._size < num)
			{
				this.Capacity = this._size;
			}
		}

		// Token: 0x0600287C RID: 10364 RVA: 0x0008A2D4 File Offset: 0x000884D4
		private static bool IsCompatibleKey(object key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			return key is TKey;
		}

		// Token: 0x04001589 RID: 5513
		private TKey[] keys;

		// Token: 0x0400158A RID: 5514
		private TValue[] values;

		// Token: 0x0400158B RID: 5515
		private int _size;

		// Token: 0x0400158C RID: 5516
		private int version;

		// Token: 0x0400158D RID: 5517
		private IComparer<TKey> comparer;

		// Token: 0x0400158E RID: 5518
		private SortedList<TKey, TValue>.KeyList keyList;

		// Token: 0x0400158F RID: 5519
		private SortedList<TKey, TValue>.ValueList valueList;

		// Token: 0x04001590 RID: 5520
		[NonSerialized]
		private object _syncRoot;

		// Token: 0x04001591 RID: 5521
		private const int DefaultCapacity = 4;

		// Token: 0x04001592 RID: 5522
		private const int MaxArrayLength = 2146435071;

		// Token: 0x020004DC RID: 1244
		[Serializable]
		private struct Enumerator : IEnumerator<KeyValuePair<TKey, TValue>>, IDisposable, IEnumerator, IDictionaryEnumerator
		{
			// Token: 0x0600287D RID: 10365 RVA: 0x0008B6DF File Offset: 0x000898DF
			internal Enumerator(SortedList<TKey, TValue> sortedList, int getEnumeratorRetType)
			{
				this._sortedList = sortedList;
				this._index = 0;
				this._version = this._sortedList.version;
				this._getEnumeratorRetType = getEnumeratorRetType;
				this._key = default(TKey);
				this._value = default(TValue);
			}

			// Token: 0x0600287E RID: 10366 RVA: 0x0008B71F File Offset: 0x0008991F
			public void Dispose()
			{
				this._index = 0;
				this._key = default(TKey);
				this._value = default(TValue);
			}

			// Token: 0x17000858 RID: 2136
			// (get) Token: 0x0600287F RID: 10367 RVA: 0x0008B740 File Offset: 0x00089940
			object IDictionaryEnumerator.Key
			{
				get
				{
					if (this._index == 0 || this._index == this._sortedList.Count + 1)
					{
						throw new InvalidOperationException("Enumeration has either not started or has already finished.");
					}
					return this._key;
				}
			}

			// Token: 0x06002880 RID: 10368 RVA: 0x0008B778 File Offset: 0x00089978
			public bool MoveNext()
			{
				if (this._version != this._sortedList.version)
				{
					throw new InvalidOperationException("Collection was modified; enumeration operation may not execute.");
				}
				if (this._index < this._sortedList.Count)
				{
					this._key = this._sortedList.keys[this._index];
					this._value = this._sortedList.values[this._index];
					this._index++;
					return true;
				}
				this._index = this._sortedList.Count + 1;
				this._key = default(TKey);
				this._value = default(TValue);
				return false;
			}

			// Token: 0x17000859 RID: 2137
			// (get) Token: 0x06002881 RID: 10369 RVA: 0x0008B82C File Offset: 0x00089A2C
			DictionaryEntry IDictionaryEnumerator.Entry
			{
				get
				{
					if (this._index == 0 || this._index == this._sortedList.Count + 1)
					{
						throw new InvalidOperationException("Enumeration has either not started or has already finished.");
					}
					return new DictionaryEntry(this._key, this._value);
				}
			}

			// Token: 0x1700085A RID: 2138
			// (get) Token: 0x06002882 RID: 10370 RVA: 0x0008B87C File Offset: 0x00089A7C
			public KeyValuePair<TKey, TValue> Current
			{
				get
				{
					return new KeyValuePair<TKey, TValue>(this._key, this._value);
				}
			}

			// Token: 0x1700085B RID: 2139
			// (get) Token: 0x06002883 RID: 10371 RVA: 0x0008B890 File Offset: 0x00089A90
			object IEnumerator.Current
			{
				get
				{
					if (this._index == 0 || this._index == this._sortedList.Count + 1)
					{
						throw new InvalidOperationException("Enumeration has either not started or has already finished.");
					}
					if (this._getEnumeratorRetType == 2)
					{
						return new DictionaryEntry(this._key, this._value);
					}
					return new KeyValuePair<TKey, TValue>(this._key, this._value);
				}
			}

			// Token: 0x1700085C RID: 2140
			// (get) Token: 0x06002884 RID: 10372 RVA: 0x0008B905 File Offset: 0x00089B05
			object IDictionaryEnumerator.Value
			{
				get
				{
					if (this._index == 0 || this._index == this._sortedList.Count + 1)
					{
						throw new InvalidOperationException("Enumeration has either not started or has already finished.");
					}
					return this._value;
				}
			}

			// Token: 0x06002885 RID: 10373 RVA: 0x0008B93A File Offset: 0x00089B3A
			void IEnumerator.Reset()
			{
				if (this._version != this._sortedList.version)
				{
					throw new InvalidOperationException("Collection was modified; enumeration operation may not execute.");
				}
				this._index = 0;
				this._key = default(TKey);
				this._value = default(TValue);
			}

			// Token: 0x04001593 RID: 5523
			private SortedList<TKey, TValue> _sortedList;

			// Token: 0x04001594 RID: 5524
			private TKey _key;

			// Token: 0x04001595 RID: 5525
			private TValue _value;

			// Token: 0x04001596 RID: 5526
			private int _index;

			// Token: 0x04001597 RID: 5527
			private int _version;

			// Token: 0x04001598 RID: 5528
			private int _getEnumeratorRetType;

			// Token: 0x04001599 RID: 5529
			internal const int KeyValuePair = 1;

			// Token: 0x0400159A RID: 5530
			internal const int DictEntry = 2;
		}

		// Token: 0x020004DD RID: 1245
		[Serializable]
		private sealed class SortedListKeyEnumerator : IEnumerator<!0>, IDisposable, IEnumerator
		{
			// Token: 0x06002886 RID: 10374 RVA: 0x0008B979 File Offset: 0x00089B79
			internal SortedListKeyEnumerator(SortedList<TKey, TValue> sortedList)
			{
				this._sortedList = sortedList;
				this._version = sortedList.version;
			}

			// Token: 0x06002887 RID: 10375 RVA: 0x0008B994 File Offset: 0x00089B94
			public void Dispose()
			{
				this._index = 0;
				this._currentKey = default(TKey);
			}

			// Token: 0x06002888 RID: 10376 RVA: 0x0008B9AC File Offset: 0x00089BAC
			public bool MoveNext()
			{
				if (this._version != this._sortedList.version)
				{
					throw new InvalidOperationException("Collection was modified; enumeration operation may not execute.");
				}
				if (this._index < this._sortedList.Count)
				{
					this._currentKey = this._sortedList.keys[this._index];
					this._index++;
					return true;
				}
				this._index = this._sortedList.Count + 1;
				this._currentKey = default(TKey);
				return false;
			}

			// Token: 0x1700085D RID: 2141
			// (get) Token: 0x06002889 RID: 10377 RVA: 0x0008BA36 File Offset: 0x00089C36
			public TKey Current
			{
				get
				{
					return this._currentKey;
				}
			}

			// Token: 0x1700085E RID: 2142
			// (get) Token: 0x0600288A RID: 10378 RVA: 0x0008BA3E File Offset: 0x00089C3E
			object IEnumerator.Current
			{
				get
				{
					if (this._index == 0 || this._index == this._sortedList.Count + 1)
					{
						throw new InvalidOperationException("Enumeration has either not started or has already finished.");
					}
					return this._currentKey;
				}
			}

			// Token: 0x0600288B RID: 10379 RVA: 0x0008BA73 File Offset: 0x00089C73
			void IEnumerator.Reset()
			{
				if (this._version != this._sortedList.version)
				{
					throw new InvalidOperationException("Collection was modified; enumeration operation may not execute.");
				}
				this._index = 0;
				this._currentKey = default(TKey);
			}

			// Token: 0x0400159B RID: 5531
			private SortedList<TKey, TValue> _sortedList;

			// Token: 0x0400159C RID: 5532
			private int _index;

			// Token: 0x0400159D RID: 5533
			private int _version;

			// Token: 0x0400159E RID: 5534
			private TKey _currentKey;
		}

		// Token: 0x020004DE RID: 1246
		[Serializable]
		private sealed class SortedListValueEnumerator : IEnumerator<TValue>, IDisposable, IEnumerator
		{
			// Token: 0x0600288C RID: 10380 RVA: 0x0008BAA6 File Offset: 0x00089CA6
			internal SortedListValueEnumerator(SortedList<TKey, TValue> sortedList)
			{
				this._sortedList = sortedList;
				this._version = sortedList.version;
			}

			// Token: 0x0600288D RID: 10381 RVA: 0x0008BAC1 File Offset: 0x00089CC1
			public void Dispose()
			{
				this._index = 0;
				this._currentValue = default(TValue);
			}

			// Token: 0x0600288E RID: 10382 RVA: 0x0008BAD8 File Offset: 0x00089CD8
			public bool MoveNext()
			{
				if (this._version != this._sortedList.version)
				{
					throw new InvalidOperationException("Collection was modified; enumeration operation may not execute.");
				}
				if (this._index < this._sortedList.Count)
				{
					this._currentValue = this._sortedList.values[this._index];
					this._index++;
					return true;
				}
				this._index = this._sortedList.Count + 1;
				this._currentValue = default(TValue);
				return false;
			}

			// Token: 0x1700085F RID: 2143
			// (get) Token: 0x0600288F RID: 10383 RVA: 0x0008BB62 File Offset: 0x00089D62
			public TValue Current
			{
				get
				{
					return this._currentValue;
				}
			}

			// Token: 0x17000860 RID: 2144
			// (get) Token: 0x06002890 RID: 10384 RVA: 0x0008BB6A File Offset: 0x00089D6A
			object IEnumerator.Current
			{
				get
				{
					if (this._index == 0 || this._index == this._sortedList.Count + 1)
					{
						throw new InvalidOperationException("Enumeration has either not started or has already finished.");
					}
					return this._currentValue;
				}
			}

			// Token: 0x06002891 RID: 10385 RVA: 0x0008BB9F File Offset: 0x00089D9F
			void IEnumerator.Reset()
			{
				if (this._version != this._sortedList.version)
				{
					throw new InvalidOperationException("Collection was modified; enumeration operation may not execute.");
				}
				this._index = 0;
				this._currentValue = default(TValue);
			}

			// Token: 0x0400159F RID: 5535
			private SortedList<TKey, TValue> _sortedList;

			// Token: 0x040015A0 RID: 5536
			private int _index;

			// Token: 0x040015A1 RID: 5537
			private int _version;

			// Token: 0x040015A2 RID: 5538
			private TValue _currentValue;
		}

		// Token: 0x020004DF RID: 1247
		[DebuggerDisplay("Count = {Count}")]
		[DebuggerTypeProxy(typeof(DictionaryKeyCollectionDebugView<, >))]
		[Serializable]
		private sealed class KeyList : IList<TKey>, ICollection<!0>, IEnumerable<!0>, IEnumerable, ICollection
		{
			// Token: 0x06002892 RID: 10386 RVA: 0x0008BBD2 File Offset: 0x00089DD2
			internal KeyList(SortedList<TKey, TValue> dictionary)
			{
				this._dict = dictionary;
			}

			// Token: 0x17000861 RID: 2145
			// (get) Token: 0x06002893 RID: 10387 RVA: 0x0008BBE1 File Offset: 0x00089DE1
			public int Count
			{
				get
				{
					return this._dict._size;
				}
			}

			// Token: 0x17000862 RID: 2146
			// (get) Token: 0x06002894 RID: 10388 RVA: 0x0000390E File Offset: 0x00001B0E
			public bool IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x17000863 RID: 2147
			// (get) Token: 0x06002895 RID: 10389 RVA: 0x00003062 File Offset: 0x00001262
			bool ICollection.IsSynchronized
			{
				get
				{
					return false;
				}
			}

			// Token: 0x17000864 RID: 2148
			// (get) Token: 0x06002896 RID: 10390 RVA: 0x0008BBEE File Offset: 0x00089DEE
			object ICollection.SyncRoot
			{
				get
				{
					return ((ICollection)this._dict).SyncRoot;
				}
			}

			// Token: 0x06002897 RID: 10391 RVA: 0x0008BBFB File Offset: 0x00089DFB
			public void Add(TKey key)
			{
				throw new NotSupportedException("This operation is not supported on SortedList nested types because they require modifying the original SortedList.");
			}

			// Token: 0x06002898 RID: 10392 RVA: 0x0008BBFB File Offset: 0x00089DFB
			public void Clear()
			{
				throw new NotSupportedException("This operation is not supported on SortedList nested types because they require modifying the original SortedList.");
			}

			// Token: 0x06002899 RID: 10393 RVA: 0x0008BC07 File Offset: 0x00089E07
			public bool Contains(TKey key)
			{
				return this._dict.ContainsKey(key);
			}

			// Token: 0x0600289A RID: 10394 RVA: 0x0008BC15 File Offset: 0x00089E15
			public void CopyTo(TKey[] array, int arrayIndex)
			{
				Array.Copy(this._dict.keys, 0, array, arrayIndex, this._dict.Count);
			}

			// Token: 0x0600289B RID: 10395 RVA: 0x0008BC38 File Offset: 0x00089E38
			void ICollection.CopyTo(Array array, int arrayIndex)
			{
				if (array != null && array.Rank != 1)
				{
					throw new ArgumentException("Only single dimensional arrays are supported for the requested action.", "array");
				}
				try
				{
					Array.Copy(this._dict.keys, 0, array, arrayIndex, this._dict.Count);
				}
				catch (ArrayTypeMismatchException)
				{
					throw new ArgumentException("Target array type is not compatible with the type of items in the collection.", "array");
				}
			}

			// Token: 0x0600289C RID: 10396 RVA: 0x0008BBFB File Offset: 0x00089DFB
			public void Insert(int index, TKey value)
			{
				throw new NotSupportedException("This operation is not supported on SortedList nested types because they require modifying the original SortedList.");
			}

			// Token: 0x17000865 RID: 2149
			public TKey this[int index]
			{
				get
				{
					return this._dict.GetKey(index);
				}
				set
				{
					throw new NotSupportedException("Mutating a key collection derived from a dictionary is not allowed.");
				}
			}

			// Token: 0x0600289F RID: 10399 RVA: 0x0008BCB2 File Offset: 0x00089EB2
			public IEnumerator<TKey> GetEnumerator()
			{
				return new SortedList<TKey, TValue>.SortedListKeyEnumerator(this._dict);
			}

			// Token: 0x060028A0 RID: 10400 RVA: 0x0008BCB2 File Offset: 0x00089EB2
			IEnumerator IEnumerable.GetEnumerator()
			{
				return new SortedList<TKey, TValue>.SortedListKeyEnumerator(this._dict);
			}

			// Token: 0x060028A1 RID: 10401 RVA: 0x0008BCC0 File Offset: 0x00089EC0
			public int IndexOf(TKey key)
			{
				if (key == null)
				{
					throw new ArgumentNullException("key");
				}
				int num = Array.BinarySearch<TKey>(this._dict.keys, 0, this._dict.Count, key, this._dict.comparer);
				if (num >= 0)
				{
					return num;
				}
				return -1;
			}

			// Token: 0x060028A2 RID: 10402 RVA: 0x0008BBFB File Offset: 0x00089DFB
			public bool Remove(TKey key)
			{
				throw new NotSupportedException("This operation is not supported on SortedList nested types because they require modifying the original SortedList.");
			}

			// Token: 0x060028A3 RID: 10403 RVA: 0x0008BBFB File Offset: 0x00089DFB
			public void RemoveAt(int index)
			{
				throw new NotSupportedException("This operation is not supported on SortedList nested types because they require modifying the original SortedList.");
			}

			// Token: 0x040015A3 RID: 5539
			private SortedList<TKey, TValue> _dict;
		}

		// Token: 0x020004E0 RID: 1248
		[DebuggerDisplay("Count = {Count}")]
		[DebuggerTypeProxy(typeof(DictionaryValueCollectionDebugView<, >))]
		[Serializable]
		private sealed class ValueList : IList<TValue>, ICollection<!1>, IEnumerable<!1>, IEnumerable, ICollection
		{
			// Token: 0x060028A4 RID: 10404 RVA: 0x0008BD10 File Offset: 0x00089F10
			internal ValueList(SortedList<TKey, TValue> dictionary)
			{
				this._dict = dictionary;
			}

			// Token: 0x17000866 RID: 2150
			// (get) Token: 0x060028A5 RID: 10405 RVA: 0x0008BD1F File Offset: 0x00089F1F
			public int Count
			{
				get
				{
					return this._dict._size;
				}
			}

			// Token: 0x17000867 RID: 2151
			// (get) Token: 0x060028A6 RID: 10406 RVA: 0x0000390E File Offset: 0x00001B0E
			public bool IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x17000868 RID: 2152
			// (get) Token: 0x060028A7 RID: 10407 RVA: 0x00003062 File Offset: 0x00001262
			bool ICollection.IsSynchronized
			{
				get
				{
					return false;
				}
			}

			// Token: 0x17000869 RID: 2153
			// (get) Token: 0x060028A8 RID: 10408 RVA: 0x0008BD2C File Offset: 0x00089F2C
			object ICollection.SyncRoot
			{
				get
				{
					return ((ICollection)this._dict).SyncRoot;
				}
			}

			// Token: 0x060028A9 RID: 10409 RVA: 0x0008BBFB File Offset: 0x00089DFB
			public void Add(TValue key)
			{
				throw new NotSupportedException("This operation is not supported on SortedList nested types because they require modifying the original SortedList.");
			}

			// Token: 0x060028AA RID: 10410 RVA: 0x0008BBFB File Offset: 0x00089DFB
			public void Clear()
			{
				throw new NotSupportedException("This operation is not supported on SortedList nested types because they require modifying the original SortedList.");
			}

			// Token: 0x060028AB RID: 10411 RVA: 0x0008BD39 File Offset: 0x00089F39
			public bool Contains(TValue value)
			{
				return this._dict.ContainsValue(value);
			}

			// Token: 0x060028AC RID: 10412 RVA: 0x0008BD47 File Offset: 0x00089F47
			public void CopyTo(TValue[] array, int arrayIndex)
			{
				Array.Copy(this._dict.values, 0, array, arrayIndex, this._dict.Count);
			}

			// Token: 0x060028AD RID: 10413 RVA: 0x0008BD68 File Offset: 0x00089F68
			void ICollection.CopyTo(Array array, int index)
			{
				if (array != null && array.Rank != 1)
				{
					throw new ArgumentException("Only single dimensional arrays are supported for the requested action.", "array");
				}
				try
				{
					Array.Copy(this._dict.values, 0, array, index, this._dict.Count);
				}
				catch (ArrayTypeMismatchException)
				{
					throw new ArgumentException("Target array type is not compatible with the type of items in the collection.", "array");
				}
			}

			// Token: 0x060028AE RID: 10414 RVA: 0x0008BBFB File Offset: 0x00089DFB
			public void Insert(int index, TValue value)
			{
				throw new NotSupportedException("This operation is not supported on SortedList nested types because they require modifying the original SortedList.");
			}

			// Token: 0x1700086A RID: 2154
			public TValue this[int index]
			{
				get
				{
					return this._dict.GetByIndex(index);
				}
				set
				{
					throw new NotSupportedException("This operation is not supported on SortedList nested types because they require modifying the original SortedList.");
				}
			}

			// Token: 0x060028B1 RID: 10417 RVA: 0x0008BDE2 File Offset: 0x00089FE2
			public IEnumerator<TValue> GetEnumerator()
			{
				return new SortedList<TKey, TValue>.SortedListValueEnumerator(this._dict);
			}

			// Token: 0x060028B2 RID: 10418 RVA: 0x0008BDE2 File Offset: 0x00089FE2
			IEnumerator IEnumerable.GetEnumerator()
			{
				return new SortedList<TKey, TValue>.SortedListValueEnumerator(this._dict);
			}

			// Token: 0x060028B3 RID: 10419 RVA: 0x0008BDEF File Offset: 0x00089FEF
			public int IndexOf(TValue value)
			{
				return Array.IndexOf<TValue>(this._dict.values, value, 0, this._dict.Count);
			}

			// Token: 0x060028B4 RID: 10420 RVA: 0x0008BBFB File Offset: 0x00089DFB
			public bool Remove(TValue value)
			{
				throw new NotSupportedException("This operation is not supported on SortedList nested types because they require modifying the original SortedList.");
			}

			// Token: 0x060028B5 RID: 10421 RVA: 0x0008BBFB File Offset: 0x00089DFB
			public void RemoveAt(int index)
			{
				throw new NotSupportedException("This operation is not supported on SortedList nested types because they require modifying the original SortedList.");
			}

			// Token: 0x040015A4 RID: 5540
			private SortedList<TKey, TValue> _dict;
		}
	}
}

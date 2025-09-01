using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using Unity;

namespace System.Collections.ObjectModel
{
	/// <summary>Represents a read-only, generic collection of key/value pairs.</summary>
	/// <typeparam name="TKey">The type of keys in the dictionary.</typeparam>
	/// <typeparam name="TValue">The type of values in the dictionary.</typeparam>
	// Token: 0x02000A83 RID: 2691
	[DebuggerDisplay("Count = {Count}")]
	[DebuggerTypeProxy(typeof(DictionaryDebugView<, >))]
	[Serializable]
	public class ReadOnlyDictionary<TKey, TValue> : IDictionary<!0, !1>, ICollection<KeyValuePair<!0, !1>>, IEnumerable<KeyValuePair<!0, !1>>, IEnumerable, IDictionary, ICollection, IReadOnlyDictionary<!0, !1>, IReadOnlyCollection<KeyValuePair<TKey, TValue>>
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.ObjectModel.ReadOnlyDictionary`2" /> class that is a wrapper around the specified dictionary.</summary>
		/// <param name="dictionary">The dictionary to wrap.</param>
		// Token: 0x06006054 RID: 24660 RVA: 0x00142E1E File Offset: 0x0014101E
		public ReadOnlyDictionary(IDictionary<TKey, TValue> dictionary)
		{
			if (dictionary == null)
			{
				throw new ArgumentNullException("dictionary");
			}
			this.m_dictionary = dictionary;
		}

		/// <summary>Gets the dictionary that is wrapped by this <see cref="T:System.Collections.ObjectModel.ReadOnlyDictionary`2" /> object.</summary>
		/// <returns>The dictionary that is wrapped by this object.</returns>
		// Token: 0x170010FB RID: 4347
		// (get) Token: 0x06006055 RID: 24661 RVA: 0x00142E3B File Offset: 0x0014103B
		protected IDictionary<TKey, TValue> Dictionary
		{
			get
			{
				return this.m_dictionary;
			}
		}

		/// <summary>Gets a key collection that contains the keys of the dictionary.</summary>
		/// <returns>A key collection that contains the keys of the dictionary.</returns>
		// Token: 0x170010FC RID: 4348
		// (get) Token: 0x06006056 RID: 24662 RVA: 0x00142E43 File Offset: 0x00141043
		public ReadOnlyDictionary<TKey, TValue>.KeyCollection Keys
		{
			get
			{
				if (this._keys == null)
				{
					this._keys = new ReadOnlyDictionary<TKey, TValue>.KeyCollection(this.m_dictionary.Keys);
				}
				return this._keys;
			}
		}

		/// <summary>Gets a collection that contains the values in the dictionary.</summary>
		/// <returns>A collection that contains the values in the object that implements <see cref="T:System.Collections.ObjectModel.ReadOnlyDictionary`2" />.</returns>
		// Token: 0x170010FD RID: 4349
		// (get) Token: 0x06006057 RID: 24663 RVA: 0x00142E69 File Offset: 0x00141069
		public ReadOnlyDictionary<TKey, TValue>.ValueCollection Values
		{
			get
			{
				if (this._values == null)
				{
					this._values = new ReadOnlyDictionary<TKey, TValue>.ValueCollection(this.m_dictionary.Values);
				}
				return this._values;
			}
		}

		/// <summary>Determines whether the dictionary contains an element that has the specified key.</summary>
		/// <param name="key">The key to locate in the dictionary.</param>
		/// <returns>
		///   <see langword="true" /> if the dictionary contains an element that has the specified key; otherwise, <see langword="false" />.</returns>
		// Token: 0x06006058 RID: 24664 RVA: 0x00142E8F File Offset: 0x0014108F
		public bool ContainsKey(TKey key)
		{
			return this.m_dictionary.ContainsKey(key);
		}

		// Token: 0x170010FE RID: 4350
		// (get) Token: 0x06006059 RID: 24665 RVA: 0x00142E9D File Offset: 0x0014109D
		ICollection<TKey> IDictionary<!0, !1>.Keys
		{
			get
			{
				return this.Keys;
			}
		}

		/// <summary>Retrieves the value that is associated with the specified key.</summary>
		/// <param name="key">The key whose value will be retrieved.</param>
		/// <param name="value">When this method returns, the value associated with the specified key, if the key is found; otherwise, the default value for the type of the <paramref name="value" /> parameter. This parameter is passed uninitialized.</param>
		/// <returns>
		///   <see langword="true" /> if the object that implements <see cref="T:System.Collections.ObjectModel.ReadOnlyDictionary`2" /> contains an element with the specified key; otherwise, <see langword="false" />.</returns>
		// Token: 0x0600605A RID: 24666 RVA: 0x00142EA5 File Offset: 0x001410A5
		public bool TryGetValue(TKey key, out TValue value)
		{
			return this.m_dictionary.TryGetValue(key, out value);
		}

		// Token: 0x170010FF RID: 4351
		// (get) Token: 0x0600605B RID: 24667 RVA: 0x00142EB4 File Offset: 0x001410B4
		ICollection<TValue> IDictionary<!0, !1>.Values
		{
			get
			{
				return this.Values;
			}
		}

		/// <summary>Gets the element that has the specified key.</summary>
		/// <param name="key">The key of the element to get.</param>
		/// <returns>The element that has the specified key.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.Collections.Generic.KeyNotFoundException">The property is retrieved and <paramref name="key" /> is not found.</exception>
		// Token: 0x17001100 RID: 4352
		public TValue this[TKey key]
		{
			get
			{
				return this.m_dictionary[key];
			}
		}

		// Token: 0x0600605D RID: 24669 RVA: 0x0013B58D File Offset: 0x0013978D
		void IDictionary<!0, !1>.Add(TKey key, TValue value)
		{
			throw new NotSupportedException("Collection is read-only.");
		}

		// Token: 0x0600605E RID: 24670 RVA: 0x0013B58D File Offset: 0x0013978D
		bool IDictionary<!0, !1>.Remove(TKey key)
		{
			throw new NotSupportedException("Collection is read-only.");
		}

		// Token: 0x17001101 RID: 4353
		TValue IDictionary<!0, !1>.this[TKey key]
		{
			get
			{
				return this.m_dictionary[key];
			}
			set
			{
				throw new NotSupportedException("Collection is read-only.");
			}
		}

		/// <summary>Gets the number of items in the dictionary.</summary>
		/// <returns>The number of items in the dictionary.</returns>
		// Token: 0x17001102 RID: 4354
		// (get) Token: 0x06006061 RID: 24673 RVA: 0x00142ECA File Offset: 0x001410CA
		public int Count
		{
			get
			{
				return this.m_dictionary.Count;
			}
		}

		// Token: 0x06006062 RID: 24674 RVA: 0x00142ED7 File Offset: 0x001410D7
		bool ICollection<KeyValuePair<!0, !1>>.Contains(KeyValuePair<TKey, TValue> item)
		{
			return this.m_dictionary.Contains(item);
		}

		// Token: 0x06006063 RID: 24675 RVA: 0x00142EE5 File Offset: 0x001410E5
		void ICollection<KeyValuePair<!0, !1>>.CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
		{
			this.m_dictionary.CopyTo(array, arrayIndex);
		}

		// Token: 0x17001103 RID: 4355
		// (get) Token: 0x06006064 RID: 24676 RVA: 0x000040F7 File Offset: 0x000022F7
		bool ICollection<KeyValuePair<!0, !1>>.IsReadOnly
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06006065 RID: 24677 RVA: 0x0013B58D File Offset: 0x0013978D
		void ICollection<KeyValuePair<!0, !1>>.Add(KeyValuePair<TKey, TValue> item)
		{
			throw new NotSupportedException("Collection is read-only.");
		}

		// Token: 0x06006066 RID: 24678 RVA: 0x0013B58D File Offset: 0x0013978D
		void ICollection<KeyValuePair<!0, !1>>.Clear()
		{
			throw new NotSupportedException("Collection is read-only.");
		}

		// Token: 0x06006067 RID: 24679 RVA: 0x0013B58D File Offset: 0x0013978D
		bool ICollection<KeyValuePair<!0, !1>>.Remove(KeyValuePair<TKey, TValue> item)
		{
			throw new NotSupportedException("Collection is read-only.");
		}

		/// <summary>Returns an enumerator that iterates through the <see cref="T:System.Collections.ObjectModel.ReadOnlyDictionary`2" />.</summary>
		/// <returns>An enumerator that can be used to iterate through the collection.</returns>
		// Token: 0x06006068 RID: 24680 RVA: 0x00142EF4 File Offset: 0x001410F4
		public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
		{
			return this.m_dictionary.GetEnumerator();
		}

		/// <summary>Returns an enumerator that iterates through a collection.</summary>
		/// <returns>An enumerator that can be used to iterate through the collection.</returns>
		// Token: 0x06006069 RID: 24681 RVA: 0x00142F01 File Offset: 0x00141101
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.m_dictionary.GetEnumerator();
		}

		// Token: 0x0600606A RID: 24682 RVA: 0x00142F0E File Offset: 0x0014110E
		private static bool IsCompatibleKey(object key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			return key is TKey;
		}

		/// <summary>Throws a <see cref="T:System.NotSupportedException" /> exception in all cases.</summary>
		/// <param name="key">The key of the element to add.</param>
		/// <param name="value">The value of the element to add.</param>
		/// <exception cref="T:System.NotSupportedException">In all cases.</exception>
		// Token: 0x0600606B RID: 24683 RVA: 0x0013B58D File Offset: 0x0013978D
		void IDictionary.Add(object key, object value)
		{
			throw new NotSupportedException("Collection is read-only.");
		}

		/// <summary>Throws a <see cref="T:System.NotSupportedException" /> exception in all cases.</summary>
		/// <exception cref="T:System.NotSupportedException">In all cases.</exception>
		// Token: 0x0600606C RID: 24684 RVA: 0x0013B58D File Offset: 0x0013978D
		void IDictionary.Clear()
		{
			throw new NotSupportedException("Collection is read-only.");
		}

		/// <summary>Determines whether the dictionary contains an element that has the specified key.</summary>
		/// <param name="key">The key to locate in the dictionary.</param>
		/// <returns>
		///   <see langword="true" /> if the dictionary contains an element that has the specified key; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is <see langword="null" />.</exception>
		// Token: 0x0600606D RID: 24685 RVA: 0x00142F27 File Offset: 0x00141127
		bool IDictionary.Contains(object key)
		{
			return ReadOnlyDictionary<TKey, TValue>.IsCompatibleKey(key) && this.ContainsKey((TKey)((object)key));
		}

		/// <summary>Returns an enumerator for the dictionary.</summary>
		/// <returns>An enumerator for the dictionary.</returns>
		// Token: 0x0600606E RID: 24686 RVA: 0x00142F40 File Offset: 0x00141140
		IDictionaryEnumerator IDictionary.GetEnumerator()
		{
			IDictionary dictionary = this.m_dictionary as IDictionary;
			if (dictionary != null)
			{
				return dictionary.GetEnumerator();
			}
			return new ReadOnlyDictionary<TKey, TValue>.DictionaryEnumerator(this.m_dictionary);
		}

		/// <summary>Gets a value that indicates whether the dictionary has a fixed size.</summary>
		/// <returns>
		///   <see langword="true" /> if the dictionary has a fixed size; otherwise, <see langword="false" />.</returns>
		// Token: 0x17001104 RID: 4356
		// (get) Token: 0x0600606F RID: 24687 RVA: 0x000040F7 File Offset: 0x000022F7
		bool IDictionary.IsFixedSize
		{
			get
			{
				return true;
			}
		}

		/// <summary>Gets a value that indicates whether the dictionary is read-only.</summary>
		/// <returns>
		///   <see langword="true" /> in all cases.</returns>
		// Token: 0x17001105 RID: 4357
		// (get) Token: 0x06006070 RID: 24688 RVA: 0x000040F7 File Offset: 0x000022F7
		bool IDictionary.IsReadOnly
		{
			get
			{
				return true;
			}
		}

		/// <summary>Gets a collection that contains the keys of the dictionary.</summary>
		/// <returns>A collection that contains the keys of the dictionary.</returns>
		// Token: 0x17001106 RID: 4358
		// (get) Token: 0x06006071 RID: 24689 RVA: 0x00142E9D File Offset: 0x0014109D
		ICollection IDictionary.Keys
		{
			get
			{
				return this.Keys;
			}
		}

		/// <summary>Throws a <see cref="T:System.NotSupportedException" /> exception in all cases.</summary>
		/// <param name="key">The key of the element to remove.</param>
		/// <exception cref="T:System.NotSupportedException">In all cases.</exception>
		// Token: 0x06006072 RID: 24690 RVA: 0x0013B58D File Offset: 0x0013978D
		void IDictionary.Remove(object key)
		{
			throw new NotSupportedException("Collection is read-only.");
		}

		/// <summary>Gets a collection that contains the values in the dictionary.</summary>
		/// <returns>A collection that contains the values in the dictionary.</returns>
		// Token: 0x17001107 RID: 4359
		// (get) Token: 0x06006073 RID: 24691 RVA: 0x00142EB4 File Offset: 0x001410B4
		ICollection IDictionary.Values
		{
			get
			{
				return this.Values;
			}
		}

		/// <summary>Gets the element that has the specified key.</summary>
		/// <param name="key">The key of the element to get or set.</param>
		/// <returns>The element that has the specified key.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.NotSupportedException">The property is set.  
		///  -or-  
		///  The property is set, <paramref name="key" /> does not exist in the collection, and the dictionary has a fixed size.</exception>
		// Token: 0x17001108 RID: 4360
		object IDictionary.this[object key]
		{
			get
			{
				if (ReadOnlyDictionary<TKey, TValue>.IsCompatibleKey(key))
				{
					return this[(TKey)((object)key)];
				}
				return null;
			}
			set
			{
				throw new NotSupportedException("Collection is read-only.");
			}
		}

		/// <summary>Copies the elements of the dictionary to an array, starting at the specified array index.</summary>
		/// <param name="array">The one-dimensional array that is the destination of the elements copied from the dictionary. The array must have zero-based indexing.</param>
		/// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="array" /> is multidimensional.  
		/// -or-  
		/// The number of elements in the source dictionary is greater than the available space from <paramref name="index" /> to the end of the destination <paramref name="array" />.  
		/// -or-  
		/// The type of the source dictionary cannot be cast automatically to the type of the destination <paramref name="array" />.</exception>
		// Token: 0x06006076 RID: 24694 RVA: 0x00142F90 File Offset: 0x00141190
		void ICollection.CopyTo(Array array, int index)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (array.Rank != 1)
			{
				throw new ArgumentException("Only single dimensional arrays are supported for the requested action.");
			}
			if (array.GetLowerBound(0) != 0)
			{
				throw new ArgumentException("The lower bound of target array must be zero.");
			}
			if (index < 0 || index > array.Length)
			{
				throw new ArgumentOutOfRangeException("index", "Non-negative number required.");
			}
			if (array.Length - index < this.Count)
			{
				throw new ArgumentException("Destination array is not long enough to copy all the items in the collection. Check array index and length.");
			}
			KeyValuePair<TKey, TValue>[] array2 = array as KeyValuePair<TKey, TValue>[];
			if (array2 != null)
			{
				this.m_dictionary.CopyTo(array2, index);
				return;
			}
			DictionaryEntry[] array3 = array as DictionaryEntry[];
			if (array3 != null)
			{
				using (IEnumerator<KeyValuePair<TKey, TValue>> enumerator = this.m_dictionary.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						KeyValuePair<TKey, TValue> keyValuePair = enumerator.Current;
						array3[index++] = new DictionaryEntry(keyValuePair.Key, keyValuePair.Value);
					}
					return;
				}
			}
			object[] array4 = array as object[];
			if (array4 == null)
			{
				throw new ArgumentException("Target array type is not compatible with the type of items in the collection.");
			}
			try
			{
				foreach (KeyValuePair<TKey, TValue> keyValuePair2 in this.m_dictionary)
				{
					array4[index++] = new KeyValuePair<TKey, TValue>(keyValuePair2.Key, keyValuePair2.Value);
				}
			}
			catch (ArrayTypeMismatchException)
			{
				throw new ArgumentException("Target array type is not compatible with the type of items in the collection.");
			}
		}

		/// <summary>Gets a value that indicates whether access to the dictionary is synchronized (thread safe).</summary>
		/// <returns>
		///   <see langword="true" /> if access to the dictionary is synchronized (thread safe); otherwise, <see langword="false" />.</returns>
		// Token: 0x17001109 RID: 4361
		// (get) Token: 0x06006077 RID: 24695 RVA: 0x0000DDA9 File Offset: 0x0000BFA9
		bool ICollection.IsSynchronized
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets an object that can be used to synchronize access to the dictionary.</summary>
		/// <returns>An object that can be used to synchronize access to the dictionary.</returns>
		// Token: 0x1700110A RID: 4362
		// (get) Token: 0x06006078 RID: 24696 RVA: 0x00143118 File Offset: 0x00141318
		object ICollection.SyncRoot
		{
			get
			{
				if (this._syncRoot == null)
				{
					ICollection collection = this.m_dictionary as ICollection;
					if (collection != null)
					{
						this._syncRoot = collection.SyncRoot;
					}
					else
					{
						Interlocked.CompareExchange<object>(ref this._syncRoot, new object(), null);
					}
				}
				return this._syncRoot;
			}
		}

		// Token: 0x1700110B RID: 4363
		// (get) Token: 0x06006079 RID: 24697 RVA: 0x00142E9D File Offset: 0x0014109D
		IEnumerable<TKey> IReadOnlyDictionary<!0, !1>.Keys
		{
			get
			{
				return this.Keys;
			}
		}

		// Token: 0x1700110C RID: 4364
		// (get) Token: 0x0600607A RID: 24698 RVA: 0x00142EB4 File Offset: 0x001410B4
		IEnumerable<TValue> IReadOnlyDictionary<!0, !1>.Values
		{
			get
			{
				return this.Values;
			}
		}

		// Token: 0x040039B1 RID: 14769
		private readonly IDictionary<TKey, TValue> m_dictionary;

		// Token: 0x040039B2 RID: 14770
		[NonSerialized]
		private object _syncRoot;

		// Token: 0x040039B3 RID: 14771
		[NonSerialized]
		private ReadOnlyDictionary<TKey, TValue>.KeyCollection _keys;

		// Token: 0x040039B4 RID: 14772
		[NonSerialized]
		private ReadOnlyDictionary<TKey, TValue>.ValueCollection _values;

		// Token: 0x02000A84 RID: 2692
		[Serializable]
		private struct DictionaryEnumerator : IDictionaryEnumerator, IEnumerator
		{
			// Token: 0x0600607B RID: 24699 RVA: 0x00143162 File Offset: 0x00141362
			public DictionaryEnumerator(IDictionary<TKey, TValue> dictionary)
			{
				this._dictionary = dictionary;
				this._enumerator = this._dictionary.GetEnumerator();
			}

			// Token: 0x1700110D RID: 4365
			// (get) Token: 0x0600607C RID: 24700 RVA: 0x0014317C File Offset: 0x0014137C
			public DictionaryEntry Entry
			{
				get
				{
					KeyValuePair<TKey, TValue> keyValuePair = this._enumerator.Current;
					object key = keyValuePair.Key;
					keyValuePair = this._enumerator.Current;
					return new DictionaryEntry(key, keyValuePair.Value);
				}
			}

			// Token: 0x1700110E RID: 4366
			// (get) Token: 0x0600607D RID: 24701 RVA: 0x001431C0 File Offset: 0x001413C0
			public object Key
			{
				get
				{
					KeyValuePair<TKey, TValue> keyValuePair = this._enumerator.Current;
					return keyValuePair.Key;
				}
			}

			// Token: 0x1700110F RID: 4367
			// (get) Token: 0x0600607E RID: 24702 RVA: 0x001431E8 File Offset: 0x001413E8
			public object Value
			{
				get
				{
					KeyValuePair<TKey, TValue> keyValuePair = this._enumerator.Current;
					return keyValuePair.Value;
				}
			}

			// Token: 0x17001110 RID: 4368
			// (get) Token: 0x0600607F RID: 24703 RVA: 0x0014320D File Offset: 0x0014140D
			public object Current
			{
				get
				{
					return this.Entry;
				}
			}

			// Token: 0x06006080 RID: 24704 RVA: 0x0014321A File Offset: 0x0014141A
			public bool MoveNext()
			{
				return this._enumerator.MoveNext();
			}

			// Token: 0x06006081 RID: 24705 RVA: 0x00143227 File Offset: 0x00141427
			public void Reset()
			{
				this._enumerator.Reset();
			}

			// Token: 0x040039B5 RID: 14773
			private readonly IDictionary<TKey, TValue> _dictionary;

			// Token: 0x040039B6 RID: 14774
			private IEnumerator<KeyValuePair<TKey, TValue>> _enumerator;
		}

		/// <summary>Represents a read-only collection of the keys of a <see cref="T:System.Collections.ObjectModel.ReadOnlyDictionary`2" /> object.</summary>
		/// <typeparam name="TKey" />
		/// <typeparam name="TValue" />
		// Token: 0x02000A85 RID: 2693
		[DebuggerDisplay("Count = {Count}")]
		[DebuggerTypeProxy(typeof(CollectionDebugView<>))]
		[Serializable]
		public sealed class KeyCollection : ICollection<!0>, IEnumerable<!0>, IEnumerable, ICollection, IReadOnlyCollection<TKey>
		{
			// Token: 0x06006082 RID: 24706 RVA: 0x00143234 File Offset: 0x00141434
			internal KeyCollection(ICollection<TKey> collection)
			{
				if (collection == null)
				{
					throw new ArgumentNullException("collection");
				}
				this._collection = collection;
			}

			// Token: 0x06006083 RID: 24707 RVA: 0x0013B58D File Offset: 0x0013978D
			void ICollection<!0>.Add(TKey item)
			{
				throw new NotSupportedException("Collection is read-only.");
			}

			// Token: 0x06006084 RID: 24708 RVA: 0x0013B58D File Offset: 0x0013978D
			void ICollection<!0>.Clear()
			{
				throw new NotSupportedException("Collection is read-only.");
			}

			// Token: 0x06006085 RID: 24709 RVA: 0x00143251 File Offset: 0x00141451
			bool ICollection<!0>.Contains(TKey item)
			{
				return this._collection.Contains(item);
			}

			/// <summary>Copies the elements of the collection to an array, starting at a specific array index.</summary>
			/// <param name="array">The one-dimensional array that is the destination of the elements copied from the collection. The array must have zero-based indexing.</param>
			/// <param name="arrayIndex">The zero-based index in <paramref name="array" /> at which copying begins.</param>
			/// <exception cref="T:System.ArgumentNullException">
			///   <paramref name="array" /> is <see langword="null" />.</exception>
			/// <exception cref="T:System.ArgumentOutOfRangeException">
			///   <paramref name="arrayIndex" /> is less than 0.</exception>
			/// <exception cref="T:System.ArgumentException">
			///   <paramref name="array" /> is multidimensional.  
			/// -or-  
			/// The number of elements in the source collection is greater than the available space from <paramref name="arrayIndex" /> to the end of the destination <paramref name="array" />.  
			/// -or-  
			/// Type <paramref name="T" /> cannot be cast automatically to the type of the destination <paramref name="array" />.</exception>
			// Token: 0x06006086 RID: 24710 RVA: 0x0014325F File Offset: 0x0014145F
			public void CopyTo(TKey[] array, int arrayIndex)
			{
				this._collection.CopyTo(array, arrayIndex);
			}

			/// <summary>Gets the number of elements in the collection.</summary>
			/// <returns>The number of elements in the collection.</returns>
			// Token: 0x17001111 RID: 4369
			// (get) Token: 0x06006087 RID: 24711 RVA: 0x0014326E File Offset: 0x0014146E
			public int Count
			{
				get
				{
					return this._collection.Count;
				}
			}

			// Token: 0x17001112 RID: 4370
			// (get) Token: 0x06006088 RID: 24712 RVA: 0x000040F7 File Offset: 0x000022F7
			bool ICollection<!0>.IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x06006089 RID: 24713 RVA: 0x0013B58D File Offset: 0x0013978D
			bool ICollection<!0>.Remove(TKey item)
			{
				throw new NotSupportedException("Collection is read-only.");
			}

			/// <summary>Returns an enumerator that iterates through the collection.</summary>
			/// <returns>An enumerator that can be used to iterate through the collection.</returns>
			// Token: 0x0600608A RID: 24714 RVA: 0x0014327B File Offset: 0x0014147B
			public IEnumerator<TKey> GetEnumerator()
			{
				return this._collection.GetEnumerator();
			}

			/// <summary>Returns an enumerator that iterates through the collection.</summary>
			/// <returns>An enumerator that can be used to iterate through the collection.</returns>
			// Token: 0x0600608B RID: 24715 RVA: 0x00143288 File Offset: 0x00141488
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this._collection.GetEnumerator();
			}

			/// <summary>Copies the elements of the collection to an array, starting at a specific array index.</summary>
			/// <param name="array">The one-dimensional array that is the destination of the elements copied from the collection. The array must have zero-based indexing.</param>
			/// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins.</param>
			/// <exception cref="T:System.ArgumentNullException">
			///   <paramref name="array" /> is <see langword="null" />.</exception>
			/// <exception cref="T:System.ArgumentOutOfRangeException">
			///   <paramref name="index" /> is less than 0.</exception>
			/// <exception cref="T:System.ArgumentException">
			///   <paramref name="array" /> is multidimensional.  
			/// -or-  
			/// The number of elements in the source collection is greater than the available space from <paramref name="index" /> to the end of the destination <paramref name="array" />.</exception>
			// Token: 0x0600608C RID: 24716 RVA: 0x00143295 File Offset: 0x00141495
			void ICollection.CopyTo(Array array, int index)
			{
				ReadOnlyDictionaryHelpers.CopyToNonGenericICollectionHelper<TKey>(this._collection, array, index);
			}

			/// <summary>Gets a value that indicates whether access to the collection is synchronized (thread safe).</summary>
			/// <returns>
			///   <see langword="true" /> if access to the collection is synchronized (thread safe); otherwise, <see langword="false" />.</returns>
			// Token: 0x17001113 RID: 4371
			// (get) Token: 0x0600608D RID: 24717 RVA: 0x0000DDA9 File Offset: 0x0000BFA9
			bool ICollection.IsSynchronized
			{
				get
				{
					return false;
				}
			}

			/// <summary>Gets an object that can be used to synchronize access to the collection.</summary>
			/// <returns>An object that can be used to synchronize access to the collection.</returns>
			// Token: 0x17001114 RID: 4372
			// (get) Token: 0x0600608E RID: 24718 RVA: 0x001432A4 File Offset: 0x001414A4
			object ICollection.SyncRoot
			{
				get
				{
					if (this._syncRoot == null)
					{
						ICollection collection = this._collection as ICollection;
						if (collection != null)
						{
							this._syncRoot = collection.SyncRoot;
						}
						else
						{
							Interlocked.CompareExchange<object>(ref this._syncRoot, new object(), null);
						}
					}
					return this._syncRoot;
				}
			}

			// Token: 0x0600608F RID: 24719 RVA: 0x000173AD File Offset: 0x000155AD
			internal KeyCollection()
			{
				ThrowStub.ThrowNotSupportedException();
			}

			// Token: 0x040039B7 RID: 14775
			private readonly ICollection<TKey> _collection;

			// Token: 0x040039B8 RID: 14776
			[NonSerialized]
			private object _syncRoot;
		}

		/// <summary>Represents a read-only collection of the values of a <see cref="T:System.Collections.ObjectModel.ReadOnlyDictionary`2" /> object.</summary>
		/// <typeparam name="TKey" />
		/// <typeparam name="TValue" />
		// Token: 0x02000A86 RID: 2694
		[DebuggerTypeProxy(typeof(CollectionDebugView<>))]
		[DebuggerDisplay("Count = {Count}")]
		[Serializable]
		public sealed class ValueCollection : ICollection<TValue>, IEnumerable<TValue>, IEnumerable, ICollection, IReadOnlyCollection<TValue>
		{
			// Token: 0x06006090 RID: 24720 RVA: 0x001432EE File Offset: 0x001414EE
			internal ValueCollection(ICollection<TValue> collection)
			{
				if (collection == null)
				{
					throw new ArgumentNullException("collection");
				}
				this._collection = collection;
			}

			// Token: 0x06006091 RID: 24721 RVA: 0x0013B58D File Offset: 0x0013978D
			void ICollection<!1>.Add(TValue item)
			{
				throw new NotSupportedException("Collection is read-only.");
			}

			// Token: 0x06006092 RID: 24722 RVA: 0x0013B58D File Offset: 0x0013978D
			void ICollection<!1>.Clear()
			{
				throw new NotSupportedException("Collection is read-only.");
			}

			// Token: 0x06006093 RID: 24723 RVA: 0x0014330B File Offset: 0x0014150B
			bool ICollection<!1>.Contains(TValue item)
			{
				return this._collection.Contains(item);
			}

			/// <summary>Copies the elements of the collection to an array, starting at a specific array index.</summary>
			/// <param name="array">The one-dimensional array that is the destination of the elements copied from the collection. The array must have zero-based indexing.</param>
			/// <param name="arrayIndex">The zero-based index in <paramref name="array" /> at which copying begins.</param>
			/// <exception cref="T:System.ArgumentNullException">
			///   <paramref name="array" /> is <see langword="null" />.</exception>
			/// <exception cref="T:System.ArgumentOutOfRangeException">
			///   <paramref name="arrayIndex" /> is less than 0.</exception>
			/// <exception cref="T:System.ArgumentException">
			///   <paramref name="array" /> is multidimensional.  
			/// -or-  
			/// The number of elements in the source collection is greater than the available space from <paramref name="arrayIndex" /> to the end of the destination <paramref name="array" />.  
			/// -or-  
			/// Type <paramref name="T" /> cannot be cast automatically to the type of the destination <paramref name="array" />.</exception>
			// Token: 0x06006094 RID: 24724 RVA: 0x00143319 File Offset: 0x00141519
			public void CopyTo(TValue[] array, int arrayIndex)
			{
				this._collection.CopyTo(array, arrayIndex);
			}

			/// <summary>Gets the number of elements in the collection.</summary>
			/// <returns>The number of elements in the collection.</returns>
			// Token: 0x17001115 RID: 4373
			// (get) Token: 0x06006095 RID: 24725 RVA: 0x00143328 File Offset: 0x00141528
			public int Count
			{
				get
				{
					return this._collection.Count;
				}
			}

			// Token: 0x17001116 RID: 4374
			// (get) Token: 0x06006096 RID: 24726 RVA: 0x000040F7 File Offset: 0x000022F7
			bool ICollection<!1>.IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x06006097 RID: 24727 RVA: 0x0013B58D File Offset: 0x0013978D
			bool ICollection<!1>.Remove(TValue item)
			{
				throw new NotSupportedException("Collection is read-only.");
			}

			/// <summary>Returns an enumerator that iterates through the collection.</summary>
			/// <returns>An enumerator that can be used to iterate through the collection.</returns>
			// Token: 0x06006098 RID: 24728 RVA: 0x00143335 File Offset: 0x00141535
			public IEnumerator<TValue> GetEnumerator()
			{
				return this._collection.GetEnumerator();
			}

			/// <summary>Returns an enumerator that iterates through the collection.</summary>
			/// <returns>An enumerator that can be used to iterate through the collection.</returns>
			// Token: 0x06006099 RID: 24729 RVA: 0x00143342 File Offset: 0x00141542
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this._collection.GetEnumerator();
			}

			/// <summary>Copies the elements of the collection to an array, starting at a specific array index.</summary>
			/// <param name="array">The one-dimensional array that is the destination of the elements copied from the collection. The array must have zero-based indexing.</param>
			/// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins.</param>
			/// <exception cref="T:System.ArgumentNullException">
			///   <paramref name="array" /> is <see langword="null" />.</exception>
			/// <exception cref="T:System.ArgumentOutOfRangeException">
			///   <paramref name="index" /> is less than 0.</exception>
			/// <exception cref="T:System.ArgumentException">
			///   <paramref name="array" /> is multidimensional.  
			/// -or-  
			/// The number of elements in the source collection is greater than the available space from <paramref name="index" /> to the end of the destination <paramref name="array" />.</exception>
			// Token: 0x0600609A RID: 24730 RVA: 0x0014334F File Offset: 0x0014154F
			void ICollection.CopyTo(Array array, int index)
			{
				ReadOnlyDictionaryHelpers.CopyToNonGenericICollectionHelper<TValue>(this._collection, array, index);
			}

			/// <summary>Gets a value that indicates whether access to the collection is synchronized (thread safe).</summary>
			/// <returns>
			///   <see langword="true" /> if access to the collection is synchronized (thread safe); otherwise, <see langword="false" />.</returns>
			// Token: 0x17001117 RID: 4375
			// (get) Token: 0x0600609B RID: 24731 RVA: 0x0000DDA9 File Offset: 0x0000BFA9
			bool ICollection.IsSynchronized
			{
				get
				{
					return false;
				}
			}

			/// <summary>Gets an object that can be used to synchronize access to the collection.</summary>
			/// <returns>An object that can be used to synchronize access to the collection.</returns>
			// Token: 0x17001118 RID: 4376
			// (get) Token: 0x0600609C RID: 24732 RVA: 0x00143360 File Offset: 0x00141560
			object ICollection.SyncRoot
			{
				get
				{
					if (this._syncRoot == null)
					{
						ICollection collection = this._collection as ICollection;
						if (collection != null)
						{
							this._syncRoot = collection.SyncRoot;
						}
						else
						{
							Interlocked.CompareExchange<object>(ref this._syncRoot, new object(), null);
						}
					}
					return this._syncRoot;
				}
			}

			// Token: 0x0600609D RID: 24733 RVA: 0x000173AD File Offset: 0x000155AD
			internal ValueCollection()
			{
				ThrowStub.ThrowNotSupportedException();
			}

			// Token: 0x040039B9 RID: 14777
			private readonly ICollection<TValue> _collection;

			// Token: 0x040039BA RID: 14778
			[NonSerialized]
			private object _syncRoot;
		}
	}
}

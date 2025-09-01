using System;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Threading;

namespace System.Collections
{
	/// <summary>Represents a collection of key/value pairs that are sorted by the keys and are accessible by key and by index.</summary>
	// Token: 0x02000A2C RID: 2604
	[DebuggerTypeProxy(typeof(SortedList.SortedListDebugView))]
	[DebuggerDisplay("Count = {Count}")]
	[Serializable]
	public class SortedList : IDictionary, ICollection, IEnumerable, ICloneable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.SortedList" /> class that is empty, has the default initial capacity, and is sorted according to the <see cref="T:System.IComparable" /> interface implemented by each key added to the <see cref="T:System.Collections.SortedList" /> object.</summary>
		// Token: 0x06005C45 RID: 23621 RVA: 0x0013666A File Offset: 0x0013486A
		public SortedList()
		{
			this.Init();
		}

		// Token: 0x06005C46 RID: 23622 RVA: 0x00136678 File Offset: 0x00134878
		private void Init()
		{
			this.keys = Array.Empty<object>();
			this.values = Array.Empty<object>();
			this._size = 0;
			this.comparer = new Comparer(CultureInfo.CurrentCulture);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.SortedList" /> class that is empty, has the specified initial capacity, and is sorted according to the <see cref="T:System.IComparable" /> interface implemented by each key added to the <see cref="T:System.Collections.SortedList" /> object.</summary>
		/// <param name="initialCapacity">The initial number of elements that the <see cref="T:System.Collections.SortedList" /> object can contain.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="initialCapacity" /> is less than zero.</exception>
		/// <exception cref="T:System.OutOfMemoryException">There is not enough available memory to create a <see cref="T:System.Collections.SortedList" /> object with the specified <paramref name="initialCapacity" />.</exception>
		// Token: 0x06005C47 RID: 23623 RVA: 0x001366A8 File Offset: 0x001348A8
		public SortedList(int initialCapacity)
		{
			if (initialCapacity < 0)
			{
				throw new ArgumentOutOfRangeException("initialCapacity", "Non-negative number required.");
			}
			this.keys = new object[initialCapacity];
			this.values = new object[initialCapacity];
			this.comparer = new Comparer(CultureInfo.CurrentCulture);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.SortedList" /> class that is empty, has the default initial capacity, and is sorted according to the specified <see cref="T:System.Collections.IComparer" /> interface.</summary>
		/// <param name="comparer">The <see cref="T:System.Collections.IComparer" /> implementation to use when comparing keys.  
		///  -or-  
		///  <see langword="null" /> to use the <see cref="T:System.IComparable" /> implementation of each key.</param>
		// Token: 0x06005C48 RID: 23624 RVA: 0x001366F7 File Offset: 0x001348F7
		public SortedList(IComparer comparer) : this()
		{
			if (comparer != null)
			{
				this.comparer = comparer;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.SortedList" /> class that is empty, has the specified initial capacity, and is sorted according to the specified <see cref="T:System.Collections.IComparer" /> interface.</summary>
		/// <param name="comparer">The <see cref="T:System.Collections.IComparer" /> implementation to use when comparing keys.  
		///  -or-  
		///  <see langword="null" /> to use the <see cref="T:System.IComparable" /> implementation of each key.</param>
		/// <param name="capacity">The initial number of elements that the <see cref="T:System.Collections.SortedList" /> object can contain.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="capacity" /> is less than zero.</exception>
		/// <exception cref="T:System.OutOfMemoryException">There is not enough available memory to create a <see cref="T:System.Collections.SortedList" /> object with the specified <paramref name="capacity" />.</exception>
		// Token: 0x06005C49 RID: 23625 RVA: 0x00136709 File Offset: 0x00134909
		public SortedList(IComparer comparer, int capacity) : this(comparer)
		{
			this.Capacity = capacity;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.SortedList" /> class that contains elements copied from the specified dictionary, has the same initial capacity as the number of elements copied, and is sorted according to the <see cref="T:System.IComparable" /> interface implemented by each key.</summary>
		/// <param name="d">The <see cref="T:System.Collections.IDictionary" /> implementation to copy to a new <see cref="T:System.Collections.SortedList" /> object.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="d" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidCastException">One or more elements in <paramref name="d" /> do not implement the <see cref="T:System.IComparable" /> interface.</exception>
		// Token: 0x06005C4A RID: 23626 RVA: 0x00136719 File Offset: 0x00134919
		public SortedList(IDictionary d) : this(d, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.SortedList" /> class that contains elements copied from the specified dictionary, has the same initial capacity as the number of elements copied, and is sorted according to the specified <see cref="T:System.Collections.IComparer" /> interface.</summary>
		/// <param name="d">The <see cref="T:System.Collections.IDictionary" /> implementation to copy to a new <see cref="T:System.Collections.SortedList" /> object.</param>
		/// <param name="comparer">The <see cref="T:System.Collections.IComparer" /> implementation to use when comparing keys.  
		///  -or-  
		///  <see langword="null" /> to use the <see cref="T:System.IComparable" /> implementation of each key.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="d" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidCastException">
		///   <paramref name="comparer" /> is <see langword="null" />, and one or more elements in <paramref name="d" /> do not implement the <see cref="T:System.IComparable" /> interface.</exception>
		// Token: 0x06005C4B RID: 23627 RVA: 0x00136724 File Offset: 0x00134924
		public SortedList(IDictionary d, IComparer comparer) : this(comparer, (d != null) ? d.Count : 0)
		{
			if (d == null)
			{
				throw new ArgumentNullException("d", "Dictionary cannot be null.");
			}
			d.Keys.CopyTo(this.keys, 0);
			d.Values.CopyTo(this.values, 0);
			Array.Sort(this.keys, comparer);
			for (int i = 0; i < this.keys.Length; i++)
			{
				this.values[i] = d[this.keys[i]];
			}
			this._size = d.Count;
		}

		/// <summary>Adds an element with the specified key and value to a <see cref="T:System.Collections.SortedList" /> object.</summary>
		/// <param name="key">The key of the element to add.</param>
		/// <param name="value">The value of the element to add. The value can be <see langword="null" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">An element with the specified <paramref name="key" /> already exists in the <see cref="T:System.Collections.SortedList" /> object.  
		///  -or-  
		///  The <see cref="T:System.Collections.SortedList" /> is set to use the <see cref="T:System.IComparable" /> interface, and <paramref name="key" /> does not implement the <see cref="T:System.IComparable" /> interface.</exception>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.SortedList" /> is read-only.  
		///  -or-  
		///  The <see cref="T:System.Collections.SortedList" /> has a fixed size.</exception>
		/// <exception cref="T:System.OutOfMemoryException">There is not enough available memory to add the element to the <see cref="T:System.Collections.SortedList" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The comparer throws an exception.</exception>
		// Token: 0x06005C4C RID: 23628 RVA: 0x001367BC File Offset: 0x001349BC
		public virtual void Add(object key, object value)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key", "Key cannot be null.");
			}
			int num = Array.BinarySearch(this.keys, 0, this._size, key, this.comparer);
			if (num >= 0)
			{
				throw new ArgumentException(SR.Format("Item has already been added. Key in dictionary: '{0}'  Key being added: '{1}'", this.GetKey(num), key));
			}
			this.Insert(~num, key, value);
		}

		/// <summary>Gets or sets the capacity of a <see cref="T:System.Collections.SortedList" /> object.</summary>
		/// <returns>The number of elements that the <see cref="T:System.Collections.SortedList" /> object can contain.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value assigned is less than the current number of elements in the <see cref="T:System.Collections.SortedList" /> object.</exception>
		/// <exception cref="T:System.OutOfMemoryException">There is not enough memory available on the system.</exception>
		// Token: 0x17001003 RID: 4099
		// (get) Token: 0x06005C4D RID: 23629 RVA: 0x0013681C File Offset: 0x00134A1C
		// (set) Token: 0x06005C4E RID: 23630 RVA: 0x00136828 File Offset: 0x00134A28
		public virtual int Capacity
		{
			get
			{
				return this.keys.Length;
			}
			set
			{
				if (value < this.Count)
				{
					throw new ArgumentOutOfRangeException("value", "capacity was less than the current size.");
				}
				if (value != this.keys.Length)
				{
					if (value > 0)
					{
						object[] destinationArray = new object[value];
						object[] destinationArray2 = new object[value];
						if (this._size > 0)
						{
							Array.Copy(this.keys, 0, destinationArray, 0, this._size);
							Array.Copy(this.values, 0, destinationArray2, 0, this._size);
						}
						this.keys = destinationArray;
						this.values = destinationArray2;
						return;
					}
					this.keys = Array.Empty<object>();
					this.values = Array.Empty<object>();
				}
			}
		}

		/// <summary>Gets the number of elements contained in a <see cref="T:System.Collections.SortedList" /> object.</summary>
		/// <returns>The number of elements contained in the <see cref="T:System.Collections.SortedList" /> object.</returns>
		// Token: 0x17001004 RID: 4100
		// (get) Token: 0x06005C4F RID: 23631 RVA: 0x001368C1 File Offset: 0x00134AC1
		public virtual int Count
		{
			get
			{
				return this._size;
			}
		}

		/// <summary>Gets the keys in a <see cref="T:System.Collections.SortedList" /> object.</summary>
		/// <returns>An <see cref="T:System.Collections.ICollection" /> object containing the keys in the <see cref="T:System.Collections.SortedList" /> object.</returns>
		// Token: 0x17001005 RID: 4101
		// (get) Token: 0x06005C50 RID: 23632 RVA: 0x001368C9 File Offset: 0x00134AC9
		public virtual ICollection Keys
		{
			get
			{
				return this.GetKeyList();
			}
		}

		/// <summary>Gets the values in a <see cref="T:System.Collections.SortedList" /> object.</summary>
		/// <returns>An <see cref="T:System.Collections.ICollection" /> object containing the values in the <see cref="T:System.Collections.SortedList" /> object.</returns>
		// Token: 0x17001006 RID: 4102
		// (get) Token: 0x06005C51 RID: 23633 RVA: 0x001368D1 File Offset: 0x00134AD1
		public virtual ICollection Values
		{
			get
			{
				return this.GetValueList();
			}
		}

		/// <summary>Gets a value indicating whether a <see cref="T:System.Collections.SortedList" /> object is read-only.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Collections.SortedList" /> object is read-only; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		// Token: 0x17001007 RID: 4103
		// (get) Token: 0x06005C52 RID: 23634 RVA: 0x0000DDA9 File Offset: 0x0000BFA9
		public virtual bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets a value indicating whether a <see cref="T:System.Collections.SortedList" /> object has a fixed size.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Collections.SortedList" /> object has a fixed size; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		// Token: 0x17001008 RID: 4104
		// (get) Token: 0x06005C53 RID: 23635 RVA: 0x0000DDA9 File Offset: 0x0000BFA9
		public virtual bool IsFixedSize
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets a value indicating whether access to a <see cref="T:System.Collections.SortedList" /> object is synchronized (thread safe).</summary>
		/// <returns>
		///   <see langword="true" /> if access to the <see cref="T:System.Collections.SortedList" /> object is synchronized (thread safe); otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		// Token: 0x17001009 RID: 4105
		// (get) Token: 0x06005C54 RID: 23636 RVA: 0x0000DDA9 File Offset: 0x0000BFA9
		public virtual bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets an object that can be used to synchronize access to a <see cref="T:System.Collections.SortedList" /> object.</summary>
		/// <returns>An object that can be used to synchronize access to the <see cref="T:System.Collections.SortedList" /> object.</returns>
		// Token: 0x1700100A RID: 4106
		// (get) Token: 0x06005C55 RID: 23637 RVA: 0x001368D9 File Offset: 0x00134AD9
		public virtual object SyncRoot
		{
			get
			{
				if (this._syncRoot == null)
				{
					Interlocked.CompareExchange<object>(ref this._syncRoot, new object(), null);
				}
				return this._syncRoot;
			}
		}

		/// <summary>Removes all elements from a <see cref="T:System.Collections.SortedList" /> object.</summary>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.SortedList" /> object is read-only.  
		///  -or-  
		///  The <see cref="T:System.Collections.SortedList" /> has a fixed size.</exception>
		// Token: 0x06005C56 RID: 23638 RVA: 0x001368FB File Offset: 0x00134AFB
		public virtual void Clear()
		{
			this.version++;
			Array.Clear(this.keys, 0, this._size);
			Array.Clear(this.values, 0, this._size);
			this._size = 0;
		}

		/// <summary>Creates a shallow copy of a <see cref="T:System.Collections.SortedList" /> object.</summary>
		/// <returns>A shallow copy of the <see cref="T:System.Collections.SortedList" /> object.</returns>
		// Token: 0x06005C57 RID: 23639 RVA: 0x00136938 File Offset: 0x00134B38
		public virtual object Clone()
		{
			SortedList sortedList = new SortedList(this._size);
			Array.Copy(this.keys, 0, sortedList.keys, 0, this._size);
			Array.Copy(this.values, 0, sortedList.values, 0, this._size);
			sortedList._size = this._size;
			sortedList.version = this.version;
			sortedList.comparer = this.comparer;
			return sortedList;
		}

		/// <summary>Determines whether a <see cref="T:System.Collections.SortedList" /> object contains a specific key.</summary>
		/// <param name="key">The key to locate in the <see cref="T:System.Collections.SortedList" /> object.</param>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Collections.SortedList" /> object contains an element with the specified <paramref name="key" />; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The comparer throws an exception.</exception>
		// Token: 0x06005C58 RID: 23640 RVA: 0x001369A8 File Offset: 0x00134BA8
		public virtual bool Contains(object key)
		{
			return this.IndexOfKey(key) >= 0;
		}

		/// <summary>Determines whether a <see cref="T:System.Collections.SortedList" /> object contains a specific key.</summary>
		/// <param name="key">The key to locate in the <see cref="T:System.Collections.SortedList" /> object.</param>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Collections.SortedList" /> object contains an element with the specified <paramref name="key" />; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The comparer throws an exception.</exception>
		// Token: 0x06005C59 RID: 23641 RVA: 0x001369A8 File Offset: 0x00134BA8
		public virtual bool ContainsKey(object key)
		{
			return this.IndexOfKey(key) >= 0;
		}

		/// <summary>Determines whether a <see cref="T:System.Collections.SortedList" /> object contains a specific value.</summary>
		/// <param name="value">The value to locate in the <see cref="T:System.Collections.SortedList" /> object. The value can be <see langword="null" />.</param>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Collections.SortedList" /> object contains an element with the specified <paramref name="value" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x06005C5A RID: 23642 RVA: 0x001369B7 File Offset: 0x00134BB7
		public virtual bool ContainsValue(object value)
		{
			return this.IndexOfValue(value) >= 0;
		}

		/// <summary>Copies <see cref="T:System.Collections.SortedList" /> elements to a one-dimensional <see cref="T:System.Array" /> object, starting at the specified index in the array.</summary>
		/// <param name="array">The one-dimensional <see cref="T:System.Array" /> object that is the destination of the <see cref="T:System.Collections.DictionaryEntry" /> objects copied from <see cref="T:System.Collections.SortedList" />. The <see cref="T:System.Array" /> must have zero-based indexing.</param>
		/// <param name="arrayIndex">The zero-based index in <paramref name="array" /> at which copying begins.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="arrayIndex" /> is less than zero.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="array" /> is multidimensional.  
		/// -or-  
		/// The number of elements in the source <see cref="T:System.Collections.SortedList" /> object is greater than the available space from <paramref name="arrayIndex" /> to the end of the destination <paramref name="array" />.</exception>
		/// <exception cref="T:System.InvalidCastException">The type of the source <see cref="T:System.Collections.SortedList" /> cannot be cast automatically to the type of the destination <paramref name="array" />.</exception>
		// Token: 0x06005C5B RID: 23643 RVA: 0x001369C8 File Offset: 0x00134BC8
		public virtual void CopyTo(Array array, int arrayIndex)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array", "Array cannot be null.");
			}
			if (array.Rank != 1)
			{
				throw new ArgumentException("Only single dimensional arrays are supported for the requested action.", "array");
			}
			if (arrayIndex < 0)
			{
				throw new ArgumentOutOfRangeException("arrayIndex", "Non-negative number required.");
			}
			if (array.Length - arrayIndex < this.Count)
			{
				throw new ArgumentException("Destination array is not long enough to copy all the items in the collection. Check array index and length.");
			}
			for (int i = 0; i < this.Count; i++)
			{
				DictionaryEntry dictionaryEntry = new DictionaryEntry(this.keys[i], this.values[i]);
				array.SetValue(dictionaryEntry, i + arrayIndex);
			}
		}

		// Token: 0x06005C5C RID: 23644 RVA: 0x00136A68 File Offset: 0x00134C68
		internal virtual KeyValuePairs[] ToKeyValuePairsArray()
		{
			KeyValuePairs[] array = new KeyValuePairs[this.Count];
			for (int i = 0; i < this.Count; i++)
			{
				array[i] = new KeyValuePairs(this.keys[i], this.values[i]);
			}
			return array;
		}

		// Token: 0x06005C5D RID: 23645 RVA: 0x00136AAC File Offset: 0x00134CAC
		private void EnsureCapacity(int min)
		{
			int num = (this.keys.Length == 0) ? 16 : (this.keys.Length * 2);
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

		/// <summary>Gets the value at the specified index of a <see cref="T:System.Collections.SortedList" /> object.</summary>
		/// <param name="index">The zero-based index of the value to get.</param>
		/// <returns>The value at the specified index of the <see cref="T:System.Collections.SortedList" /> object.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is outside the range of valid indexes for the <see cref="T:System.Collections.SortedList" /> object.</exception>
		// Token: 0x06005C5E RID: 23646 RVA: 0x00136AEC File Offset: 0x00134CEC
		public virtual object GetByIndex(int index)
		{
			if (index < 0 || index >= this.Count)
			{
				throw new ArgumentOutOfRangeException("index", "Index was out of range. Must be non-negative and less than the size of the collection.");
			}
			return this.values[index];
		}

		/// <summary>Returns an <see cref="T:System.Collections.IEnumerator" /> that iterates through the <see cref="T:System.Collections.SortedList" />.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> for the <see cref="T:System.Collections.SortedList" />.</returns>
		// Token: 0x06005C5F RID: 23647 RVA: 0x00136B13 File Offset: 0x00134D13
		IEnumerator IEnumerable.GetEnumerator()
		{
			return new SortedList.SortedListEnumerator(this, 0, this._size, 3);
		}

		/// <summary>Returns an <see cref="T:System.Collections.IDictionaryEnumerator" /> object that iterates through a <see cref="T:System.Collections.SortedList" /> object.</summary>
		/// <returns>An <see cref="T:System.Collections.IDictionaryEnumerator" /> object for the <see cref="T:System.Collections.SortedList" /> object.</returns>
		// Token: 0x06005C60 RID: 23648 RVA: 0x00136B13 File Offset: 0x00134D13
		public virtual IDictionaryEnumerator GetEnumerator()
		{
			return new SortedList.SortedListEnumerator(this, 0, this._size, 3);
		}

		/// <summary>Gets the key at the specified index of a <see cref="T:System.Collections.SortedList" /> object.</summary>
		/// <param name="index">The zero-based index of the key to get.</param>
		/// <returns>The key at the specified index of the <see cref="T:System.Collections.SortedList" /> object.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is outside the range of valid indexes for the <see cref="T:System.Collections.SortedList" /> object.</exception>
		// Token: 0x06005C61 RID: 23649 RVA: 0x00136B23 File Offset: 0x00134D23
		public virtual object GetKey(int index)
		{
			if (index < 0 || index >= this.Count)
			{
				throw new ArgumentOutOfRangeException("index", "Index was out of range. Must be non-negative and less than the size of the collection.");
			}
			return this.keys[index];
		}

		/// <summary>Gets the keys in a <see cref="T:System.Collections.SortedList" /> object.</summary>
		/// <returns>An <see cref="T:System.Collections.IList" /> object containing the keys in the <see cref="T:System.Collections.SortedList" /> object.</returns>
		// Token: 0x06005C62 RID: 23650 RVA: 0x00136B4A File Offset: 0x00134D4A
		public virtual IList GetKeyList()
		{
			if (this.keyList == null)
			{
				this.keyList = new SortedList.KeyList(this);
			}
			return this.keyList;
		}

		/// <summary>Gets the values in a <see cref="T:System.Collections.SortedList" /> object.</summary>
		/// <returns>An <see cref="T:System.Collections.IList" /> object containing the values in the <see cref="T:System.Collections.SortedList" /> object.</returns>
		// Token: 0x06005C63 RID: 23651 RVA: 0x00136B66 File Offset: 0x00134D66
		public virtual IList GetValueList()
		{
			if (this.valueList == null)
			{
				this.valueList = new SortedList.ValueList(this);
			}
			return this.valueList;
		}

		/// <summary>Gets or sets the value associated with a specific key in a <see cref="T:System.Collections.SortedList" /> object.</summary>
		/// <param name="key">The key associated with the value to get or set.</param>
		/// <returns>The value associated with the <paramref name="key" /> parameter in the <see cref="T:System.Collections.SortedList" /> object, if <paramref name="key" /> is found; otherwise, <see langword="null" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.NotSupportedException">The property is set and the <see cref="T:System.Collections.SortedList" /> object is read-only.  
		///  -or-  
		///  The property is set, <paramref name="key" /> does not exist in the collection, and the <see cref="T:System.Collections.SortedList" /> has a fixed size.</exception>
		/// <exception cref="T:System.OutOfMemoryException">There is not enough available memory to add the element to the <see cref="T:System.Collections.SortedList" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The comparer throws an exception.</exception>
		// Token: 0x1700100B RID: 4107
		public virtual object this[object key]
		{
			get
			{
				int num = this.IndexOfKey(key);
				if (num >= 0)
				{
					return this.values[num];
				}
				return null;
			}
			set
			{
				if (key == null)
				{
					throw new ArgumentNullException("key", "Key cannot be null.");
				}
				int num = Array.BinarySearch(this.keys, 0, this._size, key, this.comparer);
				if (num >= 0)
				{
					this.values[num] = value;
					this.version++;
					return;
				}
				this.Insert(~num, key, value);
			}
		}

		/// <summary>Returns the zero-based index of the specified key in a <see cref="T:System.Collections.SortedList" /> object.</summary>
		/// <param name="key">The key to locate in the <see cref="T:System.Collections.SortedList" /> object.</param>
		/// <returns>The zero-based index of the <paramref name="key" /> parameter, if <paramref name="key" /> is found in the <see cref="T:System.Collections.SortedList" /> object; otherwise, -1.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The comparer throws an exception.</exception>
		// Token: 0x06005C66 RID: 23654 RVA: 0x00136C08 File Offset: 0x00134E08
		public virtual int IndexOfKey(object key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key", "Key cannot be null.");
			}
			int num = Array.BinarySearch(this.keys, 0, this._size, key, this.comparer);
			if (num < 0)
			{
				return -1;
			}
			return num;
		}

		/// <summary>Returns the zero-based index of the first occurrence of the specified value in a <see cref="T:System.Collections.SortedList" /> object.</summary>
		/// <param name="value">The value to locate in the <see cref="T:System.Collections.SortedList" /> object. The value can be <see langword="null" />.</param>
		/// <returns>The zero-based index of the first occurrence of the <paramref name="value" /> parameter, if <paramref name="value" /> is found in the <see cref="T:System.Collections.SortedList" /> object; otherwise, -1.</returns>
		// Token: 0x06005C67 RID: 23655 RVA: 0x00136C49 File Offset: 0x00134E49
		public virtual int IndexOfValue(object value)
		{
			return Array.IndexOf<object>(this.values, value, 0, this._size);
		}

		// Token: 0x06005C68 RID: 23656 RVA: 0x00136C60 File Offset: 0x00134E60
		private void Insert(int index, object key, object value)
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

		/// <summary>Removes the element at the specified index of a <see cref="T:System.Collections.SortedList" /> object.</summary>
		/// <param name="index">The zero-based index of the element to remove.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is outside the range of valid indexes for the <see cref="T:System.Collections.SortedList" /> object.</exception>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.SortedList" /> is read-only.  
		///  -or-  
		///  The <see cref="T:System.Collections.SortedList" /> has a fixed size.</exception>
		// Token: 0x06005C69 RID: 23657 RVA: 0x00136CFC File Offset: 0x00134EFC
		public virtual void RemoveAt(int index)
		{
			if (index < 0 || index >= this.Count)
			{
				throw new ArgumentOutOfRangeException("index", "Index was out of range. Must be non-negative and less than the size of the collection.");
			}
			this._size--;
			if (index < this._size)
			{
				Array.Copy(this.keys, index + 1, this.keys, index, this._size - index);
				Array.Copy(this.values, index + 1, this.values, index, this._size - index);
			}
			this.keys[this._size] = null;
			this.values[this._size] = null;
			this.version++;
		}

		/// <summary>Removes the element with the specified key from a <see cref="T:System.Collections.SortedList" /> object.</summary>
		/// <param name="key">The key of the element to remove.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.SortedList" /> object is read-only.  
		///  -or-  
		///  The <see cref="T:System.Collections.SortedList" /> has a fixed size.</exception>
		// Token: 0x06005C6A RID: 23658 RVA: 0x00136DA4 File Offset: 0x00134FA4
		public virtual void Remove(object key)
		{
			int num = this.IndexOfKey(key);
			if (num >= 0)
			{
				this.RemoveAt(num);
			}
		}

		/// <summary>Replaces the value at a specific index in a <see cref="T:System.Collections.SortedList" /> object.</summary>
		/// <param name="index">The zero-based index at which to save <paramref name="value" />.</param>
		/// <param name="value">The <see cref="T:System.Object" /> to save into the <see cref="T:System.Collections.SortedList" /> object. The value can be <see langword="null" />.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is outside the range of valid indexes for the <see cref="T:System.Collections.SortedList" /> object.</exception>
		// Token: 0x06005C6B RID: 23659 RVA: 0x00136DC4 File Offset: 0x00134FC4
		public virtual void SetByIndex(int index, object value)
		{
			if (index < 0 || index >= this.Count)
			{
				throw new ArgumentOutOfRangeException("index", "Index was out of range. Must be non-negative and less than the size of the collection.");
			}
			this.values[index] = value;
			this.version++;
		}

		/// <summary>Returns a synchronized (thread-safe) wrapper for a <see cref="T:System.Collections.SortedList" /> object.</summary>
		/// <param name="list">The <see cref="T:System.Collections.SortedList" /> object to synchronize.</param>
		/// <returns>A synchronized (thread-safe) wrapper for the <see cref="T:System.Collections.SortedList" /> object.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="list" /> is <see langword="null" />.</exception>
		// Token: 0x06005C6C RID: 23660 RVA: 0x00136DFA File Offset: 0x00134FFA
		public static SortedList Synchronized(SortedList list)
		{
			if (list == null)
			{
				throw new ArgumentNullException("list");
			}
			return new SortedList.SyncSortedList(list);
		}

		/// <summary>Sets the capacity to the actual number of elements in a <see cref="T:System.Collections.SortedList" /> object.</summary>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.SortedList" /> object is read-only.  
		///  -or-  
		///  The <see cref="T:System.Collections.SortedList" /> has a fixed size.</exception>
		// Token: 0x06005C6D RID: 23661 RVA: 0x00136E10 File Offset: 0x00135010
		public virtual void TrimToSize()
		{
			this.Capacity = this._size;
		}

		// Token: 0x0400389B RID: 14491
		private object[] keys;

		// Token: 0x0400389C RID: 14492
		private object[] values;

		// Token: 0x0400389D RID: 14493
		private int _size;

		// Token: 0x0400389E RID: 14494
		private int version;

		// Token: 0x0400389F RID: 14495
		private IComparer comparer;

		// Token: 0x040038A0 RID: 14496
		private SortedList.KeyList keyList;

		// Token: 0x040038A1 RID: 14497
		private SortedList.ValueList valueList;

		// Token: 0x040038A2 RID: 14498
		[NonSerialized]
		private object _syncRoot;

		// Token: 0x040038A3 RID: 14499
		private const int _defaultCapacity = 16;

		// Token: 0x040038A4 RID: 14500
		internal const int MaxArrayLength = 2146435071;

		// Token: 0x02000A2D RID: 2605
		[Serializable]
		private class SyncSortedList : SortedList
		{
			// Token: 0x06005C6E RID: 23662 RVA: 0x00136E1E File Offset: 0x0013501E
			internal SyncSortedList(SortedList list)
			{
				this._list = list;
				this._root = list.SyncRoot;
			}

			// Token: 0x1700100C RID: 4108
			// (get) Token: 0x06005C6F RID: 23663 RVA: 0x00136E3C File Offset: 0x0013503C
			public override int Count
			{
				get
				{
					object root = this._root;
					int count;
					lock (root)
					{
						count = this._list.Count;
					}
					return count;
				}
			}

			// Token: 0x1700100D RID: 4109
			// (get) Token: 0x06005C70 RID: 23664 RVA: 0x00136E84 File Offset: 0x00135084
			public override object SyncRoot
			{
				get
				{
					return this._root;
				}
			}

			// Token: 0x1700100E RID: 4110
			// (get) Token: 0x06005C71 RID: 23665 RVA: 0x00136E8C File Offset: 0x0013508C
			public override bool IsReadOnly
			{
				get
				{
					return this._list.IsReadOnly;
				}
			}

			// Token: 0x1700100F RID: 4111
			// (get) Token: 0x06005C72 RID: 23666 RVA: 0x00136E99 File Offset: 0x00135099
			public override bool IsFixedSize
			{
				get
				{
					return this._list.IsFixedSize;
				}
			}

			// Token: 0x17001010 RID: 4112
			// (get) Token: 0x06005C73 RID: 23667 RVA: 0x000040F7 File Offset: 0x000022F7
			public override bool IsSynchronized
			{
				get
				{
					return true;
				}
			}

			// Token: 0x17001011 RID: 4113
			public override object this[object key]
			{
				get
				{
					object root = this._root;
					object result;
					lock (root)
					{
						result = this._list[key];
					}
					return result;
				}
				set
				{
					object root = this._root;
					lock (root)
					{
						this._list[key] = value;
					}
				}
			}

			// Token: 0x06005C76 RID: 23670 RVA: 0x00136F38 File Offset: 0x00135138
			public override void Add(object key, object value)
			{
				object root = this._root;
				lock (root)
				{
					this._list.Add(key, value);
				}
			}

			// Token: 0x17001012 RID: 4114
			// (get) Token: 0x06005C77 RID: 23671 RVA: 0x00136F80 File Offset: 0x00135180
			public override int Capacity
			{
				get
				{
					object root = this._root;
					int capacity;
					lock (root)
					{
						capacity = this._list.Capacity;
					}
					return capacity;
				}
			}

			// Token: 0x06005C78 RID: 23672 RVA: 0x00136FC8 File Offset: 0x001351C8
			public override void Clear()
			{
				object root = this._root;
				lock (root)
				{
					this._list.Clear();
				}
			}

			// Token: 0x06005C79 RID: 23673 RVA: 0x00137010 File Offset: 0x00135210
			public override object Clone()
			{
				object root = this._root;
				object result;
				lock (root)
				{
					result = this._list.Clone();
				}
				return result;
			}

			// Token: 0x06005C7A RID: 23674 RVA: 0x00137058 File Offset: 0x00135258
			public override bool Contains(object key)
			{
				object root = this._root;
				bool result;
				lock (root)
				{
					result = this._list.Contains(key);
				}
				return result;
			}

			// Token: 0x06005C7B RID: 23675 RVA: 0x001370A0 File Offset: 0x001352A0
			public override bool ContainsKey(object key)
			{
				object root = this._root;
				bool result;
				lock (root)
				{
					result = this._list.ContainsKey(key);
				}
				return result;
			}

			// Token: 0x06005C7C RID: 23676 RVA: 0x001370E8 File Offset: 0x001352E8
			public override bool ContainsValue(object key)
			{
				object root = this._root;
				bool result;
				lock (root)
				{
					result = this._list.ContainsValue(key);
				}
				return result;
			}

			// Token: 0x06005C7D RID: 23677 RVA: 0x00137130 File Offset: 0x00135330
			public override void CopyTo(Array array, int index)
			{
				object root = this._root;
				lock (root)
				{
					this._list.CopyTo(array, index);
				}
			}

			// Token: 0x06005C7E RID: 23678 RVA: 0x00137178 File Offset: 0x00135378
			public override object GetByIndex(int index)
			{
				object root = this._root;
				object byIndex;
				lock (root)
				{
					byIndex = this._list.GetByIndex(index);
				}
				return byIndex;
			}

			// Token: 0x06005C7F RID: 23679 RVA: 0x001371C0 File Offset: 0x001353C0
			public override IDictionaryEnumerator GetEnumerator()
			{
				object root = this._root;
				IDictionaryEnumerator enumerator;
				lock (root)
				{
					enumerator = this._list.GetEnumerator();
				}
				return enumerator;
			}

			// Token: 0x06005C80 RID: 23680 RVA: 0x00137208 File Offset: 0x00135408
			public override object GetKey(int index)
			{
				object root = this._root;
				object key;
				lock (root)
				{
					key = this._list.GetKey(index);
				}
				return key;
			}

			// Token: 0x06005C81 RID: 23681 RVA: 0x00137250 File Offset: 0x00135450
			public override IList GetKeyList()
			{
				object root = this._root;
				IList keyList;
				lock (root)
				{
					keyList = this._list.GetKeyList();
				}
				return keyList;
			}

			// Token: 0x06005C82 RID: 23682 RVA: 0x00137298 File Offset: 0x00135498
			public override IList GetValueList()
			{
				object root = this._root;
				IList valueList;
				lock (root)
				{
					valueList = this._list.GetValueList();
				}
				return valueList;
			}

			// Token: 0x06005C83 RID: 23683 RVA: 0x001372E0 File Offset: 0x001354E0
			public override int IndexOfKey(object key)
			{
				if (key == null)
				{
					throw new ArgumentNullException("key", "Key cannot be null.");
				}
				object root = this._root;
				int result;
				lock (root)
				{
					result = this._list.IndexOfKey(key);
				}
				return result;
			}

			// Token: 0x06005C84 RID: 23684 RVA: 0x0013733C File Offset: 0x0013553C
			public override int IndexOfValue(object value)
			{
				object root = this._root;
				int result;
				lock (root)
				{
					result = this._list.IndexOfValue(value);
				}
				return result;
			}

			// Token: 0x06005C85 RID: 23685 RVA: 0x00137384 File Offset: 0x00135584
			public override void RemoveAt(int index)
			{
				object root = this._root;
				lock (root)
				{
					this._list.RemoveAt(index);
				}
			}

			// Token: 0x06005C86 RID: 23686 RVA: 0x001373CC File Offset: 0x001355CC
			public override void Remove(object key)
			{
				object root = this._root;
				lock (root)
				{
					this._list.Remove(key);
				}
			}

			// Token: 0x06005C87 RID: 23687 RVA: 0x00137414 File Offset: 0x00135614
			public override void SetByIndex(int index, object value)
			{
				object root = this._root;
				lock (root)
				{
					this._list.SetByIndex(index, value);
				}
			}

			// Token: 0x06005C88 RID: 23688 RVA: 0x0013745C File Offset: 0x0013565C
			internal override KeyValuePairs[] ToKeyValuePairsArray()
			{
				return this._list.ToKeyValuePairsArray();
			}

			// Token: 0x06005C89 RID: 23689 RVA: 0x0013746C File Offset: 0x0013566C
			public override void TrimToSize()
			{
				object root = this._root;
				lock (root)
				{
					this._list.TrimToSize();
				}
			}

			// Token: 0x040038A5 RID: 14501
			private SortedList _list;

			// Token: 0x040038A6 RID: 14502
			private object _root;
		}

		// Token: 0x02000A2E RID: 2606
		[Serializable]
		private class SortedListEnumerator : IDictionaryEnumerator, IEnumerator, ICloneable
		{
			// Token: 0x06005C8A RID: 23690 RVA: 0x001374B4 File Offset: 0x001356B4
			internal SortedListEnumerator(SortedList sortedList, int index, int count, int getObjRetType)
			{
				this._sortedList = sortedList;
				this._index = index;
				this._startIndex = index;
				this._endIndex = index + count;
				this._version = sortedList.version;
				this._getObjectRetType = getObjRetType;
				this._current = false;
			}

			// Token: 0x06005C8B RID: 23691 RVA: 0x000231CD File Offset: 0x000213CD
			public object Clone()
			{
				return base.MemberwiseClone();
			}

			// Token: 0x17001013 RID: 4115
			// (get) Token: 0x06005C8C RID: 23692 RVA: 0x00137500 File Offset: 0x00135700
			public virtual object Key
			{
				get
				{
					if (this._version != this._sortedList.version)
					{
						throw new InvalidOperationException("Collection was modified; enumeration operation may not execute.");
					}
					if (!this._current)
					{
						throw new InvalidOperationException("Enumeration has either not started or has already finished.");
					}
					return this._key;
				}
			}

			// Token: 0x06005C8D RID: 23693 RVA: 0x0013753C File Offset: 0x0013573C
			public virtual bool MoveNext()
			{
				if (this._version != this._sortedList.version)
				{
					throw new InvalidOperationException("Collection was modified; enumeration operation may not execute.");
				}
				if (this._index < this._endIndex)
				{
					this._key = this._sortedList.keys[this._index];
					this._value = this._sortedList.values[this._index];
					this._index++;
					this._current = true;
					return true;
				}
				this._key = null;
				this._value = null;
				this._current = false;
				return false;
			}

			// Token: 0x17001014 RID: 4116
			// (get) Token: 0x06005C8E RID: 23694 RVA: 0x001375D4 File Offset: 0x001357D4
			public virtual DictionaryEntry Entry
			{
				get
				{
					if (this._version != this._sortedList.version)
					{
						throw new InvalidOperationException("Collection was modified; enumeration operation may not execute.");
					}
					if (!this._current)
					{
						throw new InvalidOperationException("Enumeration has either not started or has already finished.");
					}
					return new DictionaryEntry(this._key, this._value);
				}
			}

			// Token: 0x17001015 RID: 4117
			// (get) Token: 0x06005C8F RID: 23695 RVA: 0x00137624 File Offset: 0x00135824
			public virtual object Current
			{
				get
				{
					if (!this._current)
					{
						throw new InvalidOperationException("Enumeration has either not started or has already finished.");
					}
					if (this._getObjectRetType == 1)
					{
						return this._key;
					}
					if (this._getObjectRetType == 2)
					{
						return this._value;
					}
					return new DictionaryEntry(this._key, this._value);
				}
			}

			// Token: 0x17001016 RID: 4118
			// (get) Token: 0x06005C90 RID: 23696 RVA: 0x0013767A File Offset: 0x0013587A
			public virtual object Value
			{
				get
				{
					if (this._version != this._sortedList.version)
					{
						throw new InvalidOperationException("Collection was modified; enumeration operation may not execute.");
					}
					if (!this._current)
					{
						throw new InvalidOperationException("Enumeration has either not started or has already finished.");
					}
					return this._value;
				}
			}

			// Token: 0x06005C91 RID: 23697 RVA: 0x001376B4 File Offset: 0x001358B4
			public virtual void Reset()
			{
				if (this._version != this._sortedList.version)
				{
					throw new InvalidOperationException("Collection was modified; enumeration operation may not execute.");
				}
				this._index = this._startIndex;
				this._current = false;
				this._key = null;
				this._value = null;
			}

			// Token: 0x040038A7 RID: 14503
			private SortedList _sortedList;

			// Token: 0x040038A8 RID: 14504
			private object _key;

			// Token: 0x040038A9 RID: 14505
			private object _value;

			// Token: 0x040038AA RID: 14506
			private int _index;

			// Token: 0x040038AB RID: 14507
			private int _startIndex;

			// Token: 0x040038AC RID: 14508
			private int _endIndex;

			// Token: 0x040038AD RID: 14509
			private int _version;

			// Token: 0x040038AE RID: 14510
			private bool _current;

			// Token: 0x040038AF RID: 14511
			private int _getObjectRetType;

			// Token: 0x040038B0 RID: 14512
			internal const int Keys = 1;

			// Token: 0x040038B1 RID: 14513
			internal const int Values = 2;

			// Token: 0x040038B2 RID: 14514
			internal const int DictEntry = 3;
		}

		// Token: 0x02000A2F RID: 2607
		[TypeForwardedFrom("mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089")]
		[Serializable]
		private class KeyList : IList, ICollection, IEnumerable
		{
			// Token: 0x06005C92 RID: 23698 RVA: 0x00137700 File Offset: 0x00135900
			internal KeyList(SortedList sortedList)
			{
				this.sortedList = sortedList;
			}

			// Token: 0x17001017 RID: 4119
			// (get) Token: 0x06005C93 RID: 23699 RVA: 0x0013770F File Offset: 0x0013590F
			public virtual int Count
			{
				get
				{
					return this.sortedList._size;
				}
			}

			// Token: 0x17001018 RID: 4120
			// (get) Token: 0x06005C94 RID: 23700 RVA: 0x000040F7 File Offset: 0x000022F7
			public virtual bool IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x17001019 RID: 4121
			// (get) Token: 0x06005C95 RID: 23701 RVA: 0x000040F7 File Offset: 0x000022F7
			public virtual bool IsFixedSize
			{
				get
				{
					return true;
				}
			}

			// Token: 0x1700101A RID: 4122
			// (get) Token: 0x06005C96 RID: 23702 RVA: 0x0013771C File Offset: 0x0013591C
			public virtual bool IsSynchronized
			{
				get
				{
					return this.sortedList.IsSynchronized;
				}
			}

			// Token: 0x1700101B RID: 4123
			// (get) Token: 0x06005C97 RID: 23703 RVA: 0x00137729 File Offset: 0x00135929
			public virtual object SyncRoot
			{
				get
				{
					return this.sortedList.SyncRoot;
				}
			}

			// Token: 0x06005C98 RID: 23704 RVA: 0x00137736 File Offset: 0x00135936
			public virtual int Add(object key)
			{
				throw new NotSupportedException("This operation is not supported on SortedList nested types because they require modifying the original SortedList.");
			}

			// Token: 0x06005C99 RID: 23705 RVA: 0x00137736 File Offset: 0x00135936
			public virtual void Clear()
			{
				throw new NotSupportedException("This operation is not supported on SortedList nested types because they require modifying the original SortedList.");
			}

			// Token: 0x06005C9A RID: 23706 RVA: 0x00137742 File Offset: 0x00135942
			public virtual bool Contains(object key)
			{
				return this.sortedList.Contains(key);
			}

			// Token: 0x06005C9B RID: 23707 RVA: 0x00137750 File Offset: 0x00135950
			public virtual void CopyTo(Array array, int arrayIndex)
			{
				if (array != null && array.Rank != 1)
				{
					throw new ArgumentException("Only single dimensional arrays are supported for the requested action.", "array");
				}
				Array.Copy(this.sortedList.keys, 0, array, arrayIndex, this.sortedList.Count);
			}

			// Token: 0x06005C9C RID: 23708 RVA: 0x00137736 File Offset: 0x00135936
			public virtual void Insert(int index, object value)
			{
				throw new NotSupportedException("This operation is not supported on SortedList nested types because they require modifying the original SortedList.");
			}

			// Token: 0x1700101C RID: 4124
			public virtual object this[int index]
			{
				get
				{
					return this.sortedList.GetKey(index);
				}
				set
				{
					throw new NotSupportedException("Mutating a key collection derived from a dictionary is not allowed.");
				}
			}

			// Token: 0x06005C9F RID: 23711 RVA: 0x001377A6 File Offset: 0x001359A6
			public virtual IEnumerator GetEnumerator()
			{
				return new SortedList.SortedListEnumerator(this.sortedList, 0, this.sortedList.Count, 1);
			}

			// Token: 0x06005CA0 RID: 23712 RVA: 0x001377C0 File Offset: 0x001359C0
			public virtual int IndexOf(object key)
			{
				if (key == null)
				{
					throw new ArgumentNullException("key", "Key cannot be null.");
				}
				int num = Array.BinarySearch(this.sortedList.keys, 0, this.sortedList.Count, key, this.sortedList.comparer);
				if (num >= 0)
				{
					return num;
				}
				return -1;
			}

			// Token: 0x06005CA1 RID: 23713 RVA: 0x00137736 File Offset: 0x00135936
			public virtual void Remove(object key)
			{
				throw new NotSupportedException("This operation is not supported on SortedList nested types because they require modifying the original SortedList.");
			}

			// Token: 0x06005CA2 RID: 23714 RVA: 0x00137736 File Offset: 0x00135936
			public virtual void RemoveAt(int index)
			{
				throw new NotSupportedException("This operation is not supported on SortedList nested types because they require modifying the original SortedList.");
			}

			// Token: 0x040038B3 RID: 14515
			private SortedList sortedList;
		}

		// Token: 0x02000A30 RID: 2608
		[TypeForwardedFrom("mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089")]
		[Serializable]
		private class ValueList : IList, ICollection, IEnumerable
		{
			// Token: 0x06005CA3 RID: 23715 RVA: 0x00137810 File Offset: 0x00135A10
			internal ValueList(SortedList sortedList)
			{
				this.sortedList = sortedList;
			}

			// Token: 0x1700101D RID: 4125
			// (get) Token: 0x06005CA4 RID: 23716 RVA: 0x0013781F File Offset: 0x00135A1F
			public virtual int Count
			{
				get
				{
					return this.sortedList._size;
				}
			}

			// Token: 0x1700101E RID: 4126
			// (get) Token: 0x06005CA5 RID: 23717 RVA: 0x000040F7 File Offset: 0x000022F7
			public virtual bool IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x1700101F RID: 4127
			// (get) Token: 0x06005CA6 RID: 23718 RVA: 0x000040F7 File Offset: 0x000022F7
			public virtual bool IsFixedSize
			{
				get
				{
					return true;
				}
			}

			// Token: 0x17001020 RID: 4128
			// (get) Token: 0x06005CA7 RID: 23719 RVA: 0x0013782C File Offset: 0x00135A2C
			public virtual bool IsSynchronized
			{
				get
				{
					return this.sortedList.IsSynchronized;
				}
			}

			// Token: 0x17001021 RID: 4129
			// (get) Token: 0x06005CA8 RID: 23720 RVA: 0x00137839 File Offset: 0x00135A39
			public virtual object SyncRoot
			{
				get
				{
					return this.sortedList.SyncRoot;
				}
			}

			// Token: 0x06005CA9 RID: 23721 RVA: 0x00137736 File Offset: 0x00135936
			public virtual int Add(object key)
			{
				throw new NotSupportedException("This operation is not supported on SortedList nested types because they require modifying the original SortedList.");
			}

			// Token: 0x06005CAA RID: 23722 RVA: 0x00137736 File Offset: 0x00135936
			public virtual void Clear()
			{
				throw new NotSupportedException("This operation is not supported on SortedList nested types because they require modifying the original SortedList.");
			}

			// Token: 0x06005CAB RID: 23723 RVA: 0x00137846 File Offset: 0x00135A46
			public virtual bool Contains(object value)
			{
				return this.sortedList.ContainsValue(value);
			}

			// Token: 0x06005CAC RID: 23724 RVA: 0x00137854 File Offset: 0x00135A54
			public virtual void CopyTo(Array array, int arrayIndex)
			{
				if (array != null && array.Rank != 1)
				{
					throw new ArgumentException("Only single dimensional arrays are supported for the requested action.", "array");
				}
				Array.Copy(this.sortedList.values, 0, array, arrayIndex, this.sortedList.Count);
			}

			// Token: 0x06005CAD RID: 23725 RVA: 0x00137736 File Offset: 0x00135936
			public virtual void Insert(int index, object value)
			{
				throw new NotSupportedException("This operation is not supported on SortedList nested types because they require modifying the original SortedList.");
			}

			// Token: 0x17001022 RID: 4130
			public virtual object this[int index]
			{
				get
				{
					return this.sortedList.GetByIndex(index);
				}
				set
				{
					throw new NotSupportedException("This operation is not supported on SortedList nested types because they require modifying the original SortedList.");
				}
			}

			// Token: 0x06005CB0 RID: 23728 RVA: 0x0013789E File Offset: 0x00135A9E
			public virtual IEnumerator GetEnumerator()
			{
				return new SortedList.SortedListEnumerator(this.sortedList, 0, this.sortedList.Count, 2);
			}

			// Token: 0x06005CB1 RID: 23729 RVA: 0x001378B8 File Offset: 0x00135AB8
			public virtual int IndexOf(object value)
			{
				return Array.IndexOf<object>(this.sortedList.values, value, 0, this.sortedList.Count);
			}

			// Token: 0x06005CB2 RID: 23730 RVA: 0x00137736 File Offset: 0x00135936
			public virtual void Remove(object value)
			{
				throw new NotSupportedException("This operation is not supported on SortedList nested types because they require modifying the original SortedList.");
			}

			// Token: 0x06005CB3 RID: 23731 RVA: 0x00137736 File Offset: 0x00135936
			public virtual void RemoveAt(int index)
			{
				throw new NotSupportedException("This operation is not supported on SortedList nested types because they require modifying the original SortedList.");
			}

			// Token: 0x040038B4 RID: 14516
			private SortedList sortedList;
		}

		// Token: 0x02000A31 RID: 2609
		internal class SortedListDebugView
		{
			// Token: 0x06005CB4 RID: 23732 RVA: 0x001378D7 File Offset: 0x00135AD7
			public SortedListDebugView(SortedList sortedList)
			{
				if (sortedList == null)
				{
					throw new ArgumentNullException("sortedList");
				}
				this._sortedList = sortedList;
			}

			// Token: 0x17001023 RID: 4131
			// (get) Token: 0x06005CB5 RID: 23733 RVA: 0x001378F4 File Offset: 0x00135AF4
			[DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
			public KeyValuePairs[] Items
			{
				get
				{
					return this._sortedList.ToKeyValuePairsArray();
				}
			}

			// Token: 0x040038B5 RID: 14517
			private SortedList _sortedList;
		}
	}
}

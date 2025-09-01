using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Threading;

namespace System.Collections.Specialized
{
	/// <summary>Represents a collection of key/value pairs that are accessible by the key or index.</summary>
	// Token: 0x020004AD RID: 1197
	[Serializable]
	public class OrderedDictionary : IOrderedDictionary, IDictionary, ICollection, IEnumerable, ISerializable, IDeserializationCallback
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Specialized.OrderedDictionary" /> class.</summary>
		// Token: 0x0600268A RID: 9866 RVA: 0x00086A50 File Offset: 0x00084C50
		public OrderedDictionary() : this(0)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Specialized.OrderedDictionary" /> class using the specified initial capacity.</summary>
		/// <param name="capacity">The initial number of elements that the <see cref="T:System.Collections.Specialized.OrderedDictionary" /> collection can contain.</param>
		// Token: 0x0600268B RID: 9867 RVA: 0x00086A59 File Offset: 0x00084C59
		public OrderedDictionary(int capacity) : this(capacity, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Specialized.OrderedDictionary" /> class using the specified comparer.</summary>
		/// <param name="comparer">The <see cref="T:System.Collections.IComparer" /> to use to determine whether two keys are equal.  
		///  -or-  
		///  <see langword="null" /> to use the default comparer, which is each key's implementation of <see cref="M:System.Object.Equals(System.Object)" />.</param>
		// Token: 0x0600268C RID: 9868 RVA: 0x00086A63 File Offset: 0x00084C63
		public OrderedDictionary(IEqualityComparer comparer) : this(0, comparer)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Specialized.OrderedDictionary" /> class using the specified initial capacity and comparer.</summary>
		/// <param name="capacity">The initial number of elements that the <see cref="T:System.Collections.Specialized.OrderedDictionary" /> collection can contain.</param>
		/// <param name="comparer">The <see cref="T:System.Collections.IComparer" /> to use to determine whether two keys are equal.  
		///  -or-  
		///  <see langword="null" /> to use the default comparer, which is each key's implementation of <see cref="M:System.Object.Equals(System.Object)" />.</param>
		// Token: 0x0600268D RID: 9869 RVA: 0x00086A6D File Offset: 0x00084C6D
		public OrderedDictionary(int capacity, IEqualityComparer comparer)
		{
			this._initialCapacity = capacity;
			this._comparer = comparer;
		}

		// Token: 0x0600268E RID: 9870 RVA: 0x00086A83 File Offset: 0x00084C83
		private OrderedDictionary(OrderedDictionary dictionary)
		{
			this._readOnly = true;
			this._objectsArray = dictionary._objectsArray;
			this._objectsTable = dictionary._objectsTable;
			this._comparer = dictionary._comparer;
			this._initialCapacity = dictionary._initialCapacity;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Specialized.OrderedDictionary" /> class that is serializable using the specified <see cref="T:System.Runtime.Serialization.SerializationInfo" /> and <see cref="T:System.Runtime.Serialization.StreamingContext" /> objects.</summary>
		/// <param name="info">A <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object containing the information required to serialize the <see cref="T:System.Collections.Specialized.OrderedDictionary" /> collection.</param>
		/// <param name="context">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> object containing the source and destination of the serialized stream associated with the <see cref="T:System.Collections.Specialized.OrderedDictionary" />.</param>
		// Token: 0x0600268F RID: 9871 RVA: 0x00086AC2 File Offset: 0x00084CC2
		protected OrderedDictionary(SerializationInfo info, StreamingContext context)
		{
			this._siInfo = info;
		}

		/// <summary>Gets the number of key/values pairs contained in the <see cref="T:System.Collections.Specialized.OrderedDictionary" /> collection.</summary>
		/// <returns>The number of key/value pairs contained in the <see cref="T:System.Collections.Specialized.OrderedDictionary" /> collection.</returns>
		// Token: 0x170007C9 RID: 1993
		// (get) Token: 0x06002690 RID: 9872 RVA: 0x00086AD1 File Offset: 0x00084CD1
		public int Count
		{
			get
			{
				return this.objectsArray.Count;
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Collections.Specialized.OrderedDictionary" /> has a fixed size.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Collections.Specialized.OrderedDictionary" /> has a fixed size; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		// Token: 0x170007CA RID: 1994
		// (get) Token: 0x06002691 RID: 9873 RVA: 0x00086ADE File Offset: 0x00084CDE
		bool IDictionary.IsFixedSize
		{
			get
			{
				return this._readOnly;
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Collections.Specialized.OrderedDictionary" /> collection is read-only.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Collections.Specialized.OrderedDictionary" /> collection is read-only; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		// Token: 0x170007CB RID: 1995
		// (get) Token: 0x06002692 RID: 9874 RVA: 0x00086ADE File Offset: 0x00084CDE
		public bool IsReadOnly
		{
			get
			{
				return this._readOnly;
			}
		}

		/// <summary>Gets a value indicating whether access to the <see cref="T:System.Collections.Specialized.OrderedDictionary" /> object is synchronized (thread-safe).</summary>
		/// <returns>This method always returns <see langword="false" />.</returns>
		// Token: 0x170007CC RID: 1996
		// (get) Token: 0x06002693 RID: 9875 RVA: 0x00003062 File Offset: 0x00001262
		bool ICollection.IsSynchronized
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets an <see cref="T:System.Collections.ICollection" /> object containing the keys in the <see cref="T:System.Collections.Specialized.OrderedDictionary" /> collection.</summary>
		/// <returns>An <see cref="T:System.Collections.ICollection" /> object containing the keys in the <see cref="T:System.Collections.Specialized.OrderedDictionary" /> collection.</returns>
		// Token: 0x170007CD RID: 1997
		// (get) Token: 0x06002694 RID: 9876 RVA: 0x00086AE6 File Offset: 0x00084CE6
		public ICollection Keys
		{
			get
			{
				return new OrderedDictionary.OrderedDictionaryKeyValueCollection(this.objectsArray, true);
			}
		}

		// Token: 0x170007CE RID: 1998
		// (get) Token: 0x06002695 RID: 9877 RVA: 0x00086AF4 File Offset: 0x00084CF4
		private ArrayList objectsArray
		{
			get
			{
				if (this._objectsArray == null)
				{
					this._objectsArray = new ArrayList(this._initialCapacity);
				}
				return this._objectsArray;
			}
		}

		// Token: 0x170007CF RID: 1999
		// (get) Token: 0x06002696 RID: 9878 RVA: 0x00086B15 File Offset: 0x00084D15
		private Hashtable objectsTable
		{
			get
			{
				if (this._objectsTable == null)
				{
					this._objectsTable = new Hashtable(this._initialCapacity, this._comparer);
				}
				return this._objectsTable;
			}
		}

		/// <summary>Gets an object that can be used to synchronize access to the <see cref="T:System.Collections.Specialized.OrderedDictionary" /> object.</summary>
		/// <returns>An object that can be used to synchronize access to the <see cref="T:System.Collections.Specialized.OrderedDictionary" /> object.</returns>
		// Token: 0x170007D0 RID: 2000
		// (get) Token: 0x06002697 RID: 9879 RVA: 0x00086B3C File Offset: 0x00084D3C
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

		/// <summary>Gets or sets the value at the specified index.</summary>
		/// <param name="index">The zero-based index of the value to get or set.</param>
		/// <returns>The value of the item at the specified index.</returns>
		/// <exception cref="T:System.NotSupportedException">The property is being set and the <see cref="T:System.Collections.Specialized.OrderedDictionary" /> collection is read-only.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero.  
		/// -or-  
		/// <paramref name="index" /> is equal to or greater than <see cref="P:System.Collections.Specialized.OrderedDictionary.Count" />.</exception>
		// Token: 0x170007D1 RID: 2001
		public object this[int index]
		{
			get
			{
				return ((DictionaryEntry)this.objectsArray[index]).Value;
			}
			set
			{
				if (this._readOnly)
				{
					throw new NotSupportedException("The OrderedDictionary is readonly and cannot be modified.");
				}
				if (index < 0 || index >= this.objectsArray.Count)
				{
					throw new ArgumentOutOfRangeException("index");
				}
				object key = ((DictionaryEntry)this.objectsArray[index]).Key;
				this.objectsArray[index] = new DictionaryEntry(key, value);
				this.objectsTable[key] = value;
			}
		}

		/// <summary>Gets or sets the value with the specified key.</summary>
		/// <param name="key">The key of the value to get or set.</param>
		/// <returns>The value associated with the specified key. If the specified key is not found, attempting to get it returns <see langword="null" />, and attempting to set it creates a new element using the specified key.</returns>
		/// <exception cref="T:System.NotSupportedException">The property is being set and the <see cref="T:System.Collections.Specialized.OrderedDictionary" /> collection is read-only.</exception>
		// Token: 0x170007D2 RID: 2002
		public object this[object key]
		{
			get
			{
				return this.objectsTable[key];
			}
			set
			{
				if (this._readOnly)
				{
					throw new NotSupportedException("The OrderedDictionary is readonly and cannot be modified.");
				}
				if (this.objectsTable.Contains(key))
				{
					this.objectsTable[key] = value;
					this.objectsArray[this.IndexOfKey(key)] = new DictionaryEntry(key, value);
					return;
				}
				this.Add(key, value);
			}
		}

		/// <summary>Gets an <see cref="T:System.Collections.ICollection" /> object containing the values in the <see cref="T:System.Collections.Specialized.OrderedDictionary" /> collection.</summary>
		/// <returns>An <see cref="T:System.Collections.ICollection" /> object containing the values in the <see cref="T:System.Collections.Specialized.OrderedDictionary" /> collection.</returns>
		// Token: 0x170007D3 RID: 2003
		// (get) Token: 0x0600269C RID: 9884 RVA: 0x00086C76 File Offset: 0x00084E76
		public ICollection Values
		{
			get
			{
				return new OrderedDictionary.OrderedDictionaryKeyValueCollection(this.objectsArray, false);
			}
		}

		/// <summary>Adds an entry with the specified key and value into the <see cref="T:System.Collections.Specialized.OrderedDictionary" /> collection with the lowest available index.</summary>
		/// <param name="key">The key of the entry to add.</param>
		/// <param name="value">The value of the entry to add. This value can be <see langword="null" />.</param>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.Specialized.OrderedDictionary" /> collection is read-only.</exception>
		/// <exception cref="T:System.ArgumentException">An element with the same key already exists in the <see cref="T:System.Collections.Specialized.OrderedDictionary" /> collection.</exception>
		// Token: 0x0600269D RID: 9885 RVA: 0x00086C84 File Offset: 0x00084E84
		public void Add(object key, object value)
		{
			if (this._readOnly)
			{
				throw new NotSupportedException("The OrderedDictionary is readonly and cannot be modified.");
			}
			this.objectsTable.Add(key, value);
			this.objectsArray.Add(new DictionaryEntry(key, value));
		}

		/// <summary>Removes all elements from the <see cref="T:System.Collections.Specialized.OrderedDictionary" /> collection.</summary>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.Specialized.OrderedDictionary" /> collection is read-only.</exception>
		// Token: 0x0600269E RID: 9886 RVA: 0x00086CBE File Offset: 0x00084EBE
		public void Clear()
		{
			if (this._readOnly)
			{
				throw new NotSupportedException("The OrderedDictionary is readonly and cannot be modified.");
			}
			this.objectsTable.Clear();
			this.objectsArray.Clear();
		}

		/// <summary>Returns a read-only copy of the current <see cref="T:System.Collections.Specialized.OrderedDictionary" /> collection.</summary>
		/// <returns>A read-only copy of the current <see cref="T:System.Collections.Specialized.OrderedDictionary" /> collection.</returns>
		// Token: 0x0600269F RID: 9887 RVA: 0x00086CE9 File Offset: 0x00084EE9
		public OrderedDictionary AsReadOnly()
		{
			return new OrderedDictionary(this);
		}

		/// <summary>Determines whether the <see cref="T:System.Collections.Specialized.OrderedDictionary" /> collection contains a specific key.</summary>
		/// <param name="key">The key to locate in the <see cref="T:System.Collections.Specialized.OrderedDictionary" /> collection.</param>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Collections.Specialized.OrderedDictionary" /> collection contains an element with the specified key; otherwise, <see langword="false" />.</returns>
		// Token: 0x060026A0 RID: 9888 RVA: 0x00086CF1 File Offset: 0x00084EF1
		public bool Contains(object key)
		{
			return this.objectsTable.Contains(key);
		}

		/// <summary>Copies the <see cref="T:System.Collections.Specialized.OrderedDictionary" /> elements to a one-dimensional <see cref="T:System.Array" /> object at the specified index.</summary>
		/// <param name="array">The one-dimensional <see cref="T:System.Array" /> object that is the destination of the <see cref="T:System.Collections.DictionaryEntry" /> objects copied from <see cref="T:System.Collections.Specialized.OrderedDictionary" /> collection. The <see cref="T:System.Array" /> must have zero-based indexing.</param>
		/// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins.</param>
		// Token: 0x060026A1 RID: 9889 RVA: 0x00086CFF File Offset: 0x00084EFF
		public void CopyTo(Array array, int index)
		{
			this.objectsTable.CopyTo(array, index);
		}

		// Token: 0x060026A2 RID: 9890 RVA: 0x00086D10 File Offset: 0x00084F10
		private int IndexOfKey(object key)
		{
			for (int i = 0; i < this.objectsArray.Count; i++)
			{
				object key2 = ((DictionaryEntry)this.objectsArray[i]).Key;
				if (this._comparer != null)
				{
					if (this._comparer.Equals(key2, key))
					{
						return i;
					}
				}
				else if (key2.Equals(key))
				{
					return i;
				}
			}
			return -1;
		}

		/// <summary>Inserts a new entry into the <see cref="T:System.Collections.Specialized.OrderedDictionary" /> collection with the specified key and value at the specified index.</summary>
		/// <param name="index">The zero-based index at which the element should be inserted.</param>
		/// <param name="key">The key of the entry to add.</param>
		/// <param name="value">The value of the entry to add. The value can be <see langword="null" />.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is out of range.</exception>
		/// <exception cref="T:System.NotSupportedException">This collection is read-only.</exception>
		// Token: 0x060026A3 RID: 9891 RVA: 0x00086D74 File Offset: 0x00084F74
		public void Insert(int index, object key, object value)
		{
			if (this._readOnly)
			{
				throw new NotSupportedException("The OrderedDictionary is readonly and cannot be modified.");
			}
			if (index > this.Count || index < 0)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			this.objectsTable.Add(key, value);
			this.objectsArray.Insert(index, new DictionaryEntry(key, value));
		}

		/// <summary>Removes the entry at the specified index from the <see cref="T:System.Collections.Specialized.OrderedDictionary" /> collection.</summary>
		/// <param name="index">The zero-based index of the entry to remove.</param>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.Specialized.OrderedDictionary" /> collection is read-only.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero.  
		/// -or-
		///  <paramref name="index" /> is equal to or greater than <see cref="P:System.Collections.Specialized.OrderedDictionary.Count" />.</exception>
		// Token: 0x060026A4 RID: 9892 RVA: 0x00086DD4 File Offset: 0x00084FD4
		public void RemoveAt(int index)
		{
			if (this._readOnly)
			{
				throw new NotSupportedException("The OrderedDictionary is readonly and cannot be modified.");
			}
			if (index >= this.Count || index < 0)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			object key = ((DictionaryEntry)this.objectsArray[index]).Key;
			this.objectsArray.RemoveAt(index);
			this.objectsTable.Remove(key);
		}

		/// <summary>Removes the entry with the specified key from the <see cref="T:System.Collections.Specialized.OrderedDictionary" /> collection.</summary>
		/// <param name="key">The key of the entry to remove.</param>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.Specialized.OrderedDictionary" /> collection is read-only.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is <see langword="null" />.</exception>
		// Token: 0x060026A5 RID: 9893 RVA: 0x00086E40 File Offset: 0x00085040
		public void Remove(object key)
		{
			if (this._readOnly)
			{
				throw new NotSupportedException("The OrderedDictionary is readonly and cannot be modified.");
			}
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			int num = this.IndexOfKey(key);
			if (num < 0)
			{
				return;
			}
			this.objectsTable.Remove(key);
			this.objectsArray.RemoveAt(num);
		}

		/// <summary>Returns an <see cref="T:System.Collections.IDictionaryEnumerator" /> object that iterates through the <see cref="T:System.Collections.Specialized.OrderedDictionary" /> collection.</summary>
		/// <returns>An <see cref="T:System.Collections.IDictionaryEnumerator" /> object for the <see cref="T:System.Collections.Specialized.OrderedDictionary" /> collection.</returns>
		// Token: 0x060026A6 RID: 9894 RVA: 0x00086E93 File Offset: 0x00085093
		public virtual IDictionaryEnumerator GetEnumerator()
		{
			return new OrderedDictionary.OrderedDictionaryEnumerator(this.objectsArray, 3);
		}

		/// <summary>Returns an <see cref="T:System.Collections.IDictionaryEnumerator" /> object that iterates through the <see cref="T:System.Collections.Specialized.OrderedDictionary" /> collection.</summary>
		/// <returns>An <see cref="T:System.Collections.IDictionaryEnumerator" /> object for the <see cref="T:System.Collections.Specialized.OrderedDictionary" /> collection.</returns>
		// Token: 0x060026A7 RID: 9895 RVA: 0x00086E93 File Offset: 0x00085093
		IEnumerator IEnumerable.GetEnumerator()
		{
			return new OrderedDictionary.OrderedDictionaryEnumerator(this.objectsArray, 3);
		}

		/// <summary>Implements the <see cref="T:System.Runtime.Serialization.ISerializable" /> interface and returns the data needed to serialize the <see cref="T:System.Collections.Specialized.OrderedDictionary" /> collection.</summary>
		/// <param name="info">A <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object containing the information required to serialize the <see cref="T:System.Collections.Specialized.OrderedDictionary" /> collection.</param>
		/// <param name="context">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> object containing the source and destination of the serialized stream associated with the <see cref="T:System.Collections.Specialized.OrderedDictionary" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="info" /> is <see langword="null" />.</exception>
		// Token: 0x060026A8 RID: 9896 RVA: 0x00086EA4 File Offset: 0x000850A4
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			info.AddValue("KeyComparer", this._comparer, typeof(IEqualityComparer));
			info.AddValue("ReadOnly", this._readOnly);
			info.AddValue("InitialCapacity", this._initialCapacity);
			object[] array = new object[this.Count];
			this._objectsArray.CopyTo(array);
			info.AddValue("ArrayList", array);
		}

		/// <summary>Implements the <see cref="T:System.Runtime.Serialization.ISerializable" /> interface and is called back by the deserialization event when deserialization is complete.</summary>
		/// <param name="sender">The source of the deserialization event.</param>
		// Token: 0x060026A9 RID: 9897 RVA: 0x00086F20 File Offset: 0x00085120
		void IDeserializationCallback.OnDeserialization(object sender)
		{
			this.OnDeserialization(sender);
		}

		/// <summary>Implements the <see cref="T:System.Runtime.Serialization.ISerializable" /> interface and is called back by the deserialization event when deserialization is complete.</summary>
		/// <param name="sender">The source of the deserialization event.</param>
		/// <exception cref="T:System.Runtime.Serialization.SerializationException">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object associated with the current <see cref="T:System.Collections.Specialized.OrderedDictionary" /> collection is invalid.</exception>
		// Token: 0x060026AA RID: 9898 RVA: 0x00086F2C File Offset: 0x0008512C
		protected virtual void OnDeserialization(object sender)
		{
			if (this._siInfo == null)
			{
				throw new SerializationException("OnDeserialization method was called while the object was not being deserialized.");
			}
			this._comparer = (IEqualityComparer)this._siInfo.GetValue("KeyComparer", typeof(IEqualityComparer));
			this._readOnly = this._siInfo.GetBoolean("ReadOnly");
			this._initialCapacity = this._siInfo.GetInt32("InitialCapacity");
			object[] array = (object[])this._siInfo.GetValue("ArrayList", typeof(object[]));
			if (array != null)
			{
				foreach (object obj in array)
				{
					DictionaryEntry dictionaryEntry;
					try
					{
						dictionaryEntry = (DictionaryEntry)obj;
					}
					catch
					{
						throw new SerializationException("There was an error deserializing the OrderedDictionary.  The ArrayList does not contain DictionaryEntries.");
					}
					this.objectsArray.Add(dictionaryEntry);
					this.objectsTable.Add(dictionaryEntry.Key, dictionaryEntry.Value);
				}
			}
		}

		// Token: 0x040014FF RID: 5375
		private ArrayList _objectsArray;

		// Token: 0x04001500 RID: 5376
		private Hashtable _objectsTable;

		// Token: 0x04001501 RID: 5377
		private int _initialCapacity;

		// Token: 0x04001502 RID: 5378
		private IEqualityComparer _comparer;

		// Token: 0x04001503 RID: 5379
		private bool _readOnly;

		// Token: 0x04001504 RID: 5380
		private object _syncRoot;

		// Token: 0x04001505 RID: 5381
		private SerializationInfo _siInfo;

		// Token: 0x04001506 RID: 5382
		private const string KeyComparerName = "KeyComparer";

		// Token: 0x04001507 RID: 5383
		private const string ArrayListName = "ArrayList";

		// Token: 0x04001508 RID: 5384
		private const string ReadOnlyName = "ReadOnly";

		// Token: 0x04001509 RID: 5385
		private const string InitCapacityName = "InitialCapacity";

		// Token: 0x020004AE RID: 1198
		private class OrderedDictionaryEnumerator : IDictionaryEnumerator, IEnumerator
		{
			// Token: 0x060026AB RID: 9899 RVA: 0x00087028 File Offset: 0x00085228
			internal OrderedDictionaryEnumerator(ArrayList array, int objectReturnType)
			{
				this._arrayEnumerator = array.GetEnumerator();
				this._objectReturnType = objectReturnType;
			}

			// Token: 0x170007D4 RID: 2004
			// (get) Token: 0x060026AC RID: 9900 RVA: 0x00087044 File Offset: 0x00085244
			public object Current
			{
				get
				{
					if (this._objectReturnType == 1)
					{
						DictionaryEntry dictionaryEntry = (DictionaryEntry)this._arrayEnumerator.Current;
						return dictionaryEntry.Key;
					}
					if (this._objectReturnType == 2)
					{
						DictionaryEntry dictionaryEntry = (DictionaryEntry)this._arrayEnumerator.Current;
						return dictionaryEntry.Value;
					}
					return this.Entry;
				}
			}

			// Token: 0x170007D5 RID: 2005
			// (get) Token: 0x060026AD RID: 9901 RVA: 0x000870A0 File Offset: 0x000852A0
			public DictionaryEntry Entry
			{
				get
				{
					DictionaryEntry dictionaryEntry = (DictionaryEntry)this._arrayEnumerator.Current;
					object key = dictionaryEntry.Key;
					dictionaryEntry = (DictionaryEntry)this._arrayEnumerator.Current;
					return new DictionaryEntry(key, dictionaryEntry.Value);
				}
			}

			// Token: 0x170007D6 RID: 2006
			// (get) Token: 0x060026AE RID: 9902 RVA: 0x000870E4 File Offset: 0x000852E4
			public object Key
			{
				get
				{
					DictionaryEntry dictionaryEntry = (DictionaryEntry)this._arrayEnumerator.Current;
					return dictionaryEntry.Key;
				}
			}

			// Token: 0x170007D7 RID: 2007
			// (get) Token: 0x060026AF RID: 9903 RVA: 0x0008710C File Offset: 0x0008530C
			public object Value
			{
				get
				{
					DictionaryEntry dictionaryEntry = (DictionaryEntry)this._arrayEnumerator.Current;
					return dictionaryEntry.Value;
				}
			}

			// Token: 0x060026B0 RID: 9904 RVA: 0x00087131 File Offset: 0x00085331
			public bool MoveNext()
			{
				return this._arrayEnumerator.MoveNext();
			}

			// Token: 0x060026B1 RID: 9905 RVA: 0x0008713E File Offset: 0x0008533E
			public void Reset()
			{
				this._arrayEnumerator.Reset();
			}

			// Token: 0x0400150A RID: 5386
			private int _objectReturnType;

			// Token: 0x0400150B RID: 5387
			internal const int Keys = 1;

			// Token: 0x0400150C RID: 5388
			internal const int Values = 2;

			// Token: 0x0400150D RID: 5389
			internal const int DictionaryEntry = 3;

			// Token: 0x0400150E RID: 5390
			private IEnumerator _arrayEnumerator;
		}

		// Token: 0x020004AF RID: 1199
		private class OrderedDictionaryKeyValueCollection : ICollection, IEnumerable
		{
			// Token: 0x060026B2 RID: 9906 RVA: 0x0008714B File Offset: 0x0008534B
			public OrderedDictionaryKeyValueCollection(ArrayList array, bool isKeys)
			{
				this._objects = array;
				this._isKeys = isKeys;
			}

			// Token: 0x060026B3 RID: 9907 RVA: 0x00087164 File Offset: 0x00085364
			void ICollection.CopyTo(Array array, int index)
			{
				if (array == null)
				{
					throw new ArgumentNullException("array");
				}
				if (index < 0)
				{
					throw new ArgumentOutOfRangeException("index", index, "Non-negative number required.");
				}
				foreach (object obj in this._objects)
				{
					array.SetValue(this._isKeys ? ((DictionaryEntry)obj).Key : ((DictionaryEntry)obj).Value, index);
					index++;
				}
			}

			// Token: 0x170007D8 RID: 2008
			// (get) Token: 0x060026B4 RID: 9908 RVA: 0x0008720C File Offset: 0x0008540C
			int ICollection.Count
			{
				get
				{
					return this._objects.Count;
				}
			}

			// Token: 0x170007D9 RID: 2009
			// (get) Token: 0x060026B5 RID: 9909 RVA: 0x00003062 File Offset: 0x00001262
			bool ICollection.IsSynchronized
			{
				get
				{
					return false;
				}
			}

			// Token: 0x170007DA RID: 2010
			// (get) Token: 0x060026B6 RID: 9910 RVA: 0x00087219 File Offset: 0x00085419
			object ICollection.SyncRoot
			{
				get
				{
					return this._objects.SyncRoot;
				}
			}

			// Token: 0x060026B7 RID: 9911 RVA: 0x00087226 File Offset: 0x00085426
			IEnumerator IEnumerable.GetEnumerator()
			{
				return new OrderedDictionary.OrderedDictionaryEnumerator(this._objects, this._isKeys ? 1 : 2);
			}

			// Token: 0x0400150F RID: 5391
			private ArrayList _objects;

			// Token: 0x04001510 RID: 5392
			private bool _isKeys;
		}
	}
}

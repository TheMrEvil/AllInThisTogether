using System;
using System.Globalization;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Threading;
using Unity;

namespace System.Collections.Specialized
{
	/// <summary>Provides the <see langword="abstract" /> base class for a collection of associated <see cref="T:System.String" /> keys and <see cref="T:System.Object" /> values that can be accessed either with the key or with the index.</summary>
	// Token: 0x020004B9 RID: 1209
	[Serializable]
	public abstract class NameObjectCollectionBase : ICollection, IEnumerable, ISerializable, IDeserializationCallback
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Specialized.NameObjectCollectionBase" /> class that is empty.</summary>
		// Token: 0x0600271C RID: 10012 RVA: 0x00087BD9 File Offset: 0x00085DD9
		protected NameObjectCollectionBase() : this(NameObjectCollectionBase.defaultComparer)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Specialized.NameObjectCollectionBase" /> class that is empty, has the default initial capacity, and uses the specified <see cref="T:System.Collections.IEqualityComparer" /> object.</summary>
		/// <param name="equalityComparer">The <see cref="T:System.Collections.IEqualityComparer" /> object to use to determine whether two keys are equal and to generate hash codes for the keys in the collection.</param>
		// Token: 0x0600271D RID: 10013 RVA: 0x00087BE8 File Offset: 0x00085DE8
		protected NameObjectCollectionBase(IEqualityComparer equalityComparer)
		{
			IEqualityComparer keyComparer;
			if (equalityComparer != null)
			{
				keyComparer = equalityComparer;
			}
			else
			{
				IEqualityComparer equalityComparer2 = NameObjectCollectionBase.defaultComparer;
				keyComparer = equalityComparer2;
			}
			this._keyComparer = keyComparer;
			this.Reset();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Specialized.NameObjectCollectionBase" /> class that is empty, has the specified initial capacity, and uses the specified <see cref="T:System.Collections.IEqualityComparer" /> object.</summary>
		/// <param name="capacity">The approximate number of entries that the <see cref="T:System.Collections.Specialized.NameObjectCollectionBase" /> object can initially contain.</param>
		/// <param name="equalityComparer">The <see cref="T:System.Collections.IEqualityComparer" /> object to use to determine whether two keys are equal and to generate hash codes for the keys in the collection.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="capacity" /> is less than zero.</exception>
		// Token: 0x0600271E RID: 10014 RVA: 0x00087C14 File Offset: 0x00085E14
		protected NameObjectCollectionBase(int capacity, IEqualityComparer equalityComparer) : this(equalityComparer)
		{
			this.Reset(capacity);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Specialized.NameObjectCollectionBase" /> class that is empty, has the default initial capacity, and uses the specified hash code provider and the specified comparer.</summary>
		/// <param name="hashProvider">The <see cref="T:System.Collections.IHashCodeProvider" /> that will supply the hash codes for all keys in the <see cref="T:System.Collections.Specialized.NameObjectCollectionBase" /> instance.</param>
		/// <param name="comparer">The <see cref="T:System.Collections.IComparer" /> to use to determine whether two keys are equal.</param>
		// Token: 0x0600271F RID: 10015 RVA: 0x00087C24 File Offset: 0x00085E24
		[Obsolete("Please use NameObjectCollectionBase(IEqualityComparer) instead.")]
		protected NameObjectCollectionBase(IHashCodeProvider hashProvider, IComparer comparer)
		{
			this._keyComparer = new CompatibleComparer(comparer, hashProvider);
			this.Reset();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Specialized.NameObjectCollectionBase" /> class that is empty, has the specified initial capacity and uses the specified hash code provider and the specified comparer.</summary>
		/// <param name="capacity">The approximate number of entries that the <see cref="T:System.Collections.Specialized.NameObjectCollectionBase" /> instance can initially contain.</param>
		/// <param name="hashProvider">The <see cref="T:System.Collections.IHashCodeProvider" /> that will supply the hash codes for all keys in the <see cref="T:System.Collections.Specialized.NameObjectCollectionBase" /> instance.</param>
		/// <param name="comparer">The <see cref="T:System.Collections.IComparer" /> to use to determine whether two keys are equal.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="capacity" /> is less than zero.</exception>
		// Token: 0x06002720 RID: 10016 RVA: 0x00087C3F File Offset: 0x00085E3F
		[Obsolete("Please use NameObjectCollectionBase(Int32, IEqualityComparer) instead.")]
		protected NameObjectCollectionBase(int capacity, IHashCodeProvider hashProvider, IComparer comparer)
		{
			this._keyComparer = new CompatibleComparer(comparer, hashProvider);
			this.Reset(capacity);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Specialized.NameObjectCollectionBase" /> class that is empty, has the specified initial capacity, and uses the default hash code provider and the default comparer.</summary>
		/// <param name="capacity">The approximate number of entries that the <see cref="T:System.Collections.Specialized.NameObjectCollectionBase" /> instance can initially contain.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="capacity" /> is less than zero.</exception>
		// Token: 0x06002721 RID: 10017 RVA: 0x00087C5B File Offset: 0x00085E5B
		protected NameObjectCollectionBase(int capacity)
		{
			this._keyComparer = StringComparer.InvariantCultureIgnoreCase;
			this.Reset(capacity);
		}

		// Token: 0x06002722 RID: 10018 RVA: 0x0000219B File Offset: 0x0000039B
		internal NameObjectCollectionBase(DBNull dummy)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Specialized.NameObjectCollectionBase" /> class that is serializable and uses the specified <see cref="T:System.Runtime.Serialization.SerializationInfo" /> and <see cref="T:System.Runtime.Serialization.StreamingContext" />.</summary>
		/// <param name="info">A <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object that contains the information required to serialize the new <see cref="T:System.Collections.Specialized.NameObjectCollectionBase" /> instance.</param>
		/// <param name="context">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> object that contains the source and destination of the serialized stream associated with the new <see cref="T:System.Collections.Specialized.NameObjectCollectionBase" /> instance.</param>
		// Token: 0x06002723 RID: 10019 RVA: 0x00087C75 File Offset: 0x00085E75
		protected NameObjectCollectionBase(SerializationInfo info, StreamingContext context)
		{
			this._serializationInfo = info;
		}

		/// <summary>Implements the <see cref="T:System.Runtime.Serialization.ISerializable" /> interface and returns the data needed to serialize the <see cref="T:System.Collections.Specialized.NameObjectCollectionBase" /> instance.</summary>
		/// <param name="info">A <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object that contains the information required to serialize the <see cref="T:System.Collections.Specialized.NameObjectCollectionBase" /> instance.</param>
		/// <param name="context">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> object that contains the source and destination of the serialized stream associated with the <see cref="T:System.Collections.Specialized.NameObjectCollectionBase" /> instance.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="info" /> is <see langword="null" />.</exception>
		// Token: 0x06002724 RID: 10020 RVA: 0x00087C84 File Offset: 0x00085E84
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			info.AddValue("ReadOnly", this._readOnly);
			if (this._keyComparer == NameObjectCollectionBase.defaultComparer)
			{
				info.AddValue("HashProvider", CompatibleComparer.DefaultHashCodeProvider, typeof(IHashCodeProvider));
				info.AddValue("Comparer", CompatibleComparer.DefaultComparer, typeof(IComparer));
			}
			else if (this._keyComparer == null)
			{
				info.AddValue("HashProvider", null, typeof(IHashCodeProvider));
				info.AddValue("Comparer", null, typeof(IComparer));
			}
			else if (this._keyComparer is CompatibleComparer)
			{
				CompatibleComparer compatibleComparer = (CompatibleComparer)this._keyComparer;
				info.AddValue("HashProvider", compatibleComparer.HashCodeProvider, typeof(IHashCodeProvider));
				info.AddValue("Comparer", compatibleComparer.Comparer, typeof(IComparer));
			}
			else
			{
				info.AddValue("KeyComparer", this._keyComparer, typeof(IEqualityComparer));
			}
			int count = this._entriesArray.Count;
			info.AddValue("Count", count);
			string[] array = new string[count];
			object[] array2 = new object[count];
			for (int i = 0; i < count; i++)
			{
				NameObjectCollectionBase.NameObjectEntry nameObjectEntry = (NameObjectCollectionBase.NameObjectEntry)this._entriesArray[i];
				array[i] = nameObjectEntry.Key;
				array2[i] = nameObjectEntry.Value;
			}
			info.AddValue("Keys", array, typeof(string[]));
			info.AddValue("Values", array2, typeof(object[]));
			info.AddValue("Version", this._version);
		}

		/// <summary>Implements the <see cref="T:System.Runtime.Serialization.ISerializable" /> interface and raises the deserialization event when the deserialization is complete.</summary>
		/// <param name="sender">The source of the deserialization event.</param>
		/// <exception cref="T:System.Runtime.Serialization.SerializationException">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object associated with the current <see cref="T:System.Collections.Specialized.NameObjectCollectionBase" /> instance is invalid.</exception>
		// Token: 0x06002725 RID: 10021 RVA: 0x00087E38 File Offset: 0x00086038
		public virtual void OnDeserialization(object sender)
		{
			if (this._keyComparer != null)
			{
				return;
			}
			if (this._serializationInfo == null)
			{
				throw new SerializationException();
			}
			SerializationInfo serializationInfo = this._serializationInfo;
			this._serializationInfo = null;
			bool readOnly = false;
			int num = 0;
			string[] array = null;
			object[] array2 = null;
			IHashCodeProvider hashCodeProvider = null;
			IComparer comparer = null;
			bool flag = false;
			int version = 0;
			SerializationInfoEnumerator enumerator = serializationInfo.GetEnumerator();
			while (enumerator.MoveNext())
			{
				string name = enumerator.Name;
				uint num2 = <PrivateImplementationDetails>.ComputeStringHash(name);
				if (num2 <= 1573770551U)
				{
					if (num2 <= 1202781175U)
					{
						if (num2 != 891156946U)
						{
							if (num2 == 1202781175U)
							{
								if (name == "ReadOnly")
								{
									readOnly = serializationInfo.GetBoolean("ReadOnly");
								}
							}
						}
						else if (name == "Comparer")
						{
							comparer = (IComparer)serializationInfo.GetValue("Comparer", typeof(IComparer));
						}
					}
					else if (num2 != 1228509323U)
					{
						if (num2 == 1573770551U)
						{
							if (name == "Version")
							{
								flag = true;
								version = serializationInfo.GetInt32("Version");
							}
						}
					}
					else if (name == "KeyComparer")
					{
						this._keyComparer = (IEqualityComparer)serializationInfo.GetValue("KeyComparer", typeof(IEqualityComparer));
					}
				}
				else if (num2 <= 1944240600U)
				{
					if (num2 != 1613443821U)
					{
						if (num2 == 1944240600U)
						{
							if (name == "HashProvider")
							{
								hashCodeProvider = (IHashCodeProvider)serializationInfo.GetValue("HashProvider", typeof(IHashCodeProvider));
							}
						}
					}
					else if (name == "Keys")
					{
						array = (string[])serializationInfo.GetValue("Keys", typeof(string[]));
					}
				}
				else if (num2 != 2370642523U)
				{
					if (num2 == 3790059668U)
					{
						if (name == "Count")
						{
							num = serializationInfo.GetInt32("Count");
						}
					}
				}
				else if (name == "Values")
				{
					array2 = (object[])serializationInfo.GetValue("Values", typeof(object[]));
				}
			}
			if (this._keyComparer == null)
			{
				if (comparer == null || hashCodeProvider == null)
				{
					throw new SerializationException();
				}
				this._keyComparer = new CompatibleComparer(comparer, hashCodeProvider);
			}
			if (array == null || array2 == null)
			{
				throw new SerializationException();
			}
			this.Reset(num);
			for (int i = 0; i < num; i++)
			{
				this.BaseAdd(array[i], array2[i]);
			}
			this._readOnly = readOnly;
			if (flag)
			{
				this._version = version;
			}
		}

		// Token: 0x06002726 RID: 10022 RVA: 0x00088116 File Offset: 0x00086316
		private void Reset()
		{
			this._entriesArray = new ArrayList();
			this._entriesTable = new Hashtable(this._keyComparer);
			this._nullKeyEntry = null;
			this._version++;
		}

		// Token: 0x06002727 RID: 10023 RVA: 0x0008814D File Offset: 0x0008634D
		private void Reset(int capacity)
		{
			this._entriesArray = new ArrayList(capacity);
			this._entriesTable = new Hashtable(capacity, this._keyComparer);
			this._nullKeyEntry = null;
			this._version++;
		}

		// Token: 0x06002728 RID: 10024 RVA: 0x00088186 File Offset: 0x00086386
		private NameObjectCollectionBase.NameObjectEntry FindEntry(string key)
		{
			if (key != null)
			{
				return (NameObjectCollectionBase.NameObjectEntry)this._entriesTable[key];
			}
			return this._nullKeyEntry;
		}

		// Token: 0x170007F6 RID: 2038
		// (get) Token: 0x06002729 RID: 10025 RVA: 0x000881A7 File Offset: 0x000863A7
		// (set) Token: 0x0600272A RID: 10026 RVA: 0x000881AF File Offset: 0x000863AF
		internal IEqualityComparer Comparer
		{
			get
			{
				return this._keyComparer;
			}
			set
			{
				this._keyComparer = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the <see cref="T:System.Collections.Specialized.NameObjectCollectionBase" /> instance is read-only.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Collections.Specialized.NameObjectCollectionBase" /> instance is read-only; otherwise, <see langword="false" />.</returns>
		// Token: 0x170007F7 RID: 2039
		// (get) Token: 0x0600272B RID: 10027 RVA: 0x000881B8 File Offset: 0x000863B8
		// (set) Token: 0x0600272C RID: 10028 RVA: 0x000881C0 File Offset: 0x000863C0
		protected bool IsReadOnly
		{
			get
			{
				return this._readOnly;
			}
			set
			{
				this._readOnly = value;
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Collections.Specialized.NameObjectCollectionBase" /> instance contains entries whose keys are not <see langword="null" />.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Collections.Specialized.NameObjectCollectionBase" /> instance contains entries whose keys are not <see langword="null" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x0600272D RID: 10029 RVA: 0x000881C9 File Offset: 0x000863C9
		protected bool BaseHasKeys()
		{
			return this._entriesTable.Count > 0;
		}

		/// <summary>Adds an entry with the specified key and value into the <see cref="T:System.Collections.Specialized.NameObjectCollectionBase" /> instance.</summary>
		/// <param name="name">The <see cref="T:System.String" /> key of the entry to add. The key can be <see langword="null" />.</param>
		/// <param name="value">The <see cref="T:System.Object" /> value of the entry to add. The value can be <see langword="null" />.</param>
		/// <exception cref="T:System.NotSupportedException">The collection is read-only.</exception>
		// Token: 0x0600272E RID: 10030 RVA: 0x000881DC File Offset: 0x000863DC
		protected void BaseAdd(string name, object value)
		{
			if (this._readOnly)
			{
				throw new NotSupportedException(SR.GetString("Collection is read-only."));
			}
			NameObjectCollectionBase.NameObjectEntry nameObjectEntry = new NameObjectCollectionBase.NameObjectEntry(name, value);
			if (name != null)
			{
				if (this._entriesTable[name] == null)
				{
					this._entriesTable.Add(name, nameObjectEntry);
				}
			}
			else if (this._nullKeyEntry == null)
			{
				this._nullKeyEntry = nameObjectEntry;
			}
			this._entriesArray.Add(nameObjectEntry);
			this._version++;
		}

		/// <summary>Removes the entries with the specified key from the <see cref="T:System.Collections.Specialized.NameObjectCollectionBase" /> instance.</summary>
		/// <param name="name">The <see cref="T:System.String" /> key of the entries to remove. The key can be <see langword="null" />.</param>
		/// <exception cref="T:System.NotSupportedException">The collection is read-only.</exception>
		// Token: 0x0600272F RID: 10031 RVA: 0x0008825C File Offset: 0x0008645C
		protected void BaseRemove(string name)
		{
			if (this._readOnly)
			{
				throw new NotSupportedException(SR.GetString("Collection is read-only."));
			}
			if (name != null)
			{
				this._entriesTable.Remove(name);
				for (int i = this._entriesArray.Count - 1; i >= 0; i--)
				{
					if (this._keyComparer.Equals(name, this.BaseGetKey(i)))
					{
						this._entriesArray.RemoveAt(i);
					}
				}
			}
			else
			{
				this._nullKeyEntry = null;
				for (int j = this._entriesArray.Count - 1; j >= 0; j--)
				{
					if (this.BaseGetKey(j) == null)
					{
						this._entriesArray.RemoveAt(j);
					}
				}
			}
			this._version++;
		}

		/// <summary>Removes the entry at the specified index of the <see cref="T:System.Collections.Specialized.NameObjectCollectionBase" /> instance.</summary>
		/// <param name="index">The zero-based index of the entry to remove.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is outside the valid range of indexes for the collection.</exception>
		/// <exception cref="T:System.NotSupportedException">The collection is read-only.</exception>
		// Token: 0x06002730 RID: 10032 RVA: 0x00088314 File Offset: 0x00086514
		protected void BaseRemoveAt(int index)
		{
			if (this._readOnly)
			{
				throw new NotSupportedException(SR.GetString("Collection is read-only."));
			}
			string text = this.BaseGetKey(index);
			if (text != null)
			{
				this._entriesTable.Remove(text);
			}
			else
			{
				this._nullKeyEntry = null;
			}
			this._entriesArray.RemoveAt(index);
			this._version++;
		}

		/// <summary>Removes all entries from the <see cref="T:System.Collections.Specialized.NameObjectCollectionBase" /> instance.</summary>
		/// <exception cref="T:System.NotSupportedException">The collection is read-only.</exception>
		// Token: 0x06002731 RID: 10033 RVA: 0x00088377 File Offset: 0x00086577
		protected void BaseClear()
		{
			if (this._readOnly)
			{
				throw new NotSupportedException(SR.GetString("Collection is read-only."));
			}
			this.Reset();
		}

		/// <summary>Gets the value of the first entry with the specified key from the <see cref="T:System.Collections.Specialized.NameObjectCollectionBase" /> instance.</summary>
		/// <param name="name">The <see cref="T:System.String" /> key of the entry to get. The key can be <see langword="null" />.</param>
		/// <returns>An <see cref="T:System.Object" /> that represents the value of the first entry with the specified key, if found; otherwise, <see langword="null" />.</returns>
		// Token: 0x06002732 RID: 10034 RVA: 0x00088398 File Offset: 0x00086598
		protected object BaseGet(string name)
		{
			NameObjectCollectionBase.NameObjectEntry nameObjectEntry = this.FindEntry(name);
			if (nameObjectEntry == null)
			{
				return null;
			}
			return nameObjectEntry.Value;
		}

		/// <summary>Sets the value of the first entry with the specified key in the <see cref="T:System.Collections.Specialized.NameObjectCollectionBase" /> instance, if found; otherwise, adds an entry with the specified key and value into the <see cref="T:System.Collections.Specialized.NameObjectCollectionBase" /> instance.</summary>
		/// <param name="name">The <see cref="T:System.String" /> key of the entry to set. The key can be <see langword="null" />.</param>
		/// <param name="value">The <see cref="T:System.Object" /> that represents the new value of the entry to set. The value can be <see langword="null" />.</param>
		/// <exception cref="T:System.NotSupportedException">The collection is read-only.</exception>
		// Token: 0x06002733 RID: 10035 RVA: 0x000883B8 File Offset: 0x000865B8
		protected void BaseSet(string name, object value)
		{
			if (this._readOnly)
			{
				throw new NotSupportedException(SR.GetString("Collection is read-only."));
			}
			NameObjectCollectionBase.NameObjectEntry nameObjectEntry = this.FindEntry(name);
			if (nameObjectEntry != null)
			{
				nameObjectEntry.Value = value;
				this._version++;
				return;
			}
			this.BaseAdd(name, value);
		}

		/// <summary>Gets the value of the entry at the specified index of the <see cref="T:System.Collections.Specialized.NameObjectCollectionBase" /> instance.</summary>
		/// <param name="index">The zero-based index of the value to get.</param>
		/// <returns>An <see cref="T:System.Object" /> that represents the value of the entry at the specified index.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is outside the valid range of indexes for the collection.</exception>
		// Token: 0x06002734 RID: 10036 RVA: 0x00088406 File Offset: 0x00086606
		protected object BaseGet(int index)
		{
			return ((NameObjectCollectionBase.NameObjectEntry)this._entriesArray[index]).Value;
		}

		/// <summary>Gets the key of the entry at the specified index of the <see cref="T:System.Collections.Specialized.NameObjectCollectionBase" /> instance.</summary>
		/// <param name="index">The zero-based index of the key to get.</param>
		/// <returns>A <see cref="T:System.String" /> that represents the key of the entry at the specified index.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is outside the valid range of indexes for the collection.</exception>
		// Token: 0x06002735 RID: 10037 RVA: 0x0008841E File Offset: 0x0008661E
		protected string BaseGetKey(int index)
		{
			return ((NameObjectCollectionBase.NameObjectEntry)this._entriesArray[index]).Key;
		}

		/// <summary>Sets the value of the entry at the specified index of the <see cref="T:System.Collections.Specialized.NameObjectCollectionBase" /> instance.</summary>
		/// <param name="index">The zero-based index of the entry to set.</param>
		/// <param name="value">The <see cref="T:System.Object" /> that represents the new value of the entry to set. The value can be <see langword="null" />.</param>
		/// <exception cref="T:System.NotSupportedException">The collection is read-only.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is outside the valid range of indexes for the collection.</exception>
		// Token: 0x06002736 RID: 10038 RVA: 0x00088436 File Offset: 0x00086636
		protected void BaseSet(int index, object value)
		{
			if (this._readOnly)
			{
				throw new NotSupportedException(SR.GetString("Collection is read-only."));
			}
			((NameObjectCollectionBase.NameObjectEntry)this._entriesArray[index]).Value = value;
			this._version++;
		}

		/// <summary>Returns an enumerator that iterates through the <see cref="T:System.Collections.Specialized.NameObjectCollectionBase" />.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> for the <see cref="T:System.Collections.Specialized.NameObjectCollectionBase" /> instance.</returns>
		// Token: 0x06002737 RID: 10039 RVA: 0x00088475 File Offset: 0x00086675
		public virtual IEnumerator GetEnumerator()
		{
			return new NameObjectCollectionBase.NameObjectKeysEnumerator(this);
		}

		/// <summary>Gets the number of key/value pairs contained in the <see cref="T:System.Collections.Specialized.NameObjectCollectionBase" /> instance.</summary>
		/// <returns>The number of key/value pairs contained in the <see cref="T:System.Collections.Specialized.NameObjectCollectionBase" /> instance.</returns>
		// Token: 0x170007F8 RID: 2040
		// (get) Token: 0x06002738 RID: 10040 RVA: 0x0008847D File Offset: 0x0008667D
		public virtual int Count
		{
			get
			{
				return this._entriesArray.Count;
			}
		}

		/// <summary>Copies the entire <see cref="T:System.Collections.Specialized.NameObjectCollectionBase" /> to a compatible one-dimensional <see cref="T:System.Array" />, starting at the specified index of the target array.</summary>
		/// <param name="array">The one-dimensional <see cref="T:System.Array" /> that is the destination of the elements copied from <see cref="T:System.Collections.Specialized.NameObjectCollectionBase" />. The <see cref="T:System.Array" /> must have zero-based indexing.</param>
		/// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="array" /> is multidimensional.  
		/// -or-  
		/// The number of elements in the source <see cref="T:System.Collections.Specialized.NameObjectCollectionBase" /> is greater than the available space from <paramref name="index" /> to the end of the destination <paramref name="array" />.</exception>
		/// <exception cref="T:System.InvalidCastException">The type of the source <see cref="T:System.Collections.Specialized.NameObjectCollectionBase" /> cannot be cast automatically to the type of the destination <paramref name="array" />.</exception>
		// Token: 0x06002739 RID: 10041 RVA: 0x0008848C File Offset: 0x0008668C
		void ICollection.CopyTo(Array array, int index)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (array.Rank != 1)
			{
				throw new ArgumentException(SR.GetString("Multi dimension array is not supported on this operation."));
			}
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index", SR.GetString("Index {0} is out of range.", new object[]
				{
					index.ToString(CultureInfo.CurrentCulture)
				}));
			}
			if (array.Length - index < this._entriesArray.Count)
			{
				throw new ArgumentException(SR.GetString("Insufficient space in the target location to copy the information."));
			}
			foreach (object value in this)
			{
				array.SetValue(value, index++);
			}
		}

		/// <summary>Gets an object that can be used to synchronize access to the <see cref="T:System.Collections.Specialized.NameObjectCollectionBase" /> object.</summary>
		/// <returns>An object that can be used to synchronize access to the <see cref="T:System.Collections.Specialized.NameObjectCollectionBase" /> object.</returns>
		// Token: 0x170007F9 RID: 2041
		// (get) Token: 0x0600273A RID: 10042 RVA: 0x00088536 File Offset: 0x00086736
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

		/// <summary>Gets a value indicating whether access to the <see cref="T:System.Collections.Specialized.NameObjectCollectionBase" /> object is synchronized (thread safe).</summary>
		/// <returns>
		///   <see langword="true" /> if access to the <see cref="T:System.Collections.Specialized.NameObjectCollectionBase" /> object is synchronized (thread safe); otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		// Token: 0x170007FA RID: 2042
		// (get) Token: 0x0600273B RID: 10043 RVA: 0x00003062 File Offset: 0x00001262
		bool ICollection.IsSynchronized
		{
			get
			{
				return false;
			}
		}

		/// <summary>Returns a <see cref="T:System.String" /> array that contains all the keys in the <see cref="T:System.Collections.Specialized.NameObjectCollectionBase" /> instance.</summary>
		/// <returns>A <see cref="T:System.String" /> array that contains all the keys in the <see cref="T:System.Collections.Specialized.NameObjectCollectionBase" /> instance.</returns>
		// Token: 0x0600273C RID: 10044 RVA: 0x00088558 File Offset: 0x00086758
		protected string[] BaseGetAllKeys()
		{
			int count = this._entriesArray.Count;
			string[] array = new string[count];
			for (int i = 0; i < count; i++)
			{
				array[i] = this.BaseGetKey(i);
			}
			return array;
		}

		/// <summary>Returns an <see cref="T:System.Object" /> array that contains all the values in the <see cref="T:System.Collections.Specialized.NameObjectCollectionBase" /> instance.</summary>
		/// <returns>An <see cref="T:System.Object" /> array that contains all the values in the <see cref="T:System.Collections.Specialized.NameObjectCollectionBase" /> instance.</returns>
		// Token: 0x0600273D RID: 10045 RVA: 0x00088590 File Offset: 0x00086790
		protected object[] BaseGetAllValues()
		{
			int count = this._entriesArray.Count;
			object[] array = new object[count];
			for (int i = 0; i < count; i++)
			{
				array[i] = this.BaseGet(i);
			}
			return array;
		}

		/// <summary>Returns an array of the specified type that contains all the values in the <see cref="T:System.Collections.Specialized.NameObjectCollectionBase" /> instance.</summary>
		/// <param name="type">A <see cref="T:System.Type" /> that represents the type of array to return.</param>
		/// <returns>An array of the specified type that contains all the values in the <see cref="T:System.Collections.Specialized.NameObjectCollectionBase" /> instance.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="type" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="type" /> is not a valid <see cref="T:System.Type" />.</exception>
		// Token: 0x0600273E RID: 10046 RVA: 0x000885C8 File Offset: 0x000867C8
		protected object[] BaseGetAllValues(Type type)
		{
			int count = this._entriesArray.Count;
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			object[] array = (object[])SecurityUtils.ArrayCreateInstance(type, count);
			for (int i = 0; i < count; i++)
			{
				array[i] = this.BaseGet(i);
			}
			return array;
		}

		/// <summary>Gets a <see cref="T:System.Collections.Specialized.NameObjectCollectionBase.KeysCollection" /> instance that contains all the keys in the <see cref="T:System.Collections.Specialized.NameObjectCollectionBase" /> instance.</summary>
		/// <returns>A <see cref="T:System.Collections.Specialized.NameObjectCollectionBase.KeysCollection" /> instance that contains all the keys in the <see cref="T:System.Collections.Specialized.NameObjectCollectionBase" /> instance.</returns>
		// Token: 0x170007FB RID: 2043
		// (get) Token: 0x0600273F RID: 10047 RVA: 0x00088619 File Offset: 0x00086819
		public virtual NameObjectCollectionBase.KeysCollection Keys
		{
			get
			{
				if (this._keys == null)
				{
					this._keys = new NameObjectCollectionBase.KeysCollection(this);
				}
				return this._keys;
			}
		}

		// Token: 0x06002740 RID: 10048 RVA: 0x00088635 File Offset: 0x00086835
		// Note: this type is marked as 'beforefieldinit'.
		static NameObjectCollectionBase()
		{
		}

		// Token: 0x04001521 RID: 5409
		private const string ReadOnlyName = "ReadOnly";

		// Token: 0x04001522 RID: 5410
		private const string CountName = "Count";

		// Token: 0x04001523 RID: 5411
		private const string ComparerName = "Comparer";

		// Token: 0x04001524 RID: 5412
		private const string HashCodeProviderName = "HashProvider";

		// Token: 0x04001525 RID: 5413
		private const string KeysName = "Keys";

		// Token: 0x04001526 RID: 5414
		private const string ValuesName = "Values";

		// Token: 0x04001527 RID: 5415
		private const string KeyComparerName = "KeyComparer";

		// Token: 0x04001528 RID: 5416
		private const string VersionName = "Version";

		// Token: 0x04001529 RID: 5417
		private bool _readOnly;

		// Token: 0x0400152A RID: 5418
		private ArrayList _entriesArray;

		// Token: 0x0400152B RID: 5419
		private IEqualityComparer _keyComparer;

		// Token: 0x0400152C RID: 5420
		private volatile Hashtable _entriesTable;

		// Token: 0x0400152D RID: 5421
		private volatile NameObjectCollectionBase.NameObjectEntry _nullKeyEntry;

		// Token: 0x0400152E RID: 5422
		private NameObjectCollectionBase.KeysCollection _keys;

		// Token: 0x0400152F RID: 5423
		private SerializationInfo _serializationInfo;

		// Token: 0x04001530 RID: 5424
		private int _version;

		// Token: 0x04001531 RID: 5425
		[NonSerialized]
		private object _syncRoot;

		// Token: 0x04001532 RID: 5426
		private static StringComparer defaultComparer = StringComparer.InvariantCultureIgnoreCase;

		// Token: 0x020004BA RID: 1210
		internal class NameObjectEntry
		{
			// Token: 0x06002741 RID: 10049 RVA: 0x00088641 File Offset: 0x00086841
			internal NameObjectEntry(string name, object value)
			{
				this.Key = name;
				this.Value = value;
			}

			// Token: 0x04001533 RID: 5427
			internal string Key;

			// Token: 0x04001534 RID: 5428
			internal object Value;
		}

		// Token: 0x020004BB RID: 1211
		[Serializable]
		internal class NameObjectKeysEnumerator : IEnumerator
		{
			// Token: 0x06002742 RID: 10050 RVA: 0x00088657 File Offset: 0x00086857
			internal NameObjectKeysEnumerator(NameObjectCollectionBase coll)
			{
				this._coll = coll;
				this._version = this._coll._version;
				this._pos = -1;
			}

			// Token: 0x06002743 RID: 10051 RVA: 0x00088680 File Offset: 0x00086880
			public bool MoveNext()
			{
				if (this._version != this._coll._version)
				{
					throw new InvalidOperationException(SR.GetString("Collection was modified; enumeration operation may not execute."));
				}
				if (this._pos < this._coll.Count - 1)
				{
					this._pos++;
					return true;
				}
				this._pos = this._coll.Count;
				return false;
			}

			// Token: 0x06002744 RID: 10052 RVA: 0x000886E7 File Offset: 0x000868E7
			public void Reset()
			{
				if (this._version != this._coll._version)
				{
					throw new InvalidOperationException(SR.GetString("Collection was modified; enumeration operation may not execute."));
				}
				this._pos = -1;
			}

			// Token: 0x170007FC RID: 2044
			// (get) Token: 0x06002745 RID: 10053 RVA: 0x00088713 File Offset: 0x00086913
			public object Current
			{
				get
				{
					if (this._pos >= 0 && this._pos < this._coll.Count)
					{
						return this._coll.BaseGetKey(this._pos);
					}
					throw new InvalidOperationException(SR.GetString("Enumeration has either not started or has already finished."));
				}
			}

			// Token: 0x04001535 RID: 5429
			private int _pos;

			// Token: 0x04001536 RID: 5430
			private NameObjectCollectionBase _coll;

			// Token: 0x04001537 RID: 5431
			private int _version;
		}

		/// <summary>Represents a collection of the <see cref="T:System.String" /> keys of a collection.</summary>
		// Token: 0x020004BC RID: 1212
		[Serializable]
		public class KeysCollection : ICollection, IEnumerable
		{
			// Token: 0x06002746 RID: 10054 RVA: 0x00088752 File Offset: 0x00086952
			internal KeysCollection(NameObjectCollectionBase coll)
			{
				this._coll = coll;
			}

			/// <summary>Gets the key at the specified index of the collection.</summary>
			/// <param name="index">The zero-based index of the key to get from the collection.</param>
			/// <returns>A <see cref="T:System.String" /> that contains the key at the specified index of the collection.</returns>
			/// <exception cref="T:System.ArgumentOutOfRangeException">
			///   <paramref name="index" /> is outside the valid range of indexes for the collection.</exception>
			// Token: 0x06002747 RID: 10055 RVA: 0x00088761 File Offset: 0x00086961
			public virtual string Get(int index)
			{
				return this._coll.BaseGetKey(index);
			}

			/// <summary>Gets the entry at the specified index of the collection.</summary>
			/// <param name="index">The zero-based index of the entry to locate in the collection.</param>
			/// <returns>The <see cref="T:System.String" /> key of the entry at the specified index of the collection.</returns>
			/// <exception cref="T:System.ArgumentOutOfRangeException">
			///   <paramref name="index" /> is outside the valid range of indexes for the collection.</exception>
			// Token: 0x170007FD RID: 2045
			public string this[int index]
			{
				get
				{
					return this.Get(index);
				}
			}

			/// <summary>Returns an enumerator that iterates through the <see cref="T:System.Collections.Specialized.NameObjectCollectionBase.KeysCollection" />.</summary>
			/// <returns>An <see cref="T:System.Collections.IEnumerator" /> for the <see cref="T:System.Collections.Specialized.NameObjectCollectionBase.KeysCollection" />.</returns>
			// Token: 0x06002749 RID: 10057 RVA: 0x00088778 File Offset: 0x00086978
			public IEnumerator GetEnumerator()
			{
				return new NameObjectCollectionBase.NameObjectKeysEnumerator(this._coll);
			}

			/// <summary>Gets the number of keys in the <see cref="T:System.Collections.Specialized.NameObjectCollectionBase.KeysCollection" />.</summary>
			/// <returns>The number of keys in the <see cref="T:System.Collections.Specialized.NameObjectCollectionBase.KeysCollection" />.</returns>
			// Token: 0x170007FE RID: 2046
			// (get) Token: 0x0600274A RID: 10058 RVA: 0x00088785 File Offset: 0x00086985
			public int Count
			{
				get
				{
					return this._coll.Count;
				}
			}

			/// <summary>Copies the entire <see cref="T:System.Collections.Specialized.NameObjectCollectionBase.KeysCollection" /> to a compatible one-dimensional <see cref="T:System.Array" />, starting at the specified index of the target array.</summary>
			/// <param name="array">The one-dimensional <see cref="T:System.Array" /> that is the destination of the elements copied from <see cref="T:System.Collections.Specialized.NameObjectCollectionBase.KeysCollection" />. The <see cref="T:System.Array" /> must have zero-based indexing.</param>
			/// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins.</param>
			/// <exception cref="T:System.ArgumentNullException">
			///   <paramref name="array" /> is <see langword="null" />.</exception>
			/// <exception cref="T:System.ArgumentOutOfRangeException">
			///   <paramref name="index" /> is less than zero.</exception>
			/// <exception cref="T:System.ArgumentException">
			///   <paramref name="array" /> is multidimensional.  
			/// -or-  
			/// The number of elements in the source <see cref="T:System.Collections.Specialized.NameObjectCollectionBase.KeysCollection" /> is greater than the available space from <paramref name="index" /> to the end of the destination <paramref name="array" />.</exception>
			/// <exception cref="T:System.InvalidCastException">The type of the source <see cref="T:System.Collections.Specialized.NameObjectCollectionBase.KeysCollection" /> cannot be cast automatically to the type of the destination <paramref name="array" />.</exception>
			// Token: 0x0600274B RID: 10059 RVA: 0x00088794 File Offset: 0x00086994
			void ICollection.CopyTo(Array array, int index)
			{
				if (array == null)
				{
					throw new ArgumentNullException("array");
				}
				if (array.Rank != 1)
				{
					throw new ArgumentException(SR.GetString("Multi dimension array is not supported on this operation."));
				}
				if (index < 0)
				{
					throw new ArgumentOutOfRangeException("index", SR.GetString("Index {0} is out of range.", new object[]
					{
						index.ToString(CultureInfo.CurrentCulture)
					}));
				}
				if (array.Length - index < this._coll.Count)
				{
					throw new ArgumentException(SR.GetString("Insufficient space in the target location to copy the information."));
				}
				foreach (object value in this)
				{
					array.SetValue(value, index++);
				}
			}

			/// <summary>Gets an object that can be used to synchronize access to the <see cref="T:System.Collections.Specialized.NameObjectCollectionBase.KeysCollection" />.</summary>
			/// <returns>An object that can be used to synchronize access to the <see cref="T:System.Collections.Specialized.NameObjectCollectionBase.KeysCollection" />.</returns>
			// Token: 0x170007FF RID: 2047
			// (get) Token: 0x0600274C RID: 10060 RVA: 0x0008883E File Offset: 0x00086A3E
			object ICollection.SyncRoot
			{
				get
				{
					return ((ICollection)this._coll).SyncRoot;
				}
			}

			/// <summary>Gets a value indicating whether access to the <see cref="T:System.Collections.Specialized.NameObjectCollectionBase.KeysCollection" /> is synchronized (thread safe).</summary>
			/// <returns>
			///   <see langword="true" /> if access to the <see cref="T:System.Collections.Specialized.NameObjectCollectionBase.KeysCollection" /> is synchronized (thread safe); otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
			// Token: 0x17000800 RID: 2048
			// (get) Token: 0x0600274D RID: 10061 RVA: 0x00003062 File Offset: 0x00001262
			bool ICollection.IsSynchronized
			{
				get
				{
					return false;
				}
			}

			// Token: 0x0600274E RID: 10062 RVA: 0x00013BCA File Offset: 0x00011DCA
			internal KeysCollection()
			{
				ThrowStub.ThrowNotSupportedException();
			}

			// Token: 0x04001538 RID: 5432
			private NameObjectCollectionBase _coll;
		}
	}
}

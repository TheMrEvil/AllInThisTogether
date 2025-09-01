using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace System.ComponentModel
{
	/// <summary>Represents a collection of <see cref="T:System.ComponentModel.PropertyDescriptor" /> objects.</summary>
	// Token: 0x020003E0 RID: 992
	public class PropertyDescriptorCollection : ICollection, IEnumerable, IList, IDictionary
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.PropertyDescriptorCollection" /> class.</summary>
		/// <param name="properties">An array of type <see cref="T:System.ComponentModel.PropertyDescriptor" /> that provides the properties for this collection.</param>
		// Token: 0x0600206A RID: 8298 RVA: 0x00070284 File Offset: 0x0006E484
		public PropertyDescriptorCollection(PropertyDescriptor[] properties)
		{
			if (properties == null)
			{
				this._properties = Array.Empty<PropertyDescriptor>();
				this.Count = 0;
			}
			else
			{
				this._properties = properties;
				this.Count = properties.Length;
			}
			this._propsOwned = true;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.PropertyDescriptorCollection" /> class, which is optionally read-only.</summary>
		/// <param name="properties">An array of type <see cref="T:System.ComponentModel.PropertyDescriptor" /> that provides the properties for this collection.</param>
		/// <param name="readOnly">If <see langword="true" />, specifies that the collection cannot be modified.</param>
		// Token: 0x0600206B RID: 8299 RVA: 0x000702D0 File Offset: 0x0006E4D0
		public PropertyDescriptorCollection(PropertyDescriptor[] properties, bool readOnly) : this(properties)
		{
			this._readOnly = readOnly;
		}

		// Token: 0x0600206C RID: 8300 RVA: 0x000702E0 File Offset: 0x0006E4E0
		private PropertyDescriptorCollection(PropertyDescriptor[] properties, int propCount, string[] namedSort, IComparer comparer)
		{
			this._propsOwned = false;
			if (namedSort != null)
			{
				this._namedSort = (string[])namedSort.Clone();
			}
			this._comparer = comparer;
			this._properties = properties;
			this.Count = propCount;
			this._needSort = true;
		}

		/// <summary>Gets the number of property descriptors in the collection.</summary>
		/// <returns>The number of property descriptors in the collection.</returns>
		// Token: 0x170006A3 RID: 1699
		// (get) Token: 0x0600206D RID: 8301 RVA: 0x00070336 File Offset: 0x0006E536
		// (set) Token: 0x0600206E RID: 8302 RVA: 0x0007033E File Offset: 0x0006E53E
		public int Count
		{
			[CompilerGenerated]
			get
			{
				return this.<Count>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<Count>k__BackingField = value;
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.ComponentModel.PropertyDescriptor" /> at the specified index number.</summary>
		/// <param name="index">The zero-based index of the <see cref="T:System.ComponentModel.PropertyDescriptor" /> to get or set.</param>
		/// <returns>The <see cref="T:System.ComponentModel.PropertyDescriptor" /> with the specified index number.</returns>
		/// <exception cref="T:System.IndexOutOfRangeException">The <paramref name="index" /> parameter is not a valid index for <see cref="P:System.ComponentModel.PropertyDescriptorCollection.Item(System.Int32)" />.</exception>
		// Token: 0x170006A4 RID: 1700
		public virtual PropertyDescriptor this[int index]
		{
			get
			{
				if (index >= this.Count)
				{
					throw new IndexOutOfRangeException();
				}
				this.EnsurePropsOwned();
				return this._properties[index];
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.ComponentModel.PropertyDescriptor" /> with the specified name.</summary>
		/// <param name="name">The name of the <see cref="T:System.ComponentModel.PropertyDescriptor" /> to get from the collection.</param>
		/// <returns>The <see cref="T:System.ComponentModel.PropertyDescriptor" /> with the specified name, or <see langword="null" /> if the property does not exist.</returns>
		// Token: 0x170006A5 RID: 1701
		public virtual PropertyDescriptor this[string name]
		{
			get
			{
				return this.Find(name, false);
			}
		}

		/// <summary>Adds the specified <see cref="T:System.ComponentModel.PropertyDescriptor" /> to the collection.</summary>
		/// <param name="value">The <see cref="T:System.ComponentModel.PropertyDescriptor" /> to add to the collection.</param>
		/// <returns>The index of the <see cref="T:System.ComponentModel.PropertyDescriptor" /> that was added to the collection.</returns>
		/// <exception cref="T:System.NotSupportedException">The collection is read-only.</exception>
		// Token: 0x06002071 RID: 8305 RVA: 0x00070370 File Offset: 0x0006E570
		public int Add(PropertyDescriptor value)
		{
			if (this._readOnly)
			{
				throw new NotSupportedException();
			}
			this.EnsureSize(this.Count + 1);
			PropertyDescriptor[] properties = this._properties;
			int count = this.Count;
			this.Count = count + 1;
			properties[count] = value;
			return this.Count - 1;
		}

		/// <summary>Removes all <see cref="T:System.ComponentModel.PropertyDescriptor" /> objects from the collection.</summary>
		/// <exception cref="T:System.NotSupportedException">The collection is read-only.</exception>
		// Token: 0x06002072 RID: 8306 RVA: 0x000703BA File Offset: 0x0006E5BA
		public void Clear()
		{
			if (this._readOnly)
			{
				throw new NotSupportedException();
			}
			this.Count = 0;
			this._cachedFoundProperties = null;
		}

		/// <summary>Returns whether the collection contains the given <see cref="T:System.ComponentModel.PropertyDescriptor" />.</summary>
		/// <param name="value">The <see cref="T:System.ComponentModel.PropertyDescriptor" /> to find in the collection.</param>
		/// <returns>
		///   <see langword="true" /> if the collection contains the given <see cref="T:System.ComponentModel.PropertyDescriptor" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x06002073 RID: 8307 RVA: 0x000703D8 File Offset: 0x0006E5D8
		public bool Contains(PropertyDescriptor value)
		{
			return this.IndexOf(value) >= 0;
		}

		/// <summary>Copies the entire collection to an array, starting at the specified index number.</summary>
		/// <param name="array">An array of <see cref="T:System.ComponentModel.PropertyDescriptor" /> objects to copy elements of the collection to.</param>
		/// <param name="index">The index of the <paramref name="array" /> parameter at which copying begins.</param>
		// Token: 0x06002074 RID: 8308 RVA: 0x000703E7 File Offset: 0x0006E5E7
		public void CopyTo(Array array, int index)
		{
			this.EnsurePropsOwned();
			Array.Copy(this._properties, 0, array, index, this.Count);
		}

		// Token: 0x06002075 RID: 8309 RVA: 0x00070404 File Offset: 0x0006E604
		private void EnsurePropsOwned()
		{
			if (!this._propsOwned)
			{
				this._propsOwned = true;
				if (this._properties != null)
				{
					PropertyDescriptor[] array = new PropertyDescriptor[this.Count];
					Array.Copy(this._properties, 0, array, 0, this.Count);
					this._properties = array;
				}
			}
			if (this._needSort)
			{
				this._needSort = false;
				this.InternalSort(this._namedSort);
			}
		}

		// Token: 0x06002076 RID: 8310 RVA: 0x0007046C File Offset: 0x0006E66C
		private void EnsureSize(int sizeNeeded)
		{
			if (sizeNeeded <= this._properties.Length)
			{
				return;
			}
			if (this._properties.Length == 0)
			{
				this.Count = 0;
				this._properties = new PropertyDescriptor[sizeNeeded];
				return;
			}
			this.EnsurePropsOwned();
			PropertyDescriptor[] array = new PropertyDescriptor[Math.Max(sizeNeeded, this._properties.Length * 2)];
			Array.Copy(this._properties, 0, array, 0, this.Count);
			this._properties = array;
		}

		/// <summary>Returns the <see cref="T:System.ComponentModel.PropertyDescriptor" /> with the specified name, using a Boolean to indicate whether to ignore case.</summary>
		/// <param name="name">The name of the <see cref="T:System.ComponentModel.PropertyDescriptor" /> to return from the collection.</param>
		/// <param name="ignoreCase">
		///   <see langword="true" /> if you want to ignore the case of the property name; otherwise, <see langword="false" />.</param>
		/// <returns>A <see cref="T:System.ComponentModel.PropertyDescriptor" /> with the specified name, or <see langword="null" /> if the property does not exist.</returns>
		// Token: 0x06002077 RID: 8311 RVA: 0x000704DC File Offset: 0x0006E6DC
		public virtual PropertyDescriptor Find(string name, bool ignoreCase)
		{
			object internalSyncObject = this._internalSyncObject;
			PropertyDescriptor result;
			lock (internalSyncObject)
			{
				PropertyDescriptor propertyDescriptor = null;
				if (this._cachedFoundProperties == null || this._cachedIgnoreCase != ignoreCase)
				{
					this._cachedIgnoreCase = ignoreCase;
					if (ignoreCase)
					{
						this._cachedFoundProperties = new Hashtable(StringComparer.OrdinalIgnoreCase);
					}
					else
					{
						this._cachedFoundProperties = new Hashtable();
					}
				}
				object obj = this._cachedFoundProperties[name];
				if (obj != null)
				{
					result = (PropertyDescriptor)obj;
				}
				else
				{
					for (int i = 0; i < this.Count; i++)
					{
						if (ignoreCase)
						{
							if (string.Equals(this._properties[i].Name, name, StringComparison.OrdinalIgnoreCase))
							{
								this._cachedFoundProperties[name] = this._properties[i];
								propertyDescriptor = this._properties[i];
								break;
							}
						}
						else if (this._properties[i].Name.Equals(name))
						{
							this._cachedFoundProperties[name] = this._properties[i];
							propertyDescriptor = this._properties[i];
							break;
						}
					}
					result = propertyDescriptor;
				}
			}
			return result;
		}

		/// <summary>Returns the index of the given <see cref="T:System.ComponentModel.PropertyDescriptor" />.</summary>
		/// <param name="value">The <see cref="T:System.ComponentModel.PropertyDescriptor" /> to return the index of.</param>
		/// <returns>The index of the given <see cref="T:System.ComponentModel.PropertyDescriptor" />.</returns>
		// Token: 0x06002078 RID: 8312 RVA: 0x000705FC File Offset: 0x0006E7FC
		public int IndexOf(PropertyDescriptor value)
		{
			return Array.IndexOf<PropertyDescriptor>(this._properties, value, 0, this.Count);
		}

		/// <summary>Adds the <see cref="T:System.ComponentModel.PropertyDescriptor" /> to the collection at the specified index number.</summary>
		/// <param name="index">The index at which to add the <paramref name="value" /> parameter to the collection.</param>
		/// <param name="value">The <see cref="T:System.ComponentModel.PropertyDescriptor" /> to add to the collection.</param>
		/// <exception cref="T:System.NotSupportedException">The collection is read-only.</exception>
		// Token: 0x06002079 RID: 8313 RVA: 0x00070614 File Offset: 0x0006E814
		public void Insert(int index, PropertyDescriptor value)
		{
			if (this._readOnly)
			{
				throw new NotSupportedException();
			}
			this.EnsureSize(this.Count + 1);
			if (index < this.Count)
			{
				Array.Copy(this._properties, index, this._properties, index + 1, this.Count - index);
			}
			this._properties[index] = value;
			int count = this.Count;
			this.Count = count + 1;
		}

		/// <summary>Removes the specified <see cref="T:System.ComponentModel.PropertyDescriptor" /> from the collection.</summary>
		/// <param name="value">The <see cref="T:System.ComponentModel.PropertyDescriptor" /> to remove from the collection.</param>
		/// <exception cref="T:System.NotSupportedException">The collection is read-only.</exception>
		// Token: 0x0600207A RID: 8314 RVA: 0x0007067C File Offset: 0x0006E87C
		public void Remove(PropertyDescriptor value)
		{
			if (this._readOnly)
			{
				throw new NotSupportedException();
			}
			int num = this.IndexOf(value);
			if (num != -1)
			{
				this.RemoveAt(num);
			}
		}

		/// <summary>Removes the <see cref="T:System.ComponentModel.PropertyDescriptor" /> at the specified index from the collection.</summary>
		/// <param name="index">The index of the <see cref="T:System.ComponentModel.PropertyDescriptor" /> to remove from the collection.</param>
		/// <exception cref="T:System.NotSupportedException">The collection is read-only.</exception>
		// Token: 0x0600207B RID: 8315 RVA: 0x000706AC File Offset: 0x0006E8AC
		public void RemoveAt(int index)
		{
			if (this._readOnly)
			{
				throw new NotSupportedException();
			}
			if (index < this.Count - 1)
			{
				Array.Copy(this._properties, index + 1, this._properties, index, this.Count - index - 1);
			}
			this._properties[this.Count - 1] = null;
			int count = this.Count;
			this.Count = count - 1;
		}

		/// <summary>Sorts the members of this collection, using the default sort for this collection, which is usually alphabetical.</summary>
		/// <returns>A new <see cref="T:System.ComponentModel.PropertyDescriptorCollection" /> that contains the sorted <see cref="T:System.ComponentModel.PropertyDescriptor" /> objects.</returns>
		// Token: 0x0600207C RID: 8316 RVA: 0x00070711 File Offset: 0x0006E911
		public virtual PropertyDescriptorCollection Sort()
		{
			return new PropertyDescriptorCollection(this._properties, this.Count, this._namedSort, this._comparer);
		}

		/// <summary>Sorts the members of this collection. The specified order is applied first, followed by the default sort for this collection, which is usually alphabetical.</summary>
		/// <param name="names">An array of strings describing the order in which to sort the <see cref="T:System.ComponentModel.PropertyDescriptor" /> objects in this collection.</param>
		/// <returns>A new <see cref="T:System.ComponentModel.PropertyDescriptorCollection" /> that contains the sorted <see cref="T:System.ComponentModel.PropertyDescriptor" /> objects.</returns>
		// Token: 0x0600207D RID: 8317 RVA: 0x00070730 File Offset: 0x0006E930
		public virtual PropertyDescriptorCollection Sort(string[] names)
		{
			return new PropertyDescriptorCollection(this._properties, this.Count, names, this._comparer);
		}

		/// <summary>Sorts the members of this collection. The specified order is applied first, followed by the sort using the specified <see cref="T:System.Collections.IComparer" />.</summary>
		/// <param name="names">An array of strings describing the order in which to sort the <see cref="T:System.ComponentModel.PropertyDescriptor" /> objects in this collection.</param>
		/// <param name="comparer">A comparer to use to sort the <see cref="T:System.ComponentModel.PropertyDescriptor" /> objects in this collection.</param>
		/// <returns>A new <see cref="T:System.ComponentModel.PropertyDescriptorCollection" /> that contains the sorted <see cref="T:System.ComponentModel.PropertyDescriptor" /> objects.</returns>
		// Token: 0x0600207E RID: 8318 RVA: 0x0007074A File Offset: 0x0006E94A
		public virtual PropertyDescriptorCollection Sort(string[] names, IComparer comparer)
		{
			return new PropertyDescriptorCollection(this._properties, this.Count, names, comparer);
		}

		/// <summary>Sorts the members of this collection, using the specified <see cref="T:System.Collections.IComparer" />.</summary>
		/// <param name="comparer">A comparer to use to sort the <see cref="T:System.ComponentModel.PropertyDescriptor" /> objects in this collection.</param>
		/// <returns>A new <see cref="T:System.ComponentModel.PropertyDescriptorCollection" /> that contains the sorted <see cref="T:System.ComponentModel.PropertyDescriptor" /> objects.</returns>
		// Token: 0x0600207F RID: 8319 RVA: 0x0007075F File Offset: 0x0006E95F
		public virtual PropertyDescriptorCollection Sort(IComparer comparer)
		{
			return new PropertyDescriptorCollection(this._properties, this.Count, this._namedSort, comparer);
		}

		/// <summary>Sorts the members of this collection. The specified order is applied first, followed by the default sort for this collection, which is usually alphabetical.</summary>
		/// <param name="names">An array of strings describing the order in which to sort the <see cref="T:System.ComponentModel.PropertyDescriptor" /> objects in this collection.</param>
		// Token: 0x06002080 RID: 8320 RVA: 0x0007077C File Offset: 0x0006E97C
		protected void InternalSort(string[] names)
		{
			if (this._properties.Length == 0)
			{
				return;
			}
			this.InternalSort(this._comparer);
			if (names != null && names.Length != 0)
			{
				List<PropertyDescriptor> list = new List<PropertyDescriptor>(this._properties);
				int num = 0;
				int num2 = this._properties.Length;
				for (int i = 0; i < names.Length; i++)
				{
					for (int j = 0; j < num2; j++)
					{
						PropertyDescriptor propertyDescriptor = list[j];
						if (propertyDescriptor != null && propertyDescriptor.Name.Equals(names[i]))
						{
							this._properties[num++] = propertyDescriptor;
							list[j] = null;
							break;
						}
					}
				}
				for (int k = 0; k < num2; k++)
				{
					if (list[k] != null)
					{
						this._properties[num++] = list[k];
					}
				}
			}
		}

		/// <summary>Sorts the members of this collection, using the specified <see cref="T:System.Collections.IComparer" />.</summary>
		/// <param name="sorter">A comparer to use to sort the <see cref="T:System.ComponentModel.PropertyDescriptor" /> objects in this collection.</param>
		// Token: 0x06002081 RID: 8321 RVA: 0x00070847 File Offset: 0x0006EA47
		protected void InternalSort(IComparer sorter)
		{
			if (sorter == null)
			{
				TypeDescriptor.SortDescriptorArray(this);
				return;
			}
			Array.Sort(this._properties, sorter);
		}

		/// <summary>Returns an enumerator for this class.</summary>
		/// <returns>An enumerator of type <see cref="T:System.Collections.IEnumerator" />.</returns>
		// Token: 0x06002082 RID: 8322 RVA: 0x00070860 File Offset: 0x0006EA60
		public virtual IEnumerator GetEnumerator()
		{
			this.EnsurePropsOwned();
			if (this._properties.Length != this.Count)
			{
				PropertyDescriptor[] array = new PropertyDescriptor[this.Count];
				Array.Copy(this._properties, 0, array, 0, this.Count);
				return array.GetEnumerator();
			}
			return this._properties.GetEnumerator();
		}

		/// <summary>Gets a value indicating whether access to the collection is synchronized (thread safe).</summary>
		/// <returns>
		///   <see langword="true" /> if access to the collection is synchronized (thread safe); otherwise, <see langword="false" />.</returns>
		// Token: 0x170006A6 RID: 1702
		// (get) Token: 0x06002083 RID: 8323 RVA: 0x00003062 File Offset: 0x00001262
		bool ICollection.IsSynchronized
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets an object that can be used to synchronize access to the collection.</summary>
		/// <returns>An object that can be used to synchronize access to the collection.</returns>
		// Token: 0x170006A7 RID: 1703
		// (get) Token: 0x06002084 RID: 8324 RVA: 0x00002F6A File Offset: 0x0000116A
		object ICollection.SyncRoot
		{
			get
			{
				return null;
			}
		}

		/// <summary>Gets the number of elements contained in the collection.</summary>
		/// <returns>The number of elements contained in the collection.</returns>
		// Token: 0x170006A8 RID: 1704
		// (get) Token: 0x06002085 RID: 8325 RVA: 0x000708B5 File Offset: 0x0006EAB5
		int ICollection.Count
		{
			get
			{
				return this.Count;
			}
		}

		/// <summary>Removes all items from the collection.</summary>
		/// <exception cref="T:System.NotSupportedException">The collection is read-only.</exception>
		// Token: 0x06002086 RID: 8326 RVA: 0x000708BD File Offset: 0x0006EABD
		void IList.Clear()
		{
			this.Clear();
		}

		/// <summary>Removes all elements from the <see cref="T:System.Collections.IDictionary" />.</summary>
		// Token: 0x06002087 RID: 8327 RVA: 0x000708BD File Offset: 0x0006EABD
		void IDictionary.Clear()
		{
			this.Clear();
		}

		/// <summary>Returns an <see cref="T:System.Collections.IEnumerator" /> for the <see cref="T:System.Collections.IDictionary" />.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> for the <see cref="T:System.Collections.IDictionary" />.</returns>
		// Token: 0x06002088 RID: 8328 RVA: 0x000708C5 File Offset: 0x0006EAC5
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		/// <summary>Removes the item at the specified index.</summary>
		/// <param name="index">The zero-based index of the item to remove.</param>
		/// <exception cref="T:System.NotSupportedException">The collection is read-only.</exception>
		// Token: 0x06002089 RID: 8329 RVA: 0x000708CD File Offset: 0x0006EACD
		void IList.RemoveAt(int index)
		{
			this.RemoveAt(index);
		}

		/// <summary>Adds an element with the provided key and value to the <see cref="T:System.Collections.IDictionary" />.</summary>
		/// <param name="key">The <see cref="T:System.Object" /> to use as the key of the element to add.</param>
		/// <param name="value">The <see cref="T:System.Object" /> to use as the value of the element to add.</param>
		/// <exception cref="T:System.NotSupportedException">The collection is read-only.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="value" /> is <see langword="null" />.</exception>
		// Token: 0x0600208A RID: 8330 RVA: 0x000708D8 File Offset: 0x0006EAD8
		void IDictionary.Add(object key, object value)
		{
			PropertyDescriptor propertyDescriptor = value as PropertyDescriptor;
			if (propertyDescriptor == null)
			{
				throw new ArgumentException("value");
			}
			this.Add(propertyDescriptor);
		}

		/// <summary>Determines whether the <see cref="T:System.Collections.IDictionary" /> contains an element with the specified key.</summary>
		/// <param name="key">The key to locate in the <see cref="T:System.Collections.IDictionary" />.</param>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Collections.IDictionary" /> contains an element with the key; otherwise, <see langword="false" />.</returns>
		// Token: 0x0600208B RID: 8331 RVA: 0x00070902 File Offset: 0x0006EB02
		bool IDictionary.Contains(object key)
		{
			return key is string && this[(string)key] != null;
		}

		/// <summary>Returns an enumerator for this class.</summary>
		/// <returns>An enumerator of type <see cref="T:System.Collections.IEnumerator" />.</returns>
		// Token: 0x0600208C RID: 8332 RVA: 0x0007091D File Offset: 0x0006EB1D
		IDictionaryEnumerator IDictionary.GetEnumerator()
		{
			return new PropertyDescriptorCollection.PropertyDescriptorEnumerator(this);
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Collections.IDictionary" /> has a fixed size.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Collections.IDictionary" /> has a fixed size; otherwise, <see langword="false" />.</returns>
		// Token: 0x170006A9 RID: 1705
		// (get) Token: 0x0600208D RID: 8333 RVA: 0x00070925 File Offset: 0x0006EB25
		bool IDictionary.IsFixedSize
		{
			get
			{
				return this._readOnly;
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Collections.IDictionary" /> is read-only.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Collections.IDictionary" /> is read-only; otherwise, <see langword="false" />.</returns>
		// Token: 0x170006AA RID: 1706
		// (get) Token: 0x0600208E RID: 8334 RVA: 0x00070925 File Offset: 0x0006EB25
		bool IDictionary.IsReadOnly
		{
			get
			{
				return this._readOnly;
			}
		}

		/// <summary>Gets or sets the element with the specified key.</summary>
		/// <param name="key">The key of the element to get or set.</param>
		/// <returns>The element with the specified key.</returns>
		// Token: 0x170006AB RID: 1707
		object IDictionary.this[object key]
		{
			get
			{
				if (key is string)
				{
					return this[(string)key];
				}
				return null;
			}
			set
			{
				if (this._readOnly)
				{
					throw new NotSupportedException();
				}
				if (value != null && !(value is PropertyDescriptor))
				{
					throw new ArgumentException("value");
				}
				int num = -1;
				if (key is int)
				{
					num = (int)key;
					if (num < 0 || num >= this.Count)
					{
						throw new IndexOutOfRangeException();
					}
				}
				else
				{
					if (!(key is string))
					{
						throw new ArgumentException("key");
					}
					for (int i = 0; i < this.Count; i++)
					{
						if (this._properties[i].Name.Equals((string)key))
						{
							num = i;
							break;
						}
					}
				}
				if (num == -1)
				{
					this.Add((PropertyDescriptor)value);
					return;
				}
				this.EnsurePropsOwned();
				this._properties[num] = (PropertyDescriptor)value;
				if (this._cachedFoundProperties != null && key is string)
				{
					this._cachedFoundProperties[key] = value;
				}
			}
		}

		/// <summary>Gets an <see cref="T:System.Collections.ICollection" /> containing the keys of the <see cref="T:System.Collections.IDictionary" />.</summary>
		/// <returns>An <see cref="T:System.Collections.ICollection" /> containing the keys of the <see cref="T:System.Collections.IDictionary" />.</returns>
		// Token: 0x170006AC RID: 1708
		// (get) Token: 0x06002091 RID: 8337 RVA: 0x00070A24 File Offset: 0x0006EC24
		ICollection IDictionary.Keys
		{
			get
			{
				string[] array = new string[this.Count];
				for (int i = 0; i < this.Count; i++)
				{
					array[i] = this._properties[i].Name;
				}
				return array;
			}
		}

		/// <summary>Gets an <see cref="T:System.Collections.ICollection" /> containing the values in the <see cref="T:System.Collections.IDictionary" />.</summary>
		/// <returns>An <see cref="T:System.Collections.ICollection" /> containing the values in the <see cref="T:System.Collections.IDictionary" />.</returns>
		// Token: 0x170006AD RID: 1709
		// (get) Token: 0x06002092 RID: 8338 RVA: 0x00070A60 File Offset: 0x0006EC60
		ICollection IDictionary.Values
		{
			get
			{
				if (this._properties.Length != this.Count)
				{
					PropertyDescriptor[] array = new PropertyDescriptor[this.Count];
					Array.Copy(this._properties, 0, array, 0, this.Count);
					return array;
				}
				return (ICollection)this._properties.Clone();
			}
		}

		/// <summary>Removes the element with the specified key from the <see cref="T:System.Collections.IDictionary" />.</summary>
		/// <param name="key">The key of the element to remove.</param>
		// Token: 0x06002093 RID: 8339 RVA: 0x00070AB0 File Offset: 0x0006ECB0
		void IDictionary.Remove(object key)
		{
			if (key is string)
			{
				PropertyDescriptor propertyDescriptor = this[(string)key];
				if (propertyDescriptor != null)
				{
					((IList)this).Remove(propertyDescriptor);
				}
			}
		}

		/// <summary>Adds an item to the <see cref="T:System.Collections.IList" />.</summary>
		/// <param name="value">The item to add to the collection.</param>
		/// <returns>The position into which the new element was inserted.</returns>
		// Token: 0x06002094 RID: 8340 RVA: 0x00070ADC File Offset: 0x0006ECDC
		int IList.Add(object value)
		{
			return this.Add((PropertyDescriptor)value);
		}

		/// <summary>Determines whether the collection contains a specific value.</summary>
		/// <param name="value">The item to locate in the collection.</param>
		/// <returns>
		///   <see langword="true" /> if the item is found in the collection; otherwise, <see langword="false" />.</returns>
		// Token: 0x06002095 RID: 8341 RVA: 0x00070AEA File Offset: 0x0006ECEA
		bool IList.Contains(object value)
		{
			return this.Contains((PropertyDescriptor)value);
		}

		/// <summary>Determines the index of a specified item in the collection.</summary>
		/// <param name="value">The item to locate in the collection.</param>
		/// <returns>The index of <paramref name="value" /> if found in the list, otherwise -1.</returns>
		// Token: 0x06002096 RID: 8342 RVA: 0x00070AF8 File Offset: 0x0006ECF8
		int IList.IndexOf(object value)
		{
			return this.IndexOf((PropertyDescriptor)value);
		}

		/// <summary>Inserts an item into the collection at a specified index.</summary>
		/// <param name="index">The zero-based index at which <paramref name="value" /> should be inserted.</param>
		/// <param name="value">The item to insert into the collection.</param>
		/// <exception cref="T:System.NotSupportedException">The collection is read-only.</exception>
		// Token: 0x06002097 RID: 8343 RVA: 0x00070B06 File Offset: 0x0006ED06
		void IList.Insert(int index, object value)
		{
			this.Insert(index, (PropertyDescriptor)value);
		}

		/// <summary>Gets a value indicating whether the collection is read-only.</summary>
		/// <returns>
		///   <see langword="true" /> if the collection is read-only; otherwise, <see langword="false" />.</returns>
		// Token: 0x170006AE RID: 1710
		// (get) Token: 0x06002098 RID: 8344 RVA: 0x00070925 File Offset: 0x0006EB25
		bool IList.IsReadOnly
		{
			get
			{
				return this._readOnly;
			}
		}

		/// <summary>Gets a value indicating whether the collection has a fixed size.</summary>
		/// <returns>
		///   <see langword="true" /> if the collection has a fixed size; otherwise, <see langword="false" />.</returns>
		// Token: 0x170006AF RID: 1711
		// (get) Token: 0x06002099 RID: 8345 RVA: 0x00070925 File Offset: 0x0006EB25
		bool IList.IsFixedSize
		{
			get
			{
				return this._readOnly;
			}
		}

		/// <summary>Removes the first occurrence of a specified value from the collection.</summary>
		/// <param name="value">The item to remove from the collection.</param>
		/// <exception cref="T:System.NotSupportedException">The collection is read-only.</exception>
		// Token: 0x0600209A RID: 8346 RVA: 0x00070B15 File Offset: 0x0006ED15
		void IList.Remove(object value)
		{
			this.Remove((PropertyDescriptor)value);
		}

		/// <summary>Gets or sets an item from the collection at a specified index.</summary>
		/// <param name="index">The zero-based index of the item to get or set.</param>
		/// <returns>The element at the specified index.</returns>
		/// <exception cref="T:System.NotSupportedException">The collection is read-only.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="value" /> is not a <see cref="T:System.ComponentModel.PropertyDescriptor" />.</exception>
		/// <exception cref="T:System.IndexOutOfRangeException">
		///   <paramref name="index" /> is less than 0.  
		/// -or-  
		/// <paramref name="index" /> is equal to or greater than <see cref="P:System.ComponentModel.EventDescriptorCollection.Count" />.</exception>
		// Token: 0x170006B0 RID: 1712
		object IList.this[int index]
		{
			get
			{
				return this[index];
			}
			set
			{
				if (this._readOnly)
				{
					throw new NotSupportedException();
				}
				if (index >= this.Count)
				{
					throw new IndexOutOfRangeException();
				}
				if (value != null && !(value is PropertyDescriptor))
				{
					throw new ArgumentException("value");
				}
				this.EnsurePropsOwned();
				this._properties[index] = (PropertyDescriptor)value;
			}
		}

		// Token: 0x0600209D RID: 8349 RVA: 0x00070B80 File Offset: 0x0006ED80
		// Note: this type is marked as 'beforefieldinit'.
		static PropertyDescriptorCollection()
		{
		}

		/// <summary>Specifies an empty collection that you can use instead of creating a new one with no items. This <see langword="static" /> field is read-only.</summary>
		// Token: 0x04000FC4 RID: 4036
		public static readonly PropertyDescriptorCollection Empty = new PropertyDescriptorCollection(null, true);

		// Token: 0x04000FC5 RID: 4037
		private IDictionary _cachedFoundProperties;

		// Token: 0x04000FC6 RID: 4038
		private bool _cachedIgnoreCase;

		// Token: 0x04000FC7 RID: 4039
		private PropertyDescriptor[] _properties;

		// Token: 0x04000FC8 RID: 4040
		private readonly string[] _namedSort;

		// Token: 0x04000FC9 RID: 4041
		private readonly IComparer _comparer;

		// Token: 0x04000FCA RID: 4042
		private bool _propsOwned;

		// Token: 0x04000FCB RID: 4043
		private bool _needSort;

		// Token: 0x04000FCC RID: 4044
		private bool _readOnly;

		// Token: 0x04000FCD RID: 4045
		private readonly object _internalSyncObject = new object();

		// Token: 0x04000FCE RID: 4046
		[CompilerGenerated]
		private int <Count>k__BackingField;

		// Token: 0x020003E1 RID: 993
		private class PropertyDescriptorEnumerator : IDictionaryEnumerator, IEnumerator
		{
			// Token: 0x0600209E RID: 8350 RVA: 0x00070B8E File Offset: 0x0006ED8E
			public PropertyDescriptorEnumerator(PropertyDescriptorCollection owner)
			{
				this._owner = owner;
			}

			// Token: 0x170006B1 RID: 1713
			// (get) Token: 0x0600209F RID: 8351 RVA: 0x00070BA4 File Offset: 0x0006EDA4
			public object Current
			{
				get
				{
					return this.Entry;
				}
			}

			// Token: 0x170006B2 RID: 1714
			// (get) Token: 0x060020A0 RID: 8352 RVA: 0x00070BB4 File Offset: 0x0006EDB4
			public DictionaryEntry Entry
			{
				get
				{
					PropertyDescriptor propertyDescriptor = this._owner[this._index];
					return new DictionaryEntry(propertyDescriptor.Name, propertyDescriptor);
				}
			}

			// Token: 0x170006B3 RID: 1715
			// (get) Token: 0x060020A1 RID: 8353 RVA: 0x00070BDF File Offset: 0x0006EDDF
			public object Key
			{
				get
				{
					return this._owner[this._index].Name;
				}
			}

			// Token: 0x170006B4 RID: 1716
			// (get) Token: 0x060020A2 RID: 8354 RVA: 0x00070BDF File Offset: 0x0006EDDF
			public object Value
			{
				get
				{
					return this._owner[this._index].Name;
				}
			}

			// Token: 0x060020A3 RID: 8355 RVA: 0x00070BF7 File Offset: 0x0006EDF7
			public bool MoveNext()
			{
				if (this._index < this._owner.Count - 1)
				{
					this._index++;
					return true;
				}
				return false;
			}

			// Token: 0x060020A4 RID: 8356 RVA: 0x00070C1F File Offset: 0x0006EE1F
			public void Reset()
			{
				this._index = -1;
			}

			// Token: 0x04000FCF RID: 4047
			private PropertyDescriptorCollection _owner;

			// Token: 0x04000FD0 RID: 4048
			private int _index = -1;
		}
	}
}

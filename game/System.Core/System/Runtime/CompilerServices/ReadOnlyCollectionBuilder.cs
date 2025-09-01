using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq.Expressions;

namespace System.Runtime.CompilerServices
{
	/// <summary>The builder for read only collection.</summary>
	/// <typeparam name="T">The type of the collection element.</typeparam>
	// Token: 0x020002E5 RID: 741
	[Serializable]
	public sealed class ReadOnlyCollectionBuilder<T> : IList<!0>, ICollection<!0>, IEnumerable<!0>, IEnumerable, IList, ICollection
	{
		/// <summary>Constructs a ReadOnlyCollectionBuilder.</summary>
		// Token: 0x06001676 RID: 5750 RVA: 0x0004BD7E File Offset: 0x00049F7E
		public ReadOnlyCollectionBuilder()
		{
			this._items = Array.Empty<T>();
		}

		/// <summary>Constructs a ReadOnlyCollectionBuilder with a given initial capacity. The contents are empty but builder will have reserved room for the given number of elements before any reallocations are required.</summary>
		/// <param name="capacity">Initial capacity.</param>
		// Token: 0x06001677 RID: 5751 RVA: 0x0004BD91 File Offset: 0x00049F91
		public ReadOnlyCollectionBuilder(int capacity)
		{
			if (capacity < 0)
			{
				throw new ArgumentOutOfRangeException("capacity");
			}
			this._items = new T[capacity];
		}

		/// <summary>Constructs a ReadOnlyCollectionBuilder, copying contents of the given collection.</summary>
		/// <param name="collection">Collection to copy elements from.</param>
		// Token: 0x06001678 RID: 5752 RVA: 0x0004BDB4 File Offset: 0x00049FB4
		public ReadOnlyCollectionBuilder(IEnumerable<T> collection)
		{
			if (collection == null)
			{
				throw new ArgumentNullException("collection");
			}
			ICollection<T> collection2 = collection as ICollection<!0>;
			if (collection2 != null)
			{
				int count = collection2.Count;
				this._items = new T[count];
				collection2.CopyTo(this._items, 0);
				this._size = count;
				return;
			}
			this._size = 0;
			this._items = new T[4];
			foreach (!0 item in collection)
			{
				this.Add(item);
			}
		}

		/// <summary>Gets and sets the capacity of this ReadOnlyCollectionBuilder.</summary>
		/// <returns>The capacity of this ReadOnlyCollectionBuilder.</returns>
		// Token: 0x170003D9 RID: 985
		// (get) Token: 0x06001679 RID: 5753 RVA: 0x0004BE54 File Offset: 0x0004A054
		// (set) Token: 0x0600167A RID: 5754 RVA: 0x0004BE60 File Offset: 0x0004A060
		public int Capacity
		{
			get
			{
				return this._items.Length;
			}
			set
			{
				if (value < this._size)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				if (value != this._items.Length)
				{
					if (value > 0)
					{
						T[] array = new T[value];
						if (this._size > 0)
						{
							Array.Copy(this._items, 0, array, 0, this._size);
						}
						this._items = array;
						return;
					}
					this._items = Array.Empty<T>();
				}
			}
		}

		/// <summary>Returns number of elements in the ReadOnlyCollectionBuilder.</summary>
		/// <returns>The number of elements in the ReadOnlyCollectionBuilder.</returns>
		// Token: 0x170003DA RID: 986
		// (get) Token: 0x0600167B RID: 5755 RVA: 0x0004BEC7 File Offset: 0x0004A0C7
		public int Count
		{
			get
			{
				return this._size;
			}
		}

		/// <summary>Returns the index of the first occurrence of a given value in the builder.</summary>
		/// <param name="item">An item to search for.</param>
		/// <returns>The index of the first occurrence of an item.</returns>
		// Token: 0x0600167C RID: 5756 RVA: 0x0004BECF File Offset: 0x0004A0CF
		public int IndexOf(T item)
		{
			return Array.IndexOf<T>(this._items, item, 0, this._size);
		}

		/// <summary>Inserts an item to the <see cref="T:System.Runtime.CompilerServices.ReadOnlyCollectionBuilder`1" /> at the specified index.</summary>
		/// <param name="index">The zero-based index at which item should be inserted.</param>
		/// <param name="item">The object to insert into the <see cref="T:System.Runtime.CompilerServices.ReadOnlyCollectionBuilder`1" />.</param>
		// Token: 0x0600167D RID: 5757 RVA: 0x0004BEE4 File Offset: 0x0004A0E4
		public void Insert(int index, T item)
		{
			if (index > this._size)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			if (this._size == this._items.Length)
			{
				this.EnsureCapacity(this._size + 1);
			}
			if (index < this._size)
			{
				Array.Copy(this._items, index, this._items, index + 1, this._size - index);
			}
			this._items[index] = item;
			this._size++;
			this._version++;
		}

		/// <summary>Removes the <see cref="T:System.Runtime.CompilerServices.ReadOnlyCollectionBuilder`1" /> item at the specified index.</summary>
		/// <param name="index">The zero-based index of the item to remove.</param>
		// Token: 0x0600167E RID: 5758 RVA: 0x0004BF74 File Offset: 0x0004A174
		public void RemoveAt(int index)
		{
			if (index < 0 || index >= this._size)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			this._size--;
			if (index < this._size)
			{
				Array.Copy(this._items, index + 1, this._items, index, this._size - index);
			}
			this._items[this._size] = default(T);
			this._version++;
		}

		/// <summary>Gets or sets the element at the specified index.</summary>
		/// <param name="index">The zero-based index of the element to get or set.</param>
		/// <returns>The element at the specified index.</returns>
		// Token: 0x170003DB RID: 987
		public T this[int index]
		{
			get
			{
				if (index >= this._size)
				{
					throw new ArgumentOutOfRangeException("index");
				}
				return this._items[index];
			}
			set
			{
				if (index >= this._size)
				{
					throw new ArgumentOutOfRangeException("index");
				}
				this._items[index] = value;
				this._version++;
			}
		}

		/// <summary>Adds an item to the <see cref="T:System.Runtime.CompilerServices.ReadOnlyCollectionBuilder`1" />.</summary>
		/// <param name="item">The object to add to the <see cref="T:System.Runtime.CompilerServices.ReadOnlyCollectionBuilder`1" />.</param>
		// Token: 0x06001681 RID: 5761 RVA: 0x0004C048 File Offset: 0x0004A248
		public void Add(T item)
		{
			if (this._size == this._items.Length)
			{
				this.EnsureCapacity(this._size + 1);
			}
			T[] items = this._items;
			int size = this._size;
			this._size = size + 1;
			items[size] = item;
			this._version++;
		}

		/// <summary>Removes all items from the <see cref="T:System.Runtime.CompilerServices.ReadOnlyCollectionBuilder`1" />.</summary>
		// Token: 0x06001682 RID: 5762 RVA: 0x0004C09E File Offset: 0x0004A29E
		public void Clear()
		{
			if (this._size > 0)
			{
				Array.Clear(this._items, 0, this._size);
				this._size = 0;
			}
			this._version++;
		}

		/// <summary>Determines whether the <see cref="T:System.Runtime.CompilerServices.ReadOnlyCollectionBuilder`1" /> contains a specific value</summary>
		/// <param name="item">the object to locate in the <see cref="T:System.Runtime.CompilerServices.ReadOnlyCollectionBuilder`1" />.</param>
		/// <returns>true if item is found in the <see cref="T:System.Runtime.CompilerServices.ReadOnlyCollectionBuilder`1" />; otherwise, false.</returns>
		// Token: 0x06001683 RID: 5763 RVA: 0x0004C0D0 File Offset: 0x0004A2D0
		public bool Contains(T item)
		{
			if (item == null)
			{
				for (int i = 0; i < this._size; i++)
				{
					if (this._items[i] == null)
					{
						return true;
					}
				}
				return false;
			}
			EqualityComparer<T> @default = EqualityComparer<T>.Default;
			for (int j = 0; j < this._size; j++)
			{
				if (@default.Equals(this._items[j], item))
				{
					return true;
				}
			}
			return false;
		}

		/// <summary>Copies the elements of the <see cref="T:System.Runtime.CompilerServices.ReadOnlyCollectionBuilder`1" /> to an <see cref="T:System.Array" />, starting at particular <see cref="T:System.Array" /> index.</summary>
		/// <param name="array">The one-dimensional <see cref="T:System.Array" /> that is the destination of the elements copied from <see cref="T:System.Runtime.CompilerServices.ReadOnlyCollectionBuilder`1" />.</param>
		/// <param name="arrayIndex">The zero-based index in array at which copying begins.</param>
		// Token: 0x06001684 RID: 5764 RVA: 0x0004C13C File Offset: 0x0004A33C
		public void CopyTo(T[] array, int arrayIndex)
		{
			Array.Copy(this._items, 0, array, arrayIndex, this._size);
		}

		// Token: 0x170003DC RID: 988
		// (get) Token: 0x06001685 RID: 5765 RVA: 0x000023D1 File Offset: 0x000005D1
		bool ICollection<!0>.IsReadOnly
		{
			get
			{
				return false;
			}
		}

		/// <summary>Removes the first occurrence of a specific object from the <see cref="T:System.Runtime.CompilerServices.ReadOnlyCollectionBuilder`1" />.</summary>
		/// <param name="item">The object to remove from the <see cref="T:System.Runtime.CompilerServices.ReadOnlyCollectionBuilder`1" />.</param>
		/// <returns>true if item was successfully removed from the <see cref="T:System.Runtime.CompilerServices.ReadOnlyCollectionBuilder`1" />; otherwise, false. This method also returns false if item is not found in the original <see cref="T:System.Runtime.CompilerServices.ReadOnlyCollectionBuilder`1" />.</returns>
		// Token: 0x06001686 RID: 5766 RVA: 0x0004C154 File Offset: 0x0004A354
		public bool Remove(T item)
		{
			int num = this.IndexOf(item);
			if (num >= 0)
			{
				this.RemoveAt(num);
				return true;
			}
			return false;
		}

		/// <summary>Returns an enumerator that iterates through the collection.</summary>
		/// <returns>A <see cref="T:System.Collections.Generic.IEnumerator`1" /> that can be used to iterate through the collection.</returns>
		// Token: 0x06001687 RID: 5767 RVA: 0x0004C177 File Offset: 0x0004A377
		public IEnumerator<T> GetEnumerator()
		{
			return new ReadOnlyCollectionBuilder<T>.Enumerator(this);
		}

		/// <summary>Returns an enumerator that iterates through the collection.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> that can be used to iterate through the collection.</returns>
		// Token: 0x06001688 RID: 5768 RVA: 0x0004C17F File Offset: 0x0004A37F
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Collections.IList" /> is read-only.</summary>
		/// <returns>
		///     <see langword="true" /> if the <see cref="T:System.Collections.IList" /> is read-only; otherwise, <see langword="false" />.</returns>
		// Token: 0x170003DD RID: 989
		// (get) Token: 0x06001689 RID: 5769 RVA: 0x000023D1 File Offset: 0x000005D1
		bool IList.IsReadOnly
		{
			get
			{
				return false;
			}
		}

		/// <summary>Adds an item to the <see cref="T:System.Collections.IList" />.</summary>
		/// <param name="value">The <see cref="T:System.Object" /> to add to the <see cref="T:System.Collections.IList" />.</param>
		/// <returns>The position into which the new element was inserted.</returns>
		// Token: 0x0600168A RID: 5770 RVA: 0x0004C188 File Offset: 0x0004A388
		int IList.Add(object value)
		{
			ReadOnlyCollectionBuilder<T>.ValidateNullValue(value, "value");
			try
			{
				this.Add((T)((object)value));
			}
			catch (InvalidCastException)
			{
				throw Error.InvalidTypeException(value, typeof(T), "value");
			}
			return this.Count - 1;
		}

		/// <summary>Determines whether the <see cref="T:System.Collections.IList" /> contains a specific value.</summary>
		/// <param name="value">The <see cref="T:System.Object" /> to locate in the <see cref="T:System.Collections.IList" />.</param>
		/// <returns>
		///     <see langword="true" /> if <paramref name="item" /> is found in the <see cref="T:System.Collections.IList" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x0600168B RID: 5771 RVA: 0x0004C1E0 File Offset: 0x0004A3E0
		bool IList.Contains(object value)
		{
			return ReadOnlyCollectionBuilder<T>.IsCompatibleObject(value) && this.Contains((T)((object)value));
		}

		/// <summary>Determines the index of a specific item in the <see cref="T:System.Collections.IList" />.</summary>
		/// <param name="value">The object to locate in the <see cref="T:System.Collections.IList" />.</param>
		/// <returns>The index of <paramref name="item" /> if found in the list; otherwise, –1.</returns>
		// Token: 0x0600168C RID: 5772 RVA: 0x0004C1F8 File Offset: 0x0004A3F8
		int IList.IndexOf(object value)
		{
			if (ReadOnlyCollectionBuilder<T>.IsCompatibleObject(value))
			{
				return this.IndexOf((T)((object)value));
			}
			return -1;
		}

		/// <summary>Inserts an item to the <see cref="T:System.Collections.IList" /> at the specified index.</summary>
		/// <param name="index">The zero-based index at which <paramref name="item" /> should be inserted.</param>
		/// <param name="value">The object to insert into the <see cref="T:System.Collections.IList" />.</param>
		// Token: 0x0600168D RID: 5773 RVA: 0x0004C210 File Offset: 0x0004A410
		void IList.Insert(int index, object value)
		{
			ReadOnlyCollectionBuilder<T>.ValidateNullValue(value, "value");
			try
			{
				this.Insert(index, (T)((object)value));
			}
			catch (InvalidCastException)
			{
				throw Error.InvalidTypeException(value, typeof(T), "value");
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Collections.IList" /> has a fixed size.</summary>
		/// <returns>
		///     <see langword="true" /> if the <see cref="T:System.Collections.IList" /> has a fixed size; otherwise, <see langword="false" />.</returns>
		// Token: 0x170003DE RID: 990
		// (get) Token: 0x0600168E RID: 5774 RVA: 0x000023D1 File Offset: 0x000005D1
		bool IList.IsFixedSize
		{
			get
			{
				return false;
			}
		}

		/// <summary>Removes the first occurrence of a specific object from the <see cref="T:System.Collections.IList" />.</summary>
		/// <param name="value">The object to remove from the <see cref="T:System.Collections.IList" />.</param>
		// Token: 0x0600168F RID: 5775 RVA: 0x0004C260 File Offset: 0x0004A460
		void IList.Remove(object value)
		{
			if (ReadOnlyCollectionBuilder<T>.IsCompatibleObject(value))
			{
				this.Remove((T)((object)value));
			}
		}

		/// <summary>Gets or sets the element at the specified index.</summary>
		/// <param name="index">The zero-based index of the element to get or set.</param>
		/// <returns>The element at the specified index.</returns>
		// Token: 0x170003DF RID: 991
		object IList.this[int index]
		{
			get
			{
				return this[index];
			}
			set
			{
				ReadOnlyCollectionBuilder<T>.ValidateNullValue(value, "value");
				try
				{
					this[index] = (T)((object)value);
				}
				catch (InvalidCastException)
				{
					throw Error.InvalidTypeException(value, typeof(T), "value");
				}
			}
		}

		/// <summary>Copies the elements of the <see cref="T:System.Collections.Generic.ICollection`1" /> to an array, starting at the specified array index.</summary>
		/// <param name="array">The one-dimensional array that is the destination of the elements copied from <see cref="T:System.Collections.Generic.ICollection`1" />. The array must have zero-based indexing.</param>
		/// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins.</param>
		// Token: 0x06001692 RID: 5778 RVA: 0x0004C2D8 File Offset: 0x0004A4D8
		void ICollection.CopyTo(Array array, int index)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (array.Rank != 1)
			{
				throw new ArgumentException("array");
			}
			Array.Copy(this._items, 0, array, index, this._size);
		}

		/// <summary>Gets a value indicating whether access to the <see cref="T:System.Collections.ICollection" /> is synchronized (thread safe).</summary>
		/// <returns>
		///     <see langword="true" /> if access to the <see cref="T:System.Collections.ICollection" /> is synchronized (thread safe); otherwise, <see langword="false" />.</returns>
		// Token: 0x170003E0 RID: 992
		// (get) Token: 0x06001693 RID: 5779 RVA: 0x000023D1 File Offset: 0x000005D1
		bool ICollection.IsSynchronized
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets an object that can be used to synchronize access to the <see cref="T:System.Collections.ICollection" />.</summary>
		/// <returns>An object that can be used to synchronize access to the <see cref="T:System.Collections.ICollection" />.</returns>
		// Token: 0x170003E1 RID: 993
		// (get) Token: 0x06001694 RID: 5780 RVA: 0x000022A7 File Offset: 0x000004A7
		object ICollection.SyncRoot
		{
			get
			{
				return this;
			}
		}

		/// <summary>Reverses the order of the elements in the entire <see cref="T:System.Runtime.CompilerServices.ReadOnlyCollectionBuilder`1" />.</summary>
		// Token: 0x06001695 RID: 5781 RVA: 0x0004C310 File Offset: 0x0004A510
		public void Reverse()
		{
			this.Reverse(0, this.Count);
		}

		/// <summary>Reverses the order of the elements in the specified range.</summary>
		/// <param name="index">The zero-based starting index of the range to reverse.</param>
		/// <param name="count">The number of elements in the range to reverse.</param>
		// Token: 0x06001696 RID: 5782 RVA: 0x0004C31F File Offset: 0x0004A51F
		public void Reverse(int index, int count)
		{
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			Array.Reverse<T>(this._items, index, count);
			this._version++;
		}

		/// <summary>Copies the elements of the <see cref="T:System.Runtime.CompilerServices.ReadOnlyCollectionBuilder`1" /> to a new array.</summary>
		/// <returns>An array containing copies of the elements of the <see cref="T:System.Runtime.CompilerServices.ReadOnlyCollectionBuilder`1" />.</returns>
		// Token: 0x06001697 RID: 5783 RVA: 0x0004C35C File Offset: 0x0004A55C
		public T[] ToArray()
		{
			T[] array = new T[this._size];
			Array.Copy(this._items, 0, array, 0, this._size);
			return array;
		}

		/// <summary>Creates a <see cref="T:System.Collections.ObjectModel.ReadOnlyCollection`1" /> containing all of the elements of the <see cref="T:System.Runtime.CompilerServices.ReadOnlyCollectionBuilder`1" />, avoiding copying the elements to the new array if possible. Resets the <see cref="T:System.Runtime.CompilerServices.ReadOnlyCollectionBuilder`1" /> after the <see cref="T:System.Collections.ObjectModel.ReadOnlyCollection`1" /> has been created.</summary>
		/// <returns>A new instance of <see cref="T:System.Collections.ObjectModel.ReadOnlyCollection`1" />.</returns>
		// Token: 0x06001698 RID: 5784 RVA: 0x0004C38C File Offset: 0x0004A58C
		public ReadOnlyCollection<T> ToReadOnlyCollection()
		{
			T[] list;
			if (this._size == this._items.Length)
			{
				list = this._items;
			}
			else
			{
				list = this.ToArray();
			}
			this._items = Array.Empty<T>();
			this._size = 0;
			this._version++;
			return new TrueReadOnlyCollection<T>(list);
		}

		// Token: 0x06001699 RID: 5785 RVA: 0x0004C3E0 File Offset: 0x0004A5E0
		private void EnsureCapacity(int min)
		{
			if (this._items.Length < min)
			{
				int num = 4;
				if (this._items.Length != 0)
				{
					num = this._items.Length * 2;
				}
				if (num < min)
				{
					num = min;
				}
				this.Capacity = num;
			}
		}

		// Token: 0x0600169A RID: 5786 RVA: 0x0004C41C File Offset: 0x0004A61C
		private static bool IsCompatibleObject(object value)
		{
			return value is T || (value == null && default(T) == null);
		}

		// Token: 0x0600169B RID: 5787 RVA: 0x0004C44C File Offset: 0x0004A64C
		private static void ValidateNullValue(object value, string argument)
		{
			if (value == null && default(T) != null)
			{
				throw Error.InvalidNullValue(typeof(T), argument);
			}
		}

		// Token: 0x04000B56 RID: 2902
		private const int DefaultCapacity = 4;

		// Token: 0x04000B57 RID: 2903
		private T[] _items;

		// Token: 0x04000B58 RID: 2904
		private int _size;

		// Token: 0x04000B59 RID: 2905
		private int _version;

		// Token: 0x020002E6 RID: 742
		[Serializable]
		private class Enumerator : IEnumerator<!0>, IDisposable, IEnumerator
		{
			// Token: 0x0600169C RID: 5788 RVA: 0x0004C47D File Offset: 0x0004A67D
			internal Enumerator(ReadOnlyCollectionBuilder<T> builder)
			{
				this._builder = builder;
				this._version = builder._version;
				this._index = 0;
				this._current = default(T);
			}

			// Token: 0x170003E2 RID: 994
			// (get) Token: 0x0600169D RID: 5789 RVA: 0x0004C4AB File Offset: 0x0004A6AB
			public T Current
			{
				get
				{
					return this._current;
				}
			}

			// Token: 0x0600169E RID: 5790 RVA: 0x00003A59 File Offset: 0x00001C59
			public void Dispose()
			{
			}

			// Token: 0x170003E3 RID: 995
			// (get) Token: 0x0600169F RID: 5791 RVA: 0x0004C4B3 File Offset: 0x0004A6B3
			object IEnumerator.Current
			{
				get
				{
					if (this._index == 0 || this._index > this._builder._size)
					{
						throw Error.EnumerationIsDone();
					}
					return this._current;
				}
			}

			// Token: 0x060016A0 RID: 5792 RVA: 0x0004C4E4 File Offset: 0x0004A6E4
			public bool MoveNext()
			{
				if (this._version != this._builder._version)
				{
					throw Error.CollectionModifiedWhileEnumerating();
				}
				if (this._index < this._builder._size)
				{
					T[] items = this._builder._items;
					int index = this._index;
					this._index = index + 1;
					this._current = items[index];
					return true;
				}
				this._index = this._builder._size + 1;
				this._current = default(T);
				return false;
			}

			// Token: 0x060016A1 RID: 5793 RVA: 0x0004C566 File Offset: 0x0004A766
			void IEnumerator.Reset()
			{
				if (this._version != this._builder._version)
				{
					throw Error.CollectionModifiedWhileEnumerating();
				}
				this._index = 0;
				this._current = default(T);
			}

			// Token: 0x04000B5A RID: 2906
			private readonly ReadOnlyCollectionBuilder<T> _builder;

			// Token: 0x04000B5B RID: 2907
			private readonly int _version;

			// Token: 0x04000B5C RID: 2908
			private int _index;

			// Token: 0x04000B5D RID: 2909
			private T _current;
		}
	}
}

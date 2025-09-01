using System;
using System.Diagnostics;
using System.Threading;

namespace System.Collections
{
	/// <summary>Implements the <see cref="T:System.Collections.IList" /> interface using an array whose size is dynamically increased as required.</summary>
	// Token: 0x02000A3B RID: 2619
	[DebuggerDisplay("Count = {Count}")]
	[DebuggerTypeProxy(typeof(ArrayList.ArrayListDebugView))]
	[Serializable]
	public class ArrayList : IList, ICollection, IEnumerable, ICloneable
	{
		// Token: 0x06005D01 RID: 23809 RVA: 0x0000259F File Offset: 0x0000079F
		internal ArrayList(bool trash)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.ArrayList" /> class that is empty and has the default initial capacity.</summary>
		// Token: 0x06005D02 RID: 23810 RVA: 0x00138DFF File Offset: 0x00136FFF
		public ArrayList()
		{
			this._items = Array.Empty<object>();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.ArrayList" /> class that is empty and has the specified initial capacity.</summary>
		/// <param name="capacity">The number of elements that the new list can initially store.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="capacity" /> is less than zero.</exception>
		// Token: 0x06005D03 RID: 23811 RVA: 0x00138E14 File Offset: 0x00137014
		public ArrayList(int capacity)
		{
			if (capacity < 0)
			{
				throw new ArgumentOutOfRangeException("capacity", SR.Format("'{0}' must be non-negative.", "capacity"));
			}
			if (capacity == 0)
			{
				this._items = Array.Empty<object>();
				return;
			}
			this._items = new object[capacity];
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.ArrayList" /> class that contains elements copied from the specified collection and that has the same initial capacity as the number of elements copied.</summary>
		/// <param name="c">The <see cref="T:System.Collections.ICollection" /> whose elements are copied to the new list.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="c" /> is <see langword="null" />.</exception>
		// Token: 0x06005D04 RID: 23812 RVA: 0x00138E60 File Offset: 0x00137060
		public ArrayList(ICollection c)
		{
			if (c == null)
			{
				throw new ArgumentNullException("c", "Collection cannot be null.");
			}
			int count = c.Count;
			if (count == 0)
			{
				this._items = Array.Empty<object>();
				return;
			}
			this._items = new object[count];
			this.AddRange(c);
		}

		/// <summary>Gets or sets the number of elements that the <see cref="T:System.Collections.ArrayList" /> can contain.</summary>
		/// <returns>The number of elements that the <see cref="T:System.Collections.ArrayList" /> can contain.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <see cref="P:System.Collections.ArrayList.Capacity" /> is set to a value that is less than <see cref="P:System.Collections.ArrayList.Count" />.</exception>
		/// <exception cref="T:System.OutOfMemoryException">There is not enough memory available on the system.</exception>
		// Token: 0x17001035 RID: 4149
		// (get) Token: 0x06005D05 RID: 23813 RVA: 0x00138EAF File Offset: 0x001370AF
		// (set) Token: 0x06005D06 RID: 23814 RVA: 0x00138EBC File Offset: 0x001370BC
		public virtual int Capacity
		{
			get
			{
				return this._items.Length;
			}
			set
			{
				if (value < this._size)
				{
					throw new ArgumentOutOfRangeException("value", "capacity was less than the current size.");
				}
				if (value != this._items.Length)
				{
					if (value > 0)
					{
						object[] array = new object[value];
						if (this._size > 0)
						{
							Array.Copy(this._items, 0, array, 0, this._size);
						}
						this._items = array;
						return;
					}
					this._items = new object[4];
				}
			}
		}

		/// <summary>Gets the number of elements actually contained in the <see cref="T:System.Collections.ArrayList" />.</summary>
		/// <returns>The number of elements actually contained in the <see cref="T:System.Collections.ArrayList" />.</returns>
		// Token: 0x17001036 RID: 4150
		// (get) Token: 0x06005D07 RID: 23815 RVA: 0x00138F29 File Offset: 0x00137129
		public virtual int Count
		{
			get
			{
				return this._size;
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Collections.ArrayList" /> has a fixed size.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Collections.ArrayList" /> has a fixed size; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		// Token: 0x17001037 RID: 4151
		// (get) Token: 0x06005D08 RID: 23816 RVA: 0x0000DDA9 File Offset: 0x0000BFA9
		public virtual bool IsFixedSize
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Collections.ArrayList" /> is read-only.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Collections.ArrayList" /> is read-only; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		// Token: 0x17001038 RID: 4152
		// (get) Token: 0x06005D09 RID: 23817 RVA: 0x0000DDA9 File Offset: 0x0000BFA9
		public virtual bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets a value indicating whether access to the <see cref="T:System.Collections.ArrayList" /> is synchronized (thread safe).</summary>
		/// <returns>
		///   <see langword="true" /> if access to the <see cref="T:System.Collections.ArrayList" /> is synchronized (thread safe); otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		// Token: 0x17001039 RID: 4153
		// (get) Token: 0x06005D0A RID: 23818 RVA: 0x0000DDA9 File Offset: 0x0000BFA9
		public virtual bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets an object that can be used to synchronize access to the <see cref="T:System.Collections.ArrayList" />.</summary>
		/// <returns>An object that can be used to synchronize access to the <see cref="T:System.Collections.ArrayList" />.</returns>
		// Token: 0x1700103A RID: 4154
		// (get) Token: 0x06005D0B RID: 23819 RVA: 0x00138F31 File Offset: 0x00137131
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

		/// <summary>Gets or sets the element at the specified index.</summary>
		/// <param name="index">The zero-based index of the element to get or set.</param>
		/// <returns>The element at the specified index.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero.  
		/// -or-  
		/// <paramref name="index" /> is equal to or greater than <see cref="P:System.Collections.ArrayList.Count" />.</exception>
		// Token: 0x1700103B RID: 4155
		public virtual object this[int index]
		{
			get
			{
				if (index < 0 || index >= this._size)
				{
					throw new ArgumentOutOfRangeException("index", "Index was out of range. Must be non-negative and less than the size of the collection.");
				}
				return this._items[index];
			}
			set
			{
				if (index < 0 || index >= this._size)
				{
					throw new ArgumentOutOfRangeException("index", "Index was out of range. Must be non-negative and less than the size of the collection.");
				}
				this._items[index] = value;
				this._version++;
			}
		}

		/// <summary>Creates an <see cref="T:System.Collections.ArrayList" /> wrapper for a specific <see cref="T:System.Collections.IList" />.</summary>
		/// <param name="list">The <see cref="T:System.Collections.IList" /> to wrap.</param>
		/// <returns>The <see cref="T:System.Collections.ArrayList" /> wrapper around the <see cref="T:System.Collections.IList" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="list" /> is <see langword="null" />.</exception>
		// Token: 0x06005D0E RID: 23822 RVA: 0x00138FB0 File Offset: 0x001371B0
		public static ArrayList Adapter(IList list)
		{
			if (list == null)
			{
				throw new ArgumentNullException("list");
			}
			return new ArrayList.IListWrapper(list);
		}

		/// <summary>Adds an object to the end of the <see cref="T:System.Collections.ArrayList" />.</summary>
		/// <param name="value">The <see cref="T:System.Object" /> to be added to the end of the <see cref="T:System.Collections.ArrayList" />. The value can be <see langword="null" />.</param>
		/// <returns>The <see cref="T:System.Collections.ArrayList" /> index at which the <paramref name="value" /> has been added.</returns>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.ArrayList" /> is read-only.  
		///  -or-  
		///  The <see cref="T:System.Collections.ArrayList" /> has a fixed size.</exception>
		// Token: 0x06005D0F RID: 23823 RVA: 0x00138FC8 File Offset: 0x001371C8
		public virtual int Add(object value)
		{
			if (this._size == this._items.Length)
			{
				this.EnsureCapacity(this._size + 1);
			}
			this._items[this._size] = value;
			this._version++;
			int size = this._size;
			this._size = size + 1;
			return size;
		}

		/// <summary>Adds the elements of an <see cref="T:System.Collections.ICollection" /> to the end of the <see cref="T:System.Collections.ArrayList" />.</summary>
		/// <param name="c">The <see cref="T:System.Collections.ICollection" /> whose elements should be added to the end of the <see cref="T:System.Collections.ArrayList" />. The collection itself cannot be <see langword="null" />, but it can contain elements that are <see langword="null" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="c" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.ArrayList" /> is read-only.  
		///  -or-  
		///  The <see cref="T:System.Collections.ArrayList" /> has a fixed size.</exception>
		// Token: 0x06005D10 RID: 23824 RVA: 0x00139020 File Offset: 0x00137220
		public virtual void AddRange(ICollection c)
		{
			this.InsertRange(this._size, c);
		}

		/// <summary>Searches a range of elements in the sorted <see cref="T:System.Collections.ArrayList" /> for an element using the specified comparer and returns the zero-based index of the element.</summary>
		/// <param name="index">The zero-based starting index of the range to search.</param>
		/// <param name="count">The length of the range to search.</param>
		/// <param name="value">The <see cref="T:System.Object" /> to locate. The value can be <see langword="null" />.</param>
		/// <param name="comparer">The <see cref="T:System.Collections.IComparer" /> implementation to use when comparing elements.  
		///  -or-  
		///  <see langword="null" /> to use the default comparer that is the <see cref="T:System.IComparable" /> implementation of each element.</param>
		/// <returns>The zero-based index of <paramref name="value" /> in the sorted <see cref="T:System.Collections.ArrayList" />, if <paramref name="value" /> is found; otherwise, a negative number, which is the bitwise complement of the index of the next element that is larger than <paramref name="value" /> or, if there is no larger element, the bitwise complement of <see cref="P:System.Collections.ArrayList.Count" />.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="index" /> and <paramref name="count" /> do not denote a valid range in the <see cref="T:System.Collections.ArrayList" />.  
		/// -or-  
		/// <paramref name="comparer" /> is <see langword="null" /> and neither <paramref name="value" /> nor the elements of <see cref="T:System.Collections.ArrayList" /> implement the <see cref="T:System.IComparable" /> interface.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="comparer" /> is <see langword="null" /> and <paramref name="value" /> is not of the same type as the elements of the <see cref="T:System.Collections.ArrayList" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero.  
		/// -or-  
		/// <paramref name="count" /> is less than zero.</exception>
		// Token: 0x06005D11 RID: 23825 RVA: 0x00139030 File Offset: 0x00137230
		public virtual int BinarySearch(int index, int count, object value, IComparer comparer)
		{
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index", "Non-negative number required.");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", "Non-negative number required.");
			}
			if (this._size - index < count)
			{
				throw new ArgumentException("Offset and length were out of bounds for the array or count is greater than the number of elements from index to the end of the source collection.");
			}
			return Array.BinarySearch(this._items, index, count, value, comparer);
		}

		/// <summary>Searches the entire sorted <see cref="T:System.Collections.ArrayList" /> for an element using the default comparer and returns the zero-based index of the element.</summary>
		/// <param name="value">The <see cref="T:System.Object" /> to locate. The value can be <see langword="null" />.</param>
		/// <returns>The zero-based index of <paramref name="value" /> in the sorted <see cref="T:System.Collections.ArrayList" />, if <paramref name="value" /> is found; otherwise, a negative number, which is the bitwise complement of the index of the next element that is larger than <paramref name="value" /> or, if there is no larger element, the bitwise complement of <see cref="P:System.Collections.ArrayList.Count" />.</returns>
		/// <exception cref="T:System.ArgumentException">Neither <paramref name="value" /> nor the elements of <see cref="T:System.Collections.ArrayList" /> implement the <see cref="T:System.IComparable" /> interface.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="value" /> is not of the same type as the elements of the <see cref="T:System.Collections.ArrayList" />.</exception>
		// Token: 0x06005D12 RID: 23826 RVA: 0x0013908B File Offset: 0x0013728B
		public virtual int BinarySearch(object value)
		{
			return this.BinarySearch(0, this.Count, value, null);
		}

		/// <summary>Searches the entire sorted <see cref="T:System.Collections.ArrayList" /> for an element using the specified comparer and returns the zero-based index of the element.</summary>
		/// <param name="value">The <see cref="T:System.Object" /> to locate. The value can be <see langword="null" />.</param>
		/// <param name="comparer">The <see cref="T:System.Collections.IComparer" /> implementation to use when comparing elements.  
		///  -or-  
		///  <see langword="null" /> to use the default comparer that is the <see cref="T:System.IComparable" /> implementation of each element.</param>
		/// <returns>The zero-based index of <paramref name="value" /> in the sorted <see cref="T:System.Collections.ArrayList" />, if <paramref name="value" /> is found; otherwise, a negative number, which is the bitwise complement of the index of the next element that is larger than <paramref name="value" /> or, if there is no larger element, the bitwise complement of <see cref="P:System.Collections.ArrayList.Count" />.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="comparer" /> is <see langword="null" /> and neither <paramref name="value" /> nor the elements of <see cref="T:System.Collections.ArrayList" /> implement the <see cref="T:System.IComparable" /> interface.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="comparer" /> is <see langword="null" /> and <paramref name="value" /> is not of the same type as the elements of the <see cref="T:System.Collections.ArrayList" />.</exception>
		// Token: 0x06005D13 RID: 23827 RVA: 0x0013909C File Offset: 0x0013729C
		public virtual int BinarySearch(object value, IComparer comparer)
		{
			return this.BinarySearch(0, this.Count, value, comparer);
		}

		/// <summary>Removes all elements from the <see cref="T:System.Collections.ArrayList" />.</summary>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.ArrayList" /> is read-only.  
		///  -or-  
		///  The <see cref="T:System.Collections.ArrayList" /> has a fixed size.</exception>
		// Token: 0x06005D14 RID: 23828 RVA: 0x001390AD File Offset: 0x001372AD
		public virtual void Clear()
		{
			if (this._size > 0)
			{
				Array.Clear(this._items, 0, this._size);
				this._size = 0;
			}
			this._version++;
		}

		/// <summary>Creates a shallow copy of the <see cref="T:System.Collections.ArrayList" />.</summary>
		/// <returns>A shallow copy of the <see cref="T:System.Collections.ArrayList" />.</returns>
		// Token: 0x06005D15 RID: 23829 RVA: 0x001390E0 File Offset: 0x001372E0
		public virtual object Clone()
		{
			ArrayList arrayList = new ArrayList(this._size);
			arrayList._size = this._size;
			arrayList._version = this._version;
			Array.Copy(this._items, 0, arrayList._items, 0, this._size);
			return arrayList;
		}

		/// <summary>Determines whether an element is in the <see cref="T:System.Collections.ArrayList" />.</summary>
		/// <param name="item">The <see cref="T:System.Object" /> to locate in the <see cref="T:System.Collections.ArrayList" />. The value can be <see langword="null" />.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="item" /> is found in the <see cref="T:System.Collections.ArrayList" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x06005D16 RID: 23830 RVA: 0x0013912C File Offset: 0x0013732C
		public virtual bool Contains(object item)
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
			for (int j = 0; j < this._size; j++)
			{
				if (this._items[j] != null && this._items[j].Equals(item))
				{
					return true;
				}
			}
			return false;
		}

		/// <summary>Copies the entire <see cref="T:System.Collections.ArrayList" /> to a compatible one-dimensional <see cref="T:System.Array" />, starting at the beginning of the target array.</summary>
		/// <param name="array">The one-dimensional <see cref="T:System.Array" /> that is the destination of the elements copied from <see cref="T:System.Collections.ArrayList" />. The <see cref="T:System.Array" /> must have zero-based indexing.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="array" /> is multidimensional.  
		/// -or-  
		/// The number of elements in the source <see cref="T:System.Collections.ArrayList" /> is greater than the number of elements that the destination <paramref name="array" /> can contain.</exception>
		/// <exception cref="T:System.InvalidCastException">The type of the source <see cref="T:System.Collections.ArrayList" /> cannot be cast automatically to the type of the destination <paramref name="array" />.</exception>
		// Token: 0x06005D17 RID: 23831 RVA: 0x00139189 File Offset: 0x00137389
		public virtual void CopyTo(Array array)
		{
			this.CopyTo(array, 0);
		}

		/// <summary>Copies the entire <see cref="T:System.Collections.ArrayList" /> to a compatible one-dimensional <see cref="T:System.Array" />, starting at the specified index of the target array.</summary>
		/// <param name="array">The one-dimensional <see cref="T:System.Array" /> that is the destination of the elements copied from <see cref="T:System.Collections.ArrayList" />. The <see cref="T:System.Array" /> must have zero-based indexing.</param>
		/// <param name="arrayIndex">The zero-based index in <paramref name="array" /> at which copying begins.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="arrayIndex" /> is less than zero.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="array" /> is multidimensional.  
		/// -or-  
		/// The number of elements in the source <see cref="T:System.Collections.ArrayList" /> is greater than the available space from <paramref name="arrayIndex" /> to the end of the destination <paramref name="array" />.</exception>
		/// <exception cref="T:System.InvalidCastException">The type of the source <see cref="T:System.Collections.ArrayList" /> cannot be cast automatically to the type of the destination <paramref name="array" />.</exception>
		// Token: 0x06005D18 RID: 23832 RVA: 0x00139193 File Offset: 0x00137393
		public virtual void CopyTo(Array array, int arrayIndex)
		{
			if (array != null && array.Rank != 1)
			{
				throw new ArgumentException("Only single dimensional arrays are supported for the requested action.", "array");
			}
			Array.Copy(this._items, 0, array, arrayIndex, this._size);
		}

		/// <summary>Copies a range of elements from the <see cref="T:System.Collections.ArrayList" /> to a compatible one-dimensional <see cref="T:System.Array" />, starting at the specified index of the target array.</summary>
		/// <param name="index">The zero-based index in the source <see cref="T:System.Collections.ArrayList" /> at which copying begins.</param>
		/// <param name="array">The one-dimensional <see cref="T:System.Array" /> that is the destination of the elements copied from <see cref="T:System.Collections.ArrayList" />. The <see cref="T:System.Array" /> must have zero-based indexing.</param>
		/// <param name="arrayIndex">The zero-based index in <paramref name="array" /> at which copying begins.</param>
		/// <param name="count">The number of elements to copy.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero.  
		/// -or-  
		/// <paramref name="arrayIndex" /> is less than zero.  
		/// -or-  
		/// <paramref name="count" /> is less than zero.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="array" /> is multidimensional.  
		/// -or-  
		/// <paramref name="index" /> is equal to or greater than the <see cref="P:System.Collections.ArrayList.Count" /> of the source <see cref="T:System.Collections.ArrayList" />.  
		/// -or-  
		/// The number of elements from <paramref name="index" /> to the end of the source <see cref="T:System.Collections.ArrayList" /> is greater than the available space from <paramref name="arrayIndex" /> to the end of the destination <paramref name="array" />.</exception>
		/// <exception cref="T:System.InvalidCastException">The type of the source <see cref="T:System.Collections.ArrayList" /> cannot be cast automatically to the type of the destination <paramref name="array" />.</exception>
		// Token: 0x06005D19 RID: 23833 RVA: 0x001391C8 File Offset: 0x001373C8
		public virtual void CopyTo(int index, Array array, int arrayIndex, int count)
		{
			if (this._size - index < count)
			{
				throw new ArgumentException("Offset and length were out of bounds for the array or count is greater than the number of elements from index to the end of the source collection.");
			}
			if (array != null && array.Rank != 1)
			{
				throw new ArgumentException("Only single dimensional arrays are supported for the requested action.", "array");
			}
			Array.Copy(this._items, index, array, arrayIndex, count);
		}

		// Token: 0x06005D1A RID: 23834 RVA: 0x00139218 File Offset: 0x00137418
		private void EnsureCapacity(int min)
		{
			if (this._items.Length < min)
			{
				int num = (this._items.Length == 0) ? 4 : (this._items.Length * 2);
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
		}

		/// <summary>Returns an <see cref="T:System.Collections.IList" /> wrapper with a fixed size.</summary>
		/// <param name="list">The <see cref="T:System.Collections.IList" /> to wrap.</param>
		/// <returns>An <see cref="T:System.Collections.IList" /> wrapper with a fixed size.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="list" /> is <see langword="null" />.</exception>
		// Token: 0x06005D1B RID: 23835 RVA: 0x00139262 File Offset: 0x00137462
		public static IList FixedSize(IList list)
		{
			if (list == null)
			{
				throw new ArgumentNullException("list");
			}
			return new ArrayList.FixedSizeList(list);
		}

		/// <summary>Returns an <see cref="T:System.Collections.ArrayList" /> wrapper with a fixed size.</summary>
		/// <param name="list">The <see cref="T:System.Collections.ArrayList" /> to wrap.</param>
		/// <returns>An <see cref="T:System.Collections.ArrayList" /> wrapper with a fixed size.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="list" /> is <see langword="null" />.</exception>
		// Token: 0x06005D1C RID: 23836 RVA: 0x00139278 File Offset: 0x00137478
		public static ArrayList FixedSize(ArrayList list)
		{
			if (list == null)
			{
				throw new ArgumentNullException("list");
			}
			return new ArrayList.FixedSizeArrayList(list);
		}

		/// <summary>Returns an enumerator for the entire <see cref="T:System.Collections.ArrayList" />.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> for the entire <see cref="T:System.Collections.ArrayList" />.</returns>
		// Token: 0x06005D1D RID: 23837 RVA: 0x0013928E File Offset: 0x0013748E
		public virtual IEnumerator GetEnumerator()
		{
			return new ArrayList.ArrayListEnumeratorSimple(this);
		}

		/// <summary>Returns an enumerator for a range of elements in the <see cref="T:System.Collections.ArrayList" />.</summary>
		/// <param name="index">The zero-based starting index of the <see cref="T:System.Collections.ArrayList" /> section that the enumerator should refer to.</param>
		/// <param name="count">The number of elements in the <see cref="T:System.Collections.ArrayList" /> section that the enumerator should refer to.</param>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> for the specified range of elements in the <see cref="T:System.Collections.ArrayList" />.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero.  
		/// -or-  
		/// <paramref name="count" /> is less than zero.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="index" /> and <paramref name="count" /> do not specify a valid range in the <see cref="T:System.Collections.ArrayList" />.</exception>
		// Token: 0x06005D1E RID: 23838 RVA: 0x00139298 File Offset: 0x00137498
		public virtual IEnumerator GetEnumerator(int index, int count)
		{
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index", "Non-negative number required.");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", "Non-negative number required.");
			}
			if (this._size - index < count)
			{
				throw new ArgumentException("Offset and length were out of bounds for the array or count is greater than the number of elements from index to the end of the source collection.");
			}
			return new ArrayList.ArrayListEnumerator(this, index, count);
		}

		/// <summary>Searches for the specified <see cref="T:System.Object" /> and returns the zero-based index of the first occurrence within the entire <see cref="T:System.Collections.ArrayList" />.</summary>
		/// <param name="value">The <see cref="T:System.Object" /> to locate in the <see cref="T:System.Collections.ArrayList" />. The value can be <see langword="null" />.</param>
		/// <returns>The zero-based index of the first occurrence of <paramref name="value" /> within the entire <see cref="T:System.Collections.ArrayList" />, if found; otherwise, -1.</returns>
		// Token: 0x06005D1F RID: 23839 RVA: 0x001392EB File Offset: 0x001374EB
		public virtual int IndexOf(object value)
		{
			return Array.IndexOf(this._items, value, 0, this._size);
		}

		/// <summary>Searches for the specified <see cref="T:System.Object" /> and returns the zero-based index of the first occurrence within the range of elements in the <see cref="T:System.Collections.ArrayList" /> that extends from the specified index to the last element.</summary>
		/// <param name="value">The <see cref="T:System.Object" /> to locate in the <see cref="T:System.Collections.ArrayList" />. The value can be <see langword="null" />.</param>
		/// <param name="startIndex">The zero-based starting index of the search. 0 (zero) is valid in an empty list.</param>
		/// <returns>The zero-based index of the first occurrence of <paramref name="value" /> within the range of elements in the <see cref="T:System.Collections.ArrayList" /> that extends from <paramref name="startIndex" /> to the last element, if found; otherwise, -1.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="startIndex" /> is outside the range of valid indexes for the <see cref="T:System.Collections.ArrayList" />.</exception>
		// Token: 0x06005D20 RID: 23840 RVA: 0x00139300 File Offset: 0x00137500
		public virtual int IndexOf(object value, int startIndex)
		{
			if (startIndex > this._size)
			{
				throw new ArgumentOutOfRangeException("startIndex", "Index was out of range. Must be non-negative and less than the size of the collection.");
			}
			return Array.IndexOf(this._items, value, startIndex, this._size - startIndex);
		}

		/// <summary>Searches for the specified <see cref="T:System.Object" /> and returns the zero-based index of the first occurrence within the range of elements in the <see cref="T:System.Collections.ArrayList" /> that starts at the specified index and contains the specified number of elements.</summary>
		/// <param name="value">The <see cref="T:System.Object" /> to locate in the <see cref="T:System.Collections.ArrayList" />. The value can be <see langword="null" />.</param>
		/// <param name="startIndex">The zero-based starting index of the search. 0 (zero) is valid in an empty list.</param>
		/// <param name="count">The number of elements in the section to search.</param>
		/// <returns>The zero-based index of the first occurrence of <paramref name="value" /> within the range of elements in the <see cref="T:System.Collections.ArrayList" /> that starts at <paramref name="startIndex" /> and contains <paramref name="count" /> number of elements, if found; otherwise, -1.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="startIndex" /> is outside the range of valid indexes for the <see cref="T:System.Collections.ArrayList" />.  
		/// -or-  
		/// <paramref name="count" /> is less than zero.  
		/// -or-  
		/// <paramref name="startIndex" /> and <paramref name="count" /> do not specify a valid section in the <see cref="T:System.Collections.ArrayList" />.</exception>
		// Token: 0x06005D21 RID: 23841 RVA: 0x00139330 File Offset: 0x00137530
		public virtual int IndexOf(object value, int startIndex, int count)
		{
			if (startIndex > this._size)
			{
				throw new ArgumentOutOfRangeException("startIndex", "Index was out of range. Must be non-negative and less than the size of the collection.");
			}
			if (count < 0 || startIndex > this._size - count)
			{
				throw new ArgumentOutOfRangeException("count", "Count must be positive and count must refer to a location within the string/array/collection.");
			}
			return Array.IndexOf(this._items, value, startIndex, count);
		}

		/// <summary>Inserts an element into the <see cref="T:System.Collections.ArrayList" /> at the specified index.</summary>
		/// <param name="index">The zero-based index at which <paramref name="value" /> should be inserted.</param>
		/// <param name="value">The <see cref="T:System.Object" /> to insert. The value can be <see langword="null" />.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero.  
		/// -or-  
		/// <paramref name="index" /> is greater than <see cref="P:System.Collections.ArrayList.Count" />.</exception>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.ArrayList" /> is read-only.  
		///  -or-  
		///  The <see cref="T:System.Collections.ArrayList" /> has a fixed size.</exception>
		// Token: 0x06005D22 RID: 23842 RVA: 0x00139384 File Offset: 0x00137584
		public virtual void Insert(int index, object value)
		{
			if (index < 0 || index > this._size)
			{
				throw new ArgumentOutOfRangeException("index", "Insertion index was out of range. Must be non-negative and less than or equal to size.");
			}
			if (this._size == this._items.Length)
			{
				this.EnsureCapacity(this._size + 1);
			}
			if (index < this._size)
			{
				Array.Copy(this._items, index, this._items, index + 1, this._size - index);
			}
			this._items[index] = value;
			this._size++;
			this._version++;
		}

		/// <summary>Inserts the elements of a collection into the <see cref="T:System.Collections.ArrayList" /> at the specified index.</summary>
		/// <param name="index">The zero-based index at which the new elements should be inserted.</param>
		/// <param name="c">The <see cref="T:System.Collections.ICollection" /> whose elements should be inserted into the <see cref="T:System.Collections.ArrayList" />. The collection itself cannot be <see langword="null" />, but it can contain elements that are <see langword="null" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="c" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero.  
		/// -or-  
		/// <paramref name="index" /> is greater than <see cref="P:System.Collections.ArrayList.Count" />.</exception>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.ArrayList" /> is read-only.  
		///  -or-  
		///  The <see cref="T:System.Collections.ArrayList" /> has a fixed size.</exception>
		// Token: 0x06005D23 RID: 23843 RVA: 0x00139418 File Offset: 0x00137618
		public virtual void InsertRange(int index, ICollection c)
		{
			if (c == null)
			{
				throw new ArgumentNullException("c", "Collection cannot be null.");
			}
			if (index < 0 || index > this._size)
			{
				throw new ArgumentOutOfRangeException("index", "Index was out of range. Must be non-negative and less than the size of the collection.");
			}
			int count = c.Count;
			if (count > 0)
			{
				this.EnsureCapacity(this._size + count);
				if (index < this._size)
				{
					Array.Copy(this._items, index, this._items, index + count, this._size - index);
				}
				object[] array = new object[count];
				c.CopyTo(array, 0);
				array.CopyTo(this._items, index);
				this._size += count;
				this._version++;
			}
		}

		/// <summary>Searches for the specified <see cref="T:System.Object" /> and returns the zero-based index of the last occurrence within the entire <see cref="T:System.Collections.ArrayList" />.</summary>
		/// <param name="value">The <see cref="T:System.Object" /> to locate in the <see cref="T:System.Collections.ArrayList" />. The value can be <see langword="null" />.</param>
		/// <returns>The zero-based index of the last occurrence of <paramref name="value" /> within the entire the <see cref="T:System.Collections.ArrayList" />, if found; otherwise, -1.</returns>
		// Token: 0x06005D24 RID: 23844 RVA: 0x001394CC File Offset: 0x001376CC
		public virtual int LastIndexOf(object value)
		{
			return this.LastIndexOf(value, this._size - 1, this._size);
		}

		/// <summary>Searches for the specified <see cref="T:System.Object" /> and returns the zero-based index of the last occurrence within the range of elements in the <see cref="T:System.Collections.ArrayList" /> that extends from the first element to the specified index.</summary>
		/// <param name="value">The <see cref="T:System.Object" /> to locate in the <see cref="T:System.Collections.ArrayList" />. The value can be <see langword="null" />.</param>
		/// <param name="startIndex">The zero-based starting index of the backward search.</param>
		/// <returns>The zero-based index of the last occurrence of <paramref name="value" /> within the range of elements in the <see cref="T:System.Collections.ArrayList" /> that extends from the first element to <paramref name="startIndex" />, if found; otherwise, -1.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="startIndex" /> is outside the range of valid indexes for the <see cref="T:System.Collections.ArrayList" />.</exception>
		// Token: 0x06005D25 RID: 23845 RVA: 0x001394E3 File Offset: 0x001376E3
		public virtual int LastIndexOf(object value, int startIndex)
		{
			if (startIndex >= this._size)
			{
				throw new ArgumentOutOfRangeException("startIndex", "Index was out of range. Must be non-negative and less than the size of the collection.");
			}
			return this.LastIndexOf(value, startIndex, startIndex + 1);
		}

		/// <summary>Searches for the specified <see cref="T:System.Object" /> and returns the zero-based index of the last occurrence within the range of elements in the <see cref="T:System.Collections.ArrayList" /> that contains the specified number of elements and ends at the specified index.</summary>
		/// <param name="value">The <see cref="T:System.Object" /> to locate in the <see cref="T:System.Collections.ArrayList" />. The value can be <see langword="null" />.</param>
		/// <param name="startIndex">The zero-based starting index of the backward search.</param>
		/// <param name="count">The number of elements in the section to search.</param>
		/// <returns>The zero-based index of the last occurrence of <paramref name="value" /> within the range of elements in the <see cref="T:System.Collections.ArrayList" /> that contains <paramref name="count" /> number of elements and ends at <paramref name="startIndex" />, if found; otherwise, -1.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="startIndex" /> is outside the range of valid indexes for the <see cref="T:System.Collections.ArrayList" />.  
		/// -or-  
		/// <paramref name="count" /> is less than zero.  
		/// -or-  
		/// <paramref name="startIndex" /> and <paramref name="count" /> do not specify a valid section in the <see cref="T:System.Collections.ArrayList" />.</exception>
		// Token: 0x06005D26 RID: 23846 RVA: 0x0013950C File Offset: 0x0013770C
		public virtual int LastIndexOf(object value, int startIndex, int count)
		{
			if (this.Count != 0 && (startIndex < 0 || count < 0))
			{
				throw new ArgumentOutOfRangeException((startIndex < 0) ? "startIndex" : "count", "Non-negative number required.");
			}
			if (this._size == 0)
			{
				return -1;
			}
			if (startIndex >= this._size || count > startIndex + 1)
			{
				throw new ArgumentOutOfRangeException((startIndex >= this._size) ? "startIndex" : "count", "Must be less than or equal to the size of the collection.");
			}
			return Array.LastIndexOf(this._items, value, startIndex, count);
		}

		/// <summary>Returns a read-only <see cref="T:System.Collections.IList" /> wrapper.</summary>
		/// <param name="list">The <see cref="T:System.Collections.IList" /> to wrap.</param>
		/// <returns>A read-only <see cref="T:System.Collections.IList" /> wrapper around <paramref name="list" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="list" /> is <see langword="null" />.</exception>
		// Token: 0x06005D27 RID: 23847 RVA: 0x0013958B File Offset: 0x0013778B
		public static IList ReadOnly(IList list)
		{
			if (list == null)
			{
				throw new ArgumentNullException("list");
			}
			return new ArrayList.ReadOnlyList(list);
		}

		/// <summary>Returns a read-only <see cref="T:System.Collections.ArrayList" /> wrapper.</summary>
		/// <param name="list">The <see cref="T:System.Collections.ArrayList" /> to wrap.</param>
		/// <returns>A read-only <see cref="T:System.Collections.ArrayList" /> wrapper around <paramref name="list" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="list" /> is <see langword="null" />.</exception>
		// Token: 0x06005D28 RID: 23848 RVA: 0x001395A1 File Offset: 0x001377A1
		public static ArrayList ReadOnly(ArrayList list)
		{
			if (list == null)
			{
				throw new ArgumentNullException("list");
			}
			return new ArrayList.ReadOnlyArrayList(list);
		}

		/// <summary>Removes the first occurrence of a specific object from the <see cref="T:System.Collections.ArrayList" />.</summary>
		/// <param name="obj">The <see cref="T:System.Object" /> to remove from the <see cref="T:System.Collections.ArrayList" />. The value can be <see langword="null" />.</param>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.ArrayList" /> is read-only.  
		///  -or-  
		///  The <see cref="T:System.Collections.ArrayList" /> has a fixed size.</exception>
		// Token: 0x06005D29 RID: 23849 RVA: 0x001395B8 File Offset: 0x001377B8
		public virtual void Remove(object obj)
		{
			int num = this.IndexOf(obj);
			if (num >= 0)
			{
				this.RemoveAt(num);
			}
		}

		/// <summary>Removes the element at the specified index of the <see cref="T:System.Collections.ArrayList" />.</summary>
		/// <param name="index">The zero-based index of the element to remove.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero.  
		/// -or-  
		/// <paramref name="index" /> is equal to or greater than <see cref="P:System.Collections.ArrayList.Count" />.</exception>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.ArrayList" /> is read-only.  
		///  -or-  
		///  The <see cref="T:System.Collections.ArrayList" /> has a fixed size.</exception>
		// Token: 0x06005D2A RID: 23850 RVA: 0x001395D8 File Offset: 0x001377D8
		public virtual void RemoveAt(int index)
		{
			if (index < 0 || index >= this._size)
			{
				throw new ArgumentOutOfRangeException("index", "Index was out of range. Must be non-negative and less than the size of the collection.");
			}
			this._size--;
			if (index < this._size)
			{
				Array.Copy(this._items, index + 1, this._items, index, this._size - index);
			}
			this._items[this._size] = null;
			this._version++;
		}

		/// <summary>Removes a range of elements from the <see cref="T:System.Collections.ArrayList" />.</summary>
		/// <param name="index">The zero-based starting index of the range of elements to remove.</param>
		/// <param name="count">The number of elements to remove.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero.  
		/// -or-  
		/// <paramref name="count" /> is less than zero.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="index" /> and <paramref name="count" /> do not denote a valid range of elements in the <see cref="T:System.Collections.ArrayList" />.</exception>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.ArrayList" /> is read-only.  
		///  -or-  
		///  The <see cref="T:System.Collections.ArrayList" /> has a fixed size.</exception>
		// Token: 0x06005D2B RID: 23851 RVA: 0x00139654 File Offset: 0x00137854
		public virtual void RemoveRange(int index, int count)
		{
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index", "Non-negative number required.");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", "Non-negative number required.");
			}
			if (this._size - index < count)
			{
				throw new ArgumentException("Offset and length were out of bounds for the array or count is greater than the number of elements from index to the end of the source collection.");
			}
			if (count > 0)
			{
				int i = this._size;
				this._size -= count;
				if (index < this._size)
				{
					Array.Copy(this._items, index + count, this._items, index, this._size - index);
				}
				while (i > this._size)
				{
					this._items[--i] = null;
				}
				this._version++;
			}
		}

		/// <summary>Returns an <see cref="T:System.Collections.ArrayList" /> whose elements are copies of the specified value.</summary>
		/// <param name="value">The <see cref="T:System.Object" /> to copy multiple times in the new <see cref="T:System.Collections.ArrayList" />. The value can be <see langword="null" />.</param>
		/// <param name="count">The number of times <paramref name="value" /> should be copied.</param>
		/// <returns>An <see cref="T:System.Collections.ArrayList" /> with <paramref name="count" /> number of elements, all of which are copies of <paramref name="value" />.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="count" /> is less than zero.</exception>
		// Token: 0x06005D2C RID: 23852 RVA: 0x00139704 File Offset: 0x00137904
		public static ArrayList Repeat(object value, int count)
		{
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", "Non-negative number required.");
			}
			ArrayList arrayList = new ArrayList((count > 4) ? count : 4);
			for (int i = 0; i < count; i++)
			{
				arrayList.Add(value);
			}
			return arrayList;
		}

		/// <summary>Reverses the order of the elements in the entire <see cref="T:System.Collections.ArrayList" />.</summary>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.ArrayList" /> is read-only.</exception>
		// Token: 0x06005D2D RID: 23853 RVA: 0x00139748 File Offset: 0x00137948
		public virtual void Reverse()
		{
			this.Reverse(0, this.Count);
		}

		/// <summary>Reverses the order of the elements in the specified range.</summary>
		/// <param name="index">The zero-based starting index of the range to reverse.</param>
		/// <param name="count">The number of elements in the range to reverse.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero.  
		/// -or-  
		/// <paramref name="count" /> is less than zero.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="index" /> and <paramref name="count" /> do not denote a valid range of elements in the <see cref="T:System.Collections.ArrayList" />.</exception>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.ArrayList" /> is read-only.</exception>
		// Token: 0x06005D2E RID: 23854 RVA: 0x00139758 File Offset: 0x00137958
		public virtual void Reverse(int index, int count)
		{
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index", "Non-negative number required.");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", "Non-negative number required.");
			}
			if (this._size - index < count)
			{
				throw new ArgumentException("Offset and length were out of bounds for the array or count is greater than the number of elements from index to the end of the source collection.");
			}
			Array.Reverse<object>(this._items, index, count);
			this._version++;
		}

		/// <summary>Copies the elements of a collection over a range of elements in the <see cref="T:System.Collections.ArrayList" />.</summary>
		/// <param name="index">The zero-based <see cref="T:System.Collections.ArrayList" /> index at which to start copying the elements of <paramref name="c" />.</param>
		/// <param name="c">The <see cref="T:System.Collections.ICollection" /> whose elements to copy to the <see cref="T:System.Collections.ArrayList" />. The collection itself cannot be <see langword="null" />, but it can contain elements that are <see langword="null" />.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero.  
		/// -or-  
		/// <paramref name="index" /> plus the number of elements in <paramref name="c" /> is greater than <see cref="P:System.Collections.ArrayList.Count" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="c" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.ArrayList" /> is read-only.</exception>
		// Token: 0x06005D2F RID: 23855 RVA: 0x001397C0 File Offset: 0x001379C0
		public virtual void SetRange(int index, ICollection c)
		{
			if (c == null)
			{
				throw new ArgumentNullException("c", "Collection cannot be null.");
			}
			int count = c.Count;
			if (index < 0 || index > this._size - count)
			{
				throw new ArgumentOutOfRangeException("index", "Index was out of range. Must be non-negative and less than the size of the collection.");
			}
			if (count > 0)
			{
				c.CopyTo(this._items, index);
				this._version++;
			}
		}

		/// <summary>Returns an <see cref="T:System.Collections.ArrayList" /> which represents a subset of the elements in the source <see cref="T:System.Collections.ArrayList" />.</summary>
		/// <param name="index">The zero-based <see cref="T:System.Collections.ArrayList" /> index at which the range starts.</param>
		/// <param name="count">The number of elements in the range.</param>
		/// <returns>An <see cref="T:System.Collections.ArrayList" /> which represents a subset of the elements in the source <see cref="T:System.Collections.ArrayList" />.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero.  
		/// -or-  
		/// <paramref name="count" /> is less than zero.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="index" /> and <paramref name="count" /> do not denote a valid range of elements in the <see cref="T:System.Collections.ArrayList" />.</exception>
		// Token: 0x06005D30 RID: 23856 RVA: 0x00139828 File Offset: 0x00137A28
		public virtual ArrayList GetRange(int index, int count)
		{
			if (index < 0 || count < 0)
			{
				throw new ArgumentOutOfRangeException((index < 0) ? "index" : "count", "Non-negative number required.");
			}
			if (this._size - index < count)
			{
				throw new ArgumentException("Offset and length were out of bounds for the array or count is greater than the number of elements from index to the end of the source collection.");
			}
			return new ArrayList.Range(this, index, count);
		}

		/// <summary>Sorts the elements in the entire <see cref="T:System.Collections.ArrayList" />.</summary>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.ArrayList" /> is read-only.</exception>
		// Token: 0x06005D31 RID: 23857 RVA: 0x00139876 File Offset: 0x00137A76
		public virtual void Sort()
		{
			this.Sort(0, this.Count, Comparer.Default);
		}

		/// <summary>Sorts the elements in the entire <see cref="T:System.Collections.ArrayList" /> using the specified comparer.</summary>
		/// <param name="comparer">The <see cref="T:System.Collections.IComparer" /> implementation to use when comparing elements.  
		///  -or-  
		///  A null reference (<see langword="Nothing" /> in Visual Basic) to use the <see cref="T:System.IComparable" /> implementation of each element.</param>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.ArrayList" /> is read-only.</exception>
		/// <exception cref="T:System.InvalidOperationException">An error occurred while comparing two elements.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <see langword="null" /> is passed for <paramref name="comparer" />, and the elements in the list do not implement <see cref="T:System.IComparable" />.</exception>
		// Token: 0x06005D32 RID: 23858 RVA: 0x0013988A File Offset: 0x00137A8A
		public virtual void Sort(IComparer comparer)
		{
			this.Sort(0, this.Count, comparer);
		}

		/// <summary>Sorts the elements in a range of elements in <see cref="T:System.Collections.ArrayList" /> using the specified comparer.</summary>
		/// <param name="index">The zero-based starting index of the range to sort.</param>
		/// <param name="count">The length of the range to sort.</param>
		/// <param name="comparer">The <see cref="T:System.Collections.IComparer" /> implementation to use when comparing elements.  
		///  -or-  
		///  A null reference (<see langword="Nothing" /> in Visual Basic) to use the <see cref="T:System.IComparable" /> implementation of each element.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero.  
		/// -or-  
		/// <paramref name="count" /> is less than zero.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="index" /> and <paramref name="count" /> do not specify a valid range in the <see cref="T:System.Collections.ArrayList" />.</exception>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.ArrayList" /> is read-only.</exception>
		/// <exception cref="T:System.InvalidOperationException">An error occurred while comparing two elements.</exception>
		// Token: 0x06005D33 RID: 23859 RVA: 0x0013989C File Offset: 0x00137A9C
		public virtual void Sort(int index, int count, IComparer comparer)
		{
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index", "Non-negative number required.");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", "Non-negative number required.");
			}
			if (this._size - index < count)
			{
				throw new ArgumentException("Offset and length were out of bounds for the array or count is greater than the number of elements from index to the end of the source collection.");
			}
			Array.Sort(this._items, index, count, comparer);
			this._version++;
		}

		/// <summary>Returns an <see cref="T:System.Collections.IList" /> wrapper that is synchronized (thread safe).</summary>
		/// <param name="list">The <see cref="T:System.Collections.IList" /> to synchronize.</param>
		/// <returns>An <see cref="T:System.Collections.IList" /> wrapper that is synchronized (thread safe).</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="list" /> is <see langword="null" />.</exception>
		// Token: 0x06005D34 RID: 23860 RVA: 0x00139903 File Offset: 0x00137B03
		public static IList Synchronized(IList list)
		{
			if (list == null)
			{
				throw new ArgumentNullException("list");
			}
			return new ArrayList.SyncIList(list);
		}

		/// <summary>Returns an <see cref="T:System.Collections.ArrayList" /> wrapper that is synchronized (thread safe).</summary>
		/// <param name="list">The <see cref="T:System.Collections.ArrayList" /> to synchronize.</param>
		/// <returns>An <see cref="T:System.Collections.ArrayList" /> wrapper that is synchronized (thread safe).</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="list" /> is <see langword="null" />.</exception>
		// Token: 0x06005D35 RID: 23861 RVA: 0x00139919 File Offset: 0x00137B19
		public static ArrayList Synchronized(ArrayList list)
		{
			if (list == null)
			{
				throw new ArgumentNullException("list");
			}
			return new ArrayList.SyncArrayList(list);
		}

		/// <summary>Copies the elements of the <see cref="T:System.Collections.ArrayList" /> to a new <see cref="T:System.Object" /> array.</summary>
		/// <returns>An <see cref="T:System.Object" /> array containing copies of the elements of the <see cref="T:System.Collections.ArrayList" />.</returns>
		// Token: 0x06005D36 RID: 23862 RVA: 0x00139930 File Offset: 0x00137B30
		public virtual object[] ToArray()
		{
			if (this._size == 0)
			{
				return Array.Empty<object>();
			}
			object[] array = new object[this._size];
			Array.Copy(this._items, 0, array, 0, this._size);
			return array;
		}

		/// <summary>Copies the elements of the <see cref="T:System.Collections.ArrayList" /> to a new array of the specified element type.</summary>
		/// <param name="type">The element <see cref="T:System.Type" /> of the destination array to create and copy elements to.</param>
		/// <returns>An array of the specified element type containing copies of the elements of the <see cref="T:System.Collections.ArrayList" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="type" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidCastException">The type of the source <see cref="T:System.Collections.ArrayList" /> cannot be cast automatically to the specified type.</exception>
		// Token: 0x06005D37 RID: 23863 RVA: 0x0013996C File Offset: 0x00137B6C
		public virtual Array ToArray(Type type)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			Array array = Array.CreateInstance(type, this._size);
			Array.Copy(this._items, 0, array, 0, this._size);
			return array;
		}

		/// <summary>Sets the capacity to the actual number of elements in the <see cref="T:System.Collections.ArrayList" />.</summary>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.ArrayList" /> is read-only.  
		///  -or-  
		///  The <see cref="T:System.Collections.ArrayList" /> has a fixed size.</exception>
		// Token: 0x06005D38 RID: 23864 RVA: 0x001399AF File Offset: 0x00137BAF
		public virtual void TrimToSize()
		{
			this.Capacity = this._size;
		}

		// Token: 0x040038D0 RID: 14544
		private object[] _items;

		// Token: 0x040038D1 RID: 14545
		private int _size;

		// Token: 0x040038D2 RID: 14546
		private int _version;

		// Token: 0x040038D3 RID: 14547
		[NonSerialized]
		private object _syncRoot;

		// Token: 0x040038D4 RID: 14548
		private const int _defaultCapacity = 4;

		// Token: 0x040038D5 RID: 14549
		internal const int MaxArrayLength = 2146435071;

		// Token: 0x02000A3C RID: 2620
		[Serializable]
		private class IListWrapper : ArrayList
		{
			// Token: 0x06005D39 RID: 23865 RVA: 0x001399BD File Offset: 0x00137BBD
			internal IListWrapper(IList list)
			{
				this._list = list;
				this._version = 0;
			}

			// Token: 0x1700103C RID: 4156
			// (get) Token: 0x06005D3A RID: 23866 RVA: 0x001399D3 File Offset: 0x00137BD3
			// (set) Token: 0x06005D3B RID: 23867 RVA: 0x001399E0 File Offset: 0x00137BE0
			public override int Capacity
			{
				get
				{
					return this._list.Count;
				}
				set
				{
					if (value < this.Count)
					{
						throw new ArgumentOutOfRangeException("value", "capacity was less than the current size.");
					}
				}
			}

			// Token: 0x1700103D RID: 4157
			// (get) Token: 0x06005D3C RID: 23868 RVA: 0x001399D3 File Offset: 0x00137BD3
			public override int Count
			{
				get
				{
					return this._list.Count;
				}
			}

			// Token: 0x1700103E RID: 4158
			// (get) Token: 0x06005D3D RID: 23869 RVA: 0x001399FB File Offset: 0x00137BFB
			public override bool IsReadOnly
			{
				get
				{
					return this._list.IsReadOnly;
				}
			}

			// Token: 0x1700103F RID: 4159
			// (get) Token: 0x06005D3E RID: 23870 RVA: 0x00139A08 File Offset: 0x00137C08
			public override bool IsFixedSize
			{
				get
				{
					return this._list.IsFixedSize;
				}
			}

			// Token: 0x17001040 RID: 4160
			// (get) Token: 0x06005D3F RID: 23871 RVA: 0x00139A15 File Offset: 0x00137C15
			public override bool IsSynchronized
			{
				get
				{
					return this._list.IsSynchronized;
				}
			}

			// Token: 0x17001041 RID: 4161
			public override object this[int index]
			{
				get
				{
					return this._list[index];
				}
				set
				{
					this._list[index] = value;
					this._version++;
				}
			}

			// Token: 0x17001042 RID: 4162
			// (get) Token: 0x06005D42 RID: 23874 RVA: 0x00139A4D File Offset: 0x00137C4D
			public override object SyncRoot
			{
				get
				{
					return this._list.SyncRoot;
				}
			}

			// Token: 0x06005D43 RID: 23875 RVA: 0x00139A5A File Offset: 0x00137C5A
			public override int Add(object obj)
			{
				int result = this._list.Add(obj);
				this._version++;
				return result;
			}

			// Token: 0x06005D44 RID: 23876 RVA: 0x00139A76 File Offset: 0x00137C76
			public override void AddRange(ICollection c)
			{
				this.InsertRange(this.Count, c);
			}

			// Token: 0x06005D45 RID: 23877 RVA: 0x00139A88 File Offset: 0x00137C88
			public override int BinarySearch(int index, int count, object value, IComparer comparer)
			{
				if (index < 0 || count < 0)
				{
					throw new ArgumentOutOfRangeException((index < 0) ? "index" : "count", "Non-negative number required.");
				}
				if (this.Count - index < count)
				{
					throw new ArgumentException("Offset and length were out of bounds for the array or count is greater than the number of elements from index to the end of the source collection.");
				}
				if (comparer == null)
				{
					comparer = Comparer.Default;
				}
				int i = index;
				int num = index + count - 1;
				while (i <= num)
				{
					int num2 = (i + num) / 2;
					int num3 = comparer.Compare(value, this._list[num2]);
					if (num3 == 0)
					{
						return num2;
					}
					if (num3 < 0)
					{
						num = num2 - 1;
					}
					else
					{
						i = num2 + 1;
					}
				}
				return ~i;
			}

			// Token: 0x06005D46 RID: 23878 RVA: 0x00139B17 File Offset: 0x00137D17
			public override void Clear()
			{
				if (this._list.IsFixedSize)
				{
					throw new NotSupportedException("Collection was of a fixed size.");
				}
				this._list.Clear();
				this._version++;
			}

			// Token: 0x06005D47 RID: 23879 RVA: 0x00139B4A File Offset: 0x00137D4A
			public override object Clone()
			{
				return new ArrayList.IListWrapper(this._list);
			}

			// Token: 0x06005D48 RID: 23880 RVA: 0x00139B57 File Offset: 0x00137D57
			public override bool Contains(object obj)
			{
				return this._list.Contains(obj);
			}

			// Token: 0x06005D49 RID: 23881 RVA: 0x00139B65 File Offset: 0x00137D65
			public override void CopyTo(Array array, int index)
			{
				this._list.CopyTo(array, index);
			}

			// Token: 0x06005D4A RID: 23882 RVA: 0x00139B74 File Offset: 0x00137D74
			public override void CopyTo(int index, Array array, int arrayIndex, int count)
			{
				if (array == null)
				{
					throw new ArgumentNullException("array");
				}
				if (index < 0 || arrayIndex < 0)
				{
					throw new ArgumentOutOfRangeException((index < 0) ? "index" : "arrayIndex", "Non-negative number required.");
				}
				if (count < 0)
				{
					throw new ArgumentOutOfRangeException("count", "Non-negative number required.");
				}
				if (array.Length - arrayIndex < count)
				{
					throw new ArgumentException("Offset and length were out of bounds for the array or count is greater than the number of elements from index to the end of the source collection.");
				}
				if (array.Rank != 1)
				{
					throw new ArgumentException("Only single dimensional arrays are supported for the requested action.", "array");
				}
				if (this._list.Count - index < count)
				{
					throw new ArgumentException("Offset and length were out of bounds for the array or count is greater than the number of elements from index to the end of the source collection.");
				}
				for (int i = index; i < index + count; i++)
				{
					array.SetValue(this._list[i], arrayIndex++);
				}
			}

			// Token: 0x06005D4B RID: 23883 RVA: 0x00139C3A File Offset: 0x00137E3A
			public override IEnumerator GetEnumerator()
			{
				return this._list.GetEnumerator();
			}

			// Token: 0x06005D4C RID: 23884 RVA: 0x00139C48 File Offset: 0x00137E48
			public override IEnumerator GetEnumerator(int index, int count)
			{
				if (index < 0 || count < 0)
				{
					throw new ArgumentOutOfRangeException((index < 0) ? "index" : "count", "Non-negative number required.");
				}
				if (this._list.Count - index < count)
				{
					throw new ArgumentException("Offset and length were out of bounds for the array or count is greater than the number of elements from index to the end of the source collection.");
				}
				return new ArrayList.IListWrapper.IListWrapperEnumWrapper(this, index, count);
			}

			// Token: 0x06005D4D RID: 23885 RVA: 0x00139C9B File Offset: 0x00137E9B
			public override int IndexOf(object value)
			{
				return this._list.IndexOf(value);
			}

			// Token: 0x06005D4E RID: 23886 RVA: 0x00139CA9 File Offset: 0x00137EA9
			public override int IndexOf(object value, int startIndex)
			{
				return this.IndexOf(value, startIndex, this._list.Count - startIndex);
			}

			// Token: 0x06005D4F RID: 23887 RVA: 0x00139CC0 File Offset: 0x00137EC0
			public override int IndexOf(object value, int startIndex, int count)
			{
				if (startIndex < 0 || startIndex > this.Count)
				{
					throw new ArgumentOutOfRangeException("startIndex", "Index was out of range. Must be non-negative and less than the size of the collection.");
				}
				if (count < 0 || startIndex > this.Count - count)
				{
					throw new ArgumentOutOfRangeException("count", "Count must be positive and count must refer to a location within the string/array/collection.");
				}
				int num = startIndex + count;
				if (value == null)
				{
					for (int i = startIndex; i < num; i++)
					{
						if (this._list[i] == null)
						{
							return i;
						}
					}
					return -1;
				}
				for (int j = startIndex; j < num; j++)
				{
					if (this._list[j] != null && this._list[j].Equals(value))
					{
						return j;
					}
				}
				return -1;
			}

			// Token: 0x06005D50 RID: 23888 RVA: 0x00139D5F File Offset: 0x00137F5F
			public override void Insert(int index, object obj)
			{
				this._list.Insert(index, obj);
				this._version++;
			}

			// Token: 0x06005D51 RID: 23889 RVA: 0x00139D7C File Offset: 0x00137F7C
			public override void InsertRange(int index, ICollection c)
			{
				if (c == null)
				{
					throw new ArgumentNullException("c", "Collection cannot be null.");
				}
				if (index < 0 || index > this.Count)
				{
					throw new ArgumentOutOfRangeException("index", "Index was out of range. Must be non-negative and less than the size of the collection.");
				}
				if (c.Count > 0)
				{
					ArrayList arrayList = this._list as ArrayList;
					if (arrayList != null)
					{
						arrayList.InsertRange(index, c);
					}
					else
					{
						foreach (object value in c)
						{
							this._list.Insert(index++, value);
						}
					}
					this._version++;
				}
			}

			// Token: 0x06005D52 RID: 23890 RVA: 0x00139E11 File Offset: 0x00138011
			public override int LastIndexOf(object value)
			{
				return this.LastIndexOf(value, this._list.Count - 1, this._list.Count);
			}

			// Token: 0x06005D53 RID: 23891 RVA: 0x00139E32 File Offset: 0x00138032
			public override int LastIndexOf(object value, int startIndex)
			{
				return this.LastIndexOf(value, startIndex, startIndex + 1);
			}

			// Token: 0x06005D54 RID: 23892 RVA: 0x00139E40 File Offset: 0x00138040
			public override int LastIndexOf(object value, int startIndex, int count)
			{
				if (this._list.Count == 0)
				{
					return -1;
				}
				if (startIndex < 0 || startIndex >= this._list.Count)
				{
					throw new ArgumentOutOfRangeException("startIndex", "Index was out of range. Must be non-negative and less than the size of the collection.");
				}
				if (count < 0 || count > startIndex + 1)
				{
					throw new ArgumentOutOfRangeException("count", "Count must be positive and count must refer to a location within the string/array/collection.");
				}
				int num = startIndex - count + 1;
				if (value == null)
				{
					for (int i = startIndex; i >= num; i--)
					{
						if (this._list[i] == null)
						{
							return i;
						}
					}
					return -1;
				}
				for (int j = startIndex; j >= num; j--)
				{
					if (this._list[j] != null && this._list[j].Equals(value))
					{
						return j;
					}
				}
				return -1;
			}

			// Token: 0x06005D55 RID: 23893 RVA: 0x00139EF0 File Offset: 0x001380F0
			public override void Remove(object value)
			{
				int num = this.IndexOf(value);
				if (num >= 0)
				{
					this.RemoveAt(num);
				}
			}

			// Token: 0x06005D56 RID: 23894 RVA: 0x00139F10 File Offset: 0x00138110
			public override void RemoveAt(int index)
			{
				this._list.RemoveAt(index);
				this._version++;
			}

			// Token: 0x06005D57 RID: 23895 RVA: 0x00139F2C File Offset: 0x0013812C
			public override void RemoveRange(int index, int count)
			{
				if (index < 0 || count < 0)
				{
					throw new ArgumentOutOfRangeException((index < 0) ? "index" : "count", "Non-negative number required.");
				}
				if (this._list.Count - index < count)
				{
					throw new ArgumentException("Offset and length were out of bounds for the array or count is greater than the number of elements from index to the end of the source collection.");
				}
				if (count > 0)
				{
					this._version++;
				}
				while (count > 0)
				{
					this._list.RemoveAt(index);
					count--;
				}
			}

			// Token: 0x06005D58 RID: 23896 RVA: 0x00139FA0 File Offset: 0x001381A0
			public override void Reverse(int index, int count)
			{
				if (index < 0 || count < 0)
				{
					throw new ArgumentOutOfRangeException((index < 0) ? "index" : "count", "Non-negative number required.");
				}
				if (this._list.Count - index < count)
				{
					throw new ArgumentException("Offset and length were out of bounds for the array or count is greater than the number of elements from index to the end of the source collection.");
				}
				int i = index;
				int num = index + count - 1;
				while (i < num)
				{
					object value = this._list[i];
					this._list[i++] = this._list[num];
					this._list[num--] = value;
				}
				this._version++;
			}

			// Token: 0x06005D59 RID: 23897 RVA: 0x0013A044 File Offset: 0x00138244
			public override void SetRange(int index, ICollection c)
			{
				if (c == null)
				{
					throw new ArgumentNullException("c", "Collection cannot be null.");
				}
				if (index < 0 || index > this._list.Count - c.Count)
				{
					throw new ArgumentOutOfRangeException("index", "Index was out of range. Must be non-negative and less than the size of the collection.");
				}
				if (c.Count > 0)
				{
					foreach (object value in c)
					{
						this._list[index++] = value;
					}
					this._version++;
				}
			}

			// Token: 0x06005D5A RID: 23898 RVA: 0x0013A0CC File Offset: 0x001382CC
			public override ArrayList GetRange(int index, int count)
			{
				if (index < 0 || count < 0)
				{
					throw new ArgumentOutOfRangeException((index < 0) ? "index" : "count", "Non-negative number required.");
				}
				if (this._list.Count - index < count)
				{
					throw new ArgumentException("Offset and length were out of bounds for the array or count is greater than the number of elements from index to the end of the source collection.");
				}
				return new ArrayList.Range(this, index, count);
			}

			// Token: 0x06005D5B RID: 23899 RVA: 0x0013A120 File Offset: 0x00138320
			public override void Sort(int index, int count, IComparer comparer)
			{
				if (index < 0 || count < 0)
				{
					throw new ArgumentOutOfRangeException((index < 0) ? "index" : "count", "Non-negative number required.");
				}
				if (this._list.Count - index < count)
				{
					throw new ArgumentException("Offset and length were out of bounds for the array or count is greater than the number of elements from index to the end of the source collection.");
				}
				object[] array = new object[count];
				this.CopyTo(index, array, 0, count);
				Array.Sort(array, 0, count, comparer);
				for (int i = 0; i < count; i++)
				{
					this._list[i + index] = array[i];
				}
				this._version++;
			}

			// Token: 0x06005D5C RID: 23900 RVA: 0x0013A1B0 File Offset: 0x001383B0
			public override object[] ToArray()
			{
				if (this.Count == 0)
				{
					return Array.Empty<object>();
				}
				object[] array = new object[this.Count];
				this._list.CopyTo(array, 0);
				return array;
			}

			// Token: 0x06005D5D RID: 23901 RVA: 0x0013A1E8 File Offset: 0x001383E8
			public override Array ToArray(Type type)
			{
				if (type == null)
				{
					throw new ArgumentNullException("type");
				}
				Array array = Array.CreateInstance(type, this._list.Count);
				this._list.CopyTo(array, 0);
				return array;
			}

			// Token: 0x06005D5E RID: 23902 RVA: 0x00004BF9 File Offset: 0x00002DF9
			public override void TrimToSize()
			{
			}

			// Token: 0x040038D6 RID: 14550
			private IList _list;

			// Token: 0x02000A3D RID: 2621
			[Serializable]
			private sealed class IListWrapperEnumWrapper : IEnumerator, ICloneable
			{
				// Token: 0x06005D5F RID: 23903 RVA: 0x0013A22C File Offset: 0x0013842C
				internal IListWrapperEnumWrapper(ArrayList.IListWrapper listWrapper, int startIndex, int count)
				{
					this._en = listWrapper.GetEnumerator();
					this._initialStartIndex = startIndex;
					this._initialCount = count;
					while (startIndex-- > 0 && this._en.MoveNext())
					{
					}
					this._remaining = count;
					this._firstCall = true;
				}

				// Token: 0x06005D60 RID: 23904 RVA: 0x0000259F File Offset: 0x0000079F
				private IListWrapperEnumWrapper()
				{
				}

				// Token: 0x06005D61 RID: 23905 RVA: 0x0013A280 File Offset: 0x00138480
				public object Clone()
				{
					return new ArrayList.IListWrapper.IListWrapperEnumWrapper
					{
						_en = (IEnumerator)((ICloneable)this._en).Clone(),
						_initialStartIndex = this._initialStartIndex,
						_initialCount = this._initialCount,
						_remaining = this._remaining,
						_firstCall = this._firstCall
					};
				}

				// Token: 0x06005D62 RID: 23906 RVA: 0x0013A2E0 File Offset: 0x001384E0
				public bool MoveNext()
				{
					if (this._firstCall)
					{
						this._firstCall = false;
						int remaining = this._remaining;
						this._remaining = remaining - 1;
						return remaining > 0 && this._en.MoveNext();
					}
					if (this._remaining < 0)
					{
						return false;
					}
					if (this._en.MoveNext())
					{
						int remaining = this._remaining;
						this._remaining = remaining - 1;
						return remaining > 0;
					}
					return false;
				}

				// Token: 0x17001043 RID: 4163
				// (get) Token: 0x06005D63 RID: 23907 RVA: 0x0013A34C File Offset: 0x0013854C
				public object Current
				{
					get
					{
						if (this._firstCall)
						{
							throw new InvalidOperationException("Enumeration has not started. Call MoveNext.");
						}
						if (this._remaining < 0)
						{
							throw new InvalidOperationException("Enumeration already finished.");
						}
						return this._en.Current;
					}
				}

				// Token: 0x06005D64 RID: 23908 RVA: 0x0013A380 File Offset: 0x00138580
				public void Reset()
				{
					this._en.Reset();
					int initialStartIndex = this._initialStartIndex;
					while (initialStartIndex-- > 0 && this._en.MoveNext())
					{
					}
					this._remaining = this._initialCount;
					this._firstCall = true;
				}

				// Token: 0x040038D7 RID: 14551
				private IEnumerator _en;

				// Token: 0x040038D8 RID: 14552
				private int _remaining;

				// Token: 0x040038D9 RID: 14553
				private int _initialStartIndex;

				// Token: 0x040038DA RID: 14554
				private int _initialCount;

				// Token: 0x040038DB RID: 14555
				private bool _firstCall;
			}
		}

		// Token: 0x02000A3E RID: 2622
		[Serializable]
		private class SyncArrayList : ArrayList
		{
			// Token: 0x06005D65 RID: 23909 RVA: 0x0013A3C7 File Offset: 0x001385C7
			internal SyncArrayList(ArrayList list) : base(false)
			{
				this._list = list;
				this._root = list.SyncRoot;
			}

			// Token: 0x17001044 RID: 4164
			// (get) Token: 0x06005D66 RID: 23910 RVA: 0x0013A3E4 File Offset: 0x001385E4
			// (set) Token: 0x06005D67 RID: 23911 RVA: 0x0013A42C File Offset: 0x0013862C
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
				set
				{
					object root = this._root;
					lock (root)
					{
						this._list.Capacity = value;
					}
				}
			}

			// Token: 0x17001045 RID: 4165
			// (get) Token: 0x06005D68 RID: 23912 RVA: 0x0013A474 File Offset: 0x00138674
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

			// Token: 0x17001046 RID: 4166
			// (get) Token: 0x06005D69 RID: 23913 RVA: 0x0013A4BC File Offset: 0x001386BC
			public override bool IsReadOnly
			{
				get
				{
					return this._list.IsReadOnly;
				}
			}

			// Token: 0x17001047 RID: 4167
			// (get) Token: 0x06005D6A RID: 23914 RVA: 0x0013A4C9 File Offset: 0x001386C9
			public override bool IsFixedSize
			{
				get
				{
					return this._list.IsFixedSize;
				}
			}

			// Token: 0x17001048 RID: 4168
			// (get) Token: 0x06005D6B RID: 23915 RVA: 0x000040F7 File Offset: 0x000022F7
			public override bool IsSynchronized
			{
				get
				{
					return true;
				}
			}

			// Token: 0x17001049 RID: 4169
			public override object this[int index]
			{
				get
				{
					object root = this._root;
					object result;
					lock (root)
					{
						result = this._list[index];
					}
					return result;
				}
				set
				{
					object root = this._root;
					lock (root)
					{
						this._list[index] = value;
					}
				}
			}

			// Token: 0x1700104A RID: 4170
			// (get) Token: 0x06005D6E RID: 23918 RVA: 0x0013A568 File Offset: 0x00138768
			public override object SyncRoot
			{
				get
				{
					return this._root;
				}
			}

			// Token: 0x06005D6F RID: 23919 RVA: 0x0013A570 File Offset: 0x00138770
			public override int Add(object value)
			{
				object root = this._root;
				int result;
				lock (root)
				{
					result = this._list.Add(value);
				}
				return result;
			}

			// Token: 0x06005D70 RID: 23920 RVA: 0x0013A5B8 File Offset: 0x001387B8
			public override void AddRange(ICollection c)
			{
				object root = this._root;
				lock (root)
				{
					this._list.AddRange(c);
				}
			}

			// Token: 0x06005D71 RID: 23921 RVA: 0x0013A600 File Offset: 0x00138800
			public override int BinarySearch(object value)
			{
				object root = this._root;
				int result;
				lock (root)
				{
					result = this._list.BinarySearch(value);
				}
				return result;
			}

			// Token: 0x06005D72 RID: 23922 RVA: 0x0013A648 File Offset: 0x00138848
			public override int BinarySearch(object value, IComparer comparer)
			{
				object root = this._root;
				int result;
				lock (root)
				{
					result = this._list.BinarySearch(value, comparer);
				}
				return result;
			}

			// Token: 0x06005D73 RID: 23923 RVA: 0x0013A694 File Offset: 0x00138894
			public override int BinarySearch(int index, int count, object value, IComparer comparer)
			{
				object root = this._root;
				int result;
				lock (root)
				{
					result = this._list.BinarySearch(index, count, value, comparer);
				}
				return result;
			}

			// Token: 0x06005D74 RID: 23924 RVA: 0x0013A6E0 File Offset: 0x001388E0
			public override void Clear()
			{
				object root = this._root;
				lock (root)
				{
					this._list.Clear();
				}
			}

			// Token: 0x06005D75 RID: 23925 RVA: 0x0013A728 File Offset: 0x00138928
			public override object Clone()
			{
				object root = this._root;
				object result;
				lock (root)
				{
					result = new ArrayList.SyncArrayList((ArrayList)this._list.Clone());
				}
				return result;
			}

			// Token: 0x06005D76 RID: 23926 RVA: 0x0013A77C File Offset: 0x0013897C
			public override bool Contains(object item)
			{
				object root = this._root;
				bool result;
				lock (root)
				{
					result = this._list.Contains(item);
				}
				return result;
			}

			// Token: 0x06005D77 RID: 23927 RVA: 0x0013A7C4 File Offset: 0x001389C4
			public override void CopyTo(Array array)
			{
				object root = this._root;
				lock (root)
				{
					this._list.CopyTo(array);
				}
			}

			// Token: 0x06005D78 RID: 23928 RVA: 0x0013A80C File Offset: 0x00138A0C
			public override void CopyTo(Array array, int index)
			{
				object root = this._root;
				lock (root)
				{
					this._list.CopyTo(array, index);
				}
			}

			// Token: 0x06005D79 RID: 23929 RVA: 0x0013A854 File Offset: 0x00138A54
			public override void CopyTo(int index, Array array, int arrayIndex, int count)
			{
				object root = this._root;
				lock (root)
				{
					this._list.CopyTo(index, array, arrayIndex, count);
				}
			}

			// Token: 0x06005D7A RID: 23930 RVA: 0x0013A8A0 File Offset: 0x00138AA0
			public override IEnumerator GetEnumerator()
			{
				object root = this._root;
				IEnumerator enumerator;
				lock (root)
				{
					enumerator = this._list.GetEnumerator();
				}
				return enumerator;
			}

			// Token: 0x06005D7B RID: 23931 RVA: 0x0013A8E8 File Offset: 0x00138AE8
			public override IEnumerator GetEnumerator(int index, int count)
			{
				object root = this._root;
				IEnumerator enumerator;
				lock (root)
				{
					enumerator = this._list.GetEnumerator(index, count);
				}
				return enumerator;
			}

			// Token: 0x06005D7C RID: 23932 RVA: 0x0013A934 File Offset: 0x00138B34
			public override int IndexOf(object value)
			{
				object root = this._root;
				int result;
				lock (root)
				{
					result = this._list.IndexOf(value);
				}
				return result;
			}

			// Token: 0x06005D7D RID: 23933 RVA: 0x0013A97C File Offset: 0x00138B7C
			public override int IndexOf(object value, int startIndex)
			{
				object root = this._root;
				int result;
				lock (root)
				{
					result = this._list.IndexOf(value, startIndex);
				}
				return result;
			}

			// Token: 0x06005D7E RID: 23934 RVA: 0x0013A9C8 File Offset: 0x00138BC8
			public override int IndexOf(object value, int startIndex, int count)
			{
				object root = this._root;
				int result;
				lock (root)
				{
					result = this._list.IndexOf(value, startIndex, count);
				}
				return result;
			}

			// Token: 0x06005D7F RID: 23935 RVA: 0x0013AA14 File Offset: 0x00138C14
			public override void Insert(int index, object value)
			{
				object root = this._root;
				lock (root)
				{
					this._list.Insert(index, value);
				}
			}

			// Token: 0x06005D80 RID: 23936 RVA: 0x0013AA5C File Offset: 0x00138C5C
			public override void InsertRange(int index, ICollection c)
			{
				object root = this._root;
				lock (root)
				{
					this._list.InsertRange(index, c);
				}
			}

			// Token: 0x06005D81 RID: 23937 RVA: 0x0013AAA4 File Offset: 0x00138CA4
			public override int LastIndexOf(object value)
			{
				object root = this._root;
				int result;
				lock (root)
				{
					result = this._list.LastIndexOf(value);
				}
				return result;
			}

			// Token: 0x06005D82 RID: 23938 RVA: 0x0013AAEC File Offset: 0x00138CEC
			public override int LastIndexOf(object value, int startIndex)
			{
				object root = this._root;
				int result;
				lock (root)
				{
					result = this._list.LastIndexOf(value, startIndex);
				}
				return result;
			}

			// Token: 0x06005D83 RID: 23939 RVA: 0x0013AB38 File Offset: 0x00138D38
			public override int LastIndexOf(object value, int startIndex, int count)
			{
				object root = this._root;
				int result;
				lock (root)
				{
					result = this._list.LastIndexOf(value, startIndex, count);
				}
				return result;
			}

			// Token: 0x06005D84 RID: 23940 RVA: 0x0013AB84 File Offset: 0x00138D84
			public override void Remove(object value)
			{
				object root = this._root;
				lock (root)
				{
					this._list.Remove(value);
				}
			}

			// Token: 0x06005D85 RID: 23941 RVA: 0x0013ABCC File Offset: 0x00138DCC
			public override void RemoveAt(int index)
			{
				object root = this._root;
				lock (root)
				{
					this._list.RemoveAt(index);
				}
			}

			// Token: 0x06005D86 RID: 23942 RVA: 0x0013AC14 File Offset: 0x00138E14
			public override void RemoveRange(int index, int count)
			{
				object root = this._root;
				lock (root)
				{
					this._list.RemoveRange(index, count);
				}
			}

			// Token: 0x06005D87 RID: 23943 RVA: 0x0013AC5C File Offset: 0x00138E5C
			public override void Reverse(int index, int count)
			{
				object root = this._root;
				lock (root)
				{
					this._list.Reverse(index, count);
				}
			}

			// Token: 0x06005D88 RID: 23944 RVA: 0x0013ACA4 File Offset: 0x00138EA4
			public override void SetRange(int index, ICollection c)
			{
				object root = this._root;
				lock (root)
				{
					this._list.SetRange(index, c);
				}
			}

			// Token: 0x06005D89 RID: 23945 RVA: 0x0013ACEC File Offset: 0x00138EEC
			public override ArrayList GetRange(int index, int count)
			{
				object root = this._root;
				ArrayList range;
				lock (root)
				{
					range = this._list.GetRange(index, count);
				}
				return range;
			}

			// Token: 0x06005D8A RID: 23946 RVA: 0x0013AD38 File Offset: 0x00138F38
			public override void Sort()
			{
				object root = this._root;
				lock (root)
				{
					this._list.Sort();
				}
			}

			// Token: 0x06005D8B RID: 23947 RVA: 0x0013AD80 File Offset: 0x00138F80
			public override void Sort(IComparer comparer)
			{
				object root = this._root;
				lock (root)
				{
					this._list.Sort(comparer);
				}
			}

			// Token: 0x06005D8C RID: 23948 RVA: 0x0013ADC8 File Offset: 0x00138FC8
			public override void Sort(int index, int count, IComparer comparer)
			{
				object root = this._root;
				lock (root)
				{
					this._list.Sort(index, count, comparer);
				}
			}

			// Token: 0x06005D8D RID: 23949 RVA: 0x0013AE10 File Offset: 0x00139010
			public override object[] ToArray()
			{
				object root = this._root;
				object[] result;
				lock (root)
				{
					result = this._list.ToArray();
				}
				return result;
			}

			// Token: 0x06005D8E RID: 23950 RVA: 0x0013AE58 File Offset: 0x00139058
			public override Array ToArray(Type type)
			{
				object root = this._root;
				Array result;
				lock (root)
				{
					result = this._list.ToArray(type);
				}
				return result;
			}

			// Token: 0x06005D8F RID: 23951 RVA: 0x0013AEA0 File Offset: 0x001390A0
			public override void TrimToSize()
			{
				object root = this._root;
				lock (root)
				{
					this._list.TrimToSize();
				}
			}

			// Token: 0x040038DC RID: 14556
			private ArrayList _list;

			// Token: 0x040038DD RID: 14557
			private object _root;
		}

		// Token: 0x02000A3F RID: 2623
		[Serializable]
		private class SyncIList : IList, ICollection, IEnumerable
		{
			// Token: 0x06005D90 RID: 23952 RVA: 0x0013AEE8 File Offset: 0x001390E8
			internal SyncIList(IList list)
			{
				this._list = list;
				this._root = list.SyncRoot;
			}

			// Token: 0x1700104B RID: 4171
			// (get) Token: 0x06005D91 RID: 23953 RVA: 0x0013AF04 File Offset: 0x00139104
			public virtual int Count
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

			// Token: 0x1700104C RID: 4172
			// (get) Token: 0x06005D92 RID: 23954 RVA: 0x0013AF4C File Offset: 0x0013914C
			public virtual bool IsReadOnly
			{
				get
				{
					return this._list.IsReadOnly;
				}
			}

			// Token: 0x1700104D RID: 4173
			// (get) Token: 0x06005D93 RID: 23955 RVA: 0x0013AF59 File Offset: 0x00139159
			public virtual bool IsFixedSize
			{
				get
				{
					return this._list.IsFixedSize;
				}
			}

			// Token: 0x1700104E RID: 4174
			// (get) Token: 0x06005D94 RID: 23956 RVA: 0x000040F7 File Offset: 0x000022F7
			public virtual bool IsSynchronized
			{
				get
				{
					return true;
				}
			}

			// Token: 0x1700104F RID: 4175
			public virtual object this[int index]
			{
				get
				{
					object root = this._root;
					object result;
					lock (root)
					{
						result = this._list[index];
					}
					return result;
				}
				set
				{
					object root = this._root;
					lock (root)
					{
						this._list[index] = value;
					}
				}
			}

			// Token: 0x17001050 RID: 4176
			// (get) Token: 0x06005D97 RID: 23959 RVA: 0x0013AFF8 File Offset: 0x001391F8
			public virtual object SyncRoot
			{
				get
				{
					return this._root;
				}
			}

			// Token: 0x06005D98 RID: 23960 RVA: 0x0013B000 File Offset: 0x00139200
			public virtual int Add(object value)
			{
				object root = this._root;
				int result;
				lock (root)
				{
					result = this._list.Add(value);
				}
				return result;
			}

			// Token: 0x06005D99 RID: 23961 RVA: 0x0013B048 File Offset: 0x00139248
			public virtual void Clear()
			{
				object root = this._root;
				lock (root)
				{
					this._list.Clear();
				}
			}

			// Token: 0x06005D9A RID: 23962 RVA: 0x0013B090 File Offset: 0x00139290
			public virtual bool Contains(object item)
			{
				object root = this._root;
				bool result;
				lock (root)
				{
					result = this._list.Contains(item);
				}
				return result;
			}

			// Token: 0x06005D9B RID: 23963 RVA: 0x0013B0D8 File Offset: 0x001392D8
			public virtual void CopyTo(Array array, int index)
			{
				object root = this._root;
				lock (root)
				{
					this._list.CopyTo(array, index);
				}
			}

			// Token: 0x06005D9C RID: 23964 RVA: 0x0013B120 File Offset: 0x00139320
			public virtual IEnumerator GetEnumerator()
			{
				object root = this._root;
				IEnumerator enumerator;
				lock (root)
				{
					enumerator = this._list.GetEnumerator();
				}
				return enumerator;
			}

			// Token: 0x06005D9D RID: 23965 RVA: 0x0013B168 File Offset: 0x00139368
			public virtual int IndexOf(object value)
			{
				object root = this._root;
				int result;
				lock (root)
				{
					result = this._list.IndexOf(value);
				}
				return result;
			}

			// Token: 0x06005D9E RID: 23966 RVA: 0x0013B1B0 File Offset: 0x001393B0
			public virtual void Insert(int index, object value)
			{
				object root = this._root;
				lock (root)
				{
					this._list.Insert(index, value);
				}
			}

			// Token: 0x06005D9F RID: 23967 RVA: 0x0013B1F8 File Offset: 0x001393F8
			public virtual void Remove(object value)
			{
				object root = this._root;
				lock (root)
				{
					this._list.Remove(value);
				}
			}

			// Token: 0x06005DA0 RID: 23968 RVA: 0x0013B240 File Offset: 0x00139440
			public virtual void RemoveAt(int index)
			{
				object root = this._root;
				lock (root)
				{
					this._list.RemoveAt(index);
				}
			}

			// Token: 0x040038DE RID: 14558
			private IList _list;

			// Token: 0x040038DF RID: 14559
			private object _root;
		}

		// Token: 0x02000A40 RID: 2624
		[Serializable]
		private class FixedSizeList : IList, ICollection, IEnumerable
		{
			// Token: 0x06005DA1 RID: 23969 RVA: 0x0013B288 File Offset: 0x00139488
			internal FixedSizeList(IList l)
			{
				this._list = l;
			}

			// Token: 0x17001051 RID: 4177
			// (get) Token: 0x06005DA2 RID: 23970 RVA: 0x0013B297 File Offset: 0x00139497
			public virtual int Count
			{
				get
				{
					return this._list.Count;
				}
			}

			// Token: 0x17001052 RID: 4178
			// (get) Token: 0x06005DA3 RID: 23971 RVA: 0x0013B2A4 File Offset: 0x001394A4
			public virtual bool IsReadOnly
			{
				get
				{
					return this._list.IsReadOnly;
				}
			}

			// Token: 0x17001053 RID: 4179
			// (get) Token: 0x06005DA4 RID: 23972 RVA: 0x000040F7 File Offset: 0x000022F7
			public virtual bool IsFixedSize
			{
				get
				{
					return true;
				}
			}

			// Token: 0x17001054 RID: 4180
			// (get) Token: 0x06005DA5 RID: 23973 RVA: 0x0013B2B1 File Offset: 0x001394B1
			public virtual bool IsSynchronized
			{
				get
				{
					return this._list.IsSynchronized;
				}
			}

			// Token: 0x17001055 RID: 4181
			public virtual object this[int index]
			{
				get
				{
					return this._list[index];
				}
				set
				{
					this._list[index] = value;
				}
			}

			// Token: 0x17001056 RID: 4182
			// (get) Token: 0x06005DA8 RID: 23976 RVA: 0x0013B2DB File Offset: 0x001394DB
			public virtual object SyncRoot
			{
				get
				{
					return this._list.SyncRoot;
				}
			}

			// Token: 0x06005DA9 RID: 23977 RVA: 0x0004ED88 File Offset: 0x0004CF88
			public virtual int Add(object obj)
			{
				throw new NotSupportedException("Collection was of a fixed size.");
			}

			// Token: 0x06005DAA RID: 23978 RVA: 0x0004ED88 File Offset: 0x0004CF88
			public virtual void Clear()
			{
				throw new NotSupportedException("Collection was of a fixed size.");
			}

			// Token: 0x06005DAB RID: 23979 RVA: 0x0013B2E8 File Offset: 0x001394E8
			public virtual bool Contains(object obj)
			{
				return this._list.Contains(obj);
			}

			// Token: 0x06005DAC RID: 23980 RVA: 0x0013B2F6 File Offset: 0x001394F6
			public virtual void CopyTo(Array array, int index)
			{
				this._list.CopyTo(array, index);
			}

			// Token: 0x06005DAD RID: 23981 RVA: 0x0013B305 File Offset: 0x00139505
			public virtual IEnumerator GetEnumerator()
			{
				return this._list.GetEnumerator();
			}

			// Token: 0x06005DAE RID: 23982 RVA: 0x0013B312 File Offset: 0x00139512
			public virtual int IndexOf(object value)
			{
				return this._list.IndexOf(value);
			}

			// Token: 0x06005DAF RID: 23983 RVA: 0x0004ED88 File Offset: 0x0004CF88
			public virtual void Insert(int index, object obj)
			{
				throw new NotSupportedException("Collection was of a fixed size.");
			}

			// Token: 0x06005DB0 RID: 23984 RVA: 0x0004ED88 File Offset: 0x0004CF88
			public virtual void Remove(object value)
			{
				throw new NotSupportedException("Collection was of a fixed size.");
			}

			// Token: 0x06005DB1 RID: 23985 RVA: 0x0004ED88 File Offset: 0x0004CF88
			public virtual void RemoveAt(int index)
			{
				throw new NotSupportedException("Collection was of a fixed size.");
			}

			// Token: 0x040038E0 RID: 14560
			private IList _list;
		}

		// Token: 0x02000A41 RID: 2625
		[Serializable]
		private class FixedSizeArrayList : ArrayList
		{
			// Token: 0x06005DB2 RID: 23986 RVA: 0x0013B320 File Offset: 0x00139520
			internal FixedSizeArrayList(ArrayList l)
			{
				this._list = l;
				this._version = this._list._version;
			}

			// Token: 0x17001057 RID: 4183
			// (get) Token: 0x06005DB3 RID: 23987 RVA: 0x0013B340 File Offset: 0x00139540
			public override int Count
			{
				get
				{
					return this._list.Count;
				}
			}

			// Token: 0x17001058 RID: 4184
			// (get) Token: 0x06005DB4 RID: 23988 RVA: 0x0013B34D File Offset: 0x0013954D
			public override bool IsReadOnly
			{
				get
				{
					return this._list.IsReadOnly;
				}
			}

			// Token: 0x17001059 RID: 4185
			// (get) Token: 0x06005DB5 RID: 23989 RVA: 0x000040F7 File Offset: 0x000022F7
			public override bool IsFixedSize
			{
				get
				{
					return true;
				}
			}

			// Token: 0x1700105A RID: 4186
			// (get) Token: 0x06005DB6 RID: 23990 RVA: 0x0013B35A File Offset: 0x0013955A
			public override bool IsSynchronized
			{
				get
				{
					return this._list.IsSynchronized;
				}
			}

			// Token: 0x1700105B RID: 4187
			public override object this[int index]
			{
				get
				{
					return this._list[index];
				}
				set
				{
					this._list[index] = value;
					this._version = this._list._version;
				}
			}

			// Token: 0x1700105C RID: 4188
			// (get) Token: 0x06005DB9 RID: 23993 RVA: 0x0013B395 File Offset: 0x00139595
			public override object SyncRoot
			{
				get
				{
					return this._list.SyncRoot;
				}
			}

			// Token: 0x06005DBA RID: 23994 RVA: 0x0004ED88 File Offset: 0x0004CF88
			public override int Add(object obj)
			{
				throw new NotSupportedException("Collection was of a fixed size.");
			}

			// Token: 0x06005DBB RID: 23995 RVA: 0x0004ED88 File Offset: 0x0004CF88
			public override void AddRange(ICollection c)
			{
				throw new NotSupportedException("Collection was of a fixed size.");
			}

			// Token: 0x06005DBC RID: 23996 RVA: 0x0013B3A2 File Offset: 0x001395A2
			public override int BinarySearch(int index, int count, object value, IComparer comparer)
			{
				return this._list.BinarySearch(index, count, value, comparer);
			}

			// Token: 0x1700105D RID: 4189
			// (get) Token: 0x06005DBD RID: 23997 RVA: 0x0013B3B4 File Offset: 0x001395B4
			// (set) Token: 0x06005DBE RID: 23998 RVA: 0x0004ED88 File Offset: 0x0004CF88
			public override int Capacity
			{
				get
				{
					return this._list.Capacity;
				}
				set
				{
					throw new NotSupportedException("Collection was of a fixed size.");
				}
			}

			// Token: 0x06005DBF RID: 23999 RVA: 0x0004ED88 File Offset: 0x0004CF88
			public override void Clear()
			{
				throw new NotSupportedException("Collection was of a fixed size.");
			}

			// Token: 0x06005DC0 RID: 24000 RVA: 0x0013B3C1 File Offset: 0x001395C1
			public override object Clone()
			{
				return new ArrayList.FixedSizeArrayList(this._list)
				{
					_list = (ArrayList)this._list.Clone()
				};
			}

			// Token: 0x06005DC1 RID: 24001 RVA: 0x0013B3E4 File Offset: 0x001395E4
			public override bool Contains(object obj)
			{
				return this._list.Contains(obj);
			}

			// Token: 0x06005DC2 RID: 24002 RVA: 0x0013B3F2 File Offset: 0x001395F2
			public override void CopyTo(Array array, int index)
			{
				this._list.CopyTo(array, index);
			}

			// Token: 0x06005DC3 RID: 24003 RVA: 0x0013B401 File Offset: 0x00139601
			public override void CopyTo(int index, Array array, int arrayIndex, int count)
			{
				this._list.CopyTo(index, array, arrayIndex, count);
			}

			// Token: 0x06005DC4 RID: 24004 RVA: 0x0013B413 File Offset: 0x00139613
			public override IEnumerator GetEnumerator()
			{
				return this._list.GetEnumerator();
			}

			// Token: 0x06005DC5 RID: 24005 RVA: 0x0013B420 File Offset: 0x00139620
			public override IEnumerator GetEnumerator(int index, int count)
			{
				return this._list.GetEnumerator(index, count);
			}

			// Token: 0x06005DC6 RID: 24006 RVA: 0x0013B42F File Offset: 0x0013962F
			public override int IndexOf(object value)
			{
				return this._list.IndexOf(value);
			}

			// Token: 0x06005DC7 RID: 24007 RVA: 0x0013B43D File Offset: 0x0013963D
			public override int IndexOf(object value, int startIndex)
			{
				return this._list.IndexOf(value, startIndex);
			}

			// Token: 0x06005DC8 RID: 24008 RVA: 0x0013B44C File Offset: 0x0013964C
			public override int IndexOf(object value, int startIndex, int count)
			{
				return this._list.IndexOf(value, startIndex, count);
			}

			// Token: 0x06005DC9 RID: 24009 RVA: 0x0004ED88 File Offset: 0x0004CF88
			public override void Insert(int index, object obj)
			{
				throw new NotSupportedException("Collection was of a fixed size.");
			}

			// Token: 0x06005DCA RID: 24010 RVA: 0x0004ED88 File Offset: 0x0004CF88
			public override void InsertRange(int index, ICollection c)
			{
				throw new NotSupportedException("Collection was of a fixed size.");
			}

			// Token: 0x06005DCB RID: 24011 RVA: 0x0013B45C File Offset: 0x0013965C
			public override int LastIndexOf(object value)
			{
				return this._list.LastIndexOf(value);
			}

			// Token: 0x06005DCC RID: 24012 RVA: 0x0013B46A File Offset: 0x0013966A
			public override int LastIndexOf(object value, int startIndex)
			{
				return this._list.LastIndexOf(value, startIndex);
			}

			// Token: 0x06005DCD RID: 24013 RVA: 0x0013B479 File Offset: 0x00139679
			public override int LastIndexOf(object value, int startIndex, int count)
			{
				return this._list.LastIndexOf(value, startIndex, count);
			}

			// Token: 0x06005DCE RID: 24014 RVA: 0x0004ED88 File Offset: 0x0004CF88
			public override void Remove(object value)
			{
				throw new NotSupportedException("Collection was of a fixed size.");
			}

			// Token: 0x06005DCF RID: 24015 RVA: 0x0004ED88 File Offset: 0x0004CF88
			public override void RemoveAt(int index)
			{
				throw new NotSupportedException("Collection was of a fixed size.");
			}

			// Token: 0x06005DD0 RID: 24016 RVA: 0x0004ED88 File Offset: 0x0004CF88
			public override void RemoveRange(int index, int count)
			{
				throw new NotSupportedException("Collection was of a fixed size.");
			}

			// Token: 0x06005DD1 RID: 24017 RVA: 0x0013B489 File Offset: 0x00139689
			public override void SetRange(int index, ICollection c)
			{
				this._list.SetRange(index, c);
				this._version = this._list._version;
			}

			// Token: 0x06005DD2 RID: 24018 RVA: 0x0013B4AC File Offset: 0x001396AC
			public override ArrayList GetRange(int index, int count)
			{
				if (index < 0 || count < 0)
				{
					throw new ArgumentOutOfRangeException((index < 0) ? "index" : "count", "Non-negative number required.");
				}
				if (this.Count - index < count)
				{
					throw new ArgumentException("Offset and length were out of bounds for the array or count is greater than the number of elements from index to the end of the source collection.");
				}
				return new ArrayList.Range(this, index, count);
			}

			// Token: 0x06005DD3 RID: 24019 RVA: 0x0013B4FA File Offset: 0x001396FA
			public override void Reverse(int index, int count)
			{
				this._list.Reverse(index, count);
				this._version = this._list._version;
			}

			// Token: 0x06005DD4 RID: 24020 RVA: 0x0013B51A File Offset: 0x0013971A
			public override void Sort(int index, int count, IComparer comparer)
			{
				this._list.Sort(index, count, comparer);
				this._version = this._list._version;
			}

			// Token: 0x06005DD5 RID: 24021 RVA: 0x0013B53B File Offset: 0x0013973B
			public override object[] ToArray()
			{
				return this._list.ToArray();
			}

			// Token: 0x06005DD6 RID: 24022 RVA: 0x0013B548 File Offset: 0x00139748
			public override Array ToArray(Type type)
			{
				return this._list.ToArray(type);
			}

			// Token: 0x06005DD7 RID: 24023 RVA: 0x0004ED88 File Offset: 0x0004CF88
			public override void TrimToSize()
			{
				throw new NotSupportedException("Collection was of a fixed size.");
			}

			// Token: 0x040038E1 RID: 14561
			private ArrayList _list;
		}

		// Token: 0x02000A42 RID: 2626
		[Serializable]
		private class ReadOnlyList : IList, ICollection, IEnumerable
		{
			// Token: 0x06005DD8 RID: 24024 RVA: 0x0013B556 File Offset: 0x00139756
			internal ReadOnlyList(IList l)
			{
				this._list = l;
			}

			// Token: 0x1700105E RID: 4190
			// (get) Token: 0x06005DD9 RID: 24025 RVA: 0x0013B565 File Offset: 0x00139765
			public virtual int Count
			{
				get
				{
					return this._list.Count;
				}
			}

			// Token: 0x1700105F RID: 4191
			// (get) Token: 0x06005DDA RID: 24026 RVA: 0x000040F7 File Offset: 0x000022F7
			public virtual bool IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x17001060 RID: 4192
			// (get) Token: 0x06005DDB RID: 24027 RVA: 0x000040F7 File Offset: 0x000022F7
			public virtual bool IsFixedSize
			{
				get
				{
					return true;
				}
			}

			// Token: 0x17001061 RID: 4193
			// (get) Token: 0x06005DDC RID: 24028 RVA: 0x0013B572 File Offset: 0x00139772
			public virtual bool IsSynchronized
			{
				get
				{
					return this._list.IsSynchronized;
				}
			}

			// Token: 0x17001062 RID: 4194
			public virtual object this[int index]
			{
				get
				{
					return this._list[index];
				}
				set
				{
					throw new NotSupportedException("Collection is read-only.");
				}
			}

			// Token: 0x17001063 RID: 4195
			// (get) Token: 0x06005DDF RID: 24031 RVA: 0x0013B599 File Offset: 0x00139799
			public virtual object SyncRoot
			{
				get
				{
					return this._list.SyncRoot;
				}
			}

			// Token: 0x06005DE0 RID: 24032 RVA: 0x0013B58D File Offset: 0x0013978D
			public virtual int Add(object obj)
			{
				throw new NotSupportedException("Collection is read-only.");
			}

			// Token: 0x06005DE1 RID: 24033 RVA: 0x0013B58D File Offset: 0x0013978D
			public virtual void Clear()
			{
				throw new NotSupportedException("Collection is read-only.");
			}

			// Token: 0x06005DE2 RID: 24034 RVA: 0x0013B5A6 File Offset: 0x001397A6
			public virtual bool Contains(object obj)
			{
				return this._list.Contains(obj);
			}

			// Token: 0x06005DE3 RID: 24035 RVA: 0x0013B5B4 File Offset: 0x001397B4
			public virtual void CopyTo(Array array, int index)
			{
				this._list.CopyTo(array, index);
			}

			// Token: 0x06005DE4 RID: 24036 RVA: 0x0013B5C3 File Offset: 0x001397C3
			public virtual IEnumerator GetEnumerator()
			{
				return this._list.GetEnumerator();
			}

			// Token: 0x06005DE5 RID: 24037 RVA: 0x0013B5D0 File Offset: 0x001397D0
			public virtual int IndexOf(object value)
			{
				return this._list.IndexOf(value);
			}

			// Token: 0x06005DE6 RID: 24038 RVA: 0x0013B58D File Offset: 0x0013978D
			public virtual void Insert(int index, object obj)
			{
				throw new NotSupportedException("Collection is read-only.");
			}

			// Token: 0x06005DE7 RID: 24039 RVA: 0x0013B58D File Offset: 0x0013978D
			public virtual void Remove(object value)
			{
				throw new NotSupportedException("Collection is read-only.");
			}

			// Token: 0x06005DE8 RID: 24040 RVA: 0x0013B58D File Offset: 0x0013978D
			public virtual void RemoveAt(int index)
			{
				throw new NotSupportedException("Collection is read-only.");
			}

			// Token: 0x040038E2 RID: 14562
			private IList _list;
		}

		// Token: 0x02000A43 RID: 2627
		[Serializable]
		private class ReadOnlyArrayList : ArrayList
		{
			// Token: 0x06005DE9 RID: 24041 RVA: 0x0013B5DE File Offset: 0x001397DE
			internal ReadOnlyArrayList(ArrayList l)
			{
				this._list = l;
			}

			// Token: 0x17001064 RID: 4196
			// (get) Token: 0x06005DEA RID: 24042 RVA: 0x0013B5ED File Offset: 0x001397ED
			public override int Count
			{
				get
				{
					return this._list.Count;
				}
			}

			// Token: 0x17001065 RID: 4197
			// (get) Token: 0x06005DEB RID: 24043 RVA: 0x000040F7 File Offset: 0x000022F7
			public override bool IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x17001066 RID: 4198
			// (get) Token: 0x06005DEC RID: 24044 RVA: 0x000040F7 File Offset: 0x000022F7
			public override bool IsFixedSize
			{
				get
				{
					return true;
				}
			}

			// Token: 0x17001067 RID: 4199
			// (get) Token: 0x06005DED RID: 24045 RVA: 0x0013B5FA File Offset: 0x001397FA
			public override bool IsSynchronized
			{
				get
				{
					return this._list.IsSynchronized;
				}
			}

			// Token: 0x17001068 RID: 4200
			public override object this[int index]
			{
				get
				{
					return this._list[index];
				}
				set
				{
					throw new NotSupportedException("Collection is read-only.");
				}
			}

			// Token: 0x17001069 RID: 4201
			// (get) Token: 0x06005DF0 RID: 24048 RVA: 0x0013B615 File Offset: 0x00139815
			public override object SyncRoot
			{
				get
				{
					return this._list.SyncRoot;
				}
			}

			// Token: 0x06005DF1 RID: 24049 RVA: 0x0013B58D File Offset: 0x0013978D
			public override int Add(object obj)
			{
				throw new NotSupportedException("Collection is read-only.");
			}

			// Token: 0x06005DF2 RID: 24050 RVA: 0x0013B58D File Offset: 0x0013978D
			public override void AddRange(ICollection c)
			{
				throw new NotSupportedException("Collection is read-only.");
			}

			// Token: 0x06005DF3 RID: 24051 RVA: 0x0013B622 File Offset: 0x00139822
			public override int BinarySearch(int index, int count, object value, IComparer comparer)
			{
				return this._list.BinarySearch(index, count, value, comparer);
			}

			// Token: 0x1700106A RID: 4202
			// (get) Token: 0x06005DF4 RID: 24052 RVA: 0x0013B634 File Offset: 0x00139834
			// (set) Token: 0x06005DF5 RID: 24053 RVA: 0x0013B58D File Offset: 0x0013978D
			public override int Capacity
			{
				get
				{
					return this._list.Capacity;
				}
				set
				{
					throw new NotSupportedException("Collection is read-only.");
				}
			}

			// Token: 0x06005DF6 RID: 24054 RVA: 0x0013B58D File Offset: 0x0013978D
			public override void Clear()
			{
				throw new NotSupportedException("Collection is read-only.");
			}

			// Token: 0x06005DF7 RID: 24055 RVA: 0x0013B641 File Offset: 0x00139841
			public override object Clone()
			{
				return new ArrayList.ReadOnlyArrayList(this._list)
				{
					_list = (ArrayList)this._list.Clone()
				};
			}

			// Token: 0x06005DF8 RID: 24056 RVA: 0x0013B664 File Offset: 0x00139864
			public override bool Contains(object obj)
			{
				return this._list.Contains(obj);
			}

			// Token: 0x06005DF9 RID: 24057 RVA: 0x0013B672 File Offset: 0x00139872
			public override void CopyTo(Array array, int index)
			{
				this._list.CopyTo(array, index);
			}

			// Token: 0x06005DFA RID: 24058 RVA: 0x0013B681 File Offset: 0x00139881
			public override void CopyTo(int index, Array array, int arrayIndex, int count)
			{
				this._list.CopyTo(index, array, arrayIndex, count);
			}

			// Token: 0x06005DFB RID: 24059 RVA: 0x0013B693 File Offset: 0x00139893
			public override IEnumerator GetEnumerator()
			{
				return this._list.GetEnumerator();
			}

			// Token: 0x06005DFC RID: 24060 RVA: 0x0013B6A0 File Offset: 0x001398A0
			public override IEnumerator GetEnumerator(int index, int count)
			{
				return this._list.GetEnumerator(index, count);
			}

			// Token: 0x06005DFD RID: 24061 RVA: 0x0013B6AF File Offset: 0x001398AF
			public override int IndexOf(object value)
			{
				return this._list.IndexOf(value);
			}

			// Token: 0x06005DFE RID: 24062 RVA: 0x0013B6BD File Offset: 0x001398BD
			public override int IndexOf(object value, int startIndex)
			{
				return this._list.IndexOf(value, startIndex);
			}

			// Token: 0x06005DFF RID: 24063 RVA: 0x0013B6CC File Offset: 0x001398CC
			public override int IndexOf(object value, int startIndex, int count)
			{
				return this._list.IndexOf(value, startIndex, count);
			}

			// Token: 0x06005E00 RID: 24064 RVA: 0x0013B58D File Offset: 0x0013978D
			public override void Insert(int index, object obj)
			{
				throw new NotSupportedException("Collection is read-only.");
			}

			// Token: 0x06005E01 RID: 24065 RVA: 0x0013B58D File Offset: 0x0013978D
			public override void InsertRange(int index, ICollection c)
			{
				throw new NotSupportedException("Collection is read-only.");
			}

			// Token: 0x06005E02 RID: 24066 RVA: 0x0013B6DC File Offset: 0x001398DC
			public override int LastIndexOf(object value)
			{
				return this._list.LastIndexOf(value);
			}

			// Token: 0x06005E03 RID: 24067 RVA: 0x0013B6EA File Offset: 0x001398EA
			public override int LastIndexOf(object value, int startIndex)
			{
				return this._list.LastIndexOf(value, startIndex);
			}

			// Token: 0x06005E04 RID: 24068 RVA: 0x0013B6F9 File Offset: 0x001398F9
			public override int LastIndexOf(object value, int startIndex, int count)
			{
				return this._list.LastIndexOf(value, startIndex, count);
			}

			// Token: 0x06005E05 RID: 24069 RVA: 0x0013B58D File Offset: 0x0013978D
			public override void Remove(object value)
			{
				throw new NotSupportedException("Collection is read-only.");
			}

			// Token: 0x06005E06 RID: 24070 RVA: 0x0013B58D File Offset: 0x0013978D
			public override void RemoveAt(int index)
			{
				throw new NotSupportedException("Collection is read-only.");
			}

			// Token: 0x06005E07 RID: 24071 RVA: 0x0013B58D File Offset: 0x0013978D
			public override void RemoveRange(int index, int count)
			{
				throw new NotSupportedException("Collection is read-only.");
			}

			// Token: 0x06005E08 RID: 24072 RVA: 0x0013B58D File Offset: 0x0013978D
			public override void SetRange(int index, ICollection c)
			{
				throw new NotSupportedException("Collection is read-only.");
			}

			// Token: 0x06005E09 RID: 24073 RVA: 0x0013B70C File Offset: 0x0013990C
			public override ArrayList GetRange(int index, int count)
			{
				if (index < 0 || count < 0)
				{
					throw new ArgumentOutOfRangeException((index < 0) ? "index" : "count", "Non-negative number required.");
				}
				if (this.Count - index < count)
				{
					throw new ArgumentException("Offset and length were out of bounds for the array or count is greater than the number of elements from index to the end of the source collection.");
				}
				return new ArrayList.Range(this, index, count);
			}

			// Token: 0x06005E0A RID: 24074 RVA: 0x0013B58D File Offset: 0x0013978D
			public override void Reverse(int index, int count)
			{
				throw new NotSupportedException("Collection is read-only.");
			}

			// Token: 0x06005E0B RID: 24075 RVA: 0x0013B58D File Offset: 0x0013978D
			public override void Sort(int index, int count, IComparer comparer)
			{
				throw new NotSupportedException("Collection is read-only.");
			}

			// Token: 0x06005E0C RID: 24076 RVA: 0x0013B75A File Offset: 0x0013995A
			public override object[] ToArray()
			{
				return this._list.ToArray();
			}

			// Token: 0x06005E0D RID: 24077 RVA: 0x0013B767 File Offset: 0x00139967
			public override Array ToArray(Type type)
			{
				return this._list.ToArray(type);
			}

			// Token: 0x06005E0E RID: 24078 RVA: 0x0013B58D File Offset: 0x0013978D
			public override void TrimToSize()
			{
				throw new NotSupportedException("Collection is read-only.");
			}

			// Token: 0x040038E3 RID: 14563
			private ArrayList _list;
		}

		// Token: 0x02000A44 RID: 2628
		[Serializable]
		private sealed class ArrayListEnumerator : IEnumerator, ICloneable
		{
			// Token: 0x06005E0F RID: 24079 RVA: 0x0013B775 File Offset: 0x00139975
			internal ArrayListEnumerator(ArrayList list, int index, int count)
			{
				this._list = list;
				this._startIndex = index;
				this._index = index - 1;
				this._endIndex = this._index + count;
				this._version = list._version;
				this._currentElement = null;
			}

			// Token: 0x06005E10 RID: 24080 RVA: 0x000231CD File Offset: 0x000213CD
			public object Clone()
			{
				return base.MemberwiseClone();
			}

			// Token: 0x06005E11 RID: 24081 RVA: 0x0013B7B8 File Offset: 0x001399B8
			public bool MoveNext()
			{
				if (this._version != this._list._version)
				{
					throw new InvalidOperationException("Collection was modified; enumeration operation may not execute.");
				}
				if (this._index < this._endIndex)
				{
					ArrayList list = this._list;
					int index = this._index + 1;
					this._index = index;
					this._currentElement = list[index];
					return true;
				}
				this._index = this._endIndex + 1;
				return false;
			}

			// Token: 0x1700106B RID: 4203
			// (get) Token: 0x06005E12 RID: 24082 RVA: 0x0013B824 File Offset: 0x00139A24
			public object Current
			{
				get
				{
					if (this._index < this._startIndex)
					{
						throw new InvalidOperationException("Enumeration has not started. Call MoveNext.");
					}
					if (this._index > this._endIndex)
					{
						throw new InvalidOperationException("Enumeration already finished.");
					}
					return this._currentElement;
				}
			}

			// Token: 0x06005E13 RID: 24083 RVA: 0x0013B85E File Offset: 0x00139A5E
			public void Reset()
			{
				if (this._version != this._list._version)
				{
					throw new InvalidOperationException("Collection was modified; enumeration operation may not execute.");
				}
				this._index = this._startIndex - 1;
			}

			// Token: 0x040038E4 RID: 14564
			private ArrayList _list;

			// Token: 0x040038E5 RID: 14565
			private int _index;

			// Token: 0x040038E6 RID: 14566
			private int _endIndex;

			// Token: 0x040038E7 RID: 14567
			private int _version;

			// Token: 0x040038E8 RID: 14568
			private object _currentElement;

			// Token: 0x040038E9 RID: 14569
			private int _startIndex;
		}

		// Token: 0x02000A45 RID: 2629
		[Serializable]
		private class Range : ArrayList
		{
			// Token: 0x06005E14 RID: 24084 RVA: 0x0013B88C File Offset: 0x00139A8C
			internal Range(ArrayList list, int index, int count) : base(false)
			{
				this._baseList = list;
				this._baseIndex = index;
				this._baseSize = count;
				this._baseVersion = list._version;
				this._version = list._version;
			}

			// Token: 0x06005E15 RID: 24085 RVA: 0x0013B8C2 File Offset: 0x00139AC2
			private void InternalUpdateRange()
			{
				if (this._baseVersion != this._baseList._version)
				{
					throw new InvalidOperationException("This range in the underlying list is invalid. A possible cause is that elements were removed.");
				}
			}

			// Token: 0x06005E16 RID: 24086 RVA: 0x0013B8E2 File Offset: 0x00139AE2
			private void InternalUpdateVersion()
			{
				this._baseVersion++;
				this._version++;
			}

			// Token: 0x06005E17 RID: 24087 RVA: 0x0013B900 File Offset: 0x00139B00
			public override int Add(object value)
			{
				this.InternalUpdateRange();
				this._baseList.Insert(this._baseIndex + this._baseSize, value);
				this.InternalUpdateVersion();
				int baseSize = this._baseSize;
				this._baseSize = baseSize + 1;
				return baseSize;
			}

			// Token: 0x06005E18 RID: 24088 RVA: 0x0013B944 File Offset: 0x00139B44
			public override void AddRange(ICollection c)
			{
				if (c == null)
				{
					throw new ArgumentNullException("c");
				}
				this.InternalUpdateRange();
				int count = c.Count;
				if (count > 0)
				{
					this._baseList.InsertRange(this._baseIndex + this._baseSize, c);
					this.InternalUpdateVersion();
					this._baseSize += count;
				}
			}

			// Token: 0x06005E19 RID: 24089 RVA: 0x0013B9A0 File Offset: 0x00139BA0
			public override int BinarySearch(int index, int count, object value, IComparer comparer)
			{
				if (index < 0 || count < 0)
				{
					throw new ArgumentOutOfRangeException((index < 0) ? "index" : "count", "Non-negative number required.");
				}
				if (this._baseSize - index < count)
				{
					throw new ArgumentException("Offset and length were out of bounds for the array or count is greater than the number of elements from index to the end of the source collection.");
				}
				this.InternalUpdateRange();
				int num = this._baseList.BinarySearch(this._baseIndex + index, count, value, comparer);
				if (num >= 0)
				{
					return num - this._baseIndex;
				}
				return num + this._baseIndex;
			}

			// Token: 0x1700106C RID: 4204
			// (get) Token: 0x06005E1A RID: 24090 RVA: 0x0013BA19 File Offset: 0x00139C19
			// (set) Token: 0x06005E1B RID: 24091 RVA: 0x001399E0 File Offset: 0x00137BE0
			public override int Capacity
			{
				get
				{
					return this._baseList.Capacity;
				}
				set
				{
					if (value < this.Count)
					{
						throw new ArgumentOutOfRangeException("value", "capacity was less than the current size.");
					}
				}
			}

			// Token: 0x06005E1C RID: 24092 RVA: 0x0013BA26 File Offset: 0x00139C26
			public override void Clear()
			{
				this.InternalUpdateRange();
				if (this._baseSize != 0)
				{
					this._baseList.RemoveRange(this._baseIndex, this._baseSize);
					this.InternalUpdateVersion();
					this._baseSize = 0;
				}
			}

			// Token: 0x06005E1D RID: 24093 RVA: 0x0013BA5A File Offset: 0x00139C5A
			public override object Clone()
			{
				this.InternalUpdateRange();
				return new ArrayList.Range(this._baseList, this._baseIndex, this._baseSize)
				{
					_baseList = (ArrayList)this._baseList.Clone()
				};
			}

			// Token: 0x06005E1E RID: 24094 RVA: 0x0013BA90 File Offset: 0x00139C90
			public override bool Contains(object item)
			{
				this.InternalUpdateRange();
				if (item == null)
				{
					for (int i = 0; i < this._baseSize; i++)
					{
						if (this._baseList[this._baseIndex + i] == null)
						{
							return true;
						}
					}
					return false;
				}
				for (int j = 0; j < this._baseSize; j++)
				{
					if (this._baseList[this._baseIndex + j] != null && this._baseList[this._baseIndex + j].Equals(item))
					{
						return true;
					}
				}
				return false;
			}

			// Token: 0x06005E1F RID: 24095 RVA: 0x0013BB14 File Offset: 0x00139D14
			public override void CopyTo(Array array, int index)
			{
				if (array == null)
				{
					throw new ArgumentNullException("array");
				}
				if (array.Rank != 1)
				{
					throw new ArgumentException("Only single dimensional arrays are supported for the requested action.", "array");
				}
				if (index < 0)
				{
					throw new ArgumentOutOfRangeException("index", "Non-negative number required.");
				}
				if (array.Length - index < this._baseSize)
				{
					throw new ArgumentException("Offset and length were out of bounds for the array or count is greater than the number of elements from index to the end of the source collection.");
				}
				this.InternalUpdateRange();
				this._baseList.CopyTo(this._baseIndex, array, index, this._baseSize);
			}

			// Token: 0x06005E20 RID: 24096 RVA: 0x0013BB98 File Offset: 0x00139D98
			public override void CopyTo(int index, Array array, int arrayIndex, int count)
			{
				if (array == null)
				{
					throw new ArgumentNullException("array");
				}
				if (array.Rank != 1)
				{
					throw new ArgumentException("Only single dimensional arrays are supported for the requested action.", "array");
				}
				if (index < 0 || count < 0)
				{
					throw new ArgumentOutOfRangeException((index < 0) ? "index" : "count", "Non-negative number required.");
				}
				if (array.Length - arrayIndex < count)
				{
					throw new ArgumentException("Offset and length were out of bounds for the array or count is greater than the number of elements from index to the end of the source collection.");
				}
				if (this._baseSize - index < count)
				{
					throw new ArgumentException("Offset and length were out of bounds for the array or count is greater than the number of elements from index to the end of the source collection.");
				}
				this.InternalUpdateRange();
				this._baseList.CopyTo(this._baseIndex + index, array, arrayIndex, count);
			}

			// Token: 0x1700106D RID: 4205
			// (get) Token: 0x06005E21 RID: 24097 RVA: 0x0013BC3B File Offset: 0x00139E3B
			public override int Count
			{
				get
				{
					this.InternalUpdateRange();
					return this._baseSize;
				}
			}

			// Token: 0x1700106E RID: 4206
			// (get) Token: 0x06005E22 RID: 24098 RVA: 0x0013BC49 File Offset: 0x00139E49
			public override bool IsReadOnly
			{
				get
				{
					return this._baseList.IsReadOnly;
				}
			}

			// Token: 0x1700106F RID: 4207
			// (get) Token: 0x06005E23 RID: 24099 RVA: 0x0013BC56 File Offset: 0x00139E56
			public override bool IsFixedSize
			{
				get
				{
					return this._baseList.IsFixedSize;
				}
			}

			// Token: 0x17001070 RID: 4208
			// (get) Token: 0x06005E24 RID: 24100 RVA: 0x0013BC63 File Offset: 0x00139E63
			public override bool IsSynchronized
			{
				get
				{
					return this._baseList.IsSynchronized;
				}
			}

			// Token: 0x06005E25 RID: 24101 RVA: 0x0013BC70 File Offset: 0x00139E70
			public override IEnumerator GetEnumerator()
			{
				return this.GetEnumerator(0, this._baseSize);
			}

			// Token: 0x06005E26 RID: 24102 RVA: 0x0013BC80 File Offset: 0x00139E80
			public override IEnumerator GetEnumerator(int index, int count)
			{
				if (index < 0 || count < 0)
				{
					throw new ArgumentOutOfRangeException((index < 0) ? "index" : "count", "Non-negative number required.");
				}
				if (this._baseSize - index < count)
				{
					throw new ArgumentException("Offset and length were out of bounds for the array or count is greater than the number of elements from index to the end of the source collection.");
				}
				this.InternalUpdateRange();
				return this._baseList.GetEnumerator(this._baseIndex + index, count);
			}

			// Token: 0x06005E27 RID: 24103 RVA: 0x0013BCE0 File Offset: 0x00139EE0
			public override ArrayList GetRange(int index, int count)
			{
				if (index < 0 || count < 0)
				{
					throw new ArgumentOutOfRangeException((index < 0) ? "index" : "count", "Non-negative number required.");
				}
				if (this._baseSize - index < count)
				{
					throw new ArgumentException("Offset and length were out of bounds for the array or count is greater than the number of elements from index to the end of the source collection.");
				}
				this.InternalUpdateRange();
				return new ArrayList.Range(this, index, count);
			}

			// Token: 0x17001071 RID: 4209
			// (get) Token: 0x06005E28 RID: 24104 RVA: 0x0013BD34 File Offset: 0x00139F34
			public override object SyncRoot
			{
				get
				{
					return this._baseList.SyncRoot;
				}
			}

			// Token: 0x06005E29 RID: 24105 RVA: 0x0013BD44 File Offset: 0x00139F44
			public override int IndexOf(object value)
			{
				this.InternalUpdateRange();
				int num = this._baseList.IndexOf(value, this._baseIndex, this._baseSize);
				if (num >= 0)
				{
					return num - this._baseIndex;
				}
				return -1;
			}

			// Token: 0x06005E2A RID: 24106 RVA: 0x0013BD80 File Offset: 0x00139F80
			public override int IndexOf(object value, int startIndex)
			{
				if (startIndex < 0)
				{
					throw new ArgumentOutOfRangeException("startIndex", "Non-negative number required.");
				}
				if (startIndex > this._baseSize)
				{
					throw new ArgumentOutOfRangeException("startIndex", "Index was out of range. Must be non-negative and less than the size of the collection.");
				}
				this.InternalUpdateRange();
				int num = this._baseList.IndexOf(value, this._baseIndex + startIndex, this._baseSize - startIndex);
				if (num >= 0)
				{
					return num - this._baseIndex;
				}
				return -1;
			}

			// Token: 0x06005E2B RID: 24107 RVA: 0x0013BDEC File Offset: 0x00139FEC
			public override int IndexOf(object value, int startIndex, int count)
			{
				if (startIndex < 0 || startIndex > this._baseSize)
				{
					throw new ArgumentOutOfRangeException("startIndex", "Index was out of range. Must be non-negative and less than the size of the collection.");
				}
				if (count < 0 || startIndex > this._baseSize - count)
				{
					throw new ArgumentOutOfRangeException("count", "Count must be positive and count must refer to a location within the string/array/collection.");
				}
				this.InternalUpdateRange();
				int num = this._baseList.IndexOf(value, this._baseIndex + startIndex, count);
				if (num >= 0)
				{
					return num - this._baseIndex;
				}
				return -1;
			}

			// Token: 0x06005E2C RID: 24108 RVA: 0x0013BE60 File Offset: 0x0013A060
			public override void Insert(int index, object value)
			{
				if (index < 0 || index > this._baseSize)
				{
					throw new ArgumentOutOfRangeException("index", "Index was out of range. Must be non-negative and less than the size of the collection.");
				}
				this.InternalUpdateRange();
				this._baseList.Insert(this._baseIndex + index, value);
				this.InternalUpdateVersion();
				this._baseSize++;
			}

			// Token: 0x06005E2D RID: 24109 RVA: 0x0013BEB8 File Offset: 0x0013A0B8
			public override void InsertRange(int index, ICollection c)
			{
				if (index < 0 || index > this._baseSize)
				{
					throw new ArgumentOutOfRangeException("index", "Index was out of range. Must be non-negative and less than the size of the collection.");
				}
				if (c == null)
				{
					throw new ArgumentNullException("c");
				}
				this.InternalUpdateRange();
				int count = c.Count;
				if (count > 0)
				{
					this._baseList.InsertRange(this._baseIndex + index, c);
					this._baseSize += count;
					this.InternalUpdateVersion();
				}
			}

			// Token: 0x06005E2E RID: 24110 RVA: 0x0013BF2C File Offset: 0x0013A12C
			public override int LastIndexOf(object value)
			{
				this.InternalUpdateRange();
				int num = this._baseList.LastIndexOf(value, this._baseIndex + this._baseSize - 1, this._baseSize);
				if (num >= 0)
				{
					return num - this._baseIndex;
				}
				return -1;
			}

			// Token: 0x06005E2F RID: 24111 RVA: 0x00139E32 File Offset: 0x00138032
			public override int LastIndexOf(object value, int startIndex)
			{
				return this.LastIndexOf(value, startIndex, startIndex + 1);
			}

			// Token: 0x06005E30 RID: 24112 RVA: 0x0013BF70 File Offset: 0x0013A170
			public override int LastIndexOf(object value, int startIndex, int count)
			{
				this.InternalUpdateRange();
				if (this._baseSize == 0)
				{
					return -1;
				}
				if (startIndex >= this._baseSize)
				{
					throw new ArgumentOutOfRangeException("startIndex", "Index was out of range. Must be non-negative and less than the size of the collection.");
				}
				if (startIndex < 0)
				{
					throw new ArgumentOutOfRangeException("startIndex", "Non-negative number required.");
				}
				int num = this._baseList.LastIndexOf(value, this._baseIndex + startIndex, count);
				if (num >= 0)
				{
					return num - this._baseIndex;
				}
				return -1;
			}

			// Token: 0x06005E31 RID: 24113 RVA: 0x0013BFE0 File Offset: 0x0013A1E0
			public override void RemoveAt(int index)
			{
				if (index < 0 || index >= this._baseSize)
				{
					throw new ArgumentOutOfRangeException("index", "Index was out of range. Must be non-negative and less than the size of the collection.");
				}
				this.InternalUpdateRange();
				this._baseList.RemoveAt(this._baseIndex + index);
				this.InternalUpdateVersion();
				this._baseSize--;
			}

			// Token: 0x06005E32 RID: 24114 RVA: 0x0013C038 File Offset: 0x0013A238
			public override void RemoveRange(int index, int count)
			{
				if (index < 0 || count < 0)
				{
					throw new ArgumentOutOfRangeException((index < 0) ? "index" : "count", "Non-negative number required.");
				}
				if (this._baseSize - index < count)
				{
					throw new ArgumentException("Offset and length were out of bounds for the array or count is greater than the number of elements from index to the end of the source collection.");
				}
				this.InternalUpdateRange();
				if (count > 0)
				{
					this._baseList.RemoveRange(this._baseIndex + index, count);
					this.InternalUpdateVersion();
					this._baseSize -= count;
				}
			}

			// Token: 0x06005E33 RID: 24115 RVA: 0x0013C0B0 File Offset: 0x0013A2B0
			public override void Reverse(int index, int count)
			{
				if (index < 0 || count < 0)
				{
					throw new ArgumentOutOfRangeException((index < 0) ? "index" : "count", "Non-negative number required.");
				}
				if (this._baseSize - index < count)
				{
					throw new ArgumentException("Offset and length were out of bounds for the array or count is greater than the number of elements from index to the end of the source collection.");
				}
				this.InternalUpdateRange();
				this._baseList.Reverse(this._baseIndex + index, count);
				this.InternalUpdateVersion();
			}

			// Token: 0x06005E34 RID: 24116 RVA: 0x0013C118 File Offset: 0x0013A318
			public override void SetRange(int index, ICollection c)
			{
				this.InternalUpdateRange();
				if (index < 0 || index >= this._baseSize)
				{
					throw new ArgumentOutOfRangeException("index", "Index was out of range. Must be non-negative and less than the size of the collection.");
				}
				this._baseList.SetRange(this._baseIndex + index, c);
				if (c.Count > 0)
				{
					this.InternalUpdateVersion();
				}
			}

			// Token: 0x06005E35 RID: 24117 RVA: 0x0013C16C File Offset: 0x0013A36C
			public override void Sort(int index, int count, IComparer comparer)
			{
				if (index < 0 || count < 0)
				{
					throw new ArgumentOutOfRangeException((index < 0) ? "index" : "count", "Non-negative number required.");
				}
				if (this._baseSize - index < count)
				{
					throw new ArgumentException("Offset and length were out of bounds for the array or count is greater than the number of elements from index to the end of the source collection.");
				}
				this.InternalUpdateRange();
				this._baseList.Sort(this._baseIndex + index, count, comparer);
				this.InternalUpdateVersion();
			}

			// Token: 0x17001072 RID: 4210
			public override object this[int index]
			{
				get
				{
					this.InternalUpdateRange();
					if (index < 0 || index >= this._baseSize)
					{
						throw new ArgumentOutOfRangeException("index", "Index was out of range. Must be non-negative and less than the size of the collection.");
					}
					return this._baseList[this._baseIndex + index];
				}
				set
				{
					this.InternalUpdateRange();
					if (index < 0 || index >= this._baseSize)
					{
						throw new ArgumentOutOfRangeException("index", "Index was out of range. Must be non-negative and less than the size of the collection.");
					}
					this._baseList[this._baseIndex + index] = value;
					this.InternalUpdateVersion();
				}
			}

			// Token: 0x06005E38 RID: 24120 RVA: 0x0013C24C File Offset: 0x0013A44C
			public override object[] ToArray()
			{
				this.InternalUpdateRange();
				if (this._baseSize == 0)
				{
					return Array.Empty<object>();
				}
				object[] array = new object[this._baseSize];
				Array.Copy(this._baseList._items, this._baseIndex, array, 0, this._baseSize);
				return array;
			}

			// Token: 0x06005E39 RID: 24121 RVA: 0x0013C298 File Offset: 0x0013A498
			public override Array ToArray(Type type)
			{
				if (type == null)
				{
					throw new ArgumentNullException("type");
				}
				this.InternalUpdateRange();
				Array array = Array.CreateInstance(type, this._baseSize);
				this._baseList.CopyTo(this._baseIndex, array, 0, this._baseSize);
				return array;
			}

			// Token: 0x06005E3A RID: 24122 RVA: 0x0013C2E6 File Offset: 0x0013A4E6
			public override void TrimToSize()
			{
				throw new NotSupportedException("The specified operation is not supported on Ranges.");
			}

			// Token: 0x040038EA RID: 14570
			private ArrayList _baseList;

			// Token: 0x040038EB RID: 14571
			private int _baseIndex;

			// Token: 0x040038EC RID: 14572
			private int _baseSize;

			// Token: 0x040038ED RID: 14573
			private int _baseVersion;
		}

		// Token: 0x02000A46 RID: 2630
		[Serializable]
		private sealed class ArrayListEnumeratorSimple : IEnumerator, ICloneable
		{
			// Token: 0x06005E3B RID: 24123 RVA: 0x0013C2F4 File Offset: 0x0013A4F4
			internal ArrayListEnumeratorSimple(ArrayList list)
			{
				this._list = list;
				this._index = -1;
				this._version = list._version;
				this._isArrayList = (list.GetType() == typeof(ArrayList));
				this._currentElement = ArrayList.ArrayListEnumeratorSimple.s_dummyObject;
			}

			// Token: 0x06005E3C RID: 24124 RVA: 0x000231CD File Offset: 0x000213CD
			public object Clone()
			{
				return base.MemberwiseClone();
			}

			// Token: 0x06005E3D RID: 24125 RVA: 0x0013C348 File Offset: 0x0013A548
			public bool MoveNext()
			{
				if (this._version != this._list._version)
				{
					throw new InvalidOperationException("Collection was modified; enumeration operation may not execute.");
				}
				if (this._isArrayList)
				{
					if (this._index < this._list._size - 1)
					{
						object[] items = this._list._items;
						int num = this._index + 1;
						this._index = num;
						this._currentElement = items[num];
						return true;
					}
					this._currentElement = ArrayList.ArrayListEnumeratorSimple.s_dummyObject;
					this._index = this._list._size;
					return false;
				}
				else
				{
					if (this._index < this._list.Count - 1)
					{
						ArrayList list = this._list;
						int num = this._index + 1;
						this._index = num;
						this._currentElement = list[num];
						return true;
					}
					this._index = this._list.Count;
					this._currentElement = ArrayList.ArrayListEnumeratorSimple.s_dummyObject;
					return false;
				}
			}

			// Token: 0x17001073 RID: 4211
			// (get) Token: 0x06005E3E RID: 24126 RVA: 0x0013C42C File Offset: 0x0013A62C
			public object Current
			{
				get
				{
					object currentElement = this._currentElement;
					if (ArrayList.ArrayListEnumeratorSimple.s_dummyObject != currentElement)
					{
						return currentElement;
					}
					if (this._index == -1)
					{
						throw new InvalidOperationException("Enumeration has not started. Call MoveNext.");
					}
					throw new InvalidOperationException("Enumeration already finished.");
				}
			}

			// Token: 0x06005E3F RID: 24127 RVA: 0x0013C468 File Offset: 0x0013A668
			public void Reset()
			{
				if (this._version != this._list._version)
				{
					throw new InvalidOperationException("Collection was modified; enumeration operation may not execute.");
				}
				this._currentElement = ArrayList.ArrayListEnumeratorSimple.s_dummyObject;
				this._index = -1;
			}

			// Token: 0x06005E40 RID: 24128 RVA: 0x0013C49A File Offset: 0x0013A69A
			// Note: this type is marked as 'beforefieldinit'.
			static ArrayListEnumeratorSimple()
			{
			}

			// Token: 0x040038EE RID: 14574
			private ArrayList _list;

			// Token: 0x040038EF RID: 14575
			private int _index;

			// Token: 0x040038F0 RID: 14576
			private int _version;

			// Token: 0x040038F1 RID: 14577
			private object _currentElement;

			// Token: 0x040038F2 RID: 14578
			private bool _isArrayList;

			// Token: 0x040038F3 RID: 14579
			private static object s_dummyObject = new object();
		}

		// Token: 0x02000A47 RID: 2631
		internal class ArrayListDebugView
		{
			// Token: 0x06005E41 RID: 24129 RVA: 0x0013C4A6 File Offset: 0x0013A6A6
			public ArrayListDebugView(ArrayList arrayList)
			{
				if (arrayList == null)
				{
					throw new ArgumentNullException("arrayList");
				}
				this._arrayList = arrayList;
			}

			// Token: 0x17001074 RID: 4212
			// (get) Token: 0x06005E42 RID: 24130 RVA: 0x0013C4C3 File Offset: 0x0013A6C3
			[DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
			public object[] Items
			{
				get
				{
					return this._arrayList.ToArray();
				}
			}

			// Token: 0x040038F4 RID: 14580
			private ArrayList _arrayList;
		}
	}
}

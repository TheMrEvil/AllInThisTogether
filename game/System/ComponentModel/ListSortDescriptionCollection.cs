using System;
using System.Collections;

namespace System.ComponentModel
{
	/// <summary>Represents a collection of <see cref="T:System.ComponentModel.ListSortDescription" /> objects.</summary>
	// Token: 0x020003D1 RID: 977
	public class ListSortDescriptionCollection : IList, ICollection, IEnumerable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.ListSortDescriptionCollection" /> class.</summary>
		// Token: 0x06001F84 RID: 8068 RVA: 0x0006D608 File Offset: 0x0006B808
		public ListSortDescriptionCollection()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.ListSortDescriptionCollection" /> class with the specified array of <see cref="T:System.ComponentModel.ListSortDescription" /> objects.</summary>
		/// <param name="sorts">The array of <see cref="T:System.ComponentModel.ListSortDescription" /> objects to be contained in the collection.</param>
		// Token: 0x06001F85 RID: 8069 RVA: 0x0006D61C File Offset: 0x0006B81C
		public ListSortDescriptionCollection(ListSortDescription[] sorts)
		{
			if (sorts != null)
			{
				for (int i = 0; i < sorts.Length; i++)
				{
					this._sorts.Add(sorts[i]);
				}
			}
		}

		/// <summary>Gets or sets the specified <see cref="T:System.ComponentModel.ListSortDescription" />.</summary>
		/// <param name="index">The zero-based index of the <see cref="T:System.ComponentModel.ListSortDescription" /> to get or set in the collection.</param>
		/// <returns>The <see cref="T:System.ComponentModel.ListSortDescription" /> with the specified index.</returns>
		/// <exception cref="T:System.InvalidOperationException">An item is set in the <see cref="T:System.ComponentModel.ListSortDescriptionCollection" />, which is read-only.</exception>
		// Token: 0x1700066B RID: 1643
		public ListSortDescription this[int index]
		{
			get
			{
				return (ListSortDescription)this._sorts[index];
			}
			set
			{
				throw new InvalidOperationException("Once a ListSortDescriptionCollection has been created it can't be modified.");
			}
		}

		/// <summary>Gets a value indicating whether the collection has a fixed size.</summary>
		/// <returns>
		///   <see langword="true" /> in all cases.</returns>
		// Token: 0x1700066C RID: 1644
		// (get) Token: 0x06001F88 RID: 8072 RVA: 0x0000390E File Offset: 0x00001B0E
		bool IList.IsFixedSize
		{
			get
			{
				return true;
			}
		}

		/// <summary>Gets a value indicating whether the collection is read-only.</summary>
		/// <returns>
		///   <see langword="true" /> in all cases.</returns>
		// Token: 0x1700066D RID: 1645
		// (get) Token: 0x06001F89 RID: 8073 RVA: 0x0000390E File Offset: 0x00001B0E
		bool IList.IsReadOnly
		{
			get
			{
				return true;
			}
		}

		/// <summary>Gets the specified <see cref="T:System.ComponentModel.ListSortDescription" />.</summary>
		/// <param name="index">The zero-based index of the <see cref="T:System.ComponentModel.ListSortDescription" /> to get in the collection</param>
		/// <returns>The <see cref="T:System.ComponentModel.ListSortDescription" /> with the specified index.</returns>
		// Token: 0x1700066E RID: 1646
		object IList.this[int index]
		{
			get
			{
				return this[index];
			}
			set
			{
				throw new InvalidOperationException("Once a ListSortDescriptionCollection has been created it can't be modified.");
			}
		}

		/// <summary>Adds an item to the collection.</summary>
		/// <param name="value">The item to add to the collection.</param>
		/// <returns>The position into which the new element was inserted.</returns>
		/// <exception cref="T:System.InvalidOperationException">In all cases.</exception>
		// Token: 0x06001F8C RID: 8076 RVA: 0x0006D66D File Offset: 0x0006B86D
		int IList.Add(object value)
		{
			throw new InvalidOperationException("Once a ListSortDescriptionCollection has been created it can't be modified.");
		}

		/// <summary>Removes all items from the collection.</summary>
		/// <exception cref="T:System.InvalidOperationException">In all cases.</exception>
		// Token: 0x06001F8D RID: 8077 RVA: 0x0006D66D File Offset: 0x0006B86D
		void IList.Clear()
		{
			throw new InvalidOperationException("Once a ListSortDescriptionCollection has been created it can't be modified.");
		}

		/// <summary>Determines if the <see cref="T:System.ComponentModel.ListSortDescriptionCollection" /> contains a specific value.</summary>
		/// <param name="value">The <see cref="T:System.Object" /> to locate in the collection.</param>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Object" /> is found in the collection; otherwise, <see langword="false" />.</returns>
		// Token: 0x06001F8E RID: 8078 RVA: 0x0006D682 File Offset: 0x0006B882
		public bool Contains(object value)
		{
			return ((IList)this._sorts).Contains(value);
		}

		/// <summary>Returns the index of the specified item in the collection.</summary>
		/// <param name="value">The <see cref="T:System.Object" /> to locate in the collection.</param>
		/// <returns>The index of <paramref name="value" /> if found in the list; otherwise, -1.</returns>
		// Token: 0x06001F8F RID: 8079 RVA: 0x0006D690 File Offset: 0x0006B890
		public int IndexOf(object value)
		{
			return ((IList)this._sorts).IndexOf(value);
		}

		/// <summary>Inserts an item into the collection at a specified index.</summary>
		/// <param name="index">The zero-based index of the <see cref="T:System.ComponentModel.ListSortDescription" /> to get or set in the collection</param>
		/// <param name="value">The item to insert into the collection.</param>
		/// <exception cref="T:System.InvalidOperationException">In all cases.</exception>
		// Token: 0x06001F90 RID: 8080 RVA: 0x0006D66D File Offset: 0x0006B86D
		void IList.Insert(int index, object value)
		{
			throw new InvalidOperationException("Once a ListSortDescriptionCollection has been created it can't be modified.");
		}

		/// <summary>Removes the first occurrence of an item from the collection.</summary>
		/// <param name="value">The item to remove from the collection.</param>
		/// <exception cref="T:System.InvalidOperationException">In all cases.</exception>
		// Token: 0x06001F91 RID: 8081 RVA: 0x0006D66D File Offset: 0x0006B86D
		void IList.Remove(object value)
		{
			throw new InvalidOperationException("Once a ListSortDescriptionCollection has been created it can't be modified.");
		}

		/// <summary>Removes an item from the collection at a specified index.</summary>
		/// <param name="index">The zero-based index of the <see cref="T:System.ComponentModel.ListSortDescription" /> to remove from the collection</param>
		/// <exception cref="T:System.InvalidOperationException">In all cases.</exception>
		// Token: 0x06001F92 RID: 8082 RVA: 0x0006D66D File Offset: 0x0006B86D
		void IList.RemoveAt(int index)
		{
			throw new InvalidOperationException("Once a ListSortDescriptionCollection has been created it can't be modified.");
		}

		/// <summary>Gets the number of items in the collection.</summary>
		/// <returns>The number of items in the collection.</returns>
		// Token: 0x1700066F RID: 1647
		// (get) Token: 0x06001F93 RID: 8083 RVA: 0x0006D69E File Offset: 0x0006B89E
		public int Count
		{
			get
			{
				return this._sorts.Count;
			}
		}

		/// <summary>Gets a value indicating whether access to the collection is thread safe.</summary>
		/// <returns>
		///   <see langword="true" /> in all cases.</returns>
		// Token: 0x17000670 RID: 1648
		// (get) Token: 0x06001F94 RID: 8084 RVA: 0x0000390E File Offset: 0x00001B0E
		bool ICollection.IsSynchronized
		{
			get
			{
				return true;
			}
		}

		/// <summary>Gets the current instance that can be used to synchronize access to the collection.</summary>
		/// <returns>The current instance of the <see cref="T:System.ComponentModel.ListSortDescriptionCollection" />.</returns>
		// Token: 0x17000671 RID: 1649
		// (get) Token: 0x06001F95 RID: 8085 RVA: 0x000075E1 File Offset: 0x000057E1
		object ICollection.SyncRoot
		{
			get
			{
				return this;
			}
		}

		/// <summary>Copies the contents of the collection to the specified array, starting at the specified destination array index.</summary>
		/// <param name="array">The destination array for the items copied from the collection.</param>
		/// <param name="index">The index of the destination array at which copying begins.</param>
		// Token: 0x06001F96 RID: 8086 RVA: 0x0006D6AB File Offset: 0x0006B8AB
		public void CopyTo(Array array, int index)
		{
			this._sorts.CopyTo(array, index);
		}

		/// <summary>Gets a <see cref="T:System.Collections.IEnumerator" /> that can be used to iterate through the collection.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> that can be used to iterate through the collection.</returns>
		// Token: 0x06001F97 RID: 8087 RVA: 0x0006D6BA File Offset: 0x0006B8BA
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this._sorts.GetEnumerator();
		}

		// Token: 0x04000F6D RID: 3949
		private ArrayList _sorts = new ArrayList();
	}
}

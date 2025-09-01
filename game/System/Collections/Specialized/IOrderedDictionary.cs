using System;

namespace System.Collections.Specialized
{
	/// <summary>Represents an indexed collection of key/value pairs.</summary>
	// Token: 0x020004A6 RID: 1190
	public interface IOrderedDictionary : IDictionary, ICollection, IEnumerable
	{
		/// <summary>Gets or sets the element at the specified index.</summary>
		/// <param name="index">The zero-based index of the element to get or set.</param>
		/// <returns>The element at the specified index.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than 0.  
		/// -or-  
		/// <paramref name="index" /> is equal to or greater than <see cref="P:System.Collections.ICollection.Count" />.</exception>
		// Token: 0x170007B5 RID: 1973
		object this[int index]
		{
			get;
			set;
		}

		/// <summary>Returns an enumerator that iterates through the <see cref="T:System.Collections.Specialized.IOrderedDictionary" /> collection.</summary>
		/// <returns>An <see cref="T:System.Collections.IDictionaryEnumerator" /> for the entire <see cref="T:System.Collections.Specialized.IOrderedDictionary" /> collection.</returns>
		// Token: 0x06002645 RID: 9797
		IDictionaryEnumerator GetEnumerator();

		/// <summary>Inserts a key/value pair into the collection at the specified index.</summary>
		/// <param name="index">The zero-based index at which the key/value pair should be inserted.</param>
		/// <param name="key">The object to use as the key of the element to add.</param>
		/// <param name="value">The object to use as the value of the element to add.  The value can be <see langword="null" />.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than 0.  
		/// -or-  
		/// <paramref name="index" /> is greater than <see cref="P:System.Collections.ICollection.Count" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">An element with the same key already exists in the <see cref="T:System.Collections.Specialized.IOrderedDictionary" /> collection.</exception>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.Specialized.IOrderedDictionary" /> collection is read-only.  
		///  -or-  
		///  The <see cref="T:System.Collections.Specialized.IOrderedDictionary" /> collection has a fixed size.</exception>
		// Token: 0x06002646 RID: 9798
		void Insert(int index, object key, object value);

		/// <summary>Removes the element at the specified index.</summary>
		/// <param name="index">The zero-based index of the element to remove.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than 0.  
		/// -or-  
		/// <paramref name="index" /> is equal to or greater than <see cref="P:System.Collections.ICollection.Count" />.</exception>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.Specialized.IOrderedDictionary" /> collection is read-only.  
		///  -or-  
		///  The <see cref="T:System.Collections.Specialized.IOrderedDictionary" /> collection has a fixed size.</exception>
		// Token: 0x06002647 RID: 9799
		void RemoveAt(int index);
	}
}

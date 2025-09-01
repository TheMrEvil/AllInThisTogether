using System;
using System.Collections;
using System.Runtime.CompilerServices;

namespace System.Security.Cryptography.Xml
{
	/// <summary>Represents a collection of <see cref="T:System.Security.Cryptography.Xml.EncryptionProperty" /> classes used in XML encryption. This class cannot be inherited.</summary>
	// Token: 0x0200003B RID: 59
	public sealed class EncryptionPropertyCollection : IList, ICollection, IEnumerable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Xml.EncryptionPropertyCollection" /> class.</summary>
		// Token: 0x06000176 RID: 374 RVA: 0x00007928 File Offset: 0x00005B28
		public EncryptionPropertyCollection()
		{
			this._props = new ArrayList();
		}

		/// <summary>Returns an enumerator that iterates through an <see cref="T:System.Security.Cryptography.Xml.EncryptionPropertyCollection" /> object.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> object that can be used to iterate through an <see cref="T:System.Security.Cryptography.Xml.EncryptionPropertyCollection" /> object.</returns>
		// Token: 0x06000177 RID: 375 RVA: 0x0000793B File Offset: 0x00005B3B
		public IEnumerator GetEnumerator()
		{
			return this._props.GetEnumerator();
		}

		/// <summary>Gets the number of elements contained in the <see cref="T:System.Security.Cryptography.Xml.EncryptionPropertyCollection" /> object.</summary>
		/// <returns>The number of elements contained in the <see cref="T:System.Security.Cryptography.Xml.EncryptionPropertyCollection" /> object.</returns>
		// Token: 0x1700004E RID: 78
		// (get) Token: 0x06000178 RID: 376 RVA: 0x00007948 File Offset: 0x00005B48
		public int Count
		{
			get
			{
				return this._props.Count;
			}
		}

		/// <summary>Adds an item to the <see cref="T:System.Collections.IList" />.</summary>
		/// <param name="value">The <see cref="T:System.Object" /> to add to the <see cref="T:System.Collections.IList" />.</param>
		/// <returns>The position into which the new element was inserted.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="value" /> uses an incorrect object type.</exception>
		// Token: 0x06000179 RID: 377 RVA: 0x00007955 File Offset: 0x00005B55
		int IList.Add(object value)
		{
			if (!(value is EncryptionProperty))
			{
				throw new ArgumentException("Type of input object is invalid.", "value");
			}
			return this._props.Add(value);
		}

		/// <summary>Adds an <see cref="T:System.Security.Cryptography.Xml.EncryptionProperty" /> object to the <see cref="T:System.Security.Cryptography.Xml.EncryptionPropertyCollection" /> object.</summary>
		/// <param name="value">An <see cref="T:System.Security.Cryptography.Xml.EncryptionProperty" /> object to add to the <see cref="T:System.Security.Cryptography.Xml.EncryptionPropertyCollection" /> object.</param>
		/// <returns>The position at which the new element is inserted.</returns>
		// Token: 0x0600017A RID: 378 RVA: 0x0000797B File Offset: 0x00005B7B
		public int Add(EncryptionProperty value)
		{
			return this._props.Add(value);
		}

		/// <summary>Removes all items from the <see cref="T:System.Security.Cryptography.Xml.EncryptionPropertyCollection" /> object.</summary>
		// Token: 0x0600017B RID: 379 RVA: 0x00007989 File Offset: 0x00005B89
		public void Clear()
		{
			this._props.Clear();
		}

		/// <summary>Determines whether the <see cref="T:System.Collections.IList" /> contains a specific value.</summary>
		/// <param name="value">The <see cref="T:System.Object" /> to locate in the <see cref="T:System.Collections.IList" />.</param>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Object" /> is found in the <see cref="T:System.Collections.IList" />; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="value" /> uses an incorrect object type.</exception>
		// Token: 0x0600017C RID: 380 RVA: 0x00007996 File Offset: 0x00005B96
		bool IList.Contains(object value)
		{
			if (!(value is EncryptionProperty))
			{
				throw new ArgumentException("Type of input object is invalid.", "value");
			}
			return this._props.Contains(value);
		}

		/// <summary>Determines whether the <see cref="T:System.Security.Cryptography.Xml.EncryptionPropertyCollection" /> object contains a specific <see cref="T:System.Security.Cryptography.Xml.EncryptionProperty" /> object.</summary>
		/// <param name="value">The <see cref="T:System.Security.Cryptography.Xml.EncryptionProperty" /> object to locate in the <see cref="T:System.Security.Cryptography.Xml.EncryptionPropertyCollection" /> object.</param>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Security.Cryptography.Xml.EncryptionProperty" /> object is found in the <see cref="T:System.Security.Cryptography.Xml.EncryptionPropertyCollection" /> object; otherwise, <see langword="false" />.</returns>
		// Token: 0x0600017D RID: 381 RVA: 0x000079BC File Offset: 0x00005BBC
		public bool Contains(EncryptionProperty value)
		{
			return this._props.Contains(value);
		}

		/// <summary>Determines the index of a specific item in the <see cref="T:System.Collections.IList" />.</summary>
		/// <param name="value">The <see cref="T:System.Object" /> to locate in the <see cref="T:System.Collections.IList" />.</param>
		/// <returns>The index of <paramref name="value" /> if found in the list; otherwise, -1.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="value" /> uses an incorrect object type.</exception>
		// Token: 0x0600017E RID: 382 RVA: 0x000079CA File Offset: 0x00005BCA
		int IList.IndexOf(object value)
		{
			if (!(value is EncryptionProperty))
			{
				throw new ArgumentException("Type of input object is invalid.", "value");
			}
			return this._props.IndexOf(value);
		}

		/// <summary>Determines the index of a specific item in the <see cref="T:System.Security.Cryptography.Xml.EncryptionPropertyCollection" /> object.</summary>
		/// <param name="value">The <see cref="T:System.Security.Cryptography.Xml.EncryptionProperty" /> object to locate in the <see cref="T:System.Security.Cryptography.Xml.EncryptionPropertyCollection" /> object.</param>
		/// <returns>The index of <paramref name="value" /> if found in the collection; otherwise, -1.</returns>
		// Token: 0x0600017F RID: 383 RVA: 0x000079F0 File Offset: 0x00005BF0
		public int IndexOf(EncryptionProperty value)
		{
			return this._props.IndexOf(value);
		}

		/// <summary>Inserts an item to the <see cref="T:System.Collections.IList" /> at the specified index.</summary>
		/// <param name="index">The zero-based index at which <paramref name="value" /> should be inserted.</param>
		/// <param name="value">The <see cref="T:System.Object" /> to insert into the <see cref="T:System.Collections.IList" />.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="value" /> uses an incorrect object type.</exception>
		// Token: 0x06000180 RID: 384 RVA: 0x000079FE File Offset: 0x00005BFE
		void IList.Insert(int index, object value)
		{
			if (!(value is EncryptionProperty))
			{
				throw new ArgumentException("Type of input object is invalid.", "value");
			}
			this._props.Insert(index, value);
		}

		/// <summary>Inserts an <see cref="T:System.Security.Cryptography.Xml.EncryptionProperty" /> object into the <see cref="T:System.Security.Cryptography.Xml.EncryptionPropertyCollection" /> object at the specified position.</summary>
		/// <param name="index">The zero-based index at which <paramref name="value" /> should be inserted.</param>
		/// <param name="value">An <see cref="T:System.Security.Cryptography.Xml.EncryptionProperty" /> object to insert into the <see cref="T:System.Security.Cryptography.Xml.EncryptionPropertyCollection" /> object.</param>
		// Token: 0x06000181 RID: 385 RVA: 0x00007A25 File Offset: 0x00005C25
		public void Insert(int index, EncryptionProperty value)
		{
			this._props.Insert(index, value);
		}

		/// <summary>Removes the first occurrence of a specific object from the <see cref="T:System.Collections.IList" />.</summary>
		/// <param name="value">The <see cref="T:System.Object" /> to remove from the <see cref="T:System.Collections.IList" />.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="value" /> uses an incorrect object type.</exception>
		// Token: 0x06000182 RID: 386 RVA: 0x00007A34 File Offset: 0x00005C34
		void IList.Remove(object value)
		{
			if (!(value is EncryptionProperty))
			{
				throw new ArgumentException("Type of input object is invalid.", "value");
			}
			this._props.Remove(value);
		}

		/// <summary>Removes the first occurrence of a specific <see cref="T:System.Security.Cryptography.Xml.EncryptionProperty" /> object from the <see cref="T:System.Security.Cryptography.Xml.EncryptionPropertyCollection" /> object.</summary>
		/// <param name="value">The <see cref="T:System.Security.Cryptography.Xml.EncryptionProperty" /> object to remove from the <see cref="T:System.Security.Cryptography.Xml.EncryptionPropertyCollection" /> object.</param>
		// Token: 0x06000183 RID: 387 RVA: 0x00007A5A File Offset: 0x00005C5A
		public void Remove(EncryptionProperty value)
		{
			this._props.Remove(value);
		}

		/// <summary>Removes the <see cref="T:System.Security.Cryptography.Xml.EncryptionProperty" /> object at the specified index.</summary>
		/// <param name="index">The zero-based index of the <see cref="T:System.Security.Cryptography.Xml.EncryptionProperty" /> object to remove.</param>
		// Token: 0x06000184 RID: 388 RVA: 0x00007A68 File Offset: 0x00005C68
		public void RemoveAt(int index)
		{
			this._props.RemoveAt(index);
		}

		/// <summary>Gets a value that indicates whether the <see cref="T:System.Security.Cryptography.Xml.EncryptionPropertyCollection" /> object has a fixed size.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Security.Cryptography.Xml.EncryptionPropertyCollection" /> object has a fixed size; otherwise, <see langword="false" />.</returns>
		// Token: 0x1700004F RID: 79
		// (get) Token: 0x06000185 RID: 389 RVA: 0x00007A76 File Offset: 0x00005C76
		public bool IsFixedSize
		{
			get
			{
				return this._props.IsFixedSize;
			}
		}

		/// <summary>Gets a value that indicates whether the <see cref="T:System.Security.Cryptography.Xml.EncryptionPropertyCollection" /> object is read-only.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Security.Cryptography.Xml.EncryptionPropertyCollection" /> object is read-only; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000050 RID: 80
		// (get) Token: 0x06000186 RID: 390 RVA: 0x00007A83 File Offset: 0x00005C83
		public bool IsReadOnly
		{
			get
			{
				return this._props.IsReadOnly;
			}
		}

		/// <summary>Returns the <see cref="T:System.Security.Cryptography.Xml.EncryptionProperty" /> object at the specified index.</summary>
		/// <param name="index">The index of the <see cref="T:System.Security.Cryptography.Xml.EncryptionProperty" /> object to return.</param>
		/// <returns>The <see cref="T:System.Security.Cryptography.Xml.EncryptionProperty" /> object at the specified index.</returns>
		// Token: 0x06000187 RID: 391 RVA: 0x00007A90 File Offset: 0x00005C90
		public EncryptionProperty Item(int index)
		{
			return (EncryptionProperty)this._props[index];
		}

		/// <summary>Gets or sets the <see cref="T:System.Security.Cryptography.Xml.EncryptionProperty" /> object at the specified index.</summary>
		/// <param name="index">The index of the <see cref="T:System.Security.Cryptography.Xml.EncryptionProperty" /> object to return.</param>
		/// <returns>The <see cref="T:System.Security.Cryptography.Xml.EncryptionProperty" /> object at the specified index.</returns>
		// Token: 0x17000051 RID: 81
		[IndexerName("ItemOf")]
		public EncryptionProperty this[int index]
		{
			get
			{
				return (EncryptionProperty)((IList)this)[index];
			}
			set
			{
				((IList)this)[index] = value;
			}
		}

		/// <summary>Gets the element at the specified index.</summary>
		/// <param name="index">The <see cref="T:System.Object" /> to remove from the <see cref="T:System.Collections.IList" />.</param>
		/// <returns>The element at the specified index.</returns>
		// Token: 0x17000052 RID: 82
		object IList.this[int index]
		{
			get
			{
				return this._props[index];
			}
			set
			{
				if (!(value is EncryptionProperty))
				{
					throw new ArgumentException("Type of input object is invalid.", "value");
				}
				this._props[index] = value;
			}
		}

		/// <summary>Copies the elements of the <see cref="T:System.Security.Cryptography.Xml.EncryptionPropertyCollection" /> object to an array, starting at a particular array index.</summary>
		/// <param name="array">The one-dimensional <see cref="T:System.Array" /> object that is the destination of the elements copied from the <see cref="T:System.Security.Cryptography.Xml.EncryptionPropertyCollection" /> object. The array must have zero-based indexing.</param>
		/// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins.</param>
		// Token: 0x0600018C RID: 396 RVA: 0x00007AF0 File Offset: 0x00005CF0
		public void CopyTo(Array array, int index)
		{
			this._props.CopyTo(array, index);
		}

		/// <summary>Copies the elements of the <see cref="T:System.Security.Cryptography.Xml.EncryptionPropertyCollection" /> object to an array of <see cref="T:System.Security.Cryptography.Xml.EncryptionProperty" /> objects, starting at a particular array index.</summary>
		/// <param name="array">The one-dimensional array of  <see cref="T:System.Security.Cryptography.Xml.EncryptionProperty" /> objects that is the destination of the elements copied from the <see cref="T:System.Security.Cryptography.Xml.EncryptionPropertyCollection" /> object. The array must have zero-based indexing.</param>
		/// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins.</param>
		// Token: 0x0600018D RID: 397 RVA: 0x00007AF0 File Offset: 0x00005CF0
		public void CopyTo(EncryptionProperty[] array, int index)
		{
			this._props.CopyTo(array, index);
		}

		/// <summary>Gets an object that can be used to synchronize access to the <see cref="T:System.Security.Cryptography.Xml.EncryptionPropertyCollection" /> object.</summary>
		/// <returns>An object that can be used to synchronize access to the <see cref="T:System.Security.Cryptography.Xml.EncryptionPropertyCollection" /> object.</returns>
		// Token: 0x17000053 RID: 83
		// (get) Token: 0x0600018E RID: 398 RVA: 0x00007AFF File Offset: 0x00005CFF
		public object SyncRoot
		{
			get
			{
				return this._props.SyncRoot;
			}
		}

		/// <summary>Gets a value that indicates whether access to the <see cref="T:System.Security.Cryptography.Xml.EncryptionPropertyCollection" /> object is synchronized (thread safe).</summary>
		/// <returns>
		///   <see langword="true" /> if access to the <see cref="T:System.Security.Cryptography.Xml.EncryptionPropertyCollection" /> object is synchronized (thread safe); otherwise, <see langword="false" />.</returns>
		// Token: 0x17000054 RID: 84
		// (get) Token: 0x0600018F RID: 399 RVA: 0x00007B0C File Offset: 0x00005D0C
		public bool IsSynchronized
		{
			get
			{
				return this._props.IsSynchronized;
			}
		}

		// Token: 0x040001A0 RID: 416
		private ArrayList _props;
	}
}

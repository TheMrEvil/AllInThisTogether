using System;
using System.Collections;
using System.Runtime.CompilerServices;

namespace System.Security.Cryptography.Xml
{
	/// <summary>Represents the <see langword="&lt;ReferenceList&gt;" /> element used in XML encryption. This class cannot be inherited.</summary>
	// Token: 0x02000052 RID: 82
	public sealed class ReferenceList : IList, ICollection, IEnumerable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Xml.ReferenceList" /> class.</summary>
		// Token: 0x0600021D RID: 541 RVA: 0x00009CB4 File Offset: 0x00007EB4
		public ReferenceList()
		{
			this._references = new ArrayList();
		}

		/// <summary>Returns an enumerator that iterates through a <see cref="T:System.Security.Cryptography.Xml.ReferenceList" /> collection.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> object that can be used to iterate through a <see cref="T:System.Security.Cryptography.Xml.ReferenceList" /> collection.</returns>
		// Token: 0x0600021E RID: 542 RVA: 0x00009CC7 File Offset: 0x00007EC7
		public IEnumerator GetEnumerator()
		{
			return this._references.GetEnumerator();
		}

		/// <summary>Gets the number of elements contained in the <see cref="T:System.Security.Cryptography.Xml.ReferenceList" /> object.</summary>
		/// <returns>The number of elements contained in the <see cref="T:System.Security.Cryptography.Xml.ReferenceList" /> object.</returns>
		// Token: 0x1700006C RID: 108
		// (get) Token: 0x0600021F RID: 543 RVA: 0x00009CD4 File Offset: 0x00007ED4
		public int Count
		{
			get
			{
				return this._references.Count;
			}
		}

		/// <summary>Adds a <see cref="T:System.Security.Cryptography.Xml.DataReference" /> or <see cref="T:System.Security.Cryptography.Xml.KeyReference" /> object to the <see cref="T:System.Security.Cryptography.Xml.ReferenceList" /> collection.</summary>
		/// <param name="value">A <see cref="T:System.Security.Cryptography.Xml.DataReference" /> or <see cref="T:System.Security.Cryptography.Xml.KeyReference" /> object to add to the <see cref="T:System.Security.Cryptography.Xml.ReferenceList" /> collection.</param>
		/// <returns>The position at which the new element was inserted.</returns>
		/// <exception cref="T:System.ArgumentException">The <paramref name="value" /> parameter is not a <see cref="T:System.Security.Cryptography.Xml.DataReference" /> object.  
		///  -or-  
		///  The <paramref name="value" /> parameter is not a <see cref="T:System.Security.Cryptography.Xml.KeyReference" /> object.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="value" /> parameter is <see langword="null" />.</exception>
		// Token: 0x06000220 RID: 544 RVA: 0x00009CE1 File Offset: 0x00007EE1
		public int Add(object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (!(value is DataReference) && !(value is KeyReference))
			{
				throw new ArgumentException("Type of input object is invalid.", "value");
			}
			return this._references.Add(value);
		}

		/// <summary>Removes all items from the <see cref="T:System.Security.Cryptography.Xml.ReferenceList" /> collection.</summary>
		// Token: 0x06000221 RID: 545 RVA: 0x00009D1D File Offset: 0x00007F1D
		public void Clear()
		{
			this._references.Clear();
		}

		/// <summary>Determines whether the <see cref="T:System.Security.Cryptography.Xml.ReferenceList" /> collection contains a specific <see cref="T:System.Security.Cryptography.Xml.DataReference" /> or <see cref="T:System.Security.Cryptography.Xml.KeyReference" /> object.</summary>
		/// <param name="value">The <see cref="T:System.Security.Cryptography.Xml.DataReference" /> or <see cref="T:System.Security.Cryptography.Xml.KeyReference" /> object to locate in the <see cref="T:System.Security.Cryptography.Xml.ReferenceList" /> collection.</param>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Security.Cryptography.Xml.DataReference" /> or <see cref="T:System.Security.Cryptography.Xml.KeyReference" /> object is found in the <see cref="T:System.Security.Cryptography.Xml.ReferenceList" /> collection; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000222 RID: 546 RVA: 0x00009D2A File Offset: 0x00007F2A
		public bool Contains(object value)
		{
			return this._references.Contains(value);
		}

		/// <summary>Determines the index of a specific item in the <see cref="T:System.Security.Cryptography.Xml.ReferenceList" /> collection.</summary>
		/// <param name="value">The <see cref="T:System.Security.Cryptography.Xml.DataReference" /> or <see cref="T:System.Security.Cryptography.Xml.KeyReference" /> object to locate in the <see cref="T:System.Security.Cryptography.Xml.ReferenceList" /> collection.</param>
		/// <returns>The index of <paramref name="value" /> if found in the collection; otherwise, -1.</returns>
		// Token: 0x06000223 RID: 547 RVA: 0x00009D38 File Offset: 0x00007F38
		public int IndexOf(object value)
		{
			return this._references.IndexOf(value);
		}

		/// <summary>Inserts a <see cref="T:System.Security.Cryptography.Xml.DataReference" /> or <see cref="T:System.Security.Cryptography.Xml.KeyReference" /> object into the <see cref="T:System.Security.Cryptography.Xml.ReferenceList" /> collection at the specified position.</summary>
		/// <param name="index">The zero-based index at which <paramref name="value" /> should be inserted.</param>
		/// <param name="value">A <see cref="T:System.Security.Cryptography.Xml.DataReference" /> or <see cref="T:System.Security.Cryptography.Xml.KeyReference" /> object to insert into the <see cref="T:System.Security.Cryptography.Xml.ReferenceList" /> collection.</param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="value" /> parameter is not a <see cref="T:System.Security.Cryptography.Xml.DataReference" /> object.  
		///  -or-  
		///  The <paramref name="value" /> parameter is not a <see cref="T:System.Security.Cryptography.Xml.KeyReference" /> object.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="value" /> parameter is <see langword="null" />.</exception>
		// Token: 0x06000224 RID: 548 RVA: 0x00009D46 File Offset: 0x00007F46
		public void Insert(int index, object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (!(value is DataReference) && !(value is KeyReference))
			{
				throw new ArgumentException("Type of input object is invalid.", "value");
			}
			this._references.Insert(index, value);
		}

		/// <summary>Removes the first occurrence of a specific <see cref="T:System.Security.Cryptography.Xml.DataReference" /> or <see cref="T:System.Security.Cryptography.Xml.KeyReference" /> object from the <see cref="T:System.Security.Cryptography.Xml.ReferenceList" /> collection.</summary>
		/// <param name="value">The <see cref="T:System.Security.Cryptography.Xml.DataReference" /> or <see cref="T:System.Security.Cryptography.Xml.KeyReference" /> object to remove from the <see cref="T:System.Security.Cryptography.Xml.ReferenceList" /> collection.</param>
		// Token: 0x06000225 RID: 549 RVA: 0x00009D83 File Offset: 0x00007F83
		public void Remove(object value)
		{
			this._references.Remove(value);
		}

		/// <summary>Removes the <see cref="T:System.Security.Cryptography.Xml.DataReference" /> or <see cref="T:System.Security.Cryptography.Xml.KeyReference" /> object at the specified index.</summary>
		/// <param name="index">The zero-based index of the <see cref="T:System.Security.Cryptography.Xml.DataReference" /> or <see cref="T:System.Security.Cryptography.Xml.KeyReference" /> object to remove.</param>
		// Token: 0x06000226 RID: 550 RVA: 0x00009D91 File Offset: 0x00007F91
		public void RemoveAt(int index)
		{
			this._references.RemoveAt(index);
		}

		/// <summary>Returns the <see cref="T:System.Security.Cryptography.Xml.DataReference" /> or <see cref="T:System.Security.Cryptography.Xml.KeyReference" /> object at the specified index.</summary>
		/// <param name="index">The index of the <see cref="T:System.Security.Cryptography.Xml.DataReference" /> or <see cref="T:System.Security.Cryptography.Xml.KeyReference" /> object to return.</param>
		/// <returns>The <see cref="T:System.Security.Cryptography.Xml.DataReference" /> or <see cref="T:System.Security.Cryptography.Xml.KeyReference" /> object at the specified index.</returns>
		// Token: 0x06000227 RID: 551 RVA: 0x00009D9F File Offset: 0x00007F9F
		public EncryptedReference Item(int index)
		{
			return (EncryptedReference)this._references[index];
		}

		/// <summary>Gets or sets the <see cref="T:System.Security.Cryptography.Xml.DataReference" /> or <see cref="T:System.Security.Cryptography.Xml.KeyReference" /> object at the specified index.</summary>
		/// <param name="index">The index of the <see cref="T:System.Security.Cryptography.Xml.DataReference" /> or <see cref="T:System.Security.Cryptography.Xml.KeyReference" /> object to return.</param>
		/// <returns>The <see cref="T:System.Security.Cryptography.Xml.ReferenceList" /> object at the specified index.</returns>
		// Token: 0x1700006D RID: 109
		[IndexerName("ItemOf")]
		public EncryptedReference this[int index]
		{
			get
			{
				return this.Item(index);
			}
			set
			{
				((IList)this)[index] = value;
			}
		}

		/// <summary>For a description of this member, see <see cref="P:System.Collections.IList.Item(System.Int32)" />.</summary>
		/// <param name="index">The zero-based index of the element to get or set.</param>
		/// <returns>The element at the specified index.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is not a valid index in the <see cref="T:System.Collections.IList" />.</exception>
		// Token: 0x1700006E RID: 110
		object IList.this[int index]
		{
			get
			{
				return this._references[index];
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				if (!(value is DataReference) && !(value is KeyReference))
				{
					throw new ArgumentException("Type of input object is invalid.", "value");
				}
				this._references[index] = value;
			}
		}

		/// <summary>Copies the elements of the <see cref="T:System.Security.Cryptography.Xml.ReferenceList" /> object to an array, starting at a specified array index.</summary>
		/// <param name="array">The one-dimensional <see cref="T:System.Array" /> object that is the destination of the elements copied from the <see cref="T:System.Security.Cryptography.Xml.ReferenceList" /> object. The array must have zero-based indexing.</param>
		/// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins.</param>
		// Token: 0x0600022C RID: 556 RVA: 0x00009E06 File Offset: 0x00008006
		public void CopyTo(Array array, int index)
		{
			this._references.CopyTo(array, index);
		}

		/// <summary>For a description of this member, see <see cref="P:System.Collections.IList.IsFixedSize" />.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Collections.IList" /> has a fixed size; otherwise, <see langword="false" />.</returns>
		// Token: 0x1700006F RID: 111
		// (get) Token: 0x0600022D RID: 557 RVA: 0x00009E15 File Offset: 0x00008015
		bool IList.IsFixedSize
		{
			get
			{
				return this._references.IsFixedSize;
			}
		}

		/// <summary>For a description of this member, see <see cref="P:System.Collections.IList.IsReadOnly" />.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Collections.IList" /> is read-only; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000070 RID: 112
		// (get) Token: 0x0600022E RID: 558 RVA: 0x00009E22 File Offset: 0x00008022
		bool IList.IsReadOnly
		{
			get
			{
				return this._references.IsReadOnly;
			}
		}

		/// <summary>Gets an object that can be used to synchronize access to the <see cref="T:System.Security.Cryptography.Xml.ReferenceList" /> object.</summary>
		/// <returns>An object that can be used to synchronize access to the <see cref="T:System.Security.Cryptography.Xml.ReferenceList" /> object.</returns>
		// Token: 0x17000071 RID: 113
		// (get) Token: 0x0600022F RID: 559 RVA: 0x00009E2F File Offset: 0x0000802F
		public object SyncRoot
		{
			get
			{
				return this._references.SyncRoot;
			}
		}

		/// <summary>Gets a value that indicates whether access to the <see cref="T:System.Security.Cryptography.Xml.ReferenceList" /> object is synchronized (thread safe).</summary>
		/// <returns>
		///   <see langword="true" /> if access to the <see cref="T:System.Security.Cryptography.Xml.ReferenceList" /> object is synchronized (thread safe); otherwise, <see langword="false" />.</returns>
		// Token: 0x17000072 RID: 114
		// (get) Token: 0x06000230 RID: 560 RVA: 0x00009E3C File Offset: 0x0000803C
		public bool IsSynchronized
		{
			get
			{
				return this._references.IsSynchronized;
			}
		}

		// Token: 0x040001C4 RID: 452
		private ArrayList _references;
	}
}

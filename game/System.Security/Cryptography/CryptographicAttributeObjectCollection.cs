using System;
using System.Collections;
using System.Collections.Generic;

namespace System.Security.Cryptography
{
	/// <summary>Contains a set of <see cref="T:System.Security.Cryptography.CryptographicAttributeObject" /> objects.</summary>
	// Token: 0x0200000E RID: 14
	public sealed class CryptographicAttributeObjectCollection : ICollection, IEnumerable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.CryptographicAttributeObjectCollection" /> class.</summary>
		// Token: 0x0600002D RID: 45 RVA: 0x000026D4 File Offset: 0x000008D4
		public CryptographicAttributeObjectCollection()
		{
			this._list = new List<CryptographicAttributeObject>();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.CryptographicAttributeObjectCollection" /> class, adding a specified <see cref="T:System.Security.Cryptography.CryptographicAttributeObject" /> to the collection.</summary>
		/// <param name="attribute">A <see cref="T:System.Security.Cryptography.CryptographicAttributeObject" /> object that is added to the collection.</param>
		// Token: 0x0600002E RID: 46 RVA: 0x000026E7 File Offset: 0x000008E7
		public CryptographicAttributeObjectCollection(CryptographicAttributeObject attribute)
		{
			this._list = new List<CryptographicAttributeObject>();
			this._list.Add(attribute);
		}

		/// <summary>Adds the specified <see cref="T:System.Security.Cryptography.AsnEncodedData" /> object to the collection.</summary>
		/// <param name="asnEncodedData">The <see cref="T:System.Security.Cryptography.AsnEncodedData" /> object to add to the collection.</param>
		/// <returns>
		///   <see langword="true" /> if the method returns the zero-based index of the added item; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="asnEncodedData" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">A cryptographic operation could not be completed.</exception>
		// Token: 0x0600002F RID: 47 RVA: 0x00002706 File Offset: 0x00000906
		public int Add(AsnEncodedData asnEncodedData)
		{
			if (asnEncodedData == null)
			{
				throw new ArgumentNullException("asnEncodedData");
			}
			return this.Add(new CryptographicAttributeObject(asnEncodedData.Oid, new AsnEncodedDataCollection(asnEncodedData)));
		}

		/// <summary>Adds the specified <see cref="T:System.Security.Cryptography.CryptographicAttributeObject" /> object to the collection.</summary>
		/// <param name="attribute">The <see cref="T:System.Security.Cryptography.CryptographicAttributeObject" /> object to add to the collection.</param>
		/// <returns>
		///   <see langword="true" /> if the method returns the zero-based index of the added item; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="asnEncodedData" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">A cryptographic operation could not be completed.</exception>
		/// <exception cref="T:System.InvalidOperationException">The specified item already exists in the collection.</exception>
		// Token: 0x06000030 RID: 48 RVA: 0x00002730 File Offset: 0x00000930
		public int Add(CryptographicAttributeObject attribute)
		{
			if (attribute == null)
			{
				throw new ArgumentNullException("attribute");
			}
			string value = attribute.Oid.Value;
			int i = 0;
			while (i < this._list.Count)
			{
				CryptographicAttributeObject cryptographicAttributeObject = this._list[i];
				if (cryptographicAttributeObject.Values == attribute.Values)
				{
					throw new InvalidOperationException("Duplicate items are not allowed in the collection.");
				}
				string value2 = cryptographicAttributeObject.Oid.Value;
				if (string.Equals(value, value2, StringComparison.OrdinalIgnoreCase))
				{
					if (string.Equals(value, "1.2.840.113549.1.9.5", StringComparison.OrdinalIgnoreCase))
					{
						throw new CryptographicException("Cannot add multiple PKCS 9 signing time attributes.");
					}
					foreach (AsnEncodedData asnEncodedData in attribute.Values)
					{
						cryptographicAttributeObject.Values.Add(asnEncodedData);
					}
					return i;
				}
				else
				{
					i++;
				}
			}
			int count = this._list.Count;
			this._list.Add(attribute);
			return count;
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00002810 File Offset: 0x00000A10
		internal void AddWithoutMerge(CryptographicAttributeObject attribute)
		{
			this._list.Add(attribute);
		}

		/// <summary>Removes the specified <see cref="T:System.Security.Cryptography.CryptographicAttributeObject" /> object from the collection.</summary>
		/// <param name="attribute">The <see cref="T:System.Security.Cryptography.CryptographicAttributeObject" /> object to remove from the collection.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="attribute" /> is <see langword="null" />.</exception>
		// Token: 0x06000032 RID: 50 RVA: 0x0000281E File Offset: 0x00000A1E
		public void Remove(CryptographicAttributeObject attribute)
		{
			if (attribute == null)
			{
				throw new ArgumentNullException("attribute");
			}
			this._list.Remove(attribute);
		}

		/// <summary>Gets the <see cref="T:System.Security.Cryptography.CryptographicAttributeObject" /> object at the specified index in the collection.</summary>
		/// <param name="index">An <see cref="T:System.Int32" /> value that represents the zero-based index of the <see cref="T:System.Security.Cryptography.CryptographicAttributeObject" /> object to retrieve.</param>
		/// <returns>The <see cref="T:System.Security.Cryptography.CryptographicAttributeObject" /> object at the specified index.</returns>
		// Token: 0x17000009 RID: 9
		public CryptographicAttributeObject this[int index]
		{
			get
			{
				return this._list[index];
			}
		}

		/// <summary>Gets the number of items in the collection.</summary>
		/// <returns>The number of items in the collection.</returns>
		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000034 RID: 52 RVA: 0x00002849 File Offset: 0x00000A49
		public int Count
		{
			get
			{
				return this._list.Count;
			}
		}

		/// <summary>Gets a value that indicates whether access to the collection is synchronized, or thread safe.</summary>
		/// <returns>
		///   <see langword="true" /> if access to the collection is thread safe; otherwise <see langword="false" />.</returns>
		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000035 RID: 53 RVA: 0x00002856 File Offset: 0x00000A56
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets an <see cref="T:System.Object" /> object used to synchronize access to the collection.</summary>
		/// <returns>An <see cref="T:System.Object" /> object used to synchronize access to the <see cref="T:System.Security.Cryptography.CryptographicAttributeObjectCollection" /> collection.</returns>
		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000036 RID: 54 RVA: 0x00002859 File Offset: 0x00000A59
		public object SyncRoot
		{
			get
			{
				return this;
			}
		}

		/// <summary>Gets a <see cref="T:System.Security.Cryptography.CryptographicAttributeObjectEnumerator" /> object for the collection.</summary>
		/// <returns>
		///   <see langword="true" /> if the method returns a <see cref="T:System.Security.Cryptography.CryptographicAttributeObjectEnumerator" /> object that can be used to enumerate the collection; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000037 RID: 55 RVA: 0x0000285C File Offset: 0x00000A5C
		public CryptographicAttributeObjectEnumerator GetEnumerator()
		{
			return new CryptographicAttributeObjectEnumerator(this);
		}

		/// <summary>Returns an enumerator that iterates through the <see cref="T:System.Security.Cryptography.CryptographicAttributeObjectCollection" /> collection.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> object that can be used to iterate through the <see cref="T:System.Security.Cryptography.CryptographicAttributeObjectCollection" /> collection.</returns>
		// Token: 0x06000038 RID: 56 RVA: 0x0000285C File Offset: 0x00000A5C
		IEnumerator IEnumerable.GetEnumerator()
		{
			return new CryptographicAttributeObjectEnumerator(this);
		}

		/// <summary>Copies the elements of this <see cref="T:System.Security.Cryptography.CryptographicAttributeObjectCollection" /> collection to an <see cref="T:System.Array" /> array, starting at a particular index.</summary>
		/// <param name="array">The one-dimensional <see cref="T:System.Array" /> array that is the destination of the elements copied from this <see cref="T:System.Security.Cryptography.CryptographicAttributeObjectCollection" />. The <see cref="T:System.Array" /> array must have zero-based indexing.</param>
		/// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins.</param>
		// Token: 0x06000039 RID: 57 RVA: 0x00002864 File Offset: 0x00000A64
		void ICollection.CopyTo(Array array, int index)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (array.Rank != 1)
			{
				throw new ArgumentException("Only single dimensional arrays are supported for the requested action.");
			}
			if (index < 0 || index >= array.Length)
			{
				throw new ArgumentOutOfRangeException("index", "Index was out of range. Must be non-negative and less than the size of the collection.");
			}
			if (index > array.Length - this.Count)
			{
				throw new ArgumentException("Offset and length were out of bounds for the array or count is greater than the number of elements from index to the end of the source collection.");
			}
			for (int i = 0; i < this.Count; i++)
			{
				array.SetValue(this[i], index);
				index++;
			}
		}

		/// <summary>Copies the <see cref="T:System.Security.Cryptography.CryptographicAttributeObjectCollection" /> collection to an array of <see cref="T:System.Security.Cryptography.CryptographicAttributeObject" /> objects.</summary>
		/// <param name="array">An array of <see cref="T:System.Security.Cryptography.CryptographicAttributeObject" /> objects that the collection is copied to.</param>
		/// <param name="index">The zero-based index in <paramref name="array" /> to which the collection is to be copied.</param>
		/// <exception cref="T:System.ArgumentException">One of the arguments provided to a method was not valid.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <see langword="null" /> was passed to a method that does not accept it as a valid argument.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value of an argument was outside the allowable range of values as defined by the called method.</exception>
		// Token: 0x0600003A RID: 58 RVA: 0x000028F0 File Offset: 0x00000AF0
		public void CopyTo(CryptographicAttributeObject[] array, int index)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (index < 0 || index >= array.Length)
			{
				throw new ArgumentOutOfRangeException("index", "Index was out of range. Must be non-negative and less than the size of the collection.");
			}
			if (index > array.Length - this.Count)
			{
				throw new ArgumentException("Offset and length were out of bounds for the array or count is greater than the number of elements from index to the end of the source collection.");
			}
			this._list.CopyTo(array, index);
		}

		// Token: 0x04000082 RID: 130
		private readonly List<CryptographicAttributeObject> _list;
	}
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

namespace System.Security.Cryptography.Pkcs
{
	/// <summary>The <see cref="T:System.Security.Cryptography.Pkcs.CmsRecipientCollection" /> class represents a set of <see cref="T:System.Security.Cryptography.Pkcs.CmsRecipient" /> objects. <see cref="T:System.Security.Cryptography.Pkcs.CmsRecipientCollection" /> implements the <see cref="T:System.Collections.ICollection" /> interface.</summary>
	// Token: 0x0200006A RID: 106
	public sealed class CmsRecipientCollection : ICollection, IEnumerable
	{
		/// <summary>The <see cref="M:System.Security.Cryptography.Pkcs.CmsRecipientCollection.#ctor" /> constructor creates an instance of the <see cref="T:System.Security.Cryptography.Pkcs.CmsRecipientCollection" /> class.</summary>
		// Token: 0x06000382 RID: 898 RVA: 0x00010ED2 File Offset: 0x0000F0D2
		public CmsRecipientCollection()
		{
			this._recipients = new List<CmsRecipient>();
		}

		/// <summary>The <see cref="M:System.Security.Cryptography.Pkcs.CmsRecipientCollection.#ctor(System.Security.Cryptography.Pkcs.CmsRecipient)" /> constructor creates an instance of the <see cref="T:System.Security.Cryptography.Pkcs.CmsRecipientCollection" /> class and adds the specified recipient.</summary>
		/// <param name="recipient">An instance of the <see cref="T:System.Security.Cryptography.Pkcs.CmsRecipient" /> class that represents the specified CMS/PKCS #7 recipient.</param>
		// Token: 0x06000383 RID: 899 RVA: 0x00010EE5 File Offset: 0x0000F0E5
		public CmsRecipientCollection(CmsRecipient recipient)
		{
			this._recipients = new List<CmsRecipient>(1);
			this._recipients.Add(recipient);
		}

		/// <summary>The <see cref="M:System.Security.Cryptography.Pkcs.CmsRecipientCollection.#ctor(System.Security.Cryptography.Pkcs.SubjectIdentifierType,System.Security.Cryptography.X509Certificates.X509Certificate2Collection)" /> constructor creates an instance of the <see cref="T:System.Security.Cryptography.Pkcs.CmsRecipientCollection" /> class and adds recipients based on the specified subject identifier and set of certificates that identify the recipients.</summary>
		/// <param name="recipientIdentifierType">A member of the <see cref="T:System.Security.Cryptography.Pkcs.SubjectIdentifierType" /> enumeration that specifies the type of subject identifier.</param>
		/// <param name="certificates">An <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2Collection" /> collection that contains the certificates that identify the recipients.</param>
		// Token: 0x06000384 RID: 900 RVA: 0x00010F08 File Offset: 0x0000F108
		public CmsRecipientCollection(SubjectIdentifierType recipientIdentifierType, X509Certificate2Collection certificates)
		{
			if (certificates == null)
			{
				throw new NullReferenceException();
			}
			this._recipients = new List<CmsRecipient>(certificates.Count);
			for (int i = 0; i < certificates.Count; i++)
			{
				this._recipients.Add(new CmsRecipient(recipientIdentifierType, certificates[i]));
			}
		}

		/// <summary>The <see cref="P:System.Security.Cryptography.Pkcs.CmsRecipientCollection.Item(System.Int32)" /> property retrieves the <see cref="T:System.Security.Cryptography.Pkcs.CmsRecipient" /> object at the specified index in the collection.</summary>
		/// <param name="index">An <see cref="T:System.Int32" /> value that represents the index in the collection. The index is zero based.</param>
		/// <returns>A <see cref="T:System.Security.Cryptography.Pkcs.CmsRecipient" /> object at the specified index.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value of an argument was outside the allowable range of values as defined by the called method.</exception>
		// Token: 0x170000BE RID: 190
		public CmsRecipient this[int index]
		{
			get
			{
				if (index < 0 || index >= this._recipients.Count)
				{
					throw new ArgumentOutOfRangeException("index", "Index was out of range. Must be non-negative and less than the size of the collection.");
				}
				return this._recipients[index];
			}
		}

		/// <summary>The <see cref="P:System.Security.Cryptography.Pkcs.CmsRecipientCollection.Count" /> property retrieves the number of items in the <see cref="T:System.Security.Cryptography.Pkcs.CmsRecipientCollection" /> collection.</summary>
		/// <returns>An <see cref="T:System.Int32" /> value that represents the number of items in the collection.</returns>
		// Token: 0x170000BF RID: 191
		// (get) Token: 0x06000386 RID: 902 RVA: 0x00010F8E File Offset: 0x0000F18E
		public int Count
		{
			get
			{
				return this._recipients.Count;
			}
		}

		/// <summary>The <see cref="M:System.Security.Cryptography.Pkcs.CmsRecipientCollection.Add(System.Security.Cryptography.Pkcs.CmsRecipient)" /> method adds a recipient to the <see cref="T:System.Security.Cryptography.Pkcs.CmsRecipientCollection" /> collection.</summary>
		/// <param name="recipient">A <see cref="T:System.Security.Cryptography.Pkcs.CmsRecipient" /> object that represents the recipient to add to the <see cref="T:System.Security.Cryptography.Pkcs.CmsRecipientCollection" /> collection.</param>
		/// <returns>If the method succeeds, the method returns an <see cref="T:System.Int32" /> value that represents the zero-based position where the recipient is to be inserted.  
		///  If the method fails, it throws an exception.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="recipient" /> is <see langword="null" />.</exception>
		// Token: 0x06000387 RID: 903 RVA: 0x00010F9B File Offset: 0x0000F19B
		public int Add(CmsRecipient recipient)
		{
			if (recipient == null)
			{
				throw new ArgumentNullException("recipient");
			}
			int count = this._recipients.Count;
			this._recipients.Add(recipient);
			return count;
		}

		/// <summary>The <see cref="M:System.Security.Cryptography.Pkcs.CmsRecipientCollection.Remove(System.Security.Cryptography.Pkcs.CmsRecipient)" /> method removes a recipient from the <see cref="T:System.Security.Cryptography.Pkcs.CmsRecipientCollection" /> collection.</summary>
		/// <param name="recipient">A <see cref="T:System.Security.Cryptography.Pkcs.CmsRecipient" /> object that represents the recipient to remove from the collection.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="recipient" /> is <see langword="null" />.</exception>
		// Token: 0x06000388 RID: 904 RVA: 0x00010FC2 File Offset: 0x0000F1C2
		public void Remove(CmsRecipient recipient)
		{
			if (recipient == null)
			{
				throw new ArgumentNullException("recipient");
			}
			this._recipients.Remove(recipient);
		}

		/// <summary>The <see cref="M:System.Security.Cryptography.Pkcs.CmsRecipientCollection.GetEnumerator" /> method returns a <see cref="T:System.Security.Cryptography.Pkcs.CmsRecipientEnumerator" /> object for the <see cref="T:System.Security.Cryptography.Pkcs.CmsRecipientCollection" /> collection.</summary>
		/// <returns>A <see cref="T:System.Security.Cryptography.Pkcs.CmsRecipientEnumerator" /> object that can be used to enumerate the <see cref="T:System.Security.Cryptography.Pkcs.CmsRecipientCollection" /> collection.</returns>
		// Token: 0x06000389 RID: 905 RVA: 0x00010FDF File Offset: 0x0000F1DF
		public CmsRecipientEnumerator GetEnumerator()
		{
			return new CmsRecipientEnumerator(this);
		}

		/// <summary>The <see cref="M:System.Security.Cryptography.Pkcs.CmsRecipientCollection.System#Collections#IEnumerable#GetEnumerator" /> method returns a <see cref="T:System.Security.Cryptography.Pkcs.CmsRecipientEnumerator" /> object for the <see cref="T:System.Security.Cryptography.Pkcs.CmsRecipientCollection" /> collection.</summary>
		/// <returns>A <see cref="T:System.Security.Cryptography.Pkcs.CmsRecipientEnumerator" /> object that can be used to enumerate the <see cref="T:System.Security.Cryptography.Pkcs.CmsRecipientCollection" /> collection.</returns>
		// Token: 0x0600038A RID: 906 RVA: 0x00010FDF File Offset: 0x0000F1DF
		IEnumerator IEnumerable.GetEnumerator()
		{
			return new CmsRecipientEnumerator(this);
		}

		/// <summary>The <see cref="M:System.Security.Cryptography.Pkcs.CmsRecipientCollection.CopyTo(System.Array,System.Int32)" /> method copies the <see cref="T:System.Security.Cryptography.Pkcs.CmsRecipientCollection" /> collection to an array.</summary>
		/// <param name="array">An <see cref="T:System.Array" /> object to which the <see cref="T:System.Security.Cryptography.Pkcs.CmsRecipientCollection" /> collection is to be copied.</param>
		/// <param name="index">The zero-based index in <paramref name="array" /> where the <see cref="T:System.Security.Cryptography.Pkcs.CmsRecipientCollection" /> collection is copied.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="array" /> is not large enough to hold the specified elements.  
		///
		/// -or-  
		///
		/// <paramref name="array" /> does not contain the proper number of dimensions.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is outside the range of elements in <paramref name="array" />.</exception>
		// Token: 0x0600038B RID: 907 RVA: 0x00010FE8 File Offset: 0x0000F1E8
		public void CopyTo(Array array, int index)
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

		/// <summary>The <see cref="M:System.Security.Cryptography.Pkcs.CmsRecipientCollection.CopyTo(System.Security.Cryptography.Pkcs.CmsRecipient[],System.Int32)" /> method copies the <see cref="T:System.Security.Cryptography.Pkcs.CmsRecipientCollection" /> collection to a <see cref="T:System.Security.Cryptography.Pkcs.CmsRecipient" /> array.</summary>
		/// <param name="array">An array of <see cref="T:System.Security.Cryptography.Pkcs.CmsRecipient" /> objects where the <see cref="T:System.Security.Cryptography.Pkcs.CmsRecipientCollection" /> collection is to be copied.</param>
		/// <param name="index">The zero-based index for the array of <see cref="T:System.Security.Cryptography.Pkcs.CmsRecipient" /> objects in <paramref name="array" /> to which the <see cref="T:System.Security.Cryptography.Pkcs.CmsRecipientCollection" /> collection is copied.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="array" /> is not large enough to hold the specified elements.  
		///
		/// -or-  
		///
		/// <paramref name="array" /> does not contain the proper number of dimensions.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is outside the range of elements in <paramref name="array" />.</exception>
		// Token: 0x0600038C RID: 908 RVA: 0x00011074 File Offset: 0x0000F274
		public void CopyTo(CmsRecipient[] array, int index)
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
			this._recipients.CopyTo(array, index);
		}

		/// <summary>The <see cref="P:System.Security.Cryptography.Pkcs.CmsRecipientCollection.IsSynchronized" /> property retrieves whether access to the collection is synchronized, or thread safe. This property always returns <see langword="false" />, which means that the collection is not thread safe.</summary>
		/// <returns>A <see cref="T:System.Boolean" /> value of <see langword="false" />, which means that the collection is not thread safe.</returns>
		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x0600038D RID: 909 RVA: 0x00002856 File Offset: 0x00000A56
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		/// <summary>The <see cref="P:System.Security.Cryptography.Pkcs.CmsRecipientCollection.SyncRoot" /> property retrieves an <see cref="T:System.Object" /> object used to synchronize access to the <see cref="T:System.Security.Cryptography.Pkcs.CmsRecipientCollection" /> collection.</summary>
		/// <returns>An <see cref="T:System.Object" /> object that is used to synchronize access to the <see cref="T:System.Security.Cryptography.Pkcs.CmsRecipientCollection" /> collection.</returns>
		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x0600038E RID: 910 RVA: 0x00002859 File Offset: 0x00000A59
		public object SyncRoot
		{
			get
			{
				return this;
			}
		}

		// Token: 0x04000266 RID: 614
		private readonly List<CmsRecipient> _recipients;
	}
}

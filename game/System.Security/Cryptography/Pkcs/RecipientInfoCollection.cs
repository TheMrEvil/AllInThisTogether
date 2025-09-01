using System;
using System.Collections;
using System.Collections.Generic;

namespace System.Security.Cryptography.Pkcs
{
	/// <summary>The <see cref="T:System.Security.Cryptography.Pkcs.RecipientInfoCollection" /> class represents a collection of <see cref="T:System.Security.Cryptography.Pkcs.RecipientInfo" /> objects. <see cref="T:System.Security.Cryptography.Pkcs.RecipientInfoCollection" /> implements the <see cref="T:System.Collections.ICollection" /> interface.</summary>
	// Token: 0x02000081 RID: 129
	public sealed class RecipientInfoCollection : ICollection, IEnumerable
	{
		// Token: 0x0600042B RID: 1067 RVA: 0x00012D0E File Offset: 0x00010F0E
		internal RecipientInfoCollection()
		{
			this._recipientInfos = Array.Empty<RecipientInfo>();
		}

		// Token: 0x0600042C RID: 1068 RVA: 0x00012D21 File Offset: 0x00010F21
		internal RecipientInfoCollection(RecipientInfo recipientInfo)
		{
			this._recipientInfos = new RecipientInfo[]
			{
				recipientInfo
			};
		}

		// Token: 0x0600042D RID: 1069 RVA: 0x00012D39 File Offset: 0x00010F39
		internal RecipientInfoCollection(ICollection<RecipientInfo> recipientInfos)
		{
			this._recipientInfos = new RecipientInfo[recipientInfos.Count];
			recipientInfos.CopyTo(this._recipientInfos, 0);
		}

		/// <summary>The <see cref="P:System.Security.Cryptography.Pkcs.RecipientInfoCollection.Item(System.Int32)" /> property retrieves the <see cref="T:System.Security.Cryptography.Pkcs.RecipientInfo" /> object at the specified index in the collection.</summary>
		/// <param name="index">An int value that represents the index in the collection. The index is zero based.</param>
		/// <returns>A <see cref="T:System.Security.Cryptography.Pkcs.RecipientInfo" /> object at the specified index.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value of an argument was outside the allowable range of values as defined by the called method.</exception>
		// Token: 0x170000EF RID: 239
		public RecipientInfo this[int index]
		{
			get
			{
				if (index < 0 || index >= this._recipientInfos.Length)
				{
					throw new ArgumentOutOfRangeException("index", "Index was out of range. Must be non-negative and less than the size of the collection.");
				}
				return this._recipientInfos[index];
			}
		}

		/// <summary>The <see cref="P:System.Security.Cryptography.Pkcs.RecipientInfoCollection.Count" /> property retrieves the number of items in the <see cref="T:System.Security.Cryptography.Pkcs.RecipientInfoCollection" /> collection.</summary>
		/// <returns>An int value that represents the number of items in the collection.</returns>
		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x0600042F RID: 1071 RVA: 0x00012D88 File Offset: 0x00010F88
		public int Count
		{
			get
			{
				return this._recipientInfos.Length;
			}
		}

		/// <summary>The <see cref="M:System.Security.Cryptography.Pkcs.RecipientInfoCollection.GetEnumerator" /> method returns a <see cref="T:System.Security.Cryptography.Pkcs.RecipientInfoEnumerator" /> object for the <see cref="T:System.Security.Cryptography.Pkcs.RecipientInfoCollection" /> collection.</summary>
		/// <returns>A <see cref="T:System.Security.Cryptography.Pkcs.RecipientInfoEnumerator" /> object that can be used to enumerate the <see cref="T:System.Security.Cryptography.Pkcs.RecipientInfoCollection" /> collection.</returns>
		// Token: 0x06000430 RID: 1072 RVA: 0x00012D92 File Offset: 0x00010F92
		public RecipientInfoEnumerator GetEnumerator()
		{
			return new RecipientInfoEnumerator(this);
		}

		/// <summary>The <see cref="M:System.Security.Cryptography.Pkcs.RecipientInfoCollection.System#Collections#IEnumerable#GetEnumerator" /> method returns a <see cref="T:System.Security.Cryptography.Pkcs.RecipientInfoEnumerator" /> object for the <see cref="T:System.Security.Cryptography.Pkcs.RecipientInfoCollection" /> collection.</summary>
		/// <returns>A <see cref="T:System.Security.Cryptography.Pkcs.RecipientInfoEnumerator" /> object that can be used to enumerate the <see cref="T:System.Security.Cryptography.Pkcs.RecipientInfoCollection" /> collection.</returns>
		// Token: 0x06000431 RID: 1073 RVA: 0x00012D9A File Offset: 0x00010F9A
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		/// <summary>The <see cref="M:System.Security.Cryptography.Pkcs.RecipientInfoCollection.CopyTo(System.Array,System.Int32)" /> method copies the <see cref="T:System.Security.Cryptography.Pkcs.RecipientInfoCollection" /> collection to an array.</summary>
		/// <param name="array">An <see cref="T:System.Array" /> object to which  the <see cref="T:System.Security.Cryptography.Pkcs.RecipientInfoCollection" /> collection is to be copied.</param>
		/// <param name="index">The zero-based index in <paramref name="array" /> where the <see cref="T:System.Security.Cryptography.Pkcs.RecipientInfoCollection" /> collection is copied.</param>
		/// <exception cref="T:System.ArgumentException">One of the arguments provided to a method was not valid.</exception>
		/// <exception cref="T:System.ArgumentNullException">A null reference was passed to a method that does not accept it as a valid argument.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value of an argument was outside the allowable range of values as defined by the called method.</exception>
		// Token: 0x06000432 RID: 1074 RVA: 0x00012DA4 File Offset: 0x00010FA4
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

		/// <summary>The <see cref="M:System.Security.Cryptography.Pkcs.RecipientInfoCollection.CopyTo(System.Security.Cryptography.Pkcs.RecipientInfo[],System.Int32)" /> method copies the <see cref="T:System.Security.Cryptography.Pkcs.RecipientInfoCollection" /> collection to a <see cref="T:System.Security.Cryptography.Pkcs.RecipientInfo" /> array.</summary>
		/// <param name="array">An array of <see cref="T:System.Security.Cryptography.Pkcs.RecipientInfo" /> objects where the <see cref="T:System.Security.Cryptography.Pkcs.RecipientInfoCollection" /> collection is to be copied.</param>
		/// <param name="index">The zero-based index in <paramref name="array" /> where the <see cref="T:System.Security.Cryptography.Pkcs.RecipientInfoCollection" /> collection is copied.</param>
		/// <exception cref="T:System.ArgumentException">One of the arguments provided to a method was not valid.</exception>
		/// <exception cref="T:System.ArgumentNullException">A null reference was passed to a method that does not accept it as a valid argument.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value of an argument was outside the allowable range of values as defined by the called method.</exception>
		// Token: 0x06000433 RID: 1075 RVA: 0x00012E2F File Offset: 0x0001102F
		public void CopyTo(RecipientInfo[] array, int index)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (index < 0 || index >= array.Length)
			{
				throw new ArgumentOutOfRangeException("index", "Index was out of range. Must be non-negative and less than the size of the collection.");
			}
			this._recipientInfos.CopyTo(array, index);
		}

		/// <summary>The <see cref="P:System.Security.Cryptography.Pkcs.RecipientInfoCollection.IsSynchronized" /> property retrieves whether access to the collection is synchronized, or thread safe. This property always returns <see langword="false" />, which means the collection is not thread safe.</summary>
		/// <returns>A <see cref="T:System.Boolean" /> value of <see langword="false" />, which means the collection is not thread safe.</returns>
		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x06000434 RID: 1076 RVA: 0x00002856 File Offset: 0x00000A56
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		/// <summary>The <see cref="P:System.Security.Cryptography.Pkcs.RecipientInfoCollection.SyncRoot" /> property retrieves an <see cref="T:System.Object" /> object used to synchronize access to the <see cref="T:System.Security.Cryptography.Pkcs.RecipientInfoCollection" /> collection.</summary>
		/// <returns>An <see cref="T:System.Object" /> object used to synchronize access to the <see cref="T:System.Security.Cryptography.Pkcs.RecipientInfoCollection" /> collection.</returns>
		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x06000435 RID: 1077 RVA: 0x00002859 File Offset: 0x00000A59
		public object SyncRoot
		{
			get
			{
				return this;
			}
		}

		// Token: 0x0400029B RID: 667
		private readonly RecipientInfo[] _recipientInfos;
	}
}

using System;
using System.Collections;

namespace System.Security.Cryptography.X509Certificates
{
	/// <summary>Defines a collection that stores <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" /> objects.</summary>
	// Token: 0x020002D6 RID: 726
	[Serializable]
	public class X509CertificateCollection : CollectionBase
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.X509Certificates.X509CertificateCollection" /> class.</summary>
		// Token: 0x060016D0 RID: 5840 RVA: 0x0004E358 File Offset: 0x0004C558
		public X509CertificateCollection()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.X509Certificates.X509CertificateCollection" /> class from an array of <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" /> objects.</summary>
		/// <param name="value">The array of <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" /> objects with which to initialize the new object.</param>
		// Token: 0x060016D1 RID: 5841 RVA: 0x0005AF2C File Offset: 0x0005912C
		public X509CertificateCollection(X509Certificate[] value)
		{
			this.AddRange(value);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.X509Certificates.X509CertificateCollection" /> class from another <see cref="T:System.Security.Cryptography.X509Certificates.X509CertificateCollection" />.</summary>
		/// <param name="value">The <see cref="T:System.Security.Cryptography.X509Certificates.X509CertificateCollection" /> with which to initialize the new object.</param>
		// Token: 0x060016D2 RID: 5842 RVA: 0x0005AF3B File Offset: 0x0005913B
		public X509CertificateCollection(X509CertificateCollection value)
		{
			this.AddRange(value);
		}

		/// <summary>Gets or sets the entry at the specified index of the current <see cref="T:System.Security.Cryptography.X509Certificates.X509CertificateCollection" />.</summary>
		/// <param name="index">The zero-based index of the entry to locate in the current <see cref="T:System.Security.Cryptography.X509Certificates.X509CertificateCollection" />.</param>
		/// <returns>The <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" /> at the specified index of the current <see cref="T:System.Security.Cryptography.X509Certificates.X509CertificateCollection" />.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="index" /> parameter is outside the valid range of indexes for the collection.</exception>
		// Token: 0x17000461 RID: 1121
		public X509Certificate this[int index]
		{
			get
			{
				return (X509Certificate)base.InnerList[index];
			}
			set
			{
				base.InnerList[index] = value;
			}
		}

		/// <summary>Adds an <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" /> with the specified value to the current <see cref="T:System.Security.Cryptography.X509Certificates.X509CertificateCollection" />.</summary>
		/// <param name="value">The <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" /> to add to the current <see cref="T:System.Security.Cryptography.X509Certificates.X509CertificateCollection" />.</param>
		/// <returns>The index into the current <see cref="T:System.Security.Cryptography.X509Certificates.X509CertificateCollection" /> at which the new <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" /> was inserted.</returns>
		// Token: 0x060016D5 RID: 5845 RVA: 0x0005AF5D File Offset: 0x0005915D
		public int Add(X509Certificate value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			return base.InnerList.Add(value);
		}

		/// <summary>Copies the elements of an array of type <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" /> to the end of the current <see cref="T:System.Security.Cryptography.X509Certificates.X509CertificateCollection" />.</summary>
		/// <param name="value">The array of type <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" /> containing the objects to add to the current <see cref="T:System.Security.Cryptography.X509Certificates.X509CertificateCollection" />.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="value" /> parameter is <see langword="null" />.</exception>
		// Token: 0x060016D6 RID: 5846 RVA: 0x0005AF7C File Offset: 0x0005917C
		public void AddRange(X509Certificate[] value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			for (int i = 0; i < value.Length; i++)
			{
				base.InnerList.Add(value[i]);
			}
		}

		/// <summary>Copies the elements of the specified <see cref="T:System.Security.Cryptography.X509Certificates.X509CertificateCollection" /> to the end of the current <see cref="T:System.Security.Cryptography.X509Certificates.X509CertificateCollection" />.</summary>
		/// <param name="value">The <see cref="T:System.Security.Cryptography.X509Certificates.X509CertificateCollection" /> containing the objects to add to the collection.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="value" /> parameter is <see langword="null" />.</exception>
		// Token: 0x060016D7 RID: 5847 RVA: 0x0005AFB4 File Offset: 0x000591B4
		public void AddRange(X509CertificateCollection value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			for (int i = 0; i < value.InnerList.Count; i++)
			{
				base.InnerList.Add(value[i]);
			}
		}

		/// <summary>Gets a value indicating whether the current <see cref="T:System.Security.Cryptography.X509Certificates.X509CertificateCollection" /> contains the specified <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" />.</summary>
		/// <param name="value">The <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" /> to locate.</param>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" /> is contained in this collection; otherwise, <see langword="false" />.</returns>
		// Token: 0x060016D8 RID: 5848 RVA: 0x0005AFF8 File Offset: 0x000591F8
		public bool Contains(X509Certificate value)
		{
			if (value == null)
			{
				return false;
			}
			byte[] certHash = value.GetCertHash();
			for (int i = 0; i < base.InnerList.Count; i++)
			{
				X509Certificate x509Certificate = (X509Certificate)base.InnerList[i];
				if (this.Compare(x509Certificate.GetCertHash(), certHash))
				{
					return true;
				}
			}
			return false;
		}

		/// <summary>Copies the <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" /> values in the current <see cref="T:System.Security.Cryptography.X509Certificates.X509CertificateCollection" /> to a one-dimensional <see cref="T:System.Array" /> instance at the specified index.</summary>
		/// <param name="array">The one-dimensional <see cref="T:System.Array" /> that is the destination of the values copied from <see cref="T:System.Security.Cryptography.X509Certificates.X509CertificateCollection" />.</param>
		/// <param name="index">The index into <paramref name="array" /> to begin copying.</param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="array" /> parameter is multidimensional.  
		///  -or-  
		///  The number of elements in the <see cref="T:System.Security.Cryptography.X509Certificates.X509CertificateCollection" /> is greater than the available space between <paramref name="arrayIndex" /> and the end of <paramref name="array" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="array" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="arrayIndex" /> parameter is less than the <paramref name="array" /> parameter's lower bound.</exception>
		// Token: 0x060016D9 RID: 5849 RVA: 0x0004E442 File Offset: 0x0004C642
		public void CopyTo(X509Certificate[] array, int index)
		{
			base.InnerList.CopyTo(array, index);
		}

		/// <summary>Returns an enumerator that can iterate through the <see cref="T:System.Security.Cryptography.X509Certificates.X509CertificateCollection" />.</summary>
		/// <returns>An enumerator of the subelements of <see cref="T:System.Security.Cryptography.X509Certificates.X509CertificateCollection" /> you can use to iterate through the collection.</returns>
		// Token: 0x060016DA RID: 5850 RVA: 0x0005B04B File Offset: 0x0005924B
		public new X509CertificateCollection.X509CertificateEnumerator GetEnumerator()
		{
			return new X509CertificateCollection.X509CertificateEnumerator(this);
		}

		/// <summary>Builds a hash value based on all values contained in the current <see cref="T:System.Security.Cryptography.X509Certificates.X509CertificateCollection" />.</summary>
		/// <returns>A hash value based on all values contained in the current <see cref="T:System.Security.Cryptography.X509Certificates.X509CertificateCollection" />.</returns>
		// Token: 0x060016DB RID: 5851 RVA: 0x0005B053 File Offset: 0x00059253
		public override int GetHashCode()
		{
			return base.InnerList.GetHashCode();
		}

		/// <summary>Returns the index of the specified <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" /> in the current <see cref="T:System.Security.Cryptography.X509Certificates.X509CertificateCollection" />.</summary>
		/// <param name="value">The <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" /> to locate.</param>
		/// <returns>The index of the <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" /> specified by the <paramref name="value" /> parameter in the <see cref="T:System.Security.Cryptography.X509Certificates.X509CertificateCollection" />, if found; otherwise, -1.</returns>
		// Token: 0x060016DC RID: 5852 RVA: 0x0004E451 File Offset: 0x0004C651
		public int IndexOf(X509Certificate value)
		{
			return base.InnerList.IndexOf(value);
		}

		/// <summary>Inserts a <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" /> into the current <see cref="T:System.Security.Cryptography.X509Certificates.X509CertificateCollection" /> at the specified index.</summary>
		/// <param name="index">The zero-based index where <paramref name="value" /> should be inserted.</param>
		/// <param name="value">The <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" /> to insert.</param>
		// Token: 0x060016DD RID: 5853 RVA: 0x0004E45F File Offset: 0x0004C65F
		public void Insert(int index, X509Certificate value)
		{
			base.InnerList.Insert(index, value);
		}

		/// <summary>Removes a specific <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" /> from the current <see cref="T:System.Security.Cryptography.X509Certificates.X509CertificateCollection" />.</summary>
		/// <param name="value">The <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" /> to remove from the current <see cref="T:System.Security.Cryptography.X509Certificates.X509CertificateCollection" />.</param>
		/// <exception cref="T:System.ArgumentException">The <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" /> specified by the <paramref name="value" /> parameter is not found in the current <see cref="T:System.Security.Cryptography.X509Certificates.X509CertificateCollection" />.</exception>
		// Token: 0x060016DE RID: 5854 RVA: 0x0005B060 File Offset: 0x00059260
		public void Remove(X509Certificate value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (this.IndexOf(value) == -1)
			{
				throw new ArgumentException("value", Locale.GetText("Not part of the collection."));
			}
			base.InnerList.Remove(value);
		}

		// Token: 0x060016DF RID: 5855 RVA: 0x0005B09C File Offset: 0x0005929C
		private bool Compare(byte[] array1, byte[] array2)
		{
			if (array1 == null && array2 == null)
			{
				return true;
			}
			if (array1 == null || array2 == null)
			{
				return false;
			}
			if (array1.Length != array2.Length)
			{
				return false;
			}
			for (int i = 0; i < array1.Length; i++)
			{
				if (array1[i] != array2[i])
				{
					return false;
				}
			}
			return true;
		}

		/// <summary>Enumerates the <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" /> objects in an <see cref="T:System.Security.Cryptography.X509Certificates.X509CertificateCollection" />.</summary>
		// Token: 0x020002D7 RID: 727
		public class X509CertificateEnumerator : IEnumerator
		{
			/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.X509Certificates.X509CertificateCollection.X509CertificateEnumerator" /> class for the specified <see cref="T:System.Security.Cryptography.X509Certificates.X509CertificateCollection" />.</summary>
			/// <param name="mappings">The <see cref="T:System.Security.Cryptography.X509Certificates.X509CertificateCollection" /> to enumerate.</param>
			// Token: 0x060016E0 RID: 5856 RVA: 0x0005B0DC File Offset: 0x000592DC
			public X509CertificateEnumerator(X509CertificateCollection mappings)
			{
				this.enumerator = ((IEnumerable)mappings).GetEnumerator();
			}

			/// <summary>Gets the current <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" /> in the <see cref="T:System.Security.Cryptography.X509Certificates.X509CertificateCollection" />.</summary>
			/// <returns>The current <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" /> in the <see cref="T:System.Security.Cryptography.X509Certificates.X509CertificateCollection" />.</returns>
			/// <exception cref="T:System.InvalidOperationException">The enumerator is positioned before the first element of the collection or after the last element.</exception>
			// Token: 0x17000462 RID: 1122
			// (get) Token: 0x060016E1 RID: 5857 RVA: 0x0005B0F0 File Offset: 0x000592F0
			public X509Certificate Current
			{
				get
				{
					return (X509Certificate)this.enumerator.Current;
				}
			}

			/// <summary>For a description of this member, see <see cref="P:System.Collections.IEnumerator.Current" />.</summary>
			/// <returns>The current X.509 certificate object in the <see cref="T:System.Security.Cryptography.X509Certificates.X509CertificateCollection" /> object.</returns>
			/// <exception cref="T:System.InvalidOperationException">The enumerator is positioned before the first element of the collection or after the last element.</exception>
			// Token: 0x17000463 RID: 1123
			// (get) Token: 0x060016E2 RID: 5858 RVA: 0x0005B102 File Offset: 0x00059302
			object IEnumerator.Current
			{
				get
				{
					return this.enumerator.Current;
				}
			}

			/// <summary>For a description of this member, see <see cref="M:System.Collections.IEnumerator.MoveNext" />.</summary>
			/// <returns>
			///   <see langword="true" /> if the enumerator was successfully advanced to the next element; <see langword="false" /> if the enumerator has passed the end of the collection.</returns>
			/// <exception cref="T:System.InvalidOperationException">The collection was modified after the enumerator was instantiated.</exception>
			// Token: 0x060016E3 RID: 5859 RVA: 0x0005B10F File Offset: 0x0005930F
			bool IEnumerator.MoveNext()
			{
				return this.enumerator.MoveNext();
			}

			/// <summary>For a description of this member, see <see cref="M:System.Collections.IEnumerator.Reset" />.</summary>
			/// <exception cref="T:System.InvalidOperationException">The collection was modified after the enumerator was instantiated.</exception>
			// Token: 0x060016E4 RID: 5860 RVA: 0x0005B11C File Offset: 0x0005931C
			void IEnumerator.Reset()
			{
				this.enumerator.Reset();
			}

			/// <summary>Advances the enumerator to the next element of the collection.</summary>
			/// <returns>
			///   <see langword="true" /> if the enumerator was successfully advanced to the next element; <see langword="false" /> if the enumerator has passed the end of the collection.</returns>
			/// <exception cref="T:System.InvalidOperationException">The collection was modified after the enumerator was instantiated.</exception>
			// Token: 0x060016E5 RID: 5861 RVA: 0x0005B10F File Offset: 0x0005930F
			public bool MoveNext()
			{
				return this.enumerator.MoveNext();
			}

			/// <summary>Sets the enumerator to its initial position, which is before the first element in the collection.</summary>
			/// <exception cref="T:System.InvalidOperationException">The collection is modified after the enumerator is instantiated.</exception>
			// Token: 0x060016E6 RID: 5862 RVA: 0x0005B11C File Offset: 0x0005931C
			public void Reset()
			{
				this.enumerator.Reset();
			}

			// Token: 0x04000D00 RID: 3328
			private IEnumerator enumerator;
		}
	}
}

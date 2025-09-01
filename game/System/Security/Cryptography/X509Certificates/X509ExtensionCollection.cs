using System;
using System.Collections;
using Mono.Security;
using Mono.Security.X509;

namespace System.Security.Cryptography.X509Certificates
{
	/// <summary>Represents a collection of <see cref="T:System.Security.Cryptography.X509Certificates.X509Extension" /> objects. This class cannot be inherited.</summary>
	// Token: 0x020002E3 RID: 739
	public sealed class X509ExtensionCollection : ICollection, IEnumerable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.X509Certificates.X509ExtensionCollection" /> class.</summary>
		// Token: 0x0600177E RID: 6014 RVA: 0x0005D274 File Offset: 0x0005B474
		public X509ExtensionCollection()
		{
			this._list = new ArrayList();
		}

		// Token: 0x0600177F RID: 6015 RVA: 0x0005D288 File Offset: 0x0005B488
		internal X509ExtensionCollection(X509Certificate cert)
		{
			this._list = new ArrayList(cert.Extensions.Count);
			if (cert.Extensions.Count == 0)
			{
				return;
			}
			foreach (object obj in cert.Extensions)
			{
				X509Extension x509Extension = (X509Extension)obj;
				bool critical = x509Extension.Critical;
				string oid = x509Extension.Oid;
				byte[] array = null;
				ASN1 value = x509Extension.Value;
				if (value.Tag == 4 && value.Count > 0)
				{
					array = value[0].GetBytes();
				}
				X509Extension x509Extension2 = (X509Extension)CryptoConfig.CreateFromName(oid, new object[]
				{
					new AsnEncodedData(oid, array ?? X509ExtensionCollection.Empty),
					critical
				});
				if (x509Extension2 == null)
				{
					x509Extension2 = new X509Extension(oid, array ?? X509ExtensionCollection.Empty, critical);
				}
				this._list.Add(x509Extension2);
			}
		}

		/// <summary>Gets the number of <see cref="T:System.Security.Cryptography.X509Certificates.X509Extension" /> objects in a <see cref="T:System.Security.Cryptography.X509Certificates.X509ExtensionCollection" /> object.</summary>
		/// <returns>An integer representing the number of <see cref="T:System.Security.Cryptography.X509Certificates.X509Extension" /> objects in the <see cref="T:System.Security.Cryptography.X509Certificates.X509ExtensionCollection" /> object.</returns>
		// Token: 0x17000494 RID: 1172
		// (get) Token: 0x06001780 RID: 6016 RVA: 0x0005D3A4 File Offset: 0x0005B5A4
		public int Count
		{
			get
			{
				return this._list.Count;
			}
		}

		/// <summary>Gets a value indicating whether the collection is guaranteed to be thread safe.</summary>
		/// <returns>
		///   <see langword="true" /> if the collection is thread safe; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000495 RID: 1173
		// (get) Token: 0x06001781 RID: 6017 RVA: 0x0005D3B1 File Offset: 0x0005B5B1
		public bool IsSynchronized
		{
			get
			{
				return this._list.IsSynchronized;
			}
		}

		/// <summary>Gets an object that you can use to synchronize access to the <see cref="T:System.Security.Cryptography.X509Certificates.X509ExtensionCollection" /> object.</summary>
		/// <returns>An object that you can use to synchronize access to the <see cref="T:System.Security.Cryptography.X509Certificates.X509ExtensionCollection" /> object.</returns>
		// Token: 0x17000496 RID: 1174
		// (get) Token: 0x06001782 RID: 6018 RVA: 0x000075E1 File Offset: 0x000057E1
		public object SyncRoot
		{
			get
			{
				return this;
			}
		}

		/// <summary>Gets the <see cref="T:System.Security.Cryptography.X509Certificates.X509Extension" /> object at the specified index.</summary>
		/// <param name="index">The location of the <see cref="T:System.Security.Cryptography.X509Certificates.X509Extension" /> object to retrieve.</param>
		/// <returns>An <see cref="T:System.Security.Cryptography.X509Certificates.X509Extension" /> object.</returns>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="index" /> is less than zero.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is equal to or greater than the length of the array.</exception>
		// Token: 0x17000497 RID: 1175
		public X509Extension this[int index]
		{
			get
			{
				if (index < 0)
				{
					throw new InvalidOperationException("index");
				}
				return (X509Extension)this._list[index];
			}
		}

		/// <summary>Gets the first <see cref="T:System.Security.Cryptography.X509Certificates.X509Extension" /> object whose value or friendly name is specified by an object identifier (OID).</summary>
		/// <param name="oid">The object identifier (OID) of the extension to retrieve.</param>
		/// <returns>An <see cref="T:System.Security.Cryptography.X509Certificates.X509Extension" /> object.</returns>
		// Token: 0x17000498 RID: 1176
		public X509Extension this[string oid]
		{
			get
			{
				if (oid == null)
				{
					throw new ArgumentNullException("oid");
				}
				if (this._list.Count == 0 || oid.Length == 0)
				{
					return null;
				}
				foreach (object obj in this._list)
				{
					X509Extension x509Extension = (X509Extension)obj;
					if (x509Extension.Oid.Value.Equals(oid))
					{
						return x509Extension;
					}
				}
				return null;
			}
		}

		/// <summary>Adds an <see cref="T:System.Security.Cryptography.X509Certificates.X509Extension" /> object to an <see cref="T:System.Security.Cryptography.X509Certificates.X509ExtensionCollection" /> object.</summary>
		/// <param name="extension">An <see cref="T:System.Security.Cryptography.X509Certificates.X509Extension" /> object to add to the <see cref="T:System.Security.Cryptography.X509Certificates.X509ExtensionCollection" /> object.</param>
		/// <returns>The index at which the <paramref name="extension" /> parameter was added.</returns>
		/// <exception cref="T:System.ArgumentNullException">The value of the <paramref name="extension" /> parameter is <see langword="null" />.</exception>
		// Token: 0x06001785 RID: 6021 RVA: 0x0005D474 File Offset: 0x0005B674
		public int Add(X509Extension extension)
		{
			if (extension == null)
			{
				throw new ArgumentNullException("extension");
			}
			return this._list.Add(extension);
		}

		/// <summary>Copies a collection into an array starting at the specified index.</summary>
		/// <param name="array">An array of <see cref="T:System.Security.Cryptography.X509Certificates.X509Extension" /> objects.</param>
		/// <param name="index">The location in the array at which copying starts.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="index" /> is a zero-length string or contains an invalid value.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="index" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> specifies a value that is not in the range of the array.</exception>
		// Token: 0x06001786 RID: 6022 RVA: 0x0005D490 File Offset: 0x0005B690
		public void CopyTo(X509Extension[] array, int index)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("negative index");
			}
			if (index >= array.Length)
			{
				throw new ArgumentOutOfRangeException("index >= array.Length");
			}
			this._list.CopyTo(array, index);
		}

		/// <summary>Copies the collection into an array starting at the specified index.</summary>
		/// <param name="array">An array of <see cref="T:System.Security.Cryptography.X509Certificates.X509Extension" /> objects.</param>
		/// <param name="index">The location in the array at which copying starts.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="index" /> is a zero-length string or contains an invalid value.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="index" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> specifies a value that is not in the range of the array.</exception>
		// Token: 0x06001787 RID: 6023 RVA: 0x0005D4CD File Offset: 0x0005B6CD
		void ICollection.CopyTo(Array array, int index)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("negative index");
			}
			if (index >= array.Length)
			{
				throw new ArgumentOutOfRangeException("index >= array.Length");
			}
			this._list.CopyTo(array, index);
		}

		/// <summary>Returns an enumerator that can iterate through an <see cref="T:System.Security.Cryptography.X509Certificates.X509ExtensionCollection" /> object.</summary>
		/// <returns>An <see cref="T:System.Security.Cryptography.X509Certificates.X509ExtensionEnumerator" /> object to use to iterate through the <see cref="T:System.Security.Cryptography.X509Certificates.X509ExtensionCollection" /> object.</returns>
		// Token: 0x06001788 RID: 6024 RVA: 0x0005D50D File Offset: 0x0005B70D
		public X509ExtensionEnumerator GetEnumerator()
		{
			return new X509ExtensionEnumerator(this._list);
		}

		/// <summary>Returns an enumerator that can iterate through an <see cref="T:System.Security.Cryptography.X509Certificates.X509ExtensionCollection" /> object.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> object to use to iterate through the <see cref="T:System.Security.Cryptography.X509Certificates.X509ExtensionCollection" /> object.</returns>
		// Token: 0x06001789 RID: 6025 RVA: 0x0005D50D File Offset: 0x0005B70D
		IEnumerator IEnumerable.GetEnumerator()
		{
			return new X509ExtensionEnumerator(this._list);
		}

		// Token: 0x0600178A RID: 6026 RVA: 0x0005D51A File Offset: 0x0005B71A
		// Note: this type is marked as 'beforefieldinit'.
		static X509ExtensionCollection()
		{
		}

		// Token: 0x04000D29 RID: 3369
		private static byte[] Empty = new byte[0];

		// Token: 0x04000D2A RID: 3370
		private ArrayList _list;
	}
}

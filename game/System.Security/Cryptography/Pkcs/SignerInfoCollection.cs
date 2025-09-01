using System;
using System.Collections;
using System.Security.Cryptography.Pkcs.Asn1;
using System.Security.Cryptography.Xml;

namespace System.Security.Cryptography.Pkcs
{
	/// <summary>The <see cref="T:System.Security.Cryptography.Pkcs.SignerInfoCollection" /> class represents a collection of <see cref="T:System.Security.Cryptography.Pkcs.SignerInfo" /> objects. <see cref="T:System.Security.Cryptography.Pkcs.SignerInfoCollection" /> implements the <see cref="T:System.Collections.ICollection" /> interface.</summary>
	// Token: 0x02000087 RID: 135
	public sealed class SignerInfoCollection : ICollection, IEnumerable
	{
		// Token: 0x06000480 RID: 1152 RVA: 0x000145B0 File Offset: 0x000127B0
		internal SignerInfoCollection()
		{
			this._signerInfos = Array.Empty<SignerInfo>();
		}

		// Token: 0x06000481 RID: 1153 RVA: 0x000145C3 File Offset: 0x000127C3
		internal SignerInfoCollection(SignerInfo[] signerInfos)
		{
			this._signerInfos = signerInfos;
		}

		// Token: 0x06000482 RID: 1154 RVA: 0x000145D4 File Offset: 0x000127D4
		internal SignerInfoCollection(SignerInfoAsn[] signedDataSignerInfos, SignedCms ownerDocument)
		{
			this._signerInfos = new SignerInfo[signedDataSignerInfos.Length];
			for (int i = 0; i < signedDataSignerInfos.Length; i++)
			{
				this._signerInfos[i] = new SignerInfo(ref signedDataSignerInfos[i], ownerDocument);
			}
		}

		/// <summary>The <see cref="P:System.Security.Cryptography.Pkcs.SignerInfoCollection.Item(System.Int32)" /> property retrieves the <see cref="T:System.Security.Cryptography.Pkcs.SignerInfo" /> object at the specified index in the collection.</summary>
		/// <param name="index">An int value that represents the index in the collection. The index is zero based.</param>
		/// <returns>A <see cref="T:System.Security.Cryptography.Pkcs.SignerInfo" /> object  at the specified index.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value of an argument was outside the allowable range of values as defined by the called method.</exception>
		// Token: 0x17000102 RID: 258
		public SignerInfo this[int index]
		{
			get
			{
				if (index < 0 || index >= this._signerInfos.Length)
				{
					throw new ArgumentOutOfRangeException("index");
				}
				return this._signerInfos[index];
			}
		}

		/// <summary>The <see cref="P:System.Security.Cryptography.Pkcs.SignerInfoCollection.Count" /> property retrieves the number of items in the <see cref="T:System.Security.Cryptography.Pkcs.SignerInfoCollection" /> collection.</summary>
		/// <returns>An int value that represents the number of items in the collection.</returns>
		// Token: 0x17000103 RID: 259
		// (get) Token: 0x06000484 RID: 1156 RVA: 0x0001463C File Offset: 0x0001283C
		public int Count
		{
			get
			{
				return this._signerInfos.Length;
			}
		}

		/// <summary>The <see cref="M:System.Security.Cryptography.Pkcs.SignerInfoCollection.GetEnumerator" /> method returns a <see cref="T:System.Security.Cryptography.Pkcs.SignerInfoEnumerator" /> object for the <see cref="T:System.Security.Cryptography.Pkcs.SignerInfoCollection" /> collection.</summary>
		/// <returns>A <see cref="T:System.Security.Cryptography.Pkcs.SignerInfoEnumerator" /> object that can be used to enumerate the <see cref="T:System.Security.Cryptography.Pkcs.SignerInfoCollection" /> collection.</returns>
		// Token: 0x06000485 RID: 1157 RVA: 0x00014646 File Offset: 0x00012846
		public SignerInfoEnumerator GetEnumerator()
		{
			return new SignerInfoEnumerator(this);
		}

		/// <summary>The <see cref="M:System.Security.Cryptography.Pkcs.SignerInfoCollection.System#Collections#IEnumerable#GetEnumerator" /> method returns a <see cref="T:System.Security.Cryptography.Pkcs.SignerInfoEnumerator" /> object for the <see cref="T:System.Security.Cryptography.Pkcs.SignerInfoCollection" /> collection.</summary>
		/// <returns>A <see cref="T:System.Security.Cryptography.Pkcs.SignerInfoEnumerator" /> object that can be used to enumerate the <see cref="T:System.Security.Cryptography.Pkcs.SignerInfoCollection" /> collection.</returns>
		// Token: 0x06000486 RID: 1158 RVA: 0x00014646 File Offset: 0x00012846
		IEnumerator IEnumerable.GetEnumerator()
		{
			return new SignerInfoEnumerator(this);
		}

		/// <summary>The <see cref="M:System.Security.Cryptography.Pkcs.SignerInfoCollection.CopyTo(System.Array,System.Int32)" /> method copies the <see cref="T:System.Security.Cryptography.Pkcs.SignerInfoCollection" /> collection to an array.</summary>
		/// <param name="array">An <see cref="T:System.Array" /> object to which the <see cref="T:System.Security.Cryptography.Pkcs.SignerInfoCollection" /> collection is to be copied.</param>
		/// <param name="index">The zero-based index in <paramref name="array" /> where the <see cref="T:System.Security.Cryptography.Pkcs.SignerInfoCollection" /> collection is copied.</param>
		/// <exception cref="T:System.ArgumentException">One of the arguments provided to a method was not valid.</exception>
		/// <exception cref="T:System.ArgumentNullException">A null reference was passed to a method that does not accept it as a valid argument.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value of an argument was outside the allowable range of values as defined by the called method.</exception>
		// Token: 0x06000487 RID: 1159 RVA: 0x00014650 File Offset: 0x00012850
		public void CopyTo(Array array, int index)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (array.Rank != 1)
			{
				throw new ArgumentException("Only single dimensional arrays are supported for the requested action.", "array");
			}
			if (index < 0 || index >= array.Length)
			{
				throw new ArgumentOutOfRangeException("index", "Index was out of range. Must be non-negative and less than the size of the collection.");
			}
			if (index + this.Count > array.Length)
			{
				throw new ArgumentException("Offset and length were out of bounds for the array or count is greater than the number of elements from index to the end of the source collection.");
			}
			for (int i = 0; i < this.Count; i++)
			{
				array.SetValue(this[i], index + i);
			}
		}

		/// <summary>The <see cref="M:System.Security.Cryptography.Pkcs.SignerInfoCollection.CopyTo(System.Security.Cryptography.Pkcs.SignerInfo[],System.Int32)" /> method copies the <see cref="T:System.Security.Cryptography.Pkcs.SignerInfoCollection" /> collection to a <see cref="T:System.Security.Cryptography.Pkcs.SignerInfo" /> array.</summary>
		/// <param name="array">An array of <see cref="T:System.Security.Cryptography.Pkcs.SignerInfo" /> objects where the <see cref="T:System.Security.Cryptography.Pkcs.SignerInfoCollection" /> collection is to be copied.</param>
		/// <param name="index">The zero-based index in <paramref name="array" /> where the <see cref="T:System.Security.Cryptography.Pkcs.SignerInfoCollection" /> collection is copied.</param>
		/// <exception cref="T:System.ArgumentException">One of the arguments provided to a method was not valid.</exception>
		/// <exception cref="T:System.ArgumentNullException">A null reference was passed to a method that does not accept it as a valid argument.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value of an argument was outside the allowable range of values as defined by the called method.</exception>
		// Token: 0x06000488 RID: 1160 RVA: 0x000146DD File Offset: 0x000128DD
		public void CopyTo(SignerInfo[] array, int index)
		{
			((ICollection)this).CopyTo(array, index);
		}

		/// <summary>The <see cref="P:System.Security.Cryptography.Pkcs.SignerInfoCollection.IsSynchronized" /> property retrieves whether access to the collection is synchronized, or thread safe. This property always returns <see langword="false" />, which means the collection is not thread safe.</summary>
		/// <returns>A <see cref="T:System.Boolean" /> value of <see langword="false" />, which means the collection is not thread safe.</returns>
		// Token: 0x17000104 RID: 260
		// (get) Token: 0x06000489 RID: 1161 RVA: 0x00002856 File Offset: 0x00000A56
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		/// <summary>The <see cref="P:System.Security.Cryptography.Pkcs.SignerInfoCollection.SyncRoot" /> property retrieves an <see cref="T:System.Object" /> object is used to synchronize access to the <see cref="T:System.Security.Cryptography.Pkcs.SignerInfoCollection" /> collection.</summary>
		/// <returns>An <see cref="T:System.Object" /> object is used to synchronize access to the <see cref="T:System.Security.Cryptography.Pkcs.SignerInfoCollection" /> collection.</returns>
		// Token: 0x17000105 RID: 261
		// (get) Token: 0x0600048A RID: 1162 RVA: 0x00002859 File Offset: 0x00000A59
		public object SyncRoot
		{
			get
			{
				return this;
			}
		}

		// Token: 0x0600048B RID: 1163 RVA: 0x000146E8 File Offset: 0x000128E8
		internal int FindIndexForSigner(SignerInfo signer)
		{
			SubjectIdentifier signerIdentifier = signer.SignerIdentifier;
			X509IssuerSerial x509IssuerSerial = default(X509IssuerSerial);
			if (signerIdentifier.Type == SubjectIdentifierType.IssuerAndSerialNumber)
			{
				x509IssuerSerial = (X509IssuerSerial)signerIdentifier.Value;
			}
			for (int i = 0; i < this._signerInfos.Length; i++)
			{
				SubjectIdentifier signerIdentifier2 = this._signerInfos[i].SignerIdentifier;
				if (signerIdentifier2.Type == signerIdentifier.Type)
				{
					bool flag = false;
					switch (signerIdentifier.Type)
					{
					case SubjectIdentifierType.IssuerAndSerialNumber:
					{
						X509IssuerSerial x509IssuerSerial2 = (X509IssuerSerial)signerIdentifier2.Value;
						if (x509IssuerSerial2.IssuerName == x509IssuerSerial.IssuerName && x509IssuerSerial2.SerialNumber == x509IssuerSerial.SerialNumber)
						{
							flag = true;
						}
						break;
					}
					case SubjectIdentifierType.SubjectKeyIdentifier:
						if ((string)signerIdentifier.Value == (string)signerIdentifier2.Value)
						{
							flag = true;
						}
						break;
					case SubjectIdentifierType.NoSignature:
						flag = true;
						break;
					default:
						throw new CryptographicException();
					}
					if (flag)
					{
						return i;
					}
				}
			}
			return -1;
		}

		// Token: 0x040002BC RID: 700
		private readonly SignerInfo[] _signerInfos;
	}
}

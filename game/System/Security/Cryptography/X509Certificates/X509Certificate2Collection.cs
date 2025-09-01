using System;
using System.Collections;
using System.Globalization;

namespace System.Security.Cryptography.X509Certificates
{
	/// <summary>Represents a collection of <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2" /> objects. This class cannot be inherited.</summary>
	// Token: 0x020002D1 RID: 721
	public class X509Certificate2Collection : X509CertificateCollection
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2Collection" /> class without any <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2" /> information.</summary>
		// Token: 0x06001662 RID: 5730 RVA: 0x00059C1B File Offset: 0x00057E1B
		public X509Certificate2Collection()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2Collection" /> class using the specified certificate collection.</summary>
		/// <param name="certificates">An <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2Collection" /> object.</param>
		// Token: 0x06001663 RID: 5731 RVA: 0x00059C23 File Offset: 0x00057E23
		public X509Certificate2Collection(X509Certificate2Collection certificates)
		{
			this.AddRange(certificates);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2Collection" /> class using an <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2" /> object.</summary>
		/// <param name="certificate">An <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2" /> object to start the collection from.</param>
		// Token: 0x06001664 RID: 5732 RVA: 0x00059C32 File Offset: 0x00057E32
		public X509Certificate2Collection(X509Certificate2 certificate)
		{
			this.Add(certificate);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2Collection" /> class using an array of <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2" /> objects.</summary>
		/// <param name="certificates">An array of <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2" /> objects.</param>
		// Token: 0x06001665 RID: 5733 RVA: 0x00059C42 File Offset: 0x00057E42
		public X509Certificate2Collection(X509Certificate2[] certificates)
		{
			this.AddRange(certificates);
		}

		/// <summary>Gets or sets the element at the specified index.</summary>
		/// <param name="index">The zero-based index of the element to get or set.</param>
		/// <returns>The element at the specified index.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero.  
		/// -or-  
		/// <paramref name="index" /> is equal to or greater than the <see cref="P:System.Collections.CollectionBase.Count" /> property.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="index" /> is <see langword="null" />.</exception>
		// Token: 0x17000437 RID: 1079
		public X509Certificate2 this[int index]
		{
			get
			{
				if (index < 0)
				{
					throw new ArgumentOutOfRangeException("negative index");
				}
				if (index >= base.InnerList.Count)
				{
					throw new ArgumentOutOfRangeException("index >= Count");
				}
				return (X509Certificate2)base.InnerList[index];
			}
			set
			{
				base.InnerList[index] = value;
			}
		}

		/// <summary>Adds an object to the end of the <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2Collection" />.</summary>
		/// <param name="certificate">An X.509 certificate represented as an <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2" /> object.</param>
		/// <returns>The <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2Collection" /> index at which the <paramref name="certificate" /> has been added.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="certificate" /> is <see langword="null" />.</exception>
		// Token: 0x06001668 RID: 5736 RVA: 0x00059C8C File Offset: 0x00057E8C
		public int Add(X509Certificate2 certificate)
		{
			if (certificate == null)
			{
				throw new ArgumentNullException("certificate");
			}
			return base.InnerList.Add(certificate);
		}

		/// <summary>Adds multiple <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2" /> objects in an array to the <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2Collection" /> object.</summary>
		/// <param name="certificates">An array of <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2" /> objects.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="certificates" /> is <see langword="null" />.</exception>
		// Token: 0x06001669 RID: 5737 RVA: 0x00059CA8 File Offset: 0x00057EA8
		[MonoTODO("Method isn't transactional (like documented)")]
		public void AddRange(X509Certificate2[] certificates)
		{
			if (certificates == null)
			{
				throw new ArgumentNullException("certificates");
			}
			for (int i = 0; i < certificates.Length; i++)
			{
				base.InnerList.Add(certificates[i]);
			}
		}

		/// <summary>Adds multiple <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2" /> objects in an <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2Collection" /> object to another <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2Collection" /> object.</summary>
		/// <param name="certificates">An <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2Collection" /> object.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="certificates" /> is <see langword="null" />.</exception>
		// Token: 0x0600166A RID: 5738 RVA: 0x00059CE0 File Offset: 0x00057EE0
		[MonoTODO("Method isn't transactional (like documented)")]
		public void AddRange(X509Certificate2Collection certificates)
		{
			if (certificates == null)
			{
				throw new ArgumentNullException("certificates");
			}
			base.InnerList.AddRange(certificates);
		}

		/// <summary>Determines whether the <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2Collection" /> object contains a specific certificate.</summary>
		/// <param name="certificate">The <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2" /> object to locate in the collection.</param>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2Collection" /> contains the specified <paramref name="certificate" />; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="certificate" /> is <see langword="null" />.</exception>
		// Token: 0x0600166B RID: 5739 RVA: 0x00059CFC File Offset: 0x00057EFC
		public bool Contains(X509Certificate2 certificate)
		{
			if (certificate == null)
			{
				throw new ArgumentNullException("certificate");
			}
			using (IEnumerator enumerator = base.InnerList.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (((X509Certificate2)enumerator.Current).Equals(certificate))
					{
						return true;
					}
				}
			}
			return false;
		}

		/// <summary>Exports X.509 certificate information into a byte array.</summary>
		/// <param name="contentType">A supported <see cref="T:System.Security.Cryptography.X509Certificates.X509ContentType" /> object.</param>
		/// <returns>X.509 certificate information in a byte array.</returns>
		// Token: 0x0600166C RID: 5740 RVA: 0x00059D6C File Offset: 0x00057F6C
		[MonoTODO("only support X509ContentType.Cert")]
		public byte[] Export(X509ContentType contentType)
		{
			return this.Export(contentType, null);
		}

		/// <summary>Exports X.509 certificate information into a byte array using a password.</summary>
		/// <param name="contentType">A supported <see cref="T:System.Security.Cryptography.X509Certificates.X509ContentType" /> object.</param>
		/// <param name="password">A string used to protect the byte array.</param>
		/// <returns>X.509 certificate information in a byte array.</returns>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The certificate is unreadable, the content is invalid or, in the case of a certificate requiring a password, the private key could not be exported because the password provided was incorrect.</exception>
		// Token: 0x0600166D RID: 5741 RVA: 0x00059D78 File Offset: 0x00057F78
		[MonoTODO("only support X509ContentType.Cert")]
		public byte[] Export(X509ContentType contentType, string password)
		{
			switch (contentType)
			{
			case X509ContentType.Cert:
			case X509ContentType.SerializedCert:
			case X509ContentType.Pfx:
				if (base.Count > 0)
				{
					return this[base.Count - 1].Export(contentType, password);
				}
				break;
			case X509ContentType.SerializedStore:
			case X509ContentType.Pkcs7:
				break;
			default:
				throw new CryptographicException(Locale.GetText("Cannot export certificate(s) to the '{0}' format", new object[]
				{
					contentType
				}));
			}
			return null;
		}

		// Token: 0x0600166E RID: 5742 RVA: 0x00059DE4 File Offset: 0x00057FE4
		private string GetKeyIdentifier(X509Certificate2 x)
		{
			X509SubjectKeyIdentifierExtension x509SubjectKeyIdentifierExtension = x.Extensions["2.5.29.14"] as X509SubjectKeyIdentifierExtension;
			if (x509SubjectKeyIdentifierExtension == null)
			{
				x509SubjectKeyIdentifierExtension = new X509SubjectKeyIdentifierExtension(x.PublicKey, X509SubjectKeyIdentifierHashAlgorithm.CapiSha1, false);
			}
			return x509SubjectKeyIdentifierExtension.SubjectKeyIdentifier;
		}

		/// <summary>Searches an <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2Collection" /> object using the search criteria specified by the <see cref="T:System.Security.Cryptography.X509Certificates.X509FindType" /> enumeration and the <paramref name="findValue" /> object.</summary>
		/// <param name="findType">One of the <see cref="T:System.Security.Cryptography.X509Certificates.X509FindType" /> values.</param>
		/// <param name="findValue">The search criteria as an object.</param>
		/// <param name="validOnly">
		///   <see langword="true" /> to allow only valid certificates to be returned from the search; otherwise, <see langword="false" />.</param>
		/// <returns>An <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2Collection" /> object.</returns>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">
		///   <paramref name="findType" /> is invalid.</exception>
		// Token: 0x0600166F RID: 5743 RVA: 0x00059E20 File Offset: 0x00058020
		[MonoTODO("Does not support X509FindType.FindByTemplateName, FindByApplicationPolicy and FindByCertificatePolicy")]
		public X509Certificate2Collection Find(X509FindType findType, object findValue, bool validOnly)
		{
			if (findValue == null)
			{
				throw new ArgumentNullException("findValue");
			}
			string text = string.Empty;
			string text2 = string.Empty;
			X509KeyUsageFlags x509KeyUsageFlags = X509KeyUsageFlags.None;
			DateTime t = DateTime.MinValue;
			switch (findType)
			{
			case X509FindType.FindByThumbprint:
			case X509FindType.FindBySubjectName:
			case X509FindType.FindBySubjectDistinguishedName:
			case X509FindType.FindByIssuerName:
			case X509FindType.FindByIssuerDistinguishedName:
			case X509FindType.FindBySerialNumber:
			case X509FindType.FindByTemplateName:
			case X509FindType.FindBySubjectKeyIdentifier:
				try
				{
					text = (string)findValue;
					goto IL_190;
				}
				catch (Exception inner)
				{
					throw new CryptographicException(Locale.GetText("Invalid find value type '{0}', expected '{1}'.", new object[]
					{
						findValue.GetType(),
						"string"
					}), inner);
				}
				break;
			case X509FindType.FindByTimeValid:
			case X509FindType.FindByTimeNotYetValid:
			case X509FindType.FindByTimeExpired:
				goto IL_13C;
			case X509FindType.FindByApplicationPolicy:
			case X509FindType.FindByCertificatePolicy:
			case X509FindType.FindByExtension:
				break;
			case X509FindType.FindByKeyUsage:
				goto IL_107;
			default:
				goto IL_171;
			}
			try
			{
				text2 = (string)findValue;
			}
			catch (Exception inner2)
			{
				throw new CryptographicException(Locale.GetText("Invalid find value type '{0}', expected '{1}'.", new object[]
				{
					findValue.GetType(),
					"X509KeyUsageFlags"
				}), inner2);
			}
			try
			{
				CryptoConfig.EncodeOID(text2);
				goto IL_190;
			}
			catch (CryptographicUnexpectedOperationException)
			{
				string text3 = Locale.GetText("Invalid OID value '{0}'.", new object[]
				{
					text2
				});
				throw new ArgumentException("findValue", text3);
			}
			IL_107:
			try
			{
				x509KeyUsageFlags = (X509KeyUsageFlags)findValue;
				goto IL_190;
			}
			catch (Exception inner3)
			{
				throw new CryptographicException(Locale.GetText("Invalid find value type '{0}', expected '{1}'.", new object[]
				{
					findValue.GetType(),
					"X509KeyUsageFlags"
				}), inner3);
			}
			IL_13C:
			try
			{
				t = (DateTime)findValue;
				goto IL_190;
			}
			catch (Exception inner4)
			{
				throw new CryptographicException(Locale.GetText("Invalid find value type '{0}', expected '{1}'.", new object[]
				{
					findValue.GetType(),
					"X509DateTime"
				}), inner4);
			}
			IL_171:
			throw new CryptographicException(Locale.GetText("Invalid find type '{0}'.", new object[]
			{
				findType
			}));
			IL_190:
			CultureInfo invariantCulture = CultureInfo.InvariantCulture;
			X509Certificate2Collection x509Certificate2Collection = new X509Certificate2Collection();
			foreach (object obj in base.InnerList)
			{
				X509Certificate2 x509Certificate = (X509Certificate2)obj;
				bool flag = false;
				switch (findType)
				{
				case X509FindType.FindByThumbprint:
					flag = (string.Compare(text, x509Certificate.Thumbprint, true, invariantCulture) == 0 || string.Compare(text, x509Certificate.GetCertHashString(), true, invariantCulture) == 0);
					break;
				case X509FindType.FindBySubjectName:
					foreach (string text4 in x509Certificate.SubjectName.Format(true).Split(X509Certificate2Collection.newline_split, StringSplitOptions.RemoveEmptyEntries))
					{
						int startIndex = text4.IndexOf('=');
						flag = (text4.IndexOf(text, startIndex, StringComparison.InvariantCultureIgnoreCase) >= 0);
						if (flag)
						{
							break;
						}
					}
					break;
				case X509FindType.FindBySubjectDistinguishedName:
					flag = (string.Compare(text, x509Certificate.Subject, true, invariantCulture) == 0);
					break;
				case X509FindType.FindByIssuerName:
					flag = (x509Certificate.GetNameInfo(X509NameType.SimpleName, true).IndexOf(text, StringComparison.InvariantCultureIgnoreCase) >= 0);
					break;
				case X509FindType.FindByIssuerDistinguishedName:
					flag = (string.Compare(text, x509Certificate.Issuer, true, invariantCulture) == 0);
					break;
				case X509FindType.FindBySerialNumber:
					flag = (string.Compare(text, x509Certificate.SerialNumber, true, invariantCulture) == 0);
					break;
				case X509FindType.FindByTimeValid:
					flag = (t >= x509Certificate.NotBefore && t <= x509Certificate.NotAfter);
					break;
				case X509FindType.FindByTimeNotYetValid:
					flag = (t < x509Certificate.NotBefore);
					break;
				case X509FindType.FindByTimeExpired:
					flag = (t > x509Certificate.NotAfter);
					break;
				case X509FindType.FindByApplicationPolicy:
					flag = (x509Certificate.Extensions.Count == 0);
					break;
				case X509FindType.FindByExtension:
					flag = (x509Certificate.Extensions[text2] != null);
					break;
				case X509FindType.FindByKeyUsage:
				{
					X509KeyUsageExtension x509KeyUsageExtension = x509Certificate.Extensions["2.5.29.15"] as X509KeyUsageExtension;
					flag = (x509KeyUsageExtension == null || (x509KeyUsageExtension.KeyUsages & x509KeyUsageFlags) == x509KeyUsageFlags);
					break;
				}
				case X509FindType.FindBySubjectKeyIdentifier:
					flag = (string.Compare(text, this.GetKeyIdentifier(x509Certificate), true, invariantCulture) == 0);
					break;
				}
				if (flag)
				{
					if (validOnly)
					{
						try
						{
							if (x509Certificate.Verify())
							{
								x509Certificate2Collection.Add(x509Certificate);
							}
							continue;
						}
						catch
						{
							continue;
						}
					}
					x509Certificate2Collection.Add(x509Certificate);
				}
			}
			return x509Certificate2Collection;
		}

		/// <summary>Returns an enumerator that can iterate through a <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2Collection" /> object.</summary>
		/// <returns>An <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2Enumerator" /> object that can iterate through the <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2Collection" /> object.</returns>
		// Token: 0x06001670 RID: 5744 RVA: 0x0005A2DC File Offset: 0x000584DC
		public new X509Certificate2Enumerator GetEnumerator()
		{
			return new X509Certificate2Enumerator(this);
		}

		/// <summary>Imports a certificate in the form of a byte array into a <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2Collection" /> object.</summary>
		/// <param name="rawData">A byte array containing data from an X.509 certificate.</param>
		// Token: 0x06001671 RID: 5745 RVA: 0x0005A2E4 File Offset: 0x000584E4
		[MonoTODO("same limitations as X509Certificate2.Import")]
		public void Import(byte[] rawData)
		{
			X509Certificate2 certificate = new X509Certificate2(rawData);
			this.Add(certificate);
		}

		/// <summary>Imports a certificate, in the form of a byte array that requires a password to access the certificate, into a <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2Collection" /> object.</summary>
		/// <param name="rawData">A byte array containing data from an <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2" /> object.</param>
		/// <param name="password">The password required to access the certificate information.</param>
		/// <param name="keyStorageFlags">A bitwise combination of the enumeration values that control how and where the certificate is imported.</param>
		// Token: 0x06001672 RID: 5746 RVA: 0x0005A300 File Offset: 0x00058500
		[MonoTODO("same limitations as X509Certificate2.Import")]
		public void Import(byte[] rawData, string password, X509KeyStorageFlags keyStorageFlags)
		{
			X509Certificate2 certificate = new X509Certificate2(rawData, password, keyStorageFlags);
			this.Add(certificate);
		}

		/// <summary>Imports a certificate file into a <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2Collection" /> object.</summary>
		/// <param name="fileName">The name of the file containing the certificate information.</param>
		// Token: 0x06001673 RID: 5747 RVA: 0x0005A320 File Offset: 0x00058520
		[MonoTODO("same limitations as X509Certificate2.Import")]
		public void Import(string fileName)
		{
			X509Certificate2 certificate = new X509Certificate2(fileName);
			this.Add(certificate);
		}

		/// <summary>Imports a certificate file that requires a password into a <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2Collection" /> object.</summary>
		/// <param name="fileName">The name of the file containing the certificate information.</param>
		/// <param name="password">The password required to access the certificate information.</param>
		/// <param name="keyStorageFlags">A bitwise combination of the enumeration values that control how and where the certificate is imported.</param>
		// Token: 0x06001674 RID: 5748 RVA: 0x0005A33C File Offset: 0x0005853C
		[MonoTODO("same limitations as X509Certificate2.Import")]
		public void Import(string fileName, string password, X509KeyStorageFlags keyStorageFlags)
		{
			X509Certificate2 certificate = new X509Certificate2(fileName, password, keyStorageFlags);
			this.Add(certificate);
		}

		/// <summary>Inserts an object into the <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2Collection" /> object at the specified index.</summary>
		/// <param name="index">The zero-based index at which to insert <paramref name="certificate" />.</param>
		/// <param name="certificate">The <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2" /> object to insert.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero.  
		/// -or-  
		/// <paramref name="index" /> is greater than the <see cref="P:System.Collections.CollectionBase.Count" /> property.</exception>
		/// <exception cref="T:System.NotSupportedException">The collection is read-only.  
		///  -or-  
		///  The collection has a fixed size.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="certificate" /> is <see langword="null" />.</exception>
		// Token: 0x06001675 RID: 5749 RVA: 0x0005A35C File Offset: 0x0005855C
		public void Insert(int index, X509Certificate2 certificate)
		{
			if (certificate == null)
			{
				throw new ArgumentNullException("certificate");
			}
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("negative index");
			}
			if (index >= base.InnerList.Count)
			{
				throw new ArgumentOutOfRangeException("index >= Count");
			}
			base.InnerList.Insert(index, certificate);
		}

		/// <summary>Removes the first occurrence of a certificate from the <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2Collection" /> object.</summary>
		/// <param name="certificate">The <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2" /> object to be removed from the <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2Collection" /> object.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="certificate" /> is <see langword="null" />.</exception>
		// Token: 0x06001676 RID: 5750 RVA: 0x0005A3AC File Offset: 0x000585AC
		public void Remove(X509Certificate2 certificate)
		{
			if (certificate == null)
			{
				throw new ArgumentNullException("certificate");
			}
			for (int i = 0; i < base.InnerList.Count; i++)
			{
				if (((X509Certificate)base.InnerList[i]).Equals(certificate))
				{
					base.InnerList.RemoveAt(i);
					return;
				}
			}
		}

		/// <summary>Removes multiple <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2" /> objects in an array from an <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2Collection" /> object.</summary>
		/// <param name="certificates">An array of <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2" /> objects.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="certificates" /> is <see langword="null" />.</exception>
		// Token: 0x06001677 RID: 5751 RVA: 0x0005A404 File Offset: 0x00058604
		[MonoTODO("Method isn't transactional (like documented)")]
		public void RemoveRange(X509Certificate2[] certificates)
		{
			if (certificates == null)
			{
				throw new ArgumentNullException("certificate");
			}
			foreach (X509Certificate2 certificate in certificates)
			{
				this.Remove(certificate);
			}
		}

		/// <summary>Removes multiple <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2" /> objects in an <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2Collection" /> object from another <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2Collection" /> object.</summary>
		/// <param name="certificates">An <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2Collection" /> object.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="certificates" /> is <see langword="null" />.</exception>
		// Token: 0x06001678 RID: 5752 RVA: 0x0005A43C File Offset: 0x0005863C
		[MonoTODO("Method isn't transactional (like documented)")]
		public void RemoveRange(X509Certificate2Collection certificates)
		{
			if (certificates == null)
			{
				throw new ArgumentNullException("certificate");
			}
			foreach (X509Certificate2 certificate in certificates)
			{
				this.Remove(certificate);
			}
		}

		// Token: 0x06001679 RID: 5753 RVA: 0x0005A476 File Offset: 0x00058676
		// Note: this type is marked as 'beforefieldinit'.
		static X509Certificate2Collection()
		{
		}

		// Token: 0x04000CF7 RID: 3319
		private static string[] newline_split = new string[]
		{
			Environment.NewLine
		};
	}
}

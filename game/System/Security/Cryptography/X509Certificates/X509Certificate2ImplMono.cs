using System;
using System.IO;
using Microsoft.Win32.SafeHandles;
using Mono.Security;
using Mono.Security.Authenticode;
using Mono.Security.Cryptography;
using Mono.Security.X509;

namespace System.Security.Cryptography.X509Certificates
{
	// Token: 0x020002D4 RID: 724
	internal class X509Certificate2ImplMono : X509Certificate2ImplUnix
	{
		// Token: 0x17000445 RID: 1093
		// (get) Token: 0x06001697 RID: 5783 RVA: 0x0005A4FC File Offset: 0x000586FC
		public override bool IsValid
		{
			get
			{
				return this._cert != null;
			}
		}

		// Token: 0x17000446 RID: 1094
		// (get) Token: 0x06001698 RID: 5784 RVA: 0x00011E31 File Offset: 0x00010031
		public override IntPtr Handle
		{
			get
			{
				return IntPtr.Zero;
			}
		}

		// Token: 0x06001699 RID: 5785 RVA: 0x00011E31 File Offset: 0x00010031
		public override IntPtr GetNativeAppleCertificate()
		{
			return IntPtr.Zero;
		}

		// Token: 0x0600169A RID: 5786 RVA: 0x0005A507 File Offset: 0x00058707
		public X509Certificate2ImplMono(X509Certificate cert)
		{
			this._cert = cert;
		}

		// Token: 0x0600169B RID: 5787 RVA: 0x0005A516 File Offset: 0x00058716
		private X509Certificate2ImplMono(X509Certificate2ImplMono other)
		{
			this._cert = other._cert;
			if (other.intermediateCerts != null)
			{
				this.intermediateCerts = other.intermediateCerts.Clone();
			}
		}

		// Token: 0x0600169C RID: 5788 RVA: 0x0005A544 File Offset: 0x00058744
		public X509Certificate2ImplMono(byte[] rawData, SafePasswordHandle password, X509KeyStorageFlags keyStorageFlags)
		{
			switch (X509Certificate2.GetCertContentType(rawData))
			{
			case X509ContentType.Cert:
			case X509ContentType.Pkcs7:
				this._cert = new X509Certificate(rawData);
				return;
			case X509ContentType.Pfx:
				this._cert = this.ImportPkcs12(rawData, password);
				return;
			case X509ContentType.Authenticode:
			{
				AuthenticodeDeformatter authenticodeDeformatter = new AuthenticodeDeformatter(rawData);
				this._cert = authenticodeDeformatter.SigningCertificate;
				if (this._cert != null)
				{
					return;
				}
				break;
			}
			}
			throw new CryptographicException(Locale.GetText("Unable to decode certificate."));
		}

		// Token: 0x0600169D RID: 5789 RVA: 0x0005A5C7 File Offset: 0x000587C7
		public override X509CertificateImpl Clone()
		{
			base.ThrowIfContextInvalid();
			return new X509Certificate2ImplMono(this);
		}

		// Token: 0x17000447 RID: 1095
		// (get) Token: 0x0600169E RID: 5790 RVA: 0x0005A5D5 File Offset: 0x000587D5
		private X509Certificate Cert
		{
			get
			{
				base.ThrowIfContextInvalid();
				return this._cert;
			}
		}

		// Token: 0x0600169F RID: 5791 RVA: 0x0005A5E3 File Offset: 0x000587E3
		protected override byte[] GetRawCertData()
		{
			base.ThrowIfContextInvalid();
			return this.Cert.RawData;
		}

		// Token: 0x060016A0 RID: 5792 RVA: 0x0005A5F6 File Offset: 0x000587F6
		public override bool Equals(X509CertificateImpl other, out bool result)
		{
			result = false;
			return false;
		}

		// Token: 0x060016A1 RID: 5793 RVA: 0x0005A5FC File Offset: 0x000587FC
		public X509Certificate2ImplMono()
		{
			this._cert = null;
		}

		// Token: 0x17000448 RID: 1096
		// (get) Token: 0x060016A2 RID: 5794 RVA: 0x0005A60B File Offset: 0x0005880B
		public override bool HasPrivateKey
		{
			get
			{
				return this.PrivateKey != null;
			}
		}

		// Token: 0x17000449 RID: 1097
		// (get) Token: 0x060016A3 RID: 5795 RVA: 0x0005A618 File Offset: 0x00058818
		// (set) Token: 0x060016A4 RID: 5796 RVA: 0x0005A714 File Offset: 0x00058914
		public override AsymmetricAlgorithm PrivateKey
		{
			get
			{
				if (this._cert == null)
				{
					throw new CryptographicException(X509Certificate2ImplMono.empty_error);
				}
				try
				{
					RSACryptoServiceProvider rsacryptoServiceProvider = this._cert.RSA as RSACryptoServiceProvider;
					if (rsacryptoServiceProvider != null)
					{
						if (rsacryptoServiceProvider.PublicOnly)
						{
							return null;
						}
						RSACryptoServiceProvider rsacryptoServiceProvider2 = new RSACryptoServiceProvider();
						rsacryptoServiceProvider2.ImportParameters(this._cert.RSA.ExportParameters(true));
						return rsacryptoServiceProvider2;
					}
					else
					{
						RSAManaged rsamanaged = this._cert.RSA as RSAManaged;
						if (rsamanaged != null)
						{
							if (rsamanaged.PublicOnly)
							{
								return null;
							}
							RSAManaged rsamanaged2 = new RSAManaged();
							rsamanaged2.ImportParameters(this._cert.RSA.ExportParameters(true));
							return rsamanaged2;
						}
						else
						{
							DSACryptoServiceProvider dsacryptoServiceProvider = this._cert.DSA as DSACryptoServiceProvider;
							if (dsacryptoServiceProvider != null)
							{
								if (dsacryptoServiceProvider.PublicOnly)
								{
									return null;
								}
								DSACryptoServiceProvider dsacryptoServiceProvider2 = new DSACryptoServiceProvider();
								dsacryptoServiceProvider2.ImportParameters(this._cert.DSA.ExportParameters(true));
								return dsacryptoServiceProvider2;
							}
						}
					}
				}
				catch
				{
				}
				return null;
			}
			set
			{
				if (this._cert == null)
				{
					throw new CryptographicException(X509Certificate2ImplMono.empty_error);
				}
				if (value == null)
				{
					this._cert.RSA = null;
					this._cert.DSA = null;
					return;
				}
				if (value is RSA)
				{
					this._cert.RSA = (RSA)value;
					return;
				}
				if (value is DSA)
				{
					this._cert.DSA = (DSA)value;
					return;
				}
				throw new NotSupportedException();
			}
		}

		// Token: 0x060016A5 RID: 5797 RVA: 0x0005A789 File Offset: 0x00058989
		public override RSA GetRSAPrivateKey()
		{
			return this.PrivateKey as RSA;
		}

		// Token: 0x060016A6 RID: 5798 RVA: 0x0005A796 File Offset: 0x00058996
		public override DSA GetDSAPrivateKey()
		{
			return this.PrivateKey as DSA;
		}

		// Token: 0x1700044A RID: 1098
		// (get) Token: 0x060016A7 RID: 5799 RVA: 0x0005A7A4 File Offset: 0x000589A4
		public override PublicKey PublicKey
		{
			get
			{
				if (this._cert == null)
				{
					throw new CryptographicException(X509Certificate2ImplMono.empty_error);
				}
				if (this._publicKey == null)
				{
					try
					{
						this._publicKey = new PublicKey(this._cert);
					}
					catch (Exception inner)
					{
						throw new CryptographicException(Locale.GetText("Unable to decode public key."), inner);
					}
				}
				return this._publicKey;
			}
		}

		// Token: 0x060016A8 RID: 5800 RVA: 0x0005A808 File Offset: 0x00058A08
		private X509Certificate ImportPkcs12(byte[] rawData, SafePasswordHandle password)
		{
			if (password == null || password.IsInvalid)
			{
				return this.ImportPkcs12(rawData, null);
			}
			string password2 = password.Mono_DangerousGetString();
			return this.ImportPkcs12(rawData, password2);
		}

		// Token: 0x060016A9 RID: 5801 RVA: 0x0005A838 File Offset: 0x00058A38
		private X509Certificate ImportPkcs12(byte[] rawData, string password)
		{
			PKCS12 pkcs = null;
			if (string.IsNullOrEmpty(password))
			{
				try
				{
					pkcs = new PKCS12(rawData, null);
					goto IL_2B;
				}
				catch
				{
					pkcs = new PKCS12(rawData, string.Empty);
					goto IL_2B;
				}
			}
			pkcs = new PKCS12(rawData, password);
			IL_2B:
			if (pkcs.Certificates.Count == 0)
			{
				return null;
			}
			if (pkcs.Keys.Count == 0)
			{
				return pkcs.Certificates[0];
			}
			X509Certificate x509Certificate = null;
			AsymmetricAlgorithm asymmetricAlgorithm = pkcs.Keys[0] as AsymmetricAlgorithm;
			string a = asymmetricAlgorithm.ToXmlString(false);
			foreach (X509Certificate x509Certificate2 in pkcs.Certificates)
			{
				if ((x509Certificate2.RSA != null && a == x509Certificate2.RSA.ToXmlString(false)) || (x509Certificate2.DSA != null && a == x509Certificate2.DSA.ToXmlString(false)))
				{
					x509Certificate = x509Certificate2;
					break;
				}
			}
			if (x509Certificate == null)
			{
				x509Certificate = pkcs.Certificates[0];
			}
			else
			{
				x509Certificate.RSA = (asymmetricAlgorithm as RSA);
				x509Certificate.DSA = (asymmetricAlgorithm as DSA);
			}
			if (pkcs.Certificates.Count > 1)
			{
				this.intermediateCerts = new X509CertificateImplCollection();
				foreach (X509Certificate x509Certificate3 in pkcs.Certificates)
				{
					if (x509Certificate3 != x509Certificate)
					{
						X509Certificate2ImplMono impl = new X509Certificate2ImplMono(x509Certificate3);
						this.intermediateCerts.Add(impl, true);
					}
				}
			}
			return x509Certificate;
		}

		// Token: 0x060016AA RID: 5802 RVA: 0x0005A9F0 File Offset: 0x00058BF0
		public override void Reset()
		{
			this._cert = null;
			this._publicKey = null;
			if (this.intermediateCerts != null)
			{
				this.intermediateCerts.Dispose();
				this.intermediateCerts = null;
			}
		}

		// Token: 0x060016AB RID: 5803 RVA: 0x0005AA1A File Offset: 0x00058C1A
		[MonoTODO("by default this depends on the incomplete X509Chain")]
		public override bool Verify(X509Certificate2 thisCertificate)
		{
			if (this._cert == null)
			{
				throw new CryptographicException(X509Certificate2ImplMono.empty_error);
			}
			return X509Chain.Create().Build(thisCertificate);
		}

		// Token: 0x060016AC RID: 5804 RVA: 0x0005AA40 File Offset: 0x00058C40
		[MonoTODO("Detection limited to Cert, Pfx, Pkcs12, Pkcs7 and Unknown")]
		public static X509ContentType GetCertContentType(byte[] rawData)
		{
			if (rawData == null || rawData.Length == 0)
			{
				throw new ArgumentException("rawData");
			}
			X509ContentType result = X509ContentType.Unknown;
			try
			{
				ASN1 asn = new ASN1(rawData);
				if (asn.Tag != 48)
				{
					throw new CryptographicException(Locale.GetText("Unable to decode certificate."));
				}
				if (asn.Count == 0)
				{
					return result;
				}
				if (asn.Count == 3)
				{
					byte tag = asn[0].Tag;
					if (tag != 2)
					{
						if (tag == 48 && asn[1].Tag == 48 && asn[2].Tag == 3)
						{
							result = X509ContentType.Cert;
						}
					}
					else if (asn[1].Tag == 48 && asn[2].Tag == 48)
					{
						result = X509ContentType.Pfx;
					}
				}
				if (asn[0].Tag == 6 && asn[0].CompareValue(X509Certificate2ImplMono.signedData))
				{
					result = X509ContentType.Pkcs7;
				}
			}
			catch (Exception inner)
			{
				throw new CryptographicException(Locale.GetText("Unable to decode certificate."), inner);
			}
			return result;
		}

		// Token: 0x060016AD RID: 5805 RVA: 0x0005AB44 File Offset: 0x00058D44
		[MonoTODO("Detection limited to Cert, Pfx, Pkcs12 and Unknown")]
		public static X509ContentType GetCertContentType(string fileName)
		{
			if (fileName == null)
			{
				throw new ArgumentNullException("fileName");
			}
			if (fileName.Length == 0)
			{
				throw new ArgumentException("fileName");
			}
			return X509Certificate2ImplMono.GetCertContentType(File.ReadAllBytes(fileName));
		}

		// Token: 0x1700044B RID: 1099
		// (get) Token: 0x060016AE RID: 5806 RVA: 0x0005AB72 File Offset: 0x00058D72
		internal override X509CertificateImplCollection IntermediateCertificates
		{
			get
			{
				return this.intermediateCerts;
			}
		}

		// Token: 0x1700044C RID: 1100
		// (get) Token: 0x060016AF RID: 5807 RVA: 0x0005AB7A File Offset: 0x00058D7A
		internal X509Certificate MonoCertificate
		{
			get
			{
				return this._cert;
			}
		}

		// Token: 0x1700044D RID: 1101
		// (get) Token: 0x060016B0 RID: 5808 RVA: 0x000075E1 File Offset: 0x000057E1
		internal override X509Certificate2Impl FallbackImpl
		{
			get
			{
				return this;
			}
		}

		// Token: 0x060016B1 RID: 5809 RVA: 0x0005AB82 File Offset: 0x00058D82
		// Note: this type is marked as 'beforefieldinit'.
		static X509Certificate2ImplMono()
		{
		}

		// Token: 0x04000CF9 RID: 3321
		private PublicKey _publicKey;

		// Token: 0x04000CFA RID: 3322
		private X509CertificateImplCollection intermediateCerts;

		// Token: 0x04000CFB RID: 3323
		private X509Certificate _cert;

		// Token: 0x04000CFC RID: 3324
		private static string empty_error = Locale.GetText("Certificate instance is empty.");

		// Token: 0x04000CFD RID: 3325
		private static byte[] signedData = new byte[]
		{
			42,
			134,
			72,
			134,
			247,
			13,
			1,
			7,
			2
		};
	}
}

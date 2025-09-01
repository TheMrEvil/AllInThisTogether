using System;
using System.Security;
using System.Security.Cryptography;
using Mono.Security.Cryptography;
using Mono.Security.X509;

namespace Mono.Security.Authenticode
{
	// Token: 0x02000066 RID: 102
	public class AuthenticodeDeformatter : AuthenticodeBase
	{
		// Token: 0x060003EA RID: 1002 RVA: 0x00015384 File Offset: 0x00013584
		public AuthenticodeDeformatter()
		{
			this.reason = -1;
			this.signerChain = new X509Chain();
			this.timestampChain = new X509Chain();
		}

		// Token: 0x060003EB RID: 1003 RVA: 0x000153A9 File Offset: 0x000135A9
		public AuthenticodeDeformatter(string fileName) : this()
		{
			this.FileName = fileName;
		}

		// Token: 0x060003EC RID: 1004 RVA: 0x000153B8 File Offset: 0x000135B8
		public AuthenticodeDeformatter(byte[] rawData) : this()
		{
			this.RawData = rawData;
		}

		// Token: 0x1700010C RID: 268
		// (get) Token: 0x060003ED RID: 1005 RVA: 0x000153C7 File Offset: 0x000135C7
		// (set) Token: 0x060003EE RID: 1006 RVA: 0x000153D0 File Offset: 0x000135D0
		public string FileName
		{
			get
			{
				return this.filename;
			}
			set
			{
				this.Reset();
				this.filename = value;
				try
				{
					this.CheckSignature();
				}
				catch (SecurityException)
				{
					throw;
				}
				catch
				{
					this.reason = 1;
				}
			}
		}

		// Token: 0x1700010D RID: 269
		// (get) Token: 0x060003EF RID: 1007 RVA: 0x0001541C File Offset: 0x0001361C
		// (set) Token: 0x060003F0 RID: 1008 RVA: 0x00015424 File Offset: 0x00013624
		public byte[] RawData
		{
			get
			{
				return this.rawdata;
			}
			set
			{
				this.Reset();
				this.rawdata = value;
				try
				{
					this.CheckSignature();
				}
				catch (SecurityException)
				{
					throw;
				}
				catch
				{
					this.reason = 1;
				}
			}
		}

		// Token: 0x1700010E RID: 270
		// (get) Token: 0x060003F1 RID: 1009 RVA: 0x00015470 File Offset: 0x00013670
		public byte[] Hash
		{
			get
			{
				if (this.signedHash == null)
				{
					return null;
				}
				return (byte[])this.signedHash.Value.Clone();
			}
		}

		// Token: 0x1700010F RID: 271
		// (get) Token: 0x060003F2 RID: 1010 RVA: 0x00015491 File Offset: 0x00013691
		public int Reason
		{
			get
			{
				if (this.reason == -1)
				{
					this.IsTrusted();
				}
				return this.reason;
			}
		}

		// Token: 0x060003F3 RID: 1011 RVA: 0x000154AC File Offset: 0x000136AC
		public bool IsTrusted()
		{
			if (this.entry == null)
			{
				this.reason = 1;
				return false;
			}
			if (this.signingCertificate == null)
			{
				this.reason = 7;
				return false;
			}
			if (this.signerChain.Root == null || !this.trustedRoot)
			{
				this.reason = 6;
				return false;
			}
			if (this.timestamp != DateTime.MinValue)
			{
				if (this.timestampChain.Root == null || !this.trustedTimestampRoot)
				{
					this.reason = 6;
					return false;
				}
				if (!this.signingCertificate.WasCurrent(this.Timestamp))
				{
					this.reason = 4;
					return false;
				}
			}
			else if (!this.signingCertificate.IsCurrent)
			{
				this.reason = 8;
				return false;
			}
			if (this.reason == -1)
			{
				this.reason = 0;
			}
			return true;
		}

		// Token: 0x17000110 RID: 272
		// (get) Token: 0x060003F4 RID: 1012 RVA: 0x0001556C File Offset: 0x0001376C
		public byte[] Signature
		{
			get
			{
				if (this.entry == null)
				{
					return null;
				}
				return (byte[])this.entry.Clone();
			}
		}

		// Token: 0x17000111 RID: 273
		// (get) Token: 0x060003F5 RID: 1013 RVA: 0x00015588 File Offset: 0x00013788
		public DateTime Timestamp
		{
			get
			{
				return this.timestamp;
			}
		}

		// Token: 0x17000112 RID: 274
		// (get) Token: 0x060003F6 RID: 1014 RVA: 0x00015590 File Offset: 0x00013790
		public X509CertificateCollection Certificates
		{
			get
			{
				return this.coll;
			}
		}

		// Token: 0x17000113 RID: 275
		// (get) Token: 0x060003F7 RID: 1015 RVA: 0x00015598 File Offset: 0x00013798
		public X509Certificate SigningCertificate
		{
			get
			{
				return this.signingCertificate;
			}
		}

		// Token: 0x060003F8 RID: 1016 RVA: 0x000155A0 File Offset: 0x000137A0
		private bool CheckSignature()
		{
			if (this.filename != null)
			{
				base.Open(this.filename);
			}
			else
			{
				base.Open(this.rawdata);
			}
			this.entry = base.GetSecurityEntry();
			if (this.entry == null)
			{
				this.reason = 1;
				base.Close();
				return false;
			}
			PKCS7.ContentInfo contentInfo = new PKCS7.ContentInfo(this.entry);
			if (contentInfo.ContentType != "1.2.840.113549.1.7.2")
			{
				base.Close();
				return false;
			}
			PKCS7.SignedData signedData = new PKCS7.SignedData(contentInfo.Content);
			if (signedData.ContentInfo.ContentType != "1.3.6.1.4.1.311.2.1.4")
			{
				base.Close();
				return false;
			}
			this.coll = signedData.Certificates;
			ASN1 content = signedData.ContentInfo.Content;
			this.signedHash = content[0][1][1];
			int length = this.signedHash.Length;
			HashAlgorithm hashAlgorithm;
			if (length <= 20)
			{
				if (length == 16)
				{
					hashAlgorithm = MD5.Create();
					this.hash = base.GetHash(hashAlgorithm);
					goto IL_176;
				}
				if (length == 20)
				{
					hashAlgorithm = SHA1.Create();
					this.hash = base.GetHash(hashAlgorithm);
					goto IL_176;
				}
			}
			else
			{
				if (length == 32)
				{
					hashAlgorithm = SHA256.Create();
					this.hash = base.GetHash(hashAlgorithm);
					goto IL_176;
				}
				if (length == 48)
				{
					hashAlgorithm = SHA384.Create();
					this.hash = base.GetHash(hashAlgorithm);
					goto IL_176;
				}
				if (length == 64)
				{
					hashAlgorithm = SHA512.Create();
					this.hash = base.GetHash(hashAlgorithm);
					goto IL_176;
				}
			}
			this.reason = 5;
			base.Close();
			return false;
			IL_176:
			base.Close();
			if (!this.signedHash.CompareValue(this.hash))
			{
				this.reason = 2;
			}
			byte[] value = content[0].Value;
			hashAlgorithm.Initialize();
			byte[] calculatedMessageDigest = hashAlgorithm.ComputeHash(value);
			return this.VerifySignature(signedData, calculatedMessageDigest, hashAlgorithm) && this.reason == 0;
		}

		// Token: 0x060003F9 RID: 1017 RVA: 0x00015778 File Offset: 0x00013978
		private bool CompareIssuerSerial(string issuer, byte[] serial, X509Certificate x509)
		{
			if (issuer != x509.IssuerName)
			{
				return false;
			}
			if (serial.Length != x509.SerialNumber.Length)
			{
				return false;
			}
			int num = serial.Length;
			for (int i = 0; i < serial.Length; i++)
			{
				if (serial[i] != x509.SerialNumber[--num])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060003FA RID: 1018 RVA: 0x000157CC File Offset: 0x000139CC
		private bool VerifySignature(PKCS7.SignedData sd, byte[] calculatedMessageDigest, HashAlgorithm ha)
		{
			string a = null;
			ASN1 asn = null;
			for (int i = 0; i < sd.SignerInfo.AuthenticatedAttributes.Count; i++)
			{
				ASN1 asn2 = (ASN1)sd.SignerInfo.AuthenticatedAttributes[i];
				string a2 = ASN1Convert.ToOid(asn2[0]);
				if (!(a2 == "1.2.840.113549.1.9.3"))
				{
					if (!(a2 == "1.2.840.113549.1.9.4"))
					{
						if (!(a2 == "1.3.6.1.4.1.311.2.1.11") && !(a2 == "1.3.6.1.4.1.311.2.1.12"))
						{
						}
					}
					else
					{
						asn = asn2[1][0];
					}
				}
				else
				{
					a = ASN1Convert.ToOid(asn2[1][0]);
				}
			}
			if (a != "1.3.6.1.4.1.311.2.1.4")
			{
				return false;
			}
			if (asn == null)
			{
				return false;
			}
			if (!asn.CompareValue(calculatedMessageDigest))
			{
				return false;
			}
			string str = CryptoConfig.MapNameToOID(ha.ToString());
			ASN1 asn3 = new ASN1(49);
			foreach (object obj in sd.SignerInfo.AuthenticatedAttributes)
			{
				ASN1 asn4 = (ASN1)obj;
				asn3.Add(asn4);
			}
			ha.Initialize();
			byte[] rgbHash = ha.ComputeHash(asn3.GetBytes());
			byte[] signature = sd.SignerInfo.Signature;
			string issuerName = sd.SignerInfo.IssuerName;
			byte[] serialNumber = sd.SignerInfo.SerialNumber;
			foreach (X509Certificate x509Certificate in this.coll)
			{
				if (this.CompareIssuerSerial(issuerName, serialNumber, x509Certificate) && x509Certificate.PublicKey.Length > signature.Length >> 3)
				{
					this.signingCertificate = x509Certificate;
					if (((RSACryptoServiceProvider)x509Certificate.RSA).VerifyHash(rgbHash, str, signature))
					{
						this.signerChain.LoadCertificates(this.coll);
						this.trustedRoot = this.signerChain.Build(x509Certificate);
						break;
					}
				}
			}
			if (sd.SignerInfo.UnauthenticatedAttributes.Count == 0)
			{
				this.trustedTimestampRoot = true;
			}
			else
			{
				for (int j = 0; j < sd.SignerInfo.UnauthenticatedAttributes.Count; j++)
				{
					ASN1 asn5 = (ASN1)sd.SignerInfo.UnauthenticatedAttributes[j];
					if (ASN1Convert.ToOid(asn5[0]) == "1.2.840.113549.1.9.6")
					{
						PKCS7.SignerInfo cs = new PKCS7.SignerInfo(asn5[1]);
						this.trustedTimestampRoot = this.VerifyCounterSignature(cs, signature);
					}
				}
			}
			return this.trustedRoot && this.trustedTimestampRoot;
		}

		// Token: 0x060003FB RID: 1019 RVA: 0x00015A98 File Offset: 0x00013C98
		private bool VerifyCounterSignature(PKCS7.SignerInfo cs, byte[] signature)
		{
			if (cs.Version > 1)
			{
				return false;
			}
			string a = null;
			ASN1 asn = null;
			for (int i = 0; i < cs.AuthenticatedAttributes.Count; i++)
			{
				ASN1 asn2 = (ASN1)cs.AuthenticatedAttributes[i];
				string a2 = ASN1Convert.ToOid(asn2[0]);
				if (!(a2 == "1.2.840.113549.1.9.3"))
				{
					if (!(a2 == "1.2.840.113549.1.9.4"))
					{
						if (a2 == "1.2.840.113549.1.9.5")
						{
							this.timestamp = ASN1Convert.ToDateTime(asn2[1][0]);
						}
					}
					else
					{
						asn = asn2[1][0];
					}
				}
				else
				{
					a = ASN1Convert.ToOid(asn2[1][0]);
				}
			}
			if (a != "1.2.840.113549.1.7.1")
			{
				return false;
			}
			if (asn == null)
			{
				return false;
			}
			string hashName = null;
			int length = asn.Length;
			if (length <= 20)
			{
				if (length != 16)
				{
					if (length == 20)
					{
						hashName = "SHA1";
					}
				}
				else
				{
					hashName = "MD5";
				}
			}
			else if (length != 32)
			{
				if (length != 48)
				{
					if (length == 64)
					{
						hashName = "SHA512";
					}
				}
				else
				{
					hashName = "SHA384";
				}
			}
			else
			{
				hashName = "SHA256";
			}
			HashAlgorithm hashAlgorithm = HashAlgorithm.Create(hashName);
			if (!asn.CompareValue(hashAlgorithm.ComputeHash(signature)))
			{
				return false;
			}
			byte[] signature2 = cs.Signature;
			ASN1 asn3 = new ASN1(49);
			foreach (object obj in cs.AuthenticatedAttributes)
			{
				ASN1 asn4 = (ASN1)obj;
				asn3.Add(asn4);
			}
			byte[] hashValue = hashAlgorithm.ComputeHash(asn3.GetBytes());
			string issuerName = cs.IssuerName;
			byte[] serialNumber = cs.SerialNumber;
			foreach (X509Certificate x509Certificate in this.coll)
			{
				if (this.CompareIssuerSerial(issuerName, serialNumber, x509Certificate) && x509Certificate.PublicKey.Length > signature2.Length)
				{
					RSACryptoServiceProvider rsacryptoServiceProvider = (RSACryptoServiceProvider)x509Certificate.RSA;
					RSAManaged rsamanaged = new RSAManaged();
					rsamanaged.ImportParameters(rsacryptoServiceProvider.ExportParameters(false));
					if (PKCS1.Verify_v15(rsamanaged, hashAlgorithm, hashValue, signature2, true))
					{
						this.timestampChain.LoadCertificates(this.coll);
						return this.timestampChain.Build(x509Certificate);
					}
				}
			}
			return false;
		}

		// Token: 0x060003FC RID: 1020 RVA: 0x00015D24 File Offset: 0x00013F24
		private void Reset()
		{
			this.filename = null;
			this.rawdata = null;
			this.entry = null;
			this.hash = null;
			this.signedHash = null;
			this.signingCertificate = null;
			this.reason = -1;
			this.trustedRoot = false;
			this.trustedTimestampRoot = false;
			this.signerChain.Reset();
			this.timestampChain.Reset();
			this.timestamp = DateTime.MinValue;
		}

		// Token: 0x0400030F RID: 783
		private string filename;

		// Token: 0x04000310 RID: 784
		private byte[] rawdata;

		// Token: 0x04000311 RID: 785
		private byte[] hash;

		// Token: 0x04000312 RID: 786
		private X509CertificateCollection coll;

		// Token: 0x04000313 RID: 787
		private ASN1 signedHash;

		// Token: 0x04000314 RID: 788
		private DateTime timestamp;

		// Token: 0x04000315 RID: 789
		private X509Certificate signingCertificate;

		// Token: 0x04000316 RID: 790
		private int reason;

		// Token: 0x04000317 RID: 791
		private bool trustedRoot;

		// Token: 0x04000318 RID: 792
		private bool trustedTimestampRoot;

		// Token: 0x04000319 RID: 793
		private byte[] entry;

		// Token: 0x0400031A RID: 794
		private X509Chain signerChain;

		// Token: 0x0400031B RID: 795
		private X509Chain timestampChain;
	}
}

using System;
using System.Collections;
using System.Globalization;
using System.IO;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using Mono.Security.X509;

namespace Mono.Security.Authenticode
{
	// Token: 0x02000067 RID: 103
	public class AuthenticodeFormatter : AuthenticodeBase
	{
		// Token: 0x060003FD RID: 1021 RVA: 0x00015D91 File Offset: 0x00013F91
		public AuthenticodeFormatter()
		{
			this.certs = new X509CertificateCollection();
			this.crls = new ArrayList();
			this.authority = Authority.Maximum;
			this.pkcs7 = new PKCS7.SignedData();
		}

		// Token: 0x17000114 RID: 276
		// (get) Token: 0x060003FE RID: 1022 RVA: 0x00015DC1 File Offset: 0x00013FC1
		// (set) Token: 0x060003FF RID: 1023 RVA: 0x00015DC9 File Offset: 0x00013FC9
		public Authority Authority
		{
			get
			{
				return this.authority;
			}
			set
			{
				this.authority = value;
			}
		}

		// Token: 0x17000115 RID: 277
		// (get) Token: 0x06000400 RID: 1024 RVA: 0x00015DD2 File Offset: 0x00013FD2
		public X509CertificateCollection Certificates
		{
			get
			{
				return this.certs;
			}
		}

		// Token: 0x17000116 RID: 278
		// (get) Token: 0x06000401 RID: 1025 RVA: 0x00015DDA File Offset: 0x00013FDA
		public ArrayList Crl
		{
			get
			{
				return this.crls;
			}
		}

		// Token: 0x17000117 RID: 279
		// (get) Token: 0x06000402 RID: 1026 RVA: 0x00015DE2 File Offset: 0x00013FE2
		// (set) Token: 0x06000403 RID: 1027 RVA: 0x00015E00 File Offset: 0x00014000
		public string Hash
		{
			get
			{
				if (this.hash == null)
				{
					this.hash = "SHA1";
				}
				return this.hash;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("Hash");
				}
				string a = value.ToUpper(CultureInfo.InvariantCulture);
				if (a == "MD5" || a == "SHA1" || a == "SHA256" || a == "SHA384" || a == "SHA512")
				{
					this.hash = a;
					return;
				}
				if (!(a == "SHA2"))
				{
					throw new ArgumentException("Invalid Authenticode hash algorithm");
				}
				this.hash = "SHA256";
			}
		}

		// Token: 0x17000118 RID: 280
		// (get) Token: 0x06000404 RID: 1028 RVA: 0x00015E95 File Offset: 0x00014095
		// (set) Token: 0x06000405 RID: 1029 RVA: 0x00015E9D File Offset: 0x0001409D
		public RSA RSA
		{
			get
			{
				return this.rsa;
			}
			set
			{
				this.rsa = value;
			}
		}

		// Token: 0x17000119 RID: 281
		// (get) Token: 0x06000406 RID: 1030 RVA: 0x00015EA6 File Offset: 0x000140A6
		// (set) Token: 0x06000407 RID: 1031 RVA: 0x00015EAE File Offset: 0x000140AE
		public Uri TimestampUrl
		{
			get
			{
				return this.timestamp;
			}
			set
			{
				this.timestamp = value;
			}
		}

		// Token: 0x1700011A RID: 282
		// (get) Token: 0x06000408 RID: 1032 RVA: 0x00015EB7 File Offset: 0x000140B7
		// (set) Token: 0x06000409 RID: 1033 RVA: 0x00015EBF File Offset: 0x000140BF
		public string Description
		{
			get
			{
				return this.description;
			}
			set
			{
				this.description = value;
			}
		}

		// Token: 0x1700011B RID: 283
		// (get) Token: 0x0600040A RID: 1034 RVA: 0x00015EC8 File Offset: 0x000140C8
		// (set) Token: 0x0600040B RID: 1035 RVA: 0x00015ED0 File Offset: 0x000140D0
		public Uri Url
		{
			get
			{
				return this.url;
			}
			set
			{
				this.url = value;
			}
		}

		// Token: 0x0600040C RID: 1036 RVA: 0x00015ED9 File Offset: 0x000140D9
		private ASN1 AlgorithmIdentifier(string oid)
		{
			ASN1 asn = new ASN1(48);
			asn.Add(ASN1Convert.FromOid(oid));
			asn.Add(new ASN1(5));
			return asn;
		}

		// Token: 0x0600040D RID: 1037 RVA: 0x00015EFC File Offset: 0x000140FC
		private ASN1 Attribute(string oid, ASN1 value)
		{
			ASN1 asn = new ASN1(48);
			asn.Add(ASN1Convert.FromOid(oid));
			asn.Add(new ASN1(49)).Add(value);
			return asn;
		}

		// Token: 0x0600040E RID: 1038 RVA: 0x00015F28 File Offset: 0x00014128
		private ASN1 Opus(string description, string url)
		{
			ASN1 asn = new ASN1(48);
			if (description != null)
			{
				asn.Add(new ASN1(160)).Add(new ASN1(128, Encoding.BigEndianUnicode.GetBytes(description)));
			}
			if (url != null)
			{
				asn.Add(new ASN1(161)).Add(new ASN1(128, Encoding.ASCII.GetBytes(url)));
			}
			return asn;
		}

		// Token: 0x0600040F RID: 1039 RVA: 0x00015F9C File Offset: 0x0001419C
		private byte[] Header(byte[] fileHash, string hashAlgorithm)
		{
			string oid = CryptoConfig.MapNameToOID(hashAlgorithm);
			ASN1 asn = new ASN1(48);
			ASN1 asn2 = asn.Add(new ASN1(48));
			asn2.Add(ASN1Convert.FromOid("1.3.6.1.4.1.311.2.1.15"));
			asn2.Add(new ASN1(48, AuthenticodeFormatter.obsolete));
			ASN1 asn3 = asn.Add(new ASN1(48));
			asn3.Add(this.AlgorithmIdentifier(oid));
			asn3.Add(new ASN1(4, fileHash));
			this.pkcs7.HashName = hashAlgorithm;
			this.pkcs7.Certificates.AddRange(this.certs);
			this.pkcs7.ContentInfo.ContentType = "1.3.6.1.4.1.311.2.1.4";
			this.pkcs7.ContentInfo.Content.Add(asn);
			this.pkcs7.SignerInfo.Certificate = this.certs[0];
			this.pkcs7.SignerInfo.Key = this.rsa;
			ASN1 value;
			if (this.url == null)
			{
				value = this.Attribute("1.3.6.1.4.1.311.2.1.12", this.Opus(this.description, null));
			}
			else
			{
				value = this.Attribute("1.3.6.1.4.1.311.2.1.12", this.Opus(this.description, this.url.ToString()));
			}
			this.pkcs7.SignerInfo.AuthenticatedAttributes.Add(value);
			this.pkcs7.GetASN1();
			return this.pkcs7.SignerInfo.Signature;
		}

		// Token: 0x06000410 RID: 1040 RVA: 0x00016114 File Offset: 0x00014314
		public ASN1 TimestampRequest(byte[] signature)
		{
			PKCS7.ContentInfo contentInfo = new PKCS7.ContentInfo("1.2.840.113549.1.7.1");
			contentInfo.Content.Add(new ASN1(4, signature));
			return PKCS7.AlgorithmIdentifier("1.3.6.1.4.1.311.3.2.1", contentInfo.ASN1);
		}

		// Token: 0x06000411 RID: 1041 RVA: 0x00016150 File Offset: 0x00014350
		public void ProcessTimestamp(byte[] response)
		{
			ASN1 asn = new ASN1(Convert.FromBase64String(Encoding.ASCII.GetString(response)));
			for (int i = 0; i < asn[1][0][3].Count; i++)
			{
				this.pkcs7.Certificates.Add(new X509Certificate(asn[1][0][3][i].GetBytes()));
			}
			this.pkcs7.SignerInfo.UnauthenticatedAttributes.Add(this.Attribute("1.2.840.113549.1.9.6", asn[1][0][4][0]));
		}

		// Token: 0x06000412 RID: 1042 RVA: 0x00016204 File Offset: 0x00014404
		private byte[] Timestamp(byte[] signature)
		{
			ASN1 asn = this.TimestampRequest(signature);
			WebClient webClient = new WebClient();
			webClient.Headers.Add("Content-Type", "application/octet-stream");
			webClient.Headers.Add("Accept", "application/octet-stream");
			byte[] bytes = Encoding.ASCII.GetBytes(Convert.ToBase64String(asn.GetBytes()));
			return webClient.UploadData(this.timestamp.ToString(), bytes);
		}

		// Token: 0x06000413 RID: 1043 RVA: 0x00016270 File Offset: 0x00014470
		private bool Save(string fileName, byte[] asn)
		{
			File.Copy(fileName, fileName + ".bak", true);
			using (FileStream fileStream = File.Open(fileName, FileMode.Open, FileAccess.ReadWrite))
			{
				int num;
				if (base.SecurityOffset > 0)
				{
					num = base.SecurityOffset;
				}
				else if (base.CoffSymbolTableOffset > 0)
				{
					fileStream.Seek((long)(base.PEOffset + 12), SeekOrigin.Begin);
					for (int i = 0; i < 8; i++)
					{
						fileStream.WriteByte(0);
					}
					num = base.CoffSymbolTableOffset;
				}
				else
				{
					num = (int)fileStream.Length;
				}
				int num2 = num & 7;
				if (num2 > 0)
				{
					num2 = 8 - num2;
				}
				byte[] bytes = BitConverterLE.GetBytes(num + num2);
				if (base.PE64)
				{
					fileStream.Seek((long)(base.PEOffset + 168), SeekOrigin.Begin);
				}
				else
				{
					fileStream.Seek((long)(base.PEOffset + 152), SeekOrigin.Begin);
				}
				fileStream.Write(bytes, 0, 4);
				int num3 = asn.Length + 8;
				int num4 = num3 & 7;
				if (num4 > 0)
				{
					num4 = 8 - num4;
				}
				bytes = BitConverterLE.GetBytes(num3 + num4);
				if (base.PE64)
				{
					fileStream.Seek((long)(base.PEOffset + 168 + 4), SeekOrigin.Begin);
				}
				else
				{
					fileStream.Seek((long)(base.PEOffset + 156), SeekOrigin.Begin);
				}
				fileStream.Write(bytes, 0, 4);
				fileStream.Seek((long)num, SeekOrigin.Begin);
				if (num2 > 0)
				{
					byte[] array = new byte[num2];
					fileStream.Write(array, 0, array.Length);
				}
				fileStream.Write(bytes, 0, bytes.Length);
				bytes = BitConverterLE.GetBytes(512);
				fileStream.Write(bytes, 0, bytes.Length);
				bytes = BitConverterLE.GetBytes(2);
				fileStream.Write(bytes, 0, bytes.Length);
				fileStream.Write(asn, 0, asn.Length);
				if (num4 > 0)
				{
					byte[] array2 = new byte[num4];
					fileStream.Write(array2, 0, array2.Length);
				}
				fileStream.Close();
			}
			return true;
		}

		// Token: 0x06000414 RID: 1044 RVA: 0x00016448 File Offset: 0x00014648
		public bool Sign(string fileName)
		{
			try
			{
				base.Open(fileName);
				HashAlgorithm hashAlgorithm = HashAlgorithm.Create(this.Hash);
				byte[] fileHash = base.GetHash(hashAlgorithm);
				byte[] signature = this.Header(fileHash, this.Hash);
				if (this.timestamp != null)
				{
					byte[] response = this.Timestamp(signature);
					this.ProcessTimestamp(response);
				}
				PKCS7.ContentInfo contentInfo = new PKCS7.ContentInfo("1.2.840.113549.1.7.2");
				contentInfo.Content.Add(this.pkcs7.ASN1);
				this.authenticode = contentInfo.ASN1;
				base.Close();
				return this.Save(fileName, this.authenticode.GetBytes());
			}
			catch (Exception value)
			{
				Console.WriteLine(value);
			}
			return false;
		}

		// Token: 0x06000415 RID: 1045 RVA: 0x00016504 File Offset: 0x00014704
		public bool Timestamp(string fileName)
		{
			try
			{
				byte[] signature = new AuthenticodeDeformatter(fileName).Signature;
				if (signature != null)
				{
					base.Open(fileName);
					PKCS7.ContentInfo contentInfo = new PKCS7.ContentInfo(signature);
					this.pkcs7 = new PKCS7.SignedData(contentInfo.Content);
					byte[] bytes = this.Timestamp(this.pkcs7.SignerInfo.Signature);
					ASN1 asn = new ASN1(Convert.FromBase64String(Encoding.ASCII.GetString(bytes)));
					ASN1 asn2 = new ASN1(signature);
					ASN1 asn3 = asn2.Element(1, 160);
					if (asn3 == null)
					{
						return false;
					}
					ASN1 asn4 = asn3.Element(0, 48);
					if (asn4 == null)
					{
						return false;
					}
					ASN1 asn5 = asn4.Element(3, 160);
					if (asn5 == null)
					{
						asn5 = new ASN1(160);
						asn4.Add(asn5);
					}
					for (int i = 0; i < asn[1][0][3].Count; i++)
					{
						asn5.Add(asn[1][0][3][i]);
					}
					ASN1 asn6 = asn4[asn4.Count - 1][0];
					ASN1 asn7 = asn6[asn6.Count - 1];
					if (asn7.Tag != 161)
					{
						asn7 = new ASN1(161);
						asn6.Add(asn7);
					}
					asn7.Add(this.Attribute("1.2.840.113549.1.9.6", asn[1][0][4][0]));
					return this.Save(fileName, asn2.GetBytes());
				}
			}
			catch (Exception value)
			{
				Console.WriteLine(value);
			}
			return false;
		}

		// Token: 0x06000416 RID: 1046 RVA: 0x000166D4 File Offset: 0x000148D4
		// Note: this type is marked as 'beforefieldinit'.
		static AuthenticodeFormatter()
		{
		}

		// Token: 0x0400031C RID: 796
		private Authority authority;

		// Token: 0x0400031D RID: 797
		private X509CertificateCollection certs;

		// Token: 0x0400031E RID: 798
		private ArrayList crls;

		// Token: 0x0400031F RID: 799
		private string hash;

		// Token: 0x04000320 RID: 800
		private RSA rsa;

		// Token: 0x04000321 RID: 801
		private Uri timestamp;

		// Token: 0x04000322 RID: 802
		private ASN1 authenticode;

		// Token: 0x04000323 RID: 803
		private PKCS7.SignedData pkcs7;

		// Token: 0x04000324 RID: 804
		private string description;

		// Token: 0x04000325 RID: 805
		private Uri url;

		// Token: 0x04000326 RID: 806
		private const string signedData = "1.2.840.113549.1.7.2";

		// Token: 0x04000327 RID: 807
		private const string countersignature = "1.2.840.113549.1.9.6";

		// Token: 0x04000328 RID: 808
		private const string spcStatementType = "1.3.6.1.4.1.311.2.1.11";

		// Token: 0x04000329 RID: 809
		private const string spcSpOpusInfo = "1.3.6.1.4.1.311.2.1.12";

		// Token: 0x0400032A RID: 810
		private const string spcPelmageData = "1.3.6.1.4.1.311.2.1.15";

		// Token: 0x0400032B RID: 811
		private const string commercialCodeSigning = "1.3.6.1.4.1.311.2.1.22";

		// Token: 0x0400032C RID: 812
		private const string timestampCountersignature = "1.3.6.1.4.1.311.3.2.1";

		// Token: 0x0400032D RID: 813
		private static byte[] obsolete = new byte[]
		{
			3,
			1,
			0,
			160,
			32,
			162,
			30,
			128,
			28,
			0,
			60,
			0,
			60,
			0,
			60,
			0,
			79,
			0,
			98,
			0,
			115,
			0,
			111,
			0,
			108,
			0,
			101,
			0,
			116,
			0,
			101,
			0,
			62,
			0,
			62,
			0,
			62
		};
	}
}

using System;
using System.Collections;
using System.Security.Cryptography;
using Mono.Security.X509;

namespace Mono.Security
{
	// Token: 0x0200000A RID: 10
	public sealed class PKCS7
	{
		// Token: 0x0600004A RID: 74 RVA: 0x00003804 File Offset: 0x00001A04
		private PKCS7()
		{
		}

		// Token: 0x0600004B RID: 75 RVA: 0x0000380C File Offset: 0x00001A0C
		public static ASN1 Attribute(string oid, ASN1 value)
		{
			ASN1 asn = new ASN1(48);
			asn.Add(ASN1Convert.FromOid(oid));
			asn.Add(new ASN1(49)).Add(value);
			return asn;
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00003836 File Offset: 0x00001A36
		public static ASN1 AlgorithmIdentifier(string oid)
		{
			ASN1 asn = new ASN1(48);
			asn.Add(ASN1Convert.FromOid(oid));
			asn.Add(new ASN1(5));
			return asn;
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00003859 File Offset: 0x00001A59
		public static ASN1 AlgorithmIdentifier(string oid, ASN1 parameters)
		{
			ASN1 asn = new ASN1(48);
			asn.Add(ASN1Convert.FromOid(oid));
			asn.Add(parameters);
			return asn;
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00003878 File Offset: 0x00001A78
		public static ASN1 IssuerAndSerialNumber(X509Certificate x509)
		{
			ASN1 asn = null;
			ASN1 asn2 = null;
			ASN1 asn3 = new ASN1(x509.RawData);
			int i = 0;
			bool flag = false;
			while (i < asn3[0].Count)
			{
				ASN1 asn4 = asn3[0][i++];
				if (asn4.Tag == 2)
				{
					asn2 = asn4;
				}
				else if (asn4.Tag == 48)
				{
					if (flag)
					{
						asn = asn4;
						break;
					}
					flag = true;
				}
			}
			ASN1 asn5 = new ASN1(48);
			asn5.Add(asn);
			asn5.Add(asn2);
			return asn5;
		}

		// Token: 0x0200007B RID: 123
		public class Oid
		{
			// Token: 0x060004B8 RID: 1208 RVA: 0x000180BF File Offset: 0x000162BF
			public Oid()
			{
			}

			// Token: 0x0400038D RID: 909
			public const string rsaEncryption = "1.2.840.113549.1.1.1";

			// Token: 0x0400038E RID: 910
			public const string data = "1.2.840.113549.1.7.1";

			// Token: 0x0400038F RID: 911
			public const string signedData = "1.2.840.113549.1.7.2";

			// Token: 0x04000390 RID: 912
			public const string envelopedData = "1.2.840.113549.1.7.3";

			// Token: 0x04000391 RID: 913
			public const string signedAndEnvelopedData = "1.2.840.113549.1.7.4";

			// Token: 0x04000392 RID: 914
			public const string digestedData = "1.2.840.113549.1.7.5";

			// Token: 0x04000393 RID: 915
			public const string encryptedData = "1.2.840.113549.1.7.6";

			// Token: 0x04000394 RID: 916
			public const string contentType = "1.2.840.113549.1.9.3";

			// Token: 0x04000395 RID: 917
			public const string messageDigest = "1.2.840.113549.1.9.4";

			// Token: 0x04000396 RID: 918
			public const string signingTime = "1.2.840.113549.1.9.5";

			// Token: 0x04000397 RID: 919
			public const string countersignature = "1.2.840.113549.1.9.6";
		}

		// Token: 0x0200007C RID: 124
		public class ContentInfo
		{
			// Token: 0x060004B9 RID: 1209 RVA: 0x000180C7 File Offset: 0x000162C7
			public ContentInfo()
			{
				this.content = new ASN1(160);
			}

			// Token: 0x060004BA RID: 1210 RVA: 0x000180DF File Offset: 0x000162DF
			public ContentInfo(string oid) : this()
			{
				this.contentType = oid;
			}

			// Token: 0x060004BB RID: 1211 RVA: 0x000180EE File Offset: 0x000162EE
			public ContentInfo(byte[] data) : this(new ASN1(data))
			{
			}

			// Token: 0x060004BC RID: 1212 RVA: 0x000180FC File Offset: 0x000162FC
			public ContentInfo(ASN1 asn1)
			{
				if (asn1.Tag != 48 || (asn1.Count < 1 && asn1.Count > 2))
				{
					throw new ArgumentException("Invalid ASN1");
				}
				if (asn1[0].Tag != 6)
				{
					throw new ArgumentException("Invalid contentType");
				}
				this.contentType = ASN1Convert.ToOid(asn1[0]);
				if (asn1.Count > 1)
				{
					if (asn1[1].Tag != 160)
					{
						throw new ArgumentException("Invalid content");
					}
					this.content = asn1[1];
				}
			}

			// Token: 0x17000130 RID: 304
			// (get) Token: 0x060004BD RID: 1213 RVA: 0x00018196 File Offset: 0x00016396
			public ASN1 ASN1
			{
				get
				{
					return this.GetASN1();
				}
			}

			// Token: 0x17000131 RID: 305
			// (get) Token: 0x060004BE RID: 1214 RVA: 0x0001819E File Offset: 0x0001639E
			// (set) Token: 0x060004BF RID: 1215 RVA: 0x000181A6 File Offset: 0x000163A6
			public ASN1 Content
			{
				get
				{
					return this.content;
				}
				set
				{
					this.content = value;
				}
			}

			// Token: 0x17000132 RID: 306
			// (get) Token: 0x060004C0 RID: 1216 RVA: 0x000181AF File Offset: 0x000163AF
			// (set) Token: 0x060004C1 RID: 1217 RVA: 0x000181B7 File Offset: 0x000163B7
			public string ContentType
			{
				get
				{
					return this.contentType;
				}
				set
				{
					this.contentType = value;
				}
			}

			// Token: 0x060004C2 RID: 1218 RVA: 0x000181C0 File Offset: 0x000163C0
			internal ASN1 GetASN1()
			{
				ASN1 asn = new ASN1(48);
				asn.Add(ASN1Convert.FromOid(this.contentType));
				if (this.content != null && this.content.Count > 0)
				{
					asn.Add(this.content);
				}
				return asn;
			}

			// Token: 0x060004C3 RID: 1219 RVA: 0x0001820B File Offset: 0x0001640B
			public byte[] GetBytes()
			{
				return this.GetASN1().GetBytes();
			}

			// Token: 0x04000398 RID: 920
			private string contentType;

			// Token: 0x04000399 RID: 921
			private ASN1 content;
		}

		// Token: 0x0200007D RID: 125
		public class EncryptedData
		{
			// Token: 0x060004C4 RID: 1220 RVA: 0x00018218 File Offset: 0x00016418
			public EncryptedData()
			{
				this._version = 0;
			}

			// Token: 0x060004C5 RID: 1221 RVA: 0x00018227 File Offset: 0x00016427
			public EncryptedData(byte[] data) : this(new ASN1(data))
			{
			}

			// Token: 0x060004C6 RID: 1222 RVA: 0x00018238 File Offset: 0x00016438
			public EncryptedData(ASN1 asn1) : this()
			{
				if (asn1.Tag != 48 || asn1.Count < 2)
				{
					throw new ArgumentException("Invalid EncryptedData");
				}
				if (asn1[0].Tag != 2)
				{
					throw new ArgumentException("Invalid version");
				}
				this._version = asn1[0].Value[0];
				ASN1 asn2 = asn1[1];
				if (asn2.Tag != 48)
				{
					throw new ArgumentException("missing EncryptedContentInfo");
				}
				ASN1 asn3 = asn2[0];
				if (asn3.Tag != 6)
				{
					throw new ArgumentException("missing EncryptedContentInfo.ContentType");
				}
				this._content = new PKCS7.ContentInfo(ASN1Convert.ToOid(asn3));
				ASN1 asn4 = asn2[1];
				if (asn4.Tag != 48)
				{
					throw new ArgumentException("missing EncryptedContentInfo.ContentEncryptionAlgorithmIdentifier");
				}
				this._encryptionAlgorithm = new PKCS7.ContentInfo(ASN1Convert.ToOid(asn4[0]));
				this._encryptionAlgorithm.Content = asn4[1];
				ASN1 asn5 = asn2[2];
				if (asn5.Tag != 128)
				{
					throw new ArgumentException("missing EncryptedContentInfo.EncryptedContent");
				}
				this._encrypted = asn5.Value;
			}

			// Token: 0x17000133 RID: 307
			// (get) Token: 0x060004C7 RID: 1223 RVA: 0x00018351 File Offset: 0x00016551
			public ASN1 ASN1
			{
				get
				{
					return this.GetASN1();
				}
			}

			// Token: 0x17000134 RID: 308
			// (get) Token: 0x060004C8 RID: 1224 RVA: 0x00018359 File Offset: 0x00016559
			public PKCS7.ContentInfo ContentInfo
			{
				get
				{
					return this._content;
				}
			}

			// Token: 0x17000135 RID: 309
			// (get) Token: 0x060004C9 RID: 1225 RVA: 0x00018361 File Offset: 0x00016561
			public PKCS7.ContentInfo EncryptionAlgorithm
			{
				get
				{
					return this._encryptionAlgorithm;
				}
			}

			// Token: 0x17000136 RID: 310
			// (get) Token: 0x060004CA RID: 1226 RVA: 0x00018369 File Offset: 0x00016569
			public byte[] EncryptedContent
			{
				get
				{
					if (this._encrypted == null)
					{
						return null;
					}
					return (byte[])this._encrypted.Clone();
				}
			}

			// Token: 0x17000137 RID: 311
			// (get) Token: 0x060004CB RID: 1227 RVA: 0x00018385 File Offset: 0x00016585
			// (set) Token: 0x060004CC RID: 1228 RVA: 0x0001838D File Offset: 0x0001658D
			public byte Version
			{
				get
				{
					return this._version;
				}
				set
				{
					this._version = value;
				}
			}

			// Token: 0x060004CD RID: 1229 RVA: 0x00018396 File Offset: 0x00016596
			internal ASN1 GetASN1()
			{
				return null;
			}

			// Token: 0x060004CE RID: 1230 RVA: 0x00018399 File Offset: 0x00016599
			public byte[] GetBytes()
			{
				return this.GetASN1().GetBytes();
			}

			// Token: 0x0400039A RID: 922
			private byte _version;

			// Token: 0x0400039B RID: 923
			private PKCS7.ContentInfo _content;

			// Token: 0x0400039C RID: 924
			private PKCS7.ContentInfo _encryptionAlgorithm;

			// Token: 0x0400039D RID: 925
			private byte[] _encrypted;
		}

		// Token: 0x0200007E RID: 126
		public class EnvelopedData
		{
			// Token: 0x060004CF RID: 1231 RVA: 0x000183A6 File Offset: 0x000165A6
			public EnvelopedData()
			{
				this._version = 0;
				this._content = new PKCS7.ContentInfo();
				this._encryptionAlgorithm = new PKCS7.ContentInfo();
				this._recipientInfos = new ArrayList();
			}

			// Token: 0x060004D0 RID: 1232 RVA: 0x000183D6 File Offset: 0x000165D6
			public EnvelopedData(byte[] data) : this(new ASN1(data))
			{
			}

			// Token: 0x060004D1 RID: 1233 RVA: 0x000183E4 File Offset: 0x000165E4
			public EnvelopedData(ASN1 asn1) : this()
			{
				if (asn1[0].Tag != 48 || asn1[0].Count < 3)
				{
					throw new ArgumentException("Invalid EnvelopedData");
				}
				if (asn1[0][0].Tag != 2)
				{
					throw new ArgumentException("Invalid version");
				}
				this._version = asn1[0][0].Value[0];
				ASN1 asn2 = asn1[0][1];
				if (asn2.Tag != 49)
				{
					throw new ArgumentException("missing RecipientInfos");
				}
				for (int i = 0; i < asn2.Count; i++)
				{
					ASN1 data = asn2[i];
					this._recipientInfos.Add(new PKCS7.RecipientInfo(data));
				}
				ASN1 asn3 = asn1[0][2];
				if (asn3.Tag != 48)
				{
					throw new ArgumentException("missing EncryptedContentInfo");
				}
				ASN1 asn4 = asn3[0];
				if (asn4.Tag != 6)
				{
					throw new ArgumentException("missing EncryptedContentInfo.ContentType");
				}
				this._content = new PKCS7.ContentInfo(ASN1Convert.ToOid(asn4));
				ASN1 asn5 = asn3[1];
				if (asn5.Tag != 48)
				{
					throw new ArgumentException("missing EncryptedContentInfo.ContentEncryptionAlgorithmIdentifier");
				}
				this._encryptionAlgorithm = new PKCS7.ContentInfo(ASN1Convert.ToOid(asn5[0]));
				this._encryptionAlgorithm.Content = asn5[1];
				ASN1 asn6 = asn3[2];
				if (asn6.Tag != 128)
				{
					throw new ArgumentException("missing EncryptedContentInfo.EncryptedContent");
				}
				this._encrypted = asn6.Value;
			}

			// Token: 0x17000138 RID: 312
			// (get) Token: 0x060004D2 RID: 1234 RVA: 0x00018570 File Offset: 0x00016770
			public ArrayList RecipientInfos
			{
				get
				{
					return this._recipientInfos;
				}
			}

			// Token: 0x17000139 RID: 313
			// (get) Token: 0x060004D3 RID: 1235 RVA: 0x00018578 File Offset: 0x00016778
			public ASN1 ASN1
			{
				get
				{
					return this.GetASN1();
				}
			}

			// Token: 0x1700013A RID: 314
			// (get) Token: 0x060004D4 RID: 1236 RVA: 0x00018580 File Offset: 0x00016780
			public PKCS7.ContentInfo ContentInfo
			{
				get
				{
					return this._content;
				}
			}

			// Token: 0x1700013B RID: 315
			// (get) Token: 0x060004D5 RID: 1237 RVA: 0x00018588 File Offset: 0x00016788
			public PKCS7.ContentInfo EncryptionAlgorithm
			{
				get
				{
					return this._encryptionAlgorithm;
				}
			}

			// Token: 0x1700013C RID: 316
			// (get) Token: 0x060004D6 RID: 1238 RVA: 0x00018590 File Offset: 0x00016790
			public byte[] EncryptedContent
			{
				get
				{
					if (this._encrypted == null)
					{
						return null;
					}
					return (byte[])this._encrypted.Clone();
				}
			}

			// Token: 0x1700013D RID: 317
			// (get) Token: 0x060004D7 RID: 1239 RVA: 0x000185AC File Offset: 0x000167AC
			// (set) Token: 0x060004D8 RID: 1240 RVA: 0x000185B4 File Offset: 0x000167B4
			public byte Version
			{
				get
				{
					return this._version;
				}
				set
				{
					this._version = value;
				}
			}

			// Token: 0x060004D9 RID: 1241 RVA: 0x000185BD File Offset: 0x000167BD
			internal ASN1 GetASN1()
			{
				return new ASN1(48);
			}

			// Token: 0x060004DA RID: 1242 RVA: 0x000185C6 File Offset: 0x000167C6
			public byte[] GetBytes()
			{
				return this.GetASN1().GetBytes();
			}

			// Token: 0x0400039E RID: 926
			private byte _version;

			// Token: 0x0400039F RID: 927
			private PKCS7.ContentInfo _content;

			// Token: 0x040003A0 RID: 928
			private PKCS7.ContentInfo _encryptionAlgorithm;

			// Token: 0x040003A1 RID: 929
			private ArrayList _recipientInfos;

			// Token: 0x040003A2 RID: 930
			private byte[] _encrypted;
		}

		// Token: 0x0200007F RID: 127
		public class RecipientInfo
		{
			// Token: 0x060004DB RID: 1243 RVA: 0x000185D3 File Offset: 0x000167D3
			public RecipientInfo()
			{
			}

			// Token: 0x060004DC RID: 1244 RVA: 0x000185DC File Offset: 0x000167DC
			public RecipientInfo(ASN1 data)
			{
				if (data.Tag != 48)
				{
					throw new ArgumentException("Invalid RecipientInfo");
				}
				ASN1 asn = data[0];
				if (asn.Tag != 2)
				{
					throw new ArgumentException("missing Version");
				}
				this._version = (int)asn.Value[0];
				ASN1 asn2 = data[1];
				if (asn2.Tag == 128 && this._version == 3)
				{
					this._ski = asn2.Value;
				}
				else
				{
					this._issuer = X501.ToString(asn2[0]);
					this._serial = asn2[1].Value;
				}
				ASN1 asn3 = data[2];
				this._oid = ASN1Convert.ToOid(asn3[0]);
				ASN1 asn4 = data[3];
				this._key = asn4.Value;
			}

			// Token: 0x1700013E RID: 318
			// (get) Token: 0x060004DD RID: 1245 RVA: 0x000186AC File Offset: 0x000168AC
			public string Oid
			{
				get
				{
					return this._oid;
				}
			}

			// Token: 0x1700013F RID: 319
			// (get) Token: 0x060004DE RID: 1246 RVA: 0x000186B4 File Offset: 0x000168B4
			public byte[] Key
			{
				get
				{
					if (this._key == null)
					{
						return null;
					}
					return (byte[])this._key.Clone();
				}
			}

			// Token: 0x17000140 RID: 320
			// (get) Token: 0x060004DF RID: 1247 RVA: 0x000186D0 File Offset: 0x000168D0
			public byte[] SubjectKeyIdentifier
			{
				get
				{
					if (this._ski == null)
					{
						return null;
					}
					return (byte[])this._ski.Clone();
				}
			}

			// Token: 0x17000141 RID: 321
			// (get) Token: 0x060004E0 RID: 1248 RVA: 0x000186EC File Offset: 0x000168EC
			public string Issuer
			{
				get
				{
					return this._issuer;
				}
			}

			// Token: 0x17000142 RID: 322
			// (get) Token: 0x060004E1 RID: 1249 RVA: 0x000186F4 File Offset: 0x000168F4
			public byte[] Serial
			{
				get
				{
					if (this._serial == null)
					{
						return null;
					}
					return (byte[])this._serial.Clone();
				}
			}

			// Token: 0x17000143 RID: 323
			// (get) Token: 0x060004E2 RID: 1250 RVA: 0x00018710 File Offset: 0x00016910
			public int Version
			{
				get
				{
					return this._version;
				}
			}

			// Token: 0x040003A3 RID: 931
			private int _version;

			// Token: 0x040003A4 RID: 932
			private string _oid;

			// Token: 0x040003A5 RID: 933
			private byte[] _key;

			// Token: 0x040003A6 RID: 934
			private byte[] _ski;

			// Token: 0x040003A7 RID: 935
			private string _issuer;

			// Token: 0x040003A8 RID: 936
			private byte[] _serial;
		}

		// Token: 0x02000080 RID: 128
		public class SignedData
		{
			// Token: 0x060004E3 RID: 1251 RVA: 0x00018718 File Offset: 0x00016918
			public SignedData()
			{
				this.version = 1;
				this.contentInfo = new PKCS7.ContentInfo();
				this.certs = new X509CertificateCollection();
				this.crls = new ArrayList();
				this.signerInfo = new PKCS7.SignerInfo();
				this.mda = true;
				this.signed = false;
			}

			// Token: 0x060004E4 RID: 1252 RVA: 0x0001876C File Offset: 0x0001696C
			public SignedData(byte[] data) : this(new ASN1(data))
			{
			}

			// Token: 0x060004E5 RID: 1253 RVA: 0x0001877C File Offset: 0x0001697C
			public SignedData(ASN1 asn1)
			{
				if (asn1[0].Tag != 48 || asn1[0].Count < 4)
				{
					throw new ArgumentException("Invalid SignedData");
				}
				if (asn1[0][0].Tag != 2)
				{
					throw new ArgumentException("Invalid version");
				}
				this.version = asn1[0][0].Value[0];
				this.contentInfo = new PKCS7.ContentInfo(asn1[0][2]);
				int num = 3;
				this.certs = new X509CertificateCollection();
				if (asn1[0][num].Tag == 160)
				{
					for (int i = 0; i < asn1[0][num].Count; i++)
					{
						this.certs.Add(new X509Certificate(asn1[0][num][i].GetBytes()));
					}
					num++;
				}
				this.crls = new ArrayList();
				if (asn1[0][num].Tag == 161)
				{
					for (int j = 0; j < asn1[0][num].Count; j++)
					{
						this.crls.Add(asn1[0][num][j].GetBytes());
					}
					num++;
				}
				if (asn1[0][num].Count > 0)
				{
					this.signerInfo = new PKCS7.SignerInfo(asn1[0][num]);
				}
				else
				{
					this.signerInfo = new PKCS7.SignerInfo();
				}
				if (this.signerInfo.HashName != null)
				{
					this.HashName = this.OidToName(this.signerInfo.HashName);
				}
				this.mda = (this.signerInfo.AuthenticatedAttributes.Count > 0);
			}

			// Token: 0x17000144 RID: 324
			// (get) Token: 0x060004E6 RID: 1254 RVA: 0x0001895B File Offset: 0x00016B5B
			public ASN1 ASN1
			{
				get
				{
					return this.GetASN1();
				}
			}

			// Token: 0x17000145 RID: 325
			// (get) Token: 0x060004E7 RID: 1255 RVA: 0x00018963 File Offset: 0x00016B63
			public X509CertificateCollection Certificates
			{
				get
				{
					return this.certs;
				}
			}

			// Token: 0x17000146 RID: 326
			// (get) Token: 0x060004E8 RID: 1256 RVA: 0x0001896B File Offset: 0x00016B6B
			public PKCS7.ContentInfo ContentInfo
			{
				get
				{
					return this.contentInfo;
				}
			}

			// Token: 0x17000147 RID: 327
			// (get) Token: 0x060004E9 RID: 1257 RVA: 0x00018973 File Offset: 0x00016B73
			public ArrayList Crls
			{
				get
				{
					return this.crls;
				}
			}

			// Token: 0x17000148 RID: 328
			// (get) Token: 0x060004EA RID: 1258 RVA: 0x0001897B File Offset: 0x00016B7B
			// (set) Token: 0x060004EB RID: 1259 RVA: 0x00018983 File Offset: 0x00016B83
			public string HashName
			{
				get
				{
					return this.hashAlgorithm;
				}
				set
				{
					this.hashAlgorithm = value;
					this.signerInfo.HashName = value;
				}
			}

			// Token: 0x17000149 RID: 329
			// (get) Token: 0x060004EC RID: 1260 RVA: 0x00018998 File Offset: 0x00016B98
			public PKCS7.SignerInfo SignerInfo
			{
				get
				{
					return this.signerInfo;
				}
			}

			// Token: 0x1700014A RID: 330
			// (get) Token: 0x060004ED RID: 1261 RVA: 0x000189A0 File Offset: 0x00016BA0
			// (set) Token: 0x060004EE RID: 1262 RVA: 0x000189A8 File Offset: 0x00016BA8
			public byte Version
			{
				get
				{
					return this.version;
				}
				set
				{
					this.version = value;
				}
			}

			// Token: 0x1700014B RID: 331
			// (get) Token: 0x060004EF RID: 1263 RVA: 0x000189B1 File Offset: 0x00016BB1
			// (set) Token: 0x060004F0 RID: 1264 RVA: 0x000189B9 File Offset: 0x00016BB9
			public bool UseAuthenticatedAttributes
			{
				get
				{
					return this.mda;
				}
				set
				{
					this.mda = value;
				}
			}

			// Token: 0x060004F1 RID: 1265 RVA: 0x000189C4 File Offset: 0x00016BC4
			public bool VerifySignature(AsymmetricAlgorithm aa)
			{
				if (aa == null)
				{
					return false;
				}
				RSAPKCS1SignatureDeformatter rsapkcs1SignatureDeformatter = new RSAPKCS1SignatureDeformatter(aa);
				rsapkcs1SignatureDeformatter.SetHashAlgorithm(this.hashAlgorithm);
				HashAlgorithm hashAlgorithm = HashAlgorithm.Create(this.hashAlgorithm);
				byte[] signature = this.signerInfo.Signature;
				byte[] array;
				if (this.mda)
				{
					ASN1 asn = new ASN1(49);
					foreach (object obj in this.signerInfo.AuthenticatedAttributes)
					{
						ASN1 asn2 = (ASN1)obj;
						asn.Add(asn2);
					}
					array = hashAlgorithm.ComputeHash(asn.GetBytes());
				}
				else
				{
					array = hashAlgorithm.ComputeHash(this.contentInfo.Content[0].Value);
				}
				return array != null && signature != null && rsapkcs1SignatureDeformatter.VerifySignature(array, signature);
			}

			// Token: 0x060004F2 RID: 1266 RVA: 0x00018AB0 File Offset: 0x00016CB0
			internal string OidToName(string oid)
			{
				if (oid == "1.3.14.3.2.26")
				{
					return "SHA1";
				}
				if (oid == "1.2.840.113549.2.2")
				{
					return "MD2";
				}
				if (oid == "1.2.840.113549.2.5")
				{
					return "MD5";
				}
				if (oid == "2.16.840.1.101.3.4.1")
				{
					return "SHA256";
				}
				if (oid == "2.16.840.1.101.3.4.2")
				{
					return "SHA384";
				}
				if (!(oid == "2.16.840.1.101.3.4.3"))
				{
					return oid;
				}
				return "SHA512";
			}

			// Token: 0x060004F3 RID: 1267 RVA: 0x00018B34 File Offset: 0x00016D34
			internal ASN1 GetASN1()
			{
				ASN1 asn = new ASN1(48);
				byte[] data = new byte[]
				{
					this.version
				};
				asn.Add(new ASN1(2, data));
				ASN1 asn2 = asn.Add(new ASN1(49));
				if (this.hashAlgorithm != null)
				{
					string oid = CryptoConfig.MapNameToOID(this.hashAlgorithm);
					asn2.Add(PKCS7.AlgorithmIdentifier(oid));
				}
				ASN1 asn3 = this.contentInfo.ASN1;
				asn.Add(asn3);
				if (!this.signed && this.hashAlgorithm != null)
				{
					if (this.mda)
					{
						ASN1 value = PKCS7.Attribute("1.2.840.113549.1.9.3", asn3[0]);
						this.signerInfo.AuthenticatedAttributes.Add(value);
						byte[] data2 = HashAlgorithm.Create(this.hashAlgorithm).ComputeHash(asn3[1][0].Value);
						ASN1 asn4 = new ASN1(48);
						ASN1 value2 = PKCS7.Attribute("1.2.840.113549.1.9.4", asn4.Add(new ASN1(4, data2)));
						this.signerInfo.AuthenticatedAttributes.Add(value2);
					}
					else
					{
						RSAPKCS1SignatureFormatter rsapkcs1SignatureFormatter = new RSAPKCS1SignatureFormatter(this.signerInfo.Key);
						rsapkcs1SignatureFormatter.SetHashAlgorithm(this.hashAlgorithm);
						byte[] rgbHash = HashAlgorithm.Create(this.hashAlgorithm).ComputeHash(asn3[1][0].Value);
						this.signerInfo.Signature = rsapkcs1SignatureFormatter.CreateSignature(rgbHash);
					}
					this.signed = true;
				}
				if (this.certs.Count > 0)
				{
					ASN1 asn5 = asn.Add(new ASN1(160));
					foreach (X509Certificate x509Certificate in this.certs)
					{
						asn5.Add(new ASN1(x509Certificate.RawData));
					}
				}
				if (this.crls.Count > 0)
				{
					ASN1 asn6 = asn.Add(new ASN1(161));
					foreach (object obj in this.crls)
					{
						byte[] data3 = (byte[])obj;
						asn6.Add(new ASN1(data3));
					}
				}
				ASN1 asn7 = asn.Add(new ASN1(49));
				if (this.signerInfo.Key != null)
				{
					asn7.Add(this.signerInfo.ASN1);
				}
				return asn;
			}

			// Token: 0x060004F4 RID: 1268 RVA: 0x00018DD4 File Offset: 0x00016FD4
			public byte[] GetBytes()
			{
				return this.GetASN1().GetBytes();
			}

			// Token: 0x040003A9 RID: 937
			private byte version;

			// Token: 0x040003AA RID: 938
			private string hashAlgorithm;

			// Token: 0x040003AB RID: 939
			private PKCS7.ContentInfo contentInfo;

			// Token: 0x040003AC RID: 940
			private X509CertificateCollection certs;

			// Token: 0x040003AD RID: 941
			private ArrayList crls;

			// Token: 0x040003AE RID: 942
			private PKCS7.SignerInfo signerInfo;

			// Token: 0x040003AF RID: 943
			private bool mda;

			// Token: 0x040003B0 RID: 944
			private bool signed;
		}

		// Token: 0x02000081 RID: 129
		public class SignerInfo
		{
			// Token: 0x060004F5 RID: 1269 RVA: 0x00018DE1 File Offset: 0x00016FE1
			public SignerInfo()
			{
				this.version = 1;
				this.authenticatedAttributes = new ArrayList();
				this.unauthenticatedAttributes = new ArrayList();
			}

			// Token: 0x060004F6 RID: 1270 RVA: 0x00018E06 File Offset: 0x00017006
			public SignerInfo(byte[] data) : this(new ASN1(data))
			{
			}

			// Token: 0x060004F7 RID: 1271 RVA: 0x00018E14 File Offset: 0x00017014
			public SignerInfo(ASN1 asn1) : this()
			{
				if (asn1[0].Tag != 48 || asn1[0].Count < 5)
				{
					throw new ArgumentException("Invalid SignedData");
				}
				if (asn1[0][0].Tag != 2)
				{
					throw new ArgumentException("Invalid version");
				}
				this.version = asn1[0][0].Value[0];
				ASN1 asn2 = asn1[0][1];
				if (asn2.Tag == 128 && this.version == 3)
				{
					this.ski = asn2.Value;
				}
				else
				{
					this.issuer = X501.ToString(asn2[0]);
					this.serial = asn2[1].Value;
				}
				ASN1 asn3 = asn1[0][2];
				this.hashAlgorithm = ASN1Convert.ToOid(asn3[0]);
				int num = 3;
				ASN1 asn4 = asn1[0][num];
				if (asn4.Tag == 160)
				{
					num++;
					for (int i = 0; i < asn4.Count; i++)
					{
						this.authenticatedAttributes.Add(asn4[i]);
					}
				}
				num++;
				ASN1 asn5 = asn1[0][num++];
				if (asn5.Tag == 4)
				{
					this.signature = asn5.Value;
				}
				ASN1 asn6 = asn1[0][num];
				if (asn6 != null && asn6.Tag == 161)
				{
					for (int j = 0; j < asn6.Count; j++)
					{
						this.unauthenticatedAttributes.Add(asn6[j]);
					}
				}
			}

			// Token: 0x1700014C RID: 332
			// (get) Token: 0x060004F8 RID: 1272 RVA: 0x00018FC5 File Offset: 0x000171C5
			public string IssuerName
			{
				get
				{
					return this.issuer;
				}
			}

			// Token: 0x1700014D RID: 333
			// (get) Token: 0x060004F9 RID: 1273 RVA: 0x00018FCD File Offset: 0x000171CD
			public byte[] SerialNumber
			{
				get
				{
					if (this.serial == null)
					{
						return null;
					}
					return (byte[])this.serial.Clone();
				}
			}

			// Token: 0x1700014E RID: 334
			// (get) Token: 0x060004FA RID: 1274 RVA: 0x00018FE9 File Offset: 0x000171E9
			public byte[] SubjectKeyIdentifier
			{
				get
				{
					if (this.ski == null)
					{
						return null;
					}
					return (byte[])this.ski.Clone();
				}
			}

			// Token: 0x1700014F RID: 335
			// (get) Token: 0x060004FB RID: 1275 RVA: 0x00019005 File Offset: 0x00017205
			public ASN1 ASN1
			{
				get
				{
					return this.GetASN1();
				}
			}

			// Token: 0x17000150 RID: 336
			// (get) Token: 0x060004FC RID: 1276 RVA: 0x0001900D File Offset: 0x0001720D
			public ArrayList AuthenticatedAttributes
			{
				get
				{
					return this.authenticatedAttributes;
				}
			}

			// Token: 0x17000151 RID: 337
			// (get) Token: 0x060004FD RID: 1277 RVA: 0x00019015 File Offset: 0x00017215
			// (set) Token: 0x060004FE RID: 1278 RVA: 0x0001901D File Offset: 0x0001721D
			public X509Certificate Certificate
			{
				get
				{
					return this.x509;
				}
				set
				{
					this.x509 = value;
				}
			}

			// Token: 0x17000152 RID: 338
			// (get) Token: 0x060004FF RID: 1279 RVA: 0x00019026 File Offset: 0x00017226
			// (set) Token: 0x06000500 RID: 1280 RVA: 0x0001902E File Offset: 0x0001722E
			public string HashName
			{
				get
				{
					return this.hashAlgorithm;
				}
				set
				{
					this.hashAlgorithm = value;
				}
			}

			// Token: 0x17000153 RID: 339
			// (get) Token: 0x06000501 RID: 1281 RVA: 0x00019037 File Offset: 0x00017237
			// (set) Token: 0x06000502 RID: 1282 RVA: 0x0001903F File Offset: 0x0001723F
			public AsymmetricAlgorithm Key
			{
				get
				{
					return this.key;
				}
				set
				{
					this.key = value;
				}
			}

			// Token: 0x17000154 RID: 340
			// (get) Token: 0x06000503 RID: 1283 RVA: 0x00019048 File Offset: 0x00017248
			// (set) Token: 0x06000504 RID: 1284 RVA: 0x00019064 File Offset: 0x00017264
			public byte[] Signature
			{
				get
				{
					if (this.signature == null)
					{
						return null;
					}
					return (byte[])this.signature.Clone();
				}
				set
				{
					if (value != null)
					{
						this.signature = (byte[])value.Clone();
					}
				}
			}

			// Token: 0x17000155 RID: 341
			// (get) Token: 0x06000505 RID: 1285 RVA: 0x0001907A File Offset: 0x0001727A
			public ArrayList UnauthenticatedAttributes
			{
				get
				{
					return this.unauthenticatedAttributes;
				}
			}

			// Token: 0x17000156 RID: 342
			// (get) Token: 0x06000506 RID: 1286 RVA: 0x00019082 File Offset: 0x00017282
			// (set) Token: 0x06000507 RID: 1287 RVA: 0x0001908A File Offset: 0x0001728A
			public byte Version
			{
				get
				{
					return this.version;
				}
				set
				{
					this.version = value;
				}
			}

			// Token: 0x06000508 RID: 1288 RVA: 0x00019094 File Offset: 0x00017294
			internal ASN1 GetASN1()
			{
				if (this.key == null || this.hashAlgorithm == null)
				{
					return null;
				}
				byte[] data = new byte[]
				{
					this.version
				};
				ASN1 asn = new ASN1(48);
				asn.Add(new ASN1(2, data));
				asn.Add(PKCS7.IssuerAndSerialNumber(this.x509));
				string oid = CryptoConfig.MapNameToOID(this.hashAlgorithm);
				asn.Add(PKCS7.AlgorithmIdentifier(oid));
				ASN1 asn2 = null;
				if (this.authenticatedAttributes.Count > 0)
				{
					asn2 = asn.Add(new ASN1(160));
					this.authenticatedAttributes.Sort(new PKCS7.SortedSet());
					foreach (object obj in this.authenticatedAttributes)
					{
						ASN1 asn3 = (ASN1)obj;
						asn2.Add(asn3);
					}
				}
				if (this.key is RSA)
				{
					asn.Add(PKCS7.AlgorithmIdentifier("1.2.840.113549.1.1.1"));
					if (asn2 != null)
					{
						RSAPKCS1SignatureFormatter rsapkcs1SignatureFormatter = new RSAPKCS1SignatureFormatter(this.key);
						rsapkcs1SignatureFormatter.SetHashAlgorithm(this.hashAlgorithm);
						byte[] bytes = asn2.GetBytes();
						bytes[0] = 49;
						byte[] rgbHash = HashAlgorithm.Create(this.hashAlgorithm).ComputeHash(bytes);
						this.signature = rsapkcs1SignatureFormatter.CreateSignature(rgbHash);
					}
					asn.Add(new ASN1(4, this.signature));
					if (this.unauthenticatedAttributes.Count > 0)
					{
						ASN1 asn4 = asn.Add(new ASN1(161));
						this.unauthenticatedAttributes.Sort(new PKCS7.SortedSet());
						foreach (object obj2 in this.unauthenticatedAttributes)
						{
							ASN1 asn5 = (ASN1)obj2;
							asn4.Add(asn5);
						}
					}
					return asn;
				}
				if (this.key is DSA)
				{
					throw new NotImplementedException("not yet");
				}
				throw new CryptographicException("Unknown assymetric algorithm");
			}

			// Token: 0x06000509 RID: 1289 RVA: 0x000192B4 File Offset: 0x000174B4
			public byte[] GetBytes()
			{
				return this.GetASN1().GetBytes();
			}

			// Token: 0x040003B1 RID: 945
			private byte version;

			// Token: 0x040003B2 RID: 946
			private X509Certificate x509;

			// Token: 0x040003B3 RID: 947
			private string hashAlgorithm;

			// Token: 0x040003B4 RID: 948
			private AsymmetricAlgorithm key;

			// Token: 0x040003B5 RID: 949
			private ArrayList authenticatedAttributes;

			// Token: 0x040003B6 RID: 950
			private ArrayList unauthenticatedAttributes;

			// Token: 0x040003B7 RID: 951
			private byte[] signature;

			// Token: 0x040003B8 RID: 952
			private string issuer;

			// Token: 0x040003B9 RID: 953
			private byte[] serial;

			// Token: 0x040003BA RID: 954
			private byte[] ski;
		}

		// Token: 0x02000082 RID: 130
		internal class SortedSet : IComparer
		{
			// Token: 0x0600050A RID: 1290 RVA: 0x000192C4 File Offset: 0x000174C4
			public int Compare(object x, object y)
			{
				if (x == null)
				{
					if (y != null)
					{
						return -1;
					}
					return 0;
				}
				else
				{
					if (y == null)
					{
						return 1;
					}
					ASN1 asn = x as ASN1;
					ASN1 asn2 = y as ASN1;
					if (asn == null || asn2 == null)
					{
						throw new ArgumentException(Locale.GetText("Invalid objects."));
					}
					byte[] bytes = asn.GetBytes();
					byte[] bytes2 = asn2.GetBytes();
					int num = 0;
					while (num < bytes.Length && num != bytes2.Length)
					{
						if (bytes[num] != bytes2[num])
						{
							if (bytes[num] >= bytes2[num])
							{
								return 1;
							}
							return -1;
						}
						else
						{
							num++;
						}
					}
					if (bytes.Length > bytes2.Length)
					{
						return 1;
					}
					if (bytes.Length < bytes2.Length)
					{
						return -1;
					}
					return 0;
				}
			}

			// Token: 0x0600050B RID: 1291 RVA: 0x0001934D File Offset: 0x0001754D
			public SortedSet()
			{
			}
		}
	}
}

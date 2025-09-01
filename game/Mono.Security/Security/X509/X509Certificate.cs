using System;
using System.Runtime.Serialization;
using System.Security.Cryptography;
using System.Security.Permissions;
using System.Text;
using Mono.Security.Cryptography;

namespace Mono.Security.X509
{
	// Token: 0x02000013 RID: 19
	public class X509Certificate : ISerializable
	{
		// Token: 0x060000D2 RID: 210 RVA: 0x00008E88 File Offset: 0x00007088
		private void Parse(byte[] data)
		{
			try
			{
				this.decoder = new ASN1(data);
				if (this.decoder.Tag != 48)
				{
					throw new CryptographicException(X509Certificate.encoding_error);
				}
				if (this.decoder[0].Tag != 48)
				{
					throw new CryptographicException(X509Certificate.encoding_error);
				}
				ASN1 asn = this.decoder[0];
				int num = 0;
				ASN1 asn2 = this.decoder[0][num];
				this.version = 1;
				if (asn2.Tag == 160 && asn2.Count > 0)
				{
					this.version += (int)asn2[0].Value[0];
					num++;
				}
				ASN1 asn3 = this.decoder[0][num++];
				if (asn3.Tag != 2)
				{
					throw new CryptographicException(X509Certificate.encoding_error);
				}
				this.serialnumber = asn3.Value;
				Array.Reverse<byte>(this.serialnumber, 0, this.serialnumber.Length);
				num++;
				this.issuer = asn.Element(num++, 48);
				this.m_issuername = X501.ToString(this.issuer);
				ASN1 asn4 = asn.Element(num++, 48);
				ASN1 time = asn4[0];
				this.m_from = ASN1Convert.ToDateTime(time);
				ASN1 time2 = asn4[1];
				this.m_until = ASN1Convert.ToDateTime(time2);
				this.subject = asn.Element(num++, 48);
				this.m_subject = X501.ToString(this.subject);
				ASN1 asn5 = asn.Element(num++, 48);
				ASN1 asn6 = asn5.Element(0, 48);
				ASN1 asn7 = asn6.Element(0, 6);
				this.m_keyalgo = ASN1Convert.ToOid(asn7);
				ASN1 asn8 = asn6[1];
				this.m_keyalgoparams = ((asn6.Count > 1) ? asn8.GetBytes() : null);
				ASN1 asn9 = asn5.Element(1, 3);
				int num2 = asn9.Length - 1;
				this.m_publickey = new byte[num2];
				Buffer.BlockCopy(asn9.Value, 1, this.m_publickey, 0, num2);
				byte[] value = this.decoder[2].Value;
				this.signature = new byte[value.Length - 1];
				Buffer.BlockCopy(value, 1, this.signature, 0, this.signature.Length);
				asn6 = this.decoder[1];
				asn7 = asn6.Element(0, 6);
				this.m_signaturealgo = ASN1Convert.ToOid(asn7);
				asn8 = asn6[1];
				if (asn8 != null)
				{
					this.m_signaturealgoparams = asn8.GetBytes();
				}
				else
				{
					this.m_signaturealgoparams = null;
				}
				ASN1 asn10 = asn.Element(num, 129);
				if (asn10 != null)
				{
					num++;
					this.issuerUniqueID = asn10.Value;
				}
				ASN1 asn11 = asn.Element(num, 130);
				if (asn11 != null)
				{
					num++;
					this.subjectUniqueID = asn11.Value;
				}
				ASN1 asn12 = asn.Element(num, 163);
				if (asn12 != null && asn12.Count == 1)
				{
					this.extensions = new X509ExtensionCollection(asn12[0]);
				}
				else
				{
					this.extensions = new X509ExtensionCollection(null);
				}
				this.m_encodedcert = (byte[])data.Clone();
			}
			catch (Exception inner)
			{
				throw new CryptographicException(X509Certificate.encoding_error, inner);
			}
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x000091D4 File Offset: 0x000073D4
		public X509Certificate(byte[] data)
		{
			if (data != null)
			{
				if (data.Length != 0 && data[0] != 48)
				{
					try
					{
						data = X509Certificate.PEM("CERTIFICATE", data);
					}
					catch (Exception inner)
					{
						throw new CryptographicException(X509Certificate.encoding_error, inner);
					}
				}
				this.Parse(data);
			}
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x00009228 File Offset: 0x00007428
		private byte[] GetUnsignedBigInteger(byte[] integer)
		{
			if (integer[0] == 0)
			{
				int num = integer.Length - 1;
				byte[] array = new byte[num];
				Buffer.BlockCopy(integer, 1, array, 0, num);
				return array;
			}
			return integer;
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x060000D5 RID: 213 RVA: 0x00009254 File Offset: 0x00007454
		// (set) Token: 0x060000D6 RID: 214 RVA: 0x0000938E File Offset: 0x0000758E
		public DSA DSA
		{
			get
			{
				if (this.m_keyalgoparams == null)
				{
					throw new CryptographicException("Missing key algorithm parameters.");
				}
				if (this._dsa == null && this.m_keyalgo == "1.2.840.10040.4.1")
				{
					DSAParameters dsaparameters = default(DSAParameters);
					ASN1 asn = new ASN1(this.m_publickey);
					if (asn == null || asn.Tag != 2)
					{
						return null;
					}
					dsaparameters.Y = this.GetUnsignedBigInteger(asn.Value);
					ASN1 asn2 = new ASN1(this.m_keyalgoparams);
					if (asn2 == null || asn2.Tag != 48 || asn2.Count < 3)
					{
						return null;
					}
					if (asn2[0].Tag != 2 || asn2[1].Tag != 2 || asn2[2].Tag != 2)
					{
						return null;
					}
					dsaparameters.P = this.GetUnsignedBigInteger(asn2[0].Value);
					dsaparameters.Q = this.GetUnsignedBigInteger(asn2[1].Value);
					dsaparameters.G = this.GetUnsignedBigInteger(asn2[2].Value);
					this._dsa = new DSACryptoServiceProvider(dsaparameters.Y.Length << 3);
					this._dsa.ImportParameters(dsaparameters);
				}
				return this._dsa;
			}
			set
			{
				this._dsa = value;
				if (value != null)
				{
					this._rsa = null;
				}
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x060000D7 RID: 215 RVA: 0x000093A1 File Offset: 0x000075A1
		public X509ExtensionCollection Extensions
		{
			get
			{
				return this.extensions;
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x060000D8 RID: 216 RVA: 0x000093AC File Offset: 0x000075AC
		public byte[] Hash
		{
			get
			{
				if (this.certhash == null)
				{
					if (this.decoder == null || this.decoder.Count < 1)
					{
						return null;
					}
					string text = PKCS1.HashNameFromOid(this.m_signaturealgo, false);
					if (text == null)
					{
						return null;
					}
					byte[] bytes = this.decoder[0].GetBytes();
					using (HashAlgorithm hashAlgorithm = PKCS1.CreateFromName(text))
					{
						this.certhash = hashAlgorithm.ComputeHash(bytes, 0, bytes.Length);
					}
				}
				return (byte[])this.certhash.Clone();
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x060000D9 RID: 217 RVA: 0x00009444 File Offset: 0x00007644
		public virtual string IssuerName
		{
			get
			{
				return this.m_issuername;
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x060000DA RID: 218 RVA: 0x0000944C File Offset: 0x0000764C
		public virtual string KeyAlgorithm
		{
			get
			{
				return this.m_keyalgo;
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x060000DB RID: 219 RVA: 0x00009454 File Offset: 0x00007654
		// (set) Token: 0x060000DC RID: 220 RVA: 0x00009470 File Offset: 0x00007670
		public virtual byte[] KeyAlgorithmParameters
		{
			get
			{
				if (this.m_keyalgoparams == null)
				{
					return null;
				}
				return (byte[])this.m_keyalgoparams.Clone();
			}
			set
			{
				this.m_keyalgoparams = value;
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x060000DD RID: 221 RVA: 0x00009479 File Offset: 0x00007679
		public virtual byte[] PublicKey
		{
			get
			{
				if (this.m_publickey == null)
				{
					return null;
				}
				return (byte[])this.m_publickey.Clone();
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x060000DE RID: 222 RVA: 0x00009498 File Offset: 0x00007698
		// (set) Token: 0x060000DF RID: 223 RVA: 0x0000954D File Offset: 0x0000774D
		public virtual RSA RSA
		{
			get
			{
				if (this._rsa == null && this.m_keyalgo == "1.2.840.113549.1.1.1")
				{
					RSAParameters rsaparameters = default(RSAParameters);
					ASN1 asn = new ASN1(this.m_publickey);
					ASN1 asn2 = asn[0];
					if (asn2 == null || asn2.Tag != 2)
					{
						return null;
					}
					ASN1 asn3 = asn[1];
					if (asn3.Tag != 2)
					{
						return null;
					}
					rsaparameters.Modulus = this.GetUnsignedBigInteger(asn2.Value);
					rsaparameters.Exponent = asn3.Value;
					int dwKeySize = rsaparameters.Modulus.Length << 3;
					this._rsa = new RSACryptoServiceProvider(dwKeySize);
					this._rsa.ImportParameters(rsaparameters);
				}
				return this._rsa;
			}
			set
			{
				if (value != null)
				{
					this._dsa = null;
				}
				this._rsa = value;
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x060000E0 RID: 224 RVA: 0x00009560 File Offset: 0x00007760
		public virtual byte[] RawData
		{
			get
			{
				if (this.m_encodedcert == null)
				{
					return null;
				}
				return (byte[])this.m_encodedcert.Clone();
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x060000E1 RID: 225 RVA: 0x0000957C File Offset: 0x0000777C
		public virtual byte[] SerialNumber
		{
			get
			{
				if (this.serialnumber == null)
				{
					return null;
				}
				return (byte[])this.serialnumber.Clone();
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x060000E2 RID: 226 RVA: 0x00009598 File Offset: 0x00007798
		public virtual byte[] Signature
		{
			get
			{
				if (this.signature == null)
				{
					return null;
				}
				string signaturealgo = this.m_signaturealgo;
				uint num = <PrivateImplementationDetails>.ComputeStringHash(signaturealgo);
				if (num <= 719034781U)
				{
					if (num <= 601591448U)
					{
						if (num != 510574318U)
						{
							if (num != 601591448U)
							{
								goto IL_22D;
							}
							if (!(signaturealgo == "1.2.840.113549.1.1.5"))
							{
								goto IL_22D;
							}
						}
						else
						{
							if (!(signaturealgo == "1.2.840.10040.4.3"))
							{
								goto IL_22D;
							}
							ASN1 asn = new ASN1(this.signature);
							if (asn == null || asn.Count != 2)
							{
								return null;
							}
							byte[] value = asn[0].Value;
							byte[] value2 = asn[1].Value;
							byte[] array = new byte[40];
							int num2 = Math.Max(0, value.Length - 20);
							int dstOffset = Math.Max(0, 20 - value.Length);
							Buffer.BlockCopy(value, num2, array, dstOffset, value.Length - num2);
							int num3 = Math.Max(0, value2.Length - 20);
							int dstOffset2 = Math.Max(20, 40 - value2.Length);
							Buffer.BlockCopy(value2, num3, array, dstOffset2, value2.Length - num3);
							return array;
						}
					}
					else if (num != 618369067U)
					{
						if (num != 702257162U)
						{
							if (num != 719034781U)
							{
								goto IL_22D;
							}
							if (!(signaturealgo == "1.2.840.113549.1.1.2"))
							{
								goto IL_22D;
							}
						}
						else if (!(signaturealgo == "1.2.840.113549.1.1.3"))
						{
							goto IL_22D;
						}
					}
					else if (!(signaturealgo == "1.2.840.113549.1.1.4"))
					{
						goto IL_22D;
					}
				}
				else if (num <= 2477476687U)
				{
					if (num != 875536856U)
					{
						if (num != 2477476687U)
						{
							goto IL_22D;
						}
						if (!(signaturealgo == "1.2.840.113549.1.1.11"))
						{
							goto IL_22D;
						}
					}
					else if (!(signaturealgo == "1.3.14.3.2.29"))
					{
						goto IL_22D;
					}
				}
				else if (num != 2494254306U)
				{
					if (num != 2511031925U)
					{
						if (num != 3493391575U)
						{
							goto IL_22D;
						}
						if (!(signaturealgo == "1.3.36.3.3.1.2"))
						{
							goto IL_22D;
						}
					}
					else if (!(signaturealgo == "1.2.840.113549.1.1.13"))
					{
						goto IL_22D;
					}
				}
				else if (!(signaturealgo == "1.2.840.113549.1.1.12"))
				{
					goto IL_22D;
				}
				return (byte[])this.signature.Clone();
				IL_22D:
				throw new CryptographicException("Unsupported hash algorithm: " + this.m_signaturealgo);
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x060000E3 RID: 227 RVA: 0x000097E7 File Offset: 0x000079E7
		public virtual string SignatureAlgorithm
		{
			get
			{
				return this.m_signaturealgo;
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x060000E4 RID: 228 RVA: 0x000097EF File Offset: 0x000079EF
		public virtual byte[] SignatureAlgorithmParameters
		{
			get
			{
				if (this.m_signaturealgoparams == null)
				{
					return this.m_signaturealgoparams;
				}
				return (byte[])this.m_signaturealgoparams.Clone();
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x060000E5 RID: 229 RVA: 0x00009810 File Offset: 0x00007A10
		public virtual string SubjectName
		{
			get
			{
				return this.m_subject;
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x060000E6 RID: 230 RVA: 0x00009818 File Offset: 0x00007A18
		public virtual DateTime ValidFrom
		{
			get
			{
				return this.m_from;
			}
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x060000E7 RID: 231 RVA: 0x00009820 File Offset: 0x00007A20
		public virtual DateTime ValidUntil
		{
			get
			{
				return this.m_until;
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x060000E8 RID: 232 RVA: 0x00009828 File Offset: 0x00007A28
		public int Version
		{
			get
			{
				return this.version;
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x060000E9 RID: 233 RVA: 0x00009830 File Offset: 0x00007A30
		public bool IsCurrent
		{
			get
			{
				return this.WasCurrent(DateTime.UtcNow);
			}
		}

		// Token: 0x060000EA RID: 234 RVA: 0x0000983D File Offset: 0x00007A3D
		public bool WasCurrent(DateTime instant)
		{
			return instant > this.ValidFrom && instant <= this.ValidUntil;
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x060000EB RID: 235 RVA: 0x0000985B File Offset: 0x00007A5B
		public byte[] IssuerUniqueIdentifier
		{
			get
			{
				if (this.issuerUniqueID == null)
				{
					return null;
				}
				return (byte[])this.issuerUniqueID.Clone();
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x060000EC RID: 236 RVA: 0x00009877 File Offset: 0x00007A77
		public byte[] SubjectUniqueIdentifier
		{
			get
			{
				if (this.subjectUniqueID == null)
				{
					return null;
				}
				return (byte[])this.subjectUniqueID.Clone();
			}
		}

		// Token: 0x060000ED RID: 237 RVA: 0x00009893 File Offset: 0x00007A93
		internal bool VerifySignature(DSA dsa)
		{
			DSASignatureDeformatter dsasignatureDeformatter = new DSASignatureDeformatter(dsa);
			dsasignatureDeformatter.SetHashAlgorithm("SHA1");
			return dsasignatureDeformatter.VerifySignature(this.Hash, this.Signature);
		}

		// Token: 0x060000EE RID: 238 RVA: 0x000098B7 File Offset: 0x00007AB7
		internal bool VerifySignature(RSA rsa)
		{
			if (this.m_signaturealgo == "1.2.840.10040.4.3")
			{
				return false;
			}
			RSAPKCS1SignatureDeformatter rsapkcs1SignatureDeformatter = new RSAPKCS1SignatureDeformatter(rsa);
			rsapkcs1SignatureDeformatter.SetHashAlgorithm(PKCS1.HashNameFromOid(this.m_signaturealgo, true));
			return rsapkcs1SignatureDeformatter.VerifySignature(this.Hash, this.Signature);
		}

		// Token: 0x060000EF RID: 239 RVA: 0x000098F8 File Offset: 0x00007AF8
		public bool VerifySignature(AsymmetricAlgorithm aa)
		{
			if (aa == null)
			{
				throw new ArgumentNullException("aa");
			}
			if (aa is RSA)
			{
				return this.VerifySignature(aa as RSA);
			}
			if (aa is DSA)
			{
				return this.VerifySignature(aa as DSA);
			}
			throw new NotSupportedException("Unknown Asymmetric Algorithm " + aa.ToString());
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x00009952 File Offset: 0x00007B52
		public bool CheckSignature(byte[] hash, string hashAlgorithm, byte[] signature)
		{
			return ((RSACryptoServiceProvider)this.RSA).VerifyHash(hash, hashAlgorithm, signature);
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x060000F1 RID: 241 RVA: 0x00009968 File Offset: 0x00007B68
		public bool IsSelfSigned
		{
			get
			{
				if (this.m_issuername != this.m_subject)
				{
					return false;
				}
				bool result;
				try
				{
					if (this.RSA != null)
					{
						result = this.VerifySignature(this.RSA);
					}
					else if (this.DSA != null)
					{
						result = this.VerifySignature(this.DSA);
					}
					else
					{
						result = false;
					}
				}
				catch (CryptographicException)
				{
					result = false;
				}
				return result;
			}
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x000099D4 File Offset: 0x00007BD4
		public ASN1 GetIssuerName()
		{
			return this.issuer;
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x000099DC File Offset: 0x00007BDC
		public ASN1 GetSubjectName()
		{
			return this.subject;
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x000099E4 File Offset: 0x00007BE4
		protected X509Certificate(SerializationInfo info, StreamingContext context)
		{
			this.Parse((byte[])info.GetValue("raw", typeof(byte[])));
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x00009A0C File Offset: 0x00007C0C
		[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
		public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue("raw", this.m_encodedcert);
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x00009A20 File Offset: 0x00007C20
		private static byte[] PEM(string type, byte[] data)
		{
			string @string = Encoding.ASCII.GetString(data);
			string text = string.Format("-----BEGIN {0}-----", type);
			string value = string.Format("-----END {0}-----", type);
			int num = @string.IndexOf(text) + text.Length;
			int num2 = @string.IndexOf(value, num);
			return Convert.FromBase64String(@string.Substring(num, num2 - num));
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x00009A76 File Offset: 0x00007C76
		// Note: this type is marked as 'beforefieldinit'.
		static X509Certificate()
		{
		}

		// Token: 0x0400008D RID: 141
		private ASN1 decoder;

		// Token: 0x0400008E RID: 142
		private byte[] m_encodedcert;

		// Token: 0x0400008F RID: 143
		private DateTime m_from;

		// Token: 0x04000090 RID: 144
		private DateTime m_until;

		// Token: 0x04000091 RID: 145
		private ASN1 issuer;

		// Token: 0x04000092 RID: 146
		private string m_issuername;

		// Token: 0x04000093 RID: 147
		private string m_keyalgo;

		// Token: 0x04000094 RID: 148
		private byte[] m_keyalgoparams;

		// Token: 0x04000095 RID: 149
		private ASN1 subject;

		// Token: 0x04000096 RID: 150
		private string m_subject;

		// Token: 0x04000097 RID: 151
		private byte[] m_publickey;

		// Token: 0x04000098 RID: 152
		private byte[] signature;

		// Token: 0x04000099 RID: 153
		private string m_signaturealgo;

		// Token: 0x0400009A RID: 154
		private byte[] m_signaturealgoparams;

		// Token: 0x0400009B RID: 155
		private byte[] certhash;

		// Token: 0x0400009C RID: 156
		private RSA _rsa;

		// Token: 0x0400009D RID: 157
		private DSA _dsa;

		// Token: 0x0400009E RID: 158
		internal const string OID_DSA = "1.2.840.10040.4.1";

		// Token: 0x0400009F RID: 159
		internal const string OID_RSA = "1.2.840.113549.1.1.1";

		// Token: 0x040000A0 RID: 160
		internal const string OID_ECC = "1.2.840.10045.2.1";

		// Token: 0x040000A1 RID: 161
		private int version;

		// Token: 0x040000A2 RID: 162
		private byte[] serialnumber;

		// Token: 0x040000A3 RID: 163
		private byte[] issuerUniqueID;

		// Token: 0x040000A4 RID: 164
		private byte[] subjectUniqueID;

		// Token: 0x040000A5 RID: 165
		private X509ExtensionCollection extensions;

		// Token: 0x040000A6 RID: 166
		private static string encoding_error = Locale.GetText("Input data cannot be coded as a valid certificate.");
	}
}

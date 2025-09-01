using System;
using System.Globalization;
using System.Text;

namespace Mono.Security.X509.Extensions
{
	// Token: 0x02000024 RID: 36
	public class KeyAttributesExtension : X509Extension
	{
		// Token: 0x060001B6 RID: 438 RVA: 0x0000C8AC File Offset: 0x0000AAAC
		public KeyAttributesExtension()
		{
			this.extnOid = "2.5.29.2";
		}

		// Token: 0x060001B7 RID: 439 RVA: 0x0000C8BF File Offset: 0x0000AABF
		public KeyAttributesExtension(ASN1 asn1) : base(asn1)
		{
		}

		// Token: 0x060001B8 RID: 440 RVA: 0x0000C8C8 File Offset: 0x0000AAC8
		public KeyAttributesExtension(X509Extension extension) : base(extension)
		{
		}

		// Token: 0x060001B9 RID: 441 RVA: 0x0000C8D4 File Offset: 0x0000AAD4
		protected override void Decode()
		{
			ASN1 asn = new ASN1(this.extnValue.Value);
			if (asn.Tag != 48)
			{
				throw new ArgumentException("Invalid KeyAttributesExtension extension");
			}
			int num = 0;
			if (num < asn.Count)
			{
				ASN1 asn2 = asn[num];
				if (asn2.Tag == 4)
				{
					num++;
					this.keyId = asn2.Value;
				}
			}
			if (num < asn.Count)
			{
				ASN1 asn3 = asn[num];
				if (asn3.Tag == 3)
				{
					num++;
					int i = 1;
					while (i < asn3.Value.Length)
					{
						this.kubits = (this.kubits << 8) + (int)asn3.Value[i++];
					}
				}
			}
			if (num < asn.Count)
			{
				ASN1 asn4 = asn[num];
				if (asn4.Tag == 48)
				{
					int num2 = 0;
					if (num2 < asn4.Count)
					{
						ASN1 asn5 = asn4[num2];
						if (asn5.Tag == 129)
						{
							num2++;
							this.notBefore = ASN1Convert.ToDateTime(asn5);
						}
					}
					if (num2 < asn4.Count)
					{
						ASN1 asn6 = asn4[num2];
						if (asn6.Tag == 130)
						{
							this.notAfter = ASN1Convert.ToDateTime(asn6);
						}
					}
				}
			}
		}

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x060001BA RID: 442 RVA: 0x0000CA07 File Offset: 0x0000AC07
		public byte[] KeyIdentifier
		{
			get
			{
				if (this.keyId == null)
				{
					return null;
				}
				return (byte[])this.keyId.Clone();
			}
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x060001BB RID: 443 RVA: 0x0000CA23 File Offset: 0x0000AC23
		public override string Name
		{
			get
			{
				return "Key Attributes";
			}
		}

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x060001BC RID: 444 RVA: 0x0000CA2A File Offset: 0x0000AC2A
		public DateTime NotAfter
		{
			get
			{
				return this.notAfter;
			}
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x060001BD RID: 445 RVA: 0x0000CA32 File Offset: 0x0000AC32
		public DateTime NotBefore
		{
			get
			{
				return this.notBefore;
			}
		}

		// Token: 0x060001BE RID: 446 RVA: 0x0000CA3C File Offset: 0x0000AC3C
		public bool Support(KeyUsages usage)
		{
			int num = Convert.ToInt32(usage, CultureInfo.InvariantCulture);
			return (num & this.kubits) == num;
		}

		// Token: 0x060001BF RID: 447 RVA: 0x0000CA68 File Offset: 0x0000AC68
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (this.keyId != null)
			{
				stringBuilder.Append("KeyID=");
				for (int i = 0; i < this.keyId.Length; i++)
				{
					stringBuilder.Append(this.keyId[i].ToString("X2", CultureInfo.InvariantCulture));
					if (i % 2 == 1)
					{
						stringBuilder.Append(" ");
					}
				}
				stringBuilder.Append(Environment.NewLine);
			}
			if (this.kubits != 0)
			{
				stringBuilder.Append("Key Usage=");
				if (this.Support(KeyUsages.digitalSignature))
				{
					stringBuilder.Append("Digital Signature");
				}
				if (this.Support(KeyUsages.nonRepudiation))
				{
					if (stringBuilder.Length > 0)
					{
						stringBuilder.Append(" , ");
					}
					stringBuilder.Append("Non-Repudiation");
				}
				if (this.Support(KeyUsages.keyEncipherment))
				{
					if (stringBuilder.Length > 0)
					{
						stringBuilder.Append(" , ");
					}
					stringBuilder.Append("Key Encipherment");
				}
				if (this.Support(KeyUsages.dataEncipherment))
				{
					if (stringBuilder.Length > 0)
					{
						stringBuilder.Append(" , ");
					}
					stringBuilder.Append("Data Encipherment");
				}
				if (this.Support(KeyUsages.keyAgreement))
				{
					if (stringBuilder.Length > 0)
					{
						stringBuilder.Append(" , ");
					}
					stringBuilder.Append("Key Agreement");
				}
				if (this.Support(KeyUsages.keyCertSign))
				{
					if (stringBuilder.Length > 0)
					{
						stringBuilder.Append(" , ");
					}
					stringBuilder.Append("Certificate Signing");
				}
				if (this.Support(KeyUsages.cRLSign))
				{
					if (stringBuilder.Length > 0)
					{
						stringBuilder.Append(" , ");
					}
					stringBuilder.Append("CRL Signing");
				}
				if (this.Support(KeyUsages.encipherOnly))
				{
					if (stringBuilder.Length > 0)
					{
						stringBuilder.Append(" , ");
					}
					stringBuilder.Append("Encipher Only ");
				}
				if (this.Support(KeyUsages.decipherOnly))
				{
					if (stringBuilder.Length > 0)
					{
						stringBuilder.Append(" , ");
					}
					stringBuilder.Append("Decipher Only");
				}
				stringBuilder.Append("(");
				stringBuilder.Append(this.kubits.ToString("X2", CultureInfo.InvariantCulture));
				stringBuilder.Append(")");
				stringBuilder.Append(Environment.NewLine);
			}
			if (this.notBefore != DateTime.MinValue)
			{
				stringBuilder.Append("Not Before=");
				stringBuilder.Append(this.notBefore.ToString(CultureInfo.CurrentUICulture));
				stringBuilder.Append(Environment.NewLine);
			}
			if (this.notAfter != DateTime.MinValue)
			{
				stringBuilder.Append("Not After=");
				stringBuilder.Append(this.notAfter.ToString(CultureInfo.CurrentUICulture));
				stringBuilder.Append(Environment.NewLine);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x040000E4 RID: 228
		private byte[] keyId;

		// Token: 0x040000E5 RID: 229
		private int kubits;

		// Token: 0x040000E6 RID: 230
		private DateTime notBefore;

		// Token: 0x040000E7 RID: 231
		private DateTime notAfter;
	}
}

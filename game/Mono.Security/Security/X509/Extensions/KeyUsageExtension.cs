using System;
using System.Globalization;
using System.Text;

namespace Mono.Security.X509.Extensions
{
	// Token: 0x02000026 RID: 38
	public class KeyUsageExtension : X509Extension
	{
		// Token: 0x060001C0 RID: 448 RVA: 0x0000CD31 File Offset: 0x0000AF31
		public KeyUsageExtension(ASN1 asn1) : base(asn1)
		{
		}

		// Token: 0x060001C1 RID: 449 RVA: 0x0000CD3A File Offset: 0x0000AF3A
		public KeyUsageExtension(X509Extension extension) : base(extension)
		{
		}

		// Token: 0x060001C2 RID: 450 RVA: 0x0000CD43 File Offset: 0x0000AF43
		public KeyUsageExtension()
		{
			this.extnOid = "2.5.29.15";
		}

		// Token: 0x060001C3 RID: 451 RVA: 0x0000CD58 File Offset: 0x0000AF58
		protected override void Decode()
		{
			ASN1 asn = new ASN1(this.extnValue.Value);
			if (asn.Tag != 3)
			{
				throw new ArgumentException("Invalid KeyUsage extension");
			}
			int i = 1;
			while (i < asn.Value.Length)
			{
				this.kubits = (this.kubits << 8) + (int)asn.Value[i++];
			}
		}

		// Token: 0x060001C4 RID: 452 RVA: 0x0000CDB4 File Offset: 0x0000AFB4
		protected override void Encode()
		{
			this.extnValue = new ASN1(4);
			ushort num = (ushort)this.kubits;
			if (num <= 0)
			{
				ASN1 extnValue = this.extnValue;
				byte tag = 3;
				byte[] array = new byte[2];
				array[0] = 7;
				extnValue.Add(new ASN1(tag, array));
				return;
			}
			byte b = 15;
			while (b > 0 && (num & 32768) != 32768)
			{
				num = (ushort)(num << 1);
				b -= 1;
			}
			if (this.kubits > 255)
			{
				b -= 8;
				this.extnValue.Add(new ASN1(3, new byte[]
				{
					b,
					(byte)this.kubits,
					(byte)(this.kubits >> 8)
				}));
				return;
			}
			this.extnValue.Add(new ASN1(3, new byte[]
			{
				b,
				(byte)this.kubits
			}));
		}

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x060001C5 RID: 453 RVA: 0x0000CE88 File Offset: 0x0000B088
		// (set) Token: 0x060001C6 RID: 454 RVA: 0x0000CE90 File Offset: 0x0000B090
		public KeyUsages KeyUsage
		{
			get
			{
				return (KeyUsages)this.kubits;
			}
			set
			{
				this.kubits = Convert.ToInt32(value, CultureInfo.InvariantCulture);
			}
		}

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x060001C7 RID: 455 RVA: 0x0000CEA8 File Offset: 0x0000B0A8
		public override string Name
		{
			get
			{
				return "Key Usage";
			}
		}

		// Token: 0x060001C8 RID: 456 RVA: 0x0000CEB0 File Offset: 0x0000B0B0
		public bool Support(KeyUsages usage)
		{
			int num = Convert.ToInt32(usage, CultureInfo.InvariantCulture);
			return (num & this.kubits) == num;
		}

		// Token: 0x060001C9 RID: 457 RVA: 0x0000CEDC File Offset: 0x0000B0DC
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
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
			return stringBuilder.ToString();
		}

		// Token: 0x040000F3 RID: 243
		private int kubits;
	}
}

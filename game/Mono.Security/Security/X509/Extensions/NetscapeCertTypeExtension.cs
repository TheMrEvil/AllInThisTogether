using System;
using System.Globalization;
using System.Text;

namespace Mono.Security.X509.Extensions
{
	// Token: 0x02000027 RID: 39
	public class NetscapeCertTypeExtension : X509Extension
	{
		// Token: 0x060001CA RID: 458 RVA: 0x0000D0A5 File Offset: 0x0000B2A5
		public NetscapeCertTypeExtension()
		{
			this.extnOid = "2.16.840.1.113730.1.1";
		}

		// Token: 0x060001CB RID: 459 RVA: 0x0000D0B8 File Offset: 0x0000B2B8
		public NetscapeCertTypeExtension(ASN1 asn1) : base(asn1)
		{
		}

		// Token: 0x060001CC RID: 460 RVA: 0x0000D0C1 File Offset: 0x0000B2C1
		public NetscapeCertTypeExtension(X509Extension extension) : base(extension)
		{
		}

		// Token: 0x060001CD RID: 461 RVA: 0x0000D0CC File Offset: 0x0000B2CC
		protected override void Decode()
		{
			ASN1 asn = new ASN1(this.extnValue.Value);
			if (asn.Tag != 3)
			{
				throw new ArgumentException("Invalid NetscapeCertType extension");
			}
			int i = 1;
			while (i < asn.Value.Length)
			{
				this.ctbits = (this.ctbits << 8) + (int)asn.Value[i++];
			}
		}

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x060001CE RID: 462 RVA: 0x0000D128 File Offset: 0x0000B328
		public override string Name
		{
			get
			{
				return "NetscapeCertType";
			}
		}

		// Token: 0x060001CF RID: 463 RVA: 0x0000D130 File Offset: 0x0000B330
		public bool Support(NetscapeCertTypeExtension.CertTypes usage)
		{
			int num = Convert.ToInt32(usage, CultureInfo.InvariantCulture);
			return (num & this.ctbits) == num;
		}

		// Token: 0x060001D0 RID: 464 RVA: 0x0000D15C File Offset: 0x0000B35C
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (this.Support(NetscapeCertTypeExtension.CertTypes.SslClient))
			{
				stringBuilder.Append("SSL Client Authentication");
			}
			if (this.Support(NetscapeCertTypeExtension.CertTypes.SslServer))
			{
				if (stringBuilder.Length > 0)
				{
					stringBuilder.Append(" , ");
				}
				stringBuilder.Append("SSL Server Authentication");
			}
			if (this.Support(NetscapeCertTypeExtension.CertTypes.Smime))
			{
				if (stringBuilder.Length > 0)
				{
					stringBuilder.Append(" , ");
				}
				stringBuilder.Append("SMIME");
			}
			if (this.Support(NetscapeCertTypeExtension.CertTypes.ObjectSigning))
			{
				if (stringBuilder.Length > 0)
				{
					stringBuilder.Append(" , ");
				}
				stringBuilder.Append("Object Signing");
			}
			if (this.Support(NetscapeCertTypeExtension.CertTypes.SslCA))
			{
				if (stringBuilder.Length > 0)
				{
					stringBuilder.Append(" , ");
				}
				stringBuilder.Append("SSL CA");
			}
			if (this.Support(NetscapeCertTypeExtension.CertTypes.SmimeCA))
			{
				if (stringBuilder.Length > 0)
				{
					stringBuilder.Append(" , ");
				}
				stringBuilder.Append("SMIME CA");
			}
			if (this.Support(NetscapeCertTypeExtension.CertTypes.ObjectSigningCA))
			{
				if (stringBuilder.Length > 0)
				{
					stringBuilder.Append(" , ");
				}
				stringBuilder.Append("Object Signing CA");
			}
			stringBuilder.Append("(");
			stringBuilder.Append(this.ctbits.ToString("X2", CultureInfo.InvariantCulture));
			stringBuilder.Append(")");
			stringBuilder.Append(Environment.NewLine);
			return stringBuilder.ToString();
		}

		// Token: 0x040000F4 RID: 244
		private int ctbits;

		// Token: 0x0200009D RID: 157
		[Flags]
		public enum CertTypes
		{
			// Token: 0x040003E8 RID: 1000
			SslClient = 128,
			// Token: 0x040003E9 RID: 1001
			SslServer = 64,
			// Token: 0x040003EA RID: 1002
			Smime = 32,
			// Token: 0x040003EB RID: 1003
			ObjectSigning = 16,
			// Token: 0x040003EC RID: 1004
			SslCA = 4,
			// Token: 0x040003ED RID: 1005
			SmimeCA = 2,
			// Token: 0x040003EE RID: 1006
			ObjectSigningCA = 1
		}
	}
}

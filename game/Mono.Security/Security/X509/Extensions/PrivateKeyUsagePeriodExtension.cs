using System;
using System.Globalization;
using System.Text;

namespace Mono.Security.X509.Extensions
{
	// Token: 0x02000028 RID: 40
	public class PrivateKeyUsagePeriodExtension : X509Extension
	{
		// Token: 0x060001D1 RID: 465 RVA: 0x0000D2CD File Offset: 0x0000B4CD
		public PrivateKeyUsagePeriodExtension()
		{
			this.extnOid = "2.5.29.16";
		}

		// Token: 0x060001D2 RID: 466 RVA: 0x0000D2E0 File Offset: 0x0000B4E0
		public PrivateKeyUsagePeriodExtension(ASN1 asn1) : base(asn1)
		{
		}

		// Token: 0x060001D3 RID: 467 RVA: 0x0000D2E9 File Offset: 0x0000B4E9
		public PrivateKeyUsagePeriodExtension(X509Extension extension) : base(extension)
		{
		}

		// Token: 0x060001D4 RID: 468 RVA: 0x0000D2F4 File Offset: 0x0000B4F4
		protected override void Decode()
		{
			ASN1 asn = new ASN1(this.extnValue.Value);
			if (asn.Tag != 48)
			{
				throw new ArgumentException("Invalid PrivateKeyUsagePeriod extension");
			}
			for (int i = 0; i < asn.Count; i++)
			{
				byte tag = asn[i].Tag;
				if (tag != 128)
				{
					if (tag != 129)
					{
						throw new ArgumentException("Invalid PrivateKeyUsagePeriod extension");
					}
					this.notAfter = ASN1Convert.ToDateTime(asn[i]);
				}
				else
				{
					this.notBefore = ASN1Convert.ToDateTime(asn[i]);
				}
			}
		}

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x060001D5 RID: 469 RVA: 0x0000D38A File Offset: 0x0000B58A
		public override string Name
		{
			get
			{
				return "Private Key Usage Period";
			}
		}

		// Token: 0x060001D6 RID: 470 RVA: 0x0000D394 File Offset: 0x0000B594
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (this.notBefore != DateTime.MinValue)
			{
				stringBuilder.Append("Not Before: ");
				stringBuilder.Append(this.notBefore.ToString(CultureInfo.CurrentUICulture));
				stringBuilder.Append(Environment.NewLine);
			}
			if (this.notAfter != DateTime.MinValue)
			{
				stringBuilder.Append("Not After: ");
				stringBuilder.Append(this.notAfter.ToString(CultureInfo.CurrentUICulture));
				stringBuilder.Append(Environment.NewLine);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x040000F5 RID: 245
		private DateTime notBefore;

		// Token: 0x040000F6 RID: 246
		private DateTime notAfter;
	}
}

using System;

namespace Mono.Security.X509.Extensions
{
	// Token: 0x02000029 RID: 41
	public class SubjectAltNameExtension : X509Extension
	{
		// Token: 0x060001D7 RID: 471 RVA: 0x0000D42F File Offset: 0x0000B62F
		public SubjectAltNameExtension()
		{
			this.extnOid = "2.5.29.17";
			this._names = new GeneralNames();
		}

		// Token: 0x060001D8 RID: 472 RVA: 0x0000D44D File Offset: 0x0000B64D
		public SubjectAltNameExtension(ASN1 asn1) : base(asn1)
		{
		}

		// Token: 0x060001D9 RID: 473 RVA: 0x0000D456 File Offset: 0x0000B656
		public SubjectAltNameExtension(X509Extension extension) : base(extension)
		{
		}

		// Token: 0x060001DA RID: 474 RVA: 0x0000D45F File Offset: 0x0000B65F
		public SubjectAltNameExtension(string[] rfc822, string[] dnsNames, string[] ipAddresses, string[] uris)
		{
			this._names = new GeneralNames(rfc822, dnsNames, ipAddresses, uris);
			this.extnValue = new ASN1(4, this._names.GetBytes());
			this.extnOid = "2.5.29.17";
		}

		// Token: 0x060001DB RID: 475 RVA: 0x0000D49C File Offset: 0x0000B69C
		protected override void Decode()
		{
			ASN1 asn = new ASN1(this.extnValue.Value);
			if (asn.Tag != 48)
			{
				throw new ArgumentException("Invalid SubjectAltName extension");
			}
			this._names = new GeneralNames(asn);
		}

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x060001DC RID: 476 RVA: 0x0000D4DB File Offset: 0x0000B6DB
		public override string Name
		{
			get
			{
				return "Subject Alternative Name";
			}
		}

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x060001DD RID: 477 RVA: 0x0000D4E2 File Offset: 0x0000B6E2
		public string[] RFC822
		{
			get
			{
				return this._names.RFC822;
			}
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x060001DE RID: 478 RVA: 0x0000D4EF File Offset: 0x0000B6EF
		public string[] DNSNames
		{
			get
			{
				return this._names.DNSNames;
			}
		}

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x060001DF RID: 479 RVA: 0x0000D4FC File Offset: 0x0000B6FC
		public string[] IPAddresses
		{
			get
			{
				return this._names.IPAddresses;
			}
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x060001E0 RID: 480 RVA: 0x0000D509 File Offset: 0x0000B709
		public string[] UniformResourceIdentifiers
		{
			get
			{
				return this._names.UniformResourceIdentifiers;
			}
		}

		// Token: 0x060001E1 RID: 481 RVA: 0x0000D516 File Offset: 0x0000B716
		public override string ToString()
		{
			return this._names.ToString();
		}

		// Token: 0x040000F7 RID: 247
		private GeneralNames _names;
	}
}

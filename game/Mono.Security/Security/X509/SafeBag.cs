using System;

namespace Mono.Security.X509
{
	// Token: 0x0200000E RID: 14
	internal class SafeBag
	{
		// Token: 0x06000068 RID: 104 RVA: 0x000043ED File Offset: 0x000025ED
		public SafeBag(string bagOID, ASN1 asn1)
		{
			this._bagOID = bagOID;
			this._asn1 = asn1;
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000069 RID: 105 RVA: 0x00004403 File Offset: 0x00002603
		public string BagOID
		{
			get
			{
				return this._bagOID;
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600006A RID: 106 RVA: 0x0000440B File Offset: 0x0000260B
		public ASN1 ASN1
		{
			get
			{
				return this._asn1;
			}
		}

		// Token: 0x04000053 RID: 83
		private string _bagOID;

		// Token: 0x04000054 RID: 84
		private ASN1 _asn1;
	}
}

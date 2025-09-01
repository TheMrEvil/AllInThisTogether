using System;
using System.Globalization;
using System.Text;

namespace Mono.Security.X509.Extensions
{
	// Token: 0x0200001E RID: 30
	public class AuthorityKeyIdentifierExtension : X509Extension
	{
		// Token: 0x06000183 RID: 387 RVA: 0x0000B929 File Offset: 0x00009B29
		public AuthorityKeyIdentifierExtension()
		{
			this.extnOid = "2.5.29.35";
		}

		// Token: 0x06000184 RID: 388 RVA: 0x0000B93C File Offset: 0x00009B3C
		public AuthorityKeyIdentifierExtension(ASN1 asn1) : base(asn1)
		{
		}

		// Token: 0x06000185 RID: 389 RVA: 0x0000B945 File Offset: 0x00009B45
		public AuthorityKeyIdentifierExtension(X509Extension extension) : base(extension)
		{
		}

		// Token: 0x06000186 RID: 390 RVA: 0x0000B950 File Offset: 0x00009B50
		protected override void Decode()
		{
			ASN1 asn = new ASN1(this.extnValue.Value);
			if (asn.Tag != 48)
			{
				throw new ArgumentException("Invalid AuthorityKeyIdentifier extension");
			}
			for (int i = 0; i < asn.Count; i++)
			{
				ASN1 asn2 = asn[i];
				if (asn2.Tag == 128)
				{
					this.aki = asn2.Value;
				}
			}
		}

		// Token: 0x06000187 RID: 391 RVA: 0x0000B9B8 File Offset: 0x00009BB8
		protected override void Encode()
		{
			ASN1 asn = new ASN1(48);
			if (this.aki == null)
			{
				throw new InvalidOperationException("Invalid AuthorityKeyIdentifier extension");
			}
			asn.Add(new ASN1(128, this.aki));
			this.extnValue = new ASN1(4);
			this.extnValue.Add(asn);
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x06000188 RID: 392 RVA: 0x0000BA10 File Offset: 0x00009C10
		public override string Name
		{
			get
			{
				return "Authority Key Identifier";
			}
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x06000189 RID: 393 RVA: 0x0000BA17 File Offset: 0x00009C17
		// (set) Token: 0x0600018A RID: 394 RVA: 0x0000BA33 File Offset: 0x00009C33
		public byte[] Identifier
		{
			get
			{
				if (this.aki == null)
				{
					return null;
				}
				return (byte[])this.aki.Clone();
			}
			set
			{
				this.aki = value;
			}
		}

		// Token: 0x0600018B RID: 395 RVA: 0x0000BA3C File Offset: 0x00009C3C
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (this.aki != null)
			{
				int i = 0;
				stringBuilder.Append("KeyID=");
				while (i < this.aki.Length)
				{
					stringBuilder.Append(this.aki[i].ToString("X2", CultureInfo.InvariantCulture));
					if (i % 2 == 1)
					{
						stringBuilder.Append(" ");
					}
					i++;
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x040000D7 RID: 215
		private byte[] aki;
	}
}

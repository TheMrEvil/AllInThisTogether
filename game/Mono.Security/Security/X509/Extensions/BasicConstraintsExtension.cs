using System;
using System.Globalization;
using System.Text;

namespace Mono.Security.X509.Extensions
{
	// Token: 0x0200001F RID: 31
	public class BasicConstraintsExtension : X509Extension
	{
		// Token: 0x0600018C RID: 396 RVA: 0x0000BAB0 File Offset: 0x00009CB0
		public BasicConstraintsExtension()
		{
			this.extnOid = "2.5.29.19";
			this.pathLenConstraint = -1;
		}

		// Token: 0x0600018D RID: 397 RVA: 0x0000BACA File Offset: 0x00009CCA
		public BasicConstraintsExtension(ASN1 asn1) : base(asn1)
		{
		}

		// Token: 0x0600018E RID: 398 RVA: 0x0000BAD3 File Offset: 0x00009CD3
		public BasicConstraintsExtension(X509Extension extension) : base(extension)
		{
		}

		// Token: 0x0600018F RID: 399 RVA: 0x0000BADC File Offset: 0x00009CDC
		protected override void Decode()
		{
			this.cA = false;
			this.pathLenConstraint = -1;
			ASN1 asn = new ASN1(this.extnValue.Value);
			if (asn.Tag != 48)
			{
				throw new ArgumentException("Invalid BasicConstraints extension");
			}
			int num = 0;
			ASN1 asn2 = asn[num++];
			if (asn2 != null && asn2.Tag == 1)
			{
				this.cA = (asn2.Value[0] == byte.MaxValue);
				asn2 = asn[num++];
			}
			if (asn2 != null && asn2.Tag == 2)
			{
				this.pathLenConstraint = ASN1Convert.ToInt32(asn2);
			}
		}

		// Token: 0x06000190 RID: 400 RVA: 0x0000BB70 File Offset: 0x00009D70
		protected override void Encode()
		{
			ASN1 asn = new ASN1(48);
			if (this.cA)
			{
				asn.Add(new ASN1(1, new byte[]
				{
					byte.MaxValue
				}));
			}
			if (this.cA && this.pathLenConstraint >= 0)
			{
				asn.Add(ASN1Convert.FromInt32(this.pathLenConstraint));
			}
			this.extnValue = new ASN1(4);
			this.extnValue.Add(asn);
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x06000191 RID: 401 RVA: 0x0000BBE4 File Offset: 0x00009DE4
		// (set) Token: 0x06000192 RID: 402 RVA: 0x0000BBEC File Offset: 0x00009DEC
		public bool CertificateAuthority
		{
			get
			{
				return this.cA;
			}
			set
			{
				this.cA = value;
			}
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x06000193 RID: 403 RVA: 0x0000BBF5 File Offset: 0x00009DF5
		public override string Name
		{
			get
			{
				return "Basic Constraints";
			}
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x06000194 RID: 404 RVA: 0x0000BBFC File Offset: 0x00009DFC
		// (set) Token: 0x06000195 RID: 405 RVA: 0x0000BC04 File Offset: 0x00009E04
		public int PathLenConstraint
		{
			get
			{
				return this.pathLenConstraint;
			}
			set
			{
				if (value < -1)
				{
					throw new ArgumentOutOfRangeException(Locale.GetText("PathLenConstraint must be positive or -1 for none ({0}).", new object[]
					{
						value
					}));
				}
				this.pathLenConstraint = value;
			}
		}

		// Token: 0x06000196 RID: 406 RVA: 0x0000BC30 File Offset: 0x00009E30
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("Subject Type=");
			stringBuilder.Append(this.cA ? "CA" : "End Entity");
			stringBuilder.Append(Environment.NewLine);
			stringBuilder.Append("Path Length Constraint=");
			if (this.pathLenConstraint == -1)
			{
				stringBuilder.Append("None");
			}
			else
			{
				stringBuilder.Append(this.pathLenConstraint.ToString(CultureInfo.InvariantCulture));
			}
			stringBuilder.Append(Environment.NewLine);
			return stringBuilder.ToString();
		}

		// Token: 0x040000D8 RID: 216
		public const int NoPathLengthConstraint = -1;

		// Token: 0x040000D9 RID: 217
		private bool cA;

		// Token: 0x040000DA RID: 218
		private int pathLenConstraint;
	}
}

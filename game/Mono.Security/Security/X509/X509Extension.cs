using System;
using System.Globalization;
using System.Text;

namespace Mono.Security.X509
{
	// Token: 0x02000018 RID: 24
	public class X509Extension
	{
		// Token: 0x06000132 RID: 306 RVA: 0x0000A471 File Offset: 0x00008671
		protected X509Extension()
		{
			this.extnCritical = false;
		}

		// Token: 0x06000133 RID: 307 RVA: 0x0000A480 File Offset: 0x00008680
		public X509Extension(ASN1 asn1)
		{
			if (asn1.Tag != 48 || asn1.Count < 2)
			{
				throw new ArgumentException(Locale.GetText("Invalid X.509 extension."));
			}
			if (asn1[0].Tag != 6)
			{
				throw new ArgumentException(Locale.GetText("Invalid X.509 extension."));
			}
			this.extnOid = ASN1Convert.ToOid(asn1[0]);
			this.extnCritical = (asn1[1].Tag == 1 && asn1[1].Value[0] == byte.MaxValue);
			this.extnValue = asn1[asn1.Count - 1];
			if (this.extnValue.Tag == 4 && this.extnValue.Length > 0 && this.extnValue.Count == 0)
			{
				try
				{
					ASN1 asn2 = new ASN1(this.extnValue.Value);
					this.extnValue.Value = null;
					this.extnValue.Add(asn2);
				}
				catch
				{
				}
			}
			this.Decode();
		}

		// Token: 0x06000134 RID: 308 RVA: 0x0000A598 File Offset: 0x00008798
		public X509Extension(X509Extension extension)
		{
			if (extension == null)
			{
				throw new ArgumentNullException("extension");
			}
			if (extension.Value == null || extension.Value.Tag != 4 || extension.Value.Count != 1)
			{
				throw new ArgumentException(Locale.GetText("Invalid X.509 extension."));
			}
			this.extnOid = extension.Oid;
			this.extnCritical = extension.Critical;
			this.extnValue = extension.Value;
			this.Decode();
		}

		// Token: 0x06000135 RID: 309 RVA: 0x0000A617 File Offset: 0x00008817
		protected virtual void Decode()
		{
		}

		// Token: 0x06000136 RID: 310 RVA: 0x0000A619 File Offset: 0x00008819
		protected virtual void Encode()
		{
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x06000137 RID: 311 RVA: 0x0000A61C File Offset: 0x0000881C
		public ASN1 ASN1
		{
			get
			{
				ASN1 asn = new ASN1(48);
				asn.Add(ASN1Convert.FromOid(this.extnOid));
				if (this.extnCritical)
				{
					asn.Add(new ASN1(1, new byte[]
					{
						byte.MaxValue
					}));
				}
				this.Encode();
				asn.Add(this.extnValue);
				return asn;
			}
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x06000138 RID: 312 RVA: 0x0000A67A File Offset: 0x0000887A
		public string Oid
		{
			get
			{
				return this.extnOid;
			}
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x06000139 RID: 313 RVA: 0x0000A682 File Offset: 0x00008882
		// (set) Token: 0x0600013A RID: 314 RVA: 0x0000A68A File Offset: 0x0000888A
		public bool Critical
		{
			get
			{
				return this.extnCritical;
			}
			set
			{
				this.extnCritical = value;
			}
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x0600013B RID: 315 RVA: 0x0000A693 File Offset: 0x00008893
		public virtual string Name
		{
			get
			{
				return this.extnOid;
			}
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x0600013C RID: 316 RVA: 0x0000A69B File Offset: 0x0000889B
		public ASN1 Value
		{
			get
			{
				if (this.extnValue == null)
				{
					this.Encode();
				}
				return this.extnValue;
			}
		}

		// Token: 0x0600013D RID: 317 RVA: 0x0000A6B4 File Offset: 0x000088B4
		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}
			X509Extension x509Extension = obj as X509Extension;
			if (x509Extension == null)
			{
				return false;
			}
			if (this.extnCritical != x509Extension.extnCritical)
			{
				return false;
			}
			if (this.extnOid != x509Extension.extnOid)
			{
				return false;
			}
			if (this.extnValue.Length != x509Extension.extnValue.Length)
			{
				return false;
			}
			for (int i = 0; i < this.extnValue.Length; i++)
			{
				if (this.extnValue[i] != x509Extension.extnValue[i])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600013E RID: 318 RVA: 0x0000A744 File Offset: 0x00008944
		public byte[] GetBytes()
		{
			return this.ASN1.GetBytes();
		}

		// Token: 0x0600013F RID: 319 RVA: 0x0000A751 File Offset: 0x00008951
		public override int GetHashCode()
		{
			return this.extnOid.GetHashCode();
		}

		// Token: 0x06000140 RID: 320 RVA: 0x0000A760 File Offset: 0x00008960
		private void WriteLine(StringBuilder sb, int n, int pos)
		{
			byte[] value = this.extnValue.Value;
			int num = pos;
			for (int i = 0; i < 8; i++)
			{
				if (i < n)
				{
					sb.Append(value[num++].ToString("X2", CultureInfo.InvariantCulture));
					sb.Append(" ");
				}
				else
				{
					sb.Append("   ");
				}
			}
			sb.Append("  ");
			num = pos;
			for (int j = 0; j < n; j++)
			{
				byte b = value[num++];
				if (b < 32)
				{
					sb.Append(".");
				}
				else
				{
					sb.Append(Convert.ToChar(b));
				}
			}
			sb.Append(Environment.NewLine);
		}

		// Token: 0x06000141 RID: 321 RVA: 0x0000A818 File Offset: 0x00008A18
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			int num = this.extnValue.Length >> 3;
			int n = this.extnValue.Length - (num << 3);
			int num2 = 0;
			for (int i = 0; i < num; i++)
			{
				this.WriteLine(stringBuilder, 8, num2);
				num2 += 8;
			}
			this.WriteLine(stringBuilder, n, num2);
			return stringBuilder.ToString();
		}

		// Token: 0x040000BE RID: 190
		protected string extnOid;

		// Token: 0x040000BF RID: 191
		protected bool extnCritical;

		// Token: 0x040000C0 RID: 192
		protected ASN1 extnValue;
	}
}

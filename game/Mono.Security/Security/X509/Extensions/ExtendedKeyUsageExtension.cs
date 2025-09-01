using System;
using System.Collections;
using System.Text;

namespace Mono.Security.X509.Extensions
{
	// Token: 0x02000022 RID: 34
	public class ExtendedKeyUsageExtension : X509Extension
	{
		// Token: 0x060001A4 RID: 420 RVA: 0x0000BFAC File Offset: 0x0000A1AC
		public ExtendedKeyUsageExtension()
		{
			this.extnOid = "2.5.29.37";
			this.keyPurpose = new ArrayList();
		}

		// Token: 0x060001A5 RID: 421 RVA: 0x0000BFCA File Offset: 0x0000A1CA
		public ExtendedKeyUsageExtension(ASN1 asn1) : base(asn1)
		{
		}

		// Token: 0x060001A6 RID: 422 RVA: 0x0000BFD3 File Offset: 0x0000A1D3
		public ExtendedKeyUsageExtension(X509Extension extension) : base(extension)
		{
		}

		// Token: 0x060001A7 RID: 423 RVA: 0x0000BFDC File Offset: 0x0000A1DC
		protected override void Decode()
		{
			this.keyPurpose = new ArrayList();
			ASN1 asn = new ASN1(this.extnValue.Value);
			if (asn.Tag != 48)
			{
				throw new ArgumentException("Invalid ExtendedKeyUsage extension");
			}
			for (int i = 0; i < asn.Count; i++)
			{
				this.keyPurpose.Add(ASN1Convert.ToOid(asn[i]));
			}
		}

		// Token: 0x060001A8 RID: 424 RVA: 0x0000C044 File Offset: 0x0000A244
		protected override void Encode()
		{
			ASN1 asn = new ASN1(48);
			foreach (object obj in this.keyPurpose)
			{
				string oid = (string)obj;
				asn.Add(ASN1Convert.FromOid(oid));
			}
			this.extnValue = new ASN1(4);
			this.extnValue.Add(asn);
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x060001A9 RID: 425 RVA: 0x0000C0C4 File Offset: 0x0000A2C4
		public ArrayList KeyPurpose
		{
			get
			{
				return this.keyPurpose;
			}
		}

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x060001AA RID: 426 RVA: 0x0000C0CC File Offset: 0x0000A2CC
		public override string Name
		{
			get
			{
				return "Extended Key Usage";
			}
		}

		// Token: 0x060001AB RID: 427 RVA: 0x0000C0D4 File Offset: 0x0000A2D4
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (object obj in this.keyPurpose)
			{
				string text = (string)obj;
				if (!(text == "1.3.6.1.5.5.7.3.1"))
				{
					if (!(text == "1.3.6.1.5.5.7.3.2"))
					{
						if (!(text == "1.3.6.1.5.5.7.3.3"))
						{
							if (!(text == "1.3.6.1.5.5.7.3.4"))
							{
								if (!(text == "1.3.6.1.5.5.7.3.8"))
								{
									if (!(text == "1.3.6.1.5.5.7.3.9"))
									{
										stringBuilder.Append("unknown");
									}
									else
									{
										stringBuilder.Append("OCSP Signing");
									}
								}
								else
								{
									stringBuilder.Append("Time Stamping");
								}
							}
							else
							{
								stringBuilder.Append("Email Protection");
							}
						}
						else
						{
							stringBuilder.Append("Code Signing");
						}
					}
					else
					{
						stringBuilder.Append("Client Authentication");
					}
				}
				else
				{
					stringBuilder.Append("Server Authentication");
				}
				stringBuilder.AppendFormat(" ({0}){1}", text, Environment.NewLine);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x040000DD RID: 221
		private ArrayList keyPurpose;
	}
}

using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace Mono.Security.X509.Extensions
{
	// Token: 0x02000020 RID: 32
	public class CRLDistributionPointsExtension : X509Extension
	{
		// Token: 0x06000197 RID: 407 RVA: 0x0000BCC2 File Offset: 0x00009EC2
		public CRLDistributionPointsExtension()
		{
			this.extnOid = "2.5.29.31";
			this.dps = new List<CRLDistributionPointsExtension.DistributionPoint>();
		}

		// Token: 0x06000198 RID: 408 RVA: 0x0000BCE0 File Offset: 0x00009EE0
		public CRLDistributionPointsExtension(ASN1 asn1) : base(asn1)
		{
		}

		// Token: 0x06000199 RID: 409 RVA: 0x0000BCE9 File Offset: 0x00009EE9
		public CRLDistributionPointsExtension(X509Extension extension) : base(extension)
		{
		}

		// Token: 0x0600019A RID: 410 RVA: 0x0000BCF4 File Offset: 0x00009EF4
		protected override void Decode()
		{
			this.dps = new List<CRLDistributionPointsExtension.DistributionPoint>();
			ASN1 asn = new ASN1(this.extnValue.Value);
			if (asn.Tag != 48)
			{
				throw new ArgumentException("Invalid CRLDistributionPoints extension");
			}
			for (int i = 0; i < asn.Count; i++)
			{
				this.dps.Add(new CRLDistributionPointsExtension.DistributionPoint(asn[i]));
			}
		}

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x0600019B RID: 411 RVA: 0x0000BD5A File Offset: 0x00009F5A
		public override string Name
		{
			get
			{
				return "CRL Distribution Points";
			}
		}

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x0600019C RID: 412 RVA: 0x0000BD61 File Offset: 0x00009F61
		public IEnumerable<CRLDistributionPointsExtension.DistributionPoint> DistributionPoints
		{
			get
			{
				return this.dps;
			}
		}

		// Token: 0x0600019D RID: 413 RVA: 0x0000BD6C File Offset: 0x00009F6C
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			int num = 1;
			foreach (CRLDistributionPointsExtension.DistributionPoint distributionPoint in this.dps)
			{
				stringBuilder.Append("[");
				stringBuilder.Append(num++);
				stringBuilder.Append("]CRL Distribution Point");
				stringBuilder.Append(Environment.NewLine);
				stringBuilder.Append("\tDistribution Point Name:");
				stringBuilder.Append("\t\tFull Name:");
				stringBuilder.Append(Environment.NewLine);
				stringBuilder.Append("\t\t\t");
				stringBuilder.Append(distributionPoint.Name);
				stringBuilder.Append(Environment.NewLine);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x040000DB RID: 219
		private List<CRLDistributionPointsExtension.DistributionPoint> dps;

		// Token: 0x0200009B RID: 155
		public class DistributionPoint
		{
			// Token: 0x1700016A RID: 362
			// (get) Token: 0x06000555 RID: 1365 RVA: 0x00019BB2 File Offset: 0x00017DB2
			// (set) Token: 0x06000556 RID: 1366 RVA: 0x00019BBA File Offset: 0x00017DBA
			public string Name
			{
				[CompilerGenerated]
				get
				{
					return this.<Name>k__BackingField;
				}
				[CompilerGenerated]
				private set
				{
					this.<Name>k__BackingField = value;
				}
			}

			// Token: 0x1700016B RID: 363
			// (get) Token: 0x06000557 RID: 1367 RVA: 0x00019BC3 File Offset: 0x00017DC3
			// (set) Token: 0x06000558 RID: 1368 RVA: 0x00019BCB File Offset: 0x00017DCB
			public CRLDistributionPointsExtension.ReasonFlags Reasons
			{
				[CompilerGenerated]
				get
				{
					return this.<Reasons>k__BackingField;
				}
				[CompilerGenerated]
				private set
				{
					this.<Reasons>k__BackingField = value;
				}
			}

			// Token: 0x1700016C RID: 364
			// (get) Token: 0x06000559 RID: 1369 RVA: 0x00019BD4 File Offset: 0x00017DD4
			// (set) Token: 0x0600055A RID: 1370 RVA: 0x00019BDC File Offset: 0x00017DDC
			public string CRLIssuer
			{
				[CompilerGenerated]
				get
				{
					return this.<CRLIssuer>k__BackingField;
				}
				[CompilerGenerated]
				private set
				{
					this.<CRLIssuer>k__BackingField = value;
				}
			}

			// Token: 0x0600055B RID: 1371 RVA: 0x00019BE5 File Offset: 0x00017DE5
			public DistributionPoint(string dp, CRLDistributionPointsExtension.ReasonFlags reasons, string issuer)
			{
				this.Name = dp;
				this.Reasons = reasons;
				this.CRLIssuer = issuer;
			}

			// Token: 0x0600055C RID: 1372 RVA: 0x00019C04 File Offset: 0x00017E04
			public DistributionPoint(ASN1 dp)
			{
				for (int i = 0; i < dp.Count; i++)
				{
					ASN1 asn = dp[i];
					switch (asn.Tag)
					{
					case 160:
						for (int j = 0; j < asn.Count; j++)
						{
							ASN1 asn2 = asn[j];
							if (asn2.Tag == 160)
							{
								this.Name = new GeneralNames(asn2).ToString();
							}
						}
						break;
					}
				}
			}

			// Token: 0x040003DA RID: 986
			[CompilerGenerated]
			private string <Name>k__BackingField;

			// Token: 0x040003DB RID: 987
			[CompilerGenerated]
			private CRLDistributionPointsExtension.ReasonFlags <Reasons>k__BackingField;

			// Token: 0x040003DC RID: 988
			[CompilerGenerated]
			private string <CRLIssuer>k__BackingField;
		}

		// Token: 0x0200009C RID: 156
		[Flags]
		public enum ReasonFlags
		{
			// Token: 0x040003DE RID: 990
			Unused = 0,
			// Token: 0x040003DF RID: 991
			KeyCompromise = 1,
			// Token: 0x040003E0 RID: 992
			CACompromise = 2,
			// Token: 0x040003E1 RID: 993
			AffiliationChanged = 3,
			// Token: 0x040003E2 RID: 994
			Superseded = 4,
			// Token: 0x040003E3 RID: 995
			CessationOfOperation = 5,
			// Token: 0x040003E4 RID: 996
			CertificateHold = 6,
			// Token: 0x040003E5 RID: 997
			PrivilegeWithdrawn = 7,
			// Token: 0x040003E6 RID: 998
			AACompromise = 8
		}
	}
}

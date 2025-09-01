using System;

namespace Mono.Math.Prime.Generator
{
	// Token: 0x0200006F RID: 111
	public abstract class PrimeGeneratorBase
	{
		// Token: 0x17000123 RID: 291
		// (get) Token: 0x0600047D RID: 1149 RVA: 0x00017CE1 File Offset: 0x00015EE1
		public virtual ConfidenceFactor Confidence
		{
			get
			{
				return ConfidenceFactor.Medium;
			}
		}

		// Token: 0x17000124 RID: 292
		// (get) Token: 0x0600047E RID: 1150 RVA: 0x00017CE4 File Offset: 0x00015EE4
		public virtual PrimalityTest PrimalityTest
		{
			get
			{
				return new PrimalityTest(PrimalityTests.RabinMillerTest);
			}
		}

		// Token: 0x17000125 RID: 293
		// (get) Token: 0x0600047F RID: 1151 RVA: 0x00017CF2 File Offset: 0x00015EF2
		public virtual int TrialDivisionBounds
		{
			get
			{
				return 4000;
			}
		}

		// Token: 0x06000480 RID: 1152 RVA: 0x00017CF9 File Offset: 0x00015EF9
		protected bool PostTrialDivisionTests(BigInteger bi)
		{
			return this.PrimalityTest(bi, this.Confidence);
		}

		// Token: 0x06000481 RID: 1153
		public abstract BigInteger GenerateNewPrime(int bits);

		// Token: 0x06000482 RID: 1154 RVA: 0x00017D0D File Offset: 0x00015F0D
		protected PrimeGeneratorBase()
		{
		}
	}
}

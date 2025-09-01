using System;

namespace Mono.Math.Prime.Generator
{
	// Token: 0x0200009F RID: 159
	internal abstract class PrimeGeneratorBase
	{
		// Token: 0x17000068 RID: 104
		// (get) Token: 0x060003F9 RID: 1017 RVA: 0x00015831 File Offset: 0x00013A31
		public virtual ConfidenceFactor Confidence
		{
			get
			{
				return ConfidenceFactor.Medium;
			}
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x060003FA RID: 1018 RVA: 0x00015834 File Offset: 0x00013A34
		public virtual PrimalityTest PrimalityTest
		{
			get
			{
				return new PrimalityTest(PrimalityTests.RabinMillerTest);
			}
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x060003FB RID: 1019 RVA: 0x00015842 File Offset: 0x00013A42
		public virtual int TrialDivisionBounds
		{
			get
			{
				return 4000;
			}
		}

		// Token: 0x060003FC RID: 1020 RVA: 0x00015849 File Offset: 0x00013A49
		protected bool PostTrialDivisionTests(BigInteger bi)
		{
			return this.PrimalityTest(bi, this.Confidence);
		}

		// Token: 0x060003FD RID: 1021
		public abstract BigInteger GenerateNewPrime(int bits);

		// Token: 0x060003FE RID: 1022 RVA: 0x0000259F File Offset: 0x0000079F
		protected PrimeGeneratorBase()
		{
		}
	}
}

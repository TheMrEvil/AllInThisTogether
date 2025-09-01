using System;

namespace Mono.Math.Prime.Generator
{
	// Token: 0x0200006E RID: 110
	public class NextPrimeFinder : SequentialSearchPrimeGeneratorBase
	{
		// Token: 0x0600047B RID: 1147 RVA: 0x00017CB7 File Offset: 0x00015EB7
		protected override BigInteger GenerateSearchBase(int bits, object Context)
		{
			if (Context == null)
			{
				throw new ArgumentNullException("Context");
			}
			BigInteger bigInteger = new BigInteger((BigInteger)Context);
			bigInteger.SetBit(0U);
			return bigInteger;
		}

		// Token: 0x0600047C RID: 1148 RVA: 0x00017CD9 File Offset: 0x00015ED9
		public NextPrimeFinder()
		{
		}
	}
}

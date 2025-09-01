using System;

namespace Mono.Math.Prime.Generator
{
	// Token: 0x0200009E RID: 158
	internal class NextPrimeFinder : SequentialSearchPrimeGeneratorBase
	{
		// Token: 0x060003F7 RID: 1015 RVA: 0x00015807 File Offset: 0x00013A07
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

		// Token: 0x060003F8 RID: 1016 RVA: 0x00015829 File Offset: 0x00013A29
		public NextPrimeFinder()
		{
		}
	}
}

using System;

namespace Mono.Math.Prime.Generator
{
	// Token: 0x02000070 RID: 112
	public class SequentialSearchPrimeGeneratorBase : PrimeGeneratorBase
	{
		// Token: 0x06000483 RID: 1155 RVA: 0x00017D15 File Offset: 0x00015F15
		protected virtual BigInteger GenerateSearchBase(int bits, object context)
		{
			BigInteger bigInteger = BigInteger.GenerateRandom(bits);
			bigInteger.SetBit(0U);
			return bigInteger;
		}

		// Token: 0x06000484 RID: 1156 RVA: 0x00017D24 File Offset: 0x00015F24
		public override BigInteger GenerateNewPrime(int bits)
		{
			return this.GenerateNewPrime(bits, null);
		}

		// Token: 0x06000485 RID: 1157 RVA: 0x00017D30 File Offset: 0x00015F30
		public virtual BigInteger GenerateNewPrime(int bits, object context)
		{
			BigInteger bigInteger = this.GenerateSearchBase(bits, context);
			uint num = bigInteger % 3234846615U;
			int trialDivisionBounds = this.TrialDivisionBounds;
			uint[] smallPrimes = BigInteger.smallPrimes;
			for (;;)
			{
				if (num % 3U != 0U && num % 5U != 0U && num % 7U != 0U && num % 11U != 0U && num % 13U != 0U && num % 17U != 0U && num % 19U != 0U && num % 23U != 0U && num % 29U != 0U)
				{
					int num2 = 10;
					while (num2 < smallPrimes.Length && (ulong)smallPrimes[num2] <= (ulong)((long)trialDivisionBounds))
					{
						if (bigInteger % smallPrimes[num2] == 0U)
						{
							goto IL_9D;
						}
						num2++;
					}
					if (this.IsPrimeAcceptable(bigInteger, context) && this.PrimalityTest(bigInteger, this.Confidence))
					{
						break;
					}
				}
				IL_9D:
				num += 2U;
				if (num >= 3234846615U)
				{
					num -= 3234846615U;
				}
				bigInteger.Incr2();
			}
			return bigInteger;
		}

		// Token: 0x06000486 RID: 1158 RVA: 0x00017DF8 File Offset: 0x00015FF8
		protected virtual bool IsPrimeAcceptable(BigInteger bi, object context)
		{
			return true;
		}

		// Token: 0x06000487 RID: 1159 RVA: 0x00017DFB File Offset: 0x00015FFB
		public SequentialSearchPrimeGeneratorBase()
		{
		}
	}
}

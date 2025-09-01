using System;

namespace Mono.Math.Prime.Generator
{
	// Token: 0x020000A0 RID: 160
	internal class SequentialSearchPrimeGeneratorBase : PrimeGeneratorBase
	{
		// Token: 0x060003FF RID: 1023 RVA: 0x0001585D File Offset: 0x00013A5D
		protected virtual BigInteger GenerateSearchBase(int bits, object context)
		{
			BigInteger bigInteger = BigInteger.GenerateRandom(bits);
			bigInteger.SetBit(0U);
			return bigInteger;
		}

		// Token: 0x06000400 RID: 1024 RVA: 0x0001586C File Offset: 0x00013A6C
		public override BigInteger GenerateNewPrime(int bits)
		{
			return this.GenerateNewPrime(bits, null);
		}

		// Token: 0x06000401 RID: 1025 RVA: 0x00015878 File Offset: 0x00013A78
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

		// Token: 0x06000402 RID: 1026 RVA: 0x000040F7 File Offset: 0x000022F7
		protected virtual bool IsPrimeAcceptable(BigInteger bi, object context)
		{
			return true;
		}

		// Token: 0x06000403 RID: 1027 RVA: 0x00015940 File Offset: 0x00013B40
		public SequentialSearchPrimeGeneratorBase()
		{
		}
	}
}

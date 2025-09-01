using System;

namespace System
{
	/// <summary>Represents a pseudo-random number generator, which is a device that produces a sequence of numbers that meet certain statistical requirements for randomness.</summary>
	// Token: 0x02000172 RID: 370
	public class Random
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Random" /> class, using a time-dependent default seed value.</summary>
		// Token: 0x06000E95 RID: 3733 RVA: 0x0003BB5F File Offset: 0x00039D5F
		public Random() : this(Random.GenerateSeed())
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Random" /> class, using the specified seed value.</summary>
		/// <param name="Seed">A number used to calculate a starting value for the pseudo-random number sequence. If a negative number is specified, the absolute value of the number is used.</param>
		// Token: 0x06000E96 RID: 3734 RVA: 0x0003BB6C File Offset: 0x00039D6C
		public Random(int Seed)
		{
			int num = 0;
			int num2 = (Seed == int.MinValue) ? int.MaxValue : Math.Abs(Seed);
			int num3 = 161803398 - num2;
			this._seedArray[55] = num3;
			int num4 = 1;
			for (int i = 1; i < 55; i++)
			{
				if ((num += 21) >= 55)
				{
					num -= 55;
				}
				this._seedArray[num] = num4;
				num4 = num3 - num4;
				if (num4 < 0)
				{
					num4 += int.MaxValue;
				}
				num3 = this._seedArray[num];
			}
			for (int j = 1; j < 5; j++)
			{
				for (int k = 1; k < 56; k++)
				{
					int num5 = k + 30;
					if (num5 >= 55)
					{
						num5 -= 55;
					}
					this._seedArray[k] -= this._seedArray[1 + num5];
					if (this._seedArray[k] < 0)
					{
						this._seedArray[k] += int.MaxValue;
					}
				}
			}
			this._inext = 0;
			this._inextp = 21;
			Seed = 1;
		}

		/// <summary>Returns a random floating-point number between 0.0 and 1.0.</summary>
		/// <returns>A double-precision floating point number that is greater than or equal to 0.0, and less than 1.0.</returns>
		// Token: 0x06000E97 RID: 3735 RVA: 0x0003BC7F File Offset: 0x00039E7F
		protected virtual double Sample()
		{
			return (double)this.InternalSample() * 4.656612875245797E-10;
		}

		// Token: 0x06000E98 RID: 3736 RVA: 0x0003BC94 File Offset: 0x00039E94
		private int InternalSample()
		{
			int num = this._inext;
			int num2 = this._inextp;
			if (++num >= 56)
			{
				num = 1;
			}
			if (++num2 >= 56)
			{
				num2 = 1;
			}
			int num3 = this._seedArray[num] - this._seedArray[num2];
			if (num3 == 2147483647)
			{
				num3--;
			}
			if (num3 < 0)
			{
				num3 += int.MaxValue;
			}
			this._seedArray[num] = num3;
			this._inext = num;
			this._inextp = num2;
			return num3;
		}

		// Token: 0x06000E99 RID: 3737 RVA: 0x0003BD08 File Offset: 0x00039F08
		private static int GenerateSeed()
		{
			Random random = Random.t_threadRandom;
			if (random == null)
			{
				Random obj = Random.s_globalRandom;
				int seed;
				lock (obj)
				{
					seed = Random.s_globalRandom.Next();
				}
				random = new Random(seed);
				Random.t_threadRandom = random;
			}
			return random.Next();
		}

		// Token: 0x06000E9A RID: 3738 RVA: 0x0003BD68 File Offset: 0x00039F68
		private unsafe static int GenerateGlobalSeed()
		{
			int result;
			Interop.GetRandomBytes((byte*)(&result), 4);
			return result;
		}

		/// <summary>Returns a non-negative random integer.</summary>
		/// <returns>A 32-bit signed integer that is greater than or equal to 0 and less than <see cref="F:System.Int32.MaxValue" />.</returns>
		// Token: 0x06000E9B RID: 3739 RVA: 0x0003BD7F File Offset: 0x00039F7F
		public virtual int Next()
		{
			return this.InternalSample();
		}

		// Token: 0x06000E9C RID: 3740 RVA: 0x0003BD88 File Offset: 0x00039F88
		private double GetSampleForLargeRange()
		{
			int num = this.InternalSample();
			if (this.InternalSample() % 2 == 0)
			{
				num = -num;
			}
			return ((double)num + 2147483646.0) / 4294967293.0;
		}

		/// <summary>Returns a random integer that is within a specified range.</summary>
		/// <param name="minValue">The inclusive lower bound of the random number returned.</param>
		/// <param name="maxValue">The exclusive upper bound of the random number returned. <paramref name="maxValue" /> must be greater than or equal to <paramref name="minValue" />.</param>
		/// <returns>A 32-bit signed integer greater than or equal to <paramref name="minValue" /> and less than <paramref name="maxValue" />; that is, the range of return values includes <paramref name="minValue" /> but not <paramref name="maxValue" />. If <paramref name="minValue" /> equals <paramref name="maxValue" />, <paramref name="minValue" /> is returned.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="minValue" /> is greater than <paramref name="maxValue" />.</exception>
		// Token: 0x06000E9D RID: 3741 RVA: 0x0003BDC8 File Offset: 0x00039FC8
		public virtual int Next(int minValue, int maxValue)
		{
			if (minValue > maxValue)
			{
				throw new ArgumentOutOfRangeException("minValue", SR.Format("'{0}' cannot be greater than {1}.", "minValue", "maxValue"));
			}
			long num = (long)maxValue - (long)minValue;
			if (num <= 2147483647L)
			{
				return (int)(this.Sample() * (double)num) + minValue;
			}
			return (int)((long)(this.GetSampleForLargeRange() * (double)num) + (long)minValue);
		}

		/// <summary>Returns a non-negative random integer that is less than the specified maximum.</summary>
		/// <param name="maxValue">The exclusive upper bound of the random number to be generated. <paramref name="maxValue" /> must be greater than or equal to 0.</param>
		/// <returns>A 32-bit signed integer that is greater than or equal to 0, and less than <paramref name="maxValue" />; that is, the range of return values ordinarily includes 0 but not <paramref name="maxValue" />. However, if <paramref name="maxValue" /> equals 0, <paramref name="maxValue" /> is returned.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="maxValue" /> is less than 0.</exception>
		// Token: 0x06000E9E RID: 3742 RVA: 0x0003BE22 File Offset: 0x0003A022
		public virtual int Next(int maxValue)
		{
			if (maxValue < 0)
			{
				throw new ArgumentOutOfRangeException("maxValue", SR.Format("'{0}' must be greater than zero.", "maxValue"));
			}
			return (int)(this.Sample() * (double)maxValue);
		}

		/// <summary>Returns a random floating-point number that is greater than or equal to 0.0, and less than 1.0.</summary>
		/// <returns>A double-precision floating point number that is greater than or equal to 0.0, and less than 1.0.</returns>
		// Token: 0x06000E9F RID: 3743 RVA: 0x0003BE4C File Offset: 0x0003A04C
		public virtual double NextDouble()
		{
			return this.Sample();
		}

		/// <summary>Fills the elements of a specified array of bytes with random numbers.</summary>
		/// <param name="buffer">An array of bytes to contain random numbers.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffer" /> is <see langword="null" />.</exception>
		// Token: 0x06000EA0 RID: 3744 RVA: 0x0003BE54 File Offset: 0x0003A054
		public virtual void NextBytes(byte[] buffer)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			for (int i = 0; i < buffer.Length; i++)
			{
				buffer[i] = (byte)this.InternalSample();
			}
		}

		// Token: 0x06000EA1 RID: 3745 RVA: 0x0003BE88 File Offset: 0x0003A088
		public unsafe virtual void NextBytes(Span<byte> buffer)
		{
			for (int i = 0; i < buffer.Length; i++)
			{
				*buffer[i] = (byte)this.Next();
			}
		}

		// Token: 0x06000EA2 RID: 3746 RVA: 0x0003BEB7 File Offset: 0x0003A0B7
		// Note: this type is marked as 'beforefieldinit'.
		static Random()
		{
		}

		// Token: 0x040012C4 RID: 4804
		private const int MBIG = 2147483647;

		// Token: 0x040012C5 RID: 4805
		private const int MSEED = 161803398;

		// Token: 0x040012C6 RID: 4806
		private const int MZ = 0;

		// Token: 0x040012C7 RID: 4807
		private int _inext;

		// Token: 0x040012C8 RID: 4808
		private int _inextp;

		// Token: 0x040012C9 RID: 4809
		private int[] _seedArray = new int[56];

		// Token: 0x040012CA RID: 4810
		[ThreadStatic]
		private static Random t_threadRandom;

		// Token: 0x040012CB RID: 4811
		private static readonly Random s_globalRandom = new Random(Random.GenerateGlobalSeed());
	}
}

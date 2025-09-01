using System;
using System.Runtime.CompilerServices;

namespace System
{
	// Token: 0x02000155 RID: 341
	public static class MathF
	{
		// Token: 0x06000D25 RID: 3365 RVA: 0x00033599 File Offset: 0x00031799
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float Abs(float x)
		{
			return Math.Abs(x);
		}

		// Token: 0x06000D26 RID: 3366 RVA: 0x000335A4 File Offset: 0x000317A4
		public static float IEEERemainder(float x, float y)
		{
			if (float.IsNaN(x))
			{
				return x;
			}
			if (float.IsNaN(y))
			{
				return y;
			}
			float num = x % y;
			if (float.IsNaN(num))
			{
				return float.NaN;
			}
			if (num == 0f && float.IsNegative(x))
			{
				return --0f;
			}
			float num2 = num - MathF.Abs(y) * (float)MathF.Sign(x);
			if (MathF.Abs(num2) == MathF.Abs(num))
			{
				float x2 = x / y;
				if (MathF.Abs(MathF.Round(x2)) > MathF.Abs(x2))
				{
					return num2;
				}
				return num;
			}
			else
			{
				if (MathF.Abs(num2) < MathF.Abs(num))
				{
					return num2;
				}
				return num;
			}
		}

		// Token: 0x06000D27 RID: 3367 RVA: 0x00033638 File Offset: 0x00031838
		public static float Log(float x, float y)
		{
			if (float.IsNaN(x))
			{
				return x;
			}
			if (float.IsNaN(y))
			{
				return y;
			}
			if (y == 1f)
			{
				return float.NaN;
			}
			if (x != 1f && (y == 0f || float.IsPositiveInfinity(y)))
			{
				return float.NaN;
			}
			return MathF.Log(x) / MathF.Log(y);
		}

		// Token: 0x06000D28 RID: 3368 RVA: 0x00033692 File Offset: 0x00031892
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float Max(float x, float y)
		{
			return Math.Max(x, y);
		}

		// Token: 0x06000D29 RID: 3369 RVA: 0x0003369B File Offset: 0x0003189B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float Min(float x, float y)
		{
			return Math.Min(x, y);
		}

		// Token: 0x06000D2A RID: 3370 RVA: 0x000336A4 File Offset: 0x000318A4
		[Intrinsic]
		public static float Round(float x)
		{
			if (x == (float)((int)x))
			{
				return x;
			}
			float num = MathF.Floor(x + 0.5f);
			if (x == MathF.Floor(x) + 0.5f && MathF.FMod(num, 2f) != 0f)
			{
				num -= 1f;
			}
			return MathF.CopySign(num, x);
		}

		// Token: 0x06000D2B RID: 3371 RVA: 0x000336F6 File Offset: 0x000318F6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float Round(float x, int digits)
		{
			return MathF.Round(x, digits, MidpointRounding.ToEven);
		}

		// Token: 0x06000D2C RID: 3372 RVA: 0x00033700 File Offset: 0x00031900
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float Round(float x, MidpointRounding mode)
		{
			return MathF.Round(x, 0, mode);
		}

		// Token: 0x06000D2D RID: 3373 RVA: 0x0003370C File Offset: 0x0003190C
		public unsafe static float Round(float x, int digits, MidpointRounding mode)
		{
			if (digits < 0 || digits > 6)
			{
				throw new ArgumentOutOfRangeException("digits", "Rounding digits must be between 0 and 15, inclusive.");
			}
			if (mode < MidpointRounding.ToEven || mode > MidpointRounding.AwayFromZero)
			{
				throw new ArgumentException(SR.Format("The Enum type should contain one and only one instance field.", mode, "MidpointRounding"), "mode");
			}
			if (MathF.Abs(x) < MathF.singleRoundLimit)
			{
				float num = MathF.roundPower10Single[digits];
				x *= num;
				if (mode == MidpointRounding.AwayFromZero)
				{
					float x2 = MathF.ModF(x, &x);
					if (MathF.Abs(x2) >= 0.5f)
					{
						x += (float)MathF.Sign(x2);
					}
				}
				else
				{
					x = MathF.Round(x);
				}
				x /= num;
			}
			return x;
		}

		// Token: 0x06000D2E RID: 3374 RVA: 0x000337A9 File Offset: 0x000319A9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int Sign(float x)
		{
			return Math.Sign(x);
		}

		// Token: 0x06000D2F RID: 3375 RVA: 0x000337B1 File Offset: 0x000319B1
		public unsafe static float Truncate(float x)
		{
			MathF.ModF(x, &x);
			return x;
		}

		// Token: 0x06000D30 RID: 3376 RVA: 0x000337C0 File Offset: 0x000319C0
		private static float CopySign(float x, float y)
		{
			int num = BitConverter.SingleToInt32Bits(x);
			int num2 = BitConverter.SingleToInt32Bits(y);
			if ((num ^ num2) >> 31 != 0)
			{
				return BitConverter.Int32BitsToSingle(num ^ int.MinValue);
			}
			return x;
		}

		// Token: 0x06000D31 RID: 3377
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern float Acos(float x);

		// Token: 0x06000D32 RID: 3378
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern float Acosh(float x);

		// Token: 0x06000D33 RID: 3379
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern float Asin(float x);

		// Token: 0x06000D34 RID: 3380
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern float Asinh(float x);

		// Token: 0x06000D35 RID: 3381
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern float Atan(float x);

		// Token: 0x06000D36 RID: 3382
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern float Atan2(float y, float x);

		// Token: 0x06000D37 RID: 3383
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern float Atanh(float x);

		// Token: 0x06000D38 RID: 3384
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern float Cbrt(float x);

		// Token: 0x06000D39 RID: 3385
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern float Ceiling(float x);

		// Token: 0x06000D3A RID: 3386
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern float Cos(float x);

		// Token: 0x06000D3B RID: 3387
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern float Cosh(float x);

		// Token: 0x06000D3C RID: 3388
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern float Exp(float x);

		// Token: 0x06000D3D RID: 3389
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern float Floor(float x);

		// Token: 0x06000D3E RID: 3390
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern float Log(float x);

		// Token: 0x06000D3F RID: 3391
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern float Log10(float x);

		// Token: 0x06000D40 RID: 3392
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern float Pow(float x, float y);

		// Token: 0x06000D41 RID: 3393
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern float Sin(float x);

		// Token: 0x06000D42 RID: 3394
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern float Sinh(float x);

		// Token: 0x06000D43 RID: 3395
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern float Sqrt(float x);

		// Token: 0x06000D44 RID: 3396
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern float Tan(float x);

		// Token: 0x06000D45 RID: 3397
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern float Tanh(float x);

		// Token: 0x06000D46 RID: 3398
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float FMod(float x, float y);

		// Token: 0x06000D47 RID: 3399
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern float ModF(float x, float* intptr);

		// Token: 0x06000D48 RID: 3400 RVA: 0x000337F1 File Offset: 0x000319F1
		// Note: this type is marked as 'beforefieldinit'.
		static MathF()
		{
		}

		// Token: 0x04001277 RID: 4727
		public const float E = 2.7182817f;

		// Token: 0x04001278 RID: 4728
		public const float PI = 3.1415927f;

		// Token: 0x04001279 RID: 4729
		private const int maxRoundingDigits = 6;

		// Token: 0x0400127A RID: 4730
		private static float[] roundPower10Single = new float[]
		{
			1f,
			10f,
			100f,
			1000f,
			10000f,
			100000f,
			1000000f
		};

		// Token: 0x0400127B RID: 4731
		private static float singleRoundLimit = 100000000f;
	}
}

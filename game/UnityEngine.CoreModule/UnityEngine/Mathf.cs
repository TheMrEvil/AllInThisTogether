using System;
using System.Runtime.CompilerServices;
using Unity.IL2CPP.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Internal;
using UnityEngineInternal;

namespace UnityEngine
{
	// Token: 0x020001C8 RID: 456
	[NativeHeader("Runtime/Math/ColorSpaceConversion.h")]
	[NativeHeader("Runtime/Math/FloatConversion.h")]
	[NativeHeader("Runtime/Math/PerlinNoise.h")]
	[Il2CppEagerStaticClassConstruction]
	[NativeHeader("Runtime/Utilities/BitUtility.h")]
	public struct Mathf
	{
		// Token: 0x0600149B RID: 5275
		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int ClosestPowerOfTwo(int value);

		// Token: 0x0600149C RID: 5276
		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool IsPowerOfTwo(int value);

		// Token: 0x0600149D RID: 5277
		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int NextPowerOfTwo(int value);

		// Token: 0x0600149E RID: 5278
		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern float GammaToLinearSpace(float value);

		// Token: 0x0600149F RID: 5279
		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern float LinearToGammaSpace(float value);

		// Token: 0x060014A0 RID: 5280 RVA: 0x0001FB78 File Offset: 0x0001DD78
		[FreeFunction(IsThreadSafe = true)]
		public static Color CorrelatedColorTemperatureToRGB(float kelvin)
		{
			Color result;
			Mathf.CorrelatedColorTemperatureToRGB_Injected(kelvin, out result);
			return result;
		}

		// Token: 0x060014A1 RID: 5281
		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern ushort FloatToHalf(float val);

		// Token: 0x060014A2 RID: 5282
		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern float HalfToFloat(ushort val);

		// Token: 0x060014A3 RID: 5283
		[FreeFunction("PerlinNoise::NoiseNormalized", IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern float PerlinNoise(float x, float y);

		// Token: 0x060014A4 RID: 5284 RVA: 0x0001FB90 File Offset: 0x0001DD90
		public static float Sin(float f)
		{
			return (float)Math.Sin((double)f);
		}

		// Token: 0x060014A5 RID: 5285 RVA: 0x0001FBAC File Offset: 0x0001DDAC
		public static float Cos(float f)
		{
			return (float)Math.Cos((double)f);
		}

		// Token: 0x060014A6 RID: 5286 RVA: 0x0001FBC8 File Offset: 0x0001DDC8
		public static float Tan(float f)
		{
			return (float)Math.Tan((double)f);
		}

		// Token: 0x060014A7 RID: 5287 RVA: 0x0001FBE4 File Offset: 0x0001DDE4
		public static float Asin(float f)
		{
			return (float)Math.Asin((double)f);
		}

		// Token: 0x060014A8 RID: 5288 RVA: 0x0001FC00 File Offset: 0x0001DE00
		public static float Acos(float f)
		{
			return (float)Math.Acos((double)f);
		}

		// Token: 0x060014A9 RID: 5289 RVA: 0x0001FC1C File Offset: 0x0001DE1C
		public static float Atan(float f)
		{
			return (float)Math.Atan((double)f);
		}

		// Token: 0x060014AA RID: 5290 RVA: 0x0001FC38 File Offset: 0x0001DE38
		public static float Atan2(float y, float x)
		{
			return (float)Math.Atan2((double)y, (double)x);
		}

		// Token: 0x060014AB RID: 5291 RVA: 0x0001FC54 File Offset: 0x0001DE54
		public static float Sqrt(float f)
		{
			return (float)Math.Sqrt((double)f);
		}

		// Token: 0x060014AC RID: 5292 RVA: 0x0001FC70 File Offset: 0x0001DE70
		public static float Abs(float f)
		{
			return Math.Abs(f);
		}

		// Token: 0x060014AD RID: 5293 RVA: 0x0001FC88 File Offset: 0x0001DE88
		public static int Abs(int value)
		{
			return Math.Abs(value);
		}

		// Token: 0x060014AE RID: 5294 RVA: 0x0001FCA0 File Offset: 0x0001DEA0
		public static float Min(float a, float b)
		{
			return (a < b) ? a : b;
		}

		// Token: 0x060014AF RID: 5295 RVA: 0x0001FCBC File Offset: 0x0001DEBC
		public static float Min(params float[] values)
		{
			int num = values.Length;
			bool flag = num == 0;
			float result;
			if (flag)
			{
				result = 0f;
			}
			else
			{
				float num2 = values[0];
				for (int i = 1; i < num; i++)
				{
					bool flag2 = values[i] < num2;
					if (flag2)
					{
						num2 = values[i];
					}
				}
				result = num2;
			}
			return result;
		}

		// Token: 0x060014B0 RID: 5296 RVA: 0x0001FD14 File Offset: 0x0001DF14
		public static int Min(int a, int b)
		{
			return (a < b) ? a : b;
		}

		// Token: 0x060014B1 RID: 5297 RVA: 0x0001FD30 File Offset: 0x0001DF30
		public static int Min(params int[] values)
		{
			int num = values.Length;
			bool flag = num == 0;
			int result;
			if (flag)
			{
				result = 0;
			}
			else
			{
				int num2 = values[0];
				for (int i = 1; i < num; i++)
				{
					bool flag2 = values[i] < num2;
					if (flag2)
					{
						num2 = values[i];
					}
				}
				result = num2;
			}
			return result;
		}

		// Token: 0x060014B2 RID: 5298 RVA: 0x0001FD84 File Offset: 0x0001DF84
		public static float Max(float a, float b)
		{
			return (a > b) ? a : b;
		}

		// Token: 0x060014B3 RID: 5299 RVA: 0x0001FDA0 File Offset: 0x0001DFA0
		public static float Max(params float[] values)
		{
			int num = values.Length;
			bool flag = num == 0;
			float result;
			if (flag)
			{
				result = 0f;
			}
			else
			{
				float num2 = values[0];
				for (int i = 1; i < num; i++)
				{
					bool flag2 = values[i] > num2;
					if (flag2)
					{
						num2 = values[i];
					}
				}
				result = num2;
			}
			return result;
		}

		// Token: 0x060014B4 RID: 5300 RVA: 0x0001FDF8 File Offset: 0x0001DFF8
		public static int Max(int a, int b)
		{
			return (a > b) ? a : b;
		}

		// Token: 0x060014B5 RID: 5301 RVA: 0x0001FE14 File Offset: 0x0001E014
		public static int Max(params int[] values)
		{
			int num = values.Length;
			bool flag = num == 0;
			int result;
			if (flag)
			{
				result = 0;
			}
			else
			{
				int num2 = values[0];
				for (int i = 1; i < num; i++)
				{
					bool flag2 = values[i] > num2;
					if (flag2)
					{
						num2 = values[i];
					}
				}
				result = num2;
			}
			return result;
		}

		// Token: 0x060014B6 RID: 5302 RVA: 0x0001FE68 File Offset: 0x0001E068
		public static float Pow(float f, float p)
		{
			return (float)Math.Pow((double)f, (double)p);
		}

		// Token: 0x060014B7 RID: 5303 RVA: 0x0001FE84 File Offset: 0x0001E084
		public static float Exp(float power)
		{
			return (float)Math.Exp((double)power);
		}

		// Token: 0x060014B8 RID: 5304 RVA: 0x0001FEA0 File Offset: 0x0001E0A0
		public static float Log(float f, float p)
		{
			return (float)Math.Log((double)f, (double)p);
		}

		// Token: 0x060014B9 RID: 5305 RVA: 0x0001FEBC File Offset: 0x0001E0BC
		public static float Log(float f)
		{
			return (float)Math.Log((double)f);
		}

		// Token: 0x060014BA RID: 5306 RVA: 0x0001FED8 File Offset: 0x0001E0D8
		public static float Log10(float f)
		{
			return (float)Math.Log10((double)f);
		}

		// Token: 0x060014BB RID: 5307 RVA: 0x0001FEF4 File Offset: 0x0001E0F4
		public static float Ceil(float f)
		{
			return (float)Math.Ceiling((double)f);
		}

		// Token: 0x060014BC RID: 5308 RVA: 0x0001FF10 File Offset: 0x0001E110
		public static float Floor(float f)
		{
			return (float)Math.Floor((double)f);
		}

		// Token: 0x060014BD RID: 5309 RVA: 0x0001FF2C File Offset: 0x0001E12C
		public static float Round(float f)
		{
			return (float)Math.Round((double)f);
		}

		// Token: 0x060014BE RID: 5310 RVA: 0x0001FF48 File Offset: 0x0001E148
		public static int CeilToInt(float f)
		{
			return (int)Math.Ceiling((double)f);
		}

		// Token: 0x060014BF RID: 5311 RVA: 0x0001FF64 File Offset: 0x0001E164
		public static int FloorToInt(float f)
		{
			return (int)Math.Floor((double)f);
		}

		// Token: 0x060014C0 RID: 5312 RVA: 0x0001FF80 File Offset: 0x0001E180
		public static int RoundToInt(float f)
		{
			return (int)Math.Round((double)f);
		}

		// Token: 0x060014C1 RID: 5313 RVA: 0x0001FF9C File Offset: 0x0001E19C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float Sign(float f)
		{
			return (f >= 0f) ? 1f : -1f;
		}

		// Token: 0x060014C2 RID: 5314 RVA: 0x0001FFC4 File Offset: 0x0001E1C4
		public static float Clamp(float value, float min, float max)
		{
			bool flag = value < min;
			if (flag)
			{
				value = min;
			}
			else
			{
				bool flag2 = value > max;
				if (flag2)
				{
					value = max;
				}
			}
			return value;
		}

		// Token: 0x060014C3 RID: 5315 RVA: 0x0001FFF0 File Offset: 0x0001E1F0
		public static int Clamp(int value, int min, int max)
		{
			bool flag = value < min;
			if (flag)
			{
				value = min;
			}
			else
			{
				bool flag2 = value > max;
				if (flag2)
				{
					value = max;
				}
			}
			return value;
		}

		// Token: 0x060014C4 RID: 5316 RVA: 0x0002001C File Offset: 0x0001E21C
		public static float Clamp01(float value)
		{
			bool flag = value < 0f;
			float result;
			if (flag)
			{
				result = 0f;
			}
			else
			{
				bool flag2 = value > 1f;
				if (flag2)
				{
					result = 1f;
				}
				else
				{
					result = value;
				}
			}
			return result;
		}

		// Token: 0x060014C5 RID: 5317 RVA: 0x00020058 File Offset: 0x0001E258
		public static float Lerp(float a, float b, float t)
		{
			return a + (b - a) * Mathf.Clamp01(t);
		}

		// Token: 0x060014C6 RID: 5318 RVA: 0x00020078 File Offset: 0x0001E278
		public static float LerpUnclamped(float a, float b, float t)
		{
			return a + (b - a) * t;
		}

		// Token: 0x060014C7 RID: 5319 RVA: 0x00020094 File Offset: 0x0001E294
		public static float LerpAngle(float a, float b, float t)
		{
			float num = Mathf.Repeat(b - a, 360f);
			bool flag = num > 180f;
			if (flag)
			{
				num -= 360f;
			}
			return a + num * Mathf.Clamp01(t);
		}

		// Token: 0x060014C8 RID: 5320 RVA: 0x000200D4 File Offset: 0x0001E2D4
		public static float MoveTowards(float current, float target, float maxDelta)
		{
			bool flag = Mathf.Abs(target - current) <= maxDelta;
			float result;
			if (flag)
			{
				result = target;
			}
			else
			{
				result = current + Mathf.Sign(target - current) * maxDelta;
			}
			return result;
		}

		// Token: 0x060014C9 RID: 5321 RVA: 0x00020108 File Offset: 0x0001E308
		public static float MoveTowardsAngle(float current, float target, float maxDelta)
		{
			float num = Mathf.DeltaAngle(current, target);
			bool flag = -maxDelta < num && num < maxDelta;
			float result;
			if (flag)
			{
				result = target;
			}
			else
			{
				target = current + num;
				result = Mathf.MoveTowards(current, target, maxDelta);
			}
			return result;
		}

		// Token: 0x060014CA RID: 5322 RVA: 0x00020144 File Offset: 0x0001E344
		public static float SmoothStep(float from, float to, float t)
		{
			t = Mathf.Clamp01(t);
			t = -2f * t * t * t + 3f * t * t;
			return to * t + from * (1f - t);
		}

		// Token: 0x060014CB RID: 5323 RVA: 0x00020184 File Offset: 0x0001E384
		public static float Gamma(float value, float absmax, float gamma)
		{
			bool flag = value < 0f;
			float num = Mathf.Abs(value);
			bool flag2 = num > absmax;
			float result;
			if (flag2)
			{
				result = (flag ? (-num) : num);
			}
			else
			{
				float num2 = Mathf.Pow(num / absmax, gamma) * absmax;
				result = (flag ? (-num2) : num2);
			}
			return result;
		}

		// Token: 0x060014CC RID: 5324 RVA: 0x000201D0 File Offset: 0x0001E3D0
		public static bool Approximately(float a, float b)
		{
			return Mathf.Abs(b - a) < Mathf.Max(1E-06f * Mathf.Max(Mathf.Abs(a), Mathf.Abs(b)), Mathf.Epsilon * 8f);
		}

		// Token: 0x060014CD RID: 5325 RVA: 0x00020214 File Offset: 0x0001E414
		[ExcludeFromDocs]
		public static float SmoothDamp(float current, float target, ref float currentVelocity, float smoothTime, float maxSpeed)
		{
			float deltaTime = Time.deltaTime;
			return Mathf.SmoothDamp(current, target, ref currentVelocity, smoothTime, maxSpeed, deltaTime);
		}

		// Token: 0x060014CE RID: 5326 RVA: 0x00020238 File Offset: 0x0001E438
		[ExcludeFromDocs]
		public static float SmoothDamp(float current, float target, ref float currentVelocity, float smoothTime)
		{
			float deltaTime = Time.deltaTime;
			float positiveInfinity = float.PositiveInfinity;
			return Mathf.SmoothDamp(current, target, ref currentVelocity, smoothTime, positiveInfinity, deltaTime);
		}

		// Token: 0x060014CF RID: 5327 RVA: 0x00020264 File Offset: 0x0001E464
		public static float SmoothDamp(float current, float target, ref float currentVelocity, float smoothTime, [DefaultValue("Mathf.Infinity")] float maxSpeed, [DefaultValue("Time.deltaTime")] float deltaTime)
		{
			smoothTime = Mathf.Max(0.0001f, smoothTime);
			float num = 2f / smoothTime;
			float num2 = num * deltaTime;
			float num3 = 1f / (1f + num2 + 0.48f * num2 * num2 + 0.235f * num2 * num2 * num2);
			float num4 = current - target;
			float num5 = target;
			float num6 = maxSpeed * smoothTime;
			num4 = Mathf.Clamp(num4, -num6, num6);
			target = current - num4;
			float num7 = (currentVelocity + num * num4) * deltaTime;
			currentVelocity = (currentVelocity - num * num7) * num3;
			float num8 = target + (num4 + num7) * num3;
			bool flag = num5 - current > 0f == num8 > num5;
			if (flag)
			{
				num8 = num5;
				currentVelocity = (num8 - num5) / deltaTime;
			}
			return num8;
		}

		// Token: 0x060014D0 RID: 5328 RVA: 0x00020320 File Offset: 0x0001E520
		[ExcludeFromDocs]
		public static float SmoothDampAngle(float current, float target, ref float currentVelocity, float smoothTime, float maxSpeed)
		{
			float deltaTime = Time.deltaTime;
			return Mathf.SmoothDampAngle(current, target, ref currentVelocity, smoothTime, maxSpeed, deltaTime);
		}

		// Token: 0x060014D1 RID: 5329 RVA: 0x00020344 File Offset: 0x0001E544
		[ExcludeFromDocs]
		public static float SmoothDampAngle(float current, float target, ref float currentVelocity, float smoothTime)
		{
			float deltaTime = Time.deltaTime;
			float positiveInfinity = float.PositiveInfinity;
			return Mathf.SmoothDampAngle(current, target, ref currentVelocity, smoothTime, positiveInfinity, deltaTime);
		}

		// Token: 0x060014D2 RID: 5330 RVA: 0x00020370 File Offset: 0x0001E570
		public static float SmoothDampAngle(float current, float target, ref float currentVelocity, float smoothTime, [DefaultValue("Mathf.Infinity")] float maxSpeed, [DefaultValue("Time.deltaTime")] float deltaTime)
		{
			target = current + Mathf.DeltaAngle(current, target);
			return Mathf.SmoothDamp(current, target, ref currentVelocity, smoothTime, maxSpeed, deltaTime);
		}

		// Token: 0x060014D3 RID: 5331 RVA: 0x0002039C File Offset: 0x0001E59C
		public static float Repeat(float t, float length)
		{
			return Mathf.Clamp(t - Mathf.Floor(t / length) * length, 0f, length);
		}

		// Token: 0x060014D4 RID: 5332 RVA: 0x000203C8 File Offset: 0x0001E5C8
		public static float PingPong(float t, float length)
		{
			t = Mathf.Repeat(t, length * 2f);
			return length - Mathf.Abs(t - length);
		}

		// Token: 0x060014D5 RID: 5333 RVA: 0x000203F4 File Offset: 0x0001E5F4
		public static float InverseLerp(float a, float b, float value)
		{
			bool flag = a != b;
			float result;
			if (flag)
			{
				result = Mathf.Clamp01((value - a) / (b - a));
			}
			else
			{
				result = 0f;
			}
			return result;
		}

		// Token: 0x060014D6 RID: 5334 RVA: 0x00020428 File Offset: 0x0001E628
		public static float DeltaAngle(float current, float target)
		{
			float num = Mathf.Repeat(target - current, 360f);
			bool flag = num > 180f;
			if (flag)
			{
				num -= 360f;
			}
			return num;
		}

		// Token: 0x060014D7 RID: 5335 RVA: 0x00020460 File Offset: 0x0001E660
		internal static bool LineIntersection(Vector2 p1, Vector2 p2, Vector2 p3, Vector2 p4, ref Vector2 result)
		{
			float num = p2.x - p1.x;
			float num2 = p2.y - p1.y;
			float num3 = p4.x - p3.x;
			float num4 = p4.y - p3.y;
			float num5 = num * num4 - num2 * num3;
			bool flag = num5 == 0f;
			bool result2;
			if (flag)
			{
				result2 = false;
			}
			else
			{
				float num6 = p3.x - p1.x;
				float num7 = p3.y - p1.y;
				float num8 = (num6 * num4 - num7 * num3) / num5;
				result.x = p1.x + num8 * num;
				result.y = p1.y + num8 * num2;
				result2 = true;
			}
			return result2;
		}

		// Token: 0x060014D8 RID: 5336 RVA: 0x0002051C File Offset: 0x0001E71C
		internal static bool LineSegmentIntersection(Vector2 p1, Vector2 p2, Vector2 p3, Vector2 p4, ref Vector2 result)
		{
			float num = p2.x - p1.x;
			float num2 = p2.y - p1.y;
			float num3 = p4.x - p3.x;
			float num4 = p4.y - p3.y;
			float num5 = num * num4 - num2 * num3;
			bool flag = num5 == 0f;
			bool result2;
			if (flag)
			{
				result2 = false;
			}
			else
			{
				float num6 = p3.x - p1.x;
				float num7 = p3.y - p1.y;
				float num8 = (num6 * num4 - num7 * num3) / num5;
				bool flag2 = num8 < 0f || num8 > 1f;
				if (flag2)
				{
					result2 = false;
				}
				else
				{
					float num9 = (num6 * num2 - num7 * num) / num5;
					bool flag3 = num9 < 0f || num9 > 1f;
					if (flag3)
					{
						result2 = false;
					}
					else
					{
						result.x = p1.x + num8 * num;
						result.y = p1.y + num8 * num2;
						result2 = true;
					}
				}
			}
			return result2;
		}

		// Token: 0x060014D9 RID: 5337 RVA: 0x0002062C File Offset: 0x0001E82C
		internal static long RandomToLong(Random r)
		{
			byte[] array = new byte[8];
			r.NextBytes(array);
			return (long)(BitConverter.ToUInt64(array, 0) & 9223372036854775807UL);
		}

		// Token: 0x060014DA RID: 5338 RVA: 0x0002065E File Offset: 0x0001E85E
		// Note: this type is marked as 'beforefieldinit'.
		static Mathf()
		{
		}

		// Token: 0x060014DB RID: 5339
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void CorrelatedColorTemperatureToRGB_Injected(float kelvin, out Color ret);

		// Token: 0x04000779 RID: 1913
		public const float PI = 3.1415927f;

		// Token: 0x0400077A RID: 1914
		public const float Infinity = float.PositiveInfinity;

		// Token: 0x0400077B RID: 1915
		public const float NegativeInfinity = float.NegativeInfinity;

		// Token: 0x0400077C RID: 1916
		public const float Deg2Rad = 0.017453292f;

		// Token: 0x0400077D RID: 1917
		public const float Rad2Deg = 57.29578f;

		// Token: 0x0400077E RID: 1918
		public static readonly float Epsilon = MathfInternal.IsFlushToZeroEnabled ? MathfInternal.FloatMinNormal : MathfInternal.FloatMinDenormal;
	}
}

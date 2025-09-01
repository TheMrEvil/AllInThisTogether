using System;
using System.Runtime.CompilerServices;
using Unity.IL2CPP.CompilerServices;

namespace Unity.Mathematics
{
	// Token: 0x02000017 RID: 23
	[Il2CppEagerStaticClassConstruction]
	[Serializable]
	public struct double3x4 : IEquatable<double3x4>, IFormattable
	{
		// Token: 0x06000D3E RID: 3390 RVA: 0x00027DAC File Offset: 0x00025FAC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public double3x4(double3 c0, double3 c1, double3 c2, double3 c3)
		{
			this.c0 = c0;
			this.c1 = c1;
			this.c2 = c2;
			this.c3 = c3;
		}

		// Token: 0x06000D3F RID: 3391 RVA: 0x00027DCC File Offset: 0x00025FCC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public double3x4(double m00, double m01, double m02, double m03, double m10, double m11, double m12, double m13, double m20, double m21, double m22, double m23)
		{
			this.c0 = new double3(m00, m10, m20);
			this.c1 = new double3(m01, m11, m21);
			this.c2 = new double3(m02, m12, m22);
			this.c3 = new double3(m03, m13, m23);
		}

		// Token: 0x06000D40 RID: 3392 RVA: 0x00027E1A File Offset: 0x0002601A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public double3x4(double v)
		{
			this.c0 = v;
			this.c1 = v;
			this.c2 = v;
			this.c3 = v;
		}

		// Token: 0x06000D41 RID: 3393 RVA: 0x00027E4C File Offset: 0x0002604C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public double3x4(bool v)
		{
			this.c0 = math.select(new double3(0.0), new double3(1.0), v);
			this.c1 = math.select(new double3(0.0), new double3(1.0), v);
			this.c2 = math.select(new double3(0.0), new double3(1.0), v);
			this.c3 = math.select(new double3(0.0), new double3(1.0), v);
		}

		// Token: 0x06000D42 RID: 3394 RVA: 0x00027EFC File Offset: 0x000260FC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public double3x4(bool3x4 v)
		{
			this.c0 = math.select(new double3(0.0), new double3(1.0), v.c0);
			this.c1 = math.select(new double3(0.0), new double3(1.0), v.c1);
			this.c2 = math.select(new double3(0.0), new double3(1.0), v.c2);
			this.c3 = math.select(new double3(0.0), new double3(1.0), v.c3);
		}

		// Token: 0x06000D43 RID: 3395 RVA: 0x00027FBD File Offset: 0x000261BD
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public double3x4(int v)
		{
			this.c0 = v;
			this.c1 = v;
			this.c2 = v;
			this.c3 = v;
		}

		// Token: 0x06000D44 RID: 3396 RVA: 0x00027FF0 File Offset: 0x000261F0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public double3x4(int3x4 v)
		{
			this.c0 = v.c0;
			this.c1 = v.c1;
			this.c2 = v.c2;
			this.c3 = v.c3;
		}

		// Token: 0x06000D45 RID: 3397 RVA: 0x00028041 File Offset: 0x00026241
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public double3x4(uint v)
		{
			this.c0 = v;
			this.c1 = v;
			this.c2 = v;
			this.c3 = v;
		}

		// Token: 0x06000D46 RID: 3398 RVA: 0x00028074 File Offset: 0x00026274
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public double3x4(uint3x4 v)
		{
			this.c0 = v.c0;
			this.c1 = v.c1;
			this.c2 = v.c2;
			this.c3 = v.c3;
		}

		// Token: 0x06000D47 RID: 3399 RVA: 0x000280C5 File Offset: 0x000262C5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public double3x4(float v)
		{
			this.c0 = v;
			this.c1 = v;
			this.c2 = v;
			this.c3 = v;
		}

		// Token: 0x06000D48 RID: 3400 RVA: 0x000280F8 File Offset: 0x000262F8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public double3x4(float3x4 v)
		{
			this.c0 = v.c0;
			this.c1 = v.c1;
			this.c2 = v.c2;
			this.c3 = v.c3;
		}

		// Token: 0x06000D49 RID: 3401 RVA: 0x00028149 File Offset: 0x00026349
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator double3x4(double v)
		{
			return new double3x4(v);
		}

		// Token: 0x06000D4A RID: 3402 RVA: 0x00028151 File Offset: 0x00026351
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator double3x4(bool v)
		{
			return new double3x4(v);
		}

		// Token: 0x06000D4B RID: 3403 RVA: 0x00028159 File Offset: 0x00026359
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator double3x4(bool3x4 v)
		{
			return new double3x4(v);
		}

		// Token: 0x06000D4C RID: 3404 RVA: 0x00028161 File Offset: 0x00026361
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator double3x4(int v)
		{
			return new double3x4(v);
		}

		// Token: 0x06000D4D RID: 3405 RVA: 0x00028169 File Offset: 0x00026369
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator double3x4(int3x4 v)
		{
			return new double3x4(v);
		}

		// Token: 0x06000D4E RID: 3406 RVA: 0x00028171 File Offset: 0x00026371
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator double3x4(uint v)
		{
			return new double3x4(v);
		}

		// Token: 0x06000D4F RID: 3407 RVA: 0x00028179 File Offset: 0x00026379
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator double3x4(uint3x4 v)
		{
			return new double3x4(v);
		}

		// Token: 0x06000D50 RID: 3408 RVA: 0x00028181 File Offset: 0x00026381
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator double3x4(float v)
		{
			return new double3x4(v);
		}

		// Token: 0x06000D51 RID: 3409 RVA: 0x00028189 File Offset: 0x00026389
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator double3x4(float3x4 v)
		{
			return new double3x4(v);
		}

		// Token: 0x06000D52 RID: 3410 RVA: 0x00028194 File Offset: 0x00026394
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3x4 operator *(double3x4 lhs, double3x4 rhs)
		{
			return new double3x4(lhs.c0 * rhs.c0, lhs.c1 * rhs.c1, lhs.c2 * rhs.c2, lhs.c3 * rhs.c3);
		}

		// Token: 0x06000D53 RID: 3411 RVA: 0x000281EA File Offset: 0x000263EA
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3x4 operator *(double3x4 lhs, double rhs)
		{
			return new double3x4(lhs.c0 * rhs, lhs.c1 * rhs, lhs.c2 * rhs, lhs.c3 * rhs);
		}

		// Token: 0x06000D54 RID: 3412 RVA: 0x00028221 File Offset: 0x00026421
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3x4 operator *(double lhs, double3x4 rhs)
		{
			return new double3x4(lhs * rhs.c0, lhs * rhs.c1, lhs * rhs.c2, lhs * rhs.c3);
		}

		// Token: 0x06000D55 RID: 3413 RVA: 0x00028258 File Offset: 0x00026458
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3x4 operator +(double3x4 lhs, double3x4 rhs)
		{
			return new double3x4(lhs.c0 + rhs.c0, lhs.c1 + rhs.c1, lhs.c2 + rhs.c2, lhs.c3 + rhs.c3);
		}

		// Token: 0x06000D56 RID: 3414 RVA: 0x000282AE File Offset: 0x000264AE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3x4 operator +(double3x4 lhs, double rhs)
		{
			return new double3x4(lhs.c0 + rhs, lhs.c1 + rhs, lhs.c2 + rhs, lhs.c3 + rhs);
		}

		// Token: 0x06000D57 RID: 3415 RVA: 0x000282E5 File Offset: 0x000264E5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3x4 operator +(double lhs, double3x4 rhs)
		{
			return new double3x4(lhs + rhs.c0, lhs + rhs.c1, lhs + rhs.c2, lhs + rhs.c3);
		}

		// Token: 0x06000D58 RID: 3416 RVA: 0x0002831C File Offset: 0x0002651C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3x4 operator -(double3x4 lhs, double3x4 rhs)
		{
			return new double3x4(lhs.c0 - rhs.c0, lhs.c1 - rhs.c1, lhs.c2 - rhs.c2, lhs.c3 - rhs.c3);
		}

		// Token: 0x06000D59 RID: 3417 RVA: 0x00028372 File Offset: 0x00026572
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3x4 operator -(double3x4 lhs, double rhs)
		{
			return new double3x4(lhs.c0 - rhs, lhs.c1 - rhs, lhs.c2 - rhs, lhs.c3 - rhs);
		}

		// Token: 0x06000D5A RID: 3418 RVA: 0x000283A9 File Offset: 0x000265A9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3x4 operator -(double lhs, double3x4 rhs)
		{
			return new double3x4(lhs - rhs.c0, lhs - rhs.c1, lhs - rhs.c2, lhs - rhs.c3);
		}

		// Token: 0x06000D5B RID: 3419 RVA: 0x000283E0 File Offset: 0x000265E0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3x4 operator /(double3x4 lhs, double3x4 rhs)
		{
			return new double3x4(lhs.c0 / rhs.c0, lhs.c1 / rhs.c1, lhs.c2 / rhs.c2, lhs.c3 / rhs.c3);
		}

		// Token: 0x06000D5C RID: 3420 RVA: 0x00028436 File Offset: 0x00026636
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3x4 operator /(double3x4 lhs, double rhs)
		{
			return new double3x4(lhs.c0 / rhs, lhs.c1 / rhs, lhs.c2 / rhs, lhs.c3 / rhs);
		}

		// Token: 0x06000D5D RID: 3421 RVA: 0x0002846D File Offset: 0x0002666D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3x4 operator /(double lhs, double3x4 rhs)
		{
			return new double3x4(lhs / rhs.c0, lhs / rhs.c1, lhs / rhs.c2, lhs / rhs.c3);
		}

		// Token: 0x06000D5E RID: 3422 RVA: 0x000284A4 File Offset: 0x000266A4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3x4 operator %(double3x4 lhs, double3x4 rhs)
		{
			return new double3x4(lhs.c0 % rhs.c0, lhs.c1 % rhs.c1, lhs.c2 % rhs.c2, lhs.c3 % rhs.c3);
		}

		// Token: 0x06000D5F RID: 3423 RVA: 0x000284FA File Offset: 0x000266FA
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3x4 operator %(double3x4 lhs, double rhs)
		{
			return new double3x4(lhs.c0 % rhs, lhs.c1 % rhs, lhs.c2 % rhs, lhs.c3 % rhs);
		}

		// Token: 0x06000D60 RID: 3424 RVA: 0x00028531 File Offset: 0x00026731
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3x4 operator %(double lhs, double3x4 rhs)
		{
			return new double3x4(lhs % rhs.c0, lhs % rhs.c1, lhs % rhs.c2, lhs % rhs.c3);
		}

		// Token: 0x06000D61 RID: 3425 RVA: 0x00028568 File Offset: 0x00026768
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3x4 operator ++(double3x4 val)
		{
			double3 @double = ++val.c0;
			val.c0 = @double;
			double3 double2 = @double;
			@double = ++val.c1;
			val.c1 = @double;
			double3 double3 = @double;
			@double = ++val.c2;
			val.c2 = @double;
			double3 double4 = @double;
			@double = ++val.c3;
			val.c3 = @double;
			return new double3x4(double2, double3, double4, @double);
		}

		// Token: 0x06000D62 RID: 3426 RVA: 0x000285E4 File Offset: 0x000267E4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3x4 operator --(double3x4 val)
		{
			double3 @double = --val.c0;
			val.c0 = @double;
			double3 double2 = @double;
			@double = --val.c1;
			val.c1 = @double;
			double3 double3 = @double;
			@double = --val.c2;
			val.c2 = @double;
			double3 double4 = @double;
			@double = --val.c3;
			val.c3 = @double;
			return new double3x4(double2, double3, double4, @double);
		}

		// Token: 0x06000D63 RID: 3427 RVA: 0x00028660 File Offset: 0x00026860
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x4 operator <(double3x4 lhs, double3x4 rhs)
		{
			return new bool3x4(lhs.c0 < rhs.c0, lhs.c1 < rhs.c1, lhs.c2 < rhs.c2, lhs.c3 < rhs.c3);
		}

		// Token: 0x06000D64 RID: 3428 RVA: 0x000286B6 File Offset: 0x000268B6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x4 operator <(double3x4 lhs, double rhs)
		{
			return new bool3x4(lhs.c0 < rhs, lhs.c1 < rhs, lhs.c2 < rhs, lhs.c3 < rhs);
		}

		// Token: 0x06000D65 RID: 3429 RVA: 0x000286ED File Offset: 0x000268ED
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x4 operator <(double lhs, double3x4 rhs)
		{
			return new bool3x4(lhs < rhs.c0, lhs < rhs.c1, lhs < rhs.c2, lhs < rhs.c3);
		}

		// Token: 0x06000D66 RID: 3430 RVA: 0x00028724 File Offset: 0x00026924
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x4 operator <=(double3x4 lhs, double3x4 rhs)
		{
			return new bool3x4(lhs.c0 <= rhs.c0, lhs.c1 <= rhs.c1, lhs.c2 <= rhs.c2, lhs.c3 <= rhs.c3);
		}

		// Token: 0x06000D67 RID: 3431 RVA: 0x0002877A File Offset: 0x0002697A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x4 operator <=(double3x4 lhs, double rhs)
		{
			return new bool3x4(lhs.c0 <= rhs, lhs.c1 <= rhs, lhs.c2 <= rhs, lhs.c3 <= rhs);
		}

		// Token: 0x06000D68 RID: 3432 RVA: 0x000287B1 File Offset: 0x000269B1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x4 operator <=(double lhs, double3x4 rhs)
		{
			return new bool3x4(lhs <= rhs.c0, lhs <= rhs.c1, lhs <= rhs.c2, lhs <= rhs.c3);
		}

		// Token: 0x06000D69 RID: 3433 RVA: 0x000287E8 File Offset: 0x000269E8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x4 operator >(double3x4 lhs, double3x4 rhs)
		{
			return new bool3x4(lhs.c0 > rhs.c0, lhs.c1 > rhs.c1, lhs.c2 > rhs.c2, lhs.c3 > rhs.c3);
		}

		// Token: 0x06000D6A RID: 3434 RVA: 0x0002883E File Offset: 0x00026A3E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x4 operator >(double3x4 lhs, double rhs)
		{
			return new bool3x4(lhs.c0 > rhs, lhs.c1 > rhs, lhs.c2 > rhs, lhs.c3 > rhs);
		}

		// Token: 0x06000D6B RID: 3435 RVA: 0x00028875 File Offset: 0x00026A75
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x4 operator >(double lhs, double3x4 rhs)
		{
			return new bool3x4(lhs > rhs.c0, lhs > rhs.c1, lhs > rhs.c2, lhs > rhs.c3);
		}

		// Token: 0x06000D6C RID: 3436 RVA: 0x000288AC File Offset: 0x00026AAC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x4 operator >=(double3x4 lhs, double3x4 rhs)
		{
			return new bool3x4(lhs.c0 >= rhs.c0, lhs.c1 >= rhs.c1, lhs.c2 >= rhs.c2, lhs.c3 >= rhs.c3);
		}

		// Token: 0x06000D6D RID: 3437 RVA: 0x00028902 File Offset: 0x00026B02
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x4 operator >=(double3x4 lhs, double rhs)
		{
			return new bool3x4(lhs.c0 >= rhs, lhs.c1 >= rhs, lhs.c2 >= rhs, lhs.c3 >= rhs);
		}

		// Token: 0x06000D6E RID: 3438 RVA: 0x00028939 File Offset: 0x00026B39
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x4 operator >=(double lhs, double3x4 rhs)
		{
			return new bool3x4(lhs >= rhs.c0, lhs >= rhs.c1, lhs >= rhs.c2, lhs >= rhs.c3);
		}

		// Token: 0x06000D6F RID: 3439 RVA: 0x00028970 File Offset: 0x00026B70
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3x4 operator -(double3x4 val)
		{
			return new double3x4(-val.c0, -val.c1, -val.c2, -val.c3);
		}

		// Token: 0x06000D70 RID: 3440 RVA: 0x000289A3 File Offset: 0x00026BA3
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3x4 operator +(double3x4 val)
		{
			return new double3x4(+val.c0, +val.c1, +val.c2, +val.c3);
		}

		// Token: 0x06000D71 RID: 3441 RVA: 0x000289D8 File Offset: 0x00026BD8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x4 operator ==(double3x4 lhs, double3x4 rhs)
		{
			return new bool3x4(lhs.c0 == rhs.c0, lhs.c1 == rhs.c1, lhs.c2 == rhs.c2, lhs.c3 == rhs.c3);
		}

		// Token: 0x06000D72 RID: 3442 RVA: 0x00028A2E File Offset: 0x00026C2E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x4 operator ==(double3x4 lhs, double rhs)
		{
			return new bool3x4(lhs.c0 == rhs, lhs.c1 == rhs, lhs.c2 == rhs, lhs.c3 == rhs);
		}

		// Token: 0x06000D73 RID: 3443 RVA: 0x00028A65 File Offset: 0x00026C65
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x4 operator ==(double lhs, double3x4 rhs)
		{
			return new bool3x4(lhs == rhs.c0, lhs == rhs.c1, lhs == rhs.c2, lhs == rhs.c3);
		}

		// Token: 0x06000D74 RID: 3444 RVA: 0x00028A9C File Offset: 0x00026C9C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x4 operator !=(double3x4 lhs, double3x4 rhs)
		{
			return new bool3x4(lhs.c0 != rhs.c0, lhs.c1 != rhs.c1, lhs.c2 != rhs.c2, lhs.c3 != rhs.c3);
		}

		// Token: 0x06000D75 RID: 3445 RVA: 0x00028AF2 File Offset: 0x00026CF2
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x4 operator !=(double3x4 lhs, double rhs)
		{
			return new bool3x4(lhs.c0 != rhs, lhs.c1 != rhs, lhs.c2 != rhs, lhs.c3 != rhs);
		}

		// Token: 0x06000D76 RID: 3446 RVA: 0x00028B29 File Offset: 0x00026D29
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x4 operator !=(double lhs, double3x4 rhs)
		{
			return new bool3x4(lhs != rhs.c0, lhs != rhs.c1, lhs != rhs.c2, lhs != rhs.c3);
		}

		// Token: 0x17000286 RID: 646
		public unsafe double3 this[int index]
		{
			get
			{
				fixed (double3x4* ptr = &this)
				{
					return ref *(double3*)(ptr + (IntPtr)index * (IntPtr)sizeof(double3) / (IntPtr)sizeof(double3x4));
				}
			}
		}

		// Token: 0x06000D78 RID: 3448 RVA: 0x00028B7C File Offset: 0x00026D7C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool Equals(double3x4 rhs)
		{
			return this.c0.Equals(rhs.c0) && this.c1.Equals(rhs.c1) && this.c2.Equals(rhs.c2) && this.c3.Equals(rhs.c3);
		}

		// Token: 0x06000D79 RID: 3449 RVA: 0x00028BD8 File Offset: 0x00026DD8
		public override bool Equals(object o)
		{
			if (o is double3x4)
			{
				double3x4 rhs = (double3x4)o;
				return this.Equals(rhs);
			}
			return false;
		}

		// Token: 0x06000D7A RID: 3450 RVA: 0x00028BFD File Offset: 0x00026DFD
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override int GetHashCode()
		{
			return (int)math.hash(this);
		}

		// Token: 0x06000D7B RID: 3451 RVA: 0x00028C0C File Offset: 0x00026E0C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override string ToString()
		{
			return string.Format("double3x4({0}, {1}, {2}, {3},  {4}, {5}, {6}, {7},  {8}, {9}, {10}, {11})", new object[]
			{
				this.c0.x,
				this.c1.x,
				this.c2.x,
				this.c3.x,
				this.c0.y,
				this.c1.y,
				this.c2.y,
				this.c3.y,
				this.c0.z,
				this.c1.z,
				this.c2.z,
				this.c3.z
			});
		}

		// Token: 0x06000D7C RID: 3452 RVA: 0x00028D14 File Offset: 0x00026F14
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public string ToString(string format, IFormatProvider formatProvider)
		{
			return string.Format("double3x4({0}, {1}, {2}, {3},  {4}, {5}, {6}, {7},  {8}, {9}, {10}, {11})", new object[]
			{
				this.c0.x.ToString(format, formatProvider),
				this.c1.x.ToString(format, formatProvider),
				this.c2.x.ToString(format, formatProvider),
				this.c3.x.ToString(format, formatProvider),
				this.c0.y.ToString(format, formatProvider),
				this.c1.y.ToString(format, formatProvider),
				this.c2.y.ToString(format, formatProvider),
				this.c3.y.ToString(format, formatProvider),
				this.c0.z.ToString(format, formatProvider),
				this.c1.z.ToString(format, formatProvider),
				this.c2.z.ToString(format, formatProvider),
				this.c3.z.ToString(format, formatProvider)
			});
		}

		// Token: 0x04000057 RID: 87
		public double3 c0;

		// Token: 0x04000058 RID: 88
		public double3 c1;

		// Token: 0x04000059 RID: 89
		public double3 c2;

		// Token: 0x0400005A RID: 90
		public double3 c3;

		// Token: 0x0400005B RID: 91
		public static readonly double3x4 zero;
	}
}

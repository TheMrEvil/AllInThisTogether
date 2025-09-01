using System;
using System.Runtime.CompilerServices;
using Unity.IL2CPP.CompilerServices;

namespace Unity.Mathematics
{
	// Token: 0x02000015 RID: 21
	[Il2CppEagerStaticClassConstruction]
	[Serializable]
	public struct double3x2 : IEquatable<double3x2>, IFormattable
	{
		// Token: 0x06000CBF RID: 3263 RVA: 0x000267F6 File Offset: 0x000249F6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public double3x2(double3 c0, double3 c1)
		{
			this.c0 = c0;
			this.c1 = c1;
		}

		// Token: 0x06000CC0 RID: 3264 RVA: 0x00026806 File Offset: 0x00024A06
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public double3x2(double m00, double m01, double m10, double m11, double m20, double m21)
		{
			this.c0 = new double3(m00, m10, m20);
			this.c1 = new double3(m01, m11, m21);
		}

		// Token: 0x06000CC1 RID: 3265 RVA: 0x00026827 File Offset: 0x00024A27
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public double3x2(double v)
		{
			this.c0 = v;
			this.c1 = v;
		}

		// Token: 0x06000CC2 RID: 3266 RVA: 0x00026844 File Offset: 0x00024A44
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public double3x2(bool v)
		{
			this.c0 = math.select(new double3(0.0), new double3(1.0), v);
			this.c1 = math.select(new double3(0.0), new double3(1.0), v);
		}

		// Token: 0x06000CC3 RID: 3267 RVA: 0x000268A4 File Offset: 0x00024AA4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public double3x2(bool3x2 v)
		{
			this.c0 = math.select(new double3(0.0), new double3(1.0), v.c0);
			this.c1 = math.select(new double3(0.0), new double3(1.0), v.c1);
		}

		// Token: 0x06000CC4 RID: 3268 RVA: 0x0002690B File Offset: 0x00024B0B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public double3x2(int v)
		{
			this.c0 = v;
			this.c1 = v;
		}

		// Token: 0x06000CC5 RID: 3269 RVA: 0x00026925 File Offset: 0x00024B25
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public double3x2(int3x2 v)
		{
			this.c0 = v.c0;
			this.c1 = v.c1;
		}

		// Token: 0x06000CC6 RID: 3270 RVA: 0x00026949 File Offset: 0x00024B49
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public double3x2(uint v)
		{
			this.c0 = v;
			this.c1 = v;
		}

		// Token: 0x06000CC7 RID: 3271 RVA: 0x00026963 File Offset: 0x00024B63
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public double3x2(uint3x2 v)
		{
			this.c0 = v.c0;
			this.c1 = v.c1;
		}

		// Token: 0x06000CC8 RID: 3272 RVA: 0x00026987 File Offset: 0x00024B87
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public double3x2(float v)
		{
			this.c0 = v;
			this.c1 = v;
		}

		// Token: 0x06000CC9 RID: 3273 RVA: 0x000269A1 File Offset: 0x00024BA1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public double3x2(float3x2 v)
		{
			this.c0 = v.c0;
			this.c1 = v.c1;
		}

		// Token: 0x06000CCA RID: 3274 RVA: 0x000269C5 File Offset: 0x00024BC5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator double3x2(double v)
		{
			return new double3x2(v);
		}

		// Token: 0x06000CCB RID: 3275 RVA: 0x000269CD File Offset: 0x00024BCD
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator double3x2(bool v)
		{
			return new double3x2(v);
		}

		// Token: 0x06000CCC RID: 3276 RVA: 0x000269D5 File Offset: 0x00024BD5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator double3x2(bool3x2 v)
		{
			return new double3x2(v);
		}

		// Token: 0x06000CCD RID: 3277 RVA: 0x000269DD File Offset: 0x00024BDD
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator double3x2(int v)
		{
			return new double3x2(v);
		}

		// Token: 0x06000CCE RID: 3278 RVA: 0x000269E5 File Offset: 0x00024BE5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator double3x2(int3x2 v)
		{
			return new double3x2(v);
		}

		// Token: 0x06000CCF RID: 3279 RVA: 0x000269ED File Offset: 0x00024BED
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator double3x2(uint v)
		{
			return new double3x2(v);
		}

		// Token: 0x06000CD0 RID: 3280 RVA: 0x000269F5 File Offset: 0x00024BF5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator double3x2(uint3x2 v)
		{
			return new double3x2(v);
		}

		// Token: 0x06000CD1 RID: 3281 RVA: 0x000269FD File Offset: 0x00024BFD
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator double3x2(float v)
		{
			return new double3x2(v);
		}

		// Token: 0x06000CD2 RID: 3282 RVA: 0x00026A05 File Offset: 0x00024C05
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator double3x2(float3x2 v)
		{
			return new double3x2(v);
		}

		// Token: 0x06000CD3 RID: 3283 RVA: 0x00026A0D File Offset: 0x00024C0D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3x2 operator *(double3x2 lhs, double3x2 rhs)
		{
			return new double3x2(lhs.c0 * rhs.c0, lhs.c1 * rhs.c1);
		}

		// Token: 0x06000CD4 RID: 3284 RVA: 0x00026A36 File Offset: 0x00024C36
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3x2 operator *(double3x2 lhs, double rhs)
		{
			return new double3x2(lhs.c0 * rhs, lhs.c1 * rhs);
		}

		// Token: 0x06000CD5 RID: 3285 RVA: 0x00026A55 File Offset: 0x00024C55
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3x2 operator *(double lhs, double3x2 rhs)
		{
			return new double3x2(lhs * rhs.c0, lhs * rhs.c1);
		}

		// Token: 0x06000CD6 RID: 3286 RVA: 0x00026A74 File Offset: 0x00024C74
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3x2 operator +(double3x2 lhs, double3x2 rhs)
		{
			return new double3x2(lhs.c0 + rhs.c0, lhs.c1 + rhs.c1);
		}

		// Token: 0x06000CD7 RID: 3287 RVA: 0x00026A9D File Offset: 0x00024C9D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3x2 operator +(double3x2 lhs, double rhs)
		{
			return new double3x2(lhs.c0 + rhs, lhs.c1 + rhs);
		}

		// Token: 0x06000CD8 RID: 3288 RVA: 0x00026ABC File Offset: 0x00024CBC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3x2 operator +(double lhs, double3x2 rhs)
		{
			return new double3x2(lhs + rhs.c0, lhs + rhs.c1);
		}

		// Token: 0x06000CD9 RID: 3289 RVA: 0x00026ADB File Offset: 0x00024CDB
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3x2 operator -(double3x2 lhs, double3x2 rhs)
		{
			return new double3x2(lhs.c0 - rhs.c0, lhs.c1 - rhs.c1);
		}

		// Token: 0x06000CDA RID: 3290 RVA: 0x00026B04 File Offset: 0x00024D04
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3x2 operator -(double3x2 lhs, double rhs)
		{
			return new double3x2(lhs.c0 - rhs, lhs.c1 - rhs);
		}

		// Token: 0x06000CDB RID: 3291 RVA: 0x00026B23 File Offset: 0x00024D23
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3x2 operator -(double lhs, double3x2 rhs)
		{
			return new double3x2(lhs - rhs.c0, lhs - rhs.c1);
		}

		// Token: 0x06000CDC RID: 3292 RVA: 0x00026B42 File Offset: 0x00024D42
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3x2 operator /(double3x2 lhs, double3x2 rhs)
		{
			return new double3x2(lhs.c0 / rhs.c0, lhs.c1 / rhs.c1);
		}

		// Token: 0x06000CDD RID: 3293 RVA: 0x00026B6B File Offset: 0x00024D6B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3x2 operator /(double3x2 lhs, double rhs)
		{
			return new double3x2(lhs.c0 / rhs, lhs.c1 / rhs);
		}

		// Token: 0x06000CDE RID: 3294 RVA: 0x00026B8A File Offset: 0x00024D8A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3x2 operator /(double lhs, double3x2 rhs)
		{
			return new double3x2(lhs / rhs.c0, lhs / rhs.c1);
		}

		// Token: 0x06000CDF RID: 3295 RVA: 0x00026BA9 File Offset: 0x00024DA9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3x2 operator %(double3x2 lhs, double3x2 rhs)
		{
			return new double3x2(lhs.c0 % rhs.c0, lhs.c1 % rhs.c1);
		}

		// Token: 0x06000CE0 RID: 3296 RVA: 0x00026BD2 File Offset: 0x00024DD2
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3x2 operator %(double3x2 lhs, double rhs)
		{
			return new double3x2(lhs.c0 % rhs, lhs.c1 % rhs);
		}

		// Token: 0x06000CE1 RID: 3297 RVA: 0x00026BF1 File Offset: 0x00024DF1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3x2 operator %(double lhs, double3x2 rhs)
		{
			return new double3x2(lhs % rhs.c0, lhs % rhs.c1);
		}

		// Token: 0x06000CE2 RID: 3298 RVA: 0x00026C10 File Offset: 0x00024E10
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3x2 operator ++(double3x2 val)
		{
			double3 @double = ++val.c0;
			val.c0 = @double;
			double3 double2 = @double;
			@double = ++val.c1;
			val.c1 = @double;
			return new double3x2(double2, @double);
		}

		// Token: 0x06000CE3 RID: 3299 RVA: 0x00026C58 File Offset: 0x00024E58
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3x2 operator --(double3x2 val)
		{
			double3 @double = --val.c0;
			val.c0 = @double;
			double3 double2 = @double;
			@double = --val.c1;
			val.c1 = @double;
			return new double3x2(double2, @double);
		}

		// Token: 0x06000CE4 RID: 3300 RVA: 0x00026C9E File Offset: 0x00024E9E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x2 operator <(double3x2 lhs, double3x2 rhs)
		{
			return new bool3x2(lhs.c0 < rhs.c0, lhs.c1 < rhs.c1);
		}

		// Token: 0x06000CE5 RID: 3301 RVA: 0x00026CC7 File Offset: 0x00024EC7
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x2 operator <(double3x2 lhs, double rhs)
		{
			return new bool3x2(lhs.c0 < rhs, lhs.c1 < rhs);
		}

		// Token: 0x06000CE6 RID: 3302 RVA: 0x00026CE6 File Offset: 0x00024EE6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x2 operator <(double lhs, double3x2 rhs)
		{
			return new bool3x2(lhs < rhs.c0, lhs < rhs.c1);
		}

		// Token: 0x06000CE7 RID: 3303 RVA: 0x00026D05 File Offset: 0x00024F05
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x2 operator <=(double3x2 lhs, double3x2 rhs)
		{
			return new bool3x2(lhs.c0 <= rhs.c0, lhs.c1 <= rhs.c1);
		}

		// Token: 0x06000CE8 RID: 3304 RVA: 0x00026D2E File Offset: 0x00024F2E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x2 operator <=(double3x2 lhs, double rhs)
		{
			return new bool3x2(lhs.c0 <= rhs, lhs.c1 <= rhs);
		}

		// Token: 0x06000CE9 RID: 3305 RVA: 0x00026D4D File Offset: 0x00024F4D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x2 operator <=(double lhs, double3x2 rhs)
		{
			return new bool3x2(lhs <= rhs.c0, lhs <= rhs.c1);
		}

		// Token: 0x06000CEA RID: 3306 RVA: 0x00026D6C File Offset: 0x00024F6C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x2 operator >(double3x2 lhs, double3x2 rhs)
		{
			return new bool3x2(lhs.c0 > rhs.c0, lhs.c1 > rhs.c1);
		}

		// Token: 0x06000CEB RID: 3307 RVA: 0x00026D95 File Offset: 0x00024F95
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x2 operator >(double3x2 lhs, double rhs)
		{
			return new bool3x2(lhs.c0 > rhs, lhs.c1 > rhs);
		}

		// Token: 0x06000CEC RID: 3308 RVA: 0x00026DB4 File Offset: 0x00024FB4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x2 operator >(double lhs, double3x2 rhs)
		{
			return new bool3x2(lhs > rhs.c0, lhs > rhs.c1);
		}

		// Token: 0x06000CED RID: 3309 RVA: 0x00026DD3 File Offset: 0x00024FD3
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x2 operator >=(double3x2 lhs, double3x2 rhs)
		{
			return new bool3x2(lhs.c0 >= rhs.c0, lhs.c1 >= rhs.c1);
		}

		// Token: 0x06000CEE RID: 3310 RVA: 0x00026DFC File Offset: 0x00024FFC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x2 operator >=(double3x2 lhs, double rhs)
		{
			return new bool3x2(lhs.c0 >= rhs, lhs.c1 >= rhs);
		}

		// Token: 0x06000CEF RID: 3311 RVA: 0x00026E1B File Offset: 0x0002501B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x2 operator >=(double lhs, double3x2 rhs)
		{
			return new bool3x2(lhs >= rhs.c0, lhs >= rhs.c1);
		}

		// Token: 0x06000CF0 RID: 3312 RVA: 0x00026E3A File Offset: 0x0002503A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3x2 operator -(double3x2 val)
		{
			return new double3x2(-val.c0, -val.c1);
		}

		// Token: 0x06000CF1 RID: 3313 RVA: 0x00026E57 File Offset: 0x00025057
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3x2 operator +(double3x2 val)
		{
			return new double3x2(+val.c0, +val.c1);
		}

		// Token: 0x06000CF2 RID: 3314 RVA: 0x00026E74 File Offset: 0x00025074
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x2 operator ==(double3x2 lhs, double3x2 rhs)
		{
			return new bool3x2(lhs.c0 == rhs.c0, lhs.c1 == rhs.c1);
		}

		// Token: 0x06000CF3 RID: 3315 RVA: 0x00026E9D File Offset: 0x0002509D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x2 operator ==(double3x2 lhs, double rhs)
		{
			return new bool3x2(lhs.c0 == rhs, lhs.c1 == rhs);
		}

		// Token: 0x06000CF4 RID: 3316 RVA: 0x00026EBC File Offset: 0x000250BC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x2 operator ==(double lhs, double3x2 rhs)
		{
			return new bool3x2(lhs == rhs.c0, lhs == rhs.c1);
		}

		// Token: 0x06000CF5 RID: 3317 RVA: 0x00026EDB File Offset: 0x000250DB
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x2 operator !=(double3x2 lhs, double3x2 rhs)
		{
			return new bool3x2(lhs.c0 != rhs.c0, lhs.c1 != rhs.c1);
		}

		// Token: 0x06000CF6 RID: 3318 RVA: 0x00026F04 File Offset: 0x00025104
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x2 operator !=(double3x2 lhs, double rhs)
		{
			return new bool3x2(lhs.c0 != rhs, lhs.c1 != rhs);
		}

		// Token: 0x06000CF7 RID: 3319 RVA: 0x00026F23 File Offset: 0x00025123
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x2 operator !=(double lhs, double3x2 rhs)
		{
			return new bool3x2(lhs != rhs.c0, lhs != rhs.c1);
		}

		// Token: 0x17000284 RID: 644
		public unsafe double3 this[int index]
		{
			get
			{
				fixed (double3x2* ptr = &this)
				{
					return ref *(double3*)(ptr + (IntPtr)index * (IntPtr)sizeof(double3) / (IntPtr)sizeof(double3x2));
				}
			}
		}

		// Token: 0x06000CF9 RID: 3321 RVA: 0x00026F5F File Offset: 0x0002515F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool Equals(double3x2 rhs)
		{
			return this.c0.Equals(rhs.c0) && this.c1.Equals(rhs.c1);
		}

		// Token: 0x06000CFA RID: 3322 RVA: 0x00026F88 File Offset: 0x00025188
		public override bool Equals(object o)
		{
			if (o is double3x2)
			{
				double3x2 rhs = (double3x2)o;
				return this.Equals(rhs);
			}
			return false;
		}

		// Token: 0x06000CFB RID: 3323 RVA: 0x00026FAD File Offset: 0x000251AD
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override int GetHashCode()
		{
			return (int)math.hash(this);
		}

		// Token: 0x06000CFC RID: 3324 RVA: 0x00026FBC File Offset: 0x000251BC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override string ToString()
		{
			return string.Format("double3x2({0}, {1},  {2}, {3},  {4}, {5})", new object[]
			{
				this.c0.x,
				this.c1.x,
				this.c0.y,
				this.c1.y,
				this.c0.z,
				this.c1.z
			});
		}

		// Token: 0x06000CFD RID: 3325 RVA: 0x0002704C File Offset: 0x0002524C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public string ToString(string format, IFormatProvider formatProvider)
		{
			return string.Format("double3x2({0}, {1},  {2}, {3},  {4}, {5})", new object[]
			{
				this.c0.x.ToString(format, formatProvider),
				this.c1.x.ToString(format, formatProvider),
				this.c0.y.ToString(format, formatProvider),
				this.c1.y.ToString(format, formatProvider),
				this.c0.z.ToString(format, formatProvider),
				this.c1.z.ToString(format, formatProvider)
			});
		}

		// Token: 0x0400004F RID: 79
		public double3 c0;

		// Token: 0x04000050 RID: 80
		public double3 c1;

		// Token: 0x04000051 RID: 81
		public static readonly double3x2 zero;
	}
}

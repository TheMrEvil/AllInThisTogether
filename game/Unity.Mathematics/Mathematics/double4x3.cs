using System;
using System.Runtime.CompilerServices;
using Unity.IL2CPP.CompilerServices;

namespace Unity.Mathematics
{
	// Token: 0x0200001A RID: 26
	[Il2CppEagerStaticClassConstruction]
	[Serializable]
	public struct double4x3 : IEquatable<double4x3>, IFormattable
	{
		// Token: 0x06000F92 RID: 3986 RVA: 0x0002D511 File Offset: 0x0002B711
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public double4x3(double4 c0, double4 c1, double4 c2)
		{
			this.c0 = c0;
			this.c1 = c1;
			this.c2 = c2;
		}

		// Token: 0x06000F93 RID: 3987 RVA: 0x0002D528 File Offset: 0x0002B728
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public double4x3(double m00, double m01, double m02, double m10, double m11, double m12, double m20, double m21, double m22, double m30, double m31, double m32)
		{
			this.c0 = new double4(m00, m10, m20, m30);
			this.c1 = new double4(m01, m11, m21, m31);
			this.c2 = new double4(m02, m12, m22, m32);
		}

		// Token: 0x06000F94 RID: 3988 RVA: 0x0002D560 File Offset: 0x0002B760
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public double4x3(double v)
		{
			this.c0 = v;
			this.c1 = v;
			this.c2 = v;
		}

		// Token: 0x06000F95 RID: 3989 RVA: 0x0002D588 File Offset: 0x0002B788
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public double4x3(bool v)
		{
			this.c0 = math.select(new double4(0.0), new double4(1.0), v);
			this.c1 = math.select(new double4(0.0), new double4(1.0), v);
			this.c2 = math.select(new double4(0.0), new double4(1.0), v);
		}

		// Token: 0x06000F96 RID: 3990 RVA: 0x0002D610 File Offset: 0x0002B810
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public double4x3(bool4x3 v)
		{
			this.c0 = math.select(new double4(0.0), new double4(1.0), v.c0);
			this.c1 = math.select(new double4(0.0), new double4(1.0), v.c1);
			this.c2 = math.select(new double4(0.0), new double4(1.0), v.c2);
		}

		// Token: 0x06000F97 RID: 3991 RVA: 0x0002D6A4 File Offset: 0x0002B8A4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public double4x3(int v)
		{
			this.c0 = v;
			this.c1 = v;
			this.c2 = v;
		}

		// Token: 0x06000F98 RID: 3992 RVA: 0x0002D6CA File Offset: 0x0002B8CA
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public double4x3(int4x3 v)
		{
			this.c0 = v.c0;
			this.c1 = v.c1;
			this.c2 = v.c2;
		}

		// Token: 0x06000F99 RID: 3993 RVA: 0x0002D6FF File Offset: 0x0002B8FF
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public double4x3(uint v)
		{
			this.c0 = v;
			this.c1 = v;
			this.c2 = v;
		}

		// Token: 0x06000F9A RID: 3994 RVA: 0x0002D725 File Offset: 0x0002B925
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public double4x3(uint4x3 v)
		{
			this.c0 = v.c0;
			this.c1 = v.c1;
			this.c2 = v.c2;
		}

		// Token: 0x06000F9B RID: 3995 RVA: 0x0002D75A File Offset: 0x0002B95A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public double4x3(float v)
		{
			this.c0 = v;
			this.c1 = v;
			this.c2 = v;
		}

		// Token: 0x06000F9C RID: 3996 RVA: 0x0002D780 File Offset: 0x0002B980
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public double4x3(float4x3 v)
		{
			this.c0 = v.c0;
			this.c1 = v.c1;
			this.c2 = v.c2;
		}

		// Token: 0x06000F9D RID: 3997 RVA: 0x0002D7B5 File Offset: 0x0002B9B5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator double4x3(double v)
		{
			return new double4x3(v);
		}

		// Token: 0x06000F9E RID: 3998 RVA: 0x0002D7BD File Offset: 0x0002B9BD
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator double4x3(bool v)
		{
			return new double4x3(v);
		}

		// Token: 0x06000F9F RID: 3999 RVA: 0x0002D7C5 File Offset: 0x0002B9C5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator double4x3(bool4x3 v)
		{
			return new double4x3(v);
		}

		// Token: 0x06000FA0 RID: 4000 RVA: 0x0002D7CD File Offset: 0x0002B9CD
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator double4x3(int v)
		{
			return new double4x3(v);
		}

		// Token: 0x06000FA1 RID: 4001 RVA: 0x0002D7D5 File Offset: 0x0002B9D5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator double4x3(int4x3 v)
		{
			return new double4x3(v);
		}

		// Token: 0x06000FA2 RID: 4002 RVA: 0x0002D7DD File Offset: 0x0002B9DD
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator double4x3(uint v)
		{
			return new double4x3(v);
		}

		// Token: 0x06000FA3 RID: 4003 RVA: 0x0002D7E5 File Offset: 0x0002B9E5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator double4x3(uint4x3 v)
		{
			return new double4x3(v);
		}

		// Token: 0x06000FA4 RID: 4004 RVA: 0x0002D7ED File Offset: 0x0002B9ED
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator double4x3(float v)
		{
			return new double4x3(v);
		}

		// Token: 0x06000FA5 RID: 4005 RVA: 0x0002D7F5 File Offset: 0x0002B9F5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator double4x3(float4x3 v)
		{
			return new double4x3(v);
		}

		// Token: 0x06000FA6 RID: 4006 RVA: 0x0002D7FD File Offset: 0x0002B9FD
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4x3 operator *(double4x3 lhs, double4x3 rhs)
		{
			return new double4x3(lhs.c0 * rhs.c0, lhs.c1 * rhs.c1, lhs.c2 * rhs.c2);
		}

		// Token: 0x06000FA7 RID: 4007 RVA: 0x0002D837 File Offset: 0x0002BA37
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4x3 operator *(double4x3 lhs, double rhs)
		{
			return new double4x3(lhs.c0 * rhs, lhs.c1 * rhs, lhs.c2 * rhs);
		}

		// Token: 0x06000FA8 RID: 4008 RVA: 0x0002D862 File Offset: 0x0002BA62
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4x3 operator *(double lhs, double4x3 rhs)
		{
			return new double4x3(lhs * rhs.c0, lhs * rhs.c1, lhs * rhs.c2);
		}

		// Token: 0x06000FA9 RID: 4009 RVA: 0x0002D88D File Offset: 0x0002BA8D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4x3 operator +(double4x3 lhs, double4x3 rhs)
		{
			return new double4x3(lhs.c0 + rhs.c0, lhs.c1 + rhs.c1, lhs.c2 + rhs.c2);
		}

		// Token: 0x06000FAA RID: 4010 RVA: 0x0002D8C7 File Offset: 0x0002BAC7
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4x3 operator +(double4x3 lhs, double rhs)
		{
			return new double4x3(lhs.c0 + rhs, lhs.c1 + rhs, lhs.c2 + rhs);
		}

		// Token: 0x06000FAB RID: 4011 RVA: 0x0002D8F2 File Offset: 0x0002BAF2
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4x3 operator +(double lhs, double4x3 rhs)
		{
			return new double4x3(lhs + rhs.c0, lhs + rhs.c1, lhs + rhs.c2);
		}

		// Token: 0x06000FAC RID: 4012 RVA: 0x0002D91D File Offset: 0x0002BB1D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4x3 operator -(double4x3 lhs, double4x3 rhs)
		{
			return new double4x3(lhs.c0 - rhs.c0, lhs.c1 - rhs.c1, lhs.c2 - rhs.c2);
		}

		// Token: 0x06000FAD RID: 4013 RVA: 0x0002D957 File Offset: 0x0002BB57
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4x3 operator -(double4x3 lhs, double rhs)
		{
			return new double4x3(lhs.c0 - rhs, lhs.c1 - rhs, lhs.c2 - rhs);
		}

		// Token: 0x06000FAE RID: 4014 RVA: 0x0002D982 File Offset: 0x0002BB82
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4x3 operator -(double lhs, double4x3 rhs)
		{
			return new double4x3(lhs - rhs.c0, lhs - rhs.c1, lhs - rhs.c2);
		}

		// Token: 0x06000FAF RID: 4015 RVA: 0x0002D9AD File Offset: 0x0002BBAD
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4x3 operator /(double4x3 lhs, double4x3 rhs)
		{
			return new double4x3(lhs.c0 / rhs.c0, lhs.c1 / rhs.c1, lhs.c2 / rhs.c2);
		}

		// Token: 0x06000FB0 RID: 4016 RVA: 0x0002D9E7 File Offset: 0x0002BBE7
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4x3 operator /(double4x3 lhs, double rhs)
		{
			return new double4x3(lhs.c0 / rhs, lhs.c1 / rhs, lhs.c2 / rhs);
		}

		// Token: 0x06000FB1 RID: 4017 RVA: 0x0002DA12 File Offset: 0x0002BC12
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4x3 operator /(double lhs, double4x3 rhs)
		{
			return new double4x3(lhs / rhs.c0, lhs / rhs.c1, lhs / rhs.c2);
		}

		// Token: 0x06000FB2 RID: 4018 RVA: 0x0002DA3D File Offset: 0x0002BC3D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4x3 operator %(double4x3 lhs, double4x3 rhs)
		{
			return new double4x3(lhs.c0 % rhs.c0, lhs.c1 % rhs.c1, lhs.c2 % rhs.c2);
		}

		// Token: 0x06000FB3 RID: 4019 RVA: 0x0002DA77 File Offset: 0x0002BC77
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4x3 operator %(double4x3 lhs, double rhs)
		{
			return new double4x3(lhs.c0 % rhs, lhs.c1 % rhs, lhs.c2 % rhs);
		}

		// Token: 0x06000FB4 RID: 4020 RVA: 0x0002DAA2 File Offset: 0x0002BCA2
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4x3 operator %(double lhs, double4x3 rhs)
		{
			return new double4x3(lhs % rhs.c0, lhs % rhs.c1, lhs % rhs.c2);
		}

		// Token: 0x06000FB5 RID: 4021 RVA: 0x0002DAD0 File Offset: 0x0002BCD0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4x3 operator ++(double4x3 val)
		{
			double4 @double = ++val.c0;
			val.c0 = @double;
			double4 double2 = @double;
			@double = ++val.c1;
			val.c1 = @double;
			double4 double3 = @double;
			@double = ++val.c2;
			val.c2 = @double;
			return new double4x3(double2, double3, @double);
		}

		// Token: 0x06000FB6 RID: 4022 RVA: 0x0002DB30 File Offset: 0x0002BD30
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4x3 operator --(double4x3 val)
		{
			double4 @double = --val.c0;
			val.c0 = @double;
			double4 double2 = @double;
			@double = --val.c1;
			val.c1 = @double;
			double4 double3 = @double;
			@double = --val.c2;
			val.c2 = @double;
			return new double4x3(double2, double3, @double);
		}

		// Token: 0x06000FB7 RID: 4023 RVA: 0x0002DB90 File Offset: 0x0002BD90
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x3 operator <(double4x3 lhs, double4x3 rhs)
		{
			return new bool4x3(lhs.c0 < rhs.c0, lhs.c1 < rhs.c1, lhs.c2 < rhs.c2);
		}

		// Token: 0x06000FB8 RID: 4024 RVA: 0x0002DBCA File Offset: 0x0002BDCA
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x3 operator <(double4x3 lhs, double rhs)
		{
			return new bool4x3(lhs.c0 < rhs, lhs.c1 < rhs, lhs.c2 < rhs);
		}

		// Token: 0x06000FB9 RID: 4025 RVA: 0x0002DBF5 File Offset: 0x0002BDF5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x3 operator <(double lhs, double4x3 rhs)
		{
			return new bool4x3(lhs < rhs.c0, lhs < rhs.c1, lhs < rhs.c2);
		}

		// Token: 0x06000FBA RID: 4026 RVA: 0x0002DC20 File Offset: 0x0002BE20
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x3 operator <=(double4x3 lhs, double4x3 rhs)
		{
			return new bool4x3(lhs.c0 <= rhs.c0, lhs.c1 <= rhs.c1, lhs.c2 <= rhs.c2);
		}

		// Token: 0x06000FBB RID: 4027 RVA: 0x0002DC5A File Offset: 0x0002BE5A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x3 operator <=(double4x3 lhs, double rhs)
		{
			return new bool4x3(lhs.c0 <= rhs, lhs.c1 <= rhs, lhs.c2 <= rhs);
		}

		// Token: 0x06000FBC RID: 4028 RVA: 0x0002DC85 File Offset: 0x0002BE85
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x3 operator <=(double lhs, double4x3 rhs)
		{
			return new bool4x3(lhs <= rhs.c0, lhs <= rhs.c1, lhs <= rhs.c2);
		}

		// Token: 0x06000FBD RID: 4029 RVA: 0x0002DCB0 File Offset: 0x0002BEB0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x3 operator >(double4x3 lhs, double4x3 rhs)
		{
			return new bool4x3(lhs.c0 > rhs.c0, lhs.c1 > rhs.c1, lhs.c2 > rhs.c2);
		}

		// Token: 0x06000FBE RID: 4030 RVA: 0x0002DCEA File Offset: 0x0002BEEA
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x3 operator >(double4x3 lhs, double rhs)
		{
			return new bool4x3(lhs.c0 > rhs, lhs.c1 > rhs, lhs.c2 > rhs);
		}

		// Token: 0x06000FBF RID: 4031 RVA: 0x0002DD15 File Offset: 0x0002BF15
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x3 operator >(double lhs, double4x3 rhs)
		{
			return new bool4x3(lhs > rhs.c0, lhs > rhs.c1, lhs > rhs.c2);
		}

		// Token: 0x06000FC0 RID: 4032 RVA: 0x0002DD40 File Offset: 0x0002BF40
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x3 operator >=(double4x3 lhs, double4x3 rhs)
		{
			return new bool4x3(lhs.c0 >= rhs.c0, lhs.c1 >= rhs.c1, lhs.c2 >= rhs.c2);
		}

		// Token: 0x06000FC1 RID: 4033 RVA: 0x0002DD7A File Offset: 0x0002BF7A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x3 operator >=(double4x3 lhs, double rhs)
		{
			return new bool4x3(lhs.c0 >= rhs, lhs.c1 >= rhs, lhs.c2 >= rhs);
		}

		// Token: 0x06000FC2 RID: 4034 RVA: 0x0002DDA5 File Offset: 0x0002BFA5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x3 operator >=(double lhs, double4x3 rhs)
		{
			return new bool4x3(lhs >= rhs.c0, lhs >= rhs.c1, lhs >= rhs.c2);
		}

		// Token: 0x06000FC3 RID: 4035 RVA: 0x0002DDD0 File Offset: 0x0002BFD0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4x3 operator -(double4x3 val)
		{
			return new double4x3(-val.c0, -val.c1, -val.c2);
		}

		// Token: 0x06000FC4 RID: 4036 RVA: 0x0002DDF8 File Offset: 0x0002BFF8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4x3 operator +(double4x3 val)
		{
			return new double4x3(+val.c0, +val.c1, +val.c2);
		}

		// Token: 0x06000FC5 RID: 4037 RVA: 0x0002DE20 File Offset: 0x0002C020
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x3 operator ==(double4x3 lhs, double4x3 rhs)
		{
			return new bool4x3(lhs.c0 == rhs.c0, lhs.c1 == rhs.c1, lhs.c2 == rhs.c2);
		}

		// Token: 0x06000FC6 RID: 4038 RVA: 0x0002DE5A File Offset: 0x0002C05A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x3 operator ==(double4x3 lhs, double rhs)
		{
			return new bool4x3(lhs.c0 == rhs, lhs.c1 == rhs, lhs.c2 == rhs);
		}

		// Token: 0x06000FC7 RID: 4039 RVA: 0x0002DE85 File Offset: 0x0002C085
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x3 operator ==(double lhs, double4x3 rhs)
		{
			return new bool4x3(lhs == rhs.c0, lhs == rhs.c1, lhs == rhs.c2);
		}

		// Token: 0x06000FC8 RID: 4040 RVA: 0x0002DEB0 File Offset: 0x0002C0B0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x3 operator !=(double4x3 lhs, double4x3 rhs)
		{
			return new bool4x3(lhs.c0 != rhs.c0, lhs.c1 != rhs.c1, lhs.c2 != rhs.c2);
		}

		// Token: 0x06000FC9 RID: 4041 RVA: 0x0002DEEA File Offset: 0x0002C0EA
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x3 operator !=(double4x3 lhs, double rhs)
		{
			return new bool4x3(lhs.c0 != rhs, lhs.c1 != rhs, lhs.c2 != rhs);
		}

		// Token: 0x06000FCA RID: 4042 RVA: 0x0002DF15 File Offset: 0x0002C115
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x3 operator !=(double lhs, double4x3 rhs)
		{
			return new bool4x3(lhs != rhs.c0, lhs != rhs.c1, lhs != rhs.c2);
		}

		// Token: 0x170003D9 RID: 985
		public unsafe double4 this[int index]
		{
			get
			{
				fixed (double4x3* ptr = &this)
				{
					return ref *(double4*)(ptr + (IntPtr)index * (IntPtr)sizeof(double4) / (IntPtr)sizeof(double4x3));
				}
			}
		}

		// Token: 0x06000FCC RID: 4044 RVA: 0x0002DF5B File Offset: 0x0002C15B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool Equals(double4x3 rhs)
		{
			return this.c0.Equals(rhs.c0) && this.c1.Equals(rhs.c1) && this.c2.Equals(rhs.c2);
		}

		// Token: 0x06000FCD RID: 4045 RVA: 0x0002DF98 File Offset: 0x0002C198
		public override bool Equals(object o)
		{
			if (o is double4x3)
			{
				double4x3 rhs = (double4x3)o;
				return this.Equals(rhs);
			}
			return false;
		}

		// Token: 0x06000FCE RID: 4046 RVA: 0x0002DFBD File Offset: 0x0002C1BD
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override int GetHashCode()
		{
			return (int)math.hash(this);
		}

		// Token: 0x06000FCF RID: 4047 RVA: 0x0002DFCC File Offset: 0x0002C1CC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override string ToString()
		{
			return string.Format("double4x3({0}, {1}, {2},  {3}, {4}, {5},  {6}, {7}, {8},  {9}, {10}, {11})", new object[]
			{
				this.c0.x,
				this.c1.x,
				this.c2.x,
				this.c0.y,
				this.c1.y,
				this.c2.y,
				this.c0.z,
				this.c1.z,
				this.c2.z,
				this.c0.w,
				this.c1.w,
				this.c2.w
			});
		}

		// Token: 0x06000FD0 RID: 4048 RVA: 0x0002E0D4 File Offset: 0x0002C2D4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public string ToString(string format, IFormatProvider formatProvider)
		{
			return string.Format("double4x3({0}, {1}, {2},  {3}, {4}, {5},  {6}, {7}, {8},  {9}, {10}, {11})", new object[]
			{
				this.c0.x.ToString(format, formatProvider),
				this.c1.x.ToString(format, formatProvider),
				this.c2.x.ToString(format, formatProvider),
				this.c0.y.ToString(format, formatProvider),
				this.c1.y.ToString(format, formatProvider),
				this.c2.y.ToString(format, formatProvider),
				this.c0.z.ToString(format, formatProvider),
				this.c1.z.ToString(format, formatProvider),
				this.c2.z.ToString(format, formatProvider),
				this.c0.w.ToString(format, formatProvider),
				this.c1.w.ToString(format, formatProvider),
				this.c2.w.ToString(format, formatProvider)
			});
		}

		// Token: 0x04000064 RID: 100
		public double4 c0;

		// Token: 0x04000065 RID: 101
		public double4 c1;

		// Token: 0x04000066 RID: 102
		public double4 c2;

		// Token: 0x04000067 RID: 103
		public static readonly double4x3 zero;
	}
}

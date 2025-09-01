using System;
using System.Runtime.CompilerServices;
using Unity.IL2CPP.CompilerServices;

namespace Unity.Mathematics
{
	// Token: 0x02000019 RID: 25
	[Il2CppEagerStaticClassConstruction]
	[Serializable]
	public struct double4x2 : IEquatable<double4x2>, IFormattable
	{
		// Token: 0x06000F53 RID: 3923 RVA: 0x0002CBCD File Offset: 0x0002ADCD
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public double4x2(double4 c0, double4 c1)
		{
			this.c0 = c0;
			this.c1 = c1;
		}

		// Token: 0x06000F54 RID: 3924 RVA: 0x0002CBDD File Offset: 0x0002ADDD
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public double4x2(double m00, double m01, double m10, double m11, double m20, double m21, double m30, double m31)
		{
			this.c0 = new double4(m00, m10, m20, m30);
			this.c1 = new double4(m01, m11, m21, m31);
		}

		// Token: 0x06000F55 RID: 3925 RVA: 0x0002CC02 File Offset: 0x0002AE02
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public double4x2(double v)
		{
			this.c0 = v;
			this.c1 = v;
		}

		// Token: 0x06000F56 RID: 3926 RVA: 0x0002CC1C File Offset: 0x0002AE1C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public double4x2(bool v)
		{
			this.c0 = math.select(new double4(0.0), new double4(1.0), v);
			this.c1 = math.select(new double4(0.0), new double4(1.0), v);
		}

		// Token: 0x06000F57 RID: 3927 RVA: 0x0002CC7C File Offset: 0x0002AE7C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public double4x2(bool4x2 v)
		{
			this.c0 = math.select(new double4(0.0), new double4(1.0), v.c0);
			this.c1 = math.select(new double4(0.0), new double4(1.0), v.c1);
		}

		// Token: 0x06000F58 RID: 3928 RVA: 0x0002CCE3 File Offset: 0x0002AEE3
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public double4x2(int v)
		{
			this.c0 = v;
			this.c1 = v;
		}

		// Token: 0x06000F59 RID: 3929 RVA: 0x0002CCFD File Offset: 0x0002AEFD
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public double4x2(int4x2 v)
		{
			this.c0 = v.c0;
			this.c1 = v.c1;
		}

		// Token: 0x06000F5A RID: 3930 RVA: 0x0002CD21 File Offset: 0x0002AF21
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public double4x2(uint v)
		{
			this.c0 = v;
			this.c1 = v;
		}

		// Token: 0x06000F5B RID: 3931 RVA: 0x0002CD3B File Offset: 0x0002AF3B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public double4x2(uint4x2 v)
		{
			this.c0 = v.c0;
			this.c1 = v.c1;
		}

		// Token: 0x06000F5C RID: 3932 RVA: 0x0002CD5F File Offset: 0x0002AF5F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public double4x2(float v)
		{
			this.c0 = v;
			this.c1 = v;
		}

		// Token: 0x06000F5D RID: 3933 RVA: 0x0002CD79 File Offset: 0x0002AF79
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public double4x2(float4x2 v)
		{
			this.c0 = v.c0;
			this.c1 = v.c1;
		}

		// Token: 0x06000F5E RID: 3934 RVA: 0x0002CD9D File Offset: 0x0002AF9D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator double4x2(double v)
		{
			return new double4x2(v);
		}

		// Token: 0x06000F5F RID: 3935 RVA: 0x0002CDA5 File Offset: 0x0002AFA5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator double4x2(bool v)
		{
			return new double4x2(v);
		}

		// Token: 0x06000F60 RID: 3936 RVA: 0x0002CDAD File Offset: 0x0002AFAD
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator double4x2(bool4x2 v)
		{
			return new double4x2(v);
		}

		// Token: 0x06000F61 RID: 3937 RVA: 0x0002CDB5 File Offset: 0x0002AFB5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator double4x2(int v)
		{
			return new double4x2(v);
		}

		// Token: 0x06000F62 RID: 3938 RVA: 0x0002CDBD File Offset: 0x0002AFBD
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator double4x2(int4x2 v)
		{
			return new double4x2(v);
		}

		// Token: 0x06000F63 RID: 3939 RVA: 0x0002CDC5 File Offset: 0x0002AFC5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator double4x2(uint v)
		{
			return new double4x2(v);
		}

		// Token: 0x06000F64 RID: 3940 RVA: 0x0002CDCD File Offset: 0x0002AFCD
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator double4x2(uint4x2 v)
		{
			return new double4x2(v);
		}

		// Token: 0x06000F65 RID: 3941 RVA: 0x0002CDD5 File Offset: 0x0002AFD5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator double4x2(float v)
		{
			return new double4x2(v);
		}

		// Token: 0x06000F66 RID: 3942 RVA: 0x0002CDDD File Offset: 0x0002AFDD
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator double4x2(float4x2 v)
		{
			return new double4x2(v);
		}

		// Token: 0x06000F67 RID: 3943 RVA: 0x0002CDE5 File Offset: 0x0002AFE5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4x2 operator *(double4x2 lhs, double4x2 rhs)
		{
			return new double4x2(lhs.c0 * rhs.c0, lhs.c1 * rhs.c1);
		}

		// Token: 0x06000F68 RID: 3944 RVA: 0x0002CE0E File Offset: 0x0002B00E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4x2 operator *(double4x2 lhs, double rhs)
		{
			return new double4x2(lhs.c0 * rhs, lhs.c1 * rhs);
		}

		// Token: 0x06000F69 RID: 3945 RVA: 0x0002CE2D File Offset: 0x0002B02D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4x2 operator *(double lhs, double4x2 rhs)
		{
			return new double4x2(lhs * rhs.c0, lhs * rhs.c1);
		}

		// Token: 0x06000F6A RID: 3946 RVA: 0x0002CE4C File Offset: 0x0002B04C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4x2 operator +(double4x2 lhs, double4x2 rhs)
		{
			return new double4x2(lhs.c0 + rhs.c0, lhs.c1 + rhs.c1);
		}

		// Token: 0x06000F6B RID: 3947 RVA: 0x0002CE75 File Offset: 0x0002B075
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4x2 operator +(double4x2 lhs, double rhs)
		{
			return new double4x2(lhs.c0 + rhs, lhs.c1 + rhs);
		}

		// Token: 0x06000F6C RID: 3948 RVA: 0x0002CE94 File Offset: 0x0002B094
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4x2 operator +(double lhs, double4x2 rhs)
		{
			return new double4x2(lhs + rhs.c0, lhs + rhs.c1);
		}

		// Token: 0x06000F6D RID: 3949 RVA: 0x0002CEB3 File Offset: 0x0002B0B3
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4x2 operator -(double4x2 lhs, double4x2 rhs)
		{
			return new double4x2(lhs.c0 - rhs.c0, lhs.c1 - rhs.c1);
		}

		// Token: 0x06000F6E RID: 3950 RVA: 0x0002CEDC File Offset: 0x0002B0DC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4x2 operator -(double4x2 lhs, double rhs)
		{
			return new double4x2(lhs.c0 - rhs, lhs.c1 - rhs);
		}

		// Token: 0x06000F6F RID: 3951 RVA: 0x0002CEFB File Offset: 0x0002B0FB
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4x2 operator -(double lhs, double4x2 rhs)
		{
			return new double4x2(lhs - rhs.c0, lhs - rhs.c1);
		}

		// Token: 0x06000F70 RID: 3952 RVA: 0x0002CF1A File Offset: 0x0002B11A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4x2 operator /(double4x2 lhs, double4x2 rhs)
		{
			return new double4x2(lhs.c0 / rhs.c0, lhs.c1 / rhs.c1);
		}

		// Token: 0x06000F71 RID: 3953 RVA: 0x0002CF43 File Offset: 0x0002B143
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4x2 operator /(double4x2 lhs, double rhs)
		{
			return new double4x2(lhs.c0 / rhs, lhs.c1 / rhs);
		}

		// Token: 0x06000F72 RID: 3954 RVA: 0x0002CF62 File Offset: 0x0002B162
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4x2 operator /(double lhs, double4x2 rhs)
		{
			return new double4x2(lhs / rhs.c0, lhs / rhs.c1);
		}

		// Token: 0x06000F73 RID: 3955 RVA: 0x0002CF81 File Offset: 0x0002B181
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4x2 operator %(double4x2 lhs, double4x2 rhs)
		{
			return new double4x2(lhs.c0 % rhs.c0, lhs.c1 % rhs.c1);
		}

		// Token: 0x06000F74 RID: 3956 RVA: 0x0002CFAA File Offset: 0x0002B1AA
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4x2 operator %(double4x2 lhs, double rhs)
		{
			return new double4x2(lhs.c0 % rhs, lhs.c1 % rhs);
		}

		// Token: 0x06000F75 RID: 3957 RVA: 0x0002CFC9 File Offset: 0x0002B1C9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4x2 operator %(double lhs, double4x2 rhs)
		{
			return new double4x2(lhs % rhs.c0, lhs % rhs.c1);
		}

		// Token: 0x06000F76 RID: 3958 RVA: 0x0002CFE8 File Offset: 0x0002B1E8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4x2 operator ++(double4x2 val)
		{
			double4 @double = ++val.c0;
			val.c0 = @double;
			double4 double2 = @double;
			@double = ++val.c1;
			val.c1 = @double;
			return new double4x2(double2, @double);
		}

		// Token: 0x06000F77 RID: 3959 RVA: 0x0002D030 File Offset: 0x0002B230
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4x2 operator --(double4x2 val)
		{
			double4 @double = --val.c0;
			val.c0 = @double;
			double4 double2 = @double;
			@double = --val.c1;
			val.c1 = @double;
			return new double4x2(double2, @double);
		}

		// Token: 0x06000F78 RID: 3960 RVA: 0x0002D076 File Offset: 0x0002B276
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x2 operator <(double4x2 lhs, double4x2 rhs)
		{
			return new bool4x2(lhs.c0 < rhs.c0, lhs.c1 < rhs.c1);
		}

		// Token: 0x06000F79 RID: 3961 RVA: 0x0002D09F File Offset: 0x0002B29F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x2 operator <(double4x2 lhs, double rhs)
		{
			return new bool4x2(lhs.c0 < rhs, lhs.c1 < rhs);
		}

		// Token: 0x06000F7A RID: 3962 RVA: 0x0002D0BE File Offset: 0x0002B2BE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x2 operator <(double lhs, double4x2 rhs)
		{
			return new bool4x2(lhs < rhs.c0, lhs < rhs.c1);
		}

		// Token: 0x06000F7B RID: 3963 RVA: 0x0002D0DD File Offset: 0x0002B2DD
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x2 operator <=(double4x2 lhs, double4x2 rhs)
		{
			return new bool4x2(lhs.c0 <= rhs.c0, lhs.c1 <= rhs.c1);
		}

		// Token: 0x06000F7C RID: 3964 RVA: 0x0002D106 File Offset: 0x0002B306
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x2 operator <=(double4x2 lhs, double rhs)
		{
			return new bool4x2(lhs.c0 <= rhs, lhs.c1 <= rhs);
		}

		// Token: 0x06000F7D RID: 3965 RVA: 0x0002D125 File Offset: 0x0002B325
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x2 operator <=(double lhs, double4x2 rhs)
		{
			return new bool4x2(lhs <= rhs.c0, lhs <= rhs.c1);
		}

		// Token: 0x06000F7E RID: 3966 RVA: 0x0002D144 File Offset: 0x0002B344
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x2 operator >(double4x2 lhs, double4x2 rhs)
		{
			return new bool4x2(lhs.c0 > rhs.c0, lhs.c1 > rhs.c1);
		}

		// Token: 0x06000F7F RID: 3967 RVA: 0x0002D16D File Offset: 0x0002B36D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x2 operator >(double4x2 lhs, double rhs)
		{
			return new bool4x2(lhs.c0 > rhs, lhs.c1 > rhs);
		}

		// Token: 0x06000F80 RID: 3968 RVA: 0x0002D18C File Offset: 0x0002B38C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x2 operator >(double lhs, double4x2 rhs)
		{
			return new bool4x2(lhs > rhs.c0, lhs > rhs.c1);
		}

		// Token: 0x06000F81 RID: 3969 RVA: 0x0002D1AB File Offset: 0x0002B3AB
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x2 operator >=(double4x2 lhs, double4x2 rhs)
		{
			return new bool4x2(lhs.c0 >= rhs.c0, lhs.c1 >= rhs.c1);
		}

		// Token: 0x06000F82 RID: 3970 RVA: 0x0002D1D4 File Offset: 0x0002B3D4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x2 operator >=(double4x2 lhs, double rhs)
		{
			return new bool4x2(lhs.c0 >= rhs, lhs.c1 >= rhs);
		}

		// Token: 0x06000F83 RID: 3971 RVA: 0x0002D1F3 File Offset: 0x0002B3F3
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x2 operator >=(double lhs, double4x2 rhs)
		{
			return new bool4x2(lhs >= rhs.c0, lhs >= rhs.c1);
		}

		// Token: 0x06000F84 RID: 3972 RVA: 0x0002D212 File Offset: 0x0002B412
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4x2 operator -(double4x2 val)
		{
			return new double4x2(-val.c0, -val.c1);
		}

		// Token: 0x06000F85 RID: 3973 RVA: 0x0002D22F File Offset: 0x0002B42F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4x2 operator +(double4x2 val)
		{
			return new double4x2(+val.c0, +val.c1);
		}

		// Token: 0x06000F86 RID: 3974 RVA: 0x0002D24C File Offset: 0x0002B44C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x2 operator ==(double4x2 lhs, double4x2 rhs)
		{
			return new bool4x2(lhs.c0 == rhs.c0, lhs.c1 == rhs.c1);
		}

		// Token: 0x06000F87 RID: 3975 RVA: 0x0002D275 File Offset: 0x0002B475
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x2 operator ==(double4x2 lhs, double rhs)
		{
			return new bool4x2(lhs.c0 == rhs, lhs.c1 == rhs);
		}

		// Token: 0x06000F88 RID: 3976 RVA: 0x0002D294 File Offset: 0x0002B494
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x2 operator ==(double lhs, double4x2 rhs)
		{
			return new bool4x2(lhs == rhs.c0, lhs == rhs.c1);
		}

		// Token: 0x06000F89 RID: 3977 RVA: 0x0002D2B3 File Offset: 0x0002B4B3
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x2 operator !=(double4x2 lhs, double4x2 rhs)
		{
			return new bool4x2(lhs.c0 != rhs.c0, lhs.c1 != rhs.c1);
		}

		// Token: 0x06000F8A RID: 3978 RVA: 0x0002D2DC File Offset: 0x0002B4DC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x2 operator !=(double4x2 lhs, double rhs)
		{
			return new bool4x2(lhs.c0 != rhs, lhs.c1 != rhs);
		}

		// Token: 0x06000F8B RID: 3979 RVA: 0x0002D2FB File Offset: 0x0002B4FB
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x2 operator !=(double lhs, double4x2 rhs)
		{
			return new bool4x2(lhs != rhs.c0, lhs != rhs.c1);
		}

		// Token: 0x170003D8 RID: 984
		public unsafe double4 this[int index]
		{
			get
			{
				fixed (double4x2* ptr = &this)
				{
					return ref *(double4*)(ptr + (IntPtr)index * (IntPtr)sizeof(double4) / (IntPtr)sizeof(double4x2));
				}
			}
		}

		// Token: 0x06000F8D RID: 3981 RVA: 0x0002D337 File Offset: 0x0002B537
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool Equals(double4x2 rhs)
		{
			return this.c0.Equals(rhs.c0) && this.c1.Equals(rhs.c1);
		}

		// Token: 0x06000F8E RID: 3982 RVA: 0x0002D360 File Offset: 0x0002B560
		public override bool Equals(object o)
		{
			if (o is double4x2)
			{
				double4x2 rhs = (double4x2)o;
				return this.Equals(rhs);
			}
			return false;
		}

		// Token: 0x06000F8F RID: 3983 RVA: 0x0002D385 File Offset: 0x0002B585
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override int GetHashCode()
		{
			return (int)math.hash(this);
		}

		// Token: 0x06000F90 RID: 3984 RVA: 0x0002D394 File Offset: 0x0002B594
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override string ToString()
		{
			return string.Format("double4x2({0}, {1},  {2}, {3},  {4}, {5},  {6}, {7})", new object[]
			{
				this.c0.x,
				this.c1.x,
				this.c0.y,
				this.c1.y,
				this.c0.z,
				this.c1.z,
				this.c0.w,
				this.c1.w
			});
		}

		// Token: 0x06000F91 RID: 3985 RVA: 0x0002D44C File Offset: 0x0002B64C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public string ToString(string format, IFormatProvider formatProvider)
		{
			return string.Format("double4x2({0}, {1},  {2}, {3},  {4}, {5},  {6}, {7})", new object[]
			{
				this.c0.x.ToString(format, formatProvider),
				this.c1.x.ToString(format, formatProvider),
				this.c0.y.ToString(format, formatProvider),
				this.c1.y.ToString(format, formatProvider),
				this.c0.z.ToString(format, formatProvider),
				this.c1.z.ToString(format, formatProvider),
				this.c0.w.ToString(format, formatProvider),
				this.c1.w.ToString(format, formatProvider)
			});
		}

		// Token: 0x04000061 RID: 97
		public double4 c0;

		// Token: 0x04000062 RID: 98
		public double4 c1;

		// Token: 0x04000063 RID: 99
		public static readonly double4x2 zero;
	}
}

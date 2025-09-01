using System;
using System.Runtime.CompilerServices;
using Unity.IL2CPP.CompilerServices;

namespace Unity.Mathematics
{
	// Token: 0x02000011 RID: 17
	[Il2CppEagerStaticClassConstruction]
	[Serializable]
	public struct double2x2 : IEquatable<double2x2>, IFormattable
	{
		// Token: 0x06000B3A RID: 2874 RVA: 0x00022AEA File Offset: 0x00020CEA
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public double2x2(double2 c0, double2 c1)
		{
			this.c0 = c0;
			this.c1 = c1;
		}

		// Token: 0x06000B3B RID: 2875 RVA: 0x00022AFA File Offset: 0x00020CFA
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public double2x2(double m00, double m01, double m10, double m11)
		{
			this.c0 = new double2(m00, m10);
			this.c1 = new double2(m01, m11);
		}

		// Token: 0x06000B3C RID: 2876 RVA: 0x00022B17 File Offset: 0x00020D17
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public double2x2(double v)
		{
			this.c0 = v;
			this.c1 = v;
		}

		// Token: 0x06000B3D RID: 2877 RVA: 0x00022B34 File Offset: 0x00020D34
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public double2x2(bool v)
		{
			this.c0 = math.select(new double2(0.0), new double2(1.0), v);
			this.c1 = math.select(new double2(0.0), new double2(1.0), v);
		}

		// Token: 0x06000B3E RID: 2878 RVA: 0x00022B94 File Offset: 0x00020D94
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public double2x2(bool2x2 v)
		{
			this.c0 = math.select(new double2(0.0), new double2(1.0), v.c0);
			this.c1 = math.select(new double2(0.0), new double2(1.0), v.c1);
		}

		// Token: 0x06000B3F RID: 2879 RVA: 0x00022BFB File Offset: 0x00020DFB
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public double2x2(int v)
		{
			this.c0 = v;
			this.c1 = v;
		}

		// Token: 0x06000B40 RID: 2880 RVA: 0x00022C15 File Offset: 0x00020E15
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public double2x2(int2x2 v)
		{
			this.c0 = v.c0;
			this.c1 = v.c1;
		}

		// Token: 0x06000B41 RID: 2881 RVA: 0x00022C39 File Offset: 0x00020E39
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public double2x2(uint v)
		{
			this.c0 = v;
			this.c1 = v;
		}

		// Token: 0x06000B42 RID: 2882 RVA: 0x00022C53 File Offset: 0x00020E53
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public double2x2(uint2x2 v)
		{
			this.c0 = v.c0;
			this.c1 = v.c1;
		}

		// Token: 0x06000B43 RID: 2883 RVA: 0x00022C77 File Offset: 0x00020E77
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public double2x2(float v)
		{
			this.c0 = v;
			this.c1 = v;
		}

		// Token: 0x06000B44 RID: 2884 RVA: 0x00022C91 File Offset: 0x00020E91
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public double2x2(float2x2 v)
		{
			this.c0 = v.c0;
			this.c1 = v.c1;
		}

		// Token: 0x06000B45 RID: 2885 RVA: 0x00022CB5 File Offset: 0x00020EB5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator double2x2(double v)
		{
			return new double2x2(v);
		}

		// Token: 0x06000B46 RID: 2886 RVA: 0x00022CBD File Offset: 0x00020EBD
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator double2x2(bool v)
		{
			return new double2x2(v);
		}

		// Token: 0x06000B47 RID: 2887 RVA: 0x00022CC5 File Offset: 0x00020EC5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator double2x2(bool2x2 v)
		{
			return new double2x2(v);
		}

		// Token: 0x06000B48 RID: 2888 RVA: 0x00022CCD File Offset: 0x00020ECD
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator double2x2(int v)
		{
			return new double2x2(v);
		}

		// Token: 0x06000B49 RID: 2889 RVA: 0x00022CD5 File Offset: 0x00020ED5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator double2x2(int2x2 v)
		{
			return new double2x2(v);
		}

		// Token: 0x06000B4A RID: 2890 RVA: 0x00022CDD File Offset: 0x00020EDD
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator double2x2(uint v)
		{
			return new double2x2(v);
		}

		// Token: 0x06000B4B RID: 2891 RVA: 0x00022CE5 File Offset: 0x00020EE5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator double2x2(uint2x2 v)
		{
			return new double2x2(v);
		}

		// Token: 0x06000B4C RID: 2892 RVA: 0x00022CED File Offset: 0x00020EED
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator double2x2(float v)
		{
			return new double2x2(v);
		}

		// Token: 0x06000B4D RID: 2893 RVA: 0x00022CF5 File Offset: 0x00020EF5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator double2x2(float2x2 v)
		{
			return new double2x2(v);
		}

		// Token: 0x06000B4E RID: 2894 RVA: 0x00022CFD File Offset: 0x00020EFD
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2x2 operator *(double2x2 lhs, double2x2 rhs)
		{
			return new double2x2(lhs.c0 * rhs.c0, lhs.c1 * rhs.c1);
		}

		// Token: 0x06000B4F RID: 2895 RVA: 0x00022D26 File Offset: 0x00020F26
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2x2 operator *(double2x2 lhs, double rhs)
		{
			return new double2x2(lhs.c0 * rhs, lhs.c1 * rhs);
		}

		// Token: 0x06000B50 RID: 2896 RVA: 0x00022D45 File Offset: 0x00020F45
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2x2 operator *(double lhs, double2x2 rhs)
		{
			return new double2x2(lhs * rhs.c0, lhs * rhs.c1);
		}

		// Token: 0x06000B51 RID: 2897 RVA: 0x00022D64 File Offset: 0x00020F64
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2x2 operator +(double2x2 lhs, double2x2 rhs)
		{
			return new double2x2(lhs.c0 + rhs.c0, lhs.c1 + rhs.c1);
		}

		// Token: 0x06000B52 RID: 2898 RVA: 0x00022D8D File Offset: 0x00020F8D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2x2 operator +(double2x2 lhs, double rhs)
		{
			return new double2x2(lhs.c0 + rhs, lhs.c1 + rhs);
		}

		// Token: 0x06000B53 RID: 2899 RVA: 0x00022DAC File Offset: 0x00020FAC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2x2 operator +(double lhs, double2x2 rhs)
		{
			return new double2x2(lhs + rhs.c0, lhs + rhs.c1);
		}

		// Token: 0x06000B54 RID: 2900 RVA: 0x00022DCB File Offset: 0x00020FCB
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2x2 operator -(double2x2 lhs, double2x2 rhs)
		{
			return new double2x2(lhs.c0 - rhs.c0, lhs.c1 - rhs.c1);
		}

		// Token: 0x06000B55 RID: 2901 RVA: 0x00022DF4 File Offset: 0x00020FF4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2x2 operator -(double2x2 lhs, double rhs)
		{
			return new double2x2(lhs.c0 - rhs, lhs.c1 - rhs);
		}

		// Token: 0x06000B56 RID: 2902 RVA: 0x00022E13 File Offset: 0x00021013
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2x2 operator -(double lhs, double2x2 rhs)
		{
			return new double2x2(lhs - rhs.c0, lhs - rhs.c1);
		}

		// Token: 0x06000B57 RID: 2903 RVA: 0x00022E32 File Offset: 0x00021032
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2x2 operator /(double2x2 lhs, double2x2 rhs)
		{
			return new double2x2(lhs.c0 / rhs.c0, lhs.c1 / rhs.c1);
		}

		// Token: 0x06000B58 RID: 2904 RVA: 0x00022E5B File Offset: 0x0002105B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2x2 operator /(double2x2 lhs, double rhs)
		{
			return new double2x2(lhs.c0 / rhs, lhs.c1 / rhs);
		}

		// Token: 0x06000B59 RID: 2905 RVA: 0x00022E7A File Offset: 0x0002107A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2x2 operator /(double lhs, double2x2 rhs)
		{
			return new double2x2(lhs / rhs.c0, lhs / rhs.c1);
		}

		// Token: 0x06000B5A RID: 2906 RVA: 0x00022E99 File Offset: 0x00021099
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2x2 operator %(double2x2 lhs, double2x2 rhs)
		{
			return new double2x2(lhs.c0 % rhs.c0, lhs.c1 % rhs.c1);
		}

		// Token: 0x06000B5B RID: 2907 RVA: 0x00022EC2 File Offset: 0x000210C2
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2x2 operator %(double2x2 lhs, double rhs)
		{
			return new double2x2(lhs.c0 % rhs, lhs.c1 % rhs);
		}

		// Token: 0x06000B5C RID: 2908 RVA: 0x00022EE1 File Offset: 0x000210E1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2x2 operator %(double lhs, double2x2 rhs)
		{
			return new double2x2(lhs % rhs.c0, lhs % rhs.c1);
		}

		// Token: 0x06000B5D RID: 2909 RVA: 0x00022F00 File Offset: 0x00021100
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2x2 operator ++(double2x2 val)
		{
			double2 @double = ++val.c0;
			val.c0 = @double;
			double2 double2 = @double;
			@double = ++val.c1;
			val.c1 = @double;
			return new double2x2(double2, @double);
		}

		// Token: 0x06000B5E RID: 2910 RVA: 0x00022F48 File Offset: 0x00021148
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2x2 operator --(double2x2 val)
		{
			double2 @double = --val.c0;
			val.c0 = @double;
			double2 double2 = @double;
			@double = --val.c1;
			val.c1 = @double;
			return new double2x2(double2, @double);
		}

		// Token: 0x06000B5F RID: 2911 RVA: 0x00022F8E File Offset: 0x0002118E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x2 operator <(double2x2 lhs, double2x2 rhs)
		{
			return new bool2x2(lhs.c0 < rhs.c0, lhs.c1 < rhs.c1);
		}

		// Token: 0x06000B60 RID: 2912 RVA: 0x00022FB7 File Offset: 0x000211B7
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x2 operator <(double2x2 lhs, double rhs)
		{
			return new bool2x2(lhs.c0 < rhs, lhs.c1 < rhs);
		}

		// Token: 0x06000B61 RID: 2913 RVA: 0x00022FD6 File Offset: 0x000211D6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x2 operator <(double lhs, double2x2 rhs)
		{
			return new bool2x2(lhs < rhs.c0, lhs < rhs.c1);
		}

		// Token: 0x06000B62 RID: 2914 RVA: 0x00022FF5 File Offset: 0x000211F5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x2 operator <=(double2x2 lhs, double2x2 rhs)
		{
			return new bool2x2(lhs.c0 <= rhs.c0, lhs.c1 <= rhs.c1);
		}

		// Token: 0x06000B63 RID: 2915 RVA: 0x0002301E File Offset: 0x0002121E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x2 operator <=(double2x2 lhs, double rhs)
		{
			return new bool2x2(lhs.c0 <= rhs, lhs.c1 <= rhs);
		}

		// Token: 0x06000B64 RID: 2916 RVA: 0x0002303D File Offset: 0x0002123D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x2 operator <=(double lhs, double2x2 rhs)
		{
			return new bool2x2(lhs <= rhs.c0, lhs <= rhs.c1);
		}

		// Token: 0x06000B65 RID: 2917 RVA: 0x0002305C File Offset: 0x0002125C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x2 operator >(double2x2 lhs, double2x2 rhs)
		{
			return new bool2x2(lhs.c0 > rhs.c0, lhs.c1 > rhs.c1);
		}

		// Token: 0x06000B66 RID: 2918 RVA: 0x00023085 File Offset: 0x00021285
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x2 operator >(double2x2 lhs, double rhs)
		{
			return new bool2x2(lhs.c0 > rhs, lhs.c1 > rhs);
		}

		// Token: 0x06000B67 RID: 2919 RVA: 0x000230A4 File Offset: 0x000212A4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x2 operator >(double lhs, double2x2 rhs)
		{
			return new bool2x2(lhs > rhs.c0, lhs > rhs.c1);
		}

		// Token: 0x06000B68 RID: 2920 RVA: 0x000230C3 File Offset: 0x000212C3
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x2 operator >=(double2x2 lhs, double2x2 rhs)
		{
			return new bool2x2(lhs.c0 >= rhs.c0, lhs.c1 >= rhs.c1);
		}

		// Token: 0x06000B69 RID: 2921 RVA: 0x000230EC File Offset: 0x000212EC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x2 operator >=(double2x2 lhs, double rhs)
		{
			return new bool2x2(lhs.c0 >= rhs, lhs.c1 >= rhs);
		}

		// Token: 0x06000B6A RID: 2922 RVA: 0x0002310B File Offset: 0x0002130B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x2 operator >=(double lhs, double2x2 rhs)
		{
			return new bool2x2(lhs >= rhs.c0, lhs >= rhs.c1);
		}

		// Token: 0x06000B6B RID: 2923 RVA: 0x0002312A File Offset: 0x0002132A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2x2 operator -(double2x2 val)
		{
			return new double2x2(-val.c0, -val.c1);
		}

		// Token: 0x06000B6C RID: 2924 RVA: 0x00023147 File Offset: 0x00021347
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2x2 operator +(double2x2 val)
		{
			return new double2x2(+val.c0, +val.c1);
		}

		// Token: 0x06000B6D RID: 2925 RVA: 0x00023164 File Offset: 0x00021364
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x2 operator ==(double2x2 lhs, double2x2 rhs)
		{
			return new bool2x2(lhs.c0 == rhs.c0, lhs.c1 == rhs.c1);
		}

		// Token: 0x06000B6E RID: 2926 RVA: 0x0002318D File Offset: 0x0002138D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x2 operator ==(double2x2 lhs, double rhs)
		{
			return new bool2x2(lhs.c0 == rhs, lhs.c1 == rhs);
		}

		// Token: 0x06000B6F RID: 2927 RVA: 0x000231AC File Offset: 0x000213AC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x2 operator ==(double lhs, double2x2 rhs)
		{
			return new bool2x2(lhs == rhs.c0, lhs == rhs.c1);
		}

		// Token: 0x06000B70 RID: 2928 RVA: 0x000231CB File Offset: 0x000213CB
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x2 operator !=(double2x2 lhs, double2x2 rhs)
		{
			return new bool2x2(lhs.c0 != rhs.c0, lhs.c1 != rhs.c1);
		}

		// Token: 0x06000B71 RID: 2929 RVA: 0x000231F4 File Offset: 0x000213F4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x2 operator !=(double2x2 lhs, double rhs)
		{
			return new bool2x2(lhs.c0 != rhs, lhs.c1 != rhs);
		}

		// Token: 0x06000B72 RID: 2930 RVA: 0x00023213 File Offset: 0x00021413
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x2 operator !=(double lhs, double2x2 rhs)
		{
			return new bool2x2(lhs != rhs.c0, lhs != rhs.c1);
		}

		// Token: 0x1700020B RID: 523
		public unsafe double2 this[int index]
		{
			get
			{
				fixed (double2x2* ptr = &this)
				{
					return ref *(double2*)(ptr + (IntPtr)index * (IntPtr)sizeof(double2) / (IntPtr)sizeof(double2x2));
				}
			}
		}

		// Token: 0x06000B74 RID: 2932 RVA: 0x0002324F File Offset: 0x0002144F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool Equals(double2x2 rhs)
		{
			return this.c0.Equals(rhs.c0) && this.c1.Equals(rhs.c1);
		}

		// Token: 0x06000B75 RID: 2933 RVA: 0x00023278 File Offset: 0x00021478
		public override bool Equals(object o)
		{
			if (o is double2x2)
			{
				double2x2 rhs = (double2x2)o;
				return this.Equals(rhs);
			}
			return false;
		}

		// Token: 0x06000B76 RID: 2934 RVA: 0x0002329D File Offset: 0x0002149D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override int GetHashCode()
		{
			return (int)math.hash(this);
		}

		// Token: 0x06000B77 RID: 2935 RVA: 0x000232AC File Offset: 0x000214AC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override string ToString()
		{
			return string.Format("double2x2({0}, {1},  {2}, {3})", new object[]
			{
				this.c0.x,
				this.c1.x,
				this.c0.y,
				this.c1.y
			});
		}

		// Token: 0x06000B78 RID: 2936 RVA: 0x00023318 File Offset: 0x00021518
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public string ToString(string format, IFormatProvider formatProvider)
		{
			return string.Format("double2x2({0}, {1},  {2}, {3})", new object[]
			{
				this.c0.x.ToString(format, formatProvider),
				this.c1.x.ToString(format, formatProvider),
				this.c0.y.ToString(format, formatProvider),
				this.c1.y.ToString(format, formatProvider)
			});
		}

		// Token: 0x06000B79 RID: 2937 RVA: 0x00023389 File Offset: 0x00021589
		// Note: this type is marked as 'beforefieldinit'.
		static double2x2()
		{
		}

		// Token: 0x0400003E RID: 62
		public double2 c0;

		// Token: 0x0400003F RID: 63
		public double2 c1;

		// Token: 0x04000040 RID: 64
		public static readonly double2x2 identity = new double2x2(1.0, 0.0, 0.0, 1.0);

		// Token: 0x04000041 RID: 65
		public static readonly double2x2 zero;
	}
}

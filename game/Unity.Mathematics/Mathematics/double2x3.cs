using System;
using System.Runtime.CompilerServices;
using Unity.IL2CPP.CompilerServices;

namespace Unity.Mathematics
{
	// Token: 0x02000012 RID: 18
	[Il2CppEagerStaticClassConstruction]
	[Serializable]
	public struct double2x3 : IEquatable<double2x3>, IFormattable
	{
		// Token: 0x06000B7A RID: 2938 RVA: 0x000233B9 File Offset: 0x000215B9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public double2x3(double2 c0, double2 c1, double2 c2)
		{
			this.c0 = c0;
			this.c1 = c1;
			this.c2 = c2;
		}

		// Token: 0x06000B7B RID: 2939 RVA: 0x000233D0 File Offset: 0x000215D0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public double2x3(double m00, double m01, double m02, double m10, double m11, double m12)
		{
			this.c0 = new double2(m00, m10);
			this.c1 = new double2(m01, m11);
			this.c2 = new double2(m02, m12);
		}

		// Token: 0x06000B7C RID: 2940 RVA: 0x000233FC File Offset: 0x000215FC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public double2x3(double v)
		{
			this.c0 = v;
			this.c1 = v;
			this.c2 = v;
		}

		// Token: 0x06000B7D RID: 2941 RVA: 0x00023424 File Offset: 0x00021624
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public double2x3(bool v)
		{
			this.c0 = math.select(new double2(0.0), new double2(1.0), v);
			this.c1 = math.select(new double2(0.0), new double2(1.0), v);
			this.c2 = math.select(new double2(0.0), new double2(1.0), v);
		}

		// Token: 0x06000B7E RID: 2942 RVA: 0x000234AC File Offset: 0x000216AC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public double2x3(bool2x3 v)
		{
			this.c0 = math.select(new double2(0.0), new double2(1.0), v.c0);
			this.c1 = math.select(new double2(0.0), new double2(1.0), v.c1);
			this.c2 = math.select(new double2(0.0), new double2(1.0), v.c2);
		}

		// Token: 0x06000B7F RID: 2943 RVA: 0x00023540 File Offset: 0x00021740
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public double2x3(int v)
		{
			this.c0 = v;
			this.c1 = v;
			this.c2 = v;
		}

		// Token: 0x06000B80 RID: 2944 RVA: 0x00023566 File Offset: 0x00021766
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public double2x3(int2x3 v)
		{
			this.c0 = v.c0;
			this.c1 = v.c1;
			this.c2 = v.c2;
		}

		// Token: 0x06000B81 RID: 2945 RVA: 0x0002359B File Offset: 0x0002179B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public double2x3(uint v)
		{
			this.c0 = v;
			this.c1 = v;
			this.c2 = v;
		}

		// Token: 0x06000B82 RID: 2946 RVA: 0x000235C1 File Offset: 0x000217C1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public double2x3(uint2x3 v)
		{
			this.c0 = v.c0;
			this.c1 = v.c1;
			this.c2 = v.c2;
		}

		// Token: 0x06000B83 RID: 2947 RVA: 0x000235F6 File Offset: 0x000217F6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public double2x3(float v)
		{
			this.c0 = v;
			this.c1 = v;
			this.c2 = v;
		}

		// Token: 0x06000B84 RID: 2948 RVA: 0x0002361C File Offset: 0x0002181C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public double2x3(float2x3 v)
		{
			this.c0 = v.c0;
			this.c1 = v.c1;
			this.c2 = v.c2;
		}

		// Token: 0x06000B85 RID: 2949 RVA: 0x00023651 File Offset: 0x00021851
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator double2x3(double v)
		{
			return new double2x3(v);
		}

		// Token: 0x06000B86 RID: 2950 RVA: 0x00023659 File Offset: 0x00021859
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator double2x3(bool v)
		{
			return new double2x3(v);
		}

		// Token: 0x06000B87 RID: 2951 RVA: 0x00023661 File Offset: 0x00021861
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator double2x3(bool2x3 v)
		{
			return new double2x3(v);
		}

		// Token: 0x06000B88 RID: 2952 RVA: 0x00023669 File Offset: 0x00021869
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator double2x3(int v)
		{
			return new double2x3(v);
		}

		// Token: 0x06000B89 RID: 2953 RVA: 0x00023671 File Offset: 0x00021871
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator double2x3(int2x3 v)
		{
			return new double2x3(v);
		}

		// Token: 0x06000B8A RID: 2954 RVA: 0x00023679 File Offset: 0x00021879
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator double2x3(uint v)
		{
			return new double2x3(v);
		}

		// Token: 0x06000B8B RID: 2955 RVA: 0x00023681 File Offset: 0x00021881
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator double2x3(uint2x3 v)
		{
			return new double2x3(v);
		}

		// Token: 0x06000B8C RID: 2956 RVA: 0x00023689 File Offset: 0x00021889
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator double2x3(float v)
		{
			return new double2x3(v);
		}

		// Token: 0x06000B8D RID: 2957 RVA: 0x00023691 File Offset: 0x00021891
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator double2x3(float2x3 v)
		{
			return new double2x3(v);
		}

		// Token: 0x06000B8E RID: 2958 RVA: 0x00023699 File Offset: 0x00021899
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2x3 operator *(double2x3 lhs, double2x3 rhs)
		{
			return new double2x3(lhs.c0 * rhs.c0, lhs.c1 * rhs.c1, lhs.c2 * rhs.c2);
		}

		// Token: 0x06000B8F RID: 2959 RVA: 0x000236D3 File Offset: 0x000218D3
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2x3 operator *(double2x3 lhs, double rhs)
		{
			return new double2x3(lhs.c0 * rhs, lhs.c1 * rhs, lhs.c2 * rhs);
		}

		// Token: 0x06000B90 RID: 2960 RVA: 0x000236FE File Offset: 0x000218FE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2x3 operator *(double lhs, double2x3 rhs)
		{
			return new double2x3(lhs * rhs.c0, lhs * rhs.c1, lhs * rhs.c2);
		}

		// Token: 0x06000B91 RID: 2961 RVA: 0x00023729 File Offset: 0x00021929
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2x3 operator +(double2x3 lhs, double2x3 rhs)
		{
			return new double2x3(lhs.c0 + rhs.c0, lhs.c1 + rhs.c1, lhs.c2 + rhs.c2);
		}

		// Token: 0x06000B92 RID: 2962 RVA: 0x00023763 File Offset: 0x00021963
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2x3 operator +(double2x3 lhs, double rhs)
		{
			return new double2x3(lhs.c0 + rhs, lhs.c1 + rhs, lhs.c2 + rhs);
		}

		// Token: 0x06000B93 RID: 2963 RVA: 0x0002378E File Offset: 0x0002198E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2x3 operator +(double lhs, double2x3 rhs)
		{
			return new double2x3(lhs + rhs.c0, lhs + rhs.c1, lhs + rhs.c2);
		}

		// Token: 0x06000B94 RID: 2964 RVA: 0x000237B9 File Offset: 0x000219B9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2x3 operator -(double2x3 lhs, double2x3 rhs)
		{
			return new double2x3(lhs.c0 - rhs.c0, lhs.c1 - rhs.c1, lhs.c2 - rhs.c2);
		}

		// Token: 0x06000B95 RID: 2965 RVA: 0x000237F3 File Offset: 0x000219F3
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2x3 operator -(double2x3 lhs, double rhs)
		{
			return new double2x3(lhs.c0 - rhs, lhs.c1 - rhs, lhs.c2 - rhs);
		}

		// Token: 0x06000B96 RID: 2966 RVA: 0x0002381E File Offset: 0x00021A1E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2x3 operator -(double lhs, double2x3 rhs)
		{
			return new double2x3(lhs - rhs.c0, lhs - rhs.c1, lhs - rhs.c2);
		}

		// Token: 0x06000B97 RID: 2967 RVA: 0x00023849 File Offset: 0x00021A49
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2x3 operator /(double2x3 lhs, double2x3 rhs)
		{
			return new double2x3(lhs.c0 / rhs.c0, lhs.c1 / rhs.c1, lhs.c2 / rhs.c2);
		}

		// Token: 0x06000B98 RID: 2968 RVA: 0x00023883 File Offset: 0x00021A83
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2x3 operator /(double2x3 lhs, double rhs)
		{
			return new double2x3(lhs.c0 / rhs, lhs.c1 / rhs, lhs.c2 / rhs);
		}

		// Token: 0x06000B99 RID: 2969 RVA: 0x000238AE File Offset: 0x00021AAE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2x3 operator /(double lhs, double2x3 rhs)
		{
			return new double2x3(lhs / rhs.c0, lhs / rhs.c1, lhs / rhs.c2);
		}

		// Token: 0x06000B9A RID: 2970 RVA: 0x000238D9 File Offset: 0x00021AD9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2x3 operator %(double2x3 lhs, double2x3 rhs)
		{
			return new double2x3(lhs.c0 % rhs.c0, lhs.c1 % rhs.c1, lhs.c2 % rhs.c2);
		}

		// Token: 0x06000B9B RID: 2971 RVA: 0x00023913 File Offset: 0x00021B13
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2x3 operator %(double2x3 lhs, double rhs)
		{
			return new double2x3(lhs.c0 % rhs, lhs.c1 % rhs, lhs.c2 % rhs);
		}

		// Token: 0x06000B9C RID: 2972 RVA: 0x0002393E File Offset: 0x00021B3E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2x3 operator %(double lhs, double2x3 rhs)
		{
			return new double2x3(lhs % rhs.c0, lhs % rhs.c1, lhs % rhs.c2);
		}

		// Token: 0x06000B9D RID: 2973 RVA: 0x0002396C File Offset: 0x00021B6C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2x3 operator ++(double2x3 val)
		{
			double2 @double = ++val.c0;
			val.c0 = @double;
			double2 double2 = @double;
			@double = ++val.c1;
			val.c1 = @double;
			double2 double3 = @double;
			@double = ++val.c2;
			val.c2 = @double;
			return new double2x3(double2, double3, @double);
		}

		// Token: 0x06000B9E RID: 2974 RVA: 0x000239CC File Offset: 0x00021BCC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2x3 operator --(double2x3 val)
		{
			double2 @double = --val.c0;
			val.c0 = @double;
			double2 double2 = @double;
			@double = --val.c1;
			val.c1 = @double;
			double2 double3 = @double;
			@double = --val.c2;
			val.c2 = @double;
			return new double2x3(double2, double3, @double);
		}

		// Token: 0x06000B9F RID: 2975 RVA: 0x00023A2C File Offset: 0x00021C2C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x3 operator <(double2x3 lhs, double2x3 rhs)
		{
			return new bool2x3(lhs.c0 < rhs.c0, lhs.c1 < rhs.c1, lhs.c2 < rhs.c2);
		}

		// Token: 0x06000BA0 RID: 2976 RVA: 0x00023A66 File Offset: 0x00021C66
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x3 operator <(double2x3 lhs, double rhs)
		{
			return new bool2x3(lhs.c0 < rhs, lhs.c1 < rhs, lhs.c2 < rhs);
		}

		// Token: 0x06000BA1 RID: 2977 RVA: 0x00023A91 File Offset: 0x00021C91
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x3 operator <(double lhs, double2x3 rhs)
		{
			return new bool2x3(lhs < rhs.c0, lhs < rhs.c1, lhs < rhs.c2);
		}

		// Token: 0x06000BA2 RID: 2978 RVA: 0x00023ABC File Offset: 0x00021CBC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x3 operator <=(double2x3 lhs, double2x3 rhs)
		{
			return new bool2x3(lhs.c0 <= rhs.c0, lhs.c1 <= rhs.c1, lhs.c2 <= rhs.c2);
		}

		// Token: 0x06000BA3 RID: 2979 RVA: 0x00023AF6 File Offset: 0x00021CF6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x3 operator <=(double2x3 lhs, double rhs)
		{
			return new bool2x3(lhs.c0 <= rhs, lhs.c1 <= rhs, lhs.c2 <= rhs);
		}

		// Token: 0x06000BA4 RID: 2980 RVA: 0x00023B21 File Offset: 0x00021D21
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x3 operator <=(double lhs, double2x3 rhs)
		{
			return new bool2x3(lhs <= rhs.c0, lhs <= rhs.c1, lhs <= rhs.c2);
		}

		// Token: 0x06000BA5 RID: 2981 RVA: 0x00023B4C File Offset: 0x00021D4C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x3 operator >(double2x3 lhs, double2x3 rhs)
		{
			return new bool2x3(lhs.c0 > rhs.c0, lhs.c1 > rhs.c1, lhs.c2 > rhs.c2);
		}

		// Token: 0x06000BA6 RID: 2982 RVA: 0x00023B86 File Offset: 0x00021D86
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x3 operator >(double2x3 lhs, double rhs)
		{
			return new bool2x3(lhs.c0 > rhs, lhs.c1 > rhs, lhs.c2 > rhs);
		}

		// Token: 0x06000BA7 RID: 2983 RVA: 0x00023BB1 File Offset: 0x00021DB1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x3 operator >(double lhs, double2x3 rhs)
		{
			return new bool2x3(lhs > rhs.c0, lhs > rhs.c1, lhs > rhs.c2);
		}

		// Token: 0x06000BA8 RID: 2984 RVA: 0x00023BDC File Offset: 0x00021DDC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x3 operator >=(double2x3 lhs, double2x3 rhs)
		{
			return new bool2x3(lhs.c0 >= rhs.c0, lhs.c1 >= rhs.c1, lhs.c2 >= rhs.c2);
		}

		// Token: 0x06000BA9 RID: 2985 RVA: 0x00023C16 File Offset: 0x00021E16
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x3 operator >=(double2x3 lhs, double rhs)
		{
			return new bool2x3(lhs.c0 >= rhs, lhs.c1 >= rhs, lhs.c2 >= rhs);
		}

		// Token: 0x06000BAA RID: 2986 RVA: 0x00023C41 File Offset: 0x00021E41
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x3 operator >=(double lhs, double2x3 rhs)
		{
			return new bool2x3(lhs >= rhs.c0, lhs >= rhs.c1, lhs >= rhs.c2);
		}

		// Token: 0x06000BAB RID: 2987 RVA: 0x00023C6C File Offset: 0x00021E6C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2x3 operator -(double2x3 val)
		{
			return new double2x3(-val.c0, -val.c1, -val.c2);
		}

		// Token: 0x06000BAC RID: 2988 RVA: 0x00023C94 File Offset: 0x00021E94
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2x3 operator +(double2x3 val)
		{
			return new double2x3(+val.c0, +val.c1, +val.c2);
		}

		// Token: 0x06000BAD RID: 2989 RVA: 0x00023CBC File Offset: 0x00021EBC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x3 operator ==(double2x3 lhs, double2x3 rhs)
		{
			return new bool2x3(lhs.c0 == rhs.c0, lhs.c1 == rhs.c1, lhs.c2 == rhs.c2);
		}

		// Token: 0x06000BAE RID: 2990 RVA: 0x00023CF6 File Offset: 0x00021EF6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x3 operator ==(double2x3 lhs, double rhs)
		{
			return new bool2x3(lhs.c0 == rhs, lhs.c1 == rhs, lhs.c2 == rhs);
		}

		// Token: 0x06000BAF RID: 2991 RVA: 0x00023D21 File Offset: 0x00021F21
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x3 operator ==(double lhs, double2x3 rhs)
		{
			return new bool2x3(lhs == rhs.c0, lhs == rhs.c1, lhs == rhs.c2);
		}

		// Token: 0x06000BB0 RID: 2992 RVA: 0x00023D4C File Offset: 0x00021F4C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x3 operator !=(double2x3 lhs, double2x3 rhs)
		{
			return new bool2x3(lhs.c0 != rhs.c0, lhs.c1 != rhs.c1, lhs.c2 != rhs.c2);
		}

		// Token: 0x06000BB1 RID: 2993 RVA: 0x00023D86 File Offset: 0x00021F86
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x3 operator !=(double2x3 lhs, double rhs)
		{
			return new bool2x3(lhs.c0 != rhs, lhs.c1 != rhs, lhs.c2 != rhs);
		}

		// Token: 0x06000BB2 RID: 2994 RVA: 0x00023DB1 File Offset: 0x00021FB1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x3 operator !=(double lhs, double2x3 rhs)
		{
			return new bool2x3(lhs != rhs.c0, lhs != rhs.c1, lhs != rhs.c2);
		}

		// Token: 0x1700020C RID: 524
		public unsafe double2 this[int index]
		{
			get
			{
				fixed (double2x3* ptr = &this)
				{
					return ref *(double2*)(ptr + (IntPtr)index * (IntPtr)sizeof(double2) / (IntPtr)sizeof(double2x3));
				}
			}
		}

		// Token: 0x06000BB4 RID: 2996 RVA: 0x00023DF7 File Offset: 0x00021FF7
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool Equals(double2x3 rhs)
		{
			return this.c0.Equals(rhs.c0) && this.c1.Equals(rhs.c1) && this.c2.Equals(rhs.c2);
		}

		// Token: 0x06000BB5 RID: 2997 RVA: 0x00023E34 File Offset: 0x00022034
		public override bool Equals(object o)
		{
			if (o is double2x3)
			{
				double2x3 rhs = (double2x3)o;
				return this.Equals(rhs);
			}
			return false;
		}

		// Token: 0x06000BB6 RID: 2998 RVA: 0x00023E59 File Offset: 0x00022059
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override int GetHashCode()
		{
			return (int)math.hash(this);
		}

		// Token: 0x06000BB7 RID: 2999 RVA: 0x00023E68 File Offset: 0x00022068
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override string ToString()
		{
			return string.Format("double2x3({0}, {1}, {2},  {3}, {4}, {5})", new object[]
			{
				this.c0.x,
				this.c1.x,
				this.c2.x,
				this.c0.y,
				this.c1.y,
				this.c2.y
			});
		}

		// Token: 0x06000BB8 RID: 3000 RVA: 0x00023EF8 File Offset: 0x000220F8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public string ToString(string format, IFormatProvider formatProvider)
		{
			return string.Format("double2x3({0}, {1}, {2},  {3}, {4}, {5})", new object[]
			{
				this.c0.x.ToString(format, formatProvider),
				this.c1.x.ToString(format, formatProvider),
				this.c2.x.ToString(format, formatProvider),
				this.c0.y.ToString(format, formatProvider),
				this.c1.y.ToString(format, formatProvider),
				this.c2.y.ToString(format, formatProvider)
			});
		}

		// Token: 0x04000042 RID: 66
		public double2 c0;

		// Token: 0x04000043 RID: 67
		public double2 c1;

		// Token: 0x04000044 RID: 68
		public double2 c2;

		// Token: 0x04000045 RID: 69
		public static readonly double2x3 zero;
	}
}

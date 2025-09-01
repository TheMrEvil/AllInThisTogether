using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Unity.IL2CPP.CompilerServices;

namespace Unity.Mathematics
{
	// Token: 0x02000010 RID: 16
	[DebuggerTypeProxy(typeof(double2.DebuggerProxy))]
	[Il2CppEagerStaticClassConstruction]
	[Serializable]
	public struct double2 : IEquatable<double2>, IFormattable
	{
		// Token: 0x06000AD8 RID: 2776 RVA: 0x0002208D File Offset: 0x0002028D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public double2(double x, double y)
		{
			this.x = x;
			this.y = y;
		}

		// Token: 0x06000AD9 RID: 2777 RVA: 0x0002209D File Offset: 0x0002029D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public double2(double2 xy)
		{
			this.x = xy.x;
			this.y = xy.y;
		}

		// Token: 0x06000ADA RID: 2778 RVA: 0x000220B7 File Offset: 0x000202B7
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public double2(double v)
		{
			this.x = v;
			this.y = v;
		}

		// Token: 0x06000ADB RID: 2779 RVA: 0x000220C7 File Offset: 0x000202C7
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public double2(bool v)
		{
			this.x = (v ? 1.0 : 0.0);
			this.y = (v ? 1.0 : 0.0);
		}

		// Token: 0x06000ADC RID: 2780 RVA: 0x00022104 File Offset: 0x00020304
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public double2(bool2 v)
		{
			this.x = (v.x ? 1.0 : 0.0);
			this.y = (v.y ? 1.0 : 0.0);
		}

		// Token: 0x06000ADD RID: 2781 RVA: 0x00022155 File Offset: 0x00020355
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public double2(int v)
		{
			this.x = (double)v;
			this.y = (double)v;
		}

		// Token: 0x06000ADE RID: 2782 RVA: 0x00022167 File Offset: 0x00020367
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public double2(int2 v)
		{
			this.x = (double)v.x;
			this.y = (double)v.y;
		}

		// Token: 0x06000ADF RID: 2783 RVA: 0x00022183 File Offset: 0x00020383
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public double2(uint v)
		{
			this.x = v;
			this.y = v;
		}

		// Token: 0x06000AE0 RID: 2784 RVA: 0x00022197 File Offset: 0x00020397
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public double2(uint2 v)
		{
			this.x = v.x;
			this.y = v.y;
		}

		// Token: 0x06000AE1 RID: 2785 RVA: 0x000221B5 File Offset: 0x000203B5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public double2(half v)
		{
			this.x = v;
			this.y = v;
		}

		// Token: 0x06000AE2 RID: 2786 RVA: 0x000221CF File Offset: 0x000203CF
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public double2(half2 v)
		{
			this.x = v.x;
			this.y = v.y;
		}

		// Token: 0x06000AE3 RID: 2787 RVA: 0x000221F3 File Offset: 0x000203F3
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public double2(float v)
		{
			this.x = (double)v;
			this.y = (double)v;
		}

		// Token: 0x06000AE4 RID: 2788 RVA: 0x00022205 File Offset: 0x00020405
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public double2(float2 v)
		{
			this.x = (double)v.x;
			this.y = (double)v.y;
		}

		// Token: 0x06000AE5 RID: 2789 RVA: 0x00022221 File Offset: 0x00020421
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator double2(double v)
		{
			return new double2(v);
		}

		// Token: 0x06000AE6 RID: 2790 RVA: 0x00022229 File Offset: 0x00020429
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator double2(bool v)
		{
			return new double2(v);
		}

		// Token: 0x06000AE7 RID: 2791 RVA: 0x00022231 File Offset: 0x00020431
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator double2(bool2 v)
		{
			return new double2(v);
		}

		// Token: 0x06000AE8 RID: 2792 RVA: 0x00022239 File Offset: 0x00020439
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator double2(int v)
		{
			return new double2(v);
		}

		// Token: 0x06000AE9 RID: 2793 RVA: 0x00022241 File Offset: 0x00020441
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator double2(int2 v)
		{
			return new double2(v);
		}

		// Token: 0x06000AEA RID: 2794 RVA: 0x00022249 File Offset: 0x00020449
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator double2(uint v)
		{
			return new double2(v);
		}

		// Token: 0x06000AEB RID: 2795 RVA: 0x00022251 File Offset: 0x00020451
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator double2(uint2 v)
		{
			return new double2(v);
		}

		// Token: 0x06000AEC RID: 2796 RVA: 0x00022259 File Offset: 0x00020459
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator double2(half v)
		{
			return new double2(v);
		}

		// Token: 0x06000AED RID: 2797 RVA: 0x00022261 File Offset: 0x00020461
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator double2(half2 v)
		{
			return new double2(v);
		}

		// Token: 0x06000AEE RID: 2798 RVA: 0x00022269 File Offset: 0x00020469
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator double2(float v)
		{
			return new double2(v);
		}

		// Token: 0x06000AEF RID: 2799 RVA: 0x00022271 File Offset: 0x00020471
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator double2(float2 v)
		{
			return new double2(v);
		}

		// Token: 0x06000AF0 RID: 2800 RVA: 0x00022279 File Offset: 0x00020479
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2 operator *(double2 lhs, double2 rhs)
		{
			return new double2(lhs.x * rhs.x, lhs.y * rhs.y);
		}

		// Token: 0x06000AF1 RID: 2801 RVA: 0x0002229A File Offset: 0x0002049A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2 operator *(double2 lhs, double rhs)
		{
			return new double2(lhs.x * rhs, lhs.y * rhs);
		}

		// Token: 0x06000AF2 RID: 2802 RVA: 0x000222B1 File Offset: 0x000204B1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2 operator *(double lhs, double2 rhs)
		{
			return new double2(lhs * rhs.x, lhs * rhs.y);
		}

		// Token: 0x06000AF3 RID: 2803 RVA: 0x000222C8 File Offset: 0x000204C8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2 operator +(double2 lhs, double2 rhs)
		{
			return new double2(lhs.x + rhs.x, lhs.y + rhs.y);
		}

		// Token: 0x06000AF4 RID: 2804 RVA: 0x000222E9 File Offset: 0x000204E9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2 operator +(double2 lhs, double rhs)
		{
			return new double2(lhs.x + rhs, lhs.y + rhs);
		}

		// Token: 0x06000AF5 RID: 2805 RVA: 0x00022300 File Offset: 0x00020500
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2 operator +(double lhs, double2 rhs)
		{
			return new double2(lhs + rhs.x, lhs + rhs.y);
		}

		// Token: 0x06000AF6 RID: 2806 RVA: 0x00022317 File Offset: 0x00020517
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2 operator -(double2 lhs, double2 rhs)
		{
			return new double2(lhs.x - rhs.x, lhs.y - rhs.y);
		}

		// Token: 0x06000AF7 RID: 2807 RVA: 0x00022338 File Offset: 0x00020538
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2 operator -(double2 lhs, double rhs)
		{
			return new double2(lhs.x - rhs, lhs.y - rhs);
		}

		// Token: 0x06000AF8 RID: 2808 RVA: 0x0002234F File Offset: 0x0002054F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2 operator -(double lhs, double2 rhs)
		{
			return new double2(lhs - rhs.x, lhs - rhs.y);
		}

		// Token: 0x06000AF9 RID: 2809 RVA: 0x00022366 File Offset: 0x00020566
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2 operator /(double2 lhs, double2 rhs)
		{
			return new double2(lhs.x / rhs.x, lhs.y / rhs.y);
		}

		// Token: 0x06000AFA RID: 2810 RVA: 0x00022387 File Offset: 0x00020587
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2 operator /(double2 lhs, double rhs)
		{
			return new double2(lhs.x / rhs, lhs.y / rhs);
		}

		// Token: 0x06000AFB RID: 2811 RVA: 0x0002239E File Offset: 0x0002059E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2 operator /(double lhs, double2 rhs)
		{
			return new double2(lhs / rhs.x, lhs / rhs.y);
		}

		// Token: 0x06000AFC RID: 2812 RVA: 0x000223B5 File Offset: 0x000205B5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2 operator %(double2 lhs, double2 rhs)
		{
			return new double2(lhs.x % rhs.x, lhs.y % rhs.y);
		}

		// Token: 0x06000AFD RID: 2813 RVA: 0x000223D6 File Offset: 0x000205D6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2 operator %(double2 lhs, double rhs)
		{
			return new double2(lhs.x % rhs, lhs.y % rhs);
		}

		// Token: 0x06000AFE RID: 2814 RVA: 0x000223ED File Offset: 0x000205ED
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2 operator %(double lhs, double2 rhs)
		{
			return new double2(lhs % rhs.x, lhs % rhs.y);
		}

		// Token: 0x06000AFF RID: 2815 RVA: 0x00022404 File Offset: 0x00020604
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2 operator ++(double2 val)
		{
			double num = val.x + 1.0;
			val.x = num;
			double num2 = num;
			num = val.y + 1.0;
			val.y = num;
			return new double2(num2, num);
		}

		// Token: 0x06000B00 RID: 2816 RVA: 0x00022444 File Offset: 0x00020644
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2 operator --(double2 val)
		{
			double num = val.x - 1.0;
			val.x = num;
			double num2 = num;
			num = val.y - 1.0;
			val.y = num;
			return new double2(num2, num);
		}

		// Token: 0x06000B01 RID: 2817 RVA: 0x00022484 File Offset: 0x00020684
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2 operator <(double2 lhs, double2 rhs)
		{
			return new bool2(lhs.x < rhs.x, lhs.y < rhs.y);
		}

		// Token: 0x06000B02 RID: 2818 RVA: 0x000224A7 File Offset: 0x000206A7
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2 operator <(double2 lhs, double rhs)
		{
			return new bool2(lhs.x < rhs, lhs.y < rhs);
		}

		// Token: 0x06000B03 RID: 2819 RVA: 0x000224C0 File Offset: 0x000206C0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2 operator <(double lhs, double2 rhs)
		{
			return new bool2(lhs < rhs.x, lhs < rhs.y);
		}

		// Token: 0x06000B04 RID: 2820 RVA: 0x000224D9 File Offset: 0x000206D9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2 operator <=(double2 lhs, double2 rhs)
		{
			return new bool2(lhs.x <= rhs.x, lhs.y <= rhs.y);
		}

		// Token: 0x06000B05 RID: 2821 RVA: 0x00022502 File Offset: 0x00020702
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2 operator <=(double2 lhs, double rhs)
		{
			return new bool2(lhs.x <= rhs, lhs.y <= rhs);
		}

		// Token: 0x06000B06 RID: 2822 RVA: 0x00022521 File Offset: 0x00020721
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2 operator <=(double lhs, double2 rhs)
		{
			return new bool2(lhs <= rhs.x, lhs <= rhs.y);
		}

		// Token: 0x06000B07 RID: 2823 RVA: 0x00022540 File Offset: 0x00020740
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2 operator >(double2 lhs, double2 rhs)
		{
			return new bool2(lhs.x > rhs.x, lhs.y > rhs.y);
		}

		// Token: 0x06000B08 RID: 2824 RVA: 0x00022563 File Offset: 0x00020763
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2 operator >(double2 lhs, double rhs)
		{
			return new bool2(lhs.x > rhs, lhs.y > rhs);
		}

		// Token: 0x06000B09 RID: 2825 RVA: 0x0002257C File Offset: 0x0002077C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2 operator >(double lhs, double2 rhs)
		{
			return new bool2(lhs > rhs.x, lhs > rhs.y);
		}

		// Token: 0x06000B0A RID: 2826 RVA: 0x00022595 File Offset: 0x00020795
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2 operator >=(double2 lhs, double2 rhs)
		{
			return new bool2(lhs.x >= rhs.x, lhs.y >= rhs.y);
		}

		// Token: 0x06000B0B RID: 2827 RVA: 0x000225BE File Offset: 0x000207BE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2 operator >=(double2 lhs, double rhs)
		{
			return new bool2(lhs.x >= rhs, lhs.y >= rhs);
		}

		// Token: 0x06000B0C RID: 2828 RVA: 0x000225DD File Offset: 0x000207DD
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2 operator >=(double lhs, double2 rhs)
		{
			return new bool2(lhs >= rhs.x, lhs >= rhs.y);
		}

		// Token: 0x06000B0D RID: 2829 RVA: 0x000225FC File Offset: 0x000207FC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2 operator -(double2 val)
		{
			return new double2(-val.x, -val.y);
		}

		// Token: 0x06000B0E RID: 2830 RVA: 0x00022611 File Offset: 0x00020811
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2 operator +(double2 val)
		{
			return new double2(val.x, val.y);
		}

		// Token: 0x06000B0F RID: 2831 RVA: 0x00022624 File Offset: 0x00020824
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2 operator ==(double2 lhs, double2 rhs)
		{
			return new bool2(lhs.x == rhs.x, lhs.y == rhs.y);
		}

		// Token: 0x06000B10 RID: 2832 RVA: 0x00022647 File Offset: 0x00020847
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2 operator ==(double2 lhs, double rhs)
		{
			return new bool2(lhs.x == rhs, lhs.y == rhs);
		}

		// Token: 0x06000B11 RID: 2833 RVA: 0x00022660 File Offset: 0x00020860
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2 operator ==(double lhs, double2 rhs)
		{
			return new bool2(lhs == rhs.x, lhs == rhs.y);
		}

		// Token: 0x06000B12 RID: 2834 RVA: 0x00022679 File Offset: 0x00020879
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2 operator !=(double2 lhs, double2 rhs)
		{
			return new bool2(lhs.x != rhs.x, lhs.y != rhs.y);
		}

		// Token: 0x06000B13 RID: 2835 RVA: 0x000226A2 File Offset: 0x000208A2
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2 operator !=(double2 lhs, double rhs)
		{
			return new bool2(lhs.x != rhs, lhs.y != rhs);
		}

		// Token: 0x06000B14 RID: 2836 RVA: 0x000226C1 File Offset: 0x000208C1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2 operator !=(double lhs, double2 rhs)
		{
			return new bool2(lhs != rhs.x, lhs != rhs.y);
		}

		// Token: 0x170001EE RID: 494
		// (get) Token: 0x06000B15 RID: 2837 RVA: 0x000226E0 File Offset: 0x000208E0
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 xxxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.x, this.x, this.x, this.x);
			}
		}

		// Token: 0x170001EF RID: 495
		// (get) Token: 0x06000B16 RID: 2838 RVA: 0x000226FF File Offset: 0x000208FF
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 xxxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.x, this.x, this.x, this.y);
			}
		}

		// Token: 0x170001F0 RID: 496
		// (get) Token: 0x06000B17 RID: 2839 RVA: 0x0002271E File Offset: 0x0002091E
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 xxyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.x, this.x, this.y, this.x);
			}
		}

		// Token: 0x170001F1 RID: 497
		// (get) Token: 0x06000B18 RID: 2840 RVA: 0x0002273D File Offset: 0x0002093D
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 xxyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.x, this.x, this.y, this.y);
			}
		}

		// Token: 0x170001F2 RID: 498
		// (get) Token: 0x06000B19 RID: 2841 RVA: 0x0002275C File Offset: 0x0002095C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 xyxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.x, this.y, this.x, this.x);
			}
		}

		// Token: 0x170001F3 RID: 499
		// (get) Token: 0x06000B1A RID: 2842 RVA: 0x0002277B File Offset: 0x0002097B
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 xyxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.x, this.y, this.x, this.y);
			}
		}

		// Token: 0x170001F4 RID: 500
		// (get) Token: 0x06000B1B RID: 2843 RVA: 0x0002279A File Offset: 0x0002099A
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 xyyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.x, this.y, this.y, this.x);
			}
		}

		// Token: 0x170001F5 RID: 501
		// (get) Token: 0x06000B1C RID: 2844 RVA: 0x000227B9 File Offset: 0x000209B9
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 xyyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.x, this.y, this.y, this.y);
			}
		}

		// Token: 0x170001F6 RID: 502
		// (get) Token: 0x06000B1D RID: 2845 RVA: 0x000227D8 File Offset: 0x000209D8
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 yxxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.y, this.x, this.x, this.x);
			}
		}

		// Token: 0x170001F7 RID: 503
		// (get) Token: 0x06000B1E RID: 2846 RVA: 0x000227F7 File Offset: 0x000209F7
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 yxxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.y, this.x, this.x, this.y);
			}
		}

		// Token: 0x170001F8 RID: 504
		// (get) Token: 0x06000B1F RID: 2847 RVA: 0x00022816 File Offset: 0x00020A16
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 yxyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.y, this.x, this.y, this.x);
			}
		}

		// Token: 0x170001F9 RID: 505
		// (get) Token: 0x06000B20 RID: 2848 RVA: 0x00022835 File Offset: 0x00020A35
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 yxyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.y, this.x, this.y, this.y);
			}
		}

		// Token: 0x170001FA RID: 506
		// (get) Token: 0x06000B21 RID: 2849 RVA: 0x00022854 File Offset: 0x00020A54
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 yyxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.y, this.y, this.x, this.x);
			}
		}

		// Token: 0x170001FB RID: 507
		// (get) Token: 0x06000B22 RID: 2850 RVA: 0x00022873 File Offset: 0x00020A73
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 yyxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.y, this.y, this.x, this.y);
			}
		}

		// Token: 0x170001FC RID: 508
		// (get) Token: 0x06000B23 RID: 2851 RVA: 0x00022892 File Offset: 0x00020A92
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 yyyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.y, this.y, this.y, this.x);
			}
		}

		// Token: 0x170001FD RID: 509
		// (get) Token: 0x06000B24 RID: 2852 RVA: 0x000228B1 File Offset: 0x00020AB1
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 yyyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.y, this.y, this.y, this.y);
			}
		}

		// Token: 0x170001FE RID: 510
		// (get) Token: 0x06000B25 RID: 2853 RVA: 0x000228D0 File Offset: 0x00020AD0
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double3 xxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double3(this.x, this.x, this.x);
			}
		}

		// Token: 0x170001FF RID: 511
		// (get) Token: 0x06000B26 RID: 2854 RVA: 0x000228E9 File Offset: 0x00020AE9
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double3 xxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double3(this.x, this.x, this.y);
			}
		}

		// Token: 0x17000200 RID: 512
		// (get) Token: 0x06000B27 RID: 2855 RVA: 0x00022902 File Offset: 0x00020B02
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double3 xyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double3(this.x, this.y, this.x);
			}
		}

		// Token: 0x17000201 RID: 513
		// (get) Token: 0x06000B28 RID: 2856 RVA: 0x0002291B File Offset: 0x00020B1B
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double3 xyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double3(this.x, this.y, this.y);
			}
		}

		// Token: 0x17000202 RID: 514
		// (get) Token: 0x06000B29 RID: 2857 RVA: 0x00022934 File Offset: 0x00020B34
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double3 yxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double3(this.y, this.x, this.x);
			}
		}

		// Token: 0x17000203 RID: 515
		// (get) Token: 0x06000B2A RID: 2858 RVA: 0x0002294D File Offset: 0x00020B4D
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double3 yxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double3(this.y, this.x, this.y);
			}
		}

		// Token: 0x17000204 RID: 516
		// (get) Token: 0x06000B2B RID: 2859 RVA: 0x00022966 File Offset: 0x00020B66
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double3 yyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double3(this.y, this.y, this.x);
			}
		}

		// Token: 0x17000205 RID: 517
		// (get) Token: 0x06000B2C RID: 2860 RVA: 0x0002297F File Offset: 0x00020B7F
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double3 yyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double3(this.y, this.y, this.y);
			}
		}

		// Token: 0x17000206 RID: 518
		// (get) Token: 0x06000B2D RID: 2861 RVA: 0x00022998 File Offset: 0x00020B98
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double2 xx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double2(this.x, this.x);
			}
		}

		// Token: 0x17000207 RID: 519
		// (get) Token: 0x06000B2E RID: 2862 RVA: 0x000229AB File Offset: 0x00020BAB
		// (set) Token: 0x06000B2F RID: 2863 RVA: 0x000229BE File Offset: 0x00020BBE
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double2 xy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double2(this.x, this.y);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.x = value.x;
				this.y = value.y;
			}
		}

		// Token: 0x17000208 RID: 520
		// (get) Token: 0x06000B30 RID: 2864 RVA: 0x000229D8 File Offset: 0x00020BD8
		// (set) Token: 0x06000B31 RID: 2865 RVA: 0x000229EB File Offset: 0x00020BEB
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double2 yx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double2(this.y, this.x);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.y = value.x;
				this.x = value.y;
			}
		}

		// Token: 0x17000209 RID: 521
		// (get) Token: 0x06000B32 RID: 2866 RVA: 0x00022A05 File Offset: 0x00020C05
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double2 yy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double2(this.y, this.y);
			}
		}

		// Token: 0x1700020A RID: 522
		public unsafe double this[int index]
		{
			get
			{
				fixed (double2* ptr = &this)
				{
					return ((double*)ptr)[index];
				}
			}
			set
			{
				fixed (double* ptr = &this.x)
				{
					ptr[index] = value;
				}
			}
		}

		// Token: 0x06000B35 RID: 2869 RVA: 0x00022A50 File Offset: 0x00020C50
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool Equals(double2 rhs)
		{
			return this.x == rhs.x && this.y == rhs.y;
		}

		// Token: 0x06000B36 RID: 2870 RVA: 0x00022A70 File Offset: 0x00020C70
		public override bool Equals(object o)
		{
			if (o is double2)
			{
				double2 rhs = (double2)o;
				return this.Equals(rhs);
			}
			return false;
		}

		// Token: 0x06000B37 RID: 2871 RVA: 0x00022A95 File Offset: 0x00020C95
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override int GetHashCode()
		{
			return (int)math.hash(this);
		}

		// Token: 0x06000B38 RID: 2872 RVA: 0x00022AA2 File Offset: 0x00020CA2
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override string ToString()
		{
			return string.Format("double2({0}, {1})", this.x, this.y);
		}

		// Token: 0x06000B39 RID: 2873 RVA: 0x00022AC4 File Offset: 0x00020CC4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public string ToString(string format, IFormatProvider formatProvider)
		{
			return string.Format("double2({0}, {1})", this.x.ToString(format, formatProvider), this.y.ToString(format, formatProvider));
		}

		// Token: 0x0400003B RID: 59
		public double x;

		// Token: 0x0400003C RID: 60
		public double y;

		// Token: 0x0400003D RID: 61
		public static readonly double2 zero;

		// Token: 0x02000054 RID: 84
		internal sealed class DebuggerProxy
		{
			// Token: 0x0600246A RID: 9322 RVA: 0x00067508 File Offset: 0x00065708
			public DebuggerProxy(double2 v)
			{
				this.x = v.x;
				this.y = v.y;
			}

			// Token: 0x0400013C RID: 316
			public double x;

			// Token: 0x0400013D RID: 317
			public double y;
		}
	}
}

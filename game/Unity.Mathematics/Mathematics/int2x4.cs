using System;
using System.Runtime.CompilerServices;
using Unity.IL2CPP.CompilerServices;

namespace Unity.Mathematics
{
	// Token: 0x0200002F RID: 47
	[Il2CppEagerStaticClassConstruction]
	[Serializable]
	public struct int2x4 : IEquatable<int2x4>, IFormattable
	{
		// Token: 0x06001921 RID: 6433 RVA: 0x000450A7 File Offset: 0x000432A7
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int2x4(int2 c0, int2 c1, int2 c2, int2 c3)
		{
			this.c0 = c0;
			this.c1 = c1;
			this.c2 = c2;
			this.c3 = c3;
		}

		// Token: 0x06001922 RID: 6434 RVA: 0x000450C6 File Offset: 0x000432C6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int2x4(int m00, int m01, int m02, int m03, int m10, int m11, int m12, int m13)
		{
			this.c0 = new int2(m00, m10);
			this.c1 = new int2(m01, m11);
			this.c2 = new int2(m02, m12);
			this.c3 = new int2(m03, m13);
		}

		// Token: 0x06001923 RID: 6435 RVA: 0x00045101 File Offset: 0x00043301
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int2x4(int v)
		{
			this.c0 = v;
			this.c1 = v;
			this.c2 = v;
			this.c3 = v;
		}

		// Token: 0x06001924 RID: 6436 RVA: 0x00045134 File Offset: 0x00043334
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int2x4(bool v)
		{
			this.c0 = math.select(new int2(0), new int2(1), v);
			this.c1 = math.select(new int2(0), new int2(1), v);
			this.c2 = math.select(new int2(0), new int2(1), v);
			this.c3 = math.select(new int2(0), new int2(1), v);
		}

		// Token: 0x06001925 RID: 6437 RVA: 0x000451A4 File Offset: 0x000433A4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int2x4(bool2x4 v)
		{
			this.c0 = math.select(new int2(0), new int2(1), v.c0);
			this.c1 = math.select(new int2(0), new int2(1), v.c1);
			this.c2 = math.select(new int2(0), new int2(1), v.c2);
			this.c3 = math.select(new int2(0), new int2(1), v.c3);
		}

		// Token: 0x06001926 RID: 6438 RVA: 0x00045225 File Offset: 0x00043425
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int2x4(uint v)
		{
			this.c0 = (int2)v;
			this.c1 = (int2)v;
			this.c2 = (int2)v;
			this.c3 = (int2)v;
		}

		// Token: 0x06001927 RID: 6439 RVA: 0x00045258 File Offset: 0x00043458
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int2x4(uint2x4 v)
		{
			this.c0 = (int2)v.c0;
			this.c1 = (int2)v.c1;
			this.c2 = (int2)v.c2;
			this.c3 = (int2)v.c3;
		}

		// Token: 0x06001928 RID: 6440 RVA: 0x000452A9 File Offset: 0x000434A9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int2x4(float v)
		{
			this.c0 = (int2)v;
			this.c1 = (int2)v;
			this.c2 = (int2)v;
			this.c3 = (int2)v;
		}

		// Token: 0x06001929 RID: 6441 RVA: 0x000452DC File Offset: 0x000434DC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int2x4(float2x4 v)
		{
			this.c0 = (int2)v.c0;
			this.c1 = (int2)v.c1;
			this.c2 = (int2)v.c2;
			this.c3 = (int2)v.c3;
		}

		// Token: 0x0600192A RID: 6442 RVA: 0x0004532D File Offset: 0x0004352D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int2x4(double v)
		{
			this.c0 = (int2)v;
			this.c1 = (int2)v;
			this.c2 = (int2)v;
			this.c3 = (int2)v;
		}

		// Token: 0x0600192B RID: 6443 RVA: 0x00045360 File Offset: 0x00043560
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int2x4(double2x4 v)
		{
			this.c0 = (int2)v.c0;
			this.c1 = (int2)v.c1;
			this.c2 = (int2)v.c2;
			this.c3 = (int2)v.c3;
		}

		// Token: 0x0600192C RID: 6444 RVA: 0x000453B1 File Offset: 0x000435B1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator int2x4(int v)
		{
			return new int2x4(v);
		}

		// Token: 0x0600192D RID: 6445 RVA: 0x000453B9 File Offset: 0x000435B9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator int2x4(bool v)
		{
			return new int2x4(v);
		}

		// Token: 0x0600192E RID: 6446 RVA: 0x000453C1 File Offset: 0x000435C1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator int2x4(bool2x4 v)
		{
			return new int2x4(v);
		}

		// Token: 0x0600192F RID: 6447 RVA: 0x000453C9 File Offset: 0x000435C9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator int2x4(uint v)
		{
			return new int2x4(v);
		}

		// Token: 0x06001930 RID: 6448 RVA: 0x000453D1 File Offset: 0x000435D1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator int2x4(uint2x4 v)
		{
			return new int2x4(v);
		}

		// Token: 0x06001931 RID: 6449 RVA: 0x000453D9 File Offset: 0x000435D9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator int2x4(float v)
		{
			return new int2x4(v);
		}

		// Token: 0x06001932 RID: 6450 RVA: 0x000453E1 File Offset: 0x000435E1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator int2x4(float2x4 v)
		{
			return new int2x4(v);
		}

		// Token: 0x06001933 RID: 6451 RVA: 0x000453E9 File Offset: 0x000435E9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator int2x4(double v)
		{
			return new int2x4(v);
		}

		// Token: 0x06001934 RID: 6452 RVA: 0x000453F1 File Offset: 0x000435F1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator int2x4(double2x4 v)
		{
			return new int2x4(v);
		}

		// Token: 0x06001935 RID: 6453 RVA: 0x000453FC File Offset: 0x000435FC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2x4 operator *(int2x4 lhs, int2x4 rhs)
		{
			return new int2x4(lhs.c0 * rhs.c0, lhs.c1 * rhs.c1, lhs.c2 * rhs.c2, lhs.c3 * rhs.c3);
		}

		// Token: 0x06001936 RID: 6454 RVA: 0x00045452 File Offset: 0x00043652
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2x4 operator *(int2x4 lhs, int rhs)
		{
			return new int2x4(lhs.c0 * rhs, lhs.c1 * rhs, lhs.c2 * rhs, lhs.c3 * rhs);
		}

		// Token: 0x06001937 RID: 6455 RVA: 0x00045489 File Offset: 0x00043689
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2x4 operator *(int lhs, int2x4 rhs)
		{
			return new int2x4(lhs * rhs.c0, lhs * rhs.c1, lhs * rhs.c2, lhs * rhs.c3);
		}

		// Token: 0x06001938 RID: 6456 RVA: 0x000454C0 File Offset: 0x000436C0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2x4 operator +(int2x4 lhs, int2x4 rhs)
		{
			return new int2x4(lhs.c0 + rhs.c0, lhs.c1 + rhs.c1, lhs.c2 + rhs.c2, lhs.c3 + rhs.c3);
		}

		// Token: 0x06001939 RID: 6457 RVA: 0x00045516 File Offset: 0x00043716
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2x4 operator +(int2x4 lhs, int rhs)
		{
			return new int2x4(lhs.c0 + rhs, lhs.c1 + rhs, lhs.c2 + rhs, lhs.c3 + rhs);
		}

		// Token: 0x0600193A RID: 6458 RVA: 0x0004554D File Offset: 0x0004374D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2x4 operator +(int lhs, int2x4 rhs)
		{
			return new int2x4(lhs + rhs.c0, lhs + rhs.c1, lhs + rhs.c2, lhs + rhs.c3);
		}

		// Token: 0x0600193B RID: 6459 RVA: 0x00045584 File Offset: 0x00043784
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2x4 operator -(int2x4 lhs, int2x4 rhs)
		{
			return new int2x4(lhs.c0 - rhs.c0, lhs.c1 - rhs.c1, lhs.c2 - rhs.c2, lhs.c3 - rhs.c3);
		}

		// Token: 0x0600193C RID: 6460 RVA: 0x000455DA File Offset: 0x000437DA
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2x4 operator -(int2x4 lhs, int rhs)
		{
			return new int2x4(lhs.c0 - rhs, lhs.c1 - rhs, lhs.c2 - rhs, lhs.c3 - rhs);
		}

		// Token: 0x0600193D RID: 6461 RVA: 0x00045611 File Offset: 0x00043811
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2x4 operator -(int lhs, int2x4 rhs)
		{
			return new int2x4(lhs - rhs.c0, lhs - rhs.c1, lhs - rhs.c2, lhs - rhs.c3);
		}

		// Token: 0x0600193E RID: 6462 RVA: 0x00045648 File Offset: 0x00043848
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2x4 operator /(int2x4 lhs, int2x4 rhs)
		{
			return new int2x4(lhs.c0 / rhs.c0, lhs.c1 / rhs.c1, lhs.c2 / rhs.c2, lhs.c3 / rhs.c3);
		}

		// Token: 0x0600193F RID: 6463 RVA: 0x0004569E File Offset: 0x0004389E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2x4 operator /(int2x4 lhs, int rhs)
		{
			return new int2x4(lhs.c0 / rhs, lhs.c1 / rhs, lhs.c2 / rhs, lhs.c3 / rhs);
		}

		// Token: 0x06001940 RID: 6464 RVA: 0x000456D5 File Offset: 0x000438D5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2x4 operator /(int lhs, int2x4 rhs)
		{
			return new int2x4(lhs / rhs.c0, lhs / rhs.c1, lhs / rhs.c2, lhs / rhs.c3);
		}

		// Token: 0x06001941 RID: 6465 RVA: 0x0004570C File Offset: 0x0004390C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2x4 operator %(int2x4 lhs, int2x4 rhs)
		{
			return new int2x4(lhs.c0 % rhs.c0, lhs.c1 % rhs.c1, lhs.c2 % rhs.c2, lhs.c3 % rhs.c3);
		}

		// Token: 0x06001942 RID: 6466 RVA: 0x00045762 File Offset: 0x00043962
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2x4 operator %(int2x4 lhs, int rhs)
		{
			return new int2x4(lhs.c0 % rhs, lhs.c1 % rhs, lhs.c2 % rhs, lhs.c3 % rhs);
		}

		// Token: 0x06001943 RID: 6467 RVA: 0x00045799 File Offset: 0x00043999
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2x4 operator %(int lhs, int2x4 rhs)
		{
			return new int2x4(lhs % rhs.c0, lhs % rhs.c1, lhs % rhs.c2, lhs % rhs.c3);
		}

		// Token: 0x06001944 RID: 6468 RVA: 0x000457D0 File Offset: 0x000439D0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2x4 operator ++(int2x4 val)
		{
			int2 @int = ++val.c0;
			val.c0 = @int;
			int2 int2 = @int;
			@int = ++val.c1;
			val.c1 = @int;
			int2 int3 = @int;
			@int = ++val.c2;
			val.c2 = @int;
			int2 int4 = @int;
			@int = ++val.c3;
			val.c3 = @int;
			return new int2x4(int2, int3, int4, @int);
		}

		// Token: 0x06001945 RID: 6469 RVA: 0x0004584C File Offset: 0x00043A4C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2x4 operator --(int2x4 val)
		{
			int2 @int = --val.c0;
			val.c0 = @int;
			int2 int2 = @int;
			@int = --val.c1;
			val.c1 = @int;
			int2 int3 = @int;
			@int = --val.c2;
			val.c2 = @int;
			int2 int4 = @int;
			@int = --val.c3;
			val.c3 = @int;
			return new int2x4(int2, int3, int4, @int);
		}

		// Token: 0x06001946 RID: 6470 RVA: 0x000458C8 File Offset: 0x00043AC8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x4 operator <(int2x4 lhs, int2x4 rhs)
		{
			return new bool2x4(lhs.c0 < rhs.c0, lhs.c1 < rhs.c1, lhs.c2 < rhs.c2, lhs.c3 < rhs.c3);
		}

		// Token: 0x06001947 RID: 6471 RVA: 0x0004591E File Offset: 0x00043B1E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x4 operator <(int2x4 lhs, int rhs)
		{
			return new bool2x4(lhs.c0 < rhs, lhs.c1 < rhs, lhs.c2 < rhs, lhs.c3 < rhs);
		}

		// Token: 0x06001948 RID: 6472 RVA: 0x00045955 File Offset: 0x00043B55
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x4 operator <(int lhs, int2x4 rhs)
		{
			return new bool2x4(lhs < rhs.c0, lhs < rhs.c1, lhs < rhs.c2, lhs < rhs.c3);
		}

		// Token: 0x06001949 RID: 6473 RVA: 0x0004598C File Offset: 0x00043B8C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x4 operator <=(int2x4 lhs, int2x4 rhs)
		{
			return new bool2x4(lhs.c0 <= rhs.c0, lhs.c1 <= rhs.c1, lhs.c2 <= rhs.c2, lhs.c3 <= rhs.c3);
		}

		// Token: 0x0600194A RID: 6474 RVA: 0x000459E2 File Offset: 0x00043BE2
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x4 operator <=(int2x4 lhs, int rhs)
		{
			return new bool2x4(lhs.c0 <= rhs, lhs.c1 <= rhs, lhs.c2 <= rhs, lhs.c3 <= rhs);
		}

		// Token: 0x0600194B RID: 6475 RVA: 0x00045A19 File Offset: 0x00043C19
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x4 operator <=(int lhs, int2x4 rhs)
		{
			return new bool2x4(lhs <= rhs.c0, lhs <= rhs.c1, lhs <= rhs.c2, lhs <= rhs.c3);
		}

		// Token: 0x0600194C RID: 6476 RVA: 0x00045A50 File Offset: 0x00043C50
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x4 operator >(int2x4 lhs, int2x4 rhs)
		{
			return new bool2x4(lhs.c0 > rhs.c0, lhs.c1 > rhs.c1, lhs.c2 > rhs.c2, lhs.c3 > rhs.c3);
		}

		// Token: 0x0600194D RID: 6477 RVA: 0x00045AA6 File Offset: 0x00043CA6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x4 operator >(int2x4 lhs, int rhs)
		{
			return new bool2x4(lhs.c0 > rhs, lhs.c1 > rhs, lhs.c2 > rhs, lhs.c3 > rhs);
		}

		// Token: 0x0600194E RID: 6478 RVA: 0x00045ADD File Offset: 0x00043CDD
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x4 operator >(int lhs, int2x4 rhs)
		{
			return new bool2x4(lhs > rhs.c0, lhs > rhs.c1, lhs > rhs.c2, lhs > rhs.c3);
		}

		// Token: 0x0600194F RID: 6479 RVA: 0x00045B14 File Offset: 0x00043D14
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x4 operator >=(int2x4 lhs, int2x4 rhs)
		{
			return new bool2x4(lhs.c0 >= rhs.c0, lhs.c1 >= rhs.c1, lhs.c2 >= rhs.c2, lhs.c3 >= rhs.c3);
		}

		// Token: 0x06001950 RID: 6480 RVA: 0x00045B6A File Offset: 0x00043D6A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x4 operator >=(int2x4 lhs, int rhs)
		{
			return new bool2x4(lhs.c0 >= rhs, lhs.c1 >= rhs, lhs.c2 >= rhs, lhs.c3 >= rhs);
		}

		// Token: 0x06001951 RID: 6481 RVA: 0x00045BA1 File Offset: 0x00043DA1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x4 operator >=(int lhs, int2x4 rhs)
		{
			return new bool2x4(lhs >= rhs.c0, lhs >= rhs.c1, lhs >= rhs.c2, lhs >= rhs.c3);
		}

		// Token: 0x06001952 RID: 6482 RVA: 0x00045BD8 File Offset: 0x00043DD8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2x4 operator -(int2x4 val)
		{
			return new int2x4(-val.c0, -val.c1, -val.c2, -val.c3);
		}

		// Token: 0x06001953 RID: 6483 RVA: 0x00045C0B File Offset: 0x00043E0B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2x4 operator +(int2x4 val)
		{
			return new int2x4(+val.c0, +val.c1, +val.c2, +val.c3);
		}

		// Token: 0x06001954 RID: 6484 RVA: 0x00045C3E File Offset: 0x00043E3E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2x4 operator <<(int2x4 x, int n)
		{
			return new int2x4(x.c0 << n, x.c1 << n, x.c2 << n, x.c3 << n);
		}

		// Token: 0x06001955 RID: 6485 RVA: 0x00045C75 File Offset: 0x00043E75
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2x4 operator >>(int2x4 x, int n)
		{
			return new int2x4(x.c0 >> n, x.c1 >> n, x.c2 >> n, x.c3 >> n);
		}

		// Token: 0x06001956 RID: 6486 RVA: 0x00045CAC File Offset: 0x00043EAC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x4 operator ==(int2x4 lhs, int2x4 rhs)
		{
			return new bool2x4(lhs.c0 == rhs.c0, lhs.c1 == rhs.c1, lhs.c2 == rhs.c2, lhs.c3 == rhs.c3);
		}

		// Token: 0x06001957 RID: 6487 RVA: 0x00045D02 File Offset: 0x00043F02
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x4 operator ==(int2x4 lhs, int rhs)
		{
			return new bool2x4(lhs.c0 == rhs, lhs.c1 == rhs, lhs.c2 == rhs, lhs.c3 == rhs);
		}

		// Token: 0x06001958 RID: 6488 RVA: 0x00045D39 File Offset: 0x00043F39
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x4 operator ==(int lhs, int2x4 rhs)
		{
			return new bool2x4(lhs == rhs.c0, lhs == rhs.c1, lhs == rhs.c2, lhs == rhs.c3);
		}

		// Token: 0x06001959 RID: 6489 RVA: 0x00045D70 File Offset: 0x00043F70
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x4 operator !=(int2x4 lhs, int2x4 rhs)
		{
			return new bool2x4(lhs.c0 != rhs.c0, lhs.c1 != rhs.c1, lhs.c2 != rhs.c2, lhs.c3 != rhs.c3);
		}

		// Token: 0x0600195A RID: 6490 RVA: 0x00045DC6 File Offset: 0x00043FC6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x4 operator !=(int2x4 lhs, int rhs)
		{
			return new bool2x4(lhs.c0 != rhs, lhs.c1 != rhs, lhs.c2 != rhs, lhs.c3 != rhs);
		}

		// Token: 0x0600195B RID: 6491 RVA: 0x00045DFD File Offset: 0x00043FFD
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x4 operator !=(int lhs, int2x4 rhs)
		{
			return new bool2x4(lhs != rhs.c0, lhs != rhs.c1, lhs != rhs.c2, lhs != rhs.c3);
		}

		// Token: 0x0600195C RID: 6492 RVA: 0x00045E34 File Offset: 0x00044034
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2x4 operator ~(int2x4 val)
		{
			return new int2x4(~val.c0, ~val.c1, ~val.c2, ~val.c3);
		}

		// Token: 0x0600195D RID: 6493 RVA: 0x00045E68 File Offset: 0x00044068
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2x4 operator &(int2x4 lhs, int2x4 rhs)
		{
			return new int2x4(lhs.c0 & rhs.c0, lhs.c1 & rhs.c1, lhs.c2 & rhs.c2, lhs.c3 & rhs.c3);
		}

		// Token: 0x0600195E RID: 6494 RVA: 0x00045EBE File Offset: 0x000440BE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2x4 operator &(int2x4 lhs, int rhs)
		{
			return new int2x4(lhs.c0 & rhs, lhs.c1 & rhs, lhs.c2 & rhs, lhs.c3 & rhs);
		}

		// Token: 0x0600195F RID: 6495 RVA: 0x00045EF5 File Offset: 0x000440F5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2x4 operator &(int lhs, int2x4 rhs)
		{
			return new int2x4(lhs & rhs.c0, lhs & rhs.c1, lhs & rhs.c2, lhs & rhs.c3);
		}

		// Token: 0x06001960 RID: 6496 RVA: 0x00045F2C File Offset: 0x0004412C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2x4 operator |(int2x4 lhs, int2x4 rhs)
		{
			return new int2x4(lhs.c0 | rhs.c0, lhs.c1 | rhs.c1, lhs.c2 | rhs.c2, lhs.c3 | rhs.c3);
		}

		// Token: 0x06001961 RID: 6497 RVA: 0x00045F82 File Offset: 0x00044182
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2x4 operator |(int2x4 lhs, int rhs)
		{
			return new int2x4(lhs.c0 | rhs, lhs.c1 | rhs, lhs.c2 | rhs, lhs.c3 | rhs);
		}

		// Token: 0x06001962 RID: 6498 RVA: 0x00045FB9 File Offset: 0x000441B9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2x4 operator |(int lhs, int2x4 rhs)
		{
			return new int2x4(lhs | rhs.c0, lhs | rhs.c1, lhs | rhs.c2, lhs | rhs.c3);
		}

		// Token: 0x06001963 RID: 6499 RVA: 0x00045FF0 File Offset: 0x000441F0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2x4 operator ^(int2x4 lhs, int2x4 rhs)
		{
			return new int2x4(lhs.c0 ^ rhs.c0, lhs.c1 ^ rhs.c1, lhs.c2 ^ rhs.c2, lhs.c3 ^ rhs.c3);
		}

		// Token: 0x06001964 RID: 6500 RVA: 0x00046046 File Offset: 0x00044246
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2x4 operator ^(int2x4 lhs, int rhs)
		{
			return new int2x4(lhs.c0 ^ rhs, lhs.c1 ^ rhs, lhs.c2 ^ rhs, lhs.c3 ^ rhs);
		}

		// Token: 0x06001965 RID: 6501 RVA: 0x0004607D File Offset: 0x0004427D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2x4 operator ^(int lhs, int2x4 rhs)
		{
			return new int2x4(lhs ^ rhs.c0, lhs ^ rhs.c1, lhs ^ rhs.c2, lhs ^ rhs.c3);
		}

		// Token: 0x170007CF RID: 1999
		public unsafe int2 this[int index]
		{
			get
			{
				fixed (int2x4* ptr = &this)
				{
					return ref *(int2*)(ptr + (IntPtr)index * (IntPtr)sizeof(int2) / (IntPtr)sizeof(int2x4));
				}
			}
		}

		// Token: 0x06001967 RID: 6503 RVA: 0x000460D0 File Offset: 0x000442D0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool Equals(int2x4 rhs)
		{
			return this.c0.Equals(rhs.c0) && this.c1.Equals(rhs.c1) && this.c2.Equals(rhs.c2) && this.c3.Equals(rhs.c3);
		}

		// Token: 0x06001968 RID: 6504 RVA: 0x0004612C File Offset: 0x0004432C
		public override bool Equals(object o)
		{
			if (o is int2x4)
			{
				int2x4 rhs = (int2x4)o;
				return this.Equals(rhs);
			}
			return false;
		}

		// Token: 0x06001969 RID: 6505 RVA: 0x00046151 File Offset: 0x00044351
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override int GetHashCode()
		{
			return (int)math.hash(this);
		}

		// Token: 0x0600196A RID: 6506 RVA: 0x00046160 File Offset: 0x00044360
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override string ToString()
		{
			return string.Format("int2x4({0}, {1}, {2}, {3},  {4}, {5}, {6}, {7})", new object[]
			{
				this.c0.x,
				this.c1.x,
				this.c2.x,
				this.c3.x,
				this.c0.y,
				this.c1.y,
				this.c2.y,
				this.c3.y
			});
		}

		// Token: 0x0600196B RID: 6507 RVA: 0x00046218 File Offset: 0x00044418
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public string ToString(string format, IFormatProvider formatProvider)
		{
			return string.Format("int2x4({0}, {1}, {2}, {3},  {4}, {5}, {6}, {7})", new object[]
			{
				this.c0.x.ToString(format, formatProvider),
				this.c1.x.ToString(format, formatProvider),
				this.c2.x.ToString(format, formatProvider),
				this.c3.x.ToString(format, formatProvider),
				this.c0.y.ToString(format, formatProvider),
				this.c1.y.ToString(format, formatProvider),
				this.c2.y.ToString(format, formatProvider),
				this.c3.y.ToString(format, formatProvider)
			});
		}

		// Token: 0x040000BA RID: 186
		public int2 c0;

		// Token: 0x040000BB RID: 187
		public int2 c1;

		// Token: 0x040000BC RID: 188
		public int2 c2;

		// Token: 0x040000BD RID: 189
		public int2 c3;

		// Token: 0x040000BE RID: 190
		public static readonly int2x4 zero;
	}
}

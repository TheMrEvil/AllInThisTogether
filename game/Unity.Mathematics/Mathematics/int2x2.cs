using System;
using System.Runtime.CompilerServices;
using Unity.IL2CPP.CompilerServices;

namespace Unity.Mathematics
{
	// Token: 0x0200002D RID: 45
	[Il2CppEagerStaticClassConstruction]
	[Serializable]
	public struct int2x2 : IEquatable<int2x2>, IFormattable
	{
		// Token: 0x0600188A RID: 6282 RVA: 0x0004391A File Offset: 0x00041B1A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int2x2(int2 c0, int2 c1)
		{
			this.c0 = c0;
			this.c1 = c1;
		}

		// Token: 0x0600188B RID: 6283 RVA: 0x0004392A File Offset: 0x00041B2A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int2x2(int m00, int m01, int m10, int m11)
		{
			this.c0 = new int2(m00, m10);
			this.c1 = new int2(m01, m11);
		}

		// Token: 0x0600188C RID: 6284 RVA: 0x00043947 File Offset: 0x00041B47
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int2x2(int v)
		{
			this.c0 = v;
			this.c1 = v;
		}

		// Token: 0x0600188D RID: 6285 RVA: 0x00043961 File Offset: 0x00041B61
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int2x2(bool v)
		{
			this.c0 = math.select(new int2(0), new int2(1), v);
			this.c1 = math.select(new int2(0), new int2(1), v);
		}

		// Token: 0x0600188E RID: 6286 RVA: 0x00043993 File Offset: 0x00041B93
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int2x2(bool2x2 v)
		{
			this.c0 = math.select(new int2(0), new int2(1), v.c0);
			this.c1 = math.select(new int2(0), new int2(1), v.c1);
		}

		// Token: 0x0600188F RID: 6287 RVA: 0x000439CF File Offset: 0x00041BCF
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int2x2(uint v)
		{
			this.c0 = (int2)v;
			this.c1 = (int2)v;
		}

		// Token: 0x06001890 RID: 6288 RVA: 0x000439E9 File Offset: 0x00041BE9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int2x2(uint2x2 v)
		{
			this.c0 = (int2)v.c0;
			this.c1 = (int2)v.c1;
		}

		// Token: 0x06001891 RID: 6289 RVA: 0x00043A0D File Offset: 0x00041C0D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int2x2(float v)
		{
			this.c0 = (int2)v;
			this.c1 = (int2)v;
		}

		// Token: 0x06001892 RID: 6290 RVA: 0x00043A27 File Offset: 0x00041C27
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int2x2(float2x2 v)
		{
			this.c0 = (int2)v.c0;
			this.c1 = (int2)v.c1;
		}

		// Token: 0x06001893 RID: 6291 RVA: 0x00043A4B File Offset: 0x00041C4B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int2x2(double v)
		{
			this.c0 = (int2)v;
			this.c1 = (int2)v;
		}

		// Token: 0x06001894 RID: 6292 RVA: 0x00043A65 File Offset: 0x00041C65
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int2x2(double2x2 v)
		{
			this.c0 = (int2)v.c0;
			this.c1 = (int2)v.c1;
		}

		// Token: 0x06001895 RID: 6293 RVA: 0x00043A89 File Offset: 0x00041C89
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator int2x2(int v)
		{
			return new int2x2(v);
		}

		// Token: 0x06001896 RID: 6294 RVA: 0x00043A91 File Offset: 0x00041C91
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator int2x2(bool v)
		{
			return new int2x2(v);
		}

		// Token: 0x06001897 RID: 6295 RVA: 0x00043A99 File Offset: 0x00041C99
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator int2x2(bool2x2 v)
		{
			return new int2x2(v);
		}

		// Token: 0x06001898 RID: 6296 RVA: 0x00043AA1 File Offset: 0x00041CA1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator int2x2(uint v)
		{
			return new int2x2(v);
		}

		// Token: 0x06001899 RID: 6297 RVA: 0x00043AA9 File Offset: 0x00041CA9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator int2x2(uint2x2 v)
		{
			return new int2x2(v);
		}

		// Token: 0x0600189A RID: 6298 RVA: 0x00043AB1 File Offset: 0x00041CB1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator int2x2(float v)
		{
			return new int2x2(v);
		}

		// Token: 0x0600189B RID: 6299 RVA: 0x00043AB9 File Offset: 0x00041CB9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator int2x2(float2x2 v)
		{
			return new int2x2(v);
		}

		// Token: 0x0600189C RID: 6300 RVA: 0x00043AC1 File Offset: 0x00041CC1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator int2x2(double v)
		{
			return new int2x2(v);
		}

		// Token: 0x0600189D RID: 6301 RVA: 0x00043AC9 File Offset: 0x00041CC9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator int2x2(double2x2 v)
		{
			return new int2x2(v);
		}

		// Token: 0x0600189E RID: 6302 RVA: 0x00043AD1 File Offset: 0x00041CD1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2x2 operator *(int2x2 lhs, int2x2 rhs)
		{
			return new int2x2(lhs.c0 * rhs.c0, lhs.c1 * rhs.c1);
		}

		// Token: 0x0600189F RID: 6303 RVA: 0x00043AFA File Offset: 0x00041CFA
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2x2 operator *(int2x2 lhs, int rhs)
		{
			return new int2x2(lhs.c0 * rhs, lhs.c1 * rhs);
		}

		// Token: 0x060018A0 RID: 6304 RVA: 0x00043B19 File Offset: 0x00041D19
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2x2 operator *(int lhs, int2x2 rhs)
		{
			return new int2x2(lhs * rhs.c0, lhs * rhs.c1);
		}

		// Token: 0x060018A1 RID: 6305 RVA: 0x00043B38 File Offset: 0x00041D38
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2x2 operator +(int2x2 lhs, int2x2 rhs)
		{
			return new int2x2(lhs.c0 + rhs.c0, lhs.c1 + rhs.c1);
		}

		// Token: 0x060018A2 RID: 6306 RVA: 0x00043B61 File Offset: 0x00041D61
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2x2 operator +(int2x2 lhs, int rhs)
		{
			return new int2x2(lhs.c0 + rhs, lhs.c1 + rhs);
		}

		// Token: 0x060018A3 RID: 6307 RVA: 0x00043B80 File Offset: 0x00041D80
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2x2 operator +(int lhs, int2x2 rhs)
		{
			return new int2x2(lhs + rhs.c0, lhs + rhs.c1);
		}

		// Token: 0x060018A4 RID: 6308 RVA: 0x00043B9F File Offset: 0x00041D9F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2x2 operator -(int2x2 lhs, int2x2 rhs)
		{
			return new int2x2(lhs.c0 - rhs.c0, lhs.c1 - rhs.c1);
		}

		// Token: 0x060018A5 RID: 6309 RVA: 0x00043BC8 File Offset: 0x00041DC8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2x2 operator -(int2x2 lhs, int rhs)
		{
			return new int2x2(lhs.c0 - rhs, lhs.c1 - rhs);
		}

		// Token: 0x060018A6 RID: 6310 RVA: 0x00043BE7 File Offset: 0x00041DE7
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2x2 operator -(int lhs, int2x2 rhs)
		{
			return new int2x2(lhs - rhs.c0, lhs - rhs.c1);
		}

		// Token: 0x060018A7 RID: 6311 RVA: 0x00043C06 File Offset: 0x00041E06
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2x2 operator /(int2x2 lhs, int2x2 rhs)
		{
			return new int2x2(lhs.c0 / rhs.c0, lhs.c1 / rhs.c1);
		}

		// Token: 0x060018A8 RID: 6312 RVA: 0x00043C2F File Offset: 0x00041E2F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2x2 operator /(int2x2 lhs, int rhs)
		{
			return new int2x2(lhs.c0 / rhs, lhs.c1 / rhs);
		}

		// Token: 0x060018A9 RID: 6313 RVA: 0x00043C4E File Offset: 0x00041E4E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2x2 operator /(int lhs, int2x2 rhs)
		{
			return new int2x2(lhs / rhs.c0, lhs / rhs.c1);
		}

		// Token: 0x060018AA RID: 6314 RVA: 0x00043C6D File Offset: 0x00041E6D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2x2 operator %(int2x2 lhs, int2x2 rhs)
		{
			return new int2x2(lhs.c0 % rhs.c0, lhs.c1 % rhs.c1);
		}

		// Token: 0x060018AB RID: 6315 RVA: 0x00043C96 File Offset: 0x00041E96
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2x2 operator %(int2x2 lhs, int rhs)
		{
			return new int2x2(lhs.c0 % rhs, lhs.c1 % rhs);
		}

		// Token: 0x060018AC RID: 6316 RVA: 0x00043CB5 File Offset: 0x00041EB5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2x2 operator %(int lhs, int2x2 rhs)
		{
			return new int2x2(lhs % rhs.c0, lhs % rhs.c1);
		}

		// Token: 0x060018AD RID: 6317 RVA: 0x00043CD4 File Offset: 0x00041ED4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2x2 operator ++(int2x2 val)
		{
			int2 @int = ++val.c0;
			val.c0 = @int;
			int2 int2 = @int;
			@int = ++val.c1;
			val.c1 = @int;
			return new int2x2(int2, @int);
		}

		// Token: 0x060018AE RID: 6318 RVA: 0x00043D1C File Offset: 0x00041F1C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2x2 operator --(int2x2 val)
		{
			int2 @int = --val.c0;
			val.c0 = @int;
			int2 int2 = @int;
			@int = --val.c1;
			val.c1 = @int;
			return new int2x2(int2, @int);
		}

		// Token: 0x060018AF RID: 6319 RVA: 0x00043D62 File Offset: 0x00041F62
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x2 operator <(int2x2 lhs, int2x2 rhs)
		{
			return new bool2x2(lhs.c0 < rhs.c0, lhs.c1 < rhs.c1);
		}

		// Token: 0x060018B0 RID: 6320 RVA: 0x00043D8B File Offset: 0x00041F8B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x2 operator <(int2x2 lhs, int rhs)
		{
			return new bool2x2(lhs.c0 < rhs, lhs.c1 < rhs);
		}

		// Token: 0x060018B1 RID: 6321 RVA: 0x00043DAA File Offset: 0x00041FAA
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x2 operator <(int lhs, int2x2 rhs)
		{
			return new bool2x2(lhs < rhs.c0, lhs < rhs.c1);
		}

		// Token: 0x060018B2 RID: 6322 RVA: 0x00043DC9 File Offset: 0x00041FC9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x2 operator <=(int2x2 lhs, int2x2 rhs)
		{
			return new bool2x2(lhs.c0 <= rhs.c0, lhs.c1 <= rhs.c1);
		}

		// Token: 0x060018B3 RID: 6323 RVA: 0x00043DF2 File Offset: 0x00041FF2
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x2 operator <=(int2x2 lhs, int rhs)
		{
			return new bool2x2(lhs.c0 <= rhs, lhs.c1 <= rhs);
		}

		// Token: 0x060018B4 RID: 6324 RVA: 0x00043E11 File Offset: 0x00042011
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x2 operator <=(int lhs, int2x2 rhs)
		{
			return new bool2x2(lhs <= rhs.c0, lhs <= rhs.c1);
		}

		// Token: 0x060018B5 RID: 6325 RVA: 0x00043E30 File Offset: 0x00042030
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x2 operator >(int2x2 lhs, int2x2 rhs)
		{
			return new bool2x2(lhs.c0 > rhs.c0, lhs.c1 > rhs.c1);
		}

		// Token: 0x060018B6 RID: 6326 RVA: 0x00043E59 File Offset: 0x00042059
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x2 operator >(int2x2 lhs, int rhs)
		{
			return new bool2x2(lhs.c0 > rhs, lhs.c1 > rhs);
		}

		// Token: 0x060018B7 RID: 6327 RVA: 0x00043E78 File Offset: 0x00042078
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x2 operator >(int lhs, int2x2 rhs)
		{
			return new bool2x2(lhs > rhs.c0, lhs > rhs.c1);
		}

		// Token: 0x060018B8 RID: 6328 RVA: 0x00043E97 File Offset: 0x00042097
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x2 operator >=(int2x2 lhs, int2x2 rhs)
		{
			return new bool2x2(lhs.c0 >= rhs.c0, lhs.c1 >= rhs.c1);
		}

		// Token: 0x060018B9 RID: 6329 RVA: 0x00043EC0 File Offset: 0x000420C0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x2 operator >=(int2x2 lhs, int rhs)
		{
			return new bool2x2(lhs.c0 >= rhs, lhs.c1 >= rhs);
		}

		// Token: 0x060018BA RID: 6330 RVA: 0x00043EDF File Offset: 0x000420DF
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x2 operator >=(int lhs, int2x2 rhs)
		{
			return new bool2x2(lhs >= rhs.c0, lhs >= rhs.c1);
		}

		// Token: 0x060018BB RID: 6331 RVA: 0x00043EFE File Offset: 0x000420FE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2x2 operator -(int2x2 val)
		{
			return new int2x2(-val.c0, -val.c1);
		}

		// Token: 0x060018BC RID: 6332 RVA: 0x00043F1B File Offset: 0x0004211B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2x2 operator +(int2x2 val)
		{
			return new int2x2(+val.c0, +val.c1);
		}

		// Token: 0x060018BD RID: 6333 RVA: 0x00043F38 File Offset: 0x00042138
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2x2 operator <<(int2x2 x, int n)
		{
			return new int2x2(x.c0 << n, x.c1 << n);
		}

		// Token: 0x060018BE RID: 6334 RVA: 0x00043F57 File Offset: 0x00042157
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2x2 operator >>(int2x2 x, int n)
		{
			return new int2x2(x.c0 >> n, x.c1 >> n);
		}

		// Token: 0x060018BF RID: 6335 RVA: 0x00043F76 File Offset: 0x00042176
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x2 operator ==(int2x2 lhs, int2x2 rhs)
		{
			return new bool2x2(lhs.c0 == rhs.c0, lhs.c1 == rhs.c1);
		}

		// Token: 0x060018C0 RID: 6336 RVA: 0x00043F9F File Offset: 0x0004219F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x2 operator ==(int2x2 lhs, int rhs)
		{
			return new bool2x2(lhs.c0 == rhs, lhs.c1 == rhs);
		}

		// Token: 0x060018C1 RID: 6337 RVA: 0x00043FBE File Offset: 0x000421BE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x2 operator ==(int lhs, int2x2 rhs)
		{
			return new bool2x2(lhs == rhs.c0, lhs == rhs.c1);
		}

		// Token: 0x060018C2 RID: 6338 RVA: 0x00043FDD File Offset: 0x000421DD
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x2 operator !=(int2x2 lhs, int2x2 rhs)
		{
			return new bool2x2(lhs.c0 != rhs.c0, lhs.c1 != rhs.c1);
		}

		// Token: 0x060018C3 RID: 6339 RVA: 0x00044006 File Offset: 0x00042206
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x2 operator !=(int2x2 lhs, int rhs)
		{
			return new bool2x2(lhs.c0 != rhs, lhs.c1 != rhs);
		}

		// Token: 0x060018C4 RID: 6340 RVA: 0x00044025 File Offset: 0x00042225
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x2 operator !=(int lhs, int2x2 rhs)
		{
			return new bool2x2(lhs != rhs.c0, lhs != rhs.c1);
		}

		// Token: 0x060018C5 RID: 6341 RVA: 0x00044044 File Offset: 0x00042244
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2x2 operator ~(int2x2 val)
		{
			return new int2x2(~val.c0, ~val.c1);
		}

		// Token: 0x060018C6 RID: 6342 RVA: 0x00044061 File Offset: 0x00042261
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2x2 operator &(int2x2 lhs, int2x2 rhs)
		{
			return new int2x2(lhs.c0 & rhs.c0, lhs.c1 & rhs.c1);
		}

		// Token: 0x060018C7 RID: 6343 RVA: 0x0004408A File Offset: 0x0004228A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2x2 operator &(int2x2 lhs, int rhs)
		{
			return new int2x2(lhs.c0 & rhs, lhs.c1 & rhs);
		}

		// Token: 0x060018C8 RID: 6344 RVA: 0x000440A9 File Offset: 0x000422A9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2x2 operator &(int lhs, int2x2 rhs)
		{
			return new int2x2(lhs & rhs.c0, lhs & rhs.c1);
		}

		// Token: 0x060018C9 RID: 6345 RVA: 0x000440C8 File Offset: 0x000422C8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2x2 operator |(int2x2 lhs, int2x2 rhs)
		{
			return new int2x2(lhs.c0 | rhs.c0, lhs.c1 | rhs.c1);
		}

		// Token: 0x060018CA RID: 6346 RVA: 0x000440F1 File Offset: 0x000422F1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2x2 operator |(int2x2 lhs, int rhs)
		{
			return new int2x2(lhs.c0 | rhs, lhs.c1 | rhs);
		}

		// Token: 0x060018CB RID: 6347 RVA: 0x00044110 File Offset: 0x00042310
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2x2 operator |(int lhs, int2x2 rhs)
		{
			return new int2x2(lhs | rhs.c0, lhs | rhs.c1);
		}

		// Token: 0x060018CC RID: 6348 RVA: 0x0004412F File Offset: 0x0004232F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2x2 operator ^(int2x2 lhs, int2x2 rhs)
		{
			return new int2x2(lhs.c0 ^ rhs.c0, lhs.c1 ^ rhs.c1);
		}

		// Token: 0x060018CD RID: 6349 RVA: 0x00044158 File Offset: 0x00042358
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2x2 operator ^(int2x2 lhs, int rhs)
		{
			return new int2x2(lhs.c0 ^ rhs, lhs.c1 ^ rhs);
		}

		// Token: 0x060018CE RID: 6350 RVA: 0x00044177 File Offset: 0x00042377
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2x2 operator ^(int lhs, int2x2 rhs)
		{
			return new int2x2(lhs ^ rhs.c0, lhs ^ rhs.c1);
		}

		// Token: 0x170007CD RID: 1997
		public unsafe int2 this[int index]
		{
			get
			{
				fixed (int2x2* ptr = &this)
				{
					return ref *(int2*)(ptr + (IntPtr)index * (IntPtr)sizeof(int2) / (IntPtr)sizeof(int2x2));
				}
			}
		}

		// Token: 0x060018D0 RID: 6352 RVA: 0x000441B3 File Offset: 0x000423B3
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool Equals(int2x2 rhs)
		{
			return this.c0.Equals(rhs.c0) && this.c1.Equals(rhs.c1);
		}

		// Token: 0x060018D1 RID: 6353 RVA: 0x000441DC File Offset: 0x000423DC
		public override bool Equals(object o)
		{
			if (o is int2x2)
			{
				int2x2 rhs = (int2x2)o;
				return this.Equals(rhs);
			}
			return false;
		}

		// Token: 0x060018D2 RID: 6354 RVA: 0x00044201 File Offset: 0x00042401
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override int GetHashCode()
		{
			return (int)math.hash(this);
		}

		// Token: 0x060018D3 RID: 6355 RVA: 0x00044210 File Offset: 0x00042410
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override string ToString()
		{
			return string.Format("int2x2({0}, {1},  {2}, {3})", new object[]
			{
				this.c0.x,
				this.c1.x,
				this.c0.y,
				this.c1.y
			});
		}

		// Token: 0x060018D4 RID: 6356 RVA: 0x0004427C File Offset: 0x0004247C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public string ToString(string format, IFormatProvider formatProvider)
		{
			return string.Format("int2x2({0}, {1},  {2}, {3})", new object[]
			{
				this.c0.x.ToString(format, formatProvider),
				this.c1.x.ToString(format, formatProvider),
				this.c0.y.ToString(format, formatProvider),
				this.c1.y.ToString(format, formatProvider)
			});
		}

		// Token: 0x060018D5 RID: 6357 RVA: 0x000442ED File Offset: 0x000424ED
		// Note: this type is marked as 'beforefieldinit'.
		static int2x2()
		{
		}

		// Token: 0x040000B2 RID: 178
		public int2 c0;

		// Token: 0x040000B3 RID: 179
		public int2 c1;

		// Token: 0x040000B4 RID: 180
		public static readonly int2x2 identity = new int2x2(1, 0, 0, 1);

		// Token: 0x040000B5 RID: 181
		public static readonly int2x2 zero;
	}
}

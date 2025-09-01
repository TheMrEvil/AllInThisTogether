using System;
using System.Runtime.CompilerServices;
using Unity.IL2CPP.CompilerServices;

namespace Unity.Mathematics
{
	// Token: 0x02000035 RID: 53
	[Il2CppEagerStaticClassConstruction]
	[Serializable]
	public struct int4x2 : IEquatable<int4x2>, IFormattable
	{
		// Token: 0x06001CFB RID: 7419 RVA: 0x0004E5BD File Offset: 0x0004C7BD
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int4x2(int4 c0, int4 c1)
		{
			this.c0 = c0;
			this.c1 = c1;
		}

		// Token: 0x06001CFC RID: 7420 RVA: 0x0004E5CD File Offset: 0x0004C7CD
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int4x2(int m00, int m01, int m10, int m11, int m20, int m21, int m30, int m31)
		{
			this.c0 = new int4(m00, m10, m20, m30);
			this.c1 = new int4(m01, m11, m21, m31);
		}

		// Token: 0x06001CFD RID: 7421 RVA: 0x0004E5F2 File Offset: 0x0004C7F2
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int4x2(int v)
		{
			this.c0 = v;
			this.c1 = v;
		}

		// Token: 0x06001CFE RID: 7422 RVA: 0x0004E60C File Offset: 0x0004C80C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int4x2(bool v)
		{
			this.c0 = math.select(new int4(0), new int4(1), v);
			this.c1 = math.select(new int4(0), new int4(1), v);
		}

		// Token: 0x06001CFF RID: 7423 RVA: 0x0004E63E File Offset: 0x0004C83E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int4x2(bool4x2 v)
		{
			this.c0 = math.select(new int4(0), new int4(1), v.c0);
			this.c1 = math.select(new int4(0), new int4(1), v.c1);
		}

		// Token: 0x06001D00 RID: 7424 RVA: 0x0004E67A File Offset: 0x0004C87A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int4x2(uint v)
		{
			this.c0 = (int4)v;
			this.c1 = (int4)v;
		}

		// Token: 0x06001D01 RID: 7425 RVA: 0x0004E694 File Offset: 0x0004C894
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int4x2(uint4x2 v)
		{
			this.c0 = (int4)v.c0;
			this.c1 = (int4)v.c1;
		}

		// Token: 0x06001D02 RID: 7426 RVA: 0x0004E6B8 File Offset: 0x0004C8B8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int4x2(float v)
		{
			this.c0 = (int4)v;
			this.c1 = (int4)v;
		}

		// Token: 0x06001D03 RID: 7427 RVA: 0x0004E6D2 File Offset: 0x0004C8D2
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int4x2(float4x2 v)
		{
			this.c0 = (int4)v.c0;
			this.c1 = (int4)v.c1;
		}

		// Token: 0x06001D04 RID: 7428 RVA: 0x0004E6F6 File Offset: 0x0004C8F6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int4x2(double v)
		{
			this.c0 = (int4)v;
			this.c1 = (int4)v;
		}

		// Token: 0x06001D05 RID: 7429 RVA: 0x0004E710 File Offset: 0x0004C910
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int4x2(double4x2 v)
		{
			this.c0 = (int4)v.c0;
			this.c1 = (int4)v.c1;
		}

		// Token: 0x06001D06 RID: 7430 RVA: 0x0004E734 File Offset: 0x0004C934
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator int4x2(int v)
		{
			return new int4x2(v);
		}

		// Token: 0x06001D07 RID: 7431 RVA: 0x0004E73C File Offset: 0x0004C93C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator int4x2(bool v)
		{
			return new int4x2(v);
		}

		// Token: 0x06001D08 RID: 7432 RVA: 0x0004E744 File Offset: 0x0004C944
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator int4x2(bool4x2 v)
		{
			return new int4x2(v);
		}

		// Token: 0x06001D09 RID: 7433 RVA: 0x0004E74C File Offset: 0x0004C94C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator int4x2(uint v)
		{
			return new int4x2(v);
		}

		// Token: 0x06001D0A RID: 7434 RVA: 0x0004E754 File Offset: 0x0004C954
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator int4x2(uint4x2 v)
		{
			return new int4x2(v);
		}

		// Token: 0x06001D0B RID: 7435 RVA: 0x0004E75C File Offset: 0x0004C95C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator int4x2(float v)
		{
			return new int4x2(v);
		}

		// Token: 0x06001D0C RID: 7436 RVA: 0x0004E764 File Offset: 0x0004C964
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator int4x2(float4x2 v)
		{
			return new int4x2(v);
		}

		// Token: 0x06001D0D RID: 7437 RVA: 0x0004E76C File Offset: 0x0004C96C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator int4x2(double v)
		{
			return new int4x2(v);
		}

		// Token: 0x06001D0E RID: 7438 RVA: 0x0004E774 File Offset: 0x0004C974
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator int4x2(double4x2 v)
		{
			return new int4x2(v);
		}

		// Token: 0x06001D0F RID: 7439 RVA: 0x0004E77C File Offset: 0x0004C97C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4x2 operator *(int4x2 lhs, int4x2 rhs)
		{
			return new int4x2(lhs.c0 * rhs.c0, lhs.c1 * rhs.c1);
		}

		// Token: 0x06001D10 RID: 7440 RVA: 0x0004E7A5 File Offset: 0x0004C9A5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4x2 operator *(int4x2 lhs, int rhs)
		{
			return new int4x2(lhs.c0 * rhs, lhs.c1 * rhs);
		}

		// Token: 0x06001D11 RID: 7441 RVA: 0x0004E7C4 File Offset: 0x0004C9C4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4x2 operator *(int lhs, int4x2 rhs)
		{
			return new int4x2(lhs * rhs.c0, lhs * rhs.c1);
		}

		// Token: 0x06001D12 RID: 7442 RVA: 0x0004E7E3 File Offset: 0x0004C9E3
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4x2 operator +(int4x2 lhs, int4x2 rhs)
		{
			return new int4x2(lhs.c0 + rhs.c0, lhs.c1 + rhs.c1);
		}

		// Token: 0x06001D13 RID: 7443 RVA: 0x0004E80C File Offset: 0x0004CA0C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4x2 operator +(int4x2 lhs, int rhs)
		{
			return new int4x2(lhs.c0 + rhs, lhs.c1 + rhs);
		}

		// Token: 0x06001D14 RID: 7444 RVA: 0x0004E82B File Offset: 0x0004CA2B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4x2 operator +(int lhs, int4x2 rhs)
		{
			return new int4x2(lhs + rhs.c0, lhs + rhs.c1);
		}

		// Token: 0x06001D15 RID: 7445 RVA: 0x0004E84A File Offset: 0x0004CA4A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4x2 operator -(int4x2 lhs, int4x2 rhs)
		{
			return new int4x2(lhs.c0 - rhs.c0, lhs.c1 - rhs.c1);
		}

		// Token: 0x06001D16 RID: 7446 RVA: 0x0004E873 File Offset: 0x0004CA73
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4x2 operator -(int4x2 lhs, int rhs)
		{
			return new int4x2(lhs.c0 - rhs, lhs.c1 - rhs);
		}

		// Token: 0x06001D17 RID: 7447 RVA: 0x0004E892 File Offset: 0x0004CA92
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4x2 operator -(int lhs, int4x2 rhs)
		{
			return new int4x2(lhs - rhs.c0, lhs - rhs.c1);
		}

		// Token: 0x06001D18 RID: 7448 RVA: 0x0004E8B1 File Offset: 0x0004CAB1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4x2 operator /(int4x2 lhs, int4x2 rhs)
		{
			return new int4x2(lhs.c0 / rhs.c0, lhs.c1 / rhs.c1);
		}

		// Token: 0x06001D19 RID: 7449 RVA: 0x0004E8DA File Offset: 0x0004CADA
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4x2 operator /(int4x2 lhs, int rhs)
		{
			return new int4x2(lhs.c0 / rhs, lhs.c1 / rhs);
		}

		// Token: 0x06001D1A RID: 7450 RVA: 0x0004E8F9 File Offset: 0x0004CAF9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4x2 operator /(int lhs, int4x2 rhs)
		{
			return new int4x2(lhs / rhs.c0, lhs / rhs.c1);
		}

		// Token: 0x06001D1B RID: 7451 RVA: 0x0004E918 File Offset: 0x0004CB18
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4x2 operator %(int4x2 lhs, int4x2 rhs)
		{
			return new int4x2(lhs.c0 % rhs.c0, lhs.c1 % rhs.c1);
		}

		// Token: 0x06001D1C RID: 7452 RVA: 0x0004E941 File Offset: 0x0004CB41
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4x2 operator %(int4x2 lhs, int rhs)
		{
			return new int4x2(lhs.c0 % rhs, lhs.c1 % rhs);
		}

		// Token: 0x06001D1D RID: 7453 RVA: 0x0004E960 File Offset: 0x0004CB60
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4x2 operator %(int lhs, int4x2 rhs)
		{
			return new int4x2(lhs % rhs.c0, lhs % rhs.c1);
		}

		// Token: 0x06001D1E RID: 7454 RVA: 0x0004E980 File Offset: 0x0004CB80
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4x2 operator ++(int4x2 val)
		{
			int4 @int = ++val.c0;
			val.c0 = @int;
			int4 int2 = @int;
			@int = ++val.c1;
			val.c1 = @int;
			return new int4x2(int2, @int);
		}

		// Token: 0x06001D1F RID: 7455 RVA: 0x0004E9C8 File Offset: 0x0004CBC8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4x2 operator --(int4x2 val)
		{
			int4 @int = --val.c0;
			val.c0 = @int;
			int4 int2 = @int;
			@int = --val.c1;
			val.c1 = @int;
			return new int4x2(int2, @int);
		}

		// Token: 0x06001D20 RID: 7456 RVA: 0x0004EA0E File Offset: 0x0004CC0E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x2 operator <(int4x2 lhs, int4x2 rhs)
		{
			return new bool4x2(lhs.c0 < rhs.c0, lhs.c1 < rhs.c1);
		}

		// Token: 0x06001D21 RID: 7457 RVA: 0x0004EA37 File Offset: 0x0004CC37
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x2 operator <(int4x2 lhs, int rhs)
		{
			return new bool4x2(lhs.c0 < rhs, lhs.c1 < rhs);
		}

		// Token: 0x06001D22 RID: 7458 RVA: 0x0004EA56 File Offset: 0x0004CC56
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x2 operator <(int lhs, int4x2 rhs)
		{
			return new bool4x2(lhs < rhs.c0, lhs < rhs.c1);
		}

		// Token: 0x06001D23 RID: 7459 RVA: 0x0004EA75 File Offset: 0x0004CC75
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x2 operator <=(int4x2 lhs, int4x2 rhs)
		{
			return new bool4x2(lhs.c0 <= rhs.c0, lhs.c1 <= rhs.c1);
		}

		// Token: 0x06001D24 RID: 7460 RVA: 0x0004EA9E File Offset: 0x0004CC9E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x2 operator <=(int4x2 lhs, int rhs)
		{
			return new bool4x2(lhs.c0 <= rhs, lhs.c1 <= rhs);
		}

		// Token: 0x06001D25 RID: 7461 RVA: 0x0004EABD File Offset: 0x0004CCBD
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x2 operator <=(int lhs, int4x2 rhs)
		{
			return new bool4x2(lhs <= rhs.c0, lhs <= rhs.c1);
		}

		// Token: 0x06001D26 RID: 7462 RVA: 0x0004EADC File Offset: 0x0004CCDC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x2 operator >(int4x2 lhs, int4x2 rhs)
		{
			return new bool4x2(lhs.c0 > rhs.c0, lhs.c1 > rhs.c1);
		}

		// Token: 0x06001D27 RID: 7463 RVA: 0x0004EB05 File Offset: 0x0004CD05
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x2 operator >(int4x2 lhs, int rhs)
		{
			return new bool4x2(lhs.c0 > rhs, lhs.c1 > rhs);
		}

		// Token: 0x06001D28 RID: 7464 RVA: 0x0004EB24 File Offset: 0x0004CD24
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x2 operator >(int lhs, int4x2 rhs)
		{
			return new bool4x2(lhs > rhs.c0, lhs > rhs.c1);
		}

		// Token: 0x06001D29 RID: 7465 RVA: 0x0004EB43 File Offset: 0x0004CD43
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x2 operator >=(int4x2 lhs, int4x2 rhs)
		{
			return new bool4x2(lhs.c0 >= rhs.c0, lhs.c1 >= rhs.c1);
		}

		// Token: 0x06001D2A RID: 7466 RVA: 0x0004EB6C File Offset: 0x0004CD6C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x2 operator >=(int4x2 lhs, int rhs)
		{
			return new bool4x2(lhs.c0 >= rhs, lhs.c1 >= rhs);
		}

		// Token: 0x06001D2B RID: 7467 RVA: 0x0004EB8B File Offset: 0x0004CD8B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x2 operator >=(int lhs, int4x2 rhs)
		{
			return new bool4x2(lhs >= rhs.c0, lhs >= rhs.c1);
		}

		// Token: 0x06001D2C RID: 7468 RVA: 0x0004EBAA File Offset: 0x0004CDAA
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4x2 operator -(int4x2 val)
		{
			return new int4x2(-val.c0, -val.c1);
		}

		// Token: 0x06001D2D RID: 7469 RVA: 0x0004EBC7 File Offset: 0x0004CDC7
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4x2 operator +(int4x2 val)
		{
			return new int4x2(+val.c0, +val.c1);
		}

		// Token: 0x06001D2E RID: 7470 RVA: 0x0004EBE4 File Offset: 0x0004CDE4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4x2 operator <<(int4x2 x, int n)
		{
			return new int4x2(x.c0 << n, x.c1 << n);
		}

		// Token: 0x06001D2F RID: 7471 RVA: 0x0004EC03 File Offset: 0x0004CE03
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4x2 operator >>(int4x2 x, int n)
		{
			return new int4x2(x.c0 >> n, x.c1 >> n);
		}

		// Token: 0x06001D30 RID: 7472 RVA: 0x0004EC22 File Offset: 0x0004CE22
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x2 operator ==(int4x2 lhs, int4x2 rhs)
		{
			return new bool4x2(lhs.c0 == rhs.c0, lhs.c1 == rhs.c1);
		}

		// Token: 0x06001D31 RID: 7473 RVA: 0x0004EC4B File Offset: 0x0004CE4B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x2 operator ==(int4x2 lhs, int rhs)
		{
			return new bool4x2(lhs.c0 == rhs, lhs.c1 == rhs);
		}

		// Token: 0x06001D32 RID: 7474 RVA: 0x0004EC6A File Offset: 0x0004CE6A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x2 operator ==(int lhs, int4x2 rhs)
		{
			return new bool4x2(lhs == rhs.c0, lhs == rhs.c1);
		}

		// Token: 0x06001D33 RID: 7475 RVA: 0x0004EC89 File Offset: 0x0004CE89
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x2 operator !=(int4x2 lhs, int4x2 rhs)
		{
			return new bool4x2(lhs.c0 != rhs.c0, lhs.c1 != rhs.c1);
		}

		// Token: 0x06001D34 RID: 7476 RVA: 0x0004ECB2 File Offset: 0x0004CEB2
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x2 operator !=(int4x2 lhs, int rhs)
		{
			return new bool4x2(lhs.c0 != rhs, lhs.c1 != rhs);
		}

		// Token: 0x06001D35 RID: 7477 RVA: 0x0004ECD1 File Offset: 0x0004CED1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x2 operator !=(int lhs, int4x2 rhs)
		{
			return new bool4x2(lhs != rhs.c0, lhs != rhs.c1);
		}

		// Token: 0x06001D36 RID: 7478 RVA: 0x0004ECF0 File Offset: 0x0004CEF0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4x2 operator ~(int4x2 val)
		{
			return new int4x2(~val.c0, ~val.c1);
		}

		// Token: 0x06001D37 RID: 7479 RVA: 0x0004ED0D File Offset: 0x0004CF0D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4x2 operator &(int4x2 lhs, int4x2 rhs)
		{
			return new int4x2(lhs.c0 & rhs.c0, lhs.c1 & rhs.c1);
		}

		// Token: 0x06001D38 RID: 7480 RVA: 0x0004ED36 File Offset: 0x0004CF36
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4x2 operator &(int4x2 lhs, int rhs)
		{
			return new int4x2(lhs.c0 & rhs, lhs.c1 & rhs);
		}

		// Token: 0x06001D39 RID: 7481 RVA: 0x0004ED55 File Offset: 0x0004CF55
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4x2 operator &(int lhs, int4x2 rhs)
		{
			return new int4x2(lhs & rhs.c0, lhs & rhs.c1);
		}

		// Token: 0x06001D3A RID: 7482 RVA: 0x0004ED74 File Offset: 0x0004CF74
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4x2 operator |(int4x2 lhs, int4x2 rhs)
		{
			return new int4x2(lhs.c0 | rhs.c0, lhs.c1 | rhs.c1);
		}

		// Token: 0x06001D3B RID: 7483 RVA: 0x0004ED9D File Offset: 0x0004CF9D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4x2 operator |(int4x2 lhs, int rhs)
		{
			return new int4x2(lhs.c0 | rhs, lhs.c1 | rhs);
		}

		// Token: 0x06001D3C RID: 7484 RVA: 0x0004EDBC File Offset: 0x0004CFBC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4x2 operator |(int lhs, int4x2 rhs)
		{
			return new int4x2(lhs | rhs.c0, lhs | rhs.c1);
		}

		// Token: 0x06001D3D RID: 7485 RVA: 0x0004EDDB File Offset: 0x0004CFDB
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4x2 operator ^(int4x2 lhs, int4x2 rhs)
		{
			return new int4x2(lhs.c0 ^ rhs.c0, lhs.c1 ^ rhs.c1);
		}

		// Token: 0x06001D3E RID: 7486 RVA: 0x0004EE04 File Offset: 0x0004D004
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4x2 operator ^(int4x2 lhs, int rhs)
		{
			return new int4x2(lhs.c0 ^ rhs, lhs.c1 ^ rhs);
		}

		// Token: 0x06001D3F RID: 7487 RVA: 0x0004EE23 File Offset: 0x0004D023
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4x2 operator ^(int lhs, int4x2 rhs)
		{
			return new int4x2(lhs ^ rhs.c0, lhs ^ rhs.c1);
		}

		// Token: 0x1700099A RID: 2458
		public unsafe int4 this[int index]
		{
			get
			{
				fixed (int4x2* ptr = &this)
				{
					return ref *(int4*)(ptr + (IntPtr)index * (IntPtr)sizeof(int4) / (IntPtr)sizeof(int4x2));
				}
			}
		}

		// Token: 0x06001D41 RID: 7489 RVA: 0x0004EE5F File Offset: 0x0004D05F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool Equals(int4x2 rhs)
		{
			return this.c0.Equals(rhs.c0) && this.c1.Equals(rhs.c1);
		}

		// Token: 0x06001D42 RID: 7490 RVA: 0x0004EE88 File Offset: 0x0004D088
		public override bool Equals(object o)
		{
			if (o is int4x2)
			{
				int4x2 rhs = (int4x2)o;
				return this.Equals(rhs);
			}
			return false;
		}

		// Token: 0x06001D43 RID: 7491 RVA: 0x0004EEAD File Offset: 0x0004D0AD
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override int GetHashCode()
		{
			return (int)math.hash(this);
		}

		// Token: 0x06001D44 RID: 7492 RVA: 0x0004EEBC File Offset: 0x0004D0BC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override string ToString()
		{
			return string.Format("int4x2({0}, {1},  {2}, {3},  {4}, {5},  {6}, {7})", new object[]
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

		// Token: 0x06001D45 RID: 7493 RVA: 0x0004EF74 File Offset: 0x0004D174
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public string ToString(string format, IFormatProvider formatProvider)
		{
			return string.Format("int4x2({0}, {1},  {2}, {3},  {4}, {5},  {6}, {7})", new object[]
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

		// Token: 0x040000D5 RID: 213
		public int4 c0;

		// Token: 0x040000D6 RID: 214
		public int4 c1;

		// Token: 0x040000D7 RID: 215
		public static readonly int4x2 zero;
	}
}

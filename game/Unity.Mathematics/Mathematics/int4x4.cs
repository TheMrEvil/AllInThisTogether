using System;
using System.Runtime.CompilerServices;
using Unity.IL2CPP.CompilerServices;

namespace Unity.Mathematics
{
	// Token: 0x02000037 RID: 55
	[Il2CppEagerStaticClassConstruction]
	[Serializable]
	public struct int4x4 : IEquatable<int4x4>, IFormattable
	{
		// Token: 0x06001D91 RID: 7569 RVA: 0x0004FEE9 File Offset: 0x0004E0E9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int4x4(int4 c0, int4 c1, int4 c2, int4 c3)
		{
			this.c0 = c0;
			this.c1 = c1;
			this.c2 = c2;
			this.c3 = c3;
		}

		// Token: 0x06001D92 RID: 7570 RVA: 0x0004FF08 File Offset: 0x0004E108
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int4x4(int m00, int m01, int m02, int m03, int m10, int m11, int m12, int m13, int m20, int m21, int m22, int m23, int m30, int m31, int m32, int m33)
		{
			this.c0 = new int4(m00, m10, m20, m30);
			this.c1 = new int4(m01, m11, m21, m31);
			this.c2 = new int4(m02, m12, m22, m32);
			this.c3 = new int4(m03, m13, m23, m33);
		}

		// Token: 0x06001D93 RID: 7571 RVA: 0x0004FF5E File Offset: 0x0004E15E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int4x4(int v)
		{
			this.c0 = v;
			this.c1 = v;
			this.c2 = v;
			this.c3 = v;
		}

		// Token: 0x06001D94 RID: 7572 RVA: 0x0004FF90 File Offset: 0x0004E190
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int4x4(bool v)
		{
			this.c0 = math.select(new int4(0), new int4(1), v);
			this.c1 = math.select(new int4(0), new int4(1), v);
			this.c2 = math.select(new int4(0), new int4(1), v);
			this.c3 = math.select(new int4(0), new int4(1), v);
		}

		// Token: 0x06001D95 RID: 7573 RVA: 0x00050000 File Offset: 0x0004E200
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int4x4(bool4x4 v)
		{
			this.c0 = math.select(new int4(0), new int4(1), v.c0);
			this.c1 = math.select(new int4(0), new int4(1), v.c1);
			this.c2 = math.select(new int4(0), new int4(1), v.c2);
			this.c3 = math.select(new int4(0), new int4(1), v.c3);
		}

		// Token: 0x06001D96 RID: 7574 RVA: 0x00050081 File Offset: 0x0004E281
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int4x4(uint v)
		{
			this.c0 = (int4)v;
			this.c1 = (int4)v;
			this.c2 = (int4)v;
			this.c3 = (int4)v;
		}

		// Token: 0x06001D97 RID: 7575 RVA: 0x000500B4 File Offset: 0x0004E2B4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int4x4(uint4x4 v)
		{
			this.c0 = (int4)v.c0;
			this.c1 = (int4)v.c1;
			this.c2 = (int4)v.c2;
			this.c3 = (int4)v.c3;
		}

		// Token: 0x06001D98 RID: 7576 RVA: 0x00050105 File Offset: 0x0004E305
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int4x4(float v)
		{
			this.c0 = (int4)v;
			this.c1 = (int4)v;
			this.c2 = (int4)v;
			this.c3 = (int4)v;
		}

		// Token: 0x06001D99 RID: 7577 RVA: 0x00050138 File Offset: 0x0004E338
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int4x4(float4x4 v)
		{
			this.c0 = (int4)v.c0;
			this.c1 = (int4)v.c1;
			this.c2 = (int4)v.c2;
			this.c3 = (int4)v.c3;
		}

		// Token: 0x06001D9A RID: 7578 RVA: 0x00050189 File Offset: 0x0004E389
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int4x4(double v)
		{
			this.c0 = (int4)v;
			this.c1 = (int4)v;
			this.c2 = (int4)v;
			this.c3 = (int4)v;
		}

		// Token: 0x06001D9B RID: 7579 RVA: 0x000501BC File Offset: 0x0004E3BC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int4x4(double4x4 v)
		{
			this.c0 = (int4)v.c0;
			this.c1 = (int4)v.c1;
			this.c2 = (int4)v.c2;
			this.c3 = (int4)v.c3;
		}

		// Token: 0x06001D9C RID: 7580 RVA: 0x0005020D File Offset: 0x0004E40D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator int4x4(int v)
		{
			return new int4x4(v);
		}

		// Token: 0x06001D9D RID: 7581 RVA: 0x00050215 File Offset: 0x0004E415
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator int4x4(bool v)
		{
			return new int4x4(v);
		}

		// Token: 0x06001D9E RID: 7582 RVA: 0x0005021D File Offset: 0x0004E41D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator int4x4(bool4x4 v)
		{
			return new int4x4(v);
		}

		// Token: 0x06001D9F RID: 7583 RVA: 0x00050225 File Offset: 0x0004E425
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator int4x4(uint v)
		{
			return new int4x4(v);
		}

		// Token: 0x06001DA0 RID: 7584 RVA: 0x0005022D File Offset: 0x0004E42D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator int4x4(uint4x4 v)
		{
			return new int4x4(v);
		}

		// Token: 0x06001DA1 RID: 7585 RVA: 0x00050235 File Offset: 0x0004E435
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator int4x4(float v)
		{
			return new int4x4(v);
		}

		// Token: 0x06001DA2 RID: 7586 RVA: 0x0005023D File Offset: 0x0004E43D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator int4x4(float4x4 v)
		{
			return new int4x4(v);
		}

		// Token: 0x06001DA3 RID: 7587 RVA: 0x00050245 File Offset: 0x0004E445
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator int4x4(double v)
		{
			return new int4x4(v);
		}

		// Token: 0x06001DA4 RID: 7588 RVA: 0x0005024D File Offset: 0x0004E44D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator int4x4(double4x4 v)
		{
			return new int4x4(v);
		}

		// Token: 0x06001DA5 RID: 7589 RVA: 0x00050258 File Offset: 0x0004E458
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4x4 operator *(int4x4 lhs, int4x4 rhs)
		{
			return new int4x4(lhs.c0 * rhs.c0, lhs.c1 * rhs.c1, lhs.c2 * rhs.c2, lhs.c3 * rhs.c3);
		}

		// Token: 0x06001DA6 RID: 7590 RVA: 0x000502AE File Offset: 0x0004E4AE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4x4 operator *(int4x4 lhs, int rhs)
		{
			return new int4x4(lhs.c0 * rhs, lhs.c1 * rhs, lhs.c2 * rhs, lhs.c3 * rhs);
		}

		// Token: 0x06001DA7 RID: 7591 RVA: 0x000502E5 File Offset: 0x0004E4E5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4x4 operator *(int lhs, int4x4 rhs)
		{
			return new int4x4(lhs * rhs.c0, lhs * rhs.c1, lhs * rhs.c2, lhs * rhs.c3);
		}

		// Token: 0x06001DA8 RID: 7592 RVA: 0x0005031C File Offset: 0x0004E51C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4x4 operator +(int4x4 lhs, int4x4 rhs)
		{
			return new int4x4(lhs.c0 + rhs.c0, lhs.c1 + rhs.c1, lhs.c2 + rhs.c2, lhs.c3 + rhs.c3);
		}

		// Token: 0x06001DA9 RID: 7593 RVA: 0x00050372 File Offset: 0x0004E572
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4x4 operator +(int4x4 lhs, int rhs)
		{
			return new int4x4(lhs.c0 + rhs, lhs.c1 + rhs, lhs.c2 + rhs, lhs.c3 + rhs);
		}

		// Token: 0x06001DAA RID: 7594 RVA: 0x000503A9 File Offset: 0x0004E5A9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4x4 operator +(int lhs, int4x4 rhs)
		{
			return new int4x4(lhs + rhs.c0, lhs + rhs.c1, lhs + rhs.c2, lhs + rhs.c3);
		}

		// Token: 0x06001DAB RID: 7595 RVA: 0x000503E0 File Offset: 0x0004E5E0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4x4 operator -(int4x4 lhs, int4x4 rhs)
		{
			return new int4x4(lhs.c0 - rhs.c0, lhs.c1 - rhs.c1, lhs.c2 - rhs.c2, lhs.c3 - rhs.c3);
		}

		// Token: 0x06001DAC RID: 7596 RVA: 0x00050436 File Offset: 0x0004E636
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4x4 operator -(int4x4 lhs, int rhs)
		{
			return new int4x4(lhs.c0 - rhs, lhs.c1 - rhs, lhs.c2 - rhs, lhs.c3 - rhs);
		}

		// Token: 0x06001DAD RID: 7597 RVA: 0x0005046D File Offset: 0x0004E66D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4x4 operator -(int lhs, int4x4 rhs)
		{
			return new int4x4(lhs - rhs.c0, lhs - rhs.c1, lhs - rhs.c2, lhs - rhs.c3);
		}

		// Token: 0x06001DAE RID: 7598 RVA: 0x000504A4 File Offset: 0x0004E6A4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4x4 operator /(int4x4 lhs, int4x4 rhs)
		{
			return new int4x4(lhs.c0 / rhs.c0, lhs.c1 / rhs.c1, lhs.c2 / rhs.c2, lhs.c3 / rhs.c3);
		}

		// Token: 0x06001DAF RID: 7599 RVA: 0x000504FA File Offset: 0x0004E6FA
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4x4 operator /(int4x4 lhs, int rhs)
		{
			return new int4x4(lhs.c0 / rhs, lhs.c1 / rhs, lhs.c2 / rhs, lhs.c3 / rhs);
		}

		// Token: 0x06001DB0 RID: 7600 RVA: 0x00050531 File Offset: 0x0004E731
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4x4 operator /(int lhs, int4x4 rhs)
		{
			return new int4x4(lhs / rhs.c0, lhs / rhs.c1, lhs / rhs.c2, lhs / rhs.c3);
		}

		// Token: 0x06001DB1 RID: 7601 RVA: 0x00050568 File Offset: 0x0004E768
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4x4 operator %(int4x4 lhs, int4x4 rhs)
		{
			return new int4x4(lhs.c0 % rhs.c0, lhs.c1 % rhs.c1, lhs.c2 % rhs.c2, lhs.c3 % rhs.c3);
		}

		// Token: 0x06001DB2 RID: 7602 RVA: 0x000505BE File Offset: 0x0004E7BE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4x4 operator %(int4x4 lhs, int rhs)
		{
			return new int4x4(lhs.c0 % rhs, lhs.c1 % rhs, lhs.c2 % rhs, lhs.c3 % rhs);
		}

		// Token: 0x06001DB3 RID: 7603 RVA: 0x000505F5 File Offset: 0x0004E7F5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4x4 operator %(int lhs, int4x4 rhs)
		{
			return new int4x4(lhs % rhs.c0, lhs % rhs.c1, lhs % rhs.c2, lhs % rhs.c3);
		}

		// Token: 0x06001DB4 RID: 7604 RVA: 0x0005062C File Offset: 0x0004E82C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4x4 operator ++(int4x4 val)
		{
			int4 @int = ++val.c0;
			val.c0 = @int;
			int4 int2 = @int;
			@int = ++val.c1;
			val.c1 = @int;
			int4 int3 = @int;
			@int = ++val.c2;
			val.c2 = @int;
			int4 int4 = @int;
			@int = ++val.c3;
			val.c3 = @int;
			return new int4x4(int2, int3, int4, @int);
		}

		// Token: 0x06001DB5 RID: 7605 RVA: 0x000506A8 File Offset: 0x0004E8A8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4x4 operator --(int4x4 val)
		{
			int4 @int = --val.c0;
			val.c0 = @int;
			int4 int2 = @int;
			@int = --val.c1;
			val.c1 = @int;
			int4 int3 = @int;
			@int = --val.c2;
			val.c2 = @int;
			int4 int4 = @int;
			@int = --val.c3;
			val.c3 = @int;
			return new int4x4(int2, int3, int4, @int);
		}

		// Token: 0x06001DB6 RID: 7606 RVA: 0x00050724 File Offset: 0x0004E924
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x4 operator <(int4x4 lhs, int4x4 rhs)
		{
			return new bool4x4(lhs.c0 < rhs.c0, lhs.c1 < rhs.c1, lhs.c2 < rhs.c2, lhs.c3 < rhs.c3);
		}

		// Token: 0x06001DB7 RID: 7607 RVA: 0x0005077A File Offset: 0x0004E97A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x4 operator <(int4x4 lhs, int rhs)
		{
			return new bool4x4(lhs.c0 < rhs, lhs.c1 < rhs, lhs.c2 < rhs, lhs.c3 < rhs);
		}

		// Token: 0x06001DB8 RID: 7608 RVA: 0x000507B1 File Offset: 0x0004E9B1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x4 operator <(int lhs, int4x4 rhs)
		{
			return new bool4x4(lhs < rhs.c0, lhs < rhs.c1, lhs < rhs.c2, lhs < rhs.c3);
		}

		// Token: 0x06001DB9 RID: 7609 RVA: 0x000507E8 File Offset: 0x0004E9E8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x4 operator <=(int4x4 lhs, int4x4 rhs)
		{
			return new bool4x4(lhs.c0 <= rhs.c0, lhs.c1 <= rhs.c1, lhs.c2 <= rhs.c2, lhs.c3 <= rhs.c3);
		}

		// Token: 0x06001DBA RID: 7610 RVA: 0x0005083E File Offset: 0x0004EA3E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x4 operator <=(int4x4 lhs, int rhs)
		{
			return new bool4x4(lhs.c0 <= rhs, lhs.c1 <= rhs, lhs.c2 <= rhs, lhs.c3 <= rhs);
		}

		// Token: 0x06001DBB RID: 7611 RVA: 0x00050875 File Offset: 0x0004EA75
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x4 operator <=(int lhs, int4x4 rhs)
		{
			return new bool4x4(lhs <= rhs.c0, lhs <= rhs.c1, lhs <= rhs.c2, lhs <= rhs.c3);
		}

		// Token: 0x06001DBC RID: 7612 RVA: 0x000508AC File Offset: 0x0004EAAC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x4 operator >(int4x4 lhs, int4x4 rhs)
		{
			return new bool4x4(lhs.c0 > rhs.c0, lhs.c1 > rhs.c1, lhs.c2 > rhs.c2, lhs.c3 > rhs.c3);
		}

		// Token: 0x06001DBD RID: 7613 RVA: 0x00050902 File Offset: 0x0004EB02
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x4 operator >(int4x4 lhs, int rhs)
		{
			return new bool4x4(lhs.c0 > rhs, lhs.c1 > rhs, lhs.c2 > rhs, lhs.c3 > rhs);
		}

		// Token: 0x06001DBE RID: 7614 RVA: 0x00050939 File Offset: 0x0004EB39
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x4 operator >(int lhs, int4x4 rhs)
		{
			return new bool4x4(lhs > rhs.c0, lhs > rhs.c1, lhs > rhs.c2, lhs > rhs.c3);
		}

		// Token: 0x06001DBF RID: 7615 RVA: 0x00050970 File Offset: 0x0004EB70
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x4 operator >=(int4x4 lhs, int4x4 rhs)
		{
			return new bool4x4(lhs.c0 >= rhs.c0, lhs.c1 >= rhs.c1, lhs.c2 >= rhs.c2, lhs.c3 >= rhs.c3);
		}

		// Token: 0x06001DC0 RID: 7616 RVA: 0x000509C6 File Offset: 0x0004EBC6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x4 operator >=(int4x4 lhs, int rhs)
		{
			return new bool4x4(lhs.c0 >= rhs, lhs.c1 >= rhs, lhs.c2 >= rhs, lhs.c3 >= rhs);
		}

		// Token: 0x06001DC1 RID: 7617 RVA: 0x000509FD File Offset: 0x0004EBFD
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x4 operator >=(int lhs, int4x4 rhs)
		{
			return new bool4x4(lhs >= rhs.c0, lhs >= rhs.c1, lhs >= rhs.c2, lhs >= rhs.c3);
		}

		// Token: 0x06001DC2 RID: 7618 RVA: 0x00050A34 File Offset: 0x0004EC34
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4x4 operator -(int4x4 val)
		{
			return new int4x4(-val.c0, -val.c1, -val.c2, -val.c3);
		}

		// Token: 0x06001DC3 RID: 7619 RVA: 0x00050A67 File Offset: 0x0004EC67
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4x4 operator +(int4x4 val)
		{
			return new int4x4(+val.c0, +val.c1, +val.c2, +val.c3);
		}

		// Token: 0x06001DC4 RID: 7620 RVA: 0x00050A9A File Offset: 0x0004EC9A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4x4 operator <<(int4x4 x, int n)
		{
			return new int4x4(x.c0 << n, x.c1 << n, x.c2 << n, x.c3 << n);
		}

		// Token: 0x06001DC5 RID: 7621 RVA: 0x00050AD1 File Offset: 0x0004ECD1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4x4 operator >>(int4x4 x, int n)
		{
			return new int4x4(x.c0 >> n, x.c1 >> n, x.c2 >> n, x.c3 >> n);
		}

		// Token: 0x06001DC6 RID: 7622 RVA: 0x00050B08 File Offset: 0x0004ED08
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x4 operator ==(int4x4 lhs, int4x4 rhs)
		{
			return new bool4x4(lhs.c0 == rhs.c0, lhs.c1 == rhs.c1, lhs.c2 == rhs.c2, lhs.c3 == rhs.c3);
		}

		// Token: 0x06001DC7 RID: 7623 RVA: 0x00050B5E File Offset: 0x0004ED5E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x4 operator ==(int4x4 lhs, int rhs)
		{
			return new bool4x4(lhs.c0 == rhs, lhs.c1 == rhs, lhs.c2 == rhs, lhs.c3 == rhs);
		}

		// Token: 0x06001DC8 RID: 7624 RVA: 0x00050B95 File Offset: 0x0004ED95
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x4 operator ==(int lhs, int4x4 rhs)
		{
			return new bool4x4(lhs == rhs.c0, lhs == rhs.c1, lhs == rhs.c2, lhs == rhs.c3);
		}

		// Token: 0x06001DC9 RID: 7625 RVA: 0x00050BCC File Offset: 0x0004EDCC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x4 operator !=(int4x4 lhs, int4x4 rhs)
		{
			return new bool4x4(lhs.c0 != rhs.c0, lhs.c1 != rhs.c1, lhs.c2 != rhs.c2, lhs.c3 != rhs.c3);
		}

		// Token: 0x06001DCA RID: 7626 RVA: 0x00050C22 File Offset: 0x0004EE22
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x4 operator !=(int4x4 lhs, int rhs)
		{
			return new bool4x4(lhs.c0 != rhs, lhs.c1 != rhs, lhs.c2 != rhs, lhs.c3 != rhs);
		}

		// Token: 0x06001DCB RID: 7627 RVA: 0x00050C59 File Offset: 0x0004EE59
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x4 operator !=(int lhs, int4x4 rhs)
		{
			return new bool4x4(lhs != rhs.c0, lhs != rhs.c1, lhs != rhs.c2, lhs != rhs.c3);
		}

		// Token: 0x06001DCC RID: 7628 RVA: 0x00050C90 File Offset: 0x0004EE90
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4x4 operator ~(int4x4 val)
		{
			return new int4x4(~val.c0, ~val.c1, ~val.c2, ~val.c3);
		}

		// Token: 0x06001DCD RID: 7629 RVA: 0x00050CC4 File Offset: 0x0004EEC4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4x4 operator &(int4x4 lhs, int4x4 rhs)
		{
			return new int4x4(lhs.c0 & rhs.c0, lhs.c1 & rhs.c1, lhs.c2 & rhs.c2, lhs.c3 & rhs.c3);
		}

		// Token: 0x06001DCE RID: 7630 RVA: 0x00050D1A File Offset: 0x0004EF1A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4x4 operator &(int4x4 lhs, int rhs)
		{
			return new int4x4(lhs.c0 & rhs, lhs.c1 & rhs, lhs.c2 & rhs, lhs.c3 & rhs);
		}

		// Token: 0x06001DCF RID: 7631 RVA: 0x00050D51 File Offset: 0x0004EF51
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4x4 operator &(int lhs, int4x4 rhs)
		{
			return new int4x4(lhs & rhs.c0, lhs & rhs.c1, lhs & rhs.c2, lhs & rhs.c3);
		}

		// Token: 0x06001DD0 RID: 7632 RVA: 0x00050D88 File Offset: 0x0004EF88
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4x4 operator |(int4x4 lhs, int4x4 rhs)
		{
			return new int4x4(lhs.c0 | rhs.c0, lhs.c1 | rhs.c1, lhs.c2 | rhs.c2, lhs.c3 | rhs.c3);
		}

		// Token: 0x06001DD1 RID: 7633 RVA: 0x00050DDE File Offset: 0x0004EFDE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4x4 operator |(int4x4 lhs, int rhs)
		{
			return new int4x4(lhs.c0 | rhs, lhs.c1 | rhs, lhs.c2 | rhs, lhs.c3 | rhs);
		}

		// Token: 0x06001DD2 RID: 7634 RVA: 0x00050E15 File Offset: 0x0004F015
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4x4 operator |(int lhs, int4x4 rhs)
		{
			return new int4x4(lhs | rhs.c0, lhs | rhs.c1, lhs | rhs.c2, lhs | rhs.c3);
		}

		// Token: 0x06001DD3 RID: 7635 RVA: 0x00050E4C File Offset: 0x0004F04C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4x4 operator ^(int4x4 lhs, int4x4 rhs)
		{
			return new int4x4(lhs.c0 ^ rhs.c0, lhs.c1 ^ rhs.c1, lhs.c2 ^ rhs.c2, lhs.c3 ^ rhs.c3);
		}

		// Token: 0x06001DD4 RID: 7636 RVA: 0x00050EA2 File Offset: 0x0004F0A2
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4x4 operator ^(int4x4 lhs, int rhs)
		{
			return new int4x4(lhs.c0 ^ rhs, lhs.c1 ^ rhs, lhs.c2 ^ rhs, lhs.c3 ^ rhs);
		}

		// Token: 0x06001DD5 RID: 7637 RVA: 0x00050ED9 File Offset: 0x0004F0D9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4x4 operator ^(int lhs, int4x4 rhs)
		{
			return new int4x4(lhs ^ rhs.c0, lhs ^ rhs.c1, lhs ^ rhs.c2, lhs ^ rhs.c3);
		}

		// Token: 0x1700099C RID: 2460
		public unsafe int4 this[int index]
		{
			get
			{
				fixed (int4x4* ptr = &this)
				{
					return ref *(int4*)(ptr + (IntPtr)index * (IntPtr)sizeof(int4) / (IntPtr)sizeof(int4x4));
				}
			}
		}

		// Token: 0x06001DD7 RID: 7639 RVA: 0x00050F2C File Offset: 0x0004F12C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool Equals(int4x4 rhs)
		{
			return this.c0.Equals(rhs.c0) && this.c1.Equals(rhs.c1) && this.c2.Equals(rhs.c2) && this.c3.Equals(rhs.c3);
		}

		// Token: 0x06001DD8 RID: 7640 RVA: 0x00050F88 File Offset: 0x0004F188
		public override bool Equals(object o)
		{
			if (o is int4x4)
			{
				int4x4 rhs = (int4x4)o;
				return this.Equals(rhs);
			}
			return false;
		}

		// Token: 0x06001DD9 RID: 7641 RVA: 0x00050FAD File Offset: 0x0004F1AD
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override int GetHashCode()
		{
			return (int)math.hash(this);
		}

		// Token: 0x06001DDA RID: 7642 RVA: 0x00050FBC File Offset: 0x0004F1BC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override string ToString()
		{
			return string.Format("int4x4({0}, {1}, {2}, {3},  {4}, {5}, {6}, {7},  {8}, {9}, {10}, {11},  {12}, {13}, {14}, {15})", new object[]
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
				this.c3.z,
				this.c0.w,
				this.c1.w,
				this.c2.w,
				this.c3.w
			});
		}

		// Token: 0x06001DDB RID: 7643 RVA: 0x00051114 File Offset: 0x0004F314
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public string ToString(string format, IFormatProvider formatProvider)
		{
			return string.Format("int4x4({0}, {1}, {2}, {3},  {4}, {5}, {6}, {7},  {8}, {9}, {10}, {11},  {12}, {13}, {14}, {15})", new object[]
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
				this.c3.z.ToString(format, formatProvider),
				this.c0.w.ToString(format, formatProvider),
				this.c1.w.ToString(format, formatProvider),
				this.c2.w.ToString(format, formatProvider),
				this.c3.w.ToString(format, formatProvider)
			});
		}

		// Token: 0x06001DDC RID: 7644 RVA: 0x0005128C File Offset: 0x0004F48C
		// Note: this type is marked as 'beforefieldinit'.
		static int4x4()
		{
		}

		// Token: 0x040000DC RID: 220
		public int4 c0;

		// Token: 0x040000DD RID: 221
		public int4 c1;

		// Token: 0x040000DE RID: 222
		public int4 c2;

		// Token: 0x040000DF RID: 223
		public int4 c3;

		// Token: 0x040000E0 RID: 224
		public static readonly int4x4 identity = new int4x4(1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1);

		// Token: 0x040000E1 RID: 225
		public static readonly int4x4 zero;
	}
}

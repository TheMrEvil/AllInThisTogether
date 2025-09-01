using System;
using System.Runtime.CompilerServices;
using Unity.IL2CPP.CompilerServices;

namespace Unity.Mathematics
{
	// Token: 0x02000031 RID: 49
	[Il2CppEagerStaticClassConstruction]
	[Serializable]
	public struct int3x2 : IEquatable<int3x2>, IFormattable
	{
		// Token: 0x06001A3B RID: 6715 RVA: 0x00047C06 File Offset: 0x00045E06
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int3x2(int3 c0, int3 c1)
		{
			this.c0 = c0;
			this.c1 = c1;
		}

		// Token: 0x06001A3C RID: 6716 RVA: 0x00047C16 File Offset: 0x00045E16
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int3x2(int m00, int m01, int m10, int m11, int m20, int m21)
		{
			this.c0 = new int3(m00, m10, m20);
			this.c1 = new int3(m01, m11, m21);
		}

		// Token: 0x06001A3D RID: 6717 RVA: 0x00047C37 File Offset: 0x00045E37
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int3x2(int v)
		{
			this.c0 = v;
			this.c1 = v;
		}

		// Token: 0x06001A3E RID: 6718 RVA: 0x00047C51 File Offset: 0x00045E51
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int3x2(bool v)
		{
			this.c0 = math.select(new int3(0), new int3(1), v);
			this.c1 = math.select(new int3(0), new int3(1), v);
		}

		// Token: 0x06001A3F RID: 6719 RVA: 0x00047C83 File Offset: 0x00045E83
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int3x2(bool3x2 v)
		{
			this.c0 = math.select(new int3(0), new int3(1), v.c0);
			this.c1 = math.select(new int3(0), new int3(1), v.c1);
		}

		// Token: 0x06001A40 RID: 6720 RVA: 0x00047CBF File Offset: 0x00045EBF
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int3x2(uint v)
		{
			this.c0 = (int3)v;
			this.c1 = (int3)v;
		}

		// Token: 0x06001A41 RID: 6721 RVA: 0x00047CD9 File Offset: 0x00045ED9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int3x2(uint3x2 v)
		{
			this.c0 = (int3)v.c0;
			this.c1 = (int3)v.c1;
		}

		// Token: 0x06001A42 RID: 6722 RVA: 0x00047CFD File Offset: 0x00045EFD
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int3x2(float v)
		{
			this.c0 = (int3)v;
			this.c1 = (int3)v;
		}

		// Token: 0x06001A43 RID: 6723 RVA: 0x00047D17 File Offset: 0x00045F17
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int3x2(float3x2 v)
		{
			this.c0 = (int3)v.c0;
			this.c1 = (int3)v.c1;
		}

		// Token: 0x06001A44 RID: 6724 RVA: 0x00047D3B File Offset: 0x00045F3B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int3x2(double v)
		{
			this.c0 = (int3)v;
			this.c1 = (int3)v;
		}

		// Token: 0x06001A45 RID: 6725 RVA: 0x00047D55 File Offset: 0x00045F55
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int3x2(double3x2 v)
		{
			this.c0 = (int3)v.c0;
			this.c1 = (int3)v.c1;
		}

		// Token: 0x06001A46 RID: 6726 RVA: 0x00047D79 File Offset: 0x00045F79
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator int3x2(int v)
		{
			return new int3x2(v);
		}

		// Token: 0x06001A47 RID: 6727 RVA: 0x00047D81 File Offset: 0x00045F81
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator int3x2(bool v)
		{
			return new int3x2(v);
		}

		// Token: 0x06001A48 RID: 6728 RVA: 0x00047D89 File Offset: 0x00045F89
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator int3x2(bool3x2 v)
		{
			return new int3x2(v);
		}

		// Token: 0x06001A49 RID: 6729 RVA: 0x00047D91 File Offset: 0x00045F91
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator int3x2(uint v)
		{
			return new int3x2(v);
		}

		// Token: 0x06001A4A RID: 6730 RVA: 0x00047D99 File Offset: 0x00045F99
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator int3x2(uint3x2 v)
		{
			return new int3x2(v);
		}

		// Token: 0x06001A4B RID: 6731 RVA: 0x00047DA1 File Offset: 0x00045FA1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator int3x2(float v)
		{
			return new int3x2(v);
		}

		// Token: 0x06001A4C RID: 6732 RVA: 0x00047DA9 File Offset: 0x00045FA9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator int3x2(float3x2 v)
		{
			return new int3x2(v);
		}

		// Token: 0x06001A4D RID: 6733 RVA: 0x00047DB1 File Offset: 0x00045FB1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator int3x2(double v)
		{
			return new int3x2(v);
		}

		// Token: 0x06001A4E RID: 6734 RVA: 0x00047DB9 File Offset: 0x00045FB9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator int3x2(double3x2 v)
		{
			return new int3x2(v);
		}

		// Token: 0x06001A4F RID: 6735 RVA: 0x00047DC1 File Offset: 0x00045FC1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3x2 operator *(int3x2 lhs, int3x2 rhs)
		{
			return new int3x2(lhs.c0 * rhs.c0, lhs.c1 * rhs.c1);
		}

		// Token: 0x06001A50 RID: 6736 RVA: 0x00047DEA File Offset: 0x00045FEA
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3x2 operator *(int3x2 lhs, int rhs)
		{
			return new int3x2(lhs.c0 * rhs, lhs.c1 * rhs);
		}

		// Token: 0x06001A51 RID: 6737 RVA: 0x00047E09 File Offset: 0x00046009
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3x2 operator *(int lhs, int3x2 rhs)
		{
			return new int3x2(lhs * rhs.c0, lhs * rhs.c1);
		}

		// Token: 0x06001A52 RID: 6738 RVA: 0x00047E28 File Offset: 0x00046028
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3x2 operator +(int3x2 lhs, int3x2 rhs)
		{
			return new int3x2(lhs.c0 + rhs.c0, lhs.c1 + rhs.c1);
		}

		// Token: 0x06001A53 RID: 6739 RVA: 0x00047E51 File Offset: 0x00046051
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3x2 operator +(int3x2 lhs, int rhs)
		{
			return new int3x2(lhs.c0 + rhs, lhs.c1 + rhs);
		}

		// Token: 0x06001A54 RID: 6740 RVA: 0x00047E70 File Offset: 0x00046070
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3x2 operator +(int lhs, int3x2 rhs)
		{
			return new int3x2(lhs + rhs.c0, lhs + rhs.c1);
		}

		// Token: 0x06001A55 RID: 6741 RVA: 0x00047E8F File Offset: 0x0004608F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3x2 operator -(int3x2 lhs, int3x2 rhs)
		{
			return new int3x2(lhs.c0 - rhs.c0, lhs.c1 - rhs.c1);
		}

		// Token: 0x06001A56 RID: 6742 RVA: 0x00047EB8 File Offset: 0x000460B8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3x2 operator -(int3x2 lhs, int rhs)
		{
			return new int3x2(lhs.c0 - rhs, lhs.c1 - rhs);
		}

		// Token: 0x06001A57 RID: 6743 RVA: 0x00047ED7 File Offset: 0x000460D7
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3x2 operator -(int lhs, int3x2 rhs)
		{
			return new int3x2(lhs - rhs.c0, lhs - rhs.c1);
		}

		// Token: 0x06001A58 RID: 6744 RVA: 0x00047EF6 File Offset: 0x000460F6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3x2 operator /(int3x2 lhs, int3x2 rhs)
		{
			return new int3x2(lhs.c0 / rhs.c0, lhs.c1 / rhs.c1);
		}

		// Token: 0x06001A59 RID: 6745 RVA: 0x00047F1F File Offset: 0x0004611F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3x2 operator /(int3x2 lhs, int rhs)
		{
			return new int3x2(lhs.c0 / rhs, lhs.c1 / rhs);
		}

		// Token: 0x06001A5A RID: 6746 RVA: 0x00047F3E File Offset: 0x0004613E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3x2 operator /(int lhs, int3x2 rhs)
		{
			return new int3x2(lhs / rhs.c0, lhs / rhs.c1);
		}

		// Token: 0x06001A5B RID: 6747 RVA: 0x00047F5D File Offset: 0x0004615D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3x2 operator %(int3x2 lhs, int3x2 rhs)
		{
			return new int3x2(lhs.c0 % rhs.c0, lhs.c1 % rhs.c1);
		}

		// Token: 0x06001A5C RID: 6748 RVA: 0x00047F86 File Offset: 0x00046186
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3x2 operator %(int3x2 lhs, int rhs)
		{
			return new int3x2(lhs.c0 % rhs, lhs.c1 % rhs);
		}

		// Token: 0x06001A5D RID: 6749 RVA: 0x00047FA5 File Offset: 0x000461A5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3x2 operator %(int lhs, int3x2 rhs)
		{
			return new int3x2(lhs % rhs.c0, lhs % rhs.c1);
		}

		// Token: 0x06001A5E RID: 6750 RVA: 0x00047FC4 File Offset: 0x000461C4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3x2 operator ++(int3x2 val)
		{
			int3 @int = ++val.c0;
			val.c0 = @int;
			int3 int2 = @int;
			@int = ++val.c1;
			val.c1 = @int;
			return new int3x2(int2, @int);
		}

		// Token: 0x06001A5F RID: 6751 RVA: 0x0004800C File Offset: 0x0004620C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3x2 operator --(int3x2 val)
		{
			int3 @int = --val.c0;
			val.c0 = @int;
			int3 int2 = @int;
			@int = --val.c1;
			val.c1 = @int;
			return new int3x2(int2, @int);
		}

		// Token: 0x06001A60 RID: 6752 RVA: 0x00048052 File Offset: 0x00046252
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x2 operator <(int3x2 lhs, int3x2 rhs)
		{
			return new bool3x2(lhs.c0 < rhs.c0, lhs.c1 < rhs.c1);
		}

		// Token: 0x06001A61 RID: 6753 RVA: 0x0004807B File Offset: 0x0004627B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x2 operator <(int3x2 lhs, int rhs)
		{
			return new bool3x2(lhs.c0 < rhs, lhs.c1 < rhs);
		}

		// Token: 0x06001A62 RID: 6754 RVA: 0x0004809A File Offset: 0x0004629A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x2 operator <(int lhs, int3x2 rhs)
		{
			return new bool3x2(lhs < rhs.c0, lhs < rhs.c1);
		}

		// Token: 0x06001A63 RID: 6755 RVA: 0x000480B9 File Offset: 0x000462B9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x2 operator <=(int3x2 lhs, int3x2 rhs)
		{
			return new bool3x2(lhs.c0 <= rhs.c0, lhs.c1 <= rhs.c1);
		}

		// Token: 0x06001A64 RID: 6756 RVA: 0x000480E2 File Offset: 0x000462E2
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x2 operator <=(int3x2 lhs, int rhs)
		{
			return new bool3x2(lhs.c0 <= rhs, lhs.c1 <= rhs);
		}

		// Token: 0x06001A65 RID: 6757 RVA: 0x00048101 File Offset: 0x00046301
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x2 operator <=(int lhs, int3x2 rhs)
		{
			return new bool3x2(lhs <= rhs.c0, lhs <= rhs.c1);
		}

		// Token: 0x06001A66 RID: 6758 RVA: 0x00048120 File Offset: 0x00046320
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x2 operator >(int3x2 lhs, int3x2 rhs)
		{
			return new bool3x2(lhs.c0 > rhs.c0, lhs.c1 > rhs.c1);
		}

		// Token: 0x06001A67 RID: 6759 RVA: 0x00048149 File Offset: 0x00046349
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x2 operator >(int3x2 lhs, int rhs)
		{
			return new bool3x2(lhs.c0 > rhs, lhs.c1 > rhs);
		}

		// Token: 0x06001A68 RID: 6760 RVA: 0x00048168 File Offset: 0x00046368
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x2 operator >(int lhs, int3x2 rhs)
		{
			return new bool3x2(lhs > rhs.c0, lhs > rhs.c1);
		}

		// Token: 0x06001A69 RID: 6761 RVA: 0x00048187 File Offset: 0x00046387
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x2 operator >=(int3x2 lhs, int3x2 rhs)
		{
			return new bool3x2(lhs.c0 >= rhs.c0, lhs.c1 >= rhs.c1);
		}

		// Token: 0x06001A6A RID: 6762 RVA: 0x000481B0 File Offset: 0x000463B0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x2 operator >=(int3x2 lhs, int rhs)
		{
			return new bool3x2(lhs.c0 >= rhs, lhs.c1 >= rhs);
		}

		// Token: 0x06001A6B RID: 6763 RVA: 0x000481CF File Offset: 0x000463CF
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x2 operator >=(int lhs, int3x2 rhs)
		{
			return new bool3x2(lhs >= rhs.c0, lhs >= rhs.c1);
		}

		// Token: 0x06001A6C RID: 6764 RVA: 0x000481EE File Offset: 0x000463EE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3x2 operator -(int3x2 val)
		{
			return new int3x2(-val.c0, -val.c1);
		}

		// Token: 0x06001A6D RID: 6765 RVA: 0x0004820B File Offset: 0x0004640B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3x2 operator +(int3x2 val)
		{
			return new int3x2(+val.c0, +val.c1);
		}

		// Token: 0x06001A6E RID: 6766 RVA: 0x00048228 File Offset: 0x00046428
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3x2 operator <<(int3x2 x, int n)
		{
			return new int3x2(x.c0 << n, x.c1 << n);
		}

		// Token: 0x06001A6F RID: 6767 RVA: 0x00048247 File Offset: 0x00046447
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3x2 operator >>(int3x2 x, int n)
		{
			return new int3x2(x.c0 >> n, x.c1 >> n);
		}

		// Token: 0x06001A70 RID: 6768 RVA: 0x00048266 File Offset: 0x00046466
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x2 operator ==(int3x2 lhs, int3x2 rhs)
		{
			return new bool3x2(lhs.c0 == rhs.c0, lhs.c1 == rhs.c1);
		}

		// Token: 0x06001A71 RID: 6769 RVA: 0x0004828F File Offset: 0x0004648F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x2 operator ==(int3x2 lhs, int rhs)
		{
			return new bool3x2(lhs.c0 == rhs, lhs.c1 == rhs);
		}

		// Token: 0x06001A72 RID: 6770 RVA: 0x000482AE File Offset: 0x000464AE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x2 operator ==(int lhs, int3x2 rhs)
		{
			return new bool3x2(lhs == rhs.c0, lhs == rhs.c1);
		}

		// Token: 0x06001A73 RID: 6771 RVA: 0x000482CD File Offset: 0x000464CD
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x2 operator !=(int3x2 lhs, int3x2 rhs)
		{
			return new bool3x2(lhs.c0 != rhs.c0, lhs.c1 != rhs.c1);
		}

		// Token: 0x06001A74 RID: 6772 RVA: 0x000482F6 File Offset: 0x000464F6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x2 operator !=(int3x2 lhs, int rhs)
		{
			return new bool3x2(lhs.c0 != rhs, lhs.c1 != rhs);
		}

		// Token: 0x06001A75 RID: 6773 RVA: 0x00048315 File Offset: 0x00046515
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x2 operator !=(int lhs, int3x2 rhs)
		{
			return new bool3x2(lhs != rhs.c0, lhs != rhs.c1);
		}

		// Token: 0x06001A76 RID: 6774 RVA: 0x00048334 File Offset: 0x00046534
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3x2 operator ~(int3x2 val)
		{
			return new int3x2(~val.c0, ~val.c1);
		}

		// Token: 0x06001A77 RID: 6775 RVA: 0x00048351 File Offset: 0x00046551
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3x2 operator &(int3x2 lhs, int3x2 rhs)
		{
			return new int3x2(lhs.c0 & rhs.c0, lhs.c1 & rhs.c1);
		}

		// Token: 0x06001A78 RID: 6776 RVA: 0x0004837A File Offset: 0x0004657A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3x2 operator &(int3x2 lhs, int rhs)
		{
			return new int3x2(lhs.c0 & rhs, lhs.c1 & rhs);
		}

		// Token: 0x06001A79 RID: 6777 RVA: 0x00048399 File Offset: 0x00046599
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3x2 operator &(int lhs, int3x2 rhs)
		{
			return new int3x2(lhs & rhs.c0, lhs & rhs.c1);
		}

		// Token: 0x06001A7A RID: 6778 RVA: 0x000483B8 File Offset: 0x000465B8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3x2 operator |(int3x2 lhs, int3x2 rhs)
		{
			return new int3x2(lhs.c0 | rhs.c0, lhs.c1 | rhs.c1);
		}

		// Token: 0x06001A7B RID: 6779 RVA: 0x000483E1 File Offset: 0x000465E1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3x2 operator |(int3x2 lhs, int rhs)
		{
			return new int3x2(lhs.c0 | rhs, lhs.c1 | rhs);
		}

		// Token: 0x06001A7C RID: 6780 RVA: 0x00048400 File Offset: 0x00046600
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3x2 operator |(int lhs, int3x2 rhs)
		{
			return new int3x2(lhs | rhs.c0, lhs | rhs.c1);
		}

		// Token: 0x06001A7D RID: 6781 RVA: 0x0004841F File Offset: 0x0004661F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3x2 operator ^(int3x2 lhs, int3x2 rhs)
		{
			return new int3x2(lhs.c0 ^ rhs.c0, lhs.c1 ^ rhs.c1);
		}

		// Token: 0x06001A7E RID: 6782 RVA: 0x00048448 File Offset: 0x00046648
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3x2 operator ^(int3x2 lhs, int rhs)
		{
			return new int3x2(lhs.c0 ^ rhs, lhs.c1 ^ rhs);
		}

		// Token: 0x06001A7F RID: 6783 RVA: 0x00048467 File Offset: 0x00046667
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3x2 operator ^(int lhs, int3x2 rhs)
		{
			return new int3x2(lhs ^ rhs.c0, lhs ^ rhs.c1);
		}

		// Token: 0x17000846 RID: 2118
		public unsafe int3 this[int index]
		{
			get
			{
				fixed (int3x2* ptr = &this)
				{
					return ref *(int3*)(ptr + (IntPtr)index * (IntPtr)sizeof(int3) / (IntPtr)sizeof(int3x2));
				}
			}
		}

		// Token: 0x06001A81 RID: 6785 RVA: 0x000484A3 File Offset: 0x000466A3
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool Equals(int3x2 rhs)
		{
			return this.c0.Equals(rhs.c0) && this.c1.Equals(rhs.c1);
		}

		// Token: 0x06001A82 RID: 6786 RVA: 0x000484CC File Offset: 0x000466CC
		public override bool Equals(object o)
		{
			if (o is int3x2)
			{
				int3x2 rhs = (int3x2)o;
				return this.Equals(rhs);
			}
			return false;
		}

		// Token: 0x06001A83 RID: 6787 RVA: 0x000484F1 File Offset: 0x000466F1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override int GetHashCode()
		{
			return (int)math.hash(this);
		}

		// Token: 0x06001A84 RID: 6788 RVA: 0x00048500 File Offset: 0x00046700
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override string ToString()
		{
			return string.Format("int3x2({0}, {1},  {2}, {3},  {4}, {5})", new object[]
			{
				this.c0.x,
				this.c1.x,
				this.c0.y,
				this.c1.y,
				this.c0.z,
				this.c1.z
			});
		}

		// Token: 0x06001A85 RID: 6789 RVA: 0x00048590 File Offset: 0x00046790
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public string ToString(string format, IFormatProvider formatProvider)
		{
			return string.Format("int3x2({0}, {1},  {2}, {3},  {4}, {5})", new object[]
			{
				this.c0.x.ToString(format, formatProvider),
				this.c1.x.ToString(format, formatProvider),
				this.c0.y.ToString(format, formatProvider),
				this.c1.y.ToString(format, formatProvider),
				this.c0.z.ToString(format, formatProvider),
				this.c1.z.ToString(format, formatProvider)
			});
		}

		// Token: 0x040000C3 RID: 195
		public int3 c0;

		// Token: 0x040000C4 RID: 196
		public int3 c1;

		// Token: 0x040000C5 RID: 197
		public static readonly int3x2 zero;
	}
}

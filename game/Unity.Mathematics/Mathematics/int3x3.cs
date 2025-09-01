using System;
using System.Runtime.CompilerServices;
using Unity.IL2CPP.CompilerServices;

namespace Unity.Mathematics
{
	// Token: 0x02000032 RID: 50
	[Il2CppEagerStaticClassConstruction]
	[Serializable]
	public struct int3x3 : IEquatable<int3x3>, IFormattable
	{
		// Token: 0x06001A86 RID: 6790 RVA: 0x0004862B File Offset: 0x0004682B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int3x3(int3 c0, int3 c1, int3 c2)
		{
			this.c0 = c0;
			this.c1 = c1;
			this.c2 = c2;
		}

		// Token: 0x06001A87 RID: 6791 RVA: 0x00048642 File Offset: 0x00046842
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int3x3(int m00, int m01, int m02, int m10, int m11, int m12, int m20, int m21, int m22)
		{
			this.c0 = new int3(m00, m10, m20);
			this.c1 = new int3(m01, m11, m21);
			this.c2 = new int3(m02, m12, m22);
		}

		// Token: 0x06001A88 RID: 6792 RVA: 0x00048674 File Offset: 0x00046874
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int3x3(int v)
		{
			this.c0 = v;
			this.c1 = v;
			this.c2 = v;
		}

		// Token: 0x06001A89 RID: 6793 RVA: 0x0004869C File Offset: 0x0004689C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int3x3(bool v)
		{
			this.c0 = math.select(new int3(0), new int3(1), v);
			this.c1 = math.select(new int3(0), new int3(1), v);
			this.c2 = math.select(new int3(0), new int3(1), v);
		}

		// Token: 0x06001A8A RID: 6794 RVA: 0x000486F4 File Offset: 0x000468F4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int3x3(bool3x3 v)
		{
			this.c0 = math.select(new int3(0), new int3(1), v.c0);
			this.c1 = math.select(new int3(0), new int3(1), v.c1);
			this.c2 = math.select(new int3(0), new int3(1), v.c2);
		}

		// Token: 0x06001A8B RID: 6795 RVA: 0x00048758 File Offset: 0x00046958
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int3x3(uint v)
		{
			this.c0 = (int3)v;
			this.c1 = (int3)v;
			this.c2 = (int3)v;
		}

		// Token: 0x06001A8C RID: 6796 RVA: 0x0004877E File Offset: 0x0004697E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int3x3(uint3x3 v)
		{
			this.c0 = (int3)v.c0;
			this.c1 = (int3)v.c1;
			this.c2 = (int3)v.c2;
		}

		// Token: 0x06001A8D RID: 6797 RVA: 0x000487B3 File Offset: 0x000469B3
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int3x3(float v)
		{
			this.c0 = (int3)v;
			this.c1 = (int3)v;
			this.c2 = (int3)v;
		}

		// Token: 0x06001A8E RID: 6798 RVA: 0x000487D9 File Offset: 0x000469D9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int3x3(float3x3 v)
		{
			this.c0 = (int3)v.c0;
			this.c1 = (int3)v.c1;
			this.c2 = (int3)v.c2;
		}

		// Token: 0x06001A8F RID: 6799 RVA: 0x0004880E File Offset: 0x00046A0E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int3x3(double v)
		{
			this.c0 = (int3)v;
			this.c1 = (int3)v;
			this.c2 = (int3)v;
		}

		// Token: 0x06001A90 RID: 6800 RVA: 0x00048834 File Offset: 0x00046A34
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int3x3(double3x3 v)
		{
			this.c0 = (int3)v.c0;
			this.c1 = (int3)v.c1;
			this.c2 = (int3)v.c2;
		}

		// Token: 0x06001A91 RID: 6801 RVA: 0x00048869 File Offset: 0x00046A69
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator int3x3(int v)
		{
			return new int3x3(v);
		}

		// Token: 0x06001A92 RID: 6802 RVA: 0x00048871 File Offset: 0x00046A71
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator int3x3(bool v)
		{
			return new int3x3(v);
		}

		// Token: 0x06001A93 RID: 6803 RVA: 0x00048879 File Offset: 0x00046A79
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator int3x3(bool3x3 v)
		{
			return new int3x3(v);
		}

		// Token: 0x06001A94 RID: 6804 RVA: 0x00048881 File Offset: 0x00046A81
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator int3x3(uint v)
		{
			return new int3x3(v);
		}

		// Token: 0x06001A95 RID: 6805 RVA: 0x00048889 File Offset: 0x00046A89
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator int3x3(uint3x3 v)
		{
			return new int3x3(v);
		}

		// Token: 0x06001A96 RID: 6806 RVA: 0x00048891 File Offset: 0x00046A91
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator int3x3(float v)
		{
			return new int3x3(v);
		}

		// Token: 0x06001A97 RID: 6807 RVA: 0x00048899 File Offset: 0x00046A99
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator int3x3(float3x3 v)
		{
			return new int3x3(v);
		}

		// Token: 0x06001A98 RID: 6808 RVA: 0x000488A1 File Offset: 0x00046AA1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator int3x3(double v)
		{
			return new int3x3(v);
		}

		// Token: 0x06001A99 RID: 6809 RVA: 0x000488A9 File Offset: 0x00046AA9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator int3x3(double3x3 v)
		{
			return new int3x3(v);
		}

		// Token: 0x06001A9A RID: 6810 RVA: 0x000488B1 File Offset: 0x00046AB1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3x3 operator *(int3x3 lhs, int3x3 rhs)
		{
			return new int3x3(lhs.c0 * rhs.c0, lhs.c1 * rhs.c1, lhs.c2 * rhs.c2);
		}

		// Token: 0x06001A9B RID: 6811 RVA: 0x000488EB File Offset: 0x00046AEB
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3x3 operator *(int3x3 lhs, int rhs)
		{
			return new int3x3(lhs.c0 * rhs, lhs.c1 * rhs, lhs.c2 * rhs);
		}

		// Token: 0x06001A9C RID: 6812 RVA: 0x00048916 File Offset: 0x00046B16
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3x3 operator *(int lhs, int3x3 rhs)
		{
			return new int3x3(lhs * rhs.c0, lhs * rhs.c1, lhs * rhs.c2);
		}

		// Token: 0x06001A9D RID: 6813 RVA: 0x00048941 File Offset: 0x00046B41
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3x3 operator +(int3x3 lhs, int3x3 rhs)
		{
			return new int3x3(lhs.c0 + rhs.c0, lhs.c1 + rhs.c1, lhs.c2 + rhs.c2);
		}

		// Token: 0x06001A9E RID: 6814 RVA: 0x0004897B File Offset: 0x00046B7B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3x3 operator +(int3x3 lhs, int rhs)
		{
			return new int3x3(lhs.c0 + rhs, lhs.c1 + rhs, lhs.c2 + rhs);
		}

		// Token: 0x06001A9F RID: 6815 RVA: 0x000489A6 File Offset: 0x00046BA6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3x3 operator +(int lhs, int3x3 rhs)
		{
			return new int3x3(lhs + rhs.c0, lhs + rhs.c1, lhs + rhs.c2);
		}

		// Token: 0x06001AA0 RID: 6816 RVA: 0x000489D1 File Offset: 0x00046BD1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3x3 operator -(int3x3 lhs, int3x3 rhs)
		{
			return new int3x3(lhs.c0 - rhs.c0, lhs.c1 - rhs.c1, lhs.c2 - rhs.c2);
		}

		// Token: 0x06001AA1 RID: 6817 RVA: 0x00048A0B File Offset: 0x00046C0B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3x3 operator -(int3x3 lhs, int rhs)
		{
			return new int3x3(lhs.c0 - rhs, lhs.c1 - rhs, lhs.c2 - rhs);
		}

		// Token: 0x06001AA2 RID: 6818 RVA: 0x00048A36 File Offset: 0x00046C36
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3x3 operator -(int lhs, int3x3 rhs)
		{
			return new int3x3(lhs - rhs.c0, lhs - rhs.c1, lhs - rhs.c2);
		}

		// Token: 0x06001AA3 RID: 6819 RVA: 0x00048A61 File Offset: 0x00046C61
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3x3 operator /(int3x3 lhs, int3x3 rhs)
		{
			return new int3x3(lhs.c0 / rhs.c0, lhs.c1 / rhs.c1, lhs.c2 / rhs.c2);
		}

		// Token: 0x06001AA4 RID: 6820 RVA: 0x00048A9B File Offset: 0x00046C9B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3x3 operator /(int3x3 lhs, int rhs)
		{
			return new int3x3(lhs.c0 / rhs, lhs.c1 / rhs, lhs.c2 / rhs);
		}

		// Token: 0x06001AA5 RID: 6821 RVA: 0x00048AC6 File Offset: 0x00046CC6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3x3 operator /(int lhs, int3x3 rhs)
		{
			return new int3x3(lhs / rhs.c0, lhs / rhs.c1, lhs / rhs.c2);
		}

		// Token: 0x06001AA6 RID: 6822 RVA: 0x00048AF1 File Offset: 0x00046CF1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3x3 operator %(int3x3 lhs, int3x3 rhs)
		{
			return new int3x3(lhs.c0 % rhs.c0, lhs.c1 % rhs.c1, lhs.c2 % rhs.c2);
		}

		// Token: 0x06001AA7 RID: 6823 RVA: 0x00048B2B File Offset: 0x00046D2B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3x3 operator %(int3x3 lhs, int rhs)
		{
			return new int3x3(lhs.c0 % rhs, lhs.c1 % rhs, lhs.c2 % rhs);
		}

		// Token: 0x06001AA8 RID: 6824 RVA: 0x00048B56 File Offset: 0x00046D56
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3x3 operator %(int lhs, int3x3 rhs)
		{
			return new int3x3(lhs % rhs.c0, lhs % rhs.c1, lhs % rhs.c2);
		}

		// Token: 0x06001AA9 RID: 6825 RVA: 0x00048B84 File Offset: 0x00046D84
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3x3 operator ++(int3x3 val)
		{
			int3 @int = ++val.c0;
			val.c0 = @int;
			int3 int2 = @int;
			@int = ++val.c1;
			val.c1 = @int;
			int3 int3 = @int;
			@int = ++val.c2;
			val.c2 = @int;
			return new int3x3(int2, int3, @int);
		}

		// Token: 0x06001AAA RID: 6826 RVA: 0x00048BE4 File Offset: 0x00046DE4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3x3 operator --(int3x3 val)
		{
			int3 @int = --val.c0;
			val.c0 = @int;
			int3 int2 = @int;
			@int = --val.c1;
			val.c1 = @int;
			int3 int3 = @int;
			@int = --val.c2;
			val.c2 = @int;
			return new int3x3(int2, int3, @int);
		}

		// Token: 0x06001AAB RID: 6827 RVA: 0x00048C44 File Offset: 0x00046E44
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x3 operator <(int3x3 lhs, int3x3 rhs)
		{
			return new bool3x3(lhs.c0 < rhs.c0, lhs.c1 < rhs.c1, lhs.c2 < rhs.c2);
		}

		// Token: 0x06001AAC RID: 6828 RVA: 0x00048C7E File Offset: 0x00046E7E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x3 operator <(int3x3 lhs, int rhs)
		{
			return new bool3x3(lhs.c0 < rhs, lhs.c1 < rhs, lhs.c2 < rhs);
		}

		// Token: 0x06001AAD RID: 6829 RVA: 0x00048CA9 File Offset: 0x00046EA9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x3 operator <(int lhs, int3x3 rhs)
		{
			return new bool3x3(lhs < rhs.c0, lhs < rhs.c1, lhs < rhs.c2);
		}

		// Token: 0x06001AAE RID: 6830 RVA: 0x00048CD4 File Offset: 0x00046ED4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x3 operator <=(int3x3 lhs, int3x3 rhs)
		{
			return new bool3x3(lhs.c0 <= rhs.c0, lhs.c1 <= rhs.c1, lhs.c2 <= rhs.c2);
		}

		// Token: 0x06001AAF RID: 6831 RVA: 0x00048D0E File Offset: 0x00046F0E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x3 operator <=(int3x3 lhs, int rhs)
		{
			return new bool3x3(lhs.c0 <= rhs, lhs.c1 <= rhs, lhs.c2 <= rhs);
		}

		// Token: 0x06001AB0 RID: 6832 RVA: 0x00048D39 File Offset: 0x00046F39
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x3 operator <=(int lhs, int3x3 rhs)
		{
			return new bool3x3(lhs <= rhs.c0, lhs <= rhs.c1, lhs <= rhs.c2);
		}

		// Token: 0x06001AB1 RID: 6833 RVA: 0x00048D64 File Offset: 0x00046F64
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x3 operator >(int3x3 lhs, int3x3 rhs)
		{
			return new bool3x3(lhs.c0 > rhs.c0, lhs.c1 > rhs.c1, lhs.c2 > rhs.c2);
		}

		// Token: 0x06001AB2 RID: 6834 RVA: 0x00048D9E File Offset: 0x00046F9E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x3 operator >(int3x3 lhs, int rhs)
		{
			return new bool3x3(lhs.c0 > rhs, lhs.c1 > rhs, lhs.c2 > rhs);
		}

		// Token: 0x06001AB3 RID: 6835 RVA: 0x00048DC9 File Offset: 0x00046FC9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x3 operator >(int lhs, int3x3 rhs)
		{
			return new bool3x3(lhs > rhs.c0, lhs > rhs.c1, lhs > rhs.c2);
		}

		// Token: 0x06001AB4 RID: 6836 RVA: 0x00048DF4 File Offset: 0x00046FF4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x3 operator >=(int3x3 lhs, int3x3 rhs)
		{
			return new bool3x3(lhs.c0 >= rhs.c0, lhs.c1 >= rhs.c1, lhs.c2 >= rhs.c2);
		}

		// Token: 0x06001AB5 RID: 6837 RVA: 0x00048E2E File Offset: 0x0004702E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x3 operator >=(int3x3 lhs, int rhs)
		{
			return new bool3x3(lhs.c0 >= rhs, lhs.c1 >= rhs, lhs.c2 >= rhs);
		}

		// Token: 0x06001AB6 RID: 6838 RVA: 0x00048E59 File Offset: 0x00047059
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x3 operator >=(int lhs, int3x3 rhs)
		{
			return new bool3x3(lhs >= rhs.c0, lhs >= rhs.c1, lhs >= rhs.c2);
		}

		// Token: 0x06001AB7 RID: 6839 RVA: 0x00048E84 File Offset: 0x00047084
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3x3 operator -(int3x3 val)
		{
			return new int3x3(-val.c0, -val.c1, -val.c2);
		}

		// Token: 0x06001AB8 RID: 6840 RVA: 0x00048EAC File Offset: 0x000470AC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3x3 operator +(int3x3 val)
		{
			return new int3x3(+val.c0, +val.c1, +val.c2);
		}

		// Token: 0x06001AB9 RID: 6841 RVA: 0x00048ED4 File Offset: 0x000470D4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3x3 operator <<(int3x3 x, int n)
		{
			return new int3x3(x.c0 << n, x.c1 << n, x.c2 << n);
		}

		// Token: 0x06001ABA RID: 6842 RVA: 0x00048EFF File Offset: 0x000470FF
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3x3 operator >>(int3x3 x, int n)
		{
			return new int3x3(x.c0 >> n, x.c1 >> n, x.c2 >> n);
		}

		// Token: 0x06001ABB RID: 6843 RVA: 0x00048F2A File Offset: 0x0004712A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x3 operator ==(int3x3 lhs, int3x3 rhs)
		{
			return new bool3x3(lhs.c0 == rhs.c0, lhs.c1 == rhs.c1, lhs.c2 == rhs.c2);
		}

		// Token: 0x06001ABC RID: 6844 RVA: 0x00048F64 File Offset: 0x00047164
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x3 operator ==(int3x3 lhs, int rhs)
		{
			return new bool3x3(lhs.c0 == rhs, lhs.c1 == rhs, lhs.c2 == rhs);
		}

		// Token: 0x06001ABD RID: 6845 RVA: 0x00048F8F File Offset: 0x0004718F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x3 operator ==(int lhs, int3x3 rhs)
		{
			return new bool3x3(lhs == rhs.c0, lhs == rhs.c1, lhs == rhs.c2);
		}

		// Token: 0x06001ABE RID: 6846 RVA: 0x00048FBA File Offset: 0x000471BA
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x3 operator !=(int3x3 lhs, int3x3 rhs)
		{
			return new bool3x3(lhs.c0 != rhs.c0, lhs.c1 != rhs.c1, lhs.c2 != rhs.c2);
		}

		// Token: 0x06001ABF RID: 6847 RVA: 0x00048FF4 File Offset: 0x000471F4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x3 operator !=(int3x3 lhs, int rhs)
		{
			return new bool3x3(lhs.c0 != rhs, lhs.c1 != rhs, lhs.c2 != rhs);
		}

		// Token: 0x06001AC0 RID: 6848 RVA: 0x0004901F File Offset: 0x0004721F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x3 operator !=(int lhs, int3x3 rhs)
		{
			return new bool3x3(lhs != rhs.c0, lhs != rhs.c1, lhs != rhs.c2);
		}

		// Token: 0x06001AC1 RID: 6849 RVA: 0x0004904A File Offset: 0x0004724A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3x3 operator ~(int3x3 val)
		{
			return new int3x3(~val.c0, ~val.c1, ~val.c2);
		}

		// Token: 0x06001AC2 RID: 6850 RVA: 0x00049072 File Offset: 0x00047272
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3x3 operator &(int3x3 lhs, int3x3 rhs)
		{
			return new int3x3(lhs.c0 & rhs.c0, lhs.c1 & rhs.c1, lhs.c2 & rhs.c2);
		}

		// Token: 0x06001AC3 RID: 6851 RVA: 0x000490AC File Offset: 0x000472AC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3x3 operator &(int3x3 lhs, int rhs)
		{
			return new int3x3(lhs.c0 & rhs, lhs.c1 & rhs, lhs.c2 & rhs);
		}

		// Token: 0x06001AC4 RID: 6852 RVA: 0x000490D7 File Offset: 0x000472D7
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3x3 operator &(int lhs, int3x3 rhs)
		{
			return new int3x3(lhs & rhs.c0, lhs & rhs.c1, lhs & rhs.c2);
		}

		// Token: 0x06001AC5 RID: 6853 RVA: 0x00049102 File Offset: 0x00047302
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3x3 operator |(int3x3 lhs, int3x3 rhs)
		{
			return new int3x3(lhs.c0 | rhs.c0, lhs.c1 | rhs.c1, lhs.c2 | rhs.c2);
		}

		// Token: 0x06001AC6 RID: 6854 RVA: 0x0004913C File Offset: 0x0004733C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3x3 operator |(int3x3 lhs, int rhs)
		{
			return new int3x3(lhs.c0 | rhs, lhs.c1 | rhs, lhs.c2 | rhs);
		}

		// Token: 0x06001AC7 RID: 6855 RVA: 0x00049167 File Offset: 0x00047367
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3x3 operator |(int lhs, int3x3 rhs)
		{
			return new int3x3(lhs | rhs.c0, lhs | rhs.c1, lhs | rhs.c2);
		}

		// Token: 0x06001AC8 RID: 6856 RVA: 0x00049192 File Offset: 0x00047392
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3x3 operator ^(int3x3 lhs, int3x3 rhs)
		{
			return new int3x3(lhs.c0 ^ rhs.c0, lhs.c1 ^ rhs.c1, lhs.c2 ^ rhs.c2);
		}

		// Token: 0x06001AC9 RID: 6857 RVA: 0x000491CC File Offset: 0x000473CC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3x3 operator ^(int3x3 lhs, int rhs)
		{
			return new int3x3(lhs.c0 ^ rhs, lhs.c1 ^ rhs, lhs.c2 ^ rhs);
		}

		// Token: 0x06001ACA RID: 6858 RVA: 0x000491F7 File Offset: 0x000473F7
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3x3 operator ^(int lhs, int3x3 rhs)
		{
			return new int3x3(lhs ^ rhs.c0, lhs ^ rhs.c1, lhs ^ rhs.c2);
		}

		// Token: 0x17000847 RID: 2119
		public unsafe int3 this[int index]
		{
			get
			{
				fixed (int3x3* ptr = &this)
				{
					return ref *(int3*)(ptr + (IntPtr)index * (IntPtr)sizeof(int3) / (IntPtr)sizeof(int3x3));
				}
			}
		}

		// Token: 0x06001ACC RID: 6860 RVA: 0x0004923F File Offset: 0x0004743F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool Equals(int3x3 rhs)
		{
			return this.c0.Equals(rhs.c0) && this.c1.Equals(rhs.c1) && this.c2.Equals(rhs.c2);
		}

		// Token: 0x06001ACD RID: 6861 RVA: 0x0004927C File Offset: 0x0004747C
		public override bool Equals(object o)
		{
			if (o is int3x3)
			{
				int3x3 rhs = (int3x3)o;
				return this.Equals(rhs);
			}
			return false;
		}

		// Token: 0x06001ACE RID: 6862 RVA: 0x000492A1 File Offset: 0x000474A1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override int GetHashCode()
		{
			return (int)math.hash(this);
		}

		// Token: 0x06001ACF RID: 6863 RVA: 0x000492B0 File Offset: 0x000474B0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override string ToString()
		{
			return string.Format("int3x3({0}, {1}, {2},  {3}, {4}, {5},  {6}, {7}, {8})", new object[]
			{
				this.c0.x,
				this.c1.x,
				this.c2.x,
				this.c0.y,
				this.c1.y,
				this.c2.y,
				this.c0.z,
				this.c1.z,
				this.c2.z
			});
		}

		// Token: 0x06001AD0 RID: 6864 RVA: 0x0004937C File Offset: 0x0004757C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public string ToString(string format, IFormatProvider formatProvider)
		{
			return string.Format("int3x3({0}, {1}, {2},  {3}, {4}, {5},  {6}, {7}, {8})", new object[]
			{
				this.c0.x.ToString(format, formatProvider),
				this.c1.x.ToString(format, formatProvider),
				this.c2.x.ToString(format, formatProvider),
				this.c0.y.ToString(format, formatProvider),
				this.c1.y.ToString(format, formatProvider),
				this.c2.y.ToString(format, formatProvider),
				this.c0.z.ToString(format, formatProvider),
				this.c1.z.ToString(format, formatProvider),
				this.c2.z.ToString(format, formatProvider)
			});
		}

		// Token: 0x06001AD1 RID: 6865 RVA: 0x00049458 File Offset: 0x00047658
		// Note: this type is marked as 'beforefieldinit'.
		static int3x3()
		{
		}

		// Token: 0x040000C6 RID: 198
		public int3 c0;

		// Token: 0x040000C7 RID: 199
		public int3 c1;

		// Token: 0x040000C8 RID: 200
		public int3 c2;

		// Token: 0x040000C9 RID: 201
		public static readonly int3x3 identity = new int3x3(1, 0, 0, 0, 1, 0, 0, 0, 1);

		// Token: 0x040000CA RID: 202
		public static readonly int3x3 zero;
	}
}

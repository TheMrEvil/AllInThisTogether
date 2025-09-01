using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Unity.IL2CPP.CompilerServices;

namespace Unity.Mathematics
{
	// Token: 0x0200003E RID: 62
	[DebuggerTypeProxy(typeof(uint2.DebuggerProxy))]
	[Il2CppEagerStaticClassConstruction]
	[Serializable]
	public struct uint2 : IEquatable<uint2>, IFormattable
	{
		// Token: 0x06001E86 RID: 7814 RVA: 0x00058979 File Offset: 0x00056B79
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public uint2(uint x, uint y)
		{
			this.x = x;
			this.y = y;
		}

		// Token: 0x06001E87 RID: 7815 RVA: 0x00058989 File Offset: 0x00056B89
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public uint2(uint2 xy)
		{
			this.x = xy.x;
			this.y = xy.y;
		}

		// Token: 0x06001E88 RID: 7816 RVA: 0x000589A3 File Offset: 0x00056BA3
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public uint2(uint v)
		{
			this.x = v;
			this.y = v;
		}

		// Token: 0x06001E89 RID: 7817 RVA: 0x000589B3 File Offset: 0x00056BB3
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public uint2(bool v)
		{
			this.x = (v ? 1U : 0U);
			this.y = (v ? 1U : 0U);
		}

		// Token: 0x06001E8A RID: 7818 RVA: 0x000589CF File Offset: 0x00056BCF
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public uint2(bool2 v)
		{
			this.x = (v.x ? 1U : 0U);
			this.y = (v.y ? 1U : 0U);
		}

		// Token: 0x06001E8B RID: 7819 RVA: 0x000589F5 File Offset: 0x00056BF5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public uint2(int v)
		{
			this.x = (uint)v;
			this.y = (uint)v;
		}

		// Token: 0x06001E8C RID: 7820 RVA: 0x00058A05 File Offset: 0x00056C05
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public uint2(int2 v)
		{
			this.x = (uint)v.x;
			this.y = (uint)v.y;
		}

		// Token: 0x06001E8D RID: 7821 RVA: 0x00058A1F File Offset: 0x00056C1F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public uint2(float v)
		{
			this.x = (uint)v;
			this.y = (uint)v;
		}

		// Token: 0x06001E8E RID: 7822 RVA: 0x00058A31 File Offset: 0x00056C31
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public uint2(float2 v)
		{
			this.x = (uint)v.x;
			this.y = (uint)v.y;
		}

		// Token: 0x06001E8F RID: 7823 RVA: 0x00058A4D File Offset: 0x00056C4D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public uint2(double v)
		{
			this.x = (uint)v;
			this.y = (uint)v;
		}

		// Token: 0x06001E90 RID: 7824 RVA: 0x00058A5F File Offset: 0x00056C5F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public uint2(double2 v)
		{
			this.x = (uint)v.x;
			this.y = (uint)v.y;
		}

		// Token: 0x06001E91 RID: 7825 RVA: 0x00058A7B File Offset: 0x00056C7B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator uint2(uint v)
		{
			return new uint2(v);
		}

		// Token: 0x06001E92 RID: 7826 RVA: 0x00058A83 File Offset: 0x00056C83
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator uint2(bool v)
		{
			return new uint2(v);
		}

		// Token: 0x06001E93 RID: 7827 RVA: 0x00058A8B File Offset: 0x00056C8B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator uint2(bool2 v)
		{
			return new uint2(v);
		}

		// Token: 0x06001E94 RID: 7828 RVA: 0x00058A93 File Offset: 0x00056C93
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator uint2(int v)
		{
			return new uint2(v);
		}

		// Token: 0x06001E95 RID: 7829 RVA: 0x00058A9B File Offset: 0x00056C9B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator uint2(int2 v)
		{
			return new uint2(v);
		}

		// Token: 0x06001E96 RID: 7830 RVA: 0x00058AA3 File Offset: 0x00056CA3
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator uint2(float v)
		{
			return new uint2(v);
		}

		// Token: 0x06001E97 RID: 7831 RVA: 0x00058AAB File Offset: 0x00056CAB
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator uint2(float2 v)
		{
			return new uint2(v);
		}

		// Token: 0x06001E98 RID: 7832 RVA: 0x00058AB3 File Offset: 0x00056CB3
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator uint2(double v)
		{
			return new uint2(v);
		}

		// Token: 0x06001E99 RID: 7833 RVA: 0x00058ABB File Offset: 0x00056CBB
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator uint2(double2 v)
		{
			return new uint2(v);
		}

		// Token: 0x06001E9A RID: 7834 RVA: 0x00058AC3 File Offset: 0x00056CC3
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2 operator *(uint2 lhs, uint2 rhs)
		{
			return new uint2(lhs.x * rhs.x, lhs.y * rhs.y);
		}

		// Token: 0x06001E9B RID: 7835 RVA: 0x00058AE4 File Offset: 0x00056CE4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2 operator *(uint2 lhs, uint rhs)
		{
			return new uint2(lhs.x * rhs, lhs.y * rhs);
		}

		// Token: 0x06001E9C RID: 7836 RVA: 0x00058AFB File Offset: 0x00056CFB
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2 operator *(uint lhs, uint2 rhs)
		{
			return new uint2(lhs * rhs.x, lhs * rhs.y);
		}

		// Token: 0x06001E9D RID: 7837 RVA: 0x00058B12 File Offset: 0x00056D12
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2 operator +(uint2 lhs, uint2 rhs)
		{
			return new uint2(lhs.x + rhs.x, lhs.y + rhs.y);
		}

		// Token: 0x06001E9E RID: 7838 RVA: 0x00058B33 File Offset: 0x00056D33
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2 operator +(uint2 lhs, uint rhs)
		{
			return new uint2(lhs.x + rhs, lhs.y + rhs);
		}

		// Token: 0x06001E9F RID: 7839 RVA: 0x00058B4A File Offset: 0x00056D4A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2 operator +(uint lhs, uint2 rhs)
		{
			return new uint2(lhs + rhs.x, lhs + rhs.y);
		}

		// Token: 0x06001EA0 RID: 7840 RVA: 0x00058B61 File Offset: 0x00056D61
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2 operator -(uint2 lhs, uint2 rhs)
		{
			return new uint2(lhs.x - rhs.x, lhs.y - rhs.y);
		}

		// Token: 0x06001EA1 RID: 7841 RVA: 0x00058B82 File Offset: 0x00056D82
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2 operator -(uint2 lhs, uint rhs)
		{
			return new uint2(lhs.x - rhs, lhs.y - rhs);
		}

		// Token: 0x06001EA2 RID: 7842 RVA: 0x00058B99 File Offset: 0x00056D99
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2 operator -(uint lhs, uint2 rhs)
		{
			return new uint2(lhs - rhs.x, lhs - rhs.y);
		}

		// Token: 0x06001EA3 RID: 7843 RVA: 0x00058BB0 File Offset: 0x00056DB0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2 operator /(uint2 lhs, uint2 rhs)
		{
			return new uint2(lhs.x / rhs.x, lhs.y / rhs.y);
		}

		// Token: 0x06001EA4 RID: 7844 RVA: 0x00058BD1 File Offset: 0x00056DD1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2 operator /(uint2 lhs, uint rhs)
		{
			return new uint2(lhs.x / rhs, lhs.y / rhs);
		}

		// Token: 0x06001EA5 RID: 7845 RVA: 0x00058BE8 File Offset: 0x00056DE8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2 operator /(uint lhs, uint2 rhs)
		{
			return new uint2(lhs / rhs.x, lhs / rhs.y);
		}

		// Token: 0x06001EA6 RID: 7846 RVA: 0x00058BFF File Offset: 0x00056DFF
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2 operator %(uint2 lhs, uint2 rhs)
		{
			return new uint2(lhs.x % rhs.x, lhs.y % rhs.y);
		}

		// Token: 0x06001EA7 RID: 7847 RVA: 0x00058C20 File Offset: 0x00056E20
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2 operator %(uint2 lhs, uint rhs)
		{
			return new uint2(lhs.x % rhs, lhs.y % rhs);
		}

		// Token: 0x06001EA8 RID: 7848 RVA: 0x00058C37 File Offset: 0x00056E37
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2 operator %(uint lhs, uint2 rhs)
		{
			return new uint2(lhs % rhs.x, lhs % rhs.y);
		}

		// Token: 0x06001EA9 RID: 7849 RVA: 0x00058C50 File Offset: 0x00056E50
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2 operator ++(uint2 val)
		{
			uint num = val.x + 1U;
			val.x = num;
			uint num2 = num;
			num = val.y + 1U;
			val.y = num;
			return new uint2(num2, num);
		}

		// Token: 0x06001EAA RID: 7850 RVA: 0x00058C80 File Offset: 0x00056E80
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2 operator --(uint2 val)
		{
			uint num = val.x - 1U;
			val.x = num;
			uint num2 = num;
			num = val.y - 1U;
			val.y = num;
			return new uint2(num2, num);
		}

		// Token: 0x06001EAB RID: 7851 RVA: 0x00058CB0 File Offset: 0x00056EB0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2 operator <(uint2 lhs, uint2 rhs)
		{
			return new bool2(lhs.x < rhs.x, lhs.y < rhs.y);
		}

		// Token: 0x06001EAC RID: 7852 RVA: 0x00058CD3 File Offset: 0x00056ED3
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2 operator <(uint2 lhs, uint rhs)
		{
			return new bool2(lhs.x < rhs, lhs.y < rhs);
		}

		// Token: 0x06001EAD RID: 7853 RVA: 0x00058CEC File Offset: 0x00056EEC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2 operator <(uint lhs, uint2 rhs)
		{
			return new bool2(lhs < rhs.x, lhs < rhs.y);
		}

		// Token: 0x06001EAE RID: 7854 RVA: 0x00058D05 File Offset: 0x00056F05
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2 operator <=(uint2 lhs, uint2 rhs)
		{
			return new bool2(lhs.x <= rhs.x, lhs.y <= rhs.y);
		}

		// Token: 0x06001EAF RID: 7855 RVA: 0x00058D2E File Offset: 0x00056F2E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2 operator <=(uint2 lhs, uint rhs)
		{
			return new bool2(lhs.x <= rhs, lhs.y <= rhs);
		}

		// Token: 0x06001EB0 RID: 7856 RVA: 0x00058D4D File Offset: 0x00056F4D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2 operator <=(uint lhs, uint2 rhs)
		{
			return new bool2(lhs <= rhs.x, lhs <= rhs.y);
		}

		// Token: 0x06001EB1 RID: 7857 RVA: 0x00058D6C File Offset: 0x00056F6C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2 operator >(uint2 lhs, uint2 rhs)
		{
			return new bool2(lhs.x > rhs.x, lhs.y > rhs.y);
		}

		// Token: 0x06001EB2 RID: 7858 RVA: 0x00058D8F File Offset: 0x00056F8F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2 operator >(uint2 lhs, uint rhs)
		{
			return new bool2(lhs.x > rhs, lhs.y > rhs);
		}

		// Token: 0x06001EB3 RID: 7859 RVA: 0x00058DA8 File Offset: 0x00056FA8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2 operator >(uint lhs, uint2 rhs)
		{
			return new bool2(lhs > rhs.x, lhs > rhs.y);
		}

		// Token: 0x06001EB4 RID: 7860 RVA: 0x00058DC1 File Offset: 0x00056FC1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2 operator >=(uint2 lhs, uint2 rhs)
		{
			return new bool2(lhs.x >= rhs.x, lhs.y >= rhs.y);
		}

		// Token: 0x06001EB5 RID: 7861 RVA: 0x00058DEA File Offset: 0x00056FEA
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2 operator >=(uint2 lhs, uint rhs)
		{
			return new bool2(lhs.x >= rhs, lhs.y >= rhs);
		}

		// Token: 0x06001EB6 RID: 7862 RVA: 0x00058E09 File Offset: 0x00057009
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2 operator >=(uint lhs, uint2 rhs)
		{
			return new bool2(lhs >= rhs.x, lhs >= rhs.y);
		}

		// Token: 0x06001EB7 RID: 7863 RVA: 0x00058E28 File Offset: 0x00057028
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2 operator -(uint2 val)
		{
			return new uint2((uint)(-(uint)((ulong)val.x)), (uint)(-(uint)((ulong)val.y)));
		}

		// Token: 0x06001EB8 RID: 7864 RVA: 0x00058E41 File Offset: 0x00057041
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2 operator +(uint2 val)
		{
			return new uint2(val.x, val.y);
		}

		// Token: 0x06001EB9 RID: 7865 RVA: 0x00058E54 File Offset: 0x00057054
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2 operator <<(uint2 x, int n)
		{
			return new uint2(x.x << n, x.y << n);
		}

		// Token: 0x06001EBA RID: 7866 RVA: 0x00058E71 File Offset: 0x00057071
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2 operator >>(uint2 x, int n)
		{
			return new uint2(x.x >> n, x.y >> n);
		}

		// Token: 0x06001EBB RID: 7867 RVA: 0x00058E8E File Offset: 0x0005708E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2 operator ==(uint2 lhs, uint2 rhs)
		{
			return new bool2(lhs.x == rhs.x, lhs.y == rhs.y);
		}

		// Token: 0x06001EBC RID: 7868 RVA: 0x00058EB1 File Offset: 0x000570B1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2 operator ==(uint2 lhs, uint rhs)
		{
			return new bool2(lhs.x == rhs, lhs.y == rhs);
		}

		// Token: 0x06001EBD RID: 7869 RVA: 0x00058ECA File Offset: 0x000570CA
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2 operator ==(uint lhs, uint2 rhs)
		{
			return new bool2(lhs == rhs.x, lhs == rhs.y);
		}

		// Token: 0x06001EBE RID: 7870 RVA: 0x00058EE3 File Offset: 0x000570E3
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2 operator !=(uint2 lhs, uint2 rhs)
		{
			return new bool2(lhs.x != rhs.x, lhs.y != rhs.y);
		}

		// Token: 0x06001EBF RID: 7871 RVA: 0x00058F0C File Offset: 0x0005710C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2 operator !=(uint2 lhs, uint rhs)
		{
			return new bool2(lhs.x != rhs, lhs.y != rhs);
		}

		// Token: 0x06001EC0 RID: 7872 RVA: 0x00058F2B File Offset: 0x0005712B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2 operator !=(uint lhs, uint2 rhs)
		{
			return new bool2(lhs != rhs.x, lhs != rhs.y);
		}

		// Token: 0x06001EC1 RID: 7873 RVA: 0x00058F4A File Offset: 0x0005714A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2 operator ~(uint2 val)
		{
			return new uint2(~val.x, ~val.y);
		}

		// Token: 0x06001EC2 RID: 7874 RVA: 0x00058F5F File Offset: 0x0005715F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2 operator &(uint2 lhs, uint2 rhs)
		{
			return new uint2(lhs.x & rhs.x, lhs.y & rhs.y);
		}

		// Token: 0x06001EC3 RID: 7875 RVA: 0x00058F80 File Offset: 0x00057180
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2 operator &(uint2 lhs, uint rhs)
		{
			return new uint2(lhs.x & rhs, lhs.y & rhs);
		}

		// Token: 0x06001EC4 RID: 7876 RVA: 0x00058F97 File Offset: 0x00057197
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2 operator &(uint lhs, uint2 rhs)
		{
			return new uint2(lhs & rhs.x, lhs & rhs.y);
		}

		// Token: 0x06001EC5 RID: 7877 RVA: 0x00058FAE File Offset: 0x000571AE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2 operator |(uint2 lhs, uint2 rhs)
		{
			return new uint2(lhs.x | rhs.x, lhs.y | rhs.y);
		}

		// Token: 0x06001EC6 RID: 7878 RVA: 0x00058FCF File Offset: 0x000571CF
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2 operator |(uint2 lhs, uint rhs)
		{
			return new uint2(lhs.x | rhs, lhs.y | rhs);
		}

		// Token: 0x06001EC7 RID: 7879 RVA: 0x00058FE6 File Offset: 0x000571E6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2 operator |(uint lhs, uint2 rhs)
		{
			return new uint2(lhs | rhs.x, lhs | rhs.y);
		}

		// Token: 0x06001EC8 RID: 7880 RVA: 0x00058FFD File Offset: 0x000571FD
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2 operator ^(uint2 lhs, uint2 rhs)
		{
			return new uint2(lhs.x ^ rhs.x, lhs.y ^ rhs.y);
		}

		// Token: 0x06001EC9 RID: 7881 RVA: 0x0005901E File Offset: 0x0005721E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2 operator ^(uint2 lhs, uint rhs)
		{
			return new uint2(lhs.x ^ rhs, lhs.y ^ rhs);
		}

		// Token: 0x06001ECA RID: 7882 RVA: 0x00059035 File Offset: 0x00057235
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2 operator ^(uint lhs, uint2 rhs)
		{
			return new uint2(lhs ^ rhs.x, lhs ^ rhs.y);
		}

		// Token: 0x1700099D RID: 2461
		// (get) Token: 0x06001ECB RID: 7883 RVA: 0x0005904C File Offset: 0x0005724C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 xxxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.x, this.x, this.x, this.x);
			}
		}

		// Token: 0x1700099E RID: 2462
		// (get) Token: 0x06001ECC RID: 7884 RVA: 0x0005906B File Offset: 0x0005726B
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 xxxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.x, this.x, this.x, this.y);
			}
		}

		// Token: 0x1700099F RID: 2463
		// (get) Token: 0x06001ECD RID: 7885 RVA: 0x0005908A File Offset: 0x0005728A
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 xxyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.x, this.x, this.y, this.x);
			}
		}

		// Token: 0x170009A0 RID: 2464
		// (get) Token: 0x06001ECE RID: 7886 RVA: 0x000590A9 File Offset: 0x000572A9
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 xxyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.x, this.x, this.y, this.y);
			}
		}

		// Token: 0x170009A1 RID: 2465
		// (get) Token: 0x06001ECF RID: 7887 RVA: 0x000590C8 File Offset: 0x000572C8
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 xyxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.x, this.y, this.x, this.x);
			}
		}

		// Token: 0x170009A2 RID: 2466
		// (get) Token: 0x06001ED0 RID: 7888 RVA: 0x000590E7 File Offset: 0x000572E7
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 xyxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.x, this.y, this.x, this.y);
			}
		}

		// Token: 0x170009A3 RID: 2467
		// (get) Token: 0x06001ED1 RID: 7889 RVA: 0x00059106 File Offset: 0x00057306
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 xyyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.x, this.y, this.y, this.x);
			}
		}

		// Token: 0x170009A4 RID: 2468
		// (get) Token: 0x06001ED2 RID: 7890 RVA: 0x00059125 File Offset: 0x00057325
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 xyyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.x, this.y, this.y, this.y);
			}
		}

		// Token: 0x170009A5 RID: 2469
		// (get) Token: 0x06001ED3 RID: 7891 RVA: 0x00059144 File Offset: 0x00057344
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 yxxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.y, this.x, this.x, this.x);
			}
		}

		// Token: 0x170009A6 RID: 2470
		// (get) Token: 0x06001ED4 RID: 7892 RVA: 0x00059163 File Offset: 0x00057363
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 yxxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.y, this.x, this.x, this.y);
			}
		}

		// Token: 0x170009A7 RID: 2471
		// (get) Token: 0x06001ED5 RID: 7893 RVA: 0x00059182 File Offset: 0x00057382
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 yxyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.y, this.x, this.y, this.x);
			}
		}

		// Token: 0x170009A8 RID: 2472
		// (get) Token: 0x06001ED6 RID: 7894 RVA: 0x000591A1 File Offset: 0x000573A1
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 yxyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.y, this.x, this.y, this.y);
			}
		}

		// Token: 0x170009A9 RID: 2473
		// (get) Token: 0x06001ED7 RID: 7895 RVA: 0x000591C0 File Offset: 0x000573C0
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 yyxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.y, this.y, this.x, this.x);
			}
		}

		// Token: 0x170009AA RID: 2474
		// (get) Token: 0x06001ED8 RID: 7896 RVA: 0x000591DF File Offset: 0x000573DF
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 yyxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.y, this.y, this.x, this.y);
			}
		}

		// Token: 0x170009AB RID: 2475
		// (get) Token: 0x06001ED9 RID: 7897 RVA: 0x000591FE File Offset: 0x000573FE
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 yyyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.y, this.y, this.y, this.x);
			}
		}

		// Token: 0x170009AC RID: 2476
		// (get) Token: 0x06001EDA RID: 7898 RVA: 0x0005921D File Offset: 0x0005741D
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 yyyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.y, this.y, this.y, this.y);
			}
		}

		// Token: 0x170009AD RID: 2477
		// (get) Token: 0x06001EDB RID: 7899 RVA: 0x0005923C File Offset: 0x0005743C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint3 xxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint3(this.x, this.x, this.x);
			}
		}

		// Token: 0x170009AE RID: 2478
		// (get) Token: 0x06001EDC RID: 7900 RVA: 0x00059255 File Offset: 0x00057455
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint3 xxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint3(this.x, this.x, this.y);
			}
		}

		// Token: 0x170009AF RID: 2479
		// (get) Token: 0x06001EDD RID: 7901 RVA: 0x0005926E File Offset: 0x0005746E
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint3 xyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint3(this.x, this.y, this.x);
			}
		}

		// Token: 0x170009B0 RID: 2480
		// (get) Token: 0x06001EDE RID: 7902 RVA: 0x00059287 File Offset: 0x00057487
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint3 xyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint3(this.x, this.y, this.y);
			}
		}

		// Token: 0x170009B1 RID: 2481
		// (get) Token: 0x06001EDF RID: 7903 RVA: 0x000592A0 File Offset: 0x000574A0
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint3 yxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint3(this.y, this.x, this.x);
			}
		}

		// Token: 0x170009B2 RID: 2482
		// (get) Token: 0x06001EE0 RID: 7904 RVA: 0x000592B9 File Offset: 0x000574B9
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint3 yxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint3(this.y, this.x, this.y);
			}
		}

		// Token: 0x170009B3 RID: 2483
		// (get) Token: 0x06001EE1 RID: 7905 RVA: 0x000592D2 File Offset: 0x000574D2
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint3 yyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint3(this.y, this.y, this.x);
			}
		}

		// Token: 0x170009B4 RID: 2484
		// (get) Token: 0x06001EE2 RID: 7906 RVA: 0x000592EB File Offset: 0x000574EB
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint3 yyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint3(this.y, this.y, this.y);
			}
		}

		// Token: 0x170009B5 RID: 2485
		// (get) Token: 0x06001EE3 RID: 7907 RVA: 0x00059304 File Offset: 0x00057504
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint2 xx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint2(this.x, this.x);
			}
		}

		// Token: 0x170009B6 RID: 2486
		// (get) Token: 0x06001EE4 RID: 7908 RVA: 0x00059317 File Offset: 0x00057517
		// (set) Token: 0x06001EE5 RID: 7909 RVA: 0x0005932A File Offset: 0x0005752A
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint2 xy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint2(this.x, this.y);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.x = value.x;
				this.y = value.y;
			}
		}

		// Token: 0x170009B7 RID: 2487
		// (get) Token: 0x06001EE6 RID: 7910 RVA: 0x00059344 File Offset: 0x00057544
		// (set) Token: 0x06001EE7 RID: 7911 RVA: 0x00059357 File Offset: 0x00057557
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint2 yx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint2(this.y, this.x);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.y = value.x;
				this.x = value.y;
			}
		}

		// Token: 0x170009B8 RID: 2488
		// (get) Token: 0x06001EE8 RID: 7912 RVA: 0x00059371 File Offset: 0x00057571
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint2 yy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint2(this.y, this.y);
			}
		}

		// Token: 0x170009B9 RID: 2489
		public unsafe uint this[int index]
		{
			get
			{
				fixed (uint2* ptr = &this)
				{
					return ((uint*)ptr)[index];
				}
			}
			set
			{
				fixed (uint* ptr = &this.x)
				{
					ptr[index] = value;
				}
			}
		}

		// Token: 0x06001EEB RID: 7915 RVA: 0x000593BC File Offset: 0x000575BC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool Equals(uint2 rhs)
		{
			return this.x == rhs.x && this.y == rhs.y;
		}

		// Token: 0x06001EEC RID: 7916 RVA: 0x000593DC File Offset: 0x000575DC
		public override bool Equals(object o)
		{
			if (o is uint2)
			{
				uint2 rhs = (uint2)o;
				return this.Equals(rhs);
			}
			return false;
		}

		// Token: 0x06001EED RID: 7917 RVA: 0x00059401 File Offset: 0x00057601
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override int GetHashCode()
		{
			return (int)math.hash(this);
		}

		// Token: 0x06001EEE RID: 7918 RVA: 0x0005940E File Offset: 0x0005760E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override string ToString()
		{
			return string.Format("uint2({0}, {1})", this.x, this.y);
		}

		// Token: 0x06001EEF RID: 7919 RVA: 0x00059430 File Offset: 0x00057630
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public string ToString(string format, IFormatProvider formatProvider)
		{
			return string.Format("uint2({0}, {1})", this.x.ToString(format, formatProvider), this.y.ToString(format, formatProvider));
		}

		// Token: 0x040000E8 RID: 232
		public uint x;

		// Token: 0x040000E9 RID: 233
		public uint y;

		// Token: 0x040000EA RID: 234
		public static readonly uint2 zero;

		// Token: 0x02000060 RID: 96
		internal sealed class DebuggerProxy
		{
			// Token: 0x06002476 RID: 9334 RVA: 0x00067718 File Offset: 0x00065918
			public DebuggerProxy(uint2 v)
			{
				this.x = v.x;
				this.y = v.y;
			}

			// Token: 0x04000160 RID: 352
			public uint x;

			// Token: 0x04000161 RID: 353
			public uint y;
		}
	}
}

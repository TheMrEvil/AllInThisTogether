using System;
using System.Runtime.CompilerServices;
using Unity.IL2CPP.CompilerServices;

namespace Unity.Mathematics
{
	// Token: 0x02000048 RID: 72
	[Il2CppEagerStaticClassConstruction]
	[Serializable]
	public struct uint4x3 : IEquatable<uint4x3>, IFormattable
	{
		// Token: 0x060023AC RID: 9132 RVA: 0x00064B85 File Offset: 0x00062D85
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public uint4x3(uint4 c0, uint4 c1, uint4 c2)
		{
			this.c0 = c0;
			this.c1 = c1;
			this.c2 = c2;
		}

		// Token: 0x060023AD RID: 9133 RVA: 0x00064B9C File Offset: 0x00062D9C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public uint4x3(uint m00, uint m01, uint m02, uint m10, uint m11, uint m12, uint m20, uint m21, uint m22, uint m30, uint m31, uint m32)
		{
			this.c0 = new uint4(m00, m10, m20, m30);
			this.c1 = new uint4(m01, m11, m21, m31);
			this.c2 = new uint4(m02, m12, m22, m32);
		}

		// Token: 0x060023AE RID: 9134 RVA: 0x00064BD4 File Offset: 0x00062DD4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public uint4x3(uint v)
		{
			this.c0 = v;
			this.c1 = v;
			this.c2 = v;
		}

		// Token: 0x060023AF RID: 9135 RVA: 0x00064BFC File Offset: 0x00062DFC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public uint4x3(bool v)
		{
			this.c0 = math.select(new uint4(0U), new uint4(1U), v);
			this.c1 = math.select(new uint4(0U), new uint4(1U), v);
			this.c2 = math.select(new uint4(0U), new uint4(1U), v);
		}

		// Token: 0x060023B0 RID: 9136 RVA: 0x00064C54 File Offset: 0x00062E54
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public uint4x3(bool4x3 v)
		{
			this.c0 = math.select(new uint4(0U), new uint4(1U), v.c0);
			this.c1 = math.select(new uint4(0U), new uint4(1U), v.c1);
			this.c2 = math.select(new uint4(0U), new uint4(1U), v.c2);
		}

		// Token: 0x060023B1 RID: 9137 RVA: 0x00064CB8 File Offset: 0x00062EB8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public uint4x3(int v)
		{
			this.c0 = (uint4)v;
			this.c1 = (uint4)v;
			this.c2 = (uint4)v;
		}

		// Token: 0x060023B2 RID: 9138 RVA: 0x00064CDE File Offset: 0x00062EDE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public uint4x3(int4x3 v)
		{
			this.c0 = (uint4)v.c0;
			this.c1 = (uint4)v.c1;
			this.c2 = (uint4)v.c2;
		}

		// Token: 0x060023B3 RID: 9139 RVA: 0x00064D13 File Offset: 0x00062F13
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public uint4x3(float v)
		{
			this.c0 = (uint4)v;
			this.c1 = (uint4)v;
			this.c2 = (uint4)v;
		}

		// Token: 0x060023B4 RID: 9140 RVA: 0x00064D39 File Offset: 0x00062F39
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public uint4x3(float4x3 v)
		{
			this.c0 = (uint4)v.c0;
			this.c1 = (uint4)v.c1;
			this.c2 = (uint4)v.c2;
		}

		// Token: 0x060023B5 RID: 9141 RVA: 0x00064D6E File Offset: 0x00062F6E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public uint4x3(double v)
		{
			this.c0 = (uint4)v;
			this.c1 = (uint4)v;
			this.c2 = (uint4)v;
		}

		// Token: 0x060023B6 RID: 9142 RVA: 0x00064D94 File Offset: 0x00062F94
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public uint4x3(double4x3 v)
		{
			this.c0 = (uint4)v.c0;
			this.c1 = (uint4)v.c1;
			this.c2 = (uint4)v.c2;
		}

		// Token: 0x060023B7 RID: 9143 RVA: 0x00064DC9 File Offset: 0x00062FC9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator uint4x3(uint v)
		{
			return new uint4x3(v);
		}

		// Token: 0x060023B8 RID: 9144 RVA: 0x00064DD1 File Offset: 0x00062FD1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator uint4x3(bool v)
		{
			return new uint4x3(v);
		}

		// Token: 0x060023B9 RID: 9145 RVA: 0x00064DD9 File Offset: 0x00062FD9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator uint4x3(bool4x3 v)
		{
			return new uint4x3(v);
		}

		// Token: 0x060023BA RID: 9146 RVA: 0x00064DE1 File Offset: 0x00062FE1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator uint4x3(int v)
		{
			return new uint4x3(v);
		}

		// Token: 0x060023BB RID: 9147 RVA: 0x00064DE9 File Offset: 0x00062FE9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator uint4x3(int4x3 v)
		{
			return new uint4x3(v);
		}

		// Token: 0x060023BC RID: 9148 RVA: 0x00064DF1 File Offset: 0x00062FF1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator uint4x3(float v)
		{
			return new uint4x3(v);
		}

		// Token: 0x060023BD RID: 9149 RVA: 0x00064DF9 File Offset: 0x00062FF9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator uint4x3(float4x3 v)
		{
			return new uint4x3(v);
		}

		// Token: 0x060023BE RID: 9150 RVA: 0x00064E01 File Offset: 0x00063001
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator uint4x3(double v)
		{
			return new uint4x3(v);
		}

		// Token: 0x060023BF RID: 9151 RVA: 0x00064E09 File Offset: 0x00063009
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator uint4x3(double4x3 v)
		{
			return new uint4x3(v);
		}

		// Token: 0x060023C0 RID: 9152 RVA: 0x00064E11 File Offset: 0x00063011
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4x3 operator *(uint4x3 lhs, uint4x3 rhs)
		{
			return new uint4x3(lhs.c0 * rhs.c0, lhs.c1 * rhs.c1, lhs.c2 * rhs.c2);
		}

		// Token: 0x060023C1 RID: 9153 RVA: 0x00064E4B File Offset: 0x0006304B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4x3 operator *(uint4x3 lhs, uint rhs)
		{
			return new uint4x3(lhs.c0 * rhs, lhs.c1 * rhs, lhs.c2 * rhs);
		}

		// Token: 0x060023C2 RID: 9154 RVA: 0x00064E76 File Offset: 0x00063076
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4x3 operator *(uint lhs, uint4x3 rhs)
		{
			return new uint4x3(lhs * rhs.c0, lhs * rhs.c1, lhs * rhs.c2);
		}

		// Token: 0x060023C3 RID: 9155 RVA: 0x00064EA1 File Offset: 0x000630A1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4x3 operator +(uint4x3 lhs, uint4x3 rhs)
		{
			return new uint4x3(lhs.c0 + rhs.c0, lhs.c1 + rhs.c1, lhs.c2 + rhs.c2);
		}

		// Token: 0x060023C4 RID: 9156 RVA: 0x00064EDB File Offset: 0x000630DB
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4x3 operator +(uint4x3 lhs, uint rhs)
		{
			return new uint4x3(lhs.c0 + rhs, lhs.c1 + rhs, lhs.c2 + rhs);
		}

		// Token: 0x060023C5 RID: 9157 RVA: 0x00064F06 File Offset: 0x00063106
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4x3 operator +(uint lhs, uint4x3 rhs)
		{
			return new uint4x3(lhs + rhs.c0, lhs + rhs.c1, lhs + rhs.c2);
		}

		// Token: 0x060023C6 RID: 9158 RVA: 0x00064F31 File Offset: 0x00063131
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4x3 operator -(uint4x3 lhs, uint4x3 rhs)
		{
			return new uint4x3(lhs.c0 - rhs.c0, lhs.c1 - rhs.c1, lhs.c2 - rhs.c2);
		}

		// Token: 0x060023C7 RID: 9159 RVA: 0x00064F6B File Offset: 0x0006316B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4x3 operator -(uint4x3 lhs, uint rhs)
		{
			return new uint4x3(lhs.c0 - rhs, lhs.c1 - rhs, lhs.c2 - rhs);
		}

		// Token: 0x060023C8 RID: 9160 RVA: 0x00064F96 File Offset: 0x00063196
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4x3 operator -(uint lhs, uint4x3 rhs)
		{
			return new uint4x3(lhs - rhs.c0, lhs - rhs.c1, lhs - rhs.c2);
		}

		// Token: 0x060023C9 RID: 9161 RVA: 0x00064FC1 File Offset: 0x000631C1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4x3 operator /(uint4x3 lhs, uint4x3 rhs)
		{
			return new uint4x3(lhs.c0 / rhs.c0, lhs.c1 / rhs.c1, lhs.c2 / rhs.c2);
		}

		// Token: 0x060023CA RID: 9162 RVA: 0x00064FFB File Offset: 0x000631FB
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4x3 operator /(uint4x3 lhs, uint rhs)
		{
			return new uint4x3(lhs.c0 / rhs, lhs.c1 / rhs, lhs.c2 / rhs);
		}

		// Token: 0x060023CB RID: 9163 RVA: 0x00065026 File Offset: 0x00063226
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4x3 operator /(uint lhs, uint4x3 rhs)
		{
			return new uint4x3(lhs / rhs.c0, lhs / rhs.c1, lhs / rhs.c2);
		}

		// Token: 0x060023CC RID: 9164 RVA: 0x00065051 File Offset: 0x00063251
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4x3 operator %(uint4x3 lhs, uint4x3 rhs)
		{
			return new uint4x3(lhs.c0 % rhs.c0, lhs.c1 % rhs.c1, lhs.c2 % rhs.c2);
		}

		// Token: 0x060023CD RID: 9165 RVA: 0x0006508B File Offset: 0x0006328B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4x3 operator %(uint4x3 lhs, uint rhs)
		{
			return new uint4x3(lhs.c0 % rhs, lhs.c1 % rhs, lhs.c2 % rhs);
		}

		// Token: 0x060023CE RID: 9166 RVA: 0x000650B6 File Offset: 0x000632B6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4x3 operator %(uint lhs, uint4x3 rhs)
		{
			return new uint4x3(lhs % rhs.c0, lhs % rhs.c1, lhs % rhs.c2);
		}

		// Token: 0x060023CF RID: 9167 RVA: 0x000650E4 File Offset: 0x000632E4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4x3 operator ++(uint4x3 val)
		{
			uint4 @uint = ++val.c0;
			val.c0 = @uint;
			uint4 uint2 = @uint;
			@uint = ++val.c1;
			val.c1 = @uint;
			uint4 uint3 = @uint;
			@uint = ++val.c2;
			val.c2 = @uint;
			return new uint4x3(uint2, uint3, @uint);
		}

		// Token: 0x060023D0 RID: 9168 RVA: 0x00065144 File Offset: 0x00063344
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4x3 operator --(uint4x3 val)
		{
			uint4 @uint = --val.c0;
			val.c0 = @uint;
			uint4 uint2 = @uint;
			@uint = --val.c1;
			val.c1 = @uint;
			uint4 uint3 = @uint;
			@uint = --val.c2;
			val.c2 = @uint;
			return new uint4x3(uint2, uint3, @uint);
		}

		// Token: 0x060023D1 RID: 9169 RVA: 0x000651A4 File Offset: 0x000633A4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x3 operator <(uint4x3 lhs, uint4x3 rhs)
		{
			return new bool4x3(lhs.c0 < rhs.c0, lhs.c1 < rhs.c1, lhs.c2 < rhs.c2);
		}

		// Token: 0x060023D2 RID: 9170 RVA: 0x000651DE File Offset: 0x000633DE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x3 operator <(uint4x3 lhs, uint rhs)
		{
			return new bool4x3(lhs.c0 < rhs, lhs.c1 < rhs, lhs.c2 < rhs);
		}

		// Token: 0x060023D3 RID: 9171 RVA: 0x00065209 File Offset: 0x00063409
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x3 operator <(uint lhs, uint4x3 rhs)
		{
			return new bool4x3(lhs < rhs.c0, lhs < rhs.c1, lhs < rhs.c2);
		}

		// Token: 0x060023D4 RID: 9172 RVA: 0x00065234 File Offset: 0x00063434
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x3 operator <=(uint4x3 lhs, uint4x3 rhs)
		{
			return new bool4x3(lhs.c0 <= rhs.c0, lhs.c1 <= rhs.c1, lhs.c2 <= rhs.c2);
		}

		// Token: 0x060023D5 RID: 9173 RVA: 0x0006526E File Offset: 0x0006346E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x3 operator <=(uint4x3 lhs, uint rhs)
		{
			return new bool4x3(lhs.c0 <= rhs, lhs.c1 <= rhs, lhs.c2 <= rhs);
		}

		// Token: 0x060023D6 RID: 9174 RVA: 0x00065299 File Offset: 0x00063499
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x3 operator <=(uint lhs, uint4x3 rhs)
		{
			return new bool4x3(lhs <= rhs.c0, lhs <= rhs.c1, lhs <= rhs.c2);
		}

		// Token: 0x060023D7 RID: 9175 RVA: 0x000652C4 File Offset: 0x000634C4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x3 operator >(uint4x3 lhs, uint4x3 rhs)
		{
			return new bool4x3(lhs.c0 > rhs.c0, lhs.c1 > rhs.c1, lhs.c2 > rhs.c2);
		}

		// Token: 0x060023D8 RID: 9176 RVA: 0x000652FE File Offset: 0x000634FE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x3 operator >(uint4x3 lhs, uint rhs)
		{
			return new bool4x3(lhs.c0 > rhs, lhs.c1 > rhs, lhs.c2 > rhs);
		}

		// Token: 0x060023D9 RID: 9177 RVA: 0x00065329 File Offset: 0x00063529
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x3 operator >(uint lhs, uint4x3 rhs)
		{
			return new bool4x3(lhs > rhs.c0, lhs > rhs.c1, lhs > rhs.c2);
		}

		// Token: 0x060023DA RID: 9178 RVA: 0x00065354 File Offset: 0x00063554
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x3 operator >=(uint4x3 lhs, uint4x3 rhs)
		{
			return new bool4x3(lhs.c0 >= rhs.c0, lhs.c1 >= rhs.c1, lhs.c2 >= rhs.c2);
		}

		// Token: 0x060023DB RID: 9179 RVA: 0x0006538E File Offset: 0x0006358E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x3 operator >=(uint4x3 lhs, uint rhs)
		{
			return new bool4x3(lhs.c0 >= rhs, lhs.c1 >= rhs, lhs.c2 >= rhs);
		}

		// Token: 0x060023DC RID: 9180 RVA: 0x000653B9 File Offset: 0x000635B9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x3 operator >=(uint lhs, uint4x3 rhs)
		{
			return new bool4x3(lhs >= rhs.c0, lhs >= rhs.c1, lhs >= rhs.c2);
		}

		// Token: 0x060023DD RID: 9181 RVA: 0x000653E4 File Offset: 0x000635E4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4x3 operator -(uint4x3 val)
		{
			return new uint4x3(-val.c0, -val.c1, -val.c2);
		}

		// Token: 0x060023DE RID: 9182 RVA: 0x0006540C File Offset: 0x0006360C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4x3 operator +(uint4x3 val)
		{
			return new uint4x3(+val.c0, +val.c1, +val.c2);
		}

		// Token: 0x060023DF RID: 9183 RVA: 0x00065434 File Offset: 0x00063634
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4x3 operator <<(uint4x3 x, int n)
		{
			return new uint4x3(x.c0 << n, x.c1 << n, x.c2 << n);
		}

		// Token: 0x060023E0 RID: 9184 RVA: 0x0006545F File Offset: 0x0006365F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4x3 operator >>(uint4x3 x, int n)
		{
			return new uint4x3(x.c0 >> n, x.c1 >> n, x.c2 >> n);
		}

		// Token: 0x060023E1 RID: 9185 RVA: 0x0006548A File Offset: 0x0006368A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x3 operator ==(uint4x3 lhs, uint4x3 rhs)
		{
			return new bool4x3(lhs.c0 == rhs.c0, lhs.c1 == rhs.c1, lhs.c2 == rhs.c2);
		}

		// Token: 0x060023E2 RID: 9186 RVA: 0x000654C4 File Offset: 0x000636C4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x3 operator ==(uint4x3 lhs, uint rhs)
		{
			return new bool4x3(lhs.c0 == rhs, lhs.c1 == rhs, lhs.c2 == rhs);
		}

		// Token: 0x060023E3 RID: 9187 RVA: 0x000654EF File Offset: 0x000636EF
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x3 operator ==(uint lhs, uint4x3 rhs)
		{
			return new bool4x3(lhs == rhs.c0, lhs == rhs.c1, lhs == rhs.c2);
		}

		// Token: 0x060023E4 RID: 9188 RVA: 0x0006551A File Offset: 0x0006371A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x3 operator !=(uint4x3 lhs, uint4x3 rhs)
		{
			return new bool4x3(lhs.c0 != rhs.c0, lhs.c1 != rhs.c1, lhs.c2 != rhs.c2);
		}

		// Token: 0x060023E5 RID: 9189 RVA: 0x00065554 File Offset: 0x00063754
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x3 operator !=(uint4x3 lhs, uint rhs)
		{
			return new bool4x3(lhs.c0 != rhs, lhs.c1 != rhs, lhs.c2 != rhs);
		}

		// Token: 0x060023E6 RID: 9190 RVA: 0x0006557F File Offset: 0x0006377F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x3 operator !=(uint lhs, uint4x3 rhs)
		{
			return new bool4x3(lhs != rhs.c0, lhs != rhs.c1, lhs != rhs.c2);
		}

		// Token: 0x060023E7 RID: 9191 RVA: 0x000655AA File Offset: 0x000637AA
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4x3 operator ~(uint4x3 val)
		{
			return new uint4x3(~val.c0, ~val.c1, ~val.c2);
		}

		// Token: 0x060023E8 RID: 9192 RVA: 0x000655D2 File Offset: 0x000637D2
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4x3 operator &(uint4x3 lhs, uint4x3 rhs)
		{
			return new uint4x3(lhs.c0 & rhs.c0, lhs.c1 & rhs.c1, lhs.c2 & rhs.c2);
		}

		// Token: 0x060023E9 RID: 9193 RVA: 0x0006560C File Offset: 0x0006380C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4x3 operator &(uint4x3 lhs, uint rhs)
		{
			return new uint4x3(lhs.c0 & rhs, lhs.c1 & rhs, lhs.c2 & rhs);
		}

		// Token: 0x060023EA RID: 9194 RVA: 0x00065637 File Offset: 0x00063837
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4x3 operator &(uint lhs, uint4x3 rhs)
		{
			return new uint4x3(lhs & rhs.c0, lhs & rhs.c1, lhs & rhs.c2);
		}

		// Token: 0x060023EB RID: 9195 RVA: 0x00065662 File Offset: 0x00063862
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4x3 operator |(uint4x3 lhs, uint4x3 rhs)
		{
			return new uint4x3(lhs.c0 | rhs.c0, lhs.c1 | rhs.c1, lhs.c2 | rhs.c2);
		}

		// Token: 0x060023EC RID: 9196 RVA: 0x0006569C File Offset: 0x0006389C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4x3 operator |(uint4x3 lhs, uint rhs)
		{
			return new uint4x3(lhs.c0 | rhs, lhs.c1 | rhs, lhs.c2 | rhs);
		}

		// Token: 0x060023ED RID: 9197 RVA: 0x000656C7 File Offset: 0x000638C7
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4x3 operator |(uint lhs, uint4x3 rhs)
		{
			return new uint4x3(lhs | rhs.c0, lhs | rhs.c1, lhs | rhs.c2);
		}

		// Token: 0x060023EE RID: 9198 RVA: 0x000656F2 File Offset: 0x000638F2
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4x3 operator ^(uint4x3 lhs, uint4x3 rhs)
		{
			return new uint4x3(lhs.c0 ^ rhs.c0, lhs.c1 ^ rhs.c1, lhs.c2 ^ rhs.c2);
		}

		// Token: 0x060023EF RID: 9199 RVA: 0x0006572C File Offset: 0x0006392C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4x3 operator ^(uint4x3 lhs, uint rhs)
		{
			return new uint4x3(lhs.c0 ^ rhs, lhs.c1 ^ rhs, lhs.c2 ^ rhs);
		}

		// Token: 0x060023F0 RID: 9200 RVA: 0x00065757 File Offset: 0x00063957
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4x3 operator ^(uint lhs, uint4x3 rhs)
		{
			return new uint4x3(lhs ^ rhs.c0, lhs ^ rhs.c1, lhs ^ rhs.c2);
		}

		// Token: 0x17000B88 RID: 2952
		public unsafe uint4 this[int index]
		{
			get
			{
				fixed (uint4x3* ptr = &this)
				{
					return ref *(uint4*)(ptr + (IntPtr)index * (IntPtr)sizeof(uint4) / (IntPtr)sizeof(uint4x3));
				}
			}
		}

		// Token: 0x060023F2 RID: 9202 RVA: 0x0006579F File Offset: 0x0006399F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool Equals(uint4x3 rhs)
		{
			return this.c0.Equals(rhs.c0) && this.c1.Equals(rhs.c1) && this.c2.Equals(rhs.c2);
		}

		// Token: 0x060023F3 RID: 9203 RVA: 0x000657DC File Offset: 0x000639DC
		public override bool Equals(object o)
		{
			if (o is uint4x3)
			{
				uint4x3 rhs = (uint4x3)o;
				return this.Equals(rhs);
			}
			return false;
		}

		// Token: 0x060023F4 RID: 9204 RVA: 0x00065801 File Offset: 0x00063A01
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override int GetHashCode()
		{
			return (int)math.hash(this);
		}

		// Token: 0x060023F5 RID: 9205 RVA: 0x00065810 File Offset: 0x00063A10
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override string ToString()
		{
			return string.Format("uint4x3({0}, {1}, {2},  {3}, {4}, {5},  {6}, {7}, {8},  {9}, {10}, {11})", new object[]
			{
				this.c0.x,
				this.c1.x,
				this.c2.x,
				this.c0.y,
				this.c1.y,
				this.c2.y,
				this.c0.z,
				this.c1.z,
				this.c2.z,
				this.c0.w,
				this.c1.w,
				this.c2.w
			});
		}

		// Token: 0x060023F6 RID: 9206 RVA: 0x00065918 File Offset: 0x00063B18
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public string ToString(string format, IFormatProvider formatProvider)
		{
			return string.Format("uint4x3({0}, {1}, {2},  {3}, {4}, {5},  {6}, {7}, {8},  {9}, {10}, {11})", new object[]
			{
				this.c0.x.ToString(format, formatProvider),
				this.c1.x.ToString(format, formatProvider),
				this.c2.x.ToString(format, formatProvider),
				this.c0.y.ToString(format, formatProvider),
				this.c1.y.ToString(format, formatProvider),
				this.c2.y.ToString(format, formatProvider),
				this.c0.z.ToString(format, formatProvider),
				this.c1.z.ToString(format, formatProvider),
				this.c2.z.ToString(format, formatProvider),
				this.c0.w.ToString(format, formatProvider),
				this.c1.w.ToString(format, formatProvider),
				this.c2.w.ToString(format, formatProvider)
			});
		}

		// Token: 0x04000111 RID: 273
		public uint4 c0;

		// Token: 0x04000112 RID: 274
		public uint4 c1;

		// Token: 0x04000113 RID: 275
		public uint4 c2;

		// Token: 0x04000114 RID: 276
		public static readonly uint4x3 zero;
	}
}

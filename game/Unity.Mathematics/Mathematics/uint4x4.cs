using System;
using System.Runtime.CompilerServices;
using Unity.IL2CPP.CompilerServices;

namespace Unity.Mathematics
{
	// Token: 0x02000049 RID: 73
	[Il2CppEagerStaticClassConstruction]
	[Serializable]
	public struct uint4x4 : IEquatable<uint4x4>, IFormattable
	{
		// Token: 0x060023F7 RID: 9207 RVA: 0x00065A35 File Offset: 0x00063C35
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public uint4x4(uint4 c0, uint4 c1, uint4 c2, uint4 c3)
		{
			this.c0 = c0;
			this.c1 = c1;
			this.c2 = c2;
			this.c3 = c3;
		}

		// Token: 0x060023F8 RID: 9208 RVA: 0x00065A54 File Offset: 0x00063C54
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public uint4x4(uint m00, uint m01, uint m02, uint m03, uint m10, uint m11, uint m12, uint m13, uint m20, uint m21, uint m22, uint m23, uint m30, uint m31, uint m32, uint m33)
		{
			this.c0 = new uint4(m00, m10, m20, m30);
			this.c1 = new uint4(m01, m11, m21, m31);
			this.c2 = new uint4(m02, m12, m22, m32);
			this.c3 = new uint4(m03, m13, m23, m33);
		}

		// Token: 0x060023F9 RID: 9209 RVA: 0x00065AAA File Offset: 0x00063CAA
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public uint4x4(uint v)
		{
			this.c0 = v;
			this.c1 = v;
			this.c2 = v;
			this.c3 = v;
		}

		// Token: 0x060023FA RID: 9210 RVA: 0x00065ADC File Offset: 0x00063CDC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public uint4x4(bool v)
		{
			this.c0 = math.select(new uint4(0U), new uint4(1U), v);
			this.c1 = math.select(new uint4(0U), new uint4(1U), v);
			this.c2 = math.select(new uint4(0U), new uint4(1U), v);
			this.c3 = math.select(new uint4(0U), new uint4(1U), v);
		}

		// Token: 0x060023FB RID: 9211 RVA: 0x00065B4C File Offset: 0x00063D4C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public uint4x4(bool4x4 v)
		{
			this.c0 = math.select(new uint4(0U), new uint4(1U), v.c0);
			this.c1 = math.select(new uint4(0U), new uint4(1U), v.c1);
			this.c2 = math.select(new uint4(0U), new uint4(1U), v.c2);
			this.c3 = math.select(new uint4(0U), new uint4(1U), v.c3);
		}

		// Token: 0x060023FC RID: 9212 RVA: 0x00065BCD File Offset: 0x00063DCD
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public uint4x4(int v)
		{
			this.c0 = (uint4)v;
			this.c1 = (uint4)v;
			this.c2 = (uint4)v;
			this.c3 = (uint4)v;
		}

		// Token: 0x060023FD RID: 9213 RVA: 0x00065C00 File Offset: 0x00063E00
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public uint4x4(int4x4 v)
		{
			this.c0 = (uint4)v.c0;
			this.c1 = (uint4)v.c1;
			this.c2 = (uint4)v.c2;
			this.c3 = (uint4)v.c3;
		}

		// Token: 0x060023FE RID: 9214 RVA: 0x00065C51 File Offset: 0x00063E51
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public uint4x4(float v)
		{
			this.c0 = (uint4)v;
			this.c1 = (uint4)v;
			this.c2 = (uint4)v;
			this.c3 = (uint4)v;
		}

		// Token: 0x060023FF RID: 9215 RVA: 0x00065C84 File Offset: 0x00063E84
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public uint4x4(float4x4 v)
		{
			this.c0 = (uint4)v.c0;
			this.c1 = (uint4)v.c1;
			this.c2 = (uint4)v.c2;
			this.c3 = (uint4)v.c3;
		}

		// Token: 0x06002400 RID: 9216 RVA: 0x00065CD5 File Offset: 0x00063ED5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public uint4x4(double v)
		{
			this.c0 = (uint4)v;
			this.c1 = (uint4)v;
			this.c2 = (uint4)v;
			this.c3 = (uint4)v;
		}

		// Token: 0x06002401 RID: 9217 RVA: 0x00065D08 File Offset: 0x00063F08
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public uint4x4(double4x4 v)
		{
			this.c0 = (uint4)v.c0;
			this.c1 = (uint4)v.c1;
			this.c2 = (uint4)v.c2;
			this.c3 = (uint4)v.c3;
		}

		// Token: 0x06002402 RID: 9218 RVA: 0x00065D59 File Offset: 0x00063F59
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator uint4x4(uint v)
		{
			return new uint4x4(v);
		}

		// Token: 0x06002403 RID: 9219 RVA: 0x00065D61 File Offset: 0x00063F61
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator uint4x4(bool v)
		{
			return new uint4x4(v);
		}

		// Token: 0x06002404 RID: 9220 RVA: 0x00065D69 File Offset: 0x00063F69
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator uint4x4(bool4x4 v)
		{
			return new uint4x4(v);
		}

		// Token: 0x06002405 RID: 9221 RVA: 0x00065D71 File Offset: 0x00063F71
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator uint4x4(int v)
		{
			return new uint4x4(v);
		}

		// Token: 0x06002406 RID: 9222 RVA: 0x00065D79 File Offset: 0x00063F79
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator uint4x4(int4x4 v)
		{
			return new uint4x4(v);
		}

		// Token: 0x06002407 RID: 9223 RVA: 0x00065D81 File Offset: 0x00063F81
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator uint4x4(float v)
		{
			return new uint4x4(v);
		}

		// Token: 0x06002408 RID: 9224 RVA: 0x00065D89 File Offset: 0x00063F89
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator uint4x4(float4x4 v)
		{
			return new uint4x4(v);
		}

		// Token: 0x06002409 RID: 9225 RVA: 0x00065D91 File Offset: 0x00063F91
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator uint4x4(double v)
		{
			return new uint4x4(v);
		}

		// Token: 0x0600240A RID: 9226 RVA: 0x00065D99 File Offset: 0x00063F99
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator uint4x4(double4x4 v)
		{
			return new uint4x4(v);
		}

		// Token: 0x0600240B RID: 9227 RVA: 0x00065DA4 File Offset: 0x00063FA4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4x4 operator *(uint4x4 lhs, uint4x4 rhs)
		{
			return new uint4x4(lhs.c0 * rhs.c0, lhs.c1 * rhs.c1, lhs.c2 * rhs.c2, lhs.c3 * rhs.c3);
		}

		// Token: 0x0600240C RID: 9228 RVA: 0x00065DFA File Offset: 0x00063FFA
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4x4 operator *(uint4x4 lhs, uint rhs)
		{
			return new uint4x4(lhs.c0 * rhs, lhs.c1 * rhs, lhs.c2 * rhs, lhs.c3 * rhs);
		}

		// Token: 0x0600240D RID: 9229 RVA: 0x00065E31 File Offset: 0x00064031
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4x4 operator *(uint lhs, uint4x4 rhs)
		{
			return new uint4x4(lhs * rhs.c0, lhs * rhs.c1, lhs * rhs.c2, lhs * rhs.c3);
		}

		// Token: 0x0600240E RID: 9230 RVA: 0x00065E68 File Offset: 0x00064068
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4x4 operator +(uint4x4 lhs, uint4x4 rhs)
		{
			return new uint4x4(lhs.c0 + rhs.c0, lhs.c1 + rhs.c1, lhs.c2 + rhs.c2, lhs.c3 + rhs.c3);
		}

		// Token: 0x0600240F RID: 9231 RVA: 0x00065EBE File Offset: 0x000640BE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4x4 operator +(uint4x4 lhs, uint rhs)
		{
			return new uint4x4(lhs.c0 + rhs, lhs.c1 + rhs, lhs.c2 + rhs, lhs.c3 + rhs);
		}

		// Token: 0x06002410 RID: 9232 RVA: 0x00065EF5 File Offset: 0x000640F5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4x4 operator +(uint lhs, uint4x4 rhs)
		{
			return new uint4x4(lhs + rhs.c0, lhs + rhs.c1, lhs + rhs.c2, lhs + rhs.c3);
		}

		// Token: 0x06002411 RID: 9233 RVA: 0x00065F2C File Offset: 0x0006412C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4x4 operator -(uint4x4 lhs, uint4x4 rhs)
		{
			return new uint4x4(lhs.c0 - rhs.c0, lhs.c1 - rhs.c1, lhs.c2 - rhs.c2, lhs.c3 - rhs.c3);
		}

		// Token: 0x06002412 RID: 9234 RVA: 0x00065F82 File Offset: 0x00064182
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4x4 operator -(uint4x4 lhs, uint rhs)
		{
			return new uint4x4(lhs.c0 - rhs, lhs.c1 - rhs, lhs.c2 - rhs, lhs.c3 - rhs);
		}

		// Token: 0x06002413 RID: 9235 RVA: 0x00065FB9 File Offset: 0x000641B9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4x4 operator -(uint lhs, uint4x4 rhs)
		{
			return new uint4x4(lhs - rhs.c0, lhs - rhs.c1, lhs - rhs.c2, lhs - rhs.c3);
		}

		// Token: 0x06002414 RID: 9236 RVA: 0x00065FF0 File Offset: 0x000641F0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4x4 operator /(uint4x4 lhs, uint4x4 rhs)
		{
			return new uint4x4(lhs.c0 / rhs.c0, lhs.c1 / rhs.c1, lhs.c2 / rhs.c2, lhs.c3 / rhs.c3);
		}

		// Token: 0x06002415 RID: 9237 RVA: 0x00066046 File Offset: 0x00064246
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4x4 operator /(uint4x4 lhs, uint rhs)
		{
			return new uint4x4(lhs.c0 / rhs, lhs.c1 / rhs, lhs.c2 / rhs, lhs.c3 / rhs);
		}

		// Token: 0x06002416 RID: 9238 RVA: 0x0006607D File Offset: 0x0006427D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4x4 operator /(uint lhs, uint4x4 rhs)
		{
			return new uint4x4(lhs / rhs.c0, lhs / rhs.c1, lhs / rhs.c2, lhs / rhs.c3);
		}

		// Token: 0x06002417 RID: 9239 RVA: 0x000660B4 File Offset: 0x000642B4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4x4 operator %(uint4x4 lhs, uint4x4 rhs)
		{
			return new uint4x4(lhs.c0 % rhs.c0, lhs.c1 % rhs.c1, lhs.c2 % rhs.c2, lhs.c3 % rhs.c3);
		}

		// Token: 0x06002418 RID: 9240 RVA: 0x0006610A File Offset: 0x0006430A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4x4 operator %(uint4x4 lhs, uint rhs)
		{
			return new uint4x4(lhs.c0 % rhs, lhs.c1 % rhs, lhs.c2 % rhs, lhs.c3 % rhs);
		}

		// Token: 0x06002419 RID: 9241 RVA: 0x00066141 File Offset: 0x00064341
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4x4 operator %(uint lhs, uint4x4 rhs)
		{
			return new uint4x4(lhs % rhs.c0, lhs % rhs.c1, lhs % rhs.c2, lhs % rhs.c3);
		}

		// Token: 0x0600241A RID: 9242 RVA: 0x00066178 File Offset: 0x00064378
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4x4 operator ++(uint4x4 val)
		{
			uint4 @uint = ++val.c0;
			val.c0 = @uint;
			uint4 uint2 = @uint;
			@uint = ++val.c1;
			val.c1 = @uint;
			uint4 uint3 = @uint;
			@uint = ++val.c2;
			val.c2 = @uint;
			uint4 uint4 = @uint;
			@uint = ++val.c3;
			val.c3 = @uint;
			return new uint4x4(uint2, uint3, uint4, @uint);
		}

		// Token: 0x0600241B RID: 9243 RVA: 0x000661F4 File Offset: 0x000643F4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4x4 operator --(uint4x4 val)
		{
			uint4 @uint = --val.c0;
			val.c0 = @uint;
			uint4 uint2 = @uint;
			@uint = --val.c1;
			val.c1 = @uint;
			uint4 uint3 = @uint;
			@uint = --val.c2;
			val.c2 = @uint;
			uint4 uint4 = @uint;
			@uint = --val.c3;
			val.c3 = @uint;
			return new uint4x4(uint2, uint3, uint4, @uint);
		}

		// Token: 0x0600241C RID: 9244 RVA: 0x00066270 File Offset: 0x00064470
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x4 operator <(uint4x4 lhs, uint4x4 rhs)
		{
			return new bool4x4(lhs.c0 < rhs.c0, lhs.c1 < rhs.c1, lhs.c2 < rhs.c2, lhs.c3 < rhs.c3);
		}

		// Token: 0x0600241D RID: 9245 RVA: 0x000662C6 File Offset: 0x000644C6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x4 operator <(uint4x4 lhs, uint rhs)
		{
			return new bool4x4(lhs.c0 < rhs, lhs.c1 < rhs, lhs.c2 < rhs, lhs.c3 < rhs);
		}

		// Token: 0x0600241E RID: 9246 RVA: 0x000662FD File Offset: 0x000644FD
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x4 operator <(uint lhs, uint4x4 rhs)
		{
			return new bool4x4(lhs < rhs.c0, lhs < rhs.c1, lhs < rhs.c2, lhs < rhs.c3);
		}

		// Token: 0x0600241F RID: 9247 RVA: 0x00066334 File Offset: 0x00064534
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x4 operator <=(uint4x4 lhs, uint4x4 rhs)
		{
			return new bool4x4(lhs.c0 <= rhs.c0, lhs.c1 <= rhs.c1, lhs.c2 <= rhs.c2, lhs.c3 <= rhs.c3);
		}

		// Token: 0x06002420 RID: 9248 RVA: 0x0006638A File Offset: 0x0006458A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x4 operator <=(uint4x4 lhs, uint rhs)
		{
			return new bool4x4(lhs.c0 <= rhs, lhs.c1 <= rhs, lhs.c2 <= rhs, lhs.c3 <= rhs);
		}

		// Token: 0x06002421 RID: 9249 RVA: 0x000663C1 File Offset: 0x000645C1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x4 operator <=(uint lhs, uint4x4 rhs)
		{
			return new bool4x4(lhs <= rhs.c0, lhs <= rhs.c1, lhs <= rhs.c2, lhs <= rhs.c3);
		}

		// Token: 0x06002422 RID: 9250 RVA: 0x000663F8 File Offset: 0x000645F8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x4 operator >(uint4x4 lhs, uint4x4 rhs)
		{
			return new bool4x4(lhs.c0 > rhs.c0, lhs.c1 > rhs.c1, lhs.c2 > rhs.c2, lhs.c3 > rhs.c3);
		}

		// Token: 0x06002423 RID: 9251 RVA: 0x0006644E File Offset: 0x0006464E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x4 operator >(uint4x4 lhs, uint rhs)
		{
			return new bool4x4(lhs.c0 > rhs, lhs.c1 > rhs, lhs.c2 > rhs, lhs.c3 > rhs);
		}

		// Token: 0x06002424 RID: 9252 RVA: 0x00066485 File Offset: 0x00064685
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x4 operator >(uint lhs, uint4x4 rhs)
		{
			return new bool4x4(lhs > rhs.c0, lhs > rhs.c1, lhs > rhs.c2, lhs > rhs.c3);
		}

		// Token: 0x06002425 RID: 9253 RVA: 0x000664BC File Offset: 0x000646BC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x4 operator >=(uint4x4 lhs, uint4x4 rhs)
		{
			return new bool4x4(lhs.c0 >= rhs.c0, lhs.c1 >= rhs.c1, lhs.c2 >= rhs.c2, lhs.c3 >= rhs.c3);
		}

		// Token: 0x06002426 RID: 9254 RVA: 0x00066512 File Offset: 0x00064712
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x4 operator >=(uint4x4 lhs, uint rhs)
		{
			return new bool4x4(lhs.c0 >= rhs, lhs.c1 >= rhs, lhs.c2 >= rhs, lhs.c3 >= rhs);
		}

		// Token: 0x06002427 RID: 9255 RVA: 0x00066549 File Offset: 0x00064749
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x4 operator >=(uint lhs, uint4x4 rhs)
		{
			return new bool4x4(lhs >= rhs.c0, lhs >= rhs.c1, lhs >= rhs.c2, lhs >= rhs.c3);
		}

		// Token: 0x06002428 RID: 9256 RVA: 0x00066580 File Offset: 0x00064780
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4x4 operator -(uint4x4 val)
		{
			return new uint4x4(-val.c0, -val.c1, -val.c2, -val.c3);
		}

		// Token: 0x06002429 RID: 9257 RVA: 0x000665B3 File Offset: 0x000647B3
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4x4 operator +(uint4x4 val)
		{
			return new uint4x4(+val.c0, +val.c1, +val.c2, +val.c3);
		}

		// Token: 0x0600242A RID: 9258 RVA: 0x000665E6 File Offset: 0x000647E6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4x4 operator <<(uint4x4 x, int n)
		{
			return new uint4x4(x.c0 << n, x.c1 << n, x.c2 << n, x.c3 << n);
		}

		// Token: 0x0600242B RID: 9259 RVA: 0x0006661D File Offset: 0x0006481D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4x4 operator >>(uint4x4 x, int n)
		{
			return new uint4x4(x.c0 >> n, x.c1 >> n, x.c2 >> n, x.c3 >> n);
		}

		// Token: 0x0600242C RID: 9260 RVA: 0x00066654 File Offset: 0x00064854
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x4 operator ==(uint4x4 lhs, uint4x4 rhs)
		{
			return new bool4x4(lhs.c0 == rhs.c0, lhs.c1 == rhs.c1, lhs.c2 == rhs.c2, lhs.c3 == rhs.c3);
		}

		// Token: 0x0600242D RID: 9261 RVA: 0x000666AA File Offset: 0x000648AA
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x4 operator ==(uint4x4 lhs, uint rhs)
		{
			return new bool4x4(lhs.c0 == rhs, lhs.c1 == rhs, lhs.c2 == rhs, lhs.c3 == rhs);
		}

		// Token: 0x0600242E RID: 9262 RVA: 0x000666E1 File Offset: 0x000648E1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x4 operator ==(uint lhs, uint4x4 rhs)
		{
			return new bool4x4(lhs == rhs.c0, lhs == rhs.c1, lhs == rhs.c2, lhs == rhs.c3);
		}

		// Token: 0x0600242F RID: 9263 RVA: 0x00066718 File Offset: 0x00064918
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x4 operator !=(uint4x4 lhs, uint4x4 rhs)
		{
			return new bool4x4(lhs.c0 != rhs.c0, lhs.c1 != rhs.c1, lhs.c2 != rhs.c2, lhs.c3 != rhs.c3);
		}

		// Token: 0x06002430 RID: 9264 RVA: 0x0006676E File Offset: 0x0006496E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x4 operator !=(uint4x4 lhs, uint rhs)
		{
			return new bool4x4(lhs.c0 != rhs, lhs.c1 != rhs, lhs.c2 != rhs, lhs.c3 != rhs);
		}

		// Token: 0x06002431 RID: 9265 RVA: 0x000667A5 File Offset: 0x000649A5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x4 operator !=(uint lhs, uint4x4 rhs)
		{
			return new bool4x4(lhs != rhs.c0, lhs != rhs.c1, lhs != rhs.c2, lhs != rhs.c3);
		}

		// Token: 0x06002432 RID: 9266 RVA: 0x000667DC File Offset: 0x000649DC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4x4 operator ~(uint4x4 val)
		{
			return new uint4x4(~val.c0, ~val.c1, ~val.c2, ~val.c3);
		}

		// Token: 0x06002433 RID: 9267 RVA: 0x00066810 File Offset: 0x00064A10
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4x4 operator &(uint4x4 lhs, uint4x4 rhs)
		{
			return new uint4x4(lhs.c0 & rhs.c0, lhs.c1 & rhs.c1, lhs.c2 & rhs.c2, lhs.c3 & rhs.c3);
		}

		// Token: 0x06002434 RID: 9268 RVA: 0x00066866 File Offset: 0x00064A66
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4x4 operator &(uint4x4 lhs, uint rhs)
		{
			return new uint4x4(lhs.c0 & rhs, lhs.c1 & rhs, lhs.c2 & rhs, lhs.c3 & rhs);
		}

		// Token: 0x06002435 RID: 9269 RVA: 0x0006689D File Offset: 0x00064A9D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4x4 operator &(uint lhs, uint4x4 rhs)
		{
			return new uint4x4(lhs & rhs.c0, lhs & rhs.c1, lhs & rhs.c2, lhs & rhs.c3);
		}

		// Token: 0x06002436 RID: 9270 RVA: 0x000668D4 File Offset: 0x00064AD4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4x4 operator |(uint4x4 lhs, uint4x4 rhs)
		{
			return new uint4x4(lhs.c0 | rhs.c0, lhs.c1 | rhs.c1, lhs.c2 | rhs.c2, lhs.c3 | rhs.c3);
		}

		// Token: 0x06002437 RID: 9271 RVA: 0x0006692A File Offset: 0x00064B2A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4x4 operator |(uint4x4 lhs, uint rhs)
		{
			return new uint4x4(lhs.c0 | rhs, lhs.c1 | rhs, lhs.c2 | rhs, lhs.c3 | rhs);
		}

		// Token: 0x06002438 RID: 9272 RVA: 0x00066961 File Offset: 0x00064B61
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4x4 operator |(uint lhs, uint4x4 rhs)
		{
			return new uint4x4(lhs | rhs.c0, lhs | rhs.c1, lhs | rhs.c2, lhs | rhs.c3);
		}

		// Token: 0x06002439 RID: 9273 RVA: 0x00066998 File Offset: 0x00064B98
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4x4 operator ^(uint4x4 lhs, uint4x4 rhs)
		{
			return new uint4x4(lhs.c0 ^ rhs.c0, lhs.c1 ^ rhs.c1, lhs.c2 ^ rhs.c2, lhs.c3 ^ rhs.c3);
		}

		// Token: 0x0600243A RID: 9274 RVA: 0x000669EE File Offset: 0x00064BEE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4x4 operator ^(uint4x4 lhs, uint rhs)
		{
			return new uint4x4(lhs.c0 ^ rhs, lhs.c1 ^ rhs, lhs.c2 ^ rhs, lhs.c3 ^ rhs);
		}

		// Token: 0x0600243B RID: 9275 RVA: 0x00066A25 File Offset: 0x00064C25
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4x4 operator ^(uint lhs, uint4x4 rhs)
		{
			return new uint4x4(lhs ^ rhs.c0, lhs ^ rhs.c1, lhs ^ rhs.c2, lhs ^ rhs.c3);
		}

		// Token: 0x17000B89 RID: 2953
		public unsafe uint4 this[int index]
		{
			get
			{
				fixed (uint4x4* ptr = &this)
				{
					return ref *(uint4*)(ptr + (IntPtr)index * (IntPtr)sizeof(uint4) / (IntPtr)sizeof(uint4x4));
				}
			}
		}

		// Token: 0x0600243D RID: 9277 RVA: 0x00066A78 File Offset: 0x00064C78
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool Equals(uint4x4 rhs)
		{
			return this.c0.Equals(rhs.c0) && this.c1.Equals(rhs.c1) && this.c2.Equals(rhs.c2) && this.c3.Equals(rhs.c3);
		}

		// Token: 0x0600243E RID: 9278 RVA: 0x00066AD4 File Offset: 0x00064CD4
		public override bool Equals(object o)
		{
			if (o is uint4x4)
			{
				uint4x4 rhs = (uint4x4)o;
				return this.Equals(rhs);
			}
			return false;
		}

		// Token: 0x0600243F RID: 9279 RVA: 0x00066AF9 File Offset: 0x00064CF9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override int GetHashCode()
		{
			return (int)math.hash(this);
		}

		// Token: 0x06002440 RID: 9280 RVA: 0x00066B08 File Offset: 0x00064D08
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override string ToString()
		{
			return string.Format("uint4x4({0}, {1}, {2}, {3},  {4}, {5}, {6}, {7},  {8}, {9}, {10}, {11},  {12}, {13}, {14}, {15})", new object[]
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

		// Token: 0x06002441 RID: 9281 RVA: 0x00066C60 File Offset: 0x00064E60
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public string ToString(string format, IFormatProvider formatProvider)
		{
			return string.Format("uint4x4({0}, {1}, {2}, {3},  {4}, {5}, {6}, {7},  {8}, {9}, {10}, {11},  {12}, {13}, {14}, {15})", new object[]
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

		// Token: 0x06002442 RID: 9282 RVA: 0x00066DD8 File Offset: 0x00064FD8
		// Note: this type is marked as 'beforefieldinit'.
		static uint4x4()
		{
		}

		// Token: 0x04000115 RID: 277
		public uint4 c0;

		// Token: 0x04000116 RID: 278
		public uint4 c1;

		// Token: 0x04000117 RID: 279
		public uint4 c2;

		// Token: 0x04000118 RID: 280
		public uint4 c3;

		// Token: 0x04000119 RID: 281
		public static readonly uint4x4 identity = new uint4x4(1U, 0U, 0U, 0U, 0U, 1U, 0U, 0U, 0U, 0U, 1U, 0U, 0U, 0U, 0U, 1U);

		// Token: 0x0400011A RID: 282
		public static readonly uint4x4 zero;
	}
}

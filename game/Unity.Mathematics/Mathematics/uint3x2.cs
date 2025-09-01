using System;
using System.Runtime.CompilerServices;
using Unity.IL2CPP.CompilerServices;

namespace Unity.Mathematics
{
	// Token: 0x02000043 RID: 67
	[Il2CppEagerStaticClassConstruction]
	[Serializable]
	public struct uint3x2 : IEquatable<uint3x2>, IFormattable
	{
		// Token: 0x060020A1 RID: 8353 RVA: 0x0005D74A File Offset: 0x0005B94A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public uint3x2(uint3 c0, uint3 c1)
		{
			this.c0 = c0;
			this.c1 = c1;
		}

		// Token: 0x060020A2 RID: 8354 RVA: 0x0005D75A File Offset: 0x0005B95A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public uint3x2(uint m00, uint m01, uint m10, uint m11, uint m20, uint m21)
		{
			this.c0 = new uint3(m00, m10, m20);
			this.c1 = new uint3(m01, m11, m21);
		}

		// Token: 0x060020A3 RID: 8355 RVA: 0x0005D77B File Offset: 0x0005B97B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public uint3x2(uint v)
		{
			this.c0 = v;
			this.c1 = v;
		}

		// Token: 0x060020A4 RID: 8356 RVA: 0x0005D795 File Offset: 0x0005B995
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public uint3x2(bool v)
		{
			this.c0 = math.select(new uint3(0U), new uint3(1U), v);
			this.c1 = math.select(new uint3(0U), new uint3(1U), v);
		}

		// Token: 0x060020A5 RID: 8357 RVA: 0x0005D7C7 File Offset: 0x0005B9C7
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public uint3x2(bool3x2 v)
		{
			this.c0 = math.select(new uint3(0U), new uint3(1U), v.c0);
			this.c1 = math.select(new uint3(0U), new uint3(1U), v.c1);
		}

		// Token: 0x060020A6 RID: 8358 RVA: 0x0005D803 File Offset: 0x0005BA03
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public uint3x2(int v)
		{
			this.c0 = (uint3)v;
			this.c1 = (uint3)v;
		}

		// Token: 0x060020A7 RID: 8359 RVA: 0x0005D81D File Offset: 0x0005BA1D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public uint3x2(int3x2 v)
		{
			this.c0 = (uint3)v.c0;
			this.c1 = (uint3)v.c1;
		}

		// Token: 0x060020A8 RID: 8360 RVA: 0x0005D841 File Offset: 0x0005BA41
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public uint3x2(float v)
		{
			this.c0 = (uint3)v;
			this.c1 = (uint3)v;
		}

		// Token: 0x060020A9 RID: 8361 RVA: 0x0005D85B File Offset: 0x0005BA5B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public uint3x2(float3x2 v)
		{
			this.c0 = (uint3)v.c0;
			this.c1 = (uint3)v.c1;
		}

		// Token: 0x060020AA RID: 8362 RVA: 0x0005D87F File Offset: 0x0005BA7F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public uint3x2(double v)
		{
			this.c0 = (uint3)v;
			this.c1 = (uint3)v;
		}

		// Token: 0x060020AB RID: 8363 RVA: 0x0005D899 File Offset: 0x0005BA99
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public uint3x2(double3x2 v)
		{
			this.c0 = (uint3)v.c0;
			this.c1 = (uint3)v.c1;
		}

		// Token: 0x060020AC RID: 8364 RVA: 0x0005D8BD File Offset: 0x0005BABD
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator uint3x2(uint v)
		{
			return new uint3x2(v);
		}

		// Token: 0x060020AD RID: 8365 RVA: 0x0005D8C5 File Offset: 0x0005BAC5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator uint3x2(bool v)
		{
			return new uint3x2(v);
		}

		// Token: 0x060020AE RID: 8366 RVA: 0x0005D8CD File Offset: 0x0005BACD
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator uint3x2(bool3x2 v)
		{
			return new uint3x2(v);
		}

		// Token: 0x060020AF RID: 8367 RVA: 0x0005D8D5 File Offset: 0x0005BAD5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator uint3x2(int v)
		{
			return new uint3x2(v);
		}

		// Token: 0x060020B0 RID: 8368 RVA: 0x0005D8DD File Offset: 0x0005BADD
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator uint3x2(int3x2 v)
		{
			return new uint3x2(v);
		}

		// Token: 0x060020B1 RID: 8369 RVA: 0x0005D8E5 File Offset: 0x0005BAE5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator uint3x2(float v)
		{
			return new uint3x2(v);
		}

		// Token: 0x060020B2 RID: 8370 RVA: 0x0005D8ED File Offset: 0x0005BAED
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator uint3x2(float3x2 v)
		{
			return new uint3x2(v);
		}

		// Token: 0x060020B3 RID: 8371 RVA: 0x0005D8F5 File Offset: 0x0005BAF5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator uint3x2(double v)
		{
			return new uint3x2(v);
		}

		// Token: 0x060020B4 RID: 8372 RVA: 0x0005D8FD File Offset: 0x0005BAFD
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator uint3x2(double3x2 v)
		{
			return new uint3x2(v);
		}

		// Token: 0x060020B5 RID: 8373 RVA: 0x0005D905 File Offset: 0x0005BB05
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3x2 operator *(uint3x2 lhs, uint3x2 rhs)
		{
			return new uint3x2(lhs.c0 * rhs.c0, lhs.c1 * rhs.c1);
		}

		// Token: 0x060020B6 RID: 8374 RVA: 0x0005D92E File Offset: 0x0005BB2E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3x2 operator *(uint3x2 lhs, uint rhs)
		{
			return new uint3x2(lhs.c0 * rhs, lhs.c1 * rhs);
		}

		// Token: 0x060020B7 RID: 8375 RVA: 0x0005D94D File Offset: 0x0005BB4D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3x2 operator *(uint lhs, uint3x2 rhs)
		{
			return new uint3x2(lhs * rhs.c0, lhs * rhs.c1);
		}

		// Token: 0x060020B8 RID: 8376 RVA: 0x0005D96C File Offset: 0x0005BB6C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3x2 operator +(uint3x2 lhs, uint3x2 rhs)
		{
			return new uint3x2(lhs.c0 + rhs.c0, lhs.c1 + rhs.c1);
		}

		// Token: 0x060020B9 RID: 8377 RVA: 0x0005D995 File Offset: 0x0005BB95
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3x2 operator +(uint3x2 lhs, uint rhs)
		{
			return new uint3x2(lhs.c0 + rhs, lhs.c1 + rhs);
		}

		// Token: 0x060020BA RID: 8378 RVA: 0x0005D9B4 File Offset: 0x0005BBB4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3x2 operator +(uint lhs, uint3x2 rhs)
		{
			return new uint3x2(lhs + rhs.c0, lhs + rhs.c1);
		}

		// Token: 0x060020BB RID: 8379 RVA: 0x0005D9D3 File Offset: 0x0005BBD3
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3x2 operator -(uint3x2 lhs, uint3x2 rhs)
		{
			return new uint3x2(lhs.c0 - rhs.c0, lhs.c1 - rhs.c1);
		}

		// Token: 0x060020BC RID: 8380 RVA: 0x0005D9FC File Offset: 0x0005BBFC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3x2 operator -(uint3x2 lhs, uint rhs)
		{
			return new uint3x2(lhs.c0 - rhs, lhs.c1 - rhs);
		}

		// Token: 0x060020BD RID: 8381 RVA: 0x0005DA1B File Offset: 0x0005BC1B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3x2 operator -(uint lhs, uint3x2 rhs)
		{
			return new uint3x2(lhs - rhs.c0, lhs - rhs.c1);
		}

		// Token: 0x060020BE RID: 8382 RVA: 0x0005DA3A File Offset: 0x0005BC3A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3x2 operator /(uint3x2 lhs, uint3x2 rhs)
		{
			return new uint3x2(lhs.c0 / rhs.c0, lhs.c1 / rhs.c1);
		}

		// Token: 0x060020BF RID: 8383 RVA: 0x0005DA63 File Offset: 0x0005BC63
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3x2 operator /(uint3x2 lhs, uint rhs)
		{
			return new uint3x2(lhs.c0 / rhs, lhs.c1 / rhs);
		}

		// Token: 0x060020C0 RID: 8384 RVA: 0x0005DA82 File Offset: 0x0005BC82
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3x2 operator /(uint lhs, uint3x2 rhs)
		{
			return new uint3x2(lhs / rhs.c0, lhs / rhs.c1);
		}

		// Token: 0x060020C1 RID: 8385 RVA: 0x0005DAA1 File Offset: 0x0005BCA1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3x2 operator %(uint3x2 lhs, uint3x2 rhs)
		{
			return new uint3x2(lhs.c0 % rhs.c0, lhs.c1 % rhs.c1);
		}

		// Token: 0x060020C2 RID: 8386 RVA: 0x0005DACA File Offset: 0x0005BCCA
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3x2 operator %(uint3x2 lhs, uint rhs)
		{
			return new uint3x2(lhs.c0 % rhs, lhs.c1 % rhs);
		}

		// Token: 0x060020C3 RID: 8387 RVA: 0x0005DAE9 File Offset: 0x0005BCE9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3x2 operator %(uint lhs, uint3x2 rhs)
		{
			return new uint3x2(lhs % rhs.c0, lhs % rhs.c1);
		}

		// Token: 0x060020C4 RID: 8388 RVA: 0x0005DB08 File Offset: 0x0005BD08
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3x2 operator ++(uint3x2 val)
		{
			uint3 @uint = ++val.c0;
			val.c0 = @uint;
			uint3 uint2 = @uint;
			@uint = ++val.c1;
			val.c1 = @uint;
			return new uint3x2(uint2, @uint);
		}

		// Token: 0x060020C5 RID: 8389 RVA: 0x0005DB50 File Offset: 0x0005BD50
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3x2 operator --(uint3x2 val)
		{
			uint3 @uint = --val.c0;
			val.c0 = @uint;
			uint3 uint2 = @uint;
			@uint = --val.c1;
			val.c1 = @uint;
			return new uint3x2(uint2, @uint);
		}

		// Token: 0x060020C6 RID: 8390 RVA: 0x0005DB96 File Offset: 0x0005BD96
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x2 operator <(uint3x2 lhs, uint3x2 rhs)
		{
			return new bool3x2(lhs.c0 < rhs.c0, lhs.c1 < rhs.c1);
		}

		// Token: 0x060020C7 RID: 8391 RVA: 0x0005DBBF File Offset: 0x0005BDBF
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x2 operator <(uint3x2 lhs, uint rhs)
		{
			return new bool3x2(lhs.c0 < rhs, lhs.c1 < rhs);
		}

		// Token: 0x060020C8 RID: 8392 RVA: 0x0005DBDE File Offset: 0x0005BDDE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x2 operator <(uint lhs, uint3x2 rhs)
		{
			return new bool3x2(lhs < rhs.c0, lhs < rhs.c1);
		}

		// Token: 0x060020C9 RID: 8393 RVA: 0x0005DBFD File Offset: 0x0005BDFD
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x2 operator <=(uint3x2 lhs, uint3x2 rhs)
		{
			return new bool3x2(lhs.c0 <= rhs.c0, lhs.c1 <= rhs.c1);
		}

		// Token: 0x060020CA RID: 8394 RVA: 0x0005DC26 File Offset: 0x0005BE26
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x2 operator <=(uint3x2 lhs, uint rhs)
		{
			return new bool3x2(lhs.c0 <= rhs, lhs.c1 <= rhs);
		}

		// Token: 0x060020CB RID: 8395 RVA: 0x0005DC45 File Offset: 0x0005BE45
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x2 operator <=(uint lhs, uint3x2 rhs)
		{
			return new bool3x2(lhs <= rhs.c0, lhs <= rhs.c1);
		}

		// Token: 0x060020CC RID: 8396 RVA: 0x0005DC64 File Offset: 0x0005BE64
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x2 operator >(uint3x2 lhs, uint3x2 rhs)
		{
			return new bool3x2(lhs.c0 > rhs.c0, lhs.c1 > rhs.c1);
		}

		// Token: 0x060020CD RID: 8397 RVA: 0x0005DC8D File Offset: 0x0005BE8D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x2 operator >(uint3x2 lhs, uint rhs)
		{
			return new bool3x2(lhs.c0 > rhs, lhs.c1 > rhs);
		}

		// Token: 0x060020CE RID: 8398 RVA: 0x0005DCAC File Offset: 0x0005BEAC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x2 operator >(uint lhs, uint3x2 rhs)
		{
			return new bool3x2(lhs > rhs.c0, lhs > rhs.c1);
		}

		// Token: 0x060020CF RID: 8399 RVA: 0x0005DCCB File Offset: 0x0005BECB
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x2 operator >=(uint3x2 lhs, uint3x2 rhs)
		{
			return new bool3x2(lhs.c0 >= rhs.c0, lhs.c1 >= rhs.c1);
		}

		// Token: 0x060020D0 RID: 8400 RVA: 0x0005DCF4 File Offset: 0x0005BEF4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x2 operator >=(uint3x2 lhs, uint rhs)
		{
			return new bool3x2(lhs.c0 >= rhs, lhs.c1 >= rhs);
		}

		// Token: 0x060020D1 RID: 8401 RVA: 0x0005DD13 File Offset: 0x0005BF13
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x2 operator >=(uint lhs, uint3x2 rhs)
		{
			return new bool3x2(lhs >= rhs.c0, lhs >= rhs.c1);
		}

		// Token: 0x060020D2 RID: 8402 RVA: 0x0005DD32 File Offset: 0x0005BF32
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3x2 operator -(uint3x2 val)
		{
			return new uint3x2(-val.c0, -val.c1);
		}

		// Token: 0x060020D3 RID: 8403 RVA: 0x0005DD4F File Offset: 0x0005BF4F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3x2 operator +(uint3x2 val)
		{
			return new uint3x2(+val.c0, +val.c1);
		}

		// Token: 0x060020D4 RID: 8404 RVA: 0x0005DD6C File Offset: 0x0005BF6C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3x2 operator <<(uint3x2 x, int n)
		{
			return new uint3x2(x.c0 << n, x.c1 << n);
		}

		// Token: 0x060020D5 RID: 8405 RVA: 0x0005DD8B File Offset: 0x0005BF8B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3x2 operator >>(uint3x2 x, int n)
		{
			return new uint3x2(x.c0 >> n, x.c1 >> n);
		}

		// Token: 0x060020D6 RID: 8406 RVA: 0x0005DDAA File Offset: 0x0005BFAA
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x2 operator ==(uint3x2 lhs, uint3x2 rhs)
		{
			return new bool3x2(lhs.c0 == rhs.c0, lhs.c1 == rhs.c1);
		}

		// Token: 0x060020D7 RID: 8407 RVA: 0x0005DDD3 File Offset: 0x0005BFD3
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x2 operator ==(uint3x2 lhs, uint rhs)
		{
			return new bool3x2(lhs.c0 == rhs, lhs.c1 == rhs);
		}

		// Token: 0x060020D8 RID: 8408 RVA: 0x0005DDF2 File Offset: 0x0005BFF2
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x2 operator ==(uint lhs, uint3x2 rhs)
		{
			return new bool3x2(lhs == rhs.c0, lhs == rhs.c1);
		}

		// Token: 0x060020D9 RID: 8409 RVA: 0x0005DE11 File Offset: 0x0005C011
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x2 operator !=(uint3x2 lhs, uint3x2 rhs)
		{
			return new bool3x2(lhs.c0 != rhs.c0, lhs.c1 != rhs.c1);
		}

		// Token: 0x060020DA RID: 8410 RVA: 0x0005DE3A File Offset: 0x0005C03A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x2 operator !=(uint3x2 lhs, uint rhs)
		{
			return new bool3x2(lhs.c0 != rhs, lhs.c1 != rhs);
		}

		// Token: 0x060020DB RID: 8411 RVA: 0x0005DE59 File Offset: 0x0005C059
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x2 operator !=(uint lhs, uint3x2 rhs)
		{
			return new bool3x2(lhs != rhs.c0, lhs != rhs.c1);
		}

		// Token: 0x060020DC RID: 8412 RVA: 0x0005DE78 File Offset: 0x0005C078
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3x2 operator ~(uint3x2 val)
		{
			return new uint3x2(~val.c0, ~val.c1);
		}

		// Token: 0x060020DD RID: 8413 RVA: 0x0005DE95 File Offset: 0x0005C095
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3x2 operator &(uint3x2 lhs, uint3x2 rhs)
		{
			return new uint3x2(lhs.c0 & rhs.c0, lhs.c1 & rhs.c1);
		}

		// Token: 0x060020DE RID: 8414 RVA: 0x0005DEBE File Offset: 0x0005C0BE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3x2 operator &(uint3x2 lhs, uint rhs)
		{
			return new uint3x2(lhs.c0 & rhs, lhs.c1 & rhs);
		}

		// Token: 0x060020DF RID: 8415 RVA: 0x0005DEDD File Offset: 0x0005C0DD
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3x2 operator &(uint lhs, uint3x2 rhs)
		{
			return new uint3x2(lhs & rhs.c0, lhs & rhs.c1);
		}

		// Token: 0x060020E0 RID: 8416 RVA: 0x0005DEFC File Offset: 0x0005C0FC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3x2 operator |(uint3x2 lhs, uint3x2 rhs)
		{
			return new uint3x2(lhs.c0 | rhs.c0, lhs.c1 | rhs.c1);
		}

		// Token: 0x060020E1 RID: 8417 RVA: 0x0005DF25 File Offset: 0x0005C125
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3x2 operator |(uint3x2 lhs, uint rhs)
		{
			return new uint3x2(lhs.c0 | rhs, lhs.c1 | rhs);
		}

		// Token: 0x060020E2 RID: 8418 RVA: 0x0005DF44 File Offset: 0x0005C144
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3x2 operator |(uint lhs, uint3x2 rhs)
		{
			return new uint3x2(lhs | rhs.c0, lhs | rhs.c1);
		}

		// Token: 0x060020E3 RID: 8419 RVA: 0x0005DF63 File Offset: 0x0005C163
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3x2 operator ^(uint3x2 lhs, uint3x2 rhs)
		{
			return new uint3x2(lhs.c0 ^ rhs.c0, lhs.c1 ^ rhs.c1);
		}

		// Token: 0x060020E4 RID: 8420 RVA: 0x0005DF8C File Offset: 0x0005C18C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3x2 operator ^(uint3x2 lhs, uint rhs)
		{
			return new uint3x2(lhs.c0 ^ rhs, lhs.c1 ^ rhs);
		}

		// Token: 0x060020E5 RID: 8421 RVA: 0x0005DFAB File Offset: 0x0005C1AB
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3x2 operator ^(uint lhs, uint3x2 rhs)
		{
			return new uint3x2(lhs ^ rhs.c0, lhs ^ rhs.c1);
		}

		// Token: 0x17000A33 RID: 2611
		public unsafe uint3 this[int index]
		{
			get
			{
				fixed (uint3x2* ptr = &this)
				{
					return ref *(uint3*)(ptr + (IntPtr)index * (IntPtr)sizeof(uint3) / (IntPtr)sizeof(uint3x2));
				}
			}
		}

		// Token: 0x060020E7 RID: 8423 RVA: 0x0005DFE7 File Offset: 0x0005C1E7
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool Equals(uint3x2 rhs)
		{
			return this.c0.Equals(rhs.c0) && this.c1.Equals(rhs.c1);
		}

		// Token: 0x060020E8 RID: 8424 RVA: 0x0005E010 File Offset: 0x0005C210
		public override bool Equals(object o)
		{
			if (o is uint3x2)
			{
				uint3x2 rhs = (uint3x2)o;
				return this.Equals(rhs);
			}
			return false;
		}

		// Token: 0x060020E9 RID: 8425 RVA: 0x0005E035 File Offset: 0x0005C235
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override int GetHashCode()
		{
			return (int)math.hash(this);
		}

		// Token: 0x060020EA RID: 8426 RVA: 0x0005E044 File Offset: 0x0005C244
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override string ToString()
		{
			return string.Format("uint3x2({0}, {1},  {2}, {3},  {4}, {5})", new object[]
			{
				this.c0.x,
				this.c1.x,
				this.c0.y,
				this.c1.y,
				this.c0.z,
				this.c1.z
			});
		}

		// Token: 0x060020EB RID: 8427 RVA: 0x0005E0D4 File Offset: 0x0005C2D4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public string ToString(string format, IFormatProvider formatProvider)
		{
			return string.Format("uint3x2({0}, {1},  {2}, {3},  {4}, {5})", new object[]
			{
				this.c0.x.ToString(format, formatProvider),
				this.c1.x.ToString(format, formatProvider),
				this.c0.y.ToString(format, formatProvider),
				this.c1.y.ToString(format, formatProvider),
				this.c0.z.ToString(format, formatProvider),
				this.c1.z.ToString(format, formatProvider)
			});
		}

		// Token: 0x040000FC RID: 252
		public uint3 c0;

		// Token: 0x040000FD RID: 253
		public uint3 c1;

		// Token: 0x040000FE RID: 254
		public static readonly uint3x2 zero;
	}
}

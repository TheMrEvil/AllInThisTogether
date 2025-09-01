using System;
using System.Runtime.CompilerServices;
using Unity.IL2CPP.CompilerServices;

namespace Unity.Mathematics
{
	// Token: 0x02000047 RID: 71
	[Il2CppEagerStaticClassConstruction]
	[Serializable]
	public struct uint4x2 : IEquatable<uint4x2>, IFormattable
	{
		// Token: 0x06002361 RID: 9057 RVA: 0x00064109 File Offset: 0x00062309
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public uint4x2(uint4 c0, uint4 c1)
		{
			this.c0 = c0;
			this.c1 = c1;
		}

		// Token: 0x06002362 RID: 9058 RVA: 0x00064119 File Offset: 0x00062319
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public uint4x2(uint m00, uint m01, uint m10, uint m11, uint m20, uint m21, uint m30, uint m31)
		{
			this.c0 = new uint4(m00, m10, m20, m30);
			this.c1 = new uint4(m01, m11, m21, m31);
		}

		// Token: 0x06002363 RID: 9059 RVA: 0x0006413E File Offset: 0x0006233E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public uint4x2(uint v)
		{
			this.c0 = v;
			this.c1 = v;
		}

		// Token: 0x06002364 RID: 9060 RVA: 0x00064158 File Offset: 0x00062358
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public uint4x2(bool v)
		{
			this.c0 = math.select(new uint4(0U), new uint4(1U), v);
			this.c1 = math.select(new uint4(0U), new uint4(1U), v);
		}

		// Token: 0x06002365 RID: 9061 RVA: 0x0006418A File Offset: 0x0006238A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public uint4x2(bool4x2 v)
		{
			this.c0 = math.select(new uint4(0U), new uint4(1U), v.c0);
			this.c1 = math.select(new uint4(0U), new uint4(1U), v.c1);
		}

		// Token: 0x06002366 RID: 9062 RVA: 0x000641C6 File Offset: 0x000623C6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public uint4x2(int v)
		{
			this.c0 = (uint4)v;
			this.c1 = (uint4)v;
		}

		// Token: 0x06002367 RID: 9063 RVA: 0x000641E0 File Offset: 0x000623E0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public uint4x2(int4x2 v)
		{
			this.c0 = (uint4)v.c0;
			this.c1 = (uint4)v.c1;
		}

		// Token: 0x06002368 RID: 9064 RVA: 0x00064204 File Offset: 0x00062404
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public uint4x2(float v)
		{
			this.c0 = (uint4)v;
			this.c1 = (uint4)v;
		}

		// Token: 0x06002369 RID: 9065 RVA: 0x0006421E File Offset: 0x0006241E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public uint4x2(float4x2 v)
		{
			this.c0 = (uint4)v.c0;
			this.c1 = (uint4)v.c1;
		}

		// Token: 0x0600236A RID: 9066 RVA: 0x00064242 File Offset: 0x00062442
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public uint4x2(double v)
		{
			this.c0 = (uint4)v;
			this.c1 = (uint4)v;
		}

		// Token: 0x0600236B RID: 9067 RVA: 0x0006425C File Offset: 0x0006245C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public uint4x2(double4x2 v)
		{
			this.c0 = (uint4)v.c0;
			this.c1 = (uint4)v.c1;
		}

		// Token: 0x0600236C RID: 9068 RVA: 0x00064280 File Offset: 0x00062480
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator uint4x2(uint v)
		{
			return new uint4x2(v);
		}

		// Token: 0x0600236D RID: 9069 RVA: 0x00064288 File Offset: 0x00062488
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator uint4x2(bool v)
		{
			return new uint4x2(v);
		}

		// Token: 0x0600236E RID: 9070 RVA: 0x00064290 File Offset: 0x00062490
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator uint4x2(bool4x2 v)
		{
			return new uint4x2(v);
		}

		// Token: 0x0600236F RID: 9071 RVA: 0x00064298 File Offset: 0x00062498
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator uint4x2(int v)
		{
			return new uint4x2(v);
		}

		// Token: 0x06002370 RID: 9072 RVA: 0x000642A0 File Offset: 0x000624A0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator uint4x2(int4x2 v)
		{
			return new uint4x2(v);
		}

		// Token: 0x06002371 RID: 9073 RVA: 0x000642A8 File Offset: 0x000624A8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator uint4x2(float v)
		{
			return new uint4x2(v);
		}

		// Token: 0x06002372 RID: 9074 RVA: 0x000642B0 File Offset: 0x000624B0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator uint4x2(float4x2 v)
		{
			return new uint4x2(v);
		}

		// Token: 0x06002373 RID: 9075 RVA: 0x000642B8 File Offset: 0x000624B8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator uint4x2(double v)
		{
			return new uint4x2(v);
		}

		// Token: 0x06002374 RID: 9076 RVA: 0x000642C0 File Offset: 0x000624C0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator uint4x2(double4x2 v)
		{
			return new uint4x2(v);
		}

		// Token: 0x06002375 RID: 9077 RVA: 0x000642C8 File Offset: 0x000624C8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4x2 operator *(uint4x2 lhs, uint4x2 rhs)
		{
			return new uint4x2(lhs.c0 * rhs.c0, lhs.c1 * rhs.c1);
		}

		// Token: 0x06002376 RID: 9078 RVA: 0x000642F1 File Offset: 0x000624F1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4x2 operator *(uint4x2 lhs, uint rhs)
		{
			return new uint4x2(lhs.c0 * rhs, lhs.c1 * rhs);
		}

		// Token: 0x06002377 RID: 9079 RVA: 0x00064310 File Offset: 0x00062510
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4x2 operator *(uint lhs, uint4x2 rhs)
		{
			return new uint4x2(lhs * rhs.c0, lhs * rhs.c1);
		}

		// Token: 0x06002378 RID: 9080 RVA: 0x0006432F File Offset: 0x0006252F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4x2 operator +(uint4x2 lhs, uint4x2 rhs)
		{
			return new uint4x2(lhs.c0 + rhs.c0, lhs.c1 + rhs.c1);
		}

		// Token: 0x06002379 RID: 9081 RVA: 0x00064358 File Offset: 0x00062558
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4x2 operator +(uint4x2 lhs, uint rhs)
		{
			return new uint4x2(lhs.c0 + rhs, lhs.c1 + rhs);
		}

		// Token: 0x0600237A RID: 9082 RVA: 0x00064377 File Offset: 0x00062577
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4x2 operator +(uint lhs, uint4x2 rhs)
		{
			return new uint4x2(lhs + rhs.c0, lhs + rhs.c1);
		}

		// Token: 0x0600237B RID: 9083 RVA: 0x00064396 File Offset: 0x00062596
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4x2 operator -(uint4x2 lhs, uint4x2 rhs)
		{
			return new uint4x2(lhs.c0 - rhs.c0, lhs.c1 - rhs.c1);
		}

		// Token: 0x0600237C RID: 9084 RVA: 0x000643BF File Offset: 0x000625BF
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4x2 operator -(uint4x2 lhs, uint rhs)
		{
			return new uint4x2(lhs.c0 - rhs, lhs.c1 - rhs);
		}

		// Token: 0x0600237D RID: 9085 RVA: 0x000643DE File Offset: 0x000625DE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4x2 operator -(uint lhs, uint4x2 rhs)
		{
			return new uint4x2(lhs - rhs.c0, lhs - rhs.c1);
		}

		// Token: 0x0600237E RID: 9086 RVA: 0x000643FD File Offset: 0x000625FD
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4x2 operator /(uint4x2 lhs, uint4x2 rhs)
		{
			return new uint4x2(lhs.c0 / rhs.c0, lhs.c1 / rhs.c1);
		}

		// Token: 0x0600237F RID: 9087 RVA: 0x00064426 File Offset: 0x00062626
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4x2 operator /(uint4x2 lhs, uint rhs)
		{
			return new uint4x2(lhs.c0 / rhs, lhs.c1 / rhs);
		}

		// Token: 0x06002380 RID: 9088 RVA: 0x00064445 File Offset: 0x00062645
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4x2 operator /(uint lhs, uint4x2 rhs)
		{
			return new uint4x2(lhs / rhs.c0, lhs / rhs.c1);
		}

		// Token: 0x06002381 RID: 9089 RVA: 0x00064464 File Offset: 0x00062664
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4x2 operator %(uint4x2 lhs, uint4x2 rhs)
		{
			return new uint4x2(lhs.c0 % rhs.c0, lhs.c1 % rhs.c1);
		}

		// Token: 0x06002382 RID: 9090 RVA: 0x0006448D File Offset: 0x0006268D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4x2 operator %(uint4x2 lhs, uint rhs)
		{
			return new uint4x2(lhs.c0 % rhs, lhs.c1 % rhs);
		}

		// Token: 0x06002383 RID: 9091 RVA: 0x000644AC File Offset: 0x000626AC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4x2 operator %(uint lhs, uint4x2 rhs)
		{
			return new uint4x2(lhs % rhs.c0, lhs % rhs.c1);
		}

		// Token: 0x06002384 RID: 9092 RVA: 0x000644CC File Offset: 0x000626CC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4x2 operator ++(uint4x2 val)
		{
			uint4 @uint = ++val.c0;
			val.c0 = @uint;
			uint4 uint2 = @uint;
			@uint = ++val.c1;
			val.c1 = @uint;
			return new uint4x2(uint2, @uint);
		}

		// Token: 0x06002385 RID: 9093 RVA: 0x00064514 File Offset: 0x00062714
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4x2 operator --(uint4x2 val)
		{
			uint4 @uint = --val.c0;
			val.c0 = @uint;
			uint4 uint2 = @uint;
			@uint = --val.c1;
			val.c1 = @uint;
			return new uint4x2(uint2, @uint);
		}

		// Token: 0x06002386 RID: 9094 RVA: 0x0006455A File Offset: 0x0006275A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x2 operator <(uint4x2 lhs, uint4x2 rhs)
		{
			return new bool4x2(lhs.c0 < rhs.c0, lhs.c1 < rhs.c1);
		}

		// Token: 0x06002387 RID: 9095 RVA: 0x00064583 File Offset: 0x00062783
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x2 operator <(uint4x2 lhs, uint rhs)
		{
			return new bool4x2(lhs.c0 < rhs, lhs.c1 < rhs);
		}

		// Token: 0x06002388 RID: 9096 RVA: 0x000645A2 File Offset: 0x000627A2
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x2 operator <(uint lhs, uint4x2 rhs)
		{
			return new bool4x2(lhs < rhs.c0, lhs < rhs.c1);
		}

		// Token: 0x06002389 RID: 9097 RVA: 0x000645C1 File Offset: 0x000627C1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x2 operator <=(uint4x2 lhs, uint4x2 rhs)
		{
			return new bool4x2(lhs.c0 <= rhs.c0, lhs.c1 <= rhs.c1);
		}

		// Token: 0x0600238A RID: 9098 RVA: 0x000645EA File Offset: 0x000627EA
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x2 operator <=(uint4x2 lhs, uint rhs)
		{
			return new bool4x2(lhs.c0 <= rhs, lhs.c1 <= rhs);
		}

		// Token: 0x0600238B RID: 9099 RVA: 0x00064609 File Offset: 0x00062809
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x2 operator <=(uint lhs, uint4x2 rhs)
		{
			return new bool4x2(lhs <= rhs.c0, lhs <= rhs.c1);
		}

		// Token: 0x0600238C RID: 9100 RVA: 0x00064628 File Offset: 0x00062828
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x2 operator >(uint4x2 lhs, uint4x2 rhs)
		{
			return new bool4x2(lhs.c0 > rhs.c0, lhs.c1 > rhs.c1);
		}

		// Token: 0x0600238D RID: 9101 RVA: 0x00064651 File Offset: 0x00062851
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x2 operator >(uint4x2 lhs, uint rhs)
		{
			return new bool4x2(lhs.c0 > rhs, lhs.c1 > rhs);
		}

		// Token: 0x0600238E RID: 9102 RVA: 0x00064670 File Offset: 0x00062870
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x2 operator >(uint lhs, uint4x2 rhs)
		{
			return new bool4x2(lhs > rhs.c0, lhs > rhs.c1);
		}

		// Token: 0x0600238F RID: 9103 RVA: 0x0006468F File Offset: 0x0006288F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x2 operator >=(uint4x2 lhs, uint4x2 rhs)
		{
			return new bool4x2(lhs.c0 >= rhs.c0, lhs.c1 >= rhs.c1);
		}

		// Token: 0x06002390 RID: 9104 RVA: 0x000646B8 File Offset: 0x000628B8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x2 operator >=(uint4x2 lhs, uint rhs)
		{
			return new bool4x2(lhs.c0 >= rhs, lhs.c1 >= rhs);
		}

		// Token: 0x06002391 RID: 9105 RVA: 0x000646D7 File Offset: 0x000628D7
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x2 operator >=(uint lhs, uint4x2 rhs)
		{
			return new bool4x2(lhs >= rhs.c0, lhs >= rhs.c1);
		}

		// Token: 0x06002392 RID: 9106 RVA: 0x000646F6 File Offset: 0x000628F6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4x2 operator -(uint4x2 val)
		{
			return new uint4x2(-val.c0, -val.c1);
		}

		// Token: 0x06002393 RID: 9107 RVA: 0x00064713 File Offset: 0x00062913
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4x2 operator +(uint4x2 val)
		{
			return new uint4x2(+val.c0, +val.c1);
		}

		// Token: 0x06002394 RID: 9108 RVA: 0x00064730 File Offset: 0x00062930
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4x2 operator <<(uint4x2 x, int n)
		{
			return new uint4x2(x.c0 << n, x.c1 << n);
		}

		// Token: 0x06002395 RID: 9109 RVA: 0x0006474F File Offset: 0x0006294F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4x2 operator >>(uint4x2 x, int n)
		{
			return new uint4x2(x.c0 >> n, x.c1 >> n);
		}

		// Token: 0x06002396 RID: 9110 RVA: 0x0006476E File Offset: 0x0006296E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x2 operator ==(uint4x2 lhs, uint4x2 rhs)
		{
			return new bool4x2(lhs.c0 == rhs.c0, lhs.c1 == rhs.c1);
		}

		// Token: 0x06002397 RID: 9111 RVA: 0x00064797 File Offset: 0x00062997
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x2 operator ==(uint4x2 lhs, uint rhs)
		{
			return new bool4x2(lhs.c0 == rhs, lhs.c1 == rhs);
		}

		// Token: 0x06002398 RID: 9112 RVA: 0x000647B6 File Offset: 0x000629B6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x2 operator ==(uint lhs, uint4x2 rhs)
		{
			return new bool4x2(lhs == rhs.c0, lhs == rhs.c1);
		}

		// Token: 0x06002399 RID: 9113 RVA: 0x000647D5 File Offset: 0x000629D5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x2 operator !=(uint4x2 lhs, uint4x2 rhs)
		{
			return new bool4x2(lhs.c0 != rhs.c0, lhs.c1 != rhs.c1);
		}

		// Token: 0x0600239A RID: 9114 RVA: 0x000647FE File Offset: 0x000629FE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x2 operator !=(uint4x2 lhs, uint rhs)
		{
			return new bool4x2(lhs.c0 != rhs, lhs.c1 != rhs);
		}

		// Token: 0x0600239B RID: 9115 RVA: 0x0006481D File Offset: 0x00062A1D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x2 operator !=(uint lhs, uint4x2 rhs)
		{
			return new bool4x2(lhs != rhs.c0, lhs != rhs.c1);
		}

		// Token: 0x0600239C RID: 9116 RVA: 0x0006483C File Offset: 0x00062A3C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4x2 operator ~(uint4x2 val)
		{
			return new uint4x2(~val.c0, ~val.c1);
		}

		// Token: 0x0600239D RID: 9117 RVA: 0x00064859 File Offset: 0x00062A59
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4x2 operator &(uint4x2 lhs, uint4x2 rhs)
		{
			return new uint4x2(lhs.c0 & rhs.c0, lhs.c1 & rhs.c1);
		}

		// Token: 0x0600239E RID: 9118 RVA: 0x00064882 File Offset: 0x00062A82
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4x2 operator &(uint4x2 lhs, uint rhs)
		{
			return new uint4x2(lhs.c0 & rhs, lhs.c1 & rhs);
		}

		// Token: 0x0600239F RID: 9119 RVA: 0x000648A1 File Offset: 0x00062AA1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4x2 operator &(uint lhs, uint4x2 rhs)
		{
			return new uint4x2(lhs & rhs.c0, lhs & rhs.c1);
		}

		// Token: 0x060023A0 RID: 9120 RVA: 0x000648C0 File Offset: 0x00062AC0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4x2 operator |(uint4x2 lhs, uint4x2 rhs)
		{
			return new uint4x2(lhs.c0 | rhs.c0, lhs.c1 | rhs.c1);
		}

		// Token: 0x060023A1 RID: 9121 RVA: 0x000648E9 File Offset: 0x00062AE9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4x2 operator |(uint4x2 lhs, uint rhs)
		{
			return new uint4x2(lhs.c0 | rhs, lhs.c1 | rhs);
		}

		// Token: 0x060023A2 RID: 9122 RVA: 0x00064908 File Offset: 0x00062B08
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4x2 operator |(uint lhs, uint4x2 rhs)
		{
			return new uint4x2(lhs | rhs.c0, lhs | rhs.c1);
		}

		// Token: 0x060023A3 RID: 9123 RVA: 0x00064927 File Offset: 0x00062B27
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4x2 operator ^(uint4x2 lhs, uint4x2 rhs)
		{
			return new uint4x2(lhs.c0 ^ rhs.c0, lhs.c1 ^ rhs.c1);
		}

		// Token: 0x060023A4 RID: 9124 RVA: 0x00064950 File Offset: 0x00062B50
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4x2 operator ^(uint4x2 lhs, uint rhs)
		{
			return new uint4x2(lhs.c0 ^ rhs, lhs.c1 ^ rhs);
		}

		// Token: 0x060023A5 RID: 9125 RVA: 0x0006496F File Offset: 0x00062B6F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4x2 operator ^(uint lhs, uint4x2 rhs)
		{
			return new uint4x2(lhs ^ rhs.c0, lhs ^ rhs.c1);
		}

		// Token: 0x17000B87 RID: 2951
		public unsafe uint4 this[int index]
		{
			get
			{
				fixed (uint4x2* ptr = &this)
				{
					return ref *(uint4*)(ptr + (IntPtr)index * (IntPtr)sizeof(uint4) / (IntPtr)sizeof(uint4x2));
				}
			}
		}

		// Token: 0x060023A7 RID: 9127 RVA: 0x000649AB File Offset: 0x00062BAB
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool Equals(uint4x2 rhs)
		{
			return this.c0.Equals(rhs.c0) && this.c1.Equals(rhs.c1);
		}

		// Token: 0x060023A8 RID: 9128 RVA: 0x000649D4 File Offset: 0x00062BD4
		public override bool Equals(object o)
		{
			if (o is uint4x2)
			{
				uint4x2 rhs = (uint4x2)o;
				return this.Equals(rhs);
			}
			return false;
		}

		// Token: 0x060023A9 RID: 9129 RVA: 0x000649F9 File Offset: 0x00062BF9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override int GetHashCode()
		{
			return (int)math.hash(this);
		}

		// Token: 0x060023AA RID: 9130 RVA: 0x00064A08 File Offset: 0x00062C08
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override string ToString()
		{
			return string.Format("uint4x2({0}, {1},  {2}, {3},  {4}, {5},  {6}, {7})", new object[]
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

		// Token: 0x060023AB RID: 9131 RVA: 0x00064AC0 File Offset: 0x00062CC0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public string ToString(string format, IFormatProvider formatProvider)
		{
			return string.Format("uint4x2({0}, {1},  {2}, {3},  {4}, {5},  {6}, {7})", new object[]
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

		// Token: 0x0400010E RID: 270
		public uint4 c0;

		// Token: 0x0400010F RID: 271
		public uint4 c1;

		// Token: 0x04000110 RID: 272
		public static readonly uint4x2 zero;
	}
}

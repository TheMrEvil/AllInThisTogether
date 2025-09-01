using System;
using System.Runtime.CompilerServices;
using Unity.IL2CPP.CompilerServices;

namespace Unity.Mathematics
{
	// Token: 0x02000044 RID: 68
	[Il2CppEagerStaticClassConstruction]
	[Serializable]
	public struct uint3x3 : IEquatable<uint3x3>, IFormattable
	{
		// Token: 0x060020EC RID: 8428 RVA: 0x0005E16F File Offset: 0x0005C36F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public uint3x3(uint3 c0, uint3 c1, uint3 c2)
		{
			this.c0 = c0;
			this.c1 = c1;
			this.c2 = c2;
		}

		// Token: 0x060020ED RID: 8429 RVA: 0x0005E186 File Offset: 0x0005C386
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public uint3x3(uint m00, uint m01, uint m02, uint m10, uint m11, uint m12, uint m20, uint m21, uint m22)
		{
			this.c0 = new uint3(m00, m10, m20);
			this.c1 = new uint3(m01, m11, m21);
			this.c2 = new uint3(m02, m12, m22);
		}

		// Token: 0x060020EE RID: 8430 RVA: 0x0005E1B8 File Offset: 0x0005C3B8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public uint3x3(uint v)
		{
			this.c0 = v;
			this.c1 = v;
			this.c2 = v;
		}

		// Token: 0x060020EF RID: 8431 RVA: 0x0005E1E0 File Offset: 0x0005C3E0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public uint3x3(bool v)
		{
			this.c0 = math.select(new uint3(0U), new uint3(1U), v);
			this.c1 = math.select(new uint3(0U), new uint3(1U), v);
			this.c2 = math.select(new uint3(0U), new uint3(1U), v);
		}

		// Token: 0x060020F0 RID: 8432 RVA: 0x0005E238 File Offset: 0x0005C438
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public uint3x3(bool3x3 v)
		{
			this.c0 = math.select(new uint3(0U), new uint3(1U), v.c0);
			this.c1 = math.select(new uint3(0U), new uint3(1U), v.c1);
			this.c2 = math.select(new uint3(0U), new uint3(1U), v.c2);
		}

		// Token: 0x060020F1 RID: 8433 RVA: 0x0005E29C File Offset: 0x0005C49C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public uint3x3(int v)
		{
			this.c0 = (uint3)v;
			this.c1 = (uint3)v;
			this.c2 = (uint3)v;
		}

		// Token: 0x060020F2 RID: 8434 RVA: 0x0005E2C2 File Offset: 0x0005C4C2
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public uint3x3(int3x3 v)
		{
			this.c0 = (uint3)v.c0;
			this.c1 = (uint3)v.c1;
			this.c2 = (uint3)v.c2;
		}

		// Token: 0x060020F3 RID: 8435 RVA: 0x0005E2F7 File Offset: 0x0005C4F7
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public uint3x3(float v)
		{
			this.c0 = (uint3)v;
			this.c1 = (uint3)v;
			this.c2 = (uint3)v;
		}

		// Token: 0x060020F4 RID: 8436 RVA: 0x0005E31D File Offset: 0x0005C51D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public uint3x3(float3x3 v)
		{
			this.c0 = (uint3)v.c0;
			this.c1 = (uint3)v.c1;
			this.c2 = (uint3)v.c2;
		}

		// Token: 0x060020F5 RID: 8437 RVA: 0x0005E352 File Offset: 0x0005C552
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public uint3x3(double v)
		{
			this.c0 = (uint3)v;
			this.c1 = (uint3)v;
			this.c2 = (uint3)v;
		}

		// Token: 0x060020F6 RID: 8438 RVA: 0x0005E378 File Offset: 0x0005C578
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public uint3x3(double3x3 v)
		{
			this.c0 = (uint3)v.c0;
			this.c1 = (uint3)v.c1;
			this.c2 = (uint3)v.c2;
		}

		// Token: 0x060020F7 RID: 8439 RVA: 0x0005E3AD File Offset: 0x0005C5AD
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator uint3x3(uint v)
		{
			return new uint3x3(v);
		}

		// Token: 0x060020F8 RID: 8440 RVA: 0x0005E3B5 File Offset: 0x0005C5B5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator uint3x3(bool v)
		{
			return new uint3x3(v);
		}

		// Token: 0x060020F9 RID: 8441 RVA: 0x0005E3BD File Offset: 0x0005C5BD
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator uint3x3(bool3x3 v)
		{
			return new uint3x3(v);
		}

		// Token: 0x060020FA RID: 8442 RVA: 0x0005E3C5 File Offset: 0x0005C5C5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator uint3x3(int v)
		{
			return new uint3x3(v);
		}

		// Token: 0x060020FB RID: 8443 RVA: 0x0005E3CD File Offset: 0x0005C5CD
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator uint3x3(int3x3 v)
		{
			return new uint3x3(v);
		}

		// Token: 0x060020FC RID: 8444 RVA: 0x0005E3D5 File Offset: 0x0005C5D5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator uint3x3(float v)
		{
			return new uint3x3(v);
		}

		// Token: 0x060020FD RID: 8445 RVA: 0x0005E3DD File Offset: 0x0005C5DD
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator uint3x3(float3x3 v)
		{
			return new uint3x3(v);
		}

		// Token: 0x060020FE RID: 8446 RVA: 0x0005E3E5 File Offset: 0x0005C5E5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator uint3x3(double v)
		{
			return new uint3x3(v);
		}

		// Token: 0x060020FF RID: 8447 RVA: 0x0005E3ED File Offset: 0x0005C5ED
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator uint3x3(double3x3 v)
		{
			return new uint3x3(v);
		}

		// Token: 0x06002100 RID: 8448 RVA: 0x0005E3F5 File Offset: 0x0005C5F5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3x3 operator *(uint3x3 lhs, uint3x3 rhs)
		{
			return new uint3x3(lhs.c0 * rhs.c0, lhs.c1 * rhs.c1, lhs.c2 * rhs.c2);
		}

		// Token: 0x06002101 RID: 8449 RVA: 0x0005E42F File Offset: 0x0005C62F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3x3 operator *(uint3x3 lhs, uint rhs)
		{
			return new uint3x3(lhs.c0 * rhs, lhs.c1 * rhs, lhs.c2 * rhs);
		}

		// Token: 0x06002102 RID: 8450 RVA: 0x0005E45A File Offset: 0x0005C65A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3x3 operator *(uint lhs, uint3x3 rhs)
		{
			return new uint3x3(lhs * rhs.c0, lhs * rhs.c1, lhs * rhs.c2);
		}

		// Token: 0x06002103 RID: 8451 RVA: 0x0005E485 File Offset: 0x0005C685
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3x3 operator +(uint3x3 lhs, uint3x3 rhs)
		{
			return new uint3x3(lhs.c0 + rhs.c0, lhs.c1 + rhs.c1, lhs.c2 + rhs.c2);
		}

		// Token: 0x06002104 RID: 8452 RVA: 0x0005E4BF File Offset: 0x0005C6BF
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3x3 operator +(uint3x3 lhs, uint rhs)
		{
			return new uint3x3(lhs.c0 + rhs, lhs.c1 + rhs, lhs.c2 + rhs);
		}

		// Token: 0x06002105 RID: 8453 RVA: 0x0005E4EA File Offset: 0x0005C6EA
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3x3 operator +(uint lhs, uint3x3 rhs)
		{
			return new uint3x3(lhs + rhs.c0, lhs + rhs.c1, lhs + rhs.c2);
		}

		// Token: 0x06002106 RID: 8454 RVA: 0x0005E515 File Offset: 0x0005C715
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3x3 operator -(uint3x3 lhs, uint3x3 rhs)
		{
			return new uint3x3(lhs.c0 - rhs.c0, lhs.c1 - rhs.c1, lhs.c2 - rhs.c2);
		}

		// Token: 0x06002107 RID: 8455 RVA: 0x0005E54F File Offset: 0x0005C74F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3x3 operator -(uint3x3 lhs, uint rhs)
		{
			return new uint3x3(lhs.c0 - rhs, lhs.c1 - rhs, lhs.c2 - rhs);
		}

		// Token: 0x06002108 RID: 8456 RVA: 0x0005E57A File Offset: 0x0005C77A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3x3 operator -(uint lhs, uint3x3 rhs)
		{
			return new uint3x3(lhs - rhs.c0, lhs - rhs.c1, lhs - rhs.c2);
		}

		// Token: 0x06002109 RID: 8457 RVA: 0x0005E5A5 File Offset: 0x0005C7A5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3x3 operator /(uint3x3 lhs, uint3x3 rhs)
		{
			return new uint3x3(lhs.c0 / rhs.c0, lhs.c1 / rhs.c1, lhs.c2 / rhs.c2);
		}

		// Token: 0x0600210A RID: 8458 RVA: 0x0005E5DF File Offset: 0x0005C7DF
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3x3 operator /(uint3x3 lhs, uint rhs)
		{
			return new uint3x3(lhs.c0 / rhs, lhs.c1 / rhs, lhs.c2 / rhs);
		}

		// Token: 0x0600210B RID: 8459 RVA: 0x0005E60A File Offset: 0x0005C80A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3x3 operator /(uint lhs, uint3x3 rhs)
		{
			return new uint3x3(lhs / rhs.c0, lhs / rhs.c1, lhs / rhs.c2);
		}

		// Token: 0x0600210C RID: 8460 RVA: 0x0005E635 File Offset: 0x0005C835
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3x3 operator %(uint3x3 lhs, uint3x3 rhs)
		{
			return new uint3x3(lhs.c0 % rhs.c0, lhs.c1 % rhs.c1, lhs.c2 % rhs.c2);
		}

		// Token: 0x0600210D RID: 8461 RVA: 0x0005E66F File Offset: 0x0005C86F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3x3 operator %(uint3x3 lhs, uint rhs)
		{
			return new uint3x3(lhs.c0 % rhs, lhs.c1 % rhs, lhs.c2 % rhs);
		}

		// Token: 0x0600210E RID: 8462 RVA: 0x0005E69A File Offset: 0x0005C89A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3x3 operator %(uint lhs, uint3x3 rhs)
		{
			return new uint3x3(lhs % rhs.c0, lhs % rhs.c1, lhs % rhs.c2);
		}

		// Token: 0x0600210F RID: 8463 RVA: 0x0005E6C8 File Offset: 0x0005C8C8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3x3 operator ++(uint3x3 val)
		{
			uint3 @uint = ++val.c0;
			val.c0 = @uint;
			uint3 uint2 = @uint;
			@uint = ++val.c1;
			val.c1 = @uint;
			uint3 uint3 = @uint;
			@uint = ++val.c2;
			val.c2 = @uint;
			return new uint3x3(uint2, uint3, @uint);
		}

		// Token: 0x06002110 RID: 8464 RVA: 0x0005E728 File Offset: 0x0005C928
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3x3 operator --(uint3x3 val)
		{
			uint3 @uint = --val.c0;
			val.c0 = @uint;
			uint3 uint2 = @uint;
			@uint = --val.c1;
			val.c1 = @uint;
			uint3 uint3 = @uint;
			@uint = --val.c2;
			val.c2 = @uint;
			return new uint3x3(uint2, uint3, @uint);
		}

		// Token: 0x06002111 RID: 8465 RVA: 0x0005E788 File Offset: 0x0005C988
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x3 operator <(uint3x3 lhs, uint3x3 rhs)
		{
			return new bool3x3(lhs.c0 < rhs.c0, lhs.c1 < rhs.c1, lhs.c2 < rhs.c2);
		}

		// Token: 0x06002112 RID: 8466 RVA: 0x0005E7C2 File Offset: 0x0005C9C2
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x3 operator <(uint3x3 lhs, uint rhs)
		{
			return new bool3x3(lhs.c0 < rhs, lhs.c1 < rhs, lhs.c2 < rhs);
		}

		// Token: 0x06002113 RID: 8467 RVA: 0x0005E7ED File Offset: 0x0005C9ED
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x3 operator <(uint lhs, uint3x3 rhs)
		{
			return new bool3x3(lhs < rhs.c0, lhs < rhs.c1, lhs < rhs.c2);
		}

		// Token: 0x06002114 RID: 8468 RVA: 0x0005E818 File Offset: 0x0005CA18
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x3 operator <=(uint3x3 lhs, uint3x3 rhs)
		{
			return new bool3x3(lhs.c0 <= rhs.c0, lhs.c1 <= rhs.c1, lhs.c2 <= rhs.c2);
		}

		// Token: 0x06002115 RID: 8469 RVA: 0x0005E852 File Offset: 0x0005CA52
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x3 operator <=(uint3x3 lhs, uint rhs)
		{
			return new bool3x3(lhs.c0 <= rhs, lhs.c1 <= rhs, lhs.c2 <= rhs);
		}

		// Token: 0x06002116 RID: 8470 RVA: 0x0005E87D File Offset: 0x0005CA7D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x3 operator <=(uint lhs, uint3x3 rhs)
		{
			return new bool3x3(lhs <= rhs.c0, lhs <= rhs.c1, lhs <= rhs.c2);
		}

		// Token: 0x06002117 RID: 8471 RVA: 0x0005E8A8 File Offset: 0x0005CAA8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x3 operator >(uint3x3 lhs, uint3x3 rhs)
		{
			return new bool3x3(lhs.c0 > rhs.c0, lhs.c1 > rhs.c1, lhs.c2 > rhs.c2);
		}

		// Token: 0x06002118 RID: 8472 RVA: 0x0005E8E2 File Offset: 0x0005CAE2
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x3 operator >(uint3x3 lhs, uint rhs)
		{
			return new bool3x3(lhs.c0 > rhs, lhs.c1 > rhs, lhs.c2 > rhs);
		}

		// Token: 0x06002119 RID: 8473 RVA: 0x0005E90D File Offset: 0x0005CB0D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x3 operator >(uint lhs, uint3x3 rhs)
		{
			return new bool3x3(lhs > rhs.c0, lhs > rhs.c1, lhs > rhs.c2);
		}

		// Token: 0x0600211A RID: 8474 RVA: 0x0005E938 File Offset: 0x0005CB38
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x3 operator >=(uint3x3 lhs, uint3x3 rhs)
		{
			return new bool3x3(lhs.c0 >= rhs.c0, lhs.c1 >= rhs.c1, lhs.c2 >= rhs.c2);
		}

		// Token: 0x0600211B RID: 8475 RVA: 0x0005E972 File Offset: 0x0005CB72
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x3 operator >=(uint3x3 lhs, uint rhs)
		{
			return new bool3x3(lhs.c0 >= rhs, lhs.c1 >= rhs, lhs.c2 >= rhs);
		}

		// Token: 0x0600211C RID: 8476 RVA: 0x0005E99D File Offset: 0x0005CB9D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x3 operator >=(uint lhs, uint3x3 rhs)
		{
			return new bool3x3(lhs >= rhs.c0, lhs >= rhs.c1, lhs >= rhs.c2);
		}

		// Token: 0x0600211D RID: 8477 RVA: 0x0005E9C8 File Offset: 0x0005CBC8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3x3 operator -(uint3x3 val)
		{
			return new uint3x3(-val.c0, -val.c1, -val.c2);
		}

		// Token: 0x0600211E RID: 8478 RVA: 0x0005E9F0 File Offset: 0x0005CBF0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3x3 operator +(uint3x3 val)
		{
			return new uint3x3(+val.c0, +val.c1, +val.c2);
		}

		// Token: 0x0600211F RID: 8479 RVA: 0x0005EA18 File Offset: 0x0005CC18
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3x3 operator <<(uint3x3 x, int n)
		{
			return new uint3x3(x.c0 << n, x.c1 << n, x.c2 << n);
		}

		// Token: 0x06002120 RID: 8480 RVA: 0x0005EA43 File Offset: 0x0005CC43
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3x3 operator >>(uint3x3 x, int n)
		{
			return new uint3x3(x.c0 >> n, x.c1 >> n, x.c2 >> n);
		}

		// Token: 0x06002121 RID: 8481 RVA: 0x0005EA6E File Offset: 0x0005CC6E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x3 operator ==(uint3x3 lhs, uint3x3 rhs)
		{
			return new bool3x3(lhs.c0 == rhs.c0, lhs.c1 == rhs.c1, lhs.c2 == rhs.c2);
		}

		// Token: 0x06002122 RID: 8482 RVA: 0x0005EAA8 File Offset: 0x0005CCA8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x3 operator ==(uint3x3 lhs, uint rhs)
		{
			return new bool3x3(lhs.c0 == rhs, lhs.c1 == rhs, lhs.c2 == rhs);
		}

		// Token: 0x06002123 RID: 8483 RVA: 0x0005EAD3 File Offset: 0x0005CCD3
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x3 operator ==(uint lhs, uint3x3 rhs)
		{
			return new bool3x3(lhs == rhs.c0, lhs == rhs.c1, lhs == rhs.c2);
		}

		// Token: 0x06002124 RID: 8484 RVA: 0x0005EAFE File Offset: 0x0005CCFE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x3 operator !=(uint3x3 lhs, uint3x3 rhs)
		{
			return new bool3x3(lhs.c0 != rhs.c0, lhs.c1 != rhs.c1, lhs.c2 != rhs.c2);
		}

		// Token: 0x06002125 RID: 8485 RVA: 0x0005EB38 File Offset: 0x0005CD38
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x3 operator !=(uint3x3 lhs, uint rhs)
		{
			return new bool3x3(lhs.c0 != rhs, lhs.c1 != rhs, lhs.c2 != rhs);
		}

		// Token: 0x06002126 RID: 8486 RVA: 0x0005EB63 File Offset: 0x0005CD63
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x3 operator !=(uint lhs, uint3x3 rhs)
		{
			return new bool3x3(lhs != rhs.c0, lhs != rhs.c1, lhs != rhs.c2);
		}

		// Token: 0x06002127 RID: 8487 RVA: 0x0005EB8E File Offset: 0x0005CD8E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3x3 operator ~(uint3x3 val)
		{
			return new uint3x3(~val.c0, ~val.c1, ~val.c2);
		}

		// Token: 0x06002128 RID: 8488 RVA: 0x0005EBB6 File Offset: 0x0005CDB6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3x3 operator &(uint3x3 lhs, uint3x3 rhs)
		{
			return new uint3x3(lhs.c0 & rhs.c0, lhs.c1 & rhs.c1, lhs.c2 & rhs.c2);
		}

		// Token: 0x06002129 RID: 8489 RVA: 0x0005EBF0 File Offset: 0x0005CDF0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3x3 operator &(uint3x3 lhs, uint rhs)
		{
			return new uint3x3(lhs.c0 & rhs, lhs.c1 & rhs, lhs.c2 & rhs);
		}

		// Token: 0x0600212A RID: 8490 RVA: 0x0005EC1B File Offset: 0x0005CE1B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3x3 operator &(uint lhs, uint3x3 rhs)
		{
			return new uint3x3(lhs & rhs.c0, lhs & rhs.c1, lhs & rhs.c2);
		}

		// Token: 0x0600212B RID: 8491 RVA: 0x0005EC46 File Offset: 0x0005CE46
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3x3 operator |(uint3x3 lhs, uint3x3 rhs)
		{
			return new uint3x3(lhs.c0 | rhs.c0, lhs.c1 | rhs.c1, lhs.c2 | rhs.c2);
		}

		// Token: 0x0600212C RID: 8492 RVA: 0x0005EC80 File Offset: 0x0005CE80
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3x3 operator |(uint3x3 lhs, uint rhs)
		{
			return new uint3x3(lhs.c0 | rhs, lhs.c1 | rhs, lhs.c2 | rhs);
		}

		// Token: 0x0600212D RID: 8493 RVA: 0x0005ECAB File Offset: 0x0005CEAB
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3x3 operator |(uint lhs, uint3x3 rhs)
		{
			return new uint3x3(lhs | rhs.c0, lhs | rhs.c1, lhs | rhs.c2);
		}

		// Token: 0x0600212E RID: 8494 RVA: 0x0005ECD6 File Offset: 0x0005CED6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3x3 operator ^(uint3x3 lhs, uint3x3 rhs)
		{
			return new uint3x3(lhs.c0 ^ rhs.c0, lhs.c1 ^ rhs.c1, lhs.c2 ^ rhs.c2);
		}

		// Token: 0x0600212F RID: 8495 RVA: 0x0005ED10 File Offset: 0x0005CF10
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3x3 operator ^(uint3x3 lhs, uint rhs)
		{
			return new uint3x3(lhs.c0 ^ rhs, lhs.c1 ^ rhs, lhs.c2 ^ rhs);
		}

		// Token: 0x06002130 RID: 8496 RVA: 0x0005ED3B File Offset: 0x0005CF3B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3x3 operator ^(uint lhs, uint3x3 rhs)
		{
			return new uint3x3(lhs ^ rhs.c0, lhs ^ rhs.c1, lhs ^ rhs.c2);
		}

		// Token: 0x17000A34 RID: 2612
		public unsafe uint3 this[int index]
		{
			get
			{
				fixed (uint3x3* ptr = &this)
				{
					return ref *(uint3*)(ptr + (IntPtr)index * (IntPtr)sizeof(uint3) / (IntPtr)sizeof(uint3x3));
				}
			}
		}

		// Token: 0x06002132 RID: 8498 RVA: 0x0005ED83 File Offset: 0x0005CF83
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool Equals(uint3x3 rhs)
		{
			return this.c0.Equals(rhs.c0) && this.c1.Equals(rhs.c1) && this.c2.Equals(rhs.c2);
		}

		// Token: 0x06002133 RID: 8499 RVA: 0x0005EDC0 File Offset: 0x0005CFC0
		public override bool Equals(object o)
		{
			if (o is uint3x3)
			{
				uint3x3 rhs = (uint3x3)o;
				return this.Equals(rhs);
			}
			return false;
		}

		// Token: 0x06002134 RID: 8500 RVA: 0x0005EDE5 File Offset: 0x0005CFE5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override int GetHashCode()
		{
			return (int)math.hash(this);
		}

		// Token: 0x06002135 RID: 8501 RVA: 0x0005EDF4 File Offset: 0x0005CFF4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override string ToString()
		{
			return string.Format("uint3x3({0}, {1}, {2},  {3}, {4}, {5},  {6}, {7}, {8})", new object[]
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

		// Token: 0x06002136 RID: 8502 RVA: 0x0005EEC0 File Offset: 0x0005D0C0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public string ToString(string format, IFormatProvider formatProvider)
		{
			return string.Format("uint3x3({0}, {1}, {2},  {3}, {4}, {5},  {6}, {7}, {8})", new object[]
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

		// Token: 0x06002137 RID: 8503 RVA: 0x0005EF9C File Offset: 0x0005D19C
		// Note: this type is marked as 'beforefieldinit'.
		static uint3x3()
		{
		}

		// Token: 0x040000FF RID: 255
		public uint3 c0;

		// Token: 0x04000100 RID: 256
		public uint3 c1;

		// Token: 0x04000101 RID: 257
		public uint3 c2;

		// Token: 0x04000102 RID: 258
		public static readonly uint3x3 identity = new uint3x3(1U, 0U, 0U, 0U, 1U, 0U, 0U, 0U, 1U);

		// Token: 0x04000103 RID: 259
		public static readonly uint3x3 zero;
	}
}

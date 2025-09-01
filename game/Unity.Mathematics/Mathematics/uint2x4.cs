using System;
using System.Runtime.CompilerServices;
using Unity.IL2CPP.CompilerServices;

namespace Unity.Mathematics
{
	// Token: 0x02000041 RID: 65
	[Il2CppEagerStaticClassConstruction]
	[Serializable]
	public struct uint2x4 : IEquatable<uint2x4>, IFormattable
	{
		// Token: 0x06001F87 RID: 8071 RVA: 0x0005ABE3 File Offset: 0x00058DE3
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public uint2x4(uint2 c0, uint2 c1, uint2 c2, uint2 c3)
		{
			this.c0 = c0;
			this.c1 = c1;
			this.c2 = c2;
			this.c3 = c3;
		}

		// Token: 0x06001F88 RID: 8072 RVA: 0x0005AC02 File Offset: 0x00058E02
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public uint2x4(uint m00, uint m01, uint m02, uint m03, uint m10, uint m11, uint m12, uint m13)
		{
			this.c0 = new uint2(m00, m10);
			this.c1 = new uint2(m01, m11);
			this.c2 = new uint2(m02, m12);
			this.c3 = new uint2(m03, m13);
		}

		// Token: 0x06001F89 RID: 8073 RVA: 0x0005AC3D File Offset: 0x00058E3D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public uint2x4(uint v)
		{
			this.c0 = v;
			this.c1 = v;
			this.c2 = v;
			this.c3 = v;
		}

		// Token: 0x06001F8A RID: 8074 RVA: 0x0005AC70 File Offset: 0x00058E70
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public uint2x4(bool v)
		{
			this.c0 = math.select(new uint2(0U), new uint2(1U), v);
			this.c1 = math.select(new uint2(0U), new uint2(1U), v);
			this.c2 = math.select(new uint2(0U), new uint2(1U), v);
			this.c3 = math.select(new uint2(0U), new uint2(1U), v);
		}

		// Token: 0x06001F8B RID: 8075 RVA: 0x0005ACE0 File Offset: 0x00058EE0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public uint2x4(bool2x4 v)
		{
			this.c0 = math.select(new uint2(0U), new uint2(1U), v.c0);
			this.c1 = math.select(new uint2(0U), new uint2(1U), v.c1);
			this.c2 = math.select(new uint2(0U), new uint2(1U), v.c2);
			this.c3 = math.select(new uint2(0U), new uint2(1U), v.c3);
		}

		// Token: 0x06001F8C RID: 8076 RVA: 0x0005AD61 File Offset: 0x00058F61
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public uint2x4(int v)
		{
			this.c0 = (uint2)v;
			this.c1 = (uint2)v;
			this.c2 = (uint2)v;
			this.c3 = (uint2)v;
		}

		// Token: 0x06001F8D RID: 8077 RVA: 0x0005AD94 File Offset: 0x00058F94
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public uint2x4(int2x4 v)
		{
			this.c0 = (uint2)v.c0;
			this.c1 = (uint2)v.c1;
			this.c2 = (uint2)v.c2;
			this.c3 = (uint2)v.c3;
		}

		// Token: 0x06001F8E RID: 8078 RVA: 0x0005ADE5 File Offset: 0x00058FE5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public uint2x4(float v)
		{
			this.c0 = (uint2)v;
			this.c1 = (uint2)v;
			this.c2 = (uint2)v;
			this.c3 = (uint2)v;
		}

		// Token: 0x06001F8F RID: 8079 RVA: 0x0005AE18 File Offset: 0x00059018
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public uint2x4(float2x4 v)
		{
			this.c0 = (uint2)v.c0;
			this.c1 = (uint2)v.c1;
			this.c2 = (uint2)v.c2;
			this.c3 = (uint2)v.c3;
		}

		// Token: 0x06001F90 RID: 8080 RVA: 0x0005AE69 File Offset: 0x00059069
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public uint2x4(double v)
		{
			this.c0 = (uint2)v;
			this.c1 = (uint2)v;
			this.c2 = (uint2)v;
			this.c3 = (uint2)v;
		}

		// Token: 0x06001F91 RID: 8081 RVA: 0x0005AE9C File Offset: 0x0005909C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public uint2x4(double2x4 v)
		{
			this.c0 = (uint2)v.c0;
			this.c1 = (uint2)v.c1;
			this.c2 = (uint2)v.c2;
			this.c3 = (uint2)v.c3;
		}

		// Token: 0x06001F92 RID: 8082 RVA: 0x0005AEED File Offset: 0x000590ED
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator uint2x4(uint v)
		{
			return new uint2x4(v);
		}

		// Token: 0x06001F93 RID: 8083 RVA: 0x0005AEF5 File Offset: 0x000590F5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator uint2x4(bool v)
		{
			return new uint2x4(v);
		}

		// Token: 0x06001F94 RID: 8084 RVA: 0x0005AEFD File Offset: 0x000590FD
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator uint2x4(bool2x4 v)
		{
			return new uint2x4(v);
		}

		// Token: 0x06001F95 RID: 8085 RVA: 0x0005AF05 File Offset: 0x00059105
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator uint2x4(int v)
		{
			return new uint2x4(v);
		}

		// Token: 0x06001F96 RID: 8086 RVA: 0x0005AF0D File Offset: 0x0005910D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator uint2x4(int2x4 v)
		{
			return new uint2x4(v);
		}

		// Token: 0x06001F97 RID: 8087 RVA: 0x0005AF15 File Offset: 0x00059115
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator uint2x4(float v)
		{
			return new uint2x4(v);
		}

		// Token: 0x06001F98 RID: 8088 RVA: 0x0005AF1D File Offset: 0x0005911D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator uint2x4(float2x4 v)
		{
			return new uint2x4(v);
		}

		// Token: 0x06001F99 RID: 8089 RVA: 0x0005AF25 File Offset: 0x00059125
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator uint2x4(double v)
		{
			return new uint2x4(v);
		}

		// Token: 0x06001F9A RID: 8090 RVA: 0x0005AF2D File Offset: 0x0005912D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator uint2x4(double2x4 v)
		{
			return new uint2x4(v);
		}

		// Token: 0x06001F9B RID: 8091 RVA: 0x0005AF38 File Offset: 0x00059138
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2x4 operator *(uint2x4 lhs, uint2x4 rhs)
		{
			return new uint2x4(lhs.c0 * rhs.c0, lhs.c1 * rhs.c1, lhs.c2 * rhs.c2, lhs.c3 * rhs.c3);
		}

		// Token: 0x06001F9C RID: 8092 RVA: 0x0005AF8E File Offset: 0x0005918E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2x4 operator *(uint2x4 lhs, uint rhs)
		{
			return new uint2x4(lhs.c0 * rhs, lhs.c1 * rhs, lhs.c2 * rhs, lhs.c3 * rhs);
		}

		// Token: 0x06001F9D RID: 8093 RVA: 0x0005AFC5 File Offset: 0x000591C5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2x4 operator *(uint lhs, uint2x4 rhs)
		{
			return new uint2x4(lhs * rhs.c0, lhs * rhs.c1, lhs * rhs.c2, lhs * rhs.c3);
		}

		// Token: 0x06001F9E RID: 8094 RVA: 0x0005AFFC File Offset: 0x000591FC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2x4 operator +(uint2x4 lhs, uint2x4 rhs)
		{
			return new uint2x4(lhs.c0 + rhs.c0, lhs.c1 + rhs.c1, lhs.c2 + rhs.c2, lhs.c3 + rhs.c3);
		}

		// Token: 0x06001F9F RID: 8095 RVA: 0x0005B052 File Offset: 0x00059252
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2x4 operator +(uint2x4 lhs, uint rhs)
		{
			return new uint2x4(lhs.c0 + rhs, lhs.c1 + rhs, lhs.c2 + rhs, lhs.c3 + rhs);
		}

		// Token: 0x06001FA0 RID: 8096 RVA: 0x0005B089 File Offset: 0x00059289
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2x4 operator +(uint lhs, uint2x4 rhs)
		{
			return new uint2x4(lhs + rhs.c0, lhs + rhs.c1, lhs + rhs.c2, lhs + rhs.c3);
		}

		// Token: 0x06001FA1 RID: 8097 RVA: 0x0005B0C0 File Offset: 0x000592C0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2x4 operator -(uint2x4 lhs, uint2x4 rhs)
		{
			return new uint2x4(lhs.c0 - rhs.c0, lhs.c1 - rhs.c1, lhs.c2 - rhs.c2, lhs.c3 - rhs.c3);
		}

		// Token: 0x06001FA2 RID: 8098 RVA: 0x0005B116 File Offset: 0x00059316
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2x4 operator -(uint2x4 lhs, uint rhs)
		{
			return new uint2x4(lhs.c0 - rhs, lhs.c1 - rhs, lhs.c2 - rhs, lhs.c3 - rhs);
		}

		// Token: 0x06001FA3 RID: 8099 RVA: 0x0005B14D File Offset: 0x0005934D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2x4 operator -(uint lhs, uint2x4 rhs)
		{
			return new uint2x4(lhs - rhs.c0, lhs - rhs.c1, lhs - rhs.c2, lhs - rhs.c3);
		}

		// Token: 0x06001FA4 RID: 8100 RVA: 0x0005B184 File Offset: 0x00059384
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2x4 operator /(uint2x4 lhs, uint2x4 rhs)
		{
			return new uint2x4(lhs.c0 / rhs.c0, lhs.c1 / rhs.c1, lhs.c2 / rhs.c2, lhs.c3 / rhs.c3);
		}

		// Token: 0x06001FA5 RID: 8101 RVA: 0x0005B1DA File Offset: 0x000593DA
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2x4 operator /(uint2x4 lhs, uint rhs)
		{
			return new uint2x4(lhs.c0 / rhs, lhs.c1 / rhs, lhs.c2 / rhs, lhs.c3 / rhs);
		}

		// Token: 0x06001FA6 RID: 8102 RVA: 0x0005B211 File Offset: 0x00059411
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2x4 operator /(uint lhs, uint2x4 rhs)
		{
			return new uint2x4(lhs / rhs.c0, lhs / rhs.c1, lhs / rhs.c2, lhs / rhs.c3);
		}

		// Token: 0x06001FA7 RID: 8103 RVA: 0x0005B248 File Offset: 0x00059448
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2x4 operator %(uint2x4 lhs, uint2x4 rhs)
		{
			return new uint2x4(lhs.c0 % rhs.c0, lhs.c1 % rhs.c1, lhs.c2 % rhs.c2, lhs.c3 % rhs.c3);
		}

		// Token: 0x06001FA8 RID: 8104 RVA: 0x0005B29E File Offset: 0x0005949E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2x4 operator %(uint2x4 lhs, uint rhs)
		{
			return new uint2x4(lhs.c0 % rhs, lhs.c1 % rhs, lhs.c2 % rhs, lhs.c3 % rhs);
		}

		// Token: 0x06001FA9 RID: 8105 RVA: 0x0005B2D5 File Offset: 0x000594D5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2x4 operator %(uint lhs, uint2x4 rhs)
		{
			return new uint2x4(lhs % rhs.c0, lhs % rhs.c1, lhs % rhs.c2, lhs % rhs.c3);
		}

		// Token: 0x06001FAA RID: 8106 RVA: 0x0005B30C File Offset: 0x0005950C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2x4 operator ++(uint2x4 val)
		{
			uint2 @uint = ++val.c0;
			val.c0 = @uint;
			uint2 uint2 = @uint;
			@uint = ++val.c1;
			val.c1 = @uint;
			uint2 uint3 = @uint;
			@uint = ++val.c2;
			val.c2 = @uint;
			uint2 uint4 = @uint;
			@uint = ++val.c3;
			val.c3 = @uint;
			return new uint2x4(uint2, uint3, uint4, @uint);
		}

		// Token: 0x06001FAB RID: 8107 RVA: 0x0005B388 File Offset: 0x00059588
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2x4 operator --(uint2x4 val)
		{
			uint2 @uint = --val.c0;
			val.c0 = @uint;
			uint2 uint2 = @uint;
			@uint = --val.c1;
			val.c1 = @uint;
			uint2 uint3 = @uint;
			@uint = --val.c2;
			val.c2 = @uint;
			uint2 uint4 = @uint;
			@uint = --val.c3;
			val.c3 = @uint;
			return new uint2x4(uint2, uint3, uint4, @uint);
		}

		// Token: 0x06001FAC RID: 8108 RVA: 0x0005B404 File Offset: 0x00059604
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x4 operator <(uint2x4 lhs, uint2x4 rhs)
		{
			return new bool2x4(lhs.c0 < rhs.c0, lhs.c1 < rhs.c1, lhs.c2 < rhs.c2, lhs.c3 < rhs.c3);
		}

		// Token: 0x06001FAD RID: 8109 RVA: 0x0005B45A File Offset: 0x0005965A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x4 operator <(uint2x4 lhs, uint rhs)
		{
			return new bool2x4(lhs.c0 < rhs, lhs.c1 < rhs, lhs.c2 < rhs, lhs.c3 < rhs);
		}

		// Token: 0x06001FAE RID: 8110 RVA: 0x0005B491 File Offset: 0x00059691
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x4 operator <(uint lhs, uint2x4 rhs)
		{
			return new bool2x4(lhs < rhs.c0, lhs < rhs.c1, lhs < rhs.c2, lhs < rhs.c3);
		}

		// Token: 0x06001FAF RID: 8111 RVA: 0x0005B4C8 File Offset: 0x000596C8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x4 operator <=(uint2x4 lhs, uint2x4 rhs)
		{
			return new bool2x4(lhs.c0 <= rhs.c0, lhs.c1 <= rhs.c1, lhs.c2 <= rhs.c2, lhs.c3 <= rhs.c3);
		}

		// Token: 0x06001FB0 RID: 8112 RVA: 0x0005B51E File Offset: 0x0005971E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x4 operator <=(uint2x4 lhs, uint rhs)
		{
			return new bool2x4(lhs.c0 <= rhs, lhs.c1 <= rhs, lhs.c2 <= rhs, lhs.c3 <= rhs);
		}

		// Token: 0x06001FB1 RID: 8113 RVA: 0x0005B555 File Offset: 0x00059755
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x4 operator <=(uint lhs, uint2x4 rhs)
		{
			return new bool2x4(lhs <= rhs.c0, lhs <= rhs.c1, lhs <= rhs.c2, lhs <= rhs.c3);
		}

		// Token: 0x06001FB2 RID: 8114 RVA: 0x0005B58C File Offset: 0x0005978C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x4 operator >(uint2x4 lhs, uint2x4 rhs)
		{
			return new bool2x4(lhs.c0 > rhs.c0, lhs.c1 > rhs.c1, lhs.c2 > rhs.c2, lhs.c3 > rhs.c3);
		}

		// Token: 0x06001FB3 RID: 8115 RVA: 0x0005B5E2 File Offset: 0x000597E2
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x4 operator >(uint2x4 lhs, uint rhs)
		{
			return new bool2x4(lhs.c0 > rhs, lhs.c1 > rhs, lhs.c2 > rhs, lhs.c3 > rhs);
		}

		// Token: 0x06001FB4 RID: 8116 RVA: 0x0005B619 File Offset: 0x00059819
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x4 operator >(uint lhs, uint2x4 rhs)
		{
			return new bool2x4(lhs > rhs.c0, lhs > rhs.c1, lhs > rhs.c2, lhs > rhs.c3);
		}

		// Token: 0x06001FB5 RID: 8117 RVA: 0x0005B650 File Offset: 0x00059850
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x4 operator >=(uint2x4 lhs, uint2x4 rhs)
		{
			return new bool2x4(lhs.c0 >= rhs.c0, lhs.c1 >= rhs.c1, lhs.c2 >= rhs.c2, lhs.c3 >= rhs.c3);
		}

		// Token: 0x06001FB6 RID: 8118 RVA: 0x0005B6A6 File Offset: 0x000598A6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x4 operator >=(uint2x4 lhs, uint rhs)
		{
			return new bool2x4(lhs.c0 >= rhs, lhs.c1 >= rhs, lhs.c2 >= rhs, lhs.c3 >= rhs);
		}

		// Token: 0x06001FB7 RID: 8119 RVA: 0x0005B6DD File Offset: 0x000598DD
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x4 operator >=(uint lhs, uint2x4 rhs)
		{
			return new bool2x4(lhs >= rhs.c0, lhs >= rhs.c1, lhs >= rhs.c2, lhs >= rhs.c3);
		}

		// Token: 0x06001FB8 RID: 8120 RVA: 0x0005B714 File Offset: 0x00059914
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2x4 operator -(uint2x4 val)
		{
			return new uint2x4(-val.c0, -val.c1, -val.c2, -val.c3);
		}

		// Token: 0x06001FB9 RID: 8121 RVA: 0x0005B747 File Offset: 0x00059947
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2x4 operator +(uint2x4 val)
		{
			return new uint2x4(+val.c0, +val.c1, +val.c2, +val.c3);
		}

		// Token: 0x06001FBA RID: 8122 RVA: 0x0005B77A File Offset: 0x0005997A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2x4 operator <<(uint2x4 x, int n)
		{
			return new uint2x4(x.c0 << n, x.c1 << n, x.c2 << n, x.c3 << n);
		}

		// Token: 0x06001FBB RID: 8123 RVA: 0x0005B7B1 File Offset: 0x000599B1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2x4 operator >>(uint2x4 x, int n)
		{
			return new uint2x4(x.c0 >> n, x.c1 >> n, x.c2 >> n, x.c3 >> n);
		}

		// Token: 0x06001FBC RID: 8124 RVA: 0x0005B7E8 File Offset: 0x000599E8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x4 operator ==(uint2x4 lhs, uint2x4 rhs)
		{
			return new bool2x4(lhs.c0 == rhs.c0, lhs.c1 == rhs.c1, lhs.c2 == rhs.c2, lhs.c3 == rhs.c3);
		}

		// Token: 0x06001FBD RID: 8125 RVA: 0x0005B83E File Offset: 0x00059A3E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x4 operator ==(uint2x4 lhs, uint rhs)
		{
			return new bool2x4(lhs.c0 == rhs, lhs.c1 == rhs, lhs.c2 == rhs, lhs.c3 == rhs);
		}

		// Token: 0x06001FBE RID: 8126 RVA: 0x0005B875 File Offset: 0x00059A75
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x4 operator ==(uint lhs, uint2x4 rhs)
		{
			return new bool2x4(lhs == rhs.c0, lhs == rhs.c1, lhs == rhs.c2, lhs == rhs.c3);
		}

		// Token: 0x06001FBF RID: 8127 RVA: 0x0005B8AC File Offset: 0x00059AAC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x4 operator !=(uint2x4 lhs, uint2x4 rhs)
		{
			return new bool2x4(lhs.c0 != rhs.c0, lhs.c1 != rhs.c1, lhs.c2 != rhs.c2, lhs.c3 != rhs.c3);
		}

		// Token: 0x06001FC0 RID: 8128 RVA: 0x0005B902 File Offset: 0x00059B02
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x4 operator !=(uint2x4 lhs, uint rhs)
		{
			return new bool2x4(lhs.c0 != rhs, lhs.c1 != rhs, lhs.c2 != rhs, lhs.c3 != rhs);
		}

		// Token: 0x06001FC1 RID: 8129 RVA: 0x0005B939 File Offset: 0x00059B39
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x4 operator !=(uint lhs, uint2x4 rhs)
		{
			return new bool2x4(lhs != rhs.c0, lhs != rhs.c1, lhs != rhs.c2, lhs != rhs.c3);
		}

		// Token: 0x06001FC2 RID: 8130 RVA: 0x0005B970 File Offset: 0x00059B70
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2x4 operator ~(uint2x4 val)
		{
			return new uint2x4(~val.c0, ~val.c1, ~val.c2, ~val.c3);
		}

		// Token: 0x06001FC3 RID: 8131 RVA: 0x0005B9A4 File Offset: 0x00059BA4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2x4 operator &(uint2x4 lhs, uint2x4 rhs)
		{
			return new uint2x4(lhs.c0 & rhs.c0, lhs.c1 & rhs.c1, lhs.c2 & rhs.c2, lhs.c3 & rhs.c3);
		}

		// Token: 0x06001FC4 RID: 8132 RVA: 0x0005B9FA File Offset: 0x00059BFA
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2x4 operator &(uint2x4 lhs, uint rhs)
		{
			return new uint2x4(lhs.c0 & rhs, lhs.c1 & rhs, lhs.c2 & rhs, lhs.c3 & rhs);
		}

		// Token: 0x06001FC5 RID: 8133 RVA: 0x0005BA31 File Offset: 0x00059C31
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2x4 operator &(uint lhs, uint2x4 rhs)
		{
			return new uint2x4(lhs & rhs.c0, lhs & rhs.c1, lhs & rhs.c2, lhs & rhs.c3);
		}

		// Token: 0x06001FC6 RID: 8134 RVA: 0x0005BA68 File Offset: 0x00059C68
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2x4 operator |(uint2x4 lhs, uint2x4 rhs)
		{
			return new uint2x4(lhs.c0 | rhs.c0, lhs.c1 | rhs.c1, lhs.c2 | rhs.c2, lhs.c3 | rhs.c3);
		}

		// Token: 0x06001FC7 RID: 8135 RVA: 0x0005BABE File Offset: 0x00059CBE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2x4 operator |(uint2x4 lhs, uint rhs)
		{
			return new uint2x4(lhs.c0 | rhs, lhs.c1 | rhs, lhs.c2 | rhs, lhs.c3 | rhs);
		}

		// Token: 0x06001FC8 RID: 8136 RVA: 0x0005BAF5 File Offset: 0x00059CF5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2x4 operator |(uint lhs, uint2x4 rhs)
		{
			return new uint2x4(lhs | rhs.c0, lhs | rhs.c1, lhs | rhs.c2, lhs | rhs.c3);
		}

		// Token: 0x06001FC9 RID: 8137 RVA: 0x0005BB2C File Offset: 0x00059D2C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2x4 operator ^(uint2x4 lhs, uint2x4 rhs)
		{
			return new uint2x4(lhs.c0 ^ rhs.c0, lhs.c1 ^ rhs.c1, lhs.c2 ^ rhs.c2, lhs.c3 ^ rhs.c3);
		}

		// Token: 0x06001FCA RID: 8138 RVA: 0x0005BB82 File Offset: 0x00059D82
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2x4 operator ^(uint2x4 lhs, uint rhs)
		{
			return new uint2x4(lhs.c0 ^ rhs, lhs.c1 ^ rhs, lhs.c2 ^ rhs, lhs.c3 ^ rhs);
		}

		// Token: 0x06001FCB RID: 8139 RVA: 0x0005BBB9 File Offset: 0x00059DB9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2x4 operator ^(uint lhs, uint2x4 rhs)
		{
			return new uint2x4(lhs ^ rhs.c0, lhs ^ rhs.c1, lhs ^ rhs.c2, lhs ^ rhs.c3);
		}

		// Token: 0x170009BC RID: 2492
		public unsafe uint2 this[int index]
		{
			get
			{
				fixed (uint2x4* ptr = &this)
				{
					return ref *(uint2*)(ptr + (IntPtr)index * (IntPtr)sizeof(uint2) / (IntPtr)sizeof(uint2x4));
				}
			}
		}

		// Token: 0x06001FCD RID: 8141 RVA: 0x0005BC0C File Offset: 0x00059E0C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool Equals(uint2x4 rhs)
		{
			return this.c0.Equals(rhs.c0) && this.c1.Equals(rhs.c1) && this.c2.Equals(rhs.c2) && this.c3.Equals(rhs.c3);
		}

		// Token: 0x06001FCE RID: 8142 RVA: 0x0005BC68 File Offset: 0x00059E68
		public override bool Equals(object o)
		{
			if (o is uint2x4)
			{
				uint2x4 rhs = (uint2x4)o;
				return this.Equals(rhs);
			}
			return false;
		}

		// Token: 0x06001FCF RID: 8143 RVA: 0x0005BC8D File Offset: 0x00059E8D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override int GetHashCode()
		{
			return (int)math.hash(this);
		}

		// Token: 0x06001FD0 RID: 8144 RVA: 0x0005BC9C File Offset: 0x00059E9C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override string ToString()
		{
			return string.Format("uint2x4({0}, {1}, {2}, {3},  {4}, {5}, {6}, {7})", new object[]
			{
				this.c0.x,
				this.c1.x,
				this.c2.x,
				this.c3.x,
				this.c0.y,
				this.c1.y,
				this.c2.y,
				this.c3.y
			});
		}

		// Token: 0x06001FD1 RID: 8145 RVA: 0x0005BD54 File Offset: 0x00059F54
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public string ToString(string format, IFormatProvider formatProvider)
		{
			return string.Format("uint2x4({0}, {1}, {2}, {3},  {4}, {5}, {6}, {7})", new object[]
			{
				this.c0.x.ToString(format, formatProvider),
				this.c1.x.ToString(format, formatProvider),
				this.c2.x.ToString(format, formatProvider),
				this.c3.x.ToString(format, formatProvider),
				this.c0.y.ToString(format, formatProvider),
				this.c1.y.ToString(format, formatProvider),
				this.c2.y.ToString(format, formatProvider),
				this.c3.y.ToString(format, formatProvider)
			});
		}

		// Token: 0x040000F3 RID: 243
		public uint2 c0;

		// Token: 0x040000F4 RID: 244
		public uint2 c1;

		// Token: 0x040000F5 RID: 245
		public uint2 c2;

		// Token: 0x040000F6 RID: 246
		public uint2 c3;

		// Token: 0x040000F7 RID: 247
		public static readonly uint2x4 zero;
	}
}

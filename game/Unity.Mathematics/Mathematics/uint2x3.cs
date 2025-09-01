using System;
using System.Runtime.CompilerServices;
using Unity.IL2CPP.CompilerServices;

namespace Unity.Mathematics
{
	// Token: 0x02000040 RID: 64
	[Il2CppEagerStaticClassConstruction]
	[Serializable]
	public struct uint2x3 : IEquatable<uint2x3>, IFormattable
	{
		// Token: 0x06001F3C RID: 7996 RVA: 0x00059E39 File Offset: 0x00058039
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public uint2x3(uint2 c0, uint2 c1, uint2 c2)
		{
			this.c0 = c0;
			this.c1 = c1;
			this.c2 = c2;
		}

		// Token: 0x06001F3D RID: 7997 RVA: 0x00059E50 File Offset: 0x00058050
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public uint2x3(uint m00, uint m01, uint m02, uint m10, uint m11, uint m12)
		{
			this.c0 = new uint2(m00, m10);
			this.c1 = new uint2(m01, m11);
			this.c2 = new uint2(m02, m12);
		}

		// Token: 0x06001F3E RID: 7998 RVA: 0x00059E7C File Offset: 0x0005807C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public uint2x3(uint v)
		{
			this.c0 = v;
			this.c1 = v;
			this.c2 = v;
		}

		// Token: 0x06001F3F RID: 7999 RVA: 0x00059EA4 File Offset: 0x000580A4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public uint2x3(bool v)
		{
			this.c0 = math.select(new uint2(0U), new uint2(1U), v);
			this.c1 = math.select(new uint2(0U), new uint2(1U), v);
			this.c2 = math.select(new uint2(0U), new uint2(1U), v);
		}

		// Token: 0x06001F40 RID: 8000 RVA: 0x00059EFC File Offset: 0x000580FC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public uint2x3(bool2x3 v)
		{
			this.c0 = math.select(new uint2(0U), new uint2(1U), v.c0);
			this.c1 = math.select(new uint2(0U), new uint2(1U), v.c1);
			this.c2 = math.select(new uint2(0U), new uint2(1U), v.c2);
		}

		// Token: 0x06001F41 RID: 8001 RVA: 0x00059F60 File Offset: 0x00058160
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public uint2x3(int v)
		{
			this.c0 = (uint2)v;
			this.c1 = (uint2)v;
			this.c2 = (uint2)v;
		}

		// Token: 0x06001F42 RID: 8002 RVA: 0x00059F86 File Offset: 0x00058186
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public uint2x3(int2x3 v)
		{
			this.c0 = (uint2)v.c0;
			this.c1 = (uint2)v.c1;
			this.c2 = (uint2)v.c2;
		}

		// Token: 0x06001F43 RID: 8003 RVA: 0x00059FBB File Offset: 0x000581BB
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public uint2x3(float v)
		{
			this.c0 = (uint2)v;
			this.c1 = (uint2)v;
			this.c2 = (uint2)v;
		}

		// Token: 0x06001F44 RID: 8004 RVA: 0x00059FE1 File Offset: 0x000581E1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public uint2x3(float2x3 v)
		{
			this.c0 = (uint2)v.c0;
			this.c1 = (uint2)v.c1;
			this.c2 = (uint2)v.c2;
		}

		// Token: 0x06001F45 RID: 8005 RVA: 0x0005A016 File Offset: 0x00058216
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public uint2x3(double v)
		{
			this.c0 = (uint2)v;
			this.c1 = (uint2)v;
			this.c2 = (uint2)v;
		}

		// Token: 0x06001F46 RID: 8006 RVA: 0x0005A03C File Offset: 0x0005823C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public uint2x3(double2x3 v)
		{
			this.c0 = (uint2)v.c0;
			this.c1 = (uint2)v.c1;
			this.c2 = (uint2)v.c2;
		}

		// Token: 0x06001F47 RID: 8007 RVA: 0x0005A071 File Offset: 0x00058271
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator uint2x3(uint v)
		{
			return new uint2x3(v);
		}

		// Token: 0x06001F48 RID: 8008 RVA: 0x0005A079 File Offset: 0x00058279
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator uint2x3(bool v)
		{
			return new uint2x3(v);
		}

		// Token: 0x06001F49 RID: 8009 RVA: 0x0005A081 File Offset: 0x00058281
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator uint2x3(bool2x3 v)
		{
			return new uint2x3(v);
		}

		// Token: 0x06001F4A RID: 8010 RVA: 0x0005A089 File Offset: 0x00058289
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator uint2x3(int v)
		{
			return new uint2x3(v);
		}

		// Token: 0x06001F4B RID: 8011 RVA: 0x0005A091 File Offset: 0x00058291
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator uint2x3(int2x3 v)
		{
			return new uint2x3(v);
		}

		// Token: 0x06001F4C RID: 8012 RVA: 0x0005A099 File Offset: 0x00058299
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator uint2x3(float v)
		{
			return new uint2x3(v);
		}

		// Token: 0x06001F4D RID: 8013 RVA: 0x0005A0A1 File Offset: 0x000582A1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator uint2x3(float2x3 v)
		{
			return new uint2x3(v);
		}

		// Token: 0x06001F4E RID: 8014 RVA: 0x0005A0A9 File Offset: 0x000582A9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator uint2x3(double v)
		{
			return new uint2x3(v);
		}

		// Token: 0x06001F4F RID: 8015 RVA: 0x0005A0B1 File Offset: 0x000582B1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator uint2x3(double2x3 v)
		{
			return new uint2x3(v);
		}

		// Token: 0x06001F50 RID: 8016 RVA: 0x0005A0B9 File Offset: 0x000582B9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2x3 operator *(uint2x3 lhs, uint2x3 rhs)
		{
			return new uint2x3(lhs.c0 * rhs.c0, lhs.c1 * rhs.c1, lhs.c2 * rhs.c2);
		}

		// Token: 0x06001F51 RID: 8017 RVA: 0x0005A0F3 File Offset: 0x000582F3
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2x3 operator *(uint2x3 lhs, uint rhs)
		{
			return new uint2x3(lhs.c0 * rhs, lhs.c1 * rhs, lhs.c2 * rhs);
		}

		// Token: 0x06001F52 RID: 8018 RVA: 0x0005A11E File Offset: 0x0005831E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2x3 operator *(uint lhs, uint2x3 rhs)
		{
			return new uint2x3(lhs * rhs.c0, lhs * rhs.c1, lhs * rhs.c2);
		}

		// Token: 0x06001F53 RID: 8019 RVA: 0x0005A149 File Offset: 0x00058349
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2x3 operator +(uint2x3 lhs, uint2x3 rhs)
		{
			return new uint2x3(lhs.c0 + rhs.c0, lhs.c1 + rhs.c1, lhs.c2 + rhs.c2);
		}

		// Token: 0x06001F54 RID: 8020 RVA: 0x0005A183 File Offset: 0x00058383
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2x3 operator +(uint2x3 lhs, uint rhs)
		{
			return new uint2x3(lhs.c0 + rhs, lhs.c1 + rhs, lhs.c2 + rhs);
		}

		// Token: 0x06001F55 RID: 8021 RVA: 0x0005A1AE File Offset: 0x000583AE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2x3 operator +(uint lhs, uint2x3 rhs)
		{
			return new uint2x3(lhs + rhs.c0, lhs + rhs.c1, lhs + rhs.c2);
		}

		// Token: 0x06001F56 RID: 8022 RVA: 0x0005A1D9 File Offset: 0x000583D9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2x3 operator -(uint2x3 lhs, uint2x3 rhs)
		{
			return new uint2x3(lhs.c0 - rhs.c0, lhs.c1 - rhs.c1, lhs.c2 - rhs.c2);
		}

		// Token: 0x06001F57 RID: 8023 RVA: 0x0005A213 File Offset: 0x00058413
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2x3 operator -(uint2x3 lhs, uint rhs)
		{
			return new uint2x3(lhs.c0 - rhs, lhs.c1 - rhs, lhs.c2 - rhs);
		}

		// Token: 0x06001F58 RID: 8024 RVA: 0x0005A23E File Offset: 0x0005843E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2x3 operator -(uint lhs, uint2x3 rhs)
		{
			return new uint2x3(lhs - rhs.c0, lhs - rhs.c1, lhs - rhs.c2);
		}

		// Token: 0x06001F59 RID: 8025 RVA: 0x0005A269 File Offset: 0x00058469
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2x3 operator /(uint2x3 lhs, uint2x3 rhs)
		{
			return new uint2x3(lhs.c0 / rhs.c0, lhs.c1 / rhs.c1, lhs.c2 / rhs.c2);
		}

		// Token: 0x06001F5A RID: 8026 RVA: 0x0005A2A3 File Offset: 0x000584A3
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2x3 operator /(uint2x3 lhs, uint rhs)
		{
			return new uint2x3(lhs.c0 / rhs, lhs.c1 / rhs, lhs.c2 / rhs);
		}

		// Token: 0x06001F5B RID: 8027 RVA: 0x0005A2CE File Offset: 0x000584CE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2x3 operator /(uint lhs, uint2x3 rhs)
		{
			return new uint2x3(lhs / rhs.c0, lhs / rhs.c1, lhs / rhs.c2);
		}

		// Token: 0x06001F5C RID: 8028 RVA: 0x0005A2F9 File Offset: 0x000584F9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2x3 operator %(uint2x3 lhs, uint2x3 rhs)
		{
			return new uint2x3(lhs.c0 % rhs.c0, lhs.c1 % rhs.c1, lhs.c2 % rhs.c2);
		}

		// Token: 0x06001F5D RID: 8029 RVA: 0x0005A333 File Offset: 0x00058533
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2x3 operator %(uint2x3 lhs, uint rhs)
		{
			return new uint2x3(lhs.c0 % rhs, lhs.c1 % rhs, lhs.c2 % rhs);
		}

		// Token: 0x06001F5E RID: 8030 RVA: 0x0005A35E File Offset: 0x0005855E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2x3 operator %(uint lhs, uint2x3 rhs)
		{
			return new uint2x3(lhs % rhs.c0, lhs % rhs.c1, lhs % rhs.c2);
		}

		// Token: 0x06001F5F RID: 8031 RVA: 0x0005A38C File Offset: 0x0005858C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2x3 operator ++(uint2x3 val)
		{
			uint2 @uint = ++val.c0;
			val.c0 = @uint;
			uint2 uint2 = @uint;
			@uint = ++val.c1;
			val.c1 = @uint;
			uint2 uint3 = @uint;
			@uint = ++val.c2;
			val.c2 = @uint;
			return new uint2x3(uint2, uint3, @uint);
		}

		// Token: 0x06001F60 RID: 8032 RVA: 0x0005A3EC File Offset: 0x000585EC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2x3 operator --(uint2x3 val)
		{
			uint2 @uint = --val.c0;
			val.c0 = @uint;
			uint2 uint2 = @uint;
			@uint = --val.c1;
			val.c1 = @uint;
			uint2 uint3 = @uint;
			@uint = --val.c2;
			val.c2 = @uint;
			return new uint2x3(uint2, uint3, @uint);
		}

		// Token: 0x06001F61 RID: 8033 RVA: 0x0005A44C File Offset: 0x0005864C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x3 operator <(uint2x3 lhs, uint2x3 rhs)
		{
			return new bool2x3(lhs.c0 < rhs.c0, lhs.c1 < rhs.c1, lhs.c2 < rhs.c2);
		}

		// Token: 0x06001F62 RID: 8034 RVA: 0x0005A486 File Offset: 0x00058686
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x3 operator <(uint2x3 lhs, uint rhs)
		{
			return new bool2x3(lhs.c0 < rhs, lhs.c1 < rhs, lhs.c2 < rhs);
		}

		// Token: 0x06001F63 RID: 8035 RVA: 0x0005A4B1 File Offset: 0x000586B1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x3 operator <(uint lhs, uint2x3 rhs)
		{
			return new bool2x3(lhs < rhs.c0, lhs < rhs.c1, lhs < rhs.c2);
		}

		// Token: 0x06001F64 RID: 8036 RVA: 0x0005A4DC File Offset: 0x000586DC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x3 operator <=(uint2x3 lhs, uint2x3 rhs)
		{
			return new bool2x3(lhs.c0 <= rhs.c0, lhs.c1 <= rhs.c1, lhs.c2 <= rhs.c2);
		}

		// Token: 0x06001F65 RID: 8037 RVA: 0x0005A516 File Offset: 0x00058716
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x3 operator <=(uint2x3 lhs, uint rhs)
		{
			return new bool2x3(lhs.c0 <= rhs, lhs.c1 <= rhs, lhs.c2 <= rhs);
		}

		// Token: 0x06001F66 RID: 8038 RVA: 0x0005A541 File Offset: 0x00058741
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x3 operator <=(uint lhs, uint2x3 rhs)
		{
			return new bool2x3(lhs <= rhs.c0, lhs <= rhs.c1, lhs <= rhs.c2);
		}

		// Token: 0x06001F67 RID: 8039 RVA: 0x0005A56C File Offset: 0x0005876C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x3 operator >(uint2x3 lhs, uint2x3 rhs)
		{
			return new bool2x3(lhs.c0 > rhs.c0, lhs.c1 > rhs.c1, lhs.c2 > rhs.c2);
		}

		// Token: 0x06001F68 RID: 8040 RVA: 0x0005A5A6 File Offset: 0x000587A6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x3 operator >(uint2x3 lhs, uint rhs)
		{
			return new bool2x3(lhs.c0 > rhs, lhs.c1 > rhs, lhs.c2 > rhs);
		}

		// Token: 0x06001F69 RID: 8041 RVA: 0x0005A5D1 File Offset: 0x000587D1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x3 operator >(uint lhs, uint2x3 rhs)
		{
			return new bool2x3(lhs > rhs.c0, lhs > rhs.c1, lhs > rhs.c2);
		}

		// Token: 0x06001F6A RID: 8042 RVA: 0x0005A5FC File Offset: 0x000587FC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x3 operator >=(uint2x3 lhs, uint2x3 rhs)
		{
			return new bool2x3(lhs.c0 >= rhs.c0, lhs.c1 >= rhs.c1, lhs.c2 >= rhs.c2);
		}

		// Token: 0x06001F6B RID: 8043 RVA: 0x0005A636 File Offset: 0x00058836
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x3 operator >=(uint2x3 lhs, uint rhs)
		{
			return new bool2x3(lhs.c0 >= rhs, lhs.c1 >= rhs, lhs.c2 >= rhs);
		}

		// Token: 0x06001F6C RID: 8044 RVA: 0x0005A661 File Offset: 0x00058861
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x3 operator >=(uint lhs, uint2x3 rhs)
		{
			return new bool2x3(lhs >= rhs.c0, lhs >= rhs.c1, lhs >= rhs.c2);
		}

		// Token: 0x06001F6D RID: 8045 RVA: 0x0005A68C File Offset: 0x0005888C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2x3 operator -(uint2x3 val)
		{
			return new uint2x3(-val.c0, -val.c1, -val.c2);
		}

		// Token: 0x06001F6E RID: 8046 RVA: 0x0005A6B4 File Offset: 0x000588B4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2x3 operator +(uint2x3 val)
		{
			return new uint2x3(+val.c0, +val.c1, +val.c2);
		}

		// Token: 0x06001F6F RID: 8047 RVA: 0x0005A6DC File Offset: 0x000588DC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2x3 operator <<(uint2x3 x, int n)
		{
			return new uint2x3(x.c0 << n, x.c1 << n, x.c2 << n);
		}

		// Token: 0x06001F70 RID: 8048 RVA: 0x0005A707 File Offset: 0x00058907
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2x3 operator >>(uint2x3 x, int n)
		{
			return new uint2x3(x.c0 >> n, x.c1 >> n, x.c2 >> n);
		}

		// Token: 0x06001F71 RID: 8049 RVA: 0x0005A732 File Offset: 0x00058932
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x3 operator ==(uint2x3 lhs, uint2x3 rhs)
		{
			return new bool2x3(lhs.c0 == rhs.c0, lhs.c1 == rhs.c1, lhs.c2 == rhs.c2);
		}

		// Token: 0x06001F72 RID: 8050 RVA: 0x0005A76C File Offset: 0x0005896C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x3 operator ==(uint2x3 lhs, uint rhs)
		{
			return new bool2x3(lhs.c0 == rhs, lhs.c1 == rhs, lhs.c2 == rhs);
		}

		// Token: 0x06001F73 RID: 8051 RVA: 0x0005A797 File Offset: 0x00058997
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x3 operator ==(uint lhs, uint2x3 rhs)
		{
			return new bool2x3(lhs == rhs.c0, lhs == rhs.c1, lhs == rhs.c2);
		}

		// Token: 0x06001F74 RID: 8052 RVA: 0x0005A7C2 File Offset: 0x000589C2
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x3 operator !=(uint2x3 lhs, uint2x3 rhs)
		{
			return new bool2x3(lhs.c0 != rhs.c0, lhs.c1 != rhs.c1, lhs.c2 != rhs.c2);
		}

		// Token: 0x06001F75 RID: 8053 RVA: 0x0005A7FC File Offset: 0x000589FC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x3 operator !=(uint2x3 lhs, uint rhs)
		{
			return new bool2x3(lhs.c0 != rhs, lhs.c1 != rhs, lhs.c2 != rhs);
		}

		// Token: 0x06001F76 RID: 8054 RVA: 0x0005A827 File Offset: 0x00058A27
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x3 operator !=(uint lhs, uint2x3 rhs)
		{
			return new bool2x3(lhs != rhs.c0, lhs != rhs.c1, lhs != rhs.c2);
		}

		// Token: 0x06001F77 RID: 8055 RVA: 0x0005A852 File Offset: 0x00058A52
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2x3 operator ~(uint2x3 val)
		{
			return new uint2x3(~val.c0, ~val.c1, ~val.c2);
		}

		// Token: 0x06001F78 RID: 8056 RVA: 0x0005A87A File Offset: 0x00058A7A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2x3 operator &(uint2x3 lhs, uint2x3 rhs)
		{
			return new uint2x3(lhs.c0 & rhs.c0, lhs.c1 & rhs.c1, lhs.c2 & rhs.c2);
		}

		// Token: 0x06001F79 RID: 8057 RVA: 0x0005A8B4 File Offset: 0x00058AB4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2x3 operator &(uint2x3 lhs, uint rhs)
		{
			return new uint2x3(lhs.c0 & rhs, lhs.c1 & rhs, lhs.c2 & rhs);
		}

		// Token: 0x06001F7A RID: 8058 RVA: 0x0005A8DF File Offset: 0x00058ADF
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2x3 operator &(uint lhs, uint2x3 rhs)
		{
			return new uint2x3(lhs & rhs.c0, lhs & rhs.c1, lhs & rhs.c2);
		}

		// Token: 0x06001F7B RID: 8059 RVA: 0x0005A90A File Offset: 0x00058B0A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2x3 operator |(uint2x3 lhs, uint2x3 rhs)
		{
			return new uint2x3(lhs.c0 | rhs.c0, lhs.c1 | rhs.c1, lhs.c2 | rhs.c2);
		}

		// Token: 0x06001F7C RID: 8060 RVA: 0x0005A944 File Offset: 0x00058B44
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2x3 operator |(uint2x3 lhs, uint rhs)
		{
			return new uint2x3(lhs.c0 | rhs, lhs.c1 | rhs, lhs.c2 | rhs);
		}

		// Token: 0x06001F7D RID: 8061 RVA: 0x0005A96F File Offset: 0x00058B6F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2x3 operator |(uint lhs, uint2x3 rhs)
		{
			return new uint2x3(lhs | rhs.c0, lhs | rhs.c1, lhs | rhs.c2);
		}

		// Token: 0x06001F7E RID: 8062 RVA: 0x0005A99A File Offset: 0x00058B9A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2x3 operator ^(uint2x3 lhs, uint2x3 rhs)
		{
			return new uint2x3(lhs.c0 ^ rhs.c0, lhs.c1 ^ rhs.c1, lhs.c2 ^ rhs.c2);
		}

		// Token: 0x06001F7F RID: 8063 RVA: 0x0005A9D4 File Offset: 0x00058BD4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2x3 operator ^(uint2x3 lhs, uint rhs)
		{
			return new uint2x3(lhs.c0 ^ rhs, lhs.c1 ^ rhs, lhs.c2 ^ rhs);
		}

		// Token: 0x06001F80 RID: 8064 RVA: 0x0005A9FF File Offset: 0x00058BFF
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2x3 operator ^(uint lhs, uint2x3 rhs)
		{
			return new uint2x3(lhs ^ rhs.c0, lhs ^ rhs.c1, lhs ^ rhs.c2);
		}

		// Token: 0x170009BB RID: 2491
		public unsafe uint2 this[int index]
		{
			get
			{
				fixed (uint2x3* ptr = &this)
				{
					return ref *(uint2*)(ptr + (IntPtr)index * (IntPtr)sizeof(uint2) / (IntPtr)sizeof(uint2x3));
				}
			}
		}

		// Token: 0x06001F82 RID: 8066 RVA: 0x0005AA47 File Offset: 0x00058C47
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool Equals(uint2x3 rhs)
		{
			return this.c0.Equals(rhs.c0) && this.c1.Equals(rhs.c1) && this.c2.Equals(rhs.c2);
		}

		// Token: 0x06001F83 RID: 8067 RVA: 0x0005AA84 File Offset: 0x00058C84
		public override bool Equals(object o)
		{
			if (o is uint2x3)
			{
				uint2x3 rhs = (uint2x3)o;
				return this.Equals(rhs);
			}
			return false;
		}

		// Token: 0x06001F84 RID: 8068 RVA: 0x0005AAA9 File Offset: 0x00058CA9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override int GetHashCode()
		{
			return (int)math.hash(this);
		}

		// Token: 0x06001F85 RID: 8069 RVA: 0x0005AAB8 File Offset: 0x00058CB8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override string ToString()
		{
			return string.Format("uint2x3({0}, {1}, {2},  {3}, {4}, {5})", new object[]
			{
				this.c0.x,
				this.c1.x,
				this.c2.x,
				this.c0.y,
				this.c1.y,
				this.c2.y
			});
		}

		// Token: 0x06001F86 RID: 8070 RVA: 0x0005AB48 File Offset: 0x00058D48
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public string ToString(string format, IFormatProvider formatProvider)
		{
			return string.Format("uint2x3({0}, {1}, {2},  {3}, {4}, {5})", new object[]
			{
				this.c0.x.ToString(format, formatProvider),
				this.c1.x.ToString(format, formatProvider),
				this.c2.x.ToString(format, formatProvider),
				this.c0.y.ToString(format, formatProvider),
				this.c1.y.ToString(format, formatProvider),
				this.c2.y.ToString(format, formatProvider)
			});
		}

		// Token: 0x040000EF RID: 239
		public uint2 c0;

		// Token: 0x040000F0 RID: 240
		public uint2 c1;

		// Token: 0x040000F1 RID: 241
		public uint2 c2;

		// Token: 0x040000F2 RID: 242
		public static readonly uint2x3 zero;
	}
}

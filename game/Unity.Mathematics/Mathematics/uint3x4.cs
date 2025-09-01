using System;
using System.Runtime.CompilerServices;
using Unity.IL2CPP.CompilerServices;

namespace Unity.Mathematics
{
	// Token: 0x02000045 RID: 69
	[Il2CppEagerStaticClassConstruction]
	[Serializable]
	public struct uint3x4 : IEquatable<uint3x4>, IFormattable
	{
		// Token: 0x06002138 RID: 8504 RVA: 0x0005EFBC File Offset: 0x0005D1BC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public uint3x4(uint3 c0, uint3 c1, uint3 c2, uint3 c3)
		{
			this.c0 = c0;
			this.c1 = c1;
			this.c2 = c2;
			this.c3 = c3;
		}

		// Token: 0x06002139 RID: 8505 RVA: 0x0005EFDC File Offset: 0x0005D1DC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public uint3x4(uint m00, uint m01, uint m02, uint m03, uint m10, uint m11, uint m12, uint m13, uint m20, uint m21, uint m22, uint m23)
		{
			this.c0 = new uint3(m00, m10, m20);
			this.c1 = new uint3(m01, m11, m21);
			this.c2 = new uint3(m02, m12, m22);
			this.c3 = new uint3(m03, m13, m23);
		}

		// Token: 0x0600213A RID: 8506 RVA: 0x0005F02A File Offset: 0x0005D22A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public uint3x4(uint v)
		{
			this.c0 = v;
			this.c1 = v;
			this.c2 = v;
			this.c3 = v;
		}

		// Token: 0x0600213B RID: 8507 RVA: 0x0005F05C File Offset: 0x0005D25C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public uint3x4(bool v)
		{
			this.c0 = math.select(new uint3(0U), new uint3(1U), v);
			this.c1 = math.select(new uint3(0U), new uint3(1U), v);
			this.c2 = math.select(new uint3(0U), new uint3(1U), v);
			this.c3 = math.select(new uint3(0U), new uint3(1U), v);
		}

		// Token: 0x0600213C RID: 8508 RVA: 0x0005F0CC File Offset: 0x0005D2CC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public uint3x4(bool3x4 v)
		{
			this.c0 = math.select(new uint3(0U), new uint3(1U), v.c0);
			this.c1 = math.select(new uint3(0U), new uint3(1U), v.c1);
			this.c2 = math.select(new uint3(0U), new uint3(1U), v.c2);
			this.c3 = math.select(new uint3(0U), new uint3(1U), v.c3);
		}

		// Token: 0x0600213D RID: 8509 RVA: 0x0005F14D File Offset: 0x0005D34D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public uint3x4(int v)
		{
			this.c0 = (uint3)v;
			this.c1 = (uint3)v;
			this.c2 = (uint3)v;
			this.c3 = (uint3)v;
		}

		// Token: 0x0600213E RID: 8510 RVA: 0x0005F180 File Offset: 0x0005D380
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public uint3x4(int3x4 v)
		{
			this.c0 = (uint3)v.c0;
			this.c1 = (uint3)v.c1;
			this.c2 = (uint3)v.c2;
			this.c3 = (uint3)v.c3;
		}

		// Token: 0x0600213F RID: 8511 RVA: 0x0005F1D1 File Offset: 0x0005D3D1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public uint3x4(float v)
		{
			this.c0 = (uint3)v;
			this.c1 = (uint3)v;
			this.c2 = (uint3)v;
			this.c3 = (uint3)v;
		}

		// Token: 0x06002140 RID: 8512 RVA: 0x0005F204 File Offset: 0x0005D404
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public uint3x4(float3x4 v)
		{
			this.c0 = (uint3)v.c0;
			this.c1 = (uint3)v.c1;
			this.c2 = (uint3)v.c2;
			this.c3 = (uint3)v.c3;
		}

		// Token: 0x06002141 RID: 8513 RVA: 0x0005F255 File Offset: 0x0005D455
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public uint3x4(double v)
		{
			this.c0 = (uint3)v;
			this.c1 = (uint3)v;
			this.c2 = (uint3)v;
			this.c3 = (uint3)v;
		}

		// Token: 0x06002142 RID: 8514 RVA: 0x0005F288 File Offset: 0x0005D488
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public uint3x4(double3x4 v)
		{
			this.c0 = (uint3)v.c0;
			this.c1 = (uint3)v.c1;
			this.c2 = (uint3)v.c2;
			this.c3 = (uint3)v.c3;
		}

		// Token: 0x06002143 RID: 8515 RVA: 0x0005F2D9 File Offset: 0x0005D4D9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator uint3x4(uint v)
		{
			return new uint3x4(v);
		}

		// Token: 0x06002144 RID: 8516 RVA: 0x0005F2E1 File Offset: 0x0005D4E1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator uint3x4(bool v)
		{
			return new uint3x4(v);
		}

		// Token: 0x06002145 RID: 8517 RVA: 0x0005F2E9 File Offset: 0x0005D4E9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator uint3x4(bool3x4 v)
		{
			return new uint3x4(v);
		}

		// Token: 0x06002146 RID: 8518 RVA: 0x0005F2F1 File Offset: 0x0005D4F1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator uint3x4(int v)
		{
			return new uint3x4(v);
		}

		// Token: 0x06002147 RID: 8519 RVA: 0x0005F2F9 File Offset: 0x0005D4F9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator uint3x4(int3x4 v)
		{
			return new uint3x4(v);
		}

		// Token: 0x06002148 RID: 8520 RVA: 0x0005F301 File Offset: 0x0005D501
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator uint3x4(float v)
		{
			return new uint3x4(v);
		}

		// Token: 0x06002149 RID: 8521 RVA: 0x0005F309 File Offset: 0x0005D509
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator uint3x4(float3x4 v)
		{
			return new uint3x4(v);
		}

		// Token: 0x0600214A RID: 8522 RVA: 0x0005F311 File Offset: 0x0005D511
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator uint3x4(double v)
		{
			return new uint3x4(v);
		}

		// Token: 0x0600214B RID: 8523 RVA: 0x0005F319 File Offset: 0x0005D519
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator uint3x4(double3x4 v)
		{
			return new uint3x4(v);
		}

		// Token: 0x0600214C RID: 8524 RVA: 0x0005F324 File Offset: 0x0005D524
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3x4 operator *(uint3x4 lhs, uint3x4 rhs)
		{
			return new uint3x4(lhs.c0 * rhs.c0, lhs.c1 * rhs.c1, lhs.c2 * rhs.c2, lhs.c3 * rhs.c3);
		}

		// Token: 0x0600214D RID: 8525 RVA: 0x0005F37A File Offset: 0x0005D57A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3x4 operator *(uint3x4 lhs, uint rhs)
		{
			return new uint3x4(lhs.c0 * rhs, lhs.c1 * rhs, lhs.c2 * rhs, lhs.c3 * rhs);
		}

		// Token: 0x0600214E RID: 8526 RVA: 0x0005F3B1 File Offset: 0x0005D5B1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3x4 operator *(uint lhs, uint3x4 rhs)
		{
			return new uint3x4(lhs * rhs.c0, lhs * rhs.c1, lhs * rhs.c2, lhs * rhs.c3);
		}

		// Token: 0x0600214F RID: 8527 RVA: 0x0005F3E8 File Offset: 0x0005D5E8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3x4 operator +(uint3x4 lhs, uint3x4 rhs)
		{
			return new uint3x4(lhs.c0 + rhs.c0, lhs.c1 + rhs.c1, lhs.c2 + rhs.c2, lhs.c3 + rhs.c3);
		}

		// Token: 0x06002150 RID: 8528 RVA: 0x0005F43E File Offset: 0x0005D63E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3x4 operator +(uint3x4 lhs, uint rhs)
		{
			return new uint3x4(lhs.c0 + rhs, lhs.c1 + rhs, lhs.c2 + rhs, lhs.c3 + rhs);
		}

		// Token: 0x06002151 RID: 8529 RVA: 0x0005F475 File Offset: 0x0005D675
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3x4 operator +(uint lhs, uint3x4 rhs)
		{
			return new uint3x4(lhs + rhs.c0, lhs + rhs.c1, lhs + rhs.c2, lhs + rhs.c3);
		}

		// Token: 0x06002152 RID: 8530 RVA: 0x0005F4AC File Offset: 0x0005D6AC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3x4 operator -(uint3x4 lhs, uint3x4 rhs)
		{
			return new uint3x4(lhs.c0 - rhs.c0, lhs.c1 - rhs.c1, lhs.c2 - rhs.c2, lhs.c3 - rhs.c3);
		}

		// Token: 0x06002153 RID: 8531 RVA: 0x0005F502 File Offset: 0x0005D702
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3x4 operator -(uint3x4 lhs, uint rhs)
		{
			return new uint3x4(lhs.c0 - rhs, lhs.c1 - rhs, lhs.c2 - rhs, lhs.c3 - rhs);
		}

		// Token: 0x06002154 RID: 8532 RVA: 0x0005F539 File Offset: 0x0005D739
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3x4 operator -(uint lhs, uint3x4 rhs)
		{
			return new uint3x4(lhs - rhs.c0, lhs - rhs.c1, lhs - rhs.c2, lhs - rhs.c3);
		}

		// Token: 0x06002155 RID: 8533 RVA: 0x0005F570 File Offset: 0x0005D770
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3x4 operator /(uint3x4 lhs, uint3x4 rhs)
		{
			return new uint3x4(lhs.c0 / rhs.c0, lhs.c1 / rhs.c1, lhs.c2 / rhs.c2, lhs.c3 / rhs.c3);
		}

		// Token: 0x06002156 RID: 8534 RVA: 0x0005F5C6 File Offset: 0x0005D7C6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3x4 operator /(uint3x4 lhs, uint rhs)
		{
			return new uint3x4(lhs.c0 / rhs, lhs.c1 / rhs, lhs.c2 / rhs, lhs.c3 / rhs);
		}

		// Token: 0x06002157 RID: 8535 RVA: 0x0005F5FD File Offset: 0x0005D7FD
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3x4 operator /(uint lhs, uint3x4 rhs)
		{
			return new uint3x4(lhs / rhs.c0, lhs / rhs.c1, lhs / rhs.c2, lhs / rhs.c3);
		}

		// Token: 0x06002158 RID: 8536 RVA: 0x0005F634 File Offset: 0x0005D834
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3x4 operator %(uint3x4 lhs, uint3x4 rhs)
		{
			return new uint3x4(lhs.c0 % rhs.c0, lhs.c1 % rhs.c1, lhs.c2 % rhs.c2, lhs.c3 % rhs.c3);
		}

		// Token: 0x06002159 RID: 8537 RVA: 0x0005F68A File Offset: 0x0005D88A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3x4 operator %(uint3x4 lhs, uint rhs)
		{
			return new uint3x4(lhs.c0 % rhs, lhs.c1 % rhs, lhs.c2 % rhs, lhs.c3 % rhs);
		}

		// Token: 0x0600215A RID: 8538 RVA: 0x0005F6C1 File Offset: 0x0005D8C1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3x4 operator %(uint lhs, uint3x4 rhs)
		{
			return new uint3x4(lhs % rhs.c0, lhs % rhs.c1, lhs % rhs.c2, lhs % rhs.c3);
		}

		// Token: 0x0600215B RID: 8539 RVA: 0x0005F6F8 File Offset: 0x0005D8F8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3x4 operator ++(uint3x4 val)
		{
			uint3 @uint = ++val.c0;
			val.c0 = @uint;
			uint3 uint2 = @uint;
			@uint = ++val.c1;
			val.c1 = @uint;
			uint3 uint3 = @uint;
			@uint = ++val.c2;
			val.c2 = @uint;
			uint3 uint4 = @uint;
			@uint = ++val.c3;
			val.c3 = @uint;
			return new uint3x4(uint2, uint3, uint4, @uint);
		}

		// Token: 0x0600215C RID: 8540 RVA: 0x0005F774 File Offset: 0x0005D974
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3x4 operator --(uint3x4 val)
		{
			uint3 @uint = --val.c0;
			val.c0 = @uint;
			uint3 uint2 = @uint;
			@uint = --val.c1;
			val.c1 = @uint;
			uint3 uint3 = @uint;
			@uint = --val.c2;
			val.c2 = @uint;
			uint3 uint4 = @uint;
			@uint = --val.c3;
			val.c3 = @uint;
			return new uint3x4(uint2, uint3, uint4, @uint);
		}

		// Token: 0x0600215D RID: 8541 RVA: 0x0005F7F0 File Offset: 0x0005D9F0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x4 operator <(uint3x4 lhs, uint3x4 rhs)
		{
			return new bool3x4(lhs.c0 < rhs.c0, lhs.c1 < rhs.c1, lhs.c2 < rhs.c2, lhs.c3 < rhs.c3);
		}

		// Token: 0x0600215E RID: 8542 RVA: 0x0005F846 File Offset: 0x0005DA46
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x4 operator <(uint3x4 lhs, uint rhs)
		{
			return new bool3x4(lhs.c0 < rhs, lhs.c1 < rhs, lhs.c2 < rhs, lhs.c3 < rhs);
		}

		// Token: 0x0600215F RID: 8543 RVA: 0x0005F87D File Offset: 0x0005DA7D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x4 operator <(uint lhs, uint3x4 rhs)
		{
			return new bool3x4(lhs < rhs.c0, lhs < rhs.c1, lhs < rhs.c2, lhs < rhs.c3);
		}

		// Token: 0x06002160 RID: 8544 RVA: 0x0005F8B4 File Offset: 0x0005DAB4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x4 operator <=(uint3x4 lhs, uint3x4 rhs)
		{
			return new bool3x4(lhs.c0 <= rhs.c0, lhs.c1 <= rhs.c1, lhs.c2 <= rhs.c2, lhs.c3 <= rhs.c3);
		}

		// Token: 0x06002161 RID: 8545 RVA: 0x0005F90A File Offset: 0x0005DB0A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x4 operator <=(uint3x4 lhs, uint rhs)
		{
			return new bool3x4(lhs.c0 <= rhs, lhs.c1 <= rhs, lhs.c2 <= rhs, lhs.c3 <= rhs);
		}

		// Token: 0x06002162 RID: 8546 RVA: 0x0005F941 File Offset: 0x0005DB41
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x4 operator <=(uint lhs, uint3x4 rhs)
		{
			return new bool3x4(lhs <= rhs.c0, lhs <= rhs.c1, lhs <= rhs.c2, lhs <= rhs.c3);
		}

		// Token: 0x06002163 RID: 8547 RVA: 0x0005F978 File Offset: 0x0005DB78
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x4 operator >(uint3x4 lhs, uint3x4 rhs)
		{
			return new bool3x4(lhs.c0 > rhs.c0, lhs.c1 > rhs.c1, lhs.c2 > rhs.c2, lhs.c3 > rhs.c3);
		}

		// Token: 0x06002164 RID: 8548 RVA: 0x0005F9CE File Offset: 0x0005DBCE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x4 operator >(uint3x4 lhs, uint rhs)
		{
			return new bool3x4(lhs.c0 > rhs, lhs.c1 > rhs, lhs.c2 > rhs, lhs.c3 > rhs);
		}

		// Token: 0x06002165 RID: 8549 RVA: 0x0005FA05 File Offset: 0x0005DC05
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x4 operator >(uint lhs, uint3x4 rhs)
		{
			return new bool3x4(lhs > rhs.c0, lhs > rhs.c1, lhs > rhs.c2, lhs > rhs.c3);
		}

		// Token: 0x06002166 RID: 8550 RVA: 0x0005FA3C File Offset: 0x0005DC3C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x4 operator >=(uint3x4 lhs, uint3x4 rhs)
		{
			return new bool3x4(lhs.c0 >= rhs.c0, lhs.c1 >= rhs.c1, lhs.c2 >= rhs.c2, lhs.c3 >= rhs.c3);
		}

		// Token: 0x06002167 RID: 8551 RVA: 0x0005FA92 File Offset: 0x0005DC92
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x4 operator >=(uint3x4 lhs, uint rhs)
		{
			return new bool3x4(lhs.c0 >= rhs, lhs.c1 >= rhs, lhs.c2 >= rhs, lhs.c3 >= rhs);
		}

		// Token: 0x06002168 RID: 8552 RVA: 0x0005FAC9 File Offset: 0x0005DCC9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x4 operator >=(uint lhs, uint3x4 rhs)
		{
			return new bool3x4(lhs >= rhs.c0, lhs >= rhs.c1, lhs >= rhs.c2, lhs >= rhs.c3);
		}

		// Token: 0x06002169 RID: 8553 RVA: 0x0005FB00 File Offset: 0x0005DD00
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3x4 operator -(uint3x4 val)
		{
			return new uint3x4(-val.c0, -val.c1, -val.c2, -val.c3);
		}

		// Token: 0x0600216A RID: 8554 RVA: 0x0005FB33 File Offset: 0x0005DD33
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3x4 operator +(uint3x4 val)
		{
			return new uint3x4(+val.c0, +val.c1, +val.c2, +val.c3);
		}

		// Token: 0x0600216B RID: 8555 RVA: 0x0005FB66 File Offset: 0x0005DD66
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3x4 operator <<(uint3x4 x, int n)
		{
			return new uint3x4(x.c0 << n, x.c1 << n, x.c2 << n, x.c3 << n);
		}

		// Token: 0x0600216C RID: 8556 RVA: 0x0005FB9D File Offset: 0x0005DD9D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3x4 operator >>(uint3x4 x, int n)
		{
			return new uint3x4(x.c0 >> n, x.c1 >> n, x.c2 >> n, x.c3 >> n);
		}

		// Token: 0x0600216D RID: 8557 RVA: 0x0005FBD4 File Offset: 0x0005DDD4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x4 operator ==(uint3x4 lhs, uint3x4 rhs)
		{
			return new bool3x4(lhs.c0 == rhs.c0, lhs.c1 == rhs.c1, lhs.c2 == rhs.c2, lhs.c3 == rhs.c3);
		}

		// Token: 0x0600216E RID: 8558 RVA: 0x0005FC2A File Offset: 0x0005DE2A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x4 operator ==(uint3x4 lhs, uint rhs)
		{
			return new bool3x4(lhs.c0 == rhs, lhs.c1 == rhs, lhs.c2 == rhs, lhs.c3 == rhs);
		}

		// Token: 0x0600216F RID: 8559 RVA: 0x0005FC61 File Offset: 0x0005DE61
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x4 operator ==(uint lhs, uint3x4 rhs)
		{
			return new bool3x4(lhs == rhs.c0, lhs == rhs.c1, lhs == rhs.c2, lhs == rhs.c3);
		}

		// Token: 0x06002170 RID: 8560 RVA: 0x0005FC98 File Offset: 0x0005DE98
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x4 operator !=(uint3x4 lhs, uint3x4 rhs)
		{
			return new bool3x4(lhs.c0 != rhs.c0, lhs.c1 != rhs.c1, lhs.c2 != rhs.c2, lhs.c3 != rhs.c3);
		}

		// Token: 0x06002171 RID: 8561 RVA: 0x0005FCEE File Offset: 0x0005DEEE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x4 operator !=(uint3x4 lhs, uint rhs)
		{
			return new bool3x4(lhs.c0 != rhs, lhs.c1 != rhs, lhs.c2 != rhs, lhs.c3 != rhs);
		}

		// Token: 0x06002172 RID: 8562 RVA: 0x0005FD25 File Offset: 0x0005DF25
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x4 operator !=(uint lhs, uint3x4 rhs)
		{
			return new bool3x4(lhs != rhs.c0, lhs != rhs.c1, lhs != rhs.c2, lhs != rhs.c3);
		}

		// Token: 0x06002173 RID: 8563 RVA: 0x0005FD5C File Offset: 0x0005DF5C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3x4 operator ~(uint3x4 val)
		{
			return new uint3x4(~val.c0, ~val.c1, ~val.c2, ~val.c3);
		}

		// Token: 0x06002174 RID: 8564 RVA: 0x0005FD90 File Offset: 0x0005DF90
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3x4 operator &(uint3x4 lhs, uint3x4 rhs)
		{
			return new uint3x4(lhs.c0 & rhs.c0, lhs.c1 & rhs.c1, lhs.c2 & rhs.c2, lhs.c3 & rhs.c3);
		}

		// Token: 0x06002175 RID: 8565 RVA: 0x0005FDE6 File Offset: 0x0005DFE6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3x4 operator &(uint3x4 lhs, uint rhs)
		{
			return new uint3x4(lhs.c0 & rhs, lhs.c1 & rhs, lhs.c2 & rhs, lhs.c3 & rhs);
		}

		// Token: 0x06002176 RID: 8566 RVA: 0x0005FE1D File Offset: 0x0005E01D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3x4 operator &(uint lhs, uint3x4 rhs)
		{
			return new uint3x4(lhs & rhs.c0, lhs & rhs.c1, lhs & rhs.c2, lhs & rhs.c3);
		}

		// Token: 0x06002177 RID: 8567 RVA: 0x0005FE54 File Offset: 0x0005E054
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3x4 operator |(uint3x4 lhs, uint3x4 rhs)
		{
			return new uint3x4(lhs.c0 | rhs.c0, lhs.c1 | rhs.c1, lhs.c2 | rhs.c2, lhs.c3 | rhs.c3);
		}

		// Token: 0x06002178 RID: 8568 RVA: 0x0005FEAA File Offset: 0x0005E0AA
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3x4 operator |(uint3x4 lhs, uint rhs)
		{
			return new uint3x4(lhs.c0 | rhs, lhs.c1 | rhs, lhs.c2 | rhs, lhs.c3 | rhs);
		}

		// Token: 0x06002179 RID: 8569 RVA: 0x0005FEE1 File Offset: 0x0005E0E1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3x4 operator |(uint lhs, uint3x4 rhs)
		{
			return new uint3x4(lhs | rhs.c0, lhs | rhs.c1, lhs | rhs.c2, lhs | rhs.c3);
		}

		// Token: 0x0600217A RID: 8570 RVA: 0x0005FF18 File Offset: 0x0005E118
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3x4 operator ^(uint3x4 lhs, uint3x4 rhs)
		{
			return new uint3x4(lhs.c0 ^ rhs.c0, lhs.c1 ^ rhs.c1, lhs.c2 ^ rhs.c2, lhs.c3 ^ rhs.c3);
		}

		// Token: 0x0600217B RID: 8571 RVA: 0x0005FF6E File Offset: 0x0005E16E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3x4 operator ^(uint3x4 lhs, uint rhs)
		{
			return new uint3x4(lhs.c0 ^ rhs, lhs.c1 ^ rhs, lhs.c2 ^ rhs, lhs.c3 ^ rhs);
		}

		// Token: 0x0600217C RID: 8572 RVA: 0x0005FFA5 File Offset: 0x0005E1A5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3x4 operator ^(uint lhs, uint3x4 rhs)
		{
			return new uint3x4(lhs ^ rhs.c0, lhs ^ rhs.c1, lhs ^ rhs.c2, lhs ^ rhs.c3);
		}

		// Token: 0x17000A35 RID: 2613
		public unsafe uint3 this[int index]
		{
			get
			{
				fixed (uint3x4* ptr = &this)
				{
					return ref *(uint3*)(ptr + (IntPtr)index * (IntPtr)sizeof(uint3) / (IntPtr)sizeof(uint3x4));
				}
			}
		}

		// Token: 0x0600217E RID: 8574 RVA: 0x0005FFF8 File Offset: 0x0005E1F8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool Equals(uint3x4 rhs)
		{
			return this.c0.Equals(rhs.c0) && this.c1.Equals(rhs.c1) && this.c2.Equals(rhs.c2) && this.c3.Equals(rhs.c3);
		}

		// Token: 0x0600217F RID: 8575 RVA: 0x00060054 File Offset: 0x0005E254
		public override bool Equals(object o)
		{
			if (o is uint3x4)
			{
				uint3x4 rhs = (uint3x4)o;
				return this.Equals(rhs);
			}
			return false;
		}

		// Token: 0x06002180 RID: 8576 RVA: 0x00060079 File Offset: 0x0005E279
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override int GetHashCode()
		{
			return (int)math.hash(this);
		}

		// Token: 0x06002181 RID: 8577 RVA: 0x00060088 File Offset: 0x0005E288
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override string ToString()
		{
			return string.Format("uint3x4({0}, {1}, {2}, {3},  {4}, {5}, {6}, {7},  {8}, {9}, {10}, {11})", new object[]
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
				this.c3.z
			});
		}

		// Token: 0x06002182 RID: 8578 RVA: 0x00060190 File Offset: 0x0005E390
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public string ToString(string format, IFormatProvider formatProvider)
		{
			return string.Format("uint3x4({0}, {1}, {2}, {3},  {4}, {5}, {6}, {7},  {8}, {9}, {10}, {11})", new object[]
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
				this.c3.z.ToString(format, formatProvider)
			});
		}

		// Token: 0x04000104 RID: 260
		public uint3 c0;

		// Token: 0x04000105 RID: 261
		public uint3 c1;

		// Token: 0x04000106 RID: 262
		public uint3 c2;

		// Token: 0x04000107 RID: 263
		public uint3 c3;

		// Token: 0x04000108 RID: 264
		public static readonly uint3x4 zero;
	}
}

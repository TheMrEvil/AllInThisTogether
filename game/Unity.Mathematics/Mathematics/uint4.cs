using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Unity.IL2CPP.CompilerServices;

namespace Unity.Mathematics
{
	// Token: 0x02000046 RID: 70
	[DebuggerTypeProxy(typeof(uint4.DebuggerProxy))]
	[Il2CppEagerStaticClassConstruction]
	[Serializable]
	public struct uint4 : IEquatable<uint4>, IFormattable
	{
		// Token: 0x06002183 RID: 8579 RVA: 0x000602AD File Offset: 0x0005E4AD
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public uint4(uint x, uint y, uint z, uint w)
		{
			this.x = x;
			this.y = y;
			this.z = z;
			this.w = w;
		}

		// Token: 0x06002184 RID: 8580 RVA: 0x000602CC File Offset: 0x0005E4CC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public uint4(uint x, uint y, uint2 zw)
		{
			this.x = x;
			this.y = y;
			this.z = zw.x;
			this.w = zw.y;
		}

		// Token: 0x06002185 RID: 8581 RVA: 0x000602F4 File Offset: 0x0005E4F4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public uint4(uint x, uint2 yz, uint w)
		{
			this.x = x;
			this.y = yz.x;
			this.z = yz.y;
			this.w = w;
		}

		// Token: 0x06002186 RID: 8582 RVA: 0x0006031C File Offset: 0x0005E51C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public uint4(uint x, uint3 yzw)
		{
			this.x = x;
			this.y = yzw.x;
			this.z = yzw.y;
			this.w = yzw.z;
		}

		// Token: 0x06002187 RID: 8583 RVA: 0x00060349 File Offset: 0x0005E549
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public uint4(uint2 xy, uint z, uint w)
		{
			this.x = xy.x;
			this.y = xy.y;
			this.z = z;
			this.w = w;
		}

		// Token: 0x06002188 RID: 8584 RVA: 0x00060371 File Offset: 0x0005E571
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public uint4(uint2 xy, uint2 zw)
		{
			this.x = xy.x;
			this.y = xy.y;
			this.z = zw.x;
			this.w = zw.y;
		}

		// Token: 0x06002189 RID: 8585 RVA: 0x000603A3 File Offset: 0x0005E5A3
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public uint4(uint3 xyz, uint w)
		{
			this.x = xyz.x;
			this.y = xyz.y;
			this.z = xyz.z;
			this.w = w;
		}

		// Token: 0x0600218A RID: 8586 RVA: 0x000603D0 File Offset: 0x0005E5D0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public uint4(uint4 xyzw)
		{
			this.x = xyzw.x;
			this.y = xyzw.y;
			this.z = xyzw.z;
			this.w = xyzw.w;
		}

		// Token: 0x0600218B RID: 8587 RVA: 0x00060402 File Offset: 0x0005E602
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public uint4(uint v)
		{
			this.x = v;
			this.y = v;
			this.z = v;
			this.w = v;
		}

		// Token: 0x0600218C RID: 8588 RVA: 0x00060420 File Offset: 0x0005E620
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public uint4(bool v)
		{
			this.x = (v ? 1U : 0U);
			this.y = (v ? 1U : 0U);
			this.z = (v ? 1U : 0U);
			this.w = (v ? 1U : 0U);
		}

		// Token: 0x0600218D RID: 8589 RVA: 0x00060458 File Offset: 0x0005E658
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public uint4(bool4 v)
		{
			this.x = (v.x ? 1U : 0U);
			this.y = (v.y ? 1U : 0U);
			this.z = (v.z ? 1U : 0U);
			this.w = (v.w ? 1U : 0U);
		}

		// Token: 0x0600218E RID: 8590 RVA: 0x000604AD File Offset: 0x0005E6AD
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public uint4(int v)
		{
			this.x = (uint)v;
			this.y = (uint)v;
			this.z = (uint)v;
			this.w = (uint)v;
		}

		// Token: 0x0600218F RID: 8591 RVA: 0x000604CB File Offset: 0x0005E6CB
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public uint4(int4 v)
		{
			this.x = (uint)v.x;
			this.y = (uint)v.y;
			this.z = (uint)v.z;
			this.w = (uint)v.w;
		}

		// Token: 0x06002190 RID: 8592 RVA: 0x000604FD File Offset: 0x0005E6FD
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public uint4(float v)
		{
			this.x = (uint)v;
			this.y = (uint)v;
			this.z = (uint)v;
			this.w = (uint)v;
		}

		// Token: 0x06002191 RID: 8593 RVA: 0x0006051F File Offset: 0x0005E71F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public uint4(float4 v)
		{
			this.x = (uint)v.x;
			this.y = (uint)v.y;
			this.z = (uint)v.z;
			this.w = (uint)v.w;
		}

		// Token: 0x06002192 RID: 8594 RVA: 0x00060555 File Offset: 0x0005E755
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public uint4(double v)
		{
			this.x = (uint)v;
			this.y = (uint)v;
			this.z = (uint)v;
			this.w = (uint)v;
		}

		// Token: 0x06002193 RID: 8595 RVA: 0x00060577 File Offset: 0x0005E777
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public uint4(double4 v)
		{
			this.x = (uint)v.x;
			this.y = (uint)v.y;
			this.z = (uint)v.z;
			this.w = (uint)v.w;
		}

		// Token: 0x06002194 RID: 8596 RVA: 0x000605AD File Offset: 0x0005E7AD
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator uint4(uint v)
		{
			return new uint4(v);
		}

		// Token: 0x06002195 RID: 8597 RVA: 0x000605B5 File Offset: 0x0005E7B5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator uint4(bool v)
		{
			return new uint4(v);
		}

		// Token: 0x06002196 RID: 8598 RVA: 0x000605BD File Offset: 0x0005E7BD
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator uint4(bool4 v)
		{
			return new uint4(v);
		}

		// Token: 0x06002197 RID: 8599 RVA: 0x000605C5 File Offset: 0x0005E7C5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator uint4(int v)
		{
			return new uint4(v);
		}

		// Token: 0x06002198 RID: 8600 RVA: 0x000605CD File Offset: 0x0005E7CD
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator uint4(int4 v)
		{
			return new uint4(v);
		}

		// Token: 0x06002199 RID: 8601 RVA: 0x000605D5 File Offset: 0x0005E7D5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator uint4(float v)
		{
			return new uint4(v);
		}

		// Token: 0x0600219A RID: 8602 RVA: 0x000605DD File Offset: 0x0005E7DD
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator uint4(float4 v)
		{
			return new uint4(v);
		}

		// Token: 0x0600219B RID: 8603 RVA: 0x000605E5 File Offset: 0x0005E7E5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator uint4(double v)
		{
			return new uint4(v);
		}

		// Token: 0x0600219C RID: 8604 RVA: 0x000605ED File Offset: 0x0005E7ED
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator uint4(double4 v)
		{
			return new uint4(v);
		}

		// Token: 0x0600219D RID: 8605 RVA: 0x000605F5 File Offset: 0x0005E7F5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4 operator *(uint4 lhs, uint4 rhs)
		{
			return new uint4(lhs.x * rhs.x, lhs.y * rhs.y, lhs.z * rhs.z, lhs.w * rhs.w);
		}

		// Token: 0x0600219E RID: 8606 RVA: 0x00060630 File Offset: 0x0005E830
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4 operator *(uint4 lhs, uint rhs)
		{
			return new uint4(lhs.x * rhs, lhs.y * rhs, lhs.z * rhs, lhs.w * rhs);
		}

		// Token: 0x0600219F RID: 8607 RVA: 0x00060657 File Offset: 0x0005E857
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4 operator *(uint lhs, uint4 rhs)
		{
			return new uint4(lhs * rhs.x, lhs * rhs.y, lhs * rhs.z, lhs * rhs.w);
		}

		// Token: 0x060021A0 RID: 8608 RVA: 0x0006067E File Offset: 0x0005E87E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4 operator +(uint4 lhs, uint4 rhs)
		{
			return new uint4(lhs.x + rhs.x, lhs.y + rhs.y, lhs.z + rhs.z, lhs.w + rhs.w);
		}

		// Token: 0x060021A1 RID: 8609 RVA: 0x000606B9 File Offset: 0x0005E8B9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4 operator +(uint4 lhs, uint rhs)
		{
			return new uint4(lhs.x + rhs, lhs.y + rhs, lhs.z + rhs, lhs.w + rhs);
		}

		// Token: 0x060021A2 RID: 8610 RVA: 0x000606E0 File Offset: 0x0005E8E0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4 operator +(uint lhs, uint4 rhs)
		{
			return new uint4(lhs + rhs.x, lhs + rhs.y, lhs + rhs.z, lhs + rhs.w);
		}

		// Token: 0x060021A3 RID: 8611 RVA: 0x00060707 File Offset: 0x0005E907
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4 operator -(uint4 lhs, uint4 rhs)
		{
			return new uint4(lhs.x - rhs.x, lhs.y - rhs.y, lhs.z - rhs.z, lhs.w - rhs.w);
		}

		// Token: 0x060021A4 RID: 8612 RVA: 0x00060742 File Offset: 0x0005E942
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4 operator -(uint4 lhs, uint rhs)
		{
			return new uint4(lhs.x - rhs, lhs.y - rhs, lhs.z - rhs, lhs.w - rhs);
		}

		// Token: 0x060021A5 RID: 8613 RVA: 0x00060769 File Offset: 0x0005E969
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4 operator -(uint lhs, uint4 rhs)
		{
			return new uint4(lhs - rhs.x, lhs - rhs.y, lhs - rhs.z, lhs - rhs.w);
		}

		// Token: 0x060021A6 RID: 8614 RVA: 0x00060790 File Offset: 0x0005E990
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4 operator /(uint4 lhs, uint4 rhs)
		{
			return new uint4(lhs.x / rhs.x, lhs.y / rhs.y, lhs.z / rhs.z, lhs.w / rhs.w);
		}

		// Token: 0x060021A7 RID: 8615 RVA: 0x000607CB File Offset: 0x0005E9CB
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4 operator /(uint4 lhs, uint rhs)
		{
			return new uint4(lhs.x / rhs, lhs.y / rhs, lhs.z / rhs, lhs.w / rhs);
		}

		// Token: 0x060021A8 RID: 8616 RVA: 0x000607F2 File Offset: 0x0005E9F2
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4 operator /(uint lhs, uint4 rhs)
		{
			return new uint4(lhs / rhs.x, lhs / rhs.y, lhs / rhs.z, lhs / rhs.w);
		}

		// Token: 0x060021A9 RID: 8617 RVA: 0x00060819 File Offset: 0x0005EA19
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4 operator %(uint4 lhs, uint4 rhs)
		{
			return new uint4(lhs.x % rhs.x, lhs.y % rhs.y, lhs.z % rhs.z, lhs.w % rhs.w);
		}

		// Token: 0x060021AA RID: 8618 RVA: 0x00060854 File Offset: 0x0005EA54
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4 operator %(uint4 lhs, uint rhs)
		{
			return new uint4(lhs.x % rhs, lhs.y % rhs, lhs.z % rhs, lhs.w % rhs);
		}

		// Token: 0x060021AB RID: 8619 RVA: 0x0006087B File Offset: 0x0005EA7B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4 operator %(uint lhs, uint4 rhs)
		{
			return new uint4(lhs % rhs.x, lhs % rhs.y, lhs % rhs.z, lhs % rhs.w);
		}

		// Token: 0x060021AC RID: 8620 RVA: 0x000608A4 File Offset: 0x0005EAA4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4 operator ++(uint4 val)
		{
			uint num = val.x + 1U;
			val.x = num;
			uint num2 = num;
			num = val.y + 1U;
			val.y = num;
			uint num3 = num;
			num = val.z + 1U;
			val.z = num;
			uint num4 = num;
			num = val.w + 1U;
			val.w = num;
			return new uint4(num2, num3, num4, num);
		}

		// Token: 0x060021AD RID: 8621 RVA: 0x000608F4 File Offset: 0x0005EAF4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4 operator --(uint4 val)
		{
			uint num = val.x - 1U;
			val.x = num;
			uint num2 = num;
			num = val.y - 1U;
			val.y = num;
			uint num3 = num;
			num = val.z - 1U;
			val.z = num;
			uint num4 = num;
			num = val.w - 1U;
			val.w = num;
			return new uint4(num2, num3, num4, num);
		}

		// Token: 0x060021AE RID: 8622 RVA: 0x00060942 File Offset: 0x0005EB42
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4 operator <(uint4 lhs, uint4 rhs)
		{
			return new bool4(lhs.x < rhs.x, lhs.y < rhs.y, lhs.z < rhs.z, lhs.w < rhs.w);
		}

		// Token: 0x060021AF RID: 8623 RVA: 0x00060981 File Offset: 0x0005EB81
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4 operator <(uint4 lhs, uint rhs)
		{
			return new bool4(lhs.x < rhs, lhs.y < rhs, lhs.z < rhs, lhs.w < rhs);
		}

		// Token: 0x060021B0 RID: 8624 RVA: 0x000609AC File Offset: 0x0005EBAC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4 operator <(uint lhs, uint4 rhs)
		{
			return new bool4(lhs < rhs.x, lhs < rhs.y, lhs < rhs.z, lhs < rhs.w);
		}

		// Token: 0x060021B1 RID: 8625 RVA: 0x000609D8 File Offset: 0x0005EBD8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4 operator <=(uint4 lhs, uint4 rhs)
		{
			return new bool4(lhs.x <= rhs.x, lhs.y <= rhs.y, lhs.z <= rhs.z, lhs.w <= rhs.w);
		}

		// Token: 0x060021B2 RID: 8626 RVA: 0x00060A2E File Offset: 0x0005EC2E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4 operator <=(uint4 lhs, uint rhs)
		{
			return new bool4(lhs.x <= rhs, lhs.y <= rhs, lhs.z <= rhs, lhs.w <= rhs);
		}

		// Token: 0x060021B3 RID: 8627 RVA: 0x00060A65 File Offset: 0x0005EC65
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4 operator <=(uint lhs, uint4 rhs)
		{
			return new bool4(lhs <= rhs.x, lhs <= rhs.y, lhs <= rhs.z, lhs <= rhs.w);
		}

		// Token: 0x060021B4 RID: 8628 RVA: 0x00060A9C File Offset: 0x0005EC9C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4 operator >(uint4 lhs, uint4 rhs)
		{
			return new bool4(lhs.x > rhs.x, lhs.y > rhs.y, lhs.z > rhs.z, lhs.w > rhs.w);
		}

		// Token: 0x060021B5 RID: 8629 RVA: 0x00060ADB File Offset: 0x0005ECDB
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4 operator >(uint4 lhs, uint rhs)
		{
			return new bool4(lhs.x > rhs, lhs.y > rhs, lhs.z > rhs, lhs.w > rhs);
		}

		// Token: 0x060021B6 RID: 8630 RVA: 0x00060B06 File Offset: 0x0005ED06
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4 operator >(uint lhs, uint4 rhs)
		{
			return new bool4(lhs > rhs.x, lhs > rhs.y, lhs > rhs.z, lhs > rhs.w);
		}

		// Token: 0x060021B7 RID: 8631 RVA: 0x00060B34 File Offset: 0x0005ED34
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4 operator >=(uint4 lhs, uint4 rhs)
		{
			return new bool4(lhs.x >= rhs.x, lhs.y >= rhs.y, lhs.z >= rhs.z, lhs.w >= rhs.w);
		}

		// Token: 0x060021B8 RID: 8632 RVA: 0x00060B8A File Offset: 0x0005ED8A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4 operator >=(uint4 lhs, uint rhs)
		{
			return new bool4(lhs.x >= rhs, lhs.y >= rhs, lhs.z >= rhs, lhs.w >= rhs);
		}

		// Token: 0x060021B9 RID: 8633 RVA: 0x00060BC1 File Offset: 0x0005EDC1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4 operator >=(uint lhs, uint4 rhs)
		{
			return new bool4(lhs >= rhs.x, lhs >= rhs.y, lhs >= rhs.z, lhs >= rhs.w);
		}

		// Token: 0x060021BA RID: 8634 RVA: 0x00060BF8 File Offset: 0x0005EDF8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4 operator -(uint4 val)
		{
			return new uint4((uint)(-(uint)((ulong)val.x)), (uint)(-(uint)((ulong)val.y)), (uint)(-(uint)((ulong)val.z)), (uint)(-(uint)((ulong)val.w)));
		}

		// Token: 0x060021BB RID: 8635 RVA: 0x00060C23 File Offset: 0x0005EE23
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4 operator +(uint4 val)
		{
			return new uint4(val.x, val.y, val.z, val.w);
		}

		// Token: 0x060021BC RID: 8636 RVA: 0x00060C42 File Offset: 0x0005EE42
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4 operator <<(uint4 x, int n)
		{
			return new uint4(x.x << n, x.y << n, x.z << n, x.w << n);
		}

		// Token: 0x060021BD RID: 8637 RVA: 0x00060C75 File Offset: 0x0005EE75
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4 operator >>(uint4 x, int n)
		{
			return new uint4(x.x >> n, x.y >> n, x.z >> n, x.w >> n);
		}

		// Token: 0x060021BE RID: 8638 RVA: 0x00060CA8 File Offset: 0x0005EEA8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4 operator ==(uint4 lhs, uint4 rhs)
		{
			return new bool4(lhs.x == rhs.x, lhs.y == rhs.y, lhs.z == rhs.z, lhs.w == rhs.w);
		}

		// Token: 0x060021BF RID: 8639 RVA: 0x00060CE7 File Offset: 0x0005EEE7
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4 operator ==(uint4 lhs, uint rhs)
		{
			return new bool4(lhs.x == rhs, lhs.y == rhs, lhs.z == rhs, lhs.w == rhs);
		}

		// Token: 0x060021C0 RID: 8640 RVA: 0x00060D12 File Offset: 0x0005EF12
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4 operator ==(uint lhs, uint4 rhs)
		{
			return new bool4(lhs == rhs.x, lhs == rhs.y, lhs == rhs.z, lhs == rhs.w);
		}

		// Token: 0x060021C1 RID: 8641 RVA: 0x00060D40 File Offset: 0x0005EF40
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4 operator !=(uint4 lhs, uint4 rhs)
		{
			return new bool4(lhs.x != rhs.x, lhs.y != rhs.y, lhs.z != rhs.z, lhs.w != rhs.w);
		}

		// Token: 0x060021C2 RID: 8642 RVA: 0x00060D96 File Offset: 0x0005EF96
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4 operator !=(uint4 lhs, uint rhs)
		{
			return new bool4(lhs.x != rhs, lhs.y != rhs, lhs.z != rhs, lhs.w != rhs);
		}

		// Token: 0x060021C3 RID: 8643 RVA: 0x00060DCD File Offset: 0x0005EFCD
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4 operator !=(uint lhs, uint4 rhs)
		{
			return new bool4(lhs != rhs.x, lhs != rhs.y, lhs != rhs.z, lhs != rhs.w);
		}

		// Token: 0x060021C4 RID: 8644 RVA: 0x00060E04 File Offset: 0x0005F004
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4 operator ~(uint4 val)
		{
			return new uint4(~val.x, ~val.y, ~val.z, ~val.w);
		}

		// Token: 0x060021C5 RID: 8645 RVA: 0x00060E27 File Offset: 0x0005F027
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4 operator &(uint4 lhs, uint4 rhs)
		{
			return new uint4(lhs.x & rhs.x, lhs.y & rhs.y, lhs.z & rhs.z, lhs.w & rhs.w);
		}

		// Token: 0x060021C6 RID: 8646 RVA: 0x00060E62 File Offset: 0x0005F062
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4 operator &(uint4 lhs, uint rhs)
		{
			return new uint4(lhs.x & rhs, lhs.y & rhs, lhs.z & rhs, lhs.w & rhs);
		}

		// Token: 0x060021C7 RID: 8647 RVA: 0x00060E89 File Offset: 0x0005F089
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4 operator &(uint lhs, uint4 rhs)
		{
			return new uint4(lhs & rhs.x, lhs & rhs.y, lhs & rhs.z, lhs & rhs.w);
		}

		// Token: 0x060021C8 RID: 8648 RVA: 0x00060EB0 File Offset: 0x0005F0B0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4 operator |(uint4 lhs, uint4 rhs)
		{
			return new uint4(lhs.x | rhs.x, lhs.y | rhs.y, lhs.z | rhs.z, lhs.w | rhs.w);
		}

		// Token: 0x060021C9 RID: 8649 RVA: 0x00060EEB File Offset: 0x0005F0EB
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4 operator |(uint4 lhs, uint rhs)
		{
			return new uint4(lhs.x | rhs, lhs.y | rhs, lhs.z | rhs, lhs.w | rhs);
		}

		// Token: 0x060021CA RID: 8650 RVA: 0x00060F12 File Offset: 0x0005F112
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4 operator |(uint lhs, uint4 rhs)
		{
			return new uint4(lhs | rhs.x, lhs | rhs.y, lhs | rhs.z, lhs | rhs.w);
		}

		// Token: 0x060021CB RID: 8651 RVA: 0x00060F39 File Offset: 0x0005F139
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4 operator ^(uint4 lhs, uint4 rhs)
		{
			return new uint4(lhs.x ^ rhs.x, lhs.y ^ rhs.y, lhs.z ^ rhs.z, lhs.w ^ rhs.w);
		}

		// Token: 0x060021CC RID: 8652 RVA: 0x00060F74 File Offset: 0x0005F174
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4 operator ^(uint4 lhs, uint rhs)
		{
			return new uint4(lhs.x ^ rhs, lhs.y ^ rhs, lhs.z ^ rhs, lhs.w ^ rhs);
		}

		// Token: 0x060021CD RID: 8653 RVA: 0x00060F9B File Offset: 0x0005F19B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4 operator ^(uint lhs, uint4 rhs)
		{
			return new uint4(lhs ^ rhs.x, lhs ^ rhs.y, lhs ^ rhs.z, lhs ^ rhs.w);
		}

		// Token: 0x17000A36 RID: 2614
		// (get) Token: 0x060021CE RID: 8654 RVA: 0x00060FC2 File Offset: 0x0005F1C2
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 xxxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.x, this.x, this.x, this.x);
			}
		}

		// Token: 0x17000A37 RID: 2615
		// (get) Token: 0x060021CF RID: 8655 RVA: 0x00060FE1 File Offset: 0x0005F1E1
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 xxxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.x, this.x, this.x, this.y);
			}
		}

		// Token: 0x17000A38 RID: 2616
		// (get) Token: 0x060021D0 RID: 8656 RVA: 0x00061000 File Offset: 0x0005F200
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 xxxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.x, this.x, this.x, this.z);
			}
		}

		// Token: 0x17000A39 RID: 2617
		// (get) Token: 0x060021D1 RID: 8657 RVA: 0x0006101F File Offset: 0x0005F21F
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 xxxw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.x, this.x, this.x, this.w);
			}
		}

		// Token: 0x17000A3A RID: 2618
		// (get) Token: 0x060021D2 RID: 8658 RVA: 0x0006103E File Offset: 0x0005F23E
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 xxyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.x, this.x, this.y, this.x);
			}
		}

		// Token: 0x17000A3B RID: 2619
		// (get) Token: 0x060021D3 RID: 8659 RVA: 0x0006105D File Offset: 0x0005F25D
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 xxyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.x, this.x, this.y, this.y);
			}
		}

		// Token: 0x17000A3C RID: 2620
		// (get) Token: 0x060021D4 RID: 8660 RVA: 0x0006107C File Offset: 0x0005F27C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 xxyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.x, this.x, this.y, this.z);
			}
		}

		// Token: 0x17000A3D RID: 2621
		// (get) Token: 0x060021D5 RID: 8661 RVA: 0x0006109B File Offset: 0x0005F29B
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 xxyw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.x, this.x, this.y, this.w);
			}
		}

		// Token: 0x17000A3E RID: 2622
		// (get) Token: 0x060021D6 RID: 8662 RVA: 0x000610BA File Offset: 0x0005F2BA
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 xxzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.x, this.x, this.z, this.x);
			}
		}

		// Token: 0x17000A3F RID: 2623
		// (get) Token: 0x060021D7 RID: 8663 RVA: 0x000610D9 File Offset: 0x0005F2D9
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 xxzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.x, this.x, this.z, this.y);
			}
		}

		// Token: 0x17000A40 RID: 2624
		// (get) Token: 0x060021D8 RID: 8664 RVA: 0x000610F8 File Offset: 0x0005F2F8
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 xxzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.x, this.x, this.z, this.z);
			}
		}

		// Token: 0x17000A41 RID: 2625
		// (get) Token: 0x060021D9 RID: 8665 RVA: 0x00061117 File Offset: 0x0005F317
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 xxzw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.x, this.x, this.z, this.w);
			}
		}

		// Token: 0x17000A42 RID: 2626
		// (get) Token: 0x060021DA RID: 8666 RVA: 0x00061136 File Offset: 0x0005F336
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 xxwx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.x, this.x, this.w, this.x);
			}
		}

		// Token: 0x17000A43 RID: 2627
		// (get) Token: 0x060021DB RID: 8667 RVA: 0x00061155 File Offset: 0x0005F355
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 xxwy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.x, this.x, this.w, this.y);
			}
		}

		// Token: 0x17000A44 RID: 2628
		// (get) Token: 0x060021DC RID: 8668 RVA: 0x00061174 File Offset: 0x0005F374
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 xxwz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.x, this.x, this.w, this.z);
			}
		}

		// Token: 0x17000A45 RID: 2629
		// (get) Token: 0x060021DD RID: 8669 RVA: 0x00061193 File Offset: 0x0005F393
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 xxww
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.x, this.x, this.w, this.w);
			}
		}

		// Token: 0x17000A46 RID: 2630
		// (get) Token: 0x060021DE RID: 8670 RVA: 0x000611B2 File Offset: 0x0005F3B2
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 xyxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.x, this.y, this.x, this.x);
			}
		}

		// Token: 0x17000A47 RID: 2631
		// (get) Token: 0x060021DF RID: 8671 RVA: 0x000611D1 File Offset: 0x0005F3D1
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 xyxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.x, this.y, this.x, this.y);
			}
		}

		// Token: 0x17000A48 RID: 2632
		// (get) Token: 0x060021E0 RID: 8672 RVA: 0x000611F0 File Offset: 0x0005F3F0
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 xyxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.x, this.y, this.x, this.z);
			}
		}

		// Token: 0x17000A49 RID: 2633
		// (get) Token: 0x060021E1 RID: 8673 RVA: 0x0006120F File Offset: 0x0005F40F
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 xyxw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.x, this.y, this.x, this.w);
			}
		}

		// Token: 0x17000A4A RID: 2634
		// (get) Token: 0x060021E2 RID: 8674 RVA: 0x0006122E File Offset: 0x0005F42E
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 xyyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.x, this.y, this.y, this.x);
			}
		}

		// Token: 0x17000A4B RID: 2635
		// (get) Token: 0x060021E3 RID: 8675 RVA: 0x0006124D File Offset: 0x0005F44D
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 xyyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.x, this.y, this.y, this.y);
			}
		}

		// Token: 0x17000A4C RID: 2636
		// (get) Token: 0x060021E4 RID: 8676 RVA: 0x0006126C File Offset: 0x0005F46C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 xyyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.x, this.y, this.y, this.z);
			}
		}

		// Token: 0x17000A4D RID: 2637
		// (get) Token: 0x060021E5 RID: 8677 RVA: 0x0006128B File Offset: 0x0005F48B
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 xyyw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.x, this.y, this.y, this.w);
			}
		}

		// Token: 0x17000A4E RID: 2638
		// (get) Token: 0x060021E6 RID: 8678 RVA: 0x000612AA File Offset: 0x0005F4AA
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 xyzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.x, this.y, this.z, this.x);
			}
		}

		// Token: 0x17000A4F RID: 2639
		// (get) Token: 0x060021E7 RID: 8679 RVA: 0x000612C9 File Offset: 0x0005F4C9
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 xyzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.x, this.y, this.z, this.y);
			}
		}

		// Token: 0x17000A50 RID: 2640
		// (get) Token: 0x060021E8 RID: 8680 RVA: 0x000612E8 File Offset: 0x0005F4E8
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 xyzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.x, this.y, this.z, this.z);
			}
		}

		// Token: 0x17000A51 RID: 2641
		// (get) Token: 0x060021E9 RID: 8681 RVA: 0x00061307 File Offset: 0x0005F507
		// (set) Token: 0x060021EA RID: 8682 RVA: 0x00061326 File Offset: 0x0005F526
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 xyzw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.x, this.y, this.z, this.w);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.x = value.x;
				this.y = value.y;
				this.z = value.z;
				this.w = value.w;
			}
		}

		// Token: 0x17000A52 RID: 2642
		// (get) Token: 0x060021EB RID: 8683 RVA: 0x00061358 File Offset: 0x0005F558
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 xywx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.x, this.y, this.w, this.x);
			}
		}

		// Token: 0x17000A53 RID: 2643
		// (get) Token: 0x060021EC RID: 8684 RVA: 0x00061377 File Offset: 0x0005F577
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 xywy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.x, this.y, this.w, this.y);
			}
		}

		// Token: 0x17000A54 RID: 2644
		// (get) Token: 0x060021ED RID: 8685 RVA: 0x00061396 File Offset: 0x0005F596
		// (set) Token: 0x060021EE RID: 8686 RVA: 0x000613B5 File Offset: 0x0005F5B5
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 xywz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.x, this.y, this.w, this.z);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.x = value.x;
				this.y = value.y;
				this.w = value.z;
				this.z = value.w;
			}
		}

		// Token: 0x17000A55 RID: 2645
		// (get) Token: 0x060021EF RID: 8687 RVA: 0x000613E7 File Offset: 0x0005F5E7
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 xyww
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.x, this.y, this.w, this.w);
			}
		}

		// Token: 0x17000A56 RID: 2646
		// (get) Token: 0x060021F0 RID: 8688 RVA: 0x00061406 File Offset: 0x0005F606
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 xzxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.x, this.z, this.x, this.x);
			}
		}

		// Token: 0x17000A57 RID: 2647
		// (get) Token: 0x060021F1 RID: 8689 RVA: 0x00061425 File Offset: 0x0005F625
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 xzxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.x, this.z, this.x, this.y);
			}
		}

		// Token: 0x17000A58 RID: 2648
		// (get) Token: 0x060021F2 RID: 8690 RVA: 0x00061444 File Offset: 0x0005F644
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 xzxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.x, this.z, this.x, this.z);
			}
		}

		// Token: 0x17000A59 RID: 2649
		// (get) Token: 0x060021F3 RID: 8691 RVA: 0x00061463 File Offset: 0x0005F663
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 xzxw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.x, this.z, this.x, this.w);
			}
		}

		// Token: 0x17000A5A RID: 2650
		// (get) Token: 0x060021F4 RID: 8692 RVA: 0x00061482 File Offset: 0x0005F682
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 xzyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.x, this.z, this.y, this.x);
			}
		}

		// Token: 0x17000A5B RID: 2651
		// (get) Token: 0x060021F5 RID: 8693 RVA: 0x000614A1 File Offset: 0x0005F6A1
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 xzyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.x, this.z, this.y, this.y);
			}
		}

		// Token: 0x17000A5C RID: 2652
		// (get) Token: 0x060021F6 RID: 8694 RVA: 0x000614C0 File Offset: 0x0005F6C0
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 xzyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.x, this.z, this.y, this.z);
			}
		}

		// Token: 0x17000A5D RID: 2653
		// (get) Token: 0x060021F7 RID: 8695 RVA: 0x000614DF File Offset: 0x0005F6DF
		// (set) Token: 0x060021F8 RID: 8696 RVA: 0x000614FE File Offset: 0x0005F6FE
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 xzyw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.x, this.z, this.y, this.w);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.x = value.x;
				this.z = value.y;
				this.y = value.z;
				this.w = value.w;
			}
		}

		// Token: 0x17000A5E RID: 2654
		// (get) Token: 0x060021F9 RID: 8697 RVA: 0x00061530 File Offset: 0x0005F730
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 xzzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.x, this.z, this.z, this.x);
			}
		}

		// Token: 0x17000A5F RID: 2655
		// (get) Token: 0x060021FA RID: 8698 RVA: 0x0006154F File Offset: 0x0005F74F
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 xzzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.x, this.z, this.z, this.y);
			}
		}

		// Token: 0x17000A60 RID: 2656
		// (get) Token: 0x060021FB RID: 8699 RVA: 0x0006156E File Offset: 0x0005F76E
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 xzzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.x, this.z, this.z, this.z);
			}
		}

		// Token: 0x17000A61 RID: 2657
		// (get) Token: 0x060021FC RID: 8700 RVA: 0x0006158D File Offset: 0x0005F78D
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 xzzw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.x, this.z, this.z, this.w);
			}
		}

		// Token: 0x17000A62 RID: 2658
		// (get) Token: 0x060021FD RID: 8701 RVA: 0x000615AC File Offset: 0x0005F7AC
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 xzwx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.x, this.z, this.w, this.x);
			}
		}

		// Token: 0x17000A63 RID: 2659
		// (get) Token: 0x060021FE RID: 8702 RVA: 0x000615CB File Offset: 0x0005F7CB
		// (set) Token: 0x060021FF RID: 8703 RVA: 0x000615EA File Offset: 0x0005F7EA
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 xzwy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.x, this.z, this.w, this.y);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.x = value.x;
				this.z = value.y;
				this.w = value.z;
				this.y = value.w;
			}
		}

		// Token: 0x17000A64 RID: 2660
		// (get) Token: 0x06002200 RID: 8704 RVA: 0x0006161C File Offset: 0x0005F81C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 xzwz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.x, this.z, this.w, this.z);
			}
		}

		// Token: 0x17000A65 RID: 2661
		// (get) Token: 0x06002201 RID: 8705 RVA: 0x0006163B File Offset: 0x0005F83B
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 xzww
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.x, this.z, this.w, this.w);
			}
		}

		// Token: 0x17000A66 RID: 2662
		// (get) Token: 0x06002202 RID: 8706 RVA: 0x0006165A File Offset: 0x0005F85A
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 xwxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.x, this.w, this.x, this.x);
			}
		}

		// Token: 0x17000A67 RID: 2663
		// (get) Token: 0x06002203 RID: 8707 RVA: 0x00061679 File Offset: 0x0005F879
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 xwxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.x, this.w, this.x, this.y);
			}
		}

		// Token: 0x17000A68 RID: 2664
		// (get) Token: 0x06002204 RID: 8708 RVA: 0x00061698 File Offset: 0x0005F898
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 xwxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.x, this.w, this.x, this.z);
			}
		}

		// Token: 0x17000A69 RID: 2665
		// (get) Token: 0x06002205 RID: 8709 RVA: 0x000616B7 File Offset: 0x0005F8B7
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 xwxw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.x, this.w, this.x, this.w);
			}
		}

		// Token: 0x17000A6A RID: 2666
		// (get) Token: 0x06002206 RID: 8710 RVA: 0x000616D6 File Offset: 0x0005F8D6
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 xwyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.x, this.w, this.y, this.x);
			}
		}

		// Token: 0x17000A6B RID: 2667
		// (get) Token: 0x06002207 RID: 8711 RVA: 0x000616F5 File Offset: 0x0005F8F5
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 xwyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.x, this.w, this.y, this.y);
			}
		}

		// Token: 0x17000A6C RID: 2668
		// (get) Token: 0x06002208 RID: 8712 RVA: 0x00061714 File Offset: 0x0005F914
		// (set) Token: 0x06002209 RID: 8713 RVA: 0x00061733 File Offset: 0x0005F933
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 xwyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.x, this.w, this.y, this.z);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.x = value.x;
				this.w = value.y;
				this.y = value.z;
				this.z = value.w;
			}
		}

		// Token: 0x17000A6D RID: 2669
		// (get) Token: 0x0600220A RID: 8714 RVA: 0x00061765 File Offset: 0x0005F965
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 xwyw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.x, this.w, this.y, this.w);
			}
		}

		// Token: 0x17000A6E RID: 2670
		// (get) Token: 0x0600220B RID: 8715 RVA: 0x00061784 File Offset: 0x0005F984
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 xwzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.x, this.w, this.z, this.x);
			}
		}

		// Token: 0x17000A6F RID: 2671
		// (get) Token: 0x0600220C RID: 8716 RVA: 0x000617A3 File Offset: 0x0005F9A3
		// (set) Token: 0x0600220D RID: 8717 RVA: 0x000617C2 File Offset: 0x0005F9C2
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 xwzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.x, this.w, this.z, this.y);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.x = value.x;
				this.w = value.y;
				this.z = value.z;
				this.y = value.w;
			}
		}

		// Token: 0x17000A70 RID: 2672
		// (get) Token: 0x0600220E RID: 8718 RVA: 0x000617F4 File Offset: 0x0005F9F4
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 xwzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.x, this.w, this.z, this.z);
			}
		}

		// Token: 0x17000A71 RID: 2673
		// (get) Token: 0x0600220F RID: 8719 RVA: 0x00061813 File Offset: 0x0005FA13
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 xwzw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.x, this.w, this.z, this.w);
			}
		}

		// Token: 0x17000A72 RID: 2674
		// (get) Token: 0x06002210 RID: 8720 RVA: 0x00061832 File Offset: 0x0005FA32
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 xwwx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.x, this.w, this.w, this.x);
			}
		}

		// Token: 0x17000A73 RID: 2675
		// (get) Token: 0x06002211 RID: 8721 RVA: 0x00061851 File Offset: 0x0005FA51
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 xwwy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.x, this.w, this.w, this.y);
			}
		}

		// Token: 0x17000A74 RID: 2676
		// (get) Token: 0x06002212 RID: 8722 RVA: 0x00061870 File Offset: 0x0005FA70
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 xwwz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.x, this.w, this.w, this.z);
			}
		}

		// Token: 0x17000A75 RID: 2677
		// (get) Token: 0x06002213 RID: 8723 RVA: 0x0006188F File Offset: 0x0005FA8F
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 xwww
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.x, this.w, this.w, this.w);
			}
		}

		// Token: 0x17000A76 RID: 2678
		// (get) Token: 0x06002214 RID: 8724 RVA: 0x000618AE File Offset: 0x0005FAAE
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 yxxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.y, this.x, this.x, this.x);
			}
		}

		// Token: 0x17000A77 RID: 2679
		// (get) Token: 0x06002215 RID: 8725 RVA: 0x000618CD File Offset: 0x0005FACD
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 yxxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.y, this.x, this.x, this.y);
			}
		}

		// Token: 0x17000A78 RID: 2680
		// (get) Token: 0x06002216 RID: 8726 RVA: 0x000618EC File Offset: 0x0005FAEC
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 yxxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.y, this.x, this.x, this.z);
			}
		}

		// Token: 0x17000A79 RID: 2681
		// (get) Token: 0x06002217 RID: 8727 RVA: 0x0006190B File Offset: 0x0005FB0B
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 yxxw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.y, this.x, this.x, this.w);
			}
		}

		// Token: 0x17000A7A RID: 2682
		// (get) Token: 0x06002218 RID: 8728 RVA: 0x0006192A File Offset: 0x0005FB2A
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 yxyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.y, this.x, this.y, this.x);
			}
		}

		// Token: 0x17000A7B RID: 2683
		// (get) Token: 0x06002219 RID: 8729 RVA: 0x00061949 File Offset: 0x0005FB49
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 yxyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.y, this.x, this.y, this.y);
			}
		}

		// Token: 0x17000A7C RID: 2684
		// (get) Token: 0x0600221A RID: 8730 RVA: 0x00061968 File Offset: 0x0005FB68
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 yxyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.y, this.x, this.y, this.z);
			}
		}

		// Token: 0x17000A7D RID: 2685
		// (get) Token: 0x0600221B RID: 8731 RVA: 0x00061987 File Offset: 0x0005FB87
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 yxyw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.y, this.x, this.y, this.w);
			}
		}

		// Token: 0x17000A7E RID: 2686
		// (get) Token: 0x0600221C RID: 8732 RVA: 0x000619A6 File Offset: 0x0005FBA6
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 yxzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.y, this.x, this.z, this.x);
			}
		}

		// Token: 0x17000A7F RID: 2687
		// (get) Token: 0x0600221D RID: 8733 RVA: 0x000619C5 File Offset: 0x0005FBC5
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 yxzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.y, this.x, this.z, this.y);
			}
		}

		// Token: 0x17000A80 RID: 2688
		// (get) Token: 0x0600221E RID: 8734 RVA: 0x000619E4 File Offset: 0x0005FBE4
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 yxzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.y, this.x, this.z, this.z);
			}
		}

		// Token: 0x17000A81 RID: 2689
		// (get) Token: 0x0600221F RID: 8735 RVA: 0x00061A03 File Offset: 0x0005FC03
		// (set) Token: 0x06002220 RID: 8736 RVA: 0x00061A22 File Offset: 0x0005FC22
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 yxzw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.y, this.x, this.z, this.w);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.y = value.x;
				this.x = value.y;
				this.z = value.z;
				this.w = value.w;
			}
		}

		// Token: 0x17000A82 RID: 2690
		// (get) Token: 0x06002221 RID: 8737 RVA: 0x00061A54 File Offset: 0x0005FC54
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 yxwx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.y, this.x, this.w, this.x);
			}
		}

		// Token: 0x17000A83 RID: 2691
		// (get) Token: 0x06002222 RID: 8738 RVA: 0x00061A73 File Offset: 0x0005FC73
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 yxwy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.y, this.x, this.w, this.y);
			}
		}

		// Token: 0x17000A84 RID: 2692
		// (get) Token: 0x06002223 RID: 8739 RVA: 0x00061A92 File Offset: 0x0005FC92
		// (set) Token: 0x06002224 RID: 8740 RVA: 0x00061AB1 File Offset: 0x0005FCB1
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 yxwz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.y, this.x, this.w, this.z);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.y = value.x;
				this.x = value.y;
				this.w = value.z;
				this.z = value.w;
			}
		}

		// Token: 0x17000A85 RID: 2693
		// (get) Token: 0x06002225 RID: 8741 RVA: 0x00061AE3 File Offset: 0x0005FCE3
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 yxww
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.y, this.x, this.w, this.w);
			}
		}

		// Token: 0x17000A86 RID: 2694
		// (get) Token: 0x06002226 RID: 8742 RVA: 0x00061B02 File Offset: 0x0005FD02
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 yyxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.y, this.y, this.x, this.x);
			}
		}

		// Token: 0x17000A87 RID: 2695
		// (get) Token: 0x06002227 RID: 8743 RVA: 0x00061B21 File Offset: 0x0005FD21
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 yyxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.y, this.y, this.x, this.y);
			}
		}

		// Token: 0x17000A88 RID: 2696
		// (get) Token: 0x06002228 RID: 8744 RVA: 0x00061B40 File Offset: 0x0005FD40
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 yyxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.y, this.y, this.x, this.z);
			}
		}

		// Token: 0x17000A89 RID: 2697
		// (get) Token: 0x06002229 RID: 8745 RVA: 0x00061B5F File Offset: 0x0005FD5F
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 yyxw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.y, this.y, this.x, this.w);
			}
		}

		// Token: 0x17000A8A RID: 2698
		// (get) Token: 0x0600222A RID: 8746 RVA: 0x00061B7E File Offset: 0x0005FD7E
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 yyyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.y, this.y, this.y, this.x);
			}
		}

		// Token: 0x17000A8B RID: 2699
		// (get) Token: 0x0600222B RID: 8747 RVA: 0x00061B9D File Offset: 0x0005FD9D
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 yyyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.y, this.y, this.y, this.y);
			}
		}

		// Token: 0x17000A8C RID: 2700
		// (get) Token: 0x0600222C RID: 8748 RVA: 0x00061BBC File Offset: 0x0005FDBC
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 yyyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.y, this.y, this.y, this.z);
			}
		}

		// Token: 0x17000A8D RID: 2701
		// (get) Token: 0x0600222D RID: 8749 RVA: 0x00061BDB File Offset: 0x0005FDDB
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 yyyw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.y, this.y, this.y, this.w);
			}
		}

		// Token: 0x17000A8E RID: 2702
		// (get) Token: 0x0600222E RID: 8750 RVA: 0x00061BFA File Offset: 0x0005FDFA
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 yyzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.y, this.y, this.z, this.x);
			}
		}

		// Token: 0x17000A8F RID: 2703
		// (get) Token: 0x0600222F RID: 8751 RVA: 0x00061C19 File Offset: 0x0005FE19
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 yyzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.y, this.y, this.z, this.y);
			}
		}

		// Token: 0x17000A90 RID: 2704
		// (get) Token: 0x06002230 RID: 8752 RVA: 0x00061C38 File Offset: 0x0005FE38
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 yyzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.y, this.y, this.z, this.z);
			}
		}

		// Token: 0x17000A91 RID: 2705
		// (get) Token: 0x06002231 RID: 8753 RVA: 0x00061C57 File Offset: 0x0005FE57
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 yyzw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.y, this.y, this.z, this.w);
			}
		}

		// Token: 0x17000A92 RID: 2706
		// (get) Token: 0x06002232 RID: 8754 RVA: 0x00061C76 File Offset: 0x0005FE76
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 yywx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.y, this.y, this.w, this.x);
			}
		}

		// Token: 0x17000A93 RID: 2707
		// (get) Token: 0x06002233 RID: 8755 RVA: 0x00061C95 File Offset: 0x0005FE95
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 yywy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.y, this.y, this.w, this.y);
			}
		}

		// Token: 0x17000A94 RID: 2708
		// (get) Token: 0x06002234 RID: 8756 RVA: 0x00061CB4 File Offset: 0x0005FEB4
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 yywz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.y, this.y, this.w, this.z);
			}
		}

		// Token: 0x17000A95 RID: 2709
		// (get) Token: 0x06002235 RID: 8757 RVA: 0x00061CD3 File Offset: 0x0005FED3
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 yyww
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.y, this.y, this.w, this.w);
			}
		}

		// Token: 0x17000A96 RID: 2710
		// (get) Token: 0x06002236 RID: 8758 RVA: 0x00061CF2 File Offset: 0x0005FEF2
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 yzxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.y, this.z, this.x, this.x);
			}
		}

		// Token: 0x17000A97 RID: 2711
		// (get) Token: 0x06002237 RID: 8759 RVA: 0x00061D11 File Offset: 0x0005FF11
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 yzxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.y, this.z, this.x, this.y);
			}
		}

		// Token: 0x17000A98 RID: 2712
		// (get) Token: 0x06002238 RID: 8760 RVA: 0x00061D30 File Offset: 0x0005FF30
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 yzxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.y, this.z, this.x, this.z);
			}
		}

		// Token: 0x17000A99 RID: 2713
		// (get) Token: 0x06002239 RID: 8761 RVA: 0x00061D4F File Offset: 0x0005FF4F
		// (set) Token: 0x0600223A RID: 8762 RVA: 0x00061D6E File Offset: 0x0005FF6E
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 yzxw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.y, this.z, this.x, this.w);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.y = value.x;
				this.z = value.y;
				this.x = value.z;
				this.w = value.w;
			}
		}

		// Token: 0x17000A9A RID: 2714
		// (get) Token: 0x0600223B RID: 8763 RVA: 0x00061DA0 File Offset: 0x0005FFA0
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 yzyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.y, this.z, this.y, this.x);
			}
		}

		// Token: 0x17000A9B RID: 2715
		// (get) Token: 0x0600223C RID: 8764 RVA: 0x00061DBF File Offset: 0x0005FFBF
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 yzyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.y, this.z, this.y, this.y);
			}
		}

		// Token: 0x17000A9C RID: 2716
		// (get) Token: 0x0600223D RID: 8765 RVA: 0x00061DDE File Offset: 0x0005FFDE
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 yzyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.y, this.z, this.y, this.z);
			}
		}

		// Token: 0x17000A9D RID: 2717
		// (get) Token: 0x0600223E RID: 8766 RVA: 0x00061DFD File Offset: 0x0005FFFD
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 yzyw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.y, this.z, this.y, this.w);
			}
		}

		// Token: 0x17000A9E RID: 2718
		// (get) Token: 0x0600223F RID: 8767 RVA: 0x00061E1C File Offset: 0x0006001C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 yzzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.y, this.z, this.z, this.x);
			}
		}

		// Token: 0x17000A9F RID: 2719
		// (get) Token: 0x06002240 RID: 8768 RVA: 0x00061E3B File Offset: 0x0006003B
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 yzzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.y, this.z, this.z, this.y);
			}
		}

		// Token: 0x17000AA0 RID: 2720
		// (get) Token: 0x06002241 RID: 8769 RVA: 0x00061E5A File Offset: 0x0006005A
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 yzzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.y, this.z, this.z, this.z);
			}
		}

		// Token: 0x17000AA1 RID: 2721
		// (get) Token: 0x06002242 RID: 8770 RVA: 0x00061E79 File Offset: 0x00060079
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 yzzw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.y, this.z, this.z, this.w);
			}
		}

		// Token: 0x17000AA2 RID: 2722
		// (get) Token: 0x06002243 RID: 8771 RVA: 0x00061E98 File Offset: 0x00060098
		// (set) Token: 0x06002244 RID: 8772 RVA: 0x00061EB7 File Offset: 0x000600B7
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 yzwx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.y, this.z, this.w, this.x);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.y = value.x;
				this.z = value.y;
				this.w = value.z;
				this.x = value.w;
			}
		}

		// Token: 0x17000AA3 RID: 2723
		// (get) Token: 0x06002245 RID: 8773 RVA: 0x00061EE9 File Offset: 0x000600E9
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 yzwy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.y, this.z, this.w, this.y);
			}
		}

		// Token: 0x17000AA4 RID: 2724
		// (get) Token: 0x06002246 RID: 8774 RVA: 0x00061F08 File Offset: 0x00060108
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 yzwz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.y, this.z, this.w, this.z);
			}
		}

		// Token: 0x17000AA5 RID: 2725
		// (get) Token: 0x06002247 RID: 8775 RVA: 0x00061F27 File Offset: 0x00060127
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 yzww
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.y, this.z, this.w, this.w);
			}
		}

		// Token: 0x17000AA6 RID: 2726
		// (get) Token: 0x06002248 RID: 8776 RVA: 0x00061F46 File Offset: 0x00060146
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 ywxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.y, this.w, this.x, this.x);
			}
		}

		// Token: 0x17000AA7 RID: 2727
		// (get) Token: 0x06002249 RID: 8777 RVA: 0x00061F65 File Offset: 0x00060165
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 ywxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.y, this.w, this.x, this.y);
			}
		}

		// Token: 0x17000AA8 RID: 2728
		// (get) Token: 0x0600224A RID: 8778 RVA: 0x00061F84 File Offset: 0x00060184
		// (set) Token: 0x0600224B RID: 8779 RVA: 0x00061FA3 File Offset: 0x000601A3
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 ywxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.y, this.w, this.x, this.z);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.y = value.x;
				this.w = value.y;
				this.x = value.z;
				this.z = value.w;
			}
		}

		// Token: 0x17000AA9 RID: 2729
		// (get) Token: 0x0600224C RID: 8780 RVA: 0x00061FD5 File Offset: 0x000601D5
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 ywxw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.y, this.w, this.x, this.w);
			}
		}

		// Token: 0x17000AAA RID: 2730
		// (get) Token: 0x0600224D RID: 8781 RVA: 0x00061FF4 File Offset: 0x000601F4
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 ywyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.y, this.w, this.y, this.x);
			}
		}

		// Token: 0x17000AAB RID: 2731
		// (get) Token: 0x0600224E RID: 8782 RVA: 0x00062013 File Offset: 0x00060213
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 ywyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.y, this.w, this.y, this.y);
			}
		}

		// Token: 0x17000AAC RID: 2732
		// (get) Token: 0x0600224F RID: 8783 RVA: 0x00062032 File Offset: 0x00060232
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 ywyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.y, this.w, this.y, this.z);
			}
		}

		// Token: 0x17000AAD RID: 2733
		// (get) Token: 0x06002250 RID: 8784 RVA: 0x00062051 File Offset: 0x00060251
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 ywyw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.y, this.w, this.y, this.w);
			}
		}

		// Token: 0x17000AAE RID: 2734
		// (get) Token: 0x06002251 RID: 8785 RVA: 0x00062070 File Offset: 0x00060270
		// (set) Token: 0x06002252 RID: 8786 RVA: 0x0006208F File Offset: 0x0006028F
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 ywzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.y, this.w, this.z, this.x);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.y = value.x;
				this.w = value.y;
				this.z = value.z;
				this.x = value.w;
			}
		}

		// Token: 0x17000AAF RID: 2735
		// (get) Token: 0x06002253 RID: 8787 RVA: 0x000620C1 File Offset: 0x000602C1
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 ywzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.y, this.w, this.z, this.y);
			}
		}

		// Token: 0x17000AB0 RID: 2736
		// (get) Token: 0x06002254 RID: 8788 RVA: 0x000620E0 File Offset: 0x000602E0
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 ywzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.y, this.w, this.z, this.z);
			}
		}

		// Token: 0x17000AB1 RID: 2737
		// (get) Token: 0x06002255 RID: 8789 RVA: 0x000620FF File Offset: 0x000602FF
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 ywzw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.y, this.w, this.z, this.w);
			}
		}

		// Token: 0x17000AB2 RID: 2738
		// (get) Token: 0x06002256 RID: 8790 RVA: 0x0006211E File Offset: 0x0006031E
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 ywwx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.y, this.w, this.w, this.x);
			}
		}

		// Token: 0x17000AB3 RID: 2739
		// (get) Token: 0x06002257 RID: 8791 RVA: 0x0006213D File Offset: 0x0006033D
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 ywwy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.y, this.w, this.w, this.y);
			}
		}

		// Token: 0x17000AB4 RID: 2740
		// (get) Token: 0x06002258 RID: 8792 RVA: 0x0006215C File Offset: 0x0006035C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 ywwz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.y, this.w, this.w, this.z);
			}
		}

		// Token: 0x17000AB5 RID: 2741
		// (get) Token: 0x06002259 RID: 8793 RVA: 0x0006217B File Offset: 0x0006037B
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 ywww
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.y, this.w, this.w, this.w);
			}
		}

		// Token: 0x17000AB6 RID: 2742
		// (get) Token: 0x0600225A RID: 8794 RVA: 0x0006219A File Offset: 0x0006039A
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 zxxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.z, this.x, this.x, this.x);
			}
		}

		// Token: 0x17000AB7 RID: 2743
		// (get) Token: 0x0600225B RID: 8795 RVA: 0x000621B9 File Offset: 0x000603B9
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 zxxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.z, this.x, this.x, this.y);
			}
		}

		// Token: 0x17000AB8 RID: 2744
		// (get) Token: 0x0600225C RID: 8796 RVA: 0x000621D8 File Offset: 0x000603D8
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 zxxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.z, this.x, this.x, this.z);
			}
		}

		// Token: 0x17000AB9 RID: 2745
		// (get) Token: 0x0600225D RID: 8797 RVA: 0x000621F7 File Offset: 0x000603F7
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 zxxw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.z, this.x, this.x, this.w);
			}
		}

		// Token: 0x17000ABA RID: 2746
		// (get) Token: 0x0600225E RID: 8798 RVA: 0x00062216 File Offset: 0x00060416
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 zxyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.z, this.x, this.y, this.x);
			}
		}

		// Token: 0x17000ABB RID: 2747
		// (get) Token: 0x0600225F RID: 8799 RVA: 0x00062235 File Offset: 0x00060435
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 zxyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.z, this.x, this.y, this.y);
			}
		}

		// Token: 0x17000ABC RID: 2748
		// (get) Token: 0x06002260 RID: 8800 RVA: 0x00062254 File Offset: 0x00060454
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 zxyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.z, this.x, this.y, this.z);
			}
		}

		// Token: 0x17000ABD RID: 2749
		// (get) Token: 0x06002261 RID: 8801 RVA: 0x00062273 File Offset: 0x00060473
		// (set) Token: 0x06002262 RID: 8802 RVA: 0x00062292 File Offset: 0x00060492
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 zxyw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.z, this.x, this.y, this.w);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.z = value.x;
				this.x = value.y;
				this.y = value.z;
				this.w = value.w;
			}
		}

		// Token: 0x17000ABE RID: 2750
		// (get) Token: 0x06002263 RID: 8803 RVA: 0x000622C4 File Offset: 0x000604C4
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 zxzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.z, this.x, this.z, this.x);
			}
		}

		// Token: 0x17000ABF RID: 2751
		// (get) Token: 0x06002264 RID: 8804 RVA: 0x000622E3 File Offset: 0x000604E3
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 zxzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.z, this.x, this.z, this.y);
			}
		}

		// Token: 0x17000AC0 RID: 2752
		// (get) Token: 0x06002265 RID: 8805 RVA: 0x00062302 File Offset: 0x00060502
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 zxzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.z, this.x, this.z, this.z);
			}
		}

		// Token: 0x17000AC1 RID: 2753
		// (get) Token: 0x06002266 RID: 8806 RVA: 0x00062321 File Offset: 0x00060521
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 zxzw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.z, this.x, this.z, this.w);
			}
		}

		// Token: 0x17000AC2 RID: 2754
		// (get) Token: 0x06002267 RID: 8807 RVA: 0x00062340 File Offset: 0x00060540
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 zxwx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.z, this.x, this.w, this.x);
			}
		}

		// Token: 0x17000AC3 RID: 2755
		// (get) Token: 0x06002268 RID: 8808 RVA: 0x0006235F File Offset: 0x0006055F
		// (set) Token: 0x06002269 RID: 8809 RVA: 0x0006237E File Offset: 0x0006057E
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 zxwy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.z, this.x, this.w, this.y);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.z = value.x;
				this.x = value.y;
				this.w = value.z;
				this.y = value.w;
			}
		}

		// Token: 0x17000AC4 RID: 2756
		// (get) Token: 0x0600226A RID: 8810 RVA: 0x000623B0 File Offset: 0x000605B0
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 zxwz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.z, this.x, this.w, this.z);
			}
		}

		// Token: 0x17000AC5 RID: 2757
		// (get) Token: 0x0600226B RID: 8811 RVA: 0x000623CF File Offset: 0x000605CF
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 zxww
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.z, this.x, this.w, this.w);
			}
		}

		// Token: 0x17000AC6 RID: 2758
		// (get) Token: 0x0600226C RID: 8812 RVA: 0x000623EE File Offset: 0x000605EE
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 zyxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.z, this.y, this.x, this.x);
			}
		}

		// Token: 0x17000AC7 RID: 2759
		// (get) Token: 0x0600226D RID: 8813 RVA: 0x0006240D File Offset: 0x0006060D
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 zyxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.z, this.y, this.x, this.y);
			}
		}

		// Token: 0x17000AC8 RID: 2760
		// (get) Token: 0x0600226E RID: 8814 RVA: 0x0006242C File Offset: 0x0006062C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 zyxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.z, this.y, this.x, this.z);
			}
		}

		// Token: 0x17000AC9 RID: 2761
		// (get) Token: 0x0600226F RID: 8815 RVA: 0x0006244B File Offset: 0x0006064B
		// (set) Token: 0x06002270 RID: 8816 RVA: 0x0006246A File Offset: 0x0006066A
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 zyxw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.z, this.y, this.x, this.w);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.z = value.x;
				this.y = value.y;
				this.x = value.z;
				this.w = value.w;
			}
		}

		// Token: 0x17000ACA RID: 2762
		// (get) Token: 0x06002271 RID: 8817 RVA: 0x0006249C File Offset: 0x0006069C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 zyyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.z, this.y, this.y, this.x);
			}
		}

		// Token: 0x17000ACB RID: 2763
		// (get) Token: 0x06002272 RID: 8818 RVA: 0x000624BB File Offset: 0x000606BB
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 zyyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.z, this.y, this.y, this.y);
			}
		}

		// Token: 0x17000ACC RID: 2764
		// (get) Token: 0x06002273 RID: 8819 RVA: 0x000624DA File Offset: 0x000606DA
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 zyyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.z, this.y, this.y, this.z);
			}
		}

		// Token: 0x17000ACD RID: 2765
		// (get) Token: 0x06002274 RID: 8820 RVA: 0x000624F9 File Offset: 0x000606F9
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 zyyw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.z, this.y, this.y, this.w);
			}
		}

		// Token: 0x17000ACE RID: 2766
		// (get) Token: 0x06002275 RID: 8821 RVA: 0x00062518 File Offset: 0x00060718
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 zyzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.z, this.y, this.z, this.x);
			}
		}

		// Token: 0x17000ACF RID: 2767
		// (get) Token: 0x06002276 RID: 8822 RVA: 0x00062537 File Offset: 0x00060737
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 zyzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.z, this.y, this.z, this.y);
			}
		}

		// Token: 0x17000AD0 RID: 2768
		// (get) Token: 0x06002277 RID: 8823 RVA: 0x00062556 File Offset: 0x00060756
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 zyzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.z, this.y, this.z, this.z);
			}
		}

		// Token: 0x17000AD1 RID: 2769
		// (get) Token: 0x06002278 RID: 8824 RVA: 0x00062575 File Offset: 0x00060775
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 zyzw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.z, this.y, this.z, this.w);
			}
		}

		// Token: 0x17000AD2 RID: 2770
		// (get) Token: 0x06002279 RID: 8825 RVA: 0x00062594 File Offset: 0x00060794
		// (set) Token: 0x0600227A RID: 8826 RVA: 0x000625B3 File Offset: 0x000607B3
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 zywx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.z, this.y, this.w, this.x);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.z = value.x;
				this.y = value.y;
				this.w = value.z;
				this.x = value.w;
			}
		}

		// Token: 0x17000AD3 RID: 2771
		// (get) Token: 0x0600227B RID: 8827 RVA: 0x000625E5 File Offset: 0x000607E5
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 zywy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.z, this.y, this.w, this.y);
			}
		}

		// Token: 0x17000AD4 RID: 2772
		// (get) Token: 0x0600227C RID: 8828 RVA: 0x00062604 File Offset: 0x00060804
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 zywz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.z, this.y, this.w, this.z);
			}
		}

		// Token: 0x17000AD5 RID: 2773
		// (get) Token: 0x0600227D RID: 8829 RVA: 0x00062623 File Offset: 0x00060823
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 zyww
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.z, this.y, this.w, this.w);
			}
		}

		// Token: 0x17000AD6 RID: 2774
		// (get) Token: 0x0600227E RID: 8830 RVA: 0x00062642 File Offset: 0x00060842
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 zzxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.z, this.z, this.x, this.x);
			}
		}

		// Token: 0x17000AD7 RID: 2775
		// (get) Token: 0x0600227F RID: 8831 RVA: 0x00062661 File Offset: 0x00060861
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 zzxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.z, this.z, this.x, this.y);
			}
		}

		// Token: 0x17000AD8 RID: 2776
		// (get) Token: 0x06002280 RID: 8832 RVA: 0x00062680 File Offset: 0x00060880
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 zzxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.z, this.z, this.x, this.z);
			}
		}

		// Token: 0x17000AD9 RID: 2777
		// (get) Token: 0x06002281 RID: 8833 RVA: 0x0006269F File Offset: 0x0006089F
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 zzxw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.z, this.z, this.x, this.w);
			}
		}

		// Token: 0x17000ADA RID: 2778
		// (get) Token: 0x06002282 RID: 8834 RVA: 0x000626BE File Offset: 0x000608BE
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 zzyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.z, this.z, this.y, this.x);
			}
		}

		// Token: 0x17000ADB RID: 2779
		// (get) Token: 0x06002283 RID: 8835 RVA: 0x000626DD File Offset: 0x000608DD
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 zzyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.z, this.z, this.y, this.y);
			}
		}

		// Token: 0x17000ADC RID: 2780
		// (get) Token: 0x06002284 RID: 8836 RVA: 0x000626FC File Offset: 0x000608FC
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 zzyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.z, this.z, this.y, this.z);
			}
		}

		// Token: 0x17000ADD RID: 2781
		// (get) Token: 0x06002285 RID: 8837 RVA: 0x0006271B File Offset: 0x0006091B
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 zzyw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.z, this.z, this.y, this.w);
			}
		}

		// Token: 0x17000ADE RID: 2782
		// (get) Token: 0x06002286 RID: 8838 RVA: 0x0006273A File Offset: 0x0006093A
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 zzzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.z, this.z, this.z, this.x);
			}
		}

		// Token: 0x17000ADF RID: 2783
		// (get) Token: 0x06002287 RID: 8839 RVA: 0x00062759 File Offset: 0x00060959
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 zzzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.z, this.z, this.z, this.y);
			}
		}

		// Token: 0x17000AE0 RID: 2784
		// (get) Token: 0x06002288 RID: 8840 RVA: 0x00062778 File Offset: 0x00060978
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 zzzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.z, this.z, this.z, this.z);
			}
		}

		// Token: 0x17000AE1 RID: 2785
		// (get) Token: 0x06002289 RID: 8841 RVA: 0x00062797 File Offset: 0x00060997
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 zzzw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.z, this.z, this.z, this.w);
			}
		}

		// Token: 0x17000AE2 RID: 2786
		// (get) Token: 0x0600228A RID: 8842 RVA: 0x000627B6 File Offset: 0x000609B6
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 zzwx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.z, this.z, this.w, this.x);
			}
		}

		// Token: 0x17000AE3 RID: 2787
		// (get) Token: 0x0600228B RID: 8843 RVA: 0x000627D5 File Offset: 0x000609D5
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 zzwy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.z, this.z, this.w, this.y);
			}
		}

		// Token: 0x17000AE4 RID: 2788
		// (get) Token: 0x0600228C RID: 8844 RVA: 0x000627F4 File Offset: 0x000609F4
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 zzwz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.z, this.z, this.w, this.z);
			}
		}

		// Token: 0x17000AE5 RID: 2789
		// (get) Token: 0x0600228D RID: 8845 RVA: 0x00062813 File Offset: 0x00060A13
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 zzww
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.z, this.z, this.w, this.w);
			}
		}

		// Token: 0x17000AE6 RID: 2790
		// (get) Token: 0x0600228E RID: 8846 RVA: 0x00062832 File Offset: 0x00060A32
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 zwxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.z, this.w, this.x, this.x);
			}
		}

		// Token: 0x17000AE7 RID: 2791
		// (get) Token: 0x0600228F RID: 8847 RVA: 0x00062851 File Offset: 0x00060A51
		// (set) Token: 0x06002290 RID: 8848 RVA: 0x00062870 File Offset: 0x00060A70
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 zwxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.z, this.w, this.x, this.y);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.z = value.x;
				this.w = value.y;
				this.x = value.z;
				this.y = value.w;
			}
		}

		// Token: 0x17000AE8 RID: 2792
		// (get) Token: 0x06002291 RID: 8849 RVA: 0x000628A2 File Offset: 0x00060AA2
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 zwxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.z, this.w, this.x, this.z);
			}
		}

		// Token: 0x17000AE9 RID: 2793
		// (get) Token: 0x06002292 RID: 8850 RVA: 0x000628C1 File Offset: 0x00060AC1
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 zwxw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.z, this.w, this.x, this.w);
			}
		}

		// Token: 0x17000AEA RID: 2794
		// (get) Token: 0x06002293 RID: 8851 RVA: 0x000628E0 File Offset: 0x00060AE0
		// (set) Token: 0x06002294 RID: 8852 RVA: 0x000628FF File Offset: 0x00060AFF
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 zwyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.z, this.w, this.y, this.x);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.z = value.x;
				this.w = value.y;
				this.y = value.z;
				this.x = value.w;
			}
		}

		// Token: 0x17000AEB RID: 2795
		// (get) Token: 0x06002295 RID: 8853 RVA: 0x00062931 File Offset: 0x00060B31
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 zwyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.z, this.w, this.y, this.y);
			}
		}

		// Token: 0x17000AEC RID: 2796
		// (get) Token: 0x06002296 RID: 8854 RVA: 0x00062950 File Offset: 0x00060B50
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 zwyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.z, this.w, this.y, this.z);
			}
		}

		// Token: 0x17000AED RID: 2797
		// (get) Token: 0x06002297 RID: 8855 RVA: 0x0006296F File Offset: 0x00060B6F
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 zwyw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.z, this.w, this.y, this.w);
			}
		}

		// Token: 0x17000AEE RID: 2798
		// (get) Token: 0x06002298 RID: 8856 RVA: 0x0006298E File Offset: 0x00060B8E
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 zwzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.z, this.w, this.z, this.x);
			}
		}

		// Token: 0x17000AEF RID: 2799
		// (get) Token: 0x06002299 RID: 8857 RVA: 0x000629AD File Offset: 0x00060BAD
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 zwzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.z, this.w, this.z, this.y);
			}
		}

		// Token: 0x17000AF0 RID: 2800
		// (get) Token: 0x0600229A RID: 8858 RVA: 0x000629CC File Offset: 0x00060BCC
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 zwzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.z, this.w, this.z, this.z);
			}
		}

		// Token: 0x17000AF1 RID: 2801
		// (get) Token: 0x0600229B RID: 8859 RVA: 0x000629EB File Offset: 0x00060BEB
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 zwzw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.z, this.w, this.z, this.w);
			}
		}

		// Token: 0x17000AF2 RID: 2802
		// (get) Token: 0x0600229C RID: 8860 RVA: 0x00062A0A File Offset: 0x00060C0A
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 zwwx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.z, this.w, this.w, this.x);
			}
		}

		// Token: 0x17000AF3 RID: 2803
		// (get) Token: 0x0600229D RID: 8861 RVA: 0x00062A29 File Offset: 0x00060C29
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 zwwy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.z, this.w, this.w, this.y);
			}
		}

		// Token: 0x17000AF4 RID: 2804
		// (get) Token: 0x0600229E RID: 8862 RVA: 0x00062A48 File Offset: 0x00060C48
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 zwwz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.z, this.w, this.w, this.z);
			}
		}

		// Token: 0x17000AF5 RID: 2805
		// (get) Token: 0x0600229F RID: 8863 RVA: 0x00062A67 File Offset: 0x00060C67
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 zwww
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.z, this.w, this.w, this.w);
			}
		}

		// Token: 0x17000AF6 RID: 2806
		// (get) Token: 0x060022A0 RID: 8864 RVA: 0x00062A86 File Offset: 0x00060C86
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 wxxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.w, this.x, this.x, this.x);
			}
		}

		// Token: 0x17000AF7 RID: 2807
		// (get) Token: 0x060022A1 RID: 8865 RVA: 0x00062AA5 File Offset: 0x00060CA5
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 wxxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.w, this.x, this.x, this.y);
			}
		}

		// Token: 0x17000AF8 RID: 2808
		// (get) Token: 0x060022A2 RID: 8866 RVA: 0x00062AC4 File Offset: 0x00060CC4
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 wxxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.w, this.x, this.x, this.z);
			}
		}

		// Token: 0x17000AF9 RID: 2809
		// (get) Token: 0x060022A3 RID: 8867 RVA: 0x00062AE3 File Offset: 0x00060CE3
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 wxxw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.w, this.x, this.x, this.w);
			}
		}

		// Token: 0x17000AFA RID: 2810
		// (get) Token: 0x060022A4 RID: 8868 RVA: 0x00062B02 File Offset: 0x00060D02
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 wxyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.w, this.x, this.y, this.x);
			}
		}

		// Token: 0x17000AFB RID: 2811
		// (get) Token: 0x060022A5 RID: 8869 RVA: 0x00062B21 File Offset: 0x00060D21
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 wxyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.w, this.x, this.y, this.y);
			}
		}

		// Token: 0x17000AFC RID: 2812
		// (get) Token: 0x060022A6 RID: 8870 RVA: 0x00062B40 File Offset: 0x00060D40
		// (set) Token: 0x060022A7 RID: 8871 RVA: 0x00062B5F File Offset: 0x00060D5F
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 wxyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.w, this.x, this.y, this.z);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.w = value.x;
				this.x = value.y;
				this.y = value.z;
				this.z = value.w;
			}
		}

		// Token: 0x17000AFD RID: 2813
		// (get) Token: 0x060022A8 RID: 8872 RVA: 0x00062B91 File Offset: 0x00060D91
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 wxyw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.w, this.x, this.y, this.w);
			}
		}

		// Token: 0x17000AFE RID: 2814
		// (get) Token: 0x060022A9 RID: 8873 RVA: 0x00062BB0 File Offset: 0x00060DB0
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 wxzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.w, this.x, this.z, this.x);
			}
		}

		// Token: 0x17000AFF RID: 2815
		// (get) Token: 0x060022AA RID: 8874 RVA: 0x00062BCF File Offset: 0x00060DCF
		// (set) Token: 0x060022AB RID: 8875 RVA: 0x00062BEE File Offset: 0x00060DEE
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 wxzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.w, this.x, this.z, this.y);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.w = value.x;
				this.x = value.y;
				this.z = value.z;
				this.y = value.w;
			}
		}

		// Token: 0x17000B00 RID: 2816
		// (get) Token: 0x060022AC RID: 8876 RVA: 0x00062C20 File Offset: 0x00060E20
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 wxzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.w, this.x, this.z, this.z);
			}
		}

		// Token: 0x17000B01 RID: 2817
		// (get) Token: 0x060022AD RID: 8877 RVA: 0x00062C3F File Offset: 0x00060E3F
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 wxzw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.w, this.x, this.z, this.w);
			}
		}

		// Token: 0x17000B02 RID: 2818
		// (get) Token: 0x060022AE RID: 8878 RVA: 0x00062C5E File Offset: 0x00060E5E
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 wxwx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.w, this.x, this.w, this.x);
			}
		}

		// Token: 0x17000B03 RID: 2819
		// (get) Token: 0x060022AF RID: 8879 RVA: 0x00062C7D File Offset: 0x00060E7D
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 wxwy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.w, this.x, this.w, this.y);
			}
		}

		// Token: 0x17000B04 RID: 2820
		// (get) Token: 0x060022B0 RID: 8880 RVA: 0x00062C9C File Offset: 0x00060E9C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 wxwz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.w, this.x, this.w, this.z);
			}
		}

		// Token: 0x17000B05 RID: 2821
		// (get) Token: 0x060022B1 RID: 8881 RVA: 0x00062CBB File Offset: 0x00060EBB
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 wxww
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.w, this.x, this.w, this.w);
			}
		}

		// Token: 0x17000B06 RID: 2822
		// (get) Token: 0x060022B2 RID: 8882 RVA: 0x00062CDA File Offset: 0x00060EDA
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 wyxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.w, this.y, this.x, this.x);
			}
		}

		// Token: 0x17000B07 RID: 2823
		// (get) Token: 0x060022B3 RID: 8883 RVA: 0x00062CF9 File Offset: 0x00060EF9
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 wyxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.w, this.y, this.x, this.y);
			}
		}

		// Token: 0x17000B08 RID: 2824
		// (get) Token: 0x060022B4 RID: 8884 RVA: 0x00062D18 File Offset: 0x00060F18
		// (set) Token: 0x060022B5 RID: 8885 RVA: 0x00062D37 File Offset: 0x00060F37
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 wyxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.w, this.y, this.x, this.z);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.w = value.x;
				this.y = value.y;
				this.x = value.z;
				this.z = value.w;
			}
		}

		// Token: 0x17000B09 RID: 2825
		// (get) Token: 0x060022B6 RID: 8886 RVA: 0x00062D69 File Offset: 0x00060F69
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 wyxw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.w, this.y, this.x, this.w);
			}
		}

		// Token: 0x17000B0A RID: 2826
		// (get) Token: 0x060022B7 RID: 8887 RVA: 0x00062D88 File Offset: 0x00060F88
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 wyyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.w, this.y, this.y, this.x);
			}
		}

		// Token: 0x17000B0B RID: 2827
		// (get) Token: 0x060022B8 RID: 8888 RVA: 0x00062DA7 File Offset: 0x00060FA7
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 wyyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.w, this.y, this.y, this.y);
			}
		}

		// Token: 0x17000B0C RID: 2828
		// (get) Token: 0x060022B9 RID: 8889 RVA: 0x00062DC6 File Offset: 0x00060FC6
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 wyyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.w, this.y, this.y, this.z);
			}
		}

		// Token: 0x17000B0D RID: 2829
		// (get) Token: 0x060022BA RID: 8890 RVA: 0x00062DE5 File Offset: 0x00060FE5
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 wyyw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.w, this.y, this.y, this.w);
			}
		}

		// Token: 0x17000B0E RID: 2830
		// (get) Token: 0x060022BB RID: 8891 RVA: 0x00062E04 File Offset: 0x00061004
		// (set) Token: 0x060022BC RID: 8892 RVA: 0x00062E23 File Offset: 0x00061023
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 wyzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.w, this.y, this.z, this.x);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.w = value.x;
				this.y = value.y;
				this.z = value.z;
				this.x = value.w;
			}
		}

		// Token: 0x17000B0F RID: 2831
		// (get) Token: 0x060022BD RID: 8893 RVA: 0x00062E55 File Offset: 0x00061055
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 wyzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.w, this.y, this.z, this.y);
			}
		}

		// Token: 0x17000B10 RID: 2832
		// (get) Token: 0x060022BE RID: 8894 RVA: 0x00062E74 File Offset: 0x00061074
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 wyzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.w, this.y, this.z, this.z);
			}
		}

		// Token: 0x17000B11 RID: 2833
		// (get) Token: 0x060022BF RID: 8895 RVA: 0x00062E93 File Offset: 0x00061093
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 wyzw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.w, this.y, this.z, this.w);
			}
		}

		// Token: 0x17000B12 RID: 2834
		// (get) Token: 0x060022C0 RID: 8896 RVA: 0x00062EB2 File Offset: 0x000610B2
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 wywx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.w, this.y, this.w, this.x);
			}
		}

		// Token: 0x17000B13 RID: 2835
		// (get) Token: 0x060022C1 RID: 8897 RVA: 0x00062ED1 File Offset: 0x000610D1
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 wywy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.w, this.y, this.w, this.y);
			}
		}

		// Token: 0x17000B14 RID: 2836
		// (get) Token: 0x060022C2 RID: 8898 RVA: 0x00062EF0 File Offset: 0x000610F0
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 wywz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.w, this.y, this.w, this.z);
			}
		}

		// Token: 0x17000B15 RID: 2837
		// (get) Token: 0x060022C3 RID: 8899 RVA: 0x00062F0F File Offset: 0x0006110F
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 wyww
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.w, this.y, this.w, this.w);
			}
		}

		// Token: 0x17000B16 RID: 2838
		// (get) Token: 0x060022C4 RID: 8900 RVA: 0x00062F2E File Offset: 0x0006112E
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 wzxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.w, this.z, this.x, this.x);
			}
		}

		// Token: 0x17000B17 RID: 2839
		// (get) Token: 0x060022C5 RID: 8901 RVA: 0x00062F4D File Offset: 0x0006114D
		// (set) Token: 0x060022C6 RID: 8902 RVA: 0x00062F6C File Offset: 0x0006116C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 wzxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.w, this.z, this.x, this.y);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.w = value.x;
				this.z = value.y;
				this.x = value.z;
				this.y = value.w;
			}
		}

		// Token: 0x17000B18 RID: 2840
		// (get) Token: 0x060022C7 RID: 8903 RVA: 0x00062F9E File Offset: 0x0006119E
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 wzxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.w, this.z, this.x, this.z);
			}
		}

		// Token: 0x17000B19 RID: 2841
		// (get) Token: 0x060022C8 RID: 8904 RVA: 0x00062FBD File Offset: 0x000611BD
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 wzxw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.w, this.z, this.x, this.w);
			}
		}

		// Token: 0x17000B1A RID: 2842
		// (get) Token: 0x060022C9 RID: 8905 RVA: 0x00062FDC File Offset: 0x000611DC
		// (set) Token: 0x060022CA RID: 8906 RVA: 0x00062FFB File Offset: 0x000611FB
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 wzyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.w, this.z, this.y, this.x);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.w = value.x;
				this.z = value.y;
				this.y = value.z;
				this.x = value.w;
			}
		}

		// Token: 0x17000B1B RID: 2843
		// (get) Token: 0x060022CB RID: 8907 RVA: 0x0006302D File Offset: 0x0006122D
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 wzyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.w, this.z, this.y, this.y);
			}
		}

		// Token: 0x17000B1C RID: 2844
		// (get) Token: 0x060022CC RID: 8908 RVA: 0x0006304C File Offset: 0x0006124C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 wzyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.w, this.z, this.y, this.z);
			}
		}

		// Token: 0x17000B1D RID: 2845
		// (get) Token: 0x060022CD RID: 8909 RVA: 0x0006306B File Offset: 0x0006126B
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 wzyw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.w, this.z, this.y, this.w);
			}
		}

		// Token: 0x17000B1E RID: 2846
		// (get) Token: 0x060022CE RID: 8910 RVA: 0x0006308A File Offset: 0x0006128A
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 wzzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.w, this.z, this.z, this.x);
			}
		}

		// Token: 0x17000B1F RID: 2847
		// (get) Token: 0x060022CF RID: 8911 RVA: 0x000630A9 File Offset: 0x000612A9
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 wzzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.w, this.z, this.z, this.y);
			}
		}

		// Token: 0x17000B20 RID: 2848
		// (get) Token: 0x060022D0 RID: 8912 RVA: 0x000630C8 File Offset: 0x000612C8
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 wzzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.w, this.z, this.z, this.z);
			}
		}

		// Token: 0x17000B21 RID: 2849
		// (get) Token: 0x060022D1 RID: 8913 RVA: 0x000630E7 File Offset: 0x000612E7
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 wzzw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.w, this.z, this.z, this.w);
			}
		}

		// Token: 0x17000B22 RID: 2850
		// (get) Token: 0x060022D2 RID: 8914 RVA: 0x00063106 File Offset: 0x00061306
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 wzwx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.w, this.z, this.w, this.x);
			}
		}

		// Token: 0x17000B23 RID: 2851
		// (get) Token: 0x060022D3 RID: 8915 RVA: 0x00063125 File Offset: 0x00061325
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 wzwy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.w, this.z, this.w, this.y);
			}
		}

		// Token: 0x17000B24 RID: 2852
		// (get) Token: 0x060022D4 RID: 8916 RVA: 0x00063144 File Offset: 0x00061344
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 wzwz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.w, this.z, this.w, this.z);
			}
		}

		// Token: 0x17000B25 RID: 2853
		// (get) Token: 0x060022D5 RID: 8917 RVA: 0x00063163 File Offset: 0x00061363
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 wzww
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.w, this.z, this.w, this.w);
			}
		}

		// Token: 0x17000B26 RID: 2854
		// (get) Token: 0x060022D6 RID: 8918 RVA: 0x00063182 File Offset: 0x00061382
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 wwxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.w, this.w, this.x, this.x);
			}
		}

		// Token: 0x17000B27 RID: 2855
		// (get) Token: 0x060022D7 RID: 8919 RVA: 0x000631A1 File Offset: 0x000613A1
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 wwxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.w, this.w, this.x, this.y);
			}
		}

		// Token: 0x17000B28 RID: 2856
		// (get) Token: 0x060022D8 RID: 8920 RVA: 0x000631C0 File Offset: 0x000613C0
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 wwxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.w, this.w, this.x, this.z);
			}
		}

		// Token: 0x17000B29 RID: 2857
		// (get) Token: 0x060022D9 RID: 8921 RVA: 0x000631DF File Offset: 0x000613DF
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 wwxw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.w, this.w, this.x, this.w);
			}
		}

		// Token: 0x17000B2A RID: 2858
		// (get) Token: 0x060022DA RID: 8922 RVA: 0x000631FE File Offset: 0x000613FE
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 wwyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.w, this.w, this.y, this.x);
			}
		}

		// Token: 0x17000B2B RID: 2859
		// (get) Token: 0x060022DB RID: 8923 RVA: 0x0006321D File Offset: 0x0006141D
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 wwyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.w, this.w, this.y, this.y);
			}
		}

		// Token: 0x17000B2C RID: 2860
		// (get) Token: 0x060022DC RID: 8924 RVA: 0x0006323C File Offset: 0x0006143C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 wwyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.w, this.w, this.y, this.z);
			}
		}

		// Token: 0x17000B2D RID: 2861
		// (get) Token: 0x060022DD RID: 8925 RVA: 0x0006325B File Offset: 0x0006145B
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 wwyw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.w, this.w, this.y, this.w);
			}
		}

		// Token: 0x17000B2E RID: 2862
		// (get) Token: 0x060022DE RID: 8926 RVA: 0x0006327A File Offset: 0x0006147A
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 wwzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.w, this.w, this.z, this.x);
			}
		}

		// Token: 0x17000B2F RID: 2863
		// (get) Token: 0x060022DF RID: 8927 RVA: 0x00063299 File Offset: 0x00061499
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 wwzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.w, this.w, this.z, this.y);
			}
		}

		// Token: 0x17000B30 RID: 2864
		// (get) Token: 0x060022E0 RID: 8928 RVA: 0x000632B8 File Offset: 0x000614B8
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 wwzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.w, this.w, this.z, this.z);
			}
		}

		// Token: 0x17000B31 RID: 2865
		// (get) Token: 0x060022E1 RID: 8929 RVA: 0x000632D7 File Offset: 0x000614D7
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 wwzw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.w, this.w, this.z, this.w);
			}
		}

		// Token: 0x17000B32 RID: 2866
		// (get) Token: 0x060022E2 RID: 8930 RVA: 0x000632F6 File Offset: 0x000614F6
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 wwwx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.w, this.w, this.w, this.x);
			}
		}

		// Token: 0x17000B33 RID: 2867
		// (get) Token: 0x060022E3 RID: 8931 RVA: 0x00063315 File Offset: 0x00061515
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 wwwy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.w, this.w, this.w, this.y);
			}
		}

		// Token: 0x17000B34 RID: 2868
		// (get) Token: 0x060022E4 RID: 8932 RVA: 0x00063334 File Offset: 0x00061534
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 wwwz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.w, this.w, this.w, this.z);
			}
		}

		// Token: 0x17000B35 RID: 2869
		// (get) Token: 0x060022E5 RID: 8933 RVA: 0x00063353 File Offset: 0x00061553
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 wwww
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.w, this.w, this.w, this.w);
			}
		}

		// Token: 0x17000B36 RID: 2870
		// (get) Token: 0x060022E6 RID: 8934 RVA: 0x00063372 File Offset: 0x00061572
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint3 xxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint3(this.x, this.x, this.x);
			}
		}

		// Token: 0x17000B37 RID: 2871
		// (get) Token: 0x060022E7 RID: 8935 RVA: 0x0006338B File Offset: 0x0006158B
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint3 xxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint3(this.x, this.x, this.y);
			}
		}

		// Token: 0x17000B38 RID: 2872
		// (get) Token: 0x060022E8 RID: 8936 RVA: 0x000633A4 File Offset: 0x000615A4
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint3 xxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint3(this.x, this.x, this.z);
			}
		}

		// Token: 0x17000B39 RID: 2873
		// (get) Token: 0x060022E9 RID: 8937 RVA: 0x000633BD File Offset: 0x000615BD
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint3 xxw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint3(this.x, this.x, this.w);
			}
		}

		// Token: 0x17000B3A RID: 2874
		// (get) Token: 0x060022EA RID: 8938 RVA: 0x000633D6 File Offset: 0x000615D6
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint3 xyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint3(this.x, this.y, this.x);
			}
		}

		// Token: 0x17000B3B RID: 2875
		// (get) Token: 0x060022EB RID: 8939 RVA: 0x000633EF File Offset: 0x000615EF
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint3 xyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint3(this.x, this.y, this.y);
			}
		}

		// Token: 0x17000B3C RID: 2876
		// (get) Token: 0x060022EC RID: 8940 RVA: 0x00063408 File Offset: 0x00061608
		// (set) Token: 0x060022ED RID: 8941 RVA: 0x00063421 File Offset: 0x00061621
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint3 xyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint3(this.x, this.y, this.z);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.x = value.x;
				this.y = value.y;
				this.z = value.z;
			}
		}

		// Token: 0x17000B3D RID: 2877
		// (get) Token: 0x060022EE RID: 8942 RVA: 0x00063447 File Offset: 0x00061647
		// (set) Token: 0x060022EF RID: 8943 RVA: 0x00063460 File Offset: 0x00061660
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint3 xyw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint3(this.x, this.y, this.w);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.x = value.x;
				this.y = value.y;
				this.w = value.z;
			}
		}

		// Token: 0x17000B3E RID: 2878
		// (get) Token: 0x060022F0 RID: 8944 RVA: 0x00063486 File Offset: 0x00061686
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint3 xzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint3(this.x, this.z, this.x);
			}
		}

		// Token: 0x17000B3F RID: 2879
		// (get) Token: 0x060022F1 RID: 8945 RVA: 0x0006349F File Offset: 0x0006169F
		// (set) Token: 0x060022F2 RID: 8946 RVA: 0x000634B8 File Offset: 0x000616B8
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint3 xzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint3(this.x, this.z, this.y);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.x = value.x;
				this.z = value.y;
				this.y = value.z;
			}
		}

		// Token: 0x17000B40 RID: 2880
		// (get) Token: 0x060022F3 RID: 8947 RVA: 0x000634DE File Offset: 0x000616DE
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint3 xzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint3(this.x, this.z, this.z);
			}
		}

		// Token: 0x17000B41 RID: 2881
		// (get) Token: 0x060022F4 RID: 8948 RVA: 0x000634F7 File Offset: 0x000616F7
		// (set) Token: 0x060022F5 RID: 8949 RVA: 0x00063510 File Offset: 0x00061710
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint3 xzw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint3(this.x, this.z, this.w);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.x = value.x;
				this.z = value.y;
				this.w = value.z;
			}
		}

		// Token: 0x17000B42 RID: 2882
		// (get) Token: 0x060022F6 RID: 8950 RVA: 0x00063536 File Offset: 0x00061736
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint3 xwx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint3(this.x, this.w, this.x);
			}
		}

		// Token: 0x17000B43 RID: 2883
		// (get) Token: 0x060022F7 RID: 8951 RVA: 0x0006354F File Offset: 0x0006174F
		// (set) Token: 0x060022F8 RID: 8952 RVA: 0x00063568 File Offset: 0x00061768
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint3 xwy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint3(this.x, this.w, this.y);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.x = value.x;
				this.w = value.y;
				this.y = value.z;
			}
		}

		// Token: 0x17000B44 RID: 2884
		// (get) Token: 0x060022F9 RID: 8953 RVA: 0x0006358E File Offset: 0x0006178E
		// (set) Token: 0x060022FA RID: 8954 RVA: 0x000635A7 File Offset: 0x000617A7
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint3 xwz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint3(this.x, this.w, this.z);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.x = value.x;
				this.w = value.y;
				this.z = value.z;
			}
		}

		// Token: 0x17000B45 RID: 2885
		// (get) Token: 0x060022FB RID: 8955 RVA: 0x000635CD File Offset: 0x000617CD
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint3 xww
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint3(this.x, this.w, this.w);
			}
		}

		// Token: 0x17000B46 RID: 2886
		// (get) Token: 0x060022FC RID: 8956 RVA: 0x000635E6 File Offset: 0x000617E6
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint3 yxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint3(this.y, this.x, this.x);
			}
		}

		// Token: 0x17000B47 RID: 2887
		// (get) Token: 0x060022FD RID: 8957 RVA: 0x000635FF File Offset: 0x000617FF
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint3 yxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint3(this.y, this.x, this.y);
			}
		}

		// Token: 0x17000B48 RID: 2888
		// (get) Token: 0x060022FE RID: 8958 RVA: 0x00063618 File Offset: 0x00061818
		// (set) Token: 0x060022FF RID: 8959 RVA: 0x00063631 File Offset: 0x00061831
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint3 yxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint3(this.y, this.x, this.z);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.y = value.x;
				this.x = value.y;
				this.z = value.z;
			}
		}

		// Token: 0x17000B49 RID: 2889
		// (get) Token: 0x06002300 RID: 8960 RVA: 0x00063657 File Offset: 0x00061857
		// (set) Token: 0x06002301 RID: 8961 RVA: 0x00063670 File Offset: 0x00061870
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint3 yxw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint3(this.y, this.x, this.w);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.y = value.x;
				this.x = value.y;
				this.w = value.z;
			}
		}

		// Token: 0x17000B4A RID: 2890
		// (get) Token: 0x06002302 RID: 8962 RVA: 0x00063696 File Offset: 0x00061896
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint3 yyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint3(this.y, this.y, this.x);
			}
		}

		// Token: 0x17000B4B RID: 2891
		// (get) Token: 0x06002303 RID: 8963 RVA: 0x000636AF File Offset: 0x000618AF
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint3 yyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint3(this.y, this.y, this.y);
			}
		}

		// Token: 0x17000B4C RID: 2892
		// (get) Token: 0x06002304 RID: 8964 RVA: 0x000636C8 File Offset: 0x000618C8
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint3 yyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint3(this.y, this.y, this.z);
			}
		}

		// Token: 0x17000B4D RID: 2893
		// (get) Token: 0x06002305 RID: 8965 RVA: 0x000636E1 File Offset: 0x000618E1
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint3 yyw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint3(this.y, this.y, this.w);
			}
		}

		// Token: 0x17000B4E RID: 2894
		// (get) Token: 0x06002306 RID: 8966 RVA: 0x000636FA File Offset: 0x000618FA
		// (set) Token: 0x06002307 RID: 8967 RVA: 0x00063713 File Offset: 0x00061913
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint3 yzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint3(this.y, this.z, this.x);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.y = value.x;
				this.z = value.y;
				this.x = value.z;
			}
		}

		// Token: 0x17000B4F RID: 2895
		// (get) Token: 0x06002308 RID: 8968 RVA: 0x00063739 File Offset: 0x00061939
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint3 yzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint3(this.y, this.z, this.y);
			}
		}

		// Token: 0x17000B50 RID: 2896
		// (get) Token: 0x06002309 RID: 8969 RVA: 0x00063752 File Offset: 0x00061952
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint3 yzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint3(this.y, this.z, this.z);
			}
		}

		// Token: 0x17000B51 RID: 2897
		// (get) Token: 0x0600230A RID: 8970 RVA: 0x0006376B File Offset: 0x0006196B
		// (set) Token: 0x0600230B RID: 8971 RVA: 0x00063784 File Offset: 0x00061984
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint3 yzw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint3(this.y, this.z, this.w);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.y = value.x;
				this.z = value.y;
				this.w = value.z;
			}
		}

		// Token: 0x17000B52 RID: 2898
		// (get) Token: 0x0600230C RID: 8972 RVA: 0x000637AA File Offset: 0x000619AA
		// (set) Token: 0x0600230D RID: 8973 RVA: 0x000637C3 File Offset: 0x000619C3
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint3 ywx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint3(this.y, this.w, this.x);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.y = value.x;
				this.w = value.y;
				this.x = value.z;
			}
		}

		// Token: 0x17000B53 RID: 2899
		// (get) Token: 0x0600230E RID: 8974 RVA: 0x000637E9 File Offset: 0x000619E9
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint3 ywy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint3(this.y, this.w, this.y);
			}
		}

		// Token: 0x17000B54 RID: 2900
		// (get) Token: 0x0600230F RID: 8975 RVA: 0x00063802 File Offset: 0x00061A02
		// (set) Token: 0x06002310 RID: 8976 RVA: 0x0006381B File Offset: 0x00061A1B
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint3 ywz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint3(this.y, this.w, this.z);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.y = value.x;
				this.w = value.y;
				this.z = value.z;
			}
		}

		// Token: 0x17000B55 RID: 2901
		// (get) Token: 0x06002311 RID: 8977 RVA: 0x00063841 File Offset: 0x00061A41
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint3 yww
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint3(this.y, this.w, this.w);
			}
		}

		// Token: 0x17000B56 RID: 2902
		// (get) Token: 0x06002312 RID: 8978 RVA: 0x0006385A File Offset: 0x00061A5A
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint3 zxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint3(this.z, this.x, this.x);
			}
		}

		// Token: 0x17000B57 RID: 2903
		// (get) Token: 0x06002313 RID: 8979 RVA: 0x00063873 File Offset: 0x00061A73
		// (set) Token: 0x06002314 RID: 8980 RVA: 0x0006388C File Offset: 0x00061A8C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint3 zxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint3(this.z, this.x, this.y);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.z = value.x;
				this.x = value.y;
				this.y = value.z;
			}
		}

		// Token: 0x17000B58 RID: 2904
		// (get) Token: 0x06002315 RID: 8981 RVA: 0x000638B2 File Offset: 0x00061AB2
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint3 zxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint3(this.z, this.x, this.z);
			}
		}

		// Token: 0x17000B59 RID: 2905
		// (get) Token: 0x06002316 RID: 8982 RVA: 0x000638CB File Offset: 0x00061ACB
		// (set) Token: 0x06002317 RID: 8983 RVA: 0x000638E4 File Offset: 0x00061AE4
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint3 zxw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint3(this.z, this.x, this.w);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.z = value.x;
				this.x = value.y;
				this.w = value.z;
			}
		}

		// Token: 0x17000B5A RID: 2906
		// (get) Token: 0x06002318 RID: 8984 RVA: 0x0006390A File Offset: 0x00061B0A
		// (set) Token: 0x06002319 RID: 8985 RVA: 0x00063923 File Offset: 0x00061B23
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint3 zyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint3(this.z, this.y, this.x);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.z = value.x;
				this.y = value.y;
				this.x = value.z;
			}
		}

		// Token: 0x17000B5B RID: 2907
		// (get) Token: 0x0600231A RID: 8986 RVA: 0x00063949 File Offset: 0x00061B49
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint3 zyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint3(this.z, this.y, this.y);
			}
		}

		// Token: 0x17000B5C RID: 2908
		// (get) Token: 0x0600231B RID: 8987 RVA: 0x00063962 File Offset: 0x00061B62
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint3 zyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint3(this.z, this.y, this.z);
			}
		}

		// Token: 0x17000B5D RID: 2909
		// (get) Token: 0x0600231C RID: 8988 RVA: 0x0006397B File Offset: 0x00061B7B
		// (set) Token: 0x0600231D RID: 8989 RVA: 0x00063994 File Offset: 0x00061B94
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint3 zyw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint3(this.z, this.y, this.w);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.z = value.x;
				this.y = value.y;
				this.w = value.z;
			}
		}

		// Token: 0x17000B5E RID: 2910
		// (get) Token: 0x0600231E RID: 8990 RVA: 0x000639BA File Offset: 0x00061BBA
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint3 zzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint3(this.z, this.z, this.x);
			}
		}

		// Token: 0x17000B5F RID: 2911
		// (get) Token: 0x0600231F RID: 8991 RVA: 0x000639D3 File Offset: 0x00061BD3
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint3 zzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint3(this.z, this.z, this.y);
			}
		}

		// Token: 0x17000B60 RID: 2912
		// (get) Token: 0x06002320 RID: 8992 RVA: 0x000639EC File Offset: 0x00061BEC
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint3 zzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint3(this.z, this.z, this.z);
			}
		}

		// Token: 0x17000B61 RID: 2913
		// (get) Token: 0x06002321 RID: 8993 RVA: 0x00063A05 File Offset: 0x00061C05
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint3 zzw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint3(this.z, this.z, this.w);
			}
		}

		// Token: 0x17000B62 RID: 2914
		// (get) Token: 0x06002322 RID: 8994 RVA: 0x00063A1E File Offset: 0x00061C1E
		// (set) Token: 0x06002323 RID: 8995 RVA: 0x00063A37 File Offset: 0x00061C37
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint3 zwx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint3(this.z, this.w, this.x);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.z = value.x;
				this.w = value.y;
				this.x = value.z;
			}
		}

		// Token: 0x17000B63 RID: 2915
		// (get) Token: 0x06002324 RID: 8996 RVA: 0x00063A5D File Offset: 0x00061C5D
		// (set) Token: 0x06002325 RID: 8997 RVA: 0x00063A76 File Offset: 0x00061C76
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint3 zwy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint3(this.z, this.w, this.y);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.z = value.x;
				this.w = value.y;
				this.y = value.z;
			}
		}

		// Token: 0x17000B64 RID: 2916
		// (get) Token: 0x06002326 RID: 8998 RVA: 0x00063A9C File Offset: 0x00061C9C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint3 zwz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint3(this.z, this.w, this.z);
			}
		}

		// Token: 0x17000B65 RID: 2917
		// (get) Token: 0x06002327 RID: 8999 RVA: 0x00063AB5 File Offset: 0x00061CB5
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint3 zww
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint3(this.z, this.w, this.w);
			}
		}

		// Token: 0x17000B66 RID: 2918
		// (get) Token: 0x06002328 RID: 9000 RVA: 0x00063ACE File Offset: 0x00061CCE
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint3 wxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint3(this.w, this.x, this.x);
			}
		}

		// Token: 0x17000B67 RID: 2919
		// (get) Token: 0x06002329 RID: 9001 RVA: 0x00063AE7 File Offset: 0x00061CE7
		// (set) Token: 0x0600232A RID: 9002 RVA: 0x00063B00 File Offset: 0x00061D00
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint3 wxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint3(this.w, this.x, this.y);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.w = value.x;
				this.x = value.y;
				this.y = value.z;
			}
		}

		// Token: 0x17000B68 RID: 2920
		// (get) Token: 0x0600232B RID: 9003 RVA: 0x00063B26 File Offset: 0x00061D26
		// (set) Token: 0x0600232C RID: 9004 RVA: 0x00063B3F File Offset: 0x00061D3F
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint3 wxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint3(this.w, this.x, this.z);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.w = value.x;
				this.x = value.y;
				this.z = value.z;
			}
		}

		// Token: 0x17000B69 RID: 2921
		// (get) Token: 0x0600232D RID: 9005 RVA: 0x00063B65 File Offset: 0x00061D65
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint3 wxw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint3(this.w, this.x, this.w);
			}
		}

		// Token: 0x17000B6A RID: 2922
		// (get) Token: 0x0600232E RID: 9006 RVA: 0x00063B7E File Offset: 0x00061D7E
		// (set) Token: 0x0600232F RID: 9007 RVA: 0x00063B97 File Offset: 0x00061D97
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint3 wyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint3(this.w, this.y, this.x);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.w = value.x;
				this.y = value.y;
				this.x = value.z;
			}
		}

		// Token: 0x17000B6B RID: 2923
		// (get) Token: 0x06002330 RID: 9008 RVA: 0x00063BBD File Offset: 0x00061DBD
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint3 wyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint3(this.w, this.y, this.y);
			}
		}

		// Token: 0x17000B6C RID: 2924
		// (get) Token: 0x06002331 RID: 9009 RVA: 0x00063BD6 File Offset: 0x00061DD6
		// (set) Token: 0x06002332 RID: 9010 RVA: 0x00063BEF File Offset: 0x00061DEF
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint3 wyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint3(this.w, this.y, this.z);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.w = value.x;
				this.y = value.y;
				this.z = value.z;
			}
		}

		// Token: 0x17000B6D RID: 2925
		// (get) Token: 0x06002333 RID: 9011 RVA: 0x00063C15 File Offset: 0x00061E15
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint3 wyw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint3(this.w, this.y, this.w);
			}
		}

		// Token: 0x17000B6E RID: 2926
		// (get) Token: 0x06002334 RID: 9012 RVA: 0x00063C2E File Offset: 0x00061E2E
		// (set) Token: 0x06002335 RID: 9013 RVA: 0x00063C47 File Offset: 0x00061E47
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint3 wzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint3(this.w, this.z, this.x);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.w = value.x;
				this.z = value.y;
				this.x = value.z;
			}
		}

		// Token: 0x17000B6F RID: 2927
		// (get) Token: 0x06002336 RID: 9014 RVA: 0x00063C6D File Offset: 0x00061E6D
		// (set) Token: 0x06002337 RID: 9015 RVA: 0x00063C86 File Offset: 0x00061E86
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint3 wzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint3(this.w, this.z, this.y);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.w = value.x;
				this.z = value.y;
				this.y = value.z;
			}
		}

		// Token: 0x17000B70 RID: 2928
		// (get) Token: 0x06002338 RID: 9016 RVA: 0x00063CAC File Offset: 0x00061EAC
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint3 wzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint3(this.w, this.z, this.z);
			}
		}

		// Token: 0x17000B71 RID: 2929
		// (get) Token: 0x06002339 RID: 9017 RVA: 0x00063CC5 File Offset: 0x00061EC5
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint3 wzw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint3(this.w, this.z, this.w);
			}
		}

		// Token: 0x17000B72 RID: 2930
		// (get) Token: 0x0600233A RID: 9018 RVA: 0x00063CDE File Offset: 0x00061EDE
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint3 wwx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint3(this.w, this.w, this.x);
			}
		}

		// Token: 0x17000B73 RID: 2931
		// (get) Token: 0x0600233B RID: 9019 RVA: 0x00063CF7 File Offset: 0x00061EF7
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint3 wwy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint3(this.w, this.w, this.y);
			}
		}

		// Token: 0x17000B74 RID: 2932
		// (get) Token: 0x0600233C RID: 9020 RVA: 0x00063D10 File Offset: 0x00061F10
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint3 wwz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint3(this.w, this.w, this.z);
			}
		}

		// Token: 0x17000B75 RID: 2933
		// (get) Token: 0x0600233D RID: 9021 RVA: 0x00063D29 File Offset: 0x00061F29
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint3 www
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint3(this.w, this.w, this.w);
			}
		}

		// Token: 0x17000B76 RID: 2934
		// (get) Token: 0x0600233E RID: 9022 RVA: 0x00063D42 File Offset: 0x00061F42
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint2 xx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint2(this.x, this.x);
			}
		}

		// Token: 0x17000B77 RID: 2935
		// (get) Token: 0x0600233F RID: 9023 RVA: 0x00063D55 File Offset: 0x00061F55
		// (set) Token: 0x06002340 RID: 9024 RVA: 0x00063D68 File Offset: 0x00061F68
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

		// Token: 0x17000B78 RID: 2936
		// (get) Token: 0x06002341 RID: 9025 RVA: 0x00063D82 File Offset: 0x00061F82
		// (set) Token: 0x06002342 RID: 9026 RVA: 0x00063D95 File Offset: 0x00061F95
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint2 xz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint2(this.x, this.z);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.x = value.x;
				this.z = value.y;
			}
		}

		// Token: 0x17000B79 RID: 2937
		// (get) Token: 0x06002343 RID: 9027 RVA: 0x00063DAF File Offset: 0x00061FAF
		// (set) Token: 0x06002344 RID: 9028 RVA: 0x00063DC2 File Offset: 0x00061FC2
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint2 xw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint2(this.x, this.w);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.x = value.x;
				this.w = value.y;
			}
		}

		// Token: 0x17000B7A RID: 2938
		// (get) Token: 0x06002345 RID: 9029 RVA: 0x00063DDC File Offset: 0x00061FDC
		// (set) Token: 0x06002346 RID: 9030 RVA: 0x00063DEF File Offset: 0x00061FEF
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

		// Token: 0x17000B7B RID: 2939
		// (get) Token: 0x06002347 RID: 9031 RVA: 0x00063E09 File Offset: 0x00062009
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint2 yy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint2(this.y, this.y);
			}
		}

		// Token: 0x17000B7C RID: 2940
		// (get) Token: 0x06002348 RID: 9032 RVA: 0x00063E1C File Offset: 0x0006201C
		// (set) Token: 0x06002349 RID: 9033 RVA: 0x00063E2F File Offset: 0x0006202F
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint2 yz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint2(this.y, this.z);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.y = value.x;
				this.z = value.y;
			}
		}

		// Token: 0x17000B7D RID: 2941
		// (get) Token: 0x0600234A RID: 9034 RVA: 0x00063E49 File Offset: 0x00062049
		// (set) Token: 0x0600234B RID: 9035 RVA: 0x00063E5C File Offset: 0x0006205C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint2 yw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint2(this.y, this.w);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.y = value.x;
				this.w = value.y;
			}
		}

		// Token: 0x17000B7E RID: 2942
		// (get) Token: 0x0600234C RID: 9036 RVA: 0x00063E76 File Offset: 0x00062076
		// (set) Token: 0x0600234D RID: 9037 RVA: 0x00063E89 File Offset: 0x00062089
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint2 zx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint2(this.z, this.x);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.z = value.x;
				this.x = value.y;
			}
		}

		// Token: 0x17000B7F RID: 2943
		// (get) Token: 0x0600234E RID: 9038 RVA: 0x00063EA3 File Offset: 0x000620A3
		// (set) Token: 0x0600234F RID: 9039 RVA: 0x00063EB6 File Offset: 0x000620B6
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint2 zy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint2(this.z, this.y);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.z = value.x;
				this.y = value.y;
			}
		}

		// Token: 0x17000B80 RID: 2944
		// (get) Token: 0x06002350 RID: 9040 RVA: 0x00063ED0 File Offset: 0x000620D0
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint2 zz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint2(this.z, this.z);
			}
		}

		// Token: 0x17000B81 RID: 2945
		// (get) Token: 0x06002351 RID: 9041 RVA: 0x00063EE3 File Offset: 0x000620E3
		// (set) Token: 0x06002352 RID: 9042 RVA: 0x00063EF6 File Offset: 0x000620F6
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint2 zw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint2(this.z, this.w);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.z = value.x;
				this.w = value.y;
			}
		}

		// Token: 0x17000B82 RID: 2946
		// (get) Token: 0x06002353 RID: 9043 RVA: 0x00063F10 File Offset: 0x00062110
		// (set) Token: 0x06002354 RID: 9044 RVA: 0x00063F23 File Offset: 0x00062123
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint2 wx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint2(this.w, this.x);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.w = value.x;
				this.x = value.y;
			}
		}

		// Token: 0x17000B83 RID: 2947
		// (get) Token: 0x06002355 RID: 9045 RVA: 0x00063F3D File Offset: 0x0006213D
		// (set) Token: 0x06002356 RID: 9046 RVA: 0x00063F50 File Offset: 0x00062150
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint2 wy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint2(this.w, this.y);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.w = value.x;
				this.y = value.y;
			}
		}

		// Token: 0x17000B84 RID: 2948
		// (get) Token: 0x06002357 RID: 9047 RVA: 0x00063F6A File Offset: 0x0006216A
		// (set) Token: 0x06002358 RID: 9048 RVA: 0x00063F7D File Offset: 0x0006217D
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint2 wz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint2(this.w, this.z);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.w = value.x;
				this.z = value.y;
			}
		}

		// Token: 0x17000B85 RID: 2949
		// (get) Token: 0x06002359 RID: 9049 RVA: 0x00063F97 File Offset: 0x00062197
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint2 ww
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint2(this.w, this.w);
			}
		}

		// Token: 0x17000B86 RID: 2950
		public unsafe uint this[int index]
		{
			get
			{
				fixed (uint4* ptr = &this)
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

		// Token: 0x0600235C RID: 9052 RVA: 0x00063FE4 File Offset: 0x000621E4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool Equals(uint4 rhs)
		{
			return this.x == rhs.x && this.y == rhs.y && this.z == rhs.z && this.w == rhs.w;
		}

		// Token: 0x0600235D RID: 9053 RVA: 0x00064020 File Offset: 0x00062220
		public override bool Equals(object o)
		{
			if (o is uint4)
			{
				uint4 rhs = (uint4)o;
				return this.Equals(rhs);
			}
			return false;
		}

		// Token: 0x0600235E RID: 9054 RVA: 0x00064045 File Offset: 0x00062245
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override int GetHashCode()
		{
			return (int)math.hash(this);
		}

		// Token: 0x0600235F RID: 9055 RVA: 0x00064054 File Offset: 0x00062254
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override string ToString()
		{
			return string.Format("uint4({0}, {1}, {2}, {3})", new object[]
			{
				this.x,
				this.y,
				this.z,
				this.w
			});
		}

		// Token: 0x06002360 RID: 9056 RVA: 0x000640AC File Offset: 0x000622AC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public string ToString(string format, IFormatProvider formatProvider)
		{
			return string.Format("uint4({0}, {1}, {2}, {3})", new object[]
			{
				this.x.ToString(format, formatProvider),
				this.y.ToString(format, formatProvider),
				this.z.ToString(format, formatProvider),
				this.w.ToString(format, formatProvider)
			});
		}

		// Token: 0x04000109 RID: 265
		public uint x;

		// Token: 0x0400010A RID: 266
		public uint y;

		// Token: 0x0400010B RID: 267
		public uint z;

		// Token: 0x0400010C RID: 268
		public uint w;

		// Token: 0x0400010D RID: 269
		public static readonly uint4 zero;

		// Token: 0x02000062 RID: 98
		internal sealed class DebuggerProxy
		{
			// Token: 0x06002478 RID: 9336 RVA: 0x00067764 File Offset: 0x00065964
			public DebuggerProxy(uint4 v)
			{
				this.x = v.x;
				this.y = v.y;
				this.z = v.z;
				this.w = v.w;
			}

			// Token: 0x04000165 RID: 357
			public uint x;

			// Token: 0x04000166 RID: 358
			public uint y;

			// Token: 0x04000167 RID: 359
			public uint z;

			// Token: 0x04000168 RID: 360
			public uint w;
		}
	}
}

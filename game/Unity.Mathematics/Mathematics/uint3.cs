using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Unity.IL2CPP.CompilerServices;

namespace Unity.Mathematics
{
	// Token: 0x02000042 RID: 66
	[DebuggerTypeProxy(typeof(uint3.DebuggerProxy))]
	[Il2CppEagerStaticClassConstruction]
	[Serializable]
	public struct uint3 : IEquatable<uint3>, IFormattable
	{
		// Token: 0x06001FD2 RID: 8146 RVA: 0x0005BE19 File Offset: 0x0005A019
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public uint3(uint x, uint y, uint z)
		{
			this.x = x;
			this.y = y;
			this.z = z;
		}

		// Token: 0x06001FD3 RID: 8147 RVA: 0x0005BE30 File Offset: 0x0005A030
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public uint3(uint x, uint2 yz)
		{
			this.x = x;
			this.y = yz.x;
			this.z = yz.y;
		}

		// Token: 0x06001FD4 RID: 8148 RVA: 0x0005BE51 File Offset: 0x0005A051
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public uint3(uint2 xy, uint z)
		{
			this.x = xy.x;
			this.y = xy.y;
			this.z = z;
		}

		// Token: 0x06001FD5 RID: 8149 RVA: 0x0005BE72 File Offset: 0x0005A072
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public uint3(uint3 xyz)
		{
			this.x = xyz.x;
			this.y = xyz.y;
			this.z = xyz.z;
		}

		// Token: 0x06001FD6 RID: 8150 RVA: 0x0005BE98 File Offset: 0x0005A098
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public uint3(uint v)
		{
			this.x = v;
			this.y = v;
			this.z = v;
		}

		// Token: 0x06001FD7 RID: 8151 RVA: 0x0005BEAF File Offset: 0x0005A0AF
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public uint3(bool v)
		{
			this.x = (v ? 1U : 0U);
			this.y = (v ? 1U : 0U);
			this.z = (v ? 1U : 0U);
		}

		// Token: 0x06001FD8 RID: 8152 RVA: 0x0005BED8 File Offset: 0x0005A0D8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public uint3(bool3 v)
		{
			this.x = (v.x ? 1U : 0U);
			this.y = (v.y ? 1U : 0U);
			this.z = (v.z ? 1U : 0U);
		}

		// Token: 0x06001FD9 RID: 8153 RVA: 0x0005BF10 File Offset: 0x0005A110
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public uint3(int v)
		{
			this.x = (uint)v;
			this.y = (uint)v;
			this.z = (uint)v;
		}

		// Token: 0x06001FDA RID: 8154 RVA: 0x0005BF27 File Offset: 0x0005A127
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public uint3(int3 v)
		{
			this.x = (uint)v.x;
			this.y = (uint)v.y;
			this.z = (uint)v.z;
		}

		// Token: 0x06001FDB RID: 8155 RVA: 0x0005BF4D File Offset: 0x0005A14D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public uint3(float v)
		{
			this.x = (uint)v;
			this.y = (uint)v;
			this.z = (uint)v;
		}

		// Token: 0x06001FDC RID: 8156 RVA: 0x0005BF67 File Offset: 0x0005A167
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public uint3(float3 v)
		{
			this.x = (uint)v.x;
			this.y = (uint)v.y;
			this.z = (uint)v.z;
		}

		// Token: 0x06001FDD RID: 8157 RVA: 0x0005BF90 File Offset: 0x0005A190
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public uint3(double v)
		{
			this.x = (uint)v;
			this.y = (uint)v;
			this.z = (uint)v;
		}

		// Token: 0x06001FDE RID: 8158 RVA: 0x0005BFAA File Offset: 0x0005A1AA
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public uint3(double3 v)
		{
			this.x = (uint)v.x;
			this.y = (uint)v.y;
			this.z = (uint)v.z;
		}

		// Token: 0x06001FDF RID: 8159 RVA: 0x0005BFD3 File Offset: 0x0005A1D3
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator uint3(uint v)
		{
			return new uint3(v);
		}

		// Token: 0x06001FE0 RID: 8160 RVA: 0x0005BFDB File Offset: 0x0005A1DB
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator uint3(bool v)
		{
			return new uint3(v);
		}

		// Token: 0x06001FE1 RID: 8161 RVA: 0x0005BFE3 File Offset: 0x0005A1E3
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator uint3(bool3 v)
		{
			return new uint3(v);
		}

		// Token: 0x06001FE2 RID: 8162 RVA: 0x0005BFEB File Offset: 0x0005A1EB
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator uint3(int v)
		{
			return new uint3(v);
		}

		// Token: 0x06001FE3 RID: 8163 RVA: 0x0005BFF3 File Offset: 0x0005A1F3
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator uint3(int3 v)
		{
			return new uint3(v);
		}

		// Token: 0x06001FE4 RID: 8164 RVA: 0x0005BFFB File Offset: 0x0005A1FB
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator uint3(float v)
		{
			return new uint3(v);
		}

		// Token: 0x06001FE5 RID: 8165 RVA: 0x0005C003 File Offset: 0x0005A203
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator uint3(float3 v)
		{
			return new uint3(v);
		}

		// Token: 0x06001FE6 RID: 8166 RVA: 0x0005C00B File Offset: 0x0005A20B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator uint3(double v)
		{
			return new uint3(v);
		}

		// Token: 0x06001FE7 RID: 8167 RVA: 0x0005C013 File Offset: 0x0005A213
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator uint3(double3 v)
		{
			return new uint3(v);
		}

		// Token: 0x06001FE8 RID: 8168 RVA: 0x0005C01B File Offset: 0x0005A21B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3 operator *(uint3 lhs, uint3 rhs)
		{
			return new uint3(lhs.x * rhs.x, lhs.y * rhs.y, lhs.z * rhs.z);
		}

		// Token: 0x06001FE9 RID: 8169 RVA: 0x0005C049 File Offset: 0x0005A249
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3 operator *(uint3 lhs, uint rhs)
		{
			return new uint3(lhs.x * rhs, lhs.y * rhs, lhs.z * rhs);
		}

		// Token: 0x06001FEA RID: 8170 RVA: 0x0005C068 File Offset: 0x0005A268
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3 operator *(uint lhs, uint3 rhs)
		{
			return new uint3(lhs * rhs.x, lhs * rhs.y, lhs * rhs.z);
		}

		// Token: 0x06001FEB RID: 8171 RVA: 0x0005C087 File Offset: 0x0005A287
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3 operator +(uint3 lhs, uint3 rhs)
		{
			return new uint3(lhs.x + rhs.x, lhs.y + rhs.y, lhs.z + rhs.z);
		}

		// Token: 0x06001FEC RID: 8172 RVA: 0x0005C0B5 File Offset: 0x0005A2B5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3 operator +(uint3 lhs, uint rhs)
		{
			return new uint3(lhs.x + rhs, lhs.y + rhs, lhs.z + rhs);
		}

		// Token: 0x06001FED RID: 8173 RVA: 0x0005C0D4 File Offset: 0x0005A2D4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3 operator +(uint lhs, uint3 rhs)
		{
			return new uint3(lhs + rhs.x, lhs + rhs.y, lhs + rhs.z);
		}

		// Token: 0x06001FEE RID: 8174 RVA: 0x0005C0F3 File Offset: 0x0005A2F3
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3 operator -(uint3 lhs, uint3 rhs)
		{
			return new uint3(lhs.x - rhs.x, lhs.y - rhs.y, lhs.z - rhs.z);
		}

		// Token: 0x06001FEF RID: 8175 RVA: 0x0005C121 File Offset: 0x0005A321
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3 operator -(uint3 lhs, uint rhs)
		{
			return new uint3(lhs.x - rhs, lhs.y - rhs, lhs.z - rhs);
		}

		// Token: 0x06001FF0 RID: 8176 RVA: 0x0005C140 File Offset: 0x0005A340
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3 operator -(uint lhs, uint3 rhs)
		{
			return new uint3(lhs - rhs.x, lhs - rhs.y, lhs - rhs.z);
		}

		// Token: 0x06001FF1 RID: 8177 RVA: 0x0005C15F File Offset: 0x0005A35F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3 operator /(uint3 lhs, uint3 rhs)
		{
			return new uint3(lhs.x / rhs.x, lhs.y / rhs.y, lhs.z / rhs.z);
		}

		// Token: 0x06001FF2 RID: 8178 RVA: 0x0005C18D File Offset: 0x0005A38D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3 operator /(uint3 lhs, uint rhs)
		{
			return new uint3(lhs.x / rhs, lhs.y / rhs, lhs.z / rhs);
		}

		// Token: 0x06001FF3 RID: 8179 RVA: 0x0005C1AC File Offset: 0x0005A3AC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3 operator /(uint lhs, uint3 rhs)
		{
			return new uint3(lhs / rhs.x, lhs / rhs.y, lhs / rhs.z);
		}

		// Token: 0x06001FF4 RID: 8180 RVA: 0x0005C1CB File Offset: 0x0005A3CB
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3 operator %(uint3 lhs, uint3 rhs)
		{
			return new uint3(lhs.x % rhs.x, lhs.y % rhs.y, lhs.z % rhs.z);
		}

		// Token: 0x06001FF5 RID: 8181 RVA: 0x0005C1F9 File Offset: 0x0005A3F9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3 operator %(uint3 lhs, uint rhs)
		{
			return new uint3(lhs.x % rhs, lhs.y % rhs, lhs.z % rhs);
		}

		// Token: 0x06001FF6 RID: 8182 RVA: 0x0005C218 File Offset: 0x0005A418
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3 operator %(uint lhs, uint3 rhs)
		{
			return new uint3(lhs % rhs.x, lhs % rhs.y, lhs % rhs.z);
		}

		// Token: 0x06001FF7 RID: 8183 RVA: 0x0005C238 File Offset: 0x0005A438
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3 operator ++(uint3 val)
		{
			uint num = val.x + 1U;
			val.x = num;
			uint num2 = num;
			num = val.y + 1U;
			val.y = num;
			uint num3 = num;
			num = val.z + 1U;
			val.z = num;
			return new uint3(num2, num3, num);
		}

		// Token: 0x06001FF8 RID: 8184 RVA: 0x0005C278 File Offset: 0x0005A478
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3 operator --(uint3 val)
		{
			uint num = val.x - 1U;
			val.x = num;
			uint num2 = num;
			num = val.y - 1U;
			val.y = num;
			uint num3 = num;
			num = val.z - 1U;
			val.z = num;
			return new uint3(num2, num3, num);
		}

		// Token: 0x06001FF9 RID: 8185 RVA: 0x0005C2B7 File Offset: 0x0005A4B7
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3 operator <(uint3 lhs, uint3 rhs)
		{
			return new bool3(lhs.x < rhs.x, lhs.y < rhs.y, lhs.z < rhs.z);
		}

		// Token: 0x06001FFA RID: 8186 RVA: 0x0005C2E8 File Offset: 0x0005A4E8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3 operator <(uint3 lhs, uint rhs)
		{
			return new bool3(lhs.x < rhs, lhs.y < rhs, lhs.z < rhs);
		}

		// Token: 0x06001FFB RID: 8187 RVA: 0x0005C30A File Offset: 0x0005A50A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3 operator <(uint lhs, uint3 rhs)
		{
			return new bool3(lhs < rhs.x, lhs < rhs.y, lhs < rhs.z);
		}

		// Token: 0x06001FFC RID: 8188 RVA: 0x0005C32C File Offset: 0x0005A52C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3 operator <=(uint3 lhs, uint3 rhs)
		{
			return new bool3(lhs.x <= rhs.x, lhs.y <= rhs.y, lhs.z <= rhs.z);
		}

		// Token: 0x06001FFD RID: 8189 RVA: 0x0005C366 File Offset: 0x0005A566
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3 operator <=(uint3 lhs, uint rhs)
		{
			return new bool3(lhs.x <= rhs, lhs.y <= rhs, lhs.z <= rhs);
		}

		// Token: 0x06001FFE RID: 8190 RVA: 0x0005C391 File Offset: 0x0005A591
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3 operator <=(uint lhs, uint3 rhs)
		{
			return new bool3(lhs <= rhs.x, lhs <= rhs.y, lhs <= rhs.z);
		}

		// Token: 0x06001FFF RID: 8191 RVA: 0x0005C3BC File Offset: 0x0005A5BC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3 operator >(uint3 lhs, uint3 rhs)
		{
			return new bool3(lhs.x > rhs.x, lhs.y > rhs.y, lhs.z > rhs.z);
		}

		// Token: 0x06002000 RID: 8192 RVA: 0x0005C3ED File Offset: 0x0005A5ED
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3 operator >(uint3 lhs, uint rhs)
		{
			return new bool3(lhs.x > rhs, lhs.y > rhs, lhs.z > rhs);
		}

		// Token: 0x06002001 RID: 8193 RVA: 0x0005C40F File Offset: 0x0005A60F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3 operator >(uint lhs, uint3 rhs)
		{
			return new bool3(lhs > rhs.x, lhs > rhs.y, lhs > rhs.z);
		}

		// Token: 0x06002002 RID: 8194 RVA: 0x0005C431 File Offset: 0x0005A631
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3 operator >=(uint3 lhs, uint3 rhs)
		{
			return new bool3(lhs.x >= rhs.x, lhs.y >= rhs.y, lhs.z >= rhs.z);
		}

		// Token: 0x06002003 RID: 8195 RVA: 0x0005C46B File Offset: 0x0005A66B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3 operator >=(uint3 lhs, uint rhs)
		{
			return new bool3(lhs.x >= rhs, lhs.y >= rhs, lhs.z >= rhs);
		}

		// Token: 0x06002004 RID: 8196 RVA: 0x0005C496 File Offset: 0x0005A696
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3 operator >=(uint lhs, uint3 rhs)
		{
			return new bool3(lhs >= rhs.x, lhs >= rhs.y, lhs >= rhs.z);
		}

		// Token: 0x06002005 RID: 8197 RVA: 0x0005C4C1 File Offset: 0x0005A6C1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3 operator -(uint3 val)
		{
			return new uint3((uint)(-(uint)((ulong)val.x)), (uint)(-(uint)((ulong)val.y)), (uint)(-(uint)((ulong)val.z)));
		}

		// Token: 0x06002006 RID: 8198 RVA: 0x0005C4E3 File Offset: 0x0005A6E3
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3 operator +(uint3 val)
		{
			return new uint3(val.x, val.y, val.z);
		}

		// Token: 0x06002007 RID: 8199 RVA: 0x0005C4FC File Offset: 0x0005A6FC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3 operator <<(uint3 x, int n)
		{
			return new uint3(x.x << n, x.y << n, x.z << n);
		}

		// Token: 0x06002008 RID: 8200 RVA: 0x0005C524 File Offset: 0x0005A724
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3 operator >>(uint3 x, int n)
		{
			return new uint3(x.x >> n, x.y >> n, x.z >> n);
		}

		// Token: 0x06002009 RID: 8201 RVA: 0x0005C54C File Offset: 0x0005A74C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3 operator ==(uint3 lhs, uint3 rhs)
		{
			return new bool3(lhs.x == rhs.x, lhs.y == rhs.y, lhs.z == rhs.z);
		}

		// Token: 0x0600200A RID: 8202 RVA: 0x0005C57D File Offset: 0x0005A77D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3 operator ==(uint3 lhs, uint rhs)
		{
			return new bool3(lhs.x == rhs, lhs.y == rhs, lhs.z == rhs);
		}

		// Token: 0x0600200B RID: 8203 RVA: 0x0005C59F File Offset: 0x0005A79F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3 operator ==(uint lhs, uint3 rhs)
		{
			return new bool3(lhs == rhs.x, lhs == rhs.y, lhs == rhs.z);
		}

		// Token: 0x0600200C RID: 8204 RVA: 0x0005C5C1 File Offset: 0x0005A7C1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3 operator !=(uint3 lhs, uint3 rhs)
		{
			return new bool3(lhs.x != rhs.x, lhs.y != rhs.y, lhs.z != rhs.z);
		}

		// Token: 0x0600200D RID: 8205 RVA: 0x0005C5FB File Offset: 0x0005A7FB
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3 operator !=(uint3 lhs, uint rhs)
		{
			return new bool3(lhs.x != rhs, lhs.y != rhs, lhs.z != rhs);
		}

		// Token: 0x0600200E RID: 8206 RVA: 0x0005C626 File Offset: 0x0005A826
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3 operator !=(uint lhs, uint3 rhs)
		{
			return new bool3(lhs != rhs.x, lhs != rhs.y, lhs != rhs.z);
		}

		// Token: 0x0600200F RID: 8207 RVA: 0x0005C651 File Offset: 0x0005A851
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3 operator ~(uint3 val)
		{
			return new uint3(~val.x, ~val.y, ~val.z);
		}

		// Token: 0x06002010 RID: 8208 RVA: 0x0005C66D File Offset: 0x0005A86D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3 operator &(uint3 lhs, uint3 rhs)
		{
			return new uint3(lhs.x & rhs.x, lhs.y & rhs.y, lhs.z & rhs.z);
		}

		// Token: 0x06002011 RID: 8209 RVA: 0x0005C69B File Offset: 0x0005A89B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3 operator &(uint3 lhs, uint rhs)
		{
			return new uint3(lhs.x & rhs, lhs.y & rhs, lhs.z & rhs);
		}

		// Token: 0x06002012 RID: 8210 RVA: 0x0005C6BA File Offset: 0x0005A8BA
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3 operator &(uint lhs, uint3 rhs)
		{
			return new uint3(lhs & rhs.x, lhs & rhs.y, lhs & rhs.z);
		}

		// Token: 0x06002013 RID: 8211 RVA: 0x0005C6D9 File Offset: 0x0005A8D9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3 operator |(uint3 lhs, uint3 rhs)
		{
			return new uint3(lhs.x | rhs.x, lhs.y | rhs.y, lhs.z | rhs.z);
		}

		// Token: 0x06002014 RID: 8212 RVA: 0x0005C707 File Offset: 0x0005A907
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3 operator |(uint3 lhs, uint rhs)
		{
			return new uint3(lhs.x | rhs, lhs.y | rhs, lhs.z | rhs);
		}

		// Token: 0x06002015 RID: 8213 RVA: 0x0005C726 File Offset: 0x0005A926
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3 operator |(uint lhs, uint3 rhs)
		{
			return new uint3(lhs | rhs.x, lhs | rhs.y, lhs | rhs.z);
		}

		// Token: 0x06002016 RID: 8214 RVA: 0x0005C745 File Offset: 0x0005A945
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3 operator ^(uint3 lhs, uint3 rhs)
		{
			return new uint3(lhs.x ^ rhs.x, lhs.y ^ rhs.y, lhs.z ^ rhs.z);
		}

		// Token: 0x06002017 RID: 8215 RVA: 0x0005C773 File Offset: 0x0005A973
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3 operator ^(uint3 lhs, uint rhs)
		{
			return new uint3(lhs.x ^ rhs, lhs.y ^ rhs, lhs.z ^ rhs);
		}

		// Token: 0x06002018 RID: 8216 RVA: 0x0005C792 File Offset: 0x0005A992
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3 operator ^(uint lhs, uint3 rhs)
		{
			return new uint3(lhs ^ rhs.x, lhs ^ rhs.y, lhs ^ rhs.z);
		}

		// Token: 0x170009BD RID: 2493
		// (get) Token: 0x06002019 RID: 8217 RVA: 0x0005C7B1 File Offset: 0x0005A9B1
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 xxxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.x, this.x, this.x, this.x);
			}
		}

		// Token: 0x170009BE RID: 2494
		// (get) Token: 0x0600201A RID: 8218 RVA: 0x0005C7D0 File Offset: 0x0005A9D0
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 xxxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.x, this.x, this.x, this.y);
			}
		}

		// Token: 0x170009BF RID: 2495
		// (get) Token: 0x0600201B RID: 8219 RVA: 0x0005C7EF File Offset: 0x0005A9EF
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 xxxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.x, this.x, this.x, this.z);
			}
		}

		// Token: 0x170009C0 RID: 2496
		// (get) Token: 0x0600201C RID: 8220 RVA: 0x0005C80E File Offset: 0x0005AA0E
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 xxyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.x, this.x, this.y, this.x);
			}
		}

		// Token: 0x170009C1 RID: 2497
		// (get) Token: 0x0600201D RID: 8221 RVA: 0x0005C82D File Offset: 0x0005AA2D
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 xxyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.x, this.x, this.y, this.y);
			}
		}

		// Token: 0x170009C2 RID: 2498
		// (get) Token: 0x0600201E RID: 8222 RVA: 0x0005C84C File Offset: 0x0005AA4C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 xxyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.x, this.x, this.y, this.z);
			}
		}

		// Token: 0x170009C3 RID: 2499
		// (get) Token: 0x0600201F RID: 8223 RVA: 0x0005C86B File Offset: 0x0005AA6B
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 xxzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.x, this.x, this.z, this.x);
			}
		}

		// Token: 0x170009C4 RID: 2500
		// (get) Token: 0x06002020 RID: 8224 RVA: 0x0005C88A File Offset: 0x0005AA8A
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 xxzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.x, this.x, this.z, this.y);
			}
		}

		// Token: 0x170009C5 RID: 2501
		// (get) Token: 0x06002021 RID: 8225 RVA: 0x0005C8A9 File Offset: 0x0005AAA9
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 xxzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.x, this.x, this.z, this.z);
			}
		}

		// Token: 0x170009C6 RID: 2502
		// (get) Token: 0x06002022 RID: 8226 RVA: 0x0005C8C8 File Offset: 0x0005AAC8
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 xyxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.x, this.y, this.x, this.x);
			}
		}

		// Token: 0x170009C7 RID: 2503
		// (get) Token: 0x06002023 RID: 8227 RVA: 0x0005C8E7 File Offset: 0x0005AAE7
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 xyxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.x, this.y, this.x, this.y);
			}
		}

		// Token: 0x170009C8 RID: 2504
		// (get) Token: 0x06002024 RID: 8228 RVA: 0x0005C906 File Offset: 0x0005AB06
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 xyxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.x, this.y, this.x, this.z);
			}
		}

		// Token: 0x170009C9 RID: 2505
		// (get) Token: 0x06002025 RID: 8229 RVA: 0x0005C925 File Offset: 0x0005AB25
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 xyyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.x, this.y, this.y, this.x);
			}
		}

		// Token: 0x170009CA RID: 2506
		// (get) Token: 0x06002026 RID: 8230 RVA: 0x0005C944 File Offset: 0x0005AB44
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 xyyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.x, this.y, this.y, this.y);
			}
		}

		// Token: 0x170009CB RID: 2507
		// (get) Token: 0x06002027 RID: 8231 RVA: 0x0005C963 File Offset: 0x0005AB63
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 xyyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.x, this.y, this.y, this.z);
			}
		}

		// Token: 0x170009CC RID: 2508
		// (get) Token: 0x06002028 RID: 8232 RVA: 0x0005C982 File Offset: 0x0005AB82
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 xyzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.x, this.y, this.z, this.x);
			}
		}

		// Token: 0x170009CD RID: 2509
		// (get) Token: 0x06002029 RID: 8233 RVA: 0x0005C9A1 File Offset: 0x0005ABA1
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 xyzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.x, this.y, this.z, this.y);
			}
		}

		// Token: 0x170009CE RID: 2510
		// (get) Token: 0x0600202A RID: 8234 RVA: 0x0005C9C0 File Offset: 0x0005ABC0
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 xyzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.x, this.y, this.z, this.z);
			}
		}

		// Token: 0x170009CF RID: 2511
		// (get) Token: 0x0600202B RID: 8235 RVA: 0x0005C9DF File Offset: 0x0005ABDF
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 xzxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.x, this.z, this.x, this.x);
			}
		}

		// Token: 0x170009D0 RID: 2512
		// (get) Token: 0x0600202C RID: 8236 RVA: 0x0005C9FE File Offset: 0x0005ABFE
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 xzxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.x, this.z, this.x, this.y);
			}
		}

		// Token: 0x170009D1 RID: 2513
		// (get) Token: 0x0600202D RID: 8237 RVA: 0x0005CA1D File Offset: 0x0005AC1D
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 xzxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.x, this.z, this.x, this.z);
			}
		}

		// Token: 0x170009D2 RID: 2514
		// (get) Token: 0x0600202E RID: 8238 RVA: 0x0005CA3C File Offset: 0x0005AC3C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 xzyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.x, this.z, this.y, this.x);
			}
		}

		// Token: 0x170009D3 RID: 2515
		// (get) Token: 0x0600202F RID: 8239 RVA: 0x0005CA5B File Offset: 0x0005AC5B
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 xzyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.x, this.z, this.y, this.y);
			}
		}

		// Token: 0x170009D4 RID: 2516
		// (get) Token: 0x06002030 RID: 8240 RVA: 0x0005CA7A File Offset: 0x0005AC7A
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 xzyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.x, this.z, this.y, this.z);
			}
		}

		// Token: 0x170009D5 RID: 2517
		// (get) Token: 0x06002031 RID: 8241 RVA: 0x0005CA99 File Offset: 0x0005AC99
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 xzzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.x, this.z, this.z, this.x);
			}
		}

		// Token: 0x170009D6 RID: 2518
		// (get) Token: 0x06002032 RID: 8242 RVA: 0x0005CAB8 File Offset: 0x0005ACB8
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 xzzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.x, this.z, this.z, this.y);
			}
		}

		// Token: 0x170009D7 RID: 2519
		// (get) Token: 0x06002033 RID: 8243 RVA: 0x0005CAD7 File Offset: 0x0005ACD7
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 xzzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.x, this.z, this.z, this.z);
			}
		}

		// Token: 0x170009D8 RID: 2520
		// (get) Token: 0x06002034 RID: 8244 RVA: 0x0005CAF6 File Offset: 0x0005ACF6
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 yxxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.y, this.x, this.x, this.x);
			}
		}

		// Token: 0x170009D9 RID: 2521
		// (get) Token: 0x06002035 RID: 8245 RVA: 0x0005CB15 File Offset: 0x0005AD15
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 yxxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.y, this.x, this.x, this.y);
			}
		}

		// Token: 0x170009DA RID: 2522
		// (get) Token: 0x06002036 RID: 8246 RVA: 0x0005CB34 File Offset: 0x0005AD34
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 yxxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.y, this.x, this.x, this.z);
			}
		}

		// Token: 0x170009DB RID: 2523
		// (get) Token: 0x06002037 RID: 8247 RVA: 0x0005CB53 File Offset: 0x0005AD53
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 yxyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.y, this.x, this.y, this.x);
			}
		}

		// Token: 0x170009DC RID: 2524
		// (get) Token: 0x06002038 RID: 8248 RVA: 0x0005CB72 File Offset: 0x0005AD72
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 yxyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.y, this.x, this.y, this.y);
			}
		}

		// Token: 0x170009DD RID: 2525
		// (get) Token: 0x06002039 RID: 8249 RVA: 0x0005CB91 File Offset: 0x0005AD91
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 yxyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.y, this.x, this.y, this.z);
			}
		}

		// Token: 0x170009DE RID: 2526
		// (get) Token: 0x0600203A RID: 8250 RVA: 0x0005CBB0 File Offset: 0x0005ADB0
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 yxzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.y, this.x, this.z, this.x);
			}
		}

		// Token: 0x170009DF RID: 2527
		// (get) Token: 0x0600203B RID: 8251 RVA: 0x0005CBCF File Offset: 0x0005ADCF
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 yxzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.y, this.x, this.z, this.y);
			}
		}

		// Token: 0x170009E0 RID: 2528
		// (get) Token: 0x0600203C RID: 8252 RVA: 0x0005CBEE File Offset: 0x0005ADEE
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 yxzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.y, this.x, this.z, this.z);
			}
		}

		// Token: 0x170009E1 RID: 2529
		// (get) Token: 0x0600203D RID: 8253 RVA: 0x0005CC0D File Offset: 0x0005AE0D
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 yyxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.y, this.y, this.x, this.x);
			}
		}

		// Token: 0x170009E2 RID: 2530
		// (get) Token: 0x0600203E RID: 8254 RVA: 0x0005CC2C File Offset: 0x0005AE2C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 yyxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.y, this.y, this.x, this.y);
			}
		}

		// Token: 0x170009E3 RID: 2531
		// (get) Token: 0x0600203F RID: 8255 RVA: 0x0005CC4B File Offset: 0x0005AE4B
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 yyxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.y, this.y, this.x, this.z);
			}
		}

		// Token: 0x170009E4 RID: 2532
		// (get) Token: 0x06002040 RID: 8256 RVA: 0x0005CC6A File Offset: 0x0005AE6A
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 yyyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.y, this.y, this.y, this.x);
			}
		}

		// Token: 0x170009E5 RID: 2533
		// (get) Token: 0x06002041 RID: 8257 RVA: 0x0005CC89 File Offset: 0x0005AE89
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 yyyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.y, this.y, this.y, this.y);
			}
		}

		// Token: 0x170009E6 RID: 2534
		// (get) Token: 0x06002042 RID: 8258 RVA: 0x0005CCA8 File Offset: 0x0005AEA8
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 yyyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.y, this.y, this.y, this.z);
			}
		}

		// Token: 0x170009E7 RID: 2535
		// (get) Token: 0x06002043 RID: 8259 RVA: 0x0005CCC7 File Offset: 0x0005AEC7
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 yyzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.y, this.y, this.z, this.x);
			}
		}

		// Token: 0x170009E8 RID: 2536
		// (get) Token: 0x06002044 RID: 8260 RVA: 0x0005CCE6 File Offset: 0x0005AEE6
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 yyzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.y, this.y, this.z, this.y);
			}
		}

		// Token: 0x170009E9 RID: 2537
		// (get) Token: 0x06002045 RID: 8261 RVA: 0x0005CD05 File Offset: 0x0005AF05
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 yyzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.y, this.y, this.z, this.z);
			}
		}

		// Token: 0x170009EA RID: 2538
		// (get) Token: 0x06002046 RID: 8262 RVA: 0x0005CD24 File Offset: 0x0005AF24
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 yzxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.y, this.z, this.x, this.x);
			}
		}

		// Token: 0x170009EB RID: 2539
		// (get) Token: 0x06002047 RID: 8263 RVA: 0x0005CD43 File Offset: 0x0005AF43
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 yzxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.y, this.z, this.x, this.y);
			}
		}

		// Token: 0x170009EC RID: 2540
		// (get) Token: 0x06002048 RID: 8264 RVA: 0x0005CD62 File Offset: 0x0005AF62
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 yzxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.y, this.z, this.x, this.z);
			}
		}

		// Token: 0x170009ED RID: 2541
		// (get) Token: 0x06002049 RID: 8265 RVA: 0x0005CD81 File Offset: 0x0005AF81
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 yzyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.y, this.z, this.y, this.x);
			}
		}

		// Token: 0x170009EE RID: 2542
		// (get) Token: 0x0600204A RID: 8266 RVA: 0x0005CDA0 File Offset: 0x0005AFA0
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 yzyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.y, this.z, this.y, this.y);
			}
		}

		// Token: 0x170009EF RID: 2543
		// (get) Token: 0x0600204B RID: 8267 RVA: 0x0005CDBF File Offset: 0x0005AFBF
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 yzyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.y, this.z, this.y, this.z);
			}
		}

		// Token: 0x170009F0 RID: 2544
		// (get) Token: 0x0600204C RID: 8268 RVA: 0x0005CDDE File Offset: 0x0005AFDE
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 yzzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.y, this.z, this.z, this.x);
			}
		}

		// Token: 0x170009F1 RID: 2545
		// (get) Token: 0x0600204D RID: 8269 RVA: 0x0005CDFD File Offset: 0x0005AFFD
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 yzzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.y, this.z, this.z, this.y);
			}
		}

		// Token: 0x170009F2 RID: 2546
		// (get) Token: 0x0600204E RID: 8270 RVA: 0x0005CE1C File Offset: 0x0005B01C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 yzzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.y, this.z, this.z, this.z);
			}
		}

		// Token: 0x170009F3 RID: 2547
		// (get) Token: 0x0600204F RID: 8271 RVA: 0x0005CE3B File Offset: 0x0005B03B
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 zxxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.z, this.x, this.x, this.x);
			}
		}

		// Token: 0x170009F4 RID: 2548
		// (get) Token: 0x06002050 RID: 8272 RVA: 0x0005CE5A File Offset: 0x0005B05A
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 zxxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.z, this.x, this.x, this.y);
			}
		}

		// Token: 0x170009F5 RID: 2549
		// (get) Token: 0x06002051 RID: 8273 RVA: 0x0005CE79 File Offset: 0x0005B079
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 zxxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.z, this.x, this.x, this.z);
			}
		}

		// Token: 0x170009F6 RID: 2550
		// (get) Token: 0x06002052 RID: 8274 RVA: 0x0005CE98 File Offset: 0x0005B098
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 zxyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.z, this.x, this.y, this.x);
			}
		}

		// Token: 0x170009F7 RID: 2551
		// (get) Token: 0x06002053 RID: 8275 RVA: 0x0005CEB7 File Offset: 0x0005B0B7
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 zxyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.z, this.x, this.y, this.y);
			}
		}

		// Token: 0x170009F8 RID: 2552
		// (get) Token: 0x06002054 RID: 8276 RVA: 0x0005CED6 File Offset: 0x0005B0D6
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 zxyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.z, this.x, this.y, this.z);
			}
		}

		// Token: 0x170009F9 RID: 2553
		// (get) Token: 0x06002055 RID: 8277 RVA: 0x0005CEF5 File Offset: 0x0005B0F5
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 zxzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.z, this.x, this.z, this.x);
			}
		}

		// Token: 0x170009FA RID: 2554
		// (get) Token: 0x06002056 RID: 8278 RVA: 0x0005CF14 File Offset: 0x0005B114
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 zxzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.z, this.x, this.z, this.y);
			}
		}

		// Token: 0x170009FB RID: 2555
		// (get) Token: 0x06002057 RID: 8279 RVA: 0x0005CF33 File Offset: 0x0005B133
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 zxzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.z, this.x, this.z, this.z);
			}
		}

		// Token: 0x170009FC RID: 2556
		// (get) Token: 0x06002058 RID: 8280 RVA: 0x0005CF52 File Offset: 0x0005B152
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 zyxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.z, this.y, this.x, this.x);
			}
		}

		// Token: 0x170009FD RID: 2557
		// (get) Token: 0x06002059 RID: 8281 RVA: 0x0005CF71 File Offset: 0x0005B171
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 zyxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.z, this.y, this.x, this.y);
			}
		}

		// Token: 0x170009FE RID: 2558
		// (get) Token: 0x0600205A RID: 8282 RVA: 0x0005CF90 File Offset: 0x0005B190
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 zyxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.z, this.y, this.x, this.z);
			}
		}

		// Token: 0x170009FF RID: 2559
		// (get) Token: 0x0600205B RID: 8283 RVA: 0x0005CFAF File Offset: 0x0005B1AF
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 zyyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.z, this.y, this.y, this.x);
			}
		}

		// Token: 0x17000A00 RID: 2560
		// (get) Token: 0x0600205C RID: 8284 RVA: 0x0005CFCE File Offset: 0x0005B1CE
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 zyyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.z, this.y, this.y, this.y);
			}
		}

		// Token: 0x17000A01 RID: 2561
		// (get) Token: 0x0600205D RID: 8285 RVA: 0x0005CFED File Offset: 0x0005B1ED
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 zyyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.z, this.y, this.y, this.z);
			}
		}

		// Token: 0x17000A02 RID: 2562
		// (get) Token: 0x0600205E RID: 8286 RVA: 0x0005D00C File Offset: 0x0005B20C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 zyzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.z, this.y, this.z, this.x);
			}
		}

		// Token: 0x17000A03 RID: 2563
		// (get) Token: 0x0600205F RID: 8287 RVA: 0x0005D02B File Offset: 0x0005B22B
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 zyzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.z, this.y, this.z, this.y);
			}
		}

		// Token: 0x17000A04 RID: 2564
		// (get) Token: 0x06002060 RID: 8288 RVA: 0x0005D04A File Offset: 0x0005B24A
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 zyzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.z, this.y, this.z, this.z);
			}
		}

		// Token: 0x17000A05 RID: 2565
		// (get) Token: 0x06002061 RID: 8289 RVA: 0x0005D069 File Offset: 0x0005B269
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 zzxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.z, this.z, this.x, this.x);
			}
		}

		// Token: 0x17000A06 RID: 2566
		// (get) Token: 0x06002062 RID: 8290 RVA: 0x0005D088 File Offset: 0x0005B288
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 zzxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.z, this.z, this.x, this.y);
			}
		}

		// Token: 0x17000A07 RID: 2567
		// (get) Token: 0x06002063 RID: 8291 RVA: 0x0005D0A7 File Offset: 0x0005B2A7
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 zzxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.z, this.z, this.x, this.z);
			}
		}

		// Token: 0x17000A08 RID: 2568
		// (get) Token: 0x06002064 RID: 8292 RVA: 0x0005D0C6 File Offset: 0x0005B2C6
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 zzyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.z, this.z, this.y, this.x);
			}
		}

		// Token: 0x17000A09 RID: 2569
		// (get) Token: 0x06002065 RID: 8293 RVA: 0x0005D0E5 File Offset: 0x0005B2E5
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 zzyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.z, this.z, this.y, this.y);
			}
		}

		// Token: 0x17000A0A RID: 2570
		// (get) Token: 0x06002066 RID: 8294 RVA: 0x0005D104 File Offset: 0x0005B304
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 zzyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.z, this.z, this.y, this.z);
			}
		}

		// Token: 0x17000A0B RID: 2571
		// (get) Token: 0x06002067 RID: 8295 RVA: 0x0005D123 File Offset: 0x0005B323
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 zzzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.z, this.z, this.z, this.x);
			}
		}

		// Token: 0x17000A0C RID: 2572
		// (get) Token: 0x06002068 RID: 8296 RVA: 0x0005D142 File Offset: 0x0005B342
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 zzzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.z, this.z, this.z, this.y);
			}
		}

		// Token: 0x17000A0D RID: 2573
		// (get) Token: 0x06002069 RID: 8297 RVA: 0x0005D161 File Offset: 0x0005B361
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 zzzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.z, this.z, this.z, this.z);
			}
		}

		// Token: 0x17000A0E RID: 2574
		// (get) Token: 0x0600206A RID: 8298 RVA: 0x0005D180 File Offset: 0x0005B380
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint3 xxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint3(this.x, this.x, this.x);
			}
		}

		// Token: 0x17000A0F RID: 2575
		// (get) Token: 0x0600206B RID: 8299 RVA: 0x0005D199 File Offset: 0x0005B399
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint3 xxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint3(this.x, this.x, this.y);
			}
		}

		// Token: 0x17000A10 RID: 2576
		// (get) Token: 0x0600206C RID: 8300 RVA: 0x0005D1B2 File Offset: 0x0005B3B2
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint3 xxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint3(this.x, this.x, this.z);
			}
		}

		// Token: 0x17000A11 RID: 2577
		// (get) Token: 0x0600206D RID: 8301 RVA: 0x0005D1CB File Offset: 0x0005B3CB
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint3 xyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint3(this.x, this.y, this.x);
			}
		}

		// Token: 0x17000A12 RID: 2578
		// (get) Token: 0x0600206E RID: 8302 RVA: 0x0005D1E4 File Offset: 0x0005B3E4
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint3 xyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint3(this.x, this.y, this.y);
			}
		}

		// Token: 0x17000A13 RID: 2579
		// (get) Token: 0x0600206F RID: 8303 RVA: 0x0005D1FD File Offset: 0x0005B3FD
		// (set) Token: 0x06002070 RID: 8304 RVA: 0x0005D216 File Offset: 0x0005B416
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

		// Token: 0x17000A14 RID: 2580
		// (get) Token: 0x06002071 RID: 8305 RVA: 0x0005D23C File Offset: 0x0005B43C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint3 xzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint3(this.x, this.z, this.x);
			}
		}

		// Token: 0x17000A15 RID: 2581
		// (get) Token: 0x06002072 RID: 8306 RVA: 0x0005D255 File Offset: 0x0005B455
		// (set) Token: 0x06002073 RID: 8307 RVA: 0x0005D26E File Offset: 0x0005B46E
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

		// Token: 0x17000A16 RID: 2582
		// (get) Token: 0x06002074 RID: 8308 RVA: 0x0005D294 File Offset: 0x0005B494
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint3 xzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint3(this.x, this.z, this.z);
			}
		}

		// Token: 0x17000A17 RID: 2583
		// (get) Token: 0x06002075 RID: 8309 RVA: 0x0005D2AD File Offset: 0x0005B4AD
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint3 yxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint3(this.y, this.x, this.x);
			}
		}

		// Token: 0x17000A18 RID: 2584
		// (get) Token: 0x06002076 RID: 8310 RVA: 0x0005D2C6 File Offset: 0x0005B4C6
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint3 yxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint3(this.y, this.x, this.y);
			}
		}

		// Token: 0x17000A19 RID: 2585
		// (get) Token: 0x06002077 RID: 8311 RVA: 0x0005D2DF File Offset: 0x0005B4DF
		// (set) Token: 0x06002078 RID: 8312 RVA: 0x0005D2F8 File Offset: 0x0005B4F8
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

		// Token: 0x17000A1A RID: 2586
		// (get) Token: 0x06002079 RID: 8313 RVA: 0x0005D31E File Offset: 0x0005B51E
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint3 yyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint3(this.y, this.y, this.x);
			}
		}

		// Token: 0x17000A1B RID: 2587
		// (get) Token: 0x0600207A RID: 8314 RVA: 0x0005D337 File Offset: 0x0005B537
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint3 yyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint3(this.y, this.y, this.y);
			}
		}

		// Token: 0x17000A1C RID: 2588
		// (get) Token: 0x0600207B RID: 8315 RVA: 0x0005D350 File Offset: 0x0005B550
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint3 yyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint3(this.y, this.y, this.z);
			}
		}

		// Token: 0x17000A1D RID: 2589
		// (get) Token: 0x0600207C RID: 8316 RVA: 0x0005D369 File Offset: 0x0005B569
		// (set) Token: 0x0600207D RID: 8317 RVA: 0x0005D382 File Offset: 0x0005B582
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

		// Token: 0x17000A1E RID: 2590
		// (get) Token: 0x0600207E RID: 8318 RVA: 0x0005D3A8 File Offset: 0x0005B5A8
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint3 yzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint3(this.y, this.z, this.y);
			}
		}

		// Token: 0x17000A1F RID: 2591
		// (get) Token: 0x0600207F RID: 8319 RVA: 0x0005D3C1 File Offset: 0x0005B5C1
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint3 yzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint3(this.y, this.z, this.z);
			}
		}

		// Token: 0x17000A20 RID: 2592
		// (get) Token: 0x06002080 RID: 8320 RVA: 0x0005D3DA File Offset: 0x0005B5DA
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint3 zxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint3(this.z, this.x, this.x);
			}
		}

		// Token: 0x17000A21 RID: 2593
		// (get) Token: 0x06002081 RID: 8321 RVA: 0x0005D3F3 File Offset: 0x0005B5F3
		// (set) Token: 0x06002082 RID: 8322 RVA: 0x0005D40C File Offset: 0x0005B60C
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

		// Token: 0x17000A22 RID: 2594
		// (get) Token: 0x06002083 RID: 8323 RVA: 0x0005D432 File Offset: 0x0005B632
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint3 zxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint3(this.z, this.x, this.z);
			}
		}

		// Token: 0x17000A23 RID: 2595
		// (get) Token: 0x06002084 RID: 8324 RVA: 0x0005D44B File Offset: 0x0005B64B
		// (set) Token: 0x06002085 RID: 8325 RVA: 0x0005D464 File Offset: 0x0005B664
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

		// Token: 0x17000A24 RID: 2596
		// (get) Token: 0x06002086 RID: 8326 RVA: 0x0005D48A File Offset: 0x0005B68A
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint3 zyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint3(this.z, this.y, this.y);
			}
		}

		// Token: 0x17000A25 RID: 2597
		// (get) Token: 0x06002087 RID: 8327 RVA: 0x0005D4A3 File Offset: 0x0005B6A3
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint3 zyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint3(this.z, this.y, this.z);
			}
		}

		// Token: 0x17000A26 RID: 2598
		// (get) Token: 0x06002088 RID: 8328 RVA: 0x0005D4BC File Offset: 0x0005B6BC
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint3 zzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint3(this.z, this.z, this.x);
			}
		}

		// Token: 0x17000A27 RID: 2599
		// (get) Token: 0x06002089 RID: 8329 RVA: 0x0005D4D5 File Offset: 0x0005B6D5
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint3 zzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint3(this.z, this.z, this.y);
			}
		}

		// Token: 0x17000A28 RID: 2600
		// (get) Token: 0x0600208A RID: 8330 RVA: 0x0005D4EE File Offset: 0x0005B6EE
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint3 zzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint3(this.z, this.z, this.z);
			}
		}

		// Token: 0x17000A29 RID: 2601
		// (get) Token: 0x0600208B RID: 8331 RVA: 0x0005D507 File Offset: 0x0005B707
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint2 xx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint2(this.x, this.x);
			}
		}

		// Token: 0x17000A2A RID: 2602
		// (get) Token: 0x0600208C RID: 8332 RVA: 0x0005D51A File Offset: 0x0005B71A
		// (set) Token: 0x0600208D RID: 8333 RVA: 0x0005D52D File Offset: 0x0005B72D
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

		// Token: 0x17000A2B RID: 2603
		// (get) Token: 0x0600208E RID: 8334 RVA: 0x0005D547 File Offset: 0x0005B747
		// (set) Token: 0x0600208F RID: 8335 RVA: 0x0005D55A File Offset: 0x0005B75A
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

		// Token: 0x17000A2C RID: 2604
		// (get) Token: 0x06002090 RID: 8336 RVA: 0x0005D574 File Offset: 0x0005B774
		// (set) Token: 0x06002091 RID: 8337 RVA: 0x0005D587 File Offset: 0x0005B787
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

		// Token: 0x17000A2D RID: 2605
		// (get) Token: 0x06002092 RID: 8338 RVA: 0x0005D5A1 File Offset: 0x0005B7A1
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint2 yy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint2(this.y, this.y);
			}
		}

		// Token: 0x17000A2E RID: 2606
		// (get) Token: 0x06002093 RID: 8339 RVA: 0x0005D5B4 File Offset: 0x0005B7B4
		// (set) Token: 0x06002094 RID: 8340 RVA: 0x0005D5C7 File Offset: 0x0005B7C7
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

		// Token: 0x17000A2F RID: 2607
		// (get) Token: 0x06002095 RID: 8341 RVA: 0x0005D5E1 File Offset: 0x0005B7E1
		// (set) Token: 0x06002096 RID: 8342 RVA: 0x0005D5F4 File Offset: 0x0005B7F4
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

		// Token: 0x17000A30 RID: 2608
		// (get) Token: 0x06002097 RID: 8343 RVA: 0x0005D60E File Offset: 0x0005B80E
		// (set) Token: 0x06002098 RID: 8344 RVA: 0x0005D621 File Offset: 0x0005B821
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

		// Token: 0x17000A31 RID: 2609
		// (get) Token: 0x06002099 RID: 8345 RVA: 0x0005D63B File Offset: 0x0005B83B
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint2 zz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint2(this.z, this.z);
			}
		}

		// Token: 0x17000A32 RID: 2610
		public unsafe uint this[int index]
		{
			get
			{
				fixed (uint3* ptr = &this)
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

		// Token: 0x0600209C RID: 8348 RVA: 0x0005D688 File Offset: 0x0005B888
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool Equals(uint3 rhs)
		{
			return this.x == rhs.x && this.y == rhs.y && this.z == rhs.z;
		}

		// Token: 0x0600209D RID: 8349 RVA: 0x0005D6B8 File Offset: 0x0005B8B8
		public override bool Equals(object o)
		{
			if (o is uint3)
			{
				uint3 rhs = (uint3)o;
				return this.Equals(rhs);
			}
			return false;
		}

		// Token: 0x0600209E RID: 8350 RVA: 0x0005D6DD File Offset: 0x0005B8DD
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override int GetHashCode()
		{
			return (int)math.hash(this);
		}

		// Token: 0x0600209F RID: 8351 RVA: 0x0005D6EA File Offset: 0x0005B8EA
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override string ToString()
		{
			return string.Format("uint3({0}, {1}, {2})", this.x, this.y, this.z);
		}

		// Token: 0x060020A0 RID: 8352 RVA: 0x0005D717 File Offset: 0x0005B917
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public string ToString(string format, IFormatProvider formatProvider)
		{
			return string.Format("uint3({0}, {1}, {2})", this.x.ToString(format, formatProvider), this.y.ToString(format, formatProvider), this.z.ToString(format, formatProvider));
		}

		// Token: 0x040000F8 RID: 248
		public uint x;

		// Token: 0x040000F9 RID: 249
		public uint y;

		// Token: 0x040000FA RID: 250
		public uint z;

		// Token: 0x040000FB RID: 251
		public static readonly uint3 zero;

		// Token: 0x02000061 RID: 97
		internal sealed class DebuggerProxy
		{
			// Token: 0x06002477 RID: 9335 RVA: 0x00067738 File Offset: 0x00065938
			public DebuggerProxy(uint3 v)
			{
				this.x = v.x;
				this.y = v.y;
				this.z = v.z;
			}

			// Token: 0x04000162 RID: 354
			public uint x;

			// Token: 0x04000163 RID: 355
			public uint y;

			// Token: 0x04000164 RID: 356
			public uint z;
		}
	}
}

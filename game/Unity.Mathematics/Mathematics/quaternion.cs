using System;
using System.Runtime.CompilerServices;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;

namespace Unity.Mathematics
{
	// Token: 0x02000038 RID: 56
	[Il2CppEagerStaticClassConstruction]
	[Serializable]
	public struct quaternion : IEquatable<quaternion>, IFormattable
	{
		// Token: 0x06001DDD RID: 7645 RVA: 0x000512B3 File Offset: 0x0004F4B3
		public static implicit operator Quaternion(quaternion q)
		{
			return new Quaternion(q.value.x, q.value.y, q.value.z, q.value.w);
		}

		// Token: 0x06001DDE RID: 7646 RVA: 0x000512E6 File Offset: 0x0004F4E6
		public static implicit operator quaternion(Quaternion q)
		{
			return new quaternion(q.x, q.y, q.z, q.w);
		}

		// Token: 0x06001DDF RID: 7647 RVA: 0x00051305 File Offset: 0x0004F505
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public quaternion(float x, float y, float z, float w)
		{
			this.value.x = x;
			this.value.y = y;
			this.value.z = z;
			this.value.w = w;
		}

		// Token: 0x06001DE0 RID: 7648 RVA: 0x00051338 File Offset: 0x0004F538
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public quaternion(float4 value)
		{
			this.value = value;
		}

		// Token: 0x06001DE1 RID: 7649 RVA: 0x00051341 File Offset: 0x0004F541
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator quaternion(float4 v)
		{
			return new quaternion(v);
		}

		// Token: 0x06001DE2 RID: 7650 RVA: 0x0005134C File Offset: 0x0004F54C
		public quaternion(float3x3 m)
		{
			float3 c = m.c0;
			float3 c2 = m.c1;
			float3 c3 = m.c2;
			uint num = math.asuint(c.x) & 2147483648U;
			float x = c2.y + math.asfloat(math.asuint(c3.z) ^ num);
			uint4 @uint = math.uint4((int)num >> 31);
			uint4 uint2 = math.uint4(math.asint(x) >> 31);
			float x2 = 1f + math.abs(c.x);
			uint4 rhs = math.uint4(0U, 2147483648U, 2147483648U, 2147483648U) ^ (@uint & math.uint4(0U, 2147483648U, 0U, 2147483648U)) ^ (uint2 & math.uint4(2147483648U, 2147483648U, 2147483648U, 0U));
			this.value = math.float4(x2, c.y, c3.x, c2.z) + math.asfloat(math.asuint(math.float4(x, c2.x, c.z, c3.y)) ^ rhs);
			this.value = math.asfloat((math.asuint(this.value) & ~@uint) | (math.asuint(this.value.zwxy) & @uint));
			this.value = math.asfloat((math.asuint(this.value.wzyx) & ~uint2) | (math.asuint(this.value) & uint2));
			this.value = math.normalize(this.value);
		}

		// Token: 0x06001DE3 RID: 7651 RVA: 0x000514FC File Offset: 0x0004F6FC
		public quaternion(float4x4 m)
		{
			float4 c = m.c0;
			float4 c2 = m.c1;
			float4 c3 = m.c2;
			uint num = math.asuint(c.x) & 2147483648U;
			float x = c2.y + math.asfloat(math.asuint(c3.z) ^ num);
			uint4 @uint = math.uint4((int)num >> 31);
			uint4 uint2 = math.uint4(math.asint(x) >> 31);
			float x2 = 1f + math.abs(c.x);
			uint4 rhs = math.uint4(0U, 2147483648U, 2147483648U, 2147483648U) ^ (@uint & math.uint4(0U, 2147483648U, 0U, 2147483648U)) ^ (uint2 & math.uint4(2147483648U, 2147483648U, 2147483648U, 0U));
			this.value = math.float4(x2, c.y, c3.x, c2.z) + math.asfloat(math.asuint(math.float4(x, c2.x, c.z, c3.y)) ^ rhs);
			this.value = math.asfloat((math.asuint(this.value) & ~@uint) | (math.asuint(this.value.zwxy) & @uint));
			this.value = math.asfloat((math.asuint(this.value.wzyx) & ~uint2) | (math.asuint(this.value) & uint2));
			this.value = math.normalize(this.value);
		}

		// Token: 0x06001DE4 RID: 7652 RVA: 0x000516AC File Offset: 0x0004F8AC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static quaternion AxisAngle(float3 axis, float angle)
		{
			float rhs;
			float w;
			math.sincos(0.5f * angle, out rhs, out w);
			return math.quaternion(math.float4(axis * rhs, w));
		}

		// Token: 0x06001DE5 RID: 7653 RVA: 0x000516DC File Offset: 0x0004F8DC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static quaternion EulerXYZ(float3 xyz)
		{
			float3 @float;
			float3 float2;
			math.sincos(0.5f * xyz, out @float, out float2);
			return math.quaternion(math.float4(@float.xyz, float2.x) * float2.yxxy * float2.zzyz + @float.yxxy * @float.zzyz * math.float4(float2.xyz, @float.x) * math.float4(-1f, 1f, -1f, 1f));
		}

		// Token: 0x06001DE6 RID: 7654 RVA: 0x0005177C File Offset: 0x0004F97C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static quaternion EulerXZY(float3 xyz)
		{
			float3 @float;
			float3 float2;
			math.sincos(0.5f * xyz, out @float, out float2);
			return math.quaternion(math.float4(@float.xyz, float2.x) * float2.yxxy * float2.zzyz + @float.yxxy * @float.zzyz * math.float4(float2.xyz, @float.x) * math.float4(1f, 1f, -1f, -1f));
		}

		// Token: 0x06001DE7 RID: 7655 RVA: 0x0005181C File Offset: 0x0004FA1C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static quaternion EulerYXZ(float3 xyz)
		{
			float3 @float;
			float3 float2;
			math.sincos(0.5f * xyz, out @float, out float2);
			return math.quaternion(math.float4(@float.xyz, float2.x) * float2.yxxy * float2.zzyz + @float.yxxy * @float.zzyz * math.float4(float2.xyz, @float.x) * math.float4(-1f, 1f, 1f, -1f));
		}

		// Token: 0x06001DE8 RID: 7656 RVA: 0x000518BC File Offset: 0x0004FABC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static quaternion EulerYZX(float3 xyz)
		{
			float3 @float;
			float3 float2;
			math.sincos(0.5f * xyz, out @float, out float2);
			return math.quaternion(math.float4(@float.xyz, float2.x) * float2.yxxy * float2.zzyz + @float.yxxy * @float.zzyz * math.float4(float2.xyz, @float.x) * math.float4(-1f, -1f, 1f, 1f));
		}

		// Token: 0x06001DE9 RID: 7657 RVA: 0x0005195C File Offset: 0x0004FB5C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static quaternion EulerZXY(float3 xyz)
		{
			float3 @float;
			float3 float2;
			math.sincos(0.5f * xyz, out @float, out float2);
			return math.quaternion(math.float4(@float.xyz, float2.x) * float2.yxxy * float2.zzyz + @float.yxxy * @float.zzyz * math.float4(float2.xyz, @float.x) * math.float4(1f, -1f, -1f, 1f));
		}

		// Token: 0x06001DEA RID: 7658 RVA: 0x000519FC File Offset: 0x0004FBFC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static quaternion EulerZYX(float3 xyz)
		{
			float3 @float;
			float3 float2;
			math.sincos(0.5f * xyz, out @float, out float2);
			return math.quaternion(math.float4(@float.xyz, float2.x) * float2.yxxy * float2.zzyz + @float.yxxy * @float.zzyz * math.float4(float2.xyz, @float.x) * math.float4(1f, -1f, 1f, -1f));
		}

		// Token: 0x06001DEB RID: 7659 RVA: 0x00051A99 File Offset: 0x0004FC99
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static quaternion EulerXYZ(float x, float y, float z)
		{
			return quaternion.EulerXYZ(math.float3(x, y, z));
		}

		// Token: 0x06001DEC RID: 7660 RVA: 0x00051AA8 File Offset: 0x0004FCA8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static quaternion EulerXZY(float x, float y, float z)
		{
			return quaternion.EulerXZY(math.float3(x, y, z));
		}

		// Token: 0x06001DED RID: 7661 RVA: 0x00051AB7 File Offset: 0x0004FCB7
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static quaternion EulerYXZ(float x, float y, float z)
		{
			return quaternion.EulerYXZ(math.float3(x, y, z));
		}

		// Token: 0x06001DEE RID: 7662 RVA: 0x00051AC6 File Offset: 0x0004FCC6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static quaternion EulerYZX(float x, float y, float z)
		{
			return quaternion.EulerYZX(math.float3(x, y, z));
		}

		// Token: 0x06001DEF RID: 7663 RVA: 0x00051AD5 File Offset: 0x0004FCD5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static quaternion EulerZXY(float x, float y, float z)
		{
			return quaternion.EulerZXY(math.float3(x, y, z));
		}

		// Token: 0x06001DF0 RID: 7664 RVA: 0x00051AE4 File Offset: 0x0004FCE4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static quaternion EulerZYX(float x, float y, float z)
		{
			return quaternion.EulerZYX(math.float3(x, y, z));
		}

		// Token: 0x06001DF1 RID: 7665 RVA: 0x00051AF4 File Offset: 0x0004FCF4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static quaternion Euler(float3 xyz, math.RotationOrder order = math.RotationOrder.ZXY)
		{
			switch (order)
			{
			case math.RotationOrder.XYZ:
				return quaternion.EulerXYZ(xyz);
			case math.RotationOrder.XZY:
				return quaternion.EulerXZY(xyz);
			case math.RotationOrder.YXZ:
				return quaternion.EulerYXZ(xyz);
			case math.RotationOrder.YZX:
				return quaternion.EulerYZX(xyz);
			case math.RotationOrder.ZXY:
				return quaternion.EulerZXY(xyz);
			case math.RotationOrder.ZYX:
				return quaternion.EulerZYX(xyz);
			default:
				return quaternion.identity;
			}
		}

		// Token: 0x06001DF2 RID: 7666 RVA: 0x00051B50 File Offset: 0x0004FD50
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static quaternion Euler(float x, float y, float z, math.RotationOrder order = math.RotationOrder.ZXY)
		{
			return quaternion.Euler(math.float3(x, y, z), order);
		}

		// Token: 0x06001DF3 RID: 7667 RVA: 0x00051B60 File Offset: 0x0004FD60
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static quaternion RotateX(float angle)
		{
			float x;
			float w;
			math.sincos(0.5f * angle, out x, out w);
			return math.quaternion(x, 0f, 0f, w);
		}

		// Token: 0x06001DF4 RID: 7668 RVA: 0x00051B90 File Offset: 0x0004FD90
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static quaternion RotateY(float angle)
		{
			float y;
			float w;
			math.sincos(0.5f * angle, out y, out w);
			return math.quaternion(0f, y, 0f, w);
		}

		// Token: 0x06001DF5 RID: 7669 RVA: 0x00051BC0 File Offset: 0x0004FDC0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static quaternion RotateZ(float angle)
		{
			float z;
			float w;
			math.sincos(0.5f * angle, out z, out w);
			return math.quaternion(0f, 0f, z, w);
		}

		// Token: 0x06001DF6 RID: 7670 RVA: 0x00051BF0 File Offset: 0x0004FDF0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static quaternion LookRotation(float3 forward, float3 up)
		{
			float3 @float = math.normalize(math.cross(up, forward));
			return math.quaternion(math.float3x3(@float, math.cross(forward, @float), forward));
		}

		// Token: 0x06001DF7 RID: 7671 RVA: 0x00051C20 File Offset: 0x0004FE20
		public static quaternion LookRotationSafe(float3 forward, float3 up)
		{
			float x = math.dot(forward, forward);
			float num = math.dot(up, up);
			forward *= math.rsqrt(x);
			up *= math.rsqrt(num);
			float3 @float = math.cross(up, forward);
			float num2 = math.dot(@float, @float);
			@float *= math.rsqrt(num2);
			float num3 = math.min(math.min(x, num), num2);
			float num4 = math.max(math.max(x, num), num2);
			bool c = num3 > 1E-35f && num4 < 1E+35f && math.isfinite(x) && math.isfinite(num) && math.isfinite(num2);
			return math.quaternion(math.select(math.float4(0f, 0f, 0f, 1f), math.quaternion(math.float3x3(@float, math.cross(forward, @float), forward)).value, c));
		}

		// Token: 0x06001DF8 RID: 7672 RVA: 0x00051CFC File Offset: 0x0004FEFC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool Equals(quaternion x)
		{
			return this.value.x == x.value.x && this.value.y == x.value.y && this.value.z == x.value.z && this.value.w == x.value.w;
		}

		// Token: 0x06001DF9 RID: 7673 RVA: 0x00051D6C File Offset: 0x0004FF6C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override bool Equals(object x)
		{
			if (x is quaternion)
			{
				quaternion x2 = (quaternion)x;
				return this.Equals(x2);
			}
			return false;
		}

		// Token: 0x06001DFA RID: 7674 RVA: 0x00051D91 File Offset: 0x0004FF91
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override int GetHashCode()
		{
			return (int)math.hash(this);
		}

		// Token: 0x06001DFB RID: 7675 RVA: 0x00051DA0 File Offset: 0x0004FFA0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override string ToString()
		{
			return string.Format("quaternion({0}f, {1}f, {2}f, {3}f)", new object[]
			{
				this.value.x,
				this.value.y,
				this.value.z,
				this.value.w
			});
		}

		// Token: 0x06001DFC RID: 7676 RVA: 0x00051E0C File Offset: 0x0005000C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public string ToString(string format, IFormatProvider formatProvider)
		{
			return string.Format("quaternion({0}f, {1}f, {2}f, {3}f)", new object[]
			{
				this.value.x.ToString(format, formatProvider),
				this.value.y.ToString(format, formatProvider),
				this.value.z.ToString(format, formatProvider),
				this.value.w.ToString(format, formatProvider)
			});
		}

		// Token: 0x06001DFD RID: 7677 RVA: 0x00051E7D File Offset: 0x0005007D
		// Note: this type is marked as 'beforefieldinit'.
		static quaternion()
		{
		}

		// Token: 0x040000E2 RID: 226
		public float4 value;

		// Token: 0x040000E3 RID: 227
		public static readonly quaternion identity = new quaternion(0f, 0f, 0f, 1f);
	}
}

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;

namespace Unity.Mathematics
{
	// Token: 0x02000024 RID: 36
	[DebuggerTypeProxy(typeof(float4.DebuggerProxy))]
	[Il2CppEagerStaticClassConstruction]
	[Serializable]
	public struct float4 : IEquatable<float4>, IFormattable
	{
		// Token: 0x060012D8 RID: 4824 RVA: 0x00036B69 File Offset: 0x00034D69
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float4(float x, float y, float z, float w)
		{
			this.x = x;
			this.y = y;
			this.z = z;
			this.w = w;
		}

		// Token: 0x060012D9 RID: 4825 RVA: 0x00036B88 File Offset: 0x00034D88
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float4(float x, float y, float2 zw)
		{
			this.x = x;
			this.y = y;
			this.z = zw.x;
			this.w = zw.y;
		}

		// Token: 0x060012DA RID: 4826 RVA: 0x00036BB0 File Offset: 0x00034DB0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float4(float x, float2 yz, float w)
		{
			this.x = x;
			this.y = yz.x;
			this.z = yz.y;
			this.w = w;
		}

		// Token: 0x060012DB RID: 4827 RVA: 0x00036BD8 File Offset: 0x00034DD8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float4(float x, float3 yzw)
		{
			this.x = x;
			this.y = yzw.x;
			this.z = yzw.y;
			this.w = yzw.z;
		}

		// Token: 0x060012DC RID: 4828 RVA: 0x00036C05 File Offset: 0x00034E05
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float4(float2 xy, float z, float w)
		{
			this.x = xy.x;
			this.y = xy.y;
			this.z = z;
			this.w = w;
		}

		// Token: 0x060012DD RID: 4829 RVA: 0x00036C2D File Offset: 0x00034E2D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float4(float2 xy, float2 zw)
		{
			this.x = xy.x;
			this.y = xy.y;
			this.z = zw.x;
			this.w = zw.y;
		}

		// Token: 0x060012DE RID: 4830 RVA: 0x00036C5F File Offset: 0x00034E5F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float4(float3 xyz, float w)
		{
			this.x = xyz.x;
			this.y = xyz.y;
			this.z = xyz.z;
			this.w = w;
		}

		// Token: 0x060012DF RID: 4831 RVA: 0x00036C8C File Offset: 0x00034E8C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float4(float4 xyzw)
		{
			this.x = xyzw.x;
			this.y = xyzw.y;
			this.z = xyzw.z;
			this.w = xyzw.w;
		}

		// Token: 0x060012E0 RID: 4832 RVA: 0x00036CBE File Offset: 0x00034EBE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float4(float v)
		{
			this.x = v;
			this.y = v;
			this.z = v;
			this.w = v;
		}

		// Token: 0x060012E1 RID: 4833 RVA: 0x00036CDC File Offset: 0x00034EDC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float4(bool v)
		{
			this.x = (v ? 1f : 0f);
			this.y = (v ? 1f : 0f);
			this.z = (v ? 1f : 0f);
			this.w = (v ? 1f : 0f);
		}

		// Token: 0x060012E2 RID: 4834 RVA: 0x00036D40 File Offset: 0x00034F40
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float4(bool4 v)
		{
			this.x = (v.x ? 1f : 0f);
			this.y = (v.y ? 1f : 0f);
			this.z = (v.z ? 1f : 0f);
			this.w = (v.w ? 1f : 0f);
		}

		// Token: 0x060012E3 RID: 4835 RVA: 0x00036DB5 File Offset: 0x00034FB5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float4(int v)
		{
			this.x = (float)v;
			this.y = (float)v;
			this.z = (float)v;
			this.w = (float)v;
		}

		// Token: 0x060012E4 RID: 4836 RVA: 0x00036DD7 File Offset: 0x00034FD7
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float4(int4 v)
		{
			this.x = (float)v.x;
			this.y = (float)v.y;
			this.z = (float)v.z;
			this.w = (float)v.w;
		}

		// Token: 0x060012E5 RID: 4837 RVA: 0x00036E0D File Offset: 0x0003500D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float4(uint v)
		{
			this.x = v;
			this.y = v;
			this.z = v;
			this.w = v;
		}

		// Token: 0x060012E6 RID: 4838 RVA: 0x00036E33 File Offset: 0x00035033
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float4(uint4 v)
		{
			this.x = v.x;
			this.y = v.y;
			this.z = v.z;
			this.w = v.w;
		}

		// Token: 0x060012E7 RID: 4839 RVA: 0x00036E6D File Offset: 0x0003506D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float4(half v)
		{
			this.x = v;
			this.y = v;
			this.z = v;
			this.w = v;
		}

		// Token: 0x060012E8 RID: 4840 RVA: 0x00036EA0 File Offset: 0x000350A0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float4(half4 v)
		{
			this.x = v.x;
			this.y = v.y;
			this.z = v.z;
			this.w = v.w;
		}

		// Token: 0x060012E9 RID: 4841 RVA: 0x00036EF1 File Offset: 0x000350F1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float4(double v)
		{
			this.x = (float)v;
			this.y = (float)v;
			this.z = (float)v;
			this.w = (float)v;
		}

		// Token: 0x060012EA RID: 4842 RVA: 0x00036F13 File Offset: 0x00035113
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float4(double4 v)
		{
			this.x = (float)v.x;
			this.y = (float)v.y;
			this.z = (float)v.z;
			this.w = (float)v.w;
		}

		// Token: 0x060012EB RID: 4843 RVA: 0x00036F49 File Offset: 0x00035149
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator float4(float v)
		{
			return new float4(v);
		}

		// Token: 0x060012EC RID: 4844 RVA: 0x00036F51 File Offset: 0x00035151
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator float4(bool v)
		{
			return new float4(v);
		}

		// Token: 0x060012ED RID: 4845 RVA: 0x00036F59 File Offset: 0x00035159
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator float4(bool4 v)
		{
			return new float4(v);
		}

		// Token: 0x060012EE RID: 4846 RVA: 0x00036F61 File Offset: 0x00035161
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator float4(int v)
		{
			return new float4(v);
		}

		// Token: 0x060012EF RID: 4847 RVA: 0x00036F69 File Offset: 0x00035169
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator float4(int4 v)
		{
			return new float4(v);
		}

		// Token: 0x060012F0 RID: 4848 RVA: 0x00036F71 File Offset: 0x00035171
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator float4(uint v)
		{
			return new float4(v);
		}

		// Token: 0x060012F1 RID: 4849 RVA: 0x00036F79 File Offset: 0x00035179
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator float4(uint4 v)
		{
			return new float4(v);
		}

		// Token: 0x060012F2 RID: 4850 RVA: 0x00036F81 File Offset: 0x00035181
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator float4(half v)
		{
			return new float4(v);
		}

		// Token: 0x060012F3 RID: 4851 RVA: 0x00036F89 File Offset: 0x00035189
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator float4(half4 v)
		{
			return new float4(v);
		}

		// Token: 0x060012F4 RID: 4852 RVA: 0x00036F91 File Offset: 0x00035191
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator float4(double v)
		{
			return new float4(v);
		}

		// Token: 0x060012F5 RID: 4853 RVA: 0x00036F99 File Offset: 0x00035199
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator float4(double4 v)
		{
			return new float4(v);
		}

		// Token: 0x060012F6 RID: 4854 RVA: 0x00036FA1 File Offset: 0x000351A1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4 operator *(float4 lhs, float4 rhs)
		{
			return new float4(lhs.x * rhs.x, lhs.y * rhs.y, lhs.z * rhs.z, lhs.w * rhs.w);
		}

		// Token: 0x060012F7 RID: 4855 RVA: 0x00036FDC File Offset: 0x000351DC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4 operator *(float4 lhs, float rhs)
		{
			return new float4(lhs.x * rhs, lhs.y * rhs, lhs.z * rhs, lhs.w * rhs);
		}

		// Token: 0x060012F8 RID: 4856 RVA: 0x00037003 File Offset: 0x00035203
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4 operator *(float lhs, float4 rhs)
		{
			return new float4(lhs * rhs.x, lhs * rhs.y, lhs * rhs.z, lhs * rhs.w);
		}

		// Token: 0x060012F9 RID: 4857 RVA: 0x0003702A File Offset: 0x0003522A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4 operator +(float4 lhs, float4 rhs)
		{
			return new float4(lhs.x + rhs.x, lhs.y + rhs.y, lhs.z + rhs.z, lhs.w + rhs.w);
		}

		// Token: 0x060012FA RID: 4858 RVA: 0x00037065 File Offset: 0x00035265
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4 operator +(float4 lhs, float rhs)
		{
			return new float4(lhs.x + rhs, lhs.y + rhs, lhs.z + rhs, lhs.w + rhs);
		}

		// Token: 0x060012FB RID: 4859 RVA: 0x0003708C File Offset: 0x0003528C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4 operator +(float lhs, float4 rhs)
		{
			return new float4(lhs + rhs.x, lhs + rhs.y, lhs + rhs.z, lhs + rhs.w);
		}

		// Token: 0x060012FC RID: 4860 RVA: 0x000370B3 File Offset: 0x000352B3
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4 operator -(float4 lhs, float4 rhs)
		{
			return new float4(lhs.x - rhs.x, lhs.y - rhs.y, lhs.z - rhs.z, lhs.w - rhs.w);
		}

		// Token: 0x060012FD RID: 4861 RVA: 0x000370EE File Offset: 0x000352EE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4 operator -(float4 lhs, float rhs)
		{
			return new float4(lhs.x - rhs, lhs.y - rhs, lhs.z - rhs, lhs.w - rhs);
		}

		// Token: 0x060012FE RID: 4862 RVA: 0x00037115 File Offset: 0x00035315
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4 operator -(float lhs, float4 rhs)
		{
			return new float4(lhs - rhs.x, lhs - rhs.y, lhs - rhs.z, lhs - rhs.w);
		}

		// Token: 0x060012FF RID: 4863 RVA: 0x0003713C File Offset: 0x0003533C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4 operator /(float4 lhs, float4 rhs)
		{
			return new float4(lhs.x / rhs.x, lhs.y / rhs.y, lhs.z / rhs.z, lhs.w / rhs.w);
		}

		// Token: 0x06001300 RID: 4864 RVA: 0x00037177 File Offset: 0x00035377
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4 operator /(float4 lhs, float rhs)
		{
			return new float4(lhs.x / rhs, lhs.y / rhs, lhs.z / rhs, lhs.w / rhs);
		}

		// Token: 0x06001301 RID: 4865 RVA: 0x0003719E File Offset: 0x0003539E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4 operator /(float lhs, float4 rhs)
		{
			return new float4(lhs / rhs.x, lhs / rhs.y, lhs / rhs.z, lhs / rhs.w);
		}

		// Token: 0x06001302 RID: 4866 RVA: 0x000371C5 File Offset: 0x000353C5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4 operator %(float4 lhs, float4 rhs)
		{
			return new float4(lhs.x % rhs.x, lhs.y % rhs.y, lhs.z % rhs.z, lhs.w % rhs.w);
		}

		// Token: 0x06001303 RID: 4867 RVA: 0x00037200 File Offset: 0x00035400
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4 operator %(float4 lhs, float rhs)
		{
			return new float4(lhs.x % rhs, lhs.y % rhs, lhs.z % rhs, lhs.w % rhs);
		}

		// Token: 0x06001304 RID: 4868 RVA: 0x00037227 File Offset: 0x00035427
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4 operator %(float lhs, float4 rhs)
		{
			return new float4(lhs % rhs.x, lhs % rhs.y, lhs % rhs.z, lhs % rhs.w);
		}

		// Token: 0x06001305 RID: 4869 RVA: 0x00037250 File Offset: 0x00035450
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4 operator ++(float4 val)
		{
			float num = val.x + 1f;
			val.x = num;
			float num2 = num;
			num = val.y + 1f;
			val.y = num;
			float num3 = num;
			num = val.z + 1f;
			val.z = num;
			float num4 = num;
			num = val.w + 1f;
			val.w = num;
			return new float4(num2, num3, num4, num);
		}

		// Token: 0x06001306 RID: 4870 RVA: 0x000372B0 File Offset: 0x000354B0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4 operator --(float4 val)
		{
			float num = val.x - 1f;
			val.x = num;
			float num2 = num;
			num = val.y - 1f;
			val.y = num;
			float num3 = num;
			num = val.z - 1f;
			val.z = num;
			float num4 = num;
			num = val.w - 1f;
			val.w = num;
			return new float4(num2, num3, num4, num);
		}

		// Token: 0x06001307 RID: 4871 RVA: 0x0003730E File Offset: 0x0003550E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4 operator <(float4 lhs, float4 rhs)
		{
			return new bool4(lhs.x < rhs.x, lhs.y < rhs.y, lhs.z < rhs.z, lhs.w < rhs.w);
		}

		// Token: 0x06001308 RID: 4872 RVA: 0x0003734D File Offset: 0x0003554D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4 operator <(float4 lhs, float rhs)
		{
			return new bool4(lhs.x < rhs, lhs.y < rhs, lhs.z < rhs, lhs.w < rhs);
		}

		// Token: 0x06001309 RID: 4873 RVA: 0x00037378 File Offset: 0x00035578
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4 operator <(float lhs, float4 rhs)
		{
			return new bool4(lhs < rhs.x, lhs < rhs.y, lhs < rhs.z, lhs < rhs.w);
		}

		// Token: 0x0600130A RID: 4874 RVA: 0x000373A4 File Offset: 0x000355A4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4 operator <=(float4 lhs, float4 rhs)
		{
			return new bool4(lhs.x <= rhs.x, lhs.y <= rhs.y, lhs.z <= rhs.z, lhs.w <= rhs.w);
		}

		// Token: 0x0600130B RID: 4875 RVA: 0x000373FA File Offset: 0x000355FA
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4 operator <=(float4 lhs, float rhs)
		{
			return new bool4(lhs.x <= rhs, lhs.y <= rhs, lhs.z <= rhs, lhs.w <= rhs);
		}

		// Token: 0x0600130C RID: 4876 RVA: 0x00037431 File Offset: 0x00035631
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4 operator <=(float lhs, float4 rhs)
		{
			return new bool4(lhs <= rhs.x, lhs <= rhs.y, lhs <= rhs.z, lhs <= rhs.w);
		}

		// Token: 0x0600130D RID: 4877 RVA: 0x00037468 File Offset: 0x00035668
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4 operator >(float4 lhs, float4 rhs)
		{
			return new bool4(lhs.x > rhs.x, lhs.y > rhs.y, lhs.z > rhs.z, lhs.w > rhs.w);
		}

		// Token: 0x0600130E RID: 4878 RVA: 0x000374A7 File Offset: 0x000356A7
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4 operator >(float4 lhs, float rhs)
		{
			return new bool4(lhs.x > rhs, lhs.y > rhs, lhs.z > rhs, lhs.w > rhs);
		}

		// Token: 0x0600130F RID: 4879 RVA: 0x000374D2 File Offset: 0x000356D2
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4 operator >(float lhs, float4 rhs)
		{
			return new bool4(lhs > rhs.x, lhs > rhs.y, lhs > rhs.z, lhs > rhs.w);
		}

		// Token: 0x06001310 RID: 4880 RVA: 0x00037500 File Offset: 0x00035700
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4 operator >=(float4 lhs, float4 rhs)
		{
			return new bool4(lhs.x >= rhs.x, lhs.y >= rhs.y, lhs.z >= rhs.z, lhs.w >= rhs.w);
		}

		// Token: 0x06001311 RID: 4881 RVA: 0x00037556 File Offset: 0x00035756
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4 operator >=(float4 lhs, float rhs)
		{
			return new bool4(lhs.x >= rhs, lhs.y >= rhs, lhs.z >= rhs, lhs.w >= rhs);
		}

		// Token: 0x06001312 RID: 4882 RVA: 0x0003758D File Offset: 0x0003578D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4 operator >=(float lhs, float4 rhs)
		{
			return new bool4(lhs >= rhs.x, lhs >= rhs.y, lhs >= rhs.z, lhs >= rhs.w);
		}

		// Token: 0x06001313 RID: 4883 RVA: 0x000375C4 File Offset: 0x000357C4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4 operator -(float4 val)
		{
			return new float4(-val.x, -val.y, -val.z, -val.w);
		}

		// Token: 0x06001314 RID: 4884 RVA: 0x000375E7 File Offset: 0x000357E7
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4 operator +(float4 val)
		{
			return new float4(val.x, val.y, val.z, val.w);
		}

		// Token: 0x06001315 RID: 4885 RVA: 0x00037606 File Offset: 0x00035806
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4 operator ==(float4 lhs, float4 rhs)
		{
			return new bool4(lhs.x == rhs.x, lhs.y == rhs.y, lhs.z == rhs.z, lhs.w == rhs.w);
		}

		// Token: 0x06001316 RID: 4886 RVA: 0x00037645 File Offset: 0x00035845
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4 operator ==(float4 lhs, float rhs)
		{
			return new bool4(lhs.x == rhs, lhs.y == rhs, lhs.z == rhs, lhs.w == rhs);
		}

		// Token: 0x06001317 RID: 4887 RVA: 0x00037670 File Offset: 0x00035870
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4 operator ==(float lhs, float4 rhs)
		{
			return new bool4(lhs == rhs.x, lhs == rhs.y, lhs == rhs.z, lhs == rhs.w);
		}

		// Token: 0x06001318 RID: 4888 RVA: 0x0003769C File Offset: 0x0003589C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4 operator !=(float4 lhs, float4 rhs)
		{
			return new bool4(lhs.x != rhs.x, lhs.y != rhs.y, lhs.z != rhs.z, lhs.w != rhs.w);
		}

		// Token: 0x06001319 RID: 4889 RVA: 0x000376F2 File Offset: 0x000358F2
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4 operator !=(float4 lhs, float rhs)
		{
			return new bool4(lhs.x != rhs, lhs.y != rhs, lhs.z != rhs, lhs.w != rhs);
		}

		// Token: 0x0600131A RID: 4890 RVA: 0x00037729 File Offset: 0x00035929
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4 operator !=(float lhs, float4 rhs)
		{
			return new bool4(lhs != rhs.x, lhs != rhs.y, lhs != rhs.z, lhs != rhs.w);
		}

		// Token: 0x17000474 RID: 1140
		// (get) Token: 0x0600131B RID: 4891 RVA: 0x00037760 File Offset: 0x00035960
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 xxxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.x, this.x, this.x, this.x);
			}
		}

		// Token: 0x17000475 RID: 1141
		// (get) Token: 0x0600131C RID: 4892 RVA: 0x0003777F File Offset: 0x0003597F
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 xxxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.x, this.x, this.x, this.y);
			}
		}

		// Token: 0x17000476 RID: 1142
		// (get) Token: 0x0600131D RID: 4893 RVA: 0x0003779E File Offset: 0x0003599E
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 xxxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.x, this.x, this.x, this.z);
			}
		}

		// Token: 0x17000477 RID: 1143
		// (get) Token: 0x0600131E RID: 4894 RVA: 0x000377BD File Offset: 0x000359BD
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 xxxw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.x, this.x, this.x, this.w);
			}
		}

		// Token: 0x17000478 RID: 1144
		// (get) Token: 0x0600131F RID: 4895 RVA: 0x000377DC File Offset: 0x000359DC
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 xxyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.x, this.x, this.y, this.x);
			}
		}

		// Token: 0x17000479 RID: 1145
		// (get) Token: 0x06001320 RID: 4896 RVA: 0x000377FB File Offset: 0x000359FB
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 xxyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.x, this.x, this.y, this.y);
			}
		}

		// Token: 0x1700047A RID: 1146
		// (get) Token: 0x06001321 RID: 4897 RVA: 0x0003781A File Offset: 0x00035A1A
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 xxyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.x, this.x, this.y, this.z);
			}
		}

		// Token: 0x1700047B RID: 1147
		// (get) Token: 0x06001322 RID: 4898 RVA: 0x00037839 File Offset: 0x00035A39
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 xxyw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.x, this.x, this.y, this.w);
			}
		}

		// Token: 0x1700047C RID: 1148
		// (get) Token: 0x06001323 RID: 4899 RVA: 0x00037858 File Offset: 0x00035A58
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 xxzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.x, this.x, this.z, this.x);
			}
		}

		// Token: 0x1700047D RID: 1149
		// (get) Token: 0x06001324 RID: 4900 RVA: 0x00037877 File Offset: 0x00035A77
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 xxzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.x, this.x, this.z, this.y);
			}
		}

		// Token: 0x1700047E RID: 1150
		// (get) Token: 0x06001325 RID: 4901 RVA: 0x00037896 File Offset: 0x00035A96
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 xxzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.x, this.x, this.z, this.z);
			}
		}

		// Token: 0x1700047F RID: 1151
		// (get) Token: 0x06001326 RID: 4902 RVA: 0x000378B5 File Offset: 0x00035AB5
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 xxzw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.x, this.x, this.z, this.w);
			}
		}

		// Token: 0x17000480 RID: 1152
		// (get) Token: 0x06001327 RID: 4903 RVA: 0x000378D4 File Offset: 0x00035AD4
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 xxwx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.x, this.x, this.w, this.x);
			}
		}

		// Token: 0x17000481 RID: 1153
		// (get) Token: 0x06001328 RID: 4904 RVA: 0x000378F3 File Offset: 0x00035AF3
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 xxwy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.x, this.x, this.w, this.y);
			}
		}

		// Token: 0x17000482 RID: 1154
		// (get) Token: 0x06001329 RID: 4905 RVA: 0x00037912 File Offset: 0x00035B12
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 xxwz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.x, this.x, this.w, this.z);
			}
		}

		// Token: 0x17000483 RID: 1155
		// (get) Token: 0x0600132A RID: 4906 RVA: 0x00037931 File Offset: 0x00035B31
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 xxww
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.x, this.x, this.w, this.w);
			}
		}

		// Token: 0x17000484 RID: 1156
		// (get) Token: 0x0600132B RID: 4907 RVA: 0x00037950 File Offset: 0x00035B50
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 xyxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.x, this.y, this.x, this.x);
			}
		}

		// Token: 0x17000485 RID: 1157
		// (get) Token: 0x0600132C RID: 4908 RVA: 0x0003796F File Offset: 0x00035B6F
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 xyxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.x, this.y, this.x, this.y);
			}
		}

		// Token: 0x17000486 RID: 1158
		// (get) Token: 0x0600132D RID: 4909 RVA: 0x0003798E File Offset: 0x00035B8E
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 xyxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.x, this.y, this.x, this.z);
			}
		}

		// Token: 0x17000487 RID: 1159
		// (get) Token: 0x0600132E RID: 4910 RVA: 0x000379AD File Offset: 0x00035BAD
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 xyxw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.x, this.y, this.x, this.w);
			}
		}

		// Token: 0x17000488 RID: 1160
		// (get) Token: 0x0600132F RID: 4911 RVA: 0x000379CC File Offset: 0x00035BCC
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 xyyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.x, this.y, this.y, this.x);
			}
		}

		// Token: 0x17000489 RID: 1161
		// (get) Token: 0x06001330 RID: 4912 RVA: 0x000379EB File Offset: 0x00035BEB
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 xyyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.x, this.y, this.y, this.y);
			}
		}

		// Token: 0x1700048A RID: 1162
		// (get) Token: 0x06001331 RID: 4913 RVA: 0x00037A0A File Offset: 0x00035C0A
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 xyyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.x, this.y, this.y, this.z);
			}
		}

		// Token: 0x1700048B RID: 1163
		// (get) Token: 0x06001332 RID: 4914 RVA: 0x00037A29 File Offset: 0x00035C29
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 xyyw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.x, this.y, this.y, this.w);
			}
		}

		// Token: 0x1700048C RID: 1164
		// (get) Token: 0x06001333 RID: 4915 RVA: 0x00037A48 File Offset: 0x00035C48
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 xyzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.x, this.y, this.z, this.x);
			}
		}

		// Token: 0x1700048D RID: 1165
		// (get) Token: 0x06001334 RID: 4916 RVA: 0x00037A67 File Offset: 0x00035C67
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 xyzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.x, this.y, this.z, this.y);
			}
		}

		// Token: 0x1700048E RID: 1166
		// (get) Token: 0x06001335 RID: 4917 RVA: 0x00037A86 File Offset: 0x00035C86
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 xyzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.x, this.y, this.z, this.z);
			}
		}

		// Token: 0x1700048F RID: 1167
		// (get) Token: 0x06001336 RID: 4918 RVA: 0x00037AA5 File Offset: 0x00035CA5
		// (set) Token: 0x06001337 RID: 4919 RVA: 0x00037AC4 File Offset: 0x00035CC4
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 xyzw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.x, this.y, this.z, this.w);
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

		// Token: 0x17000490 RID: 1168
		// (get) Token: 0x06001338 RID: 4920 RVA: 0x00037AF6 File Offset: 0x00035CF6
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 xywx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.x, this.y, this.w, this.x);
			}
		}

		// Token: 0x17000491 RID: 1169
		// (get) Token: 0x06001339 RID: 4921 RVA: 0x00037B15 File Offset: 0x00035D15
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 xywy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.x, this.y, this.w, this.y);
			}
		}

		// Token: 0x17000492 RID: 1170
		// (get) Token: 0x0600133A RID: 4922 RVA: 0x00037B34 File Offset: 0x00035D34
		// (set) Token: 0x0600133B RID: 4923 RVA: 0x00037B53 File Offset: 0x00035D53
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 xywz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.x, this.y, this.w, this.z);
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

		// Token: 0x17000493 RID: 1171
		// (get) Token: 0x0600133C RID: 4924 RVA: 0x00037B85 File Offset: 0x00035D85
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 xyww
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.x, this.y, this.w, this.w);
			}
		}

		// Token: 0x17000494 RID: 1172
		// (get) Token: 0x0600133D RID: 4925 RVA: 0x00037BA4 File Offset: 0x00035DA4
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 xzxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.x, this.z, this.x, this.x);
			}
		}

		// Token: 0x17000495 RID: 1173
		// (get) Token: 0x0600133E RID: 4926 RVA: 0x00037BC3 File Offset: 0x00035DC3
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 xzxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.x, this.z, this.x, this.y);
			}
		}

		// Token: 0x17000496 RID: 1174
		// (get) Token: 0x0600133F RID: 4927 RVA: 0x00037BE2 File Offset: 0x00035DE2
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 xzxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.x, this.z, this.x, this.z);
			}
		}

		// Token: 0x17000497 RID: 1175
		// (get) Token: 0x06001340 RID: 4928 RVA: 0x00037C01 File Offset: 0x00035E01
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 xzxw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.x, this.z, this.x, this.w);
			}
		}

		// Token: 0x17000498 RID: 1176
		// (get) Token: 0x06001341 RID: 4929 RVA: 0x00037C20 File Offset: 0x00035E20
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 xzyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.x, this.z, this.y, this.x);
			}
		}

		// Token: 0x17000499 RID: 1177
		// (get) Token: 0x06001342 RID: 4930 RVA: 0x00037C3F File Offset: 0x00035E3F
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 xzyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.x, this.z, this.y, this.y);
			}
		}

		// Token: 0x1700049A RID: 1178
		// (get) Token: 0x06001343 RID: 4931 RVA: 0x00037C5E File Offset: 0x00035E5E
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 xzyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.x, this.z, this.y, this.z);
			}
		}

		// Token: 0x1700049B RID: 1179
		// (get) Token: 0x06001344 RID: 4932 RVA: 0x00037C7D File Offset: 0x00035E7D
		// (set) Token: 0x06001345 RID: 4933 RVA: 0x00037C9C File Offset: 0x00035E9C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 xzyw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.x, this.z, this.y, this.w);
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

		// Token: 0x1700049C RID: 1180
		// (get) Token: 0x06001346 RID: 4934 RVA: 0x00037CCE File Offset: 0x00035ECE
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 xzzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.x, this.z, this.z, this.x);
			}
		}

		// Token: 0x1700049D RID: 1181
		// (get) Token: 0x06001347 RID: 4935 RVA: 0x00037CED File Offset: 0x00035EED
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 xzzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.x, this.z, this.z, this.y);
			}
		}

		// Token: 0x1700049E RID: 1182
		// (get) Token: 0x06001348 RID: 4936 RVA: 0x00037D0C File Offset: 0x00035F0C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 xzzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.x, this.z, this.z, this.z);
			}
		}

		// Token: 0x1700049F RID: 1183
		// (get) Token: 0x06001349 RID: 4937 RVA: 0x00037D2B File Offset: 0x00035F2B
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 xzzw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.x, this.z, this.z, this.w);
			}
		}

		// Token: 0x170004A0 RID: 1184
		// (get) Token: 0x0600134A RID: 4938 RVA: 0x00037D4A File Offset: 0x00035F4A
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 xzwx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.x, this.z, this.w, this.x);
			}
		}

		// Token: 0x170004A1 RID: 1185
		// (get) Token: 0x0600134B RID: 4939 RVA: 0x00037D69 File Offset: 0x00035F69
		// (set) Token: 0x0600134C RID: 4940 RVA: 0x00037D88 File Offset: 0x00035F88
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 xzwy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.x, this.z, this.w, this.y);
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

		// Token: 0x170004A2 RID: 1186
		// (get) Token: 0x0600134D RID: 4941 RVA: 0x00037DBA File Offset: 0x00035FBA
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 xzwz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.x, this.z, this.w, this.z);
			}
		}

		// Token: 0x170004A3 RID: 1187
		// (get) Token: 0x0600134E RID: 4942 RVA: 0x00037DD9 File Offset: 0x00035FD9
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 xzww
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.x, this.z, this.w, this.w);
			}
		}

		// Token: 0x170004A4 RID: 1188
		// (get) Token: 0x0600134F RID: 4943 RVA: 0x00037DF8 File Offset: 0x00035FF8
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 xwxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.x, this.w, this.x, this.x);
			}
		}

		// Token: 0x170004A5 RID: 1189
		// (get) Token: 0x06001350 RID: 4944 RVA: 0x00037E17 File Offset: 0x00036017
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 xwxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.x, this.w, this.x, this.y);
			}
		}

		// Token: 0x170004A6 RID: 1190
		// (get) Token: 0x06001351 RID: 4945 RVA: 0x00037E36 File Offset: 0x00036036
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 xwxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.x, this.w, this.x, this.z);
			}
		}

		// Token: 0x170004A7 RID: 1191
		// (get) Token: 0x06001352 RID: 4946 RVA: 0x00037E55 File Offset: 0x00036055
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 xwxw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.x, this.w, this.x, this.w);
			}
		}

		// Token: 0x170004A8 RID: 1192
		// (get) Token: 0x06001353 RID: 4947 RVA: 0x00037E74 File Offset: 0x00036074
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 xwyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.x, this.w, this.y, this.x);
			}
		}

		// Token: 0x170004A9 RID: 1193
		// (get) Token: 0x06001354 RID: 4948 RVA: 0x00037E93 File Offset: 0x00036093
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 xwyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.x, this.w, this.y, this.y);
			}
		}

		// Token: 0x170004AA RID: 1194
		// (get) Token: 0x06001355 RID: 4949 RVA: 0x00037EB2 File Offset: 0x000360B2
		// (set) Token: 0x06001356 RID: 4950 RVA: 0x00037ED1 File Offset: 0x000360D1
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 xwyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.x, this.w, this.y, this.z);
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

		// Token: 0x170004AB RID: 1195
		// (get) Token: 0x06001357 RID: 4951 RVA: 0x00037F03 File Offset: 0x00036103
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 xwyw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.x, this.w, this.y, this.w);
			}
		}

		// Token: 0x170004AC RID: 1196
		// (get) Token: 0x06001358 RID: 4952 RVA: 0x00037F22 File Offset: 0x00036122
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 xwzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.x, this.w, this.z, this.x);
			}
		}

		// Token: 0x170004AD RID: 1197
		// (get) Token: 0x06001359 RID: 4953 RVA: 0x00037F41 File Offset: 0x00036141
		// (set) Token: 0x0600135A RID: 4954 RVA: 0x00037F60 File Offset: 0x00036160
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 xwzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.x, this.w, this.z, this.y);
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

		// Token: 0x170004AE RID: 1198
		// (get) Token: 0x0600135B RID: 4955 RVA: 0x00037F92 File Offset: 0x00036192
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 xwzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.x, this.w, this.z, this.z);
			}
		}

		// Token: 0x170004AF RID: 1199
		// (get) Token: 0x0600135C RID: 4956 RVA: 0x00037FB1 File Offset: 0x000361B1
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 xwzw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.x, this.w, this.z, this.w);
			}
		}

		// Token: 0x170004B0 RID: 1200
		// (get) Token: 0x0600135D RID: 4957 RVA: 0x00037FD0 File Offset: 0x000361D0
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 xwwx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.x, this.w, this.w, this.x);
			}
		}

		// Token: 0x170004B1 RID: 1201
		// (get) Token: 0x0600135E RID: 4958 RVA: 0x00037FEF File Offset: 0x000361EF
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 xwwy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.x, this.w, this.w, this.y);
			}
		}

		// Token: 0x170004B2 RID: 1202
		// (get) Token: 0x0600135F RID: 4959 RVA: 0x0003800E File Offset: 0x0003620E
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 xwwz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.x, this.w, this.w, this.z);
			}
		}

		// Token: 0x170004B3 RID: 1203
		// (get) Token: 0x06001360 RID: 4960 RVA: 0x0003802D File Offset: 0x0003622D
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 xwww
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.x, this.w, this.w, this.w);
			}
		}

		// Token: 0x170004B4 RID: 1204
		// (get) Token: 0x06001361 RID: 4961 RVA: 0x0003804C File Offset: 0x0003624C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 yxxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.y, this.x, this.x, this.x);
			}
		}

		// Token: 0x170004B5 RID: 1205
		// (get) Token: 0x06001362 RID: 4962 RVA: 0x0003806B File Offset: 0x0003626B
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 yxxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.y, this.x, this.x, this.y);
			}
		}

		// Token: 0x170004B6 RID: 1206
		// (get) Token: 0x06001363 RID: 4963 RVA: 0x0003808A File Offset: 0x0003628A
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 yxxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.y, this.x, this.x, this.z);
			}
		}

		// Token: 0x170004B7 RID: 1207
		// (get) Token: 0x06001364 RID: 4964 RVA: 0x000380A9 File Offset: 0x000362A9
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 yxxw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.y, this.x, this.x, this.w);
			}
		}

		// Token: 0x170004B8 RID: 1208
		// (get) Token: 0x06001365 RID: 4965 RVA: 0x000380C8 File Offset: 0x000362C8
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 yxyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.y, this.x, this.y, this.x);
			}
		}

		// Token: 0x170004B9 RID: 1209
		// (get) Token: 0x06001366 RID: 4966 RVA: 0x000380E7 File Offset: 0x000362E7
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 yxyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.y, this.x, this.y, this.y);
			}
		}

		// Token: 0x170004BA RID: 1210
		// (get) Token: 0x06001367 RID: 4967 RVA: 0x00038106 File Offset: 0x00036306
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 yxyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.y, this.x, this.y, this.z);
			}
		}

		// Token: 0x170004BB RID: 1211
		// (get) Token: 0x06001368 RID: 4968 RVA: 0x00038125 File Offset: 0x00036325
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 yxyw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.y, this.x, this.y, this.w);
			}
		}

		// Token: 0x170004BC RID: 1212
		// (get) Token: 0x06001369 RID: 4969 RVA: 0x00038144 File Offset: 0x00036344
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 yxzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.y, this.x, this.z, this.x);
			}
		}

		// Token: 0x170004BD RID: 1213
		// (get) Token: 0x0600136A RID: 4970 RVA: 0x00038163 File Offset: 0x00036363
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 yxzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.y, this.x, this.z, this.y);
			}
		}

		// Token: 0x170004BE RID: 1214
		// (get) Token: 0x0600136B RID: 4971 RVA: 0x00038182 File Offset: 0x00036382
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 yxzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.y, this.x, this.z, this.z);
			}
		}

		// Token: 0x170004BF RID: 1215
		// (get) Token: 0x0600136C RID: 4972 RVA: 0x000381A1 File Offset: 0x000363A1
		// (set) Token: 0x0600136D RID: 4973 RVA: 0x000381C0 File Offset: 0x000363C0
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 yxzw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.y, this.x, this.z, this.w);
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

		// Token: 0x170004C0 RID: 1216
		// (get) Token: 0x0600136E RID: 4974 RVA: 0x000381F2 File Offset: 0x000363F2
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 yxwx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.y, this.x, this.w, this.x);
			}
		}

		// Token: 0x170004C1 RID: 1217
		// (get) Token: 0x0600136F RID: 4975 RVA: 0x00038211 File Offset: 0x00036411
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 yxwy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.y, this.x, this.w, this.y);
			}
		}

		// Token: 0x170004C2 RID: 1218
		// (get) Token: 0x06001370 RID: 4976 RVA: 0x00038230 File Offset: 0x00036430
		// (set) Token: 0x06001371 RID: 4977 RVA: 0x0003824F File Offset: 0x0003644F
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 yxwz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.y, this.x, this.w, this.z);
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

		// Token: 0x170004C3 RID: 1219
		// (get) Token: 0x06001372 RID: 4978 RVA: 0x00038281 File Offset: 0x00036481
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 yxww
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.y, this.x, this.w, this.w);
			}
		}

		// Token: 0x170004C4 RID: 1220
		// (get) Token: 0x06001373 RID: 4979 RVA: 0x000382A0 File Offset: 0x000364A0
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 yyxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.y, this.y, this.x, this.x);
			}
		}

		// Token: 0x170004C5 RID: 1221
		// (get) Token: 0x06001374 RID: 4980 RVA: 0x000382BF File Offset: 0x000364BF
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 yyxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.y, this.y, this.x, this.y);
			}
		}

		// Token: 0x170004C6 RID: 1222
		// (get) Token: 0x06001375 RID: 4981 RVA: 0x000382DE File Offset: 0x000364DE
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 yyxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.y, this.y, this.x, this.z);
			}
		}

		// Token: 0x170004C7 RID: 1223
		// (get) Token: 0x06001376 RID: 4982 RVA: 0x000382FD File Offset: 0x000364FD
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 yyxw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.y, this.y, this.x, this.w);
			}
		}

		// Token: 0x170004C8 RID: 1224
		// (get) Token: 0x06001377 RID: 4983 RVA: 0x0003831C File Offset: 0x0003651C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 yyyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.y, this.y, this.y, this.x);
			}
		}

		// Token: 0x170004C9 RID: 1225
		// (get) Token: 0x06001378 RID: 4984 RVA: 0x0003833B File Offset: 0x0003653B
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 yyyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.y, this.y, this.y, this.y);
			}
		}

		// Token: 0x170004CA RID: 1226
		// (get) Token: 0x06001379 RID: 4985 RVA: 0x0003835A File Offset: 0x0003655A
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 yyyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.y, this.y, this.y, this.z);
			}
		}

		// Token: 0x170004CB RID: 1227
		// (get) Token: 0x0600137A RID: 4986 RVA: 0x00038379 File Offset: 0x00036579
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 yyyw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.y, this.y, this.y, this.w);
			}
		}

		// Token: 0x170004CC RID: 1228
		// (get) Token: 0x0600137B RID: 4987 RVA: 0x00038398 File Offset: 0x00036598
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 yyzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.y, this.y, this.z, this.x);
			}
		}

		// Token: 0x170004CD RID: 1229
		// (get) Token: 0x0600137C RID: 4988 RVA: 0x000383B7 File Offset: 0x000365B7
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 yyzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.y, this.y, this.z, this.y);
			}
		}

		// Token: 0x170004CE RID: 1230
		// (get) Token: 0x0600137D RID: 4989 RVA: 0x000383D6 File Offset: 0x000365D6
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 yyzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.y, this.y, this.z, this.z);
			}
		}

		// Token: 0x170004CF RID: 1231
		// (get) Token: 0x0600137E RID: 4990 RVA: 0x000383F5 File Offset: 0x000365F5
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 yyzw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.y, this.y, this.z, this.w);
			}
		}

		// Token: 0x170004D0 RID: 1232
		// (get) Token: 0x0600137F RID: 4991 RVA: 0x00038414 File Offset: 0x00036614
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 yywx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.y, this.y, this.w, this.x);
			}
		}

		// Token: 0x170004D1 RID: 1233
		// (get) Token: 0x06001380 RID: 4992 RVA: 0x00038433 File Offset: 0x00036633
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 yywy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.y, this.y, this.w, this.y);
			}
		}

		// Token: 0x170004D2 RID: 1234
		// (get) Token: 0x06001381 RID: 4993 RVA: 0x00038452 File Offset: 0x00036652
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 yywz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.y, this.y, this.w, this.z);
			}
		}

		// Token: 0x170004D3 RID: 1235
		// (get) Token: 0x06001382 RID: 4994 RVA: 0x00038471 File Offset: 0x00036671
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 yyww
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.y, this.y, this.w, this.w);
			}
		}

		// Token: 0x170004D4 RID: 1236
		// (get) Token: 0x06001383 RID: 4995 RVA: 0x00038490 File Offset: 0x00036690
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 yzxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.y, this.z, this.x, this.x);
			}
		}

		// Token: 0x170004D5 RID: 1237
		// (get) Token: 0x06001384 RID: 4996 RVA: 0x000384AF File Offset: 0x000366AF
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 yzxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.y, this.z, this.x, this.y);
			}
		}

		// Token: 0x170004D6 RID: 1238
		// (get) Token: 0x06001385 RID: 4997 RVA: 0x000384CE File Offset: 0x000366CE
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 yzxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.y, this.z, this.x, this.z);
			}
		}

		// Token: 0x170004D7 RID: 1239
		// (get) Token: 0x06001386 RID: 4998 RVA: 0x000384ED File Offset: 0x000366ED
		// (set) Token: 0x06001387 RID: 4999 RVA: 0x0003850C File Offset: 0x0003670C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 yzxw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.y, this.z, this.x, this.w);
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

		// Token: 0x170004D8 RID: 1240
		// (get) Token: 0x06001388 RID: 5000 RVA: 0x0003853E File Offset: 0x0003673E
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 yzyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.y, this.z, this.y, this.x);
			}
		}

		// Token: 0x170004D9 RID: 1241
		// (get) Token: 0x06001389 RID: 5001 RVA: 0x0003855D File Offset: 0x0003675D
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 yzyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.y, this.z, this.y, this.y);
			}
		}

		// Token: 0x170004DA RID: 1242
		// (get) Token: 0x0600138A RID: 5002 RVA: 0x0003857C File Offset: 0x0003677C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 yzyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.y, this.z, this.y, this.z);
			}
		}

		// Token: 0x170004DB RID: 1243
		// (get) Token: 0x0600138B RID: 5003 RVA: 0x0003859B File Offset: 0x0003679B
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 yzyw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.y, this.z, this.y, this.w);
			}
		}

		// Token: 0x170004DC RID: 1244
		// (get) Token: 0x0600138C RID: 5004 RVA: 0x000385BA File Offset: 0x000367BA
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 yzzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.y, this.z, this.z, this.x);
			}
		}

		// Token: 0x170004DD RID: 1245
		// (get) Token: 0x0600138D RID: 5005 RVA: 0x000385D9 File Offset: 0x000367D9
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 yzzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.y, this.z, this.z, this.y);
			}
		}

		// Token: 0x170004DE RID: 1246
		// (get) Token: 0x0600138E RID: 5006 RVA: 0x000385F8 File Offset: 0x000367F8
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 yzzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.y, this.z, this.z, this.z);
			}
		}

		// Token: 0x170004DF RID: 1247
		// (get) Token: 0x0600138F RID: 5007 RVA: 0x00038617 File Offset: 0x00036817
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 yzzw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.y, this.z, this.z, this.w);
			}
		}

		// Token: 0x170004E0 RID: 1248
		// (get) Token: 0x06001390 RID: 5008 RVA: 0x00038636 File Offset: 0x00036836
		// (set) Token: 0x06001391 RID: 5009 RVA: 0x00038655 File Offset: 0x00036855
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 yzwx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.y, this.z, this.w, this.x);
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

		// Token: 0x170004E1 RID: 1249
		// (get) Token: 0x06001392 RID: 5010 RVA: 0x00038687 File Offset: 0x00036887
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 yzwy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.y, this.z, this.w, this.y);
			}
		}

		// Token: 0x170004E2 RID: 1250
		// (get) Token: 0x06001393 RID: 5011 RVA: 0x000386A6 File Offset: 0x000368A6
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 yzwz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.y, this.z, this.w, this.z);
			}
		}

		// Token: 0x170004E3 RID: 1251
		// (get) Token: 0x06001394 RID: 5012 RVA: 0x000386C5 File Offset: 0x000368C5
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 yzww
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.y, this.z, this.w, this.w);
			}
		}

		// Token: 0x170004E4 RID: 1252
		// (get) Token: 0x06001395 RID: 5013 RVA: 0x000386E4 File Offset: 0x000368E4
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 ywxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.y, this.w, this.x, this.x);
			}
		}

		// Token: 0x170004E5 RID: 1253
		// (get) Token: 0x06001396 RID: 5014 RVA: 0x00038703 File Offset: 0x00036903
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 ywxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.y, this.w, this.x, this.y);
			}
		}

		// Token: 0x170004E6 RID: 1254
		// (get) Token: 0x06001397 RID: 5015 RVA: 0x00038722 File Offset: 0x00036922
		// (set) Token: 0x06001398 RID: 5016 RVA: 0x00038741 File Offset: 0x00036941
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 ywxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.y, this.w, this.x, this.z);
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

		// Token: 0x170004E7 RID: 1255
		// (get) Token: 0x06001399 RID: 5017 RVA: 0x00038773 File Offset: 0x00036973
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 ywxw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.y, this.w, this.x, this.w);
			}
		}

		// Token: 0x170004E8 RID: 1256
		// (get) Token: 0x0600139A RID: 5018 RVA: 0x00038792 File Offset: 0x00036992
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 ywyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.y, this.w, this.y, this.x);
			}
		}

		// Token: 0x170004E9 RID: 1257
		// (get) Token: 0x0600139B RID: 5019 RVA: 0x000387B1 File Offset: 0x000369B1
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 ywyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.y, this.w, this.y, this.y);
			}
		}

		// Token: 0x170004EA RID: 1258
		// (get) Token: 0x0600139C RID: 5020 RVA: 0x000387D0 File Offset: 0x000369D0
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 ywyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.y, this.w, this.y, this.z);
			}
		}

		// Token: 0x170004EB RID: 1259
		// (get) Token: 0x0600139D RID: 5021 RVA: 0x000387EF File Offset: 0x000369EF
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 ywyw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.y, this.w, this.y, this.w);
			}
		}

		// Token: 0x170004EC RID: 1260
		// (get) Token: 0x0600139E RID: 5022 RVA: 0x0003880E File Offset: 0x00036A0E
		// (set) Token: 0x0600139F RID: 5023 RVA: 0x0003882D File Offset: 0x00036A2D
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 ywzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.y, this.w, this.z, this.x);
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

		// Token: 0x170004ED RID: 1261
		// (get) Token: 0x060013A0 RID: 5024 RVA: 0x0003885F File Offset: 0x00036A5F
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 ywzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.y, this.w, this.z, this.y);
			}
		}

		// Token: 0x170004EE RID: 1262
		// (get) Token: 0x060013A1 RID: 5025 RVA: 0x0003887E File Offset: 0x00036A7E
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 ywzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.y, this.w, this.z, this.z);
			}
		}

		// Token: 0x170004EF RID: 1263
		// (get) Token: 0x060013A2 RID: 5026 RVA: 0x0003889D File Offset: 0x00036A9D
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 ywzw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.y, this.w, this.z, this.w);
			}
		}

		// Token: 0x170004F0 RID: 1264
		// (get) Token: 0x060013A3 RID: 5027 RVA: 0x000388BC File Offset: 0x00036ABC
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 ywwx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.y, this.w, this.w, this.x);
			}
		}

		// Token: 0x170004F1 RID: 1265
		// (get) Token: 0x060013A4 RID: 5028 RVA: 0x000388DB File Offset: 0x00036ADB
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 ywwy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.y, this.w, this.w, this.y);
			}
		}

		// Token: 0x170004F2 RID: 1266
		// (get) Token: 0x060013A5 RID: 5029 RVA: 0x000388FA File Offset: 0x00036AFA
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 ywwz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.y, this.w, this.w, this.z);
			}
		}

		// Token: 0x170004F3 RID: 1267
		// (get) Token: 0x060013A6 RID: 5030 RVA: 0x00038919 File Offset: 0x00036B19
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 ywww
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.y, this.w, this.w, this.w);
			}
		}

		// Token: 0x170004F4 RID: 1268
		// (get) Token: 0x060013A7 RID: 5031 RVA: 0x00038938 File Offset: 0x00036B38
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 zxxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.z, this.x, this.x, this.x);
			}
		}

		// Token: 0x170004F5 RID: 1269
		// (get) Token: 0x060013A8 RID: 5032 RVA: 0x00038957 File Offset: 0x00036B57
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 zxxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.z, this.x, this.x, this.y);
			}
		}

		// Token: 0x170004F6 RID: 1270
		// (get) Token: 0x060013A9 RID: 5033 RVA: 0x00038976 File Offset: 0x00036B76
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 zxxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.z, this.x, this.x, this.z);
			}
		}

		// Token: 0x170004F7 RID: 1271
		// (get) Token: 0x060013AA RID: 5034 RVA: 0x00038995 File Offset: 0x00036B95
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 zxxw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.z, this.x, this.x, this.w);
			}
		}

		// Token: 0x170004F8 RID: 1272
		// (get) Token: 0x060013AB RID: 5035 RVA: 0x000389B4 File Offset: 0x00036BB4
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 zxyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.z, this.x, this.y, this.x);
			}
		}

		// Token: 0x170004F9 RID: 1273
		// (get) Token: 0x060013AC RID: 5036 RVA: 0x000389D3 File Offset: 0x00036BD3
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 zxyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.z, this.x, this.y, this.y);
			}
		}

		// Token: 0x170004FA RID: 1274
		// (get) Token: 0x060013AD RID: 5037 RVA: 0x000389F2 File Offset: 0x00036BF2
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 zxyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.z, this.x, this.y, this.z);
			}
		}

		// Token: 0x170004FB RID: 1275
		// (get) Token: 0x060013AE RID: 5038 RVA: 0x00038A11 File Offset: 0x00036C11
		// (set) Token: 0x060013AF RID: 5039 RVA: 0x00038A30 File Offset: 0x00036C30
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 zxyw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.z, this.x, this.y, this.w);
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

		// Token: 0x170004FC RID: 1276
		// (get) Token: 0x060013B0 RID: 5040 RVA: 0x00038A62 File Offset: 0x00036C62
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 zxzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.z, this.x, this.z, this.x);
			}
		}

		// Token: 0x170004FD RID: 1277
		// (get) Token: 0x060013B1 RID: 5041 RVA: 0x00038A81 File Offset: 0x00036C81
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 zxzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.z, this.x, this.z, this.y);
			}
		}

		// Token: 0x170004FE RID: 1278
		// (get) Token: 0x060013B2 RID: 5042 RVA: 0x00038AA0 File Offset: 0x00036CA0
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 zxzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.z, this.x, this.z, this.z);
			}
		}

		// Token: 0x170004FF RID: 1279
		// (get) Token: 0x060013B3 RID: 5043 RVA: 0x00038ABF File Offset: 0x00036CBF
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 zxzw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.z, this.x, this.z, this.w);
			}
		}

		// Token: 0x17000500 RID: 1280
		// (get) Token: 0x060013B4 RID: 5044 RVA: 0x00038ADE File Offset: 0x00036CDE
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 zxwx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.z, this.x, this.w, this.x);
			}
		}

		// Token: 0x17000501 RID: 1281
		// (get) Token: 0x060013B5 RID: 5045 RVA: 0x00038AFD File Offset: 0x00036CFD
		// (set) Token: 0x060013B6 RID: 5046 RVA: 0x00038B1C File Offset: 0x00036D1C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 zxwy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.z, this.x, this.w, this.y);
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

		// Token: 0x17000502 RID: 1282
		// (get) Token: 0x060013B7 RID: 5047 RVA: 0x00038B4E File Offset: 0x00036D4E
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 zxwz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.z, this.x, this.w, this.z);
			}
		}

		// Token: 0x17000503 RID: 1283
		// (get) Token: 0x060013B8 RID: 5048 RVA: 0x00038B6D File Offset: 0x00036D6D
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 zxww
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.z, this.x, this.w, this.w);
			}
		}

		// Token: 0x17000504 RID: 1284
		// (get) Token: 0x060013B9 RID: 5049 RVA: 0x00038B8C File Offset: 0x00036D8C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 zyxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.z, this.y, this.x, this.x);
			}
		}

		// Token: 0x17000505 RID: 1285
		// (get) Token: 0x060013BA RID: 5050 RVA: 0x00038BAB File Offset: 0x00036DAB
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 zyxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.z, this.y, this.x, this.y);
			}
		}

		// Token: 0x17000506 RID: 1286
		// (get) Token: 0x060013BB RID: 5051 RVA: 0x00038BCA File Offset: 0x00036DCA
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 zyxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.z, this.y, this.x, this.z);
			}
		}

		// Token: 0x17000507 RID: 1287
		// (get) Token: 0x060013BC RID: 5052 RVA: 0x00038BE9 File Offset: 0x00036DE9
		// (set) Token: 0x060013BD RID: 5053 RVA: 0x00038C08 File Offset: 0x00036E08
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 zyxw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.z, this.y, this.x, this.w);
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

		// Token: 0x17000508 RID: 1288
		// (get) Token: 0x060013BE RID: 5054 RVA: 0x00038C3A File Offset: 0x00036E3A
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 zyyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.z, this.y, this.y, this.x);
			}
		}

		// Token: 0x17000509 RID: 1289
		// (get) Token: 0x060013BF RID: 5055 RVA: 0x00038C59 File Offset: 0x00036E59
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 zyyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.z, this.y, this.y, this.y);
			}
		}

		// Token: 0x1700050A RID: 1290
		// (get) Token: 0x060013C0 RID: 5056 RVA: 0x00038C78 File Offset: 0x00036E78
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 zyyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.z, this.y, this.y, this.z);
			}
		}

		// Token: 0x1700050B RID: 1291
		// (get) Token: 0x060013C1 RID: 5057 RVA: 0x00038C97 File Offset: 0x00036E97
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 zyyw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.z, this.y, this.y, this.w);
			}
		}

		// Token: 0x1700050C RID: 1292
		// (get) Token: 0x060013C2 RID: 5058 RVA: 0x00038CB6 File Offset: 0x00036EB6
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 zyzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.z, this.y, this.z, this.x);
			}
		}

		// Token: 0x1700050D RID: 1293
		// (get) Token: 0x060013C3 RID: 5059 RVA: 0x00038CD5 File Offset: 0x00036ED5
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 zyzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.z, this.y, this.z, this.y);
			}
		}

		// Token: 0x1700050E RID: 1294
		// (get) Token: 0x060013C4 RID: 5060 RVA: 0x00038CF4 File Offset: 0x00036EF4
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 zyzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.z, this.y, this.z, this.z);
			}
		}

		// Token: 0x1700050F RID: 1295
		// (get) Token: 0x060013C5 RID: 5061 RVA: 0x00038D13 File Offset: 0x00036F13
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 zyzw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.z, this.y, this.z, this.w);
			}
		}

		// Token: 0x17000510 RID: 1296
		// (get) Token: 0x060013C6 RID: 5062 RVA: 0x00038D32 File Offset: 0x00036F32
		// (set) Token: 0x060013C7 RID: 5063 RVA: 0x00038D51 File Offset: 0x00036F51
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 zywx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.z, this.y, this.w, this.x);
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

		// Token: 0x17000511 RID: 1297
		// (get) Token: 0x060013C8 RID: 5064 RVA: 0x00038D83 File Offset: 0x00036F83
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 zywy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.z, this.y, this.w, this.y);
			}
		}

		// Token: 0x17000512 RID: 1298
		// (get) Token: 0x060013C9 RID: 5065 RVA: 0x00038DA2 File Offset: 0x00036FA2
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 zywz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.z, this.y, this.w, this.z);
			}
		}

		// Token: 0x17000513 RID: 1299
		// (get) Token: 0x060013CA RID: 5066 RVA: 0x00038DC1 File Offset: 0x00036FC1
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 zyww
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.z, this.y, this.w, this.w);
			}
		}

		// Token: 0x17000514 RID: 1300
		// (get) Token: 0x060013CB RID: 5067 RVA: 0x00038DE0 File Offset: 0x00036FE0
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 zzxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.z, this.z, this.x, this.x);
			}
		}

		// Token: 0x17000515 RID: 1301
		// (get) Token: 0x060013CC RID: 5068 RVA: 0x00038DFF File Offset: 0x00036FFF
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 zzxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.z, this.z, this.x, this.y);
			}
		}

		// Token: 0x17000516 RID: 1302
		// (get) Token: 0x060013CD RID: 5069 RVA: 0x00038E1E File Offset: 0x0003701E
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 zzxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.z, this.z, this.x, this.z);
			}
		}

		// Token: 0x17000517 RID: 1303
		// (get) Token: 0x060013CE RID: 5070 RVA: 0x00038E3D File Offset: 0x0003703D
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 zzxw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.z, this.z, this.x, this.w);
			}
		}

		// Token: 0x17000518 RID: 1304
		// (get) Token: 0x060013CF RID: 5071 RVA: 0x00038E5C File Offset: 0x0003705C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 zzyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.z, this.z, this.y, this.x);
			}
		}

		// Token: 0x17000519 RID: 1305
		// (get) Token: 0x060013D0 RID: 5072 RVA: 0x00038E7B File Offset: 0x0003707B
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 zzyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.z, this.z, this.y, this.y);
			}
		}

		// Token: 0x1700051A RID: 1306
		// (get) Token: 0x060013D1 RID: 5073 RVA: 0x00038E9A File Offset: 0x0003709A
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 zzyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.z, this.z, this.y, this.z);
			}
		}

		// Token: 0x1700051B RID: 1307
		// (get) Token: 0x060013D2 RID: 5074 RVA: 0x00038EB9 File Offset: 0x000370B9
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 zzyw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.z, this.z, this.y, this.w);
			}
		}

		// Token: 0x1700051C RID: 1308
		// (get) Token: 0x060013D3 RID: 5075 RVA: 0x00038ED8 File Offset: 0x000370D8
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 zzzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.z, this.z, this.z, this.x);
			}
		}

		// Token: 0x1700051D RID: 1309
		// (get) Token: 0x060013D4 RID: 5076 RVA: 0x00038EF7 File Offset: 0x000370F7
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 zzzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.z, this.z, this.z, this.y);
			}
		}

		// Token: 0x1700051E RID: 1310
		// (get) Token: 0x060013D5 RID: 5077 RVA: 0x00038F16 File Offset: 0x00037116
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 zzzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.z, this.z, this.z, this.z);
			}
		}

		// Token: 0x1700051F RID: 1311
		// (get) Token: 0x060013D6 RID: 5078 RVA: 0x00038F35 File Offset: 0x00037135
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 zzzw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.z, this.z, this.z, this.w);
			}
		}

		// Token: 0x17000520 RID: 1312
		// (get) Token: 0x060013D7 RID: 5079 RVA: 0x00038F54 File Offset: 0x00037154
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 zzwx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.z, this.z, this.w, this.x);
			}
		}

		// Token: 0x17000521 RID: 1313
		// (get) Token: 0x060013D8 RID: 5080 RVA: 0x00038F73 File Offset: 0x00037173
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 zzwy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.z, this.z, this.w, this.y);
			}
		}

		// Token: 0x17000522 RID: 1314
		// (get) Token: 0x060013D9 RID: 5081 RVA: 0x00038F92 File Offset: 0x00037192
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 zzwz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.z, this.z, this.w, this.z);
			}
		}

		// Token: 0x17000523 RID: 1315
		// (get) Token: 0x060013DA RID: 5082 RVA: 0x00038FB1 File Offset: 0x000371B1
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 zzww
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.z, this.z, this.w, this.w);
			}
		}

		// Token: 0x17000524 RID: 1316
		// (get) Token: 0x060013DB RID: 5083 RVA: 0x00038FD0 File Offset: 0x000371D0
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 zwxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.z, this.w, this.x, this.x);
			}
		}

		// Token: 0x17000525 RID: 1317
		// (get) Token: 0x060013DC RID: 5084 RVA: 0x00038FEF File Offset: 0x000371EF
		// (set) Token: 0x060013DD RID: 5085 RVA: 0x0003900E File Offset: 0x0003720E
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 zwxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.z, this.w, this.x, this.y);
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

		// Token: 0x17000526 RID: 1318
		// (get) Token: 0x060013DE RID: 5086 RVA: 0x00039040 File Offset: 0x00037240
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 zwxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.z, this.w, this.x, this.z);
			}
		}

		// Token: 0x17000527 RID: 1319
		// (get) Token: 0x060013DF RID: 5087 RVA: 0x0003905F File Offset: 0x0003725F
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 zwxw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.z, this.w, this.x, this.w);
			}
		}

		// Token: 0x17000528 RID: 1320
		// (get) Token: 0x060013E0 RID: 5088 RVA: 0x0003907E File Offset: 0x0003727E
		// (set) Token: 0x060013E1 RID: 5089 RVA: 0x0003909D File Offset: 0x0003729D
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 zwyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.z, this.w, this.y, this.x);
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

		// Token: 0x17000529 RID: 1321
		// (get) Token: 0x060013E2 RID: 5090 RVA: 0x000390CF File Offset: 0x000372CF
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 zwyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.z, this.w, this.y, this.y);
			}
		}

		// Token: 0x1700052A RID: 1322
		// (get) Token: 0x060013E3 RID: 5091 RVA: 0x000390EE File Offset: 0x000372EE
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 zwyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.z, this.w, this.y, this.z);
			}
		}

		// Token: 0x1700052B RID: 1323
		// (get) Token: 0x060013E4 RID: 5092 RVA: 0x0003910D File Offset: 0x0003730D
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 zwyw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.z, this.w, this.y, this.w);
			}
		}

		// Token: 0x1700052C RID: 1324
		// (get) Token: 0x060013E5 RID: 5093 RVA: 0x0003912C File Offset: 0x0003732C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 zwzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.z, this.w, this.z, this.x);
			}
		}

		// Token: 0x1700052D RID: 1325
		// (get) Token: 0x060013E6 RID: 5094 RVA: 0x0003914B File Offset: 0x0003734B
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 zwzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.z, this.w, this.z, this.y);
			}
		}

		// Token: 0x1700052E RID: 1326
		// (get) Token: 0x060013E7 RID: 5095 RVA: 0x0003916A File Offset: 0x0003736A
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 zwzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.z, this.w, this.z, this.z);
			}
		}

		// Token: 0x1700052F RID: 1327
		// (get) Token: 0x060013E8 RID: 5096 RVA: 0x00039189 File Offset: 0x00037389
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 zwzw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.z, this.w, this.z, this.w);
			}
		}

		// Token: 0x17000530 RID: 1328
		// (get) Token: 0x060013E9 RID: 5097 RVA: 0x000391A8 File Offset: 0x000373A8
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 zwwx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.z, this.w, this.w, this.x);
			}
		}

		// Token: 0x17000531 RID: 1329
		// (get) Token: 0x060013EA RID: 5098 RVA: 0x000391C7 File Offset: 0x000373C7
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 zwwy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.z, this.w, this.w, this.y);
			}
		}

		// Token: 0x17000532 RID: 1330
		// (get) Token: 0x060013EB RID: 5099 RVA: 0x000391E6 File Offset: 0x000373E6
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 zwwz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.z, this.w, this.w, this.z);
			}
		}

		// Token: 0x17000533 RID: 1331
		// (get) Token: 0x060013EC RID: 5100 RVA: 0x00039205 File Offset: 0x00037405
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 zwww
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.z, this.w, this.w, this.w);
			}
		}

		// Token: 0x17000534 RID: 1332
		// (get) Token: 0x060013ED RID: 5101 RVA: 0x00039224 File Offset: 0x00037424
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 wxxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.w, this.x, this.x, this.x);
			}
		}

		// Token: 0x17000535 RID: 1333
		// (get) Token: 0x060013EE RID: 5102 RVA: 0x00039243 File Offset: 0x00037443
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 wxxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.w, this.x, this.x, this.y);
			}
		}

		// Token: 0x17000536 RID: 1334
		// (get) Token: 0x060013EF RID: 5103 RVA: 0x00039262 File Offset: 0x00037462
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 wxxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.w, this.x, this.x, this.z);
			}
		}

		// Token: 0x17000537 RID: 1335
		// (get) Token: 0x060013F0 RID: 5104 RVA: 0x00039281 File Offset: 0x00037481
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 wxxw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.w, this.x, this.x, this.w);
			}
		}

		// Token: 0x17000538 RID: 1336
		// (get) Token: 0x060013F1 RID: 5105 RVA: 0x000392A0 File Offset: 0x000374A0
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 wxyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.w, this.x, this.y, this.x);
			}
		}

		// Token: 0x17000539 RID: 1337
		// (get) Token: 0x060013F2 RID: 5106 RVA: 0x000392BF File Offset: 0x000374BF
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 wxyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.w, this.x, this.y, this.y);
			}
		}

		// Token: 0x1700053A RID: 1338
		// (get) Token: 0x060013F3 RID: 5107 RVA: 0x000392DE File Offset: 0x000374DE
		// (set) Token: 0x060013F4 RID: 5108 RVA: 0x000392FD File Offset: 0x000374FD
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 wxyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.w, this.x, this.y, this.z);
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

		// Token: 0x1700053B RID: 1339
		// (get) Token: 0x060013F5 RID: 5109 RVA: 0x0003932F File Offset: 0x0003752F
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 wxyw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.w, this.x, this.y, this.w);
			}
		}

		// Token: 0x1700053C RID: 1340
		// (get) Token: 0x060013F6 RID: 5110 RVA: 0x0003934E File Offset: 0x0003754E
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 wxzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.w, this.x, this.z, this.x);
			}
		}

		// Token: 0x1700053D RID: 1341
		// (get) Token: 0x060013F7 RID: 5111 RVA: 0x0003936D File Offset: 0x0003756D
		// (set) Token: 0x060013F8 RID: 5112 RVA: 0x0003938C File Offset: 0x0003758C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 wxzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.w, this.x, this.z, this.y);
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

		// Token: 0x1700053E RID: 1342
		// (get) Token: 0x060013F9 RID: 5113 RVA: 0x000393BE File Offset: 0x000375BE
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 wxzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.w, this.x, this.z, this.z);
			}
		}

		// Token: 0x1700053F RID: 1343
		// (get) Token: 0x060013FA RID: 5114 RVA: 0x000393DD File Offset: 0x000375DD
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 wxzw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.w, this.x, this.z, this.w);
			}
		}

		// Token: 0x17000540 RID: 1344
		// (get) Token: 0x060013FB RID: 5115 RVA: 0x000393FC File Offset: 0x000375FC
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 wxwx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.w, this.x, this.w, this.x);
			}
		}

		// Token: 0x17000541 RID: 1345
		// (get) Token: 0x060013FC RID: 5116 RVA: 0x0003941B File Offset: 0x0003761B
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 wxwy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.w, this.x, this.w, this.y);
			}
		}

		// Token: 0x17000542 RID: 1346
		// (get) Token: 0x060013FD RID: 5117 RVA: 0x0003943A File Offset: 0x0003763A
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 wxwz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.w, this.x, this.w, this.z);
			}
		}

		// Token: 0x17000543 RID: 1347
		// (get) Token: 0x060013FE RID: 5118 RVA: 0x00039459 File Offset: 0x00037659
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 wxww
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.w, this.x, this.w, this.w);
			}
		}

		// Token: 0x17000544 RID: 1348
		// (get) Token: 0x060013FF RID: 5119 RVA: 0x00039478 File Offset: 0x00037678
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 wyxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.w, this.y, this.x, this.x);
			}
		}

		// Token: 0x17000545 RID: 1349
		// (get) Token: 0x06001400 RID: 5120 RVA: 0x00039497 File Offset: 0x00037697
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 wyxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.w, this.y, this.x, this.y);
			}
		}

		// Token: 0x17000546 RID: 1350
		// (get) Token: 0x06001401 RID: 5121 RVA: 0x000394B6 File Offset: 0x000376B6
		// (set) Token: 0x06001402 RID: 5122 RVA: 0x000394D5 File Offset: 0x000376D5
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 wyxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.w, this.y, this.x, this.z);
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

		// Token: 0x17000547 RID: 1351
		// (get) Token: 0x06001403 RID: 5123 RVA: 0x00039507 File Offset: 0x00037707
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 wyxw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.w, this.y, this.x, this.w);
			}
		}

		// Token: 0x17000548 RID: 1352
		// (get) Token: 0x06001404 RID: 5124 RVA: 0x00039526 File Offset: 0x00037726
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 wyyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.w, this.y, this.y, this.x);
			}
		}

		// Token: 0x17000549 RID: 1353
		// (get) Token: 0x06001405 RID: 5125 RVA: 0x00039545 File Offset: 0x00037745
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 wyyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.w, this.y, this.y, this.y);
			}
		}

		// Token: 0x1700054A RID: 1354
		// (get) Token: 0x06001406 RID: 5126 RVA: 0x00039564 File Offset: 0x00037764
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 wyyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.w, this.y, this.y, this.z);
			}
		}

		// Token: 0x1700054B RID: 1355
		// (get) Token: 0x06001407 RID: 5127 RVA: 0x00039583 File Offset: 0x00037783
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 wyyw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.w, this.y, this.y, this.w);
			}
		}

		// Token: 0x1700054C RID: 1356
		// (get) Token: 0x06001408 RID: 5128 RVA: 0x000395A2 File Offset: 0x000377A2
		// (set) Token: 0x06001409 RID: 5129 RVA: 0x000395C1 File Offset: 0x000377C1
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 wyzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.w, this.y, this.z, this.x);
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

		// Token: 0x1700054D RID: 1357
		// (get) Token: 0x0600140A RID: 5130 RVA: 0x000395F3 File Offset: 0x000377F3
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 wyzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.w, this.y, this.z, this.y);
			}
		}

		// Token: 0x1700054E RID: 1358
		// (get) Token: 0x0600140B RID: 5131 RVA: 0x00039612 File Offset: 0x00037812
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 wyzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.w, this.y, this.z, this.z);
			}
		}

		// Token: 0x1700054F RID: 1359
		// (get) Token: 0x0600140C RID: 5132 RVA: 0x00039631 File Offset: 0x00037831
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 wyzw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.w, this.y, this.z, this.w);
			}
		}

		// Token: 0x17000550 RID: 1360
		// (get) Token: 0x0600140D RID: 5133 RVA: 0x00039650 File Offset: 0x00037850
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 wywx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.w, this.y, this.w, this.x);
			}
		}

		// Token: 0x17000551 RID: 1361
		// (get) Token: 0x0600140E RID: 5134 RVA: 0x0003966F File Offset: 0x0003786F
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 wywy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.w, this.y, this.w, this.y);
			}
		}

		// Token: 0x17000552 RID: 1362
		// (get) Token: 0x0600140F RID: 5135 RVA: 0x0003968E File Offset: 0x0003788E
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 wywz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.w, this.y, this.w, this.z);
			}
		}

		// Token: 0x17000553 RID: 1363
		// (get) Token: 0x06001410 RID: 5136 RVA: 0x000396AD File Offset: 0x000378AD
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 wyww
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.w, this.y, this.w, this.w);
			}
		}

		// Token: 0x17000554 RID: 1364
		// (get) Token: 0x06001411 RID: 5137 RVA: 0x000396CC File Offset: 0x000378CC
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 wzxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.w, this.z, this.x, this.x);
			}
		}

		// Token: 0x17000555 RID: 1365
		// (get) Token: 0x06001412 RID: 5138 RVA: 0x000396EB File Offset: 0x000378EB
		// (set) Token: 0x06001413 RID: 5139 RVA: 0x0003970A File Offset: 0x0003790A
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 wzxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.w, this.z, this.x, this.y);
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

		// Token: 0x17000556 RID: 1366
		// (get) Token: 0x06001414 RID: 5140 RVA: 0x0003973C File Offset: 0x0003793C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 wzxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.w, this.z, this.x, this.z);
			}
		}

		// Token: 0x17000557 RID: 1367
		// (get) Token: 0x06001415 RID: 5141 RVA: 0x0003975B File Offset: 0x0003795B
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 wzxw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.w, this.z, this.x, this.w);
			}
		}

		// Token: 0x17000558 RID: 1368
		// (get) Token: 0x06001416 RID: 5142 RVA: 0x0003977A File Offset: 0x0003797A
		// (set) Token: 0x06001417 RID: 5143 RVA: 0x00039799 File Offset: 0x00037999
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 wzyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.w, this.z, this.y, this.x);
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

		// Token: 0x17000559 RID: 1369
		// (get) Token: 0x06001418 RID: 5144 RVA: 0x000397CB File Offset: 0x000379CB
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 wzyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.w, this.z, this.y, this.y);
			}
		}

		// Token: 0x1700055A RID: 1370
		// (get) Token: 0x06001419 RID: 5145 RVA: 0x000397EA File Offset: 0x000379EA
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 wzyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.w, this.z, this.y, this.z);
			}
		}

		// Token: 0x1700055B RID: 1371
		// (get) Token: 0x0600141A RID: 5146 RVA: 0x00039809 File Offset: 0x00037A09
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 wzyw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.w, this.z, this.y, this.w);
			}
		}

		// Token: 0x1700055C RID: 1372
		// (get) Token: 0x0600141B RID: 5147 RVA: 0x00039828 File Offset: 0x00037A28
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 wzzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.w, this.z, this.z, this.x);
			}
		}

		// Token: 0x1700055D RID: 1373
		// (get) Token: 0x0600141C RID: 5148 RVA: 0x00039847 File Offset: 0x00037A47
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 wzzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.w, this.z, this.z, this.y);
			}
		}

		// Token: 0x1700055E RID: 1374
		// (get) Token: 0x0600141D RID: 5149 RVA: 0x00039866 File Offset: 0x00037A66
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 wzzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.w, this.z, this.z, this.z);
			}
		}

		// Token: 0x1700055F RID: 1375
		// (get) Token: 0x0600141E RID: 5150 RVA: 0x00039885 File Offset: 0x00037A85
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 wzzw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.w, this.z, this.z, this.w);
			}
		}

		// Token: 0x17000560 RID: 1376
		// (get) Token: 0x0600141F RID: 5151 RVA: 0x000398A4 File Offset: 0x00037AA4
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 wzwx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.w, this.z, this.w, this.x);
			}
		}

		// Token: 0x17000561 RID: 1377
		// (get) Token: 0x06001420 RID: 5152 RVA: 0x000398C3 File Offset: 0x00037AC3
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 wzwy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.w, this.z, this.w, this.y);
			}
		}

		// Token: 0x17000562 RID: 1378
		// (get) Token: 0x06001421 RID: 5153 RVA: 0x000398E2 File Offset: 0x00037AE2
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 wzwz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.w, this.z, this.w, this.z);
			}
		}

		// Token: 0x17000563 RID: 1379
		// (get) Token: 0x06001422 RID: 5154 RVA: 0x00039901 File Offset: 0x00037B01
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 wzww
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.w, this.z, this.w, this.w);
			}
		}

		// Token: 0x17000564 RID: 1380
		// (get) Token: 0x06001423 RID: 5155 RVA: 0x00039920 File Offset: 0x00037B20
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 wwxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.w, this.w, this.x, this.x);
			}
		}

		// Token: 0x17000565 RID: 1381
		// (get) Token: 0x06001424 RID: 5156 RVA: 0x0003993F File Offset: 0x00037B3F
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 wwxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.w, this.w, this.x, this.y);
			}
		}

		// Token: 0x17000566 RID: 1382
		// (get) Token: 0x06001425 RID: 5157 RVA: 0x0003995E File Offset: 0x00037B5E
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 wwxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.w, this.w, this.x, this.z);
			}
		}

		// Token: 0x17000567 RID: 1383
		// (get) Token: 0x06001426 RID: 5158 RVA: 0x0003997D File Offset: 0x00037B7D
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 wwxw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.w, this.w, this.x, this.w);
			}
		}

		// Token: 0x17000568 RID: 1384
		// (get) Token: 0x06001427 RID: 5159 RVA: 0x0003999C File Offset: 0x00037B9C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 wwyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.w, this.w, this.y, this.x);
			}
		}

		// Token: 0x17000569 RID: 1385
		// (get) Token: 0x06001428 RID: 5160 RVA: 0x000399BB File Offset: 0x00037BBB
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 wwyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.w, this.w, this.y, this.y);
			}
		}

		// Token: 0x1700056A RID: 1386
		// (get) Token: 0x06001429 RID: 5161 RVA: 0x000399DA File Offset: 0x00037BDA
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 wwyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.w, this.w, this.y, this.z);
			}
		}

		// Token: 0x1700056B RID: 1387
		// (get) Token: 0x0600142A RID: 5162 RVA: 0x000399F9 File Offset: 0x00037BF9
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 wwyw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.w, this.w, this.y, this.w);
			}
		}

		// Token: 0x1700056C RID: 1388
		// (get) Token: 0x0600142B RID: 5163 RVA: 0x00039A18 File Offset: 0x00037C18
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 wwzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.w, this.w, this.z, this.x);
			}
		}

		// Token: 0x1700056D RID: 1389
		// (get) Token: 0x0600142C RID: 5164 RVA: 0x00039A37 File Offset: 0x00037C37
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 wwzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.w, this.w, this.z, this.y);
			}
		}

		// Token: 0x1700056E RID: 1390
		// (get) Token: 0x0600142D RID: 5165 RVA: 0x00039A56 File Offset: 0x00037C56
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 wwzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.w, this.w, this.z, this.z);
			}
		}

		// Token: 0x1700056F RID: 1391
		// (get) Token: 0x0600142E RID: 5166 RVA: 0x00039A75 File Offset: 0x00037C75
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 wwzw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.w, this.w, this.z, this.w);
			}
		}

		// Token: 0x17000570 RID: 1392
		// (get) Token: 0x0600142F RID: 5167 RVA: 0x00039A94 File Offset: 0x00037C94
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 wwwx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.w, this.w, this.w, this.x);
			}
		}

		// Token: 0x17000571 RID: 1393
		// (get) Token: 0x06001430 RID: 5168 RVA: 0x00039AB3 File Offset: 0x00037CB3
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 wwwy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.w, this.w, this.w, this.y);
			}
		}

		// Token: 0x17000572 RID: 1394
		// (get) Token: 0x06001431 RID: 5169 RVA: 0x00039AD2 File Offset: 0x00037CD2
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 wwwz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.w, this.w, this.w, this.z);
			}
		}

		// Token: 0x17000573 RID: 1395
		// (get) Token: 0x06001432 RID: 5170 RVA: 0x00039AF1 File Offset: 0x00037CF1
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 wwww
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.w, this.w, this.w, this.w);
			}
		}

		// Token: 0x17000574 RID: 1396
		// (get) Token: 0x06001433 RID: 5171 RVA: 0x00039B10 File Offset: 0x00037D10
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float3 xxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float3(this.x, this.x, this.x);
			}
		}

		// Token: 0x17000575 RID: 1397
		// (get) Token: 0x06001434 RID: 5172 RVA: 0x00039B29 File Offset: 0x00037D29
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float3 xxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float3(this.x, this.x, this.y);
			}
		}

		// Token: 0x17000576 RID: 1398
		// (get) Token: 0x06001435 RID: 5173 RVA: 0x00039B42 File Offset: 0x00037D42
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float3 xxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float3(this.x, this.x, this.z);
			}
		}

		// Token: 0x17000577 RID: 1399
		// (get) Token: 0x06001436 RID: 5174 RVA: 0x00039B5B File Offset: 0x00037D5B
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float3 xxw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float3(this.x, this.x, this.w);
			}
		}

		// Token: 0x17000578 RID: 1400
		// (get) Token: 0x06001437 RID: 5175 RVA: 0x00039B74 File Offset: 0x00037D74
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float3 xyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float3(this.x, this.y, this.x);
			}
		}

		// Token: 0x17000579 RID: 1401
		// (get) Token: 0x06001438 RID: 5176 RVA: 0x00039B8D File Offset: 0x00037D8D
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float3 xyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float3(this.x, this.y, this.y);
			}
		}

		// Token: 0x1700057A RID: 1402
		// (get) Token: 0x06001439 RID: 5177 RVA: 0x00039BA6 File Offset: 0x00037DA6
		// (set) Token: 0x0600143A RID: 5178 RVA: 0x00039BBF File Offset: 0x00037DBF
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float3 xyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float3(this.x, this.y, this.z);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.x = value.x;
				this.y = value.y;
				this.z = value.z;
			}
		}

		// Token: 0x1700057B RID: 1403
		// (get) Token: 0x0600143B RID: 5179 RVA: 0x00039BE5 File Offset: 0x00037DE5
		// (set) Token: 0x0600143C RID: 5180 RVA: 0x00039BFE File Offset: 0x00037DFE
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float3 xyw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float3(this.x, this.y, this.w);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.x = value.x;
				this.y = value.y;
				this.w = value.z;
			}
		}

		// Token: 0x1700057C RID: 1404
		// (get) Token: 0x0600143D RID: 5181 RVA: 0x00039C24 File Offset: 0x00037E24
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float3 xzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float3(this.x, this.z, this.x);
			}
		}

		// Token: 0x1700057D RID: 1405
		// (get) Token: 0x0600143E RID: 5182 RVA: 0x00039C3D File Offset: 0x00037E3D
		// (set) Token: 0x0600143F RID: 5183 RVA: 0x00039C56 File Offset: 0x00037E56
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float3 xzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float3(this.x, this.z, this.y);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.x = value.x;
				this.z = value.y;
				this.y = value.z;
			}
		}

		// Token: 0x1700057E RID: 1406
		// (get) Token: 0x06001440 RID: 5184 RVA: 0x00039C7C File Offset: 0x00037E7C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float3 xzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float3(this.x, this.z, this.z);
			}
		}

		// Token: 0x1700057F RID: 1407
		// (get) Token: 0x06001441 RID: 5185 RVA: 0x00039C95 File Offset: 0x00037E95
		// (set) Token: 0x06001442 RID: 5186 RVA: 0x00039CAE File Offset: 0x00037EAE
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float3 xzw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float3(this.x, this.z, this.w);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.x = value.x;
				this.z = value.y;
				this.w = value.z;
			}
		}

		// Token: 0x17000580 RID: 1408
		// (get) Token: 0x06001443 RID: 5187 RVA: 0x00039CD4 File Offset: 0x00037ED4
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float3 xwx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float3(this.x, this.w, this.x);
			}
		}

		// Token: 0x17000581 RID: 1409
		// (get) Token: 0x06001444 RID: 5188 RVA: 0x00039CED File Offset: 0x00037EED
		// (set) Token: 0x06001445 RID: 5189 RVA: 0x00039D06 File Offset: 0x00037F06
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float3 xwy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float3(this.x, this.w, this.y);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.x = value.x;
				this.w = value.y;
				this.y = value.z;
			}
		}

		// Token: 0x17000582 RID: 1410
		// (get) Token: 0x06001446 RID: 5190 RVA: 0x00039D2C File Offset: 0x00037F2C
		// (set) Token: 0x06001447 RID: 5191 RVA: 0x00039D45 File Offset: 0x00037F45
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float3 xwz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float3(this.x, this.w, this.z);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.x = value.x;
				this.w = value.y;
				this.z = value.z;
			}
		}

		// Token: 0x17000583 RID: 1411
		// (get) Token: 0x06001448 RID: 5192 RVA: 0x00039D6B File Offset: 0x00037F6B
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float3 xww
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float3(this.x, this.w, this.w);
			}
		}

		// Token: 0x17000584 RID: 1412
		// (get) Token: 0x06001449 RID: 5193 RVA: 0x00039D84 File Offset: 0x00037F84
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float3 yxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float3(this.y, this.x, this.x);
			}
		}

		// Token: 0x17000585 RID: 1413
		// (get) Token: 0x0600144A RID: 5194 RVA: 0x00039D9D File Offset: 0x00037F9D
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float3 yxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float3(this.y, this.x, this.y);
			}
		}

		// Token: 0x17000586 RID: 1414
		// (get) Token: 0x0600144B RID: 5195 RVA: 0x00039DB6 File Offset: 0x00037FB6
		// (set) Token: 0x0600144C RID: 5196 RVA: 0x00039DCF File Offset: 0x00037FCF
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float3 yxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float3(this.y, this.x, this.z);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.y = value.x;
				this.x = value.y;
				this.z = value.z;
			}
		}

		// Token: 0x17000587 RID: 1415
		// (get) Token: 0x0600144D RID: 5197 RVA: 0x00039DF5 File Offset: 0x00037FF5
		// (set) Token: 0x0600144E RID: 5198 RVA: 0x00039E0E File Offset: 0x0003800E
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float3 yxw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float3(this.y, this.x, this.w);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.y = value.x;
				this.x = value.y;
				this.w = value.z;
			}
		}

		// Token: 0x17000588 RID: 1416
		// (get) Token: 0x0600144F RID: 5199 RVA: 0x00039E34 File Offset: 0x00038034
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float3 yyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float3(this.y, this.y, this.x);
			}
		}

		// Token: 0x17000589 RID: 1417
		// (get) Token: 0x06001450 RID: 5200 RVA: 0x00039E4D File Offset: 0x0003804D
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float3 yyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float3(this.y, this.y, this.y);
			}
		}

		// Token: 0x1700058A RID: 1418
		// (get) Token: 0x06001451 RID: 5201 RVA: 0x00039E66 File Offset: 0x00038066
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float3 yyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float3(this.y, this.y, this.z);
			}
		}

		// Token: 0x1700058B RID: 1419
		// (get) Token: 0x06001452 RID: 5202 RVA: 0x00039E7F File Offset: 0x0003807F
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float3 yyw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float3(this.y, this.y, this.w);
			}
		}

		// Token: 0x1700058C RID: 1420
		// (get) Token: 0x06001453 RID: 5203 RVA: 0x00039E98 File Offset: 0x00038098
		// (set) Token: 0x06001454 RID: 5204 RVA: 0x00039EB1 File Offset: 0x000380B1
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float3 yzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float3(this.y, this.z, this.x);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.y = value.x;
				this.z = value.y;
				this.x = value.z;
			}
		}

		// Token: 0x1700058D RID: 1421
		// (get) Token: 0x06001455 RID: 5205 RVA: 0x00039ED7 File Offset: 0x000380D7
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float3 yzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float3(this.y, this.z, this.y);
			}
		}

		// Token: 0x1700058E RID: 1422
		// (get) Token: 0x06001456 RID: 5206 RVA: 0x00039EF0 File Offset: 0x000380F0
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float3 yzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float3(this.y, this.z, this.z);
			}
		}

		// Token: 0x1700058F RID: 1423
		// (get) Token: 0x06001457 RID: 5207 RVA: 0x00039F09 File Offset: 0x00038109
		// (set) Token: 0x06001458 RID: 5208 RVA: 0x00039F22 File Offset: 0x00038122
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float3 yzw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float3(this.y, this.z, this.w);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.y = value.x;
				this.z = value.y;
				this.w = value.z;
			}
		}

		// Token: 0x17000590 RID: 1424
		// (get) Token: 0x06001459 RID: 5209 RVA: 0x00039F48 File Offset: 0x00038148
		// (set) Token: 0x0600145A RID: 5210 RVA: 0x00039F61 File Offset: 0x00038161
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float3 ywx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float3(this.y, this.w, this.x);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.y = value.x;
				this.w = value.y;
				this.x = value.z;
			}
		}

		// Token: 0x17000591 RID: 1425
		// (get) Token: 0x0600145B RID: 5211 RVA: 0x00039F87 File Offset: 0x00038187
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float3 ywy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float3(this.y, this.w, this.y);
			}
		}

		// Token: 0x17000592 RID: 1426
		// (get) Token: 0x0600145C RID: 5212 RVA: 0x00039FA0 File Offset: 0x000381A0
		// (set) Token: 0x0600145D RID: 5213 RVA: 0x00039FB9 File Offset: 0x000381B9
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float3 ywz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float3(this.y, this.w, this.z);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.y = value.x;
				this.w = value.y;
				this.z = value.z;
			}
		}

		// Token: 0x17000593 RID: 1427
		// (get) Token: 0x0600145E RID: 5214 RVA: 0x00039FDF File Offset: 0x000381DF
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float3 yww
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float3(this.y, this.w, this.w);
			}
		}

		// Token: 0x17000594 RID: 1428
		// (get) Token: 0x0600145F RID: 5215 RVA: 0x00039FF8 File Offset: 0x000381F8
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float3 zxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float3(this.z, this.x, this.x);
			}
		}

		// Token: 0x17000595 RID: 1429
		// (get) Token: 0x06001460 RID: 5216 RVA: 0x0003A011 File Offset: 0x00038211
		// (set) Token: 0x06001461 RID: 5217 RVA: 0x0003A02A File Offset: 0x0003822A
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float3 zxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float3(this.z, this.x, this.y);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.z = value.x;
				this.x = value.y;
				this.y = value.z;
			}
		}

		// Token: 0x17000596 RID: 1430
		// (get) Token: 0x06001462 RID: 5218 RVA: 0x0003A050 File Offset: 0x00038250
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float3 zxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float3(this.z, this.x, this.z);
			}
		}

		// Token: 0x17000597 RID: 1431
		// (get) Token: 0x06001463 RID: 5219 RVA: 0x0003A069 File Offset: 0x00038269
		// (set) Token: 0x06001464 RID: 5220 RVA: 0x0003A082 File Offset: 0x00038282
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float3 zxw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float3(this.z, this.x, this.w);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.z = value.x;
				this.x = value.y;
				this.w = value.z;
			}
		}

		// Token: 0x17000598 RID: 1432
		// (get) Token: 0x06001465 RID: 5221 RVA: 0x0003A0A8 File Offset: 0x000382A8
		// (set) Token: 0x06001466 RID: 5222 RVA: 0x0003A0C1 File Offset: 0x000382C1
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float3 zyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float3(this.z, this.y, this.x);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.z = value.x;
				this.y = value.y;
				this.x = value.z;
			}
		}

		// Token: 0x17000599 RID: 1433
		// (get) Token: 0x06001467 RID: 5223 RVA: 0x0003A0E7 File Offset: 0x000382E7
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float3 zyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float3(this.z, this.y, this.y);
			}
		}

		// Token: 0x1700059A RID: 1434
		// (get) Token: 0x06001468 RID: 5224 RVA: 0x0003A100 File Offset: 0x00038300
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float3 zyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float3(this.z, this.y, this.z);
			}
		}

		// Token: 0x1700059B RID: 1435
		// (get) Token: 0x06001469 RID: 5225 RVA: 0x0003A119 File Offset: 0x00038319
		// (set) Token: 0x0600146A RID: 5226 RVA: 0x0003A132 File Offset: 0x00038332
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float3 zyw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float3(this.z, this.y, this.w);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.z = value.x;
				this.y = value.y;
				this.w = value.z;
			}
		}

		// Token: 0x1700059C RID: 1436
		// (get) Token: 0x0600146B RID: 5227 RVA: 0x0003A158 File Offset: 0x00038358
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float3 zzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float3(this.z, this.z, this.x);
			}
		}

		// Token: 0x1700059D RID: 1437
		// (get) Token: 0x0600146C RID: 5228 RVA: 0x0003A171 File Offset: 0x00038371
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float3 zzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float3(this.z, this.z, this.y);
			}
		}

		// Token: 0x1700059E RID: 1438
		// (get) Token: 0x0600146D RID: 5229 RVA: 0x0003A18A File Offset: 0x0003838A
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float3 zzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float3(this.z, this.z, this.z);
			}
		}

		// Token: 0x1700059F RID: 1439
		// (get) Token: 0x0600146E RID: 5230 RVA: 0x0003A1A3 File Offset: 0x000383A3
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float3 zzw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float3(this.z, this.z, this.w);
			}
		}

		// Token: 0x170005A0 RID: 1440
		// (get) Token: 0x0600146F RID: 5231 RVA: 0x0003A1BC File Offset: 0x000383BC
		// (set) Token: 0x06001470 RID: 5232 RVA: 0x0003A1D5 File Offset: 0x000383D5
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float3 zwx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float3(this.z, this.w, this.x);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.z = value.x;
				this.w = value.y;
				this.x = value.z;
			}
		}

		// Token: 0x170005A1 RID: 1441
		// (get) Token: 0x06001471 RID: 5233 RVA: 0x0003A1FB File Offset: 0x000383FB
		// (set) Token: 0x06001472 RID: 5234 RVA: 0x0003A214 File Offset: 0x00038414
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float3 zwy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float3(this.z, this.w, this.y);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.z = value.x;
				this.w = value.y;
				this.y = value.z;
			}
		}

		// Token: 0x170005A2 RID: 1442
		// (get) Token: 0x06001473 RID: 5235 RVA: 0x0003A23A File Offset: 0x0003843A
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float3 zwz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float3(this.z, this.w, this.z);
			}
		}

		// Token: 0x170005A3 RID: 1443
		// (get) Token: 0x06001474 RID: 5236 RVA: 0x0003A253 File Offset: 0x00038453
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float3 zww
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float3(this.z, this.w, this.w);
			}
		}

		// Token: 0x170005A4 RID: 1444
		// (get) Token: 0x06001475 RID: 5237 RVA: 0x0003A26C File Offset: 0x0003846C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float3 wxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float3(this.w, this.x, this.x);
			}
		}

		// Token: 0x170005A5 RID: 1445
		// (get) Token: 0x06001476 RID: 5238 RVA: 0x0003A285 File Offset: 0x00038485
		// (set) Token: 0x06001477 RID: 5239 RVA: 0x0003A29E File Offset: 0x0003849E
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float3 wxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float3(this.w, this.x, this.y);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.w = value.x;
				this.x = value.y;
				this.y = value.z;
			}
		}

		// Token: 0x170005A6 RID: 1446
		// (get) Token: 0x06001478 RID: 5240 RVA: 0x0003A2C4 File Offset: 0x000384C4
		// (set) Token: 0x06001479 RID: 5241 RVA: 0x0003A2DD File Offset: 0x000384DD
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float3 wxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float3(this.w, this.x, this.z);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.w = value.x;
				this.x = value.y;
				this.z = value.z;
			}
		}

		// Token: 0x170005A7 RID: 1447
		// (get) Token: 0x0600147A RID: 5242 RVA: 0x0003A303 File Offset: 0x00038503
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float3 wxw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float3(this.w, this.x, this.w);
			}
		}

		// Token: 0x170005A8 RID: 1448
		// (get) Token: 0x0600147B RID: 5243 RVA: 0x0003A31C File Offset: 0x0003851C
		// (set) Token: 0x0600147C RID: 5244 RVA: 0x0003A335 File Offset: 0x00038535
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float3 wyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float3(this.w, this.y, this.x);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.w = value.x;
				this.y = value.y;
				this.x = value.z;
			}
		}

		// Token: 0x170005A9 RID: 1449
		// (get) Token: 0x0600147D RID: 5245 RVA: 0x0003A35B File Offset: 0x0003855B
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float3 wyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float3(this.w, this.y, this.y);
			}
		}

		// Token: 0x170005AA RID: 1450
		// (get) Token: 0x0600147E RID: 5246 RVA: 0x0003A374 File Offset: 0x00038574
		// (set) Token: 0x0600147F RID: 5247 RVA: 0x0003A38D File Offset: 0x0003858D
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float3 wyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float3(this.w, this.y, this.z);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.w = value.x;
				this.y = value.y;
				this.z = value.z;
			}
		}

		// Token: 0x170005AB RID: 1451
		// (get) Token: 0x06001480 RID: 5248 RVA: 0x0003A3B3 File Offset: 0x000385B3
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float3 wyw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float3(this.w, this.y, this.w);
			}
		}

		// Token: 0x170005AC RID: 1452
		// (get) Token: 0x06001481 RID: 5249 RVA: 0x0003A3CC File Offset: 0x000385CC
		// (set) Token: 0x06001482 RID: 5250 RVA: 0x0003A3E5 File Offset: 0x000385E5
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float3 wzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float3(this.w, this.z, this.x);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.w = value.x;
				this.z = value.y;
				this.x = value.z;
			}
		}

		// Token: 0x170005AD RID: 1453
		// (get) Token: 0x06001483 RID: 5251 RVA: 0x0003A40B File Offset: 0x0003860B
		// (set) Token: 0x06001484 RID: 5252 RVA: 0x0003A424 File Offset: 0x00038624
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float3 wzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float3(this.w, this.z, this.y);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.w = value.x;
				this.z = value.y;
				this.y = value.z;
			}
		}

		// Token: 0x170005AE RID: 1454
		// (get) Token: 0x06001485 RID: 5253 RVA: 0x0003A44A File Offset: 0x0003864A
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float3 wzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float3(this.w, this.z, this.z);
			}
		}

		// Token: 0x170005AF RID: 1455
		// (get) Token: 0x06001486 RID: 5254 RVA: 0x0003A463 File Offset: 0x00038663
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float3 wzw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float3(this.w, this.z, this.w);
			}
		}

		// Token: 0x170005B0 RID: 1456
		// (get) Token: 0x06001487 RID: 5255 RVA: 0x0003A47C File Offset: 0x0003867C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float3 wwx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float3(this.w, this.w, this.x);
			}
		}

		// Token: 0x170005B1 RID: 1457
		// (get) Token: 0x06001488 RID: 5256 RVA: 0x0003A495 File Offset: 0x00038695
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float3 wwy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float3(this.w, this.w, this.y);
			}
		}

		// Token: 0x170005B2 RID: 1458
		// (get) Token: 0x06001489 RID: 5257 RVA: 0x0003A4AE File Offset: 0x000386AE
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float3 wwz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float3(this.w, this.w, this.z);
			}
		}

		// Token: 0x170005B3 RID: 1459
		// (get) Token: 0x0600148A RID: 5258 RVA: 0x0003A4C7 File Offset: 0x000386C7
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float3 www
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float3(this.w, this.w, this.w);
			}
		}

		// Token: 0x170005B4 RID: 1460
		// (get) Token: 0x0600148B RID: 5259 RVA: 0x0003A4E0 File Offset: 0x000386E0
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float2 xx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float2(this.x, this.x);
			}
		}

		// Token: 0x170005B5 RID: 1461
		// (get) Token: 0x0600148C RID: 5260 RVA: 0x0003A4F3 File Offset: 0x000386F3
		// (set) Token: 0x0600148D RID: 5261 RVA: 0x0003A506 File Offset: 0x00038706
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float2 xy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float2(this.x, this.y);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.x = value.x;
				this.y = value.y;
			}
		}

		// Token: 0x170005B6 RID: 1462
		// (get) Token: 0x0600148E RID: 5262 RVA: 0x0003A520 File Offset: 0x00038720
		// (set) Token: 0x0600148F RID: 5263 RVA: 0x0003A533 File Offset: 0x00038733
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float2 xz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float2(this.x, this.z);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.x = value.x;
				this.z = value.y;
			}
		}

		// Token: 0x170005B7 RID: 1463
		// (get) Token: 0x06001490 RID: 5264 RVA: 0x0003A54D File Offset: 0x0003874D
		// (set) Token: 0x06001491 RID: 5265 RVA: 0x0003A560 File Offset: 0x00038760
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float2 xw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float2(this.x, this.w);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.x = value.x;
				this.w = value.y;
			}
		}

		// Token: 0x170005B8 RID: 1464
		// (get) Token: 0x06001492 RID: 5266 RVA: 0x0003A57A File Offset: 0x0003877A
		// (set) Token: 0x06001493 RID: 5267 RVA: 0x0003A58D File Offset: 0x0003878D
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float2 yx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float2(this.y, this.x);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.y = value.x;
				this.x = value.y;
			}
		}

		// Token: 0x170005B9 RID: 1465
		// (get) Token: 0x06001494 RID: 5268 RVA: 0x0003A5A7 File Offset: 0x000387A7
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float2 yy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float2(this.y, this.y);
			}
		}

		// Token: 0x170005BA RID: 1466
		// (get) Token: 0x06001495 RID: 5269 RVA: 0x0003A5BA File Offset: 0x000387BA
		// (set) Token: 0x06001496 RID: 5270 RVA: 0x0003A5CD File Offset: 0x000387CD
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float2 yz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float2(this.y, this.z);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.y = value.x;
				this.z = value.y;
			}
		}

		// Token: 0x170005BB RID: 1467
		// (get) Token: 0x06001497 RID: 5271 RVA: 0x0003A5E7 File Offset: 0x000387E7
		// (set) Token: 0x06001498 RID: 5272 RVA: 0x0003A5FA File Offset: 0x000387FA
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float2 yw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float2(this.y, this.w);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.y = value.x;
				this.w = value.y;
			}
		}

		// Token: 0x170005BC RID: 1468
		// (get) Token: 0x06001499 RID: 5273 RVA: 0x0003A614 File Offset: 0x00038814
		// (set) Token: 0x0600149A RID: 5274 RVA: 0x0003A627 File Offset: 0x00038827
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float2 zx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float2(this.z, this.x);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.z = value.x;
				this.x = value.y;
			}
		}

		// Token: 0x170005BD RID: 1469
		// (get) Token: 0x0600149B RID: 5275 RVA: 0x0003A641 File Offset: 0x00038841
		// (set) Token: 0x0600149C RID: 5276 RVA: 0x0003A654 File Offset: 0x00038854
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float2 zy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float2(this.z, this.y);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.z = value.x;
				this.y = value.y;
			}
		}

		// Token: 0x170005BE RID: 1470
		// (get) Token: 0x0600149D RID: 5277 RVA: 0x0003A66E File Offset: 0x0003886E
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float2 zz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float2(this.z, this.z);
			}
		}

		// Token: 0x170005BF RID: 1471
		// (get) Token: 0x0600149E RID: 5278 RVA: 0x0003A681 File Offset: 0x00038881
		// (set) Token: 0x0600149F RID: 5279 RVA: 0x0003A694 File Offset: 0x00038894
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float2 zw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float2(this.z, this.w);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.z = value.x;
				this.w = value.y;
			}
		}

		// Token: 0x170005C0 RID: 1472
		// (get) Token: 0x060014A0 RID: 5280 RVA: 0x0003A6AE File Offset: 0x000388AE
		// (set) Token: 0x060014A1 RID: 5281 RVA: 0x0003A6C1 File Offset: 0x000388C1
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float2 wx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float2(this.w, this.x);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.w = value.x;
				this.x = value.y;
			}
		}

		// Token: 0x170005C1 RID: 1473
		// (get) Token: 0x060014A2 RID: 5282 RVA: 0x0003A6DB File Offset: 0x000388DB
		// (set) Token: 0x060014A3 RID: 5283 RVA: 0x0003A6EE File Offset: 0x000388EE
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float2 wy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float2(this.w, this.y);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.w = value.x;
				this.y = value.y;
			}
		}

		// Token: 0x170005C2 RID: 1474
		// (get) Token: 0x060014A4 RID: 5284 RVA: 0x0003A708 File Offset: 0x00038908
		// (set) Token: 0x060014A5 RID: 5285 RVA: 0x0003A71B File Offset: 0x0003891B
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float2 wz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float2(this.w, this.z);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.w = value.x;
				this.z = value.y;
			}
		}

		// Token: 0x170005C3 RID: 1475
		// (get) Token: 0x060014A6 RID: 5286 RVA: 0x0003A735 File Offset: 0x00038935
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float2 ww
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float2(this.w, this.w);
			}
		}

		// Token: 0x170005C4 RID: 1476
		public unsafe float this[int index]
		{
			get
			{
				fixed (float4* ptr = &this)
				{
					return ((float*)ptr)[index];
				}
			}
			set
			{
				fixed (float* ptr = &this.x)
				{
					ptr[index] = value;
				}
			}
		}

		// Token: 0x060014A9 RID: 5289 RVA: 0x0003A780 File Offset: 0x00038980
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool Equals(float4 rhs)
		{
			return this.x == rhs.x && this.y == rhs.y && this.z == rhs.z && this.w == rhs.w;
		}

		// Token: 0x060014AA RID: 5290 RVA: 0x0003A7BC File Offset: 0x000389BC
		public override bool Equals(object o)
		{
			if (o is float4)
			{
				float4 rhs = (float4)o;
				return this.Equals(rhs);
			}
			return false;
		}

		// Token: 0x060014AB RID: 5291 RVA: 0x0003A7E1 File Offset: 0x000389E1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override int GetHashCode()
		{
			return (int)math.hash(this);
		}

		// Token: 0x060014AC RID: 5292 RVA: 0x0003A7F0 File Offset: 0x000389F0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override string ToString()
		{
			return string.Format("float4({0}f, {1}f, {2}f, {3}f)", new object[]
			{
				this.x,
				this.y,
				this.z,
				this.w
			});
		}

		// Token: 0x060014AD RID: 5293 RVA: 0x0003A848 File Offset: 0x00038A48
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public string ToString(string format, IFormatProvider formatProvider)
		{
			return string.Format("float4({0}f, {1}f, {2}f, {3}f)", new object[]
			{
				this.x.ToString(format, formatProvider),
				this.y.ToString(format, formatProvider),
				this.z.ToString(format, formatProvider),
				this.w.ToString(format, formatProvider)
			});
		}

		// Token: 0x060014AE RID: 5294 RVA: 0x0003A8A5 File Offset: 0x00038AA5
		public static implicit operator float4(Vector4 v)
		{
			return new float4(v.x, v.y, v.z, v.w);
		}

		// Token: 0x060014AF RID: 5295 RVA: 0x0003A8C4 File Offset: 0x00038AC4
		public static implicit operator Vector4(float4 v)
		{
			return new Vector4(v.x, v.y, v.z, v.w);
		}

		// Token: 0x0400008F RID: 143
		public float x;

		// Token: 0x04000090 RID: 144
		public float y;

		// Token: 0x04000091 RID: 145
		public float z;

		// Token: 0x04000092 RID: 146
		public float w;

		// Token: 0x04000093 RID: 147
		public static readonly float4 zero;

		// Token: 0x02000059 RID: 89
		internal sealed class DebuggerProxy
		{
			// Token: 0x0600246F RID: 9327 RVA: 0x000675D8 File Offset: 0x000657D8
			public DebuggerProxy(float4 v)
			{
				this.x = v.x;
				this.y = v.y;
				this.z = v.z;
				this.w = v.w;
			}

			// Token: 0x0400014A RID: 330
			public float x;

			// Token: 0x0400014B RID: 331
			public float y;

			// Token: 0x0400014C RID: 332
			public float z;

			// Token: 0x0400014D RID: 333
			public float w;
		}
	}
}

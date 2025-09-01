using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;

namespace Unity.Mathematics
{
	// Token: 0x02000020 RID: 32
	[DebuggerTypeProxy(typeof(float3.DebuggerProxy))]
	[Il2CppEagerStaticClassConstruction]
	[Serializable]
	public struct float3 : IEquatable<float3>, IFormattable
	{
		// Token: 0x06001137 RID: 4407 RVA: 0x00032245 File Offset: 0x00030445
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float3(float x, float y, float z)
		{
			this.x = x;
			this.y = y;
			this.z = z;
		}

		// Token: 0x06001138 RID: 4408 RVA: 0x0003225C File Offset: 0x0003045C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float3(float x, float2 yz)
		{
			this.x = x;
			this.y = yz.x;
			this.z = yz.y;
		}

		// Token: 0x06001139 RID: 4409 RVA: 0x0003227D File Offset: 0x0003047D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float3(float2 xy, float z)
		{
			this.x = xy.x;
			this.y = xy.y;
			this.z = z;
		}

		// Token: 0x0600113A RID: 4410 RVA: 0x0003229E File Offset: 0x0003049E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float3(float3 xyz)
		{
			this.x = xyz.x;
			this.y = xyz.y;
			this.z = xyz.z;
		}

		// Token: 0x0600113B RID: 4411 RVA: 0x000322C4 File Offset: 0x000304C4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float3(float v)
		{
			this.x = v;
			this.y = v;
			this.z = v;
		}

		// Token: 0x0600113C RID: 4412 RVA: 0x000322DC File Offset: 0x000304DC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float3(bool v)
		{
			this.x = (v ? 1f : 0f);
			this.y = (v ? 1f : 0f);
			this.z = (v ? 1f : 0f);
		}

		// Token: 0x0600113D RID: 4413 RVA: 0x00032328 File Offset: 0x00030528
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float3(bool3 v)
		{
			this.x = (v.x ? 1f : 0f);
			this.y = (v.y ? 1f : 0f);
			this.z = (v.z ? 1f : 0f);
		}

		// Token: 0x0600113E RID: 4414 RVA: 0x00032383 File Offset: 0x00030583
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float3(int v)
		{
			this.x = (float)v;
			this.y = (float)v;
			this.z = (float)v;
		}

		// Token: 0x0600113F RID: 4415 RVA: 0x0003239D File Offset: 0x0003059D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float3(int3 v)
		{
			this.x = (float)v.x;
			this.y = (float)v.y;
			this.z = (float)v.z;
		}

		// Token: 0x06001140 RID: 4416 RVA: 0x000323C6 File Offset: 0x000305C6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float3(uint v)
		{
			this.x = v;
			this.y = v;
			this.z = v;
		}

		// Token: 0x06001141 RID: 4417 RVA: 0x000323E3 File Offset: 0x000305E3
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float3(uint3 v)
		{
			this.x = v.x;
			this.y = v.y;
			this.z = v.z;
		}

		// Token: 0x06001142 RID: 4418 RVA: 0x0003240F File Offset: 0x0003060F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float3(half v)
		{
			this.x = v;
			this.y = v;
			this.z = v;
		}

		// Token: 0x06001143 RID: 4419 RVA: 0x00032435 File Offset: 0x00030635
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float3(half3 v)
		{
			this.x = v.x;
			this.y = v.y;
			this.z = v.z;
		}

		// Token: 0x06001144 RID: 4420 RVA: 0x0003246A File Offset: 0x0003066A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float3(double v)
		{
			this.x = (float)v;
			this.y = (float)v;
			this.z = (float)v;
		}

		// Token: 0x06001145 RID: 4421 RVA: 0x00032484 File Offset: 0x00030684
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float3(double3 v)
		{
			this.x = (float)v.x;
			this.y = (float)v.y;
			this.z = (float)v.z;
		}

		// Token: 0x06001146 RID: 4422 RVA: 0x000324AD File Offset: 0x000306AD
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator float3(float v)
		{
			return new float3(v);
		}

		// Token: 0x06001147 RID: 4423 RVA: 0x000324B5 File Offset: 0x000306B5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator float3(bool v)
		{
			return new float3(v);
		}

		// Token: 0x06001148 RID: 4424 RVA: 0x000324BD File Offset: 0x000306BD
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator float3(bool3 v)
		{
			return new float3(v);
		}

		// Token: 0x06001149 RID: 4425 RVA: 0x000324C5 File Offset: 0x000306C5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator float3(int v)
		{
			return new float3(v);
		}

		// Token: 0x0600114A RID: 4426 RVA: 0x000324CD File Offset: 0x000306CD
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator float3(int3 v)
		{
			return new float3(v);
		}

		// Token: 0x0600114B RID: 4427 RVA: 0x000324D5 File Offset: 0x000306D5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator float3(uint v)
		{
			return new float3(v);
		}

		// Token: 0x0600114C RID: 4428 RVA: 0x000324DD File Offset: 0x000306DD
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator float3(uint3 v)
		{
			return new float3(v);
		}

		// Token: 0x0600114D RID: 4429 RVA: 0x000324E5 File Offset: 0x000306E5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator float3(half v)
		{
			return new float3(v);
		}

		// Token: 0x0600114E RID: 4430 RVA: 0x000324ED File Offset: 0x000306ED
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator float3(half3 v)
		{
			return new float3(v);
		}

		// Token: 0x0600114F RID: 4431 RVA: 0x000324F5 File Offset: 0x000306F5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator float3(double v)
		{
			return new float3(v);
		}

		// Token: 0x06001150 RID: 4432 RVA: 0x000324FD File Offset: 0x000306FD
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator float3(double3 v)
		{
			return new float3(v);
		}

		// Token: 0x06001151 RID: 4433 RVA: 0x00032505 File Offset: 0x00030705
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3 operator *(float3 lhs, float3 rhs)
		{
			return new float3(lhs.x * rhs.x, lhs.y * rhs.y, lhs.z * rhs.z);
		}

		// Token: 0x06001152 RID: 4434 RVA: 0x00032533 File Offset: 0x00030733
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3 operator *(float3 lhs, float rhs)
		{
			return new float3(lhs.x * rhs, lhs.y * rhs, lhs.z * rhs);
		}

		// Token: 0x06001153 RID: 4435 RVA: 0x00032552 File Offset: 0x00030752
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3 operator *(float lhs, float3 rhs)
		{
			return new float3(lhs * rhs.x, lhs * rhs.y, lhs * rhs.z);
		}

		// Token: 0x06001154 RID: 4436 RVA: 0x00032571 File Offset: 0x00030771
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3 operator +(float3 lhs, float3 rhs)
		{
			return new float3(lhs.x + rhs.x, lhs.y + rhs.y, lhs.z + rhs.z);
		}

		// Token: 0x06001155 RID: 4437 RVA: 0x0003259F File Offset: 0x0003079F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3 operator +(float3 lhs, float rhs)
		{
			return new float3(lhs.x + rhs, lhs.y + rhs, lhs.z + rhs);
		}

		// Token: 0x06001156 RID: 4438 RVA: 0x000325BE File Offset: 0x000307BE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3 operator +(float lhs, float3 rhs)
		{
			return new float3(lhs + rhs.x, lhs + rhs.y, lhs + rhs.z);
		}

		// Token: 0x06001157 RID: 4439 RVA: 0x000325DD File Offset: 0x000307DD
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3 operator -(float3 lhs, float3 rhs)
		{
			return new float3(lhs.x - rhs.x, lhs.y - rhs.y, lhs.z - rhs.z);
		}

		// Token: 0x06001158 RID: 4440 RVA: 0x0003260B File Offset: 0x0003080B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3 operator -(float3 lhs, float rhs)
		{
			return new float3(lhs.x - rhs, lhs.y - rhs, lhs.z - rhs);
		}

		// Token: 0x06001159 RID: 4441 RVA: 0x0003262A File Offset: 0x0003082A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3 operator -(float lhs, float3 rhs)
		{
			return new float3(lhs - rhs.x, lhs - rhs.y, lhs - rhs.z);
		}

		// Token: 0x0600115A RID: 4442 RVA: 0x00032649 File Offset: 0x00030849
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3 operator /(float3 lhs, float3 rhs)
		{
			return new float3(lhs.x / rhs.x, lhs.y / rhs.y, lhs.z / rhs.z);
		}

		// Token: 0x0600115B RID: 4443 RVA: 0x00032677 File Offset: 0x00030877
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3 operator /(float3 lhs, float rhs)
		{
			return new float3(lhs.x / rhs, lhs.y / rhs, lhs.z / rhs);
		}

		// Token: 0x0600115C RID: 4444 RVA: 0x00032696 File Offset: 0x00030896
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3 operator /(float lhs, float3 rhs)
		{
			return new float3(lhs / rhs.x, lhs / rhs.y, lhs / rhs.z);
		}

		// Token: 0x0600115D RID: 4445 RVA: 0x000326B5 File Offset: 0x000308B5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3 operator %(float3 lhs, float3 rhs)
		{
			return new float3(lhs.x % rhs.x, lhs.y % rhs.y, lhs.z % rhs.z);
		}

		// Token: 0x0600115E RID: 4446 RVA: 0x000326E3 File Offset: 0x000308E3
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3 operator %(float3 lhs, float rhs)
		{
			return new float3(lhs.x % rhs, lhs.y % rhs, lhs.z % rhs);
		}

		// Token: 0x0600115F RID: 4447 RVA: 0x00032702 File Offset: 0x00030902
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3 operator %(float lhs, float3 rhs)
		{
			return new float3(lhs % rhs.x, lhs % rhs.y, lhs % rhs.z);
		}

		// Token: 0x06001160 RID: 4448 RVA: 0x00032724 File Offset: 0x00030924
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3 operator ++(float3 val)
		{
			float num = val.x + 1f;
			val.x = num;
			float num2 = num;
			num = val.y + 1f;
			val.y = num;
			float num3 = num;
			num = val.z + 1f;
			val.z = num;
			return new float3(num2, num3, num);
		}

		// Token: 0x06001161 RID: 4449 RVA: 0x00032770 File Offset: 0x00030970
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3 operator --(float3 val)
		{
			float num = val.x - 1f;
			val.x = num;
			float num2 = num;
			num = val.y - 1f;
			val.y = num;
			float num3 = num;
			num = val.z - 1f;
			val.z = num;
			return new float3(num2, num3, num);
		}

		// Token: 0x06001162 RID: 4450 RVA: 0x000327BB File Offset: 0x000309BB
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3 operator <(float3 lhs, float3 rhs)
		{
			return new bool3(lhs.x < rhs.x, lhs.y < rhs.y, lhs.z < rhs.z);
		}

		// Token: 0x06001163 RID: 4451 RVA: 0x000327EC File Offset: 0x000309EC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3 operator <(float3 lhs, float rhs)
		{
			return new bool3(lhs.x < rhs, lhs.y < rhs, lhs.z < rhs);
		}

		// Token: 0x06001164 RID: 4452 RVA: 0x0003280E File Offset: 0x00030A0E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3 operator <(float lhs, float3 rhs)
		{
			return new bool3(lhs < rhs.x, lhs < rhs.y, lhs < rhs.z);
		}

		// Token: 0x06001165 RID: 4453 RVA: 0x00032830 File Offset: 0x00030A30
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3 operator <=(float3 lhs, float3 rhs)
		{
			return new bool3(lhs.x <= rhs.x, lhs.y <= rhs.y, lhs.z <= rhs.z);
		}

		// Token: 0x06001166 RID: 4454 RVA: 0x0003286A File Offset: 0x00030A6A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3 operator <=(float3 lhs, float rhs)
		{
			return new bool3(lhs.x <= rhs, lhs.y <= rhs, lhs.z <= rhs);
		}

		// Token: 0x06001167 RID: 4455 RVA: 0x00032895 File Offset: 0x00030A95
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3 operator <=(float lhs, float3 rhs)
		{
			return new bool3(lhs <= rhs.x, lhs <= rhs.y, lhs <= rhs.z);
		}

		// Token: 0x06001168 RID: 4456 RVA: 0x000328C0 File Offset: 0x00030AC0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3 operator >(float3 lhs, float3 rhs)
		{
			return new bool3(lhs.x > rhs.x, lhs.y > rhs.y, lhs.z > rhs.z);
		}

		// Token: 0x06001169 RID: 4457 RVA: 0x000328F1 File Offset: 0x00030AF1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3 operator >(float3 lhs, float rhs)
		{
			return new bool3(lhs.x > rhs, lhs.y > rhs, lhs.z > rhs);
		}

		// Token: 0x0600116A RID: 4458 RVA: 0x00032913 File Offset: 0x00030B13
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3 operator >(float lhs, float3 rhs)
		{
			return new bool3(lhs > rhs.x, lhs > rhs.y, lhs > rhs.z);
		}

		// Token: 0x0600116B RID: 4459 RVA: 0x00032935 File Offset: 0x00030B35
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3 operator >=(float3 lhs, float3 rhs)
		{
			return new bool3(lhs.x >= rhs.x, lhs.y >= rhs.y, lhs.z >= rhs.z);
		}

		// Token: 0x0600116C RID: 4460 RVA: 0x0003296F File Offset: 0x00030B6F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3 operator >=(float3 lhs, float rhs)
		{
			return new bool3(lhs.x >= rhs, lhs.y >= rhs, lhs.z >= rhs);
		}

		// Token: 0x0600116D RID: 4461 RVA: 0x0003299A File Offset: 0x00030B9A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3 operator >=(float lhs, float3 rhs)
		{
			return new bool3(lhs >= rhs.x, lhs >= rhs.y, lhs >= rhs.z);
		}

		// Token: 0x0600116E RID: 4462 RVA: 0x000329C5 File Offset: 0x00030BC5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3 operator -(float3 val)
		{
			return new float3(-val.x, -val.y, -val.z);
		}

		// Token: 0x0600116F RID: 4463 RVA: 0x000329E1 File Offset: 0x00030BE1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3 operator +(float3 val)
		{
			return new float3(val.x, val.y, val.z);
		}

		// Token: 0x06001170 RID: 4464 RVA: 0x000329FA File Offset: 0x00030BFA
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3 operator ==(float3 lhs, float3 rhs)
		{
			return new bool3(lhs.x == rhs.x, lhs.y == rhs.y, lhs.z == rhs.z);
		}

		// Token: 0x06001171 RID: 4465 RVA: 0x00032A2B File Offset: 0x00030C2B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3 operator ==(float3 lhs, float rhs)
		{
			return new bool3(lhs.x == rhs, lhs.y == rhs, lhs.z == rhs);
		}

		// Token: 0x06001172 RID: 4466 RVA: 0x00032A4D File Offset: 0x00030C4D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3 operator ==(float lhs, float3 rhs)
		{
			return new bool3(lhs == rhs.x, lhs == rhs.y, lhs == rhs.z);
		}

		// Token: 0x06001173 RID: 4467 RVA: 0x00032A6F File Offset: 0x00030C6F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3 operator !=(float3 lhs, float3 rhs)
		{
			return new bool3(lhs.x != rhs.x, lhs.y != rhs.y, lhs.z != rhs.z);
		}

		// Token: 0x06001174 RID: 4468 RVA: 0x00032AA9 File Offset: 0x00030CA9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3 operator !=(float3 lhs, float rhs)
		{
			return new bool3(lhs.x != rhs, lhs.y != rhs, lhs.z != rhs);
		}

		// Token: 0x06001175 RID: 4469 RVA: 0x00032AD4 File Offset: 0x00030CD4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3 operator !=(float lhs, float3 rhs)
		{
			return new bool3(lhs != rhs.x, lhs != rhs.y, lhs != rhs.z);
		}

		// Token: 0x170003FB RID: 1019
		// (get) Token: 0x06001176 RID: 4470 RVA: 0x00032AFF File Offset: 0x00030CFF
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 xxxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.x, this.x, this.x, this.x);
			}
		}

		// Token: 0x170003FC RID: 1020
		// (get) Token: 0x06001177 RID: 4471 RVA: 0x00032B1E File Offset: 0x00030D1E
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 xxxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.x, this.x, this.x, this.y);
			}
		}

		// Token: 0x170003FD RID: 1021
		// (get) Token: 0x06001178 RID: 4472 RVA: 0x00032B3D File Offset: 0x00030D3D
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 xxxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.x, this.x, this.x, this.z);
			}
		}

		// Token: 0x170003FE RID: 1022
		// (get) Token: 0x06001179 RID: 4473 RVA: 0x00032B5C File Offset: 0x00030D5C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 xxyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.x, this.x, this.y, this.x);
			}
		}

		// Token: 0x170003FF RID: 1023
		// (get) Token: 0x0600117A RID: 4474 RVA: 0x00032B7B File Offset: 0x00030D7B
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 xxyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.x, this.x, this.y, this.y);
			}
		}

		// Token: 0x17000400 RID: 1024
		// (get) Token: 0x0600117B RID: 4475 RVA: 0x00032B9A File Offset: 0x00030D9A
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 xxyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.x, this.x, this.y, this.z);
			}
		}

		// Token: 0x17000401 RID: 1025
		// (get) Token: 0x0600117C RID: 4476 RVA: 0x00032BB9 File Offset: 0x00030DB9
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 xxzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.x, this.x, this.z, this.x);
			}
		}

		// Token: 0x17000402 RID: 1026
		// (get) Token: 0x0600117D RID: 4477 RVA: 0x00032BD8 File Offset: 0x00030DD8
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 xxzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.x, this.x, this.z, this.y);
			}
		}

		// Token: 0x17000403 RID: 1027
		// (get) Token: 0x0600117E RID: 4478 RVA: 0x00032BF7 File Offset: 0x00030DF7
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 xxzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.x, this.x, this.z, this.z);
			}
		}

		// Token: 0x17000404 RID: 1028
		// (get) Token: 0x0600117F RID: 4479 RVA: 0x00032C16 File Offset: 0x00030E16
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 xyxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.x, this.y, this.x, this.x);
			}
		}

		// Token: 0x17000405 RID: 1029
		// (get) Token: 0x06001180 RID: 4480 RVA: 0x00032C35 File Offset: 0x00030E35
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 xyxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.x, this.y, this.x, this.y);
			}
		}

		// Token: 0x17000406 RID: 1030
		// (get) Token: 0x06001181 RID: 4481 RVA: 0x00032C54 File Offset: 0x00030E54
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 xyxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.x, this.y, this.x, this.z);
			}
		}

		// Token: 0x17000407 RID: 1031
		// (get) Token: 0x06001182 RID: 4482 RVA: 0x00032C73 File Offset: 0x00030E73
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 xyyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.x, this.y, this.y, this.x);
			}
		}

		// Token: 0x17000408 RID: 1032
		// (get) Token: 0x06001183 RID: 4483 RVA: 0x00032C92 File Offset: 0x00030E92
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 xyyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.x, this.y, this.y, this.y);
			}
		}

		// Token: 0x17000409 RID: 1033
		// (get) Token: 0x06001184 RID: 4484 RVA: 0x00032CB1 File Offset: 0x00030EB1
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 xyyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.x, this.y, this.y, this.z);
			}
		}

		// Token: 0x1700040A RID: 1034
		// (get) Token: 0x06001185 RID: 4485 RVA: 0x00032CD0 File Offset: 0x00030ED0
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 xyzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.x, this.y, this.z, this.x);
			}
		}

		// Token: 0x1700040B RID: 1035
		// (get) Token: 0x06001186 RID: 4486 RVA: 0x00032CEF File Offset: 0x00030EEF
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 xyzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.x, this.y, this.z, this.y);
			}
		}

		// Token: 0x1700040C RID: 1036
		// (get) Token: 0x06001187 RID: 4487 RVA: 0x00032D0E File Offset: 0x00030F0E
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 xyzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.x, this.y, this.z, this.z);
			}
		}

		// Token: 0x1700040D RID: 1037
		// (get) Token: 0x06001188 RID: 4488 RVA: 0x00032D2D File Offset: 0x00030F2D
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 xzxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.x, this.z, this.x, this.x);
			}
		}

		// Token: 0x1700040E RID: 1038
		// (get) Token: 0x06001189 RID: 4489 RVA: 0x00032D4C File Offset: 0x00030F4C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 xzxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.x, this.z, this.x, this.y);
			}
		}

		// Token: 0x1700040F RID: 1039
		// (get) Token: 0x0600118A RID: 4490 RVA: 0x00032D6B File Offset: 0x00030F6B
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 xzxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.x, this.z, this.x, this.z);
			}
		}

		// Token: 0x17000410 RID: 1040
		// (get) Token: 0x0600118B RID: 4491 RVA: 0x00032D8A File Offset: 0x00030F8A
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 xzyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.x, this.z, this.y, this.x);
			}
		}

		// Token: 0x17000411 RID: 1041
		// (get) Token: 0x0600118C RID: 4492 RVA: 0x00032DA9 File Offset: 0x00030FA9
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 xzyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.x, this.z, this.y, this.y);
			}
		}

		// Token: 0x17000412 RID: 1042
		// (get) Token: 0x0600118D RID: 4493 RVA: 0x00032DC8 File Offset: 0x00030FC8
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 xzyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.x, this.z, this.y, this.z);
			}
		}

		// Token: 0x17000413 RID: 1043
		// (get) Token: 0x0600118E RID: 4494 RVA: 0x00032DE7 File Offset: 0x00030FE7
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 xzzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.x, this.z, this.z, this.x);
			}
		}

		// Token: 0x17000414 RID: 1044
		// (get) Token: 0x0600118F RID: 4495 RVA: 0x00032E06 File Offset: 0x00031006
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 xzzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.x, this.z, this.z, this.y);
			}
		}

		// Token: 0x17000415 RID: 1045
		// (get) Token: 0x06001190 RID: 4496 RVA: 0x00032E25 File Offset: 0x00031025
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 xzzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.x, this.z, this.z, this.z);
			}
		}

		// Token: 0x17000416 RID: 1046
		// (get) Token: 0x06001191 RID: 4497 RVA: 0x00032E44 File Offset: 0x00031044
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 yxxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.y, this.x, this.x, this.x);
			}
		}

		// Token: 0x17000417 RID: 1047
		// (get) Token: 0x06001192 RID: 4498 RVA: 0x00032E63 File Offset: 0x00031063
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 yxxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.y, this.x, this.x, this.y);
			}
		}

		// Token: 0x17000418 RID: 1048
		// (get) Token: 0x06001193 RID: 4499 RVA: 0x00032E82 File Offset: 0x00031082
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 yxxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.y, this.x, this.x, this.z);
			}
		}

		// Token: 0x17000419 RID: 1049
		// (get) Token: 0x06001194 RID: 4500 RVA: 0x00032EA1 File Offset: 0x000310A1
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 yxyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.y, this.x, this.y, this.x);
			}
		}

		// Token: 0x1700041A RID: 1050
		// (get) Token: 0x06001195 RID: 4501 RVA: 0x00032EC0 File Offset: 0x000310C0
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 yxyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.y, this.x, this.y, this.y);
			}
		}

		// Token: 0x1700041B RID: 1051
		// (get) Token: 0x06001196 RID: 4502 RVA: 0x00032EDF File Offset: 0x000310DF
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 yxyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.y, this.x, this.y, this.z);
			}
		}

		// Token: 0x1700041C RID: 1052
		// (get) Token: 0x06001197 RID: 4503 RVA: 0x00032EFE File Offset: 0x000310FE
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 yxzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.y, this.x, this.z, this.x);
			}
		}

		// Token: 0x1700041D RID: 1053
		// (get) Token: 0x06001198 RID: 4504 RVA: 0x00032F1D File Offset: 0x0003111D
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 yxzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.y, this.x, this.z, this.y);
			}
		}

		// Token: 0x1700041E RID: 1054
		// (get) Token: 0x06001199 RID: 4505 RVA: 0x00032F3C File Offset: 0x0003113C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 yxzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.y, this.x, this.z, this.z);
			}
		}

		// Token: 0x1700041F RID: 1055
		// (get) Token: 0x0600119A RID: 4506 RVA: 0x00032F5B File Offset: 0x0003115B
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 yyxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.y, this.y, this.x, this.x);
			}
		}

		// Token: 0x17000420 RID: 1056
		// (get) Token: 0x0600119B RID: 4507 RVA: 0x00032F7A File Offset: 0x0003117A
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 yyxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.y, this.y, this.x, this.y);
			}
		}

		// Token: 0x17000421 RID: 1057
		// (get) Token: 0x0600119C RID: 4508 RVA: 0x00032F99 File Offset: 0x00031199
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 yyxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.y, this.y, this.x, this.z);
			}
		}

		// Token: 0x17000422 RID: 1058
		// (get) Token: 0x0600119D RID: 4509 RVA: 0x00032FB8 File Offset: 0x000311B8
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 yyyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.y, this.y, this.y, this.x);
			}
		}

		// Token: 0x17000423 RID: 1059
		// (get) Token: 0x0600119E RID: 4510 RVA: 0x00032FD7 File Offset: 0x000311D7
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 yyyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.y, this.y, this.y, this.y);
			}
		}

		// Token: 0x17000424 RID: 1060
		// (get) Token: 0x0600119F RID: 4511 RVA: 0x00032FF6 File Offset: 0x000311F6
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 yyyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.y, this.y, this.y, this.z);
			}
		}

		// Token: 0x17000425 RID: 1061
		// (get) Token: 0x060011A0 RID: 4512 RVA: 0x00033015 File Offset: 0x00031215
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 yyzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.y, this.y, this.z, this.x);
			}
		}

		// Token: 0x17000426 RID: 1062
		// (get) Token: 0x060011A1 RID: 4513 RVA: 0x00033034 File Offset: 0x00031234
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 yyzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.y, this.y, this.z, this.y);
			}
		}

		// Token: 0x17000427 RID: 1063
		// (get) Token: 0x060011A2 RID: 4514 RVA: 0x00033053 File Offset: 0x00031253
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 yyzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.y, this.y, this.z, this.z);
			}
		}

		// Token: 0x17000428 RID: 1064
		// (get) Token: 0x060011A3 RID: 4515 RVA: 0x00033072 File Offset: 0x00031272
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 yzxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.y, this.z, this.x, this.x);
			}
		}

		// Token: 0x17000429 RID: 1065
		// (get) Token: 0x060011A4 RID: 4516 RVA: 0x00033091 File Offset: 0x00031291
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 yzxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.y, this.z, this.x, this.y);
			}
		}

		// Token: 0x1700042A RID: 1066
		// (get) Token: 0x060011A5 RID: 4517 RVA: 0x000330B0 File Offset: 0x000312B0
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 yzxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.y, this.z, this.x, this.z);
			}
		}

		// Token: 0x1700042B RID: 1067
		// (get) Token: 0x060011A6 RID: 4518 RVA: 0x000330CF File Offset: 0x000312CF
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 yzyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.y, this.z, this.y, this.x);
			}
		}

		// Token: 0x1700042C RID: 1068
		// (get) Token: 0x060011A7 RID: 4519 RVA: 0x000330EE File Offset: 0x000312EE
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 yzyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.y, this.z, this.y, this.y);
			}
		}

		// Token: 0x1700042D RID: 1069
		// (get) Token: 0x060011A8 RID: 4520 RVA: 0x0003310D File Offset: 0x0003130D
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 yzyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.y, this.z, this.y, this.z);
			}
		}

		// Token: 0x1700042E RID: 1070
		// (get) Token: 0x060011A9 RID: 4521 RVA: 0x0003312C File Offset: 0x0003132C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 yzzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.y, this.z, this.z, this.x);
			}
		}

		// Token: 0x1700042F RID: 1071
		// (get) Token: 0x060011AA RID: 4522 RVA: 0x0003314B File Offset: 0x0003134B
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 yzzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.y, this.z, this.z, this.y);
			}
		}

		// Token: 0x17000430 RID: 1072
		// (get) Token: 0x060011AB RID: 4523 RVA: 0x0003316A File Offset: 0x0003136A
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 yzzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.y, this.z, this.z, this.z);
			}
		}

		// Token: 0x17000431 RID: 1073
		// (get) Token: 0x060011AC RID: 4524 RVA: 0x00033189 File Offset: 0x00031389
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 zxxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.z, this.x, this.x, this.x);
			}
		}

		// Token: 0x17000432 RID: 1074
		// (get) Token: 0x060011AD RID: 4525 RVA: 0x000331A8 File Offset: 0x000313A8
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 zxxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.z, this.x, this.x, this.y);
			}
		}

		// Token: 0x17000433 RID: 1075
		// (get) Token: 0x060011AE RID: 4526 RVA: 0x000331C7 File Offset: 0x000313C7
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 zxxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.z, this.x, this.x, this.z);
			}
		}

		// Token: 0x17000434 RID: 1076
		// (get) Token: 0x060011AF RID: 4527 RVA: 0x000331E6 File Offset: 0x000313E6
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 zxyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.z, this.x, this.y, this.x);
			}
		}

		// Token: 0x17000435 RID: 1077
		// (get) Token: 0x060011B0 RID: 4528 RVA: 0x00033205 File Offset: 0x00031405
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 zxyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.z, this.x, this.y, this.y);
			}
		}

		// Token: 0x17000436 RID: 1078
		// (get) Token: 0x060011B1 RID: 4529 RVA: 0x00033224 File Offset: 0x00031424
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 zxyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.z, this.x, this.y, this.z);
			}
		}

		// Token: 0x17000437 RID: 1079
		// (get) Token: 0x060011B2 RID: 4530 RVA: 0x00033243 File Offset: 0x00031443
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 zxzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.z, this.x, this.z, this.x);
			}
		}

		// Token: 0x17000438 RID: 1080
		// (get) Token: 0x060011B3 RID: 4531 RVA: 0x00033262 File Offset: 0x00031462
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 zxzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.z, this.x, this.z, this.y);
			}
		}

		// Token: 0x17000439 RID: 1081
		// (get) Token: 0x060011B4 RID: 4532 RVA: 0x00033281 File Offset: 0x00031481
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 zxzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.z, this.x, this.z, this.z);
			}
		}

		// Token: 0x1700043A RID: 1082
		// (get) Token: 0x060011B5 RID: 4533 RVA: 0x000332A0 File Offset: 0x000314A0
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 zyxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.z, this.y, this.x, this.x);
			}
		}

		// Token: 0x1700043B RID: 1083
		// (get) Token: 0x060011B6 RID: 4534 RVA: 0x000332BF File Offset: 0x000314BF
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 zyxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.z, this.y, this.x, this.y);
			}
		}

		// Token: 0x1700043C RID: 1084
		// (get) Token: 0x060011B7 RID: 4535 RVA: 0x000332DE File Offset: 0x000314DE
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 zyxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.z, this.y, this.x, this.z);
			}
		}

		// Token: 0x1700043D RID: 1085
		// (get) Token: 0x060011B8 RID: 4536 RVA: 0x000332FD File Offset: 0x000314FD
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 zyyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.z, this.y, this.y, this.x);
			}
		}

		// Token: 0x1700043E RID: 1086
		// (get) Token: 0x060011B9 RID: 4537 RVA: 0x0003331C File Offset: 0x0003151C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 zyyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.z, this.y, this.y, this.y);
			}
		}

		// Token: 0x1700043F RID: 1087
		// (get) Token: 0x060011BA RID: 4538 RVA: 0x0003333B File Offset: 0x0003153B
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 zyyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.z, this.y, this.y, this.z);
			}
		}

		// Token: 0x17000440 RID: 1088
		// (get) Token: 0x060011BB RID: 4539 RVA: 0x0003335A File Offset: 0x0003155A
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 zyzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.z, this.y, this.z, this.x);
			}
		}

		// Token: 0x17000441 RID: 1089
		// (get) Token: 0x060011BC RID: 4540 RVA: 0x00033379 File Offset: 0x00031579
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 zyzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.z, this.y, this.z, this.y);
			}
		}

		// Token: 0x17000442 RID: 1090
		// (get) Token: 0x060011BD RID: 4541 RVA: 0x00033398 File Offset: 0x00031598
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 zyzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.z, this.y, this.z, this.z);
			}
		}

		// Token: 0x17000443 RID: 1091
		// (get) Token: 0x060011BE RID: 4542 RVA: 0x000333B7 File Offset: 0x000315B7
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 zzxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.z, this.z, this.x, this.x);
			}
		}

		// Token: 0x17000444 RID: 1092
		// (get) Token: 0x060011BF RID: 4543 RVA: 0x000333D6 File Offset: 0x000315D6
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 zzxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.z, this.z, this.x, this.y);
			}
		}

		// Token: 0x17000445 RID: 1093
		// (get) Token: 0x060011C0 RID: 4544 RVA: 0x000333F5 File Offset: 0x000315F5
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 zzxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.z, this.z, this.x, this.z);
			}
		}

		// Token: 0x17000446 RID: 1094
		// (get) Token: 0x060011C1 RID: 4545 RVA: 0x00033414 File Offset: 0x00031614
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 zzyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.z, this.z, this.y, this.x);
			}
		}

		// Token: 0x17000447 RID: 1095
		// (get) Token: 0x060011C2 RID: 4546 RVA: 0x00033433 File Offset: 0x00031633
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 zzyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.z, this.z, this.y, this.y);
			}
		}

		// Token: 0x17000448 RID: 1096
		// (get) Token: 0x060011C3 RID: 4547 RVA: 0x00033452 File Offset: 0x00031652
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 zzyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.z, this.z, this.y, this.z);
			}
		}

		// Token: 0x17000449 RID: 1097
		// (get) Token: 0x060011C4 RID: 4548 RVA: 0x00033471 File Offset: 0x00031671
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 zzzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.z, this.z, this.z, this.x);
			}
		}

		// Token: 0x1700044A RID: 1098
		// (get) Token: 0x060011C5 RID: 4549 RVA: 0x00033490 File Offset: 0x00031690
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 zzzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.z, this.z, this.z, this.y);
			}
		}

		// Token: 0x1700044B RID: 1099
		// (get) Token: 0x060011C6 RID: 4550 RVA: 0x000334AF File Offset: 0x000316AF
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 zzzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.z, this.z, this.z, this.z);
			}
		}

		// Token: 0x1700044C RID: 1100
		// (get) Token: 0x060011C7 RID: 4551 RVA: 0x000334CE File Offset: 0x000316CE
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float3 xxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float3(this.x, this.x, this.x);
			}
		}

		// Token: 0x1700044D RID: 1101
		// (get) Token: 0x060011C8 RID: 4552 RVA: 0x000334E7 File Offset: 0x000316E7
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float3 xxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float3(this.x, this.x, this.y);
			}
		}

		// Token: 0x1700044E RID: 1102
		// (get) Token: 0x060011C9 RID: 4553 RVA: 0x00033500 File Offset: 0x00031700
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float3 xxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float3(this.x, this.x, this.z);
			}
		}

		// Token: 0x1700044F RID: 1103
		// (get) Token: 0x060011CA RID: 4554 RVA: 0x00033519 File Offset: 0x00031719
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float3 xyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float3(this.x, this.y, this.x);
			}
		}

		// Token: 0x17000450 RID: 1104
		// (get) Token: 0x060011CB RID: 4555 RVA: 0x00033532 File Offset: 0x00031732
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float3 xyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float3(this.x, this.y, this.y);
			}
		}

		// Token: 0x17000451 RID: 1105
		// (get) Token: 0x060011CC RID: 4556 RVA: 0x0003354B File Offset: 0x0003174B
		// (set) Token: 0x060011CD RID: 4557 RVA: 0x00033564 File Offset: 0x00031764
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

		// Token: 0x17000452 RID: 1106
		// (get) Token: 0x060011CE RID: 4558 RVA: 0x0003358A File Offset: 0x0003178A
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float3 xzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float3(this.x, this.z, this.x);
			}
		}

		// Token: 0x17000453 RID: 1107
		// (get) Token: 0x060011CF RID: 4559 RVA: 0x000335A3 File Offset: 0x000317A3
		// (set) Token: 0x060011D0 RID: 4560 RVA: 0x000335BC File Offset: 0x000317BC
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

		// Token: 0x17000454 RID: 1108
		// (get) Token: 0x060011D1 RID: 4561 RVA: 0x000335E2 File Offset: 0x000317E2
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float3 xzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float3(this.x, this.z, this.z);
			}
		}

		// Token: 0x17000455 RID: 1109
		// (get) Token: 0x060011D2 RID: 4562 RVA: 0x000335FB File Offset: 0x000317FB
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float3 yxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float3(this.y, this.x, this.x);
			}
		}

		// Token: 0x17000456 RID: 1110
		// (get) Token: 0x060011D3 RID: 4563 RVA: 0x00033614 File Offset: 0x00031814
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float3 yxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float3(this.y, this.x, this.y);
			}
		}

		// Token: 0x17000457 RID: 1111
		// (get) Token: 0x060011D4 RID: 4564 RVA: 0x0003362D File Offset: 0x0003182D
		// (set) Token: 0x060011D5 RID: 4565 RVA: 0x00033646 File Offset: 0x00031846
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

		// Token: 0x17000458 RID: 1112
		// (get) Token: 0x060011D6 RID: 4566 RVA: 0x0003366C File Offset: 0x0003186C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float3 yyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float3(this.y, this.y, this.x);
			}
		}

		// Token: 0x17000459 RID: 1113
		// (get) Token: 0x060011D7 RID: 4567 RVA: 0x00033685 File Offset: 0x00031885
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float3 yyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float3(this.y, this.y, this.y);
			}
		}

		// Token: 0x1700045A RID: 1114
		// (get) Token: 0x060011D8 RID: 4568 RVA: 0x0003369E File Offset: 0x0003189E
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float3 yyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float3(this.y, this.y, this.z);
			}
		}

		// Token: 0x1700045B RID: 1115
		// (get) Token: 0x060011D9 RID: 4569 RVA: 0x000336B7 File Offset: 0x000318B7
		// (set) Token: 0x060011DA RID: 4570 RVA: 0x000336D0 File Offset: 0x000318D0
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

		// Token: 0x1700045C RID: 1116
		// (get) Token: 0x060011DB RID: 4571 RVA: 0x000336F6 File Offset: 0x000318F6
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float3 yzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float3(this.y, this.z, this.y);
			}
		}

		// Token: 0x1700045D RID: 1117
		// (get) Token: 0x060011DC RID: 4572 RVA: 0x0003370F File Offset: 0x0003190F
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float3 yzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float3(this.y, this.z, this.z);
			}
		}

		// Token: 0x1700045E RID: 1118
		// (get) Token: 0x060011DD RID: 4573 RVA: 0x00033728 File Offset: 0x00031928
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float3 zxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float3(this.z, this.x, this.x);
			}
		}

		// Token: 0x1700045F RID: 1119
		// (get) Token: 0x060011DE RID: 4574 RVA: 0x00033741 File Offset: 0x00031941
		// (set) Token: 0x060011DF RID: 4575 RVA: 0x0003375A File Offset: 0x0003195A
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

		// Token: 0x17000460 RID: 1120
		// (get) Token: 0x060011E0 RID: 4576 RVA: 0x00033780 File Offset: 0x00031980
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float3 zxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float3(this.z, this.x, this.z);
			}
		}

		// Token: 0x17000461 RID: 1121
		// (get) Token: 0x060011E1 RID: 4577 RVA: 0x00033799 File Offset: 0x00031999
		// (set) Token: 0x060011E2 RID: 4578 RVA: 0x000337B2 File Offset: 0x000319B2
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

		// Token: 0x17000462 RID: 1122
		// (get) Token: 0x060011E3 RID: 4579 RVA: 0x000337D8 File Offset: 0x000319D8
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float3 zyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float3(this.z, this.y, this.y);
			}
		}

		// Token: 0x17000463 RID: 1123
		// (get) Token: 0x060011E4 RID: 4580 RVA: 0x000337F1 File Offset: 0x000319F1
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float3 zyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float3(this.z, this.y, this.z);
			}
		}

		// Token: 0x17000464 RID: 1124
		// (get) Token: 0x060011E5 RID: 4581 RVA: 0x0003380A File Offset: 0x00031A0A
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float3 zzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float3(this.z, this.z, this.x);
			}
		}

		// Token: 0x17000465 RID: 1125
		// (get) Token: 0x060011E6 RID: 4582 RVA: 0x00033823 File Offset: 0x00031A23
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float3 zzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float3(this.z, this.z, this.y);
			}
		}

		// Token: 0x17000466 RID: 1126
		// (get) Token: 0x060011E7 RID: 4583 RVA: 0x0003383C File Offset: 0x00031A3C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float3 zzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float3(this.z, this.z, this.z);
			}
		}

		// Token: 0x17000467 RID: 1127
		// (get) Token: 0x060011E8 RID: 4584 RVA: 0x00033855 File Offset: 0x00031A55
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float2 xx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float2(this.x, this.x);
			}
		}

		// Token: 0x17000468 RID: 1128
		// (get) Token: 0x060011E9 RID: 4585 RVA: 0x00033868 File Offset: 0x00031A68
		// (set) Token: 0x060011EA RID: 4586 RVA: 0x0003387B File Offset: 0x00031A7B
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

		// Token: 0x17000469 RID: 1129
		// (get) Token: 0x060011EB RID: 4587 RVA: 0x00033895 File Offset: 0x00031A95
		// (set) Token: 0x060011EC RID: 4588 RVA: 0x000338A8 File Offset: 0x00031AA8
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

		// Token: 0x1700046A RID: 1130
		// (get) Token: 0x060011ED RID: 4589 RVA: 0x000338C2 File Offset: 0x00031AC2
		// (set) Token: 0x060011EE RID: 4590 RVA: 0x000338D5 File Offset: 0x00031AD5
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

		// Token: 0x1700046B RID: 1131
		// (get) Token: 0x060011EF RID: 4591 RVA: 0x000338EF File Offset: 0x00031AEF
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float2 yy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float2(this.y, this.y);
			}
		}

		// Token: 0x1700046C RID: 1132
		// (get) Token: 0x060011F0 RID: 4592 RVA: 0x00033902 File Offset: 0x00031B02
		// (set) Token: 0x060011F1 RID: 4593 RVA: 0x00033915 File Offset: 0x00031B15
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

		// Token: 0x1700046D RID: 1133
		// (get) Token: 0x060011F2 RID: 4594 RVA: 0x0003392F File Offset: 0x00031B2F
		// (set) Token: 0x060011F3 RID: 4595 RVA: 0x00033942 File Offset: 0x00031B42
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

		// Token: 0x1700046E RID: 1134
		// (get) Token: 0x060011F4 RID: 4596 RVA: 0x0003395C File Offset: 0x00031B5C
		// (set) Token: 0x060011F5 RID: 4597 RVA: 0x0003396F File Offset: 0x00031B6F
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

		// Token: 0x1700046F RID: 1135
		// (get) Token: 0x060011F6 RID: 4598 RVA: 0x00033989 File Offset: 0x00031B89
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float2 zz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float2(this.z, this.z);
			}
		}

		// Token: 0x17000470 RID: 1136
		public unsafe float this[int index]
		{
			get
			{
				fixed (float3* ptr = &this)
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

		// Token: 0x060011F9 RID: 4601 RVA: 0x000339D4 File Offset: 0x00031BD4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool Equals(float3 rhs)
		{
			return this.x == rhs.x && this.y == rhs.y && this.z == rhs.z;
		}

		// Token: 0x060011FA RID: 4602 RVA: 0x00033A04 File Offset: 0x00031C04
		public override bool Equals(object o)
		{
			if (o is float3)
			{
				float3 rhs = (float3)o;
				return this.Equals(rhs);
			}
			return false;
		}

		// Token: 0x060011FB RID: 4603 RVA: 0x00033A29 File Offset: 0x00031C29
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override int GetHashCode()
		{
			return (int)math.hash(this);
		}

		// Token: 0x060011FC RID: 4604 RVA: 0x00033A36 File Offset: 0x00031C36
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override string ToString()
		{
			return string.Format("float3({0}f, {1}f, {2}f)", this.x, this.y, this.z);
		}

		// Token: 0x060011FD RID: 4605 RVA: 0x00033A63 File Offset: 0x00031C63
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public string ToString(string format, IFormatProvider formatProvider)
		{
			return string.Format("float3({0}f, {1}f, {2}f)", this.x.ToString(format, formatProvider), this.y.ToString(format, formatProvider), this.z.ToString(format, formatProvider));
		}

		// Token: 0x060011FE RID: 4606 RVA: 0x00033A96 File Offset: 0x00031C96
		public static implicit operator Vector3(float3 v)
		{
			return new Vector3(v.x, v.y, v.z);
		}

		// Token: 0x060011FF RID: 4607 RVA: 0x00033AAF File Offset: 0x00031CAF
		public static implicit operator float3(Vector3 v)
		{
			return new float3(v.x, v.y, v.z);
		}

		// Token: 0x0400007E RID: 126
		public float x;

		// Token: 0x0400007F RID: 127
		public float y;

		// Token: 0x04000080 RID: 128
		public float z;

		// Token: 0x04000081 RID: 129
		public static readonly float3 zero;

		// Token: 0x02000058 RID: 88
		internal sealed class DebuggerProxy
		{
			// Token: 0x0600246E RID: 9326 RVA: 0x000675AC File Offset: 0x000657AC
			public DebuggerProxy(float3 v)
			{
				this.x = v.x;
				this.y = v.y;
				this.z = v.z;
			}

			// Token: 0x04000147 RID: 327
			public float x;

			// Token: 0x04000148 RID: 328
			public float y;

			// Token: 0x04000149 RID: 329
			public float z;
		}
	}
}

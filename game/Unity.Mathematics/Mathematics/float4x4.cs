using System;
using System.Runtime.CompilerServices;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;

namespace Unity.Mathematics
{
	// Token: 0x02000027 RID: 39
	[Il2CppEagerStaticClassConstruction]
	[Serializable]
	public struct float4x4 : IEquatable<float4x4>, IFormattable
	{
		// Token: 0x0600152E RID: 5422 RVA: 0x0003BEB9 File Offset: 0x0003A0B9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float4x4(float4 c0, float4 c1, float4 c2, float4 c3)
		{
			this.c0 = c0;
			this.c1 = c1;
			this.c2 = c2;
			this.c3 = c3;
		}

		// Token: 0x0600152F RID: 5423 RVA: 0x0003BED8 File Offset: 0x0003A0D8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float4x4(float m00, float m01, float m02, float m03, float m10, float m11, float m12, float m13, float m20, float m21, float m22, float m23, float m30, float m31, float m32, float m33)
		{
			this.c0 = new float4(m00, m10, m20, m30);
			this.c1 = new float4(m01, m11, m21, m31);
			this.c2 = new float4(m02, m12, m22, m32);
			this.c3 = new float4(m03, m13, m23, m33);
		}

		// Token: 0x06001530 RID: 5424 RVA: 0x0003BF2E File Offset: 0x0003A12E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float4x4(float v)
		{
			this.c0 = v;
			this.c1 = v;
			this.c2 = v;
			this.c3 = v;
		}

		// Token: 0x06001531 RID: 5425 RVA: 0x0003BF60 File Offset: 0x0003A160
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float4x4(bool v)
		{
			this.c0 = math.select(new float4(0f), new float4(1f), v);
			this.c1 = math.select(new float4(0f), new float4(1f), v);
			this.c2 = math.select(new float4(0f), new float4(1f), v);
			this.c3 = math.select(new float4(0f), new float4(1f), v);
		}

		// Token: 0x06001532 RID: 5426 RVA: 0x0003BFF0 File Offset: 0x0003A1F0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float4x4(bool4x4 v)
		{
			this.c0 = math.select(new float4(0f), new float4(1f), v.c0);
			this.c1 = math.select(new float4(0f), new float4(1f), v.c1);
			this.c2 = math.select(new float4(0f), new float4(1f), v.c2);
			this.c3 = math.select(new float4(0f), new float4(1f), v.c3);
		}

		// Token: 0x06001533 RID: 5427 RVA: 0x0003C091 File Offset: 0x0003A291
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float4x4(int v)
		{
			this.c0 = v;
			this.c1 = v;
			this.c2 = v;
			this.c3 = v;
		}

		// Token: 0x06001534 RID: 5428 RVA: 0x0003C0C4 File Offset: 0x0003A2C4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float4x4(int4x4 v)
		{
			this.c0 = v.c0;
			this.c1 = v.c1;
			this.c2 = v.c2;
			this.c3 = v.c3;
		}

		// Token: 0x06001535 RID: 5429 RVA: 0x0003C115 File Offset: 0x0003A315
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float4x4(uint v)
		{
			this.c0 = v;
			this.c1 = v;
			this.c2 = v;
			this.c3 = v;
		}

		// Token: 0x06001536 RID: 5430 RVA: 0x0003C148 File Offset: 0x0003A348
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float4x4(uint4x4 v)
		{
			this.c0 = v.c0;
			this.c1 = v.c1;
			this.c2 = v.c2;
			this.c3 = v.c3;
		}

		// Token: 0x06001537 RID: 5431 RVA: 0x0003C199 File Offset: 0x0003A399
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float4x4(double v)
		{
			this.c0 = (float4)v;
			this.c1 = (float4)v;
			this.c2 = (float4)v;
			this.c3 = (float4)v;
		}

		// Token: 0x06001538 RID: 5432 RVA: 0x0003C1CC File Offset: 0x0003A3CC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float4x4(double4x4 v)
		{
			this.c0 = (float4)v.c0;
			this.c1 = (float4)v.c1;
			this.c2 = (float4)v.c2;
			this.c3 = (float4)v.c3;
		}

		// Token: 0x06001539 RID: 5433 RVA: 0x0003C21D File Offset: 0x0003A41D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator float4x4(float v)
		{
			return new float4x4(v);
		}

		// Token: 0x0600153A RID: 5434 RVA: 0x0003C225 File Offset: 0x0003A425
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator float4x4(bool v)
		{
			return new float4x4(v);
		}

		// Token: 0x0600153B RID: 5435 RVA: 0x0003C22D File Offset: 0x0003A42D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator float4x4(bool4x4 v)
		{
			return new float4x4(v);
		}

		// Token: 0x0600153C RID: 5436 RVA: 0x0003C235 File Offset: 0x0003A435
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator float4x4(int v)
		{
			return new float4x4(v);
		}

		// Token: 0x0600153D RID: 5437 RVA: 0x0003C23D File Offset: 0x0003A43D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator float4x4(int4x4 v)
		{
			return new float4x4(v);
		}

		// Token: 0x0600153E RID: 5438 RVA: 0x0003C245 File Offset: 0x0003A445
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator float4x4(uint v)
		{
			return new float4x4(v);
		}

		// Token: 0x0600153F RID: 5439 RVA: 0x0003C24D File Offset: 0x0003A44D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator float4x4(uint4x4 v)
		{
			return new float4x4(v);
		}

		// Token: 0x06001540 RID: 5440 RVA: 0x0003C255 File Offset: 0x0003A455
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator float4x4(double v)
		{
			return new float4x4(v);
		}

		// Token: 0x06001541 RID: 5441 RVA: 0x0003C25D File Offset: 0x0003A45D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator float4x4(double4x4 v)
		{
			return new float4x4(v);
		}

		// Token: 0x06001542 RID: 5442 RVA: 0x0003C268 File Offset: 0x0003A468
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4x4 operator *(float4x4 lhs, float4x4 rhs)
		{
			return new float4x4(lhs.c0 * rhs.c0, lhs.c1 * rhs.c1, lhs.c2 * rhs.c2, lhs.c3 * rhs.c3);
		}

		// Token: 0x06001543 RID: 5443 RVA: 0x0003C2BE File Offset: 0x0003A4BE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4x4 operator *(float4x4 lhs, float rhs)
		{
			return new float4x4(lhs.c0 * rhs, lhs.c1 * rhs, lhs.c2 * rhs, lhs.c3 * rhs);
		}

		// Token: 0x06001544 RID: 5444 RVA: 0x0003C2F5 File Offset: 0x0003A4F5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4x4 operator *(float lhs, float4x4 rhs)
		{
			return new float4x4(lhs * rhs.c0, lhs * rhs.c1, lhs * rhs.c2, lhs * rhs.c3);
		}

		// Token: 0x06001545 RID: 5445 RVA: 0x0003C32C File Offset: 0x0003A52C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4x4 operator +(float4x4 lhs, float4x4 rhs)
		{
			return new float4x4(lhs.c0 + rhs.c0, lhs.c1 + rhs.c1, lhs.c2 + rhs.c2, lhs.c3 + rhs.c3);
		}

		// Token: 0x06001546 RID: 5446 RVA: 0x0003C382 File Offset: 0x0003A582
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4x4 operator +(float4x4 lhs, float rhs)
		{
			return new float4x4(lhs.c0 + rhs, lhs.c1 + rhs, lhs.c2 + rhs, lhs.c3 + rhs);
		}

		// Token: 0x06001547 RID: 5447 RVA: 0x0003C3B9 File Offset: 0x0003A5B9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4x4 operator +(float lhs, float4x4 rhs)
		{
			return new float4x4(lhs + rhs.c0, lhs + rhs.c1, lhs + rhs.c2, lhs + rhs.c3);
		}

		// Token: 0x06001548 RID: 5448 RVA: 0x0003C3F0 File Offset: 0x0003A5F0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4x4 operator -(float4x4 lhs, float4x4 rhs)
		{
			return new float4x4(lhs.c0 - rhs.c0, lhs.c1 - rhs.c1, lhs.c2 - rhs.c2, lhs.c3 - rhs.c3);
		}

		// Token: 0x06001549 RID: 5449 RVA: 0x0003C446 File Offset: 0x0003A646
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4x4 operator -(float4x4 lhs, float rhs)
		{
			return new float4x4(lhs.c0 - rhs, lhs.c1 - rhs, lhs.c2 - rhs, lhs.c3 - rhs);
		}

		// Token: 0x0600154A RID: 5450 RVA: 0x0003C47D File Offset: 0x0003A67D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4x4 operator -(float lhs, float4x4 rhs)
		{
			return new float4x4(lhs - rhs.c0, lhs - rhs.c1, lhs - rhs.c2, lhs - rhs.c3);
		}

		// Token: 0x0600154B RID: 5451 RVA: 0x0003C4B4 File Offset: 0x0003A6B4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4x4 operator /(float4x4 lhs, float4x4 rhs)
		{
			return new float4x4(lhs.c0 / rhs.c0, lhs.c1 / rhs.c1, lhs.c2 / rhs.c2, lhs.c3 / rhs.c3);
		}

		// Token: 0x0600154C RID: 5452 RVA: 0x0003C50A File Offset: 0x0003A70A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4x4 operator /(float4x4 lhs, float rhs)
		{
			return new float4x4(lhs.c0 / rhs, lhs.c1 / rhs, lhs.c2 / rhs, lhs.c3 / rhs);
		}

		// Token: 0x0600154D RID: 5453 RVA: 0x0003C541 File Offset: 0x0003A741
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4x4 operator /(float lhs, float4x4 rhs)
		{
			return new float4x4(lhs / rhs.c0, lhs / rhs.c1, lhs / rhs.c2, lhs / rhs.c3);
		}

		// Token: 0x0600154E RID: 5454 RVA: 0x0003C578 File Offset: 0x0003A778
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4x4 operator %(float4x4 lhs, float4x4 rhs)
		{
			return new float4x4(lhs.c0 % rhs.c0, lhs.c1 % rhs.c1, lhs.c2 % rhs.c2, lhs.c3 % rhs.c3);
		}

		// Token: 0x0600154F RID: 5455 RVA: 0x0003C5CE File Offset: 0x0003A7CE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4x4 operator %(float4x4 lhs, float rhs)
		{
			return new float4x4(lhs.c0 % rhs, lhs.c1 % rhs, lhs.c2 % rhs, lhs.c3 % rhs);
		}

		// Token: 0x06001550 RID: 5456 RVA: 0x0003C605 File Offset: 0x0003A805
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4x4 operator %(float lhs, float4x4 rhs)
		{
			return new float4x4(lhs % rhs.c0, lhs % rhs.c1, lhs % rhs.c2, lhs % rhs.c3);
		}

		// Token: 0x06001551 RID: 5457 RVA: 0x0003C63C File Offset: 0x0003A83C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4x4 operator ++(float4x4 val)
		{
			float4 @float = ++val.c0;
			val.c0 = @float;
			float4 float2 = @float;
			@float = ++val.c1;
			val.c1 = @float;
			float4 float3 = @float;
			@float = ++val.c2;
			val.c2 = @float;
			float4 float4 = @float;
			@float = ++val.c3;
			val.c3 = @float;
			return new float4x4(float2, float3, float4, @float);
		}

		// Token: 0x06001552 RID: 5458 RVA: 0x0003C6B8 File Offset: 0x0003A8B8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4x4 operator --(float4x4 val)
		{
			float4 @float = --val.c0;
			val.c0 = @float;
			float4 float2 = @float;
			@float = --val.c1;
			val.c1 = @float;
			float4 float3 = @float;
			@float = --val.c2;
			val.c2 = @float;
			float4 float4 = @float;
			@float = --val.c3;
			val.c3 = @float;
			return new float4x4(float2, float3, float4, @float);
		}

		// Token: 0x06001553 RID: 5459 RVA: 0x0003C734 File Offset: 0x0003A934
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x4 operator <(float4x4 lhs, float4x4 rhs)
		{
			return new bool4x4(lhs.c0 < rhs.c0, lhs.c1 < rhs.c1, lhs.c2 < rhs.c2, lhs.c3 < rhs.c3);
		}

		// Token: 0x06001554 RID: 5460 RVA: 0x0003C78A File Offset: 0x0003A98A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x4 operator <(float4x4 lhs, float rhs)
		{
			return new bool4x4(lhs.c0 < rhs, lhs.c1 < rhs, lhs.c2 < rhs, lhs.c3 < rhs);
		}

		// Token: 0x06001555 RID: 5461 RVA: 0x0003C7C1 File Offset: 0x0003A9C1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x4 operator <(float lhs, float4x4 rhs)
		{
			return new bool4x4(lhs < rhs.c0, lhs < rhs.c1, lhs < rhs.c2, lhs < rhs.c3);
		}

		// Token: 0x06001556 RID: 5462 RVA: 0x0003C7F8 File Offset: 0x0003A9F8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x4 operator <=(float4x4 lhs, float4x4 rhs)
		{
			return new bool4x4(lhs.c0 <= rhs.c0, lhs.c1 <= rhs.c1, lhs.c2 <= rhs.c2, lhs.c3 <= rhs.c3);
		}

		// Token: 0x06001557 RID: 5463 RVA: 0x0003C84E File Offset: 0x0003AA4E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x4 operator <=(float4x4 lhs, float rhs)
		{
			return new bool4x4(lhs.c0 <= rhs, lhs.c1 <= rhs, lhs.c2 <= rhs, lhs.c3 <= rhs);
		}

		// Token: 0x06001558 RID: 5464 RVA: 0x0003C885 File Offset: 0x0003AA85
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x4 operator <=(float lhs, float4x4 rhs)
		{
			return new bool4x4(lhs <= rhs.c0, lhs <= rhs.c1, lhs <= rhs.c2, lhs <= rhs.c3);
		}

		// Token: 0x06001559 RID: 5465 RVA: 0x0003C8BC File Offset: 0x0003AABC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x4 operator >(float4x4 lhs, float4x4 rhs)
		{
			return new bool4x4(lhs.c0 > rhs.c0, lhs.c1 > rhs.c1, lhs.c2 > rhs.c2, lhs.c3 > rhs.c3);
		}

		// Token: 0x0600155A RID: 5466 RVA: 0x0003C912 File Offset: 0x0003AB12
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x4 operator >(float4x4 lhs, float rhs)
		{
			return new bool4x4(lhs.c0 > rhs, lhs.c1 > rhs, lhs.c2 > rhs, lhs.c3 > rhs);
		}

		// Token: 0x0600155B RID: 5467 RVA: 0x0003C949 File Offset: 0x0003AB49
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x4 operator >(float lhs, float4x4 rhs)
		{
			return new bool4x4(lhs > rhs.c0, lhs > rhs.c1, lhs > rhs.c2, lhs > rhs.c3);
		}

		// Token: 0x0600155C RID: 5468 RVA: 0x0003C980 File Offset: 0x0003AB80
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x4 operator >=(float4x4 lhs, float4x4 rhs)
		{
			return new bool4x4(lhs.c0 >= rhs.c0, lhs.c1 >= rhs.c1, lhs.c2 >= rhs.c2, lhs.c3 >= rhs.c3);
		}

		// Token: 0x0600155D RID: 5469 RVA: 0x0003C9D6 File Offset: 0x0003ABD6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x4 operator >=(float4x4 lhs, float rhs)
		{
			return new bool4x4(lhs.c0 >= rhs, lhs.c1 >= rhs, lhs.c2 >= rhs, lhs.c3 >= rhs);
		}

		// Token: 0x0600155E RID: 5470 RVA: 0x0003CA0D File Offset: 0x0003AC0D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x4 operator >=(float lhs, float4x4 rhs)
		{
			return new bool4x4(lhs >= rhs.c0, lhs >= rhs.c1, lhs >= rhs.c2, lhs >= rhs.c3);
		}

		// Token: 0x0600155F RID: 5471 RVA: 0x0003CA44 File Offset: 0x0003AC44
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4x4 operator -(float4x4 val)
		{
			return new float4x4(-val.c0, -val.c1, -val.c2, -val.c3);
		}

		// Token: 0x06001560 RID: 5472 RVA: 0x0003CA77 File Offset: 0x0003AC77
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4x4 operator +(float4x4 val)
		{
			return new float4x4(+val.c0, +val.c1, +val.c2, +val.c3);
		}

		// Token: 0x06001561 RID: 5473 RVA: 0x0003CAAC File Offset: 0x0003ACAC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x4 operator ==(float4x4 lhs, float4x4 rhs)
		{
			return new bool4x4(lhs.c0 == rhs.c0, lhs.c1 == rhs.c1, lhs.c2 == rhs.c2, lhs.c3 == rhs.c3);
		}

		// Token: 0x06001562 RID: 5474 RVA: 0x0003CB02 File Offset: 0x0003AD02
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x4 operator ==(float4x4 lhs, float rhs)
		{
			return new bool4x4(lhs.c0 == rhs, lhs.c1 == rhs, lhs.c2 == rhs, lhs.c3 == rhs);
		}

		// Token: 0x06001563 RID: 5475 RVA: 0x0003CB39 File Offset: 0x0003AD39
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x4 operator ==(float lhs, float4x4 rhs)
		{
			return new bool4x4(lhs == rhs.c0, lhs == rhs.c1, lhs == rhs.c2, lhs == rhs.c3);
		}

		// Token: 0x06001564 RID: 5476 RVA: 0x0003CB70 File Offset: 0x0003AD70
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x4 operator !=(float4x4 lhs, float4x4 rhs)
		{
			return new bool4x4(lhs.c0 != rhs.c0, lhs.c1 != rhs.c1, lhs.c2 != rhs.c2, lhs.c3 != rhs.c3);
		}

		// Token: 0x06001565 RID: 5477 RVA: 0x0003CBC6 File Offset: 0x0003ADC6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x4 operator !=(float4x4 lhs, float rhs)
		{
			return new bool4x4(lhs.c0 != rhs, lhs.c1 != rhs, lhs.c2 != rhs, lhs.c3 != rhs);
		}

		// Token: 0x06001566 RID: 5478 RVA: 0x0003CBFD File Offset: 0x0003ADFD
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x4 operator !=(float lhs, float4x4 rhs)
		{
			return new bool4x4(lhs != rhs.c0, lhs != rhs.c1, lhs != rhs.c2, lhs != rhs.c3);
		}

		// Token: 0x170005C7 RID: 1479
		public unsafe float4 this[int index]
		{
			get
			{
				fixed (float4x4* ptr = &this)
				{
					return ref *(float4*)(ptr + (IntPtr)index * (IntPtr)sizeof(float4) / (IntPtr)sizeof(float4x4));
				}
			}
		}

		// Token: 0x06001568 RID: 5480 RVA: 0x0003CC50 File Offset: 0x0003AE50
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool Equals(float4x4 rhs)
		{
			return this.c0.Equals(rhs.c0) && this.c1.Equals(rhs.c1) && this.c2.Equals(rhs.c2) && this.c3.Equals(rhs.c3);
		}

		// Token: 0x06001569 RID: 5481 RVA: 0x0003CCAC File Offset: 0x0003AEAC
		public override bool Equals(object o)
		{
			if (o is float4x4)
			{
				float4x4 rhs = (float4x4)o;
				return this.Equals(rhs);
			}
			return false;
		}

		// Token: 0x0600156A RID: 5482 RVA: 0x0003CCD1 File Offset: 0x0003AED1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override int GetHashCode()
		{
			return (int)math.hash(this);
		}

		// Token: 0x0600156B RID: 5483 RVA: 0x0003CCE0 File Offset: 0x0003AEE0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override string ToString()
		{
			return string.Format("float4x4({0}f, {1}f, {2}f, {3}f,  {4}f, {5}f, {6}f, {7}f,  {8}f, {9}f, {10}f, {11}f,  {12}f, {13}f, {14}f, {15}f)", new object[]
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
				this.c3.z,
				this.c0.w,
				this.c1.w,
				this.c2.w,
				this.c3.w
			});
		}

		// Token: 0x0600156C RID: 5484 RVA: 0x0003CE38 File Offset: 0x0003B038
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public string ToString(string format, IFormatProvider formatProvider)
		{
			return string.Format("float4x4({0}f, {1}f, {2}f, {3}f,  {4}f, {5}f, {6}f, {7}f,  {8}f, {9}f, {10}f, {11}f,  {12}f, {13}f, {14}f, {15}f)", new object[]
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
				this.c3.z.ToString(format, formatProvider),
				this.c0.w.ToString(format, formatProvider),
				this.c1.w.ToString(format, formatProvider),
				this.c2.w.ToString(format, formatProvider),
				this.c3.w.ToString(format, formatProvider)
			});
		}

		// Token: 0x0600156D RID: 5485 RVA: 0x0003CFAD File Offset: 0x0003B1AD
		public static implicit operator float4x4(Matrix4x4 m)
		{
			return new float4x4(m.GetColumn(0), m.GetColumn(1), m.GetColumn(2), m.GetColumn(3));
		}

		// Token: 0x0600156E RID: 5486 RVA: 0x0003CFE8 File Offset: 0x0003B1E8
		public static implicit operator Matrix4x4(float4x4 m)
		{
			return new Matrix4x4(m.c0, m.c1, m.c2, m.c3);
		}

		// Token: 0x0600156F RID: 5487 RVA: 0x0003D01C File Offset: 0x0003B21C
		public float4x4(float3x3 rotation, float3 translation)
		{
			this.c0 = math.float4(rotation.c0, 0f);
			this.c1 = math.float4(rotation.c1, 0f);
			this.c2 = math.float4(rotation.c2, 0f);
			this.c3 = math.float4(translation, 1f);
		}

		// Token: 0x06001570 RID: 5488 RVA: 0x0003D07C File Offset: 0x0003B27C
		public float4x4(quaternion rotation, float3 translation)
		{
			float3x3 float3x = math.float3x3(rotation);
			this.c0 = math.float4(float3x.c0, 0f);
			this.c1 = math.float4(float3x.c1, 0f);
			this.c2 = math.float4(float3x.c2, 0f);
			this.c3 = math.float4(translation, 1f);
		}

		// Token: 0x06001571 RID: 5489 RVA: 0x0003D0E4 File Offset: 0x0003B2E4
		public float4x4(RigidTransform transform)
		{
			float3x3 float3x = math.float3x3(transform.rot);
			this.c0 = math.float4(float3x.c0, 0f);
			this.c1 = math.float4(float3x.c1, 0f);
			this.c2 = math.float4(float3x.c2, 0f);
			this.c3 = math.float4(transform.pos, 1f);
		}

		// Token: 0x06001572 RID: 5490 RVA: 0x0003D158 File Offset: 0x0003B358
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4x4 AxisAngle(float3 axis, float angle)
		{
			float rhs;
			float num;
			math.sincos(angle, out rhs, out num);
			float4 @float = math.float4(axis, 0f);
			float4 yzxx = @float.yzxx;
			float4 zxyx = @float.zxyx;
			float4 rhs2 = @float - @float * num;
			float4 float2 = math.float4(@float.xyz * rhs, num);
			uint4 rhs3 = math.uint4(0U, 0U, 2147483648U, 0U);
			uint4 rhs4 = math.uint4(2147483648U, 0U, 0U, 0U);
			uint4 rhs5 = math.uint4(0U, 2147483648U, 0U, 0U);
			uint4 rhs6 = math.uint4(uint.MaxValue, uint.MaxValue, uint.MaxValue, 0U);
			return math.float4x4(@float.x * rhs2 + math.asfloat((math.asuint(float2.wzyx) ^ rhs3) & rhs6), @float.y * rhs2 + math.asfloat((math.asuint(float2.zwxx) ^ rhs4) & rhs6), @float.z * rhs2 + math.asfloat((math.asuint(float2.yxwx) ^ rhs5) & rhs6), math.float4(0f, 0f, 0f, 1f));
		}

		// Token: 0x06001573 RID: 5491 RVA: 0x0003D294 File Offset: 0x0003B494
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4x4 EulerXYZ(float3 xyz)
		{
			float3 @float;
			float3 float2;
			math.sincos(xyz, out @float, out float2);
			return math.float4x4(float2.y * float2.z, float2.z * @float.x * @float.y - float2.x * @float.z, float2.x * float2.z * @float.y + @float.x * @float.z, 0f, float2.y * @float.z, float2.x * float2.z + @float.x * @float.y * @float.z, float2.x * @float.y * @float.z - float2.z * @float.x, 0f, -@float.y, float2.y * @float.x, float2.x * float2.y, 0f, 0f, 0f, 0f, 1f);
		}

		// Token: 0x06001574 RID: 5492 RVA: 0x0003D398 File Offset: 0x0003B598
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4x4 EulerXZY(float3 xyz)
		{
			float3 @float;
			float3 float2;
			math.sincos(xyz, out @float, out float2);
			return math.float4x4(float2.y * float2.z, @float.x * @float.y - float2.x * float2.y * @float.z, float2.x * @float.y + float2.y * @float.x * @float.z, 0f, @float.z, float2.x * float2.z, -float2.z * @float.x, 0f, -float2.z * @float.y, float2.y * @float.x + float2.x * @float.y * @float.z, float2.x * float2.y - @float.x * @float.y * @float.z, 0f, 0f, 0f, 0f, 1f);
		}

		// Token: 0x06001575 RID: 5493 RVA: 0x0003D49C File Offset: 0x0003B69C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4x4 EulerYXZ(float3 xyz)
		{
			float3 @float;
			float3 float2;
			math.sincos(xyz, out @float, out float2);
			return math.float4x4(float2.y * float2.z - @float.x * @float.y * @float.z, -float2.x * @float.z, float2.z * @float.y + float2.y * @float.x * @float.z, 0f, float2.z * @float.x * @float.y + float2.y * @float.z, float2.x * float2.z, @float.y * @float.z - float2.y * float2.z * @float.x, 0f, -float2.x * @float.y, @float.x, float2.x * float2.y, 0f, 0f, 0f, 0f, 1f);
		}

		// Token: 0x06001576 RID: 5494 RVA: 0x0003D5A0 File Offset: 0x0003B7A0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4x4 EulerYZX(float3 xyz)
		{
			float3 @float;
			float3 float2;
			math.sincos(xyz, out @float, out float2);
			return math.float4x4(float2.y * float2.z, -@float.z, float2.z * @float.y, 0f, @float.x * @float.y + float2.x * float2.y * @float.z, float2.x * float2.z, float2.x * @float.y * @float.z - float2.y * @float.x, 0f, float2.y * @float.x * @float.z - float2.x * @float.y, float2.z * @float.x, float2.x * float2.y + @float.x * @float.y * @float.z, 0f, 0f, 0f, 0f, 1f);
		}

		// Token: 0x06001577 RID: 5495 RVA: 0x0003D6A4 File Offset: 0x0003B8A4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4x4 EulerZXY(float3 xyz)
		{
			float3 @float;
			float3 float2;
			math.sincos(xyz, out @float, out float2);
			return math.float4x4(float2.y * float2.z + @float.x * @float.y * @float.z, float2.z * @float.x * @float.y - float2.y * @float.z, float2.x * @float.y, 0f, float2.x * @float.z, float2.x * float2.z, -@float.x, 0f, float2.y * @float.x * @float.z - float2.z * @float.y, float2.y * float2.z * @float.x + @float.y * @float.z, float2.x * float2.y, 0f, 0f, 0f, 0f, 1f);
		}

		// Token: 0x06001578 RID: 5496 RVA: 0x0003D7A8 File Offset: 0x0003B9A8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4x4 EulerZYX(float3 xyz)
		{
			float3 @float;
			float3 float2;
			math.sincos(xyz, out @float, out float2);
			return math.float4x4(float2.y * float2.z, -float2.y * @float.z, @float.y, 0f, float2.z * @float.x * @float.y + float2.x * @float.z, float2.x * float2.z - @float.x * @float.y * @float.z, -float2.y * @float.x, 0f, @float.x * @float.z - float2.x * float2.z * @float.y, float2.z * @float.x + float2.x * @float.y * @float.z, float2.x * float2.y, 0f, 0f, 0f, 0f, 1f);
		}

		// Token: 0x06001579 RID: 5497 RVA: 0x0003D8AB File Offset: 0x0003BAAB
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4x4 EulerXYZ(float x, float y, float z)
		{
			return float4x4.EulerXYZ(math.float3(x, y, z));
		}

		// Token: 0x0600157A RID: 5498 RVA: 0x0003D8BA File Offset: 0x0003BABA
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4x4 EulerXZY(float x, float y, float z)
		{
			return float4x4.EulerXZY(math.float3(x, y, z));
		}

		// Token: 0x0600157B RID: 5499 RVA: 0x0003D8C9 File Offset: 0x0003BAC9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4x4 EulerYXZ(float x, float y, float z)
		{
			return float4x4.EulerYXZ(math.float3(x, y, z));
		}

		// Token: 0x0600157C RID: 5500 RVA: 0x0003D8D8 File Offset: 0x0003BAD8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4x4 EulerYZX(float x, float y, float z)
		{
			return float4x4.EulerYZX(math.float3(x, y, z));
		}

		// Token: 0x0600157D RID: 5501 RVA: 0x0003D8E7 File Offset: 0x0003BAE7
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4x4 EulerZXY(float x, float y, float z)
		{
			return float4x4.EulerZXY(math.float3(x, y, z));
		}

		// Token: 0x0600157E RID: 5502 RVA: 0x0003D8F6 File Offset: 0x0003BAF6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4x4 EulerZYX(float x, float y, float z)
		{
			return float4x4.EulerZYX(math.float3(x, y, z));
		}

		// Token: 0x0600157F RID: 5503 RVA: 0x0003D908 File Offset: 0x0003BB08
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4x4 Euler(float3 xyz, math.RotationOrder order = math.RotationOrder.ZXY)
		{
			switch (order)
			{
			case math.RotationOrder.XYZ:
				return float4x4.EulerXYZ(xyz);
			case math.RotationOrder.XZY:
				return float4x4.EulerXZY(xyz);
			case math.RotationOrder.YXZ:
				return float4x4.EulerYXZ(xyz);
			case math.RotationOrder.YZX:
				return float4x4.EulerYZX(xyz);
			case math.RotationOrder.ZXY:
				return float4x4.EulerZXY(xyz);
			case math.RotationOrder.ZYX:
				return float4x4.EulerZYX(xyz);
			default:
				return float4x4.identity;
			}
		}

		// Token: 0x06001580 RID: 5504 RVA: 0x0003D964 File Offset: 0x0003BB64
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4x4 Euler(float x, float y, float z, math.RotationOrder order = math.RotationOrder.ZXY)
		{
			return float4x4.Euler(math.float3(x, y, z), order);
		}

		// Token: 0x06001581 RID: 5505 RVA: 0x0003D974 File Offset: 0x0003BB74
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4x4 RotateX(float angle)
		{
			float num;
			float num2;
			math.sincos(angle, out num, out num2);
			return math.float4x4(1f, 0f, 0f, 0f, 0f, num2, -num, 0f, 0f, num, num2, 0f, 0f, 0f, 0f, 1f);
		}

		// Token: 0x06001582 RID: 5506 RVA: 0x0003D9D4 File Offset: 0x0003BBD4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4x4 RotateY(float angle)
		{
			float num;
			float num2;
			math.sincos(angle, out num, out num2);
			return math.float4x4(num2, 0f, num, 0f, 0f, 1f, 0f, 0f, -num, 0f, num2, 0f, 0f, 0f, 0f, 1f);
		}

		// Token: 0x06001583 RID: 5507 RVA: 0x0003DA34 File Offset: 0x0003BC34
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4x4 RotateZ(float angle)
		{
			float num;
			float num2;
			math.sincos(angle, out num, out num2);
			return math.float4x4(num2, -num, 0f, 0f, num, num2, 0f, 0f, 0f, 0f, 1f, 0f, 0f, 0f, 0f, 1f);
		}

		// Token: 0x06001584 RID: 5508 RVA: 0x0003DA94 File Offset: 0x0003BC94
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4x4 Scale(float s)
		{
			return math.float4x4(s, 0f, 0f, 0f, 0f, s, 0f, 0f, 0f, 0f, s, 0f, 0f, 0f, 0f, 1f);
		}

		// Token: 0x06001585 RID: 5509 RVA: 0x0003DAEC File Offset: 0x0003BCEC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4x4 Scale(float x, float y, float z)
		{
			return math.float4x4(x, 0f, 0f, 0f, 0f, y, 0f, 0f, 0f, 0f, z, 0f, 0f, 0f, 0f, 1f);
		}

		// Token: 0x06001586 RID: 5510 RVA: 0x0003DB42 File Offset: 0x0003BD42
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4x4 Scale(float3 scales)
		{
			return float4x4.Scale(scales.x, scales.y, scales.z);
		}

		// Token: 0x06001587 RID: 5511 RVA: 0x0003DB5C File Offset: 0x0003BD5C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4x4 Translate(float3 vector)
		{
			return math.float4x4(math.float4(1f, 0f, 0f, 0f), math.float4(0f, 1f, 0f, 0f), math.float4(0f, 0f, 1f, 0f), math.float4(vector.x, vector.y, vector.z, 1f));
		}

		// Token: 0x06001588 RID: 5512 RVA: 0x0003DBD8 File Offset: 0x0003BDD8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4x4 LookAt(float3 eye, float3 target, float3 up)
		{
			float3x3 float3x = float3x3.LookRotation(math.normalize(target - eye), up);
			float4x4 result;
			result.c0 = math.float4(float3x.c0, 0f);
			result.c1 = math.float4(float3x.c1, 0f);
			result.c2 = math.float4(float3x.c2, 0f);
			result.c3 = math.float4(eye, 1f);
			return result;
		}

		// Token: 0x06001589 RID: 5513 RVA: 0x0003DC50 File Offset: 0x0003BE50
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4x4 Ortho(float width, float height, float near, float far)
		{
			float num = 1f / width;
			float num2 = 1f / height;
			float num3 = 1f / (far - near);
			return math.float4x4(2f * num, 0f, 0f, 0f, 0f, 2f * num2, 0f, 0f, 0f, 0f, -2f * num3, -(far + near) * num3, 0f, 0f, 0f, 1f);
		}

		// Token: 0x0600158A RID: 5514 RVA: 0x0003DCD4 File Offset: 0x0003BED4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4x4 OrthoOffCenter(float left, float right, float bottom, float top, float near, float far)
		{
			float num = 1f / (right - left);
			float num2 = 1f / (top - bottom);
			float num3 = 1f / (far - near);
			return math.float4x4(2f * num, 0f, 0f, -(right + left) * num, 0f, 2f * num2, 0f, -(top + bottom) * num2, 0f, 0f, -2f * num3, -(far + near) * num3, 0f, 0f, 0f, 1f);
		}

		// Token: 0x0600158B RID: 5515 RVA: 0x0003DD64 File Offset: 0x0003BF64
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4x4 PerspectiveFov(float verticalFov, float aspect, float near, float far)
		{
			float num = 1f / math.tan(verticalFov * 0.5f);
			float num2 = 1f / (near - far);
			return math.float4x4(num / aspect, 0f, 0f, 0f, 0f, num, 0f, 0f, 0f, 0f, (far + near) * num2, 2f * near * far * num2, 0f, 0f, -1f, 0f);
		}

		// Token: 0x0600158C RID: 5516 RVA: 0x0003DDE4 File Offset: 0x0003BFE4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4x4 PerspectiveOffCenter(float left, float right, float bottom, float top, float near, float far)
		{
			float num = 1f / (near - far);
			float num2 = 1f / (right - left);
			float num3 = 1f / (top - bottom);
			return math.float4x4(2f * near * num2, 0f, (left + right) * num2, 0f, 0f, 2f * near * num3, (bottom + top) * num3, 0f, 0f, 0f, (far + near) * num, 2f * near * far * num, 0f, 0f, -1f, 0f);
		}

		// Token: 0x0600158D RID: 5517 RVA: 0x0003DE7C File Offset: 0x0003C07C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4x4 TRS(float3 translation, quaternion rotation, float3 scale)
		{
			float3x3 float3x = math.float3x3(rotation);
			return math.float4x4(math.float4(float3x.c0 * scale.x, 0f), math.float4(float3x.c1 * scale.y, 0f), math.float4(float3x.c2 * scale.z, 0f), math.float4(translation, 1f));
		}

		// Token: 0x0600158E RID: 5518 RVA: 0x0003DEF4 File Offset: 0x0003C0F4
		// Note: this type is marked as 'beforefieldinit'.
		static float4x4()
		{
		}

		// Token: 0x0400009B RID: 155
		public float4 c0;

		// Token: 0x0400009C RID: 156
		public float4 c1;

		// Token: 0x0400009D RID: 157
		public float4 c2;

		// Token: 0x0400009E RID: 158
		public float4 c3;

		// Token: 0x0400009F RID: 159
		public static readonly float4x4 identity = new float4x4(1f, 0f, 0f, 0f, 0f, 1f, 0f, 0f, 0f, 0f, 1f, 0f, 0f, 0f, 0f, 1f);

		// Token: 0x040000A0 RID: 160
		public static readonly float4x4 zero;
	}
}

using System;
using System.Runtime.CompilerServices;
using Unity.IL2CPP.CompilerServices;

namespace Unity.Mathematics
{
	// Token: 0x02000026 RID: 38
	[Il2CppEagerStaticClassConstruction]
	[Serializable]
	public struct float4x3 : IEquatable<float4x3>, IFormattable
	{
		// Token: 0x060014EF RID: 5359 RVA: 0x0003B209 File Offset: 0x00039409
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float4x3(float4 c0, float4 c1, float4 c2)
		{
			this.c0 = c0;
			this.c1 = c1;
			this.c2 = c2;
		}

		// Token: 0x060014F0 RID: 5360 RVA: 0x0003B220 File Offset: 0x00039420
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float4x3(float m00, float m01, float m02, float m10, float m11, float m12, float m20, float m21, float m22, float m30, float m31, float m32)
		{
			this.c0 = new float4(m00, m10, m20, m30);
			this.c1 = new float4(m01, m11, m21, m31);
			this.c2 = new float4(m02, m12, m22, m32);
		}

		// Token: 0x060014F1 RID: 5361 RVA: 0x0003B258 File Offset: 0x00039458
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float4x3(float v)
		{
			this.c0 = v;
			this.c1 = v;
			this.c2 = v;
		}

		// Token: 0x060014F2 RID: 5362 RVA: 0x0003B280 File Offset: 0x00039480
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float4x3(bool v)
		{
			this.c0 = math.select(new float4(0f), new float4(1f), v);
			this.c1 = math.select(new float4(0f), new float4(1f), v);
			this.c2 = math.select(new float4(0f), new float4(1f), v);
		}

		// Token: 0x060014F3 RID: 5363 RVA: 0x0003B2F0 File Offset: 0x000394F0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float4x3(bool4x3 v)
		{
			this.c0 = math.select(new float4(0f), new float4(1f), v.c0);
			this.c1 = math.select(new float4(0f), new float4(1f), v.c1);
			this.c2 = math.select(new float4(0f), new float4(1f), v.c2);
		}

		// Token: 0x060014F4 RID: 5364 RVA: 0x0003B36C File Offset: 0x0003956C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float4x3(int v)
		{
			this.c0 = v;
			this.c1 = v;
			this.c2 = v;
		}

		// Token: 0x060014F5 RID: 5365 RVA: 0x0003B392 File Offset: 0x00039592
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float4x3(int4x3 v)
		{
			this.c0 = v.c0;
			this.c1 = v.c1;
			this.c2 = v.c2;
		}

		// Token: 0x060014F6 RID: 5366 RVA: 0x0003B3C7 File Offset: 0x000395C7
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float4x3(uint v)
		{
			this.c0 = v;
			this.c1 = v;
			this.c2 = v;
		}

		// Token: 0x060014F7 RID: 5367 RVA: 0x0003B3ED File Offset: 0x000395ED
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float4x3(uint4x3 v)
		{
			this.c0 = v.c0;
			this.c1 = v.c1;
			this.c2 = v.c2;
		}

		// Token: 0x060014F8 RID: 5368 RVA: 0x0003B422 File Offset: 0x00039622
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float4x3(double v)
		{
			this.c0 = (float4)v;
			this.c1 = (float4)v;
			this.c2 = (float4)v;
		}

		// Token: 0x060014F9 RID: 5369 RVA: 0x0003B448 File Offset: 0x00039648
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float4x3(double4x3 v)
		{
			this.c0 = (float4)v.c0;
			this.c1 = (float4)v.c1;
			this.c2 = (float4)v.c2;
		}

		// Token: 0x060014FA RID: 5370 RVA: 0x0003B47D File Offset: 0x0003967D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator float4x3(float v)
		{
			return new float4x3(v);
		}

		// Token: 0x060014FB RID: 5371 RVA: 0x0003B485 File Offset: 0x00039685
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator float4x3(bool v)
		{
			return new float4x3(v);
		}

		// Token: 0x060014FC RID: 5372 RVA: 0x0003B48D File Offset: 0x0003968D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator float4x3(bool4x3 v)
		{
			return new float4x3(v);
		}

		// Token: 0x060014FD RID: 5373 RVA: 0x0003B495 File Offset: 0x00039695
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator float4x3(int v)
		{
			return new float4x3(v);
		}

		// Token: 0x060014FE RID: 5374 RVA: 0x0003B49D File Offset: 0x0003969D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator float4x3(int4x3 v)
		{
			return new float4x3(v);
		}

		// Token: 0x060014FF RID: 5375 RVA: 0x0003B4A5 File Offset: 0x000396A5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator float4x3(uint v)
		{
			return new float4x3(v);
		}

		// Token: 0x06001500 RID: 5376 RVA: 0x0003B4AD File Offset: 0x000396AD
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator float4x3(uint4x3 v)
		{
			return new float4x3(v);
		}

		// Token: 0x06001501 RID: 5377 RVA: 0x0003B4B5 File Offset: 0x000396B5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator float4x3(double v)
		{
			return new float4x3(v);
		}

		// Token: 0x06001502 RID: 5378 RVA: 0x0003B4BD File Offset: 0x000396BD
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator float4x3(double4x3 v)
		{
			return new float4x3(v);
		}

		// Token: 0x06001503 RID: 5379 RVA: 0x0003B4C5 File Offset: 0x000396C5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4x3 operator *(float4x3 lhs, float4x3 rhs)
		{
			return new float4x3(lhs.c0 * rhs.c0, lhs.c1 * rhs.c1, lhs.c2 * rhs.c2);
		}

		// Token: 0x06001504 RID: 5380 RVA: 0x0003B4FF File Offset: 0x000396FF
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4x3 operator *(float4x3 lhs, float rhs)
		{
			return new float4x3(lhs.c0 * rhs, lhs.c1 * rhs, lhs.c2 * rhs);
		}

		// Token: 0x06001505 RID: 5381 RVA: 0x0003B52A File Offset: 0x0003972A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4x3 operator *(float lhs, float4x3 rhs)
		{
			return new float4x3(lhs * rhs.c0, lhs * rhs.c1, lhs * rhs.c2);
		}

		// Token: 0x06001506 RID: 5382 RVA: 0x0003B555 File Offset: 0x00039755
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4x3 operator +(float4x3 lhs, float4x3 rhs)
		{
			return new float4x3(lhs.c0 + rhs.c0, lhs.c1 + rhs.c1, lhs.c2 + rhs.c2);
		}

		// Token: 0x06001507 RID: 5383 RVA: 0x0003B58F File Offset: 0x0003978F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4x3 operator +(float4x3 lhs, float rhs)
		{
			return new float4x3(lhs.c0 + rhs, lhs.c1 + rhs, lhs.c2 + rhs);
		}

		// Token: 0x06001508 RID: 5384 RVA: 0x0003B5BA File Offset: 0x000397BA
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4x3 operator +(float lhs, float4x3 rhs)
		{
			return new float4x3(lhs + rhs.c0, lhs + rhs.c1, lhs + rhs.c2);
		}

		// Token: 0x06001509 RID: 5385 RVA: 0x0003B5E5 File Offset: 0x000397E5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4x3 operator -(float4x3 lhs, float4x3 rhs)
		{
			return new float4x3(lhs.c0 - rhs.c0, lhs.c1 - rhs.c1, lhs.c2 - rhs.c2);
		}

		// Token: 0x0600150A RID: 5386 RVA: 0x0003B61F File Offset: 0x0003981F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4x3 operator -(float4x3 lhs, float rhs)
		{
			return new float4x3(lhs.c0 - rhs, lhs.c1 - rhs, lhs.c2 - rhs);
		}

		// Token: 0x0600150B RID: 5387 RVA: 0x0003B64A File Offset: 0x0003984A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4x3 operator -(float lhs, float4x3 rhs)
		{
			return new float4x3(lhs - rhs.c0, lhs - rhs.c1, lhs - rhs.c2);
		}

		// Token: 0x0600150C RID: 5388 RVA: 0x0003B675 File Offset: 0x00039875
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4x3 operator /(float4x3 lhs, float4x3 rhs)
		{
			return new float4x3(lhs.c0 / rhs.c0, lhs.c1 / rhs.c1, lhs.c2 / rhs.c2);
		}

		// Token: 0x0600150D RID: 5389 RVA: 0x0003B6AF File Offset: 0x000398AF
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4x3 operator /(float4x3 lhs, float rhs)
		{
			return new float4x3(lhs.c0 / rhs, lhs.c1 / rhs, lhs.c2 / rhs);
		}

		// Token: 0x0600150E RID: 5390 RVA: 0x0003B6DA File Offset: 0x000398DA
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4x3 operator /(float lhs, float4x3 rhs)
		{
			return new float4x3(lhs / rhs.c0, lhs / rhs.c1, lhs / rhs.c2);
		}

		// Token: 0x0600150F RID: 5391 RVA: 0x0003B705 File Offset: 0x00039905
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4x3 operator %(float4x3 lhs, float4x3 rhs)
		{
			return new float4x3(lhs.c0 % rhs.c0, lhs.c1 % rhs.c1, lhs.c2 % rhs.c2);
		}

		// Token: 0x06001510 RID: 5392 RVA: 0x0003B73F File Offset: 0x0003993F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4x3 operator %(float4x3 lhs, float rhs)
		{
			return new float4x3(lhs.c0 % rhs, lhs.c1 % rhs, lhs.c2 % rhs);
		}

		// Token: 0x06001511 RID: 5393 RVA: 0x0003B76A File Offset: 0x0003996A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4x3 operator %(float lhs, float4x3 rhs)
		{
			return new float4x3(lhs % rhs.c0, lhs % rhs.c1, lhs % rhs.c2);
		}

		// Token: 0x06001512 RID: 5394 RVA: 0x0003B798 File Offset: 0x00039998
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4x3 operator ++(float4x3 val)
		{
			float4 @float = ++val.c0;
			val.c0 = @float;
			float4 float2 = @float;
			@float = ++val.c1;
			val.c1 = @float;
			float4 float3 = @float;
			@float = ++val.c2;
			val.c2 = @float;
			return new float4x3(float2, float3, @float);
		}

		// Token: 0x06001513 RID: 5395 RVA: 0x0003B7F8 File Offset: 0x000399F8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4x3 operator --(float4x3 val)
		{
			float4 @float = --val.c0;
			val.c0 = @float;
			float4 float2 = @float;
			@float = --val.c1;
			val.c1 = @float;
			float4 float3 = @float;
			@float = --val.c2;
			val.c2 = @float;
			return new float4x3(float2, float3, @float);
		}

		// Token: 0x06001514 RID: 5396 RVA: 0x0003B858 File Offset: 0x00039A58
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x3 operator <(float4x3 lhs, float4x3 rhs)
		{
			return new bool4x3(lhs.c0 < rhs.c0, lhs.c1 < rhs.c1, lhs.c2 < rhs.c2);
		}

		// Token: 0x06001515 RID: 5397 RVA: 0x0003B892 File Offset: 0x00039A92
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x3 operator <(float4x3 lhs, float rhs)
		{
			return new bool4x3(lhs.c0 < rhs, lhs.c1 < rhs, lhs.c2 < rhs);
		}

		// Token: 0x06001516 RID: 5398 RVA: 0x0003B8BD File Offset: 0x00039ABD
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x3 operator <(float lhs, float4x3 rhs)
		{
			return new bool4x3(lhs < rhs.c0, lhs < rhs.c1, lhs < rhs.c2);
		}

		// Token: 0x06001517 RID: 5399 RVA: 0x0003B8E8 File Offset: 0x00039AE8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x3 operator <=(float4x3 lhs, float4x3 rhs)
		{
			return new bool4x3(lhs.c0 <= rhs.c0, lhs.c1 <= rhs.c1, lhs.c2 <= rhs.c2);
		}

		// Token: 0x06001518 RID: 5400 RVA: 0x0003B922 File Offset: 0x00039B22
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x3 operator <=(float4x3 lhs, float rhs)
		{
			return new bool4x3(lhs.c0 <= rhs, lhs.c1 <= rhs, lhs.c2 <= rhs);
		}

		// Token: 0x06001519 RID: 5401 RVA: 0x0003B94D File Offset: 0x00039B4D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x3 operator <=(float lhs, float4x3 rhs)
		{
			return new bool4x3(lhs <= rhs.c0, lhs <= rhs.c1, lhs <= rhs.c2);
		}

		// Token: 0x0600151A RID: 5402 RVA: 0x0003B978 File Offset: 0x00039B78
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x3 operator >(float4x3 lhs, float4x3 rhs)
		{
			return new bool4x3(lhs.c0 > rhs.c0, lhs.c1 > rhs.c1, lhs.c2 > rhs.c2);
		}

		// Token: 0x0600151B RID: 5403 RVA: 0x0003B9B2 File Offset: 0x00039BB2
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x3 operator >(float4x3 lhs, float rhs)
		{
			return new bool4x3(lhs.c0 > rhs, lhs.c1 > rhs, lhs.c2 > rhs);
		}

		// Token: 0x0600151C RID: 5404 RVA: 0x0003B9DD File Offset: 0x00039BDD
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x3 operator >(float lhs, float4x3 rhs)
		{
			return new bool4x3(lhs > rhs.c0, lhs > rhs.c1, lhs > rhs.c2);
		}

		// Token: 0x0600151D RID: 5405 RVA: 0x0003BA08 File Offset: 0x00039C08
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x3 operator >=(float4x3 lhs, float4x3 rhs)
		{
			return new bool4x3(lhs.c0 >= rhs.c0, lhs.c1 >= rhs.c1, lhs.c2 >= rhs.c2);
		}

		// Token: 0x0600151E RID: 5406 RVA: 0x0003BA42 File Offset: 0x00039C42
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x3 operator >=(float4x3 lhs, float rhs)
		{
			return new bool4x3(lhs.c0 >= rhs, lhs.c1 >= rhs, lhs.c2 >= rhs);
		}

		// Token: 0x0600151F RID: 5407 RVA: 0x0003BA6D File Offset: 0x00039C6D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x3 operator >=(float lhs, float4x3 rhs)
		{
			return new bool4x3(lhs >= rhs.c0, lhs >= rhs.c1, lhs >= rhs.c2);
		}

		// Token: 0x06001520 RID: 5408 RVA: 0x0003BA98 File Offset: 0x00039C98
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4x3 operator -(float4x3 val)
		{
			return new float4x3(-val.c0, -val.c1, -val.c2);
		}

		// Token: 0x06001521 RID: 5409 RVA: 0x0003BAC0 File Offset: 0x00039CC0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4x3 operator +(float4x3 val)
		{
			return new float4x3(+val.c0, +val.c1, +val.c2);
		}

		// Token: 0x06001522 RID: 5410 RVA: 0x0003BAE8 File Offset: 0x00039CE8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x3 operator ==(float4x3 lhs, float4x3 rhs)
		{
			return new bool4x3(lhs.c0 == rhs.c0, lhs.c1 == rhs.c1, lhs.c2 == rhs.c2);
		}

		// Token: 0x06001523 RID: 5411 RVA: 0x0003BB22 File Offset: 0x00039D22
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x3 operator ==(float4x3 lhs, float rhs)
		{
			return new bool4x3(lhs.c0 == rhs, lhs.c1 == rhs, lhs.c2 == rhs);
		}

		// Token: 0x06001524 RID: 5412 RVA: 0x0003BB4D File Offset: 0x00039D4D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x3 operator ==(float lhs, float4x3 rhs)
		{
			return new bool4x3(lhs == rhs.c0, lhs == rhs.c1, lhs == rhs.c2);
		}

		// Token: 0x06001525 RID: 5413 RVA: 0x0003BB78 File Offset: 0x00039D78
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x3 operator !=(float4x3 lhs, float4x3 rhs)
		{
			return new bool4x3(lhs.c0 != rhs.c0, lhs.c1 != rhs.c1, lhs.c2 != rhs.c2);
		}

		// Token: 0x06001526 RID: 5414 RVA: 0x0003BBB2 File Offset: 0x00039DB2
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x3 operator !=(float4x3 lhs, float rhs)
		{
			return new bool4x3(lhs.c0 != rhs, lhs.c1 != rhs, lhs.c2 != rhs);
		}

		// Token: 0x06001527 RID: 5415 RVA: 0x0003BBDD File Offset: 0x00039DDD
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x3 operator !=(float lhs, float4x3 rhs)
		{
			return new bool4x3(lhs != rhs.c0, lhs != rhs.c1, lhs != rhs.c2);
		}

		// Token: 0x170005C6 RID: 1478
		public unsafe float4 this[int index]
		{
			get
			{
				fixed (float4x3* ptr = &this)
				{
					return ref *(float4*)(ptr + (IntPtr)index * (IntPtr)sizeof(float4) / (IntPtr)sizeof(float4x3));
				}
			}
		}

		// Token: 0x06001529 RID: 5417 RVA: 0x0003BC23 File Offset: 0x00039E23
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool Equals(float4x3 rhs)
		{
			return this.c0.Equals(rhs.c0) && this.c1.Equals(rhs.c1) && this.c2.Equals(rhs.c2);
		}

		// Token: 0x0600152A RID: 5418 RVA: 0x0003BC60 File Offset: 0x00039E60
		public override bool Equals(object o)
		{
			if (o is float4x3)
			{
				float4x3 rhs = (float4x3)o;
				return this.Equals(rhs);
			}
			return false;
		}

		// Token: 0x0600152B RID: 5419 RVA: 0x0003BC85 File Offset: 0x00039E85
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override int GetHashCode()
		{
			return (int)math.hash(this);
		}

		// Token: 0x0600152C RID: 5420 RVA: 0x0003BC94 File Offset: 0x00039E94
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override string ToString()
		{
			return string.Format("float4x3({0}f, {1}f, {2}f,  {3}f, {4}f, {5}f,  {6}f, {7}f, {8}f,  {9}f, {10}f, {11}f)", new object[]
			{
				this.c0.x,
				this.c1.x,
				this.c2.x,
				this.c0.y,
				this.c1.y,
				this.c2.y,
				this.c0.z,
				this.c1.z,
				this.c2.z,
				this.c0.w,
				this.c1.w,
				this.c2.w
			});
		}

		// Token: 0x0600152D RID: 5421 RVA: 0x0003BD9C File Offset: 0x00039F9C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public string ToString(string format, IFormatProvider formatProvider)
		{
			return string.Format("float4x3({0}f, {1}f, {2}f,  {3}f, {4}f, {5}f,  {6}f, {7}f, {8}f,  {9}f, {10}f, {11}f)", new object[]
			{
				this.c0.x.ToString(format, formatProvider),
				this.c1.x.ToString(format, formatProvider),
				this.c2.x.ToString(format, formatProvider),
				this.c0.y.ToString(format, formatProvider),
				this.c1.y.ToString(format, formatProvider),
				this.c2.y.ToString(format, formatProvider),
				this.c0.z.ToString(format, formatProvider),
				this.c1.z.ToString(format, formatProvider),
				this.c2.z.ToString(format, formatProvider),
				this.c0.w.ToString(format, formatProvider),
				this.c1.w.ToString(format, formatProvider),
				this.c2.w.ToString(format, formatProvider)
			});
		}

		// Token: 0x04000097 RID: 151
		public float4 c0;

		// Token: 0x04000098 RID: 152
		public float4 c1;

		// Token: 0x04000099 RID: 153
		public float4 c2;

		// Token: 0x0400009A RID: 154
		public static readonly float4x3 zero;
	}
}

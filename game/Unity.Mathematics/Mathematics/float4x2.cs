using System;
using System.Runtime.CompilerServices;
using Unity.IL2CPP.CompilerServices;

namespace Unity.Mathematics
{
	// Token: 0x02000025 RID: 37
	[Il2CppEagerStaticClassConstruction]
	[Serializable]
	public struct float4x2 : IEquatable<float4x2>, IFormattable
	{
		// Token: 0x060014B0 RID: 5296 RVA: 0x0003A8E3 File Offset: 0x00038AE3
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float4x2(float4 c0, float4 c1)
		{
			this.c0 = c0;
			this.c1 = c1;
		}

		// Token: 0x060014B1 RID: 5297 RVA: 0x0003A8F3 File Offset: 0x00038AF3
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float4x2(float m00, float m01, float m10, float m11, float m20, float m21, float m30, float m31)
		{
			this.c0 = new float4(m00, m10, m20, m30);
			this.c1 = new float4(m01, m11, m21, m31);
		}

		// Token: 0x060014B2 RID: 5298 RVA: 0x0003A918 File Offset: 0x00038B18
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float4x2(float v)
		{
			this.c0 = v;
			this.c1 = v;
		}

		// Token: 0x060014B3 RID: 5299 RVA: 0x0003A934 File Offset: 0x00038B34
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float4x2(bool v)
		{
			this.c0 = math.select(new float4(0f), new float4(1f), v);
			this.c1 = math.select(new float4(0f), new float4(1f), v);
		}

		// Token: 0x060014B4 RID: 5300 RVA: 0x0003A984 File Offset: 0x00038B84
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float4x2(bool4x2 v)
		{
			this.c0 = math.select(new float4(0f), new float4(1f), v.c0);
			this.c1 = math.select(new float4(0f), new float4(1f), v.c1);
		}

		// Token: 0x060014B5 RID: 5301 RVA: 0x0003A9DB File Offset: 0x00038BDB
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float4x2(int v)
		{
			this.c0 = v;
			this.c1 = v;
		}

		// Token: 0x060014B6 RID: 5302 RVA: 0x0003A9F5 File Offset: 0x00038BF5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float4x2(int4x2 v)
		{
			this.c0 = v.c0;
			this.c1 = v.c1;
		}

		// Token: 0x060014B7 RID: 5303 RVA: 0x0003AA19 File Offset: 0x00038C19
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float4x2(uint v)
		{
			this.c0 = v;
			this.c1 = v;
		}

		// Token: 0x060014B8 RID: 5304 RVA: 0x0003AA33 File Offset: 0x00038C33
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float4x2(uint4x2 v)
		{
			this.c0 = v.c0;
			this.c1 = v.c1;
		}

		// Token: 0x060014B9 RID: 5305 RVA: 0x0003AA57 File Offset: 0x00038C57
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float4x2(double v)
		{
			this.c0 = (float4)v;
			this.c1 = (float4)v;
		}

		// Token: 0x060014BA RID: 5306 RVA: 0x0003AA71 File Offset: 0x00038C71
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float4x2(double4x2 v)
		{
			this.c0 = (float4)v.c0;
			this.c1 = (float4)v.c1;
		}

		// Token: 0x060014BB RID: 5307 RVA: 0x0003AA95 File Offset: 0x00038C95
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator float4x2(float v)
		{
			return new float4x2(v);
		}

		// Token: 0x060014BC RID: 5308 RVA: 0x0003AA9D File Offset: 0x00038C9D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator float4x2(bool v)
		{
			return new float4x2(v);
		}

		// Token: 0x060014BD RID: 5309 RVA: 0x0003AAA5 File Offset: 0x00038CA5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator float4x2(bool4x2 v)
		{
			return new float4x2(v);
		}

		// Token: 0x060014BE RID: 5310 RVA: 0x0003AAAD File Offset: 0x00038CAD
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator float4x2(int v)
		{
			return new float4x2(v);
		}

		// Token: 0x060014BF RID: 5311 RVA: 0x0003AAB5 File Offset: 0x00038CB5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator float4x2(int4x2 v)
		{
			return new float4x2(v);
		}

		// Token: 0x060014C0 RID: 5312 RVA: 0x0003AABD File Offset: 0x00038CBD
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator float4x2(uint v)
		{
			return new float4x2(v);
		}

		// Token: 0x060014C1 RID: 5313 RVA: 0x0003AAC5 File Offset: 0x00038CC5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator float4x2(uint4x2 v)
		{
			return new float4x2(v);
		}

		// Token: 0x060014C2 RID: 5314 RVA: 0x0003AACD File Offset: 0x00038CCD
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator float4x2(double v)
		{
			return new float4x2(v);
		}

		// Token: 0x060014C3 RID: 5315 RVA: 0x0003AAD5 File Offset: 0x00038CD5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator float4x2(double4x2 v)
		{
			return new float4x2(v);
		}

		// Token: 0x060014C4 RID: 5316 RVA: 0x0003AADD File Offset: 0x00038CDD
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4x2 operator *(float4x2 lhs, float4x2 rhs)
		{
			return new float4x2(lhs.c0 * rhs.c0, lhs.c1 * rhs.c1);
		}

		// Token: 0x060014C5 RID: 5317 RVA: 0x0003AB06 File Offset: 0x00038D06
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4x2 operator *(float4x2 lhs, float rhs)
		{
			return new float4x2(lhs.c0 * rhs, lhs.c1 * rhs);
		}

		// Token: 0x060014C6 RID: 5318 RVA: 0x0003AB25 File Offset: 0x00038D25
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4x2 operator *(float lhs, float4x2 rhs)
		{
			return new float4x2(lhs * rhs.c0, lhs * rhs.c1);
		}

		// Token: 0x060014C7 RID: 5319 RVA: 0x0003AB44 File Offset: 0x00038D44
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4x2 operator +(float4x2 lhs, float4x2 rhs)
		{
			return new float4x2(lhs.c0 + rhs.c0, lhs.c1 + rhs.c1);
		}

		// Token: 0x060014C8 RID: 5320 RVA: 0x0003AB6D File Offset: 0x00038D6D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4x2 operator +(float4x2 lhs, float rhs)
		{
			return new float4x2(lhs.c0 + rhs, lhs.c1 + rhs);
		}

		// Token: 0x060014C9 RID: 5321 RVA: 0x0003AB8C File Offset: 0x00038D8C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4x2 operator +(float lhs, float4x2 rhs)
		{
			return new float4x2(lhs + rhs.c0, lhs + rhs.c1);
		}

		// Token: 0x060014CA RID: 5322 RVA: 0x0003ABAB File Offset: 0x00038DAB
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4x2 operator -(float4x2 lhs, float4x2 rhs)
		{
			return new float4x2(lhs.c0 - rhs.c0, lhs.c1 - rhs.c1);
		}

		// Token: 0x060014CB RID: 5323 RVA: 0x0003ABD4 File Offset: 0x00038DD4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4x2 operator -(float4x2 lhs, float rhs)
		{
			return new float4x2(lhs.c0 - rhs, lhs.c1 - rhs);
		}

		// Token: 0x060014CC RID: 5324 RVA: 0x0003ABF3 File Offset: 0x00038DF3
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4x2 operator -(float lhs, float4x2 rhs)
		{
			return new float4x2(lhs - rhs.c0, lhs - rhs.c1);
		}

		// Token: 0x060014CD RID: 5325 RVA: 0x0003AC12 File Offset: 0x00038E12
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4x2 operator /(float4x2 lhs, float4x2 rhs)
		{
			return new float4x2(lhs.c0 / rhs.c0, lhs.c1 / rhs.c1);
		}

		// Token: 0x060014CE RID: 5326 RVA: 0x0003AC3B File Offset: 0x00038E3B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4x2 operator /(float4x2 lhs, float rhs)
		{
			return new float4x2(lhs.c0 / rhs, lhs.c1 / rhs);
		}

		// Token: 0x060014CF RID: 5327 RVA: 0x0003AC5A File Offset: 0x00038E5A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4x2 operator /(float lhs, float4x2 rhs)
		{
			return new float4x2(lhs / rhs.c0, lhs / rhs.c1);
		}

		// Token: 0x060014D0 RID: 5328 RVA: 0x0003AC79 File Offset: 0x00038E79
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4x2 operator %(float4x2 lhs, float4x2 rhs)
		{
			return new float4x2(lhs.c0 % rhs.c0, lhs.c1 % rhs.c1);
		}

		// Token: 0x060014D1 RID: 5329 RVA: 0x0003ACA2 File Offset: 0x00038EA2
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4x2 operator %(float4x2 lhs, float rhs)
		{
			return new float4x2(lhs.c0 % rhs, lhs.c1 % rhs);
		}

		// Token: 0x060014D2 RID: 5330 RVA: 0x0003ACC1 File Offset: 0x00038EC1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4x2 operator %(float lhs, float4x2 rhs)
		{
			return new float4x2(lhs % rhs.c0, lhs % rhs.c1);
		}

		// Token: 0x060014D3 RID: 5331 RVA: 0x0003ACE0 File Offset: 0x00038EE0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4x2 operator ++(float4x2 val)
		{
			float4 @float = ++val.c0;
			val.c0 = @float;
			float4 float2 = @float;
			@float = ++val.c1;
			val.c1 = @float;
			return new float4x2(float2, @float);
		}

		// Token: 0x060014D4 RID: 5332 RVA: 0x0003AD28 File Offset: 0x00038F28
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4x2 operator --(float4x2 val)
		{
			float4 @float = --val.c0;
			val.c0 = @float;
			float4 float2 = @float;
			@float = --val.c1;
			val.c1 = @float;
			return new float4x2(float2, @float);
		}

		// Token: 0x060014D5 RID: 5333 RVA: 0x0003AD6E File Offset: 0x00038F6E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x2 operator <(float4x2 lhs, float4x2 rhs)
		{
			return new bool4x2(lhs.c0 < rhs.c0, lhs.c1 < rhs.c1);
		}

		// Token: 0x060014D6 RID: 5334 RVA: 0x0003AD97 File Offset: 0x00038F97
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x2 operator <(float4x2 lhs, float rhs)
		{
			return new bool4x2(lhs.c0 < rhs, lhs.c1 < rhs);
		}

		// Token: 0x060014D7 RID: 5335 RVA: 0x0003ADB6 File Offset: 0x00038FB6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x2 operator <(float lhs, float4x2 rhs)
		{
			return new bool4x2(lhs < rhs.c0, lhs < rhs.c1);
		}

		// Token: 0x060014D8 RID: 5336 RVA: 0x0003ADD5 File Offset: 0x00038FD5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x2 operator <=(float4x2 lhs, float4x2 rhs)
		{
			return new bool4x2(lhs.c0 <= rhs.c0, lhs.c1 <= rhs.c1);
		}

		// Token: 0x060014D9 RID: 5337 RVA: 0x0003ADFE File Offset: 0x00038FFE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x2 operator <=(float4x2 lhs, float rhs)
		{
			return new bool4x2(lhs.c0 <= rhs, lhs.c1 <= rhs);
		}

		// Token: 0x060014DA RID: 5338 RVA: 0x0003AE1D File Offset: 0x0003901D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x2 operator <=(float lhs, float4x2 rhs)
		{
			return new bool4x2(lhs <= rhs.c0, lhs <= rhs.c1);
		}

		// Token: 0x060014DB RID: 5339 RVA: 0x0003AE3C File Offset: 0x0003903C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x2 operator >(float4x2 lhs, float4x2 rhs)
		{
			return new bool4x2(lhs.c0 > rhs.c0, lhs.c1 > rhs.c1);
		}

		// Token: 0x060014DC RID: 5340 RVA: 0x0003AE65 File Offset: 0x00039065
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x2 operator >(float4x2 lhs, float rhs)
		{
			return new bool4x2(lhs.c0 > rhs, lhs.c1 > rhs);
		}

		// Token: 0x060014DD RID: 5341 RVA: 0x0003AE84 File Offset: 0x00039084
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x2 operator >(float lhs, float4x2 rhs)
		{
			return new bool4x2(lhs > rhs.c0, lhs > rhs.c1);
		}

		// Token: 0x060014DE RID: 5342 RVA: 0x0003AEA3 File Offset: 0x000390A3
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x2 operator >=(float4x2 lhs, float4x2 rhs)
		{
			return new bool4x2(lhs.c0 >= rhs.c0, lhs.c1 >= rhs.c1);
		}

		// Token: 0x060014DF RID: 5343 RVA: 0x0003AECC File Offset: 0x000390CC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x2 operator >=(float4x2 lhs, float rhs)
		{
			return new bool4x2(lhs.c0 >= rhs, lhs.c1 >= rhs);
		}

		// Token: 0x060014E0 RID: 5344 RVA: 0x0003AEEB File Offset: 0x000390EB
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x2 operator >=(float lhs, float4x2 rhs)
		{
			return new bool4x2(lhs >= rhs.c0, lhs >= rhs.c1);
		}

		// Token: 0x060014E1 RID: 5345 RVA: 0x0003AF0A File Offset: 0x0003910A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4x2 operator -(float4x2 val)
		{
			return new float4x2(-val.c0, -val.c1);
		}

		// Token: 0x060014E2 RID: 5346 RVA: 0x0003AF27 File Offset: 0x00039127
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4x2 operator +(float4x2 val)
		{
			return new float4x2(+val.c0, +val.c1);
		}

		// Token: 0x060014E3 RID: 5347 RVA: 0x0003AF44 File Offset: 0x00039144
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x2 operator ==(float4x2 lhs, float4x2 rhs)
		{
			return new bool4x2(lhs.c0 == rhs.c0, lhs.c1 == rhs.c1);
		}

		// Token: 0x060014E4 RID: 5348 RVA: 0x0003AF6D File Offset: 0x0003916D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x2 operator ==(float4x2 lhs, float rhs)
		{
			return new bool4x2(lhs.c0 == rhs, lhs.c1 == rhs);
		}

		// Token: 0x060014E5 RID: 5349 RVA: 0x0003AF8C File Offset: 0x0003918C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x2 operator ==(float lhs, float4x2 rhs)
		{
			return new bool4x2(lhs == rhs.c0, lhs == rhs.c1);
		}

		// Token: 0x060014E6 RID: 5350 RVA: 0x0003AFAB File Offset: 0x000391AB
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x2 operator !=(float4x2 lhs, float4x2 rhs)
		{
			return new bool4x2(lhs.c0 != rhs.c0, lhs.c1 != rhs.c1);
		}

		// Token: 0x060014E7 RID: 5351 RVA: 0x0003AFD4 File Offset: 0x000391D4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x2 operator !=(float4x2 lhs, float rhs)
		{
			return new bool4x2(lhs.c0 != rhs, lhs.c1 != rhs);
		}

		// Token: 0x060014E8 RID: 5352 RVA: 0x0003AFF3 File Offset: 0x000391F3
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x2 operator !=(float lhs, float4x2 rhs)
		{
			return new bool4x2(lhs != rhs.c0, lhs != rhs.c1);
		}

		// Token: 0x170005C5 RID: 1477
		public unsafe float4 this[int index]
		{
			get
			{
				fixed (float4x2* ptr = &this)
				{
					return ref *(float4*)(ptr + (IntPtr)index * (IntPtr)sizeof(float4) / (IntPtr)sizeof(float4x2));
				}
			}
		}

		// Token: 0x060014EA RID: 5354 RVA: 0x0003B02F File Offset: 0x0003922F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool Equals(float4x2 rhs)
		{
			return this.c0.Equals(rhs.c0) && this.c1.Equals(rhs.c1);
		}

		// Token: 0x060014EB RID: 5355 RVA: 0x0003B058 File Offset: 0x00039258
		public override bool Equals(object o)
		{
			if (o is float4x2)
			{
				float4x2 rhs = (float4x2)o;
				return this.Equals(rhs);
			}
			return false;
		}

		// Token: 0x060014EC RID: 5356 RVA: 0x0003B07D File Offset: 0x0003927D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override int GetHashCode()
		{
			return (int)math.hash(this);
		}

		// Token: 0x060014ED RID: 5357 RVA: 0x0003B08C File Offset: 0x0003928C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override string ToString()
		{
			return string.Format("float4x2({0}f, {1}f,  {2}f, {3}f,  {4}f, {5}f,  {6}f, {7}f)", new object[]
			{
				this.c0.x,
				this.c1.x,
				this.c0.y,
				this.c1.y,
				this.c0.z,
				this.c1.z,
				this.c0.w,
				this.c1.w
			});
		}

		// Token: 0x060014EE RID: 5358 RVA: 0x0003B144 File Offset: 0x00039344
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public string ToString(string format, IFormatProvider formatProvider)
		{
			return string.Format("float4x2({0}f, {1}f,  {2}f, {3}f,  {4}f, {5}f,  {6}f, {7}f)", new object[]
			{
				this.c0.x.ToString(format, formatProvider),
				this.c1.x.ToString(format, formatProvider),
				this.c0.y.ToString(format, formatProvider),
				this.c1.y.ToString(format, formatProvider),
				this.c0.z.ToString(format, formatProvider),
				this.c1.z.ToString(format, formatProvider),
				this.c0.w.ToString(format, formatProvider),
				this.c1.w.ToString(format, formatProvider)
			});
		}

		// Token: 0x04000094 RID: 148
		public float4 c0;

		// Token: 0x04000095 RID: 149
		public float4 c1;

		// Token: 0x04000096 RID: 150
		public static readonly float4x2 zero;
	}
}

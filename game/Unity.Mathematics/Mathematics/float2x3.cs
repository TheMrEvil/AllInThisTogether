using System;
using System.Runtime.CompilerServices;
using Unity.IL2CPP.CompilerServices;

namespace Unity.Mathematics
{
	// Token: 0x0200001E RID: 30
	[Il2CppEagerStaticClassConstruction]
	[Serializable]
	public struct float2x3 : IEquatable<float2x3>, IFormattable
	{
		// Token: 0x060010B9 RID: 4281 RVA: 0x00030712 File Offset: 0x0002E912
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float2x3(float2 c0, float2 c1, float2 c2)
		{
			this.c0 = c0;
			this.c1 = c1;
			this.c2 = c2;
		}

		// Token: 0x060010BA RID: 4282 RVA: 0x00030729 File Offset: 0x0002E929
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float2x3(float m00, float m01, float m02, float m10, float m11, float m12)
		{
			this.c0 = new float2(m00, m10);
			this.c1 = new float2(m01, m11);
			this.c2 = new float2(m02, m12);
		}

		// Token: 0x060010BB RID: 4283 RVA: 0x00030755 File Offset: 0x0002E955
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float2x3(float v)
		{
			this.c0 = v;
			this.c1 = v;
			this.c2 = v;
		}

		// Token: 0x060010BC RID: 4284 RVA: 0x0003077C File Offset: 0x0002E97C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float2x3(bool v)
		{
			this.c0 = math.select(new float2(0f), new float2(1f), v);
			this.c1 = math.select(new float2(0f), new float2(1f), v);
			this.c2 = math.select(new float2(0f), new float2(1f), v);
		}

		// Token: 0x060010BD RID: 4285 RVA: 0x000307EC File Offset: 0x0002E9EC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float2x3(bool2x3 v)
		{
			this.c0 = math.select(new float2(0f), new float2(1f), v.c0);
			this.c1 = math.select(new float2(0f), new float2(1f), v.c1);
			this.c2 = math.select(new float2(0f), new float2(1f), v.c2);
		}

		// Token: 0x060010BE RID: 4286 RVA: 0x00030868 File Offset: 0x0002EA68
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float2x3(int v)
		{
			this.c0 = v;
			this.c1 = v;
			this.c2 = v;
		}

		// Token: 0x060010BF RID: 4287 RVA: 0x0003088E File Offset: 0x0002EA8E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float2x3(int2x3 v)
		{
			this.c0 = v.c0;
			this.c1 = v.c1;
			this.c2 = v.c2;
		}

		// Token: 0x060010C0 RID: 4288 RVA: 0x000308C3 File Offset: 0x0002EAC3
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float2x3(uint v)
		{
			this.c0 = v;
			this.c1 = v;
			this.c2 = v;
		}

		// Token: 0x060010C1 RID: 4289 RVA: 0x000308E9 File Offset: 0x0002EAE9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float2x3(uint2x3 v)
		{
			this.c0 = v.c0;
			this.c1 = v.c1;
			this.c2 = v.c2;
		}

		// Token: 0x060010C2 RID: 4290 RVA: 0x0003091E File Offset: 0x0002EB1E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float2x3(double v)
		{
			this.c0 = (float2)v;
			this.c1 = (float2)v;
			this.c2 = (float2)v;
		}

		// Token: 0x060010C3 RID: 4291 RVA: 0x00030944 File Offset: 0x0002EB44
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float2x3(double2x3 v)
		{
			this.c0 = (float2)v.c0;
			this.c1 = (float2)v.c1;
			this.c2 = (float2)v.c2;
		}

		// Token: 0x060010C4 RID: 4292 RVA: 0x00030979 File Offset: 0x0002EB79
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator float2x3(float v)
		{
			return new float2x3(v);
		}

		// Token: 0x060010C5 RID: 4293 RVA: 0x00030981 File Offset: 0x0002EB81
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator float2x3(bool v)
		{
			return new float2x3(v);
		}

		// Token: 0x060010C6 RID: 4294 RVA: 0x00030989 File Offset: 0x0002EB89
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator float2x3(bool2x3 v)
		{
			return new float2x3(v);
		}

		// Token: 0x060010C7 RID: 4295 RVA: 0x00030991 File Offset: 0x0002EB91
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator float2x3(int v)
		{
			return new float2x3(v);
		}

		// Token: 0x060010C8 RID: 4296 RVA: 0x00030999 File Offset: 0x0002EB99
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator float2x3(int2x3 v)
		{
			return new float2x3(v);
		}

		// Token: 0x060010C9 RID: 4297 RVA: 0x000309A1 File Offset: 0x0002EBA1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator float2x3(uint v)
		{
			return new float2x3(v);
		}

		// Token: 0x060010CA RID: 4298 RVA: 0x000309A9 File Offset: 0x0002EBA9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator float2x3(uint2x3 v)
		{
			return new float2x3(v);
		}

		// Token: 0x060010CB RID: 4299 RVA: 0x000309B1 File Offset: 0x0002EBB1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator float2x3(double v)
		{
			return new float2x3(v);
		}

		// Token: 0x060010CC RID: 4300 RVA: 0x000309B9 File Offset: 0x0002EBB9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator float2x3(double2x3 v)
		{
			return new float2x3(v);
		}

		// Token: 0x060010CD RID: 4301 RVA: 0x000309C1 File Offset: 0x0002EBC1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2x3 operator *(float2x3 lhs, float2x3 rhs)
		{
			return new float2x3(lhs.c0 * rhs.c0, lhs.c1 * rhs.c1, lhs.c2 * rhs.c2);
		}

		// Token: 0x060010CE RID: 4302 RVA: 0x000309FB File Offset: 0x0002EBFB
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2x3 operator *(float2x3 lhs, float rhs)
		{
			return new float2x3(lhs.c0 * rhs, lhs.c1 * rhs, lhs.c2 * rhs);
		}

		// Token: 0x060010CF RID: 4303 RVA: 0x00030A26 File Offset: 0x0002EC26
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2x3 operator *(float lhs, float2x3 rhs)
		{
			return new float2x3(lhs * rhs.c0, lhs * rhs.c1, lhs * rhs.c2);
		}

		// Token: 0x060010D0 RID: 4304 RVA: 0x00030A51 File Offset: 0x0002EC51
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2x3 operator +(float2x3 lhs, float2x3 rhs)
		{
			return new float2x3(lhs.c0 + rhs.c0, lhs.c1 + rhs.c1, lhs.c2 + rhs.c2);
		}

		// Token: 0x060010D1 RID: 4305 RVA: 0x00030A8B File Offset: 0x0002EC8B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2x3 operator +(float2x3 lhs, float rhs)
		{
			return new float2x3(lhs.c0 + rhs, lhs.c1 + rhs, lhs.c2 + rhs);
		}

		// Token: 0x060010D2 RID: 4306 RVA: 0x00030AB6 File Offset: 0x0002ECB6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2x3 operator +(float lhs, float2x3 rhs)
		{
			return new float2x3(lhs + rhs.c0, lhs + rhs.c1, lhs + rhs.c2);
		}

		// Token: 0x060010D3 RID: 4307 RVA: 0x00030AE1 File Offset: 0x0002ECE1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2x3 operator -(float2x3 lhs, float2x3 rhs)
		{
			return new float2x3(lhs.c0 - rhs.c0, lhs.c1 - rhs.c1, lhs.c2 - rhs.c2);
		}

		// Token: 0x060010D4 RID: 4308 RVA: 0x00030B1B File Offset: 0x0002ED1B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2x3 operator -(float2x3 lhs, float rhs)
		{
			return new float2x3(lhs.c0 - rhs, lhs.c1 - rhs, lhs.c2 - rhs);
		}

		// Token: 0x060010D5 RID: 4309 RVA: 0x00030B46 File Offset: 0x0002ED46
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2x3 operator -(float lhs, float2x3 rhs)
		{
			return new float2x3(lhs - rhs.c0, lhs - rhs.c1, lhs - rhs.c2);
		}

		// Token: 0x060010D6 RID: 4310 RVA: 0x00030B71 File Offset: 0x0002ED71
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2x3 operator /(float2x3 lhs, float2x3 rhs)
		{
			return new float2x3(lhs.c0 / rhs.c0, lhs.c1 / rhs.c1, lhs.c2 / rhs.c2);
		}

		// Token: 0x060010D7 RID: 4311 RVA: 0x00030BAB File Offset: 0x0002EDAB
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2x3 operator /(float2x3 lhs, float rhs)
		{
			return new float2x3(lhs.c0 / rhs, lhs.c1 / rhs, lhs.c2 / rhs);
		}

		// Token: 0x060010D8 RID: 4312 RVA: 0x00030BD6 File Offset: 0x0002EDD6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2x3 operator /(float lhs, float2x3 rhs)
		{
			return new float2x3(lhs / rhs.c0, lhs / rhs.c1, lhs / rhs.c2);
		}

		// Token: 0x060010D9 RID: 4313 RVA: 0x00030C01 File Offset: 0x0002EE01
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2x3 operator %(float2x3 lhs, float2x3 rhs)
		{
			return new float2x3(lhs.c0 % rhs.c0, lhs.c1 % rhs.c1, lhs.c2 % rhs.c2);
		}

		// Token: 0x060010DA RID: 4314 RVA: 0x00030C3B File Offset: 0x0002EE3B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2x3 operator %(float2x3 lhs, float rhs)
		{
			return new float2x3(lhs.c0 % rhs, lhs.c1 % rhs, lhs.c2 % rhs);
		}

		// Token: 0x060010DB RID: 4315 RVA: 0x00030C66 File Offset: 0x0002EE66
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2x3 operator %(float lhs, float2x3 rhs)
		{
			return new float2x3(lhs % rhs.c0, lhs % rhs.c1, lhs % rhs.c2);
		}

		// Token: 0x060010DC RID: 4316 RVA: 0x00030C94 File Offset: 0x0002EE94
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2x3 operator ++(float2x3 val)
		{
			float2 @float = ++val.c0;
			val.c0 = @float;
			float2 float2 = @float;
			@float = ++val.c1;
			val.c1 = @float;
			float2 float3 = @float;
			@float = ++val.c2;
			val.c2 = @float;
			return new float2x3(float2, float3, @float);
		}

		// Token: 0x060010DD RID: 4317 RVA: 0x00030CF4 File Offset: 0x0002EEF4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2x3 operator --(float2x3 val)
		{
			float2 @float = --val.c0;
			val.c0 = @float;
			float2 float2 = @float;
			@float = --val.c1;
			val.c1 = @float;
			float2 float3 = @float;
			@float = --val.c2;
			val.c2 = @float;
			return new float2x3(float2, float3, @float);
		}

		// Token: 0x060010DE RID: 4318 RVA: 0x00030D54 File Offset: 0x0002EF54
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x3 operator <(float2x3 lhs, float2x3 rhs)
		{
			return new bool2x3(lhs.c0 < rhs.c0, lhs.c1 < rhs.c1, lhs.c2 < rhs.c2);
		}

		// Token: 0x060010DF RID: 4319 RVA: 0x00030D8E File Offset: 0x0002EF8E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x3 operator <(float2x3 lhs, float rhs)
		{
			return new bool2x3(lhs.c0 < rhs, lhs.c1 < rhs, lhs.c2 < rhs);
		}

		// Token: 0x060010E0 RID: 4320 RVA: 0x00030DB9 File Offset: 0x0002EFB9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x3 operator <(float lhs, float2x3 rhs)
		{
			return new bool2x3(lhs < rhs.c0, lhs < rhs.c1, lhs < rhs.c2);
		}

		// Token: 0x060010E1 RID: 4321 RVA: 0x00030DE4 File Offset: 0x0002EFE4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x3 operator <=(float2x3 lhs, float2x3 rhs)
		{
			return new bool2x3(lhs.c0 <= rhs.c0, lhs.c1 <= rhs.c1, lhs.c2 <= rhs.c2);
		}

		// Token: 0x060010E2 RID: 4322 RVA: 0x00030E1E File Offset: 0x0002F01E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x3 operator <=(float2x3 lhs, float rhs)
		{
			return new bool2x3(lhs.c0 <= rhs, lhs.c1 <= rhs, lhs.c2 <= rhs);
		}

		// Token: 0x060010E3 RID: 4323 RVA: 0x00030E49 File Offset: 0x0002F049
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x3 operator <=(float lhs, float2x3 rhs)
		{
			return new bool2x3(lhs <= rhs.c0, lhs <= rhs.c1, lhs <= rhs.c2);
		}

		// Token: 0x060010E4 RID: 4324 RVA: 0x00030E74 File Offset: 0x0002F074
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x3 operator >(float2x3 lhs, float2x3 rhs)
		{
			return new bool2x3(lhs.c0 > rhs.c0, lhs.c1 > rhs.c1, lhs.c2 > rhs.c2);
		}

		// Token: 0x060010E5 RID: 4325 RVA: 0x00030EAE File Offset: 0x0002F0AE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x3 operator >(float2x3 lhs, float rhs)
		{
			return new bool2x3(lhs.c0 > rhs, lhs.c1 > rhs, lhs.c2 > rhs);
		}

		// Token: 0x060010E6 RID: 4326 RVA: 0x00030ED9 File Offset: 0x0002F0D9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x3 operator >(float lhs, float2x3 rhs)
		{
			return new bool2x3(lhs > rhs.c0, lhs > rhs.c1, lhs > rhs.c2);
		}

		// Token: 0x060010E7 RID: 4327 RVA: 0x00030F04 File Offset: 0x0002F104
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x3 operator >=(float2x3 lhs, float2x3 rhs)
		{
			return new bool2x3(lhs.c0 >= rhs.c0, lhs.c1 >= rhs.c1, lhs.c2 >= rhs.c2);
		}

		// Token: 0x060010E8 RID: 4328 RVA: 0x00030F3E File Offset: 0x0002F13E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x3 operator >=(float2x3 lhs, float rhs)
		{
			return new bool2x3(lhs.c0 >= rhs, lhs.c1 >= rhs, lhs.c2 >= rhs);
		}

		// Token: 0x060010E9 RID: 4329 RVA: 0x00030F69 File Offset: 0x0002F169
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x3 operator >=(float lhs, float2x3 rhs)
		{
			return new bool2x3(lhs >= rhs.c0, lhs >= rhs.c1, lhs >= rhs.c2);
		}

		// Token: 0x060010EA RID: 4330 RVA: 0x00030F94 File Offset: 0x0002F194
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2x3 operator -(float2x3 val)
		{
			return new float2x3(-val.c0, -val.c1, -val.c2);
		}

		// Token: 0x060010EB RID: 4331 RVA: 0x00030FBC File Offset: 0x0002F1BC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2x3 operator +(float2x3 val)
		{
			return new float2x3(+val.c0, +val.c1, +val.c2);
		}

		// Token: 0x060010EC RID: 4332 RVA: 0x00030FE4 File Offset: 0x0002F1E4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x3 operator ==(float2x3 lhs, float2x3 rhs)
		{
			return new bool2x3(lhs.c0 == rhs.c0, lhs.c1 == rhs.c1, lhs.c2 == rhs.c2);
		}

		// Token: 0x060010ED RID: 4333 RVA: 0x0003101E File Offset: 0x0002F21E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x3 operator ==(float2x3 lhs, float rhs)
		{
			return new bool2x3(lhs.c0 == rhs, lhs.c1 == rhs, lhs.c2 == rhs);
		}

		// Token: 0x060010EE RID: 4334 RVA: 0x00031049 File Offset: 0x0002F249
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x3 operator ==(float lhs, float2x3 rhs)
		{
			return new bool2x3(lhs == rhs.c0, lhs == rhs.c1, lhs == rhs.c2);
		}

		// Token: 0x060010EF RID: 4335 RVA: 0x00031074 File Offset: 0x0002F274
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x3 operator !=(float2x3 lhs, float2x3 rhs)
		{
			return new bool2x3(lhs.c0 != rhs.c0, lhs.c1 != rhs.c1, lhs.c2 != rhs.c2);
		}

		// Token: 0x060010F0 RID: 4336 RVA: 0x000310AE File Offset: 0x0002F2AE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x3 operator !=(float2x3 lhs, float rhs)
		{
			return new bool2x3(lhs.c0 != rhs, lhs.c1 != rhs, lhs.c2 != rhs);
		}

		// Token: 0x060010F1 RID: 4337 RVA: 0x000310D9 File Offset: 0x0002F2D9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x3 operator !=(float lhs, float2x3 rhs)
		{
			return new bool2x3(lhs != rhs.c0, lhs != rhs.c1, lhs != rhs.c2);
		}

		// Token: 0x170003F9 RID: 1017
		public unsafe float2 this[int index]
		{
			get
			{
				fixed (float2x3* ptr = &this)
				{
					return ref *(float2*)(ptr + (IntPtr)index * (IntPtr)sizeof(float2) / (IntPtr)sizeof(float2x3));
				}
			}
		}

		// Token: 0x060010F3 RID: 4339 RVA: 0x0003111F File Offset: 0x0002F31F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool Equals(float2x3 rhs)
		{
			return this.c0.Equals(rhs.c0) && this.c1.Equals(rhs.c1) && this.c2.Equals(rhs.c2);
		}

		// Token: 0x060010F4 RID: 4340 RVA: 0x0003115C File Offset: 0x0002F35C
		public override bool Equals(object o)
		{
			if (o is float2x3)
			{
				float2x3 rhs = (float2x3)o;
				return this.Equals(rhs);
			}
			return false;
		}

		// Token: 0x060010F5 RID: 4341 RVA: 0x00031181 File Offset: 0x0002F381
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override int GetHashCode()
		{
			return (int)math.hash(this);
		}

		// Token: 0x060010F6 RID: 4342 RVA: 0x00031190 File Offset: 0x0002F390
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override string ToString()
		{
			return string.Format("float2x3({0}f, {1}f, {2}f,  {3}f, {4}f, {5}f)", new object[]
			{
				this.c0.x,
				this.c1.x,
				this.c2.x,
				this.c0.y,
				this.c1.y,
				this.c2.y
			});
		}

		// Token: 0x060010F7 RID: 4343 RVA: 0x00031220 File Offset: 0x0002F420
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public string ToString(string format, IFormatProvider formatProvider)
		{
			return string.Format("float2x3({0}f, {1}f, {2}f,  {3}f, {4}f, {5}f)", new object[]
			{
				this.c0.x.ToString(format, formatProvider),
				this.c1.x.ToString(format, formatProvider),
				this.c2.x.ToString(format, formatProvider),
				this.c0.y.ToString(format, formatProvider),
				this.c1.y.ToString(format, formatProvider),
				this.c2.y.ToString(format, formatProvider)
			});
		}

		// Token: 0x04000075 RID: 117
		public float2 c0;

		// Token: 0x04000076 RID: 118
		public float2 c1;

		// Token: 0x04000077 RID: 119
		public float2 c2;

		// Token: 0x04000078 RID: 120
		public static readonly float2x3 zero;
	}
}

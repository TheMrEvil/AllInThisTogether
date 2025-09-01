using System;
using System.Runtime.CompilerServices;
using Unity.IL2CPP.CompilerServices;

namespace Unity.Mathematics
{
	// Token: 0x0200001D RID: 29
	[Il2CppEagerStaticClassConstruction]
	[Serializable]
	public struct float2x2 : IEquatable<float2x2>, IFormattable
	{
		// Token: 0x06001075 RID: 4213 RVA: 0x0002FE18 File Offset: 0x0002E018
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float2x2(float2 c0, float2 c1)
		{
			this.c0 = c0;
			this.c1 = c1;
		}

		// Token: 0x06001076 RID: 4214 RVA: 0x0002FE28 File Offset: 0x0002E028
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float2x2(float m00, float m01, float m10, float m11)
		{
			this.c0 = new float2(m00, m10);
			this.c1 = new float2(m01, m11);
		}

		// Token: 0x06001077 RID: 4215 RVA: 0x0002FE45 File Offset: 0x0002E045
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float2x2(float v)
		{
			this.c0 = v;
			this.c1 = v;
		}

		// Token: 0x06001078 RID: 4216 RVA: 0x0002FE60 File Offset: 0x0002E060
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float2x2(bool v)
		{
			this.c0 = math.select(new float2(0f), new float2(1f), v);
			this.c1 = math.select(new float2(0f), new float2(1f), v);
		}

		// Token: 0x06001079 RID: 4217 RVA: 0x0002FEB0 File Offset: 0x0002E0B0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float2x2(bool2x2 v)
		{
			this.c0 = math.select(new float2(0f), new float2(1f), v.c0);
			this.c1 = math.select(new float2(0f), new float2(1f), v.c1);
		}

		// Token: 0x0600107A RID: 4218 RVA: 0x0002FF07 File Offset: 0x0002E107
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float2x2(int v)
		{
			this.c0 = v;
			this.c1 = v;
		}

		// Token: 0x0600107B RID: 4219 RVA: 0x0002FF21 File Offset: 0x0002E121
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float2x2(int2x2 v)
		{
			this.c0 = v.c0;
			this.c1 = v.c1;
		}

		// Token: 0x0600107C RID: 4220 RVA: 0x0002FF45 File Offset: 0x0002E145
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float2x2(uint v)
		{
			this.c0 = v;
			this.c1 = v;
		}

		// Token: 0x0600107D RID: 4221 RVA: 0x0002FF5F File Offset: 0x0002E15F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float2x2(uint2x2 v)
		{
			this.c0 = v.c0;
			this.c1 = v.c1;
		}

		// Token: 0x0600107E RID: 4222 RVA: 0x0002FF83 File Offset: 0x0002E183
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float2x2(double v)
		{
			this.c0 = (float2)v;
			this.c1 = (float2)v;
		}

		// Token: 0x0600107F RID: 4223 RVA: 0x0002FF9D File Offset: 0x0002E19D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float2x2(double2x2 v)
		{
			this.c0 = (float2)v.c0;
			this.c1 = (float2)v.c1;
		}

		// Token: 0x06001080 RID: 4224 RVA: 0x0002FFC1 File Offset: 0x0002E1C1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator float2x2(float v)
		{
			return new float2x2(v);
		}

		// Token: 0x06001081 RID: 4225 RVA: 0x0002FFC9 File Offset: 0x0002E1C9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator float2x2(bool v)
		{
			return new float2x2(v);
		}

		// Token: 0x06001082 RID: 4226 RVA: 0x0002FFD1 File Offset: 0x0002E1D1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator float2x2(bool2x2 v)
		{
			return new float2x2(v);
		}

		// Token: 0x06001083 RID: 4227 RVA: 0x0002FFD9 File Offset: 0x0002E1D9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator float2x2(int v)
		{
			return new float2x2(v);
		}

		// Token: 0x06001084 RID: 4228 RVA: 0x0002FFE1 File Offset: 0x0002E1E1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator float2x2(int2x2 v)
		{
			return new float2x2(v);
		}

		// Token: 0x06001085 RID: 4229 RVA: 0x0002FFE9 File Offset: 0x0002E1E9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator float2x2(uint v)
		{
			return new float2x2(v);
		}

		// Token: 0x06001086 RID: 4230 RVA: 0x0002FFF1 File Offset: 0x0002E1F1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator float2x2(uint2x2 v)
		{
			return new float2x2(v);
		}

		// Token: 0x06001087 RID: 4231 RVA: 0x0002FFF9 File Offset: 0x0002E1F9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator float2x2(double v)
		{
			return new float2x2(v);
		}

		// Token: 0x06001088 RID: 4232 RVA: 0x00030001 File Offset: 0x0002E201
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator float2x2(double2x2 v)
		{
			return new float2x2(v);
		}

		// Token: 0x06001089 RID: 4233 RVA: 0x00030009 File Offset: 0x0002E209
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2x2 operator *(float2x2 lhs, float2x2 rhs)
		{
			return new float2x2(lhs.c0 * rhs.c0, lhs.c1 * rhs.c1);
		}

		// Token: 0x0600108A RID: 4234 RVA: 0x00030032 File Offset: 0x0002E232
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2x2 operator *(float2x2 lhs, float rhs)
		{
			return new float2x2(lhs.c0 * rhs, lhs.c1 * rhs);
		}

		// Token: 0x0600108B RID: 4235 RVA: 0x00030051 File Offset: 0x0002E251
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2x2 operator *(float lhs, float2x2 rhs)
		{
			return new float2x2(lhs * rhs.c0, lhs * rhs.c1);
		}

		// Token: 0x0600108C RID: 4236 RVA: 0x00030070 File Offset: 0x0002E270
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2x2 operator +(float2x2 lhs, float2x2 rhs)
		{
			return new float2x2(lhs.c0 + rhs.c0, lhs.c1 + rhs.c1);
		}

		// Token: 0x0600108D RID: 4237 RVA: 0x00030099 File Offset: 0x0002E299
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2x2 operator +(float2x2 lhs, float rhs)
		{
			return new float2x2(lhs.c0 + rhs, lhs.c1 + rhs);
		}

		// Token: 0x0600108E RID: 4238 RVA: 0x000300B8 File Offset: 0x0002E2B8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2x2 operator +(float lhs, float2x2 rhs)
		{
			return new float2x2(lhs + rhs.c0, lhs + rhs.c1);
		}

		// Token: 0x0600108F RID: 4239 RVA: 0x000300D7 File Offset: 0x0002E2D7
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2x2 operator -(float2x2 lhs, float2x2 rhs)
		{
			return new float2x2(lhs.c0 - rhs.c0, lhs.c1 - rhs.c1);
		}

		// Token: 0x06001090 RID: 4240 RVA: 0x00030100 File Offset: 0x0002E300
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2x2 operator -(float2x2 lhs, float rhs)
		{
			return new float2x2(lhs.c0 - rhs, lhs.c1 - rhs);
		}

		// Token: 0x06001091 RID: 4241 RVA: 0x0003011F File Offset: 0x0002E31F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2x2 operator -(float lhs, float2x2 rhs)
		{
			return new float2x2(lhs - rhs.c0, lhs - rhs.c1);
		}

		// Token: 0x06001092 RID: 4242 RVA: 0x0003013E File Offset: 0x0002E33E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2x2 operator /(float2x2 lhs, float2x2 rhs)
		{
			return new float2x2(lhs.c0 / rhs.c0, lhs.c1 / rhs.c1);
		}

		// Token: 0x06001093 RID: 4243 RVA: 0x00030167 File Offset: 0x0002E367
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2x2 operator /(float2x2 lhs, float rhs)
		{
			return new float2x2(lhs.c0 / rhs, lhs.c1 / rhs);
		}

		// Token: 0x06001094 RID: 4244 RVA: 0x00030186 File Offset: 0x0002E386
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2x2 operator /(float lhs, float2x2 rhs)
		{
			return new float2x2(lhs / rhs.c0, lhs / rhs.c1);
		}

		// Token: 0x06001095 RID: 4245 RVA: 0x000301A5 File Offset: 0x0002E3A5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2x2 operator %(float2x2 lhs, float2x2 rhs)
		{
			return new float2x2(lhs.c0 % rhs.c0, lhs.c1 % rhs.c1);
		}

		// Token: 0x06001096 RID: 4246 RVA: 0x000301CE File Offset: 0x0002E3CE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2x2 operator %(float2x2 lhs, float rhs)
		{
			return new float2x2(lhs.c0 % rhs, lhs.c1 % rhs);
		}

		// Token: 0x06001097 RID: 4247 RVA: 0x000301ED File Offset: 0x0002E3ED
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2x2 operator %(float lhs, float2x2 rhs)
		{
			return new float2x2(lhs % rhs.c0, lhs % rhs.c1);
		}

		// Token: 0x06001098 RID: 4248 RVA: 0x0003020C File Offset: 0x0002E40C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2x2 operator ++(float2x2 val)
		{
			float2 @float = ++val.c0;
			val.c0 = @float;
			float2 float2 = @float;
			@float = ++val.c1;
			val.c1 = @float;
			return new float2x2(float2, @float);
		}

		// Token: 0x06001099 RID: 4249 RVA: 0x00030254 File Offset: 0x0002E454
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2x2 operator --(float2x2 val)
		{
			float2 @float = --val.c0;
			val.c0 = @float;
			float2 float2 = @float;
			@float = --val.c1;
			val.c1 = @float;
			return new float2x2(float2, @float);
		}

		// Token: 0x0600109A RID: 4250 RVA: 0x0003029A File Offset: 0x0002E49A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x2 operator <(float2x2 lhs, float2x2 rhs)
		{
			return new bool2x2(lhs.c0 < rhs.c0, lhs.c1 < rhs.c1);
		}

		// Token: 0x0600109B RID: 4251 RVA: 0x000302C3 File Offset: 0x0002E4C3
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x2 operator <(float2x2 lhs, float rhs)
		{
			return new bool2x2(lhs.c0 < rhs, lhs.c1 < rhs);
		}

		// Token: 0x0600109C RID: 4252 RVA: 0x000302E2 File Offset: 0x0002E4E2
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x2 operator <(float lhs, float2x2 rhs)
		{
			return new bool2x2(lhs < rhs.c0, lhs < rhs.c1);
		}

		// Token: 0x0600109D RID: 4253 RVA: 0x00030301 File Offset: 0x0002E501
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x2 operator <=(float2x2 lhs, float2x2 rhs)
		{
			return new bool2x2(lhs.c0 <= rhs.c0, lhs.c1 <= rhs.c1);
		}

		// Token: 0x0600109E RID: 4254 RVA: 0x0003032A File Offset: 0x0002E52A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x2 operator <=(float2x2 lhs, float rhs)
		{
			return new bool2x2(lhs.c0 <= rhs, lhs.c1 <= rhs);
		}

		// Token: 0x0600109F RID: 4255 RVA: 0x00030349 File Offset: 0x0002E549
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x2 operator <=(float lhs, float2x2 rhs)
		{
			return new bool2x2(lhs <= rhs.c0, lhs <= rhs.c1);
		}

		// Token: 0x060010A0 RID: 4256 RVA: 0x00030368 File Offset: 0x0002E568
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x2 operator >(float2x2 lhs, float2x2 rhs)
		{
			return new bool2x2(lhs.c0 > rhs.c0, lhs.c1 > rhs.c1);
		}

		// Token: 0x060010A1 RID: 4257 RVA: 0x00030391 File Offset: 0x0002E591
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x2 operator >(float2x2 lhs, float rhs)
		{
			return new bool2x2(lhs.c0 > rhs, lhs.c1 > rhs);
		}

		// Token: 0x060010A2 RID: 4258 RVA: 0x000303B0 File Offset: 0x0002E5B0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x2 operator >(float lhs, float2x2 rhs)
		{
			return new bool2x2(lhs > rhs.c0, lhs > rhs.c1);
		}

		// Token: 0x060010A3 RID: 4259 RVA: 0x000303CF File Offset: 0x0002E5CF
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x2 operator >=(float2x2 lhs, float2x2 rhs)
		{
			return new bool2x2(lhs.c0 >= rhs.c0, lhs.c1 >= rhs.c1);
		}

		// Token: 0x060010A4 RID: 4260 RVA: 0x000303F8 File Offset: 0x0002E5F8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x2 operator >=(float2x2 lhs, float rhs)
		{
			return new bool2x2(lhs.c0 >= rhs, lhs.c1 >= rhs);
		}

		// Token: 0x060010A5 RID: 4261 RVA: 0x00030417 File Offset: 0x0002E617
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x2 operator >=(float lhs, float2x2 rhs)
		{
			return new bool2x2(lhs >= rhs.c0, lhs >= rhs.c1);
		}

		// Token: 0x060010A6 RID: 4262 RVA: 0x00030436 File Offset: 0x0002E636
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2x2 operator -(float2x2 val)
		{
			return new float2x2(-val.c0, -val.c1);
		}

		// Token: 0x060010A7 RID: 4263 RVA: 0x00030453 File Offset: 0x0002E653
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2x2 operator +(float2x2 val)
		{
			return new float2x2(+val.c0, +val.c1);
		}

		// Token: 0x060010A8 RID: 4264 RVA: 0x00030470 File Offset: 0x0002E670
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x2 operator ==(float2x2 lhs, float2x2 rhs)
		{
			return new bool2x2(lhs.c0 == rhs.c0, lhs.c1 == rhs.c1);
		}

		// Token: 0x060010A9 RID: 4265 RVA: 0x00030499 File Offset: 0x0002E699
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x2 operator ==(float2x2 lhs, float rhs)
		{
			return new bool2x2(lhs.c0 == rhs, lhs.c1 == rhs);
		}

		// Token: 0x060010AA RID: 4266 RVA: 0x000304B8 File Offset: 0x0002E6B8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x2 operator ==(float lhs, float2x2 rhs)
		{
			return new bool2x2(lhs == rhs.c0, lhs == rhs.c1);
		}

		// Token: 0x060010AB RID: 4267 RVA: 0x000304D7 File Offset: 0x0002E6D7
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x2 operator !=(float2x2 lhs, float2x2 rhs)
		{
			return new bool2x2(lhs.c0 != rhs.c0, lhs.c1 != rhs.c1);
		}

		// Token: 0x060010AC RID: 4268 RVA: 0x00030500 File Offset: 0x0002E700
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x2 operator !=(float2x2 lhs, float rhs)
		{
			return new bool2x2(lhs.c0 != rhs, lhs.c1 != rhs);
		}

		// Token: 0x060010AD RID: 4269 RVA: 0x0003051F File Offset: 0x0002E71F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x2 operator !=(float lhs, float2x2 rhs)
		{
			return new bool2x2(lhs != rhs.c0, lhs != rhs.c1);
		}

		// Token: 0x170003F8 RID: 1016
		public unsafe float2 this[int index]
		{
			get
			{
				fixed (float2x2* ptr = &this)
				{
					return ref *(float2*)(ptr + (IntPtr)index * (IntPtr)sizeof(float2) / (IntPtr)sizeof(float2x2));
				}
			}
		}

		// Token: 0x060010AF RID: 4271 RVA: 0x0003055B File Offset: 0x0002E75B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool Equals(float2x2 rhs)
		{
			return this.c0.Equals(rhs.c0) && this.c1.Equals(rhs.c1);
		}

		// Token: 0x060010B0 RID: 4272 RVA: 0x00030584 File Offset: 0x0002E784
		public override bool Equals(object o)
		{
			if (o is float2x2)
			{
				float2x2 rhs = (float2x2)o;
				return this.Equals(rhs);
			}
			return false;
		}

		// Token: 0x060010B1 RID: 4273 RVA: 0x000305A9 File Offset: 0x0002E7A9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override int GetHashCode()
		{
			return (int)math.hash(this);
		}

		// Token: 0x060010B2 RID: 4274 RVA: 0x000305B8 File Offset: 0x0002E7B8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override string ToString()
		{
			return string.Format("float2x2({0}f, {1}f,  {2}f, {3}f)", new object[]
			{
				this.c0.x,
				this.c1.x,
				this.c0.y,
				this.c1.y
			});
		}

		// Token: 0x060010B3 RID: 4275 RVA: 0x00030624 File Offset: 0x0002E824
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public string ToString(string format, IFormatProvider formatProvider)
		{
			return string.Format("float2x2({0}f, {1}f,  {2}f, {3}f)", new object[]
			{
				this.c0.x.ToString(format, formatProvider),
				this.c1.x.ToString(format, formatProvider),
				this.c0.y.ToString(format, formatProvider),
				this.c1.y.ToString(format, formatProvider)
			});
		}

		// Token: 0x060010B4 RID: 4276 RVA: 0x00030698 File Offset: 0x0002E898
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2x2 Rotate(float angle)
		{
			float num;
			float num2;
			math.sincos(angle, out num, out num2);
			return math.float2x2(num2, -num, num, num2);
		}

		// Token: 0x060010B5 RID: 4277 RVA: 0x000306B9 File Offset: 0x0002E8B9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2x2 Scale(float s)
		{
			return math.float2x2(s, 0f, 0f, s);
		}

		// Token: 0x060010B6 RID: 4278 RVA: 0x000306CC File Offset: 0x0002E8CC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2x2 Scale(float x, float y)
		{
			return math.float2x2(x, 0f, 0f, y);
		}

		// Token: 0x060010B7 RID: 4279 RVA: 0x000306DF File Offset: 0x0002E8DF
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2x2 Scale(float2 v)
		{
			return float2x2.Scale(v.x, v.y);
		}

		// Token: 0x060010B8 RID: 4280 RVA: 0x000306F2 File Offset: 0x0002E8F2
		// Note: this type is marked as 'beforefieldinit'.
		static float2x2()
		{
		}

		// Token: 0x04000071 RID: 113
		public float2 c0;

		// Token: 0x04000072 RID: 114
		public float2 c1;

		// Token: 0x04000073 RID: 115
		public static readonly float2x2 identity = new float2x2(1f, 0f, 0f, 1f);

		// Token: 0x04000074 RID: 116
		public static readonly float2x2 zero;
	}
}

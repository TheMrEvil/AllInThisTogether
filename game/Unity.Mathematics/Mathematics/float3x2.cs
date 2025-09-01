using System;
using System.Runtime.CompilerServices;
using Unity.IL2CPP.CompilerServices;

namespace Unity.Mathematics
{
	// Token: 0x02000021 RID: 33
	[Il2CppEagerStaticClassConstruction]
	[Serializable]
	public struct float3x2 : IEquatable<float3x2>, IFormattable
	{
		// Token: 0x06001200 RID: 4608 RVA: 0x00033AC8 File Offset: 0x00031CC8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float3x2(float3 c0, float3 c1)
		{
			this.c0 = c0;
			this.c1 = c1;
		}

		// Token: 0x06001201 RID: 4609 RVA: 0x00033AD8 File Offset: 0x00031CD8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float3x2(float m00, float m01, float m10, float m11, float m20, float m21)
		{
			this.c0 = new float3(m00, m10, m20);
			this.c1 = new float3(m01, m11, m21);
		}

		// Token: 0x06001202 RID: 4610 RVA: 0x00033AF9 File Offset: 0x00031CF9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float3x2(float v)
		{
			this.c0 = v;
			this.c1 = v;
		}

		// Token: 0x06001203 RID: 4611 RVA: 0x00033B14 File Offset: 0x00031D14
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float3x2(bool v)
		{
			this.c0 = math.select(new float3(0f), new float3(1f), v);
			this.c1 = math.select(new float3(0f), new float3(1f), v);
		}

		// Token: 0x06001204 RID: 4612 RVA: 0x00033B64 File Offset: 0x00031D64
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float3x2(bool3x2 v)
		{
			this.c0 = math.select(new float3(0f), new float3(1f), v.c0);
			this.c1 = math.select(new float3(0f), new float3(1f), v.c1);
		}

		// Token: 0x06001205 RID: 4613 RVA: 0x00033BBB File Offset: 0x00031DBB
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float3x2(int v)
		{
			this.c0 = v;
			this.c1 = v;
		}

		// Token: 0x06001206 RID: 4614 RVA: 0x00033BD5 File Offset: 0x00031DD5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float3x2(int3x2 v)
		{
			this.c0 = v.c0;
			this.c1 = v.c1;
		}

		// Token: 0x06001207 RID: 4615 RVA: 0x00033BF9 File Offset: 0x00031DF9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float3x2(uint v)
		{
			this.c0 = v;
			this.c1 = v;
		}

		// Token: 0x06001208 RID: 4616 RVA: 0x00033C13 File Offset: 0x00031E13
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float3x2(uint3x2 v)
		{
			this.c0 = v.c0;
			this.c1 = v.c1;
		}

		// Token: 0x06001209 RID: 4617 RVA: 0x00033C37 File Offset: 0x00031E37
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float3x2(double v)
		{
			this.c0 = (float3)v;
			this.c1 = (float3)v;
		}

		// Token: 0x0600120A RID: 4618 RVA: 0x00033C51 File Offset: 0x00031E51
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float3x2(double3x2 v)
		{
			this.c0 = (float3)v.c0;
			this.c1 = (float3)v.c1;
		}

		// Token: 0x0600120B RID: 4619 RVA: 0x00033C75 File Offset: 0x00031E75
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator float3x2(float v)
		{
			return new float3x2(v);
		}

		// Token: 0x0600120C RID: 4620 RVA: 0x00033C7D File Offset: 0x00031E7D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator float3x2(bool v)
		{
			return new float3x2(v);
		}

		// Token: 0x0600120D RID: 4621 RVA: 0x00033C85 File Offset: 0x00031E85
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator float3x2(bool3x2 v)
		{
			return new float3x2(v);
		}

		// Token: 0x0600120E RID: 4622 RVA: 0x00033C8D File Offset: 0x00031E8D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator float3x2(int v)
		{
			return new float3x2(v);
		}

		// Token: 0x0600120F RID: 4623 RVA: 0x00033C95 File Offset: 0x00031E95
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator float3x2(int3x2 v)
		{
			return new float3x2(v);
		}

		// Token: 0x06001210 RID: 4624 RVA: 0x00033C9D File Offset: 0x00031E9D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator float3x2(uint v)
		{
			return new float3x2(v);
		}

		// Token: 0x06001211 RID: 4625 RVA: 0x00033CA5 File Offset: 0x00031EA5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator float3x2(uint3x2 v)
		{
			return new float3x2(v);
		}

		// Token: 0x06001212 RID: 4626 RVA: 0x00033CAD File Offset: 0x00031EAD
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator float3x2(double v)
		{
			return new float3x2(v);
		}

		// Token: 0x06001213 RID: 4627 RVA: 0x00033CB5 File Offset: 0x00031EB5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator float3x2(double3x2 v)
		{
			return new float3x2(v);
		}

		// Token: 0x06001214 RID: 4628 RVA: 0x00033CBD File Offset: 0x00031EBD
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3x2 operator *(float3x2 lhs, float3x2 rhs)
		{
			return new float3x2(lhs.c0 * rhs.c0, lhs.c1 * rhs.c1);
		}

		// Token: 0x06001215 RID: 4629 RVA: 0x00033CE6 File Offset: 0x00031EE6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3x2 operator *(float3x2 lhs, float rhs)
		{
			return new float3x2(lhs.c0 * rhs, lhs.c1 * rhs);
		}

		// Token: 0x06001216 RID: 4630 RVA: 0x00033D05 File Offset: 0x00031F05
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3x2 operator *(float lhs, float3x2 rhs)
		{
			return new float3x2(lhs * rhs.c0, lhs * rhs.c1);
		}

		// Token: 0x06001217 RID: 4631 RVA: 0x00033D24 File Offset: 0x00031F24
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3x2 operator +(float3x2 lhs, float3x2 rhs)
		{
			return new float3x2(lhs.c0 + rhs.c0, lhs.c1 + rhs.c1);
		}

		// Token: 0x06001218 RID: 4632 RVA: 0x00033D4D File Offset: 0x00031F4D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3x2 operator +(float3x2 lhs, float rhs)
		{
			return new float3x2(lhs.c0 + rhs, lhs.c1 + rhs);
		}

		// Token: 0x06001219 RID: 4633 RVA: 0x00033D6C File Offset: 0x00031F6C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3x2 operator +(float lhs, float3x2 rhs)
		{
			return new float3x2(lhs + rhs.c0, lhs + rhs.c1);
		}

		// Token: 0x0600121A RID: 4634 RVA: 0x00033D8B File Offset: 0x00031F8B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3x2 operator -(float3x2 lhs, float3x2 rhs)
		{
			return new float3x2(lhs.c0 - rhs.c0, lhs.c1 - rhs.c1);
		}

		// Token: 0x0600121B RID: 4635 RVA: 0x00033DB4 File Offset: 0x00031FB4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3x2 operator -(float3x2 lhs, float rhs)
		{
			return new float3x2(lhs.c0 - rhs, lhs.c1 - rhs);
		}

		// Token: 0x0600121C RID: 4636 RVA: 0x00033DD3 File Offset: 0x00031FD3
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3x2 operator -(float lhs, float3x2 rhs)
		{
			return new float3x2(lhs - rhs.c0, lhs - rhs.c1);
		}

		// Token: 0x0600121D RID: 4637 RVA: 0x00033DF2 File Offset: 0x00031FF2
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3x2 operator /(float3x2 lhs, float3x2 rhs)
		{
			return new float3x2(lhs.c0 / rhs.c0, lhs.c1 / rhs.c1);
		}

		// Token: 0x0600121E RID: 4638 RVA: 0x00033E1B File Offset: 0x0003201B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3x2 operator /(float3x2 lhs, float rhs)
		{
			return new float3x2(lhs.c0 / rhs, lhs.c1 / rhs);
		}

		// Token: 0x0600121F RID: 4639 RVA: 0x00033E3A File Offset: 0x0003203A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3x2 operator /(float lhs, float3x2 rhs)
		{
			return new float3x2(lhs / rhs.c0, lhs / rhs.c1);
		}

		// Token: 0x06001220 RID: 4640 RVA: 0x00033E59 File Offset: 0x00032059
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3x2 operator %(float3x2 lhs, float3x2 rhs)
		{
			return new float3x2(lhs.c0 % rhs.c0, lhs.c1 % rhs.c1);
		}

		// Token: 0x06001221 RID: 4641 RVA: 0x00033E82 File Offset: 0x00032082
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3x2 operator %(float3x2 lhs, float rhs)
		{
			return new float3x2(lhs.c0 % rhs, lhs.c1 % rhs);
		}

		// Token: 0x06001222 RID: 4642 RVA: 0x00033EA1 File Offset: 0x000320A1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3x2 operator %(float lhs, float3x2 rhs)
		{
			return new float3x2(lhs % rhs.c0, lhs % rhs.c1);
		}

		// Token: 0x06001223 RID: 4643 RVA: 0x00033EC0 File Offset: 0x000320C0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3x2 operator ++(float3x2 val)
		{
			float3 @float = ++val.c0;
			val.c0 = @float;
			float3 float2 = @float;
			@float = ++val.c1;
			val.c1 = @float;
			return new float3x2(float2, @float);
		}

		// Token: 0x06001224 RID: 4644 RVA: 0x00033F08 File Offset: 0x00032108
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3x2 operator --(float3x2 val)
		{
			float3 @float = --val.c0;
			val.c0 = @float;
			float3 float2 = @float;
			@float = --val.c1;
			val.c1 = @float;
			return new float3x2(float2, @float);
		}

		// Token: 0x06001225 RID: 4645 RVA: 0x00033F4E File Offset: 0x0003214E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x2 operator <(float3x2 lhs, float3x2 rhs)
		{
			return new bool3x2(lhs.c0 < rhs.c0, lhs.c1 < rhs.c1);
		}

		// Token: 0x06001226 RID: 4646 RVA: 0x00033F77 File Offset: 0x00032177
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x2 operator <(float3x2 lhs, float rhs)
		{
			return new bool3x2(lhs.c0 < rhs, lhs.c1 < rhs);
		}

		// Token: 0x06001227 RID: 4647 RVA: 0x00033F96 File Offset: 0x00032196
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x2 operator <(float lhs, float3x2 rhs)
		{
			return new bool3x2(lhs < rhs.c0, lhs < rhs.c1);
		}

		// Token: 0x06001228 RID: 4648 RVA: 0x00033FB5 File Offset: 0x000321B5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x2 operator <=(float3x2 lhs, float3x2 rhs)
		{
			return new bool3x2(lhs.c0 <= rhs.c0, lhs.c1 <= rhs.c1);
		}

		// Token: 0x06001229 RID: 4649 RVA: 0x00033FDE File Offset: 0x000321DE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x2 operator <=(float3x2 lhs, float rhs)
		{
			return new bool3x2(lhs.c0 <= rhs, lhs.c1 <= rhs);
		}

		// Token: 0x0600122A RID: 4650 RVA: 0x00033FFD File Offset: 0x000321FD
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x2 operator <=(float lhs, float3x2 rhs)
		{
			return new bool3x2(lhs <= rhs.c0, lhs <= rhs.c1);
		}

		// Token: 0x0600122B RID: 4651 RVA: 0x0003401C File Offset: 0x0003221C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x2 operator >(float3x2 lhs, float3x2 rhs)
		{
			return new bool3x2(lhs.c0 > rhs.c0, lhs.c1 > rhs.c1);
		}

		// Token: 0x0600122C RID: 4652 RVA: 0x00034045 File Offset: 0x00032245
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x2 operator >(float3x2 lhs, float rhs)
		{
			return new bool3x2(lhs.c0 > rhs, lhs.c1 > rhs);
		}

		// Token: 0x0600122D RID: 4653 RVA: 0x00034064 File Offset: 0x00032264
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x2 operator >(float lhs, float3x2 rhs)
		{
			return new bool3x2(lhs > rhs.c0, lhs > rhs.c1);
		}

		// Token: 0x0600122E RID: 4654 RVA: 0x00034083 File Offset: 0x00032283
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x2 operator >=(float3x2 lhs, float3x2 rhs)
		{
			return new bool3x2(lhs.c0 >= rhs.c0, lhs.c1 >= rhs.c1);
		}

		// Token: 0x0600122F RID: 4655 RVA: 0x000340AC File Offset: 0x000322AC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x2 operator >=(float3x2 lhs, float rhs)
		{
			return new bool3x2(lhs.c0 >= rhs, lhs.c1 >= rhs);
		}

		// Token: 0x06001230 RID: 4656 RVA: 0x000340CB File Offset: 0x000322CB
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x2 operator >=(float lhs, float3x2 rhs)
		{
			return new bool3x2(lhs >= rhs.c0, lhs >= rhs.c1);
		}

		// Token: 0x06001231 RID: 4657 RVA: 0x000340EA File Offset: 0x000322EA
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3x2 operator -(float3x2 val)
		{
			return new float3x2(-val.c0, -val.c1);
		}

		// Token: 0x06001232 RID: 4658 RVA: 0x00034107 File Offset: 0x00032307
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3x2 operator +(float3x2 val)
		{
			return new float3x2(+val.c0, +val.c1);
		}

		// Token: 0x06001233 RID: 4659 RVA: 0x00034124 File Offset: 0x00032324
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x2 operator ==(float3x2 lhs, float3x2 rhs)
		{
			return new bool3x2(lhs.c0 == rhs.c0, lhs.c1 == rhs.c1);
		}

		// Token: 0x06001234 RID: 4660 RVA: 0x0003414D File Offset: 0x0003234D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x2 operator ==(float3x2 lhs, float rhs)
		{
			return new bool3x2(lhs.c0 == rhs, lhs.c1 == rhs);
		}

		// Token: 0x06001235 RID: 4661 RVA: 0x0003416C File Offset: 0x0003236C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x2 operator ==(float lhs, float3x2 rhs)
		{
			return new bool3x2(lhs == rhs.c0, lhs == rhs.c1);
		}

		// Token: 0x06001236 RID: 4662 RVA: 0x0003418B File Offset: 0x0003238B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x2 operator !=(float3x2 lhs, float3x2 rhs)
		{
			return new bool3x2(lhs.c0 != rhs.c0, lhs.c1 != rhs.c1);
		}

		// Token: 0x06001237 RID: 4663 RVA: 0x000341B4 File Offset: 0x000323B4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x2 operator !=(float3x2 lhs, float rhs)
		{
			return new bool3x2(lhs.c0 != rhs, lhs.c1 != rhs);
		}

		// Token: 0x06001238 RID: 4664 RVA: 0x000341D3 File Offset: 0x000323D3
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x2 operator !=(float lhs, float3x2 rhs)
		{
			return new bool3x2(lhs != rhs.c0, lhs != rhs.c1);
		}

		// Token: 0x17000471 RID: 1137
		public unsafe float3 this[int index]
		{
			get
			{
				fixed (float3x2* ptr = &this)
				{
					return ref *(float3*)(ptr + (IntPtr)index * (IntPtr)sizeof(float3) / (IntPtr)sizeof(float3x2));
				}
			}
		}

		// Token: 0x0600123A RID: 4666 RVA: 0x0003420F File Offset: 0x0003240F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool Equals(float3x2 rhs)
		{
			return this.c0.Equals(rhs.c0) && this.c1.Equals(rhs.c1);
		}

		// Token: 0x0600123B RID: 4667 RVA: 0x00034238 File Offset: 0x00032438
		public override bool Equals(object o)
		{
			if (o is float3x2)
			{
				float3x2 rhs = (float3x2)o;
				return this.Equals(rhs);
			}
			return false;
		}

		// Token: 0x0600123C RID: 4668 RVA: 0x0003425D File Offset: 0x0003245D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override int GetHashCode()
		{
			return (int)math.hash(this);
		}

		// Token: 0x0600123D RID: 4669 RVA: 0x0003426C File Offset: 0x0003246C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override string ToString()
		{
			return string.Format("float3x2({0}f, {1}f,  {2}f, {3}f,  {4}f, {5}f)", new object[]
			{
				this.c0.x,
				this.c1.x,
				this.c0.y,
				this.c1.y,
				this.c0.z,
				this.c1.z
			});
		}

		// Token: 0x0600123E RID: 4670 RVA: 0x000342FC File Offset: 0x000324FC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public string ToString(string format, IFormatProvider formatProvider)
		{
			return string.Format("float3x2({0}f, {1}f,  {2}f, {3}f,  {4}f, {5}f)", new object[]
			{
				this.c0.x.ToString(format, formatProvider),
				this.c1.x.ToString(format, formatProvider),
				this.c0.y.ToString(format, formatProvider),
				this.c1.y.ToString(format, formatProvider),
				this.c0.z.ToString(format, formatProvider),
				this.c1.z.ToString(format, formatProvider)
			});
		}

		// Token: 0x04000082 RID: 130
		public float3 c0;

		// Token: 0x04000083 RID: 131
		public float3 c1;

		// Token: 0x04000084 RID: 132
		public static readonly float3x2 zero;
	}
}

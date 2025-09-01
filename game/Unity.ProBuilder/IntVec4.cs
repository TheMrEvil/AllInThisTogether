using System;

namespace UnityEngine.ProBuilder
{
	// Token: 0x02000022 RID: 34
	internal struct IntVec4 : IEquatable<IntVec4>
	{
		// Token: 0x1700003F RID: 63
		// (get) Token: 0x06000142 RID: 322 RVA: 0x00011482 File Offset: 0x0000F682
		public float x
		{
			get
			{
				return this.value.x;
			}
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x06000143 RID: 323 RVA: 0x0001148F File Offset: 0x0000F68F
		public float y
		{
			get
			{
				return this.value.y;
			}
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x06000144 RID: 324 RVA: 0x0001149C File Offset: 0x0000F69C
		public float z
		{
			get
			{
				return this.value.z;
			}
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x06000145 RID: 325 RVA: 0x000114A9 File Offset: 0x0000F6A9
		public float w
		{
			get
			{
				return this.value.w;
			}
		}

		// Token: 0x06000146 RID: 326 RVA: 0x000114B6 File Offset: 0x0000F6B6
		public IntVec4(Vector4 vector)
		{
			this.value = vector;
		}

		// Token: 0x06000147 RID: 327 RVA: 0x000114C0 File Offset: 0x0000F6C0
		public override string ToString()
		{
			return string.Format("({0:F2}, {1:F2}, {2:F2}, {3:F2})", new object[]
			{
				this.x,
				this.y,
				this.z,
				this.w
			});
		}

		// Token: 0x06000148 RID: 328 RVA: 0x00011515 File Offset: 0x0000F715
		public static bool operator ==(IntVec4 a, IntVec4 b)
		{
			return a.Equals(b);
		}

		// Token: 0x06000149 RID: 329 RVA: 0x0001151F File Offset: 0x0000F71F
		public static bool operator !=(IntVec4 a, IntVec4 b)
		{
			return !(a == b);
		}

		// Token: 0x0600014A RID: 330 RVA: 0x0001152C File Offset: 0x0000F72C
		public bool Equals(IntVec4 p)
		{
			return IntVec4.round(this.x) == IntVec4.round(p.x) && IntVec4.round(this.y) == IntVec4.round(p.y) && IntVec4.round(this.z) == IntVec4.round(p.z) && IntVec4.round(this.w) == IntVec4.round(p.w);
		}

		// Token: 0x0600014B RID: 331 RVA: 0x000115A0 File Offset: 0x0000F7A0
		public bool Equals(Vector4 p)
		{
			return IntVec4.round(this.x) == IntVec4.round(p.x) && IntVec4.round(this.y) == IntVec4.round(p.y) && IntVec4.round(this.z) == IntVec4.round(p.z) && IntVec4.round(this.w) == IntVec4.round(p.w);
		}

		// Token: 0x0600014C RID: 332 RVA: 0x0001160F File Offset: 0x0000F80F
		public override bool Equals(object b)
		{
			return (b is IntVec4 && this.Equals((IntVec4)b)) || (b is Vector4 && this.Equals((Vector4)b));
		}

		// Token: 0x0600014D RID: 333 RVA: 0x0001163F File Offset: 0x0000F83F
		public override int GetHashCode()
		{
			return VectorHash.GetHashCode(this.value);
		}

		// Token: 0x0600014E RID: 334 RVA: 0x0001164C File Offset: 0x0000F84C
		private static int round(float v)
		{
			return Convert.ToInt32(v * 1000f);
		}

		// Token: 0x0600014F RID: 335 RVA: 0x0001165A File Offset: 0x0000F85A
		public static implicit operator Vector4(IntVec4 p)
		{
			return p.value;
		}

		// Token: 0x06000150 RID: 336 RVA: 0x00011662 File Offset: 0x0000F862
		public static implicit operator IntVec4(Vector4 p)
		{
			return new IntVec4(p);
		}

		// Token: 0x0400006A RID: 106
		public Vector4 value;
	}
}

using System;

namespace UnityEngine.ProBuilder
{
	// Token: 0x02000020 RID: 32
	internal struct IntVec2 : IEquatable<IntVec2>
	{
		// Token: 0x1700003A RID: 58
		// (get) Token: 0x06000127 RID: 295 RVA: 0x000111DF File Offset: 0x0000F3DF
		public float x
		{
			get
			{
				return this.value.x;
			}
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x06000128 RID: 296 RVA: 0x000111EC File Offset: 0x0000F3EC
		public float y
		{
			get
			{
				return this.value.y;
			}
		}

		// Token: 0x06000129 RID: 297 RVA: 0x000111F9 File Offset: 0x0000F3F9
		public IntVec2(Vector2 vector)
		{
			this.value = vector;
		}

		// Token: 0x0600012A RID: 298 RVA: 0x00011202 File Offset: 0x0000F402
		public override string ToString()
		{
			return string.Format("({0:F2}, {1:F2})", this.x, this.y);
		}

		// Token: 0x0600012B RID: 299 RVA: 0x00011224 File Offset: 0x0000F424
		public static bool operator ==(IntVec2 a, IntVec2 b)
		{
			return a.Equals(b);
		}

		// Token: 0x0600012C RID: 300 RVA: 0x0001122E File Offset: 0x0000F42E
		public static bool operator !=(IntVec2 a, IntVec2 b)
		{
			return !(a == b);
		}

		// Token: 0x0600012D RID: 301 RVA: 0x0001123A File Offset: 0x0000F43A
		public bool Equals(IntVec2 p)
		{
			return IntVec2.round(this.x) == IntVec2.round(p.x) && IntVec2.round(this.y) == IntVec2.round(p.y);
		}

		// Token: 0x0600012E RID: 302 RVA: 0x00011270 File Offset: 0x0000F470
		public bool Equals(Vector2 p)
		{
			return IntVec2.round(this.x) == IntVec2.round(p.x) && IntVec2.round(this.y) == IntVec2.round(p.y);
		}

		// Token: 0x0600012F RID: 303 RVA: 0x000112A4 File Offset: 0x0000F4A4
		public override bool Equals(object b)
		{
			return (b is IntVec2 && this.Equals((IntVec2)b)) || (b is Vector2 && this.Equals((Vector2)b));
		}

		// Token: 0x06000130 RID: 304 RVA: 0x000112D4 File Offset: 0x0000F4D4
		public override int GetHashCode()
		{
			return VectorHash.GetHashCode(this.value);
		}

		// Token: 0x06000131 RID: 305 RVA: 0x000112E1 File Offset: 0x0000F4E1
		private static int round(float v)
		{
			return Convert.ToInt32(v * 1000f);
		}

		// Token: 0x06000132 RID: 306 RVA: 0x000112EF File Offset: 0x0000F4EF
		public static implicit operator Vector2(IntVec2 p)
		{
			return p.value;
		}

		// Token: 0x06000133 RID: 307 RVA: 0x000112F7 File Offset: 0x0000F4F7
		public static implicit operator IntVec2(Vector2 p)
		{
			return new IntVec2(p);
		}

		// Token: 0x04000068 RID: 104
		public Vector2 value;
	}
}

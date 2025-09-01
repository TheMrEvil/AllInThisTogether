using System;

namespace UnityEngine.ProBuilder
{
	// Token: 0x02000021 RID: 33
	internal struct IntVec3 : IEquatable<IntVec3>
	{
		// Token: 0x1700003C RID: 60
		// (get) Token: 0x06000134 RID: 308 RVA: 0x000112FF File Offset: 0x0000F4FF
		public float x
		{
			get
			{
				return this.value.x;
			}
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x06000135 RID: 309 RVA: 0x0001130C File Offset: 0x0000F50C
		public float y
		{
			get
			{
				return this.value.y;
			}
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x06000136 RID: 310 RVA: 0x00011319 File Offset: 0x0000F519
		public float z
		{
			get
			{
				return this.value.z;
			}
		}

		// Token: 0x06000137 RID: 311 RVA: 0x00011326 File Offset: 0x0000F526
		public IntVec3(Vector3 vector)
		{
			this.value = vector;
		}

		// Token: 0x06000138 RID: 312 RVA: 0x0001132F File Offset: 0x0000F52F
		public override string ToString()
		{
			return string.Format("({0:F2}, {1:F2}, {2:F2})", this.x, this.y, this.z);
		}

		// Token: 0x06000139 RID: 313 RVA: 0x0001135C File Offset: 0x0000F55C
		public static bool operator ==(IntVec3 a, IntVec3 b)
		{
			return a.Equals(b);
		}

		// Token: 0x0600013A RID: 314 RVA: 0x00011366 File Offset: 0x0000F566
		public static bool operator !=(IntVec3 a, IntVec3 b)
		{
			return !(a == b);
		}

		// Token: 0x0600013B RID: 315 RVA: 0x00011374 File Offset: 0x0000F574
		public bool Equals(IntVec3 p)
		{
			return IntVec3.round(this.x) == IntVec3.round(p.x) && IntVec3.round(this.y) == IntVec3.round(p.y) && IntVec3.round(this.z) == IntVec3.round(p.z);
		}

		// Token: 0x0600013C RID: 316 RVA: 0x000113D0 File Offset: 0x0000F5D0
		public bool Equals(Vector3 p)
		{
			return IntVec3.round(this.x) == IntVec3.round(p.x) && IntVec3.round(this.y) == IntVec3.round(p.y) && IntVec3.round(this.z) == IntVec3.round(p.z);
		}

		// Token: 0x0600013D RID: 317 RVA: 0x00011427 File Offset: 0x0000F627
		public override bool Equals(object b)
		{
			return (b is IntVec3 && this.Equals((IntVec3)b)) || (b is Vector3 && this.Equals((Vector3)b));
		}

		// Token: 0x0600013E RID: 318 RVA: 0x00011457 File Offset: 0x0000F657
		public override int GetHashCode()
		{
			return VectorHash.GetHashCode(this.value);
		}

		// Token: 0x0600013F RID: 319 RVA: 0x00011464 File Offset: 0x0000F664
		private static int round(float v)
		{
			return Convert.ToInt32(v * 1000f);
		}

		// Token: 0x06000140 RID: 320 RVA: 0x00011472 File Offset: 0x0000F672
		public static implicit operator Vector3(IntVec3 p)
		{
			return p.value;
		}

		// Token: 0x06000141 RID: 321 RVA: 0x0001147A File Offset: 0x0000F67A
		public static implicit operator IntVec3(Vector3 p)
		{
			return new IntVec3(p);
		}

		// Token: 0x04000069 RID: 105
		public Vector3 value;
	}
}

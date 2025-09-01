using System;

namespace UnityEngine.ProBuilder
{
	// Token: 0x02000063 RID: 99
	internal struct Vector3Mask : IEquatable<Vector3Mask>
	{
		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x060003AE RID: 942 RVA: 0x000224D7 File Offset: 0x000206D7
		public float x
		{
			get
			{
				if ((this.m_Mask & 1) != 1)
				{
					return 0f;
				}
				return 1f;
			}
		}

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x060003AF RID: 943 RVA: 0x000224EF File Offset: 0x000206EF
		public float y
		{
			get
			{
				if ((this.m_Mask & 2) != 2)
				{
					return 0f;
				}
				return 1f;
			}
		}

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x060003B0 RID: 944 RVA: 0x00022507 File Offset: 0x00020707
		public float z
		{
			get
			{
				if ((this.m_Mask & 4) != 4)
				{
					return 0f;
				}
				return 1f;
			}
		}

		// Token: 0x060003B1 RID: 945 RVA: 0x00022520 File Offset: 0x00020720
		public Vector3Mask(Vector3 v, float epsilon = 1E-45f)
		{
			this.m_Mask = 0;
			if (Mathf.Abs(v.x) > epsilon)
			{
				this.m_Mask |= 1;
			}
			if (Mathf.Abs(v.y) > epsilon)
			{
				this.m_Mask |= 2;
			}
			if (Mathf.Abs(v.z) > epsilon)
			{
				this.m_Mask |= 4;
			}
		}

		// Token: 0x060003B2 RID: 946 RVA: 0x0002258B File Offset: 0x0002078B
		public Vector3Mask(byte mask)
		{
			this.m_Mask = mask;
		}

		// Token: 0x060003B3 RID: 947 RVA: 0x00022594 File Offset: 0x00020794
		public override string ToString()
		{
			return string.Format("{{{0}, {1}, {2}}}", this.x, this.y, this.z);
		}

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x060003B4 RID: 948 RVA: 0x000225C4 File Offset: 0x000207C4
		public int active
		{
			get
			{
				int num = 0;
				if ((this.m_Mask & 1) > 0)
				{
					num++;
				}
				if ((this.m_Mask & 2) > 0)
				{
					num++;
				}
				if ((this.m_Mask & 4) > 0)
				{
					num++;
				}
				return num;
			}
		}

		// Token: 0x060003B5 RID: 949 RVA: 0x00022601 File Offset: 0x00020801
		public static implicit operator Vector3(Vector3Mask mask)
		{
			return new Vector3(mask.x, mask.y, mask.z);
		}

		// Token: 0x060003B6 RID: 950 RVA: 0x0002261D File Offset: 0x0002081D
		public static explicit operator Vector3Mask(Vector3 v)
		{
			return new Vector3Mask(v, float.Epsilon);
		}

		// Token: 0x060003B7 RID: 951 RVA: 0x0002262A File Offset: 0x0002082A
		public static Vector3Mask operator |(Vector3Mask left, Vector3Mask right)
		{
			return new Vector3Mask(left.m_Mask | right.m_Mask);
		}

		// Token: 0x060003B8 RID: 952 RVA: 0x0002263F File Offset: 0x0002083F
		public static Vector3Mask operator &(Vector3Mask left, Vector3Mask right)
		{
			return new Vector3Mask(left.m_Mask & right.m_Mask);
		}

		// Token: 0x060003B9 RID: 953 RVA: 0x00022654 File Offset: 0x00020854
		public static Vector3Mask operator ^(Vector3Mask left, Vector3Mask right)
		{
			return new Vector3Mask(left.m_Mask ^ right.m_Mask);
		}

		// Token: 0x060003BA RID: 954 RVA: 0x00022669 File Offset: 0x00020869
		public static Vector3 operator *(Vector3Mask mask, float value)
		{
			return new Vector3(mask.x * value, mask.y * value, mask.z * value);
		}

		// Token: 0x060003BB RID: 955 RVA: 0x0002268B File Offset: 0x0002088B
		public static Vector3 operator *(Vector3Mask mask, Vector3 value)
		{
			return new Vector3(mask.x * value.x, mask.y * value.y, mask.z * value.z);
		}

		// Token: 0x060003BC RID: 956 RVA: 0x000226BC File Offset: 0x000208BC
		public static Vector3 operator *(Quaternion rotation, Vector3Mask mask)
		{
			int active = mask.active;
			if (active > 2)
			{
				return mask;
			}
			Vector3 vector = (rotation * mask).Abs();
			if (active > 1)
			{
				return new Vector3((float)((vector.x > vector.y || vector.x > vector.z) ? 1 : 0), (float)((vector.y > vector.x || vector.y > vector.z) ? 1 : 0), (float)((vector.z > vector.x || vector.z > vector.y) ? 1 : 0));
			}
			return new Vector3((float)((vector.x > vector.y && vector.x > vector.z) ? 1 : 0), (float)((vector.y > vector.z && vector.y > vector.x) ? 1 : 0), (float)((vector.z > vector.x && vector.z > vector.y) ? 1 : 0));
		}

		// Token: 0x060003BD RID: 957 RVA: 0x000227C3 File Offset: 0x000209C3
		public static bool operator ==(Vector3Mask left, Vector3Mask right)
		{
			return left.m_Mask == right.m_Mask;
		}

		// Token: 0x060003BE RID: 958 RVA: 0x000227D3 File Offset: 0x000209D3
		public static bool operator !=(Vector3Mask left, Vector3Mask right)
		{
			return !(left == right);
		}

		// Token: 0x170000A9 RID: 169
		public float this[int i]
		{
			get
			{
				if (i < 0 || i > 2)
				{
					throw new IndexOutOfRangeException();
				}
				return (float)(1 & this.m_Mask >> i) * 1f;
			}
			set
			{
				if (i < 0 || i > 2)
				{
					throw new IndexOutOfRangeException();
				}
				this.m_Mask &= (byte)(~(byte)(1 << i));
				this.m_Mask |= (byte)(((value > 0f) ? 1 : 0) << (i & 31));
			}
		}

		// Token: 0x060003C1 RID: 961 RVA: 0x00022855 File Offset: 0x00020A55
		public bool Equals(Vector3Mask other)
		{
			return this.m_Mask == other.m_Mask;
		}

		// Token: 0x060003C2 RID: 962 RVA: 0x00022865 File Offset: 0x00020A65
		public override bool Equals(object obj)
		{
			return obj != null && obj is Vector3Mask && this.Equals((Vector3Mask)obj);
		}

		// Token: 0x060003C3 RID: 963 RVA: 0x00022882 File Offset: 0x00020A82
		public override int GetHashCode()
		{
			return this.m_Mask.GetHashCode();
		}

		// Token: 0x060003C4 RID: 964 RVA: 0x0002288F File Offset: 0x00020A8F
		// Note: this type is marked as 'beforefieldinit'.
		static Vector3Mask()
		{
		}

		// Token: 0x04000217 RID: 535
		private const byte X = 1;

		// Token: 0x04000218 RID: 536
		private const byte Y = 2;

		// Token: 0x04000219 RID: 537
		private const byte Z = 4;

		// Token: 0x0400021A RID: 538
		public static readonly Vector3Mask XYZ = new Vector3Mask(7);

		// Token: 0x0400021B RID: 539
		private byte m_Mask;
	}
}

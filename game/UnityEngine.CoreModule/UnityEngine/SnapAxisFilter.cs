using System;

namespace UnityEngine
{
	// Token: 0x0200023E RID: 574
	internal struct SnapAxisFilter : IEquatable<SnapAxisFilter>
	{
		// Token: 0x1700049F RID: 1183
		// (get) Token: 0x0600188E RID: 6286 RVA: 0x00027B04 File Offset: 0x00025D04
		public float x
		{
			get
			{
				return ((this.m_Mask & SnapAxis.X) == SnapAxis.X) ? 1f : 0f;
			}
		}

		// Token: 0x170004A0 RID: 1184
		// (get) Token: 0x0600188F RID: 6287 RVA: 0x00027B30 File Offset: 0x00025D30
		public float y
		{
			get
			{
				return ((this.m_Mask & SnapAxis.Y) == SnapAxis.Y) ? 1f : 0f;
			}
		}

		// Token: 0x170004A1 RID: 1185
		// (get) Token: 0x06001890 RID: 6288 RVA: 0x00027B5C File Offset: 0x00025D5C
		public float z
		{
			get
			{
				return ((this.m_Mask & SnapAxis.Z) == SnapAxis.Z) ? 1f : 0f;
			}
		}

		// Token: 0x06001891 RID: 6289 RVA: 0x00027B88 File Offset: 0x00025D88
		public SnapAxisFilter(Vector3 v)
		{
			this.m_Mask = SnapAxis.None;
			float num = 1E-06f;
			bool flag = Mathf.Abs(v.x) > num;
			if (flag)
			{
				this.m_Mask |= SnapAxis.X;
			}
			bool flag2 = Mathf.Abs(v.y) > num;
			if (flag2)
			{
				this.m_Mask |= SnapAxis.Y;
			}
			bool flag3 = Mathf.Abs(v.z) > num;
			if (flag3)
			{
				this.m_Mask |= SnapAxis.Z;
			}
		}

		// Token: 0x06001892 RID: 6290 RVA: 0x00027C04 File Offset: 0x00025E04
		public SnapAxisFilter(SnapAxis axis)
		{
			this.m_Mask = SnapAxis.None;
			bool flag = (axis & SnapAxis.X) == SnapAxis.X;
			if (flag)
			{
				this.m_Mask |= SnapAxis.X;
			}
			bool flag2 = (axis & SnapAxis.Y) == SnapAxis.Y;
			if (flag2)
			{
				this.m_Mask |= SnapAxis.Y;
			}
			bool flag3 = (axis & SnapAxis.Z) == SnapAxis.Z;
			if (flag3)
			{
				this.m_Mask |= SnapAxis.Z;
			}
		}

		// Token: 0x06001893 RID: 6291 RVA: 0x00027C64 File Offset: 0x00025E64
		public override string ToString()
		{
			return string.Format("{{{0}, {1}, {2}}}", this.x, this.y, this.z);
		}

		// Token: 0x170004A2 RID: 1186
		// (get) Token: 0x06001894 RID: 6292 RVA: 0x00027CA4 File Offset: 0x00025EA4
		public int active
		{
			get
			{
				int num = 0;
				bool flag = (this.m_Mask & SnapAxis.X) > SnapAxis.None;
				if (flag)
				{
					num++;
				}
				bool flag2 = (this.m_Mask & SnapAxis.Y) > SnapAxis.None;
				if (flag2)
				{
					num++;
				}
				bool flag3 = (this.m_Mask & SnapAxis.Z) > SnapAxis.None;
				if (flag3)
				{
					num++;
				}
				return num;
			}
		}

		// Token: 0x06001895 RID: 6293 RVA: 0x00027CF4 File Offset: 0x00025EF4
		public static implicit operator Vector3(SnapAxisFilter mask)
		{
			return new Vector3(mask.x, mask.y, mask.z);
		}

		// Token: 0x06001896 RID: 6294 RVA: 0x00027D20 File Offset: 0x00025F20
		public static explicit operator SnapAxisFilter(Vector3 v)
		{
			return new SnapAxisFilter(v);
		}

		// Token: 0x06001897 RID: 6295 RVA: 0x00027D38 File Offset: 0x00025F38
		public static explicit operator SnapAxis(SnapAxisFilter mask)
		{
			return mask.m_Mask;
		}

		// Token: 0x06001898 RID: 6296 RVA: 0x00027D50 File Offset: 0x00025F50
		public static SnapAxisFilter operator |(SnapAxisFilter left, SnapAxisFilter right)
		{
			return new SnapAxisFilter(left.m_Mask | right.m_Mask);
		}

		// Token: 0x06001899 RID: 6297 RVA: 0x00027D74 File Offset: 0x00025F74
		public static SnapAxisFilter operator &(SnapAxisFilter left, SnapAxisFilter right)
		{
			return new SnapAxisFilter(left.m_Mask & right.m_Mask);
		}

		// Token: 0x0600189A RID: 6298 RVA: 0x00027D98 File Offset: 0x00025F98
		public static SnapAxisFilter operator ^(SnapAxisFilter left, SnapAxisFilter right)
		{
			return new SnapAxisFilter(left.m_Mask ^ right.m_Mask);
		}

		// Token: 0x0600189B RID: 6299 RVA: 0x00027DBC File Offset: 0x00025FBC
		public static SnapAxisFilter operator ~(SnapAxisFilter left)
		{
			return new SnapAxisFilter(~left.m_Mask);
		}

		// Token: 0x0600189C RID: 6300 RVA: 0x00027DDC File Offset: 0x00025FDC
		public static Vector3 operator *(SnapAxisFilter mask, float value)
		{
			return new Vector3(mask.x * value, mask.y * value, mask.z * value);
		}

		// Token: 0x0600189D RID: 6301 RVA: 0x00027E10 File Offset: 0x00026010
		public static Vector3 operator *(SnapAxisFilter mask, Vector3 right)
		{
			return new Vector3(mask.x * right.x, mask.y * right.y, mask.z * right.z);
		}

		// Token: 0x0600189E RID: 6302 RVA: 0x00027E54 File Offset: 0x00026054
		public static Vector3 operator *(Quaternion rotation, SnapAxisFilter mask)
		{
			int active = mask.active;
			bool flag = active > 2;
			Vector3 result;
			if (flag)
			{
				result = mask;
			}
			else
			{
				Vector3 vector = rotation * mask;
				vector = new Vector3(Mathf.Abs(vector.x), Mathf.Abs(vector.y), Mathf.Abs(vector.z));
				bool flag2 = active > 1;
				if (flag2)
				{
					result = new Vector3((float)((vector.x > vector.y || vector.x > vector.z) ? 1 : 0), (float)((vector.y > vector.x || vector.y > vector.z) ? 1 : 0), (float)((vector.z > vector.x || vector.z > vector.y) ? 1 : 0));
				}
				else
				{
					result = new Vector3((float)((vector.x > vector.y && vector.x > vector.z) ? 1 : 0), (float)((vector.y > vector.z && vector.y > vector.x) ? 1 : 0), (float)((vector.z > vector.x && vector.z > vector.y) ? 1 : 0));
				}
			}
			return result;
		}

		// Token: 0x0600189F RID: 6303 RVA: 0x00027F98 File Offset: 0x00026198
		public static bool operator ==(SnapAxisFilter left, SnapAxisFilter right)
		{
			return left.m_Mask == right.m_Mask;
		}

		// Token: 0x060018A0 RID: 6304 RVA: 0x00027FB8 File Offset: 0x000261B8
		public static bool operator !=(SnapAxisFilter left, SnapAxisFilter right)
		{
			return !(left == right);
		}

		// Token: 0x170004A3 RID: 1187
		public float this[int i]
		{
			get
			{
				bool flag = i < 0 || i > 2;
				if (flag)
				{
					throw new IndexOutOfRangeException();
				}
				return (float)(SnapAxis.X & this.m_Mask >> (i & 31)) * 1f;
			}
			set
			{
				bool flag = i < 0 || i > 2;
				if (flag)
				{
					throw new IndexOutOfRangeException();
				}
				this.m_Mask &= (SnapAxis)(~(SnapAxis)(1 << i));
				this.m_Mask |= (SnapAxis)(((value > 0f) ? 1 : 0) << (i & 31));
			}
		}

		// Token: 0x060018A3 RID: 6307 RVA: 0x00028068 File Offset: 0x00026268
		public bool Equals(SnapAxisFilter other)
		{
			return this.m_Mask == other.m_Mask;
		}

		// Token: 0x060018A4 RID: 6308 RVA: 0x00028088 File Offset: 0x00026288
		public override bool Equals(object obj)
		{
			bool flag = obj == null;
			return !flag && obj is SnapAxisFilter && this.Equals((SnapAxisFilter)obj);
		}

		// Token: 0x060018A5 RID: 6309 RVA: 0x000280C0 File Offset: 0x000262C0
		public override int GetHashCode()
		{
			return this.m_Mask.GetHashCode();
		}

		// Token: 0x060018A6 RID: 6310 RVA: 0x000280E3 File Offset: 0x000262E3
		// Note: this type is marked as 'beforefieldinit'.
		static SnapAxisFilter()
		{
		}

		// Token: 0x04000848 RID: 2120
		private const SnapAxis X = SnapAxis.X;

		// Token: 0x04000849 RID: 2121
		private const SnapAxis Y = SnapAxis.Y;

		// Token: 0x0400084A RID: 2122
		private const SnapAxis Z = SnapAxis.Z;

		// Token: 0x0400084B RID: 2123
		public static readonly SnapAxisFilter all = new SnapAxisFilter(SnapAxis.All);

		// Token: 0x0400084C RID: 2124
		private SnapAxis m_Mask;
	}
}

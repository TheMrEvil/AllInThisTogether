using System;

namespace UnityEngine.ProBuilder
{
	// Token: 0x02000062 RID: 98
	internal struct Vector2Mask
	{
		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x060003A3 RID: 931 RVA: 0x000223C1 File Offset: 0x000205C1
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

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x060003A4 RID: 932 RVA: 0x000223D9 File Offset: 0x000205D9
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

		// Token: 0x060003A5 RID: 933 RVA: 0x000223F4 File Offset: 0x000205F4
		public Vector2Mask(Vector3 v, float epsilon = 1E-45f)
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
		}

		// Token: 0x060003A6 RID: 934 RVA: 0x00022442 File Offset: 0x00020642
		public Vector2Mask(byte mask)
		{
			this.m_Mask = mask;
		}

		// Token: 0x060003A7 RID: 935 RVA: 0x0002244B File Offset: 0x0002064B
		public static implicit operator Vector2(Vector2Mask mask)
		{
			return new Vector2(mask.x, mask.y);
		}

		// Token: 0x060003A8 RID: 936 RVA: 0x00022460 File Offset: 0x00020660
		public static implicit operator Vector2Mask(Vector2 v)
		{
			return new Vector2Mask(v, float.Epsilon);
		}

		// Token: 0x060003A9 RID: 937 RVA: 0x00022472 File Offset: 0x00020672
		public static Vector2Mask operator |(Vector2Mask left, Vector2Mask right)
		{
			return new Vector2Mask(left.m_Mask | right.m_Mask);
		}

		// Token: 0x060003AA RID: 938 RVA: 0x00022487 File Offset: 0x00020687
		public static Vector2Mask operator &(Vector2Mask left, Vector2Mask right)
		{
			return new Vector2Mask(left.m_Mask & right.m_Mask);
		}

		// Token: 0x060003AB RID: 939 RVA: 0x0002249C File Offset: 0x0002069C
		public static Vector2Mask operator ^(Vector2Mask left, Vector2Mask right)
		{
			return new Vector2Mask(left.m_Mask ^ right.m_Mask);
		}

		// Token: 0x060003AC RID: 940 RVA: 0x000224B1 File Offset: 0x000206B1
		public static Vector2 operator *(Vector2Mask mask, float value)
		{
			return new Vector2(mask.x * value, mask.y * value);
		}

		// Token: 0x060003AD RID: 941 RVA: 0x000224CA File Offset: 0x000206CA
		// Note: this type is marked as 'beforefieldinit'.
		static Vector2Mask()
		{
		}

		// Token: 0x04000213 RID: 531
		private const byte X = 1;

		// Token: 0x04000214 RID: 532
		private const byte Y = 2;

		// Token: 0x04000215 RID: 533
		public static readonly Vector2Mask XY = new Vector2Mask(3);

		// Token: 0x04000216 RID: 534
		private byte m_Mask;
	}
}

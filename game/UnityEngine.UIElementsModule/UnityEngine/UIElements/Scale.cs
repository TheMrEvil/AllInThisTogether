using System;

namespace UnityEngine.UIElements
{
	// Token: 0x0200027F RID: 639
	public struct Scale : IEquatable<Scale>
	{
		// Token: 0x060014B5 RID: 5301 RVA: 0x0005A53C File Offset: 0x0005873C
		public Scale(Vector3 scale)
		{
			this.m_Scale = new Vector3(scale.x, scale.y, 1f);
			this.m_IsNone = false;
		}

		// Token: 0x060014B6 RID: 5302 RVA: 0x0005A564 File Offset: 0x00058764
		internal static Scale Initial()
		{
			return new Scale(Vector3.one);
		}

		// Token: 0x060014B7 RID: 5303 RVA: 0x0005A580 File Offset: 0x00058780
		public static Scale None()
		{
			Scale result = Scale.Initial();
			result.m_IsNone = true;
			return result;
		}

		// Token: 0x170004DD RID: 1245
		// (get) Token: 0x060014B8 RID: 5304 RVA: 0x0005A5A1 File Offset: 0x000587A1
		// (set) Token: 0x060014B9 RID: 5305 RVA: 0x0005A5A9 File Offset: 0x000587A9
		public Vector3 value
		{
			get
			{
				return this.m_Scale;
			}
			set
			{
				this.m_Scale = new Vector3(value.x, value.y, 1f);
			}
		}

		// Token: 0x060014BA RID: 5306 RVA: 0x0005A5C7 File Offset: 0x000587C7
		internal bool IsNone()
		{
			return this.m_IsNone;
		}

		// Token: 0x060014BB RID: 5307 RVA: 0x0005A5D0 File Offset: 0x000587D0
		public static bool operator ==(Scale lhs, Scale rhs)
		{
			return lhs.m_Scale == rhs.m_Scale;
		}

		// Token: 0x060014BC RID: 5308 RVA: 0x0005A5F4 File Offset: 0x000587F4
		public static bool operator !=(Scale lhs, Scale rhs)
		{
			return !(lhs == rhs);
		}

		// Token: 0x060014BD RID: 5309 RVA: 0x0005A610 File Offset: 0x00058810
		public bool Equals(Scale other)
		{
			return other == this;
		}

		// Token: 0x060014BE RID: 5310 RVA: 0x0005A630 File Offset: 0x00058830
		public override bool Equals(object obj)
		{
			bool result;
			if (obj is Scale)
			{
				Scale other = (Scale)obj;
				result = this.Equals(other);
			}
			else
			{
				result = false;
			}
			return result;
		}

		// Token: 0x060014BF RID: 5311 RVA: 0x0005A65C File Offset: 0x0005885C
		public override int GetHashCode()
		{
			return this.m_Scale.GetHashCode() * 793;
		}

		// Token: 0x060014C0 RID: 5312 RVA: 0x0005A688 File Offset: 0x00058888
		public override string ToString()
		{
			return this.m_Scale.ToString();
		}

		// Token: 0x0400093B RID: 2363
		private Vector3 m_Scale;

		// Token: 0x0400093C RID: 2364
		private bool m_IsNone;
	}
}

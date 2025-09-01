using System;
using System.Globalization;

namespace UnityEngine
{
	// Token: 0x02000114 RID: 276
	public struct Ray : IFormattable
	{
		// Token: 0x060006F5 RID: 1781 RVA: 0x0000A28B File Offset: 0x0000848B
		public Ray(Vector3 origin, Vector3 direction)
		{
			this.m_Origin = origin;
			this.m_Direction = direction.normalized;
		}

		// Token: 0x17000184 RID: 388
		// (get) Token: 0x060006F6 RID: 1782 RVA: 0x0000A2A4 File Offset: 0x000084A4
		// (set) Token: 0x060006F7 RID: 1783 RVA: 0x0000A2BC File Offset: 0x000084BC
		public Vector3 origin
		{
			get
			{
				return this.m_Origin;
			}
			set
			{
				this.m_Origin = value;
			}
		}

		// Token: 0x17000185 RID: 389
		// (get) Token: 0x060006F8 RID: 1784 RVA: 0x0000A2C8 File Offset: 0x000084C8
		// (set) Token: 0x060006F9 RID: 1785 RVA: 0x0000A2E0 File Offset: 0x000084E0
		public Vector3 direction
		{
			get
			{
				return this.m_Direction;
			}
			set
			{
				this.m_Direction = value.normalized;
			}
		}

		// Token: 0x060006FA RID: 1786 RVA: 0x0000A2F0 File Offset: 0x000084F0
		public Vector3 GetPoint(float distance)
		{
			return this.m_Origin + this.m_Direction * distance;
		}

		// Token: 0x060006FB RID: 1787 RVA: 0x0000A31C File Offset: 0x0000851C
		public override string ToString()
		{
			return this.ToString(null, null);
		}

		// Token: 0x060006FC RID: 1788 RVA: 0x0000A338 File Offset: 0x00008538
		public string ToString(string format)
		{
			return this.ToString(format, null);
		}

		// Token: 0x060006FD RID: 1789 RVA: 0x0000A354 File Offset: 0x00008554
		public string ToString(string format, IFormatProvider formatProvider)
		{
			bool flag = string.IsNullOrEmpty(format);
			if (flag)
			{
				format = "F2";
			}
			bool flag2 = formatProvider == null;
			if (flag2)
			{
				formatProvider = CultureInfo.InvariantCulture.NumberFormat;
			}
			return UnityString.Format("Origin: {0}, Dir: {1}", new object[]
			{
				this.m_Origin.ToString(format, formatProvider),
				this.m_Direction.ToString(format, formatProvider)
			});
		}

		// Token: 0x04000391 RID: 913
		private Vector3 m_Origin;

		// Token: 0x04000392 RID: 914
		private Vector3 m_Direction;
	}
}

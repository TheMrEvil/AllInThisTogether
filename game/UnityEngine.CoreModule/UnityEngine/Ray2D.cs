using System;
using System.Globalization;

namespace UnityEngine
{
	// Token: 0x02000115 RID: 277
	public struct Ray2D : IFormattable
	{
		// Token: 0x060006FE RID: 1790 RVA: 0x0000A3BB File Offset: 0x000085BB
		public Ray2D(Vector2 origin, Vector2 direction)
		{
			this.m_Origin = origin;
			this.m_Direction = direction.normalized;
		}

		// Token: 0x17000186 RID: 390
		// (get) Token: 0x060006FF RID: 1791 RVA: 0x0000A3D4 File Offset: 0x000085D4
		// (set) Token: 0x06000700 RID: 1792 RVA: 0x0000A3EC File Offset: 0x000085EC
		public Vector2 origin
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

		// Token: 0x17000187 RID: 391
		// (get) Token: 0x06000701 RID: 1793 RVA: 0x0000A3F8 File Offset: 0x000085F8
		// (set) Token: 0x06000702 RID: 1794 RVA: 0x0000A410 File Offset: 0x00008610
		public Vector2 direction
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

		// Token: 0x06000703 RID: 1795 RVA: 0x0000A420 File Offset: 0x00008620
		public Vector2 GetPoint(float distance)
		{
			return this.m_Origin + this.m_Direction * distance;
		}

		// Token: 0x06000704 RID: 1796 RVA: 0x0000A44C File Offset: 0x0000864C
		public override string ToString()
		{
			return this.ToString(null, null);
		}

		// Token: 0x06000705 RID: 1797 RVA: 0x0000A468 File Offset: 0x00008668
		public string ToString(string format)
		{
			return this.ToString(format, null);
		}

		// Token: 0x06000706 RID: 1798 RVA: 0x0000A484 File Offset: 0x00008684
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

		// Token: 0x04000393 RID: 915
		private Vector2 m_Origin;

		// Token: 0x04000394 RID: 916
		private Vector2 m_Direction;
	}
}

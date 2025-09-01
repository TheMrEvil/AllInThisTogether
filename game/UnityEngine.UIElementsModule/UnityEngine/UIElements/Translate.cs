using System;
using System.Globalization;

namespace UnityEngine.UIElements
{
	// Token: 0x02000298 RID: 664
	public struct Translate : IEquatable<Translate>
	{
		// Token: 0x06001604 RID: 5636 RVA: 0x0005FFBF File Offset: 0x0005E1BF
		public Translate(Length x, Length y, float z)
		{
			this.m_X = x;
			this.m_Y = y;
			this.m_Z = z;
			this.m_isNone = false;
		}

		// Token: 0x06001605 RID: 5637 RVA: 0x0005FFDE File Offset: 0x0005E1DE
		public Translate(Length x, Length y)
		{
			this = new Translate(x, y, 0f);
		}

		// Token: 0x06001606 RID: 5638 RVA: 0x0005FFF0 File Offset: 0x0005E1F0
		public static Translate None()
		{
			return new Translate
			{
				m_isNone = true
			};
		}

		// Token: 0x17000507 RID: 1287
		// (get) Token: 0x06001607 RID: 5639 RVA: 0x00060013 File Offset: 0x0005E213
		// (set) Token: 0x06001608 RID: 5640 RVA: 0x0006001B File Offset: 0x0005E21B
		public Length x
		{
			get
			{
				return this.m_X;
			}
			set
			{
				this.m_X = value;
			}
		}

		// Token: 0x17000508 RID: 1288
		// (get) Token: 0x06001609 RID: 5641 RVA: 0x00060024 File Offset: 0x0005E224
		// (set) Token: 0x0600160A RID: 5642 RVA: 0x0006002C File Offset: 0x0005E22C
		public Length y
		{
			get
			{
				return this.m_Y;
			}
			set
			{
				this.m_Y = value;
			}
		}

		// Token: 0x17000509 RID: 1289
		// (get) Token: 0x0600160B RID: 5643 RVA: 0x00060035 File Offset: 0x0005E235
		// (set) Token: 0x0600160C RID: 5644 RVA: 0x0006003D File Offset: 0x0005E23D
		public float z
		{
			get
			{
				return this.m_Z;
			}
			set
			{
				this.m_Z = value;
			}
		}

		// Token: 0x0600160D RID: 5645 RVA: 0x00060046 File Offset: 0x0005E246
		internal bool IsNone()
		{
			return this.m_isNone;
		}

		// Token: 0x0600160E RID: 5646 RVA: 0x00060050 File Offset: 0x0005E250
		public static bool operator ==(Translate lhs, Translate rhs)
		{
			return lhs.m_X == rhs.m_X && lhs.m_Y == rhs.m_Y && lhs.m_Z == rhs.m_Z && lhs.m_isNone == rhs.m_isNone;
		}

		// Token: 0x0600160F RID: 5647 RVA: 0x000600A8 File Offset: 0x0005E2A8
		public static bool operator !=(Translate lhs, Translate rhs)
		{
			return !(lhs == rhs);
		}

		// Token: 0x06001610 RID: 5648 RVA: 0x000600C4 File Offset: 0x0005E2C4
		public bool Equals(Translate other)
		{
			return other == this;
		}

		// Token: 0x06001611 RID: 5649 RVA: 0x000600E4 File Offset: 0x0005E2E4
		public override bool Equals(object obj)
		{
			bool result;
			if (obj is Translate)
			{
				Translate other = (Translate)obj;
				result = this.Equals(other);
			}
			else
			{
				result = false;
			}
			return result;
		}

		// Token: 0x06001612 RID: 5650 RVA: 0x00060110 File Offset: 0x0005E310
		public override int GetHashCode()
		{
			return this.m_X.GetHashCode() * 793 ^ this.m_Y.GetHashCode() * 791 ^ this.m_Z.GetHashCode() * 571;
		}

		// Token: 0x06001613 RID: 5651 RVA: 0x00060164 File Offset: 0x0005E364
		public override string ToString()
		{
			string text = this.m_Z.ToString(CultureInfo.InvariantCulture.NumberFormat);
			return string.Concat(new string[]
			{
				this.m_X.ToString(),
				" ",
				this.m_Y.ToString(),
				" ",
				text
			});
		}

		// Token: 0x04000972 RID: 2418
		private Length m_X;

		// Token: 0x04000973 RID: 2419
		private Length m_Y;

		// Token: 0x04000974 RID: 2420
		private float m_Z;

		// Token: 0x04000975 RID: 2421
		private bool m_isNone;
	}
}

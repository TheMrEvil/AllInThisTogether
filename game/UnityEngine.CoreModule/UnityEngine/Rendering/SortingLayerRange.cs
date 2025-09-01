using System;

namespace UnityEngine.Rendering
{
	// Token: 0x02000415 RID: 1045
	public struct SortingLayerRange : IEquatable<SortingLayerRange>
	{
		// Token: 0x06002405 RID: 9221 RVA: 0x0003CEEA File Offset: 0x0003B0EA
		public SortingLayerRange(short lowerBound, short upperBound)
		{
			this.m_LowerBound = lowerBound;
			this.m_UpperBound = upperBound;
		}

		// Token: 0x170006AB RID: 1707
		// (get) Token: 0x06002406 RID: 9222 RVA: 0x0003CEFC File Offset: 0x0003B0FC
		// (set) Token: 0x06002407 RID: 9223 RVA: 0x0003CF14 File Offset: 0x0003B114
		public short lowerBound
		{
			get
			{
				return this.m_LowerBound;
			}
			set
			{
				this.m_LowerBound = value;
			}
		}

		// Token: 0x170006AC RID: 1708
		// (get) Token: 0x06002408 RID: 9224 RVA: 0x0003CF20 File Offset: 0x0003B120
		// (set) Token: 0x06002409 RID: 9225 RVA: 0x0003CF38 File Offset: 0x0003B138
		public short upperBound
		{
			get
			{
				return this.m_UpperBound;
			}
			set
			{
				this.m_UpperBound = value;
			}
		}

		// Token: 0x170006AD RID: 1709
		// (get) Token: 0x0600240A RID: 9226 RVA: 0x0003CF44 File Offset: 0x0003B144
		public static SortingLayerRange all
		{
			get
			{
				return new SortingLayerRange
				{
					m_LowerBound = short.MinValue,
					m_UpperBound = short.MaxValue
				};
			}
		}

		// Token: 0x0600240B RID: 9227 RVA: 0x0003CF74 File Offset: 0x0003B174
		public bool Equals(SortingLayerRange other)
		{
			return this.m_LowerBound == other.m_LowerBound && this.m_UpperBound == other.m_UpperBound;
		}

		// Token: 0x0600240C RID: 9228 RVA: 0x0003CFA8 File Offset: 0x0003B1A8
		public override bool Equals(object obj)
		{
			bool flag = !(obj is SortingLayerRange);
			return !flag && this.Equals((SortingLayerRange)obj);
		}

		// Token: 0x0600240D RID: 9229 RVA: 0x0003CFDC File Offset: 0x0003B1DC
		public static bool operator !=(SortingLayerRange lhs, SortingLayerRange rhs)
		{
			return !lhs.Equals(rhs);
		}

		// Token: 0x0600240E RID: 9230 RVA: 0x0003CFFC File Offset: 0x0003B1FC
		public static bool operator ==(SortingLayerRange lhs, SortingLayerRange rhs)
		{
			return lhs.Equals(rhs);
		}

		// Token: 0x0600240F RID: 9231 RVA: 0x0003D018 File Offset: 0x0003B218
		public override int GetHashCode()
		{
			return (int)this.m_UpperBound << 16 | ((int)this.m_LowerBound & 65535);
		}

		// Token: 0x04000D53 RID: 3411
		private short m_LowerBound;

		// Token: 0x04000D54 RID: 3412
		private short m_UpperBound;
	}
}

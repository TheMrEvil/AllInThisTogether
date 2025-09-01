using System;

namespace Steamworks
{
	// Token: 0x020001AA RID: 426
	[Serializable]
	public struct HServerQuery : IEquatable<HServerQuery>, IComparable<HServerQuery>
	{
		// Token: 0x06000A35 RID: 2613 RVA: 0x0000F37F File Offset: 0x0000D57F
		public HServerQuery(int value)
		{
			this.m_HServerQuery = value;
		}

		// Token: 0x06000A36 RID: 2614 RVA: 0x0000F388 File Offset: 0x0000D588
		public override string ToString()
		{
			return this.m_HServerQuery.ToString();
		}

		// Token: 0x06000A37 RID: 2615 RVA: 0x0000F395 File Offset: 0x0000D595
		public override bool Equals(object other)
		{
			return other is HServerQuery && this == (HServerQuery)other;
		}

		// Token: 0x06000A38 RID: 2616 RVA: 0x0000F3B2 File Offset: 0x0000D5B2
		public override int GetHashCode()
		{
			return this.m_HServerQuery.GetHashCode();
		}

		// Token: 0x06000A39 RID: 2617 RVA: 0x0000F3BF File Offset: 0x0000D5BF
		public static bool operator ==(HServerQuery x, HServerQuery y)
		{
			return x.m_HServerQuery == y.m_HServerQuery;
		}

		// Token: 0x06000A3A RID: 2618 RVA: 0x0000F3CF File Offset: 0x0000D5CF
		public static bool operator !=(HServerQuery x, HServerQuery y)
		{
			return !(x == y);
		}

		// Token: 0x06000A3B RID: 2619 RVA: 0x0000F3DB File Offset: 0x0000D5DB
		public static explicit operator HServerQuery(int value)
		{
			return new HServerQuery(value);
		}

		// Token: 0x06000A3C RID: 2620 RVA: 0x0000F3E3 File Offset: 0x0000D5E3
		public static explicit operator int(HServerQuery that)
		{
			return that.m_HServerQuery;
		}

		// Token: 0x06000A3D RID: 2621 RVA: 0x0000F3EB File Offset: 0x0000D5EB
		public bool Equals(HServerQuery other)
		{
			return this.m_HServerQuery == other.m_HServerQuery;
		}

		// Token: 0x06000A3E RID: 2622 RVA: 0x0000F3FB File Offset: 0x0000D5FB
		public int CompareTo(HServerQuery other)
		{
			return this.m_HServerQuery.CompareTo(other.m_HServerQuery);
		}

		// Token: 0x06000A3F RID: 2623 RVA: 0x0000F40E File Offset: 0x0000D60E
		// Note: this type is marked as 'beforefieldinit'.
		static HServerQuery()
		{
		}

		// Token: 0x04000AC2 RID: 2754
		public static readonly HServerQuery Invalid = new HServerQuery(-1);

		// Token: 0x04000AC3 RID: 2755
		public int m_HServerQuery;
	}
}

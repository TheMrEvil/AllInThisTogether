using System;

namespace Steamworks.Data
{
	// Token: 0x020001CD RID: 461
	public struct PublishedFileId : IEquatable<PublishedFileId>, IComparable<PublishedFileId>
	{
		// Token: 0x06000E99 RID: 3737 RVA: 0x00018680 File Offset: 0x00016880
		public static implicit operator PublishedFileId(ulong value)
		{
			return new PublishedFileId
			{
				Value = value
			};
		}

		// Token: 0x06000E9A RID: 3738 RVA: 0x0001869E File Offset: 0x0001689E
		public static implicit operator ulong(PublishedFileId value)
		{
			return value.Value;
		}

		// Token: 0x06000E9B RID: 3739 RVA: 0x000186A6 File Offset: 0x000168A6
		public override string ToString()
		{
			return this.Value.ToString();
		}

		// Token: 0x06000E9C RID: 3740 RVA: 0x000186B3 File Offset: 0x000168B3
		public override int GetHashCode()
		{
			return this.Value.GetHashCode();
		}

		// Token: 0x06000E9D RID: 3741 RVA: 0x000186C0 File Offset: 0x000168C0
		public override bool Equals(object p)
		{
			return this.Equals((PublishedFileId)p);
		}

		// Token: 0x06000E9E RID: 3742 RVA: 0x000186CE File Offset: 0x000168CE
		public bool Equals(PublishedFileId p)
		{
			return p.Value == this.Value;
		}

		// Token: 0x06000E9F RID: 3743 RVA: 0x000186DE File Offset: 0x000168DE
		public static bool operator ==(PublishedFileId a, PublishedFileId b)
		{
			return a.Equals(b);
		}

		// Token: 0x06000EA0 RID: 3744 RVA: 0x000186E8 File Offset: 0x000168E8
		public static bool operator !=(PublishedFileId a, PublishedFileId b)
		{
			return !a.Equals(b);
		}

		// Token: 0x06000EA1 RID: 3745 RVA: 0x000186F5 File Offset: 0x000168F5
		public int CompareTo(PublishedFileId other)
		{
			return this.Value.CompareTo(other.Value);
		}

		// Token: 0x04000BBC RID: 3004
		public ulong Value;
	}
}

using System;

namespace Steamworks.Data
{
	// Token: 0x020001C9 RID: 457
	internal struct HServerListRequest : IEquatable<HServerListRequest>, IComparable<HServerListRequest>
	{
		// Token: 0x06000E75 RID: 3701 RVA: 0x00018444 File Offset: 0x00016644
		public static implicit operator HServerListRequest(IntPtr value)
		{
			return new HServerListRequest
			{
				Value = value
			};
		}

		// Token: 0x06000E76 RID: 3702 RVA: 0x00018462 File Offset: 0x00016662
		public static implicit operator IntPtr(HServerListRequest value)
		{
			return value.Value;
		}

		// Token: 0x06000E77 RID: 3703 RVA: 0x0001846A File Offset: 0x0001666A
		public override string ToString()
		{
			return this.Value.ToString();
		}

		// Token: 0x06000E78 RID: 3704 RVA: 0x00018477 File Offset: 0x00016677
		public override int GetHashCode()
		{
			return this.Value.GetHashCode();
		}

		// Token: 0x06000E79 RID: 3705 RVA: 0x00018484 File Offset: 0x00016684
		public override bool Equals(object p)
		{
			return this.Equals((HServerListRequest)p);
		}

		// Token: 0x06000E7A RID: 3706 RVA: 0x00018492 File Offset: 0x00016692
		public bool Equals(HServerListRequest p)
		{
			return p.Value == this.Value;
		}

		// Token: 0x06000E7B RID: 3707 RVA: 0x000184A5 File Offset: 0x000166A5
		public static bool operator ==(HServerListRequest a, HServerListRequest b)
		{
			return a.Equals(b);
		}

		// Token: 0x06000E7C RID: 3708 RVA: 0x000184AF File Offset: 0x000166AF
		public static bool operator !=(HServerListRequest a, HServerListRequest b)
		{
			return !a.Equals(b);
		}

		// Token: 0x06000E7D RID: 3709 RVA: 0x000184BC File Offset: 0x000166BC
		public int CompareTo(HServerListRequest other)
		{
			return this.Value.ToInt64().CompareTo(other.Value.ToInt64());
		}

		// Token: 0x04000BB8 RID: 3000
		public IntPtr Value;
	}
}

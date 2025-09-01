using System;

namespace Steamworks.Data
{
	// Token: 0x020001CC RID: 460
	internal struct PublishedFileUpdateHandle_t : IEquatable<PublishedFileUpdateHandle_t>, IComparable<PublishedFileUpdateHandle_t>
	{
		// Token: 0x06000E90 RID: 3728 RVA: 0x000185F8 File Offset: 0x000167F8
		public static implicit operator PublishedFileUpdateHandle_t(ulong value)
		{
			return new PublishedFileUpdateHandle_t
			{
				Value = value
			};
		}

		// Token: 0x06000E91 RID: 3729 RVA: 0x00018616 File Offset: 0x00016816
		public static implicit operator ulong(PublishedFileUpdateHandle_t value)
		{
			return value.Value;
		}

		// Token: 0x06000E92 RID: 3730 RVA: 0x0001861E File Offset: 0x0001681E
		public override string ToString()
		{
			return this.Value.ToString();
		}

		// Token: 0x06000E93 RID: 3731 RVA: 0x0001862B File Offset: 0x0001682B
		public override int GetHashCode()
		{
			return this.Value.GetHashCode();
		}

		// Token: 0x06000E94 RID: 3732 RVA: 0x00018638 File Offset: 0x00016838
		public override bool Equals(object p)
		{
			return this.Equals((PublishedFileUpdateHandle_t)p);
		}

		// Token: 0x06000E95 RID: 3733 RVA: 0x00018646 File Offset: 0x00016846
		public bool Equals(PublishedFileUpdateHandle_t p)
		{
			return p.Value == this.Value;
		}

		// Token: 0x06000E96 RID: 3734 RVA: 0x00018656 File Offset: 0x00016856
		public static bool operator ==(PublishedFileUpdateHandle_t a, PublishedFileUpdateHandle_t b)
		{
			return a.Equals(b);
		}

		// Token: 0x06000E97 RID: 3735 RVA: 0x00018660 File Offset: 0x00016860
		public static bool operator !=(PublishedFileUpdateHandle_t a, PublishedFileUpdateHandle_t b)
		{
			return !a.Equals(b);
		}

		// Token: 0x06000E98 RID: 3736 RVA: 0x0001866D File Offset: 0x0001686D
		public int CompareTo(PublishedFileUpdateHandle_t other)
		{
			return this.Value.CompareTo(other.Value);
		}

		// Token: 0x04000BBB RID: 3003
		public ulong Value;
	}
}

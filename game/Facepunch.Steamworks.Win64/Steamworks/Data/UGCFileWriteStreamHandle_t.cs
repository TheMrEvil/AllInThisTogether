using System;

namespace Steamworks.Data
{
	// Token: 0x020001CE RID: 462
	internal struct UGCFileWriteStreamHandle_t : IEquatable<UGCFileWriteStreamHandle_t>, IComparable<UGCFileWriteStreamHandle_t>
	{
		// Token: 0x06000EA2 RID: 3746 RVA: 0x00018708 File Offset: 0x00016908
		public static implicit operator UGCFileWriteStreamHandle_t(ulong value)
		{
			return new UGCFileWriteStreamHandle_t
			{
				Value = value
			};
		}

		// Token: 0x06000EA3 RID: 3747 RVA: 0x00018726 File Offset: 0x00016926
		public static implicit operator ulong(UGCFileWriteStreamHandle_t value)
		{
			return value.Value;
		}

		// Token: 0x06000EA4 RID: 3748 RVA: 0x0001872E File Offset: 0x0001692E
		public override string ToString()
		{
			return this.Value.ToString();
		}

		// Token: 0x06000EA5 RID: 3749 RVA: 0x0001873B File Offset: 0x0001693B
		public override int GetHashCode()
		{
			return this.Value.GetHashCode();
		}

		// Token: 0x06000EA6 RID: 3750 RVA: 0x00018748 File Offset: 0x00016948
		public override bool Equals(object p)
		{
			return this.Equals((UGCFileWriteStreamHandle_t)p);
		}

		// Token: 0x06000EA7 RID: 3751 RVA: 0x00018756 File Offset: 0x00016956
		public bool Equals(UGCFileWriteStreamHandle_t p)
		{
			return p.Value == this.Value;
		}

		// Token: 0x06000EA8 RID: 3752 RVA: 0x00018766 File Offset: 0x00016966
		public static bool operator ==(UGCFileWriteStreamHandle_t a, UGCFileWriteStreamHandle_t b)
		{
			return a.Equals(b);
		}

		// Token: 0x06000EA9 RID: 3753 RVA: 0x00018770 File Offset: 0x00016970
		public static bool operator !=(UGCFileWriteStreamHandle_t a, UGCFileWriteStreamHandle_t b)
		{
			return !a.Equals(b);
		}

		// Token: 0x06000EAA RID: 3754 RVA: 0x0001877D File Offset: 0x0001697D
		public int CompareTo(UGCFileWriteStreamHandle_t other)
		{
			return this.Value.CompareTo(other.Value);
		}

		// Token: 0x04000BBD RID: 3005
		public ulong Value;
	}
}

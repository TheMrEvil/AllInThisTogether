using System;

namespace Steamworks.Data
{
	// Token: 0x020001B9 RID: 441
	internal struct AssetClassId_t : IEquatable<AssetClassId_t>, IComparable<AssetClassId_t>
	{
		// Token: 0x06000DE5 RID: 3557 RVA: 0x00017BA8 File Offset: 0x00015DA8
		public static implicit operator AssetClassId_t(ulong value)
		{
			return new AssetClassId_t
			{
				Value = value
			};
		}

		// Token: 0x06000DE6 RID: 3558 RVA: 0x00017BC6 File Offset: 0x00015DC6
		public static implicit operator ulong(AssetClassId_t value)
		{
			return value.Value;
		}

		// Token: 0x06000DE7 RID: 3559 RVA: 0x00017BCE File Offset: 0x00015DCE
		public override string ToString()
		{
			return this.Value.ToString();
		}

		// Token: 0x06000DE8 RID: 3560 RVA: 0x00017BDB File Offset: 0x00015DDB
		public override int GetHashCode()
		{
			return this.Value.GetHashCode();
		}

		// Token: 0x06000DE9 RID: 3561 RVA: 0x00017BE8 File Offset: 0x00015DE8
		public override bool Equals(object p)
		{
			return this.Equals((AssetClassId_t)p);
		}

		// Token: 0x06000DEA RID: 3562 RVA: 0x00017BF6 File Offset: 0x00015DF6
		public bool Equals(AssetClassId_t p)
		{
			return p.Value == this.Value;
		}

		// Token: 0x06000DEB RID: 3563 RVA: 0x00017C06 File Offset: 0x00015E06
		public static bool operator ==(AssetClassId_t a, AssetClassId_t b)
		{
			return a.Equals(b);
		}

		// Token: 0x06000DEC RID: 3564 RVA: 0x00017C10 File Offset: 0x00015E10
		public static bool operator !=(AssetClassId_t a, AssetClassId_t b)
		{
			return !a.Equals(b);
		}

		// Token: 0x06000DED RID: 3565 RVA: 0x00017C1D File Offset: 0x00015E1D
		public int CompareTo(AssetClassId_t other)
		{
			return this.Value.CompareTo(other.Value);
		}

		// Token: 0x04000BA8 RID: 2984
		public ulong Value;
	}
}

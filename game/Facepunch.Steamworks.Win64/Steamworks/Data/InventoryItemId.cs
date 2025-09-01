using System;

namespace Steamworks.Data
{
	// Token: 0x020001E1 RID: 481
	public struct InventoryItemId : IEquatable<InventoryItemId>, IComparable<InventoryItemId>
	{
		// Token: 0x06000F4D RID: 3917 RVA: 0x00019120 File Offset: 0x00017320
		public static implicit operator InventoryItemId(ulong value)
		{
			return new InventoryItemId
			{
				Value = value
			};
		}

		// Token: 0x06000F4E RID: 3918 RVA: 0x0001913E File Offset: 0x0001733E
		public static implicit operator ulong(InventoryItemId value)
		{
			return value.Value;
		}

		// Token: 0x06000F4F RID: 3919 RVA: 0x00019146 File Offset: 0x00017346
		public override string ToString()
		{
			return this.Value.ToString();
		}

		// Token: 0x06000F50 RID: 3920 RVA: 0x00019153 File Offset: 0x00017353
		public override int GetHashCode()
		{
			return this.Value.GetHashCode();
		}

		// Token: 0x06000F51 RID: 3921 RVA: 0x00019160 File Offset: 0x00017360
		public override bool Equals(object p)
		{
			return this.Equals((InventoryItemId)p);
		}

		// Token: 0x06000F52 RID: 3922 RVA: 0x0001916E File Offset: 0x0001736E
		public bool Equals(InventoryItemId p)
		{
			return p.Value == this.Value;
		}

		// Token: 0x06000F53 RID: 3923 RVA: 0x0001917E File Offset: 0x0001737E
		public static bool operator ==(InventoryItemId a, InventoryItemId b)
		{
			return a.Equals(b);
		}

		// Token: 0x06000F54 RID: 3924 RVA: 0x00019188 File Offset: 0x00017388
		public static bool operator !=(InventoryItemId a, InventoryItemId b)
		{
			return !a.Equals(b);
		}

		// Token: 0x06000F55 RID: 3925 RVA: 0x00019195 File Offset: 0x00017395
		public int CompareTo(InventoryItemId other)
		{
			return this.Value.CompareTo(other.Value);
		}

		// Token: 0x04000BD0 RID: 3024
		public ulong Value;
	}
}

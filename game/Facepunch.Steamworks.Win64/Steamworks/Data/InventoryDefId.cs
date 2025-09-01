using System;

namespace Steamworks.Data
{
	// Token: 0x020001E2 RID: 482
	public struct InventoryDefId : IEquatable<InventoryDefId>, IComparable<InventoryDefId>
	{
		// Token: 0x06000F56 RID: 3926 RVA: 0x000191A8 File Offset: 0x000173A8
		public static implicit operator InventoryDefId(int value)
		{
			return new InventoryDefId
			{
				Value = value
			};
		}

		// Token: 0x06000F57 RID: 3927 RVA: 0x000191C6 File Offset: 0x000173C6
		public static implicit operator int(InventoryDefId value)
		{
			return value.Value;
		}

		// Token: 0x06000F58 RID: 3928 RVA: 0x000191CE File Offset: 0x000173CE
		public override string ToString()
		{
			return this.Value.ToString();
		}

		// Token: 0x06000F59 RID: 3929 RVA: 0x000191DB File Offset: 0x000173DB
		public override int GetHashCode()
		{
			return this.Value.GetHashCode();
		}

		// Token: 0x06000F5A RID: 3930 RVA: 0x000191E8 File Offset: 0x000173E8
		public override bool Equals(object p)
		{
			return this.Equals((InventoryDefId)p);
		}

		// Token: 0x06000F5B RID: 3931 RVA: 0x000191F6 File Offset: 0x000173F6
		public bool Equals(InventoryDefId p)
		{
			return p.Value == this.Value;
		}

		// Token: 0x06000F5C RID: 3932 RVA: 0x00019206 File Offset: 0x00017406
		public static bool operator ==(InventoryDefId a, InventoryDefId b)
		{
			return a.Equals(b);
		}

		// Token: 0x06000F5D RID: 3933 RVA: 0x00019210 File Offset: 0x00017410
		public static bool operator !=(InventoryDefId a, InventoryDefId b)
		{
			return !a.Equals(b);
		}

		// Token: 0x06000F5E RID: 3934 RVA: 0x0001921D File Offset: 0x0001741D
		public int CompareTo(InventoryDefId other)
		{
			return this.Value.CompareTo(other.Value);
		}

		// Token: 0x04000BD1 RID: 3025
		public int Value;
	}
}

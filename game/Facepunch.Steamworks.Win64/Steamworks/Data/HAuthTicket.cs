using System;

namespace Steamworks.Data
{
	// Token: 0x020001C4 RID: 452
	internal struct HAuthTicket : IEquatable<HAuthTicket>, IComparable<HAuthTicket>
	{
		// Token: 0x06000E48 RID: 3656 RVA: 0x00018180 File Offset: 0x00016380
		public static implicit operator HAuthTicket(uint value)
		{
			return new HAuthTicket
			{
				Value = value
			};
		}

		// Token: 0x06000E49 RID: 3657 RVA: 0x0001819E File Offset: 0x0001639E
		public static implicit operator uint(HAuthTicket value)
		{
			return value.Value;
		}

		// Token: 0x06000E4A RID: 3658 RVA: 0x000181A6 File Offset: 0x000163A6
		public override string ToString()
		{
			return this.Value.ToString();
		}

		// Token: 0x06000E4B RID: 3659 RVA: 0x000181B3 File Offset: 0x000163B3
		public override int GetHashCode()
		{
			return this.Value.GetHashCode();
		}

		// Token: 0x06000E4C RID: 3660 RVA: 0x000181C0 File Offset: 0x000163C0
		public override bool Equals(object p)
		{
			return this.Equals((HAuthTicket)p);
		}

		// Token: 0x06000E4D RID: 3661 RVA: 0x000181CE File Offset: 0x000163CE
		public bool Equals(HAuthTicket p)
		{
			return p.Value == this.Value;
		}

		// Token: 0x06000E4E RID: 3662 RVA: 0x000181DE File Offset: 0x000163DE
		public static bool operator ==(HAuthTicket a, HAuthTicket b)
		{
			return a.Equals(b);
		}

		// Token: 0x06000E4F RID: 3663 RVA: 0x000181E8 File Offset: 0x000163E8
		public static bool operator !=(HAuthTicket a, HAuthTicket b)
		{
			return !a.Equals(b);
		}

		// Token: 0x06000E50 RID: 3664 RVA: 0x000181F5 File Offset: 0x000163F5
		public int CompareTo(HAuthTicket other)
		{
			return this.Value.CompareTo(other.Value);
		}

		// Token: 0x04000BB3 RID: 2995
		public uint Value;
	}
}

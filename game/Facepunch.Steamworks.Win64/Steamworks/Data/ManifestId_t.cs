using System;

namespace Steamworks.Data
{
	// Token: 0x020001C1 RID: 449
	internal struct ManifestId_t : IEquatable<ManifestId_t>, IComparable<ManifestId_t>
	{
		// Token: 0x06000E2D RID: 3629 RVA: 0x00017FE8 File Offset: 0x000161E8
		public static implicit operator ManifestId_t(ulong value)
		{
			return new ManifestId_t
			{
				Value = value
			};
		}

		// Token: 0x06000E2E RID: 3630 RVA: 0x00018006 File Offset: 0x00016206
		public static implicit operator ulong(ManifestId_t value)
		{
			return value.Value;
		}

		// Token: 0x06000E2F RID: 3631 RVA: 0x0001800E File Offset: 0x0001620E
		public override string ToString()
		{
			return this.Value.ToString();
		}

		// Token: 0x06000E30 RID: 3632 RVA: 0x0001801B File Offset: 0x0001621B
		public override int GetHashCode()
		{
			return this.Value.GetHashCode();
		}

		// Token: 0x06000E31 RID: 3633 RVA: 0x00018028 File Offset: 0x00016228
		public override bool Equals(object p)
		{
			return this.Equals((ManifestId_t)p);
		}

		// Token: 0x06000E32 RID: 3634 RVA: 0x00018036 File Offset: 0x00016236
		public bool Equals(ManifestId_t p)
		{
			return p.Value == this.Value;
		}

		// Token: 0x06000E33 RID: 3635 RVA: 0x00018046 File Offset: 0x00016246
		public static bool operator ==(ManifestId_t a, ManifestId_t b)
		{
			return a.Equals(b);
		}

		// Token: 0x06000E34 RID: 3636 RVA: 0x00018050 File Offset: 0x00016250
		public static bool operator !=(ManifestId_t a, ManifestId_t b)
		{
			return !a.Equals(b);
		}

		// Token: 0x06000E35 RID: 3637 RVA: 0x0001805D File Offset: 0x0001625D
		public int CompareTo(ManifestId_t other)
		{
			return this.Value.CompareTo(other.Value);
		}

		// Token: 0x04000BB0 RID: 2992
		public ulong Value;
	}
}

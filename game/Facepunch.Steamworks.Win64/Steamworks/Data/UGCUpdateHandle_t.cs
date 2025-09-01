using System;

namespace Steamworks.Data
{
	// Token: 0x020001DF RID: 479
	internal struct UGCUpdateHandle_t : IEquatable<UGCUpdateHandle_t>, IComparable<UGCUpdateHandle_t>
	{
		// Token: 0x06000F3B RID: 3899 RVA: 0x00019010 File Offset: 0x00017210
		public static implicit operator UGCUpdateHandle_t(ulong value)
		{
			return new UGCUpdateHandle_t
			{
				Value = value
			};
		}

		// Token: 0x06000F3C RID: 3900 RVA: 0x0001902E File Offset: 0x0001722E
		public static implicit operator ulong(UGCUpdateHandle_t value)
		{
			return value.Value;
		}

		// Token: 0x06000F3D RID: 3901 RVA: 0x00019036 File Offset: 0x00017236
		public override string ToString()
		{
			return this.Value.ToString();
		}

		// Token: 0x06000F3E RID: 3902 RVA: 0x00019043 File Offset: 0x00017243
		public override int GetHashCode()
		{
			return this.Value.GetHashCode();
		}

		// Token: 0x06000F3F RID: 3903 RVA: 0x00019050 File Offset: 0x00017250
		public override bool Equals(object p)
		{
			return this.Equals((UGCUpdateHandle_t)p);
		}

		// Token: 0x06000F40 RID: 3904 RVA: 0x0001905E File Offset: 0x0001725E
		public bool Equals(UGCUpdateHandle_t p)
		{
			return p.Value == this.Value;
		}

		// Token: 0x06000F41 RID: 3905 RVA: 0x0001906E File Offset: 0x0001726E
		public static bool operator ==(UGCUpdateHandle_t a, UGCUpdateHandle_t b)
		{
			return a.Equals(b);
		}

		// Token: 0x06000F42 RID: 3906 RVA: 0x00019078 File Offset: 0x00017278
		public static bool operator !=(UGCUpdateHandle_t a, UGCUpdateHandle_t b)
		{
			return !a.Equals(b);
		}

		// Token: 0x06000F43 RID: 3907 RVA: 0x00019085 File Offset: 0x00017285
		public int CompareTo(UGCUpdateHandle_t other)
		{
			return this.Value.CompareTo(other.Value);
		}

		// Token: 0x04000BCE RID: 3022
		public ulong Value;
	}
}

using System;

namespace Steamworks.Data
{
	// Token: 0x020001DE RID: 478
	internal struct UGCQueryHandle_t : IEquatable<UGCQueryHandle_t>, IComparable<UGCQueryHandle_t>
	{
		// Token: 0x06000F32 RID: 3890 RVA: 0x00018F88 File Offset: 0x00017188
		public static implicit operator UGCQueryHandle_t(ulong value)
		{
			return new UGCQueryHandle_t
			{
				Value = value
			};
		}

		// Token: 0x06000F33 RID: 3891 RVA: 0x00018FA6 File Offset: 0x000171A6
		public static implicit operator ulong(UGCQueryHandle_t value)
		{
			return value.Value;
		}

		// Token: 0x06000F34 RID: 3892 RVA: 0x00018FAE File Offset: 0x000171AE
		public override string ToString()
		{
			return this.Value.ToString();
		}

		// Token: 0x06000F35 RID: 3893 RVA: 0x00018FBB File Offset: 0x000171BB
		public override int GetHashCode()
		{
			return this.Value.GetHashCode();
		}

		// Token: 0x06000F36 RID: 3894 RVA: 0x00018FC8 File Offset: 0x000171C8
		public override bool Equals(object p)
		{
			return this.Equals((UGCQueryHandle_t)p);
		}

		// Token: 0x06000F37 RID: 3895 RVA: 0x00018FD6 File Offset: 0x000171D6
		public bool Equals(UGCQueryHandle_t p)
		{
			return p.Value == this.Value;
		}

		// Token: 0x06000F38 RID: 3896 RVA: 0x00018FE6 File Offset: 0x000171E6
		public static bool operator ==(UGCQueryHandle_t a, UGCQueryHandle_t b)
		{
			return a.Equals(b);
		}

		// Token: 0x06000F39 RID: 3897 RVA: 0x00018FF0 File Offset: 0x000171F0
		public static bool operator !=(UGCQueryHandle_t a, UGCQueryHandle_t b)
		{
			return !a.Equals(b);
		}

		// Token: 0x06000F3A RID: 3898 RVA: 0x00018FFD File Offset: 0x000171FD
		public int CompareTo(UGCQueryHandle_t other)
		{
			return this.Value.CompareTo(other.Value);
		}

		// Token: 0x04000BCD RID: 3021
		public ulong Value;
	}
}

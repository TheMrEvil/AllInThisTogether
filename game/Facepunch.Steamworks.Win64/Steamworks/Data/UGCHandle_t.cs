using System;

namespace Steamworks.Data
{
	// Token: 0x020001CB RID: 459
	internal struct UGCHandle_t : IEquatable<UGCHandle_t>, IComparable<UGCHandle_t>
	{
		// Token: 0x06000E87 RID: 3719 RVA: 0x00018570 File Offset: 0x00016770
		public static implicit operator UGCHandle_t(ulong value)
		{
			return new UGCHandle_t
			{
				Value = value
			};
		}

		// Token: 0x06000E88 RID: 3720 RVA: 0x0001858E File Offset: 0x0001678E
		public static implicit operator ulong(UGCHandle_t value)
		{
			return value.Value;
		}

		// Token: 0x06000E89 RID: 3721 RVA: 0x00018596 File Offset: 0x00016796
		public override string ToString()
		{
			return this.Value.ToString();
		}

		// Token: 0x06000E8A RID: 3722 RVA: 0x000185A3 File Offset: 0x000167A3
		public override int GetHashCode()
		{
			return this.Value.GetHashCode();
		}

		// Token: 0x06000E8B RID: 3723 RVA: 0x000185B0 File Offset: 0x000167B0
		public override bool Equals(object p)
		{
			return this.Equals((UGCHandle_t)p);
		}

		// Token: 0x06000E8C RID: 3724 RVA: 0x000185BE File Offset: 0x000167BE
		public bool Equals(UGCHandle_t p)
		{
			return p.Value == this.Value;
		}

		// Token: 0x06000E8D RID: 3725 RVA: 0x000185CE File Offset: 0x000167CE
		public static bool operator ==(UGCHandle_t a, UGCHandle_t b)
		{
			return a.Equals(b);
		}

		// Token: 0x06000E8E RID: 3726 RVA: 0x000185D8 File Offset: 0x000167D8
		public static bool operator !=(UGCHandle_t a, UGCHandle_t b)
		{
			return !a.Equals(b);
		}

		// Token: 0x06000E8F RID: 3727 RVA: 0x000185E5 File Offset: 0x000167E5
		public int CompareTo(UGCHandle_t other)
		{
			return this.Value.CompareTo(other.Value);
		}

		// Token: 0x04000BBA RID: 3002
		public ulong Value;
	}
}

using System;

namespace Steamworks.Data
{
	// Token: 0x020001B6 RID: 438
	internal struct TxnID_t : IEquatable<TxnID_t>, IComparable<TxnID_t>
	{
		// Token: 0x06000DCA RID: 3530 RVA: 0x00017A10 File Offset: 0x00015C10
		public static implicit operator TxnID_t(ulong value)
		{
			return new TxnID_t
			{
				Value = value
			};
		}

		// Token: 0x06000DCB RID: 3531 RVA: 0x00017A2E File Offset: 0x00015C2E
		public static implicit operator ulong(TxnID_t value)
		{
			return value.Value;
		}

		// Token: 0x06000DCC RID: 3532 RVA: 0x00017A36 File Offset: 0x00015C36
		public override string ToString()
		{
			return this.Value.ToString();
		}

		// Token: 0x06000DCD RID: 3533 RVA: 0x00017A43 File Offset: 0x00015C43
		public override int GetHashCode()
		{
			return this.Value.GetHashCode();
		}

		// Token: 0x06000DCE RID: 3534 RVA: 0x00017A50 File Offset: 0x00015C50
		public override bool Equals(object p)
		{
			return this.Equals((TxnID_t)p);
		}

		// Token: 0x06000DCF RID: 3535 RVA: 0x00017A5E File Offset: 0x00015C5E
		public bool Equals(TxnID_t p)
		{
			return p.Value == this.Value;
		}

		// Token: 0x06000DD0 RID: 3536 RVA: 0x00017A6E File Offset: 0x00015C6E
		public static bool operator ==(TxnID_t a, TxnID_t b)
		{
			return a.Equals(b);
		}

		// Token: 0x06000DD1 RID: 3537 RVA: 0x00017A78 File Offset: 0x00015C78
		public static bool operator !=(TxnID_t a, TxnID_t b)
		{
			return !a.Equals(b);
		}

		// Token: 0x06000DD2 RID: 3538 RVA: 0x00017A85 File Offset: 0x00015C85
		public int CompareTo(TxnID_t other)
		{
			return this.Value.CompareTo(other.Value);
		}

		// Token: 0x04000BA5 RID: 2981
		public ulong Value;
	}
}

using System;

namespace Steamworks.Data
{
	// Token: 0x020001D6 RID: 470
	internal struct InputHandle_t : IEquatable<InputHandle_t>, IComparable<InputHandle_t>
	{
		// Token: 0x06000EEA RID: 3818 RVA: 0x00018B48 File Offset: 0x00016D48
		public static implicit operator InputHandle_t(ulong value)
		{
			return new InputHandle_t
			{
				Value = value
			};
		}

		// Token: 0x06000EEB RID: 3819 RVA: 0x00018B66 File Offset: 0x00016D66
		public static implicit operator ulong(InputHandle_t value)
		{
			return value.Value;
		}

		// Token: 0x06000EEC RID: 3820 RVA: 0x00018B6E File Offset: 0x00016D6E
		public override string ToString()
		{
			return this.Value.ToString();
		}

		// Token: 0x06000EED RID: 3821 RVA: 0x00018B7B File Offset: 0x00016D7B
		public override int GetHashCode()
		{
			return this.Value.GetHashCode();
		}

		// Token: 0x06000EEE RID: 3822 RVA: 0x00018B88 File Offset: 0x00016D88
		public override bool Equals(object p)
		{
			return this.Equals((InputHandle_t)p);
		}

		// Token: 0x06000EEF RID: 3823 RVA: 0x00018B96 File Offset: 0x00016D96
		public bool Equals(InputHandle_t p)
		{
			return p.Value == this.Value;
		}

		// Token: 0x06000EF0 RID: 3824 RVA: 0x00018BA6 File Offset: 0x00016DA6
		public static bool operator ==(InputHandle_t a, InputHandle_t b)
		{
			return a.Equals(b);
		}

		// Token: 0x06000EF1 RID: 3825 RVA: 0x00018BB0 File Offset: 0x00016DB0
		public static bool operator !=(InputHandle_t a, InputHandle_t b)
		{
			return !a.Equals(b);
		}

		// Token: 0x06000EF2 RID: 3826 RVA: 0x00018BBD File Offset: 0x00016DBD
		public int CompareTo(InputHandle_t other)
		{
			return this.Value.CompareTo(other.Value);
		}

		// Token: 0x04000BC5 RID: 3013
		public ulong Value;
	}
}

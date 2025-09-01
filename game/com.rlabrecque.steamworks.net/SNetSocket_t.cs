using System;

namespace Steamworks
{
	// Token: 0x020001AC RID: 428
	[Serializable]
	public struct SNetSocket_t : IEquatable<SNetSocket_t>, IComparable<SNetSocket_t>
	{
		// Token: 0x06000A4A RID: 2634 RVA: 0x0000F4AA File Offset: 0x0000D6AA
		public SNetSocket_t(uint value)
		{
			this.m_SNetSocket = value;
		}

		// Token: 0x06000A4B RID: 2635 RVA: 0x0000F4B3 File Offset: 0x0000D6B3
		public override string ToString()
		{
			return this.m_SNetSocket.ToString();
		}

		// Token: 0x06000A4C RID: 2636 RVA: 0x0000F4C0 File Offset: 0x0000D6C0
		public override bool Equals(object other)
		{
			return other is SNetSocket_t && this == (SNetSocket_t)other;
		}

		// Token: 0x06000A4D RID: 2637 RVA: 0x0000F4DD File Offset: 0x0000D6DD
		public override int GetHashCode()
		{
			return this.m_SNetSocket.GetHashCode();
		}

		// Token: 0x06000A4E RID: 2638 RVA: 0x0000F4EA File Offset: 0x0000D6EA
		public static bool operator ==(SNetSocket_t x, SNetSocket_t y)
		{
			return x.m_SNetSocket == y.m_SNetSocket;
		}

		// Token: 0x06000A4F RID: 2639 RVA: 0x0000F4FA File Offset: 0x0000D6FA
		public static bool operator !=(SNetSocket_t x, SNetSocket_t y)
		{
			return !(x == y);
		}

		// Token: 0x06000A50 RID: 2640 RVA: 0x0000F506 File Offset: 0x0000D706
		public static explicit operator SNetSocket_t(uint value)
		{
			return new SNetSocket_t(value);
		}

		// Token: 0x06000A51 RID: 2641 RVA: 0x0000F50E File Offset: 0x0000D70E
		public static explicit operator uint(SNetSocket_t that)
		{
			return that.m_SNetSocket;
		}

		// Token: 0x06000A52 RID: 2642 RVA: 0x0000F516 File Offset: 0x0000D716
		public bool Equals(SNetSocket_t other)
		{
			return this.m_SNetSocket == other.m_SNetSocket;
		}

		// Token: 0x06000A53 RID: 2643 RVA: 0x0000F526 File Offset: 0x0000D726
		public int CompareTo(SNetSocket_t other)
		{
			return this.m_SNetSocket.CompareTo(other.m_SNetSocket);
		}

		// Token: 0x04000AC5 RID: 2757
		public uint m_SNetSocket;
	}
}

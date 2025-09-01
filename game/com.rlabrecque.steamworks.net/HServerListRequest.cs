using System;

namespace Steamworks
{
	// Token: 0x020001A9 RID: 425
	[Serializable]
	public struct HServerListRequest : IEquatable<HServerListRequest>
	{
		// Token: 0x06000A2B RID: 2603 RVA: 0x0000F2EC File Offset: 0x0000D4EC
		public HServerListRequest(IntPtr value)
		{
			this.m_HServerListRequest = value;
		}

		// Token: 0x06000A2C RID: 2604 RVA: 0x0000F2F5 File Offset: 0x0000D4F5
		public override string ToString()
		{
			return this.m_HServerListRequest.ToString();
		}

		// Token: 0x06000A2D RID: 2605 RVA: 0x0000F302 File Offset: 0x0000D502
		public override bool Equals(object other)
		{
			return other is HServerListRequest && this == (HServerListRequest)other;
		}

		// Token: 0x06000A2E RID: 2606 RVA: 0x0000F31F File Offset: 0x0000D51F
		public override int GetHashCode()
		{
			return this.m_HServerListRequest.GetHashCode();
		}

		// Token: 0x06000A2F RID: 2607 RVA: 0x0000F32C File Offset: 0x0000D52C
		public static bool operator ==(HServerListRequest x, HServerListRequest y)
		{
			return x.m_HServerListRequest == y.m_HServerListRequest;
		}

		// Token: 0x06000A30 RID: 2608 RVA: 0x0000F33F File Offset: 0x0000D53F
		public static bool operator !=(HServerListRequest x, HServerListRequest y)
		{
			return !(x == y);
		}

		// Token: 0x06000A31 RID: 2609 RVA: 0x0000F34B File Offset: 0x0000D54B
		public static explicit operator HServerListRequest(IntPtr value)
		{
			return new HServerListRequest(value);
		}

		// Token: 0x06000A32 RID: 2610 RVA: 0x0000F353 File Offset: 0x0000D553
		public static explicit operator IntPtr(HServerListRequest that)
		{
			return that.m_HServerListRequest;
		}

		// Token: 0x06000A33 RID: 2611 RVA: 0x0000F35B File Offset: 0x0000D55B
		public bool Equals(HServerListRequest other)
		{
			return this.m_HServerListRequest == other.m_HServerListRequest;
		}

		// Token: 0x06000A34 RID: 2612 RVA: 0x0000F36E File Offset: 0x0000D56E
		// Note: this type is marked as 'beforefieldinit'.
		static HServerListRequest()
		{
		}

		// Token: 0x04000AC0 RID: 2752
		public static readonly HServerListRequest Invalid = new HServerListRequest(IntPtr.Zero);

		// Token: 0x04000AC1 RID: 2753
		public IntPtr m_HServerListRequest;
	}
}

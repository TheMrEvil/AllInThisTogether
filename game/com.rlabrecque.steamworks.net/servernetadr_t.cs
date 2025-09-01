using System;

namespace Steamworks
{
	// Token: 0x02000193 RID: 403
	[Serializable]
	public struct servernetadr_t
	{
		// Token: 0x06000941 RID: 2369 RVA: 0x0000E2C7 File Offset: 0x0000C4C7
		public void Init(uint ip, ushort usQueryPort, ushort usConnectionPort)
		{
			this.m_unIP = ip;
			this.m_usQueryPort = usQueryPort;
			this.m_usConnectionPort = usConnectionPort;
		}

		// Token: 0x06000942 RID: 2370 RVA: 0x0000E2DE File Offset: 0x0000C4DE
		public ushort GetQueryPort()
		{
			return this.m_usQueryPort;
		}

		// Token: 0x06000943 RID: 2371 RVA: 0x0000E2E6 File Offset: 0x0000C4E6
		public void SetQueryPort(ushort usPort)
		{
			this.m_usQueryPort = usPort;
		}

		// Token: 0x06000944 RID: 2372 RVA: 0x0000E2EF File Offset: 0x0000C4EF
		public ushort GetConnectionPort()
		{
			return this.m_usConnectionPort;
		}

		// Token: 0x06000945 RID: 2373 RVA: 0x0000E2F7 File Offset: 0x0000C4F7
		public void SetConnectionPort(ushort usPort)
		{
			this.m_usConnectionPort = usPort;
		}

		// Token: 0x06000946 RID: 2374 RVA: 0x0000E300 File Offset: 0x0000C500
		public uint GetIP()
		{
			return this.m_unIP;
		}

		// Token: 0x06000947 RID: 2375 RVA: 0x0000E308 File Offset: 0x0000C508
		public void SetIP(uint unIP)
		{
			this.m_unIP = unIP;
		}

		// Token: 0x06000948 RID: 2376 RVA: 0x0000E311 File Offset: 0x0000C511
		public string GetConnectionAddressString()
		{
			return servernetadr_t.ToString(this.m_unIP, this.m_usConnectionPort);
		}

		// Token: 0x06000949 RID: 2377 RVA: 0x0000E324 File Offset: 0x0000C524
		public string GetQueryAddressString()
		{
			return servernetadr_t.ToString(this.m_unIP, this.m_usQueryPort);
		}

		// Token: 0x0600094A RID: 2378 RVA: 0x0000E338 File Offset: 0x0000C538
		public static string ToString(uint unIP, ushort usPort)
		{
			return string.Format("{0}.{1}.{2}.{3}:{4}", new object[]
			{
				(ulong)(unIP >> 24) & 255UL,
				(ulong)(unIP >> 16) & 255UL,
				(ulong)(unIP >> 8) & 255UL,
				(ulong)unIP & 255UL,
				usPort
			});
		}

		// Token: 0x0600094B RID: 2379 RVA: 0x0000E3AA File Offset: 0x0000C5AA
		public static bool operator <(servernetadr_t x, servernetadr_t y)
		{
			return x.m_unIP < y.m_unIP || (x.m_unIP == y.m_unIP && x.m_usQueryPort < y.m_usQueryPort);
		}

		// Token: 0x0600094C RID: 2380 RVA: 0x0000E3DA File Offset: 0x0000C5DA
		public static bool operator >(servernetadr_t x, servernetadr_t y)
		{
			return x.m_unIP > y.m_unIP || (x.m_unIP == y.m_unIP && x.m_usQueryPort > y.m_usQueryPort);
		}

		// Token: 0x0600094D RID: 2381 RVA: 0x0000E40A File Offset: 0x0000C60A
		public override bool Equals(object other)
		{
			return other is servernetadr_t && this == (servernetadr_t)other;
		}

		// Token: 0x0600094E RID: 2382 RVA: 0x0000E427 File Offset: 0x0000C627
		public override int GetHashCode()
		{
			return this.m_unIP.GetHashCode() + this.m_usQueryPort.GetHashCode() + this.m_usConnectionPort.GetHashCode();
		}

		// Token: 0x0600094F RID: 2383 RVA: 0x0000E44C File Offset: 0x0000C64C
		public static bool operator ==(servernetadr_t x, servernetadr_t y)
		{
			return x.m_unIP == y.m_unIP && x.m_usQueryPort == y.m_usQueryPort && x.m_usConnectionPort == y.m_usConnectionPort;
		}

		// Token: 0x06000950 RID: 2384 RVA: 0x0000E47A File Offset: 0x0000C67A
		public static bool operator !=(servernetadr_t x, servernetadr_t y)
		{
			return !(x == y);
		}

		// Token: 0x06000951 RID: 2385 RVA: 0x0000E486 File Offset: 0x0000C686
		public bool Equals(servernetadr_t other)
		{
			return this.m_unIP == other.m_unIP && this.m_usQueryPort == other.m_usQueryPort && this.m_usConnectionPort == other.m_usConnectionPort;
		}

		// Token: 0x06000952 RID: 2386 RVA: 0x0000E4B4 File Offset: 0x0000C6B4
		public int CompareTo(servernetadr_t other)
		{
			return this.m_unIP.CompareTo(other.m_unIP) + this.m_usQueryPort.CompareTo(other.m_usQueryPort) + this.m_usConnectionPort.CompareTo(other.m_usConnectionPort);
		}

		// Token: 0x04000A92 RID: 2706
		private ushort m_usConnectionPort;

		// Token: 0x04000A93 RID: 2707
		private ushort m_usQueryPort;

		// Token: 0x04000A94 RID: 2708
		private uint m_unIP;
	}
}

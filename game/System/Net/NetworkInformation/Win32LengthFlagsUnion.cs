using System;

namespace System.Net.NetworkInformation
{
	// Token: 0x02000747 RID: 1863
	internal struct Win32LengthFlagsUnion
	{
		// Token: 0x17000D43 RID: 3395
		// (get) Token: 0x06003B26 RID: 15142 RVA: 0x000CBE31 File Offset: 0x000CA031
		public bool IsDnsEligible
		{
			get
			{
				return (this.Flags & 1U) > 0U;
			}
		}

		// Token: 0x17000D44 RID: 3396
		// (get) Token: 0x06003B27 RID: 15143 RVA: 0x000CBE3E File Offset: 0x000CA03E
		public bool IsTransient
		{
			get
			{
				return (this.Flags & 2U) > 0U;
			}
		}

		// Token: 0x04002329 RID: 9001
		private const int IP_ADAPTER_ADDRESS_DNS_ELIGIBLE = 1;

		// Token: 0x0400232A RID: 9002
		private const int IP_ADAPTER_ADDRESS_TRANSIENT = 2;

		// Token: 0x0400232B RID: 9003
		public uint Length;

		// Token: 0x0400232C RID: 9004
		public uint Flags;
	}
}

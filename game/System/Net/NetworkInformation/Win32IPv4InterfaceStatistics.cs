using System;

namespace System.Net.NetworkInformation
{
	// Token: 0x02000730 RID: 1840
	internal class Win32IPv4InterfaceStatistics : IPv4InterfaceStatistics
	{
		// Token: 0x06003AB7 RID: 15031 RVA: 0x000CB474 File Offset: 0x000C9674
		public Win32IPv4InterfaceStatistics(Win32_MIB_IFROW info)
		{
			this.info = info;
		}

		// Token: 0x17000CEE RID: 3310
		// (get) Token: 0x06003AB8 RID: 15032 RVA: 0x000CB483 File Offset: 0x000C9683
		public override long BytesReceived
		{
			get
			{
				return (long)this.info.InOctets;
			}
		}

		// Token: 0x17000CEF RID: 3311
		// (get) Token: 0x06003AB9 RID: 15033 RVA: 0x000CB491 File Offset: 0x000C9691
		public override long BytesSent
		{
			get
			{
				return (long)this.info.OutOctets;
			}
		}

		// Token: 0x17000CF0 RID: 3312
		// (get) Token: 0x06003ABA RID: 15034 RVA: 0x000CB49F File Offset: 0x000C969F
		public override long IncomingPacketsDiscarded
		{
			get
			{
				return (long)this.info.InDiscards;
			}
		}

		// Token: 0x17000CF1 RID: 3313
		// (get) Token: 0x06003ABB RID: 15035 RVA: 0x000CB4AD File Offset: 0x000C96AD
		public override long IncomingPacketsWithErrors
		{
			get
			{
				return (long)this.info.InErrors;
			}
		}

		// Token: 0x17000CF2 RID: 3314
		// (get) Token: 0x06003ABC RID: 15036 RVA: 0x000CB4BB File Offset: 0x000C96BB
		public override long IncomingUnknownProtocolPackets
		{
			get
			{
				return (long)this.info.InUnknownProtos;
			}
		}

		// Token: 0x17000CF3 RID: 3315
		// (get) Token: 0x06003ABD RID: 15037 RVA: 0x000CB4C9 File Offset: 0x000C96C9
		public override long NonUnicastPacketsReceived
		{
			get
			{
				return (long)this.info.InNUcastPkts;
			}
		}

		// Token: 0x17000CF4 RID: 3316
		// (get) Token: 0x06003ABE RID: 15038 RVA: 0x000CB4D7 File Offset: 0x000C96D7
		public override long NonUnicastPacketsSent
		{
			get
			{
				return (long)this.info.OutNUcastPkts;
			}
		}

		// Token: 0x17000CF5 RID: 3317
		// (get) Token: 0x06003ABF RID: 15039 RVA: 0x000CB4E5 File Offset: 0x000C96E5
		public override long OutgoingPacketsDiscarded
		{
			get
			{
				return (long)this.info.OutDiscards;
			}
		}

		// Token: 0x17000CF6 RID: 3318
		// (get) Token: 0x06003AC0 RID: 15040 RVA: 0x000CB4F3 File Offset: 0x000C96F3
		public override long OutgoingPacketsWithErrors
		{
			get
			{
				return (long)this.info.OutErrors;
			}
		}

		// Token: 0x17000CF7 RID: 3319
		// (get) Token: 0x06003AC1 RID: 15041 RVA: 0x000CB501 File Offset: 0x000C9701
		public override long OutputQueueLength
		{
			get
			{
				return (long)this.info.OutQLen;
			}
		}

		// Token: 0x17000CF8 RID: 3320
		// (get) Token: 0x06003AC2 RID: 15042 RVA: 0x000CB50F File Offset: 0x000C970F
		public override long UnicastPacketsReceived
		{
			get
			{
				return (long)this.info.InUcastPkts;
			}
		}

		// Token: 0x17000CF9 RID: 3321
		// (get) Token: 0x06003AC3 RID: 15043 RVA: 0x000CB51D File Offset: 0x000C971D
		public override long UnicastPacketsSent
		{
			get
			{
				return (long)this.info.OutUcastPkts;
			}
		}

		// Token: 0x0400227D RID: 8829
		private Win32_MIB_IFROW info;
	}
}

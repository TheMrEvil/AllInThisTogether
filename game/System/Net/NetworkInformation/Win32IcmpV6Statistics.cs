using System;

namespace System.Net.NetworkInformation
{
	// Token: 0x02000736 RID: 1846
	internal class Win32IcmpV6Statistics : IcmpV6Statistics
	{
		// Token: 0x06003AE3 RID: 15075 RVA: 0x000CB6E0 File Offset: 0x000C98E0
		public Win32IcmpV6Statistics(Win32_MIB_ICMP_EX info)
		{
			this.iin = info.InStats;
			this.iout = info.OutStats;
		}

		// Token: 0x17000D16 RID: 3350
		// (get) Token: 0x06003AE4 RID: 15076 RVA: 0x000CB700 File Offset: 0x000C9900
		public override long DestinationUnreachableMessagesReceived
		{
			get
			{
				return (long)((ulong)this.iin.Counts[1]);
			}
		}

		// Token: 0x17000D17 RID: 3351
		// (get) Token: 0x06003AE5 RID: 15077 RVA: 0x000CB710 File Offset: 0x000C9910
		public override long DestinationUnreachableMessagesSent
		{
			get
			{
				return (long)((ulong)this.iout.Counts[1]);
			}
		}

		// Token: 0x17000D18 RID: 3352
		// (get) Token: 0x06003AE6 RID: 15078 RVA: 0x000CB720 File Offset: 0x000C9920
		public override long EchoRepliesReceived
		{
			get
			{
				return (long)((ulong)this.iin.Counts[129]);
			}
		}

		// Token: 0x17000D19 RID: 3353
		// (get) Token: 0x06003AE7 RID: 15079 RVA: 0x000CB734 File Offset: 0x000C9934
		public override long EchoRepliesSent
		{
			get
			{
				return (long)((ulong)this.iout.Counts[129]);
			}
		}

		// Token: 0x17000D1A RID: 3354
		// (get) Token: 0x06003AE8 RID: 15080 RVA: 0x000CB748 File Offset: 0x000C9948
		public override long EchoRequestsReceived
		{
			get
			{
				return (long)((ulong)this.iin.Counts[128]);
			}
		}

		// Token: 0x17000D1B RID: 3355
		// (get) Token: 0x06003AE9 RID: 15081 RVA: 0x000CB75C File Offset: 0x000C995C
		public override long EchoRequestsSent
		{
			get
			{
				return (long)((ulong)this.iout.Counts[128]);
			}
		}

		// Token: 0x17000D1C RID: 3356
		// (get) Token: 0x06003AEA RID: 15082 RVA: 0x000CB770 File Offset: 0x000C9970
		public override long ErrorsReceived
		{
			get
			{
				return (long)((ulong)this.iin.Errors);
			}
		}

		// Token: 0x17000D1D RID: 3357
		// (get) Token: 0x06003AEB RID: 15083 RVA: 0x000CB77E File Offset: 0x000C997E
		public override long ErrorsSent
		{
			get
			{
				return (long)((ulong)this.iout.Errors);
			}
		}

		// Token: 0x17000D1E RID: 3358
		// (get) Token: 0x06003AEC RID: 15084 RVA: 0x000CB78C File Offset: 0x000C998C
		public override long MembershipQueriesReceived
		{
			get
			{
				return (long)((ulong)this.iin.Counts[130]);
			}
		}

		// Token: 0x17000D1F RID: 3359
		// (get) Token: 0x06003AED RID: 15085 RVA: 0x000CB7A0 File Offset: 0x000C99A0
		public override long MembershipQueriesSent
		{
			get
			{
				return (long)((ulong)this.iout.Counts[130]);
			}
		}

		// Token: 0x17000D20 RID: 3360
		// (get) Token: 0x06003AEE RID: 15086 RVA: 0x000CB7B4 File Offset: 0x000C99B4
		public override long MembershipReductionsReceived
		{
			get
			{
				return (long)((ulong)this.iin.Counts[132]);
			}
		}

		// Token: 0x17000D21 RID: 3361
		// (get) Token: 0x06003AEF RID: 15087 RVA: 0x000CB7C8 File Offset: 0x000C99C8
		public override long MembershipReductionsSent
		{
			get
			{
				return (long)((ulong)this.iout.Counts[132]);
			}
		}

		// Token: 0x17000D22 RID: 3362
		// (get) Token: 0x06003AF0 RID: 15088 RVA: 0x000CB7DC File Offset: 0x000C99DC
		public override long MembershipReportsReceived
		{
			get
			{
				return (long)((ulong)this.iin.Counts[131]);
			}
		}

		// Token: 0x17000D23 RID: 3363
		// (get) Token: 0x06003AF1 RID: 15089 RVA: 0x000CB7F0 File Offset: 0x000C99F0
		public override long MembershipReportsSent
		{
			get
			{
				return (long)((ulong)this.iout.Counts[131]);
			}
		}

		// Token: 0x17000D24 RID: 3364
		// (get) Token: 0x06003AF2 RID: 15090 RVA: 0x000CB804 File Offset: 0x000C9A04
		public override long MessagesReceived
		{
			get
			{
				return (long)((ulong)this.iin.Msgs);
			}
		}

		// Token: 0x17000D25 RID: 3365
		// (get) Token: 0x06003AF3 RID: 15091 RVA: 0x000CB812 File Offset: 0x000C9A12
		public override long MessagesSent
		{
			get
			{
				return (long)((ulong)this.iout.Msgs);
			}
		}

		// Token: 0x17000D26 RID: 3366
		// (get) Token: 0x06003AF4 RID: 15092 RVA: 0x000CB820 File Offset: 0x000C9A20
		public override long NeighborAdvertisementsReceived
		{
			get
			{
				return (long)((ulong)this.iin.Counts[136]);
			}
		}

		// Token: 0x17000D27 RID: 3367
		// (get) Token: 0x06003AF5 RID: 15093 RVA: 0x000CB834 File Offset: 0x000C9A34
		public override long NeighborAdvertisementsSent
		{
			get
			{
				return (long)((ulong)this.iout.Counts[136]);
			}
		}

		// Token: 0x17000D28 RID: 3368
		// (get) Token: 0x06003AF6 RID: 15094 RVA: 0x000CB848 File Offset: 0x000C9A48
		public override long NeighborSolicitsReceived
		{
			get
			{
				return (long)((ulong)this.iin.Counts[135]);
			}
		}

		// Token: 0x17000D29 RID: 3369
		// (get) Token: 0x06003AF7 RID: 15095 RVA: 0x000CB85C File Offset: 0x000C9A5C
		public override long NeighborSolicitsSent
		{
			get
			{
				return (long)((ulong)this.iout.Counts[135]);
			}
		}

		// Token: 0x17000D2A RID: 3370
		// (get) Token: 0x06003AF8 RID: 15096 RVA: 0x000CB870 File Offset: 0x000C9A70
		public override long PacketTooBigMessagesReceived
		{
			get
			{
				return (long)((ulong)this.iin.Counts[2]);
			}
		}

		// Token: 0x17000D2B RID: 3371
		// (get) Token: 0x06003AF9 RID: 15097 RVA: 0x000CB880 File Offset: 0x000C9A80
		public override long PacketTooBigMessagesSent
		{
			get
			{
				return (long)((ulong)this.iout.Counts[2]);
			}
		}

		// Token: 0x17000D2C RID: 3372
		// (get) Token: 0x06003AFA RID: 15098 RVA: 0x000CB890 File Offset: 0x000C9A90
		public override long ParameterProblemsReceived
		{
			get
			{
				return (long)((ulong)this.iin.Counts[4]);
			}
		}

		// Token: 0x17000D2D RID: 3373
		// (get) Token: 0x06003AFB RID: 15099 RVA: 0x000CB8A0 File Offset: 0x000C9AA0
		public override long ParameterProblemsSent
		{
			get
			{
				return (long)((ulong)this.iout.Counts[4]);
			}
		}

		// Token: 0x17000D2E RID: 3374
		// (get) Token: 0x06003AFC RID: 15100 RVA: 0x000CB8B0 File Offset: 0x000C9AB0
		public override long RedirectsReceived
		{
			get
			{
				return (long)((ulong)this.iin.Counts[137]);
			}
		}

		// Token: 0x17000D2F RID: 3375
		// (get) Token: 0x06003AFD RID: 15101 RVA: 0x000CB8C4 File Offset: 0x000C9AC4
		public override long RedirectsSent
		{
			get
			{
				return (long)((ulong)this.iout.Counts[137]);
			}
		}

		// Token: 0x17000D30 RID: 3376
		// (get) Token: 0x06003AFE RID: 15102 RVA: 0x000CB8D8 File Offset: 0x000C9AD8
		public override long RouterAdvertisementsReceived
		{
			get
			{
				return (long)((ulong)this.iin.Counts[134]);
			}
		}

		// Token: 0x17000D31 RID: 3377
		// (get) Token: 0x06003AFF RID: 15103 RVA: 0x000CB8EC File Offset: 0x000C9AEC
		public override long RouterAdvertisementsSent
		{
			get
			{
				return (long)((ulong)this.iout.Counts[134]);
			}
		}

		// Token: 0x17000D32 RID: 3378
		// (get) Token: 0x06003B00 RID: 15104 RVA: 0x000CB900 File Offset: 0x000C9B00
		public override long RouterSolicitsReceived
		{
			get
			{
				return (long)((ulong)this.iin.Counts[133]);
			}
		}

		// Token: 0x17000D33 RID: 3379
		// (get) Token: 0x06003B01 RID: 15105 RVA: 0x000CB914 File Offset: 0x000C9B14
		public override long RouterSolicitsSent
		{
			get
			{
				return (long)((ulong)this.iout.Counts[133]);
			}
		}

		// Token: 0x17000D34 RID: 3380
		// (get) Token: 0x06003B02 RID: 15106 RVA: 0x000CB928 File Offset: 0x000C9B28
		public override long TimeExceededMessagesReceived
		{
			get
			{
				return (long)((ulong)this.iin.Counts[3]);
			}
		}

		// Token: 0x17000D35 RID: 3381
		// (get) Token: 0x06003B03 RID: 15107 RVA: 0x000CB938 File Offset: 0x000C9B38
		public override long TimeExceededMessagesSent
		{
			get
			{
				return (long)((ulong)this.iout.Counts[3]);
			}
		}

		// Token: 0x0400229F RID: 8863
		private Win32_MIBICMPSTATS_EX iin;

		// Token: 0x040022A0 RID: 8864
		private Win32_MIBICMPSTATS_EX iout;
	}
}

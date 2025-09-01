using System;

namespace System.Net.NetworkInformation
{
	// Token: 0x02000732 RID: 1842
	internal class Win32IcmpV4Statistics : IcmpV4Statistics
	{
		// Token: 0x06003AC7 RID: 15047 RVA: 0x000CB554 File Offset: 0x000C9754
		public Win32IcmpV4Statistics(Win32_MIBICMPINFO info)
		{
			this.iin = info.InStats;
			this.iout = info.OutStats;
		}

		// Token: 0x17000CFC RID: 3324
		// (get) Token: 0x06003AC8 RID: 15048 RVA: 0x000CB574 File Offset: 0x000C9774
		public override long AddressMaskRepliesReceived
		{
			get
			{
				return (long)((ulong)this.iin.AddrMaskReps);
			}
		}

		// Token: 0x17000CFD RID: 3325
		// (get) Token: 0x06003AC9 RID: 15049 RVA: 0x000CB582 File Offset: 0x000C9782
		public override long AddressMaskRepliesSent
		{
			get
			{
				return (long)((ulong)this.iout.AddrMaskReps);
			}
		}

		// Token: 0x17000CFE RID: 3326
		// (get) Token: 0x06003ACA RID: 15050 RVA: 0x000CB590 File Offset: 0x000C9790
		public override long AddressMaskRequestsReceived
		{
			get
			{
				return (long)((ulong)this.iin.AddrMasks);
			}
		}

		// Token: 0x17000CFF RID: 3327
		// (get) Token: 0x06003ACB RID: 15051 RVA: 0x000CB59E File Offset: 0x000C979E
		public override long AddressMaskRequestsSent
		{
			get
			{
				return (long)((ulong)this.iout.AddrMasks);
			}
		}

		// Token: 0x17000D00 RID: 3328
		// (get) Token: 0x06003ACC RID: 15052 RVA: 0x000CB5AC File Offset: 0x000C97AC
		public override long DestinationUnreachableMessagesReceived
		{
			get
			{
				return (long)((ulong)this.iin.DestUnreachs);
			}
		}

		// Token: 0x17000D01 RID: 3329
		// (get) Token: 0x06003ACD RID: 15053 RVA: 0x000CB5BA File Offset: 0x000C97BA
		public override long DestinationUnreachableMessagesSent
		{
			get
			{
				return (long)((ulong)this.iout.DestUnreachs);
			}
		}

		// Token: 0x17000D02 RID: 3330
		// (get) Token: 0x06003ACE RID: 15054 RVA: 0x000CB5C8 File Offset: 0x000C97C8
		public override long EchoRepliesReceived
		{
			get
			{
				return (long)((ulong)this.iin.EchoReps);
			}
		}

		// Token: 0x17000D03 RID: 3331
		// (get) Token: 0x06003ACF RID: 15055 RVA: 0x000CB5D6 File Offset: 0x000C97D6
		public override long EchoRepliesSent
		{
			get
			{
				return (long)((ulong)this.iout.EchoReps);
			}
		}

		// Token: 0x17000D04 RID: 3332
		// (get) Token: 0x06003AD0 RID: 15056 RVA: 0x000CB5E4 File Offset: 0x000C97E4
		public override long EchoRequestsReceived
		{
			get
			{
				return (long)((ulong)this.iin.Echos);
			}
		}

		// Token: 0x17000D05 RID: 3333
		// (get) Token: 0x06003AD1 RID: 15057 RVA: 0x000CB5F2 File Offset: 0x000C97F2
		public override long EchoRequestsSent
		{
			get
			{
				return (long)((ulong)this.iout.Echos);
			}
		}

		// Token: 0x17000D06 RID: 3334
		// (get) Token: 0x06003AD2 RID: 15058 RVA: 0x000CB600 File Offset: 0x000C9800
		public override long ErrorsReceived
		{
			get
			{
				return (long)((ulong)this.iin.Errors);
			}
		}

		// Token: 0x17000D07 RID: 3335
		// (get) Token: 0x06003AD3 RID: 15059 RVA: 0x000CB60E File Offset: 0x000C980E
		public override long ErrorsSent
		{
			get
			{
				return (long)((ulong)this.iout.Errors);
			}
		}

		// Token: 0x17000D08 RID: 3336
		// (get) Token: 0x06003AD4 RID: 15060 RVA: 0x000CB61C File Offset: 0x000C981C
		public override long MessagesReceived
		{
			get
			{
				return (long)((ulong)this.iin.Msgs);
			}
		}

		// Token: 0x17000D09 RID: 3337
		// (get) Token: 0x06003AD5 RID: 15061 RVA: 0x000CB62A File Offset: 0x000C982A
		public override long MessagesSent
		{
			get
			{
				return (long)((ulong)this.iout.Msgs);
			}
		}

		// Token: 0x17000D0A RID: 3338
		// (get) Token: 0x06003AD6 RID: 15062 RVA: 0x000CB638 File Offset: 0x000C9838
		public override long ParameterProblemsReceived
		{
			get
			{
				return (long)((ulong)this.iin.ParmProbs);
			}
		}

		// Token: 0x17000D0B RID: 3339
		// (get) Token: 0x06003AD7 RID: 15063 RVA: 0x000CB646 File Offset: 0x000C9846
		public override long ParameterProblemsSent
		{
			get
			{
				return (long)((ulong)this.iout.ParmProbs);
			}
		}

		// Token: 0x17000D0C RID: 3340
		// (get) Token: 0x06003AD8 RID: 15064 RVA: 0x000CB654 File Offset: 0x000C9854
		public override long RedirectsReceived
		{
			get
			{
				return (long)((ulong)this.iin.Redirects);
			}
		}

		// Token: 0x17000D0D RID: 3341
		// (get) Token: 0x06003AD9 RID: 15065 RVA: 0x000CB662 File Offset: 0x000C9862
		public override long RedirectsSent
		{
			get
			{
				return (long)((ulong)this.iout.Redirects);
			}
		}

		// Token: 0x17000D0E RID: 3342
		// (get) Token: 0x06003ADA RID: 15066 RVA: 0x000CB670 File Offset: 0x000C9870
		public override long SourceQuenchesReceived
		{
			get
			{
				return (long)((ulong)this.iin.SrcQuenchs);
			}
		}

		// Token: 0x17000D0F RID: 3343
		// (get) Token: 0x06003ADB RID: 15067 RVA: 0x000CB67E File Offset: 0x000C987E
		public override long SourceQuenchesSent
		{
			get
			{
				return (long)((ulong)this.iout.SrcQuenchs);
			}
		}

		// Token: 0x17000D10 RID: 3344
		// (get) Token: 0x06003ADC RID: 15068 RVA: 0x000CB68C File Offset: 0x000C988C
		public override long TimeExceededMessagesReceived
		{
			get
			{
				return (long)((ulong)this.iin.TimeExcds);
			}
		}

		// Token: 0x17000D11 RID: 3345
		// (get) Token: 0x06003ADD RID: 15069 RVA: 0x000CB69A File Offset: 0x000C989A
		public override long TimeExceededMessagesSent
		{
			get
			{
				return (long)((ulong)this.iout.TimeExcds);
			}
		}

		// Token: 0x17000D12 RID: 3346
		// (get) Token: 0x06003ADE RID: 15070 RVA: 0x000CB6A8 File Offset: 0x000C98A8
		public override long TimestampRepliesReceived
		{
			get
			{
				return (long)((ulong)this.iin.TimestampReps);
			}
		}

		// Token: 0x17000D13 RID: 3347
		// (get) Token: 0x06003ADF RID: 15071 RVA: 0x000CB6B6 File Offset: 0x000C98B6
		public override long TimestampRepliesSent
		{
			get
			{
				return (long)((ulong)this.iout.TimestampReps);
			}
		}

		// Token: 0x17000D14 RID: 3348
		// (get) Token: 0x06003AE0 RID: 15072 RVA: 0x000CB6C4 File Offset: 0x000C98C4
		public override long TimestampRequestsReceived
		{
			get
			{
				return (long)((ulong)this.iin.Timestamps);
			}
		}

		// Token: 0x17000D15 RID: 3349
		// (get) Token: 0x06003AE1 RID: 15073 RVA: 0x000CB6D2 File Offset: 0x000C98D2
		public override long TimestampRequestsSent
		{
			get
			{
				return (long)((ulong)this.iout.Timestamps);
			}
		}

		// Token: 0x0400227F RID: 8831
		private Win32_MIBICMPSTATS iin;

		// Token: 0x04002280 RID: 8832
		private Win32_MIBICMPSTATS iout;
	}
}

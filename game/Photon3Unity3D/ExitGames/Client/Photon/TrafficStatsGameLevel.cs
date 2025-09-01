using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace ExitGames.Client.Photon
{
	// Token: 0x0200003C RID: 60
	public class TrafficStatsGameLevel
	{
		// Token: 0x17000098 RID: 152
		// (get) Token: 0x0600030A RID: 778 RVA: 0x00019341 File Offset: 0x00017541
		// (set) Token: 0x0600030B RID: 779 RVA: 0x00019349 File Offset: 0x00017549
		public int OperationByteCount
		{
			[CompilerGenerated]
			get
			{
				return this.<OperationByteCount>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<OperationByteCount>k__BackingField = value;
			}
		}

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x0600030C RID: 780 RVA: 0x00019352 File Offset: 0x00017552
		// (set) Token: 0x0600030D RID: 781 RVA: 0x0001935A File Offset: 0x0001755A
		public int OperationCount
		{
			[CompilerGenerated]
			get
			{
				return this.<OperationCount>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<OperationCount>k__BackingField = value;
			}
		}

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x0600030E RID: 782 RVA: 0x00019363 File Offset: 0x00017563
		// (set) Token: 0x0600030F RID: 783 RVA: 0x0001936B File Offset: 0x0001756B
		public int ResultByteCount
		{
			[CompilerGenerated]
			get
			{
				return this.<ResultByteCount>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<ResultByteCount>k__BackingField = value;
			}
		}

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x06000310 RID: 784 RVA: 0x00019374 File Offset: 0x00017574
		// (set) Token: 0x06000311 RID: 785 RVA: 0x0001937C File Offset: 0x0001757C
		public int ResultCount
		{
			[CompilerGenerated]
			get
			{
				return this.<ResultCount>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<ResultCount>k__BackingField = value;
			}
		}

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x06000312 RID: 786 RVA: 0x00019385 File Offset: 0x00017585
		// (set) Token: 0x06000313 RID: 787 RVA: 0x0001938D File Offset: 0x0001758D
		public int EventByteCount
		{
			[CompilerGenerated]
			get
			{
				return this.<EventByteCount>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<EventByteCount>k__BackingField = value;
			}
		}

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x06000314 RID: 788 RVA: 0x00019396 File Offset: 0x00017596
		// (set) Token: 0x06000315 RID: 789 RVA: 0x0001939E File Offset: 0x0001759E
		public int EventCount
		{
			[CompilerGenerated]
			get
			{
				return this.<EventCount>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<EventCount>k__BackingField = value;
			}
		}

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x06000316 RID: 790 RVA: 0x000193A7 File Offset: 0x000175A7
		// (set) Token: 0x06000317 RID: 791 RVA: 0x000193AF File Offset: 0x000175AF
		public int LongestOpResponseCallback
		{
			[CompilerGenerated]
			get
			{
				return this.<LongestOpResponseCallback>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<LongestOpResponseCallback>k__BackingField = value;
			}
		}

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x06000318 RID: 792 RVA: 0x000193B8 File Offset: 0x000175B8
		// (set) Token: 0x06000319 RID: 793 RVA: 0x000193C0 File Offset: 0x000175C0
		public byte LongestOpResponseCallbackOpCode
		{
			[CompilerGenerated]
			get
			{
				return this.<LongestOpResponseCallbackOpCode>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<LongestOpResponseCallbackOpCode>k__BackingField = value;
			}
		}

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x0600031A RID: 794 RVA: 0x000193C9 File Offset: 0x000175C9
		// (set) Token: 0x0600031B RID: 795 RVA: 0x000193D1 File Offset: 0x000175D1
		public int LongestEventCallback
		{
			[CompilerGenerated]
			get
			{
				return this.<LongestEventCallback>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<LongestEventCallback>k__BackingField = value;
			}
		}

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x0600031C RID: 796 RVA: 0x000193DA File Offset: 0x000175DA
		// (set) Token: 0x0600031D RID: 797 RVA: 0x000193E2 File Offset: 0x000175E2
		public int LongestMessageCallback
		{
			[CompilerGenerated]
			get
			{
				return this.<LongestMessageCallback>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<LongestMessageCallback>k__BackingField = value;
			}
		}

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x0600031E RID: 798 RVA: 0x000193EB File Offset: 0x000175EB
		// (set) Token: 0x0600031F RID: 799 RVA: 0x000193F3 File Offset: 0x000175F3
		public int LongestRawMessageCallback
		{
			[CompilerGenerated]
			get
			{
				return this.<LongestRawMessageCallback>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<LongestRawMessageCallback>k__BackingField = value;
			}
		}

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x06000320 RID: 800 RVA: 0x000193FC File Offset: 0x000175FC
		// (set) Token: 0x06000321 RID: 801 RVA: 0x00019404 File Offset: 0x00017604
		public byte LongestEventCallbackCode
		{
			[CompilerGenerated]
			get
			{
				return this.<LongestEventCallbackCode>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<LongestEventCallbackCode>k__BackingField = value;
			}
		}

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x06000322 RID: 802 RVA: 0x0001940D File Offset: 0x0001760D
		// (set) Token: 0x06000323 RID: 803 RVA: 0x00019415 File Offset: 0x00017615
		public int LongestDeltaBetweenDispatching
		{
			[CompilerGenerated]
			get
			{
				return this.<LongestDeltaBetweenDispatching>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<LongestDeltaBetweenDispatching>k__BackingField = value;
			}
		}

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x06000324 RID: 804 RVA: 0x0001941E File Offset: 0x0001761E
		// (set) Token: 0x06000325 RID: 805 RVA: 0x00019426 File Offset: 0x00017626
		public int LongestDeltaBetweenSending
		{
			[CompilerGenerated]
			get
			{
				return this.<LongestDeltaBetweenSending>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<LongestDeltaBetweenSending>k__BackingField = value;
			}
		}

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x06000326 RID: 806 RVA: 0x00019430 File Offset: 0x00017630
		[Obsolete("Use DispatchIncomingCommandsCalls, which has proper naming.")]
		public int DispatchCalls
		{
			get
			{
				return this.DispatchIncomingCommandsCalls;
			}
		}

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x06000327 RID: 807 RVA: 0x00019448 File Offset: 0x00017648
		// (set) Token: 0x06000328 RID: 808 RVA: 0x00019450 File Offset: 0x00017650
		public int DispatchIncomingCommandsCalls
		{
			[CompilerGenerated]
			get
			{
				return this.<DispatchIncomingCommandsCalls>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<DispatchIncomingCommandsCalls>k__BackingField = value;
			}
		}

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x06000329 RID: 809 RVA: 0x00019459 File Offset: 0x00017659
		// (set) Token: 0x0600032A RID: 810 RVA: 0x00019461 File Offset: 0x00017661
		public int SendOutgoingCommandsCalls
		{
			[CompilerGenerated]
			get
			{
				return this.<SendOutgoingCommandsCalls>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<SendOutgoingCommandsCalls>k__BackingField = value;
			}
		}

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x0600032B RID: 811 RVA: 0x0001946C File Offset: 0x0001766C
		public int TotalByteCount
		{
			get
			{
				return this.OperationByteCount + this.ResultByteCount + this.EventByteCount;
			}
		}

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x0600032C RID: 812 RVA: 0x00019494 File Offset: 0x00017694
		public int TotalMessageCount
		{
			get
			{
				return this.OperationCount + this.ResultCount + this.EventCount;
			}
		}

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x0600032D RID: 813 RVA: 0x000194BC File Offset: 0x000176BC
		public int TotalIncomingByteCount
		{
			get
			{
				return this.ResultByteCount + this.EventByteCount;
			}
		}

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x0600032E RID: 814 RVA: 0x000194DC File Offset: 0x000176DC
		public int TotalIncomingMessageCount
		{
			get
			{
				return this.ResultCount + this.EventCount;
			}
		}

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x0600032F RID: 815 RVA: 0x000194FC File Offset: 0x000176FC
		public int TotalOutgoingByteCount
		{
			get
			{
				return this.OperationByteCount;
			}
		}

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x06000330 RID: 816 RVA: 0x00019514 File Offset: 0x00017714
		public int TotalOutgoingMessageCount
		{
			get
			{
				return this.OperationCount;
			}
		}

		// Token: 0x06000331 RID: 817 RVA: 0x0001952C File Offset: 0x0001772C
		internal TrafficStatsGameLevel(Stopwatch sw)
		{
			this.watch = sw;
		}

		// Token: 0x06000332 RID: 818 RVA: 0x00019540 File Offset: 0x00017740
		internal void CountOperation(int operationBytes)
		{
			this.OperationByteCount += operationBytes;
			int operationCount = this.OperationCount;
			this.OperationCount = operationCount + 1;
		}

		// Token: 0x06000333 RID: 819 RVA: 0x00019570 File Offset: 0x00017770
		internal void CountResult(int resultBytes)
		{
			this.ResultByteCount += resultBytes;
			int resultCount = this.ResultCount;
			this.ResultCount = resultCount + 1;
		}

		// Token: 0x06000334 RID: 820 RVA: 0x000195A0 File Offset: 0x000177A0
		internal void CountEvent(int eventBytes)
		{
			this.EventByteCount += eventBytes;
			int eventCount = this.EventCount;
			this.EventCount = eventCount + 1;
		}

		// Token: 0x06000335 RID: 821 RVA: 0x000195D0 File Offset: 0x000177D0
		internal void TimeForResponseCallback(byte code, int time)
		{
			bool flag = time > this.LongestOpResponseCallback;
			if (flag)
			{
				this.LongestOpResponseCallback = time;
				this.LongestOpResponseCallbackOpCode = code;
			}
		}

		// Token: 0x06000336 RID: 822 RVA: 0x00019600 File Offset: 0x00017800
		internal void TimeForEventCallback(byte code, int time)
		{
			bool flag = time > this.LongestEventCallback;
			if (flag)
			{
				this.LongestEventCallback = time;
				this.LongestEventCallbackCode = code;
			}
		}

		// Token: 0x06000337 RID: 823 RVA: 0x00019630 File Offset: 0x00017830
		internal void TimeForMessageCallback(int time)
		{
			bool flag = time > this.LongestMessageCallback;
			if (flag)
			{
				this.LongestMessageCallback = time;
			}
		}

		// Token: 0x06000338 RID: 824 RVA: 0x00019658 File Offset: 0x00017858
		internal void TimeForRawMessageCallback(int time)
		{
			bool flag = time > this.LongestRawMessageCallback;
			if (flag)
			{
				this.LongestRawMessageCallback = time;
			}
		}

		// Token: 0x06000339 RID: 825 RVA: 0x00019680 File Offset: 0x00017880
		internal void DispatchIncomingCommandsCalled()
		{
			bool flag = this.timeOfLastDispatchCall != 0;
			if (flag)
			{
				int num = (int)this.watch.ElapsedMilliseconds - this.timeOfLastDispatchCall;
				bool flag2 = num > this.LongestDeltaBetweenDispatching;
				if (flag2)
				{
					this.LongestDeltaBetweenDispatching = num;
				}
			}
			int dispatchIncomingCommandsCalls = this.DispatchIncomingCommandsCalls;
			this.DispatchIncomingCommandsCalls = dispatchIncomingCommandsCalls + 1;
			this.timeOfLastDispatchCall = (int)this.watch.ElapsedMilliseconds;
		}

		// Token: 0x0600033A RID: 826 RVA: 0x000196EC File Offset: 0x000178EC
		internal void SendOutgoingCommandsCalled()
		{
			bool flag = this.timeOfLastSendCall != 0;
			if (flag)
			{
				int num = (int)this.watch.ElapsedMilliseconds - this.timeOfLastSendCall;
				bool flag2 = num > this.LongestDeltaBetweenSending;
				if (flag2)
				{
					this.LongestDeltaBetweenSending = num;
				}
			}
			int sendOutgoingCommandsCalls = this.SendOutgoingCommandsCalls;
			this.SendOutgoingCommandsCalls = sendOutgoingCommandsCalls + 1;
			this.timeOfLastSendCall = (int)this.watch.ElapsedMilliseconds;
		}

		// Token: 0x0600033B RID: 827 RVA: 0x00019758 File Offset: 0x00017958
		public void ResetMaximumCounters()
		{
			this.LongestDeltaBetweenDispatching = 0;
			this.LongestDeltaBetweenSending = 0;
			this.LongestEventCallback = 0;
			this.LongestEventCallbackCode = 0;
			this.LongestOpResponseCallback = 0;
			this.LongestOpResponseCallbackOpCode = 0;
			this.timeOfLastDispatchCall = 0;
			this.timeOfLastSendCall = 0;
		}

		// Token: 0x0600033C RID: 828 RVA: 0x000197A4 File Offset: 0x000179A4
		public override string ToString()
		{
			return string.Format("OperationByteCount: {0} ResultByteCount: {1} EventByteCount: {2}", this.OperationByteCount, this.ResultByteCount, this.EventByteCount);
		}

		// Token: 0x0600033D RID: 829 RVA: 0x000197E4 File Offset: 0x000179E4
		public string ToStringVitalStats()
		{
			return string.Format("Longest delta between Send: {0}ms Dispatch: {1}ms. Longest callback OnEv: {3}={2}ms OnResp: {5}={4}ms. Calls of Send: {6} Dispatch: {7}.", new object[]
			{
				this.LongestDeltaBetweenSending,
				this.LongestDeltaBetweenDispatching,
				this.LongestEventCallback,
				this.LongestEventCallbackCode,
				this.LongestOpResponseCallback,
				this.LongestOpResponseCallbackOpCode,
				this.SendOutgoingCommandsCalls,
				this.DispatchIncomingCommandsCalls
			});
		}

		// Token: 0x040001BB RID: 443
		private Stopwatch watch;

		// Token: 0x040001BC RID: 444
		private int timeOfLastDispatchCall;

		// Token: 0x040001BD RID: 445
		private int timeOfLastSendCall;

		// Token: 0x040001BE RID: 446
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private int <OperationByteCount>k__BackingField;

		// Token: 0x040001BF RID: 447
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private int <OperationCount>k__BackingField;

		// Token: 0x040001C0 RID: 448
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private int <ResultByteCount>k__BackingField;

		// Token: 0x040001C1 RID: 449
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private int <ResultCount>k__BackingField;

		// Token: 0x040001C2 RID: 450
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private int <EventByteCount>k__BackingField;

		// Token: 0x040001C3 RID: 451
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private int <EventCount>k__BackingField;

		// Token: 0x040001C4 RID: 452
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private int <LongestOpResponseCallback>k__BackingField;

		// Token: 0x040001C5 RID: 453
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private byte <LongestOpResponseCallbackOpCode>k__BackingField;

		// Token: 0x040001C6 RID: 454
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private int <LongestEventCallback>k__BackingField;

		// Token: 0x040001C7 RID: 455
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private int <LongestMessageCallback>k__BackingField;

		// Token: 0x040001C8 RID: 456
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private int <LongestRawMessageCallback>k__BackingField;

		// Token: 0x040001C9 RID: 457
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private byte <LongestEventCallbackCode>k__BackingField;

		// Token: 0x040001CA RID: 458
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private int <LongestDeltaBetweenDispatching>k__BackingField;

		// Token: 0x040001CB RID: 459
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private int <LongestDeltaBetweenSending>k__BackingField;

		// Token: 0x040001CC RID: 460
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private int <DispatchIncomingCommandsCalls>k__BackingField;

		// Token: 0x040001CD RID: 461
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private int <SendOutgoingCommandsCalls>k__BackingField;
	}
}

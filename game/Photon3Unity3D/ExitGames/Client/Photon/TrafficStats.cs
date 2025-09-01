using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace ExitGames.Client.Photon
{
	// Token: 0x0200003D RID: 61
	public class TrafficStats
	{
		// Token: 0x170000AF RID: 175
		// (get) Token: 0x0600033E RID: 830 RVA: 0x00019876 File Offset: 0x00017A76
		// (set) Token: 0x0600033F RID: 831 RVA: 0x0001987E File Offset: 0x00017A7E
		public int PackageHeaderSize
		{
			[CompilerGenerated]
			get
			{
				return this.<PackageHeaderSize>k__BackingField;
			}
			[CompilerGenerated]
			internal set
			{
				this.<PackageHeaderSize>k__BackingField = value;
			}
		}

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x06000340 RID: 832 RVA: 0x00019887 File Offset: 0x00017A87
		// (set) Token: 0x06000341 RID: 833 RVA: 0x0001988F File Offset: 0x00017A8F
		public int ReliableCommandCount
		{
			[CompilerGenerated]
			get
			{
				return this.<ReliableCommandCount>k__BackingField;
			}
			[CompilerGenerated]
			internal set
			{
				this.<ReliableCommandCount>k__BackingField = value;
			}
		}

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x06000342 RID: 834 RVA: 0x00019898 File Offset: 0x00017A98
		// (set) Token: 0x06000343 RID: 835 RVA: 0x000198A0 File Offset: 0x00017AA0
		public int UnreliableCommandCount
		{
			[CompilerGenerated]
			get
			{
				return this.<UnreliableCommandCount>k__BackingField;
			}
			[CompilerGenerated]
			internal set
			{
				this.<UnreliableCommandCount>k__BackingField = value;
			}
		}

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x06000344 RID: 836 RVA: 0x000198A9 File Offset: 0x00017AA9
		// (set) Token: 0x06000345 RID: 837 RVA: 0x000198B1 File Offset: 0x00017AB1
		public int FragmentCommandCount
		{
			[CompilerGenerated]
			get
			{
				return this.<FragmentCommandCount>k__BackingField;
			}
			[CompilerGenerated]
			internal set
			{
				this.<FragmentCommandCount>k__BackingField = value;
			}
		}

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x06000346 RID: 838 RVA: 0x000198BA File Offset: 0x00017ABA
		// (set) Token: 0x06000347 RID: 839 RVA: 0x000198C2 File Offset: 0x00017AC2
		public int ControlCommandCount
		{
			[CompilerGenerated]
			get
			{
				return this.<ControlCommandCount>k__BackingField;
			}
			[CompilerGenerated]
			internal set
			{
				this.<ControlCommandCount>k__BackingField = value;
			}
		}

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x06000348 RID: 840 RVA: 0x000198CB File Offset: 0x00017ACB
		// (set) Token: 0x06000349 RID: 841 RVA: 0x000198D3 File Offset: 0x00017AD3
		public int TotalPacketCount
		{
			[CompilerGenerated]
			get
			{
				return this.<TotalPacketCount>k__BackingField;
			}
			[CompilerGenerated]
			internal set
			{
				this.<TotalPacketCount>k__BackingField = value;
			}
		}

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x0600034A RID: 842 RVA: 0x000198DC File Offset: 0x00017ADC
		// (set) Token: 0x0600034B RID: 843 RVA: 0x000198E4 File Offset: 0x00017AE4
		public int TotalCommandsInPackets
		{
			[CompilerGenerated]
			get
			{
				return this.<TotalCommandsInPackets>k__BackingField;
			}
			[CompilerGenerated]
			internal set
			{
				this.<TotalCommandsInPackets>k__BackingField = value;
			}
		}

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x0600034C RID: 844 RVA: 0x000198ED File Offset: 0x00017AED
		// (set) Token: 0x0600034D RID: 845 RVA: 0x000198F5 File Offset: 0x00017AF5
		public int ReliableCommandBytes
		{
			[CompilerGenerated]
			get
			{
				return this.<ReliableCommandBytes>k__BackingField;
			}
			[CompilerGenerated]
			internal set
			{
				this.<ReliableCommandBytes>k__BackingField = value;
			}
		}

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x0600034E RID: 846 RVA: 0x000198FE File Offset: 0x00017AFE
		// (set) Token: 0x0600034F RID: 847 RVA: 0x00019906 File Offset: 0x00017B06
		public int UnreliableCommandBytes
		{
			[CompilerGenerated]
			get
			{
				return this.<UnreliableCommandBytes>k__BackingField;
			}
			[CompilerGenerated]
			internal set
			{
				this.<UnreliableCommandBytes>k__BackingField = value;
			}
		}

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x06000350 RID: 848 RVA: 0x0001990F File Offset: 0x00017B0F
		// (set) Token: 0x06000351 RID: 849 RVA: 0x00019917 File Offset: 0x00017B17
		public int FragmentCommandBytes
		{
			[CompilerGenerated]
			get
			{
				return this.<FragmentCommandBytes>k__BackingField;
			}
			[CompilerGenerated]
			internal set
			{
				this.<FragmentCommandBytes>k__BackingField = value;
			}
		}

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x06000352 RID: 850 RVA: 0x00019920 File Offset: 0x00017B20
		// (set) Token: 0x06000353 RID: 851 RVA: 0x00019928 File Offset: 0x00017B28
		public int ControlCommandBytes
		{
			[CompilerGenerated]
			get
			{
				return this.<ControlCommandBytes>k__BackingField;
			}
			[CompilerGenerated]
			internal set
			{
				this.<ControlCommandBytes>k__BackingField = value;
			}
		}

		// Token: 0x06000354 RID: 852 RVA: 0x00019931 File Offset: 0x00017B31
		internal TrafficStats(int packageHeaderSize)
		{
			this.PackageHeaderSize = packageHeaderSize;
		}

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x06000355 RID: 853 RVA: 0x00019944 File Offset: 0x00017B44
		public int TotalCommandCount
		{
			get
			{
				return this.ReliableCommandCount + this.UnreliableCommandCount + this.FragmentCommandCount + this.ControlCommandCount;
			}
		}

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x06000356 RID: 854 RVA: 0x00019974 File Offset: 0x00017B74
		public int TotalCommandBytes
		{
			get
			{
				return this.ReliableCommandBytes + this.UnreliableCommandBytes + this.FragmentCommandBytes + this.ControlCommandBytes;
			}
		}

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x06000357 RID: 855 RVA: 0x000199A4 File Offset: 0x00017BA4
		public int TotalPacketBytes
		{
			get
			{
				return this.TotalCommandBytes + this.TotalPacketCount * this.PackageHeaderSize;
			}
		}

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x06000358 RID: 856 RVA: 0x000199CA File Offset: 0x00017BCA
		// (set) Token: 0x06000359 RID: 857 RVA: 0x000199D2 File Offset: 0x00017BD2
		public int TimestampOfLastAck
		{
			[CompilerGenerated]
			get
			{
				return this.<TimestampOfLastAck>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<TimestampOfLastAck>k__BackingField = value;
			}
		}

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x0600035A RID: 858 RVA: 0x000199DB File Offset: 0x00017BDB
		// (set) Token: 0x0600035B RID: 859 RVA: 0x000199E3 File Offset: 0x00017BE3
		public int TimestampOfLastReliableCommand
		{
			[CompilerGenerated]
			get
			{
				return this.<TimestampOfLastReliableCommand>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<TimestampOfLastReliableCommand>k__BackingField = value;
			}
		}

		// Token: 0x0600035C RID: 860 RVA: 0x000199EC File Offset: 0x00017BEC
		internal void CountControlCommand(int size)
		{
			this.ControlCommandBytes += size;
			int controlCommandCount = this.ControlCommandCount;
			this.ControlCommandCount = controlCommandCount + 1;
		}

		// Token: 0x0600035D RID: 861 RVA: 0x00019A1C File Offset: 0x00017C1C
		internal void CountReliableOpCommand(int size)
		{
			this.ReliableCommandBytes += size;
			int reliableCommandCount = this.ReliableCommandCount;
			this.ReliableCommandCount = reliableCommandCount + 1;
		}

		// Token: 0x0600035E RID: 862 RVA: 0x00019A4C File Offset: 0x00017C4C
		internal void CountUnreliableOpCommand(int size)
		{
			this.UnreliableCommandBytes += size;
			int unreliableCommandCount = this.UnreliableCommandCount;
			this.UnreliableCommandCount = unreliableCommandCount + 1;
		}

		// Token: 0x0600035F RID: 863 RVA: 0x00019A7C File Offset: 0x00017C7C
		internal void CountFragmentOpCommand(int size)
		{
			this.FragmentCommandBytes += size;
			int fragmentCommandCount = this.FragmentCommandCount;
			this.FragmentCommandCount = fragmentCommandCount + 1;
		}

		// Token: 0x06000360 RID: 864 RVA: 0x00019AAC File Offset: 0x00017CAC
		public override string ToString()
		{
			return string.Format("TotalPacketBytes: {0} TotalCommandBytes: {1} TotalPacketCount: {2} TotalCommandsInPackets: {3}", new object[]
			{
				this.TotalPacketBytes,
				this.TotalCommandBytes,
				this.TotalPacketCount,
				this.TotalCommandsInPackets
			});
		}

		// Token: 0x040001CE RID: 462
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private int <PackageHeaderSize>k__BackingField;

		// Token: 0x040001CF RID: 463
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private int <ReliableCommandCount>k__BackingField;

		// Token: 0x040001D0 RID: 464
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private int <UnreliableCommandCount>k__BackingField;

		// Token: 0x040001D1 RID: 465
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private int <FragmentCommandCount>k__BackingField;

		// Token: 0x040001D2 RID: 466
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private int <ControlCommandCount>k__BackingField;

		// Token: 0x040001D3 RID: 467
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private int <TotalPacketCount>k__BackingField;

		// Token: 0x040001D4 RID: 468
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private int <TotalCommandsInPackets>k__BackingField;

		// Token: 0x040001D5 RID: 469
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private int <ReliableCommandBytes>k__BackingField;

		// Token: 0x040001D6 RID: 470
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private int <UnreliableCommandBytes>k__BackingField;

		// Token: 0x040001D7 RID: 471
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private int <FragmentCommandBytes>k__BackingField;

		// Token: 0x040001D8 RID: 472
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private int <ControlCommandBytes>k__BackingField;

		// Token: 0x040001D9 RID: 473
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private int <TimestampOfLastAck>k__BackingField;

		// Token: 0x040001DA RID: 474
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private int <TimestampOfLastReliableCommand>k__BackingField;
	}
}

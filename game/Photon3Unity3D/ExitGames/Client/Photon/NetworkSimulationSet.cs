using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading;

namespace ExitGames.Client.Photon
{
	// Token: 0x02000016 RID: 22
	public class NetworkSimulationSet
	{
		// Token: 0x17000029 RID: 41
		// (get) Token: 0x060000D0 RID: 208 RVA: 0x0000826C File Offset: 0x0000646C
		// (set) Token: 0x060000D1 RID: 209 RVA: 0x00008284 File Offset: 0x00006484
		protected internal bool IsSimulationEnabled
		{
			get
			{
				return this.isSimulationEnabled;
			}
			set
			{
				ManualResetEvent netSimManualResetEvent = this.NetSimManualResetEvent;
				lock (netSimManualResetEvent)
				{
					bool flag2 = value == this.isSimulationEnabled;
					if (!flag2)
					{
						bool flag3 = !value;
						if (flag3)
						{
							LinkedList<SimulationItem> netSimListIncoming = this.peerBase.NetSimListIncoming;
							lock (netSimListIncoming)
							{
								foreach (SimulationItem simulationItem in this.peerBase.NetSimListIncoming)
								{
									bool flag5 = this.peerBase.PhotonSocket != null && this.peerBase.PhotonSocket.Connected;
									if (flag5)
									{
										this.peerBase.ReceiveIncomingCommands(simulationItem.DelayedData, simulationItem.DelayedData.Length);
									}
								}
								this.peerBase.NetSimListIncoming.Clear();
							}
							LinkedList<SimulationItem> netSimListOutgoing = this.peerBase.NetSimListOutgoing;
							lock (netSimListOutgoing)
							{
								foreach (SimulationItem simulationItem2 in this.peerBase.NetSimListOutgoing)
								{
									bool flag7 = this.peerBase.PhotonSocket != null && this.peerBase.PhotonSocket.Connected;
									if (flag7)
									{
										this.peerBase.PhotonSocket.Send(simulationItem2.DelayedData, simulationItem2.DelayedData.Length);
									}
								}
								this.peerBase.NetSimListOutgoing.Clear();
							}
						}
						this.isSimulationEnabled = value;
						bool flag8 = this.isSimulationEnabled;
						if (flag8)
						{
							bool flag9 = this.netSimThread == null;
							if (flag9)
							{
								this.netSimThread = new Thread(new ThreadStart(this.peerBase.NetworkSimRun));
								this.netSimThread.IsBackground = true;
								this.netSimThread.Name = "netSim";
								this.netSimThread.Start();
							}
							this.NetSimManualResetEvent.Set();
						}
						else
						{
							this.NetSimManualResetEvent.Reset();
						}
					}
				}
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x060000D2 RID: 210 RVA: 0x00008550 File Offset: 0x00006750
		// (set) Token: 0x060000D3 RID: 211 RVA: 0x00008568 File Offset: 0x00006768
		public int OutgoingLag
		{
			get
			{
				return this.outgoingLag;
			}
			set
			{
				this.outgoingLag = value;
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x060000D4 RID: 212 RVA: 0x00008574 File Offset: 0x00006774
		// (set) Token: 0x060000D5 RID: 213 RVA: 0x0000858C File Offset: 0x0000678C
		public int OutgoingJitter
		{
			get
			{
				return this.outgoingJitter;
			}
			set
			{
				this.outgoingJitter = value;
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x060000D6 RID: 214 RVA: 0x00008598 File Offset: 0x00006798
		// (set) Token: 0x060000D7 RID: 215 RVA: 0x000085B0 File Offset: 0x000067B0
		public int OutgoingLossPercentage
		{
			get
			{
				return this.outgoingLossPercentage;
			}
			set
			{
				this.outgoingLossPercentage = value;
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x060000D8 RID: 216 RVA: 0x000085BC File Offset: 0x000067BC
		// (set) Token: 0x060000D9 RID: 217 RVA: 0x000085D4 File Offset: 0x000067D4
		public int IncomingLag
		{
			get
			{
				return this.incomingLag;
			}
			set
			{
				this.incomingLag = value;
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x060000DA RID: 218 RVA: 0x000085E0 File Offset: 0x000067E0
		// (set) Token: 0x060000DB RID: 219 RVA: 0x000085F8 File Offset: 0x000067F8
		public int IncomingJitter
		{
			get
			{
				return this.incomingJitter;
			}
			set
			{
				this.incomingJitter = value;
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x060000DC RID: 220 RVA: 0x00008604 File Offset: 0x00006804
		// (set) Token: 0x060000DD RID: 221 RVA: 0x0000861C File Offset: 0x0000681C
		public int IncomingLossPercentage
		{
			get
			{
				return this.incomingLossPercentage;
			}
			set
			{
				this.incomingLossPercentage = value;
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x060000DE RID: 222 RVA: 0x00008626 File Offset: 0x00006826
		// (set) Token: 0x060000DF RID: 223 RVA: 0x0000862E File Offset: 0x0000682E
		public int LostPackagesOut
		{
			[CompilerGenerated]
			get
			{
				return this.<LostPackagesOut>k__BackingField;
			}
			[CompilerGenerated]
			internal set
			{
				this.<LostPackagesOut>k__BackingField = value;
			}
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x060000E0 RID: 224 RVA: 0x00008637 File Offset: 0x00006837
		// (set) Token: 0x060000E1 RID: 225 RVA: 0x0000863F File Offset: 0x0000683F
		public int LostPackagesIn
		{
			[CompilerGenerated]
			get
			{
				return this.<LostPackagesIn>k__BackingField;
			}
			[CompilerGenerated]
			internal set
			{
				this.<LostPackagesIn>k__BackingField = value;
			}
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x00008648 File Offset: 0x00006848
		public override string ToString()
		{
			return string.Format("NetworkSimulationSet {6}.  Lag in={0} out={1}. Jitter in={2} out={3}. Loss in={4} out={5}.", new object[]
			{
				this.incomingLag,
				this.outgoingLag,
				this.incomingJitter,
				this.outgoingJitter,
				this.incomingLossPercentage,
				this.outgoingLossPercentage,
				this.IsSimulationEnabled
			});
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x000086CC File Offset: 0x000068CC
		public NetworkSimulationSet()
		{
		}

		// Token: 0x040000B3 RID: 179
		private bool isSimulationEnabled = false;

		// Token: 0x040000B4 RID: 180
		private int outgoingLag = 100;

		// Token: 0x040000B5 RID: 181
		private int outgoingJitter = 0;

		// Token: 0x040000B6 RID: 182
		private int outgoingLossPercentage = 1;

		// Token: 0x040000B7 RID: 183
		private int incomingLag = 100;

		// Token: 0x040000B8 RID: 184
		private int incomingJitter = 0;

		// Token: 0x040000B9 RID: 185
		private int incomingLossPercentage = 1;

		// Token: 0x040000BA RID: 186
		internal PeerBase peerBase;

		// Token: 0x040000BB RID: 187
		private Thread netSimThread;

		// Token: 0x040000BC RID: 188
		protected internal readonly ManualResetEvent NetSimManualResetEvent = new ManualResetEvent(false);

		// Token: 0x040000BD RID: 189
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private int <LostPackagesOut>k__BackingField;

		// Token: 0x040000BE RID: 190
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private int <LostPackagesIn>k__BackingField;
	}
}

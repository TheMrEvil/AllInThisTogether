using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace ExitGames.Client.Photon
{
	// Token: 0x02000015 RID: 21
	internal class SimulationItem
	{
		// Token: 0x060000CD RID: 205 RVA: 0x00008237 File Offset: 0x00006437
		public SimulationItem()
		{
			this.stopw = new Stopwatch();
			this.stopw.Start();
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x060000CE RID: 206 RVA: 0x00008258 File Offset: 0x00006458
		// (set) Token: 0x060000CF RID: 207 RVA: 0x00008260 File Offset: 0x00006460
		public int Delay
		{
			[CompilerGenerated]
			get
			{
				return this.<Delay>k__BackingField;
			}
			[CompilerGenerated]
			internal set
			{
				this.<Delay>k__BackingField = value;
			}
		}

		// Token: 0x040000AF RID: 175
		internal readonly Stopwatch stopw;

		// Token: 0x040000B0 RID: 176
		public int TimeToExecute;

		// Token: 0x040000B1 RID: 177
		public byte[] DelayedData;

		// Token: 0x040000B2 RID: 178
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private int <Delay>k__BackingField;
	}
}

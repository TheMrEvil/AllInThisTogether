using System;

namespace System.Net.NetworkInformation
{
	// Token: 0x02000753 RID: 1875
	internal struct Win32_MIB_UDPSTATS
	{
		// Token: 0x0400235C RID: 9052
		public uint InDatagrams;

		// Token: 0x0400235D RID: 9053
		public uint NoPorts;

		// Token: 0x0400235E RID: 9054
		public uint InErrors;

		// Token: 0x0400235F RID: 9055
		public uint OutDatagrams;

		// Token: 0x04002360 RID: 9056
		public int NumAddrs;
	}
}

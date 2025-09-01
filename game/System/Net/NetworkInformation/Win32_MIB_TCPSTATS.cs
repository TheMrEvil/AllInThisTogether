using System;

namespace System.Net.NetworkInformation
{
	// Token: 0x02000751 RID: 1873
	internal struct Win32_MIB_TCPSTATS
	{
		// Token: 0x0400234C RID: 9036
		public uint RtoAlgorithm;

		// Token: 0x0400234D RID: 9037
		public uint RtoMin;

		// Token: 0x0400234E RID: 9038
		public uint RtoMax;

		// Token: 0x0400234F RID: 9039
		public uint MaxConn;

		// Token: 0x04002350 RID: 9040
		public uint ActiveOpens;

		// Token: 0x04002351 RID: 9041
		public uint PassiveOpens;

		// Token: 0x04002352 RID: 9042
		public uint AttemptFails;

		// Token: 0x04002353 RID: 9043
		public uint EstabResets;

		// Token: 0x04002354 RID: 9044
		public uint CurrEstab;

		// Token: 0x04002355 RID: 9045
		public uint InSegs;

		// Token: 0x04002356 RID: 9046
		public uint OutSegs;

		// Token: 0x04002357 RID: 9047
		public uint RetransSegs;

		// Token: 0x04002358 RID: 9048
		public uint InErrs;

		// Token: 0x04002359 RID: 9049
		public uint OutRsts;

		// Token: 0x0400235A RID: 9050
		public uint NumConns;
	}
}

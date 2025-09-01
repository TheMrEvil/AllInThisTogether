using System;

namespace System.Net.NetworkInformation
{
	// Token: 0x0200072C RID: 1836
	internal struct Win32_MIB_IPSTATS
	{
		// Token: 0x0400225C RID: 8796
		public int Forwarding;

		// Token: 0x0400225D RID: 8797
		public int DefaultTTL;

		// Token: 0x0400225E RID: 8798
		public uint InReceives;

		// Token: 0x0400225F RID: 8799
		public uint InHdrErrors;

		// Token: 0x04002260 RID: 8800
		public uint InAddrErrors;

		// Token: 0x04002261 RID: 8801
		public uint ForwDatagrams;

		// Token: 0x04002262 RID: 8802
		public uint InUnknownProtos;

		// Token: 0x04002263 RID: 8803
		public uint InDiscards;

		// Token: 0x04002264 RID: 8804
		public uint InDelivers;

		// Token: 0x04002265 RID: 8805
		public uint OutRequests;

		// Token: 0x04002266 RID: 8806
		public uint RoutingDiscards;

		// Token: 0x04002267 RID: 8807
		public uint OutDiscards;

		// Token: 0x04002268 RID: 8808
		public uint OutNoRoutes;

		// Token: 0x04002269 RID: 8809
		public uint ReasmTimeout;

		// Token: 0x0400226A RID: 8810
		public uint ReasmReqds;

		// Token: 0x0400226B RID: 8811
		public uint ReasmOks;

		// Token: 0x0400226C RID: 8812
		public uint ReasmFails;

		// Token: 0x0400226D RID: 8813
		public uint FragOks;

		// Token: 0x0400226E RID: 8814
		public uint FragFails;

		// Token: 0x0400226F RID: 8815
		public uint FragCreates;

		// Token: 0x04002270 RID: 8816
		public int NumIf;

		// Token: 0x04002271 RID: 8817
		public int NumAddr;

		// Token: 0x04002272 RID: 8818
		public int NumRoutes;
	}
}

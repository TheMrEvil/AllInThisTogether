using System;

namespace System.Net.NetworkInformation
{
	// Token: 0x02000734 RID: 1844
	internal struct Win32_MIBICMPSTATS
	{
		// Token: 0x04002283 RID: 8835
		public uint Msgs;

		// Token: 0x04002284 RID: 8836
		public uint Errors;

		// Token: 0x04002285 RID: 8837
		public uint DestUnreachs;

		// Token: 0x04002286 RID: 8838
		public uint TimeExcds;

		// Token: 0x04002287 RID: 8839
		public uint ParmProbs;

		// Token: 0x04002288 RID: 8840
		public uint SrcQuenchs;

		// Token: 0x04002289 RID: 8841
		public uint Redirects;

		// Token: 0x0400228A RID: 8842
		public uint Echos;

		// Token: 0x0400228B RID: 8843
		public uint EchoReps;

		// Token: 0x0400228C RID: 8844
		public uint Timestamps;

		// Token: 0x0400228D RID: 8845
		public uint TimestampReps;

		// Token: 0x0400228E RID: 8846
		public uint AddrMasks;

		// Token: 0x0400228F RID: 8847
		public uint AddrMaskReps;
	}
}

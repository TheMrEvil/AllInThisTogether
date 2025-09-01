using System;
using System.Runtime.InteropServices;

namespace System.Net.NetworkInformation
{
	// Token: 0x02000745 RID: 1861
	internal struct Win32_MIB_IFROW
	{
		// Token: 0x0400230A RID: 8970
		private const int MAX_INTERFACE_NAME_LEN = 256;

		// Token: 0x0400230B RID: 8971
		private const int MAXLEN_PHYSADDR = 8;

		// Token: 0x0400230C RID: 8972
		private const int MAXLEN_IFDESCR = 256;

		// Token: 0x0400230D RID: 8973
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 512)]
		public char[] Name;

		// Token: 0x0400230E RID: 8974
		public int Index;

		// Token: 0x0400230F RID: 8975
		public NetworkInterfaceType Type;

		// Token: 0x04002310 RID: 8976
		public int Mtu;

		// Token: 0x04002311 RID: 8977
		public uint Speed;

		// Token: 0x04002312 RID: 8978
		public int PhysAddrLen;

		// Token: 0x04002313 RID: 8979
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
		public byte[] PhysAddr;

		// Token: 0x04002314 RID: 8980
		public uint AdminStatus;

		// Token: 0x04002315 RID: 8981
		public uint OperStatus;

		// Token: 0x04002316 RID: 8982
		public uint LastChange;

		// Token: 0x04002317 RID: 8983
		public int InOctets;

		// Token: 0x04002318 RID: 8984
		public int InUcastPkts;

		// Token: 0x04002319 RID: 8985
		public int InNUcastPkts;

		// Token: 0x0400231A RID: 8986
		public int InDiscards;

		// Token: 0x0400231B RID: 8987
		public int InErrors;

		// Token: 0x0400231C RID: 8988
		public int InUnknownProtos;

		// Token: 0x0400231D RID: 8989
		public int OutOctets;

		// Token: 0x0400231E RID: 8990
		public int OutUcastPkts;

		// Token: 0x0400231F RID: 8991
		public int OutNUcastPkts;

		// Token: 0x04002320 RID: 8992
		public int OutDiscards;

		// Token: 0x04002321 RID: 8993
		public int OutErrors;

		// Token: 0x04002322 RID: 8994
		public int OutQLen;

		// Token: 0x04002323 RID: 8995
		public int DescrLen;

		// Token: 0x04002324 RID: 8996
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 256)]
		public byte[] Descr;
	}
}

using System;
using System.Runtime.InteropServices;

namespace System.Net.NetworkInformation
{
	// Token: 0x02000738 RID: 1848
	internal struct Win32_MIBICMPSTATS_EX
	{
		// Token: 0x040022A3 RID: 8867
		public uint Msgs;

		// Token: 0x040022A4 RID: 8868
		public uint Errors;

		// Token: 0x040022A5 RID: 8869
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 256)]
		public uint[] Counts;
	}
}

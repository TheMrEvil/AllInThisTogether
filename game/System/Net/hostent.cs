using System;
using System.Runtime.InteropServices;

namespace System.Net
{
	// Token: 0x020005FB RID: 1531
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
	internal struct hostent
	{
		// Token: 0x04001C1F RID: 7199
		public IntPtr h_name;

		// Token: 0x04001C20 RID: 7200
		public IntPtr h_aliases;

		// Token: 0x04001C21 RID: 7201
		public short h_addrtype;

		// Token: 0x04001C22 RID: 7202
		public short h_length;

		// Token: 0x04001C23 RID: 7203
		public IntPtr h_addr_list;
	}
}

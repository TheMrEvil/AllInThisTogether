using System;
using System.Runtime.InteropServices;

namespace Steamworks.Data
{
	// Token: 0x020001AC RID: 428
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct SteamParamStringArray_t
	{
		// Token: 0x04000B5E RID: 2910
		internal IntPtr Strings;

		// Token: 0x04000B5F RID: 2911
		internal int NumStrings;
	}
}

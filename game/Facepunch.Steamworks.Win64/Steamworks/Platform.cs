using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020000BA RID: 186
	internal static class Platform
	{
		// Token: 0x04000773 RID: 1907
		public const int StructPlatformPackSize = 8;

		// Token: 0x04000774 RID: 1908
		public const string LibraryName = "steam_api64";

		// Token: 0x04000775 RID: 1909
		public const CallingConvention CC = CallingConvention.Cdecl;

		// Token: 0x04000776 RID: 1910
		public const int StructPackSize = 4;
	}
}

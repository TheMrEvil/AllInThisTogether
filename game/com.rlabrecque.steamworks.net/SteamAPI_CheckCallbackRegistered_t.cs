using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000195 RID: 405
	// (Invoke) Token: 0x06000958 RID: 2392
	[UnmanagedFunctionPointer(CallingConvention.StdCall)]
	public delegate void SteamAPI_CheckCallbackRegistered_t(int iCallbackNum);
}

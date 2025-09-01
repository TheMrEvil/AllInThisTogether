using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020001A3 RID: 419
	// (Invoke) Token: 0x060009FD RID: 2557
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	public delegate void SteamInputActionEventCallbackPointer(IntPtr SteamInputActionEvent);
}

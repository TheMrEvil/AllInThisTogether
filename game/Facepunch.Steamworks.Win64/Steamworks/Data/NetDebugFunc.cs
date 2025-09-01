using System;
using System.Runtime.InteropServices;

namespace Steamworks.Data
{
	// Token: 0x020001EA RID: 490
	// (Invoke) Token: 0x06000FA2 RID: 4002
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void NetDebugFunc([In] NetDebugOutput nType, [In] IntPtr pszMsg);
}

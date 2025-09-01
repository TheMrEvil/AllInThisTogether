using System;
using System.Runtime.InteropServices;

namespace Mono.Btls
{
	// Token: 0x020000E3 RID: 227
	// (Invoke) Token: 0x060004D0 RID: 1232
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate int MonoBtlsSelectCallback(string[] acceptableIssuers);
}

using System;
using System.Runtime.InteropServices;

namespace Mono.Btls
{
	// Token: 0x020000E2 RID: 226
	// (Invoke) Token: 0x060004CC RID: 1228
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate int MonoBtlsVerifyCallback(MonoBtlsX509StoreCtx ctx);
}

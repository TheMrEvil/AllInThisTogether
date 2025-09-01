using System;
using System.Runtime.InteropServices;

namespace Mono.Btls
{
	// Token: 0x020000E4 RID: 228
	// (Invoke) Token: 0x060004D4 RID: 1236
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate int MonoBtlsServerNameCallback();
}

using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine.Diagnostics
{
	// Token: 0x0200044F RID: 1103
	[NativeHeader("Runtime/Export/Diagnostics/DiagnosticsUtils.bindings.h")]
	public static class Utils
	{
		// Token: 0x060026F2 RID: 9970
		[FreeFunction("DiagnosticsUtils_Bindings::ForceCrash", ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void ForceCrash(ForcedCrashCategory crashCategory);

		// Token: 0x060026F3 RID: 9971
		[FreeFunction("DiagnosticsUtils_Bindings::NativeAssert")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void NativeAssert(string message);

		// Token: 0x060026F4 RID: 9972
		[FreeFunction("DiagnosticsUtils_Bindings::NativeError")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void NativeError(string message);

		// Token: 0x060026F5 RID: 9973
		[FreeFunction("DiagnosticsUtils_Bindings::NativeWarning")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void NativeWarning(string message);
	}
}

using System;
using System.Runtime.InteropServices;

namespace UnityEngine.Yoga
{
	// Token: 0x02000011 RID: 17
	// (Invoke) Token: 0x06000025 RID: 37
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void YogaLogger(IntPtr unmanagedConfigPtr, IntPtr unmanagedNotePtr, YogaLogLevel level, string message);
}

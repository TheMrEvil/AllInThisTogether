using System;
using System.Runtime.InteropServices;

namespace UnityEngine.Yoga
{
	// Token: 0x02000007 RID: 7
	// (Invoke) Token: 0x06000010 RID: 16
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate float YogaBaselineFunc(IntPtr unmanagedNodePtr, float width, float height);
}

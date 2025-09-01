using System;
using System.Runtime.InteropServices;

namespace UnityEngine.Yoga
{
	// Token: 0x02000013 RID: 19
	// (Invoke) Token: 0x06000029 RID: 41
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate YogaSize YogaMeasureFunc(IntPtr unmanagedNodePtr, float width, YogaMeasureMode widthMode, float height, YogaMeasureMode heightMode);
}

using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting.APIUpdating;

namespace UnityEngine.U2D
{
	// Token: 0x02000271 RID: 625
	[MovedFrom("UnityEngine.Experimental.U2D")]
	[NativeHeader("Runtime/2D/Common/PixelSnapping.h")]
	public static class PixelPerfectRendering
	{
		// Token: 0x17000576 RID: 1398
		// (get) Token: 0x06001B22 RID: 6946
		// (set) Token: 0x06001B23 RID: 6947
		public static extern float pixelSnapSpacing { [FreeFunction("GetPixelSnapSpacing")] [MethodImpl(MethodImplOptions.InternalCall)] get; [FreeFunction("SetPixelSnapSpacing")] [MethodImpl(MethodImplOptions.InternalCall)] set; }
	}
}

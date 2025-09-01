using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting.APIUpdating;

namespace UnityEngine.Windows.WebCam
{
	// Token: 0x020002B2 RID: 690
	[MovedFrom("UnityEngine.XR.WSA.WebCam")]
	[NativeHeader("PlatformDependent/Win/Webcam/WebCam.h")]
	[StaticAccessor("WebCam::GetInstance()", StaticAccessorType.Dot)]
	public class WebCam
	{
		// Token: 0x170005B8 RID: 1464
		// (get) Token: 0x06001D0C RID: 7436
		public static extern WebCamMode Mode { [NativeConditional("(PLATFORM_WIN || PLATFORM_WINRT) && !PLATFORM_XBOXONE")] [NativeName("GetWebCamMode")] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x06001D0D RID: 7437 RVA: 0x00002072 File Offset: 0x00000272
		public WebCam()
		{
		}
	}
}

using System;
using UnityEngine.Bindings;

namespace UnityEngine.XR
{
	// Token: 0x02000023 RID: 35
	[NativeHeader("Modules/XR/XRPrefix.h")]
	[NativeType(Header = "Modules/XR/Subsystems/Display/XRDisplaySubsystemDescriptor.h")]
	public struct XRMirrorViewBlitModeDesc
	{
		// Token: 0x040000DD RID: 221
		public int blitMode;

		// Token: 0x040000DE RID: 222
		public string blitModeDesc;
	}
}

using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine.Experimental.GlobalIllumination
{
	// Token: 0x02000468 RID: 1128
	[StaticAccessor("GetRenderSettings()", StaticAccessorType.Dot)]
	[NativeHeader("Runtime/Camera/RenderSettings.h")]
	public class RenderSettings
	{
		// Token: 0x170007C2 RID: 1986
		// (get) Token: 0x060027F2 RID: 10226
		// (set) Token: 0x060027F3 RID: 10227
		public static extern bool useRadianceAmbientProbe { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x060027F4 RID: 10228 RVA: 0x00002072 File Offset: 0x00000272
		public RenderSettings()
		{
		}
	}
}

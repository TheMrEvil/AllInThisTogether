using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine.Experimental.Rendering
{
	// Token: 0x02000477 RID: 1143
	public static class GraphicsDeviceSettings
	{
		// Token: 0x170007C8 RID: 1992
		// (get) Token: 0x06002841 RID: 10305
		// (set) Token: 0x06002842 RID: 10306
		[StaticAccessor("GetGfxDevice()", StaticAccessorType.Dot)]
		public static extern WaitForPresentSyncPoint waitForPresentSyncPoint { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170007C9 RID: 1993
		// (get) Token: 0x06002843 RID: 10307
		// (set) Token: 0x06002844 RID: 10308
		[StaticAccessor("GetGfxDevice()", StaticAccessorType.Dot)]
		public static extern GraphicsJobsSyncPoint graphicsJobsSyncPoint { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }
	}
}

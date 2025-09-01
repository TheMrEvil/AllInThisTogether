using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x0200012F RID: 303
	[StaticAccessor("ScalableBufferManager::GetInstance()", StaticAccessorType.Dot)]
	[NativeHeader("Runtime/GfxDevice/ScalableBufferManager.h")]
	public static class ScalableBufferManager
	{
		// Token: 0x170001F2 RID: 498
		// (get) Token: 0x0600098C RID: 2444
		public static extern float widthScaleFactor { [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x170001F3 RID: 499
		// (get) Token: 0x0600098D RID: 2445
		public static extern float heightScaleFactor { [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x0600098E RID: 2446
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void ResizeBuffers(float widthScale, float heightScale);
	}
}

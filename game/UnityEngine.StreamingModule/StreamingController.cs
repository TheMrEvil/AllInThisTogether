using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x02000002 RID: 2
	[NativeHeader("Modules/Streaming/StreamingController.h")]
	[RequireComponent(typeof(Camera))]
	public class StreamingController : Behaviour
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000001 RID: 1
		// (set) Token: 0x06000002 RID: 2
		public extern float streamingMipmapBias { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x06000003 RID: 3
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetPreloading(float timeoutSeconds = 0f, bool activateCameraOnTimeout = false, Camera disableCameraCuttingFrom = null);

		// Token: 0x06000004 RID: 4
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void CancelPreloading();

		// Token: 0x06000005 RID: 5
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern bool IsPreloading();

		// Token: 0x06000006 RID: 6 RVA: 0x00002050 File Offset: 0x00000250
		public StreamingController()
		{
		}
	}
}

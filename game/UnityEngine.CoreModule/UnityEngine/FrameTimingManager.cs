using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x02000131 RID: 305
	[StaticAccessor("GetUncheckedRealGfxDevice().GetFrameTimingManager()", StaticAccessorType.Dot)]
	public static class FrameTimingManager
	{
		// Token: 0x0600098F RID: 2447
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void CaptureFrameTimings();

		// Token: 0x06000990 RID: 2448
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern uint GetLatestTimings(uint numFrames, [Unmarshalled] FrameTiming[] timings);

		// Token: 0x06000991 RID: 2449
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern float GetVSyncsPerSecond();

		// Token: 0x06000992 RID: 2450
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern ulong GetGpuTimerFrequency();

		// Token: 0x06000993 RID: 2451
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern ulong GetCpuTimerFrequency();
	}
}

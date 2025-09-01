using System;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x02000130 RID: 304
	[NativeHeader("Runtime/GfxDevice/FrameTiming.h")]
	public struct FrameTiming
	{
		// Token: 0x040003D2 RID: 978
		[NativeName("m_CPUTimePresentCalled")]
		public ulong cpuTimePresentCalled;

		// Token: 0x040003D3 RID: 979
		[NativeName("m_CPUFrameTime")]
		public double cpuFrameTime;

		// Token: 0x040003D4 RID: 980
		[NativeName("m_CPUTimeFrameComplete")]
		public ulong cpuTimeFrameComplete;

		// Token: 0x040003D5 RID: 981
		[NativeName("m_GPUFrameTime")]
		public double gpuFrameTime;

		// Token: 0x040003D6 RID: 982
		[NativeName("m_HeightScale")]
		public float heightScale;

		// Token: 0x040003D7 RID: 983
		[NativeName("m_WidthScale")]
		public float widthScale;

		// Token: 0x040003D8 RID: 984
		[NativeName("m_SyncInterval")]
		public uint syncInterval;
	}
}

using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000171 RID: 369
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct InputMotionDataV2_t
	{
		// Token: 0x040009C8 RID: 2504
		public float driftCorrectedQuatX;

		// Token: 0x040009C9 RID: 2505
		public float driftCorrectedQuatY;

		// Token: 0x040009CA RID: 2506
		public float driftCorrectedQuatZ;

		// Token: 0x040009CB RID: 2507
		public float driftCorrectedQuatW;

		// Token: 0x040009CC RID: 2508
		public float sensorFusionQuatX;

		// Token: 0x040009CD RID: 2509
		public float sensorFusionQuatY;

		// Token: 0x040009CE RID: 2510
		public float sensorFusionQuatZ;

		// Token: 0x040009CF RID: 2511
		public float sensorFusionQuatW;

		// Token: 0x040009D0 RID: 2512
		public float deferredSensorFusionQuatX;

		// Token: 0x040009D1 RID: 2513
		public float deferredSensorFusionQuatY;

		// Token: 0x040009D2 RID: 2514
		public float deferredSensorFusionQuatZ;

		// Token: 0x040009D3 RID: 2515
		public float deferredSensorFusionQuatW;

		// Token: 0x040009D4 RID: 2516
		public float gravityX;

		// Token: 0x040009D5 RID: 2517
		public float gravityY;

		// Token: 0x040009D6 RID: 2518
		public float gravityZ;

		// Token: 0x040009D7 RID: 2519
		public float degreesPerSecondX;

		// Token: 0x040009D8 RID: 2520
		public float degreesPerSecondY;

		// Token: 0x040009D9 RID: 2521
		public float degreesPerSecondZ;
	}
}

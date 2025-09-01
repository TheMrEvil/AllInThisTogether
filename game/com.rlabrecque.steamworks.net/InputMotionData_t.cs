using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000170 RID: 368
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct InputMotionData_t
	{
		// Token: 0x040009BE RID: 2494
		public float rotQuatX;

		// Token: 0x040009BF RID: 2495
		public float rotQuatY;

		// Token: 0x040009C0 RID: 2496
		public float rotQuatZ;

		// Token: 0x040009C1 RID: 2497
		public float rotQuatW;

		// Token: 0x040009C2 RID: 2498
		public float posAccelX;

		// Token: 0x040009C3 RID: 2499
		public float posAccelY;

		// Token: 0x040009C4 RID: 2500
		public float posAccelZ;

		// Token: 0x040009C5 RID: 2501
		public float rotVelX;

		// Token: 0x040009C6 RID: 2502
		public float rotVelY;

		// Token: 0x040009C7 RID: 2503
		public float rotVelZ;
	}
}

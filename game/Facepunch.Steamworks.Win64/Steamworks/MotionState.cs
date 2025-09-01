using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020000AC RID: 172
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	internal struct MotionState
	{
		// Token: 0x04000748 RID: 1864
		public float RotQuatX;

		// Token: 0x04000749 RID: 1865
		public float RotQuatY;

		// Token: 0x0400074A RID: 1866
		public float RotQuatZ;

		// Token: 0x0400074B RID: 1867
		public float RotQuatW;

		// Token: 0x0400074C RID: 1868
		public float PosAccelX;

		// Token: 0x0400074D RID: 1869
		public float PosAccelY;

		// Token: 0x0400074E RID: 1870
		public float PosAccelZ;

		// Token: 0x0400074F RID: 1871
		public float RotVelX;

		// Token: 0x04000750 RID: 1872
		public float RotVelY;

		// Token: 0x04000751 RID: 1873
		public float RotVelZ;
	}
}

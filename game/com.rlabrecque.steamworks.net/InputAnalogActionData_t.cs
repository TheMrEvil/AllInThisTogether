using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x0200016E RID: 366
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public struct InputAnalogActionData_t
	{
		// Token: 0x040009B8 RID: 2488
		public EInputSourceMode eMode;

		// Token: 0x040009B9 RID: 2489
		public float x;

		// Token: 0x040009BA RID: 2490
		public float y;

		// Token: 0x040009BB RID: 2491
		public byte bActive;
	}
}

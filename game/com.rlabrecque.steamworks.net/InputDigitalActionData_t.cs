using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x0200016F RID: 367
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public struct InputDigitalActionData_t
	{
		// Token: 0x040009BC RID: 2492
		public byte bState;

		// Token: 0x040009BD RID: 2493
		public byte bActive;
	}
}

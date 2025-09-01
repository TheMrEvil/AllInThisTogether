using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020000AB RID: 171
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public struct AnalogState
	{
		// Token: 0x17000087 RID: 135
		// (get) Token: 0x06000965 RID: 2405 RVA: 0x00010D81 File Offset: 0x0000EF81
		public bool Active
		{
			get
			{
				return this.BActive > 0;
			}
		}

		// Token: 0x04000744 RID: 1860
		public InputSourceMode EMode;

		// Token: 0x04000745 RID: 1861
		public float X;

		// Token: 0x04000746 RID: 1862
		public float Y;

		// Token: 0x04000747 RID: 1863
		internal byte BActive;
	}
}

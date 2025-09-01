using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020000AD RID: 173
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public struct DigitalState
	{
		// Token: 0x17000088 RID: 136
		// (get) Token: 0x06000966 RID: 2406 RVA: 0x00010D8C File Offset: 0x0000EF8C
		public bool Pressed
		{
			get
			{
				return this.BState > 0;
			}
		}

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x06000967 RID: 2407 RVA: 0x00010D97 File Offset: 0x0000EF97
		public bool Active
		{
			get
			{
				return this.BActive > 0;
			}
		}

		// Token: 0x04000752 RID: 1874
		[MarshalAs(UnmanagedType.I1)]
		internal byte BState;

		// Token: 0x04000753 RID: 1875
		[MarshalAs(UnmanagedType.I1)]
		internal byte BActive;
	}
}

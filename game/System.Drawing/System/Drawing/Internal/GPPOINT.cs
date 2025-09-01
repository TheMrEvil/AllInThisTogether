using System;
using System.Runtime.InteropServices;

namespace System.Drawing.Internal
{
	// Token: 0x020000F2 RID: 242
	[StructLayout(LayoutKind.Sequential)]
	internal class GPPOINT
	{
		// Token: 0x06000C00 RID: 3072 RVA: 0x00002050 File Offset: 0x00000250
		internal GPPOINT()
		{
		}

		// Token: 0x06000C01 RID: 3073 RVA: 0x0001BB05 File Offset: 0x00019D05
		internal GPPOINT(Point pt)
		{
			this.X = pt.X;
			this.Y = pt.Y;
		}

		// Token: 0x0400082E RID: 2094
		internal int X;

		// Token: 0x0400082F RID: 2095
		internal int Y;
	}
}

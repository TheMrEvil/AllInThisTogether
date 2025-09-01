using System;
using System.Runtime.InteropServices;

namespace System.Drawing.Internal
{
	// Token: 0x020000F3 RID: 243
	[StructLayout(LayoutKind.Sequential)]
	internal class GPPOINTF
	{
		// Token: 0x06000C02 RID: 3074 RVA: 0x00002050 File Offset: 0x00000250
		internal GPPOINTF()
		{
		}

		// Token: 0x06000C03 RID: 3075 RVA: 0x0001BB27 File Offset: 0x00019D27
		internal GPPOINTF(PointF pt)
		{
			this.X = pt.X;
			this.Y = pt.Y;
		}

		// Token: 0x06000C04 RID: 3076 RVA: 0x0001BB49 File Offset: 0x00019D49
		internal PointF ToPoint()
		{
			return new PointF(this.X, this.Y);
		}

		// Token: 0x04000830 RID: 2096
		internal float X;

		// Token: 0x04000831 RID: 2097
		internal float Y;
	}
}

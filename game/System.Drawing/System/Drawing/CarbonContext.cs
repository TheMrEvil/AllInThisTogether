using System;

namespace System.Drawing
{
	// Token: 0x020000AE RID: 174
	internal struct CarbonContext : IMacContext
	{
		// Token: 0x06000A5C RID: 2652 RVA: 0x00017A96 File Offset: 0x00015C96
		public CarbonContext(IntPtr port, IntPtr ctx, int width, int height)
		{
			this.port = port;
			this.ctx = ctx;
			this.width = width;
			this.height = height;
		}

		// Token: 0x06000A5D RID: 2653 RVA: 0x00017AB5 File Offset: 0x00015CB5
		public void Synchronize()
		{
			MacSupport.CGContextSynchronize(this.ctx);
		}

		// Token: 0x06000A5E RID: 2654 RVA: 0x00017AC2 File Offset: 0x00015CC2
		public void Release()
		{
			MacSupport.ReleaseContext(this.port, this.ctx);
		}

		// Token: 0x04000645 RID: 1605
		public IntPtr port;

		// Token: 0x04000646 RID: 1606
		public IntPtr ctx;

		// Token: 0x04000647 RID: 1607
		public int width;

		// Token: 0x04000648 RID: 1608
		public int height;
	}
}

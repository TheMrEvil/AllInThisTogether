using System;

namespace System.Drawing
{
	// Token: 0x020000AF RID: 175
	internal class CocoaContext : IMacContext
	{
		// Token: 0x06000A5F RID: 2655 RVA: 0x00017AD5 File Offset: 0x00015CD5
		public CocoaContext(IntPtr focusHandle, IntPtr ctx, int width, int height)
		{
			this.focusHandle = focusHandle;
			this.ctx = ctx;
			this.width = width;
			this.height = height;
		}

		// Token: 0x06000A60 RID: 2656 RVA: 0x00017AFA File Offset: 0x00015CFA
		public void Synchronize()
		{
			MacSupport.CGContextSynchronize(this.ctx);
		}

		// Token: 0x06000A61 RID: 2657 RVA: 0x00017B08 File Offset: 0x00015D08
		public void Release()
		{
			if (IntPtr.Zero != this.focusHandle)
			{
				MacSupport.CGContextFlush(this.ctx);
			}
			MacSupport.CGContextRestoreGState(this.ctx);
			if (IntPtr.Zero != this.focusHandle)
			{
				MacSupport.objc_msgSend(this.focusHandle, MacSupport.sel_registerName("unlockFocus"));
			}
		}

		// Token: 0x04000649 RID: 1609
		public IntPtr focusHandle;

		// Token: 0x0400064A RID: 1610
		public IntPtr ctx;

		// Token: 0x0400064B RID: 1611
		public int width;

		// Token: 0x0400064C RID: 1612
		public int height;
	}
}

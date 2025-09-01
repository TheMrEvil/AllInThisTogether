using System;
using System.Drawing.Imaging;
using System.Threading;

namespace System.Drawing
{
	// Token: 0x0200007F RID: 127
	internal class AnimateEventArgs : EventArgs
	{
		// Token: 0x06000621 RID: 1569 RVA: 0x00011D33 File Offset: 0x0000FF33
		public AnimateEventArgs(Image image)
		{
			this.frameCount = image.GetFrameCount(FrameDimension.Time);
		}

		// Token: 0x170001E6 RID: 486
		// (get) Token: 0x06000622 RID: 1570 RVA: 0x00011D4C File Offset: 0x0000FF4C
		// (set) Token: 0x06000623 RID: 1571 RVA: 0x00011D54 File Offset: 0x0000FF54
		public Thread RunThread
		{
			get
			{
				return this.thread;
			}
			set
			{
				this.thread = value;
			}
		}

		// Token: 0x06000624 RID: 1572 RVA: 0x00011D5D File Offset: 0x0000FF5D
		public int GetNextFrame()
		{
			if (this.activeFrame < this.frameCount - 1)
			{
				this.activeFrame++;
			}
			else
			{
				this.activeFrame = 0;
			}
			return this.activeFrame;
		}

		// Token: 0x040004CC RID: 1228
		private int frameCount;

		// Token: 0x040004CD RID: 1229
		private int activeFrame;

		// Token: 0x040004CE RID: 1230
		private Thread thread;
	}
}

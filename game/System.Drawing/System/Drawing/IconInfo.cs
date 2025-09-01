using System;

namespace System.Drawing
{
	// Token: 0x020000A3 RID: 163
	internal struct IconInfo
	{
		// Token: 0x170002CC RID: 716
		// (get) Token: 0x06000A2F RID: 2607 RVA: 0x000174DE File Offset: 0x000156DE
		// (set) Token: 0x06000A30 RID: 2608 RVA: 0x000174E9 File Offset: 0x000156E9
		public bool IsIcon
		{
			get
			{
				return this.fIcon == 1;
			}
			set
			{
				this.fIcon = (value ? 1 : 0);
			}
		}

		// Token: 0x0400061D RID: 1565
		private int fIcon;

		// Token: 0x0400061E RID: 1566
		public int xHotspot;

		// Token: 0x0400061F RID: 1567
		public int yHotspot;

		// Token: 0x04000620 RID: 1568
		public IntPtr hbmMask;

		// Token: 0x04000621 RID: 1569
		public IntPtr hbmColor;
	}
}

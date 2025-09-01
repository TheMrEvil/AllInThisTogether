using System;

namespace Mono
{
	// Token: 0x02000029 RID: 41
	internal struct CFRange
	{
		// Token: 0x06000055 RID: 85 RVA: 0x000026E5 File Offset: 0x000008E5
		public CFRange(int loc, int len)
		{
			this.Location = (IntPtr)loc;
			this.Length = (IntPtr)len;
		}

		// Token: 0x04000114 RID: 276
		public IntPtr Location;

		// Token: 0x04000115 RID: 277
		public IntPtr Length;
	}
}

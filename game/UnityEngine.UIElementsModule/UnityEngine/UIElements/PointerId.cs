using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000219 RID: 537
	public static class PointerId
	{
		// Token: 0x06001062 RID: 4194 RVA: 0x00042050 File Offset: 0x00040250
		// Note: this type is marked as 'beforefieldinit'.
		static PointerId()
		{
		}

		// Token: 0x0400074A RID: 1866
		public static readonly int maxPointers = 32;

		// Token: 0x0400074B RID: 1867
		public static readonly int invalidPointerId = -1;

		// Token: 0x0400074C RID: 1868
		public static readonly int mousePointerId = 0;

		// Token: 0x0400074D RID: 1869
		public static readonly int touchPointerIdBase = 1;

		// Token: 0x0400074E RID: 1870
		public static readonly int touchPointerCount = 20;

		// Token: 0x0400074F RID: 1871
		public static readonly int penPointerIdBase = PointerId.touchPointerIdBase + PointerId.touchPointerCount;

		// Token: 0x04000750 RID: 1872
		public static readonly int penPointerCount = 2;

		// Token: 0x04000751 RID: 1873
		internal static readonly int[] hoveringPointers = new int[]
		{
			PointerId.mousePointerId
		};
	}
}

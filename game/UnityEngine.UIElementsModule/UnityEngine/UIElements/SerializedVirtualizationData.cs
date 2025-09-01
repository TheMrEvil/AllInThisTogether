using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000123 RID: 291
	[Serializable]
	internal class SerializedVirtualizationData
	{
		// Token: 0x0600098E RID: 2446 RVA: 0x000020C2 File Offset: 0x000002C2
		public SerializedVirtualizationData()
		{
		}

		// Token: 0x04000414 RID: 1044
		public Vector2 scrollOffset;

		// Token: 0x04000415 RID: 1045
		public int firstVisibleIndex;

		// Token: 0x04000416 RID: 1046
		public float contentPadding;

		// Token: 0x04000417 RID: 1047
		public float contentHeight;

		// Token: 0x04000418 RID: 1048
		public int anchoredItemIndex;

		// Token: 0x04000419 RID: 1049
		public float anchorOffset;
	}
}

using System;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x020001AB RID: 427
	[UsedByNativeCode]
	[Serializable]
	public struct CustomRenderTextureUpdateZone
	{
		// Token: 0x040005C6 RID: 1478
		public Vector3 updateZoneCenter;

		// Token: 0x040005C7 RID: 1479
		public Vector3 updateZoneSize;

		// Token: 0x040005C8 RID: 1480
		public float rotation;

		// Token: 0x040005C9 RID: 1481
		public int passIndex;

		// Token: 0x040005CA RID: 1482
		public bool needSwap;
	}
}

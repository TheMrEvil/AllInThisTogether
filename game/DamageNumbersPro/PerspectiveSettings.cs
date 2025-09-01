using System;
using UnityEngine;

namespace DamageNumbersPro
{
	// Token: 0x0200000D RID: 13
	[Serializable]
	public struct PerspectiveSettings
	{
		// Token: 0x0600009A RID: 154 RVA: 0x00006732 File Offset: 0x00004932
		public PerspectiveSettings(float customDefault)
		{
			this.baseDistance = 10f;
			this.closeDistance = 5f;
			this.farDistance = 50f;
			this.closeScale = 2f;
			this.farScale = 0.5f;
		}

		// Token: 0x040000D4 RID: 212
		[Header("Distances:")]
		[Tooltip("The consistent size of the number is based on this distance.")]
		public float baseDistance;

		// Token: 0x040000D5 RID: 213
		[Tooltip("The closest distance the number will be scaling up to.")]
		public float closeDistance;

		// Token: 0x040000D6 RID: 214
		[Tooltip("The farthest distance the number will be scaling down to.")]
		public float farDistance;

		// Token: 0x040000D7 RID: 215
		[Header("Scales:")]
		[Tooltip("The max scale the number reaches at the closest distance.")]
		public float closeScale;

		// Token: 0x040000D8 RID: 216
		[Tooltip("The min scale the number reaches at the farthest distance.")]
		public float farScale;
	}
}

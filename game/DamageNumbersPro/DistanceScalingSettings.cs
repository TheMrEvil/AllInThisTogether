using System;
using UnityEngine;

namespace DamageNumbersPro
{
	// Token: 0x02000011 RID: 17
	[Serializable]
	public struct DistanceScalingSettings
	{
		// Token: 0x0600009E RID: 158 RVA: 0x000068E8 File Offset: 0x00004AE8
		public DistanceScalingSettings(float customDefault)
		{
			this.baseDistance = 15f;
			this.closeDistance = 5f;
			this.farDistance = 50f;
			this.closeScale = 2f;
			this.farScale = 0.5f;
		}

		// Token: 0x040000E4 RID: 228
		[Header("Distances:")]
		[Tooltip("The consistent size of the number is based on this distance.")]
		public float baseDistance;

		// Token: 0x040000E5 RID: 229
		[Tooltip("The closest distance the number will be scaling up to.")]
		public float closeDistance;

		// Token: 0x040000E6 RID: 230
		[Tooltip("The farthest distance the number will be scaling down to.")]
		public float farDistance;

		// Token: 0x040000E7 RID: 231
		[Header("Scales:")]
		[Tooltip("The max scale the number reaches at the closest distance.")]
		public float closeScale;

		// Token: 0x040000E8 RID: 232
		[Tooltip("The min scale the number reaches at the farthest distance.")]
		public float farScale;
	}
}

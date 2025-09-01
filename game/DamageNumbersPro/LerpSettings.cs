using System;
using UnityEngine;

namespace DamageNumbersPro
{
	// Token: 0x0200000B RID: 11
	[Serializable]
	public struct LerpSettings
	{
		// Token: 0x06000099 RID: 153 RVA: 0x000066F2 File Offset: 0x000048F2
		public LerpSettings(int customDefault)
		{
			this.minX = -0.4f;
			this.maxX = 0.4f;
			this.minY = 0.5f;
			this.maxY = 1f;
			this.speed = 5f;
			this.randomFlip = false;
		}

		// Token: 0x040000CA RID: 202
		[Header("Speed:")]
		[Tooltip("Speed at which it moves to the offset position.")]
		public float speed;

		// Token: 0x040000CB RID: 203
		[Header("Offset:")]
		[Tooltip("Minimum of horizontal offset.")]
		public float minX;

		// Token: 0x040000CC RID: 204
		[Tooltip("Maximum of horizontal offset.")]
		public float maxX;

		// Token: 0x040000CD RID: 205
		[Tooltip("Minimum of vertical offset.")]
		public float minY;

		// Token: 0x040000CE RID: 206
		[Tooltip("Maximum of vertical offset.")]
		public float maxY;

		// Token: 0x040000CF RID: 207
		[Header("Horizontal Flip:")]
		[Tooltip("Randomly flips the X Offset.\nUseful for avoiding small movements.\nSet Min X and Max X to a positive value.")]
		public bool randomFlip;
	}
}

using System;
using UnityEngine;

namespace DamageNumbersPro
{
	// Token: 0x02000017 RID: 23
	[Serializable]
	public struct VelocitySettings
	{
		// Token: 0x060000A4 RID: 164 RVA: 0x00006A3C File Offset: 0x00004C3C
		public VelocitySettings(float customDefault)
		{
			this.minX = -1f;
			this.maxX = 1f;
			this.minY = 4f;
			this.maxY = 5f;
			this.randomFlip = false;
			this.dragX = 0.1f;
			this.dragY = 1f;
			this.gravity = 3f;
		}

		// Token: 0x04000100 RID: 256
		[Header("Velocity:")]
		[Tooltip("Minimum of horizontal velocity.")]
		public float minX;

		// Token: 0x04000101 RID: 257
		[Tooltip("Maximum of horizontal velocity.")]
		public float maxX;

		// Token: 0x04000102 RID: 258
		[Tooltip("Minimum of vertical velocity.")]
		public float minY;

		// Token: 0x04000103 RID: 259
		[Tooltip("Maximum of vertical velocity.")]
		public float maxY;

		// Token: 0x04000104 RID: 260
		[Header("Horizontal Flip:")]
		[Tooltip("Randomly flips the X Velocity.\nUseful for avoiding small movements.\nSet Min X and Max X to a positive value.")]
		public bool randomFlip;

		// Token: 0x04000105 RID: 261
		[Header("Drag:")]
		[Tooltip("Reduces horizontal velocity over time.")]
		public float dragX;

		// Token: 0x04000106 RID: 262
		[Tooltip("Reduces vertical velocity over time.")]
		public float dragY;

		// Token: 0x04000107 RID: 263
		[Header("Gravity:")]
		[Tooltip("Increases vertical velocity downwards.")]
		public float gravity;
	}
}

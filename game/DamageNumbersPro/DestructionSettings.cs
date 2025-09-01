using System;
using UnityEngine;

namespace DamageNumbersPro
{
	// Token: 0x02000010 RID: 16
	[Serializable]
	public struct DestructionSettings
	{
		// Token: 0x0600009D RID: 157 RVA: 0x00006840 File Offset: 0x00004A40
		public DestructionSettings(float customDefault)
		{
			this.maxDistance = 2f;
			this.spawnDelay = 0.2f;
			this.duration = 0.3f;
			this.scaleCurve = new AnimationCurve(new Keyframe[]
			{
				new Keyframe(0f, 1f),
				new Keyframe(1f, 0.5f)
			});
			this.alphaCurve = new AnimationCurve(new Keyframe[]
			{
				new Keyframe(0f, 1f),
				new Keyframe(1f, 0f)
			});
		}

		// Token: 0x040000DF RID: 223
		[Header("Main:")]
		[Tooltip("The maximum distance at which damage numbers will be destroyed.")]
		public float maxDistance;

		// Token: 0x040000E0 RID: 224
		[Tooltip("The delay after spawning that numbers will be destroyed.")]
		public float spawnDelay;

		// Token: 0x040000E1 RID: 225
		[Header("Animation:")]
		public float duration;

		// Token: 0x040000E2 RID: 226
		[Tooltip("The scale over the destruction duration.")]
		public AnimationCurve scaleCurve;

		// Token: 0x040000E3 RID: 227
		[Tooltip("The alpha over the destruction duration.")]
		public AnimationCurve alphaCurve;
	}
}

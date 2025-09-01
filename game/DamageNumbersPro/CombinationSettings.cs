using System;
using UnityEngine;

namespace DamageNumbersPro
{
	// Token: 0x02000004 RID: 4
	[Serializable]
	public struct CombinationSettings
	{
		// Token: 0x06000004 RID: 4 RVA: 0x000020AC File Offset: 0x000002AC
		public CombinationSettings(float customDefault)
		{
			this.method = CombinationMethod.ABSORB_NEW;
			this.maxDistance = 10f;
			this.bonusLifetime = 1f;
			this.spawnDelay = 0.2f;
			this.absorbDuration = 0.4f;
			this.scaleCurve = new AnimationCurve(new Keyframe[]
			{
				new Keyframe(0f, 1f),
				new Keyframe(0.7f, 1f),
				new Keyframe(1f, 0f)
			});
			this.alphaCurve = new AnimationCurve(new Keyframe[]
			{
				new Keyframe(0f, 1f),
				new Keyframe(0.5f, 1f),
				new Keyframe(1f, 0f)
			});
			this.moveToAbsorber = true;
			this.teleportToAbsorber = false;
			this.instantGain = false;
			this.absorberScaleFactor = 1.5f;
			this.absorberScaleFade = 15f;
		}

		// Token: 0x04000009 RID: 9
		[Header("Main:")]
		[Tooltip("ABSORB_NEW: Oldest damage number absorbs newer damage numbers.\n\nREPLACE_OLD: New damage numbers absorb all existing damage numbers.\n\nIS_ALWAYS_ABSORBER: Will absorb all IS_ALWAYS_VICTIM damage numbers.\n\nIS_ALWAYS_VICTIM: Will be absorbed by the closest IS_ALWAYS_ABSORBER damage number.")]
		public CombinationMethod method;

		// Token: 0x0400000A RID: 10
		[Tooltip("The maximum distance at which numbers will combine.")]
		public float maxDistance;

		// Token: 0x0400000B RID: 11
		[Tooltip("The absorbtion delay after spawning.")]
		public float spawnDelay;

		// Token: 0x0400000C RID: 12
		[Header("Animation:")]
		[Tooltip("The length of the absorb animation.")]
		public float absorbDuration;

		// Token: 0x0400000D RID: 13
		[Tooltip("The scale over the absorb duration.")]
		public AnimationCurve scaleCurve;

		// Token: 0x0400000E RID: 14
		[Tooltip("The alpha over the absorb duration.")]
		public AnimationCurve alphaCurve;

		// Token: 0x0400000F RID: 15
		[Tooltip("If enabled the damage number will move towards it's absorber.")]
		public bool moveToAbsorber;

		// Token: 0x04000010 RID: 16
		[Tooltip("If enabled the damage number will teleport (spawn) inside it's absorber.")]
		public bool teleportToAbsorber;

		// Token: 0x04000011 RID: 17
		[Tooltip("How much the absorber is scaled up when it absorbs a damage number.")]
		public float absorberScaleFactor;

		// Token: 0x04000012 RID: 18
		[Tooltip("How quickly the absorber scales back to it's original size after being scaled up.")]
		public float absorberScaleFade;

		// Token: 0x04000013 RID: 19
		[Header("Other:")]
		[Tooltip("If true, the absorber will instantly gain the numbers of the target.  Should be used when combination is very fast.")]
		public bool instantGain;

		// Token: 0x04000014 RID: 20
		[Tooltip("The lifetime of the absorber is reset but also increased by this bonus lifetime.")]
		public float bonusLifetime;
	}
}

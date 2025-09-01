using System;
using Febucci.UI.Core;
using UnityEngine;
using UnityEngine.Scripting;

namespace Febucci.UI.Effects
{
	// Token: 0x02000012 RID: 18
	[Preserve]
	[CreateAssetMenu(fileName = "Bounce", menuName = "Text Animator/Animations/Behaviors/Bounce")]
	[EffectInfo("bounce", EffectCategory.Behaviors)]
	[DefaultValue("baseAmplitude", 13.19f)]
	[DefaultValue("baseFrequency", 1f)]
	[DefaultValue("baseWaveSize", 0.2f)]
	public sealed class BounceBehavior : BehaviorScriptableSine
	{
		// Token: 0x06000040 RID: 64 RVA: 0x00002EC5 File Offset: 0x000010C5
		private float BounceTween(float t)
		{
			if (t <= 0.2f)
			{
				return Tween.EaseInOut(t / 0.2f);
			}
			t -= 0.2f;
			if (t <= 0.6f)
			{
				return 1f - Tween.BounceOut(t / 0.6f);
			}
			return 0f;
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00002F08 File Offset: 0x00001108
		public override void ApplyEffectTo(ref CharacterData character, TAnimCore animator)
		{
			character.current.positions.MoveChar(Vector3.up * character.uniformIntensity * this.BounceTween(Mathf.Repeat(animator.time.timeSinceStart * this.frequency - this.waveSize * (float)character.index, 1f)) * this.amplitude);
		}

		// Token: 0x06000042 RID: 66 RVA: 0x00002F76 File Offset: 0x00001176
		public BounceBehavior()
		{
		}
	}
}

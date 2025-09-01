using System;
using Febucci.UI.Core;
using UnityEngine;
using UnityEngine.Scripting;

namespace Febucci.UI.Effects
{
	// Token: 0x0200001C RID: 28
	[Preserve]
	[CreateAssetMenu(menuName = "Text Animator/Animations/Behaviors/Wave", fileName = "Wave Behavior")]
	[EffectInfo("wave", EffectCategory.Behaviors)]
	[DefaultValue("baseAmplitude", 7.27f)]
	[DefaultValue("baseFrequency", 4f)]
	[DefaultValue("baseWaveSize", 0.4f)]
	public sealed class WaveBehavior : BehaviorScriptableSine
	{
		// Token: 0x06000065 RID: 101 RVA: 0x00003900 File Offset: 0x00001B00
		public override void ApplyEffectTo(ref CharacterData character, TAnimCore animator)
		{
			character.current.positions.MoveChar(Vector3.up * Mathf.Sin(animator.time.timeSinceStart * this.frequency + (float)character.index * this.waveSize) * this.amplitude * character.uniformIntensity);
		}

		// Token: 0x06000066 RID: 102 RVA: 0x00003963 File Offset: 0x00001B63
		public WaveBehavior()
		{
		}
	}
}

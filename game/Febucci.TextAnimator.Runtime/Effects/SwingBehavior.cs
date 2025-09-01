using System;
using Febucci.UI.Core;
using UnityEngine;
using UnityEngine.Scripting;

namespace Febucci.UI.Effects
{
	// Token: 0x0200001B RID: 27
	[Preserve]
	[CreateAssetMenu(menuName = "Text Animator/Animations/Behaviors/Swing", fileName = "Swing Behavior")]
	[EffectInfo("swing", EffectCategory.Behaviors)]
	[DefaultValue("baseAmplitude", 22.74f)]
	[DefaultValue("baseFrequency", 3.65f)]
	[DefaultValue("baseWaveSize", 0.171f)]
	public sealed class SwingBehavior : BehaviorScriptableSine
	{
		// Token: 0x06000063 RID: 99 RVA: 0x000038B9 File Offset: 0x00001AB9
		public override void ApplyEffectTo(ref CharacterData character, TAnimCore animator)
		{
			character.current.positions.RotateChar(Mathf.Cos(animator.time.timeSinceStart * this.frequency + (float)character.index * this.waveSize) * this.amplitude);
		}

		// Token: 0x06000064 RID: 100 RVA: 0x000038F8 File Offset: 0x00001AF8
		public SwingBehavior()
		{
		}
	}
}

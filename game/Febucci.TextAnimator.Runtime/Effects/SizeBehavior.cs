using System;
using Febucci.UI.Core;
using UnityEngine;
using UnityEngine.Scripting;

namespace Febucci.UI.Effects
{
	// Token: 0x02000019 RID: 25
	[Preserve]
	[CreateAssetMenu(menuName = "Text Animator/Animations/Behaviors/Size", fileName = "Size Behavior")]
	[EffectInfo("incr", EffectCategory.Behaviors)]
	public sealed class SizeBehavior : BehaviorScriptableBase
	{
		// Token: 0x0600005D RID: 93 RVA: 0x0000365E File Offset: 0x0000185E
		public override void ResetContext(TAnimCore animator)
		{
			this.amplitude = this.baseAmplitude * -1f + 1f;
			this.frequency = this.baseFrequency;
			this.waveSize = this.baseWaveSize;
		}

		// Token: 0x0600005E RID: 94 RVA: 0x00003690 File Offset: 0x00001890
		public override void SetModifier(ModifierInfo modifier)
		{
			string name = modifier.name;
			if (name == "a")
			{
				this.amplitude = this.baseAmplitude * modifier.value * -1f + 1f;
				return;
			}
			if (name == "f")
			{
				this.frequency = this.baseFrequency * modifier.value;
				return;
			}
			if (!(name == "w"))
			{
				return;
			}
			this.waveSize = this.baseWaveSize * modifier.value;
		}

		// Token: 0x0600005F RID: 95 RVA: 0x00003714 File Offset: 0x00001914
		public override void ApplyEffectTo(ref CharacterData character, TAnimCore animator)
		{
			character.current.positions.LerpUnclamped(character.current.positions.GetMiddlePos(), (Mathf.Cos(animator.time.timeSinceStart * this.frequency + (float)character.index * this.waveSize) + 1f) / 2f * this.amplitude);
		}

		// Token: 0x06000060 RID: 96 RVA: 0x0000377A File Offset: 0x0000197A
		public SizeBehavior()
		{
		}

		// Token: 0x04000056 RID: 86
		public float baseAmplitude = 1.5f;

		// Token: 0x04000057 RID: 87
		public float baseFrequency = 4f;

		// Token: 0x04000058 RID: 88
		public float baseWaveSize = 0.2f;

		// Token: 0x04000059 RID: 89
		private float amplitude;

		// Token: 0x0400005A RID: 90
		private float frequency;

		// Token: 0x0400005B RID: 91
		private float waveSize;
	}
}

using System;
using Febucci.UI.Core;
using UnityEngine;
using UnityEngine.Scripting;

namespace Febucci.UI.Effects
{
	// Token: 0x02000015 RID: 21
	[Preserve]
	[CreateAssetMenu(fileName = "Pendulum Behavior", menuName = "Text Animator/Animations/Behaviors/Pendulum")]
	[EffectInfo("pend", EffectCategory.Behaviors)]
	[DefaultValue("baseAmplitude", 24.7f)]
	[DefaultValue("baseFrequency", 3.1f)]
	[DefaultValue("baseWaveSize", 0.2f)]
	public sealed class PendulumBehavior : BehaviorScriptableSine
	{
		// Token: 0x0600004B RID: 75 RVA: 0x000031FE File Offset: 0x000013FE
		public override void ResetContext(TAnimCore animator)
		{
			base.ResetContext(animator);
			if (this.anchorBottom)
			{
				this.targetVertex1 = 0;
				this.targetVertex2 = 3;
				return;
			}
			this.targetVertex1 = 1;
			this.targetVertex2 = 2;
		}

		// Token: 0x0600004C RID: 76 RVA: 0x0000322C File Offset: 0x0000142C
		public override void ApplyEffectTo(ref CharacterData character, TAnimCore animator)
		{
			character.current.positions.RotateChar(Mathf.Sin(-animator.time.timeSinceStart * this.frequency + this.waveSize * (float)character.index) * this.amplitude, (character.current.positions[this.targetVertex1] + character.current.positions[this.targetVertex2]) / 2f);
		}

		// Token: 0x0600004D RID: 77 RVA: 0x000032B2 File Offset: 0x000014B2
		public PendulumBehavior()
		{
		}

		// Token: 0x04000042 RID: 66
		public bool anchorBottom;

		// Token: 0x04000043 RID: 67
		private int targetVertex1;

		// Token: 0x04000044 RID: 68
		private int targetVertex2;
	}
}

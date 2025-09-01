using System;
using Febucci.UI.Core;
using UnityEngine;
using UnityEngine.Scripting;

namespace Febucci.UI.Effects
{
	// Token: 0x02000013 RID: 19
	[Preserve]
	[CreateAssetMenu(menuName = "Text Animator/Animations/Behaviors/Dangle", fileName = "Dangle Behavior")]
	[EffectInfo("dangle", EffectCategory.Behaviors)]
	[DefaultValue("baseAmplitude", 7.87f)]
	[DefaultValue("baseFrequency", 3.37f)]
	[DefaultValue("baseWaveSize", 0.306f)]
	public sealed class DangleBehavior : BehaviorScriptableSine
	{
		// Token: 0x06000043 RID: 67 RVA: 0x00002F7E File Offset: 0x0000117E
		public override void ResetContext(TAnimCore animator)
		{
			base.ResetContext(animator);
			if (this.anchorBottom)
			{
				this.targetIndex1 = 1;
				this.targetIndex2 = 2;
				return;
			}
			this.targetIndex1 = 0;
			this.targetIndex2 = 3;
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00002FAC File Offset: 0x000011AC
		public override void ApplyEffectTo(ref CharacterData character, TAnimCore animator)
		{
			this.sin = Mathf.Sin(this.frequency * animator.time.timeSinceStart + (float)character.index * this.waveSize) * this.amplitude * character.uniformIntensity;
			character.current.positions[this.targetIndex1] += Vector3.right * this.sin;
			character.current.positions[this.targetIndex2] += Vector3.right * this.sin;
		}

		// Token: 0x06000045 RID: 69 RVA: 0x0000305F File Offset: 0x0000125F
		public DangleBehavior()
		{
		}

		// Token: 0x04000039 RID: 57
		public bool anchorBottom;

		// Token: 0x0400003A RID: 58
		private float sin;

		// Token: 0x0400003B RID: 59
		private int targetIndex1;

		// Token: 0x0400003C RID: 60
		private int targetIndex2;
	}
}

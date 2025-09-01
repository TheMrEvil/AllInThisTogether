using System;
using Febucci.UI.Core;
using UnityEngine;
using UnityEngine.Scripting;

namespace Febucci.UI.Effects
{
	// Token: 0x02000016 RID: 22
	[Preserve]
	[CreateAssetMenu(menuName = "Text Animator/Animations/Behaviors/Rainbow", fileName = "Rainbow Behavior")]
	[EffectInfo("rainb", EffectCategory.Behaviors)]
	public sealed class RainbowBehavior : BehaviorScriptableBase
	{
		// Token: 0x0600004E RID: 78 RVA: 0x000032BC File Offset: 0x000014BC
		public override void SetModifier(ModifierInfo modifier)
		{
			string name = modifier.name;
			if (name == "f")
			{
				this.frequency = this.baseFrequency * modifier.value;
				return;
			}
			if (!(name == "s"))
			{
				return;
			}
			this.waveSize = this.baseWaveSize * modifier.value;
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00003312 File Offset: 0x00001512
		public override void ResetContext(TAnimCore animator)
		{
			this.frequency = this.baseFrequency;
			this.waveSize = this.baseWaveSize;
		}

		// Token: 0x06000050 RID: 80 RVA: 0x0000332C File Offset: 0x0000152C
		public override void ApplyEffectTo(ref CharacterData character, TAnimCore animator)
		{
			for (byte b = 0; b < 4; b += 1)
			{
				this.temp = Color.HSVToRGB(Mathf.PingPong(animator.time.timeSinceStart * this.frequency + (float)character.index * this.waveSize, 1f), 1f, 1f);
				this.temp.a = character.current.colors[(int)b].a;
				character.current.colors[(int)b] = this.temp;
			}
		}

		// Token: 0x06000051 RID: 81 RVA: 0x000033C9 File Offset: 0x000015C9
		public RainbowBehavior()
		{
		}

		// Token: 0x04000045 RID: 69
		public float baseFrequency = 0.5f;

		// Token: 0x04000046 RID: 70
		public float baseWaveSize = 0.08f;

		// Token: 0x04000047 RID: 71
		private float frequency;

		// Token: 0x04000048 RID: 72
		private float waveSize;

		// Token: 0x04000049 RID: 73
		private Color32 temp;
	}
}

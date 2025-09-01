using System;
using Febucci.UI.Core;
using UnityEngine;
using UnityEngine.Scripting;

namespace Febucci.UI.Effects
{
	// Token: 0x02000018 RID: 24
	[Preserve]
	[CreateAssetMenu(menuName = "Text Animator/Animations/Behaviors/Shake", fileName = "Shake Behavior")]
	[EffectInfo("shake", EffectCategory.Behaviors)]
	[DefaultValue("baseAmplitude", 1.13f)]
	[DefaultValue("baseDelay", 0.1f)]
	[DefaultValue("baseWaveSize", 0.45f)]
	public sealed class ShakeBehavior : BehaviorScriptableBase
	{
		// Token: 0x06000056 RID: 86 RVA: 0x000034AA File Offset: 0x000016AA
		private void ClampValues()
		{
			this.delay = Mathf.Clamp(this.delay, 0.002f, 500f);
		}

		// Token: 0x06000057 RID: 87 RVA: 0x000034C7 File Offset: 0x000016C7
		protected override void OnInitialize()
		{
			base.OnInitialize();
			this.randIndex = UnityEngine.Random.Range(0, 25);
		}

		// Token: 0x06000058 RID: 88 RVA: 0x000034DD File Offset: 0x000016DD
		public override void ResetContext(TAnimCore animator)
		{
			this.amplitude = this.baseAmplitude;
			this.delay = this.baseDelay;
			this.waveSize = this.baseWaveSize;
			this.ClampValues();
		}

		// Token: 0x06000059 RID: 89 RVA: 0x0000350C File Offset: 0x0000170C
		public override void SetModifier(ModifierInfo modifier)
		{
			string name = modifier.name;
			if (!(name == "a"))
			{
				if (!(name == "d"))
				{
					if (name == "w")
					{
						this.waveSize = this.baseWaveSize * modifier.value;
					}
				}
				else
				{
					this.delay = this.baseDelay * modifier.value;
				}
			}
			else
			{
				this.amplitude = this.baseAmplitude * modifier.value;
			}
			this.ClampValues();
		}

		// Token: 0x0600005A RID: 90 RVA: 0x0000358C File Offset: 0x0000178C
		public override void ApplyEffectTo(ref CharacterData character, TAnimCore animator)
		{
			this.timePassed = animator.time.timeSinceStart;
			this.timePassed += (float)character.index * this.waveSize;
			this.randIndex = Mathf.RoundToInt(this.timePassed / this.delay) % 25;
			if (this.randIndex < 0)
			{
				this.randIndex *= -1;
			}
			character.current.positions.MoveChar(TextUtilities.fakeRandoms[this.randIndex] * this.amplitude * character.uniformIntensity);
		}

		// Token: 0x0600005B RID: 91 RVA: 0x0000362D File Offset: 0x0000182D
		private void OnValidate()
		{
			this.ClampValues();
		}

		// Token: 0x0600005C RID: 92 RVA: 0x00003635 File Offset: 0x00001835
		public ShakeBehavior()
		{
		}

		// Token: 0x0400004E RID: 78
		public float baseAmplitude = 0.085f;

		// Token: 0x0400004F RID: 79
		public float baseDelay = 0.04f;

		// Token: 0x04000050 RID: 80
		public float baseWaveSize = 0.2f;

		// Token: 0x04000051 RID: 81
		private float amplitude;

		// Token: 0x04000052 RID: 82
		private float delay;

		// Token: 0x04000053 RID: 83
		private float waveSize;

		// Token: 0x04000054 RID: 84
		private int randIndex;

		// Token: 0x04000055 RID: 85
		private float timePassed;
	}
}

using System;
using Febucci.UI.Core;
using UnityEngine;
using UnityEngine.Scripting;

namespace Febucci.UI.Effects
{
	// Token: 0x02000014 RID: 20
	[Preserve]
	[CreateAssetMenu(fileName = "Fade Behavior", menuName = "Text Animator/Animations/Behaviors/Fade")]
	[EffectInfo("fade", EffectCategory.Behaviors)]
	public sealed class FadeBehavior : BehaviorScriptableBase
	{
		// Token: 0x06000046 RID: 70 RVA: 0x00003067 File Offset: 0x00001267
		public override void ResetContext(TAnimCore animator)
		{
			this.delay = this.baseDelay;
			this.SetTimeToShow(this.baseSpeed);
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00003081 File Offset: 0x00001281
		private void SetTimeToShow(float speed)
		{
			this.timeToShow = 1f / speed;
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00003090 File Offset: 0x00001290
		public override void SetModifier(ModifierInfo modifier)
		{
			string name = modifier.name;
			if (name == "f")
			{
				this.SetTimeToShow(this.baseSpeed * modifier.value);
				return;
			}
			if (!(name == "d"))
			{
				return;
			}
			this.delay = this.baseDelay * modifier.value;
		}

		// Token: 0x06000049 RID: 73 RVA: 0x000030E8 File Offset: 0x000012E8
		public override void ApplyEffectTo(ref CharacterData character, TAnimCore animator)
		{
			if (character.passedTime <= this.delay)
			{
				return;
			}
			float num = (character.passedTime - this.delay) / this.timeToShow;
			if (num > 1f)
			{
				num = 1f;
			}
			if (num < 1f && num >= 0f)
			{
				for (int i = 0; i < 4; i++)
				{
					this.temp = character.current.colors[i];
					this.temp.a = 0;
					character.current.colors[i] = Color32.LerpUnclamped(character.current.colors[i], this.temp, Tween.EaseInOut(num));
				}
				return;
			}
			for (int j = 0; j < 4; j++)
			{
				this.temp = character.current.colors[j];
				this.temp.a = 0;
				character.current.colors[j] = this.temp;
			}
		}

		// Token: 0x0600004A RID: 74 RVA: 0x000031E0 File Offset: 0x000013E0
		public FadeBehavior()
		{
		}

		// Token: 0x0400003D RID: 61
		private Color32 temp;

		// Token: 0x0400003E RID: 62
		public float baseSpeed = 0.5f;

		// Token: 0x0400003F RID: 63
		public float baseDelay = 1f;

		// Token: 0x04000040 RID: 64
		private float delay;

		// Token: 0x04000041 RID: 65
		private float timeToShow;
	}
}

using System;
using Febucci.UI.Core;
using UnityEngine;
using UnityEngine.Scripting;

namespace Febucci.UI.Effects
{
	// Token: 0x0200000C RID: 12
	[Preserve]
	[CreateAssetMenu(fileName = "Offset Appearance", menuName = "Text Animator/Animations/Appearances/Offset")]
	[EffectInfo("offset", EffectCategory.Appearances)]
	public sealed class OffsetAppearance : AppearanceScriptableBase
	{
		// Token: 0x06000025 RID: 37 RVA: 0x000029EC File Offset: 0x00000BEC
		public override void ResetContext(TAnimCore animator)
		{
			base.ResetContext(animator);
			this.amount = this.baseAmount;
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00002A04 File Offset: 0x00000C04
		public override void ApplyEffectTo(ref CharacterData character, TAnimCore animator)
		{
			character.current.positions.MoveChar(this.baseDirection * this.amount * character.uniformIntensity * Tween.EaseIn(1f - character.passedTime / this.duration));
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00002A5F File Offset: 0x00000C5F
		public override void SetModifier(ModifierInfo modifier)
		{
			if (modifier.name == "a")
			{
				this.amount = this.baseAmount * modifier.value;
				return;
			}
			base.SetModifier(modifier);
		}

		// Token: 0x06000028 RID: 40 RVA: 0x00002A8E File Offset: 0x00000C8E
		public OffsetAppearance()
		{
		}

		// Token: 0x04000027 RID: 39
		public float baseAmount = 10f;

		// Token: 0x04000028 RID: 40
		private float amount;

		// Token: 0x04000029 RID: 41
		public Vector2 baseDirection = Vector2.one;
	}
}

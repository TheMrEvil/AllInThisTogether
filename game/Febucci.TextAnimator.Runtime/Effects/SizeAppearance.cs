using System;
using Febucci.UI.Core;
using UnityEngine;
using UnityEngine.Scripting;

namespace Febucci.UI.Effects
{
	// Token: 0x0200000F RID: 15
	[Preserve]
	[CreateAssetMenu(fileName = "Size Appearance", menuName = "Text Animator/Animations/Appearances/Size")]
	[EffectInfo("size", EffectCategory.Appearances)]
	public sealed class SizeAppearance : AppearanceScriptableBase
	{
		// Token: 0x06000032 RID: 50 RVA: 0x00002C61 File Offset: 0x00000E61
		public override void ResetContext(TAnimCore animator)
		{
			base.ResetContext(animator);
			this.amplitude = this.baseAmplitude * -1f + 1f;
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00002C84 File Offset: 0x00000E84
		public override void ApplyEffectTo(ref CharacterData character, TAnimCore animator)
		{
			character.current.positions.LerpUnclamped(character.current.positions.GetMiddlePos(), Tween.EaseIn(1f - character.passedTime / this.duration) * this.amplitude);
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00002CD0 File Offset: 0x00000ED0
		public override void SetModifier(ModifierInfo modifier)
		{
			if (modifier.name == "a")
			{
				this.amplitude = this.baseAmplitude * modifier.value;
				return;
			}
			base.SetModifier(modifier);
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00002CFF File Offset: 0x00000EFF
		public SizeAppearance()
		{
		}

		// Token: 0x0400002F RID: 47
		private float amplitude;

		// Token: 0x04000030 RID: 48
		public float baseAmplitude = 2f;
	}
}

using System;
using Febucci.UI.Core;
using UnityEngine;
using UnityEngine.Scripting;

namespace Febucci.UI.Effects
{
	// Token: 0x0200000E RID: 14
	[Preserve]
	[CreateAssetMenu(fileName = "Rotating Appearance", menuName = "Text Animator/Animations/Appearances/Rotating")]
	[EffectInfo("rot", EffectCategory.Appearances)]
	[DefaultValue("baseDuration", 0.7f)]
	public sealed class RotatingAppearance : AppearanceScriptableBase
	{
		// Token: 0x0600002E RID: 46 RVA: 0x00002BD6 File Offset: 0x00000DD6
		public override void ResetContext(TAnimCore animator)
		{
			base.ResetContext(animator);
			this.targetAngle = this.baseTargetAngle;
		}

		// Token: 0x0600002F RID: 47 RVA: 0x00002BEB File Offset: 0x00000DEB
		public override void ApplyEffectTo(ref CharacterData character, TAnimCore animator)
		{
			character.current.positions.RotateChar(Mathf.Lerp(this.targetAngle, 0f, Tween.EaseInOut(character.passedTime / this.duration)));
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00002C1F File Offset: 0x00000E1F
		public override void SetModifier(ModifierInfo modifier)
		{
			if (modifier.name == "a")
			{
				this.targetAngle = this.baseTargetAngle * modifier.value;
				return;
			}
			base.SetModifier(modifier);
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00002C4E File Offset: 0x00000E4E
		public RotatingAppearance()
		{
		}

		// Token: 0x0400002D RID: 45
		public float baseTargetAngle = 50f;

		// Token: 0x0400002E RID: 46
		private float targetAngle;
	}
}

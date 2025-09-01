using System;
using Febucci.UI.Core;
using UnityEngine;
using UnityEngine.Scripting;

namespace Febucci.UI.Effects
{
	// Token: 0x02000017 RID: 23
	[Preserve]
	[CreateAssetMenu(menuName = "Text Animator/Animations/Behaviors/Rotation", fileName = "Rotation Behavior")]
	[EffectInfo("rot", EffectCategory.Behaviors)]
	public sealed class RotationBehavior : BehaviorScriptableBase
	{
		// Token: 0x06000052 RID: 82 RVA: 0x000033E8 File Offset: 0x000015E8
		public override void SetModifier(ModifierInfo modifier)
		{
			string name = modifier.name;
			if (name == "f")
			{
				this.angleSpeed = this.baseRotSpeed * modifier.value;
				return;
			}
			if (!(name == "w"))
			{
				return;
			}
			this.angleDiffBetweenChars = this.baseDiffBetweenChars * modifier.value;
		}

		// Token: 0x06000053 RID: 83 RVA: 0x0000343E File Offset: 0x0000163E
		public override void ResetContext(TAnimCore animator)
		{
			this.angleSpeed = this.baseRotSpeed;
			this.angleDiffBetweenChars = this.baseDiffBetweenChars;
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00003458 File Offset: 0x00001658
		public override void ApplyEffectTo(ref CharacterData character, TAnimCore animator)
		{
			character.current.positions.RotateChar(-animator.time.timeSinceStart * this.angleSpeed + this.angleDiffBetweenChars * (float)character.index);
		}

		// Token: 0x06000055 RID: 85 RVA: 0x0000348C File Offset: 0x0000168C
		public RotationBehavior()
		{
		}

		// Token: 0x0400004A RID: 74
		public float baseRotSpeed = 180f;

		// Token: 0x0400004B RID: 75
		public float baseDiffBetweenChars = 10f;

		// Token: 0x0400004C RID: 76
		private float angleSpeed;

		// Token: 0x0400004D RID: 77
		private float angleDiffBetweenChars;
	}
}

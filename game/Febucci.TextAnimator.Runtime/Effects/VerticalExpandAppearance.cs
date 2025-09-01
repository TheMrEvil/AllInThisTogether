using System;
using Febucci.UI.Core;
using UnityEngine;
using UnityEngine.Scripting;

namespace Febucci.UI.Effects
{
	// Token: 0x02000010 RID: 16
	[Preserve]
	[CreateAssetMenu(fileName = "Vertical Expand Appearance", menuName = "Text Animator/Animations/Appearances/Vertical Expand")]
	[EffectInfo("vertexp", EffectCategory.Appearances)]
	public sealed class VerticalExpandAppearance : AppearanceScriptableBase
	{
		// Token: 0x06000036 RID: 54 RVA: 0x00002D12 File Offset: 0x00000F12
		public override void ResetContext(TAnimCore animator)
		{
			base.ResetContext(animator);
			this.SetOrientation(this.startsFromBottom);
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00002D27 File Offset: 0x00000F27
		private void SetOrientation(bool fromBottom)
		{
			if (fromBottom)
			{
				this.startA = 0;
				this.targetA = 1;
				this.startB = 3;
				this.targetB = 2;
				return;
			}
			this.startA = 1;
			this.targetA = 0;
			this.startB = 2;
			this.targetB = 3;
		}

		// Token: 0x06000038 RID: 56 RVA: 0x00002D68 File Offset: 0x00000F68
		public override void ApplyEffectTo(ref CharacterData character, TAnimCore animator)
		{
			this.pct = Tween.EaseInOut(character.passedTime / this.duration);
			character.current.positions[this.targetA] = Vector3.LerpUnclamped(character.current.positions[this.startA], character.current.positions[this.targetA], this.pct);
			character.current.positions[this.targetB] = Vector3.LerpUnclamped(character.current.positions[this.startB], character.current.positions[this.targetB], this.pct);
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00002E27 File Offset: 0x00001027
		public override void SetModifier(ModifierInfo modifier)
		{
			if (modifier.name == "bot")
			{
				this.SetOrientation((int)modifier.value == 1);
				return;
			}
			base.SetModifier(modifier);
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00002E53 File Offset: 0x00001053
		public VerticalExpandAppearance()
		{
		}

		// Token: 0x04000031 RID: 49
		public bool startsFromBottom = true;

		// Token: 0x04000032 RID: 50
		private int startA;

		// Token: 0x04000033 RID: 51
		private int targetA;

		// Token: 0x04000034 RID: 52
		private int startB;

		// Token: 0x04000035 RID: 53
		private int targetB;

		// Token: 0x04000036 RID: 54
		private float pct;
	}
}

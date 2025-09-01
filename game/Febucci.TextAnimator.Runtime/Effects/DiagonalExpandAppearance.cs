using System;
using Febucci.UI.Core;
using UnityEngine;
using UnityEngine.Scripting;

namespace Febucci.UI.Effects
{
	// Token: 0x02000009 RID: 9
	[Preserve]
	[CreateAssetMenu(fileName = "Diagonal Expand Appearance", menuName = "Text Animator/Animations/Appearances/Diagonal Expand")]
	[EffectInfo("diagexp", EffectCategory.Appearances)]
	public sealed class DiagonalExpandAppearance : AppearanceScriptableBase
	{
		// Token: 0x0600001A RID: 26 RVA: 0x000024B3 File Offset: 0x000006B3
		public override void ResetContext(TAnimCore animator)
		{
			base.ResetContext(animator);
			this.diagonalFromBttmLeft = true;
			this.UpdateOrientation();
		}

		// Token: 0x0600001B RID: 27 RVA: 0x000024C9 File Offset: 0x000006C9
		private void UpdateOrientation()
		{
			if (this.diagonalFromBttmLeft)
			{
				this.targetA = 0;
				this.targetB = 2;
				return;
			}
			this.targetA = 1;
			this.targetB = 3;
		}

		// Token: 0x0600001C RID: 28 RVA: 0x000024F0 File Offset: 0x000006F0
		public override void ApplyEffectTo(ref CharacterData character, TAnimCore animator)
		{
			this.middlePos = character.current.positions.GetMiddlePos();
			this.pct = Tween.EaseInOut(character.passedTime / this.duration);
			character.current.positions[this.targetA] = Vector3.LerpUnclamped(this.middlePos, character.current.positions[this.targetA], this.pct);
			character.current.positions[this.targetB] = Vector3.LerpUnclamped(this.middlePos, character.current.positions[this.targetB], this.pct);
		}

		// Token: 0x0600001D RID: 29 RVA: 0x000025A5 File Offset: 0x000007A5
		public override void SetModifier(ModifierInfo modifier)
		{
			if (modifier.name == "bot")
			{
				this.diagonalFromBttmLeft = ((int)modifier.value == 1);
				this.UpdateOrientation();
				return;
			}
			base.SetModifier(modifier);
		}

		// Token: 0x0600001E RID: 30 RVA: 0x000025D7 File Offset: 0x000007D7
		public DiagonalExpandAppearance()
		{
		}

		// Token: 0x0400001D RID: 29
		public bool diagonalFromBttmLeft;

		// Token: 0x0400001E RID: 30
		private int targetA;

		// Token: 0x0400001F RID: 31
		private int targetB;

		// Token: 0x04000020 RID: 32
		private Vector3 middlePos;

		// Token: 0x04000021 RID: 33
		private float pct;
	}
}

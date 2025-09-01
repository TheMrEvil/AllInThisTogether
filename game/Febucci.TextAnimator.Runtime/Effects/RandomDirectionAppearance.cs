using System;
using Febucci.UI.Core;
using UnityEngine;
using UnityEngine.Scripting;

namespace Febucci.UI.Effects
{
	// Token: 0x0200000D RID: 13
	[Preserve]
	[CreateAssetMenu(fileName = "RandomDir Appearance", menuName = "Text Animator/Animations/Appearances/Random Direction")]
	[EffectInfo("rdir", EffectCategory.Appearances)]
	public sealed class RandomDirectionAppearance : AppearanceScriptableBase
	{
		// Token: 0x06000029 RID: 41 RVA: 0x00002AAC File Offset: 0x00000CAC
		public override void ResetContext(TAnimCore animator)
		{
			base.ResetContext(animator);
			this.amount = this.baseAmount;
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00002AC4 File Offset: 0x00000CC4
		protected override void OnInitialize()
		{
			base.OnInitialize();
			this.directions = new Vector3[20];
			for (int i = 0; i < this.directions.Length; i++)
			{
				this.directions[i] = TextUtilities.fakeRandoms[UnityEngine.Random.Range(0, 24)] * Mathf.Sign(Mathf.Sin((float)i));
			}
		}

		// Token: 0x0600002B RID: 43 RVA: 0x00002B28 File Offset: 0x00000D28
		public override void ApplyEffectTo(ref CharacterData character, TAnimCore animator)
		{
			int num = character.index % this.directions.Length;
			character.current.positions.MoveChar(this.directions[num] * this.amount * character.uniformIntensity * Tween.EaseIn(1f - character.passedTime / this.duration));
		}

		// Token: 0x0600002C RID: 44 RVA: 0x00002B94 File Offset: 0x00000D94
		public override void SetModifier(ModifierInfo modifier)
		{
			if (modifier.name == "a")
			{
				this.amount = this.baseAmount * modifier.value;
				return;
			}
			base.SetModifier(modifier);
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00002BC3 File Offset: 0x00000DC3
		public RandomDirectionAppearance()
		{
		}

		// Token: 0x0400002A RID: 42
		public float baseAmount = 10f;

		// Token: 0x0400002B RID: 43
		private float amount;

		// Token: 0x0400002C RID: 44
		private Vector3[] directions;
	}
}

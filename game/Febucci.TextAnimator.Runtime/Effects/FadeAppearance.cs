using System;
using Febucci.UI.Core;
using UnityEngine;
using UnityEngine.Scripting;

namespace Febucci.UI.Effects
{
	// Token: 0x0200000A RID: 10
	[Preserve]
	[CreateAssetMenu(fileName = "Fade Appearance", menuName = "Text Animator/Animations/Appearances/Fade")]
	[EffectInfo("fade", EffectCategory.Appearances)]
	public sealed class FadeAppearance : AppearanceScriptableBase
	{
		// Token: 0x0600001F RID: 31 RVA: 0x000025E0 File Offset: 0x000007E0
		public override void ApplyEffectTo(ref CharacterData character, TAnimCore animator)
		{
			for (int i = 0; i < 4; i++)
			{
				this.temp = character.current.colors[i];
				this.temp.a = 0;
				character.current.colors[i] = Color32.LerpUnclamped(character.current.colors[i], this.temp, Tween.EaseInOut(1f - character.passedTime / this.duration));
			}
		}

		// Token: 0x06000020 RID: 32 RVA: 0x00002661 File Offset: 0x00000861
		public FadeAppearance()
		{
		}

		// Token: 0x04000022 RID: 34
		private Color32 temp;
	}
}

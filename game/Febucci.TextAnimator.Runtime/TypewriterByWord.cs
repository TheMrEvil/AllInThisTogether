using System;
using Febucci.Attributes;
using Febucci.UI.Core;
using UnityEngine;

namespace Febucci.UI
{
	// Token: 0x02000006 RID: 6
	[HelpURL("https://www.febucci.com/text-animator-unity/docs/typewriters/")]
	public class TypewriterByWord : TypewriterCore
	{
		// Token: 0x0600000C RID: 12 RVA: 0x00002267 File Offset: 0x00000467
		private bool IsCharInsideAnyWord(int charIndex)
		{
			return base.TextAnimator.Characters[charIndex].wordIndex >= 0;
		}

		// Token: 0x0600000D RID: 13 RVA: 0x00002288 File Offset: 0x00000488
		protected override float GetWaitAppearanceTimeOf(int charIndex)
		{
			if (this.IsCharInsideAnyWord(charIndex) || base.TextAnimator.latestCharacterShown.index <= 0)
			{
				return 0f;
			}
			int wordIndex = base.TextAnimator.Characters[base.TextAnimator.latestCharacterShown.index - 1].wordIndex;
			if (wordIndex < 0 || wordIndex >= base.TextAnimator.WordsCount)
			{
				return this.waitForNormalWord;
			}
			WordInfo wordInfo = base.TextAnimator.Words[wordIndex];
			if (!char.IsPunctuation(base.TextAnimator.Characters[wordInfo.lastCharacterIndex].info.character))
			{
				return this.waitForNormalWord;
			}
			return this.waitForWordWithPuntuaction;
		}

		// Token: 0x0600000E RID: 14 RVA: 0x00002344 File Offset: 0x00000544
		protected override float GetWaitDisappearanceTimeOf(int charIndex)
		{
			if (this.IsCharInsideAnyWord(charIndex))
			{
				return 0f;
			}
			return this.disappearanceDelay;
		}

		// Token: 0x0600000F RID: 15 RVA: 0x0000235B File Offset: 0x0000055B
		public TypewriterByWord()
		{
		}

		// Token: 0x04000013 RID: 19
		[SerializeField]
		[CharsDisplayTime]
		private float waitForNormalWord = 0.3f;

		// Token: 0x04000014 RID: 20
		[SerializeField]
		[CharsDisplayTime]
		private float waitForWordWithPuntuaction = 0.5f;

		// Token: 0x04000015 RID: 21
		[SerializeField]
		[CharsDisplayTime]
		private float disappearanceDelay = 0.5f;
	}
}

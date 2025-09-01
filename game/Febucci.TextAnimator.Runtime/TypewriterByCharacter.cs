using System;
using System.Runtime.CompilerServices;
using Febucci.Attributes;
using Febucci.UI.Core;
using UnityEngine;

namespace Febucci.UI
{
	// Token: 0x02000005 RID: 5
	[HelpURL("https://www.febucci.com/text-animator-unity/docs/typewriters/")]
	[AddComponentMenu("Febucci/TextAnimator/Typewriter - By Character")]
	public class TypewriterByCharacter : TypewriterCore
	{
		// Token: 0x06000008 RID: 8 RVA: 0x000020AC File Offset: 0x000002AC
		protected override float GetWaitAppearanceTimeOf(int charIndex)
		{
			char character = base.TextAnimator.Characters[charIndex].info.character;
			if (!this.waitForLastCharacter && base.TextAnimator.allLettersShown)
			{
				return 0f;
			}
			if (this.avoidMultiplePunctuactionWait && char.IsPunctuation(character) && charIndex < base.TextAnimator.CharactersCount - 1 && char.IsPunctuation(base.TextAnimator.Characters[charIndex + 1].info.character))
			{
				return this.waitForNormalChars;
			}
			if (!this.waitForNewLines && !base.TextAnimator.latestCharacterShown.info.isRendered && TypewriterByCharacter.<GetWaitAppearanceTimeOf>g__IsUnicodeNewLine|9_0(Convert.ToUInt64(base.TextAnimator.latestCharacterShown.info.character)))
			{
				return 0f;
			}
			if (character <= '.')
			{
				if (character == '!')
				{
					goto IL_10D;
				}
				switch (character)
				{
				case ')':
				case ',':
				case '-':
					break;
				case '*':
				case '+':
					goto IL_114;
				case '.':
					goto IL_10D;
				default:
					goto IL_114;
				}
			}
			else if (character != ':' && character != ';')
			{
				if (character != '?')
				{
					goto IL_114;
				}
				goto IL_10D;
			}
			return this.waitMiddle;
			IL_10D:
			return this.waitLong;
			IL_114:
			return this.waitForNormalChars;
		}

		// Token: 0x06000009 RID: 9 RVA: 0x000021D3 File Offset: 0x000003D3
		protected override float GetWaitDisappearanceTimeOf(int charIndex)
		{
			if (!this.useTypewriterWaitForDisappearances)
			{
				return this.disappearanceWaitTime;
			}
			return this.GetWaitAppearanceTimeOf(charIndex) * (1f / this.disappearanceSpeedMultiplier);
		}

		// Token: 0x0600000A RID: 10 RVA: 0x000021F8 File Offset: 0x000003F8
		public TypewriterByCharacter()
		{
		}

		// Token: 0x0600000B RID: 11 RVA: 0x00002257 File Offset: 0x00000457
		[CompilerGenerated]
		internal static bool <GetWaitAppearanceTimeOf>g__IsUnicodeNewLine|9_0(ulong unicode)
		{
			return unicode == 10UL || unicode == 13UL;
		}

		// Token: 0x0400000A RID: 10
		[SerializeField]
		[CharsDisplayTime]
		[Tooltip("Wait time for normal letters")]
		public float waitForNormalChars = 0.03f;

		// Token: 0x0400000B RID: 11
		[SerializeField]
		[CharsDisplayTime]
		[Tooltip("Wait time for ! ? .")]
		public float waitLong = 0.6f;

		// Token: 0x0400000C RID: 12
		[SerializeField]
		[CharsDisplayTime]
		[Tooltip("Wait time for ; : ) - ,")]
		public float waitMiddle = 0.2f;

		// Token: 0x0400000D RID: 13
		[SerializeField]
		[Tooltip("-True: only the last punctuaction on a sequence waits for its category time.\n-False: each punctuaction will wait, regardless if it's in a sequence or not")]
		public bool avoidMultiplePunctuactionWait;

		// Token: 0x0400000E RID: 14
		[SerializeField]
		[Tooltip("True if you want the typewriter to wait for new line characters")]
		public bool waitForNewLines = true;

		// Token: 0x0400000F RID: 15
		[SerializeField]
		[Tooltip("True if you want the typewriter to wait for all characters, false if you want to skip waiting for the last one")]
		public bool waitForLastCharacter = true;

		// Token: 0x04000010 RID: 16
		[SerializeField]
		[Tooltip("True if you want to use the same typewriter's wait times for the disappearance progression, false if you want to use a different wait time")]
		public bool useTypewriterWaitForDisappearances = true;

		// Token: 0x04000011 RID: 17
		[SerializeField]
		[CharsDisplayTime]
		[Tooltip("Wait time for characters in the disappearance progression")]
		private float disappearanceWaitTime = 0.015f;

		// Token: 0x04000012 RID: 18
		[SerializeField]
		[MinValue(0.1f)]
		[Tooltip("How much faster/slower is the disappearance progression compared to the typewriter's typing speed")]
		public float disappearanceSpeedMultiplier = 1f;
	}
}

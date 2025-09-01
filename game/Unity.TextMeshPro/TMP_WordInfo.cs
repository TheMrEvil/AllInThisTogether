using System;

namespace TMPro
{
	// Token: 0x02000017 RID: 23
	public struct TMP_WordInfo
	{
		// Token: 0x0600010B RID: 267 RVA: 0x00017168 File Offset: 0x00015368
		public string GetWord()
		{
			string text = string.Empty;
			TMP_CharacterInfo[] characterInfo = this.textComponent.textInfo.characterInfo;
			for (int i = this.firstCharacterIndex; i < this.lastCharacterIndex + 1; i++)
			{
				text += characterInfo[i].character.ToString();
			}
			return text;
		}

		// Token: 0x040000B0 RID: 176
		public TMP_Text textComponent;

		// Token: 0x040000B1 RID: 177
		public int firstCharacterIndex;

		// Token: 0x040000B2 RID: 178
		public int lastCharacterIndex;

		// Token: 0x040000B3 RID: 179
		public int characterCount;
	}
}

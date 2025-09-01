using System;

namespace TMPro
{
	// Token: 0x02000016 RID: 22
	public struct TMP_LinkInfo
	{
		// Token: 0x06000108 RID: 264 RVA: 0x0001709C File Offset: 0x0001529C
		internal void SetLinkID(char[] text, int startIndex, int length)
		{
			if (this.linkID == null || this.linkID.Length < length)
			{
				this.linkID = new char[length];
			}
			for (int i = 0; i < length; i++)
			{
				this.linkID[i] = text[startIndex + i];
			}
		}

		// Token: 0x06000109 RID: 265 RVA: 0x000170E4 File Offset: 0x000152E4
		public string GetLinkText()
		{
			string text = string.Empty;
			TMP_TextInfo textInfo = this.textComponent.textInfo;
			for (int i = this.linkTextfirstCharacterIndex; i < this.linkTextfirstCharacterIndex + this.linkTextLength; i++)
			{
				text += textInfo.characterInfo[i].character.ToString();
			}
			return text;
		}

		// Token: 0x0600010A RID: 266 RVA: 0x0001713E File Offset: 0x0001533E
		public string GetLinkID()
		{
			if (this.textComponent == null)
			{
				return string.Empty;
			}
			return new string(this.linkID, 0, this.linkIdLength);
		}

		// Token: 0x040000A9 RID: 169
		public TMP_Text textComponent;

		// Token: 0x040000AA RID: 170
		public int hashCode;

		// Token: 0x040000AB RID: 171
		public int linkIdFirstCharacterIndex;

		// Token: 0x040000AC RID: 172
		public int linkIdLength;

		// Token: 0x040000AD RID: 173
		public int linkTextfirstCharacterIndex;

		// Token: 0x040000AE RID: 174
		public int linkTextLength;

		// Token: 0x040000AF RID: 175
		internal char[] linkID;
	}
}

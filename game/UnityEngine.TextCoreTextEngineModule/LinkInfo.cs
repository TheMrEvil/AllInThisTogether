using System;

namespace UnityEngine.TextCore.Text
{
	// Token: 0x02000013 RID: 19
	internal struct LinkInfo
	{
		// Token: 0x060000A0 RID: 160 RVA: 0x000060F8 File Offset: 0x000042F8
		internal void SetLinkId(char[] text, int startIndex, int length)
		{
			bool flag = this.linkId == null || this.linkId.Length < length;
			if (flag)
			{
				this.linkId = new char[length];
			}
			for (int i = 0; i < length; i++)
			{
				this.linkId[i] = text[startIndex + i];
			}
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x0000614C File Offset: 0x0000434C
		public string GetLinkText(TextInfo textInfo)
		{
			string text = string.Empty;
			for (int i = this.linkTextfirstCharacterIndex; i < this.linkTextfirstCharacterIndex + this.linkTextLength; i++)
			{
				text += textInfo.textElementInfo[i].character.ToString();
			}
			return text;
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x000061A4 File Offset: 0x000043A4
		public string GetLinkId()
		{
			return new string(this.linkId, 0, this.linkIdLength);
		}

		// Token: 0x04000082 RID: 130
		public int hashCode;

		// Token: 0x04000083 RID: 131
		public int linkIdFirstCharacterIndex;

		// Token: 0x04000084 RID: 132
		public int linkIdLength;

		// Token: 0x04000085 RID: 133
		public int linkTextfirstCharacterIndex;

		// Token: 0x04000086 RID: 134
		public int linkTextLength;

		// Token: 0x04000087 RID: 135
		internal char[] linkId;
	}
}

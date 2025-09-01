using System;

namespace Febucci.UI.Core
{
	// Token: 0x0200003E RID: 62
	public struct WordInfo
	{
		// Token: 0x0600015F RID: 351 RVA: 0x00006C68 File Offset: 0x00004E68
		public WordInfo(int firstCharacterIndex, int lastCharacterIndex, string text)
		{
			this.firstCharacterIndex = firstCharacterIndex;
			this.lastCharacterIndex = lastCharacterIndex;
			this.text = text;
		}

		// Token: 0x040000F0 RID: 240
		public readonly int firstCharacterIndex;

		// Token: 0x040000F1 RID: 241
		public readonly int lastCharacterIndex;

		// Token: 0x040000F2 RID: 242
		public readonly string text;
	}
}

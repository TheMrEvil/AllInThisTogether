using System;

namespace UnityEngine.UIElements.StyleSheets.Syntax
{
	// Token: 0x02000384 RID: 900
	internal struct StyleSyntaxToken
	{
		// Token: 0x06001CC3 RID: 7363 RVA: 0x00088C75 File Offset: 0x00086E75
		public StyleSyntaxToken(StyleSyntaxTokenType t)
		{
			this.type = t;
			this.text = null;
			this.number = 0;
		}

		// Token: 0x06001CC4 RID: 7364 RVA: 0x00088C8D File Offset: 0x00086E8D
		public StyleSyntaxToken(StyleSyntaxTokenType type, string text)
		{
			this.type = type;
			this.text = text;
			this.number = 0;
		}

		// Token: 0x06001CC5 RID: 7365 RVA: 0x00088CA5 File Offset: 0x00086EA5
		public StyleSyntaxToken(StyleSyntaxTokenType type, int number)
		{
			this.type = type;
			this.text = null;
			this.number = number;
		}

		// Token: 0x04000E9B RID: 3739
		public StyleSyntaxTokenType type;

		// Token: 0x04000E9C RID: 3740
		public string text;

		// Token: 0x04000E9D RID: 3741
		public int number;
	}
}

using System;

namespace System.Security.Util
{
	// Token: 0x020003F6 RID: 1014
	internal sealed class TokenizerStringBlock
	{
		// Token: 0x060029A2 RID: 10658 RVA: 0x00097B2D File Offset: 0x00095D2D
		public TokenizerStringBlock()
		{
		}

		// Token: 0x04001F3F RID: 7999
		internal string[] m_block = new string[16];

		// Token: 0x04001F40 RID: 8000
		internal TokenizerStringBlock m_next;
	}
}

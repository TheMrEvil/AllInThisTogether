using System;

namespace System.Security.Util
{
	// Token: 0x020003F5 RID: 1013
	internal sealed class TokenizerShortBlock
	{
		// Token: 0x060029A1 RID: 10657 RVA: 0x00097B18 File Offset: 0x00095D18
		public TokenizerShortBlock()
		{
		}

		// Token: 0x04001F3D RID: 7997
		internal short[] m_block = new short[16];

		// Token: 0x04001F3E RID: 7998
		internal TokenizerShortBlock m_next;
	}
}

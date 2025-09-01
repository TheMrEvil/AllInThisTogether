using System;

namespace System.Security.Util
{
	// Token: 0x020003F7 RID: 1015
	internal sealed class TokenizerStream
	{
		// Token: 0x060029A3 RID: 10659 RVA: 0x00097B42 File Offset: 0x00095D42
		internal TokenizerStream()
		{
			this.m_countTokens = 0;
			this.m_headTokens = new TokenizerShortBlock();
			this.m_headStrings = new TokenizerStringBlock();
			this.Reset();
		}

		// Token: 0x060029A4 RID: 10660 RVA: 0x00097B70 File Offset: 0x00095D70
		internal void AddToken(short token)
		{
			if (this.m_currentTokens.m_block.Length <= this.m_indexTokens)
			{
				this.m_currentTokens.m_next = new TokenizerShortBlock();
				this.m_currentTokens = this.m_currentTokens.m_next;
				this.m_indexTokens = 0;
			}
			this.m_countTokens++;
			short[] block = this.m_currentTokens.m_block;
			int indexTokens = this.m_indexTokens;
			this.m_indexTokens = indexTokens + 1;
			block[indexTokens] = token;
		}

		// Token: 0x060029A5 RID: 10661 RVA: 0x00097BE8 File Offset: 0x00095DE8
		internal void AddString(string str)
		{
			if (this.m_currentStrings.m_block.Length <= this.m_indexStrings)
			{
				this.m_currentStrings.m_next = new TokenizerStringBlock();
				this.m_currentStrings = this.m_currentStrings.m_next;
				this.m_indexStrings = 0;
			}
			string[] block = this.m_currentStrings.m_block;
			int indexStrings = this.m_indexStrings;
			this.m_indexStrings = indexStrings + 1;
			block[indexStrings] = str;
		}

		// Token: 0x060029A6 RID: 10662 RVA: 0x00097C50 File Offset: 0x00095E50
		internal void Reset()
		{
			this.m_lastTokens = null;
			this.m_currentTokens = this.m_headTokens;
			this.m_currentStrings = this.m_headStrings;
			this.m_indexTokens = 0;
			this.m_indexStrings = 0;
		}

		// Token: 0x060029A7 RID: 10663 RVA: 0x00097C80 File Offset: 0x00095E80
		internal short GetNextFullToken()
		{
			if (this.m_currentTokens.m_block.Length <= this.m_indexTokens)
			{
				this.m_lastTokens = this.m_currentTokens;
				this.m_currentTokens = this.m_currentTokens.m_next;
				this.m_indexTokens = 0;
			}
			short[] block = this.m_currentTokens.m_block;
			int indexTokens = this.m_indexTokens;
			this.m_indexTokens = indexTokens + 1;
			return block[indexTokens];
		}

		// Token: 0x060029A8 RID: 10664 RVA: 0x00097CE3 File Offset: 0x00095EE3
		internal short GetNextToken()
		{
			return this.GetNextFullToken() & 255;
		}

		// Token: 0x060029A9 RID: 10665 RVA: 0x00097CF4 File Offset: 0x00095EF4
		internal string GetNextString()
		{
			if (this.m_currentStrings.m_block.Length <= this.m_indexStrings)
			{
				this.m_currentStrings = this.m_currentStrings.m_next;
				this.m_indexStrings = 0;
			}
			string[] block = this.m_currentStrings.m_block;
			int indexStrings = this.m_indexStrings;
			this.m_indexStrings = indexStrings + 1;
			return block[indexStrings];
		}

		// Token: 0x060029AA RID: 10666 RVA: 0x00097D4B File Offset: 0x00095F4B
		internal void ThrowAwayNextString()
		{
			this.GetNextString();
		}

		// Token: 0x060029AB RID: 10667 RVA: 0x00097D54 File Offset: 0x00095F54
		internal void TagLastToken(short tag)
		{
			if (this.m_indexTokens == 0)
			{
				this.m_lastTokens.m_block[this.m_lastTokens.m_block.Length - 1] = (short)((ushort)this.m_lastTokens.m_block[this.m_lastTokens.m_block.Length - 1] | (ushort)tag);
				return;
			}
			this.m_currentTokens.m_block[this.m_indexTokens - 1] = (short)((ushort)this.m_currentTokens.m_block[this.m_indexTokens - 1] | (ushort)tag);
		}

		// Token: 0x060029AC RID: 10668 RVA: 0x00097DD2 File Offset: 0x00095FD2
		internal int GetTokenCount()
		{
			return this.m_countTokens;
		}

		// Token: 0x060029AD RID: 10669 RVA: 0x00097DDC File Offset: 0x00095FDC
		internal void GoToPosition(int position)
		{
			this.Reset();
			for (int i = 0; i < position; i++)
			{
				if (this.GetNextToken() == 3)
				{
					this.ThrowAwayNextString();
				}
			}
		}

		// Token: 0x04001F41 RID: 8001
		private int m_countTokens;

		// Token: 0x04001F42 RID: 8002
		private TokenizerShortBlock m_headTokens;

		// Token: 0x04001F43 RID: 8003
		private TokenizerShortBlock m_lastTokens;

		// Token: 0x04001F44 RID: 8004
		private TokenizerShortBlock m_currentTokens;

		// Token: 0x04001F45 RID: 8005
		private int m_indexTokens;

		// Token: 0x04001F46 RID: 8006
		private TokenizerStringBlock m_headStrings;

		// Token: 0x04001F47 RID: 8007
		private TokenizerStringBlock m_currentStrings;

		// Token: 0x04001F48 RID: 8008
		private int m_indexStrings;
	}
}

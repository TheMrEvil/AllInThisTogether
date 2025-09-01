using System;
using System.Diagnostics;

namespace System.Xml.Xsl.Runtime
{
	// Token: 0x0200048F RID: 1167
	internal class TokenInfo
	{
		// Token: 0x06002D7D RID: 11645 RVA: 0x0000216B File Offset: 0x0000036B
		private TokenInfo()
		{
		}

		// Token: 0x06002D7E RID: 11646 RVA: 0x0000B528 File Offset: 0x00009728
		[Conditional("DEBUG")]
		public void AssertSeparator(bool isSeparator)
		{
		}

		// Token: 0x06002D7F RID: 11647 RVA: 0x00109DB0 File Offset: 0x00107FB0
		public static TokenInfo CreateSeparator(string formatString, int startIdx, int tokLen)
		{
			return new TokenInfo
			{
				startIdx = startIdx,
				formatString = formatString,
				length = tokLen
			};
		}

		// Token: 0x06002D80 RID: 11648 RVA: 0x00109DCC File Offset: 0x00107FCC
		public static TokenInfo CreateFormat(string formatString, int startIdx, int tokLen)
		{
			TokenInfo tokenInfo = new TokenInfo();
			tokenInfo.formatString = null;
			tokenInfo.length = 1;
			bool flag = false;
			char c = formatString[startIdx];
			if (c <= 'A')
			{
				if (c == '1' || c == 'A')
				{
					goto IL_89;
				}
			}
			else if (c == 'I' || c == 'a' || c == 'i')
			{
				goto IL_89;
			}
			if (!CharUtil.IsDecimalDigitOne(c))
			{
				if (CharUtil.IsDecimalDigitOne(c + '\u0001'))
				{
					int num = startIdx;
					do
					{
						tokenInfo.length++;
					}
					while (--tokLen > 0 && c == formatString[++num]);
					if (formatString[num] == (c += '\u0001'))
					{
						goto IL_89;
					}
				}
				flag = true;
			}
			IL_89:
			if (tokLen != 1)
			{
				flag = true;
			}
			if (flag)
			{
				tokenInfo.startChar = '1';
				tokenInfo.length = 1;
			}
			else
			{
				tokenInfo.startChar = c;
			}
			return tokenInfo;
		}

		// Token: 0x04002334 RID: 9012
		public char startChar;

		// Token: 0x04002335 RID: 9013
		public int startIdx;

		// Token: 0x04002336 RID: 9014
		public string formatString;

		// Token: 0x04002337 RID: 9015
		public int length;
	}
}

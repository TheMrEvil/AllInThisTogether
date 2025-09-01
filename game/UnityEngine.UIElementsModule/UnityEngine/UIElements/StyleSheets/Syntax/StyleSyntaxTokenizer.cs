using System;
using System.Collections.Generic;

namespace UnityEngine.UIElements.StyleSheets.Syntax
{
	// Token: 0x02000385 RID: 901
	internal class StyleSyntaxTokenizer
	{
		// Token: 0x170006DC RID: 1756
		// (get) Token: 0x06001CC6 RID: 7366 RVA: 0x00088CC0 File Offset: 0x00086EC0
		public StyleSyntaxToken current
		{
			get
			{
				bool flag = this.m_CurrentTokenIndex < 0 || this.m_CurrentTokenIndex >= this.m_Tokens.Count;
				StyleSyntaxToken result;
				if (flag)
				{
					result = new StyleSyntaxToken(StyleSyntaxTokenType.Unknown);
				}
				else
				{
					result = this.m_Tokens[this.m_CurrentTokenIndex];
				}
				return result;
			}
		}

		// Token: 0x06001CC7 RID: 7367 RVA: 0x00088D14 File Offset: 0x00086F14
		public StyleSyntaxToken MoveNext()
		{
			StyleSyntaxToken current = this.current;
			bool flag = current.type == StyleSyntaxTokenType.Unknown;
			StyleSyntaxToken result;
			if (flag)
			{
				result = current;
			}
			else
			{
				this.m_CurrentTokenIndex++;
				current = this.current;
				bool flag2 = this.m_CurrentTokenIndex == this.m_Tokens.Count;
				if (flag2)
				{
					this.m_CurrentTokenIndex = -1;
				}
				result = current;
			}
			return result;
		}

		// Token: 0x06001CC8 RID: 7368 RVA: 0x00088D74 File Offset: 0x00086F74
		public StyleSyntaxToken PeekNext()
		{
			int num = this.m_CurrentTokenIndex + 1;
			bool flag = this.m_CurrentTokenIndex < 0 || num >= this.m_Tokens.Count;
			StyleSyntaxToken result;
			if (flag)
			{
				result = new StyleSyntaxToken(StyleSyntaxTokenType.Unknown);
			}
			else
			{
				result = this.m_Tokens[num];
			}
			return result;
		}

		// Token: 0x06001CC9 RID: 7369 RVA: 0x00088DC8 File Offset: 0x00086FC8
		public void Tokenize(string syntax)
		{
			this.m_Tokens.Clear();
			this.m_CurrentTokenIndex = 0;
			syntax = syntax.Trim(new char[]
			{
				' '
			}).ToLower();
			int i = 0;
			while (i < syntax.Length)
			{
				char c = syntax[i];
				char c2 = c;
				char c3 = c2;
				if (c3 <= '?')
				{
					switch (c3)
					{
					case ' ':
						i = StyleSyntaxTokenizer.GlobCharacter(syntax, i, ' ');
						this.m_Tokens.Add(new StyleSyntaxToken(StyleSyntaxTokenType.Space));
						break;
					case '!':
						this.m_Tokens.Add(new StyleSyntaxToken(StyleSyntaxTokenType.ExclamationPoint));
						break;
					case '"':
					case '$':
					case '%':
					case '(':
					case ')':
						goto IL_2EA;
					case '#':
						this.m_Tokens.Add(new StyleSyntaxToken(StyleSyntaxTokenType.HashMark));
						break;
					case '&':
					{
						bool flag = !StyleSyntaxTokenizer.IsNextCharacter(syntax, i, '&');
						if (flag)
						{
							string text = (i + 1 < syntax.Length) ? syntax[i + 1].ToString() : "EOF";
							Debug.LogAssertionFormat("Expected '&' got '{0}'", new object[]
							{
								text
							});
							this.m_Tokens.Add(new StyleSyntaxToken(StyleSyntaxTokenType.Unknown));
						}
						else
						{
							this.m_Tokens.Add(new StyleSyntaxToken(StyleSyntaxTokenType.DoubleAmpersand));
							i++;
						}
						break;
					}
					case '\'':
						this.m_Tokens.Add(new StyleSyntaxToken(StyleSyntaxTokenType.SingleQuote));
						break;
					case '*':
						this.m_Tokens.Add(new StyleSyntaxToken(StyleSyntaxTokenType.Asterisk));
						break;
					case '+':
						this.m_Tokens.Add(new StyleSyntaxToken(StyleSyntaxTokenType.Plus));
						break;
					case ',':
						this.m_Tokens.Add(new StyleSyntaxToken(StyleSyntaxTokenType.Comma));
						break;
					default:
						switch (c3)
						{
						case '<':
							this.m_Tokens.Add(new StyleSyntaxToken(StyleSyntaxTokenType.LessThan));
							break;
						case '=':
							goto IL_2EA;
						case '>':
							this.m_Tokens.Add(new StyleSyntaxToken(StyleSyntaxTokenType.GreaterThan));
							break;
						case '?':
							this.m_Tokens.Add(new StyleSyntaxToken(StyleSyntaxTokenType.QuestionMark));
							break;
						default:
							goto IL_2EA;
						}
						break;
					}
				}
				else if (c3 != '[')
				{
					if (c3 != ']')
					{
						switch (c3)
						{
						case '{':
							this.m_Tokens.Add(new StyleSyntaxToken(StyleSyntaxTokenType.OpenBrace));
							break;
						case '|':
						{
							bool flag2 = StyleSyntaxTokenizer.IsNextCharacter(syntax, i, '|');
							if (flag2)
							{
								this.m_Tokens.Add(new StyleSyntaxToken(StyleSyntaxTokenType.DoubleBar));
								i++;
							}
							else
							{
								this.m_Tokens.Add(new StyleSyntaxToken(StyleSyntaxTokenType.SingleBar));
							}
							break;
						}
						case '}':
							this.m_Tokens.Add(new StyleSyntaxToken(StyleSyntaxTokenType.CloseBrace));
							break;
						default:
							goto IL_2EA;
						}
					}
					else
					{
						this.m_Tokens.Add(new StyleSyntaxToken(StyleSyntaxTokenType.CloseBracket));
					}
				}
				else
				{
					this.m_Tokens.Add(new StyleSyntaxToken(StyleSyntaxTokenType.OpenBracket));
				}
				IL_3C5:
				i++;
				continue;
				IL_2EA:
				bool flag3 = char.IsNumber(c);
				if (flag3)
				{
					int startIndex = i;
					int num = 1;
					while (StyleSyntaxTokenizer.IsNextNumber(syntax, i))
					{
						i++;
						num++;
					}
					string s = syntax.Substring(startIndex, num);
					int number = int.Parse(s);
					this.m_Tokens.Add(new StyleSyntaxToken(StyleSyntaxTokenType.Number, number));
				}
				else
				{
					bool flag4 = char.IsLetter(c);
					if (flag4)
					{
						int startIndex2 = i;
						int num2 = 1;
						while (StyleSyntaxTokenizer.IsNextLetterOrDash(syntax, i))
						{
							i++;
							num2++;
						}
						string text2 = syntax.Substring(startIndex2, num2);
						this.m_Tokens.Add(new StyleSyntaxToken(StyleSyntaxTokenType.String, text2));
					}
					else
					{
						Debug.LogAssertionFormat("Expected letter or number got '{0}'", new object[]
						{
							c
						});
						this.m_Tokens.Add(new StyleSyntaxToken(StyleSyntaxTokenType.Unknown));
					}
				}
				goto IL_3C5;
			}
			this.m_Tokens.Add(new StyleSyntaxToken(StyleSyntaxTokenType.End));
		}

		// Token: 0x06001CCA RID: 7370 RVA: 0x000891C4 File Offset: 0x000873C4
		private static bool IsNextCharacter(string s, int index, char c)
		{
			return index + 1 < s.Length && s[index + 1] == c;
		}

		// Token: 0x06001CCB RID: 7371 RVA: 0x000891F0 File Offset: 0x000873F0
		private static bool IsNextLetterOrDash(string s, int index)
		{
			return index + 1 < s.Length && (char.IsLetter(s[index + 1]) || s[index + 1] == '-');
		}

		// Token: 0x06001CCC RID: 7372 RVA: 0x00089230 File Offset: 0x00087430
		private static bool IsNextNumber(string s, int index)
		{
			return index + 1 < s.Length && char.IsNumber(s[index + 1]);
		}

		// Token: 0x06001CCD RID: 7373 RVA: 0x00089260 File Offset: 0x00087460
		private static int GlobCharacter(string s, int index, char c)
		{
			while (StyleSyntaxTokenizer.IsNextCharacter(s, index, c))
			{
				index++;
			}
			return index;
		}

		// Token: 0x06001CCE RID: 7374 RVA: 0x00089288 File Offset: 0x00087488
		public StyleSyntaxTokenizer()
		{
		}

		// Token: 0x04000E9E RID: 3742
		private List<StyleSyntaxToken> m_Tokens = new List<StyleSyntaxToken>();

		// Token: 0x04000E9F RID: 3743
		private int m_CurrentTokenIndex = -1;
	}
}

using System;
using System.Globalization;

namespace System.Net.Http.Headers
{
	// Token: 0x0200004E RID: 78
	internal class Lexer
	{
		// Token: 0x060002F2 RID: 754 RVA: 0x0000A8E1 File Offset: 0x00008AE1
		public Lexer(string stream)
		{
			this.s = stream;
		}

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x060002F3 RID: 755 RVA: 0x0000A8F0 File Offset: 0x00008AF0
		// (set) Token: 0x060002F4 RID: 756 RVA: 0x0000A8F8 File Offset: 0x00008AF8
		public int Position
		{
			get
			{
				return this.pos;
			}
			set
			{
				this.pos = value;
			}
		}

		// Token: 0x060002F5 RID: 757 RVA: 0x0000A901 File Offset: 0x00008B01
		public string GetStringValue(Token token)
		{
			return this.s.Substring(token.StartPosition, token.EndPosition - token.StartPosition);
		}

		// Token: 0x060002F6 RID: 758 RVA: 0x0000A924 File Offset: 0x00008B24
		public string GetStringValue(Token start, Token end)
		{
			return this.s.Substring(start.StartPosition, end.EndPosition - start.StartPosition);
		}

		// Token: 0x060002F7 RID: 759 RVA: 0x0000A947 File Offset: 0x00008B47
		public string GetQuotedStringValue(Token start)
		{
			return this.s.Substring(start.StartPosition + 1, start.EndPosition - start.StartPosition - 2);
		}

		// Token: 0x060002F8 RID: 760 RVA: 0x0000A96E File Offset: 0x00008B6E
		public string GetRemainingStringValue(int position)
		{
			if (position <= this.s.Length)
			{
				return this.s.Substring(position);
			}
			return null;
		}

		// Token: 0x060002F9 RID: 761 RVA: 0x0000A98C File Offset: 0x00008B8C
		public bool IsStarStringValue(Token token)
		{
			return token.EndPosition - token.StartPosition == 1 && this.s[token.StartPosition] == '*';
		}

		// Token: 0x060002FA RID: 762 RVA: 0x0000A9B8 File Offset: 0x00008BB8
		public bool TryGetNumericValue(Token token, out int value)
		{
			return int.TryParse(this.GetStringValue(token), NumberStyles.None, CultureInfo.InvariantCulture, out value);
		}

		// Token: 0x060002FB RID: 763 RVA: 0x0000A9CD File Offset: 0x00008BCD
		public bool TryGetNumericValue(Token token, out long value)
		{
			return long.TryParse(this.GetStringValue(token), NumberStyles.None, CultureInfo.InvariantCulture, out value);
		}

		// Token: 0x060002FC RID: 764 RVA: 0x0000A9E4 File Offset: 0x00008BE4
		public TimeSpan? TryGetTimeSpanValue(Token token)
		{
			int num;
			if (this.TryGetNumericValue(token, out num))
			{
				return new TimeSpan?(TimeSpan.FromSeconds((double)num));
			}
			return null;
		}

		// Token: 0x060002FD RID: 765 RVA: 0x0000AA12 File Offset: 0x00008C12
		public bool TryGetDateValue(Token token, out DateTimeOffset value)
		{
			return Lexer.TryGetDateValue((token == Token.Type.QuotedString) ? this.s.Substring(token.StartPosition + 1, token.EndPosition - token.StartPosition - 2) : this.GetStringValue(token), out value);
		}

		// Token: 0x060002FE RID: 766 RVA: 0x0000AA51 File Offset: 0x00008C51
		public static bool TryGetDateValue(string text, out DateTimeOffset value)
		{
			return DateTimeOffset.TryParseExact(text, Lexer.dt_formats, DateTimeFormatInfo.InvariantInfo, DateTimeStyles.AllowLeadingWhite | DateTimeStyles.AllowTrailingWhite | DateTimeStyles.AllowInnerWhite | DateTimeStyles.AssumeUniversal, out value);
		}

		// Token: 0x060002FF RID: 767 RVA: 0x0000AA66 File Offset: 0x00008C66
		public bool TryGetDoubleValue(Token token, out double value)
		{
			return double.TryParse(this.GetStringValue(token), NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out value);
		}

		// Token: 0x06000300 RID: 768 RVA: 0x0000AA7C File Offset: 0x00008C7C
		public static bool IsValidToken(string input)
		{
			int i;
			for (i = 0; i < input.Length; i++)
			{
				if (!Lexer.IsValidCharacter(input[i]))
				{
					return false;
				}
			}
			return i > 0;
		}

		// Token: 0x06000301 RID: 769 RVA: 0x0000AAAE File Offset: 0x00008CAE
		public static bool IsValidCharacter(char input)
		{
			return (int)input < Lexer.last_token_char && Lexer.token_chars[(int)input];
		}

		// Token: 0x06000302 RID: 770 RVA: 0x0000AAC1 File Offset: 0x00008CC1
		public void EatChar()
		{
			this.pos++;
		}

		// Token: 0x06000303 RID: 771 RVA: 0x0000AAD1 File Offset: 0x00008CD1
		public int PeekChar()
		{
			if (this.pos >= this.s.Length)
			{
				return -1;
			}
			return (int)this.s[this.pos];
		}

		// Token: 0x06000304 RID: 772 RVA: 0x0000AAFC File Offset: 0x00008CFC
		public bool ScanCommentOptional(out string value)
		{
			Token token;
			return this.ScanCommentOptional(out value, out token) || token == Token.Type.End;
		}

		// Token: 0x06000305 RID: 773 RVA: 0x0000AB20 File Offset: 0x00008D20
		public bool ScanCommentOptional(out string value, out Token readToken)
		{
			readToken = this.Scan(false);
			if (readToken != Token.Type.OpenParens)
			{
				value = null;
				return false;
			}
			int num = 1;
			while (this.pos < this.s.Length)
			{
				char c = this.s[this.pos];
				if (c == '(')
				{
					num++;
					this.pos++;
				}
				else if (c == ')')
				{
					this.pos++;
					if (--num <= 0)
					{
						int startPosition = readToken.StartPosition;
						value = this.s.Substring(startPosition, this.pos - startPosition);
						return true;
					}
				}
				else
				{
					if (c < ' ' || c > '~')
					{
						break;
					}
					this.pos++;
				}
			}
			value = null;
			return false;
		}

		// Token: 0x06000306 RID: 774 RVA: 0x0000ABEC File Offset: 0x00008DEC
		public Token Scan(bool recognizeDash = false)
		{
			int startPosition = this.pos;
			if (this.s == null)
			{
				return new Token(Token.Type.Error, 0, 0);
			}
			Token.Type type;
			if (this.pos >= this.s.Length)
			{
				type = Token.Type.End;
			}
			else
			{
				type = Token.Type.Error;
				char c;
				for (;;)
				{
					string text = this.s;
					int num = this.pos;
					this.pos = num + 1;
					c = text[num];
					if (c > '"')
					{
						goto IL_6D;
					}
					if (c != '\t' && c != ' ')
					{
						break;
					}
					if (this.pos == this.s.Length)
					{
						goto Block_12;
					}
				}
				if (c != '"')
				{
					goto IL_171;
				}
				startPosition = this.pos - 1;
				while (this.pos < this.s.Length)
				{
					string text2 = this.s;
					int num = this.pos;
					this.pos = num + 1;
					c = text2[num];
					if (c == '\\')
					{
						if (this.pos + 1 >= this.s.Length)
						{
							break;
						}
						this.pos++;
					}
					else if (c == '"')
					{
						type = Token.Type.QuotedString;
						break;
					}
				}
				goto IL_1D3;
				IL_6D:
				if (c <= '/')
				{
					if (c == '(')
					{
						startPosition = this.pos - 1;
						type = Token.Type.OpenParens;
						goto IL_1D3;
					}
					switch (c)
					{
					case ',':
						type = Token.Type.SeparatorComma;
						goto IL_1D3;
					case '-':
						if (recognizeDash)
						{
							type = Token.Type.SeparatorDash;
							goto IL_1D3;
						}
						goto IL_171;
					case '.':
						goto IL_171;
					case '/':
						type = Token.Type.SeparatorSlash;
						goto IL_1D3;
					default:
						goto IL_171;
					}
				}
				else
				{
					if (c == ';')
					{
						type = Token.Type.SeparatorSemicolon;
						goto IL_1D3;
					}
					if (c != '=')
					{
						goto IL_171;
					}
					type = Token.Type.SeparatorEqual;
					goto IL_1D3;
				}
				Block_12:
				type = Token.Type.End;
				goto IL_1D3;
				IL_171:
				if ((int)c < Lexer.last_token_char && Lexer.token_chars[(int)c])
				{
					startPosition = this.pos - 1;
					type = Token.Type.Token;
					while (this.pos < this.s.Length)
					{
						c = this.s[this.pos];
						if ((int)c >= Lexer.last_token_char || !Lexer.token_chars[(int)c])
						{
							break;
						}
						this.pos++;
					}
				}
			}
			IL_1D3:
			return new Token(type, startPosition, this.pos);
		}

		// Token: 0x06000307 RID: 775 RVA: 0x0000ADDC File Offset: 0x00008FDC
		// Note: this type is marked as 'beforefieldinit'.
		static Lexer()
		{
		}

		// Token: 0x0400012E RID: 302
		private static readonly bool[] token_chars = new bool[]
		{
			false,
			false,
			false,
			false,
			false,
			false,
			false,
			false,
			false,
			false,
			false,
			false,
			false,
			false,
			false,
			false,
			false,
			false,
			false,
			false,
			false,
			false,
			false,
			false,
			false,
			false,
			false,
			false,
			false,
			false,
			false,
			false,
			false,
			true,
			false,
			true,
			true,
			true,
			true,
			true,
			false,
			false,
			true,
			true,
			false,
			true,
			true,
			false,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			false,
			false,
			false,
			false,
			false,
			false,
			false,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			false,
			false,
			false,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			false,
			true,
			false,
			true
		};

		// Token: 0x0400012F RID: 303
		private static readonly int last_token_char = Lexer.token_chars.Length;

		// Token: 0x04000130 RID: 304
		private static readonly string[] dt_formats = new string[]
		{
			"r",
			"dddd, dd'-'MMM'-'yy HH:mm:ss 'GMT'",
			"ddd MMM d HH:mm:ss yyyy",
			"d MMM yy H:m:s",
			"ddd, d MMM yyyy H:m:s zzz"
		};

		// Token: 0x04000131 RID: 305
		private readonly string s;

		// Token: 0x04000132 RID: 306
		private int pos;
	}
}

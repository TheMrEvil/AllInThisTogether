using System;
using System.Text;

namespace System.Data.Odbc
{
	// Token: 0x020002FF RID: 767
	internal sealed class CStringTokenizer
	{
		// Token: 0x0600220E RID: 8718 RVA: 0x0009E8E0 File Offset: 0x0009CAE0
		internal CStringTokenizer(string text, char quote, char escape)
		{
			this._token = new StringBuilder();
			this._quote = quote;
			this._escape = escape;
			this._sqlstatement = text;
			if (text != null)
			{
				int num = text.IndexOf('\0');
				this._len = ((0 > num) ? text.Length : num);
				return;
			}
			this._len = 0;
		}

		// Token: 0x17000613 RID: 1555
		// (get) Token: 0x0600220F RID: 8719 RVA: 0x0009E939 File Offset: 0x0009CB39
		internal int CurrentPosition
		{
			get
			{
				return this._idx;
			}
		}

		// Token: 0x06002210 RID: 8720 RVA: 0x0009E944 File Offset: 0x0009CB44
		internal string NextToken()
		{
			if (this._token.Length != 0)
			{
				this._idx += this._token.Length;
				this._token.Remove(0, this._token.Length);
			}
			while (this._idx < this._len && char.IsWhiteSpace(this._sqlstatement[this._idx]))
			{
				this._idx++;
			}
			if (this._idx == this._len)
			{
				return string.Empty;
			}
			int i = this._idx;
			bool flag = false;
			while (!flag && i < this._len)
			{
				if (this.IsValidNameChar(this._sqlstatement[i]))
				{
					while (i < this._len)
					{
						if (!this.IsValidNameChar(this._sqlstatement[i]))
						{
							break;
						}
						this._token.Append(this._sqlstatement[i]);
						i++;
					}
				}
				else
				{
					char c = this._sqlstatement[i];
					if (c == '[')
					{
						i = this.GetTokenFromBracket(i);
					}
					else
					{
						if (' ' == this._quote || c != this._quote)
						{
							if (!char.IsWhiteSpace(c))
							{
								if (c == ',')
								{
									if (i == this._idx)
									{
										this._token.Append(c);
									}
								}
								else
								{
									this._token.Append(c);
								}
							}
							break;
						}
						i = this.GetTokenFromQuote(i);
					}
				}
			}
			if (this._token.Length <= 0)
			{
				return string.Empty;
			}
			return this._token.ToString();
		}

		// Token: 0x06002211 RID: 8721 RVA: 0x0009EAD7 File Offset: 0x0009CCD7
		private int GetTokenFromBracket(int curidx)
		{
			while (curidx < this._len)
			{
				this._token.Append(this._sqlstatement[curidx]);
				curidx++;
				if (this._sqlstatement[curidx - 1] == ']')
				{
					break;
				}
			}
			return curidx;
		}

		// Token: 0x06002212 RID: 8722 RVA: 0x0009EB14 File Offset: 0x0009CD14
		private int GetTokenFromQuote(int curidx)
		{
			int i;
			for (i = curidx; i < this._len; i++)
			{
				this._token.Append(this._sqlstatement[i]);
				if (this._sqlstatement[i] == this._quote && i > curidx && this._sqlstatement[i - 1] != this._escape && i + 1 < this._len && this._sqlstatement[i + 1] != this._quote)
				{
					return i + 1;
				}
			}
			return i;
		}

		// Token: 0x06002213 RID: 8723 RVA: 0x0009EBA0 File Offset: 0x0009CDA0
		private bool IsValidNameChar(char ch)
		{
			return char.IsLetterOrDigit(ch) || ch == '_' || ch == '-' || ch == '.' || ch == '$' || ch == '#' || ch == '@' || ch == '~' || ch == '`' || ch == '%' || ch == '^' || ch == '&' || ch == '|';
		}

		// Token: 0x06002214 RID: 8724 RVA: 0x0009EBF4 File Offset: 0x0009CDF4
		internal int FindTokenIndex(string tokenString)
		{
			string text;
			do
			{
				text = this.NextToken();
				if (this._idx == this._len || string.IsNullOrEmpty(text))
				{
					return -1;
				}
			}
			while (string.Compare(tokenString, text, StringComparison.OrdinalIgnoreCase) != 0);
			return this._idx;
		}

		// Token: 0x06002215 RID: 8725 RVA: 0x0009EC30 File Offset: 0x0009CE30
		internal bool StartsWith(string tokenString)
		{
			int num = 0;
			while (num < this._len && char.IsWhiteSpace(this._sqlstatement[num]))
			{
				num++;
			}
			if (this._len - num < tokenString.Length)
			{
				return false;
			}
			if (string.Compare(this._sqlstatement, num, tokenString, 0, tokenString.Length, StringComparison.OrdinalIgnoreCase) == 0)
			{
				this._idx = 0;
				this.NextToken();
				return true;
			}
			return false;
		}

		// Token: 0x0400181C RID: 6172
		private readonly StringBuilder _token;

		// Token: 0x0400181D RID: 6173
		private readonly string _sqlstatement;

		// Token: 0x0400181E RID: 6174
		private readonly char _quote;

		// Token: 0x0400181F RID: 6175
		private readonly char _escape;

		// Token: 0x04001820 RID: 6176
		private int _len;

		// Token: 0x04001821 RID: 6177
		private int _idx;
	}
}

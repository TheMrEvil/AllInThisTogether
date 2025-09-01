using System;

namespace System.Net
{
	// Token: 0x0200064B RID: 1611
	internal class CookieTokenizer
	{
		// Token: 0x060032BD RID: 12989 RVA: 0x000AFF14 File Offset: 0x000AE114
		internal CookieTokenizer(string tokenStream)
		{
			this.m_length = tokenStream.Length;
			this.m_tokenStream = tokenStream;
		}

		// Token: 0x17000A38 RID: 2616
		// (get) Token: 0x060032BE RID: 12990 RVA: 0x000AFF2F File Offset: 0x000AE12F
		// (set) Token: 0x060032BF RID: 12991 RVA: 0x000AFF37 File Offset: 0x000AE137
		internal bool EndOfCookie
		{
			get
			{
				return this.m_eofCookie;
			}
			set
			{
				this.m_eofCookie = value;
			}
		}

		// Token: 0x17000A39 RID: 2617
		// (get) Token: 0x060032C0 RID: 12992 RVA: 0x000AFF40 File Offset: 0x000AE140
		internal bool Eof
		{
			get
			{
				return this.m_index >= this.m_length;
			}
		}

		// Token: 0x17000A3A RID: 2618
		// (get) Token: 0x060032C1 RID: 12993 RVA: 0x000AFF53 File Offset: 0x000AE153
		// (set) Token: 0x060032C2 RID: 12994 RVA: 0x000AFF5B File Offset: 0x000AE15B
		internal string Name
		{
			get
			{
				return this.m_name;
			}
			set
			{
				this.m_name = value;
			}
		}

		// Token: 0x17000A3B RID: 2619
		// (get) Token: 0x060032C3 RID: 12995 RVA: 0x000AFF64 File Offset: 0x000AE164
		// (set) Token: 0x060032C4 RID: 12996 RVA: 0x000AFF6C File Offset: 0x000AE16C
		internal bool Quoted
		{
			get
			{
				return this.m_quoted;
			}
			set
			{
				this.m_quoted = value;
			}
		}

		// Token: 0x17000A3C RID: 2620
		// (get) Token: 0x060032C5 RID: 12997 RVA: 0x000AFF75 File Offset: 0x000AE175
		// (set) Token: 0x060032C6 RID: 12998 RVA: 0x000AFF7D File Offset: 0x000AE17D
		internal CookieToken Token
		{
			get
			{
				return this.m_token;
			}
			set
			{
				this.m_token = value;
			}
		}

		// Token: 0x17000A3D RID: 2621
		// (get) Token: 0x060032C7 RID: 12999 RVA: 0x000AFF86 File Offset: 0x000AE186
		// (set) Token: 0x060032C8 RID: 13000 RVA: 0x000AFF8E File Offset: 0x000AE18E
		internal string Value
		{
			get
			{
				return this.m_value;
			}
			set
			{
				this.m_value = value;
			}
		}

		// Token: 0x060032C9 RID: 13001 RVA: 0x000AFF98 File Offset: 0x000AE198
		internal string Extract()
		{
			string text = string.Empty;
			if (this.m_tokenLength != 0)
			{
				text = this.m_tokenStream.Substring(this.m_start, this.m_tokenLength);
				if (!this.Quoted)
				{
					text = text.Trim();
				}
			}
			return text;
		}

		// Token: 0x060032CA RID: 13002 RVA: 0x000AFFDC File Offset: 0x000AE1DC
		internal CookieToken FindNext(bool ignoreComma, bool ignoreEquals)
		{
			this.m_tokenLength = 0;
			this.m_start = this.m_index;
			while (this.m_index < this.m_length && char.IsWhiteSpace(this.m_tokenStream[this.m_index]))
			{
				this.m_index++;
				this.m_start++;
			}
			CookieToken result = CookieToken.End;
			int num = 1;
			if (!this.Eof)
			{
				if (this.m_tokenStream[this.m_index] == '"')
				{
					this.Quoted = true;
					this.m_index++;
					bool flag = false;
					while (this.m_index < this.m_length)
					{
						char c = this.m_tokenStream[this.m_index];
						if (!flag && c == '"')
						{
							break;
						}
						if (flag)
						{
							flag = false;
						}
						else if (c == '\\')
						{
							flag = true;
						}
						this.m_index++;
					}
					if (this.m_index < this.m_length)
					{
						this.m_index++;
					}
					this.m_tokenLength = this.m_index - this.m_start;
					num = 0;
					ignoreComma = false;
				}
				while (this.m_index < this.m_length && this.m_tokenStream[this.m_index] != ';' && (ignoreEquals || this.m_tokenStream[this.m_index] != '=') && (ignoreComma || this.m_tokenStream[this.m_index] != ','))
				{
					if (this.m_tokenStream[this.m_index] == ',')
					{
						this.m_start = this.m_index + 1;
						this.m_tokenLength = -1;
						ignoreComma = false;
					}
					this.m_index++;
					this.m_tokenLength += num;
				}
				if (!this.Eof)
				{
					char c2 = this.m_tokenStream[this.m_index];
					if (c2 != ';')
					{
						if (c2 != '=')
						{
							result = CookieToken.EndCookie;
						}
						else
						{
							result = CookieToken.Equals;
						}
					}
					else
					{
						result = CookieToken.EndToken;
					}
					this.m_index++;
				}
			}
			return result;
		}

		// Token: 0x060032CB RID: 13003 RVA: 0x000B01E0 File Offset: 0x000AE3E0
		internal CookieToken Next(bool first, bool parseResponseCookies)
		{
			this.Reset();
			CookieToken cookieToken = this.FindNext(false, false);
			if (cookieToken == CookieToken.EndCookie)
			{
				this.EndOfCookie = true;
			}
			if (cookieToken == CookieToken.End || cookieToken == CookieToken.EndCookie)
			{
				if ((this.Name = this.Extract()).Length != 0)
				{
					this.Token = this.TokenFromName(parseResponseCookies);
					return CookieToken.Attribute;
				}
				return cookieToken;
			}
			else
			{
				this.Name = this.Extract();
				if (first)
				{
					this.Token = CookieToken.CookieName;
				}
				else
				{
					this.Token = this.TokenFromName(parseResponseCookies);
				}
				if (cookieToken == CookieToken.Equals)
				{
					cookieToken = this.FindNext(!first && this.Token == CookieToken.Expires, true);
					if (cookieToken == CookieToken.EndCookie)
					{
						this.EndOfCookie = true;
					}
					this.Value = this.Extract();
					return CookieToken.NameValuePair;
				}
				return CookieToken.Attribute;
			}
		}

		// Token: 0x060032CC RID: 13004 RVA: 0x000B0292 File Offset: 0x000AE492
		internal void Reset()
		{
			this.m_eofCookie = false;
			this.m_name = string.Empty;
			this.m_quoted = false;
			this.m_start = this.m_index;
			this.m_token = CookieToken.Nothing;
			this.m_tokenLength = 0;
			this.m_value = string.Empty;
		}

		// Token: 0x060032CD RID: 13005 RVA: 0x000B02D4 File Offset: 0x000AE4D4
		internal CookieToken TokenFromName(bool parseResponseCookies)
		{
			if (!parseResponseCookies)
			{
				for (int i = 0; i < CookieTokenizer.RecognizedServerAttributes.Length; i++)
				{
					if (CookieTokenizer.RecognizedServerAttributes[i].IsEqualTo(this.Name))
					{
						return CookieTokenizer.RecognizedServerAttributes[i].Token;
					}
				}
			}
			else
			{
				for (int j = 0; j < CookieTokenizer.RecognizedAttributes.Length; j++)
				{
					if (CookieTokenizer.RecognizedAttributes[j].IsEqualTo(this.Name))
					{
						return CookieTokenizer.RecognizedAttributes[j].Token;
					}
				}
			}
			return CookieToken.Unknown;
		}

		// Token: 0x060032CE RID: 13006 RVA: 0x000B0360 File Offset: 0x000AE560
		// Note: this type is marked as 'beforefieldinit'.
		static CookieTokenizer()
		{
		}

		// Token: 0x04001DCF RID: 7631
		private bool m_eofCookie;

		// Token: 0x04001DD0 RID: 7632
		private int m_index;

		// Token: 0x04001DD1 RID: 7633
		private int m_length;

		// Token: 0x04001DD2 RID: 7634
		private string m_name;

		// Token: 0x04001DD3 RID: 7635
		private bool m_quoted;

		// Token: 0x04001DD4 RID: 7636
		private int m_start;

		// Token: 0x04001DD5 RID: 7637
		private CookieToken m_token;

		// Token: 0x04001DD6 RID: 7638
		private int m_tokenLength;

		// Token: 0x04001DD7 RID: 7639
		private string m_tokenStream;

		// Token: 0x04001DD8 RID: 7640
		private string m_value;

		// Token: 0x04001DD9 RID: 7641
		private static CookieTokenizer.RecognizedAttribute[] RecognizedAttributes = new CookieTokenizer.RecognizedAttribute[]
		{
			new CookieTokenizer.RecognizedAttribute("Path", CookieToken.Path),
			new CookieTokenizer.RecognizedAttribute("Max-Age", CookieToken.MaxAge),
			new CookieTokenizer.RecognizedAttribute("Expires", CookieToken.Expires),
			new CookieTokenizer.RecognizedAttribute("Version", CookieToken.Version),
			new CookieTokenizer.RecognizedAttribute("Domain", CookieToken.Domain),
			new CookieTokenizer.RecognizedAttribute("Secure", CookieToken.Secure),
			new CookieTokenizer.RecognizedAttribute("Discard", CookieToken.Discard),
			new CookieTokenizer.RecognizedAttribute("Port", CookieToken.Port),
			new CookieTokenizer.RecognizedAttribute("Comment", CookieToken.Comment),
			new CookieTokenizer.RecognizedAttribute("CommentURL", CookieToken.CommentUrl),
			new CookieTokenizer.RecognizedAttribute("HttpOnly", CookieToken.HttpOnly)
		};

		// Token: 0x04001DDA RID: 7642
		private static CookieTokenizer.RecognizedAttribute[] RecognizedServerAttributes = new CookieTokenizer.RecognizedAttribute[]
		{
			new CookieTokenizer.RecognizedAttribute("$Path", CookieToken.Path),
			new CookieTokenizer.RecognizedAttribute("$Version", CookieToken.Version),
			new CookieTokenizer.RecognizedAttribute("$Domain", CookieToken.Domain),
			new CookieTokenizer.RecognizedAttribute("$Port", CookieToken.Port),
			new CookieTokenizer.RecognizedAttribute("$HttpOnly", CookieToken.HttpOnly)
		};

		// Token: 0x0200064C RID: 1612
		private struct RecognizedAttribute
		{
			// Token: 0x060032CF RID: 13007 RVA: 0x000B04B4 File Offset: 0x000AE6B4
			internal RecognizedAttribute(string name, CookieToken token)
			{
				this.m_name = name;
				this.m_token = token;
			}

			// Token: 0x17000A3E RID: 2622
			// (get) Token: 0x060032D0 RID: 13008 RVA: 0x000B04C4 File Offset: 0x000AE6C4
			internal CookieToken Token
			{
				get
				{
					return this.m_token;
				}
			}

			// Token: 0x060032D1 RID: 13009 RVA: 0x000B04CC File Offset: 0x000AE6CC
			internal bool IsEqualTo(string value)
			{
				return string.Compare(this.m_name, value, StringComparison.OrdinalIgnoreCase) == 0;
			}

			// Token: 0x04001DDB RID: 7643
			private string m_name;

			// Token: 0x04001DDC RID: 7644
			private CookieToken m_token;
		}
	}
}

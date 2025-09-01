using System;
using System.Globalization;
using System.Runtime.CompilerServices;

namespace System
{
	// Token: 0x02000125 RID: 293
	internal ref struct __DTString
	{
		// Token: 0x170000DE RID: 222
		// (get) Token: 0x06000B70 RID: 2928 RVA: 0x0002EAE2 File Offset: 0x0002CCE2
		internal int Length
		{
			get
			{
				return this.Value.Length;
			}
		}

		// Token: 0x06000B71 RID: 2929 RVA: 0x0002EAEF File Offset: 0x0002CCEF
		internal __DTString(ReadOnlySpan<char> str, DateTimeFormatInfo dtfi, bool checkDigitToken)
		{
			this = new __DTString(str, dtfi);
			this.m_checkDigitToken = checkDigitToken;
		}

		// Token: 0x06000B72 RID: 2930 RVA: 0x0002EB00 File Offset: 0x0002CD00
		internal __DTString(ReadOnlySpan<char> str, DateTimeFormatInfo dtfi)
		{
			this.Index = -1;
			this.Value = str;
			this.m_current = '\0';
			if (dtfi != null)
			{
				this.m_info = dtfi.CompareInfo;
				this.m_checkDigitToken = ((dtfi.FormatFlags & DateTimeFormatFlags.UseDigitPrefixInTokens) > DateTimeFormatFlags.None);
				return;
			}
			this.m_info = CultureInfo.CurrentCulture.CompareInfo;
			this.m_checkDigitToken = false;
		}

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x06000B73 RID: 2931 RVA: 0x0002EB5B File Offset: 0x0002CD5B
		internal CompareInfo CompareInfo
		{
			get
			{
				return this.m_info;
			}
		}

		// Token: 0x06000B74 RID: 2932 RVA: 0x0002EB63 File Offset: 0x0002CD63
		internal unsafe bool GetNext()
		{
			this.Index++;
			if (this.Index < this.Length)
			{
				this.m_current = (char)(*this.Value[this.Index]);
				return true;
			}
			return false;
		}

		// Token: 0x06000B75 RID: 2933 RVA: 0x0002EB9C File Offset: 0x0002CD9C
		internal bool AtEnd()
		{
			return this.Index >= this.Length;
		}

		// Token: 0x06000B76 RID: 2934 RVA: 0x0002EBAF File Offset: 0x0002CDAF
		internal unsafe bool Advance(int count)
		{
			this.Index += count;
			if (this.Index < this.Length)
			{
				this.m_current = (char)(*this.Value[this.Index]);
				return true;
			}
			return false;
		}

		// Token: 0x06000B77 RID: 2935 RVA: 0x0002EBE8 File Offset: 0x0002CDE8
		internal unsafe void GetRegularToken(out TokenType tokenType, out int tokenValue, DateTimeFormatInfo dtfi)
		{
			tokenValue = 0;
			if (this.Index >= this.Length)
			{
				tokenType = TokenType.EndOfString;
				return;
			}
			tokenType = TokenType.UnknownToken;
			IL_19:
			while (!DateTimeParse.IsDigit(this.m_current))
			{
				if (char.IsWhiteSpace(this.m_current))
				{
					for (;;)
					{
						int num = this.Index + 1;
						this.Index = num;
						if (num >= this.Length)
						{
							break;
						}
						this.m_current = (char)(*this.Value[this.Index]);
						if (!char.IsWhiteSpace(this.m_current))
						{
							goto IL_19;
						}
					}
					tokenType = TokenType.EndOfString;
					return;
				}
				dtfi.Tokenize(TokenType.RegularTokenMask, out tokenType, out tokenValue, ref this);
				return;
			}
			tokenValue = (int)(this.m_current - '0');
			int index = this.Index;
			for (;;)
			{
				int num = this.Index + 1;
				this.Index = num;
				if (num >= this.Length)
				{
					break;
				}
				this.m_current = (char)(*this.Value[this.Index]);
				int num2 = (int)(this.m_current - '0');
				if (num2 < 0 || num2 > 9)
				{
					break;
				}
				tokenValue = tokenValue * 10 + num2;
			}
			if (this.Index - index > 8)
			{
				tokenType = TokenType.NumberToken;
				tokenValue = -1;
			}
			else if (this.Index - index < 3)
			{
				tokenType = TokenType.NumberToken;
			}
			else
			{
				tokenType = TokenType.YearNumberToken;
			}
			if (!this.m_checkDigitToken)
			{
				return;
			}
			int index2 = this.Index;
			char current = this.m_current;
			this.Index = index;
			this.m_current = (char)(*this.Value[this.Index]);
			TokenType tokenType2;
			int num3;
			if (dtfi.Tokenize(TokenType.RegularTokenMask, out tokenType2, out num3, ref this))
			{
				tokenType = tokenType2;
				tokenValue = num3;
				return;
			}
			this.Index = index2;
			this.m_current = current;
		}

		// Token: 0x06000B78 RID: 2936 RVA: 0x0002ED6C File Offset: 0x0002CF6C
		internal TokenType GetSeparatorToken(DateTimeFormatInfo dtfi, out int indexBeforeSeparator, out char charBeforeSeparator)
		{
			indexBeforeSeparator = this.Index;
			charBeforeSeparator = this.m_current;
			if (!this.SkipWhiteSpaceCurrent())
			{
				return TokenType.SEP_End;
			}
			TokenType result;
			if (!DateTimeParse.IsDigit(this.m_current))
			{
				int num;
				if (!dtfi.Tokenize(TokenType.SeparatorTokenMask, out result, out num, ref this))
				{
					result = TokenType.SEP_Space;
				}
			}
			else
			{
				result = TokenType.SEP_Space;
			}
			return result;
		}

		// Token: 0x06000B79 RID: 2937 RVA: 0x0002EDC5 File Offset: 0x0002CFC5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal bool MatchSpecifiedWord(string target)
		{
			return this.Index + target.Length <= this.Length && this.m_info.Compare(this.Value.Slice(this.Index, target.Length), target, CompareOptions.IgnoreCase) == 0;
		}

		// Token: 0x06000B7A RID: 2938 RVA: 0x0002EE08 File Offset: 0x0002D008
		internal unsafe bool MatchSpecifiedWords(string target, bool checkWordBoundary, ref int matchLength)
		{
			int num = this.Value.Length - this.Index;
			matchLength = target.Length;
			if (matchLength > num || this.m_info.Compare(this.Value.Slice(this.Index, matchLength), target, CompareOptions.IgnoreCase) != 0)
			{
				int num2 = 0;
				int num3 = this.Index;
				int num4 = target.IndexOfAny(__DTString.WhiteSpaceChecks, num2);
				if (num4 == -1)
				{
					return false;
				}
				for (;;)
				{
					int num5 = num4 - num2;
					if (num3 >= this.Value.Length - num5)
					{
						break;
					}
					if (num5 == 0)
					{
						matchLength--;
					}
					else
					{
						if (!char.IsWhiteSpace((char)(*this.Value[num3 + num5])))
						{
							return false;
						}
						if (this.m_info.CompareOptionIgnoreCase(this.Value.Slice(num3, num5), target.AsSpan(num2, num5)) != 0)
						{
							return false;
						}
						num3 = num3 + num5 + 1;
					}
					num2 = num4 + 1;
					while (num3 < this.Value.Length && char.IsWhiteSpace((char)(*this.Value[num3])))
					{
						num3++;
						matchLength++;
					}
					if ((num4 = target.IndexOfAny(__DTString.WhiteSpaceChecks, num2)) < 0)
					{
						goto Block_8;
					}
				}
				return false;
				Block_8:
				if (num2 < target.Length)
				{
					int num6 = target.Length - num2;
					if (num3 > this.Value.Length - num6)
					{
						return false;
					}
					if (this.m_info.CompareOptionIgnoreCase(this.Value.Slice(num3, num6), target.AsSpan(num2, num6)) != 0)
					{
						return false;
					}
				}
			}
			if (checkWordBoundary)
			{
				int num7 = this.Index + matchLength;
				if (num7 < this.Value.Length && char.IsLetter((char)(*this.Value[num7])))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000B7B RID: 2939 RVA: 0x0002EFA8 File Offset: 0x0002D1A8
		internal bool Match(string str)
		{
			int num = this.Index + 1;
			this.Index = num;
			if (num >= this.Length)
			{
				return false;
			}
			if (str.Length > this.Value.Length - this.Index)
			{
				return false;
			}
			if (this.m_info.Compare(this.Value.Slice(this.Index, str.Length), str, CompareOptions.Ordinal) == 0)
			{
				this.Index += str.Length - 1;
				return true;
			}
			return false;
		}

		// Token: 0x06000B7C RID: 2940 RVA: 0x0002F030 File Offset: 0x0002D230
		internal unsafe bool Match(char ch)
		{
			int num = this.Index + 1;
			this.Index = num;
			if (num >= this.Length)
			{
				return false;
			}
			if (*this.Value[this.Index] == (ushort)ch)
			{
				this.m_current = ch;
				return true;
			}
			this.Index--;
			return false;
		}

		// Token: 0x06000B7D RID: 2941 RVA: 0x0002F088 File Offset: 0x0002D288
		internal int MatchLongestWords(string[] words, ref int maxMatchStrLen)
		{
			int result = -1;
			for (int i = 0; i < words.Length; i++)
			{
				string text = words[i];
				int length = text.Length;
				if (this.MatchSpecifiedWords(text, false, ref length) && length > maxMatchStrLen)
				{
					maxMatchStrLen = length;
					result = i;
				}
			}
			return result;
		}

		// Token: 0x06000B7E RID: 2942 RVA: 0x0002F0C8 File Offset: 0x0002D2C8
		internal unsafe int GetRepeatCount()
		{
			char c = (char)(*this.Value[this.Index]);
			int num = this.Index + 1;
			while (num < this.Length && *this.Value[num] == (ushort)c)
			{
				num++;
			}
			int result = num - this.Index;
			this.Index = num - 1;
			return result;
		}

		// Token: 0x06000B7F RID: 2943 RVA: 0x0002F124 File Offset: 0x0002D324
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal unsafe bool GetNextDigit()
		{
			int num = this.Index + 1;
			this.Index = num;
			return num < this.Length && DateTimeParse.IsDigit((char)(*this.Value[this.Index]));
		}

		// Token: 0x06000B80 RID: 2944 RVA: 0x0002F163 File Offset: 0x0002D363
		internal unsafe char GetChar()
		{
			return (char)(*this.Value[this.Index]);
		}

		// Token: 0x06000B81 RID: 2945 RVA: 0x0002F177 File Offset: 0x0002D377
		internal unsafe int GetDigit()
		{
			return (int)(*this.Value[this.Index] - 48);
		}

		// Token: 0x06000B82 RID: 2946 RVA: 0x0002F18E File Offset: 0x0002D38E
		internal unsafe void SkipWhiteSpaces()
		{
			while (this.Index + 1 < this.Length)
			{
				if (!char.IsWhiteSpace((char)(*this.Value[this.Index + 1])))
				{
					return;
				}
				this.Index++;
			}
		}

		// Token: 0x06000B83 RID: 2947 RVA: 0x0002F1CC File Offset: 0x0002D3CC
		internal unsafe bool SkipWhiteSpaceCurrent()
		{
			if (this.Index >= this.Length)
			{
				return false;
			}
			if (!char.IsWhiteSpace(this.m_current))
			{
				return true;
			}
			do
			{
				int num = this.Index + 1;
				this.Index = num;
				if (num >= this.Length)
				{
					return false;
				}
				this.m_current = (char)(*this.Value[this.Index]);
			}
			while (char.IsWhiteSpace(this.m_current));
			return true;
		}

		// Token: 0x06000B84 RID: 2948 RVA: 0x0002F23C File Offset: 0x0002D43C
		internal unsafe void TrimTail()
		{
			int num = this.Length - 1;
			while (num >= 0 && char.IsWhiteSpace((char)(*this.Value[num])))
			{
				num--;
			}
			this.Value = this.Value.Slice(0, num + 1);
		}

		// Token: 0x06000B85 RID: 2949 RVA: 0x0002F288 File Offset: 0x0002D488
		internal unsafe void RemoveTrailingInQuoteSpaces()
		{
			int num = this.Length - 1;
			if (num <= 1)
			{
				return;
			}
			char c = (char)(*this.Value[num]);
			if ((c == '\'' || c == '"') && char.IsWhiteSpace((char)(*this.Value[num - 1])))
			{
				num--;
				while (num >= 1 && char.IsWhiteSpace((char)(*this.Value[num - 1])))
				{
					num--;
				}
				Span<char> span = new char[num + 1];
				*span[num] = c;
				this.Value.Slice(0, num).CopyTo(span);
				this.Value = span;
			}
		}

		// Token: 0x06000B86 RID: 2950 RVA: 0x0002F330 File Offset: 0x0002D530
		internal unsafe void RemoveLeadingInQuoteSpaces()
		{
			if (this.Length <= 2)
			{
				return;
			}
			int num = 0;
			char c = (char)(*this.Value[num]);
			if (c != '\'')
			{
				if (c != '"')
				{
					return;
				}
			}
			while (num + 1 < this.Length && char.IsWhiteSpace((char)(*this.Value[num + 1])))
			{
				num++;
			}
			if (num != 0)
			{
				Span<char> span = new char[this.Value.Length - num];
				*span[0] = c;
				this.Value.Slice(num + 1).CopyTo(span.Slice(1));
				this.Value = span;
			}
		}

		// Token: 0x06000B87 RID: 2951 RVA: 0x0002F3D8 File Offset: 0x0002D5D8
		internal unsafe DTSubString GetSubString()
		{
			DTSubString dtsubString = default(DTSubString);
			dtsubString.index = this.Index;
			dtsubString.s = this.Value;
			while (this.Index + dtsubString.length < this.Length)
			{
				char c = (char)(*this.Value[this.Index + dtsubString.length]);
				DTSubStringType dtsubStringType;
				if (c >= '0' && c <= '9')
				{
					dtsubStringType = DTSubStringType.Number;
				}
				else
				{
					dtsubStringType = DTSubStringType.Other;
				}
				if (dtsubString.length == 0)
				{
					dtsubString.type = dtsubStringType;
				}
				else if (dtsubString.type != dtsubStringType)
				{
					break;
				}
				dtsubString.length++;
				if (dtsubStringType != DTSubStringType.Number)
				{
					break;
				}
				if (dtsubString.length > 8)
				{
					dtsubString.type = DTSubStringType.Invalid;
					return dtsubString;
				}
				int num = (int)(c - '0');
				dtsubString.value = dtsubString.value * 10 + num;
			}
			if (dtsubString.length == 0)
			{
				dtsubString.type = DTSubStringType.End;
				return dtsubString;
			}
			return dtsubString;
		}

		// Token: 0x06000B88 RID: 2952 RVA: 0x0002F4B3 File Offset: 0x0002D6B3
		internal unsafe void ConsumeSubString(DTSubString sub)
		{
			this.Index = sub.index + sub.length;
			if (this.Index < this.Length)
			{
				this.m_current = (char)(*this.Value[this.Index]);
			}
		}

		// Token: 0x06000B89 RID: 2953 RVA: 0x0002F4EE File Offset: 0x0002D6EE
		// Note: this type is marked as 'beforefieldinit'.
		static __DTString()
		{
		}

		// Token: 0x04001141 RID: 4417
		internal ReadOnlySpan<char> Value;

		// Token: 0x04001142 RID: 4418
		internal int Index;

		// Token: 0x04001143 RID: 4419
		internal char m_current;

		// Token: 0x04001144 RID: 4420
		private CompareInfo m_info;

		// Token: 0x04001145 RID: 4421
		private bool m_checkDigitToken;

		// Token: 0x04001146 RID: 4422
		private static readonly char[] WhiteSpaceChecks = new char[]
		{
			' ',
			'\u00a0'
		};
	}
}

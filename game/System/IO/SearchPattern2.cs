using System;

namespace System.IO
{
	// Token: 0x02000525 RID: 1317
	internal class SearchPattern2
	{
		// Token: 0x06002A7E RID: 10878 RVA: 0x000929E9 File Offset: 0x00090BE9
		public SearchPattern2(string pattern) : this(pattern, false)
		{
		}

		// Token: 0x06002A7F RID: 10879 RVA: 0x000929F3 File Offset: 0x00090BF3
		public SearchPattern2(string pattern, bool ignore)
		{
			this.ignore = ignore;
			this.pattern = pattern;
			this.Compile(pattern);
		}

		// Token: 0x06002A80 RID: 10880 RVA: 0x00092A10 File Offset: 0x00090C10
		public bool IsMatch(string text, bool ignorecase)
		{
			if (!this.hasWildcard && string.Compare(this.pattern, text, ignorecase) == 0)
			{
				return true;
			}
			string fileName = Path.GetFileName(text);
			if (!this.hasWildcard)
			{
				return string.Compare(this.pattern, fileName, ignorecase) == 0;
			}
			return this.Match(this.ops, fileName, 0);
		}

		// Token: 0x06002A81 RID: 10881 RVA: 0x00092A67 File Offset: 0x00090C67
		public bool IsMatch(string text)
		{
			return this.IsMatch(text, this.ignore);
		}

		// Token: 0x170008A8 RID: 2216
		// (get) Token: 0x06002A82 RID: 10882 RVA: 0x00092A76 File Offset: 0x00090C76
		public bool HasWildcard
		{
			get
			{
				return this.hasWildcard;
			}
		}

		// Token: 0x06002A83 RID: 10883 RVA: 0x00092A80 File Offset: 0x00090C80
		private void Compile(string pattern)
		{
			if (pattern == null || pattern.IndexOfAny(SearchPattern2.InvalidChars) >= 0)
			{
				throw new ArgumentException("Invalid search pattern: '" + pattern + "'");
			}
			if (pattern == "*")
			{
				this.ops = new SearchPattern2.Op(SearchPattern2.OpCode.True);
				this.hasWildcard = true;
				return;
			}
			this.ops = null;
			int i = 0;
			SearchPattern2.Op op = null;
			while (i < pattern.Length)
			{
				char c = pattern[i];
				SearchPattern2.Op op2;
				if (c != '*')
				{
					if (c == '?')
					{
						op2 = new SearchPattern2.Op(SearchPattern2.OpCode.AnyChar);
						i++;
						this.hasWildcard = true;
					}
					else
					{
						op2 = new SearchPattern2.Op(SearchPattern2.OpCode.ExactString);
						int num = pattern.IndexOfAny(SearchPattern2.WildcardChars, i);
						if (num < 0)
						{
							num = pattern.Length;
						}
						op2.Argument = pattern.Substring(i, num - i);
						if (this.ignore)
						{
							op2.Argument = op2.Argument.ToLower();
						}
						i = num;
					}
				}
				else
				{
					op2 = new SearchPattern2.Op(SearchPattern2.OpCode.AnyString);
					i++;
					this.hasWildcard = true;
				}
				if (op == null)
				{
					this.ops = op2;
				}
				else
				{
					op.Next = op2;
				}
				op = op2;
			}
			if (op == null)
			{
				this.ops = new SearchPattern2.Op(SearchPattern2.OpCode.End);
				return;
			}
			op.Next = new SearchPattern2.Op(SearchPattern2.OpCode.End);
		}

		// Token: 0x06002A84 RID: 10884 RVA: 0x00092BAC File Offset: 0x00090DAC
		private bool Match(SearchPattern2.Op op, string text, int ptr)
		{
			while (op != null)
			{
				switch (op.Code)
				{
				case SearchPattern2.OpCode.ExactString:
				{
					int length = op.Argument.Length;
					if (ptr + length > text.Length)
					{
						return false;
					}
					string text2 = text.Substring(ptr, length);
					if (this.ignore)
					{
						text2 = text2.ToLower();
					}
					if (text2 != op.Argument)
					{
						return false;
					}
					ptr += length;
					break;
				}
				case SearchPattern2.OpCode.AnyChar:
					if (++ptr > text.Length)
					{
						return false;
					}
					break;
				case SearchPattern2.OpCode.AnyString:
					while (ptr <= text.Length)
					{
						if (this.Match(op.Next, text, ptr))
						{
							return true;
						}
						ptr++;
					}
					return false;
				case SearchPattern2.OpCode.End:
					return ptr == text.Length;
				case SearchPattern2.OpCode.True:
					return true;
				}
				op = op.Next;
			}
			return true;
		}

		// Token: 0x06002A85 RID: 10885 RVA: 0x00092C7C File Offset: 0x00090E7C
		// Note: this type is marked as 'beforefieldinit'.
		static SearchPattern2()
		{
		}

		// Token: 0x040016EF RID: 5871
		private SearchPattern2.Op ops;

		// Token: 0x040016F0 RID: 5872
		private bool ignore;

		// Token: 0x040016F1 RID: 5873
		private bool hasWildcard;

		// Token: 0x040016F2 RID: 5874
		private string pattern;

		// Token: 0x040016F3 RID: 5875
		internal static readonly char[] WildcardChars = new char[]
		{
			'*',
			'?'
		};

		// Token: 0x040016F4 RID: 5876
		internal static readonly char[] InvalidChars = new char[]
		{
			Path.DirectorySeparatorChar,
			Path.AltDirectorySeparatorChar
		};

		// Token: 0x02000526 RID: 1318
		private class Op
		{
			// Token: 0x06002A86 RID: 10886 RVA: 0x00092CAE File Offset: 0x00090EAE
			public Op(SearchPattern2.OpCode code)
			{
				this.Code = code;
				this.Argument = null;
				this.Next = null;
			}

			// Token: 0x040016F5 RID: 5877
			public SearchPattern2.OpCode Code;

			// Token: 0x040016F6 RID: 5878
			public string Argument;

			// Token: 0x040016F7 RID: 5879
			public SearchPattern2.Op Next;
		}

		// Token: 0x02000527 RID: 1319
		private enum OpCode
		{
			// Token: 0x040016F9 RID: 5881
			ExactString,
			// Token: 0x040016FA RID: 5882
			AnyChar,
			// Token: 0x040016FB RID: 5883
			AnyString,
			// Token: 0x040016FC RID: 5884
			End,
			// Token: 0x040016FD RID: 5885
			True
		}
	}
}

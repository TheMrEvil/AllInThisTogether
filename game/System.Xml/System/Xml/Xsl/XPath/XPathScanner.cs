using System;

namespace System.Xml.Xsl.XPath
{
	// Token: 0x0200042F RID: 1071
	internal sealed class XPathScanner
	{
		// Token: 0x06002AAD RID: 10925 RVA: 0x001018A2 File Offset: 0x000FFAA2
		public XPathScanner(string xpathExpr) : this(xpathExpr, 0)
		{
		}

		// Token: 0x06002AAE RID: 10926 RVA: 0x001018AC File Offset: 0x000FFAAC
		public XPathScanner(string xpathExpr, int startFrom)
		{
			this.xpathExpr = xpathExpr;
			this.kind = LexKind.Unknown;
			this.SetSourceIndex(startFrom);
			this.NextLex();
		}

		// Token: 0x170007F6 RID: 2038
		// (get) Token: 0x06002AAF RID: 10927 RVA: 0x001018DA File Offset: 0x000FFADA
		public string Source
		{
			get
			{
				return this.xpathExpr;
			}
		}

		// Token: 0x170007F7 RID: 2039
		// (get) Token: 0x06002AB0 RID: 10928 RVA: 0x001018E2 File Offset: 0x000FFAE2
		public LexKind Kind
		{
			get
			{
				return this.kind;
			}
		}

		// Token: 0x170007F8 RID: 2040
		// (get) Token: 0x06002AB1 RID: 10929 RVA: 0x001018EA File Offset: 0x000FFAEA
		public int LexStart
		{
			get
			{
				return this.lexStart;
			}
		}

		// Token: 0x170007F9 RID: 2041
		// (get) Token: 0x06002AB2 RID: 10930 RVA: 0x001018F2 File Offset: 0x000FFAF2
		public int LexSize
		{
			get
			{
				return this.curIndex - this.lexStart;
			}
		}

		// Token: 0x170007FA RID: 2042
		// (get) Token: 0x06002AB3 RID: 10931 RVA: 0x00101901 File Offset: 0x000FFB01
		public int PrevLexEnd
		{
			get
			{
				return this.prevLexEnd;
			}
		}

		// Token: 0x06002AB4 RID: 10932 RVA: 0x00101909 File Offset: 0x000FFB09
		private void SetSourceIndex(int index)
		{
			this.curIndex = index - 1;
			this.NextChar();
		}

		// Token: 0x06002AB5 RID: 10933 RVA: 0x0010191C File Offset: 0x000FFB1C
		private void NextChar()
		{
			this.curIndex++;
			if (this.curIndex < this.xpathExpr.Length)
			{
				this.curChar = this.xpathExpr[this.curIndex];
				return;
			}
			this.curChar = '\0';
		}

		// Token: 0x170007FB RID: 2043
		// (get) Token: 0x06002AB6 RID: 10934 RVA: 0x00101969 File Offset: 0x000FFB69
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x170007FC RID: 2044
		// (get) Token: 0x06002AB7 RID: 10935 RVA: 0x00101971 File Offset: 0x000FFB71
		public string Prefix
		{
			get
			{
				return this.prefix;
			}
		}

		// Token: 0x170007FD RID: 2045
		// (get) Token: 0x06002AB8 RID: 10936 RVA: 0x00101979 File Offset: 0x000FFB79
		public string RawValue
		{
			get
			{
				if (this.kind == LexKind.Eof)
				{
					return this.LexKindToString(this.kind);
				}
				return this.xpathExpr.Substring(this.lexStart, this.curIndex - this.lexStart);
			}
		}

		// Token: 0x170007FE RID: 2046
		// (get) Token: 0x06002AB9 RID: 10937 RVA: 0x001019B0 File Offset: 0x000FFBB0
		public string StringValue
		{
			get
			{
				return this.stringValue;
			}
		}

		// Token: 0x170007FF RID: 2047
		// (get) Token: 0x06002ABA RID: 10938 RVA: 0x001019B8 File Offset: 0x000FFBB8
		public bool CanBeFunction
		{
			get
			{
				return this.canBeFunction;
			}
		}

		// Token: 0x17000800 RID: 2048
		// (get) Token: 0x06002ABB RID: 10939 RVA: 0x001019C0 File Offset: 0x000FFBC0
		public XPathAxis Axis
		{
			get
			{
				return this.axis;
			}
		}

		// Token: 0x06002ABC RID: 10940 RVA: 0x001019C8 File Offset: 0x000FFBC8
		private void SkipSpace()
		{
			while (this.xmlCharType.IsWhiteSpace(this.curChar))
			{
				this.NextChar();
			}
		}

		// Token: 0x06002ABD RID: 10941 RVA: 0x000D314B File Offset: 0x000D134B
		private static bool IsAsciiDigit(char ch)
		{
			return ch - '0' <= '\t';
		}

		// Token: 0x06002ABE RID: 10942 RVA: 0x001019E8 File Offset: 0x000FFBE8
		public void NextLex()
		{
			this.prevLexEnd = this.curIndex;
			this.prevKind = this.kind;
			this.SkipSpace();
			this.lexStart = this.curIndex;
			char c = this.curChar;
			if (c <= '[')
			{
				if (c != '\0')
				{
					switch (c)
					{
					case '!':
						this.NextChar();
						if (this.curChar == '=')
						{
							this.kind = LexKind.Ne;
							this.NextChar();
							return;
						}
						this.kind = LexKind.Unknown;
						return;
					case '"':
					case '\'':
						this.kind = LexKind.String;
						this.ScanString();
						return;
					case '#':
					case '%':
					case '&':
					case ';':
					case '?':
						goto IL_27C;
					case '$':
					case '(':
					case ')':
					case ',':
					case '@':
						goto IL_F2;
					case '*':
						this.kind = LexKind.Star;
						this.NextChar();
						this.CheckOperator(true);
						return;
					case '+':
						this.kind = LexKind.Plus;
						this.NextChar();
						return;
					case '-':
						this.kind = LexKind.Minus;
						this.NextChar();
						return;
					case '.':
						this.NextChar();
						if (this.curChar == '.')
						{
							this.kind = LexKind.DotDot;
							this.NextChar();
							return;
						}
						if (!XPathScanner.IsAsciiDigit(this.curChar))
						{
							this.kind = LexKind.Dot;
							return;
						}
						this.SetSourceIndex(this.lexStart);
						break;
					case '/':
						this.NextChar();
						if (this.curChar == '/')
						{
							this.kind = LexKind.SlashSlash;
							this.NextChar();
							return;
						}
						this.kind = LexKind.Slash;
						return;
					case '0':
					case '1':
					case '2':
					case '3':
					case '4':
					case '5':
					case '6':
					case '7':
					case '8':
					case '9':
						break;
					case ':':
						this.NextChar();
						if (this.curChar == ':')
						{
							this.kind = LexKind.ColonColon;
							this.NextChar();
							return;
						}
						this.kind = LexKind.Unknown;
						return;
					case '<':
						this.NextChar();
						if (this.curChar == '=')
						{
							this.kind = LexKind.Le;
							this.NextChar();
							return;
						}
						this.kind = LexKind.Lt;
						return;
					case '=':
						this.kind = LexKind.Eq;
						this.NextChar();
						return;
					case '>':
						this.NextChar();
						if (this.curChar == '=')
						{
							this.kind = LexKind.Ge;
							this.NextChar();
							return;
						}
						this.kind = LexKind.Gt;
						return;
					default:
						if (c != '[')
						{
							goto IL_27C;
						}
						goto IL_F2;
					}
					this.kind = LexKind.Number;
					this.ScanNumber();
					return;
				}
				this.kind = LexKind.Eof;
				return;
			}
			else if (c != ']')
			{
				if (c == '|')
				{
					this.kind = LexKind.Union;
					this.NextChar();
					return;
				}
				if (c != '}')
				{
					goto IL_27C;
				}
			}
			IL_F2:
			this.kind = (LexKind)this.curChar;
			this.NextChar();
			return;
			IL_27C:
			if (this.xmlCharType.IsStartNCNameSingleChar(this.curChar))
			{
				this.kind = LexKind.Name;
				this.name = this.ScanNCName();
				this.prefix = string.Empty;
				this.canBeFunction = false;
				this.axis = XPathAxis.Unknown;
				bool flag = false;
				int sourceIndex = this.curIndex;
				if (this.curChar == ':')
				{
					this.NextChar();
					if (this.curChar == ':')
					{
						this.NextChar();
						flag = true;
						this.SetSourceIndex(sourceIndex);
					}
					else if (this.curChar == '*')
					{
						this.NextChar();
						this.prefix = this.name;
						this.name = "*";
					}
					else if (this.xmlCharType.IsStartNCNameSingleChar(this.curChar))
					{
						this.prefix = this.name;
						this.name = this.ScanNCName();
						sourceIndex = this.curIndex;
						this.SkipSpace();
						this.canBeFunction = (this.curChar == '(');
						this.SetSourceIndex(sourceIndex);
					}
					else
					{
						this.SetSourceIndex(sourceIndex);
					}
				}
				else
				{
					this.SkipSpace();
					if (this.curChar == ':')
					{
						this.NextChar();
						if (this.curChar == ':')
						{
							this.NextChar();
							flag = true;
						}
						this.SetSourceIndex(sourceIndex);
					}
					else
					{
						this.canBeFunction = (this.curChar == '(');
					}
				}
				if (!this.CheckOperator(false) && flag)
				{
					this.axis = this.CheckAxis();
					return;
				}
			}
			else
			{
				this.kind = LexKind.Unknown;
				this.NextChar();
			}
		}

		// Token: 0x06002ABF RID: 10943 RVA: 0x00101DE0 File Offset: 0x000FFFE0
		private bool CheckOperator(bool star)
		{
			LexKind lexKind;
			if (star)
			{
				lexKind = LexKind.Multiply;
			}
			else
			{
				if (this.prefix.Length != 0 || this.name.Length > 3)
				{
					return false;
				}
				string a = this.name;
				if (!(a == "or"))
				{
					if (!(a == "and"))
					{
						if (!(a == "div"))
						{
							if (!(a == "mod"))
							{
								return false;
							}
							lexKind = LexKind.Modulo;
						}
						else
						{
							lexKind = LexKind.Divide;
						}
					}
					else
					{
						lexKind = LexKind.And;
					}
				}
				else
				{
					lexKind = LexKind.Or;
				}
			}
			if (this.prevKind <= LexKind.Union)
			{
				return false;
			}
			LexKind lexKind2 = this.prevKind;
			if (lexKind2 <= LexKind.LParens)
			{
				if (lexKind2 - LexKind.ColonColon > 1 && lexKind2 != LexKind.Dollar && lexKind2 != LexKind.LParens)
				{
					goto IL_BE;
				}
			}
			else if (lexKind2 <= LexKind.Slash)
			{
				if (lexKind2 != LexKind.Comma && lexKind2 != LexKind.Slash)
				{
					goto IL_BE;
				}
			}
			else if (lexKind2 != LexKind.At && lexKind2 != LexKind.LBracket)
			{
				goto IL_BE;
			}
			return false;
			IL_BE:
			this.kind = lexKind;
			return true;
		}

		// Token: 0x06002AC0 RID: 10944 RVA: 0x00101EB4 File Offset: 0x001000B4
		private XPathAxis CheckAxis()
		{
			this.kind = LexKind.Axis;
			string text = this.name;
			uint num = <PrivateImplementationDetails>.ComputeStringHash(text);
			if (num <= 2535512472U)
			{
				if (num <= 1047347951U)
				{
					if (num != 21436113U)
					{
						if (num != 510973315U)
						{
							if (num == 1047347951U)
							{
								if (text == "attribute")
								{
									return XPathAxis.Attribute;
								}
							}
						}
						else if (text == "ancestor-or-self")
						{
							return XPathAxis.AncestorOrSelf;
						}
					}
					else if (text == "preceding-sibling")
					{
						return XPathAxis.PrecedingSibling;
					}
				}
				else if (num != 1683726967U)
				{
					if (num != 2452897184U)
					{
						if (num == 2535512472U)
						{
							if (text == "following")
							{
								return XPathAxis.Following;
							}
						}
					}
					else if (text == "ancestor")
					{
						return XPathAxis.Ancestor;
					}
				}
				else if (text == "self")
				{
					return XPathAxis.Self;
				}
			}
			else if (num <= 3726896370U)
			{
				if (num != 2944295921U)
				{
					if (num != 3402529440U)
					{
						if (num == 3726896370U)
						{
							if (text == "preceding")
							{
								return XPathAxis.Preceding;
							}
						}
					}
					else if (text == "namespace")
					{
						return XPathAxis.Namespace;
					}
				}
				else if (text == "descendant-or-self")
				{
					return XPathAxis.DescendantOrSelf;
				}
			}
			else if (num <= 3939368189U)
			{
				if (num != 3852476509U)
				{
					if (num == 3939368189U)
					{
						if (text == "parent")
						{
							return XPathAxis.Parent;
						}
					}
				}
				else if (text == "child")
				{
					return XPathAxis.Child;
				}
			}
			else if (num != 3998959382U)
			{
				if (num == 4042989175U)
				{
					if (text == "following-sibling")
					{
						return XPathAxis.FollowingSibling;
					}
				}
			}
			else if (text == "descendant")
			{
				return XPathAxis.Descendant;
			}
			this.kind = LexKind.Name;
			return XPathAxis.Unknown;
		}

		// Token: 0x06002AC1 RID: 10945 RVA: 0x001020A4 File Offset: 0x001002A4
		private void ScanNumber()
		{
			while (XPathScanner.IsAsciiDigit(this.curChar))
			{
				this.NextChar();
			}
			if (this.curChar == '.')
			{
				this.NextChar();
				while (XPathScanner.IsAsciiDigit(this.curChar))
				{
					this.NextChar();
				}
			}
			if (((int)this.curChar & -33) == 69)
			{
				this.NextChar();
				if (this.curChar == '+' || this.curChar == '-')
				{
					this.NextChar();
				}
				while (XPathScanner.IsAsciiDigit(this.curChar))
				{
					this.NextChar();
				}
				throw this.CreateException("Scientific notation is not allowed.", Array.Empty<string>());
			}
		}

		// Token: 0x06002AC2 RID: 10946 RVA: 0x00102140 File Offset: 0x00100340
		private void ScanString()
		{
			int num = this.curIndex + 1;
			int num2 = this.xpathExpr.IndexOf(this.curChar, num);
			if (num2 < 0)
			{
				this.SetSourceIndex(this.xpathExpr.Length);
				throw this.CreateException("String literal was not closed.", Array.Empty<string>());
			}
			this.stringValue = this.xpathExpr.Substring(num, num2 - num);
			this.SetSourceIndex(num2 + 1);
		}

		// Token: 0x06002AC3 RID: 10947 RVA: 0x001021B0 File Offset: 0x001003B0
		private string ScanNCName()
		{
			int num = this.curIndex;
			while (this.xmlCharType.IsNCNameSingleChar(this.curChar))
			{
				this.NextChar();
			}
			return this.xpathExpr.Substring(num, this.curIndex - num);
		}

		// Token: 0x06002AC4 RID: 10948 RVA: 0x001021F3 File Offset: 0x001003F3
		public void PassToken(LexKind t)
		{
			this.CheckToken(t);
			this.NextLex();
		}

		// Token: 0x06002AC5 RID: 10949 RVA: 0x00102204 File Offset: 0x00100404
		public void CheckToken(LexKind t)
		{
			if (this.kind == t)
			{
				return;
			}
			if (t == LexKind.Eof)
			{
				throw this.CreateException("Expected end of the expression, found '{0}'.", new string[]
				{
					this.RawValue
				});
			}
			throw this.CreateException("Expected token '{0}', found '{1}'.", new string[]
			{
				this.LexKindToString(t),
				this.RawValue
			});
		}

		// Token: 0x06002AC6 RID: 10950 RVA: 0x0010225F File Offset: 0x0010045F
		private string LexKindToString(LexKind t)
		{
			if (LexKind.Eof < t)
			{
				return new string((char)t, 1);
			}
			switch (t)
			{
			case LexKind.Name:
				return "<name>";
			case LexKind.String:
				return "<string literal>";
			case LexKind.Eof:
				return "<eof>";
			default:
				return string.Empty;
			}
		}

		// Token: 0x06002AC7 RID: 10951 RVA: 0x0010229D File Offset: 0x0010049D
		public XPathCompileException CreateException(string resId, params string[] args)
		{
			return new XPathCompileException(this.xpathExpr, this.lexStart, this.curIndex, resId, args);
		}

		// Token: 0x0400219F RID: 8607
		private string xpathExpr;

		// Token: 0x040021A0 RID: 8608
		private int curIndex;

		// Token: 0x040021A1 RID: 8609
		private char curChar;

		// Token: 0x040021A2 RID: 8610
		private LexKind kind;

		// Token: 0x040021A3 RID: 8611
		private string name;

		// Token: 0x040021A4 RID: 8612
		private string prefix;

		// Token: 0x040021A5 RID: 8613
		private string stringValue;

		// Token: 0x040021A6 RID: 8614
		private bool canBeFunction;

		// Token: 0x040021A7 RID: 8615
		private int lexStart;

		// Token: 0x040021A8 RID: 8616
		private int prevLexEnd;

		// Token: 0x040021A9 RID: 8617
		private LexKind prevKind;

		// Token: 0x040021AA RID: 8618
		private XPathAxis axis;

		// Token: 0x040021AB RID: 8619
		private XmlCharType xmlCharType = XmlCharType.Instance;
	}
}

using System;
using System.Globalization;
using System.Xml;
using System.Xml.XPath;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x02000661 RID: 1633
	internal sealed class XPathScanner
	{
		// Token: 0x0600422F RID: 16943 RVA: 0x0016A334 File Offset: 0x00168534
		public XPathScanner(string xpathExpr)
		{
			if (xpathExpr == null)
			{
				throw XPathException.Create("'{0}' is an invalid expression.", string.Empty);
			}
			this._xpathExpr = xpathExpr;
			this.NextChar();
			this.NextLex();
		}

		// Token: 0x17000C9F RID: 3231
		// (get) Token: 0x06004230 RID: 16944 RVA: 0x0016A389 File Offset: 0x00168589
		public string SourceText
		{
			get
			{
				return this._xpathExpr;
			}
		}

		// Token: 0x17000CA0 RID: 3232
		// (get) Token: 0x06004231 RID: 16945 RVA: 0x0016A391 File Offset: 0x00168591
		private char CurrentChar
		{
			get
			{
				return this._currentChar;
			}
		}

		// Token: 0x06004232 RID: 16946 RVA: 0x0016A39C File Offset: 0x0016859C
		private bool NextChar()
		{
			if (this._xpathExprIndex < this._xpathExpr.Length)
			{
				string xpathExpr = this._xpathExpr;
				int xpathExprIndex = this._xpathExprIndex;
				this._xpathExprIndex = xpathExprIndex + 1;
				this._currentChar = xpathExpr[xpathExprIndex];
				return true;
			}
			this._currentChar = '\0';
			return false;
		}

		// Token: 0x17000CA1 RID: 3233
		// (get) Token: 0x06004233 RID: 16947 RVA: 0x0016A3E8 File Offset: 0x001685E8
		public XPathScanner.LexKind Kind
		{
			get
			{
				return this._kind;
			}
		}

		// Token: 0x17000CA2 RID: 3234
		// (get) Token: 0x06004234 RID: 16948 RVA: 0x0016A3F0 File Offset: 0x001685F0
		public string Name
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x17000CA3 RID: 3235
		// (get) Token: 0x06004235 RID: 16949 RVA: 0x0016A3F8 File Offset: 0x001685F8
		public string Prefix
		{
			get
			{
				return this._prefix;
			}
		}

		// Token: 0x17000CA4 RID: 3236
		// (get) Token: 0x06004236 RID: 16950 RVA: 0x0016A400 File Offset: 0x00168600
		public string StringValue
		{
			get
			{
				return this._stringValue;
			}
		}

		// Token: 0x17000CA5 RID: 3237
		// (get) Token: 0x06004237 RID: 16951 RVA: 0x0016A408 File Offset: 0x00168608
		public double NumberValue
		{
			get
			{
				return this._numberValue;
			}
		}

		// Token: 0x17000CA6 RID: 3238
		// (get) Token: 0x06004238 RID: 16952 RVA: 0x0016A410 File Offset: 0x00168610
		public bool CanBeFunction
		{
			get
			{
				return this._canBeFunction;
			}
		}

		// Token: 0x06004239 RID: 16953 RVA: 0x0016A418 File Offset: 0x00168618
		private void SkipSpace()
		{
			while (this._xmlCharType.IsWhiteSpace(this.CurrentChar) && this.NextChar())
			{
			}
		}

		// Token: 0x0600423A RID: 16954 RVA: 0x0016A438 File Offset: 0x00168638
		public bool NextLex()
		{
			this.SkipSpace();
			char currentChar = this.CurrentChar;
			if (currentChar <= '@')
			{
				if (currentChar == '\0')
				{
					this._kind = XPathScanner.LexKind.Eof;
					return false;
				}
				switch (currentChar)
				{
				case '!':
					this._kind = XPathScanner.LexKind.Bang;
					this.NextChar();
					if (this.CurrentChar == '=')
					{
						this._kind = XPathScanner.LexKind.Ne;
						this.NextChar();
						return true;
					}
					return true;
				case '"':
				case '\'':
					this._kind = XPathScanner.LexKind.String;
					this._stringValue = this.ScanString();
					return true;
				case '#':
				case '$':
				case '(':
				case ')':
				case '*':
				case '+':
				case ',':
				case '-':
				case '=':
				case '@':
					break;
				case '%':
				case '&':
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
				case ':':
				case ';':
				case '?':
					goto IL_21D;
				case '.':
					this._kind = XPathScanner.LexKind.Dot;
					this.NextChar();
					if (this.CurrentChar == '.')
					{
						this._kind = XPathScanner.LexKind.DotDot;
						this.NextChar();
						return true;
					}
					if (XmlCharType.IsDigit(this.CurrentChar))
					{
						this._kind = XPathScanner.LexKind.Number;
						this._numberValue = this.ScanFraction();
						return true;
					}
					return true;
				case '/':
					this._kind = XPathScanner.LexKind.Slash;
					this.NextChar();
					if (this.CurrentChar == '/')
					{
						this._kind = XPathScanner.LexKind.SlashSlash;
						this.NextChar();
						return true;
					}
					return true;
				case '<':
					this._kind = XPathScanner.LexKind.Lt;
					this.NextChar();
					if (this.CurrentChar == '=')
					{
						this._kind = XPathScanner.LexKind.Le;
						this.NextChar();
						return true;
					}
					return true;
				case '>':
					this._kind = XPathScanner.LexKind.Gt;
					this.NextChar();
					if (this.CurrentChar == '=')
					{
						this._kind = XPathScanner.LexKind.Ge;
						this.NextChar();
						return true;
					}
					return true;
				default:
					goto IL_21D;
				}
			}
			else if (currentChar != '[' && currentChar != ']' && currentChar != '|')
			{
				goto IL_21D;
			}
			this._kind = (XPathScanner.LexKind)Convert.ToInt32(this.CurrentChar, CultureInfo.InvariantCulture);
			this.NextChar();
			return true;
			IL_21D:
			if (XmlCharType.IsDigit(this.CurrentChar))
			{
				this._kind = XPathScanner.LexKind.Number;
				this._numberValue = this.ScanNumber();
			}
			else
			{
				if (!this._xmlCharType.IsStartNCNameSingleChar(this.CurrentChar))
				{
					throw XPathException.Create("'{0}' has an invalid token.", this.SourceText);
				}
				this._kind = XPathScanner.LexKind.Name;
				this._name = this.ScanName();
				this._prefix = string.Empty;
				if (this.CurrentChar == ':')
				{
					this.NextChar();
					if (this.CurrentChar == ':')
					{
						this.NextChar();
						this._kind = XPathScanner.LexKind.Axe;
					}
					else
					{
						this._prefix = this._name;
						if (this.CurrentChar == '*')
						{
							this.NextChar();
							this._name = "*";
						}
						else
						{
							if (!this._xmlCharType.IsStartNCNameSingleChar(this.CurrentChar))
							{
								throw XPathException.Create("'{0}' has an invalid qualified name.", this.SourceText);
							}
							this._name = this.ScanName();
						}
					}
				}
				else
				{
					this.SkipSpace();
					if (this.CurrentChar == ':')
					{
						this.NextChar();
						if (this.CurrentChar != ':')
						{
							throw XPathException.Create("'{0}' has an invalid qualified name.", this.SourceText);
						}
						this.NextChar();
						this._kind = XPathScanner.LexKind.Axe;
					}
				}
				this.SkipSpace();
				this._canBeFunction = (this.CurrentChar == '(');
			}
			return true;
		}

		// Token: 0x0600423B RID: 16955 RVA: 0x0016A7B8 File Offset: 0x001689B8
		private double ScanNumber()
		{
			int startIndex = this._xpathExprIndex - 1;
			int num = 0;
			while (XmlCharType.IsDigit(this.CurrentChar))
			{
				this.NextChar();
				num++;
			}
			if (this.CurrentChar == '.')
			{
				this.NextChar();
				num++;
				while (XmlCharType.IsDigit(this.CurrentChar))
				{
					this.NextChar();
					num++;
				}
			}
			return XmlConvert.ToXPathDouble(this._xpathExpr.Substring(startIndex, num));
		}

		// Token: 0x0600423C RID: 16956 RVA: 0x0016A82C File Offset: 0x00168A2C
		private double ScanFraction()
		{
			int startIndex = this._xpathExprIndex - 2;
			int num = 1;
			while (XmlCharType.IsDigit(this.CurrentChar))
			{
				this.NextChar();
				num++;
			}
			return XmlConvert.ToXPathDouble(this._xpathExpr.Substring(startIndex, num));
		}

		// Token: 0x0600423D RID: 16957 RVA: 0x0016A870 File Offset: 0x00168A70
		private string ScanString()
		{
			char currentChar = this.CurrentChar;
			this.NextChar();
			int startIndex = this._xpathExprIndex - 1;
			int num = 0;
			while (this.CurrentChar != currentChar)
			{
				if (!this.NextChar())
				{
					throw XPathException.Create("This is an unclosed string.");
				}
				num++;
			}
			this.NextChar();
			return this._xpathExpr.Substring(startIndex, num);
		}

		// Token: 0x0600423E RID: 16958 RVA: 0x0016A8CC File Offset: 0x00168ACC
		private string ScanName()
		{
			int startIndex = this._xpathExprIndex - 1;
			int num = 0;
			while (this._xmlCharType.IsNCNameSingleChar(this.CurrentChar))
			{
				this.NextChar();
				num++;
			}
			return this._xpathExpr.Substring(startIndex, num);
		}

		// Token: 0x04002EC2 RID: 11970
		private string _xpathExpr;

		// Token: 0x04002EC3 RID: 11971
		private int _xpathExprIndex;

		// Token: 0x04002EC4 RID: 11972
		private XPathScanner.LexKind _kind;

		// Token: 0x04002EC5 RID: 11973
		private char _currentChar;

		// Token: 0x04002EC6 RID: 11974
		private string _name;

		// Token: 0x04002EC7 RID: 11975
		private string _prefix;

		// Token: 0x04002EC8 RID: 11976
		private string _stringValue;

		// Token: 0x04002EC9 RID: 11977
		private double _numberValue = double.NaN;

		// Token: 0x04002ECA RID: 11978
		private bool _canBeFunction;

		// Token: 0x04002ECB RID: 11979
		private XmlCharType _xmlCharType = XmlCharType.Instance;

		// Token: 0x02000662 RID: 1634
		public enum LexKind
		{
			// Token: 0x04002ECD RID: 11981
			Comma = 44,
			// Token: 0x04002ECE RID: 11982
			Slash = 47,
			// Token: 0x04002ECF RID: 11983
			At = 64,
			// Token: 0x04002ED0 RID: 11984
			Dot = 46,
			// Token: 0x04002ED1 RID: 11985
			LParens = 40,
			// Token: 0x04002ED2 RID: 11986
			RParens,
			// Token: 0x04002ED3 RID: 11987
			LBracket = 91,
			// Token: 0x04002ED4 RID: 11988
			RBracket = 93,
			// Token: 0x04002ED5 RID: 11989
			Star = 42,
			// Token: 0x04002ED6 RID: 11990
			Plus,
			// Token: 0x04002ED7 RID: 11991
			Minus = 45,
			// Token: 0x04002ED8 RID: 11992
			Eq = 61,
			// Token: 0x04002ED9 RID: 11993
			Lt = 60,
			// Token: 0x04002EDA RID: 11994
			Gt = 62,
			// Token: 0x04002EDB RID: 11995
			Bang = 33,
			// Token: 0x04002EDC RID: 11996
			Dollar = 36,
			// Token: 0x04002EDD RID: 11997
			Apos = 39,
			// Token: 0x04002EDE RID: 11998
			Quote = 34,
			// Token: 0x04002EDF RID: 11999
			Union = 124,
			// Token: 0x04002EE0 RID: 12000
			Ne = 78,
			// Token: 0x04002EE1 RID: 12001
			Le = 76,
			// Token: 0x04002EE2 RID: 12002
			Ge = 71,
			// Token: 0x04002EE3 RID: 12003
			And = 65,
			// Token: 0x04002EE4 RID: 12004
			Or = 79,
			// Token: 0x04002EE5 RID: 12005
			DotDot = 68,
			// Token: 0x04002EE6 RID: 12006
			SlashSlash = 83,
			// Token: 0x04002EE7 RID: 12007
			Name = 110,
			// Token: 0x04002EE8 RID: 12008
			String = 115,
			// Token: 0x04002EE9 RID: 12009
			Number = 100,
			// Token: 0x04002EEA RID: 12010
			Axe = 97,
			// Token: 0x04002EEB RID: 12011
			Eof = 69
		}
	}
}

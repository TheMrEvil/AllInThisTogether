using System;
using System.Collections.Generic;
using System.Xml.XmlConfiguration;
using System.Xml.XPath;

namespace System.Xml.Xsl.XPath
{
	// Token: 0x0200042C RID: 1068
	internal class XPathParser<Node>
	{
		// Token: 0x06002A6C RID: 10860 RVA: 0x0010040C File Offset: 0x000FE60C
		public Node Parse(XPathScanner scanner, IXPathBuilder<Node> builder, LexKind endLex)
		{
			Node result = default(Node);
			this.scanner = scanner;
			this.builder = builder;
			this.posInfo.Clear();
			try
			{
				builder.StartBuild();
				result = this.ParseExpr();
				scanner.CheckToken(endLex);
			}
			catch (XPathCompileException ex)
			{
				if (ex.queryString == null)
				{
					ex.queryString = scanner.Source;
					this.PopPosInfo(out ex.startChar, out ex.endChar);
				}
				throw;
			}
			finally
			{
				result = builder.EndBuild(result);
			}
			return result;
		}

		// Token: 0x06002A6D RID: 10861 RVA: 0x001004A0 File Offset: 0x000FE6A0
		internal static bool IsStep(LexKind lexKind)
		{
			return lexKind == LexKind.Dot || lexKind == LexKind.DotDot || lexKind == LexKind.At || lexKind == LexKind.Axis || lexKind == LexKind.Star || lexKind == LexKind.Name;
		}

		// Token: 0x06002A6E RID: 10862 RVA: 0x001004C4 File Offset: 0x000FE6C4
		private Node ParseLocationPath()
		{
			if (this.scanner.Kind == LexKind.Slash)
			{
				this.scanner.NextLex();
				Node node = this.builder.Axis(XPathAxis.Root, XPathNodeType.All, null, null);
				if (XPathParser<Node>.IsStep(this.scanner.Kind))
				{
					node = this.builder.JoinStep(node, this.ParseRelativeLocationPath());
				}
				return node;
			}
			if (this.scanner.Kind == LexKind.SlashSlash)
			{
				this.scanner.NextLex();
				return this.builder.JoinStep(this.builder.Axis(XPathAxis.Root, XPathNodeType.All, null, null), this.builder.JoinStep(this.builder.Axis(XPathAxis.DescendantOrSelf, XPathNodeType.All, null, null), this.ParseRelativeLocationPath()));
			}
			return this.ParseRelativeLocationPath();
		}

		// Token: 0x06002A6F RID: 10863 RVA: 0x00100584 File Offset: 0x000FE784
		private Node ParseRelativeLocationPath()
		{
			int num = this.parseRelativePath + 1;
			this.parseRelativePath = num;
			if (num > 1024 && XsltConfigSection.LimitXPathComplexity)
			{
				throw this.scanner.CreateException("The stylesheet is too complex.", Array.Empty<string>());
			}
			Node node = this.ParseStep();
			if (this.scanner.Kind == LexKind.Slash)
			{
				this.scanner.NextLex();
				node = this.builder.JoinStep(node, this.ParseRelativeLocationPath());
			}
			else if (this.scanner.Kind == LexKind.SlashSlash)
			{
				this.scanner.NextLex();
				node = this.builder.JoinStep(node, this.builder.JoinStep(this.builder.Axis(XPathAxis.DescendantOrSelf, XPathNodeType.All, null, null), this.ParseRelativeLocationPath()));
			}
			this.parseRelativePath--;
			return node;
		}

		// Token: 0x06002A70 RID: 10864 RVA: 0x00100654 File Offset: 0x000FE854
		private Node ParseStep()
		{
			Node node;
			if (LexKind.Dot == this.scanner.Kind)
			{
				this.scanner.NextLex();
				node = this.builder.Axis(XPathAxis.Self, XPathNodeType.All, null, null);
				if (LexKind.LBracket == this.scanner.Kind)
				{
					throw this.scanner.CreateException("Abbreviated step '.' cannot be followed by a predicate. Use the full form 'self::node()[predicate]' instead.", Array.Empty<string>());
				}
			}
			else if (LexKind.DotDot == this.scanner.Kind)
			{
				this.scanner.NextLex();
				node = this.builder.Axis(XPathAxis.Parent, XPathNodeType.All, null, null);
				if (LexKind.LBracket == this.scanner.Kind)
				{
					throw this.scanner.CreateException("Abbreviated step '..' cannot be followed by a predicate. Use the full form 'parent::node()[predicate]' instead.", Array.Empty<string>());
				}
			}
			else
			{
				LexKind kind = this.scanner.Kind;
				XPathAxis axis;
				if (kind <= LexKind.Name)
				{
					if (kind == LexKind.Axis)
					{
						axis = this.scanner.Axis;
						this.scanner.NextLex();
						this.scanner.NextLex();
						goto IL_12D;
					}
					if (kind != LexKind.Name)
					{
						goto IL_108;
					}
				}
				else if (kind != LexKind.Star)
				{
					if (kind != LexKind.At)
					{
						goto IL_108;
					}
					axis = XPathAxis.Attribute;
					this.scanner.NextLex();
					goto IL_12D;
				}
				axis = XPathAxis.Child;
				goto IL_12D;
				IL_108:
				throw this.scanner.CreateException("Unexpected token '{0}' in the expression.", new string[]
				{
					this.scanner.RawValue
				});
				IL_12D:
				node = this.ParseNodeTest(axis);
				while (LexKind.LBracket == this.scanner.Kind)
				{
					node = this.builder.Predicate(node, this.ParsePredicate(), XPathParser<Node>.IsReverseAxis(axis));
				}
			}
			return node;
		}

		// Token: 0x06002A71 RID: 10865 RVA: 0x001007C1 File Offset: 0x000FE9C1
		private static bool IsReverseAxis(XPathAxis axis)
		{
			return axis == XPathAxis.Ancestor || axis == XPathAxis.Preceding || axis == XPathAxis.AncestorOrSelf || axis == XPathAxis.PrecedingSibling;
		}

		// Token: 0x06002A72 RID: 10866 RVA: 0x001007D8 File Offset: 0x000FE9D8
		private Node ParseNodeTest(XPathAxis axis)
		{
			int lexStart = this.scanner.LexStart;
			XPathNodeType nodeType;
			string prefix;
			string name;
			XPathParser<Node>.InternalParseNodeTest(this.scanner, axis, out nodeType, out prefix, out name);
			this.PushPosInfo(lexStart, this.scanner.PrevLexEnd);
			Node result = this.builder.Axis(axis, nodeType, prefix, name);
			this.PopPosInfo();
			return result;
		}

		// Token: 0x06002A73 RID: 10867 RVA: 0x0010082C File Offset: 0x000FEA2C
		private static bool IsNodeType(XPathScanner scanner)
		{
			return scanner.Prefix.Length == 0 && (scanner.Name == "node" || scanner.Name == "text" || scanner.Name == "processing-instruction" || scanner.Name == "comment");
		}

		// Token: 0x06002A74 RID: 10868 RVA: 0x00100890 File Offset: 0x000FEA90
		private static XPathNodeType PrincipalNodeType(XPathAxis axis)
		{
			if (axis == XPathAxis.Attribute)
			{
				return XPathNodeType.Attribute;
			}
			if (axis != XPathAxis.Namespace)
			{
				return XPathNodeType.Element;
			}
			return XPathNodeType.Namespace;
		}

		// Token: 0x06002A75 RID: 10869 RVA: 0x001008A0 File Offset: 0x000FEAA0
		internal static void InternalParseNodeTest(XPathScanner scanner, XPathAxis axis, out XPathNodeType nodeType, out string nodePrefix, out string nodeName)
		{
			LexKind kind = scanner.Kind;
			if (kind != LexKind.Name)
			{
				if (kind != LexKind.Star)
				{
					throw scanner.CreateException("Expected a node test, found '{0}'.", new string[]
					{
						scanner.RawValue
					});
				}
				nodePrefix = null;
				nodeName = null;
				nodeType = XPathParser<Node>.PrincipalNodeType(axis);
				scanner.NextLex();
				return;
			}
			else
			{
				if (scanner.CanBeFunction && XPathParser<Node>.IsNodeType(scanner))
				{
					nodePrefix = null;
					nodeName = null;
					string name = scanner.Name;
					if (!(name == "comment"))
					{
						if (!(name == "text"))
						{
							if (!(name == "node"))
							{
								nodeType = XPathNodeType.ProcessingInstruction;
							}
							else
							{
								nodeType = XPathNodeType.All;
							}
						}
						else
						{
							nodeType = XPathNodeType.Text;
						}
					}
					else
					{
						nodeType = XPathNodeType.Comment;
					}
					scanner.NextLex();
					scanner.PassToken(LexKind.LParens);
					if (nodeType == XPathNodeType.ProcessingInstruction && scanner.Kind != LexKind.RParens)
					{
						scanner.CheckToken(LexKind.String);
						nodePrefix = string.Empty;
						nodeName = scanner.StringValue;
						scanner.NextLex();
					}
					scanner.PassToken(LexKind.RParens);
					return;
				}
				nodePrefix = scanner.Prefix;
				nodeName = scanner.Name;
				nodeType = XPathParser<Node>.PrincipalNodeType(axis);
				scanner.NextLex();
				if (nodeName == "*")
				{
					nodeName = null;
					return;
				}
				return;
			}
		}

		// Token: 0x06002A76 RID: 10870 RVA: 0x001009CE File Offset: 0x000FEBCE
		private Node ParsePredicate()
		{
			this.scanner.PassToken(LexKind.LBracket);
			Node result = this.ParseExpr();
			this.scanner.PassToken(LexKind.RBracket);
			return result;
		}

		// Token: 0x06002A77 RID: 10871 RVA: 0x001009F0 File Offset: 0x000FEBF0
		private Node ParseExpr()
		{
			return this.ParseSubExpr(0);
		}

		// Token: 0x06002A78 RID: 10872 RVA: 0x001009FC File Offset: 0x000FEBFC
		private Node ParseSubExpr(int callerPrec)
		{
			int num = this.parseSubExprDepth + 1;
			this.parseSubExprDepth = num;
			if (num > 1024 && XsltConfigSection.LimitXPathComplexity)
			{
				throw this.scanner.CreateException("The stylesheet is too complex.", Array.Empty<string>());
			}
			Node node;
			if (this.scanner.Kind == LexKind.Minus)
			{
				XPathOperator xpathOperator = XPathOperator.UnaryMinus;
				int callerPrec2 = XPathParser<Node>.XPathOperatorPrecedence[(int)xpathOperator];
				this.scanner.NextLex();
				node = this.builder.Operator(xpathOperator, this.ParseSubExpr(callerPrec2), default(Node));
			}
			else
			{
				node = this.ParseUnionExpr();
			}
			for (;;)
			{
				XPathOperator xpathOperator = (XPathOperator)((this.scanner.Kind <= LexKind.Union) ? this.scanner.Kind : LexKind.Unknown);
				int num2 = XPathParser<Node>.XPathOperatorPrecedence[(int)xpathOperator];
				if (num2 <= callerPrec)
				{
					break;
				}
				this.scanner.NextLex();
				node = this.builder.Operator(xpathOperator, node, this.ParseSubExpr(num2));
			}
			this.parseSubExprDepth--;
			return node;
		}

		// Token: 0x06002A79 RID: 10873 RVA: 0x00100AE8 File Offset: 0x000FECE8
		private Node ParseUnionExpr()
		{
			int lexStart = this.scanner.LexStart;
			Node node = this.ParsePathExpr();
			if (this.scanner.Kind == LexKind.Union)
			{
				this.PushPosInfo(lexStart, this.scanner.PrevLexEnd);
				node = this.builder.Operator(XPathOperator.Union, default(Node), node);
				this.PopPosInfo();
				while (this.scanner.Kind == LexKind.Union)
				{
					this.scanner.NextLex();
					lexStart = this.scanner.LexStart;
					Node right = this.ParsePathExpr();
					this.PushPosInfo(lexStart, this.scanner.PrevLexEnd);
					node = this.builder.Operator(XPathOperator.Union, node, right);
					this.PopPosInfo();
				}
			}
			return node;
		}

		// Token: 0x06002A7A RID: 10874 RVA: 0x00100BA4 File Offset: 0x000FEDA4
		private Node ParsePathExpr()
		{
			if (this.IsPrimaryExpr())
			{
				int lexStart = this.scanner.LexStart;
				Node node = this.ParseFilterExpr();
				int prevLexEnd = this.scanner.PrevLexEnd;
				if (this.scanner.Kind == LexKind.Slash)
				{
					this.scanner.NextLex();
					this.PushPosInfo(lexStart, prevLexEnd);
					node = this.builder.JoinStep(node, this.ParseRelativeLocationPath());
					this.PopPosInfo();
				}
				else if (this.scanner.Kind == LexKind.SlashSlash)
				{
					this.scanner.NextLex();
					this.PushPosInfo(lexStart, prevLexEnd);
					node = this.builder.JoinStep(node, this.builder.JoinStep(this.builder.Axis(XPathAxis.DescendantOrSelf, XPathNodeType.All, null, null), this.ParseRelativeLocationPath()));
					this.PopPosInfo();
				}
				return node;
			}
			return this.ParseLocationPath();
		}

		// Token: 0x06002A7B RID: 10875 RVA: 0x00100C78 File Offset: 0x000FEE78
		private Node ParseFilterExpr()
		{
			int lexStart = this.scanner.LexStart;
			Node node = this.ParsePrimaryExpr();
			int prevLexEnd = this.scanner.PrevLexEnd;
			while (this.scanner.Kind == LexKind.LBracket)
			{
				this.PushPosInfo(lexStart, prevLexEnd);
				node = this.builder.Predicate(node, this.ParsePredicate(), false);
				this.PopPosInfo();
			}
			return node;
		}

		// Token: 0x06002A7C RID: 10876 RVA: 0x00100CD8 File Offset: 0x000FEED8
		private bool IsPrimaryExpr()
		{
			return this.scanner.Kind == LexKind.String || this.scanner.Kind == LexKind.Number || this.scanner.Kind == LexKind.Dollar || this.scanner.Kind == LexKind.LParens || (this.scanner.Kind == LexKind.Name && this.scanner.CanBeFunction && !XPathParser<Node>.IsNodeType(this.scanner));
		}

		// Token: 0x06002A7D RID: 10877 RVA: 0x00100D50 File Offset: 0x000FEF50
		private Node ParsePrimaryExpr()
		{
			LexKind kind = this.scanner.Kind;
			Node result;
			if (kind <= LexKind.String)
			{
				if (kind == LexKind.Number)
				{
					result = this.builder.Number(XPathConvert.StringToDouble(this.scanner.RawValue));
					this.scanner.NextLex();
					return result;
				}
				if (kind == LexKind.String)
				{
					result = this.builder.String(this.scanner.StringValue);
					this.scanner.NextLex();
					return result;
				}
			}
			else
			{
				if (kind == LexKind.Dollar)
				{
					int lexStart = this.scanner.LexStart;
					this.scanner.NextLex();
					this.scanner.CheckToken(LexKind.Name);
					this.PushPosInfo(lexStart, this.scanner.LexStart + this.scanner.LexSize);
					result = this.builder.Variable(this.scanner.Prefix, this.scanner.Name);
					this.PopPosInfo();
					this.scanner.NextLex();
					return result;
				}
				if (kind == LexKind.LParens)
				{
					this.scanner.NextLex();
					result = this.ParseExpr();
					this.scanner.PassToken(LexKind.RParens);
					return result;
				}
			}
			result = this.ParseFunctionCall();
			return result;
		}

		// Token: 0x06002A7E RID: 10878 RVA: 0x00100E84 File Offset: 0x000FF084
		private Node ParseFunctionCall()
		{
			List<Node> list = new List<Node>();
			string name = this.scanner.Name;
			string prefix = this.scanner.Prefix;
			int lexStart = this.scanner.LexStart;
			this.scanner.PassToken(LexKind.Name);
			this.scanner.PassToken(LexKind.LParens);
			if (this.scanner.Kind != LexKind.RParens)
			{
				for (;;)
				{
					list.Add(this.ParseExpr());
					if (this.scanner.Kind != LexKind.Comma)
					{
						break;
					}
					this.scanner.NextLex();
				}
				this.scanner.CheckToken(LexKind.RParens);
			}
			this.scanner.NextLex();
			this.PushPosInfo(lexStart, this.scanner.PrevLexEnd);
			Node result = this.builder.Function(prefix, name, list);
			this.PopPosInfo();
			return result;
		}

		// Token: 0x06002A7F RID: 10879 RVA: 0x00100F4C File Offset: 0x000FF14C
		private void PushPosInfo(int startChar, int endChar)
		{
			this.posInfo.Push(startChar);
			this.posInfo.Push(endChar);
		}

		// Token: 0x06002A80 RID: 10880 RVA: 0x00100F66 File Offset: 0x000FF166
		private void PopPosInfo()
		{
			this.posInfo.Pop();
			this.posInfo.Pop();
		}

		// Token: 0x06002A81 RID: 10881 RVA: 0x00100F80 File Offset: 0x000FF180
		private void PopPosInfo(out int startChar, out int endChar)
		{
			endChar = this.posInfo.Pop();
			startChar = this.posInfo.Pop();
		}

		// Token: 0x06002A82 RID: 10882 RVA: 0x00100F9C File Offset: 0x000FF19C
		public XPathParser()
		{
		}

		// Token: 0x06002A83 RID: 10883 RVA: 0x00100FAF File Offset: 0x000FF1AF
		// Note: this type is marked as 'beforefieldinit'.
		static XPathParser()
		{
		}

		// Token: 0x04002170 RID: 8560
		private XPathScanner scanner;

		// Token: 0x04002171 RID: 8561
		private IXPathBuilder<Node> builder;

		// Token: 0x04002172 RID: 8562
		private Stack<int> posInfo = new Stack<int>();

		// Token: 0x04002173 RID: 8563
		private const int MaxParseRelativePathDepth = 1024;

		// Token: 0x04002174 RID: 8564
		private int parseRelativePath;

		// Token: 0x04002175 RID: 8565
		private const int MaxParseSubExprDepth = 1024;

		// Token: 0x04002176 RID: 8566
		private int parseSubExprDepth;

		// Token: 0x04002177 RID: 8567
		private static int[] XPathOperatorPrecedence = new int[]
		{
			0,
			1,
			2,
			3,
			3,
			4,
			4,
			4,
			4,
			5,
			5,
			6,
			6,
			6,
			7,
			8
		};
	}
}

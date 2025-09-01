using System;
using System.Collections.Generic;
using System.Xml.XmlConfiguration;
using System.Xml.XPath;
using System.Xml.Xsl.Qil;
using System.Xml.Xsl.XPath;

namespace System.Xml.Xsl.Xslt
{
	// Token: 0x020003FD RID: 1021
	internal class XPathPatternParser
	{
		// Token: 0x060028A7 RID: 10407 RVA: 0x000F48F4 File Offset: 0x000F2AF4
		public QilNode Parse(XPathScanner scanner, XPathPatternParser.IPatternBuilder ptrnBuilder)
		{
			QilNode result = null;
			ptrnBuilder.StartBuild();
			try
			{
				this.scanner = scanner;
				this.ptrnBuilder = ptrnBuilder;
				result = this.ParsePattern();
				this.scanner.CheckToken(LexKind.Eof);
			}
			finally
			{
				result = ptrnBuilder.EndBuild(result);
			}
			return result;
		}

		// Token: 0x060028A8 RID: 10408 RVA: 0x000F4948 File Offset: 0x000F2B48
		private QilNode ParsePattern()
		{
			QilNode qilNode = this.ParseLocationPathPattern();
			while (this.scanner.Kind == LexKind.Union)
			{
				this.scanner.NextLex();
				qilNode = this.ptrnBuilder.Operator(XPathOperator.Union, qilNode, this.ParseLocationPathPattern());
			}
			return qilNode;
		}

		// Token: 0x060028A9 RID: 10409 RVA: 0x000F4990 File Offset: 0x000F2B90
		private QilNode ParseLocationPathPattern()
		{
			LexKind kind = this.scanner.Kind;
			if (kind != LexKind.SlashSlash)
			{
				if (kind != LexKind.Name)
				{
					if (kind == LexKind.Slash)
					{
						this.scanner.NextLex();
						QilNode qilNode = this.ptrnBuilder.Axis(XPathAxis.Root, XPathNodeType.All, null, null);
						if (XPathParser<QilNode>.IsStep(this.scanner.Kind))
						{
							qilNode = this.ptrnBuilder.JoinStep(qilNode, this.ParseRelativePathPattern());
						}
						return qilNode;
					}
				}
				else if (this.scanner.CanBeFunction && this.scanner.Prefix.Length == 0 && (this.scanner.Name == "id" || this.scanner.Name == "key"))
				{
					QilNode qilNode = this.ParseIdKeyPattern();
					LexKind kind2 = this.scanner.Kind;
					if (kind2 != LexKind.SlashSlash)
					{
						if (kind2 == LexKind.Slash)
						{
							this.scanner.NextLex();
							qilNode = this.ptrnBuilder.JoinStep(qilNode, this.ParseRelativePathPattern());
						}
					}
					else
					{
						this.scanner.NextLex();
						qilNode = this.ptrnBuilder.JoinStep(qilNode, this.ptrnBuilder.JoinStep(this.ptrnBuilder.Axis(XPathAxis.DescendantOrSelf, XPathNodeType.All, null, null), this.ParseRelativePathPattern()));
					}
					return qilNode;
				}
				return this.ParseRelativePathPattern();
			}
			this.scanner.NextLex();
			return this.ptrnBuilder.JoinStep(this.ptrnBuilder.Axis(XPathAxis.Root, XPathNodeType.All, null, null), this.ptrnBuilder.JoinStep(this.ptrnBuilder.Axis(XPathAxis.DescendantOrSelf, XPathNodeType.All, null, null), this.ParseRelativePathPattern()));
		}

		// Token: 0x060028AA RID: 10410 RVA: 0x000F4B20 File Offset: 0x000F2D20
		private QilNode ParseIdKeyPattern()
		{
			List<QilNode> list = new List<QilNode>(2);
			if (this.scanner.Name == "id")
			{
				this.scanner.NextLex();
				this.scanner.PassToken(LexKind.LParens);
				this.scanner.CheckToken(LexKind.String);
				list.Add(this.ptrnBuilder.String(this.scanner.StringValue));
				this.scanner.NextLex();
				this.scanner.PassToken(LexKind.RParens);
				return this.ptrnBuilder.Function("", "id", list);
			}
			this.scanner.NextLex();
			this.scanner.PassToken(LexKind.LParens);
			this.scanner.CheckToken(LexKind.String);
			list.Add(this.ptrnBuilder.String(this.scanner.StringValue));
			this.scanner.NextLex();
			this.scanner.PassToken(LexKind.Comma);
			this.scanner.CheckToken(LexKind.String);
			list.Add(this.ptrnBuilder.String(this.scanner.StringValue));
			this.scanner.NextLex();
			this.scanner.PassToken(LexKind.RParens);
			return this.ptrnBuilder.Function("", "key", list);
		}

		// Token: 0x060028AB RID: 10411 RVA: 0x000F4C6C File Offset: 0x000F2E6C
		private QilNode ParseRelativePathPattern()
		{
			int num = this.parseRelativePath + 1;
			this.parseRelativePath = num;
			if (num > 1024 && XsltConfigSection.LimitXPathComplexity)
			{
				throw this.scanner.CreateException("The stylesheet is too complex.", Array.Empty<string>());
			}
			QilNode qilNode = this.ParseStepPattern();
			if (this.scanner.Kind == LexKind.Slash)
			{
				this.scanner.NextLex();
				qilNode = this.ptrnBuilder.JoinStep(qilNode, this.ParseRelativePathPattern());
			}
			else if (this.scanner.Kind == LexKind.SlashSlash)
			{
				this.scanner.NextLex();
				qilNode = this.ptrnBuilder.JoinStep(qilNode, this.ptrnBuilder.JoinStep(this.ptrnBuilder.Axis(XPathAxis.DescendantOrSelf, XPathNodeType.All, null, null), this.ParseRelativePathPattern()));
			}
			this.parseRelativePath--;
			return qilNode;
		}

		// Token: 0x060028AC RID: 10412 RVA: 0x000F4D3C File Offset: 0x000F2F3C
		private QilNode ParseStepPattern()
		{
			LexKind kind = this.scanner.Kind;
			XPathAxis xpathAxis;
			if (kind <= LexKind.Name)
			{
				if (kind != LexKind.DotDot)
				{
					if (kind != LexKind.Axis)
					{
						if (kind != LexKind.Name)
						{
							goto IL_A6;
						}
						goto IL_A2;
					}
					else
					{
						xpathAxis = this.scanner.Axis;
						if (xpathAxis != XPathAxis.Child && xpathAxis != XPathAxis.Attribute)
						{
							throw this.scanner.CreateException("Only 'child' and 'attribute' axes are allowed in a pattern outside predicates.", Array.Empty<string>());
						}
						this.scanner.NextLex();
						this.scanner.NextLex();
						goto IL_CB;
					}
				}
			}
			else
			{
				if (kind == LexKind.Star)
				{
					goto IL_A2;
				}
				if (kind != LexKind.Dot)
				{
					if (kind != LexKind.At)
					{
						goto IL_A6;
					}
					xpathAxis = XPathAxis.Attribute;
					this.scanner.NextLex();
					goto IL_CB;
				}
			}
			throw this.scanner.CreateException("Only 'child' and 'attribute' axes are allowed in a pattern outside predicates.", Array.Empty<string>());
			IL_A2:
			xpathAxis = XPathAxis.Child;
			goto IL_CB;
			IL_A6:
			throw this.scanner.CreateException("Unexpected token '{0}' in the expression.", new string[]
			{
				this.scanner.RawValue
			});
			IL_CB:
			XPathNodeType nodeType;
			string prefix;
			string name;
			XPathParser<QilNode>.InternalParseNodeTest(this.scanner, xpathAxis, out nodeType, out prefix, out name);
			QilNode qilNode = this.ptrnBuilder.Axis(xpathAxis, nodeType, prefix, name);
			XPathPatternBuilder xpathPatternBuilder = this.ptrnBuilder as XPathPatternBuilder;
			if (xpathPatternBuilder != null)
			{
				List<QilNode> list = new List<QilNode>();
				while (this.scanner.Kind == LexKind.LBracket)
				{
					list.Add(this.ParsePredicate(qilNode));
				}
				if (list.Count > 0)
				{
					qilNode = xpathPatternBuilder.BuildPredicates(qilNode, list);
				}
			}
			else
			{
				while (this.scanner.Kind == LexKind.LBracket)
				{
					qilNode = this.ptrnBuilder.Predicate(qilNode, this.ParsePredicate(qilNode), false);
				}
			}
			return qilNode;
		}

		// Token: 0x060028AD RID: 10413 RVA: 0x000F4EAA File Offset: 0x000F30AA
		private QilNode ParsePredicate(QilNode context)
		{
			this.scanner.NextLex();
			QilNode result = this.predicateParser.Parse(this.scanner, this.ptrnBuilder.GetPredicateBuilder(context), LexKind.RBracket);
			this.scanner.NextLex();
			return result;
		}

		// Token: 0x060028AE RID: 10414 RVA: 0x000F4EE1 File Offset: 0x000F30E1
		public XPathPatternParser()
		{
		}

		// Token: 0x0400201B RID: 8219
		private XPathScanner scanner;

		// Token: 0x0400201C RID: 8220
		private XPathPatternParser.IPatternBuilder ptrnBuilder;

		// Token: 0x0400201D RID: 8221
		private XPathParser<QilNode> predicateParser = new XPathParser<QilNode>();

		// Token: 0x0400201E RID: 8222
		private const int MaxParseRelativePathDepth = 1024;

		// Token: 0x0400201F RID: 8223
		private int parseRelativePath;

		// Token: 0x020003FE RID: 1022
		public interface IPatternBuilder : IXPathBuilder<QilNode>
		{
			// Token: 0x060028AF RID: 10415
			IXPathBuilder<QilNode> GetPredicateBuilder(QilNode context);
		}
	}
}

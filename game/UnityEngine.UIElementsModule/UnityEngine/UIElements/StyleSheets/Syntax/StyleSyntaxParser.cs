using System;
using System.Collections.Generic;

namespace UnityEngine.UIElements.StyleSheets.Syntax
{
	// Token: 0x02000382 RID: 898
	internal class StyleSyntaxParser
	{
		// Token: 0x06001CB3 RID: 7347 RVA: 0x000881C0 File Offset: 0x000863C0
		public Expression Parse(string syntax)
		{
			bool flag = string.IsNullOrEmpty(syntax);
			Expression result;
			if (flag)
			{
				result = null;
			}
			else
			{
				Expression expression = null;
				bool flag2 = !this.m_ParsedExpressionCache.TryGetValue(syntax, out expression);
				if (flag2)
				{
					StyleSyntaxTokenizer styleSyntaxTokenizer = new StyleSyntaxTokenizer();
					styleSyntaxTokenizer.Tokenize(syntax);
					try
					{
						expression = this.ParseExpression(styleSyntaxTokenizer);
					}
					catch (Exception exception)
					{
						Debug.LogException(exception);
					}
					this.m_ParsedExpressionCache[syntax] = expression;
				}
				result = expression;
			}
			return result;
		}

		// Token: 0x06001CB4 RID: 7348 RVA: 0x00088244 File Offset: 0x00086444
		private Expression ParseExpression(StyleSyntaxTokenizer tokenizer)
		{
			StyleSyntaxToken current = tokenizer.current;
			while (!StyleSyntaxParser.IsExpressionEnd(current))
			{
				bool flag = current.type == StyleSyntaxTokenType.String || current.type == StyleSyntaxTokenType.LessThan;
				Expression item;
				if (flag)
				{
					item = this.ParseTerm(tokenizer);
				}
				else
				{
					bool flag2 = current.type == StyleSyntaxTokenType.OpenBracket;
					if (!flag2)
					{
						throw new Exception(string.Format("Unexpected token '{0}' in expression", current.type));
					}
					item = this.ParseGroup(tokenizer);
				}
				this.m_ExpressionStack.Push(item);
				ExpressionCombinator expressionCombinator = this.ParseCombinatorType(tokenizer);
				bool flag3 = expressionCombinator > ExpressionCombinator.None;
				if (flag3)
				{
					bool flag4 = this.m_CombinatorStack.Count > 0;
					if (flag4)
					{
						ExpressionCombinator expressionCombinator2 = this.m_CombinatorStack.Peek();
						int num = (int)expressionCombinator2;
						int num2 = (int)expressionCombinator;
						while (num > num2 && expressionCombinator2 != ExpressionCombinator.Group)
						{
							this.ProcessCombinatorStack();
							expressionCombinator2 = ((this.m_CombinatorStack.Count > 0) ? this.m_CombinatorStack.Peek() : ExpressionCombinator.None);
							num = (int)expressionCombinator2;
						}
					}
					this.m_CombinatorStack.Push(expressionCombinator);
				}
				current = tokenizer.current;
			}
			while (this.m_CombinatorStack.Count > 0)
			{
				ExpressionCombinator expressionCombinator3 = this.m_CombinatorStack.Peek();
				bool flag5 = expressionCombinator3 == ExpressionCombinator.Group;
				if (flag5)
				{
					this.m_CombinatorStack.Pop();
					break;
				}
				this.ProcessCombinatorStack();
			}
			return this.m_ExpressionStack.Pop();
		}

		// Token: 0x06001CB5 RID: 7349 RVA: 0x000883D0 File Offset: 0x000865D0
		private void ProcessCombinatorStack()
		{
			ExpressionCombinator expressionCombinator = this.m_CombinatorStack.Pop();
			Expression item = this.m_ExpressionStack.Pop();
			Expression item2 = this.m_ExpressionStack.Pop();
			this.m_ProcessExpressionList.Clear();
			this.m_ProcessExpressionList.Add(item2);
			this.m_ProcessExpressionList.Add(item);
			while (this.m_CombinatorStack.Count > 0 && expressionCombinator == this.m_CombinatorStack.Peek())
			{
				Expression item3 = this.m_ExpressionStack.Pop();
				this.m_ProcessExpressionList.Insert(0, item3);
				this.m_CombinatorStack.Pop();
			}
			Expression expression = new Expression(ExpressionType.Combinator);
			expression.combinator = expressionCombinator;
			expression.subExpressions = this.m_ProcessExpressionList.ToArray();
			this.m_ExpressionStack.Push(expression);
		}

		// Token: 0x06001CB6 RID: 7350 RVA: 0x000884A8 File Offset: 0x000866A8
		private Expression ParseTerm(StyleSyntaxTokenizer tokenizer)
		{
			StyleSyntaxToken current = tokenizer.current;
			bool flag = current.type == StyleSyntaxTokenType.LessThan;
			Expression expression;
			if (flag)
			{
				expression = this.ParseDataType(tokenizer);
			}
			else
			{
				bool flag2 = current.type == StyleSyntaxTokenType.String;
				if (!flag2)
				{
					throw new Exception(string.Format("Unexpected token '{0}' in expression. Expected term token", current.type));
				}
				expression = new Expression(ExpressionType.Keyword);
				expression.keyword = current.text.ToLower();
				tokenizer.MoveNext();
			}
			this.ParseMultiplier(tokenizer, ref expression.multiplier);
			return expression;
		}

		// Token: 0x06001CB7 RID: 7351 RVA: 0x0008853C File Offset: 0x0008673C
		private ExpressionCombinator ParseCombinatorType(StyleSyntaxTokenizer tokenizer)
		{
			ExpressionCombinator expressionCombinator = ExpressionCombinator.None;
			StyleSyntaxToken styleSyntaxToken = tokenizer.current;
			while (!StyleSyntaxParser.IsExpressionEnd(styleSyntaxToken) && expressionCombinator == ExpressionCombinator.None)
			{
				StyleSyntaxToken styleSyntaxToken2 = tokenizer.PeekNext();
				switch (styleSyntaxToken.type)
				{
				case StyleSyntaxTokenType.Space:
				{
					bool flag = !StyleSyntaxParser.IsCombinator(styleSyntaxToken2) && styleSyntaxToken2.type != StyleSyntaxTokenType.CloseBracket;
					if (flag)
					{
						expressionCombinator = ExpressionCombinator.Juxtaposition;
					}
					break;
				}
				case StyleSyntaxTokenType.SingleBar:
					expressionCombinator = ExpressionCombinator.Or;
					break;
				case StyleSyntaxTokenType.DoubleBar:
					expressionCombinator = ExpressionCombinator.OrOr;
					break;
				case StyleSyntaxTokenType.DoubleAmpersand:
					expressionCombinator = ExpressionCombinator.AndAnd;
					break;
				default:
					throw new Exception(string.Format("Unexpected token '{0}' in expression. Expected combinator token", styleSyntaxToken.type));
				}
				styleSyntaxToken = tokenizer.MoveNext();
			}
			StyleSyntaxParser.EatSpace(tokenizer);
			return expressionCombinator;
		}

		// Token: 0x06001CB8 RID: 7352 RVA: 0x00088600 File Offset: 0x00086800
		private Expression ParseGroup(StyleSyntaxTokenizer tokenizer)
		{
			StyleSyntaxToken current = tokenizer.current;
			bool flag = current.type != StyleSyntaxTokenType.OpenBracket;
			if (flag)
			{
				throw new Exception(string.Format("Unexpected token '{0}' in group expression. Expected '[' token", current.type));
			}
			this.m_CombinatorStack.Push(ExpressionCombinator.Group);
			tokenizer.MoveNext();
			StyleSyntaxParser.EatSpace(tokenizer);
			Expression expression = this.ParseExpression(tokenizer);
			current = tokenizer.current;
			bool flag2 = current.type != StyleSyntaxTokenType.CloseBracket;
			if (flag2)
			{
				throw new Exception(string.Format("Unexpected token '{0}' in group expression. Expected ']' token", current.type));
			}
			tokenizer.MoveNext();
			Expression expression2 = new Expression(ExpressionType.Combinator);
			expression2.combinator = ExpressionCombinator.Group;
			expression2.subExpressions = new Expression[]
			{
				expression
			};
			this.ParseMultiplier(tokenizer, ref expression2.multiplier);
			return expression2;
		}

		// Token: 0x06001CB9 RID: 7353 RVA: 0x000886D4 File Offset: 0x000868D4
		private Expression ParseDataType(StyleSyntaxTokenizer tokenizer)
		{
			StyleSyntaxToken styleSyntaxToken = tokenizer.current;
			bool flag = styleSyntaxToken.type != StyleSyntaxTokenType.LessThan;
			if (flag)
			{
				throw new Exception(string.Format("Unexpected token '{0}' in data type expression. Expected '<' token", styleSyntaxToken.type));
			}
			styleSyntaxToken = tokenizer.MoveNext();
			StyleSyntaxTokenType type = styleSyntaxToken.type;
			StyleSyntaxTokenType styleSyntaxTokenType = type;
			Expression expression;
			if (styleSyntaxTokenType != StyleSyntaxTokenType.String)
			{
				if (styleSyntaxTokenType != StyleSyntaxTokenType.SingleQuote)
				{
					throw new Exception(string.Format("Unexpected token '{0}' in data type expression", styleSyntaxToken.type));
				}
				expression = this.ParseProperty(tokenizer);
			}
			else
			{
				string syntax;
				bool flag2 = StylePropertyCache.TryGetNonTerminalValue(styleSyntaxToken.text, out syntax);
				if (flag2)
				{
					expression = this.ParseNonTerminalValue(syntax);
				}
				else
				{
					DataType dataType = DataType.None;
					try
					{
						object obj = Enum.Parse(typeof(DataType), styleSyntaxToken.text.Replace("-", ""), true);
						bool flag3 = obj != null;
						if (flag3)
						{
							dataType = (DataType)obj;
						}
					}
					catch (Exception)
					{
						throw new Exception("Unknown data type '" + styleSyntaxToken.text + "'");
					}
					expression = new Expression(ExpressionType.Data);
					expression.dataType = dataType;
				}
				tokenizer.MoveNext();
			}
			styleSyntaxToken = tokenizer.current;
			bool flag4 = styleSyntaxToken.type != StyleSyntaxTokenType.GreaterThan;
			if (flag4)
			{
				throw new Exception(string.Format("Unexpected token '{0}' in data type expression. Expected '>' token", styleSyntaxToken.type));
			}
			tokenizer.MoveNext();
			return expression;
		}

		// Token: 0x06001CBA RID: 7354 RVA: 0x00088850 File Offset: 0x00086A50
		private Expression ParseNonTerminalValue(string syntax)
		{
			Expression expression = null;
			bool flag = !this.m_ParsedExpressionCache.TryGetValue(syntax, out expression);
			if (flag)
			{
				this.m_CombinatorStack.Push(ExpressionCombinator.Group);
				expression = this.Parse(syntax);
			}
			return new Expression(ExpressionType.Combinator)
			{
				combinator = ExpressionCombinator.Group,
				subExpressions = new Expression[]
				{
					expression
				}
			};
		}

		// Token: 0x06001CBB RID: 7355 RVA: 0x000888B0 File Offset: 0x00086AB0
		private Expression ParseProperty(StyleSyntaxTokenizer tokenizer)
		{
			Expression expression = null;
			StyleSyntaxToken styleSyntaxToken = tokenizer.current;
			bool flag = styleSyntaxToken.type != StyleSyntaxTokenType.SingleQuote;
			if (flag)
			{
				throw new Exception(string.Format("Unexpected token '{0}' in property expression. Expected ''' token", styleSyntaxToken.type));
			}
			styleSyntaxToken = tokenizer.MoveNext();
			bool flag2 = styleSyntaxToken.type != StyleSyntaxTokenType.String;
			if (flag2)
			{
				throw new Exception(string.Format("Unexpected token '{0}' in property expression. Expected 'string' token", styleSyntaxToken.type));
			}
			string text = styleSyntaxToken.text;
			string text2;
			bool flag3 = !StylePropertyCache.TryGetSyntax(text, out text2);
			if (flag3)
			{
				throw new Exception("Unknown property '" + text + "' <''> expression.");
			}
			bool flag4 = !this.m_ParsedExpressionCache.TryGetValue(text2, out expression);
			if (flag4)
			{
				this.m_CombinatorStack.Push(ExpressionCombinator.Group);
				expression = this.Parse(text2);
			}
			styleSyntaxToken = tokenizer.MoveNext();
			bool flag5 = styleSyntaxToken.type != StyleSyntaxTokenType.SingleQuote;
			if (flag5)
			{
				throw new Exception(string.Format("Unexpected token '{0}' in property expression. Expected ''' token", styleSyntaxToken.type));
			}
			styleSyntaxToken = tokenizer.MoveNext();
			bool flag6 = styleSyntaxToken.type != StyleSyntaxTokenType.GreaterThan;
			if (flag6)
			{
				throw new Exception(string.Format("Unexpected token '{0}' in property expression. Expected '>' token", styleSyntaxToken.type));
			}
			return new Expression(ExpressionType.Combinator)
			{
				combinator = ExpressionCombinator.Group,
				subExpressions = new Expression[]
				{
					expression
				}
			};
		}

		// Token: 0x06001CBC RID: 7356 RVA: 0x00088A18 File Offset: 0x00086C18
		private void ParseMultiplier(StyleSyntaxTokenizer tokenizer, ref ExpressionMultiplier multiplier)
		{
			StyleSyntaxToken styleSyntaxToken = tokenizer.current;
			bool flag = StyleSyntaxParser.IsMultiplier(styleSyntaxToken);
			if (flag)
			{
				switch (styleSyntaxToken.type)
				{
				case StyleSyntaxTokenType.Asterisk:
					multiplier.type = ExpressionMultiplierType.ZeroOrMore;
					goto IL_A1;
				case StyleSyntaxTokenType.Plus:
					multiplier.type = ExpressionMultiplierType.OneOrMore;
					goto IL_A1;
				case StyleSyntaxTokenType.QuestionMark:
					multiplier.type = ExpressionMultiplierType.ZeroOrOne;
					goto IL_A1;
				case StyleSyntaxTokenType.HashMark:
					multiplier.type = ExpressionMultiplierType.OneOrMoreComma;
					goto IL_A1;
				case StyleSyntaxTokenType.ExclamationPoint:
					multiplier.type = ExpressionMultiplierType.GroupAtLeastOne;
					goto IL_A1;
				case StyleSyntaxTokenType.OpenBrace:
					multiplier.type = ExpressionMultiplierType.Ranges;
					goto IL_A1;
				}
				throw new Exception(string.Format("Unexpected token '{0}' in expression. Expected multiplier token", styleSyntaxToken.type));
				IL_A1:
				styleSyntaxToken = tokenizer.MoveNext();
			}
			bool flag2 = multiplier.type == ExpressionMultiplierType.Ranges;
			if (flag2)
			{
				this.ParseRanges(tokenizer, out multiplier.min, out multiplier.max);
			}
		}

		// Token: 0x06001CBD RID: 7357 RVA: 0x00088AF4 File Offset: 0x00086CF4
		private void ParseRanges(StyleSyntaxTokenizer tokenizer, out int min, out int max)
		{
			min = -1;
			max = -1;
			StyleSyntaxToken styleSyntaxToken = tokenizer.current;
			bool flag = false;
			while (styleSyntaxToken.type != StyleSyntaxTokenType.CloseBrace)
			{
				StyleSyntaxTokenType type = styleSyntaxToken.type;
				StyleSyntaxTokenType styleSyntaxTokenType = type;
				if (styleSyntaxTokenType != StyleSyntaxTokenType.Number)
				{
					if (styleSyntaxTokenType != StyleSyntaxTokenType.Comma)
					{
						throw new Exception(string.Format("Unexpected token '{0}' in expression. Expected ranges token", styleSyntaxToken.type));
					}
					flag = true;
				}
				else
				{
					bool flag2 = !flag;
					if (flag2)
					{
						min = styleSyntaxToken.number;
					}
					else
					{
						max = styleSyntaxToken.number;
					}
				}
				styleSyntaxToken = tokenizer.MoveNext();
			}
			tokenizer.MoveNext();
		}

		// Token: 0x06001CBE RID: 7358 RVA: 0x00088B8C File Offset: 0x00086D8C
		private static void EatSpace(StyleSyntaxTokenizer tokenizer)
		{
			StyleSyntaxToken current = tokenizer.current;
			bool flag = current.type == StyleSyntaxTokenType.Space;
			if (flag)
			{
				tokenizer.MoveNext();
			}
		}

		// Token: 0x06001CBF RID: 7359 RVA: 0x00088BB8 File Offset: 0x00086DB8
		private static bool IsExpressionEnd(StyleSyntaxToken token)
		{
			StyleSyntaxTokenType type = token.type;
			StyleSyntaxTokenType styleSyntaxTokenType = type;
			return styleSyntaxTokenType == StyleSyntaxTokenType.CloseBracket || styleSyntaxTokenType == StyleSyntaxTokenType.End;
		}

		// Token: 0x06001CC0 RID: 7360 RVA: 0x00088BE8 File Offset: 0x00086DE8
		private static bool IsCombinator(StyleSyntaxToken token)
		{
			StyleSyntaxTokenType type = token.type;
			StyleSyntaxTokenType styleSyntaxTokenType = type;
			return styleSyntaxTokenType - StyleSyntaxTokenType.Space <= 3;
		}

		// Token: 0x06001CC1 RID: 7361 RVA: 0x00088C10 File Offset: 0x00086E10
		private static bool IsMultiplier(StyleSyntaxToken token)
		{
			StyleSyntaxTokenType type = token.type;
			StyleSyntaxTokenType styleSyntaxTokenType = type;
			return styleSyntaxTokenType - StyleSyntaxTokenType.Asterisk <= 4 || styleSyntaxTokenType == StyleSyntaxTokenType.OpenBrace;
		}

		// Token: 0x06001CC2 RID: 7362 RVA: 0x00088C40 File Offset: 0x00086E40
		public StyleSyntaxParser()
		{
		}

		// Token: 0x04000E81 RID: 3713
		private List<Expression> m_ProcessExpressionList = new List<Expression>();

		// Token: 0x04000E82 RID: 3714
		private Stack<Expression> m_ExpressionStack = new Stack<Expression>();

		// Token: 0x04000E83 RID: 3715
		private Stack<ExpressionCombinator> m_CombinatorStack = new Stack<ExpressionCombinator>();

		// Token: 0x04000E84 RID: 3716
		private Dictionary<string, Expression> m_ParsedExpressionCache = new Dictionary<string, Expression>();
	}
}

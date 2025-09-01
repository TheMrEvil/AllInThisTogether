using System;
using System.Collections.Generic;
using System.Dynamic.Utils;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace System.Linq.Expressions
{
	// Token: 0x02000246 RID: 582
	internal sealed class DebugViewWriter : ExpressionVisitor
	{
		// Token: 0x06000FDD RID: 4061 RVA: 0x00035976 File Offset: 0x00033B76
		private DebugViewWriter(TextWriter file)
		{
			this._out = file;
		}

		// Token: 0x170002AC RID: 684
		// (get) Token: 0x06000FDE RID: 4062 RVA: 0x00035990 File Offset: 0x00033B90
		private int Base
		{
			get
			{
				if (this._stack.Count <= 0)
				{
					return 0;
				}
				return this._stack.Peek();
			}
		}

		// Token: 0x170002AD RID: 685
		// (get) Token: 0x06000FDF RID: 4063 RVA: 0x000359AD File Offset: 0x00033BAD
		private int Delta
		{
			get
			{
				return this._delta;
			}
		}

		// Token: 0x170002AE RID: 686
		// (get) Token: 0x06000FE0 RID: 4064 RVA: 0x000359B5 File Offset: 0x00033BB5
		private int Depth
		{
			get
			{
				return this.Base + this.Delta;
			}
		}

		// Token: 0x06000FE1 RID: 4065 RVA: 0x000359C4 File Offset: 0x00033BC4
		private void Indent()
		{
			this._delta += 4;
		}

		// Token: 0x06000FE2 RID: 4066 RVA: 0x000359D4 File Offset: 0x00033BD4
		private void Dedent()
		{
			this._delta -= 4;
		}

		// Token: 0x06000FE3 RID: 4067 RVA: 0x000359E4 File Offset: 0x00033BE4
		private void NewLine()
		{
			this._flow = DebugViewWriter.Flow.NewLine;
		}

		// Token: 0x06000FE4 RID: 4068 RVA: 0x000359F0 File Offset: 0x00033BF0
		private static int GetId<T>(T e, ref Dictionary<T, int> ids)
		{
			if (ids == null)
			{
				ids = new Dictionary<T, int>();
				ids.Add(e, 1);
				return 1;
			}
			int num;
			if (!ids.TryGetValue(e, out num))
			{
				num = ids.Count + 1;
				ids.Add(e, num);
			}
			return num;
		}

		// Token: 0x06000FE5 RID: 4069 RVA: 0x00035A33 File Offset: 0x00033C33
		private int GetLambdaId(LambdaExpression le)
		{
			return DebugViewWriter.GetId<LambdaExpression>(le, ref this._lambdaIds);
		}

		// Token: 0x06000FE6 RID: 4070 RVA: 0x00035A41 File Offset: 0x00033C41
		private int GetParamId(ParameterExpression p)
		{
			return DebugViewWriter.GetId<ParameterExpression>(p, ref this._paramIds);
		}

		// Token: 0x06000FE7 RID: 4071 RVA: 0x00035A4F File Offset: 0x00033C4F
		private int GetLabelTargetId(LabelTarget target)
		{
			return DebugViewWriter.GetId<LabelTarget>(target, ref this._labelIds);
		}

		// Token: 0x06000FE8 RID: 4072 RVA: 0x00035A5D File Offset: 0x00033C5D
		internal static void WriteTo(Expression node, TextWriter writer)
		{
			new DebugViewWriter(writer).WriteTo(node);
		}

		// Token: 0x06000FE9 RID: 4073 RVA: 0x00035A6C File Offset: 0x00033C6C
		private void WriteTo(Expression node)
		{
			LambdaExpression lambdaExpression = node as LambdaExpression;
			if (lambdaExpression != null)
			{
				this.WriteLambda(lambdaExpression);
			}
			else
			{
				this.Visit(node);
			}
			while (this._lambdas != null && this._lambdas.Count > 0)
			{
				this.WriteLine();
				this.WriteLine();
				this.WriteLambda(this._lambdas.Dequeue());
			}
		}

		// Token: 0x06000FEA RID: 4074 RVA: 0x00035AC9 File Offset: 0x00033CC9
		private void Out(string s)
		{
			this.Out(DebugViewWriter.Flow.None, s, DebugViewWriter.Flow.None);
		}

		// Token: 0x06000FEB RID: 4075 RVA: 0x00035AD4 File Offset: 0x00033CD4
		private void Out(DebugViewWriter.Flow before, string s)
		{
			this.Out(before, s, DebugViewWriter.Flow.None);
		}

		// Token: 0x06000FEC RID: 4076 RVA: 0x00035ADF File Offset: 0x00033CDF
		private void Out(string s, DebugViewWriter.Flow after)
		{
			this.Out(DebugViewWriter.Flow.None, s, after);
		}

		// Token: 0x06000FED RID: 4077 RVA: 0x00035AEC File Offset: 0x00033CEC
		private void Out(DebugViewWriter.Flow before, string s, DebugViewWriter.Flow after)
		{
			switch (this.GetFlow(before))
			{
			case DebugViewWriter.Flow.Space:
				this.Write(" ");
				break;
			case DebugViewWriter.Flow.NewLine:
				this.WriteLine();
				this.Write(new string(' ', this.Depth));
				break;
			}
			this.Write(s);
			this._flow = after;
		}

		// Token: 0x06000FEE RID: 4078 RVA: 0x00035B49 File Offset: 0x00033D49
		private void WriteLine()
		{
			this._out.WriteLine();
			this._column = 0;
		}

		// Token: 0x06000FEF RID: 4079 RVA: 0x00035B5D File Offset: 0x00033D5D
		private void Write(string s)
		{
			this._out.Write(s);
			this._column += s.Length;
		}

		// Token: 0x06000FF0 RID: 4080 RVA: 0x00035B7E File Offset: 0x00033D7E
		private DebugViewWriter.Flow GetFlow(DebugViewWriter.Flow flow)
		{
			int val = (int)this.CheckBreak(this._flow);
			flow = this.CheckBreak(flow);
			return (DebugViewWriter.Flow)Math.Max(val, (int)flow);
		}

		// Token: 0x06000FF1 RID: 4081 RVA: 0x00035B9B File Offset: 0x00033D9B
		private DebugViewWriter.Flow CheckBreak(DebugViewWriter.Flow flow)
		{
			if ((flow & DebugViewWriter.Flow.Break) != DebugViewWriter.Flow.None)
			{
				if (this._column > 120 + this.Depth)
				{
					flow = DebugViewWriter.Flow.NewLine;
				}
				else
				{
					flow &= ~DebugViewWriter.Flow.Break;
				}
			}
			return flow;
		}

		// Token: 0x06000FF2 RID: 4082 RVA: 0x00035BC6 File Offset: 0x00033DC6
		private void VisitExpressions<T>(char open, IReadOnlyList<T> expressions) where T : Expression
		{
			this.VisitExpressions<T>(open, ',', expressions);
		}

		// Token: 0x06000FF3 RID: 4083 RVA: 0x00035BD2 File Offset: 0x00033DD2
		private void VisitExpressions<T>(char open, char separator, IReadOnlyList<T> expressions) where T : Expression
		{
			this.VisitExpressions<T>(open, separator, expressions, delegate(T e)
			{
				this.Visit(e);
			});
		}

		// Token: 0x06000FF4 RID: 4084 RVA: 0x00035BE9 File Offset: 0x00033DE9
		private void VisitDeclarations(IReadOnlyList<ParameterExpression> expressions)
		{
			this.VisitExpressions<ParameterExpression>('(', ',', expressions, delegate(ParameterExpression variable)
			{
				this.Out(variable.Type.ToString());
				if (variable.IsByRef)
				{
					this.Out("&");
				}
				this.Out(" ");
				this.VisitParameter(variable);
			});
		}

		// Token: 0x06000FF5 RID: 4085 RVA: 0x00035C04 File Offset: 0x00033E04
		private void VisitExpressions<T>(char open, char separator, IReadOnlyList<T> expressions, Action<T> visit)
		{
			this.Out(open.ToString());
			if (expressions != null)
			{
				this.Indent();
				bool flag = true;
				foreach (T obj in expressions)
				{
					if (flag)
					{
						if (open == '{' || expressions.Count > 1)
						{
							this.NewLine();
						}
						flag = false;
					}
					else
					{
						this.Out(separator.ToString(), DebugViewWriter.Flow.NewLine);
					}
					visit(obj);
				}
				this.Dedent();
			}
			char c;
			if (open != '(')
			{
				if (open != '[')
				{
					if (open != '{')
					{
						throw ContractUtils.Unreachable;
					}
					c = '}';
				}
				else
				{
					c = ']';
				}
			}
			else
			{
				c = ')';
			}
			if (open == '{')
			{
				this.NewLine();
			}
			this.Out(c.ToString(), DebugViewWriter.Flow.Break);
		}

		// Token: 0x06000FF6 RID: 4086 RVA: 0x00035CD8 File Offset: 0x00033ED8
		protected internal override Expression VisitBinary(BinaryExpression node)
		{
			if (node.NodeType == ExpressionType.ArrayIndex)
			{
				this.ParenthesizedVisit(node, node.Left);
				this.Out("[");
				this.Visit(node.Right);
				this.Out("]");
			}
			else
			{
				bool flag = DebugViewWriter.NeedsParentheses(node, node.Left);
				bool flag2 = DebugViewWriter.NeedsParentheses(node, node.Right);
				DebugViewWriter.Flow before = DebugViewWriter.Flow.Space;
				ExpressionType nodeType = node.NodeType;
				string s;
				switch (nodeType)
				{
				case ExpressionType.Add:
					s = "+";
					goto IL_2F0;
				case ExpressionType.AddChecked:
					s = "#+";
					goto IL_2F0;
				case ExpressionType.And:
					s = "&";
					goto IL_2F0;
				case ExpressionType.AndAlso:
					s = "&&";
					before = (DebugViewWriter.Flow.Space | DebugViewWriter.Flow.Break);
					goto IL_2F0;
				case ExpressionType.ArrayLength:
				case ExpressionType.ArrayIndex:
				case ExpressionType.Call:
				case ExpressionType.Conditional:
				case ExpressionType.Constant:
				case ExpressionType.Convert:
				case ExpressionType.ConvertChecked:
				case ExpressionType.Invoke:
				case ExpressionType.Lambda:
				case ExpressionType.ListInit:
				case ExpressionType.MemberAccess:
				case ExpressionType.MemberInit:
				case ExpressionType.Negate:
				case ExpressionType.UnaryPlus:
				case ExpressionType.NegateChecked:
				case ExpressionType.New:
				case ExpressionType.NewArrayInit:
				case ExpressionType.NewArrayBounds:
				case ExpressionType.Not:
				case ExpressionType.Parameter:
				case ExpressionType.Quote:
				case ExpressionType.TypeAs:
				case ExpressionType.TypeIs:
					break;
				case ExpressionType.Coalesce:
					s = "??";
					goto IL_2F0;
				case ExpressionType.Divide:
					s = "/";
					goto IL_2F0;
				case ExpressionType.Equal:
					s = "==";
					goto IL_2F0;
				case ExpressionType.ExclusiveOr:
					s = "^";
					goto IL_2F0;
				case ExpressionType.GreaterThan:
					s = ">";
					goto IL_2F0;
				case ExpressionType.GreaterThanOrEqual:
					s = ">=";
					goto IL_2F0;
				case ExpressionType.LeftShift:
					s = "<<";
					goto IL_2F0;
				case ExpressionType.LessThan:
					s = "<";
					goto IL_2F0;
				case ExpressionType.LessThanOrEqual:
					s = "<=";
					goto IL_2F0;
				case ExpressionType.Modulo:
					s = "%";
					goto IL_2F0;
				case ExpressionType.Multiply:
					s = "*";
					goto IL_2F0;
				case ExpressionType.MultiplyChecked:
					s = "#*";
					goto IL_2F0;
				case ExpressionType.NotEqual:
					s = "!=";
					goto IL_2F0;
				case ExpressionType.Or:
					s = "|";
					goto IL_2F0;
				case ExpressionType.OrElse:
					s = "||";
					before = (DebugViewWriter.Flow.Space | DebugViewWriter.Flow.Break);
					goto IL_2F0;
				case ExpressionType.Power:
					s = "**";
					goto IL_2F0;
				case ExpressionType.RightShift:
					s = ">>";
					goto IL_2F0;
				case ExpressionType.Subtract:
					s = "-";
					goto IL_2F0;
				case ExpressionType.SubtractChecked:
					s = "#-";
					goto IL_2F0;
				case ExpressionType.Assign:
					s = "=";
					goto IL_2F0;
				default:
					switch (nodeType)
					{
					case ExpressionType.AddAssign:
						s = "+=";
						goto IL_2F0;
					case ExpressionType.AndAssign:
						s = "&=";
						goto IL_2F0;
					case ExpressionType.DivideAssign:
						s = "/=";
						goto IL_2F0;
					case ExpressionType.ExclusiveOrAssign:
						s = "^=";
						goto IL_2F0;
					case ExpressionType.LeftShiftAssign:
						s = "<<=";
						goto IL_2F0;
					case ExpressionType.ModuloAssign:
						s = "%=";
						goto IL_2F0;
					case ExpressionType.MultiplyAssign:
						s = "*=";
						goto IL_2F0;
					case ExpressionType.OrAssign:
						s = "|=";
						goto IL_2F0;
					case ExpressionType.PowerAssign:
						s = "**=";
						goto IL_2F0;
					case ExpressionType.RightShiftAssign:
						s = ">>=";
						goto IL_2F0;
					case ExpressionType.SubtractAssign:
						s = "-=";
						goto IL_2F0;
					case ExpressionType.AddAssignChecked:
						s = "#+=";
						goto IL_2F0;
					case ExpressionType.MultiplyAssignChecked:
						s = "#*=";
						goto IL_2F0;
					case ExpressionType.SubtractAssignChecked:
						s = "#-=";
						goto IL_2F0;
					}
					break;
				}
				throw new InvalidOperationException();
				IL_2F0:
				if (flag)
				{
					this.Out("(", DebugViewWriter.Flow.None);
				}
				this.Visit(node.Left);
				if (flag)
				{
					this.Out(DebugViewWriter.Flow.None, ")", DebugViewWriter.Flow.Break);
				}
				this.Out(before, s, DebugViewWriter.Flow.Space | DebugViewWriter.Flow.Break);
				if (flag2)
				{
					this.Out("(", DebugViewWriter.Flow.None);
				}
				this.Visit(node.Right);
				if (flag2)
				{
					this.Out(DebugViewWriter.Flow.None, ")", DebugViewWriter.Flow.Break);
				}
			}
			return node;
		}

		// Token: 0x06000FF7 RID: 4087 RVA: 0x00036044 File Offset: 0x00034244
		protected internal override Expression VisitParameter(ParameterExpression node)
		{
			this.Out("$");
			if (string.IsNullOrEmpty(node.Name))
			{
				this.Out("var" + this.GetParamId(node).ToString());
			}
			else
			{
				this.Out(DebugViewWriter.GetDisplayName(node.Name));
			}
			return node;
		}

		// Token: 0x06000FF8 RID: 4088 RVA: 0x0003609C File Offset: 0x0003429C
		protected internal override Expression VisitLambda<T>(Expression<T> node)
		{
			this.Out(string.Format(CultureInfo.CurrentCulture, ".Lambda {0}<{1}>", this.GetLambdaName(node), node.Type.ToString()));
			if (this._lambdas == null)
			{
				this._lambdas = new Queue<LambdaExpression>();
			}
			if (!this._lambdas.Contains(node))
			{
				this._lambdas.Enqueue(node);
			}
			return node;
		}

		// Token: 0x06000FF9 RID: 4089 RVA: 0x00036100 File Offset: 0x00034300
		private static bool IsSimpleExpression(Expression node)
		{
			BinaryExpression binaryExpression = node as BinaryExpression;
			return binaryExpression != null && !(binaryExpression.Left is BinaryExpression) && !(binaryExpression.Right is BinaryExpression);
		}

		// Token: 0x06000FFA RID: 4090 RVA: 0x0003613C File Offset: 0x0003433C
		protected internal override Expression VisitConditional(ConditionalExpression node)
		{
			if (DebugViewWriter.IsSimpleExpression(node.Test))
			{
				this.Out(".If (");
				this.Visit(node.Test);
				this.Out(") {", DebugViewWriter.Flow.NewLine);
			}
			else
			{
				this.Out(".If (", DebugViewWriter.Flow.NewLine);
				this.Indent();
				this.Visit(node.Test);
				this.Dedent();
				this.Out(DebugViewWriter.Flow.NewLine, ") {", DebugViewWriter.Flow.NewLine);
			}
			this.Indent();
			this.Visit(node.IfTrue);
			this.Dedent();
			this.Out(DebugViewWriter.Flow.NewLine, "} .Else {", DebugViewWriter.Flow.NewLine);
			this.Indent();
			this.Visit(node.IfFalse);
			this.Dedent();
			this.Out(DebugViewWriter.Flow.NewLine, "}");
			return node;
		}

		// Token: 0x06000FFB RID: 4091 RVA: 0x000361FC File Offset: 0x000343FC
		protected internal override Expression VisitConstant(ConstantExpression node)
		{
			object value = node.Value;
			if (value == null)
			{
				this.Out("null");
			}
			else if (value is string && node.Type == typeof(string))
			{
				this.Out(string.Format(CultureInfo.CurrentCulture, "\"{0}\"", value));
			}
			else if (value is char && node.Type == typeof(char))
			{
				this.Out(string.Format(CultureInfo.CurrentCulture, "'{0}'", value));
			}
			else if ((value is int && node.Type == typeof(int)) || (value is bool && node.Type == typeof(bool)))
			{
				this.Out(value.ToString());
			}
			else
			{
				string constantValueSuffix = DebugViewWriter.GetConstantValueSuffix(node.Type);
				if (constantValueSuffix != null)
				{
					this.Out(value.ToString());
					this.Out(constantValueSuffix);
				}
				else
				{
					this.Out(string.Format(CultureInfo.CurrentCulture, ".Constant<{0}>({1})", node.Type.ToString(), value));
				}
			}
			return node;
		}

		// Token: 0x06000FFC RID: 4092 RVA: 0x0003632C File Offset: 0x0003452C
		private static string GetConstantValueSuffix(Type type)
		{
			if (type == typeof(uint))
			{
				return "U";
			}
			if (type == typeof(long))
			{
				return "L";
			}
			if (type == typeof(ulong))
			{
				return "UL";
			}
			if (type == typeof(double))
			{
				return "D";
			}
			if (type == typeof(float))
			{
				return "F";
			}
			if (type == typeof(decimal))
			{
				return "M";
			}
			return null;
		}

		// Token: 0x06000FFD RID: 4093 RVA: 0x000363CA File Offset: 0x000345CA
		protected internal override Expression VisitRuntimeVariables(RuntimeVariablesExpression node)
		{
			this.Out(".RuntimeVariables");
			this.VisitExpressions<ParameterExpression>('(', node.Variables);
			return node;
		}

		// Token: 0x06000FFE RID: 4094 RVA: 0x000363E8 File Offset: 0x000345E8
		private void OutMember(Expression node, Expression instance, MemberInfo member)
		{
			if (instance != null)
			{
				this.ParenthesizedVisit(node, instance);
				this.Out("." + member.Name);
				return;
			}
			this.Out(member.DeclaringType.ToString() + "." + member.Name);
		}

		// Token: 0x06000FFF RID: 4095 RVA: 0x00036438 File Offset: 0x00034638
		protected internal override Expression VisitMember(MemberExpression node)
		{
			this.OutMember(node, node.Expression, node.Member);
			return node;
		}

		// Token: 0x06001000 RID: 4096 RVA: 0x0003644E File Offset: 0x0003464E
		protected internal override Expression VisitInvocation(InvocationExpression node)
		{
			this.Out(".Invoke ");
			this.ParenthesizedVisit(node, node.Expression);
			this.VisitExpressions<Expression>('(', node.Arguments);
			return node;
		}

		// Token: 0x06001001 RID: 4097 RVA: 0x00036478 File Offset: 0x00034678
		private static bool NeedsParentheses(Expression parent, Expression child)
		{
			if (child == null)
			{
				return false;
			}
			ExpressionType nodeType = parent.NodeType;
			if (nodeType <= ExpressionType.Increment)
			{
				if (nodeType != ExpressionType.Decrement && nodeType != ExpressionType.Increment)
				{
					goto IL_2B;
				}
			}
			else if (nodeType != ExpressionType.Unbox && nodeType - ExpressionType.IsTrue > 1)
			{
				goto IL_2B;
			}
			return true;
			IL_2B:
			int operatorPrecedence = DebugViewWriter.GetOperatorPrecedence(child);
			int operatorPrecedence2 = DebugViewWriter.GetOperatorPrecedence(parent);
			if (operatorPrecedence == operatorPrecedence2)
			{
				nodeType = parent.NodeType;
				if (nodeType <= ExpressionType.ExclusiveOr)
				{
					if (nodeType <= ExpressionType.AndAlso)
					{
						if (nodeType <= ExpressionType.AddChecked)
						{
							return false;
						}
						if (nodeType - ExpressionType.And > 1)
						{
							return true;
						}
					}
					else
					{
						if (nodeType == ExpressionType.Divide)
						{
							goto IL_8C;
						}
						if (nodeType != ExpressionType.ExclusiveOr)
						{
							return true;
						}
					}
				}
				else if (nodeType <= ExpressionType.MultiplyChecked)
				{
					if (nodeType == ExpressionType.Modulo)
					{
						goto IL_8C;
					}
					if (nodeType - ExpressionType.Multiply > 1)
					{
						return true;
					}
					return false;
				}
				else if (nodeType - ExpressionType.Or > 1)
				{
					if (nodeType - ExpressionType.Subtract > 1)
					{
						return true;
					}
					goto IL_8C;
				}
				return false;
				IL_8C:
				BinaryExpression binaryExpression = parent as BinaryExpression;
				return child == binaryExpression.Right;
			}
			return (child != null && child.NodeType == ExpressionType.Constant && (parent.NodeType == ExpressionType.Negate || parent.NodeType == ExpressionType.NegateChecked)) || operatorPrecedence < operatorPrecedence2;
		}

		// Token: 0x06001002 RID: 4098 RVA: 0x0003654C File Offset: 0x0003474C
		private static int GetOperatorPrecedence(Expression node)
		{
			switch (node.NodeType)
			{
			case ExpressionType.Add:
			case ExpressionType.AddChecked:
			case ExpressionType.Subtract:
			case ExpressionType.SubtractChecked:
				return 10;
			case ExpressionType.And:
				return 6;
			case ExpressionType.AndAlso:
				return 3;
			case ExpressionType.Coalesce:
			case ExpressionType.Assign:
			case ExpressionType.AddAssign:
			case ExpressionType.AndAssign:
			case ExpressionType.DivideAssign:
			case ExpressionType.ExclusiveOrAssign:
			case ExpressionType.LeftShiftAssign:
			case ExpressionType.ModuloAssign:
			case ExpressionType.MultiplyAssign:
			case ExpressionType.OrAssign:
			case ExpressionType.PowerAssign:
			case ExpressionType.RightShiftAssign:
			case ExpressionType.SubtractAssign:
			case ExpressionType.AddAssignChecked:
			case ExpressionType.MultiplyAssignChecked:
			case ExpressionType.SubtractAssignChecked:
				return 1;
			case ExpressionType.Constant:
			case ExpressionType.Parameter:
				return 15;
			case ExpressionType.Convert:
			case ExpressionType.ConvertChecked:
			case ExpressionType.Negate:
			case ExpressionType.UnaryPlus:
			case ExpressionType.NegateChecked:
			case ExpressionType.Not:
			case ExpressionType.Decrement:
			case ExpressionType.Increment:
			case ExpressionType.Throw:
			case ExpressionType.Unbox:
			case ExpressionType.PreIncrementAssign:
			case ExpressionType.PreDecrementAssign:
			case ExpressionType.OnesComplement:
			case ExpressionType.IsTrue:
			case ExpressionType.IsFalse:
				return 12;
			case ExpressionType.Divide:
			case ExpressionType.Modulo:
			case ExpressionType.Multiply:
			case ExpressionType.MultiplyChecked:
				return 11;
			case ExpressionType.Equal:
			case ExpressionType.NotEqual:
				return 7;
			case ExpressionType.ExclusiveOr:
				return 5;
			case ExpressionType.GreaterThan:
			case ExpressionType.GreaterThanOrEqual:
			case ExpressionType.LessThan:
			case ExpressionType.LessThanOrEqual:
			case ExpressionType.TypeAs:
			case ExpressionType.TypeIs:
			case ExpressionType.TypeEqual:
				return 8;
			case ExpressionType.LeftShift:
			case ExpressionType.RightShift:
				return 9;
			case ExpressionType.Or:
				return 4;
			case ExpressionType.OrElse:
				return 2;
			case ExpressionType.Power:
				return 13;
			}
			return 14;
		}

		// Token: 0x06001003 RID: 4099 RVA: 0x000366E0 File Offset: 0x000348E0
		private void ParenthesizedVisit(Expression parent, Expression nodeToVisit)
		{
			if (DebugViewWriter.NeedsParentheses(parent, nodeToVisit))
			{
				this.Out("(");
				this.Visit(nodeToVisit);
				this.Out(")");
				return;
			}
			this.Visit(nodeToVisit);
		}

		// Token: 0x06001004 RID: 4100 RVA: 0x00036714 File Offset: 0x00034914
		protected internal override Expression VisitMethodCall(MethodCallExpression node)
		{
			this.Out(".Call ");
			if (node.Object != null)
			{
				this.ParenthesizedVisit(node, node.Object);
			}
			else if (node.Method.DeclaringType != null)
			{
				this.Out(node.Method.DeclaringType.ToString());
			}
			else
			{
				this.Out("<UnknownType>");
			}
			this.Out(".");
			this.Out(node.Method.Name);
			this.VisitExpressions<Expression>('(', node.Arguments);
			return node;
		}

		// Token: 0x06001005 RID: 4101 RVA: 0x000367A4 File Offset: 0x000349A4
		protected internal override Expression VisitNewArray(NewArrayExpression node)
		{
			if (node.NodeType == ExpressionType.NewArrayBounds)
			{
				this.Out(".NewArray " + node.Type.GetElementType().ToString());
				this.VisitExpressions<Expression>('[', node.Expressions);
			}
			else
			{
				this.Out(".NewArray " + node.Type.ToString(), DebugViewWriter.Flow.Space);
				this.VisitExpressions<Expression>('{', node.Expressions);
			}
			return node;
		}

		// Token: 0x06001006 RID: 4102 RVA: 0x00036816 File Offset: 0x00034A16
		protected internal override Expression VisitNew(NewExpression node)
		{
			this.Out(".New " + node.Type.ToString());
			this.VisitExpressions<Expression>('(', node.Arguments);
			return node;
		}

		// Token: 0x06001007 RID: 4103 RVA: 0x00036842 File Offset: 0x00034A42
		protected override ElementInit VisitElementInit(ElementInit node)
		{
			if (node.Arguments.Count == 1)
			{
				this.Visit(node.Arguments[0]);
			}
			else
			{
				this.VisitExpressions<Expression>('{', node.Arguments);
			}
			return node;
		}

		// Token: 0x06001008 RID: 4104 RVA: 0x00036876 File Offset: 0x00034A76
		protected internal override Expression VisitListInit(ListInitExpression node)
		{
			this.Visit(node.NewExpression);
			this.VisitExpressions<ElementInit>('{', ',', node.Initializers, delegate(ElementInit e)
			{
				this.VisitElementInit(e);
			});
			return node;
		}

		// Token: 0x06001009 RID: 4105 RVA: 0x000368A2 File Offset: 0x00034AA2
		protected override MemberAssignment VisitMemberAssignment(MemberAssignment assignment)
		{
			this.Out(assignment.Member.Name);
			this.Out(DebugViewWriter.Flow.Space, "=", DebugViewWriter.Flow.Space);
			this.Visit(assignment.Expression);
			return assignment;
		}

		// Token: 0x0600100A RID: 4106 RVA: 0x000368D0 File Offset: 0x00034AD0
		protected override MemberListBinding VisitMemberListBinding(MemberListBinding binding)
		{
			this.Out(binding.Member.Name);
			this.Out(DebugViewWriter.Flow.Space, "=", DebugViewWriter.Flow.Space);
			this.VisitExpressions<ElementInit>('{', ',', binding.Initializers, delegate(ElementInit e)
			{
				this.VisitElementInit(e);
			});
			return binding;
		}

		// Token: 0x0600100B RID: 4107 RVA: 0x0003690D File Offset: 0x00034B0D
		protected override MemberMemberBinding VisitMemberMemberBinding(MemberMemberBinding binding)
		{
			this.Out(binding.Member.Name);
			this.Out(DebugViewWriter.Flow.Space, "=", DebugViewWriter.Flow.Space);
			this.VisitExpressions<MemberBinding>('{', ',', binding.Bindings, delegate(MemberBinding e)
			{
				this.VisitMemberBinding(e);
			});
			return binding;
		}

		// Token: 0x0600100C RID: 4108 RVA: 0x0003694A File Offset: 0x00034B4A
		protected internal override Expression VisitMemberInit(MemberInitExpression node)
		{
			this.Visit(node.NewExpression);
			this.VisitExpressions<MemberBinding>('{', ',', node.Bindings, delegate(MemberBinding e)
			{
				this.VisitMemberBinding(e);
			});
			return node;
		}

		// Token: 0x0600100D RID: 4109 RVA: 0x00036978 File Offset: 0x00034B78
		protected internal override Expression VisitTypeBinary(TypeBinaryExpression node)
		{
			this.ParenthesizedVisit(node, node.Expression);
			ExpressionType nodeType = node.NodeType;
			if (nodeType != ExpressionType.TypeIs)
			{
				if (nodeType == ExpressionType.TypeEqual)
				{
					this.Out(DebugViewWriter.Flow.Space, ".TypeEqual", DebugViewWriter.Flow.Space);
				}
			}
			else
			{
				this.Out(DebugViewWriter.Flow.Space, ".Is", DebugViewWriter.Flow.Space);
			}
			this.Out(node.TypeOperand.ToString());
			return node;
		}

		// Token: 0x0600100E RID: 4110 RVA: 0x000369D4 File Offset: 0x00034BD4
		protected internal override Expression VisitUnary(UnaryExpression node)
		{
			ExpressionType nodeType = node.NodeType;
			if (nodeType <= ExpressionType.Quote)
			{
				if (nodeType <= ExpressionType.Convert)
				{
					if (nodeType != ExpressionType.ArrayLength)
					{
						if (nodeType == ExpressionType.Convert)
						{
							this.Out("(" + node.Type.ToString() + ")");
						}
					}
				}
				else if (nodeType != ExpressionType.ConvertChecked)
				{
					switch (nodeType)
					{
					case ExpressionType.Negate:
						this.Out("-");
						break;
					case ExpressionType.UnaryPlus:
						this.Out("+");
						break;
					case ExpressionType.NegateChecked:
						this.Out("#-");
						break;
					case ExpressionType.New:
					case ExpressionType.NewArrayInit:
					case ExpressionType.NewArrayBounds:
						break;
					case ExpressionType.Not:
						this.Out((node.Type == typeof(bool)) ? "!" : "~");
						break;
					default:
						if (nodeType == ExpressionType.Quote)
						{
							this.Out("'");
						}
						break;
					}
				}
				else
				{
					this.Out("#(" + node.Type.ToString() + ")");
				}
			}
			else if (nodeType <= ExpressionType.Increment)
			{
				if (nodeType != ExpressionType.TypeAs)
				{
					if (nodeType != ExpressionType.Decrement)
					{
						if (nodeType == ExpressionType.Increment)
						{
							this.Out(".Increment");
						}
					}
					else
					{
						this.Out(".Decrement");
					}
				}
			}
			else if (nodeType != ExpressionType.Throw)
			{
				if (nodeType != ExpressionType.Unbox)
				{
					switch (nodeType)
					{
					case ExpressionType.PreIncrementAssign:
						this.Out("++");
						break;
					case ExpressionType.PreDecrementAssign:
						this.Out("--");
						break;
					case ExpressionType.OnesComplement:
						this.Out("~");
						break;
					case ExpressionType.IsTrue:
						this.Out(".IsTrue");
						break;
					case ExpressionType.IsFalse:
						this.Out(".IsFalse");
						break;
					}
				}
				else
				{
					this.Out(".Unbox");
				}
			}
			else if (node.Operand == null)
			{
				this.Out(".Rethrow");
			}
			else
			{
				this.Out(".Throw", DebugViewWriter.Flow.Space);
			}
			this.ParenthesizedVisit(node, node.Operand);
			nodeType = node.NodeType;
			if (nodeType <= ExpressionType.TypeAs)
			{
				if (nodeType != ExpressionType.ArrayLength)
				{
					if (nodeType == ExpressionType.TypeAs)
					{
						this.Out(DebugViewWriter.Flow.Space, ".As", DebugViewWriter.Flow.Space | DebugViewWriter.Flow.Break);
						this.Out(node.Type.ToString());
					}
				}
				else
				{
					this.Out(".Length");
				}
			}
			else if (nodeType != ExpressionType.PostIncrementAssign)
			{
				if (nodeType == ExpressionType.PostDecrementAssign)
				{
					this.Out("--");
				}
			}
			else
			{
				this.Out("++");
			}
			return node;
		}

		// Token: 0x0600100F RID: 4111 RVA: 0x00036C5C File Offset: 0x00034E5C
		protected internal override Expression VisitBlock(BlockExpression node)
		{
			this.Out(".Block");
			if (node.Type != node.GetExpression(node.ExpressionCount - 1).Type)
			{
				this.Out(string.Format(CultureInfo.CurrentCulture, "<{0}>", node.Type.ToString()));
			}
			this.VisitDeclarations(node.Variables);
			this.Out(" ");
			this.VisitExpressions<Expression>('{', ';', node.Expressions);
			return node;
		}

		// Token: 0x06001010 RID: 4112 RVA: 0x00036CDC File Offset: 0x00034EDC
		protected internal override Expression VisitDefault(DefaultExpression node)
		{
			this.Out(".Default(" + node.Type.ToString() + ")");
			return node;
		}

		// Token: 0x06001011 RID: 4113 RVA: 0x00036CFF File Offset: 0x00034EFF
		protected internal override Expression VisitLabel(LabelExpression node)
		{
			this.Out(".Label", DebugViewWriter.Flow.NewLine);
			this.Indent();
			this.Visit(node.DefaultValue);
			this.Dedent();
			this.NewLine();
			this.DumpLabel(node.Target);
			return node;
		}

		// Token: 0x06001012 RID: 4114 RVA: 0x00036D3C File Offset: 0x00034F3C
		protected internal override Expression VisitGoto(GotoExpression node)
		{
			this.Out("." + node.Kind.ToString(), DebugViewWriter.Flow.Space);
			this.Out(this.GetLabelTargetName(node.Target), DebugViewWriter.Flow.Space);
			this.Out("{", DebugViewWriter.Flow.Space);
			this.Visit(node.Value);
			this.Out(DebugViewWriter.Flow.Space, "}");
			return node;
		}

		// Token: 0x06001013 RID: 4115 RVA: 0x00036DA8 File Offset: 0x00034FA8
		protected internal override Expression VisitLoop(LoopExpression node)
		{
			this.Out(".Loop", DebugViewWriter.Flow.Space);
			if (node.ContinueLabel != null)
			{
				this.DumpLabel(node.ContinueLabel);
			}
			this.Out(" {", DebugViewWriter.Flow.NewLine);
			this.Indent();
			this.Visit(node.Body);
			this.Dedent();
			this.Out(DebugViewWriter.Flow.NewLine, "}");
			if (node.BreakLabel != null)
			{
				this.Out("", DebugViewWriter.Flow.NewLine);
				this.DumpLabel(node.BreakLabel);
			}
			return node;
		}

		// Token: 0x06001014 RID: 4116 RVA: 0x00036E28 File Offset: 0x00035028
		protected override SwitchCase VisitSwitchCase(SwitchCase node)
		{
			foreach (Expression node2 in node.TestValues)
			{
				this.Out(".Case (");
				this.Visit(node2);
				this.Out("):", DebugViewWriter.Flow.NewLine);
			}
			this.Indent();
			this.Indent();
			this.Visit(node.Body);
			this.Dedent();
			this.Dedent();
			this.NewLine();
			return node;
		}

		// Token: 0x06001015 RID: 4117 RVA: 0x00036EBC File Offset: 0x000350BC
		protected internal override Expression VisitSwitch(SwitchExpression node)
		{
			this.Out(".Switch ");
			this.Out("(");
			this.Visit(node.SwitchValue);
			this.Out(") {", DebugViewWriter.Flow.NewLine);
			ExpressionVisitor.Visit<SwitchCase>(node.Cases, new Func<SwitchCase, SwitchCase>(this.VisitSwitchCase));
			if (node.DefaultBody != null)
			{
				this.Out(".Default:", DebugViewWriter.Flow.NewLine);
				this.Indent();
				this.Indent();
				this.Visit(node.DefaultBody);
				this.Dedent();
				this.Dedent();
				this.NewLine();
			}
			this.Out("}");
			return node;
		}

		// Token: 0x06001016 RID: 4118 RVA: 0x00036F5C File Offset: 0x0003515C
		protected override CatchBlock VisitCatchBlock(CatchBlock node)
		{
			this.Out(DebugViewWriter.Flow.NewLine, "} .Catch (" + node.Test.ToString());
			if (node.Variable != null)
			{
				this.Out(DebugViewWriter.Flow.Space, "");
				this.VisitParameter(node.Variable);
			}
			if (node.Filter != null)
			{
				this.Out(") .If (", DebugViewWriter.Flow.Break);
				this.Visit(node.Filter);
			}
			this.Out(") {", DebugViewWriter.Flow.NewLine);
			this.Indent();
			this.Visit(node.Body);
			this.Dedent();
			return node;
		}

		// Token: 0x06001017 RID: 4119 RVA: 0x00036FF4 File Offset: 0x000351F4
		protected internal override Expression VisitTry(TryExpression node)
		{
			this.Out(".Try {", DebugViewWriter.Flow.NewLine);
			this.Indent();
			this.Visit(node.Body);
			this.Dedent();
			ExpressionVisitor.Visit<CatchBlock>(node.Handlers, new Func<CatchBlock, CatchBlock>(this.VisitCatchBlock));
			if (node.Finally != null)
			{
				this.Out(DebugViewWriter.Flow.NewLine, "} .Finally {", DebugViewWriter.Flow.NewLine);
				this.Indent();
				this.Visit(node.Finally);
				this.Dedent();
			}
			else if (node.Fault != null)
			{
				this.Out(DebugViewWriter.Flow.NewLine, "} .Fault {", DebugViewWriter.Flow.NewLine);
				this.Indent();
				this.Visit(node.Fault);
				this.Dedent();
			}
			this.Out(DebugViewWriter.Flow.NewLine, "}");
			return node;
		}

		// Token: 0x06001018 RID: 4120 RVA: 0x000370AC File Offset: 0x000352AC
		protected internal override Expression VisitIndex(IndexExpression node)
		{
			if (node.Indexer != null)
			{
				this.OutMember(node, node.Object, node.Indexer);
			}
			else
			{
				this.ParenthesizedVisit(node, node.Object);
			}
			this.VisitExpressions<Expression>('[', node.Arguments);
			return node;
		}

		// Token: 0x06001019 RID: 4121 RVA: 0x000370F8 File Offset: 0x000352F8
		protected internal override Expression VisitExtension(Expression node)
		{
			this.Out(string.Format(CultureInfo.CurrentCulture, ".Extension<{0}>", node.GetType().ToString()));
			if (node.CanReduce)
			{
				this.Out(DebugViewWriter.Flow.Space, "{", DebugViewWriter.Flow.NewLine);
				this.Indent();
				this.Visit(node.Reduce());
				this.Dedent();
				this.Out(DebugViewWriter.Flow.NewLine, "}");
			}
			return node;
		}

		// Token: 0x0600101A RID: 4122 RVA: 0x00037160 File Offset: 0x00035360
		protected internal override Expression VisitDebugInfo(DebugInfoExpression node)
		{
			this.Out(string.Format(CultureInfo.CurrentCulture, ".DebugInfo({0}: {1}, {2} - {3}, {4})", new object[]
			{
				node.Document.FileName,
				node.StartLine,
				node.StartColumn,
				node.EndLine,
				node.EndColumn
			}));
			return node;
		}

		// Token: 0x0600101B RID: 4123 RVA: 0x000371CF File Offset: 0x000353CF
		private void DumpLabel(LabelTarget target)
		{
			this.Out(string.Format(CultureInfo.CurrentCulture, ".LabelTarget {0}:", this.GetLabelTargetName(target)));
		}

		// Token: 0x0600101C RID: 4124 RVA: 0x000371F0 File Offset: 0x000353F0
		private string GetLabelTargetName(LabelTarget target)
		{
			if (string.IsNullOrEmpty(target.Name))
			{
				return "#Label" + this.GetLabelTargetId(target).ToString();
			}
			return DebugViewWriter.GetDisplayName(target.Name);
		}

		// Token: 0x0600101D RID: 4125 RVA: 0x00037230 File Offset: 0x00035430
		private void WriteLambda(LambdaExpression lambda)
		{
			this.Out(string.Format(CultureInfo.CurrentCulture, ".Lambda {0}<{1}>", this.GetLambdaName(lambda), lambda.Type.ToString()));
			this.VisitDeclarations(lambda.Parameters);
			this.Out(DebugViewWriter.Flow.Space, "{", DebugViewWriter.Flow.NewLine);
			this.Indent();
			this.Visit(lambda.Body);
			this.Dedent();
			this.Out(DebugViewWriter.Flow.NewLine, "}");
		}

		// Token: 0x0600101E RID: 4126 RVA: 0x000372A4 File Offset: 0x000354A4
		private string GetLambdaName(LambdaExpression lambda)
		{
			if (string.IsNullOrEmpty(lambda.Name))
			{
				return "#Lambda" + this.GetLambdaId(lambda).ToString();
			}
			return DebugViewWriter.GetDisplayName(lambda.Name);
		}

		// Token: 0x0600101F RID: 4127 RVA: 0x000372E4 File Offset: 0x000354E4
		private static bool ContainsWhiteSpace(string name)
		{
			for (int i = 0; i < name.Length; i++)
			{
				if (char.IsWhiteSpace(name[i]))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06001020 RID: 4128 RVA: 0x00037315 File Offset: 0x00035515
		private static string QuoteName(string name)
		{
			return string.Format(CultureInfo.CurrentCulture, "'{0}'", name);
		}

		// Token: 0x06001021 RID: 4129 RVA: 0x00037327 File Offset: 0x00035527
		private static string GetDisplayName(string name)
		{
			if (DebugViewWriter.ContainsWhiteSpace(name))
			{
				return DebugViewWriter.QuoteName(name);
			}
			return name;
		}

		// Token: 0x06001022 RID: 4130 RVA: 0x00037339 File Offset: 0x00035539
		[CompilerGenerated]
		private void <VisitExpressions>b__37_0<T>(T e) where T : Expression
		{
			this.Visit(e);
		}

		// Token: 0x06001023 RID: 4131 RVA: 0x00037348 File Offset: 0x00035548
		[CompilerGenerated]
		private void <VisitDeclarations>b__38_0(ParameterExpression variable)
		{
			this.Out(variable.Type.ToString());
			if (variable.IsByRef)
			{
				this.Out("&");
			}
			this.Out(" ");
			this.VisitParameter(variable);
		}

		// Token: 0x06001024 RID: 4132 RVA: 0x00037381 File Offset: 0x00035581
		[CompilerGenerated]
		private void <VisitListInit>b__58_0(ElementInit e)
		{
			this.VisitElementInit(e);
		}

		// Token: 0x06001025 RID: 4133 RVA: 0x00037381 File Offset: 0x00035581
		[CompilerGenerated]
		private void <VisitMemberListBinding>b__60_0(ElementInit e)
		{
			this.VisitElementInit(e);
		}

		// Token: 0x06001026 RID: 4134 RVA: 0x0003738B File Offset: 0x0003558B
		[CompilerGenerated]
		private void <VisitMemberMemberBinding>b__61_0(MemberBinding e)
		{
			this.VisitMemberBinding(e);
		}

		// Token: 0x06001027 RID: 4135 RVA: 0x0003738B File Offset: 0x0003558B
		[CompilerGenerated]
		private void <VisitMemberInit>b__62_0(MemberBinding e)
		{
			this.VisitMemberBinding(e);
		}

		// Token: 0x0400096E RID: 2414
		private const int Tab = 4;

		// Token: 0x0400096F RID: 2415
		private const int MaxColumn = 120;

		// Token: 0x04000970 RID: 2416
		private readonly TextWriter _out;

		// Token: 0x04000971 RID: 2417
		private int _column;

		// Token: 0x04000972 RID: 2418
		private readonly Stack<int> _stack = new Stack<int>();

		// Token: 0x04000973 RID: 2419
		private int _delta;

		// Token: 0x04000974 RID: 2420
		private DebugViewWriter.Flow _flow;

		// Token: 0x04000975 RID: 2421
		private Queue<LambdaExpression> _lambdas;

		// Token: 0x04000976 RID: 2422
		private Dictionary<LambdaExpression, int> _lambdaIds;

		// Token: 0x04000977 RID: 2423
		private Dictionary<ParameterExpression, int> _paramIds;

		// Token: 0x04000978 RID: 2424
		private Dictionary<LabelTarget, int> _labelIds;

		// Token: 0x02000247 RID: 583
		[Flags]
		private enum Flow
		{
			// Token: 0x0400097A RID: 2426
			None = 0,
			// Token: 0x0400097B RID: 2427
			Space = 1,
			// Token: 0x0400097C RID: 2428
			NewLine = 2,
			// Token: 0x0400097D RID: 2429
			Break = 32768
		}
	}
}

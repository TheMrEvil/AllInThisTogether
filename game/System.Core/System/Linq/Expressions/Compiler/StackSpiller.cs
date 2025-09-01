using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Dynamic.Utils;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace System.Linq.Expressions.Compiler
{
	// Token: 0x020002C5 RID: 709
	internal sealed class StackSpiller
	{
		// Token: 0x0600159F RID: 5535 RVA: 0x0004818C File Offset: 0x0004638C
		private StackSpiller.Result RewriteExpression(Expression node, StackSpiller.Stack stack)
		{
			if (node == null)
			{
				return new StackSpiller.Result(StackSpiller.RewriteAction.None, null);
			}
			if (!this._guard.TryEnterOnCurrentStack())
			{
				return this._guard.RunOnEmptyStack<StackSpiller, Expression, StackSpiller.Stack, StackSpiller.Result>((StackSpiller @this, Expression n, StackSpiller.Stack s) => @this.RewriteExpression(n, s), this, node, stack);
			}
			StackSpiller.Result result;
			switch (node.NodeType)
			{
			case ExpressionType.Add:
			case ExpressionType.AddChecked:
			case ExpressionType.And:
			case ExpressionType.ArrayIndex:
			case ExpressionType.Divide:
			case ExpressionType.Equal:
			case ExpressionType.ExclusiveOr:
			case ExpressionType.GreaterThan:
			case ExpressionType.GreaterThanOrEqual:
			case ExpressionType.LeftShift:
			case ExpressionType.LessThan:
			case ExpressionType.LessThanOrEqual:
			case ExpressionType.Modulo:
			case ExpressionType.Multiply:
			case ExpressionType.MultiplyChecked:
			case ExpressionType.NotEqual:
			case ExpressionType.Or:
			case ExpressionType.Power:
			case ExpressionType.RightShift:
			case ExpressionType.Subtract:
			case ExpressionType.SubtractChecked:
				result = this.RewriteBinaryExpression(node, stack);
				break;
			case ExpressionType.AndAlso:
			case ExpressionType.Coalesce:
			case ExpressionType.OrElse:
				result = this.RewriteLogicalBinaryExpression(node, stack);
				break;
			case ExpressionType.ArrayLength:
			case ExpressionType.Convert:
			case ExpressionType.ConvertChecked:
			case ExpressionType.Negate:
			case ExpressionType.UnaryPlus:
			case ExpressionType.NegateChecked:
			case ExpressionType.Not:
			case ExpressionType.TypeAs:
			case ExpressionType.Decrement:
			case ExpressionType.Increment:
			case ExpressionType.Unbox:
			case ExpressionType.OnesComplement:
			case ExpressionType.IsTrue:
			case ExpressionType.IsFalse:
				result = this.RewriteUnaryExpression(node, stack);
				break;
			case ExpressionType.Call:
				result = this.RewriteMethodCallExpression(node, stack);
				break;
			case ExpressionType.Conditional:
				result = this.RewriteConditionalExpression(node, stack);
				break;
			case ExpressionType.Constant:
			case ExpressionType.Parameter:
			case ExpressionType.Quote:
			case ExpressionType.DebugInfo:
			case ExpressionType.Default:
			case ExpressionType.RuntimeVariables:
				result = new StackSpiller.Result(StackSpiller.RewriteAction.None, node);
				break;
			case ExpressionType.Invoke:
				result = this.RewriteInvocationExpression(node, stack);
				break;
			case ExpressionType.Lambda:
				result = StackSpiller.RewriteLambdaExpression(node);
				break;
			case ExpressionType.ListInit:
				result = this.RewriteListInitExpression(node, stack);
				break;
			case ExpressionType.MemberAccess:
				result = this.RewriteMemberExpression(node, stack);
				break;
			case ExpressionType.MemberInit:
				result = this.RewriteMemberInitExpression(node, stack);
				break;
			case ExpressionType.New:
				result = this.RewriteNewExpression(node, stack);
				break;
			case ExpressionType.NewArrayInit:
			case ExpressionType.NewArrayBounds:
				result = this.RewriteNewArrayExpression(node, stack);
				break;
			case ExpressionType.TypeIs:
			case ExpressionType.TypeEqual:
				result = this.RewriteTypeBinaryExpression(node, stack);
				break;
			case ExpressionType.Assign:
				result = this.RewriteAssignBinaryExpression(node, stack);
				break;
			case ExpressionType.Block:
				result = this.RewriteBlockExpression(node, stack);
				break;
			case ExpressionType.Dynamic:
				result = this.RewriteDynamicExpression(node);
				break;
			case ExpressionType.Extension:
				result = this.RewriteExtensionExpression(node, stack);
				break;
			case ExpressionType.Goto:
				result = this.RewriteGotoExpression(node, stack);
				break;
			case ExpressionType.Index:
				result = this.RewriteIndexExpression(node, stack);
				break;
			case ExpressionType.Label:
				result = this.RewriteLabelExpression(node, stack);
				break;
			case ExpressionType.Loop:
				result = this.RewriteLoopExpression(node, stack);
				break;
			case ExpressionType.Switch:
				result = this.RewriteSwitchExpression(node, stack);
				break;
			case ExpressionType.Throw:
				result = this.RewriteThrowUnaryExpression(node, stack);
				break;
			case ExpressionType.Try:
				result = this.RewriteTryExpression(node, stack);
				break;
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
			case ExpressionType.PreIncrementAssign:
			case ExpressionType.PreDecrementAssign:
			case ExpressionType.PostIncrementAssign:
			case ExpressionType.PostDecrementAssign:
				result = this.RewriteReducibleExpression(node, stack);
				break;
			default:
				result = this.RewriteExpression(node.ReduceAndCheck(), stack);
				if (result.Action == StackSpiller.RewriteAction.None)
				{
					result = new StackSpiller.Result(result.Action | StackSpiller.RewriteAction.Copy, result.Node);
				}
				break;
			}
			return result;
		}

		// Token: 0x060015A0 RID: 5536 RVA: 0x000484C2 File Offset: 0x000466C2
		private static Expression MakeBlock(ArrayBuilder<Expression> expressions)
		{
			return new SpilledExpressionBlock(expressions.ToArray());
		}

		// Token: 0x060015A1 RID: 5537 RVA: 0x000484D0 File Offset: 0x000466D0
		private static Expression MakeBlock(params Expression[] expressions)
		{
			return new SpilledExpressionBlock(expressions);
		}

		// Token: 0x060015A2 RID: 5538 RVA: 0x000484D0 File Offset: 0x000466D0
		private static Expression MakeBlock(IReadOnlyList<Expression> expressions)
		{
			return new SpilledExpressionBlock(expressions);
		}

		// Token: 0x060015A3 RID: 5539 RVA: 0x000484D8 File Offset: 0x000466D8
		private ParameterExpression MakeTemp(Type type)
		{
			return this._tm.Temp(type);
		}

		// Token: 0x060015A4 RID: 5540 RVA: 0x000484E6 File Offset: 0x000466E6
		private int Mark()
		{
			return this._tm.Mark();
		}

		// Token: 0x060015A5 RID: 5541 RVA: 0x000484F3 File Offset: 0x000466F3
		private void Free(int mark)
		{
			this._tm.Free(mark);
		}

		// Token: 0x060015A6 RID: 5542 RVA: 0x00003A59 File Offset: 0x00001C59
		[Conditional("DEBUG")]
		private void VerifyTemps()
		{
		}

		// Token: 0x060015A7 RID: 5543 RVA: 0x00048504 File Offset: 0x00046704
		private ParameterExpression ToTemp(Expression expression, out Expression save, bool byRef)
		{
			Type type = byRef ? expression.Type.MakeByRefType() : expression.Type;
			ParameterExpression parameterExpression = this.MakeTemp(type);
			save = AssignBinaryExpression.Make(parameterExpression, expression, byRef);
			return parameterExpression;
		}

		// Token: 0x060015A8 RID: 5544 RVA: 0x0004853B File Offset: 0x0004673B
		internal static LambdaExpression AnalyzeLambda(LambdaExpression lambda)
		{
			return lambda.Accept(new StackSpiller(StackSpiller.Stack.Empty));
		}

		// Token: 0x060015A9 RID: 5545 RVA: 0x00048549 File Offset: 0x00046749
		private StackSpiller(StackSpiller.Stack stack)
		{
			this._startingStack = stack;
		}

		// Token: 0x060015AA RID: 5546 RVA: 0x00048570 File Offset: 0x00046770
		internal Expression<T> Rewrite<T>(Expression<T> lambda)
		{
			StackSpiller.Result result = this.RewriteExpressionFreeTemps(lambda.Body, this._startingStack);
			this._lambdaRewrite = result.Action;
			if (result.Action != StackSpiller.RewriteAction.None)
			{
				Expression expression = result.Node;
				if (this._tm.Temps.Count > 0)
				{
					expression = Expression.Block(this._tm.Temps, new TrueReadOnlyCollection<Expression>(new Expression[]
					{
						expression
					}));
				}
				return Expression<T>.Create(expression, lambda.Name, lambda.TailCall, new ParameterList(lambda));
			}
			return lambda;
		}

		// Token: 0x060015AB RID: 5547 RVA: 0x00003A59 File Offset: 0x00001C59
		[Conditional("DEBUG")]
		private static void VerifyRewrite(StackSpiller.Result result, Expression node)
		{
		}

		// Token: 0x060015AC RID: 5548 RVA: 0x000485F8 File Offset: 0x000467F8
		private StackSpiller.Result RewriteExpressionFreeTemps(Expression expression, StackSpiller.Stack stack)
		{
			int mark = this.Mark();
			StackSpiller.Result result = this.RewriteExpression(expression, stack);
			this.Free(mark);
			return result;
		}

		// Token: 0x060015AD RID: 5549 RVA: 0x0004861C File Offset: 0x0004681C
		private StackSpiller.Result RewriteDynamicExpression(Expression expr)
		{
			IDynamicExpression dynamicExpression = (IDynamicExpression)expr;
			StackSpiller.ChildRewriter childRewriter = new StackSpiller.ChildRewriter(this, StackSpiller.Stack.NonEmpty, dynamicExpression.ArgumentCount);
			childRewriter.AddArguments(dynamicExpression);
			if (childRewriter.Action == StackSpiller.RewriteAction.SpillStack)
			{
				StackSpiller.RequireNoRefArgs(dynamicExpression.DelegateType.GetInvokeMethod());
			}
			return childRewriter.Finish(childRewriter.Rewrite ? dynamicExpression.Rewrite(childRewriter[0, -1]) : expr);
		}

		// Token: 0x060015AE RID: 5550 RVA: 0x00048680 File Offset: 0x00046880
		private StackSpiller.Result RewriteIndexAssignment(BinaryExpression node, StackSpiller.Stack stack)
		{
			IndexExpression indexExpression = (IndexExpression)node.Left;
			StackSpiller.ChildRewriter childRewriter = new StackSpiller.ChildRewriter(this, stack, 2 + indexExpression.ArgumentCount);
			childRewriter.Add(indexExpression.Object);
			childRewriter.AddArguments(indexExpression);
			childRewriter.Add(node.Right);
			if (childRewriter.Action == StackSpiller.RewriteAction.SpillStack)
			{
				childRewriter.MarkRefInstance(indexExpression.Object);
			}
			if (childRewriter.Rewrite)
			{
				node = new AssignBinaryExpression(new IndexExpression(childRewriter[0], indexExpression.Indexer, childRewriter[1, -2]), childRewriter[-1]);
			}
			return childRewriter.Finish(node);
		}

		// Token: 0x060015AF RID: 5551 RVA: 0x00048718 File Offset: 0x00046918
		private StackSpiller.Result RewriteLogicalBinaryExpression(Expression expr, StackSpiller.Stack stack)
		{
			BinaryExpression binaryExpression = (BinaryExpression)expr;
			StackSpiller.Result result = this.RewriteExpression(binaryExpression.Left, stack);
			StackSpiller.Result result2 = this.RewriteExpression(binaryExpression.Right, stack);
			StackSpiller.Result result3 = this.RewriteExpression(binaryExpression.Conversion, stack);
			StackSpiller.RewriteAction rewriteAction = result.Action | result2.Action | result3.Action;
			if (rewriteAction != StackSpiller.RewriteAction.None)
			{
				expr = BinaryExpression.Create(binaryExpression.NodeType, result.Node, result2.Node, binaryExpression.Type, binaryExpression.Method, (LambdaExpression)result3.Node);
			}
			return new StackSpiller.Result(rewriteAction, expr);
		}

		// Token: 0x060015B0 RID: 5552 RVA: 0x000487A4 File Offset: 0x000469A4
		private StackSpiller.Result RewriteReducibleExpression(Expression expr, StackSpiller.Stack stack)
		{
			StackSpiller.Result result = this.RewriteExpression(expr.Reduce(), stack);
			return new StackSpiller.Result(result.Action | StackSpiller.RewriteAction.Copy, result.Node);
		}

		// Token: 0x060015B1 RID: 5553 RVA: 0x000487D4 File Offset: 0x000469D4
		private StackSpiller.Result RewriteBinaryExpression(Expression expr, StackSpiller.Stack stack)
		{
			BinaryExpression binaryExpression = (BinaryExpression)expr;
			StackSpiller.ChildRewriter childRewriter = new StackSpiller.ChildRewriter(this, stack, 3);
			childRewriter.Add(binaryExpression.Left);
			childRewriter.Add(binaryExpression.Right);
			childRewriter.Add(binaryExpression.Conversion);
			if (childRewriter.Action == StackSpiller.RewriteAction.SpillStack)
			{
				StackSpiller.RequireNoRefArgs(binaryExpression.Method);
			}
			return childRewriter.Finish(childRewriter.Rewrite ? BinaryExpression.Create(binaryExpression.NodeType, childRewriter[0], childRewriter[1], binaryExpression.Type, binaryExpression.Method, (LambdaExpression)childRewriter[2]) : expr);
		}

		// Token: 0x060015B2 RID: 5554 RVA: 0x0004886C File Offset: 0x00046A6C
		private StackSpiller.Result RewriteVariableAssignment(BinaryExpression node, StackSpiller.Stack stack)
		{
			StackSpiller.Result result = this.RewriteExpression(node.Right, stack);
			if (result.Action != StackSpiller.RewriteAction.None)
			{
				node = new AssignBinaryExpression(node.Left, result.Node);
			}
			return new StackSpiller.Result(result.Action, node);
		}

		// Token: 0x060015B3 RID: 5555 RVA: 0x000488B0 File Offset: 0x00046AB0
		private StackSpiller.Result RewriteAssignBinaryExpression(Expression expr, StackSpiller.Stack stack)
		{
			BinaryExpression binaryExpression = (BinaryExpression)expr;
			ExpressionType nodeType = binaryExpression.Left.NodeType;
			if (nodeType <= ExpressionType.Parameter)
			{
				if (nodeType == ExpressionType.MemberAccess)
				{
					return this.RewriteMemberAssignment(binaryExpression, stack);
				}
				if (nodeType == ExpressionType.Parameter)
				{
					return this.RewriteVariableAssignment(binaryExpression, stack);
				}
			}
			else
			{
				if (nodeType == ExpressionType.Extension)
				{
					return this.RewriteExtensionAssignment(binaryExpression, stack);
				}
				if (nodeType == ExpressionType.Index)
				{
					return this.RewriteIndexAssignment(binaryExpression, stack);
				}
			}
			throw Error.InvalidLvalue(binaryExpression.Left.NodeType);
		}

		// Token: 0x060015B4 RID: 5556 RVA: 0x00048920 File Offset: 0x00046B20
		private StackSpiller.Result RewriteExtensionAssignment(BinaryExpression node, StackSpiller.Stack stack)
		{
			node = new AssignBinaryExpression(node.Left.ReduceExtensions(), node.Right);
			StackSpiller.Result result = this.RewriteAssignBinaryExpression(node, stack);
			return new StackSpiller.Result(result.Action | StackSpiller.RewriteAction.Copy, result.Node);
		}

		// Token: 0x060015B5 RID: 5557 RVA: 0x00048964 File Offset: 0x00046B64
		private static StackSpiller.Result RewriteLambdaExpression(Expression expr)
		{
			LambdaExpression lambdaExpression = (LambdaExpression)expr;
			expr = StackSpiller.AnalyzeLambda(lambdaExpression);
			return new StackSpiller.Result((expr == lambdaExpression) ? StackSpiller.RewriteAction.None : StackSpiller.RewriteAction.Copy, expr);
		}

		// Token: 0x060015B6 RID: 5558 RVA: 0x00048990 File Offset: 0x00046B90
		private StackSpiller.Result RewriteConditionalExpression(Expression expr, StackSpiller.Stack stack)
		{
			ConditionalExpression conditionalExpression = (ConditionalExpression)expr;
			StackSpiller.Result result = this.RewriteExpression(conditionalExpression.Test, stack);
			StackSpiller.Result result2 = this.RewriteExpression(conditionalExpression.IfTrue, stack);
			StackSpiller.Result result3 = this.RewriteExpression(conditionalExpression.IfFalse, stack);
			StackSpiller.RewriteAction rewriteAction = result.Action | result2.Action | result3.Action;
			if (rewriteAction != StackSpiller.RewriteAction.None)
			{
				expr = ConditionalExpression.Make(result.Node, result2.Node, result3.Node, conditionalExpression.Type);
			}
			return new StackSpiller.Result(rewriteAction, expr);
		}

		// Token: 0x060015B7 RID: 5559 RVA: 0x00048A0C File Offset: 0x00046C0C
		private StackSpiller.Result RewriteMemberAssignment(BinaryExpression node, StackSpiller.Stack stack)
		{
			MemberExpression memberExpression = (MemberExpression)node.Left;
			StackSpiller.ChildRewriter childRewriter = new StackSpiller.ChildRewriter(this, stack, 2);
			childRewriter.Add(memberExpression.Expression);
			childRewriter.Add(node.Right);
			if (childRewriter.Action == StackSpiller.RewriteAction.SpillStack)
			{
				childRewriter.MarkRefInstance(memberExpression.Expression);
			}
			if (childRewriter.Rewrite)
			{
				return childRewriter.Finish(new AssignBinaryExpression(MemberExpression.Make(childRewriter[0], memberExpression.Member), childRewriter[1]));
			}
			return new StackSpiller.Result(StackSpiller.RewriteAction.None, node);
		}

		// Token: 0x060015B8 RID: 5560 RVA: 0x00048A90 File Offset: 0x00046C90
		private StackSpiller.Result RewriteMemberExpression(Expression expr, StackSpiller.Stack stack)
		{
			MemberExpression memberExpression = (MemberExpression)expr;
			StackSpiller.Result result = this.RewriteExpression(memberExpression.Expression, stack);
			if (result.Action != StackSpiller.RewriteAction.None)
			{
				if (result.Action == StackSpiller.RewriteAction.SpillStack && memberExpression.Member is PropertyInfo)
				{
					StackSpiller.RequireNotRefInstance(memberExpression.Expression);
				}
				expr = MemberExpression.Make(result.Node, memberExpression.Member);
			}
			return new StackSpiller.Result(result.Action, expr);
		}

		// Token: 0x060015B9 RID: 5561 RVA: 0x00048AFC File Offset: 0x00046CFC
		private StackSpiller.Result RewriteIndexExpression(Expression expr, StackSpiller.Stack stack)
		{
			IndexExpression indexExpression = (IndexExpression)expr;
			StackSpiller.ChildRewriter childRewriter = new StackSpiller.ChildRewriter(this, stack, indexExpression.ArgumentCount + 1);
			childRewriter.Add(indexExpression.Object);
			childRewriter.AddArguments(indexExpression);
			if (childRewriter.Action == StackSpiller.RewriteAction.SpillStack)
			{
				childRewriter.MarkRefInstance(indexExpression.Object);
			}
			if (childRewriter.Rewrite)
			{
				expr = new IndexExpression(childRewriter[0], indexExpression.Indexer, childRewriter[1, -1]);
			}
			return childRewriter.Finish(expr);
		}

		// Token: 0x060015BA RID: 5562 RVA: 0x00048B74 File Offset: 0x00046D74
		private StackSpiller.Result RewriteMethodCallExpression(Expression expr, StackSpiller.Stack stack)
		{
			MethodCallExpression methodCallExpression = (MethodCallExpression)expr;
			StackSpiller.ChildRewriter childRewriter = new StackSpiller.ChildRewriter(this, stack, methodCallExpression.ArgumentCount + 1);
			childRewriter.Add(methodCallExpression.Object);
			childRewriter.AddArguments(methodCallExpression);
			if (childRewriter.Action == StackSpiller.RewriteAction.SpillStack)
			{
				childRewriter.MarkRefInstance(methodCallExpression.Object);
				childRewriter.MarkRefArgs(methodCallExpression.Method, 1);
			}
			if (childRewriter.Rewrite)
			{
				if (methodCallExpression.Object != null)
				{
					expr = new InstanceMethodCallExpressionN(methodCallExpression.Method, childRewriter[0], childRewriter[1, -1]);
				}
				else
				{
					expr = new MethodCallExpressionN(methodCallExpression.Method, childRewriter[1, -1]);
				}
			}
			return childRewriter.Finish(expr);
		}

		// Token: 0x060015BB RID: 5563 RVA: 0x00048C18 File Offset: 0x00046E18
		private StackSpiller.Result RewriteNewArrayExpression(Expression expr, StackSpiller.Stack stack)
		{
			NewArrayExpression newArrayExpression = (NewArrayExpression)expr;
			if (newArrayExpression.NodeType == ExpressionType.NewArrayInit)
			{
				stack = StackSpiller.Stack.NonEmpty;
			}
			StackSpiller.ChildRewriter childRewriter = new StackSpiller.ChildRewriter(this, stack, newArrayExpression.Expressions.Count);
			childRewriter.Add(newArrayExpression.Expressions);
			if (childRewriter.Rewrite)
			{
				expr = NewArrayExpression.Make(newArrayExpression.NodeType, newArrayExpression.Type, new TrueReadOnlyCollection<Expression>(childRewriter[0, -1]));
			}
			return childRewriter.Finish(expr);
		}

		// Token: 0x060015BC RID: 5564 RVA: 0x00048C88 File Offset: 0x00046E88
		private StackSpiller.Result RewriteInvocationExpression(Expression expr, StackSpiller.Stack stack)
		{
			InvocationExpression invocationExpression = (InvocationExpression)expr;
			LambdaExpression lambdaExpression = invocationExpression.LambdaOperand;
			StackSpiller.ChildRewriter childRewriter;
			if (lambdaExpression != null)
			{
				childRewriter = new StackSpiller.ChildRewriter(this, stack, invocationExpression.ArgumentCount);
				childRewriter.AddArguments(invocationExpression);
				if (childRewriter.Action == StackSpiller.RewriteAction.SpillStack)
				{
					childRewriter.MarkRefArgs(Expression.GetInvokeMethod(invocationExpression.Expression), 0);
				}
				StackSpiller stackSpiller = new StackSpiller(stack);
				lambdaExpression = lambdaExpression.Accept(stackSpiller);
				if (childRewriter.Rewrite || stackSpiller._lambdaRewrite != StackSpiller.RewriteAction.None)
				{
					invocationExpression = new InvocationExpressionN(lambdaExpression, childRewriter[0, -1], invocationExpression.Type);
				}
				StackSpiller.Result result = childRewriter.Finish(invocationExpression);
				return new StackSpiller.Result(result.Action | stackSpiller._lambdaRewrite, result.Node);
			}
			childRewriter = new StackSpiller.ChildRewriter(this, stack, invocationExpression.ArgumentCount + 1);
			childRewriter.Add(invocationExpression.Expression);
			childRewriter.AddArguments(invocationExpression);
			if (childRewriter.Action == StackSpiller.RewriteAction.SpillStack)
			{
				childRewriter.MarkRefArgs(Expression.GetInvokeMethod(invocationExpression.Expression), 1);
			}
			return childRewriter.Finish(childRewriter.Rewrite ? new InvocationExpressionN(childRewriter[0], childRewriter[1, -1], invocationExpression.Type) : expr);
		}

		// Token: 0x060015BD RID: 5565 RVA: 0x00048D9C File Offset: 0x00046F9C
		private StackSpiller.Result RewriteNewExpression(Expression expr, StackSpiller.Stack stack)
		{
			NewExpression newExpression = (NewExpression)expr;
			StackSpiller.ChildRewriter childRewriter = new StackSpiller.ChildRewriter(this, stack, newExpression.ArgumentCount);
			childRewriter.AddArguments(newExpression);
			if (childRewriter.Action == StackSpiller.RewriteAction.SpillStack)
			{
				childRewriter.MarkRefArgs(newExpression.Constructor, 0);
			}
			return childRewriter.Finish(childRewriter.Rewrite ? new NewExpression(newExpression.Constructor, childRewriter[0, -1], newExpression.Members) : expr);
		}

		// Token: 0x060015BE RID: 5566 RVA: 0x00048E08 File Offset: 0x00047008
		private StackSpiller.Result RewriteTypeBinaryExpression(Expression expr, StackSpiller.Stack stack)
		{
			TypeBinaryExpression typeBinaryExpression = (TypeBinaryExpression)expr;
			StackSpiller.Result result = this.RewriteExpression(typeBinaryExpression.Expression, stack);
			if (result.Action != StackSpiller.RewriteAction.None)
			{
				expr = new TypeBinaryExpression(result.Node, typeBinaryExpression.TypeOperand, typeBinaryExpression.NodeType);
			}
			return new StackSpiller.Result(result.Action, expr);
		}

		// Token: 0x060015BF RID: 5567 RVA: 0x00048E58 File Offset: 0x00047058
		private StackSpiller.Result RewriteThrowUnaryExpression(Expression expr, StackSpiller.Stack stack)
		{
			UnaryExpression unaryExpression = (UnaryExpression)expr;
			StackSpiller.Result result = this.RewriteExpressionFreeTemps(unaryExpression.Operand, StackSpiller.Stack.Empty);
			StackSpiller.RewriteAction rewriteAction = result.Action;
			if (stack != StackSpiller.Stack.Empty)
			{
				rewriteAction = StackSpiller.RewriteAction.SpillStack;
			}
			if (rewriteAction != StackSpiller.RewriteAction.None)
			{
				expr = new UnaryExpression(ExpressionType.Throw, result.Node, unaryExpression.Type, null);
			}
			return new StackSpiller.Result(rewriteAction, expr);
		}

		// Token: 0x060015C0 RID: 5568 RVA: 0x00048EA8 File Offset: 0x000470A8
		private StackSpiller.Result RewriteUnaryExpression(Expression expr, StackSpiller.Stack stack)
		{
			UnaryExpression unaryExpression = (UnaryExpression)expr;
			StackSpiller.Result result = this.RewriteExpression(unaryExpression.Operand, stack);
			if (result.Action == StackSpiller.RewriteAction.SpillStack)
			{
				StackSpiller.RequireNoRefArgs(unaryExpression.Method);
			}
			if (result.Action != StackSpiller.RewriteAction.None)
			{
				expr = new UnaryExpression(unaryExpression.NodeType, result.Node, unaryExpression.Type, unaryExpression.Method);
			}
			return new StackSpiller.Result(result.Action, expr);
		}

		// Token: 0x060015C1 RID: 5569 RVA: 0x00048F14 File Offset: 0x00047114
		private StackSpiller.Result RewriteListInitExpression(Expression expr, StackSpiller.Stack stack)
		{
			ListInitExpression listInitExpression = (ListInitExpression)expr;
			StackSpiller.Result result = this.RewriteExpression(listInitExpression.NewExpression, stack);
			Expression node = result.Node;
			StackSpiller.RewriteAction rewriteAction = result.Action;
			ReadOnlyCollection<ElementInit> initializers = listInitExpression.Initializers;
			int count = initializers.Count;
			StackSpiller.ChildRewriter[] array = new StackSpiller.ChildRewriter[count];
			for (int i = 0; i < count; i++)
			{
				ElementInit elementInit = initializers[i];
				StackSpiller.ChildRewriter childRewriter = new StackSpiller.ChildRewriter(this, StackSpiller.Stack.NonEmpty, elementInit.Arguments.Count);
				childRewriter.Add(elementInit.Arguments);
				rewriteAction |= childRewriter.Action;
				array[i] = childRewriter;
			}
			switch (rewriteAction)
			{
			case StackSpiller.RewriteAction.None:
				goto IL_1EA;
			case StackSpiller.RewriteAction.Copy:
			{
				ElementInit[] array2 = new ElementInit[count];
				for (int j = 0; j < count; j++)
				{
					StackSpiller.ChildRewriter childRewriter2 = array[j];
					if (childRewriter2.Action == StackSpiller.RewriteAction.None)
					{
						array2[j] = initializers[j];
					}
					else
					{
						array2[j] = new ElementInit(initializers[j].AddMethod, new TrueReadOnlyCollection<Expression>(childRewriter2[0, -1]));
					}
				}
				expr = new ListInitExpression((NewExpression)node, new TrueReadOnlyCollection<ElementInit>(array2));
				goto IL_1EA;
			}
			case StackSpiller.RewriteAction.SpillStack:
			{
				bool flag = StackSpiller.IsRefInstance(listInitExpression.NewExpression);
				ArrayBuilder<Expression> expressions = new ArrayBuilder<Expression>(count + 2 + (flag ? 1 : 0));
				ParameterExpression parameterExpression = this.MakeTemp(node.Type);
				expressions.UncheckedAdd(new AssignBinaryExpression(parameterExpression, node));
				ParameterExpression parameterExpression2 = parameterExpression;
				if (flag)
				{
					parameterExpression2 = this.MakeTemp(parameterExpression.Type.MakeByRefType());
					expressions.UncheckedAdd(new ByRefAssignBinaryExpression(parameterExpression2, parameterExpression));
				}
				for (int k = 0; k < count; k++)
				{
					StackSpiller.ChildRewriter childRewriter3 = array[k];
					StackSpiller.Result result2 = childRewriter3.Finish(new InstanceMethodCallExpressionN(initializers[k].AddMethod, parameterExpression2, childRewriter3[0, -1]));
					expressions.UncheckedAdd(result2.Node);
				}
				expressions.UncheckedAdd(parameterExpression);
				expr = StackSpiller.MakeBlock(expressions);
				goto IL_1EA;
			}
			}
			throw ContractUtils.Unreachable;
			IL_1EA:
			return new StackSpiller.Result(rewriteAction, expr);
		}

		// Token: 0x060015C2 RID: 5570 RVA: 0x00049114 File Offset: 0x00047314
		private StackSpiller.Result RewriteMemberInitExpression(Expression expr, StackSpiller.Stack stack)
		{
			MemberInitExpression memberInitExpression = (MemberInitExpression)expr;
			StackSpiller.Result result = this.RewriteExpression(memberInitExpression.NewExpression, stack);
			Expression node = result.Node;
			StackSpiller.RewriteAction rewriteAction = result.Action;
			ReadOnlyCollection<MemberBinding> bindings = memberInitExpression.Bindings;
			int count = bindings.Count;
			StackSpiller.BindingRewriter[] array = new StackSpiller.BindingRewriter[count];
			for (int i = 0; i < count; i++)
			{
				StackSpiller.BindingRewriter bindingRewriter = StackSpiller.BindingRewriter.Create(bindings[i], this, StackSpiller.Stack.NonEmpty);
				array[i] = bindingRewriter;
				rewriteAction |= bindingRewriter.Action;
			}
			switch (rewriteAction)
			{
			case StackSpiller.RewriteAction.None:
				goto IL_175;
			case StackSpiller.RewriteAction.Copy:
			{
				MemberBinding[] array2 = new MemberBinding[count];
				for (int j = 0; j < count; j++)
				{
					array2[j] = array[j].AsBinding();
				}
				expr = new MemberInitExpression((NewExpression)node, new TrueReadOnlyCollection<MemberBinding>(array2));
				goto IL_175;
			}
			case StackSpiller.RewriteAction.SpillStack:
			{
				bool flag = StackSpiller.IsRefInstance(memberInitExpression.NewExpression);
				ArrayBuilder<Expression> expressions = new ArrayBuilder<Expression>(count + 2 + (flag ? 1 : 0));
				ParameterExpression parameterExpression = this.MakeTemp(node.Type);
				expressions.UncheckedAdd(new AssignBinaryExpression(parameterExpression, node));
				ParameterExpression parameterExpression2 = parameterExpression;
				if (flag)
				{
					parameterExpression2 = this.MakeTemp(parameterExpression.Type.MakeByRefType());
					expressions.UncheckedAdd(new ByRefAssignBinaryExpression(parameterExpression2, parameterExpression));
				}
				for (int k = 0; k < count; k++)
				{
					Expression item = array[k].AsExpression(parameterExpression2);
					expressions.UncheckedAdd(item);
				}
				expressions.UncheckedAdd(parameterExpression);
				expr = StackSpiller.MakeBlock(expressions);
				goto IL_175;
			}
			}
			throw ContractUtils.Unreachable;
			IL_175:
			return new StackSpiller.Result(rewriteAction, expr);
		}

		// Token: 0x060015C3 RID: 5571 RVA: 0x000492A0 File Offset: 0x000474A0
		private StackSpiller.Result RewriteBlockExpression(Expression expr, StackSpiller.Stack stack)
		{
			BlockExpression blockExpression = (BlockExpression)expr;
			int expressionCount = blockExpression.ExpressionCount;
			StackSpiller.RewriteAction rewriteAction = StackSpiller.RewriteAction.None;
			Expression[] array = null;
			for (int i = 0; i < expressionCount; i++)
			{
				Expression expression = blockExpression.GetExpression(i);
				StackSpiller.Result result = this.RewriteExpression(expression, stack);
				rewriteAction |= result.Action;
				if (array == null && result.Action != StackSpiller.RewriteAction.None)
				{
					array = StackSpiller.Clone<Expression>(blockExpression.Expressions, i);
				}
				if (array != null)
				{
					array[i] = result.Node;
				}
			}
			if (rewriteAction != StackSpiller.RewriteAction.None)
			{
				expr = blockExpression.Rewrite(null, array);
			}
			return new StackSpiller.Result(rewriteAction, expr);
		}

		// Token: 0x060015C4 RID: 5572 RVA: 0x0004932C File Offset: 0x0004752C
		private StackSpiller.Result RewriteLabelExpression(Expression expr, StackSpiller.Stack stack)
		{
			LabelExpression labelExpression = (LabelExpression)expr;
			StackSpiller.Result result = this.RewriteExpression(labelExpression.DefaultValue, stack);
			if (result.Action != StackSpiller.RewriteAction.None)
			{
				expr = new LabelExpression(labelExpression.Target, result.Node);
			}
			return new StackSpiller.Result(result.Action, expr);
		}

		// Token: 0x060015C5 RID: 5573 RVA: 0x00049378 File Offset: 0x00047578
		private StackSpiller.Result RewriteLoopExpression(Expression expr, StackSpiller.Stack stack)
		{
			LoopExpression loopExpression = (LoopExpression)expr;
			StackSpiller.Result result = this.RewriteExpression(loopExpression.Body, StackSpiller.Stack.Empty);
			StackSpiller.RewriteAction rewriteAction = result.Action;
			if (stack != StackSpiller.Stack.Empty)
			{
				rewriteAction = StackSpiller.RewriteAction.SpillStack;
			}
			if (rewriteAction != StackSpiller.RewriteAction.None)
			{
				expr = new LoopExpression(result.Node, loopExpression.BreakLabel, loopExpression.ContinueLabel);
			}
			return new StackSpiller.Result(rewriteAction, expr);
		}

		// Token: 0x060015C6 RID: 5574 RVA: 0x000493CC File Offset: 0x000475CC
		private StackSpiller.Result RewriteGotoExpression(Expression expr, StackSpiller.Stack stack)
		{
			GotoExpression gotoExpression = (GotoExpression)expr;
			StackSpiller.Result result = this.RewriteExpressionFreeTemps(gotoExpression.Value, StackSpiller.Stack.Empty);
			StackSpiller.RewriteAction rewriteAction = result.Action;
			if (stack != StackSpiller.Stack.Empty)
			{
				rewriteAction = StackSpiller.RewriteAction.SpillStack;
			}
			if (rewriteAction != StackSpiller.RewriteAction.None)
			{
				expr = Expression.MakeGoto(gotoExpression.Kind, gotoExpression.Target, result.Node, gotoExpression.Type);
			}
			return new StackSpiller.Result(rewriteAction, expr);
		}

		// Token: 0x060015C7 RID: 5575 RVA: 0x00049424 File Offset: 0x00047624
		private StackSpiller.Result RewriteSwitchExpression(Expression expr, StackSpiller.Stack stack)
		{
			SwitchExpression switchExpression = (SwitchExpression)expr;
			StackSpiller.Result result = this.RewriteExpressionFreeTemps(switchExpression.SwitchValue, stack);
			StackSpiller.RewriteAction rewriteAction = result.Action;
			ReadOnlyCollection<SwitchCase> readOnlyCollection = switchExpression.Cases;
			SwitchCase[] array = null;
			for (int i = 0; i < readOnlyCollection.Count; i++)
			{
				SwitchCase switchCase = readOnlyCollection[i];
				Expression[] array2 = null;
				ReadOnlyCollection<Expression> readOnlyCollection2 = switchCase.TestValues;
				for (int j = 0; j < readOnlyCollection2.Count; j++)
				{
					StackSpiller.Result result2 = this.RewriteExpression(readOnlyCollection2[j], stack);
					rewriteAction |= result2.Action;
					if (array2 == null && result2.Action != StackSpiller.RewriteAction.None)
					{
						array2 = StackSpiller.Clone<Expression>(readOnlyCollection2, j);
					}
					if (array2 != null)
					{
						array2[j] = result2.Node;
					}
				}
				StackSpiller.Result result3 = this.RewriteExpression(switchCase.Body, stack);
				rewriteAction |= result3.Action;
				if (result3.Action != StackSpiller.RewriteAction.None || array2 != null)
				{
					if (array2 != null)
					{
						readOnlyCollection2 = new ReadOnlyCollection<Expression>(array2);
					}
					switchCase = new SwitchCase(result3.Node, readOnlyCollection2);
					if (array == null)
					{
						array = StackSpiller.Clone<SwitchCase>(readOnlyCollection, i);
					}
				}
				if (array != null)
				{
					array[i] = switchCase;
				}
			}
			StackSpiller.Result result4 = this.RewriteExpression(switchExpression.DefaultBody, stack);
			rewriteAction |= result4.Action;
			if (rewriteAction != StackSpiller.RewriteAction.None)
			{
				if (array != null)
				{
					readOnlyCollection = new ReadOnlyCollection<SwitchCase>(array);
				}
				expr = new SwitchExpression(switchExpression.Type, result.Node, result4.Node, switchExpression.Comparison, readOnlyCollection);
			}
			return new StackSpiller.Result(rewriteAction, expr);
		}

		// Token: 0x060015C8 RID: 5576 RVA: 0x00049590 File Offset: 0x00047790
		private StackSpiller.Result RewriteTryExpression(Expression expr, StackSpiller.Stack stack)
		{
			TryExpression tryExpression = (TryExpression)expr;
			StackSpiller.Result result = this.RewriteExpression(tryExpression.Body, StackSpiller.Stack.Empty);
			ReadOnlyCollection<CatchBlock> readOnlyCollection = tryExpression.Handlers;
			CatchBlock[] array = null;
			StackSpiller.RewriteAction rewriteAction = result.Action;
			if (readOnlyCollection != null)
			{
				for (int i = 0; i < readOnlyCollection.Count; i++)
				{
					StackSpiller.RewriteAction rewriteAction2 = result.Action;
					CatchBlock catchBlock = readOnlyCollection[i];
					Expression filter = catchBlock.Filter;
					if (catchBlock.Filter != null)
					{
						StackSpiller.Result result2 = this.RewriteExpression(catchBlock.Filter, StackSpiller.Stack.Empty);
						rewriteAction |= result2.Action;
						rewriteAction2 |= result2.Action;
						filter = result2.Node;
					}
					StackSpiller.Result result3 = this.RewriteExpression(catchBlock.Body, StackSpiller.Stack.Empty);
					rewriteAction |= result3.Action;
					rewriteAction2 |= result3.Action;
					if (rewriteAction2 != StackSpiller.RewriteAction.None)
					{
						catchBlock = Expression.MakeCatchBlock(catchBlock.Test, catchBlock.Variable, result3.Node, filter);
						if (array == null)
						{
							array = StackSpiller.Clone<CatchBlock>(readOnlyCollection, i);
						}
					}
					if (array != null)
					{
						array[i] = catchBlock;
					}
				}
			}
			StackSpiller.Result result4 = this.RewriteExpression(tryExpression.Fault, StackSpiller.Stack.Empty);
			rewriteAction |= result4.Action;
			StackSpiller.Result result5 = this.RewriteExpression(tryExpression.Finally, StackSpiller.Stack.Empty);
			rewriteAction |= result5.Action;
			if (stack != StackSpiller.Stack.Empty)
			{
				rewriteAction = StackSpiller.RewriteAction.SpillStack;
			}
			if (rewriteAction != StackSpiller.RewriteAction.None)
			{
				if (array != null)
				{
					readOnlyCollection = new ReadOnlyCollection<CatchBlock>(array);
				}
				expr = new TryExpression(tryExpression.Type, result.Node, result5.Node, result4.Node, readOnlyCollection);
			}
			return new StackSpiller.Result(rewriteAction, expr);
		}

		// Token: 0x060015C9 RID: 5577 RVA: 0x0004970C File Offset: 0x0004790C
		private StackSpiller.Result RewriteExtensionExpression(Expression expr, StackSpiller.Stack stack)
		{
			StackSpiller.Result result = this.RewriteExpression(expr.ReduceExtensions(), stack);
			return new StackSpiller.Result(result.Action | StackSpiller.RewriteAction.Copy, result.Node);
		}

		// Token: 0x060015CA RID: 5578 RVA: 0x0004973C File Offset: 0x0004793C
		private static T[] Clone<T>(ReadOnlyCollection<T> original, int max)
		{
			T[] array = new T[original.Count];
			for (int i = 0; i < max; i++)
			{
				array[i] = original[i];
			}
			return array;
		}

		// Token: 0x060015CB RID: 5579 RVA: 0x00049770 File Offset: 0x00047970
		private static void RequireNoRefArgs(MethodBase method)
		{
			if (method != null)
			{
				if (method.GetParametersCached().Any((ParameterInfo p) => p.ParameterType.IsByRef))
				{
					throw Error.TryNotSupportedForMethodsWithRefArgs(method);
				}
			}
		}

		// Token: 0x060015CC RID: 5580 RVA: 0x000497AE File Offset: 0x000479AE
		private static void RequireNotRefInstance(Expression instance)
		{
			if (StackSpiller.IsRefInstance(instance))
			{
				throw Error.TryNotSupportedForValueTypeInstances(instance.Type);
			}
		}

		// Token: 0x060015CD RID: 5581 RVA: 0x000497C4 File Offset: 0x000479C4
		private static bool IsRefInstance(Expression instance)
		{
			return instance != null && instance.Type.IsValueType && instance.Type.GetTypeCode() == TypeCode.Object;
		}

		// Token: 0x04000B0B RID: 2827
		private readonly StackGuard _guard = new StackGuard();

		// Token: 0x04000B0C RID: 2828
		private readonly StackSpiller.TempMaker _tm = new StackSpiller.TempMaker();

		// Token: 0x04000B0D RID: 2829
		private readonly StackSpiller.Stack _startingStack;

		// Token: 0x04000B0E RID: 2830
		private StackSpiller.RewriteAction _lambdaRewrite;

		// Token: 0x020002C6 RID: 710
		private abstract class BindingRewriter
		{
			// Token: 0x060015CE RID: 5582 RVA: 0x000497E6 File Offset: 0x000479E6
			internal BindingRewriter(MemberBinding binding, StackSpiller spiller)
			{
				this._binding = binding;
				this._spiller = spiller;
			}

			// Token: 0x170003C0 RID: 960
			// (get) Token: 0x060015CF RID: 5583 RVA: 0x000497FC File Offset: 0x000479FC
			internal StackSpiller.RewriteAction Action
			{
				get
				{
					return this._action;
				}
			}

			// Token: 0x060015D0 RID: 5584
			internal abstract MemberBinding AsBinding();

			// Token: 0x060015D1 RID: 5585
			internal abstract Expression AsExpression(Expression target);

			// Token: 0x060015D2 RID: 5586 RVA: 0x00049804 File Offset: 0x00047A04
			internal static StackSpiller.BindingRewriter Create(MemberBinding binding, StackSpiller spiller, StackSpiller.Stack stack)
			{
				switch (binding.BindingType)
				{
				case MemberBindingType.Assignment:
					return new StackSpiller.MemberAssignmentRewriter((MemberAssignment)binding, spiller, stack);
				case MemberBindingType.MemberBinding:
					return new StackSpiller.MemberMemberBindingRewriter((MemberMemberBinding)binding, spiller, stack);
				case MemberBindingType.ListBinding:
					return new StackSpiller.ListBindingRewriter((MemberListBinding)binding, spiller, stack);
				default:
					throw Error.UnhandledBinding();
				}
			}

			// Token: 0x060015D3 RID: 5587 RVA: 0x0004985C File Offset: 0x00047A5C
			protected void RequireNoValueProperty()
			{
				PropertyInfo propertyInfo = this._binding.Member as PropertyInfo;
				if (propertyInfo != null && propertyInfo.PropertyType.IsValueType)
				{
					throw Error.CannotAutoInitializeValueTypeMemberThroughProperty(propertyInfo);
				}
			}

			// Token: 0x04000B0F RID: 2831
			protected readonly MemberBinding _binding;

			// Token: 0x04000B10 RID: 2832
			protected readonly StackSpiller _spiller;

			// Token: 0x04000B11 RID: 2833
			protected StackSpiller.RewriteAction _action;
		}

		// Token: 0x020002C7 RID: 711
		private sealed class MemberMemberBindingRewriter : StackSpiller.BindingRewriter
		{
			// Token: 0x060015D4 RID: 5588 RVA: 0x00049898 File Offset: 0x00047A98
			internal MemberMemberBindingRewriter(MemberMemberBinding binding, StackSpiller spiller, StackSpiller.Stack stack) : base(binding, spiller)
			{
				this._bindings = binding.Bindings;
				int count = this._bindings.Count;
				this._bindingRewriters = new StackSpiller.BindingRewriter[count];
				for (int i = 0; i < count; i++)
				{
					StackSpiller.BindingRewriter bindingRewriter = StackSpiller.BindingRewriter.Create(this._bindings[i], spiller, stack);
					this._action |= bindingRewriter.Action;
					this._bindingRewriters[i] = bindingRewriter;
				}
			}

			// Token: 0x060015D5 RID: 5589 RVA: 0x00049910 File Offset: 0x00047B10
			internal override MemberBinding AsBinding()
			{
				StackSpiller.RewriteAction action = this._action;
				if (action == StackSpiller.RewriteAction.None)
				{
					return this._binding;
				}
				if (action != StackSpiller.RewriteAction.Copy)
				{
					throw ContractUtils.Unreachable;
				}
				int count = this._bindings.Count;
				MemberBinding[] array = new MemberBinding[count];
				for (int i = 0; i < count; i++)
				{
					array[i] = this._bindingRewriters[i].AsBinding();
				}
				return new MemberMemberBinding(this._binding.Member, new TrueReadOnlyCollection<MemberBinding>(array));
			}

			// Token: 0x060015D6 RID: 5590 RVA: 0x00049980 File Offset: 0x00047B80
			internal override Expression AsExpression(Expression target)
			{
				base.RequireNoValueProperty();
				Expression expression = MemberExpression.Make(target, this._binding.Member);
				Expression expression2 = this._spiller.MakeTemp(expression.Type);
				int count = this._bindings.Count;
				Expression[] array = new Expression[count + 2];
				array[0] = new AssignBinaryExpression(expression2, expression);
				for (int i = 0; i < count; i++)
				{
					StackSpiller.BindingRewriter bindingRewriter = this._bindingRewriters[i];
					array[i + 1] = bindingRewriter.AsExpression(expression2);
				}
				if (expression2.Type.IsValueType)
				{
					array[count + 1] = Expression.Block(typeof(void), new Expression[]
					{
						new AssignBinaryExpression(MemberExpression.Make(target, this._binding.Member), expression2)
					});
				}
				else
				{
					array[count + 1] = Utils.Empty;
				}
				return StackSpiller.MakeBlock(array);
			}

			// Token: 0x04000B12 RID: 2834
			private readonly ReadOnlyCollection<MemberBinding> _bindings;

			// Token: 0x04000B13 RID: 2835
			private readonly StackSpiller.BindingRewriter[] _bindingRewriters;
		}

		// Token: 0x020002C8 RID: 712
		private sealed class ListBindingRewriter : StackSpiller.BindingRewriter
		{
			// Token: 0x060015D7 RID: 5591 RVA: 0x00049A54 File Offset: 0x00047C54
			internal ListBindingRewriter(MemberListBinding binding, StackSpiller spiller, StackSpiller.Stack stack) : base(binding, spiller)
			{
				this._inits = binding.Initializers;
				int count = this._inits.Count;
				this._childRewriters = new StackSpiller.ChildRewriter[count];
				for (int i = 0; i < count; i++)
				{
					ElementInit elementInit = this._inits[i];
					StackSpiller.ChildRewriter childRewriter = new StackSpiller.ChildRewriter(spiller, stack, elementInit.Arguments.Count);
					childRewriter.Add(elementInit.Arguments);
					this._action |= childRewriter.Action;
					this._childRewriters[i] = childRewriter;
				}
			}

			// Token: 0x060015D8 RID: 5592 RVA: 0x00049AE4 File Offset: 0x00047CE4
			internal override MemberBinding AsBinding()
			{
				StackSpiller.RewriteAction action = this._action;
				if (action == StackSpiller.RewriteAction.None)
				{
					return this._binding;
				}
				if (action != StackSpiller.RewriteAction.Copy)
				{
					throw ContractUtils.Unreachable;
				}
				int count = this._inits.Count;
				ElementInit[] array = new ElementInit[count];
				for (int i = 0; i < count; i++)
				{
					StackSpiller.ChildRewriter childRewriter = this._childRewriters[i];
					if (childRewriter.Action == StackSpiller.RewriteAction.None)
					{
						array[i] = this._inits[i];
					}
					else
					{
						array[i] = new ElementInit(this._inits[i].AddMethod, new TrueReadOnlyCollection<Expression>(childRewriter[0, -1]));
					}
				}
				return new MemberListBinding(this._binding.Member, new TrueReadOnlyCollection<ElementInit>(array));
			}

			// Token: 0x060015D9 RID: 5593 RVA: 0x00049B94 File Offset: 0x00047D94
			internal override Expression AsExpression(Expression target)
			{
				base.RequireNoValueProperty();
				Expression expression = MemberExpression.Make(target, this._binding.Member);
				Expression expression2 = this._spiller.MakeTemp(expression.Type);
				int count = this._inits.Count;
				Expression[] array = new Expression[count + 2];
				array[0] = new AssignBinaryExpression(expression2, expression);
				for (int i = 0; i < count; i++)
				{
					StackSpiller.ChildRewriter childRewriter = this._childRewriters[i];
					StackSpiller.Result result = childRewriter.Finish(new InstanceMethodCallExpressionN(this._inits[i].AddMethod, expression2, childRewriter[0, -1]));
					array[i + 1] = result.Node;
				}
				if (expression2.Type.IsValueType)
				{
					array[count + 1] = Expression.Block(typeof(void), new Expression[]
					{
						new AssignBinaryExpression(MemberExpression.Make(target, this._binding.Member), expression2)
					});
				}
				else
				{
					array[count + 1] = Utils.Empty;
				}
				return StackSpiller.MakeBlock(array);
			}

			// Token: 0x04000B14 RID: 2836
			private readonly ReadOnlyCollection<ElementInit> _inits;

			// Token: 0x04000B15 RID: 2837
			private readonly StackSpiller.ChildRewriter[] _childRewriters;
		}

		// Token: 0x020002C9 RID: 713
		private sealed class MemberAssignmentRewriter : StackSpiller.BindingRewriter
		{
			// Token: 0x060015DA RID: 5594 RVA: 0x00049C90 File Offset: 0x00047E90
			internal MemberAssignmentRewriter(MemberAssignment binding, StackSpiller spiller, StackSpiller.Stack stack) : base(binding, spiller)
			{
				StackSpiller.Result result = spiller.RewriteExpression(binding.Expression, stack);
				this._action = result.Action;
				this._rhs = result.Node;
			}

			// Token: 0x060015DB RID: 5595 RVA: 0x00049CCC File Offset: 0x00047ECC
			internal override MemberBinding AsBinding()
			{
				StackSpiller.RewriteAction action = this._action;
				if (action == StackSpiller.RewriteAction.None)
				{
					return this._binding;
				}
				if (action != StackSpiller.RewriteAction.Copy)
				{
					throw ContractUtils.Unreachable;
				}
				return new MemberAssignment(this._binding.Member, this._rhs);
			}

			// Token: 0x060015DC RID: 5596 RVA: 0x00049D0C File Offset: 0x00047F0C
			internal override Expression AsExpression(Expression target)
			{
				Expression expression = MemberExpression.Make(target, this._binding.Member);
				Expression expression2 = this._spiller.MakeTemp(expression.Type);
				return StackSpiller.MakeBlock(new Expression[]
				{
					new AssignBinaryExpression(expression2, this._rhs),
					new AssignBinaryExpression(expression, expression2),
					Utils.Empty
				});
			}

			// Token: 0x04000B16 RID: 2838
			private readonly Expression _rhs;
		}

		// Token: 0x020002CA RID: 714
		private sealed class ChildRewriter
		{
			// Token: 0x060015DD RID: 5597 RVA: 0x00049D69 File Offset: 0x00047F69
			internal ChildRewriter(StackSpiller self, StackSpiller.Stack stack, int count)
			{
				this._self = self;
				this._stack = stack;
				this._expressions = new Expression[count];
			}

			// Token: 0x060015DE RID: 5598 RVA: 0x00049D8C File Offset: 0x00047F8C
			internal void Add(Expression expression)
			{
				int expressionsCount;
				if (expression == null)
				{
					Expression[] expressions = this._expressions;
					expressionsCount = this._expressionsCount;
					this._expressionsCount = expressionsCount + 1;
					expressions[expressionsCount] = null;
					return;
				}
				StackSpiller.Result result = this._self.RewriteExpression(expression, this._stack);
				this._action |= result.Action;
				this._stack = StackSpiller.Stack.NonEmpty;
				if (result.Action == StackSpiller.RewriteAction.SpillStack)
				{
					this._lastSpillIndex = this._expressionsCount;
				}
				Expression[] expressions2 = this._expressions;
				expressionsCount = this._expressionsCount;
				this._expressionsCount = expressionsCount + 1;
				expressions2[expressionsCount] = result.Node;
			}

			// Token: 0x060015DF RID: 5599 RVA: 0x00049E18 File Offset: 0x00048018
			internal void Add(ReadOnlyCollection<Expression> expressions)
			{
				int i = 0;
				int count = expressions.Count;
				while (i < count)
				{
					this.Add(expressions[i]);
					i++;
				}
			}

			// Token: 0x060015E0 RID: 5600 RVA: 0x00049E48 File Offset: 0x00048048
			internal void AddArguments(IArgumentProvider expressions)
			{
				int i = 0;
				int argumentCount = expressions.ArgumentCount;
				while (i < argumentCount)
				{
					this.Add(expressions.GetArgument(i));
					i++;
				}
			}

			// Token: 0x060015E1 RID: 5601 RVA: 0x00049E78 File Offset: 0x00048078
			private void EnsureDone()
			{
				if (!this._done)
				{
					this._done = true;
					if (this._action == StackSpiller.RewriteAction.SpillStack)
					{
						Expression[] expressions = this._expressions;
						int num = this._lastSpillIndex + 1;
						List<Expression> list = new List<Expression>(num + 1);
						for (int i = 0; i < num; i++)
						{
							Expression expression = expressions[i];
							if (StackSpiller.ChildRewriter.ShouldSaveToTemp(expression))
							{
								Expression[] array = expressions;
								int num2 = i;
								StackSpiller self = this._self;
								Expression expression2 = expression;
								bool[] byRefs = this._byRefs;
								Expression item;
								array[num2] = self.ToTemp(expression2, out item, byRefs != null && byRefs[i]);
								list.Add(item);
							}
						}
						list.Capacity = list.Count + 1;
						this._comma = list;
					}
				}
			}

			// Token: 0x060015E2 RID: 5602 RVA: 0x00049F14 File Offset: 0x00048114
			private static bool ShouldSaveToTemp(Expression expression)
			{
				if (expression == null)
				{
					return false;
				}
				ExpressionType nodeType = expression.NodeType;
				if (nodeType <= ExpressionType.MemberAccess)
				{
					if (nodeType != ExpressionType.Constant)
					{
						if (nodeType != ExpressionType.MemberAccess)
						{
							return true;
						}
						FieldInfo fieldInfo = ((MemberExpression)expression).Member as FieldInfo;
						if (!(fieldInfo != null))
						{
							return true;
						}
						if (fieldInfo.IsLiteral)
						{
							return false;
						}
						if (fieldInfo.IsInitOnly && fieldInfo.IsStatic)
						{
							return false;
						}
						return true;
					}
				}
				else if (nodeType != ExpressionType.Default)
				{
					if (nodeType != ExpressionType.RuntimeVariables)
					{
						return true;
					}
					return false;
				}
				return false;
			}

			// Token: 0x170003C1 RID: 961
			// (get) Token: 0x060015E3 RID: 5603 RVA: 0x00049F85 File Offset: 0x00048185
			internal bool Rewrite
			{
				get
				{
					return this._action > StackSpiller.RewriteAction.None;
				}
			}

			// Token: 0x170003C2 RID: 962
			// (get) Token: 0x060015E4 RID: 5604 RVA: 0x00049F90 File Offset: 0x00048190
			internal StackSpiller.RewriteAction Action
			{
				get
				{
					return this._action;
				}
			}

			// Token: 0x060015E5 RID: 5605 RVA: 0x00049F98 File Offset: 0x00048198
			internal void MarkRefInstance(Expression expr)
			{
				if (StackSpiller.IsRefInstance(expr))
				{
					this.MarkRef(0);
				}
			}

			// Token: 0x060015E6 RID: 5606 RVA: 0x00049FAC File Offset: 0x000481AC
			internal void MarkRefArgs(MethodBase method, int startIndex)
			{
				ParameterInfo[] parametersCached = method.GetParametersCached();
				int i = 0;
				int num = parametersCached.Length;
				while (i < num)
				{
					if (parametersCached[i].ParameterType.IsByRef)
					{
						this.MarkRef(startIndex + i);
					}
					i++;
				}
			}

			// Token: 0x060015E7 RID: 5607 RVA: 0x00049FE8 File Offset: 0x000481E8
			private void MarkRef(int index)
			{
				if (this._byRefs == null)
				{
					this._byRefs = new bool[this._expressions.Length];
				}
				this._byRefs[index] = true;
			}

			// Token: 0x060015E8 RID: 5608 RVA: 0x0004A00E File Offset: 0x0004820E
			internal StackSpiller.Result Finish(Expression expression)
			{
				this.EnsureDone();
				if (this._action == StackSpiller.RewriteAction.SpillStack)
				{
					this._comma.Add(expression);
					expression = StackSpiller.MakeBlock(this._comma);
				}
				return new StackSpiller.Result(this._action, expression);
			}

			// Token: 0x170003C3 RID: 963
			internal Expression this[int index]
			{
				get
				{
					this.EnsureDone();
					if (index < 0)
					{
						index += this._expressions.Length;
					}
					return this._expressions[index];
				}
			}

			// Token: 0x170003C4 RID: 964
			internal Expression[] this[int first, int last]
			{
				get
				{
					this.EnsureDone();
					if (last < 0)
					{
						last += this._expressions.Length;
					}
					int num = last - first + 1;
					ContractUtils.RequiresArrayRange<Expression>(this._expressions, first, num, "first", "last");
					if (num == this._expressions.Length)
					{
						return this._expressions;
					}
					Expression[] array = new Expression[num];
					Array.Copy(this._expressions, first, array, 0, num);
					return array;
				}
			}

			// Token: 0x04000B17 RID: 2839
			private readonly StackSpiller _self;

			// Token: 0x04000B18 RID: 2840
			private readonly Expression[] _expressions;

			// Token: 0x04000B19 RID: 2841
			private int _expressionsCount;

			// Token: 0x04000B1A RID: 2842
			private int _lastSpillIndex;

			// Token: 0x04000B1B RID: 2843
			private List<Expression> _comma;

			// Token: 0x04000B1C RID: 2844
			private StackSpiller.RewriteAction _action;

			// Token: 0x04000B1D RID: 2845
			private StackSpiller.Stack _stack;

			// Token: 0x04000B1E RID: 2846
			private bool _done;

			// Token: 0x04000B1F RID: 2847
			private bool[] _byRefs;
		}

		// Token: 0x020002CB RID: 715
		private sealed class TempMaker
		{
			// Token: 0x170003C5 RID: 965
			// (get) Token: 0x060015EB RID: 5611 RVA: 0x0004A0CD File Offset: 0x000482CD
			internal List<ParameterExpression> Temps
			{
				[CompilerGenerated]
				get
				{
					return this.<Temps>k__BackingField;
				}
			} = new List<ParameterExpression>();

			// Token: 0x060015EC RID: 5612 RVA: 0x0004A0D8 File Offset: 0x000482D8
			internal ParameterExpression Temp(Type type)
			{
				ParameterExpression parameterExpression;
				if (this._freeTemps != null)
				{
					for (int i = this._freeTemps.Count - 1; i >= 0; i--)
					{
						parameterExpression = this._freeTemps[i];
						if (parameterExpression.Type == type)
						{
							this._freeTemps.RemoveAt(i);
							return this.UseTemp(parameterExpression);
						}
					}
				}
				string str = "$temp$";
				int temp = this._temp;
				this._temp = temp + 1;
				parameterExpression = ParameterExpression.Make(type, str + temp.ToString(), false);
				this.Temps.Add(parameterExpression);
				return this.UseTemp(parameterExpression);
			}

			// Token: 0x060015ED RID: 5613 RVA: 0x0004A170 File Offset: 0x00048370
			private ParameterExpression UseTemp(ParameterExpression temp)
			{
				if (this._usedTemps == null)
				{
					this._usedTemps = new Stack<ParameterExpression>();
				}
				this._usedTemps.Push(temp);
				return temp;
			}

			// Token: 0x060015EE RID: 5614 RVA: 0x0004A192 File Offset: 0x00048392
			private void FreeTemp(ParameterExpression temp)
			{
				if (this._freeTemps == null)
				{
					this._freeTemps = new List<ParameterExpression>();
				}
				this._freeTemps.Add(temp);
			}

			// Token: 0x060015EF RID: 5615 RVA: 0x0004A1B3 File Offset: 0x000483B3
			internal int Mark()
			{
				Stack<ParameterExpression> usedTemps = this._usedTemps;
				if (usedTemps == null)
				{
					return 0;
				}
				return usedTemps.Count;
			}

			// Token: 0x060015F0 RID: 5616 RVA: 0x0004A1C6 File Offset: 0x000483C6
			internal void Free(int mark)
			{
				if (this._usedTemps != null)
				{
					while (mark < this._usedTemps.Count)
					{
						this.FreeTemp(this._usedTemps.Pop());
					}
				}
			}

			// Token: 0x060015F1 RID: 5617 RVA: 0x00003A59 File Offset: 0x00001C59
			[Conditional("DEBUG")]
			internal void VerifyTemps()
			{
			}

			// Token: 0x060015F2 RID: 5618 RVA: 0x0004A1F1 File Offset: 0x000483F1
			public TempMaker()
			{
			}

			// Token: 0x04000B20 RID: 2848
			private int _temp;

			// Token: 0x04000B21 RID: 2849
			private List<ParameterExpression> _freeTemps;

			// Token: 0x04000B22 RID: 2850
			private Stack<ParameterExpression> _usedTemps;

			// Token: 0x04000B23 RID: 2851
			[CompilerGenerated]
			private readonly List<ParameterExpression> <Temps>k__BackingField;
		}

		// Token: 0x020002CC RID: 716
		private enum Stack
		{
			// Token: 0x04000B25 RID: 2853
			Empty,
			// Token: 0x04000B26 RID: 2854
			NonEmpty
		}

		// Token: 0x020002CD RID: 717
		[Flags]
		private enum RewriteAction
		{
			// Token: 0x04000B28 RID: 2856
			None = 0,
			// Token: 0x04000B29 RID: 2857
			Copy = 1,
			// Token: 0x04000B2A RID: 2858
			SpillStack = 3
		}

		// Token: 0x020002CE RID: 718
		private readonly struct Result
		{
			// Token: 0x060015F3 RID: 5619 RVA: 0x0004A204 File Offset: 0x00048404
			internal Result(StackSpiller.RewriteAction action, Expression node)
			{
				this.Action = action;
				this.Node = node;
			}

			// Token: 0x04000B2B RID: 2859
			internal readonly StackSpiller.RewriteAction Action;

			// Token: 0x04000B2C RID: 2860
			internal readonly Expression Node;
		}

		// Token: 0x020002CF RID: 719
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x060015F4 RID: 5620 RVA: 0x0004A214 File Offset: 0x00048414
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x060015F5 RID: 5621 RVA: 0x00002162 File Offset: 0x00000362
			public <>c()
			{
			}

			// Token: 0x060015F6 RID: 5622 RVA: 0x0004A220 File Offset: 0x00048420
			internal StackSpiller.Result <RewriteExpression>b__6_0(StackSpiller @this, Expression n, StackSpiller.Stack s)
			{
				return @this.RewriteExpression(n, s);
			}

			// Token: 0x060015F7 RID: 5623 RVA: 0x0004A22A File Offset: 0x0004842A
			internal bool <RequireNoRefArgs>b__57_0(ParameterInfo p)
			{
				return p.ParameterType.IsByRef;
			}

			// Token: 0x04000B2D RID: 2861
			public static readonly StackSpiller.<>c <>9 = new StackSpiller.<>c();

			// Token: 0x04000B2E RID: 2862
			public static Func<StackSpiller, Expression, StackSpiller.Stack, StackSpiller.Result> <>9__6_0;

			// Token: 0x04000B2F RID: 2863
			public static Func<ParameterInfo, bool> <>9__57_0;
		}
	}
}

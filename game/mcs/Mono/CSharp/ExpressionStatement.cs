using System;

namespace Mono.CSharp
{
	// Token: 0x020001A5 RID: 421
	public abstract class ExpressionStatement : Expression
	{
		// Token: 0x06001678 RID: 5752 RVA: 0x0000AF70 File Offset: 0x00009170
		public virtual void MarkReachable(Reachability rc)
		{
		}

		// Token: 0x06001679 RID: 5753 RVA: 0x0006C0CC File Offset: 0x0006A2CC
		public ExpressionStatement ResolveStatement(BlockContext ec)
		{
			Expression expression = base.Resolve(ec);
			if (expression == null)
			{
				return null;
			}
			ExpressionStatement expressionStatement = expression as ExpressionStatement;
			if (expressionStatement == null || expression is AnonymousMethodBody)
			{
				base.Error_InvalidExpressionStatement(ec);
			}
			if (MemberAccess.IsValidDotExpression(expression.Type) && !(expression is Assign) && !(expression is Await))
			{
				ExpressionStatement.WarningAsyncWithoutWait(ec, expression);
			}
			return expressionStatement;
		}

		// Token: 0x0600167A RID: 5754 RVA: 0x0006C124 File Offset: 0x0006A324
		private static void WarningAsyncWithoutWait(BlockContext bc, Expression e)
		{
			if (bc.CurrentAnonymousMethod is AsyncInitializer)
			{
				MethodGroupExpr methodGroupExpr = new AwaitStatement.AwaitableMemberAccess(e)
				{
					ProbingMode = true
				}.Resolve(bc) as MethodGroupExpr;
				if (methodGroupExpr == null)
				{
					return;
				}
				Arguments arguments = new Arguments(0);
				methodGroupExpr = methodGroupExpr.OverloadResolve(bc, ref arguments, null, OverloadResolver.Restrictions.ProbingOnly);
				if (methodGroupExpr == null)
				{
					return;
				}
				AwaiterDefinition awaiter = bc.Module.GetAwaiter(methodGroupExpr.BestCandidateReturnType);
				if (!awaiter.IsValidPattern || !awaiter.INotifyCompletion)
				{
					return;
				}
				bc.Report.Warning(4014, 1, e.Location, "The statement is not awaited and execution of current method continues before the call is completed. Consider using `await' operator");
				return;
			}
			else
			{
				Invocation invocation = e as Invocation;
				if (invocation != null && invocation.MethodGroup != null && invocation.MethodGroup.BestCandidate.IsAsync)
				{
					bc.Report.Warning(4014, 1, e.Location, "The statement is not awaited and execution of current method continues before the call is completed. Consider using `await' operator or calling `Wait' method");
					return;
				}
				return;
			}
		}

		// Token: 0x0600167B RID: 5755
		public abstract void EmitStatement(EmitContext ec);

		// Token: 0x0600167C RID: 5756 RVA: 0x0006C1F3 File Offset: 0x0006A3F3
		public override void EmitSideEffect(EmitContext ec)
		{
			this.EmitStatement(ec);
		}

		// Token: 0x0600167D RID: 5757 RVA: 0x00068BDB File Offset: 0x00066DDB
		protected ExpressionStatement()
		{
		}
	}
}

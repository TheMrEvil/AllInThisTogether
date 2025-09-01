using System;
using System.Reflection.Emit;

namespace Mono.CSharp
{
	// Token: 0x020001DF RID: 479
	public class ConditionalLogicalOperator : UserOperatorCall
	{
		// Token: 0x06001917 RID: 6423 RVA: 0x0007BEDF File Offset: 0x0007A0DF
		public ConditionalLogicalOperator(MethodSpec oper, Arguments arguments, Func<ResolveContext, Expression, Expression> expr_tree, bool is_and, Location loc) : base(oper, arguments, expr_tree, loc)
		{
			this.is_and = is_and;
			this.eclass = ExprClass.Unresolved;
		}

		// Token: 0x06001918 RID: 6424 RVA: 0x0007BEFC File Offset: 0x0007A0FC
		protected override Expression DoResolve(ResolveContext ec)
		{
			AParametersCollection parameters = this.oper.Parameters;
			if (!TypeSpecComparer.IsEqual(this.type, parameters.Types[0]) || !TypeSpecComparer.IsEqual(this.type, parameters.Types[1]))
			{
				ec.Report.Error(217, this.loc, "A user-defined operator `{0}' must have each parameter type and return type of the same type in order to be applicable as a short circuit operator", this.oper.GetSignatureForError());
				return null;
			}
			Expression e = new EmptyExpression(this.type);
			Expression operatorTrue = Expression.GetOperatorTrue(ec, e, this.loc);
			Expression operatorFalse = Expression.GetOperatorFalse(ec, e, this.loc);
			if (operatorTrue == null || operatorFalse == null)
			{
				ec.Report.Error(218, this.loc, "The type `{0}' must have operator `true' and operator `false' defined when `{1}' is used as a short circuit operator", this.type.GetSignatureForError(), this.oper.GetSignatureForError());
				return null;
			}
			this.oper_expr = (this.is_and ? operatorFalse : operatorTrue);
			this.eclass = ExprClass.Value;
			return this;
		}

		// Token: 0x06001919 RID: 6425 RVA: 0x0007BFE4 File Offset: 0x0007A1E4
		public override void Emit(EmitContext ec)
		{
			Label label = ec.DefineLabel();
			bool flag = ec.HasSet(BuilderContext.Options.AsyncBody) && this.arguments[1].Expr.ContainsEmitWithAwait();
			if (flag)
			{
				this.arguments[0] = this.arguments[0].EmitToField(ec, false);
				this.arguments[0].Expr.Emit(ec);
			}
			else
			{
				this.arguments[0].Expr.Emit(ec);
				ec.Emit(OpCodes.Dup);
				this.arguments.RemoveAt(0);
			}
			this.oper_expr.EmitBranchable(ec, label, true);
			base.Emit(ec);
			if (flag)
			{
				Label label2 = ec.DefineLabel();
				ec.Emit(OpCodes.Br_S, label2);
				ec.MarkLabel(label);
				this.arguments[0].Expr.Emit(ec);
				ec.MarkLabel(label2);
				return;
			}
			ec.MarkLabel(label);
		}

		// Token: 0x040009BE RID: 2494
		private readonly bool is_and;

		// Token: 0x040009BF RID: 2495
		private Expression oper_expr;
	}
}

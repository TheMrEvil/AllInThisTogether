using System;
using System.Reflection.Emit;

namespace Mono.CSharp.Nullable
{
	// Token: 0x02000302 RID: 770
	public class LiftedUnaryOperator : Unary, IMemoryLocation
	{
		// Token: 0x06002478 RID: 9336 RVA: 0x000AE432 File Offset: 0x000AC632
		public LiftedUnaryOperator(Unary.Operator op, Expression expr, Location loc) : base(op, expr, loc)
		{
		}

		// Token: 0x06002479 RID: 9337 RVA: 0x000AE43D File Offset: 0x000AC63D
		public void AddressOf(EmitContext ec, AddressOp mode)
		{
			this.unwrap.AddressOf(ec, mode);
		}

		// Token: 0x0600247A RID: 9338 RVA: 0x000AE44C File Offset: 0x000AC64C
		public override Expression CreateExpressionTree(ResolveContext ec)
		{
			if (this.user_operator != null)
			{
				return this.user_operator.CreateExpressionTree(ec);
			}
			if (this.Oper == Unary.Operator.UnaryPlus)
			{
				return this.Expr.CreateExpressionTree(ec);
			}
			return base.CreateExpressionTree(ec);
		}

		// Token: 0x0600247B RID: 9339 RVA: 0x000AE480 File Offset: 0x000AC680
		protected override Expression DoResolve(ResolveContext ec)
		{
			this.unwrap = Unwrap.Create(this.Expr, false);
			if (this.unwrap == null)
			{
				return null;
			}
			Expression expression = base.ResolveOperator(ec, this.unwrap);
			if (expression == null)
			{
				this.Error_OperatorCannotBeApplied(ec, this.loc, Unary.OperName(this.Oper), this.Expr.Type);
				return null;
			}
			if (expression != this)
			{
				if (this.user_operator == null)
				{
					return expression;
				}
			}
			else
			{
				expression = (this.Expr = LiftedUnaryOperator.LiftExpression(ec, this.Expr));
			}
			if (expression == null)
			{
				return null;
			}
			this.eclass = ExprClass.Value;
			this.type = expression.Type;
			return this;
		}

		// Token: 0x0600247C RID: 9340 RVA: 0x000AE51C File Offset: 0x000AC71C
		public override void Emit(EmitContext ec)
		{
			Label label = ec.DefineLabel();
			Label label2 = ec.DefineLabel();
			this.unwrap.EmitCheck(ec);
			ec.Emit(OpCodes.Brfalse, label);
			if (this.user_operator != null)
			{
				this.user_operator.Emit(ec);
			}
			else
			{
				base.EmitOperator(ec, NullableInfo.GetUnderlyingType(this.type));
			}
			ec.Emit(OpCodes.Newobj, NullableInfo.GetConstructor(this.type));
			ec.Emit(OpCodes.Br_S, label2);
			ec.MarkLabel(label);
			LiftedNull.Create(this.type, this.loc).Emit(ec);
			ec.MarkLabel(label2);
		}

		// Token: 0x0600247D RID: 9341 RVA: 0x000AE5C0 File Offset: 0x000AC7C0
		private static Expression LiftExpression(ResolveContext ec, Expression expr)
		{
			NullableType nullableType = new NullableType(expr.Type, expr.Location);
			if (nullableType.ResolveAsType(ec, false) == null)
			{
				return null;
			}
			expr.Type = nullableType.Type;
			return expr;
		}

		// Token: 0x0600247E RID: 9342 RVA: 0x000AE5F8 File Offset: 0x000AC7F8
		protected override Expression ResolveEnumOperator(ResolveContext ec, Expression expr, TypeSpec[] predefined)
		{
			expr = base.ResolveEnumOperator(ec, expr, predefined);
			if (expr == null)
			{
				return null;
			}
			this.Expr = LiftedUnaryOperator.LiftExpression(ec, this.Expr);
			return LiftedUnaryOperator.LiftExpression(ec, expr);
		}

		// Token: 0x0600247F RID: 9343 RVA: 0x000AE623 File Offset: 0x000AC823
		protected override Expression ResolveUserOperator(ResolveContext ec, Expression expr)
		{
			expr = base.ResolveUserOperator(ec, expr);
			if (expr == null)
			{
				return null;
			}
			if (this.Expr is Unwrap)
			{
				this.user_operator = LiftedUnaryOperator.LiftExpression(ec, expr);
				return this.user_operator;
			}
			return expr;
		}

		// Token: 0x04000D93 RID: 3475
		private Unwrap unwrap;

		// Token: 0x04000D94 RID: 3476
		private Expression user_operator;
	}
}

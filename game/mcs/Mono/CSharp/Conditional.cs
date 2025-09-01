using System;
using System.Reflection.Emit;

namespace Mono.CSharp
{
	// Token: 0x020001E3 RID: 483
	public class Conditional : Expression
	{
		// Token: 0x06001925 RID: 6437 RVA: 0x0007C53A File Offset: 0x0007A73A
		public Conditional(Expression expr, Expression true_expr, Expression false_expr, Location loc)
		{
			this.expr = expr;
			this.true_expr = true_expr;
			this.false_expr = false_expr;
			this.loc = loc;
		}

		// Token: 0x170005D5 RID: 1493
		// (get) Token: 0x06001926 RID: 6438 RVA: 0x0007C55F File Offset: 0x0007A75F
		public Expression Expr
		{
			get
			{
				return this.expr;
			}
		}

		// Token: 0x170005D6 RID: 1494
		// (get) Token: 0x06001927 RID: 6439 RVA: 0x0007C567 File Offset: 0x0007A767
		public Expression TrueExpr
		{
			get
			{
				return this.true_expr;
			}
		}

		// Token: 0x170005D7 RID: 1495
		// (get) Token: 0x06001928 RID: 6440 RVA: 0x0007C56F File Offset: 0x0007A76F
		public Expression FalseExpr
		{
			get
			{
				return this.false_expr;
			}
		}

		// Token: 0x06001929 RID: 6441 RVA: 0x0007C577 File Offset: 0x0007A777
		public override bool ContainsEmitWithAwait()
		{
			return this.Expr.ContainsEmitWithAwait() || this.true_expr.ContainsEmitWithAwait() || this.false_expr.ContainsEmitWithAwait();
		}

		// Token: 0x0600192A RID: 6442 RVA: 0x0007C5A0 File Offset: 0x0007A7A0
		public override Expression CreateExpressionTree(ResolveContext ec)
		{
			Arguments arguments = new Arguments(3);
			arguments.Add(new Argument(this.expr.CreateExpressionTree(ec)));
			arguments.Add(new Argument(this.true_expr.CreateExpressionTree(ec)));
			arguments.Add(new Argument(this.false_expr.CreateExpressionTree(ec)));
			return base.CreateExpressionFactoryCall(ec, "Condition", arguments);
		}

		// Token: 0x0600192B RID: 6443 RVA: 0x0007C608 File Offset: 0x0007A808
		protected override Expression DoResolve(ResolveContext ec)
		{
			this.expr = this.expr.Resolve(ec);
			this.true_expr = this.true_expr.Resolve(ec);
			this.false_expr = this.false_expr.Resolve(ec);
			if (this.true_expr == null || this.false_expr == null || this.expr == null)
			{
				return null;
			}
			this.eclass = ExprClass.Value;
			TypeSpec type = this.true_expr.Type;
			TypeSpec type2 = this.false_expr.Type;
			this.type = type;
			if (!TypeSpecComparer.IsEqual(type, type2))
			{
				Expression expression = Convert.ImplicitConversion(ec, this.true_expr, type2, this.loc);
				if (expression != null && type.BuiltinType != BuiltinTypeSpec.Type.Dynamic)
				{
					this.type = type2;
					if (type2.BuiltinType != BuiltinTypeSpec.Type.Dynamic)
					{
						Expression expression2 = Convert.ImplicitConversion(ec, this.false_expr, type, this.loc);
						if (expression2 != null)
						{
							if (expression2.Type.BuiltinType == BuiltinTypeSpec.Type.Int && expression is Constant)
							{
								this.type = type;
								expression2 = null;
							}
							else if (this.type.BuiltinType == BuiltinTypeSpec.Type.Int && expression2 is Constant)
							{
								expression2 = null;
							}
						}
						if (expression2 != null)
						{
							ec.Report.Error(172, this.true_expr.Location, "Type of conditional expression cannot be determined as `{0}' and `{1}' convert implicitly to each other", type.GetSignatureForError(), type2.GetSignatureForError());
						}
					}
					this.true_expr = expression;
					if (this.true_expr.Type != this.type)
					{
						this.true_expr = EmptyCast.Create(this.true_expr, this.type);
					}
				}
				else
				{
					if ((expression = Convert.ImplicitConversion(ec, this.false_expr, type, this.loc)) == null)
					{
						if (type2 != InternalType.ErrorType)
						{
							ec.Report.Error(173, this.true_expr.Location, "Type of conditional expression cannot be determined because there is no implicit conversion between `{0}' and `{1}'", type.GetSignatureForError(), type2.GetSignatureForError());
						}
						return null;
					}
					this.false_expr = expression;
				}
			}
			Constant constant = this.expr as Constant;
			if (constant != null)
			{
				bool isDefaultValue = constant.IsDefaultValue;
				if (!(isDefaultValue ? (this.true_expr is Constant) : (this.false_expr is Constant)))
				{
					Expression.Warning_UnreachableExpression(ec, isDefaultValue ? this.true_expr.Location : this.false_expr.Location);
				}
				return ReducedExpression.Create(isDefaultValue ? this.false_expr : this.true_expr, this, this.false_expr is Constant && this.true_expr is Constant).Resolve(ec);
			}
			return this;
		}

		// Token: 0x0600192C RID: 6444 RVA: 0x0007C880 File Offset: 0x0007AA80
		public override void Emit(EmitContext ec)
		{
			Label label = ec.DefineLabel();
			Label label2 = ec.DefineLabel();
			this.expr.EmitBranchable(ec, label, false);
			this.true_expr.Emit(ec);
			if (this.type.IsInterface && this.true_expr is EmptyCast && this.false_expr is EmptyCast)
			{
				LocalBuilder temporaryLocal = ec.GetTemporaryLocal(this.type);
				ec.Emit(OpCodes.Stloc, temporaryLocal);
				ec.Emit(OpCodes.Ldloc, temporaryLocal);
				ec.FreeTemporaryLocal(temporaryLocal, this.type);
			}
			ec.Emit(OpCodes.Br, label2);
			ec.MarkLabel(label);
			this.false_expr.Emit(ec);
			ec.MarkLabel(label2);
		}

		// Token: 0x0600192D RID: 6445 RVA: 0x0007C934 File Offset: 0x0007AB34
		public override void FlowAnalysis(FlowAnalysisContext fc)
		{
			this.expr.FlowAnalysisConditional(fc);
			DefiniteAssignmentBitSet definiteAssignmentOnTrue = fc.DefiniteAssignmentOnTrue;
			DefiniteAssignmentBitSet definiteAssignmentOnFalse = fc.DefiniteAssignmentOnFalse;
			fc.BranchDefiniteAssignment(definiteAssignmentOnTrue);
			this.true_expr.FlowAnalysis(fc);
			DefiniteAssignmentBitSet definiteAssignment = fc.DefiniteAssignment;
			fc.BranchDefiniteAssignment(definiteAssignmentOnFalse);
			this.false_expr.FlowAnalysis(fc);
			fc.DefiniteAssignment &= definiteAssignment;
		}

		// Token: 0x0600192E RID: 6446 RVA: 0x0007C9A0 File Offset: 0x0007ABA0
		public override void FlowAnalysisConditional(FlowAnalysisContext fc)
		{
			this.expr.FlowAnalysisConditional(fc);
			DefiniteAssignmentBitSet definiteAssignmentOnTrue = fc.DefiniteAssignmentOnTrue;
			DefiniteAssignmentBitSet definiteAssignmentOnFalse = fc.DefiniteAssignmentOnFalse;
			fc.DefiniteAssignmentOnTrue = (fc.DefiniteAssignmentOnFalse = (fc.DefiniteAssignment = new DefiniteAssignmentBitSet(definiteAssignmentOnTrue)));
			this.true_expr.FlowAnalysisConditional(fc);
			DefiniteAssignmentBitSet definiteAssignment = fc.DefiniteAssignment;
			DefiniteAssignmentBitSet definiteAssignmentOnTrue2 = fc.DefiniteAssignmentOnTrue;
			DefiniteAssignmentBitSet definiteAssignmentOnFalse2 = fc.DefiniteAssignmentOnFalse;
			fc.DefiniteAssignmentOnTrue = (fc.DefiniteAssignmentOnFalse = (fc.DefiniteAssignment = new DefiniteAssignmentBitSet(definiteAssignmentOnFalse)));
			this.false_expr.FlowAnalysisConditional(fc);
			fc.DefiniteAssignment &= definiteAssignment;
			fc.DefiniteAssignmentOnTrue = (definiteAssignmentOnTrue2 & fc.DefiniteAssignmentOnTrue);
			fc.DefiniteAssignmentOnFalse = (definiteAssignmentOnFalse2 & fc.DefiniteAssignmentOnFalse);
		}

		// Token: 0x0600192F RID: 6447 RVA: 0x0007CA75 File Offset: 0x0007AC75
		protected override void CloneTo(CloneContext clonectx, Expression t)
		{
			Conditional conditional = (Conditional)t;
			conditional.expr = this.expr.Clone(clonectx);
			conditional.true_expr = this.true_expr.Clone(clonectx);
			conditional.false_expr = this.false_expr.Clone(clonectx);
		}

		// Token: 0x040009C3 RID: 2499
		private Expression expr;

		// Token: 0x040009C4 RID: 2500
		private Expression true_expr;

		// Token: 0x040009C5 RID: 2501
		private Expression false_expr;
	}
}

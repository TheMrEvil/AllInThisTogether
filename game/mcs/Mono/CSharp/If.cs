using System;
using System.Reflection.Emit;

namespace Mono.CSharp
{
	// Token: 0x0200029B RID: 667
	public class If : Statement
	{
		// Token: 0x06002045 RID: 8261 RVA: 0x0009F6E2 File Offset: 0x0009D8E2
		public If(Expression bool_expr, Statement true_statement, Location l) : this(bool_expr, true_statement, null, l)
		{
		}

		// Token: 0x06002046 RID: 8262 RVA: 0x0009F6EE File Offset: 0x0009D8EE
		public If(Expression bool_expr, Statement true_statement, Statement false_statement, Location l)
		{
			this.expr = bool_expr;
			this.TrueStatement = true_statement;
			this.FalseStatement = false_statement;
			this.loc = l;
		}

		// Token: 0x17000769 RID: 1897
		// (get) Token: 0x06002047 RID: 8263 RVA: 0x0009F713 File Offset: 0x0009D913
		public Expression Expr
		{
			get
			{
				return this.expr;
			}
		}

		// Token: 0x06002048 RID: 8264 RVA: 0x0009F71C File Offset: 0x0009D91C
		public override bool Resolve(BlockContext ec)
		{
			this.expr = this.expr.Resolve(ec);
			bool flag = this.TrueStatement.Resolve(ec);
			if (this.FalseStatement != null)
			{
				flag &= this.FalseStatement.Resolve(ec);
			}
			return flag;
		}

		// Token: 0x06002049 RID: 8265 RVA: 0x0009F760 File Offset: 0x0009D960
		protected override void DoEmit(EmitContext ec)
		{
			Label label = ec.DefineLabel();
			Constant constant = this.expr as Constant;
			if (constant == null)
			{
				this.expr.EmitBranchable(ec, label, false);
				this.TrueStatement.Emit(ec);
				if (this.FalseStatement != null)
				{
					bool flag = false;
					Label label2 = ec.DefineLabel();
					if (!this.true_returns)
					{
						ec.Emit(OpCodes.Br, label2);
						flag = true;
					}
					ec.MarkLabel(label);
					this.FalseStatement.Emit(ec);
					if (flag)
					{
						ec.MarkLabel(label2);
						return;
					}
				}
				else
				{
					ec.MarkLabel(label);
				}
				return;
			}
			constant.EmitSideEffect(ec);
			if (!constant.IsDefaultValue)
			{
				this.TrueStatement.Emit(ec);
				return;
			}
			if (this.FalseStatement != null)
			{
				this.FalseStatement.Emit(ec);
			}
		}

		// Token: 0x0600204A RID: 8266 RVA: 0x0009F81C File Offset: 0x0009DA1C
		protected override bool DoFlowAnalysis(FlowAnalysisContext fc)
		{
			this.expr.FlowAnalysisConditional(fc);
			DefiniteAssignmentBitSet definiteAssignmentBitSet = new DefiniteAssignmentBitSet(fc.DefiniteAssignmentOnFalse);
			fc.DefiniteAssignment = fc.DefiniteAssignmentOnTrue;
			bool flag = this.TrueStatement.FlowAnalysis(fc);
			if (this.FalseStatement == null)
			{
				Constant constant = this.expr as Constant;
				if (constant != null && !constant.IsDefaultValue)
				{
					return this.true_returns;
				}
				if (this.true_returns)
				{
					fc.DefiniteAssignment = definiteAssignmentBitSet;
				}
				else
				{
					fc.DefiniteAssignment &= definiteAssignmentBitSet;
				}
				return false;
			}
			else
			{
				if (this.true_returns)
				{
					fc.DefiniteAssignment = definiteAssignmentBitSet;
					return this.FalseStatement.FlowAnalysis(fc);
				}
				DefiniteAssignmentBitSet definiteAssignment = fc.DefiniteAssignment;
				fc.DefiniteAssignment = definiteAssignmentBitSet;
				flag &= this.FalseStatement.FlowAnalysis(fc);
				if (!this.TrueStatement.IsUnreachable)
				{
					if (this.false_returns || this.FalseStatement.IsUnreachable)
					{
						fc.DefiniteAssignment = definiteAssignment;
					}
					else
					{
						fc.DefiniteAssignment &= definiteAssignment;
					}
				}
				return flag;
			}
		}

		// Token: 0x0600204B RID: 8267 RVA: 0x0009F928 File Offset: 0x0009DB28
		public override Reachability MarkReachable(Reachability rc)
		{
			if (rc.IsUnreachable)
			{
				return rc;
			}
			base.MarkReachable(rc);
			Constant constant = this.expr as Constant;
			if (constant != null)
			{
				if (!constant.IsDefaultValue)
				{
					rc = this.TrueStatement.MarkReachable(rc);
				}
				else if (this.FalseStatement != null)
				{
					rc = this.FalseStatement.MarkReachable(rc);
				}
				return rc;
			}
			Reachability a = this.TrueStatement.MarkReachable(rc);
			this.true_returns = a.IsUnreachable;
			if (this.FalseStatement == null)
			{
				return rc;
			}
			Reachability b = this.FalseStatement.MarkReachable(rc);
			this.false_returns = b.IsUnreachable;
			return a & b;
		}

		// Token: 0x0600204C RID: 8268 RVA: 0x0009F9D0 File Offset: 0x0009DBD0
		protected override void CloneTo(CloneContext clonectx, Statement t)
		{
			If @if = (If)t;
			@if.expr = this.expr.Clone(clonectx);
			@if.TrueStatement = this.TrueStatement.Clone(clonectx);
			if (this.FalseStatement != null)
			{
				@if.FalseStatement = this.FalseStatement.Clone(clonectx);
			}
		}

		// Token: 0x0600204D RID: 8269 RVA: 0x0009FA22 File Offset: 0x0009DC22
		public override object Accept(StructuralVisitor visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x04000C10 RID: 3088
		private Expression expr;

		// Token: 0x04000C11 RID: 3089
		public Statement TrueStatement;

		// Token: 0x04000C12 RID: 3090
		public Statement FalseStatement;

		// Token: 0x04000C13 RID: 3091
		private bool true_returns;

		// Token: 0x04000C14 RID: 3092
		private bool false_returns;
	}
}

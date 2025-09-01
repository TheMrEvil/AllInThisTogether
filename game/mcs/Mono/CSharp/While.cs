using System;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace Mono.CSharp
{
	// Token: 0x0200029D RID: 669
	public class While : LoopStatement
	{
		// Token: 0x06002059 RID: 8281 RVA: 0x0009FC6C File Offset: 0x0009DE6C
		public While(BooleanExpression bool_expr, Statement statement, Location l) : base(statement)
		{
			this.expr = bool_expr;
			this.loc = l;
		}

		// Token: 0x0600205A RID: 8282 RVA: 0x0009FC84 File Offset: 0x0009DE84
		public override bool Resolve(BlockContext bc)
		{
			bool flag = true;
			this.expr = this.expr.Resolve(bc);
			if (this.expr == null)
			{
				flag = false;
			}
			Constant constant = this.expr as Constant;
			if (constant != null)
			{
				this.empty = constant.IsDefaultValue;
				this.infinite = !this.empty;
			}
			return flag & base.Resolve(bc);
		}

		// Token: 0x0600205B RID: 8283 RVA: 0x0009FCE4 File Offset: 0x0009DEE4
		protected override void DoEmit(EmitContext ec)
		{
			if (this.empty)
			{
				this.expr.EmitSideEffect(ec);
				return;
			}
			Label loopBegin = ec.LoopBegin;
			Label loopEnd = ec.LoopEnd;
			ec.LoopBegin = ec.DefineLabel();
			ec.LoopEnd = ec.DefineLabel();
			if (this.expr is Constant)
			{
				ec.MarkLabel(ec.LoopBegin);
				if (ec.EmitAccurateDebugInfo)
				{
					ec.Emit(OpCodes.Nop);
				}
				this.expr.EmitSideEffect(ec);
				base.Statement.Emit(ec);
				ec.Emit(OpCodes.Br, ec.LoopBegin);
				ec.MarkLabel(ec.LoopEnd);
			}
			else
			{
				Label label = ec.DefineLabel();
				ec.Emit(OpCodes.Br, ec.LoopBegin);
				ec.MarkLabel(label);
				base.Statement.Emit(ec);
				ec.MarkLabel(ec.LoopBegin);
				ec.Mark(this.loc);
				this.expr.EmitBranchable(ec, label, true);
				ec.MarkLabel(ec.LoopEnd);
			}
			ec.LoopBegin = loopBegin;
			ec.LoopEnd = loopEnd;
		}

		// Token: 0x0600205C RID: 8284 RVA: 0x0009FDFC File Offset: 0x0009DFFC
		protected override bool DoFlowAnalysis(FlowAnalysisContext fc)
		{
			this.expr.FlowAnalysisConditional(fc);
			fc.DefiniteAssignment = fc.DefiniteAssignmentOnTrue;
			DefiniteAssignmentBitSet definiteAssignment = new DefiniteAssignmentBitSet(fc.DefiniteAssignmentOnFalse);
			base.Statement.FlowAnalysis(fc);
			if (this.end_reachable_das != null)
			{
				definiteAssignment = DefiniteAssignmentBitSet.And(this.end_reachable_das);
				this.end_reachable_das = null;
			}
			fc.DefiniteAssignment = definiteAssignment;
			return this.infinite && !this.end_reachable;
		}

		// Token: 0x0600205D RID: 8285 RVA: 0x0009FE70 File Offset: 0x0009E070
		public override Reachability MarkReachable(Reachability rc)
		{
			if (rc.IsUnreachable)
			{
				return rc;
			}
			base.MarkReachable(rc);
			if (this.empty)
			{
				base.Statement.MarkReachable(Reachability.CreateUnreachable());
				return rc;
			}
			base.Statement.MarkReachable(rc);
			if (this.infinite && !this.end_reachable)
			{
				return Reachability.CreateUnreachable();
			}
			return rc;
		}

		// Token: 0x0600205E RID: 8286 RVA: 0x0009FECF File Offset: 0x0009E0CF
		protected override void CloneTo(CloneContext clonectx, Statement t)
		{
			While @while = (While)t;
			@while.expr = this.expr.Clone(clonectx);
			@while.Statement = base.Statement.Clone(clonectx);
		}

		// Token: 0x0600205F RID: 8287 RVA: 0x0009FEFA File Offset: 0x0009E0FA
		public override object Accept(StructuralVisitor visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x06002060 RID: 8288 RVA: 0x0009FF03 File Offset: 0x0009E103
		public override void AddEndDefiniteAssignment(FlowAnalysisContext fc)
		{
			if (!this.infinite)
			{
				return;
			}
			if (this.end_reachable_das == null)
			{
				this.end_reachable_das = new List<DefiniteAssignmentBitSet>();
			}
			this.end_reachable_das.Add(fc.DefiniteAssignment);
		}

		// Token: 0x06002061 RID: 8289 RVA: 0x0009FF32 File Offset: 0x0009E132
		public override void SetEndReachable()
		{
			this.end_reachable = true;
		}

		// Token: 0x04000C19 RID: 3097
		public Expression expr;

		// Token: 0x04000C1A RID: 3098
		private bool empty;

		// Token: 0x04000C1B RID: 3099
		private bool infinite;

		// Token: 0x04000C1C RID: 3100
		private bool end_reachable;

		// Token: 0x04000C1D RID: 3101
		private List<DefiniteAssignmentBitSet> end_reachable_das;
	}
}

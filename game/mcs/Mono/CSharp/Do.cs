using System;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;

namespace Mono.CSharp
{
	// Token: 0x0200029C RID: 668
	public class Do : LoopStatement
	{
		// Token: 0x0600204E RID: 8270 RVA: 0x0009FA2B File Offset: 0x0009DC2B
		public Do(Statement statement, BooleanExpression bool_expr, Location doLocation, Location whileLocation) : base(statement)
		{
			this.expr = bool_expr;
			this.loc = doLocation;
			this.WhileLocation = whileLocation;
		}

		// Token: 0x1700076A RID: 1898
		// (get) Token: 0x0600204F RID: 8271 RVA: 0x0009FA4A File Offset: 0x0009DC4A
		// (set) Token: 0x06002050 RID: 8272 RVA: 0x0009FA52 File Offset: 0x0009DC52
		public Location WhileLocation
		{
			[CompilerGenerated]
			get
			{
				return this.<WhileLocation>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<WhileLocation>k__BackingField = value;
			}
		}

		// Token: 0x06002051 RID: 8273 RVA: 0x0009FA5B File Offset: 0x0009DC5B
		public override bool Resolve(BlockContext bc)
		{
			bool result = base.Resolve(bc);
			this.expr = this.expr.Resolve(bc);
			return result;
		}

		// Token: 0x06002052 RID: 8274 RVA: 0x0009FA78 File Offset: 0x0009DC78
		protected override void DoEmit(EmitContext ec)
		{
			Label label = ec.DefineLabel();
			Label loopBegin = ec.LoopBegin;
			Label loopEnd = ec.LoopEnd;
			ec.LoopBegin = ec.DefineLabel();
			ec.LoopEnd = ec.DefineLabel();
			ec.MarkLabel(label);
			base.Statement.Emit(ec);
			ec.MarkLabel(ec.LoopBegin);
			ec.Mark(this.WhileLocation);
			if (this.expr is Constant)
			{
				bool flag = !((Constant)this.expr).IsDefaultValue;
				this.expr.EmitSideEffect(ec);
				if (flag)
				{
					ec.Emit(OpCodes.Br, label);
				}
			}
			else
			{
				this.expr.EmitBranchable(ec, label, true);
			}
			ec.MarkLabel(ec.LoopEnd);
			ec.LoopBegin = loopBegin;
			ec.LoopEnd = loopEnd;
		}

		// Token: 0x06002053 RID: 8275 RVA: 0x0009FB44 File Offset: 0x0009DD44
		protected override bool DoFlowAnalysis(FlowAnalysisContext fc)
		{
			bool flag = base.Statement.FlowAnalysis(fc);
			this.expr.FlowAnalysisConditional(fc);
			fc.DefiniteAssignment = fc.DefiniteAssignmentOnFalse;
			if (flag && !this.iterator_reachable)
			{
				return !this.end_reachable;
			}
			if (!this.end_reachable)
			{
				Constant constant = this.expr as Constant;
				if (constant != null && !constant.IsDefaultValue)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06002054 RID: 8276 RVA: 0x0009FBAC File Offset: 0x0009DDAC
		public override Reachability MarkReachable(Reachability rc)
		{
			base.MarkReachable(rc);
			if (!base.Statement.MarkReachable(rc).IsUnreachable || this.iterator_reachable)
			{
				if (!this.end_reachable)
				{
					Constant constant = this.expr as Constant;
					if (constant != null && !constant.IsDefaultValue)
					{
						return Reachability.CreateUnreachable();
					}
				}
				return rc;
			}
			this.expr = new UnreachableExpression(this.expr);
			if (!this.end_reachable)
			{
				return Reachability.CreateUnreachable();
			}
			return rc;
		}

		// Token: 0x06002055 RID: 8277 RVA: 0x0009FC26 File Offset: 0x0009DE26
		protected override void CloneTo(CloneContext clonectx, Statement t)
		{
			Do @do = (Do)t;
			@do.Statement = base.Statement.Clone(clonectx);
			@do.expr = this.expr.Clone(clonectx);
		}

		// Token: 0x06002056 RID: 8278 RVA: 0x0009FC51 File Offset: 0x0009DE51
		public override object Accept(StructuralVisitor visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x06002057 RID: 8279 RVA: 0x0009FC5A File Offset: 0x0009DE5A
		public override void SetEndReachable()
		{
			this.end_reachable = true;
		}

		// Token: 0x06002058 RID: 8280 RVA: 0x0009FC63 File Offset: 0x0009DE63
		public override void SetIteratorReachable()
		{
			this.iterator_reachable = true;
		}

		// Token: 0x04000C15 RID: 3093
		public Expression expr;

		// Token: 0x04000C16 RID: 3094
		private bool iterator_reachable;

		// Token: 0x04000C17 RID: 3095
		private bool end_reachable;

		// Token: 0x04000C18 RID: 3096
		[CompilerGenerated]
		private Location <WhileLocation>k__BackingField;
	}
}

using System;

namespace Mono.CSharp
{
	// Token: 0x0200022D RID: 557
	public abstract class YieldStatement<T> : ResumableStatement where T : StateMachineInitializer
	{
		// Token: 0x06001C38 RID: 7224 RVA: 0x000892B2 File Offset: 0x000874B2
		protected YieldStatement(Expression expr, Location l)
		{
			this.expr = expr;
			this.loc = l;
		}

		// Token: 0x1700066B RID: 1643
		// (get) Token: 0x06001C39 RID: 7225 RVA: 0x000892C8 File Offset: 0x000874C8
		public Expression Expr
		{
			get
			{
				return this.expr;
			}
		}

		// Token: 0x06001C3A RID: 7226 RVA: 0x000892D0 File Offset: 0x000874D0
		protected override void CloneTo(CloneContext clonectx, Statement t)
		{
			((YieldStatement<T>)t).expr = this.expr.Clone(clonectx);
		}

		// Token: 0x06001C3B RID: 7227 RVA: 0x000892E9 File Offset: 0x000874E9
		protected override void DoEmit(EmitContext ec)
		{
			this.machine_initializer.InjectYield(ec, this.expr, this.resume_pc, this.unwind_protect, this.resume_point);
		}

		// Token: 0x06001C3C RID: 7228 RVA: 0x00089314 File Offset: 0x00087514
		protected override bool DoFlowAnalysis(FlowAnalysisContext fc)
		{
			this.expr.FlowAnalysis(fc);
			this.RegisterResumePoint();
			return false;
		}

		// Token: 0x06001C3D RID: 7229 RVA: 0x0008932C File Offset: 0x0008752C
		public override bool Resolve(BlockContext bc)
		{
			this.expr = this.expr.Resolve(bc);
			if (this.expr == null)
			{
				return false;
			}
			this.machine_initializer = (bc.CurrentAnonymousMethod as T);
			this.inside_try_block = bc.CurrentTryBlock;
			return true;
		}

		// Token: 0x06001C3E RID: 7230 RVA: 0x00089378 File Offset: 0x00087578
		public void RegisterResumePoint()
		{
			if (this.resume_pc != 0)
			{
				return;
			}
			if (this.inside_try_block == null)
			{
				this.resume_pc = this.machine_initializer.AddResumePoint(this);
				return;
			}
			this.resume_pc = this.inside_try_block.AddResumePoint(this, this.resume_pc, this.machine_initializer);
			this.unwind_protect = true;
			this.inside_try_block = null;
		}

		// Token: 0x04000A6A RID: 2666
		protected Expression expr;

		// Token: 0x04000A6B RID: 2667
		protected bool unwind_protect;

		// Token: 0x04000A6C RID: 2668
		protected T machine_initializer;

		// Token: 0x04000A6D RID: 2669
		private int resume_pc;

		// Token: 0x04000A6E RID: 2670
		private ExceptionStatement inside_try_block;
	}
}

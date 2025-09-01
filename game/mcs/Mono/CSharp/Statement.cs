using System;

namespace Mono.CSharp
{
	// Token: 0x02000299 RID: 665
	public abstract class Statement
	{
		// Token: 0x17000768 RID: 1896
		// (get) Token: 0x06002030 RID: 8240 RVA: 0x0009F539 File Offset: 0x0009D739
		public bool IsUnreachable
		{
			get
			{
				return !this.reachable;
			}
		}

		// Token: 0x06002031 RID: 8241 RVA: 0x0000212D File Offset: 0x0000032D
		public virtual bool Resolve(BlockContext bc)
		{
			return true;
		}

		// Token: 0x06002032 RID: 8242
		protected abstract void DoEmit(EmitContext ec);

		// Token: 0x06002033 RID: 8243 RVA: 0x0009F544 File Offset: 0x0009D744
		public virtual void Emit(EmitContext ec)
		{
			ec.Mark(this.loc);
			this.DoEmit(ec);
			if (ec.StatementEpilogue != null)
			{
				ec.EmitEpilogue();
			}
		}

		// Token: 0x06002034 RID: 8244
		protected abstract void CloneTo(CloneContext clonectx, Statement target);

		// Token: 0x06002035 RID: 8245 RVA: 0x0009F568 File Offset: 0x0009D768
		public Statement Clone(CloneContext clonectx)
		{
			Statement statement = (Statement)base.MemberwiseClone();
			this.CloneTo(clonectx, statement);
			return statement;
		}

		// Token: 0x06002036 RID: 8246 RVA: 0x0009F58A File Offset: 0x0009D78A
		public virtual Expression CreateExpressionTree(ResolveContext ec)
		{
			ec.Report.Error(834, this.loc, "A lambda expression with statement body cannot be converted to an expresion tree");
			return null;
		}

		// Token: 0x06002037 RID: 8247 RVA: 0x0009F5A8 File Offset: 0x0009D7A8
		public virtual object Accept(StructuralVisitor visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x06002038 RID: 8248
		protected abstract bool DoFlowAnalysis(FlowAnalysisContext fc);

		// Token: 0x06002039 RID: 8249 RVA: 0x0009F5B4 File Offset: 0x0009D7B4
		public bool FlowAnalysis(FlowAnalysisContext fc)
		{
			if (this.reachable)
			{
				fc.UnreachableReported = false;
				return this.DoFlowAnalysis(fc);
			}
			if (this is Block)
			{
				return this.DoFlowAnalysis(fc);
			}
			if (this is EmptyStatement || this.loc.IsNull)
			{
				return true;
			}
			if (fc.UnreachableReported)
			{
				return true;
			}
			fc.Report.Warning(162, 2, this.loc, "Unreachable code detected");
			fc.UnreachableReported = true;
			return true;
		}

		// Token: 0x0600203A RID: 8250 RVA: 0x0009F62D File Offset: 0x0009D82D
		public virtual Reachability MarkReachable(Reachability rc)
		{
			if (!rc.IsUnreachable)
			{
				this.reachable = true;
			}
			return rc;
		}

		// Token: 0x0600203B RID: 8251 RVA: 0x0009F640 File Offset: 0x0009D840
		protected void CheckExitBoundaries(BlockContext bc, Block scope)
		{
			if (bc.CurrentBlock.ParametersBlock.Original != scope.ParametersBlock.Original)
			{
				bc.Report.Error(1632, this.loc, "Control cannot leave the body of an anonymous method");
				return;
			}
			Block block = bc.CurrentBlock;
			while (block != null && block != scope)
			{
				if (block.IsFinallyBlock)
				{
					this.Error_FinallyClauseExit(bc);
					return;
				}
				block = block.Parent;
			}
		}

		// Token: 0x0600203C RID: 8252 RVA: 0x0009F6AD File Offset: 0x0009D8AD
		protected void Error_FinallyClauseExit(BlockContext bc)
		{
			bc.Report.Error(157, this.loc, "Control cannot leave the body of a finally clause");
		}

		// Token: 0x0600203D RID: 8253 RVA: 0x00002CCC File Offset: 0x00000ECC
		protected Statement()
		{
		}

		// Token: 0x04000C0E RID: 3086
		public Location loc;

		// Token: 0x04000C0F RID: 3087
		protected bool reachable;
	}
}

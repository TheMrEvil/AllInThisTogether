using System;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace Mono.CSharp
{
	// Token: 0x020002C3 RID: 707
	public class TryFinally : TryFinallyBlock
	{
		// Token: 0x0600220F RID: 8719 RVA: 0x000A6EB0 File Offset: 0x000A50B0
		public TryFinally(Statement stmt, ExplicitBlock fini, Location loc) : base(stmt, loc)
		{
			this.fini = fini;
		}

		// Token: 0x170007C5 RID: 1989
		// (get) Token: 0x06002210 RID: 8720 RVA: 0x000A6EC1 File Offset: 0x000A50C1
		public ExplicitBlock FinallyBlock
		{
			get
			{
				return this.fini;
			}
		}

		// Token: 0x06002211 RID: 8721 RVA: 0x000A6EC9 File Offset: 0x000A50C9
		public void RegisterForControlExitCheck(DefiniteAssignmentBitSet vector)
		{
			if (this.try_exit_dat == null)
			{
				this.try_exit_dat = new List<DefiniteAssignmentBitSet>();
			}
			this.try_exit_dat.Add(vector);
		}

		// Token: 0x06002212 RID: 8722 RVA: 0x000A6EEC File Offset: 0x000A50EC
		public override bool Resolve(BlockContext bc)
		{
			bool flag = base.Resolve(bc);
			this.fini.SetFinallyBlock();
			using (bc.Set(ResolveContext.Options.FinallyScope))
			{
				flag &= this.fini.Resolve(bc);
			}
			return flag;
		}

		// Token: 0x06002213 RID: 8723 RVA: 0x000A6F48 File Offset: 0x000A5148
		protected override void EmitBeginException(EmitContext ec)
		{
			if (this.fini.HasAwait && this.stmt is TryCatch)
			{
				ec.BeginExceptionBlock();
			}
			base.EmitBeginException(ec);
		}

		// Token: 0x06002214 RID: 8724 RVA: 0x000A6F74 File Offset: 0x000A5174
		protected override void EmitTryBody(EmitContext ec)
		{
			if (this.fini.HasAwait)
			{
				if (ec.TryFinallyUnwind == null)
				{
					ec.TryFinallyUnwind = new List<TryFinally>();
				}
				ec.TryFinallyUnwind.Add(this);
				this.stmt.Emit(ec);
				if (this.stmt is TryCatch)
				{
					ec.EndExceptionBlock();
				}
				ec.TryFinallyUnwind.Remove(this);
				if (this.start_fin_label != null)
				{
					ec.MarkLabel(this.start_fin_label.Value);
				}
				return;
			}
			this.stmt.Emit(ec);
		}

		// Token: 0x06002215 RID: 8725 RVA: 0x000A7004 File Offset: 0x000A5204
		protected override bool EmitBeginFinallyBlock(EmitContext ec)
		{
			return !this.fini.HasAwait && base.EmitBeginFinallyBlock(ec);
		}

		// Token: 0x06002216 RID: 8726 RVA: 0x000A701C File Offset: 0x000A521C
		public override void EmitFinallyBody(EmitContext ec)
		{
			if (!this.fini.HasAwait)
			{
				this.fini.Emit(ec);
				return;
			}
			BuiltinTypeSpec @object = ec.BuiltinTypes.Object;
			ec.BeginCatchBlock(@object);
			LocalBuilder temporaryLocal = ec.GetTemporaryLocal(@object);
			ec.Emit(OpCodes.Stloc, temporaryLocal);
			StackFieldExpr temporaryField = ec.GetTemporaryField(@object, false);
			ec.EmitThis();
			ec.Emit(OpCodes.Ldloc, temporaryLocal);
			temporaryField.EmitAssignFromStack(ec);
			ec.EndExceptionBlock();
			ec.FreeTemporaryLocal(temporaryLocal, @object);
			this.fini.Emit(ec);
			temporaryField.Emit(ec);
			Label label = ec.DefineLabel();
			ec.Emit(OpCodes.Brfalse_S, label);
			temporaryField.Emit(ec);
			ec.Emit(OpCodes.Throw);
			ec.MarkLabel(label);
			temporaryField.IsAvailableForReuse = true;
			this.EmitUnwindFinallyTable(ec);
		}

		// Token: 0x06002217 RID: 8727 RVA: 0x000A70E8 File Offset: 0x000A52E8
		private bool IsParentBlock(Block block)
		{
			for (Block parent = this.fini; parent != null; parent = parent.Parent)
			{
				if (parent == block)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06002218 RID: 8728 RVA: 0x000A7110 File Offset: 0x000A5310
		public static Label EmitRedirectedJump(EmitContext ec, AsyncInitializer initializer, Label label, Block labelBlock)
		{
			int i;
			if (labelBlock != null)
			{
				for (i = ec.TryFinallyUnwind.Count; i != 0; i--)
				{
					if (!ec.TryFinallyUnwind[i - 1].IsParentBlock(labelBlock))
					{
						break;
					}
				}
			}
			else
			{
				i = 0;
			}
			bool setReturnState = true;
			while (i < ec.TryFinallyUnwind.Count)
			{
				TryFinally tryFinally = ec.TryFinallyUnwind[i];
				if (labelBlock != null && !tryFinally.IsParentBlock(labelBlock))
				{
					break;
				}
				tryFinally.EmitRedirectedExit(ec, label, initializer, setReturnState);
				setReturnState = false;
				if (tryFinally.start_fin_label == null)
				{
					tryFinally.start_fin_label = new Label?(ec.DefineLabel());
				}
				label = tryFinally.start_fin_label.Value;
				i++;
			}
			return label;
		}

		// Token: 0x06002219 RID: 8729 RVA: 0x000A71B6 File Offset: 0x000A53B6
		public static Label EmitRedirectedReturn(EmitContext ec, AsyncInitializer initializer)
		{
			return TryFinally.EmitRedirectedJump(ec, initializer, initializer.BodyEnd, null);
		}

		// Token: 0x0600221A RID: 8730 RVA: 0x000A71C8 File Offset: 0x000A53C8
		private void EmitRedirectedExit(EmitContext ec, Label label, AsyncInitializer initializer, bool setReturnState)
		{
			if (this.redirected_jumps == null)
			{
				this.redirected_jumps = new List<Label>();
				this.redirected_jumps.Add(ec.DefineLabel());
				if (setReturnState)
				{
					initializer.HoistedReturnState = ec.GetTemporaryField(ec.Module.Compiler.BuiltinTypes.Int, true);
				}
			}
			int num = this.redirected_jumps.IndexOf(label);
			if (num < 0)
			{
				this.redirected_jumps.Add(label);
				num = this.redirected_jumps.Count - 1;
			}
			if (setReturnState)
			{
				IntConstant source = new IntConstant(initializer.HoistedReturnState.Type, num, Location.Null);
				initializer.HoistedReturnState.EmitAssign(ec, source, false, false);
			}
		}

		// Token: 0x0600221B RID: 8731 RVA: 0x000A7274 File Offset: 0x000A5474
		private void EmitUnwindFinallyTable(EmitContext ec)
		{
			if (this.redirected_jumps == null)
			{
				return;
			}
			((AsyncInitializer)ec.CurrentAnonymousMethod).HoistedReturnState.EmitLoad(ec);
			ec.Emit(OpCodes.Switch, this.redirected_jumps.ToArray());
			ec.MarkLabel(this.redirected_jumps[0]);
		}

		// Token: 0x0600221C RID: 8732 RVA: 0x000A72C8 File Offset: 0x000A54C8
		protected override bool DoFlowAnalysis(FlowAnalysisContext fc)
		{
			DefiniteAssignmentBitSet definiteAssignment = fc.BranchDefiniteAssignment();
			TryFinally tryFinally = fc.TryFinally;
			fc.TryFinally = this;
			bool flag = base.Statement.FlowAnalysis(fc);
			fc.TryFinally = tryFinally;
			DefiniteAssignmentBitSet definiteAssignment2 = fc.DefiniteAssignment;
			fc.DefiniteAssignment = definiteAssignment;
			bool flag2 = this.fini.FlowAnalysis(fc);
			if (this.try_exit_dat != null)
			{
				foreach (DefiniteAssignmentBitSet b in this.try_exit_dat)
				{
					fc.ParametersBlock.CheckControlExit(fc, fc.DefiniteAssignment | b);
				}
				this.try_exit_dat = null;
			}
			fc.DefiniteAssignment |= definiteAssignment2;
			return flag || flag2;
		}

		// Token: 0x0600221D RID: 8733 RVA: 0x000A73A0 File Offset: 0x000A55A0
		public override Reachability MarkReachable(Reachability rc)
		{
			return this.fini.MarkReachable(rc) | base.MarkReachable(rc);
		}

		// Token: 0x0600221E RID: 8734 RVA: 0x000A73BC File Offset: 0x000A55BC
		protected override void CloneTo(CloneContext clonectx, Statement t)
		{
			TryFinally tryFinally = (TryFinally)t;
			tryFinally.stmt = this.stmt.Clone(clonectx);
			if (this.fini != null)
			{
				tryFinally.fini = (ExplicitBlock)clonectx.LookupBlock(this.fini);
			}
		}

		// Token: 0x0600221F RID: 8735 RVA: 0x000A7401 File Offset: 0x000A5601
		public override object Accept(StructuralVisitor visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x04000C93 RID: 3219
		private ExplicitBlock fini;

		// Token: 0x04000C94 RID: 3220
		private List<DefiniteAssignmentBitSet> try_exit_dat;

		// Token: 0x04000C95 RID: 3221
		private List<Label> redirected_jumps;

		// Token: 0x04000C96 RID: 3222
		private Label? start_fin_label;
	}
}

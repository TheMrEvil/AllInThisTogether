using System;
using System.Reflection.Emit;

namespace Mono.CSharp
{
	// Token: 0x020002BB RID: 699
	public abstract class TryFinallyBlock : ExceptionStatement
	{
		// Token: 0x060021C7 RID: 8647 RVA: 0x000A5BDC File Offset: 0x000A3DDC
		protected TryFinallyBlock(Statement stmt, Location loc) : base(loc)
		{
			this.stmt = stmt;
		}

		// Token: 0x170007BB RID: 1979
		// (get) Token: 0x060021C8 RID: 8648 RVA: 0x000A5BEC File Offset: 0x000A3DEC
		public Statement Statement
		{
			get
			{
				return this.stmt;
			}
		}

		// Token: 0x060021C9 RID: 8649
		protected abstract void EmitTryBody(EmitContext ec);

		// Token: 0x060021CA RID: 8650
		public abstract void EmitFinallyBody(EmitContext ec);

		// Token: 0x060021CB RID: 8651 RVA: 0x000A5BF4 File Offset: 0x000A3DF4
		public override Label PrepareForDispose(EmitContext ec, Label end)
		{
			if (!this.prepared_for_dispose)
			{
				this.prepared_for_dispose = true;
				this.dispose_try_block = ec.DefineLabel();
			}
			return this.dispose_try_block;
		}

		// Token: 0x060021CC RID: 8652 RVA: 0x000A5C18 File Offset: 0x000A3E18
		protected sealed override void DoEmit(EmitContext ec)
		{
			this.EmitTryBodyPrepare(ec);
			this.EmitTryBody(ec);
			bool flag = this.EmitBeginFinallyBlock(ec);
			Label label = ec.DefineLabel();
			if (this.resume_points != null && flag)
			{
				StateMachineInitializer stateMachineInitializer = (StateMachineInitializer)ec.CurrentAnonymousMethod;
				ec.Emit(OpCodes.Ldloc, stateMachineInitializer.SkipFinally);
				ec.Emit(OpCodes.Brfalse_S, label);
				ec.Emit(OpCodes.Endfinally);
			}
			ec.MarkLabel(label);
			if (this.finally_host != null)
			{
				this.finally_host.Define();
				this.finally_host.PrepareEmit();
				this.finally_host.Emit();
				this.finally_host.Parent.AddMember(this.finally_host);
				CallEmitter callEmitter = default(CallEmitter);
				callEmitter.InstanceExpression = new CompilerGeneratedThis(ec.CurrentType, this.loc);
				callEmitter.EmitPredefined(ec, this.finally_host.Spec, new Arguments(0), true, null);
			}
			else
			{
				this.EmitFinallyBody(ec);
			}
			if (flag)
			{
				ec.EndExceptionBlock();
			}
		}

		// Token: 0x060021CD RID: 8653 RVA: 0x000A5D20 File Offset: 0x000A3F20
		public override void EmitForDispose(EmitContext ec, LocalBuilder pc, Label end, bool have_dispatcher)
		{
			if (this.emitted_dispose)
			{
				return;
			}
			this.emitted_dispose = true;
			Label label = ec.DefineLabel();
			if (have_dispatcher)
			{
				ec.Emit(OpCodes.Br, end);
			}
			ec.BeginExceptionBlock();
			ec.MarkLabel(this.dispose_try_block);
			Label[] array = null;
			for (int i = 0; i < this.resume_points.Count; i++)
			{
				Label label2 = this.resume_points[i].PrepareForDispose(ec, label);
				if (!label2.Equals(label) || array != null)
				{
					if (array == null)
					{
						array = new Label[this.resume_points.Count];
						for (int j = 0; j < i; j++)
						{
							array[j] = label;
						}
					}
					array[i] = label2;
				}
			}
			if (array != null)
			{
				int num = 1;
				while (num < array.Length && array[0].Equals(array[num]))
				{
					num++;
				}
				bool flag = num < array.Length;
				if (flag)
				{
					ec.Emit(OpCodes.Ldloc, pc);
					ec.EmitInt(this.first_resume_pc);
					ec.Emit(OpCodes.Sub);
					ec.Emit(OpCodes.Switch, array);
				}
				foreach (ResumableStatement resumableStatement in this.resume_points)
				{
					resumableStatement.EmitForDispose(ec, pc, label, flag);
				}
			}
			ec.MarkLabel(label);
			ec.BeginFinallyBlock();
			if (this.finally_host != null)
			{
				CallEmitter callEmitter = default(CallEmitter);
				callEmitter.InstanceExpression = new CompilerGeneratedThis(ec.CurrentType, this.loc);
				callEmitter.EmitPredefined(ec, this.finally_host.Spec, new Arguments(0), true, null);
			}
			else
			{
				this.EmitFinallyBody(ec);
			}
			ec.EndExceptionBlock();
		}

		// Token: 0x060021CE RID: 8654 RVA: 0x000A5EF0 File Offset: 0x000A40F0
		protected override bool DoFlowAnalysis(FlowAnalysisContext fc)
		{
			bool result = this.stmt.FlowAnalysis(fc);
			this.parent = null;
			return result;
		}

		// Token: 0x060021CF RID: 8655 RVA: 0x000A5F05 File Offset: 0x000A4105
		protected virtual bool EmitBeginFinallyBlock(EmitContext ec)
		{
			ec.BeginFinallyBlock();
			return true;
		}

		// Token: 0x060021D0 RID: 8656 RVA: 0x000A5F0E File Offset: 0x000A410E
		public override Reachability MarkReachable(Reachability rc)
		{
			base.MarkReachable(rc);
			return this.Statement.MarkReachable(rc);
		}

		// Token: 0x060021D1 RID: 8657 RVA: 0x000A5F24 File Offset: 0x000A4124
		public override bool Resolve(BlockContext bc)
		{
			this.parent = bc.CurrentTryBlock;
			bc.CurrentTryBlock = this;
			bool flag;
			using (bc.Set(ResolveContext.Options.TryScope))
			{
				flag = this.stmt.Resolve(bc);
			}
			bc.CurrentTryBlock = this.parent;
			if (bc.CurrentIterator != null && !bc.IsInProbingMode)
			{
				Block block = this.stmt as Block;
				if (block != null && block.Explicit.HasYield)
				{
					this.finally_host = bc.CurrentIterator.CreateFinallyHost(this);
				}
			}
			return base.Resolve(bc) && flag;
		}

		// Token: 0x04000C7B RID: 3195
		protected Statement stmt;

		// Token: 0x04000C7C RID: 3196
		private Label dispose_try_block;

		// Token: 0x04000C7D RID: 3197
		private bool prepared_for_dispose;

		// Token: 0x04000C7E RID: 3198
		private bool emitted_dispose;

		// Token: 0x04000C7F RID: 3199
		private Method finally_host;
	}
}

using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;

namespace Mono.CSharp
{
	// Token: 0x02000233 RID: 563
	public abstract class StateMachineInitializer : AnonymousExpression
	{
		// Token: 0x06001C5F RID: 7263 RVA: 0x00089C90 File Offset: 0x00087E90
		protected StateMachineInitializer(ParametersBlock block, TypeDefinition host, TypeSpec returnType) : base(block, returnType, block.StartLocation)
		{
			this.Host = host;
		}

		// Token: 0x17000673 RID: 1651
		// (get) Token: 0x06001C60 RID: 7264 RVA: 0x00089CA7 File Offset: 0x00087EA7
		// (set) Token: 0x06001C61 RID: 7265 RVA: 0x00089CAF File Offset: 0x00087EAF
		public Label BodyEnd
		{
			[CompilerGenerated]
			get
			{
				return this.<BodyEnd>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<BodyEnd>k__BackingField = value;
			}
		}

		// Token: 0x17000674 RID: 1652
		// (get) Token: 0x06001C62 RID: 7266 RVA: 0x00089CB8 File Offset: 0x00087EB8
		public LocalBuilder CurrentPC
		{
			get
			{
				return this.current_pc;
			}
		}

		// Token: 0x17000675 RID: 1653
		// (get) Token: 0x06001C63 RID: 7267 RVA: 0x00089CC0 File Offset: 0x00087EC0
		public LocalBuilder SkipFinally
		{
			get
			{
				return this.skip_finally;
			}
		}

		// Token: 0x17000676 RID: 1654
		// (get) Token: 0x06001C64 RID: 7268 RVA: 0x00089CC8 File Offset: 0x00087EC8
		public override AnonymousMethodStorey Storey
		{
			get
			{
				return this.storey;
			}
		}

		// Token: 0x06001C65 RID: 7269 RVA: 0x00089CD0 File Offset: 0x00087ED0
		public int AddResumePoint(ResumableStatement stmt)
		{
			if (this.resume_points == null)
			{
				this.resume_points = new List<ResumableStatement>();
			}
			this.resume_points.Add(stmt);
			return this.resume_points.Count;
		}

		// Token: 0x06001C66 RID: 7270 RVA: 0x0006D4AD File Offset: 0x0006B6AD
		public override Expression CreateExpressionTree(ResolveContext ec)
		{
			throw new NotSupportedException("ET");
		}

		// Token: 0x06001C67 RID: 7271 RVA: 0x00089CFC File Offset: 0x00087EFC
		protected virtual BlockContext CreateBlockContext(BlockContext bc)
		{
			return new BlockContext(bc, this.block, bc.ReturnType)
			{
				CurrentAnonymousMethod = this,
				AssignmentInfoOffset = bc.AssignmentInfoOffset,
				EnclosingLoop = bc.EnclosingLoop,
				EnclosingLoopOrSwitch = bc.EnclosingLoopOrSwitch,
				Switch = bc.Switch
			};
		}

		// Token: 0x06001C68 RID: 7272 RVA: 0x00089D54 File Offset: 0x00087F54
		protected override Expression DoResolve(ResolveContext rc)
		{
			BlockContext blockContext = (BlockContext)rc;
			BlockContext blockContext2 = this.CreateBlockContext(blockContext);
			base.Block.Resolve(blockContext2);
			if (!rc.IsInProbingMode)
			{
				StateMachineMethod stateMachineMethod = new StateMachineMethod(this.storey, this, new TypeExpression(this.ReturnType, this.loc), Modifiers.PUBLIC, new MemberName("MoveNext", this.loc), (Block.Flags)0);
				stateMachineMethod.Block.AddStatement(new StateMachineInitializer.MoveNextBodyStatement(this));
				this.storey.AddEntryMethod(stateMachineMethod);
			}
			blockContext.AssignmentInfoOffset = blockContext2.AssignmentInfoOffset;
			this.eclass = ExprClass.Value;
			return this;
		}

		// Token: 0x06001C69 RID: 7273 RVA: 0x00089DE6 File Offset: 0x00087FE6
		public override void Emit(EmitContext ec)
		{
			this.storey.Instance.Emit(ec);
		}

		// Token: 0x06001C6A RID: 7274 RVA: 0x00089DFC File Offset: 0x00087FFC
		private void EmitMoveNext_NoResumePoints(EmitContext ec)
		{
			ec.EmitThis();
			ec.Emit(OpCodes.Ldfld, this.storey.PC.Spec);
			ec.EmitThis();
			ec.EmitInt(-1);
			ec.Emit(OpCodes.Stfld, this.storey.PC.Spec);
			ec.Emit(OpCodes.Brtrue, this.move_next_error);
			this.BodyEnd = ec.DefineLabel();
			AsyncInitializer asyncInitializer = this as AsyncInitializer;
			if (asyncInitializer != null)
			{
				ec.BeginExceptionBlock();
			}
			this.block.EmitEmbedded(ec);
			if (asyncInitializer != null)
			{
				asyncInitializer.EmitCatchBlock(ec);
			}
			ec.MarkLabel(this.BodyEnd);
			this.EmitMoveNextEpilogue(ec);
			ec.MarkLabel(this.move_next_error);
			if (this.ReturnType.Kind != MemberKind.Void)
			{
				ec.EmitInt(0);
				ec.Emit(OpCodes.Ret);
			}
			ec.MarkLabel(this.move_next_ok);
		}

		// Token: 0x06001C6B RID: 7275 RVA: 0x00089EE4 File Offset: 0x000880E4
		private void EmitMoveNext(EmitContext ec)
		{
			this.move_next_ok = ec.DefineLabel();
			this.move_next_error = ec.DefineLabel();
			if (this.resume_points == null)
			{
				this.EmitMoveNext_NoResumePoints(ec);
				return;
			}
			this.current_pc = ec.GetTemporaryLocal(ec.BuiltinTypes.UInt);
			ec.EmitThis();
			ec.Emit(OpCodes.Ldfld, this.storey.PC.Spec);
			ec.Emit(OpCodes.Stloc, this.current_pc);
			ec.EmitThis();
			ec.EmitInt(-1);
			ec.Emit(OpCodes.Stfld, this.storey.PC.Spec);
			Label[] array = new Label[1 + this.resume_points.Count];
			array[0] = ec.DefineLabel();
			bool flag = false;
			for (int i = 0; i < this.resume_points.Count; i++)
			{
				ResumableStatement resumableStatement = this.resume_points[i];
				flag |= (resumableStatement is ExceptionStatement);
				array[i + 1] = resumableStatement.PrepareForEmit(ec);
			}
			if (flag)
			{
				this.skip_finally = ec.GetTemporaryLocal(ec.BuiltinTypes.Bool);
				ec.EmitInt(0);
				ec.Emit(OpCodes.Stloc, this.skip_finally);
			}
			AsyncInitializer asyncInitializer = this as AsyncInitializer;
			if (asyncInitializer != null)
			{
				ec.BeginExceptionBlock();
			}
			ec.Emit(OpCodes.Ldloc, this.current_pc);
			ec.Emit(OpCodes.Switch, array);
			ec.Emit((asyncInitializer != null) ? OpCodes.Leave : OpCodes.Br, this.move_next_error);
			ec.MarkLabel(array[0]);
			this.BodyEnd = ec.DefineLabel();
			this.block.EmitEmbedded(ec);
			ec.MarkLabel(this.BodyEnd);
			if (asyncInitializer != null)
			{
				asyncInitializer.EmitCatchBlock(ec);
			}
			ec.Mark(base.Block.Original.EndLocation);
			ec.EmitThis();
			ec.EmitInt(-1);
			ec.Emit(OpCodes.Stfld, this.storey.PC.Spec);
			this.EmitMoveNextEpilogue(ec);
			ec.MarkLabel(this.move_next_error);
			if (this.ReturnType.Kind != MemberKind.Void)
			{
				ec.EmitInt(0);
				ec.Emit(OpCodes.Ret);
			}
			ec.MarkLabel(this.move_next_ok);
			if (this.ReturnType.Kind != MemberKind.Void)
			{
				ec.EmitInt(1);
				ec.Emit(OpCodes.Ret);
			}
		}

		// Token: 0x06001C6C RID: 7276 RVA: 0x0000AF70 File Offset: 0x00009170
		protected virtual void EmitMoveNextEpilogue(EmitContext ec)
		{
		}

		// Token: 0x06001C6D RID: 7277 RVA: 0x0008A149 File Offset: 0x00088349
		public void EmitLeave(EmitContext ec, bool unwind_protect)
		{
			ec.Emit(unwind_protect ? OpCodes.Leave : OpCodes.Br, this.move_next_ok);
		}

		// Token: 0x06001C6E RID: 7278 RVA: 0x0008A168 File Offset: 0x00088368
		public virtual void InjectYield(EmitContext ec, Expression expr, int resume_pc, bool unwind_protect, Label resume_point)
		{
			Label label = ec.DefineLabel();
			IteratorStorey iteratorStorey = this.storey as IteratorStorey;
			if (iteratorStorey != null)
			{
				ec.EmitThis();
				ec.Emit(OpCodes.Ldfld, iteratorStorey.DisposingField.Spec);
				ec.Emit(OpCodes.Brtrue_S, label);
			}
			ec.EmitThis();
			ec.EmitInt(resume_pc);
			ec.Emit(OpCodes.Stfld, this.storey.PC.Spec);
			if (iteratorStorey != null)
			{
				ec.MarkLabel(label);
			}
			if (unwind_protect && this.skip_finally != null)
			{
				ec.EmitInt(1);
				ec.Emit(OpCodes.Stloc, this.skip_finally);
			}
		}

		// Token: 0x06001C6F RID: 7279 RVA: 0x0008A209 File Offset: 0x00088409
		public void SetStateMachine(StateMachine stateMachine)
		{
			this.storey = stateMachine;
		}

		// Token: 0x04000A7C RID: 2684
		public readonly TypeDefinition Host;

		// Token: 0x04000A7D RID: 2685
		protected StateMachine storey;

		// Token: 0x04000A7E RID: 2686
		protected Label move_next_ok;

		// Token: 0x04000A7F RID: 2687
		protected Label move_next_error;

		// Token: 0x04000A80 RID: 2688
		private LocalBuilder skip_finally;

		// Token: 0x04000A81 RID: 2689
		protected LocalBuilder current_pc;

		// Token: 0x04000A82 RID: 2690
		protected List<ResumableStatement> resume_points;

		// Token: 0x04000A83 RID: 2691
		[CompilerGenerated]
		private Label <BodyEnd>k__BackingField;

		// Token: 0x020003CB RID: 971
		private sealed class MoveNextBodyStatement : Statement
		{
			// Token: 0x0600275C RID: 10076 RVA: 0x000BC101 File Offset: 0x000BA301
			public MoveNextBodyStatement(StateMachineInitializer stateMachine)
			{
				this.state_machine = stateMachine;
				this.loc = stateMachine.Location;
			}

			// Token: 0x0600275D RID: 10077 RVA: 0x0000225C File Offset: 0x0000045C
			protected override void CloneTo(CloneContext clonectx, Statement target)
			{
				throw new NotSupportedException();
			}

			// Token: 0x0600275E RID: 10078 RVA: 0x0000212D File Offset: 0x0000032D
			public override bool Resolve(BlockContext ec)
			{
				return true;
			}

			// Token: 0x0600275F RID: 10079 RVA: 0x000BC11C File Offset: 0x000BA31C
			protected override void DoEmit(EmitContext ec)
			{
				this.state_machine.EmitMoveNext(ec);
			}

			// Token: 0x06002760 RID: 10080 RVA: 0x000A7A48 File Offset: 0x000A5C48
			public override void Emit(EmitContext ec)
			{
				this.DoEmit(ec);
			}

			// Token: 0x06002761 RID: 10081 RVA: 0x000BC12A File Offset: 0x000BA32A
			protected override bool DoFlowAnalysis(FlowAnalysisContext fc)
			{
				return this.state_machine.ReturnType.Kind != MemberKind.Void;
			}

			// Token: 0x06002762 RID: 10082 RVA: 0x000BC146 File Offset: 0x000BA346
			public override Reachability MarkReachable(Reachability rc)
			{
				base.MarkReachable(rc);
				if (this.state_machine.ReturnType.Kind != MemberKind.Void)
				{
					rc = Reachability.CreateUnreachable();
				}
				return rc;
			}

			// Token: 0x040010C2 RID: 4290
			private readonly StateMachineInitializer state_machine;
		}
	}
}

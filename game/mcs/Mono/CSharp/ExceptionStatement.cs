using System;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace Mono.CSharp
{
	// Token: 0x020002BC RID: 700
	public abstract class ExceptionStatement : ResumableStatement
	{
		// Token: 0x060021D2 RID: 8658 RVA: 0x000A5FD0 File Offset: 0x000A41D0
		protected ExceptionStatement(Location loc)
		{
			this.loc = loc;
		}

		// Token: 0x060021D3 RID: 8659 RVA: 0x000A5FDF File Offset: 0x000A41DF
		protected virtual void EmitBeginException(EmitContext ec)
		{
			ec.BeginExceptionBlock();
		}

		// Token: 0x060021D4 RID: 8660 RVA: 0x000A5FE8 File Offset: 0x000A41E8
		protected virtual void EmitTryBodyPrepare(EmitContext ec)
		{
			StateMachineInitializer stateMachineInitializer = null;
			if (this.resume_points != null)
			{
				stateMachineInitializer = (StateMachineInitializer)ec.CurrentAnonymousMethod;
				ec.EmitInt(-3);
				ec.Emit(OpCodes.Stloc, stateMachineInitializer.CurrentPC);
			}
			this.EmitBeginException(ec);
			if (this.resume_points != null)
			{
				ec.MarkLabel(this.resume_point);
				ec.Emit(OpCodes.Ldloc, stateMachineInitializer.CurrentPC);
				ec.EmitInt(this.first_resume_pc);
				ec.Emit(OpCodes.Sub);
				Label[] array = new Label[this.resume_points.Count];
				for (int i = 0; i < this.resume_points.Count; i++)
				{
					array[i] = this.resume_points[i].PrepareForEmit(ec);
				}
				ec.Emit(OpCodes.Switch, array);
			}
		}

		// Token: 0x060021D5 RID: 8661 RVA: 0x000A60B8 File Offset: 0x000A42B8
		public virtual int AddResumePoint(ResumableStatement stmt, int pc, StateMachineInitializer stateMachine)
		{
			if (this.parent != null)
			{
				TryCatch tryCatch = this as TryCatch;
				ResumableStatement stmt2 = (tryCatch != null && tryCatch.IsTryCatchFinally) ? stmt : this;
				pc = this.parent.AddResumePoint(stmt2, pc, stateMachine);
			}
			else
			{
				pc = stateMachine.AddResumePoint(this);
			}
			if (this.resume_points == null)
			{
				this.resume_points = new List<ResumableStatement>();
				this.first_resume_pc = pc;
			}
			if (pc != this.first_resume_pc + this.resume_points.Count)
			{
				throw new InternalErrorException("missed an intervening AddResumePoint?");
			}
			this.resume_points.Add(stmt);
			return pc;
		}

		// Token: 0x04000C80 RID: 3200
		protected List<ResumableStatement> resume_points;

		// Token: 0x04000C81 RID: 3201
		protected int first_resume_pc;

		// Token: 0x04000C82 RID: 3202
		protected ExceptionStatement parent;
	}
}

using System;
using System.Reflection.Emit;

namespace Mono.CSharp
{
	// Token: 0x020002BD RID: 701
	public class Lock : TryFinallyBlock
	{
		// Token: 0x060021D6 RID: 8662 RVA: 0x000A6146 File Offset: 0x000A4346
		public Lock(Expression expr, Statement stmt, Location loc) : base(stmt, loc)
		{
			this.expr = expr;
		}

		// Token: 0x170007BC RID: 1980
		// (get) Token: 0x060021D7 RID: 8663 RVA: 0x000A6157 File Offset: 0x000A4357
		public Expression Expr
		{
			get
			{
				return this.expr;
			}
		}

		// Token: 0x060021D8 RID: 8664 RVA: 0x000A615F File Offset: 0x000A435F
		protected override bool DoFlowAnalysis(FlowAnalysisContext fc)
		{
			this.expr.FlowAnalysis(fc);
			return base.DoFlowAnalysis(fc);
		}

		// Token: 0x060021D9 RID: 8665 RVA: 0x000A6174 File Offset: 0x000A4374
		public override bool Resolve(BlockContext ec)
		{
			this.expr = this.expr.Resolve(ec);
			if (this.expr == null)
			{
				return false;
			}
			if (!TypeSpec.IsReferenceType(this.expr.Type) && this.expr.Type != InternalType.ErrorType)
			{
				ec.Report.Error(185, this.loc, "`{0}' is not a reference type as required by the lock statement", this.expr.Type.GetSignatureForError());
			}
			if (this.expr.Type.IsGenericParameter)
			{
				this.expr = Convert.ImplicitTypeParameterConversion(this.expr, (TypeParameterSpec)this.expr.Type, ec.BuiltinTypes.Object);
			}
			VariableReference variableReference = this.expr as VariableReference;
			bool isLockedByStatement;
			if (variableReference != null)
			{
				isLockedByStatement = variableReference.IsLockedByStatement;
				variableReference.IsLockedByStatement = true;
			}
			else
			{
				variableReference = null;
				isLockedByStatement = false;
			}
			this.expr_copy = TemporaryVariableReference.Create(ec.BuiltinTypes.Object, ec.CurrentBlock, this.loc);
			this.expr_copy.Resolve(ec);
			if (this.ResolvePredefinedMethods(ec) > 1)
			{
				this.lock_taken = TemporaryVariableReference.Create(ec.BuiltinTypes.Bool, ec.CurrentBlock, this.loc);
				this.lock_taken.Resolve(ec);
			}
			using (ec.Set(ResolveContext.Options.LockScope))
			{
				base.Resolve(ec);
			}
			if (variableReference != null)
			{
				variableReference.IsLockedByStatement = isLockedByStatement;
			}
			return true;
		}

		// Token: 0x060021DA RID: 8666 RVA: 0x000A62F4 File Offset: 0x000A44F4
		protected override void EmitTryBodyPrepare(EmitContext ec)
		{
			this.expr_copy.EmitAssign(ec, this.expr);
			if (this.lock_taken != null)
			{
				this.lock_taken.EmitAssign(ec, new BoolLiteral(ec.BuiltinTypes, false, this.loc));
			}
			else
			{
				this.expr_copy.Emit(ec);
				ec.Emit(OpCodes.Call, ec.Module.PredefinedMembers.MonitorEnter.Get());
			}
			base.EmitTryBodyPrepare(ec);
		}

		// Token: 0x060021DB RID: 8667 RVA: 0x000A6370 File Offset: 0x000A4570
		protected override void EmitTryBody(EmitContext ec)
		{
			if (this.lock_taken != null)
			{
				this.expr_copy.Emit(ec);
				this.lock_taken.LocalInfo.CreateBuilder(ec);
				this.lock_taken.AddressOf(ec, AddressOp.Load);
				ec.Emit(OpCodes.Call, ec.Module.PredefinedMembers.MonitorEnter_v4.Get());
			}
			base.Statement.Emit(ec);
		}

		// Token: 0x060021DC RID: 8668 RVA: 0x000A63DC File Offset: 0x000A45DC
		public override void EmitFinallyBody(EmitContext ec)
		{
			Label label = ec.DefineLabel();
			if (this.lock_taken != null)
			{
				this.lock_taken.Emit(ec);
				ec.Emit(OpCodes.Brfalse_S, label);
			}
			this.expr_copy.Emit(ec);
			MethodSpec methodSpec = ec.Module.PredefinedMembers.MonitorExit.Resolve(this.loc);
			if (methodSpec != null)
			{
				ec.Emit(OpCodes.Call, methodSpec);
			}
			ec.MarkLabel(label);
		}

		// Token: 0x060021DD RID: 8669 RVA: 0x000A6450 File Offset: 0x000A4650
		private int ResolvePredefinedMethods(ResolveContext rc)
		{
			if (rc.Module.PredefinedMembers.MonitorEnter_v4.Get() != null)
			{
				return 4;
			}
			if (rc.Module.PredefinedMembers.MonitorEnter.Get() != null)
			{
				return 1;
			}
			rc.Module.PredefinedMembers.MonitorEnter_v4.Resolve(this.loc);
			return 0;
		}

		// Token: 0x060021DE RID: 8670 RVA: 0x000A64AC File Offset: 0x000A46AC
		protected override void CloneTo(CloneContext clonectx, Statement t)
		{
			Lock @lock = (Lock)t;
			@lock.expr = this.expr.Clone(clonectx);
			@lock.stmt = base.Statement.Clone(clonectx);
		}

		// Token: 0x060021DF RID: 8671 RVA: 0x000A64D7 File Offset: 0x000A46D7
		public override object Accept(StructuralVisitor visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x04000C83 RID: 3203
		private Expression expr;

		// Token: 0x04000C84 RID: 3204
		private TemporaryVariableReference expr_copy;

		// Token: 0x04000C85 RID: 3205
		private TemporaryVariableReference lock_taken;
	}
}

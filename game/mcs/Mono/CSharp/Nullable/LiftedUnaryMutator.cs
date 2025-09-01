using System;
using System.Reflection.Emit;

namespace Mono.CSharp.Nullable
{
	// Token: 0x02000305 RID: 773
	internal class LiftedUnaryMutator : UnaryMutator
	{
		// Token: 0x060024A2 RID: 9378 RVA: 0x000AF87F File Offset: 0x000ADA7F
		public LiftedUnaryMutator(UnaryMutator.Mode mode, Expression expr, Location loc) : base(mode, expr, loc)
		{
		}

		// Token: 0x060024A3 RID: 9379 RVA: 0x000AF88C File Offset: 0x000ADA8C
		protected override Expression DoResolve(ResolveContext ec)
		{
			Expression expr = this.expr;
			this.expr = Unwrap.Create(this.expr);
			Expression result = base.DoResolveOperation(ec);
			this.expr = expr;
			this.type = this.expr.Type;
			return result;
		}

		// Token: 0x060024A4 RID: 9380 RVA: 0x000AF8D0 File Offset: 0x000ADAD0
		protected override void EmitOperation(EmitContext ec)
		{
			Label label = ec.DefineLabel();
			Label label2 = ec.DefineLabel();
			LocalTemporary localTemporary = new LocalTemporary(this.type);
			localTemporary.Store(ec);
			CallEmitter callEmitter = default(CallEmitter);
			callEmitter.InstanceExpression = localTemporary;
			callEmitter.EmitPredefined(ec, NullableInfo.GetHasValue(this.expr.Type), null, false, null);
			ec.Emit(OpCodes.Brfalse, label);
			callEmitter = default(CallEmitter);
			callEmitter.InstanceExpression = localTemporary;
			callEmitter.EmitPredefined(ec, NullableInfo.GetGetValueOrDefault(this.expr.Type), null, false, null);
			localTemporary.Release(ec);
			base.EmitOperation(ec);
			ec.Emit(OpCodes.Newobj, NullableInfo.GetConstructor(this.type));
			ec.Emit(OpCodes.Br_S, label2);
			ec.MarkLabel(label);
			LiftedNull.Create(this.type, this.loc).Emit(ec);
			ec.MarkLabel(label2);
		}
	}
}

using System;
using System.Reflection.Emit;

namespace Mono.CSharp.Nullable
{
	// Token: 0x02000300 RID: 768
	public class LiftedNull : NullConstant, IMemoryLocation
	{
		// Token: 0x0600246A RID: 9322 RVA: 0x000AE19B File Offset: 0x000AC39B
		private LiftedNull(TypeSpec nullable_type, Location loc) : base(nullable_type, loc)
		{
			this.eclass = ExprClass.Value;
		}

		// Token: 0x0600246B RID: 9323 RVA: 0x000AE1AC File Offset: 0x000AC3AC
		public static Constant Create(TypeSpec nullable, Location loc)
		{
			return new LiftedNull(nullable, loc);
		}

		// Token: 0x0600246C RID: 9324 RVA: 0x000AE1B8 File Offset: 0x000AC3B8
		public static Constant CreateFromExpression(ResolveContext rc, Expression e)
		{
			if (!rc.HasSet(ResolveContext.Options.ExpressionTreeConversion))
			{
				rc.Report.Warning(458, 2, e.Location, "The result of the expression is always `null' of type `{0}'", e.Type.GetSignatureForError());
			}
			return ReducedExpression.Create(LiftedNull.Create(e.Type, e.Location), e);
		}

		// Token: 0x0600246D RID: 9325 RVA: 0x000AE210 File Offset: 0x000AC410
		public override void Emit(EmitContext ec)
		{
			LocalTemporary localTemporary = new LocalTemporary(this.type);
			localTemporary.AddressOf(ec, AddressOp.Store);
			ec.Emit(OpCodes.Initobj, this.type);
			localTemporary.Emit(ec);
			localTemporary.Release(ec);
		}

		// Token: 0x0600246E RID: 9326 RVA: 0x000AE243 File Offset: 0x000AC443
		public void AddressOf(EmitContext ec, AddressOp Mode)
		{
			LocalTemporary localTemporary = new LocalTemporary(this.type);
			localTemporary.AddressOf(ec, AddressOp.Store);
			ec.Emit(OpCodes.Initobj, this.type);
			localTemporary.AddressOf(ec, Mode);
		}
	}
}

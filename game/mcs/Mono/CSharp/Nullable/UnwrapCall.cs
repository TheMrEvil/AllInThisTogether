using System;

namespace Mono.CSharp.Nullable
{
	// Token: 0x020002FE RID: 766
	public class UnwrapCall : CompositeExpression
	{
		// Token: 0x06002463 RID: 9315 RVA: 0x00084369 File Offset: 0x00082569
		public UnwrapCall(Expression expr) : base(expr)
		{
		}

		// Token: 0x06002464 RID: 9316 RVA: 0x000AE071 File Offset: 0x000AC271
		protected override Expression DoResolve(ResolveContext rc)
		{
			base.DoResolve(rc);
			if (this.type != null)
			{
				this.type = NullableInfo.GetUnderlyingType(this.type);
			}
			return this;
		}

		// Token: 0x06002465 RID: 9317 RVA: 0x000AE098 File Offset: 0x000AC298
		public override void Emit(EmitContext ec)
		{
			CallEmitter callEmitter = default(CallEmitter);
			callEmitter.InstanceExpression = base.Child;
			callEmitter.EmitPredefined(ec, NullableInfo.GetValue(base.Child.Type), null, false, null);
		}
	}
}

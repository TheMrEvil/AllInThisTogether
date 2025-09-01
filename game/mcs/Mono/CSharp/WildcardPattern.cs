using System;

namespace Mono.CSharp
{
	// Token: 0x020001D2 RID: 466
	internal class WildcardPattern : PatternExpression
	{
		// Token: 0x06001885 RID: 6277 RVA: 0x00076B23 File Offset: 0x00074D23
		public WildcardPattern(Location loc) : base(loc)
		{
		}

		// Token: 0x06001886 RID: 6278 RVA: 0x00076B2C File Offset: 0x00074D2C
		protected override Expression DoResolve(ResolveContext rc)
		{
			this.eclass = ExprClass.Value;
			this.type = rc.BuiltinTypes.Object;
			return this;
		}

		// Token: 0x06001887 RID: 6279 RVA: 0x00076B47 File Offset: 0x00074D47
		public override void Emit(EmitContext ec)
		{
			ec.EmitInt(1);
		}
	}
}

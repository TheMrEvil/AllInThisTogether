using System;

namespace Mono.CSharp
{
	// Token: 0x02000141 RID: 321
	public abstract class IntegralConstant : Constant
	{
		// Token: 0x06001014 RID: 4116 RVA: 0x00042267 File Offset: 0x00040467
		protected IntegralConstant(TypeSpec type, Location loc) : base(loc)
		{
			this.type = type;
			this.eclass = ExprClass.Value;
		}

		// Token: 0x06001015 RID: 4117 RVA: 0x00042280 File Offset: 0x00040480
		public override void Error_ValueCannotBeConverted(ResolveContext ec, TypeSpec target, bool expl)
		{
			try
			{
				this.ConvertExplicitly(true, target);
				base.Error_ValueCannotBeConverted(ec, target, expl);
			}
			catch
			{
				ec.Report.Error(31, this.loc, "Constant value `{0}' cannot be converted to a `{1}'", this.GetValue().ToString(), target.GetSignatureForError());
			}
		}

		// Token: 0x06001016 RID: 4118 RVA: 0x000422E0 File Offset: 0x000404E0
		public override string GetValueAsLiteral()
		{
			return this.GetValue().ToString();
		}

		// Token: 0x06001017 RID: 4119
		public abstract Constant Increment();
	}
}

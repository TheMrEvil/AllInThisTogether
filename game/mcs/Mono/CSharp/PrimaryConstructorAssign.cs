using System;

namespace Mono.CSharp
{
	// Token: 0x02000117 RID: 279
	internal class PrimaryConstructorAssign : SimpleAssign
	{
		// Token: 0x06000DBC RID: 3516 RVA: 0x00032ADB File Offset: 0x00030CDB
		public PrimaryConstructorAssign(Field field, Parameter parameter) : base(null, null, parameter.Location)
		{
			this.field = field;
			this.parameter = parameter;
		}

		// Token: 0x06000DBD RID: 3517 RVA: 0x00032AFC File Offset: 0x00030CFC
		protected override Expression DoResolve(ResolveContext rc)
		{
			this.target = new FieldExpr(this.field, this.loc);
			this.source = rc.CurrentBlock.ParametersBlock.GetParameterInfo(this.parameter).CreateReferenceExpression(rc, this.loc);
			return base.DoResolve(rc);
		}

		// Token: 0x06000DBE RID: 3518 RVA: 0x00032B50 File Offset: 0x00030D50
		public override void EmitStatement(EmitContext ec)
		{
			using (ec.With(BuilderContext.Options.OmitDebugInfo, true))
			{
				base.EmitStatement(ec);
			}
		}

		// Token: 0x04000667 RID: 1639
		private readonly Field field;

		// Token: 0x04000668 RID: 1640
		private readonly Parameter parameter;
	}
}

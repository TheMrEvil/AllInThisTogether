using System;

namespace Mono.CSharp
{
	// Token: 0x02000174 RID: 372
	internal class HoistedEvaluatorVariable : HoistedVariable
	{
		// Token: 0x060011E7 RID: 4583 RVA: 0x0004A495 File Offset: 0x00048695
		public HoistedEvaluatorVariable(Field field) : base(null, field)
		{
		}

		// Token: 0x060011E8 RID: 4584 RVA: 0x0004A49F File Offset: 0x0004869F
		protected override FieldExpr GetFieldExpression(EmitContext ec)
		{
			return new FieldExpr(this.field, this.field.Location);
		}
	}
}

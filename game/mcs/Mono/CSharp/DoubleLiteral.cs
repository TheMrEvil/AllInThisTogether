using System;

namespace Mono.CSharp
{
	// Token: 0x0200023E RID: 574
	public class DoubleLiteral : DoubleConstant, ILiteralConstant
	{
		// Token: 0x06001C98 RID: 7320 RVA: 0x0008A8F3 File Offset: 0x00088AF3
		public DoubleLiteral(BuiltinTypes types, double d, Location loc) : base(types, d, loc)
		{
		}

		// Token: 0x06001C99 RID: 7321 RVA: 0x0008A900 File Offset: 0x00088B00
		public override void Error_ValueCannotBeConverted(ResolveContext ec, TypeSpec target, bool expl)
		{
			if (target.BuiltinType == BuiltinTypeSpec.Type.Float)
			{
				DoubleLiteral.Error_664(ec, this.loc, "float", "f");
				return;
			}
			if (target.BuiltinType == BuiltinTypeSpec.Type.Decimal)
			{
				DoubleLiteral.Error_664(ec, this.loc, "decimal", "m");
				return;
			}
			base.Error_ValueCannotBeConverted(ec, target, expl);
		}

		// Token: 0x06001C9A RID: 7322 RVA: 0x0008A958 File Offset: 0x00088B58
		private static void Error_664(ResolveContext ec, Location loc, string type, string suffix)
		{
			ec.Report.Error(664, loc, "Literal of type double cannot be implicitly converted to type `{0}'. Add suffix `{1}' to create a literal of this type", type, suffix);
		}

		// Token: 0x17000682 RID: 1666
		// (get) Token: 0x06001C9B RID: 7323 RVA: 0x0000212D File Offset: 0x0000032D
		public override bool IsLiteral
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06001C9C RID: 7324 RVA: 0x0008A866 File Offset: 0x00088A66
		public override object Accept(StructuralVisitor visitor)
		{
			return visitor.Visit(this);
		}
	}
}

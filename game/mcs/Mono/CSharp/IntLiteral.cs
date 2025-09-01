using System;

namespace Mono.CSharp
{
	// Token: 0x02000239 RID: 569
	public class IntLiteral : IntConstant, ILiteralConstant
	{
		// Token: 0x06001C88 RID: 7304 RVA: 0x0008A87A File Offset: 0x00088A7A
		public IntLiteral(BuiltinTypes types, int l, Location loc) : base(types, l, loc)
		{
		}

		// Token: 0x06001C89 RID: 7305 RVA: 0x0008A888 File Offset: 0x00088A88
		public override Constant ConvertImplicitly(TypeSpec type)
		{
			if (this.Value != 0 || !type.IsEnum)
			{
				return base.ConvertImplicitly(type);
			}
			Constant constant = this.ConvertImplicitly(EnumSpec.GetUnderlyingType(type));
			if (constant == null)
			{
				return null;
			}
			return new EnumConstant(constant, type);
		}

		// Token: 0x1700067D RID: 1661
		// (get) Token: 0x06001C8A RID: 7306 RVA: 0x0000212D File Offset: 0x0000032D
		public override bool IsLiteral
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06001C8B RID: 7307 RVA: 0x0008A866 File Offset: 0x00088A66
		public override object Accept(StructuralVisitor visitor)
		{
			return visitor.Visit(this);
		}
	}
}

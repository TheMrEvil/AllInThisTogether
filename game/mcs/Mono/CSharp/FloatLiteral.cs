using System;

namespace Mono.CSharp
{
	// Token: 0x0200023D RID: 573
	public class FloatLiteral : FloatConstant, ILiteralConstant
	{
		// Token: 0x06001C95 RID: 7317 RVA: 0x0008A8E7 File Offset: 0x00088AE7
		public FloatLiteral(BuiltinTypes types, float f, Location loc) : base(types, (double)f, loc)
		{
		}

		// Token: 0x17000681 RID: 1665
		// (get) Token: 0x06001C96 RID: 7318 RVA: 0x0000212D File Offset: 0x0000032D
		public override bool IsLiteral
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06001C97 RID: 7319 RVA: 0x0008A866 File Offset: 0x00088A66
		public override object Accept(StructuralVisitor visitor)
		{
			return visitor.Visit(this);
		}
	}
}

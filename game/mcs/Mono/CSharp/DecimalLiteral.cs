using System;

namespace Mono.CSharp
{
	// Token: 0x0200023F RID: 575
	public class DecimalLiteral : DecimalConstant, ILiteralConstant
	{
		// Token: 0x06001C9D RID: 7325 RVA: 0x0008A972 File Offset: 0x00088B72
		public DecimalLiteral(BuiltinTypes types, decimal d, Location loc) : base(types, d, loc)
		{
		}

		// Token: 0x17000683 RID: 1667
		// (get) Token: 0x06001C9E RID: 7326 RVA: 0x0000212D File Offset: 0x0000032D
		public override bool IsLiteral
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06001C9F RID: 7327 RVA: 0x0008A866 File Offset: 0x00088A66
		public override object Accept(StructuralVisitor visitor)
		{
			return visitor.Visit(this);
		}
	}
}

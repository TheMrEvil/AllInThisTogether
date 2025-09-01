using System;

namespace Mono.CSharp
{
	// Token: 0x02000237 RID: 567
	public class BoolLiteral : BoolConstant, ILiteralConstant
	{
		// Token: 0x06001C82 RID: 7298 RVA: 0x0008A85B File Offset: 0x00088A5B
		public BoolLiteral(BuiltinTypes types, bool val, Location loc) : base(types, val, loc)
		{
		}

		// Token: 0x1700067B RID: 1659
		// (get) Token: 0x06001C83 RID: 7299 RVA: 0x0000212D File Offset: 0x0000032D
		public override bool IsLiteral
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06001C84 RID: 7300 RVA: 0x0008A866 File Offset: 0x00088A66
		public override object Accept(StructuralVisitor visitor)
		{
			return visitor.Visit(this);
		}
	}
}

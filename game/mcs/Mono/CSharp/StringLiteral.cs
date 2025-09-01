using System;

namespace Mono.CSharp
{
	// Token: 0x02000240 RID: 576
	public class StringLiteral : StringConstant, ILiteralConstant
	{
		// Token: 0x06001CA0 RID: 7328 RVA: 0x0008A97D File Offset: 0x00088B7D
		public StringLiteral(BuiltinTypes types, string s, Location loc) : base(types, s, loc)
		{
		}

		// Token: 0x17000684 RID: 1668
		// (get) Token: 0x06001CA1 RID: 7329 RVA: 0x0000212D File Offset: 0x0000032D
		public override bool IsLiteral
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06001CA2 RID: 7330 RVA: 0x0008A866 File Offset: 0x00088A66
		public override object Accept(StructuralVisitor visitor)
		{
			return visitor.Visit(this);
		}
	}
}

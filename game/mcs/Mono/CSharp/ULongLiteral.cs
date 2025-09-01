using System;

namespace Mono.CSharp
{
	// Token: 0x0200023C RID: 572
	public class ULongLiteral : ULongConstant, ILiteralConstant
	{
		// Token: 0x06001C92 RID: 7314 RVA: 0x0008A8DC File Offset: 0x00088ADC
		public ULongLiteral(BuiltinTypes types, ulong l, Location loc) : base(types, l, loc)
		{
		}

		// Token: 0x17000680 RID: 1664
		// (get) Token: 0x06001C93 RID: 7315 RVA: 0x0000212D File Offset: 0x0000032D
		public override bool IsLiteral
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06001C94 RID: 7316 RVA: 0x0008A866 File Offset: 0x00088A66
		public override object Accept(StructuralVisitor visitor)
		{
			return visitor.Visit(this);
		}
	}
}

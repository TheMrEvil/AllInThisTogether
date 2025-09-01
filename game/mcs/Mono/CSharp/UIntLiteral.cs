using System;

namespace Mono.CSharp
{
	// Token: 0x0200023A RID: 570
	public class UIntLiteral : UIntConstant, ILiteralConstant
	{
		// Token: 0x06001C8C RID: 7308 RVA: 0x0008A8C6 File Offset: 0x00088AC6
		public UIntLiteral(BuiltinTypes types, uint l, Location loc) : base(types, l, loc)
		{
		}

		// Token: 0x1700067E RID: 1662
		// (get) Token: 0x06001C8D RID: 7309 RVA: 0x0000212D File Offset: 0x0000032D
		public override bool IsLiteral
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06001C8E RID: 7310 RVA: 0x0008A866 File Offset: 0x00088A66
		public override object Accept(StructuralVisitor visitor)
		{
			return visitor.Visit(this);
		}
	}
}

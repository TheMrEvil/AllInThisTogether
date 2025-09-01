using System;

namespace Mono.CSharp
{
	// Token: 0x0200023B RID: 571
	public class LongLiteral : LongConstant, ILiteralConstant
	{
		// Token: 0x06001C8F RID: 7311 RVA: 0x0008A8D1 File Offset: 0x00088AD1
		public LongLiteral(BuiltinTypes types, long l, Location loc) : base(types, l, loc)
		{
		}

		// Token: 0x1700067F RID: 1663
		// (get) Token: 0x06001C90 RID: 7312 RVA: 0x0000212D File Offset: 0x0000032D
		public override bool IsLiteral
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06001C91 RID: 7313 RVA: 0x0008A866 File Offset: 0x00088A66
		public override object Accept(StructuralVisitor visitor)
		{
			return visitor.Visit(this);
		}
	}
}

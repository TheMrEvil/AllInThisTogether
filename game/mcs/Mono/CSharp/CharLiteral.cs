using System;

namespace Mono.CSharp
{
	// Token: 0x02000238 RID: 568
	public class CharLiteral : CharConstant, ILiteralConstant
	{
		// Token: 0x06001C85 RID: 7301 RVA: 0x0008A86F File Offset: 0x00088A6F
		public CharLiteral(BuiltinTypes types, char c, Location loc) : base(types, c, loc)
		{
		}

		// Token: 0x1700067C RID: 1660
		// (get) Token: 0x06001C86 RID: 7302 RVA: 0x0000212D File Offset: 0x0000032D
		public override bool IsLiteral
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06001C87 RID: 7303 RVA: 0x0008A866 File Offset: 0x00088A66
		public override object Accept(StructuralVisitor visitor)
		{
			return visitor.Visit(this);
		}
	}
}

using System;

namespace Mono.CSharp
{
	// Token: 0x020001FA RID: 506
	public class ConditionalMemberAccess : MemberAccess
	{
		// Token: 0x06001A4D RID: 6733 RVA: 0x00080F8A File Offset: 0x0007F18A
		public ConditionalMemberAccess(Expression expr, string identifier, TypeArguments args, Location loc) : base(expr, identifier, args, loc)
		{
		}

		// Token: 0x06001A4E RID: 6734 RVA: 0x0000212D File Offset: 0x0000032D
		public override bool HasConditionalAccess()
		{
			return true;
		}
	}
}

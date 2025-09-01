using System;

namespace Mono.CSharp.Linq
{
	// Token: 0x020002FA RID: 762
	internal sealed class TransparentMemberAccess : MemberAccess
	{
		// Token: 0x06002442 RID: 9282 RVA: 0x000ADBC1 File Offset: 0x000ABDC1
		public TransparentMemberAccess(Expression expr, string name) : base(expr, name)
		{
		}

		// Token: 0x06002443 RID: 9283 RVA: 0x000ADBCB File Offset: 0x000ABDCB
		public override Expression DoResolveLValue(ResolveContext rc, Expression right_side)
		{
			rc.Report.Error(1947, this.loc, "A range variable `{0}' cannot be assigned to. Consider using `let' clause to store the value", base.Name);
			return null;
		}
	}
}

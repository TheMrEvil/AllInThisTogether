using System;
using Parse.Abstractions.Infrastructure;

namespace Parse
{
	// Token: 0x02000022 RID: 34
	public static class RoleServiceExtensions
	{
		// Token: 0x060001D0 RID: 464 RVA: 0x00008408 File Offset: 0x00006608
		public static ParseQuery<ParseRole> GetRoleQuery(this IServiceHub serviceHub)
		{
			return serviceHub.GetQuery<ParseRole>();
		}
	}
}

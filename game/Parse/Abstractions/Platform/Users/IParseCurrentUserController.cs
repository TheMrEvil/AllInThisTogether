using System;
using System.Threading;
using System.Threading.Tasks;
using Parse.Abstractions.Infrastructure;
using Parse.Abstractions.Platform.Objects;

namespace Parse.Abstractions.Platform.Users
{
	// Token: 0x02000076 RID: 118
	public interface IParseCurrentUserController : IParseObjectCurrentController<ParseUser>
	{
		// Token: 0x060004F8 RID: 1272
		Task<string> GetCurrentSessionTokenAsync(IServiceHub serviceHub, CancellationToken cancellationToken = default(CancellationToken));

		// Token: 0x060004F9 RID: 1273
		Task LogOutAsync(IServiceHub serviceHub, CancellationToken cancellationToken = default(CancellationToken));
	}
}

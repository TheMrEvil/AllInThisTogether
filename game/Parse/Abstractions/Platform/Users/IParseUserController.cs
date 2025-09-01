using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Parse.Abstractions.Infrastructure;
using Parse.Abstractions.Infrastructure.Control;
using Parse.Abstractions.Platform.Objects;

namespace Parse.Abstractions.Platform.Users
{
	// Token: 0x02000077 RID: 119
	public interface IParseUserController
	{
		// Token: 0x060004FA RID: 1274
		Task<IObjectState> SignUpAsync(IObjectState state, IDictionary<string, IParseFieldOperation> operations, IServiceHub serviceHub, CancellationToken cancellationToken = default(CancellationToken));

		// Token: 0x060004FB RID: 1275
		Task<IObjectState> LogInAsync(string username, string password, IServiceHub serviceHub, CancellationToken cancellationToken = default(CancellationToken));

		// Token: 0x060004FC RID: 1276
		Task<IObjectState> LogInAsync(string authType, IDictionary<string, object> data, IServiceHub serviceHub, CancellationToken cancellationToken = default(CancellationToken));

		// Token: 0x060004FD RID: 1277
		Task<IObjectState> GetUserAsync(string sessionToken, IServiceHub serviceHub, CancellationToken cancellationToken = default(CancellationToken));

		// Token: 0x060004FE RID: 1278
		Task RequestPasswordResetAsync(string email, CancellationToken cancellationToken = default(CancellationToken));

		// Token: 0x17000181 RID: 385
		// (get) Token: 0x060004FF RID: 1279
		// (set) Token: 0x06000500 RID: 1280
		bool RevocableSessionEnabled { get; set; }

		// Token: 0x17000182 RID: 386
		// (get) Token: 0x06000501 RID: 1281
		object RevocableSessionEnabledMutex { get; }
	}
}

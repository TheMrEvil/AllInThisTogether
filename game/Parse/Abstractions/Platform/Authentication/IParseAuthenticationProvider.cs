using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Parse.Abstractions.Platform.Authentication
{
	// Token: 0x02000089 RID: 137
	public interface IParseAuthenticationProvider
	{
		// Token: 0x0600053F RID: 1343
		Task<IDictionary<string, object>> AuthenticateAsync(CancellationToken cancellationToken);

		// Token: 0x06000540 RID: 1344
		void Deauthenticate();

		// Token: 0x06000541 RID: 1345
		bool RestoreAuthentication(IDictionary<string, object> authData);

		// Token: 0x17000191 RID: 401
		// (get) Token: 0x06000542 RID: 1346
		string AuthType { get; }
	}
}

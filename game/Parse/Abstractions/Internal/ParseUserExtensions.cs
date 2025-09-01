using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Parse.Abstractions.Internal
{
	// Token: 0x02000075 RID: 117
	public static class ParseUserExtensions
	{
		// Token: 0x060004F4 RID: 1268 RVA: 0x00012B15 File Offset: 0x00010D15
		public static Task UnlinkFromAsync(this ParseUser user, string authType, CancellationToken cancellationToken)
		{
			return user.UnlinkFromAsync(authType, cancellationToken);
		}

		// Token: 0x060004F5 RID: 1269 RVA: 0x00012B1F File Offset: 0x00010D1F
		public static Task LinkWithAsync(this ParseUser user, string authType, CancellationToken cancellationToken)
		{
			return user.LinkWithAsync(authType, cancellationToken);
		}

		// Token: 0x060004F6 RID: 1270 RVA: 0x00012B29 File Offset: 0x00010D29
		public static Task LinkWithAsync(this ParseUser user, string authType, IDictionary<string, object> data, CancellationToken cancellationToken)
		{
			return user.LinkWithAsync(authType, data, cancellationToken);
		}

		// Token: 0x060004F7 RID: 1271 RVA: 0x00012B34 File Offset: 0x00010D34
		public static Task UpgradeToRevocableSessionAsync(this ParseUser user, CancellationToken cancellationToken)
		{
			return user.UpgradeToRevocableSessionAsync(cancellationToken);
		}
	}
}

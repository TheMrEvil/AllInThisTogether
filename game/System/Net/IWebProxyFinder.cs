using System;
using System.Collections.Generic;

namespace System.Net
{
	// Token: 0x020005D9 RID: 1497
	internal interface IWebProxyFinder : IDisposable
	{
		// Token: 0x06003032 RID: 12338
		bool GetProxies(Uri destination, out IList<string> proxyList);

		// Token: 0x06003033 RID: 12339
		void Abort();

		// Token: 0x06003034 RID: 12340
		void Reset();

		// Token: 0x170009A3 RID: 2467
		// (get) Token: 0x06003035 RID: 12341
		bool IsValid { get; }
	}
}

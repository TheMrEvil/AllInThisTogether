using System;

namespace System.Net
{
	// Token: 0x02000632 RID: 1586
	internal interface IAutoWebProxy : IWebProxy
	{
		// Token: 0x06003221 RID: 12833
		ProxyChain GetProxies(Uri destination);
	}
}

using System;

namespace System.Net
{
	// Token: 0x02000637 RID: 1591
	internal class StaticProxy : ProxyChain
	{
		// Token: 0x06003237 RID: 12855 RVA: 0x000ADB2A File Offset: 0x000ABD2A
		internal StaticProxy(Uri destination, Uri proxy) : base(destination)
		{
			if (proxy == null)
			{
				throw new ArgumentNullException("proxy");
			}
			this.m_Proxy = proxy;
		}

		// Token: 0x06003238 RID: 12856 RVA: 0x000ADB4E File Offset: 0x000ABD4E
		protected override bool GetNextProxy(out Uri proxy)
		{
			proxy = this.m_Proxy;
			if (proxy == null)
			{
				return false;
			}
			this.m_Proxy = null;
			return true;
		}

		// Token: 0x04001D5A RID: 7514
		private Uri m_Proxy;
	}
}

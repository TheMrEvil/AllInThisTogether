using System;

namespace System.Net
{
	// Token: 0x02000635 RID: 1589
	internal class ProxyScriptChain : ProxyChain
	{
		// Token: 0x06003232 RID: 12850 RVA: 0x000ADA58 File Offset: 0x000ABC58
		internal ProxyScriptChain(WebProxy proxy, Uri destination) : base(destination)
		{
			this.m_Proxy = proxy;
		}

		// Token: 0x06003233 RID: 12851 RVA: 0x000ADA68 File Offset: 0x000ABC68
		protected override bool GetNextProxy(out Uri proxy)
		{
			if (this.m_CurrentIndex < 0)
			{
				proxy = null;
				return false;
			}
			if (this.m_CurrentIndex == 0)
			{
				this.m_ScriptProxies = this.m_Proxy.GetProxiesAuto(base.Destination, ref this.m_SyncStatus);
			}
			if (this.m_ScriptProxies == null || this.m_CurrentIndex >= this.m_ScriptProxies.Length)
			{
				proxy = this.m_Proxy.GetProxyAutoFailover(base.Destination);
				this.m_CurrentIndex = -1;
				return true;
			}
			Uri[] scriptProxies = this.m_ScriptProxies;
			int currentIndex = this.m_CurrentIndex;
			this.m_CurrentIndex = currentIndex + 1;
			proxy = scriptProxies[currentIndex];
			return true;
		}

		// Token: 0x06003234 RID: 12852 RVA: 0x000ADAF7 File Offset: 0x000ABCF7
		internal override void Abort()
		{
			this.m_Proxy.AbortGetProxiesAuto(ref this.m_SyncStatus);
		}

		// Token: 0x04001D55 RID: 7509
		private WebProxy m_Proxy;

		// Token: 0x04001D56 RID: 7510
		private Uri[] m_ScriptProxies;

		// Token: 0x04001D57 RID: 7511
		private int m_CurrentIndex;

		// Token: 0x04001D58 RID: 7512
		private int m_SyncStatus;
	}
}

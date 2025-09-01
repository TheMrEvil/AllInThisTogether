using System;

namespace System.Net
{
	// Token: 0x02000647 RID: 1607
	[Serializable]
	internal sealed class EmptyWebProxy : IAutoWebProxy, IWebProxy
	{
		// Token: 0x06003282 RID: 12930 RVA: 0x0000219B File Offset: 0x0000039B
		public EmptyWebProxy()
		{
		}

		// Token: 0x06003283 RID: 12931 RVA: 0x00003914 File Offset: 0x00001B14
		public Uri GetProxy(Uri uri)
		{
			return uri;
		}

		// Token: 0x06003284 RID: 12932 RVA: 0x0000390E File Offset: 0x00001B0E
		public bool IsBypassed(Uri uri)
		{
			return true;
		}

		// Token: 0x17000A20 RID: 2592
		// (get) Token: 0x06003285 RID: 12933 RVA: 0x000AEEA0 File Offset: 0x000AD0A0
		// (set) Token: 0x06003286 RID: 12934 RVA: 0x000AEEA8 File Offset: 0x000AD0A8
		public ICredentials Credentials
		{
			get
			{
				return this.m_credentials;
			}
			set
			{
				this.m_credentials = value;
			}
		}

		// Token: 0x06003287 RID: 12935 RVA: 0x000AEEB1 File Offset: 0x000AD0B1
		ProxyChain IAutoWebProxy.GetProxies(Uri destination)
		{
			return new DirectProxy(destination);
		}

		// Token: 0x04001D8A RID: 7562
		[NonSerialized]
		private ICredentials m_credentials;
	}
}

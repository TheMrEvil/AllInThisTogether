using System;

namespace System.Net
{
	// Token: 0x02000636 RID: 1590
	internal class DirectProxy : ProxyChain
	{
		// Token: 0x06003235 RID: 12853 RVA: 0x000ADB0A File Offset: 0x000ABD0A
		internal DirectProxy(Uri destination) : base(destination)
		{
		}

		// Token: 0x06003236 RID: 12854 RVA: 0x000ADB13 File Offset: 0x000ABD13
		protected override bool GetNextProxy(out Uri proxy)
		{
			proxy = null;
			if (this.m_ProxyRetrieved)
			{
				return false;
			}
			this.m_ProxyRetrieved = true;
			return true;
		}

		// Token: 0x04001D59 RID: 7513
		private bool m_ProxyRetrieved;
	}
}

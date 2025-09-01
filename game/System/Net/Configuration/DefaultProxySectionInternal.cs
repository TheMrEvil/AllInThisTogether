using System;
using System.Configuration;
using System.Threading;

namespace System.Net.Configuration
{
	// Token: 0x02000757 RID: 1879
	internal sealed class DefaultProxySectionInternal
	{
		// Token: 0x06003B4C RID: 15180 RVA: 0x000CC118 File Offset: 0x000CA318
		private static IWebProxy GetDefaultProxy_UsingOldMonoCode()
		{
			DefaultProxySection defaultProxySection = ConfigurationManager.GetSection("system.net/defaultProxy") as DefaultProxySection;
			if (defaultProxySection == null)
			{
				return DefaultProxySectionInternal.GetSystemWebProxy();
			}
			ProxyElement proxy = defaultProxySection.Proxy;
			WebProxy webProxy;
			if (proxy.UseSystemDefault != ProxyElement.UseSystemDefaultValues.False && proxy.ProxyAddress == null)
			{
				IWebProxy systemWebProxy = DefaultProxySectionInternal.GetSystemWebProxy();
				if (!(systemWebProxy is WebProxy))
				{
					return systemWebProxy;
				}
				webProxy = (WebProxy)systemWebProxy;
			}
			else
			{
				webProxy = new WebProxy();
			}
			if (proxy.ProxyAddress != null)
			{
				webProxy.Address = proxy.ProxyAddress;
			}
			if (proxy.BypassOnLocal != ProxyElement.BypassOnLocalValues.Unspecified)
			{
				webProxy.BypassProxyOnLocal = (proxy.BypassOnLocal == ProxyElement.BypassOnLocalValues.True);
			}
			foreach (object obj in defaultProxySection.BypassList)
			{
				BypassElement bypassElement = (BypassElement)obj;
				webProxy.BypassArrayList.Add(bypassElement.Address);
			}
			return webProxy;
		}

		// Token: 0x06003B4D RID: 15181 RVA: 0x000AB8FE File Offset: 0x000A9AFE
		private static IWebProxy GetSystemWebProxy()
		{
			return System.Net.WebProxy.CreateDefaultProxy();
		}

		// Token: 0x17000D62 RID: 3426
		// (get) Token: 0x06003B4E RID: 15182 RVA: 0x000CC210 File Offset: 0x000CA410
		internal static object ClassSyncObject
		{
			get
			{
				if (DefaultProxySectionInternal.classSyncObject == null)
				{
					object value = new object();
					Interlocked.CompareExchange(ref DefaultProxySectionInternal.classSyncObject, value, null);
				}
				return DefaultProxySectionInternal.classSyncObject;
			}
		}

		// Token: 0x06003B4F RID: 15183 RVA: 0x000CC23C File Offset: 0x000CA43C
		internal static DefaultProxySectionInternal GetSection()
		{
			object obj = DefaultProxySectionInternal.ClassSyncObject;
			DefaultProxySectionInternal result;
			lock (obj)
			{
				result = new DefaultProxySectionInternal
				{
					webProxy = DefaultProxySectionInternal.GetDefaultProxy_UsingOldMonoCode()
				};
			}
			return result;
		}

		// Token: 0x17000D63 RID: 3427
		// (get) Token: 0x06003B50 RID: 15184 RVA: 0x000CC288 File Offset: 0x000CA488
		internal IWebProxy WebProxy
		{
			get
			{
				return this.webProxy;
			}
		}

		// Token: 0x06003B51 RID: 15185 RVA: 0x0000219B File Offset: 0x0000039B
		public DefaultProxySectionInternal()
		{
		}

		// Token: 0x04002363 RID: 9059
		private IWebProxy webProxy;

		// Token: 0x04002364 RID: 9060
		private static object classSyncObject;
	}
}

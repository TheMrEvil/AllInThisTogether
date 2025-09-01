using System;
using System.Collections.Generic;
using System.Net;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;

namespace Mono.Net
{
	// Token: 0x02000082 RID: 130
	internal static class CFNetwork
	{
		// Token: 0x060001F6 RID: 502
		[DllImport("/System/Library/Frameworks/CoreServices.framework/Frameworks/CFNetwork.framework/CFNetwork", EntryPoint = "CFNetworkCopyProxiesForAutoConfigurationScript")]
		private static extern IntPtr CFNetworkCopyProxiesForAutoConfigurationScriptSequential(IntPtr proxyAutoConfigurationScript, IntPtr targetURL, out IntPtr error);

		// Token: 0x060001F7 RID: 503
		[DllImport("/System/Library/Frameworks/CoreServices.framework/Frameworks/CFNetwork.framework/CFNetwork")]
		private static extern IntPtr CFNetworkExecuteProxyAutoConfigurationURL(IntPtr proxyAutoConfigURL, IntPtr targetURL, CFNetwork.CFProxyAutoConfigurationResultCallback cb, ref CFStreamClientContext clientContext);

		// Token: 0x060001F8 RID: 504 RVA: 0x00005AFC File Offset: 0x00003CFC
		private static void CFNetworkCopyProxiesForAutoConfigurationScriptThread()
		{
			bool flag = true;
			for (;;)
			{
				CFNetwork.proxy_event.WaitOne();
				do
				{
					object obj = CFNetwork.lock_obj;
					CFNetwork.GetProxyData getProxyData;
					lock (obj)
					{
						if (CFNetwork.get_proxy_queue.Count == 0)
						{
							break;
						}
						getProxyData = CFNetwork.get_proxy_queue.Dequeue();
						flag = (CFNetwork.get_proxy_queue.Count > 0);
					}
					getProxyData.result = CFNetwork.CFNetworkCopyProxiesForAutoConfigurationScriptSequential(getProxyData.script, getProxyData.targetUri, out getProxyData.error);
					getProxyData.evt.Set();
				}
				while (flag);
			}
		}

		// Token: 0x060001F9 RID: 505 RVA: 0x00005B98 File Offset: 0x00003D98
		private static IntPtr CFNetworkCopyProxiesForAutoConfigurationScript(IntPtr proxyAutoConfigurationScript, IntPtr targetURL, out IntPtr error)
		{
			IntPtr result;
			using (CFNetwork.GetProxyData getProxyData = new CFNetwork.GetProxyData())
			{
				getProxyData.script = proxyAutoConfigurationScript;
				getProxyData.targetUri = targetURL;
				object obj = CFNetwork.lock_obj;
				lock (obj)
				{
					if (CFNetwork.get_proxy_queue == null)
					{
						CFNetwork.get_proxy_queue = new Queue<CFNetwork.GetProxyData>();
						CFNetwork.proxy_event = new AutoResetEvent(false);
						new Thread(new ThreadStart(CFNetwork.CFNetworkCopyProxiesForAutoConfigurationScriptThread))
						{
							IsBackground = true
						}.Start();
					}
					CFNetwork.get_proxy_queue.Enqueue(getProxyData);
					CFNetwork.proxy_event.Set();
				}
				getProxyData.evt.WaitOne();
				error = getProxyData.error;
				result = getProxyData.result;
			}
			return result;
		}

		// Token: 0x060001FA RID: 506 RVA: 0x00005C68 File Offset: 0x00003E68
		private static CFArray CopyProxiesForAutoConfigurationScript(IntPtr proxyAutoConfigurationScript, CFUrl targetURL)
		{
			IntPtr zero = IntPtr.Zero;
			IntPtr intPtr = CFNetwork.CFNetworkCopyProxiesForAutoConfigurationScript(proxyAutoConfigurationScript, targetURL.Handle, out zero);
			if (intPtr == IntPtr.Zero)
			{
				return null;
			}
			return new CFArray(intPtr, true);
		}

		// Token: 0x060001FB RID: 507 RVA: 0x00005CA0 File Offset: 0x00003EA0
		public static CFProxy[] GetProxiesForAutoConfigurationScript(IntPtr proxyAutoConfigurationScript, CFUrl targetURL)
		{
			if (proxyAutoConfigurationScript == IntPtr.Zero)
			{
				throw new ArgumentNullException("proxyAutoConfigurationScript");
			}
			if (targetURL == null)
			{
				throw new ArgumentNullException("targetURL");
			}
			CFArray cfarray = CFNetwork.CopyProxiesForAutoConfigurationScript(proxyAutoConfigurationScript, targetURL);
			if (cfarray == null)
			{
				return null;
			}
			CFProxy[] array = new CFProxy[cfarray.Count];
			for (int i = 0; i < array.Length; i++)
			{
				CFDictionary settings = new CFDictionary(cfarray[i], false);
				array[i] = new CFProxy(settings);
			}
			cfarray.Dispose();
			return array;
		}

		// Token: 0x060001FC RID: 508 RVA: 0x00005D18 File Offset: 0x00003F18
		public static CFProxy[] GetProxiesForAutoConfigurationScript(IntPtr proxyAutoConfigurationScript, Uri targetUri)
		{
			if (proxyAutoConfigurationScript == IntPtr.Zero)
			{
				throw new ArgumentNullException("proxyAutoConfigurationScript");
			}
			if (targetUri == null)
			{
				throw new ArgumentNullException("targetUri");
			}
			CFUrl cfurl = CFUrl.Create(targetUri.AbsoluteUri);
			CFProxy[] proxiesForAutoConfigurationScript = CFNetwork.GetProxiesForAutoConfigurationScript(proxyAutoConfigurationScript, cfurl);
			cfurl.Dispose();
			return proxiesForAutoConfigurationScript;
		}

		// Token: 0x060001FD RID: 509 RVA: 0x00005D6C File Offset: 0x00003F6C
		public static CFProxy[] ExecuteProxyAutoConfigurationURL(IntPtr proxyAutoConfigURL, Uri targetURL)
		{
			CFUrl cfurl = CFUrl.Create(targetURL.AbsoluteUri);
			if (cfurl == null)
			{
				return null;
			}
			CFProxy[] proxies = null;
			CFRunLoop runLoop = CFRunLoop.CurrentRunLoop;
			CFNetwork.CFProxyAutoConfigurationResultCallback cb = delegate(IntPtr client, IntPtr proxyList, IntPtr error)
			{
				if (proxyList != IntPtr.Zero)
				{
					CFArray cfarray = new CFArray(proxyList, false);
					proxies = new CFProxy[cfarray.Count];
					for (int i = 0; i < proxies.Length; i++)
					{
						CFDictionary settings = new CFDictionary(cfarray[i], false);
						proxies[i] = new CFProxy(settings);
					}
					cfarray.Dispose();
				}
				runLoop.Stop();
			};
			CFStreamClientContext cfstreamClientContext = default(CFStreamClientContext);
			IntPtr source = CFNetwork.CFNetworkExecuteProxyAutoConfigurationURL(proxyAutoConfigURL, cfurl.Handle, cb, ref cfstreamClientContext);
			CFString mode = CFString.Create("Mono.MacProxy");
			runLoop.AddSource(source, mode);
			runLoop.RunInMode(mode, double.MaxValue, false);
			runLoop.RemoveSource(source, mode);
			return proxies;
		}

		// Token: 0x060001FE RID: 510
		[DllImport("/System/Library/Frameworks/CoreServices.framework/Frameworks/CFNetwork.framework/CFNetwork")]
		private static extern IntPtr CFNetworkCopyProxiesForURL(IntPtr url, IntPtr proxySettings);

		// Token: 0x060001FF RID: 511 RVA: 0x00005E10 File Offset: 0x00004010
		private static CFArray CopyProxiesForURL(CFUrl url, CFDictionary proxySettings)
		{
			IntPtr intPtr = CFNetwork.CFNetworkCopyProxiesForURL(url.Handle, (proxySettings != null) ? proxySettings.Handle : IntPtr.Zero);
			if (intPtr == IntPtr.Zero)
			{
				return null;
			}
			return new CFArray(intPtr, true);
		}

		// Token: 0x06000200 RID: 512 RVA: 0x00005E50 File Offset: 0x00004050
		public static CFProxy[] GetProxiesForURL(CFUrl url, CFProxySettings proxySettings)
		{
			if (url == null || url.Handle == IntPtr.Zero)
			{
				throw new ArgumentNullException("url");
			}
			if (proxySettings == null)
			{
				proxySettings = CFNetwork.GetSystemProxySettings();
			}
			CFArray cfarray = CFNetwork.CopyProxiesForURL(url, proxySettings.Dictionary);
			if (cfarray == null)
			{
				return null;
			}
			CFProxy[] array = new CFProxy[cfarray.Count];
			for (int i = 0; i < array.Length; i++)
			{
				CFDictionary settings = new CFDictionary(cfarray[i], false);
				array[i] = new CFProxy(settings);
			}
			cfarray.Dispose();
			return array;
		}

		// Token: 0x06000201 RID: 513 RVA: 0x00005ED4 File Offset: 0x000040D4
		public static CFProxy[] GetProxiesForUri(Uri uri, CFProxySettings proxySettings)
		{
			if (uri == null)
			{
				throw new ArgumentNullException("uri");
			}
			CFUrl cfurl = CFUrl.Create(uri.AbsoluteUri);
			if (cfurl == null)
			{
				return null;
			}
			CFProxy[] proxiesForURL = CFNetwork.GetProxiesForURL(cfurl, proxySettings);
			cfurl.Dispose();
			return proxiesForURL;
		}

		// Token: 0x06000202 RID: 514
		[DllImport("/System/Library/Frameworks/CoreServices.framework/Frameworks/CFNetwork.framework/CFNetwork")]
		private static extern IntPtr CFNetworkCopySystemProxySettings();

		// Token: 0x06000203 RID: 515 RVA: 0x00005F18 File Offset: 0x00004118
		public static CFProxySettings GetSystemProxySettings()
		{
			IntPtr intPtr = CFNetwork.CFNetworkCopySystemProxySettings();
			if (intPtr == IntPtr.Zero)
			{
				return null;
			}
			return new CFProxySettings(new CFDictionary(intPtr, true));
		}

		// Token: 0x06000204 RID: 516 RVA: 0x00005F46 File Offset: 0x00004146
		public static IWebProxy GetDefaultProxy()
		{
			return new CFNetwork.CFWebProxy();
		}

		// Token: 0x06000205 RID: 517 RVA: 0x00005F4D File Offset: 0x0000414D
		// Note: this type is marked as 'beforefieldinit'.
		static CFNetwork()
		{
		}

		// Token: 0x040001EE RID: 494
		public const string CFNetworkLibrary = "/System/Library/Frameworks/CoreServices.framework/Frameworks/CFNetwork.framework/CFNetwork";

		// Token: 0x040001EF RID: 495
		private static object lock_obj = new object();

		// Token: 0x040001F0 RID: 496
		private static Queue<CFNetwork.GetProxyData> get_proxy_queue;

		// Token: 0x040001F1 RID: 497
		private static AutoResetEvent proxy_event;

		// Token: 0x02000083 RID: 131
		private class GetProxyData : IDisposable
		{
			// Token: 0x06000206 RID: 518 RVA: 0x00005F59 File Offset: 0x00004159
			public void Dispose()
			{
				this.evt.Close();
			}

			// Token: 0x06000207 RID: 519 RVA: 0x00005F66 File Offset: 0x00004166
			public GetProxyData()
			{
			}

			// Token: 0x040001F2 RID: 498
			public IntPtr script;

			// Token: 0x040001F3 RID: 499
			public IntPtr targetUri;

			// Token: 0x040001F4 RID: 500
			public IntPtr error;

			// Token: 0x040001F5 RID: 501
			public IntPtr result;

			// Token: 0x040001F6 RID: 502
			public ManualResetEvent evt = new ManualResetEvent(false);
		}

		// Token: 0x02000084 RID: 132
		// (Invoke) Token: 0x06000209 RID: 521
		private delegate void CFProxyAutoConfigurationResultCallback(IntPtr client, IntPtr proxyList, IntPtr error);

		// Token: 0x02000085 RID: 133
		private class CFWebProxy : IWebProxy
		{
			// Token: 0x0600020C RID: 524 RVA: 0x0000219B File Offset: 0x0000039B
			public CFWebProxy()
			{
			}

			// Token: 0x1700004A RID: 74
			// (get) Token: 0x0600020D RID: 525 RVA: 0x00005F7A File Offset: 0x0000417A
			// (set) Token: 0x0600020E RID: 526 RVA: 0x00005F82 File Offset: 0x00004182
			public ICredentials Credentials
			{
				get
				{
					return this.credentials;
				}
				set
				{
					this.userSpecified = true;
					this.credentials = value;
				}
			}

			// Token: 0x0600020F RID: 527 RVA: 0x00005F94 File Offset: 0x00004194
			private static Uri GetProxyUri(CFProxy proxy, out NetworkCredential credentials)
			{
				CFProxyType proxyType = proxy.ProxyType;
				string str;
				if (proxyType != CFProxyType.FTP)
				{
					if (proxyType - CFProxyType.HTTP > 1)
					{
						credentials = null;
						return null;
					}
					str = "http://";
				}
				else
				{
					str = "ftp://";
				}
				string username = proxy.Username;
				string password = proxy.Password;
				string hostName = proxy.HostName;
				int port = proxy.Port;
				if (username != null)
				{
					credentials = new NetworkCredential(username, password);
				}
				else
				{
					credentials = null;
				}
				return new Uri(str + hostName + ((port != 0) ? (":" + port.ToString()) : string.Empty), UriKind.Absolute);
			}

			// Token: 0x06000210 RID: 528 RVA: 0x00006023 File Offset: 0x00004223
			private static Uri GetProxyUriFromScript(IntPtr script, Uri targetUri, out NetworkCredential credentials)
			{
				return CFNetwork.CFWebProxy.SelectProxy(CFNetwork.GetProxiesForAutoConfigurationScript(script, targetUri), targetUri, out credentials);
			}

			// Token: 0x06000211 RID: 529 RVA: 0x00006033 File Offset: 0x00004233
			private static Uri ExecuteProxyAutoConfigurationURL(IntPtr proxyAutoConfigURL, Uri targetUri, out NetworkCredential credentials)
			{
				return CFNetwork.CFWebProxy.SelectProxy(CFNetwork.ExecuteProxyAutoConfigurationURL(proxyAutoConfigURL, targetUri), targetUri, out credentials);
			}

			// Token: 0x06000212 RID: 530 RVA: 0x00006044 File Offset: 0x00004244
			private static Uri SelectProxy(CFProxy[] proxies, Uri targetUri, out NetworkCredential credentials)
			{
				if (proxies == null)
				{
					credentials = null;
					return targetUri;
				}
				for (int i = 0; i < proxies.Length; i++)
				{
					switch (proxies[i].ProxyType)
					{
					case CFProxyType.None:
						credentials = null;
						return targetUri;
					case CFProxyType.FTP:
					case CFProxyType.HTTP:
					case CFProxyType.HTTPS:
						return CFNetwork.CFWebProxy.GetProxyUri(proxies[i], out credentials);
					}
				}
				credentials = null;
				return null;
			}

			// Token: 0x06000213 RID: 531 RVA: 0x000060A8 File Offset: 0x000042A8
			public Uri GetProxy(Uri targetUri)
			{
				NetworkCredential networkCredential = null;
				Uri uri = null;
				if (targetUri == null)
				{
					throw new ArgumentNullException("targetUri");
				}
				try
				{
					CFProxySettings systemProxySettings = CFNetwork.GetSystemProxySettings();
					CFProxy[] proxiesForUri = CFNetwork.GetProxiesForUri(targetUri, systemProxySettings);
					if (proxiesForUri != null)
					{
						int num = 0;
						while (num < proxiesForUri.Length && uri == null)
						{
							switch (proxiesForUri[num].ProxyType)
							{
							case CFProxyType.None:
								uri = targetUri;
								break;
							case CFProxyType.AutoConfigurationUrl:
								uri = CFNetwork.CFWebProxy.ExecuteProxyAutoConfigurationURL(proxiesForUri[num].AutoConfigurationUrl, targetUri, out networkCredential);
								break;
							case CFProxyType.AutoConfigurationJavaScript:
								uri = CFNetwork.CFWebProxy.GetProxyUriFromScript(proxiesForUri[num].AutoConfigurationJavaScript, targetUri, out networkCredential);
								break;
							case CFProxyType.FTP:
							case CFProxyType.HTTP:
							case CFProxyType.HTTPS:
								uri = CFNetwork.CFWebProxy.GetProxyUri(proxiesForUri[num], out networkCredential);
								break;
							}
							num++;
						}
						if (uri == null)
						{
							uri = targetUri;
						}
					}
					else
					{
						uri = targetUri;
					}
				}
				catch
				{
					uri = targetUri;
				}
				if (!this.userSpecified)
				{
					this.credentials = networkCredential;
				}
				return uri;
			}

			// Token: 0x06000214 RID: 532 RVA: 0x0000619C File Offset: 0x0000439C
			public bool IsBypassed(Uri targetUri)
			{
				if (targetUri == null)
				{
					throw new ArgumentNullException("targetUri");
				}
				return this.GetProxy(targetUri) == targetUri;
			}

			// Token: 0x040001F7 RID: 503
			private ICredentials credentials;

			// Token: 0x040001F8 RID: 504
			private bool userSpecified;
		}

		// Token: 0x02000086 RID: 134
		[CompilerGenerated]
		private sealed class <>c__DisplayClass13_0
		{
			// Token: 0x06000215 RID: 533 RVA: 0x0000219B File Offset: 0x0000039B
			public <>c__DisplayClass13_0()
			{
			}

			// Token: 0x06000216 RID: 534 RVA: 0x000061C0 File Offset: 0x000043C0
			internal void <ExecuteProxyAutoConfigurationURL>b__0(IntPtr client, IntPtr proxyList, IntPtr error)
			{
				if (proxyList != IntPtr.Zero)
				{
					CFArray cfarray = new CFArray(proxyList, false);
					this.proxies = new CFProxy[cfarray.Count];
					for (int i = 0; i < this.proxies.Length; i++)
					{
						CFDictionary settings = new CFDictionary(cfarray[i], false);
						this.proxies[i] = new CFProxy(settings);
					}
					cfarray.Dispose();
				}
				this.runLoop.Stop();
			}

			// Token: 0x040001F9 RID: 505
			public CFProxy[] proxies;

			// Token: 0x040001FA RID: 506
			public CFRunLoop runLoop;
		}
	}
}

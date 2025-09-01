using System;
using System.Collections;

namespace System.Net
{
	// Token: 0x02000681 RID: 1665
	internal sealed class EndPointManager
	{
		// Token: 0x06003472 RID: 13426 RVA: 0x0000219B File Offset: 0x0000039B
		private EndPointManager()
		{
		}

		// Token: 0x06003473 RID: 13427 RVA: 0x000B6EFC File Offset: 0x000B50FC
		public static void AddListener(HttpListener listener)
		{
			ArrayList arrayList = new ArrayList();
			try
			{
				Hashtable obj = EndPointManager.ip_to_endpoints;
				lock (obj)
				{
					foreach (string text in listener.Prefixes)
					{
						EndPointManager.AddPrefixInternal(text, listener);
						arrayList.Add(text);
					}
				}
			}
			catch
			{
				foreach (object obj2 in arrayList)
				{
					EndPointManager.RemovePrefix((string)obj2, listener);
				}
				throw;
			}
		}

		// Token: 0x06003474 RID: 13428 RVA: 0x000B6FDC File Offset: 0x000B51DC
		public static void AddPrefix(string prefix, HttpListener listener)
		{
			Hashtable obj = EndPointManager.ip_to_endpoints;
			lock (obj)
			{
				EndPointManager.AddPrefixInternal(prefix, listener);
			}
		}

		// Token: 0x06003475 RID: 13429 RVA: 0x000B701C File Offset: 0x000B521C
		private static void AddPrefixInternal(string p, HttpListener listener)
		{
			ListenerPrefix listenerPrefix = new ListenerPrefix(p);
			if (listenerPrefix.Path.IndexOf('%') != -1)
			{
				throw new HttpListenerException(400, "Invalid path.");
			}
			if (listenerPrefix.Path.IndexOf("//", StringComparison.Ordinal) != -1)
			{
				throw new HttpListenerException(400, "Invalid path.");
			}
			EndPointManager.GetEPListener(listenerPrefix.Host, listenerPrefix.Port, listener, listenerPrefix.Secure).AddPrefix(listenerPrefix, listener);
		}

		// Token: 0x06003476 RID: 13430 RVA: 0x000B7094 File Offset: 0x000B5294
		private static EndPointListener GetEPListener(string host, int port, HttpListener listener, bool secure)
		{
			IPAddress ipaddress;
			if (host == "*")
			{
				ipaddress = IPAddress.Any;
			}
			else if (!IPAddress.TryParse(host, out ipaddress))
			{
				try
				{
					IPHostEntry hostByName = Dns.GetHostByName(host);
					if (hostByName != null)
					{
						ipaddress = hostByName.AddressList[0];
					}
					else
					{
						ipaddress = IPAddress.Any;
					}
				}
				catch
				{
					ipaddress = IPAddress.Any;
				}
			}
			Hashtable hashtable;
			if (EndPointManager.ip_to_endpoints.ContainsKey(ipaddress))
			{
				hashtable = (Hashtable)EndPointManager.ip_to_endpoints[ipaddress];
			}
			else
			{
				hashtable = new Hashtable();
				EndPointManager.ip_to_endpoints[ipaddress] = hashtable;
			}
			EndPointListener endPointListener;
			if (hashtable.ContainsKey(port))
			{
				endPointListener = (EndPointListener)hashtable[port];
			}
			else
			{
				endPointListener = new EndPointListener(listener, ipaddress, port, secure);
				hashtable[port] = endPointListener;
			}
			return endPointListener;
		}

		// Token: 0x06003477 RID: 13431 RVA: 0x000B7168 File Offset: 0x000B5368
		public static void RemoveEndPoint(EndPointListener epl, IPEndPoint ep)
		{
			Hashtable obj = EndPointManager.ip_to_endpoints;
			lock (obj)
			{
				Hashtable hashtable = (Hashtable)EndPointManager.ip_to_endpoints[ep.Address];
				hashtable.Remove(ep.Port);
				if (hashtable.Count == 0)
				{
					EndPointManager.ip_to_endpoints.Remove(ep.Address);
				}
				epl.Close();
			}
		}

		// Token: 0x06003478 RID: 13432 RVA: 0x000B71E4 File Offset: 0x000B53E4
		public static void RemoveListener(HttpListener listener)
		{
			Hashtable obj = EndPointManager.ip_to_endpoints;
			lock (obj)
			{
				foreach (string prefix in listener.Prefixes)
				{
					EndPointManager.RemovePrefixInternal(prefix, listener);
				}
			}
		}

		// Token: 0x06003479 RID: 13433 RVA: 0x000B7258 File Offset: 0x000B5458
		public static void RemovePrefix(string prefix, HttpListener listener)
		{
			Hashtable obj = EndPointManager.ip_to_endpoints;
			lock (obj)
			{
				EndPointManager.RemovePrefixInternal(prefix, listener);
			}
		}

		// Token: 0x0600347A RID: 13434 RVA: 0x000B7298 File Offset: 0x000B5498
		private static void RemovePrefixInternal(string prefix, HttpListener listener)
		{
			ListenerPrefix listenerPrefix = new ListenerPrefix(prefix);
			if (listenerPrefix.Path.IndexOf('%') != -1)
			{
				return;
			}
			if (listenerPrefix.Path.IndexOf("//", StringComparison.Ordinal) != -1)
			{
				return;
			}
			EndPointManager.GetEPListener(listenerPrefix.Host, listenerPrefix.Port, listener, listenerPrefix.Secure).RemovePrefix(listenerPrefix, listener);
		}

		// Token: 0x0600347B RID: 13435 RVA: 0x000B72F1 File Offset: 0x000B54F1
		// Note: this type is marked as 'beforefieldinit'.
		static EndPointManager()
		{
		}

		// Token: 0x04001E98 RID: 7832
		private static Hashtable ip_to_endpoints = new Hashtable();
	}
}

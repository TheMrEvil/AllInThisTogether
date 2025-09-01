using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;

namespace WebSocketSharp.Net
{
	// Token: 0x0200001E RID: 30
	internal sealed class EndPointManager
	{
		// Token: 0x06000210 RID: 528 RVA: 0x0000DF25 File Offset: 0x0000C125
		static EndPointManager()
		{
		}

		// Token: 0x06000211 RID: 529 RVA: 0x000094E4 File Offset: 0x000076E4
		private EndPointManager()
		{
		}

		// Token: 0x06000212 RID: 530 RVA: 0x0000DF34 File Offset: 0x0000C134
		private static void addPrefix(string uriPrefix, HttpListener listener)
		{
			HttpListenerPrefix httpListenerPrefix = new HttpListenerPrefix(uriPrefix, listener);
			IPAddress ipaddress = EndPointManager.convertToIPAddress(httpListenerPrefix.Host);
			bool flag = ipaddress == null;
			if (flag)
			{
				string message = "The URI prefix includes an invalid host.";
				throw new HttpListenerException(87, message);
			}
			bool flag2 = !ipaddress.IsLocal();
			if (flag2)
			{
				string message2 = "The URI prefix includes an invalid host.";
				throw new HttpListenerException(87, message2);
			}
			int num;
			bool flag3 = !int.TryParse(httpListenerPrefix.Port, out num);
			if (flag3)
			{
				string message3 = "The URI prefix includes an invalid port.";
				throw new HttpListenerException(87, message3);
			}
			bool flag4 = !num.IsPortNumber();
			if (flag4)
			{
				string message4 = "The URI prefix includes an invalid port.";
				throw new HttpListenerException(87, message4);
			}
			string path = httpListenerPrefix.Path;
			bool flag5 = path.IndexOf('%') != -1;
			if (flag5)
			{
				string message5 = "The URI prefix includes an invalid path.";
				throw new HttpListenerException(87, message5);
			}
			bool flag6 = path.IndexOf("//", StringComparison.Ordinal) != -1;
			if (flag6)
			{
				string message6 = "The URI prefix includes an invalid path.";
				throw new HttpListenerException(87, message6);
			}
			IPEndPoint ipendPoint = new IPEndPoint(ipaddress, num);
			EndPointListener endPointListener;
			bool flag7 = EndPointManager._endpoints.TryGetValue(ipendPoint, out endPointListener);
			if (flag7)
			{
				bool flag8 = endPointListener.IsSecure ^ httpListenerPrefix.IsSecure;
				if (flag8)
				{
					string message7 = "The URI prefix includes an invalid scheme.";
					throw new HttpListenerException(87, message7);
				}
			}
			else
			{
				endPointListener = new EndPointListener(ipendPoint, httpListenerPrefix.IsSecure, listener.CertificateFolderPath, listener.SslConfiguration, listener.ReuseAddress);
				EndPointManager._endpoints.Add(ipendPoint, endPointListener);
			}
			endPointListener.AddPrefix(httpListenerPrefix);
		}

		// Token: 0x06000213 RID: 531 RVA: 0x0000E0B8 File Offset: 0x0000C2B8
		private static IPAddress convertToIPAddress(string hostname)
		{
			bool flag = hostname == "*";
			IPAddress result;
			if (flag)
			{
				result = IPAddress.Any;
			}
			else
			{
				bool flag2 = hostname == "+";
				if (flag2)
				{
					result = IPAddress.Any;
				}
				else
				{
					result = hostname.ToIPAddress();
				}
			}
			return result;
		}

		// Token: 0x06000214 RID: 532 RVA: 0x0000E100 File Offset: 0x0000C300
		private static void removePrefix(string uriPrefix, HttpListener listener)
		{
			HttpListenerPrefix httpListenerPrefix = new HttpListenerPrefix(uriPrefix, listener);
			IPAddress ipaddress = EndPointManager.convertToIPAddress(httpListenerPrefix.Host);
			bool flag = ipaddress == null;
			if (!flag)
			{
				bool flag2 = !ipaddress.IsLocal();
				if (!flag2)
				{
					int num;
					bool flag3 = !int.TryParse(httpListenerPrefix.Port, out num);
					if (!flag3)
					{
						bool flag4 = !num.IsPortNumber();
						if (!flag4)
						{
							string path = httpListenerPrefix.Path;
							bool flag5 = path.IndexOf('%') != -1;
							if (!flag5)
							{
								bool flag6 = path.IndexOf("//", StringComparison.Ordinal) != -1;
								if (!flag6)
								{
									IPEndPoint key = new IPEndPoint(ipaddress, num);
									EndPointListener endPointListener;
									bool flag7 = !EndPointManager._endpoints.TryGetValue(key, out endPointListener);
									if (!flag7)
									{
										bool flag8 = endPointListener.IsSecure ^ httpListenerPrefix.IsSecure;
										if (!flag8)
										{
											endPointListener.RemovePrefix(httpListenerPrefix);
										}
									}
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x06000215 RID: 533 RVA: 0x0000E1EC File Offset: 0x0000C3EC
		internal static bool RemoveEndPoint(IPEndPoint endpoint)
		{
			object syncRoot = ((ICollection)EndPointManager._endpoints).SyncRoot;
			bool result;
			lock (syncRoot)
			{
				result = EndPointManager._endpoints.Remove(endpoint);
			}
			return result;
		}

		// Token: 0x06000216 RID: 534 RVA: 0x0000E234 File Offset: 0x0000C434
		public static void AddListener(HttpListener listener)
		{
			List<string> list = new List<string>();
			object syncRoot = ((ICollection)EndPointManager._endpoints).SyncRoot;
			lock (syncRoot)
			{
				try
				{
					foreach (string text in listener.Prefixes)
					{
						EndPointManager.addPrefix(text, listener);
						list.Add(text);
					}
				}
				catch
				{
					foreach (string uriPrefix in list)
					{
						EndPointManager.removePrefix(uriPrefix, listener);
					}
					throw;
				}
			}
		}

		// Token: 0x06000217 RID: 535 RVA: 0x0000E318 File Offset: 0x0000C518
		public static void AddPrefix(string uriPrefix, HttpListener listener)
		{
			object syncRoot = ((ICollection)EndPointManager._endpoints).SyncRoot;
			lock (syncRoot)
			{
				EndPointManager.addPrefix(uriPrefix, listener);
			}
		}

		// Token: 0x06000218 RID: 536 RVA: 0x0000E35C File Offset: 0x0000C55C
		public static void RemoveListener(HttpListener listener)
		{
			object syncRoot = ((ICollection)EndPointManager._endpoints).SyncRoot;
			lock (syncRoot)
			{
				foreach (string uriPrefix in listener.Prefixes)
				{
					EndPointManager.removePrefix(uriPrefix, listener);
				}
			}
		}

		// Token: 0x06000219 RID: 537 RVA: 0x0000E3D8 File Offset: 0x0000C5D8
		public static void RemovePrefix(string uriPrefix, HttpListener listener)
		{
			object syncRoot = ((ICollection)EndPointManager._endpoints).SyncRoot;
			lock (syncRoot)
			{
				EndPointManager.removePrefix(uriPrefix, listener);
			}
		}

		// Token: 0x040000C3 RID: 195
		private static readonly Dictionary<IPEndPoint, EndPointListener> _endpoints = new Dictionary<IPEndPoint, EndPointListener>();
	}
}

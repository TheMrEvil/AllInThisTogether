using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Threading;

namespace WebSocketSharp.Net
{
	// Token: 0x0200001D RID: 29
	internal sealed class EndPointListener
	{
		// Token: 0x060001FB RID: 507 RVA: 0x0000D44B File Offset: 0x0000B64B
		static EndPointListener()
		{
		}

		// Token: 0x060001FC RID: 508 RVA: 0x0000D45C File Offset: 0x0000B65C
		internal EndPointListener(IPEndPoint endpoint, bool secure, string certificateFolderPath, ServerSslConfiguration sslConfig, bool reuseAddress)
		{
			this._endpoint = endpoint;
			if (secure)
			{
				X509Certificate2 certificate = EndPointListener.getCertificate(endpoint.Port, certificateFolderPath, sslConfig.ServerCertificate);
				bool flag = certificate == null;
				if (flag)
				{
					string message = "No server certificate could be found.";
					throw new ArgumentException(message);
				}
				this._secure = true;
				this._sslConfig = new ServerSslConfiguration(sslConfig);
				this._sslConfig.ServerCertificate = certificate;
			}
			this._prefixes = new List<HttpListenerPrefix>();
			this._connections = new Dictionary<HttpConnection, HttpConnection>();
			this._connectionsSync = ((ICollection)this._connections).SyncRoot;
			this._socket = new Socket(endpoint.Address.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
			if (reuseAddress)
			{
				this._socket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
			}
			this._socket.Bind(endpoint);
			this._socket.Listen(500);
			this._socket.BeginAccept(new AsyncCallback(EndPointListener.onAccept), this);
		}

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x060001FD RID: 509 RVA: 0x0000D560 File Offset: 0x0000B760
		public IPAddress Address
		{
			get
			{
				return this._endpoint.Address;
			}
		}

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x060001FE RID: 510 RVA: 0x0000D580 File Offset: 0x0000B780
		public bool IsSecure
		{
			get
			{
				return this._secure;
			}
		}

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x060001FF RID: 511 RVA: 0x0000D598 File Offset: 0x0000B798
		public int Port
		{
			get
			{
				return this._endpoint.Port;
			}
		}

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x06000200 RID: 512 RVA: 0x0000D5B8 File Offset: 0x0000B7B8
		public ServerSslConfiguration SslConfiguration
		{
			get
			{
				return this._sslConfig;
			}
		}

		// Token: 0x06000201 RID: 513 RVA: 0x0000D5D0 File Offset: 0x0000B7D0
		private static void addSpecial(List<HttpListenerPrefix> prefixes, HttpListenerPrefix prefix)
		{
			string path = prefix.Path;
			foreach (HttpListenerPrefix httpListenerPrefix in prefixes)
			{
				bool flag = httpListenerPrefix.Path == path;
				if (flag)
				{
					string message = "The prefix is already in use.";
					throw new HttpListenerException(87, message);
				}
			}
			prefixes.Add(prefix);
		}

		// Token: 0x06000202 RID: 514 RVA: 0x0000D650 File Offset: 0x0000B850
		private void clearConnections()
		{
			HttpConnection[] array = null;
			object connectionsSync = this._connectionsSync;
			lock (connectionsSync)
			{
				int count = this._connections.Count;
				bool flag = count == 0;
				if (flag)
				{
					return;
				}
				array = new HttpConnection[count];
				Dictionary<HttpConnection, HttpConnection>.ValueCollection values = this._connections.Values;
				values.CopyTo(array, 0);
				this._connections.Clear();
			}
			foreach (HttpConnection httpConnection in array)
			{
				httpConnection.Close(true);
			}
		}

		// Token: 0x06000203 RID: 515 RVA: 0x0000D6F4 File Offset: 0x0000B8F4
		private static RSACryptoServiceProvider createRSAFromFile(string path)
		{
			RSACryptoServiceProvider rsacryptoServiceProvider = new RSACryptoServiceProvider();
			byte[] keyBlob = File.ReadAllBytes(path);
			rsacryptoServiceProvider.ImportCspBlob(keyBlob);
			return rsacryptoServiceProvider;
		}

		// Token: 0x06000204 RID: 516 RVA: 0x0000D71C File Offset: 0x0000B91C
		private static X509Certificate2 getCertificate(int port, string folderPath, X509Certificate2 defaultCertificate)
		{
			bool flag = folderPath == null || folderPath.Length == 0;
			if (flag)
			{
				folderPath = EndPointListener._defaultCertFolderPath;
			}
			try
			{
				string text = Path.Combine(folderPath, string.Format("{0}.cer", port));
				string path = Path.Combine(folderPath, string.Format("{0}.key", port));
				bool flag2 = File.Exists(text) && File.Exists(path);
				if (flag2)
				{
					return new X509Certificate2(text)
					{
						PrivateKey = EndPointListener.createRSAFromFile(path)
					};
				}
			}
			catch
			{
			}
			return defaultCertificate;
		}

		// Token: 0x06000205 RID: 517 RVA: 0x0000D7C8 File Offset: 0x0000B9C8
		private void leaveIfNoPrefix()
		{
			bool flag = this._prefixes.Count > 0;
			if (!flag)
			{
				List<HttpListenerPrefix> list = this._unhandled;
				bool flag2 = list != null && list.Count > 0;
				if (!flag2)
				{
					list = this._all;
					bool flag3 = list != null && list.Count > 0;
					if (!flag3)
					{
						this.Close();
					}
				}
			}
		}

		// Token: 0x06000206 RID: 518 RVA: 0x0000D82C File Offset: 0x0000BA2C
		private static void onAccept(IAsyncResult asyncResult)
		{
			EndPointListener endPointListener = (EndPointListener)asyncResult.AsyncState;
			Socket socket = null;
			try
			{
				socket = endPointListener._socket.EndAccept(asyncResult);
			}
			catch (ObjectDisposedException)
			{
				return;
			}
			catch (Exception)
			{
			}
			try
			{
				endPointListener._socket.BeginAccept(new AsyncCallback(EndPointListener.onAccept), endPointListener);
			}
			catch (Exception)
			{
				bool flag = socket != null;
				if (flag)
				{
					socket.Close();
				}
				return;
			}
			bool flag2 = socket == null;
			if (!flag2)
			{
				EndPointListener.processAccepted(socket, endPointListener);
			}
		}

		// Token: 0x06000207 RID: 519 RVA: 0x0000D8D0 File Offset: 0x0000BAD0
		private static void processAccepted(Socket socket, EndPointListener listener)
		{
			HttpConnection httpConnection = null;
			try
			{
				httpConnection = new HttpConnection(socket, listener);
			}
			catch (Exception)
			{
				socket.Close();
				return;
			}
			object connectionsSync = listener._connectionsSync;
			lock (connectionsSync)
			{
				listener._connections.Add(httpConnection, httpConnection);
			}
			httpConnection.BeginReadRequest();
		}

		// Token: 0x06000208 RID: 520 RVA: 0x0000D940 File Offset: 0x0000BB40
		private static bool removeSpecial(List<HttpListenerPrefix> prefixes, HttpListenerPrefix prefix)
		{
			string path = prefix.Path;
			int count = prefixes.Count;
			for (int i = 0; i < count; i++)
			{
				bool flag = prefixes[i].Path == path;
				if (flag)
				{
					prefixes.RemoveAt(i);
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000209 RID: 521 RVA: 0x0000D99C File Offset: 0x0000BB9C
		private static HttpListener searchHttpListenerFromSpecial(string path, List<HttpListenerPrefix> prefixes)
		{
			bool flag = prefixes == null;
			HttpListener result;
			if (flag)
			{
				result = null;
			}
			else
			{
				HttpListener httpListener = null;
				int num = -1;
				foreach (HttpListenerPrefix httpListenerPrefix in prefixes)
				{
					string path2 = httpListenerPrefix.Path;
					int length = path2.Length;
					bool flag2 = length < num;
					if (!flag2)
					{
						bool flag3 = path.StartsWith(path2, StringComparison.Ordinal);
						if (flag3)
						{
							num = length;
							httpListener = httpListenerPrefix.Listener;
						}
					}
				}
				result = httpListener;
			}
			return result;
		}

		// Token: 0x0600020A RID: 522 RVA: 0x0000DA3C File Offset: 0x0000BC3C
		internal static bool CertificateExists(int port, string folderPath)
		{
			bool flag = folderPath == null || folderPath.Length == 0;
			if (flag)
			{
				folderPath = EndPointListener._defaultCertFolderPath;
			}
			string path = Path.Combine(folderPath, string.Format("{0}.cer", port));
			string path2 = Path.Combine(folderPath, string.Format("{0}.key", port));
			return File.Exists(path) && File.Exists(path2);
		}

		// Token: 0x0600020B RID: 523 RVA: 0x0000DAA8 File Offset: 0x0000BCA8
		internal void RemoveConnection(HttpConnection connection)
		{
			object connectionsSync = this._connectionsSync;
			lock (connectionsSync)
			{
				this._connections.Remove(connection);
			}
		}

		// Token: 0x0600020C RID: 524 RVA: 0x0000DAEC File Offset: 0x0000BCEC
		internal bool TrySearchHttpListener(Uri uri, out HttpListener listener)
		{
			listener = null;
			bool flag = uri == null;
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				string host = uri.Host;
				bool flag2 = Uri.CheckHostName(host) == UriHostNameType.Dns;
				string b = uri.Port.ToString();
				string text = HttpUtility.UrlDecode(uri.AbsolutePath);
				bool flag3 = text[text.Length - 1] != '/';
				if (flag3)
				{
					text += "/";
				}
				bool flag4 = host != null && host.Length > 0;
				if (flag4)
				{
					List<HttpListenerPrefix> prefixes = this._prefixes;
					int num = -1;
					foreach (HttpListenerPrefix httpListenerPrefix in prefixes)
					{
						bool flag5 = flag2;
						if (flag5)
						{
							string host2 = httpListenerPrefix.Host;
							bool flag6 = Uri.CheckHostName(host2) == UriHostNameType.Dns;
							bool flag7 = flag6;
							if (flag7)
							{
								bool flag8 = host2 != host;
								if (flag8)
								{
									continue;
								}
							}
						}
						bool flag9 = httpListenerPrefix.Port != b;
						if (!flag9)
						{
							string path = httpListenerPrefix.Path;
							int length = path.Length;
							bool flag10 = length < num;
							if (!flag10)
							{
								bool flag11 = text.StartsWith(path, StringComparison.Ordinal);
								if (flag11)
								{
									num = length;
									listener = httpListenerPrefix.Listener;
								}
							}
						}
					}
					bool flag12 = num != -1;
					if (flag12)
					{
						return true;
					}
				}
				listener = EndPointListener.searchHttpListenerFromSpecial(text, this._unhandled);
				bool flag13 = listener != null;
				if (flag13)
				{
					result = true;
				}
				else
				{
					listener = EndPointListener.searchHttpListenerFromSpecial(text, this._all);
					result = (listener != null);
				}
			}
			return result;
		}

		// Token: 0x0600020D RID: 525 RVA: 0x0000DCA8 File Offset: 0x0000BEA8
		public void AddPrefix(HttpListenerPrefix prefix)
		{
			bool flag = prefix.Host == "*";
			if (flag)
			{
				List<HttpListenerPrefix> list;
				List<HttpListenerPrefix> list2;
				do
				{
					list = this._unhandled;
					list2 = ((list != null) ? new List<HttpListenerPrefix>(list) : new List<HttpListenerPrefix>());
					EndPointListener.addSpecial(list2, prefix);
				}
				while (Interlocked.CompareExchange<List<HttpListenerPrefix>>(ref this._unhandled, list2, list) != list);
			}
			else
			{
				bool flag2 = prefix.Host == "+";
				if (flag2)
				{
					List<HttpListenerPrefix> list;
					List<HttpListenerPrefix> list2;
					do
					{
						list = this._all;
						list2 = ((list != null) ? new List<HttpListenerPrefix>(list) : new List<HttpListenerPrefix>());
						EndPointListener.addSpecial(list2, prefix);
					}
					while (Interlocked.CompareExchange<List<HttpListenerPrefix>>(ref this._all, list2, list) != list);
				}
				else
				{
					List<HttpListenerPrefix> list;
					int num;
					for (;;)
					{
						list = this._prefixes;
						num = list.IndexOf(prefix);
						bool flag3 = num > -1;
						if (flag3)
						{
							break;
						}
						if (Interlocked.CompareExchange<List<HttpListenerPrefix>>(ref this._prefixes, new List<HttpListenerPrefix>(list)
						{
							prefix
						}, list) == list)
						{
							return;
						}
					}
					bool flag4 = list[num].Listener != prefix.Listener;
					if (flag4)
					{
						string message = string.Format("There is another listener for {0}.", prefix);
						throw new HttpListenerException(87, message);
					}
				}
			}
		}

		// Token: 0x0600020E RID: 526 RVA: 0x0000DDDB File Offset: 0x0000BFDB
		public void Close()
		{
			this._socket.Close();
			this.clearConnections();
			EndPointManager.RemoveEndPoint(this._endpoint);
		}

		// Token: 0x0600020F RID: 527 RVA: 0x0000DE00 File Offset: 0x0000C000
		public void RemovePrefix(HttpListenerPrefix prefix)
		{
			bool flag = prefix.Host == "*";
			if (flag)
			{
				List<HttpListenerPrefix> list;
				List<HttpListenerPrefix> list2;
				do
				{
					list = this._unhandled;
					bool flag2 = list == null;
					if (flag2)
					{
						break;
					}
					list2 = new List<HttpListenerPrefix>(list);
					bool flag3 = !EndPointListener.removeSpecial(list2, prefix);
					if (flag3)
					{
						break;
					}
				}
				while (Interlocked.CompareExchange<List<HttpListenerPrefix>>(ref this._unhandled, list2, list) != list);
				this.leaveIfNoPrefix();
			}
			else
			{
				bool flag4 = prefix.Host == "+";
				if (flag4)
				{
					List<HttpListenerPrefix> list;
					List<HttpListenerPrefix> list2;
					do
					{
						list = this._all;
						bool flag5 = list == null;
						if (flag5)
						{
							break;
						}
						list2 = new List<HttpListenerPrefix>(list);
						bool flag6 = !EndPointListener.removeSpecial(list2, prefix);
						if (flag6)
						{
							break;
						}
					}
					while (Interlocked.CompareExchange<List<HttpListenerPrefix>>(ref this._all, list2, list) != list);
					this.leaveIfNoPrefix();
				}
				else
				{
					List<HttpListenerPrefix> list;
					List<HttpListenerPrefix> list2;
					do
					{
						list = this._prefixes;
						bool flag7 = !list.Contains(prefix);
						if (flag7)
						{
							break;
						}
						list2 = new List<HttpListenerPrefix>(list);
						list2.Remove(prefix);
					}
					while (Interlocked.CompareExchange<List<HttpListenerPrefix>>(ref this._prefixes, list2, list) != list);
					this.leaveIfNoPrefix();
				}
			}
		}

		// Token: 0x040000B9 RID: 185
		private List<HttpListenerPrefix> _all;

		// Token: 0x040000BA RID: 186
		private Dictionary<HttpConnection, HttpConnection> _connections;

		// Token: 0x040000BB RID: 187
		private object _connectionsSync;

		// Token: 0x040000BC RID: 188
		private static readonly string _defaultCertFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

		// Token: 0x040000BD RID: 189
		private IPEndPoint _endpoint;

		// Token: 0x040000BE RID: 190
		private List<HttpListenerPrefix> _prefixes;

		// Token: 0x040000BF RID: 191
		private bool _secure;

		// Token: 0x040000C0 RID: 192
		private Socket _socket;

		// Token: 0x040000C1 RID: 193
		private ServerSslConfiguration _sslConfig;

		// Token: 0x040000C2 RID: 194
		private List<HttpListenerPrefix> _unhandled;
	}
}

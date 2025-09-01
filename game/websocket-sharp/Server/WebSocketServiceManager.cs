using System;
using System.Collections;
using System.Collections.Generic;

namespace WebSocketSharp.Server
{
	// Token: 0x0200004C RID: 76
	public class WebSocketServiceManager
	{
		// Token: 0x06000516 RID: 1302 RVA: 0x0001C874 File Offset: 0x0001AA74
		internal WebSocketServiceManager(Logger log)
		{
			this._log = log;
			this._hosts = new Dictionary<string, WebSocketServiceHost>();
			this._keepClean = true;
			this._state = ServerState.Ready;
			this._sync = ((ICollection)this._hosts).SyncRoot;
			this._waitTime = TimeSpan.FromSeconds(1.0);
		}

		// Token: 0x17000191 RID: 401
		// (get) Token: 0x06000517 RID: 1303 RVA: 0x0001C8D4 File Offset: 0x0001AAD4
		public int Count
		{
			get
			{
				object sync = this._sync;
				int count;
				lock (sync)
				{
					count = this._hosts.Count;
				}
				return count;
			}
		}

		// Token: 0x17000192 RID: 402
		// (get) Token: 0x06000518 RID: 1304 RVA: 0x0001C918 File Offset: 0x0001AB18
		public IEnumerable<WebSocketServiceHost> Hosts
		{
			get
			{
				object sync = this._sync;
				IEnumerable<WebSocketServiceHost> result;
				lock (sync)
				{
					result = this._hosts.Values.ToList<WebSocketServiceHost>();
				}
				return result;
			}
		}

		// Token: 0x17000193 RID: 403
		public WebSocketServiceHost this[string path]
		{
			get
			{
				bool flag = path == null;
				if (flag)
				{
					throw new ArgumentNullException("path");
				}
				bool flag2 = path.Length == 0;
				if (flag2)
				{
					throw new ArgumentException("An empty string.", "path");
				}
				bool flag3 = path[0] != '/';
				if (flag3)
				{
					string message = "It is not an absolute path.";
					throw new ArgumentException(message, "path");
				}
				bool flag4 = path.IndexOfAny(new char[]
				{
					'?',
					'#'
				}) > -1;
				if (flag4)
				{
					string message2 = "It includes either or both query and fragment components.";
					throw new ArgumentException(message2, "path");
				}
				WebSocketServiceHost result;
				this.InternalTryGetServiceHost(path, out result);
				return result;
			}
		}

		// Token: 0x17000194 RID: 404
		// (get) Token: 0x0600051A RID: 1306 RVA: 0x0001CA0C File Offset: 0x0001AC0C
		// (set) Token: 0x0600051B RID: 1307 RVA: 0x0001CA28 File Offset: 0x0001AC28
		public bool KeepClean
		{
			get
			{
				return this._keepClean;
			}
			set
			{
				object sync = this._sync;
				lock (sync)
				{
					bool flag = !this.canSet();
					if (!flag)
					{
						foreach (WebSocketServiceHost webSocketServiceHost in this._hosts.Values)
						{
							webSocketServiceHost.KeepClean = value;
						}
						this._keepClean = value;
					}
				}
			}
		}

		// Token: 0x17000195 RID: 405
		// (get) Token: 0x0600051C RID: 1308 RVA: 0x0001CAC4 File Offset: 0x0001ACC4
		public IEnumerable<string> Paths
		{
			get
			{
				object sync = this._sync;
				IEnumerable<string> result;
				lock (sync)
				{
					result = this._hosts.Keys.ToList<string>();
				}
				return result;
			}
		}

		// Token: 0x17000196 RID: 406
		// (get) Token: 0x0600051D RID: 1309 RVA: 0x0001CB0C File Offset: 0x0001AD0C
		// (set) Token: 0x0600051E RID: 1310 RVA: 0x0001CB24 File Offset: 0x0001AD24
		public TimeSpan WaitTime
		{
			get
			{
				return this._waitTime;
			}
			set
			{
				bool flag = value <= TimeSpan.Zero;
				if (flag)
				{
					string message = "It is zero or less.";
					throw new ArgumentOutOfRangeException("value", message);
				}
				object sync = this._sync;
				lock (sync)
				{
					bool flag2 = !this.canSet();
					if (!flag2)
					{
						foreach (WebSocketServiceHost webSocketServiceHost in this._hosts.Values)
						{
							webSocketServiceHost.WaitTime = value;
						}
						this._waitTime = value;
					}
				}
			}
		}

		// Token: 0x0600051F RID: 1311 RVA: 0x0001CBE4 File Offset: 0x0001ADE4
		private bool canSet()
		{
			return this._state == ServerState.Ready || this._state == ServerState.Stop;
		}

		// Token: 0x06000520 RID: 1312 RVA: 0x0001CC10 File Offset: 0x0001AE10
		internal bool InternalTryGetServiceHost(string path, out WebSocketServiceHost host)
		{
			path = path.TrimSlashFromEnd();
			object sync = this._sync;
			bool result;
			lock (sync)
			{
				result = this._hosts.TryGetValue(path, out host);
			}
			return result;
		}

		// Token: 0x06000521 RID: 1313 RVA: 0x0001CC60 File Offset: 0x0001AE60
		internal void Start()
		{
			object sync = this._sync;
			lock (sync)
			{
				foreach (WebSocketServiceHost webSocketServiceHost in this._hosts.Values)
				{
					webSocketServiceHost.Start();
				}
				this._state = ServerState.Start;
			}
		}

		// Token: 0x06000522 RID: 1314 RVA: 0x0001CCEC File Offset: 0x0001AEEC
		internal void Stop(ushort code, string reason)
		{
			object sync = this._sync;
			lock (sync)
			{
				this._state = ServerState.ShuttingDown;
				foreach (WebSocketServiceHost webSocketServiceHost in this._hosts.Values)
				{
					webSocketServiceHost.Stop(code, reason);
				}
				this._state = ServerState.Stop;
			}
		}

		// Token: 0x06000523 RID: 1315 RVA: 0x0001CD84 File Offset: 0x0001AF84
		public void AddService<TBehavior>(string path, Action<TBehavior> initializer) where TBehavior : WebSocketBehavior, new()
		{
			bool flag = path == null;
			if (flag)
			{
				throw new ArgumentNullException("path");
			}
			bool flag2 = path.Length == 0;
			if (flag2)
			{
				throw new ArgumentException("An empty string.", "path");
			}
			bool flag3 = path[0] != '/';
			if (flag3)
			{
				string message = "It is not an absolute path.";
				throw new ArgumentException(message, "path");
			}
			bool flag4 = path.IndexOfAny(new char[]
			{
				'?',
				'#'
			}) > -1;
			if (flag4)
			{
				string message2 = "It includes either or both query and fragment components.";
				throw new ArgumentException(message2, "path");
			}
			path = path.TrimSlashFromEnd();
			object sync = this._sync;
			lock (sync)
			{
				WebSocketServiceHost webSocketServiceHost;
				bool flag5 = this._hosts.TryGetValue(path, out webSocketServiceHost);
				if (flag5)
				{
					string message3 = "It is already in use.";
					throw new ArgumentException(message3, "path");
				}
				webSocketServiceHost = new WebSocketServiceHost<TBehavior>(path, initializer, this._log);
				bool flag6 = !this._keepClean;
				if (flag6)
				{
					webSocketServiceHost.KeepClean = false;
				}
				bool flag7 = this._waitTime != webSocketServiceHost.WaitTime;
				if (flag7)
				{
					webSocketServiceHost.WaitTime = this._waitTime;
				}
				bool flag8 = this._state == ServerState.Start;
				if (flag8)
				{
					webSocketServiceHost.Start();
				}
				this._hosts.Add(path, webSocketServiceHost);
			}
		}

		// Token: 0x06000524 RID: 1316 RVA: 0x0001CEF0 File Offset: 0x0001B0F0
		public void Clear()
		{
			List<WebSocketServiceHost> list = null;
			object sync = this._sync;
			lock (sync)
			{
				list = this._hosts.Values.ToList<WebSocketServiceHost>();
				this._hosts.Clear();
			}
			foreach (WebSocketServiceHost webSocketServiceHost in list)
			{
				bool flag = webSocketServiceHost.State == ServerState.Start;
				if (flag)
				{
					webSocketServiceHost.Stop(1001, string.Empty);
				}
			}
		}

		// Token: 0x06000525 RID: 1317 RVA: 0x0001CFA4 File Offset: 0x0001B1A4
		public bool RemoveService(string path)
		{
			bool flag = path == null;
			if (flag)
			{
				throw new ArgumentNullException("path");
			}
			bool flag2 = path.Length == 0;
			if (flag2)
			{
				throw new ArgumentException("An empty string.", "path");
			}
			bool flag3 = path[0] != '/';
			if (flag3)
			{
				string message = "It is not an absolute path.";
				throw new ArgumentException(message, "path");
			}
			bool flag4 = path.IndexOfAny(new char[]
			{
				'?',
				'#'
			}) > -1;
			if (flag4)
			{
				string message2 = "It includes either or both query and fragment components.";
				throw new ArgumentException(message2, "path");
			}
			path = path.TrimSlashFromEnd();
			object sync = this._sync;
			WebSocketServiceHost webSocketServiceHost;
			lock (sync)
			{
				bool flag5 = !this._hosts.TryGetValue(path, out webSocketServiceHost);
				if (flag5)
				{
					return false;
				}
				this._hosts.Remove(path);
			}
			bool flag6 = webSocketServiceHost.State == ServerState.Start;
			if (flag6)
			{
				webSocketServiceHost.Stop(1001, string.Empty);
			}
			return true;
		}

		// Token: 0x06000526 RID: 1318 RVA: 0x0001D0C4 File Offset: 0x0001B2C4
		public bool TryGetServiceHost(string path, out WebSocketServiceHost host)
		{
			bool flag = path == null;
			if (flag)
			{
				throw new ArgumentNullException("path");
			}
			bool flag2 = path.Length == 0;
			if (flag2)
			{
				throw new ArgumentException("An empty string.", "path");
			}
			bool flag3 = path[0] != '/';
			if (flag3)
			{
				string message = "It is not an absolute path.";
				throw new ArgumentException(message, "path");
			}
			bool flag4 = path.IndexOfAny(new char[]
			{
				'?',
				'#'
			}) > -1;
			if (flag4)
			{
				string message2 = "It includes either or both query and fragment components.";
				throw new ArgumentException(message2, "path");
			}
			return this.InternalTryGetServiceHost(path, out host);
		}

		// Token: 0x0400024E RID: 590
		private Dictionary<string, WebSocketServiceHost> _hosts;

		// Token: 0x0400024F RID: 591
		private volatile bool _keepClean;

		// Token: 0x04000250 RID: 592
		private Logger _log;

		// Token: 0x04000251 RID: 593
		private volatile ServerState _state;

		// Token: 0x04000252 RID: 594
		private object _sync;

		// Token: 0x04000253 RID: 595
		private TimeSpan _waitTime;
	}
}

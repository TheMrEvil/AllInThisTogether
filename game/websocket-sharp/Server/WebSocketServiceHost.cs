using System;
using WebSocketSharp.Net.WebSockets;

namespace WebSocketSharp.Server
{
	// Token: 0x02000047 RID: 71
	public abstract class WebSocketServiceHost
	{
		// Token: 0x060004CC RID: 1228 RVA: 0x0001B2AF File Offset: 0x000194AF
		protected WebSocketServiceHost(string path, Logger log)
		{
			this._path = path;
			this._log = log;
			this._sessions = new WebSocketSessionManager(log);
		}

		// Token: 0x17000179 RID: 377
		// (get) Token: 0x060004CD RID: 1229 RVA: 0x0001B2D4 File Offset: 0x000194D4
		internal ServerState State
		{
			get
			{
				return this._sessions.State;
			}
		}

		// Token: 0x1700017A RID: 378
		// (get) Token: 0x060004CE RID: 1230 RVA: 0x0001B2F4 File Offset: 0x000194F4
		protected Logger Log
		{
			get
			{
				return this._log;
			}
		}

		// Token: 0x1700017B RID: 379
		// (get) Token: 0x060004CF RID: 1231 RVA: 0x0001B30C File Offset: 0x0001950C
		// (set) Token: 0x060004D0 RID: 1232 RVA: 0x0001B329 File Offset: 0x00019529
		public bool KeepClean
		{
			get
			{
				return this._sessions.KeepClean;
			}
			set
			{
				this._sessions.KeepClean = value;
			}
		}

		// Token: 0x1700017C RID: 380
		// (get) Token: 0x060004D1 RID: 1233 RVA: 0x0001B33C File Offset: 0x0001953C
		public string Path
		{
			get
			{
				return this._path;
			}
		}

		// Token: 0x1700017D RID: 381
		// (get) Token: 0x060004D2 RID: 1234 RVA: 0x0001B354 File Offset: 0x00019554
		public WebSocketSessionManager Sessions
		{
			get
			{
				return this._sessions;
			}
		}

		// Token: 0x1700017E RID: 382
		// (get) Token: 0x060004D3 RID: 1235
		public abstract Type BehaviorType { get; }

		// Token: 0x1700017F RID: 383
		// (get) Token: 0x060004D4 RID: 1236 RVA: 0x0001B36C File Offset: 0x0001956C
		// (set) Token: 0x060004D5 RID: 1237 RVA: 0x0001B389 File Offset: 0x00019589
		public TimeSpan WaitTime
		{
			get
			{
				return this._sessions.WaitTime;
			}
			set
			{
				this._sessions.WaitTime = value;
			}
		}

		// Token: 0x060004D6 RID: 1238 RVA: 0x0001B399 File Offset: 0x00019599
		internal void Start()
		{
			this._sessions.Start();
		}

		// Token: 0x060004D7 RID: 1239 RVA: 0x0001B3A8 File Offset: 0x000195A8
		internal void StartSession(WebSocketContext context)
		{
			this.CreateSession().Start(context, this._sessions);
		}

		// Token: 0x060004D8 RID: 1240 RVA: 0x0001B3BE File Offset: 0x000195BE
		internal void Stop(ushort code, string reason)
		{
			this._sessions.Stop(code, reason);
		}

		// Token: 0x060004D9 RID: 1241
		protected abstract WebSocketBehavior CreateSession();

		// Token: 0x0400023A RID: 570
		private Logger _log;

		// Token: 0x0400023B RID: 571
		private string _path;

		// Token: 0x0400023C RID: 572
		private WebSocketSessionManager _sessions;
	}
}

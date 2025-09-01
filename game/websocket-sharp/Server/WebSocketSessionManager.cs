using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Timers;

namespace WebSocketSharp.Server
{
	// Token: 0x0200004A RID: 74
	public class WebSocketSessionManager
	{
		// Token: 0x060004E7 RID: 1255 RVA: 0x0001B5D4 File Offset: 0x000197D4
		static WebSocketSessionManager()
		{
		}

		// Token: 0x060004E8 RID: 1256 RVA: 0x0001B5E8 File Offset: 0x000197E8
		internal WebSocketSessionManager(Logger log)
		{
			this._log = log;
			this._forSweep = new object();
			this._keepClean = true;
			this._sessions = new Dictionary<string, IWebSocketSession>();
			this._state = ServerState.Ready;
			this._sync = ((ICollection)this._sessions).SyncRoot;
			this._waitTime = TimeSpan.FromSeconds(1.0);
			this.setSweepTimer(60000.0);
		}

		// Token: 0x17000188 RID: 392
		// (get) Token: 0x060004E9 RID: 1257 RVA: 0x0001B664 File Offset: 0x00019864
		internal ServerState State
		{
			get
			{
				return this._state;
			}
		}

		// Token: 0x17000189 RID: 393
		// (get) Token: 0x060004EA RID: 1258 RVA: 0x0001B680 File Offset: 0x00019880
		public IEnumerable<string> ActiveIDs
		{
			get
			{
				foreach (KeyValuePair<string, bool> res in this.broadping(WebSocketSessionManager._emptyPingFrameAsBytes))
				{
					bool value = res.Value;
					if (value)
					{
						yield return res.Key;
					}
					res = default(KeyValuePair<string, bool>);
				}
				Dictionary<string, bool>.Enumerator enumerator = default(Dictionary<string, bool>.Enumerator);
				yield break;
				yield break;
			}
		}

		// Token: 0x1700018A RID: 394
		// (get) Token: 0x060004EB RID: 1259 RVA: 0x0001B6A0 File Offset: 0x000198A0
		public int Count
		{
			get
			{
				object sync = this._sync;
				int count;
				lock (sync)
				{
					count = this._sessions.Count;
				}
				return count;
			}
		}

		// Token: 0x1700018B RID: 395
		// (get) Token: 0x060004EC RID: 1260 RVA: 0x0001B6E4 File Offset: 0x000198E4
		public IEnumerable<string> IDs
		{
			get
			{
				bool flag = this._state != ServerState.Start;
				IEnumerable<string> result;
				if (flag)
				{
					result = Enumerable.Empty<string>();
				}
				else
				{
					object sync = this._sync;
					lock (sync)
					{
						bool flag2 = this._state != ServerState.Start;
						if (flag2)
						{
							result = Enumerable.Empty<string>();
						}
						else
						{
							result = this._sessions.Keys.ToList<string>();
						}
					}
				}
				return result;
			}
		}

		// Token: 0x1700018C RID: 396
		// (get) Token: 0x060004ED RID: 1261 RVA: 0x0001B764 File Offset: 0x00019964
		public IEnumerable<string> InactiveIDs
		{
			get
			{
				foreach (KeyValuePair<string, bool> res in this.broadping(WebSocketSessionManager._emptyPingFrameAsBytes))
				{
					bool flag = !res.Value;
					if (flag)
					{
						yield return res.Key;
					}
					res = default(KeyValuePair<string, bool>);
				}
				Dictionary<string, bool>.Enumerator enumerator = default(Dictionary<string, bool>.Enumerator);
				yield break;
				yield break;
			}
		}

		// Token: 0x1700018D RID: 397
		public IWebSocketSession this[string id]
		{
			get
			{
				bool flag = id == null;
				if (flag)
				{
					throw new ArgumentNullException("id");
				}
				bool flag2 = id.Length == 0;
				if (flag2)
				{
					throw new ArgumentException("An empty string.", "id");
				}
				IWebSocketSession result;
				this.tryGetSession(id, out result);
				return result;
			}
		}

		// Token: 0x1700018E RID: 398
		// (get) Token: 0x060004EF RID: 1263 RVA: 0x0001B7D4 File Offset: 0x000199D4
		// (set) Token: 0x060004F0 RID: 1264 RVA: 0x0001B7F0 File Offset: 0x000199F0
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
						this._keepClean = value;
					}
				}
			}
		}

		// Token: 0x1700018F RID: 399
		// (get) Token: 0x060004F1 RID: 1265 RVA: 0x0001B840 File Offset: 0x00019A40
		public IEnumerable<IWebSocketSession> Sessions
		{
			get
			{
				bool flag = this._state != ServerState.Start;
				IEnumerable<IWebSocketSession> result;
				if (flag)
				{
					result = Enumerable.Empty<IWebSocketSession>();
				}
				else
				{
					object sync = this._sync;
					lock (sync)
					{
						bool flag2 = this._state != ServerState.Start;
						if (flag2)
						{
							result = Enumerable.Empty<IWebSocketSession>();
						}
						else
						{
							result = this._sessions.Values.ToList<IWebSocketSession>();
						}
					}
				}
				return result;
			}
		}

		// Token: 0x17000190 RID: 400
		// (get) Token: 0x060004F2 RID: 1266 RVA: 0x0001B8C0 File Offset: 0x00019AC0
		// (set) Token: 0x060004F3 RID: 1267 RVA: 0x0001B8D8 File Offset: 0x00019AD8
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
						this._waitTime = value;
					}
				}
			}
		}

		// Token: 0x060004F4 RID: 1268 RVA: 0x0001B948 File Offset: 0x00019B48
		private void broadcast(Opcode opcode, byte[] data, Action completed)
		{
			Dictionary<CompressionMethod, byte[]> dictionary = new Dictionary<CompressionMethod, byte[]>();
			try
			{
				foreach (IWebSocketSession webSocketSession in this.Sessions)
				{
					bool flag = this._state != ServerState.Start;
					if (flag)
					{
						this._log.Error("The service is shutting down.");
						break;
					}
					webSocketSession.Context.WebSocket.Send(opcode, data, dictionary);
				}
				bool flag2 = completed != null;
				if (flag2)
				{
					completed();
				}
			}
			catch (Exception ex)
			{
				this._log.Error(ex.Message);
				this._log.Debug(ex.ToString());
			}
			finally
			{
				dictionary.Clear();
			}
		}

		// Token: 0x060004F5 RID: 1269 RVA: 0x0001BA38 File Offset: 0x00019C38
		private void broadcast(Opcode opcode, Stream stream, Action completed)
		{
			Dictionary<CompressionMethod, Stream> dictionary = new Dictionary<CompressionMethod, Stream>();
			try
			{
				foreach (IWebSocketSession webSocketSession in this.Sessions)
				{
					bool flag = this._state != ServerState.Start;
					if (flag)
					{
						this._log.Error("The service is shutting down.");
						break;
					}
					webSocketSession.Context.WebSocket.Send(opcode, stream, dictionary);
				}
				bool flag2 = completed != null;
				if (flag2)
				{
					completed();
				}
			}
			catch (Exception ex)
			{
				this._log.Error(ex.Message);
				this._log.Debug(ex.ToString());
			}
			finally
			{
				foreach (Stream stream2 in dictionary.Values)
				{
					stream2.Dispose();
				}
				dictionary.Clear();
			}
		}

		// Token: 0x060004F6 RID: 1270 RVA: 0x0001BB70 File Offset: 0x00019D70
		private void broadcastAsync(Opcode opcode, byte[] data, Action completed)
		{
			ThreadPool.QueueUserWorkItem(delegate(object state)
			{
				this.broadcast(opcode, data, completed);
			});
		}

		// Token: 0x060004F7 RID: 1271 RVA: 0x0001BBB4 File Offset: 0x00019DB4
		private void broadcastAsync(Opcode opcode, Stream stream, Action completed)
		{
			ThreadPool.QueueUserWorkItem(delegate(object state)
			{
				this.broadcast(opcode, stream, completed);
			});
		}

		// Token: 0x060004F8 RID: 1272 RVA: 0x0001BBF8 File Offset: 0x00019DF8
		private Dictionary<string, bool> broadping(byte[] frameAsBytes)
		{
			Dictionary<string, bool> dictionary = new Dictionary<string, bool>();
			foreach (IWebSocketSession webSocketSession in this.Sessions)
			{
				bool flag = this._state != ServerState.Start;
				if (flag)
				{
					this._log.Error("The service is shutting down.");
					break;
				}
				bool value = webSocketSession.Context.WebSocket.Ping(frameAsBytes, this._waitTime);
				dictionary.Add(webSocketSession.ID, value);
			}
			return dictionary;
		}

		// Token: 0x060004F9 RID: 1273 RVA: 0x0001BCA0 File Offset: 0x00019EA0
		private bool canSet()
		{
			return this._state == ServerState.Ready || this._state == ServerState.Stop;
		}

		// Token: 0x060004FA RID: 1274 RVA: 0x0001BCCC File Offset: 0x00019ECC
		private static string createID()
		{
			return Guid.NewGuid().ToString("N");
		}

		// Token: 0x060004FB RID: 1275 RVA: 0x0001BCF0 File Offset: 0x00019EF0
		private void setSweepTimer(double interval)
		{
			this._sweepTimer = new System.Timers.Timer(interval);
			this._sweepTimer.Elapsed += delegate(object sender, ElapsedEventArgs e)
			{
				this.Sweep();
			};
		}

		// Token: 0x060004FC RID: 1276 RVA: 0x0001BD18 File Offset: 0x00019F18
		private void stop(PayloadData payloadData, bool send)
		{
			byte[] frameAsBytes = send ? WebSocketFrame.CreateCloseFrame(payloadData, false).ToArray() : null;
			object sync = this._sync;
			lock (sync)
			{
				this._state = ServerState.ShuttingDown;
				this._sweepTimer.Enabled = false;
				foreach (IWebSocketSession webSocketSession in this._sessions.Values.ToList<IWebSocketSession>())
				{
					webSocketSession.Context.WebSocket.Close(payloadData, frameAsBytes);
				}
				this._state = ServerState.Stop;
			}
		}

		// Token: 0x060004FD RID: 1277 RVA: 0x0001BDDC File Offset: 0x00019FDC
		private bool tryGetSession(string id, out IWebSocketSession session)
		{
			session = null;
			bool flag = this._state != ServerState.Start;
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				object sync = this._sync;
				lock (sync)
				{
					bool flag2 = this._state != ServerState.Start;
					if (flag2)
					{
						result = false;
					}
					else
					{
						result = this._sessions.TryGetValue(id, out session);
					}
				}
			}
			return result;
		}

		// Token: 0x060004FE RID: 1278 RVA: 0x0001BE54 File Offset: 0x0001A054
		internal string Add(IWebSocketSession session)
		{
			object sync = this._sync;
			string result;
			lock (sync)
			{
				bool flag = this._state != ServerState.Start;
				if (flag)
				{
					result = null;
				}
				else
				{
					string text = WebSocketSessionManager.createID();
					this._sessions.Add(text, session);
					result = text;
				}
			}
			return result;
		}

		// Token: 0x060004FF RID: 1279 RVA: 0x0001BEB8 File Offset: 0x0001A0B8
		internal bool Remove(string id)
		{
			object sync = this._sync;
			bool result;
			lock (sync)
			{
				result = this._sessions.Remove(id);
			}
			return result;
		}

		// Token: 0x06000500 RID: 1280 RVA: 0x0001BEFC File Offset: 0x0001A0FC
		internal void Start()
		{
			object sync = this._sync;
			lock (sync)
			{
				this._sweepTimer.Enabled = this._keepClean;
				this._state = ServerState.Start;
			}
		}

		// Token: 0x06000501 RID: 1281 RVA: 0x0001BF54 File Offset: 0x0001A154
		internal void Stop(ushort code, string reason)
		{
			bool flag = code == 1005;
			if (flag)
			{
				this.stop(PayloadData.Empty, true);
			}
			else
			{
				PayloadData payloadData = new PayloadData(code, reason);
				bool send = !code.IsReserved();
				this.stop(payloadData, send);
			}
		}

		// Token: 0x06000502 RID: 1282 RVA: 0x0001BF9C File Offset: 0x0001A19C
		public void Broadcast(byte[] data)
		{
			bool flag = this._state != ServerState.Start;
			if (flag)
			{
				string message = "The current state of the service is not Start.";
				throw new InvalidOperationException(message);
			}
			bool flag2 = data == null;
			if (flag2)
			{
				throw new ArgumentNullException("data");
			}
			bool flag3 = (long)data.Length <= (long)WebSocket.FragmentLength;
			if (flag3)
			{
				this.broadcast(Opcode.Binary, data, null);
			}
			else
			{
				this.broadcast(Opcode.Binary, new MemoryStream(data), null);
			}
		}

		// Token: 0x06000503 RID: 1283 RVA: 0x0001C00C File Offset: 0x0001A20C
		public void Broadcast(string data)
		{
			bool flag = this._state != ServerState.Start;
			if (flag)
			{
				string message = "The current state of the service is not Start.";
				throw new InvalidOperationException(message);
			}
			bool flag2 = data == null;
			if (flag2)
			{
				throw new ArgumentNullException("data");
			}
			byte[] array;
			bool flag3 = !data.TryGetUTF8EncodedBytes(out array);
			if (flag3)
			{
				string message2 = "It could not be UTF-8-encoded.";
				throw new ArgumentException(message2, "data");
			}
			bool flag4 = (long)array.Length <= (long)WebSocket.FragmentLength;
			if (flag4)
			{
				this.broadcast(Opcode.Text, array, null);
			}
			else
			{
				this.broadcast(Opcode.Text, new MemoryStream(array), null);
			}
		}

		// Token: 0x06000504 RID: 1284 RVA: 0x0001C0A4 File Offset: 0x0001A2A4
		public void Broadcast(Stream stream, int length)
		{
			bool flag = this._state != ServerState.Start;
			if (flag)
			{
				string message = "The current state of the service is not Start.";
				throw new InvalidOperationException(message);
			}
			bool flag2 = stream == null;
			if (flag2)
			{
				throw new ArgumentNullException("stream");
			}
			bool flag3 = !stream.CanRead;
			if (flag3)
			{
				string message2 = "It cannot be read.";
				throw new ArgumentException(message2, "stream");
			}
			bool flag4 = length < 1;
			if (flag4)
			{
				string message3 = "It is less than 1.";
				throw new ArgumentException(message3, "length");
			}
			byte[] array = stream.ReadBytes(length);
			int num = array.Length;
			bool flag5 = num == 0;
			if (flag5)
			{
				string message4 = "No data could be read from it.";
				throw new ArgumentException(message4, "stream");
			}
			bool flag6 = num < length;
			if (flag6)
			{
				string format = "Only {0} byte(s) of data could be read from the stream.";
				string message5 = string.Format(format, num);
				this._log.Warn(message5);
			}
			bool flag7 = num <= WebSocket.FragmentLength;
			if (flag7)
			{
				this.broadcast(Opcode.Binary, array, null);
			}
			else
			{
				this.broadcast(Opcode.Binary, new MemoryStream(array), null);
			}
		}

		// Token: 0x06000505 RID: 1285 RVA: 0x0001C1B4 File Offset: 0x0001A3B4
		public void BroadcastAsync(byte[] data, Action completed)
		{
			bool flag = this._state != ServerState.Start;
			if (flag)
			{
				string message = "The current state of the service is not Start.";
				throw new InvalidOperationException(message);
			}
			bool flag2 = data == null;
			if (flag2)
			{
				throw new ArgumentNullException("data");
			}
			bool flag3 = (long)data.Length <= (long)WebSocket.FragmentLength;
			if (flag3)
			{
				this.broadcastAsync(Opcode.Binary, data, completed);
			}
			else
			{
				this.broadcastAsync(Opcode.Binary, new MemoryStream(data), completed);
			}
		}

		// Token: 0x06000506 RID: 1286 RVA: 0x0001C224 File Offset: 0x0001A424
		public void BroadcastAsync(string data, Action completed)
		{
			bool flag = this._state != ServerState.Start;
			if (flag)
			{
				string message = "The current state of the service is not Start.";
				throw new InvalidOperationException(message);
			}
			bool flag2 = data == null;
			if (flag2)
			{
				throw new ArgumentNullException("data");
			}
			byte[] array;
			bool flag3 = !data.TryGetUTF8EncodedBytes(out array);
			if (flag3)
			{
				string message2 = "It could not be UTF-8-encoded.";
				throw new ArgumentException(message2, "data");
			}
			bool flag4 = (long)array.Length <= (long)WebSocket.FragmentLength;
			if (flag4)
			{
				this.broadcastAsync(Opcode.Text, array, completed);
			}
			else
			{
				this.broadcastAsync(Opcode.Text, new MemoryStream(array), completed);
			}
		}

		// Token: 0x06000507 RID: 1287 RVA: 0x0001C2BC File Offset: 0x0001A4BC
		public void BroadcastAsync(Stream stream, int length, Action completed)
		{
			bool flag = this._state != ServerState.Start;
			if (flag)
			{
				string message = "The current state of the service is not Start.";
				throw new InvalidOperationException(message);
			}
			bool flag2 = stream == null;
			if (flag2)
			{
				throw new ArgumentNullException("stream");
			}
			bool flag3 = !stream.CanRead;
			if (flag3)
			{
				string message2 = "It cannot be read.";
				throw new ArgumentException(message2, "stream");
			}
			bool flag4 = length < 1;
			if (flag4)
			{
				string message3 = "It is less than 1.";
				throw new ArgumentException(message3, "length");
			}
			byte[] array = stream.ReadBytes(length);
			int num = array.Length;
			bool flag5 = num == 0;
			if (flag5)
			{
				string message4 = "No data could be read from it.";
				throw new ArgumentException(message4, "stream");
			}
			bool flag6 = num < length;
			if (flag6)
			{
				string format = "Only {0} byte(s) of data could be read from the stream.";
				string message5 = string.Format(format, num);
				this._log.Warn(message5);
			}
			bool flag7 = num <= WebSocket.FragmentLength;
			if (flag7)
			{
				this.broadcastAsync(Opcode.Binary, array, completed);
			}
			else
			{
				this.broadcastAsync(Opcode.Binary, new MemoryStream(array), completed);
			}
		}

		// Token: 0x06000508 RID: 1288 RVA: 0x0001C3CC File Offset: 0x0001A5CC
		public void CloseSession(string id)
		{
			IWebSocketSession webSocketSession;
			bool flag = !this.TryGetSession(id, out webSocketSession);
			if (flag)
			{
				string message = "The session could not be found.";
				throw new InvalidOperationException(message);
			}
			webSocketSession.Context.WebSocket.Close();
		}

		// Token: 0x06000509 RID: 1289 RVA: 0x0001C40C File Offset: 0x0001A60C
		public void CloseSession(string id, ushort code, string reason)
		{
			IWebSocketSession webSocketSession;
			bool flag = !this.TryGetSession(id, out webSocketSession);
			if (flag)
			{
				string message = "The session could not be found.";
				throw new InvalidOperationException(message);
			}
			webSocketSession.Context.WebSocket.Close(code, reason);
		}

		// Token: 0x0600050A RID: 1290 RVA: 0x0001C44C File Offset: 0x0001A64C
		public void CloseSession(string id, CloseStatusCode code, string reason)
		{
			IWebSocketSession webSocketSession;
			bool flag = !this.TryGetSession(id, out webSocketSession);
			if (flag)
			{
				string message = "The session could not be found.";
				throw new InvalidOperationException(message);
			}
			webSocketSession.Context.WebSocket.Close(code, reason);
		}

		// Token: 0x0600050B RID: 1291 RVA: 0x0001C48C File Offset: 0x0001A68C
		public bool PingTo(string id)
		{
			IWebSocketSession webSocketSession;
			bool flag = !this.TryGetSession(id, out webSocketSession);
			if (flag)
			{
				string message = "The session could not be found.";
				throw new InvalidOperationException(message);
			}
			return webSocketSession.Context.WebSocket.Ping();
		}

		// Token: 0x0600050C RID: 1292 RVA: 0x0001C4CC File Offset: 0x0001A6CC
		public bool PingTo(string message, string id)
		{
			IWebSocketSession webSocketSession;
			bool flag = !this.TryGetSession(id, out webSocketSession);
			if (flag)
			{
				string message2 = "The session could not be found.";
				throw new InvalidOperationException(message2);
			}
			return webSocketSession.Context.WebSocket.Ping(message);
		}

		// Token: 0x0600050D RID: 1293 RVA: 0x0001C510 File Offset: 0x0001A710
		public void SendTo(byte[] data, string id)
		{
			IWebSocketSession webSocketSession;
			bool flag = !this.TryGetSession(id, out webSocketSession);
			if (flag)
			{
				string message = "The session could not be found.";
				throw new InvalidOperationException(message);
			}
			webSocketSession.Context.WebSocket.Send(data);
		}

		// Token: 0x0600050E RID: 1294 RVA: 0x0001C550 File Offset: 0x0001A750
		public void SendTo(string data, string id)
		{
			IWebSocketSession webSocketSession;
			bool flag = !this.TryGetSession(id, out webSocketSession);
			if (flag)
			{
				string message = "The session could not be found.";
				throw new InvalidOperationException(message);
			}
			webSocketSession.Context.WebSocket.Send(data);
		}

		// Token: 0x0600050F RID: 1295 RVA: 0x0001C590 File Offset: 0x0001A790
		public void SendTo(Stream stream, int length, string id)
		{
			IWebSocketSession webSocketSession;
			bool flag = !this.TryGetSession(id, out webSocketSession);
			if (flag)
			{
				string message = "The session could not be found.";
				throw new InvalidOperationException(message);
			}
			webSocketSession.Context.WebSocket.Send(stream, length);
		}

		// Token: 0x06000510 RID: 1296 RVA: 0x0001C5D0 File Offset: 0x0001A7D0
		public void SendToAsync(byte[] data, string id, Action<bool> completed)
		{
			IWebSocketSession webSocketSession;
			bool flag = !this.TryGetSession(id, out webSocketSession);
			if (flag)
			{
				string message = "The session could not be found.";
				throw new InvalidOperationException(message);
			}
			webSocketSession.Context.WebSocket.SendAsync(data, completed);
		}

		// Token: 0x06000511 RID: 1297 RVA: 0x0001C610 File Offset: 0x0001A810
		public void SendToAsync(string data, string id, Action<bool> completed)
		{
			IWebSocketSession webSocketSession;
			bool flag = !this.TryGetSession(id, out webSocketSession);
			if (flag)
			{
				string message = "The session could not be found.";
				throw new InvalidOperationException(message);
			}
			webSocketSession.Context.WebSocket.SendAsync(data, completed);
		}

		// Token: 0x06000512 RID: 1298 RVA: 0x0001C650 File Offset: 0x0001A850
		public void SendToAsync(Stream stream, int length, string id, Action<bool> completed)
		{
			IWebSocketSession webSocketSession;
			bool flag = !this.TryGetSession(id, out webSocketSession);
			if (flag)
			{
				string message = "The session could not be found.";
				throw new InvalidOperationException(message);
			}
			webSocketSession.Context.WebSocket.SendAsync(stream, length, completed);
		}

		// Token: 0x06000513 RID: 1299 RVA: 0x0001C694 File Offset: 0x0001A894
		public void Sweep()
		{
			bool sweeping = this._sweeping;
			if (sweeping)
			{
				this._log.Info("The sweeping is already in progress.");
			}
			else
			{
				object forSweep = this._forSweep;
				lock (forSweep)
				{
					bool sweeping2 = this._sweeping;
					if (sweeping2)
					{
						this._log.Info("The sweeping is already in progress.");
						return;
					}
					this._sweeping = true;
				}
				foreach (string key in this.InactiveIDs)
				{
					bool flag = this._state != ServerState.Start;
					if (flag)
					{
						break;
					}
					object sync = this._sync;
					lock (sync)
					{
						bool flag2 = this._state != ServerState.Start;
						if (flag2)
						{
							break;
						}
						IWebSocketSession webSocketSession;
						bool flag3 = !this._sessions.TryGetValue(key, out webSocketSession);
						if (!flag3)
						{
							WebSocketState connectionState = webSocketSession.ConnectionState;
							bool flag4 = connectionState == WebSocketState.Open;
							if (flag4)
							{
								webSocketSession.Context.WebSocket.Close(CloseStatusCode.Abnormal);
							}
							else
							{
								bool flag5 = connectionState == WebSocketState.Closing;
								if (!flag5)
								{
									this._sessions.Remove(key);
								}
							}
						}
					}
				}
				this._sweeping = false;
			}
		}

		// Token: 0x06000514 RID: 1300 RVA: 0x0001C820 File Offset: 0x0001AA20
		public bool TryGetSession(string id, out IWebSocketSession session)
		{
			bool flag = id == null;
			if (flag)
			{
				throw new ArgumentNullException("id");
			}
			bool flag2 = id.Length == 0;
			if (flag2)
			{
				throw new ArgumentException("An empty string.", "id");
			}
			return this.tryGetSession(id, out session);
		}

		// Token: 0x06000515 RID: 1301 RVA: 0x0001C86A File Offset: 0x0001AA6A
		[CompilerGenerated]
		private void <setSweepTimer>b__39_0(object sender, ElapsedEventArgs e)
		{
			this.Sweep();
		}

		// Token: 0x0400023F RID: 575
		private static readonly byte[] _emptyPingFrameAsBytes = WebSocketFrame.CreatePingFrame(false).ToArray();

		// Token: 0x04000240 RID: 576
		private object _forSweep;

		// Token: 0x04000241 RID: 577
		private volatile bool _keepClean;

		// Token: 0x04000242 RID: 578
		private Logger _log;

		// Token: 0x04000243 RID: 579
		private Dictionary<string, IWebSocketSession> _sessions;

		// Token: 0x04000244 RID: 580
		private volatile ServerState _state;

		// Token: 0x04000245 RID: 581
		private volatile bool _sweeping;

		// Token: 0x04000246 RID: 582
		private System.Timers.Timer _sweepTimer;

		// Token: 0x04000247 RID: 583
		private object _sync;

		// Token: 0x04000248 RID: 584
		private TimeSpan _waitTime;

		// Token: 0x02000075 RID: 117
		[CompilerGenerated]
		private sealed class <get_ActiveIDs>d__15 : IEnumerable<string>, IEnumerable, IEnumerator<string>, IDisposable, IEnumerator
		{
			// Token: 0x060005D9 RID: 1497 RVA: 0x0001F440 File Offset: 0x0001D640
			[DebuggerHidden]
			public <get_ActiveIDs>d__15(int <>1__state)
			{
				this.<>1__state = <>1__state;
				this.<>l__initialThreadId = Thread.CurrentThread.ManagedThreadId;
			}

			// Token: 0x060005DA RID: 1498 RVA: 0x0001F460 File Offset: 0x0001D660
			[DebuggerHidden]
			void IDisposable.Dispose()
			{
				int num = this.<>1__state;
				if (num == -3 || num == 1)
				{
					try
					{
					}
					finally
					{
						this.<>m__Finally1();
					}
				}
			}

			// Token: 0x060005DB RID: 1499 RVA: 0x0001F4A0 File Offset: 0x0001D6A0
			bool IEnumerator.MoveNext()
			{
				bool result;
				try
				{
					int num = this.<>1__state;
					if (num == 0)
					{
						this.<>1__state = -1;
						enumerator = base.broadping(WebSocketSessionManager._emptyPingFrameAsBytes).GetEnumerator();
						this.<>1__state = -3;
						goto IL_9D;
					}
					if (num != 1)
					{
						return false;
					}
					this.<>1__state = -3;
					IL_90:
					res = default(KeyValuePair<string, bool>);
					IL_9D:
					if (!enumerator.MoveNext())
					{
						this.<>m__Finally1();
						enumerator = default(Dictionary<string, bool>.Enumerator);
						result = false;
					}
					else
					{
						res = enumerator.Current;
						bool value = res.Value;
						if (!value)
						{
							goto IL_90;
						}
						this.<>2__current = res.Key;
						this.<>1__state = 1;
						result = true;
					}
				}
				catch
				{
					this.System.IDisposable.Dispose();
					throw;
				}
				return result;
			}

			// Token: 0x060005DC RID: 1500 RVA: 0x0001F588 File Offset: 0x0001D788
			private void <>m__Finally1()
			{
				this.<>1__state = -1;
				((IDisposable)enumerator).Dispose();
			}

			// Token: 0x170001B4 RID: 436
			// (get) Token: 0x060005DD RID: 1501 RVA: 0x0001F5A3 File Offset: 0x0001D7A3
			string IEnumerator<string>.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x060005DE RID: 1502 RVA: 0x0001E136 File Offset: 0x0001C336
			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x170001B5 RID: 437
			// (get) Token: 0x060005DF RID: 1503 RVA: 0x0001F5A3 File Offset: 0x0001D7A3
			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x060005E0 RID: 1504 RVA: 0x0001F5AC File Offset: 0x0001D7AC
			[DebuggerHidden]
			IEnumerator<string> IEnumerable<string>.GetEnumerator()
			{
				WebSocketSessionManager.<get_ActiveIDs>d__15 <get_ActiveIDs>d__;
				if (this.<>1__state == -2 && this.<>l__initialThreadId == Thread.CurrentThread.ManagedThreadId)
				{
					this.<>1__state = 0;
					<get_ActiveIDs>d__ = this;
				}
				else
				{
					<get_ActiveIDs>d__ = new WebSocketSessionManager.<get_ActiveIDs>d__15(0);
					<get_ActiveIDs>d__.<>4__this = this;
				}
				return <get_ActiveIDs>d__;
			}

			// Token: 0x060005E1 RID: 1505 RVA: 0x0001F5F4 File Offset: 0x0001D7F4
			[DebuggerHidden]
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.System.Collections.Generic.IEnumerable<System.String>.GetEnumerator();
			}

			// Token: 0x040002FC RID: 764
			private int <>1__state;

			// Token: 0x040002FD RID: 765
			private string <>2__current;

			// Token: 0x040002FE RID: 766
			private int <>l__initialThreadId;

			// Token: 0x040002FF RID: 767
			public WebSocketSessionManager <>4__this;

			// Token: 0x04000300 RID: 768
			private Dictionary<string, bool>.Enumerator <>s__1;

			// Token: 0x04000301 RID: 769
			private KeyValuePair<string, bool> <res>5__2;
		}

		// Token: 0x02000076 RID: 118
		[CompilerGenerated]
		private sealed class <get_InactiveIDs>d__21 : IEnumerable<string>, IEnumerable, IEnumerator<string>, IDisposable, IEnumerator
		{
			// Token: 0x060005E2 RID: 1506 RVA: 0x0001F5FC File Offset: 0x0001D7FC
			[DebuggerHidden]
			public <get_InactiveIDs>d__21(int <>1__state)
			{
				this.<>1__state = <>1__state;
				this.<>l__initialThreadId = Thread.CurrentThread.ManagedThreadId;
			}

			// Token: 0x060005E3 RID: 1507 RVA: 0x0001F61C File Offset: 0x0001D81C
			[DebuggerHidden]
			void IDisposable.Dispose()
			{
				int num = this.<>1__state;
				if (num == -3 || num == 1)
				{
					try
					{
					}
					finally
					{
						this.<>m__Finally1();
					}
				}
			}

			// Token: 0x060005E4 RID: 1508 RVA: 0x0001F65C File Offset: 0x0001D85C
			bool IEnumerator.MoveNext()
			{
				bool result;
				try
				{
					int num = this.<>1__state;
					if (num == 0)
					{
						this.<>1__state = -1;
						enumerator = base.broadping(WebSocketSessionManager._emptyPingFrameAsBytes).GetEnumerator();
						this.<>1__state = -3;
						goto IL_A0;
					}
					if (num != 1)
					{
						return false;
					}
					this.<>1__state = -3;
					IL_93:
					res = default(KeyValuePair<string, bool>);
					IL_A0:
					if (!enumerator.MoveNext())
					{
						this.<>m__Finally1();
						enumerator = default(Dictionary<string, bool>.Enumerator);
						result = false;
					}
					else
					{
						res = enumerator.Current;
						bool flag = !res.Value;
						if (!flag)
						{
							goto IL_93;
						}
						this.<>2__current = res.Key;
						this.<>1__state = 1;
						result = true;
					}
				}
				catch
				{
					this.System.IDisposable.Dispose();
					throw;
				}
				return result;
			}

			// Token: 0x060005E5 RID: 1509 RVA: 0x0001F748 File Offset: 0x0001D948
			private void <>m__Finally1()
			{
				this.<>1__state = -1;
				((IDisposable)enumerator).Dispose();
			}

			// Token: 0x170001B6 RID: 438
			// (get) Token: 0x060005E6 RID: 1510 RVA: 0x0001F763 File Offset: 0x0001D963
			string IEnumerator<string>.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x060005E7 RID: 1511 RVA: 0x0001E136 File Offset: 0x0001C336
			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x170001B7 RID: 439
			// (get) Token: 0x060005E8 RID: 1512 RVA: 0x0001F763 File Offset: 0x0001D963
			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x060005E9 RID: 1513 RVA: 0x0001F76C File Offset: 0x0001D96C
			[DebuggerHidden]
			IEnumerator<string> IEnumerable<string>.GetEnumerator()
			{
				WebSocketSessionManager.<get_InactiveIDs>d__21 <get_InactiveIDs>d__;
				if (this.<>1__state == -2 && this.<>l__initialThreadId == Thread.CurrentThread.ManagedThreadId)
				{
					this.<>1__state = 0;
					<get_InactiveIDs>d__ = this;
				}
				else
				{
					<get_InactiveIDs>d__ = new WebSocketSessionManager.<get_InactiveIDs>d__21(0);
					<get_InactiveIDs>d__.<>4__this = this;
				}
				return <get_InactiveIDs>d__;
			}

			// Token: 0x060005EA RID: 1514 RVA: 0x0001F7B4 File Offset: 0x0001D9B4
			[DebuggerHidden]
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.System.Collections.Generic.IEnumerable<System.String>.GetEnumerator();
			}

			// Token: 0x04000302 RID: 770
			private int <>1__state;

			// Token: 0x04000303 RID: 771
			private string <>2__current;

			// Token: 0x04000304 RID: 772
			private int <>l__initialThreadId;

			// Token: 0x04000305 RID: 773
			public WebSocketSessionManager <>4__this;

			// Token: 0x04000306 RID: 774
			private Dictionary<string, bool>.Enumerator <>s__1;

			// Token: 0x04000307 RID: 775
			private KeyValuePair<string, bool> <res>5__2;
		}

		// Token: 0x02000077 RID: 119
		[CompilerGenerated]
		private sealed class <>c__DisplayClass34_0
		{
			// Token: 0x060005EB RID: 1515 RVA: 0x0001DA3E File Offset: 0x0001BC3E
			public <>c__DisplayClass34_0()
			{
			}

			// Token: 0x060005EC RID: 1516 RVA: 0x0001F7BC File Offset: 0x0001D9BC
			internal void <broadcastAsync>b__0(object state)
			{
				this.<>4__this.broadcast(this.opcode, this.data, this.completed);
			}

			// Token: 0x04000308 RID: 776
			public WebSocketSessionManager <>4__this;

			// Token: 0x04000309 RID: 777
			public Opcode opcode;

			// Token: 0x0400030A RID: 778
			public byte[] data;

			// Token: 0x0400030B RID: 779
			public Action completed;
		}

		// Token: 0x02000078 RID: 120
		[CompilerGenerated]
		private sealed class <>c__DisplayClass35_0
		{
			// Token: 0x060005ED RID: 1517 RVA: 0x0001DA3E File Offset: 0x0001BC3E
			public <>c__DisplayClass35_0()
			{
			}

			// Token: 0x060005EE RID: 1518 RVA: 0x0001F7DC File Offset: 0x0001D9DC
			internal void <broadcastAsync>b__0(object state)
			{
				this.<>4__this.broadcast(this.opcode, this.stream, this.completed);
			}

			// Token: 0x0400030C RID: 780
			public WebSocketSessionManager <>4__this;

			// Token: 0x0400030D RID: 781
			public Opcode opcode;

			// Token: 0x0400030E RID: 782
			public Stream stream;

			// Token: 0x0400030F RID: 783
			public Action completed;
		}
	}
}

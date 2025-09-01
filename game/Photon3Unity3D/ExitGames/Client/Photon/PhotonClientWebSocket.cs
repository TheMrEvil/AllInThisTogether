using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;

namespace ExitGames.Client.Photon
{
	// Token: 0x0200001D RID: 29
	public class PhotonClientWebSocket : IPhotonSocket
	{
		// Token: 0x06000139 RID: 313 RVA: 0x0000AC3C File Offset: 0x00008E3C
		[Preserve]
		public PhotonClientWebSocket(PeerBase peerBase) : base(peerBase)
		{
			bool flag = base.ReportDebugOfLevel(DebugLevel.INFO);
			if (flag)
			{
				base.EnqueueDebugReturn(DebugLevel.INFO, "PhotonClientWebSocket");
			}
		}

		// Token: 0x0600013A RID: 314 RVA: 0x0000AC6C File Offset: 0x00008E6C
		public override bool Connect()
		{
			bool flag = base.Connect();
			bool flag2 = !flag;
			bool result;
			if (flag2)
			{
				result = false;
			}
			else
			{
				base.State = PhotonSocketState.Connecting;
				new Thread(new ThreadStart(this.AsyncConnectAndReceive))
				{
					IsBackground = true
				}.Start();
				result = true;
			}
			return result;
		}

		// Token: 0x0600013B RID: 315 RVA: 0x0000ACBC File Offset: 0x00008EBC
		private void AsyncConnectAndReceive()
		{
			Uri uri = null;
			try
			{
				uri = new Uri(this.ConnectAddress);
			}
			catch (Exception ex)
			{
				bool flag = base.ReportDebugOfLevel(DebugLevel.ERROR);
				if (flag)
				{
					IPhotonPeerListener listener = base.Listener;
					DebugLevel level = DebugLevel.ERROR;
					string str = "Failed to create a URI from ConnectAddress (";
					string connectAddress = this.ConnectAddress;
					string str2 = "). Exception: ";
					Exception ex2 = ex;
					listener.DebugReturn(level, str + connectAddress + str2 + ((ex2 != null) ? ex2.ToString() : null));
				}
			}
			bool flag2 = uri != null && uri.HostNameType == UriHostNameType.Dns;
			if (flag2)
			{
				try
				{
					IPAddress[] hostAddresses = Dns.GetHostAddresses(uri.Host);
					foreach (IPAddress ipaddress in hostAddresses)
					{
						bool flag3 = ipaddress.AddressFamily == AddressFamily.InterNetworkV6;
						if (flag3)
						{
							base.AddressResolvedAsIpv6 = true;
							this.ConnectAddress += "&IPv6";
							break;
						}
					}
				}
				catch (Exception ex3)
				{
					IPhotonPeerListener listener2 = base.Listener;
					DebugLevel level2 = DebugLevel.INFO;
					string str3 = "Dns.GetHostAddresses(";
					string host = uri.Host;
					string str4 = ") failed: ";
					Exception ex4 = ex3;
					listener2.DebugReturn(level2, str3 + host + str4 + ((ex4 != null) ? ex4.ToString() : null));
				}
			}
			this.clientWebSocket = new ClientWebSocket();
			this.clientWebSocket.Options.AddSubProtocol(base.SerializationProtocol);
			CancellationTokenSource cancellationTokenSource = new CancellationTokenSource(7000);
			Task task = this.clientWebSocket.ConnectAsync(new Uri(this.ConnectAddress), cancellationTokenSource.Token);
			try
			{
				task.Wait();
			}
			catch (Exception arg)
			{
				base.EnqueueDebugReturn(DebugLevel.ERROR, string.Format("AsyncConnectAndReceive() caught exception on {0}: {1}", this.ConnectAddress, arg));
			}
			bool isFaulted = task.IsFaulted;
			if (isFaulted)
			{
				DebugLevel debugLevel = DebugLevel.ERROR;
				string str5 = "ClientWebSocket IsFaulted: ";
				AggregateException exception = task.Exception;
				base.EnqueueDebugReturn(debugLevel, str5 + ((exception != null) ? exception.ToString() : null));
			}
			bool flag4 = this.clientWebSocket.State != WebSocketState.Open;
			if (flag4)
			{
				base.SocketErrorCode = (int)((this.clientWebSocket.CloseStatus != null) ? this.clientWebSocket.CloseStatus.Value : ((WebSocketCloseStatus)0));
				base.EnqueueDebugReturn(DebugLevel.ERROR, string.Concat(new string[]
				{
					"ClientWebSocket is not open. State: ",
					this.clientWebSocket.State.ToString(),
					" CloseStatus: ",
					this.clientWebSocket.CloseStatus.ToString(),
					" Description: ",
					this.clientWebSocket.CloseStatusDescription
				}));
				base.HandleException(StatusCode.ExceptionOnConnect);
			}
			else
			{
				base.State = PhotonSocketState.Connected;
				this.peerBase.OnConnect();
				MemoryStream memoryStream = new MemoryStream(base.MTU);
				bool flag5 = false;
				ArraySegment<byte> buffer = new ArraySegment<byte>(new byte[base.MTU]);
				while (this.clientWebSocket.State == WebSocketState.Open)
				{
					Task<WebSocketReceiveResult> task2 = null;
					try
					{
						task2 = this.clientWebSocket.ReceiveAsync(buffer, CancellationToken.None);
						while (!task2.IsCompleted)
						{
							task2.Wait(50);
						}
					}
					catch (Exception ex5)
					{
					}
					bool flag6 = task2.IsCompleted && this.clientWebSocket.State == WebSocketState.Open;
					if (flag6)
					{
						bool isCanceled = task2.IsCanceled;
						if (isCanceled)
						{
							base.EnqueueDebugReturn(DebugLevel.ERROR, string.Concat(new string[]
							{
								"PhotonClientWebSocket readTask.IsCanceled: ",
								task2.Status.ToString(),
								" ",
								base.ServerAddress,
								":",
								base.ServerPort.ToString(),
								" ",
								this.clientWebSocket.CloseStatusDescription
							}));
						}
						else
						{
							bool flag7 = task2.Result.Count == 0;
							if (flag7)
							{
								base.EnqueueDebugReturn(DebugLevel.INFO, string.Concat(new string[]
								{
									"PhotonClientWebSocket received 0 bytes. this.State: ",
									base.State.ToString(),
									" clientWebSocket.State: ",
									this.clientWebSocket.State.ToString(),
									" readTask.Status: ",
									task2.Status.ToString()
								}));
							}
							else
							{
								bool flag8 = !task2.Result.EndOfMessage;
								if (flag8)
								{
									flag5 = true;
									memoryStream.Write(buffer.Array, 0, task2.Result.Count);
								}
								else
								{
									bool flag9 = flag5;
									int num;
									byte[] array2;
									bool flag10;
									if (flag9)
									{
										memoryStream.Write(buffer.Array, 0, task2.Result.Count);
										num = (int)memoryStream.Length;
										array2 = memoryStream.GetBuffer();
									}
									else
									{
										num = task2.Result.Count;
										array2 = buffer.Array;
										flag10 = (array2[5] == 0);
									}
									flag10 = (array2[5] == 0);
									base.HandleReceivedDatagram(array2, num, true);
									bool flag11 = flag5;
									if (flag11)
									{
										memoryStream.SetLength(0L);
										memoryStream.Position = 0L;
										flag5 = false;
									}
									bool trafficStatsEnabled = this.peerBase.TrafficStatsEnabled;
									if (trafficStatsEnabled)
									{
										bool flag12 = flag10;
										if (flag12)
										{
											this.peerBase.TrafficStatsIncoming.CountReliableOpCommand(num);
										}
										else
										{
											this.peerBase.TrafficStatsIncoming.CountUnreliableOpCommand(num);
										}
									}
								}
							}
						}
					}
				}
				bool flag13 = base.State != PhotonSocketState.Disconnecting && base.State > PhotonSocketState.Disconnected;
				if (flag13)
				{
					base.EnqueueDebugReturn(DebugLevel.INFO, "PhotonSocket.State is " + base.State.ToString() + " but can't receive anymore. ClientWebSocket.State: " + this.clientWebSocket.State.ToString());
					bool flag14 = this.clientWebSocket.State == WebSocketState.CloseReceived;
					if (flag14)
					{
						base.HandleException(StatusCode.DisconnectByServerLogic);
					}
					bool flag15 = this.clientWebSocket.State == WebSocketState.Aborted;
					if (flag15)
					{
						base.HandleException(StatusCode.DisconnectByServerReasonUnknown);
					}
				}
				this.Disconnect();
			}
		}

		// Token: 0x0600013C RID: 316 RVA: 0x0000B31C File Offset: 0x0000951C
		public override bool Disconnect()
		{
			bool flag = this.clientWebSocket != null && this.clientWebSocket.State == WebSocketState.CloseReceived;
			bool result;
			if (flag)
			{
				try
				{
					this.clientWebSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "CloseAsync due to state CloseReceived", CancellationToken.None);
				}
				catch (Exception ex)
				{
					bool flag2 = base.ReportDebugOfLevel(DebugLevel.ALL);
					if (flag2)
					{
						DebugLevel debugLevel = DebugLevel.ALL;
						string str = "Caught exception in clientWebSocket.CloseAsync(): ";
						Exception ex2 = ex;
						base.EnqueueDebugReturn(debugLevel, str + ((ex2 != null) ? ex2.ToString() : null));
					}
				}
				base.State = PhotonSocketState.Disconnected;
				result = true;
			}
			else
			{
				bool flag3 = this.clientWebSocket != null && this.clientWebSocket.State != WebSocketState.Closed && this.clientWebSocket.State != WebSocketState.CloseSent;
				if (flag3)
				{
					base.State = PhotonSocketState.Disconnecting;
					try
					{
						this.clientWebSocket.CloseOutputAsync(WebSocketCloseStatus.NormalClosure, "ws close", CancellationToken.None);
					}
					catch (Exception ex3)
					{
						bool flag4 = base.ReportDebugOfLevel(DebugLevel.ALL);
						if (flag4)
						{
							DebugLevel debugLevel2 = DebugLevel.ALL;
							string str2 = "Caught exception in clientWebSocket.CloseOutputAsync(): ";
							Exception ex4 = ex3;
							base.EnqueueDebugReturn(debugLevel2, str2 + ((ex4 != null) ? ex4.ToString() : null));
						}
					}
				}
				base.State = PhotonSocketState.Disconnected;
				result = true;
			}
			return result;
		}

		// Token: 0x0600013D RID: 317 RVA: 0x0000B460 File Offset: 0x00009660
		public override PhotonSocketError Send(byte[] data, int length)
		{
			bool flag = this.clientWebSocket != null && this.clientWebSocket.State != WebSocketState.Open && base.State != PhotonSocketState.Disconnecting && base.State > PhotonSocketState.Disconnected;
			if (flag)
			{
				bool flag2 = this.clientWebSocket.State == WebSocketState.CloseReceived;
				if (flag2)
				{
					base.HandleException(StatusCode.DisconnectByServerLogic);
					return PhotonSocketError.Exception;
				}
				bool flag3 = this.clientWebSocket.State == WebSocketState.Aborted;
				if (flag3)
				{
					base.HandleException(StatusCode.DisconnectByServerReasonUnknown);
					return PhotonSocketError.Exception;
				}
			}
			bool flag4 = this.clientWebSocket == null;
			PhotonSocketError result;
			if (flag4)
			{
				bool flag5 = base.State == PhotonSocketState.Disconnecting || base.State == PhotonSocketState.Disconnected;
				if (flag5)
				{
					result = PhotonSocketError.Skipped;
				}
				else
				{
					bool flag6 = base.ReportDebugOfLevel(DebugLevel.ERROR);
					if (flag6)
					{
						base.EnqueueDebugReturn(DebugLevel.ERROR, string.Format("PhotonClientWebSocket.Send() failed: this.clientWebSocket is null. this.State: {0}", base.State));
					}
					result = PhotonSocketError.Exception;
				}
			}
			else
			{
				bool flag7 = this.sendTask != null && !this.sendTask.IsCompleted;
				if (flag7)
				{
					bool flag8 = !this.sendTask.Wait(5);
					if (flag8)
					{
						return PhotonSocketError.Busy;
					}
				}
				this.sendTask = this.clientWebSocket.SendAsync(new ArraySegment<byte>(data, 0, length), WebSocketMessageType.Binary, true, CancellationToken.None);
				bool flag9 = this.sendTask != null && !this.sendTask.IsCompleted;
				if (flag9)
				{
					bool flag10 = !this.sendTask.Wait(5);
					if (flag10)
					{
						return PhotonSocketError.PendingSend;
					}
				}
				this.sendTask = null;
				result = PhotonSocketError.Success;
			}
			return result;
		}

		// Token: 0x0600013E RID: 318 RVA: 0x0000B5F6 File Offset: 0x000097F6
		public override PhotonSocketError Receive(out byte[] data)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0400010C RID: 268
		private ClientWebSocket clientWebSocket;

		// Token: 0x0400010D RID: 269
		private Task sendTask;
	}
}

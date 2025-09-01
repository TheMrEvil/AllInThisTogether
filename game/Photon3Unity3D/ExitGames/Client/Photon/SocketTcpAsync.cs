using System;
using System.Net;
using System.Net.Sockets;
using System.Security;
using System.Threading;

namespace ExitGames.Client.Photon
{
	// Token: 0x02000033 RID: 51
	public class SocketTcpAsync : IPhotonSocket, IDisposable
	{
		// Token: 0x0600029B RID: 667 RVA: 0x000147E0 File Offset: 0x000129E0
		[Preserve]
		public SocketTcpAsync(PeerBase npeer) : base(npeer)
		{
			bool flag = base.ReportDebugOfLevel(DebugLevel.INFO);
			if (flag)
			{
				base.Listener.DebugReturn(DebugLevel.INFO, "SocketTcpAsync, .Net, Unity.");
			}
			this.PollReceive = false;
		}

		// Token: 0x0600029C RID: 668 RVA: 0x00014828 File Offset: 0x00012A28
		~SocketTcpAsync()
		{
			this.Dispose();
		}

		// Token: 0x0600029D RID: 669 RVA: 0x00014858 File Offset: 0x00012A58
		public void Dispose()
		{
			base.State = PhotonSocketState.Disconnecting;
			bool flag = this.sock != null;
			if (flag)
			{
				try
				{
					bool connected = this.sock.Connected;
					if (connected)
					{
						this.sock.Close();
					}
				}
				catch (Exception ex)
				{
					DebugLevel debugLevel = DebugLevel.INFO;
					string str = "Exception in Dispose(): ";
					Exception ex2 = ex;
					base.EnqueueDebugReturn(debugLevel, str + ((ex2 != null) ? ex2.ToString() : null));
				}
			}
			this.sock = null;
			base.State = PhotonSocketState.Disconnected;
		}

		// Token: 0x0600029E RID: 670 RVA: 0x000148E4 File Offset: 0x00012AE4
		public override bool Connect()
		{
			object obj = this.syncer;
			lock (obj)
			{
				bool flag2 = base.Connect();
				bool flag3 = !flag2;
				if (flag3)
				{
					return false;
				}
				base.State = PhotonSocketState.Connecting;
			}
			new Thread(new ThreadStart(this.DnsAndConnect))
			{
				IsBackground = true
			}.Start();
			return true;
		}

		// Token: 0x0600029F RID: 671 RVA: 0x0001496C File Offset: 0x00012B6C
		public override bool Disconnect()
		{
			bool flag = base.ReportDebugOfLevel(DebugLevel.INFO);
			if (flag)
			{
				base.EnqueueDebugReturn(DebugLevel.INFO, "SocketTcpAsync.Disconnect()");
			}
			object obj = this.syncer;
			lock (obj)
			{
				base.State = PhotonSocketState.Disconnecting;
				bool flag3 = this.sock != null;
				if (flag3)
				{
					try
					{
						this.sock.Close();
					}
					catch (Exception ex)
					{
						bool flag4 = base.ReportDebugOfLevel(DebugLevel.INFO);
						if (flag4)
						{
							DebugLevel debugLevel = DebugLevel.INFO;
							string str = "Exception in Disconnect(): ";
							Exception ex2 = ex;
							base.EnqueueDebugReturn(debugLevel, str + ((ex2 != null) ? ex2.ToString() : null));
						}
					}
				}
				base.State = PhotonSocketState.Disconnected;
			}
			return true;
		}

		// Token: 0x060002A0 RID: 672 RVA: 0x00014A3C File Offset: 0x00012C3C
		public override PhotonSocketError Send(byte[] data, int length)
		{
			try
			{
				bool flag = this.sock == null || !this.sock.Connected;
				if (flag)
				{
					return PhotonSocketError.Skipped;
				}
				this.sock.Send(data, 0, length, SocketFlags.None);
			}
			catch (Exception ex)
			{
				bool flag2 = base.State != PhotonSocketState.Disconnecting && base.State > PhotonSocketState.Disconnected;
				if (flag2)
				{
					bool flag3 = base.ReportDebugOfLevel(DebugLevel.INFO);
					if (flag3)
					{
						string text = "";
						bool flag4 = this.sock != null;
						if (flag4)
						{
							text = string.Format(" Local: {0} Remote: {1} ({2}, {3})", new object[]
							{
								this.sock.LocalEndPoint,
								this.sock.RemoteEndPoint,
								this.sock.Connected ? "connected" : "not connected",
								this.sock.IsBound ? "bound" : "not bound"
							});
						}
						base.EnqueueDebugReturn(DebugLevel.INFO, string.Format("Cannot send to: {0} ({4}). Uptime: {1} ms. {2} {3}", new object[]
						{
							base.ServerAddress,
							this.peerBase.timeInt,
							base.AddressResolvedAsIpv6 ? " IPv6" : string.Empty,
							text,
							ex
						}));
					}
					base.HandleException(StatusCode.SendError);
				}
				return PhotonSocketError.Exception;
			}
			return PhotonSocketError.Success;
		}

		// Token: 0x060002A1 RID: 673 RVA: 0x00014BBC File Offset: 0x00012DBC
		public override PhotonSocketError Receive(out byte[] data)
		{
			data = null;
			return PhotonSocketError.NoData;
		}

		// Token: 0x060002A2 RID: 674 RVA: 0x00014BD4 File Offset: 0x00012DD4
		internal void DnsAndConnect()
		{
			IPAddress[] ipAddresses = base.GetIpAddresses(base.ServerAddress);
			bool flag = ipAddresses == null;
			if (!flag)
			{
				string text = string.Empty;
				foreach (IPAddress ipaddress in ipAddresses)
				{
					try
					{
						this.sock = new Socket(ipaddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
						this.sock.NoDelay = true;
						this.sock.ReceiveTimeout = this.peerBase.DisconnectTimeout;
						this.sock.SendTimeout = this.peerBase.DisconnectTimeout;
						this.sock.Connect(ipaddress, base.ServerPort);
						bool flag2 = this.sock != null && this.sock.Connected;
						if (flag2)
						{
							break;
						}
					}
					catch (SecurityException ex)
					{
						bool flag3 = base.ReportDebugOfLevel(DebugLevel.ERROR);
						if (flag3)
						{
							string str = text;
							SecurityException ex2 = ex;
							text = str + ((ex2 != null) ? ex2.ToString() : null) + " ";
							DebugLevel debugLevel = DebugLevel.WARNING;
							string str2 = "SecurityException catched: ";
							SecurityException ex3 = ex;
							base.EnqueueDebugReturn(debugLevel, str2 + ((ex3 != null) ? ex3.ToString() : null));
						}
					}
					catch (SocketException ex4)
					{
						bool flag4 = base.ReportDebugOfLevel(DebugLevel.WARNING);
						if (flag4)
						{
							string[] array2 = new string[5];
							array2[0] = text;
							int num = 1;
							SocketException ex5 = ex4;
							array2[num] = ((ex5 != null) ? ex5.ToString() : null);
							array2[2] = " ";
							array2[3] = ex4.ErrorCode.ToString();
							array2[4] = "; ";
							text = string.Concat(array2);
							DebugLevel debugLevel2 = DebugLevel.WARNING;
							string str3 = "SocketException catched: ";
							SocketException ex6 = ex4;
							base.EnqueueDebugReturn(debugLevel2, str3 + ((ex6 != null) ? ex6.ToString() : null) + " ErrorCode: " + ex4.ErrorCode.ToString());
						}
					}
					catch (Exception ex7)
					{
						bool flag5 = base.ReportDebugOfLevel(DebugLevel.WARNING);
						if (flag5)
						{
							string str4 = text;
							Exception ex8 = ex7;
							text = str4 + ((ex8 != null) ? ex8.ToString() : null) + "; ";
							DebugLevel debugLevel3 = DebugLevel.WARNING;
							string str5 = "Exception catched: ";
							Exception ex9 = ex7;
							base.EnqueueDebugReturn(debugLevel3, str5 + ((ex9 != null) ? ex9.ToString() : null));
						}
					}
				}
				bool flag6 = this.sock == null || !this.sock.Connected;
				if (flag6)
				{
					bool flag7 = base.ReportDebugOfLevel(DebugLevel.ERROR);
					if (flag7)
					{
						base.EnqueueDebugReturn(DebugLevel.ERROR, "Failed to connect to server after testing each known IP. Error(s): " + text);
					}
					base.HandleException(StatusCode.ExceptionOnConnect);
				}
				else
				{
					base.AddressResolvedAsIpv6 = (this.sock.AddressFamily == AddressFamily.InterNetworkV6);
					IPhotonSocket.ServerIpAddress = this.sock.RemoteEndPoint.ToString();
					base.State = PhotonSocketState.Connected;
					this.peerBase.OnConnect();
					this.ReceiveAsync(null);
				}
			}
		}

		// Token: 0x060002A3 RID: 675 RVA: 0x00014EA4 File Offset: 0x000130A4
		private void ReceiveAsync(SocketTcpAsync.ReceiveContext context = null)
		{
			bool flag = context == null;
			if (flag)
			{
				context = new SocketTcpAsync.ReceiveContext(this.sock, new byte[9], new byte[base.MTU]);
			}
			try
			{
				this.sock.BeginReceive(context.CurrentBuffer, context.CurrentOffset, context.CurrentExpected - context.CurrentOffset, SocketFlags.None, new AsyncCallback(this.ReceiveAsync), context);
			}
			catch (Exception ex)
			{
				bool flag2 = base.State != PhotonSocketState.Disconnecting && base.State > PhotonSocketState.Disconnected;
				if (flag2)
				{
					bool flag3 = base.ReportDebugOfLevel(DebugLevel.ERROR);
					if (flag3)
					{
						DebugLevel debugLevel = DebugLevel.ERROR;
						string[] array = new string[6];
						array[0] = "SocketTcpAsync.ReceiveAsync Exception. State: ";
						array[1] = base.State.ToString();
						array[2] = ". Server: '";
						array[3] = base.ServerAddress;
						array[4] = "' Exception: ";
						int num = 5;
						Exception ex2 = ex;
						array[num] = ((ex2 != null) ? ex2.ToString() : null);
						base.EnqueueDebugReturn(debugLevel, string.Concat(array));
					}
					base.HandleException(StatusCode.ExceptionOnReceive);
				}
			}
		}

		// Token: 0x060002A4 RID: 676 RVA: 0x00014FBC File Offset: 0x000131BC
		private void ReceiveAsync(IAsyncResult ar)
		{
			bool flag = base.State == PhotonSocketState.Disconnecting || base.State == PhotonSocketState.Disconnected;
			if (!flag)
			{
				int num = 0;
				try
				{
					num = this.sock.EndReceive(ar);
					bool flag2 = num == 0;
					if (flag2)
					{
						throw new SocketException(10054);
					}
				}
				catch (SocketException ex)
				{
					bool flag3 = base.State != PhotonSocketState.Disconnecting && base.State > PhotonSocketState.Disconnected;
					if (flag3)
					{
						bool flag4 = base.ReportDebugOfLevel(DebugLevel.ERROR);
						if (flag4)
						{
							DebugLevel debugLevel = DebugLevel.ERROR;
							string[] array = new string[12];
							array[0] = "SocketTcpAsync.EndReceive SocketException. State: ";
							array[1] = base.State.ToString();
							array[2] = ". Server: '";
							array[3] = base.ServerAddress;
							array[4] = "' ErrorCode: ";
							array[5] = ex.ErrorCode.ToString();
							array[6] = " SocketErrorCode: ";
							array[7] = ex.SocketErrorCode.ToString();
							array[8] = " Message: ";
							array[9] = ex.Message;
							array[10] = " ";
							int num2 = 11;
							SocketException ex2 = ex;
							array[num2] = ((ex2 != null) ? ex2.ToString() : null);
							base.EnqueueDebugReturn(debugLevel, string.Concat(array));
						}
						base.HandleException(StatusCode.ExceptionOnReceive);
						return;
					}
				}
				catch (Exception ex3)
				{
					bool flag5 = base.State != PhotonSocketState.Disconnecting && base.State > PhotonSocketState.Disconnected;
					if (flag5)
					{
						bool flag6 = base.ReportDebugOfLevel(DebugLevel.ERROR);
						if (flag6)
						{
							DebugLevel debugLevel2 = DebugLevel.ERROR;
							string[] array2 = new string[6];
							array2[0] = "SocketTcpAsync.EndReceive Exception. State: ";
							array2[1] = base.State.ToString();
							array2[2] = ". Server: '";
							array2[3] = base.ServerAddress;
							array2[4] = "' Exception: ";
							int num3 = 5;
							Exception ex4 = ex3;
							array2[num3] = ((ex4 != null) ? ex4.ToString() : null);
							base.EnqueueDebugReturn(debugLevel2, string.Concat(array2));
						}
						base.HandleException(StatusCode.ExceptionOnReceive);
						return;
					}
				}
				SocketTcpAsync.ReceiveContext receiveContext = (SocketTcpAsync.ReceiveContext)ar.AsyncState;
				bool flag7 = num + receiveContext.CurrentOffset != receiveContext.CurrentExpected;
				if (flag7)
				{
					bool readingHeader = receiveContext.ReadingHeader;
					if (readingHeader)
					{
						receiveContext.ReceivedHeaderBytes += num;
					}
					else
					{
						receiveContext.ReceivedMessageBytes += num;
					}
					this.ReceiveAsync(receiveContext);
				}
				else
				{
					bool readingHeader2 = receiveContext.ReadingHeader;
					if (readingHeader2)
					{
						byte[] headerBuffer = receiveContext.HeaderBuffer;
						bool flag8 = headerBuffer[0] == 240;
						if (flag8)
						{
							base.HandleReceivedDatagram(headerBuffer, headerBuffer.Length, true);
							receiveContext.Reset();
							this.ReceiveAsync(receiveContext);
						}
						else
						{
							int num4 = (int)headerBuffer[1] << 24 | (int)headerBuffer[2] << 16 | (int)headerBuffer[3] << 8 | (int)headerBuffer[4];
							receiveContext.ExpectedMessageBytes = num4 - 7;
							bool flag9 = receiveContext.ExpectedMessageBytes > receiveContext.MessageBuffer.Length;
							if (flag9)
							{
								receiveContext.MessageBuffer = new byte[receiveContext.ExpectedMessageBytes];
							}
							receiveContext.MessageBuffer[0] = headerBuffer[7];
							receiveContext.MessageBuffer[1] = headerBuffer[8];
							receiveContext.ReceivedMessageBytes = 2;
							this.ReceiveAsync(receiveContext);
						}
					}
					else
					{
						base.HandleReceivedDatagram(receiveContext.MessageBuffer, receiveContext.ExpectedMessageBytes, true);
						receiveContext.Reset();
						this.ReceiveAsync(receiveContext);
					}
				}
			}
		}

		// Token: 0x0400019C RID: 412
		private Socket sock;

		// Token: 0x0400019D RID: 413
		private readonly object syncer = new object();

		// Token: 0x0200005A RID: 90
		private class ReceiveContext
		{
			// Token: 0x0600043B RID: 1083 RVA: 0x00020F01 File Offset: 0x0001F101
			public ReceiveContext(Socket socket, byte[] headerBuffer, byte[] messageBuffer)
			{
				this.HeaderBuffer = headerBuffer;
				this.MessageBuffer = messageBuffer;
				this.workSocket = socket;
			}

			// Token: 0x170000CB RID: 203
			// (get) Token: 0x0600043C RID: 1084 RVA: 0x00020F20 File Offset: 0x0001F120
			public bool ReadingHeader
			{
				get
				{
					return this.ExpectedMessageBytes == 0;
				}
			}

			// Token: 0x170000CC RID: 204
			// (get) Token: 0x0600043D RID: 1085 RVA: 0x00020F3C File Offset: 0x0001F13C
			public bool ReadingMessage
			{
				get
				{
					return this.ExpectedMessageBytes != 0;
				}
			}

			// Token: 0x170000CD RID: 205
			// (get) Token: 0x0600043E RID: 1086 RVA: 0x00020F58 File Offset: 0x0001F158
			public byte[] CurrentBuffer
			{
				get
				{
					return this.ReadingHeader ? this.HeaderBuffer : this.MessageBuffer;
				}
			}

			// Token: 0x170000CE RID: 206
			// (get) Token: 0x0600043F RID: 1087 RVA: 0x00020F80 File Offset: 0x0001F180
			public int CurrentOffset
			{
				get
				{
					return this.ReadingHeader ? this.ReceivedHeaderBytes : this.ReceivedMessageBytes;
				}
			}

			// Token: 0x170000CF RID: 207
			// (get) Token: 0x06000440 RID: 1088 RVA: 0x00020FA8 File Offset: 0x0001F1A8
			public int CurrentExpected
			{
				get
				{
					return this.ReadingHeader ? 9 : this.ExpectedMessageBytes;
				}
			}

			// Token: 0x06000441 RID: 1089 RVA: 0x00020FCC File Offset: 0x0001F1CC
			public void Reset()
			{
				this.ReceivedHeaderBytes = 0;
				this.ExpectedMessageBytes = 0;
				this.ReceivedMessageBytes = 0;
			}

			// Token: 0x04000275 RID: 629
			public Socket workSocket;

			// Token: 0x04000276 RID: 630
			public int ReceivedHeaderBytes;

			// Token: 0x04000277 RID: 631
			public byte[] HeaderBuffer;

			// Token: 0x04000278 RID: 632
			public int ExpectedMessageBytes;

			// Token: 0x04000279 RID: 633
			public int ReceivedMessageBytes;

			// Token: 0x0400027A RID: 634
			public byte[] MessageBuffer;
		}
	}
}

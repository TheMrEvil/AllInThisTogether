using System;
using System.Net;
using System.Net.Sockets;
using System.Security;
using System.Threading;

namespace ExitGames.Client.Photon
{
	// Token: 0x02000032 RID: 50
	public class SocketTcp : IPhotonSocket, IDisposable
	{
		// Token: 0x06000292 RID: 658 RVA: 0x00013C34 File Offset: 0x00011E34
		[Preserve]
		public SocketTcp(PeerBase npeer) : base(npeer)
		{
			bool flag = base.ReportDebugOfLevel(DebugLevel.INFO);
			if (flag)
			{
				base.Listener.DebugReturn(DebugLevel.INFO, "SocketTcp, .Net, Unity.");
			}
			this.PollReceive = false;
		}

		// Token: 0x06000293 RID: 659 RVA: 0x00013C7C File Offset: 0x00011E7C
		~SocketTcp()
		{
			this.Dispose();
		}

		// Token: 0x06000294 RID: 660 RVA: 0x00013CAC File Offset: 0x00011EAC
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

		// Token: 0x06000295 RID: 661 RVA: 0x00013D38 File Offset: 0x00011F38
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

		// Token: 0x06000296 RID: 662 RVA: 0x00013DC0 File Offset: 0x00011FC0
		public override bool Disconnect()
		{
			bool flag = base.ReportDebugOfLevel(DebugLevel.INFO);
			if (flag)
			{
				base.EnqueueDebugReturn(DebugLevel.INFO, "SocketTcp.Disconnect()");
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

		// Token: 0x06000297 RID: 663 RVA: 0x00013E90 File Offset: 0x00012090
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

		// Token: 0x06000298 RID: 664 RVA: 0x00014010 File Offset: 0x00012210
		public override PhotonSocketError Receive(out byte[] data)
		{
			data = null;
			return PhotonSocketError.NoData;
		}

		// Token: 0x06000299 RID: 665 RVA: 0x00014028 File Offset: 0x00012228
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
					new Thread(new ThreadStart(this.ReceiveLoop))
					{
						IsBackground = true
					}.Start();
				}
			}
		}

		// Token: 0x0600029A RID: 666 RVA: 0x00014314 File Offset: 0x00012514
		public void ReceiveLoop()
		{
			StreamBuffer streamBuffer = new StreamBuffer(base.MTU);
			byte[] array = new byte[9];
			while (base.State == PhotonSocketState.Connected)
			{
				streamBuffer.SetLength(0L);
				try
				{
					int i = 0;
					int num = 0;
					while (i < 9)
					{
						try
						{
							num = this.sock.Receive(array, i, 9 - i, SocketFlags.None);
						}
						catch (SocketException ex)
						{
							bool flag = base.State != PhotonSocketState.Disconnecting && base.State > PhotonSocketState.Disconnected && ex.SocketErrorCode == SocketError.WouldBlock;
							if (flag)
							{
								bool flag2 = base.ReportDebugOfLevel(DebugLevel.ALL);
								if (flag2)
								{
									base.EnqueueDebugReturn(DebugLevel.ALL, "ReceiveLoop() got a WouldBlock exception. This is non-fatal. Going to continue.");
								}
								continue;
							}
							throw;
						}
						i += num;
						bool flag3 = num == 0;
						if (flag3)
						{
							throw new SocketException(10054);
						}
					}
					bool flag4 = array[0] == 240;
					if (flag4)
					{
						base.HandleReceivedDatagram(array, array.Length, true);
					}
					else
					{
						int num2 = (int)array[1] << 24 | (int)array[2] << 16 | (int)array[3] << 8 | (int)array[4];
						bool flag5 = this.peerBase.TrafficStatsEnabled && array[0] == 251;
						if (flag5)
						{
							bool flag6 = array[6] == 0;
							bool flag7 = flag6;
							if (flag7)
							{
								this.peerBase.TrafficStatsIncoming.CountReliableOpCommand(num2);
							}
							else
							{
								this.peerBase.TrafficStatsIncoming.CountUnreliableOpCommand(num2);
							}
						}
						bool flag8 = base.ReportDebugOfLevel(DebugLevel.ALL);
						if (flag8)
						{
							base.EnqueueDebugReturn(DebugLevel.ALL, "TCP < " + num2.ToString());
						}
						streamBuffer.SetCapacityMinimum(num2 - 7);
						streamBuffer.Write(array, 7, i - 7);
						i = 0;
						num2 -= 9;
						while (i < num2)
						{
							try
							{
								num = this.sock.Receive(streamBuffer.GetBuffer(), streamBuffer.Position, num2 - i, SocketFlags.None);
							}
							catch (SocketException ex2)
							{
								bool flag9 = base.State != PhotonSocketState.Disconnecting && base.State > PhotonSocketState.Disconnected && ex2.SocketErrorCode == SocketError.WouldBlock;
								if (flag9)
								{
									bool flag10 = base.ReportDebugOfLevel(DebugLevel.ALL);
									if (flag10)
									{
										base.EnqueueDebugReturn(DebugLevel.ALL, "ReceiveLoop() got a WouldBlock exception. This is non-fatal. Going to continue.");
									}
									continue;
								}
								throw;
							}
							streamBuffer.Position += num;
							i += num;
							bool flag11 = num == 0;
							if (flag11)
							{
								throw new SocketException(10054);
							}
						}
						base.HandleReceivedDatagram(streamBuffer.ToArray(), streamBuffer.Length, false);
						bool flag12 = base.ReportDebugOfLevel(DebugLevel.ALL);
						if (flag12)
						{
							base.EnqueueDebugReturn(DebugLevel.ALL, "TCP < " + streamBuffer.Length.ToString() + ((streamBuffer.Length == num2 + 2) ? " OK" : " BAD"));
						}
					}
				}
				catch (SocketException ex3)
				{
					bool flag13 = base.State != PhotonSocketState.Disconnecting && base.State > PhotonSocketState.Disconnected;
					if (flag13)
					{
						bool flag14 = base.ReportDebugOfLevel(DebugLevel.ERROR);
						if (flag14)
						{
							base.EnqueueDebugReturn(DebugLevel.ERROR, "Receiving failed. SocketException: " + ex3.SocketErrorCode.ToString());
						}
						bool flag15 = ex3.SocketErrorCode == SocketError.ConnectionReset || ex3.SocketErrorCode == SocketError.ConnectionAborted;
						if (flag15)
						{
							base.HandleException(StatusCode.DisconnectByServerTimeout);
						}
						else
						{
							base.HandleException(StatusCode.ExceptionOnReceive);
						}
					}
				}
				catch (Exception ex4)
				{
					bool flag16 = base.State != PhotonSocketState.Disconnecting && base.State > PhotonSocketState.Disconnected;
					if (flag16)
					{
						bool flag17 = base.ReportDebugOfLevel(DebugLevel.ERROR);
						if (flag17)
						{
							DebugLevel debugLevel = DebugLevel.ERROR;
							string[] array2 = new string[6];
							array2[0] = "Receive issue. State: ";
							array2[1] = base.State.ToString();
							array2[2] = ". Server: '";
							array2[3] = base.ServerAddress;
							array2[4] = "' Exception: ";
							int num3 = 5;
							Exception ex5 = ex4;
							array2[num3] = ((ex5 != null) ? ex5.ToString() : null);
							base.EnqueueDebugReturn(debugLevel, string.Concat(array2));
						}
						base.HandleException(StatusCode.ExceptionOnReceive);
					}
				}
			}
			object obj = this.syncer;
			lock (obj)
			{
				bool flag19 = base.State != PhotonSocketState.Disconnecting && base.State > PhotonSocketState.Disconnected;
				if (flag19)
				{
					this.Disconnect();
				}
			}
		}

		// Token: 0x0400019A RID: 410
		private Socket sock;

		// Token: 0x0400019B RID: 411
		private readonly object syncer = new object();
	}
}

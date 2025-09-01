using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace ExitGames.Client.Photon
{
	// Token: 0x02000036 RID: 54
	public class SocketUdpBlocking : IPhotonSocket, IDisposable
	{
		// Token: 0x060002B8 RID: 696 RVA: 0x000168D0 File Offset: 0x00014AD0
		[Preserve]
		public SocketUdpBlocking(PeerBase npeer) : base(npeer)
		{
			bool flag = base.ReportDebugOfLevel(DebugLevel.INFO);
			if (flag)
			{
				base.Listener.DebugReturn(DebugLevel.INFO, "SocketUdpBlocking, .Net, Unity.");
			}
			this.PollReceive = false;
		}

		// Token: 0x060002B9 RID: 697 RVA: 0x00016918 File Offset: 0x00014B18
		~SocketUdpBlocking()
		{
			this.Dispose();
		}

		// Token: 0x060002BA RID: 698 RVA: 0x00016948 File Offset: 0x00014B48
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
						this.sock.Close(1);
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

		// Token: 0x060002BB RID: 699 RVA: 0x000169D4 File Offset: 0x00014BD4
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

		// Token: 0x060002BC RID: 700 RVA: 0x00016A5C File Offset: 0x00014C5C
		public override bool Disconnect()
		{
			bool flag = base.ReportDebugOfLevel(DebugLevel.INFO);
			if (flag)
			{
				base.EnqueueDebugReturn(DebugLevel.INFO, "SocketUdpBlocking.Disconnect()");
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
						this.sock.Close(1);
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

		// Token: 0x060002BD RID: 701 RVA: 0x00016B30 File Offset: 0x00014D30
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
						base.EnqueueDebugReturn(DebugLevel.INFO, string.Format("Cannot send to: {0}. Uptime: {1} ms. {2} {3}\n{4}", new object[]
						{
							base.ServerAddress,
							this.peerBase.timeInt,
							base.AddressResolvedAsIpv6 ? " IPv6" : string.Empty,
							text,
							ex
						}));
					}
					bool flag5 = !this.sock.Connected;
					if (flag5)
					{
						base.EnqueueDebugReturn(DebugLevel.INFO, "Socket got closed by the local system. Disconnecting from within Send with StatusCode.Disconnect.");
						base.HandleException(StatusCode.SendError);
					}
				}
				return PhotonSocketError.Exception;
			}
			return PhotonSocketError.Success;
		}

		// Token: 0x060002BE RID: 702 RVA: 0x00016CD4 File Offset: 0x00014ED4
		public override PhotonSocketError Receive(out byte[] data)
		{
			data = null;
			return PhotonSocketError.NoData;
		}

		// Token: 0x060002BF RID: 703 RVA: 0x00016CEC File Offset: 0x00014EEC
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
						this.sock = new Socket(ipaddress.AddressFamily, SocketType.Dgram, ProtocolType.Udp);
						this.sock.Blocking = false;
						this.sock.Connect(ipaddress, base.ServerPort);
						bool flag2 = this.sock != null && this.sock.Connected;
						if (flag2)
						{
							break;
						}
					}
					catch (SocketException ex)
					{
						bool flag3 = base.ReportDebugOfLevel(DebugLevel.WARNING);
						if (flag3)
						{
							string[] array2 = new string[5];
							array2[0] = text;
							int num = 1;
							SocketException ex2 = ex;
							array2[num] = ((ex2 != null) ? ex2.ToString() : null);
							array2[2] = " ";
							array2[3] = ex.ErrorCode.ToString();
							array2[4] = "; ";
							text = string.Concat(array2);
							DebugLevel debugLevel = DebugLevel.WARNING;
							string str = "SocketException caught: ";
							SocketException ex3 = ex;
							base.EnqueueDebugReturn(debugLevel, str + ((ex3 != null) ? ex3.ToString() : null) + " ErrorCode: " + ex.ErrorCode.ToString());
						}
					}
					catch (Exception ex4)
					{
						bool flag4 = base.ReportDebugOfLevel(DebugLevel.WARNING);
						if (flag4)
						{
							string str2 = text;
							Exception ex5 = ex4;
							text = str2 + ((ex5 != null) ? ex5.ToString() : null) + "; ";
							DebugLevel debugLevel2 = DebugLevel.WARNING;
							string str3 = "Exception caught: ";
							Exception ex6 = ex4;
							base.EnqueueDebugReturn(debugLevel2, str3 + ((ex6 != null) ? ex6.ToString() : null));
						}
					}
				}
				bool flag5 = this.sock == null || !this.sock.Connected;
				if (flag5)
				{
					bool flag6 = base.ReportDebugOfLevel(DebugLevel.ERROR);
					if (flag6)
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

		// Token: 0x060002C0 RID: 704 RVA: 0x00016F4C File Offset: 0x0001514C
		public void ReceiveLoop()
		{
			byte[] array = new byte[base.MTU];
			while (base.State == PhotonSocketState.Connected)
			{
				try
				{
					bool flag = this.sock.Poll(5000, SelectMode.SelectRead);
					bool flag2 = !flag;
					if (!flag2)
					{
						int length = this.sock.Receive(array);
						base.HandleReceivedDatagram(array, length, true);
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
							string[] array2 = new string[12];
							array2[0] = "Receive issue. State: ";
							array2[1] = base.State.ToString();
							array2[2] = ". Server: '";
							array2[3] = base.ServerAddress;
							array2[4] = "' ErrorCode: ";
							array2[5] = ex.ErrorCode.ToString();
							array2[6] = " SocketErrorCode: ";
							array2[7] = ex.SocketErrorCode.ToString();
							array2[8] = " Message: ";
							array2[9] = ex.Message;
							array2[10] = " ";
							int num = 11;
							SocketException ex2 = ex;
							array2[num] = ((ex2 != null) ? ex2.ToString() : null);
							base.EnqueueDebugReturn(debugLevel, string.Concat(array2));
						}
						base.HandleException(StatusCode.ExceptionOnReceive);
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
							string[] array3 = new string[8];
							array3[0] = "Receive issue. State: ";
							array3[1] = base.State.ToString();
							array3[2] = ". Server: '";
							array3[3] = base.ServerAddress;
							array3[4] = "' Message: ";
							array3[5] = ex3.Message;
							array3[6] = " Exception: ";
							int num2 = 7;
							Exception ex4 = ex3;
							array3[num2] = ((ex4 != null) ? ex4.ToString() : null);
							base.EnqueueDebugReturn(debugLevel2, string.Concat(array3));
						}
						base.HandleException(StatusCode.ExceptionOnReceive);
					}
				}
			}
			object obj = this.syncer;
			lock (obj)
			{
				bool flag8 = base.State != PhotonSocketState.Disconnecting && base.State > PhotonSocketState.Disconnected;
				if (flag8)
				{
					this.Disconnect();
				}
			}
		}

		// Token: 0x040001A2 RID: 418
		private Socket sock;

		// Token: 0x040001A3 RID: 419
		private readonly object syncer = new object();
	}
}

using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace ExitGames.Client.Photon
{
	// Token: 0x02000035 RID: 53
	public class SocketUdpAsync : IPhotonSocket, IDisposable
	{
		// Token: 0x060002AE RID: 686 RVA: 0x00015DA8 File Offset: 0x00013FA8
		[Preserve]
		public SocketUdpAsync(PeerBase npeer) : base(npeer)
		{
			bool flag = base.ReportDebugOfLevel(DebugLevel.INFO);
			if (flag)
			{
				base.Listener.DebugReturn(DebugLevel.INFO, "SocketUdpAsync, .Net, Unity.");
			}
			this.PollReceive = false;
		}

		// Token: 0x060002AF RID: 687 RVA: 0x00015DF0 File Offset: 0x00013FF0
		~SocketUdpAsync()
		{
			this.Dispose();
		}

		// Token: 0x060002B0 RID: 688 RVA: 0x00015E20 File Offset: 0x00014020
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

		// Token: 0x060002B1 RID: 689 RVA: 0x00015EAC File Offset: 0x000140AC
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

		// Token: 0x060002B2 RID: 690 RVA: 0x00015F34 File Offset: 0x00014134
		public override bool Disconnect()
		{
			bool flag = base.ReportDebugOfLevel(DebugLevel.INFO);
			if (flag)
			{
				base.EnqueueDebugReturn(DebugLevel.INFO, "SocketUdpAsync.Disconnect()");
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

		// Token: 0x060002B3 RID: 691 RVA: 0x00016004 File Offset: 0x00014204
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

		// Token: 0x060002B4 RID: 692 RVA: 0x000161A8 File Offset: 0x000143A8
		public override PhotonSocketError Receive(out byte[] data)
		{
			data = null;
			return PhotonSocketError.NoData;
		}

		// Token: 0x060002B5 RID: 693 RVA: 0x000161C0 File Offset: 0x000143C0
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
							string str = "SocketException catched: ";
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
							string str3 = "Exception catched: ";
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
					this.StartReceive();
				}
			}
		}

		// Token: 0x060002B6 RID: 694 RVA: 0x000163F4 File Offset: 0x000145F4
		public void StartReceive()
		{
			byte[] array = new byte[base.MTU];
			try
			{
				this.sock.BeginReceive(array, 0, array.Length, SocketFlags.None, new AsyncCallback(this.OnReceive), array);
			}
			catch (Exception ex)
			{
				bool flag = base.State != PhotonSocketState.Disconnecting && base.State > PhotonSocketState.Disconnected;
				if (flag)
				{
					bool flag2 = base.ReportDebugOfLevel(DebugLevel.ERROR);
					if (flag2)
					{
						DebugLevel debugLevel = DebugLevel.ERROR;
						string[] array2 = new string[6];
						array2[0] = "Receive issue. State: ";
						array2[1] = base.State.ToString();
						array2[2] = ". Server: '";
						array2[3] = base.ServerAddress;
						array2[4] = "' Exception: ";
						int num = 5;
						Exception ex2 = ex;
						array2[num] = ((ex2 != null) ? ex2.ToString() : null);
						base.EnqueueDebugReturn(debugLevel, string.Concat(array2));
					}
					base.HandleException(StatusCode.ExceptionOnReceive);
				}
			}
		}

		// Token: 0x060002B7 RID: 695 RVA: 0x000164DC File Offset: 0x000146DC
		private void OnReceive(IAsyncResult ar)
		{
			bool flag = base.State == PhotonSocketState.Disconnecting || base.State == PhotonSocketState.Disconnected;
			if (!flag)
			{
				int length = 0;
				try
				{
					length = this.sock.EndReceive(ar);
				}
				catch (SocketException ex)
				{
					bool flag2 = base.State != PhotonSocketState.Disconnecting && base.State > PhotonSocketState.Disconnected;
					if (flag2)
					{
						bool flag3 = base.ReportDebugOfLevel(DebugLevel.ERROR);
						if (flag3)
						{
							DebugLevel debugLevel = DebugLevel.ERROR;
							string[] array = new string[12];
							array[0] = "SocketException in EndReceive. State: ";
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
							int num = 11;
							SocketException ex2 = ex;
							array[num] = ((ex2 != null) ? ex2.ToString() : null);
							base.EnqueueDebugReturn(debugLevel, string.Concat(array));
						}
						base.HandleException(StatusCode.ExceptionOnReceive);
					}
				}
				catch (Exception ex3)
				{
					bool flag4 = base.State != PhotonSocketState.Disconnecting && base.State > PhotonSocketState.Disconnected;
					if (flag4)
					{
						bool flag5 = base.ReportDebugOfLevel(DebugLevel.ERROR);
						if (flag5)
						{
							DebugLevel debugLevel2 = DebugLevel.ERROR;
							string[] array2 = new string[6];
							array2[0] = "Exception in EndReceive. State: ";
							array2[1] = base.State.ToString();
							array2[2] = ". Server: '";
							array2[3] = base.ServerAddress;
							array2[4] = "' Exception: ";
							int num2 = 5;
							Exception ex4 = ex3;
							array2[num2] = ((ex4 != null) ? ex4.ToString() : null);
							base.EnqueueDebugReturn(debugLevel2, string.Concat(array2));
						}
						base.HandleException(StatusCode.ExceptionOnReceive);
					}
				}
				bool flag6 = base.State == PhotonSocketState.Disconnecting || base.State == PhotonSocketState.Disconnected;
				if (!flag6)
				{
					byte[] array3 = (byte[])ar.AsyncState;
					base.HandleReceivedDatagram(array3, length, true);
					try
					{
						this.sock.BeginReceive(array3, 0, array3.Length, SocketFlags.None, new AsyncCallback(this.OnReceive), array3);
					}
					catch (SocketException ex5)
					{
						bool flag7 = base.State != PhotonSocketState.Disconnecting && base.State > PhotonSocketState.Disconnected;
						if (flag7)
						{
							bool flag8 = base.ReportDebugOfLevel(DebugLevel.ERROR);
							if (flag8)
							{
								DebugLevel debugLevel3 = DebugLevel.ERROR;
								string[] array4 = new string[12];
								array4[0] = "SocketException in BeginReceive. State: ";
								array4[1] = base.State.ToString();
								array4[2] = ". Server: '";
								array4[3] = base.ServerAddress;
								array4[4] = "' ErrorCode: ";
								array4[5] = ex5.ErrorCode.ToString();
								array4[6] = " SocketErrorCode: ";
								array4[7] = ex5.SocketErrorCode.ToString();
								array4[8] = " Message: ";
								array4[9] = ex5.Message;
								array4[10] = " ";
								int num3 = 11;
								SocketException ex6 = ex5;
								array4[num3] = ((ex6 != null) ? ex6.ToString() : null);
								base.EnqueueDebugReturn(debugLevel3, string.Concat(array4));
							}
							base.HandleException(StatusCode.ExceptionOnReceive);
						}
					}
					catch (Exception ex7)
					{
						bool flag9 = base.State != PhotonSocketState.Disconnecting && base.State > PhotonSocketState.Disconnected;
						if (flag9)
						{
							bool flag10 = base.ReportDebugOfLevel(DebugLevel.ERROR);
							if (flag10)
							{
								DebugLevel debugLevel4 = DebugLevel.ERROR;
								string[] array5 = new string[6];
								array5[0] = "Exception in BeginReceive. State: ";
								array5[1] = base.State.ToString();
								array5[2] = ". Server: '";
								array5[3] = base.ServerAddress;
								array5[4] = "' Exception: ";
								int num4 = 5;
								Exception ex8 = ex7;
								array5[num4] = ((ex8 != null) ? ex8.ToString() : null);
								base.EnqueueDebugReturn(debugLevel4, string.Concat(array5));
							}
							base.HandleException(StatusCode.ExceptionOnReceive);
						}
					}
				}
			}
		}

		// Token: 0x040001A0 RID: 416
		private Socket sock;

		// Token: 0x040001A1 RID: 417
		private readonly object syncer = new object();
	}
}

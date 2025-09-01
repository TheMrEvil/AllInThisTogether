using System;
using System.Net.Sockets;

namespace Photon.Realtime
{
	// Token: 0x02000036 RID: 54
	public class PingMono : PhotonPing
	{
		// Token: 0x0600014D RID: 333 RVA: 0x0000878C File Offset: 0x0000698C
		public override bool StartPing(string ip)
		{
			base.Init();
			try
			{
				if (this.sock == null)
				{
					if (ip.Contains("."))
					{
						this.sock = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
					}
					else
					{
						this.sock = new Socket(AddressFamily.InterNetworkV6, SocketType.Dgram, ProtocolType.Udp);
					}
					this.sock.ReceiveTimeout = 5000;
					int port = (int)((RegionHandler.PortToPingOverride != 0) ? RegionHandler.PortToPingOverride : 5055);
					this.sock.Connect(ip, port);
				}
				this.PingBytes[this.PingBytes.Length - 1] = this.PingId;
				this.sock.Send(this.PingBytes);
				this.PingBytes[this.PingBytes.Length - 1] = this.PingId + 1;
			}
			catch (Exception)
			{
				this.sock = null;
				throw;
			}
			return false;
		}

		// Token: 0x0600014E RID: 334 RVA: 0x00008868 File Offset: 0x00006A68
		public override bool Done()
		{
			if (this.GotResult || this.sock == null)
			{
				return true;
			}
			int num = 0;
			try
			{
				if (!this.sock.Poll(0, SelectMode.SelectRead))
				{
					return false;
				}
				num = this.sock.Receive(this.PingBytes, SocketFlags.None);
			}
			catch (Exception ex)
			{
				if (this.sock != null)
				{
					this.sock.Close();
					this.sock = null;
				}
				string debugString = this.DebugString;
				string str = " Exception of socket! ";
				Type type = ex.GetType();
				this.DebugString = debugString + str + ((type != null) ? type.ToString() : null) + " ";
				return true;
			}
			bool flag = this.PingBytes[this.PingBytes.Length - 1] == this.PingId && num == this.PingLength;
			if (!flag)
			{
				this.DebugString += " ReplyMatch is false! ";
			}
			this.Successful = flag;
			this.GotResult = true;
			return true;
		}

		// Token: 0x0600014F RID: 335 RVA: 0x00008960 File Offset: 0x00006B60
		public override void Dispose()
		{
			if (this.sock == null)
			{
				return;
			}
			try
			{
				this.sock.Close();
			}
			catch
			{
			}
			this.sock = null;
		}

		// Token: 0x06000150 RID: 336 RVA: 0x000089A0 File Offset: 0x00006BA0
		public PingMono()
		{
		}

		// Token: 0x040001C0 RID: 448
		private Socket sock;
	}
}

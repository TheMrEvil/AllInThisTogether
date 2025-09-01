using System;

namespace System.Net.Sockets
{
	// Token: 0x02000791 RID: 1937
	internal sealed class DualSocketMultipleConnectAsync : MultipleConnectAsync
	{
		// Token: 0x06003CED RID: 15597 RVA: 0x000CF533 File Offset: 0x000CD733
		public DualSocketMultipleConnectAsync(SocketType socketType, ProtocolType protocolType)
		{
			if (Socket.OSSupportsIPv4)
			{
				this._socket4 = new Socket(AddressFamily.InterNetwork, socketType, protocolType);
			}
			if (Socket.OSSupportsIPv6)
			{
				this._socket6 = new Socket(AddressFamily.InterNetworkV6, socketType, protocolType);
			}
		}

		// Token: 0x06003CEE RID: 15598 RVA: 0x000CF568 File Offset: 0x000CD768
		protected override IPAddress GetNextAddress(out Socket attemptSocket)
		{
			IPAddress ipaddress = null;
			attemptSocket = null;
			while (attemptSocket == null)
			{
				if (this._nextAddress >= this._addressList.Length)
				{
					return null;
				}
				ipaddress = this._addressList[this._nextAddress];
				this._nextAddress++;
				if (ipaddress.AddressFamily == AddressFamily.InterNetworkV6)
				{
					attemptSocket = this._socket6;
				}
				else if (ipaddress.AddressFamily == AddressFamily.InterNetwork)
				{
					attemptSocket = this._socket4;
				}
			}
			Socket socket = attemptSocket;
			if (socket != null)
			{
				socket.ReplaceHandleIfNecessaryAfterFailedConnect();
			}
			return ipaddress;
		}

		// Token: 0x06003CEF RID: 15599 RVA: 0x000CF5E4 File Offset: 0x000CD7E4
		protected override void OnSucceed()
		{
			if (this._socket4 != null && !this._socket4.Connected)
			{
				this._socket4.Dispose();
			}
			if (this._socket6 != null && !this._socket6.Connected)
			{
				this._socket6.Dispose();
			}
		}

		// Token: 0x06003CF0 RID: 15600 RVA: 0x000CF631 File Offset: 0x000CD831
		protected override void OnFail(bool abortive)
		{
			Socket socket = this._socket4;
			if (socket != null)
			{
				socket.Dispose();
			}
			Socket socket2 = this._socket6;
			if (socket2 == null)
			{
				return;
			}
			socket2.Dispose();
		}

		// Token: 0x04002419 RID: 9241
		private Socket _socket4;

		// Token: 0x0400241A RID: 9242
		private Socket _socket6;
	}
}

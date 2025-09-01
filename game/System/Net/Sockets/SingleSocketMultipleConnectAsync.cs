using System;

namespace System.Net.Sockets
{
	// Token: 0x02000790 RID: 1936
	internal sealed class SingleSocketMultipleConnectAsync : MultipleConnectAsync
	{
		// Token: 0x06003CE9 RID: 15593 RVA: 0x000CF49E File Offset: 0x000CD69E
		public SingleSocketMultipleConnectAsync(Socket socket, bool userSocket)
		{
			this._socket = socket;
			this._userSocket = userSocket;
		}

		// Token: 0x06003CEA RID: 15594 RVA: 0x000CF4B4 File Offset: 0x000CD6B4
		protected override IPAddress GetNextAddress(out Socket attemptSocket)
		{
			this._socket.ReplaceHandleIfNecessaryAfterFailedConnect();
			while (this._nextAddress < this._addressList.Length)
			{
				IPAddress ipaddress = this._addressList[this._nextAddress];
				this._nextAddress++;
				if (this._socket.CanTryAddressFamily(ipaddress.AddressFamily))
				{
					attemptSocket = this._socket;
					return ipaddress;
				}
			}
			attemptSocket = null;
			return null;
		}

		// Token: 0x06003CEB RID: 15595 RVA: 0x000CF51B File Offset: 0x000CD71B
		protected override void OnFail(bool abortive)
		{
			if (abortive || !this._userSocket)
			{
				this._socket.Dispose();
			}
		}

		// Token: 0x06003CEC RID: 15596 RVA: 0x00003917 File Offset: 0x00001B17
		protected override void OnSucceed()
		{
		}

		// Token: 0x04002417 RID: 9239
		private Socket _socket;

		// Token: 0x04002418 RID: 9240
		private bool _userSocket;
	}
}

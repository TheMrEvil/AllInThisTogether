using System;
using Steamworks.Data;

namespace Steamworks
{
	// Token: 0x0200008F RID: 143
	public interface IConnectionManager
	{
		// Token: 0x06000722 RID: 1826
		void OnConnecting(ConnectionInfo info);

		// Token: 0x06000723 RID: 1827
		void OnConnected(ConnectionInfo info);

		// Token: 0x06000724 RID: 1828
		void OnDisconnected(ConnectionInfo info);

		// Token: 0x06000725 RID: 1829
		void OnMessage(IntPtr data, int size, long messageNum, long recvTime, int channel);
	}
}

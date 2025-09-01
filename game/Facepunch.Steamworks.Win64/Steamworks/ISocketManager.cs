using System;
using Steamworks.Data;

namespace Steamworks
{
	// Token: 0x02000090 RID: 144
	public interface ISocketManager
	{
		// Token: 0x06000726 RID: 1830
		void OnConnecting(Connection connection, ConnectionInfo info);

		// Token: 0x06000727 RID: 1831
		void OnConnected(Connection connection, ConnectionInfo info);

		// Token: 0x06000728 RID: 1832
		void OnDisconnected(Connection connection, ConnectionInfo info);

		// Token: 0x06000729 RID: 1833
		void OnMessage(Connection connection, NetIdentity identity, IntPtr data, int size, long messageNum, long recvTime, int channel);
	}
}

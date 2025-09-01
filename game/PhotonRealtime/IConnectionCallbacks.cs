using System;
using System.Collections.Generic;

namespace Photon.Realtime
{
	// Token: 0x0200000F RID: 15
	public interface IConnectionCallbacks
	{
		// Token: 0x060000BE RID: 190
		void OnConnected();

		// Token: 0x060000BF RID: 191
		void OnConnectedToMaster();

		// Token: 0x060000C0 RID: 192
		void OnDisconnected(DisconnectCause cause);

		// Token: 0x060000C1 RID: 193
		void OnRegionListReceived(RegionHandler regionHandler);

		// Token: 0x060000C2 RID: 194
		void OnCustomAuthenticationResponse(Dictionary<string, object> data);

		// Token: 0x060000C3 RID: 195
		void OnCustomAuthenticationFailed(string debugMessage);
	}
}

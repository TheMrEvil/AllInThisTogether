using System;
using System.Collections.Generic;

namespace Photon.Realtime
{
	// Token: 0x02000016 RID: 22
	public class ConnectionCallbacksContainer : List<IConnectionCallbacks>, IConnectionCallbacks
	{
		// Token: 0x060000D7 RID: 215 RVA: 0x000067C7 File Offset: 0x000049C7
		public ConnectionCallbacksContainer(LoadBalancingClient client)
		{
			this.client = client;
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x000067D8 File Offset: 0x000049D8
		public void OnConnected()
		{
			this.client.UpdateCallbackTargets();
			foreach (IConnectionCallbacks connectionCallbacks in this)
			{
				connectionCallbacks.OnConnected();
			}
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x00006830 File Offset: 0x00004A30
		public void OnConnectedToMaster()
		{
			this.client.UpdateCallbackTargets();
			foreach (IConnectionCallbacks connectionCallbacks in this)
			{
				connectionCallbacks.OnConnectedToMaster();
			}
		}

		// Token: 0x060000DA RID: 218 RVA: 0x00006888 File Offset: 0x00004A88
		public void OnRegionListReceived(RegionHandler regionHandler)
		{
			this.client.UpdateCallbackTargets();
			foreach (IConnectionCallbacks connectionCallbacks in this)
			{
				connectionCallbacks.OnRegionListReceived(regionHandler);
			}
		}

		// Token: 0x060000DB RID: 219 RVA: 0x000068E0 File Offset: 0x00004AE0
		public void OnDisconnected(DisconnectCause cause)
		{
			this.client.UpdateCallbackTargets();
			foreach (IConnectionCallbacks connectionCallbacks in this)
			{
				connectionCallbacks.OnDisconnected(cause);
			}
		}

		// Token: 0x060000DC RID: 220 RVA: 0x00006938 File Offset: 0x00004B38
		public void OnCustomAuthenticationResponse(Dictionary<string, object> data)
		{
			this.client.UpdateCallbackTargets();
			foreach (IConnectionCallbacks connectionCallbacks in this)
			{
				connectionCallbacks.OnCustomAuthenticationResponse(data);
			}
		}

		// Token: 0x060000DD RID: 221 RVA: 0x00006990 File Offset: 0x00004B90
		public void OnCustomAuthenticationFailed(string debugMessage)
		{
			this.client.UpdateCallbackTargets();
			foreach (IConnectionCallbacks connectionCallbacks in this)
			{
				connectionCallbacks.OnCustomAuthenticationFailed(debugMessage);
			}
		}

		// Token: 0x040000AA RID: 170
		private readonly LoadBalancingClient client;
	}
}

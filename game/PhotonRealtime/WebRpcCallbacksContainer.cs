using System;
using System.Collections.Generic;
using ExitGames.Client.Photon;

namespace Photon.Realtime
{
	// Token: 0x0200001A RID: 26
	internal class WebRpcCallbacksContainer : List<IWebRpcCallback>, IWebRpcCallback
	{
		// Token: 0x060000F1 RID: 241 RVA: 0x00006F98 File Offset: 0x00005198
		public WebRpcCallbacksContainer(LoadBalancingClient client)
		{
			this.client = client;
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x00006FA8 File Offset: 0x000051A8
		public void OnWebRpcResponse(OperationResponse response)
		{
			this.client.UpdateCallbackTargets();
			foreach (IWebRpcCallback webRpcCallback in this)
			{
				webRpcCallback.OnWebRpcResponse(response);
			}
		}

		// Token: 0x040000AE RID: 174
		private LoadBalancingClient client;
	}
}

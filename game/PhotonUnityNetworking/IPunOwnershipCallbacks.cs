using System;
using Photon.Realtime;

namespace Photon.Pun
{
	// Token: 0x0200000D RID: 13
	public interface IPunOwnershipCallbacks
	{
		// Token: 0x06000009 RID: 9
		void OnOwnershipRequest(PhotonView targetView, Player requestingPlayer);

		// Token: 0x0600000A RID: 10
		void OnOwnershipTransfered(PhotonView targetView, Player previousOwner);

		// Token: 0x0600000B RID: 11
		void OnOwnershipTransferFailed(PhotonView targetView, Player senderOfFailedRequest);
	}
}

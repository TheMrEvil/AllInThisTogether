using System;
using Photon.Realtime;

namespace Photon.Pun
{
	// Token: 0x0200000A RID: 10
	public interface IOnPhotonViewOwnerChange : IPhotonViewCallback
	{
		// Token: 0x06000006 RID: 6
		void OnOwnerChange(Player newOwner, Player previousOwner);
	}
}

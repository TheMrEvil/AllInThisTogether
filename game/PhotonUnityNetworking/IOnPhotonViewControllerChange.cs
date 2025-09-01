using System;
using Photon.Realtime;

namespace Photon.Pun
{
	// Token: 0x0200000B RID: 11
	public interface IOnPhotonViewControllerChange : IPhotonViewCallback
	{
		// Token: 0x06000007 RID: 7
		void OnControllerChange(Player newController, Player previousController);
	}
}

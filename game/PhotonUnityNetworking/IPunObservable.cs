using System;

namespace Photon.Pun
{
	// Token: 0x0200000C RID: 12
	public interface IPunObservable
	{
		// Token: 0x06000008 RID: 8
		void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info);
	}
}

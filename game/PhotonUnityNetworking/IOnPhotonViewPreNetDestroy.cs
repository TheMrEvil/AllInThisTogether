using System;

namespace Photon.Pun
{
	// Token: 0x02000009 RID: 9
	public interface IOnPhotonViewPreNetDestroy : IPhotonViewCallback
	{
		// Token: 0x06000005 RID: 5
		void OnPreNetDestroy(PhotonView rootView);
	}
}

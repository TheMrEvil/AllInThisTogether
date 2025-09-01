using System;
using UnityEngine;

namespace Photon.Pun
{
	// Token: 0x02000016 RID: 22
	public class MonoBehaviourPun : MonoBehaviour
	{
		// Token: 0x17000040 RID: 64
		// (get) Token: 0x0600011E RID: 286 RVA: 0x000087ED File Offset: 0x000069ED
		public PhotonView photonView
		{
			get
			{
				if (this.pvCache == null)
				{
					this.pvCache = PhotonView.Get(this);
				}
				return this.pvCache;
			}
		}

		// Token: 0x0600011F RID: 287 RVA: 0x0000880F File Offset: 0x00006A0F
		public MonoBehaviourPun()
		{
		}

		// Token: 0x040000A4 RID: 164
		private PhotonView pvCache;
	}
}

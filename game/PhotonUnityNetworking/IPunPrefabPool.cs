using System;
using UnityEngine;

namespace Photon.Pun
{
	// Token: 0x0200000F RID: 15
	public interface IPunPrefabPool
	{
		// Token: 0x0600000D RID: 13
		GameObject Instantiate(string prefabId, Vector3 position, Quaternion rotation);

		// Token: 0x0600000E RID: 14
		void Destroy(GameObject gameObject);
	}
}

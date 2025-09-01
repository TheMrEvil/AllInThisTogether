using System;
using System.Collections.Generic;
using UnityEngine;

namespace Photon.Pun
{
	// Token: 0x0200001C RID: 28
	public class DefaultPool : IPunPrefabPool
	{
		// Token: 0x0600015D RID: 349 RVA: 0x00008E9C File Offset: 0x0000709C
		public GameObject Instantiate(string prefabId, Vector3 position, Quaternion rotation)
		{
			GameObject gameObject = null;
			if (!this.ResourceCache.TryGetValue(prefabId, out gameObject))
			{
				gameObject = Resources.Load<GameObject>(prefabId);
				if (gameObject == null)
				{
					Debug.LogError("DefaultPool failed to load \"" + prefabId + "\". Make sure it's in a \"Resources\" folder. Or use a custom IPunPrefabPool.");
				}
				else
				{
					this.ResourceCache.Add(prefabId, gameObject);
				}
			}
			bool activeSelf = gameObject.activeSelf;
			if (activeSelf)
			{
				gameObject.SetActive(false);
			}
			GameObject result = UnityEngine.Object.Instantiate<GameObject>(gameObject, position, rotation);
			if (activeSelf)
			{
				gameObject.SetActive(true);
			}
			return result;
		}

		// Token: 0x0600015E RID: 350 RVA: 0x00008F12 File Offset: 0x00007112
		public void Destroy(GameObject gameObject)
		{
			UnityEngine.Object.Destroy(gameObject);
		}

		// Token: 0x0600015F RID: 351 RVA: 0x00008F1A File Offset: 0x0000711A
		public DefaultPool()
		{
		}

		// Token: 0x040000B8 RID: 184
		public readonly Dictionary<string, GameObject> ResourceCache = new Dictionary<string, GameObject>();
	}
}

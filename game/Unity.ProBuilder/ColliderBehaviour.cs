using System;
using UnityEngine.SceneManagement;

namespace UnityEngine.ProBuilder
{
	// Token: 0x0200000E RID: 14
	[DisallowMultipleComponent]
	internal sealed class ColliderBehaviour : EntityBehaviour
	{
		// Token: 0x06000078 RID: 120 RVA: 0x00004248 File Offset: 0x00002448
		public override void Initialize()
		{
			Collider collider = base.gameObject.GetComponent<Collider>();
			if (!collider)
			{
				collider = base.gameObject.AddComponent<MeshCollider>();
			}
			collider.isTrigger = false;
			base.SetMaterial(BuiltinMaterials.colliderMaterial);
		}

		// Token: 0x06000079 RID: 121 RVA: 0x00004288 File Offset: 0x00002488
		public override void OnEnterPlayMode()
		{
			Renderer component = base.GetComponent<Renderer>();
			if (component != null)
			{
				component.enabled = false;
			}
		}

		// Token: 0x0600007A RID: 122 RVA: 0x000042AC File Offset: 0x000024AC
		public override void OnSceneLoaded(Scene scene, LoadSceneMode mode)
		{
			Renderer component = base.GetComponent<Renderer>();
			if (component != null)
			{
				component.enabled = false;
			}
		}

		// Token: 0x0600007B RID: 123 RVA: 0x000042D0 File Offset: 0x000024D0
		public ColliderBehaviour()
		{
		}
	}
}

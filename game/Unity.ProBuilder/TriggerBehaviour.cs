using System;
using UnityEngine.SceneManagement;

namespace UnityEngine.ProBuilder
{
	// Token: 0x0200005E RID: 94
	[DisallowMultipleComponent]
	internal sealed class TriggerBehaviour : EntityBehaviour
	{
		// Token: 0x06000380 RID: 896 RVA: 0x000216DC File Offset: 0x0001F8DC
		public override void Initialize()
		{
			Collider collider = base.gameObject.GetComponent<Collider>();
			if (!collider)
			{
				collider = base.gameObject.AddComponent<MeshCollider>();
			}
			MeshCollider meshCollider = collider as MeshCollider;
			if (meshCollider)
			{
				meshCollider.convex = true;
			}
			collider.isTrigger = true;
			base.SetMaterial(BuiltinMaterials.triggerMaterial);
		}

		// Token: 0x06000381 RID: 897 RVA: 0x00021734 File Offset: 0x0001F934
		public override void OnEnterPlayMode()
		{
			Renderer renderer;
			if (base.TryGetComponent<Renderer>(out renderer))
			{
				renderer.enabled = false;
			}
		}

		// Token: 0x06000382 RID: 898 RVA: 0x00021754 File Offset: 0x0001F954
		public override void OnSceneLoaded(Scene scene, LoadSceneMode mode)
		{
			Renderer renderer;
			if (base.TryGetComponent<Renderer>(out renderer))
			{
				renderer.enabled = false;
			}
		}

		// Token: 0x06000383 RID: 899 RVA: 0x00021772 File Offset: 0x0001F972
		public TriggerBehaviour()
		{
		}
	}
}

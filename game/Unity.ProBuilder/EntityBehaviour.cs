using System;
using UnityEngine.SceneManagement;

namespace UnityEngine.ProBuilder
{
	// Token: 0x02000018 RID: 24
	internal abstract class EntityBehaviour : MonoBehaviour
	{
		// Token: 0x060000CD RID: 205
		public abstract void Initialize();

		// Token: 0x060000CE RID: 206
		public abstract void OnEnterPlayMode();

		// Token: 0x060000CF RID: 207
		public abstract void OnSceneLoaded(Scene scene, LoadSceneMode mode);

		// Token: 0x060000D0 RID: 208 RVA: 0x0000F7F3 File Offset: 0x0000D9F3
		protected void SetMaterial(Material material)
		{
			if (base.GetComponent<Renderer>())
			{
				base.GetComponent<Renderer>().sharedMaterial = material;
				return;
			}
			base.gameObject.AddComponent<MeshRenderer>().sharedMaterial = material;
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x0000F820 File Offset: 0x0000DA20
		protected EntityBehaviour()
		{
		}

		// Token: 0x04000051 RID: 81
		[Tooltip("Allow ProBuilder to automatically hide and show this object when entering or exiting play mode.")]
		public bool manageVisibility = true;
	}
}

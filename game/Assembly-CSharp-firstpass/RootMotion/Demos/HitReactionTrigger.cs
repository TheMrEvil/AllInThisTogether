using System;
using RootMotion.FinalIK;
using UnityEngine;

namespace RootMotion.Demos
{
	// Token: 0x0200013B RID: 315
	public class HitReactionTrigger : MonoBehaviour
	{
		// Token: 0x06000CF4 RID: 3316 RVA: 0x0005810C File Offset: 0x0005630C
		private void Update()
		{
			if (Input.GetMouseButtonDown(0))
			{
				Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
				RaycastHit raycastHit = default(RaycastHit);
				if (Physics.Raycast(ray, out raycastHit, 100f))
				{
					this.hitReaction.Hit(raycastHit.collider, ray.direction * this.hitForce, raycastHit.point);
					this.colliderName = raycastHit.collider.name;
				}
			}
		}

		// Token: 0x06000CF5 RID: 3317 RVA: 0x00058185 File Offset: 0x00056385
		private void OnGUI()
		{
			GUILayout.Label("LMB to shoot the Dummy, RMB to rotate the camera.", Array.Empty<GUILayoutOption>());
			if (this.colliderName != string.Empty)
			{
				GUILayout.Label("Last Bone Hit: " + this.colliderName, Array.Empty<GUILayoutOption>());
			}
		}

		// Token: 0x06000CF6 RID: 3318 RVA: 0x000581C2 File Offset: 0x000563C2
		public HitReactionTrigger()
		{
		}

		// Token: 0x04000A8B RID: 2699
		public HitReaction hitReaction;

		// Token: 0x04000A8C RID: 2700
		public float hitForce = 1f;

		// Token: 0x04000A8D RID: 2701
		private string colliderName;
	}
}

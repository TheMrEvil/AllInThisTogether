using System;
using RootMotion.FinalIK;
using UnityEngine;

namespace RootMotion.Demos
{
	// Token: 0x02000154 RID: 340
	public class HitReactionVRIKTrigger : MonoBehaviour
	{
		// Token: 0x06000D50 RID: 3408 RVA: 0x00059F00 File Offset: 0x00058100
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

		// Token: 0x06000D51 RID: 3409 RVA: 0x00059F79 File Offset: 0x00058179
		private void OnGUI()
		{
			GUILayout.Label("LMB to shoot the Dummy, RMB to rotate the camera.", Array.Empty<GUILayoutOption>());
			if (this.colliderName != string.Empty)
			{
				GUILayout.Label("Last Bone Hit: " + this.colliderName, Array.Empty<GUILayoutOption>());
			}
		}

		// Token: 0x06000D52 RID: 3410 RVA: 0x00059FB6 File Offset: 0x000581B6
		public HitReactionVRIKTrigger()
		{
		}

		// Token: 0x04000AFF RID: 2815
		public HitReactionVRIK hitReaction;

		// Token: 0x04000B00 RID: 2816
		public float hitForce = 1f;

		// Token: 0x04000B01 RID: 2817
		private string colliderName;
	}
}

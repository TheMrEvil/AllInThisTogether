using System;
using RootMotion.FinalIK;
using UnityEngine;

namespace RootMotion.Demos
{
	// Token: 0x02000148 RID: 328
	public class RagdollUtilityDemo : MonoBehaviour
	{
		// Token: 0x06000D25 RID: 3365 RVA: 0x0005955E File Offset: 0x0005775E
		private void OnGUI()
		{
			GUILayout.Label(" Press R to switch to ragdoll. \n Weigh in one of the FBBIK effectors to make kinematic changes to the ragdoll pose.\n A to blend back to animation", Array.Empty<GUILayoutOption>());
		}

		// Token: 0x06000D26 RID: 3366 RVA: 0x00059570 File Offset: 0x00057770
		private void Update()
		{
			if (Input.GetKeyDown(KeyCode.R))
			{
				this.ragdollUtility.EnableRagdoll();
			}
			if (Input.GetKeyDown(KeyCode.A))
			{
				Vector3 b = this.pelvis.position - this.root.position;
				this.root.position += b;
				this.pelvis.transform.position -= b;
				this.ragdollUtility.DisableRagdoll();
			}
		}

		// Token: 0x06000D27 RID: 3367 RVA: 0x000595F4 File Offset: 0x000577F4
		public RagdollUtilityDemo()
		{
		}

		// Token: 0x04000AD4 RID: 2772
		public RagdollUtility ragdollUtility;

		// Token: 0x04000AD5 RID: 2773
		public Transform root;

		// Token: 0x04000AD6 RID: 2774
		public Rigidbody pelvis;
	}
}

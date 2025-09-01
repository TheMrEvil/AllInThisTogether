using System;
using RootMotion.FinalIK;
using UnityEngine;

namespace RootMotion.Demos
{
	// Token: 0x0200013D RID: 317
	public class InteractionC2CDemo : MonoBehaviour
	{
		// Token: 0x06000CFA RID: 3322 RVA: 0x000583B8 File Offset: 0x000565B8
		private void OnGUI()
		{
			if (GUILayout.Button("Shake Hands", Array.Empty<GUILayoutOption>()))
			{
				this.character1.StartInteraction(FullBodyBipedEffector.RightHand, this.handShake, true);
				this.character2.StartInteraction(FullBodyBipedEffector.RightHand, this.handShake, true);
			}
		}

		// Token: 0x06000CFB RID: 3323 RVA: 0x000583F4 File Offset: 0x000565F4
		private void LateUpdate()
		{
			Vector3 position = Vector3.Lerp(this.character1.ik.solver.rightHandEffector.bone.position, this.character2.ik.solver.rightHandEffector.bone.position, 0.5f);
			this.handShake.transform.position = position;
		}

		// Token: 0x06000CFC RID: 3324 RVA: 0x0005845B File Offset: 0x0005665B
		public InteractionC2CDemo()
		{
		}

		// Token: 0x04000A96 RID: 2710
		public InteractionSystem character1;

		// Token: 0x04000A97 RID: 2711
		public InteractionSystem character2;

		// Token: 0x04000A98 RID: 2712
		public InteractionObject handShake;
	}
}

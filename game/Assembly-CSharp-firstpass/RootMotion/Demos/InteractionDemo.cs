using System;
using RootMotion.FinalIK;
using UnityEngine;

namespace RootMotion.Demos
{
	// Token: 0x0200013E RID: 318
	public class InteractionDemo : MonoBehaviour
	{
		// Token: 0x06000CFD RID: 3325 RVA: 0x00058464 File Offset: 0x00056664
		private void OnGUI()
		{
			this.interrupt = GUILayout.Toggle(this.interrupt, "Interrupt", Array.Empty<GUILayoutOption>());
			if (this.isSitting)
			{
				if (!this.interactionSystem.inInteraction && GUILayout.Button("Stand Up", Array.Empty<GUILayoutOption>()))
				{
					this.interactionSystem.ResumeAll();
					this.isSitting = false;
				}
				return;
			}
			if (GUILayout.Button("Pick Up Ball", Array.Empty<GUILayoutOption>()))
			{
				this.interactionSystem.StartInteraction(FullBodyBipedEffector.RightHand, this.ball, this.interrupt);
			}
			if (GUILayout.Button("Button Left Hand", Array.Empty<GUILayoutOption>()))
			{
				this.interactionSystem.StartInteraction(FullBodyBipedEffector.LeftHand, this.button, this.interrupt);
			}
			if (GUILayout.Button("Button Right Hand", Array.Empty<GUILayoutOption>()))
			{
				this.interactionSystem.StartInteraction(FullBodyBipedEffector.RightHand, this.button, this.interrupt);
			}
			if (GUILayout.Button("Put Out Cigarette", Array.Empty<GUILayoutOption>()))
			{
				this.interactionSystem.StartInteraction(FullBodyBipedEffector.RightFoot, this.cigarette, this.interrupt);
			}
			if (GUILayout.Button("Open Door", Array.Empty<GUILayoutOption>()))
			{
				this.interactionSystem.StartInteraction(FullBodyBipedEffector.LeftHand, this.door, this.interrupt);
			}
			if (!this.interactionSystem.inInteraction && GUILayout.Button("Sit Down", Array.Empty<GUILayoutOption>()))
			{
				this.interactionSystem.StartInteraction(FullBodyBipedEffector.Body, this.benchMain, this.interrupt);
				this.interactionSystem.StartInteraction(FullBodyBipedEffector.LeftThigh, this.benchMain, this.interrupt);
				this.interactionSystem.StartInteraction(FullBodyBipedEffector.RightThigh, this.benchMain, this.interrupt);
				this.interactionSystem.StartInteraction(FullBodyBipedEffector.LeftFoot, this.benchMain, this.interrupt);
				this.interactionSystem.StartInteraction(FullBodyBipedEffector.LeftHand, this.benchHands, this.interrupt);
				this.interactionSystem.StartInteraction(FullBodyBipedEffector.RightHand, this.benchHands, this.interrupt);
				this.isSitting = true;
			}
		}

		// Token: 0x06000CFE RID: 3326 RVA: 0x00058658 File Offset: 0x00056858
		public InteractionDemo()
		{
		}

		// Token: 0x04000A99 RID: 2713
		public InteractionSystem interactionSystem;

		// Token: 0x04000A9A RID: 2714
		public bool interrupt;

		// Token: 0x04000A9B RID: 2715
		public InteractionObject ball;

		// Token: 0x04000A9C RID: 2716
		public InteractionObject benchMain;

		// Token: 0x04000A9D RID: 2717
		public InteractionObject benchHands;

		// Token: 0x04000A9E RID: 2718
		public InteractionObject button;

		// Token: 0x04000A9F RID: 2719
		public InteractionObject cigarette;

		// Token: 0x04000AA0 RID: 2720
		public InteractionObject door;

		// Token: 0x04000AA1 RID: 2721
		private bool isSitting;
	}
}

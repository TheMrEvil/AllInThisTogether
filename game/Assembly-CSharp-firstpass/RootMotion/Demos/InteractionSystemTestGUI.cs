using System;
using RootMotion.FinalIK;
using UnityEngine;

namespace RootMotion.Demos
{
	// Token: 0x0200013F RID: 319
	public class InteractionSystemTestGUI : MonoBehaviour
	{
		// Token: 0x06000CFF RID: 3327 RVA: 0x00058660 File Offset: 0x00056860
		private void Awake()
		{
			this.interactionSystem = base.GetComponent<InteractionSystem>();
		}

		// Token: 0x06000D00 RID: 3328 RVA: 0x00058670 File Offset: 0x00056870
		private void OnGUI()
		{
			if (this.interactionSystem == null)
			{
				return;
			}
			if (GUILayout.Button("Start Interaction With " + this.interactionObject.name, Array.Empty<GUILayoutOption>()))
			{
				if (this.effectors.Length == 0)
				{
					Debug.Log("Please select the effectors to interact with.");
				}
				foreach (FullBodyBipedEffector effectorType in this.effectors)
				{
					this.interactionSystem.StartInteraction(effectorType, this.interactionObject, true);
				}
			}
			if (this.effectors.Length == 0)
			{
				return;
			}
			if (this.interactionSystem.IsPaused(this.effectors[0]) && GUILayout.Button("Resume Interaction With " + this.interactionObject.name, Array.Empty<GUILayoutOption>()))
			{
				this.interactionSystem.ResumeAll();
			}
		}

		// Token: 0x06000D01 RID: 3329 RVA: 0x00058738 File Offset: 0x00056938
		public InteractionSystemTestGUI()
		{
		}

		// Token: 0x04000AA2 RID: 2722
		[Tooltip("The object to interact to")]
		public InteractionObject interactionObject;

		// Token: 0x04000AA3 RID: 2723
		[Tooltip("The effectors to interact with")]
		public FullBodyBipedEffector[] effectors;

		// Token: 0x04000AA4 RID: 2724
		private InteractionSystem interactionSystem;
	}
}

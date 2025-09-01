using System;
using RootMotion.FinalIK;
using UnityEngine;

namespace RootMotion.Demos
{
	// Token: 0x0200014F RID: 335
	public class UserControlInteractions : UserControlThirdPerson
	{
		// Token: 0x06000D3F RID: 3391 RVA: 0x00059AB4 File Offset: 0x00057CB4
		protected override void Update()
		{
			if (this.disableInputInInteraction && this.interactionSystem != null && (this.interactionSystem.inInteraction || this.interactionSystem.IsPaused()))
			{
				float minActiveProgress = this.interactionSystem.GetMinActiveProgress();
				if (minActiveProgress > 0f && minActiveProgress < this.enableInputAtProgress)
				{
					this.state.move = Vector3.zero;
					this.state.jump = false;
					return;
				}
			}
			base.Update();
		}

		// Token: 0x06000D40 RID: 3392 RVA: 0x00059B34 File Offset: 0x00057D34
		private void OnGUI()
		{
			if (!this.character.onGround)
			{
				return;
			}
			if (this.interactionSystem.IsPaused() && this.interactionSystem.IsInSync())
			{
				GUILayout.Label("Press E to resume interaction", Array.Empty<GUILayoutOption>());
				if (Input.GetKey(KeyCode.E))
				{
					this.interactionSystem.ResumeAll();
				}
				return;
			}
			int closestTriggerIndex = this.interactionSystem.GetClosestTriggerIndex();
			if (closestTriggerIndex == -1)
			{
				return;
			}
			if (!this.interactionSystem.TriggerEffectorsReady(closestTriggerIndex))
			{
				return;
			}
			GUILayout.Label("Press E to start interaction", Array.Empty<GUILayoutOption>());
			if (Input.GetKey(KeyCode.E))
			{
				this.interactionSystem.TriggerInteraction(closestTriggerIndex, false);
			}
		}

		// Token: 0x06000D41 RID: 3393 RVA: 0x00059BD3 File Offset: 0x00057DD3
		public UserControlInteractions()
		{
		}

		// Token: 0x04000AEA RID: 2794
		public CharacterThirdPerson character;

		// Token: 0x04000AEB RID: 2795
		public InteractionSystem interactionSystem;

		// Token: 0x04000AEC RID: 2796
		public bool disableInputInInteraction = true;

		// Token: 0x04000AED RID: 2797
		public float enableInputAtProgress = 0.8f;
	}
}

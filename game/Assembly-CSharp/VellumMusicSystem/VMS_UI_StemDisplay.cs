using System;
using UnityEngine;
using UnityEngine.UI;

namespace VellumMusicSystem
{
	// Token: 0x020003C1 RID: 961
	public class VMS_UI_StemDisplay : MonoBehaviour
	{
		// Token: 0x06001FAB RID: 8107 RVA: 0x000BC779 File Offset: 0x000BA979
		private void Awake()
		{
			this.image = base.GetComponent<Image>();
		}

		// Token: 0x06001FAC RID: 8108 RVA: 0x000BC787 File Offset: 0x000BA987
		private void OnEnable()
		{
			VMS_Player.OnScheduleNewClips += this.ResetDisplay;
			VMS_ClipSet.OnGetClip = (Action<VMS_GetClipEvent>)Delegate.Combine(VMS_ClipSet.OnGetClip, new Action<VMS_GetClipEvent>(this.UpdateDisplay));
		}

		// Token: 0x06001FAD RID: 8109 RVA: 0x000BC7BA File Offset: 0x000BA9BA
		private void OnDisable()
		{
			VMS_Player.OnScheduleNewClips -= this.ResetDisplay;
			VMS_ClipSet.OnGetClip = (Action<VMS_GetClipEvent>)Delegate.Remove(VMS_ClipSet.OnGetClip, new Action<VMS_GetClipEvent>(this.UpdateDisplay));
		}

		// Token: 0x06001FAE RID: 8110 RVA: 0x000BC7ED File Offset: 0x000BA9ED
		private void ResetDisplay()
		{
			this.image.color = this.offColour;
		}

		// Token: 0x06001FAF RID: 8111 RVA: 0x000BC800 File Offset: 0x000BAA00
		private void UpdateDisplay(VMS_GetClipEvent clipEvent)
		{
			if (clipEvent.stemType == this.myStemStype && clipEvent.section == this.mySection)
			{
				this.image.color = clipEvent.clipSet.clipSetColor;
				return;
			}
		}

		// Token: 0x06001FB0 RID: 8112 RVA: 0x000BC835 File Offset: 0x000BAA35
		public VMS_UI_StemDisplay()
		{
		}

		// Token: 0x04001FD7 RID: 8151
		private Color offColour = Color.gray;

		// Token: 0x04001FD8 RID: 8152
		[SerializeField]
		private VMS_StemType myStemStype;

		// Token: 0x04001FD9 RID: 8153
		[SerializeField]
		private int mySection;

		// Token: 0x04001FDA RID: 8154
		private Image image;
	}
}

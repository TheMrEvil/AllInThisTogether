using System;
using UnityEngine;
using UnityEngine.UI;

namespace VellumMusicSystem
{
	// Token: 0x020003BE RID: 958
	public class VMS_UI_DisableButton : MonoBehaviour
	{
		// Token: 0x06001F99 RID: 8089 RVA: 0x000BC475 File Offset: 0x000BA675
		private void Awake()
		{
			this.button = base.GetComponent<Button>();
		}

		// Token: 0x06001F9A RID: 8090 RVA: 0x000BC484 File Offset: 0x000BA684
		private void OnEnable()
		{
			if (this.myClipSet == null)
			{
				this.button.image.color = Color.grey;
				this.button.interactable = false;
				base.enabled = false;
				return;
			}
			this.button.image.color = this.myClipSet.clipSetColor;
			VMS_Player.OnNewClipSetChosen += this.UpdateButton;
		}

		// Token: 0x06001F9B RID: 8091 RVA: 0x000BC4F4 File Offset: 0x000BA6F4
		private void OnDisable()
		{
			VMS_Player.OnNewClipSetChosen -= this.UpdateButton;
		}

		// Token: 0x06001F9C RID: 8092 RVA: 0x000BC507 File Offset: 0x000BA707
		private void UpdateButton(VMS_ClipSet newClipSet)
		{
			if (newClipSet == this.myClipSet)
			{
				this.button.interactable = false;
				return;
			}
			this.button.interactable = true;
		}

		// Token: 0x06001F9D RID: 8093 RVA: 0x000BC530 File Offset: 0x000BA730
		public VMS_UI_DisableButton()
		{
		}

		// Token: 0x04001FD0 RID: 8144
		private Button button;

		// Token: 0x04001FD1 RID: 8145
		[SerializeField]
		private VMS_ClipSet myClipSet;
	}
}

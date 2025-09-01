using System;
using UnityEngine;
using UnityEngine.UI;

namespace VellumMusicSystem
{
	// Token: 0x020003BF RID: 959
	public class VMS_UI_PlayStateButtonDIsplay : MonoBehaviour
	{
		// Token: 0x06001F9E RID: 8094 RVA: 0x000BC538 File Offset: 0x000BA738
		private void Awake()
		{
			this.button = base.GetComponent<Button>();
		}

		// Token: 0x06001F9F RID: 8095 RVA: 0x000BC546 File Offset: 0x000BA746
		private void OnEnable()
		{
			VMS_Player.OnMusicPlayerStart += this.ToggleOnStart;
			VMS_Player.OnMusicPlayerStopping += this.ToggleOnStop;
		}

		// Token: 0x06001FA0 RID: 8096 RVA: 0x000BC56A File Offset: 0x000BA76A
		private void OnDisable()
		{
			VMS_Player.OnMusicPlayerStart -= this.ToggleOnStart;
			VMS_Player.OnMusicPlayerStopping -= this.ToggleOnStop;
		}

		// Token: 0x06001FA1 RID: 8097 RVA: 0x000BC58E File Offset: 0x000BA78E
		private void ToggleOnStart()
		{
			if (this.disableOnPlay)
			{
				this.button.interactable = false;
				return;
			}
			this.button.interactable = true;
		}

		// Token: 0x06001FA2 RID: 8098 RVA: 0x000BC5B1 File Offset: 0x000BA7B1
		private void ToggleOnStop()
		{
			if (this.disableOnPlay)
			{
				this.button.interactable = true;
				return;
			}
			this.button.interactable = false;
		}

		// Token: 0x06001FA3 RID: 8099 RVA: 0x000BC5D4 File Offset: 0x000BA7D4
		public VMS_UI_PlayStateButtonDIsplay()
		{
		}

		// Token: 0x04001FD2 RID: 8146
		private Button button;

		// Token: 0x04001FD3 RID: 8147
		[SerializeField]
		private bool disableOnPlay;
	}
}

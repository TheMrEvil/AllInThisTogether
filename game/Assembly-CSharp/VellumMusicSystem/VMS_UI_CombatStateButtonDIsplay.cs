using System;
using UnityEngine;
using UnityEngine.UI;

namespace VellumMusicSystem
{
	// Token: 0x020003BD RID: 957
	public class VMS_UI_CombatStateButtonDIsplay : MonoBehaviour
	{
		// Token: 0x06001F94 RID: 8084 RVA: 0x000BC3F9 File Offset: 0x000BA5F9
		private void Awake()
		{
			this.button = base.GetComponent<Button>();
		}

		// Token: 0x06001F95 RID: 8085 RVA: 0x000BC407 File Offset: 0x000BA607
		private void OnEnable()
		{
			VMS_Player.OnMenuStateChange += this.ToggleDisplay;
		}

		// Token: 0x06001F96 RID: 8086 RVA: 0x000BC41A File Offset: 0x000BA61A
		private void OnDisable()
		{
			VMS_Player.OnMenuStateChange -= this.ToggleDisplay;
		}

		// Token: 0x06001F97 RID: 8087 RVA: 0x000BC42D File Offset: 0x000BA62D
		private void ToggleDisplay(bool inMenu)
		{
			if (this.disableInMenu && !inMenu)
			{
				this.button.interactable = true;
				return;
			}
			if (!this.disableInMenu && inMenu)
			{
				this.button.interactable = true;
				return;
			}
			this.button.interactable = false;
		}

		// Token: 0x06001F98 RID: 8088 RVA: 0x000BC46D File Offset: 0x000BA66D
		public VMS_UI_CombatStateButtonDIsplay()
		{
		}

		// Token: 0x04001FCE RID: 8142
		private Button button;

		// Token: 0x04001FCF RID: 8143
		[SerializeField]
		private bool disableInMenu;
	}
}

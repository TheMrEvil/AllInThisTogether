using System;
using UnityEngine;

// Token: 0x020001A9 RID: 425
public class FishingUIControl : MonoBehaviour
{
	// Token: 0x060011A8 RID: 4520 RVA: 0x0006DA88 File Offset: 0x0006BC88
	private void Update()
	{
		if (!MapManager.InLobbyScene || PlayerControl.myInstance == null)
		{
			this.Fader.alpha = 0f;
			return;
		}
		this.Fader.alpha = 1f;
		this.FishPrompt.UpdateOpacity(Fishing.CanStartFishing, 4f, true);
		this.PullPrompt.UpdateOpacity(Fishing.isFishing, 4f, true);
	}

	// Token: 0x060011A9 RID: 4521 RVA: 0x0006DAF6 File Offset: 0x0006BCF6
	public FishingUIControl()
	{
	}

	// Token: 0x04001051 RID: 4177
	public CanvasGroup Fader;

	// Token: 0x04001052 RID: 4178
	public CanvasGroup FishPrompt;

	// Token: 0x04001053 RID: 4179
	public CanvasGroup PullPrompt;
}

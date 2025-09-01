using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200016E RID: 366
public class EndRunButton : MonoBehaviour
{
	// Token: 0x06000FC1 RID: 4033 RVA: 0x00063628 File Offset: 0x00061828
	public void TickUpdate(bool InMPRoom)
	{
		if (!InMPRoom)
		{
			this.FillDisplay.alpha = 0f;
			return;
		}
		if (this.isPressed)
		{
			this.HoldT += Time.deltaTime;
		}
		else
		{
			this.HoldT = 0f;
		}
		this.FillDisplay.UpdateOpacity(this.HoldT > 0f, 3f, true);
		this.HoldFill.fillAmount = this.HoldT / Mathf.Max(this.HoldDuration, 0.1f);
		if (this.HoldT >= this.HoldDuration)
		{
			PausePanel.instance.LeaveGame();
			this.HoldT = 0f;
			this.isPressed = false;
			this.HoldFill.fillAmount = 0f;
		}
	}

	// Token: 0x06000FC2 RID: 4034 RVA: 0x000636EA File Offset: 0x000618EA
	public void ButtonDown()
	{
		this.isPressed = true;
	}

	// Token: 0x06000FC3 RID: 4035 RVA: 0x000636F3 File Offset: 0x000618F3
	public void ButtonUp()
	{
		this.isPressed = false;
	}

	// Token: 0x06000FC4 RID: 4036 RVA: 0x000636FC File Offset: 0x000618FC
	public EndRunButton()
	{
	}

	// Token: 0x04000DDD RID: 3549
	public Button ButtonRef;

	// Token: 0x04000DDE RID: 3550
	public CanvasGroup FillDisplay;

	// Token: 0x04000DDF RID: 3551
	public Image HoldFill;

	// Token: 0x04000DE0 RID: 3552
	private float HoldT;

	// Token: 0x04000DE1 RID: 3553
	public float HoldDuration = 2f;

	// Token: 0x04000DE2 RID: 3554
	private bool isPressed;
}

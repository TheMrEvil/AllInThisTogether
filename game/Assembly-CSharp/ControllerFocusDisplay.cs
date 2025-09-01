using System;
using UnityEngine;

// Token: 0x0200014B RID: 331
public class ControllerFocusDisplay : MonoBehaviour
{
	// Token: 0x06000EDD RID: 3805 RVA: 0x0005EAC4 File Offset: 0x0005CCC4
	public void SetCurrentFocus(bool isLeft)
	{
		if (this.firstSetup && isLeft == this.curIsLeft)
		{
			return;
		}
		this.firstSetup = true;
		this.curIsLeft = isLeft;
		this.FocusLeft_On.SetActive(isLeft);
		this.FocusLeft_Off.SetActive(!isLeft);
		this.FocusRight_Off.SetActive(isLeft);
		this.FocusRight_On.SetActive(!isLeft);
		AudioManager.PlayUISecondaryAction();
	}

	// Token: 0x06000EDE RID: 3806 RVA: 0x0005EB2C File Offset: 0x0005CD2C
	public ControllerFocusDisplay()
	{
	}

	// Token: 0x04000C8B RID: 3211
	public GameObject FocusLeft_On;

	// Token: 0x04000C8C RID: 3212
	public GameObject FocusLeft_Off;

	// Token: 0x04000C8D RID: 3213
	public GameObject FocusRight_On;

	// Token: 0x04000C8E RID: 3214
	public GameObject FocusRight_Off;

	// Token: 0x04000C8F RID: 3215
	private bool firstSetup;

	// Token: 0x04000C90 RID: 3216
	private bool curIsLeft;
}

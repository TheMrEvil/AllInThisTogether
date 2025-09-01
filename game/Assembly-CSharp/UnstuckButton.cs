using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Token: 0x02000175 RID: 373
public class UnstuckButton : MonoBehaviour
{
	// Token: 0x06000FE9 RID: 4073 RVA: 0x0006431C File Offset: 0x0006251C
	public void TickUpdate()
	{
		if (this.isPressed)
		{
			this.HoldT += Time.deltaTime;
		}
		else
		{
			this.HoldT = 0f;
		}
		this.HoldFill.fillAmount = this.HoldT / Mathf.Max(this.HoldDuration, 0.1f);
		if (this.HoldT >= this.HoldDuration)
		{
			this.Unstuck();
			this.HoldT = 0f;
			this.isPressed = false;
			this.HoldFill.fillAmount = 0f;
		}
	}

	// Token: 0x06000FEA RID: 4074 RVA: 0x000643A8 File Offset: 0x000625A8
	public void ButtonDown()
	{
		this.isPressed = true;
	}

	// Token: 0x06000FEB RID: 4075 RVA: 0x000643B1 File Offset: 0x000625B1
	public void ButtonUp()
	{
		this.isPressed = false;
	}

	// Token: 0x06000FEC RID: 4076 RVA: 0x000643BC File Offset: 0x000625BC
	private void Unstuck()
	{
		EventSystem.current.SetSelectedGameObject(null);
		base.GetComponent<ButtonGroupFader>().HideImmediate();
		PausePanel.instance.Resume();
		if (RaidManager.IsInRaid && RaidManager.IsEncounterStarted && RaidScene.instance != null)
		{
			RaidScene.instance.UnstuckPlayer();
			return;
		}
		PlayerControl.myInstance.ResetPosition(true);
		PlayerControl.myInstance.Movement.mover.RecalculateColliderDimensions();
		if (Library_RaidControl.instance != null && Library_RaidControl.IsInAntechamber)
		{
			Library_RaidControl.instance.Reset();
		}
	}

	// Token: 0x06000FED RID: 4077 RVA: 0x0006444C File Offset: 0x0006264C
	public UnstuckButton()
	{
	}

	// Token: 0x04000E03 RID: 3587
	public Button ButtonRef;

	// Token: 0x04000E04 RID: 3588
	public Image HoldFill;

	// Token: 0x04000E05 RID: 3589
	private float HoldT;

	// Token: 0x04000E06 RID: 3590
	public float HoldDuration = 2f;

	// Token: 0x04000E07 RID: 3591
	private bool isPressed;
}

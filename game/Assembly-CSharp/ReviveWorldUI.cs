using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020001C6 RID: 454
public class ReviveWorldUI : MonoBehaviour
{
	// Token: 0x06001296 RID: 4758 RVA: 0x00072958 File Offset: 0x00070B58
	private void Awake()
	{
		this.canvasGroup = base.GetComponent<CanvasGroup>();
		this.canvasGroup.alpha = 0f;
		this.rect = base.GetComponent<RectTransform>();
		this.canvas = base.GetComponentInParent<Canvas>().GetComponentInParent<RectTransform>();
	}

	// Token: 0x06001297 RID: 4759 RVA: 0x00072994 File Offset: 0x00070B94
	private void Update()
	{
		this.canvasGroup.UpdateOpacity(PickupManager.ClosestGhost != null, 8f, false);
		if (PickupManager.ClosestGhost != this.selected)
		{
			this.SetupDisplay(PickupManager.ClosestGhost);
		}
		if (PickupManager.ClosestGhost != null)
		{
			this.rect.FollowWorldObject(this.selected.UIRoot, this.canvas, WorldIndicators.NextIndex(), 0f);
		}
		if (this.reviveT > 0f)
		{
			this.reviveT -= Time.deltaTime;
		}
		if (this.selected == null)
		{
			return;
		}
		this.FillDisplay.fillAmount = Mathf.Lerp(this.FillDisplay.fillAmount, this.selected.playerControl.Health.ReviveProgress, Time.deltaTime * 6f);
		if (this.selected.playerControl.Health.ReviveProgress >= 1f && this.reviveT <= 0f)
		{
			this.Revive();
		}
	}

	// Token: 0x06001298 RID: 4760 RVA: 0x00072AA6 File Offset: 0x00070CA6
	private void Revive()
	{
		this.reviveT = this.reviveCD;
		PlayerControl.myInstance.PStats.IncreaseCounts(PlayerStat.Revives, 1);
	}

	// Token: 0x06001299 RID: 4761 RVA: 0x00072AC8 File Offset: 0x00070CC8
	private void SetupDisplay(GhostPlayerDisplay disp)
	{
		this.selected = disp;
		if (this.selected == null)
		{
			return;
		}
		TMP_Text titleText = this.TitleText;
		string str = "Reviving ";
		PlayerControl playerControl = this.selected.playerControl;
		titleText.text = str + ((playerControl != null) ? playerControl.GetUsernameText() : null);
	}

	// Token: 0x0600129A RID: 4762 RVA: 0x00072B17 File Offset: 0x00070D17
	public ReviveWorldUI()
	{
	}

	// Token: 0x04001197 RID: 4503
	private CanvasGroup canvasGroup;

	// Token: 0x04001198 RID: 4504
	private RectTransform canvas;

	// Token: 0x04001199 RID: 4505
	private RectTransform rect;

	// Token: 0x0400119A RID: 4506
	public Image FillDisplay;

	// Token: 0x0400119B RID: 4507
	public TextMeshProUGUI TitleText;

	// Token: 0x0400119C RID: 4508
	private GhostPlayerDisplay selected;

	// Token: 0x0400119D RID: 4509
	private float reviveCD = 5f;

	// Token: 0x0400119E RID: 4510
	private float reviveT;
}

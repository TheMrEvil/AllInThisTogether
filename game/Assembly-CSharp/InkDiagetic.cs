using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020001B6 RID: 438
public class InkDiagetic : MonoBehaviour
{
	// Token: 0x0600120E RID: 4622 RVA: 0x0006FE8A File Offset: 0x0006E08A
	private void Awake()
	{
		this.canvasGroup = base.GetComponent<CanvasGroup>();
		this.canvasGroup.alpha = 0f;
		this.rect = base.GetComponent<RectTransform>();
	}

	// Token: 0x0600120F RID: 4623 RVA: 0x0006FEB4 File Offset: 0x0006E0B4
	private void LateUpdate()
	{
		if (this.selected != null)
		{
			this.UpdateValues(this.selected.StoreOption);
			if (PlayerControl.myInstance != null)
			{
				if (PlayerControl.myInstance.Input.interactPressed && !this.wasInteractPressed)
				{
					this.wasInteractPressed = true;
					this.TryInvest();
					return;
				}
				if (!PlayerControl.myInstance.Input.interactPressed)
				{
					this.wasInteractPressed = false;
				}
			}
		}
	}

	// Token: 0x06001210 RID: 4624 RVA: 0x0006FF2C File Offset: 0x0006E12C
	public void TryInvest()
	{
		if (Time.realtimeSinceStartup - this.lastInvestTime < 0.25f)
		{
			return;
		}
		if (InkManager.MyShards <= 0)
		{
			return;
		}
		InkManager.instance.TryInvest(this.selected.StoreOption, Mathf.Min(10, InkManager.MyShards));
		this.lastInvestTime = Time.realtimeSinceStartup;
	}

	// Token: 0x06001211 RID: 4625 RVA: 0x0006FF84 File Offset: 0x0006E184
	private void UpdateValues(InkTalent p)
	{
		if (this.cVal != p.CurrentValue)
		{
			this.ProgressBar.fillAmount = (float)p.CurrentValue / (float)p.Cost;
			this.ProgressLabel.text = p.CurrentValue.ToString() + "/" + p.Cost.ToString();
			this.cVal = p.CurrentValue;
		}
	}

	// Token: 0x06001212 RID: 4626 RVA: 0x0006FFF4 File Offset: 0x0006E1F4
	private void SetupDisplay(InkTrigger ink)
	{
		this.selected = ink;
		if (this.selected == null || ink.Augment == null)
		{
			return;
		}
		this.ActionIcon.sprite = ink.Augment.Icon;
		this.TitleText.text = ink.Augment.Name;
		this.TitleText.color = GameDB.Quality(ink.Augment.DisplayQuality).PlayerColor;
		this.DetailText.text = ink.Augment.Detail;
		this.cVal = -1;
		this.wasInteractPressed = true;
	}

	// Token: 0x06001213 RID: 4627 RVA: 0x00070095 File Offset: 0x0006E295
	public InkDiagetic()
	{
	}

	// Token: 0x040010DD RID: 4317
	private CanvasGroup canvasGroup;

	// Token: 0x040010DE RID: 4318
	public RectTransform canvas;

	// Token: 0x040010DF RID: 4319
	private RectTransform rect;

	// Token: 0x040010E0 RID: 4320
	private InkTrigger selected;

	// Token: 0x040010E1 RID: 4321
	public Image ActionIcon;

	// Token: 0x040010E2 RID: 4322
	public TextMeshProUGUI TitleText;

	// Token: 0x040010E3 RID: 4323
	public TextMeshProUGUI DetailText;

	// Token: 0x040010E4 RID: 4324
	[Header("Interaction Displays")]
	public Image ProgressBar;

	// Token: 0x040010E5 RID: 4325
	public TextMeshProUGUI ProgressLabel;

	// Token: 0x040010E6 RID: 4326
	private int cVal;

	// Token: 0x040010E7 RID: 4327
	private float lastInvestTime;

	// Token: 0x040010E8 RID: 4328
	private bool wasInteractPressed;
}

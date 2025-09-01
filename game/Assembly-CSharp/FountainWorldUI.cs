using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020001AF RID: 431
public class FountainWorldUI : MonoBehaviour
{
	// Token: 0x17000149 RID: 329
	// (get) Token: 0x060011D4 RID: 4564 RVA: 0x0006EABD File Offset: 0x0006CCBD
	public static bool InFountainRange
	{
		get
		{
			return Fountain.instance != null && FountainInteraction.instance.PlayerInside && FountainWorldUI.CanInteract;
		}
	}

	// Token: 0x060011D5 RID: 4565 RVA: 0x0006EAE0 File Offset: 0x0006CCE0
	private void Awake()
	{
		this.canvasGroup = base.GetComponent<CanvasGroup>();
		this.canvasGroup.alpha = 0f;
		FountainWorldUI.instance = this;
		FountainWorldUI.CanInteract = false;
		this.rect = base.GetComponent<RectTransform>();
		this.canvas = base.GetComponentInParent<Canvas>().GetComponentInParent<RectTransform>();
	}

	// Token: 0x060011D6 RID: 4566 RVA: 0x0006EB34 File Offset: 0x0006CD34
	private void LateUpdate()
	{
		bool flag = FountainWorldUI.InFountainRange && GameplayManager.CurState != GameState.InWave;
		this.canvasGroup.UpdateOpacity(flag, 4f, false);
		if (!flag)
		{
			return;
		}
		this.rect.FollowWorldObject(Fountain.instance.Indicator.transform, this.canvas, WorldIndicators.NextIndex(), 0f);
		if (Fountain.instance.Interaction.InteractTime > 0f)
		{
			this.InteractFill.fillAmount = Fountain.instance.Interaction.HoldTimer / Fountain.instance.Interaction.InteractTime;
			return;
		}
		this.InteractFill.fillAmount = 0f;
	}

	// Token: 0x060011D7 RID: 4567 RVA: 0x0006EBE9 File Offset: 0x0006CDE9
	public static void SetReason(string Reason)
	{
		FountainWorldUI.instance.TitleText.text = Reason;
	}

	// Token: 0x060011D8 RID: 4568 RVA: 0x0006EBFB File Offset: 0x0006CDFB
	public FountainWorldUI()
	{
	}

	// Token: 0x0400108B RID: 4235
	private CanvasGroup canvasGroup;

	// Token: 0x0400108C RID: 4236
	private RectTransform canvas;

	// Token: 0x0400108D RID: 4237
	private RectTransform rect;

	// Token: 0x0400108E RID: 4238
	public TextMeshProUGUI TitleText;

	// Token: 0x0400108F RID: 4239
	public Image InteractFill;

	// Token: 0x04001090 RID: 4240
	public static FountainWorldUI instance;

	// Token: 0x04001091 RID: 4241
	public static bool CanInteract;
}

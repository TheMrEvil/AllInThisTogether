using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Token: 0x020001AA RID: 426
public class FountainStoreItemUI : MonoBehaviour, ISelectHandler, IEventSystemHandler
{
	// Token: 0x060011AA RID: 4522 RVA: 0x0006DB00 File Offset: 0x0006BD00
	public void Setup(InkTalent option)
	{
		this.Option = option;
		this.ColorIcon.sprite = option.Augment.Root.Icon;
		this.SepiaIcon.sprite = option.Augment.Root.Icon;
		this.SetupPoints();
		this.UpdateDisplay(true);
		this.TomeSpecificBorder.SetActive(this.Option.Augment.Root.Tome != null);
		UIPingable component = base.GetComponent<UIPingable>();
		if (component == null)
		{
			return;
		}
		component.Setup(option.Augment);
	}

	// Token: 0x060011AB RID: 4523 RVA: 0x0006DB94 File Offset: 0x0006BD94
	private void SetupPoints()
	{
		for (int i = 0; i < this.Option.Cost; i++)
		{
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.pointRef, this.pointRef.transform.parent);
			gameObject.SetActive(true);
			FountainUIInvestPoint component = gameObject.GetComponent<FountainUIInvestPoint>();
			component.UpdateDisplay(i, false);
			this.pts.Add(component);
		}
	}

	// Token: 0x060011AC RID: 4524 RVA: 0x0006DBF4 File Offset: 0x0006BDF4
	public void UpdateDisplay(bool fromSetup = false)
	{
		if (fromSetup)
		{
			this.BoxGroup.alpha = (this.Option.CanPurchase ? 1f : 0.5f);
			this.LockGroup.alpha = (float)((!this.Option.CanPurchase) ? 1 : 0);
		}
		this.SepiaIcon.gameObject.SetActive(this.Option.State != InkTalent.InkPurchaseState.Purchased);
		this.ColorIcon.gameObject.SetActive(this.Option.State == InkTalent.InkPurchaseState.Purchased);
		this.SepiaBorder.SetActive(this.Option.State != InkTalent.InkPurchaseState.Purchased);
		this.MetalBorder.SetActive(this.Option.State == InkTalent.InkPurchaseState.Purchased);
		this.PointGroup.SetActive(this.Option.State == InkTalent.InkPurchaseState.Available);
		this.PurchasedDisplay.SetActive(this.Option.State == InkTalent.InkPurchaseState.Purchased);
		this.UpdateInvestment(fromSetup);
		base.GetComponent<Button>().interactable = (this.Option.State == InkTalent.InkPurchaseState.Available || this.Option.State == InkTalent.InkPurchaseState.Purchased);
	}

	// Token: 0x060011AD RID: 4525 RVA: 0x0006DD1C File Offset: 0x0006BF1C
	private void UpdateInvestment(bool fromSetup)
	{
		if (this.Option.CurrentValue != this.CachedInvested)
		{
			this.CachedInvested = this.Option.CurrentValue;
			for (int i = 0; i < this.pts.Count; i++)
			{
				this.pts[i].UpdateDisplay(i, i < this.CachedInvested);
			}
			if (!fromSetup && this.CachedInvested > 0 && this.CachedInvested <= this.pts.Count)
			{
				FountainStoreUI.instance.InkInvested(this.pts[this.CachedInvested - 1].transform);
			}
		}
	}

	// Token: 0x060011AE RID: 4526 RVA: 0x0006DDC2 File Offset: 0x0006BFC2
	public void UpdateToUnlocked()
	{
		this.BoxGroup.UpdateOpacity(true, 2f, true);
		this.LockGroup.UpdateOpacity(false, 2f, true);
	}

	// Token: 0x060011AF RID: 4527 RVA: 0x0006DDE8 File Offset: 0x0006BFE8
	public void Click()
	{
		if (this.Option == null || this.Option.State != InkTalent.InkPurchaseState.Available || !this.Option.CanPurchase)
		{
			return;
		}
		InkManager.instance.TryInvest(this.Option, 1);
	}

	// Token: 0x060011B0 RID: 4528 RVA: 0x0006DE1E File Offset: 0x0006C01E
	public void OnSelect(BaseEventData ev)
	{
		if (this.Option == null)
		{
			return;
		}
		FountainStoreUI.instance.ShowDetail(this);
	}

	// Token: 0x060011B1 RID: 4529 RVA: 0x0006DE34 File Offset: 0x0006C034
	public FountainStoreItemUI()
	{
	}

	// Token: 0x04001054 RID: 4180
	public CanvasGroup BoxGroup;

	// Token: 0x04001055 RID: 4181
	public Image ColorIcon;

	// Token: 0x04001056 RID: 4182
	public Image SepiaIcon;

	// Token: 0x04001057 RID: 4183
	public Button ButtonRef;

	// Token: 0x04001058 RID: 4184
	public GameObject MetalBorder;

	// Token: 0x04001059 RID: 4185
	public GameObject SepiaBorder;

	// Token: 0x0400105A RID: 4186
	public GameObject TomeSpecificBorder;

	// Token: 0x0400105B RID: 4187
	public GameObject OrDisplay;

	// Token: 0x0400105C RID: 4188
	public GameObject PurchasedDisplay;

	// Token: 0x0400105D RID: 4189
	public CanvasGroup LockGroup;

	// Token: 0x0400105E RID: 4190
	public GameObject PointGroup;

	// Token: 0x0400105F RID: 4191
	public GameObject pointRef;

	// Token: 0x04001060 RID: 4192
	private List<FountainUIInvestPoint> pts = new List<FountainUIInvestPoint>();

	// Token: 0x04001061 RID: 4193
	[NonSerialized]
	public InkTalent Option;

	// Token: 0x04001062 RID: 4194
	private int CachedInvested = -1;
}

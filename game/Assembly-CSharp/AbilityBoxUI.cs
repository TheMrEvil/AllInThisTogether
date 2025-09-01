using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Token: 0x0200015B RID: 347
public class AbilityBoxUI : MonoBehaviour, ISelectHandler, IEventSystemHandler
{
	// Token: 0x06000F38 RID: 3896 RVA: 0x00060818 File Offset: 0x0005EA18
	private void Awake()
	{
		base.GetComponent<Button>().onClick.AddListener(new UnityAction(this.Click));
	}

	// Token: 0x06000F39 RID: 3897 RVA: 0x00060838 File Offset: 0x0005EA38
	public void Setup(PlayerAbilityType aType, AbilityTree tree, AbilityGroupBar bar)
	{
		this.abilityType = aType;
		this.abilityTree = tree;
		this.Grouping = bar;
		this.UpdateLockedState();
		foreach (Image image in this.Icons)
		{
			image.sprite = tree.Root.Usage.AbilityMetadata.Icon;
		}
		UnlockDB.AbilityUnlock abilityUnlock = UnlockDB.GetAbilityUnlock(tree);
		this.Cost = ((abilityUnlock != null) ? abilityUnlock.Cost : -1);
		this.CostText.text = ((this.Cost > 0) ? this.Cost.ToString() : "-");
		this.CostText.color = ((Currency.LoadoutCoin >= this.Cost) ? this.CanPurchaseColor : this.TooPoorColor);
		UIPingable component = base.GetComponent<UIPingable>();
		if (component == null)
		{
			return;
		}
		component.Setup(tree);
	}

	// Token: 0x06000F3A RID: 3898 RVA: 0x00060930 File Offset: 0x0005EB30
	public void Setup(AugmentTree core, Sprite baseIcon, Sprite lockIcon, Sprite borderGlow)
	{
		this.coreTree = core;
		this.UpdateLockedState();
		this.BaseIcon.sprite = baseIcon;
		this.LockIcon.sprite = lockIcon;
		this.GlowIcon.sprite = borderGlow;
		this.Cost = MetaDB.SignatureUnlockCost;
		this.CostText.text = ((this.Cost > 0) ? this.Cost.ToString() : "-");
		UIPingable component = base.GetComponent<UIPingable>();
		if (component == null)
		{
			return;
		}
		component.Setup(core);
	}

	// Token: 0x06000F3B RID: 3899 RVA: 0x000609B1 File Offset: 0x0005EBB1
	public void Click()
	{
		if (this.abilityTree != null)
		{
			AbilityPanel.instance.Select(this);
		}
		if (this.coreTree != null)
		{
			InkCoresPanel.instance.SelectCore(this.coreTree, true);
		}
	}

	// Token: 0x06000F3C RID: 3900 RVA: 0x000609EC File Offset: 0x0005EBEC
	public void OnSelect(BaseEventData ev)
	{
		if (!InputManager.IsUsingController)
		{
			return;
		}
		if (this.abilityTree != null)
		{
			AbilityPanel.instance.Hover(this);
		}
		if (this.coreTree != null)
		{
			InkCoresPanel.instance.SelectCore(this.coreTree, false);
		}
	}

	// Token: 0x06000F3D RID: 3901 RVA: 0x00060A39 File Offset: 0x0005EC39
	public void Refresh()
	{
		if (this.abilityTree != null && this.IncentiveDisplay != null)
		{
			this.IncentiveDisplay.SetActive(GoalManager.IsIncentiveAbility(this.abilityTree));
		}
	}

	// Token: 0x06000F3E RID: 3902 RVA: 0x00060A70 File Offset: 0x0005EC70
	public void UpdateLockedState()
	{
		this.isLocked = true;
		if (this.abilityTree != null)
		{
			this.isLocked = !UnlockManager.IsAbilityUnlocked(this.abilityTree);
		}
		if (this.coreTree != null)
		{
			this.isLocked = !UnlockManager.IsCoreUnlocked(this.coreTree);
		}
		this.CostDisplay.alpha = (float)(this.isLocked ? 1 : 0);
		this.CostText.color = ((Currency.LoadoutCoin >= this.Cost) ? this.CanPurchaseColor : this.TooPoorColor);
		foreach (CanvasGroup canvasGroup in this.UnlockedDisplays)
		{
			canvasGroup.alpha = (float)(this.isLocked ? 0 : 1);
		}
	}

	// Token: 0x06000F3F RID: 3903 RVA: 0x00060B58 File Offset: 0x0005ED58
	public AbilityBoxUI()
	{
	}

	// Token: 0x04000CF7 RID: 3319
	[NonSerialized]
	public AbilityTree abilityTree;

	// Token: 0x04000CF8 RID: 3320
	[NonSerialized]
	public AugmentTree coreTree;

	// Token: 0x04000CF9 RID: 3321
	[NonSerialized]
	public PlayerAbilityType abilityType;

	// Token: 0x04000CFA RID: 3322
	public List<Image> Icons;

	// Token: 0x04000CFB RID: 3323
	public GameObject EquippedDisplay;

	// Token: 0x04000CFC RID: 3324
	public GameObject SelectedDisplay;

	// Token: 0x04000CFD RID: 3325
	public ParticleSystem EquipFX;

	// Token: 0x04000CFE RID: 3326
	public ParticleSystem UnlockFX;

	// Token: 0x04000CFF RID: 3327
	public List<CanvasGroup> UnlockedDisplays;

	// Token: 0x04000D00 RID: 3328
	public TextMeshProUGUI CostText;

	// Token: 0x04000D01 RID: 3329
	public CanvasGroup CostDisplay;

	// Token: 0x04000D02 RID: 3330
	public Color CanPurchaseColor;

	// Token: 0x04000D03 RID: 3331
	public Color TooPoorColor;

	// Token: 0x04000D04 RID: 3332
	public GameObject IncentiveDisplay;

	// Token: 0x04000D05 RID: 3333
	public Image BaseIcon;

	// Token: 0x04000D06 RID: 3334
	public Image LockIcon;

	// Token: 0x04000D07 RID: 3335
	public Image GlowIcon;

	// Token: 0x04000D08 RID: 3336
	[NonSerialized]
	public AbilityGroupBar Grouping;

	// Token: 0x04000D09 RID: 3337
	[NonSerialized]
	public bool isLocked;

	// Token: 0x04000D0A RID: 3338
	private int Cost;
}

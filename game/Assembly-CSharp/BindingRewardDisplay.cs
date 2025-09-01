using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Token: 0x0200013F RID: 319
public class BindingRewardDisplay : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler
{
	// Token: 0x06000E97 RID: 3735 RVA: 0x0005C950 File Offset: 0x0005AB50
	public void Setup(Unlockable ul)
	{
		this.ulRef = ul;
		this.IsBindingReward = false;
		this.button.interactable = true;
		this.IsEmpty = false;
		this.Anim.Play();
		AudioManager.PlayInterfaceSFX(this.SetupSFX, 1f, 0f);
		if (ul == null)
		{
			this.SetupAsBinding();
			return;
		}
		UnlockDB.GenreUnlock genreUnlock = ul as UnlockDB.GenreUnlock;
		if (genreUnlock != null)
		{
			this.Setup(genreUnlock.Genre);
			return;
		}
		UnlockDB.AugmentUnlock augmentUnlock = ul as UnlockDB.AugmentUnlock;
		if (augmentUnlock != null)
		{
			this.Setup(augmentUnlock.Augment);
			return;
		}
		Cosmetic cosmetic = ul as Cosmetic;
		if (cosmetic != null)
		{
			this.Setup(cosmetic);
			return;
		}
		UnlockDB.BindingUnlock bindingUnlock = ul as UnlockDB.BindingUnlock;
		if (bindingUnlock != null)
		{
			this.Setup(bindingUnlock);
		}
	}

	// Token: 0x06000E98 RID: 3736 RVA: 0x0005C9FA File Offset: 0x0005ABFA
	public void SetupAsIncentive(int amount)
	{
		this.IncentiveGroup.gameObject.SetActive(true);
		this.IncentiveValueText.text = "+" + amount.ToString();
		this.IsIncentiveReward = true;
		this.IsEmpty = false;
	}

	// Token: 0x06000E99 RID: 3737 RVA: 0x0005CA37 File Offset: 0x0005AC37
	private void Setup(AugmentTree augment)
	{
		this.AugmentReward = augment;
		this.AugmentGroup.gameObject.SetActive(true);
		this.AugmentIcon.sprite = augment.Root.Icon;
	}

	// Token: 0x06000E9A RID: 3738 RVA: 0x0005CA67 File Offset: 0x0005AC67
	private void Setup(GenreTree tome)
	{
		this.TomeReward = tome;
		this.TomeGroup.gameObject.SetActive(true);
	}

	// Token: 0x06000E9B RID: 3739 RVA: 0x0005CA81 File Offset: 0x0005AC81
	private void Setup(Cosmetic c)
	{
		this.CosmeticReward = c;
		this.CosmeticGroup.SetActive(true);
	}

	// Token: 0x06000E9C RID: 3740 RVA: 0x0005CA96 File Offset: 0x0005AC96
	private void Setup(UnlockDB.BindingUnlock b)
	{
		this.BindingExplicit.SetActive(true);
		this.BindingExplicit_Icon.sprite = b.Binding.Root.Icon;
		this.AugmentReward = b.Binding;
	}

	// Token: 0x06000E9D RID: 3741 RVA: 0x0005CACB File Offset: 0x0005ACCB
	private void SetupAsBinding()
	{
		this.BindingGroup.gameObject.SetActive(true);
		this.IsBindingReward = true;
	}

	// Token: 0x06000E9E RID: 3742 RVA: 0x0005CAE8 File Offset: 0x0005ACE8
	public void Clear()
	{
		this.AugmentGroup.gameObject.SetActive(false);
		this.TomeGroup.gameObject.SetActive(false);
		this.CosmeticGroup.gameObject.SetActive(false);
		this.BindingGroup.gameObject.SetActive(false);
		this.BindingExplicit.gameObject.SetActive(false);
		this.IncentiveGroup.gameObject.SetActive(false);
		this.button.interactable = false;
		this.ulRef = null;
		this.TomeReward = null;
		this.AugmentReward = null;
		this.CosmeticReward = null;
		this.IsBindingReward = false;
		this.IsEmpty = true;
	}

	// Token: 0x06000E9F RID: 3743 RVA: 0x0005CB94 File Offset: 0x0005AD94
	public void OnPointerEnter(PointerEventData ev)
	{
		if (this.IsEmpty)
		{
			return;
		}
		if (this.TomeReward != null)
		{
			Tooltip.Show(this.TooltipAnchor.position, TextAnchor.LowerCenter, this.TomeReward, false);
			return;
		}
		if (this.AugmentReward != null)
		{
			Tooltip.Show(this.TooltipAnchor.position, TextAnchor.LowerCenter, this.AugmentReward, 1, null);
			return;
		}
		if (this.CosmeticReward != null)
		{
			Tooltip.Show(this.TooltipAnchor.position, TextAnchor.LowerCenter, this.CosmeticReward);
			return;
		}
		if (this.IsBindingReward)
		{
			Tooltip.SimpleInfoData simpleInfoData = new Tooltip.SimpleInfoData();
			simpleInfoData.Title = "New Binding";
			simpleInfoData.Detail = "A random binding based on your currently selected set.";
			simpleInfoData.Size = new Vector2(400f, 150f);
			Tooltip.Show(this.TooltipAnchor.position, TextAnchor.LowerCenter, simpleInfoData);
			return;
		}
		if (this.IsIncentiveReward)
		{
			Tooltip.SimpleInfoData simpleInfoData2 = new Tooltip.SimpleInfoData();
			simpleInfoData2.Title = "Suggested Reading";
			simpleInfoData2.Detail = "Total Quillmark bonus for mending the specified Tome or using the specified Spell.";
			simpleInfoData2.Size = new Vector2(400f, 150f);
			Tooltip.Show(this.TooltipAnchor.position, TextAnchor.LowerCenter, simpleInfoData2);
		}
	}

	// Token: 0x06000EA0 RID: 3744 RVA: 0x0005CCB0 File Offset: 0x0005AEB0
	public void OnPointerExit(PointerEventData ev)
	{
		Tooltip.Release();
	}

	// Token: 0x06000EA1 RID: 3745 RVA: 0x0005CCB7 File Offset: 0x0005AEB7
	public BindingRewardDisplay()
	{
	}

	// Token: 0x04000C1E RID: 3102
	public RectTransform TooltipAnchor;

	// Token: 0x04000C1F RID: 3103
	public Button button;

	// Token: 0x04000C20 RID: 3104
	public Animation Anim;

	// Token: 0x04000C21 RID: 3105
	public AudioClip SetupSFX;

	// Token: 0x04000C22 RID: 3106
	public GameObject AugmentGroup;

	// Token: 0x04000C23 RID: 3107
	public Image AugmentIcon;

	// Token: 0x04000C24 RID: 3108
	public GameObject TomeGroup;

	// Token: 0x04000C25 RID: 3109
	public GameObject BindingGroup;

	// Token: 0x04000C26 RID: 3110
	public GameObject BindingExplicit;

	// Token: 0x04000C27 RID: 3111
	public Image BindingExplicit_Icon;

	// Token: 0x04000C28 RID: 3112
	public GameObject CosmeticGroup;

	// Token: 0x04000C29 RID: 3113
	public GameObject IncentiveGroup;

	// Token: 0x04000C2A RID: 3114
	public TextMeshProUGUI IncentiveValueText;

	// Token: 0x04000C2B RID: 3115
	[NonSerialized]
	public Unlockable ulRef;

	// Token: 0x04000C2C RID: 3116
	[NonSerialized]
	public bool IsBindingReward;

	// Token: 0x04000C2D RID: 3117
	[NonSerialized]
	public bool IsIncentiveReward;

	// Token: 0x04000C2E RID: 3118
	[NonSerialized]
	public bool IsEmpty;

	// Token: 0x04000C2F RID: 3119
	private GenreTree TomeReward;

	// Token: 0x04000C30 RID: 3120
	private AugmentTree AugmentReward;

	// Token: 0x04000C31 RID: 3121
	private Cosmetic CosmeticReward;
}

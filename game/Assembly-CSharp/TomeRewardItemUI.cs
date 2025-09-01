using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200016D RID: 365
public class TomeRewardItemUI : MonoBehaviour
{
	// Token: 0x17000132 RID: 306
	// (get) Token: 0x06000FB6 RID: 4022 RVA: 0x000632A9 File Offset: 0x000614A9
	public Button MainButton
	{
		get
		{
			if (this.tome != null)
			{
				return this.TomeButton;
			}
			if (this.cosmetic != null)
			{
				return this.CosmeticButton;
			}
			if (this.isBinding)
			{
				return this.BindingButton;
			}
			return this.AugmentButton;
		}
	}

	// Token: 0x06000FB7 RID: 4023 RVA: 0x000632E4 File Offset: 0x000614E4
	public void Setup(UnlockDB.AugmentUnlock unlock)
	{
		this.UpdateUnlockInfo(unlock.BindingLevel);
		this.isBinding = false;
		this.BookInfoDisplay.SetActive(false);
		this.CosmeticInfoDisplay.SetActive(false);
		this.BindingDisplay.gameObject.SetActive(false);
		this.InfoDisplay.gameObject.SetActive(true);
		this.InfoDisplay.Setup(unlock.Augment, null);
		this.InfoDisplay.OverrideAnchorLocation(TextAnchor.MiddleRight);
		this.Checkbox.gameObject.SetActive(UnlockManager.IsAugmentUnlocked(unlock.Augment));
	}

	// Token: 0x06000FB8 RID: 4024 RVA: 0x0006337C File Offset: 0x0006157C
	public void Setup(UnlockDB.BindingUnlock unlock)
	{
		this.UpdateUnlockInfo(unlock.AtBinding);
		this.isBinding = true;
		this.BookInfoDisplay.SetActive(false);
		this.CosmeticInfoDisplay.SetActive(false);
		this.InfoDisplay.gameObject.SetActive(false);
		this.BindingDisplay.gameObject.SetActive(true);
		this.BindingDisplay.Setup(unlock.Binding, null);
		this.BindingDisplay.OverrideAnchorLocation(TextAnchor.MiddleRight);
		this.Checkbox.gameObject.SetActive(UnlockManager.IsBindingUnlocked(unlock.Binding));
	}

	// Token: 0x06000FB9 RID: 4025 RVA: 0x00063414 File Offset: 0x00061614
	public void Setup(UnlockDB.GenreUnlock unlock)
	{
		this.InfoDisplay.gameObject.SetActive(false);
		this.CosmeticInfoDisplay.SetActive(false);
		this.BindingDisplay.gameObject.SetActive(false);
		this.BookInfoDisplay.SetActive(true);
		foreach (TextMeshProUGUI textMeshProUGUI in this.BookTitle)
		{
			textMeshProUGUI.text = unlock.Genre.Root.ShortName;
		}
		this.UpdateUnlockInfo(unlock.AtBinding);
		this.tome = unlock.Genre;
		this.Checkbox.gameObject.SetActive(UnlockManager.IsGenreUnlocked(unlock.Genre));
	}

	// Token: 0x06000FBA RID: 4026 RVA: 0x000634E4 File Offset: 0x000616E4
	public void Setup(Cosmetic c)
	{
		this.InfoDisplay.gameObject.SetActive(false);
		this.BookInfoDisplay.SetActive(false);
		this.BindingDisplay.gameObject.SetActive(false);
		this.CosmeticInfoDisplay.SetActive(true);
		foreach (TextMeshProUGUI textMeshProUGUI in this.CosmeticTitle)
		{
			textMeshProUGUI.text = c.Name;
		}
		this.UpdateUnlockInfo(c.AtBinding);
		this.cosmetic = c;
		this.Checkbox.gameObject.SetActive(UnlockManager.IsCosmeticUnlocked(c));
	}

	// Token: 0x06000FBB RID: 4027 RVA: 0x000635A0 File Offset: 0x000617A0
	private void UpdateUnlockInfo(int bindingReq)
	{
		string str = "First Completion";
		if (bindingReq > 0)
		{
			str = "Beat binding level " + bindingReq.ToString();
		}
		this.RequirementText.text = str + ":";
	}

	// Token: 0x06000FBC RID: 4028 RVA: 0x000635DF File Offset: 0x000617DF
	public void BookOnSelect()
	{
		Tooltip.Show(this.BookTooltipAnchor.position, TextAnchor.MiddleRight, this.tome, false);
	}

	// Token: 0x06000FBD RID: 4029 RVA: 0x000635F9 File Offset: 0x000617F9
	public void BookOnDeselect()
	{
		Tooltip.Release();
	}

	// Token: 0x06000FBE RID: 4030 RVA: 0x00063600 File Offset: 0x00061800
	public void CosmeticOnSelect()
	{
		Tooltip.Show(this.BookTooltipAnchor.position, TextAnchor.MiddleRight, this.cosmetic);
	}

	// Token: 0x06000FBF RID: 4031 RVA: 0x00063619 File Offset: 0x00061819
	public void CosmeticOnDeselect()
	{
		Tooltip.Release();
	}

	// Token: 0x06000FC0 RID: 4032 RVA: 0x00063620 File Offset: 0x00061820
	public TomeRewardItemUI()
	{
	}

	// Token: 0x04000DCC RID: 3532
	public TextMeshProUGUI RequirementText;

	// Token: 0x04000DCD RID: 3533
	public GameObject Checkbox;

	// Token: 0x04000DCE RID: 3534
	[Header("Augment")]
	public AugmentBookBarItem InfoDisplay;

	// Token: 0x04000DCF RID: 3535
	public Button AugmentButton;

	// Token: 0x04000DD0 RID: 3536
	[Header("Book Info")]
	public GameObject BookInfoDisplay;

	// Token: 0x04000DD1 RID: 3537
	public List<TextMeshProUGUI> BookTitle;

	// Token: 0x04000DD2 RID: 3538
	public RectTransform BookTooltipAnchor;

	// Token: 0x04000DD3 RID: 3539
	private GenreTree tome;

	// Token: 0x04000DD4 RID: 3540
	public Button TomeButton;

	// Token: 0x04000DD5 RID: 3541
	[Header("Cosmetic Info")]
	public GameObject CosmeticInfoDisplay;

	// Token: 0x04000DD6 RID: 3542
	public List<TextMeshProUGUI> CosmeticTitle;

	// Token: 0x04000DD7 RID: 3543
	public RectTransform CosmeticTooltipAnchor;

	// Token: 0x04000DD8 RID: 3544
	private Cosmetic cosmetic;

	// Token: 0x04000DD9 RID: 3545
	public Button CosmeticButton;

	// Token: 0x04000DDA RID: 3546
	[Header("Binding")]
	public AugmentBookBarItem BindingDisplay;

	// Token: 0x04000DDB RID: 3547
	public Button BindingButton;

	// Token: 0x04000DDC RID: 3548
	private bool isBinding;
}

using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Token: 0x02000139 RID: 313
public class AugmentsTabElement : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler, IPointerClickHandler
{
	// Token: 0x17000126 RID: 294
	// (get) Token: 0x06000E5A RID: 3674 RVA: 0x0005AE8C File Offset: 0x0005908C
	private bool IsSelected
	{
		get
		{
			AugmentsTabElement.TabOwner ownerPanel = this.OwnerPanel;
			bool result;
			switch (ownerPanel)
			{
			case AugmentsTabElement.TabOwner.Augments:
				result = (AugmentsPanel.instance.CurTab == this.Tab);
				break;
			case AugmentsTabElement.TabOwner.Cosmetics:
				result = (CosmeticsPanel.instance.CurTab == this.Tab);
				break;
			case AugmentsTabElement.TabOwner.PostGame:
				result = (PostGamePanel.instance.CurTab == this.Tab);
				break;
			case AugmentsTabElement.TabOwner.TomeSelect:
				result = (GenrePanel.instance.CurTab == this.Tab);
				break;
			case AugmentsTabElement.TabOwner.AbilitySelect:
				result = (AbilityPanel.instance.CurTab == this.Tab);
				break;
			case AugmentsTabElement.TabOwner.PlayerNook:
				result = (NookPanel.instance.CurTab == this.Tab);
				break;
			default:
				throw new SwitchExpressionException(ownerPanel);
			}
			return result;
		}
	}

	// Token: 0x06000E5B RID: 3675 RVA: 0x0005AF4B File Offset: 0x0005914B
	public void OnPointerEnter(PointerEventData ev)
	{
		if (this.IsSelected)
		{
			return;
		}
		this.Anim.ResetTrigger("Release");
		this.Anim.SetTrigger("Hover");
	}

	// Token: 0x06000E5C RID: 3676 RVA: 0x0005AF76 File Offset: 0x00059176
	public void OnPointerExit(PointerEventData ev)
	{
		if (!this.IsSelected)
		{
			this.Release();
		}
	}

	// Token: 0x06000E5D RID: 3677 RVA: 0x0005AF88 File Offset: 0x00059188
	public void OnPointerClick(PointerEventData ev)
	{
		if (this.OwnerPanel == AugmentsTabElement.TabOwner.Augments)
		{
			AugmentsPanel.instance.SelectTab(this.Tab, false);
			return;
		}
		if (this.OwnerPanel == AugmentsTabElement.TabOwner.Cosmetics)
		{
			CosmeticsPanel.instance.SelectTab(this.Tab, false);
			return;
		}
		if (this.OwnerPanel == AugmentsTabElement.TabOwner.PostGame)
		{
			PostGamePanel.instance.SelectTab(this.Tab, false);
			return;
		}
		if (this.OwnerPanel == AugmentsTabElement.TabOwner.TomeSelect)
		{
			GenrePanel.instance.SelectTab(this.Tab, false);
			return;
		}
		if (this.OwnerPanel == AugmentsTabElement.TabOwner.AbilitySelect)
		{
			AbilityPanel.instance.SelectTab(this.Tab, false);
			return;
		}
		if (this.OwnerPanel == AugmentsTabElement.TabOwner.PlayerNook)
		{
			NookPanel.instance.SelectTab(this.Tab, false, true);
		}
	}

	// Token: 0x06000E5E RID: 3678 RVA: 0x0005B036 File Offset: 0x00059236
	public void UpateMask()
	{
		this.MarkImage.RecalculateMasking();
	}

	// Token: 0x06000E5F RID: 3679 RVA: 0x0005B043 File Offset: 0x00059243
	public void Select()
	{
		this.Anim.ResetTrigger("Hover");
		this.Anim.ResetTrigger("Release");
		this.Anim.SetTrigger("Select");
	}

	// Token: 0x06000E60 RID: 3680 RVA: 0x0005B075 File Offset: 0x00059275
	public void Release()
	{
		this.Anim.ResetTrigger("Hover");
		this.Anim.ResetTrigger("Select");
		this.Anim.SetTrigger("Release");
	}

	// Token: 0x06000E61 RID: 3681 RVA: 0x0005B0A7 File Offset: 0x000592A7
	public AugmentsTabElement()
	{
	}

	// Token: 0x04000BCC RID: 3020
	public Animator Anim;

	// Token: 0x04000BCD RID: 3021
	public AugmentsTabElement.TabOwner OwnerPanel;

	// Token: 0x04000BCE RID: 3022
	public AugmentsPanel.BookTab Tab;

	// Token: 0x04000BCF RID: 3023
	public Image MarkImage;

	// Token: 0x02000539 RID: 1337
	public enum TabOwner
	{
		// Token: 0x0400265A RID: 9818
		Augments,
		// Token: 0x0400265B RID: 9819
		Cosmetics,
		// Token: 0x0400265C RID: 9820
		PostGame,
		// Token: 0x0400265D RID: 9821
		TomeSelect,
		// Token: 0x0400265E RID: 9822
		AbilitySelect,
		// Token: 0x0400265F RID: 9823
		PlayerNook
	}
}

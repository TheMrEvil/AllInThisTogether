using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Token: 0x0200013E RID: 318
public class BindingRankUI : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler, ISelectHandler, IPointerClickHandler
{
	// Token: 0x06000E8A RID: 3722 RVA: 0x0005C4D0 File Offset: 0x0005A6D0
	public void Setup(AugmentTree augment, BindingBarUI bar)
	{
		this.button = base.GetComponent<Button>();
		this.canvas = base.GetComponentInParent<Canvas>();
		this.rect = base.GetComponent<RectTransform>();
		this.binding = augment;
		foreach (Image image in this.Icons)
		{
			image.sprite = this.binding.Root.Icon;
		}
		this.Bar = bar;
		this.ResetState();
		UIPingable component = base.GetComponent<UIPingable>();
		if (component == null)
		{
			return;
		}
		component.Setup(augment);
	}

	// Token: 0x06000E8B RID: 3723 RVA: 0x0005C57C File Offset: 0x0005A77C
	public void TickUpdate()
	{
		bool shouldShow = GameplayManager.CurState == GameState.Hub_Bindings && (this.CheckMark.activeSelf || (this.isUnlocked && this.Bar.CanActivate(this.binding)));
		this.CheckBox.UpdateOpacity(shouldShow, 4f, true);
		foreach (CanvasGroup cg in this.ActiveGroups)
		{
			cg.UpdateOpacity(this.CheckMark.activeSelf, 4f, true);
		}
		if (!UITutorial.InTutorial && InputManager.IsUsingController && UISelector.instance.CurrentSelection == this.button)
		{
			if (InputManager.UIAct.UIPrimary.WasPressed)
			{
				this.TryToggle(true);
			}
			if (InputManager.UIAct.UISecondary.WasPressed)
			{
				this.TryToggle(false);
			}
		}
	}

	// Token: 0x06000E8C RID: 3724 RVA: 0x0005C680 File Offset: 0x0005A880
	public void ResetState()
	{
		this.CheckMark.SetActive(false);
		this.isUnlocked = UnlockManager.IsBindingUnlocked(this.binding);
		this.UpdateDisplay();
	}

	// Token: 0x06000E8D RID: 3725 RVA: 0x0005C6A5 File Offset: 0x0005A8A5
	public void SetSelected(bool isSelected)
	{
		this.CheckMark.SetActive(isSelected);
		this.UpdateDisplay();
		if (isSelected && !this.wasSelected)
		{
			BindingsPanel.instance.Overview.BindingActivated(this);
		}
		this.wasSelected = isSelected;
	}

	// Token: 0x06000E8E RID: 3726 RVA: 0x0005C6DC File Offset: 0x0005A8DC
	private void UpdateDisplay()
	{
		this.LockGroup.alpha = (float)(this.isUnlocked ? 0 : 1);
		foreach (CanvasGroup canvasGroup in this.LockedFaders)
		{
			canvasGroup.alpha = (this.isUnlocked ? 1f : 0.5f);
		}
		if (this.isUnlocked)
		{
			bool flag = GameplayManager.CurState == GameState.Hub_Bindings;
		}
		if (this.LevelText != null)
		{
			bool flag2 = this.isUnlocked || this.CheckMark.activeSelf;
			this.LevelDisplay.SetActive(flag2);
			if (flag2)
			{
				this.LevelText.text = "+" + this.binding.Root.HeatLevel.ToString();
			}
		}
	}

	// Token: 0x06000E8F RID: 3727 RVA: 0x0005C7CC File Offset: 0x0005A9CC
	public void OnPointerEnter(PointerEventData ev)
	{
		this.ShowTooltip();
	}

	// Token: 0x06000E90 RID: 3728 RVA: 0x0005C7D4 File Offset: 0x0005A9D4
	public void OnPointerExit(PointerEventData ev)
	{
		Tooltip.Release();
	}

	// Token: 0x06000E91 RID: 3729 RVA: 0x0005C7DB File Offset: 0x0005A9DB
	public void OnSelect(BaseEventData ev)
	{
		this.ShowTooltip();
	}

	// Token: 0x06000E92 RID: 3730 RVA: 0x0005C7E4 File Offset: 0x0005A9E4
	private void ShowTooltip()
	{
		Transform transform = this.InfoAnchorTop;
		TextAnchor anchor;
		if (this.needsTopInfo())
		{
			anchor = (this.IsMainRank ? TextAnchor.LowerLeft : TextAnchor.LowerCenter);
		}
		else
		{
			transform = this.InfoAnchorBottom;
			anchor = (this.IsMainRank ? TextAnchor.UpperLeft : TextAnchor.UpperCenter);
		}
		Tooltip.Show(transform.position, anchor, this.binding, 1, null);
	}

	// Token: 0x06000E93 RID: 3731 RVA: 0x0005C83C File Offset: 0x0005AA3C
	private bool needsTopInfo()
	{
		ref Vector3 ptr = this.canvas.transform.InverseTransformPoint(this.rect.position);
		float height = ((RectTransform)this.canvas.transform).rect.height;
		return ptr.y / (height / 2f) < -0.25f;
	}

	// Token: 0x06000E94 RID: 3732 RVA: 0x0005C896 File Offset: 0x0005AA96
	public void OnPointerClick(PointerEventData pointerEventData)
	{
		if (pointerEventData.button == PointerEventData.InputButton.Left)
		{
			this.TryToggle(true);
			return;
		}
		if (pointerEventData.button == PointerEventData.InputButton.Right)
		{
			this.TryToggle(false);
		}
	}

	// Token: 0x06000E95 RID: 3733 RVA: 0x0005C8B8 File Offset: 0x0005AAB8
	private void TryToggle(bool toggleOn)
	{
		if (!this.isUnlocked || GameplayManager.CurState != GameState.Hub_Bindings)
		{
			return;
		}
		if (toggleOn && !BindingsPanel.instance.IsBindingActive(this.binding) && this.Bar.CanActivate(this.binding))
		{
			BindingsPanel.instance.SelectBinding(this.binding);
			return;
		}
		if (!toggleOn && BindingsPanel.instance.IsBindingActive(this.binding) && this.Bar.CanDeactivate(this.binding))
		{
			BindingsPanel.instance.TryRemoveBinding(this.binding);
		}
	}

	// Token: 0x06000E96 RID: 3734 RVA: 0x0005C948 File Offset: 0x0005AB48
	public BindingRankUI()
	{
	}

	// Token: 0x04000C0B RID: 3083
	public bool IsMainRank;

	// Token: 0x04000C0C RID: 3084
	public List<Image> Icons;

	// Token: 0x04000C0D RID: 3085
	private BindingBarUI Bar;

	// Token: 0x04000C0E RID: 3086
	public Transform InfoAnchorBottom;

	// Token: 0x04000C0F RID: 3087
	public Transform InfoAnchorTop;

	// Token: 0x04000C10 RID: 3088
	public List<CanvasGroup> LockedFaders;

	// Token: 0x04000C11 RID: 3089
	public CanvasGroup LockGroup;

	// Token: 0x04000C12 RID: 3090
	public Button Button;

	// Token: 0x04000C13 RID: 3091
	public GameObject LevelDisplay;

	// Token: 0x04000C14 RID: 3092
	public TextMeshProUGUI LevelText;

	// Token: 0x04000C15 RID: 3093
	[Header("Activation")]
	public GameObject CheckMark;

	// Token: 0x04000C16 RID: 3094
	public CanvasGroup CheckBox;

	// Token: 0x04000C17 RID: 3095
	public List<CanvasGroup> ActiveGroups;

	// Token: 0x04000C18 RID: 3096
	[NonSerialized]
	public AugmentTree binding;

	// Token: 0x04000C19 RID: 3097
	private Canvas canvas;

	// Token: 0x04000C1A RID: 3098
	private RectTransform rect;

	// Token: 0x04000C1B RID: 3099
	private bool isUnlocked;

	// Token: 0x04000C1C RID: 3100
	private Button button;

	// Token: 0x04000C1D RID: 3101
	private bool wasSelected;
}

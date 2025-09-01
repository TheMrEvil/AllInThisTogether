using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Token: 0x02000180 RID: 384
public class StatusBoxUI : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler
{
	// Token: 0x0600102F RID: 4143 RVA: 0x00065602 File Offset: 0x00063802
	private void Awake()
	{
		this.canvasGroup = base.GetComponent<CanvasGroup>();
	}

	// Token: 0x06001030 RID: 4144 RVA: 0x00065610 File Offset: 0x00063810
	public void Release()
	{
		this.shouldShow = false;
	}

	// Token: 0x06001031 RID: 4145 RVA: 0x0006561C File Offset: 0x0006381C
	private void Update()
	{
		this.stackGroup.UpdateOpacity(this.stackCount > 1, 4f, false);
		this.canvasGroup.UpdateOpacity(this.shouldShow, 8f, false);
		if (!this.shouldShow && this.canvasGroup.alpha <= 0f)
		{
			base.gameObject.SetActive(false);
		}
	}

	// Token: 0x06001032 RID: 4146 RVA: 0x00065680 File Offset: 0x00063880
	public void Setup(AugmentRootNode augment, int stacks = 1)
	{
		this.treeRef = augment;
		this.Icon.sprite = augment.Icon;
		this.TimerFill.fillAmount = 0f;
		this.UpdateAugment(stacks);
	}

	// Token: 0x06001033 RID: 4147 RVA: 0x000656B4 File Offset: 0x000638B4
	public void UpdateAugment(int stacks)
	{
		if (!base.gameObject.activeSelf)
		{
			this.shouldShow = true;
			base.transform.SetAsLastSibling();
			base.gameObject.SetActive(true);
		}
		this.stackCount = stacks;
		string text = (stacks > 1) ? stacks.ToString() : "";
		if (this.StackCount.text != text)
		{
			this.StackCount.text = text;
		}
	}

	// Token: 0x06001034 RID: 4148 RVA: 0x00065725 File Offset: 0x00063925
	public void Setup(StatusRootNode status, int stacks, float duration, float baseDuration)
	{
		this.statusRef = status;
		this.Icon.sprite = status.EffectIcon;
		this.UpdateStatus(status, stacks, duration, baseDuration);
	}

	// Token: 0x06001035 RID: 4149 RVA: 0x0006574C File Offset: 0x0006394C
	public void UpdateStatus(StatusRootNode status, int stacks, float duration, float baseDuration)
	{
		if (!base.gameObject.activeSelf)
		{
			this.shouldShow = true;
			base.transform.SetAsLastSibling();
			base.gameObject.SetActive(true);
		}
		this.TimerFill.fillAmount = ((duration > 0f) ? (1f - Mathf.Clamp(duration / baseDuration, 0f, 1f)) : 0f);
		string text = (stacks > 1) ? stacks.ToString() : "";
		if (this.StackCount.text != text)
		{
			this.StackCount.text = text;
		}
		this.stackCount = stacks;
	}

	// Token: 0x06001036 RID: 4150 RVA: 0x000657F0 File Offset: 0x000639F0
	public void OnPointerEnter(PointerEventData ev)
	{
		if (InputManager.IsUsingController)
		{
			return;
		}
		TextAnchor anchor = TextAnchor.LowerCenter;
		if (this.treeRef != null)
		{
			Tooltip.Show(this.TooltipAnchor.position, anchor, this.treeRef.tree as AugmentTree, this.stackCount, PlayerControl.myInstance);
			return;
		}
		if (this.statusRef != null)
		{
			Tooltip.Show(this.TooltipAnchor.position, anchor, this.statusRef, PlayerControl.myInstance);
		}
	}

	// Token: 0x06001037 RID: 4151 RVA: 0x0006586C File Offset: 0x00063A6C
	public void OnPointerExit(PointerEventData ev)
	{
		if (InputManager.IsUsingController)
		{
			return;
		}
		Tooltip.Release();
	}

	// Token: 0x06001038 RID: 4152 RVA: 0x0006587B File Offset: 0x00063A7B
	public StatusBoxUI()
	{
	}

	// Token: 0x04000E47 RID: 3655
	public Image Icon;

	// Token: 0x04000E48 RID: 3656
	public Image TimerFill;

	// Token: 0x04000E49 RID: 3657
	public TextMeshProUGUI StackCount;

	// Token: 0x04000E4A RID: 3658
	public CanvasGroup stackGroup;

	// Token: 0x04000E4B RID: 3659
	private CanvasGroup canvasGroup;

	// Token: 0x04000E4C RID: 3660
	[NonSerialized]
	public AugmentRootNode treeRef;

	// Token: 0x04000E4D RID: 3661
	[NonSerialized]
	public StatusRootNode statusRef;

	// Token: 0x04000E4E RID: 3662
	private int stackCount;

	// Token: 0x04000E4F RID: 3663
	private bool shouldShow = true;

	// Token: 0x04000E50 RID: 3664
	public RectTransform TooltipAnchor;
}

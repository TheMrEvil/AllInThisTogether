using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020001CA RID: 458
public class UIIndicator : MonoBehaviour
{
	// Token: 0x060012AF RID: 4783 RVA: 0x000733AD File Offset: 0x000715AD
	private void Awake()
	{
		this.canvasGroup = base.GetComponent<CanvasGroup>();
		this.rect = base.GetComponent<RectTransform>();
		this.baseSize = this.Icon.rectTransform.sizeDelta.x;
	}

	// Token: 0x060012B0 RID: 4784 RVA: 0x000733E4 File Offset: 0x000715E4
	public void Setup(Indicatable target)
	{
		this.target = target;
		this.Icon.color = target.IndicatorColor;
		this.Arrow.color = target.IndicatorColor;
		if (target.IconOverride != null)
		{
			this.Icon.sprite = target.IconOverride;
		}
		if (target.ArrowOverride != null)
		{
			this.Arrow.sprite = target.ArrowOverride;
		}
		this.Icon.rectTransform.sizeDelta = this.baseSize * target.IconScale * Vector3.one;
		this.Arrow.rectTransform.sizeDelta = this.baseSize * target.IconScale * target.ArrowScaleMult * Vector3.one;
	}

	// Token: 0x060012B1 RID: 4785 RVA: 0x000734B8 File Offset: 0x000716B8
	public void UpdatePosition(int index)
	{
		if (this.canvasGroup.alpha == 0f)
		{
			return;
		}
		if (this.target.RotationSpeed != 0f)
		{
			this.Icon.transform.Rotate(0f, 0f, this.target.RotationSpeed * Time.deltaTime, Space.Self);
		}
		if (this.target == null)
		{
			return;
		}
		bool flag = this.rect.FollowWorldObject(this.target.Root, this.canvas, index, this.EdgeDistance) < 0.66f;
		this.IconGrp.UpdateOpacity(!flag, 8f, false);
		this.ArrowGrp.UpdateOpacity(flag, 8f, false);
		this.Arrow.rectTransform.up = -this.rect.localPosition.normalized;
	}

	// Token: 0x060012B2 RID: 4786 RVA: 0x000735A0 File Offset: 0x000717A0
	public void UpdateOpacity()
	{
		bool shouldShow = this.target != null && this.target.ShouldIndicate();
		if (PlayerControl.myInstance == null || PlayerControl.myInstance.IsSpectator || GameHUD.Mode == GameHUD.HUDMode.Off || PanelManager.CurPanel != PanelType.GameInvisible)
		{
			shouldShow = false;
		}
		this.canvasGroup.UpdateOpacity(shouldShow, 4f, false);
	}

	// Token: 0x060012B3 RID: 4787 RVA: 0x00073607 File Offset: 0x00071807
	public UIIndicator()
	{
	}

	// Token: 0x040011BA RID: 4538
	private CanvasGroup canvasGroup;

	// Token: 0x040011BB RID: 4539
	private Indicatable target;

	// Token: 0x040011BC RID: 4540
	private RectTransform rect;

	// Token: 0x040011BD RID: 4541
	public RectTransform canvas;

	// Token: 0x040011BE RID: 4542
	[Range(0f, 0.5f)]
	public float EdgeDistance;

	// Token: 0x040011BF RID: 4543
	public CanvasGroup IconGrp;

	// Token: 0x040011C0 RID: 4544
	public Image Icon;

	// Token: 0x040011C1 RID: 4545
	public CanvasGroup ArrowGrp;

	// Token: 0x040011C2 RID: 4546
	public Image Arrow;

	// Token: 0x040011C3 RID: 4547
	private float baseSize = 100f;
}

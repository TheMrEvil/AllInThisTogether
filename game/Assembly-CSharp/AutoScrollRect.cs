using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Token: 0x02000205 RID: 517
[RequireComponent(typeof(ScrollRect))]
public class AutoScrollRect : MonoBehaviour
{
	// Token: 0x06001611 RID: 5649 RVA: 0x0008BAD0 File Offset: 0x00089CD0
	public void TickUpdate()
	{
		GameObject currentSelectedGameObject = EventSystem.current.currentSelectedGameObject;
		if (currentSelectedGameObject == null)
		{
			return;
		}
		if (!currentSelectedGameObject.transform.IsChildOf(this.contentRectTransform))
		{
			return;
		}
		if (this.scrollRect.vertical)
		{
			this.UpdateVertical(currentSelectedGameObject);
			return;
		}
		this.UpdateHorizontal(currentSelectedGameObject);
	}

	// Token: 0x06001612 RID: 5650 RVA: 0x0008BB24 File Offset: 0x00089D24
	private void UpdateVertical(GameObject selected)
	{
		this.selectedRectTransform = selected.GetComponent<RectTransform>();
		Rect rect = this.viewportRectTransform.rect;
		Rect rect2 = this.selectedRectTransform.rect.Transform(this.selectedRectTransform).InverseTransform(this.viewportRectTransform);
		float num = rect2.yMax - rect.yMax;
		float num2 = rect.yMin - rect2.yMin;
		if (num < 0f)
		{
			num = 0f;
		}
		if (num2 < 0f)
		{
			num2 = 0f;
		}
		float num3 = (num > 0f) ? num : (-num2);
		if (num3 == 0f)
		{
			return;
		}
		float num4 = this.contentRectTransform.rect.Transform(this.contentRectTransform).InverseTransform(this.viewportRectTransform).height - rect.height;
		float num5 = 1f / num4;
		this.scrollRect.verticalNormalizedPosition += num3 * num5;
	}

	// Token: 0x06001613 RID: 5651 RVA: 0x0008BC18 File Offset: 0x00089E18
	private void UpdateHorizontal(GameObject selected)
	{
		RectTransform component = selected.GetComponent<RectTransform>();
		Rect rect = this.viewportRectTransform.rect;
		Rect rect2 = component.rect.Transform(component).InverseTransform(this.viewportRectTransform);
		float num = rect.xMin - rect2.xMin;
		float num2 = rect2.xMax - rect.xMax;
		if (num < 0f)
		{
			num = 0f;
		}
		if (num2 < 0f)
		{
			num2 = 0f;
		}
		float num3 = (num2 > 0f) ? num2 : (-num);
		if (num3 == 0f)
		{
			return;
		}
		float num4 = this.contentRectTransform.rect.Transform(this.contentRectTransform).InverseTransform(this.viewportRectTransform).width - rect.width;
		float num5 = 1f / num4;
		this.scrollRect.horizontalNormalizedPosition += num3 * num5;
	}

	// Token: 0x06001614 RID: 5652 RVA: 0x0008BCFF File Offset: 0x00089EFF
	public AutoScrollRect()
	{
	}

	// Token: 0x040015BF RID: 5567
	public ScrollRect scrollRect;

	// Token: 0x040015C0 RID: 5568
	public RectTransform viewportRectTransform;

	// Token: 0x040015C1 RID: 5569
	public RectTransform contentRectTransform;

	// Token: 0x040015C2 RID: 5570
	private RectTransform selectedRectTransform;
}

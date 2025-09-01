using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Token: 0x0200020A RID: 522
public class MouseSelector : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IDeselectHandler, IPointerExitHandler
{
	// Token: 0x06001632 RID: 5682 RVA: 0x0008C453 File Offset: 0x0008A653
	private void Awake()
	{
		this.selectable = base.GetComponent<Selectable>();
	}

	// Token: 0x06001633 RID: 5683 RVA: 0x0008C461 File Offset: 0x0008A661
	public void OnPointerEnter(PointerEventData eventData)
	{
		if (this.selectable.interactable)
		{
			this.selectable.Select();
		}
	}

	// Token: 0x06001634 RID: 5684 RVA: 0x0008C47B File Offset: 0x0008A67B
	public void OnPointerExit(PointerEventData eventData)
	{
		if (EventSystem.current.currentSelectedGameObject == this.selectable.gameObject)
		{
			EventSystem.current.SetSelectedGameObject(null);
		}
	}

	// Token: 0x06001635 RID: 5685 RVA: 0x0008C4A4 File Offset: 0x0008A6A4
	public void OnDeselect(BaseEventData eventData)
	{
		if (this.selectable != null)
		{
			this.selectable.OnPointerExit(null);
		}
	}

	// Token: 0x06001636 RID: 5686 RVA: 0x0008C4C0 File Offset: 0x0008A6C0
	public MouseSelector()
	{
	}

	// Token: 0x040015D2 RID: 5586
	private Selectable selectable;
}

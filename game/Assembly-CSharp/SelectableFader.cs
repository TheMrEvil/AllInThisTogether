using System;
using UnityEngine;
using UnityEngine.EventSystems;

// Token: 0x0200020F RID: 527
public class SelectableFader : MonoBehaviour, IDeselectHandler, IEventSystemHandler, ISelectHandler
{
	// Token: 0x0600164F RID: 5711 RVA: 0x0008CBCE File Offset: 0x0008ADCE
	private void OnEnable()
	{
		CanvasController.SelectFaders.Add(this);
	}

	// Token: 0x06001650 RID: 5712 RVA: 0x0008CBDB File Offset: 0x0008ADDB
	private void OnDisable()
	{
		CanvasController.SelectFaders.Remove(this);
	}

	// Token: 0x06001651 RID: 5713 RVA: 0x0008CBE9 File Offset: 0x0008ADE9
	public void TickUpdate()
	{
		this.FadeGroup.UpdateOpacity(this.IsSelected, 3f, true);
	}

	// Token: 0x06001652 RID: 5714 RVA: 0x0008CC02 File Offset: 0x0008AE02
	public void OnSelect(BaseEventData eventData)
	{
		this.IsSelected = true;
	}

	// Token: 0x06001653 RID: 5715 RVA: 0x0008CC0B File Offset: 0x0008AE0B
	public void OnDeselect(BaseEventData eventData)
	{
		this.IsSelected = false;
	}

	// Token: 0x06001654 RID: 5716 RVA: 0x0008CC14 File Offset: 0x0008AE14
	public SelectableFader()
	{
	}

	// Token: 0x040015E5 RID: 5605
	public CanvasGroup FadeGroup;

	// Token: 0x040015E6 RID: 5606
	private bool IsSelected;
}

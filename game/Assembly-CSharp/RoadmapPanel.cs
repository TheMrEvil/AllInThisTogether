using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020001EE RID: 494
public class RoadmapPanel : MonoBehaviour
{
	// Token: 0x0600150E RID: 5390 RVA: 0x000840C4 File Offset: 0x000822C4
	private void Awake()
	{
		UIPanel component = base.GetComponent<UIPanel>();
		component.OnEnteredPanel = (Action)Delegate.Combine(component.OnEnteredPanel, new Action(this.OnEntered));
	}

	// Token: 0x0600150F RID: 5391 RVA: 0x000840ED File Offset: 0x000822ED
	private void OnEntered()
	{
		this.scroll.horizontalNormalizedPosition = 1f;
	}

	// Token: 0x06001510 RID: 5392 RVA: 0x000840FF File Offset: 0x000822FF
	private void Update()
	{
		if (PanelManager.CurPanel != PanelType.Roadmap)
		{
			return;
		}
		if (InputManager.IsUsingController)
		{
			this.autoScroll.TickUpdate();
		}
	}

	// Token: 0x06001511 RID: 5393 RVA: 0x0008411D File Offset: 0x0008231D
	public RoadmapPanel()
	{
	}

	// Token: 0x04001482 RID: 5250
	public ScrollRect scroll;

	// Token: 0x04001483 RID: 5251
	public AutoScrollRect autoScroll;
}

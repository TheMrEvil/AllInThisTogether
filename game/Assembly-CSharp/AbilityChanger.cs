using System;
using UnityEngine;

// Token: 0x020000A3 RID: 163
public class AbilityChanger : PositionalInteraction
{
	// Token: 0x0600079E RID: 1950 RVA: 0x00036C43 File Offset: 0x00034E43
	internal override void Update()
	{
		base.Update();
		this.Prompt.UpdateOpacity(this.PlayerInside, 2f, false);
	}

	// Token: 0x0600079F RID: 1951 RVA: 0x00036C62 File Offset: 0x00034E62
	internal override void OnEnter()
	{
		base.OnEnter();
	}

	// Token: 0x060007A0 RID: 1952 RVA: 0x00036C6A File Offset: 0x00034E6A
	internal override void OnInteract()
	{
		if (PanelManager.StackContains(PanelType.Customize_Abilities) || PanelManager.CurPanel != PanelType.GameInvisible)
		{
			return;
		}
		PanelManager.instance.PushPanel(PanelType.Customize_Abilities);
		base.OnInteract();
	}

	// Token: 0x060007A1 RID: 1953 RVA: 0x00036C8E File Offset: 0x00034E8E
	internal override void OnExit()
	{
		PanelManager.instance.RemoveFromStack(PanelType.Customize_Abilities);
		base.OnExit();
	}

	// Token: 0x060007A2 RID: 1954 RVA: 0x00036CA1 File Offset: 0x00034EA1
	public AbilityChanger()
	{
	}

	// Token: 0x0400065B RID: 1627
	public CanvasGroup Prompt;
}

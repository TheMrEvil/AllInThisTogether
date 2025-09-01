using System;
using UnityEngine;

// Token: 0x0200036C RID: 876
public class StatusModNode : AugmentRootNode
{
	// Token: 0x06001D19 RID: 7449 RVA: 0x000B0B06 File Offset: 0x000AED06
	public override Node.InspectorProps GetInspectorProps()
	{
		return new Node.InspectorProps
		{
			Title = "Modifier",
			MinInspectorSize = new Vector2(350f, 0f)
		};
	}

	// Token: 0x06001D1A RID: 7450 RVA: 0x000B0B2D File Offset: 0x000AED2D
	public StatusModNode()
	{
	}

	// Token: 0x04001DBE RID: 7614
	public bool Stacks;
}

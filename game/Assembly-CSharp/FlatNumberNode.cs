using System;
using UnityEngine;

// Token: 0x0200037F RID: 895
public class FlatNumberNode : NumberNode
{
	// Token: 0x06001D76 RID: 7542 RVA: 0x000B349A File Offset: 0x000B169A
	public override float Evaluate(EffectProperties props)
	{
		return this.Value;
	}

	// Token: 0x06001D77 RID: 7543 RVA: 0x000B34A4 File Offset: 0x000B16A4
	public override Node.InspectorProps GetInspectorProps()
	{
		return new Node.InspectorProps
		{
			Title = "Number",
			MinInspectorSize = new Vector2(120f, 0f),
			MaxInspectorSize = new Vector2(120f, 0f),
			AllowMultipleInputs = true
		};
	}

	// Token: 0x06001D78 RID: 7544 RVA: 0x000B34F2 File Offset: 0x000B16F2
	public FlatNumberNode()
	{
	}

	// Token: 0x04001E2B RID: 7723
	public float Value;
}

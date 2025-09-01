using System;
using UnityEngine;

// Token: 0x02000385 RID: 901
public class RandomNumberNode : NumberNode
{
	// Token: 0x06001D8B RID: 7563 RVA: 0x000B3995 File Offset: 0x000B1B95
	public override float Evaluate(EffectProperties props)
	{
		return props.RandomFloat(this.Min, this.Max);
	}

	// Token: 0x06001D8C RID: 7564 RVA: 0x000B39AC File Offset: 0x000B1BAC
	public override Node.InspectorProps GetInspectorProps()
	{
		return new Node.InspectorProps
		{
			Title = "Random",
			MinInspectorSize = new Vector2(170f, 0f),
			MaxInspectorSize = new Vector2(170f, 0f),
			AllowMultipleInputs = true
		};
	}

	// Token: 0x06001D8D RID: 7565 RVA: 0x000B39FA File Offset: 0x000B1BFA
	public RandomNumberNode()
	{
	}

	// Token: 0x04001E36 RID: 7734
	public float Min;

	// Token: 0x04001E37 RID: 7735
	public float Max = 100f;
}

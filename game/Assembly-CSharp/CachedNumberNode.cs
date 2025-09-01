using System;
using UnityEngine;

// Token: 0x02000378 RID: 888
public class CachedNumberNode : NumberNode
{
	// Token: 0x06001D5D RID: 7517 RVA: 0x000B29A8 File Offset: 0x000B0BA8
	public override float Evaluate(EffectProperties props)
	{
		return props.GetFloat(this.ID);
	}

	// Token: 0x06001D5E RID: 7518 RVA: 0x000B29B8 File Offset: 0x000B0BB8
	public override Node.InspectorProps GetInspectorProps()
	{
		return new Node.InspectorProps
		{
			Title = "Cached",
			MinInspectorSize = new Vector2(120f, 0f),
			MaxInspectorSize = new Vector2(120f, 0f),
			AllowMultipleInputs = true
		};
	}

	// Token: 0x06001D5F RID: 7519 RVA: 0x000B2A06 File Offset: 0x000B0C06
	public CachedNumberNode()
	{
	}

	// Token: 0x04001E0A RID: 7690
	public string ID;
}

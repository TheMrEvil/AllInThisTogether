using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000346 RID: 838
public class KeywordOverrideNode : ModOverrideNode
{
	// Token: 0x06001C52 RID: 7250 RVA: 0x000AD542 File Offset: 0x000AB742
	public override bool ShouldOverride(EffectProperties props, Node node)
	{
		return node is ActivateKeywordNode && (node as ActivateKeywordNode).Keyword == this.Keyword;
	}

	// Token: 0x06001C53 RID: 7251 RVA: 0x000AD561 File Offset: 0x000AB761
	internal override bool CanScope()
	{
		return false;
	}

	// Token: 0x06001C54 RID: 7252 RVA: 0x000AD564 File Offset: 0x000AB764
	public override Node.InspectorProps GetInspectorProps()
	{
		return new Node.InspectorProps
		{
			Title = "Keyword Override",
			MinInspectorSize = new Vector2(150f, 0f)
		};
	}

	// Token: 0x06001C55 RID: 7253 RVA: 0x000AD58B File Offset: 0x000AB78B
	public KeywordOverrideNode()
	{
	}

	// Token: 0x04001D26 RID: 7462
	public Keyword Keyword;

	// Token: 0x04001D27 RID: 7463
	[HideInInspector]
	[SerializeField]
	[OutputPort(typeof(EffectNode), true, "Extra Effects", PortLocation.Default)]
	public List<Node> ExtraEffects = new List<Node>();
}

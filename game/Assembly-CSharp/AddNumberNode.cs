using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000377 RID: 887
public class AddNumberNode : NumberNode
{
	// Token: 0x06001D5A RID: 7514 RVA: 0x000B28FC File Offset: 0x000B0AFC
	public override float Evaluate(EffectProperties props)
	{
		float num = 0f;
		foreach (Node node in this.Numbers)
		{
			NumberNode numberNode = (NumberNode)node;
			num += numberNode.Evaluate(props);
		}
		return num;
	}

	// Token: 0x06001D5B RID: 7515 RVA: 0x000B2960 File Offset: 0x000B0B60
	public override Node.InspectorProps GetInspectorProps()
	{
		return new Node.InspectorProps
		{
			Title = "Add",
			MinInspectorSize = new Vector2(150f, 0f),
			AllowMultipleInputs = true,
			ShowInspectorView = false
		};
	}

	// Token: 0x06001D5C RID: 7516 RVA: 0x000B2995 File Offset: 0x000B0B95
	public AddNumberNode()
	{
	}

	// Token: 0x04001E09 RID: 7689
	[HideInInspector]
	[SerializeField]
	[OutputPort(typeof(NumberNode), true, "Values", PortLocation.Header)]
	public List<Node> Numbers = new List<Node>();
}

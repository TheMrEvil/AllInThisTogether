using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000382 RID: 898
public class MultiplyNumberNode : NumberNode
{
	// Token: 0x06001D82 RID: 7554 RVA: 0x000B3830 File Offset: 0x000B1A30
	public override float Evaluate(EffectProperties props)
	{
		float num = 1f;
		foreach (Node node in this.Numbers)
		{
			NumberNode numberNode = (NumberNode)node;
			num *= numberNode.Evaluate(props);
		}
		return num;
	}

	// Token: 0x06001D83 RID: 7555 RVA: 0x000B3894 File Offset: 0x000B1A94
	public override Node.InspectorProps GetInspectorProps()
	{
		return new Node.InspectorProps
		{
			Title = "Mult",
			MinInspectorSize = new Vector2(125f, 0f),
			MaxInspectorSize = new Vector2(125f, 0f),
			AllowMultipleInputs = true,
			ShowInspectorView = false
		};
	}

	// Token: 0x06001D84 RID: 7556 RVA: 0x000B38E9 File Offset: 0x000B1AE9
	public MultiplyNumberNode()
	{
	}

	// Token: 0x04001E34 RID: 7732
	[HideInInspector]
	[SerializeField]
	[OutputPort(typeof(NumberNode), true, "", PortLocation.Header)]
	public List<Node> Numbers = new List<Node>();
}

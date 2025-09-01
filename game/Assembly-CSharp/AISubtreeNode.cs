using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000306 RID: 774
public class AISubtreeNode : AINode
{
	// Token: 0x06001B2C RID: 6956 RVA: 0x000A855B File Offset: 0x000A675B
	public override Node.InspectorProps GetInspectorProps()
	{
		return new Node.InspectorProps
		{
			Title = "Subtree",
			MinInspectorSize = new Vector2(200f, 0f)
		};
	}

	// Token: 0x06001B2D RID: 6957 RVA: 0x000A8584 File Offset: 0x000A6784
	public override Node Clone(Dictionary<string, Node> alreadyCloned = null, bool fullClone = false)
	{
		if (!Application.isPlaying || this.Subtree == null || (this.Subtree.RootNode as AIRootNode).Entry == null)
		{
			return base.Clone(alreadyCloned, fullClone);
		}
		Node node = (this.Subtree.RootNode as AIRootNode).Entry.Clone(alreadyCloned, fullClone);
		Vector3 position = node.position;
		foreach (Node node2 in node.GetConnectedNodes(null))
		{
			node2.position = node2.position - position + this.position;
		}
		return node;
	}

	// Token: 0x06001B2E RID: 6958 RVA: 0x000A864C File Offset: 0x000A684C
	public AISubtreeNode()
	{
	}

	// Token: 0x04001BBC RID: 7100
	public AITree Subtree;
}

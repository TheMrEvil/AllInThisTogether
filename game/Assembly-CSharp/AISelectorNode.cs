using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000304 RID: 772
public class AISelectorNode : AINode
{
	// Token: 0x06001B23 RID: 6947 RVA: 0x000A8272 File Offset: 0x000A6472
	public override Node.InspectorProps GetInspectorProps()
	{
		return new Node.InspectorProps
		{
			Title = "Selector"
		};
	}

	// Token: 0x06001B24 RID: 6948 RVA: 0x000A8284 File Offset: 0x000A6484
	internal override AILogicState Run(AIControl entity)
	{
		foreach (Node node in this.Options)
		{
			if (!this.InProgress() || node.InProgress() || !(this.runningNode != null) || this.runningNode is AIInterruptableNode)
			{
				AILogicState ailogicState = (node as AINode).DoUpdate(entity);
				if (ailogicState != AILogicState.Fail)
				{
					this.runningNode = (node as AINode);
					return ailogicState;
				}
			}
		}
		return AILogicState.Fail;
	}

	// Token: 0x06001B25 RID: 6949 RVA: 0x000A8320 File Offset: 0x000A6520
	public override List<AbilityTree> CollectActions(List<AbilityTree> list, List<Node> wasChecked = null)
	{
		if (wasChecked == null)
		{
			wasChecked = new List<Node>();
		}
		if (wasChecked.Contains(this))
		{
			return list;
		}
		wasChecked.Add(this);
		foreach (Node node in this.Options)
		{
			(node as AINode).CollectActions(list, wasChecked);
		}
		return list;
	}

	// Token: 0x06001B26 RID: 6950 RVA: 0x000A8398 File Offset: 0x000A6598
	public AISelectorNode()
	{
	}

	// Token: 0x04001BB7 RID: 7095
	[HideInInspector]
	[SerializeField]
	[OutputPort(typeof(AINode), true, "", PortLocation.Header)]
	public List<Node> Options = new List<Node>();

	// Token: 0x04001BB8 RID: 7096
	private AINode runningNode;
}

using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000302 RID: 770
public class AIRandomNode : AINode
{
	// Token: 0x06001B17 RID: 6935 RVA: 0x000A7E68 File Offset: 0x000A6068
	internal override AILogicState Run(AIControl entity)
	{
		List<Node> list = new List<Node>();
		foreach (Node item in this.Options)
		{
			list.Add(item);
		}
		list.Shuffle(null);
		foreach (Node node in list)
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

	// Token: 0x06001B18 RID: 6936 RVA: 0x000A7F54 File Offset: 0x000A6154
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

	// Token: 0x06001B19 RID: 6937 RVA: 0x000A7FCC File Offset: 0x000A61CC
	public override Node.InspectorProps GetInspectorProps()
	{
		return new Node.InspectorProps
		{
			Title = "Select Random"
		};
	}

	// Token: 0x06001B1A RID: 6938 RVA: 0x000A7FDE File Offset: 0x000A61DE
	public AIRandomNode()
	{
	}

	// Token: 0x04001BB0 RID: 7088
	[HideInInspector]
	[SerializeField]
	[OutputPort(typeof(AINode), true, "", PortLocation.Header)]
	public List<Node> Options = new List<Node>();

	// Token: 0x04001BB1 RID: 7089
	private AINode runningNode;
}

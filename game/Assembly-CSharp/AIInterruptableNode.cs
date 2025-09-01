using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020002FD RID: 765
public class AIInterruptableNode : AINode
{
	// Token: 0x06001B06 RID: 6918 RVA: 0x000A7AC8 File Offset: 0x000A5CC8
	public override Node.InspectorProps GetInspectorProps()
	{
		return new Node.InspectorProps
		{
			Title = "Interruptible",
			ShowInspectorView = false
		};
	}

	// Token: 0x06001B07 RID: 6919 RVA: 0x000A7AE1 File Offset: 0x000A5CE1
	internal override AILogicState Run(AIControl entity)
	{
		if (this.Output == null)
		{
			return AILogicState.Fail;
		}
		return (this.Output as AINode).DoUpdate(entity);
	}

	// Token: 0x06001B08 RID: 6920 RVA: 0x000A7B04 File Offset: 0x000A5D04
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
		if (this.Output != null)
		{
			(this.Output as AINode).CollectActions(list, wasChecked);
		}
		return list;
	}

	// Token: 0x06001B09 RID: 6921 RVA: 0x000A7B44 File Offset: 0x000A5D44
	public AIInterruptableNode()
	{
	}

	// Token: 0x04001BA5 RID: 7077
	[HideInInspector]
	[SerializeField]
	[OutputPort(typeof(AINode), false, "", PortLocation.Header)]
	public Node Output;
}

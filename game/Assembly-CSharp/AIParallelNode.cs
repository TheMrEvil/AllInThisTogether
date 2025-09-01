using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000301 RID: 769
public class AIParallelNode : AINode
{
	// Token: 0x06001B13 RID: 6931 RVA: 0x000A7D40 File Offset: 0x000A5F40
	public override Node.InspectorProps GetInspectorProps()
	{
		return new Node.InspectorProps
		{
			Title = "Parallel",
			ShowInspectorView = true
		};
	}

	// Token: 0x06001B14 RID: 6932 RVA: 0x000A7D5C File Offset: 0x000A5F5C
	internal override AILogicState Run(AIControl entity)
	{
		AILogicState ailogicState = AILogicState.Fail;
		foreach (Node node in this.Outputs)
		{
			AILogicState ailogicState2 = (node as AINode).DoUpdate(entity);
			if ((ailogicState == AILogicState.Fail || this.NeedAllSuccess) && ailogicState2 == AILogicState.Running)
			{
				ailogicState = AILogicState.Running;
			}
			if (ailogicState2 == AILogicState.Success && (!this.NeedAllSuccess || ailogicState == AILogicState.Fail))
			{
				ailogicState = AILogicState.Success;
			}
		}
		return ailogicState;
	}

	// Token: 0x06001B15 RID: 6933 RVA: 0x000A7DDC File Offset: 0x000A5FDC
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
		foreach (Node node in this.Outputs)
		{
			(node as AINode).CollectActions(list, wasChecked);
		}
		return list;
	}

	// Token: 0x06001B16 RID: 6934 RVA: 0x000A7E54 File Offset: 0x000A6054
	public AIParallelNode()
	{
	}

	// Token: 0x04001BAE RID: 7086
	[HideInInspector]
	[SerializeField]
	[OutputPort(typeof(AINode), true, "", PortLocation.Header)]
	public List<Node> Outputs = new List<Node>();

	// Token: 0x04001BAF RID: 7087
	public bool NeedAllSuccess;
}

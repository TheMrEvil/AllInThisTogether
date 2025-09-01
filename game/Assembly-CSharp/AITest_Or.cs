using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200030A RID: 778
public class AITest_Or : AITestNode
{
	// Token: 0x06001B3A RID: 6970 RVA: 0x000A87F4 File Offset: 0x000A69F4
	public override bool Evaluate(EntityControl entity)
	{
		foreach (Node node in this.Tests)
		{
			AITestNode aitestNode = (AITestNode)node;
			bool flag = aitestNode.Evaluate(entity);
			aitestNode.EditorStateDisplay = (flag ? NodeState.Success : NodeState.Fail);
			if (flag)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06001B3B RID: 6971 RVA: 0x000A8864 File Offset: 0x000A6A64
	public override Node.InspectorProps GetInspectorProps()
	{
		Node.InspectorProps inspectorProps = base.GetInspectorProps();
		inspectorProps.Title = "OR";
		inspectorProps.MinInspectorSize = new Vector2(100f, 0f);
		inspectorProps.ShowInspectorView = false;
		inspectorProps.SortX = true;
		return inspectorProps;
	}

	// Token: 0x06001B3C RID: 6972 RVA: 0x000A889A File Offset: 0x000A6A9A
	public AITest_Or()
	{
	}

	// Token: 0x04001BC0 RID: 7104
	[HideInInspector]
	[SerializeField]
	[InputPort(typeof(AITestNode), true, "", PortLocation.Vertical)]
	public List<Node> Tests = new List<Node>();
}
